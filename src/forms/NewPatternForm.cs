///
/// $i05 NewPatternForm.cs
/// 
/// Okno edycji dodawania nowego wzoru.
/// Tworzy lub kopiuje wybrany wzór i bezpośrednio po zamknięciu otwiera zakładkę edycji wzoru.
/// Pozwala na utworzenie wzoru o dowolnych wymiarach.
/// 
/// Autor: Kamil Biały
/// Od wersji: 0.1.x
/// Ostatnia zmiana: 2016-11-11
/// 
/// CHANGELOG:
/// [23.03.2015] Wersja początkowa.
/// [01.06.2015] Blokada pola wyboru kopiowania wzoru - brak funkcjonalności.
/// [09.06.2015] Sprawdzanie poprawności znaków wpisanych w polu nazwy wzoru.
/// [11.11.2016] Odblokowanie pola wyboru kopiowania wzoru i oprogramowanie akcji.
///              Opisanie kodu, regiony, uporządkowanie kodu.
///

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;
using CDesigner.Utils;

namespace CDesigner.Forms
{
    /// 
    /// <summary>
    /// Formularz dodawania nowego wzoru.
    /// Pozwala na kopiowanie istniejącego wzoru lub utworzenie nowego.
    /// Każdy wzór identyfikowany jest po nazwie, która jest jednocześnie nazwą folderu
    /// w którym znajdują się wszystkie jego pliki.
    /// Wszystkie dostępne wymiary i nazwy wymiarów które można wybrać podczas tworzenia wzoru
    /// pobierane są z ustawień - można je zmienić po wejściu w ustawienia.
    /// </summary>
    /// 
    /// @todo <dfn><small>[0.9.x.x]</small></dfn> Wczytywanie wymiarów i nazw wymiarów z ustawień.
    /// 
    public partial class NewPatternForm : Form
    {
#region ZMIENNE

        /// <summary>Nazwa aktualnie używanego wzoru.</summary>
		private string _name;
        
		/// <summary>Informacja o tym czy wyświetlany jest dymek z wiadomością.</summary>
		private bool _tooltip_show;

        /// <summary>Nazwy zdefiniowanych formatów dla wzorów.</summary>
        private string[] _format_names;

        /// <summary>Wymiary dla formatów podanych w kontrolce powyżej.</summary>
        private int[,] _format_dims;

        /// <summary>Lista dostępnych wzorów w aplikacji.</summary>
        private List<PatternInfo> _patterns;

#endregion

#region KONSTRUKTOR / WŁAŚCIWOŚCI

        /// <summary>
		/// Konstruktor formularza.
		/// Tworzy okno, ustawia ikonkę programu i tłumaczy wszystkie napisy.
		/// </summary>
        /// 
        /// <seealso cref="translateForm"/>
        /// <seealso cref="fillAvailablePatterns"/>
		//* ============================================================================================================
        public NewPatternForm()
        {
            this.InitializeComponent();

            // inicjalizacja zmiennych
            this._name         = "";
            this._tooltip_show = false;
            this._patterns     = null;

            // TODO: Z ustawień!!!
            this._format_names = new string[] { "A4", "A5" };
            this._format_dims  = new int[,] { {210, 297}, {148, 210} };

            // wyświetlanie nazw aktualnych wzorów
            this.fillAvailablePatterns();

            // przetłumacz kontrolki
            this.translateForm();

            // wyświetlanie nazw dostępnych formatów
            foreach( string format_name in this._format_names )
                this.CBX_PaperFormat.Items.Add( format_name );

            this.TB_PatternName.Text = "";

			// zaznacz format: własny
			this.CBX_PaperFormat.SelectedIndex = 0;

            this.Icon = Program.GetIcon();
        }

        /// <summary>
        /// Pobiera nazwę wzoru.
        /// Nazwa zapisywana jest podczas tworzenia wzoru.
        /// Dzięki temu można użyć jej jeszcze potem po zamknięciu okna dialogowego.
        /// </summary>
		//* ============================================================================================================
		public string PatternName
		{
			get { return this._name; }
		}

#endregion

#region FUNKCJE PODSTAWOWE

