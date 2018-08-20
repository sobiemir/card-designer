///
/// $i07 UpdateForm.cs
/// 
/// Okno aktualizacji programu.
/// Wyświetla listę zmian w poszczególnych wersjach oraz wersje programu aktualnego i aktualizowanego.
/// Pozwala zarówno na pobranie i aktualizacje aplikacji jak i na kompresję plików oraz utworzenie aktualizacji.
/// Aktualizacje zapisywane do pliku "update.cbd" po instalacji przenoszone są do folderu updates z numerkiem wersji.
/// Pliki przed aktualizacją wypakowywane są do folderu temp, który po instalacji jest usuwany.
/// 
/// Autor: Kamil Biały
/// Od wersji: 0.6.x
/// Ostatnia zmiana: 2016-11-14
/// 
/// CHANGELOG:
/// [27.06.2015] Wersja początkowa - pobieranie i kompresja aktualizacji.
/// [14.11.2016] Nowa wersja - zmiana koncepcji włączania kompresji, wcześniej trzeba było przytrzymać CTRL+ALT+SHIFT
///              klikając na przycisk, teraz trzeba wcześniej wpisać słowo kompresja. Przebudowana struktura formularza
///              i wydzielenie funkcji do osobnej klasy. Odświeżenie wyglądu formularza.
///

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Net;
using System.IO;
using System.Diagnostics;
using CDesigner.Utils;

namespace CDesigner.Forms
{
    /// 
    /// <summary>
    /// Klasa tworząca okno do aktualizacji programu.
    /// Wyświetla aktualną wersję i informacje o tym, czy dostępna jest aktualizacja.
    /// Pobiera aktualizację z serwera i przygotowuje do aktualizacji.
    /// W odpowiednim polu wyświetla również listę zmian w poszczególnych wersjach.
    /// Możliwa jest kompresja danych po podaniu odpowiedniej komendy.
    /// Aby włączyć kompresję, należy wpisać ciąg znaków: kompresja, wyświetli się okno informacyjne
    /// oraz przycisk zmieni nazwę z aktualizuj na kompresuj.
    /// </summary>
    /// 
	public partial class UpdateForm : Form
	{
#region ZMIENNE

        /// <summary>Czy założona blokada wykonywania akcji kontrolek na formularzu.</summary>
        private bool _locked;

        /// <summary>Ciąg znaków wpisywany z klawiatury - włączanie kompresji.</summary>
        private string _bonus;

        /// <summary>Czy włączony tryb kompresji?</summary>
        private bool _comprmode;

        /// <summary>Czy wystąpił błąd podczas kompresji lub aktualizacji?</summary>
        private bool _haserror;

#endregion

#region KONSTRUKTOR

        /// <summary>
        /// Konstruktor klasy.
		/// Tworzy okno, ustawia ikonkę programu i tłumaczy wszystkie napisy.
        /// </summary>
		//* ============================================================================================================
		public UpdateForm()
		{
			this.InitializeComponent();
            this.translateForm();
            
            this._locked    = false;
            this._bonus     = "";
            this._comprmode = false;
            this._haserror  = false;

            this.PB_AppLogo.Image = Program.GetBitmap( BITMAPCODE.CDesigner128 );

            this.Icon = Program.GetIcon();
		}

#endregion

#region FUNKCJE PODSTAWOWE

        /// <summary>
		/// Translator formularza.
		/// Funkcja tłumaczy wszystkie statyczne elementy programu.
		/// Wywoływana jest z konstruktora oraz podczas odświeżania ustawień językowych.
        /// Jej użycie nie powinno wykraczać poza dwa wyżej wymienione przypadki.
		/// </summary>
        /// 
        /// <seealso cref="UpdateForm"/>
        /// <seealso cref="Language"/>
		//* ============================================================================================================
        public void translateForm()
        {
            // etykiety
            var values = Language.GetLines( "Update", "Labels" );

            // aktualna wersja
            this.L_CurVersion.Text = values[(int)LANGCODE.I07_LAB_APPVERSION] + " " + Program.VERSION + ", " +
                Program.BUILD_DATE.ToString( "dd.MM.yyyy" ) + ".";
            this.GB_ChangeLog.Text = " " + values[(int)LANGCODE.I07_LAB_CHANGES] + " ";

            if( UpdateApp.Available )
            {
                // dane aktualizacji
                this.L_UpdateAvailable.Text = values[(int)LANGCODE.I07_LAB_AVAILABLE];
                this.L_NewVersion.Text      = values[(int)LANGCODE.I07_LAB_VERSIONUP] + " " + UpdateApp.Version + ", " +
                    UpdateApp.BuildDate.ToString( "dd.MM.yyyy" ) + ".";
            }
            else
            {
                // brak aktualizacji
                this.L_UpdateAvailable.Text = values[(int)LANGCODE.I07_LAB_UPTODATE];
                this.L_NewVersion.Text      = "";
            }

            // przyciski
            values = Language.GetLines( "Update", "Buttons" );
            this.B_Close.Text  = values[(int)LANGCODE.I07_BUT_CLOSE];
            this.B_Update.Text = values[(int)LANGCODE.I07_BUT_UPDATE];
            
            // nazwa okna
            this.Text = Language.GetLine( "FormNames", (int)LANGCODE.GFN_PROGRAMUPDATE );
        }

