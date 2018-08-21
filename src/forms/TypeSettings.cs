///
/// $i[xx] TypeSettings.cs
/// 
/// Okno ustawień pliku bazy danych (kodowanie, separator).
/// Wyświetlane jest zaraz po wybraniu pliku bazy danych.
/// Ustawienia w tym formularzu nie są używane.
/// 
/// Autor: Kamil Biały
/// Od wersji: 0.8.x.x
/// Ostatnia zmiana: 2016-07-11
/// 
/// CHANGELOG:
/// [11.07.2016] Wersja początkowa.
///

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Globalization;
using CDesigner.Utils;

namespace CDesigner.Forms
{
	/// 
	/// <summary>
	/// Formularz edycji ustawień dla typu kolumny.
	/// Możliwość jego wyświetlenia udostępniają typy: liczbowe, procentowe, daty i waluty.
	/// Wszystkie dane ustawiane w formularzu przekazywane są potem do klasy NumberFormatInfo.
	/// Z tej klasy parsowane i wyświetlane są dane odpowiednio do ustawień.
	/// </summary>
	/// 
	/// @todo Różne liczby dla różnych kultur?
	/// @todo Dodać domyślne ustawienia definiowane przez użytkownika.
	/// 
	public partial class TypeSettings : Form
	{
#region ZMIENNE
		/// <summary>Klasa zawierająca zastępcze informacje o typie kolumny.</summary>
		private ColumnTypeInfo _info;
		
		/// <summary>Klasa zawierająca oryginalne informacje o typie kolumny.</summary>
		private ColumnTypeInfo _original;

		/// <summary>Klasa z aktualnymi informacjami o wybranej lokalizacji.</summary>
		private CultureInfo _culture;

		/// <summary>Zaznaczony typ danych przesyłany do konstruktora.</summary>
		private int _type;

		/// <summary>Symbol procenta używany w zmianie typu podrzędnego.</summary>
		private string _percent;

		/// <summary>Symbol promila używany w zmianie typu podrzędnego.</summary>
		private string _permille;

		/// <summary>Blokada automatycznego uzupełniania danych w funkcjach.</summary>
		private bool _locked = false;
#endregion

#region KONSTRUKTORY / WŁAŚCIWOŚCI
		/// <summary>
		/// Konstruktor klasy z dwoma argumentami.
		/// Uzupełnia dane danymi domyślnymi i tłumaczy formularz na aktualny język.
		/// 
		/// Do konstruktora należy przekazać typ danych kolumny, który zdefiniowany jest następująco:
		/// <list type="number">
		///		<item><description>Liczba całkowita i dziesiętna, <i>String => Integer / Float / Numeric</i>.</description></item>
		///		<item><description>Procenty i promile, <i>String => Float</i>.</description></item>
		///		<item><description>Data w różnych formatach, <i>String => Integer</i>.</description></item>
		///		<item><description>Waluta w różnych formatach, <i>String => Numeric</i>.</description></item>
		/// </list>
		/// </summary>
		/// 
		/// <example>
		/// Przykład użycia konstruktora:
		/// <code>
		/// ColumnTypeInfo info = new ColumnTypeInfo();
		/// Form.TypeSettings form = new Form.TypeSettings( 2, info );
		/// 
		/// if( form.ShowDialog() == DialogResult.OK )
		///		Console.WriteLine( "Zapisano poprawnie!" );
		/// </code>
		/// </example>
		/// 
		/// <seealso cref="ColumnTypeInfo"/>
		/// 
		/// <param name="type">Typ danych w kolumnie - od 1 do 4.</param>
		/// <param name="info">Klasa z szczegółowymi informacjami o typie danych kolumny.</param>
		//* ============================================================================================================
		public TypeSettings( int type, ColumnTypeInfo info )
		{
			// inicjalizacja kontrolek
			InitializeComponent();

			info.SelectedType = type;

			// utwórz klon klasy
			this._info = new ColumnTypeInfo();
			this._info.Clone( info );

			this._original = info;

			// przetłumacz formularz
			this.TranslateForm();
			
			// ustaw typ
			this.Type = type;

			// ustaw ikonę
			this.Icon = Program.GetIcon();
		}

		/// <summary>
		/// Zerowy konstruktor klasy (bez argumentów).
		/// Uzupełnia dane danymi domyślnymi i tłumaczy formularz na aktualny język.
		/// </summary>
		/// 
		/// <example>
		/// Przykład użycia konstruktora:
		/// <code>
		/// Form.TypeSettings form = new Form.TypeSettings();
		/// 
		/// if( form.ShowDialog() == DialogResult.OK )
		///		Console.WriteLine( "Zapisano poprawnie!" );
		///	
		/// ColumnTypeInfo info = form.ColumnType;
		/// </code>
		/// </example>
		/// 
		/// <seealso cref="ColumnTypeInfo"/>
		/// <seealso cref="TranslateForm"/>
		//* ============================================================================================================
		public TypeSettings()
		{
			// inicjalizacja kontrolek
			InitializeComponent();

			// utwórz nową klasę
			this._info     = new ColumnTypeInfo();
			this._original = null;

			// przetłumacz formularz
			this.TranslateForm();

			// ustaw typ pierwszy - INT
			this.Type = 1;

			// ustaw ikonę
			this.Icon = Program.GetIcon();
		}

