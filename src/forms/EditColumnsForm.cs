using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;

/* 
 * TODO:
 * Możliwość uzupełniania danych dla nowej kolumnny
 */

namespace CDesigner
{
	public partial class EditColumnsForm : Form
	{
		private bool _locked = false;
		private ListViewItem _new_sel_col = null;
		private DatabaseReader _reader = null;
		private DataFilter _data_filter = null;
		private bool _show_tooltip = false;
		private DataFilterForm _filter = null;

		///
		/// ------------------------------------------------------------------------------------------------------------
		public EditColumnsForm( DatabaseReader reader )
		{
			this.InitializeComponent( );
			this.Icon = Program.GetIcon( );

			this._reader = reader;
			this._data_filter = new DataFilter( reader );
			
			// uzupełnij liste kolumn
			foreach( string column in this._reader.Columns )
			{
				ListViewItem item = new ListViewItem( column );
				item.Checked = true;

				this.lvDatabaseColumns.Items.Add( item );
			}

			// filtrowanie danych
			this._filter = new DataFilterForm( this._data_filter );
		}

		///
		/// ------------------------------------------------------------------------------------------------------------
		private void JoinColsForm_Move( object sender, EventArgs ev )
		{
			if( this._show_tooltip )
			{
				this.tpTooltip.Hide( this.tbColumnName );
				this._show_tooltip = false;
			}
		}

		///
		/// ------------------------------------------------------------------------------------------------------------
		private void JoinColsForm_Deactivate( object sender, EventArgs ev )
		{
			if( this._show_tooltip )
			{
				this.tpTooltip.Hide( this.tbColumnName );
				this._show_tooltip = false;
			}
		}

		///
		/// ------------------------------------------------------------------------------------------------------------
		private void bAddColumn_Click( object sender, EventArgs ev )
		{
			string text  = this.tbColumnName.Text.Trim();

			// brak nazwy kolumny
			if( text == "" )
			{
				Program.LogInfo( "Aby dodać kolumnę musisz podać jej nazwę.", "Dodawanie kolumny" );
				return;
			}

			// sprawdź czy kolumna o tej nazwie została już dodana
			foreach( ListViewItem item in this.lvNewColumns.Items )
				if( item.SubItems[0].Text == text )
				{
					Program.LogInfo( "Dodano już kolumnę o nazwie '" + item.Text + "'.", "Dodawanie kolumny" );
					return;
				}

			// sprawdź czy kolumna już istnieje, jeżeli tak, wyświelt ostrzeżenie o jej nadpisaniu
			foreach( string column in this._reader.Columns )
				if( column == text )
				{
					DialogResult result = MessageBox.Show
					(
						this,
						"Kolumna o nazwie '" + column + "' już istnieje. Zapisanie zmian spowoduje nadpisanie " +
						"wartości starej kolumny. Dopóki zmiany nie zostaną zapisane, będziesz jeszcze mógł z " +
						"niej korzystać w tym oknie.\n\nCzy na pewno chcesz utworzyć kolumnę o tej nazwie?",
						"Dodawanie kolumny",
						MessageBoxButtons.YesNo,
						MessageBoxIcon.Warning,
						MessageBoxDefaultButton.Button2
					);

					if( result != DialogResult.Yes )
					{
						Program.LogMessage( "Nadpisywanie kolumny zostało anulowane." );
						return;
					}
					Program.LogMessage( "Kolumna '" + column + "' została nadpisana." );
				}

			List<int> list = new List<int>();

			//this._new_cols.Add( list );
			//this._new_names.Add( text );

			// dodaj kolumnę
			this.lvNewColumns.Items.Add( text );
			this.lvNewColumns.Items[this.lvNewColumns.Items.Count-1].SubItems.Add("").Tag = list;

			// dodaj kolumnę do listy w filtrach
			this._filter.AddColumn( text );

			// dodaj kolumnę do listy
			this._data_filter.AddColumn( text );
		}

		///
		/// ------------------------------------------------------------------------------------------------------------
		private void bClearColumn_Click( object sender, EventArgs ev )
		{
			// wyczyść wszystkie zaznaczone wiersze
			foreach( ListViewItem item in this.lvNewColumns.SelectedItems )
			{
				// wyczyść elementy potomne z podanego elementu
				this._filter.ClearSubColumns( item.Index );

				List<int> indices = (List<int>)item.SubItems[1].Tag;
				indices.Clear( );
				item.SubItems[1].Text = "";

				this._data_filter.ClearColumn( this.lvDatabaseColumns.Items.Count + item.Index );
			}
		}

