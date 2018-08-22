///
/// $u07 DataFilter.cs
/// 
/// Plik zawierający klasę filtrowania kolumn.
/// Pozwala na połączenie kilku kolumn w jedną oraz wykluczenie istniejących.
/// W przypadku gdy nazwa nowej kolumny będzie taka sama jak nazwa starej, zostanie ona zastąpiona nową.
/// Umożliwia stosowanie filtrów dla każdej kolumny zosobna.
/// 
/// Autor: Kamil Biały
/// Od wersji: 0.7.x.x
/// Ostatnia zmiana: 2016-12-24
/// 
/// CHANGELOG:
/// [05.08.2015] Wersja początkowa.
/// [02.02.2016] Zmiana nazwy pliku i klasy z DataFilter na FilterCreator, stosowanie filtrów,
///              dodawanie, usuwanie, wszystkie brakujące elementy (wersja początkowa była wersja testową).
/// [15.07.2016] Regiony, komentarze.
/// [11.08.2016] Zamiana nazwy pliku i klasy spowrotem z FilterCreator na DataFilter, przełączenie na nowy standard.
/// [14.08.2016] Wyszukiwanie konkretnego filtra i indeksu filtra, utworzenie oddzielnej funkcji do filtrowania danych.
///              Filtowanie, zamiana, wykluczanie wartości (operacje na typach tekstowych).
/// [27.08.2016] Pernamentny zapis filtrów do klasy DataStorage.
/// [28.08.2016] Naprawiony problem z pobieraniem filtrów z kolumn podrzędnych.
/// [24.12.2016] Nowa konwencja nazewnictwa funkcji.
///

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CDesigner.Utils;

namespace CDesigner.Utils
{
	/// 
	/// <summary>
	/// Filtrowanie i tworzenie nowych kolumn w bazie danych.
	/// Klasa pozwala na połączenie istniejących kolumn w jedną oraz utworzenie filtrów
	/// dla każdej dostępnej kolumny w bazie danych oraz dla każdej nowo utworzonej.
	/// Przechowuje informacje o typach poszczególnych kolumn.
	/// </summary>
	/// 
	/// @todo Zastępowanie kolumn.
	/// @todo Przyspieszenie filtrowania, zapis do pamięci przefiltrowanych danych. [1.0.x.x]
	/// @todo Usuwanie starych kolumn, blokada w formularzu filtrowania. [0.9.x.x]
	/// @todo Filtry na innych typach. [0.9.x.x]
	/// @todo Może jakieś zabezpieczenia przeciw index overflow? [1.0.x.x]
	/// 
	public class DataFilter
	{
#region ZMIENNE
		/// <summary>Strumień danych do czytania.</summary>
		private DataStorage _storage;

		/// <summary>Lista filtrów dla poszczególnych kolumn.</summary>
		private List<List<FilterInfo>> _filters = null;

		/// <summary>Identyfikatory łączonych kolumn.</summary>
		private List<List<int>> _joinData = null;

		/// <summary>Lista nazw nowych kolumn.</summary>
		private List<string> _newColumns = null;

		/// <summary>Lista informacji o typach poszczególnych kolumn.</summary>
		private List<ColumnTypeInfo> _columnInfo = null;

		/// <summary>Informacje o dostępności kolumn w dalszych procesach przetwarzania danych.</summary>
		private List<bool> _columnActive = null;
#endregion

#region KONSTRUKTORY / WŁAŚCIWOŚCI
		/// <summary>
		/// Zerowy konstruktor klasy.
		/// Tworzy podstawowe obiekty dla późniejszego wykorzystania.
		/// </summary>
		//* ============================================================================================================
		public DataFilter()
		{
			this._joinData     = new List<List<int>>();
			this._filters      = new List<List<FilterInfo>>();
			this._newColumns   = new List<string>();
			this._columnInfo   = new List<ColumnTypeInfo>();
			this._columnActive = new List<bool>();

			this._storage = null;
		}

		/// <summary>
		/// Zmiana lub pobranie strumienia danych.
		/// Podczas zmiany strumienia danych filtry i kolumny ustawione dla poprzedniego strumienia są usuwane.
		/// </summary>
		//* ============================================================================================================
		public DataStorage Storage
		{
			get { return this._storage; }
			set
			{
				this._storage = value;

				this._joinData.Clear();
				this._filters.Clear();
				this._newColumns.Clear();
				this._columnInfo.Clear();
				
				// przypisz filtry dla nowych kolumn strumienia
				for( int x = 0; x < this._storage.ColumnsNumber; ++x )
				{
					this._filters.Add( new List<FilterInfo>() );
					this._columnInfo.Add( new ColumnTypeInfo() );
					this._columnActive.Add( true );

					// typ danych
					this._columnInfo[x].Type = DATATYPE.String;
					//this._columnInfo[x].Type = this._storage.DataType[x];
				}
			}
		}

