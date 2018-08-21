using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Reflection;
using System.Diagnostics;

namespace CDRestore
{
	/// 
	/// Klasa okna głównego
	/// Pozwala na przywrócenie i usunięcie kopii zapasowej w trybie graficznym
	/// 
	public partial class MainForm : Form
	{
		/// Blokada zamykania aplikacji podczas kopiowania plików...
		private bool _close_on = true;

		///	
		/// Konstruktor klasy Main.
		/// ------------------------------------------------------------------------------------------------------------
		public MainForm()
		{
			Program.LogMessage( "Tworzenie okna głównego." );
			this.InitializeComponent();
			Program.LogMessage( "Dodawanie plików do tabel." );

			// ikona programu
			this.Icon = Program.GetIcon();
			
			// odśwież pliki w tabelach
			this.ReloadBackupData();

			// pobierz wersje aplikacji
			try
			{
				String version = AssemblyName.GetAssemblyName("CDesigner.exe").Version.ToString();
				this.L_Version.Text = "Zainstalowana wersja: " + version;
			}
			catch( FileNotFoundException ex )
			{
				Program.LogMessage( ex.Message );
				this.L_Version.Text = "Zainstalowana wersja: brak.";
			}
		}

		///	
		/// Funkcja odświeża pliki w tabelach.
		///	Przeszukiwane foldery: "./update/" oraz "./backup/".
		/// ------------------------------------------------------------------------------------------------------------
		public void ReloadBackupData()
		{
			FileInfo[] files    = null;
			ListView   listview = null;

			for( int z = 0; z < 2; ++z )
			{
				// pobierz pliki z folderu update
				if( z == 0 )
				{
					Program.LogMessage( "Odświeżanie listy kopii zapasowych programu..." );

					files    = new DirectoryInfo("./update/").GetFiles();
					listview = this.LV_Program;
				}
				else
				{
					Program.LogMessage( "Odświeżanie listy kopii zapasowych wzorów..." );

					files    = new DirectoryInfo("./backup/").GetFiles();
					listview = this.LV_Pattern;
				}

				// wyczyść tabelę
				listview.Items.Clear();

				// dodaj pliki do tabeli
				for( int x = 0, y = files.Count(); x < y; ++x )
				{
					Program.LogMessage( " - " + files[x].Name );

					byte[] btrash = new byte[32];
					char[] ctrash = new char[32];
					
					listview.Items.Add( files[x].Name );
					
					// informacje dodatkowe
					using( FileStream file = new FileStream(files[x].FullName, FileMode.Open, FileAccess.Read) )
					{
						// data
						file.Read( btrash, 0, 19 );
						btrash[19] = 0;
						btrash.CopyTo( ctrash, 0 );
						listview.Items[x].SubItems.Add( new String(ctrash).Substring(0,10) );
						
						// wersja
						file.Read( btrash, 0, 1 );
						btrash[btrash[0]] = 0;
						file.Read( btrash, 0, btrash[0] > 32 ? 32 : btrash[0] );
						btrash.CopyTo( ctrash, 0 );
						listview.Items[x].SubItems.Add( new String(ctrash) );
					}
				}
			}
		}

		/// 
		/// Funkcja wywoływana podczas zmiany elementu w tablicy z kopiami zapasowymi wzorów.
		/// ------------------------------------------------------------------------------------------------------------
		private void lvPatternBackup_SelectedIndexChanged( object sender, EventArgs ev )
		{
			if( !this._close_on )
				return;

			// brak zaznaczonych elementów, wyłącz przyciski
			if( this.LV_Pattern.SelectedItems.Count == 0 || this.LV_Pattern.Items.Count == 0 )
			{
				this.B_Delete.Enabled  = false;
				this.B_Restore.Enabled = false;

				return;
			}

			this.B_Delete.Enabled  = true;
			this.B_Restore.Enabled = true;
		}

