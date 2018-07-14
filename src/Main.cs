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


// @TODO dodać do konfiguracji nowe pole z informacją o danych
// @TODO jednak przerobić na struktury....


namespace CDesigner
{
	public partial class Main : Form
	{
		private Panel		pCurrentPage	= null;
		private int			pPanelCounter	= 0;
		private int			pCurrentPanelID	= -1;
		private bool		pLocked			= false;

		private int			gThisContainer	= 1;
		private bool		gEditChanged	= false;

		private int			mSelectedID		= -1;
		private bool		mSelectedError	= false;
		private bool		mSelectedData	= false;
		private string		mSelectedName	= null;
		private bool		mNoImage		= false;


		private PageField   pCurrentField   = null;
		private string      pCurrentName    = null;
		private PatternData pPatternData    = null;

		private PatternData dPatternData    = new PatternData();
		private DataContent dDataContent    = new DataContent();
		private bool        dSketchDrawed   = false;


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
				break;
				// zmień strone wzoru
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

			this.pcxImageSet.Enabled   = false;
			this.pcbDrawColor.Enabled  = false;
			this.pcbDynImage.Enabled   = false;
			this.pcbDynText.Enabled    = false;
			this.pcbShowFrame.Enabled  = false;
			this.pcbStatImage.Enabled  = false;
			this.pcbStatText.Enabled   = false;

			this.pcbDrawFrameOutside.Enabled = false;
			this.pcbUseImageMargin.Enabled   = false;
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

			this.pcxImageSet.Enabled   = true;
			this.pcbDrawColor.Enabled  = true;
			this.pcbDynImage.Enabled   = true;
			this.pcbDynText.Enabled    = true;
			this.pcbShowFrame.Enabled  = true;
			this.pcbStatImage.Enabled  = true;
			this.pcbStatText.Enabled   = true;

			this.pcbDrawFrameOutside.Enabled = true;
			this.pcbUseImageMargin.Enabled   = true;
		}

		// ------------------------------------------------------------- FillFieldLabels ------------------------------
		
		private void FillFieldLabels( )
		{
			this.pLocked = true;

			// zmień nazwę
			this.ptbName.Text = this.pCurrentField.Text;

			// aktualizuj pola numeryczne
			this.pnPositionX.Value  = (decimal)this.pCurrentField.DPIBounds.X;
			this.pnPositionY.Value  = (decimal)this.pCurrentField.DPIBounds.Y;
			this.pnHeight.Value     = (decimal)this.pCurrentField.DPIBounds.Height;
			this.pnWidth.Value      = (decimal)this.pCurrentField.DPIBounds.Width;
			this.pnBorderSize.Value = (decimal)this.pCurrentField.DPIBorderSize;
			this.pnPadding.Value    = (decimal)this.pCurrentField.DPIPadding;

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

			// czcionka
			Font   font = this.pCurrentField.Font;
			string dets = (font.Bold ? "B" : "") + (font.Italic ? "I" : "") + (font.Strikeout ? "S" : "") + (font.Underline ? "U" : "");

			this.ptbFontName.Text = font.Name + ", " + font.SizeInPoints + "pt " + dets;

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
			this.RefreshProjectList();
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
			GC.Collect();

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
				this.mnPage.Enabled   = false;
				this.mbDelete.Enabled = false;
				return;
			}

			// indeks się nie zmienił...
			if( this.mSelectedID == this.mtvPatterns.SelectedNode.Index && !this.gEditChanged )
				return;

			// proszę czekać...
			this.mlStatus.Text = "Wczytywanie podglądu wzoru...";
			this.mlStatus.Refresh();
			this.Cursor = Cursors.WaitCursor;

			this.mSelectedID      = this.mtvPatterns.SelectedNode.Index;
			this.mSelectedError   = false;
			this.mSelectedData    = true;
			this.mbDelete.Enabled = true;
			this.pLocked          = true;

			// pobierz nazwę elementu
			string pattern     = this.mtvPatterns.SelectedNode.Text;
			this.mSelectedName = pattern;

