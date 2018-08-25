///
/// $u01 Program.cs
/// 
/// Plik zawierający klasę zarządzania wzorami aplikacji.
/// Pozwala na utworzenie, klonowanie i usuwanie poszczególnych wzorów.
/// Dodatkowo umożliwia wyświetlanie wzorów w podanym panelu (generowanie podglądu) w dwóch wersjach,
/// wersja szkicu - dla generatora (dynamiczne dane) - i wersja pełna - dla edytora.
/// Umożliwia również zapis wzoru w trzech wariantach - zapis do pliku PDF, zapis wzoru do pliku
/// konfiguracyjnego oraz zapis do pliku JPEG jako podgląd wzoru, wyświetlany w głównym formularzu aplikacji.
/// 
/// Autor: Kamil Biały
/// Od wersji: 0.1.x.x
///

#define LOGMESSAGE

using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.IO;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Threading;
using System.Reflection;

using CDesigner.Forms;

namespace CDesigner.Utils
{
	/// 
	/// <summary>
	/// Klasa główna aplikacji zawierająca najczęściej używane funkcje.
	/// Zawiera funkcje do logowania błędów wraz z najczęściej używanymi funkcjami w całym programie.
	/// Wszystkie funkcje w tej klasie są funkcjami statycznymi z racji tego, że nie jest tworzony obiekt klasy.
	/// Klasa uruchamia bezpośrednio pod koniec działania funkcji Main główny formularz aplikacji.
	/// W konstruktorze statycznym wczytuje informacje o dacie kompilacji i wersji programu.
	/// </summary>
	/// 
	static class Program
	{
#region ZMIENNE

		/// <summary>Strumień pliku z przetwarzanymi informacjami.</summary>
		/// @hideinitializer
		private static StreamWriter _writer = null;
		
		/// <summary>Główne okno aplikacji.</summary>
		/// @hideinitializer
		private static Form _main = null;

		/// <summary>Ikona aplikacji...</summary>
		/// @hideinitializer
		private static Icon _icon = null;
		
		/// <summary>Aktualne wcięcie (ilość spacji)...</summary>
		/// @hideinitializer
		private static string _indent = "";
		
		/// <summary>Lista obrazków używanych w programie.</summary>
		private static Bitmap[] _bitmaps;

		/// <summary>Lista bitmap, które są możliwe do wczytania.</summary>
		/// @hideinitializer
		private static string[] _bitmapList =
		{
			"images/cdesigner-16.png",   "images/cdesigner-32.png",  "images/cdesigner-48.png",
			"images/cdesigner-64.png",   "images/cdesigner-96.png",  "images/cdesigner-128.png",
			"images/cdesigner-256.png",  "images/cdesigner-512.png", "images/cdrestore-16.png",
			"images/cdrestore-32.png",   "images/cdrestore-48.png",  "images/cdrestore-64.png",
			"images/cdrestore-96.png",   "images/cdrestore-128.png", "images/cdrestore-256.png",
			"images/cdrestore-512.png",  "images/noimage.png",       "icons/item-add.png",
			"icons/item-delete.png",     "icons/first-page.png",     "icons/prev-page.png",
			"icons/next-page.png",       "icons/last-page.png"
		};

		/// <summary>Klasy globalne, ogólnodostępne dla całego programu.</summary>
		public static GlobalStruct GLOBAL;

		/// <summary>Wersja aplikacji.</summary>
		public static readonly string VERSION;

		/// <summary>Data kompilacji.</summary>
		public static readonly DateTime BUILD_DATE;

		/// <summary>Nazwa kodowa aktualnej wersji programu.</summary>
		public static readonly string CODE_NAME = "Polewik";

#endregion

#region KONSTRUKTOR STATYCZNY / PODSTAWOWE FUNKCJE

