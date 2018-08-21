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
		private Color      _border_color    = Color.Black;
		private int        _border_size     = 0;
		private string     _image_path      = null;
		private RectangleF _dpi_bounds      = new RectangleF( 0.0f, 0.0f, 1.0f, 1.0f );
		private float      _dpi_border_size = 0.0f;
		private double     _dpi_scale       = 1.0;
		private double     _dpi_conv_scale  = 3.93714927048264;
		private double     _dpi_font_size   = 8.25;
		private PointF     _dpi_text_margin = new PointF( 2.0f, 2.0f );
		private float      _dpi_padding     = 0.0f;
		private Point      _dpi_parent_size = new Point( -1, -1 );
		private Image      _back_image      = null;
		private double     _pixels_per_dpi  = 3.93714927048264;
		private Size       _parent_bounds   = new Size(800, 600);
		private int        _text_transform  = 0;
		private string     _original_text   = null;
		private string     _current_text    = "";
		private bool       _extra_margin    = false;

		// ------------------------------------------------------------- DPIBorderSize --------------------------------
		
		public float DPIBorderSize
		{
			// pobierz rozmiar ramki (w mm)
			get { return this._dpi_border_size; }
			// ustaw rozmiar ramki
			set
			{
				this._dpi_border_size = value;
				this._border_size = Convert.ToInt32( (double)this._dpi_border_size * this._dpi_conv_scale );

				// ramka musi być widoczna, gdy jest włączona...
				if( this._dpi_border_size > 0.0 && this._border_size == 0 )
					this._border_size = 1;
			}
		}

		// ------------------------------------------------------------- BorderColor ----------------------------------
		
		public Color BorderColor
		{
			// pobierz kolor ramki
			get { return this._border_color; }
			// ustaw color ramki
			set { this._border_color = value; }
		}

		// ------------------------------------------------------------- DPIPadding -----------------------------------
		
		public float DPIPadding
		{
			// pobierz wcięcie (w mm)
			get { return this._dpi_padding; }
			// ustaw wcięcie
			set
			{
				this._dpi_padding = value;

				if( this._dpi_scale < 1.0 )
					this.Padding = new Padding( Convert.ToInt32((double)this._dpi_padding * this._dpi_conv_scale + 0.01) );
				else
					this.Padding = new Padding( Convert.ToInt32((double)this._dpi_padding * this._dpi_conv_scale) );
			}
		}

		// ------------------------------------------------------------- DPIBounds ------------------------------------

		public RectangleF DPIBounds
		{
			// pobierz granice kontroki (w mm)
			get { return this._dpi_bounds; }
			// ustaw granice kontrolki
			set
			{
				// pozycja X
				if( value.X < 0 )
					value.X = this._dpi_bounds.X;
				else
				{
					this.Left = Convert.ToInt32((double)value.X * this._dpi_conv_scale);
				}

				// pozycja Y
				if( value.Y < 0 )
					value.Y = this._dpi_bounds.Y;
				else
				{
					this.Top = Convert.ToInt32((double)value.Y * this._dpi_conv_scale);
				}

				// szerokość
				if( value.Width < 0 )
					value.Width = this._dpi_bounds.Width;
				else
				{
					int width = Convert.ToInt32((double)value.Width * this._dpi_conv_scale);

					// nie przekraczaj granicy
					if( this._dpi_parent_size.X > -1 && this.Left + width > this._parent_bounds.Width )
					{
						width       = this.Width;
						value.Width = this._dpi_bounds.Width;
					}
					this.Width = width;
				}

				// wysokość
				if( value.Height < 0 )
					value.Height = this._dpi_bounds.Height;
				else
				{
					int height = Convert.ToInt32((double)value.Height * this._dpi_conv_scale);

					// nie przekraczaj granicy
					if( this._dpi_parent_size.Y > -1 && this.Top + height > this._parent_bounds.Height )
					{
						height       = this.Height;
						value.Height = this._dpi_bounds.Height;
					}
					this.Height = height;
				}

				this._dpi_bounds = value;
			}
		}

		// ------------------------------------------------------------- DPIFontSize ----------------------------------

		public double DPIFontSize
		{
			// pobierz rozmiar czcionki (rozmiar * skala)
			get { return this._dpi_font_size; }
			// ustaw rozmiar czcionki
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
			// pobierz obraz tła
			get { return this._back_image; }
			// ustaw obraz tła
			set { this._back_image = value; }
		}

		// ------------------------------------------------------------- TextMargin -----------------------------------

		public PointF TextMargin
		{
			get { return this._dpi_text_margin; }
			set { this._dpi_text_margin = value; }
		}

		// ------------------------------------------------------------- ApplyTextMargin ------------------------------

		public bool ApplyTextMargin
		{
			get { return this._extra_margin; }
			set { this._extra_margin = value; }
		}

		// ------------------------------------------------------------- BackImagePath --------------------------------

		public string BackImagePath
		{
			// pobierz ścieżkę do aktualnego obrazu
			get { return this._image_path; }
			// ustaw nową ścieżkę do obrazu
			set { this._image_path = value; }
		}

		// ------------------------------------------------------------- DPIScale -------------------------------------

		public double DPIScale
		{
			// pobierz aktualną skalę DPI
			get { return this._dpi_scale; }
			// ustaw skalę DPI
			// @TODO - ustawienia DPI
			set
			{
				// od 50 do 300 DPI
				value = value < 0.5 ? 0.5 : value > 3.0 ? 3.0 : value;

				this._dpi_scale	     = value;
				this._dpi_conv_scale = this._pixels_per_dpi * value;

				// odśwież wartości
				this.SetParentBounds( this._dpi_parent_size.X, this._dpi_parent_size.Y );
				this.DPIBounds     = this.DPIBounds;
				this.DPIBorderSize = this.DPIBorderSize;
				this.DPIFontSize   = this.DPIFontSize;
				this.DPIPadding    = this.DPIPadding;
			}
		}

		// ------------------------------------------------------------- TextTransform --------------------------------
		
		public int TextTransform
		{
			// pobierz transformacje tekstu
			get { return this._text_transform; }
			// ustaw transformacje tekstu
			set
			{
				this._text_transform = value;
				switch( this._text_transform )
				{
				case 0: this._current_text = this._original_text; break;
				case 1: this._current_text = this._original_text.ToUpper(); break;
				case 2: this._current_text = this._original_text.ToLower(); break;
				case 3: this._current_text = System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase( this._original_text.ToLower() ); break;
				}
				this.Refresh();
			}
		}

		// ------------------------------------------------------------- Text -----------------------------------------
		
		public override string Text
		{
			// pobierz tekst kontrolki
			get { return this._current_text; }
			// zmień tekst kontrolki
			set
			{
				this._original_text = value;
				switch( this._text_transform )
				{
				case 0: this._current_text = value; break;
				case 1: this._current_text = value.ToUpper(); break;
				case 2: this._current_text = value.ToLower(); break;
				case 3: this._current_text = System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase( value.ToLower() ); break;
				}
				this.Refresh();
			}
		}

		// ------------------------------------------------------------- OriginalText ---------------------------------
		
		public string OriginalText
		{
			// pobierz oryginalny tekst kontrolki (bez transformacji)
			get { return this._original_text; }
		}

		// ------------------------------------------------------------- DPIBorderSize --------------------------------
		
		public void SetParentBounds( int width, int height )
		{
			this._dpi_parent_size = new Point( width, height );

			this._parent_bounds.Width  = Convert.ToInt32( (double)width * this._dpi_conv_scale );
			this._parent_bounds.Height = Convert.ToInt32( (double)height * this._dpi_conv_scale );
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

		// ------------------------------------------------------------- SetTextAlignment -----------------------------

		public int GetTextAlignment( int alignment = -1 )
		{
			if( alignment < 0 )
				alignment = (int)this.TextAlign;

			int align = 0;

			// ustaw odpowiednie przyleganie tekstu...
			switch( alignment )
			{
				case 0x001: align = 0; break;
				case 0x002: align = 1; break;
				case 0x004: align = 2; break;
				case 0x010: align = 3; break;
				case 0x020: align = 4; break;
				case 0x040: align = 5; break;
				case 0x100: align = 6; break;
				case 0x200: align = 7; break;
				case 0x400: align = 8; break;
				default: align = 4; break;
			}

			return align;
		}

		// ------------------------------------------------------------- GetBoundsByAlignPoint ------------------------

		public PointF GetPosByAlignPoint( ContentAlignment align )
		{
			PointF point = new PointF();

			switch( (int)align )
			{
				case 0x001:
					point.X = this._dpi_bounds.X;
					point.Y = this._dpi_bounds.Y;
				break;
				case 0x004:
 					point.X = this._dpi_bounds.X + this._dpi_bounds.Width;
					point.Y = this._dpi_bounds.Y;
				break;
				case 0x100:
					point.X = this._dpi_bounds.X;
					point.Y = this._dpi_bounds.Y + this._dpi_bounds.Height;
				break;
				case 0x400:
					point.X = this._dpi_bounds.X + this._dpi_bounds.Width;
					point.Y = this._dpi_bounds.Y + this._dpi_bounds.Height;
				break;
			}

			return point;
		}

		// ------------------------------------------------------------- SetPosXByAlignPoint --------------------------

		public void SetPosXByAlignPoint( float x, ContentAlignment align )
		{
			switch( (int)align )
			{
				case 0x000:
				case 0x001:
				case 0x100: this._dpi_bounds.X = x; break;
				case 0x004:
				case 0x400: this._dpi_bounds.X = x - this._dpi_bounds.Width; break;
			}

			int left = Convert.ToInt32((double)this._dpi_bounds.X * this._dpi_conv_scale);

			if( left + this.Width > this._parent_bounds.Width )
				left = this._parent_bounds.Width - this.Width;
			else if( left < 0 )
				left = 0;

			this.Left = left;
		}

		// ------------------------------------------------------------- SetPosYByAlignPoint --------------------------

		public void SetPosYByAlignPoint( float y, ContentAlignment align )
		{
			switch( (int)align )
			{
				case 0x000:
				case 0x001:
				case 0x004: this._dpi_bounds.Y = y; break;
				case 0x100:
				case 0x400: this._dpi_bounds.Y = y - this._dpi_bounds.Height; break;
			}

			int top = Convert.ToInt32((double)this._dpi_bounds.Y * this._dpi_conv_scale);
			
			if( top + this.Height > this._parent_bounds.Height )
				top = this._parent_bounds.Height - this.Height;
			else if( top < 0 )
				top = 0;

			this.Top = top;
		}

		// ------------------------------------------------------------- SetPxLocation --------------------------------

		public void SetPxLocation( int x, int y, ContentAlignment align )
		{
			if( x + this.Width > this._parent_bounds.Width )
				x = this._parent_bounds.Width - this.Width;
			else if( x < 0 )
				x = 0;

			if( y + this.Height > this._parent_bounds.Height )
				y = this._parent_bounds.Height - this.Height;
			else if( y < 0 )
				y = 0;

			this.Location = new Point( x, y );
			this.RefreshLocation( );
		}

		// ------------------------------------------------------------- RefreshLocation ------------------------------

		public void RefreshLocation( )
		{
			this.DPIBounds = new RectangleF
 			(
				(float)((double)this.Location.X / this._dpi_conv_scale),
				(float)((double)this.Location.Y / this._dpi_conv_scale),
				this.DPIBounds.Width,
				this.DPIBounds.Height
			);
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
				ControlPaint.DrawBorder( ev.Graphics, this.ClientRectangle,
					this._border_color, this._border_size, ButtonBorderStyle.Solid,
					this._border_color, this._border_size, ButtonBorderStyle.Solid,
					this._border_color, this._border_size, ButtonBorderStyle.Solid,
					this._border_color, this._border_size, ButtonBorderStyle.Solid );
		}
	};
}