        /// <summary>
		/// Odświeżanie danych w oknie.
        /// Okno tworzone wcześniej wymaga odświeżenia.
        /// Aby go nie zwalniać z pamięci, po zamknięciu podczas otwierania warto wywołać tą funkcję.
        /// Odświeża wszystkie dynamiczne dane wyświetlane na formularzu wraz z tłumaczeniami fraz.
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
            // nie pobrano wcześniej informacji o zmianach i wersji?
            // to pobierz teraz
            if( UpdateApp.ChangeLog == null )
                try
                {
                    // pobierz informacje o wersji
                    if( !UpdateApp.CheckAvailability() )
                    {
                        this.checkException();
                        return;
                    }
                    // pobierz zmiany w kolejnych wersjach
                    if( !UpdateApp.GetChangeLog() )
                    {
                        this.checkException();
                        return;
                    }
                }
                catch( Exception ex )
                {
                    // błąd podczas pobierania zmian...
                    Program.LogError
                    (
                        Language.GetLine( "Update", "Messages", (int)LANGCODE.I07_MSG_ADMINHELP ),
                        Language.GetLine( "FormNames", (int)LANGCODE.GFN_PROGRAMUPDATE ),
                        false,
                        ex
                    );
                    return;
                }

            // tłumacz etykiety
            this.translateForm();

            // zmiany
            this.RTB_Changes.Text = UpdateApp.ChangeLog;

            // zablokuj lub odblokuj przycisk aktualizacji
            if( UpdateApp.Available )
                this.B_Update.Enabled = true;
            else
                this.B_Update.Enabled = false;
        }
        
        /// <summary>
		/// Odświeżanie danych w oknie.
        /// Okno tworzone wcześniej wymaga odświeżenia.
        /// Aby go nie zwalniać z pamięci, po zamknięciu podczas otwierania warto wywołać tą funkcję.
        /// Odświeża wszystkie dynamiczne dane wyświetlane na formularzu wraz z tłumaczeniami fraz.
        /// Funkcja w odróżnieniu od funkcji refreshForm po wywołaniu otwiera okno.
		/// </summary>
        /// 
        /// <param name="parent">Rodzic do którego przypisany będzie komunikat.</param>
        /// <param name="dialog">Czy wyświetlić jako okno modalne?</param>
        /// 
        /// <returns>Wartość zwracana przez okno modalne lub DialogResult.None.</returns>
        /// 
        /// <seealso cref="refreshForm"/>
        /// <seealso cref="Language"/>
		//* ============================================================================================================
        public DialogResult refreshAndOpen( Form parent = null, bool modal = true )
        {
            this.refreshForm();
            return Program.OpenForm( this, parent, modal );
        }

        /// <summary>
        /// Funkcja sprawdza czy podczas pobierania danych nie wystąpił błąd dostępu do serwera.
        /// Podczas wystąpienia innego błędu, funkcja rzuca wyjątek który go dotyczy.
        /// Przy wyrzuceniu błędu serwera wyświetlany jest odpowiedni komunikat.
        /// </summary>
        /// 
        /// <seealso cref="refreshForm"/>
        /// <seealso cref="refreshAndOpen"/>
		//* ============================================================================================================
        private void checkException()
        {
            if( UpdateApp.ConnectionError.Response == null )
            {
                Program.LogError
                (
                    Language.GetLine( "Update", "Messages", (int)LANGCODE.I07_MSG_NOCONNECT ),
                    Language.GetLine( "FormNames", (int)LANGCODE.GFN_PROGRAMUPDATE ),
                    false,
                    UpdateApp.ConnectionError
                );
            }
            else
                throw UpdateApp.ConnectionError;
        }

