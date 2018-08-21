///
/// $u03 Structures.cs
/// 
/// Plik zawiera klasę pozwalającą na zarządzanie ustawieniami.
/// Umożliwia operacje takie jak, odczyt pliku konfiguracyjnego i zapis.
/// Dodatkowo pozwala na operacje na liście ostatnio otwieranych wzorów, ponieważ w pewnym sensie
/// plik ten też jest częścią konfiguracji aplikacji.
/// Klasa sama wczytuje ustawienia do struktury - wie która wartość jest do którego elementu.
/// 
/// Autor: Kamil Biały
/// Od wersji: 0.3.x.x
/// Ostatnia zmiana: 2016-12-25
/// 
/// CHANGELOG:
/// [10.05.2015] Pierwsza wersja klasy.
/// [16.05.2015] Nowe atrybuty w FieldExtraData, FieldData, PageExtraData i PageData.
/// [01.06.2015] Struktura z ustawieniami aplikacji.
/// [04.06.2015] Nowe pola w FieldData i SettingsInfo, dane filtrowania, WinAPIConst dla ComboBox,
///              struktury dla grupowego pola wyboru, struktura filtrowania.
/// [06.08.2015] Zmiana nazwy pliku z Structs na Structures, typy numeryczne dla języków, typ numeryczny
///              dla wczytywanych bitmap, typ numeryczny dla filtra, rozszerzenie ustawień aplikacji.
/// [14.08.2016] Struktura informacji o kolumnie, typ danych, struktura zmiennych globalnych, typ numeryczny
///              dla zakończeń linii i ujednolicony typ numeryczny dla danych językowych.
/// [14.11.2016] Struktura informacji o wzorze, dodatkowe pozycje dla typu z danymi językowymi.
/// [04.12.2016] Struktura dla danych o aktualizacji, dodatkowe pozycje dla typu z danymi językowymi.
/// [25.12.2016] Porzędkowanie kodu, komentarze, regiony.
///

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Globalization;

using CDesigner.Forms;

namespace CDesigner.Utils
{
#region FILTROWANIE

    /// 
    /// <summary>
    /// Szczegóły dotyczące typu wczytanej kolumny.
    /// Klasa zawiera wszelkie dane na temat kolumny utworzonej z wczytanego pliku.
    /// Całość używana jest podczas filtrowania i zapisu przefiltrowanych danych.
    /// Klasa na razie w trybie testowym.
    /// </summary>
    /// 
	public class ColumnTypeInfo
	{
        /// <summary>Typ danych dla kolumny.</summary>
		public DATATYPE Type;

        /// <summary>Wybrany typ danych.</summary>
		public int SelectedType;

        /// <summary>Podrzędny typ danych dla kolumny.</summary>
		public DATATYPE Subtype;

        /// <summary>Aktualna wartość kulturowa.</summary>
		public string Culture;

        /// <summary>Format daty używany przez daną narodowość.</summary>
		public string DateFormat;

        /// <summary>Separator oddzielający część całkowitą od części dziesiętnych.</summary>
		public string Separator;

        /// <summary>Separator używany pomiędzy częściami liczby.</summary>
		public string GroupSeparator;

        /// <summary>Wzór dla wyświetlania liczb ujemnych.</summary>
		public int NegativePattern;

        /// <summary>Wzór dla wyświetlania liczb dodatnich.</summary>
		public int PositivePattern;
        
        /// <summary>Symbol dodawany do wyświetlanej liczby.</summary>
		public string NumberSymbol;

        /// <summary>Symbol NaN - wyświetlany gdy wartość nie jest liczbą.</summary>
		public string NaN;

        /// <summary>Symbol ujemnej nieskończoności.</summary>
		public string NegativeInfinity;

        /// <summary>Symbol dodatniej nieskończoności.</summary>
		public string PositiveInfinity;

        /// <summary>Znak używany do przedstawienia liczb poniżej zera.</summary>
		public string NegativeSign;

        /// <summary>Znak używany do przedstawienia liczb powyżej zera.</summary>
		public string PositiveSign;
        
        /// <summary>
        /// Konstruktor klasy używany przy jej tworzeniu.
        /// Uzupełnia domyślnymi danymi wszystkie zmienne.
        /// </summary>
        //* ============================================================================================================
		public ColumnTypeInfo()
		{
			// domyślnie jest to tekst
			this.Type             = DATATYPE.String;
            this.SelectedType     = 0;
			this.Subtype          = DATATYPE.NotSet;
            this.Culture          = "";
            this.DateFormat       = "";
            this.Separator        = "";
            this.GroupSeparator   = "";
            this.NegativePattern  = 0;
            this.PositivePattern  = 0;
            this.NumberSymbol     = "";
            this.NaN              = "";
            this.NegativeInfinity = "";
            this.PositiveInfinity = "";
            this.NegativeSign     = "";
            this.PositiveSign     = "";
		}

        /// <summary>
        /// Klonowanie podanej klasy.
        /// Funkcja klonuje podaną klasę uzupełniając aktualną danymi z podanej.
        /// </summary>
        /// 
        /// <param name="info">Klasa do klonowania.</param>
        //* ============================================================================================================
		public void Clone( ColumnTypeInfo info )
		{
			this.Type             = info.Type;
			this.SelectedType     = info.SelectedType;
			this.Subtype          = info.Subtype;
			this.Culture          = info.Culture;
			this.DateFormat       = info.DateFormat;
			this.Separator        = info.Separator;
			this.GroupSeparator   = info.GroupSeparator;
			this.NegativePattern  = info.NegativePattern;
			this.PositivePattern  = info.PositivePattern;
			this.NumberSymbol     = info.NumberSymbol;
			this.NaN              = info.NaN;
			this.NegativeInfinity = info.NegativeInfinity;
			this.PositiveInfinity = info.PositiveInfinity;
			this.NegativeSign     = info.NegativeSign;
			this.PositiveSign     = info.PositiveSign;
		}

