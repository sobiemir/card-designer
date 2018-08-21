///
/// $i02 DatabaseSettingsForm.cs
/// 
/// Okno ustawień pliku bazy danych (kodowanie, separator).
/// Wyświetlane jest zaraz po wybraniu pliku bazy danych.
/// Wywoływane z menu lub po wyborze źródła danych z paska informacji.
/// 
/// Autor: Kamil Biały
/// Od wersji: 0.8.x.x
/// Ostatnia zmiana: 2016-11-08
/// 
/// CHANGELOG:
/// [29.09.2015] Wersja początkowa.
/// [30.01.2016] Dodano ograniczenie wyświetlania wierszy.
/// [28.02.2016] Zmieniono koncepcje zmiany separatora (na combobox).
///              Przebudowano układ kontrolek i dodano nowe.
///              Posegregowano funkcje na regiony.
/// [04.07.2016] Dodatkowe tłumaczenia.
/// [26.07.2016] Komentarze, poprawki kodu.
/// [22.08.2016] Wczytywanie danych bez lub z nagłówkiem po przebudowaniu klasy parsera.
/// [08.11.2016] Zmieniono klasę filtrów z DatabaseReader na IOFileData.
///

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using CDesigner.Utils;

namespace CDesigner.Forms
{
	/// 
	/// <summary>
	/// Klasa formularza podglądu danych z pliku o obsługiwanym przez parser rozszerzeniu.
    /// Lista rozszerzeń możliwych do wczytania plików dostępna jest w klasie <see cref="IOFileData"/>.
	/// Klasa pozwala na ustawienie szczegółów odczytu wybranego pliku, takich jak:
	/// <list type="bullet">
	///		<item>Kodowanie pliku</item>
	///		<item>Separator kolumn</item>
    ///		<item>Nagłówek dla każdej kolumny</item>
	/// </list>
	/// </summary>
	/// 
    /// @todo <dfn><small>[0.9.x.x]</small></dfn> Zmiana szerokości kolumn wraz ze zmianą szerokości okna.
    /// @todo <dfn><small>[0.9.x.x]</small></dfn> Możliwość zmiany ilości wyświetlanych rekordów.
    /// @todo <dfn><small>[0.9.x.x]</small></dfn> Zmiana znaku dla ograniczników pola (enclosing tags).
    /// @todo <dfn><small>[1.0.x.x]</small></dfn> Ustawienia...
	/// @todo <dfn><small>[1.0.x.x]</small></dfn> Automatyczne wykrywanie typów danych.
    /// @todo <dfn><small>[1.0.x.x]</small></dfn> Dopiero zapis (kliknięcie na przycisk wczytaj) zmienia strumień (_storage).
    /// @todo <dfn><small>[?.?.x.x]</small></dfn> Po wciśnięciu WIN na linuksie okno się zamyka (przechwytuje sygnał zamknięcia).
    /// @todo <dfn><small>[?.?.x.x]</small></dfn> Informacja o tym że dane są wczytywane, jakiś pasek postępu czy coś...
	/// 
	public partial class DatafileSettingsForm : Form
	{
#region ZMIENNE

		/// <summary>Blokada elementów przed przypadkowym odświeżeniem.</summary>
		private bool _locked;

		/// <summary>Strumień pliku bazy danych.</summary>
		private IOFileData _storage;

		/// <summary>Aktualnie wyświetlana kolumna.</summary>
		private int _column;

        /// <summary>Informacja o tym, czy plik posiada definicje kolumn.</summary>
        private bool _hascolumns;

#endregion

#region KONSTRUKTORY / WŁAŚCIWOŚCI

		/// <summary>
		/// Konstruktor formularza.
		/// Tworzy okno, ustawia ikonkę programu i tłumaczy wszystkie napisy.
		/// </summary>
		//* ============================================================================================================
		public DatafileSettingsForm()
		{
            this._column     = -1;
            this._hascolumns = true;
            this._locked     = false;
            this._storage    = null;

			// inicjalizacja zmiennych globalnych (potrzebne przy bezpośrednim odpalaniu formularza)
			Program.InitializeGlobals();

			this.InitializeComponent();

			// ikona programu
			this.Icon = Program.GetIcon();

			// tłumaczenia okna
			this.translateForm();
		}

