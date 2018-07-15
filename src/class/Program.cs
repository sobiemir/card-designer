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

///
/// $c01 Program.cs
/// 
/// Klasa z głównymi funkcjami programu.
/// Funkcje używane w większości klas.
///
/// LOG NOTE:
/// ----------------------------------------------------------------------------
/// Zmiany w nowych wersjach:
/// 0.8.x.x:
/// - Poprawiono wyświetlanie ikony w oknach programu.
/// - Dodano osobne wczytywanie bazy danych.
/// - Dodano Edytor Kolumn.
/// - Utworzono parser plików językowych.
/// 0.7.x.x:
/// - Wersja początkowa programu przeznaczonego już do użytku.
/// ----------------------------------------------------------------------------
/// 
/// Autor: Kamil Biały
/// Od wersji: 0.7.x.x
/// Ostatnia zmiana: 2015-12-02
/// 
/// (+) - klasa zakończona
/// (-) - klasa do poprawy
/// (#) - klasa zakończona, zawiera TODO lub SETTINGS lub DELETE
/// 
/// Ukończone klasy:
/// # Program (378)
/// # Settings (298)
/// + Language (361)
/// + AssemblyLoader (104)
/// + ProgressStream (222)
/// + DatabaseSettingsForm (326)
/// # DatabaseReader (602)
/// - EditColumnsForm (480)
/// - CBackupData (573)
/// - Structures (230)
/// - DataFilterForm (849)
/// - DataFilterRow (138)
/// - FilterCreator (282)
/// - PatternEditor (904)
/// - AlignedPage (82)
/// - AlignedPictureBox (110)
/// - GroupComboBox (1587)
/// - PageField (469)
/// - DatabaseFilterForm (849)
/// - DataReader (537)
/// - DBConnection (21)
/// - InfoForm (63)
/// - MainForm (2959)
/// - NewPattern (285)
/// - SettingsForm (740)
/// - UpdateForm (429)
/// 
/// TODO: Zmienna _avaliable_chars... wczytywana z klasy Language?
///

namespace CDesigner
{
	/// 
	/// <summary>
	/// Klasa główna aplikacji.
	/// Zawiera funkcje do informacji o błędach / ostrzeżeniach...
	/// Zawiera najczęściej używane funkcje we wszystkich formularzach.
	/// </summary>
	/// 
	static class Program
	{
		// ===== PRIVATE VARIABLES ==============================================================

		/// <summary>Strumień pliku z przetwarzanymi informacjami.</summary>
		private static StreamWriter _writer = null;
		
		/// <summary>Główne okno aplikacji.</summary>
		private static Form _main = null;

		/// <summary>Ikona aplikacji...</summary>
		private static Icon _icon = null;
		
		/// START DELETE @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@ DELETE
		/// <summary>Znaki dodatkowe dopuszczalne w nazwach folderów, plików, itp...</summary>
		private static string _avaliable_chars = "ĘÓŁŚĄŻŹĆŃęółśążźćń";
		/// STOP  DELETE @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@ DELETE

		/// <summary>Lista obrazków używanych w programie.</summary>
		private static Bitmap[] _bitmaps = null;

		private static string[] _bitmapList = {
			"icons/cdesigner-16.png",  "icons/cdesigner-32.png",  "icons/cdesigner-48.png",
			"icons/cdesigner-64.png",  "icons/cdesigner-96.png",  "icons/cdesigner-128.png",
			"icons/cdesigner-256.png", "icons/cdesigner-512.png", "icons/cdrestore-16.png",
			"icons/cdrestore-32.png",  "icons/cdrestore-48.png",  "icons/cdrestore-64.png",
			"icons/cdrestore-96.png",  "icons/cdrestore-128.png", "icons/cdrestore-256.png",
			"icons/cdrestore-512.png", "icons/item-add.png",      "icons/item-delete.png",
			"icons/refresh.png",       "icons/first-page.png",    "icons/prev-page.png",
			"icons/next-page.png",     "icons/last-page.png"
		};

