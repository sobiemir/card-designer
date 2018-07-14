using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Windows.Forms;
using System.Drawing;

namespace CDesigner
{
	class PageField : Label
	{
		private Color     _border_color    = Color.Black;
		private int       _border_size     = 0;
		private string    _image_path      = null;
		private Rectangle _dpi_bounds      = new Rectangle( 0, 0, 1, 1 );
		private int       _dpi_border_size = 0;
		private double    _dpi_scale       = 1.0;
		private double    _dpi_conv_scale  = 3.938095238095238;
		private double    _dpi_font_size   = 8.25;
		private int       _dpi_padding     = 0;
		private Image     _back_image      = null;
		private double    _pixels_per_dpi  = 3.938095238095238;
		
		// ------------------------------------------------------------- DPIBorderSize --------------------------------
		
		public int DPIBorderSize
		{
			get { return this._dpi_border_size; }
			set
			{
				this._dpi_border_size = value;

				if( this._dpi_scale < 1.0 )
					this._border_size = (int)((double)this._dpi_border_size * this._dpi_scale + 0.01);
				else
					this._border_size = (int)((double)this._dpi_border_size * this._dpi_scale);
			}
		}

		// ------------------------------------------------------------- BorderColor ----------------------------------
		
		public Color BorderColor
		{
			get { return this._border_color; }
			set { this._border_color = value; }
		}

		// ------------------------------------------------------------- DPIPadding -----------------------------------
		
		public int DPIPadding
		{
			get { return this._dpi_padding; }
			set
			{
				this._dpi_padding = value;

				if( this._dpi_scale < 1.0 )
					this.Padding = new Padding( (int)((double)this._dpi_padding * this._dpi_scale + 0.01) );
				else
					this.Padding = new Padding( (int)((double)this._dpi_padding * this._dpi_scale) );
			}
		}

		// ------------------------------------------------------------- DPIBounds ------------------------------------

		public Rectangle DPIBounds
		{
			get { return this._dpi_bounds; }
			set
			{
				// pozycja X
				if( value.X < 0 )
					value.X = this._dpi_bounds.X;
				else
					this.Left = (int)((double)value.X * this._dpi_conv_scale);

				// pozycja Y
				if( value.Y < 0 )
					value.Y = this._dpi_bounds.Y;
				else
					this.Top = (int)((double)value.Y * this._dpi_conv_scale);

				// szerokość
				if( value.Width < 0 )
					value.Width = this._dpi_bounds.Width;
				else
					this.Width = (int)((double)value.Width * this._dpi_conv_scale);

				// wysokość
				if( value.Height < 0 )
					value.Height = this._dpi_bounds.Height;
				else
					this.Height = (int)((double)value.Height * this._dpi_conv_scale);

				this._dpi_bounds = value;
			}
		}

		// ------------------------------------------------------------- DPIFontSize ----------------------------------

		public double DPIFontSize
		{
			get { return this._dpi_font_size; }
			set
			{
				// rozmiar czcionki nie może być mniejszy od 1...
				value = value < 1 ? 1 : value;

				// utwórz czcionkę z nowym rozmiarem
				Font font = new Font
				(
					this.Font.FontFamily,
					(float)(value * this._dpi_scale),
					this.Font.Style,
					GraphicsUnit.Point,
					this.Font.GdiCharSet,
					this.Font.GdiVerticalFont
				);

				// zmień czcionkę i informacje o rozmiarze
				this._dpi_font_size = value;
				this.Font = font;
			}
		}

		// ------------------------------------------------------------- BackImage ------------------------------------

		public Image BackImage
		{
			get { return this._back_image; }
			set { this._back_image = value; }
		}

		// ------------------------------------------------------------- BackImagePath --------------------------------

		public string BackImagePath
		{
			get { return this._image_path; }
			set { this._image_path = value; }
		}

		// ------------------------------------------------------------- DPIScale -------------------------------------

		public double DPIScale
		{
			get { return this._dpi_scale; }
			set
			{
				// od 50 do 300 DPI
				value = value < 0.5 ? 0.5 : value > 3.0 ? 3.0 : value;

				this._dpi_scale	     = value;
				this._dpi_conv_scale = this._pixels_per_dpi * value;

				// odśwież wartości
				this.DPIBounds     = this.DPIBounds;
				this.DPIBorderSize = this.DPIBorderSize;
				this.DPIFontSize   = this.DPIFontSize;
				this.DPIPadding    = this.DPIPadding;
			}
		}

		// ------------------------------------------------------------- SetTextAlignment -----------------------------

