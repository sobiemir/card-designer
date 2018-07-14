using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace CDesigner
{
	public struct SCVData
	{
		public string[] column;
		public int rows;
		public int columns;
		public string[,] row;
	}

	public partial class DataReader : Form
	{
		private DataContent  _data_content = new DataContent();
		private PatternData  _pattern_data = new PatternData();
		private ListViewItem _selected_col = null;
		private string       _data_file    = null;
		private List<int>    _checked_cols = new List<int>();
		private int          _current_page = 0;
		private int          _selected_idx = 0;
		private Encoding     _encoding     = Encoding.Default;

		// ------------------------------------------------------------- DataContent ---------------------------------
		
		public DataContent DataContent
		{
			get { return this._data_content; }
		}

		// ------------------------------------------------------------- DataFile ------------------------------------
		
		public string DataFile
		{
			get { return this._data_file; }
		}

		// ------------------------------------------------------------- PatternData ---------------------------------
		
		public PatternData PatternData
		{
			get { return this._pattern_data; }
		}

		// ------------------------------------------------------------- CheckedCols ---------------------------------
		
		public List<int> CheckedCols
		{
			get { return this._checked_cols; }
		}

		// ------------------------------------------------------------- CheckedFormat -------------------------------
		
		public string CheckedFormat
		{
			get { return this.tbSpaces.Text; }
		}

		// ------------------------------------------------------------- DataReader ----------------------------------
		
		public DataReader( PatternData data, string dbase )
		{
			if( !File.Exists(dbase) || data.pages < 1 )
				return;

			this._data_file = dbase;
			this.InitializeComponent();
			this.cbEncoding.SelectedIndex = 0;

			// sprawdź rozszerzenie pliku
			if( dbase.EndsWith(".csv", StringComparison.OrdinalIgnoreCase) )
				this._data_content = this.LoadDataCSV( Encoding.Default, true );
			else
				return;

			// uzupełnij tabele
			for( int x = 0; x < this._data_content.columns; ++x )
				this.lvDBCols.Items.Add( this._data_content.column[x] );

			this._pattern_data    = data;
			ImageList image_list  = new ImageList();
			image_list.ColorDepth = ColorDepth.Depth32Bit;

			// wczytaj ikony
			Image image = Image.FromFile( "icons/image-field.png" );
			image_list.Images.Add( "image-field", image );
			
			image = Image.FromFile( "icons/text-field.png" );
			image_list.Images.Add( "text-field", image );

			// ustaw listę obrazków
			this.lvPageFields.SmallImageList = image_list;

			// uzupełnij tabele
			for( int x = 0; x < data.page[0].fields; ++x )
			{
				FieldData field_data = data.page[0].field[x];

				// pomiń pola, których nie można przypisać
				if( !field_data.extra.text_from_db && !field_data.extra.image_from_db )
					continue;

				// dodaj pola
				ListViewItem item = this.lvPageFields.Items.Add( field_data.name );
				
				// zapisz indeks...
				item.Tag = (object)x;

				// ustaw ikonki
				if( field_data.extra.text_from_db )
					item.ImageKey = "text-field";
				else if( field_data.extra.image_from_db )
					item.ImageKey = "image-field";

				item.SubItems.Add("");
			}

			// ustaw maksymalną wartość dla pola numerycznego
			this.nPage.Maximum = data.pages;
		}

		// ------------------------------------------------------------- ProcessCmdKey -------------------------------
		
		protected override bool ProcessCmdKey( ref Message msg, Keys keydata )
		{
			switch( keydata )
			{
				// zmień strone wzoru
				case Keys.Control | Keys.Tab:
					if( this.nPage.Value < this.nPage.Maximum )
						this.nPage.Value += 1;
				break;
				// zmień strone wzoru
				case Keys.Shift | Keys.Tab:
					if( this.nPage.Value > this.nPage.Minimum )
						this.nPage.Value -= 1;
				break;
				default:
					return base.ProcessCmdKey( ref msg, keydata );
			}
			return true;
		}

		// ------------------------------------------------------------- LoadData ------------------------------------
		
		public void ReloadData( )
		{
			// sprawdź rozszerzenie pliku
			if( this._data_file.EndsWith(".csv", StringComparison.OrdinalIgnoreCase) )
				this._data_content = this.LoadDataCSV( this._encoding );
		}

		// ------------------------------------------------------------- LoadData ------------------------------------
		
		private DataContent LoadDataCSV( Encoding encoding, bool only_cols = false )
		{
			DataContent  data = new DataContent();
			StreamReader file = new StreamReader( this._data_file, encoding, true );
			
			int chr = 0, columns = 1, rows = 0;
			this._encoding = encoding;

			// policz ilość kolumn
			while( (chr = file.Read()) != -1 )
				if( chr == '\n' )
					break;
				else if( chr == ';' )
					++columns;

			// policz ilość wierszy
			while( (chr = file.Read()) != -1 )
				if( chr == '\n' )
					++rows;

			// zamknij plik
			file.Close();

			// przydziel pamięć
			data.columns = columns;
			data.column  = new string[columns];
			data.rows    = rows;
			data.row     = new string[rows,columns];

			int column = 0, row = 0;
			file = new StreamReader( this._data_file, encoding, true );

			// pobierz nazwy kolumn
			while( (chr = file.Read()) != -1 )
				if( chr == '\n' )
					break;
				else if( chr == ';' )
					++column;
				else
					data.column[column] += (char)chr;

			// nie przetwarzaj dalej - potrzeba tylko kolumny
			if( only_cols )
			{
				file.Close();
				return data;
			}

			// resetuj licznik kolumn
			column = 0;

			// pobierz wiersze
			while( (chr = file.Read()) != -1 )
				if( chr == '\n' )
				{
					++row;
					column = 0;
				}
				else if( chr == ';' )
					++column;
				else
					data.row[row,column] += (char)chr;
			
			// zamknij strumień
			file.Close();
			return data;
		}

		// ------------------------------------------------------------- cbEncoding_SelectedIndexChanged -------------
		
		private void cbEncoding_SelectedIndexChanged( object sender, EventArgs ev )
		{
			if( this.cbEncoding.SelectedIndex == this._selected_idx )
				return;

			DataContent data     = new DataContent();
			Encoding    encoding = Encoding.Default;

			switch( this.cbEncoding.SelectedIndex )
			{
				case 1: encoding = Encoding.ASCII; break;
				case 2: encoding = Encoding.UTF8;  break;
				case 3: encoding = Encoding.BigEndianUnicode; break;
				case 4: encoding = Encoding.Unicode; break;
				case 5: encoding = Encoding.UTF32; break;
				case 6: encoding = Encoding.UTF7; break;
			}

			// wyczyść rekordy w tabeli
			this.lvDBCols.Items.Clear();

			// sprawdź rozszerzenie pliku
			if( this._data_file.EndsWith(".csv", StringComparison.OrdinalIgnoreCase) )
				this._data_content = this.LoadDataCSV( encoding, true );
			else
				return;

			// uzupełnij tabele nowymi danymi
			for( int x = 0; x < this._data_content.columns; ++x )
				this.lvDBCols.Items.Add( this._data_content.column[x] );

			// odśwież wiersze
			for( int x = 0; x < this._pattern_data.page[this._current_page].fields; ++x )
			{
				FieldData field_data = this._pattern_data.page[this._current_page].field[x];

				// pomiń pola, których nie można przypisać
				if( !field_data.extra.text_from_db && !field_data.extra.image_from_db )
					continue;

				// odśwież tekst
				if( field_data.extra.column != -1 )
					try { this.lvPageFields.Items[x].SubItems[1].Text = this._data_content.column[field_data.extra.column]; }
					catch { this.lvPageFields.Items[x].SubItems[1].Text = "brak kolumny..."; }
			}

			this._data_content = data;
			this._selected_idx = this.cbEncoding.SelectedIndex;
		}

		// ------------------------------------------------------------- nPage_ValueChanged --------------------------
		
		private void nPage_ValueChanged( object sender, EventArgs ev )
		{
			// pobierz numer strony i wyczyść listę
			int page = (int)this.nPage.Value - 1;
			this.lvPageFields.Items.Clear();

			PageData  page_data = this._pattern_data.page[page];
			FieldData field_data;

			// dodaj rekordy do tabeli
			for( int x = 0; x < page_data.fields; ++x )
			{
				field_data = page_data.field[x];
				
				// pomiń pola których nie można przypisać
				if( !field_data.extra.text_from_db && !field_data.extra.image_from_db )
					continue;

				// dodaj element
				this.lvPageFields.Items.Add( field_data.name );

				// przyporządkuj odpowiedni obrazek
				if( field_data.extra.text_from_db )
					this.lvPageFields.Items[x].ImageKey = "text-field";
				else if( field_data.extra.image_from_db )
					this.lvPageFields.Items[x].ImageKey = "image-field";

				if( field_data.extra.column == -1 )
					this.lvPageFields.Items[x].SubItems.Add("");
				else
					try { this.lvPageFields.Items[x].SubItems.Add( this._data_content.column[field_data.extra.column] ); }
					catch { this.lvPageFields.Items[x].SubItems.Add( "brak kolumny..." ); }

				this.lvPageFields.Items[x].Tag = (object)x;
			}

			// przypisz aktualną stronę
			this._current_page = page;
		}

		// ------------------------------------------------------------- lvDBCols_MouseDown --------------------------
		
		private void lvDBCols_MouseDown( object sender, MouseEventArgs ev )
		{
			ListViewItem item = this.lvDBCols.GetItemAt( ev.X, ev.Y );

			if( item == null )
				return;

			// zaznacz wybrany element
			item.Selected = true;

			// pobierz element i wywołaj funkcję przeciągania (DragAndDrop)
			ListView.SelectedListViewItemCollection rows = this.lvDBCols.SelectedItems;
			foreach( ListViewItem row in rows )
				this.lvDBCols.DoDragDrop( row, DragDropEffects.Move );
		}

		// ------------------------------------------------------------- lvDBCols_DragOver ---------------------------
		
		private void lvDBCols_DragOver( object sender, DragEventArgs ev )
		{
			ev.Effect = DragDropEffects.Move;
		}

		// ------------------------------------------------------------- lvDBCols_ItemChecked ------------------------
		
		private void lvDBCols_ItemChecked( object sender, ItemCheckedEventArgs ev )
		{
			int counter = 0;
			this.tbSpaces.Text = "";

			foreach( ListViewItem item in lvDBCols.Items )
				if( item.Checked && counter == 0 )
					this.tbSpaces.Text = "#" + (++counter);
				else if( item.Checked )
					this.tbSpaces.Text += " " + "#" + (++counter);
		}

		// ------------------------------------------------------------- lvPageFields_DragEnter ----------------------
		
		private void lvPageFields_DragEnter( object sender, DragEventArgs ev )
		{
			ev.Effect = DragDropEffects.Move;
		}

		// ------------------------------------------------------------- lvPageFields_DragOver -----------------------
		
		private void lvPageFields_DragOver( object sender, DragEventArgs ev )
		{
			// wskazywany element
			Point client_point = this.lvPageFields.PointToClient( new Point(ev.X, ev.Y) );
			ListViewItem item = this.lvPageFields.GetItemAt( client_point.X, client_point.Y );

			// zabezpieczenie przed zaznaczeniem kilku (lub miganiem zaznaczonych elementów)
			if( this._selected_col == item )
				return;
			
			if( this._selected_col != null )
				this._selected_col.Selected = false;

			this._selected_col = item;
			this._selected_col.Selected = true;
		}

		// ------------------------------------------------------------- lvPageFields_DragDrop -----------------------
		
		private void lvPageFields_DragDrop( object sender, DragEventArgs ev )
		{
			// wskazywany element
			Point client_point = this.lvPageFields.PointToClient( new Point(ev.X, ev.Y) );
			ListViewItem item = this.lvPageFields.GetItemAt( client_point.X, client_point.Y );

			// przeciągany wiersz
			ListViewItem copy = (ListViewItem)ev.Data.GetData( typeof(ListViewItem) );
			item.SubItems[1].Text = copy.Text;

			// sprawdź czy kontrolka ma odpowiedni kolor
			if( copy.BackColor != SystemColors.Window )
				copy.BackColor = SystemColors.Window;

			// zapisz indeks przeciąganej kolumny
			this._pattern_data.page[this._current_page].field[(int)item.Tag].extra.column = copy.Index;
		}

		// ------------------------------------------------------------- lvPageFields_KeyDown ------------------------
		
		private void lvPageFields_KeyDown( object sender, KeyEventArgs ev )
		{
			// usuń przypisaną kolumnę
			if( ev.KeyCode == Keys.Delete && this.lvPageFields.SelectedItems.Count > 0 )
				this.lvPageFields.SelectedItems[0].SubItems[1].Text = "";
		}

		// ------------------------------------------------------------- bSave_Click ---------------------------------
		
		private void bSave_Click( object sender, EventArgs ev )
		{
			bool empty = this.tbSpaces.Text.Trim() == "";

			this._checked_cols.Clear();

			// pobierz zaznaczone
			for( int x = 0; x < this.lvDBCols.Items.Count; ++x )
				if( this.lvDBCols.Items[x].Checked )
				{
					this._checked_cols.Add(x);

					if( this.tbSpaces.Text.IndexOf("#" + this._checked_cols.Count) < 0 || empty )
					{
						MessageBox.Show
						(
							this,
							"Formatowanie kolumn nie zawiera zaznaczonej kolumny \"" + this.lvDBCols.Items[x].Text + "\"" +
							" (#" + this._checked_cols.Count + ")",
							"Błąd formatowania kolumn..."
						);
						return;
					}
				}

			// zamknij okno
            this.DialogResult = DialogResult.OK;
            this.Close( );
		}
	}
}
