///
/// $u02 PatternEditor.cs
/// 
/// Plik zawierający klasę zarządzania wzorami aplikacji.
/// Pozwala na utworzenie, klonowanie i usuwanie poszczególnych wzorów.
/// Dodatkowo umożliwia wyświetlanie wzorów w podanym panelu (generowanie podglądu) w dwóch wersjach,
/// wersja szkicu - dla generatora (dynamiczne dane) - i wersja pełna - dla edytora.
/// Umożliwia również zapis wzoru w trzech wariantach - zapis do pliku PDF, zapis wzoru do pliku
/// konfiguracyjnego oraz zapis do pliku JPEG jako podgląd wzoru, wyświetlany w głównym formularzu aplikacji.
/// 
/// Autor: Kamil Biały
/// Od wersji: 0.2.x.x
/// Ostatnia zmiana: 2016-12-25
/// 
/// CHANGELOG:
/// [05.05.2015] Pierwsza wersja klasy.
/// [10.05.2015] Zmiana konwencji zapisu danych dla koloru i obrazu (i/c zamieniono na 1/0),
///              ulepszono funkcję odczytu danych ze wzoru, funkcja rysowania podglądu dla generatora,
///              dodano możliwość rysowania względem skali wzoru.
/// [16.05.2015] Zmieniono organizację zapisu do pliku (dodano ciąg CDCFG), zamiana wymiarów z liczb całkowitych
///              na liczby zmiennoprzecinkowe, funkcja generowania wzoru do pliku PDF.
/// [01.06.2015] Granice dla rodzica (blokada rysowania kontrolki poza granicami).
/// [06.06.2015] Transformacja wyświetlanego tekstu, zmiana kontrolki Panel na AlignedPage dla wyświetlania stron.
/// [09.06.2015] Poprawne dodawanie stron do pliku PDF (nie uwzględniało stron wzoru),
///              usuwanie starych plików konfiguracyjnych.
/// [11.11.2016] Funkcja pobierania listy wzorów, klonowanie wzoru, tworzenie folderów podczas tworzenia wzoru.
/// [12.04.2016] Import i eksport wzorów.
/// [16.12.2016] Zamiana klasy DataContent na DataStorage w funkcji DrawRow.
/// [25.12.2016] Porządkowanie kodu, komentarze, regiony.
/// [26.12.2016] Generowanie koloru strony do PDF.
/// [27.12.2016] Generowanie statycznych kopii wzoru bez danych - podawana ilość kopii.
/// [16.01.2017] Poprawa importu danych.
///

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Windows.Forms;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using PdfSharp.Pdf;
using PdfSharp.Drawing;
using PdfSharp.Drawing.Layout;

using CDesigner.Controls;

namespace CDesigner.Utils
{
	/// 
	/// <summary>
	/// Klasa pozwalająca na zarządzanie wzorami.
	/// Wzory wymagają zarówno utworzenia, jak i zapisania, wygenerowania i wyświetlenia.
	/// Klasa spełnia wszelkie powyższe funkcje, pozwala na utworzenie nowego wzoru o podanej nazwie,
	/// klonowanie go i zapis nowych danych dla edytowanego wzoru.
	/// Dodatkowo pozwala na wyświetlanie wzoru w postaci podglądu wydruku i wyświetlanie całego dla edytora.
	/// Proces zapisu składa się z kilku części, co za tym idzie, generowanie dokumentu <em>PDF</em>,
	/// zapis wzoru do pliku konfiguracyjnego (prosty zapis) oraz zapis do pliku <em>JPG</em> dla podglądu.
	/// Klasa umożliwia również import i eksport wzorów, zapisanych w folderze <em>patterns</em>.
	/// </summary>
	/// 
	public class PatternEditor
	{
#region ZMIENNE

		/// <summary>Ilość pikseli na DPI.</summary>
		/// @hideinitializer
		private static double _pixelPerDPI = 3.93714927048264;

		/// <summary>Nazwy dostępnych formatów.</summary>
		/// @hideinitializer
		public static readonly string[] FormatNames = {
			"A4",
			"A5"
		};

#endregion

		/// <summary>
		/// Wykrywa rozmiar z podanych rozmiarów wzoru.
		/// Wymiary zdefiniowane są w tablicy FormatNames, funkcja zwraca tylko jej indeks.
		/// </summary>
		/// 
		/// <param name="width">Szerokość wzoru.</param>
		/// <param name="height">Wysokość wzoru.</param>
		/// 
		/// <returns>Indeks nazwy formatu.</returns>
		//* ============================================================================================================
		public static int DetectFormat( int width, int height )
		{
			int[,] format_dims = { {210, 297}, {148, 210} };

			// szukaj formatu po rozmiarze
			for( int x = 0; x < format_dims.GetLength(0); ++x )
				if( format_dims[x,0] == width && format_dims[x,1] == height )
					return x;

			return -1;
		}

		/// <summary>
		/// Pobiera listę dostępnych wzorów.
		/// Wszystkie wzory przechowywane są w folderze patterns.
		/// Nazwa folderu jest jednocześnie nazwą folderu w którym się znajduje.
		/// </summary>
		/// 
		/// <returns>Lista dostępnych wzorów.</returns>
		//* ============================================================================================================
		public static List<PatternData> GetPatterns()
		{
			// pobierz listę folderów
			var patterns    = new List<PatternData>();
			var directories = Directory.GetDirectories( "patterns" );

			foreach( string pattern in directories )
			{
				// na linuksie są slashe a nie backslashe
				string patname = pattern.Replace( "patterns\\", "" );
					   patname = patname.Replace( "patterns/", "" );

				// dodaj wzór do listy
				patterns.Add( PatternEditor.ReadPattern(patname, true) );
			}

			return patterns;
		}

		/// <summary>
		/// Import wzoru z podanego pliku do programu.
		/// Potrafi zaimportować więcej niż jeden wzór, gdy są one zapisane w pliku.
		/// Do operacji tworzony jest tymczasowy folder temp, gdzie zapisywane są wypakowane pliki.
		/// Po rozpakowaniu plików podmienia lub kopiuje wszystkie foldery do folderu patterns.
		/// </summary>
		/// 
		/// <param name="file">Plik z wzorem / wzorami do importu.</param>
		/// 
		/// <seealso cref="Export"/>
		//* ============================================================================================================
		public static void Import( string file )
		{
			// usuń folder tymczasowy jeżeli istnieje
			if( Directory.Exists("./temp") )
				Directory.Delete( "./temp", true );

			// utwórz i rozpakuj dane
			Directory.CreateDirectory( "./temp" );
			DataBackup.Decompress( file, "./temp" );
			
			// sprawdź czy rozpakowane dane zawierają folder patterns
			if( Directory.Exists("./temp/patterns/") )
			{
				// jeżeli tak, kopiuj wszystkie wzory do folderu patterns
				var dirs = Directory.GetDirectories( "./temp/patterns/" );
				foreach( var dir in dirs )
				{
					var idir = new DirectoryInfo( dir );
					if( Directory.Exists("./patterns/" + idir.Name) )
						Directory.Delete( "./patterns/" + idir.Name );

					Directory.Move( "./temp/patterns/" + idir.Name, "./patterns/" + idir.Name );

					if( !Directory.Exists("./patterns/" + idir.Name + "/images") )
						Directory.CreateDirectory( "./patterns/" + idir.Name + "/images" );
				}
			}
			// usuń folder tymczasowy
			Directory.Delete( "./temp", true );
		}

