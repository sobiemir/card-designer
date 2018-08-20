///
/// $i03 EditColumnsForm.cs
/// 
/// Okno edycji kolumn bazy danych.
/// Uruchamia okno tworzenia kolumn i filtrowania danych.
/// Plik zawiera formularz operujący tylko i wyłącznie na kolumnach.
/// Modyfikacja danych odbywa się za pomocą innego formularza.
/// 
/// Autor: Kamil Biały
/// Od wersji: 0.8.x.x
/// Ostatnia zmiana: 2016-08-28
/// 
/// CHANGELOG:
/// [04.09.2015] Wersja początkowa.
/// [19.02.2016] Nowa wersja edytora kolumn.
///              Zmieniona koncepcja - przeniesienie filtrowania kolumn do edytora.
/// [06.07.2016] Wdrażanie tłumaczeń, regiony, komentarze.
/// [15.08.2016] Podstawowe filtrowanie, wyświetlanie rezultatów filtrów.
/// [27.08.2016] Zapis filtrów do strumienia po kliknięciu w przycisk.
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
		private bool _show_tooltip;

		/// <summary>Aktualnie zaznaczona kolumna w sekcji z filtrami (dla nowych filtrów).</summary>
		private int _current_column;

		/// <summary>Indeks nowo utworzonej kolumny, liczony od 0.</summary>
		private int _real_newindex;
		
		/// <summary>Indeks kolumny podrzędnej liczony od 0.</summary>
		private int _real_subindex;

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
            this._show_tooltip = false;
            this._locked       = false;

            this._current_column = -1;
            this._real_newindex  = -1;
            this._real_subindex  = -1;

			// zaznacz pierwszy krok
			this.cbStep.SelectedIndex = 0;

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
			this.lvcColumnName.Text    = values[(int)LANGCODE.I03_HEA_COLUMNNAME];
			this.lvcJoinedColumns.Text = values[(int)LANGCODE.I03_HEA_COLUMNS];
			this.lvcDataPreview.Text   = values[(int)LANGCODE.I03_HEA_ROWS];
			this.lvcRecordPreview.Text = values[(int)LANGCODE.I03_HEA_ROWS];
			this.lvcColumns.Text       = values[(int)LANGCODE.I03_HEA_AVALIABLE];
			this.lvcFilterList.Text    = values[(int)LANGCODE.I03_HEA_FILTERS];

			this.lvAllColumns.Groups[0].Header = values[(int)LANGCODE.I03_HEA_OLDCOLUMNS];
			this.lvAllColumns.Groups[1].Header = values[(int)LANGCODE.I03_HEA_NEWCOLUMNS];

			// kroki
			values = Language.GetLines( "EditColumns", "Steps" );
			for( int x = 0; x < this.cbStep.Items.Count; ++x )
				this.cbStep.Items[x] = (object)values[x];

			// przyciski
			values = Language.GetLines( "EditColumns", "Buttons" );
			this.bAddColumn.Text     = values[(int)LANGCODE.I03_BUT_ADD];
			this.bClearColumn.Text   = values[(int)LANGCODE.I03_BUT_CLEAR];
			this.bDeleteColumn.Text  = values[(int)LANGCODE.I03_BUT_REMOVE];
			this.bSave.Text          = values[(int)LANGCODE.I03_BUT_SAVE];
			this.bCancel.Text        = values[(int)LANGCODE.I03_BUT_CANCEL];
			this.bAddFilter.Text     = values[(int)LANGCODE.I03_BUT_ADD];
			this.bChangeFilter.Text  = values[(int)LANGCODE.I03_BUT_CHANGE];
			this.bDeleteFilter.Text  = values[(int)LANGCODE.I03_BUT_REMOVE];
			this.bChangeColType.Text = values[(int)LANGCODE.I03_BUT_CHANGETYPE];
			this.bTypeSettings.Text  = values[(int)LANGCODE.I03_BUT_ADVANCED];

			// napisy na formularzu
			values = Language.GetLines( "EditColumns", "Labels" );
			this.gbColumnType.Text   = values[(int)LANGCODE.I03_LAB_COLUMNTYPE];
			this.gbFilterConfig.Text = values[(int)LANGCODE.I03_LAB_FILTERCONF];
			this.lFilterType.Text    = values[(int)LANGCODE.I03_LAB_FILTERTYPE];
			this.lModifier.Text      = values[(int)LANGCODE.I03_LAB_MODIFIER];
			this.lResult.Text        = values[(int)LANGCODE.I03_LAB_RESULT];
			this.cbExclude.Text      = values[(int)LANGCODE.I03_LAB_EXCLUDE];
			this.cbAllCopies.Text    = values[(int)LANGCODE.I03_LAB_APPLYCOPY];

			// lista typów dla kolumn
			values = Language.GetLines( "EditColumns", "ColumnTypes" );
			this.cbColType.Items.Clear();
			for( int x = 0; x < values.Count - 1; ++x )
				this.cbColType.Items.Add( values[x] );

			// zapisane ustawienia typów
			this.cbSaved.Items.Add( values[values.Count-1] );

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
			this.lvDatabaseColumns.Items.Clear();
			this.lvAllColumns.Items.Clear();

			// uzupełnij tabele nazwami kolumn
			for( int x = 0; x < this._storage.ColumnsNumber; ++x )
			{
				this.lvDatabaseColumns.Items.Add( this._storage.Column[x] );
				this.lvDatabaseColumns.Items[x].Checked = true;

				this.lvAllColumns.Items.Add( this._storage.Column[x] );
				this.lvAllColumns.Items[x].Tag = null;
				this.lvAllColumns.Items[x].Group = this.lvAllColumns.Groups[0];
			}

			// wyświetl wiersze pierwszej kolumny
			if( this.lvDatabaseColumns.Items.Count > 0 )
			{
				this.lvDatabaseColumns.Items[0].Selected = true;
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
			if( this.cbFilterType.Items.Count == 1 )
				return FILTERTYPE.Format;

			switch( this.cbFilterType.SelectedIndex )
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
				newindex = this._filter.CalcNewColumnIndex( selected, out subindex );

			this._real_newindex  = newindex;
			this._real_subindex  = subindex;
			
			// tłumaczenia
			var values = Language.GetLines( "EditColumns", "FilterType" );

			// tylko format (nowa kolumna)
			if( newindex != -1 && subindex == -1 )
			{
				this.cbFilterType.Items.Clear();
				this.cbFilterType.Items.Add( values[5] );

				this.cbFilterType.SelectedIndex = 0;

				return;
			}

			// na razie tylko tekst
			int x;

			// wyczyść i dodaj nowe typy
			this.cbFilterType.Items.Clear();
			
			for( x = 0; x < 5; ++x )
				this.cbFilterType.Items.Add( values[x] );

			this.cbFilterType.SelectedIndex = 0;
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

			this.lvFilterList.Items.Clear();

			if( this._real_newindex != -1 )
			{
				// filtry dziedziczone (nie można ich usunąć)
                if( this._real_subindex != -1 )
                {
                    int inhcol = this._filter.SubColumns[this._real_newindex][this._real_subindex];

                    for( int x = 0, y = this._filter.Filter[inhcol].Count; x < y; ++x )
                    {
                        if( this._filter.Filter[inhcol][x].FilterCopy )
                            this.lvFilterList.Items.Add( "# " + values[(int)this._filter.Filter[inhcol][x].Filter] );
                    }
                }

				// filtry przypisane tylko do wybranej kolumny
				for( int x = 0, y = this._filter.Filter[this._current_column].Count; x < y; ++x )
				{
					var filter = this._filter.Filter[this._current_column][x];

					// tylko format i aż format
					if( this._real_subindex == -1 && filter.SubColumn == -1 )
					{
						this.lvFilterList.Items.Add( values[(int)FILTERTYPE.Format] );
						break;
					}
					else if( this._real_subindex != -1 && filter.SubColumn == this._real_subindex )
					{
						this.lvFilterList.Items.Add( values[(int)filter.Filter] );
					}
				}

				// zaznacz pierwszy filtr gdy są dostępne
				if( this.lvFilterList.Items.Count > 0 )
				{
					this.lvFilterList.SelectedIndices.Clear();
					this.lvFilterList.SelectedIndices.Add( 0 );
				}
                else
                {
                    this.cbFilterType.SelectedIndex = 0;
                    this.tbModifier.Text            = "";
                    this.tbResult.Text              = "";
                    this.cbAllCopies.Checked        = false;
                    this.cbExclude.Checked          = false;
                }
				return;
			}

			// filtry przypisane tylko do wybranej kolumny
			for( int x = 0, y = this._filter.Filter[this._current_column].Count; x < y; ++x )
			{
				FilterInfo filter = this._filter.Filter[this._current_column][x];

				this.lvFilterList.Items.Add( values[(int)filter.Filter] );
			}

			// zaznacz pierwszy filtr gdy są dostępne
			if( this.lvFilterList.Items.Count > 0 )
			{
				this.lvFilterList.SelectedIndices.Clear();
				this.lvFilterList.SelectedIndices.Add( 0 );
			}
            else
            {
                this.cbFilterType.SelectedIndex = 0;
                this.tbModifier.Text            = "";
                this.tbResult.Text              = "";
                this.cbAllCopies.Checked        = false;
                this.cbExclude.Checked          = false;
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
			var item  = this.lvAllColumns.SelectedItems[0];
			int s2rnl = Settings.Info.ECF_RowsNumberS2;
			int limit = this._storage.RowsNumber > s2rnl ? s2rnl : this._storage.RowsNumber;

			// pobierz przefiltrowane dane
			var rows = this._filter.GetRows( index, limit );

			// wyświetl wiersze z podanej kolumny
			this.lvPreviewAllRows.Items.Clear();
			for( int x = 0; x < rows.Count; ++x )
				this.lvPreviewAllRows.Items.Add( rows[x] );

			// zmień nagłowek tabeli
			this.lvcRecordPreview.Text = Language.GetLine( "EditColumns", "Headers", 2 ) +
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
            var rows = this._filter.GetRows( index, limit );

            // wyświetl wiersze
            this.lvPreviewRows.Items.Clear();
            for( int x = 0; x < rows.Count; ++x )
                this.lvPreviewRows.Items.Add( rows[x] );

			// zmień nagłowek tabeli
			this.lvcDataPreview.Text = Language.GetLine( "EditColumns", "Headers", 2 ) +
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
        /// <seealso cref="bDeleteColumn_Click" />
        /// <seealso cref="bClearColumn_Click" />
		//* ============================================================================================================
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
					Language.GetLine( "MessageNames", (int)LANGCODE.iFN_EditColumns )
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
						Language.GetLine( "MessageNames", (int)LANGCODE.iFN_EditColumns )
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
						Language.GetLine( "MessageNames", (int)LANGCODE.iFN_EditColumns ),
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
			var newitem = this.lvNewColumns.Items.Add( text );
			this.lvNewColumns.Items[newitem.Index].SubItems.Add("").Tag = list;

			// [1]: indeks kolumny (0 -> lvAllColumns.Count)
			// [2]: indeks rodzica nowej kolumny (stream.ColumnNumber -> lvAllColumns.ColumnNumber)
			int[] info = { this.lvDatabaseColumns.Items.Count + newitem.Index, -1 };

			// kolumna w tabeli z wszystkimi kolumnami
			newitem = this.lvAllColumns.Items.Add( text );
			this.lvAllColumns.Items[newitem.Index].Tag = info;
			this.lvAllColumns.Items[newitem.Index].Group = this.lvAllColumns.Groups[1];

			// dodaj kolumnę do filtrów
			this._filter.AddColumn( text );

			// wyczyść nazwę nowej kolumny w kontrolce tekstu
			this.tbColumnName.Text = "";

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
        /// <seealso cref="bAddColumn_Click" />
        /// <seealso cref="bClearColumn_Click" />
		//* ============================================================================================================
		private void bDeleteColumn_Click( object sender, EventArgs ev )
		{
			if( this.lvNewColumns.SelectedItems.Count == 0 )
				return;

			var item = this.lvNewColumns.SelectedItems[0];

			for( int x = this.lvAllColumns.Items.Count - 1; x >= 0; --x )
			{
				if( this.lvAllColumns.Items[x].Tag == null )
					continue;

				int[] info = (int[])this.lvAllColumns.Items[x].Tag;

				// usuń wszystkie podkolumny danej kolumny wraz z kolumną
				if( info[1] == item.Index || info[0] == item.Index + this.lvDatabaseColumns.Items.Count )
					this.lvAllColumns.Items.RemoveAt( x );
			}

            // zaznacz pierwszą kolumnę ze schowka po usunięciu nowej jeżeli była ona zaznaczona któraś z nowych kolumn
            // gdy sprawdzimy tylko czy była zaznaczona aktualna, może się wysypać przez indeksy
            if( this._real_newindex != -1 || this._real_subindex != -1 )
            {
                this.lvAllColumns.SelectedIndices.Clear();
                this.lvAllColumns.SelectedIndices.Add( 0 );
            }

            // usuń kolumnę z filtrów
			this._filter.RemoveColumn( item.Index );
			this.lvNewColumns.Items.Remove( item );

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
        /// <seealso cref="bAddColumn_Click" />
        /// <seealso cref="bDeleteColumn_Click" />
		//* ============================================================================================================
		private void bClearColumn_Click( object sender, EventArgs ev )
		{
			if( this.lvNewColumns.SelectedItems.Count == 0 )
				return;

			var item = this.lvNewColumns.SelectedItems[0];

			// wyczyść podkolumny
			((List<int>)item.SubItems[1].Tag).Clear();
			item.SubItems[1].Text = "";

			for( int x = this.lvAllColumns.Items.Count - 1; x >= 0; --x )
			{
				if( this.lvAllColumns.Items[x].Tag == null )
					continue;

				int[] info = (int[])this.lvAllColumns.Items[x].Tag;

				// usuń wszystkie podkolumny danej kolumny
				if( info[1] == item.Index )
					this.lvAllColumns.Items.RemoveAt( x );
			}

			// usuń z filtrów
			this._filter.RemoveSubColumns( item.Index );

#			if DEBUG
				Program.LogMessage( "Kolumna o nazwie " + item.Text + " została wyczyszczona." );
#			endif

			// pozbieraj śmieci
			GC.Collect();
		}

        /// @endcond
#endregion

#region OBSŁUGA LIST - KREATOR KOLUMN
        /// @cond EVENTS

        /// <summary>
        /// Akcja wywoływana podczas wpisywania nazwy kolumny w pole tekstowe.
        /// Podczas wpisywania przechwytywane są znaki, dzięki czemu funkcja sprawdza poprawność nowych danych.
        /// W przypadku wpisania niedozwolonego znaku wyświetla popup i informuje sygnałem o błędzie.
        /// </summary>
        /// 
        /// <param name="sender">Obiekt wywołujący zdarzenie.</param>
        /// <param name="ev">Argumenty zdarzenia.</param>
        /// 
        /// <seealso cref="tpTooltip_Draw" />
		//* ============================================================================================================
		private void tbColumnName_KeyPress( object sender, KeyPressEventArgs ev )
		{
			// backspace i ctrl
			if( ev.KeyChar == 8 || ModifierKeys == Keys.Control )
				return;

			// sprawdź czy nazwa zawiera niedozwolone znaki
			var regex = new Regex( @"^[0-9a-zA-Z" + Program.AvaliableChars + @" .\-+_]+$" );
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

        /// <summary>
        /// Akcja wywoływana podczas zmiany zaznaczonej pozycji na liście kolumn.
        /// Lista znajduje się w pierwszym kroku z prawej strony, zawiera kolumny znajdujące się w schowku.
        /// Podczas wyświetlania uwzględnia utworzone wczesniej filtry.
        /// </summary>
        /// 
        /// <param name="sender">Obiekt wywołujący zdarzenie.</param>
        /// <param name="ev">Argumenty zdarzenia.</param>
        /// 
        /// <seealso cref="lvNewColumns_SelectedIndexChanged" />
		//* ============================================================================================================
		private void lvDatabaseColumns_SelectedIndexChanged( object sender, EventArgs ev )
		{
			if( this._locked )
				return;
			if( this.lvDatabaseColumns.SelectedItems.Count == 0 )
				return;

			// zaznaczony element
			var item = this.lvDatabaseColumns.SelectedItems[0];

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
        /// <seealso cref="lvDatabaseColumns_SelectedIndexChanged" />
		//* ============================================================================================================
		private void lvNewColumns_SelectedIndexChanged( object sender, EventArgs ev )
		{
			// blokada
			if( this._locked )
				return;

			if( this.lvNewColumns.SelectedItems.Count == 0 )
			{
				this.bClearColumn.Enabled  = false;
				this.bDeleteColumn.Enabled = false;

				return;
			}

			// zaznaczony element
			var item = this.lvNewColumns.SelectedItems[0];
    
            // wyświetl wiersze
            this.refreshColumnRowsIFS( item.Index + this._storage.ColumnsNumber );

			this.bClearColumn.Enabled  = true;
			this.bDeleteColumn.Enabled = true;
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
		private void lvDatabaseColumns_MouseDown( object sender, MouseEventArgs ev )
		{
			// pobierz element pod wskaźnikiem myszy
			var item = this.lvDatabaseColumns.GetItemAt( ev.X, ev.Y );

			if( item == null )
				return;

			// zapobiega podwójnemu wczytywaniu danych
			this._locked = item.Index == 0 && this.lvPreviewRows.Items.Count == 0 ? false : true;
			item.Selected = true;
			this._locked = false;

			// przenieś-upuść
			this.lvDatabaseColumns.DoDragDrop( item, DragDropEffects.Move );
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
		private void dragDropEffects_Move( object sender, DragEventArgs ev )
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
		private void lvNewColumns_DragOver( object sender, DragEventArgs ev )
		{
			// zaznaczona kolumna
			var point = this.lvNewColumns.PointToClient( new Point(ev.X, ev.Y) );
			var item  = this.lvNewColumns.GetItemAt( point.X, point.Y );

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
		private void lvNewColumns_DragDrop( object sender, DragEventArgs ev )
		{
			// zaznaczona kolumna
			var point = this.lvNewColumns.PointToClient( new Point(ev.X, ev.Y) );
			var item  = this.lvNewColumns.GetItemAt( point.X, point.Y );

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
			this._filter.AddSubColumn( item.Index, copy.Index );

            // odświeżanie listy w drugim kroku
			for( int x = this.lvAllColumns.Items.Count - 1; x >= 0; --x )
			{
				if( this.lvAllColumns.Items[x].Tag == null )
					continue;

				int[] info = (int[])this.lvAllColumns.Items[x].Tag;

				if( info[1] == item.Index || info[0] == item.Index + this.lvDatabaseColumns.Items.Count )
				{
					int[] newinfo  = { copy.Index, item.Index };
					var   inserted = this.lvAllColumns.Items.Insert( x+1, "      " + copy.Text );
					
					inserted.Tag   = newinfo;
					inserted.Group = this.lvAllColumns.Groups[1];
					
					// odświeżenie listy (jakoś to mnie nie przekonuje), ale cóż...
					string head = this.lvAllColumns.Groups[1].Header;
					this.lvAllColumns.Groups[1].Header = "";
					this.lvAllColumns.Groups[1].Header = head;

					break;
				}
			}

			this.lvAllColumns.Update();
			this.lvNewColumns.Focus();
		}

        /// @endcond
#endregion

#region OBSŁUGA LIST - FILTROWANIE
        /// @cond EVENTS

		/// <summary>
		/// Akcja wywoływana po zmianie wyświetlanej kolumny w filtrach.
		/// Funkcja ładuje wiersze z bazy danych przefiltrowane w sposób określony w filtrach.
        /// Po załadowaniu wierszy ładowane są wszystkie przypisane do kolumny filtry.
		/// </summary>
		/// 
		/// <param name="sender">Obiekt wywołujący zdarzenie.</param>
		/// <param name="ev">Argumenty zdarzenia</param>
		//* ============================================================================================================
		private void lvAllColumns_SelectedIndexChanged( object sender, EventArgs ev )
		{
			int newcol_idx = -1;

			if( this._locked )
				return;

			// zakończ funkcję gdy nie zaznaczono elementu, nie zmieniaj aktualnie zaznaczonego
			if( this.lvAllColumns.SelectedItems.Count == 0 )
				return;

			// odblokuj kontrolkę zmiany filtru
			this.cbFilterType.Enabled = true;

			// zaznaczony element
			var item = this.lvAllColumns.SelectedItems[0];

			// zapisz indeks
			int index = item.Index;
			
			// oblicz nowy identyfikator
			if( index >= this._storage.ColumnsNumber )
				newcol_idx = this._filter.CalcNewColumnIndex( index );
			if( newcol_idx != -1 )
				index = this._filter.Storage.ColumnsNumber + newcol_idx;

			// nie odświeżaj gdy indeks kolumn ten sam
			if( this._current_column == index )
			{
				this.refreshFilters( item.Index );

				if( this._real_newindex != -1 && this._real_subindex != -1 )
                {
					this.bAddFilter.Enabled    = true;
                    this.bChangeFilter.Enabled = false;
                    this.bDeleteFilter.Enabled = false;
                }
                else if( this._real_newindex != -1 && this._real_subindex == -1 )
                {
                    this.bAddFilter.Enabled    = false;
                    this.bChangeFilter.Enabled = true;
                    this.bDeleteFilter.Enabled = false;
                }

                this.displayColumnFilters();
				return;
			}

            // odświeżanie listy filtrów
			this._current_column = index;
			this.refreshFilters( item.Index );

			if( !(this._real_newindex != -1 && this._real_subindex == -1) )
            {
				this.bAddFilter.Enabled    = true;
                this.bChangeFilter.Enabled = false;
                this.bDeleteFilter.Enabled = false;
            }
            else if( this._real_newindex != -1 && this._real_subindex == -1 )
            {
                this.bAddFilter.Enabled    = false;
                this.bChangeFilter.Enabled = true;
                this.bDeleteFilter.Enabled = false;
            }
            // odśwież listę
            this.refreshColumnRowsISS( index );

			// typ kolumny
			this.cbColType.SelectedIndex = (int)this._filter.ColumnType[index].SelectedType;
            
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
		private void lvFilterList_SelectedIndexChanged( object sender, EventArgs ev )
        {
            // blokuj przyciski gdy nic nie zostało zaznaczone
            if( this.lvFilterList.SelectedIndices.Count == 0 )
            {
                this.bChangeFilter.Enabled = false;
                this.bDeleteFilter.Enabled = false;

                return;
            }

            // pobierz indeks filtrowanej kolumny
            int selected  = this.lvFilterList.SelectedIndices[0];
            int filteridx = selected;
            int inherited = this._real_newindex != -1 && this._real_subindex != -1
                ? this._filter.IsInherited( this._real_newindex, this._real_subindex, selected, out filteridx )
                : -1;

            FilterInfo info;

            // pobierz filtr dla kolumny podrzędnej lub filtr dziedziczony
            if( inherited == -1 )
                info = this._filter.GetFilter( this._current_column, this._real_subindex, filteridx );
            else
                info = this._filter.GetFilter( inherited, -1, filteridx );

            // typ filtra
            this.cbFilterType.SelectedIndex = this._filterTypeToCBIndex( info.Filter );

            // opcje
            this.tbModifier.Text     = info.Modifier;
            this.tbResult.Text       = info.Result;
            this.cbExclude.Checked   = info.Exclude;
            this.cbAllCopies.Checked = info.FilterCopy;

            // przyciski
            this.bChangeFilter.Enabled = inherited != -1
                ? false
                : true;
            this.bDeleteFilter.Enabled = (this._real_newindex != -1 && this._real_subindex == -1) || inherited != -1
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
		private void cbFilterType_SelectedIndexChanged( object sender, EventArgs ev )
		{
			switch( this.cbFilterType.SelectedIndex )
			{
				// duże litery / format
				case 0:
                    this.tbResult.Enabled  = false;
                    this.cbExclude.Enabled = false;

					// to tylko format
					if( this.cbFilterType.Items.Count == 1 )
                    {
						this.tbModifier.Enabled  = true;
                        this.cbAllCopies.Enabled = false;
					}
                    // a tu duże litery, tylko dla starych kolumn
                    else if( this._real_newindex == -1 )
                    {
                        this.tbModifier.Enabled  = false;
						this.cbAllCopies.Enabled = true;
                    }
                    // i duże litery dla kolumn podrzędnych nowych kolumn
                    else
                    {
                        this.tbModifier.Enabled  = false;
                        this.cbAllCopies.Enabled = false;
                    }
				break;
				// małe litery / nazwa własna
				case 1:
				case 2:
                    this.tbModifier.Enabled = false;
                    this.tbResult.Enabled   = false;
                    this.cbExclude.Enabled  = false;

					if( this._real_newindex == -1 )
						this.cbAllCopies.Enabled = true;
				break;
				// równy / różny
				case 3:
				case 4:
					this.tbResult.Enabled   = true;
					this.tbModifier.Enabled = true;
					this.cbExclude.Enabled  = true;

					if( this._real_newindex == -1 )
						this.cbAllCopies.Enabled = true;
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
        private void cbExclude_CheckedChanged( object sender, EventArgs ev )
        {
            if( this.cbExclude.Checked )
            {
                this.tbResult.Text    = "";
                this.tbResult.Enabled = false;
            }
            else
                this.tbResult.Enabled = true;
        }

        /// @endcond
#endregion

#region PRZYCISKI - FILTROWANIE
        /// @cond EVENTS

        /// <summary>
        /// Akcja wywoływana podczas dodawania nowego filtru.
        /// Dodaje filtr do listy dla wybranej kolumny i odświeża wyświetlane wiersze uwzględniając dodany filtr.
        /// </summary>
		/// 
		/// <param name="sender">Obiekt wywołujący zdarzenie.</param>
		/// <param name="ev">Argumenty zdarzenia</param>
		//* ============================================================================================================
		private void bAddFilter_Click( object sender, EventArgs ev )
		{
			FilterInfo filter = this._filter.CreateFilter
			(
				this.getSelectedFilterType(),
				this.tbModifier.Text,
				this.tbResult.Text,
				this.cbExclude.Checked,
				this.cbAllCopies.Checked && this._real_subindex == -1
			);
			this._filter.AddFilter( this._current_column, this._real_subindex, filter );

#		if DEBUG
			Program.LogMessage( "Dodano nowy filtr: Kolumna(#" + this._current_column + ")" +
                (this._real_subindex != -1 ? (", Rząd(#" + this._real_subindex + "), ") : "") );
#		endif
            // odśwież listę wierszy
            this.refreshColumnRowsISS( this._current_column );
            
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
        private void bDeleteFilter_Click( object sender, EventArgs ev )
        {
            if( this.lvFilterList.SelectedIndices.Count == 0 )
                return;

            int selected  = this.lvFilterList.SelectedIndices[0];

            // usuń filtr
            this._filter.RemoveFilter( this._current_column, this._real_subindex, selected );

#		if DEBUG
			Program.LogMessage( "Usunięto filtr: Kolumna(#" + this._current_column + "), " +
                (this._real_subindex != -1 ? ("Rząd(#" + this._real_subindex + "), ") : "") +
                "Filtr(#" + this.lvFilterList.SelectedIndices[0] + ")" );
#		endif
            // odśwież listę wierszy
            this.refreshColumnRowsISS( this._current_column );

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
        private void bChangeFilter_Click( object sender, EventArgs ev )
        {
            if( this.lvFilterList.SelectedIndices.Count == 0 )
                return;

            // utwórz nowy filtr i podmień stary
			FilterInfo filter = this._filter.CreateFilter
			(
				this.getSelectedFilterType(),
				this.tbModifier.Text,
				this.tbResult.Text,
				this.cbExclude.Checked,
				this.cbAllCopies.Checked
			);

            int selected  = this.lvFilterList.SelectedIndices[0];
			this._filter.ReplaceFilter( this._current_column, this._real_subindex, selected, filter );

#		if DEBUG
			Program.LogMessage( "Zmieniono filtr: Kolumna(#" + this._current_column + "), " +
                (this._real_subindex != -1 ? ("Rząd(#" + this._real_subindex + "), ") : "") +
                "Filtr(#" + this.lvFilterList.SelectedIndices[0] + ")" );
#		endif
            // odśwież listę wierszy
            this.refreshColumnRowsISS( this._current_column );

            // odśwież listę filtrów
            this.displayColumnFilters();

            // zaznacz aktualnie edytowany filtr
            this.lvFilterList.Items[selected].Selected = true;
        }

        /// @endcond
#endregion

#region PASEK AKCJI
        /// @cond EVENTS

        /// <summary>
        /// Akcja wywoływana po zmiane elementu w polu wyboru.
        /// Znajduje się ono w pasku akcji, służy do wyboru kroku do wyświetlania.
        /// Możliwe jest wybranie dwóch kroków - kreatora kolumn i filtrowania danych.
        /// </summary>
        /// 
        /// <param name="sender">Obiekt wywołujący zdarzenie.</param>
        /// <param name="ev">Argumenty zdarzenia.</param>
		//* ============================================================================================================
		private void cbStep_SelectedIndexChanged( object sender, EventArgs ev )
		{
			if( this._locked )
				return;

			if( this.cbStep.SelectedIndex == 0 )
			{
				this.tlCreateColumns.Visible   = true;
				this.lvDatabaseColumns.Visible = true;
				this.scFiltersAndTypes.Visible = false;
				this.lvAllColumns.Visible      = false;
			}
			else
			{
				this.scFiltersAndTypes.Visible = true;
				this.lvAllColumns.Visible      = true;
				this.tlCreateColumns.Visible   = false;
				this.lvDatabaseColumns.Visible = false;
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
				this.cbStep.SelectedIndex = 0;
			break;
			// CTRL + 2
			case Keys.Control | Keys.D2:
				this.cbStep.SelectedIndex = 1;
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
		private void tpTooltip_Draw( object sender, DrawToolTipEventArgs ev )
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
		private void toolTip_Hide( object sender, EventArgs ev )
		{
			if( this._show_tooltip )
			{
				this.tpTooltip.Hide( this.tbColumnName );
				this._show_tooltip = false;
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
        /// Akcja wywoływana podczas kliknięcia w przycisk zapisania zmian.
        /// Funkcja pozwala na zapisanie wszystkich filtrów do schowka - na razie zmiany zapisane są nieodwracalne.
        /// Usuwa kolumny które zostały odznaczone w liście kolumn istniejących w schowku.
        /// </summary>
        /// 
        /// <param name="sender">Obiekt wywołujący zdarzenie.</param>
        /// <param name="ev">Argumenty zdarzenia.</param>
        /// 
        /// <seealso cref="bCancel_Click" />
		//* ============================================================================================================
        private void bSave_Click( object sender, EventArgs ev )
        {
            this._locked = true;

            // aktywne kolumny
            var columns = new List<bool>();

            foreach( ListViewItem item in this.lvDatabaseColumns.Items )
                columns.Add( item.Checked );

            // zastosuj filtry i zapisz
            this._filter.SetActiveColumns( columns );
            this._filter.ApplyFilters();

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
        /// <seealso cref="bSave_Click" />
		//* ============================================================================================================
        private void bCancel_Click( object sender, EventArgs ev )
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        /// @endcond
#endregion
    }
}
