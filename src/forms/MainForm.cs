/**
 * Plik do obsługi głównego interfejsu graficznego.
 * Zarządzanie kontrolkami i wzorami.
 *
 * Copyright ⓒ 2015. Wszystkie prawa zastrzeżone.
 *
 * Autor  - Kamil Biały
 * Wersja - 1.0
 * Zmiana - 2015-05-25
 */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using PdfSharp.Pdf;
using PdfSharp;
using PdfSharp.Drawing;
using System.Drawing.Printing;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Net;

namespace CDesigner
{
	public partial class MainForm : Form
	{
		private DatabaseReader _reader;

		// ------------------------------------------------------------- gmpUpdate_Click ------------------------------
		/// <summary>
		/// Funkcja wywoływana po naciśnięciu przycisku Aktualizuj program
		/// </summary>
		/// <param name="sender">Obiekt wysyłający zdarzenie...</param>
		/// <param name="ev">Argumenty zdarzenia...</param>
		private void gmpUpdate_Click( object sender, EventArgs ev )
		{
#		if DEBUG
			Console.WriteLine( "Kliknięto w przycisk Aktualizuj program..." );
#		endif

			// try...catch
			try
			{
				UpdateForm update = new UpdateForm();
				
				if( !update.UpdateAvaliable() )
				{
					MessageBox.Show
					(
						this,
						"Posiadasz już najnowszą wersję aplikacji.\nv" + InfoForm.VERSION + ".",
						"Aktualizacja programu",
						MessageBoxButtons.OK,
						MessageBoxIcon.Information
					);
					return;
				}
		
				if( update.ShowDialog(this) != DialogResult.OK )
					return;
			}
			catch( WebException ex )
			{
				if( ex.Response == null )
				{
#				if DEBUG
					Console.WriteLine( "BŁĄD: Nie można połączyć się z serwerem aktualizacji." );
#				endif
					MessageBox.Show
					(
						this, "Nie można połączyć się z serwerem aktualizacji.",
						"Aktualizacja programu", MessageBoxButtons.OK, MessageBoxIcon.Error
					);
				}
				else
				{
#				if DEBUG
					Console.WriteLine( "BŁĄD: " + ex.Message );
#				endif
					MessageBox.Show
					(
						this, ex.Message,
						"Aktualizacja programu", MessageBoxButtons.OK, MessageBoxIcon.Error
					);
				}
				return;
			}
		}






		private AlignedPictureBox mpbPreview = null;
		private FormWindowState   gLastState = FormWindowState.Normal;

		private int         gThisContainer  = 1;
		private bool        gEditChanged    = false;
		private bool		pLocked			= false;
		private Settings    gSettings       = null;
		
		private int         mSelectedID	    = -1;
		private bool        mSelectedError  = false;
		private bool        mSelectedData   = false;
		private string      mSelectedName   = null;

		private PageField   pCurrentField   = null;
		private string      pCurrentName    = null;
		private PatternData pPatternData    = null;
		private PageField   pMovingField    = null;
		private int         pDiffX          = 0;
		private int         pDiffY          = 0;
		private Panel		pCurrentPage	= null;
		private int			pPanelCounter	= 0;
		private int			pCurrentPanelID	= -1;
		private Size		pPageSize       = new Size();

		private PatternData dPatternData    = new PatternData();
		private DataContent dDataContent    = new DataContent();
		private bool        dSketchDrawed   = false;
		private Panel       dCurrentPage    = null;


/* ===============================================================================================================================
 * Główne funkcje klasy:
 * - Main					[Public]
 * - Main_Resize            [Form]
 * - ProcessCmdKey			[Form]
 * - RefreshProjectList		[Private]
 * - LockFieldLabels		[Private]
 * - UnlockFieldLabels		[Private]
 * - FillFieldLabels		[Private]
 * =============================================================================================================================== */

#region Główne funkcje klasy

		// ------------------------------------------------------------- Main -----------------------------------------
		
		public MainForm( )
		{
			// zdarzenia do rozwiązywania problemów z plikami dll
			AssemblyLoader.Register();

			Program.LogMessage( "Tworzenie okna głównego." );
			this.InitializeComponent();

			this.Icon = Program.GetIcon();

			// podgląd wzoru na stronie głównej
			this.mpbPreview = new AlignedPictureBox();
			
			this.mpbPreview.Align      = 1;
			this.mpbPreview.SizeMode   = PictureBoxSizeMode.AutoSize;
			this.mpbPreview.MouseDown += new MouseEventHandler( mpPreview_MouseDown );
			this.mpbPreview.Padding    = new Padding( 5 );

			this.mpPreview.Controls.Add( this.mpbPreview );

			// ustawienia
			this.gSettings = new Settings();

#		if DEBUG
			Console.WriteLine( "Załadowano kontrolki." );
#		endif

			// ostatnio otwierane wzory
			this.gmpRecent_RefreshList();

			// odświeżenie listy wzorów
			this.RefreshProjectList();
		}

		// ------------------------------------------------------------- Main_Resize ---------------------------------

		private void Main_Resize( object sender, EventArgs ev )
		{
			if( this.WindowState == this.gLastState )
				return;

			this.gLastState = this.WindowState;

			// wymuś odświeżenie i zmiane rozmiaru panelu po wróceniu do normalnego stanu
			// w przeciwnym wypadku rodzic będzie za wielki dla obrazu (dziwne zjawisko...)
			if( this.WindowState == FormWindowState.Normal )
			{
#			if DEBUG
				Console.WriteLine( "Przejście z trybu maksymalizacji - odświeżanie kontrolek." );
#			endif

				this.mpPreview.Width = this.mpPreview.Width - 1;
				this.ppPreview.Width = this.ppPreview.Width - 1;
				this.dpPreview.Width = this.dpPreview.Width - 1;
			}
		}

		// ------------------------------------------------------------- ProcessCmdKey --------------------------------
		
		protected override bool ProcessCmdKey( ref Message msg, Keys keydata )
		{
			// @TODO - możliwość wyboru trybu rotacyjnego - po przekroczeniu limitu wraca na początek...

			switch( keydata )
			{
			// przełącz stronę z wzorem do przodu
			case Keys.Control | Keys.Tab:
				if( this.gThisContainer == 1 )
				{
					if( this.mnPage.Value < this.mnPage.Maximum )
						this.mnPage.Value += 1;
				}
				else if( this.gThisContainer == 2 )
				{
					if( this.pnPage.Value < this.pnPage.Maximum )
						this.pnPage.Value += 1;
				}
				else if( this.gThisContainer == 3 )
				{
					if( this.dnPage.Value < this.dnPage.Maximum )
						this.dnPage.Value += 1;
				}
			break;

			// przełącz stronę z wzorem do tyłu
			case Keys.Shift | Keys.Tab:
				if( this.gThisContainer == 1 )
				{
					if( this.mnPage.Value > this.mnPage.Minimum )
						this.mnPage.Value -= 1;
				}
				else if( this.gThisContainer == 2 )
				{
					if( this.pnPage.Value > this.pnPage.Minimum )
						this.pnPage.Value -= 1;
				}
				else if( this.gThisContainer == 3 )
				{
					if( this.dnPage.Value > this.dnPage.Minimum )
						this.dnPage.Value -= 1;
				}
			break;

			// skrót do ustawień
			case Keys.Control | Keys.S:
				this.issGeneral_Click( null, null );
			break;
			default:
				return base.ProcessCmdKey( ref msg, keydata );
			}

			return true;
		}

		// ------------------------------------------------------------- RefreshProjectList ---------------------------
		
		private void RefreshProjectList( )
		{
#		if DEBUG
			Console.WriteLine( "Wczytywanie i uzupełnianie listy wzorów." );
#		endif

			// wyczyść wszystkie dane
			this.mtvPatterns.Nodes.Clear( );

			// utwórz folder gdy nie istnieje
			if( !Directory.Exists("patterns") )
				try { Directory.CreateDirectory( "patterns" ); }
				catch
				{
					// ten komunikat nigdy nie powinien się pojawić...
					MessageBox.Show
					(
						this,
						"Błąd tworzenia folderu głównego dla wzorów...\n" +
						"Sprawdź czy posiadasz odpowiednie uprawnienia do zapisywania danych w katalogu programu.",
						"Tworzenie folderu",
						MessageBoxButtons.OK,
						MessageBoxIcon.Error
					);
					return;
				}

			// pobierz i dodaj katalogi do listy
			string[] patterns = Directory.GetDirectories( "patterns" );

			foreach( string pattern in patterns )
			{
				TreeNode item = this.mtvPatterns.Nodes.Add( pattern.Replace("patterns\\", "") );

				// brak pliku konfiguracji
				if( !File.Exists(pattern + "/config.cfg") )
				{
#				if DEBUG
					Console.WriteLine( "Błąd: Wzór " + pattern + " nie zawiera pliku konfiguracyjnego." );
#				endif
					item.ForeColor = Color.OrangeRed;
					continue;
				}
			}
			
			this.mlStatus.Text = "Utworzono listę zapisanych wzorów.";
		}

		// ------------------------------------------------------------- LockFieldLabels ------------------------------
		
		private void LockFieldLabels( )
		{
			this.pbBorderColor.Enabled = false;
			this.pbFontName.Enabled    = false;
			this.pbFontColor.Enabled   = false;
			this.pbBackImage.Enabled   = false;
			this.pbBackColor.Enabled   = false;
			this.pnBorderSize.Enabled  = false;
			this.pnPositionX.Enabled   = false;
			this.pnPositionY.Enabled   = false;
			this.pnWidth.Enabled       = false;
			this.pnHeight.Enabled      = false;
			this.ptbName.Enabled       = false;
			this.pnPadding.Enabled     = false;
			this.pcbTextAlign.Enabled  = false;
			this.pcbPosAlign.Enabled   = false;

			this.pcxImageSet.Enabled   = false;
			this.pcbDrawColor.Enabled  = false;
			this.pcbDynImage.Enabled   = false;
			this.pcbDynText.Enabled    = false;
			this.pcbShowFrame.Enabled  = false;
			this.pcbStatImage.Enabled  = false;
			this.pcbStatText.Enabled   = false;
			
			this.pcbTextTransform.Enabled    = false;
			this.pcbDrawFrameOutside.Enabled = false;
			this.pcbUseImageMargin.Enabled   = false;


			// nie potrzeba ich blokować
			this.pcbpApplyMargin.Enabled = false;
			//this.pcbpDrawColor.Enabled   = false;
			//this.pcbpDrawImage.Enabled   = false;
			this.pcbpDrawOutside.Enabled = false;
			this.pcxpImageSet.Enabled    = false;

			this.pcbMarginLR.Enabled = false;
			this.pcbMarginTB.Enabled = false;
			this.pcbdAddMargin.Enabled = false;
		}

		// ------------------------------------------------------------- UnlockFieldLabels ----------------------------
		
		private void UnlockFieldLabels( )
		{
			this.pbBorderColor.Enabled = true;
			this.pbFontName.Enabled    = true;
			this.pbFontColor.Enabled   = true;
			//this.pbBackImage.Enabled   = true;
			this.pbBackColor.Enabled   = true;
			this.pnBorderSize.Enabled  = true;
			this.pnPositionX.Enabled   = true;
			this.pnPositionY.Enabled   = true;
			this.pnWidth.Enabled       = true;
			this.pnHeight.Enabled      = true;
			this.ptbName.Enabled       = true;
			this.pnPadding.Enabled     = true;
			this.pcbTextAlign.Enabled  = true;
			this.pcbPosAlign.Enabled   = true;

			this.pcbDrawColor.Enabled  = true;
			this.pcbDynImage.Enabled   = true;
			//this.pcbDynText.Enabled    = true;
			this.pcbShowFrame.Enabled  = true;
			//this.pcbStatImage.Enabled  = true;
			this.pcbStatText.Enabled   = true;

			this.pcbTextTransform.Enabled = true;
			//this.pcxImageSet.Enabled         = true;
			//this.pcbDrawFrameOutside.Enabled = true;
			//this.pcbUseImageMargin.Enabled   = true;

			//this.pcbpDrawColor.Enabled = true;
			//this.pcbpDrawImage.Enabled = true;

			//this.pcxpImageSet.Enabled  = true;
			//this.pcbpApplyMargin.Enabled = true;
			//this.pcbpDrawOutside.Enabled = true;

			this.pcbMarginLR.Enabled   = true;
			this.pcbMarginTB.Enabled   = true;
			this.pcbdAddMargin.Enabled = true;
		}