		/// <summary>
		/// Eksport wzoru do pliku o podanej w argumencie nazwie.
		/// Eksportuje wzór podany w argumencie poprzez jego kompresje bez szyfrowania.
		/// Możliwy jest eksport kilku wzorów poprzez podanie pustego ciągu znaków w argumencie dla nazwy wzoru.
		/// </summary>
		/// 
		/// <param name="pattern">Nazwa wzoru do eksportu lub pusty ciąg znaków.</param>
		/// <param name="outpath">Nazwa pliku wyjściowego.</param>
		/// 
		/// <seealso cref="Import"/>
		//* ============================================================================================================
		public static void Export( string pattern, string outpath )
		{
			// sprawdź czy istnieje podany wzór
			if( !Directory.Exists("./patterns/" + pattern) )
				return;

			// pobierz wszystkie pliki z podanego wzoru i kompresuj je
			var files = Program.GetFilesFromFolder( "./patterns/" + pattern, true );
			DataBackup.CreateFileList( files, "./patterns/update.lst" );
			files.Insert( 0, "./patterns/update.lst" );
			
			DataBackup.Compress( files, outpath, false );
			
			// usuń plik z listą plików
			File.Delete( "./patterns/update.lst" );
		}

		/// <summary>
		/// Usuwanie wzoru z programu.
		/// Funkcja usuwa wzór z aplikacji poprzez usunięcie jego folderu.
		/// </summary>
		/// 
		/// <param name="pattern">Nazwa wzoru do usunięcia.</param>
		//* ============================================================================================================
		public static void Delete( string pattern )
		{
			// sprawdź czy przypadkiem aktualnie nie jest wczytany ten szablon
			// usuń z listy ostatnio używanych

			// usuń folder i jego wszystkie pliki jeżeli tylko istnieje
			if( Directory.Exists("patterns/" + pattern) )
				Directory.Delete( "patterns/" + pattern, true );
		}

		/// <summary>
		/// Tworzenie nowego wzoru o podanych wymiarach i nazwie.
		/// Funkcja tworzy nowy wzór poprzez utworzenie folderu i umieszczenie w nim pliku konfiguracyjnego.
		/// Plik konfiguracyjny posiada odpowiednią strukturę opartą hierarchie drzewa.
		/// Składa się on z 3 różnych składowych - dane wzoru, dane strony i dane pola, opisane w tabeli poniżej:
		/// <table>
		///     <tr><th>Bajty</th><th>Nazwa</th><th>Opis</th></tr>
		///     <tr><td>5</td><td>IDENTITY</td><td>Identyfikator, zawsze 5 litery CDCFG.</td></tr>
		///     <tr><td>2</td><td>WIDTH</td><td>Szerokość wzoru wyrażona w milimetrach.</td></tr>
		///     <tr><td>2</td><td>HEIGHT</td><td>Wysokość wzoru wyrażona w milimetrach.</td></tr>
		///     <tr><td>1</td><td>PAGES</td><td>Ilość stron znajdujących się we wzorze.</td></tr>
		///     <tr><td>1</td><td>DYNAMIC</td><td>Treść dynamiczna - czy dane mają być wczytywane.</td></tr>
		///     <tr><th colspan="3">Pętla danych strony</th></tr>
		///     <tr><td>1</td><td>FIELDS</td><td>Ilość pól utworzonych na stronie.</td></tr>
		///     <tr><td>1</td><td>USEIMAGE</td><td>Czy strona używa obrazka w tle?</td></tr>
		///     <tr><td>4</td><td>COLOR</td><td>Wyświetlany kolor strony.</td></tr>
		///     <tr><th colspan="3">Pętla danych pola</th></tr>
		///     <tr><td>?</td><td>NAME</td><td>Tekst wyświetlany na polu.</td></tr>
		///     <tr><td>4</td><td>POSX</td><td>Pozycja pola względem osi X.</td></tr>
		///     <tr><td>4</td><td>POSY</td><td>Pozycja pola względem osi Y.</td></tr>
		///     <tr><td>4</td><td>WIDTH</td><td>Szerokość pola w milimetrach.</td></tr>
		///     <tr><td>4</td><td>HEIGHT</td><td>Wysokość pola w milimetrach.</td></tr>
		///     <tr><td>4</td><td>BORDERSIZE</td><td>Rozmiar ramki rysowanej dookoła pola.</td></tr>
		///     <tr><td>4</td><td>BORDERCOLOR</td><td>Kolor ramki rysowanej dookoła pola.</td></tr>
		///     <tr><td>1</td><td>USEIMAGE</td><td>Wyświetlanie obrazu jako tła w polu.</td></tr>
		///     <tr><td>4</td><td>COLOR</td><td>Kolor tła wyświetlanego przed ramką i tekstem.</td></tr>
		///     <tr><td>4</td><td>FONTCOLOR</td><td>Kolor czcionki dla tekstu.</td></tr>
		///     <tr><td>?</td><td>FONTNAME</td><td>Nazwa czcionki którą wypisywany jest tekst.</td></tr>
		///     <tr><td>1</td><td>FONTSTYLE</td><td>Style czcionki (pogrubienie, pochylenie, itp).</td></tr>
		///     <tr><td>4</td><td>FONTSIZE</td><td>Rozmiar wyświetlanego tekstu.</td></tr>
		///     <tr><td>4</td><td>TEXTALIGN</td><td>Położenie tekstu względem pola.</td></tr>
		///     <tr><td>1</td><td>TRANSFORM</td><td>Transformacja tekstu (duże, małe litery, itp).</td></tr>
		///     <tr><td>1</td><td>MARGIN</td><td>Margines dla tekstu wyświetlanego w polu.</td></tr>
		///     <tr><td>4</td><td>MARGINLR</td><td>Margines lewy i prawy.</td></tr>
		///     <tr><td>4</td><td>MARGINTB</td><td>Margines górny i dolny.</td></tr>
		///     <tr><td>4</td><td>PADDING</td><td>Margines wewnętrzny dla wyświetlanego tekstu.</td></tr>
		///     <tr><td>1</td><td>IMAGEDB</td><td>Tło pola pobierane z bazy danych.</td></tr>
		///     <tr><td>1</td><td>PRINTCOLOR</td><td>Generowanie dokumentu z kolorem pola.</td></tr>
		///     <tr><td>1</td><td>PRINTIMAGE</td><td>Generowanie dokumentu z obrazem pola.</td></tr>
		///     <tr><td>1</td><td>TEXTDB</td><td>Napis na polu pobierany z bazy danych.</td></tr>
		///     <tr><td>1</td><td>PRINTTEXT</td><td>Generowanie dokumentu z tekstem (tekst statyczny).</td></tr>
		///     <tr><td>1</td><td>PRINTBORDER</td><td>Generowanie dokumentu razem z ramką pola.</td></tr>
		///     <tr><td>4</td><td>STICKPOINT</td><td>Punkt zaczepienia pola względem którego liczone są wymiary.</td></tr>
		///     <tr><th colspan="3"></th></tr>
		///     <tr><td>1</td><td>PRINTCOLOR</td><td>Generowanie dokumentu razem z kolorem strony.</td></tr>
		///     <tr><td>1</td><td>PRINTIMAGE</td><td>Generowanie dokumentu razem z obrazem strony.</td></tr>
		/// </table>
		/// </summary>
		/// 
		/// <param name="pattern">Nazwa wzoru do utworzenia.</param>
		/// <param name="width">Szerokość wzoru.</param>
		/// <param name="height">Wysokość wzoru.</param>
		//* ============================================================================================================
		public static void Create( string pattern, short width, short height )
		{
			Directory.CreateDirectory( "patterns/" + pattern );
			Directory.CreateDirectory( "patterns/" + pattern + "/images" );

			var file   = new FileStream( "patterns/" + pattern + "/config.cfg", FileMode.OpenOrCreate );
			var writer = new BinaryWriter( file );

			/*
			 * Struktura wzoru:
			 * 5 | CDCFG
			 * --------------------------------------------
			 * 2 | Szerokość
			 * 2 | Wysokość
			 * 1 | Ilość stron
			 * 1 | Treść dynamiczna
			 TODO: 2 | Ilość wszystkich kontrolek
			 TODO: ? | Opis ??
			 * ===================== LOOP =================
			 *   1 | Ilość kontrolek na stronie
			 *   1 | Użycie obrazu tła
			 *   4 | Kolor strony
			 * +++++++++++++++++++++ FIELD LOOP +++++++++++
			 *     ? | Nazwa pola
			 *     4 | Pozycja X
			 *     4 | Pozycja Y
			 *     4 | Szerokość pola
			 *     4 | Wysokość pola
			 *     4 | Grubość ramki
			 *     4 | Kolor ramki
			 *     1 | Użycie obrazu tła
			 *     4 | Kolor tła
			 *     4 | Kolor czcionki
			 *     ? | Nazwa czcionki
			 *     1 | Styl czcionki
			 *     4 | Rozmiar czcionki
			 *     4 | Położenie tekstu
			 *     1 | Transformacja tekstu
			 *     1 | Margines tekstu
			 *     4 | Margines lewy-prawy
			 *     4 | Margines górny-dolny
			 *     4 | Margines wewnętrzny
			 *     1 | Tło z bazy danych
			 *     1 | Drukuj kolor
			 *     1 | Drukuj obraz
			 *     1 | Napis z bazy danych
			 *     1 | Drukuj tekst
			 *     1 | Drukuj ramkę
			 *     4 | Punkt zaczepienia
			 * ++++++++++++++++++++++++++++++++++++++++++++
			 *   1 | Drukuj kolor strony
			 *   1 | Drukuj obraz strony
			 * ============================================
			 TODO: 2 | Data utworzenia
			 TODO: 2 | Godzina utworzenia
			 TODO: 2 | Rok utworzenia
			 * --------------------------------------------
			 TODO: 5 | CDFOT
			*/

			// ciąg rozpoznawczy
			var text = new byte[5] { (byte)'C', (byte)'D', (byte)'C', (byte)'F', (byte)'G' };
			writer.Write( text, 0, 5 );

			writer.Write( width );
			writer.Write( height );
			writer.Write( (byte)1 );
			writer.Write( false );
			writer.Write( (byte)0 );
			writer.Write( false );
			writer.Write( SystemColors.Window.ToArgb() );
			writer.Write( false );
			writer.Write( false );

			// zamknij uchwyty
			writer.Close();
			file.Close();
		}

