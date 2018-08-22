///
/// $i01 MainForm.cs (I01)
/// 
/// Plik zawierający klasę tworzącą okno główne.
/// Okno główne podzielone jest na 3 sekcje: Lista wzorów, Edytor i Wydruk (Generator).
/// Każda z tych sekcji jest odosobniona (dostęp do jednej powoduje ukrycie drugiej).
/// Całość zrobiona została w ten sposób, aby przyspieszyć dostęp do poszczególnych sekcji
/// oraz zmniejszyć ilość wyświetlanych formularzy na raz, razem z możliwością szybkiego przełączania się między nimi.
/// Plik jest największym spośród wszystkich w programie, ponieważ zawiera kod wszystkich 3 sekcji, które można
/// traktować jako odosobnione formularze.
/// Formularz główny wywołuje pozostałe formularze dostępne w programie.
/// 
/// Autor: Kamil Biały
/// Od wersji: 0.1.x
/// Ostatnia zmiana: 2016-01-07
/// 
/// CHANGELOG:
/// [29.03.2015] Pierwsza wersja formularza.
/// [05.05.2015] Skróty klawiaturowe do przechodzenia pomiędzy stronami wzoru, odświeżanie listy projektów,
///              blokowanie i odblokowywanie kontrolek w edytorze w zależności od zaznaczonego pola, dodawanie stron,
///              podpięcie akcji pod menu kontekstowe, wczytywanie danych do podlgądu (przejście na generator),
///              podpięcie akcji pod nieużywane przyciski w edytorze, dwuklik do edycji wzoru i inne zmiany
///              istotne bardziej z punktu widzenia kodu a nie aplikacji.
/// [10.05.2015] Własna pozycja tekstu względem kontrolki, zmiana organizacji wczytywania danych wzoru,
///              wyświetlanie wierszy z zaznaczonymi kolumnami w generatorze, prawidłowy zapis wzoru do pliku,
///              rysowanie podglądu w generatorze.
/// [22.05.2015] Szczegóły pola w edytorze wzorów, drobne zmiany w wyglądzie, blokada ponownego generowania wzoru
///              w przypadku gdy jest on otwarty, wartości w milimetrach podawane w liczbach zmiennoprzecinkowych,
///              generowanie PDF do pliku test.pdf w folderze programu.
/// [01.06.2015] Menu ustawień ze skrótem do ustawień (CTRL+S), środkowanie wzoru w edytorze i podglądu wzoru w liście,
///              ustawienia strony w edytorze wzorów, zmiana ogranizacji układu kontrolek z tabeli na panel przesuwny,
///              możliwość przesuwania pól ruchem myszy z wciśniętym przyciskiem CTRL.
/// [07.06.2015] Początki tworzenia logów podczas działania programu w trybie DEBUG, tworzenie folderu dla wzorów gdy
///              nie istnieje, lista ostatnio otwieranych plików, ikona programu, dodatkowe ikonki w menu, dodawanie
///              kratki (#) przed numeracją kolorów w HEX.
/// [09.06.2015] Wyświetlanie popupu w przypadku błędów, skanowanie pod kątem błędów w generatorze podczas wyświetlania
///              wzorów (czy nazwa nie jest za długa) - wizualnie praktycznie to samo co wersja 0.5.0.
/// [29.06.2015] Otwieranie formularza aktualizacji programu (sprawdzanie połączenia z internetem), wczytywanie ikony
///              z pliku zamiast ze źródeł programu (mniejszy rozmiar pliku wyjściowego), połączenie 0.5.0 i 0.5.1.
/// [28.07.2015] Wyświetlanie formularza łączenia kolumn i rysowanie paska oddzielającego treść od pasku stanu i menu.
/// [22.08.2016] Osobne wczytywanie danych do aplikacji, zmiany w otwieraniu edytora kolumn.
/// [17.11.2016] Zmiana koncepcji aktualizacji poprzez sprawdzanie ich w tle a nie podczas otwierania formularza,
///              otwieranie formularza edycji wierszy.
/// [04.12.2016] Rozpoczęcie tłumaczenia formularza, formatowanie kodu, komentarze, wyświetlenie języków programu w menu,
///              poprawki w wyświetlaniu elementów, usunięcie statusu z paska informacji, szczegóły wzoru, import
///              i eksport pojedynczego wzoru i eksport wszystkich dostępnych.
/// [24.12.2016] Reorganizacja kodu, podzielenie na regiony, zakończenie tłumaczenia formularza, komentarze, wczytywanie
///              bezpośrednie danych do wzoru, zapis ustawień po zmianie języka, włączenie powiększania wzoru w edytorze
///              i generatorze (był wyłączony od pewnego czasu).
/// [26.12.2016] Domyślny obraz wyświetlany gdy brak podglądu, środkowanie strony edytora po kliknięciu w edycje,
///              podgląd nie wyświetlał wierszy po kolejnym wczytaniu danych i załadowaniu wzoru.
/// [27.12.2016] Podgląd wzoru bez danych (wcześniej i tak trzeba było wczytywać dane) i możliwość ustalenia ilości kopii,
///              drukowanie na dwa sposoby z łączeniem stron w wierszach (jak to było dotąd) i osobno, dynamiczna nazwa
///              przycisku do wczytywania danych w edytorze po zapisie (gdy wzór posiada dynamiczne dane lub nie),
///              pobieranie danych z tych wczytanych do aplikacji dla generowania wzoru.
/// [31.12.2016] Wzór dodawany jest do ostatnio otwieranych zaraz po utworzeniu, usuwanie poprawnej strony w edytorze.
/// [07.01.2017] Przechwytywanie błędów w przypadku problemów z czcionkami.
/// [16.01.2017] Odświeżanie listy po imporcie plików, wyłączenie sprawdzania poprawności wpisywanych danych w nazwie pola.
///

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using PdfSharp.Pdf;
using PdfSharp;
using PdfSharp.Drawing;
using System.Drawing.Printing;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Net;
using CDesigner.Utils;
using CDesigner.Controls;

namespace CDesigner.Forms
{
	/// 
	/// <summary>
	/// Klasa tworząca główne okno programu podzielone na trzy sekcje.
	/// Każda sekcja programu odpowiada za coś innego, połączone są w jeden formularz z powodu szybkości
	/// w działaniu, wygody (nie trzeba przełączać się pomiędzy formularzami) i synchronizacji pomiędzy sekcjami.
	/// Wyróżnia się trzy sekcje:
	/// <list type="bullet">
	///     <item><description>Główna <i>(zawierająca listę dostępnych wzorów i ich podgląd).</i></description></item>
	///     <item><description>Edytor <i>(zawierający elementy pozwalający na tworzenie i edycje wzorów).</i></description></item>
	///     <item><description>Wydruk <i>(wyświetlający strony wzoru wykorzystując wczytane dane).</i></description></item>
	/// </list>
	/// Wszystkie te sekcje są ze sobą powiązane. Możliwe jest szybkie przejście z jednej sekcji na drugą, używając
	/// przycisków znajdujących się po prawej stronie menu. Dodatkowo edytor dzieli się na kolejne 3 sekcje, dostępne po
	/// zaznaczeniu wybranego pola lub utworzeniu nowego. Są to:
	/// <list type="bullet">
	///     <item><description>Pole <i>(zawiera ustawienia pola znajdującego się na stronie).</i></description></item>
	///     <item><description>Szczegóły <i>(zawiera szczegóły wybranego pola na stronie).</i></description></item>
	///     <item><description>Strona <i>(zawiera ustawienia wybranej strony aktywnego wzoru).</i></description></item>
	/// </list>
	/// Te sekcje dzielą edytor na trzy części, ustawienia pola, powszechnie używane, szczegóły pola, używane mniej, ale
	/// nadal bardzo ważne i ustawienia strony, które są rzadko kiedy zmieniane. Raz utworzony wzór nie może zmienić
	/// swoich wymiarów. To samo tyczy się wzoru, który jest klonowany - wzór klonowany również nie może zmienić rozmiaru.
	/// </summary>
	/// 
	public partial class MainForm : Form
	{
#region ZMIENNE

		/// <summary>Strumień danych przeznaczony do edycji.</summary>
		private IOFileData _stream;

		/// <summary>Strumień danych używany w podglądzie wydruku.</summary>
		private DataStorage _preparedStream;

		/// <summary>Aktualny stan okna.</summary>
		private FormWindowState _windowLastState;

		/// <summary>Blokada kontrolek.</summary>
		private bool _locked;

		/// <summary>Indeks aktualnie wyświetlanego panelu na formularzu.</summary>
		private int _currentPanel;

		/// <summary>Pole aktywne w edytorze wzorów.</summary>
		private PageField _activeField;

		/// <summary>Strona aktywna w edytorze wzorów.</summary>
		private Panel _activePage;

		/// <summary>Flaga przechowująca informacje o tym, czy dymek jest pokazywany czy nie.</summary>
		private bool _tooltipShow;

		/// <summary>Aktualnie zaznaczone pole, przeznaczone do przesuwania.</summary>
		private PageField _movingField;

		/// <summary>Pozycja X aktualnie zaznaczonego pola.</summary>
		private int _movingDiffX;

		/// <summary>Pozycja Y aktualnie zaznaczonego pola.</summary>
		private int _movingDiffY;

		/// <summary>Zmienna informująca o tym, czy podgląd wzoru się zmienił.</summary>
		private bool _editedChanged;

		/// <summary>Panel podglądu wzoru na stronie głównej.</summary>
		private AlignedPictureBox _patternPreview;

		/// <summary>Indeks zaznaczonego wzoru na stronie głównej.</summary>
		private int _selectedID;

		/// <summary>Informacja o tym, czy zaznaczony wzór zawiera błędy.</summary>
		private bool _selectedError;

		/// <summary>Czy zaznaczony wzór posiada treść dynamiczną?</summary>
		private bool _selectedDynamic;

		/// <summary>Nazwa aktualnie zaznaczonego wzoru.</summary>
		private string _selectedName;

		/// <summary>Nazwa aktualnie edytowanego wozru.</summary>
		private string _editingName;

		/// <summary>Informacje dotyczące aktualnie edytowanego wzoru.</summary>
		private PatternData _editingData;

		/// <summary>Licznik stron występujących we wzorze.</summary>
		private int	_editingPages;

		/// <summary>Aktualny identyfikator edytowanej strony we wzorze.</summary>
		private int	_editingPageID;

		/// <summary>Wymiary aktualnie edytowanej strony.</summary>
		private Size _editingPageSize;

		/// <summary>Informacje dotyczące aktualnie podglądanego wzoru w generatorze.</summary>
		private PatternData _generatorData;

		/// <summary>Informacja o tym, czy szablon został narysowany w generatorze.</summary>
		private bool _generatorSketch;

		/// <summary>Aktualna strona wyświetlana w generatorze.</summary>
		private Panel _generatorPage;

#endregion

#region KONSTRUKTOR

		/// <summary>
		/// Konstruktor klasy CDesigner.
		/// Tworzy okno główne i uzupełnia go kontrolkami.
		/// Uzupełnia zmienne domyślnymi wartościami wraz z uzupełnianiem poszczególnych elementów w formularzu.
		/// Uruchamia w tle funkcje sprawdzającą, czy aktualzacja programu jest dostępna.
		/// </summary>
		//* ============================================================================================================
		public MainForm()
		{
#		if DEBUG
			Program.LogMessage( "Tworzenie okna głównego." );
#		endif

			this.InitializeComponent();

			// domyślne wartości
			this._currentPanel    = 1;
			this._locked          = false;
			this._preparedStream  = null;
			this._stream          = null;
			this._windowLastState = FormWindowState.Normal;
			this._activeField     = null;
			this._tooltipShow     = false;
			this._movingField     = null;
			this._movingDiffX     = 0;
			this._movingDiffY     = 0;
			this._editedChanged   = false;
			this._selectedID      = -1;
			this._selectedError   = false;
			this._selectedDynamic = false;
			this._selectedName    = null;
			this._editingName     = null;
			this._editingData     = null;
			this._editingPages    = 0;
			this._editingPageID   = -1;
			this._editingPageSize = new Size();
			this._generatorData   = null;
			this._generatorSketch = false;
			this._generatorPage   = null;

			// sprawdzanie aktualizacji w tle
			this.BW_Updates.DoWork -= this.getProgramUpdates;
			this.BW_Updates.DoWork += this.getProgramUpdates;

			this.BW_Updates.RunWorkerAsync();

			// pobierz ikonę programu
			this.Icon = Program.GetIcon();

			// podgląd wzoru na stronie głównej
			this._patternPreview = new AlignedPictureBox();
			
			this._patternPreview.Align      = 1;
			this._patternPreview.SizeMode   = PictureBoxSizeMode.AutoSize;
			this._patternPreview.MouseDown += new MouseEventHandler( P_P1_Preview_MouseDown );

			this.P_P1_Preview.Controls.Add( this._patternPreview );

			Settings.GetLastPatterns();

#		if DEBUG
			Program.LogMessage( "Załadowano kontrolki." );
#		endif

			// ostatnio otwierane wzory
			this.refreshRecentOpenList();

			// odświeżenie listy wzorów
			this.refreshProjectList();
			this.fillAvailableLanguages();

			// tłumaczenia
			this.translateMenu();
			this.translateHomeForm();
			this.translateEditorForm();
			this.translatePrintoutForm();

			// utwórz klasy korzystające z wielu formularzy
			Program.GLOBAL.SelectFile       = new OpenFileDialog();
			Program.GLOBAL.SaveFile         = new SaveFileDialog();
			Program.GLOBAL.DatafileSettings = new DatafileSettingsForm();
			Program.GLOBAL.Info             = new InfoForm();
			
			Program.GLOBAL.SelectFile.Title = Language.GetLine( "MessageNames", (int)LANGCODE.GMN_SELECTFILE );
			Program.GLOBAL.SaveFile.Title   = Language.GetLine( "MessageNames", (int)LANGCODE.GMN_SAVEFILE );

			Program.GLOBAL.SaveFile.AddExtension = true;
		}
		
#endregion

#region TŁUMACZENIA

		/// <summary>
		/// Translator menu.
		/// Funkcja tłumaczy wszystkie elementy menu głównego.
		/// Wywoływana jest z konstruktora oraz podczas odświeżania ustawień językowych.
		/// Jej użycie nie powinno wykraczać poza dwa wyżej wymienione przypadki.
		/// </summary>
		/// 
		/// <seealso cref="MainForm"/>
		/// <seealso cref="Language"/>
		//* ============================================================================================================
		protected void translateMenu()
		{
#		if DEBUG
			Program.LogMessage( "Tłumaczenie treści menu." );
#		endif

			// menu wzoru
			var values = Language.GetLines( "Menu", "Pattern" );

			this.TSMI_Pattern.Text        = values[(int)LANGCODE.I01_MEN_PAT_PATTERN];
			this.TSMI_MenuNewPattern.Text = values[(int)LANGCODE.I01_MEN_PAT_NEWPATTERN];
			this.TSMI_RecentOpen.Text     = values[(int)LANGCODE.I01_MEN_PAT_RECENTOPEN];
			this.TSMI_ClearRecent.Text    = values[(int)LANGCODE.I01_MEN_PAT_CLEARLIST];
			this.TSMI_Close.Text          = values[(int)LANGCODE.I01_MEN_PAT_CLOSE];
			this.TSMI_Import.Text         = values[(int)LANGCODE.I01_MEN_PAT_IMPORT];
			this.TSMI_ExportAll.Text      = values[(int)LANGCODE.I01_MEN_PAT_EXPORTALL];

			// menu narzędzi
			values = Language.GetLines( "Menu", "Tools" );

			this.TSMI_Tools.Text          = values[(int)LANGCODE.I01_MEN_TOL_TOOLS];
			this.TSMI_CreateEmpty.Text    = values[(int)LANGCODE.I01_MEN_TOL_MEMORYDB];
			this.TSMI_LoadDataFile.Text   = values[(int)LANGCODE.I01_MEN_TOL_LOADFILE];
			this.TSMI_EditColumns.Text    = values[(int)LANGCODE.I01_MEN_TOL_EDITCOLUMN];
			this.TSMI_EditRows.Text       = values[(int)LANGCODE.I01_MEN_TOL_EDITROW];
			this.TSMI_SaveData.Text       = values[(int)LANGCODE.I01_MEN_TOL_SAVEMEMDB];
			this.TSMI_CloseData.Text      = values[(int)LANGCODE.I01_MEN_TOL_CLOSEDATA];

			// menu językowe
			this.TSMI_Language.Text = Language.GetLine( "Menu", "Language", (int)LANGCODE.I01_MEN_LAN_LANGUAGE );

			// menu programu
			values = Language.GetLines( "Menu", "Program" );

			this.TSMI_Program.Text = values[(int)LANGCODE.I01_MEN_PRO_PROGRAM];
			this.TSMI_Info.Text    = values[(int)LANGCODE.I01_MEN_PRO_INFO];
			this.TSMI_Update.Text  = values[(int)LANGCODE.I01_MEN_PRO_UPDATES];

			// przełącznik
			values = Language.GetLines( "Menu", "Switcher" );

			this.TSMI_HomePage.Text      = values[(int)LANGCODE.I01_MEN_SWI_MAIN];
			this.TSMI_PatternEditor.Text = values[(int)LANGCODE.I01_MEN_SWI_EDITOR];
			this.TSMI_PrintPreview.Text  = values[(int)LANGCODE.I01_MEN_SWI_GENERATOR];
		}

		/// <summary>
		/// Translator głównego formularza.
		/// Funkcja tłumaczy wszystkie jego statyczne elementy.
		/// Wywoływana jest z konstruktora oraz podczas odświeżania ustawień językowych.
		/// Jej użycie nie powinno wykraczać poza dwa wyżej wymienione przypadki.
		/// </summary>
		/// 
		/// <seealso cref="MainForm"/>
		/// <seealso cref="Language"/>
		//* ============================================================================================================
		protected void translateHomeForm()
		{
#		if DEBUG
			Program.LogMessage( "Tłumaczenie kontrolek znajdujących się na głównym formularzu." );
#		endif

			// przyciski na dole
			var values = Language.GetLines( "PatternList", "Buttons" );

			this.B_P1_New.Text    = values[(int)LANGCODE.I01_PAT_BUT_NEWPATTERN];
			this.B_P1_Delete.Text = values[(int)LANGCODE.I01_PAT_BUT_DELETE];

			// napisy obok kontrolek
			values = Language.GetLines( "PatternList", "Labels" );

			this.CB_P1_ShowDetails.Text = values[(int)LANGCODE.I01_PAT_LAB_PATDETAILS];
			this.L_P1_Page.Text         = values[(int)LANGCODE.I01_PAT_LAB_PAGE];

			// menu kontekstowe
			values = Language.GetLines( "PatternList", "PatternContext" );

			this.TSMI_NewPattern.Text    = values[(int)LANGCODE.I01_PAT_CTX_NEWPATTERN];
			this.TSMI_ImportPattern.Text = values[(int)LANGCODE.I01_PAT_CTX_IMPORT];
			this.TSMI_ExportPattern.Text = values[(int)LANGCODE.I01_PAT_CTX_EXPORT];
			this.TSMI_EditPattern.Text   = values[(int)LANGCODE.I01_PAT_CTX_EDITSELECT];
			this.TSMI_RemovePattern.Text = values[(int)LANGCODE.I01_PAT_CTX_REMOVEPAT];

			this.Text = Language.GetLine( "FormNames", (int)LANGCODE.GFN_CDESIGNER );

			// przetłumacz informacje
			TV_P1_Patterns_AfterSelect( null, null );
		}
		
