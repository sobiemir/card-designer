using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Windows.Forms;
using System.Drawing;

namespace CDesigner
{
	class CustomLabel : Label
	{
		private int    _borderSize  = 0;
		private Color  _borderColor = Color.Black;
		private bool   _hasImage    = false;
		private string _imagePath   = null;
		private int    _dpiWidth    = 1;
		private int    _dpiHeight   = 1;
		private int    _dpiLeft     = 0;
		private int    _dpiTop      = 0;
		private int    _dpiBorder   = 0;
		private double _dpiScale    = 1.0;
		private double _dpiPxScale  = 3.938095238095238;
		private double _dpiFontSize = 8.25;
		private Image  _dpiImage    = null;

		// ------------------------------------------------------------- BorderSize -----------------------------------
		
		public int BorderSize
		{
			get { return this._dpiBorder; }
			set
			{
				// ograniczenie <0,10>
				value = value < 0 ? 0 : value > 10 ? 10 : value;

				this._dpiBorder  = value;

				if( this._dpiScale < 1.0 )
					this._borderSize = Convert.ToInt32( (double)this._dpiBorder * this._dpiScale + 0.01 );
				else
					this._borderSize = Convert.ToInt32( (double)this._dpiBorder * this._dpiScale );
			}
		}

		// ------------------------------------------------------------- BorderColor ----------------------------------

		public Color BorderColor
		{
			get { return this._borderColor; }
			set { this._borderColor = value; }
		}

		// ------------------------------------------------------------- HasImage -------------------------------------

		public bool HasImage
		{
			get { return this._hasImage; }
			set { this._hasImage = value; }
		}

		// ------------------------------------------------------------- ImagePath ------------------------------------

		public string ImagePath
		{
			get { return this._imagePath; }
			set { this._imagePath = value; }
		}

		// ------------------------------------------------------------- DPIHeight ------------------------------------

		public int DPIHeight
		{
			get { return this._dpiHeight; }
			set
			{
				value = value < 1 ? 1 : value;
				this._dpiHeight = value;

				// oblicz wysokość pola
				this.Height = Convert.ToInt32( (double)this._dpiHeight * this._dpiPxScale );
			}
		}

		// ------------------------------------------------------------- DPIWidth -------------------------------------

		public int DPIWidth
		{
			get { return this._dpiWidth; }
			set
			{
				value = value < 1 ? 1 : value;
				this._dpiWidth = value;

				// oblicz szerokość pola
				this.Width = Convert.ToInt32( (double)this._dpiWidth * this._dpiPxScale );
			}
		}

		// ------------------------------------------------------------- DPILeft --------------------------------------

		public int DPILeft
		{
			get { return this._dpiLeft; }
			set
			{
				value = value < 0 ? 0 : value;
				this._dpiLeft = value;

				// oblicz pozycje X pola
				this.Left = Convert.ToInt32( (double)this._dpiLeft * this._dpiPxScale );
			}
		}

		// ------------------------------------------------------------- DPITop ---------------------------------------

		public int DPITop
		{
			get { return this._dpiTop; }
			set
			{
				value = value < 0 ? 0 : value;
				this._dpiTop = value;

				// oblicz pozycje Y pola
				this.Top = Convert.ToInt32( (double)this._dpiTop * this._dpiPxScale );
			}
		}

		// ------------------------------------------------------------- DPIFont --------------------------------------

		public double FontSize
		{
			get { return (double)this._dpiFontSize; }
			set
			{
				// ograniczenie
				value = value < 1 ? 1 : value;

				// utwórz czcionkę
				Font font = new Font
				(
					this.Font.FontFamily,
					(float)(value * this._dpiScale),
					this.Font.Style,
					GraphicsUnit.Point,
					this.Font.GdiCharSet,
					this.Font.GdiVerticalFont
				);
				// zmień rozmiar czcionki
				this._dpiFontSize = value;
				this.Font         = font;
			}
		}

		// ------------------------------------------------------------- BackgroundImage ------------------------------

		public Image BackImage
		{
			get { return this._dpiImage; }
			set { this._dpiImage = value; }
		}

		// ------------------------------------------------------------- DPIScale -------------------------------------

		public double DPIScale
		{
			get { return this._dpiScale; }
			set
			{
				// ograniczenie <0.5,3.0>
				value = value < 0.5 ? 0.5 : value > 3.0 ? 3.0 : value;

				this._dpiScale   = value;
				this._dpiPxScale = 3.938095238095238 * value;

				// odśwież wartości
				this.DPIHeight  = this.DPIHeight;
				this.DPIWidth   = this.DPIWidth;
				this.DPILeft    = this.DPILeft;
				this.DPITop     = this.DPITop;
				this.BorderSize = this.BorderSize;
				this.FontSize   = this.FontSize;
			}
		}

		// ------------------------------------------------------------- SetDPIBounds ---------------------------------

		public void SetDPIBounds( int x, int y, int width, int height, int border, int dpi )
		{
			// ustaw wartości
			this._dpiWidth  = width < 1 ? 1 : width;
			this._dpiHeight = height < 1 ? 1 : height;
			this._dpiLeft   = x < 0 ? 0 : x;
			this._dpiTop    = y < 0 ? 0 : y;
			this._dpiBorder = border < 0 ? 0 : border > 10 ? 10 : border;

			// zmień DPI
			this.DPIScale = (double)dpi / 100.0;
		}

		// ------------------------------------------------------------- OnPaint --------------------------------------

		protected override void OnPaint( PaintEventArgs e )
		{
			// rysuj dopasowany obraz
			if (this._dpiImage != null)
				e.Graphics.DrawImage(this._dpiImage, 0, 0, this.Width, this.Height);

			// rysuj tekst...
			base.OnPaint( e );

			// brak ramki
			if( this._borderSize < 1 )
				return;

			// rysuj ramkę
			Pen pen = new Pen( this._borderColor, this._borderSize );
			Rectangle rectangle = ClientRectangle;

			rectangle.Inflate( -Convert.ToInt32((float)this._borderSize/2.0+.5), -Convert.ToInt32((float)this._borderSize/2.0+.5) );
			e.Graphics.DrawRectangle( pen, rectangle );
		}
	}
}
