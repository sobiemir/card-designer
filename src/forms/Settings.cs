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











		private List<string> _last_patterns;

		// ------------------------------------------------------------- Settings -------------------------------------
		
		public Settings()
		{
			this.InitializeComponent();

			// załaduj listę ostatnich używanych wzorów...
			try { this.GetLastPatterns(); }
			catch
			{
				// ta wiadomość nie powinna się pojawić
				MessageBox.Show( "Błąd tworzenia lub otwierania pliku konfiguracyjnego." );
				return;
			}
		}

		// ------------------------------------------------------------- LastPatterns ---------------------------------
		
		public List<string> LastPatterns
		{
			// pobierz ostatnio otwierane wzory
			get { return this._last_patterns; }
			// ustaw ostatnio otwierane wzory
			set
			{
				this._last_patterns = value;
				
				// wyczyść zawartość pliku (po prostu utwórz nowy)
				File.Delete("last.lst");
				
				try { File.Create("last.lst"); }
				catch
				{
					// ta wiadomość nie powinna się pojawić
					MessageBox.Show( "Błąd tworzenia lub otwierania pliku konfiguracyjnego." );
					return;
				}
			}
		}

		// ------------------------------------------------------------- GetLastPatterns ------------------------------
		
		private void GetLastPatterns()
		{
			// utwórz plik gdy nie istnieje
			if( !File.Exists("last.lst") )
			{
				File.Create( "last.lst" );
				this._last_patterns = new List<string>();

				return;
			}

			// wczytaj ostatnie projekty
			this._last_patterns = new List<string>( File.ReadLines( "last.lst" ) );
		}

		// ------------------------------------------------------------- AddToLastPatterns ----------------------------
		
		public void AddToLastPatterns( string pattern )
		{
			// usuń wzór jeżeli już taki istnieje
			if( this._last_patterns.Contains(pattern) )
				this._last_patterns.Remove( pattern );
			
			// maksymalna ilość wyświetlanych plików
			if( this._last_patterns.Count > 10 )
				this._last_patterns.RemoveAt( 9 );

			// dodaj wzór do listy
			this._last_patterns.Insert( 0, pattern );

			// zapisz nowe ustawienie ostatnich wzorów
			File.WriteAllLines( "last.lst", this._last_patterns.AsEnumerable() );
		}

		// ------------------------------------------------------------- AddToLastPatterns ----------------------------
		
		public void RemoveFromLastPatterns( string pattern )
		{
			// usuń i zapisz listę wzorów
			if( this._last_patterns.Remove(pattern) )
				File.WriteAllLines( "last.lst", this._last_patterns.AsEnumerable() );
		}













		// ------------------------------------------------------------- Settings -------------------------------------
		
		public Settings( int page, int subpage = -1 )
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
		}

		// ------------------------------------------------------------- FillOptions ----------------------------------
		
		private void FillOptions( )
		{
			// obrazki
			this._transparent = Image.FromFile( "transparent.png" );
			this._no_image    = Image.FromFile( "noimage.png" );

			// ustawienia głównes
			this.gnPadding.Value           = (decimal)this._settings.g_padding;
			this.gcbPrevPos.SelectedIndex  = this._settings.g_page_float;
			this.gcbAskBeforeClose.Checked = this._settings.g_ask_before_close;
			this.gcbSaveSlide.Checked      = this._settings.g_save_sliders;
			this.gcbShortcuts.Checked      = this._settings.g_shortcuts_on;
			this.gcbInfoPanel.Checked      = this._settings.g_show_status;
			this.gnPixelsPerDPI.Value      = (decimal)this._settings.g_pixels_per_dpi;

			this.rnMinRes.Value       = (decimal)this._settings.r_min_res;
			this.rnMaxRes.Value       = (decimal)this._settings.r_max_res;
			this.rcbCustomRes.Checked = this._settings.r_custom_res;

			this.FillResRecords();
		}

		// ------------------------------------------------------------- FillResRecords -------------------------------
		
		public void FillResRecords( )
		{
			this.rlvResList.Items.Clear();
			this.gcbResolution.Items.Clear();

			// dodaj elementy do list
			const int a4w = 210, a4h = 297;
			for( int x = 0; x < this._settings.r_list.Count; ++x )
			{
				int    dpi = this._settings.r_list[x];
				double pxs = this._settings.g_pixels_per_dpi;
				double pxd = (double)dpi / 100.0 * pxs;

				// dodaj element do list
				ListViewItem item = this.rlvResList.Items.Add( dpi.ToString() );
				item.SubItems.Add( (Convert.ToInt32((double)a4w * pxd)).ToString() + " x " + (Convert.ToInt32((double)a4h * pxd)).ToString() + " px" );

				// dodaj element do listy rozwijanej
				int cbidx = this.gcbResolution.Items.Add( dpi.ToString() );
				if( dpi == this._settings.g_def_scale )
					this.gcbResolution.SelectedIndex = cbidx;
			}

			// brak elementów?
			if( this.gcbResolution.Items.Count == 0 )
				return;

			// zaznacz pierwszy z brzegu gdy nie jest wybrany żaden lub wpisz gdy program pozwala na własne wartości
			if( this.gcbResolution.SelectedIndex < 0 )
			{
				if( this._settings.r_custom_res )
					this.gcbResolution.Text = this._settings.g_def_scale.ToString();
				else
					this.gcbResolution.SelectedIndex = 0;
			}
		}

		// ------------------------------------------------------------- OpenSettings ---------------------------------
		
		public void LoadSettings( )
		{
			this._settings = new SettingsInfo();

			this._settings.g_def_scale      = 100;
			this._settings.g_back_color     = SystemColors.ScrollBar;
			this._settings.g_padding        = 5;
			this._settings.g_page_float     = 1;
			this._settings.g_pixels_per_dpi = 3.93714927048264;

			this._settings.g_ask_before_close = false;
			this._settings.g_save_sliders     = false;
			this._settings.g_shortcuts_on     = true;
			this._settings.g_show_status      = true;

			this._settings.r_min_res    = 50;
			this._settings.r_max_res    = 300;
			this._settings.r_list       = new List<int> { 50, 75, 100, 150, 200, 300 };
			this._settings.r_custom_res = true;
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
				
				this.pPagePreview.Show();
				this.lPreview.Hide();
			break;
			case 2:
				current = this.tlResolutions;

				this.pPagePreview.Hide();
				this.lPreview.Show();
			break;
			case 3:
				current = this.tlEditor;
				
				this.pPagePreview.Hide();
				this.lPreview.Show();
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

#region Rozdzielczości

		// ------------------------------------------------------------- rnMinRes_ValueChanged ------------------------
		
		private void rnMinRes_ValueChanged( object sender, EventArgs ev )
		{
			int num = (int)this.rnMinRes.Value,
				cnt = 0;

			// różnica pomiędzy największą a najmniejszą rozdzielczością musi być co najmniej 25 DPI
			if( num + 25 > this._settings.r_max_res )
			{
				this.rnMinRes.Value = this._settings.r_max_res - 25;
				return;
			}

			this._settings.r_min_res  = num;
			this.rnResolution.Minimum = num;

			// szukaj mniejszych rozdzielczości
			for( int x = 0; x < this._settings.r_list.Count; ++x )
				if( this._settings.r_list[x] < num )
					cnt++;

			// usuń elementy przekraczające zakres
			if( cnt > 0 )
			{
				this._settings.r_list.RemoveRange( 0, cnt );
			
				// wypełnij ponownie liste
				this.FillResRecords();
			}

			// dodaj minimalną rozdzielczość w przypadku usunięcia wszystkich
			if( this.rlvResList.Items.Count == 0 )
			{
				this._settings.r_list.Add( this._settings.r_min_res );
				this.FillResRecords();
			}
		}

		// ------------------------------------------------------------- rnMaxRes_ValueChanged ------------------------
		
		private void rnMaxRes_ValueChanged( object sender, EventArgs ev )
		{
			int num = (int)this.rnMaxRes.Value,
				cnt = 0;

			// różnica pomiędzy największą a najmniejszą rozdzielczością musi być co najmniej 25 DPI
			if( this._settings.r_min_res + 25 > num )
			{
				this.rnMaxRes.Value = this._settings.r_min_res + 25;
				return;
			}

			this._settings.r_max_res  = num;
			this.rnResolution.Maximum = num;

			// szukaj większych rozdzielczości
			for( int x = 0; x < this._settings.r_list.Count; ++x )
				if( this._settings.r_list[x] > num )
					cnt++;

			if( cnt > 0 )
			{
				this._settings.r_list.RemoveRange( this._settings.r_list.Count - cnt, cnt );

				// wypełnij ponownie liste
				this.FillResRecords();
			}

			// dodaj minimalną rozdzielczość w przypadku usunięcia wszystkich
			if( this.rlvResList.Items.Count == 0 )
			{
				this._settings.r_list.Add( this._settings.r_min_res );
				this.FillResRecords();
			}
		}

		// ------------------------------------------------------------- rbCustomRes_CheckedChanged -------------------
		
		private void rcbCustomRes_CheckedChanged( object sender, EventArgs ev )
		{
			// zmień typ kontrolki
			if( this.rcbCustomRes.Checked )
				this.gcbResolution.DropDownStyle = ComboBoxStyle.DropDown;
			else
			{
				this.gcbResolution.DropDownStyle = ComboBoxStyle.DropDownList;
				this.gcbResolution.SelectedIndex = 0;
			}
			this._settings.r_custom_res = this.rcbCustomRes.Checked;
		}

		// ------------------------------------------------------------- rlvResList_SelectedIndexChanged --------------
		
		private void rlvResList_SelectedIndexChanged( object sender, EventArgs ev )
		{
			if( this.rlvResList.SelectedItems.Count == 0 )
				return;

			int num = 0;

			// konwertuj ciąg znaków na int
			try { num = Convert.ToInt32( this.rlvResList.SelectedItems[0].Text ); }
			catch { num = 100; }

			this.rnResolution.Value = num;
		}

		// ------------------------------------------------------------- rbAddRes_Click -------------------------------
		
		private void rbAddRes_Click( object sender, EventArgs ev )
		{
			int num = (int)this.rnResolution.Value;

			// nie pozwalaj dodać takiej samej rozdzielczości
			for( int x = 0; x < this._settings.r_list.Count; ++x )
				if( this._settings.r_list[x] == num )
					return;
			
			// dodaj element do tablicy i posortuj ją
			this._settings.r_list.Add( num );
			this._settings.r_list.Sort();

			// wyczyść i uzupełnij tablicę od nowa
			this.FillResRecords();
		}

		// ------------------------------------------------------------- rbChangeRes_Click ----------------------------
		
		private void rbChangeRes_Click( object sender, EventArgs ev )
		{
			// brak zaznaczonych rekordów
			if( this.rlvResList.SelectedItems.Count <= 0 )
				return;

			int num = (int)this.rnResolution.Value;

			// nie pozwalaj zmienić na istniejącą rozdzielczość
			for( int x = 0; x < this._settings.r_list.Count; ++x )
				if( this._settings.r_list[x] == num )
					return;

			// zmień element i posortuj tablice
			int index = this.rlvResList.SelectedItems[0].Index;
			this._settings.r_list[index] = num;
			this._settings.r_list.Sort();

			// wyczyść i uzupełnij tablicę od nowa
			this.FillResRecords();
		}

		// ------------------------------------------------------------- rbRemoveRes_Click ----------------------------
		
		private void rbRemoveRes_Click( object sender, EventArgs ev )
		{
			// brak zaznaczonych rekordów
			if( this.rlvResList.SelectedItems.Count <= 0 )
				return;

			if( this.rlvResList.Items.Count < 2 )
			{
				MessageBox.Show( this, "Nie możesz usunąć wszystkich rozdzielczości!", "Błąd usuwania rozdzielczości." );
				return;
			}

			// usuń zaznaczony rekord
			int index = this.rlvResList.SelectedItems[0].Index;
			this.rlvResList.Items.RemoveAt( index );
			this._settings.r_list.RemoveAt( index );
		}

#endregion


	}
}