		/// <summary>
		/// Translator edytora wzorów.
		/// Funkcja tłumaczy wszystkie jego statyczne elementy.
		/// Wywoływana jest z konstruktora oraz podczas odświeżania ustawień językowych.
		/// Jej użycie nie powinno wykraczać poza dwa wyżej wymienione przypadki.
		/// </summary>
		/// 
		/// <seealso cref="MainForm"/>
		/// <seealso cref="Language"/>
		//* ============================================================================================================
		protected void translateEditorForm()
		{
#		if DEBUG
			Program.LogMessage( "Tłumaczenie kontrolek edytora wzorów." );
#		endif

			// przełącznik
			var values = Language.GetLines( "PatternEditor", "Switcher" );

			this.TSMI_PageSwitch.Text   = values[(int)LANGCODE.I01_EDI_SWI_PAGE];
			this.TSMI_FieldSwitch.Text  = values[(int)LANGCODE.I01_EDI_SWI_FIELD];
			this.TSMI_DetailSwitch.Text = values[(int)LANGCODE.I01_EDI_SWI_DETAILS];

			// przyciski
			values = Language.GetLines( "PatternEditor", "Buttons" );

			this.B_P2_Save.Text        = values[(int)LANGCODE.I01_EDI_BUT_SAVE];
			this.B_P2_LoadData.Text    = values[(int)LANGCODE.I01_EDI_BUT_LOADDATA];
			this.B_P2_BackColor.Text   = values[(int)LANGCODE.I01_EDI_BUT_FIELDCOLOR];
			this.B_P2_BackImage.Text   = values[(int)LANGCODE.I01_EDI_BUT_FIELDIMAGE];
			this.B_P2_BorderColor.Text = values[(int)LANGCODE.I01_EDI_BUT_BRDRCOLOR];
			this.B_P2_FontColor.Text   = values[(int)LANGCODE.I01_EDI_BUT_FONTCOLOR];
			this.B_P2_FontName.Text    = values[(int)LANGCODE.I01_EDI_BUT_FONTNAME];
			this.B_P2_PageColor.Text   = values[(int)LANGCODE.I01_EDI_BUT_PAGECOLOR];
			this.B_P2_PageImage.Text   = values[(int)LANGCODE.I01_EDI_BUT_PAGEIMAGE];

			// menu kontekstowe strony
			values = Language.GetLines( "PatternEditor", "PageContext" );

			this.TSMI_AddField.Text      = values[(int)LANGCODE.I01_EDI_PCX_ADDFIELD];
			this.TSMI_RemoveAll.Text     = values[(int)LANGCODE.I01_EDI_PCX_REMOVEALL];
			this.TSMI_PageColor.Text     = values[(int)LANGCODE.I01_EDI_PCX_PAGECOLOR];
			this.TSMI_PageImage.Text     = values[(int)LANGCODE.I01_EDI_PCX_PAGEIMAGE];
			this.TSMI_PageClear.Text     = values[(int)LANGCODE.I01_EDI_PCX_CLEARBACK];
			this.TSMI_PageDrawColor.Text = values[(int)LANGCODE.I01_EDI_PCX_DRAWCOLOR];
			this.TSMI_PageDrawImage.Text = values[(int)LANGCODE.I01_EDI_PCX_DRAWIMAGE];
			this.TSMI_AddPage.Text       = values[(int)LANGCODE.I01_EDI_PCX_ADDPAGE];
			this.TSMI_RemovePage.Text    = values[(int)LANGCODE.I01_EDI_PCX_REMOVEPAGE];

			// menu kontekstowe pola
			values = Language.GetLines( "PatternEditor", "LabelContext" );

			this.TSMI_FieldColor.Text    = values[(int)LANGCODE.I01_EDI_LCX_FIELDCOLOR];
			this.TSMI_FieldImage.Text    = values[(int)LANGCODE.I01_EDI_LCX_FIELDIMAGE];
			this.TSMI_FieldClear.Text    = values[(int)LANGCODE.I01_EDI_LCX_CLEARBACK];
			this.TSMI_StaticImage.Text   = values[(int)LANGCODE.I01_EDI_LCX_IMAGESTAT];
			this.TSMI_DynamicImage.Text  = values[(int)LANGCODE.I01_EDI_LCX_IMAGEDYN];
			this.TSMI_DrawFieldFill.Text = values[(int)LANGCODE.I01_EDI_LCX_DRAWCOLOR];
			this.TSMI_FontColor.Text     = values[(int)LANGCODE.I01_EDI_LCX_FONTCOLOR];
			this.TSMI_FontName.Text      = values[(int)LANGCODE.I01_EDI_LCX_CHANGEFONT];
			this.TSMI_DynamicText.Text   = values[(int)LANGCODE.I01_EDI_LCX_TEXTDYN];
			this.TSMI_StaticText.Text    = values[(int)LANGCODE.I01_EDI_LCX_TEXTSTAT];
			this.TSMI_RemoveField.Text   = values[(int)LANGCODE.I01_EDI_LCX_REMOVE];
			this.TSMI_BorderColor.Text   = values[(int)LANGCODE.I01_EDI_LCX_BRDRCOLOR];
			this.TSMI_DrawBorder.Text    = values[(int)LANGCODE.I01_EDI_LCX_DRAWBORDER];

			// napisy na formularzu
			values = Language.GetLines( "PatternEditor", "Labels" );

			this.L_P2_Page.Text          = values[(int)LANGCODE.I01_EDI_LAB_PAGE];
			this.L_P2_PosX.Text          = values[(int)LANGCODE.I01_EDI_LAB_POSITIONX];
			this.L_P2_PosY.Text          = values[(int)LANGCODE.I01_EDI_LAB_POSITIONY];
			this.L_P2_Width.Text         = values[(int)LANGCODE.I01_EDI_LAB_FWIDTH];
			this.L_P2_Height.Text        = values[(int)LANGCODE.I01_EDI_LAB_FHEIGHT];
			this.G_P2_Appearance.Text    = " " + values[(int)LANGCODE.I01_EDI_LAB_FAPPEARNCE] + " ";
			this.G_P2_Font.Text          = " " + values[(int)LANGCODE.I01_EDI_LAB_FONT] + " ";
			this.L_P2_TextPosition.Text  = values[(int)LANGCODE.I01_EDI_LAB_TEXTPOS];
			this.L_P2_Padding.Text       = values[(int)LANGCODE.I01_EDI_LAB_TEXTMARGIN];
			this.L_P2_TextTransform.Text = values[(int)LANGCODE.I01_EDI_LAB_TEXTDRAW];
			this.L_P2_BorderWidth.Text   = values[(int)LANGCODE.I01_EDI_LAB_BORDERSIZE];
			this.G_P2_Generator.Text     = " " + values[(int)LANGCODE.I01_EDI_LAB_FIELDTOPDF] + " ";
			this.L_P2_ImageSettings.Text = values[(int)LANGCODE.I01_EDI_LAB_FIMAGESET];
			this.L_P2_StickPoint.Text    = values[(int)LANGCODE.I01_EDI_LAB_FANCHOR];
			this.L_P2_AddMargin.Text     = values[(int)LANGCODE.I01_EDI_LAB_FADDMARGIN];
			this.L_P2_PageWidth.Text     = values[(int)LANGCODE.I01_EDI_LAB_PAGEWIDTH];
			this.L_P2_PageHeight.Text    = values[(int)LANGCODE.I01_EDI_LAB_PAGEHEIGHT];
			this.G_P2_PageApperance.Text = " " + values[(int)LANGCODE.I01_EDI_LAB_PAPPEARNCE] + " ";
			this.G_P2_GeneratePDF.Text   = " " + values[(int)LANGCODE.I01_EDI_LAB_PAGETOPDF] + " ";
			this.L_P2_PageImageSet.Text  = values[(int)LANGCODE.I01_EDI_LAB_PIMAGESET];

			// pozycje tekstu
			values = Language.GetLines( "PatternEditor", "TextPosition" );

			for( int x = 0; x < 9; ++x )
				this.CBX_P2_TextPosition.Items[x] = values[x];

			// transformacja tekstu
			values = Language.GetLines( "PatternEditor", "TextTransform" );

			for( int x = 0; x < 4; ++x )
				this.CBX_P2_TextTransform.Items[x] = values[x];

			// pola do zaznaczenia
			values = Language.GetLines( "PatternEditor", "Checkboxes" );

			this.CB_P2_DrawBorder.Text       = values[(int)LANGCODE.I01_EDI_CBK_DRAWBORDER];
			this.CB_P2_DrawColor.Text        = values[(int)LANGCODE.I01_EDI_CBK_DRAWFCOLOR];
			this.CB_P2_DynamicText.Text      = values[(int)LANGCODE.I01_EDI_CBK_DYNTEXT];
			this.CB_P2_StaticText.Text       = values[(int)LANGCODE.I01_EDI_CBK_STATTEXT];
			this.CB_P2_DynamicImage.Text     = values[(int)LANGCODE.I01_EDI_CBK_DYNIMAGE];
			this.CB_P2_StaticImage.Text      = values[(int)LANGCODE.I01_EDI_CBK_STATIMAGE];
			this.CB_P2_DrawFrameOutside.Text = values[(int)LANGCODE.I01_EDI_CBK_BORDERFOUT];
			this.CB_P2_UseImageMargin.Text   = values[(int)LANGCODE.I01_EDI_CBK_ADDIMGMAR];
			this.CB_P2_AdditionalMargin.Text = values[(int)LANGCODE.I01_EDI_CBK_ADDTXTMAR];
			this.CB_P2_DrawPageColor.Text    = values[(int)LANGCODE.I01_EDI_CBK_DRAWPCOLOR];
			this.CB_P2_DrawPageImage.Text    = values[(int)LANGCODE.I01_EDI_CBK_DRAWPIMAGE];
			this.CB_P2_ApplyMargin.Text      = values[(int)LANGCODE.I01_EDI_CBK_MARGINSET];
			this.CB_P2_DrawOutside.Text      = values[(int)LANGCODE.I01_EDI_CBK_BORDEROUT];

			// punkt zaczepienia
			values = Language.GetLines( "PatternEditor", "AnchorPosition" );

			for( int x = 0; x < 4; ++x )
				this.CBX_P2_StickPoint.Items[x] = values[x];
		}
		
		/// <summary>
		/// Translator podglądu wydruku.
		/// Funkcja tłumaczy wszystkie jego statyczne elementy.
		/// Wywoływana jest z konstruktora oraz podczas odświeżania ustawień językowych.
		/// Jej użycie nie powinno wykraczać poza dwa wyżej wymienione przypadki.
		/// </summary>
		/// 
		/// <seealso cref="MainForm"/>
		/// <seealso cref="Language"/>
		//* ============================================================================================================
		protected void translatePrintoutForm()
		{
#		if DEBUG
			Program.LogMessage( "Tłumaczenie kontrolek podglądu wydruku." );
#		endif

			// przyciski
			var values = Language.GetLines( "PrintoutPreview", "Buttons" );

			this.B_P3_GeneratePDF.Text  = values[(int)LANGCODE.I01_PRI_BUT_GENERATE];
			this.B_P3_SearchErrors.Text = values[(int)LANGCODE.I01_PRI_BUT_FINDERRORS];

			// napisy na formularzu
			values = Language.GetLines( "PrintoutPreview", "Labels" );

			this.L_P3_Page.Text = values[(int)LANGCODE.I01_PRI_LAB_PAGE];
			this.L_P3_Rows.Text = values[(int)LANGCODE.I01_PRI_LAB_POSITIONS];

			// tłumacz napis w liście elementów dla danych statycznych
			if( this._generatorData != null && !this._generatorData.Dynamic && this.TV_P3_PageList.Nodes.Count == 1 )
				this.TV_P3_PageList.Nodes[0].Text = values[(int)LANGCODE.I01_PRI_LAB_ROW];
			
			this.CB_P3_CollatePages.Text = values[(int)LANGCODE.I01_PRI_LAB_COLLATE];
		}

		/// <summary>
		/// Uzupełnia menu dostępnymi do zmiany językami.
		/// Funkcja sprawdza pliki w folderze languages i wyświetla ich nazwy w menu.
		/// Dzięki temu można łatwo przełączać się pomiędzy językami.
		/// </summary>
		/// 
		/// <seealso cref="MainForm"/>
		//* ============================================================================================================
		protected void fillAvailableLanguages()
		{
			// pobierz pliki językowe
			var files = Directory.GetFiles( "./languages/" );
			var llist = new Dictionary<string, string>();
			var code  = Language.GetCode();

			// pobierz nazwy języków
			foreach( var file in files )
			{
				var finfo = new FileInfo( file );

				// rozszerzenie musi być "lex"
				if( finfo.Extension != ".lex" )
					continue;
				if( finfo.Name == "qps.lex" )
					continue;

				var name  = finfo.Name.Replace( finfo.Extension, "" );
				var lname = Language.GetLanguageNames( name );

				if( lname.ContainsKey(code) )
					llist.Add( name, lname[code] );
				else if( lname.ContainsKey("def") )
					llist.Add( name, lname["def"] );
				else
					llist.Add( name, name );
			}

			// wyczyść języki z menu
			this.TSMI_Language.DropDownItems.Clear();

			// dodaj nowe języki
			foreach( var lang in llist )
			{
				var item = new ToolStripMenuItem( lang.Value );

				if( lang.Key == code )
					item.Checked = true;

				item.Tag = lang.Key;
				item.Click += new EventHandler( TSMI_Language_Click );

				this.TSMI_Language.DropDownItems.Add( item );
			}
		}

#endregion

#region FUNKCJE PODSTAWOWE
		
		/// <summary>
		/// Blokada kontrolek w edytorze.
		/// Funkcja odblokowuje kontrolki w edytorze do edycji zaznaczonego pola.
		/// </summary>
		//* ============================================================================================================
		private void lockFieldLabels()
		{
			this.B_P2_BorderColor.Enabled     = false;
			this.B_P2_FontName.Enabled        = false;
			this.B_P2_FontColor.Enabled       = false;
			this.B_P2_BackImage.Enabled       = false;
			this.B_P2_BackColor.Enabled       = false;
			this.N_P2_BorderSize.Enabled      = false;
			this.N_P2_PosX.Enabled            = false;
			this.N_P2_PosY.Enabled            = false;
			this.N_P2_Width.Enabled           = false;
			this.N_P2_Height.Enabled          = false;
			this.TB_P2_LabelName.Enabled      = false;
			this.N_P2_Padding.Enabled         = false;
			this.CBX_P2_TextPosition.Enabled  = false;
			this.CBX_P2_StickPoint.Enabled    = false;
			this.CB_P2_DrawColor.Enabled      = false;
			this.CB_P2_DynamicImage.Enabled   = false;
			this.CB_P2_DynamicText.Enabled    = false;
			this.CB_P2_DrawBorder.Enabled     = false;
			this.CB_P2_StaticImage.Enabled    = false;
			this.CB_P2_StaticText.Enabled     = false;
			this.CBX_P2_TextTransform.Enabled = false;
			this.CB_P2_UseImageMargin.Enabled = false;
			this.CB_P2_DrawOutside.Enabled    = false;
		}

		/// <summary>
		/// Wyłączenie blokady kontrolek w edytorze.
		/// Funkcja odblokowuje kontrolki w edytorze do edycji zaznaczonego pola.
		/// </summary>
		//* ============================================================================================================
		private void unlockFieldLabels()
		{
			this.B_P2_BorderColor.Enabled     = true;
			this.B_P2_FontName.Enabled        = true;
			this.B_P2_FontColor.Enabled       = true;
			this.B_P2_BackImage.Enabled       = true;
			this.B_P2_BackColor.Enabled       = true;
			this.N_P2_BorderSize.Enabled      = true;
			this.N_P2_PosX.Enabled            = true;
			this.N_P2_PosY.Enabled            = true;
			this.N_P2_Width.Enabled           = true;
			this.N_P2_Height.Enabled          = true;
			this.TB_P2_LabelName.Enabled      = true;
			this.N_P2_Padding.Enabled         = true;
			this.CBX_P2_TextPosition.Enabled  = true;
			this.CBX_P2_StickPoint.Enabled    = true;
			this.CB_P2_DrawColor.Enabled      = true;
			this.CB_P2_DynamicText.Enabled    = true;
			this.CB_P2_DrawBorder.Enabled     = true;
			this.CBX_P2_TextTransform.Enabled = true;
			this.N_P2_MarginLR.Enabled        = true;
			this.N_P2_MarginTB.Enabled        = true;
			this.CB_P2_StaticText.Enabled     = true;
			this.CB_P2_StaticImage.Enabled    = true;
		}

		/// <summary>
		/// Uzupełnienie pól edytora.
		/// Funkcja uzupełnia pola edytora względem zaznaczonego pola do edycji.
		/// Pola uzupełniane są na dwóch kartach - POLE i SZCZEGÓŁY.
		/// </summary>
		//* ============================================================================================================
		private void fillFieldLabels()
		{
			this._locked = true;

			// zmień nazwę
			this.TB_P2_LabelName.Text = this._activeField.OriginalText;

			var location = this._activeField.getPosByAlignPoint(
				(ContentAlignment)((FieldExtraData)this._activeField.Tag).PosAlign );

			// aktualizuj pola numeryczne
			this.N_P2_PosX.Value       = (decimal)location.X;
			this.N_P2_PosY.Value       = (decimal)location.Y;
			this.N_P2_Height.Value     = (decimal)this._activeField.DPIBounds.Height;
			this.N_P2_Width.Value      = (decimal)this._activeField.DPIBounds.Width;
			this.N_P2_BorderSize.Value = (decimal)this._activeField.DPIBorderSize;
			this.N_P2_Padding.Value    = (decimal)this._activeField.DPIPadding;

			string color_r, color_g, color_b;

			// aktualizuj kolory...
			color_r = this._activeField.BorderColor.R.ToString("X2");
			color_g = this._activeField.BorderColor.G.ToString("X2");
			color_b = this._activeField.BorderColor.B.ToString("X2");
			this.TB_P2_BorderColor.Text = "#" + color_r + color_b + color_g;

			color_r = this._activeField.BackColor.R.ToString("X2");
			color_g = this._activeField.BackColor.G.ToString("X2");
			color_b = this._activeField.BackColor.B.ToString("X2");
			this.TB_P2_BackColor.Text = "#" + color_r + color_g + color_b;

			color_r = this._activeField.ForeColor.R.ToString("X2");
			color_g = this._activeField.ForeColor.G.ToString("X2");
			color_b = this._activeField.ForeColor.B.ToString("X2");
			this.TB_P2_FontColor.Text = "#" + color_r + color_g + color_b;

			// czcionka
			var font = this._activeField.Font;
			var dets = (font.Bold ? "B" : "") + (font.Italic ? "I" : "") +
				(font.Strikeout ? "S" : "") + (font.Underline ? "U" : "");

			this.TB_P2_FontName.Text = font.Name + ", " + font.SizeInPoints + "pt " + dets;

			// transformacja tekstu
			this.CBX_P2_TextTransform.SelectedIndex = this._activeField.TextTransform;

			// pozycja tekstu...
			switch( (int)this._activeField.TextAlign )
			{
				case 0x001: this.CBX_P2_TextPosition.SelectedIndex = 0; break;
				case 0x002: this.CBX_P2_TextPosition.SelectedIndex = 1; break;
				case 0x004: this.CBX_P2_TextPosition.SelectedIndex = 2; break;
				case 0x010: this.CBX_P2_TextPosition.SelectedIndex = 3; break;
				case 0x020: this.CBX_P2_TextPosition.SelectedIndex = 4; break;
				case 0x040: this.CBX_P2_TextPosition.SelectedIndex = 5; break;
				case 0x100: this.CBX_P2_TextPosition.SelectedIndex = 6; break;
				case 0x200: this.CBX_P2_TextPosition.SelectedIndex = 7; break;
				case 0x400: this.CBX_P2_TextPosition.SelectedIndex = 8; break;
			}

			// pozycja położenia...
			switch( ((FieldExtraData)this._activeField.Tag).PosAlign )
			{
				case 0x001: this.CBX_P2_StickPoint.SelectedIndex = 0; break;
				case 0x004: this.CBX_P2_StickPoint.SelectedIndex = 1; break;
				case 0x100: this.CBX_P2_StickPoint.SelectedIndex = 2; break;
				case 0x400: this.CBX_P2_StickPoint.SelectedIndex = 4; break;
				default:    this.CBX_P2_StickPoint.SelectedIndex = 0; break;
			}

			var tag = (FieldExtraData)this._activeField.Tag;

			// zaznacz lub odznacz elementy
			this.CB_P2_DrawColor.Checked   = tag.PrintColor;
			this.CB_P2_DynamicText.Checked = tag.TextFromDB;
			this.CB_P2_StaticText.Checked  = tag.PrintText;
			this.CB_P2_DrawBorder.Checked  = tag.PrintBorder;

			// nazwa / ścieżka obrazu
			this.TB_P2_BackImage.Text = this._activeField.BackImagePath != null
				? this._activeField.BackImagePath
				: "";

			this._locked = false;
		}

		/// <summary>
		/// Funkcja pozwalająca przejść do generatora bez danych.
		/// Na początek generator uzupełniany jest tylko jednym wierszem.
		/// Możliwe jest jednak zwiększenie tej ilości poprzez modyfikacje kontrolki numerycznej.
		/// Generator bez danych uruchamiany jest wtedy, gdy wzór nie posiada danych dynamicznych.
		/// </summary>
		/// 
		/// <seealso cref="loadDataForPreview"/>
		/// 
		/// <param name="data">Dane wzoru do podglądu.</param>
		//* ============================================================================================================
		private void loadStaticPatternForPreview( PatternData data )
		{
			if( data.Dynamic )
				return;

#		if DEBUG
			Program.LogMessage( "Wczytywanie podglądu wzoru z danymi statycznymi..." );
#		endif
			
			this._generatorData  = data;
			this._preparedStream = null;
			
			this.P_P3_Generator.Controls.Clear();
			this.TV_P3_PageList.Nodes.Clear();

			// przygotuj jeden wiersz
			this.TV_P3_PageList.Nodes.Add( Language.GetLine("PrintoutPreview", "Labels", (int)LANGCODE.I01_PRI_LAB_ROW) );

			// ustaw indeks początkowy dla pola
			this._locked = true;
			this.N_P3_Rows.Value            = 1;
			this.CBX_P3_Scale.SelectedIndex = 2;
			this._locked = false;

			// generator musi ponownie przerysować szkic
			this._generatorSketch = false;

			// zaznacz pierwszy element
			if( this.TV_P3_PageList.Nodes.Count > 0 )
				this.TV_P3_PageList.SelectedNode = this.TV_P3_PageList.Nodes[0];
			
			// przejdź na strone z danymi
			this.TSMI_PrintPreview_Click( null, null );

			GC.Collect();

#		if DEBUG
			Program.LogMessage( "Podgląd gotowy, można teraz manipulować ilością stron." );
#		endif
		}