        /// <summary>
        /// Funkcja pozwala na utworzenie kopii klasy informacji o wyświetlaniu liczb.
        /// Uzupełnia klasę danymi z podanej klasy informacji o wyświetlaniu liczb.
        /// Dodatkowo domyślnie w przypadku większości atrybutów, uzupełniane są z aktualnej instancji klasy.
        /// Można to zmienić, przestawiając ostatni argument funkcji.
        /// </summary>
        /// 
        /// <param name="info">Pusta klasa informacji o wyświetlaniu liczb.</param>
        /// <param name="cinfo">Klasa informacji o wyświetlaniu liczb do skopiowania.</param>
        /// <param name="fstruct">Kopiowanie wybranych elementów z instancji klasy.</param>
        //* ============================================================================================================
		public void FillNumberInfo( NumberFormatInfo info, NumberFormatInfo cinfo, bool fstruct = true )
		{
			info.CurrencyDecimalDigits    = cinfo.CurrencyDecimalDigits;
			info.CurrencyDecimalSeparator = fstruct ? this.Separator        : cinfo.CurrencyDecimalSeparator;
			info.CurrencyGroupSeparator   = fstruct ? this.GroupSeparator   : cinfo.CurrencyGroupSeparator;
			info.CurrencyGroupSizes       = cinfo.CurrencyGroupSizes;
			info.CurrencyNegativePattern  = fstruct ? this.NegativePattern  : cinfo.CurrencyNegativePattern;
			info.CurrencyPositivePattern  = fstruct ? this.PositivePattern  : cinfo.CurrencyPositivePattern;
			info.CurrencySymbol           = fstruct ? this.NumberSymbol     : cinfo.CurrencySymbol;
			info.DigitSubstitution        = cinfo.DigitSubstitution;
			info.NaNSymbol                = fstruct ? this.NaN              : cinfo.NaNSymbol;
			info.NativeDigits             = cinfo.NativeDigits;
			info.NegativeInfinitySymbol   = fstruct ? this.NegativeInfinity : cinfo.NegativeInfinitySymbol;
			info.NegativeSign             = fstruct ? this.NegativeSign     : cinfo.NegativeSign;
			info.NumberDecimalDigits      = cinfo.NumberDecimalDigits;
			info.NumberDecimalSeparator   = fstruct ? this.Separator        : cinfo.NumberDecimalSeparator;
			info.NumberGroupSeparator     = fstruct ? this.GroupSeparator   : cinfo.NumberGroupSeparator;
			info.NumberGroupSizes         = cinfo.NumberGroupSizes;
			info.NumberNegativePattern    = fstruct ? this.NegativePattern  : cinfo.NumberNegativePattern;
			info.PercentDecimalDigits     = cinfo.PercentDecimalDigits;
			info.PercentDecimalSeparator  = fstruct ? this.Separator        : cinfo.PercentDecimalSeparator;
			info.PercentGroupSeparator    = fstruct ? this.GroupSeparator   : cinfo.PercentGroupSeparator;
			info.PercentGroupSizes        = cinfo.PercentGroupSizes;
			info.PercentNegativePattern   = fstruct ? this.NegativePattern  : cinfo.PercentNegativePattern;
			info.PercentPositivePattern   = fstruct ? this.PositivePattern  : cinfo.PercentPositivePattern;
			info.PercentSymbol            = fstruct ? this.NumberSymbol     : cinfo.PercentSymbol;
			info.PerMilleSymbol           = fstruct ? this.NumberSymbol     : cinfo.PerMilleSymbol;
			info.PositiveInfinitySymbol   = fstruct ? this.PositiveInfinity : cinfo.PositiveInfinitySymbol;
			info.PositiveSign             = fstruct ? this.PositiveSign     : cinfo.PositiveSign;
		}
	};

    /// 
    /// <summary>
    /// Struktura zmiennych globalnych.
    /// Zawiera główne formularze używane we wszystkich miejscach aplikacji.
    /// Dzięki temu formularze tworzone są raz podczas tworzenia programu, co sprawia że ich pokazywanie nie będzie
    /// narzucało dodatkowego czasu oczekiwania.
    /// </summary>
    /// 
	public struct GlobalStruct
	{
        /// <summary>Formularz wyboru pliku.</summary>
		public OpenFileDialog SelectFile;

        /// <summary>Formularz zapisu pliku.</summary>
        public SaveFileDialog SaveFile;
		
        /// <summary>Formularz ustawień wczytywanego pliku.</summary>
        public DatafileSettingsForm DatafileSettings;

        /// <summary>Formularz informacji o programie.</summary>
        public InfoForm Info;
	};

    /// 
    /// <summary>
    /// Informacje o tworzonym filtrze dla kolumny.
    /// Klasa zawiera informacje o filtrze dla wybranej kolumny.
    /// Każda kolumna może mieć kilka filtrów, oprócz tych, które są składanką kilku kolumn.
    /// Takie kolumny mogą mieć tylko jeden filtr, nazywany formatem kolumny.
    /// </summary>
    /// 
	public class FilterInfo
	{
        /// <summary>Identyfikator kolumny podrzędnej, gdy jest to składanka kilku kolumn.</summary>
		public int SubColumn;

        /// <summary>Typ filtra stosowanego na kolumnie.</summary>
		public FILTERTYPE Filter;

        /// <summary>Modyfikator dla filtra - różne działanie w zależności od filtra.</summary>
		public string Modifier;

        /// <summary>Rezultat dla filtra - różne działania w zależności od filtra.</summary>
		public string Result;

        /// <summary>Wyłączanie pasujących wierszy do wzorca kolumny z danych.</summary>
		public bool Exclude;

        /// <summary>Kopiowanie filtra dla wszystkich powtórzeń kolumny.</summary>
		public bool FilterCopy;
        
        /// <summary>
        /// Konstruktor struktury używany przy jej tworzeniu.
        /// Uzupełnia domyślnymi danymi wszystkie zmienne.
        /// </summary>
        //* ============================================================================================================
        public FilterInfo()
        {
            this.SubColumn  = 0;
            this.Filter     = FILTERTYPE.Format;
            this.Modifier   = "";
            this.Result     = "";
            this.Exclude    = false;
            this.FilterCopy = false;
        }
	}

#endregion

#region TYPY NUMERYCZNE