		/// <summary>
		/// Pobiera lub zmienia typ kolumny.
		/// Po zmianie typu odświeża typy podrzędne głównej klasy informacji o typie.
		/// Po odświeżeniu chowa niepotrzebne kontrolki i uzupełnia aktywne nowymi danymi. 
		/// </summary>
		/// 
		/// <example>
		/// Przykład użycia właściwości:
		/// <code>
		/// Form.TypeSettings form = new Form.TypeSettings();
		/// form.Type = (int)DATATYPE.TypeCurrency;
		/// 
		/// if( form.ShowDialog() == DialogResult.OK )
		///		Console.WriteLine( "Zapisano poprawnie!" );
		///	
		/// ColumnTypeInfo info = form.ColumnType;
		/// </code>
		/// </example>
		/// 
		/// <seealso cref="RefreshTypeAndSubtype"/>
		/// <seealso cref="SwitchControls"/>
		/// <seealso cref="DATATYPE"/>
		//* ============================================================================================================
		public int Type
		{
			get { return this._type; }
			set
			{
				this._type = value;
				this.RefreshTypeAndSubtype( value, this._info );

				this.FillComboBoxes();
				this.FillControlsFromCulture( Thread.CurrentThread.CurrentCulture );
				this.SwitchControls();
			}
		}

		/// <summary>
		/// Pobiera lub zmienia klasę informacji o typie kolumny.
		/// Odpala właściwość <see cref="Type"/> dla odświeżenia informacji o strukturze.
		/// </summary>
		/// 
		/// <example>
		/// Przykład użycia właściwości:
		/// <code>
		/// Form.TypeSettings form = new Form.TypeSettings();
		/// 
		/// form.Type       = (int)DATATYPE.TypeCurrency;
		/// form.ColumnType = new ColumnTypeInfo();
		/// 
		/// if( form.ShowDialog() == DialogResult.OK )
		///		Console.WriteLine( "Zapisano poprawnie!" );
		/// </code>
		/// </example>
		/// 
		/// <seealso cref="TypeSettings.Type"/>
		/// <seealso cref="ColumnTypeInfo.Clone"/>
		//* ============================================================================================================
		public ColumnTypeInfo ColumnType
		{
			get { return this._info; }
			set
			{
				this._info.Clone( value );
				this._original = value;

				// odśwież dane dla typu
				this.Type = this._type;
			}
		}

		/// <summary>
		/// Pobiera informacje o aktualnie wybranej kulturze.
		/// Możliwy jest tylko odczyt danych.
		/// </summary>
		//* ============================================================================================================
		public CultureInfo Culture
		{
			get { return this._culture; }
		}