        /// <summary>
        /// Funkcja uzupełnia kontrolkę wyboru kopiowanego wzoru.
        /// Dodaje wszystkie możliwe do skopiowania wzory - te które mają plik konfiguracyjny i są poprawne.
        /// Nazwy wzorów to nazwy folderów umieszczone w folderze patterns.
        /// </summary>
        /// 
        /// <seealso cref="NewPatternForm"/>
        /// <seealso cref="PatternEditor"/>
		//* ============================================================================================================
        public void fillAvailablePatterns()
        {
#		if DEBUG
			Program.LogMessage( "Uzupełnianie listy dostępnych wzorów." );
#		endif

            // wyczyść wszystkie dostępne wzory
            this.CBX_CopyFrom.Items.Clear();
            
            this.CBX_CopyFrom.Items.Add( Language.GetLine("NewPattern", "ComboBox", (int)LANGCODE.I05_CBX_DONTCOPY) );
            this.CBX_CopyFrom.SelectedIndex = 0;

            // pobierz wzory i uzupełnij kontrolkę
            var patterns = PatternEditor.GetPatterns();
            foreach( var pattern in patterns )
            {
                if( pattern.HasConfigFile && !pattern.Corrupted )
                    this.CBX_CopyFrom.Items.Add( pattern.Name );
            }
            this._patterns = patterns;

#       if DEBUG
		    Program.LogMessage( "Ilość dostępnych wzorów: " + this._patterns.Count + "." );
#		endif

            // włącz lub wyłącz wybór
            if( this.CBX_CopyFrom.Items.Count > 1 )
                this.CBX_CopyFrom.Enabled = true;
            else
                this.CBX_CopyFrom.Enabled = false;
        }

        /// <summary>
		/// Translator formularza.
		/// Funkcja tłumaczy wszystkie statyczne elementy programu.
		/// Wywoływana jest z konstruktora oraz podczas odświeżania ustawień językowych.
        /// Jej użycie nie powinno wykraczać poza dwa wyżej wymienione przypadki.
		/// </summary>
        /// 
        /// <seealso cref="NewPatternForm"/>
        /// <seealso cref="Language"/>
		//* ============================================================================================================
		protected void translateForm()
		{
#		if DEBUG
			Program.LogMessage( "Tłumaczenie kontrolek znajdujących się na formularzu." );
#		endif

            // napisy
            var values = Language.GetLines( "NewPattern", "Labels" );

            this.L_PatternName.Text = values[(int)LANGCODE.I05_LAB_PATTNAME];
            this.L_PatternCopy.Text = values[(int)LANGCODE.I05_LAB_PATTCOPY];
            this.L_PatternSize.Text = values[(int)LANGCODE.I05_LAB_PATTSIZE];
            this.L_PaperFormat.Text = values[(int)LANGCODE.I05_LAB_PAPERTYPE];

            // przyciski
            values = Language.GetLines( "NewPattern", "Buttons" );

            this.B_Create.Text = values[(int)LANGCODE.I05_BUT_CREATE];
            this.B_Cancel.Text = values[(int)LANGCODE.I05_BUT_CANCEL];

			// tytuł okna
			this.Text = Language.GetLine( "FormNames", (int)LANGCODE.GFN_NEWPATTERN );
		}

#endregion

#region KONTROLKI
        /// @cond EVENTS

