using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;

/*
 * TODO:
 * 1. Po kliknięciu zaznaczonego elementu uaktywnia funkcje AfterSelect
 * 2. Po wciśnięciu TAB przemieszcza pomiędzy listami
 * 3. Po wciśnięciu ENTER zamyka menu
 * 4. Dodać linie do menu (przy sub-elementach) (|__)
 * 5. Po zaznaczeniu (TAB) lub przez klawiaturę (STRZAŁKI) po kliknięciu myszką w element nic się nie dzieje (a menu musi się schować)
 *    Sytuacja ma się również do przełączania (TAB). Po przełączeniu więcej niż 2x element się nie zmienia tak naprawdę...
 *    Tak naprawdę to część odnosi się do puntku nr.1
 * 6. Dla tej samej kolumny można teraz utworzyć więcej formatów niż 1. Zablokować to!
 */

namespace CDesigner
{
	///
	/// Rozszerzenie kontrolki ComboBox (listy rozwijanej).
	/// Usuwa domyślną listę i dodaje dwie oddzielne listy (kontrolki TreeView).
	/// Aby utworzyć kontrolkę należy podać menu - listę rozwijaną.
	/// Tworzy się ją poprzez wywołanie funkcji TreeComboBox.InitMenuStrip( ..., ... )
	///
	public class TreeComboBox : ComboBox
	{
		/// Zdarzenie wywoływane przy zmianie elementu...
		public event TreeViewEventHandler OnItemChanged = null;

		/// Panel wyboru elementu (menu pokazujące się po kliknięciu w kontrolkę).
		private CustomContextMenuStrip _dropdown = null;

		/// Zaznaczony element...
		private TreeNode _selected = null;

		/// Aktualna instancja objektu.
		private static object _instance = null;

		/// 
		/// Konstruktor klasy TreeComboBox - kontrolki z rozwijaną listą podwójną.
		/// Przed użyciem konstruktora należy wywołać funkcję TreeComboBox.InitTreeMenuStrip(...).
		/// ------------------------------------------------------------------------------------------------------------
		public TreeComboBox( CustomContextMenuStrip cms ) : base()
		{
			this._dropdown = cms;
			this.ContextMenuStrip = this._dropdown;
		}

		/// 
		/// Zaznaczony element w rozwijanej liście
		/// ------------------------------------------------------------------------------------------------------------
		public TreeNode SelectedNode
		{
			get { return this._selected; }
			set { this._selected = value; }
		}

		/// 
		/// Przypisane w konstruktorze menu (lista rozwijana).
		/// ------------------------------------------------------------------------------------------------------------
		public CustomContextMenuStrip TreeDropDown
		{
			get { return this._dropdown; }
		}