		/// <summary>
		/// Klonowanie wzoru o podanej nazwie.
		/// Klonowanie polega na skopiowaniu wszystkich plików z jednego folderu do drugiego.
		/// Funkcja najpierw tworzy listę plików, a potem je kopiuje.
		/// </summary>
		/// 
		/// <param name="name">Nazwa wzoru do sklonowania.</param>
		/// <param name="to_clone">Nazwa nowego wzoru.</param>
		//* ============================================================================================================
		public static void ClonePattern( string name, string to_clone )
		{
			var source = new DirectoryInfo( "patterns/" + to_clone );
			var target = new DirectoryInfo( "patterns/" + name );

			PatternEditor.CopyFiles( source, target );
		}

		/// <summary>
		/// Kopiowanie plików z wybranego folderu do innego.
		/// Kopiuje wszystkie pliki z jednego folderu do drugiego.
		/// Funkcja działa rekursywnie, a więc pobiera foldery z folderu i wchodzi do nich, kopiując pliki.
		/// </summary>
		/// 
		/// <param name="source">Informacje o folderze z którego dane mają być kopiowane.</param>
		/// <param name="target">Informacje o folderze do którego dane będą kopiowane.</param>
		//* ============================================================================================================
		private static void CopyFiles( DirectoryInfo source, DirectoryInfo target )
		{
#		if DEBUG
			Program.LogMessage( "Tworzenie folderu: " + target.FullName );
#		endif
			// utwórz folder gdy nie istnieje
			Directory.CreateDirectory( target.FullName );

			// kopiuj pliki
			foreach( FileInfo file in source.GetFiles() )
			{
#			if DEBUG
				Program.LogMessage( "Kopiowanie pliku: " + file.Name );
#			endif
				file.CopyTo( Path.Combine(target.FullName, file.Name), true );
			}

			// pobierz podfoldery
			foreach( DirectoryInfo subdir in source.GetDirectories() )
			{
				DirectoryInfo nextsubdir = target.CreateSubdirectory( subdir.Name );
				PatternEditor.CopyFiles( subdir, nextsubdir );
			}
		}