		/// <summary>
		/// Informacja o typie wybranej kolumny.
		/// Możliwy jest tylko odczyt właściwości.
		/// </summary>
		//* ============================================================================================================
		public List<ColumnTypeInfo> ColumnType
		{
			get { return this._columnInfo; }
		}

		/// <summary>
		/// Lista z nazwami nowo utworzonych kolumn.
		/// Możliwy jest tylko odczyt właściwości.
		/// </summary>
		//* ============================================================================================================
		public List<string> NewColumns
		{
			get { return this._newColumns; }
		}

		/// <summary>
		/// Lista z identyfikatorami łączonych kolumn.
		/// Możliwy jest tylko odczyt właściwości.
		/// </summary>
		//* ============================================================================================================
		public List<List<int>> SubColumns
		{
			get { return this._joinData; }
		}
		
		/// <summary>
		/// Lista z filtrami dla danej kolumny.
		/// Kolumny zawierające kolumny podrzędne przechowują również ich filtry.
		/// Możliwy jest tylko odczyt właściwości.
		/// </summary>
		//* ============================================================================================================
		public List<List<FilterInfo>> Filter
		{
			get { return this._filters; }
		}
#endregion

#region FUNKCJE PODSTAWOWE

		/// <summary>
		/// Oblicza lokalny indeks nowej kolumny.
		/// Uwzględnia kolumny podrzędne.
		/// Aby otrzymać indeks globalny wystarczy dodać do ilości starych kolumn indeks lokalny nowej kolumny.
		/// </summary>
		/// 
		/// <seealso cref="getRows"/>
		/// 
		/// <param name="column">Indeks nowej kolumny do przeliczenia.</param>
		/// 
		/// <returns>Lokalny indeks nowej kolumny.</returns>
		//* ============================================================================================================
		public int calcNewColumnIndex( int column )
		{
			// początkowy indeks kolumny i kopia (dla identyfikacji numeru kolumny)
			int col      = column - this._storage.ColumnsNumber;
			int real_col = 0;

			// oblicz numer nowej kolumny liczony od zera
			for( int x = 0; x < this._joinData.Count; ++x )
			{
				// sprawdź czy kolumna ma kolumny podrzędne
				for( int y = 0; y < this._joinData[x].Count; ++y )
				{
					col--;
					if( col <= 0 )
						break;
				}

				if( col <= 0 )
					break;

				col--;
				real_col++;
			}
			return real_col;
		}

		/// <summary>
		/// Oblicza lokalny indeks nowej kolumny.
		/// Uwzględnia kolumny podrzędne.
		/// Aby otrzymać indeks globalny wystarczy dodać do ilości starych kolumn indeks lokalny nowej kolumny.
		/// W tej wersji funkcja zwraca do drugiego parametru indeks kolumny podrzędnej, możliwy do użycia przy
		/// wywoływaniu właściwości SubColumns.
		/// </summary>
		/// 
		/// <seealso cref="getRows"/>
		/// 
		/// <param name="column">Indeks nowej kolumny do przeliczenia.</param>
		/// <param name="subidx">Zwracany indeks kolumny podrzędnej.</param>
		/// 
		/// <returns>Lokalny indeks nowej kolumny.</returns>
		//* ============================================================================================================
		public int calcNewColumnIndex( int column, out int subidx )
		{
			// początkowy indeks kolumny i kopia (dla identyfikacji numeru kolumny)
			int col      = column - this._storage.ColumnsNumber;
			int real_col = 0;
			int sub_idx  = -1;

			// oblicz numer nowej kolumny liczony od zera
			for( int x = 0; x < this._joinData.Count; ++x )
			{
				sub_idx = -1;

				// sprawdź czy kolumna ma kolumny podrzędne
				for( int y = 0; y < this._joinData[x].Count; ++y )
				{
					if( col > 0 )
						sub_idx++;
					col--;
					if( col <= 0 )
						break;
				}

				if( col <= 0 )
					break;

				col--;
				real_col++;
			}

			subidx = sub_idx;
			return real_col;
		}

