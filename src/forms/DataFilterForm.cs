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
 * Naprawić opóźnione tworzenie kontrolek...
 * Przy dodawaniu nowej kontrolki checkbox dziedziczy stan z głównego (cbSelectAll)
 */

namespace CDesigner
{
	public partial class DataFilterForm : Form
	{
		private CustomContextMenuStrip _dropdown = null;
		private TreeView _col1 = null;
		private TreeView _col2 = null;
		private bool _locked = false;

		public DataFilterForm( DatabaseReader reader )
		{
			this.InitializeComponent();
			this._dropdown = TreeComboBox.InitTreeMenuStrip( ref this._col1, ref this._col2 );

			foreach( string column in reader.Columns )
				this._col1.Nodes.Add( column );
		}

		public void AddColumn( string name )
		{
			this._col2.Nodes.Add( name );
		}

		public void AddSubColumn( int index, string name )
		{
			this._col2.Nodes[index].Nodes.Add( name );
			this._col2.Nodes[index].Expand();
		}

		public void ClearSubColumns( int index )
		{
			this._col2.Nodes[index].Nodes.Clear();
		}

		public void RemoveColumn( int index )
		{
			this._col2.Nodes.RemoveAt( index );

			GC.Collect();
		}

		private void bAddFilter_Click( object sender, EventArgs ev )
		{
			int row = this.tlFilters.RowCount - 1;

			this.tlFilters.RowCount++;

			if( this.tlFilters.RowCount == 2 )
				this.tlFilters.RowStyles[0] = new RowStyle( SizeType.Absolute, 28 );

			// dodaj nowy styl
			this.tlFilters.RowStyles.Add( new RowStyle(SizeType.Absolute, 28) );

			// zaznaczenie do usunięcia
			CheckBox check = new CheckBox();
			check.Dock = DockStyle.Fill;
			check.CheckAlign = System.Drawing.ContentAlignment.MiddleCenter;
			check.CheckedChanged += new EventHandler( this.cbSelect_CheckedChanged );

			// wybór kolumny
			TreeComboBox column = new TreeComboBox( this._dropdown );
			column.DropDownStyle = ComboBoxStyle.DropDownList;
			column.Dock = DockStyle.Fill;
			column.OnItemChanged += new TreeViewEventHandler( this.tcbColumn_OnItemChanged );
			
			// filtr
			ComboBox filter = new ComboBox();
			filter.DropDownStyle = ComboBoxStyle.DropDownList;
			filter.Dock = DockStyle.Fill;
			filter.Enabled = false;
			filter.SelectedIndexChanged += new EventHandler( this.cbFilter_SelectedIndexChanged );

			TextBox modifier = new TextBox();
			modifier.Dock = DockStyle.Fill;
			modifier.AutoCompleteMode = AutoCompleteMode.None;
			modifier.Enabled = false;
			modifier.Margin = new Padding( 3, 4, 3, 3 );

			TextBox result = new TextBox();
			result.Dock = DockStyle.Fill;
			result.AutoCompleteMode = AutoCompleteMode.None;
			result.Enabled = false;
			result.Margin = new Padding( 3, 4, 3, 3 );

			CheckBox exclude = new CheckBox();
			exclude.Dock = DockStyle.Fill;
			exclude.CheckAlign = System.Drawing.ContentAlignment.MiddleCenter;
			exclude.Enabled = false;
			exclude.CheckedChanged += new EventHandler( this.cbExclude_CheckedChanged);

			CheckBox leave = new CheckBox();
			leave.Dock = DockStyle.Fill;
			leave.CheckAlign = System.Drawing.ContentAlignment.MiddleCenter;
			leave.Enabled = false;
			leave.CheckedChanged += new EventHandler( this.cbExclude_CheckedChanged );

			object[] tag = new object[8] { row, check, column, filter, modifier, result, exclude, leave };
			check.Tag = column.Tag = filter.Tag = modifier.Tag = result.Tag = exclude.Tag = leave.Tag = tag;
			
			this.tlFilters.Controls.Add( check, 0, row );
			this.tlFilters.Controls.Add( column, 1, row );
			this.tlFilters.Controls.Add( filter, 2, row );
			this.tlFilters.Controls.Add( modifier, 3, row );
			this.tlFilters.Controls.Add( result, 4, row );
			this.tlFilters.Controls.Add( exclude, 5, row );
			this.tlFilters.Controls.Add( leave, 6, row );
		}

		void cbExclude_CheckedChanged( object sender, EventArgs ev )
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

			this._locked = false;
		}

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
					case 0:
					case 1:
					case 2: break;
					case 3:
					case 4:
						for( int x = 4; x < 8; ++x )
							((Control)tag[x]).Enabled = true;
					break;
					case 5:
						((Control)tag[5]).Enabled = true;
					break;
				}
			}
		}

		private void tcbColumn_OnItemChanged( object sender, TreeViewEventArgs ev )
		{
			TreeComboBox tcbox = (TreeComboBox)sender;
			object[] tag = (object[])tcbox.Tag;
			ComboBox cbox = (ComboBox)tag[3];

			if( ev.Node.Parent == null && ev.Node.Nodes.Count > 0 )
			{
				if( cbox.Items.Count == 1 )
					return;

				cbox.Items.Clear();
				cbox.Items.Add( "Format" );
				cbox.SelectedIndex = 0;
			}
			else
			{
				if( cbox.Items.Count == 5 )
					return;

				cbox.Items.Clear();
				cbox.Items.Add( "Duże litery" );
				cbox.Items.Add( "Małe litery" );
				cbox.Items.Add( "Nazwa własna" );
				cbox.Items.Add( "Różny [<>]" );
				cbox.Items.Add( "Równy [==]" );
				cbox.Items.Add( "Format" );
				cbox.SelectedIndex = 0;
			}

			cbox.Enabled = true;
		}

		private void cbSelect_CheckedChanged( object sender, EventArgs ev )
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

		private void cbSelectAll_CheckedChanged( object sender, EventArgs ev )
		{
			if( this._locked )
				return;

			CheckState state = this.cbSelectAll.CheckState;

			// zamień status pozostałych kontrolek (zaznaczone lub nie)
			this._locked = true;
			for( int x = 0; x < this.tlFilters.RowCount-1; ++x )
				((CheckBox)this.tlFilters.GetControlFromPosition(0, x)).CheckState = state;
			this._locked = false;
		}
	}
}
