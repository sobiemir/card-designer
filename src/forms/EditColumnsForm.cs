using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;

///
/// $i[xx] EditColumnsForm.cs
/// 
/// Okno edycji bazy danych.
/// Uruchamia okno filtrowania danych.
/// Pozwala na uruchomienie filtrowania danych i łączenie komórek.
/// 
/// Autor: Kamil Biały
/// Od wersji: 0.8.x.x
/// Ostatnia zmiana: 2015-12-06
/// 
/// #TODO    : Możliwość uzupełniania danych dla nowej kolumnny
/// #TODO    : Możliwość dodawania wierszy (ogólnie edycja bazy danych)
/// #SETTINGS: 2
///

namespace CDesigner
{
	/// 
	/// <summary>
	/// Formularz edycji bazy danych.
	/// Pozwala na łączenie wierszy i włączenie filtrowania danych w komórkach.
	/// </summary>
	/// 
	public partial class EditColumnsForm : Form
	{
		// ===== PRIVATE VARIABLES =====

		/// <summary>Blokada kontrolek (lub innych elementów) przed odświeżeniem.</summary>
		private bool _locked = false;

		private ListViewItem _new_sel_col = null;
		private DatabaseReader _reader = null;
		private FilterCreator _filter_creator = null;
		private bool _show_tooltip = false;
		private DataFilterForm _filter = null;
		private List<string> _test_content = null;
		private EditDataForm _edit_data = null;

		// ===== PUBLIC FUNCTIONS =====

		///
		/// ------------------------------------------------------------------------------------------------------------
		public EditColumnsForm( DatabaseReader reader )
		{
			List<string> lines = null;

			this.InitializeComponent();
			
			// ustaw ikonę
			this.Icon = Program.GetIcon();

			// nagłówki
			lines = Language.GetLines( "EditColumns", "Headers" );
			this.lvcColumnName.Text    = lines[0];
			this.lvcJoinedColumns.Text = lines[1];
			this.lvcDataPreview.Text   = lines[2];
			this.lvcColumns.Text       = lines[3];

			// przyciski
			lines = Language.GetLines( "EditColumns", "Buttons" );
			this.bAddColumn.Text    = lines[0];
			this.bClearColumn.Text  = lines[1];
			this.bDeleteColumn.Text = lines[2];
			this.bFilterData.Text   = lines[3];
			this.bSave.Text         = lines[4];

			this._reader = reader;
			this._filter_creator = new FilterCreator( reader );
			
			// uzupełnij liste kolumn
			foreach( string column in this._reader.Columns )
			{
				ListViewItem item = new ListViewItem( column );
				item.Checked = true;

				this.lvDatabaseColumns.Items.Add( item );
			}

			// filtrowanie danych
			this._filter = new DataFilterForm( this._filter_creator );

			// ilość miejsc dla formatowanych danych (wyświetlane dane)
			// @settings
			this._test_content = new List<string>( 7 );

			for( int x = 0; x < 7; ++x )
				this._test_content.Add( "" );

			// nazwa formularza
			this.Text = Language.GetLine( "FormNames", (int)FORMLIDX.EditColumns );

			// formularz edycji danych
			this._edit_data = new EditDataForm();
		}

		///
		/// ------------------------------------------------------------------------------------------------------------
		private void bAddColumn_Click( object sender, EventArgs ev )
		{
			// usuń białe znaki z przodu i z tyłu
			string text  = this.tbColumnName.Text.Trim();

			// brak nazwy kolumny
			if( text == "" )
			{
				Program.LogInfo
				(
					Language.GetLine( "EditColumns", "Messages", 1 ),
					Language.GetLine( "MessageNames", 1 )
				);
				return;
			}

			// sprawdź czy kolumna o tej nazwie została już dodana
			foreach( ListViewItem item in this.lvNewColumns.Items )
				if( item.SubItems[0].Text == text )
				{
					Program.LogInfo
					(
						String.Format( Language.GetLine("EditColumns", "Messages", 2), item.Text ),
						Language.GetLine( "MessageNames", 1 )
					);
					return;
				}

			// sprawdź czy kolumna już istnieje, jeżeli tak, wyświelt ostrzeżenie o jej nadpisaniu
			foreach( string column in this._reader.Columns )
				if( column == text )
				{
					DialogResult result = MessageBox.Show
					(
						this,
						String.Format( Language.GetLine("EditColumns", "Messages", 3), column ),
						Language.GetLine( "MessageNames", 1 ),
						MessageBoxButtons.YesNo,
						MessageBoxIcon.Warning,
						MessageBoxDefaultButton.Button2
					);

					// anulowanie nadpisania kolumny
					if( result != DialogResult.Yes )
					{
#					if DEBUG
						Program.LogMessage( "Nadpisywanie kolumny zostało anulowane." );
#					endif
						return;
					}
#				if DEBUG
					Program.LogMessage( "Kolumna '" + column + "' została nadpisana." );
#				endif
				}

			List<int> list = new List<int>();

			// dodaj kolumnę
			this.lvNewColumns.Items.Add( text );
			this.lvNewColumns.Items[this.lvNewColumns.Items.Count-1].SubItems.Add("").Tag = list;

			// dodaj kolumnę do listy w filtrach
			//this._filter.AddColumn( text );

			// dodaj kolumnę do listy
			//this._data_filter.AddColumn( text );

#		if DEBUG
			Program.LogMessage( "Dodano nową kolumnę: " + text + "." );
#		endif
		}