		// ------------------------------------------------------------- FillFieldLabels ------------------------------
		
		private void FillFieldLabels( )
		{
			this.pLocked = true;

			// zmień nazwę
			this.ptbName.Text = this.pCurrentField.OriginalText;

			PointF location = this.pCurrentField.GetPosByAlignPoint( (ContentAlignment)((FieldExtraData)this.pCurrentField.Tag).pos_align );

			// aktualizuj pola numeryczne
			this.pnPositionX.Value  = (decimal)location.X;
			this.pnPositionY.Value  = (decimal)location.Y;
			this.pnHeight.Value     = (decimal)this.pCurrentField.DPIBounds.Height;
			this.pnWidth.Value      = (decimal)this.pCurrentField.DPIBounds.Width;
			this.pnBorderSize.Value = (decimal)this.pCurrentField.DPIBorderSize;
			this.pnPadding.Value    = (decimal)this.pCurrentField.DPIPadding;

			string color_r, color_g, color_b;

			// aktualizuj kolory...
			color_r = this.pCurrentField.BorderColor.R.ToString("X2");
			color_g = this.pCurrentField.BorderColor.G.ToString("X2");
			color_b = this.pCurrentField.BorderColor.B.ToString("X2");
			this.ptbBorderColor.Text = "#" + color_r + color_b + color_g;

			color_r = this.pCurrentField.BackColor.R.ToString("X2");
			color_g = this.pCurrentField.BackColor.G.ToString("X2");
			color_b = this.pCurrentField.BackColor.B.ToString("X2");
			this.ptbBackColor.Text = "#" + color_r + color_g + color_b;

			color_r = this.pCurrentField.ForeColor.R.ToString("X2");
			color_g = this.pCurrentField.ForeColor.G.ToString("X2");
			color_b = this.pCurrentField.ForeColor.B.ToString("X2");
			this.ptbFontColor.Text = "#" + color_r + color_g + color_b;

			// czcionka
			Font   font = this.pCurrentField.Font;
			string dets = (font.Bold ? "B" : "") + (font.Italic ? "I" : "") + (font.Strikeout ? "S" : "") + (font.Underline ? "U" : "");

			this.ptbFontName.Text = font.Name + ", " + font.SizeInPoints + "pt " + dets;

			// transformacja tekstu
			this.pcbTextTransform.SelectedIndex = this.pCurrentField.TextTransform;

			// pozycja tekstu...
			switch( (int)this.pCurrentField.TextAlign )
			{
				case 0x001: this.pcbTextAlign.SelectedIndex = 0; break;
				case 0x002: this.pcbTextAlign.SelectedIndex = 1; break;
				case 0x004: this.pcbTextAlign.SelectedIndex = 2; break;
				case 0x010: this.pcbTextAlign.SelectedIndex = 3; break;
				case 0x020: this.pcbTextAlign.SelectedIndex = 4; break;
				case 0x040: this.pcbTextAlign.SelectedIndex = 5; break;
				case 0x100: this.pcbTextAlign.SelectedIndex = 6; break;
				case 0x200: this.pcbTextAlign.SelectedIndex = 7; break;
				case 0x400: this.pcbTextAlign.SelectedIndex = 8; break;
			}

			// pozycja położenia...
			switch( ((FieldExtraData)this.pCurrentField.Tag).pos_align )
			{
				case 0x001: this.pcbPosAlign.SelectedIndex = 0; break;
				case 0x004: this.pcbPosAlign.SelectedIndex = 1; break;
				case 0x100: this.pcbPosAlign.SelectedIndex = 2; break;
				case 0x400: this.pcbPosAlign.SelectedIndex = 4; break;
				default:    this.pcbPosAlign.SelectedIndex = 0; break;
			}

			FieldExtraData tag = (FieldExtraData)this.pCurrentField.Tag;

			// zaznacz lub odznacz elementy
			this.pcbDynImage.Checked  = tag.image_from_db;
			this.pcbDrawColor.Checked = tag.print_color;
			this.pcbStatImage.Checked = tag.print_image;
			this.pcbDynText.Checked   = tag.text_from_db;
			this.pcbStatText.Checked  = tag.print_text;
			this.pcbShowFrame.Checked = tag.print_border;

			// nazwa / ścieżka obrazu
			this.ptbBackImage.Text = this.pCurrentField.BackImagePath != null ? this.pCurrentField.BackImagePath : "";

			PointF margin = this.pCurrentField.TextMargin;

			this.pcbMarginLR.Value     = (decimal)margin.X;
			this.pcbMarginTB.Value     = (decimal)margin.Y;
			this.pcbdAddMargin.Checked = this.pCurrentField.ApplyTextMargin;

			this.pLocked = false;
		}

#endregion

/* ===============================================================================================================================
 * Menu okna:
 * - gmpNew_Click		[Menu/Button/Button]
 * - gmpClose_Click		[Menu/Button/Button]
 * - isHome_Click		[Menu/Button]
 * - isPattern_Click	[Menu/Button]
 * - isData_Click		[Menu/Button]
 * =============================================================================================================================== */

#region Menu okna

		// ------------------------------------------------------------- gmpNew_Click ---------------------------------
		
		private void gmpNew_Click( object sender, EventArgs ev )
		{
#		if DEBUG
			Console.WriteLine( "Otwieranie okna kreatora nowego wzoru." );
#		endif
			NewPattern window = new NewPattern();

			// nowy wzór
			if( window.ShowDialog(this) != DialogResult.OK )
				return;

			// odśwież listę
			this.RefreshProjectList();
			string pattern_name = window.PatternName;

			// zaznacz nowy wzór
			for( int x = 0; x < this.mtvPatterns.Nodes.Count; ++x )
				if( this.mtvPatterns.Nodes[x].Text == pattern_name )
				{
					this.mtvPatterns.SelectedNode = this.mtvPatterns.Nodes[x];
					break;
				}
			
			// przejdź do edycji wzoru
			this.ictEdit_Click( null, null );
		}

		// ------------------------------------------------------------- gmpClose_Click -------------------------------
		
		private void gmpClose_Click( object sender, EventArgs ev )
		{
			GC.Collect();

			// zakończ działanie programu
			Application.Exit();
		}

		// ------------------------------------------------------------- isHome_Click ---------------------------------
		
		private void gsMain_Click( object sender, EventArgs ev )
		{
#		if DEBUG
			Console.WriteLine( "Otwieranie strony głównej." );
#		endif

			this.tlMain.Show();
			this.gsHome.Enabled = false;

			// ukryj pozostałe panele
			if( this.gThisContainer == 2 )
			{
				this.tPattern.Hide();
				this.gsPattern.Enabled = true;
			}
			else if( this.gThisContainer == 3 )
			{
				this.tDataTable.Hide();
				this.gsData.Enabled = true;
			}

			// automatycznie odśwież podgląd, gdy jego zawartość uległa zmianie
			if( (this.pCurrentName == this.mSelectedName) && this.gEditChanged )
				if( this.mtvPatterns.SelectedNode != null )
				{
					this.mSelectedID = this.mtvPatterns.SelectedNode.Index;
					this.mtvPatterns_AfterSelect( null, null );
				}

			this.gEditChanged = false;
			this.gThisContainer = 1;
		}

		// ------------------------------------------------------------- isPattern_Click ------------------------------
		
		private void gsPattern_Click( object sender, EventArgs ev )
		{
#		if DEBUG
			Console.WriteLine( "Otwieranie edytora wzorów." );
#		endif

			this.tPattern.Show();
			this.gsPattern.Enabled = false;

			// ukryj pozostałe panele
			if( this.gThisContainer == 1 )
			{
				this.tlMain.Hide();
				this.gsHome.Enabled = true;
			}
			else if( this.gThisContainer == 3 )
			{
				this.tDataTable.Hide();
				this.gsData.Enabled = true;
			}

			this.gThisContainer = 2;
		}

		// ------------------------------------------------------------- isData_Click ---------------------------------
		
		private void gsData_Click( object sender, EventArgs ev )
		{
#		if DEBUG
			Console.WriteLine( "Otwieranie przeglądarki danych." );
#		endif

			this.tDataTable.Show();
			this.gsData.Enabled = false;

			// @TODO - automatyczne odświeżenie w razie zmiany

			// ukryj pozostałe panele
			if( this.gThisContainer == 1 )
			{
				this.tlMain.Hide();
				this.gsHome.Enabled = true;
			}
			else if( this.gThisContainer == 2 )
			{
				this.tPattern.Hide();
				this.gsPattern.Enabled = true;
			}

			this.gThisContainer = 3;
		}

#endregion

/* ===============================================================================================================================
 * Strona główna - Wzór:
 * - mtvPatterns_MouseDown		[ListBox]
 * - mtvPatterns_AfterSelect	[ListBox]
 * - icPattern_Opening			[ContextMenu]
 * - ictEdit_Click				[ContextMenu/Button]
 * - ictDelete_Click			[ContextMenu/Button]
 * - ictLoadData_Click			[ContextMenu/Button]
 * =============================================================================================================================== */

#region Strona główna - Wzór

		// ------------------------------------------------------------- mlbPatterns_MouseDown ------------------------
		
		private void mtvPatterns_MouseDown( object sender, MouseEventArgs ev )
		{
			if( ev.Button == MouseButtons.Middle )
				return;

			// zaznacz przy wciśnięciu prawego lub lewego przycisku myszy
			this.mtvPatterns.SelectedNode = this.mtvPatterns.GetNodeAt( ev.X, ev.Y );

			// resetuj zaznaczony indeks
			if( this.mtvPatterns.SelectedNode == null )
				this.mSelectedID = -1;
		}

		// ------------------------------------------------------------- mlbPatterns_AfterSelect ----------------------
		