			// sprawdź czy wzór nie jest uszkodzony
			if( !File.Exists("patterns/" + pattern + "/config.cfg") )
			{
				this.mNoImage       = true;
				this.mSelectedError = true;

				if( this.mibPreview.Image != null )
					this.mibPreview.Image.Dispose();

				// brak obrazka
				this.mibPreview.Image = File.Exists( "noimage.png" )
					? Image.FromFile( "noimage.png" )
					: null;

				// dostosuj rozmiar
				this.mpPreview_Resize( null, null );

				this.mnPage.Enabled = false;
				this.pLocked        = false;

				//MessageBox.Show( this, "Wybrany wzór jest uszkodzony!", "Błąd przetwarzania..." );
				this.Cursor        = null;
				this.mlStatus.Text = "Wybrany wzór jest uszkodzony!";

				return;
			}

			// wczytaj nagłówek
			PatternData data = PatternEditor.ReadPattern( pattern, true );
			int format = -1;
			
			// treść dynamiczna
			this.mSelectedData = data.dynamic;

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
						this.mibPreview.Image.Dispose();

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

			this.mNoImage      = true;
			this.mSelectedData = false;

			// zwolnij pamięć po obrazie
			if( this.mibPreview.Image != null )
				this.mibPreview.Image.Dispose();

			// brak obrazka
			this.mibPreview.Image = File.Exists( "noimage.png" )
				? Image.FromFile( "noimage.png" )
				: null;

			// dostosuj obraz
			this.mpPreview_Resize( null, null );

			this.mnPage.Enabled = false;
			this.pLocked        = false;

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

			// przełącz gdy wzór jest już załadowany
			if( this.pCurrentName == this.mSelectedName )
			{
				this.isPattern_Click( null, null );
				this.plStatus.Text = "Wzór \"" + this.pCurrentName + "\" jest gotowy do edycji.";
				return;
			}

			this.pLocked = true;
			this.Cursor  = Cursors.WaitCursor;
			this.mlStatus.Text = "Trwa przygotowywanie wzoru do edycji...";

			// przejście do edycji wzoru
			string pattern = this.mSelectedName;

			// zapisz indeks i nazwę aktualnego wzoru
			this.pCurrentName = pattern;

			// wczytaj wzór
			this.pcbScale.SelectedIndex = 2;
			PatternData pattern_data = PatternEditor.ReadPattern( pattern );
			PatternEditor.DrawPreview( pattern_data, this.ppPanelContainer, 1.0 );

			// ilość stron
			this.pPanelCounter  = pattern_data.pages - 1;
			this.pnPage.Maximum = pattern_data.pages;
			this.pnPage.Value   = 1;

			// aktualna strona
			this.pCurrentPage    = (Panel)this.ppPanelContainer.Controls[0];
			this.pCurrentPanelID = 0;
			this.pPatternData    = pattern_data;

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
			
			// przełącz na panel wzorów
			this.isPattern_Click( null, null );
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
				"Czy na pewno chcesz usunąć wzór o nazwie: \"" + pattern + "\"?",
				"Usuwanie wzoru...",
				MessageBoxButtons.YesNo
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
				this.isPattern.Enabled = false;
			}
			// brak wzoru, brak obrazka...
			this.mibPreview.Image = null;
			this.mSelectedID      = -1;

			// usuń pliki wzoru i odśwież liste
			Directory.Delete( "patterns/" + pattern, true );
			this.RefreshProjectList();

			this.mlStatus.Text = "Wybrany wzór został usunięty.";
			this.Cursor = null;
		}

		// ------------------------------------------------------------- ictLoadData_Click ----------------------------
		
		private void ictLoadData_Click( object sender, EventArgs ev )
		{
			// wybór pliku
			if( this.gsDBase.ShowDialog(this) != DialogResult.OK )
				return;

			// odczytaj dane
			this.dPatternData = PatternEditor.ReadPattern( this.mSelectedName );
			DataReader reader = new DataReader( this.dPatternData, this.gsDBase.FileName );

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
			
			// przejdź na strone z danymi
			this.isData_Click( null, null );
		}

#endregion

