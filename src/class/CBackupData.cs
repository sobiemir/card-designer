using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.IO.Compression;
using System.ComponentModel;

namespace CDesigner
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



		/// Compress
		/// ------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Kompresja danych.
		/// Kompresuje podane pliki i zapisuje je do jednego.
		/// Uwaga: Podane pliki muszą zawierać plik "update.lst", który zawiera listę spakowanych plików!
		/// </summary>
		/// <param name="files">Lista plików do kompresji.</param>
		/// <param name="output">Nazwa pliku wyjściowego...</param>
		/// ------------------------------------------------------------------------------------------------------------
		public static void Compress( List<string> files, string output )
		{
			List<int> sizes = new List<int>();
			long      lsize = 0;
			byte[]    table = CBackupData.HashTable();
			
			// kompresuj każdy plik do jednego
			foreach( string file in files )
			{
				using( FileStream istream = new FileStream(file, FileMode.Open, FileAccess.Read) )
				using( FileStream ostream = new FileStream("./compress.tmp", FileMode.Append, FileAccess.Write) )
				using( GZipStream compress = new GZipStream(ostream, CompressionMode.Compress) )
					istream.CopyTo( compress );

				// rozmiar skompresowanego pliku
				FileInfo info = new FileInfo( "./compress.tmp" );
				sizes.Add( (int)(info.Length - lsize) );
				lsize = info.Length;
			}

			// informacje o plikach...
			using( FileStream ostream = new FileStream(output, FileMode.Create, FileAccess.Write) )
			{
				/* =============================================
				 * [19]      TIMESTAMP - aktualny czas
				 * [1]       VERLEN    - ilość znaków wersji
				 * [VERLEN]  VERSION   - wersja
				 * [256]     HASHTABLE - klucz szyfrujący
				 * [2]       FILES     - ilość  plików
				 * [4*FILES] FILESIZES - rozmiary plików
				 * [??]      HASHDATA  - dane
				 * ============================================= */
				string tstamp = DateTime.Now.ToString( "dd.MM.yyyy,HH:mm:ss" );

				for( int x = 0, y = tstamp.Count(); x < y; ++x )
					ostream.WriteByte( (byte)tstamp[x] );

				ostream.WriteByte( (byte)InfoForm.VERSION.Length );
				for( int x = 0; x < InfoForm.VERSION.Length; ++x )
					ostream.WriteByte( (byte)InfoForm.VERSION[x] );

				ostream.Write( table, 0, 256 );

				// można sobie darować więcej plików niż 65535...
				ostream.WriteByte( (byte)(files.Count & 0xFF) );
				ostream.WriteByte( (byte)((files.Count & 0xFF00) >> 8) );
			
				// rozmiary plików
				for( int x = 0; x < sizes.Count; ++x )
				{
					ostream.WriteByte( (byte)(sizes[x] & 0xFF) );
					ostream.WriteByte( (byte)((sizes[x] & 0xFF00) >> 8) );
					ostream.WriteByte( (byte)((sizes[x] & 0xFF0000) >> 16) );
					ostream.WriteByte( (byte)((sizes[x] & 0xFF000000) >> 24) );
				}
			}

			// szyfruj dane
			CBackupData.CryptData( table, "./compress.tmp", "./compress.tmp2" );
			
			// kompresuj całość
			using( FileStream istream = new FileStream("./compress.tmp2", FileMode.Open, FileAccess.Read) )
			using( FileStream ostream = new FileStream(output, FileMode.Append, FileAccess.Write) )
			using( GZipStream compress = new GZipStream(ostream, CompressionMode.Compress) )
				istream.CopyTo( compress );

			// usuń pozostałości
			File.Delete( "./compress.tmp" );
			File.Delete( "./compress.tmp2" );
		}

		/// Decompress
		/// ------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Dekompresja danych.
		/// Na początku wyodrębniane są nagłówki pliku, potem rozpakowana zostaje skompresowana całość.
		/// Po rozpakowaniu całości następuje deszyfracja danych i dekompresja pojedynczych plików.
		/// </summary>
		/// <param name="input">Ścieżka do skompresowanego pliku.</param>
		/// <param name="output">Folder wyjściowy...</param>
		/// ------------------------------------------------------------------------------------------------------------
		public static void Decompress( string input, string output )
		{
			byte[] btrash = new byte[32];
			byte[] table  = new byte[256];

			int       files = 0;
			List<int> sizes = new List<int>();

			// dekompresja całego pliku
			using( FileStream istream = new FileStream(input, FileMode.Open, FileAccess.Read) )
			using( FileStream ostream = new FileStream("./decompress.tmp", FileMode.Create, FileAccess.Write) )
			{
				// czas utworzenia
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

				// rozpakuj dane
				using( GZipStream decompress = new GZipStream(istream, CompressionMode.Decompress) )
					decompress.CopyTo( ostream );
			}

			// odwracanie zaszyfrowanych danych
			CBackupData.DecryptData( table, "./decompress.tmp", "./decompress.tmp2" );

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

							using( MemoryStream mstream = new MemoryStream(data) )
							using( GZipStream decompress = new GZipStream(mstream, CompressionMode.Decompress) )
								decompress.CopyTo( ostream );
						}

						// odczytaj nazwy plików
						using( StreamReader rstream = new StreamReader(File.Open(output + names[0], FileMode.Open, FileAccess.Read)) )
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

						using( MemoryStream mstream = new MemoryStream(data) )
						using( GZipStream decompress = new GZipStream(mstream, CompressionMode.Decompress) )
							decompress.CopyTo( ostream );
					}
				}
			}

			// usuń pozostałe pliki
			File.Delete( "./decompress.tmp" );
			File.Delete( "./decompress.tmp2" );
		}

		/// HashTable
		/// ------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Tworzenie klucza szyfrującego.
		/// Klucz szyfrujący losowany jest dzięki klasie <see cref="Random"/>.
		/// Zamiana kluczy w tablicy wykonywana jest 4096 razy, więc dla całej tablicy będzie to 16 powtórzeń.
		/// </summary>
		/// <returns>256 bajtowy klucz szyfrujący.</returns>
		/// ------------------------------------------------------------------------------------------------------------
		private static byte[] HashTable( )
		{
			Random random = new Random();
			byte[] table  = new byte[256];

			// przypisz wartości od 0 do 255
			for( int x = 0; x < 256; ++x )
				table[x] = (byte)x;

			// wymieszaj
			for( int x = 0; x < 4096; ++x )
			{
				int randx = (table[table[random.Next(256)]] + 1) % 256;
				int randy = (table[table[random.Next(256)]] + 1) % 256;

				byte temp    = table[randx];
				table[randx] = table[randy];
				table[randy] = temp;
			}

			// odwróć wartości (xor)
			for( int x = 0; x < 256; ++x )
				table[x] ^= 255;

			return table;
		}

		/// CryptData
		/// ------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Szyfrowanie danych.
		/// Szyfrowanie przebiega częściowo (po 256 bajtów) - każda część szyfrowana jest funkcją VMPC.
		/// f(f(f(x))+1)
		/// </summary>
		/// <param name="table">256 bajtowy klucz szyfrujący (2048 bitowy).</param>
		/// <param name="input">Plik z danymi do szyfrowania.</param>
		/// <param name="output">Plik do zapisu zaszyfrowanych danych.</param>
		/// ------------------------------------------------------------------------------------------------------------
		private static void CryptData( byte[] table, string input, string output )
		{
			using( FileStream istream = new FileStream(input, FileMode.Open, FileAccess.Read) )
			using( FileStream ostream = new FileStream(output, FileMode.Create, FileAccess.Write) )
			{
				byte[] data  = new byte[256];
				byte[] hash  = new byte[256];
				int    bytes = 1;

				// szyfruj funkcją VMPC
				for( int x = 0; bytes != 0; ++x )
				{
					bytes = istream.Read( data, 0, 256 );

					for( int y = 0; y < bytes; ++y )
						if( bytes == 256 )
							hash[y] = table[(table[table[data[y]]] + 1) % bytes];
						else
							hash[y] = (byte)(table[(table[table[data[y]] % bytes] + 1) % bytes] % bytes);

					// zapisz kawałki danych
					ostream.Write( hash, 0, bytes );
				}
			}
		}

		/// DecryptData
		/// ------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Deszyfracja danych.
		/// Deszyfracja przebiega częściowo (po 256 bajtów) - w trakcie każdej części następuje odwrócenie VMPC.
		/// f(f(f(x)+1))
		/// </summary>
		/// <param name="table">256 bajtowy klucz szyfrujący (2048 bitowy).</param>
		/// <param name="input">Plik z danymi do odszyfrowania.</param>
		/// <param name="output">Plik do zapisu odszyfrowanych danych.</param>
		/// ------------------------------------------------------------------------------------------------------------
		private static void DecryptData( byte[] table, string input, string output )
		{
			byte[] reverse = new byte[256];

			// odwróć klucz szyfrujący (zamień klucze z indeksami)
			for( int x = 0; x < 256; ++x )
				reverse[table[x]] = (byte)x;

			using( FileStream istream = new FileStream(input, FileMode.Open, FileAccess.Read) )
			using( FileStream ostream = new FileStream(output, FileMode.Create, FileAccess.Write) )
			{
				byte[] data  = new byte[256];
				byte[] hash  = new byte[256];
				int    bytes = 1;

				// odwróć funkcję VMPC
				for( int x = 0; bytes != 0; ++x )
				{
					bytes = istream.Read( hash, 0, 256 );

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
				}
			}
		}
	}
}