		/// <summary>
		/// Konstruktor statyczny klasy.
		/// Pobiera informacje o dacie kompilacji i wersji aplikacji.
		/// </summary>
		/// 
		/// <seealso cref="Utils.GlobalStruct"/>
		//* ============================================================================================================
		static Program()
		{
			Version version = Assembly.GetExecutingAssembly().GetName().Version;

			Program.VERSION    = version.ToString();
			Program.BUILD_DATE = new DateTime( 2000, 1, 1 ).Add
			(
				new TimeSpan
				(
					TimeSpan.TicksPerDay    * version.Build +
					TimeSpan.TicksPerSecond * 2 * version.Revision
				)
			);

			Program._bitmaps = new Bitmap[Program._bitmapList.Count()];

			// utwórz instancję do struktury GlobalStruct
			Program.GLOBAL = new GlobalStruct();
		}
		
		/// <summary>
		/// Przygotowuje program do zamknięcia.
		/// Funkcja zamyka aplikację i plik raportowania danych gdy włączony tryb debugowania.
		/// Uruchamia funkcje zamykania tylko wtedy, gdy zostało to wymuszone.
		/// </summary>
		/// 
		/// <seealso cref="Main"/>
		/// 
		/// <param name="force">Wymuszone zakończenie aplikacji.</param>
		//* ============================================================================================================
		public static void ExitApplication( bool force )
		{
#		if LOGMESSAGE
			// zamknij dziennik zdarzeń
			if( Program._writer != null )
			{
				Program._writer.Flush();
				Program._writer.Close();
			}
#		endif
			Program._writer = null;

			if( force )
				Application.Exit();
		}

		/// <summary>
		/// Funkcja startowa aplikacji.
		/// Funkcja wczytuje potrzebne dane, uruchamia loggera i automatyczne ładowanie brakujących zależności.
		/// Po wczytaniu ustawień programu i zapisanego języka, funkcja uruchamia okno główne aplikacji.
		/// Argument -w czeka na zamknięcie aplikacji CDRestore.exe, gdy program uruchamiany jest po aktualizacji.
		/// </summary>
		/// 
		/// <seealso cref="ExitApplication"/>
		/// 
		/// <param name="args">Argumenty przekazywane do aplikacji.</param>
		//* ============================================================================================================
		[STAThread] public static void Main( string[] args )
		{
			
			bool warg = false;

			// szukaj parametru -w
			foreach( string arg in args )
				if( arg == "-w" )
					warg = true;

			try
			{
				// twórz foldery gdy nie istnieją
				if( !Directory.Exists("./update") )
					Directory.CreateDirectory( "./update" );
				if( !Directory.Exists("./backup") )
					Directory.CreateDirectory( "./backup" );
				if( !Directory.Exists("./patterns") )
					Directory.CreateDirectory( "./patterns" );

#			if LOGMESSAGE
				// otwórz plik do zapisu
				Program._writer = new StreamWriter( File.Open("./cdesigner.log", FileMode.Create, FileAccess.Write) );
#			endif

#			if DEBUG
				Program.LogMessage( "# =======================================================================" );
				Program.LogMessage( "# Program: CDesigner " );
				Program.LogMessage( "# Wersja : " + Program.VERSION );
				Program.LogMessage( "# Data   : " + Program.BUILD_DATE );
				Program.LogMessage( "# Autor  : Kamil Biały " );
				Program.LogMessage( "# =======================================================================" );
				Program.LogMessage( "" );
#			endif

				// czekaj na odblokowanie pliku lub rzuć wyjątkiem
				Program.CDRestoreLockCheck( warg );
			}
			// ups, wystąpił błąd...
			catch( Exception ex )
			{
				try
				{
					// plik jest zablokowany przez inny proces
					if( ex.Message == "__FILEISLOCKED__" || Program.IsFileLocked(ex) )
					{
						// wczytaj ustawienia
						Settings.Initialize();
						Settings.Parse();

						// wczytaj pliki językowe
						Language.Initialize();
						Language.Parse();

						// wyświetl błąd
						Program.LogError
						(
							ex.Message == "__FILEISLOCKED__"
								? Language.GetLine("GlobalErrors", (int)LANGCODE.GGE_RUNNINGCDRESTORE)
								: Language.GetLine("GlobalErrors", (int)LANGCODE.GGE_RUNNINGCDESIGNER),
							Language.GetLine("GlobalErrors", (int)LANGCODE.GGE_CRITICALERROR),
							true
						);
						return;
					}
					// ups... inny błąd?
					Program.LogError( ex.Message, "Fatal Error", true, ex );
					return;
				}
				catch( Exception ex_inner )
				{
					// błąd w błędzie... to ci dopiero heca!
					Program.LogError( ex_inner.Message, "Fatal Error", true, ex_inner );
					return;
				}
			}
			
#		if DEBUG
			Program.LogMessage( "Uruchamianie programu CDesigner." );
#		endif

			// można uruchamiać aplikację...
			try
			{
				// style wizualne formularza - wyłączyć dla linuksa
				// wykrywać czy jest możliwość ich włączenia?
				try
				{
					Application.EnableVisualStyles();
					Application.SetCompatibleTextRenderingDefault( false );
				}
				catch( Exception ex )
				{
					Program.LogMessage( "Stylizacja przycisków nie mogła zostać włączona..." );
					Program.LogMessage( ex.Message );
				}

				// rejestruj zdarzenia do rozwiązywania problemów z plikami dll
				AssemblyLoader.Register();
				
				// wczytaj ustawienia
				Settings.Initialize();
				Settings.Parse();

				// wczytaj język
				Language.Initialize();
				Language.Parse();

				// uruchom główne okno programu
				Program._main = new MainForm();
				Application.Run( Program._main );
			}
			// przechwyć jakikolwiek błąd...
			catch( Exception ex )
			{
				Program.LogError( ex.Message, "Fatal error", true, ex );
				return;
			}
			// zamykaj niezależnie od tego czy wystąpił wyjątek czy też nie
			finally
				{ Program.ExitApplication( false ); }
		}

#endregion

#region OPERACJE NA PLIKACH