		/// 
		/// Inicjalizacja listy rozwijanej (menu).
		/// Przyjmuje dwa parametry - referencje do kontrolek TreeView dla dwóch list.
		/// Zdarzenia przypisywane są automatycznie, wystarczy podpiąć pod kontrolkę TreeComboBox w konstruktorze.
		/// ------------------------------------------------------------------------------------------------------------
		public static CustomContextMenuStrip InitTreeMenuStrip( ref TreeView col1, ref TreeView col2 )
		{
			CustomContextMenuStrip menu = new CustomContextMenuStrip();
			Panel panel = new Panel();
			TableLayoutPanel table = new TableLayoutPanel();
			ToolStripControlHost item = new ToolStripControlHost( panel );

			col1 = new TreeView();
			col2 = new TreeView();

			// menu
			menu.ShowCheckMargin = false;
			menu.ShowImageMargin = false;
			menu.AutoSize = false;
			menu.Padding = new Padding( 1 );
			menu.Margin = new Padding( 0 );
			menu.Width = 402;
			menu.Height = 202;
			
			// panel
			panel.Padding = new Padding( 0 );
			panel.Margin = new Padding( 0 );
			panel.Height = 200;
			panel.Width  = 400;

			// tabela
			table = new TableLayoutPanel();
			table.Padding = new Padding( 0 );
			table.Margin = new Padding( 0 );
			table.ColumnCount = 2;
			table.RowCount = 1;
			table.Dock = DockStyle.Fill;

			// listy elementów (kolumn)
			col1.Dock = DockStyle.Fill;
			col2.Dock = DockStyle.Fill;
			col1.Margin = new Padding( 3, 3, 2, 3 );
			col2.Margin = new Padding( 1, 3, 3, 3 );
			col1.ShowRootLines = false;
			col2.ShowRootLines = false;
			col1.ShowPlusMinus = false;
			col2.ShowPlusMinus = false;
			col1.ShowLines = false;
			col2.ShowLines = false;
			col1.HideSelection = false;
			col2.HideSelection = false;
			col1.FullRowSelect = true;
			col2.FullRowSelect = true;

			// element menu
			item.Padding = new Padding( 0 );
			item.Margin = new Padding( 0 );
			item.AutoSize = false;

			// style kolumn tabeli
			table.ColumnStyles.Add( new ColumnStyle(SizeType.Percent, 50) );
			table.ColumnStyles.Add( new ColumnStyle(SizeType.Percent, 50) );

			// dodaj elementy do panelu
			table.Controls.Add( col1, 0, 0 );
			table.Controls.Add( col2, 1, 0 );
			panel.Controls.Add( table );
			menu.Items.Add( item );

			// zdarzenia
			menu.Opening += new CancelEventHandler( TreeComboBox.TreeComboBox_Opening );
			menu.Opened += new EventHandler( TreeComboBox.TreeComboBox_Opened );
			col1.KeyUp += new KeyEventHandler( TreeComboBox.TreeComboBox_KeyUp );
			col2.KeyUp += new KeyEventHandler( TreeComboBox.TreeComboBox_KeyUp );
			col1.AfterSelect += new TreeViewEventHandler( TreeComboBox.TreeComboBox_AfterSelect );
			col2.AfterSelect += new TreeViewEventHandler( TreeComboBox.TreeComboBox_AfterSelect );

			// dodatkowe dane
			col1.Tag = col1;
			col2.Tag = col1;
			menu.Tag = col1;

			return menu;
		}

		/// 
		/// Zdarzenie wywoływane po zwolnieniu przycisku.
		/// Główne zadania:
		/// - Zmiana aktywnej kontrolki w menu klawiszem TAB.
		/// - Zatwierzenie elementu klawiszem ENTER i ESC.
		/// ------------------------------------------------------------------------------------------------------------
		static void TreeComboBox_KeyUp( object sender, KeyEventArgs ev )
		{
			// ukryj menu po naciśnięciu Enter lub ESC
			if( ev.KeyCode == Keys.Enter || ev.KeyCode == Keys.Escape )
			{
				((CustomContextMenuStrip)TreeComboBox._instance).Hide();
				return;
			}

			if( ev.KeyCode != Keys.Tab )
				return;

			// pobierz drugą listę
			TreeView colx = (TreeView)sender,
					 coly = (TreeView)(colx.Parent.Controls[0] == colx
							? colx.Parent.Controls[1]
							: colx.Parent.Controls[0]);

			// ustaw drugą kontrolkę jako aktywną...
			coly.Focus();
		}

		/// 
		/// Zdarzenie wywoływane przed rozwinięciem menu.
		/// Funkcja odpowiada za zaznaczenie wybranego wcześniej elementu na kontrolkach.
		/// ------------------------------------------------------------------------------------------------------------
		private static void TreeComboBox_Opening( object sender, CancelEventArgs ev )
		{
			// nie otwieraj przy naciśnięciu prawego przycisku myszy
			if( TreeComboBox.MouseButtons != MouseButtons.Left )
				ev.Cancel = true;
			else
				TreeComboBox._instance = sender;

			// pobierz kontrolkę TreeComboBox
			TreeComboBox cbox = (TreeComboBox)((CustomContextMenuStrip)sender).SourceControl;
			
			if( cbox == null )
				return;

			// pobierz zaznaczony element
			TreeNode node = cbox.SelectedNode;

			// jeżeli brak, wyzeruj listy (wyczyść zaznaczenia)
			if( node == null )
			{
				CustomContextMenuStrip dropdown = cbox.TreeDropDown;
				TreeView tview = (TreeView)dropdown.Tag;

				tview.SelectedNode = null;
				((TreeView)tview.Parent.Controls[1]).SelectedNode = null;

				return;
			}

			// pobierz drugą listę
			TreeView colx = node.TreeView,
					 coly = (TreeView)(colx.Parent.Controls[0] == colx
							? colx.Parent.Controls[1]
							: colx.Parent.Controls[0]);

			// zaznacz element
			colx.SelectedNode = node;
			coly.SelectedNode = null;
		}