		/// <summary>
		/// Właściwość dla strumienia danych formularza.
		/// Pozwala na pobranie i ustawienia strumienia danych przetwarzanych przez klasę.
		/// Dane z tego strumienia wyświetlane będą w kontrolce ListView.
		/// </summary>
		//* ============================================================================================================
		public IOFileData Storage
		{
			// pobranie właściwości
			get
				{ return this._storage; }

			// ustawienie właściwości
			set
			{
				if( value == null )
				{
					this._storage = null;
					return;
				}

				// sprawdź czy strumień jest gotowy do użycia
				if( !value.Ready )
					return;
				
#			if DEBUG
				Program.LogMessage( "Zmiana stumienia danych." );
#			endif

				this._storage = value;

				// po ustawieniu odśwież
				this.resetControls();
				this.getPreview();
			}
		}
        
		/// <summary>
		/// Czy plik posiada definicje kolumn?
        /// Jest to tylko informacja, przed zmianą wartości należy zapoznać się ze strukturą wczytywanego pliku.
        /// W przypadku braku kolumn, uzupełniane są one domyślnymi wartościami pobieranymi z ustawień językowych.
		/// </summary>
		//* ============================================================================================================
        public bool HasColumns
        {
            get { return this._hascolumns; }
            set { this._hascolumns = value; }
        }

#endregion

#region FUNKCJE PODSTAWOWE

		/// <summary>
		/// Ustawia strumień do przetwarzania danych z wybranego pliku.
		/// Funkcja różni się od właściwości Stream tym, że dla wczytywanego strumienia zmienia właściwości na te,
        /// ustawione wcześniej w konfiguracji przez użytkownika - resetuje je do wartości początkowych.
		/// </summary>
        /// 
		/// <returns>Wartość zwracana przez OpenFileDialog lub DialogResult.Abort w przypadku błędu.</returns>
		//* ============================================================================================================
		public DialogResult fileSelector()
		{
#		if DEBUG
			Program.LogMessage( "Otwieranie okna wyboru strumienia danych." );
#		endif

			OpenFileDialog dialog = Program.GLOBAL.SelectFile;
			dialog.Filter = IOFileData.getExtensionsList( true );

			DialogResult result = dialog.ShowDialog();

			// anulowano wybór pliku?
			if( result != DialogResult.OK )
			{
#			if DEBUG
				Program.LogMessage( "Zrezygnowano z wyboru strumienia danych." );
#			endif
				return result;
			}
			
			// odczytaj strumień
			IOFileData storage = new IOFileData( dialog.FileName, Encoding.Default );

			// w przypadku błędu zwróć ABORT
			if( storage == null || !storage.Ready )
				return DialogResult.Abort;

			this.setDefaultStreamProperties( storage );

			// ustaw strumień
			this.Storage = storage;

#		if DEBUG
			Program.LogMessage( "Ustawiono strumień odczytu danych." );
#		endif

			return DialogResult.OK;
		}

		/// <summary>
		/// Ustawia domyślne właściwości dla strumienia.
		/// Funkcja tylko ustawia właściwości, nie parsuje ponownie pliku.
		/// Używana w funkcjach do zmiany lub utwierania nowego strumienia danych.
		/// </summary>
		/// 
		/// <param name="stream">Strumień danych do zmiany.</param>
		//* ============================================================================================================
		private void setDefaultStreamProperties( IOFileData stream )
		{
			// domyślne kodowanie
			if( Settings.Info.i02_Encoding == 0 )
				stream.Encoding = Encoding.Default;
			else if( Settings.Info.i02_Encoding == 1 )
				stream.Encoding = Encoding.ASCII;
			else if( Settings.Info.i02_Encoding == 2 )
				stream.Encoding = Encoding.UTF8;
			else if( Settings.Info.i02_Encoding == 3 )
				stream.Encoding = Encoding.BigEndianUnicode;
			else if( Settings.Info.i02_Encoding == 4 )
				stream.Encoding = Encoding.Unicode;
			else if( Settings.Info.i02_Encoding == 5 )
				stream.Encoding = Encoding.UTF32;
			else
				stream.Encoding = Encoding.UTF7;

			// domyślny separator
			stream.Separator = Settings.Info.i02_Separator;
		}