    /// 
    /// <summary>
    /// Typ numeryczny zawierający kod bitmapy.
    /// Kody bitmap używane są podczas pobierania lub ładowania bitmap do programu.
    /// Pod każdym takim kodem powinna być zdefiniowana osobno ścieżka do bitmapy.
    /// Ścieżki definiowane są w klasie <see cref="Program"/>.
    /// </summary>
    /// 
	public enum BITMAPCODE
	{
		CDesigner16,
		CDesigner32,
		CDesigner48,
		CDesigner64,
		CDesigner96,
		CDesigner128,
		CDesigner256,
		CDesigner512,
		CDRestore16,
		CDRestore32,
		CDRestore48,
		CDRestore64,
		CDRestore96,
		CDRestore128,
		CDRestore256,
		CDRestore512,
        REGISTER_LOGO,
		ItemAdd,
		ItemRemove,
		Refresh,
		FirstPage,
		PrevPage,
		NextPage,
		LastPage,
        IMAGE_FIELD,
        TEXT_FIELD
	};

    /// 
    /// <summary>
    /// Typ numeryczny używany do ustalenia typu danych.
    /// Używany głównie w klasie <see cref="ColumnTypeInfo"/>.
    /// Rozróżnia różne typy kolumn wczytywanych z pliku zawierającego dane.
    /// </summary>
    /// 
	public enum DATATYPE
	{
		NotSet,

		String,
		Integer,
		Float,
		Character,

		Int16,
		Int32,
		Int64,
		Single,
		Double,
		Numeric,
		Currency,
		Percent,
		Permille,
		Date,

		TypeText = 0,
		TypeNumber,
		TypePercent,
		TypeDate,
		TypeCurrency,
		TypeChar
	};

    /// <summary>
    /// Typ numeryczny dla zakończeń linii.
    /// Systemy posiadają różne zakończenia linii, tutaj są wylistowane z nazwami
    /// od głównych systemów, które je posiadają.
    /// Lista dostępnych znaków końca linii:
    /// 
	/// <list type="bullet">
	///		<item><description>Windows <i>(CRLF)</i></description></item>
	///		<item><description>Unix <i>(LF)</i></description></item>
	///		<item><description>Macintosh <i>(CR)</i></description></item>
	/// </list>
    /// </summary>
	public enum LINEENDING
	{
		Undefined,
		Windows,
		Unix,
		Macintosh
	};

    /// <summary>
    /// Typ numeryczny dla linii językowych.
    /// Ten typ numeryczny pozwala na szybkie dostanie się do konkretnej linii z plinu językowego.
    /// Kod podpowiada gdzie dane tłumaczenie się znajduje poprzez zastosowane skróty.
    /// Lista sekcji i podsekcji z plików tłumaczeniowych z wypisanymi skrótami:
    /// 
	/// <list type="bullet">
	///		<item><description>GlobalErrors [p] <i>(GGE)</i> - lista błędów globalnych.</description></item>
	///		<item><description>FormNames [p] <i>(GFN)</i> - nazwy formularzy występujących w aplikacji.</description></item>
	///		<item><description>Locale [p] <i>(GLO)</i> - znaki specjalne używane do filtrowania.</description></item>
	///		<item><description>Extensions [p] <i>(GEX)</i> - lista rozszerzeń aplikacji.</description></item>
	///		<item><description>MessageNames [p] <i>(GMN)</i> - nazwy okienek wyskakujących komunikatów.</description></item>
	///		<item><description>Menu [s] <i>(I01_MEN)</i> - tłumaczenia menu głównego programu.</description></item>
	///		<item><description>PatternList [s] <i>(I01_PAT)</i> - tłumaczenia listy wzorów.</description></item>
	///		<item><description>PatternEditor [s] <i>(I01_EDI)</i> - tłumaczenia edytora wzorów.</description></item>
	///		<item><description>PrintoutPreview [s] <i>(I01_PRI)</i> - tłumaczenia podglądu wydruku.</description></item>
	///		<item><description>DatafileSettings [s] <i>(I02)</i> - ustawienia wczytywanego pliku z danymi.</description></item>
	///		<item><description>EditColumns [s] <i>(I03)</i> - filtrowanie i edycja kolumn wczytanych danych.</description></item>
	///		<item><description>EditRows [s] <i>(I04)</i> - edycja wierszy dla wczytanych danych.</description></item>
	///		<item><description>NewPattern [s] <i>(I05)</i> - formularz tworzenia nowego wzoru.</description></item>
	///		<item><description>Info [s] <i>(I06)</i> - formularz z informacjami o programie.</description></item>
	///		<item><description>Update [s] <i>(I07)</i> - formularz aktualizacji programu.</description></item>
	///		<item><description>DataReader [s] <i>(I08)</i> - przypisywanie kolumn do dostępnych pól wzoru.</description></item>
	/// </list>
    /// </summary>
	public enum LANGCODE
	{
		// /////////////////////////// GGE [GlobalErrors]

		GGE_CRITICALERROR      = 0x00,
		GGE_RUNNINGCDESIGNER   = 0x01,
		GGE_RUNNINGCDRESTORE   = 0x02,
		GGE_OPTIONNOTAVALIABLE = 0x03,

		// /////////////////////////// GFN [GlobalFormNames]

		GFN_DATAFILESETTINGS = 0x01,
		GFN_EDITCOLUMNS      = 0x02,
        GFN_EDITDATA         = 0x03,
        GFN_NEWPATTERN       = 0x04,
        GFN_ABOUTPROGRAM     = 0x05,
        GFN_PROGRAMUPDATE    = 0x06,
        GFN_DATAREADER       = 0x08,
        GFN_CDESIGNER        = 0x09,
        
		// /////////////////////////// GLO [GlobalLocale]

        GLO_BIGCHARS   = 0x00,
        GLO_SMALLCHARS = 0x01,

		// /////////////////////////// GEX [GlobalExtensions]

		GEX_CSV = 0x00,
        GEX_CBD = 0x01,

		// /////////////////////////// GMN [GlobalMessageNames]

        GMN_PARSEERROR     = 0x00,
		GMN_SELECTFILE     = 0x01,
        GMN_FILELOADING    = 0x03,
        GMN_PATTCREATING   = 0x06,
        GMN_SAVEFILE       = 0x07,
        GMN_IMPORTFILE     = 0x08,
        GMN_DATABASE       = 0x09,
        GMN_PATTERNEDITOR  = 0x0A,
        
		// /////////////////////////// I01_MEN [MainForm_Menu]