		/// <summary>
		/// Pobiera obrazek pod numerem przekazanym w argumencie.
		/// Wszystkie możliwe obrazki dostępne są pod typem numerycznym BITMAPCODE.
		/// Każdy indeks typu numerycznego zawiera odpowiednik ścieżki, z której wczytywane są obrazki.
		/// W przypadku gdy obrazek nie istnieje, funkcja wywołuje funkcję wczytującą obrazki.
		/// </summary>
		/// 
		/// <seealso cref="LoadBitmap"/>
		/// <seealso cref="GetIcon"/>
		/// 
		/// <param name="index">Indeks obrazka do załadowania dostępny z numeracji BITMAPCODE.</param>
		/// 
		/// <returns>Obrazek w postaci bitmapy o typie Bitmap.</returns>
		//* ============================================================================================================
		public static Bitmap GetBitmap( BITMAPCODE index )
		{
			if( Program._bitmaps[(int)index] == null )
				Program._bitmaps[(int)index] = Program.LoadBitmap( index );

			return Program._bitmaps[(int)index];
		}
		
		///
		/// <summary>
		/// Ładowanie bitmapy do pamięci programu.
		/// Funkcja pozwala na załadowanie bitmapy ze ścieżki zdefiniowanej pod podanym kodem typu numerycznego.
		/// W odróżnieniu od funkcji <see cref="GetBitmap"/>, funkcja ta ładuje bitmapę, a nie tylko zwraca.
		/// </summary>
		/// 
		/// <seealso cref="GetBitmap"/>
		/// <seealso cref="GetIcon"/>
		/// 
		/// <param name="index">Indeks obrazka do załadowania dostępny z numeracji BITMAPCODE.</param>
		/// 
		/// <returns>Obrazek w postaci bitmapy o typie Bitmap.</returns>
		//* ============================================================================================================
		private static Bitmap LoadBitmap( BITMAPCODE index )
		{
			Bitmap bitmap = null;

			// gdy wystąpi błąd, wyświetl go, ale nie traktuj go jako krytycznego...
			try
				{ bitmap = new Bitmap( "./" + Program._bitmapList[(int)index] ); }
			catch( Exception ex )
			{
				Program.LogMessage( ">> Nie można załadować bitmapy: './" +
					Program._bitmapList[(int)index] + "' - " + ex.Message );
				bitmap = null;
			}

			// zwróć załadowaną bitmapę
			return bitmap;
		}

