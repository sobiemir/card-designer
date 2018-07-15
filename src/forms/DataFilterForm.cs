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
using System.Runtime.InteropServices;
using CDesigner.Utils;

/* 
 * TODO:
 * [1] Filtrowanie - większy bądź równy, mniejszy bądź równy...
 * [1] Przy ustawieniu kolumny bez dzieci i dodaniu dzieci ustawić tylko filtr format.
 * [2] Przy dodawaniu nowej kontrolki checkbox dziedziczy stan z głównego (cbSelectAll).
 * [2] Po usunięciu elementów z nowych kolumn stara kolumna ma zmieniać nazwę na Kolumny, a nowa ma być usuwana.
 * [3] Po usunięciu wszystkich, główny checkbox ma zmienić wartość oraz reagować na nowo dodany rekord.
 * [3] Po usunięciu elementu/ów przycisk usuń staje się nieaktywny...
 * [3] Ustawienia, zobacz TODO w kodzie...
 */

namespace CDesigner
{
	public partial class DataFilterForm : Form
	{
		// ===== PRIVATE VARIABLES =====

		/// <summary>Synchronizacja wszystkich kontrolek GroupComboBox w liście.</summary>
		private GroupComboBoxSync _sync = null;
		
		/// <summary>Uchwyt do elementu Stare kolumny w liście.</summary>
		private GroupComboBoxItem _newcol = null;

		/// <summary>Uchwyt do elementu Nowe kolumny w liście.</summary>
		private GroupComboBoxItem _oldcol = null;

		/// <summary>Lista zapisanych filtrów dla danych.</summary>
		private List<FilterData> _filters = null;

		/// <summary>Lista starych filtrów (aktualizowana po akceptacji filtrów).</summary>
		private List<FilterData> _old_filters = null;

		/// <summary>Blokada przetwarzania danych.</summary>
		private bool _locked = false;

		// ===== PUBLIC VARIABLES =====

		/// <summary>Pobranie listy zapisanych filtrów.</summary>
		public List<FilterData> FilterData
		{
			get { return this._old_filters; }
		}

		// ===== PUBLIC FUNCTIONS =====

		/**
		 * <summary>
		 * Konstruktor klasy DataFilterForm.
		 * Po zatwierdzeniu filtra dane w klasie FilterCreator są zmieniane!
		 * </summary>
		 * 
		 * <param name="filter">Klasa do zatwierdzania filtrów.</param>
		 * --------------------------------------------------------------------------------------------------------- **/
		public DataFilterForm( FilterCreator filter )
		{
			// pobierz nazwę z pliku językowego
			string colname = Language.GetLine( "DataFilter", "GroupComboBox", 0 );

			this.InitializeComponent();

			this._sync = new GroupComboBoxSync();

			// dodaj element do panelu wyboru
			this._oldcol = this._sync.Add( colname, true );
			this._newcol = null;

			// dodaj kolumny do listy
			foreach( string column in filter.Columns )
				this._sync.Add( column, false, this._oldcol );

			this._filters     = new List<FilterData>();
			this._old_filters = new List<FilterData>();

			// pobierz tłumaczenia dla odpowiednich sekcji
			List<string> headers = Language.GetLines( "DataFilter", "Headers" );
			List<string> buttons = Language.GetLines( "DataFilter", "Buttons" );

			// przetłumacz przyciski
			this.bAddFilter.Text = buttons[0];
			this.bDelete.Text    = buttons[1];
			this.bRestore.Text   = buttons[2];
			this.bAccept.Text    = buttons[3];

			// przetłumacz nazwy nagłówków kolumn
			this.lColumn.Text     = headers[0];
			this.lFilterType.Text = headers[1];
			this.lModifier.Text   = headers[2];
			this.lResult.Text     = headers[3];
			this.lExclude.Text    = headers[4];

			// przetłumacz nazwę formularza
			this.Text = Language.GetLine( "FormNames", (int)FORMLIDX.DataFilter );

			// ustaw ikonę programu
			this.Icon = Program.GetIcon();
		}

