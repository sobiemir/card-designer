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


// @TODO dodać do konfiguracji nowe pole z informacją o danych


namespace CDesigner
{
	public partial class Main : Form
	{
		private CDField	pCurrentLabel	= null;
		private Panel		pCurrentPanel	= null;
		private int			pLabelCounter	= 0;
		private int			pPanelCounter	= 0;
		private int			pCurrentPanelID	= -1;
		private bool		pLocked			= false;
		private Size		pPageSize;
		private int			mCurrentPattern = -1;
		private string		mCurrentName	= null;

		private int			gThisContainer	= 1;
		private bool		gEditChanged	= false;

		private int			mSelectedID		= -1;
		private bool		mSelectedError	= false;
		private bool		mSelectedData	= false;
		private string		mSelectedName	= null;
		private bool		mNoImage		= false;

/* ===============================================================================================================================
 * Funkcje główne klasy:
 * - Main					[Public]
 * - ProcessCmdKey			[Protected]
 * - RefreshProjectList		[Private]
 * - LockFieldLabels		[Private]
 * - UnlockFieldLabels		[Private]
 * - FillFieldLabels		[Private]
 * =============================================================================================================================== */

		// ------------------------------------------------------------- Main -----------------------------------------
		
		public Main( )
		{
			InitializeComponent( );

			// odśwież listę wzorów
			this.RefreshProjectList( );
		}

		// ------------------------------------------------------------- ProcessCmdKey --------------------------------
		
		protected override bool ProcessCmdKey( ref Message msg, Keys keydata )
		{
			switch( keydata )
			{
				// zmień strone wzoru
				case Keys.Control | Keys.Tab:
					if( this.mnPage.Value < this.mnPage.Maximum )
						this.mnPage.Value += 1;
				break;
				// zmień strone wzoru
				case Keys.Shift | Keys.Tab:
					if( this.mnPage.Value > this.mnPage.Minimum )
						this.mnPage.Value -= 1;
				break;
				default:
					return base.ProcessCmdKey( ref msg, keydata );
			}
			return true;
		}

		// ------------------------------------------------------------- RefreshProjectList ---------------------------
		