		/// <summary>
		/// Pobiera ikonę aplikacji.
		/// W razie potrzeby automatycznie ładuje ikonę z pliku.
		/// </summary>
		/// 
		/// <seealso cref="GetBitmap"/>
		/// <seealso cref="LoadBitmap"/>
		/// 
		/// <returns>Ikona aplikacji.</returns>
		//* ============================================================================================================
		public static Icon GetIcon()
		{
			// załaduj ikonę programu, ale nie zgłaszaj wyjątku jako błędu krytycznego
			if( Program._icon == null )
			{
				try
					{ Program._icon = new Icon( "./icons/cdesigner.ico" ); }
				catch( IOException ex )
					// nie trzeba debug, niech zapisze jeżeli może
					{ Program.LogMessage( "Nie można wczytać ikony programu - " + ex.Message ); }
			}
			return Program._icon;
		}

		/// <summary>
		/// Sprawdza czy plik nie jest blokowany przez inny proces.
		/// Funkcja sprawdza w odpowiedni sposób, czy wyjątek, który wystąpił podczas otwierania pliku, nie jest
		/// spowodowany tym, że plik jest zablokowany przez inny proces.
		/// </summary>
		/// 
		/// <seealso cref="CDRestoreLockCheck"/>
		/// 
		/// <param name="ex">Wyjątek zwracany przy próbie dostępu do pliku.</param>
		/// 
		/// <returns>True, gdy zablokowany, false gdy błąd dotyczy czegoś innego.</returns>
		//* ============================================================================================================
		public static bool IsFileLocked( Exception ex )
		{
			int error = Marshal.GetHRForException( ex ) & ((1 << 16) - 1);
					
			// ERROR_SHARING_VIOLATION[32] || ERROR_LOCK_VIOLATION[33]
			if( error == 32 || error == 33 )
				return true;

			return false;
		}

		/// <summary>
		/// Pobieranie plików z folderu.
		/// Funkcja pobiera listę plików, znajdujących się w wybranym foldrze.
		/// W przypadku gdy podany jest drugi argument, foldery pobierane są rekursywnie (gdy w folderze istnieje
		/// folder, funkcja wchodzi do niego i pobiera jego pliki).
		/// </summary>
		/// 
		/// <seealso cref="GetSize"/>
		/// <seealso cref="Utils.UpdateApp"/>
		/// <seealso cref="Utils.PatternEditor"/>
		/// 
		/// <param name="folder">Nazwa folderu do przeszukania plików.</param>
		/// <param name="recursive">Wyszukiwanie rekursywne.</param>
		/// 
		/// <returns>Lista plików w wybranym folderze.</returns>
		//* ============================================================================================================
		public static List<string> GetFilesFromFolder( string folder, bool recursive = false )
		{
			var files   = new List<string>();
			var dirinfo = new DirectoryInfo( folder );

			// pobierz listę plików
			foreach( var file in dirinfo.GetFiles() )
				if( folder[folder.Length-1] == '\\' || folder[folder.Length-1] == '/' )
					files.Add( folder + file.Name );
				else
					files.Add( folder + "/" + file.Name );
			
			// w przypadku gdy włączone przeszukiwanie rekursywne, szukaj folderów
			if( recursive )
				foreach( var dir in dirinfo.GetDirectories() )
					// gdy znajdzie się folder, uruchom funkcję ponownie dla znalezionego folderu
					if( folder[folder.Length-1] == '\\' || folder[folder.Length-1] == '/' )
						files.AddRange( Program.GetFilesFromFolder(folder + dir.Name) );
					else
						files.AddRange( Program.GetFilesFromFolder(folder + "/" + dir.Name) );

			return files;
		}

#endregion

#region WYŚWIETLANIE WIADOMOŚCI