		/// <summary>
		/// Dodaje nową pustą kolumnę do listy.
		/// Do każdej nowej kolumny można przypisać kilka starych kolumn traktowanych jako podrzędne.
		/// Każda taka kolumna ma możliwość osobnego filtrowania przed złączeniem w jedną wartość.
		/// Do kolumny można przypisać format z którego będzie korzystał łącznik kolumn podrzędnych.
		/// </summary>
		/// 
		/// <seealso cref="removeSubColumns"/>
		/// <seealso cref="addSubColumn"/>
		/// <seealso cref="removeColumn"/>
		/// 
		/// <param name="name">Nazwa kolumny do utworzenia.</param>
		/// 
		/// <returns>Identyfikator nowej kolumny.</returns>
		//* ============================================================================================================
		public int addColumn( string name )
		{
			this._newColumns.Add( name );
			this._joinData.Add( new List<int>() );
			this._filters.Add( new List<FilterInfo>() );
			this._columnInfo.Add( new ColumnTypeInfo() );

			// utwórz filtr formatu
			FilterInfo filter = new FilterInfo();

			filter.Filter     = FILTERTYPE.Format;
			filter.FilterCopy = false;
			filter.Exclude    = false;
			filter.Modifier   = "";
			filter.Result     = "";
			filter.SubColumn  = -1;

			// pierwszy filtr zawsze będzie filtrem formatującym
			this._filters[this._filters.Count - 1].Add( filter );

			return this._filters.Count - 1;
		}

		/// <summary>
		/// Usuwa kolumnę o podanym identyfikatorze.
		/// Możliwe jest usuwanie tylko nowych kolumn.
		/// Stare można wyciąć podczas filtrowania wszystkich danych.
		/// </summary>
		/// 
		/// <seealso cref="removeSubColumns"/>
		/// <seealso cref="addSubColumn"/>
		/// <seealso cref="addColumn"/>
		/// 
		/// <param name="colidx">Lokalny identyfikator nowej kolumny.</param>
		//* ============================================================================================================
		public void removeColumn( int colidx )
		{
			if( colidx < 0 || colidx >= this._joinData.Count )
				return;

			this._newColumns.RemoveAt( colidx );
			this._joinData.RemoveAt( colidx );
			this._filters.RemoveAt( colidx + this._storage.ColumnsNumber );
			this._columnInfo.RemoveAt( colidx + this._storage.ColumnsNumber );
		}

		/// <summary>
		/// Dodaje kolumnę podrzędną do nowo utworzonej kolumny.
		/// Funkcja pozwala na łączenie wielu kolumn w jedną, np. imię i nazwisko do jednej kolumny.
		/// Możliwe jest tworzenie łączeń tylko ze starych kolumn.
		/// </summary>
		/// 
		/// <seealso cref="removeSubColumns"/>
		/// <seealso cref="removeColumn"/>
		/// <seealso cref="addColumn"/>
		/// 
		/// <param name="colidx">Lokalny identyfikator nowej kolumny.</param>
		/// <param name="subidx">Identyfikator starej kolumny podrzędnej.</param>
		//* ============================================================================================================
		public void addSubColumn( int colidx, int subidx )
		{
			if( colidx < 0 || colidx >= this._joinData.Count )
				return;

			this._joinData[colidx].Add( subidx );

			// dodaj identyfikator do formatu
			if( this._joinData[colidx].Count > 1 )
				this._filters[colidx + this._storage.ColumnsNumber][0].Modifier += ", #" + this._joinData[colidx].Count;
			else
				this._filters[colidx + this._storage.ColumnsNumber][0].Modifier += "#" + this._joinData[colidx].Count;
		}

		/// <summary>
		/// Usuwa wszystkie złożenia kolumn (kolumny podrzędne).
		/// Wraz z kolumnami podrzędnymi czyści również wszystkie filtry należące do tych kolumn.
		/// </summary>
		/// 
		/// <seealso cref="addSubColumn"/>
		/// <seealso cref="removeColumn"/>
		/// <seealso cref="addColumn"/>
		/// 
		/// <param name="colidx">Identyfikator kolumny złożonej.</param>
		//* ============================================================================================================
		public void removeSubColumns( int colidx )
		{
			if( colidx < 0 || colidx >= this._joinData.Count )
				return;

			this._joinData[colidx].Clear();
			
			// zapisz format, wyczyść wszystkie filtry i dodaj format
			var format = this._filters[colidx + this._storage.ColumnsNumber][0];

			this._filters[colidx + this._storage.ColumnsNumber].Clear();
			this._filters[colidx + this._storage.ColumnsNumber].Add( format );

			// usuń format
			this._filters[colidx + this._storage.ColumnsNumber][0].Modifier = "";
		}

#endregion

#region FILTROWANIE