		private void mtvPatterns_AfterSelect( object sender, TreeViewEventArgs ev )
		{
			// indeks poza zakresem
			if( this.mtvPatterns.SelectedNode == null )
			{
				this.mnPage.Enabled   = false;
				this.mbDelete.Enabled = false;

				return;
			}

			// indeks się nie zmienił...
			if( this.mSelectedID == this.mtvPatterns.SelectedNode.Index && !this.gEditChanged )
				return;

			// odśwież status
			this.mlStatus.Text = "Proszę czekać... wczytywanie podglądu wzoru...";
			this.mlStatus.Refresh();

			// zmień kursor
			this.Cursor  = Cursors.WaitCursor;
			this.pLocked = true;

			this.mSelectedID    = this.mtvPatterns.SelectedNode.Index;
			this.mSelectedError = false;
			this.mSelectedData  = false;
			
			// włącz możliwość usuwania
			this.mbDelete.Enabled = true;

			// nazwa zaznaczonego wzoru
			string pattern = this.mSelectedName = this.mtvPatterns.SelectedNode.Text;
			Image  helper  = null,
				   trash   = null;

			// przechwyć wyjątek
			try
			{
				// sprawdź czy wzór posiada plik konfiguracyjny
				if( !File.Exists("patterns/" + pattern + "/config.cfg") )
				{
					this.mSelectedError = true;

					// obrazy
					trash  = this.mpbPreview.Image;
					helper = File.Exists("noimage.png") ? Image.FromFile("noimage.png") : null;

					// informacja
					MessageBox.Show
					(
						this,
						"Wybrany wzór jest uszkodzony i nie posiada pliku konfiguracyjnego.\n" +
						"W tym momencie możesz go tylko usunąć z listy.",
						"Otwieranie wzoru",
						MessageBoxButtons.OK,
						MessageBoxIcon.Error
					);
					this.mlStatus.Text  = "Wybrany wzór jest uszkodzony!";
					this.mnPage.Enabled = false;

					// przejdź na koniec
					goto CD_mtvPatterns_AfterSelect;
				}

				// wczytaj nagłówek
				PatternData data   = PatternEditor.ReadPattern( pattern, true );
				int         format = -1;
			
				// treść dynamiczna
				this.mSelectedData = data.dynamic;

				// wykryj format wzoru
				format = NewPattern.DetectFormat( data.size.Width, data.size.Height );

				// ustaw wartości pola numerycznego
				this.mnPage.Maximum = data.pages;
				this.mnPage.Value = 1;

				// podgląd strony
				if( File.Exists("patterns/" + pattern + "/preview0.jpg") )
				{
					// wczytaj podgląd strony
					trash = this.mpbPreview.Image;

					// nie blokuj pliku... (dziadostwo blokuje plik)
					using( Image image = Image.FromFile("patterns/" + pattern + "/preview0.jpg") )
					{
						// skopiuj obraz do pamięci
						helper = new Bitmap( image.Width, image.Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb );
						using( var canavas = Graphics.FromImage(helper) )
							canavas.DrawImageUnscaled( image, 0, 0 );

						image.Dispose();
					}
					this.mnPage.Enabled = true;

					// aktualizuj status
					if( format == -1 )
						this.mlStatus.Text = "Wzór " + pattern + ". Format własny: " + data.size.Width + " x " + data.size.Height + " mm. Ilość stron: " + data.pages + ".";
					else
					{
						string fname = NewPattern.FormatNames[format];
						this.mlStatus.Text = "Wzór " + pattern + ". Format " + fname + ": " + data.size.Width + " x " + data.size.Height + " mm. Ilość stron: " + data.pages + ".";
					}

					// przejdź na koniec
					goto CD_mtvPatterns_AfterSelect;
				}

				this.mSelectedData = false;

				// brak obrazka
				helper = File.Exists( "noimage.png" ) ? Image.FromFile( "noimage.png" ) : null;
				trash  = this.mpbPreview.Image;

				this.mnPage.Enabled = false;
			
				// aktualizuj status
				if( format == -1 )
					this.mlStatus.Text = "Wzór " + pattern + ". Format własny: " + data.size.Width + " x " + data.size.Height + " mm. Brak podglądu.";
				else
				{
					string fname = NewPattern.FormatNames[format];
					this.mlStatus.Text = "Wzór " + pattern + ". Format " + fname + ": " + data.size.Width + " x " + data.size.Height + " mm. Brak podglądu.";
				}
			}
			catch( UnauthorizedAccessException )
			{
				// ten komunikat nigdy nie powinien się pojawić...
				MessageBox.Show
				(
					this,
					"Nie posiadasz odpowiednich uprawnień, aby odczytywać dane z katalogu programu!\n",
					"Otwieranie pliku",
					MessageBoxButtons.OK,
					MessageBoxIcon.Error
				);
				return;
			}

CD_mtvPatterns_AfterSelect:

			// zmień obrazek
			this.mpbPreview.Hide();
			this.mpbPreview.Image = helper;
			this.mpbPreview.CheckLocation();
			this.mpbPreview.Show();
			
			// pozbieraj śmieci po poprzednim obrazku
			if( trash != null )
			{
				trash.Dispose();
				GC.Collect();
			}

			// odblokuj i zmień kursor
			this.pLocked = false;
			this.Cursor  = null;
		}

		// ------------------------------------------------------------- icPattern_Opening ----------------------------
		
		private void icPattern_Opening( object sender, CancelEventArgs ev )
		{
			// odblokuj wszystkie przyciski w menu
			this.ictNew.Enabled      = true;
			this.ictEdit.Enabled     = true;
			this.ictLoadData.Enabled = true;
			//@TODO
			//this.ictImport.Enabled   = true;
			//this.ictExport.Enabled   = true;
			this.ictDelete.Enabled   = true;

			// zablokuj gdy istnieje taka potrzeba
			if( this.mSelectedID == -1 )
			{
				this.ictEdit.Enabled     = false;
				this.ictLoadData.Enabled = false;
				this.ictExport.Enabled   = false;
				this.ictDelete.Enabled   = false;
			}
			else if( this.mSelectedError )
			{
				this.ictEdit.Enabled     = false;
				this.ictLoadData.Enabled = false;
				this.ictExport.Enabled   = false;
			}
			// zmień nazwę przycisku do wczytywania danych
			else if( this.mSelectedData )
				this.ictLoadData.Text = "Wczytaj dane...";
			else
				this.ictLoadData.Text = "Podgląd";
		}

		// ------------------------------------------------------------- ictEdit_Click --------------------------------
		
		private void ictEdit_Click( object sender, EventArgs ev )
		{
			// indeks poza zakresem
			if( this.mSelectedID == -1 )
				return;

			// nie można edytować projektu...
			if( this.mSelectedError )
			{
				this.mlStatus.Text = "Nie można edytować uszkodzonego wzoru!";
				return;
			}

			// przełącz gdy wzór jest już załadowany
			if( this.pCurrentName == this.mSelectedName )
			{
				this.gsPattern_Click( null, null );
				this.plStatus.Text = "Wzór \"" + this.pCurrentName + "\" jest gotowy do edycji.";
				return;
			}

			this.pLocked = true;
			this.Cursor  = Cursors.WaitCursor;
			this.mlStatus.Text = "Trwa przygotowywanie wzoru do edycji...";

			// przejście do edycji wzoru
			string pattern = this.mSelectedName;

			// dodaj do listy ostatnio otwieranych
			this.gSettings.AddToLastPatterns( pattern );
			this.gmpRecent_RefreshList();

			// zapisz indeks i nazwę aktualnego wzoru
			this.pCurrentName = pattern;

			// wczytaj wzór
			this.pcbScale.SelectedIndex = 2;
			PatternData pattern_data = PatternEditor.ReadPattern( pattern );
			PatternEditor.DrawPreview( pattern_data, this.ppPreview, 1.0 );
			this.pPageSize = pattern_data.size;

			// ilość stron
			this.pPanelCounter  = pattern_data.pages - 1;
			this.pnPage.Maximum = pattern_data.pages;
			this.pnPage.Value   = 1;

			// aktualna strona
			this.pCurrentPage    = (Panel)this.ppPreview.Controls[0];
			this.pCurrentPanelID = 0;
			this.pPatternData    = pattern_data;

			AlignedPage page;
			PageField   field;

			// dodaj akcje do stron i pól
			for( int x = 0; x < this.ppPreview.Controls.Count; ++x )
			{
				page = (AlignedPage)this.ppPreview.Controls[x];
				page.ContextMenuStrip = this.icPage;
				page.MouseDown += new MouseEventHandler( this.ppPanelContainer_MouseDown );

				for( int y = 0; y < page.Controls.Count; ++y )
				{
					field = (PageField)page.Controls[y];
					field.ContextMenuStrip = this.icLabel;
					field.MouseDown += new MouseEventHandler( this.plLabel_MouseDown );
					field.MouseUp   += new MouseEventHandler( this.plLabel_MouseUp );
					field.MouseMove += new MouseEventHandler( this.plLabel_MouseMove );
				}
			}

			// zablokuj pola
			this.LockFieldLabels();

			// rozmiar stron
			this.ptbpWidth.Text  = this.pPageSize.Width + " mm";
			this.ptbpHeight.Text = this.pPageSize.Height + " mm";

			// ustaw kontroki
			PageExtraData ptag = (PageExtraData)this.pCurrentPage.Tag;

			this.pcbpDrawColor.Checked = ptag.print_color;
			this.pcbpDrawImage.Checked = ptag.print_image;

			// kolor strony
			string color_r, color_g, color_b;
			
			color_r = this.pCurrentPage.BackColor.R.ToString("X2");
			color_g = this.pCurrentPage.BackColor.G.ToString("X2");
			color_b = this.pCurrentPage.BackColor.B.ToString("X2");
			this.ptbPageColor.Text = "#" + color_r + color_b + color_g;
			
			// przełącz na panel wzorów
			this.gsPattern_Click( null, null );
			this.Cursor = null;
			this.plStatus.Text = "Wzór \"" + pattern + "\" jest gotowy do edycji.";

			// odblokuj panel i pola
			this.pLocked = false;
		}

		// ------------------------------------------------------------- ictDelete_Click ------------------------------
		
		private void ictDelete_Click( object sender, EventArgs ev )
		{
			// indeks poza zakresem
			if( this.mSelectedID == -1 )
				return;

			// nazwa wzoru do usunięcia
			string pattern = this.mSelectedName;

			// upewnij się czy użytkownik na pewno chce go usunąć
			DialogResult result = MessageBox.Show
			(
				this,
				"Czy na pewno chcesz usunąć wzór o nazwie: \"" + pattern + "\"?\n",
				"Usuwanie wzoru...",
				MessageBoxButtons.YesNo,
				MessageBoxIcon.Question,
				MessageBoxDefaultButton.Button2 // aktywny przycisk "Nie"
			);
			if( result == DialogResult.No )
				return;

			this.Cursor = Cursors.WaitCursor;
			this.mlStatus.Text = "Trwa usuwanie wybranego wzoru z dysku...";

			// sprawdź czy usuwany jest edytowany wzór
			if( this.pCurrentName == this.mSelectedName )
			{
				this.pCurrentName = null;

				// wyłącz możliwość edycji nieistniejącego już wzoru
				this.gsPattern.Enabled = false;
			}
			// brak wzoru, brak obrazka...
			this.mpbPreview.Image = null;
			this.mSelectedID      = -1;

			// usuń pliki wzoru i odśwież liste
			try { Directory.Delete( "patterns/" + pattern, true ); }
			catch
			{
				// ten komunikat nigdy nie powinien się pojawić...
				MessageBox.Show
				(
					this,
					"Nie można usunąć wybranego wzoru...\n" +
					"Sprawdź czy posiadasz odpowiednie uprawnienia do modyfikacji danych w katalogu programu.",
					"Tworzenie folderu",
					MessageBoxButtons.OK,
					MessageBoxIcon.Error
				);
				return;
			}

			// usuń z listy ostatnio otwieranych (jeżeli się tam znajduje)
			this.gSettings.RemoveFromLastPatterns( pattern );
			this.gmpRecent_RefreshList();

			// odśwież listę wzorów
			this.RefreshProjectList();

			this.mlStatus.Text = "Wybrany wzór został usunięty.";
			this.Cursor = null;
		}

		// ------------------------------------------------------------- ictLoadData_Click ----------------------------
		
		private void ictLoadData_Click( object sender, EventArgs ev )
		{
			// wybór pliku
			if( this.gsOpenFile.ShowDialog(this) != DialogResult.OK )
				return;

			// odczytaj dane
			this.dPatternData = PatternEditor.ReadPattern( this.mSelectedName );
			DataReader reader = new DataReader( this.dPatternData, this.gsOpenFile.FileName );

			if( reader.ShowDialog(this) != DialogResult.OK )
				return;
			 
			// załaduj cały plik
			reader.ReloadData();

			this.dSketchDrawed = false;
			this.dpPreview.Controls.Clear();

			this.dtvData.Nodes.Clear();
			this.dDataContent = reader.DataContent;

			List<int> checked_cols = reader.CheckedCols;
			string    checked_fmt  = reader.CheckedFormat;
			string    checked_help = "";

			// wybierz pierwszą kolumnę w przypadku braku zaznaczenia
			if( checked_cols.Count == 0 )
			{
				checked_cols.Add(0);
				checked_fmt = "#1";
			}

			// uzupełnij wiersze
			for( int x = 0; x < this.dDataContent.rows; ++x )
			{
				checked_help = checked_fmt;

				// wyświetl wybrane kolumny
				for( int y = 0; y < checked_cols.Count; ++y )
				{
					string row = this.dDataContent.row[x,checked_cols[y]];

					if( row == " " || row == "" || row == null )
						checked_help = checked_help.Replace( "#" + (y+1), "" );
					else
						checked_help = checked_help.Replace( "#" + (y+1), this.dDataContent.row[x,checked_cols[y]] );
				}

				this.dtvData.Nodes.Add( checked_help );
			}
			
			// ustaw indeks początkowy dla pola
			this.pLocked = true;
			this.dcbZoom.SelectedIndex = 2;
			this.dnPage.Maximum = this.dPatternData.pages;
			this.pLocked = false;

			// przejdź na strone z danymi
			this.gsData_Click( null, null );
		}

#endregion

/* ===============================================================================================================================
 * Strona główna - Inne:
 * - mnPage_ValueChanged	[NumericUpDown]
 * - mpPreview_Resize		[Panel]
 * - mpPreview_MouseDown	[Panel]
 * - scHome_SplitterMoved   [SplitContainer]
 * =============================================================================================================================== */

#region Strona główna - Inne