        /// <summary>
        /// Blokowanie i odblokowywanie kontrolek.
        /// Podczas blokady kontrolek, wszystkie akcje są zbywane, dzięki czemu program staje się "zombie".
        /// Przydatne gdy po kliknięciu wykonuje się ważne zadanie, które nie może być przerwane.
        /// </summary>
        /// 
        /// <param name="value">Blokować czy nie?</param>
		//* ============================================================================================================
        private void setLock( bool value )
        {
            // przy ustawianiu blokady, ustaw kursor na kursor oczekiwania
            if( value == true )
            {
                this._locked = true;

                this.Cursor = Cursor.Current = Cursors.WaitCursor;
                Application.UseWaitCursor = true;

                this.B_Update.Enabled = false;
            }
            // w przeciwnym wypadku ustaw kurson na domyślny
            else
            {
                this._locked = false;

                this.Cursor = Cursor.Current = Cursors.Default;
                Application.UseWaitCursor = false;

                this.B_Update.Enabled = true;
            }
        }

#endregion

#region POBIERANIE, KOMPRESJA I DEKOMPRESJA
        /// @cond EVENTS

        /// <summary>
        /// Akcja wywoływana po zmianie postępu aktualizacji.
        /// Zmienia aktualną pozycję wskaźnika na pasku postępu.
        /// </summary>
		/// 
		/// <param name="sender">Obiekt wywołujący zdarzenie.</param>
		/// <param name="ev">Argumenty zdarzenia.</param>
		//* ============================================================================================================
		private void DownloadProgress( object sender, DownloadProgressChangedEventArgs ev )
		{
			this.PRB_Update.Value = ev.ProgressPercentage;
		}

        /// <summary>
        /// Akcja wywoływana po zakończeniu pobierania pliku aktualizacji.
        /// Wywoływana bezpośrednio w trybie kompresji plików programu.
        /// W zależności od trybu, funkcja uruchamia dekompresje lub kompresje plików w osobnym wątku.
        /// </summary>
		/// 
		/// <param name="sender">Obiekt wywołujący zdarzenie.</param>
		/// <param name="ev">Argumenty zdarzenia.</param>
		//* ============================================================================================================
		private void DownloadCompleted( object sender, AsyncCompletedEventArgs ev )
		{
			// błąd podczas pobierania aktualizacji
			if( ev != null && ev.Error != null )
			{
                this.setLock( false );

                // wyświetl błąd
                Program.LogError
                (
                    Language.GetLine( "Update", "Messages", (int)LANGCODE.I07_MSG_ERRUPDATE ),
                    Language.GetLine( "FormNames", (int)LANGCODE.GFN_PROGRAMUPDATE ),
                    false,
                    ev.Error
                );
				return;
			}

			if( this._comprmode )
			{
				// utwórz aktualizacje
				this.BW_Update.DoWork -= this.DecompressUpdate;
				this.BW_Update.DoWork -= this.CompressUpdate;
				this.BW_Update.DoWork += this.CompressUpdate;

				this.BW_Update.ProgressChanged -= this.DecompressionProgress;
				this.BW_Update.ProgressChanged -= this.CompressionProgress;
				this.BW_Update.ProgressChanged += this.CompressionProgress;

				this.BW_Update.RunWorkerAsync();
			}
			else
			{
				// wypakuj aktualizacje
				this.PRB_Update.Value = 0;

				this.BW_Update.DoWork -= this.DecompressUpdate;
				this.BW_Update.DoWork -= this.CompressUpdate;
				this.BW_Update.DoWork += this.DecompressUpdate;

				this.BW_Update.ProgressChanged -= this.DecompressionProgress;
				this.BW_Update.ProgressChanged -= this.CompressionProgress;
				this.BW_Update.ProgressChanged += this.DecompressionProgress;

				this.BW_Update.RunWorkerAsync();
			}
		}