		/// <summary>
		/// Tworzy nowy filtr z podanych argumentów.
		/// Filtr nie zawiera informacji o kolumnie podrzędnej, dodawana jest ona automatycznie podczas przypisywania
		/// filtra do kontretnej kolumny.
		/// W zależności od typu filtra, poszczególne wartości mogą być uwzględniane lub nie.
		/// Warto jednak pamiętać aby dla typów tekstowych nie przypisywać wartości null.
		/// Dokładny opis poszczególnych pól znajduje się w opisie struktury.
		/// </summary>
		/// 
		/// <seealso cref="addFilter"/>
		/// <seealso cref="replaceFilter"/>
		/// 
		/// <param name="filter">Typ filtra.</param>
		/// <param name="modifier">Modyfikator dla filtra (używany lub nie w zależności od typu).</param>
		/// <param name="result">Rezultat działania filtra (używany lub nie w zależności od typu).</param>
		/// <param name="exclude">Wyrzucenie wartości ze zbioru spełniających kryteria zawarte w filtrze.</param>
		/// <param name="copy">Kopiowanie filtra do innych kolumn (dziedziczenie dla kopii).</param>
		/// 
		/// <returns>Nowo utworzony filtr.</returns>
		//* ============================================================================================================
		public FilterInfo createFilter( FILTERTYPE filter, string modifier, string result, bool exclude, bool copy )
		{
			FilterInfo finfo = new FilterInfo();

			finfo.Filter     = filter;
			finfo.SubColumn  = -1;
			finfo.Result     = result;
			finfo.Modifier   = modifier;
			finfo.Exclude    = exclude;
			finfo.FilterCopy = copy;

			return finfo;
		}
		
		/// <summary>
		/// Dodaje nowy filtr do listy.
		/// Filtr nie musi zawierać informacji o kolumnie podrzędnej - dodawane jest to automatycznie.
		/// Filtr może być dziedziczony, w tym przypadku nie jest on kopiowany do każdej dziedziczonej kolumny,
		/// zostaje tylko w tej konkretnej, w której został utworzony, ale jest uwzględniany w pozostałych podczas
		/// stosowania filtrów na wartości.
		/// </summary>
		/// 
		/// <seealso cref="getRows"/>
		/// <seealso cref="replaceFilter"/>
		/// <seealso cref="removeFilter"/>
		/// 
		/// <param name="column">Indeks kolumny zawierającej filtry.</param>
		/// <param name="subcol">Indeks kolumny podrzędnej.</param>
		/// <param name="finfo">Nowy filtr do dodania.</param>
		//* ============================================================================================================
		public void addFilter( int column, int subcol, FilterInfo finfo )
		{
			// poza zakresem
			if( column >= this.Filter.Count || column < 0 )
				return;

			this.Filter[column].Add( finfo );
			this.Filter[column][this.Filter[column].Count-1].SubColumn = subcol;
		}
		
		/// <summary>
		/// Usuwa wybrany filtr z listy.
		/// Funkcja wyszukuje w kolumnie filtr o podanym identyfikatorze i usuwa go.
		/// Filtry dziedziczone nie są uwzględniane w indeksach, indeks filtra dla kolumny podrzędnej zawierającej
		/// filtry dziedziczone należy więc obliczyć wcześniej.
		/// </summary>
		/// 
		/// <seealso cref="addFilter"/>
		/// <seealso cref="replaceFilter"/>
		/// <seealso cref="isInherited"/>
		/// 
		/// <param name="column">Indeks kolumny do poszukiwania filtrów.</param>
		/// <param name="subcol">Indeks kolumny podrzędnej.</param>
		/// <param name="filter">Indeks filtru do usunięcia.</param>
		//* ============================================================================================================
		public void removeFilter( int column, int subcol, int filter )
		{
			// poza zakresem
			if( column >= this.Filter.Count || column < 0 || filter < 0 || filter >= this.Filter[column].Count )
				return;

			// indeks filtra
			int index = subcol == -1
				? filter
				: this.findSubFilterIndex( column, subcol, filter );

			// nie ma takiego filtra
			if( index == -1 )
				return;

			this.Filter[column].RemoveAt( index );
		}
		
		/// <summary>
		/// Podmienia podany w argumentach filtr na nowy.
		/// Funkcja wyszukuje w kolumnie filtr o podanym identyfikatorze i podmienia na nowy.
		/// Filtr nie musi zawierać informacji o kolumnie podrzędnej - dodawane jest to automatycznie.
		/// </summary>
		/// 
		/// <seealso cref="createFilter"/>
		/// <seealso cref="addFilter"/>
		/// <seealso cref="removeFilter"/>
		/// <seealso cref="isInherited"/>
		/// 
		/// <param name="column">Indeks kolumny do poszukiwania filtrów.</param>
		/// <param name="subcol">Indeks kolumny podrzędnej.</param>
		/// <param name="filter">Indeks filtru do podmiany.</param>
		/// <param name="finfo">Nowy filtr przeznaczony do podmiany.</param>
		//* ============================================================================================================
		public void replaceFilter( int column, int subcol, int filter, FilterInfo finfo )
		{
			// poza zakresem
			if( column >= this.Filter.Count || column < 0 || filter < 0 || filter >= this.Filter[column].Count )
				return;

			// indeks filtra
			int index = subcol == -1
				? filter
				: this.findSubFilterIndex( column, subcol, filter );

			// nie ma takiego filtra
			if( index == -1 )
				return;

			// zamień wartości
			this.Filter[column][index].SubColumn  = subcol;
			this.Filter[column][index].Filter     = finfo.Filter;
			this.Filter[column][index].Modifier   = finfo.Modifier;
			this.Filter[column][index].Result     = finfo.Result;
			this.Filter[column][index].Exclude    = finfo.Exclude;
			this.Filter[column][index].FilterCopy = finfo.FilterCopy;
		}
		