		/**
		 * <summary>
		 * Pobieranie elemntu z określonej pozycji.
		 * </summary>
		 * 
		 * <param name="index">Indeks elementu do pobrania.</param>
		 * --------------------------------------------------------------------------------------------------------- **/
		public GroupComboBoxItem GetItemAt( int index )
		{
			if( this._sync.Count <= index )
				return null;

			return this._sync[index];
		}

		/**
		 * <summary>
		 * Dodawanie nowej kolumny do listy w polu rozwijanym.
		 * </summary>
		 * 
		 * <param name="name">Nazwa dodawanej kolumny.</param>
		 * --------------------------------------------------------------------------------------------------------- **/
		public void AddColumn( string name )
		{
			// załóż blokadę (nie zmienia filtrów)
			this._locked = true;
			
			// zmień nazwę "Kolumny" na "Stare kolumny" i dodaj element "Nowe kolumny"
			if( this._newcol == null )
			{
				List<string> colnames = Language.GetLines( "DataFilter", "GroupComboBox" );

				this._oldcol.first = false;
				this._oldcol.last  = true;
				this._oldcol.text  = colnames[1];

				// dodaj kolumnę "Nowe kolumny"
				this._newcol = this._sync.Insert( 0, colnames[2], true );

				// zwiększ numery indeksów w filtrach
				foreach( FilterData filter in this._old_filters )
					filter.column++;
				
				this._newcol.first = true;
				this._newcol.last  = false;

				Program.LogMessage( "Dodano nową pozycję: 'Nowe kolumny'." );
			}

			// dodaj kolumnę do listy
			GroupComboBoxItem item = this._sync.Add( name, false, this._newcol );
			int index = this._sync.FindItemIndex( item );

			this._locked = false;

			// zwiększ numer indeksu gdy ten jest większy od indeksu wstawianego elementu
			foreach( FilterData filter in this._old_filters )
				if( filter.column >= index )
					filter.column++;

			// przelicz szerokość listy rozwijanej
			this._sync.CalculateDropDownWidth( );

			Program.LogMessage( "Kolumna '" + name + "' została dodana do listy rozwiajnej." );
		}

		/**
		 * <summary>
		 * Przypisywanie kolumny do rodzica w liście dla pola rozwijanego.
		 * </summary>
		 * 
		 * <param name="index">Indeks kolumny nadrzędnej.</param>
		 * <param name="name">Nazwa dodawanego elementu.</param>
		 * <param name="old_index">Stary indeks kolumny.</param>
		 * --------------------------------------------------------------------------------------------------------- **/
		public void AddSubColumn( int index, string name, int old_index )
		{
			int repeat = 0;

			// załóż blokadę (nie zmienia filtrów)
			this._locked = true;

			for( int x = 0; x < this._sync.Count; ++x )
			{
				// dodawaj tylko do "nowych kolumn"
				if( this._sync[x].parent == this._newcol )
				{
					// wyszukaj pozycję kolumny nadrzędnej
					if( repeat == index )
					{
						GroupComboBoxItem item = this._sync.Add( name, false, this._sync[x], (object)old_index );
						int nindex = this._sync.FindItemIndex( item );

						// zwiększ numer indeksu gdy ten jest większy od indeksu wstawianego elementu
						foreach( FilterData filter in this._old_filters )
							if( filter.column >= nindex )
								filter.column++;

						Program.LogMessage( "Dodano nową kolumnę o nazwie '" + name + " do rodzica '"
							+ this._sync[x].text + "'." );
						break;
					}
					repeat++;
				}
			}

			this._locked = false;

			// przelicz szerokość listy rozwijanej
			this._sync.CalculateDropDownWidth( );
		}