		/// <summary>
		/// Odświeża typ i typ podrzędny w klasie informacyjnej.
		/// Używany tylko wewnątrz tej klasy - automatycznie dopasowuje typ podrzędny i typ główny w zależności od
		/// podanego w argumencie typu przekazanego z pola wyboru.
		/// Szczegóły dotyczące informacji o typach znajdują się w <see cref="Forms.TypeSettings.TypeSettings"/>.
		/// 
		/// <param name="type">Typ danych w kolumnie - od 1 do 4.</param>
		/// <param name="info">Klasa z szczegółowymi informacjami o typie danych kolumny.</param>
		//* ============================================================================================================
		private void RefreshTypeAndSubtype( int type, ColumnTypeInfo info )
		{
			// sprawdź typ kolumny
			switch( type )
			{
				// liczba
				case 1:
					// sprawdź czy dane mają odpowiedni typ
					if( (info.Type    == DATATYPE.Integer && (info.Subtype == DATATYPE.Int16   ||
						 info.Subtype == DATATYPE.Int32   ||  info.Subtype == DATATYPE.Int64)) ||
						(info.Type    == DATATYPE.Float   && (info.Subtype == DATATYPE.Single  ||
						 info.Subtype == DATATYPE.Double  ||  info.Subtype == DATATYPE.Numeric )) )
					{
						this._locked = true;
						break;
					}

					// domyślnie ustaw na Int32
					if( info.Subtype == DATATYPE.NotSet )
						info.Subtype = DATATYPE.Int32;
						
					// ustaw typ główny
					switch( info.Subtype )
					{
						case DATATYPE.Int32:
						case DATATYPE.Int64:
						case DATATYPE.Int16:   info.Type = DATATYPE.Integer; break;
						case DATATYPE.Single:
						case DATATYPE.Double:  
						case DATATYPE.Numeric: info.Type = DATATYPE.Float;   break;
					}
				break;
				// procent / promil
				case 2:
					// sprawdź czy dane mają już odpowiedni typ
					if( info.Type == DATATYPE.Float && (info.Subtype == DATATYPE.Percent || info.Subtype == DATATYPE.Permille) )
					{
						this._locked = true;
						break;
					}

					// domyślnie procent
					info.Type    = DATATYPE.Float;
					info.Subtype = DATATYPE.Percent;
				break;
				// data
				case 3:
					// sprawdź czy dane mają już odpowiedni typ
					if( info.Type == DATATYPE.Integer && info.Subtype == DATATYPE.Date )
					{
						this._locked = true;
						break;
					}
					info.Type    = DATATYPE.Integer;
					info.Subtype = DATATYPE.Date;
				break;
				case 4:
					// sprawdź czy dane mają już odpowiedni typ
					if( info.Type == DATATYPE.Float && info.Subtype == DATATYPE.Currency )
					{
						this._locked = true;
						break;
					}

					info.Type    = DATATYPE.Float;
					info.Subtype = DATATYPE.Currency;
				break;
			}
		}
#endregion

#region WYPEŁNIENIA KONTROLEK / TŁUMACZENIA
		/// <summary>
		/// Translator formularza.
		/// Funkcja tłumaczy nazwę formularza, wszystkie napisy i przyciski formularza bez pól wyboru.
		/// Pola wyboru przeniesione zostały do innej funkcji z racji tego iż są one wypełniane dynamicznie.
		/// </summary>
		/// 
		/// <seealso cref="FillComboBoxes"/>
		/// <seealso cref="TypeSettings"/>
		/// <seealso cref="FillControlsFromCulture"/>
		//* ============================================================================================================
		protected void TranslateForm()
		{
			// napisy
			List<string> values = Language.GetLines( "TypeSettings", "Labels" );

			this.lSecType.Text        = values[0];
			this.lLocale.Text         = values[1];
			this.lDateFormat.Text     = values[2];
			this.lSeparator.Text      = values[3];
			this.lGroupSeparator.Text = values[4];
			this.lMinusPattern.Text   = values[5];
			this.lPlusPattern.Text    = values[6];
			this.lCurrency.Text       = values[7];
			this.lNaN.Text            = values[8];
			this.lMinusInf.Text       = values[9];
			this.lPlusInf.Text        = values[10];
			this.lMinusSign.Text      = values[11];
			this.lPlusSign.Text       = values[12];
			this.lDatePreview.Text    = values[13];

			// przyciski
			values = Language.GetLines( "TypeSettings", "Buttons" );

			this.bSave.Text        = values[0];
			this.bCancel.Text      = values[1];
			this.bLoadCulture.Text = values[2];

			// nazwa formularza
			this.Text = Language.GetLine( "FormNames", 4 );
		}

