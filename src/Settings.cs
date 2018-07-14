using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

// @TODO: położenie lewej kolumny

namespace CDesigner
{
	public partial class Settings : Form
	{
		private TableLayoutPanel _visible_table = null;
		private Image            _transparent   = null;
		private Image            _no_image      = null;
		private SettingsInfo     _settings      = null;

		// ------------------------------------------------------------- Settings -------------------------------------
		
		public Settings( int page = 0, int subpage = -1 )
		{
			this.InitializeComponent();

			foreach( TreeNode item in this.tvList.Nodes )
				if( item.Index == page && subpage > -1 )
					foreach( TreeNode subitem in item.Nodes )
					{
						if( subitem.Index == subpage )
 						{
							this.tvList.SelectedNode = subitem;
							break;
						}
					}
				else if( item.Index == page )
				{
					this.tvList.SelectedNode = item;
					break;
				}

			// wczytaj ustawienia
			this.LoadSettings();

			// uzupełnij dane
			this.FillOptions();

			this.pPagePreview.BackColor = this._settings.g_back_color;
			this.ppPage.BackgroundImage = this._transparent;
			
			this.lPreview.Hide();
		}

		// ------------------------------------------------------------- FillOptions ----------------------------------
		
		private void FillOptions( )
		{
			// obrazki
			this._transparent = Image.FromFile( "transparent.png" );
			this._no_image    = Image.FromFile( "noimage.png" );

			// ustawienia główne
			this.gcbResolution.Text        = this._settings.g_def_scale.ToString();
			this.gnPadding.Value           = (decimal)this._settings.g_padding;
			this.gcbPrevPos.SelectedIndex  = this._settings.g_page_float;
			this.gcbAskBeforeClose.Checked = this._settings.g_ask_before_close;
			this.gcbSaveSlide.Checked      = this._settings.g_save_sliders;
			this.gcbShortcuts.Checked      = this._settings.g_shortcuts_on;
			this.gcbInfoPanel.Checked      = this._settings.g_show_status;
			this.gnPixelsPerDPI.Value      = (decimal)this._settings.g_pixels_per_dpi;
		}

		// ------------------------------------------------------------- OpenSettings ---------------------------------
		
		public void LoadSettings( )
		{
			this._settings = new SettingsInfo();

			this._settings.g_def_scale      = 100;
			this._settings.g_back_color     = SystemColors.ScrollBar;
			this._settings.g_padding        = 5;
			this._settings.g_page_float     = 1;
			this._settings.g_pixels_per_dpi = 3.938095238095238;

			this._settings.g_ask_before_close = false;
			this._settings.g_save_sliders     = false;
			this._settings.g_shortcuts_on     = true;
			this._settings.g_show_status      = true;
		}

		// ------------------------------------------------------------- SaveSettings ---------------------------------
		
		public void SaveSettings( )
		{
			FileStream   file   = new FileStream( "config.cfg", FileMode.OpenOrCreate );
			BinaryWriter writer = new BinaryWriter( file );

			// identyfikator
			byte[] text = new byte[5] { (byte)'C', (byte)'D', (byte)'C', (byte)'F', (byte)'G' };
			writer.Write( text, 0, 5 );

			// ustawienia główne
			writer.Write( this._settings.g_def_scale );
			writer.Write( this._settings.g_back_color.ToArgb() );
			writer.Write( this._settings.g_padding );
			writer.Write( this._settings.g_page_float );
			writer.Write( this._settings.g_ask_before_close );
			writer.Write( this._settings.g_save_sliders );
			writer.Write( this._settings.g_shortcuts_on );
			writer.Write( this._settings.g_show_status );
			writer.Write( this._settings.g_pixels_per_dpi );

			writer.Close();
			file.Close();
		}

		// ------------------------------------------------------------- bSave_Click ----------------------------------
		
		private void bSave_Click( object sender, EventArgs ev )
		{
			this.SaveSettings( );
		}

		// ------------------------------------------------------------- tvList_AfterSelect ---------------------------
		