		/// <summary>
		/// Akcja wywoływana podczas kompresji danych.
        /// Uruchamiana jest już w osobnym procesie, kompresuje dane i zapisuje je do pliku.
        /// Nazwa pliku jest numerem aktualnej wersji programu.
		/// </summary>
		/// 
		/// <param name="sender">Obiekt wywołujący zdarzenie.</param>
		/// <param name="ev">Argumenty zdarzenia.</param>
		//* ============================================================================================================
		private void CompressUpdate( object sender, DoWorkEventArgs ev )
		{
#       if DEBUG
            Program.LogMessage( "Uruchomiono kompresję plików programu." );
#       endif

			try
			{
				var files = new List<string>();

				// dodaj plik z listą skompresowanych plików
				files.Add( "./update.lst" );
                files.AddRange( UpdateApp.ListChanges() );

				// stwórz kopie zapasową [cbd - compressed backup data]
				CBackupData.Compress( files, "./update/v" + Program.VERSION + ".cbd" );

				this.BW_Update.ReportProgress( 100, (object)2 );
			}
			catch( Exception ex )
			{
				// wyświetl komunikat o błędach
				Program.LogError
				(
                    Language.GetLine( "Update", "Messages", (int)LANGCODE.I07_MSG_ERRCRESS ),
                    Language.GetLine( "FormNames", (int)LANGCODE.GFN_PROGRAMUPDATE ),
                    false,
                    ex
				);

				// błąd... nie przetwarzaj dalej
				this.BW_Update.ReportProgress( 100, (object)1 );
                this._haserror = true;
			}
		}

        /// <summary>
        /// Akcja wykonywana podczas postępu w kompresji danych.
        /// Używana do aktualizacji wskaźnika postępu na kontrolce.
        /// Po zakończeniu wyświetlany jest komunikat o sukcesie.
        /// </summary>
		/// 
		/// <param name="sender">Obiekt wywołujący zdarzenie.</param>
		/// <param name="ev">Argumenty zdarzenia.</param>
		//* ============================================================================================================
		private void CompressionProgress( object sender, ProgressChangedEventArgs ev )
		{
			// aktualizuj status
			this.PRB_Update.Value = ev.ProgressPercentage;

			// koniec kompresji, rozpocznij dekompresje
			if( ev.ProgressPercentage == 100 )
			{
				// błąd, przerwij aktualizacje...
				if( (int)ev.UserState == 1 )
				{
#               if DEBUG
                    Program.LogMessage( "Podczas kompresji wystąpił błąd." );
#               endif
                    this.setLock( false );
				}
                // kompresja zakończona sukcesem
				else if( (int)ev.UserState == 2 )
				{
                    Program.LogInfo
                    (
                        Language.GetLine( "Update", "Messages", (int)LANGCODE.I07_MSG_SUCCESSZIP ),
                        Language.GetLine( "FormNames", (int)LANGCODE.GFN_PROGRAMUPDATE ),
                        this
                    );
                    this.setLock( false );
				}
			}
		}

        /// <summary>
        /// Akcja wywoływana podczas wypakowywania plików.
        /// Uruchamiana jest już w osobnym procesie, wypakowuje dane z pliku aktualizacji.
        /// Wszystkie pliki zapisane są w pliku update.lst dołączanym na samym początku.
        /// </summary>
		/// 
		/// <param name="sender">Obiekt wywołujący zdarzenie.</param>
		/// <param name="ev">Argumenty zdarzenia.</param>
		//* ============================================================================================================
		private void DecompressUpdate( object sender, DoWorkEventArgs ev )
		{
#       if DEBUG
			Program.LogMessage( "Wypakowywanie aktualizacji: './update.cbd'." );
#       endif
			try
			{
				// wypakuj dane do folderu "./temp/"
				CBackupData backup = new CBackupData( this.BW_Update );
				backup.DecompressUpdate( "./update.cbd", "./temp/" );
			}
			catch( Exception ex )
			{
				// wyświetl komunikat o błędach
				Program.LogError
				(
                    Language.GetLine( "Update", "Messages", (int)LANGCODE.I07_MSG_ERRDECRESS ),
                    Language.GetLine( "FormNames", (int)LANGCODE.GFN_PROGRAMUPDATE ),
                    false,
                    ex
				);

                // błąd... nie przetwarzaj dalej
				this.BW_Update.ReportProgress( 100, 2 );
                this._haserror = true;
			}
		}

