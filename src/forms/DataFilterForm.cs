using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using System.Collections;
using System.IO;

/* 
 * TODO:
 * [1] Naprawić opóźnione tworzenie kontrolek...
 * [2] Przy dodawaniu nowej kontrolki checkbox dziedziczy stan z głównego (cbSelectAll).
 * [2] Po usunięciu elementów z nowych kolumn stara kolumna ma zmieniać nazwę na Kolumny, a nowa ma być usuwana.
 * [3] Po usunięciu wszystkich, główny checkbox ma zmienić wartość oraz reagować na nowo dodany rekord.
 * [3] Po usunięciu elementu/ów przycisk usuń staje się nieaktywny...
 */

namespace CDesigner
{
	public partial class DataFilterForm : Form
	{
		private GroupComboBoxSync _sync = null;
		private GroupComboBoxItem _newcol = null;
		private GroupComboBoxItem _oldcol = null;
		private List<FilterData> _filters = null;
		private bool _locked = false;

		///
		/// ------------------------------------------------------------------------------------------------------------
		public List<FilterData> FilterData
		{
			get { return this._filters; }
		}

		///
		/// ------------------------------------------------------------------------------------------------------------
		public DataFilterForm( DataFilter filter )
		{
			this.InitializeComponent( );

			this._sync = new GroupComboBoxSync( );
			
			this._oldcol = this._sync.Add( "Kolumny", true );
			this._newcol = null;

			// dodaj kolumny do listy
			foreach( string column in filter.Columns )
				this._sync.Add( column, false, this._oldcol );

			this._filters = new List<FilterData>( );
		}

		///
		/// ------------------------------------------------------------------------------------------------------------
		public int GetRealIndex( int index, bool old = false )
		{
			if( old )
			{
				if( index >= this._oldcol.child.Count )
					return -1;

				index = this._sync.FindItemIndex( this._oldcol.child[index] );
			}
			else
			{
				if( this._newcol == null )
					return -1;

				if( index >= this._newcol.child.Count )
					return -1;

				index = this._sync.FindItemIndex( this._newcol.child[index] );
			}

			return index;
		}

		///
		/// ------------------------------------------------------------------------------------------------------------
		public GroupComboBoxItem GetItemAt( int index )
		{
			if( this._sync.Count <= index )
				return null;

			return this._sync[index];
		}