		public void SetTextAlignment( int align )
		{
			// ustaw odpowiednie przyleganie tekstu...
			switch( align )
			{
				case 0: this.TextAlign = ContentAlignment.TopLeft; break;
				case 1: this.TextAlign = ContentAlignment.TopCenter; break;
				case 2: this.TextAlign = ContentAlignment.TopRight; break;
				case 3: this.TextAlign = ContentAlignment.MiddleLeft; break;
				case 4: this.TextAlign = ContentAlignment.MiddleCenter; break;
				case 5: this.TextAlign = ContentAlignment.MiddleRight; break;
				case 6: this.TextAlign = ContentAlignment.BottomLeft; break;
				case 7: this.TextAlign = ContentAlignment.BottomCenter; break;
				case 8: this.TextAlign = ContentAlignment.BottomRight; break;
				default:
					this.TextAlign = ContentAlignment.MiddleCenter;
				break;
			}
		}

		// ------------------------------------------------------------- OnPaint --------------------------------------

		protected override void OnPaint( PaintEventArgs ev )
		{
			// rysuj dopasowany obraz
			if( this._back_image != null )
				ev.Graphics.DrawImage( this._back_image, 0, 0, this.Width, this.Height );

			// rysuj tekst...
			base.OnPaint( ev );

			// rysuj ramkę
			if( this._border_size > 0 )
				ControlPaint.DrawBorder( ev.Graphics, ClientRectangle,
					this._border_color, this._border_size, ButtonBorderStyle.Solid,
					this._border_color, this._border_size, ButtonBorderStyle.Solid,
					this._border_color, this._border_size, ButtonBorderStyle.Solid,
					this._border_color, this._border_size, ButtonBorderStyle.Solid );
		}
	}

	struct CustomLabelDetails
	{
		public bool ImageFromDB;
		public bool PrintColor;
		public bool PrintImage;
		public bool TextFromDB;
		public bool PrintText;
		public bool PrintBorder;
	}

	class CDField : Label
	{
		private Color	_border_color	= Color.Black;
		private int		_border_size	= 0;
		private string	_image_path		= null;
		private int		_dpi_width		= 1;
		private int		_dpi_height		= 1;
		private int		_dpi_left_pos	= 0;
		private int		_dpi_top_pos	= 0;
		private int		_dpi_border		= 0;
		private double	_dpi_scale		= 1.0;
		private double	_dpi_conv_scale	= 3.938095238095238;
		private double	_dpi_font_size	= 8.25;
		private int		_dpi_padding	= 0;
		private Image	_back_image		= null;
		private int		_text_align		= 5;

		// ------------------------------------------------------------- DPIBorderSize --------------------------------
		
		public int DPIBorderSize
		{
			get { return this._dpi_border; }
			set
			{
				// ograniczenie od 0 do 20
				value = value < 0 ? 0 : value > 20 ? 20 : value;

				this._dpi_border = value;

				// zamień DPI na rozmiar ramki w pikselach (rozmiar realny w 100 DPI)
				if( this._dpi_scale < 1.0 )
					this._border_size = Convert.ToInt32( (double)this._dpi_border * this._dpi_scale + 0.01 );
				else
					this._border_size = Convert.ToInt32( (double)this._dpi_border * this._dpi_scale );
			}
		}

		// ------------------------------------------------------------- BorderColor ----------------------------------
		
		public Color BorderColor
		{
			get { return this._border_color; }
			set { this._border_color = value; }
		}

		// ------------------------------------------------------------- DPIPadding -----------------------------------
		
		public int DPIPadding
		{
			get { return this._dpi_padding; }
			set
			{
				// ograniczenie od 0 do 25
				value = value < 0 ? 0 : value > 25 ? 25 : value;

				this._dpi_padding = value;

				// zamień DPI na rozmiar wewnętrznego marginesu
				if( this._dpi_scale < 1.0 )
					this.Padding = new Padding( Convert.ToInt32((double)this._dpi_padding * this._dpi_scale + 0.01) );
				else
					this.Padding = new Padding( Convert.ToInt32((double)this._dpi_padding * this._dpi_scale) );
			}
		}

		// ------------------------------------------------------------- DPIHeight ------------------------------------

		public int DPIHeight
		{
			get { return this._dpi_height; }
			set
			{
				// wysokość nie może być mniejsza od 1
				value = value < 1 ? 1 : value;
				this._dpi_height = value;

				// oblicz wysokość pola z DPI
				this.Height = Convert.ToInt32( (double)this._dpi_height * this._dpi_conv_scale );
			}
		}

		// ------------------------------------------------------------- DPIWidth -------------------------------------

		public int DPIWidth
		{
			get { return this._dpi_width; }
			set
			{
				// szerokość nie może być mniejsza niż 1
				value = value < 1 ? 1 : value;
				this._dpi_width = value;

				// oblicz szerokość pola z DPI
				this.Width = Convert.ToInt32( (double)this._dpi_width * this._dpi_conv_scale );
			}
		}