		/// <summary>
		/// Uzupełnia kontrolki formularza w zależności od lokalizacji podawanej w polu tekstowym.
		/// Domyślnie brana jest aktualna lokalizacja używana dla programu.
		/// Dane zapisywane są również do klasy ColumnTypeInfo.
		/// Funkcja ta wewnętrznie wywoływana jest tylko za pierwszym razem przy tworzeniu klasy.
		/// </summary>
		/// 
		/// <seealso cref="FillComboBoxes"/>
		/// <seealso cref="TranslateForm"/>
		///
		/// <param name="cinfo">Struktura z informacjami o lokalizacji (kulturze danego kraju).</param>
		//* ============================================================================================================
		protected void FillControlsFromCulture( CultureInfo cinfo )
		{
			NumberFormatInfo ninfo = cinfo.NumberFormat;
			
			if( !this._locked )
			{
				this._info.Culture          = cinfo.Name;
				this._info.DateFormat       = cinfo.DateTimeFormat.ShortDatePattern;
				this._info.NaN              = ninfo.NaNSymbol;
				this._info.NegativeSign     = ninfo.NegativeSign;
				this._info.PositiveSign     = ninfo.PositiveSign;
				this._info.NegativeInfinity = ninfo.NegativeInfinitySymbol;
				this._info.PositiveInfinity = ninfo.PositiveInfinitySymbol;
			}

			// data i waluta
			if( this._type == 3 || this._type == 4 )
				this.cbSecType.SelectedIndex = 0;
			
			// liczby
			if( this._type == 1 )
				switch( this._info.Subtype )
				{
					case DATATYPE.Int16:   this.cbSecType.SelectedIndex = 0; break;
					case DATATYPE.Int32:   this.cbSecType.SelectedIndex = 1; break;
					case DATATYPE.Int64:   this.cbSecType.SelectedIndex = 2; break;
					case DATATYPE.Single:  this.cbSecType.SelectedIndex = 3; break;
					case DATATYPE.Double:  this.cbSecType.SelectedIndex = 4; break;
					case DATATYPE.Numeric: this.cbSecType.SelectedIndex = 5; break;
				}
			// procenty / promile
			else if( this._type == 2 )
			{
				if( this._info.Subtype == DATATYPE.Percent )
					this.cbSecType.SelectedIndex = 0;
				else
					this.cbSecType.SelectedIndex = 1;
			}
			
			if( !this._locked )
				switch( this._info.Subtype )
				{
					// typy liczbowe
					case DATATYPE.Int16:
					case DATATYPE.Int32:
					case DATATYPE.Int64:
					case DATATYPE.Single:
					case DATATYPE.Double:
					case DATATYPE.Numeric:
						this._info.NumberSymbol    = "";
						this._info.GroupSeparator  = ninfo.NumberGroupSeparator;
						this._info.Separator       = ninfo.NumberDecimalSeparator;
						this._info.NegativePattern = ninfo.NumberNegativePattern;
						this._info.PositivePattern = 0;
					break;
					// procenty / promile
					case DATATYPE.Percent:
					case DATATYPE.Permille:
						this._info.NumberSymbol = this._info.Subtype == DATATYPE.Percent
							? ninfo.PercentSymbol
							: ninfo.PerMilleSymbol;

						this._info.GroupSeparator  = ninfo.PercentGroupSeparator;
						this._info.Separator       = ninfo.PercentDecimalSeparator;
						this._info.NegativePattern = ninfo.PercentNegativePattern;
						this._info.PositivePattern = ninfo.PercentPositivePattern;
					break;
					// waluta
					case DATATYPE.Currency:
						this._info.NumberSymbol    = ninfo.CurrencySymbol;
						this._info.GroupSeparator  = ninfo.CurrencyGroupSeparator;
						this._info.Separator       = ninfo.CurrencyDecimalSeparator;
						this._info.NegativePattern = ninfo.CurrencyNegativePattern;
						this._info.PositivePattern = ninfo.CurrencyPositivePattern;
					break;
					case DATATYPE.Date:
						this._info.NumberSymbol    = "";
						this._info.GroupSeparator  = "";
						this._info.Separator       = "";
						this._info.NegativePattern = 0;
						this._info.PositivePattern = 0;
					break;
				}

			// uzupełnij kontrolki
			this.tbCulture.Text        = this._info.Culture;
			this.tbCurrency.Text       = this._info.NumberSymbol;
			this.tbDateFormat.Text     = this._info.DateFormat;
			this.tbGroupSeparator.Text = this._info.GroupSeparator;
			this.tbMinusInf.Text       = this._info.NegativeInfinity;
			this.tbPlusInf.Text        = this._info.PositiveInfinity;
			this.tbPlusSign.Text       = this._info.PositiveSign;
			this.tbMinusSign.Text      = this._info.NegativeSign;
			this.tbNaN.Text            = this._info.NaN;
			this.tbSeparator.Text      = this._info.Separator;

			this.cbMinusPattern.SelectedIndex = this._info.NegativePattern;
			this.cbPlusPattern.SelectedIndex  = this._info.PositivePattern;

			// pobierz informacje o lokalizacji
			this._culture = CultureInfo.GetCultureInfo( this._info.Culture );

			// zapisz symbole procentu i promila
			this._percent  = this._info.Subtype == DATATYPE.Percent  ? this._info.NumberSymbol : ninfo.PercentSymbol;
			this._permille = this._info.Subtype == DATATYPE.Permille ? this._info.NumberSymbol : ninfo.PerMilleSymbol;

			// podgląd daty na początku
			if( this._info.Subtype == DATATYPE.Date )
				tbDateFormat_TextChanged( null, null );
		}

