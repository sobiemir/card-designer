using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.IO;
using System.IO.Compression;
using System.Diagnostics;

namespace CDesigner
{
	public partial class UpdateForm : Form
	{
		private string _version = null;

		private bool _close_on = true;

		/// 
		/// Konstruktor klasy UpdateForm.
		/// Pobiera nową wersje aplikacji i uzupełnia kontrolki...
		/// ------------------------------------------------------------------------------------------------------------
		public UpdateForm()
		{
			// sprawdź czy nowa wersja jest dostępna
			this.GetUpdateVersion();

			// jeżli nowa wersja nie jest dostępna, pomiń dalszą część
			if( !this.UpdateAvaliable() )
				return;

			this.InitializeComponent();

			// podziel wersje na 4 części
			string[] expl = this._version.Split('.');
			DateTime new_ver_date;

			// pobierz datę kompilacji z wersji aplikacji
			try
			{
				new_ver_date = new DateTime( 2000, 1, 1 ).Add( new TimeSpan
				(
					TimeSpan.TicksPerDay * Convert.ToInt32(expl[2]) +
					TimeSpan.TicksPerSecond * 2 * Convert.ToInt32(expl[3])
				) );
			}
			// błąd konwertowania wersji aplikacji...
			catch( Exception ex )
			{
				new_ver_date = new DateTime( 1970, 1, 1 );
				Program.LogMessage( ex.Message );
			}

			// uzupełnij wersje
			this.lCurrentVersion.Text = "Obecna wersja: " + InfoForm.VERSION + ", " +
				InfoForm.BUILD_DATE.ToString( "dd.MM.yyyy" ) + ".";
			this.lNewVersion.Text = "Wersja po aktualizacji: " + this._version + ", " +
				new_ver_date.ToString( "dd.MM.yyyy" ) + ".";

			// pobierz plik readme
			this.GetReadmeFile();
		}

		/// 
		/// Zwraca informacje o tym czy aktualizacja jest dostępna.
		/// ------------------------------------------------------------------------------------------------------------
		public bool UpdateAvaliable()
		{
			return this._version != null;
		}

		/// 
		/// Sprawdza czy dostepna jest nowa aktualizacja dla programu i zapisuje jego wersje.
		/// ------------------------------------------------------------------------------------------------------------
		private void GetUpdateVersion()
		{
			Program.LogMessage( "Wyszukiwanie nowych aktualizacji..." );

			string url = "http://app.aculo.pl/cdesigner/check/" + InfoForm.VERSION;

			// sprawdź czy jest dostępna aktualizacja
			HttpWebRequest  request  = (HttpWebRequest)WebRequest.Create( url );
			HttpWebResponse response = (HttpWebResponse)request.GetResponse();
			
			// pobierz nową wersje programu (jeżeli 0, brak dostępnych aktualizacji)
			Stream reader  = response.GetResponseStream();
			string version = reader.ReadByte() == '0' ? null : new StreamReader(reader).ReadToEnd();
			
			response.Close();
			reader.Close();

			if( version == null )
				Program.LogMessage( "Brak aktualizacji oprogramowania." );
			else
				Program.LogMessage( "Dostępna jest aktualizacja programu do wersji: v" + version + "." );

			this._version = version;
		}

		/// 
		/// Pobiera plik ReadMe z serwera.
		/// ------------------------------------------------------------------------------------------------------------
		private void GetReadmeFile( )
		{
			// sprawdź czy jest dostępna aktualizacja
			HttpWebRequest  request  = (HttpWebRequest)WebRequest.Create( "http://app.aculo.pl/cdesigner/readme" );
			HttpWebResponse response = (HttpWebResponse)request.GetResponse();
			
			// pobierz nową wersje programu (jeżeli 0, brak dostępnych aktualizacji)
			Stream reader  = response.GetResponseStream();
			this.rtbChanges.Text = new StreamReader(reader).ReadToEnd();
			
			response.Close();
			reader.Close();
		}