        /// <summary>
        /// Akcja wykonywana po zmianie formatu papieru.
        /// Po zmianie zaznaczenia funkcja uzupełnia wartości kontrolek, zawierających wymiary papieru.
        /// Dodatkowo po uzupełnieniu kontrolek blokuje je, aby nie można było zmienić wartości.
        /// Dopiero po zmianie formatu na własny kontrolki zostają odblokowane.
        /// </summary>
        /// 
		/// <param name="sender">Obiekt wywołujący zdarzenie.</param>
		/// <param name="ev">Argumenty zdarzenia.</param>
		//* ============================================================================================================
        private void CBX_PaperFormat_SelectedIndexChanged( object sender, EventArgs ev )
        {
#       if DEBUG
		    Program.LogMessage( "Zmiana formatu papieru." );
#		endif

            // wybrano format papieru
            if( this.CBX_PaperFormat.SelectedIndex > 0 )
            {
                // uzupełnij wartości w polach numerycznych
                this.N_Width.Value  = this._format_dims[this.CBX_PaperFormat.SelectedIndex - 1, 0];
                this.N_Height.Value = this._format_dims[this.CBX_PaperFormat.SelectedIndex - 1, 1];

                // zablokuj pola
                this.N_Width.Enabled  = false;
                this.N_Height.Enabled = false;

                return;
            }

            // odblokuj pola numeryczne
            this.N_Width.Enabled  = true;
            this.N_Height.Enabled = true;
        }

		/// <summary>
		/// Akcja wywoływana po wciśnięciu klawisza przy aktywnej kontrolce nazwy wzoru.
        /// Funkcja sprawdza czy wpisany znak jest zgodny ze znakami, które można wpisać.
        /// W przypadku gdy znak nie pasuje, funkcja wyświetla dymek informacyjny oraz zapobiega
        /// wpisaniu znaku - przechwytuje zdarzenie i nie przekazuje go dalej.
		/// </summary>
        /// 
		/// <param name="sender">Obiekt wywołujący zdarzenie.</param>
		/// <param name="ev">Argumenty zdarzenia.</param>
		//* ============================================================================================================
		private void TB_PatternName_KeyPress( object sender, KeyPressEventArgs ev )
		{
            var lang_chars   = Language.GetLines( "Locale" );
			var locale_chars = lang_chars[(int)LANGCODE.GLO_BIGCHARS] + lang_chars[(int)LANGCODE.GLO_SMALLCHARS];
			var regex        = new Regex( @"^[0-9a-zA-Z" + locale_chars + @" \-+_#]+$" );

            // nie sprawdzaj gdy wciśnięto klawisz Backspace lub Ctrl
			if( ev.KeyChar == 8 || ModifierKeys == Keys.Control )
				return;

            // sprawdź poprawność znaków z regex
			if( !regex.IsMatch(ev.KeyChar.ToString()) )
			{
#           if DEBUG
		        Program.LogMessage( "Wpisano niepoprawny znak: " + ev.KeyChar + "." );
		        Program.LogMessage( "Znaki lokalne: <<( " + locale_chars + " )>>." );
#		    endif

                // pokaż dymek jeżeli wisano zły znak
				if( !this._tooltip_show )
				{
					this.TT_BadChars.Show
					(
                        Language.GetLine("NewPattern", "Messages", (int)LANGCODE.I05_MES_AVALCHARS),
						this.TB_PatternName,
						new Point( -20, this.TB_PatternName.Height + 2 )
					);
					this._tooltip_show = true;
				}

                // wydaj dźwięk
				ev.Handled = true;
				System.Media.SystemSounds.Beep.Play();
				
				return;
			}

            // ukryj dymek jeżeli wpisano poprawny znak przed samoczynnym schowaniem się dymku
			if( this._tooltip_show )
			{
#           if DEBUG
		        Program.LogMessage( "Wpisanie poprawnego znaku - chowanie dymku." );
#		    endif

				this.TT_BadChars.Hide( this.TB_PatternName );
				this._tooltip_show = false;
			}
		}

        /// <summary>
        /// Akcja wywoływana po zmianie kontrolki z nazwą wzoru na inną kontrolkę.
        /// Pozwala na schowanie wyświetlanego dymku z podpowiedzią.
        /// </summary>
        /// 
		/// <param name="sender">Obiekt wywołujący zdarzenie.</param>
		/// <param name="ev">Argumenty zdarzenia.</param>
		//* ============================================================================================================
		private void TB_PatternName_Leave( object sender, EventArgs ev )
		{
			if( this._tooltip_show )
			{
#           if DEBUG
		        Program.LogMessage( "Zmiana aktywnej kontrolki - chowanie dymku." );
#		    endif

				this.TT_BadChars.Hide( this.TB_PatternName );
				this._tooltip_show = false;
			}
		}

