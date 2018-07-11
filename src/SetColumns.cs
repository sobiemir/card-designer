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

	public partial class SetColumns : Form
	{
		private ListViewItem	_selected_item	= null;
		private DBPageFields[]	_fields			= null;
		private int				_current_page	= 0;
		public static int[]		_checked_items	= new int[2];

		// ------------------------------------------------------------- SetColumns ----------------------------------
		
		public SetColumns( int pages, ref DBPageFields[] fields, string dbfile )
		{
			this.InitializeComponent( );

			// resetuj zaznaczone kolumny
			SetColumns._checked_items[0] = -1;
			SetColumns._checked_items[1] = -1;

			// przetwórz odpowiednio plik
			if( dbfile.EndsWith(".csv") || dbfile.EndsWith(".CSV") )
				this.GetColsFromCSV( dbfile );
			else
				return;

			// zapisz instancje zmiennej
			this._fields = fields;
			
			ImageList image_list = new ImageList( );
			image_list.ColorDepth = ColorDepth.Depth32Bit;

			// wczytaj ikony
			Image image = Image.FromFile( "icons/image-field.png" );
			image_list.Images.Add( "image-field", image );
			
			image = Image.FromFile( "icons/text-field.png" );
			image_list.Images.Add( "text-field", image );

			// ustaw listę obrazków
			this.lvPageFields.SmallImageList = image_list;

			// dodaj rekordy do tabeli
			for( int x = 0; x < fields[0].Fields; ++x )
			{
				if( !fields[0].TextFromDB[x] && !fields[0].ImageFromDB[x] )
					continue;
				
				// dodaj element
				this.lvPageFields.Items.Add( fields[0].Name[x] );

				// przyporządkuj odpowiedni obrazek
				if( fields[0].TextFromDB[x] )
					this.lvPageFields.Items[x].ImageKey = "text-field";
				else if( fields[0].ImageFromDB[x] )
					this.lvPageFields.Items[x].ImageKey = "image-field";

				this.lvPageFields.Items[x].SubItems.Add("");
			}

			// ustaw maksymalną wartość dla pola numerycznego
			this.nPage.Maximum = pages;
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

		// ------------------------------------------------------------- GetColsFromCSV ------------------------------
		
		private void GetColsFromCSV( string dbfile )
		{
			// otwórz plik i pobierz pierwszą linię
			StreamReader file = new StreamReader( dbfile );
			string line = file.ReadLine( );
			int start = 0, stop = 0;

			// odczytaj nazwy kolumn z pierwszej linii
			// kolumna1;kolumna2;kolumna3;...;kolumnaX

			while( (stop = line.IndexOf(';', start)) > 0 )
			{
				this.lvDBCols.Items.Add( line.Substring(start, stop - start) );
				start = stop + 1;
			}
		}

		// ------------------------------------------------------------- lvDBCols_ItemChecked ------------------------
		
		public static void GetDBData( string dbfile, ref SCVData data )
		{
			StreamReader file = new StreamReader( dbfile );
			
			// nazwy kolumn
			string line = file.ReadLine( );
			int columns = line.Count( f => f == ';' ), rows = 0;
			
			// policz rekordy
			while( !file.EndOfStream )
			{
				file.ReadLine( );
				++rows;
			}
			
			// otwórz ponownie (przewiń na początek)
			file.Close( );
			file = new StreamReader( dbfile );

			// przydziel pamięć
			data.columns = columns;
			data.rows = rows;
			data.column = new string[columns];
			data.row = new string[columns,rows];

			int start = 0, stop = 0, column = 0, row = 0;
			line = file.ReadLine( );

			// pobierz nazwy kolumn
			while( (stop = line.IndexOf(';', start)) > 0 )
			{
				data.column[row] = line.Substring( start, stop - start );
				start = stop + 1;
				row++;
			}
			column = 0;

			// iteruj po rekordach
			while( !file.EndOfStream )
			{
				line = file.ReadLine( );
				start = 0;
				row = 0;

				// pobierz nazwy rekordów
				while( (stop = line.IndexOf(';', start)) > 0 )
				{
					data.row[row,column] = line.Substring( start, stop - start );
					start = stop + 1;
					row++;
				}
				column++;
			}
		}

		// ------------------------------------------------------------- lvDBCols_ItemChecked ------------------------
		
		private void lvDBCols_ItemChecked( object sender, ItemCheckedEventArgs ev )
		{
			// nie przetwarzaj gdy przycisk nie jest zaznaczony
			if( !ev.Item.Checked )
				return;
			
			// nie pozwalaj zaznaczyć więcej niż dwóch elementów
			if( this.lvDBCols.CheckedItems.Count > 2 )
				ev.Item.Checked = false;

			// resetuj kolumny
			int index = 0;
			SetColumns._checked_items[0] = -1;
			SetColumns._checked_items[1] = -1;
			
			// zapisz zaznaczone elementy
			foreach( ListViewItem item in this.lvDBCols.Items )
				if( item.Checked )
					SetColumns._checked_items[index++] = item.Index;
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
			if( this._selected_item == item )
				return;
			
			if( this._selected_item != null )
				this._selected_item.Selected = false;

			this._selected_item = item;
			this._selected_item.Selected = true;
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

			// zapisz indeks przeciąganej kolumny
			this._fields[this._current_page].Column[item.Index] = copy.Index;
		}

		// ------------------------------------------------------------- nPage_ValueChanged --------------------------
		
		private void nPage_ValueChanged( object sender, EventArgs ev )
		{
			// pobierz numer strony i wyczyść listę
			int page = (int)this.nPage.Value - 1;
			this.lvPageFields.Items.Clear( );

			// dodaj rekordy do tabeli
			for( int x = 0; x < this._fields[page].Fields; ++x )
			{
				if( !this._fields[page].TextFromDB[x] && !this._fields[page].ImageFromDB[x] )
					continue;

				// dodaj element
				this.lvPageFields.Items.Add( this._fields[page].Name[x] );

				// przyporządkuj odpowiedni obrazek
				if( this._fields[page].TextFromDB[x] )
					this.lvPageFields.Items[x].ImageKey = "text-field";
				else if( this._fields[page].ImageFromDB[x] )
					this.lvPageFields.Items[x].ImageKey = "image-field";

				// uzupełnij pola z zapisanych danych
				if( this._fields[page].Column[x] == -1 )
					this.lvPageFields.Items[x].SubItems.Add("");
				else
					this.lvPageFields.Items[x].SubItems.Add( this.lvDBCols.Items[this._fields[page].Column[x]].Text );
			}

			// przypisz aktualną stronę
			this._current_page = page;
		}

		// ------------------------------------------------------------- bSave_Click ---------------------------------
		
		private void bSave_Click(object sender, EventArgs e)
		{
			// zamknij okno
            this.DialogResult = DialogResult.OK;
            this.Close( );
		}
	}
}
