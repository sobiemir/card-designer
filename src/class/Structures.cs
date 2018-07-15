using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace CDesigner
{
	public enum FORMLIDX
	{
		DataFilter,
		DatabaseSettings,
		EditColumns,
		EditData
	};

	public enum MSGBLIDX
	{
		ParseError,
		DatabaseSelect,
		AddColumn,
		FileLoading,

		FatalErrorTitle = 0,
		CDesignerIsRunning,
		CDRestoreIsRunning
	};

	public enum DATATYPE
	{
		String,
		Integer,
		Double,
		Character,
		Date
	};

	public enum BMPSRC
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
		

		ItemAdd,
		ItemRemove,
		Refresh,
		FirstPage,
		PrevPage,
		NextPage,
		LastPage
	};

	public enum FILTERTYPE
	{
		Format     = 0x1,
		UpperCase  = 0x2,
		LowerCase  = 0x3,
		TitleCase  = 0x4,
		NotEqual   = 0x5,
		Equal      = 0x6
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
	}
	
	public struct SettingsInfo
	{
		public static readonly SettingMemberData[] MemberData = {
			new SettingMemberData( "Language",       1, (object)        "pl" ),
			new SettingMemberData( "DSF_RowsNumber", 4, (object)(Int32) 17   ),
			new SettingMemberData( "DR_Separator",   2, (object)        ';'  ),
			new SettingMemberData( "DR_Encoding",    3, (object)(Byte)  0    ),
			new SettingMemberData( "DR_AutoCheck",   3, (object)(Byte)  1    ),
			new SettingMemberData( "FC_RowsNumber",  4, (object)(Int32) 10   ),
			new SettingMemberData( "FC_Separator",   1, (object)        " "  ),
			new SettingMemberData( "EDF_RowsNumber", 4, (object)(Int32) 50   )
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
			}
		}

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

	}

	// zmienić na STRUCT?
	public class FilterInfo
	{
		public int    Column;
		public int    Filter;
		public string Modifier;
		public string Result;
		public bool   Exclude;
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
	}

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
	}

	public class PageExtraData
	{
		public bool   print_color;
		public bool   print_image;
		public string image_path;
	}

	public class PageData
	{
		public int           fields;
		public bool          image;
		public Color         color;
		public FieldData[]   field;
		public PageExtraData extra;
	}

	public class PatternData
	{
		public string     name;
		public Size       size;
		public int        pages;
		public bool       dynamic;
		public PageData[] page;
	}

	public class DataContent
	{
		public int       columns;
		public string[]  column;
		public int       rows;
		public string[,] row;
	}
	


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