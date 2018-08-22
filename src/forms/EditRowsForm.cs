///
/// $i08 EditRowsForm.cs (I04)
/// 
/// Okno edycji wierszy bazy danych.
/// Uruchamia okno edycji, dodawania i usuwania wierszy.
/// Plik zawiera formularz operujący tylko i wyłącznie na wierszach.
/// Modyfikacja kolumn odbywa się za pomocą innego formularza.
/// 
/// Autor: Kamil Biały
/// Od wersji: 0.8.x.x
/// Ostatnia zmiana: 2016-12-24
/// 
/// CHANGELOG:
/// [13.02.2016] Wersja początkowa.
/// [30.03.2016] Szybsze wyświetlanie danych - dodawanie pustych rekordów do tabeli oraz wyświetlanie
///              bezpośrednio ze źródła danych - mniejszy narzut na obliczenia i pamięć.
/// [24.07.2016] Drobna zmiana nazewnictwa typu numerycznego dla bitmap i typów.
/// [12.11.2016] Zmiana nazwy pliku i klasy z EditDataForm na EditRowsForm, komentarze, regiony,
///              tłumaczenia formularza, przerobione funkcje odświeżania danych, usuwanie wierszy,
///              zapis danych po formacie do strumienia (przerobienie strumienia danych na nowy),
///              ukrycie dodatkowego panelu bocznego.
/// [24.12.2016] Usunięcie niepotrzebnych funkcji, nazwy przerobione na nowy standard.
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