		/// <summary>
		/// Uzupełnia pola wyboru w formularzu odpowiednimi dla sytuacji danymi.
		/// Funkcja robi za tłumacza dla pierwszego pola wyboru, w przypadku reszty dane są generowane i sklejane
		/// z zawartości odpowiednich pól tekstowych dostępnych w formularzu.
		/// </summary>
		///
		/// <seealso cref="FillControlsFromCulture"/>
		/// <seealso cref="TranslateForm"/>
		///
		/// <param name="refresh">Jeżeli ustawiony, elementy z pól wyboru są tylko aktualizowane a nie usuwane.</param>
		//* ============================================================================================================
		protected void FillComboBoxes( bool refresh = false )
		{
			switch( this._type )
			{
				// zwykła liczba
				case 1:
				{
					// typy podrzędne
					if( !refresh )
					{
						List<string> values = Language.GetLines( "TypeSettings", "ColumnSubTypes" );

						this.cbSecType.Items.Clear();
						for( int x = 0; x < 6; ++x )
							this.cbSecType.Items.Add( values[x] );
					}

					// sformułuj podgląd liczby
					string   innum = this._info.Type == DATATYPE.Float
						? "123" + this.tbGroupSeparator.Text + "456" + this.tbSeparator.Text + "78"
						: "123" + this.tbGroupSeparator.Text + "456";
					string   minus = this.tbMinusSign.Text;
					string[] range =
					{
						"(" + innum + ")",
						minus + innum,
						minus + " " + innum,
						innum + minus,
						innum + " " + minus
					};

					// odśwież podgląd liczby w wyborze wzorca liczby ujemnej
					if( refresh && cbMinusPattern.Items.Count == 5 )
					{
						for( int x = 0; x < 5; ++x )
							this.cbMinusPattern.Items[x] = range[x];
						return;
					}
					else
					{
						this.cbMinusPattern.Items.Clear();
						this.cbMinusPattern.Items.AddRange( range );
					}

					// wzorzec liczby dodatniej (brak w tym warunku)
					this.cbPlusPattern.Items.Clear();
					this.cbPlusPattern.Items.Add( Language.GetLine("TypeSettings", "ColumnSubTypes", 8) );
				}
				break;
				// procenty / promile
				case 2:
				{
					// typy podrzędne
					if( !refresh )
					{
						List<string> values = Language.GetLines( "TypeSettings", "ColumnSubTypes" );

						this.cbSecType.Items.Clear();
						for( int x = 6; x < 8; ++x )
							this.cbSecType.Items.Add( values[x] );
					}

					// sformułuj podgląd liczby
					string   innum      = "12" + this.tbSeparator.Text + "34";
					string   minus      = this.tbMinusSign.Text;
					string   percent    = this.tbCurrency.Text;
					string[] minusrange =
					{
						minus + innum + " " + percent,
						minus + innum + percent,
						minus + percent + innum,
						percent + minus + innum,
						percent + innum + minus,
						innum + minus + percent,
						innum + percent + minus,
						minus + percent + " " + innum,
						innum + " " + percent + minus,
						percent + " " + innum + minus,
						percent + " " + minus + innum,
						innum + minus + " " + percent
					};
					string[] plusrange =
					{
						innum + " " + percent,
						innum + percent,
						percent + innum,
						percent + " " + innum
					};

					// odśwież podgląd przy wyborze wzorca liczby ujemnej
					if( refresh && this.cbMinusPattern.Items.Count == 12 )
						for( int x = 0; x < 12; ++x )
							this.cbMinusPattern.Items[x] = minusrange[x];
					else
					{
						this.cbMinusPattern.Items.Clear();
						this.cbMinusPattern.Items.AddRange( minusrange );
					}

					// odśwież podgląd przy wyborze wzorca liczby dodatniej
					if( refresh && this.cbPlusPattern.Items.Count == 4 )
						for( int x = 0; x < 4; ++x )
							this.cbPlusPattern.Items[x] = plusrange[x];
					else
					{
						this.cbPlusPattern.Items.Clear();
						this.cbPlusPattern.Items.AddRange( plusrange );
					}
				}
				break;
				// data
				case 3:
					if( refresh )
						return;

					// typy podrzędne
					this.cbSecType.Items.Clear();
					this.cbSecType.Items.Add( Language.GetLine("TypeSettings", "ColumnSubTypes", 8) );

					// wzorce liczb
					this.cbMinusPattern.Items.Clear();
					this.cbMinusPattern.Items.Add( Language.GetLine("TypeSettings", "ColumnSubTypes", 8) );
					
					this.cbPlusPattern.Items.Clear();
					this.cbPlusPattern.Items.Add( Language.GetLine("TypeSettings", "ColumnSubTypes", 8) );
				break;
				// waluta
				case 4:
				{
					// typy podrzędne
					if( !refresh )
					{
						this.cbSecType.Items.Clear();
						this.cbSecType.Items.Add( Language.GetLine("TypeSettings", "ColumnSubTypes", 8) );
					}

					// sformułuj podgląd liczby
					string   innum      = "1" + this.tbGroupSeparator.Text + "234" +
										  this.tbGroupSeparator.Text + "567" + this.tbSeparator.Text + "89";
					string   minus      = this.tbMinusSign.Text;
					string   currency   = this.tbCurrency.Text;
					string[] minusrange =
					{
						"(" + currency + innum + ")",
						minus + currency + innum,
						currency + minus + innum,
						currency + innum + minus,
						"(" + innum + currency + ")",
						minus + innum + currency,
						innum + minus + currency,
						innum + currency + minus,
						minus + innum + " " + currency,
						minus + currency + " " + innum,
						innum + " " + currency + minus,
						currency + " " + innum + minus,
						currency + " " + minus + innum,
						innum + minus + " " + currency,
						"(" + currency + " " + innum + ")",
						"(" + innum + " " + currency + ")"
					};
					string[] plusrange =
					{
						currency + innum,
						innum + currency,
						currency + " " + innum,
						innum + " " + currency
					};

					// odśwież podgląd przy wyborze wzorca liczby ujemnej
					if( refresh && this.cbMinusPattern.Items.Count == 16 )
						for( int x = 0; x < 16; ++x )
							this.cbMinusPattern.Items[x] = minusrange[x];
					else
					{
						this.cbMinusPattern.Items.Clear();
						this.cbMinusPattern.Items.AddRange( minusrange );
					}

					// odśwież podgląd przy wyborze wzorca liczby dodatniej
					if( refresh && this.cbPlusPattern.Items.Count == 4 )
						for( int x = 0; x < 4; ++x )
							this.cbPlusPattern.Items[x] = plusrange[x];
					else
					{
						this.cbPlusPattern.Items.Clear();
						this.cbPlusPattern.Items.AddRange( plusrange );
					}
				}
				break;
			}
		}