        /// <summary>
        /// Akcja wywoływana przy zmianie kopiowanego wzoru.
        /// Zmiana powoduje zmianę w parametrach "wymiary" i "format" oraz ich zablokowanie.
        /// Wzór aktualny nie może mieć innych parametrów w przypadku rozmiaru niż wzór kopiowany.
        /// </summary>
        /// 
		/// <param name="sender">Obiekt wywołujący zdarzenie.</param>
		/// <param name="ev">Argumenty zdarzenia.</param>
		//* ============================================================================================================
        private void CBX_CopyFrom_SelectedIndexChanged( object sender, EventArgs ev )
        {
            int selected = this.CBX_CopyFrom.SelectedIndex;

#       if DEBUG
		    Program.LogMessage( "Zmiana kopiowanego wzoru na: " + selected + "." );
#		endif

            // uaktywnij zablokowane wcześniej kontrolki
            if( selected == 0 )
            {
                this.CBX_PaperFormat.Enabled = true;
                this.CBX_PaperFormat_SelectedIndexChanged( null, null );

                return;
            }

            // pobierz rozmiar kopiowanego wzoru - pomniejsz o jeden, nazwy zaczynają się od pierwszej pozycji
            var size    = this._patterns[selected - 1].Size;
            int pattern = PatternEditor.DetectFormat( size.Width, size.Height );

            // ustaw wybrany format wzoru (+ 1, ponieważ formaty wzorów zaczynają się od pierwszej pozycji)
            if( pattern > -1 )
                this.CBX_PaperFormat.SelectedIndex = pattern + 1;
            else
            {
                // własny format
                this.CBX_PaperFormat.SelectedIndex = 0;

                this.N_Width.Value  = size.Width;
                this.N_Height.Value = size.Height;
            }

            this.CBX_PaperFormat.Enabled = false;
            this.N_Width.Enabled         = false;
            this.N_Height.Enabled        = false;
        }

#endregion

#region FORMULARZ I PASEK AKCJI