        I01_MEN_PAT_PATTERN    = 0x00,
        I01_MEN_PAT_NEWPATTERN = 0x01,
        I01_MEN_PAT_RECENTOPEN = 0x02,
        I01_MEN_PAT_CLEARLIST  = 0x03,
        I01_MEN_PAT_CLOSE      = 0x04,
        I01_MEN_PAT_IMPORT     = 0x05,
        I01_MEN_PAT_EXPORTALL  = 0x06,
        I01_MEN_TOL_TOOLS      = 0x00,
        I01_MEN_TOL_LOADFILE   = 0x01,
        I01_MEN_TOL_MEMORYDB   = 0x02,
        I01_MEN_TOL_EDITCOLUMN = 0x03,
        I01_MEN_TOL_EDITROW    = 0x04,
        I01_MEN_TOL_SAVEMEMDB  = 0x05,
        I01_MEN_LAN_LANGUAGE   = 0x00,
        I01_MEN_PRO_PROGRAM    = 0x00,
        I01_MEN_PRO_INFO       = 0x01,
        I01_MEN_PRO_UPDATES    = 0x02,
        I01_MEN_SWI_MAIN       = 0x00,
        I01_MEN_SWI_EDITOR     = 0x01,
        I01_MEN_SWI_GENERATOR  = 0x02,
        I01_MEN_MSG_CREATEMDB  = 0x00,
        I01_MEN_MSG_PATNEXISTS = 0x01,

		// /////////////////////////// I01_PAT [MainForm_Pattern]

        I01_PAT_BUT_NEWPATTERN = 0x00,
        I01_PAT_BUT_DELETE     = 0x01,
        I01_PAT_LAB_PATDETAILS = 0x00,
        I01_PAT_LAB_PAGE       = 0x01,
        I01_PAT_PAT_NAME       = 0x00,
        I01_PAT_PAT_FORMAT     = 0x01,
        I01_PAT_PAT_SIZE       = 0x02,
        I01_PAT_PAT_DATAPLACE  = 0x03,
        I01_PAT_PAT_PAGECOUNT  = 0x04,
        I01_PAT_PAT_OWNFORMAT  = 0x05,
        I01_PAT_PAT_YES        = 0x06,
        I01_PAT_PAT_NO         = 0x07,
        I01_PAT_MES_YESIMPORT  = 0x00,
        I01_PAT_MES_IMPORTSUCC = 0x01,
        I01_PAT_MES_WANTDELETE = 0x02,
        I01_PAT_MES_PATDAMAGED = 0x03,
        I01_PAT_CTX_NEWPATTERN = 0x00,
        I01_PAT_CTX_EDITSELECT = 0x01,
        I01_PAT_CTX_PREVIEW    = 0x02,
        I01_PAT_CTX_LOADDATA   = 0x03,
        I01_PAT_CTX_IMPORT     = 0x04,
        I01_PAT_CTX_EXPORT     = 0x05,
        I01_PAT_CTX_REMOVEPAT  = 0x06,

		// /////////////////////////// I01_EDI [MainForm_Editor]

        I01_EDI_PCX_ADDFIELD   = 0x00,
        I01_EDI_PCX_REMOVEALL  = 0x01,
        I01_EDI_PCX_PAGECOLOR  = 0x02,
        I01_EDI_PCX_PAGEIMAGE  = 0x03,
        I01_EDI_PCX_CLEARBACK  = 0x04,
        I01_EDI_PCX_DRAWCOLOR  = 0x05,
        I01_EDI_PCX_DRAWIMAGE  = 0x06,
        I01_EDI_PCX_ADDPAGE    = 0x07,
        I01_EDI_PCX_REMOVEPAGE = 0x08,
        I01_EDI_LCX_FIELDCOLOR = 0x00,
        I01_EDI_LCX_FIELDIMAGE = 0x01,
        I01_EDI_LCX_CLEARBACK  = 0x02,
        I01_EDI_LCX_IMAGEDYN   = 0x03,
        I01_EDI_LCX_DRAWCOLOR  = 0x04,
        I01_EDI_LCX_IMAGESTAT  = 0x05,
        I01_EDI_LCX_FONTCOLOR  = 0x06,
        I01_EDI_LCX_CHANGEFONT = 0x07,
        I01_EDI_LCX_TEXTDYN    = 0x08,
        I01_EDI_LCX_TEXTSTAT   = 0x09,
        I01_EDI_LCX_BRDRCOLOR  = 0x0A,
        I01_EDI_LCX_DRAWBORDER = 0x0B,
        I01_EDI_LCX_REMOVE     = 0x0C,
        I01_EDI_BUT_LOADDATA   = 0x00,
        I01_EDI_BUT_PREVIEW    = 0x01,
        I01_EDI_BUT_SAVE       = 0x02,
        I01_EDI_BUT_PAGECOLOR  = 0x03,
        I01_EDI_BUT_PAGEIMAGE  = 0x04,
        I01_EDI_BUT_BRDRCOLOR  = 0x05,
        I01_EDI_BUT_FIELDCOLOR = 0x06,
        I01_EDI_BUT_FIELDIMAGE = 0x07,
        I01_EDI_BUT_FONTNAME   = 0x08,
        I01_EDI_BUT_FONTCOLOR  = 0x09,
        I01_EDI_SWI_FIELD      = 0x00,
        I01_EDI_SWI_DETAILS    = 0x01,
        I01_EDI_SWI_PAGE       = 0x02,
        I01_EDI_LAB_PAGE       = 0x00,
        I01_EDI_LAB_POSITIONX  = 0x01,
        I01_EDI_LAB_POSITIONY  = 0x02,
        I01_EDI_LAB_FWIDTH     = 0x03,
        I01_EDI_LAB_FHEIGHT    = 0x04,
        I01_EDI_LAB_FAPPEARNCE = 0x05,
        I01_EDI_LAB_FONT       = 0x06,
        I01_EDI_LAB_TEXTPOS    = 0x07,
        I01_EDI_LAB_TEXTMARGIN = 0x08,
        I01_EDI_LAB_TEXTDRAW   = 0x09,
        I01_EDI_LAB_BORDERSIZE = 0x0A,
        I01_EDI_LAB_FIELDTOPDF = 0x0B,
        I01_EDI_LAB_FIMAGESET  = 0x0C,
        I01_EDI_LAB_FANCHOR    = 0x0D,
        I01_EDI_LAB_FADDMARGIN = 0x0E,
        I01_EDI_LAB_PAGEWIDTH  = 0x0F,
        I01_EDI_LAB_PAGEHEIGHT = 0x10,
        I01_EDI_LAB_PAPPEARNCE = 0x11,
        I01_EDI_LAB_PAGETOPDF  = 0x12,
        I01_EDI_LAB_PIMAGESET  = 0x13,
        I01_EDI_LAB_NEWFIELD   = 0x14,
        I01_EDI_TPO_TOPLEFT    = 0x00,
        I01_EDI_TPO_TOPMIDDLE  = 0x01,
        I01_EDI_TOP_TOPRIGHT   = 0x02,
        I01_EDI_TOP_MIDDLLEFT  = 0x03,
        I01_EDI_TOP_CENTER     = 0x04,
        I01_EDI_TOP_MIDDLRIGHT = 0x05,
        I01_EDI_TOP_BOTTLEFT   = 0x06,
        I01_EDI_TOP_BOTTMIDDLE = 0x07,
        I01_EDI_TOP_BOTTRIGHT  = 0x08,
        I01_EDI_TTR_DONTCHANGE = 0x00,
        I01_EDI_TTR_UPPERCASE  = 0x01,
        I01_EDI_TTR_LOWERCASE  = 0x02,
        I01_EDI_TTR_TITLECASE  = 0x03,
        I01_EDI_CBK_AUTOSAVE   = 0x00,
        I01_EDI_CBK_DRAWPCOLOR = 0x01,
        I01_EDI_CBK_DRAWPIMAGE = 0x02,
        I01_EDI_CBK_MARGINSET  = 0x03,
        I01_EDI_CBK_BORDEROUT  = 0x04,
        I01_EDI_CBK_DRAWBORDER = 0x05,
        I01_EDI_CBK_DRAWFCOLOR = 0x06,
        I01_EDI_CBK_DYNTEXT    = 0x07,
        I01_EDI_CBK_STATTEXT   = 0x08,
        I01_EDI_CBK_STATIMAGE  = 0x09,
        I01_EDI_CBK_DYNIMAGE   = 0x0A,
        I01_EDI_CBK_BORDERFOUT = 0x0B,
        I01_EDI_CBK_ADDIMGMAR  = 0x0C,
        I01_EDI_CBK_ADDTXTMAR  = 0x0D,
        I01_EDI_MES_AVAILCHARS = 0x00,
        I01_EDI_MES_DREMOPAGE  = 0x01,

