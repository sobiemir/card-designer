///
/// $i04 InfoForm.cs (I06)
/// 
/// Okno informacji o programie.
/// Wyświetla informacje o programie i informacje o zarejestrowanym użytkowniku.
/// Pozwala na wyświetlenie loga ogranizacji dla której program został zarejestrowany.
/// Po wpisaniu ustalonego wcześniej ciągu znaków wyświetla sie ukryta zawartość.
/// 
/// Autor: Kamil Biały
/// Od wersji: 0.4.x
///

using System.Drawing;
using System.Windows.Forms;
using CDesigner.Utils;

namespace CDesigner.Forms
{
	/// 
	/// <summary>
	/// Klasa tworząca okno zawierające informacje o programie.
	/// Wyświetla numer wersji, datę wydania i informacje o rejestracji.
	/// Pozwala na wyświetlenie loga organizacji dla której aplikacja została zarejestrowana.
	/// </summary>
	/// 
	public partial class InfoForm : Form
	{
#region KONSTRUKTORY

		/// <summary>
		/// Konstruktor klasy.
		/// Tworzy okno, ustawia ikonkę programu i tłumaczy wszystkie napisy.
		/// </summary>
		//* ============================================================================================================
		public InfoForm()
		{
			this.InitializeComponent();

			this.PB_AppLogo.Image = Program.GetBitmap( BITMAPCODE.CDESIGNER256 );
			this.Icon = Program.GetIcon();
		}

#endregion

#region FUNKCJE PODSTAWOWE

		/// <summary>
		/// Odświeżanie danych w oknie.
		/// Okno tworzone jest wcześniej, aby można go było potem szybko otworzyć.
		/// Przed otwarciem takiego okna warto wywołać tą funkcję, aby nie było żadnych anomalii.
		/// W tej funkcji odświeżają się rzeczy zmieniane dynamicznie podczas działania programu.
		/// </summary>
		/// 
		/// <seealso cref="refreshAndOpen"/>
		/// <seealso cref="Language"/>
		//* ============================================================================================================
		public void refreshForm()
		{
#		if DEBUG
			Program.LogMessage( "Odświeżanie wszystkich potrzebnych danych." );
#		endif

			this.translateForm();
		}

		/// <summary>
		/// Odświeżanie danych i otwieranie okna.
		/// Okno tworzone jest wcześniej, aby można go było potem szybko otworzyć.
		/// Przed otwarciem takiego okna warto wywołać tą funkcję, aby nie było żadnych anomalii.
		/// W tej funkcji odświeżają się rzeczy zmieniane dynamicznie podczas działania programu.
		/// Funkcja po odświeżeniu otwiera okno.
		/// </summary>
		/// 
		/// <seealso cref="refreshForm"/>
		/// <seealso cref="Language"/>
		//* ============================================================================================================
		public DialogResult refreshAndOpen( Form parent = null, bool dialog = true )
		{
			this.refreshForm();
			return Program.OpenForm( this, parent, dialog );
		}

		/// <summary>
		/// Translator formularza.
		/// Funkcja tłumaczy wszystkie statyczne elementy programu.
		/// Wywoływana jest z konstruktora oraz podczas odświeżania ustawień językowych.
		/// Jej użycie nie powinno wykraczać poza dwa wyżej wymienione przypadki.
		/// </summary>
		/// 
		/// <seealso cref="InfoForm"/>
		/// <seealso cref="Language"/>
		//* ============================================================================================================
		protected void translateForm()
		{
			var values = Language.GetLines( "Info", "Labels" );

#		if DEBUG
			Program.LogMessage( "Tłumaczenie kontrolek znajdujących się na formularzu." );
#		endif
			// przycisk
			this.B_Close.Text = Language.GetLine( "Info", "Buttons", (int)LANGCODE.I06_BUT_CLOSE );
			
#		if DEBUG
			Program.LogMessage( "Uzupełnianie informacji o rejestracji programu." );
#		endif
			// data kompilacji aplikacji
			var buildDate = Program.BUILD_DATE.ToString("dd.MM.yyyy");

			// wersja aplikacji
			this.L_Version.Text     = "v" + Program.VERSION + " (CDesigner :: " + Program.CODE_NAME + ")";
			this.L_AuthorApp.Text   = values[(int)LANGCODE.I06_LAB_AUTHOR] + " Kamil Biały";
			this.L_ReleaseDate.Text = values[(int)LANGCODE.I06_LAB_COMPILDATE] + " " + buildDate;
			
			// nazwa okna
			this.Text = Language.GetLine( "FormNames", (int)LANGCODE.GFN_ABOUTPROGRAM );
		}

#endregion

#region ZDARZENIA
		/// @cond EVENTS

		/// <summary>
		/// Akcja wywoływana przy rysowaniu tabeli.
		/// Rysuje linię na samej górze oddzielającą treść od "paska informacji" w oknie.
		/// </summary>
		/// 
		/// <param name="sender">Obiekt wywołujący zdarzenie.</param>
		/// <param name="ev">Argumenty zdarzenia.</param>
		//* ============================================================================================================
		private void TLP_StatusBar_Paint( object sender, PaintEventArgs ev )
		{
			ev.Graphics.DrawLine
			(
				new Pen( SystemColors.ControlDark ),
				this.TLP_StatusBar.Bounds.X,
				0,
				this.TLP_StatusBar.Bounds.Right,
				0
			);
		}

		/// @endcond
#endregion
	}
}
