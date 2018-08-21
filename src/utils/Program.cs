﻿#define LOGMESSAGE

using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.IO;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Threading;
using System.Reflection;
using System.Globalization;

using CDesigner.Forms;
using System.Text;
using System.Diagnostics;

///
/// Copyright ⓒ 2015. Wszystkie prawa zastrzeżone.
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
/// 13-11-2016 - dodano funkcję do tworzenia pytań, itp
/// 
/// Autor: Kamil Biały
/// Od wersji: 0.7.x.x
/// Ostatnia zmiana: 2015-12-02
/// 
/// 
/// (+) - klasa zakończona
/// (-) - klasa do poprawy
/// (#) - klasa zakończona, zawiera todo
/// 
/// [T - ROZSZERZENIA]
/// [D - DOKUMENTACJA]
/// [C - KONTROLKI]
/// [L - LOGI]
/// [S - SEEALSO]
/// [R - REFACTORING]
/// [G - REGION]
/// 
/// Ukończone klasy:
/// + Forms.EditColumnsForm       [1553] SL
/// + Forms.EditRowsForm          [1230] SL
/// + Forms.TypeSettings          [1099] SL
/// + Forms.DatafileSettingsForm  [ 662] SL
/// + Forms.NewPatternForm        [ 529]
/// + Forms.InfoForm              [ 228]
/// + Forms.UpdateForm            [ 740]
/// + Utils.UpdateApp             [ 309]
/// + Utils.Language              [ 415] S
/// + Utils.AssemblyLoader        [ 131]
/// + Utils.DataFilter            [1031] SRL
/// + Utils.DataStorage           [ 404] SL
/// + Utils.IOFileData            [ 730] L
/// 
/// 
/// 
/// Forms.DBConnection
/// Utils.IODatabase
/// 
/// # Utils.DatafileStream [ 766] D
/// 
/// # Program (378)
/// # Settings (298)
/// + Language (361)
/// + ProgressStream (222)
/// + DatabaseSettingsForm (326)
/// # DatabaseReader (602)
/// - CBackupData (573)
/// - Structures (230)
/// - DataFilterForm (849)
/// - DataFilterRow (138)
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