		// ------------------------------------------------------------- mnPage_ValueChanged -------------------------
		
		private void mnPage_ValueChanged( object sender, EventArgs ev )
		{
#		if	DEBUG
			Console.WriteLine( "Zmiana strony wzoru na stronie głównej." );
#		endif

			// zmień status
			this.mlStatus.Text = "Wczytywanie podglądu strony wzoru...";
			this.mlStatus.Refresh();
			this.Cursor = Cursors.WaitCursor;

			// przechwyć wyjątek - brak uprawnień do otwierania folderu?
			try
			{
				// otwórz podgląd wybranej strony wzoru
				if( File.Exists("patterns/" + this.mSelectedName + "/preview" + (this.mnPage.Value - 1) + ".jpg") )
				{
					using( Image image = Image.FromFile("patterns/" + this.mSelectedName + "/preview" + (this.mnPage.Value - 1) + ".jpg") )
					{
						if( this.mpbPreview.Image != null )
							this.mpbPreview.Image.Dispose();

						// skopiuj obraz do pamięci
						Image new_image = new Bitmap( image.Width, image.Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb );
						using( var canavas = Graphics.FromImage(new_image) )
							canavas.DrawImageUnscaled( image, 0, 0 );
						this.mpbPreview.Image = new_image;

						image.Dispose();
					}
					// aktualizuj status
					this.mlStatus.Text = "Wczytano podgląd strony nr " + this.mnPage.Value + ".";
				}
				else
				{
					// @TODO - dodać jakiś inny obrazek :D
					this.mpbPreview.Image = null;
					this.mlStatus.Text = "Podgląd tej strony nie istnieje.";
				}
			}
			catch( FileNotFoundException ex )
			{
				// ten komunikat nigdy nie powinien się pojawić...
				MessageBox.Show
				(
					this,
					"Plik o nazwie \"" + ex.FileName + "\" nie istnieje!\n" +
					"Sprawdź czy twój program zawiera wszystkie potrzebne pliki.",
					"Otwieranie pliku",
					MessageBoxButtons.OK,
					MessageBoxIcon.Error
				);
				return;
			}
			catch( UnauthorizedAccessException )
			{
				// ten komunikat nigdy nie powinien się pojawić...
				MessageBox.Show
				(
					this,
					"Nie posiadasz odpowiednich uprawnień, aby odczytywać dane z katalogu programu!\n",
					"Otwieranie pliku",
					MessageBoxButtons.OK,
					MessageBoxIcon.Error
				);
				return;
			}

			this.Cursor = null;
		}

		// ------------------------------------------------------------- mpPreview_Resize ----------------------------
		
		private void mpPreview_Resize( object sender, EventArgs ev )
		{
			if( this.pLocked )
				return;

			this.mpbPreview.CheckLocation();
		}

		// ------------------------------------------------------------- mpPreview_MouseDown -------------------------

		private void mpPreview_MouseDown( object sender, MouseEventArgs ev )
		{
			this.mpPreview.Focus();
		}

		// ------------------------------------------------------------- scHome_SplitterMoved ------------------------

		private void scHome_SplitterMoved( object sender, SplitterEventArgs ev )
		{
			ColumnStyle col = this.tlMainStatusBar.ColumnStyles[0];
			col.SizeType = SizeType.Absolute;
			col.Width = this.scMain.Panel1.Width + 7;
		}

#endregion

/* ===============================================================================================================================
 * Edycja wzoru - Inne:
 * - plLabel_MouseDown              [Label]
 * - plLabel_MouseUp                [Label]
 * - plLabel_MouseMove              [Label]
 * - scPattern_SplitterMoved        [SplitContainer]
 * - pcbScale_SelectedIndexChanged	[ComboBox]
 * - pcbScale_Leave					[ComboBox]
 * - pcbScale_KeyDown				[ComboBox]
 * - pnPage_ValueChanged			[NumericUpDown]
 * - ppPanelContainer_MouseDown		[Panel]
 * - pbSave_Click                   [Button]
 * - pbLoadData_Click               [Button]
 * - icpmDetails_Click              [ContextMenu/Button]
 * - icpmFieldDetails_Click         [ContextMenu/Button]
 * - icpmPageDetails_Click          [ContextMenu/Button]
 * =============================================================================================================================== */

#region Edycja wzoru - Inne

		// ------------------------------------------------------------- plLabel_MouseDown ----------------------------
		
		private void plLabel_MouseDown( object sender, MouseEventArgs ev )
		{
			if( ev.Button == MouseButtons.Left )
			{
				this.pMovingField = (PageField)sender;
				this.pDiffX       = ev.X;
				this.pDiffY       = ev.Y;
			}

			// aktualny obiekt
			this.pCurrentField = (PageField)sender;
			
			// wypełnij pola i odblokuj je
			this.FillFieldLabels();
			this.UnlockFieldLabels();
		}

		// ------------------------------------------------------------- plLabel_MouseUp ------------------------------
		
		private void plLabel_MouseUp( object sender, MouseEventArgs ev )
		{
			if( ev.Button != MouseButtons.Left )
				return;

			this.pMovingField = null;
		}

		// ------------------------------------------------------------- plLabel_MouseMove ----------------------------
		
		private void plLabel_MouseMove( object sender, MouseEventArgs ev )
		{
			if( this.pMovingField == null )
				return;

			if( ModifierKeys != Keys.Control )
				return;

			PageField        field = (PageField)sender;
			ContentAlignment align = (ContentAlignment)((FieldExtraData)field.Tag).pos_align;

			field.SetPxLocation( field.Location.X + (ev.X - this.pDiffX), field.Location.Y + (ev.Y - this.pDiffY), align );
			PointF location = field.GetPosByAlignPoint( align );

			this.pLocked = true;
			
			this.pnPositionX.Value = (decimal)location.X;
			this.pnPositionY.Value = (decimal)location.Y;

			this.pLocked = false;
		}

		// ------------------------------------------------------------- scPattern_SplitterMoved ----------------------
		
		private void scPattern_SplitterMoved( object sender, SplitterEventArgs ev )
		{
			ColumnStyle col = this.sbPattern.ColumnStyles[1];
			col.SizeType = SizeType.Absolute;
			col.Width = this.scPattern.Panel2.Width + 5;
		}

		// ------------------------------------------------------------- pcbScale_SelectedIndexChanged ----------------
		
		private void pcbScale_SelectedIndexChanged( object sender, EventArgs ev )
		{
			if( this.pLocked )
				return;

			// próbuj zamienić string na int
			int scale = 0;
			try { scale = Convert.ToInt32(this.pcbScale.Text); }
			catch
			{
				scale = 100;
				this.pcbScale.Text = "100";
			}

			// nie można zmniejszyć więcej niż 50%
			if( scale < 50 )
			{
				this.pcbScale.Text = "50";
				scale = 50;
			}

			// nie można powiększyć więcej niż 300%
			if( scale > 300 )
			{
				this.pcbScale.Text = "300";
				scale = 300;
			}
			
			// zmień skale wzoru
			PatternEditor.ChangeScale( this.pPatternData, this.ppPreview, (double)scale / 100.0 );
		}

		// ------------------------------------------------------------- pcbScale_Leave -------------------------------
		
		private void pcbScale_Leave( object sender, EventArgs ev )
		{
			this.pcbScale_SelectedIndexChanged( sender, ev );
		}

		// ------------------------------------------------------------- pcbScale_KeyDown -----------------------------
		
		private void pcbScale_KeyDown( object sender, KeyEventArgs ev )
		{
			if( ev.KeyCode == Keys.Enter )
				this.pcbScale_SelectedIndexChanged( sender, null );
		}

		// ------------------------------------------------------------- pnPage_ValueChanged --------------------------
		
		private void pnPage_ValueChanged( object sender, EventArgs ev )
		{
			((Panel)this.ppPreview.Controls[(int)pnPage.Value - 1]).Show();
			this.pCurrentPage.Hide();
			this.pCurrentPage = (Panel)this.ppPreview.Controls[(int)pnPage.Value - 1];

			PageExtraData ptag = (PageExtraData)this.pCurrentPage.Tag;

			this.pcbpDrawColor.Checked = ptag.print_color;
			this.pcbpDrawImage.Checked = ptag.print_image;

			// kolor strony
			string color_r, color_g, color_b;

			color_r = this.pCurrentPage.BackColor.R.ToString("X2");
			color_g = this.pCurrentPage.BackColor.G.ToString("X2");
			color_b = this.pCurrentPage.BackColor.B.ToString("X2");
			this.ptbPageColor.Text = "#" + color_r + color_b + color_g;
		}

		// ------------------------------------------------------------- ppPanelContainer_MouseDown -------------------
		
		private void ppPanelContainer_MouseDown( object sender, MouseEventArgs ev )
		{
			this.ppPreview.Focus();
		}

		// ------------------------------------------------------------- pbSave_Click ---------------------------------
		
		private void pbSave_Click( object sender, EventArgs ev )
		{
			this.Cursor = Cursors.WaitCursor;
			this.plStatus.Text = "Proszę czekać, trwa zapisywanie wzoru...";

			try
			{
				// usuń pliki konfiguracyjne
				Directory.EnumerateFiles( "patterns/" + this.pCurrentName + "/images" ).ToList().ForEach( File.Delete );
			
				// usuń obrazki
				int x = 0;
				while( true )
				{
					if( File.Exists("patterns/" + this.pCurrentName + "/preview" + x + ".jpg") )
						File.Delete( "patterns/" + this.pCurrentName + "/preview" + x + ".jpg" );
					else
						break;
					++x;
				}

				// wygeneruj podgląd i zapisz
				PatternEditor.GeneratePreview( this.pPatternData, this.ppPreview, 1.0 );
				PatternEditor.Save( this.pPatternData, this.ppPreview );
			}
			catch( UnauthorizedAccessException )
			{
				// ten komunikat nigdy nie powinien się pojawić...
				MessageBox.Show
				(
					this,
					"Nie posiadasz odpowiednich uprawnień aby modyfikować dane w katalogu programu!\n",
					"Modyfikacja pliku",
					MessageBoxButtons.OK,
					MessageBoxIcon.Error
				);
				return;
			}

			this.gEditChanged = true;

			this.plStatus.Text = "Zapis wzoru zakończony powodzeniem.";
			this.Cursor = null;
		}

		// ------------------------------------------------------------- pbLoadData_Click -----------------------------
		
		private void pbLoadData_Click( object sender, EventArgs ev )
		{
			// wybór pliku
			if( this.gsOpenFile.ShowDialog(this) != DialogResult.OK )
				return;

			// odczytaj dane
			this.dPatternData = PatternEditor.ReadPattern( this.pCurrentName );
			DataReader reader = new DataReader( this.dPatternData, this.gsOpenFile.FileName );

			if( reader.ShowDialog(this) != DialogResult.OK )
				return;
			 
			// załaduj cały plik
			reader.ReloadData();

			this.dSketchDrawed = false;
			this.dpPreview.Controls.Clear();

			this.dtvData.Nodes.Clear();
			this.dDataContent = reader.DataContent;

			List<int> checked_cols = reader.CheckedCols;
			string    checked_fmt  = reader.CheckedFormat;
			string    checked_help = "";

			// wybierz pierwszą kolumnę w przypadku braku zaznaczenia
			if( checked_cols.Count == 0 )
			{
				checked_cols.Add(0);
				checked_fmt = "#1";
			}

			// uzupełnij wiersze
			for( int x = 0; x < this.dDataContent.rows; ++x )
			{
				checked_help = checked_fmt;

				// wyświetl wybrane kolumny
				for( int y = 0; y < checked_cols.Count; ++y )
				{
					string row = this.dDataContent.row[x,checked_cols[y]];

					if( row == " " || row == "" || row == null )
						checked_help = checked_help.Replace( "#" + (y+1), "" );
					else
						checked_help = checked_help.Replace( "#" + (y+1), this.dDataContent.row[x,checked_cols[y]] );
				}

				this.dtvData.Nodes.Add( checked_help );
			}

			// ustaw indeks początkowy dla pola
			this.pLocked = true;
			this.dcbZoom.SelectedIndex = 2;
			this.pLocked = false;

			// przejdź na strone z danymi
			this.gsData_Click( null, null );
		}