		// /////////////////////////// I01_PRI [MainForm_Preview]

        I01_PRI_BUT_FINDERRORS = 0x00,
        I01_PRI_BUT_GENERATE   = 0x01,
        I01_PRI_LAB_PAGE       = 0x00,

		// /////////////////////////// I02 [DatafileSettingsForm]

		I02_ENC_DEFAULT    = 0x00,
 		I02_ENC_ASCII      = 0x01,
		I02_ENC_UTF8       = 0x02,
		I02_ENC_UTF16BE    = 0x03,
		I02_ENC_UTF16LE    = 0x04,
		I02_ENC_UTF32      = 0x05,
		I02_ENC_UTF7       = 0x06,
		I02_SEP_SEMICOLON  = 0x00,
		I02_SEP_COMMA      = 0x01,
		I02_SEP_DOT        = 0x02,
		I02_SEP_TAB        = 0x03,
		I02_SEP_SPACE      = 0x04,
		I02_SEP_OTHER      = 0x05,
		I02_HEA_ROWS       = 0x00,
		I02_HEA_COLUMNS    = 0x01,
		I02_BUT_CHANGE     = 0x00,
		I02_BUT_SAVE       = 0x01,
		I02_BUT_CANCEL     = 0x02,
		I02_MSG_UNDEFDBASE = 0x00,
		I02_LAB_AUTODETECT = 0x00,
		I02_LAB_NOHEADERS  = 0x01,

		// /////////////////////////// I03 [EditColumnsForm]

		I03_HEA_COLUMNNAME = 0x00,
		I03_HEA_COLUMNS    = 0x01,
		I03_HEA_ROWS       = 0x02,
		I03_HEA_AVALIABLE  = 0x03,
		I03_HEA_FILTERS    = 0x04,
		I03_HEA_OLDCOLUMNS = 0x05,
		I03_HEA_NEWCOLUMNS = 0x06,
		I03_LAB_COLUMNTYPE = 0x00,
		I03_LAB_FILTERCONF = 0x01,
		I03_LAB_FILTERTYPE = 0x02,
		I03_LAB_MODIFIER   = 0x03,
		I03_LAB_RESULT     = 0x04,
		I03_LAB_EXCLUDE    = 0x05,
		I03_LAB_APPLYCOPY  = 0x06,
		I03_STE_COLDESIGN  = 0x00,
		I03_STE_FILTERTYPE = 0x01,
		I03_BUT_ADD        = 0x00,
		I03_BUT_CLEAR      = 0x01,
		I03_BUT_REMOVE     = 0x02,
		I03_BUT_SAVE       = 0x03,
		I03_BUT_CANCEL     = 0x04,
		I03_BUT_CHANGE     = 0x05,
		I03_BUT_CHANGETYPE = 0x06,
		I03_BUT_ADVANCED   = 0x07,
		I03_COT_TEXT       = 0x00,
		I03_COT_NUMBER     = 0x01,
		I03_COT_PERCENT    = 0x02,
		I03_COT_DATE       = 0x03,
		I03_COT_CURRENCY   = 0x04,
		I03_COT_CHARACTER  = 0x05,
		I03_COT_DEFAULT    = 0x06,
		I03_FIT_UPPERCASE  = 0x00,
		I03_FIT_LOWERCASE  = 0x01,
		I03_FIT_TITLECASE  = 0x02,
		I03_FIT_EQUAL      = 0x03,
		I03_FIT_NOTEQUAL   = 0x04,
		I03_FIT_FORMAT     = 0x05,
		I03_MES_AVALCHARS  = 0x00,
		I03_MES_NOCOLNAME  = 0x01,
		I03_MES_COLEXISTS  = 0x02,
		I03_MES_OVERRIDE   = 0x03,
        
		// /////////////////////////// I04 [EditRowsForm]