		/**
		 * <summary>
		 * Usuwanie dzieci z wybranej kolumny (czyszczenie zawartości).
		 * Dodatkowo usuwa przypisane do dzieci filtry.
		 * </summary>
		 * 
		 * <param name="index">Indeks kolumny do czyszczenia.</param>
		 * --------------------------------------------------------------------------------------------------------- **/
		public void ClearSubColumns( int index )
		{
			if( index >= this._newcol.child.Count )
				return;
			
			GroupComboBoxItem item = this._newcol.child[index];

			// sprawdź czy element ma dzieci
			if( item.child != null )
			{
				for( int x = 0; x < item.child.Count; ++x )
				{
					int iidx = this._sync.FindItemIndex( item.child[x] );

					// usuń kontrolki z zaznaczoną kolumną
					for( int y = 0, ci = this.flFilters.Controls.Count; y < ci; ++y )
					{
						DataFilterRow row = (DataFilterRow)this.flFilters.Controls[y];
						
						if( row.cbColumn.SelectedIndex == iidx )
						{
							this._sync.RemoveCombo( row.cbColumn );
							row.Controls.Clear();
							this.flFilters.Controls.RemoveAt( y );

							// zmień indeksy elementów
							for( int z = y; z < this.flFilters.Controls.Count; ++z )
								((DataFilterRow)this.flFilters.Controls[z]).RowIndex--;

							--ci;
							--y;
						}
					}

					// usuń filtry które operują na usuwanej kolumnie
					this._filters.RemoveAll( c => c.column == iidx );
					this._old_filters.RemoveAll( c => c.column == iidx );
				}
			}

			// usuń dzieci
			this._sync.RemoveChildrens( item );

			GC.Collect();
		}

		/**
		 * <summary>
		 * Usuwanie wybranej kolumny i przypisanych do niej filtrów.
		 * Usuwa również dzieci i przypisane do nich filtry.
		 * </summary>
		 * 
		 * <param name="index">Indeks kolumny do usunięcia.</param>
		 * --------------------------------------------------------------------------------------------------------- **/
		public void RemoveColumn( int index )
		{
			if( index >= this._newcol.child.Count )
				return;

			// usuń dzieci
			this.ClearSubColumns( index );

			int iidx = this._sync.FindItemIndex( this._newcol.child[index] );

			// usuń kontrolki z zaznaczoną kolumną
			for( int x = 0, ci = this.flFilters.Controls.Count; x < ci; ++x )
			{
				DataFilterRow row = (DataFilterRow)this.flFilters.Controls[x];
						
				if( row.cbColumn.SelectedIndex == iidx )
				{
					this._sync.RemoveCombo( row.cbColumn );
					row.Controls.Clear();
					this.flFilters.Controls.RemoveAt( x );

					// zmień indeksy elementów
					for( int z = x; z < this.flFilters.Controls.Count; ++z )
						((DataFilterRow)this.flFilters.Controls[z]).RowIndex--;
					
					--ci;
					--x;
				}
			}
					
			// usuń filtry które operują na usuwanej kolumnie
			this._filters.RemoveAll( c => c.column == iidx );
			this._old_filters.RemoveAll( c => c.column == iidx );

			// usuń rodzica
			this._sync.Remove( this._newcol.child[index] );

			GC.Collect();
		}

		// ===== PRIVATE FUNCTIONS =====

		/**
		 * <summary>
		 * Dodawanie nowego filtra do listy.
		 * </summary>
		 * 
		 * <param name="sender">Obiekt wywołujący zdarzenie.</param>
		 * <param name="ev">Argumenty zdarzenia.</param>
		 * --------------------------------------------------------------------------------------------------------- **/
		private void bAddFilter_Click( object sender, EventArgs ev )
		{
			DataFilterRow row = new DataFilterRow( this._sync );
			
			// wykryj szerokość elementów
			if( (this.flFilters.Controls.Count + 1) * 28 > this.flFilters.Height )
			{
				int width = this.flFilters.Width - SystemInformation.VerticalScrollBarWidth;

				for( int x = 0; x < this.flFilters.Controls.Count; ++x )
					this.flFilters.Controls[x].Width = width;

				row.Width = width;
			}
			else
				row.Width = this.flFilters.Width;

			// zdarzenia
			row.cbSelect.CheckedChanged       += new EventHandler( this.cbSelect_CheckedChanged );
			row.cbColumn.SelectedIndexChanged += new EventHandler( this.cbColumn_SelectedIndexChanged );
			row.cbFilter.SelectedIndexChanged += new EventHandler( this.cbFilter_SelectedIndexChanged );
			row.tbModifier.LostFocus          += new EventHandler( this.tbModRes_LostFocus );
			row.tbResult.LostFocus            += new EventHandler( this.tbModRes_LostFocus );
			row.cbExclude.CheckedChanged      += new EventHandler( this.cbExclude_CheckedChanged );

			// indeks wiersza
			row.RowIndex = this.flFilters.Controls.Count;

			// dodaj wiersz
			this.flFilters.Controls.Add( row );

			// dodaj nowy filtr
			FilterData data = new FilterData();

			data.column   = -1;
			data.exclude  = false;
			data.filter   = -1;
			data.modifier = "";
			data.result   = "";
			data.index    = -1;
			data.parent   = -1;
			data.level    = -1;
		
			this._filters.Add( data );

			Program.LogMessage( "Dodano nowy filtr o id " + (this.flFilters.Controls.Count - 1) + "." );
		}