		/// <summary>
		/// Wczytywanie wybranego wzoru.
		/// Funkcja wczytuje zapisane dane wzoru z pliku konfiguracyjnego.
		/// Pozwala na wczytanie samego nagłówka, dzięki czemu narzut na odczyt i pamięć jest mniejszy.
		/// Funkcja nie sprawdza dokładnie pliku, czy nie jest uszkodzony, tylko sam początek (ciąg CDCFG).
		/// </summary>
		/// 
		/// <param name="pattern">Wzór do odczytania.</param>
		/// <param name="header">Wczytywanie tylko nagłówka wzoru.</param>
		/// 
		/// <returns>Odczytane dane wzoru.</returns>
		//* ============================================================================================================
		public static PatternData ReadPattern( string pattern, bool header = false )
		{
			// sprawdź czy wzór posiada plik konfiguracyjny
			if( !File.Exists("patterns/" + pattern + "/config.cfg") )
				return new PatternData( pattern );

			var file   = new FileStream( "patterns/" + pattern + "/config.cfg", FileMode.OpenOrCreate );
			var reader = new BinaryReader( file );
			var data   = new PatternData( pattern );

			data.HasConfigFile = true;
			data.Corrupted     = true;

			// ciąg początkowy - rozpoznawczy CDCFG
			byte[] bytes = reader.ReadBytes( 5 );
			if( bytes[0] != 'C' || bytes[1] != 'D' || bytes[2] != 'C' || bytes[3] != 'F' || bytes[4] != 'G' )
				return data;

			// odczytaj opcje podstawowe wzoru
			data.Corrupted = false;
			data.Name      = pattern;
			data.Size      = new Size( reader.ReadInt16(), reader.ReadInt16() );
			data.Pages     = reader.ReadByte();
			data.Dynamic   = reader.ReadBoolean();

			// tylko nagłówek
			if( header )
			{
				reader.Close();
				file.Close();

				return data;
			}
			else
				data.Page = new PageData[data.Pages];

			PageData  page_data;
			FieldData field_data;

			// odczytaj dane strony
			for( int x = 0; x < data.Pages; ++x )
			{
				page_data = new PageData();
			
				// dane podstawowe
				page_data.Fields     = reader.ReadByte();
				page_data.Image      = reader.ReadBoolean();
				page_data.Color      = Color.FromArgb( reader.ReadInt32() );
				page_data.Field      = new FieldData[page_data.Fields];

				// odczytaj dane kontrolki
				for( int y = 0; y < page_data.Fields; ++y )
				{
					field_data = new FieldData();

					// dane podstawowe
					field_data.Name            = reader.ReadString();
					field_data.Bounds          = new RectangleF( reader.ReadSingle(), reader.ReadSingle(),
						reader.ReadSingle(), reader.ReadSingle() );
					field_data.BorderSize     = reader.ReadSingle();
					field_data.BorderColor    = Color.FromArgb( reader.ReadInt32() );
					field_data.Image           = reader.ReadBoolean();
					field_data.ImagePath      = null;
					field_data.Color           = Color.FromArgb( reader.ReadInt32() );
					field_data.FontColor      = Color.FromArgb( reader.ReadInt32() );
					field_data.FontName       = reader.ReadString();
					field_data.FontStyle      = (FontStyle)reader.ReadByte();
					field_data.FontSize       = reader.ReadSingle();
					field_data.TextAlign      = (ContentAlignment)reader.ReadInt32();
					field_data.TextTransform  = reader.ReadByte();
					field_data.TextAddMargin = reader.ReadBoolean();
					field_data.TextLeftpad    = reader.ReadSingle();
					field_data.TextToppad     = reader.ReadSingle();
					field_data.Padding         = reader.ReadSingle();

					// dane dodatkowe
					field_data.Extra = new FieldExtraData();
					field_data.Extra.ImageFromDB = reader.ReadBoolean();
					field_data.Extra.PrintColor   = reader.ReadBoolean();
					field_data.Extra.PrintImage   = reader.ReadBoolean();
					field_data.Extra.TextFromDB  = reader.ReadBoolean();
					field_data.Extra.PrintText    = reader.ReadBoolean();
					field_data.Extra.PrintBorder  = reader.ReadBoolean();
					field_data.Extra.PosAlign     = reader.ReadInt32();
					field_data.Extra.Column        = -1;
					
					page_data.Field[y] = field_data;
				}

				// informacje dodatkowe
				page_data.Extra = new PageExtraData();
				page_data.Extra.PrintColor = reader.ReadBoolean();
				page_data.Extra.PrintImage = reader.ReadBoolean();
				page_data.Extra.ImagePath  = null;

				data.Page[x] = page_data;
			}

			// zamknij strumienie
			reader.Close();
			file.Close();

			return data;
		}

		/// <summary>
		/// Rysowanie podglądu wzoru.
		/// Funkcja rysuje podgląd wzoru taki jaki został zapisany bez względu na to, co zostało ukryte.
		/// Wykorzystywany w edytorze wzorów do pełnego odwzorowania zapisanego wzoru.
		/// </summary>
		/// 
		/// <param name="data">Dane wzoru do narysowania.</param>
		/// <param name="panel">Panel zawierający strony wzoru.</param>
		/// <param name="scale">Skala w jakiej ma zostać narysowany podgląd.</param>
		//* ============================================================================================================
		public static void DrawPreview( PatternData data, Panel panel, double scale )
		{
			// wyczyść strony i pola
			panel.Controls.Clear();
			GC.Collect();

			// brak stron...
			if( data.Pages == 0 )
				return;

			double dpi_pxs = scale * PatternEditor._pixelPerDPI;
			Size   pp_size = new Size
			(
				Convert.ToInt32((double)data.Size.Width * dpi_pxs),
				Convert.ToInt32((double)data.Size.Height * dpi_pxs)
			);

			PageData  page_data;
			FieldData field_data;

			// dodawaj strony
			for( int x = 0; x < data.Pages; ++x )
			{
				AlignedPage page = new AlignedPage();
				page.Align = 1;

				page_data = data.Page[x];

				// tło strony
				if( page_data.Image && File.Exists("patterns/" + data.Name + "/images/page" + x + ".jpg" ) )
					using( Image image = Image.FromFile("patterns/" + data.Name + "/images/page" + x + ".jpg" ) )
					{
						Image new_image = new Bitmap( image.Width, image.Height, PixelFormat.Format32bppArgb );
						using( Graphics canvas = Graphics.FromImage(new_image) )
							canvas.DrawImageUnscaled( image, 0, 0 );
						

						page.BackgroundImage = new_image;
						image.Dispose();
					}
				else
					page.BackgroundImage = null;

				// kolor strony
				page.BackColor             = page_data.Color;
				page.BackgroundImageLayout = ImageLayout.Stretch;

				// skalowanie
				page.Location = new Point(0);
				page.Margin   = new Padding( 0, 0, 0, 0 );
				page.Size     = pp_size;

				if( x != 0 )
					page.Hide();

				// dodawaj pola
				for( int y = 0; y < page_data.Fields; ++y )
				{
					var field = new PageField();
					field_data = page_data.Field[y];

					// wygląd pola
					field.Text          = field_data.Name;
					field.DPIBounds     = field_data.Bounds;
					field.DPIBorderSize = field_data.BorderSize;
					field.BorderColor   = field_data.BorderColor;

					// granice rodzica
					field.setParentBounds( data.Size.Width, data.Size.Height );

					// tło pola
					if( field_data.Image && File.Exists("patterns/" + data.Name + "/images/field" + y + "_" + x + ".jpg") )
						using( Image image = Image.FromFile("patterns/" + data.Name + "/images/field" + y + "_" + x + ".jpg" ) )
						{
							Image new_image = new Bitmap( image.Width, image.Height, PixelFormat.Format32bppArgb );
							using( Graphics canvas = Graphics.FromImage(new_image) )
								canvas.DrawImageUnscaled( image, 0, 0 );

							field.BackImage = new_image;
							field.BackImagePath = "patterns/" + data.Name + "/images/field" + y + "_" + x + ".jpg";

							image.Dispose();
						}
					else
					{
						field.BackImage = null;
						field.BackImagePath = "";
					}

					// kolory
					field.BackColor = field_data.Color;
					field.ForeColor = field_data.FontColor;

					// czcionka
					FontFamily font_family;
					try   { font_family = new FontFamily( field_data.FontName ); }
					catch { font_family = new FontFamily( "Arial" ); }

					field.Font            = new Font( font_family, 8.25f, field_data.FontStyle, GraphicsUnit.World );
					field.DPIFontSize     = field_data.FontSize;
					field.TextAlign       = field_data.TextAlign;
					field.TextTransform   = field_data.TextTransform;
					field.DPIPadding      = field_data.Padding;
					field.TextMargin      = new PointF( field_data.TextLeftpad, field_data.TextToppad );
					field.ApplyTextMargin = field_data.TextAddMargin;
					
					// skalowanie
					field.DPIScale = scale;
					field.Margin   = new Padding(0);

					// dodatkowe informacje
					field.Tag = field_data.Extra;

					page.Controls.Add( field );
				}

				// dodatkowe informacje
				page.Tag = page_data.Extra;

				panel.Controls.Add( page );
			}
		}

