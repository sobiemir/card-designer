///
/// $i[xx] DataStorage.cs
/// 
/// Schowek dla wczytanych danych z pliku.
/// Pozwala na łatwy dostęp do danych zarówno z pliku jak i z bazy danych.
/// Dostęp z bazy danych planowany jest do wdrożenia.
/// Dzięki temu i pliki i baza danych posiadają jeden interfejs pozwalający na wykonywanie operacji na danych.
/// 
/// Autor: Kamil Biały
/// Od wersji: 0.8.x.x
/// Ostatnia zmiana: 2016-11-08
/// 
/// CHANGELOG:
/// [16.08.2016] Pierwsza wersja schowka dla danych w pamięci - ma on zastąpić aktualny DatafileStream.
/// [21.08.2016] Tryb edycji danych - poruszanie się po wierszach.
/// [27.08.2016] Poprawka trybu edycji - nowa zmienna dla aktualnie pobieranego wiersza.
///              Występował konflikt podczas pobierania wiersza który był usuwany ze schowka.
/// [08.11.2016] Tworzenie pustej tablicy - dla możliwości tworzenia nowego pliku, regiony.
///

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CDesigner.Utils
{
	/// 
	/// <summary>
	/// Klasa umożliwiająca odczyt lub zapis danych do pliku.
    /// Pozwala na wczytanie całego lub wybranej części pliku, co znacznie przyspiesza mniejsze operacje
    /// które nie muszą być widoczne dla wszystkich danych.
    /// W tym momencie klasa może wczytać i zapisać tylko pliki w formacie <em>csv</em>.
    /// Szczegóły dotyczące wczytywania konkretnych typów plików znajdują się w funkcjach parsujących te formaty.
	/// </summary>
	///
    /// @todo <dfn><small>[0.9.x.x]</small></dfn> Uzupełnianie pustych rekordów nowymi danymi - tworzenie tabeli.
	/// @todo <dfn><small>[1.0.x.x]</small></dfn> Lazy loading, czyli przetwarzanie krokowe dużych ilości danych.
	/// 
    public class DataStorage
    {
#region ZMIENNE

        /// <summary>Lista kolumn pobranych z pliku.</summary>
        protected string[] _columns;

		/// <summary>Tablica krotek pobranych z bazy danych.</summary>
        protected List<string[]> _rows;

		/// <summary>Liczba wszystkich kolumn w bazie danych.</summary>
        protected int _columnsNumber;

		/// <summary>Ilość wszystkich wierszy w pliku.</summary>
        protected int _rowsNumber;

		/// <summary>Czy klasa jest gotowa do dalszych obróbkach na danych?</summary>
        protected bool _isReady;

        /// <summary>Przepełnienie (gdy pobierane jest tylko kilka rekordów).</summary>
		protected bool _overflow;
        
        /// <summary>Ilość nowych kolumn zapisywanych do bazy (tylko w trybie edycji).</summary>
        private int _saveColumns;

        /// <summary>Aktualny indeks edytowanego wiersza do podmiany (tylko w trybie edycji).</summary>
        private int _currentRow;
        
        /// <summary>Aktualny indeks edytowanego wiersza do pobrania (tylko w trybie edycji).</summary>
        private int _currentRowGet;

#endregion

#region KONSTRUKTOR / WŁAŚCIWOŚCI

        /// <summary>
        /// Konstruktor klasy.
        /// Uzupełnia dane domyślnymi wartościami.
        /// </summary>
		//* ============================================================================================================
        public DataStorage()
        {
            this._columns       = null;
            this._rows          = null;
            this._columnsNumber = 0;
            this._rowsNumber    = 0;
            this._isReady       = false;
            this._overflow      = false;
            this._saveColumns   = -1;
            this._currentRow    = -1;
            this._currentRowGet = -1;
        }
        
		/// <summary>
		/// Gotowość klasy do dalszych operacji.
		/// W przypadku jakiegokolwiek błędu wartość ustawiana jest na false.
        /// Możliwe jest tylko pobranie wartości.
        /// </summary>
		//* ============================================================================================================
        public bool Ready
        {
            get { return this._isReady; }
        }
        
        /// <summary>
		/// Nadmiarowość danych w bazie danych.
		/// Ustawiana na true gdy przy obcinaniu rekordów z pliku ilość wierszy w kolumnie jest większa od limitu
		/// pobieranych danych ustawionym w funkcji parsującej plik.
		/// Właściwość tylko do odczytu.
		/// </summary>
		//* ============================================================================================================
		public bool Overflow
        {
            get { return this._overflow; }
        }

        /// <summary>
		/// Lista dostępnych komórek w pliku bazy danych.
		/// Poprzez komórki rozumie się wszystkie przecięcia wierszy z kolumnami.
		/// Wałaściwość tylko do odczytu.
		/// </summary>
		//* ============================================================================================================
		public List<string[]> Row
        {
            get { return this._rows; }
        }

        /// <summary>
		/// Lista z nazwami dostępnych kolumn w pliku bazy danych.
        /// W przypadku gdy baza danych nie zawiera informacji o kolumnach są one uzupełniane domyślnymi wartościami
        /// zależnymi od wersji językowej programu.
		/// Właściwość tylko do odczytu.
		/// </summary>
		//* ============================================================================================================
		public string[] Column
        {
            get { return this._columns; }
        }

        /// <summary>
		/// Liczba dostępnych kolumn w pliku bazy danych.
		/// Właściwość tylko do odczytu.
		/// </summary>
		//* ============================================================================================================
		public int ColumnsNumber
        {
            get { return this._columnsNumber; }
        }

        /// <summary>
		/// Liczba dostępnych wierszy w pliku bazy danych.
		/// Aby obliczyć ilość komórek należy pomnożyć ilość kolumn przez ilość wierszy.
		/// Właściwość tylko do odczytu.
		/// </summary>
		//* ============================================================================================================
		public int RowsNumber
        {
            get { return this._rowsNumber; }
        }

#endregion

#region MODYFIKACJE DANYCH

        /// <summary>
        /// Zamienia wartości wiersza o podanym identyfikatorze.
        /// Funkcja sprawdza podczas zamiany czy ilość kolumn jest zgodna z danymi nagłówkowymi.
        /// </summary>
        /// 
        /// <param name="row">Identyfikator wiersza do zamiany.</param>
        /// <param name="data">Podmieniane wartości nowego wiersza.</param>
		//* ============================================================================================================
        public void replaceRow( int row, string[] data )
        {
            if( row < 0 || row >= this._rows.Count || data == null || data.Count() != this._columnsNumber )
                throw new Exception( "Naruszona struktura kolumn w wierszu lub indeks poza zasięgiem." );

            this._rows[row] = data;
        }

        /// <summary>
        /// Usuwa wiersz o podanym identyfikatorze.
        /// Funkcja wywoływana dla wielu wierszy jest dużo wolniejesza niż usuwanie rekordów podczas edycji.
        /// </summary>
        /// 
        /// <param name="row">Identyfikator wiersza do usunięcia.</param>
		//* ============================================================================================================
        public void removeRow( int row )
        {
            if( row < 0 || row >= this._rows.Count )
                throw new Exception( "Indeks wykracza poza granice obiektu." );

            this._rows.RemoveAt( row );
            this._rowsNumber--;
        }
        
        /// <summary>
        /// Uruchamia tryb edycji danych.
        /// Funkcja pozwala na zamianę starych kolumn na nowe.
        /// Kursor ustawiany jest na wartość -1, aby przemieszczać się pomiędzy wierszami należy użyć odpowiednich
        /// funkcji operujących na kursorze.
        /// Aby zakończyć edycję należy wywołać funkcję sprawdzającą poprawność zapisanych danych.
        /// Kolumny nie są zamieniane gdy w parametrze <var>columns</var> podana zostanie wartość NULL.
        /// </summary>
        /// 
        /// <seealso cref="nextRow"/>
        /// <seealso cref="getCurrentRow"/>
        /// <seealso cref="replaceCurrentRow"/>
        /// <seealso cref="removeCurrentRow"/>
        /// <seealso cref="checkIntegrity"/>
        /// 
        /// <param name="data">Tablica zawierająca nowe kolumny lub NULL.</param>
		//* ============================================================================================================
        public void editMode( string[] columns )
        {
            if( columns == null )
                this._saveColumns = this._columnsNumber;
            else
                this._saveColumns = columns.Count();

            // tryb edycji
            this._currentRow    = -1;
            this._currentRowGet = -1;
            this._isReady       = false;

            if( columns == null )
                return;

            // zapisz nowe kolumny
            this._columns = columns;
        }
        
        /// <summary>
        /// Przenosi kursor do następnego wiersza podczas edycji.
        /// Funkcja może być wywołana tylko podczas edycji wierszy dla wybranego źródła danych.
        /// </summary>
        /// 
        /// <seealso cref="getCurrentRow"/>
        /// <seealso cref="replaceCurrentRow"/>
        /// <seealso cref="removeCurrentRow"/>
        /// 
        /// <returns>Zwraca true gdy kolejny rekord istnieje, w przeciwnym wypadku false.</returns>
		//* ============================================================================================================
        public bool nextRow()
        {
            this._currentRow++;
            this._currentRowGet++;

            if( this._currentRow < this._rowsNumber && this._currentRow > -1 )
                return true;

            return false;
        }
        
        /// <summary>
        /// Pobiera wiersz na który wskazuje kursor edycji.
        /// Funkcję można wywołać tylko podczas edycji rekordów.
        /// Zwracana wartość może być pustą tablicą gdy kursor nie będzie wskazywał na żaden rekord.
        /// Ilość kolumn jest zawsze taka sama w każdym wierszu dla tego samego źródła danych.
        /// </summary>
        /// 
        /// <seealso cref="ColumnsNumber"/>
        /// <seealso cref="editMode"/>
        /// <seealso cref="checkIntegrity"/>
        /// <seealso cref="nextRow"/>
        /// 
        /// <returns>Wiersz wskazywany przez kursor lub pusta tablica.</returns>
		//* ============================================================================================================
        public string[] getCurrentRow()
        {
            if( this._currentRow < 0 )
                return new string[this._saveColumns > 0 ? this._saveColumns : this._columnsNumber];

            return this._rows[this._currentRowGet];
        }

        /// <summary>
        /// Zamień wiersz na którym ustawiony jest kursor edycji.
        /// Funkcję można wywołać tylko podczas edycji rekordów.
        /// Poprawność danych sprawdzana jest dopiero po zakończeniu edycji rekordów.
        /// </summary>
        /// 
        /// <seealso cref="editMode"/>
        /// <seealso cref="checkIntegrity"/>
        /// <seealso cref="nextRow"/>
        /// 
        /// <param name="data">Nowy wiersz zamieniany z aktualnym wierszem.</param>
		//* ============================================================================================================
        public void replaceCurrentRow( string[] data )
        {
            if( this._currentRow < 0 )
                return;

            this._rows[this._currentRow] = data;
        }

        /// <summary>
        /// Usuwa wiersz na którą ustawiony jest kursor edycji.
        /// Funkcję można wywołać tylko podczas edycji rekordów.
        /// Usuwanie nieużywanych kolumn odbywa się dopiero po zakończeniu edycji z racji szybszego wykonania.
        /// </summary>
        /// 
        /// <seealso cref="editMode"/>
        /// <seealso cref="checkIntegrity"/>
        /// <seealso cref="nextRow"/>
		//* ============================================================================================================
        public void removeCurrentRow()
        {
            if( this._currentRow < 0 )
                return;

            this._currentRow--;
            this._rowsNumber--;
        }

        /// <summary>
        /// Dodaje nowy wiersz na sam koniec listy.
        /// Funkcję można wywołać zarówno z poziomu edycji rekordów jak i bez włączonego trybu edycji rekordów.
        /// Zwiększa licznik wszystkich wierszy i aktualny indeks kolumny w trybie edycji.
        /// W trybie edycji można jej użyć tylko na końcu modyfikacji danych.
        /// </summary>
        /// 
        /// <param name="values">Tablica z wartościami dla wiersza przeznaczona do dodania.</param>
		//* ============================================================================================================
        public void addNewRowToEnd( string[] values )
        {
            // zamień wiersz gdy istnieje, ale ma być usuwany
            if( this._rows.Count > this._rowsNumber )
                this._rows[this._rowsNumber - 1] = values;
            // lub dodaj nową pozycję w liście
            else
                this._rows.Add( values );

            this._rowsNumber++;

            // zwiększ aktualny indeks wiersza
            if( this._currentRow != -1 )
                this._currentRow++;
        }

#endregion

#region FUNKCJE DODATKOWE

        /// <summary>
        /// Tworzy pustą tablicę.
        /// Przydatne podczas tworzenia nowych plików bazodanowych.
        /// </summary>
		//* ============================================================================================================
        public void createEmpty()
        {
            this._columns       = new string[0];
            this._rows          = new List<string[]>();
            this._columnsNumber = 0;
            this._rowsNumber    = 0;
            this._isReady       = true;
            this._overflow      = false;
            this._saveColumns   = -1;
            this._currentRow    = -1;
            this._currentRowGet = -1;
        }
        
        /// <summary>
        /// Sprawdza poprawność danych i kończy włączony wcześniej tryb edycji.
        /// W przypadku niespójności danych funkcja rzuca wyjątkiem.
        /// Poprzez termin niespójności danych uważa się na razie tylko złą ilość kolumn w wierszu.
        /// Gdy dane będą spójne, funkcja ustawia flagę gotowości klasy do dalszych działań.
        /// </summary>
        /// 
        /// <seealso cref="Ready"/>
        /// <seealso cref="editMode"/>
		//* ============================================================================================================
        public void checkIntegrity()
        {
            if( this._saveColumns == 0 )
                return;

            this._isReady = false;

            // usuń zbędne dane
            if( this._rows.Count > this._currentRow )
                this._rows.RemoveRange( this._currentRow, this._rows.Count - this._currentRow );

            // sprawdź czy kolumny się zgadzają
            if( this._columns.Count() != this._saveColumns )
                throw new Exception( "Niepoprawna ilość kolumn." );

            // teraz sprawdzaj kolumny po wierszach
            for( int x = 0; x < this._rows.Count; ++x )
                if( this._rows[x] == null || this._rows[x].Count() != this._saveColumns )
                    throw new Exception( "Niepoprawna ilość kolumn w wierszu #" + x + "." );

            // zmień na nową ilość kolumn
            this._columnsNumber = this._saveColumns;

            this._saveColumns   = -1;
            this._currentRow    = -1;
            this._currentRowGet = -1;
            this._isReady       = true;
        }

#endregion
    }
}
