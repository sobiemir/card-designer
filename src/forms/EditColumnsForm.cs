///
/// $i06 EditColumnsForm.cs (I03)
/// 
/// Okno edycji kolumn bazy danych.
/// Uruchamia okno tworzenia kolumn i filtrowania danych.
/// Plik zawiera formularz operujący tylko i wyłącznie na kolumnach.
/// Modyfikacja danych odbywa się za pomocą innego formularza.
/// 
/// Autor: Kamil Biały
/// Od wersji: 0.6.x.x
/// Ostatnia zmiana: 2016-12-24
/// 
/// CHANGELOG:
/// [29.06.2015] Wersja początkowa.
/// [28.07.2015] Zmiana nazwy klasy i pliku z JoinColsForm na EditColumnsForm,
///              dodano przycisk przechodzący do formularza filtrowania danych,
///              utworzono logikę (utworzony było samo okno z kontrolkami bez akcji).
/// [05.08.2015] Uruchomienie filtrów dla kolumn, sprawdzanie poprawności wpisanych danych,
///              funkcja odświeżająca wyświetlane dane.
/// [15.02.2016] Nowy przycisk edytora wierszy - podpięcie formularza edycji wierszy.
/// [19.04.2016] Nowa wersja edytora kolumn - zmieniona koncepcja - przeniesienie filtrowania kolumn do edytora.
/// [06.07.2016] Wdrażanie tłumaczeń, regiony, komentarze.
/// [15.08.2016] Podstawowe filtrowanie, wyświetlanie rezultatów filtrów.
/// [27.08.2016] Zapis filtrów do strumienia po kliknięciu w przycisk.
/// [15.12.2016] Możliwe do wpisania znaki pobierane z pliku językowego.
/// [24.12.2016] Zmiana koncepcji nazewnictwa zmiennych.
///

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using CDesigner.Utils;
using CDesigner.Forms;

namespace CDesigner.Forms
{
	/// 
	/// <summary>
	/// Formularz edycji kolumn z bazy danych.
	/// Pozwala na łączenie kolumn i włączenie filtrowania danych w poszczególnych kolumnach.
	/// Składa się z dwóch stron, pierwsza pozwala na tworzenie pustych (dopóki pusty będzie format) lub łączonych kolumn.
	/// Druga strona pozwala na wybór odpowiednich filtrów i formatów dla każdej z kolumn, zarówno starych jak i nowych.
    /// Nowe kolumny pozwalają tylko i wyłącznie na ustawienie formatu wyświetlanych danych.
    /// Filtrowanie dla nich jest dostępne po kliknięciu na kolumnę podrzędną wybranego elementu.
    /// Możliwe jest dziedziczenie filtrów pod zaznaczeniu odpowiedniego pola.
	/// </summary>
	/// 
    /// @todo <dfn><small>[0.8.x.x]</small></dfn> Zastępowanie kolumn w przypadku takiej samej nazwy.
    /// @todo <dfn><small>[0.8.x.x]</small></dfn> Odświeżanie danych po przejściu na inny krok.
    /// @todo <dfn><small>[0.8.x.x]</small></dfn> Po przeciągnięciu kolumny do nowej, wyświetlana jest zawartość nowej a nie starej.
    /// @todo <dfn><small>[0.8.x.x]</small></dfn> Po skupieniu na polu edycji ENTER działa jako dodawanie nowego pola.
    /// @todo <dfn><small>[0.9.x.x]</small></dfn> Możliwość zmiany ilości wyświetlanych wierszy.
	/// @todo <dfn><small>[0.9.x.x]</small></dfn> Zmiana typu filtra dla kolumn.
    /// @todo <dfn><small>[0.9.x.x]</small></dfn> Zmiana szerokości kolumn wraz ze zmianą rozmiarów okna.
    /// @todo <dfn><small>[0.9.x.x]</small></dfn> Wyświetlanie zapytania przy kopiowaniu kolumny która już została skopiowana do
    ///                                           tej samej kolumny (czy na pewno chcesz ją skopiować drugi raz?).
    /// @todo <dfn><small>[1.0.x.x]</small></dfn> Ustawienia...
    /// @todo <dfn><small>[1.0.x.x]</small></dfn> Wyrażenia regularne dla tekstowych typów danych.
    /// @todo <dfn><small>[?.?.x.x]</small></dfn> Po kliknięciu myszą, zmianie klawiaturą elementu w liście i ruszeniu myszą
    ///                                           zaznacza się wcześniej zaznaczony po kliknięciu element.
    /// @todo <dfn><small>[?.?.x.x]</small></dfn> Przy bezpośrednim przejściu z ListView do ComboBox elementy w ListView migają.
    /// @todo <dfn><small>[?.?.x.x]</small></dfn> Skróty klawiaturowe nie działają przy focusie na liście z kolumnami.
    /// @todo <dfn><small>[?.?.x.x]</small></dfn> Możliwość zapisania tylko filtrów a nie danych (ukryty checkbox).
    ///                                           W tym przypadku zapisywane są tylko filtry, a przycisk anuluj powraca do
    ///                                           wcześniejszych ustawień filtrów. Pomysł jest taki żeby dane zapisywało do pamięci
    ///                                           a w czasie edycji wczytywało stare i wyświetlało stare filtry.
    ///                                           Tylko w jaki sposób będą zachowywać się zmienione lub dodane dane?
    ///                                           Może zapisywać je dodatkowo w osobnej tabeli?
    ///                                           Będzie można je potem dodać lub odrzucić w zależności od filtrów.
    ///                                           W każdym razie filtry muszą zostawać żeby odrzucać nowe kolumny przy edycji.
    ///                                           Złączeń można nie traktować poważnie przy edycji, bo są one i tak złączeniami
    ///                                           z aktualnie istniejących w bazie danych kolumn.
	/// 
    /// <example>
    /// Szybki przykład użycia klasy:
    /// <code>
    /// // utwórz nową klasę i przypisz do niej dane na których ma operować
    /// var form = new EditColumnsForm();
    /// form.Storage = new IOFileData( "test.csv" );
    /// 
    /// // pokaż formularz edycji kolumn
    /// if( form.ShowDialog() == DialogResult.OK )
    ///     Console.WriteLine( "Wszelkie zmiany zostały zapisane." );
    /// else
    ///     Console.WriteLine( "Zmiany nie zostały zapisane." );
    /// 
    /// // rób teraz z tym co chcesz
    /// var storage = form.Storage;
    /// </code>
    /// </example>
    ///
	public partial class EditColumnsForm : Form
	{
#region ZMIENNE

		/// <summary>Blokada kontrolek (lub innych elementów) przed odświeżeniem.</summary>
		private bool _locked;

		/// <summary>Schowek z danymi wczytanej bazy.</summary>
		private DataStorage _storage;

		/// <summary>Klasa filtrowania danych.</summary>
		private DataFilter _filter;

		/// <summary>Informacja o tym czy wyświetlany jest dymek z wiadomością.</summary>
		private bool _showTooltip;

		/// <summary>Aktualnie zaznaczona kolumna w sekcji z filtrami (dla nowych filtrów).</summary>
		private int _currentColumn;

		/// <summary>Indeks nowo utworzonej kolumny, liczony od 0.</summary>
		private int _realNewIndex;
		
		/// <summary>Indeks kolumny podrzędnej liczony od 0.</summary>
		private int _realSubIndex;

#endregion

#region KONSTRUKTOR / WŁAŚCIWOŚCI