		/// <summary>
		/// Wyszukuje indeks bezpośredni dla filtra podrzędnego.
		/// Filtry podrzędne zapisywane są w tej samej lokalizacji co zwykłe.
		/// Różnią się jedynie tym że są przypisane do innej kolumny, a właściwie kolumny podrzędnej.
		/// Nie uwzględnia filtrów dziedziczonych.
		/// </summary>
		/// 
		/// <seealso cref="replaceFilter"/>
		/// <seealso cref="removeFilter"/>
		/// 
		/// <param name="column">Indeks kolumny do poszukiwania filtrów.</param>
		/// <param name="subcol">Indeks kolumny podrzędnej.</param>
		/// <param name="filter">Indeks filtru do wyszukania.</param>
		/// 
		/// <returns>Bezpośredni indeks dla filtru podrzędnego.</returns>
		//* ============================================================================================================
		public int findSubFilterIndex( int column, int subcol, int filter )
		{
			int index   = 0;
			var filters = this.Filter[column];

			// szukaj odpowiedniego filtra
			for( int x = 0; x < filters.Count; ++x )
			{
				if( filters[x].SubColumn == subcol )
				{
					// zwróć indeks filtra
					if( index == filter )
						return x;
					index++;
				}
			}

			return -1;
		}
		
		/// <summary>
		/// Sprawdza czy podany indeks filtru jest filtrem dziedziczonym.
		/// W przypadku gdy filtr jest dziedziczony funkcja zwraca indeks kolumny gdzie znajduje się źródło filtra
		/// wraz z jego indeksem zwracanym w parametrze.
		/// Gdy filtr nie jest dziedziczony w parametrze zwracany jest podany indeks pomniejszony o ilość filtrów
		/// dziedziczonych.
		/// </summary>
		/// 
		/// <seealso cref="getFilter" />
		/// <seealso cref="findSubFilterIndex" />
		/// 
		/// <param name="column">Indeks kolumny do poszukiwania filtrów.</param>
		/// <param name="subcol">Indeks kolumny podrzędnej.</param>
		/// <param name="filter">Indeks filtra do sprawdzenia.</param>
		/// <param name="selected">Zwracany realny indeks filtra.</param>
		/// 
		/// <returns>Indeks źródła filtru gdy jest dziedziczony lub -1.</returns>
		//* ============================================================================================================
		public int isInherited( int column, int subcol, int filter, out int selected )
		{
			// pobierz identyfikator kolumny kopiowanej
			int realid = this._joinData[column][subcol];
			int cindex = 0;

			// szukaj kopiowanych filtrów
			for( int x = 0; x < this.Filter[realid].Count; ++x )
			{
				if( this.Filter[realid][x].FilterCopy )
				{
					// zwróć indeks konkretnego dziedziczonego filtra
					if( cindex == filter )
					{
						selected = x;
						return realid;
					}
					cindex++;
				}
			}

			// prawdziwy indeks filtru
			selected = filter - cindex;
			return -1;
		}

		/// <summary>
		/// Pobiera określony filtr z podanej kolumny.
		/// Funkcja nie uwzględnia filtrów dziedziczonych z kolumn głównych.
		/// Normalnie filtry dziedziczone powinny być wypisywane na samej górze w liście filtrów.
		/// </summary>
		/// 
		/// <seealso cref="isInherited" />
		/// <seealso cref="findSubFilterIndex" />
		/// 
		/// <param name="column">Indeks kolumny do poszukiwania filtrów.</param>
		/// <param name="subcol">Indeks kolumny podrzędnej.</param>
		/// <param name="filter">Indeks pobieranego filtra.</param>
		/// 
		/// <returns>Informacje o znalezionym filtrze lub NULL.</returns>
		//* ============================================================================================================
		public FilterInfo getFilter( int column, int subcol, int filter )
		{
			if( subcol == -1 )
				return this.Filter[column][filter];
			else
			{
				int index   = 0;
				var filters = this.Filter[column];

				// szukaj odpowiedniego filtra
				for( int x = 0; x < filters.Count; ++x )
				{
					if( filters[x].SubColumn == subcol )
					{
						// zwróć indeks filtra
						if( index == filter )
							return filters[x];
						index++;
					}
				}
			}
			return null;
		}