		/// <summary>
		/// Funkcja wczytywania danych do podlgądu.
		/// Wyodrębniona z racji tego, że zawartość jest dosyć duża, a używana jest w dwóch miejscach.
		/// Otwiera okno wyboru pliku a po jego zamknięciu okno ustawienia przetwarzania wczytywanego pliku.
		/// Po akceptacji otwiera okno przypisywania kolumn do pól dla wybranego wzoru.
		/// </summary>
		/// 
		/// <seealso cref="loadStaticPatternForPreview"/>
		/// 
		/// <param name="patname">Nazwa wzoru do wczytania.</param>
		//* ============================================================================================================
		private void loadDataForPreview( string patname )
		{
			var patdata = PatternEditor.ReadPattern( patname );
			
			// jeżeli jest null lub brak stron, nie idź dalej
			if( patdata == null || patdata.Pages == 0 )
				return;

			// ustaw maksymalną ilość stron
			this._locked = true;
			this.N_P3_Page.Value = 1;
			this.N_P3_Page.Maximum = patdata.Pages;
			this._locked = false;

			// tylko podgląd
			if( !patdata.Dynamic )
			{
				this.loadStaticPatternForPreview( patdata );
				return;
			}

			// otwieraj okno wczytywania tylko gdy nie wczytano danych do programu lub dane zostały zamknięte
			IOFileData storage = this._stream;
			if( this._stream == null )
			{
#			if DEBUG
				Program.LogMessage( "** Okno wczytywania danych potrzebnych do podglądu wydruku." );
				Program.LogMessage( "** BEGIN ================================================================== **" );
				Program.IncreaseLogIndent();
#			endif
				// wybór pliku
				var select = Program.GLOBAL.SelectFile;
				select.Title  = Language.GetLine( "MessageNames", (int)LANGCODE.GMN_SELECTFILE );
				select.Filter = IOFileData.getExtensionsList( true );

				if( select.ShowDialog(this) != DialogResult.OK )
				{
#				if DEBUG
					Program.LogMessage( "Operacja anulowana." );
					Program.DecreaseLogIndent();
					Program.LogMessage( "** END ==================================================================== **" );
#				endif
					return;
				}

				// wczytaj plik
				storage = new IOFileData( select.FileName, Encoding.Default );
				if( storage == null || !storage.Ready )
					return;

				// ustawienia odczytu pliku
				var settings = Program.GLOBAL.DatafileSettings;
				settings.Storage = storage;
				settings.translateForm();

				if( settings.ShowDialog(this) != DialogResult.OK )
				{
#				if DEBUG
					Program.DecreaseLogIndent();
					Program.LogMessage( "** END ==================================================================== **" );
#				endif
					return;
				}
			}

			// otwórz okno przypisywania kolumn do pól
			this._generatorData = patdata;
			var reader = new DataReaderForm( this._generatorData, storage );

			if( reader.refreshAndOpen(this) != DialogResult.OK )
			{
#			if DEBUG
				Program.LogMessage( "Wczytywanie danych zostało anulowane." );
				Program.DecreaseLogIndent();
				Program.LogMessage( "** END ==================================================================== **" );
#			endif
				return;
			}

			// przypisz strumień
			this._preparedStream = reader.DataStorage;

			this.P_P3_Generator.Controls.Clear();
			this.TV_P3_PageList.Nodes.Clear();

			var checkedcols = reader.CheckedCols;
			var checkedfmt  = reader.CheckedFormat;
			var checkedhelp = "";

			// przygotuj dane do wyświetlenia
			for( int x = 0; x < this._preparedStream.RowsNumber; ++x )
			{
				// kopiuj format
				checkedhelp = checkedfmt;
				for( int y = 0; y < checkedcols.Count; ++y )
				{
					var row = this._preparedStream.Row[x][checkedcols[y]];

					// podmień wartości
					if( row == " " || row == "" || row == null )
						checkedhelp = checkedhelp.Replace( "#" + (y+1), "" );
					else
						checkedhelp = checkedhelp.Replace( "#" + (y+1), this._preparedStream.Row[x][checkedcols[y]] );
				}
				this.TV_P3_PageList.Nodes.Add( checkedhelp );
			}

			// ustaw indeks początkowy dla pola
			this._locked = true;
			this.CBX_P3_Scale.SelectedIndex = 2;
			this._locked = false;

			// generator musi ponownie przerysować szkic
			this._generatorSketch = false;

			// zaznacz pierwszy element
			if( this.TV_P3_PageList.Nodes.Count > 0 )
				this.TV_P3_PageList.SelectedNode = this.TV_P3_PageList.Nodes[0];
			
			// przejdź na strone z danymi
			this.TSMI_PrintPreview_Click( null, null );

			GC.Collect();

#		if DEBUG
			Program.LogMessage( "Wczytano dane do tworzenia podglądu wydruku." );
			Program.DecreaseLogIndent();
			Program.LogMessage( "** END ==================================================================== **" );
#		endif
		}

		/// <summary>
		/// Funkcja odświeża listę ostatnio otwieranych wzorów.
		/// Lista znajduje się w menu "Wzór" pod pozycją "Ostatnio otwierane".
		/// Funkcja dodaje do menu pozycje z możliwością kliknięcia i przejścia do wzoru.
		/// W przypadku gdy brak pozycji, dostęp do podmenu "Ostatnio otwierane" zostaje zablokowany.
		/// </summary>
		//* ============================================================================================================
		private void refreshRecentOpenList()
		{
#		if DEBUG
			Program.LogMessage( "Uzupełnianie listy ostatnio otwieranych wzorów." );
#		endif

			// usuń pozycje w menu
			while( this.TSMI_RecentOpen.DropDownItems.Count > 2 )
				this.TSMI_RecentOpen.DropDownItems.RemoveAt( 0 );

			var lpatterns = Settings.LastPatterns;

			// dodaj ostatnio używane wzory
			if( lpatterns.Count > 0 )
			{
				this.TSMI_RecentOpen.Enabled = true;
				for( int x = 0; x < lpatterns.Count; ++x )
				{
					this.TSMI_RecentOpen.DropDownItems.Insert( x, new ToolStripMenuItem((x+1) + ": " + lpatterns[x]) );
					this.TSMI_RecentOpen.DropDownItems[x].Click += new EventHandler( this.TSMI_PatternItem_Click );
				}
			}
			// jeżeli brak, wyłącz pole
			else
				this.TSMI_RecentOpen.Enabled = false;
			
#		if DEBUG
			Program.LogMessage( "Ilość wczytanych ostatnio otwieranych wzorów: " + lpatterns.Count + "." );
#		endif

			GC.Collect();
		}

		/// <summary>
		/// Odświeżanie listy wzorów w panelu głównym.
		/// Funkcja pobiera wszystkie wzory dostępne w programie i wyświetla na głównej stronie w postaci listy.
		/// Tworzy folder dla wzorów w przypadku gdy nie istnieje.
		/// Gdy wzór jest uszkodzony, jego pozycja w liście podświetlona zostaje na czerwono.
		/// </summary>
		//* ============================================================================================================
		private void refreshProjectList()
		{
#		if DEBUG
			Program.LogMessage( "Wczytywanie i uzupełnianie listy wzorów." );
#		endif

			// wyczyść wszystkie dane
			this.TV_P1_Patterns.Nodes.Clear();

			// utwórz folder gdy nie istnieje
			if( !Directory.Exists("patterns") )
				Directory.CreateDirectory( "patterns" );

			var patterns = PatternEditor.GetPatterns();

			foreach( var pattern in patterns )
			{
				var item = this.TV_P1_Patterns.Nodes.Add( pattern.Name );

				if( !pattern.HasConfigFile || pattern.Corrupted )
				{
#				if DEBUG
					if( !pattern.HasConfigFile )
						Program.LogMessage( "Wzór " + pattern.Name + " nie zawiera pliku konfiguracyjnego." );
					if( pattern.Corrupted )
						Program.LogMessage( "Wzór " + pattern.Name + " jest uszkodzony." );
#				endif
					item.ForeColor = Color.OrangeRed;
					continue;
				}
			}

#		if DEBUG
			Program.LogMessage( "Ilość wczytanych wzorów: " + patterns.Count + "." );
#		endif

			// sprawdź czy można włączyć przycisk eksportu wszystkich wzorów
			if( this.TV_P1_Patterns.Nodes.Count < 1 )
				this.TSMI_ExportAll.Enabled = false;
		}

		/// <summary>
		/// Sprawdza czy aktualizacja programu jest dostępna.
		/// Funkcja sprawdza czy aktualizacja programu jest dostępna oraz pobiera informacje
		/// o zmianach w programie, które nastąpiły od wersji początkowej.
		/// Funkcja wywoływana jest w osobnym wątku w trakcie działania programu.
		/// </summary>
		/// 
		/// <param name="sender">Obiekt wywołujący zdarzenie.</param>
		/// <param name="ev">Argumenty zdarzenia.</param>
		//* ============================================================================================================
		private void getProgramUpdates( object sender, DoWorkEventArgs ev )
		{
			var result = UpdateApp.CheckAvailability();
			if( result )
				UpdateApp.GetChangeLog();
		}

#endregion

#region EDYTOR - MENU POLA
		/// @cond EVENTS

		/// <summary>
		/// Akcja wywoływana podczas wciśnięcia pozycji usuwania pola.
		/// Funkcja usuwa zaznaczone pole i zwalnia jego zasoby.
		/// </summary>
		/// 
		/// <param name="sender">Obiekt wywołujący zdarzenie.</param>
		/// <param name="ev">Argumenty zdarzenia.</param>
		//* ============================================================================================================
		private void TSMI_RemoveField_Click( object sender, EventArgs ev )
		{
			if( this._activeField.BackImage != null )
				this._activeField.BackImage.Dispose();
			
#		if DEBUG
			Program.LogMessage( "Usuwanie wybranego pola." );
#		endif

			this._activePage.Controls.Remove( this._activeField );
			this.lockFieldLabels();
		}

		/// <summary>
		/// Akcja wywoływana podczas czyszczenia tła pola.
		/// Funkcja czyści tło zaznaczonego pola - usuwa przypisany obraz i ustawia kolor na "przezroczysty".
		/// </summary>
		/// 
		/// <param name="sender">Obiekt wywołujący zdarzenie.</param>
		/// <param name="ev">Argumenty zdarzenia.</param>
		//* ============================================================================================================
		private void TSMI_FieldClear_Click( object sender, EventArgs ev )
		{
			if( this._activeField.BackImage != null )
				this._activeField.BackImage.Dispose();

			this._activeField.BackImage = null;
			this._activeField.BackColor = Color.Transparent;

#		if DEBUG
			Program.LogMessage( "Czyszczenie tła wybranego pola." );
#		endif
		}
		
		/// <summary>
		/// Akcja wywoływana po kliknięciu prawym przyciskiem myszy na polu.
		/// Funkcja uzupełnia ustawienia dotyczące generowania w wyskakującym menu kontekstowym.
		/// </summary>
		/// 
		/// <param name="sender">Obiekt wywołujący zdarzenie.</param>
		/// <param name="ev">Argumenty zdarzenia.</param>
		//* ============================================================================================================
		private void CMS_Label_Opening( object sender, CancelEventArgs ev )
		{
#		if DEBUG
			Program.LogMessage( "Otwieranie menu kontekstowego pola." );
#		endif

			//this.plLabel_Click( this.icLabel.SourceControl, ev );
			var tag = (FieldExtraData)this._activeField.Tag;

			this._locked = true;

			// zaznacz lub odznacz elementy
			this.TSMI_DynamicImage.Checked  = tag.ImageFromDB;
			this.TSMI_DrawFieldFill.Checked = tag.PrintColor;
			this.TSMI_StaticImage.Checked   = tag.PrintImage;
			this.TSMI_DynamicText.Checked   = tag.TextFromDB;
			this.TSMI_StaticText.Checked    = tag.PrintText;
			this.TSMI_DrawBorder.Checked    = tag.PrintBorder;

			this._locked = false;
		}

#endregion

#region GENERATOR

		/// <summary>
		/// Akcja wywoływana po zmianie wyświetlanego elementu w liście stron.
		/// Funkcja odświeża widok generatora i wyświetla aktualnie zaznaczony na liście wiersz.
		/// </summary>
		/// 
		/// <param name="sender">Obiekt wywołujący zdarzenie.</param>
		/// <param name="ev">Argumenty zdarzenia.</param>
		//* ============================================================================================================
		private void TV_P3_PageList_AfterSelect( object sender, TreeViewEventArgs ev )
		{
			if( this.TV_P3_PageList.SelectedNode == null )
				return;

			this._locked = true;
			
#		if DEBUG
			Program.LogMessage( "Zmiana wzoru wyświetlanego w generatorze." );
#		endif

			// narysuj wzór
			if( !this._generatorSketch )
			{
				// pobierz ustawione powiększenie
				double val = 0.0;
				if( !Double.TryParse(this.CBX_P3_Scale.SelectedText, out val) )
					val = 100.0;

				PatternEditor.DrawSketch( this._generatorData, this.P_P3_Generator, val / 100.0 );
				
				// dodaj obsługę myszy (przewijanie obrazka)
				foreach( Panel panel in this.P_P3_Generator.Controls )
					panel.MouseDown += new MouseEventHandler( this.dpPreview_MouseDown );
				
				this._generatorSketch = true;
			}

			// uzupełnij danymi narysowany wzór
			PatternEditor.DrawRow( this.P_P3_Generator, this._preparedStream, ev.Node.Index );

			// ustaw aktualną stronę
			if( this._generatorPage == null )
			{
				this._generatorPage = (Panel)this.P_P3_Generator.Controls[0];
				if( this._generatorPage != null )
					((AlignedPage)this._generatorPage).checkLocation();
			}
			this._locked = false;
		}

		/// <summary>
		/// Akcja wywoływana po zmianie rozmiaru panelu wyświetlającego wiersz.
		/// Funkcja uruchamia akcję sprawdzającą położenie panelu wewnętrznego, aby był na środku.
		/// </summary>
		/// 
		/// <param name="sender">Obiekt wywołujący zdarzenie.</param>
		/// <param name="ev">Argumenty zdarzenia.</param>
		//* ============================================================================================================
		private void P_P3_Generator_Resize( object sender, EventArgs ev )
		{
			foreach( AlignedPage page in this.P_P3_Generator.Controls )
				page.checkLocation();
		}

		/// <summary>
		/// Akcja wywoływana po wciśnięciu przycisku myszy na panelu.
		/// Powoduje ustawienie skupienia na panelu, dzięki czemu zawartość może być przewijana kółkiem myszy.
		/// </summary>
		/// 
		/// <param name="sender">Obiekt wywołujący zdarzenie.</param>
		/// <param name="ev">Argumenty zdarzenia.</param>
		//* ============================================================================================================
		private void dpPreview_MouseDown( object sender, MouseEventArgs ev )
		{
			this.P_P3_Generator.Focus();
		}

#endregion

#region PASEK INFORMACYJNY - GENERATOR
		
		/// <summary>
		/// Akcja wywoływana po zmianie DPI dla generatora.
		/// Funkcja zmienia skalę rysowania wzoru (DPI) w podglądzie generatora.
		/// </summary>
		/// 
		/// <param name="sender">Obiekt wywołujący zdarzenie.</param>
		/// <param name="ev">Argumenty zdarzenia.</param>
		//* ============================================================================================================
		private void CBX_P3_Scale_SelectedIndexChanged( object sender, EventArgs ev )
		{
			if( this._locked )
				return;

			// próbuj zamienić string na int
			int scale = 0;
			try { scale = Convert.ToInt32(this.CBX_P3_Scale.Text); }
			catch
			{
				scale = 100;
				this.CBX_P3_Scale.Text = "100";
			}

			// nie można zmniejszyć więcej niż 50%
			if( scale < 50 )
			{
				this.CBX_P3_Scale.Text = "50";
				scale = 50;
			}

			// nie można powiększyć więcej niż 300%
			if( scale > 300 )
			{
				this.CBX_P3_Scale.Text = "300";
				scale = 300;
			}

			// zmień skale wzoru
			PatternEditor.ChangeScale( this._generatorData, this.P_P3_Generator, (double)scale / 100.0 );
			
#		if DEBUG
			Program.LogMessage( "Zmiana skali wyświetlanego wzoru na '" + this.CBX_P3_Scale.Text + "'." );
#		endif
		}
		
		/// <summary>
		/// Akcja wywoływana po zmianie wartości w kontrolce strony.
		/// Zmienia aktualnie wyświetlaną stronę wzoru w generatorze na tą o podanym indeksie.
		/// </summary>
		/// 
		/// <param name="sender">Obiekt wywołujący zdarzenie.</param>
		/// <param name="ev">Argumenty zdarzenia.</param>
		//* ============================================================================================================
		private void N_P3_Page_ValueChanged( object sender, EventArgs ev )
		{
			if( this.P_P3_Generator.Controls.Count < (int)this.N_P3_Page.Value )
				return;

			((AlignedPage)this.P_P3_Generator.Controls[(int)this.N_P3_Page.Value - 1]).Show();
			((AlignedPage)this.P_P3_Generator.Controls[(int)this.N_P3_Page.Value - 1]).checkLocation();
			this._generatorPage.Hide();
			this._generatorPage = (Panel)this.P_P3_Generator.Controls[(int)this.N_P3_Page.Value - 1];

#		if DEBUG
			Program.LogMessage( "Zmiana aktywnej strony na '" + this.N_P3_Page.Value + "'." );
#		endif
		}

		/// <summary>
		/// Akcja wywoływana po opuszczeniu skupienia z kontrolki do powiększania.
		/// Funkcja uruchamia akcję zmiany indeksu, dzięki czemu sprawdza czy wartość wpisana jest możliwa do uzyskania.
		/// </summary>
		/// 
		/// <param name="sender">Obiekt wywołujący zdarzenie.</param>
		/// <param name="ev">Argumenty zdarzenia.</param>
		//* ============================================================================================================
		private void CBX_P3_Scale_Leave( object sender, EventArgs ev )
		{
			this.CBX_P3_Scale_SelectedIndexChanged( sender, ev );
		}
		
		/// <summary>
		/// Akcja wywoływana po wciśnięciu klawisza w kontrolce związanej ze skalą.
		/// Funkcja po wciśnięciu klawisza ENTER wywołuje akcje sprawdzającą wpisaną wartość.
		/// </summary>
		/// 
		/// <param name="sender">Obiekt wywołujący zdarzenie.</param>
		/// <param name="ev">Argumenty zdarzenia.</param>
		//* ============================================================================================================
		private void CBX_P3_Scale_KeyDown( object sender, KeyEventArgs ev )
		{
			if( ev.KeyCode == Keys.Enter )
				this.CBX_P3_Scale_SelectedIndexChanged( sender, null );
		}

		/// <summary>
		/// Akcja wywoływana po wciśnięciu w przycisk skanowania.
		/// Funkcja skanuje wszystkie generowane strony w poszukiwaniu błędów.
		/// Błędem, który funkcja szuka, jest obcinanie tekstu poprzez zbyt mały wymiar pola w którym się znajduje.
		/// Wszystkie strony z błędami podświetlane są na czerwono.
		/// </summary>
		/// 
		/// <param name="sender">Obiekt wywołujący zdarzenie.</param>
		/// <param name="ev">Argumenty zdarzenia.</param>
		//* ============================================================================================================
		private void B_P3_SearchErrors_Click( object sender, EventArgs ev )
		{
			float width;
			var gfx = this.CreateGraphics();

#		if DEBUG
			Program.LogMessage( "Proces wyszukiwania błędów." );
#		endif

			// szukaj po rekordach
			for( int x = 0; x < this._preparedStream.RowsNumber; ++x )
			{
				// szukaj po stronach
				for( int y = 0; y < this._generatorData.Pages; ++y )
				{
					var page = this._generatorData.Page[y];

					// szukaj po polach
					for( int z = 0; z < page.Fields; ++z )
					{
						var field = page.Field[z];

						width = PatternEditor.GetDimensionScale( field.Bounds.Width, 1.0 );

						if( !field.Extra.PrintText && !(field.Extra.TextFromDB && field.Extra.Column > -1) )
							continue;

						// pobierz tekst do wypisania
						string text = field.Extra.PrintText
							? field.Name
							: this._preparedStream.Row[x][field.Extra.Column];

						var fsize  = (float)(field.FontSize * 1.0);
						var font   = new Font( field.FontName, fsize, field.FontStyle, GraphicsUnit.Point );
						var bounds = gfx.MeasureString( text, font );

						// błąd - brak przejscia kolejnej linii
						if( bounds.Width > width )
							((TreeNode)this.TV_P3_PageList.Nodes[x]).ForeColor = Color.OrangeRed;
					}
				}
			}
		}
		
		/// <summary>
		/// Akcja wywoływana po wciśnięciu w przycisk generowania PDF.
		/// Funkcja otwiera okno wyboru miejsca i nazwy pliku do zapisu.
		/// Po zamknięciu okna, generuje PDF odpowiednią klasą.
		/// </summary>
		/// 
		/// <param name="sender">Obiekt wywołujący zdarzenie.</param>
		/// <param name="ev">Argumenty zdarzenia.</param>
		//* ============================================================================================================
		private void B_P3_GeneratePDF_Click( object sender, EventArgs ev )
		{
			if( this._locked )
				return;

			// wybór pliku
			var save = Program.GLOBAL.SaveFile;
			save.Title      = Language.GetLine( "MessageNames", (int)LANGCODE.GMN_SELECTFILE );
			save.Filter     = "PDF|*.pdf";
			save.DefaultExt = ".pdf";
			
#		if DEBUG
			Program.LogMessage( "Wybór miejsca do zapisu generowanego pliku." );
#		endif

			if( save.ShowDialog(this) != DialogResult.OK )
				return;

			// generuj PDF
			try
			{
				var copies = (int)this.N_P3_Rows.Value;
				PatternEditor.GeneratePDF
				(
					this._preparedStream,
					this._generatorData,
					save.FileName,
					copies,
					this.CB_P3_CollatePages.Checked
				);
#			if DEBUG
				Program.LogMessage( "PDF został wygenerowany w: '" + save.FileName + "'." );
#			endif
			}
			catch( Exception ex )
			{
				// przechwyć błąd generowania
				Program.LogError
				(
					Language.GetLine( "PrintoutPreview", "Messages", (int)LANGCODE.I01_PRI_MES_PDFGENERR ),
					Language.GetLine( "MessageNames", (int)LANGCODE.GMN_GENERATOR ),
					false,
					ex,
					this
				);
			}
		}

#endregion

#region EDYTOR - PODGLĄD

