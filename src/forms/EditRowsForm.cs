///
/// $i04 EditRowsForm.cs
/// 
/// Okno edycji wierszy bazy danych.
/// Uruchamia okno edycji, dodawania i usuwania wierszy.
/// Plik zawiera formularz operujący tylko i wyłącznie na wierszach.
/// Modyfikacja kolumn odbywa się za pomocą innego formularza.
/// 
/// Autor: Kamil Biały
/// Od wersji: 0.7.x.x
/// Ostatnia zmiana: 2016-09-03
/// 
/// CHANGELOG:
/// [19.02.2016] Wersja początkowa.
/// [30.03.2016] Szybsze wyświetlanie danych - dodawanie pustych rekordów do tabeli oraz wyświetlanie
///              bezpośrednio ze źródła danych - mniejszy narzut na obliczenia i pamięć.
/// [03.09.2016] Wersja edytora przystosowana do nowych standardów wraz z komentarzami i zapisem danych.
///

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

namespace CDesigner
{
	/// 
	/// <summary>
    /// Formularz edycji wierszy z bazy danych.
    /// Pozwala na dowolne zmiany w wierszach oraz dodawanie i usuwanie wierszy.
    /// Posiada możliwość podzielenia danych na kilka stron, dzięki czemu dane wyświetlane są szybciej.
    /// Paginator pozwala na przełączanie się pomiędzy kolejnymi stronami.
    /// Wszelkie zmiany wykonane podczas działania formularza nie są zapisywane do głównego źródła danych,
    /// dzięki czemu wszystkie zmiany można wycofać zamykając formularz bez zapisywania.
    /// Możliwa jest zmiana ilości wyświetlanych wierszy na stronę.
	/// </summary>
	/// 
    /// @todo <dfn><small>[0.8.x.x]</small></dfn> Kopiowanie i wstawianie wierszy na początek i po zaznaczonym wierszu.
    /// @todo <dfn><small>[0.9.x.x]</small></dfn> Obsługa filtrów.
    /// @todo <dfn><small>[0.9.x.x]</small></dfn> Dodać do typów kolumn walutę i datę.
    /// @todo <dfn><small>[1.0.x.x]</small></dfn> W ustawieniach uwzględnić to, czy ESC i ENTER mają anulować i akceptować zmiany.
    /// @todo <dfn><small>[1.0.x.x]</small></dfn> Ograniczyć wyświetlanie wierszy do 36 tys (a może do 2mln?).
    /// @todo <dfn><small>[1.0.x.x]</small></dfn> Ustawienia.
    ///
	public partial class EditRowsForm : Form
    {
#region ZMIENNE
        
		/// <summary>Schowek z danymi wczytanej bazy.</summary>
        private DataStorage _storage;
        
        /// <summary>Ilość wyświetlanych wierszy na stronę.</summary>
		private int _rows_per_page;

        /// <summary>Numer aktualnie wyświetlanej strony liczony od 0.</summary>
		private int _page;

        /// <summary>Ilość wszystkich stron w tabeli.</summary>
		private int _pages;

        /// <summary>Indeks pierwszego wiersza w tabeli z danymi.</summary>
		private int _first_row;

        /// <summary>Identyfikator usuwanego elementu.</summary>
		private int _delete_id;

        /// <summary>Ilość usuwanych rekordów - funkcje czekają na skompletowanie wszystkich.</summary>
		private int _delete_pending;

		/// <summary>Blokada kontrolek (lub innych elementów) przed odświeżeniem.</summary>
		private bool _locked;

        /// <summary>Lista identyfikatorów wszystkich przeznaczonych do usunięcia wierszy.</summary>
		private List<int> _deleting_rows;

        /// <summary>Lista zaznaczonych kolumn w tabeli.</summary>
		private List<bool> _cols_selected;

        /// <summary>Lista z identyfikatorami kolumn ułożona w kolejności wyświetlania w tabeli.</summary>
        private List<int> _rows_data;

        /// <summary>Lista wierszy ze schowka w których zaszły zmiany - zawiera indeksy do zmian.</summary>
        private List<int> _rows_changes;

