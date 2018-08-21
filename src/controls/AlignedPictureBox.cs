/**
 * Rozszerzenie klasy PictureBox.
 * Dodaje przyleganie obrazu do rodzica względem podanej wartości.
 *
 * Copyright ⓒ 2015. Wszystkie prawa zastrzeżone.
 *
 * Autor  - Kamil Biały
 * Wersja - 1.0
 * Zmiana - 2015-06-01
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace CDesigner
{
	/**
	 * Rozszerzenie klasy PictureBox.
	 * Pozwala na przyleganie obrazu do dowolnej części rodzica (lewo, prawo, środek...).
	 * Używany na stronie początkowej (z listą wzorów).
	 * 
	 * Przyleganie i wartości zmiennej _align (Align):
	 * 0 - lewo   - góra
	 * 1 - środek - góra
	 * 2 - prawo  - góra
	 * 3 - lewo   - środek
	 * 4 - środek - środek
	 * 5 - prawo  - środek
	 * 6 - lewo   - dół
	 * 7 - środek - dół
	 * 8 - prawo  - dół
	 */
	public class AlignedPictureBox : PictureBox
	{
		private int _align = 0;

		// ------------------------------------------------------------- Align ----------------------------------------
		
		public int Align
		{
			get { return this._align; }
			set { this._align = value; }
		}

		// ------------------------------------------------------------- CheckLocation --------------------------------
		
		public void CheckLocation( )
		{
			Panel parent   = (Panel)this.Parent;
			Point location = this.Location;

			// brak obrazka lub przyleganie do lewej
			if( this.Image == null || parent == null || this._align == 0 )
			{
				this.Location = new Point( 0, 0 );
				return;
			}

			// oblicz realną szerokość
			int width = parent.DisplayRectangle.Width + SystemInformation.BorderSize.Width * 2;
			width += parent.VerticalScroll.Visible ? SystemInformation.VerticalScrollBarWidth : 0;

			// oblicz realną wysokość
			int height = parent.DisplayRectangle.Height + SystemInformation.BorderSize.Height * 2;
			height += parent.HorizontalScroll.Visible ? SystemInformation.HorizontalScrollBarHeight : 0;

			// sprawdź czy należy przestawić panel w poziomie
			if( width - parent.Width == 0 )
			{
				int diff = parent.DisplayRectangle.Width - this.Width;
				location.X = diff / 2;
			}
			else if( parent.HorizontalScroll.Value == 0 )
				location.X = 0;

			// sprawdź czy należy przestawić panel w pionie
			if( height - parent.Height == 0 )
			{
				int diff = parent.DisplayRectangle.Height - this.Height;
				location.Y = diff / 2;
			}
			else if( parent.VerticalScroll.Value == 0 )
				location.Y = 0;

			// zabezpieczenie przeciwko ujemnej pozycji
			if( location.X < 0 )
				location.X = 0;
			if( location.Y < 0 )
				location.Y = 0;

			this.Location = location;
		}

		// ------------------------------------------------------------- OnPaint --------------------------------------
		
		protected override void OnPaint( PaintEventArgs ev )
		{
			// dla pewności - pewnie oryginalnie jest, ale...
			if( !this.Visible )
				return;

			base.OnPaint( ev );
		}
	};
}