		///
		/// ------------------------------------------------------------------------------------------------------------
		private void bDeleteColumn_Click( object sender, EventArgs ev )
		{
			if( this.lvNewColumns.SelectedItems.Count == 0 )
				return;

			// usuń zaznaczone elementy
			foreach( ListViewItem item in this.lvNewColumns.SelectedItems )
			{
				// usuń kolumnę z filtrów
				this._filter.RemoveColumn( item.Index );

				this.lvNewColumns.Items.Remove( item );

				this._data_filter.RemoveColumn( item.Index + this.lvDatabaseColumns.Items.Count );
			}

			// pozbieraj śmieci
			GC.Collect( );
		}

		///
		/// ------------------------------------------------------------------------------------------------------------
		private void lvDatabaseColumns_SelectedIndexChanged( object sender, EventArgs ev )
		{
			// blokada
			if( this._locked )
				return;

			// brak zaznaczonych kolumn
			if( this.lvDatabaseColumns.SelectedItems.Count == 0 )
				return;

			// zapisz indeks
			int index    = this.lvDatabaseColumns.SelectedItems[0].Index,
				rows     = this._data_filter.RowsNumber,
				position = this._data_filter.RowsNumber * index;

			this.lvPreviewRows.Items.Clear( );

			for( int x = 0, y = rows > 7 ? 7 : rows; x < y; ++x, ++position )
				this.lvPreviewRows.Items.Add( this._data_filter.Rows[position] );

			this.lvcDataPreview.Text = "Podgląd danych [" + this.lvDatabaseColumns.SelectedItems[0].Text + "]";
		}

		///
		/// ------------------------------------------------------------------------------------------------------------
		private void lvNewColumns_SelectedIndexChanged( object sender, EventArgs ev )
		{
			// blokada
			if( this._locked )
				return;

			// zaznaczono więcej niż jeden element, nie przetwarzaj dalej...
			if( this.lvNewColumns.SelectedItems.Count > 1 )
				return;

			// brak zaznaczonych kolumn
			if( this.lvNewColumns.SelectedItems.Count == 0 )
			{
				this._new_sel_col = null;

				this.bClearColumn.Enabled  = false;
				this.bDeleteColumn.Enabled = false;

				return;
			}

			// zapisz nową zaznaczoną kolumnę
			ListViewItem item = this.lvNewColumns.SelectedItems[0];
			this._new_sel_col = item;
			List<int> indices = (List<int>)item.SubItems[1].Tag;

			// zapisz indeks
			int index    = this.lvDatabaseColumns.Items.Count + item.Index,
				rows     = this._data_filter.RowsNumber,
				position = this._data_filter.RowsNumber * index;

			this.lvPreviewRows.Items.Clear( );
			
			for( int x = 0, y = rows > 7 ? 7 : rows; x < y; ++x, ++position )
				this.lvPreviewRows.Items.Add( this._data_filter.Rows[position] );
			
			// wyświetl nazwę zaznaczonej kolumny
			this.lvcDataPreview.Text = "Podgląd danych [ " + this.lvNewColumns.SelectedItems[0].Text + " ]";

			// odblokuj przyciski
			this.bClearColumn.Enabled  = true;
			this.bDeleteColumn.Enabled = true;
		}

		///
		/// ------------------------------------------------------------------------------------------------------------
		private void tbColumnName_KeyPress( object sender, KeyPressEventArgs ev )
		{
			// backspace i ctrl
			if( ev.KeyChar == 8 || ModifierKeys == Keys.Control )
				return;

			// sprawdź czy nazwa zawiera niedozwolone znaki
			Regex regex = new Regex( @"^[0-9a-zA-Z" + Program.AvaliableChars + @" .\-+_]+$" );
			if( !regex.IsMatch(ev.KeyChar.ToString()) )
			{
				// pokaż informacje o dozwolonych znakach
				if( !this._show_tooltip )
				{
					this.tpTooltip.Show
					(
						" Dopuszczalne znaki:\n" +
						" Znaki alfabetu, cyfry, . - + _ oraz spacja.",
						this.tbColumnName,
						new Point( 0, this.tbColumnName.Height + 2 )
					);
					this._show_tooltip = true;
				}

				ev.Handled = true;
				System.Media.SystemSounds.Beep.Play();
				
				return;
			}

			// schowaj informacje o dozwolonych znakach
			if( this._show_tooltip )
			{
				this.tpTooltip.Hide( this.tbColumnName );
				this._show_tooltip = false;
			}
		}