		// ------------------------------------------------------------- icpmDetails_Click ----------------------------
		
		private void icpmDetails_Click( object sender, EventArgs ev )
		{
			this.icpmDetails.Enabled      = false;
			this.icpmFieldDetails.Enabled = true;
			this.icpmPageDetails.Enabled  = true;

			this.ptDetails.Show();
			this.ptFieldDetails.Hide();
			this.ptPageDetails.Hide();
		}

		// ------------------------------------------------------------- icpmFieldDetails_Click -----------------------
		
		private void icpmFieldDetails_Click( object sender, EventArgs ev )
		{
			this.icpmDetails.Enabled      = true;
			this.icpmFieldDetails.Enabled = false;
			this.icpmPageDetails.Enabled  = true;

			this.ptFieldDetails.Show();
			this.ptDetails.Hide();
			this.ptPageDetails.Hide();
		}

		// ------------------------------------------------------------- icpmPageDetails_Click ------------------------
		
		private void icpmPageDetails_Click( object sender, EventArgs ev )
		{
			this.icpmDetails.Enabled      = true;
			this.icpmFieldDetails.Enabled = true;
			this.icpmPageDetails.Enabled  = false;
			
			this.ptPageDetails.Show();
			this.ptDetails.Hide();
			this.ptFieldDetails.Hide();
		}

#endregion

/* ===============================================================================================================================
 * Edycja wzoru - Menu "POLE":
 * - ptbName_TextChanged		             [TextBox]
 * - pnPositionX_ValueChanged                [NumericUpDown]
 * - pnPositionY_ValueChanged                [NumericUpDown]
 * - pnWidth_ValueChanged                    [NumericUpDown]
 * - pnHeight_ValueChanged                   [NumericUpDown]
 * - pbBorderColor_Click                     [Button]
 * - pbBackColor_Click                       [Button]
 * - pbBackImage_Click                       [Button]
 * - pbFontName_Click                        [Button]
 * - pbFontColor_Click                       [Button]
 * - pcbTextAlign_SelectedIndexChanged       [ComboBox]
 * - pnPadding_ValueChanged                  [NumericUpDown]
 * - pcbTextTransform_SelectedIndexChanged   [ComboBox]
 * - pnBorderSize_ValueChanged               [NumericUpDown]
 * =============================================================================================================================== */

#region Edycja wzoru - Menu "POLE"

		// ------------------------------------------------------------- ptbName_TextChanged --------------------------
		
		private void ptbName_TextChanged( object sender, EventArgs ev )
		{
			if( this.pLocked )
				return;

			this.pCurrentField.Text = this.ptbName.Text;
		}

		// ------------------------------------------------------------- pnPositionX_ValueChanged ---------------------
		
		private void pnPositionX_ValueChanged( object sender, EventArgs ev )
		{
			if( this.pLocked )
				return;

			ContentAlignment align = (ContentAlignment)((FieldExtraData)this.pCurrentField.Tag).pos_align;
			this.pCurrentField.SetPosXByAlignPoint( (float)this.pnPositionX.Value, align );
		}

		// ------------------------------------------------------------- pnPositionY_ValueChanged ---------------------
		
		private void pnPositionY_ValueChanged( object sender, EventArgs ev )
		{
			if( this.pLocked )
				return;

			ContentAlignment align = (ContentAlignment)((FieldExtraData)this.pCurrentField.Tag).pos_align;
			this.pCurrentField.SetPosYByAlignPoint( (float)this.pnPositionY.Value, align );
		}

		// ------------------------------------------------------------- pnWidth_ValueChanged -------------------------
		
		private void pnWidth_ValueChanged( object sender, EventArgs ev )
		{
			if( this.pLocked )
				return;

			RectangleF new_width = new RectangleF( -1, -1, (float)this.pnWidth.Value, -1 );
			this.pCurrentField.DPIBounds = new_width;

			this.pLocked = true;
			
			ContentAlignment align = (ContentAlignment)((FieldExtraData)this.pCurrentField.Tag).pos_align;
			PointF pos = this.pCurrentField.GetPosByAlignPoint( align );
			this.pnPositionX.Value = (decimal)pos.X;
			this.pnPositionY.Value = (decimal)pos.Y;

			this.pLocked = false;
		}

		// ------------------------------------------------------------- pnHeight_ValueChanged ------------------------
		
		private void pnHeight_ValueChanged( object sender, EventArgs ev )
		{
			if( this.pLocked )
				return;

			RectangleF new_height = new RectangleF( -1, -1, -1, (float)this.pnHeight.Value );
			this.pCurrentField.DPIBounds = new_height;

			this.pLocked = true;
			
			ContentAlignment align = (ContentAlignment)((FieldExtraData)this.pCurrentField.Tag).pos_align;
			PointF pos = this.pCurrentField.GetPosByAlignPoint( align );
			this.pnPositionX.Value = (decimal)pos.X;
			this.pnPositionY.Value = (decimal)pos.Y;

			this.pLocked = false;
		}

		// ------------------------------------------------------------- pbBorderColor_Click --------------------------
		
		private void pbBorderColor_Click( object sender, EventArgs ev )
		{
			if( this.pLocked || this.gsColor.ShowDialog(this) != DialogResult.OK )
				return;

			Color color = this.gsColor.Color;

			// zmień kolor ramki
			this.pCurrentField.BorderColor = color;
			this.ptbBorderColor.Text = "#" + color.R.ToString("X2") + color.G.ToString("X2") + color.B.ToString("X2");

			// odśwież kontrolke
			this.pCurrentField.Refresh();
		}

		// ------------------------------------------------------------- pbBackColor_Click ----------------------------
		
		private void pbBackColor_Click( object sender, EventArgs ev )
		{
			if( this.pLocked || this.gsColor.ShowDialog(this) != DialogResult.OK )
				return;

			Color color = this.gsColor.Color;

			// zmień kolor tła
			this.pCurrentField.BackColor = color;
			this.ptbBackColor.Text = "#" + color.R.ToString("X2") + color.G.ToString("X2") + color.B.ToString("X2");
			
			// odśwież kontrolke
			this.pCurrentField.Refresh();
		}

		// ------------------------------------------------------------- pbBackImage_Click ----------------------------
		
		private void pbBackImage_Click( object sender, EventArgs ev )
		{
			// okno wyboru pliku
			if( this.pLocked || this.gsImage.ShowDialog(this) != DialogResult.OK )
				return;

			if( this.pCurrentField.BackImage != null )
				this.pCurrentField.BackImage.Dispose();

			// zmień obraz pola
			this.pCurrentField.BackImage = Image.FromFile( this.gsImage.FileName );
			this.pCurrentField.BackColor = Color.Transparent;

			// ustaw ścieżkę do obrazu w polu tekstowym
			this.ptbBackImage.Text = this.gsImage.FileName;

			// odśwież kontrolke
			this.pCurrentField.Refresh();
		}

		// ------------------------------------------------------------- pbFontName_Click -----------------------------
		
		private void pbFontName_Click( object sender, EventArgs ev )
		{
			// @TODO - własne okienko do wybierania czcionek - to ma problem z czcionkami OpenType - otwiera tylko TrueType
			if( this.pLocked || this.gsFont.ShowDialog(this) != DialogResult.OK )
				return;

			this.pCurrentField.Font = this.gsFont.Font;
			this.pCurrentField.DPIFontSize = this.gsFont.Font.SizeInPoints;

			Font   font = this.pCurrentField.Font;
			string dets = (font.Bold ? "B" : "") + (font.Italic ? "I" : "") + (font.Strikeout ? "S" : "") + (font.Underline ? "U" : "");

			// odśwież dane na temat czcionki...
			this.ptbFontName.Text = font.Name + ", " + font.SizeInPoints + "pt " + dets;

			// odśwież kontrolke
			this.pCurrentField.Refresh();
		}

		// ------------------------------------------------------------- pbFontColor_Click ----------------------------
		
		private void pbFontColor_Click( object sender, EventArgs ev )
		{
			if( this.pLocked || this.gsColor.ShowDialog(this) != DialogResult.OK )
				return;

			Color color = this.gsColor.Color;

			// zmień kolor czcionki
			this.pCurrentField.ForeColor = color;
			this.ptbFontColor.Text = "#" + color.R.ToString("X2") + color.G.ToString("X2") + color.B.ToString("X2");

			// odśwież kontrolke
			this.pCurrentField.Refresh();
		}

		// ------------------------------------------------------------- pcbTextAlign_SelectedIndexChanged ------------
		
		private void pcbTextAlign_SelectedIndexChanged( object sender, EventArgs ev )
		{
			if( this.pLocked )
				return;

			this.pCurrentField.SetTextAlignment( this.pcbTextAlign.SelectedIndex );
		}

		// ------------------------------------------------------------- pnPadding_ValueChanged -----------------------
		
		private void pnPadding_ValueChanged( object sender, EventArgs ev )
		{
			if( this.pLocked )
				return;
			
			this.pCurrentField.DPIPadding = (float)this.pnPadding.Value;
		}

		// ------------------------------------------------------------- pcbTextTransform_SelectedIndexChanged --------
		
		private void pcbTextTransform_SelectedIndexChanged( object sender, EventArgs ev )
		{
			if( this.pLocked )
				return;

			this.pCurrentField.TextTransform = this.pcbTextTransform.SelectedIndex;
		}

		// ------------------------------------------------------------- pnBorderSize_ValueChanged --------------------
		
		private void pnBorderSize_ValueChanged( object sender, EventArgs ev )
		{
			if( this.pLocked )
				return;

			this.pCurrentField.DPIBorderSize = (float)this.pnBorderSize.Value;

			// odśwież kontrolke
			this.pCurrentField.Refresh();
		}

#endregion

/* ===============================================================================================================================
 * Edycja wzoru - Menu strony:
 * - icpPage_Opening		       [ContextMenu]
 * - icpAddField_Click             [ContextMenu/Button]
 * - icpDeleteFields_Click         [ContextMenu/Button]
 * - icpPageColor_Click            [ContextMenu/Button]
 * - icpPageBack_Click             [ContextMenu/Button]
 * - icpPageClear_Click            [ContextMenu/Button]
 * - icpPrintColor_CheckedChanged  [ContextMenu/CheckButton]
 * - icpPrintImage_CheckedChanged  [ContextMenu/CheckButton]
 * - icpAddPage_Click              [ContextMenu/Button]
 * - icpDeletePage_Click           [ContextMenu/Button]
 * =============================================================================================================================== */

#region Edycja wzoru - Menu strony

		// ------------------------------------------------------------- icpPage_Opening ------------------------------
		
		private void icPage_Opening( object sender, CancelEventArgs ev )
		{
			PageExtraData tag = (PageExtraData)this.pCurrentPage.Tag;

			this.pLocked = true;

			this.icpPrintColor.Checked = tag.print_color;
			this.icpPrintImage.Checked = tag.print_image;

			this.pLocked = false;
		}

		// ------------------------------------------------------------- pbAddLabel_Click -----------------------------
		
