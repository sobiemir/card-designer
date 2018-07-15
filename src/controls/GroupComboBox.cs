using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Diagnostics;
using System.Windows.Forms.VisualStyles;
using System.Drawing;

///
/// $i06 GroupComboBox.cs
/// 
/// Nowa kontrolka listy z możliwością grupowania elementów.
/// Używana głównie w filtrowaniu kolumn.
/// 
/// Autor: Kamil Biały
/// Od wersji: 0.8.x.x
/// Ostatnia zmiana: 2015-08-06
///

namespace CDesigner
{
	/// 
	/// Rozszerzenie kontrolki listy rozwijanej.
	/// Pozwala na grupowanie elementów.
	/// Rysuje charakterystyczne dla drzewa (TreeView) linie obok elementów.
	/// 
	public class GroupComboBox : ComboBox
	{
		/// Przłącznik wykonania funkcji inizjalizacyjnej.
		private static bool _initialized = false;

		/// Domyślna wysokość elementu listy.
		private static int _dn_item_height = 15;

		/// Domyślna wysokość elementu na kontrolce.
		private static int _dc_item_height = 15;

		/// Ikona dziecka w drzewie elementów...
		private static Image _combo_child = null;

		/// Ikona zakończenia w drzewie elementów...
		private static Image _combo_end = null;

		/// Ikona początku w drzewie elementów...
		private static Image _combo_start = null;

		/// Ikona rodzica w drzewie elementów...
		private static Image _combo_parent = null;

		/// Ikona dla pojedynczego elementu na liście.
		private static Image _combo_one = null;

		/// Kolekcja elementów kontrolki.
		private GroupComboBoxItems _collection = null;

		/// Flaga synchronizacji, jeżeli ustawiona, synchronizacja elementów jest włączona.
		private bool _synchronized = false;

		/// Wysokość elementu listy.
		private int _item_height = 0;

		// Mnożnik wcięcia wewnętrznego elementu podrzędnego.
		private int _padding_factor = 18;

		/// Ostatnio zaznaczony element (kliknięcie myszką).
		private int _prev_selected_index = -1;

		/// 
		/// Pobranie elementów kontrolki...
		/// ------------------------------------------------------------------------------------------------------------
		public new GroupComboBoxItems Items
		{
			get { return this._collection; }
		}

		/// 
		/// Typ rysowania kontrolki.
		/// W trakcie tworzenia ustawiany jest na CBS_OWNERDRAWFIXED (DrawMode.OwnerDrawFixed).
		/// ------------------------------------------------------------------------------------------------------------
		public new DrawMode DrawMode
		{
			get { return base.DrawMode; }
			private set { base.DrawMode = value; }
		}

		/// 
		/// Styl listy rozwijanej.
		/// Ustawiany tylko w liście (private).
		/// ------------------------------------------------------------------------------------------------------------
		public new ComboBoxStyle DropDownStyle
		{
			get { return base.DropDownStyle; }
			private set { base.DropDownStyle = value; }
		}

		/// 
		/// Mnożnik wcięcia wewnętrznego elementu podrzędnego.
		/// Innymi słowy, mnożnik marginesu wewnętrznego elementu przydzielonego do grupy.
		/// ------------------------------------------------------------------------------------------------------------
		public int ItemPaddingFactor
		{
			get { return this._padding_factor; }
			private set
			{
				this._padding_factor = value;
				this.RefreshItems( );
			}
		}

		/// 
		/// Zmiana wysokości elementu listy rozwijanej.
		/// ------------------------------------------------------------------------------------------------------------
		public new int ItemHeight
		{
			get { return this._item_height; }
			private set
			{
				base.ItemHeight = value;
				this._item_height = value;

				GroupComboBox.SendMessage
				(
					this.Handle,
					WinAPIConst.CB_SETITEMHEIGHT,
					(IntPtr)(-1),
					(IntPtr)GroupComboBox._dc_item_height
				);

				this.DropDownHeight = this._item_height * 15 + SystemInformation.BorderSize.Height * 2;
			}
		}

		/// 
		/// Pobieranie zaznaczenia i zaznaczanie wybranych elementów...
		/// ------------------------------------------------------------------------------------------------------------
		public new GroupComboBoxItem SelectedItem
		{
			get { return this._collection[this.SelectedIndex]; }
			set
			{
				// wyszukaj element i zaznacz go
				for( int x = 0; x < this._collection.Count; ++x )
					if( this._collection[x] == value )
					{
						this.SelectedIndex = x;
						break;
					}
			}
		}

		/// 
		/// Informacja o tym, czy zawartość kontrolki jest synchronizowana z innymi.
		/// W tym wypadku kontrolka nie posiada jawnej listy elementów.
		/// ------------------------------------------------------------------------------------------------------------
		public bool IsSynchronized
		{
			get { return this._synchronized; }
		}

		/// 
		/// Funkcja do wysyłania wiadomości do wybranej kontrolki.
		/// Można zmieniać lub pobierać informacje o kontrolce dzięki tej funkcji.
		/// ------------------------------------------------------------------------------------------------------------
		[DllImport("user32.dll")]
		public static extern int SendMessage( IntPtr hWnd, int wMsg, IntPtr wParam, IntPtr lParam );

		/// 
		/// Konstruktor klasy GroupComboBox.
		/// ------------------------------------------------------------------------------------------------------------
		public GroupComboBox( ) : base( )
		{
			if( !GroupComboBox._initialized )
				GroupComboBox.InitializeVars( );

			this._item_height = GroupComboBox._dn_item_height;
			this._collection  = new GroupComboBoxItems( base.Items );

			this.DrawMode       = DrawMode.OwnerDrawFixed;
			this.DropDownHeight = this._item_height * 10;
			this.DropDownStyle  = ComboBoxStyle.DropDownList;
			this.ItemHeight     = 16;
		}