		/// <summary>
		/// Tłumaczy formularz względem ustawionej lokalizacji.
		/// Tłumaczone są wszelkie możliwe statyczne kontrolki formularza.
		/// Kontrolki dynamiczne tłumaczone są w locie, podczas zmiany danych lub ustawień.
		/// Zmiana lokalizacji odbywa się poprzez klasę Language.
		/// </summary>
		//* ============================================================================================================
		protected void translateForm()
		{
#		if DEBUG
			Program.LogMessage( "Tłumaczenie kontrolek znajdujących się na formularzu." );
#		endif

			List<string> values = null;

			// tłumaczenie elementów listy opcji kodowania pliku
			values = Language.GetLines( "DatafileSettings", "Encoding" );
			for( int x = 0; x < this.CBX_Encoding.Items.Count; ++x )
				this.CBX_Encoding.Items[x] = (object)values[x];

			// tłumaczenie nagłówków tabeli
			values = Language.GetLines( "DatafileSettings", "Headers" );
			this.CH_Rows.Text    = values[(int)LANGCODE.I02_HEA_ROWS];
			this.CH_Columns.Text = values[(int)LANGCODE.I02_HEA_COLUMNS];

			// tłumaczenie przycisków
			values = Language.GetLines( "DatafileSettings", "Buttons" );
			this.B_Change.Text = values[(int)LANGCODE.I02_BUT_CHANGE];
			this.B_Save.Text   = values[(int)LANGCODE.I02_BUT_SAVE];
			this.B_Cancel.Text = values[(int)LANGCODE.I02_BUT_CANCEL];

			// tłumaczenie nazw separatorów
			values = Language.GetLines( "DatafileSettings", "Separator" );
			for( int x = 0; x < this.CBX_Separator.Items.Count; ++x )
				this.CBX_Separator.Items[x] = (object)values[x];

			// tłumaczenie tekstów na formularzu
			values = Language.GetLines( "DatafileSettings", "Labels" );
			this.CB_AutoCheck.Text = values[(int)LANGCODE.I02_LAB_AUTODETECT];
			this.CB_NoColumns.Text = values[(int)LANGCODE.I02_LAB_NOHEADERS];

			// tytuł okna
			this.Text = Language.GetLine( "FormNames", (int)LANGCODE.GFN_DATAFILESETTINGS );
		}
		
		/// <summary>
		/// Pobiera próbkę i wyświetla jako podgląd pliku.
		/// Funkcja decyduje ile wierszy pobrać z pliku (edytowalne w ustawieniach).
		/// Po pobraniu próbki uzupełnia listę kolumn i zaznacza pierwszą wyświetlając wiersze z pierwszej kolumny.
		/// </summary>
		//* ============================================================================================================
		protected void getPreview()
		{
			if( this._storage == null )
				return;

#		if DEBUG
			Program.LogMessage( "Wyświetlanie podglądu wybranego strumienia danych." );
#		endif

			this._locked = true;

			// parsuj plik
			this._storage.parse( Settings.Info.i02_RowsNumber, this._hascolumns );

			// kolumny
			this.LV_Columns.Items.Clear();
			for( int x = 0; x < this._storage.ColumnsNumber; ++x )
				this.LV_Columns.Items.Add( this._storage.Column[x] );

			// zaznacz pierwszy element
			if( this._storage.ColumnsNumber > 0 )
			{
				this.LV_Columns.Items[0].Selected = true;
				this._column = 0;
			}

			// wiersze
			this.LV_Rows.Items.Clear();
			for( int x = 0; x < this._storage.RowsNumber; ++x )
				this.LV_Rows.Items.Add( this._storage.Row[x][0] );

			// odśwież nagłówek
			this.CH_Rows.Text = Language.GetLine( "DatafileSettings", "Headers", (int)LANGCODE.I02_HEA_ROWS ) +
				" [" + this._storage.Column[0] + "]";

			this._locked = false;
		}
		
