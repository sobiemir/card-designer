using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using CDesigner.Utils;

// TODO: Dodać kopiowanie rekordów
// TODO: Dodać do typów kolumn walute i date

namespace CDesigner
{
	public partial class EditDataForm : Form
	{
		private DatabaseReader _reader = null;
		private int _rows_per_page = 0;
		private int _page = 0;
		private int _pages = 1;
		private int _first_row = 0;
		private List<string[]> _data = null;
		private int _delete_id = 0;
		private int _delete_pending = 0;
		private List<int> _deleting_rows = new List<int>();
		private bool _locked = false;
		private List<bool> _cols_selected = null;

		///
		/// ------------------------------------------------------------------------------------------------------------
		public int Page
		{
			get { return this._page + 1; }
			set
			{
				if( value > this._pages )
					value = this._pages;
				else if( value < 1 )
					value = 1;

				this._page = value - 1;
			}
		}

		///
		/// ------------------------------------------------------------------------------------------------------------
		public int RowsPerPage
		{
			get { return this._rows_per_page; }
			set
			{
				if( value < 0 )
					value = 1;

				this._rows_per_page = value;
			}
		}

		///
		/// ------------------------------------------------------------------------------------------------------------
		public int Pages
		{
			get { return this._pages; }
		}

		public EditDataForm( bool standalone, bool debug, bool run ) : this()
		{
			if( !standalone || !debug || !run )
				return;

			DatabaseReader reader = new DatabaseReader(@"C:\Users\Kamil\Desktop\Inne\zzz2.CSV");
			reader.Parse();

			this.SetDataSource( reader );
			this.RefreshDataRange();
		}

