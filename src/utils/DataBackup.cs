///
/// $u06 DataBackup.cs
/// 
/// Plik zawierający klasę do kompresji i szyfrowania danych.
/// Wszelkie aktualizacje przed utworzeniem przepuszczane są przez tą klasę.
/// To samo tyczy się tworzenia wzorów do importu z tą różnicą, że nie są one szyfrowane.
/// Klasa posiada osobne funkcje do dekompresji i deszyfracji aktualizacji z uwagi
/// na inną strukturę w budowie nagłówków.
/// 
/// Autor: Kamil Biały
/// Od wersji: 0.6.x
/// Ostatnia zmiana: 2016-12-24
/// 
/// CHANGELOG:
/// [27.06.2015] Pierwsza wersja klasy kompresji i szyfrowania plików. 
/// [13.11.2016] Zmiana nazwy z CDataBackup na DataBackup.
/// [04.12.2016] Poprawki w kompresji - możliwość kompresji bez szyfrowania.
/// [24.12.2016] Komentarze, regiony.
///

using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.IO.Compression;
using System.ComponentModel;

namespace CDesigner.Utils
{
	///
	/// Klasa kompresji i dekompresji plików.
	/// Pozwala na przypisanie dekompresji pliku do określonego wątku, dzięki czemu postęp będzie raportowany
	/// bezpośrednio do wątku. W przeciwnym razie raport zostanie przekazany do zdarzenia OnProgressChanged.
	/// Pozwala na tworzenie kompresowanych zarówno z szyfrowaniem jak i bez.
	/// Tworzone aktualizacje muszą posiadać plik "update.lst", który zawiera wszystkie kompresowane pliki!
	///
	class DataBackup
	{
#region ZMIENNE / ZDARZENIA

		/// <summary>Zdarzenie wywoływane przy zmianie postępu kompresji pliku.</summary>
		public event ProgressChangedEventHandler OnProgressChanged;

		/// <summary>Wątek w którym uruchomiona zostanie dekompresja.</summary>
		private BackgroundWorker _worker;

#endregion

#region KONSTRUKTOR / WŁAŚCIWOŚCI

		/// <summary>
		/// Konstruktor klasy DataBackup.
		/// Przypisuje wątek do zmiennej, na którym będzie działała dekompresja.
		/// </summary>
		/// 
		/// <param name="worker">Wątek dla dekompresji.</param>
		//* ============================================================================================================
		public DataBackup( BackgroundWorker worker = null )
		{
			this.OnProgressChanged = null;

			this._worker = worker;
		}

		/// <summary>
		/// Pobiera wątek przypisany do klasy.
		/// Instancja klasy może mieć tylko jeden przypisany do niej wątek.
		/// Właściwość tylko do odczytu.
		/// </summary>
		//* ============================================================================================================
		public BackgroundWorker BackgroundWorker
		{
			get { return this._worker; }
		}

#endregion

#region AKTUALIZACJE

		/// <summary>
		/// Dekompresja aktualizacji.
		/// Pozwala na dekompresje pliku aktualizacji do podanego folderu.
		/// Po drodze tworzone są dwa pliki: "decompress.tmp" i "decompress.tmp2".
		/// Pierwszy jest wynikiem dekompresji danych, drugi deszyfracji z którego potem składane są pliki.
		/// </summary>
		/// 
		/// <param name="input">Plik wejściowy do dekompresji.</param>
		/// <param name="output">Folder wyjściowy do którego plik będzie wypakowywany.</param>
		//* ============================================================================================================
		public void decompressUpdate( string input, string output )
		{
			var btrash = new byte[32];
			var table  = new byte[256];

			int index = 0,
				files = 0;

			var sizes  = new List<int>();
			var points = new List<double>();

			using( var istream = new FileStream(input, FileMode.Open, FileAccess.Read) )
			using( var ostream = new FileStream("./decompress.tmp", FileMode.Create, FileAccess.Write) )
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
				using( var stream = new ProgressStream(
					istream,
					Convert.ToInt32(points[index]),
					Convert.ToInt32(points[index+1] / 2.0),
					this._worker) )
				using( var decompress = new GZipStream(stream, CompressionMode.Decompress) )
					decompress.CopyTo( ostream );
				index++;
			}

			// deszyfracja danych
			int decrypt_start = Convert.ToInt32( points[index] / 2.0 ),
				decrypt_stop  = Convert.ToInt32( points[index] );
			this.decryptData( table, "./decompress.tmp", "./decompress.tmp2", decrypt_start, decrypt_stop );