		/// <summary>
		/// Pobiera okrojone i przefiltrowane dane ze strumienia.
		/// Funkcja nie zapisuje danych do strumienia, dane są formatowane w locie.
		/// Działanie nie jest szybkie, ale pozwala na anulowanie zmian wcześniej wprowadzonych.
		/// </summary>
		/// 
		/// <seealso cref="applyFilterForOldColumn" />
		/// <seealso cref="applyFilterForNewColumn" />
		/// <seealso cref="calcNewColumnIndex" />
		/// 
		/// <param name="column">Indeks kolumny do pobrania.</param>
		/// <param name="rows">Ilość zwracanych rekordów.</param>
		/// <param name="calc">Obliczanie indeksu nowych kolumn z przekazanego indeksu.</param>
		/// 
		/// <returns>Lista przefiltrowanych danych ze strumienia.</returns>
		//* ============================================================================================================
		public List<string> getRows( int column, int rows = 0, bool calc = false )
		{
			// identyfikator nowej kolumny (gdy wartość jest większa niż ilość starych kolumn)
			int newcol_idx = -1;

			if( column < 0 )
				column = 0;

			// oblicz nowy identyfikator
			if( calc && column >= this._storage.ColumnsNumber )
				newcol_idx = this.calcNewColumnIndex( column );

			if( rows < 1 )
				rows = this._storage.RowsNumber;
			if( rows >= this._storage.RowsNumber )
				rows = this._storage.RowsNumber;

			List<string> retval;

			// nowy identyfikator kolumny
			if( newcol_idx > -1 )
				column = this._storage.ColumnsNumber + newcol_idx;

			// upewnij się że numer kolumny nie wyjdzie poza zakres
			if( column >= this._storage.ColumnsNumber + this._newColumns.Count )
				column = 0;

			// wyświetl wiersze bez filtrów
			if( this._filters[column].Count == 0 )
			{
				retval = new List<string>( rows );
				for( int x = 0; x < rows; ++x )
					retval.Add( this._storage.Row[x][column] );
			}
			// filtruj stare kolumny
			else if( column < this._storage.ColumnsNumber )
				retval = this.applyFilterForOldColumn( column, 0, rows );
			// filtruj nowo utworzone kolumny
			else
				retval = this.applyFilterForNewColumn( column, 0, rows );

			return retval;
		}

		/// <summary>
		/// Stosuje filtry dla wierszy w wybranej kolumnie.
		/// Posiada możliwość określenia maksymalnej ilości zwracanych wierszy i pozycję startową filtrowania.
		/// Filtry obejmują tylko kolumny nowo utworzone wraz z ich kolumnami podrzędnymi, a więc nie obejmują starych,
		/// istniejących już wcześniej kolumn.
		/// Podczas filtrowania na początku tworzony jest format dla kolumn, następnie każda z kolumn podrzędnych
		/// filtrowana jest względem przypisanych do niej filtrów oraz filtrów które są dziedziczone z kolumny głównej.
		/// </summary>
		/// 
		/// <seealso cref="applyFilterForOldColumn" />
		/// <seealso cref="applyFilterForValue" />
		/// 
		/// <param name="column">Kolumna z której będą pobierane wiersze.</param>
		/// <param name="start">Pozycja początkowa dla filtrowania.</param>
		/// <param name="rows">Ilość wierszy do pobrania.</param>
		/// 
		/// <returns>Lista przefiltrowanych wierszy w kolumnie.</returns>
		//* ============================================================================================================
		private List<string> applyFilterForNewColumn( int column, int start, int rows )
		{
			List<string> values = new List<string>( rows );
			List<int>    cols   = this._joinData[column - this._storage.ColumnsNumber];
			string       format = this._filters[column][0].Modifier;

			// utwórz format jeżeli nie został zdefiniowany
			if( format == "" || format == null )
			{
				format = "";
				for( int x = 0; x < cols.Count; ++x )
				{
					if( x == 0 )
						format += "#1";
					else
						format += ", #" + (x+1);
				}
			}

			// pobierz wszystkie filtry dla kolumny podrzędnej
			var filters  = new List<List<FilterInfo>>();
			int localidx = column - this._storage.ColumnsNumber;

			for( int x = 0; x < cols.Count; ++x )
			{
				filters.Add( new List<FilterInfo>() );

				int inhid = this._joinData[localidx][x];

				// filtry dziedziczone
				for( int y = 0; y < this.Filter[inhid].Count; ++y )
					if( this.Filter[inhid][y].FilterCopy )
						filters[x].Add( this.Filter[inhid][y] );
				
				// filtry kolumny podrzędnej
				for( int y = 0; y < this.Filter[column].Count; ++y )
					if( this.Filter[column][y].SubColumn == x )
						filters[x].Add( this.Filter[column][y] );
			}

			// filtruj wiersze
			for( int pos = start, end = start + rows; pos < end; ++pos )
			{
				// zapisz format kolumn
				string value = format;
				bool   skip  = false;

				// zastosuj filtry dla poszczególnych kolumn
				for( int x = 0; x < cols.Count; ++x )
				{
					string colval = this.applyFilterForValue( this._storage.Row[pos][cols[x]], filters[x] );

					// pomiń gdy wartość nie spełnia warunku filtra
					if( colval == null )
					{
						skip = true;
						break;
					}

					// zamień wszystkie wystąpienia
					value = value.Replace( "#" + (x+1), colval ); 
				}

				// pomiń
				if( skip )
					continue;

				// zamień #\ na # i dodaj wartość do listy
				value = value.Replace( "#\\", "#" );
				values.Add( value );
			}

			// zwróć listę kolumn
			return values;
		}
		