		///
		/// ------------------------------------------------------------------------------------------------------------
		public EditDataForm()
		{
			this.InitializeComponent();

			this._rows_per_page = Settings.Info.EDF_RowsNumber;
			this._data          = new List<string[]>();
			this._cols_selected = new List<bool>();

			// podwójne buforowanie siatki (przerzuca obliczenia na GPU...)
			Type type = this.gvData.GetType();
			PropertyInfo info = type.GetProperty( "DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic );
			info.SetValue( this.gvData, true, null );

			// ikony przycisków
			this.bInsertRow.Image = Program.GetBitmap( BITMAPCODE.ItemAdd );
			this.bRemoveRow.Image = Program.GetBitmap( BITMAPCODE.ItemRemove );
			this.bFirstPage.Image = Program.GetBitmap( BITMAPCODE.FirstPage );
			this.bPrevPage.Image  = Program.GetBitmap( BITMAPCODE.PrevPage );
			this.bNextPage.Image  = Program.GetBitmap( BITMAPCODE.NextPage );
			this.bLastPage.Image  = Program.GetBitmap( BITMAPCODE.LastPage );

			this.Text = Language.GetLine( "FormNames", (int)FORMLIDX.EditData );
			this.Icon = Program.GetIcon();

			this.tbRowsPerPage.Text = Settings.Info.EDF_RowsNumber.ToString();
			this.lRowsPerPage.Text  = Language.GetLine( "EditData", "Labels", 1 );
			this.tbPageNum.Text     = "1";

			this.gbType.Text = Language.GetLine( "EditData", "Labels", 2 );

			// pobierz nazwy typów danych
			List<string> types = Language.GetLines( "EditData", "ComboBoxTypes" );

			// wyczyść elementy
			this.cbColumnType.Items.Clear();

			// dodaj przetłumaczone typy
			this.cbColumnType.Items.Add( String.Format(types[0], "abcdef") );
			this.cbColumnType.Items.Add( String.Format(types[1], "512") );
			this.cbColumnType.Items.Add( String.Format(types[2], (5.12).ToString()) );
			this.cbColumnType.Items.Add( String.Format(types[3], "a") );
		}

		///
		/// ------------------------------------------------------------------------------------------------------------
		public void SetDataSource( DatabaseReader reader )
		{
			this._reader = reader;

			// wyczyść poprzednie dane
			if( this._data.Count > 0 )
				this._data.Clear();
			
			// wyczyść wiersze
			this.gvData.Rows.Clear();
			this.gvData.Columns.Clear();

			GC.Collect();

			// ustaw nowy rozmiar listy
			this._data.Capacity = reader.RowsNumber;

			// przypisz nowe elementy
			for( int x = 0; x < reader.RowsNumber; ++x )
			{
				this._data.Add( new string[reader.ColumnsNumber] );

				for( int y = 0; y < reader.ColumnsNumber; ++y )
					this._data[x][y] = reader.Rows[y,x];
			}

			// dodaj kolumnę identyfikatora
			this.gvData.Columns.Add( "gvcID", "ID" );
			this.gvData.Columns[0].SortMode = DataGridViewColumnSortMode.NotSortable;
			this.gvData.Columns[0].ReadOnly = true;
			this.gvData.Columns[0].Width = 40;
			this.gvData.Columns[0].DefaultCellStyle.BackColor = SystemColors.Control;

			// dodaj kolumny z bazy
			for( int x = 0; x < reader.ColumnsNumber; ++x )
			{
				this.gvData.Columns.Add( "gvc" + x, reader.Columns[x] );
				this.gvData.Columns[x+1].SortMode = DataGridViewColumnSortMode.NotSortable;
			}

			// wyczyść listę kolumn
			this.lvColumns.Items.Clear();
			this._cols_selected.Clear();

			// zablokuj
			this._locked = true;

			// dodaj kolumny z bazy
			for( int x = 0; x < reader.ColumnsNumber; ++x )
			{
				this.lvColumns.Items.Add( reader.Columns[x] );
				this.lvColumns.Items[x].Checked = true;

				this._cols_selected.Add( false );
			}

			// odblokuj
			this._locked = false;
		}

		///
		/// ------------------------------------------------------------------------------------------------------------
		public void RefreshDataRange()
		{
			int totalrows = 0;

			this.gvData.SuspendLayout();

			// wyświetl wszystkie wiersze na jednej stronie
			if( this._rows_per_page == 0 )
			{
				// ilość wszystkich stron
				this._pages = 1;
				this.Page   = 1;
			
				// indeks elementu początkowego i ostatniego
				int startelem = 0;
				int endelem   = this._data.Count;

				// ilość wyświetlanych wierszy
				totalrows = endelem - startelem;
			
				// zapisz indeksy pierwszego i ostatniego elementu
				this._first_row = startelem;
			}
			// podziel wiersze na strony
			else
			{
				// ilość wszystkich stron
				this._pages = this._data.Count / this._rows_per_page + 1;
			
				// zmień stronę jeżeli indeks wykracza poza granicę
				if( this._page > this._pages - 1 )
					this.Page = this._pages;
			
				// indeks elementu początkowego i ostatniego
				int startelem = this._page * this._rows_per_page;
				int endelem   = (this._page + 1) * this._rows_per_page;

				// poprawka ostatniego elementu (gdy wykracza poza faktyczną ilość elementów w tablicy)
				if( endelem > this._data.Count )
					endelem = this._data.Count;

				// ilość wyświetlanych wierszy
				totalrows = endelem - startelem;
			
				// zapisz indeksy pierwszego i ostatniego elementu
				this._first_row = startelem;
			}

			// dodaj pusty wiersz gdy brak wierszy (raczej nie powinno się zdarzyć)
			if( this.gvData.Rows.Count == 0 )
				this.gvData.Rows.Add();
			
			// ilość wierszy...
			int cond = this.gvData.AllowUserToAddRows
				? this.gvData.Rows.Count - 1
				: this.gvData.Rows.Count;

			// usuń wiersze
			if( cond > totalrows )
			{
				// szybciej jest wyczyścić wszystko i dodać wiersze niż usunąć...
				this.gvData.Rows.Clear();

				if( totalrows > 0 )
					this.gvData.Rows.Add( totalrows );
			}
			// dodaj wiersze
			else if( cond < totalrows )
				this.gvData.Rows.Add( totalrows - (this.gvData.Rows.Count - 1) );
			
			// zablokuj możliwość dodawania wierszy do środka tabeli
			if( totalrows == this._rows_per_page )
				this.gvData.AllowUserToAddRows = false;
			else
				this.gvData.AllowUserToAddRows = true;

			this.gvData.ResumeLayout( false );

			// wyświetl ilość stron i aktualną stronę
			this.lPageStat.Text = String.Format( Language.GetLine("EditData", "Labels", 0), this._pages );
			this.tbPageNum.Text = (this._page + 1).ToString();

			// odśwież kontrolkę
			this.gvData.Refresh();
		}

		///
		/// ------------------------------------------------------------------------------------------------------------
		protected override bool ProcessCmdKey( ref Message msg, Keys keys )
		{
			// pokaż / ukryj panel boczny
			if( keys == Keys.F2 )
			{
				this.scMain.Panel2Collapsed = !this.scMain.Panel2Collapsed;
				return true;
			}
			return base.ProcessCmdKey( ref msg, keys );
		}

		///
		/// ------------------------------------------------------------------------------------------------------------
		private void gvData_CellValueNeeded( object sender, DataGridViewCellValueEventArgs ev )
		{
			// kolumna indeksu
			if( ev.ColumnIndex == 0 )
				ev.Value = ev.RowIndex + this._first_row + 1;
			// wiersz spoza zakresu...
			else if( ev.RowIndex + this._first_row >= this._data.Count )
				ev.Value = "";
			// pobierz wartość z tablicy
			else
				ev.Value = this._data[ev.RowIndex + this._first_row][ev.ColumnIndex-1];
		}

		///
		/// ------------------------------------------------------------------------------------------------------------
		private void gvData_CellValuePushed( object sender, DataGridViewCellValueEventArgs ev )
		{
			bool need_refresh = false;
			int  row          = ev.RowIndex + this._first_row;

			// sprawdź czy indeks nie wykracza poza zakres
			if( row >= this._data.Count )
			{
				// jeżeli wykracza za dużo, nic nie rób...
				if( row >= this._data.Count + 1 )
					return;

				// jeżeli tylko o jeden wiersz, dodaj go
				this._data.Add( new string[this._reader.ColumnsNumber] );

				// i uzupełnij go pustymi wartościami
				for( int x = 0; x < this._reader.ColumnsNumber; ++x )
					this._data[row][x] = "";

				need_refresh = true;
			}

			// uzupełnij pustą lub zmień wartość wybranej komórki
			this._data[ev.RowIndex + this._first_row][ev.ColumnIndex-1] = ev.Value.ToString();

			// odśwież dane
			if( need_refresh )
				this.RefreshDataRange();
		}

		///
		/// ------------------------------------------------------------------------------------------------------------
		private void gvData_UserDeletingRow( object sender, DataGridViewRowCancelEventArgs ev )
		{
			// oczekiwanie na usuwanie
			if( this._delete_pending > 0 )
				return;

			// jeżeli zaznaczonych jest więcej niż jeden rekord, ustaw oczekiwanie na usunięcie
			if( this.gvData.SelectedRows.Count > 1 )
			{
				// pobierz indeksy usuwanych wierszy
				for( int x = 0; x < this.gvData.SelectedRows.Count; ++x )
				{
					// pomiń nowy wiersz
					if( this.gvData.AllowUserToAddRows &&
						this.gvData.SelectedRows[x].Index == this.gvData.Rows.Count - 1 )
						continue;

					this._deleting_rows.Add( this.gvData.SelectedRows[x].Index );
				}

				// oczekiwanie jest równe ilości wierszy do usunięcia
				this._delete_pending = this._deleting_rows.Count;
				return;
			}

			// nowo dodany element podczas usuwania sygnalizuje wartość o 1 większą
			// dzieje się tak ponieważ usuwany jest wiersz po nim, a nie obecny...
			if( ev.Row.Index + this._first_row >= this._data.Count )
			{
				// teraz to już przesadziło...
				if( ev.Row.Index + this._first_row - 1 >= this._data.Count )
				{
					this._delete_id = -1;
					return;
				}
				this._delete_id = ev.Row.Index + this._first_row - 1;
			}
			else
				this._delete_id = ev.Row.Index + this._first_row;
		}

		///
		/// ------------------------------------------------------------------------------------------------------------
		private void gvData_UserDeletedRow( object sender, DataGridViewRowEventArgs ev )
		{
			// oczekiwanie włączone...
			if( this._delete_pending > 0 )
			{
				// ostatni "tik" oczekiwania, usuń wszystkie zaznaczone wiersze
				if( this._delete_pending == 1 )
				{
					// uporządkuj malejąco (usuwanie od największego)
					this._deleting_rows.OrderByDescending( w => w );

					for( int x = 0; x < this._deleting_rows.Count; ++x )
						this._data.RemoveAt( this._deleting_rows[x] );

					// wyczyść tablicę
					this._deleting_rows.Clear();

					// odśwież widok
					this.RefreshDataRange();
				}

				// nie pozwól na wykonanie kodu poniżej podczas oczekiwania
				this._delete_pending--;
				return;
			}

			// usuń wiersz
			if( this._delete_id != -1 )
				this._data.RemoveAt( this._delete_id );

			// odśwież widok
			this.RefreshDataRange();
		}

		///
		/// ------------------------------------------------------------------------------------------------------------
		private void bLastPage_Click( object sender, EventArgs ev )
		{
			if( this._page == this._pages -1 )
				return;

			this._page = this._pages - 1;

			this.RefreshDataRange();
		}

		///
		/// ------------------------------------------------------------------------------------------------------------
		private void bFirstPage_Click( object sender, EventArgs ev )
		{
			if( this._page == 0 )
				return;

			this._page = 0;

			this.RefreshDataRange();
		}

		///
		/// ------------------------------------------------------------------------------------------------------------
		private void bPrevPage_Click( object sender, EventArgs ev )
		{
			if( this._page == 0 )
				return;

			this._page -= 1;

			this.RefreshDataRange();
		}

		///
		/// ------------------------------------------------------------------------------------------------------------
		private void bNextPage_Click( object sender, EventArgs ev )
		{
			if( this._page == this._pages - 1 )
				return;

			this._page += 1;

			this.RefreshDataRange();
		}

		///
		/// ------------------------------------------------------------------------------------------------------------
		private void tbPageNum_TextChanged( object sender, EventArgs ev )
		{
			int value = 0;
			
			// jeżeli tekst jest pusty, zostaw go w spokoju...
			if( this.tbPageNum.Text == "" )
				return;

			// spróbuj zamienić podany tekst na liczbę
			if( !Int32.TryParse(this.tbPageNum.Text, out value) )
				// jeżeli nie da rady, wpisz aktualną stronę
				this.tbPageNum.Text = this._page.ToString();
			else
			{
				// jeżeli podana wartość jest większa niż liczba wszystkich stron
				if( value >= this._pages )
					this.tbPageNum.Text = this._pages.ToString();
				// jeżeli wartość jest mniejsza niż 1
				else if( value < 1 )
					this.tbPageNum.Text = "1";
			}

			string text = this.tbPageNum.Text;
			int    zcnt = 0;

			// sprawdź czy tekst zawiera zera wiodące...
			for( int x = 0; x < text.Length; ++x )
				if( text[x] != '0' )
					break;
				else
					zcnt++;

			// usuń zera wiodące jeżeli istnieją
			if( zcnt > 0 )
				this.tbPageNum.Text = text.Remove( 0, zcnt );
		}

		///
		/// ------------------------------------------------------------------------------------------------------------
		private void tbPageNum_KeyPress( object sender, KeyPressEventArgs ev )
		{
			// przepuszczaj klawisze kontrolne
			if( char.IsControl(ev.KeyChar) )
				return;

			// przepuszczaj tylko cyfry
			if( ev.KeyChar < '0' || ev.KeyChar > '9' )
				ev.Handled = true;
			else
				this.tbPageNum_TextChanged( sender, null );
		}

		///
		/// ------------------------------------------------------------------------------------------------------------
		private void tbPageNum_KeyDown( object sender, KeyEventArgs ev )
		{
			// po wciśnięciu klawisza ENTER zmień stronę
			if( ev.KeyCode == Keys.Enter )
			{
				int value = 0;

				// sprawdź czy wartośc można zamienić na typ INT
				if( !Int32.TryParse(this.tbPageNum.Text, out value) )
					return;

				// zmień stronę
				if( value != this._page + 1 )
				{
					this.Page = value;
					this.RefreshDataRange();
				}
			}
		}

		///
		/// ------------------------------------------------------------------------------------------------------------
		private void tbRowsPerPage_KeyPress( object sender, KeyPressEventArgs ev )
		{
			// przepuszczaj klawisze kontrolne
			if( char.IsControl(ev.KeyChar) )
				return;

			// przepuszczaj tylko cyfry
			if( ev.KeyChar < '0' || ev.KeyChar > '9' )
				ev.Handled = true;
			else
				this.tbRowsPerPage_TextChanged( sender, null );
		}

		///
		/// ------------------------------------------------------------------------------------------------------------
		private void tbRowsPerPage_KeyDown( object sender, KeyEventArgs ev )
		{
			// po wciśnięciu klawisza ENTER zmień ilość wierszy na stronę
			if( ev.KeyCode == Keys.Enter )
			{
				int value = 0;

				// sprawdź czy wartośc można zamienić na typ INT
				if( !Int32.TryParse(this.tbRowsPerPage.Text, out value) )
					return;

				// zmień ilość wierszy na stronę
				if( value != this._rows_per_page )
				{
					this.RowsPerPage = value;
					this.RefreshDataRange();
				}
			}
		}

		///
		/// ------------------------------------------------------------------------------------------------------------
		private void tbRowsPerPage_TextChanged( object sender, EventArgs ev )
		{
			int value = 0;
			
			// jeżeli tekst jest pusty, zostaw go w spokoju...
			if( this.tbRowsPerPage.Text == "" )
				return;

			// spróbuj zamienić podany tekst na liczbę
			if( !Int32.TryParse(this.tbRowsPerPage.Text, out value) )
				// jeżeli nie da rady, wpisz aktualną ilość wierszy na stronę
				this.tbRowsPerPage.Text = this._rows_per_page.ToString();
			// jeżeli podana wartość jest mniejsza od 5 i jest różna od 0
			else if( value < 0 )
				this.tbRowsPerPage.Text = "1";

			string text = this.tbRowsPerPage.Text;
			int    zcnt = 0;

			// sprawdź czy tekst zawiera zera wiodące...
			for( int x = 0; x < text.Length - 1; ++x )
				if( text[x] != '0' )
					break;
				else
					zcnt++;

			// usuń zera wiodące jeżeli istnieją
			if( zcnt > 0 )
				this.tbRowsPerPage.Text = text.Remove( 0, zcnt );
		}

		///
		/// ------------------------------------------------------------------------------------------------------------
		private void bInsertRow_Click( object sender, EventArgs ev )
		{
			// przejdź do ostatniej strony jeżeli ta nie jest ostatnią
			if( this._page != this._pages - 1 )
			{
				this.Page = this._pages;
				this.RefreshDataRange();
			}

			// pobierz komórkę w pierwszej kolumnie i ostatnim wierszu
			int              last = this.gvData.Rows.Count - 1;
			DataGridViewCell cell = null;

			// sprawdź czy komórka jest widoczna
			for( int x = 1; x <= this._reader.ColumnsNumber; ++x )
				if( this.gvData.Rows[last].Cells[x].Visible == true )
				{
					cell = this.gvData.Rows[last].Cells[x];
					break;
				}

			// brak widocznych komórek?
			if( cell == null )
				return;

			// ustaw komórkę do edycji
			this.gvData.CurrentCell = cell;
			this.gvData.BeginEdit( true );
		}

		///
		/// ------------------------------------------------------------------------------------------------------------
		private void bRemoveRow_Click( object sender, EventArgs ev )
		{
			// pobierz indeksy usuwanych wierszy
			for( int x = 0; x < this.gvData.SelectedRows.Count; ++x )
			{
				// pomiń nowy wiersz
				if( this.gvData.AllowUserToAddRows && this.gvData.SelectedRows[x].Index == this.gvData.Rows.Count - 1 )
					continue;

				this._deleting_rows.Add( this.gvData.SelectedRows[x].Index );
			}

			// uporządkuj malejąco (usuwanie od największego)
			this._deleting_rows.OrderByDescending( w => w );

			// usuń z tablicy i z listy
			for( int x = 0; x < this._deleting_rows.Count; ++x )
			{
				this.gvData.Rows.RemoveAt( this._deleting_rows[x] );
				this._data.RemoveAt( this._deleting_rows[x] );
			}

			// wyczyść usuwane wiersze
			this._deleting_rows.Clear();

			// odśwież widok
			this.RefreshDataRange();
		}

		///
		/// ------------------------------------------------------------------------------------------------------------
		private void lvColumns_ItemChecked( object sender, ItemCheckedEventArgs ev )
		{
			if( this._locked )
				return;

			this.gvData.Columns[ev.Item.Index + 1].Visible = ev.Item.Checked;
		}

		///
		/// ------------------------------------------------------------------------------------------------------------
		private void gvData_SelectionChanged( object sender, EventArgs ev )
		{
			// wyzeruj wszystkie kolumny
			for( int x = 0; x < this._cols_selected.Count; ++x )
				this._cols_selected[x] = false;

			// sprawdź które można zaznaczyć
			foreach( DataGridViewCell cell in this.gvData.SelectedCells )
			{
				if( cell.ColumnIndex == 0 )
					continue;
				this._cols_selected[cell.ColumnIndex-1] = true;
			}

			// zablokuj
			this._locked = true;

			// wyczyść zaznaczenia
			this.lvColumns.SelectedIndices.Clear();

			// zaznacz
			for( int x = 0; x < this._cols_selected.Count; ++x )
				if( this._cols_selected[x] )
					this.lvColumns.SelectedIndices.Add( x );

			// odblokuj
			this._locked = false;

			// sprawdź czy to nie jest czasem nowy wiersz...
			if( this.gvData.SelectedRows.Count == 1 && this.gvData.AllowUserToAddRows &&
				this.gvData.SelectedRows[0].Index == this.gvData.Rows.Count - 1 )
			{
				this.bRemoveRow.Enabled = false;
				return;
			}

			// aktywuj lub deaktywuj przycisk
			if( this.gvData.SelectedRows.Count > 0 )
				this.bRemoveRow.Enabled = true;
			else
				this.bRemoveRow.Enabled = false;
		}

		///
		/// ------------------------------------------------------------------------------------------------------------
		private void tlStatusBar_Paint( object sender, PaintEventArgs ev )
		{
			ev.Graphics.DrawLine
			(
				new Pen( SystemColors.ControlDark ),
				this.tlStatusBar.Bounds.X,
				0,
				this.tlStatusBar.Bounds.Right,
				0
			);
		}

		///
		/// ------------------------------------------------------------------------------------------------------------
		private void lvColumns_SelectedIndexChanged( object sender, EventArgs ev )
		{
			// brak zaznaczenia...
			if( this.lvColumns.SelectedItems.Count == 0 )
				return;

			// pobierz ostatni element
			ListViewItem item = this.lvColumns.SelectedItems[this.lvColumns.SelectedItems.Count - 1];

			// zablokuj
			this._locked = true;

			// zaznacz element w polu wyboru
			switch( this._reader.Types[item.Index] )
			{
				case DATATYPE.String:    this.cbColumnType.SelectedIndex = 0; break;
				case DATATYPE.Integer:   this.cbColumnType.SelectedIndex = 1; break;
				case DATATYPE.Float:     this.cbColumnType.SelectedIndex = 2; break;
				case DATATYPE.Character: this.cbColumnType.SelectedIndex = 3; break;
			}

			// odblokuj
			this._locked = false;
		}

		///
		/// ------------------------------------------------------------------------------------------------------------
		private void gbSearchAndReplace_SizeChanged( object sender, EventArgs ev )
		{
			Size size;
			bool goup = false;

			// sprawdź rozmiar pierwszego przycisku
			size = TextRenderer.MeasureText( this.bReplaceAll.Text, this.bReplaceAll.Font );
			goup = size.Width > this.bReplaceAll.Width - 8 ? true : false;

			// sprawdź rozmiar drugiego przycisku
			if( !goup )
			{
				size = TextRenderer.MeasureText( this.bCount.Text, this.bCount.Font );
				goup = size.Width > this.bCount.Width - 8 ? true : false;
			}

			// powiększ lub pomniejsz przyciski
			if( goup )
			{
				this.bReplaceAll.Height = 40;
				this.bCount.Height      = 40;
			}
			else
			{
				this.bReplaceAll.Height = 24;
				this.bCount.Height      = 24;
			}

			// automatycznie dostosuj rozmiar kolumny
			this.lvcColumnName.Width = -2;
		}

		private void cbColumnType_SelectedIndexChanged( object sender, EventArgs ev )
		{
		}
	}
}