		///
		/// ------------------------------------------------------------------------------------------------------------
		private void bDeleteColumn_Click( object sender, EventArgs ev )
		{
			if( this.lvNewColumns.SelectedItems.Count == 0 )
				return;

			ListViewItem item = this.lvNewColumns.SelectedItems[0];

			// usuń kolumnę z filtrów
			//this._filter.RemoveColumn( item.Index );

			this.lvNewColumns.Items.Remove( item );

			//this._data_filter.RemoveColumn( item.Index + this.lvDatabaseColumns.Items.Count );

#			if DEBUG
				Program.LogMessage( "Usunięto kolumnę o nazwie: " + item.Text + "." );
#			endif

			// pozbieraj śmieci
			GC.Collect();
		}

		///
		/// ------------------------------------------------------------------------------------------------------------
		private void bClearColumn_Click( object sender, EventArgs ev )
		{
			if( this.lvNewColumns.SelectedItems.Count == 0 )
				return;

			ListViewItem item = this.lvNewColumns.SelectedItems[0];

			// wyczyść elementy potomne z podanego elementu
			//this._filter.ClearSubColumns( item.Index );

			((List<int>)item.SubItems[1].Tag).Clear();
			item.SubItems[1].Text = "";

			//this._data_filter.ClearColumn( this.lvDatabaseColumns.Items.Count + item.Index );

#			if DEBUG
				Program.LogMessage( "Kolumnę o nazwie " + item.Text + " została wyczyszczona." );
#			endif

			// pozbieraj śmieci
			GC.Collect();
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

			ListViewItem item = this.lvDatabaseColumns.SelectedItems[0];

			// zapisz indeks
			int index    = item.Index,
				rows     = this._filter_creator.RowsNumber,
				position = this._filter_creator.RowsNumber * index;

			this.lvPreviewRows.Items.Clear();

			// uzupełnij tabelę wierszami
			// @settings
			for( int x = 0, y = rows > 7 ? 7 : rows; x < y; ++x, ++position )
				this.lvPreviewRows.Items.Add( this._filter_creator.Rows[position] );

			// zmień nagłowek tabeli
			this.lvcDataPreview.Text = Language.GetLine( "EditColumns", "Headers", 2 ) +
				" [" + this.lvDatabaseColumns.SelectedItems[0].Text + "]";

#		if DEBUG
			Program.LogMessage( "Wczytano wiersze kolumny: " + item.Text + "." );
#		endif
		}

		///
		/// ------------------------------------------------------------------------------------------------------------
		private void lvNewColumns_SelectedIndexChanged( object sender, EventArgs ev )
		{
			// blokada
			if( this._locked )
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

			// ilość wierszy
			int rows = this._filter_creator.RowsNumber;

			this.lvPreviewRows.Items.Clear();

			// @settings
			this._filter_creator.CreateTestContent
			(
				item.Index,
				this._test_content,
				indices,
				this._filter.FilterData,
				7
			);

			// uzupełnij tabelę wierszami
			for( int x = 0; x < this._test_content.Count; ++x )
				this.lvPreviewRows.Items.Add( this._test_content[x] );
			
			// wyświetl nazwę zaznaczonej kolumny
			this.lvcDataPreview.Text = Language.GetLine( "EditColumns", "Headers", 2 ) +
				" [" + this.lvNewColumns.SelectedItems[0].Text + "]";

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
						Language.GetLine( "EditColumns", "Messages", 0 ),
						this.tbColumnName,
						new Point( 0, this.tbColumnName.Height + 2 )
					);
					this._show_tooltip = true;
				}

				// dźwięk systemowy
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
			// pobierz element pod wskaźnikiem myszy
			ListViewItem item = this.lvDatabaseColumns.GetItemAt( ev.X, ev.Y );

			if( item == null )
				return;

			// zapobiega podwójnemu wczytywaniu danych
			this._locked = item.Index == 0 && this.lvPreviewRows.Items.Count == 0 ? false : true;
			item.Selected = true;
			this._locked = false;

			// przenieś-upuść
			this.lvDatabaseColumns.DoDragDrop( item, DragDropEffects.Move );
		}

		///
		/// ------------------------------------------------------------------------------------------------------------
		private void dragDropEffects_Move( object sender, DragEventArgs ev )
		{
			ev.Effect = DragDropEffects.Move;
		}

		///
		/// ------------------------------------------------------------------------------------------------------------
		private void toolTip_Hide( object sender, EventArgs ev )
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
			//DialogResult result = this._filter.ShowDialog( );

			//if( result != DialogResult.OK )
			//	return;

			//this._data_filter.ApplyFilters( this._reader, this._filter.FilterData );
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

			list.Add( copy.Index );

			//this._data_filter.SetColumnContent( column, list );
			
			// dodaj kolumnę potomną do filtrów
			//this._filter.AddSubColumn( item.Index, copy.Text, copy.Index );

			this.lvNewColumns.Focus();
		}

		///
		/// Dodatkowa linia na górze w pasku stanu (z przyciskami i polem tekstowym na dole).
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
		/// Rysowanie dymku dla podpowiedzi.
		/// ------------------------------------------------------------------------------------------------------------
		private void tpTooltip_Draw( object sender, DrawToolTipEventArgs ev )
		{
			ev.DrawBackground();
			ev.DrawBorder();
			ev.DrawText( TextFormatFlags.VerticalCenter );
		}

		private void bEditData_Click( object sender, EventArgs ev )
		{
			this._edit_data.SetDataSource( this._reader );
			this._edit_data.RefreshDataRange();
			
			// if( this._edit_data.ShowDialog() == DialogResult.OK )
			// 	;
		}
	}
}