		/// <summary>
		/// Konstruktor klasy.
		/// Tworzy instancje klasy i tłumaczy cały formularz na aktualny język.
        /// Przed wyświetleniem formularza edycji warto ustawić dla klasy dane na których ma operować.
        /// Przykład użycia konstruktora podany został w opisie klasy.
		/// </summary>
		/// 
		/// <seealso cref="DataFilter"/>
		//* ============================================================================================================
		public EditColumnsForm()
		{
			this.InitializeComponent();

			// ikona programu
			this.Icon = Program.GetIcon();

            this._storage      = null;
			this._filter       = new DataFilter();
            this._showTooltip = false;
            this._locked       = false;

            this._currentColumn = -1;
            this._realNewIndex  = -1;
            this._realSubIndex  = -1;

			// zaznacz pierwszy krok
			this.CBX_Step.SelectedIndex = 0;

			// tłumaczenia kontrolek
			this.translateForm();
		}

		/// <summary>
		/// Pobiera lub ustawia nowy schowek danych.
		/// Po ustawieniu pobiera z pliku potrzebne dane.
		/// </summary>
        /// 
		/// <seealso cref="DataStorage"/>
		//* ============================================================================================================
		public DataStorage Storage
		{
			get { return this._storage; }
			set
			{
                if( this._locked )
                    return;

				if( value == null )
				{
					this._storage = null;
					return;
				}

				if( !value.Ready )
					return;

#			if DEBUG
				Program.LogMessage( "Zmiana strumienia danych." );
#			endif

				this._storage         = value;
				this._filter.Storage = value;

				this.getPreview();
			}
		}
        
		/// <summary>
		/// Pobiera przypisaną klasę filtra danych.
        /// Właściwosć tylko do odczytu.
		/// </summary>
        /// 
		/// <seealso cref="DataFilter"/>
		//* ============================================================================================================
        public DataFilter Filter
        {
            get { return this._filter; }
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
#		if DEBUG
			Program.LogMessage( "Tłumaczenie kontrolek znajdujących się na formularzu." );
#		endif

			// nagłówki tabel
			var values = Language.GetLines( "EditColumns", "Headers" );
			this.CH_ColumnName.Text    = values[(int)LANGCODE.I03_HEA_COLUMNNAME];
			this.CH_JoinedColumns.Text = values[(int)LANGCODE.I03_HEA_COLUMNS];
			this.CH_DataPreview.Text   = values[(int)LANGCODE.I03_HEA_ROWS];
			this.CB_RecordPreview.Text = values[(int)LANGCODE.I03_HEA_ROWS];
			this.CH_Columns.Text       = values[(int)LANGCODE.I03_HEA_AVALIABLE];
			this.CH_FilterList.Text    = values[(int)LANGCODE.I03_HEA_FILTERS];

			this.LV_AllColumns.Groups[0].Header = values[(int)LANGCODE.I03_HEA_OLDCOLUMNS];
			this.LV_AllColumns.Groups[1].Header = values[(int)LANGCODE.I03_HEA_NEWCOLUMNS];

			// kroki
			values = Language.GetLines( "EditColumns", "Steps" );
			for( int x = 0; x < this.CBX_Step.Items.Count; ++x )
				this.CBX_Step.Items[x] = (object)values[x];

			// przyciski
			values = Language.GetLines( "EditColumns", "Buttons" );
			this.B_AddColumn.Text     = values[(int)LANGCODE.I03_BUT_ADD];
			this.B_ClearColumn.Text   = values[(int)LANGCODE.I03_BUT_CLEAR];
			this.B_DeleteColumn.Text  = values[(int)LANGCODE.I03_BUT_REMOVE];
			this.B_Save.Text          = values[(int)LANGCODE.I03_BUT_SAVE];
			this.B_Cancel.Text        = values[(int)LANGCODE.I03_BUT_CANCEL];
			this.B_AddFilter.Text     = values[(int)LANGCODE.I03_BUT_ADD];
			this.B_ChangeFilter.Text  = values[(int)LANGCODE.I03_BUT_CHANGE];
			this.B_DeleteFilter.Text  = values[(int)LANGCODE.I03_BUT_REMOVE];
			this.B_ChangeColType.Text = values[(int)LANGCODE.I03_BUT_CHANGETYPE];
			this.B_AdvancedSets.Text  = values[(int)LANGCODE.I03_BUT_ADVANCED];

			// napisy na formularzu
			values = Language.GetLines( "EditColumns", "Labels" );
			this.GB_ColumnType.Text   = values[(int)LANGCODE.I03_LAB_COLUMNTYPE];
			this.GB_FilterConfig.Text = values[(int)LANGCODE.I03_LAB_FILTERCONF];
			this.L_FilterType.Text    = values[(int)LANGCODE.I03_LAB_FILTERTYPE];
			this.L_Modifier.Text      = values[(int)LANGCODE.I03_LAB_MODIFIER];
			this.L_Result.Text        = values[(int)LANGCODE.I03_LAB_RESULT];
			this.CB_Exclude.Text      = values[(int)LANGCODE.I03_LAB_EXCLUDE];
			this.CB_AllCopies.Text    = values[(int)LANGCODE.I03_LAB_APPLYCOPY];

			// lista typów dla kolumn
			values = Language.GetLines( "EditColumns", "ColumnTypes" );
			this.CBX_ColType.Items.Clear();
			for( int x = 0; x < values.Count - 1; ++x )
				this.CBX_ColType.Items.Add( values[x] );

			// zapisane ustawienia typów
			this.CBX_Saved.Items.Add( values[values.Count-1] );

			// nazwa formy
			this.Text = Language.GetLine( "FormNames", (int)LANGCODE.GFN_EDITCOLUMNS );
		}

		/// <summary>
		/// Uzupełnia listę danymi ze schowka.
        /// Wyświetla wszystkie kolumny ze schowka oraz wiersze dla pierwszej dostępnej kolumny.
        /// Domyślna ilość wyświetlanych wierszy pobierana jest z ustawień programu.
        /// Wywoływana jest praktycznie tylko podczas zmiany schowka, lecz nic nie przeszkadza jej użyć w innym miejscu.
		/// </summary>
        /// 
        /// <seealso cref="Storage" />
        /// <seealso cref="refreshColumnRowsIFS" />
		//* ============================================================================================================
		public void getPreview()
		{
			this._locked = true;

			// wyczyść tabele
			this.LV_DatabaseColumns.Items.Clear();
			this.LV_AllColumns.Items.Clear();

			// uzupełnij tabele nazwami kolumn
			for( int x = 0; x < this._storage.ColumnsNumber; ++x )
			{
				this.LV_DatabaseColumns.Items.Add( this._storage.Column[x] );
				this.LV_DatabaseColumns.Items[x].Checked = true;

				this.LV_AllColumns.Items.Add( this._storage.Column[x] );
				this.LV_AllColumns.Items[x].Tag = null;
				this.LV_AllColumns.Items[x].Group = this.LV_AllColumns.Groups[0];
			}

			// wyświetl wiersze pierwszej kolumny
			if( this.LV_DatabaseColumns.Items.Count > 0 )
			{
				this.LV_DatabaseColumns.Items[0].Selected = true;
                this.refreshColumnRowsIFS( 0 );
			}

			this._locked = false;
		}