		/// 
		/// Pobranie domyślnej wysokości elementu listy rozwijanej.
		/// Jakimś cudem domyślnie ustawiane jest 16 zaś w polu ItemHeight 15, co daje przykry rezultat...
		/// To samo tyczy się domyślnej wysokości elementu na kontrolce (wybranego elementu).
		/// ------------------------------------------------------------------------------------------------------------
		private static void InitializeVars( )
		{
			ComboBox combo = new ComboBox( );

			int dn_item_height = GroupComboBox.SendMessage
			(
				combo.Handle,
				WinAPIConst.CB_GETITEMHEIGHT,
				(IntPtr)0,
				IntPtr.Zero
			);
			int dc_item_height = GroupComboBox.SendMessage
			(
				combo.Handle,
				WinAPIConst.CB_GETITEMHEIGHT,
				(IntPtr)(-1),
				IntPtr.Zero
			);

			GroupComboBox._dn_item_height = dn_item_height;
			GroupComboBox._dc_item_height = dc_item_height;
			GroupComboBox._initialized    = true;

			// pobierz obrazki dla "drzewa" w liście rozwijanej
			try
			{
				GroupComboBox._combo_child  = Image.FromFile( "./icons/combo-child.png" );
				GroupComboBox._combo_end    = Image.FromFile( "./icons/combo-end.png" );
				GroupComboBox._combo_parent = Image.FromFile( "./icons/combo-parent.png" );
				GroupComboBox._combo_start  = Image.FromFile( "./icons/combo-start.png" );
				GroupComboBox._combo_one    = Image.FromFile( "./icons/combo-one.png" );
			}
			catch( Exception ex ) { Program.LogMessage( ex.Message ); }
		}

		/// 
		/// Automatyczne dopasowanie szerokości listy rozwijanej do elementów.
		/// ------------------------------------------------------------------------------------------------------------
		public void CalculateDropDownWidth( )
		{
			int max_size = 0;

			foreach( GroupComboBoxItem item in this.Items )
			{
				// pobierz rozmiar tekstu
				Size size  = TextRenderer.MeasureText( item.text, this.Font );
				int  width = size.Width + item.indent * this._padding_factor;

				if( max_size < width )
					max_size = width;
			}

			// dodaj do szerokości rozmiar panelu do przesuwania kontenera gdy jest za dużo elementów
			if( this.Items.Count * this.ItemHeight > this.DropDownHeight )
				max_size += SystemInformation.VerticalScrollBarWidth;

			this.DropDownWidth = max_size;
		}

		/// 
		/// Funkcja odbierająca sygnały wchodzące do kontrolki.
		/// Wysyła wiadomość o rysowaniu elemetu (OCM_DRAWITEM).
		/// Pozwala również na blokowanie kliknięć w nazwy grup...
		/// ------------------------------------------------------------------------------------------------------------
		protected override void WndProc( ref Message msg )
		{

			// wysyłanie dodatkowej wiadomości (REFLECT MESSAGES)
			if( msg.Msg == WinAPIConst.WM_DRAWITEM )
				GroupComboBox.SendMessage( this.Handle, WinAPIConst.OCM_DRAWITEM, msg.WParam, msg.LParam );

			// blokowanie zaznaczania nagłówka
			else if( msg.Msg == WinAPIConst.WM_COMMAND )
			{
				int wparam = (int)msg.WParam >> 16;

				if( wparam == WinAPIConst.CBN_SELCHANGE )
				{
					// pobierz zaznaczony element
					int cursel = GroupComboBox.SendMessage
					(
						this.Handle,
						WinAPIConst.CB_GETCURSEL,
						IntPtr.Zero,
						IntPtr.Zero
					);

					// sprawdź czy element jest nagłówkiem
					if( this.Items[cursel].header )
					{
						// sprawdź czy nagłówek posiada elementy podrzędne
						while( ++cursel < this.Items.Count )
							if( !this.Items[cursel].header )
								break;

						// brak elementów w nagłówku...
						if( cursel >= this.Items.Count )
						{
							// zmień zaznaczony element na poprzedni
							GroupComboBox.SendMessage
							(
								this.Handle,
								WinAPIConst.CB_SETCURSEL,
								(IntPtr)this._prev_selected_index,
								IntPtr.Zero
							);
							return;
						}
						// zmień zaznaczony element na podrzędny
						GroupComboBox.SendMessage
						(
							this.Handle,
							WinAPIConst.CB_SETCURSEL,
							(IntPtr)cursel,
							IntPtr.Zero
						);
					}
					this._prev_selected_index = cursel;
				}
			}

			base.WndProc( ref msg );
		}

		/// 
		/// Ustawianie domyślnych wysokości elementów dla listy i kontrolki.
		/// ------------------------------------------------------------------------------------------------------------
		protected override void OnCreateControl( )
		{
			base.OnCreateControl( );

			// ustaw wysokość elementów dla listy rozwijanej i elementu na kontrolce
			GroupComboBox.SendMessage
			(
				this.Handle,
				WinAPIConst.CB_SETITEMHEIGHT,
				(IntPtr)0,
				(IntPtr)this._item_height
			);
			GroupComboBox.SendMessage
			(
				this.Handle,
				WinAPIConst.CB_SETITEMHEIGHT,
				(IntPtr)(-1),
				(IntPtr)GroupComboBox._dc_item_height
			);
		}

		/// 
		/// Sciemnianie lub rozjaśnianie koloru.
		/// Wartości "factor" od -1.0 do 1.0 - ujemne ściemniają, dodatnie rozjaśniają.
		/// ------------------------------------------------------------------------------------------------------------
		private Color ColorBrightness( Color color, float factor )
		{
			float r = (float)color.R;
			float g = (float)color.G;
			float b = (float)color.B;

			if( factor < 0.0f )
			{
				factor = 1.0f + factor;

				r *= factor;
				g *= factor;
				b *= factor;

				return Color.FromArgb( color.A, (int)r, (int)g, (int)b );
			}

			r = (255 - r) * factor + r;
			g = (255 - g) * factor + g;
			b = (255 - b) * factor + b;

			return Color.FromArgb( color.A, (int)r, (int)g, (int)b );
		}