namespace CDesigner.Forms
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
		private int _rowPerPage;

		/// <summary>Numer aktualnie wyświetlanej strony liczony od 0.</summary>
		private int _page;

		/// <summary>Ilość wszystkich stron w tabeli.</summary>
		private int _pages;

		/// <summary>Indeks pierwszego wiersza w tabeli z danymi.</summary>
		private int _firstRow;

		/// <summary>Identyfikator usuwanego elementu.</summary>
		private int _deleteID;

		/// <summary>Ilość usuwanych rekordów - funkcje czekają na skompletowanie wszystkich.</summary>
		private int _deletePending;

		/// <summary>Lista identyfikatorów wszystkich przeznaczonych do usunięcia wierszy.</summary>
		private List<int> _deletingRows;

		/// <summary>Lista zaznaczonych kolumn w tabeli.</summary>
		private List<bool> _colsSelected;

		/// <summary>Informacja o tym, czy formularz blokuje akcje kontrolek czy nie.</summary>
		private bool _locked;

		/// <summary>Lista z identyfikatorami kolumn ułożona w kolejności wyświetlania w tabeli.</summary>
		private List<int> _rowsData;

		/// <summary>Lista wierszy ze schowka w których zaszły zmiany - zawiera indeksy do zmian.</summary>
		private List<int> _rowsChanges;

		/// <summary>Lista wszystkich zmian w postaci edycji i dodawania.</summary>
		private List<object[]> _changeLog;

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
			this._rowPerPage = Settings.Info.EDF_RowsNumber;

			this._colsSelected  = new List<bool>();
			this._storage       = null;
			this._rowsData      = null;
			this._rowsChanges   = null;
			this._changeLog     = null;
			this._deletingRows  = new List<int>();
			this._deleteID      = 0;
			this._deletePending = 0;
			this._locked        = false;

			this._page     = 0;
			this._pages    = 0;
			this._firstRow = 0;

			// podwójne buforowanie siatki - wydajność!
			var type = this.DGV_Data.GetType();
			var info = type.GetProperty( "DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic );
			info.SetValue( this.DGV_Data, true, null );

			// ikony przycisków
			this.B_InsertRow.Image = Program.GetBitmap( BITMAPCODE.ITEMADD );
			this.B_RemoveRow.Image = Program.GetBitmap( BITMAPCODE.ITEMREMOVE );
			this.B_FirstPage.Image = Program.GetBitmap( BITMAPCODE.FIRSTPAGE );
			this.P_PrevPage.Image  = Program.GetBitmap( BITMAPCODE.PREVPAGE );
			this.B_NextPage.Image  = Program.GetBitmap( BITMAPCODE.NEXTPAGE );
			this.B_LastPage.Image  = Program.GetBitmap( BITMAPCODE.LASTPAGE );

			// ilość wierszy na stronę i aktualna strona - z pustego i Salomon nie naleje
			this.TB_RowsPerPage.Text = this._rowPerPage.ToString();
			this.L_PageStat.Text     = "";

			this.translateForm();

			// ukryj drugi panel - na razie nie jest potrzebny
			this.SC_Main.Panel2Collapsed = true;
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
				this.DGV_Data.Rows.Clear();
				this.DGV_Data.Columns.Clear();

				GC.Collect();

				// dodaj kolumnę identyfikatora
				this.DGV_Data.Columns.Add( "gvcID", "ID" );

				this.DGV_Data.Columns[0].SortMode = DataGridViewColumnSortMode.NotSortable;
				this.DGV_Data.Columns[0].ReadOnly = true;
				this.DGV_Data.Columns[0].Width    = 40;
				this.DGV_Data.Columns[0].DefaultCellStyle.BackColor = SystemColors.Control;

				// dodaj kolumny z bazy
				for( int x = 0; x < value.ColumnsNumber; ++x )
				{
					this.DGV_Data.Columns.Add( "gvc" + x, value.Column[x] );
					this.DGV_Data.Columns[x+1].SortMode = DataGridViewColumnSortMode.NotSortable;
				}

				// wyczyść listę kolumn
				this.LV_Columns.Items.Clear();
				this._colsSelected.Clear();

				// zablokuj
				this._locked = true;

				// dodaj kolumny z bazy
				for( int x = 0; x < value.ColumnsNumber; ++x )
				{
					this.LV_Columns.Items.Add( value.Column[x] );
					this.LV_Columns.Items[x].Checked = true;

					this._colsSelected.Add( false );
				}

				// przydziel pamięć na dane
				this._rowsData    = new List<int>( this._storage.RowsNumber );
				this._rowsChanges = new List<int>( this._storage.RowsNumber );
				this._changeLog   = new List<object[]>();

				// przypisz realny numer wiersza i informacje o braku edycji
				for( int x = 0; x < this._storage.RowsNumber; ++x )
				{
					this._rowsData.Add( x );
					this._rowsChanges.Add( -1 );
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
			get { return this._rowPerPage; }
			set
			{
				if( value < 0 )
					value = 1;

				this._rowPerPage = value;
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
			this.L_RowsPerPage.Text = values[(int)LANGCODE.I04_LAB_ROWSONPAGE];

			values = Language.GetLines( "EditRows", "Buttons" );
			this.B_Cancel.Text = values[(int)LANGCODE.I04_BUT_CANCEL];
			this.B_Save.Text   = values[(int)LANGCODE.I04_BUT_SAVE];
			
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

			this.DGV_Data.SuspendLayout();

			// wyświetl wszystkie wiersze na jednej stronie
			if( this._rowPerPage == 0 )
			{
				// ilość wszystkich stron
				this._pages = 1;
				this.Page   = 1;
			
				// indeks elementu początkowego i ostatniego
				int startelem = 0;
				int endelem   = this._rowsData.Count;

				// ilość wyświetlanych wierszy
				totalrows = endelem - startelem;
			
				// zapisz indeksy pierwszego i ostatniego elementu
				this._firstRow = startelem;
			}
			// podziel wiersze na strony
			else
			{
				// ilość wszystkich stron
				this._pages = this._rowsData.Count / this._rowPerPage + 1;
			
				// zmień stronę jeżeli indeks wykracza poza granicę
				if( this._page > this._pages - 1 )
					this.Page = this._pages;
			
				// indeks elementu początkowego i ostatniego
				int startelem = this._page * this._rowPerPage;
				int endelem   = (this._page + 1) * this._rowPerPage;

				// poprawka ostatniego elementu (gdy wykracza poza faktyczną ilość elementów w tablicy)
				if( endelem > this._rowsData.Count )
					endelem = this._rowsData.Count;

				// ilość wyświetlanych wierszy
				totalrows = endelem - startelem;
			
				// zapisz indeksy pierwszego i ostatniego elementu
				this._firstRow = startelem;
			}

			// dodaj pusty wiersz gdy brak wierszy (raczej nie powinno się zdarzyć)
			if( this.DGV_Data.Rows.Count == 0 )
				this.DGV_Data.Rows.Add();
			
			// ilość wierszy...
			int cond = this.DGV_Data.AllowUserToAddRows
				? this.DGV_Data.Rows.Count - 1
				: this.DGV_Data.Rows.Count;

			// usuń wiersze
			if( cond > totalrows )
			{
				// szybciej jest wyczyścić wszystko i dodać wiersze niż usunąć...
				this.DGV_Data.Rows.Clear();

				if( totalrows > 0 )
					this.DGV_Data.Rows.Add( totalrows );
			}
			// dodaj wiersze
			else if( cond < totalrows )
				this.DGV_Data.Rows.Add( totalrows - (this.DGV_Data.Rows.Count - 1) );
			
			// zablokuj możliwość dodawania wierszy do środka tabeli
			if( totalrows == this._rowPerPage )
				this.DGV_Data.AllowUserToAddRows = false;
			else
				this.DGV_Data.AllowUserToAddRows = true;

			this.DGV_Data.ResumeLayout( false );

			// wyświetl ilość stron i aktualną stronę
			this.L_PageStat.Text = String.Format
			(
				Language.GetLine("EditRows", "Labels", (int)LANGCODE.I04_LAB_PAGEOFNUM),
				this._pages
			);
			this.TB_PageNum.Text = (this._page + 1).ToString();

			// odśwież kontrolkę
			this.DGV_Data.Refresh();
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
			for( int x = index; x < this._rowsData.Count - 1; ++x )
				this._rowsData[x] = this._rowsData[x+1];
			this._rowsData.RemoveAt( this._rowsData.Count - 1 );
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
		private void DGV_Data_CellValueNeeded( object sender, DataGridViewCellValueEventArgs ev )
		{
			var index = ev.RowIndex + this._firstRow;

			// kolumna indeksu
			if( ev.ColumnIndex == 0 )
				ev.Value = index + 1;
			// wiersz spoza zakresu...
			else if( index >= this._rowsData.Count )
				ev.Value = "";
			// pobierz wartość z tablicy
			else
			{
				// nowy rekord
				if( this._rowsData[index] < 0 )
				{
					int changeidx = -this._rowsData[index] - 1;
					ev.Value = this._changeLog[changeidx][ev.ColumnIndex-1];
				}
				else
				{
					int rowidx    = this._rowsData[index];
					int changeidx = this._rowsChanges[rowidx]; 

					// zmieniony rekord
					if( changeidx != -1 && this._changeLog[changeidx][ev.ColumnIndex-1] != null )
						ev.Value = this._changeLog[changeidx][ev.ColumnIndex-1].ToString();
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
		private void DGV_Data_CellValuePushed( object sender, DataGridViewCellValueEventArgs ev )
		{
			if( this._locked )
				return;

			bool need_refresh = false;
			int  row          = ev.RowIndex + this._firstRow;
			int  changeidx    = this._changeLog.Count;

			// sprawdź czy indeks nie wykracza poza zakres
			if( row >= this._rowsData.Count )
			{
				// jeżeli wykracza za dużo, nic nie rób...
				if( row >= this._rowsData.Count + 1 )
					return;

				// dodaj nowe rekordy
				this._rowsData.Add( -changeidx - 1 );
				need_refresh = true;
			}
			
			// oblicz indeksy
			int colidx = ev.ColumnIndex - 1;
			int rowidx = this._rowsData[row] < 0
				? -this._rowsData[row] - 1
				: this._rowsChanges[this._rowsData[row]];

			// przydziel miejsce jeżeli brak
			if( need_refresh || rowidx == -1 )
			{
				var values = new object[this._storage.ColumnsNumber];
				this._changeLog.Add( values );

				// i uzupełnij je pustymi wartościami - w przypadku starych pól są to nule
				for( int x = 0; x < this._storage.ColumnsNumber; ++x )
					this._changeLog[changeidx][x] = need_refresh ? "" : null;

				// istniejące rekordy
				if( this._rowsData[row] >= 0 )
					this._rowsChanges[this._rowsData[row]] = changeidx;

				rowidx = changeidx;
			}

			// uzupełnij pustą lub zmień wartość wybranej komórki
			if( ev.Value != null )
				this._changeLog[rowidx][colidx] = (object)ev.Value.ToString();

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
		private void DGV_Data_UserDeletingRow( object sender, DataGridViewRowCancelEventArgs ev )
		{
			if( this._locked )
				return;

			// oczekiwanie na usuwanie
			if( this._deletePending > 0 )
				return;

			// jeżeli zaznaczonych jest więcej niż jeden rekord, ustaw oczekiwanie na usunięcie
			if( this.DGV_Data.SelectedRows.Count > 1 )
			{
				// pobierz indeksy usuwanych wierszy
				for( int x = 0; x < this.DGV_Data.SelectedRows.Count; ++x )
				{
					// pomiń nowy wiersz
					if( this.DGV_Data.AllowUserToAddRows &&
						this.DGV_Data.SelectedRows[x].Index == this.DGV_Data.Rows.Count - 1 )
						continue;

					this._deletingRows.Add( this.DGV_Data.SelectedRows[x].Index );
				}

				// oczekiwanie jest równe ilości wierszy do usunięcia
				this._deletePending = this._deletingRows.Count;
				return;
			}

			// nowo dodany element podczas usuwania sygnalizuje wartość o 1 większą
			// dzieje się tak ponieważ usuwany jest wiersz po nim, a nie obecny...
			if( ev.Row.Index + this._firstRow >= this._rowsData.Count )
			{
				// teraz to już przesadziło...
				if( ev.Row.Index + this._firstRow - 1 >= this._rowsData.Count )
				{
					this._deleteID = -1;
					return;
				}
				this._deleteID = ev.Row.Index + this._firstRow - 1;
			}
			else
				this._deleteID = ev.Row.Index + this._firstRow;
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
		private void DGV_Data_UserDeletedRow( object sender, DataGridViewRowEventArgs ev )
		{
			if( this._locked )
				return;

			// oczekiwanie włączone...
			if( this._deletePending > 0 )
			{
				// ostatni "tik" oczekiwania, usuń wszystkie zaznaczone wiersze
				if( this._deletePending == 1 )
				{
					// uporządkuj malejąco (usuwanie od największego)
					this._deletingRows.OrderByDescending( w => w );

					for( int x = 0; x < this._deletingRows.Count; ++x )
						this._removeRow( this._deletingRows[x] );

					// wyczyść tablicę
					this._deletingRows.Clear();

					// odśwież widok
					this.refreshDataRange();
				}

				// nie pozwól na wykonanie kodu poniżej podczas oczekiwania
				this._deletePending--;
				return;
			}

			// usuń wiersz
			if( this._deleteID != -1 )
				this._removeRow( this._deleteID );

			// odśwież widok
			this.refreshDataRange();
		}

#endregion

#region OBSŁUGA KONTROLI TABELI DANYCH

		/// <summary>
		/// Akcja wywoływana podczas kliknięcia w przycisk przejścia do ostatniej strony.
		/// Gdy tabela nie ma więcej stron lub po prostu aktualną stroną jest ostatnia strona, to funkcja nic nie robi.
		/// </summary>
		/// 
		/// <param name="sender">Obiekt wywołujący zdarzenie.</param>
		/// <param name="ev">Argumenty zdarzenia</param>
		//* ============================================================================================================
		private void B_LastPage_Click( object sender, EventArgs ev )
		{
			if( this._locked || this._page == this._pages -1 )
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
		private void B_FirstPage_Click( object sender, EventArgs ev )
		{
			if( this._locked || this._page == 0 )
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
		private void B_PrevPage_Click( object sender, EventArgs ev )
		{
			if( this._locked || this._page == 0 )
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
		private void B_NextPage_Click( object sender, EventArgs ev )
		{
			if( this._locked || this._page == this._pages - 1 )
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
		private void TB_PageNum_TextChanged( object sender, EventArgs ev )
		{
			int value = 0;
			
			// jeżeli tekst jest pusty, zostaw go w spokoju...
			if( this.TB_PageNum.Text == "" )
				return;

			// spróbuj zamienić podany tekst na liczbę
			if( !Int32.TryParse(this.TB_PageNum.Text, out value) )
				// jeżeli nie da rady, wpisz aktualną stronę
				this.TB_PageNum.Text = this._page.ToString();
			else
			{
				// jeżeli podana wartość jest większa niż liczba wszystkich stron
				if( value >= this._pages )
					this.TB_PageNum.Text = this._pages.ToString();
				// jeżeli wartość jest mniejsza niż 1
				else if( value < 1 )
					this.TB_PageNum.Text = "1";
			}

			string text = this.TB_PageNum.Text;
			int    zcnt = 0;

			// sprawdź czy tekst zawiera zera wiodące...
			for( int x = 0; x < text.Length; ++x )
				if( text[x] != '0' )
					break;
				else
					zcnt++;

			// usuń zera wiodące jeżeli istnieją
			if( zcnt > 0 )
				this.TB_PageNum.Text = text.Remove( 0, zcnt );
		}
		
		/// <summary>
		/// Akcja wywoływana po naciśnięciu przycisku na kontrolce z numerem strony.
		/// Przepuszcza tylko klawisze kontrolne i cyfry.
		/// </summary>
		/// 
		/// <param name="sender">Obiekt wywołujący zdarzenie.</param>
		/// <param name="ev">Argumenty zdarzenia</param>
		//* ============================================================================================================
		private void TB_PageNum_KeyPress( object sender, KeyPressEventArgs ev )
		{
			// przepuszczaj klawisze kontrolne
			if( char.IsControl(ev.KeyChar) )
				return;

			// przepuszczaj tylko cyfry
			if( ev.KeyChar < '0' || ev.KeyChar > '9' )
				ev.Handled = true;
			else
				this.TB_PageNum_TextChanged( sender, null );
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
		private void TB_PageNum_KeyDown( object sender, KeyEventArgs ev )
		{
			// po wciśnięciu klawisza ENTER zmień stronę
			if( ev.KeyCode == Keys.Enter )
			{
				int value = 0;

				// sprawdź czy wartośc można zamienić na typ INT
				if( !Int32.TryParse(this.TB_PageNum.Text, out value) )
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
		private void TB_RowsPerPage_TextChanged( object sender, EventArgs ev )
		{
			int value = 0;
			
			// jeżeli tekst jest pusty, zostaw go w spokoju...
			if( this.TB_RowsPerPage.Text == "" )
				return;

			// spróbuj zamienić podany tekst na liczbę
			if( !Int32.TryParse(this.TB_RowsPerPage.Text, out value) )
				// jeżeli nie da rady, wpisz aktualną ilość wierszy na stronę
				this.TB_RowsPerPage.Text = this._rowPerPage.ToString();
			// jeżeli podana wartość jest mniejsza od 5 i jest różna od 0
			else if( value < 0 )
				this.TB_RowsPerPage.Text = "1";

			string text = this.TB_RowsPerPage.Text;
			int    zcnt = 0;

			// sprawdź czy tekst zawiera zera wiodące...
			for( int x = 0; x < text.Length - 1; ++x )
				if( text[x] != '0' )
					break;
				else
					zcnt++;

			// usuń zera wiodące jeżeli istnieją
			if( zcnt > 0 )
				this.TB_RowsPerPage.Text = text.Remove( 0, zcnt );
		}
		
		/// <summary>
		/// Akcja wywoływana po naciśnięciu przycisku na kontrolce zawierającą ilość wierszy na stronę.
		/// Przepuszcza tylko klawisze kontrolne i cyfry.
		/// </summary>
		/// 
		/// <param name="sender">Obiekt wywołujący zdarzenie.</param>
		/// <param name="ev">Argumenty zdarzenia</param>
		//* ============================================================================================================
		private void TB_RowsPerPage_KeyPress( object sender, KeyPressEventArgs ev )
		{
			// przepuszczaj klawisze kontrolne
			if( char.IsControl(ev.KeyChar) )
				return;

			// przepuszczaj tylko cyfry
			if( ev.KeyChar < '0' || ev.KeyChar > '9' )
				ev.Handled = true;
			else
				this.TB_RowsPerPage_TextChanged( sender, null );
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
		private void TB_RowsPerPage_KeyDown( object sender, KeyEventArgs ev )
		{
			// po wciśnięciu klawisza ENTER zmień ilość wierszy na stronę
			if( ev.KeyCode == Keys.Enter )
			{
				int value = 0;

				// sprawdź czy wartośc można zamienić na typ INT
				if( !Int32.TryParse(this.TB_RowsPerPage.Text, out value) )
					return;

				// zmień ilość wierszy na stronę
				if( value != this._rowPerPage )
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
		private void B_InsertRow_Click( object sender, EventArgs ev )
		{
			if( this._locked )
				return;

			// przejdź do ostatniej strony jeżeli ta nie jest ostatnią
			if( this._page != this._pages - 1 )
			{
				this.Page = this._pages;
				this.refreshDataRange();
			}

			// pobierz komórkę w pierwszej kolumnie i ostatnim wierszu
			int              last = this.DGV_Data.Rows.Count - 1;
			DataGridViewCell cell = null;

			// sprawdź czy komórka jest widoczna
			for( int x = 1; x <= this._storage.ColumnsNumber; ++x )
				if( this.DGV_Data.Rows[last].Cells[x].Visible == true )
				{
					cell = this.DGV_Data.Rows[last].Cells[x];
					break;
				}

			// brak widocznych komórek?
			if( cell == null )
				return;

			// ustaw komórkę do edycji
			this.DGV_Data.CurrentCell = cell;
			this.DGV_Data.BeginEdit( true );
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
		private void B_RemoveRow_Click( object sender, EventArgs ev )
		{
			if( this._locked )
				return;

			// pobierz indeksy usuwanych wierszy
			for( int x = 0; x < this.DGV_Data.SelectedRows.Count; ++x )
			{
				// pomiń nowy wiersz
				if( this.DGV_Data.AllowUserToAddRows && this.DGV_Data.SelectedRows[x].Index == this.DGV_Data.Rows.Count - 1 )
					continue;

				this._deletingRows.Add( this.DGV_Data.SelectedRows[x].Index );
			}

			// uporządkuj malejąco (usuwanie od największego) - aby uniknąć problemów z indeksami
			this._deletingRows.OrderByDescending( w => w );

			// usuń z tablicy i z listy
			for( int x = 0; x < this._deletingRows.Count; ++x )
			{
				this.DGV_Data.Rows.RemoveAt( this._deletingRows[x] );
				this._removeRow( this._deletingRows[x] );
			}

			// wyczyść usuwane wiersze
			this._deletingRows.Clear();

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
		private void DGV_Data_SelectionChanged( object sender, EventArgs ev )
		{
			// wyzeruj wszystkie kolumny
			for( int x = 0; x < this._colsSelected.Count; ++x )
				this._colsSelected[x] = false;

			// sprawdź które można zaznaczyć
			foreach( DataGridViewCell cell in this.DGV_Data.SelectedCells )
			{
				if( cell.ColumnIndex == 0 )
					continue;
				this._colsSelected[cell.ColumnIndex-1] = true;
			}

			// zablokuj
			this._locked = true;

			// wyczyść zaznaczenia
			this.LV_Columns.SelectedIndices.Clear();

			// zaznacz
			for( int x = 0; x < this._colsSelected.Count; ++x )
				if( this._colsSelected[x] )
					this.LV_Columns.SelectedIndices.Add( x );

			// odblokuj
			this._locked = false;

			// sprawdź czy to nie jest czasem nowy wiersz...
			if( this.DGV_Data.SelectedRows.Count == 1 && this.DGV_Data.AllowUserToAddRows &&
				this.DGV_Data.SelectedRows[0].Index == this.DGV_Data.Rows.Count - 1 )
			{
				this.B_RemoveRow.Enabled = false;
				return;
			}

			// aktywuj lub deaktywuj przycisk
			if( this.DGV_Data.SelectedRows.Count > 0 )
				this.B_RemoveRow.Enabled = true;
			else
				this.B_RemoveRow.Enabled = false;
		}

#endregion

#region PASEK AKCJI
		
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
				this.SC_Main.Panel2Collapsed = !this.SC_Main.Panel2Collapsed;
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
		private void TLP_StatusBar_Paint( object sender, PaintEventArgs ev )
		{
			ev.Graphics.DrawLine
			(
				new Pen( SystemColors.ControlDark ),
				this.TLP_StatusBar.Bounds.X,
				0,
				this.TLP_StatusBar.Bounds.Right,
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
		/// <seealso cref="B_Save_Click" />
		//* ============================================================================================================
		private void B_Cancel_Click( object sender, EventArgs ev )
		{
			if( this._locked )
				return;

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
		/// <seealso cref="B_Cancel_Click" />
		//* ============================================================================================================
		private void B_Save_Click( object sender, EventArgs ev )
		{
			if( this._locked )
				return;

			int dataidx = 0;
			int rowidx  = 0;

			// tryb edycji danych
			this._storage.editMode( null );

			while( this._storage.nextRow() )
			{
				// koniec zmian, teraz nowe wiersze, usuń pozostałości
				if( this._rowsData[dataidx] < 0 || this._rowsData[dataidx] != rowidx )
					this._storage.removeCurrentRow();
				else if( this._rowsData[dataidx] == rowidx )
				{
					// brak zmian
					if( this._rowsChanges[rowidx] == -1 )
					{
						rowidx++;
						dataidx++;
						continue;
					}

					string[] values = this._storage.getCurrentRow();
					int      logidx = this._rowsChanges[rowidx];

					// przypisz nowe wartości
					for( int x = 0; x < this._storage.ColumnsNumber; ++x )
						if( this._changeLog[logidx][x] != null )
							values[x] = this._changeLog[logidx][x].ToString();

					// zamień
					this._storage.replaceCurrentRow( values );

					dataidx++;
				}
				rowidx++;
			}

			// nowe rekordy
			for( int x = dataidx; x < this._rowsData.Count; ++x )
			{
				if( this._rowsData[x] >= 0 )
					continue;

				string[] values = new string[this._storage.ColumnsNumber];

				rowidx = -this._rowsData[x] - 1;

				for( int y = 0; y < this._storage.ColumnsNumber; ++y )
					values[y] = this._changeLog[rowidx][y].ToString();

				this._storage.addNewRowToEnd( values );
			}

			// zatwierdź zmiany i sprawdź poprawność danych
			this._storage.checkIntegrity();

			this.DialogResult = DialogResult.OK;
			this.Close();
		}

		/// @endcond
#endregion
	}
}