		/// <summary>
		/// Przełącznik aktywności kontrolek (aktywna/nieaktywna).
		/// Przełącza stany kontrolki w zależności od typu podrzędnego danych kolumny.
		/// </summary>
		/// 
		/// <seealso cref="TypeSettings"/>
		//* ============================================================================================================
		protected void SwitchControls()
		{
			this.cbSecType.Enabled        = true;
			this.tbDateFormat.Enabled     = true;
			this.tbSeparator.Enabled      = true;
			this.tbGroupSeparator.Enabled = true;
			this.cbMinusPattern.Enabled   = true;
			this.cbPlusPattern.Enabled    = true;
			this.tbCurrency.Enabled       = true;
			this.tbNaN.Enabled            = true;
			this.tbMinusInf.Enabled       = true;
			this.tbPlusInf.Enabled        = true;
			this.tbMinusSign.Enabled      = true;
			this.tbPlusSign.Enabled       = true;

			this.tlPreview.Hide();

			// chowaj ustawienia odpowiednio dla wybranego typu
			switch( this._info.Subtype )
			{
				// ustawienia daty
				case DATATYPE.Date:
					this.cbSecType.Enabled        = false;
					this.tbSeparator.Enabled      = false;
					this.tbGroupSeparator.Enabled = false;
					this.cbMinusPattern.Enabled   = false;
					this.cbPlusPattern.Enabled    = false;
					this.tbCurrency.Enabled       = false;
					this.tbNaN.Enabled            = false;
					this.tbMinusInf.Enabled       = false;
					this.tbPlusInf.Enabled        = false;
					this.tbMinusSign.Enabled      = false;
					this.tbPlusSign.Enabled       = false;

					this.tlPreview.Show();
				break;
				// ustawienia typów całkowitych
				case DATATYPE.Int16:
				case DATATYPE.Int32:
				case DATATYPE.Int64:
					this.tbDateFormat.Enabled  = false;
					this.tbSeparator.Enabled   = false;
					this.cbPlusPattern.Enabled = false;
					this.tbCurrency.Enabled    = false;
					this.tbNaN.Enabled         = false;
					this.tbMinusInf.Enabled    = false;
					this.tbPlusInf.Enabled     = false;
					this.cbPlusPattern.Enabled = false;
				break;
				// ustawienia typów zmiennoprzecinkowych
				case DATATYPE.Single:
				case DATATYPE.Double:
					this.tbDateFormat.Enabled  = false;
					this.cbPlusPattern.Enabled = false;
					this.tbCurrency.Enabled    = false;
				break;
				// ustawienia walut i procentów
				case DATATYPE.Currency:
					this.cbSecType.Enabled    = false;
					this.tbDateFormat.Enabled = false;
					this.tbNaN.Enabled        = false;
					this.tbMinusInf.Enabled   = false;
					this.tbPlusInf.Enabled    = false;
				break;
				case DATATYPE.Percent:
					this.tbDateFormat.Enabled     = false;
					this.tbNaN.Enabled            = false;
					this.tbMinusInf.Enabled       = false;
					this.tbPlusInf.Enabled        = false;
					this.tbGroupSeparator.Enabled = false;
				break;
				// ustawienia typów numerycznych
				case DATATYPE.Numeric:
					this.tbDateFormat.Enabled  = false;
					this.tbNaN.Enabled         = false;
					this.tbMinusInf.Enabled    = false;
					this.tbPlusInf.Enabled     = false;
					this.cbPlusPattern.Enabled = false;
				break;
			}
		}
#endregion

#region AKCJE KONTROLEK
		/// @cond ACTIONS

		/// <summary>
		/// Akcja wywoływana przy rysowaniu tabeli.
		/// Rysuje linię na samej górze oddzielającą treść od "paska informacji" w oknie.
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
		/// Akcja wykonywana po wciśnięciu przycisku "Wczytaj".
		/// Wczytuje lokalizację o nazwie przekazanej w polu tekstowym znajdującym się obok przycisku.
		/// W przypadku wystąpienia wyjątku wyświetla błąd w programie.
		/// </summary>
		/// 
		/// <param name="sender">Obiekt wywołujący zdarzenie.</param>
		/// <param name="ev">Argumenty zdarzenia.</param>
		//* ============================================================================================================
		private void bLoadCulture_Click( object sender, EventArgs ev )
		{
			string culture = this.tbCulture.Text;

			// wczytaj kulturę i uzupełnij formularz nowymi danymi
			try
			{
				CultureInfo cinfo = CultureInfo.GetCultureInfo( culture );
				this.FillControlsFromCulture( cinfo );
				this._culture = cinfo;
			}
			catch( Exception ex )
			{
				// ewidentnie błąd wczytywania kultury
				Program.LogError
				(
					String.Format( Language.GetLine("TypeSettings", "Messages", 0), culture ),
					Language.GetLine( "MessageNames", 5 ),
					false,
					ex
				);
			}
		}

