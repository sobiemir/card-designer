using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Globalization;

using CDesigner.Forms;

namespace CDesigner
{
    public class BackupHeader
    {
        public string CreateDate;
        public string ProgramVersion;
        public byte[] HashTable;
        public int    Files;
        public int[]  FileSizes;
        public long   Size;
        public long   TotalSize;
    };

	public class ColumnTypeInfo
	{
		public ColumnTypeInfo()
		{
			// domyślnie jest to tekst
			this.Type         = DATATYPE.String;
			this.Subtype      = DATATYPE.NotSet;
			this.SelectedType = 0;
		}

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

		public DATATYPE Type;
		public int      SelectedType;
		public DATATYPE Subtype;
		public string   Culture;
		public string   DateFormat;
		public string   Separator;
		public string   GroupSeparator;
		public int      NegativePattern;
		public int      PositivePattern;
		public string   NumberSymbol;
		public string   NaN;
		public string   NegativeInfinity;
		public string   PositiveInfinity;
		public string   NegativeSign;
		public string   PositiveSign;
	};

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
    /// Struktura informacji o wzorze.
    /// Zawiera wszystkie dane przechowywane w plikach konfiguracyjnych wzorów.
    /// Uzupełniana przy listowaniu wzorów i wczytywaniu konkretnego wzoru.
    /// </summary>
    public struct PatternInfo
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
        public bool DynamicContent;