		// ------------------------------------------------------------- DPILeft --------------------------------------

		public int DPILeft
		{
			get { return this._dpi_left_pos; }
			set
			{
				// pozycja nie może być mniejsza od 0
				value = value < 0 ? 0 : value;
				this._dpi_left_pos = value;

				// oblicz pozycje X pola z DPI
				this.Left = Convert.ToInt32( (double)this._dpi_left_pos * this._dpi_conv_scale );
			}
		}

		// ------------------------------------------------------------- DPITop ---------------------------------------

		public int DPITop
		{
			get { return this._dpi_top_pos; }
			set
			{
				// pozycja nie może być mniejsza od 0
				value = value < 0 ? 0 : value;
				this._dpi_top_pos = value;

				// oblicz pozycje Y pola z DPI
				this.Top = Convert.ToInt32( (double)this._dpi_top_pos * this._dpi_conv_scale );
			}
		}

		// ------------------------------------------------------------- DPIFontSize ----------------------------------

		public double DPIFontSize
		{
			get { return this._dpi_font_size; }
			set
			{
				// rozmiar czcionki nie może być mniejszy od 1...
				value = value < 1 ? 1 : value;

				// utwórz czcionkę z nowym rozmiarem
				Font font = new Font
				(
					this.Font.FontFamily,
					(float)(value * this._dpi_scale),
					this.Font.Style,
					GraphicsUnit.Point,
					this.Font.GdiCharSet,
					this.Font.GdiVerticalFont
				);

				// zmień czcionkę i informacje o rozmiarze
				this._dpi_font_size = value;
				this.Font = font;
			}
		}

		// ------------------------------------------------------------- TextAlignment --------------------------------

		public int TextAlignment
		{
			get { return this._text_align; }
			set
			{
				// ustaw odpowiednie przyleganie tekstu...
				switch( value )
				{
					case 0: this.TextAlign = ContentAlignment.TopLeft; break;
					case 1: this.TextAlign = ContentAlignment.TopCenter; break;
					case 2: this.TextAlign = ContentAlignment.TopRight; break;
					case 3: this.TextAlign = ContentAlignment.MiddleLeft; break;
					case 4: this.TextAlign = ContentAlignment.MiddleCenter; break;
					case 5: this.TextAlign = ContentAlignment.MiddleRight; break;
					case 6: this.TextAlign = ContentAlignment.BottomLeft; break;
					case 7: this.TextAlign = ContentAlignment.BottomCenter; break;
					case 8: this.TextAlign = ContentAlignment.BottomRight; break;
					default:
						this.TextAlign = ContentAlignment.MiddleCenter;
						value = 4;
					break;
				}
				this._text_align = value;
			}
		}

		// ------------------------------------------------------------- BackImage ------------------------------------

		public Image BackImage
		{
			get { return this._back_image; }
			set { this._back_image = value; }
		}

		// ------------------------------------------------------------- BackImagePath --------------------------------

		public string BackImagePath
		{
			get { return this._image_path; }
			set { this._image_path = value; }
		}

		// ------------------------------------------------------------- DPIScale -------------------------------------

		public double DPIScale
		{
			get { return this._dpi_scale; }
			set
			{
				// ograniczenie od 0.5 do 3.0 (50 DPI do 300 DPI)
				value = value < 0.5 ? 0.5 : value > 3.0 ? 3.0 : value;

				this._dpi_scale	= value;
				this._dpi_conv_scale = 3.938095238095238 * value;

				// odśwież wartości
				this.DPIHeight = this.DPIHeight;
				this.DPIWidth = this.DPIWidth;
				this.DPILeft = this.DPILeft;
				this.DPITop = this.DPITop;
				this.DPIBorderSize = this.DPIBorderSize;
				this.DPIFontSize = this.DPIFontSize;
				this.DPIPadding = this.DPIPadding;
			}
		}

		// ------------------------------------------------------------- OnPaint --------------------------------------

		protected override void OnPaint( PaintEventArgs ev )
		{
			// rysuj dopasowany obraz
			if( this._back_image != null )
				ev.Graphics.DrawImage( this._back_image, 0, 0, this.Width, this.Height );

			// rysuj tekst...
			base.OnPaint( ev );

			// rysuj ramkę
			if( this._border_size > 0 )
				ControlPaint.DrawBorder( ev.Graphics, ClientRectangle,
					this._border_color, this._border_size, ButtonBorderStyle.Solid,
					this._border_color, this._border_size, ButtonBorderStyle.Solid,
					this._border_color, this._border_size, ButtonBorderStyle.Solid,
					this._border_color, this._border_size, ButtonBorderStyle.Solid );
		}
	}
}