		/// <summary>
		/// Akcja wywoływana po zmianie separatora grupy w polu tekstowym.
		/// Po kliknięciu odświeża "podgląd" liczby w polach wzorca dla liczby dodatniej i ujemnej.
		/// </summary>
		/// 
		/// <param name="sender">Obiekt wywołujący zdarzenie.</param>
		/// <param name="ev">Argumenty zdarzenia.</param>
		//* ============================================================================================================
		private void tbGroupSeparator_TextChanged( object sender, EventArgs ev )
		{
			this.FillComboBoxes( true );
			this._info.GroupSeparator = this.tbGroupSeparator.Text;
		}

		/// <summary>
		/// Akcja wywoływana po zmianie znaku liczby ujemnej w polu tekstowym.
		/// Po kliknięciu odświeża "podgląd" liczby w polach wzorca dla liczby dodatniej i ujemnej.
		/// </summary>
		/// 
		/// <param name="sender">Obiekt wywołujący zdarzenie.</param>
		/// <param name="ev">Argumenty zdarzenia.</param>
		//* ============================================================================================================
		private void tbMinusSign_TextChanged( object sender, EventArgs ev )
		{
			this.FillComboBoxes( true );
			this._info.NegativeSign = this.tbMinusSign.Text;
		}

		/// <summary>
		/// Akcja wywoływana po zmianie separatora oddzielającego część całkowitą od dziesiętnej w polu tekstowym.
		/// Po kliknięciu odświeża "podgląd" liczby w polach wzorca liczby dodatniej i ujemnej.
		/// </summary>
		/// 
		/// <param name="sender">Obiekt wywołujący zdarzenie.</param>
		/// <param name="ev">Argumenty zdarzenia.</param>
		//* ============================================================================================================
		private void tbSeparator_TextChanged( object sender, EventArgs ev )
		{
			this.FillComboBoxes( true );
			this._info.Separator = this.tbSeparator.Text;
		}
		
		/// <summary>
		/// Akcja wywoływana po zmianie symbolu liczby w polu tekstowym.
		/// Po kliknięciu odświeża "podgląd" liczby w polach wzorca liczby dodatniej i ujemnej.
		/// </summary>
		/// 
		/// <param name="sender">Obiekt wywołujący zdarzenie.</param>
		/// <param name="ev">Argumenty zdarzenia.</param>
		//* ============================================================================================================
		private void tbCurrency_TextChanged( object sender, EventArgs ev )
		{
			this.FillComboBoxes( true );

			if( this._info.Subtype == DATATYPE.Percent )
				this._percent = this.tbCurrency.Text;
			else if( this._info.Subtype == DATATYPE.Permille )
				this._permille = this.tbCurrency.Text;

			this._info.NumberSymbol = this.tbCurrency.Text;
		}

		/// <summary>
		/// Akcja wywoływana po zmianie typu podrzędnego kolumny w polu wyboru.
		/// Po kliknięciu odświeża "podgląd" liczby w polach wzorca liczby dodatniej i ujemnej.
		/// </summary>
		/// 
		/// <param name="sender">Obiekt wywołujący zdarzenie.</param>
		/// <param name="ev">Argumenty zdarzenia.</param>
		//* ============================================================================================================
		private void cbSecType_SelectedIndexChanged( object sender, EventArgs ev )
		{
			// data i waluty - nie ma zmian
			if( this._type == 3 || this._type == 4 )
				return;

			// procenty / promile
			if( this._type == 2 )
			{
				if( this.cbSecType.SelectedIndex == 0 )
				{
					this._info.Subtype   = DATATYPE.Percent;
					this.tbCurrency.Text = this._percent;
				}
				else
				{
					this._info.Subtype   = DATATYPE.Permille;
					this.tbCurrency.Text = this._permille;
				}

				this.FillComboBoxes( true );
				return;
			}

			// liczby
			switch( this.cbSecType.SelectedIndex )
			{
				case 0:
					this._info.Subtype = DATATYPE.Int16;
					this._info.Type    = DATATYPE.Integer;
				break;
				case 1:
					this._info.Subtype = DATATYPE.Int32;
					this._info.Type    = DATATYPE.Integer;
				break;
				case 2:
					this._info.Subtype = DATATYPE.Int64;
					this._info.Type    = DATATYPE.Integer;
				break;
				case 3:
					this._info.Subtype = DATATYPE.Single;
					this._info.Type    = DATATYPE.Float;
				break;
				case 4:
					this._info.Subtype = DATATYPE.Double;
					this._info.Type    = DATATYPE.Float;
				break;
				case 5:
					this._info.Subtype = DATATYPE.Numeric;
					this._info.Type    = DATATYPE.Float;
				break;
			}

			this.FillComboBoxes( true );
			this.SwitchControls();
		}

		/// <summary>
		/// Akcja wywoływana po zmianie formatu daty w polu tekstowym.
		/// </summary>
		/// 
		/// <param name="sender">Obiekt wywołujący zdarzenie.</param>
		/// <param name="ev">Argumenty zdarzenia.</param>
		//* ============================================================================================================
		private void tbDateFormat_TextChanged( object sender, EventArgs ev )
		{
			this._info.DateFormat = this.tbDateFormat.Text;

			try
			{ this.tbDatePreview.Text = DateTime.Now.ToString( this.tbDateFormat.Text, this._culture ); }
			catch
				{ this.tbDatePreview.Text = String.Format( Language.GetLine("TypeSettings", "Messages", 1) ); }
		}

