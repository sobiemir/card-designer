///
/// $c01 PageField.cs
/// 
/// Plik zawiera klasę rozszerzająca klasę Label o dodatkowe funkcje skalowania.
/// Kontrolka dzięki temu może być rysowana względem ustawionego powiększenia w edytorze,
/// oraz dobierać wymiary odpowiednio do ustawionych wartości w milimetrach.
/// Tekst wyświetlany może ulegać transformacji, ustawionej w edytorze.
/// 
/// Autor: Kamil Biały
/// Od wersji: 0.1.x.x
/// Ostatnia zmiana: 2016-12-24
/// 
/// CHANGELOG:
/// [29.03.2015] Pierwsza wersja pliku.
/// [04.05.2015] Struktura do przechowywania dodatkowych danych, ujednolicenie nazw zmiennych,
///              Skalowanie ramki względem DPI, margines tekstu, przyleganie tekstu do krawędzi.
/// [10.05.2015] Nowa klasa - PageField - w przyszłości zastąpi CustomLabel, prostsza organizacja.
/// [16.05.2015] Zmiana nazwy pliku CustomLabel na PageField, zastąpienie i usunięcie starej klasy,
///              Pobieranie przylegania tekstu, punkt zaczepienia pola - pobieranie i ustawianie
///              wartości względem punktu zaczepienia, zmiana konwersji typów liczbowych.
/// [31.05.2015] Wymiary rodzica, funkcja do ustawiania pozycji kontrolki i jej odświeżania.
/// [05.06.2015] Zmiana przelicznika DPI, transformacje tekstu.
/// [09.06.2015] Funkcje do pilnowania, aby pole nie wychodziło poza punkty graniczne rodzica,
///              możliwość wpisania już skonwertowanych do DPI wyimarów rodzica.
/// [24.12.2016] Komentarze, regiony.
///

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace CDesigner.Controls
{
	/// 
	/// <summary>
	/// Klasa rozszerzająca klasę Label o dodatkowe funkcje skalowania.
	/// Kontrolka dzięki temu może spełniać wymogi pola, rysowanego w edytorze.
	/// Posiada funkcje zmieniające wartości w pikselach względem milimetrów podawanych w edytorze.
	/// Pozwala na zmianę wyświetlanego tekstu oraz jego transformację z zachowaniem oryginału.
	/// </summary>
	///
	class PageField : Label
	{
#region ZMIENNE

		/// <summary>Kolor ramki wokół pola.</summary>
		private Color _borderColor;

		/// <summary>Rozmiar ramki wokół pola.</summary>
		private int _borderSize;

		/// <summary>Ścieżka do obrazka wyświetlanego w polu.</summary>
		private string _imagePath;

		/// <summary>Wymiary pola w milimetrach.</summary>
		private RectangleF _dpiBounds;

		/// <summary>Wymiary ramki pola w milimetrach.</summary>
		private float _dpiBorderSize;

		/// <summary>Skala powiększenia (DPI), domyślnie 100.</summary>
		private double _dpiScale;

		/// <summary>Obliczona skala przez którą mnożone są wartości.</summary>
		private double _dpiConvScale;

		/// <summary>Rozmiar czcionki w milimetrach.</summary>
		private double _dpiFontSize;

		/// <summary>Margines wokół tekstu w milimetrach.</summary>
		private PointF _dpiTextMargin;

		/// <summary>Margines wewnętrzny tekstu w milimetrach.</summary>
		private float _dpiPadding;

		/// <summary>Rozmiar rodzica na którym znajduje się pole, podawany w milimetrach.</summary>
		private Point _dpiParentSize;

		/// <summary>Obraz wyświetlany w polu.</summary>
		private Image _backImage;

		/// <summary>Ilość pikseli na DPI, używana przy przeliczaniu.</summary>
		private double _pixelsPerDPI;

		/// <summary>Wymiary rodzica na którym znajduje się pole.</summary>
		private Size _parentBounds;

		/// <summary>Rodzaj transformacji tekstu.</summary>
		private int _textTransform;

		/// <summary>Tekst oryginalny bez transformacji.</summary>
		private string _originalText;

		/// <summary>Aktualny tekst wyświetlany w polu.</summary>
		private string _currentText;

		/// <summary>Czy dodatkowy margines został zastosowany?</summary>
		private bool _extraMargin;

#endregion

#region KONSTRUKTOR / WŁAŚCIWOŚCI

		/// <summary>
		/// Konstruktor klasy.
		/// Uzupełnia zmienne domyślnymi wartościami i wywołuje konstruktor z klasy bazowej.
		/// </summary>
		//* ============================================================================================================
		public PageField()
			: base()
		{
			this._borderColor   = Color.Black;
			this._borderSize    = 0;
			this._imagePath     = null;
			this._dpiBounds     = new RectangleF( 0.0f, 0.0f, 1.0f, 1.0f );
			this._dpiBorderSize = 0.0f;
			this._dpiScale      = 1.0;
			this._dpiConvScale  = 3.93714927048264;
			this._dpiTextMargin = new PointF( 2.0f, 2.0f );
			this._dpiPadding    = 0.0f;
			this._dpiParentSize = new Point( -1, -1 );
			this._backImage     = null;
			this._pixelsPerDPI  = 3.93714927048264;
			this._parentBounds  = new Size(800, 600);
			this._textTransform = 0;
			this._originalText  = null;
			this._currentText   = "";
			this._extraMargin   = false;
		}

		/// <summary>
		/// Właściwość pozwalająca na zmianę rozmiaru ramki.
		/// W odróżnieniu od oryginalnej właściwości, ta pozwala na narysowanie ramki o dowolnej grubości.
		/// Grubość przeliczana jest z milimetrów na piksele, pobierana jest w milimetrach.
		/// </summary>
		//* ============================================================================================================
		public float DPIBorderSize
		{
			get { return this._dpiBorderSize; }
			set
			{
				this._dpiBorderSize = value;
				this._borderSize = Convert.ToInt32( (double)this._dpiBorderSize * this._dpiConvScale );

				// ramka musi być widoczna, gdy jest włączona...
				if( this._dpiBorderSize > 0.0 && this._borderSize == 0 )
					this._borderSize = 1;
			}
		}

		/// <summary>
		/// Właściwość pozwalająca na zmianę koloru ramki.
		/// Oryginalnie kontrolka nie posiada prostego sposobu, którym można zmienić kolor wyświetlanej ramki.
		/// </summary>
		//* ============================================================================================================
		public Color BorderColor
		{
			get { return this._borderColor; }
			set { this._borderColor = value; }
		}

		/// <summary>
		/// Właściwość pozwalająca na zmianę marginesu dla tekstu w kontrolce.
		/// Wo odróżnieniu od oryginalnej właściwości, ta pozwala na ustawienie marginesu w milimetrach.
		/// Margines przeliczany jest przy ustawianiu z milimetrów na piksele, a pobierany jest w milimetrach.
		/// </summary>
		//* ============================================================================================================
		public float DPIPadding
		{
			get { return this._dpiPadding; }
			set
			{
				this._dpiPadding = value;

				if( this._dpiScale < 1.0 )
					this.Padding = new Padding( Convert.ToInt32((double)this._dpiPadding * this._dpiConvScale + 0.01) );
				else
					this.Padding = new Padding( Convert.ToInt32((double)this._dpiPadding * this._dpiConvScale) );
			}
		}

		/// <summary>
		/// Właściwość pozwalająca na zmianę punktów granicznych kontrolki.
		/// Pozwala na ustawienie punktów w milimetrach, które są przeliczane na piksele.
		/// Pobierana wartość zwracana jest w milimetrach.
		/// </summary>
		//* ============================================================================================================
		public RectangleF DPIBounds
		{
			get { return this._dpiBounds; }
			set
			{
				// pozycja X
				if( value.X < 0 )
					value.X = this._dpiBounds.X;
				else
				{
					this.Left = Convert.ToInt32((double)value.X * this._dpiConvScale);
				}

				// pozycja Y
				if( value.Y < 0 )
					value.Y = this._dpiBounds.Y;
				else
				{
					this.Top = Convert.ToInt32((double)value.Y * this._dpiConvScale);
				}

				// szerokość
				if( value.Width < 0 )
					value.Width = this._dpiBounds.Width;
				else
				{
					int width = Convert.ToInt32((double)value.Width * this._dpiConvScale);

					// nie przekraczaj granicy
					if( this._dpiParentSize.X > -1 && this.Left + width > this._parentBounds.Width )
					{
						width       = this.Width;
						value.Width = this._dpiBounds.Width;
					}
					this.Width = width;
				}

				// wysokość
				if( value.Height < 0 )
					value.Height = this._dpiBounds.Height;
				else
				{
					int height = Convert.ToInt32((double)value.Height * this._dpiConvScale);

					// nie przekraczaj granicy
					if( this._dpiParentSize.Y > -1 && this.Top + height > this._parentBounds.Height )
					{
						height       = this.Height;
						value.Height = this._dpiBounds.Height;
					}
					this.Height = height;
				}

				this._dpiBounds = value;
			}
		}

		/// <summary>
		/// Właściwość pozwalająca na zmianę rozmiaru czcionki.
		/// Czcionka wyświetlana jest w różnym rozmiarze w zależności od powiększenia.
		/// Czcionka pobierana jest w takiej wartości, w jakiej została ustawiona.
		/// </summary>
		//* ============================================================================================================
		public double DPIFontSize
		{
			// pobierz rozmiar czcionki (rozmiar * skala)
			get { return this._dpiFontSize; }
			// ustaw rozmiar czcionki
			set
			{
				// rozmiar czcionki nie może być mniejszy od 1...
				value = value < 1 ? 1 : value;

				// utwórz czcionkę z nowym rozmiarem
				var font = new Font
				(
					this.Font.FontFamily,
					(float)(value * this._dpiScale),
					this.Font.Style,
					GraphicsUnit.Point,
					this.Font.GdiCharSet,
					this.Font.GdiVerticalFont
				);

				// zmień czcionkę i informacje o rozmiarze
				this._dpiFontSize = value;
				this.Font = font;
			}
		}

		/// <summary>
		/// Właściwość pozwalająca na zmianę tła pola.
		/// Tło rysowane jest przy akcji odświeżającej kontrolkę.
		/// </summary>
		//* ============================================================================================================
		public Image BackImage
		{
			get { return this._backImage; }
			set { this._backImage = value; }
		}

		/// <summary>
		/// Właściwość pozwalająca na zmianę marginesu tekstu.
		/// Aktualnie wartość ta nie jest używana, jako margines używany jest <see cref="DPIPadding"/>.
		/// </summary>
		//* ============================================================================================================
		public PointF TextMargin
		{
			get { return this._dpiTextMargin; }
			set { this._dpiTextMargin = value; }
		}

		/// <summary>
		/// Właściwość pozwala na aktywacje dodatkowego marginesu tekstu.
		/// Dodatkowy margines nie jest aktualnie używany, jako margines traktowany jest <see cref="DPIPadding"/>.
		/// </summary>
		//* ============================================================================================================
		public bool ApplyTextMargin
		{
			get { return this._extraMargin; }
			set { this._extraMargin = value; }
		}

		/// <summary>
		/// Właściwość pozwala na zmianę ścieżki tła kontrolki.
		/// Ścieżka wyświetlana jest w edytorze wzorów, domyślnie pobierana jest z katalogu wzoru po zapisie.
		/// W przypadku zmiany obrazka, ustawiana jest na ścieżkę w której obraz się znajduje.
		/// </summary>
		//* ============================================================================================================
		public string BackImagePath
		{
			// pobierz ścieżkę do aktualnego obrazu
			get { return this._imagePath; }
			// ustaw nową ścieżkę do obrazu
			set { this._imagePath = value; }
		}

		/// <summary>
		/// Właściwość pozwalająca zmienić skalę wyświetlania pola.
		/// Skala podawana jest z przedziału od 50 do 300, traktowana jest jako DPI.
		/// Wartość podawana przeliczana przez ilość pikseli na DPI, zapewnia potem poprawne wyświetlanie elementów
		/// znajdujących się w kontrolce.
		/// Każde pole na stronie powinno mieć taką samą skalę i przelicznik.
		/// </summary>
		//* ============================================================================================================
		public double DPIScale
		{
			// pobierz aktualną skalę DPI
			get { return this._dpiScale; }
			// ustaw skalę DPI
			set
			{
				// od 50 do 300 DPI
				value = value < 0.5 ? 0.5 : value > 3.0 ? 3.0 : value;

				this._dpiScale	     = value;
				this._dpiConvScale = this._pixelsPerDPI * value;

				// odśwież wartości
				this.setParentBounds( this._dpiParentSize.X, this._dpiParentSize.Y );
				this.DPIBounds     = this.DPIBounds;
				this.DPIBorderSize = this.DPIBorderSize;
				this.DPIFontSize   = this.DPIFontSize;
				this.DPIPadding    = this.DPIPadding;
			}
		}

		/// <summary>
		/// Właściwość pozwalająca na zmianę transformacji tekstu.
		/// Właściwość posiada 4 wartości do ustawienia w typie INT:
		/// 
		/// <list type="bullet">
		///		<item><description>0 <i>(tekst wyświetlany bez zmian)</i></description></item>
		///		<item><description>1 <i>(zamiana tekstu na duże litery)</i></description></item>
		///		<item><description>2 <i>(zamiana tekstu na małe litery)</i></description></item>
		///		<item><description>3 <i>(pierwsza litera w słowie duża, pozostałe małe)</i></description></item>
		/// </list>
		/// </summary>
		//* ============================================================================================================
		public int TextTransform
		{
			// pobierz transformacje tekstu
			get { return this._textTransform; }
			// ustaw transformacje tekstu
			set
			{
				this._textTransform = value;
				switch( this._textTransform )
				{
				case 0: this._currentText = this._originalText; break;
				case 1: this._currentText = this._originalText.ToUpper(); break;
				case 2: this._currentText = this._originalText.ToLower(); break;
				case 3: this._currentText = System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase( this._originalText.ToLower() ); break;
				}
				this.Refresh();
			}
		}

		/// <summary>
		/// Właściwość pozwalająca na zmianę tekstu w kontrolce.
		/// Nadpisuje oryginalną właściwość i rozszerza o transformacje tekstu, ustawione w <see cref="TextTransform"/>.
		/// Tak więc podczas ustawiania tekstu, stosowana jest na nim aktualnie ustawiona transformacja.
		/// Tekst oryginalny jest zachowywany i możliwy do pobrania przez <see cref="OriginalText"/>.
		/// </summary>
		//* ============================================================================================================
		public override string Text
		{
			// pobierz tekst kontrolki
			get { return this._currentText; }
			// zmień tekst kontrolki
			set
			{
				this._originalText = value;
				switch( this._textTransform )
				{
				case 0: this._currentText = value; break;
				case 1: this._currentText = value.ToUpper(); break;
				case 2: this._currentText = value.ToLower(); break;
				case 3: this._currentText = System.Globalization.CultureInfo.CurrentCulture.TextInfo.
					ToTitleCase( value.ToLower() ); break;
				}
				this.Refresh();
			}
		}

		/// <summary>
		/// Właściwość pozwalająca na pobranie oryginalnego tekstu.
		/// Oryginalny tekst dodatkowo zapisywany jest osobno, aby po zmianie transformacji stosowana była ona
		/// na oryginalnym tekście a nie na tym, który był już transformowany.
		/// </summary>
		//* ============================================================================================================
		public string OriginalText
		{
			// pobierz oryginalny tekst kontrolki (bez transformacji)
			get { return this._originalText; }
		}

#endregion

#region FUNKCJE PODSTAWOWE

		/// <summary>
		/// Funkcja zmienia wymiary pola na te podane w argumentach.
		/// Wymiary podawane są w milimetrach, po czym konwertowane na piksele względem odpowiediego współczynnika.
		/// </summary>
		/// 
		/// <param name="width">Szerokość pola w milimetrach.</param>
		/// <param name="height">Wysokość pola w milimetrach.</param>
		//* ============================================================================================================
		public void setParentBounds( int width, int height )
		{
			this._dpiParentSize = new Point( width, height );

			this._parentBounds.Width  = Convert.ToInt32( (double)width * this._dpiConvScale );
			this._parentBounds.Height = Convert.ToInt32( (double)height * this._dpiConvScale );
		}

		/// <summary>
		/// Zmienia przyleganie tekstu do kontrolki.
		/// Funkcja pozwala na szybką zmianę przylegania tekstu, podawaną w typie INT.
		/// Dopuszczalne wartości:
		/// 
		/// <list type="bullet">
		///	    <item><description>0 <i>(góra - lewo)</i></description></item>
		///	    <item><description>1 <i>(góra - środek)</i></description></item>
		///	    <item><description>2 <i>(góra - prawo)</i></description></item>
		///	    <item><description>3 <i>(środek - lewo)</i></description></item>
		///	    <item><description>4 <i>(środek)</i></description></item>
		///	    <item><description>5 <i>(środek - prawo)</i></description></item>
		///	    <item><description>6 <i>(dół - lewo)</i></description></item>
		///	    <item><description>7 <i>(dół - środek)</i></description></item>
		///	    <item><description>8 <i>(dół - prawo)</i></description></item>
		/// </list>
		/// </summary>
		/// 
		/// <param name="align">Wartość przylegania kontrolki.</param>
		//* ============================================================================================================
		public void setTextAlignment( int align )
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

		/// <summary>
		/// Pobiera aktualnie ustawione przyleganie tekstu.
		/// Funkcja zwraca wartość przylegania w typie INT, używaną w edytorze wzorów.
		/// Szczegóły dotyczące zwracanej wartości znajdują się w funkcji <see cref="setTextAlignment"/>.
		/// Pozwala na pobranie aktualnego przylegania lub zamianę podanego na numer.
		/// W przypadku gdy funkcja ma zwrócić przyleganie kontrolki, nie należy nic podawać, albo podać -1.
		/// </summary>
		/// 
		/// <param name="alignment">Przyleganie do zamiany lub wartość -1.</param>
		/// 
		/// <returns>Przyleganie zamienione na formę, używaną w edytorze wzorów.</returns>
		//* ============================================================================================================
		public int getTextAlignment( int alignment = -1 )
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

		/// <summary>
		/// Pobiera pozycję pola względem punktu zaczepienia.
		/// Punkt zaczepienia pola jest punktem, względem którego podawane są wymiary w edytorze.
		/// Zmiana punktu zaczepienia powoduje również zmianę pozycji (bez zmiany szerokości i wysokości).
		/// Możliwe wartości punktu zaczepienia:
		/// 
		/// <list type="bullet">
		///     <item><description>ContentAlignment.TopLeft <i>(góra - lewo)</i></description></item>
		///     <item><description>ContentAlignment.TopRight  <i>(góra - prawo)</i></description></item>
		///     <item><description>ContentAlignment.BottomLeft <i>(dół - lewo)</i></description></item>
		///     <item><description>ContentAlignment.BottomRight <i>(dół - prawo)</i></description></item>
		/// </list>
		/// </summary>
		/// 
		/// <param name="align">Punkt zaczepienia pola.</param>
		/// 
		/// <returns>Pozycja pola względem punktu zaczepienia.</returns>
		//* ============================================================================================================
		public PointF getPosByAlignPoint( ContentAlignment align )
		{
			var point = new PointF();

			switch( (int)align )
			{
				case 0x001:
					point.X = this._dpiBounds.X;
					point.Y = this._dpiBounds.Y;
				break;
				case 0x004:
					point.X = this._dpiBounds.X + this._dpiBounds.Width;
					point.Y = this._dpiBounds.Y;
				break;
				case 0x100:
					point.X = this._dpiBounds.X;
					point.Y = this._dpiBounds.Y + this._dpiBounds.Height;
				break;
				case 0x400:
					point.X = this._dpiBounds.X + this._dpiBounds.Width;
					point.Y = this._dpiBounds.Y + this._dpiBounds.Height;
				break;
			}

			return point;
		}

		/// <summary>
		/// Zmienia pozycję kontrolki względem osi X dla wybranego punktu zaczepienia.
		/// Wszelkie możliwe kombinacje punktu zaczepienia opisane są w funkcji <see cref="getPosByAlignPoint"/>.
		/// Funkcja oblicza wartość X względem podanej relatywnej wartości dla punktu zaczepienia.
		/// </summary>
		/// 
		/// <param name="x">Nowa pozycja kontrolki względem osi X.</param>
		/// <param name="align">Punkt zaczepienia kontrolki.</param>
		//* ============================================================================================================
		public void setPosXByAlignPoint( float x, ContentAlignment align )
		{
			switch( (int)align )
			{
				case 0x000:
				case 0x001:
				case 0x100: this._dpiBounds.X = x; break;
				case 0x004:
				case 0x400: this._dpiBounds.X = x - this._dpiBounds.Width; break;
			}

			int left = Convert.ToInt32((double)this._dpiBounds.X * this._dpiConvScale);

			if( left + this.Width > this._parentBounds.Width )
				left = this._parentBounds.Width - this.Width;
			else if( left < 0 )
				left = 0;

			this.Left = left;
		}

		/// <summary>
		/// Zmienia pozycję kontrolki względem osi Y dla wybranego punktu zaczepienia.
		/// Wszelkie możliwe kombinacje punktu zaczepienia opisane są w funkcji <see cref="getPosByAlignPoint"/>.
		/// Funkcja oblicza wartość Y względem podanej relatywnej wartości dla punktu zaczepienia.
		/// </summary>
		/// 
		/// <param name="y">Nowa pozycja kontrolki względem osi Y.</param>
		/// <param name="align">Punkt zaczepienia kontrolki.</param>
		//* ============================================================================================================
		public void setPosYByAlignPoint( float y, ContentAlignment align )
		{
			switch( (int)align )
			{
				case 0x000:
				case 0x001:
				case 0x004: this._dpiBounds.Y = y; break;
				case 0x100:
				case 0x400: this._dpiBounds.Y = y - this._dpiBounds.Height; break;
			}

			int top = Convert.ToInt32((double)this._dpiBounds.Y * this._dpiConvScale);
			
			if( top + this.Height > this._parentBounds.Height )
				top = this._parentBounds.Height - this.Height;
			else if( top < 0 )
				top = 0;

			this.Top = top;
		}

		/// <summary>
		/// Zmienia pozycję kontrolki względem osi X i Y dla wybranego punktu zaczepienia.
		/// Wszelkie możliwe kombinacje punktu zaczepienia opisane są w funkcji <see cref="getPosByAlignPoint"/>.
		/// Jest to skrót pozwalający na szybką zamianę pozycji pola użyciem jednej funkcji.
		/// </summary>
		/// 
		/// <param name="x">Nowa pozycja kontrolki względem osi X.</param>
		/// <param name="y">Nowa pozycja kontrolki względem osi Y.</param>
		/// <param name="align">Punkt zaczepienia kontrolki.</param>
		//* ============================================================================================================
		public void setPxLocation( int x, int y, ContentAlignment align )
		{
			if( x + this.Width > this._parentBounds.Width )
				x = this._parentBounds.Width - this.Width;
			else if( x < 0 )
				x = 0;

			if( y + this.Height > this._parentBounds.Height )
				y = this._parentBounds.Height - this.Height;
			else if( y < 0 )
				y = 0;

			this.Location = new Point( x, y );
			this.refreshLocation();
		}

		/// <summary>
		/// Odświeża punkty graniczne kontrolki.
		/// Funkcja pozwalająca odświeżyć pozycję kontrolki i jej wymiary w prosty sposób.
		/// </summary>
		//* ============================================================================================================
		public void refreshLocation()
		{
			this.DPIBounds = new RectangleF
			(
				(float)((double)this.Location.X / this._dpiConvScale),
				(float)((double)this.Location.Y / this._dpiConvScale),
				this.DPIBounds.Width,
				this.DPIBounds.Height
			);
		}

#endregion

#region AKCJE

		/// <summary>
		/// Akcja wywoływana podczas ryoswania kontrolki.
		/// Rysuje obraz na polu wraz z ramką po narysowaniu kontrolki przez klasę bazową.
		/// Wywołuje akcję z klasy bazowej.
		/// </summary>
		/// 
		/// <param name="ev">Argumenty zdarzenia.</param>
		//* ============================================================================================================
		protected override void OnPaint( PaintEventArgs ev )
		{
			// rysuj dopasowany obraz
			if( this._backImage != null )
				ev.Graphics.DrawImage( this._backImage, 0, 0, this.Width, this.Height );

			// rysuj tekst...
			base.OnPaint( ev );

			// rysuj ramkę
			if( this._borderSize > 0 )
				ControlPaint.DrawBorder( ev.Graphics, this.ClientRectangle,
					this._borderColor, this._borderSize, ButtonBorderStyle.Solid,
					this._borderColor, this._borderSize, ButtonBorderStyle.Solid,
					this._borderColor, this._borderSize, ButtonBorderStyle.Solid,
					this._borderColor, this._borderSize, ButtonBorderStyle.Solid );
		}

#endregion
	};
}
