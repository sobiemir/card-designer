///
/// $c02 AlignedPictureBox.cs
/// 
/// Plik zawiera klasę rozszerzającą klasę PictureBox o funkcje przylegania.
/// Dzięki temu kontrolka umieszczona w innej kontrolce może w prosty sposób przylegać
/// do jednej z krawędzi.
/// 
/// Autor: Kamil Biały
/// Od wersji: 0.4.x.x
///

using System.Windows.Forms;
using System.Drawing;

namespace CDesigner.Controls
{
	/// 
	/// <summary>
	/// Klasa rozszerzająca klasę PictureBox o funkcje przylegania.
	/// Kontrolka dzięki temu może przylegać do krawędzi w zależności od podanej wartości.
	/// Typy przylegania wypisane są w właściwości, która go zmienia.
	/// </summary>
	///
	public class AlignedPictureBox : PictureBox
	{
#region KONSTRUKTOR / WŁAŚCIWOŚCI
		
		/// <summary>
		/// Konstruktor klasy.
		/// Uzupełnia zmienne domyślnymi wartościami i wywołuje konstruktor z klasy bazowej.
		/// </summary>
		//* ============================================================================================================
		public AlignedPictureBox()
			: base()
		{
			this.Align = 0;
		}

		/// <summary>
		/// Właściwość odpowiadająca za przyleganie kontrolki.
		/// Jako wartość przyjmuje typ INT. Dopuszczalne wartości dla właściwości:
		/// 
		/// <list type="bullet">
		///     <item><description>0 <i>(góra - lewo)</i></description></item>
		///     <item><description>1 <i>(góra - środek)</i></description></item>
		///     <item><description>2 <i>(góra - prawo)</i></description></item>
		///     <item><description>3 <i>(środek - lewo)</i></description></item>
		///     <item><description>4 <i>(środek)</i></description></item>
		///     <item><description>5 <i>(środek - prawo)</i></description></item>
		///     <item><description>6 <i>(dół - lewo)</i></description></item>
		///     <item><description>7 <i>(dół - środek)</i></description></item>
		///     <item><description>8 <i>(dół - prawo)</i></description></item>
		/// </list>
		/// </summary>
		//* ============================================================================================================
		public int Align { get; set; }
		
#endregion

#region FUNKCJE PODSTAWOWE

		/// <summary>
		/// Sprawdzanie pozycji kontrolki.
		/// Funkcja sprawdza umieszczenie kontrolki wewnątrz innej kontrolki.
		/// Dzięki tej funkcji kontrolka przylega zawsze do wyznaczonego punktu.
		/// </summary>
		//* ============================================================================================================
		public void checkLocation()
		{
			var parent   = (Panel)this.Parent;
			var location = this.Location;

			// brak obrazka lub przyleganie do lewej
			if( this.Image == null || parent == null || this.Align == 0 )
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
				int diff = parent.DisplayRectangle.Width - this.Image.Width;
				location.X = diff / 2;
			}
			else if( parent.HorizontalScroll.Value == 0 )
				location.X = 0;

			// sprawdź czy należy przestawić panel w pionie
			if( height - parent.Height == 0 )
			{
				int diff = parent.DisplayRectangle.Height - this.Image.Height;
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

#endregion

#region AKCJE
		
		/// <summary>
		/// Akcja wywoływana podczas ryoswania kontrolki.
		/// Upewnia się, czy kontrolka nie jest rysowana gdy nie jest widoczna.
		/// Wywołuje akcję z klasy bazowej.
		/// </summary>
		/// 
		/// <param name="ev">Argumenty zdarzenia.</param>
		//* ============================================================================================================
		protected override void OnPaint( PaintEventArgs ev )
		{
			// dla pewności - pewnie oryginalnie jest, ale...
			if( !this.Visible )
				return;

			base.OnPaint( ev );
		}

#endregion
	}
}