		/// <summary>
		/// Rysowanie szkicu wzoru.
		/// Szkic wzoru to rysunek, gdzie wyświetlane są wszystkie elementy oprócz wartości dynamicznych, zmienianych
		/// przy przełączaniu się pomiędzy kolejnymi wierszami danych ze schowka.
		/// Pokazywane są tutaj wszystkie elementy, zaznaczone do generowania w edytorze wzorów, pozostałe są ukrywane.
		/// </summary>
		/// 
		/// <param name="data">Dane wzoru do narysowania.</param>
		/// <param name="panel">Panel zawierający strony wzoru.</param>
		/// <param name="scale">Skala wzoru do rysowania.</param>
		//* ============================================================================================================
		public static void DrawSketch( PatternData data, Panel panel, double scale )
		{
			// wyczyść strony i pola
			panel.Controls.Clear();
			GC.Collect();

			// brak stron...
			if( data.Pages == 0 )
				return;

			var dpi_pxs = scale * PatternEditor._pixelPerDPI;
			var pp_size = new Size
			(
				Convert.ToInt32((double)data.Size.Width * dpi_pxs),
				Convert.ToInt32((double)data.Size.Height * dpi_pxs)
			);

			PageData  page_data;
			FieldData field_data;

			// dodawaj strony
			for( int x = 0; x < data.Pages; ++x )
			{
				var page = new AlignedPage();
				page.Align = 1;

				page_data = data.Page[x];

				// tło strony
				if( page_data.Extra.PrintImage && page_data.Image &&
					File.Exists("patterns/" + data.Name + "/images/page" + x + ".jpg" ) )
					using( var image = Image.FromFile("patterns/" + data.Name + "/images/page" + x + ".jpg" ) )
					{
						var new_image = new Bitmap( image.Width, image.Height, PixelFormat.Format32bppArgb );
						using( var canvas = Graphics.FromImage(new_image) )
							canvas.DrawImageUnscaled( image, 0, 0 );

						page.BackgroundImage = new_image;
						image.Dispose();
					}
				else
					page.BackgroundImage = null;

				// kolor strony
				page.BackColor             = (page_data.Extra.PrintColor) ? page_data.Color : SystemColors.Window;
				page.BackgroundImageLayout = ImageLayout.Stretch;

				// skalowanie
				page.Location = new Point(0);
				page.Margin   = new Padding( 0, 0, 0, 0 );
				page.Size     = pp_size;

				if( x != 0 )
					page.Hide();

				// dodawaj pola
				for( int y = 0; y < page_data.Fields; ++y )
				{
					var field = new PageField();
					field_data = page_data.Field[y];

					// wygląd pola
					field.Text          = field_data.Extra.PrintText ? field_data.Name : "";
					field.DPIBounds     = field_data.Bounds;
					field.DPIBorderSize = field_data.Extra.PrintBorder ? field_data.BorderSize : 0;
					field.BorderColor   = field_data.BorderColor;

					// granice rodzica
					field.setParentBounds( data.Size.Width, data.Size.Height );

					// tło pola
					if( field_data.Extra.PrintImage && field_data.Image &&
						File.Exists("patterns/" + data.Name + "/images/field" + y + "_" + x + ".jpg") )
						using( var image = Image.FromFile("patterns/" + data.Name + "/images/field" + y + "_" + x + ".jpg") )
						{
							var new_image = new Bitmap( image.Width, image.Height, PixelFormat.Format32bppArgb );
							using( var canvas = Graphics.FromImage(new_image) )
								canvas.DrawImage( image, 0, 0 );

							field.BackImage = new_image;
							field.BackImagePath = "patterns/" + data.Name + "/images/field" + y + "_" + x + ".jpg";

							image.Dispose();
						}
					else
					{
						field.BackImage = null;
						field.BackImagePath = "";
					}

					// kolory
					field.BackColor = field_data.Extra.PrintColor ? field_data.Color : Color.Transparent;
					field.ForeColor = field_data.FontColor;

					// czcionka - trzeba mieć pewność że da się ją rady wczytać
					FontFamily font_family;
					try   { font_family = new FontFamily( field_data.FontName ); }
					catch { font_family = new FontFamily( "Arial" ); }

					field.Font            = new Font( font_family, 8.25f, field_data.FontStyle, GraphicsUnit.Point );
					field.DPIFontSize     = field_data.FontSize;
					field.TextAlign       = field_data.TextAlign;
					field.TextTransform   = field_data.TextTransform;
					field.DPIPadding      = field_data.Padding;
					field.TextMargin      = new PointF( field_data.TextLeftpad, field_data.TextToppad );
					field.ApplyTextMargin = field_data.TextAddMargin;
					
					// skalowanie
					field.DPIScale = scale;
					field.Margin   = new Padding( 0 );

					// dodatkowe informacje
					field.Tag = field_data.Extra;

					page.Controls.Add( field );
				}

				// dodatkowe informacje
				page.Tag = page_data.Extra;

				if( x != 0 )
					page.Hide();
				else
					page.Show();

				panel.Controls.Add( page );
			}
		}

		/// <summary>
		/// Rysowanie wiersza wzoru.
		/// Funkcja uzupełnia dynamiczne pola tekstowe danymi, przekazanymi ze schowka.
		/// Każde pole dynamiczne, do którego przypisana jest kolumna uzupełniane jest z podanego wiersza.
		/// </summary>
		/// 
		/// <param name="panel">Panel z danymi wzoru.</param>
		/// <param name="storage">Schowek z danymi do odczytu.</param>
		/// <param name="row">Indeks wiersza do podmiany.</param>
		//* ============================================================================================================
		public static void DrawRow( Panel panel, DataStorage storage, int row )
		{
			AlignedPage page;
			PageField   field;

			// zmień wartości pól
			for( int x = 0; x < panel.Controls.Count; ++x )
			{
				page = (AlignedPage)panel.Controls[x];

				for( int y = 0; y < page.Controls.Count; ++y )
				{
					field = (PageField)page.Controls[y];
					int replace = ((FieldExtraData)field.Tag).Column;

					// sprawdź czy pola można zmienić i czy wartości są im przypisane
					if( ((FieldExtraData)field.Tag).TextFromDB && replace != -1 )
						field.Text = storage.Row[row][replace];
					else if( ((FieldExtraData)field.Tag).ImageFromDB && replace != -1 )
						{}
				}
			}
		}

