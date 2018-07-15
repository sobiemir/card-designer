using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.IO.Compression;
using System.ComponentModel;

namespace CDRestore
{
	///
	/// Okrojona klasa kompresji i dekompresji plików.
	/// Wersja dla tej aplikacji zawiera tylko dekompresje plików.
	/// Pozwala na przypisanie dekompresji pliku do określonego wątku, dzięki czemu postęp będzie raportowany
	/// bezpośrednio do wątku. W przeciwnym razie raport zostanie przekazany do zdarzenia OnProgressChanged.
	///
	class CBackupData
	{
		/// Zdarzenie wywoływane przy zmianie postępu dekompresji pliku.
		public event ProgressChangedEventHandler OnProgressChanged = null;

		/// Wątek w którym będzie działała dekompresja...
		private BackgroundWorker _worker = null;

		/// 
		/// Konstruktor klasy CBackupData.
		/// ------------------------------------------------------------------------------------------------------------
		public CBackupData( BackgroundWorker worker = null )
		{
			this._worker = worker;
		}

		/// 
		/// Dekompresja aktualizacji.
		/// Pozwala na dekompresje pliku aktualizacji do podanego folderu.
		/// Po drodze tworzone są dwa pliki: "./decompress.tmp" i "./decompress.tmp2".
		/// Jako argumenty funkcja przyjmuje plik wejściowy i folder wyjściowy.
		/// ------------------------------------------------------------------------------------------------------------
		public void DecompressUpdate( string input, string output )
		{
			byte[] btrash = new byte[32],
				   table  = new byte[256];

			int index = 0,
				files = 0;

			List<int>    sizes  = new List<int>();
			List<double> points = new List<double>();

			using( FileStream istream = new FileStream(input, FileMode.Open, FileAccess.Read) )
			using( FileStream ostream = new FileStream("./decompress.tmp", FileMode.Create, FileAccess.Write) )
			{
				// czas utworzenia pliku
				istream.Read( btrash, 0, 19 );

				// wersja
				istream.Read( btrash, 0, 1 );
				istream.Read( btrash, 0, btrash[0] > 32 ? 32 : btrash[0] );

				// klucz szyfrujący
				istream.Read( table, 0, 256 );

				// ilość plików
				istream.Read( btrash, 0, 2 );
				files = btrash[0] | (btrash[1] << 8);

				// odczytaj rozmiary plików
				for( int x = 0; x < files; ++x )
				{
					istream.Read( btrash, 0, 4 );
					sizes.Add( btrash[0] | (btrash[1] << 8) | (btrash[2] << 16) | (btrash[3] << 24) );
				}

				// oblicz rozmiar przetwarzanych danych
				long total_size = istream.Length;
				for( int x = 0; x < files; ++x )
					total_size += sizes[x];

				// oblicz punkty kontrolne
				points.Add(0);
				points.Add( (double)istream.Length / (double)total_size * 100.0 );
				for( int x = 0; x < files; ++x )
					points.Add( points[x+1] + ((double)sizes[x] / (double)total_size * 100.0) );

				// rozpakuj dane
				using( ProgressStream stream = new ProgressStream(
					istream,
					Convert.ToInt32(points[index]),
					Convert.ToInt32(points[index+1] / 2.0),
					this._worker) )
				using( GZipStream decompress = new GZipStream(stream, CompressionMode.Decompress) )
					decompress.CopyTo( ostream );
				index++;
			}

			// deszyfracja danych
			int decrypt_start = Convert.ToInt32( points[index] / 2.0 ),
				decrypt_stop  = Convert.ToInt32( points[index] );
			this.DecryptData( table, "./decompress.tmp", "./decompress.tmp2", decrypt_start, decrypt_stop );

			// dekompresja pojedynczych plików
			using( FileStream istream = new FileStream("./decompress.tmp2", FileMode.Open, FileAccess.Read) )
			{
				// dodaj slash do ścieżki
				if( output.Last() != '/' && output.Last() != '\\' )
					output += '/';

				List<string> names = new List<string>();
				names.Add( "update.lst" );

				for( int x = 0; x < files; ++x )
				{
					// utwórz odpowiednie foldery...
					new FileInfo(output + names[x]).Directory.Create();

					// przetwórz plik z listą pozostałych plików
					if( x == 0 )
					{
						// wypakuj plik z listą plików
						using( FileStream ostream = new FileStream(output + names[0], FileMode.Create, FileAccess.Write) )
						{
							byte[] data = new byte[sizes[x]];
							istream.Read( data, 0, sizes[x] );

							using( ProgressStream mstream = new ProgressStream(
								new MemoryStream(data),
								Convert.ToInt32(points[index]),
								Convert.ToInt32(points[index+1]),
								this._worker) )
							using( GZipStream decompress = new GZipStream(mstream, CompressionMode.Decompress) )
								decompress.CopyTo( ostream );
							index++;
						}

						// odczytaj nazwy plików
						using( StreamReader rstream = new StreamReader(
							File.Open(output + names[0], FileMode.Open, FileAccess.Read)) )
						{
							string fname = rstream.ReadLine();
							
							while( fname != null )
							{
								fname = fname.Substring( 2 );
								names.Add( fname );
								fname = rstream.ReadLine();
							}
						}
						continue;
					}

					// utwórz plik i zapisz wypakowaną zawartość
					using( FileStream ostream = new FileStream(output + names[x], FileMode.Create, FileAccess.Write) )
					{
						byte[] data = new byte[sizes[x]];
						istream.Read( data, 0, sizes[x] );

						using( ProgressStream mstream = new ProgressStream(
							new MemoryStream(data),
							Convert.ToInt32(points[index]),
							Convert.ToInt32(points[index+1]),
							this._worker) )
						using( GZipStream decompress = new GZipStream(mstream, CompressionMode.Decompress) )
							decompress.CopyTo( ostream );
						index++;
					}
				}
			}

			// usuń pozostałe pliki
			File.Delete( "./decompress.tmp" );
			File.Delete( "./decompress.tmp2" );

			// wyślij informacje o zakończeniu dekompresji
			if( this._worker != null )
				this._worker.ReportProgress( 100, (object)1 );
			else
			{
				ProgressChangedEventArgs ev = new ProgressChangedEventArgs( 100, (object)1 );
				this.OnProgressChanged( this, ev );
			}
		}