        /// <summary>
        /// Akcja wywoływana podczas postępu w rozpakowywaniu danych.
        /// Używana do aktualizacji wskaźnika postępu na kontrolce.
        /// Po zakończeniu wyświetlane jest pytanie o to czy program ma zainstalować aktualizację.
        /// Aktualizacja programu wiąże się z jego ponownym uruchomieniem.
        /// Wszystkie aktualizacje przenoszone są do folderu z kopiami zapasowymi (updates) z nazwą jako numerem wersji.
        /// </summary>
		/// 
		/// <param name="sender">Obiekt wywołujący zdarzenie.</param>
		/// <param name="ev">Argumenty zdarzenia.</param>
		//* ============================================================================================================
		private void DecompressionProgress( object sender, ProgressChangedEventArgs ev )
		{
			// aktualizuj status
			this.PRB_Update.Value = ev.ProgressPercentage;

			// błąd dekompresji
			if( (int)ev.UserState == 2 )
			{
#           if DEBUG
				Program.LogMessage( "Wypakowywanie aktualizacji zakończone błędem." );
#           endif
                this.setLock( false );
			}
			// koniec dekompresji, skopiuj pliki
			else if( (int)ev.UserState == 1 )
			{
                this.setLock( false );

				// wyświetl komunikat o błędach
				var result = Program.LogQuestion
				(
                    Language.GetLine( "Update", "Messages", (int)LANGCODE.I07_MSG_INSTALLUP ),
                    Language.GetLine( "FormNames", (int)LANGCODE.GFN_PROGRAMUPDATE ),
                    true,
                    this
				);

				// przenieś plik do folderu z kopiami zapasowymi aktualizacji
				if( File.Exists("./update/v" + UpdateApp.Version + ".cbd") )
					File.Delete( "./update/v" + UpdateApp.Version + ".cbd" );

                // utwórz folder dla aktualizacji
                if( !Directory.Exists("./update") )
                    Directory.CreateDirectory( "./update" );

				File.Move( "./update.cbd", "./update/v" + UpdateApp.Version + ".cbd" );

				// instalacja aktualizacji
				if( result == DialogResult.Yes )
				{
#               if DEBUG
					Program.LogMessage( "Uruchamianie nowego procesu dla programu CDRestore." );
#               endif

					// uruchomienie programu CDRestore
					Process process = new Process();
					process.StartInfo.FileName = "cdrestore.exe";
					process.StartInfo.Arguments = "-i -w";
					process.Start();

#               if DEBUG
                    Program.LogMessage( "Zamykanie aplikacji." );
#               endif

					// zamykanie aplikacji
					Program.ExitApplication( true );
				}
			}
		}

#endregion

#region FORMULARZ

        /// <summary>
        /// Akcja wywoływana podczas zamykania okna.
        /// Zapobiega zamknięciu okna podczas aktualizacji programu.
        /// Po co zamykać okno w trakcie aktualizacji, skoro w tym oknie jest pasek postępu?
        /// </summary>
		/// 
		/// <param name="sender">Obiekt wywołujący zdarzenie.</param>
		/// <param name="ev">Argumenty zdarzenia.</param>
		//* ============================================================================================================
		private void Update_FormClosing( object sender, FormClosingEventArgs ev )
		{
			// nie zamykaj podczas aktualizacji
			if( this._locked )
			{
#           if DEBUG
				Program.LogMessage( "Próba zamknięcia okna podczas aktualizacji programu." );
#           endif

                if( !this._haserror )
                    Program.LogInfo
                    (
                        Language.GetLine( "Update", "Messages", (int)LANGCODE.I07_MSG_CLOSEONUP ),
                        Language.GetLine( "FormNames", (int)LANGCODE.GFN_PROGRAMUPDATE ),
                        this
                    );

                this._haserror = false;
				ev.Cancel = true;

				return;
			}
		}