        /// <summary>Lista wszystkich zmian w postaci edycji i dodawania.</summary>
        private List<object[]> _change_log;

#endregion

#region KONSTRUKTOR / WŁAŚCIWOŚCI

        /// <summary>
		/// Konstruktor klasy.
		/// Tworzy instancje klasy i tłumaczy cały formularz na aktualny język.
        /// Przed wyświetleniem formularza edycji warto ustawić dla klasy dane na których ma operować.
		/// </summary>
		//* ============================================================================================================
		public EditRowsForm()
		{
			this.InitializeComponent();

            // ikona programu
			this.Icon = Program.GetIcon();

            // ilość wierszy na stronę - ustawienie początkowe
			this._rows_per_page = Settings.Info.EDF_RowsNumber;

			this._cols_selected  = new List<bool>();
            this._storage        = null;
            this._rows_data      = null;
            this._rows_changes   = null;
            this._change_log     = null;
            this._deleting_rows  = new List<int>();
            this._locked         = false;
            this._delete_id      = 0;
            this._delete_pending = 0;

            this._page      = 0;
            this._pages     = 0;
            this._first_row = 0;

			// podwójne buforowanie siatki - wydajność!
			var type = this.gvData.GetType();
			var info = type.GetProperty( "DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic );
			info.SetValue( this.gvData, true, null );

			// ikony przycisków
			this.bInsertRow.Image = Program.GetBitmap( BITMAPCODE.ItemAdd );
			this.bRemoveRow.Image = Program.GetBitmap( BITMAPCODE.ItemRemove );
			this.bFirstPage.Image = Program.GetBitmap( BITMAPCODE.FirstPage );
			this.bPrevPage.Image  = Program.GetBitmap( BITMAPCODE.PrevPage );
			this.bNextPage.Image  = Program.GetBitmap( BITMAPCODE.NextPage );
			this.bLastPage.Image  = Program.GetBitmap( BITMAPCODE.LastPage );

            // ilość wierszy na stronę i aktualna strona - z pustego i Salomon nie naleje
            this.tbRowsPerPage.Text = this._rows_per_page.ToString();
            this.lPageStat.Text     = "";

            this.translateForm();

            // ukryj drugi panel - na razie nie jest potrzebny
            this.scMain.Panel2Collapsed = true;
		}

        /// <summary>
		/// Pobiera lub ustawia nowy schowek danych.
		/// Po ustawieniu uzupełnia tabelę danymi ze schowka.
        /// </summary>
		//* ============================================================================================================
        public DataStorage Storage
        {
            get { return this._storage; }
            set
            {
                this._storage = value;

                // wyczyść wiersze
                this.gvData.Rows.Clear();
                this.gvData.Columns.Clear();

                GC.Collect();

                // dodaj kolumnę identyfikatora
                this.gvData.Columns.Add( "gvcID", "ID" );

                this.gvData.Columns[0].SortMode = DataGridViewColumnSortMode.NotSortable;
                this.gvData.Columns[0].ReadOnly = true;
                this.gvData.Columns[0].Width    = 40;
                this.gvData.Columns[0].DefaultCellStyle.BackColor = SystemColors.Control;

                // dodaj kolumny z bazy
                for( int x = 0; x < value.ColumnsNumber; ++x )
                {
                    this.gvData.Columns.Add( "gvc" + x, value.Column[x] );
                    this.gvData.Columns[x+1].SortMode = DataGridViewColumnSortMode.NotSortable;
                }

                // wyczyść listę kolumn
                this.lvColumns.Items.Clear();
                this._cols_selected.Clear();

                // zablokuj
                this._locked = true;

                // dodaj kolumny z bazy
                for( int x = 0; x < value.ColumnsNumber; ++x )
                {
                    this.lvColumns.Items.Add( value.Column[x] );
                    this.lvColumns.Items[x].Checked = true;

                    this._cols_selected.Add( false );
                }

                // przydziel pamięć na dane
                this._rows_data    = new List<int>( this._storage.RowsNumber );
                this._rows_changes = new List<int>( this._storage.RowsNumber );
                this._change_log   = new List<object[]>();

                // przypisz realny numer wiersza i informacje o braku edycji
                for( int x = 0; x < this._storage.RowsNumber; ++x )
                {
                    this._rows_data.Add( x );
                    this._rows_changes.Add( -1 );
                }

                // odblokuj
                this._locked = false;
            }
        }
        
        /// <summary>
        /// Aktualna strona wyświetlana w tabeli.
        /// Ilość stron obliczana jest na podstawie ilości wszystkich wierszy i wyświetlanych wierszy na stronie.
        /// Strony liczone są od wartości 1.
        /// </summary>
		//* ============================================================================================================
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
        
        /// <summary>
        /// Ilość wyświetlanych wierszy na stronę.
        /// W przypadku podania zera, wyświetlane są wszystkie dostępne wiersze.
        /// </summary>
		//* ============================================================================================================
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
        
        /// <summary>
        /// Ilość wszystkich stron.
        /// Wartość ta jest obliczana na podstawie ilości wszystkich wierszy i wyświetlanych wierszy na stronie.
        /// Właściwość tylko do odczytu.
        /// </summary>
		//* ============================================================================================================
		public int Pages
		{
			get { return this._pages; }
		}

#endregion

#region FUNKCJE PODSTAWOWE

        /// <summary>
		/// Translator formularza.
		/// Funkcja tłumaczy wszystkie statyczne elementy programu.
		/// Wywoływana jest z konstruktora oraz podczas odświeżania ustawień językowych.
        /// Jej użycie nie powinno wykraczać poza dwa wyżej wymienione przypadki.
		/// </summary>
		//* ============================================================================================================
		protected void translateForm()
        {
            var values = Language.GetLines( "EditRows", "Labels" );
            this.lRowsPerPage.Text = values[(int)LANGCODE.I04_LAB_ROWSONPAGE];

            values = Language.GetLines( "EditRows", "Buttons" );
            this.bCancel.Text = values[(int)LANGCODE.I04_BUT_CANCEL];
            this.bSave.Text   = values[(int)LANGCODE.I04_BUT_SAVE];
            
            // tytuł okna
			this.Text = Language.GetLine( "FormNames", (int)LANGCODE.GFN_EDITDATA );
        }

        /// <summary>
        /// Funkcja odświeża wszystkie wyświetlane dane.
        /// Dzięki niej obliczane są wszystkie elementy potrzebne do paginacji.
        /// Po obliczeniach dodaje rekordy do tabeli zgodnie z ustawioną opcją ilości wierszy na stronę.
        /// Ostatnia strona zawiera dodatkowy pusty rekord służący do dodawania do schowka nowych rekordów.
        /// Z racji tego iż dane nie są przechowywane bezpośrednio w tabeli, wszelkie operacje odwołują się do schowka,
        /// tak więc zmiana strony, strumienia danych lub ilości wyświetlanych wierszy na stronę jest dużo szybsza,
        /// ponieważ nie trzeba czyścić wszystkich wierszy - są one puste, więc kolejność nie ma znaczenia.
        /// </summary>
		//* ============================================================================================================
        public void refreshDataRange()
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
				int endelem   = this._rows_data.Count;

				// ilość wyświetlanych wierszy
				totalrows = endelem - startelem;
			
				// zapisz indeksy pierwszego i ostatniego elementu
				this._first_row = startelem;
			}
			// podziel wiersze na strony
			else
			{
				// ilość wszystkich stron
				this._pages = this._rows_data.Count / this._rows_per_page + 1;
			
				// zmień stronę jeżeli indeks wykracza poza granicę
				if( this._page > this._pages - 1 )
					this.Page = this._pages;
			
				// indeks elementu początkowego i ostatniego
				int startelem = this._page * this._rows_per_page;
				int endelem   = (this._page + 1) * this._rows_per_page;

				// poprawka ostatniego elementu (gdy wykracza poza faktyczną ilość elementów w tablicy)
				if( endelem > this._rows_data.Count )
					endelem = this._rows_data.Count;

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
			this.lPageStat.Text = String.Format
            (
                Language.GetLine("EditRows", "Labels", (int)LANGCODE.I04_LAB_PAGEOFNUM),
                this._pages
            );
			this.tbPageNum.Text = (this._page + 1).ToString();

			// odśwież kontrolkę
			this.gvData.Refresh();
		}
        