        I04_LAB_ROWSONPAGE = 0x00,
        I04_LAB_PAGEOFNUM  = 0x01,
        I04_BUT_SAVE       = 0x00,
        I04_BUT_CANCEL     = 0x01,
        
		// /////////////////////////// I05 [NewPatternForm]

        I05_LAB_PATTNAME   = 0x00,
        I05_LAB_PATTCOPY   = 0x01,
        I05_LAB_PAPERTYPE  = 0x02,
        I05_LAB_PATTSIZE   = 0x03,
        I05_CBX_DONTCOPY   = 0x00,
        I05_CBX_CUSTOMSIZE = 0x01,
        I05_BUT_CREATE     = 0x00,
        I05_BUT_CANCEL     = 0x01,
        I05_MES_NOPATTNAME = 0x00,
        I05_MES_PATTEXISTS = 0x01,
        I05_MES_NOPATTSIZE = 0x02,
        I05_MES_CANTCREATE = 0x03,
        I05_MES_AVALCHARS  = 0x04,
        
		// /////////////////////////// I06 [InfoForm]

        I06_BUT_CLOSE      = 0x00,
        I06_LAB_COMPILDATE = 0x00,
        I06_LAB_AUTHOR     = 0x01,
        I06_LAB_ABOUTCOPY  = 0x02,
        I06_LAB_REGFOR     = 0x03,
        I06_LAB_REGNUMBER  = 0x04,
        I06_LAB_EXPIREDATE = 0x05,
        I06_LAB_EXPIRNEVER = 0x06,
        
		// /////////////////////////// I07 [UpdateForm]

        I07_MSG_NOCONNECT  = 0x00,
        I07_MSG_ADMINHELP  = 0x01,
        I07_MSG_ACTIVEZIP  = 0x02,
        I07_MSG_NACTIVEZIP = 0x03,
        I07_MSG_ERRUPDATE  = 0x04,
        I07_MSG_CLOSEONUP  = 0x05,
        I07_MSG_ERRCRESS   = 0x06,
        I07_MSG_SUCCESSZIP = 0x07,
        I07_MSG_ERRDECRESS = 0x08,
        I07_MSG_INSTALLUP  = 0x09,
        I07_BUT_UPDATE     = 0x00,
        I07_BUT_CLOSE      = 0x01,
        I07_BUT_COMPRESS   = 0x02,
        I07_LAB_AVAILABLE  = 0x00,
        I07_LAB_UPTODATE   = 0x01,
        I07_LAB_APPVERSION = 0x02,
        I07_LAB_VERSIONUP  = 0x03,
        I07_LAB_CHANGES    = 0x04,
        
		// /////////////////////////// I08 [DataReaderForm]

        I08_HEA_PAGEFIELDS = 0x00,
        I08_HEA_PAGECOLS   = 0x01,
        I08_HEA_DBASECOLS  = 0x02,
        I08_LAB_PAGE       = 0x00,
        I08_LAB_NOCOLUMN   = 0x01,
        I08_BUT_SAVE       = 0x00,
        I08_BUT_CANCEL     = 0x01,
        I08_MES_AVAILCHARS = 0x00,
        I08_MES_AVAILCHMSG = 0x01,
        I08_MES_WOUTCOLUMN = 0x02,
        I08_MES_ISEMPTY    = 0x03
	};

    /// <summary>
    /// Typ numeryczny zawierający nazwy filtrów.
    /// Typ zawiera nazwy filtrów używane przy filtrowaniu kolumny.
    /// Przypisywane są do kolumny podczas jej edycji i wykorzystywane przy podglądzie i zapisie danych.
    /// Lista dostępnych filtrów:
    /// </summary>
    /// 
	/// <list type="bullet">
	///		<item><description>LowerCase <i>(Małe litery)</i></description></item>
	///		<item><description>UpperCase <i>(Duże litery)</i></description></item>
	///		<item><description>TitleCase <i>(Format liter jak w nazwie własnej)</i></description></item>
	///		<item><description>Equal <i>(Obiekty są równe)</i></description></item>
	///		<item><description>NotEqual <i>(Obiekty są różne)</i></description></item>
	///		<item><description>Format <i>(Format kolumny zawierającej podkolumny)</i></description></item>
	/// </list>
	public enum FILTERTYPE
	{
		LowerCase  = 0x0,
		UpperCase  = 0x1,
		TitleCase  = 0x2,
		Equal      = 0x3,
		NotEqual   = 0x4,
		Format     = 0x5
	};

    /// 
    /// <summary>
    /// Typ numeryczny dla kodu ustawienia.
    /// Używany generalnie tylko w konstruktorze klasy <see cref="SettingsInfo"/>.
    /// Przydatny podczas przegladania zmiennej zawierającej dane pozostałych zmiennych w strukturze.
    /// </summary>
    ///
	public enum SETTINGCODE
	{
		I02_ROWSNUMBER = 0x04,
		I02_ENCODING,
		I02_SEPARATOR,
        LASTPATSNUM,
	};

#endregion

#region USTAWIENIA

    /// 
    /// <summary>
    /// Struktura zawierająca dane dotyczące wpisu ustawienia.
    /// Używane podczas tworzenia ustawień, na tej strukturze opisany jest każdy wpis ustawienia.
    /// Między innemi dzięki tej strukturze aplikacja wie, co do czego ma być przypisywane.
    /// </summary>
    /// 
	public struct SettingMemberData
	{
        /// <summary>Nazwa ustawienia.</summary>
		public string Name;

        /// <summary>Typ wartości ustawienia.</summary>
		public byte Type;

        /// <summary>Domyślna wartość dla pozycji.</summary>
		public object DefaultValue;
        
        /// <summary>
        /// Konstruktor struktury używany przy jej tworzeniu.
        /// Uzupełnia domyślnymi danymi wszystkie zmienne.
        /// </summary>
        /// 
        /// <param name="name">Nazwa ustawienia.</param>
        /// <param name="type">Typ ustawienia.</param>
        /// <param name="defval">Domyślna wartość.</param>
        //* ============================================================================================================
		public SettingMemberData( string name, byte type, object defval )
		{
			this.Name         = name;
			this.Type         = type;
			this.DefaultValue = defval;
		}
	};