		/// <summary>
		/// Zwiększa wcięcie wyświetlanych danych.
		/// Funkcja zwiększa wcięcie dla wyswietlanych informacji w konsoli lub zapisywanych w pliku.
		/// Działa tylko na wiadomościach informacyjnych - funkcja <see cref="LogMessage"/>.
		/// </summary>
		/// 
		/// <seealso cref="DecreaseLogIndent"/>
		/// <seealso cref="LogMessage"/>
		//* ============================================================================================================
		public static void IncreaseLogIndent()
		{
#		if LOGMESSAGE || DEBUG
			Program._indent += "    ";
#		endif
		}

		/// <summary>
		/// Zmniejsza wcięcie wyświetlanych danych.
		/// Funkcja zmniejsza wcięcie dla wyswietlanych informacji w konsoli lub zapisywanych w pliku.
		/// Działa tylko na wiadomościach informacyjnych - funkcja <see cref="LogMessage"/>.
		/// </summary>
		/// 
		/// <seealso cref="IncreaseLogIndent"/>
		/// <seealso cref="LogMessage"/>
		//* ============================================================================================================
		public static void DecreaseLogIndent()
		{
#		if LOGMESSAGE || DEBUG
			if( Program._indent.Length > 7 )
				Program._indent = Program._indent.Substring( 0, Program._indent.Length - 4 );
			else if( Program._indent.Length > 0 )
				Program._indent = "";
#		endif
		}
		
		/// <summary>
		/// Wyświetlenie wiadomości w konsoli i/lub zapis do pliku.
		/// Funkcja w zależności od ustawień zapisuje wiadomość podaną w treści do pliku.
		/// Dodatkowo gdy jest taka możliwość, wyświetla ją również w konsoli w trybie debugowania.
		/// W przypadku zapisu do pliku, przed treścią wiadomości wyświetlana jest dokładna data jej zapisania.
		/// </summary>
		/// 
		/// <seealso cref="LogInfo"/>
		/// <seealso cref="LogWarning"/>
		/// <seealso cref="LogError"/>
		/// <seealso cref="LogQuestion"/>
		/// <seealso cref="IncreaseLogIndent"/>
		/// <seealso cref="DecreaseLogIndent"/>
		/// 
		/// <param name="message">Wiadomość do wyświetlenia i/lub zapisania.</param>
		//* ============================================================================================================
		public static void LogMessage( string message )
		{
#		if LOGMESSAGE || DEBUG
			message = Program._indent + message;
#		endif
#		if LOGMESSAGE
			if( Program._writer != null )
				Program._writer.WriteLine( DateTime.Now.ToString("HH:mm:ss.ff") + " >> " + message );
#		endif
#		if DEBUG
			Console.WriteLine( message );
#		endif
		}
		
