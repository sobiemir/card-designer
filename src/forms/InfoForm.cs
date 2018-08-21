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
/// Ostatnia zmiana: 2016-11-13
/// 
/// CHANGELOG:
/// [30.05.2015] Wersja początkowa.
/// [07.06.2015] Zmiana układu kontrolek w oknie.
/// [13.11.2016] Zmiana układu kontrolek i tłumaczenia, dodanie informacji o rejestracji.
///              Funkcje do odświeżania formularza. Usunięcie wersji - wersja dostępna w klasie Program.
///

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
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
#region ZMIENNE

        /// <summary>Przechwytywanie ciągu znaków po którym wyświetlona zostanie ukryta zawartość.</summary>
        private string _bonus;

#endregion

#region KONSTRUKTORY

        /// <summary>
        /// Konstruktor klasy.
		/// Tworzy okno, ustawia ikonkę programu i tłumaczy wszystkie napisy.
        /// </summary>
		//* ============================================================================================================
		public InfoForm()
		{
			this.InitializeComponent();

            this._bonus = "";

            this.PB_AppLogo.Image      = Program.GetBitmap( BITMAPCODE.CDESIGNER256 );
            this.PB_RegisterLogo.Image = Program.GetBitmap( BITMAPCODE.REGISTERLOGO );

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
#       if DEBUG
			Program.LogMessage( "Odświeżanie wszystkich potrzebnych danych." );
#       endif

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
#       if DEBUG
			Program.LogMessage( "Tłumaczenie kontrolek znajdujących się na formularzu." );
#       endif
            // przycisk
            this.B_Close.Text = Language.GetLine( "Info", "Buttons", (int)LANGCODE.I06_BUT_CLOSE );
            
#       if DEBUG
			Program.LogMessage( "Uzupełnianie informacji o rejestracji programu." );
#       endif

            // informacje o rejestracji
            this.L_RegisterFor.Text  = values[(int)LANGCODE.I06_LAB_REGFOR];
            this.L_SerialNumber.Text = values[(int)LANGCODE.I06_LAB_REGNUMBER] + " 0000-0000-0000-0000-0000";
            this.L_ExpireDate.Text   = values[(int)LANGCODE.I06_LAB_EXPIREDATE] + " " + values[(int)LANGCODE.I06_LAB_EXPIRNEVER];

            // wersja aplikacji
			this.L_Version.Text     = "v" + Program.VERSION + " (CDesigner :: " + Program.CODE_NAME + ")";
            this.L_AuthorApp.Text   = values[(int)LANGCODE.I06_LAB_AUTHOR] + " Kamil Biały";
            this.L_ReleaseDate.Text = values[(int)LANGCODE.I06_LAB_COMPILDATE] + " " + Program.BUILD_DATE.ToString("dd.MM.yyyy");

            // grupy
            this.GB_Additional.Text = " " + values[(int)LANGCODE.I06_LAB_ABOUTCOPY] + " ";
            
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

        /// <summary>
        /// Funkcja przetwarza wciśnięte klawisze.
        /// Dzięki temu sprawdza czy wpisane zostało polecenie klucz.
        /// Po wpisaniu polecenia pokazuje się na razie tylko informacja z tekstem o koniu.
        /// </summary>
        /// 
        /// <param name="msg">Przechwycone zdarzenie wciśnięcia klawisza.</param>
        /// <param name="keys">Informacje o wciśniętych klawiszach.</param>
        /// 
        /// <returns>Informacja o tym czy klawisz został przechwycony.</returns>
		//* ============================================================================================================
		protected override bool ProcessCmdKey( ref Message msg, Keys keydata )
        {
            var extrastring = "copoczniem";

            // dodaj znak do ciągu
            this._bonus += keydata.ToString();
            this._bonus  = this._bonus.ToLower();

            // sprawdź poprawność
            bool correct = true;
            switch( this._bonus.Length )
            {
                case 10: correct = extrastring[9] == this._bonus[9] ? correct : false; goto case 9;
                case 9 : correct = extrastring[8] == this._bonus[8] ? correct : false; goto case 8;
                case 8 : correct = extrastring[7] == this._bonus[7] ? correct : false; goto case 7;
                case 7 : correct = extrastring[6] == this._bonus[6] ? correct : false; goto case 6;
                case 6 : correct = extrastring[5] == this._bonus[5] ? correct : false; goto case 5;
                case 5 : correct = extrastring[4] == this._bonus[4] ? correct : false; goto case 4;
                case 4 : correct = extrastring[3] == this._bonus[3] ? correct : false; goto case 3;
                case 3 : correct = extrastring[2] == this._bonus[2] ? correct : false; goto case 2;
                case 2 : correct = extrastring[1] == this._bonus[1] ? correct : false; goto case 1;
                case 1 : correct = extrastring[0] == this._bonus[0] ? correct : false;
                break;
            }

            // jeżeli ciąg znaków został wpisany poprawnie, wyświetl ukrytą zawartość
            if( correct && this._bonus.Length == extrastring.Length )
            {
#           if DEBUG
                Program.LogMessage( "Wpisano poprawny ciąg znaków: " + this._bonus + "." );
#           endif
                Program.LogInfo( "Ja konia zamówiłem.\nWy konia nie chcecie.\nCo ja mu teraz powiem?\nKoniu nie?", "Informacja", this );
                this._bonus = "";
            }
            else if( !correct || this._bonus.Length > extrastring.Length )
            {
#           if DEBUG
                Program.LogMessage( "Niepoprawny układ: " + this._bonus + "." );
#           endif
                this._bonus = "";
            }
            return false;
        }

        /// @endcond
#endregion
    }
}