namespace CDesigner.Utils
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
		/// START DELETE @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@ DELETE
		/// <summary>Znaki dodatkowe dopuszczalne w nazwach folderów, plików, itp...</summary>
		private static string _avaliable_chars = "ĘÓŁŚĄŻŹĆŃęółśążźćń";
		/// STOP  DELETE @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@ DELETE
		/// 

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




		// ===== PRIVATE VARIABLES ==============================================================

		/// <summary>Strumień pliku z przetwarzanymi informacjami.</summary>
		private static StreamWriter _writer = null;
		
		/// <summary>Główne okno aplikacji.</summary>
		private static Form _main = null;

		/// <summary>Ikona aplikacji...</summary>
		private static Icon _icon = null;
		
		/// <summary>Aktualne wcięcie (ilość spacji)...</summary>
		private static string _indent = "";
		
		/// <summary>Lista obrazków używanych w programie.</summary>
		private static Bitmap[] _bitmaps = null;

		private static bool _globals = false;

		private static string[] _bitmapList =
		{
			"images/cdesigner-16.png",   "images/cdesigner-32.png",  "images/cdesigner-48.png",
			"images/cdesigner-64.png",   "images/cdesigner-96.png",  "images/cdesigner-128.png",
			"images/cdesigner-256.png",  "images/cdesigner-512.png", "images/cdrestore-16.png",
			"images/cdrestore-32.png",   "images/cdrestore-48.png",  "images/cdrestore-64.png",
			"images/cdrestore-96.png",   "images/cdrestore-128.png", "images/cdrestore-256.png",
			"images/cdrestore-512.png",  "images/information.jpg",   "icons/item-add.png",
			"icons/item-delete.png",     "icons/refresh.png",        "icons/first-page.png",
			"icons/prev-page.png",       "icons/next-page.png",      "icons/last-page.png"
		};

		/// <summary>Klasy globalne, ogólnodostępne dla całego programu.</summary>
		public static GlobalStruct GLOBAL;

		/// <summary>Wersja aplikacji.</summary>
		public static readonly string VERSION;

		/// <summary>Data kompilacji.</summary>
		public static readonly DateTime BUILD_DATE;

		public static readonly string CODE_NAME = "Bobo";

		/// <summary>
		/// Konstruktor statyczny klasy.
		/// Pobiera informacje o dacie kompilacji i wersji aplikacji.
		/// </summary>
		//= =====================================================================================================================
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
		/// Pobiera obrazek z folderu "images" lub "icons".
		/// Wszystkie możliwe obrazki dostępne są pod typem numerycznym BitmapFileEnum.
		/// </summary>
		/// <param name="index">Indeks obrazka do załadowania dostępny z numeracji BitmapFileEnum.</param>
		/// <returns>Obrazek w postaci bitmapy o typie Bitmap.</returns>
		//= =====================================================================================================================
		public static Bitmap GetBitmap( BITMAPCODE index )
		{
			if( Program._bitmaps[(int)index] == null )
				Program._bitmaps[(int)index] = Program._LoadBitmap( index );

			return Program._bitmaps[(int)index];
		}

		/// <summary>
		/// Pobiera ikonę aplikacji.
		/// W razie potrzeby automatycznie ładuje ikonę z pliku.
		/// </summary>
		/// <returns>Ikona aplikacji.</returns>
		//= =====================================================================================================================
		public static Icon GetIcon()
		{
			// załaduj ikonę programu, ale nie zgłaszaj wyjątku jako błędu krytycznego
			if( Program._icon == null )
			{
				try
					{ Program._icon = new Icon( "./icons/cdesigner.ico" ); }
				catch( IOException ex )
					{ Program.LogMessage( "Nie można wczytać ikony programu - " + ex.Message ); }
			}

			return Program._icon;
		}

		/// <summary>
		/// Funkcja startowa aplikacji.
		/// Argument -w czeka na zamknięcie aplikacji CDRestore.exe.
		/// </summary>
		/// <param name="args">Argumenty przekazywane do aplikacji.</param>
		//= =====================================================================================================================
		[STAThread] public static void Main( string[] args )
		{
			
			bool warg = false;

			// szukaj parametru -w
			foreach( string arg in args )
				if( arg == "-w" )
					warg = true;

			try
			{
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
				Program._CDRestoreLockCheck( warg );
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
				Utils.AssemblyLoader.Register();
				
				// wczytaj ustawienia
				Settings.Initialize();
				Settings.Parse();

				// wczytaj język
				Language.Initialize();
				Language.Parse();

				// uruchom główne okno programu
				Program._main = new MainForm();
				
				// ustawienia strumienia danych
				//Program._main = new DatafileSettingsForm();
				//((DatafileSettingsForm)Program._main).fileSelector();

				// edycja kolumn dla strumienia danych
				//Program._main = new EditColumnsForm();
				//IOFileData storage = new IOFileData( "./test/csv/zzz.csv", Encoding.Default );
				//storage.parse( -1 );
				//((EditColumnsForm)Program._main).Storage = storage;

				//Program._main = new EditRowsForm();
				//IOFileData storage = new IOFileData( "./test/csv/zzz.csv", Encoding.Default );
				//storage.parse( -1 );
				//((EditRowsForm)Program._main).Storage = storage;
				//((EditRowsForm)Program._main).refreshDataRange();

				//IOFileData filedata = new IOFileData( "./test/csv/zzz-enclosing.csv", System.Text.Encoding.Default );
				//filedata.parse( 0, true );

				//if( filedata.Ready )
				//{
				//    foreach( string column in filedata.Column )
				//        Program.LogMessage( column );
				//}

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

		/// <summary>
		/// Funkcja zamyka aplikację i plik raportowania danych gdy włączony tryb debugowania.
		/// </summary>
		/// <param name="force"></param>
		//= =====================================================================================================================
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
		/// Sprawdza czy plik nie jest blokowany przez inny proces.
		/// </summary>
		/// <param name="ex">Wyjątek zwracany przy próbie dostępu do pliku.</param>
		/// <returns>True, gdy zablokowany, false gdy błąd dotyczy czegoś innego.</returns>
		//= =====================================================================================================================
		public static bool IsFileLocked( Exception ex )
		{
			int error = Marshal.GetHRForException( ex ) & ((1 << 16) - 1);
					
			// ERROR_SHARING_VIOLATION[32] || ERROR_LOCK_VIOLATION[33]
			if( error == 32 || error == 33 )
				return true;

			return false;
		}

		/// <summary>
		/// Zwiększa wcięcie dla wyswietlanych informacji w konsoli lub zapisywanych w pliku.
		/// </summary>
		//= =====================================================================================================================
		public static void IncreaseLogIndent()
		{
#		if LOGMESSAGE || DEBUG
			Program._indent += "    ";
#		endif
		}

		/// <summary>
		/// Zmniejsza wcięcie dla wyswietlanych informacji w konsoli lub zapisywanych w pliku.
		/// </summary>
		//= =====================================================================================================================
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
		/// </summary>
		/// <param name="message">Wiadomość do wyświetlenia i/lub zapisania.</param>
		//= =====================================================================================================================
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

		/**
		 * <summary>
		 * Funkcja zapisuje wiadomość do pliku oraz wyświetla ją w oknie i konsoli.
		 * </summary>
		 * 
		 * <param name="message">Wiadomość do wyświetlenia.</param>
		 * <param name="title">Tytuł okienka.</param>
		 * --------------------------------------------------------------------------------------------------------- **/
		public static void LogInfo( string message, string title, Form parent = null )
		{
			Program.LogMessage( "INFO: " + message );

			if( parent == null )
				parent = Program._main;

			// pokaż treść ostrzeżenia
			MessageBox.Show( parent, message, title, MessageBoxButtons.OK, MessageBoxIcon.Information );
		}

		/**
		 * <summary>
		 * Funkcja zapisuje wiadomość do pliku oraz wyświetla w oknie i konsoli ostrzeżenie.
		 * </summary>
		 * 
		 * <param name="message">Wiadomość do wyświetlenia.</param>
		 * <param name="title">Tytuł okienka.</param>
		 * --------------------------------------------------------------------------------------------------------- **/
		public static void LogWarning( string message, string title, Form parent = null )
		{
			Program.LogMessage( "WARNING: " + message );

			if( parent == null )
				parent = Program._main;

			// pokaż treść ostrzeżenia
			MessageBox.Show( parent, message, title, MessageBoxButtons.OK, MessageBoxIcon.Warning );
		}

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

		public static void InitializeGlobals()
		{
			if( Program._globals )
				return;

#		if DEBUG
			Program.LogMessage( "Inicjalizacja danych globalnych." );
#		endif

			GLOBAL.SelectFile = new OpenFileDialog();
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
		 * Funkcja do sprawdzania blokady pliku CDRestore.exe.
		 * Gdy aplikacja CDRestore jest włączona, blokuje plik i program wyświetla błąd.
		 * </summary>
		 * 
		 * <param name="wait">Czeka na zamknięcie aplikacji.</param>
		 * --------------------------------------------------------------------------------------------------------- **/
		private static void _CDRestoreLockCheck( bool wait )
		{
			bool is_locked = true;

#		if DEBUG
			Program.LogMessage( "Sprawdzanie czy plik 'CDRestore.exe' nie jest zablokowany." );
#		endif

			// przetwarzaj dopóki plik jest blokowany
			while( is_locked )
			{
				try
				{
					// sprawdź czy plik nie jest blokowany
					using( FileStream stream = File.Open("./CDRestore.exe", FileMode.Open, FileAccess.ReadWrite) )
						stream.Close();

					is_locked = false;

					// sprawdź czy plik CDRestore.exe można podmienić
					if( File.Exists("./CDRestore.res") )
					{
						File.Delete( "./CDRestore.exe" );
						File.Move( "./CDRestore.res", "./CDRestore.exe" );
					}
				}
				catch( Exception ex )
				{
					// jeżeli plik jest blokowany
					if( Program.IsFileLocked(ex) )
					{
#					if DEBUG
						Program.LogMessage( "Plik 'CDRestore.exe' jest blokowany - prawdopodobnie program jest włączony." );
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

		/**
		 * <summary>
		 * Ładowanie brakującej bitmapy dla programu.
		 * </summary>
		 * --------------------------------------------------------------------------------------------------------- **/
		private static Bitmap _LoadBitmap( BITMAPCODE index )
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

		public static string GetMemoryUsage()
		{
			long memory_used = 0;

			using( Process proc = Process.GetCurrentProcess() )
				memory_used = proc.PrivateMemorySize64;

			return Program.GetSize( memory_used );
		}

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

		public static List<string> GetFilesFromFolder( string folder, bool recursive = false )
		{
			List<string> files = new List<string>();

			DirectoryInfo dirinfo = new DirectoryInfo( folder );

			foreach( var file in dirinfo.GetFiles() )
				if( folder[folder.Length-1] == '\\' || folder[folder.Length-1] == '/' )
					files.Add( folder + file.Name );
				else
					files.Add( folder + "/" + file.Name );
			
			if( recursive )
				foreach( var dir in dirinfo.GetDirectories() )
					if( folder[folder.Length-1] == '\\' || folder[folder.Length-1] == '/' )
						files.AddRange( Program.GetFilesFromFolder(folder + dir.Name) );
					else
						files.AddRange( Program.GetFilesFromFolder(folder + "/" + dir.Name) );

			return files;
		}
	}
}