        /// <summary>
        /// Akcja wywoływana po kliknięciu w przycisk tworzenia wzoru.
        /// Sprawdza nazwę i wymiary wzoru oraz tworzy folder i plik konfiguracyjny wzoru.
        /// W przypadku błędów w parsowaniu formularza, funkcja zatrzymuje akcję przycisku.
        /// </summary>
        /// 
		/// <param name="sender">Obiekt wywołujący zdarzenie.</param>
		/// <param name="ev">Argumenty zdarzenia.</param>
		//* ============================================================================================================
        private void B_Create_Click( object sender, EventArgs ev )
        {
            // pobierz zmienne językowe
            var values   = Language.GetLines( "NewPattern", "Messages" );
            var msgtitle = Language.GetLine( "MessageNames", (int)LANGCODE.GMN_PATTCREATING );

			// wyczyść ze zbędnych spacji
			this.TB_PatternName.Text = this.TB_PatternName.Text.Trim();
			Regex regex = new Regex( @"[ ]{2,}", RegexOptions.None );
			this.TB_PatternName.Text = regex.Replace( this.TB_PatternName.Text, " " );

            // brak nazwy wzoru...
            if( this.TB_PatternName.Text == "" )
            {
                Program.LogWarning( values[(int)LANGCODE.I05_MES_NOPATTNAME], msgtitle, this );
                this.DialogResult = DialogResult.None;
                return;
            }

			// wzór o tej nazwie już istnieje
            if( Directory.Exists("patterns/" + this.TB_PatternName.Text) )
            {
                Program.LogWarning( values[(int)LANGCODE.I05_MES_PATTEXISTS], msgtitle, this );
                this.DialogResult = DialogResult.None;
                return;
            }

            // zerowe wymiary papieru...
            if( this.N_Height.Value == 0 || this.N_Width.Value == 0 )
            {
                Program.LogWarning( values[(int)LANGCODE.I05_MES_NOPATTSIZE], msgtitle, this );
                this.DialogResult = DialogResult.None;
                return;
            }

			try 
            {
                // klonowanie wzoru
                if( this.CBX_CopyFrom.SelectedIndex > 0 )
                {
                    // pobierz indeks wzoru
                    int pattern = this.CBX_CopyFrom.SelectedIndex - 1;

#               if DEBUG
		            Program.LogMessage( "Ilość wzorów: " + this._patterns.Count + ", kopiowany: " + pattern + "." );
		            Program.LogMessage( "Klonowanie wzoru: '" + this._patterns[pattern].Name + "'." );
		            Program.LogMessage( "Miejsce klonowania: '" + this.TB_PatternName.Text + "'." );
                    Program.IncreaseLogIndent();
#		        endif
                    PatternEditor.ClonePattern( this.TB_PatternName.Text, this._patterns[pattern].Name );
#               if DEBUG
                    Program.DecreaseLogIndent();
#               endif
                }
			    // tworzenie wzoru
                else
                {
#               if DEBUG
		            Program.LogMessage( "Tworzenie wzoru z podanych danych." );
#		        endif
                    PatternEditor.Create( this.TB_PatternName.Text, (short)this.N_Width.Value, (short)this.N_Height.Value );
                }
            }
			catch
			{
#           if DEBUG
                // zmniejsz wcięcie w przypadku klonowania wzoru
                if( this.CBX_CopyFrom.SelectedIndex > 0 )
                    Program.DecreaseLogIndent();
#           endif

				// brak uprawnień?
                Program.LogError( values[(int)LANGCODE.I05_MES_CANTCREATE], msgtitle, false, null, this );

                // usuń wzór, ale gdy wystąpi błąd nic nie rób
                try { PatternEditor.Delete( TB_PatternName.Text ); }
                catch { Program.LogMessage( "Nie można usunąć wzoru" ); }
                
                this.DialogResult = DialogResult.Cancel;
                return;
			}

			// zapisz aktualną nazwę szablonu
			this._name = this.TB_PatternName.Text;
        }

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

        /// <summary>
        /// Akcja wywoływana podczas zamykania formularza.
        /// Sprawdza czy można zamknąć - drugi sposób po ustawieniu DialogResult na None.
        /// Zrobione specjalnie dla Mono - na .NET jest wszystko ok.
        /// </summary>
        /// 
		/// <param name="sender">Obiekt wywołujący zdarzenie.</param>
		/// <param name="ev">Argumenty zdarzenia.</param>
		//* ============================================================================================================
        private void NewPatternForm_FormClosing( object sender, FormClosingEventArgs ev )
        {
            if( this.DialogResult == DialogResult.None )
            {
#           if DEBUG
		        Program.LogMessage( "Zamknięcie formularza zostało zatrzymane." );
#		    endif
                ev.Cancel = true;
            }
        }

        /// <summary>
        /// Akcja wywoływana przy zmianie pozycji okna.
        /// Pozwala na schowanie wyświetlanego dymku z podpowiedzią.
        /// </summary>
        /// 
		/// <param name="sender">Obiekt wywołujący zdarzenie.</param>
		/// <param name="ev">Argumenty zdarzenia.</param>
		//* ============================================================================================================
		private void NewPatternForm_Move( object sender, EventArgs ev )
		{
			if( this._tooltip_show )
			{
#           if DEBUG
		        Program.LogMessage( "Przesuwanie okna - chowanie dymku." );
#		    endif

				this.TT_BadChars.Hide( this.TB_PatternName );
				this._tooltip_show = false;
			}
        }

        /// @endcond
#endregion
    }
}