		/// <summary>
		/// Stosuje filtry dla wierszy w wybranej kolumnie.
		/// Posiada możliwość określenia maksymalnej ilości zwracanych wierszy i pozycję startową filtrowania.
		/// Filtry obejmują tylko kolumny istniejące w bazie danych, a więc nie obejmuje nowo tworzonych kolumn
		/// wraz z ich kolumnami podrzędnymi.
		/// </summary>
		/// 
		/// <seealso cref="applyFilterForNewColumn" />
		/// <seealso cref="applyFilterForValue" />
		/// 
		/// <param name="column">Kolumna z której będą pobierane wiersze.</param>
		/// <param name="start">Pozycja początkowa dla filtrowania.</param>
		/// <param name="rows">Ilość wierszy do pobrania.</param>
		/// 
		/// <returns>Lista wybranych i przefiltrowanych wierszy w kolumnie.</returns>
		//* ============================================================================================================
		private List<string> applyFilterForOldColumn( int column, int start, int rows )
		{
			var values = new List<string>( rows );
			var filter = this.Filter[column];

			for( int x = 0; x < rows; ++x )
			{
				// filtruj wartość z tabeli (krotkę)
				string value = this._storage.Row[x][column];
				value = this.applyFilterForValue( value, filter );

				// dodaj gdy spełnia warunki filtrowania
				if( value != null )
					values.Add( value );
			}
			
			return values;
		}

		/// <summary>
		/// Ustawia kolumny które mają pozostać przy wyświetlaniu lub zapisie.
		/// Lista kolumn przekazywanych jako parametr ma być równa liście kolumn dostępnych <value>Storage</value>.
		/// Nie można ukryć kolumn nowo utworzonych, wychodząc z założenia że jeżeli się je utworzyło, to muszą zostać.
		/// </summary>
		/// 
		/// <param name="columns">Lista dostępności kolumn, które mają być ukryte a które nie.</param>
		//* ============================================================================================================
		public void setActiveColumns( List<bool> columns )
		{
			if( columns.Count != this._columnActive.Count )
				return;

			for( int x = 0; x < this._columnActive.Count; ++x )
				this._columnActive[x] = columns[x];
		}