        /// <summary>
        /// Zamienia indeks pobierany z pola wyboru filtra na typ filtra.
        /// Funkcja używana głównie przy dodawaniu i zmienie filtra w drugim kroku na formularzu.
        /// </summary>
        /// 
        /// <returns>Typ filtra.</returns>
        /// 
        /// <seealso cref="FILTERTYPE" />
        /// <seealso cref="refreshFilters" />
        /// <seealso cref="displayColumnFilters" />
        /// <seealso cref="_filterTypeToCBIndex" />
		//* ============================================================================================================
		public FILTERTYPE getSelectedFilterType()
		{
			if( this.CB_FilterType.Items.Count == 1 )
				return FILTERTYPE.Format;

			switch( this.CB_FilterType.SelectedIndex )
			{
				case 0: return FILTERTYPE.LowerCase;
				case 1: return FILTERTYPE.UpperCase;
				case 2: return FILTERTYPE.TitleCase;
				case 3: return FILTERTYPE.Equal;
				case 4: return FILTERTYPE.NotEqual;
			}

			return FILTERTYPE.Format;
		}
        
        /// <summary>
        /// Odświeża listę dostępnych filtrów dla danej kolumny.
        /// Lista dostępnych filtrów wyświetlana jest w drugim kroku nad polami tekstowymi.
        /// Na razie dostępne są tylko filtry dla typów tekstowych.
        /// Kolumny o różnych typach mogą posiadać różne dostępne filtry.
        /// </summary>
        /// 
        /// <param name="selected">Aktualnie zaznaczona kolumna.</param>
        /// 
        /// <seealso cref="displayColumnFilters"/>
        /// <seealso cref="getSelectedFilterType"/>
		//* ============================================================================================================
		protected void refreshFilters( int selected )
		{
			int newindex = -1,
				subindex = -1;
			
			// jeżeli jest to nowa kolumna, oblicz prawdziwy indeks kolumny i indeks podrzędny
			if( selected >= this._storage.ColumnsNumber )
				newindex = this._filter.calcNewColumnIndex( selected, out subindex );

			this._realNewIndex  = newindex;
			this._realSubIndex  = subindex;
			
			// tłumaczenia
			var values = Language.GetLines( "EditColumns", "FilterType" );

			// tylko format (nowa kolumna)
			if( newindex != -1 && subindex == -1 )
			{
				this.CB_FilterType.Items.Clear();
				this.CB_FilterType.Items.Add( values[5] );

				this.CB_FilterType.SelectedIndex = 0;

				return;
			}

			// na razie tylko tekst
			int x;

			// wyczyść i dodaj nowe typy
			this.CB_FilterType.Items.Clear();
			
			for( x = 0; x < 5; ++x )
				this.CB_FilterType.Items.Add( values[x] );

			this.CB_FilterType.SelectedIndex = 0;
		}

		/// <summary>
		/// Wyświetla filtry utworzone dla wybranej kolumny.
        /// Filtry wyświetlane są w drugim kroku pod ustawieniami filtra w środkowej kolumnie.
        /// Każda kolumna może posiadać inne filtry, nawet te kopiowane - w tym przypadku mogą być nawet dziedziczone.
		/// </summary>
        /// 
        /// <seealso cref="refreshFilters"/>
        /// <seealso cref="getSelectedFilterType"/>
		//* ============================================================================================================
		protected void displayColumnFilters()
		{
			var values = Language.GetLines( "EditColumns", "FilterType" );

			this.LV_FilterList.Items.Clear();

			if( this._realNewIndex != -1 )
			{
				// filtry dziedziczone (nie można ich usunąć)
                if( this._realSubIndex != -1 )
                {
                    int inhcol = this._filter.SubColumns[this._realNewIndex][this._realSubIndex];

                    for( int x = 0, y = this._filter.Filter[inhcol].Count; x < y; ++x )
                    {
                        if( this._filter.Filter[inhcol][x].FilterCopy )
                            this.LV_FilterList.Items.Add( "# " + values[(int)this._filter.Filter[inhcol][x].Filter] );
                    }
                }

				// filtry przypisane tylko do wybranej kolumny
				for( int x = 0, y = this._filter.Filter[this._currentColumn].Count; x < y; ++x )
				{
					var filter = this._filter.Filter[this._currentColumn][x];

					// tylko format i aż format
					if( this._realSubIndex == -1 && filter.SubColumn == -1 )
					{
						this.LV_FilterList.Items.Add( values[(int)FILTERTYPE.Format] );
						break;
					}
					else if( this._realSubIndex != -1 && filter.SubColumn == this._realSubIndex )
					{
						this.LV_FilterList.Items.Add( values[(int)filter.Filter] );
					}
				}

				// zaznacz pierwszy filtr gdy są dostępne
				if( this.LV_FilterList.Items.Count > 0 )
				{
					this.LV_FilterList.SelectedIndices.Clear();
					this.LV_FilterList.SelectedIndices.Add( 0 );
				}
                else
                {
                    this.CB_FilterType.SelectedIndex = 0;
                    this.TB_Modifier.Text            = "";
                    this.TB_Result.Text              = "";
                    this.CB_AllCopies.Checked        = false;
                    this.CB_Exclude.Checked          = false;
                }
				return;
			}

			// filtry przypisane tylko do wybranej kolumny
			for( int x = 0, y = this._filter.Filter[this._currentColumn].Count; x < y; ++x )
			{
				FilterInfo filter = this._filter.Filter[this._currentColumn][x];

				this.LV_FilterList.Items.Add( values[(int)filter.Filter] );
			}

			// zaznacz pierwszy filtr gdy są dostępne
			if( this.LV_FilterList.Items.Count > 0 )
			{
				this.LV_FilterList.SelectedIndices.Clear();
				this.LV_FilterList.SelectedIndices.Add( 0 );
			}
            else
            {
                this.CB_FilterType.SelectedIndex = 0;
                this.TB_Modifier.Text            = "";
                this.TB_Result.Text              = "";
                this.CB_AllCopies.Checked        = false;
                this.CB_Exclude.Checked          = false;
            }
		}
        
        /// <summary>
        /// Odświeża listę wierszy w kontrolce z podanej kolumny w drugim kroku.
        /// ISS - skrót od In Second Step.
        /// Funkcja używana głównie podczas zmiany danych w liście z filtrami.
        /// </summary>
        /// 
        /// <param name="index">Indeks kolumny z której będą wyświetlane wiersze.</param>
		//* ============================================================================================================
        public void refreshColumnRowsISS( int index )
        {
			// zaznaczony element
			var item  = this.LV_AllColumns.SelectedItems[0];
			int s2rnl = Settings.Info.ECF_RowsNumberS2;
			int limit = this._storage.RowsNumber > s2rnl ? s2rnl : this._storage.RowsNumber;

			// pobierz przefiltrowane dane
			var rows = this._filter.getRows( index, limit );

			// wyświetl wiersze z podanej kolumny
			this.LV_PreviewAllRows.Items.Clear();
			for( int x = 0; x < rows.Count; ++x )
				this.LV_PreviewAllRows.Items.Add( rows[x] );

			// zmień nagłowek tabeli
			this.CB_RecordPreview.Text = Language.GetLine( "EditColumns", "Headers", 2 ) +
				" [" + (index >= this._storage.ColumnsNumber
					? this._filter.NewColumns[index - this._filter.Storage.ColumnsNumber]
					: item.Text)
				+ "]";
        }
        