		///
		/// ------------------------------------------------------------------------------------------------------------
		private void bAddFilter_Click( object sender, EventArgs ev )
		{
			int row = this.tlFilters.RowCount - 1;

			this.tlFilters.RowCount++;

			// dodaj nowy styl
			if( this.tlFilters.RowCount == 2 )
				this.tlFilters.RowStyles[0] = new RowStyle( SizeType.Absolute, 28 );
			this.tlFilters.RowStyles.Add( new RowStyle(SizeType.Absolute, 28) );

			// zaznaczenie do usunięcia
			CheckBox check = new CheckBox( );
			check.Dock = DockStyle.Fill;
			check.CheckAlign = System.Drawing.ContentAlignment.MiddleCenter;

			// wybór kolumny
			GroupComboBox column = new GroupComboBox( );
			column.Dock = DockStyle.Fill;

			// dodaj kontrolkę do synchronizacji
			this._sync.AddCombo( column );
			column.CalculateDropDownWidth( );

			// filtr
			ComboBox filter = new ComboBox( );
			filter.DropDownStyle = ComboBoxStyle.DropDownList;
			filter.Dock = DockStyle.Fill;
			filter.Enabled = false;

			// modyfikator
			TextBox modifier = new TextBox( );
			modifier.Dock = DockStyle.Fill;
			modifier.AutoCompleteMode = AutoCompleteMode.None;
			modifier.Enabled = false;
			modifier.Margin = new Padding( 3, 4, 3, 3 );

			// rezultat
			TextBox result = new TextBox( );
			result.Dock = DockStyle.Fill;
			result.AutoCompleteMode = AutoCompleteMode.None;
			result.Enabled = false;
			result.Margin = new Padding( 3, 4, 3, 3 );

			// wyłączenie z widoku
			CheckBox exclude = new CheckBox( );
			exclude.Dock = DockStyle.Fill;
			exclude.CheckAlign = System.Drawing.ContentAlignment.MiddleCenter;
			exclude.Enabled = false;

			// pozostawienie na widoku
			CheckBox leave = new CheckBox( );
			leave.Dock = DockStyle.Fill;
			leave.CheckAlign = System.Drawing.ContentAlignment.MiddleCenter;
			leave.Enabled = false;

			// zdarzenia
			check.CheckedChanged += new EventHandler( this.cbCheck_CheckedChanged );
			column.SelectedIndexChanged += new EventHandler( this.cbColumn_SelectedIndexChanged );
			filter.SelectedIndexChanged += new EventHandler( this.cbFilter_SelectedIndexChanged );
			modifier.KeyUp += new KeyEventHandler( tbModifier_KeyUp );
			result.KeyUp += new KeyEventHandler( tbModifier_KeyUp );
			exclude.CheckedChanged += new EventHandler( this.cbInclExcl_CheckedChanged );
			leave.CheckedChanged += new EventHandler( this.cbInclExcl_CheckedChanged );

			// uchwyty do kontrolek
			object[] tag = new object[8] { row, check, column, filter, modifier, result, exclude, leave };
			check.Tag = column.Tag = filter.Tag = modifier.Tag = result.Tag = exclude.Tag = leave.Tag = tag;
			
			// dodaj uchwyty do każdej dodanej kontrolki
			this.tlFilters.Controls.Add( check, 0, row );
			this.tlFilters.Controls.Add( column, 1, row );
			this.tlFilters.Controls.Add( filter, 2, row );
			this.tlFilters.Controls.Add( modifier, 3, row );
			this.tlFilters.Controls.Add( result, 4, row );
			this.tlFilters.Controls.Add( exclude, 5, row );
			this.tlFilters.Controls.Add( leave, 6, row );

			// dodaj nowy filtr
			FilterData data = new FilterData( );

			data.column   = -1;
			data.exclude  = false;
			data.filter   = -1;
			data.leave    = false;
			data.modifier = "";
			data.result   = "";
			
			this._filters.Add( data );
		}

		///
		/// ------------------------------------------------------------------------------------------------------------
		public void AddColumn( string name )
		{
			// zmień nazwę Kolumny na Stare kolumny i dodaj element Nowe kolumny
			if( this._newcol == null )
			{
				this._oldcol.first = false;
				this._oldcol.last  = true;
				this._oldcol.text  = "Stare kolumny";

				this._newcol = this._sync.Insert( 0, "Nowe kolumny", true );
				
				this._newcol.first = true;
				this._newcol.last  = false;
			}

			// dodaj kolumnę do listy
			this._sync.Add( name, false, this._newcol );

			// przelicz szerokość listy rozwijanej
			this._sync.CalculateDropDownWidth( );
		}

		///
		/// ------------------------------------------------------------------------------------------------------------
		public void AddSubColumn( int index, string name )
		{
			int repeat = 0;

			for( int x = 0; x < this._sync.Count; ++x )
			{
				if( this._sync[x].parent == this._newcol )
				{
					if( repeat == index )
					{
						this._sync.Add( name, false, this._sync[x] );
						break;
					}
					repeat++;
				}
			}
			// przelicz szerokość listy rozwijanej
			this._sync.CalculateDropDownWidth( );
		}

		///
		/// ------------------------------------------------------------------------------------------------------------
		public void ClearSubColumns( int index )
		{
			if( index >= this._newcol.child.Count )
				return;

			this._sync.RemoveChildrens( this._newcol.child[index] );

			GC.Collect( );
		}

		///
		/// ------------------------------------------------------------------------------------------------------------
		public void RemoveColumn( int index )
		{
			if( index >= this._newcol.child.Count )
				return;

			this._sync.RemoveChildrens( this._newcol.child[index] );
			this._sync.Remove( this._newcol.child[index] );

			GC.Collect( );
		}

