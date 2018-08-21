using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace CDRestore
{
	///
	/// Główna klasa programu.
	/// Zawiera podstawowe funkcje programu.
	///
	static class Program
	{
		/// Strumień pliku z przetwarzanymi informacjami.
		private static StreamWriter _writer = null;

		/// Główne okno aplikacji.
		private static Form _main = null;

		/// Ikona aplikacji...
		private static Icon _icon = null;

		/// 
		/// Funkcja startowa aplikacji.
		/// Argument -i oznacza szybką instalacje kopii zapasowej programu (bez wyboru i dekompresji).
		/// Argument -w czeka na zamknięcie aplikacji CDesigner.exe
		/// ------------------------------------------------------------------------------------------------------------
		[STAThread] static void Main( string[] args )
		{
            bool quick_install  = false,
                 wait_for_close = false;

            try // blok try/catch
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault( false );

                // szukaj parametru -w oraz -i
                foreach( string arg in args )
                    if( arg == "-w" )
                        wait_for_close = true;
                    else if( arg == "-i" )
                        quick_install = true;

_CD_CHECK_ACCESS_TO_SFILE:
                try
                {
                    // sprawdź czy plik nie jest blokowany
                    using( FileStream stream = File.Open("./CDesigner.exe", FileMode.Open, FileAccess.ReadWrite) )
                        stream.Close();

#				if TRACE
                    // otwórz strumień
                    Program._writer = new StreamWriter( File.Open("./debug.log", FileMode.Create, FileAccess.Write) );
#				endif
                    Program.LogMessage( "Uruchamianie programu CDRestore." );
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

                try
                    { Program._icon = new Icon( "./icons/cdrestore.ico" ); }
                catch( IOException ex )
                    { Program.LogMessage( ex.Message ); }

                // instalacja aktualizacji
                if( quick_install )
                {
                    Program.InstallUpdate( "./temp/" );
                    Program.LogMessage( "Uruchamianie nowego procesu dla programu CDesigner." );

                    // uruchomienie programu CDesigner
                    Process process = new Process();
                    process.StartInfo.FileName = "CDesigner.exe";
                    process.StartInfo.Arguments = "-w";
                    process.Start();

                    if( Program._writer != null )
                    {
                        Program._writer.Flush();
                        Program._writer.Close();
                    }
                    return;
                }

                // otwórz główne okno aplikacji
                Program._main = new MainForm();
                Application.Run( Program._main );
            }
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
		/// Instalacja aktualizacji.
		/// Funkcja podmienia pliki programu na pliki z archiwum aktualizacji.
		/// Podmiana pliku CDRestore.exe musi przebiegać w pliku CDesigner.exe!
		/// ------------------------------------------------------------------------------------------------------------
		public static void InstallUpdate( string folder )
		{
			Program.LogMessage( "Instalowanie aktualizacji..." );

			if( !Directory.Exists(folder) )
				throw new Exception( "Folder zawierający wypakowaną aktualizacją nie istnieje!" );

			// dodaj slash do ścieżki
			if( folder.Last() != '/' && folder.Last() != '\\' )
				folder += '/';

			if( !File.Exists(folder + "update.lst") )
				throw new Exception( "Plik zawierający listę plików do aktualizacji nie istnieje!" );

			List<string> names  = new List<string>();
			names.Add( "update.lst" );

			// odczytaj nazwy plików do aktualizacji
			using( StreamReader reader = new StreamReader(
				File.Open(folder + "update.lst", FileMode.Open, FileAccess.Read)) )
			{
				for( string name = reader.ReadLine(); name != null; name = reader.ReadLine() )
				{
					// pomiń wersje
					if( name[0] == '[' )
						continue;

					names.Add( name.Substring(2) );
				}
			}

			// sprawdź czy istnieją wszystkie pliki
			for( int x = 1; x < names.Count; ++x )
				if( !File.Exists(folder + names[x]) )
					throw new Exception( "Aktualizacja nie zawiera wszystkich plików.\n" +
						"Spróbuj ją zainstalować jeszcze raz!" );

			// podmień pliki
			Program.LogMessage( "Kopiowanie plików aktualizacji..." );
			for( int x = 0; x < names.Count; ++x )
			{
				Program.LogMessage( " - " + names[x] + "..." );
				
				// plik CDRestore musi podmienić CDesigner...
				if( names[x] == "CDRestore.exe" )
				{
					if( File.Exists("./cdrestore.res") )
						File.Delete( "./cdrestore.res" );

					// przenieś plik
					new FileInfo("./" + names[x]).Directory.Create();
					File.Move( folder + names[x], "./cdrestore.res" );

					continue;
				}

				// usuń plik jeżeli istnieje
				if( File.Exists("./" + names[x]) )
					File.Delete( "./" + names[x] );

				// przenieś plik
				new FileInfo("./" + names[x]).Directory.Create();
				File.Move( folder + names[x], "./" + names[x] );
			}

			// usuń pozostałości
			Directory.Delete( "./temp/", true );
			Program.LogMessage( "Instalacja aktualizacji przebiegła pomyślnie." );
		}

		/// 
		/// Funkcja zapisuje wiadomość do pliku i wyświetla ją w konsoli.
		/// ------------------------------------------------------------------------------------------------------------
		public static void LogMessage( string message )
		{
#		if TRACE
			Program._writer.WriteLine( message );
#		endif
#		if DEBUG
			Console.WriteLine( message );
#		endif
		}

		/// 
		/// Funkcja zapisuje do pliku oraz wyświetla w oknie i konsoli błąd.
		/// Jeżeli błąd jest krytyczny, funkcja po wyświetleniu wiadomości zamyka strumień pliku i program.
		/// ------------------------------------------------------------------------------------------------------------
		public static void LogError( string message, string title, bool fatal )
		{
#		if TRACE
			if( Program._writer != null )
				Program._writer.WriteLine( "ERROR: " + message );
#		endif
#		if DEBUG
			Console.WriteLine( "ERROR: " + message );
#		endif
			// pokaż treść błędu
			try
				{ MessageBox.Show( null, message, title, MessageBoxButtons.OK, MessageBoxIcon.Error ); }
			catch
				{ fatal = true; }

			// zamknij bufor
			if( fatal && Program._writer != null )
			{
				Program._writer.Flush( );
				Program._writer.Close( );
				GC.Collect( );
			}

			// zakończ działanie aplikacji
			if( fatal )
				Application.Exit( );
		}
	}
}