        /// <summary>
        /// Odświeża listę wierszy w kontrolce z podanej kolumny w pierwszym kroku.
        /// IFS - skrót od In First Step.
        /// Funkcja używana głównie podczas przełączania z jednego kroku na drugi.
        /// </summary>
        /// 
        /// <param name="index">Indeks kolumny z której będą wyświetlane wiersze.</param>
		//* ============================================================================================================
        public void refreshColumnRowsIFS( int index )
        {
            int s1rnl = Settings.Info.ECF_RowsNumberS1;
            int limit = this._storage.RowsNumber > s1rnl ? s1rnl : this._storage.RowsNumber;

            // pobierz przefiltrowane dane
            var rows = this._filter.getRows( index, limit );

            // wyświetl wiersze
            this.LV_PreviewRows.Items.Clear();
            for( int x = 0; x < rows.Count; ++x )
                this.LV_PreviewRows.Items.Add( rows[x] );

			// zmień nagłowek tabeli
			this.CH_DataPreview.Text = Language.GetLine( "EditColumns", "Headers", 2 ) +
				" [" + (index >= this._storage.ColumnsNumber
					? this._filter.NewColumns[index - this._filter.Storage.ColumnsNumber]
					: this._storage.Column[index])
				+ "]";
        }
        
        /// <summary>
        /// Zamienia podany typ filtra na indeks dla pola wyboru.
        /// Funkcja używana przy konwersji filtra na indeks dla kontrolki zawierającej listę dostępnych filtrów.
        /// </summary>
		/// 
		/// <param name="filter">Typ filtra do zamiany na indeks.</param>
        /// 
        /// <returns>Indeks dla pola wyboru.</returns>
        /// 
        /// <seealso cref="getSelectedFilterType" />
		//* ============================================================================================================
        private int _filterTypeToCBIndex( FILTERTYPE filter )
        {
            switch( filter )
            {
                case FILTERTYPE.Format:
                case FILTERTYPE.LowerCase:
                    return 0;
                case FILTERTYPE.UpperCase:
                    return 1;
                case FILTERTYPE.TitleCase:
                    return 2;
                case FILTERTYPE.Equal:
                    return 3;
                case FILTERTYPE.NotEqual:
                    return 4;
            }
            return 0;
        }
        
#endregion

#region PRZYCISKI - KREATOR KOLUMN
        /// @cond EVENTS