		/**
		 * <summary>
		 * Zmiana zaznaczenia wybranego wiersza.
		 * </summary>
		 * 
		 * <param name="sender">Obiekt wywołujący zdarzenie.</param>
		 * <param name="ev">Argumenty zdarzenia.</param>
		 * --------------------------------------------------------------------------------------------------------- **/
		private void cbSelect_CheckedChanged( object sender, EventArgs ev )
		{
			if( this._locked )
				return;
			
			int counter = 0;

			// sprawdź które rekordy są zaznaczone
			for( int x = 0; x < this.flFilters.Controls.Count; ++x )
				if( ((DataFilterRow)this.flFilters.Controls[x]).cbSelect.CheckState == CheckState.Checked )
					counter++;

			this._locked = true;
			
			// dobierz odpowiedni stan kontrolki na samej górze (która zaznacza wszystko)
			if( counter == 0 )
				this.cbSelectAll.CheckState = CheckState.Unchecked;
			else if( counter == this.flFilters.Controls.Count )
				this.cbSelectAll.CheckState = CheckState.Checked;
			else
				this.cbSelectAll.CheckState = CheckState.Indeterminate;

			this._locked = false;
		}

		/**
		 * <summary>
		 * Zmiana kolumny wewnątrz pola wyboru.
		 * </summary>
		 * 
		 * <param name="sender">Obiekt wywołujący zdarzenie.</param>
		 * <param name="ev">Argumenty zdarzenia.</param>
		 * --------------------------------------------------------------------------------------------------------- **/
		private void cbColumn_SelectedIndexChanged( object sender, EventArgs ev )
		{
			GroupComboBox combo = (GroupComboBox)sender;
			DataFilterRow row   = (DataFilterRow)combo.Tag;
			FilterData    data  = null;

			// zapisz numer kolumny
			data = this._filters[row.RowIndex];
			data.column = combo.SelectedIndex;

			// blokada przed dalszym przetwarzaniem
			if( this._locked )
				return;

			List<string>      fnames = Language.GetLines( "DataFilter", "ComboBox" );
			GroupComboBoxItem item   = combo.Items[combo.SelectedIndex];

			// tylko format dla elementów które posiadają dzieci
			if( item.child.Count > 0 )
			{
				row.cbFilter.Items.Clear();
				row.cbFilter.Items.Add( fnames[0] );
				row.cbFilter.SelectedIndex = 0;
			}
			// dla dzieci pokaż pełne możliwości formatowania
			else
			{
				row.cbFilter.Items.Clear();
				row.cbFilter.Items.Add( fnames[1] );
				row.cbFilter.Items.Add( fnames[2] );
				row.cbFilter.Items.Add( fnames[3] );
				row.cbFilter.Items.Add( fnames[4] );
				row.cbFilter.Items.Add( fnames[5] );
				row.cbFilter.SelectedIndex = 0;
			}

			// włącz kontrolkę
			row.cbFilter.Enabled = true;

			// zapisz informacje o filtrach
			data.index  = combo.Items[combo.SelectedIndex].index;
			data.parent = combo.Items[combo.SelectedIndex].parent.index;
			data.level  = combo.Items[combo.SelectedIndex].indent;
			data.filter = row.cbFilter.Items.Count == 1
				? (int)FILTERTYPE.Format
				: (int)FILTERTYPE.UpperCase;

			// realny indeks (ten przed ułożeniem)
			if( combo.Items[combo.SelectedIndex].tag == null )
				data.real_index = -1;
			else
				data.real_index = (int)combo.Items[combo.SelectedIndex].tag;
		}

