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
	}

	public class FieldData
	{
		public string           name;
		public Rectangle        bounds;
		public int              border_size;
		public Color            border_color;
		public bool             image;
		public string           image_path;
		public Color            color;
		public Color            font_color;
		public string           font_name;
		public FontStyle        font_style;
		public float            font_size;
		public ContentAlignment text_align;
		public ContentAlignment pos_align;
		public Padding          padding;
		public FieldExtraData   extra;
	}

	public class PageExtraData
	{
		public bool print_color;
		public bool print_image;
	}

	public class PageData
	{
		public int           fields;
		public bool          image;
		public string        image_path;
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
}