		// ===== PUBLIC VARIABLES ===============================================================

		/// <summary>Wersja aplikacji.</summary>
		public static readonly string VERSION;

		/// <summary>Data kompilacji.</summary>
		public static readonly DateTime BUILD_DATE;

		// ===== CONSTRUCTORS / DESTRUCTORS =====================================================

		/**
		 * <summary>
		 * Konstruktor statyczny klasy.
		 * Pobiera informacje o dacie kompilacji i wersji aplikacji.
		 * </summary>
		 * --------------------------------------------------------------------------------------------------------- **/
		static Program()
		{
			Version version = Assembly.GetExecutingAssembly().GetName().Version;

			Program.VERSION    = version.ToString();
			Program.BUILD_DATE = new DateTime( 2000, 1, 1 ).Add( new TimeSpan
			(
				TimeSpan.TicksPerDay    * version.Build +
				TimeSpan.TicksPerSecond * 2 * version.Revision
			) );

			Program._bitmaps = new Bitmap[Program._bitmapList.Length];

			for( int x = 0; x < Program._bitmaps.Length; ++x )
				Program._bitmaps[x] = null;
		}

		// ===== GETTERS / SETTERS ==============================================================
		
		/**
		 * <summary>
		 * Pobranie bitmapy z folderu "IMAGES".
		 * </summary>
		 * --------------------------------------------------------------------------------------------------------- **/
		public static Bitmap GetBitmap( BMPSRC index )
		{
			if( Program._bitmaps[(int)index] == null )
				Program._bitmaps[(int)index] = LoadBitmap( index );

			return Program._bitmaps[(int)index];
		}

		/**
		 * <summary>
		 * Pobranie ikony aplikacji.
		 * </summary>
		 * --------------------------------------------------------------------------------------------------------- **/
		public static Icon GetIcon()
		{
			return Program._icon;
		}

		/// START DELETE @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@ DELETE
		/**
		 * <summary>
		 * Zwróć dopuszczalne znaki dodatkowe.
		 * </summary>
		 * --------------------------------------------------------------------------------------------------------- **/
		public static string AvaliableChars
		{
			get { return Program._avaliable_chars; }
		}
		/// STOP  DELETE @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@ DELETE

		// ===== PUBLIC FUNCTIONS ===============================================================
		

		
		/**
		 * <summary>
		 * Funkcja do sprawdzania blokady pliku cdrestore.exe.
		 * Gdy aplikacja CDRestore jest włączona, blokuje plik i program wyświetla błąd.
		 * </summary>
		 * 
		 * <param name="wait">Czeka na zamknięcie aplikacji.</param>
		 * --------------------------------------------------------------------------------------------------------- **/
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

					// sprawdź czy plik cdrestore.exe można podmienić
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
						Program.ExitApplication();
					}
				}
			}
		}

		/**
		 * <summary>
		 * Funkcja startowa aplikacji.
		 * Argument -w czeka na zamknięcie aplikacji CDRestore.exe
		 * </summary>
		 * 
		 * <param name="args">Argumenty aplikacji.</param>
		 * --------------------------------------------------------------------------------------------------------- **/
		[STAThread]
		public static void Main( string[] args )
		{
			
			bool warg = false;

			// szukaj parametru -w
			foreach( string arg in args )
				if( arg == "-w" )
					warg = true;

			try
			{
				// otwórz plik do zapisu
				Program._writer = new StreamWriter( File.Open("./cdesigner.log", FileMode.Create, FileAccess.Write) );

#			if DEBUG
				Program.LogMessage( "# -------------------------------------------------------------------- //" );
				Program.LogMessage( "# Program: CDesigner " );
				Program.LogMessage( "# Wersja : " + Program.VERSION );
				Program.LogMessage( "# Data   : " + Program.BUILD_DATE );
				Program.LogMessage( "# Autor  : Kamil Biały " );
				Program.LogMessage( "# -------------------------------------------------------------------- //" );
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
						Program.LogError(
							ex.Message == "__FILEISLOCKED__"
								? Language.GetLine("GlobalErrors", (int)MSGBLIDX.CDRestoreIsRunning)
								: Language.GetLine("GlobalErrors", (int)MSGBLIDX.CDesignerIsRunning),
							Language.GetLine("GlobalErrors", (int)MSGBLIDX.FatalErrorTitle),
							true
						);
						return;
					}
					// ups... inny błąd?
					Program.LogError( ex.Message, "Fatal error", true, ex );
					return;
				}
				catch( Exception ex_inner )
				{
					Console.WriteLine( "BŁĄD!!!" );
					// błąd w błędzie... to ci dopiero heca!
					Program.LogError( ex_inner.Message, "Fatal error", true, ex_inner );
					return;
				}
			}
			