		private void tvList_AfterSelect( object sender, TreeViewEventArgs ev )
		{
			TableLayoutPanel current = null;
			int index;

			// konwertuj nazwę do int
			try   { index = Convert.ToInt32( this.tvList.SelectedNode.Name ); }
			catch { index = 1; }

			switch( index )
			{
			case 1:
				current = this.tlGeneral;
			break;
			case 2:
				current = this.tlResolutions;
			break;
			case 3:
				current = this.tlEditor;
			break;
			case 4:
				current = this.tlDefField;
			break;
			}

			if( current == null )
				return;

			current.Visible = true;

			if( this._visible_table != null )
				this._visible_table.Visible = false;

			this._visible_table = current;
		}

#region Ustawienia ogólne

		// ------------------------------------------------------------- gswNoImage_CheckedChanged --------------------
		
		private void gswNoImage_CheckedChanged( object sender, EventArgs ev )
		{
			this.gswColor_IfAllUnchecked( );

			if( !this.gswNoImage.Checked )
				return;

			this.gswColor.Checked       = false;
			this.gswTransparent.Checked = false;

			this.pPagePreview.Padding = new Padding( 0 );
			this.ppPage.BackgroundImageLayout = ImageLayout.Zoom;
			this.ppPage.BackgroundImage = this._no_image;
			this.ppPage.Show();
		}

		// ------------------------------------------------------------- gswTransparent_CheckedChanged ----------------
		
		private void gswTransparent_CheckedChanged( object sender, EventArgs ev )
		{
			this.gswColor_IfAllUnchecked( );

			if( !this.gswTransparent.Checked )
				return;

			this.gswColor.Checked   = false;
			this.gswNoImage.Checked = false;
			
			this.pPagePreview.Padding = new Padding( 0 );
			this.ppPage.BackgroundImageLayout = ImageLayout.Tile;
			this.ppPage.BackgroundImage = this._transparent;
			this.ppPage.Show();
		}

		// ------------------------------------------------------------- gswColor_CheckedChanged ----------------------
		
		private void gswColor_CheckedChanged( object sender, EventArgs ev )
		{
			this.gswColor_IfAllUnchecked( );

			if( !this.gswColor.Checked )
				return;

			this.gswTransparent.Checked = false;
			this.gswNoImage.Checked     = false;
			
			this.ppPage.Hide();
		}

		// ------------------------------------------------------------- gswColor_IfAllUnchecked ----------------------
		
		private void gswColor_IfAllUnchecked( )
		{
			if( this.gswColor.Checked || this.gswNoImage.Checked || this.gswTransparent.Checked )
				return;

			this.pPagePreview.Padding = new Padding( (int)this.gnPadding.Value );
			this.ppPage.BackgroundImageLayout = ImageLayout.Tile;
			this.ppPage.BackgroundImage = this._transparent;
			this.ppPage.Show();
		}

		// ------------------------------------------------------------- gcbResolution_SelectedIndexChanged -----------
		
		private void gcbResolution_SelectedIndexChanged( object sender, EventArgs ev )
		{
			// próbuj zamienić string na int
			int scale = 0;
			try { scale = Convert.ToInt32(this.gcbResolution.Text); }
			catch
			{
				scale = 100;
				this.gcbResolution.Text = "100";
			}

			// nie można zmniejszyć więcej niż 50%
			if( scale < 50 )
			{
				this.gcbResolution.Text = "50";
				scale = 50;
			}

			// nie można powiększyć więcej niż 300%
			if( scale > 300 )
			{
				this.gcbResolution.Text = "300";
				scale = 300;
			}

			this._settings.g_def_scale = scale;
		}

		// ------------------------------------------------------------- gbPrevColor_Click ----------------------------
		
		private void gbPrevColor_Click( object sender, EventArgs ev )
		{
			if( this.dialogColor.ShowDialog(this) != DialogResult.OK )
				return;

			this._settings.g_back_color = this.dialogColor.Color;
			this.pPagePreview.BackColor = this._settings.g_back_color;
		}

		// ------------------------------------------------------------- gnPadding_ValueChanged -----------------------
		
