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

using PdfSharp;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;

using Bytescout.PDFRenderer;

namespace CDesigner
{
	public partial class Main : Form
	{
		private RasterRenderer renderer;

		private bool rendererLoaded = false;
		
		private int         prevCurr = -1;
		private bool        prevLock = false;
		private Size        prevSize;
		private CustomLabel prevCurrObj;

		// ------------------------------------------------------------------------------------------------------------

		public Main( )
		{
			InitializeComponent( );

			// utwórz folder gdy nie istnieje
			if( !Directory.Exists("patterns") )
				Directory.CreateDirectory( "patterns" );

			// odśwież listę wzorów
			this._refreshProjectList( );

			this.renderer = new RasterRenderer( );
			this.renderer.RegistrationName = "demo";
			this.renderer.RegistrationKey = "demo";
			this.renderer.Resolution = (int)nPreviewDPI.Value;
		}

		// ------------------------------------------------------------- _refreshProjectList --------------------------

		private void _refreshProjectList( )
		{
			// wyczyść wszystkie dane
			this.lbProjectList.Items.Clear( );

			// pobierz i dodaj katalogi do listy
			string[] patterns = Directory.GetDirectories( "patterns" );
			foreach( string pattern in patterns )
				this.lbProjectList.Items.Add( pattern.Replace("patterns\\", "") );
		}

		// ------------------------------------------------------------------------------------------------------------

		private void lbProjectList_SelectedValueChanged( object sender, EventArgs e )
		{
			if( lbProjectList.SelectedIndex < 0 )
				return;

			string item = lbProjectList.SelectedItem.ToString( );

			// wzór jest uszkodzony...
			if( !File.Exists("patterns/" + item + "/config.cfg") )
			{
				MessageBox.Show( "Wybrany wzór jest uszkodzony!" );

				// deaktywuj niepotrzebne przyciski
				bEditProject.Enabled   = false;
				bDeleteProject.Enabled = true;
				bLoadData.Enabled      = false;

				// brak obrazka
				if( File.Exists("noimage.png") )
				{
					ibProjectPreview.SizeMode = PictureBoxSizeMode.CenterImage;
					ibProjectPreview.Dock     = DockStyle.Fill;
					ibProjectPreview.Image    = Image.FromFile( "noimage.png" );
				}
				return;
			}

			if( File.Exists("patterns/" + item + "/preview.pdf") )
			{
				this.renderer.LoadDocumentFromFile("patterns/" + item + "/preview.pdf");
				this.rendererLoaded = true;

				this.renderer.Resolution = (int)nPreviewDPI.Value;

				nPreviewPages.Maximum = renderer.GetPageCount();
				nPreviewPages.Minimum = 1;

				Image image = this.renderer.RenderPageToImage(0);
				ibProjectPreview.Image = image;

				// aktywuj wszystkie przyciski
				bEditProject.Enabled   = true;
				bDeleteProject.Enabled = true;
				bLoadData.Enabled      = true;

				return;
			}

			// aktywuj wybrane przyciski
			bEditProject.Enabled   = true;
			bDeleteProject.Enabled = true;
			bLoadData.Enabled      = false;

			// brak obrazka
			if( File.Exists("noimage.png") )
			{
				ibProjectPreview.SizeMode = PictureBoxSizeMode.CenterImage;
				ibProjectPreview.Dock = DockStyle.Fill;
				ibProjectPreview.Image = Image.FromFile( "noimage.png" );
			}
		}

		// ------------------------------------------------------------------------------------------------------------

		private void nPreviewDPI_ValueChanged( object sender, EventArgs e )
		{
			if( !this.rendererLoaded )
				return;

			this.renderer.Resolution = (int)nPreviewDPI.Value;

			Image image = this.renderer.RenderPageToImage(0);
			ibProjectPreview.Image = image;
		}

		// ------------------------------------------------------------------------------------------------------------

		private void mmPNew_Click( object sender, EventArgs e )
		{
			NewPattern window = new NewPattern( );

			// nowy wzór - odśwież listę
			if( window.ShowDialog(this) == DialogResult.OK )
				this._refreshProjectList( );
			else
				return;

			tMainPanel.Hide( );
			tPatternPanel.Show( );

			mmSwitchHome.Enabled    = true;
			mmSwitchPattern.Enabled = false;
		}

		// ------------------------------------------------------------------------------------------------------------

		private void mmSwitchPattern_Click( object sender, EventArgs e )
		{
			tMainPanel.Hide( );
			tPatternPanel.Show( );
			
			mmSwitchHome.Enabled    = true;
			mmSwitchPattern.Enabled = false;
		}

		// ------------------------------------------------------------------------------------------------------------