#		if DEBUG
			Program.LogMessage( ">> Uruchamianie programu CDesigner." );
#		endif

			// można uruchamiać aplikację...
			try
			{
				// załaduj ikonę programu, ale nie zgłaszaj wyjątku jako błędu krytycznego
				try
					{ Program._icon = new Icon( "./icons/cdesigner.ico" ); }
				catch( IOException ex )
					{ Program.LogMessage( "Nie można wczytać ikony programu - " + ex.Message ); }
				
				// style wizualne formularza
				Application.EnableVisualStyles();
				Application.SetCompatibleTextRenderingDefault( false );
				
				// rejestruj zdarzenia do rozwiązywania problemów z plikami dll
				AssemblyLoader.Register();
				
				// wczytaj ustawienia
				Settings.Initialize();
				Settings.Parse();

				// wczytaj język
				Language.Initialize();
				Language.Parse();

				// uruchom program
				Program._main = new MainForm();
				//Program._main = new EditDataForm( true, true, true );

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
			{
				// zamknij plik...
				if( Program._writer != null )
				{
					Program._writer.Flush();
					Program._writer.Close();
				}
				Program._writer = null;
			}
		}

		/**
		 * <summary>
		 * Zamyka aplikacje i plik raportu.
		 * </summary>
		 * --------------------------------------------------------------------------------------------------------- **/
		public static void ExitApplication()
		{
			// zamknij dziennik zdarzeń
			if( Program._writer != null )
			{
				Program._writer.Flush();
				Program._writer.Close();
			}
			Program._writer = null;

			Application.Exit();
		}

		/**
		 * <summary>
		 * Funkcja sprawdza czy plik jest zablokowany przez inny proces.
		 * </summary>
		 * 
		 * <param name="ex">Wyjątek który został przechwycony przy otwieraniu pliku.</param>
		 * --------------------------------------------------------------------------------------------------------- **/
		public static bool IsFileLocked( Exception ex )
		{
			int error = Marshal.GetHRForException( ex ) & ((1 << 16) - 1);
					
			// ERROR_SHARING_VIOLATION[32] || ERROR_LOCK_VIOLATION[33]
			if( error == 32 || error == 33 )
				return true;

			return false;
		}

		/**
		 * <summary>
		 * Funkcja zapisuje wiadomość do pliku i wyświetla ją w konsoli.
		 * </summary>
		 * 
		 * <param name="message">Wiadomość do wyświetlenia.</param>
		 * --------------------------------------------------------------------------------------------------------- **/
		public static void LogMessage( string message )
		{
#		if LOGMESSAGE
			if( Program._writer != null )
				Program._writer.WriteLine( DateTime.Now.ToString("HH:mm:ss.ff") + " >> " + message );
#		endif
#		if DEBUG
			Console.WriteLine( message );
#		endif
		}

		/**
		 * <summary>
		 * Funkcja zapisuje wiadomość do pliku oraz wyświetla ją w oknie i konsoli.
		 * </summary>
		 * 
		 * <param name="message">Wiadomość do wyświetlenia.</param>
		 * <param name="title">Tytuł okienka.</param>
		 * --------------------------------------------------------------------------------------------------------- **/
		public static void LogInfo( string message, string title )
		{
#		if LOGMESSAGE
			if( Program._writer != null )
				Program._writer.WriteLine( DateTime.Now.ToString("HH:mm:ss.ff") + " >> INFO: " + message );
#		endif
#		if DEBUG
			Console.WriteLine( "INFO: " + message );
#		endif

			// pokaż treść ostrzeżenia
			MessageBox.Show( Program._main, message, title, MessageBoxButtons.OK, MessageBoxIcon.Information );
		}

		/**
		 * <summary>
		 * Funkcja zapisuje wiadomość do pliku oraz wyświetla w oknie i konsoli ostrzeżenie.
		 * </summary>
		 * 
		 * <param name="message">Wiadomość do wyświetlenia.</param>
		 * <param name="title">Tytuł okienka.</param>
		 * --------------------------------------------------------------------------------------------------------- **/
		public static void LogWarning( string message, string title )
		{
#		if LOGMESSAGE
			if( Program._writer != null )
				Program._writer.WriteLine( DateTime.Now.ToString("HH:mm:ss.ff") + " >> WARNING: " + message );
#		endif
#		if DEBUG
			Console.WriteLine( "WARNING: " + message );
#		endif

			// pokaż treść ostrzeżenia
			MessageBox.Show( Program._main, message, title, MessageBoxButtons.OK, MessageBoxIcon.Warning );
		}
		
		/**
		 * <summary>
		 * Funkcja zapisuje do pliku oraz wyświetla w oknie i konsoli błąd.
		 * Jeżeli błąd jest krytyczny, funkcja po wyświetleniu wiadomości zamyka strumień pliku i program.
		 * Pozwala również na wyświetlenie funkcji które odwoływały się do rzucanego błędu.
		 * </summary>
		 * 
		 * <param name="message">Wiadomość do wyświetlenia.</param>
		 * <param name="title">Tytuł okienka.</param>
		 * <param name="fatal">Błąd krytyczny - powoduje zamknięcie aplikacji.</param>
		 * --------------------------------------------------------------------------------------------------------- **/
		public static void LogError( string message, string title, bool fatal, Exception ex = null )
		{
#		if LOGMESSAGE
			if( Program._writer != null )
				Program._writer.WriteLine( DateTime.Now.ToString("HH:mm:ss.ff") + " >> ERROR: " + message );

			// lista ramek stosu
#			if TRACE
				if( Program._writer != null && ex != null )
					Program._writer.WriteLine( ex.StackTrace );
#			endif
#		endif
#		if DEBUG
			Console.WriteLine( "ERROR: " + message );
			// lista ramek stosu
#			if TRACE
				Console.WriteLine( System.Environment.StackTrace );
#			endif

#		endif
			// pokaż treść błędu
			try
				{ MessageBox.Show( Program._main, message, title, MessageBoxButtons.OK, MessageBoxIcon.Error ); }
			catch
				{ fatal = true; }

			if( fatal )
				Program.ExitApplication();
		}

		/**
		 * <summary>
		 * Zamiana pierwszych liter ciągu znaku na duże.
		 * </summary>
		 * 
		 * <param name="text">Ciąg znaków do zamiany.</param>
		 * --------------------------------------------------------------------------------------------------------- **/
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
		
		// ===== PRIVATE FUNCTIONS ==============================================================
		
		/**
		 * <summary>
		 * Ładowanie brakującej bitmapy dla programu.
		 * </summary>
		 * --------------------------------------------------------------------------------------------------------- **/
		private static Bitmap LoadBitmap( BMPSRC index )
		{
			Bitmap bitmap = null;

			// gdy wystąpi błąd, wyświetl go, ale nie traktuj go jako krytycznego...
			try
				{ bitmap = new Bitmap( "./" + Program._bitmapList[(int)index] ); }
			catch( Exception ex )
			{
				Program.LogMessage( ">> Nie można załadować bitmapy: './images/" +
					Program._bitmapList[(int)index] + "' - " + ex.Message );
				bitmap = null;
			}

			// zwróć załadowaną bitmapę
			return bitmap;
		}
	}
}