        /// <summary>
        /// Konstruktor struktury używany przy jej tworzeniu.
        /// Uzupełnia domyślnymi danymi wszystkie zmienne.
        /// </summary>
        /// 
        /// <param name="name">Nazwa parsowanego wzoru.</param>
		//* ============================================================================================================
        public PatternInfo( string name )
        {
            this.Name           = name;
            this.HasConfigFile  = false;
            this.Corrupted      = false;
            this.Size           = new Size( 0, 0 );
            this.Pages          = 0;
            this.DynamicContent = false;
        }
    };




	public struct GlobalStruct
	{
		public OpenFileDialog SelectFile;
        public SaveFileDialog SaveFile;
		public DatafileSettingsForm     DatafileSettings;
		public Forms.EditColumnsForm    EditColumns;
	};

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
		LastPage
	};






	public enum LINEENDING
	{
		Undefined,
		Windows,
		Unix,
		Macintosh
	};

	public enum LANGCODE
	{
		// =========================== I00 [Globals]

		GGE_CRITICALERROR       = 0x00,
		GGE_RUNNINGCDESIGNER    = 0x01,
		GGE_RUNNINGCDRESTORE    = 0x02,
		GGE_OPTIONNOTAVALIABLE  = 0x03,

		GFN_DATAFILESETTINGS    = 0x01,
		GFN_EDITCOLUMNS    = 0x02,
        GFN_EDITDATA       = 0x03,
        GFN_NEWPATTERN     = 0x04,
        GFN_ABOUTPROGRAM   = 0x05,
        GFN_PROGRAMUPDATE  = 0x06,

        GLO_BIGCHARS       = 0x00,
        GLO_SMALLCHARS     = 0x01,
		GEX_CSV            = 0x00,
        GEX_CBD            = 0x01,

		GMN_SELECTFILE     = 0x01,
        GMN_PATTCREATING   = 0x06,
        GMN_SAVEFILE       = 0x07,
        GMN_IMPORTFILE     = 0x08,
        
		// =========================== I01 [MainForm]

        I01_MEN_PAT_PATTERN    = 0x00,
        I01_MEN_PAT_NEWPATTERN = 0x01,
        I01_MEN_PAT_RECENTOPEN = 0x02,
        I01_MEN_PAT_CLEARLIST  = 0x03,
        I01_MEN_PAT_CLOSE      = 0x04,
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
        I01_PAT_BUT_NEWPATTERN = 0x00,
        I01_PAT_BUT_DELETE     = 0x01,
        I01_PAT_LAB_PATDETAILS = 0x00,
        I01_PAT_LAB_PAGE       = 0x01,
        I01_PAT_PAT_NAME       = 0x00,
        I01_PAT_PAT_FORMAT     = 0x01,
        I01_PAT_PAT_SIZE       = 0x02,
        I01_PAT_PAT_DATAPLACE  = 0x03,
        I01_PAT_PAT_PAGECOUNT  = 0x04,
        I01_PAT_MES_YESIMPORT  = 0x00,
        I01_PAT_MES_IMPORTSUCC = 0x01,

		// =========================== I02 [DatafileSettingsForm]

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

		// =========================== I03 [EditColumnsForm]

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
        
		// =========================== I04 [EditRowsForm]

        I04_LAB_ROWSONPAGE = 0x00,
        I04_LAB_PAGEOFNUM  = 0x01,
        I04_BUT_SAVE       = 0x00,
        I04_BUT_CANCEL     = 0x01,
        
		// =========================== I05 [NewPatternForm]

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
        
		// =========================== I06 [InfoForm]

        I06_BUT_CLOSE      = 0x00,
        I06_LAB_COMPILDATE = 0x00,
        I06_LAB_AUTHOR     = 0x01,
        I06_LAB_ABOUTCOPY  = 0x02,
        I06_LAB_REGFOR     = 0x03,
        I06_LAB_REGNUMBER  = 0x04,
        I06_LAB_EXPIREDATE = 0x05,
        I06_LAB_EXPIRNEVER = 0x06,
        
		// =========================== I06 [UpdateForm]

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

		// ------------------- Global

		iFN_DataFilter = 0,
		iFN_DatafileSettings,
		iFN_EditColumns,
		iFN_EditData,

		iMN_ParseError = 0,
		iMN_DatafileSelect,
		iMN_ColumnAdd,
		iMN_FileLoad,
		iMN_CriticalError,

		iGE_CriticalError = 0,
		iGE_RunningCDesigner,
		iGE_RunningCDRestore,
		iGE_OptionNotAvaliable,






		// ------------------- DatafileSettings

		dsEX_CSV = 0,

		dsCB_Default = 0,
		dsCB_ASCII,
		dsCB_UTF8,
		dsCB_UTF16BE,
		dsCB_UTF16LE,
		dsCB_UTF32,
		dsCB_UTF7,

		dsHE_RowPreview = 0,
		dsHE_Columns,

		dsBT_Change = 0,
		dsBT_Apply,
		dsBT_Cancel,

		dsMG_WrongExtension = 0,

		dsLB_AutoDetectTypes = 0,
		dsLB_WithoutHeader,

		// ------------------- EditColumns

		ecHE_Name = 0,
		ecHE_Columns,
		ecHE_RowPreview,
		ecHE_AvaliableColumns,
		ecHE_FiltersForColumn,
		ecHE_OldColumns,
		ecHE_NewColumns,

		ecLB_ColumnType = 0,
		ecLB_FilterSettings
	};

	public enum MSGBLIDX
	{
		ParseError,
		DatafileSelect,
		AddColumn,
		FileLoading,

		FatalErrorTitle = 0,
		CDesignerIsRunning,
		CDRestoreIsRunning
	};
	



	public enum FILTERTYPE
	{
		LowerCase  = 0x0,
		UpperCase  = 0x1,
		TitleCase  = 0x2,
		Equal      = 0x3,
		NotEqual   = 0x4,
		Format     = 0x5
	};

	public struct SettingMemberData
	{
		public SettingMemberData( string name, byte type, object defval )
		{
			this.Name         = name;
			this.Type         = type;
			this.DefaultValue = defval;
		}

		public string Name;
		public byte   Type;
		public object DefaultValue;
	};

	public enum SETTINGCODE
	{
		I02_ROWSNUMBER = 0x0A,
		I02_ENCODING,
		I02_SEPARATOR,

		I03_ROWSNUMST1,
		I03_ROWSNUMST2
	};

	public struct SettingsInfo
	{
		public static readonly SettingMemberData[] MemberData =
		{
			new SettingMemberData( "Language",       1, (object)        "pol" ),
			new SettingMemberData( "DSF_RowsNumber", 4, (object)(Int32) 17    ),
			new SettingMemberData( "DR_Separator",   2, (object)        ';'   ),
			new SettingMemberData( "DR_Encoding",    3, (object)(Byte)  0     ),
			new SettingMemberData( "DR_AutoCheck",   3, (object)(Byte)  1     ),
			new SettingMemberData( "FC_RowsNumber",  4, (object)(Int32) 10    ),
			new SettingMemberData( "FC_Separator",   1, (object)        " "   ),
			new SettingMemberData( "EDF_RowsNumber", 4, (object)(Int32) 50    ),


			new SettingMemberData( "ECF_RowsNumberS1", 4, (object)(Int32)5  ),
			new SettingMemberData( "ECF_RowsNumberS2", 4, (object)(Int32)10 ),






			new SettingMemberData( "i02_RowsNumber", 4, (object)(Int32) 17  ),
			new SettingMemberData( "i02_Encoding",   3, (object)(Byte)  0   ),
			new SettingMemberData( "i02_Separator",  2, (object)        ';' ),

			new SettingMemberData( "i03_RowsNumSt1", 4, (object)(Int32) 5   ),
			new SettingMemberData( "i03_RowsNumSt2", 4, (object)(Int32) 10  )
		};

		public SettingsInfo( bool set_default ) : this()
		{
			if( set_default )
			{
				this.Language       = (String)SettingsInfo.MemberData[0].DefaultValue;
				this.DSF_RowsNumber = (Int32 )SettingsInfo.MemberData[1].DefaultValue;
				this.DR_Separator   = (Char  )SettingsInfo.MemberData[2].DefaultValue;
				this.DR_Encoding    = (Byte  )SettingsInfo.MemberData[3].DefaultValue;
				this.DR_AutoCheck   = (Byte  )SettingsInfo.MemberData[4].DefaultValue;
				this.FC_RowsNumber  = (Int32 )SettingsInfo.MemberData[5].DefaultValue;
				this.FC_Separator   = (String)SettingsInfo.MemberData[6].DefaultValue;
				this.EDF_RowsNumber = (Int32 )SettingsInfo.MemberData[7].DefaultValue;


				this.ECF_RowsNumberS1 = (Int32)SettingsInfo.MemberData[8].DefaultValue;
				this.ECF_RowsNumberS2 = (Int32)SettingsInfo.MemberData[9].DefaultValue;







				this.i02_RowsNumber = (Int32)SettingsInfo.MemberData[(int)SETTINGCODE.I02_ROWSNUMBER].DefaultValue;
				this.i02_Encoding   = (Byte )SettingsInfo.MemberData[(int)SETTINGCODE.I02_ENCODING  ].DefaultValue;
				this.i02_Separator  = (Char )SettingsInfo.MemberData[(int)SETTINGCODE.I02_SEPARATOR ].DefaultValue;

				this.i03_RowsNumSt1 = (Int32)SettingsInfo.MemberData[(int)SETTINGCODE.I03_ROWSNUMST1].DefaultValue;
				this.i03_RowsNumSt2 = (Int32)SettingsInfo.MemberData[(int)SETTINGCODE.I03_ROWSNUMST2].DefaultValue;

                this.c02_RecentMax = 10;
			}
		}

        /// <summary>Limit dla wyświetlanych ostatnio otwieranych wzorów.</summary>
        public Byte c02_RecentMax;

		public Int32 i02_RowsNumber;
		public Byte  i02_Encoding;
		public Char  i02_Separator;

		public Int32 i03_RowsNumSt1;
		public Int32 i03_RowsNumSt2;










		public Int32 ECF_RowsNumberS1;
		public Int32 ECF_RowsNumberS2;






		public String Language;

		// DatabaseSettingsForm
		public Int32 DSF_RowsNumber;
		
		// DataReader
		public Char DR_Separator;
		public Byte DR_Encoding;
		public Byte DR_AutoCheck;

		// FilterCreator
		public Int32  FC_RowsNumber;
		public String FC_Separator;

		// EditDataForm
		public Int32 EDF_RowsNumber;








		public int    g_def_scale;
		public Color  g_back_color;
		public int    g_padding;
		public int    g_page_float;
		public bool   g_ask_before_close;
		public bool   g_save_sliders;
		public bool   g_shortcuts_on;
		public bool   g_show_status;
		public double g_pixels_per_dpi;

		// rozdzielczości
		public int       r_min_res;
		public int       r_max_res;
		public List<int> r_list;
		public bool      r_custom_res;

	};

	// zmienić na STRUCT?
	public class FilterInfo
	{
		public int        SubColumn;
		public FILTERTYPE Filter;
		public string     Modifier;
		public string     Result;
		public bool       Exclude;
		public bool       FilterCopy;


		// niepotrzebne
		public int        Column;
		public int    Index;
		public int    Parent;
		public int    Level;
		public int    RealIndex;
	}



	public struct WinAPIConst
	{
		public const int WS_EX_CLIENTEDGE  = 0x00000200;
		public const int WS_EX_NOACTIVE    = 0x08000000;
		public const int WS_EX_TOPMOST     = 0x00000008;
		public const int WS_EX_TRANSPARENT = 0x00000020;
 		public const int WS_EX_WINDOWEDGE  = 0x00000100;

		public const int WS_OVERLAPPED  = 0x00000000;
		public const int WS_CHILD       = 0x40000000;
		public const int WS_CHILDWINDOW = 0x40000000;
		public const int WS_TABSTOP     = 0x00010000;
		public const int WS_BORDER      = 0x00800000;
		public const int WS_DISABLED    = 0x08000000;
		public const int WS_VISIBLE     = 0x10000000;

		public const int WM_MOUSEMOVE = 0x0200;
		public const int WM_DRAWITEM  = 0x002B;
		public const int WM_NCCREATE  = 0x0081;
		public const int WM_SETFONT   = 0x0030;
		public const int WM_PAINT     = 0x000F;
		public const int WM_USER      = 0x0400;
		public const int WM_COMMAND   = 0x0111;

		public const int CBS_DROPDOWN     = 0x0002;
		public const int CBS_DROPDOWNLIST = 0x0003;
		public const int CBS_HASSTRINGS   = 0x0200;

		public const int CB_ADDSTRING     = 0x0143;
		public const int CB_SETCURSEL     = 0x014E;
		public const int CB_GETCURSEL     = 0x0147;
		public const int CB_SETITEMHEIGHT = 0x0153;
		public const int CB_GETITEMHEIGHT = 0x0154;

		public const int CBEIF_TEXT          = 0x01;
		public const int CBEIF_IMAGE         = 0x02;
		public const int CBEIF_SELECTEDIMAGE = 0x04;
		public const int CBEIF_OVERLAY       = 0x08;
		public const int CBEIF_INDENT        = 0x10;
		public const int CBEIF_LPARAM        = 0x20;

		public const int CBN_SELCHANGE = 0x01;
		public const int CBN_DROPDOWN  = 0x07;
		public const int CBN_SELENDOK  = 0x09;

		public const int SWP_NOMOVE       = 0x0002;
		public const int SWP_NOACTIVATE   = 0x0010;
		public const int SWP_NOSIZE       = 0x0001;
		public const int SWP_NOREPOSITION = 0x0200;

		public const int OCM_DRAWITEM = 0x202B;

		public const int CBEM_INSERTITEM = WM_USER + 11;
	}

	public class GroupComboBoxItem
	{
		public string text;
		public bool header;
		public int indent;
		public bool last;
		public bool first;
		public GroupComboBoxItem parent;
		public List<GroupComboBoxItem> child;
		public int index;
		public object tag;
	}

	public class FilterData
	{
		public int column;
		public int filter;
		public string modifier;
		public string result;
		public bool exclude;
		public int index;
		public int parent;
		public int level;
		public int real_index;
	}



	public class FieldExtraData
	{
		public bool image_from_db;
		public bool print_color;
		public bool print_image;
		public bool text_from_db;
		public bool print_text;
		public bool print_border;
		public int  column;
		public int  pos_align;
	};

	public class FieldData
	{
		public string           name;
		public RectangleF       bounds;
		public float            border_size;
		public Color            border_color;
		public bool             image;
		public string           image_path;
		public Color            color;
		public Color            font_color;
		public string           font_name;
		public FontStyle        font_style;
		public float            font_size;
		public ContentAlignment text_align;
		public int              text_transform;
		public float            text_leftpad;
		public float            text_toppad;
		public bool             text_add_margin;
		public float            padding;
		public FieldExtraData   extra;
	};

	public class PageExtraData
	{
		public bool   print_color;
		public bool   print_image;
		public string image_path;
	};

	public class PageData
	{
		public int           fields;
		public bool          image;
		public Color         color;
		public FieldData[]   field;
		public PageExtraData extra;
	};

	public class PatternData
	{
		public string     name;
		public Size       size;
		public int        pages;
		public bool       dynamic;
		public PageData[] page;
	};

	public class DataContent
	{
		public int       columns;
		public string[]  column;
		public int       rows;
		public string[,] row;
	};
	


	///
	///
	/// STRUKTURY WYŁĄCZONE Z UŻYTKU
	/// Stare struktury / klasy które mogą się jeszcze przydać...
	///
	///

	/*
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
	public struct ComboBoxExItem
	{
		public uint   mask;
		public IntPtr iItem;
		[MarshalAs(UnmanagedType.LPWStr)]
		public string pszText;
		public int    cchTextMax;
		public int    iImage;
		public int    iSelectedImage;
		public int    iOverlay;
		public int    iIndent;
		public IntPtr lParam;
	}
	*/
}