		private void icpAddField_Click( object sender, EventArgs ev )
		{
			PageField field = new PageField();

			// utwórz pole
			field.BorderColor   = Color.Black;
			field.DPIBorderSize = 0.3f;
			field.DPIBounds     = new Rectangle( 0, 0, 65, 10 );
			field.ForeColor     = Color.Black;
			field.Text          = "nowe pole";
			field.TextAlign     = ContentAlignment.MiddleCenter;
			field.BackColor     = Color.Transparent;
			field.DPIFontSize   = 8.25;
			field.DPIPadding    = 0.3f;
			field.DPIScale      = (double)Convert.ToInt32( this.pcbScale.Text ) / 100.0;
			field.Margin        = new Padding(0);
			field.Padding       = new Padding(0);
			field.Location      = new Point( 0, 0 );
			
			field.ApplyTextMargin = false;
			field.TextMargin      = new PointF( 2.0f, 2.0f );

			field.SetParentBounds( this.pPageSize.Width, this.pPageSize.Height );

			// przypisz zdarzenia
			field.ContextMenuStrip = this.icLabel;
			field.MouseDown += new MouseEventHandler( this.plLabel_MouseDown );
			field.MouseUp   += new MouseEventHandler( this.plLabel_MouseUp );
			field.MouseMove += new MouseEventHandler( this.plLabel_MouseMove );

			// informacje dodatkowe
			FieldExtraData field_extra = new FieldExtraData();
			field_extra.column         = -1;
			field_extra.image_from_db  = false;
			field_extra.pos_align      = (int)ContentAlignment.TopLeft;
			field_extra.print_border   = false;
			field_extra.print_color    = false;
			field_extra.print_image    = false;
			field_extra.print_text     = false;
			field_extra.text_from_db   = false;

			field.Tag = field_extra;

			// odblokuj pola i wypełnij domyślnymi danymi
			this.pCurrentField = field;
			this.FillFieldLabels();
			this.UnlockFieldLabels();

			// dodaj pole do strony
			this.pCurrentPage.Controls.Add( field );
		}

		// ------------------------------------------------------------- icpDeleteLabels_Click ------------------------
		
		private void icpDeleteFields_Click( object sender, EventArgs ev )
		{
			// ustaw obrazki jako nieaktywne
			for( int x = 0; x < this.pCurrentPage.Controls.Count; ++x )
				if( this.pCurrentPage.Controls[x].BackgroundImage != null )
					this.pCurrentPage.Controls[x].BackgroundImage.Dispose();

			this.pCurrentPage.Controls.Clear();
			this.LockFieldLabels();

			// wymuś uruchomienie zbieracza śmieci
			GC.Collect();
		}

		// ------------------------------------------------------------- icpPageColor_Click ---------------------------
		
		private void icpPageColor_Click( object sender, EventArgs ev )
		{
			if( this.gsColor.ShowDialog(this) != DialogResult.OK )
				return;

			this.pCurrentPage.BackgroundImage = null;
			this.pCurrentPage.BackColor       = this.gsColor.Color;

			Color color = this.gsColor.Color;
			this.ptbPageColor.Text = "#" + color.R.ToString("X2") + color.G.ToString("X2") + color.B.ToString("X2");
		}

		// ------------------------------------------------------------- pbPageBack_Click -----------------------------
		
		private void pbPageBack_Click( object sender, EventArgs ev )
		{
			// okno wyboru pliku
			if( this.gsImage.ShowDialog(this) != DialogResult.OK )
				return;

			if( this.pCurrentPage.BackgroundImage != null )
				this.pCurrentPage.BackgroundImage.Dispose();

			// zmień obraz panelu
			this.pCurrentPage.BackgroundImage = Image.FromFile( this.gsImage.FileName );
			this.pCurrentPage.BackColor       = Color.Empty;

			// wymuś uruchomienie zbieracza śmieci
			GC.Collect();
			
			// ustaw ścieżke do pliku
			((PageExtraData)this.pCurrentPage.Tag).image_path = this.gsImage.FileName;
		}

		// ------------------------------------------------------------- icpPageClear_Click ---------------------------
		
		private void icpPageClear_Click( object sender, EventArgs ev )
		{
			if( this.pCurrentPage.BackgroundImage != null )
				this.pCurrentPage.BackgroundImage.Dispose();

			this.pCurrentPage.BackgroundImage = null;
			this.pCurrentPage.BackColor = SystemColors.Window;

			// wymuś uruchomienie zbieracza śmieci
			GC.Collect();
		}

		// ------------------------------------------------------------- icpPrintColor_CheckedChanged -----------------
		
		private void icpPrintColor_CheckedChanged( object sender, EventArgs ev )
		{
			if( this.pLocked )
				return;

			this.pLocked = true;

			PageExtraData tag = (PageExtraData)this.pCurrentPage.Tag;
			tag.print_color = this.icpPrintColor.Checked;
			tag.print_image = false;
			
			this.pcbpDrawColor.Checked = this.icpPrintColor.Checked; 
			this.pcbpDrawImage.Checked = false;

			this.pLocked = false;
		}

		// ------------------------------------------------------------- icpPrintImage_CheckedChanged -----------------
		
		private void icpPrintImage_CheckedChanged( object sender, EventArgs ev )
		{
			if( this.pLocked )
				return;

			this.pLocked = true;

			PageExtraData tag = (PageExtraData)this.pCurrentPage.Tag;
			tag.print_image = icpPrintImage.Checked;
			tag.print_color = false;

			this.pcbpDrawColor.Checked = false;
			this.pcbpDrawImage.Checked = this.icpPrintImage.Checked;

			this.pLocked = false;
		}

		// ------------------------------------------------------------- icpAddPage_Click -----------------------------
		
		private void icpAddPage_Click( object sender, EventArgs ev )
		{
			AlignedPage page = new AlignedPage( );
			page.Align = 1;

			double scale = (double)Convert.ToInt32(this.pcbScale.Text) / 100.0;

			// zablokuj operacje odświeżania
			this.pLocked = true;

			// ustawienia panelu
			page.BackColor              = SystemColors.Window;
			page.BackgroundImageLayout	= ImageLayout.Stretch;

			page.Location = new Point( 0, 0 );
			page.Margin   = new Padding( 0, 0, 0, 0 );
			page.Size     = PatternEditor.GetDPIPageSize( this.pPatternData.size, scale );

			// informacje dodatkowe
			PageExtraData page_extra = new PageExtraData();

			page_extra.image_path  = "";
			page_extra.print_color = false;
			page_extra.print_image = false;

			page.Tag = page_extra;

			page.ContextMenuStrip = icPage;
			page.MouseDown       += new MouseEventHandler( this.ppPanelContainer_MouseDown );

			// zwiększ licznik stron
			++this.pPanelCounter;

			page.Hide();

			// dodaj panel do kontenera
			this.ppPreview.Controls.Add( page );

			page.CheckLocation();
			page.Show();

			// odblokuj odświeżanie
			this.pLocked = false;

			// zwiększ maksimum i ustaw aktualną stronę
			this.pnPage.Maximum = this.pPanelCounter + 1;
			this.pnPage.Value	= this.pPanelCounter + 1;

			// zablokuj pola
			this.LockFieldLabels();
		}

		// ------------------------------------------------------------- icpDeletePage_Click --------------------------
		
		private void icpDeletePage_Click( object sender, EventArgs ev )
		{
			// nie można usunąć strony gdy jest tylko jedna
			if( this.pPanelCounter < 1 )
			{
				MessageBox.Show
				(
					this,
					"Twój wzór posiada tylko jedną stronę - nie możesz jej usunąć!",
					"Usuń stronę",
					MessageBoxButtons.OK,
					MessageBoxIcon.Asterisk
				);
				return;
			}

			this.pLocked = true;

			// zmniejsz licznik stron
			--this.pPanelCounter;

			// wyczyść stronę
			this.icpDeleteFields_Click( null, null );
			this.icpPageClear_Click( null, null );

			// usuń stronę
			this.ppPreview.Controls.RemoveAt( this.pCurrentPanelID );

			// aktualizuj identyfikator
			this.pCurrentPanelID = this.pCurrentPanelID > this.pPanelCounter
				? this.pPanelCounter
				: this.pCurrentPanelID < 1
				? 0
				: this.pCurrentPanelID - 1;

			// ustaw nowe wartości zmiennych i pokaż stronę
			this.pCurrentPage = (Panel)this.ppPreview.Controls[this.pCurrentPanelID];
			this.pCurrentPage.Visible = true;
			this.pnPage.Value = this.pCurrentPanelID + 1;
			this.pnPage.Maximum = this.pPanelCounter + 1;
			
			// zablokuj pola
			this.LockFieldLabels();

			this.pLocked = false;
		}

#endregion

/* ===============================================================================================================================
 * Edycja wzoru - Menu pola:
 * - icLabel_Opening		   [ContextMenu]
 * - iclClearBack_Click        [ContextMenu/Button]
 * - iclDeleteField_Click      [ContextMenu/Button]
 * - iclBackFromDb_Click       [ContextMenu/CheckButton]
 * - iclPrintColor_Click       [ContextMenu/CheckButton]
 * - iclPrintImage_Click       [ContextMenu/CheckButton]
 * - iclTextFromDb_Click       [ContextMenu/CheckButton]
 * - iclPrintText_Click        [ContextMenu/CheckButton]
 * - iclPrintBorder_Click      [ContextMenu/CheckButton]
 * =============================================================================================================================== */

#region Edycja wzoru - Menu pola

		// ------------------------------------------------------------- icpLabel_Opening -----------------------------
		
		private void icLabel_Opening( object sender, CancelEventArgs ev )
		{
			//this.plLabel_Click( this.icLabel.SourceControl, ev );
			FieldExtraData tag = (FieldExtraData)this.pCurrentField.Tag;

			this.pLocked = true;

			// zaznacz lub odznacz elementy
			this.iclBackFromDb.Checked  = tag.image_from_db;
			this.iclPrintColor.Checked  = tag.print_color;
			this.iclPrintImage.Checked  = tag.print_image;
			this.iclTextFromDb.Checked  = tag.text_from_db;
			this.iclPrintText.Checked   = tag.print_text;
			this.iclPrintBorder.Checked = tag.print_border;

			this.pLocked = false;
		}

		// ------------------------------------------------------------- iclClearBack_Click ---------------------------
		
		private void iclClearBack_Click( object sender, EventArgs ev )
		{
			this.pCurrentField.BackImage = null;
			this.pCurrentField.BackColor = Color.Transparent;
		}

		// ------------------------------------------------------------- iclDeleteField_Click -------------------------
		
		private void iclDeleteField_Click( object sender, EventArgs ev )
		{
			if( this.pCurrentField.BackImage != null )
				this.pCurrentField.BackImage.Dispose();

			this.pCurrentPage.Controls.Remove( this.pCurrentField );
			this.LockFieldLabels();
		}

		// ------------------------------------------------------------- iclDeleteField_Click -------------------------
		
		private void iclBackFromDb_Click( object sender, EventArgs ev )
		{
			if( this.pLocked )
				return;

			bool is_check = this.iclBackFromDb.Checked;
			FieldExtraData tag = (FieldExtraData)this.pCurrentField.Tag;

			tag.image_from_db = is_check;
			tag.text_from_db  = false;
			tag.print_color   = false;
			tag.print_image   = false;

			this.pLocked = true;

			this.iclPrintColor.Checked = false;
			this.iclPrintImage.Checked = false;

			this.pcbDrawColor.Checked = false;
			this.pcbStatImage.Checked = false;
			this.pcbDynImage.Checked  = is_check;

			this.pLocked = false;
		}

		// ------------------------------------------------------------- iclPrintColor_Click --------------------------
		
		private void iclPrintColor_Click( object sender, EventArgs ev )
		{
			if( this.pLocked )
				return;

			bool is_check = this.iclPrintColor.Checked;
			FieldExtraData tag = (FieldExtraData)this.pCurrentField.Tag;

			tag.print_color   = is_check;
			tag.image_from_db = false;
			tag.print_image   = false;

			this.pLocked = true;

			this.iclBackFromDb.Checked = false;
			this.iclPrintImage.Checked = false;
			this.pcbDynImage.Checked   = false;
			this.pcbStatImage.Checked  = false;
			this.pcbDrawColor.Checked  = is_check;

			this.pLocked = false;
		}