        /// <summary>
        /// Akcja wywoływana po kliknięciu w przycisk dodawania kolumny.
        /// Przycisk znajduje się w pierwszym kroku w kreatorze kolumn.
        /// Sprawdza czy kolumna o podanej nazwie już istnieje.
        /// </summary>
        /// 
        /// <param name="sender">Obiekt wywołujący zdarzenie.</param>
        /// <param name="ev">Argumenty zdarzenia.</param>
        /// 
        /// <seealso cref="B_DeleteColumn_Click" />
        /// <seealso cref="B_ClearColumn_Click" />
		//* ============================================================================================================
		private void B_AddColumn_Click( object sender, EventArgs ev )
		{
			// usuń białe znaki z przodu i z tyłu
			string text  = this.TB_ColumnName.Text.Trim();

			// brak nazwy kolumny
			if( text == "" )
			{
				Program.LogInfo
				(
					Language.GetLine( "EditColumns", "Messages", 1 ),
					Language.GetLine( "MessageNames", (int)LANGCODE.GFN_EDITCOLUMNS )
				);
				return;
			}

			// sprawdź czy kolumna o tej nazwie została już dodana
			foreach( ListViewItem item in this.LV_NewColumns.Items )
				if( item.SubItems[0].Text == text )
				{
					Program.LogInfo
					(
						String.Format( Language.GetLine("EditColumns", "Messages", 2), item.Text ),
						Language.GetLine( "MessageNames", (int)LANGCODE.GFN_EDITCOLUMNS )
					);
					return;
				}

			// sprawdź czy kolumna już istnieje, jeżeli tak, wyświelt ostrzeżenie o jej nadpisaniu
			foreach( string column in this._storage.Column )
				if( column == text )
				{
					DialogResult result = MessageBox.Show
					(
						this,
						String.Format( Language.GetLine("EditColumns", "Messages", 3), column ),
						Language.GetLine( "MessageNames", (int)LANGCODE.GFN_EDITCOLUMNS ),
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
			
			var list = new List<int>();

			// dodaj kolumnę
			var newitem = this.LV_NewColumns.Items.Add( text );
			this.LV_NewColumns.Items[newitem.Index].SubItems.Add("").Tag = list;

			// [1]: indeks kolumny (0 -> lvAllColumns.Count)
			// [2]: indeks rodzica nowej kolumny (stream.ColumnNumber -> lvAllColumns.ColumnNumber)
			int[] info = { this.LV_DatabaseColumns.Items.Count + newitem.Index, -1 };

			// kolumna w tabeli z wszystkimi kolumnami
			newitem = this.LV_AllColumns.Items.Add( text );
			this.LV_AllColumns.Items[newitem.Index].Tag = info;
			this.LV_AllColumns.Items[newitem.Index].Group = this.LV_AllColumns.Groups[1];

			// dodaj kolumnę do filtrów
			this._filter.addColumn( text );

			// wyczyść nazwę nowej kolumny w kontrolce tekstu
			this.TB_ColumnName.Text = "";

#		if DEBUG
			Program.LogMessage( "Dodano nową kolumnę: " + text + "." );
#		endif
		}
        
        /// <summary>
        /// Akcja wywoływana po kliknięciu w przycisk usuwania kolumny.
        /// Przycisk znajduje się w pierwszym kroku w kreatorze kolumn.
        /// Funkcja usuwa zaznaczoną na liście kolumnę.
        /// Możliwe jest usuwanie tylko i wyłącznie nowo utworzonych kolumn.
        /// </summary>
        /// 
        /// <param name="sender">Obiekt wywołujący zdarzenie.</param>
        /// <param name="ev">Argumenty zdarzenia.</param>
        /// 
        /// <seealso cref="B_AddColumn_Click" />
        /// <seealso cref="B_ClearColumn_Click" />
		//* ============================================================================================================
		private void B_DeleteColumn_Click( object sender, EventArgs ev )
		{
			if( this.LV_NewColumns.SelectedItems.Count == 0 )
				return;

			var item = this.LV_NewColumns.SelectedItems[0];

			for( int x = this.LV_AllColumns.Items.Count - 1; x >= 0; --x )
			{
				if( this.LV_AllColumns.Items[x].Tag == null )
					continue;

				int[] info = (int[])this.LV_AllColumns.Items[x].Tag;

				// usuń wszystkie podkolumny danej kolumny wraz z kolumną
				if( info[1] == item.Index || info[0] == item.Index + this.LV_DatabaseColumns.Items.Count )
					this.LV_AllColumns.Items.RemoveAt( x );
			}

            // zaznacz pierwszą kolumnę ze schowka po usunięciu nowej jeżeli była ona zaznaczona któraś z nowych kolumn
            // gdy sprawdzimy tylko czy była zaznaczona aktualna, może się wysypać przez indeksy
            if( this._realNewIndex != -1 || this._realSubIndex != -1 )
            {
                this.LV_AllColumns.SelectedIndices.Clear();
                this.LV_AllColumns.SelectedIndices.Add( 0 );
            }

            // usuń kolumnę z filtrów
			this._filter.removeColumn( item.Index );
			this.LV_NewColumns.Items.Remove( item );

#			if DEBUG
				Program.LogMessage( "Usunięto kolumnę o nazwie: " + item.Text + "." );
#			endif

			// pozbieraj śmieci
			GC.Collect();
		}

        /// <summary>
        /// Akcja wywoływana po kliknięciu w przycisk czyszczenia kolumny.
        /// Przycisk znajduje się w pierwszym kroku w kreatorze kolumn.
        /// Przy kliknięciu czyszczone są podkolumny z których składa się kolumna wraz z wszystkimi filtrami.
        /// </summary>
        /// 
        /// <param name="sender">Obiekt wywołujący zdarzenie.</param>
        /// <param name="ev">Argumenty zdarzenia.</param>
        /// 
        /// <seealso cref="B_AddColumn_Click" />
        /// <seealso cref="B_DeleteColumn_Click" />
		//* ============================================================================================================
		private void B_ClearColumn_Click( object sender, EventArgs ev )
		{
			if( this.LV_NewColumns.SelectedItems.Count == 0 )
				return;

			var item = this.LV_NewColumns.SelectedItems[0];

			// wyczyść podkolumny
			((List<int>)item.SubItems[1].Tag).Clear();
			item.SubItems[1].Text = "";

			for( int x = this.LV_AllColumns.Items.Count - 1; x >= 0; --x )
			{
				if( this.LV_AllColumns.Items[x].Tag == null )
					continue;

				int[] info = (int[])this.LV_AllColumns.Items[x].Tag;

				// usuń wszystkie podkolumny danej kolumny
				if( info[1] == item.Index )
					this.LV_AllColumns.Items.RemoveAt( x );
			}

			// usuń z filtrów
			this._filter.removeSubColumns( item.Index );

#			if DEBUG
				Program.LogMessage( "Kolumna o nazwie " + item.Text + " została wyczyszczona." );
#			endif

			// pozbieraj śmieci
			GC.Collect();
		}

#endregion

#region OBSŁUGA LIST - KREATOR KOLUMN

        /// <summary>
        /// Akcja wywoływana podczas wpisywania nazwy kolumny w pole tekstowe.
        /// Podczas wpisywania przechwytywane są znaki, dzięki czemu funkcja sprawdza poprawność nowych danych.
        /// W przypadku wpisania niedozwolonego znaku wyświetla popup i informuje sygnałem o błędzie.
        /// </summary>
        /// 
        /// <param name="sender">Obiekt wywołujący zdarzenie.</param>
        /// <param name="ev">Argumenty zdarzenia.</param>
        /// 
        /// <seealso cref="TP_Tooltip_Draw" />
		//* ============================================================================================================
		private void TB_ColumnName_KeyPress( object sender, KeyPressEventArgs ev )
		{
			// backspace i ctrl
			if( ev.KeyChar == 8 || ModifierKeys == Keys.Control )
				return;

			// sprawdź czy nazwa zawiera niedozwolone znaki
            var lang_chars   = Language.GetLines( "Locale" );
			var locale_chars = lang_chars[(int)LANGCODE.GLO_BIGCHARS] + lang_chars[(int)LANGCODE.GLO_SMALLCHARS];
			var regex        = new Regex( @"^[0-9a-zA-Z" + locale_chars + @" .\-+_]+$" );
			if( !regex.IsMatch(ev.KeyChar.ToString()) )
			{
				// pokaż informacje o dozwolonych znakach
				if( !this._showTooltip )
				{
					this.TP_Tooltip.Show
					(
						Language.GetLine( "EditColumns", "Messages", 0 ),
						this.TB_ColumnName,
						new Point( 0, this.TB_ColumnName.Height + 2 )
					);
					this._showTooltip = true;
				}

				// dźwięk systemowy
				ev.Handled = true;
				System.Media.SystemSounds.Beep.Play();
				
				return;
			}

			// schowaj informacje o dozwolonych znakach
			if( this._showTooltip )
			{
				this.TP_Tooltip.Hide( this.TB_ColumnName );
				this._showTooltip = false;
			}
		}

        /// <summary>
        /// Akcja wywoływana podczas zmiany zaznaczonej pozycji na liście kolumn.
        /// Lista znajduje się w pierwszym kroku z prawej strony, zawiera kolumny znajdujące się w schowku.
        /// Podczas wyświetlania uwzględnia utworzone wczesniej filtry.
        /// </summary>
        /// 
        /// <param name="sender">Obiekt wywołujący zdarzenie.</param>
        /// <param name="ev">Argumenty zdarzenia.</param>
        /// 
        /// <seealso cref="LV_NewColumns_SelectedIndexChanged" />
		//* ============================================================================================================
		private void LV_DatabaseColumns_SelectedIndexChanged( object sender, EventArgs ev )
		{
			if( this._locked )
				return;
			if( this.LV_DatabaseColumns.SelectedItems.Count == 0 )
				return;

			// zaznaczony element
			var item = this.LV_DatabaseColumns.SelectedItems[0];

            // odśwież rekordy z wybranej kolumny
            this.refreshColumnRowsIFS( item.Index );

#		if DEBUG
			Program.LogMessage( "Wczytano wiersze z kolumny: '" + item.Text + "'." );
#		endif
		}

        /// <summary>
        /// Akcja wywoływana podczas zmiany zaznaczonej pozycji na liście nowych kolumn.
        /// Lista znajduje się w pierwszym kroku z lewej strony nad przyciskami służącymi do dodawania kolumn.
        /// Podczas wyświetlania uwzględniane są utworzone wcześniej w drugim kroku filtry.
        /// </summary>
        /// 
        /// <param name="sender">Obiekt wywołujący zdarzenie.</param>
        /// <param name="ev">Argumenty zdarzenia.</param>
        /// 
        /// <seealso cref="LV_DatabaseColumns_SelectedIndexChanged" />
		//* ============================================================================================================
		private void LV_NewColumns_SelectedIndexChanged( object sender, EventArgs ev )
		{
			// blokada
			if( this._locked )
				return;

			if( this.LV_NewColumns.SelectedItems.Count == 0 )
			{
				this.B_ClearColumn.Enabled  = false;
				this.B_DeleteColumn.Enabled = false;

				return;
			}

			// zaznaczony element
			var item = this.LV_NewColumns.SelectedItems[0];
    
            // wyświetl wiersze
            this.refreshColumnRowsIFS( item.Index + this._storage.ColumnsNumber );

			this.B_ClearColumn.Enabled  = true;
			this.B_DeleteColumn.Enabled = true;
		}

        /// <summary>
        /// Akcja wywoływana po naciśnięciu przycisku na elemencie w liście kolumn.
        /// Po kliknięciu w nazwę kolumny ze schowka możliwe jest dzięki temu jej przeniesienie do nowych kolumn.
        /// Ułatwia to w znacznym stopniu możliwość łączenia kolumn.
        /// Drag & Drop.
        /// </summary>
        /// 
        /// <param name="sender">Obiekt wywołujący zdarzenie.</param>
        /// <param name="ev">Argumenty zdarzenia.</param>
        /// 
        /// <seealso cref="dragDropEffects_Move" />
        /// <seealso cref="lvNewColumns_DragOver" />
        /// <seealso cref="lvNewColumns_DragDrop" />
		//* ============================================================================================================
		private void LV_DatabaseColumns_MouseDown( object sender, MouseEventArgs ev )
		{
			// pobierz element pod wskaźnikiem myszy
			var item = this.LV_DatabaseColumns.GetItemAt( ev.X, ev.Y );

			if( item == null )
				return;

			// zapobiega podwójnemu wczytywaniu danych
			this._locked = item.Index == 0 && this.LV_PreviewRows.Items.Count == 0 ? false : true;
			item.Selected = true;
			this._locked = false;

			// przenieś-upuść
			this.LV_DatabaseColumns.DoDragDrop( item, DragDropEffects.Move );
		}

        /// <summary>
        /// Akcja wywoływana podczas przemieszczania elementu.
        /// Wyświetlany jest efekt przemieszczania - przeważnie obok wskaźnika myszy rysowana jest dodatkowa ikona.
        /// Drag & Drop.
        /// </summary>
        /// 
        /// <param name="sender">Obiekt wywołujący zdarzenie.</param>
        /// <param name="ev">Argumenty zdarzenia.</param>
        /// 
        /// <seealso cref="lvDatabaseColumns_MouseDown" />
        /// <seealso cref="lvNewColumns_DragOver" />
        /// <seealso cref="lvNewColumns_DragDrop" />
		//* ============================================================================================================
		private void LV_DragDropEffects_Move( object sender, DragEventArgs ev )
		{
			ev.Effect = DragDropEffects.Move;
		}
		
        /// <summary>
        /// Akcja wywoływana podczas najechania na element podczas przeciągania.
        /// Dzięki tej funkcji po najechaniu podświetla się kolumna na którą wskazuje kursor myszy.
        /// Drag & Drop.
        /// </summary>
        /// 
        /// <param name="sender">Obiekt wywołujący zdarzenie.</param>
        /// <param name="ev">Argumenty zdarzenia.</param>
        /// 
        /// <seealso cref="lvDatabaseColumns_MouseDown" />
        /// <seealso cref="dragDropEffects_Move" />
        /// <seealso cref="lvNewColumns_DragDrop" />
		//* ============================================================================================================
		private void LV_NewColumns_DragOver( object sender, DragEventArgs ev )
		{
			// zaznaczona kolumna
			var point = this.LV_NewColumns.PointToClient( new Point(ev.X, ev.Y) );
			var item  = this.LV_NewColumns.GetItemAt( point.X, point.Y );

			if( item == null )
				return;

			// zablokuj
			this._locked = true;

			// zaznacz nową kolumnę
			if( item != null )
				item.Selected = true;

			this._locked = false;
		}
		
        /// <summary>
        /// Akcja wywoływana po puszczeniu klawisza myszy podczas przeciągania.
        /// Po przeciągnięciu starej kolumny na nową funkcja łączy przeciąganą z nową, dodając ją do formatu.
        /// Wyświetla informacje gdy kolumna kopiowana jest drugi raz do tej samej nowej kolumny.
        /// Drag & Drop.
        /// </summary>
        /// 
        /// <param name="sender">Obiekt wywołujący zdarzenie.</param>
        /// <param name="ev">Argumenty zdarzenia.</param>
        /// 
        /// <seealso cref="lvDatabaseColumns_MouseDown" />
        /// <seealso cref="dragDropEffects_Move" />
        /// <seealso cref="lvNewColumns_DragOver" />
		//* ============================================================================================================
		private void LV_NewColumns_DragDrop( object sender, DragEventArgs ev )
		{
			// zaznaczona kolumna
			var point = this.LV_NewColumns.PointToClient( new Point(ev.X, ev.Y) );
			var item  = this.LV_NewColumns.GetItemAt( point.X, point.Y );

			if( item == null )
				return;

			// kopiowana kolumna
			var copy = (ListViewItem)ev.Data.GetData( typeof(ListViewItem) );
			var list = (List<int>)item.SubItems[1].Tag;
            
			// Kolumna już została skopiowana - wyświetl informacje (TUTAJ!)

			// dodaj kolumnę do łączenia
			if( item.SubItems[1].Text.Length == 0 )
				item.SubItems[1].Text = copy.Text;
			else
				item.SubItems[1].Text += ", " + copy.Text;
			
            // dodaj kolumnę do filtrów
			list.Add( copy.Index );
			this._filter.addSubColumn( item.Index, copy.Index );

            // odświeżanie listy w drugim kroku
			for( int x = this.LV_AllColumns.Items.Count - 1; x >= 0; --x )
			{
				if( this.LV_AllColumns.Items[x].Tag == null )
					continue;

				int[] info = (int[])this.LV_AllColumns.Items[x].Tag;

				if( info[1] == item.Index || info[0] == item.Index + this.LV_DatabaseColumns.Items.Count )
				{
					int[] newinfo  = { copy.Index, item.Index };
					var   inserted = this.LV_AllColumns.Items.Insert( x+1, "      " + copy.Text );
					
					inserted.Tag   = newinfo;
					inserted.Group = this.LV_AllColumns.Groups[1];
					
					// odświeżenie listy (jakoś to mnie nie przekonuje), ale cóż...
					string head = this.LV_AllColumns.Groups[1].Header;
					this.LV_AllColumns.Groups[1].Header = "";
					this.LV_AllColumns.Groups[1].Header = head;

					break;
				}
			}

			this.LV_AllColumns.Update();
			this.LV_NewColumns.Focus();
		}

#endregion

#region OBSŁUGA LIST - FILTROWANIE

		/// <summary>
		/// Akcja wywoływana po zmianie wyświetlanej kolumny w filtrach.
		/// Funkcja ładuje wiersze z bazy danych przefiltrowane w sposób określony w filtrach.
        /// Po załadowaniu wierszy ładowane są wszystkie przypisane do kolumny filtry.
		/// </summary>
		/// 
		/// <param name="sender">Obiekt wywołujący zdarzenie.</param>
		/// <param name="ev">Argumenty zdarzenia</param>
		//* ============================================================================================================
		private void LV_AllColumns_SelectedIndexChanged( object sender, EventArgs ev )
		{
			int newcol_idx = -1;

			if( this._locked )
				return;

			// zakończ funkcję gdy nie zaznaczono elementu, nie zmieniaj aktualnie zaznaczonego
			if( this.LV_AllColumns.SelectedItems.Count == 0 )
				return;

			// odblokuj kontrolkę zmiany filtru
			this.CB_FilterType.Enabled = true;

			// zaznaczony element
			var item = this.LV_AllColumns.SelectedItems[0];

			// zapisz indeks
			int index = item.Index;
			
			// oblicz nowy identyfikator
			if( index >= this._storage.ColumnsNumber )
				newcol_idx = this._filter.calcNewColumnIndex( index );
			if( newcol_idx != -1 )
				index = this._filter.Storage.ColumnsNumber + newcol_idx;

			// nie odświeżaj gdy indeks kolumn ten sam
			if( this._currentColumn == index )
			{
				this.refreshFilters( item.Index );

				if( this._realNewIndex != -1 && this._realSubIndex != -1 )
                {
					this.B_AddFilter.Enabled    = true;
                    this.B_ChangeFilter.Enabled = false;
                    this.B_DeleteFilter.Enabled = false;
                }
                else if( this._realNewIndex != -1 && this._realSubIndex == -1 )
                {
                    this.B_AddFilter.Enabled    = false;
                    this.B_ChangeFilter.Enabled = true;
                    this.B_DeleteFilter.Enabled = false;
                }

                this.displayColumnFilters();
				return;
			}

            // odświeżanie listy filtrów
			this._currentColumn = index;
			this.refreshFilters( item.Index );

			if( !(this._realNewIndex != -1 && this._realSubIndex == -1) )
            {
				this.B_AddFilter.Enabled    = true;
                this.B_ChangeFilter.Enabled = false;
                this.B_DeleteFilter.Enabled = false;
            }
            else if( this._realNewIndex != -1 && this._realSubIndex == -1 )
            {
                this.B_AddFilter.Enabled    = false;
                this.B_ChangeFilter.Enabled = true;
                this.B_DeleteFilter.Enabled = false;
            }
            // odśwież listę
            this.refreshColumnRowsISS( index );

			// typ kolumny
			this.CBX_ColType.SelectedIndex = (int)this._filter.ColumnType[index].SelectedType;
            
#		if DEBUG
			// informacja o danych z kolumny
			Program.LogMessage( "Wczytano wiersze z kolumny: '" +
				(index >= this._storage.ColumnsNumber
					? this._filter.NewColumns[index - this._filter.Storage.ColumnsNumber]
					: item.Text)
				+ "'." );
#		endif

            this.displayColumnFilters();
		}

        /// <summary>
        /// Akcja wywoływana podczas zmiany filtra w liście z filtrami dla danej kolumny.
        /// Po zmienie filtra uzupełniane są kontrolki odpowiedzialne za edycję i dodawanie filtrów.
        /// </summary>
		/// 
		/// <param name="sender">Obiekt wywołujący zdarzenie.</param>
		/// <param name="ev">Argumenty zdarzenia</param>
		//* ============================================================================================================
		private void LV_FilterList_SelectedIndexChanged( object sender, EventArgs ev )
        {
            // blokuj przyciski gdy nic nie zostało zaznaczone
            if( this.LV_FilterList.SelectedIndices.Count == 0 )
            {
                this.B_ChangeFilter.Enabled = false;
                this.B_DeleteFilter.Enabled = false;

                return;
            }

            // pobierz indeks filtrowanej kolumny
            int selected  = this.LV_FilterList.SelectedIndices[0];
            int filteridx = selected;
            int inherited = this._realNewIndex != -1 && this._realSubIndex != -1
                ? this._filter.isInherited( this._realNewIndex, this._realSubIndex, selected, out filteridx )
                : -1;

            FilterInfo info;

            // pobierz filtr dla kolumny podrzędnej lub filtr dziedziczony
            if( inherited == -1 )
                info = this._filter.getFilter( this._currentColumn, this._realSubIndex, filteridx );
            else
                info = this._filter.getFilter( inherited, -1, filteridx );

            // typ filtra
            this.CB_FilterType.SelectedIndex = this._filterTypeToCBIndex( info.Filter );

            // opcje
            this.TB_Modifier.Text     = info.Modifier;
            this.TB_Result.Text       = info.Result;
            this.CB_Exclude.Checked   = info.Exclude;
            this.CB_AllCopies.Checked = info.FilterCopy;

            // przyciski
            this.B_ChangeFilter.Enabled = inherited != -1
                ? false
                : true;
            this.B_DeleteFilter.Enabled = (this._realNewIndex != -1 && this._realSubIndex == -1) || inherited != -1
                ? false
                : true;
		}
        
        /// <summary>
        /// Akcja wywoływana podczas zmiany typu filtra.
        /// Po zmianie typu czyszczone są kontrolki dotyczące filtrów i wyłączane gdy filtr z nich nie korzysta.
        /// </summary>
		/// 
		/// <param name="sender">Obiekt wywołujący zdarzenie.</param>
		/// <param name="ev">Argumenty zdarzenia</param>
		//* ============================================================================================================
		private void CB_FilterType_SelectedIndexChanged( object sender, EventArgs ev )
		{
			switch( this.CB_FilterType.SelectedIndex )
			{
				// duże litery / format
				case 0:
                    this.TB_Result.Enabled  = false;
                    this.CB_Exclude.Enabled = false;

					// to tylko format
					if( this.CB_FilterType.Items.Count == 1 )
                    {
						this.TB_Modifier.Enabled  = true;
                        this.CB_AllCopies.Enabled = false;
					}
                    // a tu duże litery, tylko dla starych kolumn
                    else if( this._realNewIndex == -1 )
                    {
                        this.TB_Modifier.Enabled  = false;
						this.CB_AllCopies.Enabled = true;
                    }
                    // i duże litery dla kolumn podrzędnych nowych kolumn
                    else
                    {
                        this.TB_Modifier.Enabled  = false;
                        this.CB_AllCopies.Enabled = false;
                    }
				break;
				// małe litery / nazwa własna
				case 1:
				case 2:
                    this.TB_Modifier.Enabled = false;
                    this.TB_Result.Enabled   = false;
                    this.CB_Exclude.Enabled  = false;

					if( this._realNewIndex == -1 )
						this.CB_AllCopies.Enabled = true;
				break;
				// równy / różny
				case 3:
				case 4:
					this.TB_Result.Enabled   = true;
					this.TB_Modifier.Enabled = true;
					this.CB_Exclude.Enabled  = true;

					if( this._realNewIndex == -1 )
						this.CB_AllCopies.Enabled = true;
				break;
			}
		}
        
        /// <summary>
        /// Akcja wywoływana podczas zaznaczenia wykluczenia.
        /// Wykluczenie polega na usunięciu wszystkich rekordów spełniających podane wyżej kryteria.
        /// W tym przypadku czyszczone i wyłączane jest pole wyniku, nie jest ono potrzebne.
        /// </summary>
		/// 
		/// <param name="sender">Obiekt wywołujący zdarzenie.</param>
		/// <param name="ev">Argumenty zdarzenia</param>
		//* ============================================================================================================
        private void CB_Exclude_CheckedChanged( object sender, EventArgs ev )
        {
            if( this.CB_Exclude.Checked )
            {
                this.TB_Result.Text    = "";
                this.TB_Result.Enabled = false;
            }
            else
                this.TB_Result.Enabled = true;
        }

#endregion

#region PRZYCISKI - FILTROWANIE

        /// <summary>
        /// Akcja wywoływana podczas dodawania nowego filtru.
        /// Dodaje filtr do listy dla wybranej kolumny i odświeża wyświetlane wiersze uwzględniając dodany filtr.
        /// </summary>
		/// 
		/// <param name="sender">Obiekt wywołujący zdarzenie.</param>
		/// <param name="ev">Argumenty zdarzenia</param>
		//* ============================================================================================================
		private void B_AddFilter_Click( object sender, EventArgs ev )
		{
			FilterInfo filter = this._filter.createFilter
			(
				this.getSelectedFilterType(),
				this.TB_Modifier.Text,
				this.TB_Result.Text,
				this.CB_Exclude.Checked,
				this.CB_AllCopies.Checked && this._realSubIndex == -1
			);
			this._filter.addFilter( this._currentColumn, this._realSubIndex, filter );

#		if DEBUG
			Program.LogMessage( "Dodano nowy filtr: Kolumna(#" + this._currentColumn + ")" +
                (this._realSubIndex != -1 ? (", Rząd(#" + this._realSubIndex + "), ") : "") );
#		endif
            // odśwież listę wierszy
            this.refreshColumnRowsISS( this._currentColumn );
            
            // wyświetl listę filtrów
            this.displayColumnFilters();
		}
        
        /// <summary>
        /// Akcja wywoływana po kliknięciu w przycisk usuwania filtra.
        /// Usuwa filtr z listy w tabeli i klasie z filtrami i odświeża listę wyświetlanych wierszy z wybranej kolumny.
        /// </summary>
		/// 
		/// <param name="sender">Obiekt wywołujący zdarzenie.</param>
		/// <param name="ev">Parametry zdarzenia</param>
		//* ============================================================================================================
        private void B_DeleteFilter_Click( object sender, EventArgs ev )
        {
            if( this.LV_FilterList.SelectedIndices.Count == 0 )
                return;

            int selected = this.LV_FilterList.SelectedIndices[0];

            // usuń filtr
            this._filter.removeFilter( this._currentColumn, this._realSubIndex, selected );

#		if DEBUG
			Program.LogMessage( "Usunięto filtr: Kolumna(#" + this._currentColumn + "), " +
                (this._realSubIndex != -1 ? ("Rząd(#" + this._realSubIndex + "), ") : "") +
                "Filtr(#" + this.LV_FilterList.SelectedIndices[0] + ")" );
#		endif
            // odśwież listę wierszy
            this.refreshColumnRowsISS( this._currentColumn );

            // odśwież listę filtrów
            this.displayColumnFilters();
        }

        /// <summary>
        /// Akcja wywoływana po kliknięciu w przycisk zmiany filtra.
        /// Zamienia zaznaczony filtr z listy w tabeli na nowy, zdefiniowany nad przyciskami i odświeża listę
        /// wyświetlanych wierszy z wybranej kolumny.
        /// </summary>
		/// 
		/// <param name="sender">Obiekt wywołujący zdarzenie.</param>
		/// <param name="ev">Parametry zdarzenia</param>
		//* ============================================================================================================
        private void B_ChangeFilter_Click( object sender, EventArgs ev )
        {
            if( this.LV_FilterList.SelectedIndices.Count == 0 )
                return;

            // utwórz nowy filtr i podmień stary
			FilterInfo filter = this._filter.createFilter
			(
				this.getSelectedFilterType(),
				this.TB_Modifier.Text,
				this.TB_Result.Text,
				this.CB_Exclude.Checked,
				this.CB_AllCopies.Checked
			);

            int selected = this.LV_FilterList.SelectedIndices[0];

			this._filter.replaceFilter( this._currentColumn, this._realSubIndex, selected, filter );

#		if DEBUG
			Program.LogMessage( "Zmieniono filtr: Kolumna(#" + this._currentColumn + "), " +
                (this._realSubIndex != -1 ? ("Rząd(#" + this._realSubIndex + "), ") : "") +
                "Filtr(#" + this.LV_FilterList.SelectedIndices[0] + ")" );
#		endif
            // odśwież listę wierszy
            this.refreshColumnRowsISS( this._currentColumn );

            // odśwież listę filtrów
            this.displayColumnFilters();

            // zaznacz aktualnie edytowany filtr
            this.LV_FilterList.Items[selected].Selected = true;
        }

#endregion

#region PASEK AKCJI

        /// <summary>
        /// Akcja wywoływana po zmiane elementu w polu wyboru.
        /// Znajduje się ono w pasku akcji, służy do wyboru kroku do wyświetlania.
        /// Możliwe jest wybranie dwóch kroków - kreatora kolumn i filtrowania danych.
        /// </summary>
        /// 
        /// <param name="sender">Obiekt wywołujący zdarzenie.</param>
        /// <param name="ev">Argumenty zdarzenia.</param>
		//* ============================================================================================================
		private void CBX_Step_SelectedIndexChanged( object sender, EventArgs ev )
		{
			if( this._locked )
				return;

			if( this.CBX_Step.SelectedIndex == 0 )
			{
				this.TLP_CreateColumns.Visible   = true;
				this.LV_DatabaseColumns.Visible = true;
				this.SC_FiltersAndTypes.Visible = false;
				this.LV_AllColumns.Visible      = false;
			}
			else
			{
				this.SC_FiltersAndTypes.Visible = true;
				this.LV_AllColumns.Visible      = true;
				this.TLP_CreateColumns.Visible   = false;
				this.LV_DatabaseColumns.Visible = false;
			}
		}

        /// <summary>
        /// Analiza wciśniętych klawiszy w obrębie formularza.
        /// Funkcja tworzy skróty szybkiego dostępu do poszczególnych kroków formularza.
        /// </summary>
        /// 
        /// <param name="msg">Przechwycone zdarzenie wciśnięcia klawisza.</param>
        /// <param name="keys">Informacje o wciśniętych klawiszach.</param>
        /// 
        /// <returns>Informacja o tym czy klawisz został przechwycony.</returns>
		//* ============================================================================================================
		protected override bool ProcessCmdKey( ref Message msg, Keys keydata )
		{
			switch( keydata )
			{
			// CTRL + 1
			case Keys.Control | Keys.D1:
				this.CBX_Step.SelectedIndex = 0;
			break;
			// CTRL + 2
			case Keys.Control | Keys.D2:
				this.CBX_Step.SelectedIndex = 1;
			break;
			default:
				return base.ProcessCmdKey( ref msg, keydata );
			}

			return true;
		}

        /// <summary>
        /// Akcja wywoływana podczas rysowania okna z podpowiedzią lub błędem.
        /// Umożliwia własne rysowanie okna.
        /// </summary>
        /// 
        /// <param name="sender">Obiekt wywołujący zdarzenie.</param>
        /// <param name="ev">Argumenty zdarzenia.</param>
		//* ============================================================================================================
		private void TP_Tooltip_Draw( object sender, DrawToolTipEventArgs ev )
		{
			ev.DrawBackground();
			ev.DrawBorder();
			ev.DrawText( TextFormatFlags.VerticalCenter );
		}

        /// <summary>
        /// Akcja wywoływana podczas ukrywania okna podpowiedzi.
        /// Zapobiega wyświetlaniu błędów występujących podczas ukrywania okna.
        /// </summary>
        /// 
        /// <param name="sender">Obiekt wywołujący zdarzenie.</param>
        /// <param name="ev">Argumenty zdarzenia.</param>
		//* ============================================================================================================
		private void TP_Tooltip_Hide( object sender, EventArgs ev )
		{
			if( this._showTooltip )
			{
				this.TP_Tooltip.Hide( this.TB_ColumnName );
				this._showTooltip = false;
			}
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
        /// Akcja wywoływana podczas kliknięcia w przycisk zapisania zmian.
        /// Funkcja pozwala na zapisanie wszystkich filtrów do schowka - na razie zmiany zapisane są nieodwracalne.
        /// Usuwa kolumny które zostały odznaczone w liście kolumn istniejących w schowku.
        /// </summary>
        /// 
        /// <param name="sender">Obiekt wywołujący zdarzenie.</param>
        /// <param name="ev">Argumenty zdarzenia.</param>
        /// 
        /// <seealso cref="B_Cancel_Click" />
		//* ============================================================================================================
        private void B_Save_Click( object sender, EventArgs ev )
        {
            this._locked = true;

            // aktywne kolumny
            var columns = new List<bool>();

            foreach( ListViewItem item in this.LV_DatabaseColumns.Items )
                columns.Add( item.Checked );

            // zastosuj filtry i zapisz
            this._filter.setActiveColumns( columns );
            this._filter.applyFilters();

            this._locked = false;
            
            if( !this._storage.Ready )
                this.DialogResult = DialogResult.Abort;
            else
                this.DialogResult = DialogResult.OK;

            this.Close();
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
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        /// @endcond
#endregion
    }
}