        /// <summary>
        /// Funkcja przetwarza wciśnięte klawisze.
        /// Podczas przetwarzania sprawdzane jest, czy użytkownik wpisał odpowiednie polecenie.
        /// W tym momencie dostępne jest tylko jedno polecenie: kompresja, po którym funkcja wprowadza program w stan
        /// kompresji, dzięki czemu wszystkie pliki programu są kompresowane - tworzona jest nowa aktualizacja programu.
        /// </summary>
        /// 
        /// <param name="msg">Przechwycone zdarzenie wciśnięcia klawisza.</param>
        /// <param name="keys">Informacje o wciśniętych klawiszach.</param>
        /// 
        /// <returns>Czy klawisz został przechwycony - jeżeli tak, zapobiega dalszemu przetwarzaniu klawisza.</returns>
		//* ============================================================================================================
		protected override bool ProcessCmdKey( ref Message msg, Keys keydata )
        {
            if( this._locked )
                return false;

            var extrastring = "kompresja";

            // dodaj znak do ciągu
            this._bonus += keydata.ToString();
            this._bonus  = this._bonus.ToLower();

            // sprawdź poprawność
            bool correct = true;
            switch( this._bonus.Length )
            {
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

            // jeżeli ciąg znaków został wpisany poprawnie, przełącz tryb
            if( correct && this._bonus.Length == extrastring.Length )
            {
                this._comprmode = !this._comprmode;
#           if DEBUG
                Program.LogMessage( "Wpisano poprawny ciąg znaków: " + this._bonus + "." );
#           endif

                // zmień tekst na przycisku
                if( this._comprmode )
                    this.B_Update.Text = Language.GetLine( "Update", "Buttons", (int)LANGCODE.I07_BUT_COMPRESS );
                else
                    this.B_Update.Text = Language.GetLine( "Update", "Buttons", (int)LANGCODE.I07_BUT_UPDATE );

                // wyświetl informacje o włączonym trybie
                Program.LogInfo
                (
                    this._comprmode
                        ? Language.GetLine( "Update", "Messages", (int)LANGCODE.I07_MSG_ACTIVEZIP )
                        : Language.GetLine( "Update", "Messages", (int)LANGCODE.I07_MSG_NACTIVEZIP ),
                    Language.GetLine( "FormNames", (int)LANGCODE.GFN_PROGRAMUPDATE ),
                    this
                );
                this._bonus = "";
            }
            // jeżeli nie, resetuj ciąg znaków
            else if( !correct || this._bonus.Length > extrastring.Length )
            {
#           if DEBUG
                Program.LogMessage( "Niepoprawny układ: " + this._bonus + "." );
#           endif
                this._bonus = "";
            }
            return false;
        }

#endregion

#region PASEK STATUSU

        /// <summary>
        /// Akcja wywoływana po kliknięciu na przycisk kompresji plików lub aktualizacji programu.
        /// Jest to jeden i ten sam przycisk, zmieniający zdarzenie i treść po wpisaniu komendy: kompresja.
        /// Może wywołać kompresje plików programu dla tworzenia aktualizacji lub rozpocząć pobieranie aktualizacji.
        /// Po kliknięciu blokuje okno przed zamknięciem do zakończenia aktualizacji lub kompresji lub do czasu
        /// wystąpienia błędu w podanych akcjach.
        /// </summary>
		/// 
		/// <param name="sender">Obiekt wywołujący zdarzenie.</param>
		/// <param name="ev">Argumenty zdarzenia.</param>
		//* ============================================================================================================
		private void B_Update_Click( object sender, EventArgs ev )
		{
            if( this._locked )
                return;

            // ikonka oczekiwania na zakończenie pobierania i rozpakowywania danych
            this.setLock( true );

            try
            {
                // tryb kompresji, nie pobieraj, przejdź od razu do kolejnego etapu
                if( this._comprmode )
                {
                    this.DownloadCompleted( null, new AsyncCompletedEventArgs(null, false, 0) );
                    return;
                }

			    // aktualizacja nie została jeszcze zainstalowana, nie pobieraj ponownie, zainstaluj tą
			    if( Directory.Exists("./temp/") )
			    {
			    }
			    // plik został pobrany wcześniej, nie pobieraj ponownie, rozpakuj ten
			    if( File.Exists("./update.cbd") )
			    {
			    }

                // pobierz aktualizacje
                using( var client = new WebClient() )
                {
                    client.DownloadProgressChanged -= this.DownloadProgress;
                    client.DownloadProgressChanged += this.DownloadProgress;
                    client.DownloadFileCompleted   -= this.DownloadCompleted;
                    client.DownloadFileCompleted   += this.DownloadCompleted;
                    
                    client.DownloadFileAsync( new Uri(UpdateApp.DownloadLink()), "./update.cbd" );
                }
            }
            catch( Exception ex )
            {
                // zmień kursor spowrotem na domyślny
                this.setLock( false );

                // wyświetl błąd
                Program.LogError
                (
                    Language.GetLine( "Update", "Messages", (int)LANGCODE.I07_MSG_ERRUPDATE ),
                    Language.GetLine( "FormNames", (int)LANGCODE.GFN_PROGRAMUPDATE ),
                    false,
                    ex
                );
            }
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

        /// @endcond
#endregion
    }
}