		/// <summary>
		/// Akcja wywoływana podczas rysowania okna z podpowiedzią lub błędem.
		/// Umożliwia własne rysowanie okna.
		/// </summary>
		/// 
		/// <param name="sender">Obiekt wywołujący zdarzenie.</param>
		/// <param name="ev">Argumenty zdarzenia.</param>
		//* ============================================================================================================
		private void TP_Tooltip_Draw( object sender, DrawToolTipEventArgs ev )
		{
			ev.DrawBackground();
			ev.DrawBorder();
			ev.DrawText( TextFormatFlags.VerticalCenter );
		}

		/// <summary>
		/// Akcja wywoływana po zmianie rozmiaru panelu.
		/// Funkcja powoduje odświeżenie pozycji całego wzoru, dzięki czemu zawsze jest na środku.
		/// </summary>
		/// 
		/// <param name="sender">Obiekt wywołujący zdarzenie.</param>
		/// <param name="ev">Argumenty zdarzenia.</param>
		//* ============================================================================================================
		private void P_P2_Pattern_Resize( object sender, EventArgs ev )
		{
			foreach( AlignedPage page in this.P_P2_Pattern.Controls )
				page.checkLocation();
#		if DEBUG
			Program.LogMessage( "Zmiana rozmiaru panelu w edytorze wzorów - odświeżanie położenia." );
#		endif
		}

		/// <summary>
		/// Akcja wywoływana po naciśnięciu przycisku myszy na panelu.
		/// Funkcja powoduje ustawienie skupienia na panelu, dzięki czemu można go przewijać używając kółka myszy.
		/// </summary>
		/// 
		/// <param name="sender">Obiekt wywołujący zdarzenie.</param>
		/// <param name="ev">Argumenty zdarzenia.</param>
		//* ============================================================================================================
		private void P_P2_Pattern_MouseDown( object sender, MouseEventArgs ev )
		{
			this.P_P2_Pattern.Focus();
#		if DEBUG
			Program.LogMessage( "Przejęcie skupienia przez panel." );
#		endif
		}

		/// <summary>
		/// Akcja wywoływana po wciśnięciu przycisku myszy w pole wzoru w edytorze.
		/// Funkcja zmienia aktywne pole w edytorze i zapisuje podstawowe informacje o nim.
		/// Po kliknięciu wypełnia wszystkie informacje o polu i odblokowuje zablokowane kontrolki.
		/// </summary>
		/// 
		/// <param name="sender">Obiekt wywołujący zdarzenie.</param>
		/// <param name="ev">Argumenty zdarzenia.</param>
		//* ============================================================================================================
		private void L_P2_EditorField_MouseDown( object sender, MouseEventArgs ev )
		{
			if( ev.Button == MouseButtons.Left )
			{
				this._movingField = (PageField)sender;
				this._movingDiffX       = ev.X;
				this._movingDiffY       = ev.Y;
			}

			// aktualny obiekt
			this._activeField = (PageField)sender;
			
			// wypełnij pola i odblokuj je
			this.fillFieldLabels();
			this.unlockFieldLabels();

#		if DEBUG
			Program.LogMessage( "Zaznaczono pole w edytorze - możliwość przemieszczania pola." );
#		endif
		}

		/// <summary>
		/// Akcja wywoływana po opuszczeniu klawisza myszy z pola.
		/// Powoduje blokade przemieszczania pola - domyślnie pole można przemieszczać klikając w nie z przytrzymanym
		/// klawiszem CTRL poprzez poruszanie wskaźnikiem myszy.
		/// </summary>
		/// 
		/// <param name="sender">Obiekt wywołujący zdarzenie.</param>
		/// <param name="ev">Argumenty zdarzenia.</param>
		//* ============================================================================================================
		private void L_P2_EditorField_MouseUp( object sender, MouseEventArgs ev )
		{
			if( ev.Button != MouseButtons.Left )
				return;

			this._movingField = null;
			
#		if DEBUG
			Program.LogMessage( "Blokada przemieszczania pola poprzez ruch myszą." );
#		endif
		}

		/// <summary>
		/// Akcja wywoływana podczas ruchu myszą w wybranym polu.
		/// Funkcja porusza kontrolką tylko gdy naciśnięty jest przycisk CTRL i jest zaznaczone pole.
		/// </summary>
		/// 
		/// <param name="sender">Obiekt wywołujący zdarzenie.</param>
		/// <param name="ev">Argumenty zdarzenia.</param>
		//* ============================================================================================================
		private void L_P2_EditorField_MouseMove( object sender, MouseEventArgs ev )
		{
			if( this._movingField == null )
				return;

			// przemieszczać można tylko z wciśniętym klawiszem CTRL
			if( ModifierKeys != Keys.Control )
				return;

			// pobierz pole i przyleganie kontrolki
			var field = (PageField)sender;
			var align = (ContentAlignment)((FieldExtraData)field.Tag).PosAlign;

			// ustaw pozycję
			field.setPxLocation( field.Location.X + (ev.X - this._movingDiffX),
				field.Location.Y + (ev.Y - this._movingDiffY), align );
			var location = field.getPosByAlignPoint( align );

			this._locked = true;
			
			// odśwież pozycje w edytorze
			this.N_P2_PosX.Value = (decimal)location.X;
			this.N_P2_PosY.Value = (decimal)location.Y;

			this._locked = false;
		}

#endregion

#region EDYTOR - MENU STRONY

		/// <summary>
		/// Akcja wywoływana po kliknięciu prawym przyciskiem myszy na panelu.
		/// Funkcja uzupełnia ustawienia dotyczące generowania w wyskakującym menu kontekstowym.
		/// </summary>
		/// 
		/// <param name="sender">Obiekt wywołujący zdarzenie.</param>
		/// <param name="ev">Argumenty zdarzenia.</param>
		//* ============================================================================================================
		private void CMS_Page_Opening( object sender, CancelEventArgs ev )
		{
			var tag = (PageExtraData)this._activePage.Tag;

			this._locked = true;

			this.TSMI_PageDrawColor.Checked = tag.PrintColor;
			this.TSMI_PageDrawImage.Checked = tag.PrintImage;

			this._locked = false;
			
#		if DEBUG
			Program.LogMessage( "Otwarto menu strony." );
#		endif
		}

		/// <summary>
		/// Akcja wywoływana po kliknięciu w pozycję dodawania nowego pola.
		/// Funkcja tworzy nowe pole o domyślnych wartościach z napisem zależnym od aktualnego języka.
		/// Po utworzeniu dodaje do aktualnie wybranej strony na samą górę w pozycji 0, 0.
		/// </summary>
		/// 
		/// <param name="sender">Obiekt wywołujący zdarzenie.</param>
		/// <param name="ev">Argumenty zdarzenia.</param>
		//* ============================================================================================================
		private void TSMI_AddField_Click( object sender, EventArgs ev )
		{
			if( this._locked )
				return;

			var field = new PageField();

			// utwórz pole
			field.BorderColor   = Color.Black;
			field.DPIBorderSize = 0.3f;
			field.DPIBounds     = new Rectangle( 0, 0, 65, 10 );
			field.ForeColor     = Color.Black;
			field.Text          = Language.GetLine( "PatternEditor", "Labels", (int)LANGCODE.I01_EDI_LAB_NEWFIELD );
			field.TextAlign     = ContentAlignment.MiddleCenter;
			field.BackColor     = Color.Transparent;
			field.DPIFontSize   = 8.25;
			field.DPIPadding    = 0.3f;
			field.DPIScale      = (double)Convert.ToInt32( this.CBX_P2_Scale.Text ) / 100.0;
			field.Margin        = new Padding(0);
			field.Padding       = new Padding(0);
			field.Location      = new Point( 0, 0 );
			
			field.ApplyTextMargin = false;
			field.TextMargin      = new PointF( 2.0f, 2.0f );

			field.setParentBounds( this._editingPageSize.Width, this._editingPageSize.Height );

			// przypisz zdarzenia
			field.ContextMenuStrip = this.CMS_Label;
			field.MouseDown += new MouseEventHandler( this.L_P2_EditorField_MouseDown );
			field.MouseUp   += new MouseEventHandler( this.L_P2_EditorField_MouseUp );
			field.MouseMove += new MouseEventHandler( this.L_P2_EditorField_MouseMove );

			// informacje dodatkowe
			var field_extra = new FieldExtraData();
			field_extra.Column        = -1;
			field_extra.ImageFromDB = false;
			field_extra.PosAlign     = (int)ContentAlignment.TopLeft;
			field_extra.PrintBorder  = false;
			field_extra.PrintColor   = false;
			field_extra.PrintImage   = false;
			field_extra.PrintText    = false;
			field_extra.TextFromDB  = false;

			field.Tag = field_extra;

			// odblokuj pola i wypełnij domyślnymi danymi
			this._activeField = field;
			this.fillFieldLabels();
			this.unlockFieldLabels();

			// dodaj pole do strony
			this._activePage.Controls.Add( field );

#		if DEBUG
			Program.LogMessage( "Dodano nowe pole do strony." );
#		endif
		}
		
		/// <summary>
		/// Akcja wywoływana po kliknięciu w przycisk usuwania pól.
		/// Funkcja usuwa wszystkie pola z obecnie edytowanej strony wzoru.
		/// </summary>
		/// 
		/// <param name="sender">Obiekt wywołujący zdarzenie.</param>
		/// <param name="ev">Argumenty zdarzenia.</param>
		//* ============================================================================================================
		private void TSMI_RemoveAll_Click( object sender, EventArgs ev )
		{
			if( this._locked )
				return;

			// ustaw obrazki jako nieaktywne
			for( int x = 0; x < this._activePage.Controls.Count; ++x )
				if( this._activePage.Controls[x].BackgroundImage != null )
					this._activePage.Controls[x].BackgroundImage.Dispose();

			this._activePage.Controls.Clear();
			this.lockFieldLabels();

			// wymuś uruchomienie zbieracza śmieci
			GC.Collect();
			
#		if DEBUG
			Program.LogMessage( "Usunięto wszystkie kontrolki z aktywnej strony." );
#		endif
		}
		
		/// <summary>
		/// Akcja wywoływana po kliknięciu w przycisk czyszczenia strony.
		/// Funkcja usuwa przypisany do strony kolor i obraz, dzięki czemu wraca do stanu początkowego.
		/// </summary>
		/// 
		/// <param name="sender">Obiekt wywołujący zdarzenie.</param>
		/// <param name="ev">Argumenty zdarzenia.</param>
		//* ============================================================================================================
		private void TSMI_PageClear_Click( object sender, EventArgs ev )
		{
			if( this._locked )
				return;

			if( this._activePage.BackgroundImage != null )
				this._activePage.BackgroundImage.Dispose();

			this._activePage.BackgroundImage = null;
			this._activePage.BackColor = SystemColors.Window;

			// resetuj informacje w kontrolkach
			var color = SystemColors.Window;
			this.TB_P2_PageColor.Text = "#" + color.R.ToString("X2") +
				color.G.ToString("X2") + color.B.ToString("X2");
			this.TB_P2_PageImage.Text = "";

			// wymuś uruchomienie zbieracza śmieci
			GC.Collect();
			
#		if DEBUG
			Program.LogMessage( "Aktywna strona została wyczyszczona z koloru i obrazu." );
#		endif
		}

		/// <summary>
		/// Akcja wywoływana po kliknięciu w pozycję dodawania nowej strony.
		/// Funkcja tworzy nowy panel, traktując go jako nową stronę wzoru, uzupełniając domyślnymi danymi.
		/// Panel dodawany jest do panelu głównego, po czym zmieniane są informacje dla wzoru o ilości stron.
		/// </summary>
		/// 
		/// <param name="sender">Obiekt wywołujący zdarzenie.</param>
		/// <param name="ev">Argumenty zdarzenia.</param>
		//* ============================================================================================================
		private void TSMI_AddPage_Click( object sender, EventArgs ev )
		{
			var page = new AlignedPage();
			page.Align = 1;

			var scale = (double)Convert.ToInt32(this.CBX_P2_Scale.Text) / 100.0;

			// zablokuj operacje odświeżania
			this._locked = true;

			// ustawienia panelu
			page.BackColor              = SystemColors.Window;
			page.BackgroundImageLayout	= ImageLayout.Stretch;

			page.Location = new Point( 0, 0 );
			page.Margin   = new Padding( 0, 0, 0, 0 );
			page.Size     = PatternEditor.GetDPIPageSize( this._editingData.Size, scale );

			// informacje dodatkowe
			var page_extra = new PageExtraData();

			page_extra.ImagePath  = "";
			page_extra.PrintColor = false;
			page_extra.PrintImage = false;

			page.Tag = page_extra;

			page.ContextMenuStrip = CMS_Page;
			page.MouseDown       += new MouseEventHandler( this.P_P2_Pattern_MouseDown );

			// zwiększ licznik stron
			++this._editingPages;

			page.Hide();

			// dodaj panel do kontenera
			this.P_P2_Pattern.Controls.Add( page );

			page.checkLocation();
			page.Show();

			// odblokuj odświeżanie
			this._locked = false;

			// zwiększ maksimum i ustaw aktualną stronę
			this.N_P2_Page.Maximum = this._editingPages + 1;
			this.N_P2_Page.Value	= this._editingPages + 1;

			// zablokuj pola
			this.lockFieldLabels();

#		if DEBUG
			Program.LogMessage( "Dodano nową stronę do wczytanego wzoru." );
#		endif
		}

		/// <summary>
		/// Akcja wywoływana po kliknięciu w przycisk usuwania strony.
		/// Funkcja sprawdza czy wzór zawiera tylko jedną stronę - jeżeli tak, nie usuwa jej i wyświetla ostrzeżenie.
		/// W przeciwnym wypadku usuwa aktywną stronę ze wzoru i przełacza na stronę znajdującej się zaraz po usuniętej.
		/// </summary>
		/// 
		/// <param name="sender">Obiekt wywołujący zdarzenie.</param>
		/// <param name="ev">Argumenty zdarzenia.</param>
		//* ============================================================================================================
		private void TSMI_RemovePage_Click( object sender, EventArgs ev )
		{
			// nie można usunąć strony gdy jest tylko jedna
			if( this._editingPages < 1 )
			{
				Program.LogWarning
				(
					Language.GetLine( "PatternEditor", "Messages", (int)LANGCODE.I01_EDI_MES_DREMOPAGE ),
					Language.GetLine( "MessageNames", (int)LANGCODE.GMN_PATTERNEDITOR ),
					this
				);
				return;
			}

			this._locked = true;

			// zmniejsz licznik stron
			--this._editingPages;

			// wyczyść stronę
			this.TSMI_RemoveAll_Click( null, null );
			this.TSMI_PageClear_Click( null, null );

			// usuń stronę
			this.P_P2_Pattern.Controls.RemoveAt( this._editingPageID );

			// aktualizuj identyfikator
			this._editingPageID = this._editingPageID > this._editingPages
				? this._editingPages
				: this._editingPageID < 1
					? 0
					: this._editingPageID - 1;

			// ustaw nowe wartości zmiennych i pokaż stronę
			this._activePage = (Panel)this.P_P2_Pattern.Controls[this._editingPageID];
			this._activePage.Show();
			this.N_P2_Page.Value = this._editingPageID + 1;
			this.N_P2_Page.Maximum = this._editingPages + 1;
			
			// zablokuj pola
			this.lockFieldLabels();

			this._locked = false;

#		if DEBUG
			Program.LogMessage( "Wybrana strona została usunięta." );
#		endif
		}

#endregion

#region EDYTOR - POLE

		/// <summary>
		/// Akcja wywoływana po zmianie kontrolki z formatem na inną kontrolkę.
		/// Pozwala na schowanie wyświetlanego dymku z podpowiedzią.
		/// </summary>
		/// 
		/// <param name="sender">Obiekt wywołujący zdarzenie.</param>
		/// <param name="ev">Argumenty zdarzenia.</param>
		//* ============================================================================================================
		private void TB_P2_LabelName_Leave( object sender, EventArgs ev )
		{
			// ukryj dymek
			if( this._tooltipShow )
			{
				this.TP_Tooltip.Hide( this.TB_P2_LabelName );
				this._tooltipShow = false;
			}
#		if DEBUG
			Program.LogMessage( "Zmieniono tekst w zaznaczonym polu na '" + this.TB_P2_LabelName.Text + "'." );
#		endif
		}

		/// <summary>
		/// Akcja wywoływana po zmianie tekstu w kontrolce.
		/// Zamienia tekst w aktualnie zaznaczonym polu.
		/// </summary>
		/// 
		/// <param name="sender">Obiekt wywołujący zdarzenie.</param>
		/// <param name="ev">Argumenty zdarzenia.</param>
		//* ============================================================================================================
		private void TB_P2_LabelName_TextChanged( object sender, EventArgs ev )
		{
			if( this._locked )
				return;
			this._activeField.Text = this.TB_P2_LabelName.Text;
		}

		/// <summary>
		/// Akcja wywoływana po naciśnięciu klawisza w kontrolce z formatem.
		/// W przypadku gdy podany zostanie błędny znak, wyświetony zostanie dymek informacyjny.
		/// </summary>
		/// 
		/// <param name="sender">Obiekt wywołujący zdarzenie.</param>
		/// <param name="ev">Argumenty zdarzenia.</param>
		//* ============================================================================================================
		private void TB_P2_LabelName_KeyPress( object sender, KeyPressEventArgs ev )
		{
			//if( ev.KeyChar == 8 || ModifierKeys == Keys.Control )
				//return;
			
			// sprawdź poprawność danych
			//var lang_chars   = Language.GetLines( "Locale" );
			//var locale_chars = lang_chars[(int)LANGCODE.GLO_BIGCHARS] + lang_chars[(int)LANGCODE.GLO_SMALLCHARS];
			//var regex        = new Regex( @"^[0-9a-zA-Z" + locale_chars + @" \-+_]+$" );

			//if( !regex.IsMatch(ev.KeyChar.ToString()) )
			//{
				// pokaż dymek
				//if( !this._tooltipShow )
				//{
					//this.TP_Tooltip.Show
					//(
						//Language.GetLine( "PatternEditor", "Messages", (int)LANGCODE.I01_EDI_MES_AVAILCHARS ),
						//this.TB_P2_LabelName,
						//new Point( 0, this.TB_P2_LabelName.Height + 2 )
					//);
					//this._tooltipShow = true;
				//}
				//ev.Handled = true;
				//System.Media.SystemSounds.Beep.Play();
				//return;
			//}

			// ukryj dymek
			//if( this._tooltipShow )
			//{
				//this.TP_Tooltip.Hide( this.TB_P2_LabelName );
				//this._tooltipShow = false;
			//}
		}

		/// <summary>
		/// Akcja wywoływana po zmianie wartości w kontrolce.
		/// Pozwala na zmianę pozycji aktualnie zaznaczonego pola względem osi X.
		/// </summary>
		/// 
		/// <param name="sender">Obiekt wywołujący zdarzenie.</param>
		/// <param name="ev">Argumenty zdarzenia.</param>
		//* ============================================================================================================
		private void N_P2_PosX_ValueChanged( object sender, EventArgs ev )
		{
			if( this._locked )
				return;

			var align = (ContentAlignment)((FieldExtraData)this._activeField.Tag).PosAlign;
			this._activeField.setPosXByAlignPoint( (float)this.N_P2_PosX.Value, align );

#		if DEBUG
			Program.LogMessage( "Zmiana pozycji pola względem osi X w milimetrach na '" +
				this.N_P2_PosX.Value + "'." );
#		endif
		}
		
		/// <summary>
		/// Akcja wywoływana po zmianie wartości w kontrolce.
		/// Pozwala na zmianę pozycji aktualnie zaznaczonego pola względem osi Y.
		/// </summary>
		/// 
		/// <param name="sender">Obiekt wywołujący zdarzenie.</param>
		/// <param name="ev">Argumenty zdarzenia.</param>
		//* ============================================================================================================
		private void N_P2_PosY_ValueChanged( object sender, EventArgs ev )
		{
			if( this._locked )
				return;

			var align = (ContentAlignment)((FieldExtraData)this._activeField.Tag).PosAlign;
			this._activeField.setPosYByAlignPoint( (float)this.N_P2_PosY.Value, align );
			
#		if DEBUG
			Program.LogMessage( "Zmiana pozycji pola względem osi Y w milimetrach na '" +
				this.N_P2_PosY.Value + "'." );
#		endif
		}
		
		/// <summary>
		/// Akcja wywoływana po zmianie wartości w kontrolce.
		/// Pozwala na zmianę szerokości aktualnie zaznaczonego pola.
		/// </summary>
		/// 
		/// <param name="sender">Obiekt wywołujący zdarzenie.</param>
		/// <param name="ev">Argumenty zdarzenia.</param>
		//* ============================================================================================================
		private void N_P2_Width_ValueChanged( object sender, EventArgs ev )
		{
			if( this._locked )
				return;

			// szerokość
			var new_width = new RectangleF( -1, -1, (float)this.N_P2_Width.Value, -1 );
			this._activeField.DPIBounds = new_width;

			this._locked = true;
			
			// pobierz informacje o przypięciu kontrolki do boku i oblicz nowe pozycje
			var align = (ContentAlignment)((FieldExtraData)this._activeField.Tag).PosAlign;
			var pos   = this._activeField.getPosByAlignPoint( align );

			this.N_P2_PosX.Value = (decimal)pos.X;
			this.N_P2_PosY.Value = (decimal)pos.Y;

			this._locked = false;
	
#		if DEBUG
			Program.LogMessage( "Zmiana szerokości pola w milimetrach na '" +
				this.N_P2_Width.Value + "'." );
#		endif
		}
		