		/// 
		/// Zdarzenie wywoływane po rozwinięciu menu.
		/// Funkcja odpowiada za akywacje kontrolki w której zaznaczono element.
		/// Gdy brak zaznaczonego elementu, aktywuje pierwszą kontrolkę.
		/// ------------------------------------------------------------------------------------------------------------
		static void TreeComboBox_Opened( object sender, EventArgs ev )
		{
			// pobierz kontrolkę TreeComboBox
			TreeComboBox cbox = (TreeComboBox)((CustomContextMenuStrip)sender).SourceControl;

			if( cbox == null )
				return;

			// pobierz zaznaczony element
			TreeNode node = cbox.SelectedNode;

			// brak zaznaczonego elementi
			if( node == null )
			{
				// oznacz pierwszą kontrolkę (zaznacza również pierwszy element)
				CustomContextMenuStrip menu = (CustomContextMenuStrip)sender;
				TreeView view = (TreeView)menu.Tag;

				view.Focus();
				return;
			}

			// oznacz kontrolkę (ustaw kontrolkę jako aktywną)
			node.TreeView.Focus();
		}

		/// 
		/// Zdarzenie wywoływane po wybraniu elementu.
		/// Wyświetla w kontrolce ComboBox wybrany element i uruchamia zdarzenie OnItemChanged.
		/// ------------------------------------------------------------------------------------------------------------
		static void TreeComboBox_AfterSelect( object sender, TreeViewEventArgs ev )
		{
			TreeView view = (TreeView)sender;
			
			if( view == null )
				return;

			// pobierz kontrolkę TreeComboBox
			object obj = view.Tag;
			CustomContextMenuStrip menu = (CustomContextMenuStrip)TreeComboBox._instance;
			TreeComboBox cbox = (TreeComboBox)menu.SourceControl;

			if( ev.Node == null )
				return;

			cbox.Items.Clear();

			// sprawdź z której listy zostało wywołane zdarzenie
			if( sender == obj )
			{
				string text = "#1: " + ev.Node.Text;
				cbox.Items.Add( text );
			}
			else
			{
				string text = "#2";

				// dodaj indeks elementu potomnego jeżeli istnieje
				if( ev.Node.Parent != null )
					text += "," + (ev.Node.Parent.Index + 1) + ": " + ev.Node.Text;
				else
					text += ": " + ev.Node.Text;

				cbox.Items.Add( text );
			}
			
			cbox.SelectedIndex = 0;

			// wywołaj własne zdarzenie...
			if( cbox.OnItemChanged != null )
			{
				TreeViewEventArgs args = new TreeViewEventArgs( ev.Node );
				cbox.OnItemChanged( cbox, args );
			}

			// zapisz zaznaczony element
			cbox.SelectedNode = ev.Node;

			// unikaj zamykania po zmianie elementu z klawiatury
			if( ev.Action == TreeViewAction.ByKeyboard || ev.Action == TreeViewAction.Unknown )
				return;

			// ukryj menu
			menu.Hide();
		}

		/// 
		/// Przechwytywanie sygnałów wejścia.
		/// Blokowanie klawiszy kierunkowych na kontrolce.
		/// ------------------------------------------------------------------------------------------------------------
		public override bool PreProcessMessage(ref Message msg)
		{
			// WM_KEYDOWN
			if( msg.Msg == 0x100 )
			{
				Keys code = (Keys)msg.WParam & Keys.KeyCode;

				if( code == Keys.Down || code == Keys.Up || code == Keys.Left || code == Keys.Right )
					return true;
			}

			return base.PreProcessMessage( ref msg );
		}

		/// 
		/// Przechwytywanie zdarzeń systemowych.
		/// Blokowanie wyświetlania domyślnej rozwijanej listy.
		/// ------------------------------------------------------------------------------------------------------------
		protected override void WndProc( ref Message msg )
		{
			// WM_LBUTTONDOWN || WM_LBUTTONDBLCLK
			if( msg.Msg == 0x201 || msg.Msg == 0x203 )
			{
				this._dropdown.Show( this, 0, this.Height );
				return;
			}
			
			base.WndProc( ref msg );
		}
	}
}