		/// <summary>
		/// Wyświetla okienko informacyjne z podaną wiadomością.
		/// Funkcja wyświetla okno informacyjne o podanym tytule i wiadomości.
		/// W zależności od ustawień, gdy włączone śledzenie, wiadomość zapisywana jest również do pliku.
		/// W trybie debugowania, treść wiadomości wyświetlana jest w konsoli.
		/// </summary>
		/// 
		/// <seealso cref="LogMessage"/>
		/// <seealso cref="LogWarning"/>
		/// <seealso cref="LogError"/>
		/// <seealso cref="LogQuestion"/>
		/// 
		/// <param name="message">Wiadomość do wyświetlenia.</param>
		/// <param name="title">Tytuł okienka z wiadomością.</param>
		/// <param name="parent">Rodzic, względem którego otwierane będzie okno modalne.</param>
		//* ============================================================================================================
		public static void LogInfo( string message, string title, Form parent = null )
		{
			Program.LogMessage( "INFO: " + message );

			if( parent == null )
				parent = Program._main;

			// pokaż treść ostrzeżenia
			MessageBox.Show( parent, message, title, MessageBoxButtons.OK, MessageBoxIcon.Information );
		}
		
		/// <summary>
		/// Wyświetla okienko z ostrzeżeniem o poadnej treści.
		/// Funkcja wyświetla okno z ostrzeżeniem o podanym tytule i treści.
		/// W zależności od ustawień, gdy włączone śledzenie, wiadomość zapisywana jest również do pliku.
		/// W trybie debugowania, treść wiadomości wyświetlana jest w konsoli.
		/// </summary>
		/// 
		/// <seealso cref="LogMessage"/>
		/// <seealso cref="LogInfo"/>
		/// <seealso cref="LogError"/>
		/// <seealso cref="LogQuestion"/>
		/// 
		/// <param name="message">Wiadomość do wyświetlenia.</param>
		/// <param name="title">Tytuł okienka z wiadomością.</param>
		/// <param name="parent">Rodzic, względem którego otwierane będzie okno modalne.</param>
		//* ============================================================================================================
		public static void LogWarning( string message, string title, Form parent = null )
		{
			Program.LogMessage( "WARNING: " + message );

			if( parent == null )
				parent = Program._main;

			// pokaż treść ostrzeżenia
			MessageBox.Show( parent, message, title, MessageBoxButtons.OK, MessageBoxIcon.Warning );
		}

		/// <summary>
		/// Wyświetla komunikat z zapytaniem.
		/// Funkcja wyświetla komunikat z zapytaniem o podanej treści i tytule.
		/// W zależności od ustawień, gdy włączone śledzenie, komunikat z wynikiem zapisywany jest również do pliku.
		/// W trybie debugowania, treść komunikatu razem z wynikiem wyświetlana jest w konsoli.
		/// </summary>
		/// 
		/// <seealso cref="LogMessage"/>
		/// <seealso cref="LogInfo"/>
		/// <seealso cref="LogWarning"/>
		/// <seealso cref="LogError"/>
		/// 
		/// <param name="message">Wiadomość do wyświetlenia.</param>
		/// <param name="title">Tytuł okienka z wiadomością.</param>
		/// <param name="defyes">Czy domyślnie ma być zaznaczony przycisk TAK.</param>
		/// <param name="parent">Rodzic, względem którego otwierane będzie okno modalne.</param>
		/// 
		/// <returns>Wynik wyświetlonego okienka (wciśnięty przycisk).</returns>
		//* ============================================================================================================
		public static DialogResult LogQuestion( string message, string title, bool defyes = true, Form parent = null )
		{
			Program.LogMessage( "QUESTION: " + message );

			if( parent == null )
				parent = Program._main;

			// pokaż treść pytania
			var result = MessageBox.Show( parent, message, title, MessageBoxButtons.YesNo, MessageBoxIcon.Question,
				defyes ? MessageBoxDefaultButton.Button1 : MessageBoxDefaultButton.Button2 );

			if( result == DialogResult.Yes )
				Program.LogMessage( "Wybrano przycisk: TAK." );
			else
				Program.LogMessage( "Wybrano przycisk: NIE." );

			return result;
		}
		