		/**
		 * <summary>
		 * Zmiana filtra wewnątrz pola wyboru.
		 * </summary>
		 * 
		 * <param name="sender">Obiekt wywołujący zdarzenie.</param>
		 * <param name="ev">Argumenty zdarzenia.</param>
		 * --------------------------------------------------------------------------------------------------------- **/
		private void cbFilter_SelectedIndexChanged( object sender, EventArgs ev )
		{
			if( this._locked )
				return;

			ComboBox      cbox = (ComboBox)sender;
			DataFilterRow row  = (DataFilterRow)cbox.Tag;

			// wyłącz kontrolki
			row.tbModifier.Enabled = false;
			row.tbResult.Enabled   = false;
			row.cbExclude.Enabled  = false;

			// dostępny tylko format
			if( cbox.Items.Count == 1 )
			{
				// pobierz zaznaczony element
				GroupComboBoxItem item = row.cbColumn.Items[row.cbColumn.SelectedIndex];

				// włącz tylko możliwość modyfikacji formatu
				row.tbResult.Enabled = true;
				row.tbResult.Text    = "";

				// przykładowe formatowanie: #1 #2 #3
				for( int x = 1; x <= item.child.Count; ++x )
					row.tbResult.Text += x == 1
						? "#" + x
						: FilterCreator.DefaultFormat + "#" + x;

				// zapisz rezultat do filtra
				this.tbModRes_LostFocus( row.tbResult, null );
			}
			else
				switch( cbox.SelectedIndex + (int)FILTERTYPE.UpperCase )
				{
				// modyfikacje tekstu
				case (int)FILTERTYPE.UpperCase:
				case (int)FILTERTYPE.LowerCase:
				case (int)FILTERTYPE.TitleCase:
					row.cbExclude.Checked = false;
				break;
				// nie równe
				case (int)FILTERTYPE.NotEqual:
				// równe
				case (int)FILTERTYPE.Equal:
					row.tbModifier.Enabled = true;
					row.cbExclude.Enabled  = true;

					// włącz modyfikacje wyniku gdy checkbox nie jest zaznaczony
					if( row.cbExclude.Checked == false )
						row.tbResult.Enabled = true;
				break;
				}

			// zmień filtr na liście
			this._filters[row.RowIndex].filter = cbox.Items.Count == 1
				? (int)FILTERTYPE.Format
				: (int)FILTERTYPE.UpperCase + cbox.SelectedIndex;
		}

		/**
		 * <summary>
		 * Zmiana filtra wewnątrz pola wyboru.
		 * </summary>
		 * 
		 * <param name="sender">Obiekt wywołujący zdarzenie.</param>
		 * <param name="ev">Argumenty zdarzenia.</param>
		 * --------------------------------------------------------------------------------------------------------- **/
		private void tbModRes_LostFocus( object sender, EventArgs ev )
		{
			TextBox       tbox = (TextBox)sender;
			DataFilterRow row  = (DataFilterRow)tbox.Tag;

			if( tbox == row.tbModifier )
				this._filters[row.RowIndex].modifier = tbox.Text;
			else
				this._filters[row.RowIndex].result = tbox.Text;
		}

		/**
		 * <summary>
		 * Zmiana zaznaczenia wyłączenia wierszy spełniających kryteria filtrowania.
		 * </summary>
		 * 
		 * <param name="sender">Obiekt wywołujący zdarzenie.</param>
		 * <param name="ev">Argumenty zdarzenia.</param>
		 * --------------------------------------------------------------------------------------------------------- **/
		private void cbExclude_CheckedChanged( object sender, EventArgs ev )
		{
			if( this._locked )
				return;

			CheckBox      chkbx = (CheckBox)sender;
			DataFilterRow row   = (DataFilterRow)chkbx.Tag;

			// zaznacz lub odznacz pole rezultatu w zależności od zaznaczenia
			// rezultat nie jest potrzebny gdy chcemy wyłączyć elementy z bazy danych
			row.tbResult.Enabled = (chkbx.Checked == false);
			
			// uzupełnij informacje w filtrach
			this._filters[row.RowIndex].exclude = chkbx.Checked;

			this._locked = false;
		}