		private void mmSwitchHome_Click( object sender, EventArgs e )
		{
			tMainPanel.Show( );
			tPatternPanel.Hide( );

			mmSwitchHome.Enabled = false;
			mmSwitchPattern.Enabled = true;
		}

		// ------------------------------------------------------------------------------------------------------------

		private void bEditProject_Click( object sender, EventArgs e )
		{
			// indeks zaznaczonego obiektu musi być większy niż -1
			if( this.lbProjectList.SelectedIndex < 0 )
				return;

			// aktywuj odpowiednie przyciski
			this.mmSwitchHome.Enabled = true;
			this.mmSwitchPattern.Enabled = false;

			// ukryj i pokaż odpowiednie panele
			this.tMainPanel.Hide( );
			this.tPatternPanel.Show( );

			// zapisz indeks aktualnego wzoru
			this.prevCurr = lbProjectList.SelectedIndex;

			// wczytaj dane
			string item = lbProjectList.SelectedItem.ToString( );
			FileStream file = new FileStream( "patterns/" + item + "/config.cfg", FileMode.OpenOrCreate );
			BinaryReader reader = new BinaryReader( file );

			// wczytaj rozmiar (mm)
			this.prevSize.Width = (int)reader.ReadInt16( );
			this.prevSize.Height = (int)reader.ReadInt16( );

			// zamknij plik
			reader.Close( );
			file.Close( );

			// dostosuj rozmiar
			Size pp_size = new Size
			(
				(int)((double)this.prevSize.Width * 3.938095238095238 * ((double)this.nPDPI.Value / 100.0)),
				(int)((double)this.prevSize.Height * 3.938095238095238 * ((double)this.nPDPI.Value / 100.0))
			);
			this.pPPreview.Size = pp_size;
		}

		// ------------------------------------------------------------------------------------------------------------

		private void bDeleteProject_Click( object sender, EventArgs e )
		{
			// element musi być zaznaczony
			if( lbProjectList.SelectedIndex < 0 )
				return;

			// pobierz nazwę wzoru i usuń folder o podanej nazwie wraz z zawartością
			string item = lbProjectList.SelectedItem.ToString( );
			Directory.Delete( "patterns/" + item, true );

			// odśwież listę wzorów
			this._refreshProjectList( );
		}

		private void cmppPClearBack_Click(object sender, EventArgs e)
		{
			this.pPPreview.BackgroundImage = null;
			this.pPPreview.BackColor = SystemColors.Window;
		}

		// ------------------------------------------------------------- PreviewChangePageBackground ------------------

		private void PreviewChangePageBackground( object sender, EventArgs e )
		{
			// okno wyboru pliku
			if( fdSelectImage.ShowDialog(this) != DialogResult.OK )
				return;

			// zmień obraz panelu
			this.pPPreview.BackgroundImage = Image.FromFile( fdSelectImage.FileName );
			this.pPPreview.BackColor       = Color.Empty;
		}

		// ------------------------------------------------------------- PreviewAddLabelToPage ------------------------

		private void PreviewAddLabelToPage( object sender, EventArgs e )
		{
			// utwórz nowe pole
			CustomLabel label = new CustomLabel( );

			// ustaw wartości
			label.BorderColor = Color.Black;
			label.Text        = "nowe pole";
			label.TextAlign   = ContentAlignment.MiddleCenter;
			label.BackColor   = Color.Transparent;
			label.FontSize    = 8.25;

			// ustaw granice pola
			label.SetDPIBounds( 0, 0, 100, 25, 1, (int)this.nPDPI.Value );

			// położenie i marginesy
			label.Margin   = new Padding(0);
			label.Padding  = new Padding(0);
			label.Location = new Point(0, 0);

			// menu i akcja na kliknięcie myszą
			label.ContextMenuStrip = mPreviewLabel;
			label.Click += new EventHandler( PreviewPageLabelClick );

			// dodaj do kontenera
			this.pPPreview.Controls.Add( label );
		}

		// ------------------------------------------------------------- PreviewPageLabelClick ------------------------