		/// 
		/// Zdarzenie wykonywane po kliknięciu w przycisk "Aktualizuj program".
		/// Pobiera i przechodzi do dekompresji aktualizacji.
		/// ------------------------------------------------------------------------------------------------------------
		private void bUpdate_Click( object sender, EventArgs ev )
		{
			string key = "61EEBC877DBA9736FD93B2C1CC71C";

			// aktualizacja nie została jeszcze zainstalowana, nie pobieraj ponownie, zainstaluj tą
			/*
			if( Directory.Exists("./temp/") )
			{
			}
			// plik został pobrany wcześniej i wklejony do folderu, nie pobieraj ponownie, rozpakuj tą
			if( File.Exists("./update.cbd") )
			{
			}
			*/

			// ustaw kursor oczekiwania i zabezpiecz przed zamknięciem
			Cursor.Current = Cursors.WaitCursor;
			this._close_on = false;

			Application.UseWaitCursor = true;
			this.bUpdate.Enabled      = false;

			// pobierz plik z serwera
			try { using( WebClient client = new WebClient() )
			{
				client.DownloadProgressChanged -= this.DownloadProgress;
				client.DownloadProgressChanged += this.DownloadProgress;

				client.DownloadFileCompleted -= this.DownloadCompleted;
				client.DownloadFileCompleted += this.DownloadCompleted;

				// CTRL+SHIFT+ALT pakuje aktualizacje
				if( ModifierKeys == (Keys.Control | Keys.Shift | Keys.Alt) )
					this.DownloadCompleted( null,new AsyncCompletedEventArgs(null, false, 0) );
				else
					client.DownloadFileAsync( new Uri("http://app.aculo.pl/cdesigner/download/" + key),
						"./update.cbd" );
			} }
			// przechwyć błąd
			catch( Exception ex )
			{
				Program.LogError( ex.Message, "Aktualizacja programu", false );

				Cursor.Current = Cursors.Default;
				this._close_on = true;

				Application.UseWaitCursor = false;
				this.bUpdate.Enabled      = true;
			}
		}

		/// 
		/// Wyświetla postęp pobierania pliku.
		/// ------------------------------------------------------------------------------------------------------------
		private void DownloadProgress( object sender, DownloadProgressChangedEventArgs ev )
		{
			this.pbUpdate.Value = ev.ProgressPercentage;
		}

		/// 
		/// Zdarzenie wywoływane przy pobraniu całego pliku.
		/// Tworzy i uruchamia wątek który wypakowuje aktualizacje.
		/// Pozwala również utworzyć aktualizacje.
		/// ------------------------------------------------------------------------------------------------------------
		private void DownloadCompleted( object sender, AsyncCompletedEventArgs ev )
		{
			// błąd podczas pobierania aktualizacji
			if( ev.Error != null )
			{
				Program.LogError( ev.Error.Message, "Aktualizacja programu", false );

				Cursor.Current = Cursors.Default;
				this._close_on = true;

				Application.UseWaitCursor = false;
				this.bUpdate.Enabled      = true;

				return;
			}

			// CTRL+SHIFT+ALT pakuje aktualizacje
			if( ModifierKeys == (Keys.Control | Keys.Shift | Keys.Alt) )
			{
				// utwórz aktualizacje
				this.bwTask.DoWork -= this.DecompressUpdate;
				this.bwTask.DoWork -= this.CompressUpdate;
				this.bwTask.DoWork += this.CompressUpdate;

				this.bwTask.ProgressChanged -= this.DecompressionProgress;
				this.bwTask.ProgressChanged -= this.CompressionProgress;
				this.bwTask.ProgressChanged += this.CompressionProgress;

				this.bwTask.RunWorkerAsync();
			}
			else
			{
				// wypakuj aktualizacje
				this.pbUpdate.Value = 0;

				this.bwTask.DoWork -= this.DecompressUpdate;
				this.bwTask.DoWork -= this.CompressUpdate;
				this.bwTask.DoWork += this.DecompressUpdate;

				this.bwTask.ProgressChanged -= this.DecompressionProgress;
				this.bwTask.ProgressChanged -= this.CompressionProgress;
				this.bwTask.ProgressChanged += this.DecompressionProgress;

				this.bwTask.RunWorkerAsync();
			}
		}

		/// 
		/// Kompresja aktualizacji.
		/// ------------------------------------------------------------------------------------------------------------
		private void CompressUpdate( object sender, DoWorkEventArgs ev )
		{
			try // blok try/catch
			{
				List<string> files   = new List<string>();
				List<int>    sizes   = new List<int>();
				string       version = "";

				// dodaj plik z listą skompresowanych plików
				files.Add( "./update.lst" );

				// pobierz listę plików do kompresji
				using( StreamReader lines = new StreamReader(File.Open("./update.lst", FileMode.Open, FileAccess.Read)) )
				{
					version = lines.ReadLine();
					string line = lines.ReadLine();

					while( line != null )
					{
						files.Add( line );
						line = lines.ReadLine();
					}
				}

				// stwórz kopie zapasową
				CBackupData.Compress( files, "./update/v" + version.Substring(1, version.Length-2) + ".cbd" );

				// .cbd [compressed backup data]
				this.bwTask.ReportProgress( 100, (object)2 );
			}
			catch( Exception ex )
			{
				// wyświetl komunikat o błędach
				MessageBox.Show
				(
					"Wystąpił błąd podczas aktualizowania programu.\n" + ex.Message,
					"Błąd aktualizacji...",
					MessageBoxButtons.OK,
					MessageBoxIcon.Error
				);

				// błąd... nie przetwarzaj dalej
				this.bwTask.ReportProgress( 100, (object)1 );
			}
		}