		/// <summary>
		/// Zapis wzoru w postaci pliku podglądowego o formacie JPG.
		/// Wzór musi być również zapisywany do pliku obrazkowego, aby umożliwić jego szybki podgląd.
		/// Funkcja tworzy podgląd każdej strony wzoru do osobnego pliku o formacie JPG.
		/// Teoretycznie, podgląd powinien przedstawiać to co edytor, jednak może się różnić precyzją rozmieszczenia
		/// poszczególnych elementów.
		/// </summary>
		/// 
		/// <param name="data">Dane wzoru do wygenerowania.</param>
		/// <param name="panel">Panel z danymi wzoru.</param>
		/// <param name="scale">Skala wzoru do wygenerowania.</param>
		//* ============================================================================================================
		public static void GeneratePreview( PatternData data, Panel panel, double scale )
		{
			var dpi_pxs = PatternEditor._pixelPerDPI * scale;
			int width   = Convert.ToInt32((double)data.Size.Width * dpi_pxs);
			int height  = Convert.ToInt32((double)data.Size.Height * dpi_pxs);

			// rysuj strony
			for( int x = 0; x < data.Pages; ++x )
			{
				var bmp  = new Bitmap( width, height );
				var gfx  = Graphics.FromImage( bmp );
				var page = (AlignedPage)panel.Controls[x];

				// obraz lub wypełnienie
				if( page.BackgroundImage != null )
					gfx.DrawImage( page.BackgroundImage, 0, 0, width, height );
				else
				{
					var brush = new SolidBrush( page.BackColor );
					gfx.FillRectangle( brush, 0, 0, width, height );
				}

				// rysuj pola
				for( int y = 0; y < page.Controls.Count; ++y )
				{
					var field  = (PageField)page.Controls[y];
					var bounds = field.DPIBounds;
					
					bounds.X      = (float)((double)bounds.X * dpi_pxs);
					bounds.Y      = (float)((double)bounds.Y * dpi_pxs);
					bounds.Width  = (float)((double)bounds.Width * dpi_pxs);
					bounds.Height = (float)((double)bounds.Height * dpi_pxs);

					// koloruj lub rysuj obraz
					if( field.BackImage != null )
						gfx.DrawImage( field.BackImage, bounds );
					else
					{
						var brush = new SolidBrush( field.BackColor );
						gfx.FillRectangle( brush, bounds );
					}

					// utwórz czcionkę
					var font = new Font
					(
						field.Font.FontFamily,
						(float)(field.DPIFontSize * scale),
						field.Font.Style,
						GraphicsUnit.Point,
						field.Font.GdiCharSet,
						field.Font.GdiVerticalFont
					);

					var lbrush = new SolidBrush( field.ForeColor );
					var format = new StringFormat();

					// margines wewnętrzny
					int pad = Convert.ToInt32((double)field.DPIPadding * dpi_pxs);
					var box = new RectangleF( bounds.X + pad, bounds.Y + pad, bounds.Width - pad * 2,
						bounds.Height - pad * 2 );

					// powięsz prostokąt dla czcionki
					box.Height++;
					box.Width++;

					// rozmieszczenie tekstu
					if( ((int)field.TextAlign & 0x111) != 0 )
						format.Alignment = StringAlignment.Near;
					else if( ((int)field.TextAlign & 0x222) != 0 )
						format.Alignment = StringAlignment.Center;
					else
						format.Alignment = StringAlignment.Far;

					if( ((int)field.TextAlign & 0x7) != 0 )
						format.LineAlignment = StringAlignment.Near;
					else if( ((int)field.TextAlign & 0x70) != 0 )
						format.LineAlignment = StringAlignment.Center;
					else
						format.LineAlignment = StringAlignment.Far;

					gfx.DrawString( field.Text, font, lbrush, box, format );
					//TextRenderer.DrawText( gfx, fiedl.Text, font, box, Color.Aqua, Color.Transparent, 

					int border_size = Convert.ToInt32((double)field.DPIBorderSize * dpi_pxs);
					Pen border_pen  = new Pen( field.BorderColor );

					if( field.DPIBorderSize > 0.0 && border_size == 0 )
						border_size = 1;

					// rysuj ramkę (x i y liczony jest od lewego górnego rogu, dlatego odejmuje się 1)
					bounds.Width--;
					bounds.Height--;

					for( int z = 0; z < border_size; ++z )
						gfx.DrawRectangle( border_pen, bounds.X + z, bounds.Y + z, bounds.Width - z * 2,
							bounds.Height - z * 2 );
				}

				// zapisz do pliku
				bmp.Save( "patterns/" + data.Name + "/preview" + x + ".jpg" );
			}
		}