		private void PreviewPageLabelClick( object sender, EventArgs e )
		{
			// aktualny obiekt
			this.prevCurrObj = (CustomLabel)sender;

			// zablokuj zmiane wartości
			this.prevLock = true;

			// aktywuj kontrolki
			this.bPBorderColor.Enabled  = true;
			this.bPFont.Enabled         = true;
			this.bPFontColor.Enabled    = true;
			this.bPLabelImage.Enabled   = true;
			this.bPLabelColor.Enabled   = true;
			this.nPBorderSize.Enabled   = true;
			this.nPLeft.Enabled         = true;
			this.nPTop.Enabled          = true;
			this.nPWidth.Enabled        = true;
			this.nPHeight.Enabled       = true;
			this.tbPName.Enabled        = true;
			this.tbPBorderColor.Enabled = true;
			this.tbPFontColor.Enabled   = true;
			this.tbPLabelColor.Enabled  = true;

			// zmień nazwę
			this.tbPName.Text = ((CustomLabel)sender).Text;

			// aktualizuj pola numeryczne
			this.nPLeft.Value       = this.prevCurrObj.DPILeft;
			this.nPTop.Value        = this.prevCurrObj.DPITop;
			this.nPHeight.Value     = this.prevCurrObj.DPIHeight;
			this.nPWidth.Value      = this.prevCurrObj.DPIWidth;
			this.nPBorderSize.Value = this.prevCurrObj.BorderSize;

			// aktualizuj kolory...
			this.tbPBorderColor.Text = this.prevCurrObj.BorderColor.R.ToString("X2") +
									   this.prevCurrObj.BorderColor.G.ToString("X2") +
									   this.prevCurrObj.BorderColor.B.ToString("X2");
			this.tbPLabelColor.Text  = this.prevCurrObj.BackColor.R.ToString("X2") +
									   this.prevCurrObj.BackColor.G.ToString("X2") +
									   this.prevCurrObj.BackColor.B.ToString("X2") +
									   this.prevCurrObj.BackColor.A.ToString("X2");
			this.tbPFontColor.Text   = this.prevCurrObj.ForeColor.R.ToString("X2") +
									   this.prevCurrObj.ForeColor.G.ToString("X2") +
									   this.prevCurrObj.ForeColor.B.ToString("X2");

			// czcionka...
			this.tbPFont.Text = this.prevCurrObj.Font.Name + ", " +
								this.prevCurrObj.Font.SizeInPoints + "pt " +
								(this.prevCurrObj.Font.Bold ? "B" : "") +
								(this.prevCurrObj.Font.Italic ? "I" : "") +
								(this.prevCurrObj.Font.Strikeout ? "S" : "") +
								(this.prevCurrObj.Font.Underline ? "U" : "");

			// nazwa / ścieżka obrazu
			this.tbPLabelImage.Text = this.prevCurrObj.HasImage ? this.prevCurrObj.ImagePath : "";

			// odblokuj zmiane wartości
			this.prevLock = false;
		}

		// ------------------------------------------------------------- PreviewBorderSize ----------------------------

		private void PreviewBorderSize( object sender, EventArgs e )
		{
			if( this.prevLock )
				return;
			this.prevCurrObj.BorderSize = (int)nPBorderSize.Value;

			// odśwież kontrolke
			this.prevCurrObj.Refresh( );
		}

		// ------------------------------------------------------------- PreviewChangeDPI -----------------------------

		private void PreviewChangeDPI( object sender, EventArgs e )
		{
			double dpi_scale = (double)this.nPDPI.Value / 100.0;

			// dostosuj rozmiar strony
			Size pp_size = new Size
			(
				(int)((double)this.prevSize.Width  * 3.938095238095238 * dpi_scale),
				(int)((double)this.prevSize.Height * 3.938095238095238 * dpi_scale)
			);
			this.pPPreview.Size = pp_size;

			// aktualizuj rozmiary kontrolek
			for( int x = 0; x < this.pPPreview.Controls.Count; ++x )
				((CustomLabel)this.pPPreview.Controls[x]).DPIScale = dpi_scale;
		}

		// ------------------------------------------------------------- PreviewChangeLabelName -----------------------

		private void PreviewChangeLabelName( object sender, EventArgs e )
		{
			if (this.prevLock)
				return;

			this.prevCurrObj.Text = this.tbPName.Text;
		}

		// ------------------------------------------------------------- PreviewBChangeBorderColor --------------------

		private void PreviewBChangeBorderColor( object sender, EventArgs e )
		{
			if( cdSelectColor.ShowDialog(this) != DialogResult.OK )
				return;

			this.prevCurrObj.BorderColor = cdSelectColor.Color;
			this.tbPBorderColor.Text     = cdSelectColor.Color.R.ToString("X2") +
										   cdSelectColor.Color.G.ToString("X2") +
										   cdSelectColor.Color.B.ToString("X2");
			// odśwież kontrolke
			this.prevCurrObj.Refresh( );
		}

		// ------------------------------------------------------------- PreviewBChangeLabelColor ---------------------

		private void PreviewBChangeLabelColor( object sender, EventArgs e )
		{
			if( cdSelectColor.ShowDialog(this) != DialogResult.OK )
				return;

			this.prevCurrObj.BackColor = cdSelectColor.Color;
			this.tbPLabelColor.Text    = cdSelectColor.Color.R.ToString("X2") +
										 cdSelectColor.Color.G.ToString("X2") +
										 cdSelectColor.Color.B.ToString("X2") +
										 cdSelectColor.Color.A.ToString("X2");
			// odśwież kontrolke
			this.prevCurrObj.Refresh( );
		}