    /// 
    /// <summary>
    /// Struktura zawierająca ustawienia aplikacji.
    /// Wszystkie elementy które zawiera, zostają zapisane do pliku podczas zapisu ustawień.
    /// Podczas wczytywania uzupełniane są te, które istnieją.
    /// Zapobiega to błędom, które mogą wystąpić podczas wczytywania ustawień z innej wersji programu.
    /// Struktura jest w fazie testowej.
    /// </summary>
    /// 
	public struct SettingsInfo
	{
        /// <summary>Informacje o poszczególnych elementach struktury.</summary>
        /// @hideinitializer
		public static readonly SettingMemberData[] MemberData =
		{
			new SettingMemberData( "Language",         1, (object)        "pol" ),
			new SettingMemberData( "EDF_RowsNumber",   4, (object)(Int32) 50    ),
			new SettingMemberData( "ECF_RowsNumberS1", 4, (object)(Int32) 5     ),
			new SettingMemberData( "ECF_RowsNumberS2", 4, (object)(Int32) 10    ),
			new SettingMemberData( "i02_RowsNumber",   4, (object)(Int32) 17    ),
			new SettingMemberData( "i02_Encoding",     3, (object)(Byte)  0     ),
			new SettingMemberData( "i02_Separator",    2, (object)        ';'   ),
            new SettingMemberData( "c02_RecentMax",    3, (object)(Byte)  10    )
		};
        
        /// <summary>
        /// Konstruktor struktury używany przy jej tworzeniu.
        /// Uzupełnia domyślnymi danymi wszystkie zmienne.
        /// Domyślne wartości ustalone są w zmiennej <see cref="MemberData"/>.
        /// </summary>
        /// 
        /// <param name="set_default">Ustaw domyślne wartości.</param>
        //* ============================================================================================================
		public SettingsInfo( bool set_default )
            : this()
		{
			if( set_default )
			{
				this.Language         = (String)SettingsInfo.MemberData[0].DefaultValue;
				this.EDF_RowsNumber   = (Int32 )SettingsInfo.MemberData[1].DefaultValue;
				this.ECF_RowsNumberS1 = (Int32)SettingsInfo.MemberData[2].DefaultValue;
				this.ECF_RowsNumberS2 = (Int32)SettingsInfo.MemberData[3].DefaultValue;
				this.i02_RowsNumber   = (Int32)SettingsInfo.MemberData[(int)SETTINGCODE.I02_ROWSNUMBER].DefaultValue;
				this.i02_Encoding     = (Byte )SettingsInfo.MemberData[(int)SETTINGCODE.I02_ENCODING  ].DefaultValue;
				this.i02_Separator    = (Char )SettingsInfo.MemberData[(int)SETTINGCODE.I02_SEPARATOR ].DefaultValue;
                this.c02_RecentMax    = (Byte )SettingsInfo.MemberData[(int)SETTINGCODE.LASTPATSNUM   ].DefaultValue;
			}
		}

        /// <summary>Limit dla wyświetlanych ostatnio otwieranych wzorów.</summary>
        public Byte c02_RecentMax;

        /// <summary>Ilość wyświetlanych wierszy w ustawieniach wczytywania danych.</summary>
		public Int32 i02_RowsNumber;

        /// <summary>Domyślne kodowanie pliku podczas ustawień wczytywania.</summary>
		public Byte i02_Encoding;

        /// <summary>Domyślny separator dla wczytywanych danych.</summary>
		public Char i02_Separator;

        /// <summary>Ilość wierszy wyświetlanych w polu łączenia kolumn - strona 1.</summary>
		public Int32 ECF_RowsNumberS1;

        /// <summary>Ilość wierszy wyświetlanych w polu łączenia kolumn - strona 2</summary>
		public Int32 ECF_RowsNumberS2;

        /// <summary>Język w którym wyświetlana jest aplikacja.</summary>
		public String Language;

        /// <summary>Domyślna ilość wierszy wyświetlanych podczas edycji wierszy.</summary>
		public Int32 EDF_RowsNumber;
	};

#endregion

#region WZORY

    /// 
    /// <summary>
    /// Dane dodatkowe dla pola wzoru.
    /// Klasa zawiera dodatkowe informacje o wczytywanych danych pola.
    /// Szczegóły w opisie zmiennych.
    /// </summary>
    /// 
	public class FieldExtraData
	{
        /// <summary>Czy obraz pola ma być pobierany z bazy?</summary>
		public bool ImageFromDB;

        /// <summary>Czy kolor ma być generowany na PDF?</summary>
		public bool PrintColor;
        
        /// <summary>Czy obraz statyczny ma być generowany na PDF?</summary>
		public bool PrintImage;

        /// <summary>Czy tekst ma być pobierany z bazy?</summary>
		public bool TextFromDB;

        /// <summary>Czy tekst statyczny ma być generowany na PDF?</summary>
		public bool PrintText;
        
        /// <summary>Czy ramka ma być wyświetlana wokół kontrolki w PDF?</summary>
		public bool PrintBorder;

        /// <summary>Przypisana kolumna dla danych dynamicznych.</summary>
		public int Column;

        /// <summary>Punkt zaczepienia względem którego obliczane są dane pola.</summary>
		public int PosAlign;
        
        /// <summary>
        /// Konstruktor struktury używany przy jej tworzeniu.
        /// Uzupełnia domyślnymi danymi wszystkie zmienne.
        /// </summary>
        //* ============================================================================================================
        public FieldExtraData()
        {
            this.ImageFromDB = false;
            this.PrintColor  = false;
            this.PrintImage  = false;
            this.TextFromDB  = false;
            this.PrintText   = false;
            this.PrintBorder = false;
            this.Column      = 0;
            this.PosAlign    = 0;
        }
	};

    /// 
    /// <summary>
    /// Klasa zawierająca informacje o polu.
    /// Zawiera wszystkie dane na temat pola znajdującego się na stronie.
    /// Uzupełniana podczas wczytywania wzoru, część klasy <see cref="PageData"/>.
    /// Klasa przechowuje oryginalne wartości w milimetrach.
    /// </summary>
    /// 
	public class FieldData
	{
        /// <summary>Napis na kontrolce.</summary>
		public string Name;

        /// <summary>Wartości graniczne pola.</summary>
		public RectangleF Bounds;

        /// <summary>Rozmiar ramki rysowanej wokół pola.</summary>
		public float BorderSize;