		/**
		 * <summary>
		 * Zmiana zaznaczenia wyłączenia wierszy spełniających kryteria filtrowania.
		 * </summary>
		 * 
		 * <param name="sender">Obiekt wywołujący zdarzenie.</param>
		 * <param name="ev">Argumenty zdarzenia.</param>
		 * --------------------------------------------------------------------------------------------------------- **/
		private void cbSelectAll_CheckedChanged( object sender, EventArgs ev )
		{
			CheckState state = this.cbSelectAll.CheckState;

			this.bDelete.Enabled = (state == CheckState.Checked || state == CheckState.Indeterminate);

			if( this._locked )
				return;

			// zamień status pozostałych kontrolek (zaznaczone lub nie)
			this._locked = true;
			for( int x = 0; x < this.flFilters.Controls.Count; ++x )
				((DataFilterRow)this.flFilters.Controls[x]).cbSelect.CheckState = state;
			this._locked = false;
		}

		/**
		 * <summary>
		 * Akceptacja wybranych filtrów.
		 * </summary>
		 * 
		 * <param name="sender">Obiekt wywołujący zdarzenie.</param>
		 * <param name="ev">Argumenty zdarzenia.</param>
		 * --------------------------------------------------------------------------------------------------------- **/
		private void bAccept_Click( object sender, EventArgs ev )
		{
			this._old_filters.Clear();

			// zastosuj filtry
			foreach( FilterData filter in this._filters )
			{
				// utwórz nowe klasy dla nowych filtrów
				// filtry te nie mogą się zmienić w trakcie zmian w oknie
				// klasy kopiowane są za pomocą referencji...
				FilterData data = new FilterData();
				
				data.column     = filter.column;
				data.exclude    = filter.exclude;
				data.filter     = filter.filter;
				data.index      = filter.index;
				data.level      = filter.level;
				data.modifier   = filter.modifier;
				data.parent     = filter.parent;
				data.real_index = filter.real_index;
				data.result     = filter.result;

				this._old_filters.Add( data );
			}
			
			GC.Collect();

			this.DialogResult = DialogResult.OK;
			this.Close();
		}

		/**
		 * <summary>
		 * Rysowanie dodatkowej linii w nagłówku w filtrach.
		 * </summary>
		 * 
		 * <param name="sender">Obiekt wywołujący zdarzenie.</param>
		 * <param name="ev">Argumenty zdarzenia.</param>
		 * --------------------------------------------------------------------------------------------------------- **/
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

		/**
		 * <summary>
		 * Rysowanie dodatkowej linii w panelu informacyjnym.
		 * </summary>
		 * 
		 * <param name="sender">Obiekt wywołujący zdarzenie.</param>
		 * <param name="ev">Argumenty zdarzenia.</param>
		 * --------------------------------------------------------------------------------------------------------- **/
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

		/**
		 * <summary>
		 * Usuwanie wybranych filtrów z listy.
		 * </summary>
		 * 
		 * <param name="sender">Obiekt wywołujący zdarzenie.</param>
		 * <param name="ev">Argumenty zdarzenia.</param>
		 * --------------------------------------------------------------------------------------------------------- **/
		private void bDelete_Click( object sender, EventArgs ev )
		{
			for( int x = 0; x < this.flFilters.Controls.Count; ++x )
			{
				DataFilterRow row = (DataFilterRow)this.flFilters.Controls[x];
				
				// sprawdź czy rekord jest zaznaczony
				if( row.cbSelect.CheckState != CheckState.Checked )
					continue;

				this._sync.RemoveCombo( row.cbColumn );
				row.Controls.Clear();
				this.flFilters.Controls.Remove( row );

				// zmień identyfikator pozostałych wierszy
				for( int y = x + 1; y < this.flFilters.Controls.Count; ++y )
					((DataFilterRow)this.flFilters.Controls[y]).RowIndex--;

				// usuń z listy filtrów
				this._filters.RemoveAt( x );

				--x;
			}

			GC.Collect();
		}