		// ------------------------------------------------------------- iclPrintImage_Click --------------------------
		
		private void iclPrintImage_Click( object sender, EventArgs ev )
		{
			if( this.pLocked )
				return;

			bool is_check = this.iclPrintImage.Checked;
			FieldExtraData tag = (FieldExtraData)this.pCurrentField.Tag;

			tag.image_from_db = false;
			tag.print_image   = is_check;
			tag.print_color   = false;

			this.pLocked = true;

			this.iclBackFromDb.Checked = false;
			this.iclPrintColor.Checked = false;
			this.pcbDrawColor.Checked  = false;
			this.pcbDynImage.Checked   = false;
			this.pcbStatImage.Checked  = is_check;

			this.pLocked = false;
		}

		// ------------------------------------------------------------- iclTextFromDb_Click --------------------------
		
		private void iclTextFromDb_Click( object sender, EventArgs ev )
		{
			if( this.pLocked )
				return;

			bool is_check = this.iclTextFromDb.Checked;
			FieldExtraData tag = (FieldExtraData)this.pCurrentField.Tag;

			tag.text_from_db  = is_check;
			tag.image_from_db = false;
			tag.print_text    = false;

			this.pLocked = true;

			this.iclPrintText.Checked  = false;
			this.iclBackFromDb.Checked = false;
			this.pcbStatText.Checked   = false;
			this.pcbDynImage.Checked   = false;
			this.pcbDynText.Checked    = is_check;

			this.pLocked = false;
		}

		// ------------------------------------------------------------- iclPrintText_Click ---------------------------
		
		private void iclPrintText_Click( object sender, EventArgs ev )
		{
			if( this.pLocked )
				return;

			bool is_check = this.iclPrintText.Checked;
			FieldExtraData tag = (FieldExtraData)this.pCurrentField.Tag;

			tag.text_from_db = false;
			tag.print_text   = is_check;

			this.pLocked = true;

			this.iclTextFromDb.Checked = false;
			this.pcbDynImage.Checked   = false;
			this.pcbStatText.Checked   = is_check;

			this.pLocked = false;
		}

		// ------------------------------------------------------------- iclPrintText_Click ---------------------------
		
		private void iclPrintBorder_Click( object sender, EventArgs ev )
		{
			if( this.pLocked )
				return;

			this.pLocked = true;

			((FieldExtraData)this.pCurrentField.Tag).print_border = this.iclPrintBorder.Checked;
			this.pcbShowFrame.Checked = this.iclPrintBorder.Checked;

			this.pLocked = false;
		}

#endregion

/* ===============================================================================================================================
 * Edycja wzoru - Menu "SZCZEGÓŁY":
 * - pcbShowFrame_CheckedChanged        [CheckBox]
 * - pcbDrawColor_CheckedChanged        [CheckBox]
 * - pcbDynText_CheckedChanged          [CheckBox]
 * - pcbStatImage_CheckedChanged        [CheckBox]
 * - pcbStatText_CheckedChanged         [CheckBox]
 * - pcbStatImage_CheckedChanged        [CheckBox]
 * - pcbDynImage_CheckedChanged         [CheckBox]
 * - pcbPosAlign_SelectedIndexChanged   [ComboBox]
 * =============================================================================================================================== */

#region Edycja wzoru - Menu "SZCZEGÓŁY"

		// ------------------------------------------------------------- pcbShowFrame_CheckedChanged ------------------
		
		private void pcbShowFrame_CheckedChanged( object sender, EventArgs ev )
		{
			if( this.pLocked )
				return;

			this.pLocked = true;
			((FieldExtraData)this.pCurrentField.Tag).print_border = this.pcbShowFrame.Checked;
			this.pLocked = false;
		}

		// ------------------------------------------------------------- pcbDrawColor_CheckedChanged ------------------
		
		private void pcbDrawColor_CheckedChanged( object sender, EventArgs ev )
		{
			if( this.pLocked )
				return;

			bool is_check = this.pcbDrawColor.Checked;
			FieldExtraData tag = (FieldExtraData)this.pCurrentField.Tag;

			tag.image_from_db = false;
			tag.print_color   = is_check;

			this.pLocked = true;

			this.pcbDynImage.Checked  = false;
			this.pcbStatImage.Checked = false;

			this.pLocked = false;
		}

		// ------------------------------------------------------------- pcbDynText_CheckedChanged --------------------
		
		private void pcbDynText_CheckedChanged( object sender, EventArgs ev )
		{
			if( this.pLocked )
				return;

			bool is_check = this.pcbDynText.Checked;
			FieldExtraData tag = (FieldExtraData)this.pCurrentField.Tag;

			tag.text_from_db  = is_check;
			tag.image_from_db = false;
			tag.print_text    = false;

			this.pLocked = true;

			this.pcbStatText.Checked = false;
			this.pcbDynImage.Checked = false;

			this.pLocked = false;
		}

		// ------------------------------------------------------------- pcbStatImage_CheckedChanged ------------------
		
		private void pcbStatImage_CheckedChanged( object sender, EventArgs ev )
		{
			if( this.pLocked )
				return;

			bool is_check = this.pcbStatImage.Checked;
			FieldExtraData tag = (FieldExtraData)this.pCurrentField.Tag;

			tag.image_from_db = false;
			tag.print_image   = is_check;

			this.pLocked = true;

			this.pcbDynImage.Checked  = false;
			this.pcbDrawColor.Checked = false;

			this.pLocked = false;
		}

		// ------------------------------------------------------------- pcbStatText_CheckedChanged -------------------
		
		private void pcbStatText_CheckedChanged( object sender, EventArgs ev )
		{
			if( this.pLocked )
				return;

			bool is_check = this.pcbStatText.Checked;
			FieldExtraData tag = (FieldExtraData)this.pCurrentField.Tag;

			tag.text_from_db = false;
			tag.print_text   = is_check;

			this.pLocked = true;
			this.pcbDynText.Checked = false;
			this.pLocked = false;
		}

		// ------------------------------------------------------------- pcbDynImage_CheckedChanged -------------------
		
		private void pcbDynImage_CheckedChanged(object sender, EventArgs e)
		{
			if( this.pLocked )
				return;

			bool is_check = this.pcbDynImage.Checked;
			FieldExtraData tag = (FieldExtraData)this.pCurrentField.Tag;

			tag.image_from_db = is_check;
			tag.text_from_db  = false;
			tag.print_color   = false;
			tag.print_image   = false;

			this.pLocked = true;

			this.pcbDynText.Checked  = false;
			this.pcbDrawColor.Checked = false;
			this.pcbStatImage.Checked = false;

			this.pLocked = false;
		}

		// ------------------------------------------------------------- pcbPosAlign_SelectedIndexChanged -------------
		
		private void pcbPosAlign_SelectedIndexChanged( object sender, EventArgs ev )
		{
			if( this.pLocked )
				return;

			// dobierz odpowiedni tryb przylegania
			ContentAlignment align = ContentAlignment.TopLeft;
			switch( this.pcbPosAlign.SelectedIndex )
			{
				case 0: align = ContentAlignment.TopLeft;     break;
				case 1: align = ContentAlignment.TopRight;    break;
				case 2: align = ContentAlignment.BottomLeft;  break;
				case 4: align = ContentAlignment.BottomRight; break;
			}

			// zmień przyleganie
			((FieldExtraData)this.pCurrentField.Tag).pos_align = (int)align;
			PointF point = this.pCurrentField.GetPosByAlignPoint( align );

			this.pLocked = true;

			// zmień wartości w edytorze
			this.pnPositionX.Value = (decimal)point.X;
			this.pnPositionY.Value = (decimal)point.Y;

			this.pLocked = false;
		}

		// ------------------------------------------------------------- pcbdAddMargin_CheckedChanged -----------------
		
		private void pcbdAddMargin_CheckedChanged( object sender, EventArgs ev )
		{
			if( this.pLocked )
				return;

			this.pCurrentField.ApplyTextMargin = this.pcbdAddMargin.Checked;
		}

		// ------------------------------------------------------------- pcbMarginLR_ValueChanged ---------------------
		
		private void pcbMarginLR_ValueChanged( object sender, EventArgs ev )
		{
			if( this.pLocked )
				return;

			PointF dpi_new = this.pCurrentField.TextMargin;
			dpi_new.X = (float)this.pcbMarginLR.Value;
			this.pCurrentField.TextMargin = dpi_new;
		}

		// ------------------------------------------------------------- pcbMarginTB_ValueChanged ---------------------
		
		private void pcbMarginTB_ValueChanged( object sender, EventArgs ev )
		{
			if( this.pLocked )
				return;

			PointF dpi_new = this.pCurrentField.TextMargin;
			dpi_new.Y = (float)this.pcbMarginTB.Value;
			this.pCurrentField.TextMargin = dpi_new;
		}

#endregion

/* ===============================================================================================================================
 * Edycja wzoru - Menu "STRONA":
 * - pcbpDrawColor_CheckedChanged       [CheckBox]
 * - pcbpDrawImage_CheckedChanged       [CheckBox]
 * =============================================================================================================================== */

#region Edycja wzoru - Menu "STRONA"

		// ------------------------------------------------------------- pcbpDrawColor_CheckedChanged -----------------
		
		private void pcbpDrawColor_CheckedChanged( object sender, EventArgs ev )
		{
			if( this.pLocked )
				return;

			((PageExtraData)this.pCurrentPage.Tag).print_color = this.pcbpDrawColor.Checked;
			((PageExtraData)this.pCurrentPage.Tag).print_image = false;

			this.pLocked = true;
			this.pcbpDrawImage.Checked = false;
			this.pLocked = false;
		}

		// ------------------------------------------------------------- pcbpDrawImage_CheckedChanged -----------------

		private void pcbpDrawImage_CheckedChanged( object sender, EventArgs ev )
		{
			if( this.pLocked )
				return;

			((PageExtraData)this.pCurrentPage.Tag).print_color = false;
			((PageExtraData)this.pCurrentPage.Tag).print_image = this.pcbpDrawImage.Checked;

			this.pLocked = true;
			this.pcbpDrawColor.Checked = false;
			this.pLocked = false;
		}

#endregion

/* ===============================================================================================================================
 * Przetwarzanie danych - Lista elementów:
 * - dtvData_AfterSelect       [TreeView]
 * - dbScan_Click              [Button]
 * - dbGeneratePDF_Click       [Button]
 * =============================================================================================================================== */

#region Przetwarzanie danych - Lista elementów

		// ------------------------------------------------------------- dtvData_AfterSelect --------------------------
		
		private void dtvData_AfterSelect( object sender, TreeViewEventArgs ev )
		{
			if( this.dtvData.SelectedNode == null )
				return;

			this.pLocked = true;

			// narysuj wzór
			if( !this.dSketchDrawed )
			{
				PatternEditor.DrawSketch( this.dPatternData, this.dpPreview, 1.0 /*(double)this.cbScale.SelectedText / 100.0*/ );
				
				// dodaj obsługę myszy (przewijanie obrazka)
				foreach( Panel panel in this.dpPreview.Controls )
					panel.MouseDown += new MouseEventHandler( this.dpPreview_MouseDown );
				
				this.dSketchDrawed = true;
			}

			// uzupełnij danymi narysowany wzór
			PatternEditor.DrawRow( this.dpPreview, this.dDataContent, ev.Node.Index );

			// ustaw aktualną stronę
			if( this.dCurrentPage == null )
				this.dCurrentPage = (Panel)this.dpPreview.Controls[0];

			this.pLocked = false;
		}

		// ------------------------------------------------------------- dbScan_Click ---------------------------------
		