        /// <summary>
        /// Funkcja usuwa wiersz o podanym identyfikatorze.
        /// Usuwany jest wiersz ze zmiennej zawierającej indeksy, aby można było powrócić do danych sprzed modyfikacji.
        /// Usuwanie polega na przeniesieniu wszystkich indeksów znajdujących się powyżej usuwanego o jeden w dół
        /// i usunięiciu ostatniego elementu w liście.
        /// </summary>
        /// 
        /// <param name="index">Indeks wiersza przeznaczonego do usunięcia.</param>
		//* ============================================================================================================
        private void _removeRow( int index )
        {
            for( int x = index; x < this._rows_data.Count - 1; ++x )
                this._rows_data[x] = this._rows_data[x+1];
            this._rows_data.RemoveAt( this._rows_data.Count - 1 );
        }
#endregion

#region OBSŁUGA TABELI Z DANYMI
        /// @cond EVENTS

        /// <summary>
        /// Akcja wywoływana podczas próby uzyskania dostępu do wartości komórki.
        /// Dane tabeli nie są przechowywane bezpośrednio w niej - dane niezmienione pobierane są bezpośrednio
        /// ze schowka, zaś zmienione ze zmiennej zawierającej listę zmian w wybranych wierszach.
        /// Dla indeksów wierszy przygotowana jest osobna zmienna po to, aby nie naruszać głównej struktury danych
        /// przed zapisem w razie gdyby użytkownik chciał wycować wszystkie wprowadzone zmiany.
        /// </summary>
        /// 
		/// <param name="sender">Obiekt wywołujący zdarzenie.</param>
		/// <param name="ev">Argumenty zdarzenia</param>
		//* ============================================================================================================
        private void gvData_CellValueNeeded( object sender, DataGridViewCellValueEventArgs ev )
		{
            var index = ev.RowIndex + this._first_row;

			// kolumna indeksu
			if( ev.ColumnIndex == 0 )
				ev.Value = index + 1;
			// wiersz spoza zakresu...
			else if( index >= this._rows_data.Count )
				ev.Value = "";
			// pobierz wartość z tablicy
			else
            {
                // nowy rekord
                if( this._rows_data[index] < 0 )
                {
                    int changeidx = -this._rows_data[index] - 1;
                    ev.Value = this._change_log[changeidx][ev.ColumnIndex-1];
                }
                else
                {
                    int rowidx    = this._rows_data[index];
                    int changeidx = this._rows_changes[rowidx]; 

                    // zmieniony rekord
                    if( changeidx != -1 && this._change_log[changeidx][ev.ColumnIndex-1] != null )
                        ev.Value = this._change_log[changeidx][ev.ColumnIndex-1].ToString();
                    // brak zmian
                    else
                        ev.Value = this._storage.Row[rowidx][ev.ColumnIndex-1];
                }
            }
		}