		/// 
		/// Funkcja wywoływana podczas zmiany elementu w tablicy z kopiami zapasowymi programu.
		/// ------------------------------------------------------------------------------------------------------------
		private void lvProgramBackup_SelectedIndexChanged( object sender, EventArgs ev )
		{
			if( !this._close_on )
				return;

			// brak zaznaczonych elementów, wyłącz przyciski
			if( this.LV_Program.SelectedItems.Count == 0 || this.LV_Program.Items.Count == 0 )
			{
				this.B_Delete.Enabled  = false;
				this.B_Restore.Enabled = false;

				return;
			}

			this.B_Delete.Enabled  = true;
			this.B_Restore.Enabled = true;
		}

		/// 
		/// Funkcja wywoływana podczas zmiany zakładki (Program/Wzory).
		/// ------------------------------------------------------------------------------------------------------------
		private void tcBackup_Click( object sender, EventArgs ev )
		{
			if( this.TC_Backups.SelectedTab.Name == "TP_Program" )
				this.lvProgramBackup_SelectedIndexChanged( null, null );
			else
				this.lvPatternBackup_SelectedIndexChanged( null, null );
		}

		/// 
		/// Usuwanie kopii zapasowej.
		/// ------------------------------------------------------------------------------------------------------------
		private void bDelete_Click( object sender, EventArgs ev )
		{
			string folder = "",
				   name   = "";

			// wybierz plik i odpowiedni folder
			if( this.LV_Program.SelectedItems.Count > 0 && this.TC_Backups.SelectedTab.Name == "TP_Program" )
			{
				folder = "./update/";
				name = this.LV_Program.SelectedItems[0].Text;
			}
			else if( this.LV_Pattern.SelectedItems.Count > 0 && this.TC_Backups.SelectedTab.Name == "TP_Pattern" )
			{
				folder = "./backup/";
				name = this.LV_Pattern.SelectedItems[0].Text;
			}

			// wyświetl potwierdzenie
			DialogResult result = MessageBox.Show
			(
				this,
				"Czy na pewno chcesz usunąć plik kopii zapasowej o nazwie: '" + name + "'?.",
				"Usuwanie pliku",
				MessageBoxButtons.YesNo,
				MessageBoxIcon.Warning,
				MessageBoxDefaultButton.Button2
			);

			Program.LogMessage( "Usuwanie pliku kopii zapasowej o nazwie: '" + name + "'" );

			// usuń plik i odśwież pliki w tabelach
			if( result == DialogResult.Yes )
			{
				File.Delete( folder + name );
				this.ReloadBackupData();
			}
		}

		/// Przywracanie plików z kopii zapasowej.
		/// ------------------------------------------------------------------------------------------------------------
		private void bRestore_Click(object sender, EventArgs ev )
		{
			string folder = "./update/",
				   name = this.LV_Program.SelectedItems[0].Text;

			if( this.LV_Pattern.SelectedItems.Count > 0 && this.TC_Backups.SelectedTab.Name == "TP_Pattern" )
			{
				throw new NotImplementedException();
			}

			// wyświetl potwierdzenie
			DialogResult result = MessageBox.Show
			(
				this,
				"Czy na pewno chcesz przywrócić pliki z kopii zapasowej o nazwie: '" + name + "'?.",
				"Przywracanie plików",
				MessageBoxButtons.YesNo,
				MessageBoxIcon.Warning,
				MessageBoxDefaultButton.Button2
			);

			Program.LogMessage( "Rozpoczynanie procedury przywracania programu..." );

			if( result == DialogResult.Yes )
			{
				this.B_Delete.Enabled  = false;
				this.B_Restore.Enabled = false;
				
				Cursor.Current = Cursors.WaitCursor;
				this._close_on = false;
				Application.UseWaitCursor = true;

				// wypakuj pliki kopii zapasowej
				this.BW_Task.DoWork -= this.DecompressUpdate;
				this.BW_Task.DoWork += this.DecompressUpdate;

				this.BW_Task.ProgressChanged -= this.DecompressUpdateProgress;
				this.BW_Task.ProgressChanged += this.DecompressUpdateProgress;

				this.BW_Task.RunWorkerAsync( folder + name );
			}
		}