		///
		/// ------------------------------------------------------------------------------------------------------------
		private void cbCheck_CheckedChanged( object sender, EventArgs ev )
		{
			if( this._locked )
				return;

			int counter = 0;

			// sprawdź które rekordy są zaznaczone
			for( int x = 0; x < this.tlFilters.RowCount-1; ++x )
				if( ((CheckBox)this.tlFilters.GetControlFromPosition(0, x)).CheckState == CheckState.Checked )
					counter++;

			this._locked = true;
			
			// dobierz odpowiedni stan kontrolki na samej górze (która zaznacza wszystko)
			if( counter == 0 )
				this.cbSelectAll.CheckState = CheckState.Unchecked;
			else if( counter == this.tlFilters.RowCount-1 )
				this.cbSelectAll.CheckState = CheckState.Checked;
			else
				this.cbSelectAll.CheckState = CheckState.Indeterminate;

			this._locked = false;
		}

		///
		/// ------------------------------------------------------------------------------------------------------------
		void cbColumn_SelectedIndexChanged( object sender, EventArgs ev )
		{
			GroupComboBox     combo = (GroupComboBox)sender;
			GroupComboBoxItem item  = combo.Items[combo.SelectedIndex];

			object[] tag  = (object[])combo.Tag;
			ComboBox cbox = (ComboBox)tag[3];

			if( item.parent == null && item.child.Count > 0 )
			{
				if( cbox.Items.Count == 1 )
					return;

				cbox.Items.Clear( );
				cbox.Items.Add( "Format" );
				cbox.SelectedIndex = 0;
			}
			else
			{
				if( cbox.Items.Count == 6 )
					return;

				cbox.Items.Clear( );
				cbox.Items.Add( "Duże litery" );
				cbox.Items.Add( "Małe litery" );
				cbox.Items.Add( "Nazwa własna" );
				cbox.Items.Add( "Różny [<>]" );
				cbox.Items.Add( "Równy [==]" );
				cbox.Items.Add( "Format" );
				cbox.SelectedIndex = 0;
			}

			cbox.Enabled = true;

			int row = (int)tag[0];

			this._filters[row].column = combo.SelectedIndex;
			this._filters[row].filter = cbox.Items.Count == 1 ? 5 : 0;
		}

		///
		/// ------------------------------------------------------------------------------------------------------------
		void cbFilter_SelectedIndexChanged( object sender, EventArgs ev )
		{
			ComboBox cbox = (ComboBox)sender;
			object[] tag = (object[])cbox.Tag;

			for( int x = 4; x < 8; ++x )
				((Control)tag[x]).Enabled = false;

			if( cbox.Items.Count == 1 )
				((Control)tag[5]).Enabled = true;
			else
			{
				switch( cbox.SelectedIndex )
				{
				case 0: case 1: case 2:
				break;
				case 3: case 4:
					for( int x = 4; x < 8; ++x )
						((Control)tag[x]).Enabled = true;
				break;
				case 5:
					((TextBox)tag[5]).Enabled = true;
					((TextBox)tag[5]).Text = "##";
				break;
				}
			}

			this._filters[(int)tag[0]].filter = cbox.Items.Count == 1 ? 5 : cbox.SelectedIndex;
		}

		///
		/// ------------------------------------------------------------------------------------------------------------
		void tbModifier_KeyUp( object sender, KeyEventArgs ev )
		{
			TextBox  tbox = (TextBox)sender;
			object[] tag  = (object[])tbox.Tag;

			if( tbox == tag[4] )
				this._filters[(int)tag[0]].modifier = tbox.Text;
			else
				this._filters[(int)tag[0]].result = tbox.Text;
		}