        /// <summary>
        /// Akcja wywoływana podczas zmiany wartości lub dodawania komórki do tabeli.
        /// Wszystkie zmiany w komórkach zapisywane są do osobnej zmiennej aby umożliwić ich wycofanie.
        /// W przypadku gdy tabela zmian nie zawiera danych dla danego wiersza, wyświetlane są dane ze schowka.
        /// Podczas dodawania nowych wartości indeksy dla zmian są odwracane, aby nie mylić z indeksami ze schowka.
        /// </summary>
        /// 
		/// <param name="sender">Obiekt wywołujący zdarzenie.</param>
		/// <param name="ev">Argumenty zdarzenia</param>
		//* ============================================================================================================
		private void gvData_CellValuePushed( object sender, DataGridViewCellValueEventArgs ev )
		{
			bool need_refresh = false;
			int  row          = ev.RowIndex + this._first_row;
            int  changeidx    = this._change_log.Count;

			// sprawdź czy indeks nie wykracza poza zakres
			if( row >= this._rows_data.Count )
			{
				// jeżeli wykracza za dużo, nic nie rób...
				if( row >= this._rows_data.Count + 1 )
					return;

                // dodaj nowe rekordy
				this._rows_data.Add( -changeidx - 1 );
				need_refresh = true;
			}
            
            // oblicz indeksy
            int colidx = ev.ColumnIndex - 1;
            int rowidx = this._rows_data[row] < 0
                ? -this._rows_data[row] - 1
                : this._rows_changes[this._rows_data[row]];

            // przydziel miejsce jeżeli brak
            if( need_refresh || rowidx == -1 )
            {
                var values = new object[this._storage.ColumnsNumber];
                this._change_log.Add( values );

				// i uzupełnij je pustymi wartościami - w przypadku starych pól są to nule
				for( int x = 0; x < this._storage.ColumnsNumber; ++x )
					this._change_log[changeidx][x] = need_refresh ? "" : null;

                // istniejące rekordy
                if( this._rows_data[row] >= 0 )
                    this._rows_changes[this._rows_data[row]] = changeidx;

                rowidx = changeidx;
            }

			// uzupełnij pustą lub zmień wartość wybranej komórki
            if( ev.Value != null )
		        this._change_log[rowidx][colidx] = (object)ev.Value.ToString();

			// odśwież dane
			if( need_refresh )
				this.refreshDataRange();
		}