		///
		/// Rysowanie elementu na kontrolce lub na liście rozwijanej.
		/// I tak czcionki przesuwają się o 1 px za daleko w lewo (nie mam pojęcia dlaczego...)
		/// ------------------------------------------------------------------------------------------------------------
		protected override void OnDrawItem( DrawItemEventArgs ev )
		{
			base.OnDrawItem( ev );

			// brak zaznaczonego elementu
			if( ev.Index < 0 )
			{
				ev.DrawFocusRectangle();
				return;
			}

			GroupComboBoxItem item  = this.Items[ev.Index];
			TextFormatFlags   flags = TextFormatFlags.NoPadding | TextFormatFlags.VerticalCenter
									| TextFormatFlags.SingleLine;

			// element na kontrolce
			if( (ev.State & DrawItemState.ComboBoxEdit) == DrawItemState.ComboBoxEdit )
			{
				Rectangle rect = ev.Bounds;

				rect.X     += 1;
				rect.Width -= 1;

				TextRenderer.DrawText( ev.Graphics, item.text, this.Font, rect, this.ForeColor, flags );
				ev.DrawFocusRectangle( );

				return;
			}

			// VS wywala błąd o tym że zmienna "rect" jest zadeklarowana wyżej... paranoja
			// dlatego utworzony jest w tym miejscu nowy blok
			{
				Font      font  = ev.Font;
				Rectangle rect  = ev.Bounds;
				Color     color = ev.BackColor;
				Color     fore  = ev.ForeColor;

				rect.X     += item.indent * this._padding_factor + 2;
				rect.Width -= item.indent * this._padding_factor + 4;
				
				// zmień tło dla grupy
				if( item.header )
				{
					color = this.ColorBrightness( GroupComboBox.DefaultBackColor, -0.07f );
					fore  = GroupComboBox.DefaultForeColor;
				}

				// rysuj tło
				ev.Graphics.FillRectangle( new SolidBrush(color), ev.Bounds );
				TextRenderer.DrawText( ev.Graphics, item.text, font, rect, fore, flags );

				// sprawdź czy obrazki drzewa są dostępne
				if( GroupComboBox._combo_child == null )
					return;

				// nie rysuj linii dla pierwszych rodziców
				if( item.parent == null )
					return;

				// ikona środka lub zakończenia listy
				Image icon = item.last
					? GroupComboBox._combo_end
					: GroupComboBox._combo_child;

				// ikona początku listy w grupie
				if( item.first && item.parent.parent == null )
					icon = GroupComboBox._combo_start;
				
				// element jest pierwszy i ostatni, nie rysuj nic
				if( item.first && item.last && item.parent.parent == null )
					icon = GroupComboBox._combo_one;
				
				// rysuj ikonę
				if( icon != null )
				{
					rect.X -= GroupComboBox._combo_child.Width + 2;
					ev.Graphics.DrawImage ( icon, rect.X, rect.Y, 16.0f, 16.0f );
				}

				GroupComboBoxItem parent = item.parent;
				icon = GroupComboBox._combo_parent;

				// ikony rodzica
				while( parent.parent != null )
				{
					// z założenia wszystkie ikony mają 16x16
					rect.X -= GroupComboBox._combo_child.Width + 2;

					if( !parent.last && icon != null )
						ev.Graphics.DrawImage( icon, rect.X, rect.Y, 16.0f, 16.0f );
					
					parent = parent.parent;
				}
			}
		}

		///
		/// Rysowanie elementu na kontrolce lub na liście rozwijanej.
		/// I tak czcionki przesuwają się o 1 px za daleko w lewo (nie mam pojęcia dlaczego...)
		/// ------------------------------------------------------------------------------------------------------------
		public ComboBox.ObjectCollection SetSyncItems( GroupComboBoxItems collection )
		{
			this._synchronized = true;
			this._collection = collection;

			// wyczyść stare elementy
			base.Items.Clear( );

			// dodaj elementy do listy
			foreach( GroupComboBoxItem item in collection )
				base.Items.Add( item.text );

			return base.Items;
		}
	}


	/// 
	/// Klasa rozszerzająca listy.
	/// Głównym celem jest wykrycie miejca, gdzie można dodać podany do listy element.
	///
	public class GroupComboBoxItems : List<GroupComboBoxItem>
	{
		/// Kolekcja elementów listy rozwijanej.
		protected ComboBox.ObjectCollection _collection = null;

		///
		/// Konstruktor klasy GroupComboBoxItems.
		/// ------------------------------------------------------------------------------------------------------------
		public GroupComboBoxItems( ComboBox.ObjectCollection items = null ) : base()
		{
			this._collection = items;
		}

		///
		/// Dodawanie elementu do listy.
		/// Wariant z podawaniem poszczególnych parametrów klasy GroupComboBoxItem.
		/// Automatycznie tworzy i zwraca klasę z danymi elementu.
		/// ------------------------------------------------------------------------------------------------------------
		public GroupComboBoxItem Add( string text, bool header = false, GroupComboBoxItem parent = null )
		{
			GroupComboBoxItem item = new GroupComboBoxItem( );

			item.text   = text;
			item.header = header;
			item.indent = parent == null ? 0 : parent.indent + 1;
			item.parent = parent;
			item.last   = false;
			item.first  = false;
			item.child  = new List<GroupComboBoxItem>( );

			this.Add( item );

			return item;
		}

