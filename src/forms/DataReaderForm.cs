///
/// $i03 DataReaderForm.cs (I07)
/// 
/// Okno przypisywania kolumn do pól wzoru.
/// Uruchamia okno pozwalające przypisać dane do pól wzoru.
/// Pozwala na przypisanie formatu, w którym będą się potem wyświetlały dane, poprzez zaznaczanie kolumn.
/// Można przypisać pola dla każdej dostępnej strony wzoru.
/// Przypisywanie obrazków na razie nie jest dostępne.
/// 
/// Autor: Kamil Biały
/// Od wersji: 0.2.x.x
/// Ostatnia zmiana: 2016-12-24
/// 
/// CHANGELOG:
/// [05.05.2015] Wersja początkowa.
/// [10.05.2015] Zmiana nazwy pliku i klasy z SetColumns na DataReader, rozszerzona obsługa danych.
/// [17.05.2015] Zapis indeksu pola wzoru do późniejszego użycia.
/// [01.06.2015] Sprawdzanie poprawności danych przy zapisie dla formatu.
/// [09.06.2015] Wyświetlanie dymku podczas dynamicznego sprawdzania poprawności danych.
/// [16.12.2016] Nowa wersja formularza, komentarze, opisy, zmiany nazw funkcji odpowiadające nazwom kontrolek.
///              Poprawki w funkcjach, przeniesienie wczytywania bazy do odpowiednich klas.
///              Usunięcie niepotrzebnych kontrolek, poprawa wyglądu formularza.
///              Tłumaczenia językowe dla formularza.
/// [24.12.2016] Zmiana koncepcji nazw zmiennych.
/// [26.12.2016] Naprawa błędu związanego z przenoszeniem kolumny i upuszczaniem jej na puste pole.
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
	/// Formularz przypisywania kolumn do pól wzoru.
	/// Pozwala na przypisanie kolumny do dostępnego dla wzoru pola na wybranej stronie.
	/// Dane tutaj utworzone wykorzystywane są potem do generowania dokumentu w formacie PDF.
	/// Możliwe jest ustawienie formatu w którym wyświetlane są poszczególne wiersze z bazy danch.
	/// Format można wpisać samemu po uprzednim zaznaczeniu kolumn, które mają się wyświetlać.
	/// </summary>
	///
	public partial class DataReaderForm : Form
	{
#region ZMIENNE

		/// <summary>Informacje o wczytanym wzorze.</summary>
		private PatternData _data;

		/// <summary>Aktualnie zaznaczona kolumna.</summary>
		private ListViewItem _selectedCol;

		/// <summary>Lista zaznaczonych kolumn.</summary>
		private List<int> _checkedCols;

		/// <summary>Aktualnie wyświetlana strona.</summary>
		private int _currentPage;

		/// <summary>Przypisany schowek z danymi do wyświetlenia.</summary>
		private DataStorage  _storage;

		/// <summary>Informacja o tym czy wyświetlany jest dymek z wiadomością.</summary>
		bool _tooltipShow;

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
		public DataReaderForm( PatternData data, DataStorage storage )
		{
			this.InitializeComponent();

			this._data    = data;
			this._storage = storage;

			this._tooltipShow = false;
			this._selectedCol = null;
			this._checkedCols = new List<int>();
			this._currentPage = 0;

			// pobranie ikony
			this.Icon = Program.GetIcon();
			this.translateForm();
		}

		/// <summary>
		/// Pobiera przypisane dane wzoru.
		/// Zawiera wszystkie informacje o wczytanym wzorze.
		/// </summary>
		//* ============================================================================================================
		public PatternData PatternData
		{
			get { return this._data; }
		}
		
		/// <summary>
		/// Pobiera przypisane dane z bazy.
		/// Zawiera dane podzielone na kolumny i rekordy.
		/// </summary>
		//* ============================================================================================================
		public DataStorage DataStorage
		{
			get { return this._storage; }
		}

		/// <summary>
		/// Pobiera listę zaznaczonych kolumn.
		/// Kolumny zaznacza się w kolumnie z listą kolumn zczytywanych z bazy danych.
		/// Właściwość zwraca identyfikatorych zaznaczonych kolumn.
		/// </summary>
		//* ============================================================================================================
		public List<int> CheckedCols
		{
			get { return this._checkedCols; }
		}
		
		/// <summary>
		/// Pobiera ustawiony format wyświetlania danych.
		/// Format opiera się na poszczególnych kolumnach jako numerach.
		/// Przykładowa wartość zwrócona przez właściwość: <value>#1 - #2 :: #3</value>.
		/// </summary>
		//* ============================================================================================================
		public string CheckedFormat
		{
			get { return this.TB_Format.Text; }
		}

#endregion

#region FUNKCJE PODSTAWOWE

		/// <summary>
		/// Odświeżanie danych w oknie.
		/// Okno tworzone wcześniej wymaga odświeżenia.
		/// Aby go nie zwalniać z pamięci, po zamknięciu podczas otwierania warto wywołać tą funkcję.
		/// Odświeża wszystkie dynamiczne dane wyświetlane na formularzu wraz z tłumaczeniami fraz.
		/// </summary>
		/// 
		/// <seealso cref="refreshAndOpen"/>
		/// <seealso cref="Language"/>
		/// 
		/// <returns>Czy odświeżanie kontrolki przebiegło pomyślnie?</returns>
		//* ============================================================================================================
		public bool refreshForm()
		{
#       if DEBUG
			Program.LogMessage( "Odświeżanie wszystkich potrzebnych danych." );
#       endif

			if( this._data.Pages < 1 || this._storage.ColumnsNumber == 0 || this._storage.RowsNumber == 0 )
				return false;

			// uzupełnij tabele
			for( int x = 0; x < this._storage.ColumnsNumber; ++x )
				this.LV_DatabaseCols.Items.Add( this._storage.Column[x] );

			//var image_list = new ImageList();
			//image_list.ColorDepth = ColorDepth.Depth32Bit;

			//// wczytaj ikony
			//var image = Program.GetBitmap( BITMAPCODE.IMAGEFIELD );
			//image_list.Images.Add( "image-field", image );
			//image = Program.GetBitmap( BITMAPCODE.TEXTFIELD );
			//image_list.Images.Add( "text-field", image );

			// ustaw listę obrazków
			//this.LV_PageFields.SmallImageList = image_list;

			// uzupełnij tabele
			for( int x = 0; x < this._data.Page[0].Fields; ++x )
			{
				var field_data = this._data.Page[0].Field[x];

				// pomiń pola, których nie można przypisać
				if( !field_data.Extra.TextFromDB && !field_data.Extra.ImageFromDB )
					continue;

				// dodaj pola
				var item = this.LV_PageFields.Items.Add( field_data.Name );
				item.Tag = (object)x;

				// ustaw ikonki
				//if( field_data.Extra.TextFromDB )
				//    item.ImageKey = "text-field";
				//else if( field_data.Extra.ImageFromDB )
				//    item.ImageKey = "image-field";

				item.SubItems.Add("");
			}

			// ustaw maksymalną wartość dla pola numerycznego
			this.N_Page.Maximum = this._data.Pages;

			this.translateForm();
			return true;
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
		/// <param name="modal">Czy wyświetlić jako okno modalne?</param>
		/// 
		/// <returns>Wartość zwracana przez okno modalne lub DialogResult.None.</returns>
		/// 
		/// <seealso cref="refreshForm"/>
		/// <seealso cref="Language"/>
		//* ============================================================================================================
		public DialogResult refreshAndOpen( Form parent = null, bool modal = true )
		{
			if( this.refreshForm() )
				return Program.OpenForm( this, parent, modal );
			return DialogResult.None;
		}

		/// <summary>
		/// Translator formularza.
		/// Funkcja tłumaczy wszystkie statyczne elementy programu.
		/// Wywoływana jest z konstruktora oraz podczas odświeżania ustawień językowych.
		/// Jej użycie nie powinno wykraczać poza dwa wyżej wymienione przypadki.
		/// </summary>
		/// 
		/// <seealso cref="DataReaderForm"/>
		/// <seealso cref="Language"/>
		//* ============================================================================================================
		protected void translateForm()
		{
#		if DEBUG
			Program.LogMessage( "Tłumaczenie kontrolek znajdujących się na formularzu." );
#		endif

			// kolumny
			var values = Language.GetLines( "DataReader", "Headers" );
			
			this.CH_DatabaseCols.Text = values[(int)LANGCODE.I08_HEA_DBASECOLS];
			this.CH_PageCols.Text     = values[(int)LANGCODE.I08_HEA_PAGECOLS];
			this.CH_PageFields.Text   = values[(int)LANGCODE.I08_HEA_PAGEFIELDS];

			// strona
			this.L_Page.Text = Language.GetLine( "DataReader", "Labels", (int)LANGCODE.I08_LAB_PAGE );

			// przyciski
			values = Language.GetLines( "DataReader", "Buttons" );

			this.B_Save.Text   = values[(int)LANGCODE.I08_BUT_SAVE];
			this.B_Cancel.Text = values[(int)LANGCODE.I08_BUT_CANCEL];

			// tytuł formularza
			this.Text = Language.GetLine( "FormNames", (int)LANGCODE.GFN_DATAREADER );
		}

#endregion

#region OBSŁUGA KOLUMN
		/// @cond EVENTS

		/// <summary>
		/// Akcja wywoływana po naciśnięciu przycisku na elemencie w liście kolumn.
		/// Po kliknięciu w nazwę kolumny ze schowka możliwe jest dzięki temu jej przeniesienie do nowych kolumn.
		/// Ułatwia to w znacznym stopniu możliwość łączenia kolumn.
		/// Drag & Drop.
		/// </summary>
		/// 
		/// <param name="sender">Obiekt wywołujący zdarzenie.</param>
		/// <param name="ev">Argumenty zdarzenia.</param>
		//* ============================================================================================================
		private void LV_DatabaseCols_MouseDown( object sender, MouseEventArgs ev )
		{
			var item = this.LV_DatabaseCols.GetItemAt( ev.X, ev.Y );

			if( item == null )
				return;

			// zaznacz wybrany element
			item.Selected = true;

			// pobierz element i wywołaj funkcję przeciągania (DragAndDrop)
			var rows = this.LV_DatabaseCols.SelectedItems;
			foreach( var row in rows )
				this.LV_DatabaseCols.DoDragDrop( row, DragDropEffects.Move );
		}
		
		/// <summary>
		/// Akcja wywoływana podczas najechania na element podczas przeciągania.
		/// Drag & Drop.
		/// </summary>
		/// 
		/// <param name="sender">Obiekt wywołujący zdarzenie.</param>
		/// <param name="ev">Argumenty zdarzenia.</param>
		//* ============================================================================================================
		private void LV_DatabaseCols_DragOver( object sender, DragEventArgs ev )
		{
			ev.Effect = DragDropEffects.Move;
		}

		/// <summary>
		/// Akcja wywoływana po zmianie zaznaczenia kolumny dla bazy danych.
		/// Funkcja odświeża wartość kontrolki zawierającej format wyświetlanych danych.
		/// </summary>
		/// 
		/// <param name="sender">Obiekt wywołujący zdarzenie.</param>
		/// <param name="ev">Argumenty zdarzenia.</param>
		//* ============================================================================================================
		private void LV_DatabaseCols_ItemChecked( object sender, ItemCheckedEventArgs ev )
		{
#		if DEBUG
			Program.LogMessage( "Zmiana zaznaczonego elementu." );
#		endif

			int counter = 0;
			this.TB_Format.Text = "";

			// zmień format
			foreach( ListViewItem item in this.LV_DatabaseCols.Items )
				if( item.Checked && counter == 0 )
					this.TB_Format.Text = "#" + (++counter);
				else if( item.Checked )
					this.TB_Format.Text += " " + "#" + (++counter);
		}

		/// <summary>
		/// Akcja wywoływana po wejściu w tryb przeciągania elementów.
		/// Drag & Drop.
		/// </summary>
		/// 
		/// <param name="sender">Obiekt wywołujący zdarzenie.</param>
		/// <param name="ev">Argumenty zdarzenia.</param>
		//* ============================================================================================================
		private void LV_PageFields_DragEnter( object sender, DragEventArgs ev )
		{
			ev.Effect = DragDropEffects.Move;
		}

		/// <summary>
		/// Akcja wywoływana podczas najechania na element podczas przeciągania.
		/// Dzięki tej funkcji po najechaniu podświetla się kolumna na którą wskazuje kursor myszy.
		/// Drag & Drop.
		/// </summary>
		/// 
		/// <param name="sender">Obiekt wywołujący zdarzenie.</param>
		/// <param name="ev">Argumenty zdarzenia.</param>
		//* ============================================================================================================
		private void LV_PageFields_DragOver( object sender, DragEventArgs ev )
		{
			// wskazywany element
			var client_point = this.LV_PageFields.PointToClient( new Point(ev.X, ev.Y) );
			var item = this.LV_PageFields.GetItemAt( client_point.X, client_point.Y );

			// zabezpieczenie przed zaznaczeniem kilku (lub miganiem zaznaczonych elementów)
			if( this._selectedCol == item )
				return;
			
			if( this._selectedCol != null )
				this._selectedCol.Selected = false;

			this._selectedCol = item;

			if( this._selectedCol != null )
			{
				this._selectedCol.Selected = true;
#   		if DEBUG
				Program.LogMessage( "Zaznaczone pole: '" + this._selectedCol.Index + "'." );
#	    	endif
			}
		}

		/// <summary>
		/// Akcja wywoływana po puszczeniu klawisza myszy podczas przeciągania.
		/// Po przeciągnięciu przypisuje przeciąganą kolumnę z zaznaczonym polem wzoru.
		/// Funkcja nadpisuje zapisane już pola nowymi, które zostaną upuszczone.
		/// Drag & Drop.
		/// </summary>
		/// 
		/// <param name="sender">Obiekt wywołujący zdarzenie.</param>
		/// <param name="ev">Argumenty zdarzenia.</param>
		//* ============================================================================================================
		private void LV_PageFields_DragDrop( object sender, DragEventArgs ev )
		{
			// wskazywany element
			var client_point = this.LV_PageFields.PointToClient( new Point(ev.X, ev.Y) );
			var item = this.LV_PageFields.GetItemAt( client_point.X, client_point.Y );

			// brak elementu
			if( item == null )
				return;

			// przeciągany wiersz
			var copy = (ListViewItem)ev.Data.GetData( typeof(ListViewItem) );
			item.SubItems[1].Text = copy.Text;

			// sprawdź czy kontrolka ma odpowiedni kolor
			if( copy.BackColor != SystemColors.Window )
				copy.BackColor = SystemColors.Window;

			// zapisz indeks przeciąganej kolumny
			this._data.Page[this._currentPage].Field[(int)item.Tag].Extra.Column = copy.Index;
			
#		if DEBUG
			Program.LogMessage( "Zapisano kolumnę '" + copy.Index + "' dla pola '" + ((int)item.Tag) + "'." );
#		endif
		}

		/// <summary>
		/// Akcja wywoływana po wciśnięciu przycisku przy skupieniu na kontrolce z listą pól.
		/// Funkcja powoduje usunięcie przypisanej kolumny do wybranego pola.
		/// </summary>
		/// 
		/// <param name="sender">Obiekt wywołujący zdarzenie.</param>
		/// <param name="ev">Argumenty zdarzenia.</param>
		//* ============================================================================================================
		private void LV_PageFields_KeyDown( object sender, KeyEventArgs ev )
		{
			// usuń przypisaną kolumnę
			if( ev.KeyCode == Keys.Delete && this.LV_PageFields.SelectedItems.Count > 0 )
			{
				var item = this.LV_PageFields.SelectedItems[0];
				item.SubItems[1].Text = "";
				this._data.Page[this._currentPage].Field[(int)item.Tag].Extra.Column = -1;
#		    if DEBUG
				Program.LogMessage( "Pole o numerze '" + item.Index + "' zostało wyczyszczone." );
#		    endif
			}
		}

#endregion

#region PASEK DOLNY

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

		/// <summary>
		/// Akcja wywoływana po zmianie strony wzoru.
		/// Funkcja pozwala na zmianę aktualnej strony wczytanego wzoru.
		/// Odświeża listę pól dostępnych na stronie i przypisanych do nich kolumn.
		/// </summary>
		/// 
		/// <param name="sender">Obiekt wywołujący zdarzenie.</param>
		/// <param name="ev">Argumenty zdarzenia.</param>
		//* ============================================================================================================
		private void N_Page_ValueChanged( object sender, EventArgs ev )
		{
			int page = (int)this.N_Page.Value - 1;

			// nie pozwalaj na wpisanie liczby z innego zakresu
			if( this._data.Page.Length <= page || page < 0 )
				return;
			this.LV_PageFields.Items.Clear();

			var page_data = this._data.Page[page];

			// dodaj rekordy do tabeli
			for( int x = 0; x < page_data.Fields; ++x )
			{
				var field_data = page_data.Field[x];
				
				// pomiń pola których nie można przypisać
				if( !field_data.Extra.TextFromDB && !field_data.Extra.ImageFromDB )
					continue;

				// dodaj element
				this.LV_PageFields.Items.Add( field_data.Name );
				int index = this.LV_PageFields.Items.Count - 1;

				// przyporządkuj odpowiedni obrazek
				//if( field_data.Extra.TextFromDB )
				//    this.LV_PageFields.Items[index].ImageKey = "text-field";
				//else if( field_data.Extra.ImageFromDB )
				//    this.LV_PageFields.Items[index].ImageKey = "image-field";

				// ustaw kolumnę dla pola
				if( field_data.Extra.Column == -1 )
					this.LV_PageFields.Items[index].SubItems.Add("");
				else
					try { this.LV_PageFields.Items[index].SubItems.Add( this._storage.Column[field_data.Extra.Column] ); }
					catch
					{
						this.LV_PageFields.Items[index].SubItems.Add(
							Language.GetLine( "DataReader", "Labels", (int)LANGCODE.I08_LAB_NOCOLUMN )
						);
					}
				this.LV_PageFields.Items[index].Tag = (object)x;
			}

			// przypisz aktualną stronę
			this._currentPage = page;
			
#		    if DEBUG
				Program.LogMessage( "Zmiana wyświetlanej strony na '" + page + "'." );
#		    endif
		}

		/// <summary>
		/// Akcja wywoływana po naciśnięciu klawisza w kontrolce z formatem.
		/// W przypadku gdy podany zostanie błędny znak, wyświetony zostanie dymek informacyjny.
		/// </summary>
		/// 
		/// <param name="sender">Obiekt wywołujący zdarzenie.</param>
		/// <param name="ev">Argumenty zdarzenia.</param>
		//* ============================================================================================================
		private void TB_Format_KeyPress( object sender, KeyPressEventArgs ev )
		{
			if( ev.KeyChar == 8 || ModifierKeys == Keys.Control )
				return;
			
			// sprawdź poprawność danych
			var lang_chars   = Language.GetLines( "Locale" );
			var locale_chars = lang_chars[(int)LANGCODE.GLO_BIGCHARS] + lang_chars[(int)LANGCODE.GLO_SMALLCHARS];
			var regex        = new Regex( @"^[0-9a-zA-Z" + locale_chars + @" \-+_#\:\[\]\<\>]+$" );

			if( !regex.IsMatch(ev.KeyChar.ToString()) )
			{
				// pokaż dymek
				if( !this._tooltipShow )
				{
					this.TP_Tooltip.Show
					(
						Language.GetLine( "DataReader", "Messages", (int)LANGCODE.I08_MES_AVAILCHARS ),
						this.TB_Format,
						new Point( 0, this.TB_Format.Height + 2 )
					);
					this._tooltipShow = true;
				}
				ev.Handled = true;
				System.Media.SystemSounds.Beep.Play();
				return;
			}

			// ukryj dymek
			this.DataReader_Move( null, null );
		}
		
		/// <summary>
		/// Akcja wywoływana po zmianie kontrolki z formatem na inną kontrolkę.
		/// Pozwala na schowanie wyświetlanego dymku z podpowiedzią.
		/// </summary>
		/// 
		/// <param name="sender">Obiekt wywołujący zdarzenie.</param>
		/// <param name="ev">Argumenty zdarzenia.</param>
		//* ============================================================================================================
		private void TB_Format_Leave( object sender, EventArgs ev )
		{
			this.DataReader_Move( null, null );
		}

		/// <summary>
		/// Akcja wywoływana po kliknięciu w przycisk zapisu.
		/// Poowoduje przeskanowanie formatu i zamknięcie okna dialogowego.
		/// W przypadku błędnych znaków wyświetlona zostanie wiadomość.
		/// </summary>
		/// 
		/// <param name="sender">Obiekt wywołujący zdarzenie.</param>
		/// <param name="ev">Argumenty zdarzenia.</param>
		//* ============================================================================================================
		private void B_Save_Click( object sender, EventArgs ev )
		{
#		    if DEBUG
				Program.LogMessage( "Próba zapisu danych w formularzu." );
#		    endif

			bool empty = this.TB_Format.Text.Trim() == "";

			// sprawdź poprawność danych
			var lang_chars   = Language.GetLines( "Locale" );
			var locale_chars = lang_chars[(int)LANGCODE.GLO_BIGCHARS] + lang_chars[(int)LANGCODE.GLO_SMALLCHARS];
			var regex        = new Regex( @"^[0-9a-zA-Z" + locale_chars + @" \-+_#\:\[\]\<\>]+$" );

			this._checkedCols.Clear();
			
			// sprawdź czy format jest pusty
			if( empty )
			{
				Program.LogWarning
				(
					Language.GetLine( "DataReader", "Messages", (int)LANGCODE.I08_MES_ISEMPTY ),
					Language.GetLine( "FormNames", (int)LANGCODE.GFN_DATAREADER ),
					this
				);
				return;
			}

			// pobierz zaznaczone
			for( int x = 0; x < this.LV_DatabaseCols.Items.Count; ++x )
				if( this.LV_DatabaseCols.Items[x].Checked )
				{
					this._checkedCols.Add(x);

					if( this.TB_Format.Text.IndexOf("#" + this._checkedCols.Count) < 0 || empty )
					{
						DialogResult result = Program.LogQuestion
						(
							String.Format( Language.GetLine("DataReader", "Messages", (int)LANGCODE.I08_MES_WOUTCOLUMN),
								this.LV_DatabaseCols.Items[x].Text, this._checkedCols.Count ),
							Language.GetLine( "FormNames", (int)LANGCODE.GFN_DATAREADER ),
							false,
							this
						);
						if( result == DialogResult.No )
							return;
					}
				}

			// przeskanuj pod kątem poprawności
			if( !regex.IsMatch(this.TB_Format.Text) )
			{
				Program.LogWarning
				(
					Language.GetLine( "DataReader", "Messages", (int)LANGCODE.I08_MES_AVAILCHMSG ),
					Language.GetLine( "MessageNames", (int)LANGCODE.GFN_DATAREADER ),
					this
				);
				return;
			}

			this.DialogResult = DialogResult.OK;
			this.Close();
		}

#endregion

#region FORMULARZ

		/// <summary>
		/// Akcja wywoływana po przesunięciu okna.
		/// Przesunięcie okna powoduje ukrycie okna z podpowiedzią (jeżeli jest pokazane).
		/// </summary>
		/// 
		/// <param name="sender">Obiekt wywołujący zdarzenie.</param>
		/// <param name="ev">Argumenty zdarzenia.</param>
		//* ============================================================================================================
		private void DataReader_Move( object sender, EventArgs ev )
		{
			if( this._tooltipShow )
			{
				this.TP_Tooltip.Hide( this.TB_Format );
				this._tooltipShow = false;
			}
		}

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
				// zmień strone wzoru do przodu
				case Keys.Control | Keys.Up:
					if( this.N_Page.Value < this.N_Page.Maximum )
						this.N_Page.Value += 1;
				break;
				// zmień strone wzoru do tyłu
				case Keys.Control | Keys.Down:
					if( this.N_Page.Value > this.N_Page.Minimum )
						this.N_Page.Value -= 1;
				break;
				default:
					return base.ProcessCmdKey( ref msg, keydata );
			}
			return true;
		}

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

		/// @endcond
#endregion
	}
}