        /// <summary>
        /// Akcja wywoływana podczas rozpoczęcia usuwania wiersza.
        /// Zbiera wszystkie identyfikatory zaznaczonych kolumn i przygotowuje je do usunięcia.
        /// </summary>
        /// 
		/// <param name="sender">Obiekt wywołujący zdarzenie.</param>
		/// <param name="ev">Argumenty zdarzenia</param>
		//* ============================================================================================================
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
			if( ev.Row.Index + this._first_row >= this._rows_data.Count )
			{
				// teraz to już przesadziło...
				if( ev.Row.Index + this._first_row - 1 >= this._rows_data.Count )
				{
					this._delete_id = -1;
					return;
				}
				this._delete_id = ev.Row.Index + this._first_row - 1;
			}
			else
				this._delete_id = ev.Row.Index + this._first_row;
		}

        /// <summary>
        /// Akcja wywoływana podczas usuwania wierszy.
        /// Wiersze usuwane są ze zmiennej zawierającej indeksy wierszy ze schowka.
        /// Dzięki temu możliwe jest cofnięcie wszystkich dokonanych zmian.
        /// </summary>
        /// 
		/// <param name="sender">Obiekt wywołujący zdarzenie.</param>
		/// <param name="ev">Argumenty zdarzenia</param>
		//* ============================================================================================================
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
						this._removeRow( this._deleting_rows[x] );

					// wyczyść tablicę
					this._deleting_rows.Clear();

					// odśwież widok
					this.refreshDataRange();
				}

				// nie pozwól na wykonanie kodu poniżej podczas oczekiwania
				this._delete_pending--;
				return;
			}

			// usuń wiersz
			if( this._delete_id != -1 )
                this._removeRow( this._delete_id );

			// odśwież widok
			this.refreshDataRange();
		}

        /// @endcond
#endregion