		/// <summary>
		/// Akcja wywoływana po zmianie wartości w kontrolce.
		/// Pozwala na zmianę wysokości aktualnie zaznaczonego pola.
		/// </summary>
		/// 
		/// <param name="sender">Obiekt wywołujący zdarzenie.</param>
		/// <param name="ev">Argumenty zdarzenia.</param>
		//* ============================================================================================================
		private void N_P2_Height_ValueChanged( object sender, EventArgs ev )
		{
			if( this._locked )
				return;

			// wysokość
			var new_height = new RectangleF( -1, -1, -1, (float)this.N_P2_Height.Value );
			this._activeField.DPIBounds = new_height;

			this._locked = true;
			
			// pobierz informacje o przypięciu kontrolki do boku i oblicz nową pozycję
			var align = (ContentAlignment)((FieldExtraData)this._activeField.Tag).PosAlign;
			var pos   = this._activeField.getPosByAlignPoint( align );

			this.N_P2_PosX.Value = (decimal)pos.X;
			this.N_P2_PosY.Value = (decimal)pos.Y;

			this._locked = false;
			
#		if DEBUG
			Program.LogMessage( "Zmiana wysokości pola w milimetrach na '" +
				this.N_P2_Height.Value + "'." );
#		endif
		}

		/// <summary>
		/// Akcja wykonywana po kliknięciu w przycisk wyboru koloru ramki.
		/// Zmienia aktualny kolor ramki zaznaczonego pola w edytorze.
		/// Wyświetla odpowiedni formularz wyboru koloru udostępniany przez system.
		/// </summary>
		/// 
		/// <param name="sender">Obiekt wywołujący zdarzenie.</param>
		/// <param name="ev">Argumenty zdarzenia.</param>
		//* ============================================================================================================
		private void B_P2_BorderColor_Click( object sender, EventArgs ev )
		{
			if( this._locked )
				return;
			
			if( this.CD_ColorDialog.ShowDialog(this) != DialogResult.OK )
				return;
			var color = this.CD_ColorDialog.Color;

			// zmień kolor ramki
			this._activeField.BorderColor = color;
			this.TB_P2_BorderColor.Text = "#" + color.R.ToString("X2") +
				color.G.ToString("X2") + color.B.ToString("X2");

			// odśwież kontrolke
			this._activeField.Refresh();
			
#		if DEBUG
			Program.LogMessage( "Zmiana koloru ramy na '" + this.TB_P2_BorderColor.Text + "'." );
#		endif
		}
		
		/// <summary>
		/// Akcja wykonywana po kliknięciu w przycisk wyboru koloru ramki.
		/// Zmienia aktualny kolor tła pola zaznaczonego w edytorze.
		/// Wyświetla odpowiedni formularz wyboru koloru udostępniany przez system.
		/// </summary>
		/// 
		/// <param name="sender">Obiekt wywołujący zdarzenie.</param>
		/// <param name="ev">Argumenty zdarzenia.</param>
		//* ============================================================================================================
		private void B_P2_BackColor_Click( object sender, EventArgs ev )
		{
			if( this._locked )
				return;

			if( this.CD_ColorDialog.ShowDialog(this) != DialogResult.OK )
				return;
			var color = this.CD_ColorDialog.Color;

			// zmień kolor tła
			this._activeField.BackColor = color;
			this.TB_P2_BackColor.Text = "#" + color.R.ToString("X2") +
				color.G.ToString("X2") + color.B.ToString("X2");
			
			// odśwież kontrolke
			this._activeField.Refresh();

#		if DEBUG
			Program.LogMessage( "Zmiana koloru tła kontrolki na '" + this.TB_P2_BackColor.Text + "'." );
#		endif
		}
		
		/// <summary>
		/// Akcja wykonywana po kliknięciu w przycisk wyboru czcionki.
		/// Zmienia aktualną czcionkę używaną w generowaniu napisu na polu.
		/// Wyświetla odpowiedni formularz wyboru czcionki udostępniany przez system.
		/// Niestety systemowy formularz posiada problem z wyborem czcionek OpenType (otf), więc możliwy jest tylko
		/// wybór czcionek TrueType (ttf).
		/// </summary>
		/// 
		/// <param name="sender">Obiekt wywołujący zdarzenie.</param>
		/// <param name="ev">Argumenty zdarzenia.</param>
		//* ============================================================================================================
		private void B_P2_FontName_Click( object sender, EventArgs ev )
		{
			if( this._locked )
				return;
			
#		if DEBUG
			Program.LogMessage( "Wybór czcionki...." );
#		endif

			if( this.FD_FontDialog.ShowDialog(this) != DialogResult.OK )
				return;

			this._activeField.Font        = this.FD_FontDialog.Font;
			this._activeField.DPIFontSize = this.FD_FontDialog.Font.SizeInPoints;

			var font = this._activeField.Font;
			var dets = (font.Bold ? "B" : "") + (font.Italic ? "I" : "") +
				(font.Strikeout ? "S" : "") + (font.Underline ? "U" : "");

			// odśwież dane na temat czcionki...
			this.TB_P2_FontName.Text = font.Name + ", " + font.SizeInPoints + "pt " + dets;

			// odśwież kontrolke
			this._activeField.Refresh();
			
#		if DEBUG
			Program.LogMessage( "Zmiana czcionki na '" + font.Name + "'." );
#		endif
		}
		
		/// <summary>
		/// Akcja wywoływana po kliknięciu w przycisk zmiany koloru czcionki.
		/// Wyświetla odpowiedni formularz wyboru koloru udostępniany przez system.
		/// Pozwala na zmianę koloru czcionki aktywnego pola w edytorze.
		/// </summary>
		/// 
		/// <param name="sender">Obiekt wywołujący zdarzenie.</param>
		/// <param name="ev">Argumenty zdarzenia.</param>
		//* ============================================================================================================
		private void B_P2_FontColor_Click( object sender, EventArgs ev )
		{
			if( this._locked )
				return;

			if( this.CD_ColorDialog.ShowDialog(this) != DialogResult.OK )
				return;
			var color = this.CD_ColorDialog.Color;

			// zmień kolor czcionki
			this._activeField.ForeColor = color;
			this.TB_P2_FontColor.Text = "#" + color.R.ToString("X2") +
				color.G.ToString("X2") + color.B.ToString("X2");

			// odśwież kontrolke
			this._activeField.Refresh();

#		if DEBUG
			Program.LogMessage( "Zmiana koloru czcionki na '" + this.TB_P2_FontColor.Text + "'." );
#		endif
		}
		
		/// <summary>
		/// Akcja wywoływana przy kliknięciu w przycisk zmiany obrazu.
		/// Pozwala na zmianę obrazu wyświetlanego w zaznaczonym polu w edytorze.
		/// </summary>
		/// 
		/// <param name="sender">Obiekt wywołujący zdarzenie.</param>
		/// <param name="ev">Argumenty zdarzenia.</param>
		//* ============================================================================================================
		private void B_P2_BackImage_Click( object sender, EventArgs ev )
		{
			// okno wyboru pliku
			if( this._locked )
				return;

			// wybór pliku
			var select = Program.GLOBAL.SelectFile;
			select.Title  = Language.GetLine( "MessageNames", (int)LANGCODE.GMN_SELECTFILE );
			select.Filter = "JPEG (*.jpg)|*.jpg|PNG (*.png)|*.png|BMP (*.bmp)|*.bmp";

			if( select.ShowDialog(this) != DialogResult.OK )
				return;

			if( this._activeField.BackImage != null )
				this._activeField.BackImage.Dispose();

			// zmień obraz pola
			this._activeField.BackImage = Image.FromFile( select.FileName );
			this._activeField.BackColor = Color.Transparent;

			// ustaw ścieżkę do obrazu w polu tekstowym
			this.TB_P2_BackImage.Text = select.FileName;

			// odśwież kontrolke
			this._activeField.Refresh();

#		if DEBUG
			Program.LogMessage( "Zmiana obrazka przypisanego do pola na '" + select.FileName + "'." );
#		endif
		}
		
		/// <summary>
		/// Akcja wywoływana po zmianie wartości w polu wyboru.
		/// Pozwala na zmianę położenia tekstu wyświetlanego w aktywnym polu.
		/// </summary>
		/// 
		/// <param name="sender">Obiekt wywołujący zdarzenie.</param>
		/// <param name="ev">Argumenty zdarzenia.</param>
		//* ============================================================================================================
		private void CBX_P2_TextPosition_SelectedIndexChanged( object sender, EventArgs ev )
		{
			if( this._locked )
				return;
			this._activeField.setTextAlignment( this.CBX_P2_TextPosition.SelectedIndex );
			
#		if DEBUG
			Program.LogMessage( "Zmiana pozycji przylegania tekstu." );
#		endif
		}

		/// <summary>
		/// Akcja wywoływana po zmianie wartości w polu numerycznym.
		/// Pozwala na zmianę wielkości wcięcia dla aktywnego pola w edytorze.
		/// </summary>
		/// 
		/// <param name="sender">Obiekt wywołujący zdarzenie.</param>
		/// <param name="ev">Argumenty zdarzenia.</param>
		//* ============================================================================================================
		private void N_P2_Padding_ValueChanged( object sender, EventArgs ev )
		{
			if( this._locked )
				return;
			this._activeField.DPIPadding = (float)this.N_P2_Padding.Value;

#		if DEBUG
			Program.LogMessage( "Zmiana wcięcia dla tekstu (margines wewnętrzny) na '" + this.N_P2_Padding.Value + "'." );
#		endif
		}
		
		/// <summary>
		/// Akcja wywoływana po zmianie wartości w polu wyboru.
		/// Pozwala na proste transformacje tekstu aktywnego pola w edytorze.
		/// </summary>
		/// 
		/// <param name="sender">Obiekt wywołujący zdarzenie.</param>
		/// <param name="ev">Argumenty zdarzenia.</param>
		//* ============================================================================================================
		private void CBX_P2_TextTransform_SelectedIndexChanged( object sender, EventArgs ev )
		{
			if( this._locked )
				return;
			this._activeField.TextTransform = this.CBX_P2_TextTransform.SelectedIndex;
			
#		if DEBUG
			Program.LogMessage( "Zmiana transformacji tekstu dla zaznaczonego pola." );
#		endif
		}
		
		/// <summary>
		/// Akcja wywoływana po zmianie wartości w polu numerycznym.
		/// Pozwala na zmianę wielkości ramki obejmującej zaznaczone pole.
		/// </summary>
		/// 
		/// <param name="sender">Obiekt wywołujący zdarzenie.</param>
		/// <param name="ev">Argumenty zdarzenia.</param>
		//* ============================================================================================================
		private void N_P2_BorderSize_ValueChanged( object sender, EventArgs ev )
		{
			if( this._locked )
				return;
			this._activeField.DPIBorderSize = (float)this.N_P2_BorderSize.Value;

			// odśwież kontrolke
			this._activeField.Refresh();
			
#		if DEBUG
			Program.LogMessage( "Zmiana rozmiaru ramy pola na '" + this.N_P2_BorderSize.Value + "'." );
#		endif
		}

#endregion

#region EDYTOR - SZCZEGÓŁY POLA

		/// <summary>
		/// Akcja wywoływana przy zmianie zaznaczenia pola generowania ramki.
		/// Powoduje zmianę w generowaniu pola na PDF w postaci wyświetlania utworzonej w edytorze ramki wokół pola.
		/// </summary>
		/// 
		/// <param name="sender">Obiekt wywołujący zdarzenie.</param>
		/// <param name="ev">Argumenty zdarzenia.</param>
		//* ============================================================================================================
		private void CB_P2_DrawBorder_CheckedChanged( object sender, EventArgs ev )
		{
			if( this._locked )
				return;

			// sprawdź czy obiekt został zaznaczony czy nie
			var ischecked = sender is CheckBox
				? ((CheckBox)sender).Checked
				: ((ToolStripMenuItem)sender).Checked;

			this._locked = true;
			((FieldExtraData)this._activeField.Tag).PrintBorder = ischecked;
			this.CB_P2_DrawBorder.Checked = ischecked;
			this.TSMI_DrawBorder.Checked  = ischecked;
			this._locked = false;

#		if DEBUG
			Program.LogMessage( "Generowanie ramki dla pola w PDF: " + (ischecked ? "TAK" : "NIE") + "." );
#		endif
		}

		/// <summary>
		/// Akcja wywoływana przy zmianie zaznaczenia pola generowania koloru.
		/// Powoduje zmianę w generowaniu pola na PDF w postaci wyświetlania koloru przypisanego do pola.
		/// </summary>
		/// 
		/// <param name="sender">Obiekt wywołujący zdarzenie.</param>
		/// <param name="ev">Argumenty zdarzenia.</param>
		//* ============================================================================================================
		private void CB_P2_DrawColor_CheckedChanged( object sender, EventArgs ev )
		{
			if( this._locked )
				return;

			// pobierz wartości
			var ischecked = sender is CheckBox
				? ((CheckBox)sender).Checked
				: ((ToolStripMenuItem)sender).Checked;
			var tag = (FieldExtraData)this._activeField.Tag;

			// przypisz do pola
			tag.ImageFromDB = false;
			tag.PrintImage   = false;
			tag.PrintColor   = ischecked;

			this._locked = true;

			this.CB_P2_DynamicImage.Checked = false;
			this.CB_P2_StaticImage.Checked  = false;
			this.CB_P2_DrawColor.Checked    = ischecked;
			this.TSMI_DynamicImage.Checked  = false;
			this.TSMI_StaticImage.Checked   = false;
			this.TSMI_DrawFieldFill.Checked = ischecked;

			this._locked = false;

#		if DEBUG
			Program.LogMessage( "Generowanie koloru dla pola w PDF: " + (ischecked ? "TAK" : "NIE") + "." );
#		endif
		}

		/// <summary>
		/// Akcja wywoływana przy zmianie zaznaczenia pola generowania obrazu.
		/// Powoduje zmianę w generowaniu pola na PDF w postaci wyświetlania obrazu przypisanego do pola.
		/// </summary>
		/// 
		/// <param name="sender">Obiekt wywołujący zdarzenie.</param>
		/// <param name="ev">Argumenty zdarzenia.</param>
		//* ============================================================================================================
		private void CB_P2_StaticImage_CheckedChanged( object sender, EventArgs ev )
		{
			if( this._locked )
				return;

			// pobierz wartości
			var is_check = this.CB_P2_StaticImage.Checked;
			var tag = (FieldExtraData)this._activeField.Tag;

			// przypisz do pola
			tag.ImageFromDB = false;
			tag.PrintImage   = is_check;

			this._locked = true;

			this.CB_P2_DynamicImage.Checked  = false;
			this.CB_P2_DrawColor.Checked = false;

			this._locked = false;

#		if DEBUG
			Program.LogMessage( "Generowanie obrazu dla pola w PDF: " +
				(this.CB_P2_StaticImage.Checked ? "TAK" : "NIE") + "." );
#		endif
		}

		/// <summary>
		/// Akcja wywoływana przy zmianie zaznaczenia pola generowania tekstu.
		/// Powoduje zmianę w generowaniu pola na PDF w postaci wyświetlania tekstu statycznego przypisanego do pola.
		/// </summary>
		/// 
		/// <param name="sender">Obiekt wywołujący zdarzenie.</param>
		/// <param name="ev">Argumenty zdarzenia.</param>
		//* ============================================================================================================
		private void CB_P2_StaticText_CheckedChanged( object sender, EventArgs ev )
		{
			if( this._locked )
				return;

			// pobierz wartości
			var ischecked = sender is CheckBox
				? ((CheckBox)sender).Checked
				: ((ToolStripMenuItem)sender).Checked;
			var tag = (FieldExtraData)this._activeField.Tag;

			// przypisz do pola
			tag.TextFromDB = false;
			tag.PrintText   = ischecked;

			this._locked = true;
			this.CB_P2_DynamicText.Checked = false;
			this.CB_P2_StaticText.Checked  = ischecked;
			this.TSMI_DynamicText.Checked  = false;
			this.TSMI_StaticText.Checked   = ischecked;
			this._locked = false;

#		if DEBUG
			Program.LogMessage( "Generowanie tekstu statycznego dla pola w PDF: " +
				(ischecked ? "TAK" : "NIE") + "." );
#		endif
		}

		/// <summary>
		/// Akcja wywoływana przy zmianie zaznaczenia pola generowania tekstu dynamicznego.
		/// Powoduje zmianę w generowaniu pola na PDF w postaci wyświetlania tekstu dynamicznego przypisanego do pola.
		/// </summary>
		/// 
		/// <param name="sender">Obiekt wywołujący zdarzenie.</param>
		/// <param name="ev">Argumenty zdarzenia.</param>
		//* ============================================================================================================
		private void CB_P2_DynamicText_CheckedChanged( object sender, EventArgs ev )
		{
			if( this._locked )
				return;

			// pobierz wartości
			var ischecked = sender is CheckBox
				? ((CheckBox)sender).Checked
				: ((ToolStripMenuItem)sender).Checked;
			var tag = (FieldExtraData)this._activeField.Tag;

			// przypisz do pola
			tag.TextFromDB  = ischecked;
			tag.ImageFromDB = false;
			tag.PrintText    = false;

			this._locked = true;

			// przełącz
			this.CB_P2_StaticText.Checked   = false;
			this.CB_P2_DynamicImage.Checked = false;
			this.CB_P2_DynamicText.Checked  = ischecked;
			this.TSMI_StaticText.Checked    = false;
			this.TSMI_DynamicImage.Checked  = false;
			this.TSMI_DynamicText.Checked   = ischecked;

			this._locked = false;

#		if DEBUG
			Program.LogMessage( "Generowanie tekstu dynamicznego dla pola w PDF: " +
				(ischecked ? "TAK" : "NIE") + "." );
#		endif
		}

		/// <summary>
		/// Akcja wywoływana po zmianie zaznaczenia punktu zaczepienia.
		/// Punkt zaczepienia jest punktem, od którego obliczane są granice pol podawane w edytorze (x, y, w, h).
		/// Funkcja zmienia punkt zaczepienia wraz ze zmianą wartości pozycji pola przeliczonej względem punktu.
		/// </summary>
		/// 
		/// <param name="sender">Obiekt wywołujący zdarzenie.</param>
		/// <param name="ev">Argumenty zdarzenia.</param>
		//* ============================================================================================================
		private void CBX_P2_StickPoint_SelectedIndexChanged( object sender, EventArgs ev )
		{
			if( this._locked )
				return;

			// dobierz odpowiedni tryb przylegania
			var align = ContentAlignment.TopLeft;
			switch( this.CBX_P2_StickPoint.SelectedIndex )
			{
				case 0: align = ContentAlignment.TopLeft;     break;
				case 1: align = ContentAlignment.TopRight;    break;
				case 2: align = ContentAlignment.BottomLeft;  break;
				case 4: align = ContentAlignment.BottomRight; break;
			}

			// zmień przyleganie
			((FieldExtraData)this._activeField.Tag).PosAlign = (int)align;
			var point = this._activeField.getPosByAlignPoint( align );

			this._locked = true;

			// zmień wartości w edytorze
			this.N_P2_PosX.Value = (decimal)point.X;
			this.N_P2_PosY.Value = (decimal)point.Y;

			this._locked = false;

#		if DEBUG
			Program.LogMessage( "Zmieniono punkt zaczepienia pola." );
#		endif
		}

#endregion

#region EDYTOR - STRONA

		/// <summary>
		/// Akcja wywoływana po kliknięciu w przycisk zmieniający kolor strony.
		/// Wyświetla okno wyboru koloru a po akceptacji zmienia kolor przypisany do aktualnej strony.
		/// </summary>
		/// 
		/// <param name="sender">Obiekt wywołujący zdarzenie.</param>
		/// <param name="ev">Argumenty zdarzenia.</param>
		//* ============================================================================================================
		private void B_P2_PageColor_Click( object sender, EventArgs ev )
		{
			if( this._locked )
				return;

			// pokaż okno wyboru koloru
			if( this.CD_ColorDialog.ShowDialog(this) != DialogResult.OK )
				return;

			this._activePage.BackgroundImage = null;
			this._activePage.BackColor       = this.CD_ColorDialog.Color;

			var color = this.CD_ColorDialog.Color;
			this.TB_P2_PageColor.Text = "#" + color.R.ToString("X2") +
				color.G.ToString("X2") + color.B.ToString("X2");

#		if DEBUG
			Program.LogMessage( "Zmiana koloru strony na '" + this.TB_P2_PageColor.Text + "'." );
#		endif
		}

		/// <summary>
		/// Akcja wywoływana po kliknięciu w przycisk zmieniający obraz strony.
		/// Wyświetla okno wyboru obrazka a po akceptacji zmienia obraz przypisany do aktualnej strony.
		/// </summary>
		/// 
		/// <param name="sender">Obiekt wywołujący zdarzenie.</param>
		/// <param name="ev">Argumenty zdarzenia.</param>
		//* ============================================================================================================
		private void B_P2_PageImage_Click( object sender, EventArgs ev )
		{
			// okno wyboru pliku
			if( this._locked )
				return;

			// wybór pliku
			var select = Program.GLOBAL.SelectFile;
			select.Title  = Language.GetLine( "MessageNames", (int)LANGCODE.GMN_SELECTFILE );
			select.Filter = "JPEG (*.jpg)|*.jpg|PNG (*.png)|*.png|BMP (*.bmp)|*.bmp";

			if( select.ShowDialog(this) != DialogResult.OK )
				return;
			
			if( this._activePage.BackgroundImage != null )
				this._activePage.BackgroundImage.Dispose();
			
			// zmień obraz panelu
			this._activePage.BackgroundImage = Image.FromFile( select.FileName );
			this._activePage.BackColor       = Color.Empty;

			GC.Collect();

			// ustaw ścieżkę do obrazu w polu tekstowym
			this.TB_P2_PageImage.Text = select.FileName;

			// odśwież kontrolke
			((PageExtraData)this._activePage.Tag).ImagePath = select.FileName;
			this._activePage.Refresh();

#		if DEBUG
			Program.LogMessage( "Zmiana obrazka przypisanego do strony na '" + select.FileName + "'." );
#		endif
		}