		///
		/// Dodawanie elementu do listy.
		/// Wariant z podaniem klasy GroupComboBoxItem.
		/// ------------------------------------------------------------------------------------------------------------
		public new void Add( GroupComboBoxItem item )
		{
			if( item.parent != null )
			{
				GroupComboBoxItem last = null;
				int index = 0;

				item.first = false;
				item.last  = true;

				// brak dzieci, dodawany element jest pierwszy
				if( item.parent.child.Count == 0 )
					item.first = true;
				// zmień status ostatniego elementu
				else
				{
					last = item.parent.child[item.parent.child.Count - 1];
					last.last = false;
				}

				// sprawdź indeks elementu poprzedzającego
				if( last != null )
				{
					while( last.child.Count > 0 )
						last = last.child[last.child.Count - 1];

					index = this.FindItemIndex( last ) + 1;
				}
				else
					index = this.FindItemIndex( item.parent ) + 1;

				// dodaj element do listy
				item.parent.child.Add( item );
				base.Insert( index, item );
			}
			// brak rodzica, dodaj do listy
			else
				base.Add( item );

			// dla listy kontrolki nie przejmuj się kolejnością
			// wyświetlane i tak będą dane z tej listy...
			if( this._collection != null )
				this._collection.Add( item.text );
		}

		///
		/// Wyszukiwanie identyfikatora elementu.
		/// ------------------------------------------------------------------------------------------------------------
		public int FindItemIndex( GroupComboBoxItem item )
		{
			int index = 0;

			// szukaj identyfikatora elementu
			for( ; index < this.Count; index++ )
				if( this[index] == item )
					break;

			// brak elementu w zbiorze
			if( this.Count == index )
				return -1;

			return index;
		}

		///
		/// Kopiowanie elementów z listy.
		/// Pomija sprawdzanie pokrewieństwa, które zachodzi pomiędzy elementami.
		/// ------------------------------------------------------------------------------------------------------------
		public void CopyList( GroupComboBoxItems items )
		{
			this._collection.Clear( );
			this.Clear( );

			foreach( GroupComboBoxItem item in items )
			{
				base.Add( item );
				if( this._collection != null )
					this._collection.Add( item.text );
			}
		}

		///
		/// Dodawanie elementu do kontrolki na odpowiedniej pozycji.
		/// Automatycznie tworzy element z podanych danych.
		/// Pozycja liczona jest dla dzieci rodzica do którego dodawany jest element.
		/// ------------------------------------------------------------------------------------------------------------
		public GroupComboBoxItem Insert( int index, string text, bool header = false, GroupComboBoxItem parent = null )
		{
			GroupComboBoxItem item = new GroupComboBoxItem( );

			item.text   = text;
			item.header = header;
			item.indent = parent == null ? 0 : parent.indent + 1;
			item.parent = parent;
			item.last   = false;
			item.first  = false;
			item.child  = new List<GroupComboBoxItem>( );

			this.Insert( index, item );

			return item;
		}

		///
		/// Dodawanie elementu do kontrolki na odpowiedniej pozycji...
		/// Pozycja liczona jest dla dzieci rodzica do którego dodawany jest element.
		/// ------------------------------------------------------------------------------------------------------------
		public new void Insert( int index, GroupComboBoxItem item )
		{
			int insert_index = 0;

			// wykryj indeks 
			for( int x = 0; x < this.Count; ++x )
				if( this[x].parent == item.parent )
				{
					if( insert_index == index )
					{
						insert_index = x;
						break;
					}
					insert_index++;
				}

			// przy elementach głównych i tak nie są pokazywane ikonki drzewa, sprawdzanie nie jest więc potrzebne.
			if( item.parent != null )
				// element zostaje dodany jako pierwszy
				if( index == 0 )
				{
					if( item.parent.child.Count > 0 )
						item.parent.child[0].first = false;
					else
						item.last = true;

					item.first = true;
				}
				// element zostaje dodany jako ostatni
				else if( index >= item.parent.child.Count )
				{
					item.parent.child[item.parent.child.Count - 1].last = false;
					item.last = true;

					// znajdź ostatni element
					GroupComboBoxItem child = item.parent;
					while( child.child.Count > 0 )
						child = child.child[child.child.Count - 1];

					// wylicz jego identyfikator
					insert_index = this.FindItemIndex( child );
				}

			base.Insert( insert_index, item );

			if( this._collection != null )
				this._collection.Add( item );
		}
		
		/// 
		/// Usuwanie wszystkich elementów z kontrolki.
		/// ------------------------------------------------------------------------------------------------------------
		public new void Clear( )
		{
			base.Clear( );

			if( this._collection != null )
				this._collection.Clear( );
		}

		/// 
		/// Usuwanie elementu z listy.
		/// ------------------------------------------------------------------------------------------------------------
		public new bool Remove( GroupComboBoxItem item )
		{
			bool retval = false;

			// zmień informacje dla elementów (pierwszy, ostatni)
			if( item.parent != null )
			{
				int count = item.parent.child.Count;

				if( item.parent.child[0] == item && count > 1 )
					item.parent.child[1].first = true;

				if( item.parent.child[count - 1] == item && count > 1 )
					item.parent.child[count - 2].last = true;

				retval = item.parent.child.Remove( item );
			}

			if( !retval )
				return false;

			retval = base.Remove( item );

			if( !retval )
				return false;

			// usuń element z kolekcji
			if( this._collection != null )
				this._collection.Remove( item.text );

			return true;
		}

		/// 
		/// Usuwanie z listy elementu o podanym indeksie.
		/// ------------------------------------------------------------------------------------------------------------
		public new void RemoveAt( int index )
		{
			this.Remove( this[index] );
		}