#region OBSŁUGA KONTROLI TABELI DANYCH
        /// @cond EVENTS
        
        /// <summary>
        /// Akcja wywoływana podczas kliknięcia w przycisk przejścia do ostatniej strony.
        /// Gdy tabela nie ma więcej stron lub po prostu aktualną stroną jest ostatnia strona, to funkcja nic nie robi.
        /// </summary>
        /// 
		/// <param name="sender">Obiekt wywołujący zdarzenie.</param>
		/// <param name="ev">Argumenty zdarzenia</param>
		//* ============================================================================================================
		private void bLastPage_Click( object sender, EventArgs ev )
		{
			if( this._page == this._pages -1 )
				return;

			this._page = this._pages - 1;

			this.refreshDataRange();
		}
        
        /// <summary>
        /// Akcja wywoływana podczas kliknięcia w przycisk przejścia do pierwszej strony.
        /// Gdy aktualną stroną jest pierwsza strona, to funkcja nic nie robi.
        /// </summary>
        /// 
		/// <param name="sender">Obiekt wywołujący zdarzenie.</param>
		/// <param name="ev">Argumenty zdarzenia</param>
		//* ============================================================================================================
		private void bFirstPage_Click( object sender, EventArgs ev )
		{
			if( this._page == 0 )
				return;

			this._page = 0;

			this.refreshDataRange();
		}
        
        /// <summary>
        /// Akcja wywoływana podczas kliknięcia w przycisk przejścia do strony wstecz.
        /// Gdy aktualną stroną jest pierwsza strona, to funkcja nic nie robi.
        /// </summary>
        /// 
		/// <param name="sender">Obiekt wywołujący zdarzenie.</param>
		/// <param name="ev">Argumenty zdarzenia</param>
		//* ============================================================================================================
		private void bPrevPage_Click( object sender, EventArgs ev )
		{
			if( this._page == 0 )
				return;

			this._page -= 1;

			this.refreshDataRange();
		}
        
        /// <summary>
        /// Akcja wywoływana podczas kliknięcia w przycisk przejścia do następnej strony.
        /// Gdy aktualną stroną jest ostatnia strona, to funkcja nic nie robi.
        /// Gdy tabela nie ma stron, to pierwsza strona jest zarazem pierwszą i ostatnią.
        /// </summary>
        /// 
		/// <param name="sender">Obiekt wywołujący zdarzenie.</param>
		/// <param name="ev">Argumenty zdarzenia</param>
		//* ============================================================================================================
		private void bNextPage_Click( object sender, EventArgs ev )
		{
			if( this._page == this._pages - 1 )
				return;

			this._page += 1;

			this.refreshDataRange();
		}
        
        /// <summary>
        /// Akcja wywoływana po zmianie tekstu w kontrolce z numerem strony.
        /// Zmienia stronę na numer podany w kontrolce jeżeli strona istnieje i kontrolka zawiera poprawny numer.
        /// </summary>
        /// 
		/// <param name="sender">Obiekt wywołujący zdarzenie.</param>
		/// <param name="ev">Argumenty zdarzenia</param>
		//* ============================================================================================================
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
        
        /// <summary>
        /// Akcja wywoływana po naciśnięciu przycisku na kontrolce z numerem strony.
        /// Przepuszcza tylko klawisze kontrolne i cyfry.
        /// </summary>
        /// 
		/// <param name="sender">Obiekt wywołujący zdarzenie.</param>
		/// <param name="ev">Argumenty zdarzenia</param>
		//* ============================================================================================================
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
        
        /// <summary>
        /// Akcja wywoływana po naciśnięciu przycisku na kontrolce z numerem strony.
        /// Reaguje na klawisz ENTER i traktuje aktualną wartość kontrolki jako wartość wysłaną.
        /// W odróżnieniu od zdarzenia KeyPress, zdarzenie KeyDown zawiera kody naciśniętych klawiszy.
        /// </summary>
        /// 
		/// <param name="sender">Obiekt wywołujący zdarzenie.</param>
		/// <param name="ev">Argumenty zdarzenia</param>
		//* ============================================================================================================
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
					this.refreshDataRange();
				}
			}
		}

        /// <summary>
        /// Akcja wywoływana po zmianie tekstu w kontrolce zawierającą ilość wierszy na stronę.
        /// W przypadku podania wartości 0 do kontrolki, wyświetlone zostaną wszystkie dostępne wiersze.
        /// Może to się odbić na szybkości działania lub szybkości przeglądania, choć nie musi.
        /// </summary>
        /// 
		/// <param name="sender">Obiekt wywołujący zdarzenie.</param>
		/// <param name="ev">Argumenty zdarzenia</param>
		//* ============================================================================================================
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
        
        /// <summary>
        /// Akcja wywoływana po naciśnięciu przycisku na kontrolce zawierającą ilość wierszy na stronę.
        /// Przepuszcza tylko klawisze kontrolne i cyfry.
        /// </summary>
        /// 
		/// <param name="sender">Obiekt wywołujący zdarzenie.</param>
		/// <param name="ev">Argumenty zdarzenia</param>
		//* ============================================================================================================
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
        
        /// <summary>
        /// Akcja wywoływana po naciśnięciu przycisku na kontrolce zawierającą ilość wierszy na stronę.
        /// Reaguje na klawisz ENTER i traktuje aktualną wartość kontrolki jako wartość wysłaną.
        /// W odróżnieniu od zdarzenia KeyPress, zdarzenie KeyDown zawiera kody naciśniętych klawiszy.
        /// </summary>
        /// 
		/// <param name="sender">Obiekt wywołujący zdarzenie.</param>
		/// <param name="ev">Argumenty zdarzenia</param>
		//* ============================================================================================================
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
					this.refreshDataRange();
				}
			}
		}

        /// <summary>
        /// Akcja wywoływana po kliknięciu w przycisk dodawania wiersza.
        /// Po kliknięciu przenosi kursor do ostatniego rekordu na ostatniej stronie.
        /// W związku z czym aktualną stroną będzie ostatnia dostępna strona.
        /// W przypadku gdy ilość wierszy na stronie jest równa ilości wierszy w schowku, pole do nowego rekordu
        /// widoczne będzie w ostatniej stronie.
        /// </summary>
        /// 
		/// <param name="sender">Obiekt wywołujący zdarzenie.</param>
		/// <param name="ev">Argumenty zdarzenia</param>
		//* ============================================================================================================
		private void bInsertRow_Click( object sender, EventArgs ev )
		{
			// przejdź do ostatniej strony jeżeli ta nie jest ostatnią
			if( this._page != this._pages - 1 )
			{
				this.Page = this._pages;
				this.refreshDataRange();
			}

			// pobierz komórkę w pierwszej kolumnie i ostatnim wierszu
			int              last = this.gvData.Rows.Count - 1;
			DataGridViewCell cell = null;

			// sprawdź czy komórka jest widoczna
			for( int x = 1; x <= this._storage.ColumnsNumber; ++x )
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

        /// <summary>
        /// Akcja wywoływana po kliknięciu w przydisk usuwania wiersza.
        /// Po kliknięciu usuwa wszystkie zaznaczone wiersze.
        /// Działa w taki sam sposób jak wciśnięcie przycisku DELETE po zaznaczeniu wierszy.
        /// Funkcja nie usuwa danych ze schowka, aby można było powrócić do wczesniejszych danych.
        /// </summary>
        /// 
		/// <param name="sender">Obiekt wywołujący zdarzenie.</param>
		/// <param name="ev">Argumenty zdarzenia</param>
		//* ============================================================================================================
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

			// uporządkuj malejąco (usuwanie od największego) - aby uniknąć problemów z indeksami
			this._deleting_rows.OrderByDescending( w => w );

			// usuń z tablicy i z listy
			for( int x = 0; x < this._deleting_rows.Count; ++x )
			{
				this.gvData.Rows.RemoveAt( this._deleting_rows[x] );
                this._removeRow( this._deleting_rows[x] );
			}

			// wyczyść usuwane wiersze
			this._deleting_rows.Clear();

			// odśwież widok
			this.refreshDataRange();
		}
        
        /// <summary>
        /// Akcja wywoływana po zmianie zaznaczenia w tabeli.
        /// Aktywuje i deaktywuje przycisk usuwania odpowiednio do zaznaczonych kolumn.
        /// Jeżeli w danym wierszu zaznaczone są wszystkie kolumny może on być przeznaczony do usunięcia.
        /// </summary>
        /// 
		/// <param name="sender">Obiekt wywołujący zdarzenie.</param>
		/// <param name="ev">Argumenty zdarzenia</param>
		//* ============================================================================================================
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

        /// @endcond