		private void dbScan_Click( object sender, EventArgs ev )
		{
			float width;
			Graphics gfx = this.CreateGraphics();

			// szukaj po rekordach
			for( int x = 0; x < this.dDataContent.rows; ++x )
			{
				// szukaj po stronach
				for( int y = 0; y < this.dPatternData.pages; ++y )
				{
					PageData page = this.dPatternData.page[y];

					// szukaj po polach
					for( int z = 0; z < page.fields; ++z )
					{
						FieldData field = page.field[z];

						width = PatternEditor.GetDimensionScale( field.bounds.Width, 1.0 );

						if( !field.extra.print_text && !(field.extra.text_from_db && field.extra.column > -1) )
							continue;

						// pobierz tekst do wypisania
						string text = field.extra.print_text
							? field.name
							: this.dDataContent.row[x,field.extra.column];

						float fsize = (float)(field.font_size * 1.0);
						Font  font  = new Font( field.font_name, fsize, field.font_style, GraphicsUnit.Point );

						SizeF bounds = gfx.MeasureString( text, font );

						// błąd - brak przejscia kolejnej linii
						if( bounds.Width > width )
							((TreeNode)this.dtvData.Nodes[x]).ForeColor = Color.OrangeRed;
					}
				}
			}
		}

		// ------------------------------------------------------------- dbGeneratePDF_Click --------------------------
		
		private void dbGeneratePDF_Click( object sender, EventArgs ev )
		{
			PatternEditor.GeneratePDF( this.dDataContent, this.dPatternData );
		}

#endregion

/* ===============================================================================================================================
 * Przetwarzanie danych - Inne:
 * - scData_SplitterMoved             [SplitContainer]
 * - dpPreview_MouseDown              [Panel]
 * - dcbZoom_SelectedIndexChanged     [ComboBox]
 * - dcbZoom_Leave                    [ComboBox]
 * - dcbZoom_KeyDown                  [ComboBox]
 * =============================================================================================================================== */

#region Przetwarzanie danych - Inne

		// ------------------------------------------------------------- scData_SplitterMoved -------------------------
		
		private void scData_SplitterMoved( object sender, SplitterEventArgs ev )
		{
			ColumnStyle col = this.sbData.ColumnStyles[1];
			col.SizeType = SizeType.Absolute;
			col.Width = this.scData.Panel2.Width + 7;
		}


		// ------------------------------------------------------------- dpPreview_MouseDown --------------------------
		
		private void dpPreview_MouseDown( object sender, MouseEventArgs ev )
		{
			this.dpPreview.Focus();
		}

		// ------------------------------------------------------------- dcbZoom_SelectedIndexChanged -----------------
		
		private void dcbZoom_SelectedIndexChanged( object sender, EventArgs ev )
		{
			if( this.pLocked )
				return;

			// próbuj zamienić string na int
			int scale = 0;
			try { scale = Convert.ToInt32(this.dcbZoom.Text); }
			catch
			{
				scale = 100;
				this.dcbZoom.Text = "100";
			}

			// nie można zmniejszyć więcej niż 50%
			if( scale < 50 )
			{
				this.dcbZoom.Text = "50";
				scale = 50;
			}

			// nie można powiększyć więcej niż 300%
			if( scale > 300 )
			{
				this.dcbZoom.Text = "300";
				scale = 300;
			}

			// zmień skale wzoru
			PatternEditor.ChangeScale( this.dPatternData, this.dpPreview, (double)scale / 100.0 );
		}

		// ------------------------------------------------------------- dcbZoom_Leave --------------------------------
		
		private void dcbZoom_Leave( object sender, EventArgs ev )
		{
			this.dcbZoom_SelectedIndexChanged( sender, ev );
		}

		// ------------------------------------------------------------- dcbZoom_KeyDown ------------------------------
		
		private void dcbZoom_KeyDown( object sender, KeyEventArgs ev )
		{
			if( ev.KeyCode == Keys.Enter )
				this.dcbZoom_SelectedIndexChanged( sender, null );
		}

		// ------------------------------------------------------------- dnPage_ValueChanged --------------------------
		
		private void dnPage_ValueChanged( object sender, EventArgs ev )
		{
			((Panel)this.dpPreview.Controls[(int)dnPage.Value - 1]).Show();
			this.dCurrentPage.Hide();
			this.dCurrentPage = (Panel)this.dpPreview.Controls[(int)dnPage.Value - 1];
		}

#endregion


		private void mbDelete_MouseEnter( object sender, EventArgs ev )
		{
			this.mlStatus.Text = "Usuwa wybrany z listy wzór.";
		}

		private void mbNew_MouseEnter( object sender, EventArgs ev )
		{
			this.mlStatus.Text = "Otwiera okno kreatora nowego wzoru.";
		}

		private void mlStatus_ClearText( object sender, EventArgs ev )
		{
			this.mlStatus.Text = "";
		}

		private void pcbScale_MouseEnter( object sender, EventArgs ev )
		{
			this.plStatus.Text = "Zmiana powiększenia wzoru. Możesz wybrać lub wpisać własną wartość w zakresie od 50 do 300.";
		}

		private void plStatus_ClearText( object sender, EventArgs ev )
		{
			this.plStatus.Text = "";
		}

		private void impHelp_Click( object sender, EventArgs ev )
		{
			MessageBox.Show( "Tymczasowo brak pomocy..." );
		}

		private void impInfo_Click( object sender, EventArgs ev )
		{
			InfoForm info = new InfoForm( );
			info.ShowDialog( this );
		}

		private void issGeneratePDF_Click(object sender, EventArgs e)
		{
			Settings options = new Settings( 2 );
			options.ShowDialog( this );
		}

		private void issEditor_Click(object sender, EventArgs e)
		{
			Settings options = new Settings( 1 );
			options.ShowDialog( this );
		}

		private void issGeneral_Click(object sender, EventArgs e)
		{
			Settings options = new Settings( 0 );
			options.ShowDialog( this );
		}






		// ------------------------------------------------------------- gmprClearList_Click --------------------------
		
		private void gmprClearList_Click( object sender, EventArgs ev )
		{
			// wyczyść ostatnio otwierane projekty
			this.gSettings.LastPatterns = new List<string>();

			// usuń pozycje w menu
			while( this.gmpRecent.DropDownItems.Count > 2 )
				this.gmpRecent.DropDownItems.RemoveAt( 0 );

			// wyłącz pozycje w menu
			this.gmpRecent.Enabled = false;
			GC.Collect();
		}

		// ------------------------------------------------------------- gmpRecent_RefreshList ------------------------
		
		private void gmpRecent_RefreshList( )
		{
#		if DEBUG
			Console.WriteLine( "Uzupełnianie listy ostatnio otwieranych wzorów." );
#		endif

			// usuń pozycje w menu
			while( this.gmpRecent.DropDownItems.Count > 2 )
				this.gmpRecent.DropDownItems.RemoveAt( 0 );

			List<string> lpatterns = this.gSettings.LastPatterns;

			// dodaj ostatnio używane wzory
			if( lpatterns.Count > 0 )
			{
				this.gmpRecent.Enabled = true;
				for( int x = 0; x < lpatterns.Count; ++x )
				{
					this.gmpRecent.DropDownItems.Insert( x, new ToolStripMenuItem((x+1) + ": " + lpatterns[x]) );
					this.gmpRecent.DropDownItems[x].Click += new EventHandler( this.gmprItem_Click );
				}
			}
			// jeżeli brak, wyłącz pole
			else
				this.gmpRecent.Enabled = false;

			GC.Collect();
		}

		// ------------------------------------------------------------- gmprItem_Click -------------------------------
		
		private void gmprItem_Click( object sender, EventArgs ev )
		{
			// wyszukaj wzór na liście
			string pattern = ((ToolStripItem)sender).Text;
			pattern = pattern.Substring( pattern.IndexOf(':') + 2 ).Trim();

			// wyszukaj wzór na liście
			int index = 0;
			for( ; index < this.mtvPatterns.Nodes.Count; ++index )
				if( this.mtvPatterns.Nodes[index].Text == pattern )
					break;

			// brak wzoru na liście
			if( index == this.mtvPatterns.Nodes.Count )
			{
				MessageBox.Show
				(
					this,
					"Wybrany wzór już nie istnieje!\nW związku z powyższym, zostanie on usunięty z listy.",
					"Otwieranie wzoru",
					MessageBoxButtons.OK,
					MessageBoxIcon.Warning
				);

				// usuń wzór i odśwież elementy
				this.gSettings.RemoveFromLastPatterns( pattern );
				this.gmpRecent_RefreshList();

				return;
			}

			// zaznacz i edytuj wzór
			this.mtvPatterns.SelectedNode = this.mtvPatterns.Nodes[index];
			this.ictEdit_Click( null, null );
		}



		private void ppPanelContainer_Resize( object sender, EventArgs ev )
		{
			foreach( AlignedPage page in this.ppPreview.Controls )
				page.CheckLocation();
		}

		private void dpPreview_Resize( object sender, EventArgs ev )
		{
			foreach( AlignedPage page in this.dpPreview.Controls )
				page.CheckLocation();
		}

		ToolTip _tooltip      = new ToolTip();
		bool    _tooltip_show = false;

		// ------------------------------------------------------------- ptbName_KeyPress ----------------------------
		
		private void ptbName_KeyPress( object sender, KeyPressEventArgs ev )
		{
			string locale_chars = "ĘÓŁŚĄŻŹĆŃęółśążźćń";

			if( ev.KeyChar == 8 || ModifierKeys == Keys.Control )
				return;

			Regex regex = new Regex( @"^[0-9a-zA-Z" + locale_chars + @" \-+_]+$" );
			if( !regex.IsMatch(ev.KeyChar.ToString()) )
			{
				//this._tooltip.Popup += new PopupEventHandler( _tooltip_Popup );

				if( !this._tooltip_show )
				{
					this._tooltip.Show
					(
						"Dopuszczalne znaki:\n" +
						"Znaki alfabetu, cyfry, - + _ oraz spacja.",
						this.ptbName,
						new Point( -20, this.ptbName.Height + 2 )
					);
					this._tooltip_show = true;
				}

				ev.Handled = true;
				System.Media.SystemSounds.Beep.Play();
				
				return;
			}

			if( this._tooltip_show )
			{
				this._tooltip.Hide( this.ptbName );
				this._tooltip_show = false;
			}
		}

		//private void _tooltip_Popup( object sender, PopupEventArgs ev )
		//{
		//	Console.WriteLine( ev.ToolTipSize.Width + " , " + ev.ToolTipSize.Height );
		//}

		// ------------------------------------------------------------- ptbName_Leave -------------------------------
		
		private void ptbName_Leave( object sender, EventArgs ev )
		{
			if( this._tooltip_show )
			{
				this._tooltip.Hide( this.ptbName );
				this._tooltip_show = false;
			}
		}

		// ------------------------------------------------------------- Main_Move -----------------------------------
		
		private void Main_Move( object sender, EventArgs ev )
		{
			if( this._tooltip_show )
			{
				this._tooltip.Hide( this.ptbName );
				this._tooltip_show = false;
			}
		}

		private void gmtConnectDB_Click( object sender, EventArgs ev )
		{
			DBConnection window = new DBConnection();

			window.ShowDialog( this );
		}












		private void gmtJoinColumns_Click( object sender, EventArgs ev )
		{
			if( this._reader == null )
				return;

			// łączenie kolumn
			EditColumnsForm joiner = new EditColumnsForm( this._reader );
			joiner.ShowDialog();
		}

		private void gmtLoadDatabase_Click( object sender, EventArgs ev )
		{
			DatabaseReader reader = new DatabaseReader();

			if( reader.IsReady() )
				this._reader = reader;

			// włącz przycisk do łączenia kolumn
			if( this._reader != null )
				this.gmtColumnsEditor.Enabled = true;

			GC.Collect();
		}

		private void imMain_Paint( object sender, PaintEventArgs ev )
		{
			ev.Graphics.DrawLine
			(
				new Pen( SystemColors.ControlDark ),
				this.imMain.Bounds.X,
				this.imMain.Bounds.Bottom - 1,
				this.imMain.Bounds.Right,
				this.imMain.Bounds.Bottom - 1
			);
		}

		private void tlMainStatusBar_Paint( object sender, PaintEventArgs ev )
		{
			ev.Graphics.DrawLine
			(
				new Pen( SystemColors.ControlDark ),
				this.tlMainStatusBar.Bounds.X,
				0,
				this.tlMainStatusBar.Bounds.Right,
				0
			);
		}
	}
}