		/// <summary>
		/// Akcja wywoływana po zmianie wzorca liczby ujemnej w polu wyboru.
		/// </summary>
		/// 
		/// <param name="sender">Obiekt wywołujący zdarzenie.</param>
		/// <param name="ev">Argumenty zdarzenia.</param>
		//* ============================================================================================================
		private void cbMinusPattern_SelectedIndexChanged( object sender, EventArgs ev )
		{
			this._info.NegativePattern = this.cbMinusPattern.SelectedIndex;
		}

		/// <summary>
		/// Akcja wywoływana po zmianie wzorca liczby dodatniej w polu wyboru.
		/// </summary>
		/// 
		/// <param name="sender">Obiekt wywołujący zdarzenie.</param>
		/// <param name="ev">Argumenty zdarzenia.</param>
		//* ============================================================================================================
		private void cbPlusPattern_SelectedIndexChanged( object sender, EventArgs ev )
		{
			this._info.PositivePattern = this.cbPlusPattern.SelectedIndex;
		}

		/// <summary>
		/// Akcja wywoływana po zmianie nazwy reprezentującej typ "NaN" w polu tekstowym.
		/// </summary>
		/// 
		/// <param name="sender">Obiekt wywołujący zdarzenie.</param>
		/// <param name="ev">Argumenty zdarzenia.</param>
		//* ============================================================================================================
		private void tbNaN_TextChanged( object sender, EventArgs ev )
		{
			this._info.NaN = this.tbNaN.Text;
		}
		
		/// <summary>
		/// Akcja wywoływana po zmianie nazwy reprezentującej minus nieskończoność w polu tekstowym.
		/// </summary>
		/// 
		/// <param name="sender">Obiekt wywołujący zdarzenie.</param>
		/// <param name="ev">Argumenty zdarzenia.</param>
		//* ============================================================================================================
		private void tbMinusInf_TextChanged( object sender, EventArgs ev )
		{
			this._info.NegativeInfinity = this.tbMinusInf.Text;
		}
		
		/// <summary>
		/// Akcja wywoływana po zmianie nazwy reprezentującej plus nieskończoność w polu tekstowym.
		/// </summary>
		/// 
		/// <param name="sender">Obiekt wywołujący zdarzenie.</param>
		/// <param name="ev">Argumenty zdarzenia.</param>
		//* ============================================================================================================
		private void tbPlusInf_TextChanged( object sender, EventArgs ev )
		{
			this._info.PositiveInfinity = this.tbPlusInf.Text;
		}

		/// <summary>
		/// Akcja wywoływana po zmianie znaku reprezentującego liczbę dodatnią w polu tekstowym.
		/// </summary>
		/// 
		/// <param name="sender">Obiekt wywołujący zdarzenie.</param>
		/// <param name="ev">Argumenty zdarzenia.</param>
		//* ============================================================================================================
		private void tbPlusSign_TextChanged( object sender, EventArgs ev )
		{
			this._info.PositiveSign = this.tbPlusSign.Text;
		}
		
		/// <summary>
		/// Akcja wywoływana po kliknięciu przycisku "Zapisz".
		/// Zapisuje nowe dane, kopiując je z wcześniej przygotowanej kopii struktury.
		/// </summary>
		/// 
		/// <param name="sender">Obiekt wywołujący zdarzenie.</param>
		/// <param name="ev">Argumenty zdarzenia.</param>
		//* ============================================================================================================
		private void bSave_Click( object sender, EventArgs ev )
		{
			// zapisz zmiany do oryginału
			if( this._original != null )
				this._original.Clone( this._info );

			GC.Collect();
			this.Close();
		}

		/// <summary>
		/// Akcja wywoływana przy walidacji danych kontrolki.
		/// Sprawdza czy pole tekstowe zawiera poprawną formę dla parsowania daty.
		/// </summary>
		/// 
		/// <param name="sender">Obiekt wywołujący zdarzenie.</param>
		/// <param name="ev">Argumenty zdarzenia.</param>
		//* ============================================================================================================
		private void tbDateFormat_Validating( object sender, CancelEventArgs ev )
		{
			try
			{
				string dateinstr = DateTime.Now.ToString( this.tbDateFormat.Text, this._culture );
				DateTime.ParseExact( dateinstr, this.tbDateFormat.Text, this._culture );
			}
			catch( Exception ex )
			{
				// ewidentnie błąd wczytywania kultury
				Program.LogError
				(
					String.Format( Language.GetLine("TypeSettings", "Messages", 1) ),
					Language.GetLine( "MessageNames", 5 ),
					false,
					ex
				);
				this.tbDateFormat.Text = this._culture.DateTimeFormat.ShortDatePattern;
			}
		}
		/// @endcond ACTIONS
#endregion
	}
}