/* ===============================================================================================================================
 * Strona główna - Inne:
 * - mnPage_ValueChanged	[NumericUpDown]
 * - mpPreview_Resize		[Panel]
 * - mpPreview_MouseDown	[Panel]
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
						this.mibPreview.Image.Dispose();

					// skopiuj obraz do pamięci
					Image new_image = new Bitmap( image.Width, image.Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb );
					using( var canavas = Graphics.FromImage(new_image) )
						canavas.DrawImageUnscaled( image, 0, 0 );
					this.mibPreview.Image = new_image;

					image.Dispose();
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
		
		// @TODO poprawić
		private void mpPreview_Resize( object sender, EventArgs ev )
		{
			int   diff     = 0;
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

#endregion

/* ===============================================================================================================================
 * Edycja wzoru - Inne:
 * - pcbScale_SelectedIndexChanged	[ComboBox]
 * - pcbScale_Leave					[ComboBox]
 * - pcbScale_KeyDown				[ComboBox]
 * - pnPage_ValueChanged			[NumericUpDown]
 * - ppPanelContainer_MouseDown		[Panel]
 * - plLabel_Click                  [Label]
 * - pbSave_Click                   [Button]
 * - pbLoadData_Click               [Button]
 * =============================================================================================================================== */

#region Edycja wzoru - Inne

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
			PatternEditor.ChangeScale( this.pPatternData, this.ppPanelContainer, (double)scale / 100.0 );
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

		// ------------------------------------------------------------- plLabel_Click --------------------------------
		
		private void plLabel_Click( object sender, EventArgs ev )
		{
			// aktualny obiekt
			this.pCurrentField = (PageField)sender;
			
			// wypełnij pola i odblokuj je
			this.FillFieldLabels();
			this.UnlockFieldLabels();
		}

		// ------------------------------------------------------------- pbSave_Click ---------------------------------
		
		private void pbSave_Click( object sender, EventArgs ev )
		{
			this.Cursor = Cursors.WaitCursor;
			this.plStatus.Text = "Proszę czekać, trwa zapisywanie wzoru...";

			// usuń pliki konfiguracyjne
			File.Delete( "patterns/" + this.pCurrentName + "/config.cfg" );
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
			PatternEditor.GeneratePreview( this.pPatternData, this.ppPanelContainer, 1.0 );
			PatternEditor.Save( this.pPatternData, this.ppPanelContainer );

			this.gEditChanged = true;

			this.plStatus.Text = "Zapis wzoru zakończony powodzeniem.";
			this.Cursor = null;
		}

		// ------------------------------------------------------------- pbLoadData_Click -----------------------------
		
		private void pbLoadData_Click( object sender, EventArgs ev )
		{
			// wybór pliku
			if( this.gsDBase.ShowDialog(this) != DialogResult.OK )
				return;

			// odczytaj dane
			this.dPatternData = PatternEditor.ReadPattern( this.pCurrentName );
			DataReader reader = new DataReader( this.dPatternData, this.gsDBase.FileName );

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
			
			// przejdź na strone z danymi
			this.isData_Click( null, null );
		}

#endregion

