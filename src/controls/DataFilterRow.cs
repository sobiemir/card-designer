using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace CDesigner
{
	public class DataFilterRow : Panel
	{
		private float[] _col_per = null;
		private int[] _col_posx = null;
		private int[] _col_width = null;
		private int _index = 0;

		public CheckBox      cbSelect = null;
		public GroupComboBox cbColumn = null;
		public ComboBox      cbFilter = null;
		public TextBox       tbModifier = null;
		public TextBox       tbResult = null;
		public CheckBox      cbExclude = null;

		public int RowIndex
		{
			get { return this._index; }
			set { this._index = value; }
		}

		public DataFilterRow( GroupComboBoxSync sync ) : base()
		{
			int pad1, pad2;

			// wysokość tabeli
			this.Height = 28;

			// tworzenie kontrolek
			this.cbSelect   = new CheckBox();
			this.cbColumn   = new GroupComboBox();
			this.cbFilter   = new ComboBox();
			this.tbModifier = new TextBox();
			this.tbResult   = new TextBox();
			this.cbExclude  = new CheckBox();

			// przypisz wiersz do każdej z kontrolek
			this.cbSelect.Tag = this.cbColumn.Tag = this.cbFilter.Tag = this.tbModifier.Tag =
				this.tbResult.Tag = this.cbExclude.Tag = this;

			// rozmiary kolumn
			this._col_per   = new float[6] { 5.0f, 24.0f, 20.0f, 22.0f, 24.0f, 5.0f };
			this._col_posx  = new int[6] { 0, 0, 0, 0, 0, 0 };
			this._col_width = new int[6] { 0, 0, 0, 0, 0, 0 };

			// przyleganie pól zaznaczenia
			this.cbSelect.CheckAlign = this.cbExclude.CheckAlign =
				System.Drawing.ContentAlignment.MiddleCenter;

			// lista rozwijana
			this.cbFilter.DropDownStyle = ComboBoxStyle.DropDownList;

			// brak okienka automatycznego uzupełniania
			this.tbModifier.AutoCompleteMode = this.tbResult.AutoCompleteMode =
				AutoCompleteMode.None;

			// wyłącz poszczególne kontrolki
			this.cbFilter.Enabled = this.tbModifier.Enabled = this.tbResult.Enabled =
				this.cbExclude.Enabled = false;

			// margines pól wyboru
			pad1 = (this.Height - this.cbFilter.Height) / 2;
			pad2 = this.Height - this.cbFilter.Height - pad1;

			this.cbFilter.Margin = this.cbColumn.Margin = new Padding( 2, pad1, 2, pad2 );
			
			// margines pól tekstowych
			pad1 = (this.Height - this.tbModifier.Height) / 2;
			pad2 = this.Height - this.tbModifier.Height - pad1;

			this.tbModifier.Margin = this.tbResult.Margin = new Padding( 2, pad1, 2, pad2 );

			// brak marginesu dla tabeli
			this.Margin = new Padding( 0 );

			// dodaj kontrolkę do synchronizacji
			sync.AddCombo( this.cbColumn );
			this.cbColumn.CalculateDropDownWidth();

			// dodaj kontrolki do panelu
			this.Controls.Add( this.cbSelect );
			this.Controls.Add( this.cbColumn );
			this.Controls.Add( this.cbFilter );
			this.Controls.Add( this.tbModifier );
			this.Controls.Add( this.tbResult );
			this.Controls.Add( this.cbExclude );
		}

		public void CalculateLayout( )
		{
			if( this._col_width == null )
				return;

			this._col_width[0] = (int)((float)this.Width * (this._col_per[0] / 100.0f));
			this._col_width[1] = (int)((float)this.Width * (this._col_per[1] / 100.0f)) - 4;
			this._col_width[2] = (int)((float)this.Width * (this._col_per[2] / 100.0f)) - 4;
			this._col_width[3] = (int)((float)this.Width * (this._col_per[3] / 100.0f)) - 4;
			this._col_width[4] = (int)((float)this.Width * (this._col_per[4] / 100.0f)) - 4;
			this._col_width[5] = (int)((float)this.Width * (this._col_per[5] / 100.0f));

			this._col_posx[0] = 0;
			this._col_posx[1] = this._col_width[0] + 2;
			this._col_posx[2] = this._col_posx[1] + this._col_width[1] + 4;
			this._col_posx[3] = this._col_posx[2] + this._col_width[2] + 4;
			this._col_posx[4] = this._col_posx[3] + this._col_width[3] + 4;
			this._col_posx[5] = this._col_posx[4] + this._col_width[4] + 2;

			this.cbSelect.Width   = this._col_width[0];
			this.cbColumn.Width   = this._col_width[1];
			this.cbFilter.Width   = this._col_width[2];
			this.tbModifier.Width = this._col_width[3];
			this.tbResult.Width   = this._col_width[4];
			this.cbExclude.Width  = this._col_width[5];

			this.cbSelect.Location   = new Point( this._col_posx[0], this.cbSelect.Margin.Top );
			this.cbColumn.Location   = new Point( this._col_posx[1], this.cbColumn.Margin.Top );
			this.cbFilter.Location   = new Point( this._col_posx[2], this.cbFilter.Margin.Top );
			this.tbModifier.Location = new Point( this._col_posx[3], this.tbModifier.Margin.Top );
			this.tbResult.Location   = new Point( this._col_posx[4], this.tbResult.Margin.Top );
			this.cbExclude.Location  = new Point( this._col_posx[5], this.cbExclude.Margin.Top );
		}

		protected override void OnSizeChanged( EventArgs ev )
		{
			this.CalculateLayout();
			base.OnSizeChanged( ev );
		}
	}
}