		private void RefreshProjectList( )
		{
			// wyczyść wszystkie dane
			this.mtvPatterns.Nodes.Clear( );

			// pobierz i dodaj katalogi do listy
			string[] patterns = Directory.GetDirectories( "patterns" );

			foreach( string pattern in patterns )
			{
				TreeNode item = this.mtvPatterns.Nodes.Add( pattern.Replace("patterns\\", "") );

				// brak pliku konfiguracji
				if( !File.Exists(pattern + "/config.cfg") )
				{
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
			this.pbFontName.Enabled = false;
			this.pbFontColor.Enabled = false;
			this.pbBackImage.Enabled = false;
			this.pbBackColor.Enabled = false;
			this.pnBorderSize.Enabled = false;
			this.pnPositionX.Enabled = false;
			this.pnPositionY.Enabled = false;
			this.pnWidth.Enabled = false;
			this.pnHeight.Enabled = false;
			this.ptbName.Enabled = false;
			this.ptrTextAlign.Enabled = false;
			this.pnPadding.Enabled = false;
		}

		// ------------------------------------------------------------- UnlockFieldLabels ----------------------------
		
		private void UnlockFieldLabels( )
		{
			this.pbBorderColor.Enabled = true;
			this.pbFontName.Enabled = true;
			this.pbFontColor.Enabled = true;
			this.pbBackImage.Enabled = true;
			this.pbBackColor.Enabled = true;
			this.pnBorderSize.Enabled = true;
			this.pnPositionX.Enabled = true;
			this.pnPositionY.Enabled = true;
			this.pnWidth.Enabled = true;
			this.pnHeight.Enabled = true;
			this.ptbName.Enabled = true;
			this.ptrTextAlign.Enabled = true;
			this.pnPadding.Enabled = true;
		}

		// ------------------------------------------------------------- FillFieldLabels ------------------------------
		
		private void FillFieldLabels( )
		{
			// zmień nazwę
			this.ptbName.Text = this.pCurrentLabel.Text;

			// aktualizuj pola numeryczne
			this.pnPositionX.Value = this.pCurrentLabel.DPILeft;
			this.pnPositionY.Value = this.pCurrentLabel.DPITop;
			this.pnHeight.Value	= this.pCurrentLabel.DPIHeight;
			this.pnWidth.Value = this.pCurrentLabel.DPIWidth;
			this.pnBorderSize.Value = this.pCurrentLabel.DPIBorderSize;
			this.pnPadding.Value = this.pCurrentLabel.DPIPadding;

			string color_r, color_g, color_b;

			// aktualizuj kolory...
			color_r = this.pCurrentLabel.BorderColor.R.ToString("X2");
			color_g = this.pCurrentLabel.BorderColor.G.ToString("X2");
			color_b = this.pCurrentLabel.BorderColor.B.ToString("X2");
			this.ptbBorderColor.Text = color_r + color_b + color_g;

			color_r = this.pCurrentLabel.BackColor.R.ToString("X2");
			color_g = this.pCurrentLabel.BackColor.G.ToString("X2");
			color_b = this.pCurrentLabel.BackColor.B.ToString("X2");
			this.ptbBackColor.Text = color_r + color_g + color_b;

			color_r = this.pCurrentLabel.ForeColor.R.ToString("X2");
			color_g = this.pCurrentLabel.ForeColor.G.ToString("X2");
			color_b = this.pCurrentLabel.ForeColor.B.ToString("X2");
			this.ptbFontColor.Text = color_r + color_g + color_b;

			string bold, italic, strikeout, underline, font_name, font_size;

			// czcionka...	
			bold = this.pCurrentLabel.Font.Bold ? "B" : "";
			italic = this.pCurrentLabel.Font.Italic ? "I" : "";
			underline = this.pCurrentLabel.Font.Underline ? "U" : "";
			strikeout = this.pCurrentLabel.Font.Strikeout ? "S" : "";
			font_name = this.pCurrentLabel.Font.Name;
			font_size = this.pCurrentLabel.Font.SizeInPoints.ToString( );
			this.ptbFontName.Text = font_name + ", " + font_size + "pt " + bold + italic + underline + strikeout;

			// pozycja tekstu
			this.ptrTextAlign.Value = this.pCurrentLabel.TextAlignment;

			// nazwa / ścieżka obrazu
			this.ptbBackImage.Text = this.pCurrentLabel.BackImagePath != null ? this.pCurrentLabel.BackImagePath : "";
		}

/* ===============================================================================================================================
 * Główne menu okna:
 * - ispNew_Click		[Menu/Button/Button]
 * - ispClose_Click		[Menu/Button/Button]
 * - isHome_Click		[Menu/Button]
 * - isPattern_Click	[Menu/Button]
 * - isData_Click		[Menu/Button]
 * =============================================================================================================================== */

		// ------------------------------------------------------------- ispNew_Click ---------------------------------
		
		private void ispNew_Click( object sender, EventArgs ev )
		{
			NewPattern window = new NewPattern( );

			// nowy wzór
			if( window.ShowDialog(this) != DialogResult.OK )
				return;

			// odśwież listę
			this.RefreshProjectList( );

			// zaznacz nowy wzór
			foreach( TreeNode node in this.mtvPatterns.Nodes )
				if( node.Text == PatternEditor.last_created )
				{
					this.mtvPatterns.SelectedNode = node;
					break;
				}
			
			// przejdź do edycji wzoru
			this.ictEdit_Click( null, null );
		}

		// ------------------------------------------------------------- ispClose_Click -------------------------------
		
		private void ispClose_Click(object sender, EventArgs e)
		{
			// zakończ działanie programu
			Application.Exit( );
		}

		// ------------------------------------------------------------- isHome_Click ---------------------------------
		
		private void isHome_Click( object sender, EventArgs ev )
		{
			this.tHomePanel.Show( );
			this.isHome.Enabled = false;

			if( this.gThisContainer == 2 )
			{
				this.tPatternPanel.Hide( );
				this.isPattern.Enabled = true;
			}
			else if( this.gThisContainer == 3 )
			{
				this.tDataPanel.Hide( );
				this.isData.Enabled = true;
			}

			// odśwież podgląd
			if( this.mCurrentName == this.mSelectedName && this.gEditChanged )
				if( this.mtvPatterns.SelectedNode != null )
				{
					this.mSelectedID = this.mtvPatterns.SelectedNode.Index;
					this.mtvPatterns_AfterSelect( null, null );
				}

			this.gEditChanged = false;
			this.gThisContainer = 1;
		}

		// ------------------------------------------------------------- isPattern_Click ------------------------------
		
		private void isPattern_Click( object sender, EventArgs ev )
		{
			this.tPatternPanel.Show( );
			this.isPattern.Enabled = false;

			if( this.gThisContainer == 1 )
			{
				this.tHomePanel.Hide( );
				this.isHome.Enabled = true;
			}
			else if( this.gThisContainer == 3 )
			{
				this.tDataPanel.Hide( );
				this.isData.Enabled = true;
			}

			this.gThisContainer = 2;
		}

		// ------------------------------------------------------------- isData_Click ---------------------------------
		
		private void isData_Click( object sender, EventArgs ev )
		{
			this.tDataPanel.Show( );
			this.isData.Enabled = false;

			if( this.gThisContainer == 1 )
			{
				this.tHomePanel.Hide( );
				this.isHome.Enabled = true;
			}
			else if( this.gThisContainer == 2 )
			{
				this.tPatternPanel.Hide( );
				this.isPattern.Enabled = true;
			}

			this.gThisContainer = 3;
		}

/* ===============================================================================================================================
 * Funkcje listy wzorów:
 * - mtvPatterns_MouseDown		[ListBox]
 * - mtvPatterns_AfterSelect	[ListBox]
 * - icPattern_Opening			[ContextMenu]
 * - ictEdit_Click				[ContextMenu/Button]
 * - ictDelete_Click			[ContextMenu/Button]
 * - ictLoadData_Click			[ContextMenu/Button]
 * =============================================================================================================================== */

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
		
		private void mtvPatterns_AfterSelect( object sender, EventArgs ev )
		{
			// indeks poza zakresem
			if( this.mtvPatterns.SelectedNode == null )
			{
				this.mnPage.Enabled = false;
				this.mbDelete.Enabled = false;
				return;
			}

			// indeks się nie zmienił...
			if( this.mSelectedID == this.mtvPatterns.SelectedNode.Index )
				return;

			// proszę czekać...
			this.mlStatus.Text = "Wczytywanie podglądu wzoru...";
			this.mlStatus.Refresh( );
			this.Cursor = Cursors.WaitCursor;

			this.mSelectedID = this.mtvPatterns.SelectedNode.Index;
			this.mSelectedError = false;
			this.mSelectedData = true;
			this.mbDelete.Enabled = true;

			this.pLocked = true;

			// pobierz nazwę elementu
			string pattern = this.mtvPatterns.SelectedNode.Text;
			this.mSelectedName = pattern;

			// sprawdź czy wzór nie jest uszkodzony
			if( !File.Exists("patterns/" + pattern + "/config.cfg") )
			{
				this.mNoImage = true;
				this.mSelectedError = true;

				if( this.mibPreview.Image != null )
					this.mibPreview.Image.Dispose( );

				// brak obrazka
				if( File.Exists("noimage.png") )
					this.mibPreview.Image = Image.FromFile( "noimage.png" );
				else
					this.mibPreview.Image = null;

				// dostosuj rozmiar
				this.mpPreview_Resize( null, null );
				this.mnPage.Enabled = false;
				this.pLocked = false;

				//MessageBox.Show( this, "Wybrany wzór jest uszkodzony!", "Błąd przetwarzania..." );
				this.Cursor = null;
				this.mlStatus.Text = "Wybrany wzór jest uszkodzony!";

				return;
			}

			// wczytaj podstawowe dane
			FileStream file = new FileStream( "patterns/" + pattern + "/config.cfg", FileMode.OpenOrCreate );
			BinaryReader reader = new BinaryReader( file );

			int twidth = reader.ReadInt16( ),
				theight = reader.ReadInt16( ),
				pages = reader.ReadByte( ),
				format = -1;
			
			// treść dynamiczna
			this.mSelectedData = reader.ReadByte( ) == 1;

			// wykryj format wzoru
			for( int x = 0; x < NewPattern.pf_dims.GetLength(0); ++x )
				if( NewPattern.pf_dims[x,0] == twidth && NewPattern.pf_dims[x,1] == theight )
				{
					format = x;
					break;
				}

			// ustaw wartości pola numerycznego
			this.mnPage.Maximum = pages;
			this.mnPage.Value = 1;

			// zamknij plik
			reader.Close( );
			file.Close( );

			// podgląd stron
			if( File.Exists("patterns/" + pattern + "/preview0.jpg") )
			{
				this.mNoImage = false;

				using( Image image = Image.FromFile("patterns/" + pattern + "/preview0.jpg") )
				{
					if( this.mibPreview.Image != null )
						this.mibPreview.Image.Dispose( );

					// skopiuj obraz do pamięci
					Image new_image = new Bitmap( image.Width, image.Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb );
					using( var canavas = Graphics.FromImage(new_image) )
						canavas.DrawImageUnscaled( image, 0, 0 );
					this.mibPreview.Image = new_image;

					image.Dispose( );
				}

				// dostosuj rozmiar
				this.mpPreview_Resize( null, null );
				this.mnPage.Enabled = true;
				this.pLocked = false;
				this.Cursor = null;
				
				// aktualizuj status
				if( format == -1 )
					this.mlStatus.Text = "Wzór " + pattern + ". Format własny: " + twidth + " x " + theight + " mm. Ilość stron: " + pages + ".";
				else
					this.mlStatus.Text = "Wzór " + pattern + ". Format " + NewPattern.pf_names[format] + ": " + twidth + " x " + theight + " mm. Ilość stron: " + pages + ".";

				return;
			}

			this.mNoImage = true;
			this.mSelectedData = false;

			if( this.mibPreview.Image != null )
				this.mibPreview.Image.Dispose( );

			// brak obrazka
			if( File.Exists("noimage.png") )
				this.mibPreview.Image = Image.FromFile( "noimage.png" );
			else
				this.mibPreview.Image = null;

			// dostosuj obraz
			this.mpPreview_Resize( null, null );
			this.mnPage.Enabled = false;
			this.pLocked = false;

			// aktualizuj status
			if( format == -1 )
				this.mlStatus.Text = "Wzór " + pattern + ". Format własny: " + twidth + " x " + theight + " mm. Brak podglądu.";
			else
				this.mlStatus.Text = "Wzór " + pattern + ". Format " + NewPattern.pf_names[format] + ": " + twidth + " x " + theight + " mm. Brak podglądu.";
			
			this.Cursor = null;
		}

		// ------------------------------------------------------------- icPattern_Opening ----------------------------
		
		private void icPattern_Opening( object sender, CancelEventArgs ev )
		{
			// odblokuj wszystkie przyciski w menu
			this.ictNew.Enabled = true;
			this.ictEdit.Enabled = true;
			this.ictLoadData.Enabled = true;
			this.ictImport.Enabled = true;
			this.ictExport.Enabled = true;
			this.ictDelete.Enabled = true;

			// zablokuj gdy istnieje taka potrzeba
			if( this.mSelectedID == -1 )
			{
				this.ictEdit.Enabled = false;
				this.ictLoadData.Enabled = false;
				this.ictExport.Enabled = false;
				this.ictDelete.Enabled = false;
			}
			else if( this.mSelectedError )
			{
				this.ictEdit.Enabled = false;
				this.ictLoadData.Enabled = false;
				this.ictExport.Enabled = false;
			}
			// @TODO - udoskonalić
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
			this.pLocked = true;

			// przejście do edycji wzoru
			this.isPattern_Click( null, null );

			// zapisz indeks i nazwę aktualnego wzoru
			this.mCurrentPattern = this.mSelectedID;
			string item = this.mSelectedName;
			this.mCurrentName = item;

			// otwórz wzór
			int pages = PatternEditor.Open( item, ref this.pPageSize, this.ppPanelContainer );

			// ustal rozmiar DPI
			double dpi_scale = (double)this.pnDPI.Value / 100.0;
			double dpi_px_scale = dpi_scale * 3.938095238095238;
			Size pp_size = new Size
			(
				(int)((double)this.pPageSize.Width * dpi_px_scale),
				(int)((double)this.pPageSize.Height * dpi_px_scale)
			);

			// popraw strony
			for( int x = 0; x < this.ppPanelContainer.Controls.Count; ++x )
			{
				Panel panel = (Panel)this.ppPanelContainer.Controls[x];

				// ustawienia panelu
				panel.BackgroundImageLayout	= ImageLayout.Stretch;
				panel.ContextMenuStrip = icPage;
				panel.Location = new Point( 0, 0 );
				panel.Margin = new Padding( 0, 0, 0, 0 );
				panel.Size = pp_size;

				// pozostaw pierwszy panel widoczny
				if( x != 0 )
					panel.Hide( );
				else
				{
					this.pCurrentPanelID = x;
					this.pCurrentPanel = panel;
				}

				// popraw pola
				for( int y = 0; y < panel.Controls.Count; ++y )
				{
					CDField label = (CDField)panel.Controls[y];

					// skala DPI
					label.DPIScale = dpi_scale;
					label.Margin = new Padding(0);

					// menu i akcja na kliknięcie myszą
					label.ContextMenuStrip = icLabel;
					label.Click += new EventHandler( plLabel_Click );
				}
			}

			// ilość stron
			this.pPanelCounter = pages - 1;
			this.pnPage.Maximum = pages;
			this.pnPage.Value = 1;

			// aktualna strona
			this.pCurrentPanel = (Panel)this.ppPanelContainer.Controls[0];
			this.pCurrentPanelID = 0;

			// zablokuj pola
			this.LockFieldLabels( );

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
				"Czy na pewno chcesz usunąć wzór o nazwie: \"" + pattern + "\"?",
				"Usuwanie wzoru...",
				MessageBoxButtons.YesNo
			);
			if( result == DialogResult.No )
				return;

			// sprawdź czy usuwany jest edytowany wzór
			if( this.mCurrentPattern == this.mSelectedID )
			{
				this.mCurrentPattern = -1;
				this.mCurrentName = null;

				// wyłącz możliwość edycji nieistniejącego już wzoru
				this.isPattern.Enabled = false;
			}
			// brak wzoru, brak obrazka...
			this.mibPreview.Image = null;
			this.mSelectedID = -1;

			// usuń pliki wzoru i odśwież liste
			Directory.Delete( "patterns/" + pattern, true );
			this.RefreshProjectList( );
		}

		// ------------------------------------------------------------- ictLoadData_Click ----------------------------
		
		private void ictLoadData_Click( object sender, EventArgs ev )
		{
			// wybór pliku
			if( this.gsDBase.ShowDialog(this) != DialogResult.OK )
				return;

			// pobierz informacje o polach wzoru
			DBPageFields[] fields = null;
			int pages = PatternEditor.GetPagesFields( this.mSelectedName, ref fields );

			// przypisz kolumny do pól
			SetColumns colset = new SetColumns( pages, ref fields, this.gsDBase.FileName );
			if( colset.ShowDialog(this) != DialogResult.OK )
				return;

			// przejdź na stronę
			this.isData_Click( null, null );

			// pobierz dane z pliku
			SCVData data = new SCVData( );
			SetColumns.GetDBData( this.gsDBase.FileName, ref data );

			if( SetColumns._checked_items[0] == -1 )
				SetColumns._checked_items[0] = 0;

			for( int x = 0; x < data.rows; ++x )
			{
				string node = data.row[SetColumns._checked_items[0],x];
				if( SetColumns._checked_items[1] > -1 )
					node += " " + data.row[SetColumns._checked_items[1],x];
				this.dtvData.Nodes.Add( node );
			}
		}

/* ===============================================================================================================================
 * Strona główna - inne:
 * - mnPage_ValueChanged	[NumericUpDown]
 * - mpPreview_Resize		[Panel]
 * - mpPreview_MouseEnter	[Panel]
 * =============================================================================================================================== */

		// ------------------------------------------------------------- mnPage_ValueChanged -------------------------
		
		private void mnPage_ValueChanged( object sender, EventArgs ev )
		{
			// zmień status
			this.mlStatus.Text = "Wczytywanie podglądu strony wzoru...";
			this.mlStatus.Refresh( );
			this.Cursor = Cursors.WaitCursor;

			// otwórz podgląd wybranej strony wzoru
			if( File.Exists("patterns/" + this.mSelectedName + "/preview" + (this.mnPage.Value - 1) + ".jpg") )
			{
				using( Image image = Image.FromFile("patterns/" + this.mSelectedName + "/preview" + (this.mnPage.Value - 1) + ".jpg") )
				{
					if( this.mibPreview.Image != null )
						this.mibPreview.Image.Dispose( );

					// skopiuj obraz do pamięci
					Image new_image = new Bitmap( image.Width, image.Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb );
					using( var canavas = Graphics.FromImage(new_image) )
						canavas.DrawImageUnscaled( image, 0, 0 );
					this.mibPreview.Image = new_image;

					image.Dispose( );
				}
				// aktualizuj status
				this.mlStatus.Text = "Wczytano podgląd strony nr " + this.mnPage.Value + ".";
			}
			else
			{
				// @TODO - dodać jakiś inny obrazek :D
				this.mibPreview.Image = null;
				this.mlStatus.Text = "Podgląd tej strony nie istnieje.";
			}

			this.Cursor = null;
		}

		// ------------------------------------------------------------- mpPreview_Resize ----------------------------
		
		private void mpPreview_Resize( object sender, EventArgs ev )
		{
			int diff = 0;
			Point location = new Point( 0, 0 );

			// centruj obrazek w poziomie
			if( this.mpPreview.VerticalScroll.Visible )
				diff = (this.mpPreview.Width - SystemInformation.VerticalScrollBarWidth - this.mibPreview.Width) / 2;
			else
				diff = (this.mpPreview.Width - this.mibPreview.Width) / 2;

			if( diff > 0 )
				location.X = diff;

			// centruj obrazek w pionie
			if( this.mNoImage )
			{
				if( this.mpPreview.HorizontalScroll.Visible )
					diff = (this.mpPreview.Height - SystemInformation.HorizontalScrollBarHeight - this.mibPreview.Height) / 2;
				else
					diff = (this.mpPreview.Height - this.mibPreview.Height) / 2;

				if( diff > 0 )
					location.Y = diff;
			}

			this.mibPreview.Location = location;
		}

		// ------------------------------------------------------------- mibPreview_MouseHover -----------------------
		
		private void mpPreview_MouseEnter( object sender, EventArgs ev )
		{
			// dzięki temu można przesuwać obrazek kółkiem myszy
			this.mpPreview.Focus( );
		}

/* ===============================================================================================================================
 * Edycja pola na stronie:
 * - pbAddLabel_Click		[Button]
 * =============================================================================================================================== */

		// ------------------------------------------------------------- pbAddLabel_Click -----------------------------
		
		private void pbAddLabel_Click( object sender, EventArgs ev )
		{
			CDField label = new CDField( );

			// ustaw wartości
			label.BorderColor = Color.Black;
			label.Text = "nowe pole";
			label.TextAlign = ContentAlignment.MiddleCenter;
			label.BackColor = Color.Transparent;
			label.DPIFontSize = 8.25;
			label.DPIPadding = 1;
			label.TextAlignment	= 4;

			// ustaw granice pola
			label.DPILeft = 0;
			label.DPITop = 0;
			label.DPIWidth = 65;
			label.DPIHeight = 10;
			label.DPIBorderSize = 1;
			label.DPIScale = (double)this.pnDPI.Value / 100.0;

			// położenie i marginesy
			label.Margin = new Padding(0);
			label.Padding = new Padding(0);
			label.Location = new Point(0, 0);

			// menu i akcja na kliknięcie myszą
			label.ContextMenuStrip = icLabel;
			label.Click += new EventHandler( plLabel_Click );

			// informacje dodatkowe
			label.Tag = new CustomLabelDetails( );

			// zwiększ licznik pól
			this.pLabelCounter++;

			// zmień aktualne pole
			this.pCurrentLabel = label;
			this.FillFieldLabels( );
			this.UnlockFieldLabels( );

			// dodaj do kontenera
			this.pCurrentPanel.Controls.Add( label );
		}

		// ------------------------------------------------------------- plLabel_Click --------------------------------
		
		private void plLabel_Click( object sender, EventArgs ev )
		{
			// aktualny obiekt
			this.pCurrentLabel = (CDField)sender;

			// zablokuj zmiane wartości
			this.pLocked = true;

			// wypełnij pola i odblokuj je
			this.FillFieldLabels( );
			this.UnlockFieldLabels( );

			// odblokuj zmiane wartości
			this.pLocked = false;
		}

		// ------------------------------------------------------------- ptbName_TextChanged --------------------------
		
		private void ptbName_TextChanged( object sender, EventArgs ev )
		{
			if( this.pLocked )
				return;

			this.pCurrentLabel.Text = this.ptbName.Text;
		}

		// ------------------------------------------------------------- pnBorderSize_ValueChanged --------------------
		
		private void pnBorderSize_ValueChanged( object sender, EventArgs ev )
		{
			if( this.pLocked )
				return;

			this.pCurrentLabel.DPIBorderSize = (int)pnBorderSize.Value;

			// odśwież kontrolke
			this.pCurrentLabel.Refresh( );
		}

		// ------------------------------------------------------------- pnPositionX_ValueChanged ---------------------
		
		private void pnPositionX_ValueChanged( object sender, EventArgs ev )
		{
			if( this.pLocked )
				return;

			this.pCurrentLabel.DPILeft = (int)pnPositionX.Value;
		}

		// ------------------------------------------------------------- pnPositionY_ValueChanged ---------------------
		
		private void pnPositionY_ValueChanged( object sender, EventArgs ev )
		{
			if( this.pLocked )
				return;

			this.pCurrentLabel.DPITop = (int)pnPositionY.Value;
		}

		// ------------------------------------------------------------- pnWidth_ValueChanged -------------------------
		
		private void pnWidth_ValueChanged( object sender, EventArgs ev )
		{
			if( this.pLocked )
				return;

			this.pCurrentLabel.DPIWidth = (int)pnWidth.Value;
		}

		// ------------------------------------------------------------- pnHeight_ValueChanged ------------------------
		
		private void pnHeight_ValueChanged( object sender, EventArgs ev )
		{
			if( this.pLocked )
				return;

			this.pCurrentLabel.DPIHeight = (int)pnHeight.Value;
		}

		// ------------------------------------------------------------- pbBorderColor_Click --------------------------
		
		private void pbBorderColor_Click( object sender, EventArgs ev )
		{
			if( this.pLocked || this.gsColor.ShowDialog(this) != DialogResult.OK )
				return;

			// zmień kolor ramki
			this.pCurrentLabel.BorderColor = gsColor.Color;
			this.ptbBorderColor.Text = gsColor.Color.R.ToString("X2") +
									   gsColor.Color.G.ToString("X2") +
									   gsColor.Color.B.ToString("X2");
			// odśwież kontrolke
			this.pCurrentLabel.Refresh( );
		}

		// ------------------------------------------------------------- pbFontName_Click -----------------------------
		
		private void pbFontName_Click( object sender, EventArgs ev )
		{
			if( this.pLocked || this.gsFont.ShowDialog(this) != DialogResult.OK )
				return;

			this.pCurrentLabel.Font = gsFont.Font;
			this.pCurrentLabel.DPIFontSize = gsFont.Font.SizeInPoints;

			// odśwież dane na temat czcionki...
			this.ptbFontName.Text = this.pCurrentLabel.Font.Name + ", " +
									this.pCurrentLabel.Font.SizeInPoints + "pt " +
									(this.pCurrentLabel.Font.Bold ? "B" : "") +
									(this.pCurrentLabel.Font.Italic ? "I" : "") +
									(this.pCurrentLabel.Font.Strikeout ? "S" : "") +
									(this.pCurrentLabel.Font.Underline ? "U" : "");

			// odśwież kontrolke
			this.pCurrentLabel.Refresh( );
		}

		// ------------------------------------------------------------- pbFontColor_Click ----------------------------
		
		private void pbFontColor_Click( object sender, EventArgs ev )
		{
			if( this.pLocked || this.gsColor.ShowDialog(this) != DialogResult.OK )
				return;

			// zmień kolor czcionki
			this.pCurrentLabel.ForeColor = gsColor.Color;
			this.ptbFontColor.Text		 = gsColor.Color.R.ToString("X2") +
										   gsColor.Color.G.ToString("X2") +
										   gsColor.Color.B.ToString("X2");
			// odśwież kontrolke
			this.pCurrentLabel.Refresh( );
		}

		// ------------------------------------------------------------- pbBackColor_Click ----------------------------
		
		private void pbBackColor_Click( object sender, EventArgs ev )
		{
			if( this.pLocked || gsColor.ShowDialog(this) != DialogResult.OK )
				return;

			// zmień kolor tła
			this.pCurrentLabel.BackColor = gsColor.Color;
			this.ptbBackColor.Text		 = gsColor.Color.R.ToString("X2") +
										   gsColor.Color.G.ToString("X2") +
										   gsColor.Color.B.ToString("X2") +
										   gsColor.Color.A.ToString("X2");
			// odśwież kontrolke
			this.pCurrentLabel.Refresh( );
		}

		// ------------------------------------------------------------- pbBackImage_Click ----------------------------
		
		private void pbBackImage_Click( object sender, EventArgs ev )
		{
			// okno wyboru pliku
			if( this.pLocked || gsImage.ShowDialog(this) != DialogResult.OK )
				return;

			if( this.pCurrentLabel.BackImage != null )
				this.pCurrentLabel.BackImage.Dispose( );

			// zmień obraz pola
			this.pCurrentLabel.BackImage = Image.FromFile(gsImage.FileName);
			this.pCurrentLabel.BackColor = Color.Empty;

			// ustaw ścieżkę do obrazu w polu tekstowym
			this.ptbBackImage.Text = gsImage.FileName;

			// odśwież kontrolke
			this.pCurrentLabel.Refresh( );
		}

		// ------------------------------------------------------------- ptrTextAlign_Scroll --------------------------
		
		private void ptrTextAlign_Scroll( object sender, EventArgs ev )
		{
			if( this.pLocked )
				return;

			this.pCurrentLabel.TextAlignment = this.ptrTextAlign.Value;
		}

		// ------------------------------------------------------------- pnPadding_ValueChanged -----------------------
		
		private void pnPadding_ValueChanged( object sender, EventArgs ev )
		{
			if( this.pLocked )
				return;
			
			this.pCurrentLabel.DPIPadding = (int)pnPadding.Value;

			// odśwież kontrolke
			this.pCurrentLabel.Refresh( );
		}

		// ------------------------------------------------------------- pbPageBack_Click -----------------------------
		
		private void pbPageBack_Click( object sender, EventArgs ev )
		{
			// okno wyboru pliku
			if( gsImage.ShowDialog(this) != DialogResult.OK )
				return;

			if( this.pCurrentPanel.BackgroundImage != null )
				this.pCurrentPanel.BackgroundImage.Dispose( );

			// zmień obraz panelu
			this.pCurrentPanel.BackgroundImage = Image.FromFile( gsImage.FileName );
			this.pCurrentPanel.BackColor = Color.Empty;
		}

		// ------------------------------------------------------------- pbAddPage_Click ------------------------------
		
		private void pbAddPage_Click( object sender, EventArgs ev )
		{
			Panel  panel		= new Panel( );
			double dpi_px_scale	= ((double)this.pnDPI.Value / 100.0) * 3.938095238095238;

			// zablokuj operacje odświeżania
			this.pLocked = true;

			// dostosuj rozmiar strony
			Size pp_size = new Size
			(
				(int)((double)this.pPageSize.Width  * dpi_px_scale),
				(int)((double)this.pPageSize.Height * dpi_px_scale)
			);

			// ustawienia panelu
			panel.BackColor = SystemColors.Window;
			panel.BackgroundImageLayout	= ImageLayout.Stretch;
			panel.ContextMenuStrip = icPage;
			panel.Location = new Point( 0, 0 );
			panel.Margin = new Padding( 0, 0, 0, 0 );
			panel.Size = pp_size;
			panel.Tag = new PageDetails( 0, 0 );

			// zwiększ licznik stron
			++this.pPanelCounter;
			
			// ukryj aktualny panel...
			((Panel)this.ppPanelContainer.Controls[this.pCurrentPanelID]).Visible = false;

			// dodaj panel do kontenera
			this.ppPanelContainer.Controls.Add( panel );

			// ustaw nowe wartości zmiennych
			this.pCurrentPanelID = this.pPanelCounter;
			this.pCurrentPanel   = panel;

			// zwiększ maksimum i ustaw aktualną stronę
			this.pnPage.Maximum = this.pPanelCounter + 1;
			this.pnPage.Value	= this.pPanelCounter + 1;

			// zablokuj pola
			this.LockFieldLabels( );

			// odblokuj odświeżanie
			this.pLocked = false;
		}

		// ------------------------------------------------------------- pbSave_Click ---------------------------------
		
		private void pbSave_Click( object sender, EventArgs ev )
		{
			this.Cursor = Cursors.WaitCursor;

			// usuń stary plik konfiguracyjny
			File.Delete( "patterns/" + this.mCurrentName + "/config.cfg" );

			// usuń zapisane obrazy
			Directory.EnumerateFiles( "patterns/" + this.mCurrentName + "/images" ).ToList().ForEach( File.Delete );
			
			// usuń podglądy stron
			int x = 0;
			while( true )
			{
				if( File.Exists("patterns/" + this.mCurrentName + "/preview" + x + ".jpg") )
					File.Delete( "patterns/" + this.mCurrentName + "/preview" + x + ".jpg" );
				else
					break;
				++x;
			}

			// zapisz konfiguracje, obrazy i podglądy stron
			PatternEditor.Save( this.mCurrentName, this.pPageSize, this.ppPanelContainer );
			PatternEditor.DrawPreview( this.mCurrentName, this.pPageSize, this.ppPanelContainer, 1.0 );

			this.Cursor = null;
			this.gEditChanged = true;
		}

		// ------------------------------------------------------------- icpPageColor_Click ---------------------------
		
		private void icpPageColor_Click( object sender, EventArgs ev )
		{
			if( gsColor.ShowDialog(this) != DialogResult.OK )
				return;

			this.pCurrentPanel.BackgroundImage = null;
			this.pCurrentPanel.BackColor = gsColor.Color;
		}

		// ------------------------------------------------------------- icpPageClear_Click ---------------------------
		
		private void icpPageClear_Click( object sender, EventArgs ev )
		{
			if( this.pCurrentPanel.BackgroundImage != null )
				this.pCurrentPanel.BackgroundImage.Dispose( );

			this.pCurrentPanel.BackgroundImage = null;
			this.pCurrentPanel.BackColor = SystemColors.Window;
		}

		// ------------------------------------------------------------- icpPrintColor_CheckedChanged -----------------
		
		private void icpPrintColor_CheckedChanged( object sender, EventArgs ev )
		{
			PageDetails tag = (PageDetails)this.pCurrentPanel.Tag;
			tag.PrintColor = icpPrintColor.Checked
				? 1
				: 0;
			this.pCurrentPanel.Tag = tag;
		}

		// ------------------------------------------------------------- icpPrintImage_CheckedChanged -----------------
		
		private void icpPrintImage_CheckedChanged( object sender, EventArgs ev )
		{
			PageDetails tag = (PageDetails)this.pCurrentPanel.Tag;
			tag.PrintImage = icpPrintImage.Checked
				? 1
				: 0;
			this.pCurrentPanel.Tag = tag;
		}

		// ------------------------------------------------------------- icpDeletePage_Click --------------------------
		
		private void icpDeletePage_Click( object sender, EventArgs ev )
		{
			// nie można usunąć strony gdy jest tylko jedna
			if( this.pPanelCounter < 1 )
			{
				MessageBox.Show( "Masz tylko jedną stronę. Nie możesz jej usunąć..." );
				return;
			}

			// usuń stronę i zmniejsz licznik stron
			--this.pPanelCounter;
			this.ppPanelContainer.Controls.Remove( this.pCurrentPanel );
			this.pCurrentPanel.Visible = false;

			// aktualizuj identyfikator
			this.pCurrentPanelID = this.pCurrentPanelID > this.pPanelCounter
				? this.pPanelCounter
				: this.pCurrentPanelID < 1
				? 0
				: this.pCurrentPanelID - 1;

			// ustaw nowe wartości zmiennych i pokaż stronę
			this.pCurrentPanel = (Panel)this.ppPanelContainer.Controls[this.pCurrentPanelID];
			this.pCurrentPanel.Visible = true;
			this.pnPage.Value = this.pCurrentPanelID + 1;
			this.pnPage.Maximum = this.pPanelCounter + 1;
			
			// zablokuj pola
			this.LockFieldLabels( );
		}

		// ------------------------------------------------------------- icpDeleteLabels_Click ------------------------
		
		private void icpDeleteLabels_Click( object sender, EventArgs ev )
		{
			this.pCurrentPanel.Controls.Clear( );
			this.LockFieldLabels( );
		}

		// ------------------------------------------------------------- icpPage_Opening ------------------------------
		
		private void icPage_Opening( object sender, CancelEventArgs ev )
		{
			PageDetails tag = (PageDetails)this.pCurrentPanel.Tag;

			this.icpPrintColor.Checked = tag.PrintColor == 1;
			this.icpPrintImage.Checked = tag.PrintImage == 1;
		}

		// ------------------------------------------------------------- icpLabel_Opening -----------------------------
		
		private void icLabel_Opening( object sender, CancelEventArgs ev )
		{
			this.plLabel_Click( this.icLabel.SourceControl, ev );
			CustomLabelDetails tag = (CustomLabelDetails)this.pCurrentLabel.Tag;

			// zaznacz lub odznacz elementy
			this.iclBackFromDb.Checked = tag.ImageFromDB;
			this.iclPrintColor.Checked = tag.PrintColor;
			this.iclPrintImage.Checked = tag.PrintImage;
			this.iclTextFromDb.Checked = tag.TextFromDB;
			this.iclPrintText.Checked = tag.PrintText;
			this.iclPrintBorder.Checked = tag.PrintBorder;
		}

		// ------------------------------------------------------------- iclClearBack_Click ---------------------------
		
		private void iclClearBack_Click( object sender, EventArgs ev )
		{
			this.pCurrentLabel.BackImage = null;
			this.pCurrentLabel.BackColor = Color.Transparent;
		}

		// ------------------------------------------------------------- iclDeleteField_Click -------------------------
		
		private void iclDeleteField_Click( object sender, EventArgs ev )
		{
			this.pCurrentPanel.Controls.Remove( this.pCurrentLabel );
			this.LockFieldLabels( );
		}

		// ------------------------------------------------------------- iclDeleteField_Click -------------------------
		
		private void iclBackFromDb_Click( object sender, EventArgs ev )
		{
			bool is_check = this.iclBackFromDb.Checked;
			CustomLabelDetails tag = (CustomLabelDetails)this.pCurrentLabel.Tag;

			tag.ImageFromDB = is_check;
			tag.PrintColor = false;
			tag.PrintImage = false;

			this.iclPrintColor.Checked = false;
			this.iclPrintImage.Checked = false;

			this.pCurrentLabel.Tag = tag;
		}

		// ------------------------------------------------------------- iclPrintColor_Click --------------------------
		
		private void iclPrintColor_Click( object sender, EventArgs ev )
		{
			bool is_check = this.iclPrintColor.Checked;
			CustomLabelDetails tag = (CustomLabelDetails)this.pCurrentLabel.Tag;

			tag.ImageFromDB = false;
			tag.PrintColor = is_check;

			this.iclBackFromDb.Checked = false;
			this.pCurrentLabel.Tag = tag;
		}

		// ------------------------------------------------------------- iclPrintImage_Click --------------------------
		
		private void iclPrintImage_Click( object sender, EventArgs ev )
		{
			bool is_check = this.iclPrintImage.Checked;
			CustomLabelDetails tag = (CustomLabelDetails)this.pCurrentLabel.Tag;

			tag.ImageFromDB = false;
			tag.PrintImage = is_check;

			this.iclBackFromDb.Checked = false;
			this.pCurrentLabel.Tag = tag;
		}

		// ------------------------------------------------------------- iclTextFromDb_Click --------------------------
		
		private void iclTextFromDb_Click( object sender, EventArgs ev )
		{
			bool is_check = this.iclTextFromDb.Checked;
			CustomLabelDetails tag = (CustomLabelDetails)this.pCurrentLabel.Tag;

			tag.TextFromDB = is_check;
			tag.PrintText = false;

			this.iclPrintText.Checked = false;
			this.pCurrentLabel.Tag = tag;
		}

		// ------------------------------------------------------------- iclPrintText_Click ---------------------------
		
		private void iclPrintText_Click(object sender, EventArgs e)
		{
			bool is_check = this.iclPrintText.Checked;
			CustomLabelDetails tag = (CustomLabelDetails)this.pCurrentLabel.Tag;

			tag.TextFromDB = false;
			tag.PrintText = is_check;

			this.iclTextFromDb.Checked = false;
			this.pCurrentLabel.Tag = tag;
		}

		// ------------------------------------------------------------- iclPrintText_Click ---------------------------
		
		private void iclPrintBorder_Click(object sender, EventArgs e)
		{
			CustomLabelDetails tag = (CustomLabelDetails)this.pCurrentLabel.Tag;

			tag.PrintText = this.iclPrintBorder.Checked;
			this.pCurrentLabel.Tag = tag;
		}

		// ------------------------------------------------------------- pnDPI_ValueChanged ---------------------------
		
		private void pnDPI_ValueChanged( object sender, EventArgs ev )
		{
			double dpi_scale = ((double)this.pnDPI.Value / 100.0);
			double dpi_px_scale	= dpi_scale * 3.938095238095238;

			// dostosuj rozmiar strony
			Size pp_size = new Size
			(
				(int)((double)this.pPageSize.Width * dpi_px_scale),
				(int)((double)this.pPageSize.Height * dpi_px_scale)
			);
			this.pCurrentPanel.Size = pp_size;

			// aktualizuj rozmiary kontrolek
			for( int x = 0; x < this.pCurrentPanel.Controls.Count; ++x )
				((CDField)this.pCurrentPanel.Controls[x]).DPIScale = dpi_scale;
		}

		// ------------------------------------------------------------- pnPage_ValueChanged --------------------------
		
		private void pnPage_ValueChanged( object sender, EventArgs ev )
		{
			if( this.pLocked )
				return;

			this.pCurrentPanel.Visible = false;
			this.pCurrentPanelID = (int)this.pnPage.Value - 1;
			this.pCurrentPanel = (Panel)this.ppPanelContainer.Controls[this.pCurrentPanelID];

			// zmień skalę DPI w stronie i kontrolkach
			double dpi_scale = ((double)this.pnDPI.Value / 100.0);
			double dpi_px_scale	= dpi_scale * 3.938095238095238;

			// dostosuj rozmiar strony
			Size pp_size = new Size
			(
				(int)((double)this.pPageSize.Width * dpi_px_scale),
				(int)((double)this.pPageSize.Height * dpi_px_scale)
			);
			this.pCurrentPanel.Size = pp_size;

			// aktualizuj rozmiary kontrolek
			for( int x = 0; x < this.pCurrentPanel.Controls.Count; ++x )
				((CDField)this.pCurrentPanel.Controls[x]).DPIScale = dpi_scale;

			// pokaż panel (strone)
			this.pCurrentPanel.Visible	= true;

			// zablokuj pola
			this.LockFieldLabels( );
		}







		private void mbDelete_MouseEnter(object sender, EventArgs e)
		{
			this.mlStatus.Text = "Usuwa wybrany z listy wzór.";
		}

		private void mbNew_MouseEnter(object sender, EventArgs e)
		{
			this.mlStatus.Text = "Otwiera okno z kreatorem nowego wzoru.";
		}

		private void mlStatus_ClearText(object sender, EventArgs e)
		{
			this.mlStatus.Text = "";
		}
	}
}