		/// <summary>
		/// Odświeżanie wartości kontrolek formularza.
		/// Funkcja wywoływana w przypadku zmiany strumienia lub pliku przez kliknięcie w przycisk Zmień.
		/// </summary>
		//* ============================================================================================================
		protected void resetControls()
		{
			if( this._storage == null )
				return;

#		if DEBUG
			Program.LogMessage( "Odświeżanie kontrolek." );
#		endif

			this._locked = true;

			// wyświetl informacje o kodowaniu
			// nie można na case bo Encoding jest klasą...
			if( this._storage.Encoding == Encoding.Default )
				this.CBX_Encoding.SelectedIndex = 0;
			else if( this._storage.Encoding == Encoding.ASCII )
				this.CBX_Encoding.SelectedIndex = 1;
			else if( this._storage.Encoding == Encoding.UTF8 )
				this.CBX_Encoding.SelectedIndex = 2;
			else if( this._storage.Encoding == Encoding.BigEndianUnicode )
				this.CBX_Encoding.SelectedIndex = 3;
			else if( this._storage.Encoding == Encoding.Unicode )
				this.CBX_Encoding.SelectedIndex = 4;
			else if( this._storage.Encoding == Encoding.UTF32 )
				this.CBX_Encoding.SelectedIndex = 5;
			else
				this.CBX_Encoding.SelectedIndex = 6;

			this.TB_Separator.Enabled = false;

			// separator
			switch( this._storage.Separator )
			{
			case ';' : this.CBX_Separator.SelectedIndex = 0; break;
			case ',' : this.CBX_Separator.SelectedIndex = 1; break;
			case '.' : this.CBX_Separator.SelectedIndex = 2; break;
			case '\t': this.CBX_Separator.SelectedIndex = 3; break;
			case ' ' : this.CBX_Separator.SelectedIndex = 4; break;
			default  :
				this.CBX_Separator.SelectedIndex = 5;
				this.TB_Separator.Enabled = true;
			break;
			}

			// wyświetl separator i nazwę pliku
			this.TB_Separator.Text = ((char)this._storage.Separator).ToString();
			this.TB_FileName.Text  = this._storage.FileName;

            // wczytywanie danych bez kolumn
            this.CB_NoColumns.Checked = !this._hascolumns;

			this._locked = false;
		}

#endregion

#region AKCJE KONTROLEK
		/// @cond EVENTS

		/// <summary>
		/// Zmiana wyświetlanej kolumny.
		/// Po kliknięciu w element z listy kolumn odświeża elementy w liście wierszy.
        /// Zmienia nazwę w nagłówku listy na nazwę aktualnie zaznaczonej kolumny.
		/// </summary>
		/// 
		/// <param name="sender">Obiekt wywołujący zdarzenie.</param>
		/// <param name="ev">Argumenty zdarzenia.</param>
		//* ============================================================================================================
		private void lvColumns_SelectedIndexChanged( object sender, EventArgs ev )
		{
			if( this._locked || this.LV_Columns.SelectedItems.Count == 0 || this._column == this.LV_Columns.SelectedItems[0].Index )
				return;

			// wyświetlenie nazwy kolumny w nagłówku
			this.CH_Rows.Text = Language.GetLine( "DatafileSettings", "Headers", (int)LANGCODE.I02_HEA_ROWS ) +
				" [" + this.LV_Columns.SelectedItems[0].Text + "]";
			this.LV_Rows.Items.Clear();

			// indeks zaznaczonej kolumny
			int column = this.LV_Columns.SelectedItems[0].Index;

			if( this._storage == null )
				return;

			// zmień wyświetlane rekordy
			for( int x = 0; x < this._storage.RowsNumber; ++x )
				this.LV_Rows.Items.Add( this._storage.Row[x][column] );

			this._column = this.LV_Columns.SelectedItems[0].Index;

#		if DEBUG
			Program.LogMessage( "Wyświetlono dane z kolumny o indeksie: " + this._column + "." );
#		endif
		}
	
		/// <summary>
		/// Zmiana kodowania przetwarzanego pliku.
		/// Po zmianie kodowania plik ponownie jest parsowany.
		/// Może się zdażyć tak, że po zmianie kodowania dostępna jest tylko jedna kolumna.
		/// </summary>
		/// 
		/// <param name="sender">Obiekt wywołujący zdarzenie.</param>
		/// <param name="ev">Argumenty zdarzenia.</param>
		//* ============================================================================================================
		private void sbEncoding_SelectedIndexChanged( object sender, EventArgs ev )
		{
			if( this._locked || this._storage == null )
				return;

			// rozpoznaj odpowiednie kodowanie i otwórz ponownie plik
			switch( this.CBX_Encoding.SelectedIndex )
			{
			case 1 : this._storage.Encoding = Encoding.ASCII; break;
			case 2 : this._storage.Encoding = Encoding.UTF8; break;
			case 3 : this._storage.Encoding = Encoding.BigEndianUnicode; break;
			case 4 : this._storage.Encoding = Encoding.Unicode; break;
			case 5 : this._storage.Encoding = Encoding.UTF32; break;
			case 6 : this._storage.Encoding = Encoding.UTF7; break;
			default: this._storage.Encoding = Encoding.Default; break;
			}

			// ponownie parsuj plik i wyświetl nowe informacje
			this.getPreview();

#		if DEBUG
			Program.LogMessage( "Zmieniono kodowanie strumienia danych na indeks: " + this.CBX_Encoding.SelectedIndex + "." );
#		endif
		}

