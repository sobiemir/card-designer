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

		private Panel		pCurrentPage	= null;
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



		private PatternData gPatternData    = new PatternData();

		private DataContent dDataContent    = new DataContent();
		private string		dCurrentName    = null;

		private PageField   pCurrentField   = null;

/* ===============================================================================================================================
 * Główne funkcje klasy:
 * - Main					[Public]
 * - ProcessCmdKey			[Protected]
 * - RefreshProjectList		[Private]
 * - LockFieldLabels		[Private]
 * - UnlockFieldLabels		[Private]
 * - FillFieldLabels		[Private]
 * =============================================================================================================================== */

#region Główne funkcje klasy

		// ------------------------------------------------------------- Main -----------------------------------------
		
		public Main( )
		{
			this.InitializeComponent();

			// utwórz folder gdy nie istnieje
			if( !Directory.Exists("patterns") )
				Directory.CreateDirectory( "patterns" );

			// odśwież listę wzorów
			this.RefreshProjectList();
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
		}

		// ------------------------------------------------------------- UnlockFieldLabels ----------------------------
		
		private void UnlockFieldLabels( )
		{
			this.pbBorderColor.Enabled = true;
			this.pbFontName.Enabled    = true;
			this.pbFontColor.Enabled   = true;
			this.pbBackImage.Enabled   = true;
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
		}

		// ------------------------------------------------------------- FillFieldLabels ------------------------------
		
		private void FillFieldLabels( )
		{
			this.pLocked = true;

			// zmień nazwę
			this.ptbName.Text = this.pCurrentField.Text;

			// aktualizuj pola numeryczne
			this.pnPositionX.Value  = this.pCurrentField.DPIBounds.X;
			this.pnPositionY.Value  = this.pCurrentField.DPIBounds.Y;
			this.pnHeight.Value     = this.pCurrentField.DPIBounds.Height;
			this.pnWidth.Value      = this.pCurrentField.DPIBounds.Width;
			this.pnBorderSize.Value = this.pCurrentField.DPIBorderSize;
			this.pnPadding.Value    = this.pCurrentField.DPIPadding;

			string color_r, color_g, color_b;

			// aktualizuj kolory...
			color_r = this.pCurrentField.BorderColor.R.ToString("X2");
			color_g = this.pCurrentField.BorderColor.G.ToString("X2");
			color_b = this.pCurrentField.BorderColor.B.ToString("X2");
			this.ptbBorderColor.Text = color_r + color_b + color_g;

			color_r = this.pCurrentField.BackColor.R.ToString("X2");
			color_g = this.pCurrentField.BackColor.G.ToString("X2");
			color_b = this.pCurrentField.BackColor.B.ToString("X2");
			this.ptbBackColor.Text = color_r + color_g + color_b;

			color_r = this.pCurrentField.ForeColor.R.ToString("X2");
			color_g = this.pCurrentField.ForeColor.G.ToString("X2");
			color_b = this.pCurrentField.ForeColor.B.ToString("X2");
			this.ptbFontColor.Text = color_r + color_g + color_b;

			string bold, italic, strikeout, underline, font_name, font_size;

			// czcionka...	
			bold      = this.pCurrentField.Font.Bold ? "B" : "";
			italic    = this.pCurrentField.Font.Italic ? "I" : "";
			underline = this.pCurrentField.Font.Underline ? "U" : "";
			strikeout = this.pCurrentField.Font.Strikeout ? "S" : "";
			font_name = this.pCurrentField.Font.Name;
			font_size = this.pCurrentField.Font.SizeInPoints.ToString( );
			this.ptbFontName.Text = font_name + ", " + font_size + "pt " + bold + italic + underline + strikeout;

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
			switch( (int)((FieldData)this.pCurrentField.Tag).pos_align )
			{
				case 0x001: this.pcbPosAlign.SelectedIndex = 0; break;
				case 0x004: this.pcbPosAlign.SelectedIndex = 1; break;
				case 0x100: this.pcbPosAlign.SelectedIndex = 2; break;
				case 0x400: this.pcbPosAlign.SelectedIndex = 4; break;
			}

			// nazwa / ścieżka obrazu
			this.ptbBackImage.Text = this.pCurrentField.BackImagePath != null ? this.pCurrentField.BackImagePath : "";

			this.pLocked = false;
		}