		/// <summary>
		/// Wyświetla komunikat z błędem o podanej treści.
		/// Funkcja wyświetla komunikat z błędem o podanym tytule i treści.
		/// W przypadku gdy jest to błąd krytyczny, po wyświetleniu wiadomości program jest zamykany.
		/// Funkcja umożliwia prześledzenie i wyświetlenie ścieżki, po której błąd wystąpił.
		/// W zależności od ustawień, gdy włączone śledzenie, komunikat zapisywany jest również do pliku.
		/// W trybie debugowania, treść komunikatu wyświetlana jest w konsoli.
		/// </summary>
		/// 
		/// <seealso cref="LogMessage"/>
		/// <seealso cref="LogInfo"/>
		/// <seealso cref="LogWarning"/>
		/// <seealso cref="LogQuestion"/>
		/// 
		/// <param name="message">Wiadomość do wyświetlenia.</param>
		/// <param name="title">Tytuł okienka z wiadomością.</param>
		/// <param name="fatal">Czy wyświetlany błąd jest błędem krytycznym?</param>
		/// <param name="ex">Przechwycony wyjątek z którego dane będą odczytywane.</param>
		/// <param name="parent">Rodzic, względem którego otwierane będzie okno modalne.</param>
		//* ============================================================================================================
		public static void LogError( string message, string title, bool fatal, Exception ex = null, Form parent = null )
		{
			Program.LogMessage( "ERROR: " + message );

#		if LOGMESSAGE
#			if TRACE
				if( Program._writer != null && ex != null )
					Program._writer.WriteLine( ex.StackTrace );
#			endif
#		endif
#		if DEBUG
#			if TRACE
				Console.WriteLine( System.Environment.StackTrace );
#			endif
#		endif

			if( parent == null )
				parent = Program._main;

			// pokaż treść błędu
			try
				{ MessageBox.Show( parent, message, title, MessageBoxButtons.OK, MessageBoxIcon.Error ); }
			catch
				{ fatal = true; }

			if( fatal )
				Program.ExitApplication( true );
		}

#endregion

#region POZOSTAŁE

		/// <summary>
		/// Zamiana pierwszych liter słów na duże.
		/// Zamienia wszystkie pierwsze litery, nawet pojedynczych znaków oddzielonych spacjami.
		/// </summary>
		/// 
		/// <seealso cref="Utils.DataFilter"/>
		/// 
		/// <param name="text">Tekst do transformacji.</param>
		/// 
		/// <returns>Zamieniony ciąg znaków.</returns>
		//* ============================================================================================================
		public static string StringTitleCase( string text )
		{
			char[] test = text.ToLower( ).ToCharArray( );
			bool space = true;

			// powiększaj pierwsze litery zdania (jak w nazwie własnej)
			for( int x = 0, y = test.Count(); x < y; ++x )
			{
				// jeżeli wcześniej wykryto spacje, zamień literę na dużą
				if( space == true && test[x] != '.' && test[x] != ' ' )
				{
					test[x] = Char.ToUpper( test[x] );
					space = false;
				}
				// wykryto spacje...
				if( test[x] == ' ' )
					space = true;
			}

			return new string( test );
		}