		/// <summary>
		/// Generowanie strony do formatu PDF.
		/// Funkcja odwzorowuje układ kontrolek na panelu, korzystając z informacji zawartych w zapisanych danych.
		/// Całość zapisywana jest do utworzonej poprzednio strony w klasie z biblioteki <em>PdfSharp</em>.
		/// Funkcja została wyodrębniona z innej funkcji na rzecz przejrzystości w kodzie.
		/// </summary>
		/// 
		/// <seealso cref="GeneratePDF"/>
		/// 
		/// <param name="page">Strona w klasie biblioteki PdfSharp.</param>
		/// <param name="page_data">Dane dotyczące aktualnie generowanej strony.</param>
		/// <param name="scale">Obliczona skala rysunku.</param>
		/// <param name="cols">Kolumny do podstawiania lub NULL.</param>
		//* ============================================================================================================
		private static void GeneratePDFPage( PdfPage page, PageData page_data, double scale, string[] cols )
		{
			var gfx      = XGraphics.FromPdfPage( page );
			var foptions = new XPdfFontOptions( PdfFontEncoding.Unicode, PdfFontEmbedding.Always );

			// rysuj kolor strony
			if( page_data.Extra.PrintColor )
				gfx.DrawRectangle( new XSolidBrush((XColor)page_data.Color), 0, 0, page.Width, page.Height );

			// pola na stronie
			for( int z = 0; z < page_data.Fields; ++z )
			{
				// obszar cięcia
				var field_data = page_data.Field[z];
				var bounds     = new XRect
				(
					XUnit.FromMillimeter( (double)field_data.Bounds.X ),
					XUnit.FromMillimeter( (double)field_data.Bounds.Y ),
					XUnit.FromMillimeter( (double)field_data.Bounds.Width ),
					XUnit.FromMillimeter( (double)field_data.Bounds.Height )
				);

				// kolor pola 
				if( field_data.Extra.PrintColor )
				{
					var brush = new XSolidBrush( (XColor)field_data.Color );

					if( field_data.Extra.PrintBorder )
					{
						var bfull   = XUnit.FromMillimeter( (double)field_data.BorderSize );
						var bhalf   = bfull * 0.5;
						var cbounds = new XRect( bounds.X + bhalf, bounds.Y + bhalf, bounds.Width -
							bfull, bounds.Height - bfull );
						gfx.DrawRectangle( brush, cbounds );
					}
					else
						gfx.DrawRectangle( brush, bounds );
				}

				// ramka pola
				if( field_data.Extra.PrintBorder && field_data.BorderSize > 0.0 )
				{
					var pen = new XPen( (XColor)field_data.BorderColor );
					pen.Width = XUnit.FromMillimeter( (double)field_data.BorderSize );
					var prx = pen.Width / 2.0 - 0.01;

					gfx.DrawLine( pen, bounds.X, bounds.Y + prx, bounds.X + bounds.Width, bounds.Y + prx );
					gfx.DrawLine( pen, bounds.X + prx, bounds.Y, bounds.X + prx, bounds.Y + bounds.Height );
					gfx.DrawLine( pen, bounds.X, bounds.Y + bounds.Height - prx, bounds.X + bounds.Width,
						bounds.Y + bounds.Height - prx );
					gfx.DrawLine( pen, bounds.X + bounds.Width - prx, bounds.Y, bounds.X + bounds.Width - prx,
						bounds.Y + bounds.Height );
				}

				// rysuj tekst
				if( field_data.Extra.PrintText || (field_data.Extra.TextFromDB && field_data.Extra.Column > -1) )
				{
					var fsize  = (float)(field_data.FontSize * scale);
					var font   = new XFont( field_data.FontName, fsize, (XFontStyle)field_data.FontStyle, foptions );
					var lbrush = new XSolidBrush( (XColor)field_data.FontColor );

					// pobierz napis
					var tf = new XTextFormatter( gfx );
					var text = field_data.Extra.PrintText || cols == null
						? field_data.Name
						: cols[field_data.Extra.Column];

					// margines wewnętrzny
					if( field_data.Padding > 0.0 )
					{
						double padding = XUnit.FromMillimeter( (double)field_data.Padding ),
								pad2x   = padding * 2.0;

						if( bounds.Width > pad2x && bounds.Height > pad2x )
						{
							bounds.X += padding;
							bounds.Y += padding;
							bounds.Width -= padding * 2.0;
							bounds.Height -= padding * 2.0;
						}
					}

					bounds.Y      -= scale;
					bounds.Height += scale * 2.0;

					// sprawdź czy tekst nie jest pusty
					string test = text.Trim();
					if( test != "" && test != null )
					{
						var txtm = gfx.MeasureString( text, font );
								
						// rozmieszczenie tekstu
						if( ((int)field_data.TextAlign & 0x111) != 0 )
							tf.Alignment = XParagraphAlignment.Left;
						else if( ((int)field_data.TextAlign & 0x222) != 0 )
							tf.Alignment = XParagraphAlignment.Center;
						else
							tf.Alignment = XParagraphAlignment.Right;

						// oblicz realną wysokość tekstu
						int passes = 1;
						for( double twidth = txtm.Width; twidth > bounds.Width; ++passes )
							twidth -= bounds.Width;

						// przyleganie tekstu w pionie
						if( txtm.Height * (double)passes < bounds.Height )
							// wycentrowanie linii
							if( ((int)field_data.TextAlign & 0x70) != 0 )
							{
								double theight = (bounds.Height - txtm.Height * (double)passes) * 0.5;
								bounds.Y += theight;
							}
							// przyleganie linii do dołu
							else if( ((int)field_data.TextAlign & 0x700) != 0 )
							{
								double theight = (bounds.Height - txtm.Height * (double)passes);
								bounds.Y += theight;
							}

						tf.DrawString( text, font, lbrush, bounds );
					}
				}
			}
		}
		
		/// <summary>
		/// Generowanie dokumentu PDF z podanych danych.
		/// Funkcja generuje dokument PDF dla wzoru z którego wczytane zostały dane.
		/// Dołożono wszelkich starań, aby odwzorowanie milimetrów było jak najbardziej realne, jednak problem, który
		/// może występować jest problemem czcionek - nie są one dopasowane tak, aby ich szerokość odpowiadała realnej
		/// szerokości, wzlgędem której są wyświetlane (czcionki mają kilka wysokości).
		/// Możliwe jest wygenerowanie wzoru w dwóch trybach - pierwszy to generowanie stron w taki sposób, w jaki
		/// są przedstawione (wiersz z wszystkimi stronami, kolejny wiersz z wszystkimi stronami itd), drugi to generowanie
		/// pojedynczych stron w wierszy (wszystkie wiersze ze stroną, wszystkie wiersze z kolejną stroną, itd).
		/// Technika ta zwie się łączeniem stron w wierszu.
		/// </summary>
		/// 
		/// <seealso cref="GeneratePDFPage"/>
		/// 
		/// <param name="storage">Schowek z wczytanymi danymi do zapisu.</param>
		/// <param name="pdata">Dane wzoru do wygenerowania.</param>
		/// <param name="output">Nazwa pliku wyjściowego.</param>
		/// <param name="copies">Ilość kopii w przypadku wzoru statycznego.</param>
		/// <param name="collate">Łączenie stron w wierszu.</param>
		//* ============================================================================================================
		public static void GeneratePDF( DataStorage storage, PatternData pdata, string output, int copies, bool collate )
		{
			var scale = 0.0;
			var pdf   = new PdfDocument();
			int limit = storage == null
				? copies
				: storage.RowsNumber;

			// łączenie stron w wierszu
			if( collate ) for( int x = 0; x < limit; ++x )
			{
				for( int y = 0; y < pdata.Pages; ++y )
				{
					var page = pdf.AddPage();

					// rozmiary strony
					page.Width  = XUnit.FromMillimeter( pdata.Size.Width );
					page.Height = XUnit.FromMillimeter( pdata.Size.Height );

					// oblicz skale powiększenia
					scale = page.Width.Presentation / ((double)pdata.Size.Width * PatternEditor._pixelPerDPI);

					// generuj stronę
					PatternEditor.GeneratePDFPage( page, pdata.Page[y], scale, storage == null ? null : storage.Row[x] );
				}
			}
			// strony pojedynczo w wierszach
			else for( int x = 0; x < pdata.Pages; ++x )
			{
				for( int y = 0; y < limit; ++y )
				{
					var page = pdf.AddPage();

					// rozmiary strony
					page.Width  = XUnit.FromMillimeter( pdata.Size.Width );
					page.Height = XUnit.FromMillimeter( pdata.Size.Height );

					// oblicz skale powiększenia
					scale = page.Width.Presentation / ((double)pdata.Size.Width * PatternEditor._pixelPerDPI);

					// generuj stronę
					PatternEditor.GeneratePDFPage( page, pdata.Page[x], scale, storage == null ? null : storage.Row[y] );
				}
			}

			// zapisz plik pdf
			pdf.Save( output );
		}
		