			// dekompresja pojedynczych plików
			using( var istream = new FileStream("./decompress.tmp2", FileMode.Open, FileAccess.Read) )
			{
				// dodaj slash do ścieżki
				if( output.Last() != '/' && output.Last() != '\\' )
					output += '/';

				var names = new List<string>();
				names.Add( "update.lst" );

				for( int x = 0; x < files; ++x )
				{
					// utwórz odpowiednie foldery...
					new FileInfo(output + names[x]).Directory.Create();

					// przetwórz plik z listą pozostałych plików
					if( x == 0 )
					{
						// wypakuj plik z listą plików
						using( var ostream = new FileStream(output + names[0], FileMode.Create, FileAccess.Write) )
						{
							byte[] data = new byte[sizes[x]];
							istream.Read( data, 0, sizes[x] );

							using( var mstream = new ProgressStream(
								new MemoryStream(data),
								Convert.ToInt32(points[index]),
								Convert.ToInt32(points[index+1]),
								this._worker) )
							using( var decompress = new GZipStream(mstream, CompressionMode.Decompress) )
								decompress.CopyTo( ostream );
							index++;
						}

						// odczytaj nazwy plików
						using( var rstream = new StreamReader(
							File.Open(output + names[0], FileMode.Open, FileAccess.Read)) )
						{
							rstream.ReadLine();
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
					using( var ostream = new FileStream(output + names[x], FileMode.Create, FileAccess.Write) )
					{
						byte[] data = new byte[sizes[x]];
						istream.Read( data, 0, sizes[x] );

						using( var mstream = new ProgressStream(
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
				var ev = new ProgressChangedEventArgs( 100, (object)1 );
				this.OnProgressChanged( this, ev );
			}
		}

		/// <summary>
		/// Deszyfracja pliku.
		/// Funkcja odkodowuje plik dzięki 256 bajtowemu kluczowi szyfrującemu przekazywanego w parametrze.
		/// Funkcja szyfrująca: VMPC ==> f(f(f(x))+1).
		/// Dzięki ostatnim dwóm argumentom, funkcja może rozpocząć postęp deszyfracji od wartości innej niż 0
		/// i zakończyć na wartości innej niż 100.
		/// </summary>
		/// 
		/// <param name="table">Tabela z kluczem szyfrującym.</param>
		/// <param name="input">Plik wejściowy do deszyfracji.</param>
		/// <param name="output">Plik wyjściowy odszyfrowany.</param>
		/// <param name="start">Wartość rozpoczynająca postęp (od 0 do 100).</param>
		/// <param name="stop">Wartość kończąca postęp (od 0 do 100).</param>
		//* ============================================================================================================
		private void decryptData( byte[] table, string input, string output, int start = 0, int stop = 100 )
		{
			byte[] reverse = new byte[256];

			// odwróć klucz szyfrujący (zamień klucze z indeksami)
			for( int x = 0; x < 256; ++x )
				reverse[table[x]] = (byte)x;

			using( var istream = new FileStream(input, FileMode.Open, FileAccess.Read) )
			using( var ostream = new FileStream(output, FileMode.Create, FileAccess.Write) )
			{
				var data = new byte[256];
				var hash = new byte[256];
				
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
								var ev = new ProgressChangedEventArgs( percent, (object)0 );
								this.OnProgressChanged( this, ev );
							}

						last = percent;
					}
				}
			}
		}

#endregion

#region SZYFROWANIE / KOMPRESJA

		/// <summary>
		/// Szyfrowanie danych.
		/// Szyfrowanie przebiega częściowo (po 256 bajtów) - każda część szyfrowana jest funkcją VMPC.
		/// f(f(f(x))+1).
		/// </summary>
		/// 
		/// <param name="table">256 bajtowy klucz szyfrujący (2048 bity).</param>
		/// <param name="input">Plik z danymi do szyfrowania.</param>
		/// <param name="output">Plik do zapisu szyfrowanych danych.</param>
		//* ============================================================================================================
		private static void CryptData( byte[] table, string input, string output )
		{
			using( var istream = new FileStream(input, FileMode.Open, FileAccess.Read) )
			using( var ostream = new FileStream(output, FileMode.Create, FileAccess.Write) )
			{
				var data  = new byte[256];
				var hash  = new byte[256];
				int bytes = 1;

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

		/// <summary>
		/// Deszyfracja pliku.
		/// Deszyfracja przebiega częściowo (po 256 bajtów) - w trakcie każdej części następuje odwrócenie VMPC.
		/// f(f(f(x)+1))
		/// </summary>
		/// 
		/// <param name="table">256 bajtowy klucz szyfrujący (2048 bitowy).</param>
		/// <param name="input">Plik z danymi do odszyfrowania.</param>
		/// <param name="output">Plik do zapisu odszyfrowanych danych.</param>
		//* ============================================================================================================
		private static void DecryptData( byte[] table, string input, string output )
		{
			byte[] reverse = new byte[256];

			// odwróć klucz szyfrujący (zamień klucze z indeksami)
			for( int x = 0; x < 256; ++x )
				reverse[table[x]] = (byte)x;

			using( FileStream istream = new FileStream(input, FileMode.Open, FileAccess.Read) )
			using( FileStream ostream = new FileStream(output, FileMode.Create, FileAccess.Write) )
			{
				var data  = new byte[256];
				var hash  = new byte[256];
				int bytes = 1;

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

		/// <summary>
		/// Kompresja danych.
		/// Funkcja kompresuje podane pliki i zapisuje do jednego.
		/// Podane pliku muszą zawierać plik "update.lst", który w treści ma listę wszystkich pakowanych plików.
		/// Funkcja posiada możliwość szyfrowania danych, poprzez ustawienie ostatniego argumentu na TRUE.
		/// Schemat układu danych dla kompresji:
		/// <table>
		///     <tr><th>Bajty</th><th>Nazwa</th><th>Opis</th></tr>
		///     <tr><td>19</td><td>TIMESTAMP</td><td>Czas utworzenia pliku.</td></tr>
		///     <tr><td>1</td><td>VERLEN</td><td>Ilość znaków w kolejnym pliku.</td></tr>
		///     <tr><td>VERLEN</td><td>VERSION</td><td>Wersja aplikacji która utworzyła plik.</td></tr>
		///     <tr><td>256</td><td>HASHTABLE</td><td>Klucz szyfrujący plik lub dwa zera gdy brak.</td></tr>
		///     <tr><td>2</td><td>FILES</td><td>Ilość skompresowanych plików.</td></tr>
		///     <tr><td>4 * FILES</td><td>FILESIZES</td><td>Rozmiary wszystkich skompresowanych plików.</td></tr>
		///     <tr><td>??</td><td>HASHDATA</td><td>Dane skompresowane.</td></tr>
		/// </table>
		/// </summary>
		/// 
		/// <param name="files">Lista plików do kompresji.</param>
		/// <param name="output">Plik wyjściowy, skompresowany.</param>
		/// <param name="crypt">Szyfrowanie pliku wyjściowego, domyślnie FALSE.</param>
		//* ============================================================================================================
		public static void Compress( List<string> files, string output, bool crypt = true )
		{
			var  sizes = new List<int>();
			long lsize = 0;
			var  table = DataBackup.HashTable();
			
			File.Delete( "./compress.tmp" );
			File.Delete( "./compress.tmp2" );

			// kompresuj każdy plik do jednego
			foreach( string file in files )
			{
				using( var istream = new FileStream(file, FileMode.Open, FileAccess.Read) )
				using( var ostream = new FileStream("./compress.tmp", FileMode.Append, FileAccess.Write) )
				using( var compress = new GZipStream(ostream, CompressionMode.Compress) )
					istream.CopyTo( compress );

				// rozmiar skompresowanego pliku
				var info = new FileInfo( "./compress.tmp" );
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

				ostream.WriteByte( (byte)Program.VERSION.Length );
				for( int x = 0; x < Program.VERSION.Length; ++x )
					ostream.WriteByte( (byte)Program.VERSION[x] );

				// tablica szyfrująca
				if( crypt )
					ostream.Write( table, 0, 256 );
				// lub bez tablicy
				else
				{
					ostream.WriteByte( 0 );
					ostream.WriteByte( 0 );
				}

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
			if( crypt )
				DataBackup.CryptData( table, "./compress.tmp", "./compress.tmp2" );
			else
				File.Copy( "./compress.tmp", "./compress.tmp2" );
			
			// kompresuj całość
			using( FileStream istream = new FileStream("./compress.tmp2", FileMode.Open, FileAccess.Read) )
			using( FileStream ostream = new FileStream(output, FileMode.Append, FileAccess.Write) )
			using( GZipStream compress = new GZipStream(ostream, CompressionMode.Compress) )
				istream.CopyTo( compress );

			// usuń pozostałości
			File.Delete( "./compress.tmp" );
			File.Delete( "./compress.tmp2" );
		}

		/// <summary>
		/// Dekompresja danych.
		/// Funkcja na początku wyodrębnia nagłówki pliku, po czym rozpakowuje skompresowaną całość.
		/// Po rozpakowaniu, gdy plik jest szyfrowany, następuje jego deszyfracja i dekompresja pojedynczych plików.
		/// Gdy plik nie jest szyfrowany, od razu następuje jego dekompresja.
		/// </summary>
		/// 
		/// <param name="input">Ścieżka do skompresowanego pliku.</param>
		/// <param name="output">Folder wyjściowy do którego dane będą wypakowywane.</param>
		//* ============================================================================================================
		public static void Decompress( string input, string output )
		{
			var btrash = new byte[32];
			var table  = new byte[256];
			var crypt  = false;

			int files = 0;
			var sizes = new List<int>();

			File.Delete( "./decompress.tmp" );
			File.Delete( "./decompress.tmp2" );

			// dekompresja całego pliku
			using( var istream = new FileStream(input, FileMode.Open, FileAccess.Read) )
			using( var ostream = new FileStream("./decompress.tmp", FileMode.Create, FileAccess.Write) )
			{
				// czas utworzenia
				istream.Read( btrash, 0, 19 );

				// wersja
				istream.Read( btrash, 0, 1 );
				istream.Read( btrash, 0, btrash[0] > 32 ? 32 : btrash[0] );

				// klucz szyfrujący
				istream.Read( table, 0, 2 );
				if( !(table[0] == 0 && table[1] == 0) )
				{
					crypt = true;
					istream.Read( table, 2, 254 );
				}

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
				using( var decompress = new GZipStream(istream, CompressionMode.Decompress) )
					decompress.CopyTo( ostream );
			}

			// odwracanie zaszyfrowanych danych
			if( crypt )
				DataBackup.DecryptData( table, "./decompress.tmp", "./decompress.tmp2" );
			else
				File.Copy( "./decompress.tmp", "./decompress.tmp2" );

			// dekompresja pojedynczych plików
			using( var istream = new FileStream("./decompress.tmp2", FileMode.Open, FileAccess.Read) )
			{
				// dodaj slash do ścieżki
				if( output.Last() != '/' && output.Last() != '\\' )
					output += '/';

				var names = new List<string>();
				names.Add( "update.lst" );

				for( int x = 0; x < files; ++x )
				{
					// utwórz odpowiednie foldery...
					new FileInfo(output + names[x]).Directory.Create();

					// przetwórz plik z listą pozostałych plików
					if( x == 0 )
					{
						// wypakuj plik z listą plików
						using( var ostream = new FileStream(output + names[0], FileMode.Create, FileAccess.Write) )
						{
							byte[] data = new byte[sizes[x]];
							istream.Read( data, 0, sizes[x] );

							using( var mstream = new MemoryStream(data) )
							using( var decompress = new GZipStream(mstream, CompressionMode.Decompress) )
								decompress.CopyTo( ostream );
						}

						// odczytaj nazwy plików
						using( var rstream = new StreamReader(File.Open(output + names[0], FileMode.Open, FileAccess.Read)) )
						{
							rstream.ReadLine();
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
					using( var ostream = new FileStream(output + names[x], FileMode.Create, FileAccess.Write) )
					{
						byte[] data = new byte[sizes[x]];
						istream.Read( data, 0, sizes[x] );

						using( var mstream = new MemoryStream(data) )
						using( var decompress = new GZipStream(mstream, CompressionMode.Decompress) )
							decompress.CopyTo( ostream );
					}
				}
			}

			// usuń pozostałe pliki
			File.Delete( "./decompress.tmp" );
			File.Delete( "./decompress.tmp2" );
		}

#endregion

#region POZOSTAŁE

		/// <summary>
		/// Tworzenie listy plików.
		/// Funkcja zapisuje listę plików do wybranego pliku - do listy dodawany jest nagłówek z wersją programu.
		/// Przydatne przy tworzeniu pliku "update.lst".
		/// </summary>
		/// 
		/// <param name="files">Lista plików do zapisania.</param>
		/// <param name="outpath">Plik wyjściowy z listą.</param>
		//* ============================================================================================================
		public static void CreateFileList( List<string> files, string outpath )
		{
			// usuń plik jeżeli istnieje
			File.Delete( outpath );

			// utwórz nowy z listą
			using( var writer = new StreamWriter(File.Open(outpath, FileMode.OpenOrCreate, FileAccess.Write)) )
			{
				writer.WriteLine( "[" + Program.VERSION + "]" );

				foreach( var file in files )
					writer.WriteLine( file );
			}
		}

		/// <summary>
		/// Tworzenie klucza szyfrującego.
		/// Klucz szyfrujący losowany jest dzięki klasie Random, nie jest to bezpieczna funkcja losowości.
		/// Zamiana kluczy w tablicy wykonywana jest 4096 razy, więc dla całej tablicy będzie to 16 powtórzeń.
		/// </summary>
		/// 
		/// <returns>256 bajtowy klucz szyfrujący.</returns>
		//* ============================================================================================================
		private static byte[] HashTable()
		{
			var random = new Random();
			var table  = new byte[256];

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

#endregion
	}
}