#endregion

/* ===============================================================================================================================
 * Menu okna:
 * - ispNew_Click		[Menu/Button/Button]
 * - ispClose_Click		[Menu/Button/Button]
 * - isHome_Click		[Menu/Button]
 * - isPattern_Click	[Menu/Button]
 * - isData_Click		[Menu/Button]
 * =============================================================================================================================== */

#region Menu okna

		// ------------------------------------------------------------- ispNew_Click ---------------------------------
		
		private void ispNew_Click( object sender, EventArgs ev )
		{
			NewPattern window = new NewPattern( );

			// nowy wzór
			if( window.ShowDialog(this) != DialogResult.OK )
				return;

			// odśwież listę
			this.RefreshProjectList( );
			string pattern_name = window.PatternName;

			// zaznacz nowy wzór
			foreach( TreeNode node in this.mtvPatterns.Nodes )
				if( node.Text == pattern_name )
				{
					this.mtvPatterns.SelectedNode = node;
					break;
				}
			
			// przejdź do edycji wzoru
			this.ictEdit_Click( null, null );
		}

		// ------------------------------------------------------------- ispClose_Click -------------------------------
		
		private void ispClose_Click( object sender, EventArgs ev )
		{
			// zakończ działanie programu
			Application.Exit();
		}

		// ------------------------------------------------------------- isHome_Click ---------------------------------
		
		private void isHome_Click( object sender, EventArgs ev )
		{
			this.tHomePanel.Show();
			this.isHome.Enabled = false;

			if( this.gThisContainer == 2 )
			{
				this.tPatternPanel.Hide();
				this.isPattern.Enabled = true;
			}
			else if( this.gThisContainer == 3 )
			{
				this.tDataPanel.Hide();
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
			this.tPatternPanel.Show();
			this.isPattern.Enabled = false;

			if( this.gThisContainer == 1 )
			{
				this.tHomePanel.Hide();
				this.isHome.Enabled = true;
			}
			else if( this.gThisContainer == 3 )
			{
				this.tDataPanel.Hide();
				this.isData.Enabled = true;
			}

			this.gThisContainer = 2;
		}

		// ------------------------------------------------------------- isData_Click ---------------------------------
		
		private void isData_Click( object sender, EventArgs ev )
		{
			this.tDataPanel.Show();
			this.isData.Enabled = false;

			if( this.gThisContainer == 1 )
			{
				this.tHomePanel.Hide();
				this.isHome.Enabled = true;
			}
			else if( this.gThisContainer == 2 )
			{
				this.tPatternPanel.Hide();
				this.isPattern.Enabled = true;
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

			// wczytaj nagłówek
			PatternData data = PatternEditor.ReadPattern( pattern, true );
			int format = -1;
			
			// treść dynamiczna
			this.mSelectedData = data.dynamic;
			this.gPatternData  = data;

			// wykryj format wzoru
			int[,] format_dims = NewPattern.FormatDims;

			for( int x = 0; x < format_dims.GetLength(0); ++x )
				if( format_dims[x,0] == data.size.Width && format_dims[x,1] == data.size.Height )
				{
					format = x;
					break;
				}

			// ustaw wartości pola numerycznego
			this.mnPage.Maximum = data.pages;
			this.mnPage.Value = 1;

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
				this.Cursor  = null;

				// aktualizuj status
				if( format == -1 )
					this.mlStatus.Text = "Wzór " + pattern + ". Format własny: " + data.size.Width + " x " + data.size.Height + " mm. Ilość stron: " + data.pages + ".";
				else
				{
					string[] format_names = NewPattern.FormatNames;
					this.mlStatus.Text = "Wzór " + pattern + ". Format " + format_names[format] + ": " + data.size.Width + " x " + data.size.Height + " mm. Ilość stron: " + data.pages + ".";
				}

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
				this.mlStatus.Text = "Wzór " + pattern + ". Format własny: " + data.size.Width + " x " + data.size.Height + " mm. Brak podglądu.";
			else
			{
				string[] format_names = NewPattern.FormatNames;
				this.mlStatus.Text = "Wzór " + pattern + ". Format " + format_names[format] + ": " + data.size.Width + " x " + data.size.Height + " mm. Brak podglądu.";
			}
			this.Cursor = null;
		}

		// ------------------------------------------------------------- icPattern_Opening ----------------------------
		
		private void icPattern_Opening( object sender, CancelEventArgs ev )
		{
			// odblokuj wszystkie przyciski w menu
			this.ictNew.Enabled      = true;
			this.ictEdit.Enabled     = true;
			this.ictLoadData.Enabled = true;
			this.ictImport.Enabled   = true;
			this.ictExport.Enabled   = true;
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
			this.pLocked = true;

			// przejście do edycji wzoru
			this.isPattern_Click( null, null );
			string pattern = this.mSelectedName;

			// zapisz indeks i nazwę aktualnego wzoru
			this.mCurrentPattern = this.mSelectedID;
			this.mCurrentName    = pattern;

			// wczytaj wzór
			// @TODO - opcje domyślnego rozmiaru (skali)
			this.pcbScale.SelectedIndex = 2;
			PatternData pattern_data = PatternEditor.ReadPattern( pattern );
			PatternEditor.DrawPreview( pattern_data, this.ppPanelContainer, 1.0 );

			// ilość stron
			this.pPanelCounter  = pattern_data.pages - 1;
			this.pnPage.Maximum = pattern_data.pages;
			this.pnPage.Value   = 1;

			// aktualna strona
			this.pCurrentPage   = (Panel)this.ppPanelContainer.Controls[0];
			this.pCurrentPanelID = 0;

			Panel     page;
			PageField field;

			// dodaj akcje do stron i pól
			for( int x = 0; x < this.ppPanelContainer.Controls.Count; ++x )
			{
				page = (Panel)this.ppPanelContainer.Controls[x];
				page.ContextMenuStrip = this.icPage;
				page.MouseDown += new MouseEventHandler( this.ppPanelContainer_MouseDown );

				for( int y = 0; y < page.Controls.Count; ++y )
				{
					field = (PageField)page.Controls[y];
					field.ContextMenuStrip = this.icLabel;
					field.Click += new EventHandler( this.plLabel_Click );
				}
			}

			// zablokuj pola
			this.LockFieldLabels();

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
				this.mCurrentName    = null;

				// wyłącz możliwość edycji nieistniejącego już wzoru
				this.isPattern.Enabled = false;
			}
			// brak wzoru, brak obrazka...
			this.mibPreview.Image = null;
			this.mSelectedID      = -1;

			// usuń pliki wzoru i odśwież liste
			Directory.Delete( "patterns/" + pattern, true );
			this.RefreshProjectList();
		}

		// ------------------------------------------------------------- ictLoadData_Click ----------------------------
		
		private void ictLoadData_Click( object sender, EventArgs ev )
		{
			// wybór pliku
			if( this.gsDBase.ShowDialog(this) != DialogResult.OK )
				return;

			// odczytaj dane
			this.gPatternData = PatternEditor.ReadPattern( this.gPatternData.name );
			DataReader reader = new DataReader( this.gPatternData, this.gsDBase.FileName );

			if( reader.ShowDialog(this) != DialogResult.OK )
				return;
			 
			// załaduj cały plik
			reader.ReloadData();
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
			
			// przejdź na strone z danymi
			this.dCurrentName = this.mtvPatterns.SelectedNode.Text;
			this.isData_Click( null, null );
		}
#endregion

/* ===============================================================================================================================
 * Strona główna - Inne:
 * - mnPage_ValueChanged	[NumericUpDown]
 * - mpPreview_Resize		[Panel]
 * - mpPreview_MouseDown	[Panel]
 * - plLabel_Click          [Label]
 * =============================================================================================================================== */

#region Strona główna - Inne

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

		// ------------------------------------------------------------- mpPreview_MouseDown -------------------------

		private void mpPreview_MouseDown( object sender, MouseEventArgs ev )
		{
			this.mpPreview.Focus();
		}

		// ------------------------------------------------------------- plLabel_Click --------------------------------
		
		private void plLabel_Click( object sender, EventArgs ev )
		{
			// aktualny obiekt
			this.pCurrentField = (PageField)sender;
			
			// zablokuj zmiane wartości
			this.pLocked = true;

			// wypełnij pola i odblokuj je
			this.FillFieldLabels();
			this.UnlockFieldLabels();

			// odblokuj zmiane wartości
			this.pLocked = false;
		}

#endregion

/* ===============================================================================================================================
 * Edycja wzoru - Inne:
 * - pcbScale_SelectedIndexChanged	[ComboBox]
 * - pcbScale_Leave					[ComboBox]
 * - pcbScale_KeyDown				[ComboBox]
 * - pnPage_ValueChanged			[NumericUpDown]
 * - ppPanelContainer_MouseDown		[Panel]
 * =============================================================================================================================== */

#region Edycja wzoru - Inne

		// ------------------------------------------------------------- pcbScale_SelectedIndexChanged ----------------
		
		private void pcbScale_SelectedIndexChanged( object sender, EventArgs ev )
		{
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
			
			// zmień skale wzoru
			PatternEditor.ChangeScale( this.gPatternData, this.ppPanelContainer, (double)scale / 100.0 );
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
			((Panel)this.ppPanelContainer.Controls[(int)pnPage.Value - 1]).Show();
			this.pCurrentPage.Hide();
			this.pCurrentPage = (Panel)this.ppPanelContainer.Controls[(int)pnPage.Value - 1];
		}

		// ------------------------------------------------------------- ppPanelContainer_MouseDown -------------------
		
		private void ppPanelContainer_MouseDown( object sender, MouseEventArgs ev )
		{
			this.ppPanelContainer.Focus();
		}

#endregion

/* ===============================================================================================================================
 * Edycja wzoru - Menu strony:
 * - ptbName_TextChanged		        [TextBox]
 * - pnPositionX_ValueChanged           [NumericUpDown]
 * - pnPositionY_ValueChanged           [NumericUpDown]
 * - pnWidth_ValueChanged               [NumericUpDown]
 * - pnHeight_ValueChanged              [NumericUpDown]
 * - pbBorderColor_Click                [Button]
 * - pbBackColor_Click                  [Button]
 * - pbBackImage_Click                  [Button]
 * - pbFontName_Click                   [Button]
 * - pbFontColor_Click                  [Button]
 * - pcbTextAlign_SelectedIndexChanged  [ComboBox]
 * - pnPadding_ValueChanged             [NumericUpDown]
 * - pcbPosAlign_SelectedIndexChanged   [ComboBox]
 * - pnBorderSize_ValueChanged          [NumericUpDown]
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

			Rectangle new_posx = this.pCurrentField.DPIBounds;
			new_posx.X = (int)pnPositionX.Value;
			this.pCurrentField.DPIBounds = new_posx;
		}

		// ------------------------------------------------------------- pnPositionY_ValueChanged ---------------------
		
		private void pnPositionY_ValueChanged( object sender, EventArgs ev )
		{
			if( this.pLocked )
				return;

			Rectangle new_posy = this.pCurrentField.DPIBounds;
			new_posy.Y = (int)pnPositionY.Value;
			this.pCurrentField.DPIBounds = new_posy;
		}

		// ------------------------------------------------------------- pnWidth_ValueChanged -------------------------
		
		private void pnWidth_ValueChanged( object sender, EventArgs ev )
		{
			if( this.pLocked )
				return;

			Rectangle new_width = this.pCurrentField.DPIBounds;
			new_width.Width = (int)pnWidth.Value;
			this.pCurrentField.DPIBounds = new_width;
		}

		// ------------------------------------------------------------- pnHeight_ValueChanged ------------------------
		
		private void pnHeight_ValueChanged( object sender, EventArgs ev )
		{
			if( this.pLocked )
				return;

			Rectangle new_height = this.pCurrentField.DPIBounds;
			new_height.Height = (int)pnHeight.Value;
			this.pCurrentField.DPIBounds = new_height;
		}

		// ------------------------------------------------------------- pbBorderColor_Click --------------------------
		
		private void pbBorderColor_Click( object sender, EventArgs ev )
		{
			if( this.pLocked || this.gsColor.ShowDialog(this) != DialogResult.OK )
				return;

			Color color = this.gsColor.Color;

			// zmień kolor ramki
			this.pCurrentField.BorderColor = color;
			this.ptbBorderColor.Text = color.R.ToString("X2") + color.G.ToString("X2") + color.B.ToString("X2");

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
			this.ptbBackColor.Text       = color.R.ToString("X2") + color.G.ToString("X2") + color.B.ToString("X2");
			
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
			this.pCurrentField.BackImage = Image.FromFile(this.gsImage.FileName);
			this.pCurrentField.BackColor = Color.Transparent;

			// ustaw ścieżkę do obrazu w polu tekstowym
			this.ptbBackImage.Text = this.gsImage.FileName;

			// odśwież kontrolke
			this.pCurrentField.Refresh();
		}

		// ------------------------------------------------------------- pbFontName_Click -----------------------------
		
		private void pbFontName_Click( object sender, EventArgs ev )
		{
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
			this.ptbFontColor.Text       = color.R.ToString("X2") + color.G.ToString("X2") + color.B.ToString("X2");

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
			
			this.pCurrentField.DPIPadding = (int)this.pnPadding.Value;
		}

		// ------------------------------------------------------------- pcbPosAlign_SelectedIndexChanged -------------
		
		private void pcbPosAlign_SelectedIndexChanged( object sender, EventArgs ev )
		{
			if( this.pLocked )
				return;
		}

		// ------------------------------------------------------------- pnBorderSize_ValueChanged --------------------
		
		private void pnBorderSize_ValueChanged( object sender, EventArgs ev )
		{
			if( this.pLocked )
				return;

			this.pCurrentField.DPIBorderSize = (int)this.pnBorderSize.Value;

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
			PageData tag = (PageData)this.pCurrentPage.Tag;

			this.icpPrintColor.Checked = tag.extra.print_color;
			this.icpPrintImage.Checked = tag.extra.print_image;
		}

		// ------------------------------------------------------------- pbAddLabel_Click -----------------------------
		
		private void icpAddField_Click( object sender, EventArgs ev )
		{
			PageField field      = new PageField();
			FieldData field_data = new FieldData();

			// utwórz pole
			field.BorderColor   = Color.Black;
			field.DPIBorderSize = 1;
			field.DPIBounds     = new Rectangle( 0, 0, 65, 10 );
			field.ForeColor     = Color.Black;
			field.Text          = "nowe pole";
			field.TextAlign     = ContentAlignment.MiddleCenter;
			field.BackColor     = Color.Transparent;
			field.DPIFontSize   = 8.25;
			field.DPIPadding    = 1;
			field.DPIScale      = (double)Convert.ToInt32( this.pcbScale.Text ) / 100.0;
			field.Margin        = new Padding(0);
			field.Padding       = new Padding(0);
			field.Location      = new Point( 0, 0 );

			// przypisz zdarzenia
			field.ContextMenuStrip = this.icLabel;
			field.Click           += new EventHandler( this.plLabel_Click );

			// wypełnij strukture danych
			field_data.border_color = Color.Black;
			field_data.border_size  = 1;
			field_data.bounds       = field.Bounds;
			field_data.color        = Color.Transparent;
			field_data.extra        = new FieldExtraData();
			field_data.font_color   = Color.Black;
			field_data.font_name    = field.Font.Name;
			field_data.font_size    = (float)field.DPIFontSize;
			field_data.font_style   = field.Font.Style;
			field_data.image        = false;
			field_data.image_path   = "";
			field_data.name         = "nowe pole";
			field_data.padding      = new Padding(field.DPIPadding);
			field_data.pos_align    = ContentAlignment.TopLeft;
			field_data.text_align   = field.TextAlign;

			field.Tag = field_data;

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
			this.pCurrentPage.BackColor = this.gsColor.Color;
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
			((PageData)this.pCurrentPage.Tag).image_path = this.gsImage.FileName;
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
			PageData tag = (PageData)this.pCurrentPage.Tag;
			tag.extra.print_color = icpPrintColor.Checked;
			this.pCurrentPage.Tag = tag;
		}

		// ------------------------------------------------------------- icpPrintImage_CheckedChanged -----------------
		
		private void icpPrintImage_CheckedChanged( object sender, EventArgs ev )
		{
			PageData tag = (PageData)this.pCurrentPage.Tag;
			tag.extra.print_image = icpPrintImage.Checked;
			this.pCurrentPage.Tag = tag;
		}

		// ------------------------------------------------------------- icpAddPage_Click -----------------------------
		
		private void icpAddPage_Click( object sender, EventArgs ev )
		{
			Panel  page    = new Panel( );
			double dpi_pxs = (double)Convert.ToInt32( this.pcbScale.Text ) / 100.0;

			// zablokuj operacje odświeżania
			this.pLocked = true;

			// dostosuj rozmiar strony
			Size pp_size = new Size
			(
				(int)((double)this.pPageSize.Width  * dpi_pxs),
				(int)((double)this.pPageSize.Height * dpi_pxs)
			);

			// ustawienia panelu
			page.BackColor              = SystemColors.Window;
			page.BackgroundImageLayout	= ImageLayout.Stretch;

			page.Location = new Point( 0, 0 );
			page.Margin   = new Padding( 0, 0, 0, 0 );
			page.Size     = pp_size;

			PageData page_data = new PageData();

			page_data.color      = Color.Transparent;
			page_data.extra      = new PageExtraData();
			page_data.field      = new FieldData[1];
			page_data.fields     = 0;
			page_data.image      = false;
			page_data.image_path = "";

			page.Tag = page_data;

			page.ContextMenuStrip = icPage;
			page.MouseDown       += new MouseEventHandler( this.ppPanelContainer_MouseDown );

			// zwiększ licznik stron
			++this.pPanelCounter;
			
			// ukryj aktualny panel...
			((Panel)this.ppPanelContainer.Controls[this.pCurrentPanelID]).Visible = false;

			// dodaj panel do kontenera
			this.ppPanelContainer.Controls.Add( page );

			// ustaw nowe wartości zmiennych
			this.pCurrentPanelID = this.pPanelCounter;
			this.pCurrentPage    = page;

			// zwiększ maksimum i ustaw aktualną stronę
			this.pnPage.Maximum = this.pPanelCounter + 1;
			this.pnPage.Value	= this.pPanelCounter + 1;

			// zablokuj pola
			this.LockFieldLabels();

			// odblokuj odświeżanie
			this.pLocked = false;
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

			this.pLocked = true;

			// zmniejsz licznik stron
			--this.pPanelCounter;

			// wyczyść stronę
			this.icpDeleteFields_Click( null, null );
			this.icpPageClear_Click( null, null );

			// usuń stronę
			this.ppPanelContainer.Controls.RemoveAt( this.pCurrentPanelID );

			// aktualizuj identyfikator
			this.pCurrentPanelID = this.pCurrentPanelID > this.pPanelCounter
				? this.pPanelCounter
				: this.pCurrentPanelID < 1
				? 0
				: this.pCurrentPanelID - 1;

			// ustaw nowe wartości zmiennych i pokaż stronę
			this.pCurrentPage = (Panel)this.ppPanelContainer.Controls[this.pCurrentPanelID];
			this.pCurrentPage.Visible = true;
			this.pnPage.Value = this.pCurrentPanelID + 1;
			this.pnPage.Maximum = this.pPanelCounter + 1;
			
			// zablokuj pola
			this.LockFieldLabels();

			this.pLocked = false;
		}

#endregion







		// ------------------------------------------------------------- pbSave_Click ---------------------------------
		
		private void pbSave_Click( object sender, EventArgs ev )
		{
			// @TEST !!

			//File.Delete( "patterns/" + this.mCurrentName + "/config.cfg" );

			Directory.EnumerateFiles( "patterns/" + this.mCurrentName + "/images" ).ToList().ForEach( File.Delete );
			
			int x = 0;
			while( true )
			{
				if( File.Exists("patterns/" + this.mCurrentName + "/preview" + x + ".jpg") )
					File.Delete( "patterns/" + this.mCurrentName + "/preview" + x + ".jpg" );
				else
					break;
				++x;
			}

			PatternEditor.GeneratePreview( this.gPatternData, this.ppPanelContainer, 1.0 );

			/*
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
			 */
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
			this.pCurrentPage.Controls.Remove( this.pCurrentLabel );
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








		private bool dSketchDrawed = false;


		// ------------------------------------------------------------- dtvData_AfterSelect --------------------------
		
		private void dtvData_AfterSelect( object sender, TreeViewEventArgs ev )
		{
			if( this.dtvData.SelectedNode == null )
				return;

			this.pLocked = true;

			if( !this.dSketchDrawed )
			{
				PatternEditor.DrawSketch( this.gPatternData, this.dpPreview, 1.0 /*(double)this.cbScale.SelectedText / 100.0*/ );
				this.dSketchDrawed = true;
			}

			PatternEditor.DrawRow( this.dpPreview, this.dDataContent, ev.Node.Index );

			this.pLocked = false;
		}
	}
}