		/// <summary>
		/// Akcja wywoływana po zaznaczeniu pola rysowania koloru.
		/// Funkcja zmienia koncepcje generowania strony na PDF, rysując jej kolor przed polami.
		/// </summary>
		/// 
		/// <param name="sender">Obiekt wywołujący zdarzenie.</param>
		/// <param name="ev">Argumenty zdarzenia.</param>
		//* ============================================================================================================
		private void CB_P2_DrawPageColor_CheckedChanged( object sender, EventArgs ev )
		{
			if( this._locked )
				return;
			
			// sprawdź typ kontrolki
			var ischecked = sender is CheckBox
				? ((CheckBox)sender).Checked
				: ((ToolStripMenuItem)sender).Checked;

			// ustaw dane strony
			((PageExtraData)this._activePage.Tag).PrintColor = ischecked;
			((PageExtraData)this._activePage.Tag).PrintImage = false;

			this._locked = true;
			this.TSMI_PageDrawColor.Checked  = ischecked;
			this.CB_P2_DrawPageColor.Checked = ischecked;
			this.CB_P2_DrawPageImage.Checked = false;
			this.TSMI_PageDrawImage.Checked  = false;
			this._locked = false;

#		if DEBUG
			Program.LogMessage( "Generowanie koloru dla strony w PDF: " +
				(this.CB_P2_DrawPageColor.Checked ? "TAK" : "NIE") + "." );
#		endif
		}

		/// <summary>
		/// Akcja wywoływana po zaznaczeniu pola rysowania obrazu.
		/// Funkcja zmienia koncepcje generowania strony na PDF, rysując jej obraz przed polami.
		/// </summary>
		/// 
		/// <param name="sender">Obiekt wywołujący zdarzenie.</param>
		/// <param name="ev">Argumenty zdarzenia.</param>
		//* ============================================================================================================
		private void CB_P2_DrawPageImage_CheckedChanged( object sender, EventArgs ev )
		{
			if( this._locked )
				return;
			
			// sprawdź typ kontrolki
			var ischecked = sender is CheckBox
				? ((CheckBox)sender).Checked
				: ((ToolStripMenuItem)sender).Checked;

			// ustaw dane strony
			((PageExtraData)this._activePage.Tag).PrintColor = false;
			((PageExtraData)this._activePage.Tag).PrintImage = ischecked;

			this._locked = true;
			this.TSMI_PageDrawImage.Checked  = ischecked;
			this.CB_P2_DrawPageImage.Checked = ischecked;
			this.CB_P2_DrawPageColor.Checked = false;
			this.TSMI_PageDrawColor.Checked  = false;
			this._locked = false;
			
#		if DEBUG
			Program.LogMessage( "Generowanie obrazu dla strony w PDF: " +
				(this.CB_P2_DrawPageImage.Checked ? "TAK" : "NIE") + "." );
#		endif
		}

#endregion

#region PRZEŁĄCZANIE PANELI

		/// <summary>
		/// Akcja wywoływana po kliknięciu w przycisk "Główna".
		/// Pokazuje panel strony głównej i ukrywa pozostałe panele - edytora i wydruku.
		/// W przypadku gdy zawartość została zmieniona, np. po zapisie wzoru, odświeża podgląd.
		/// </summary>
		/// 
		/// <param name="sender">Obiekt wywołujący zdarzenie.</param>
		/// <param name="ev">Argumenty zdarzenia.</param>
		//* ============================================================================================================
		private void TSMI_HomePage_Click( object sender, EventArgs ev )
		{
#		if DEBUG
			Program.LogMessage( "Przełączanie na panel strony głównej." );
#		endif

			this.TLP_Main.Show();
			this.TSMI_HomePage.Enabled = false;

			// ukryj pozostałe panele
			if( this._currentPanel == 2 )
			{
				this.TLP_Pattern.Hide();
				this.TSMI_PatternEditor.Enabled = true;
			}
			else if( this._currentPanel == 3 )
			{
				this.TLP_Generator.Hide();
				this.TSMI_PrintPreview.Enabled = true;
			}

			// automatycznie odśwież podgląd, gdy jego zawartość uległa zmianie
			if( (this._editingName == this._selectedName) && this._editedChanged )
				if( this.TV_P1_Patterns.SelectedNode != null )
				{
					this._selectedID = this.TV_P1_Patterns.SelectedNode.Index;
					this.TV_P1_Patterns_AfterSelect( null, null );
				}

			this._editedChanged = false;
			this._currentPanel = 1;

			// ustaw skupienie na kontrolkę z sekcji
			this.TV_P1_Patterns.Focus();
		}

		/// <summary>
		/// Akcja wywoływana po kliknięciu w przycisk "Edytor".
		/// Funkcja pokazuje panel edytora wzorów i ukrywa pozostałe - stronę główną i wydruk.
		/// </summary>
		/// 
		/// <param name="sender">Obiekt wywołujący zdarzenie.</param>
		/// <param name="ev">Argumenty zdarzenia.</param>
		//* ============================================================================================================
		private void TSMI_PatternEditor_Click( object sender, EventArgs ev )
		{
#		if DEBUG
			Program.LogMessage( "Przełączanie na panel edytora wzorów." );
#		endif

			// dobierz odpowiedni tekst dla przycisku
			if( this._editingData != null )
				this.B_P2_LoadData.Text = this._editingData.Dynamic
					? Language.GetLine( "PatternEditor", "Buttons", (int)LANGCODE.I01_EDI_BUT_LOADDATA )
					: Language.GetLine( "PatternEditor", "Buttons", (int)LANGCODE.I01_EDI_BUT_PREVIEW );

			this.TLP_Pattern.Show();
			this.TSMI_PatternEditor.Enabled = false;

			// ukryj pozostałe panele
			if( this._currentPanel == 1 )
			{
				this.TLP_Main.Hide();
				this.TSMI_HomePage.Enabled = true;
			}
			else if( this._currentPanel == 3 )
			{
				this.TLP_Generator.Hide();
				this.TSMI_PrintPreview.Enabled = true;
			}

			this._currentPanel = 2;

			// ustaw skupienie na kontrolkę z sekcji
			this.N_P2_Page.Focus();
		}

		/// <summary>
		/// Akcja wywoływana po kliknięciu w przycisk "Wydruk".
		/// Funkcja pokazuje panel wydruku wzoru i ukrywa pozostałe - stronę główną i edytor.
		/// </summary>
		/// 
		/// <param name="sender">Obiekt wywołujący zdarzenie.</param>
		/// <param name="ev">Argumenty zdarzenia.</param>
		//* ============================================================================================================
		private void TSMI_PrintPreview_Click( object sender, EventArgs ev )
		{
#		if DEBUG
			Program.LogMessage( "Przełączanie na panel podglądu wydruku." );
#		endif

			this.TLP_Generator.Show();
			this.TSMI_PrintPreview.Enabled = false;

			// ukryj pozostałe panele
			if( this._currentPanel == 1 )
			{
				this.TLP_Main.Hide();
				this.TSMI_HomePage.Enabled = true;
			}
			else if( this._currentPanel == 2 )
			{
				this.TLP_Pattern.Hide();
				this.TSMI_PatternEditor.Enabled = true;
			}

			this._currentPanel = 3;

			// pokaż lub ukryj kontrolkę do modyfikacji ilości kopii dla podglądu
			if( this._generatorData.Dynamic )
				this.FLP_P3_Rows.Visible = false;
			else
				this.FLP_P3_Rows.Visible = true;

			// ustaw skupienie na kontrolkę z sekcji
			this.TV_P3_PageList.Focus();
		}

		/// <summary>
		/// Akcja wywoływana po kliknięciu w przycisk "Szczegóły".
		/// Funkcja przełącza z aktualnego panelu na panel szczegółów w edytorze wzorów.
		/// </summary>
		/// 
		/// <param name="sender">Obiekt wywołujący zdarzenie.</param>
		/// <param name="ev">Argumenty zdarzenia.</param>
		//* ============================================================================================================
		private void TSMI_DetailSwitch_Click( object sender, EventArgs ev )
		{
			// zablokuj aktualne i aktywuj pozostałe
			this.TSMI_DetailSwitch.Enabled = false;
			this.TSMI_FieldSwitch.Enabled  = true;
			this.TSMI_PageSwitch.Enabled   = true;

			this.TLP_P2_LabelDetails.Show();
			this.TLP_P2_Field.Hide();
			this.TLP_P2_PageDetails.Hide();
			
			// przełącz skupienie na kontrolkę z przełączonej sekcji
			this.CB_P2_DrawBorder.Focus();
		}
		
		/// <summary>
		/// Akcja wywoływana po kliknięciu w przycisk "Pole".
		/// Funkcja przełącza z aktualnego panelu na panel pola w edytorze wzorów.
		/// </summary>
		/// 
		/// <param name="sender">Obiekt wywołujący zdarzenie.</param>
		/// <param name="ev">Argumenty zdarzenia.</param>
		//* ============================================================================================================
		private void TSMI_FieldSwitch_Click( object sender, EventArgs ev )
		{
			// zablokuj aktualne i aktywuj pozostałe
			this.TSMI_DetailSwitch.Enabled = true;
			this.TSMI_FieldSwitch.Enabled  = false;
			this.TSMI_PageSwitch.Enabled   = true;

			this.TLP_P2_Field.Show();
			this.TLP_P2_LabelDetails.Hide();
			this.TLP_P2_PageDetails.Hide();

			// przełącz skupienie na kontrolkę z przełączonej sekcji
			this.TB_P2_LabelName.Focus();
		}
		
		/// <summary>
		/// Akcja wywoływana po kliknięciu w przycisk "Strona".
		/// Funkcja przełącza z aktualnego panelu na panel strony w edytorze wzorów.
		/// </summary>
		/// 
		/// <param name="sender">Obiekt wywołujący zdarzenie.</param>
		/// <param name="ev">Argumenty zdarzenia.</param>
		//* ============================================================================================================
		private void TSMI_PageSwitch_Click( object sender, EventArgs ev )
		{
			// zablokuj aktualne i aktywuj pozostałe
			this.TSMI_DetailSwitch.Enabled = true;
			this.TSMI_FieldSwitch.Enabled  = true;
			this.TSMI_PageSwitch.Enabled   = false;
			
			this.TLP_P2_PageDetails.Show();
			this.TLP_P2_LabelDetails.Hide();
			this.TLP_P2_Field.Hide();

			// przełącz skupienie na kontrolkę z przełączonej sekcji
			this.B_P2_PageColor.Focus();
		}

#endregion

#region PASEK INFORMACYJNY - EDYTOR WZORÓW
		
		/// <summary>
		/// Akcja wywoływana po kliknięciu w przycisk wczytywania danych.
		/// Otwiera okno ustawień wczytywanych danych a po zatwierdzeniu okno przypisywania kolumn do pól.
		/// Po akceptacji strumienia, przechodzi na stronę z podglądem wydruku.
		/// </summary>
		/// 
		/// <param name="sender">Obiekt wywołujący zdarzenie.</param>
		/// <param name="ev">Argumenty zdarzenia.</param>
		//* ============================================================================================================
		private void B_P2_LoadData_Click( object sender, EventArgs ev )
		{
			this.loadDataForPreview( this._editingName );
		}

		/// <summary>
		/// Akcja wywoływana po wciśnięciu przycisku zapisu wzoru.
		/// Funkcja usuwa pliki utworzone przez wcześniejszą funkcję zapisu wraz z obrazkami.
		/// Po usunięciu generuje pliki podglądowe i zapisuje nowe ustawienia wzoru.
		/// </summary>
		/// 
		/// <param name="sender">Obiekt wywołujący zdarzenie.</param>
		/// <param name="ev">Argumenty zdarzenia.</param>
		//* ============================================================================================================
		private void B_P2_Save_Click( object sender, EventArgs ev )
		{
			this.Cursor = Cursors.WaitCursor;

			// usuń wszelkie obrazki
			if( Directory.Exists("patterns/" + this._editingName + "/images") )
				Directory.EnumerateFiles( "patterns/" + this._editingName + "/images" ).ToList().ForEach( File.Delete );
			else
				Directory.CreateDirectory( "patterns/" + this._editingName + "/images" );

			// usuń podgląd
			int x = 0;
			while( true )
			{
				if( File.Exists("patterns/" + this._editingName + "/preview" + x + ".jpg") )
					File.Delete( "patterns/" + this._editingName + "/preview" + x + ".jpg" );
				else
					break;
				++x;
			}

			// wygeneruj podgląd i zapisz wzór
			PatternEditor.GeneratePreview( this._editingData, this.P_P2_Pattern, 1.0 );
			PatternEditor.Save( this._editingData, this.P_P2_Pattern );

			// dobierz odpowiedni tekst dla przycisku
			var patdata = PatternEditor.ReadPattern( this._editingName, true );
			this.B_P2_LoadData.Text = patdata.Dynamic
				? Language.GetLine( "PatternEditor", "Buttons", (int)LANGCODE.I01_EDI_BUT_LOADDATA )
				: Language.GetLine( "PatternEditor", "Buttons", (int)LANGCODE.I01_EDI_BUT_PREVIEW );

			this._editedChanged = true;
			this.Cursor         = null;
		}

		/// <summary>
		/// Akcja wywoływana podczas zmiany zawartości kontroki sterującej numerem strony.
		/// Funkcja przełącza edytor na wybraną stronę, jeżeli taka strona została wcześniej utworzona.
		/// </summary>
		/// 
		/// <param name="sender">Obiekt wywołujący zdarzenie.</param>
		/// <param name="ev">Argumenty zdarzenia.</param>
		//* ============================================================================================================
		private void N_P2_Page_ValueChanged( object sender, EventArgs ev )
		{
			var panel = (Panel)this.P_P2_Pattern;

			// no nie da rady...
			if( this._locked || panel.Controls.Count < (int)this.N_P2_Page.Value - 1 )
				return;

			// ustaw aktualną stronę
			panel.Controls[(int)N_P2_Page.Value - 1].Show();
			this._activePage.Hide();
			this._activePage    = (Panel)panel.Controls[(int)N_P2_Page.Value - 1];
			this._editingPageID = (int)N_P2_Page.Value - 1;

			// dane dodatkowe
			var ptag = (PageExtraData)this._activePage.Tag;
			this.CB_P2_DrawPageColor.Checked = ptag.PrintColor;
			this.CB_P2_DrawPageImage.Checked = ptag.PrintImage;

			// kolor strony
			string color_r, color_g, color_b;

			color_r = this._activePage.BackColor.R.ToString("X2");
			color_g = this._activePage.BackColor.G.ToString("X2");
			color_b = this._activePage.BackColor.B.ToString("X2");
			this.TB_P2_PageColor.Text = "#" + color_r + color_b + color_g;
		}

		/// <summary>
		/// Akcja wywoływana przy traceniu skupienia przez kontrolkę.
		/// Pozwala na ustawienie nowej wartości elementu, dzięki czemu skala w edytorze zostanie zmieniona.
		/// </summary>
		/// 
		/// <param name="sender">Obiekt wywołujący zdarzenie.</param>
		/// <param name="ev">Argumenty zdarzenia.</param>
		//* ============================================================================================================
		private void CBX_P2_Scale_Leave( object sender, EventArgs ev )
		{
			this.CBX_P2_Scale_SelectedIndexChanged( sender, ev );
		}

		/// <summary>
		/// Akcja wywoływana przy naciśnięciu klawisza.
		/// Funkcja wywołuje metodę skalującą wzór w edytorze po wciśnięciu klawisza ENTER.
		/// </summary>
		/// 
		/// <param name="sender">Obiekt wywołujący zdarzenie.</param>
		/// <param name="ev">Argumenty zdarzenia.</param>
		//* ============================================================================================================
		private void CBX_P2_Scale_KeyDown( object sender, KeyEventArgs ev )
		{
			if( ev.KeyCode == Keys.Enter )
				this.CBX_P2_Scale_SelectedIndexChanged( sender, null );
		}

		/// <summary>
		/// Akcja wywoływana po zmianie elementu w kontrolce.
		/// Kontrolka wyboru przyjmuje również wartości użytkownika.
		/// Obsługuje wartości od 50 do 300, wyłącznie w typie numerycznym.
		/// Funkcja wywołuje dzięki temu funkcje skalującą edytowany dokument do wybranego rozmiaru.
		/// </summary>
		/// 
		/// <param name="sender">Obiekt wywołujący zdarzenie.</param>
		/// <param name="ev">Argumenty zdarzenia.</param>
		//* ============================================================================================================
		private void CBX_P2_Scale_SelectedIndexChanged( object sender, EventArgs ev )
		{
			if( this._locked )
				return;

			// próbuj zamienić string na int
			int scale = 0;
			try { scale = Convert.ToInt32(this.CBX_P2_Scale.Text); }
			catch
			{
				scale = 100;
				this.CBX_P2_Scale.Text = "100";
			}

			// nie można zmniejszyć więcej niż 50%
			if( scale < 50 )
			{
				this.CBX_P2_Scale.Text = "50";
				scale = 50;
			}

			// nie można powiększyć więcej niż 300%
			if( scale > 300 )
			{
				this.CBX_P2_Scale.Text = "300";
				scale = 300;
			}
			
			// zmień skale wzoru
			PatternEditor.ChangeScale( this._editingData, this.P_P2_Pattern, (double)scale / 100.0 );
		}

#endregion

#region MENU JĘZYKA

		/// <summary>
		/// Akcja wywoływana podczas zmiany języka.
		/// Po zmianie języka wszystkie widoczne formularze są ponownie tłumaczone.
		/// Dzięki temu zmiana języka wywoływana jest dynamicznie.
		/// </summary>
		/// 
		/// <param name="sender">Obiekt wywołujący zdarzenie.</param>
		/// <param name="ev">Argumenty zdarzenia.</param>
		//* ============================================================================================================
		void TSMI_Language_Click( object sender, EventArgs ev )
		{
			var item = (ToolStripMenuItem)sender;
			var code = (string)item.Tag;

			// zmień wyświetlany język
			Language.Initialize();
			Language.Parse( code );

			// odśwież listę języków
			this.fillAvailableLanguages();

			// przetłumacz menu
			this.translateMenu();
			this.translateHomeForm();
			this.translateEditorForm();
			this.translatePrintoutForm();

			// zapisz język
			Settings.Info.Language = code;
			Settings.SaveSettings();
		}

#endregion

#region PASEK INFORMACYJNY - LISTA WZORÓW

		/// <summary>
		/// Przełączanie okna ze szczegółami dotyczącymi zaznaczonego wzoru.
		/// </summary>
		/// 
		/// <param name="sender">Obiekt wywołujący zdarzenie.</param>
		/// <param name="ev">Argumenty zdarzenia.</param>
		//* ============================================================================================================
		private void CB_P1_ShowDetails_CheckedChanged( object sender, EventArgs ev )
		{
			this.SC_P1_Details.Panel2Collapsed = !this.CB_P1_ShowDetails.Checked;
		}

		/// <summary>
		/// Akcja wywoływana po kliknięciu w przycisk "Usuń".
		/// Usuwa zaznaczony na liście wzór.
		/// Przed usunięciem wyświetla pytanie, czy na pewno go usunąć.
		/// </summary>
		/// 
		/// <param name="sender">Obiekt wywołujący zdarzenie.</param>
		/// <param name="ev">Argumenty zdarzenia.</param>
		//* ============================================================================================================
		private void B_P1_Delete_Click( object sender, EventArgs ev )
		{
			// indeks poza zakresem
			if( this._selectedID == -1 )
				return;

#		if DEBUG
			Program.LogMessage( "Operacja usuwania wzoru." );
#		endif

			// nazwa wzoru do usunięcia
			var pattern = this._selectedName;

			// upewnij się czy użytkownik na pewno chce go usunąć
			var result = Program.LogQuestion
			(
				Language.GetLine( "PatternList", "Messages", (int)LANGCODE.I01_PAT_MES_WANTDELETE ),
				Language.GetLine( "MessageNames", (int)LANGCODE.GMN_PATTERNEDITOR ),
				false,
				this
			);

			if( result == DialogResult.No )
				return;
			this.Cursor = Cursors.WaitCursor;
			
			// sprawdź czy usuwany jest edytowany wzór
			if( this._editingName == this._selectedName )
			{
				this._editingName = null;

				// wyłącz możliwość edycji nieistniejącego już wzoru
				this.TSMI_PatternEditor.Enabled = false;
			}
			// brak wzoru, brak obrazka...
			this._patternPreview.Image = null;
			this._selectedID      = -1;

			// usuń pliki wzoru i odśwież liste - zakładamy że użytkownik ma uprawnienia
			if( Directory.Exists("./patterns/" + pattern) )
				Directory.Delete( "patterns/" + pattern, true );

			// usuń z listy ostatnio otwieranych (jeżeli się tam znajduje)
			Settings.RemoveFromLastPatterns( pattern );
			this.refreshRecentOpenList();

			// odśwież listę wzorów
			this.refreshProjectList();

			this.Cursor = null;

#		if DEBUG
			Program.LogMessage( "Wzór '" + pattern + "' został usunięty." );
#		endif
		}