		///
		/// Deszyfracja pliku.
		/// Funkcja odszyfrowuje plik dzięki 256 bajtowemu kluczowi szyfrującemu.
		/// Funkcja szyfrująca: VMPC ==> f(f(f(x))+1)
		/// Do funkcji można podać początek od którego funkcja ma zacząć i koniec postępu (domyślnie od 0 do 100).
		/// ------------------------------------------------------------------------------------------------------------
		private void DecryptData( byte[] table, string input, string output, int start = 0, int stop = 100 )
		{
			byte[] reverse = new byte[256];

			// odwróć klucz szyfrujący (zamień klucze z indeksami)
			for( int x = 0; x < 256; ++x )
				reverse[table[x]] = (byte)x;

			using( FileStream istream = new FileStream(input, FileMode.Open, FileAccess.Read) )
			using( FileStream ostream = new FileStream(output, FileMode.Create, FileAccess.Write) )
			{
				byte[] data = new byte[256],
					   hash = new byte[256];
				
				int	bytes = 1,
					gets  = 0,
					last  = start;

				// odwróć funkcję VMPC
				for( int x = 0; bytes != 0; ++x )
				{
					bytes = istream.Read( hash, 0, 256 );
					gets += bytes;

					for( int y = 0; y < bytes; ++y )
						if( bytes == 256 )
							data[y] = reverse[reverse[unchecked((byte)(reverse[hash[y]]-1))]];
						else
						{
							int hdat = reverse[hash[y]] - 1;
							if( hdat < 0 )
								hdat = bytes;
							data[y] = reverse[reverse[hdat]];
						}

					// zapisz kawałki danych
					ostream.Write( data, 0, bytes );

					if( this._worker != null || this.OnProgressChanged != null )
					{
						// oblicz ile procent już odczytano
						int percent = start + Convert.ToInt32( (double)gets / (double)istream.Length *
							(double)(stop-start) );
			
						// granice...
						percent = percent > stop ? stop : percent;
						percent = percent < start ? start : percent;

						// raportuj postęp deszyfracji
						if( percent > last )
							if( this._worker != null )
								this._worker.ReportProgress( percent, (object)0 );
							else if( this.OnProgressChanged != null )
							{
								ProgressChangedEventArgs ev = new ProgressChangedEventArgs( percent, (object)0 );
								this.OnProgressChanged( this, ev );
							}

						last = percent;
					}
				}
			}
		}
	}
}