		/// <summary>
		/// Zmiana separatora oddzielającego kolumny w pliku.
		/// Dla tabulacji i spacji wyznaczone są odpowiednio nietłumaczalne ciągi znaków (TAB) i (SPA).
		/// Może się zdażyć tak, że po zmianie separatora dostępna jest tylko jedna kolumna.
		/// </summary>
		/// 
		/// <param name="sender">Obiekt wywołujący zdarzenie.</param>
		/// <param name="ev">Argumenty zdarzenia.</param>
		//* ============================================================================================================
		private void sbSeparator_SelectedIndexChanged( object sender, EventArgs ev )
		{
			this.TB_Separator.Enabled = false;

			switch( this.CBX_Separator.SelectedIndex )
			{
			case 0 : this.TB_Separator.Text = ";"; break;
			case 1 : this.TB_Separator.Text = ","; break;
			case 2 : this.TB_Separator.Text = "."; break;
			case 3 : this.TB_Separator.Text = "(TAB)"; break;
			case 4 : this.TB_Separator.Text = "(SPA)"; break;
			default:
				this.TB_Separator.Enabled = true;
				this.TB_Separator.Text = "";
			break;
			}

#		if DEBUG
			if( this.CBX_Separator.SelectedIndex == 5 )
				Program.LogMessage( "Włączono możliwość zdefiniowania własnego separatora kolumn." );
			else
				Program.LogMessage( "Zmieniono separator oddzielający kolumny na: " + this.TB_Separator.Text + "." );
#		endif
		}

		/// <summary>
		/// Funkcja blokuje wszelkie operacje edycji na kontrolce.
		/// Nazwa pliku nie może być zmieniana od ręki.
		/// Wyłączona kontrolka dziwnie wyglądała, lepiej zostawić aktywną, wyświetla i tak tylko nazwę pliku.
		/// </summary>
		/// 
		/// <param name="sender">Obiekt wywołujący zdarzenie.</param>
		/// <param name="ev">Argumenty zdarzenia.</param>
		//* ============================================================================================================
		private void tbFileName_KeyPress( object sender, KeyPressEventArgs ev )
		{
			// przepuszczaj klawisze kontrolne
			if( char.IsControl(ev.KeyChar) )
				return;

			ev.Handled = true;
		}

		/// <summary>
		/// Zmiana separatora kolumn w pliku na dowolny separator.
		/// Przepuszcza tylko pojedyncze znaki. Wyjątkiem są ciągi znaków (SPA) i (TAB).
		/// Po zmianie separatora odświeża podgląd pliku.
		/// Może się zdażyć tak, że po zmianie separatora dostępna jest tylko jedna kolumna.
        /// Przed zmianą należy zapoznać się ze strukturą pliku. Separatora własnego używać tylko w ostateczności.
		/// </summary>
		/// 
		/// <param name="sender">Obiekt wywołujący zdarzenie.</param>
		/// <param name="ev">Argumenty zdarzenia.</param>
		//* ============================================================================================================
		private void tbSeparator_TextChanged( object sender, EventArgs ev )
		{
			if( this._locked || this.TB_Separator.Text == "" || this._storage == null )
				return;
			
			// zmień separator i ponowne parsuj plik
			if( this.TB_Separator.Text == "(SPA)" )
				this._storage.Separator = ' ';
			else if( this.TB_Separator.Text == "(TAB)" )
				this._storage.Separator = '\t';
			else
				this._storage.Separator = this.TB_Separator.Text[0];

			this.getPreview();

#		if DEBUG
			Program.LogMessage( "Zdefiniowano własny separator dzielący kolumny w strumieniu." );
#		endif
		}
        
