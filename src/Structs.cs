using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace CDesigner
{
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
	
	public class SettingsInfo
	{
		// ustawienia ogólne
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
		public int    r_min_res;
		public int    r_max_res;
		public int[]  r_list;
		public bool   r_custom_res;
	}
}