		private void gnPadding_ValueChanged( object sender, EventArgs ev )
		{
			this._settings.g_padding  = (int)this.gnPadding.Value;
			this.pPagePreview.Padding = new Padding( (int)this.gnPadding.Value );
		}

		// ------------------------------------------------------------- gcbPrevPos_SelectedIndexChanged --------------
		
		private void gcbPrevPos_SelectedIndexChanged( object sender, EventArgs ev )
		{
			this._settings.g_page_float = this.gcbPrevPos.SelectedIndex;
		}

		// ------------------------------------------------------------- gbTransparent_Click --------------------------
		
		private void gbTransparent_Click( object sender, EventArgs ev )
		{
			if( this.dialogImage.ShowDialog(this) != DialogResult.OK )
				return;

			// gdy wyświetlany jest obraz przezroczystości
			if( this.ppPage.BackgroundImage == this._transparent )
			{
				// wczytaj i przypisz nowy obrazek
				this._transparent = Image.FromFile( this.dialogImage.FileName );
				Image trash = this.ppPage.BackgroundImage;

				this.ppPage.BackgroundImage = this._transparent;

				// zwolnij pamięć
				if( trash != null )
				{
					trash.Dispose();
					GC.Collect();
				}
			}
			// jeżeli nie, nie przejmuj się wyświetlanym obrazem...
			else
			{
				// zwolnij pamięć
				if( this._transparent != null )
				{
					this._transparent.Dispose();
					GC.Collect();
				}

				this._transparent = Image.FromFile( this.dialogImage.FileName );
			}
		}

		// ------------------------------------------------------------- gcbAskBeforeClose_CheckedChanged -------------
		
		private void gcbAskBeforeClose_CheckedChanged( object sender, EventArgs ev )
		{
			this._settings.g_ask_before_close = this.gcbAskBeforeClose.Checked;
		}

		// ------------------------------------------------------------- gcbSaveSlide_CheckedChanged ------------------
		
		private void gcbSaveSlide_CheckedChanged( object sender, EventArgs ev )
		{
			this._settings.g_save_sliders = this.gcbSaveSlide.Checked;
		}

		// ------------------------------------------------------------- gcbShortcuts_CheckedChanged ------------------
		
		private void gcbShortcuts_CheckedChanged( object sender, EventArgs ev )
		{
			this._settings.g_shortcuts_on = this.gcbShortcuts.Checked;
		}

		// ------------------------------------------------------------- gcbInfoPanel_CheckedChanged ------------------
		
		private void gcbInfoPanel_CheckedChanged( object sender, EventArgs ev )
		{
			this._settings.g_show_status = this.gcbInfoPanel.Checked;
		}

		// ------------------------------------------------------------- gbNoPreview_Click ----------------------------
		
		private void gbNoPreview_Click( object sender, EventArgs ev )
		{
			if( this.dialogImage.ShowDialog(this) != DialogResult.OK )
				return;

			// gdy wyświetlany jest obraz przezroczystości
			if( this.ppPage.BackgroundImage == this._no_image )
			{
				// wczytaj i przypisz nowy obrazek
				this._no_image = Image.FromFile( this.dialogImage.FileName );
				Image trash = this.ppPage.BackgroundImage;

				this.ppPage.BackgroundImage = this._no_image;

				// zwolnij pamięć
				if( trash != null )
				{
					trash.Dispose();
					GC.Collect();
				}
			}
			// jeżeli nie, nie przejmuj się wyświetlanym obrazem...
			else
			{
				// zwolnij pamięć
				if( this._no_image != null )
				{
					this._no_image.Dispose();
					GC.Collect();
				}

				this._no_image = Image.FromFile( this.dialogImage.FileName );
			}
		}

		// ------------------------------------------------------------- gnPixelsPerDPI_ValueChanged ------------------
		
		private void gnPixelsPerDPI_ValueChanged( object sender, EventArgs ev )
		{
			this._settings.g_pixels_per_dpi = (double)this.gnPixelsPerDPI.Value;
		}

#endregion

	}
}