		/// 
		/// Dekompresja plików kopii zapasowej (działanie w wątku).
		/// ------------------------------------------------------------------------------------------------------------
		private void DecompressUpdate( object sender, DoWorkEventArgs ev )
		{
			Program.LogMessage( "Wypakowywanie kopii zapasowej: '" + (string)ev.Argument + "'..." );
			try
			{
				// wypakuj dane do folderu "./temp/"
				CBackupData backup = new CBackupData( this.BW_Task );
				backup.DecompressUpdate( (string)ev.Argument, "./temp/" );
			}
			catch( Exception ex )
			{
				// błąd...
				Program.LogError( ex.Message, "Przywracanie danych", false );
				this.BW_Task.ReportProgress( 100, 2 );
			}
		}

		/// 
		/// Stan dekompresji pliku kopii zapasowej.
		/// ------------------------------------------------------------------------------------------------------------
		private void DecompressUpdateProgress( object sender, ProgressChangedEventArgs ev )
		{
			this.PB_Decompress.Value = ev.ProgressPercentage;

			// błąd dekompresji
			if( (int)ev.UserState == 2 )
			{
				Cursor.Current = Cursors.Default;
				this._close_on = true;
				Application.UseWaitCursor = false;

				Program.LogMessage( "Wypakowywanie aktualizacji zakończone błędem." );

				this.tcBackup_Click( null, null );
			}
			// koniec dekompresji, skopiuj pliki
			else if( (int)ev.UserState == 1 )
			{
				try
					{ Program.InstallUpdate( "./temp/" ); }
				catch( Exception ex )
					{ Program.LogError( ex.Message, "Przywracanie danych", false ); }


				// pobierz wersje aplikacji
				try
				{
					String version = AssemblyName.GetAssemblyName("CDesigner.exe").Version.ToString();
					this.L_Version.Text = "Zainstalowana wersja: " + version;
				}
				catch( FileNotFoundException ex )
				{
					Program.LogMessage( ex.Message );
					this.L_Version.Text = "Zainstalowana wersja: brak.";

					return;
				}

				Cursor.Current = Cursors.Default;
				this._close_on = true;
				Application.UseWaitCursor = false;

				// aktywuj lub dezaktywuj przyciski
				this.tcBackup_Click( null, null );

				// spytaj, czy uruchomić program
				DialogResult result = MessageBox.Show
				(
					this,
					"Zakończono dekompresje i kopiowanie plików kopii zapasowej.\n" +
					"Czy chcesz uruchomić program CDesigner?",
					"Instalacja zakończona",
					MessageBoxButtons.YesNo,
					MessageBoxIcon.Information,
					MessageBoxDefaultButton.Button1
				);

				if( result == DialogResult.Yes )
				{
					Program.LogMessage( "Uruchamianie nowego procesu dla programu CDesigner." );

					// uruchomienie programu CDesigner
					Process process = new Process();
					process.StartInfo.FileName = "CDesigner.exe";
					process.StartInfo.Arguments = "-w";
					process.Start();

					// zamykanie aplikacji
					this.Close();
				}
			}
		}

		/// 
		/// Funkcja wywoływana podczas zamykania okna.
		/// ------------------------------------------------------------------------------------------------------------
		private void Main_FormClosing( object sender, FormClosingEventArgs ev )
		{
			if( !this._close_on )
			{
				Program.LogMessage( "Próba zamknięcia aplikacji w trakcie przywracania plików kopii zapasowej." );
				MessageBox.Show
				(
					this,
					"Nie możesz zamknąć aplikacji w trakcie przywracania plików kopii zapasowej!",
					"Zamykanie aplikacji",
					MessageBoxButtons.OK,
					MessageBoxIcon.Warning
				);
				ev.Cancel = true;
			}
		}

        private void TLP_StatusBar_Paint( object sender, PaintEventArgs ev )
        {
			ev.Graphics.DrawLine
			(
				new Pen( SystemColors.ControlDark ),
				this.TLP_StatusBar.Bounds.X,
				0,
				this.TLP_StatusBar.Bounds.Right,
				0
			);
        }
	}
}