#endregion

#region PASEK AKCJI
        /// @cond EVENTS

		/// <summary>
        /// Analiza wciśniętych klawiszy w obrębie formularza.
        /// Funkcja tworzy skrót do ukrywania / pokazywania panelu bocznego z ustawieniami dodatkowymi.
        /// </summary>
        /// 
        /// <param name="msg">Przechwycone zdarzenie wciśnięcia klawisza.</param>
        /// <param name="keys">Informacje o wciśniętych klawiszach.</param>
        /// 
        /// <returns>Informacja o tym czy klawisz został przechwycony.</returns>
		//* ============================================================================================================
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

		/// <summary>
        /// Akcja wywoływana podczas rysowania panelu akcji.
        /// Rysuje kreskę oddzielającą panel akcji od kontenera z treścią.
        /// </summary>
        /// 
        /// <param name="sender">Obiekt wywołujący zdarzenie.</param>
        /// <param name="ev">Argumenty zdarzenia.</param>
		//* ============================================================================================================
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

        /// <summary>
        /// Akcja wywoływana podczas kliknięcia w przycisk anulowania zmian.
        /// Funkcja jedyne co na razie robi to zamyka formularz.
        /// </summary>
        /// 
        /// <param name="sender">Obiekt wywołujący zdarzenie.</param>
        /// <param name="ev">Argumenty zdarzenia.</param>
        /// 
        /// <seealso cref="bSave_Click" />
		//* ============================================================================================================
        private void bCancel_Click( object sender, EventArgs ev )
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        /// <summary>
        /// Akcja wywoływana podczas kliknięcia w przycisk zapisania zmian.
        /// Zapisuje wszystkie zmiany które wystąpiły podczas działania formularza do źródła danych.
        /// Po zapisie nie ma powrotu do wcześniejszych danych, należy wczytać plik od nowa.
        /// Zapis do pliku oferuje inny formularz.
        /// </summary>
        /// 
        /// <param name="sender">Obiekt wywołujący zdarzenie.</param>
        /// <param name="ev">Argumenty zdarzenia.</param>
        /// 
        /// <seealso cref="bCancel_Click" />
		//* ============================================================================================================
        private void bSave_Click( object sender, EventArgs ev )
        {
            int dataidx = 0;
            int rowidx  = 0;

            // tryb edycji danych
            this._storage.editMode( null );

            while( this._storage.nextRow() )
            {
                // koniec zmian, teraz nowe wiersze, usuń pozostałości
                if( this._rows_data[dataidx] < 0 || this._rows_data[dataidx] != rowidx )
                    this._storage.removeCurrentRow();
                else if( this._rows_data[dataidx] == rowidx )
                {
                    // brak zmian
                    if( this._rows_changes[rowidx] == -1 )
                    {
                        rowidx++;
                        dataidx++;
                        continue;
                    }

                    string[] values = this._storage.getCurrentRow();
                    int      logidx = this._rows_changes[rowidx];

                    // przypisz nowe wartości
                    for( int x = 0; x < this._storage.ColumnsNumber; ++x )
                        if( this._change_log[logidx][x] != null )
                            values[x] = this._change_log[logidx][x].ToString();

                    // zamień
                    this._storage.replaceCurrentRow( values );

                    dataidx++;
                }
                rowidx++;
            }

            // nowe rekordy
            for( int x = dataidx; x < this._rows_data.Count; ++x )
            {
                if( this._rows_data[x] >= 0 )
                    continue;

                string[] values = new string[this._storage.ColumnsNumber];

                rowidx = -this._rows_data[x] - 1;

                for( int y = 0; y < this._storage.ColumnsNumber; ++y )
                    values[y] = this._change_log[rowidx][y].ToString();

                this._storage.addNewRowToEnd( values );
            }

            // zatwierdź zmiany i sprawdź poprawność danych
            this._storage.checkIntegrity();

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        /// @endcond
#endregion

#region PASEK BOCZNY - NA RAZIE NIE UŻYWANE
        /// @cond EVENTS

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
			/*switch( this._storage.Types[item.Index] )
			{
				case DATATYPE.String:    this.cbColumnType.SelectedIndex = 0; break;
				case DATATYPE.Integer:   this.cbColumnType.SelectedIndex = 1; break;
				case DATATYPE.Float:     this.cbColumnType.SelectedIndex = 2; break;
				case DATATYPE.Character: this.cbColumnType.SelectedIndex = 3; break;
			}*/

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

        /// @endcond
#endregion
    }
}