		/// 
		/// Usuwanie z listy dzieci podanego elementu.
		/// ------------------------------------------------------------------------------------------------------------
		public int RemoveChildrens( GroupComboBoxItem item )
		{
			int counter = 0;
			int from    = this.FindItemIndex( item );

			// brak elementu w zbiorze
			if( from == -1 )
				return 0;

			// wyszukaj i usuń dzieci z listy
			for( int x = from + 1; x < this.Count; )
			{
				if( !this.HasParent(this[x], item) )
					break;

				if( base.Remove(this[x]) )
				{
					if( this._collection != null )
						this._collection.RemoveAt( this._collection.Count - 1 );
					counter++;
				}
			}
			item.child.Clear( );

			// zwróć ilość usuniętych elementów
			return counter;
		}

		/// 
		/// Funkcja sprawdza czy element posiada jest w jakiś sposób spokrewiony z drugim elementem.
		/// ------------------------------------------------------------------------------------------------------------
		public bool HasParent( GroupComboBoxItem element, GroupComboBoxItem parent )
		{
			GroupComboBoxItem test = element;

			while( test.parent != null )
			{
				if( test.parent == parent )
					return true;
				test = test.parent;
			}

			return false;
		}
		
		///
		/// 
		/// Brakujące funkcje...
		/// 
		/// ------------------------------------------------------------------------------------------------------------
		public new void InsertRange( int index, IEnumerable<GroupComboBoxItem> items )
		{ throw new NotImplementedException( ); }
		public new void AddRange( IEnumerable<GroupComboBoxItem> items )
		{ throw new NotImplementedException( ); }
		public new int RemoveAll( Predicate<GroupComboBoxItem> match )
		{ throw new NotImplementedException( ); }
		public new void RemoveRange( int index, int count )
		{ throw new NotImplementedException( ); }
	}


	/// 
	/// Klasa synchronizacji kontrolek list rozwijanych.
	/// Zamienia jawne listy (zastąpione) na listę synchronizowaną.
	/// Pozwala to na szybsze działanie programu.
	///
	public class GroupComboBoxSync : GroupComboBoxItems
	{
		/// Lista synchronizowanych kontrolek list rozwijanych.
		protected List<GroupComboBox> _combos = null;

		/// Lista synchronizowanych kolekcji podanych wyżej kontrolek.
		protected List<ComboBox.ObjectCollection> _collections = null;

		/// 
		/// Konstruktor klasy.
		/// Tworzy obiekty list.
		/// ------------------------------------------------------------------------------------------------------------
		public GroupComboBoxSync( ) : base( null )
		{
			this._combos = new List<GroupComboBox>( );
			this._collections = new List<ComboBox.ObjectCollection>( );
		}

		/// 
		/// Dodaje kontrolkę do synchronizacji.
		/// ------------------------------------------------------------------------------------------------------------
		public void AddCombo( GroupComboBox combo )
		{
			this._combos.Add( combo );
			this._collections.Add( combo.SetSyncItems(this) );
		}

		///
		/// Zastępuje i robi to samo co funkcja Add klasy pochodnej.
		/// ------------------------------------------------------------------------------------------------------------
		public new GroupComboBoxItem Add( string text, bool header = false, GroupComboBoxItem parent = null )
		{
			GroupComboBoxItem item = new GroupComboBoxItem( );

			item.text   = text;
			item.header = header;
			item.indent = parent == null ? 0 : parent.indent + 1;
			item.parent = parent;
			item.last   = false;
			item.first  = false;
			item.child  = new List<GroupComboBoxItem>( );

			this.Add( item );

			return item;
		}

		///
		/// Dodawanie elementu do listy.
		/// Wywołuje funkcję bazową i dodaje element do kontrolki.
		/// Element dodawany jest bezpośrednio do kolekcji, gdzie kolejność nie jest ważna.
		/// ------------------------------------------------------------------------------------------------------------
		public new void Add( GroupComboBoxItem item )
		{
			base.Add( item );

			foreach( ComboBox.ObjectCollection collection in this._collections )
				collection.Add( item.text );
		}

		///
		/// Dodawanie elementu do kontrolki na odpowiedniej pozycji.
		/// Automatycznie tworzy element z podanych danych.
		/// Pozycja liczona jest dla dzieci rodzica do którego dodawany jest element.
		/// ------------------------------------------------------------------------------------------------------------
		public new GroupComboBoxItem
			Insert( int index, string text, bool header = false, GroupComboBoxItem parent = null )
		{
			GroupComboBoxItem item = new GroupComboBoxItem( );

			item.text   = text;
			item.header = header;
			item.indent = parent == null ? 0 : parent.indent + 1;
			item.parent = parent;
			item.last   = false;
			item.first  = false;
			item.child  = new List<GroupComboBoxItem>( );

			this.Insert( index, item );

			return item;
		}

		///
		/// Dodawanie elementu do kontrolki na odpowiedniej pozycji...
		/// Pozycja liczona jest dla dzieci rodzica do którego dodawany jest element.
		/// Wywołuje funkcję bazową i dodaje element do kontrolki.
		/// Element dodawany jest bezpośrednio do kolekcji, gdzie kolejność nie jest ważna.
		/// ------------------------------------------------------------------------------------------------------------
		public new void Insert( int index, GroupComboBoxItem item )
		{
			base.Insert( index, item );

			// nie przejmuj się kolejnością elementów w kontrolkach...
			foreach( ComboBox.ObjectCollection collection in this._collections )
				collection.Add( item.text );
		}

		/// 
		/// Usuwanie elementu z listy.
		/// ------------------------------------------------------------------------------------------------------------
		public new bool Remove( GroupComboBoxItem item )
		{
			bool retval = base.Remove( item );

			foreach( ComboBox.ObjectCollection collection in this._collections )
				collection.RemoveAt( collection.Count - 1 );

			return retval;
		}

		/// 
		/// Usuwanie elementu z listy o podanym indeksie.
		/// ------------------------------------------------------------------------------------------------------------
		public new void RemoveAt( int index )
		{
			base.RemoveAt( index );

			foreach( ComboBox.ObjectCollection collection in this._collections )
				collection.RemoveAt( collection.Count - 1 );
		}