		// ------------------------------------------------------------- --PreviewBChangeFontColor --------------------

		private void PreviewBChangeFontColor( object sender, EventArgs e )
		{
			if( cdSelectColor.ShowDialog(this) != DialogResult.OK )
				return;

			this.prevCurrObj.ForeColor = cdSelectColor.Color;
			this.tbPFontColor.Text     = cdSelectColor.Color.R.ToString("X2") +
										 cdSelectColor.Color.G.ToString("X2") +
										 cdSelectColor.Color.B.ToString("X2");
			// odśwież kontrolke
			this.prevCurrObj.Refresh( );
		}

		// ------------------------------------------------------------- PreviewChangeFont ----------------------------

		private void PreviewChangeFont( object sender, EventArgs e )
		{
			if( fdSelectFont.ShowDialog(this) != DialogResult.OK )
				return;
			this.prevCurrObj.Font     = fdSelectFont.Font;
			this.prevCurrObj.FontSize = fdSelectFont.Font.SizeInPoints;

			// odśwież dane na temat czcionki...
			this.tbPFont.Text = this.prevCurrObj.Font.Name + ", " +
								this.prevCurrObj.Font.SizeInPoints + "pt " +
								(this.prevCurrObj.Font.Bold ? "B" : "") +
								(this.prevCurrObj.Font.Italic ? "I" : "") +
								(this.prevCurrObj.Font.Strikeout ? "S" : "") +
								(this.prevCurrObj.Font.Underline ? "U" : "");
			// odśwież kontrolke
			this.prevCurrObj.Refresh( );
		}

		// ------------------------------------------------------------- PreviewChangeLabelBackground -----------------

		private void PreviewChangeLabelBackground( object sender, EventArgs e )
		{
			// okno wyboru pliku
			if (fdSelectImage.ShowDialog(this) != DialogResult.OK)
				return;

			// zmień obraz panelu
			this.prevCurrObj.BackImage = Image.FromFile(fdSelectImage.FileName);
			this.prevCurrObj.BackColor = Color.Empty;

			// ustaw ścieżkę do obrazu w polu tekstowym
			this.tbPLabelImage.Text = fdSelectImage.FileName;

			// odśwież kontrolke
			this.prevCurrObj.Refresh( );
		}

		// ------------------------------------------------------------- PreviewSetLabelPosX --------------------------

		private void PreviewSetLabelPosX( object sender, EventArgs e )
		{
			this.prevCurrObj.DPILeft = (int)nPLeft.Value;
		}

		// ------------------------------------------------------------- PreviewSetLabelPosY --------------------------

		private void PreviewSetLabelPosY( object sender, EventArgs e )
		{
			this.prevCurrObj.DPITop = (int)nPTop.Value;
		}

		// ------------------------------------------------------------- PreviewSetLabelWidth -------------------------

		private void PreviewSetLabelWidth( object sender, EventArgs e )
		{
			this.prevCurrObj.DPIWidth = (int)nPWidth.Value;
		}

		// ------------------------------------------------------------- PreviewSetLabelHeight ------------------------

		private void PreviewSetLabelHeight( object sender, EventArgs e )
		{
			this.prevCurrObj.DPIHeight = (int)nPHeight.Value;
		}

		// ------------------------------------------------------------- PreviewSetTextVPosition ----------------------

		private void PreviewSetTextPosition( object sender, EventArgs e )
		{
			// zmień pozycje tekstu
			switch( this.tbPTextHPos.Value | (this.tbPTextVPos.Value << 2) )
			{
				case 0:  this.prevCurrObj.TextAlign = ContentAlignment.TopLeft;      break;
				case 1:  this.prevCurrObj.TextAlign = ContentAlignment.TopCenter;    break;
				case 2:  this.prevCurrObj.TextAlign = ContentAlignment.TopRight;     break;
				case 4:  this.prevCurrObj.TextAlign = ContentAlignment.MiddleLeft;   break;
				case 5:  this.prevCurrObj.TextAlign = ContentAlignment.MiddleCenter; break;
				case 6:  this.prevCurrObj.TextAlign = ContentAlignment.MiddleRight;  break;
				case 8:  this.prevCurrObj.TextAlign = ContentAlignment.BottomLeft;   break;
				case 9:  this.prevCurrObj.TextAlign = ContentAlignment.BottomCenter; break;
				case 10: this.prevCurrObj.TextAlign = ContentAlignment.BottomRight;  break;
			}
		}
	}
}