		///
		/// ------------------------------------------------------------------------------------------------------------
		private void lvDatabaseColumns_MouseDown( object sender, MouseEventArgs ev )
		{
			ListViewItem item = this.lvDatabaseColumns.GetItemAt( ev.X, ev.Y );

			if( item == null )
				return;

			this._locked = item.Index == 0 && this.lvPreviewRows.Items.Count == 0 ? false : true;
			item.Selected = true;
			this._locked = false;

			this.lvDatabaseColumns.DoDragDrop( item, DragDropEffects.Move );
		}

		///
		/// ------------------------------------------------------------------------------------------------------------
		private void tbColumnName_Leave( object sender, EventArgs ev )
		{
			if( this._show_tooltip )
			{
				this.tpTooltip.Hide( this.tbColumnName );
				this._show_tooltip = false;
			}
		}

		///
		/// ------------------------------------------------------------------------------------------------------------
		private void bFilterData_Click( object sender, EventArgs ev )
		{
			DialogResult result = this._filter.ShowDialog( );

			if( result != DialogResult.OK )
				return;

			this._data_filter.ApplyFilters( this._reader, this._filter.FilterData );
		}

		///
		/// ------------------------------------------------------------------------------------------------------------
		private void lvDatabaseColumns_DragOver( object sender, DragEventArgs ev )
		{
			ev.Effect = DragDropEffects.Move;
		}

		///
		/// ------------------------------------------------------------------------------------------------------------
		private void lvNewColumns_DragEnter( object sender, DragEventArgs ev )
		{
			ev.Effect = DragDropEffects.Move;
		}

		///
		/// ------------------------------------------------------------------------------------------------------------
		private void lvNewColumns_DragOver( object sender, DragEventArgs ev )
		{
			// zaznaczona kolumna
			Point        point = this.lvNewColumns.PointToClient( new Point(ev.X, ev.Y) );
			ListViewItem item  = this.lvNewColumns.GetItemAt( point.X, point.Y );

			if( this._new_sel_col == item )
				return;
			if( item == null )
				return;

			// zablokuj
			this._locked = true;

			// odznacz starą kolumnę
			if( this._new_sel_col != null )
				this._new_sel_col.Selected = false;

			// zaznacz nową kolumnę
			if( item != null )
				item.Selected = true;
			this._new_sel_col = item;

			this._locked = false;
		}

		///
		/// ------------------------------------------------------------------------------------------------------------
		private void lvNewColumns_DragDrop( object sender, DragEventArgs ev )
		{
			// zaznaczona kolumna
			Point        point = this.lvNewColumns.PointToClient( new Point(ev.X, ev.Y) );
			ListViewItem item  = this.lvNewColumns.GetItemAt( point.X, point.Y );

			if( item == null )
				return;

			// kopiowana kolumna
			ListViewItem copy = (ListViewItem)ev.Data.GetData( typeof(ListViewItem) );
			List<int>    list = (List<int>)item.SubItems[1].Tag;

			// dodaj kolumnę do łączenia
			if( item.SubItems[1].Text.Length == 0 )
				item.SubItems[1].Text = copy.Text;
			else
				item.SubItems[1].Text += ", " + copy.Text;
			
			int column = this.lvDatabaseColumns.Items.Count + item.Index;

			list.Add( copy.Index );

			this._data_filter.SetColumnContent( column, list );
			
			// dodaj kolumnę potomną do filtrów
			this._filter.AddSubColumn( item.Index, copy.Text );
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
		private void tpTooltip_Draw( object sender, DrawToolTipEventArgs ev )
		{
			ev.DrawBackground();
			ev.DrawBorder();
			ev.DrawText( TextFormatFlags.VerticalCenter );
		}
	}
}