		/// 
		/// Usuwanie dzieci w podanym elemencie.
		/// ------------------------------------------------------------------------------------------------------------
		public new void RemoveChildrens( GroupComboBoxItem item )
		{
			int count = base.RemoveChildrens( item );

			foreach( ComboBox.ObjectCollection collection in this._collections )
				for( int x = collection.Count - 1, y = collection.Count - count; x >= y; --x )
					collection.RemoveAt( x );
		}

		/// 
		/// Oblicza szerokość listy rozwijanej, która pomieści wszystkie elementy.
		/// Jako przykład funkcja pobiera czcionkę z pierwszej dodanej kontrolki.
		/// Jeżeli w innych kontrolkach są inne czcionki, wynik obliczania szerokości nie będzie poprawny.
		/// W takim wypadku należy oddzielnie dla każdej kontrolki wykonać obliczenia.
		/// ------------------------------------------------------------------------------------------------------------
		public void CalculateDropDownWidth( )
		{
			if( this._combos.Count == 0 )
				return;

			int max_size = 0;

			foreach( GroupComboBoxItem item in this )
			{
				// pobierz rozmiar tekstu
				Size size  = TextRenderer.MeasureText( item.text, this._combos[0].Font );
				int  width = size.Width + item.indent * this._combos[0].ItemPaddingFactor;

				if( max_size < width )
					max_size = width;
			}

			// dodaj do szerokości rozmiar panelu do przesuwania kontenera gdy jest za dużo elementów
			if( this.Count * this._combos[0].ItemHeight > this._combos[0].DropDownHeight )
				max_size += SystemInformation.VerticalScrollBarWidth;

			foreach( GroupComboBox combo in this._combos )
				combo.DropDownWidth = max_size;
		}
	}
}


///
///
/// STARE KLASY
/// Używanie kontrolki ComboBoxEx32 dla uzyskania zwykłego wyglądu dla DropDownList.
/// Prace nad tym zostały anulowane...
/// Może się jeszcze przydadzą.
///
///