		/// <summary>
		/// Stosowanie filtrów do danych przypisanych do klasy.
		/// Usuwa stare kolumny które zostały ukryte i nie są aktywne oraz filtruje kolumny które nie zostały ukryte.
		/// Łączy kolumny tworząc nową i filtruje je zgodnie z przekazanymi wcześniej wytycznymi w podanym formacie.
		/// </summary>
		/// 
		/// <returns>Informacja o tym czy operacja się powiodła czy nie.</returns>
		//* ============================================================================================================
		public bool applyFilters()
		{
			var formats = new string[this._joinData.Count];
			var newfmts = new List<List<List<FilterInfo>>>( this._joinData.Count );

			// pobierz filtry i format dla nowych kolumn
			for( int x = 0; x < this._joinData.Count; ++x )
			{
				int filterid = x + this._storage.ColumnsNumber;
				newfmts.Add( new List<List<FilterInfo>>() );

				// pobierz format
				formats[x] = this._filters[filterid][0].Modifier;

				// utwórz jeżeli nie został zdefiniowany
				if( formats[x] == "" || formats[x] == null )
				{
					formats[x] = "";
					for( int z = 0; z < this._joinData[x].Count; ++z )
					{
						if( z == 0 )
							formats[x] += "#1";
						else
							formats[x] += ", #" + (z+1);
					}
				}

				// filtry dla kolumn podrzędnych
				for( int z = 0; z < this._joinData[x].Count; ++z )
				{
					newfmts[x].Add( new List<FilterInfo>() );

					int inhid = this._joinData[x][z];
					int idofx = x + this._storage.ColumnsNumber;

					// filtry dziedziczone
					for( int w = 0; w < this._filters[inhid].Count; ++w )
						if( this._filters[inhid][w].FilterCopy )
							newfmts[x][z].Add( this._filters[inhid][w] );
				
					// filtry kolumny podrzędnej
					for( int w = 0; w < this._filters[idofx].Count; ++w )
						if( this._filters[idofx][w].SubColumn == z )
							newfmts[x][z].Add( this._filters[idofx][w] );
				}
			}

			var columnnames = new List<string>();

			// stare kolumny - nie wszystkie muszą być dostępne
			for( int x = 0; x < this._columnActive.Count; ++x )
				if( this._columnActive[x] )
					columnnames.Add( this._storage.Column[x] );

			// nowe kolumny
			for( int x = 0; x < this._newColumns.Count; ++x )
				columnnames.Add( this._newColumns[x] );

			// tryb edycji
			this._storage.editMode( columnnames.ToArray() );

			while( this._storage.nextRow() )
			{
				int  idx = 0;
				bool del = false;

				string[] row    = this._storage.getCurrentRow();
				string[] newrow = new string[columnnames.Count];
				
				// filtry dla starych kolumn
				for( int x = 0; x < this._storage.ColumnsNumber; ++x )
				{
					if( !this._columnActive[x] )
						continue;
					newrow[idx] = this.applyFilterForValue( row[x], this._filters[x] );

					if( newrow[idx] == null )
					{
						del = true;
						break;
					}
					idx++;
				}

				// usuń wiersz przed łączeniem kolumn i przejdź do kolejnej pętli
				if( del )
				{
					this._storage.removeCurrentRow();
					continue;
				}

				// filtry dla nowych kolumn
				for( int x = 0; x < this._joinData.Count; ++x )
				{
					newrow[idx] = formats[x];

					// podkolumny
					for( int y = 0; y < this._joinData[x].Count; ++y )
					{
						string colval = this.applyFilterForValue( row[this._joinData[x][y]], newfmts[x][y] );
 
						if( colval == null )
						{
							del = true;
							break;
						}
						newrow[idx] = newrow[idx].Replace( "#" + (y+1), colval );
					}

					// przerwij jeżeli rekord jest do usunięcia
					if( del )
						break;

					newrow[idx] = newrow[idx].Replace( "#\\", "#" );
					idx++;
				}

				// usuń wiersz
				if( del )
				{
					this._storage.removeCurrentRow();
					continue;
				}

				// podmień aktualny wiersz na inny
				this._storage.replaceCurrentRow( newrow );
			}

			// sprawdź poprawność nowych danych
			this._storage.checkIntegrity();

			// jeżeli wszystko jest w porządku, zwróć true
			if( this._storage.Ready )
				return true;

			return false;
		}
		
		/// <summary>
		/// Stosuje podane w argumencie filtry na podanej wartości.
		/// Zwraca przefiltrowaną wartość lub NULL, które zostaje zwrócone gdy filtr odrzuci wartość od warunku.
		/// </summary>
		/// 
		/// <seealso cref="applyFilterForOldColumn" />
		/// <seealso cref="applyFilterForNewColumn" />
		/// 
		/// <param name="value">Wartość do przefiltrowania.</param>
		/// <param name="filters">Lista filtrów stosowanych dla podanej wartości.</param>
		/// 
		/// <returns>Nowa przefiltrowana wartość lub NULL gdy nie spełni warunku filtru.</returns>
		//* ============================================================================================================
		private string applyFilterForValue( string value, List<FilterInfo> filters )
		{
			for( int x = 0; x < filters.Count; ++x )
				switch( filters[x].Filter )
				{
				// małe litery
				case FILTERTYPE.LowerCase:
					value = value.ToLower();
				break;
				// duże litery
				case FILTERTYPE.UpperCase:
					value = value.ToUpper();
				break;
				// nazwa własna
				case FILTERTYPE.TitleCase:
					value = Program.StringTitleCase( value );
				break;
				// równy
				case FILTERTYPE.Equal:
					if( value.Equals(filters[x].Modifier) )
					{
						if( filters[x].Exclude )
							return null;
						
						if( filters[x].Result.IndexOf("#") != -1 )
							value = filters[x].Result.Replace( "##", value ).Replace( "#\\#", "##" );
						else
							value = filters[x].Result;
					}
				break;
				// różny
				case FILTERTYPE.NotEqual:
					if( !value.Equals(filters[x].Modifier) )
					{
						if( filters[x].Exclude )
							return null;
						
						if( filters[x].Result.IndexOf("#") != -1 )
							value = filters[x].Result.Replace( "##", value ).Replace( "#\\#", "##" );
						else
							value = filters[x].Result;
					}
				break;
				}

			return value;
		}

#endregion
	}
}