		/// <summary>
		/// Przełączanie pomiędzy wczytywaniem nagłówków dla kolumn z pliku.
        /// Po każdej zmianie plik jest ponownie parsowany i odświeżany w liście z kolumnami i wierszami.
		/// </summary>
		/// 
		/// <param name="sender">Obiekt wywołujący zdarzenie.</param>
		/// <param name="ev">Argumenty zdarzenia.</param>
		//* ============================================================================================================
        private void cbNoColumns_CheckedChanged( object sender, EventArgs ev )
        {
            this._hascolumns = !this.CB_NoColumns.Checked;
            this.getPreview();
        }

		/// <summary>
		/// Zdarzenie odpowiedzialne za zapis danych.
		/// Robi praktycznie to samo co zdarzenie zamknięcia kontrolki.
		/// Jedyna różnica to wartość zwracana w DialogResult.
        /// Dane na razie nie są zapisane, więc z tego powodu nie ma między nimi różnic.
		/// </summary>
		/// 
		/// <param name="sender">Obiekt wywołujący zdarzenie.</param>
		/// <param name="ev">Argumenty zdarzenia.</param>
		//* ============================================================================================================
		private void bSave_Click( object sender, EventArgs ev )
		{
#		if DEBUG
			Program.LogMessage( "Nowy strumień danych został zapisany." );
#		endif
            this._storage.parse( -1 );

			this.DialogResult = DialogResult.OK;
			this.Close();
		}

		/// <summary>
		/// Zdarzenie odpowiedzialne za zamknięcie kontrolki.
		/// Robi praktycznie to samo co zdarzenie zapisu po wciśnięciu przycisku Wczytaj.
		/// Jedyna różnica to wartość zwracana w DialogResult.
		/// </summary>
		/// 
		/// <param name="sender">Obiekt wywołujący zdarzenie.</param>
		/// <param name="ev">Argumenty zdarzenia.</param>
		//* ============================================================================================================
		private void bCancel_Click( object sender, EventArgs ev )
		{
#		if DEBUG
			Program.LogMessage( "Zrezygnowano ze zmiany strumienia danych." );
#		endif
			this.DialogResult = DialogResult.Cancel;
			this.Close();
		}

		/// <summary>
		/// Zmiana strumienia bazy danych.
		/// Po kliknięciu wyświetla okno z wyborem nowego pliku.
		/// Po wybraniu pliku automatycznie odświeża podgląd i kontrolki oraz zmienia właściwości strumienia na te,
        /// podane w konfiguracji przez użytkownika.
		/// </summary>
		/// 
		/// <param name="sender">Obiekt wywołujący zdarzenie.</param>
		/// <param name="ev">Argumenty zdarzenia.</param>
		//* ============================================================================================================
		private void bChange_Click( object sender, EventArgs ev )
		{
#		if DEBUG
			Program.LogMessage( "Otwieranie okna wyboru strumienia danych." );
#		endif

			OpenFileDialog dialog = Program.GLOBAL.SelectFile;

			dialog.Title  = Language.GetLine( "MessageNames", (int)LANGCODE.iMN_DatafileSelect );
			dialog.Filter = IOFileData.getExtensionsList( true );

			// anulowano wybór...
			if( dialog.ShowDialog() != DialogResult.OK )
			{
#			if DEBUG
				Program.LogMessage( "Zrezygnowano z wyboru strumienia danych." );
#			endif
				return;
			}

			// zmień plik i wyświetl zawartość
			if( this._storage != null )
				this._storage.FileName = dialog.FileName;
			else
				this._storage = new IOFileData( dialog.FileName, Encoding.Default );
			
			this.setDefaultStreamProperties( this._storage );

			this.resetControls();
			this.getPreview();

#		if DEBUG
			Program.LogMessage( "Zmieniono strumień odczytu danych." );
#		endif
		}

		/// <summary>
		/// Rysuje górną ramkę dla paska informacji.
		/// </summary>
		/// 
		/// <param name="sender">Obiekt wywołujący zdarzenie.</param>
		/// <param name="ev">Argumenty zdarzenia.</param>
		//* ============================================================================================================
		private void tlpStatusBar_Paint( object sender, PaintEventArgs ev )
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

		/// @endcond
#endregion
	}
}