/*
public class GroupComboBox : ComboBox
{
	/// Przłącznik wykonania funkcji inizjalizacyjnej.
	private static bool _initialized = false;

	/// Domyślna wysokość elementu listy.
	private static int _dn_item_height = 15;

	/// Domyślna wysokość elementu na kontrolce
	private static int _dc_item_height = 15;

	/// Przełącznik efektów wizualnych kontrolki...
	private bool _visual_styles = false;

	/// Kolekcja elementów kontrolki.
	private GroupComboBoxItems _collection = null;

	/// Wysokość elementu listy.
	private int _item_height = 0;

	// Mnożnik wcięcia wewnętrznego elementu podrzędnego.
	private int _padding_factor = 16;

	/// Ostatnio zaznaczony element (kliknięcie myszką).
	private int _prev_selected_index = -1;

	/// 
	/// Pobranie elementów kontrolki...
	/// ------------------------------------------------------------------------------------------------------------
	public new GroupComboBoxItems Items
	{
		get { return this._collection; }
	}

	/// 
	/// Sprawdza czy kontrolka może użyć klasy ComboBoxEx32.
	/// ------------------------------------------------------------------------------------------------------------
	public bool VisualStylesEnabled
	{
		get { return this._visual_styles; }
	}

	/// 
	/// Typ rysowania kontrolki.
	/// W trakcie tworzenia ustawiany jest na CBS_OWNERDRAWFIXED (DrawMode.OwnerDrawFixed).
	/// ------------------------------------------------------------------------------------------------------------
	public new DrawMode DrawMode
	{
		get { return base.DrawMode; }
		private set { base.DrawMode = value; }
	}

	/// 
	/// Styl listy rozwijanej.
	/// Ustawiany tylko w liście (private).
	/// ------------------------------------------------------------------------------------------------------------
	public new ComboBoxStyle DropDownStyle
	{
		get { return base.DropDownStyle; }
		private set { base.DropDownStyle = value; }
	}

	/// 
	/// Mnożnik wcięcia wewnętrznego elementu podrzędnego.
	/// Innymi słowy, mnożnik marginesu wewnętrznego elementu przydzielonego do grupy.
	/// ------------------------------------------------------------------------------------------------------------
	public int ItemPaddingFactor
	{
		get { return this._padding_factor; }
		set
		{
			this._padding_factor = value;
			this.RefreshItems();
		}
	}

	/// 
	/// Parametry kontrolki określane przy jej tworzeniu.
	/// Uwaga:
	/// Klasa ComboBoxEx32 nie działa jakimś cudem dla systemu z wyłączonymi efektami wizualnymi.
	/// ------------------------------------------------------------------------------------------------------------
	protected override CreateParams CreateParams
	{
		get
		{
			CreateParams param = base.CreateParams;

			if( this._visual_styles )
			{
				param.ClassName = "ComboBoxEx32";
				param.ExStyle  ^= WinAPIConst.WS_EX_CLIENTEDGE;
			}

			return param;
		}
	}

	/// 
	/// Zmiana wysokości elementu listy rozwijanej.
	/// ------------------------------------------------------------------------------------------------------------
	public new int ItemHeight
	{
		get { return this._item_height; }
		set
		{
			this._item_height = value;

			// ustaw wysokość elementów dla listy rozwijanej
			GroupComboBox.SendMessage
			(
				this.Handle,
				WinAPIConst.CB_SETITEMHEIGHT,
				IntPtr.Zero,
				(IntPtr)this._item_height
			);

			this.DropDownHeight = this._item_height * 15;
		}
	}

	/// 
	/// Zmiana wielkości listy rozwijanej.
	/// Dodanie różnicy wielkości elementu i domyślnej wielkości elementu.
	/// Pozwala (przynajmniej teoretycznie) to na pozbycie się wnerwiającego pustego miejsca na końcu listy.
	/// ------------------------------------------------------------------------------------------------------------
	public new int DropDownHeight
	{
		get { return base.DropDownHeight; }
		set { base.DropDownHeight = value + (this.ItemHeight - GroupComboBox._dn_item_height); }
	}

	/// 
	/// Funkcja do wysyłania wiadomości do wybranej kontrolki.
	/// ------------------------------------------------------------------------------------------------------------
	[DllImport("user32.dll")]
	public static extern int SendMessage( IntPtr hWnd, int wMsg, IntPtr wParam, IntPtr lParam );

	/// 
	/// Konstruktor klasy GroupComboBox.
	/// Argument vstyle odpowiada za ustawienie domyślnej stylizacji kontrolki dla systemu.
	/// ------------------------------------------------------------------------------------------------------------
	public GroupComboBox( bool vstyle = true ) : base()
	{
		if( !GroupComboBox._initialized )
			GroupComboBox.InitializeVars();

		// blokada... jak na razie nie wyglada to za dobrze
		vstyle = false;

		if( VisualStyleInformation.IsEnabledByUser && vstyle )
			this._visual_styles = true;
		else
			this._visual_styles = false;

		this._item_height = GroupComboBox._dn_item_height;
		this._collection  = new GroupComboBoxItems( this, base.Items );

		this.DrawMode       = DrawMode.OwnerDrawFixed;
		this.DropDownHeight = this._item_height * 10;
		this.DropDownStyle  = ComboBoxStyle.DropDownList;
	}

	/// 
	/// Pobranie domyślnej wysokości elementu listy rozwijanej.
	/// Jakimś cudem domyślnie ustawiane jest 16 zaś w polu ItemHeight 15, co daje przykry rezultat...
	/// To samo tyczy się domyślnej wysokości elementu na kontrolce (wybranego elementu).
	/// ------------------------------------------------------------------------------------------------------------
	private static void InitializeVars()
	{
		ComboBox combo = new ComboBox();

		int dn_item_height = GroupComboBox.SendMessage
		(
			combo.Handle,
			WinAPIConst.CB_GETITEMHEIGHT,
			(IntPtr)0,
			IntPtr.Zero
		);
		int dc_item_height = GroupComboBox.SendMessage
		(
			combo.Handle,
			WinAPIConst.CB_GETITEMHEIGHT,
			(IntPtr)(-1),
			IntPtr.Zero
		);

		GroupComboBox._dn_item_height = dn_item_height;
		GroupComboBox._dc_item_height = dc_item_height;
		GroupComboBox._initialized    = true;
	}

	/// 
	/// Automatyczne dopasowanie szerokości listy rozwijanej do elementów.
	/// ------------------------------------------------------------------------------------------------------------
	public void CalculateDropDownWidth()
	{
		int max_size = 0;

		foreach( GroupComboBoxItem item in this.Items )
		{
			// pobierz rozmiar tekstu
			Size size  = TextRenderer.MeasureText( item.text, this.Font );
			int  width = size.Width + item.indent * this._padding_factor;

			if( max_size < width )
				max_size = width;
		}

		this.DropDownWidth = max_size;
	}

	/// 
	/// Funkcja odbierająca sygnały wchodzące do kontrolki.
	/// Wysyła wiadomość o rysowaniu elemetu (OCM_DRAWITEM).
	/// Zatrzymuje dodawanie tekstu do kontrolki (CB_ADDSTRING) przy włączonych efektach wizualnych.
	/// Kontrolka ComboBoxEx32 teoretycznie i tak nie powinna ich obsłużyć.
	/// ------------------------------------------------------------------------------------------------------------
	protected override void WndProc( ref Message msg )
	{

		// wysyłanie dodatkowej wiadomości (REFLECT MESSAGES)
		if( msg.Msg == WinAPIConst.WM_DRAWITEM )
			GroupComboBox.SendMessage( this.Handle, WinAPIConst.OCM_DRAWITEM, msg.WParam, msg.LParam );
			
		// blokowanie wysyłania wiadomości CB_ADDSTRING dla ComboBoxEx32
		else if( this._visual_styles &&  msg.Msg == WinAPIConst.CB_ADDSTRING )
			return;

		// blokowanie kliknięć w nagłówek
		else if( msg.Msg == WinAPIConst.WM_COMMAND )
		{
			int wparam = (int)msg.WParam >> 16;

			if( wparam == WinAPIConst.CBN_SELCHANGE )
			{
				// pobierz zaznaczony element
				int cursel = GroupComboBox.SendMessage
				(
					this.Handle,
					WinAPIConst.CB_GETCURSEL,
					IntPtr.Zero,
					IntPtr.Zero
				);

				// sprawdź czy element jest nagłówkiem
				if( this.Items[cursel].header )
				{
					// sprawdź czy nagłówek posiada elementy podrzędne
					while( ++cursel < this.Items.Count )
						if( !this.Items[cursel].header )
							break;

					// brak elementów w nagłówku...
					if( cursel >= this.Items.Count )
					{
						// zmień zaznaczony element na poprzedni
						GroupComboBox.SendMessage
						(
							this.Handle,
							WinAPIConst.CB_SETCURSEL,
							(IntPtr)this._prev_selected_index,
							IntPtr.Zero
						);
						return;
					}
					// zmień zaznaczony element na podrzędny
					GroupComboBox.SendMessage
					(
						this.Handle,
						WinAPIConst.CB_SETCURSEL,
						(IntPtr)cursel,
						IntPtr.Zero
					);
				}
				this._prev_selected_index = cursel;
			}
		}
		base.WndProc( ref msg );
	}

	/// 
	/// Ustawianie domyślnych wysokości elementów dla listy i kontrolki.
	/// ------------------------------------------------------------------------------------------------------------
	protected override void OnCreateControl()
	{
		base.OnCreateControl();

		// ustaw wysokość elementów dla listy rozwijanej i elementu na kontrolce
		GroupComboBox.SendMessage
		(
			this.Handle,
			WinAPIConst.CB_SETITEMHEIGHT,
			(IntPtr)0,
			(IntPtr)this._item_height
		);
		GroupComboBox.SendMessage
		(
			this.Handle,
			WinAPIConst.CB_SETITEMHEIGHT,
			(IntPtr)(-1),
			(IntPtr)GroupComboBox._dc_item_height
		);
	}

	/// 
	/// Rysowanie elementu na kontrolce lub na liście rozwijanej.
	/// I tak czcionki przesuwają się o 1 px za daleko w lewo (nie mam pojęcia dlaczego...)
	/// ------------------------------------------------------------------------------------------------------------
	protected override void OnDrawItem( DrawItemEventArgs ev )
	{
		base.OnDrawItem( ev );

		// brak zaznaczonego elementu
		if( ev.Index < 0 )
		{
			ev.DrawFocusRectangle();
			return;
		}

		GroupComboBoxItem item  = this.Items[ev.Index];
		TextFormatFlags   flags = TextFormatFlags.NoPadding | TextFormatFlags.VerticalCenter
								| TextFormatFlags.SingleLine;

		// element na kontrolce
		if( (ev.State & DrawItemState.ComboBoxEdit) == DrawItemState.ComboBoxEdit )
		{
			Rectangle rect = ev.Bounds;

			rect.X     += 1;
			rect.Width -= 1;

			TextRenderer.DrawText( ev.Graphics, item.text, this.Font, rect, this.ForeColor, flags );
			ev.DrawFocusRectangle();

			return;
		}

		// nie rysuj gdy nagłówek jest zaznaczony
		if( item.header && (ev.State & DrawItemState.Selected) == DrawItemState.Selected )
		{
			int cursel = ev.Index;

			// sprawdź czy nagłówek posiada elementy podrzędne
			while( ++cursel < this.Items.Count )
				if( !this.Items[cursel].header )
					break;

			// brak elementów w grupie i innych grupach pod nią...
			if( cursel >= this.Items.Count )
				return;

			GroupComboBoxItem newitem = this.Items[cursel];
			Rectangle         rect    = ev.Bounds;

			// rysuj zaznaczenie
			rect.Y += this.ItemHeight;
			ev.Graphics.FillRectangle( new SolidBrush(ev.BackColor), rect );
				
			// rysuj tekst
			rect.X     += newitem.indent * this._padding_factor + 2;
			rect.Width -= newitem.indent * this._padding_factor + 4;
			TextRenderer.DrawText( ev.Graphics, newitem.text, ev.Font, rect, ev.ForeColor, flags );

			return;
		}

		// VS wywala błąd o tym że zmienna "rect" jest zadeklarowana wyżej... paranoja
		// dlatego utworzony jest w tym miejscu nowy blok
		{
			Font      font = ev.Font;
			Rectangle rect = ev.Bounds;

			rect.X     += item.indent * this._padding_factor + 2;
			rect.Width -= item.indent * this._padding_factor + 4;

			if( item.header )
				font = new Font( ev.Font, FontStyle.Underline );

			// element na liście rozwijanej
			ev.DrawBackground();
			TextRenderer.DrawText( ev.Graphics, item.text, font, rect, ev.ForeColor, flags );
		}
	}
}
	
public class GroupComboBoxItems : List<GroupComboBoxItem>, IDisposable
{
	private GroupComboBox _combobox = null;
	private ComboBox.ObjectCollection _collection = null;
	private ComboBoxExItem _item;
	private IntPtr _item_ptr = IntPtr.Zero;

	public GroupComboBoxItems( GroupComboBox combo = null, ComboBox.ObjectCollection items = null ) : base()
	{
		this._combobox = combo;

		// dodawanie elementów do kontrolki ComboBoxEx wygląda nieco inaczej...
		if( combo.VisualStylesEnabled )
		{
			this._item = new ComboBoxExItem();
			this._item.mask = WinAPIConst.CBEIF_TEXT;

			this._item_ptr = Marshal.AllocHGlobal( Marshal.SizeOf(this._item) );
			Marshal.StructureToPtr( this._item, this._item_ptr, false );
		}

		// brak klasy z elementami (bez tego kontrolka będzie się sypała...)
		if( this._combobox != null && items == null )
		{
			Program.LogError( "Nie podano elementów nadrzędnych dla listy rozwijanej.", "Błąd wewnętrzny", false );
			this._combobox = null;
		}
		
		this._collection = items;
	}

	public void Add( string text, bool header = false, GroupComboBoxItem parent = null )
	{
		GroupComboBoxItem item = new GroupComboBoxItem();

		item.text   = text;
		item.header = header;
		item.indent = parent == null ? 0 : parent.indent + 1;
		item.parent = parent;

		this.Add( item );
	}

	public new void Add( GroupComboBoxItem item )
	{
		base.Add( item );

		if( this._collection != null )
			this._collection.Add( item.text );
			
		if( this._combobox != null && this._combobox.VisualStylesEnabled )
			this.AddComboBoxItem( this.Count - 1 );
	}

	public new void AddRange( IEnumerable<GroupComboBoxItem> items )
	{
		throw new NotImplementedException();
	}

	private void AddComboBoxItem( int index )
	{
		GroupComboBoxItem item = this[index];

		this._item.iItem      = (IntPtr)index;
		this._item.pszText    = item.text;
		this._item.cchTextMax = item.text.Length;

		GroupComboBox.SendMessage
		(
			this._combobox.Handle,
			WinAPIConst.CBEM_INSERTITEM,
			IntPtr.Zero,
			this._item_ptr
		);
	}

	public void Dispose()
	{
		if( this._item_ptr != IntPtr.Zero )
			Marshal.FreeHGlobal( this._item_ptr );
	}
}
*/