		/// <summary>
		/// Akcja wywoływana po zmianie strony podglądu wzoru.
		/// Funkcja powoduje zmianę wyświetlanego obrazka odpowiadającego za stronę wzoru.
		/// W przypadku gdy obrazka nie ma, nie wyświetla nic.
		/// </summary>
		/// 
		/// <param name="sender">Obiekt wywołujący zdarzenie.</param>
		/// <param name="ev">Argumenty zdarzenia.</param>
		//* ============================================================================================================
		private void N_P1_Page_ValueChanged( object sender, EventArgs ev )
		{
#		if	DEBUG
			Program.LogMessage( "Zmiana strony wzoru na stronie głównej." );
#		endif

			// zmień status
			this.Cursor = Cursors.WaitCursor;

			// otwórz podgląd wybranej strony wzoru
			if( File.Exists("patterns/" + this._selectedName + "/preview" + (this.N_P1_Page.Value - 1) + ".jpg") )
				using( var image = Image.FromFile("patterns/" + this._selectedName + "/preview" + (this.N_P1_Page.Value - 1) + ".jpg") )
				{
					if( this._patternPreview.Image != null )
						this._patternPreview.Image.Dispose();

					// skopiuj obraz do pamięci
					var new_image = new Bitmap( image.Width, image.Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb );
					using( var canavas = Graphics.FromImage(new_image) )
						canavas.DrawImageUnscaled( image, 0, 0 );
					this._patternPreview.Image = new_image;

					image.Dispose();
				}
			else
				// @TODO - dodać jakiś inny obrazek :D
				this._patternPreview.Image = null;

			this.Cursor = null;
		}

#endregion

#region MENU NARZĘDZI

		/// <summary>
		/// Akcja wywoływana po kliknięciu w przycisk zamykania strumienia danych.
		/// Funkcja usuwa aktualny strumień i pozwala na wczytanie nowego.
		/// </summary>
		/// 
		/// <param name="sender">Obiekt wywołujący zdarzenie.</param>
		/// <param name="ev">Argumenty zdarzenia.</param>
		//* ============================================================================================================
		private void TSMI_CloseData_Click( object sender, EventArgs ev )
		{
			// w przypadku gdy strumienie są takie same, zamknij obydwa
			if( this._stream == this._preparedStream )
				this._preparedStream = null;

			this.TSMI_CloseData.Enabled   = false;
			this.TSMI_EditColumns.Enabled = false;
			this.TSMI_EditRows.Enabled    = false;
			this.TSMI_SaveData.Enabled    = false;
			
#		if DEBUG
			Program.LogMessage( "Usuwanie otwartego strumienia danych." );
#		endif

			this._stream = null;
			GC.Collect();
		}

		/// <summary>
		/// Akcja wywoływana po kliknięciu w przycisk wczytania pliku z bazą danych.
		/// Otwiera okno wyboru pliku, po czym po akceptacji otwiera okno z ustawieniami strumienia.
		/// Na razie program obsługuje tylko pliki CSV.
		/// </summary>
		/// 
		/// <param name="sender">Obiekt wywołujący zdarzenie.</param>
		/// <param name="ev">Argumenty zdarzenia.</param>
		//* ============================================================================================================
		private void TSMI_LoadDataFile_Click( object sender, EventArgs ev )
		{
#		if DEBUG
			Program.LogMessage( "** Okno wczytywania i ustawień pliku z danymi." );
			Program.LogMessage( "** BEGIN ================================================================== **" );
			Program.IncreaseLogIndent();
#		endif
			// wybór pliku
			var select = Program.GLOBAL.SelectFile;
			select.Title  = Language.GetLine( "MessageNames", (int)LANGCODE.GMN_SELECTFILE );
			select.Filter = IOFileData.getExtensionsList( true );

			if( select.ShowDialog(this) != DialogResult.OK )
			{
#			if DEBUG
				Program.LogMessage( "Operacja anulowana." );
				Program.DecreaseLogIndent();
				Program.LogMessage( "** END ==================================================================== **" );
#			endif
				return;
			}

			// wczytaj plik
			var storage = new IOFileData( select.FileName, Encoding.Default );
			if( storage == null || !storage.Ready )
				return;

			// ustawienia odczytu pliku
			var settings = Program.GLOBAL.DatafileSettings;
			settings.Storage = storage;
			settings.translateForm();

			if( settings.ShowDialog(this) != DialogResult.OK )
			{
#			if DEBUG
				Program.DecreaseLogIndent();
				Program.LogMessage( "** END ==================================================================== **" );
#			endif
				return;
			}

			// zapisz strumień i odblokuj przycisk edytora kolumn
			this._stream = storage;
			this.TSMI_EditColumns.Enabled = true;
			this.TSMI_EditRows.Enabled    = true;
			this.TSMI_SaveData.Enabled    = true;
			this.TSMI_CloseData.Enabled   = true;

			GC.Collect();

#		if DEBUG
			Program.LogMessage( "Wybrano nowe źródło odczytywania danych." );
			Program.DecreaseLogIndent();
			Program.LogMessage( "** END ==================================================================== **" );
#		endif
		}

		/// <summary>
		/// Akcja uruchamiana po kliknięciu w przycisk tworzenia bazy w pamięci komputera.
		/// Tworzy pustą bazę danych w pamięci komputera, po zakończeniu wyświetla informacje o utworzeniu.
		/// </summary>
		/// 
		/// <param name="sender">Obiekt wywołujący zdarzenie.</param>
		/// <param name="ev">Argumenty zdarzenia.</param>
		//* ============================================================================================================
		private void TSMI_CreateEmpty_Click( object sender, EventArgs ev )
		{
			// wczytaj plik
			var storage = new IOFileData();
			storage.createEmpty();

			// wyświetl informacje o utworzeniu bazy w pamięci
			Program.LogInfo
			(
				Language.GetLine( "Menu", "Messages", (int)LANGCODE.I01_MEN_MSG_CREATEMDB ),
				Language.GetLine( "MessageNames", (int)LANGCODE.GMN_DATABASE ),
				this
			);

			// zapisz strumień i odblokuj przycisk edytora kolumn
			this._stream = storage;

			this.TSMI_EditColumns.Enabled = true;
			this.TSMI_EditRows.Enabled    = true;
			this.TSMI_SaveData.Enabled    = true;
			this.TSMI_CloseData.Enabled   = true;

			GC.Collect();
		}

		/// <summary>
		/// Akcja wywoływana po kliknięciu w pozycję "Zapisz dane do pliku".
		/// Wyświetla okno do wyboru miejsca, gdzie plik ma być zapisany.
		/// Zapisuje na razie tylko do formatu CSV.
		/// Jako domyślną nazwę pliku przyjmuje aktualną datę.
		/// </summary>
		/// 
		/// <param name="sender">Obiekt wywołujący zdarzenie.</param>
		/// <param name="ev">Argumenty zdarzenia.</param>
		//* ============================================================================================================
		private void TSMI_SaveData_Click( object sender, EventArgs ev )
		{
			var dialog = Program.GLOBAL.SaveFile;

#		if DEBUG
			Program.LogMessage( "** Okno wyboru miejsca do zapisu pliku z bazą danych." );
			Program.LogMessage( "** BEGIN ================================================================== **" );
			Program.IncreaseLogIndent();
#		endif

			// jako domyślną nazwę przyjmij rozszerzenie pierwsze na liście
			dialog.DefaultExt = "." + IOFileData.Extensions[0];
			dialog.FileName   = DateTime.Now.ToString("yyyyMMddHHmm");
			dialog.Filter     = IOFileData.getExtensionsList( true );

			if( dialog.ShowDialog(this) == DialogResult.OK )
			{
#			if DEBUG
				Program.LogMessage( "Nazwa pliku do zapisu: " + dialog.FileName );
#			endif
				this._stream.save( dialog.FileName );
			}
#		if DEBUG
			else
				Program.LogMessage( "Operacja anulowana." );

			Program.DecreaseLogIndent();
			Program.LogMessage( "** END ==================================================================== **" );
#		endif
		}

		/// <summary>
		/// Akcja uruchamiana po kliknięciu w pozycję edycji wierszy.
		/// Funkcja uruchamia okno edycji wierszy, dzięki czemu wczytane dane można edytować.
		/// </summary>
		/// 
		/// <param name="sender">Obiekt wywołujący zdarzenie.</param>
		/// <param name="ev">Argumenty zdarzenia.</param>
		//* ============================================================================================================
		private void TSMI_EditRows_Click( object sender, EventArgs ev )
		{
			if( this._stream == null )
				return;

#		if DEBUG
			Program.LogMessage( "** Okno edycji wierszy." );
			Program.LogMessage( "** BEGIN ================================================================== **" );
			Program.IncreaseLogIndent();
#		endif

			var edit = new EditRowsForm();
			
			// przypisz strumień i odśwież dane
			edit.Storage = this._stream;
			edit.refreshDataRange();
				
			// wyświetl okno        
#		if DEBUG
			var result = edit.ShowDialog( this );

			if( result != DialogResult.OK )
				Program.LogMessage( "Operacja anulowana." );

			Program.DecreaseLogIndent();
			Program.LogMessage( "** END ==================================================================== **" );
#		else
			edit.ShowDialog( this );
#		endif
		}

		/// <summary>
		/// Akcja uruchamiana podczas kliknięcia w pozycję edytora kolumn w menu.
		/// Funkcja otwiera okno zarządzania kolumnami - pozwala na tworzenie nowych kolumn i filtrowanie.
		/// </summary>
		/// 
		/// <param name="sender">Obiekt wywołujący zdarzenie.</param>
		/// <param name="ev">Argumenty zdarzenia.</param>
		//* ============================================================================================================
		private void TSMI_EditColumns_Click( object sender, EventArgs ev )
		{
			if( this._stream == null )
				return;
			
#		if DEBUG
			Program.LogMessage( "** Okno edycji i filtrowania kolumn." );
			Program.LogMessage( "** BEGIN ================================================================== **" );
			Program.IncreaseLogIndent();
#		endif

			var columns = new EditColumnsForm();

			// przypisz strumień i wyświetl okno
			columns.Storage = this._stream;

#		if DEBUG
			var result = columns.ShowDialog( this );

			if( result != DialogResult.OK )
				Program.LogMessage( "Operacja anulowana." );

			Program.DecreaseLogIndent();
			Program.LogMessage( "** END ==================================================================== **" );
#		else
			columns.ShowDialog( this );
#		endif
		}

#endregion

#region MENU PROGRAM

		/// <summary>
		/// Akcja wywoływana przez kliknięcie w pozycję "Informacje" w menu.
		/// Funkcja otwiera okno informacji o programie.
		/// </summary>
		/// 
		/// <param name="sender">Obiekt wywołujący zdarzenie.</param>
		/// <param name="ev">Argumenty zdarzenia.</param>
		//* ============================================================================================================
		private void TSMI_Info_Click( object sender, EventArgs ev )
		{
#		if DEBUG
			Program.LogMessage( "** Okno wczytywania i ustawień pliku z danymi." );
			Program.LogMessage( "** BEGIN ================================================================== **" );
			Program.IncreaseLogIndent();
#		endif

			Program.GLOBAL.Info.refreshAndOpen( this );

#		if DEBUG
			Program.DecreaseLogIndent();
			Program.LogMessage( "** END ==================================================================== **" );
#		endif
		}

		/// <summary>
		/// Akcja wywoływana po naciśnięciu przycisku aktualizacji programu.
		/// Funkcja otwiera okno aktualizacji, a przed otwarciem sprawdza czy dostępne są aktualizacje.
		/// Teoretycznie aplikacja sama powinna połączyć się z internetem i sprawdzić to jeszcze przed otwarciem okna.
		/// </summary>
		/// 
		/// <param name="sender">Obiekt wywołujący zdarzenie.</param>
		/// <param name="ev">Argumenty zdarzenia.</param>
		//* ============================================================================================================
		private void TSMI_Update_Click( object sender, EventArgs ev )
		{
#		if DEBUG
			Program.LogMessage( "** Okno tworzenia wzoru." );
			Program.LogMessage( "** BEGIN ================================================================== **" );
			Program.IncreaseLogIndent();
#		endif

			// otwiera okno aktualizacji
			UpdateForm update = new UpdateForm();
			update.refreshAndOpen( this );

#		if DEBUG
			Program.DecreaseLogIndent();
			Program.LogMessage( "** END ==================================================================== **" );
#		endif
		}

#endregion

#region LISTA I PODGLĄD WZORU

		/// <summary>
		/// Akcja wywoływana podczas zmiany rozmiaru panelu.
		/// Podczas zmiany rozmiaru ponownemu sprawdzeniu ulega lokalizacja elementu znajdującego się wewnątrz.
		/// </summary>
		/// 
		/// <param name="sender">Obiekt wywołujący zdarzenie.</param>
		/// <param name="ev">Argumenty zdarzenia.</param>
		//* ============================================================================================================
		private void P_P1_Preview_Resize( object sender, EventArgs ev )
		{
			if( this._locked )
				return;

			// na linuksie wariowało, ponieważ funkcja wywoływała się przed zakończeniem konstruktora
			// wywoływała się w trakcie inicjalizacji elementów
			if( this._patternPreview != null )
				this._patternPreview.checkLocation();
		}

		/// <summary>
		/// Akcja wywoływana po wciśnięciu przycisku myszy na panelu.
		/// Ustawia na panelu skupienie, dzięki czemu można kółkiem myszy przesuwać jego zawartość.
		/// </summary>
		/// 
		/// <param name="sender">Obiekt wywołujący zdarzenie.</param>
		/// <param name="ev">Argumenty zdarzenia.</param>
		//* ============================================================================================================
		private void P_P1_Preview_MouseDown( object sender, MouseEventArgs ev )
		{
			this.P_P1_Preview.Focus();
		}

		/// <summary>
		/// Akcja wywoływana po wciśnięciu przycisku myszy na liście wzorów.
		/// Zaznacza wybrany element na liście (jeżeli nie kliknięto na żaden element, odznacza).
		/// </summary>
		/// 
		/// <param name="sender">Obiekt wywołujący zdarzenie.</param>
		/// <param name="ev">Argumenty zdarzenia.</param>
		//* ============================================================================================================
		private void TV_P1_Patterns_MouseDown( object sender, MouseEventArgs ev )
		{
			if( ev.Button == MouseButtons.Middle )
				return;

			// zaznacz przy wciśnięciu prawego lub lewego przycisku myszy
			this.TV_P1_Patterns.SelectedNode = this.TV_P1_Patterns.GetNodeAt( ev.X, ev.Y );

			// resetuj zaznaczony indeks
			if( this.TV_P1_Patterns.SelectedNode == null )
				this._selectedID = -1;
			
#		if DEBUG
			Program.LogMessage( "Zaznaczony indeks: " + this._selectedID + "." );
#		endif
		}

		/// <summary>
		/// Akcja wywoływana po zaznaczeniu elementu na liście wzorów.
		/// Powoduje zmianę podglądu i pliku konfiguracyjnego wzoru na ten zaznaczony.
		/// Uzupełnia informacje wyświetlane w szczegółach wzoru pokazywanego po zaznaczeniu pola.
		/// W przypadku gdy podgląd dla wzoru nie jest dostępny, nie wyświetla nic.
		/// </summary>
		/// 
		/// <param name="sender">Obiekt wywołujący zdarzenie.</param>
		/// <param name="ev">Argumenty zdarzenia.</param>
		//* ============================================================================================================
		private void TV_P1_Patterns_AfterSelect( object sender, TreeViewEventArgs ev )
		{
			// indeks poza zakresem
			if( this.TV_P1_Patterns.SelectedNode == null )
			{
				this.N_P1_Page.Enabled   = false;
				this.B_P1_Delete.Enabled = false;

				return;
			}

			// indeks się nie zmienił...
			if( this._selectedID == this.TV_P1_Patterns.SelectedNode.Index && !this._editedChanged && sender != null )
				return;

			// zmień kursor
			this.Cursor  = Cursors.WaitCursor;
			this._locked = true;

			this._selectedID    = this.TV_P1_Patterns.SelectedNode.Index;
			this._selectedError = false;
			this._selectedDynamic  = false;
			
			// włącz możliwość usuwania
			this.B_P1_Delete.Enabled = true;

			// nazwa zaznaczonego wzoru
			var   pattern = this._selectedName = this.TV_P1_Patterns.SelectedNode.Text;
			Image helper  = null,
				  trash   = null;

			// sprawdź czy wzór posiada plik konfiguracyjny
			if( !File.Exists("patterns/" + pattern + "/config.cfg") )
			{
				this._selectedError = true;

				// obrazy
				trash  = this._patternPreview.Image;
				helper = Program.GetBitmap( BITMAPCODE.NOIMAGE );

				// informacja
				Program.LogError
				(
					Language.GetLine( "PatternList", "Messages", (int)LANGCODE.I01_PAT_MES_PATDAMAGED ),
					Language.GetLine( "MessageNames", (int)LANGCODE.GMN_PATTERNEDITOR ),
					false, null, this
				);
				this.N_P1_Page.Enabled = false;

				// przejdź na koniec
				goto CD_mtvPatterns_AfterSelect;
			}

			// wczytaj nagłówek
			var data   = PatternEditor.ReadPattern( pattern, true );
			int format = -1;
			
			// treść dynamiczna
			this._selectedDynamic = data.Dynamic;

			// wykryj format wzoru
			format = PatternEditor.DetectFormat( data.Size.Width, data.Size.Height );

			// ustaw wartości pola numerycznego
			this.N_P1_Page.Maximum = data.Pages;
			this.N_P1_Page.Value = 1;

			var values = Language.GetLines( "PatternList", "Pattern" );

			// informacje o wzorze
			this.RTB_P1_Details.Text = 
				String.Format( values[(int)LANGCODE.I01_PAT_PAT_NAME], data.Name ) + "\n" +
				String.Format( values[(int)LANGCODE.I01_PAT_PAT_FORMAT], format == -1
					? values[(int)LANGCODE.I01_PAT_PAT_OWNFORMAT]
					: PatternEditor.FormatNames[format] ) + "\n" +
				String.Format( values[(int)LANGCODE.I01_PAT_PAT_SIZE], data.Size.Width, data.Size.Height ) + "\n" +
				String.Format( values[(int)LANGCODE.I01_PAT_PAT_DATAPLACE], data.Dynamic
					? values[(int)LANGCODE.I01_PAT_PAT_YES]
					: values[(int)LANGCODE.I01_PAT_PAT_NO] ) + "\n" +
				String.Format( values[(int)LANGCODE.I01_PAT_PAT_PAGECOUNT], data.Pages ) + "\n";

			// podgląd strony
			if( File.Exists("patterns/" + pattern + "/preview0.jpg") )
			{
				// wczytaj podgląd strony
				trash = this._patternPreview.Image;

				// nie blokuj pliku... (dziadostwo blokuje plik)
				using( var image = Image.FromFile("patterns/" + pattern + "/preview0.jpg") )
				{
					// skopiuj obraz do pamięci
					helper = new Bitmap( image.Width, image.Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb );
					using( var canavas = Graphics.FromImage(helper) )
						canavas.DrawImageUnscaled( image, 0, 0 );

					image.Dispose();
				}
				this.N_P1_Page.Enabled = true;

				// przejdź na koniec
				goto CD_mtvPatterns_AfterSelect;
			}
			this._selectedDynamic = false;

			// brak obrazka
			helper = Program.GetBitmap( BITMAPCODE.NOIMAGE );
			trash  = this._patternPreview.Image;

			this.N_P1_Page.Enabled = false;

CD_mtvPatterns_AfterSelect:
#		if DEBUG
			Program.LogMessage( "Wczytano dane podstawowe wzoru o nazwie '" + pattern + "'." );
#		endif

			// zmień obrazek
			this._patternPreview.Hide();
			this._patternPreview.Image = helper;
			this._patternPreview.checkLocation();
			this._patternPreview.Show();
			
			// pozbieraj śmieci po poprzednim obrazku
			if( trash != null && trash != Program.GetBitmap(BITMAPCODE.NOIMAGE) )
				trash.Dispose();
			GC.Collect();

			// odblokuj i zmień kursor
			this._locked = false;
			this.Cursor  = null;
		}

		/// <summary>
		/// Akcja wykonywana po podwójnym kliknięciu na wzór z listy.
		/// Funkcja otwiera wybrany wzór do edycji w edytorze wzorów i ustawia go jako ostatnio używanego.
		/// W przypadku gdy wzór jest już otwarty, nie otwiera go ponownie.
		/// Resetuje ustawione wcześniej skalowanie na 100%.
		/// </summary>
		/// 
		/// <param name="sender">Obiekt wywołujący zdarzenie.</param>
		/// <param name="ev">Argumenty zdarzenia.</param>
		//* ============================================================================================================
		private void TSMI_EditPattern_Click( object sender, EventArgs ev )
		{
			// indeks poza zakresem
			if( this._selectedID == -1 )
				return;
#		if DEBUG
			Program.LogMessage( "Przygotowywanie do uruchomienia edytora wybranego wzoru." );
#		endif

			// nie można edytować projektu...
			if( this._selectedError )
				return;

			// przełącz gdy wzór jest już załadowany
			if( this._editingName == this._selectedName )
			{
				this.TSMI_PatternEditor_Click( null, null );
				return;
			}

			this._locked = true;
			this.Cursor  = Cursors.WaitCursor;

			// przejście do edycji wzoru
			string pattern = this._selectedName;

			// dodaj do listy ostatnio otwieranych
			Settings.AddToLastPatterns( pattern );
			this.refreshRecentOpenList();

			// zapisz indeks i nazwę aktualnego wzoru
			this._editingName = pattern;

			// wczytaj wzór
			this.CBX_P2_Scale.SelectedIndex = 2;
			var pattern_data = PatternEditor.ReadPattern( pattern );
			PatternEditor.DrawPreview( pattern_data, this.P_P2_Pattern, 1.0 );
			this._editingPageSize = pattern_data.Size;

			// ilość stron
			this._editingPages     = pattern_data.Pages - 1;
			this.N_P2_Page.Maximum = pattern_data.Pages;
			this.N_P2_Page.Value   = 1;

			// aktualna strona
			this._activePage     = (Panel)this.P_P2_Pattern.Controls[0];
			this._editingPageID = 0;
			this._editingData    = pattern_data;

			AlignedPage page;
			PageField   field;

			// dodaj akcje do stron i pól
			for( int x = 0; x < this.P_P2_Pattern.Controls.Count; ++x )
			{
				page = (AlignedPage)this.P_P2_Pattern.Controls[x];
				page.ContextMenuStrip = this.CMS_Page;
				page.MouseDown += new MouseEventHandler( this.P_P2_Pattern_MouseDown );

				for( int y = 0; y < page.Controls.Count; ++y )
				{
					field = (PageField)page.Controls[y];
					field.ContextMenuStrip = this.CMS_Label;
					field.MouseDown += new MouseEventHandler( this.L_P2_EditorField_MouseDown );
					field.MouseUp   += new MouseEventHandler( this.L_P2_EditorField_MouseUp );
					field.MouseMove += new MouseEventHandler( this.L_P2_EditorField_MouseMove );
				}

				page.checkLocation();
			}

			// zablokuj pola
			this.lockFieldLabels();

			// rozmiar stron
			this.TB_P2_PageWidth.Text  = this._editingPageSize.Width + " mm";
			this.TB_P2_PageHeight.Text = this._editingPageSize.Height + " mm";

			// ustaw kontroki
			var ptag = (PageExtraData)this._activePage.Tag;

			this.CB_P2_DrawPageColor.Checked = ptag.PrintColor;
			this.CB_P2_DrawPageImage.Checked = ptag.PrintImage;

			// kolor strony
			string color_r, color_g, color_b;
			
			color_r = this._activePage.BackColor.R.ToString("X2");
			color_g = this._activePage.BackColor.G.ToString("X2");
			color_b = this._activePage.BackColor.B.ToString("X2");
			this.TB_P2_PageColor.Text = "#" + color_r + color_b + color_g;

			// przełącz na panel wzorów
			this.TSMI_PatternEditor_Click( null, null );
			this.Cursor = null;

			// odblokuj panel i pola
			this._locked = false;
		}

#endregion

#region MENU WZORU I MENU KONTEKSTOWE WZORU
		