		///
		/// ------------------------------------------------------------------------------------------------------------
		void cbInclExcl_CheckedChanged( object sender, EventArgs ev )
		{
			if( this._locked )
				return;

			CheckBox chkbx = (CheckBox)sender;
			object[] tag = (object[])chkbx.Tag;

			this._locked = true;

			// zablokuj pole wynikowe
			if( chkbx.Checked )
				((Control)tag[5]).Enabled = false;
			else
				((Control)tag[5]).Enabled = true;

			// odznacz drugą kontrolkę
			if( chkbx == tag[6] )
				((CheckBox)tag[7]).Checked = false;
			else
				((CheckBox)tag[6]).Checked = false;

			this._filters[(int)tag[0]].leave   = ((CheckBox)tag[6]).Checked;
			this._filters[(int)tag[0]].exclude = ((CheckBox)tag[7]).Checked;

			this._locked = false;
		}

		///
		/// ------------------------------------------------------------------------------------------------------------
		private void cbSelectAll_CheckedChanged( object sender, EventArgs ev )
		{
			CheckState state = this.cbSelectAll.CheckState;

			if( state == CheckState.Checked || state == CheckState.Indeterminate )
				this.bDelete.Enabled = true;
			else
				this.bDelete.Enabled = false;

			if( this._locked )
				return;

			// zamień status pozostałych kontrolek (zaznaczone lub nie)
			this._locked = true;
			for( int x = 0; x < this.tlFilters.RowCount-1; ++x )
				((CheckBox)this.tlFilters.GetControlFromPosition(0, x)).CheckState = state;
			this._locked = false;
		}

		///
		/// ------------------------------------------------------------------------------------------------------------
		private void bAccept_Click( object sender, EventArgs ev )
		{
			this.DialogResult = DialogResult.OK;
			this.Close( );
		}

		///
		/// ------------------------------------------------------------------------------------------------------------
		private void tlFilterList_Paint( object sender, PaintEventArgs ev )
		{
			ev.Graphics.DrawLine
			(
				new Pen( VisualStyleInformation.TextControlBorder ),
				this.tlFilterList.Bounds.X,
				this.tlFilterList.Bounds.Top + this.tlFilterList.RowStyles[0].Height - 1,
				this.tlFilterList.Bounds.Right,
				this.tlFilterList.Bounds.Top + this.tlFilterList.RowStyles[0].Height - 1
			);
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
		private void bDelete_Click( object sender, EventArgs ev )
		{
			CheckBox chbk = null;
			object[] tag  = null;

			for( int x = 0; x < this.tlFilters.RowCount - 1; ++x )
			{
				chbk = (CheckBox)this.tlFilters.GetControlFromPosition( 0, x );
				tag  = (object[])chbk.Tag;

				// sprawdź czy rekord jest zaznaczony
				if( chbk.CheckState != CheckState.Checked )
					continue;

				for( int y = 1; y < 8; ++y )
					this.tlFilters.Controls.Remove( (Control)tag[y] );

				for( int y = x + 1; y < this.tlFilters.RowCount - 1; ++y )
					for( int z = 0; z < 7; ++z )
						this.tlFilters.SetRow( this.tlFilters.GetControlFromPosition(z, y), y - 1 );
				
				// usuń z listy filtrów
				this._filters.RemoveAt( x );

				x--;
				this.tlFilters.RowCount -= 1;
			}

			GC.Collect( );
		}

		///
		/// ------------------------------------------------------------------------------------------------------------
		private void bClear_Click( object sender, EventArgs ev )
		{
			CheckBox chbk = null;
			object[] tag  = null;

			for( int x = 0; x < this.tlFilters.RowCount - 1; ++x )
			{
				chbk = (CheckBox)this.tlFilters.GetControlFromPosition( 0, x );
				tag  = (object[])chbk.Tag;

				for( int y = 1; y < 8; ++y )
					this.tlFilters.Controls.Remove( (Control)tag[y] );

				for( int y = x + 1; y < this.tlFilters.RowCount - 1; ++y )
					for( int z = 0; z < 7; ++z )
						this.tlFilters.SetRow( this.tlFilters.GetControlFromPosition(z, y), y - 1 );
				
				// usuń z listy filtrów
				this._filters.RemoveAt( x );

				x--;
				this.tlFilters.RowCount -= 1;
			}

			GC.Collect( );
		}
	}
}