        /// <summary>Kolor ramki rysowanej wokół pola.</summary>
		public Color BorderColor;

        /// <summary>Czy jako tło pola używany jest obraz?</summary>
		public bool Image;

        /// <summary>Ściezka do obrazu rysowanego na polu.</summary>
		public string ImagePath;

        /// <summary>Kolor rysowany jako tło kontrolki.</summary>
		public Color Color;

        /// <summary>Kolor czcionki rysowanej na polu.</summary>
		public Color FontColor;

        /// <summary>Nazwa czcionki wyświetlanej na polu.</summary>
		public string FontName;

        /// <summary>Styl rysowanej czcionki.</summary>
		public FontStyle FontStyle;

        /// <summary>Rozmiar czcionki rysowanej na polu.</summary>
		public float FontSize;

        /// <summary>Przyleganie tekstu do krawędzi kontrolki.</summary>
		public ContentAlignment TextAlign;

        /// <summary>Transformacja wyświetlanego tekstu.</summary>
		public int TextTransform;

        /// <summary>Margines z lewej i prawej strony pola.</summary>
		public float TextLeftpad;

        /// <summary>Margines z góry i dołu pola.</summary>
		public float TextToppad;

        /// <summary>Dodatkowy margines dla tekstu.</summary>
		public bool TextAddMargin;

        /// <summary>Margines wewnętrzny tekstu.</summary>
		public float Padding;

        /// <summary>Dane dodatkowe dla pola.</summary>
		public FieldExtraData Extra;
        
        /// <summary>
        /// Konstruktor struktury używany przy jej tworzeniu.
        /// Uzupełnia domyślnymi danymi wszystkie zmienne.
        /// </summary>
        //* ============================================================================================================
        public FieldData()
        {
            this.Name          = "";
            this.Bounds        = new RectangleF( 0.0f, 0.0f, 0.0f, 0.0f );
            this.BorderSize    = 0;
            this.BorderColor   = Color.Transparent;
            this.Image         = false;
            this.ImagePath     = "";
            this.Color         = Color.Transparent;
            this.FontColor     = Color.Transparent;
            this.FontName      = "";
            this.FontSize      = 0;
            this.FontStyle     = FontStyle.Regular;
            this.TextAlign     = ContentAlignment.MiddleCenter;
            this.TextTransform = 0;
            this.TextLeftpad   = 0;
            this.TextToppad    = 0;
            this.TextAddMargin = false;
            this.Padding       = 0;
            this.Extra         = null;
        }
	};

    /// 
    /// <summary>
    /// Dane dodatkowe dla strony wzoru.
    /// Klasa zawiera dodatkowe informacje o wczytywanych stronach wzoru.
    /// Szczegóły w opisie zmiennych.
    /// </summary>
    /// 
	public class PageExtraData
	{
        /// <summary>Czy generować kolor w PDF?</summary>
		public bool PrintColor;

        /// <summary>Czy generować obraz w PDF?</summary>
		public bool PrintImage;

        /// <summary>Ścieżka do obrazu wyświetlanego na stronie.</summary>
		public string ImagePath;
        
        /// <summary>
        /// Konstruktor struktury używany przy jej tworzeniu.
        /// Uzupełnia domyślnymi danymi wszystkie zmienne.
        /// </summary>
        //* ============================================================================================================
        public PageExtraData()
        {
            this.PrintColor = false;
            this.PrintImage = false;
            this.ImagePath  = "";
        }
	};

    /// 
    /// <summary>
    /// Klasa zawierająca informacje o stronie wzoru.
    /// Zawiera wszystkie dane na temat strony utworzonej we wzorze.
    /// Uzupełniana podczas wczytywania wzoru, część klasy <see cref="PatternData"/>.
    /// </summary>
    /// 
	public class PageData
	{
        /// <summary>Ilość pól utworzonych na stronie.</summary>
		public int Fields;

        /// <summary>Czy strona używa obrazu?</summary>
		public bool Image;

        /// <summary>Kolor strony wyświetlany w podglądzie.</summary>
		public Color Color;

        /// <summary>Pola utworzone dla strony.</summary>
		public FieldData[] Field;

        /// <summary>Dane dodatkowe pola.</summary>
		public PageExtraData Extra;
        
        /// <summary>
        /// Konstruktor struktury używany przy jej tworzeniu.
        /// Uzupełnia domyślnymi danymi wszystkie zmienne.
        /// </summary>
        //* ============================================================================================================
        public PageData()
        {
            this.Fields = 0;
            this.Image  = false;
            this.Color  = Color.Transparent;
            this.Field  = null;
            this.Extra  = null;
        }
	};
    
    /// 
    /// <summary>
    /// Klasa zawierająca informacje o wzorze.
    /// Zawiera wszystkie dane przechowywane w plikach konfiguracyjnych wzorów.
    /// Uzupełniana przy listowaniu wzorów i wczytywaniu konkretnego wzoru.
    /// </summary>
    /// 
	public class PatternData
	{
        /// <summary>Nazwa wzoru.</summary>
		public string Name;

        /// <summary>Czy wzór posiada plik konfiguracyjny?</summary>
        public bool HasConfigFile;

        /// <summary>Czy plik konfiguracyjny jest prawidłowy?</summary>
        public bool Corrupted;

        /// <summary>Szerokość i wysokość wzoru w milimetrach.</summary>
		public Size Size;

        /// <summary>Ilość stron znajdujących się we wzorze.</summary>
		public int Pages;

        /// <summary>Czy do wzoru można wczytać dane?</summary>
		public bool Dynamic;

        /// <summary>Dane stron zapisanych we wzorze.</summary>
		public PageData[] Page;
        
        /// <summary>
        /// Konstruktor struktury używany przy jej tworzeniu.
        /// Uzupełnia domyślnymi danymi wszystkie zmienne.
        /// </summary>
        /// 
        /// <param name="name">Nazwa parsowanego wzoru.</param>
        //* ============================================================================================================
        public PatternData( string name )
        {
            this.Name          = name;
            this.HasConfigFile = false;
            this.Corrupted     = false;
            this.Size          = new Size( 0, 0 );
            this.Pages         = 0;
            this.Dynamic       = false;
            this.Page          = null;
        }
	};

#endregion
}