/* ===============================================================================================================================
 * Edycja wzoru - Menu "POLE":
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

			RectangleF new_width = this.pCurrentField.DPIBounds;
			new_width.Width = (float)pnWidth.Value;
			this.pCurrentField.DPIBounds = new_width;
		}

		// ------------------------------------------------------------- pnHeight_ValueChanged ------------------------
		
		private void pnHeight_ValueChanged( object sender, EventArgs ev )
		{
			if( this.pLocked )
				return;

			RectangleF new_height = this.pCurrentField.DPIBounds;
			new_height.Height = (float)pnHeight.Value;
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
			
			this.pCurrentField.DPIPadding = (float)this.pnPadding.Value;
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

			this.icpPrintColor.Checked = tag.print_color;
			this.icpPrintImage.Checked = tag.print_image;
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

			// przypisz zdarzenia
			field.ContextMenuStrip = this.icLabel;
			field.Click           += new EventHandler( this.plLabel_Click );

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
			PageExtraData tag = (PageExtraData)this.pCurrentPage.Tag;
			tag.print_color = icpPrintColor.Checked;
			this.pCurrentPage.Tag = tag;
		}

		// ------------------------------------------------------------- icpPrintImage_CheckedChanged -----------------
		
		private void icpPrintImage_CheckedChanged( object sender, EventArgs ev )
		{
			PageExtraData tag = (PageExtraData)this.pCurrentPage.Tag;
			tag.print_image = icpPrintImage.Checked;
			this.pCurrentPage.Tag = tag;
		}

		// ------------------------------------------------------------- icpAddPage_Click -----------------------------
		
		private void icpAddPage_Click( object sender, EventArgs ev )
		{
			Panel  page  = new Panel( );
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

			// dodaj panel do kontenera
			this.ppPanelContainer.Controls.Add( page );

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
				MessageBox.Show( this, "Masz tylko jedną stronę. Nie możesz jej usunąć...", "Bład usuwania strony..." );
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
			this.plLabel_Click( this.icLabel.SourceControl, ev );
			FieldExtraData tag = (FieldExtraData)this.pCurrentField.Tag;

			// zaznacz lub odznacz elementy
			this.iclBackFromDb.Checked  = tag.image_from_db;
			this.iclPrintColor.Checked  = tag.print_color;
			this.iclPrintImage.Checked  = tag.print_image;
			this.iclTextFromDb.Checked  = tag.text_from_db;
			this.iclPrintText.Checked   = tag.print_text;
			this.iclPrintBorder.Checked = tag.print_border;
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
			bool is_check = this.iclBackFromDb.Checked;
			FieldExtraData tag = (FieldExtraData)this.pCurrentField.Tag;

			tag.image_from_db = is_check;
			tag.text_from_db  = false;
			tag.print_color   = false;
			tag.print_image   = false;

			this.iclPrintColor.Checked = false;
			this.iclPrintImage.Checked = false;
		}

		// ------------------------------------------------------------- iclPrintColor_Click --------------------------
		
		private void iclPrintColor_Click( object sender, EventArgs ev )
		{
			bool is_check = this.iclPrintColor.Checked;
			FieldExtraData tag = (FieldExtraData)this.pCurrentField.Tag;

			tag.image_from_db = false;
			tag.print_color   = is_check;

			this.iclBackFromDb.Checked = false;
		}

		// ------------------------------------------------------------- iclPrintImage_Click --------------------------
		
		private void iclPrintImage_Click( object sender, EventArgs ev )
		{
			bool is_check = this.iclPrintImage.Checked;
			FieldExtraData tag = (FieldExtraData)this.pCurrentField.Tag;

			tag.image_from_db = false;
			tag.print_image   = is_check;

			this.iclBackFromDb.Checked = false;
			this.iclPrintColor.Checked = false;
		}

		// ------------------------------------------------------------- iclTextFromDb_Click --------------------------
		
		private void iclTextFromDb_Click( object sender, EventArgs ev )
		{
			bool is_check = this.iclTextFromDb.Checked;
			FieldExtraData tag = (FieldExtraData)this.pCurrentField.Tag;

			tag.text_from_db  = is_check;
			tag.image_from_db = false;
			tag.print_text    = false;

			this.iclPrintText.Checked  = false;
			this.iclBackFromDb.Checked = false;
		}

		// ------------------------------------------------------------- iclPrintText_Click ---------------------------
		
		private void iclPrintText_Click( object sender, EventArgs ev )
		{
			bool is_check = this.iclPrintText.Checked;
			FieldExtraData tag = (FieldExtraData)this.pCurrentField.Tag;

			tag.text_from_db = false;
			tag.print_text   = is_check;

			this.iclTextFromDb.Checked = false;
		}

		// ------------------------------------------------------------- iclPrintText_Click ---------------------------
		
		private void iclPrintBorder_Click( object sender, EventArgs ev )
		{
			((FieldExtraData)this.pCurrentField.Tag).print_border = this.iclPrintBorder.Checked;
		}

#endregion

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

		// ------------------------------------------------------------- pcbShowFrame_CheckedChanged ------------------
		
		private void pcbShowFrame_CheckedChanged( object sender, EventArgs ev )
		{
			((FieldExtraData)this.pCurrentField.Tag).print_border = this.pcbShowFrame.Checked;
		}

		// ------------------------------------------------------------- pcbDrawColor_CheckedChanged ------------------
		
		private void pcbDrawColor_CheckedChanged( object sender, EventArgs ev )
		{
			bool is_check = this.pcbDrawColor.Checked;
			if( !is_check )
				return;

			FieldExtraData tag = (FieldExtraData)this.pCurrentField.Tag;

			tag.image_from_db = false;
			tag.print_color   = is_check;

			this.pcbDynImage.Checked  = false;
			this.pcbStatImage.Checked = false;
		}

		// ------------------------------------------------------------- pcbDynText_CheckedChanged --------------------
		
		private void pcbDynText_CheckedChanged( object sender, EventArgs ev )
		{
			bool is_check = this.pcbDynText.Checked;
			if( !is_check )
				return;

			FieldExtraData tag = (FieldExtraData)this.pCurrentField.Tag;

			tag.text_from_db  = is_check;
			tag.image_from_db = false;
			tag.print_text    = false;

			this.pcbStatText.Checked = false;
			this.pcbDynImage.Checked = false;
		}

		// ------------------------------------------------------------- pcbStatImage_CheckedChanged ------------------
		
		private void pcbStatImage_CheckedChanged( object sender, EventArgs ev )
		{
			bool is_check = this.pcbStatImage.Checked;
			if( !is_check )
				return;

			FieldExtraData tag = (FieldExtraData)this.pCurrentField.Tag;

			tag.image_from_db = false;
			tag.print_image   = is_check;

			this.pcbDynImage.Checked = false;
			this.pcbDrawColor.Checked = false;
		}

		// ------------------------------------------------------------- pcbStatText_CheckedChanged -------------------
		
		private void pcbStatText_CheckedChanged( object sender, EventArgs ev )
		{
			bool is_check = this.pcbStatText.Checked;
			if( !is_check )
				return;
	
			FieldExtraData tag = (FieldExtraData)this.pCurrentField.Tag;

			tag.text_from_db = false;
			tag.print_text   = is_check;

			this.pcbDynText.Checked = false;
		}

		// ------------------------------------------------------------- pcbDynImage_CheckedChanged -------------------
		
		private void pcbDynImage_CheckedChanged(object sender, EventArgs e)
		{
			bool is_check = this.pcbDynImage.Checked;
			if( !is_check )
				return;

			FieldExtraData tag = (FieldExtraData)this.pCurrentField.Tag;

			tag.image_from_db = is_check;
			tag.text_from_db  = false;
			tag.print_color   = false;
			tag.print_image   = false;

			this.pcbDynText.Checked  = false;
			this.pcbDrawColor.Checked = false;
			this.pcbStatImage.Checked = false;
		}








		private void mbDelete_MouseEnter( object sender, EventArgs ev )
		{
			this.mlStatus.Text = "Usuwa wybrany z listy wzór.";
		}

		private void mbNew_MouseEnter( object sender, EventArgs ev )
		{
			this.mlStatus.Text = "Otwiera okno z kreatorem nowego wzoru.";
		}

		private void mlStatus_ClearText( object sender, EventArgs ev )
		{
			this.mlStatus.Text = "";
		}

		private void pcbScale_MouseEnter( object sender, EventArgs ev )
		{
			this.plStatus.Text = "Zmiana powiększenia wzoru. Możesz wybrać lub wpisać własną wartość w zakresie od 50 do 300.";
		}

		private void plStatus_ClearText(object sender, EventArgs e)
		{
			this.plStatus.Text = "";
		}











		// ------------------------------------------------------------- dtvData_AfterSelect --------------------------
		
		private void dtvData_AfterSelect( object sender, TreeViewEventArgs ev )
		{
			if( this.dtvData.SelectedNode == null )
				return;

			this.pLocked = true;

			if( !this.dSketchDrawed )
			{
				PatternEditor.DrawSketch( this.dPatternData, this.dpPreview, 1.0 /*(double)this.cbScale.SelectedText / 100.0*/ );
				this.dSketchDrawed = true;
			}

			PatternEditor.DrawRow( this.dpPreview, this.dDataContent, ev.Node.Index );

			this.pLocked = false;
		}

		private void dbGeneratePDF_Click( object sender, EventArgs ev )
		{
			PatternEditor.GeneratePDF( this.dDataContent, this.dPatternData );
		}




	}
}