		/// <summary>
		/// Sprawdza, czy plik <em>CDRestore</em> nie jest zablokowany.
		/// Gdy aplikacja <em>CDRestore</em> jest włączona lub inny proces blokuje dostęp do pliku aplikacji,
		/// program wyświetla odpowiedni komunikat - błąd krytyczny.
		/// Dzieje się tak, ponieważ nie można uruchomić dwóch instancji programu, a <em>CDRestore</em> jest swoistym
		/// podprogramem aplikacji <em>CDesigner</em>.
		/// Funkcja umożliwia również odczekanie na zamknięcie aplikacji lub odblokowanie pliku.
		/// </summary>
		/// 
		/// <seealso cref="IsFileLocked"/>
		/// 
		/// <param name="wait">Czeka na zamknięcie aplikacji.</param>
		//* ============================================================================================================
		private static void CDRestoreLockCheck( bool wait )
		{
			bool is_locked = true;

#		if DEBUG
			Program.LogMessage( "Sprawdzanie czy plik 'cdrestore.exe' nie jest zablokowany." );
#		endif

			// przetwarzaj dopóki plik jest blokowany
			while( is_locked )
			{
				try
				{
					// sprawdź czy plik nie jest blokowany
					using( FileStream stream = File.Open("./cdrestore.exe", FileMode.Open, FileAccess.ReadWrite) )
						stream.Close();

					is_locked = false;

					// sprawdź czy plik CDRestore.exe można podmienić
					if( File.Exists("./cdrestore.res") )
					{
						File.Delete( "./cdrestore.exe" );
						File.Move( "./cdrestore.res", "./cdrestore.exe" );
					}
				}
				catch( Exception ex )
				{
					// jeżeli plik jest blokowany
					if( Program.IsFileLocked(ex) )
					{
#					if DEBUG
						Program.LogMessage( "Plik 'cdrestore.exe' jest blokowany - prawdopodobnie program jest włączony." );
#					endif

						// czekaj na zakończenie poprzedniej instancji programu
						if( wait )
						{
#						if DEBUG
							Program.LogMessage( "Usypianie wątku na pół sekundy..." );
#						endif

							Thread.Sleep( 500 );
						}
						// lub rzuć wyjątek
						else
							throw new Exception( "__FILEISLOCKED__" );
					}
					// jeżeli plik nie jest blokowany, a wystapił wyjątek, wyświetl błąd...
					else
					{
						Program.LogError( ex.Message, "Błąd aplikacji", true, ex );
						Program.ExitApplication( true );
					}
				}
			}
		}
		
		/// <summary>
		/// Zamienia wartość na rozmiar pliku.
		/// Funkcja sprawdza, czy podana wartość da się rady skrócić, jeżeli tak, zwiększa jednostkę i próbuje dalej.
		/// </summary>
		/// 
		/// <seealso cref="GetFilesFromFolder"/>
		/// 
		/// <param name="value">Rozmiar do zamiany.</param>
		/// 
		/// <returns>Zmieniona wartość na rozmiar pliku.</returns>
		//* ============================================================================================================
		public static string GetSize( Int64 value )
		{
			string[] suffixes =
			{
				"B",
				"kB",
				"MB",
				"GB",
				"TB",
				"PB"
			};

			if( value < 0 )
				return "-" + GetSize( -value );

			decimal dec = (decimal)value;

			int x;
			for( x = 0; dec > 1023; ++x )
				dec /= 1024; 

			return string.Format( "{0:n1} {1}", dec, suffixes[x] );
		}
		
		/// <summary>
		/// Otwiera podany formularz z odpowiednimi opcjami.
		/// Pozwala na otwarcie zarówno zwykłego okna jak i okna dialogowego z wybranym rodzicem.
		/// </summary>
		/// 
		/// <seealso cref="Forms.DataReaderform"/>
		/// <seealso cref="Forms.InfoForm"/>
		/// <seealso cref="Forms.UpdateForm"/>
		/// 
		/// <param name="form">Formularz do otwarcia.</param>
		/// <param name="parent">Formularz traktowany jako rodzic.</param>
		/// <param name="dialog">Czy okno ma być otwarte jako okienko modalne?</param>
		/// 
		/// <returns>Wynik zwracany przez okno modalne lub wartość None.</returns>
		//* ============================================================================================================
		public static DialogResult OpenForm( Form form, Form parent, bool dialog )
		{
			// ustaw zwracaną wartość - domyślnie nic, oznacza iż formularz nadal działa
			DialogResult result = DialogResult.None;

			// sprawdź czy ma to być okienko modalne czy nie
			if( dialog )
				if( parent != null )
					result = form.ShowDialog( parent );
				else
					result = form.ShowDialog();
			else
				if( parent != null )
					form.Show( parent );
				else
					form.Show();

			// zwróć wynik
			return result;
		}

#endregion
	}
}