		/**
		 * <summary>
		 * Przywróć poprzednie ustawienia filtrów.
		 * </summary>
		 * 
		 * <param name="sender">Obiekt wywołujący zdarzenie.</param>
		 * <param name="ev">Argumenty zdarzenia.</param>
		 * --------------------------------------------------------------------------------------------------------- **/
		private void bRestore_Click( object sender, EventArgs ev )
		{
			List<string> filters = Language.GetLines( "DataFilter", "ComboBox" );

			this._filters.Clear();

			foreach( FilterData filter in this._old_filters )
			{
				// utwórz nowe klasy dla nowych filtrów
				FilterData data = new FilterData();
				
				data.column     = filter.column;
				data.exclude    = filter.exclude;
				data.filter     = filter.filter;
				data.index      = filter.index;
				data.level      = filter.level;
				data.modifier   = filter.modifier;
				data.parent     = filter.parent;
				data.real_index = filter.real_index;
				data.result     = filter.result;

				this._filters.Add( data );
			}

			DataFilterRow row = null;
			int width = this._filters.Count * 28 > this.flFilters.Height
				? this.flFilters.Width - SystemInformation.VerticalScrollBarWidth
				: this.flFilters.Width;

			foreach( DataFilterRow crow in this.flFilters.Controls )
			{
				this._sync.RemoveCombo( crow.cbColumn );
				crow.Controls.Clear();
			}

			this.flFilters.Controls.Clear();

			foreach( FilterData filter in this._filters )
			{
				int filter_index = 0;

				row = new DataFilterRow( this._sync );
				row.Width = width;

				if( this._newcol != null && filter.parent == this._newcol.index &&
					filter.filter == (int)FILTERTYPE.Format )
					row.cbFilter.Items.Add( filters[0] );
				else
				{
					row.cbFilter.Items.Add( filters[1] );
					row.cbFilter.Items.Add( filters[2] );
					row.cbFilter.Items.Add( filters[3] );
					row.cbFilter.Items.Add( filters[4] );
					row.cbFilter.Items.Add( filters[5] );
					
					filter_index = filter.filter - (int)FILTERTYPE.UpperCase;
				}

				// zdarzenia
				row.cbSelect.CheckedChanged       += new EventHandler( this.cbSelect_CheckedChanged );
				row.cbColumn.SelectedIndexChanged += new EventHandler( this.cbColumn_SelectedIndexChanged );
				row.cbFilter.SelectedIndexChanged += new EventHandler( this.cbFilter_SelectedIndexChanged );
				row.tbModifier.LostFocus          += new EventHandler( this.tbModRes_LostFocus );
				row.tbResult.LostFocus            += new EventHandler( this.tbModRes_LostFocus );
				row.cbExclude.CheckedChanged      += new EventHandler( this.cbExclude_CheckedChanged );
				
				row.cbSelect.Checked       = false;
				row.cbColumn.SelectedIndex = filter.column;
				row.cbFilter.SelectedIndex = filter_index;
				row.tbModifier.Text        = filter.modifier;
				row.tbResult.Text          = filter.result;
				row.cbExclude.Checked      = filter.exclude;

				this.flFilters.Controls.Add( row );
			}

			GC.Collect();
		}

		private void DataFilterForm_ResizeEnd( object sender, EventArgs ev )
		{
			// @TODO - do ustawień - @SEE: flFilters_Resize
			if( this.flFilters.Controls.Count < 11 )
				return;

			int width = this.flFilters.VerticalScroll.Visible
				? this.flFilters.Width - SystemInformation.VerticalScrollBarWidth
				: this.flFilters.Width;

			for( int x = 0; x < this.flFilters.Controls.Count; ++x )
				this.flFilters.Controls[x].Width = width;
		}

		private void flFilters_Resize( object sender, EventArgs ev )
		{
			// @TODO - do ustawień
			if( this.flFilters.Controls.Count > 10 )
				return;

			int width = this.flFilters.VerticalScroll.Visible
				? this.flFilters.Width - SystemInformation.VerticalScrollBarWidth
				: this.flFilters.Width;

			for( int x = 0; x < this.flFilters.Controls.Count; ++x )
				this.flFilters.Controls[x].Width = width;
		}
	}
}