		/// <summary>
		/// Akcja wykonywana po kliknięciu w element z listy ostatnich wzorów.
		/// Funkcja sprawdza czy wzór o podanej nazwie istnieje i przesuwa go na sam początek.
		/// Jeżeli wzór istnieje, otwiera go do edycji, w przeciwnym wypadku wyświetla komunikat i usuwa go.
		/// </summary>
		/// 
		/// <param name="sender">Obiekt wywołujący zdarzenie.</param>
		/// <param name="ev">Argumenty zdarzenia.</param>
		//* ============================================================================================================
		private void TSMI_PatternItem_Click( object sender, EventArgs ev )
		{
#		if DEBUG
			Program.LogMessage( "Edycja wybranego wzoru z listy ostatnio otwieranych wzorów." );
#		endif

			// wyszukaj wzór na liście
			var pattern = ((ToolStripItem)sender).Text;
			pattern = pattern.Substring( pattern.IndexOf(':') + 2 ).Trim();

			// wyszukaj wzór na liście
			int index = 0;
			for( ; index < this.TV_P1_Patterns.Nodes.Count; ++index )
				if( this.TV_P1_Patterns.Nodes[index].Text == pattern )
					break;

			// brak wzoru na liście
			if( index == this.TV_P1_Patterns.Nodes.Count )
			{
				Program.LogInfo
				(
					Language.GetLine( "Menu", "Messages", (int)LANGCODE.I01_MEN_MSG_PATNEXISTS ),
					Language.GetLine( "MessageNames", (int)LANGCODE.GMN_PATTERNEDITOR ),
					this
				);

				// usuń wzór i odśwież elementy
				Settings.RemoveFromLastPatterns( pattern );
				this.refreshRecentOpenList();

				return;
			}

			// zaznacz i edytuj wzór
			this.TV_P1_Patterns.SelectedNode = this.TV_P1_Patterns.Nodes[index];
			this.TSMI_EditPattern_Click( null, null );
		}

		/// <summary>
		/// Akcja wywoływana podczas otwierania menu kontekstowego.
		/// Funkcja decyduje o tym, które opcje menu mają być dostępne a które nie.
		/// Uzależnione jest to tym, czy kliknięto na wzór z listy czy nie.
		/// </summary>
		/// 
		/// <param name="sender">Obiekt wywołujący zdarzenie.</param>
		/// <param name="ev">Argumenty zdarzenia.</param>
		//* ============================================================================================================
		private void CMS_Pattern_Opening( object sender, CancelEventArgs ev )
		{
#		if DEBUG
			Program.LogMessage( "Otwieranie menu - sprawdzanie dostępnych opcji." );
#		endif

			// odblokuj wszystkie przyciski w menu
			this.TSMI_NewPattern.Enabled    = true;
			this.TSMI_EditPattern.Enabled   = true;
			this.TSMI_LoadData.Enabled      = true;
			this.TSMI_ExportPattern.Enabled = true;
			this.TSMI_RemovePattern.Enabled = true;

			// zablokuj gdy istnieje taka potrzeba
			if( this._selectedID == -1 )
			{
				this.TSMI_EditPattern.Enabled   = false;
				this.TSMI_LoadData.Enabled      = false;
				this.TSMI_ExportPattern.Enabled = false;
				this.TSMI_RemovePattern.Enabled = false;
			}
			else if( this._selectedError )
			{
				this.TSMI_EditPattern.Enabled   = false;
				this.TSMI_LoadData.Enabled      = false;
				this.TSMI_ExportPattern.Enabled = false;
			}
			// zmień nazwę przycisku do wczytywania danych
			else if( this._selectedDynamic )
				this.TSMI_LoadData.Text = Language.GetLine( "PatternList", "PatternContext", (int)LANGCODE.I01_PAT_CTX_LOADDATA );
			else
				this.TSMI_LoadData.Text = Language.GetLine( "PatternList", "PatternContext", (int)LANGCODE.I01_PAT_CTX_PREVIEW );
		}

		/// <summary>
		/// Akcja wywoływana po kliknięciu pozycję z menu dotyczącą ładowania danych do wzoru.
		/// Funkcja uruchamia dwa okna, jedno po drugim, służące do wczytywania danych do zaznaczonego wzoru.
		/// Po wczytaniu danych, funkcja przechodzi do strony z podglądem wzoru.
		/// </summary>
		/// 
		/// <param name="sender">Obiekt wywołujący zdarzenie.</param>
		/// <param name="ev">Argumenty zdarzenia.</param>
		//* ============================================================================================================
		private void TSMI_LoadData_Click( object sender, EventArgs ev )
		{
			this.loadDataForPreview( this._selectedName );
		}

		/// <summary>
		/// Akcja wywoływana po kliknięciu w przycisk tworzący wzór.
		/// Otwiera formularz tworzenia wzoru, po utworzeniu zaznacza go i otwiera do edycji.
		/// </summary>
		/// 
		/// <param name="sender">Obiekt wywołujący zdarzenie.</param>
		/// <param name="ev">Argumenty zdarzenia.</param>
		//* ============================================================================================================
		private void TSMI_MenuNewPattern_Click( object sender, EventArgs ev )
		{
#		if DEBUG
			Program.LogMessage( "** Okno tworzenia wzoru." );
			Program.LogMessage( "** BEGIN ================================================================== **" );
			Program.IncreaseLogIndent();
#		endif
			var window = new NewPatternForm();

			// nowy wzór
			if( window.ShowDialog(this) != DialogResult.OK )
			{
#		if DEBUG
				Program.LogMessage( "Operacja anulowana." );
				Program.DecreaseLogIndent();
				Program.LogMessage( "** END ==================================================================== **" );
#		endif
				return;
			}
#		if DEBUG
			Program.DecreaseLogIndent();
			Program.LogMessage( "** END ==================================================================== **" );
#		endif

			// odśwież listę
			this.refreshProjectList();
			string pattern_name = window.PatternName;

			// zaznacz nowy wzór
			for( int x = 0; x < this.TV_P1_Patterns.Nodes.Count; ++x )
				if( this.TV_P1_Patterns.Nodes[x].Text == pattern_name )
				{
					this.TV_P1_Patterns.SelectedNode = this.TV_P1_Patterns.Nodes[x];
					break;
				}
			
			// przejdź do edycji wzoru
			this.TSMI_EditPattern_Click( null, null );
			
			// dodaj do ostatnio otwieranych
			Settings.AddToLastPatterns( pattern_name );
		}

		/// <summary>
		/// Akcja wywoływana po kliknięciu w przycisk zakończ.
		/// Zamyka program i odblokowuje plik do zapisu błędów.
		/// </summary>
		/// 
		/// <param name="sender">Obiekt wywołujący zdarzenie.</param>
		/// <param name="ev">Argumenty zdarzenia.</param>
		//* ============================================================================================================
		private void TSMI_Close_Click( object sender, EventArgs ev )
		{
			GC.Collect();
			Program.ExitApplication( true );
		}

		/// <summary>
		/// Kompresuje wzór i zapisuje do wybranej lokalizacji.
		/// Sprawdza który przycisk został wciśnięty i kompresuje albo wybrany wzór, albo wszystkie dostępne wzory.
		/// Wyświetla okno zapisu pliku po czym uruchamia proces kompresji danych dla pojedynczego wzoru.
		/// </summary>
		/// 
		/// <param name="sender">Obiekt wywołujący zdarzenie.</param>
		/// <param name="ev">Argumenty zdarzenia.</param>
		//* ============================================================================================================
		private void TSMI_ExportPattern_Click( object sender, EventArgs ev )
		{
#		if DEBUG
			Program.LogMessage( "** Okno wyboru miejsca do zapisu eksportowanego pliku." );
			Program.LogMessage( "** BEGIN ================================================================== **" );
			Program.IncreaseLogIndent();
#		endif
			var single = false;

			// sprawdź czy został wciśnięty przycisk eksportu konkretnego wzoru
			if( (ToolStripMenuItem)sender == this.TSMI_ExportPattern )
				single = true;

			var dialog = Program.GLOBAL.SaveFile;
			var values = Language.GetLines( "Extensions" );

			// jako domyślną nazwę pliku przyjmij nazwę eksportowanego wzoru lub aktualną datę
			dialog.DefaultExt = ".cbd";
			dialog.FileName   = single ? this.TV_P1_Patterns.SelectedNode.Text : DateTime.Now.ToString("yyyyMMddHHmm");
			dialog.Filter     = values[(int)LANGCODE.GEX_CBD] + "|*.cbd";

			// eksportuj wzór
			if( dialog.ShowDialog(this) == DialogResult.OK )
			{
#			if DEBUG
				Program.LogMessage( "Nazwa pliku do zapisu: " + dialog.FileName );
#			endif
				PatternEditor.Export( (single ? this.TV_P1_Patterns.SelectedNode.Text : ""), dialog.FileName );
			}
#		if DEBUG
			else
				Program.LogMessage( "Operacja anulowana." );

			Program.DecreaseLogIndent();
			Program.LogMessage( "** END ==================================================================== **" );
#		endif
		}

		/// <summary>
		/// Akcja wywoływana po kliknięciu w przycisk importu.
		/// Przycisk importu znajduje się w menu "Wzór" lub w menu kontekstowym pojawiającym się
		/// po kliknięciu prawym przyciskiem myszy na nazwie wzoru.
		/// Importuje wzór lub kilka wzorów zapisanych w wybranym pliku.
		/// </summary>
		/// 
		/// <param name="sender">Obiekt wywołujący zdarzenie.</param>
		/// <param name="ev">Argumenty zdarzenia.</param>
		//* ============================================================================================================
		private void TSMI_Import_Click( object sender, EventArgs ev )
		{
#		if DEBUG
			Program.LogMessage( "** Okno wczytywania pliku importu." );
			Program.LogMessage( "** BEGIN ================================================================== **" );
			Program.IncreaseLogIndent();
#		endif
			// wybór pliku
			var select = Program.GLOBAL.SelectFile;
			var values = Language.GetLines( "Extensions" );

			select.DefaultExt = ".cbd";
			select.Title      = Language.GetLine( "MessageNames", (int)LANGCODE.GMN_SELECTFILE );
			select.Filter     = values[(int)LANGCODE.GEX_CBD] + "|*.cbd";

			// wybór anulowany
			if( select.ShowDialog(this) != DialogResult.OK )
			{
#			if DEBUG
				Program.LogMessage( "Operacja anulowana." );
				Program.DecreaseLogIndent();
				Program.LogMessage( "** END ==================================================================== **" );
#			endif
				return;
			}

#		if DEBUG
			Program.LogMessage( "Plik wybrany do importu: " + select.FileName + "." );
			Program.LogMessage( "Uruchamianie importu pliku..." );
#		endif
			// wyświetl pytanie, czy zaimportować pliki
			var result = Program.LogQuestion
			(
				Language.GetLine( "PatternList", "Messages", (int)LANGCODE.I01_PAT_MES_YESIMPORT ),
				Language.GetLine( "MessageNames", (int)LANGCODE.GMN_IMPORTFILE ),
				false,
				this
			);

			// jeżeli tak, zaimportuj i wyświetl informacje o zakończeniu
			if( result == DialogResult.Yes )
			{
				PatternEditor.Import( select.FileName );
				Program.LogInfo
				(
					Language.GetLine( "PatternList", "Messages", (int)LANGCODE.I01_PAT_MES_IMPORTSUCC ),
					Language.GetLine( "MessageNames", (int)LANGCODE.GMN_IMPORTFILE ),
					this
				);
				this.refreshProjectList();
			}
#		if DEBUG
			else
				Program.LogMessage( "Operacja importu została anulowana." );

			Program.DecreaseLogIndent();
			Program.LogMessage( "** END ==================================================================== **" );
#		endif
		}

		/// <summary>
		/// Akcja wywoływana po kliknięciu w przycisk czyszczenia ostatnio otwieranych wzorów.
		/// Funkcja czyści wszystkie wzory zapisane w pliku, dzięki czemu lista ostatnio otwieranych wzorów jest pusta.
		/// </summary>
		/// 
		/// <param name="sender">Obiekt wywołujący zdarzenie.</param>
		/// <param name="ev">Argumenty zdarzenia.</param>
		//* ============================================================================================================
		private void TSMI_ClearRecent_Click( object sender, EventArgs ev )
		{
			// wyczyść ostatnio otwierane projekty
			Settings.LastPatterns = new List<string>();

			// usuń pozycje w menu
			while( this.TSMI_RecentOpen.DropDownItems.Count > 2 )
				this.TSMI_RecentOpen.DropDownItems.RemoveAt( 0 );

#		if DEBUG
			Program.LogMessage( "Czyszczenie listy ostatnio otwieranych wzorów." );
#		endif

			// wyłącz pozycje w menu
			this.TSMI_RecentOpen.Enabled = false;
			GC.Collect();
		}

#endregion

#region FORMULARZ

		/// <summary>
		/// Analiza wciśniętych klawiszy w obrębie formularza.
		/// Funkcja tworzy skróty do szybkiego przełączania pomiędzy kolejnymi stronami wzoru.
		/// </summary>
		/// 
		/// <param name="msg">Przechwycone zdarzenie wciśnięcia klawisza.</param>
		/// <param name="keys">Informacje o wciśniętych klawiszach.</param>
		/// 
		/// <returns>Informacja o tym czy klawisz został przechwycony.</returns>
		//* ============================================================================================================
		protected override bool ProcessCmdKey( ref Message msg, Keys keydata )
		{
			switch( keydata )
			{
			// przełącz stronę z wzorem do przodu
			case Keys.Control | Keys.Up:
				if( this._currentPanel == 1 )
				{
					if( this.N_P1_Page.Value == this.N_P1_Page.Maximum )
						this.N_P1_Page.Value = 1;
					else if( this.N_P1_Page.Value < this.N_P1_Page.Maximum )
						this.N_P1_Page.Value += 1;
				}
				else if( this._currentPanel == 2 )
				{
					if( this.N_P2_Page.Value == this.N_P2_Page.Maximum )
						this.N_P2_Page.Value = 1;
					else if( this.N_P2_Page.Value < this.N_P2_Page.Maximum )
						this.N_P2_Page.Value += 1;
				}
				else if( this._currentPanel == 3 )
				{
					if( this.N_P3_Page.Value == this.N_P3_Page.Maximum )
						this.N_P3_Page.Value = 1;
					else if( this.N_P3_Page.Value < this.N_P3_Page.Maximum )
						this.N_P3_Page.Value += 1;
				}
			break;

			// przełącz stronę z wzorem do tyłu
			case Keys.Control | Keys.Down:
				if( this._currentPanel == 1 )
				{
					if( this.N_P1_Page.Value == 1 )
						this.N_P1_Page.Value = this.N_P1_Page.Maximum;
					else if( this.N_P1_Page.Value > this.N_P1_Page.Minimum )
						this.N_P1_Page.Value -= 1;
				}
				else if( this._currentPanel == 2 )
				{
					if( this.N_P2_Page.Value == 1 )
						this.N_P2_Page.Value = this.N_P2_Page.Maximum;
					else if( this.N_P2_Page.Value > this.N_P2_Page.Minimum )
						this.N_P2_Page.Value -= 1;
				}
				else if( this._currentPanel == 3 )
				{
					if( this.N_P3_Page.Value == 1 )
						this.N_P3_Page.Value = this.N_P3_Page.Maximum;
					else if( this.N_P3_Page.Value > this.N_P3_Page.Minimum )
						this.N_P3_Page.Value -= 1;
				}
			break;

			// zapis wzoru
			case Keys.Control | Keys.S:
				if( this._currentPanel == 2 )
					this.B_P2_Save_Click( null, null );
			break;

			// generowanie z łączeniem stron
			case Keys.Control | Keys.G:
				if( this._currentPanel == 3 )
				{
					this.CB_P3_CollatePages.Checked = true;
					this.B_P3_GeneratePDF_Click( null, null );
				}
			break;
			// generowanie bez łączenia stron
			case Keys.Alt | Keys.G:
				if( this._currentPanel == 3 )
				{
					this.CB_P3_CollatePages.Checked = false;
					this.B_P3_GeneratePDF_Click( null, null );
				}
			break;

			// wczytywanie danych do wzoru
			case Keys.Control | Keys.Q:
				if( this._currentPanel == 2 )
					this.B_P2_LoadData_Click( null, null );
			break;

			// wyszukiwanie błędów w formularzu - generator
			case Keys.Control | Keys.E:
				if( this._currentPanel == 3 )
					this.B_P3_SearchErrors_Click( null, null );
			break;

			// domyślna akcja
			default:
				return base.ProcessCmdKey( ref msg, keydata );
			}

			return true;
		}

		/// <summary>
		/// Akcja wywoływana po zmianie rozmiaru okna.
		/// Zmiana rozmiaru powoduje odświeżenie wielkości panelu zawierającego podgląd wzoru.
		/// Gdy się nie odświeżał, wychodziły różne dziwne niespodzianki.
		/// </summary>
		/// 
		/// <param name="sender">Obiekt wywołujący zdarzenie.</param>
		/// <param name="ev">Argumenty zdarzenia.</param>
		//* ============================================================================================================
		private void Main_Resize( object sender, EventArgs ev )
		{
			if( this.WindowState == this._windowLastState )
				return;

			this._windowLastState = this.WindowState;

			// wymuś odświeżenie i zmiane rozmiaru panelu po wróceniu do normalnego stanu
			// w przeciwnym wypadku rodzic będzie za wielki dla obrazu (dziwne zjawisko...)
			if( this.WindowState == FormWindowState.Normal )
			{
#			if DEBUG
				Console.WriteLine( "Przejście z trybu maksymalizacji - odświeżanie kontrolek." );
#			endif

				this.P_P1_Preview.Width = this.P_P1_Preview.Width - 1;
				this.P_P2_Pattern.Width = this.P_P2_Pattern.Width - 1;
				this.P_P3_Generator.Width = this.P_P3_Generator.Width - 1;
			}
		}

		/// <summary>
		/// Akcja wywoływana po przesunięciu okna.
		/// Przesunięcie okna powoduje ukrycie okna z podpowiedzią (jeżeli jest pokazane).
		/// </summary>
		/// 
		/// <param name="sender">Obiekt wywołujący zdarzenie.</param>
		/// <param name="ev">Argumenty zdarzenia.</param>
		//* ============================================================================================================
		private void Main_Move( object sender, EventArgs ev )
		{
			if( this._tooltipShow )
			{
				this.TP_Tooltip.Hide( this.TB_P2_LabelName );
				this._tooltipShow = false;
			}
		}

		/// <summary>
		/// Akcja wywoływana przy rysowaniu paska menu.
		/// Rysuje linię na samym dole menu górze oddzielającą treść od menu w oknie.
		/// </summary>
		/// 
		/// <param name="sender">Obiekt wywołujący zdarzenie.</param>
		/// <param name="ev">Argumenty zdarzenia.</param>
		//* ============================================================================================================
		private void MS_Main_Paint( object sender, PaintEventArgs ev )
		{
			ev.Graphics.DrawLine
			(
				new Pen( SystemColors.ControlDark ),
				this.MS_Main.Bounds.X,
				this.MS_Main.Bounds.Bottom - 1,
				this.MS_Main.Bounds.Right,
				this.MS_Main.Bounds.Bottom - 1
			);
		}

		/// <summary>
		/// Akcja wywoływana przy rysowaniu paska informacji.
		/// Rysuje linię na samej górze paska oddzielającą treść od paska w oknie.
		/// </summary>
		/// 
		/// <param name="sender">Obiekt wywołujący zdarzenie.</param>
		/// <param name="ev">Argumenty zdarzenia.</param>
		//* ============================================================================================================
		private void TLP_P1_StatusBar_Paint( object sender, PaintEventArgs ev )
		{
			var panel = (TableLayoutPanel)sender;
			ev.Graphics.DrawLine
			(
				new Pen( SystemColors.ControlDark ),
				panel.Bounds.X,
				0,
				panel.Bounds.Right,
				0
			);
		}

		/// @endcond
#endregion
	}
}
