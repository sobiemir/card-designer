using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.IO;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Threading;

///
/// $i01 Program.cs
/// 
/// Główne funkcje programu, używane praktycznie wszędzie.
///
/// Zmiany w nowych wersjach:
/// 0.8.????.????:
/// - Poprawiono wyświetlanie ikony w oknach programu.
/// - Dodano osobne wczytywanie bazy danych.
/// - Dodano Edytor Kolumn.
/// 0.7.????.????:
/// - Wersja początkowa programu przeznaczonego już do użytku.
/// 
/// Autor: Kamil Biały
/// Od wersji: 0.7.x.x
/// Ostatnia zmiana: 2015-08-06
///

namespace CDesigner
{
	static class Program
	{
		/// Strumień pliku z przetwarzanymi informacjami.
		private static StreamWriter _writer = null;

		/// Główne okno aplikacji.
		private static Form _main = null;

		/// Ikona aplikacji...
		private static Icon _icon = null;

		/// Znaki dodatkowe dopuszczalne w nazwach folderów, plików, itp...
		private static string _avaliable_chars = "ĘÓŁŚĄŻŹĆŃęółśążźćń";

		///
		/// Zwróć dopuszczalne znaki dodatkowe...
		/// ------------------------------------------------------------------------------------------------------------
		public static string AvaliableChars
		{
			get { return Program._avaliable_chars; }
		}

		///
		/// Funkcja startowa aplikacji.
		/// Argument -w czeka na zamknięcie aplikacji CDRestore.exe
		/// ------------------------------------------------------------------------------------------------------------
		[STAThread] static void Main( string[] args )
		{
			bool wait_for_close = false;
			try
			{
				// szukaj parametru -w
				foreach( string arg in args )
					if( arg == "-w" )
						wait_for_close = true;

_CD_CHECK_ACCESS_TO_SFILE:
				try
				{
					// sprawdź czy plik nie jest blokowany
					using( FileStream stream = File.Open("./CDRestore.exe", FileMode.Open, FileAccess.ReadWrite) )
						stream.Close();

					// sprawdź czy plik CDRestore można podmienić
					if( File.Exists("./CDRestore.res") )
					{
						if( File.Exists("./CDRestore.exe") )
							File.Delete( "./CDRestore.exe" );

						File.Move( "./CDRestore.res", "./CDRestore.exe" );
					}
#				if TRACE
					Program._writer = new StreamWriter( File.Open("./debug.log", FileMode.Create, FileAccess.Write) );
#				endif
					Program.LogMessage( "Uruchamianie programu CDesigner." );
				}
				catch( IOException ex )
				{
					if( Program.IsFileLocked(ex) )
					{
						// czekaj na zakończenie poprzedniej instancji programu
						if( wait_for_close )
						{
							Thread.Sleep( 500 );
							goto _CD_CHECK_ACCESS_TO_SFILE;
						}
						throw new Exception( "Aplikacja jest już uruchomiona!\nKomunikat dotyczy aplikacji " +
							"CDesigner oraz CDRestore - można uruchomić tylko jedną z nich w tym samym czasie!" );
					}
					Program.LogError( ex.Message, "Błąd aplikacji", true );
					return;
				}

				// sprawdź czy istnieje ikona programu
				try
					{ Program._icon = new Icon( "./icons/cdesigner.ico" ); }
				catch( IOException ex )
					{ Program.LogMessage( ex.Message ); }

				Application.EnableVisualStyles();
				Application.SetCompatibleTextRenderingDefault( false );
				
				// uruchom program
				Program._main = new MainForm();
				Application.Run( Program._main );
			}
			// wyłap błąd
			catch( Exception ex )
			{
				Program.LogError( ex.Message, "Błąd aplikacji", true );
				return;
			}

			if( Program._writer != null )
			{
				Program._writer.Flush();
				Program._writer.Close();
			}
		}

		/// 
		/// Zamyka aplikacje i plik raportu.
		/// ------------------------------------------------------------------------------------------------------------
		public static void ExitApplication()
		{
			Application.Exit();
		}

		/// 
		/// Funkcja sprawdza czy plik jest zablokowany przez inny proces.
		/// ------------------------------------------------------------------------------------------------------------
		public static bool IsFileLocked( Exception ex )
		{
			int error = Marshal.GetHRForException( ex ) & ((1 << 16) - 1);
					
			// ERROR_SHARING_VIOLATION[32] || ERROR_LOCK_VIOLATION[33]
			if( error == 32 || error == 33 )
				return true;

			return false;
		}

		/// 
		/// Funkcja pobiera wcześniej wczytaną ikonę aplikacji.
		/// ------------------------------------------------------------------------------------------------------------
		public static Icon GetIcon()
		{
			return Program._icon;
		}

		/// 
		/// Funkcja zapisuje wiadomość do pliku i wyświetla ją w konsoli.
		/// ------------------------------------------------------------------------------------------------------------
		public static void LogMessage( string message )
		{
#		if TRACE
			Program._writer.WriteLine( DateTime.Now.ToString("HH:mm:ss.ff") + " >> " + message );
#		endif
#		if DEBUG
			Console.WriteLine( message );
#		endif
		}

		/// 
		/// Funkcja zapisuje wiadomość do pliku oraz wyświetla ją w oknie i konsoli.
		/// ------------------------------------------------------------------------------------------------------------
		public static void LogInfo( string message, string title )
		{
#		if TRACE
			Program._writer.WriteLine( DateTime.Now.ToString("HH:mm:ss.ff") + " >> INFO: " + message );
#		endif
#		if DEBUG
			Console.WriteLine( "INFO: " + message );
#		endif

			// pokaż treść ostrzeżenia
			MessageBox.Show( Program._main, message, title, MessageBoxButtons.OK, MessageBoxIcon.Information );
		}

		/// 
		/// Funkcja zapisuje wiadomość do pliku oraz wyświetla w oknie i konsoli ostrzeżenie.
		/// ------------------------------------------------------------------------------------------------------------
		public static void LogWarning( string message, string title )
		{
#		if TRACE
			Program._writer.WriteLine( DateTime.Now.ToString("HH:mm:ss.ff") + " >> WARNING: " + message );
#		endif
#		if DEBUG
			Console.WriteLine( "WARNING: " + message );
#		endif

			// pokaż treść ostrzeżenia
			MessageBox.Show( Program._main, message, title, MessageBoxButtons.OK, MessageBoxIcon.Warning );
		}

		/// 
		/// Funkcja zapisuje do pliku oraz wyświetla w oknie i konsoli błąd.
		/// Jeżeli błąd jest krytyczny, funkcja po wyświetleniu wiadomości zamyka strumień pliku i program.
		/// ------------------------------------------------------------------------------------------------------------
		public static void LogError( string message, string title, bool fatal )
		{
#		if TRACE
			if( Program._writer != null )
				Program._writer.WriteLine( DateTime.Now.ToString("HH:mm:ss.ff") + " >> ERROR: " + message );
#		endif
#		if DEBUG
			Console.WriteLine( "ERROR: " + message );
#		endif
			// pokaż treść błędu
			try
				{ MessageBox.Show( Program._main, message, title, MessageBoxButtons.OK, MessageBoxIcon.Error ); }
			catch
				{ fatal = true; }

			// zamknij bufor
			if( fatal && Program._writer != null )
			{
				Program._writer.Flush();
				Program._writer.Close();
				GC.Collect();
			}

			// zakończ działanie aplikacji
			if( fatal )
				Application.Exit();
		}

		/// 
		/// Zamiana pierwszych liter ciągu znaku na duże.
		/// ------------------------------------------------------------------------------------------------------------
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
	}
}