		/// <summary>
		/// Zapis edytowanego wzoru.
		/// Funkcja zapisuje zmiany w utworzonym wcześniej wzorze.
		/// Podczas zapisu tworzony jest nowy plik konfiguracyjny zawierający podane dane.
		/// Wszystkie obrazki wgrywane do wzoru, zapisywane są w podfolderze images głównego folderu wzoru.
		/// Opis schematu pliku konfiguracyjnego znajduje się w funkcji <see cref="Create"/>.
		/// </summary>
		/// 
		/// <param name="data">Dane wzoru do zapisu.</param>
		/// <param name="panel">Panel zawierający strony i kontrolki wzoru.</param>
		//* ============================================================================================================
		public static void Save( PatternData data, Panel panel )
		{
			var file   = new FileStream( "patterns/" + data.Name + "/_config.cfg", FileMode.OpenOrCreate );
			var writer = new BinaryWriter( file );

			byte[] text = new byte[5] { (byte)'C', (byte)'D', (byte)'C', (byte)'F', (byte)'G' };
			writer.Write( text, 0, 5 );

			writer.Write( (short)data.Size.Width );
			writer.Write( (short)data.Size.Height );
			writer.Write( (byte)panel.Controls.Count );

			bool   dynamic = false;
			string pattern = data.Name;
			
			foreach( Panel page in panel.Controls )
				foreach( PageField field in page.Controls )
				{
					var field_extra = (FieldExtraData)field.Tag;
					if( field_extra.TextFromDB || field_extra.ImageFromDB )
					{
						dynamic = true;
						break;
					}
				}
			writer.Write( dynamic );
			
			// zapisz konfiguracje stron
			for( int x = 0; x < panel.Controls.Count; ++x )
			{
				var page = (AlignedPage)panel.Controls[x];

				// obraz strony
				writer.Write( (byte)page.Controls.Count );
				if( page.BackgroundImage != null )
				{
					page.BackgroundImage.Save( "patterns/" + pattern + "/images/page" + x + ".jpg" );
					writer.Write( true );
				}
				else
					writer.Write( false );

				writer.Write( page.BackColor.ToArgb() );

				// zapisz konfiguracje pól na stronie
				for( int y = 0; y < page.Controls.Count; ++y )
				{
					var field = (PageField)page.Controls[y];

					writer.Write( field.OriginalText );
					writer.Write( field.DPIBounds.X );
					writer.Write( field.DPIBounds.Y );
					writer.Write( field.DPIBounds.Width );
					writer.Write( field.DPIBounds.Height );

					writer.Write( field.DPIBorderSize );
					writer.Write( field.BorderColor.ToArgb() );

					// obraz podglądu lub statyczny
					if( field.BackImage != null )
					{
						field.BackImage.Save( "patterns/" + pattern + "/images/field" + y + "_" + x + ".jpg" );
						writer.Write( true );
					}
					else
						writer.Write( false );

					writer.Write( field.BackColor.ToArgb() );
					writer.Write( field.ForeColor.ToArgb() );

					// czcionka i tekst
					writer.Write( field.Font.Name );
					writer.Write( (byte)field.Font.Style );
					writer.Write( (float)field.DPIFontSize );
					writer.Write( (int)field.TextAlign );
					writer.Write( (byte)field.TextTransform );
					writer.Write( field.ApplyTextMargin );
					writer.Write( field.TextMargin.X );
					writer.Write( field.TextMargin.Y );
					writer.Write( field.DPIPadding );

					var field_extra = (FieldExtraData)field.Tag;

					// informacje dodatkowe pola
					writer.Write( field_extra.ImageFromDB );
					writer.Write( field_extra.PrintColor );
					writer.Write( field_extra.PrintImage );
					writer.Write( field_extra.TextFromDB );
					writer.Write( field_extra.PrintText );
					writer.Write( field_extra.PrintBorder );
					writer.Write( (int)field_extra.PosAlign );
				}

				var page_extra = (PageExtraData)page.Tag;

				// informacje dodatkowe strony
				writer.Write( page_extra.PrintColor );
				writer.Write( page_extra.PrintImage );
			}

			writer.Close();
			file.Close();

			// usuń stary plik konfiguracyjny
			if( File.Exists("patterns/" + data.Name + "/config.cfg") )
				File.Delete( "patterns/" + data.Name + "/config.cfg" );

			// przenieś (zmień nazwę) nowego pliku konfiguracyjnego
			File.Move( "patterns/" + data.Name + "/_config.cfg", "patterns/" + data.Name + "/config.cfg" );
		}

		/// <summary>
		/// Zmiana skali otwartego wzoru.
		/// Funkcja zmienia skalę aktualnie otwartego wzoru, podmieniając wartości we wszystkich kontrolkach.
		/// </summary>
		/// 
		/// <param name="data">Dane wzoru do zmiany skali.</param>
		/// <param name="panel">Panel z danymi wzoru do zmiany skali.</param>
		/// <param name="scale">Skala do zamianay.</param>
		//* ============================================================================================================
		public static void ChangeScale( PatternData data, Panel panel, double scale )
		{
			// oblicz wymiary strony
			var dpi_pxs = scale * PatternEditor._pixelPerDPI;
			var pp_size = new Size
			(
				Convert.ToInt32((double)data.Size.Width * dpi_pxs),
				Convert.ToInt32((double)data.Size.Height * dpi_pxs)
			);

			AlignedPage page;
			PageField   field;

			// zmieniaj skale stron i pól
			for( int x = 0; x < panel.Controls.Count; ++x )
			{
				page = (AlignedPage)panel.Controls[x];
				page.Size = pp_size;

				for( int y = 0; y < page.Controls.Count; ++y )
				{
					field = (PageField)page.Controls[y];
					field.DPIScale = scale;
				}
			}
		}

		/// <summary>
		/// Zwraca podane wymiary wyrażone w pikselach.
		/// Funkcja pobiera wymiary w milimetrach i zwraca w pikselach, przemnożone przez odpowiednią skalę.
		/// </summary>
		/// 
		/// <param name="size">Wymiary do pomnożenia w milimetrach.</param>
		/// <param name="scale">Skala dla mnożnika.</param>
		/// 
		/// <returns>Pomnożone wymiary w pikselach.</returns>
		//* ============================================================================================================
		public static Size GetDPIPageSize( Size size, double scale )
		{
			double dpi_pxs = scale * PatternEditor._pixelPerDPI;
			return new Size( Convert.ToInt32(size.Width * dpi_pxs), Convert.ToInt32(size.Height * dpi_pxs) );
		}

		/// <summary>
		/// Zwraca podaną wartość wyrażoną w pikselach.
		/// Funkcja pobiera milimetry i zwraca piksele, przemnożone przez odpowiednią skalę.
		/// </summary>
		/// 
		/// <param name="width">Wartość do pomnożenia w milimetrach.</param>
		/// <param name="scale">Skala dla mnożnika.</param>
		/// 
		/// <returns>Pomnożona wartość w pikselach.</returns>
		//* ============================================================================================================
		public static float GetDimensionScale( float width, double scale )
		{
			return (int)((double)width * (PatternEditor._pixelPerDPI * scale));
		}
	}
}
