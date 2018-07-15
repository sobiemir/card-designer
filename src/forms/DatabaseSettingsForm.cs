using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

///
/// $i01 DatabaseSettingsForm.cs
/// 
/// Okno ustawień pliku bazy danych (kodowanie, separator).
/// Wyświetlane jest zaraz po wybraniu pliku bazy danych.
/// 
/// Autor: Kamil Biały
/// Od wersji: 0.8.x.x
/// Ostatnia zmiana: 2015-07-15
///

namespace CDesigner
{
	public partial class DatabaseSettingsForm : Form
	{
		/// Blokada kontrolek (lub innych elementów) przed odświeżeniem.
		private bool _locked;

		/// "Czytnik" bazy danych.
		private DatabaseReader _reader;

		/// Aktualnie wyświetlana kolumna (zapobiega podwójnemu zaznaczeniu).
		private int _column;

		/// 
		/// Konstruktor klasy DatabaseFileSettings.
		/// ------------------------------------------------------------------------------------------------------------
		public DatabaseSettingsForm( DatabaseReader reader )
		{
			this.InitializeComponent();
			this.Icon = Program.GetIcon();

			this._reader = reader;
			this._column = -1;

			// uzupełnij kontrolki danymi
			this.FillControls();
		}

		/// 
		/// Uzupełnianie / odświeżanie kontrolek.
		/// ------------------------------------------------------------------------------------------------------------
		private void FillControls( )
		{
			this._locked = true;

			// wyświetl informacje o kodowaniu
			// nie można na CASE bo Encoding jest klasą...
			if( this._reader.Encoding == Encoding.Default )
				this.cbEncoding.SelectedIndex = 0;
			else if( this._reader.Encoding == Encoding.ASCII )
				this.cbEncoding.SelectedIndex = 1;
			else if( this._reader.Encoding == Encoding.UTF8 )
				this.cbEncoding.SelectedIndex = 2;
			else if( this._reader.Encoding == Encoding.BigEndianUnicode )
				this.cbEncoding.SelectedIndex = 3;
			else if( this._reader.Encoding == Encoding.Unicode )
				this.cbEncoding.SelectedIndex = 4;
			else if( this._reader.Encoding == Encoding.UTF32 )
				this.cbEncoding.SelectedIndex = 5;
			else
				this.cbEncoding.SelectedIndex = 6;

			// separator
			this.tbSeparator.Text = this._reader.Separator.ToString();

			// kolumny
			this.lvColumns.Items.Clear();
			foreach( string column in this._reader.Columns )
				this.lvColumns.Items.Add( column );

			// zaznacz pierwszy element
			if( this._reader.ColumnsNumber > 0 )
				this.lvColumns.Items[0].Selected = true;

			this._locked = false;
		}

		/// 
		/// Zmiana wyświetlanej kolumny (zmiana wyświetlanych wierszy dla kolumny).
		/// ------------------------------------------------------------------------------------------------------------
		private void lvColumns_SelectedIndexChanged( object sender, EventArgs ev )
		{
			if( this._locked )
				return;

			if( this.lvColumns.SelectedItems.Count == 0 )
				return;

			if( this._column == this.lvColumns.SelectedItems[0].Index )
				return;

			Program.LogMessage( "Zmiana wyświetlanej kolumny." );

			// zmień nazwę kolumny
			this.lvcRows.Text = "Podgląd wierszy [ " + this.lvColumns.SelectedItems[0].Text + " ]";
			this.lvRows.Items.Clear();

			// uzupełnij kontrolkę
			for( int x = 0, y = this._reader.RowsNumber > 17 ? 17 : this._reader.RowsNumber; x < y; ++x )
				this.lvRows.Items.Add( this._reader.Rows[this.lvColumns.SelectedItems[0].Index, x] );

			this._column = this.lvColumns.SelectedItems[0].Index;
		}

		/// 
		/// Zmiana pliku z bazą danych.
		/// ------------------------------------------------------------------------------------------------------------
		private void bChangeFile_Click( object sender, EventArgs ev )
		{
			Program.LogMessage( "Otwieranie okna wyboru nowego pliku bazy danych." );

			OpenFileDialog dialog = new OpenFileDialog();

			dialog.Title  = "Wybór pliku bazy danych";
			dialog.Filter = DatabaseReader.JoinSupportedExtensions( true );
			DialogResult result = dialog.ShowDialog();

			if( result != DialogResult.OK )
				return;

			// zmień plik
			this._reader.ChangeDatabase( dialog.FileName );
			this._reader.Parse();

			this.FillControls();
		}

		/// 
		/// Zmiana kodowania pliku bazy danych.
		/// ------------------------------------------------------------------------------------------------------------
		private void cbEncoding_SelectedIndexChanged( object sender, EventArgs ev )
		{
			if( this._locked )
				return;

			Program.LogMessage( "Zmiana kodowania dla pliku bazy danych." );

			// rozpoznaj odpowiednie kodowanie i otwórz ponownie plik
			switch( this.cbEncoding.SelectedIndex )
			{
				case 1 : this._reader.Encoding = Encoding.ASCII; break;
				case 2 : this._reader.Encoding = Encoding.UTF8; break;
				case 3 : this._reader.Encoding = Encoding.BigEndianUnicode; break;
				case 4 : this._reader.Encoding = Encoding.Unicode; break;
				case 5 : this._reader.Encoding = Encoding.UTF32; break;
				case 6 : this._reader.Encoding = Encoding.UTF7; break;
				default: this._reader.Encoding = Encoding.Default; break;
			}
			this._reader.Parse();

			this.FillControls();
		}

		/// 
		/// Zmiana separatora dla pliku bazy danych.
		/// ------------------------------------------------------------------------------------------------------------
		private void tbSeparator_TextChanged( object sender, EventArgs ev )
		{
			if( this._locked )
				return;

			// brak znaku, pomiń
			if( this.tbSeparator.Text == "" )
				return;

			Program.LogMessage( "Zmiana separatora dla pliku bazy danych." );

			// pobierz znak dla separatora i ponownie otwórz plik
			this._reader.Separator = this.tbSeparator.Text[0];
			this._reader.Parse();

			this.FillControls();
		}

		/// 
		/// Zapisanie ustawień pliku.
		/// ------------------------------------------------------------------------------------------------------------
		private void bSave_Click( object sender, EventArgs ev )
		{
			Program.LogMessage( "Zamykanie okna ustawień pliku bazy danych." );

			this.DialogResult = DialogResult.OK;
			this.Close();
		}

		/// 
		/// Rysowanie górnej ramki dla dolnego paska
		/// ------------------------------------------------------------------------------------------------------------
		private void tlStatusBar_Paint( object sender, PaintEventArgs ev )
		{
			ev.Graphics.DrawLine
			(
				new Pen( SystemColors.ControlDark ),
				this.tlStatusBar.Bounds.X,
				0,
				this.tlStatusBar.Bounds.Right,
				0
			);
		}
	}
}