		/// 
		/// Postęp kompresji
		/// ------------------------------------------------------------------------------------------------------------
		private void CompressionProgress( object sender, ProgressChangedEventArgs ev )
		{
			// aktualizuj status
			this.pbUpdate.Value = ev.ProgressPercentage;

			// koniec kompresji, rozpocznij dekompresje
			if( ev.ProgressPercentage == 100 )
			{
				// błąd, przerwij aktualizacje...
				if( (int)ev.UserState == 1 )
				{
					MessageBox.Show( "Błąd!" );

					Cursor.Current = Cursors.Default;
					this._close_on = true;

					Application.UseWaitCursor = false;
					this.bUpdate.Enabled      = true;

					return;
				}
				else if( (int)ev.UserState == 2 )
				{
					MessageBox.Show( "Zakończono!" );

					Cursor.Current = Cursors.Default;
					this._close_on = true;

					Application.UseWaitCursor = false;
					this.bUpdate.Enabled      = true;

					return;
				}
			}
		}

		/// 
		/// Dekompresja aktualizacji.
		/// ------------------------------------------------------------------------------------------------------------
		private void DecompressUpdate( object sender, DoWorkEventArgs ev )
		{
			Program.LogMessage( "Wypakowywanie aktualizacji: './update.cbd'..." );

			try
			{
				// wypakuj dane do folderu "./temp/"
				CBackupData backup = new CBackupData( this.bwTask );
				backup.DecompressUpdate( "./update.cbd", "./temp/" );
			}
			catch( Exception ex )
			{
				// błąd...
				Program.LogError( ex.Message, "Przywracanie danych", false );
				this.bwTask.ReportProgress( 100, 2 );
			}
		}

		/// 
		/// Status dekompresji aktualizacji.
		/// ------------------------------------------------------------------------------------------------------------
		private void DecompressionProgress( object sender, ProgressChangedEventArgs ev )
		{
			// aktualizuj status
			this.pbUpdate.Value = ev.ProgressPercentage;

			// błąd dekompresji
			if( (int)ev.UserState == 2 )
			{
				Cursor.Current = Cursors.Default;
				this._close_on = true;

				Application.UseWaitCursor = false;
				this.bUpdate.Enabled      = true;

				Program.LogMessage( "Wypakowywanie aktualizacji zakończone błędem." );
			}
			// koniec dekompresji, skopiuj pliki
			else if( (int)ev.UserState == 1 )
			{
				Cursor.Current = Cursors.Default;
				this._close_on = true;

				Application.UseWaitCursor = false;
				this.bUpdate.Enabled      = true;

				// pokaż komunikat o ponownym uruchomieniu programu
				DialogResult result = MessageBox.Show
				(
					"Aktualizacja została przygotowana do instalacji.\n" +
					"Czy chcesz uruchomić ponownie program aby ją zainstalować?",
					"Aktualizacja programu",
					MessageBoxButtons.YesNo,
					MessageBoxIcon.Information,
					MessageBoxDefaultButton.Button1
				);

				// przenieś plik do folderu z kopiami zapasowymi aktualizacji
				if( File.Exists("./update/v" + this._version + ".cbd") )
					File.Delete( "./update/v" + this._version + ".cbd" );
				File.Move( "./update.cbd", "./update/v" + this._version + ".cbd" );

				// instalacja aktualizacji
				if( result == DialogResult.Yes )
				{
					Program.LogMessage( "Uruchamianie nowego procesu dla programu CDRestore." );

					// uruchomienie programu CDRestore
					Process process = new Process();
					process.StartInfo.FileName = "CDRestore.exe";
					process.StartInfo.Arguments = "-i -w";
					process.Start();

					// zamykanie aplikacji
					Program.ExitApplication();
				}
			}
		}

		/// 
		/// Funkcja wywoływana podczas zamykania okna.
		/// ------------------------------------------------------------------------------------------------------------
		private void Update_FormClosing( object sender, FormClosingEventArgs ev )
		{
			// nie zamykaj podczas aktualizacji
			if( !this._close_on )
			{
				Program.LogMessage( "Próba zamknięcia okna podczas aktualizacji programu..." );

				MessageBox.Show
				(
					this,
					"Nie możesz zamknąć okna podczas aktualizacji programu.",
					"Zamykanie okna",
					MessageBoxButtons.OK,
					MessageBoxIcon.Information
				);

				ev.Cancel = true;
				return;
			}
		}
	}
}