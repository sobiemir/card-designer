using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

///
/// $i02 DatabaseSettingsForm.cs
/// 
/// Okno ustawień pliku bazy danych (kodowanie, separator).
/// Wyświetlane jest zaraz po wybraniu pliku bazy danych.
/// 
/// Autor: Kamil Biały
/// Od wersji: 0.8.x.x
/// Ostatnia zmiana: 2016-01-31
/// 
/// Dodano ograniczenie wyświetlania wierszy.
///

namespace CDesigner
{
	/// 
	/// <summary>
	/// Formularz ustawień pliku z bazą danych.
	/// Pozwala na zmianę kodowania i separatora.
	/// Dodatkowo wyświetla podgląd kolumn i wierszy.
	/// </summary>
	/// 
	public partial class DatafileSettingsForm : Form
	{
		// ===== PRIVATE VARIABLES =====

		/// <summary>Blokada kontrolek (lub innych elementów) przed odświeżeniem.</summary>
		private bool _locked;

		/// <summary>"Czytnik" bazy danych.</summary>
		private DatabaseReader _reader;

		/// <summary>Aktualnie wyświetlana kolumna (zapobiega podwójnemu zaznaczeniu).</summary>
		private int _column;

		// ===== PUBLIC FUNCTIONS =====

		/**
		 * <summary>
		 * Konstruktor klasy DatabaseSettingsForm.
		 * </summary>
		 * 
		 * <param name="reader">Czytnik pliku bazy danych.</param>
		 * --------------------------------------------------------------------------------------------------------- **/
		public DatafileSettingsForm( DatabaseReader reader )
		{
			this.InitializeComponent();

			// ikona programu
			this.Icon = Program.GetIcon();

			this._reader = reader;
			this._column = -1;

			List<string> values = null;
			
			// translacja listy z kodowaniem
			values = Language.GetLines( "DatabaseSettings", "ComboBox" );
			for( int x = 0; x < this.cbEncoding.Items.Count; ++x )
				this.cbEncoding.Items[x] = (object)values[x];

			// translacja nagłówków tabel
			values = Language.GetLines( "DatabaseSettings", "Headers" );
			this.lvcRows.Text    = values[0];
			this.lvcColumns.Text = values[1];

			// translacja przycisków
			values = Language.GetLines( "DatabaseSettings", "Buttons" );
			this.bChangeFile.Text = values[0];
			this.bSave.Text       = values[1];

			// uzupełnij kontrolki danymi
			this.FillControls();

			// nazwa formularza
			this.Text = Language.GetLine( "FormNames", (int)FORMLIDX.DatabaseSettings );
		}

		// ===== PRIVATE FUNCTIONS =====

		/**
		 * <summary>
		 * Uzupełnianie / odświeżanie kontrolek.
		 * </summary>
		 * --------------------------------------------------------------------------------------------------------- **/
		private void FillControls()
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

		/**
		 * <summary>
		 * Zmiana wyświetlanej kolumny (zmiana wyświetlanych wierszy dla kolumny).
		 * </summary>
		 * 
		 * <param name="sender">Obiekt wywołujący zdarzenie.</param>
		 * <param name="ev">Argumenty zdarzenia.</param>
		 * --------------------------------------------------------------------------------------------------------- **/
		private void lvColumns_SelectedIndexChanged( object sender, EventArgs ev )
		{
			if( this._locked )
				return;

			if( this.lvColumns.SelectedItems.Count == 0 )
				return;

			if( this._column == this.lvColumns.SelectedItems[0].Index )
				return;

#		if DEBUG
			Program.LogMessage( "Zmiana wyświetlanej kolumny." );
#		endif

			// zmień nazwę kolumny
			this.lvcRows.Text = Language.GetLine( "DatabaseSettings", "Headers", 0 ) +
				" [" + this.lvColumns.SelectedItems[0].Text + " ]";
			this.lvRows.Items.Clear();

			// pobierz zapisaną ilość wierszy
			int rowsnum = Settings.Info.DSF_RowsNumber;

			// uzupełnij kontrolkę
			for( int x = 0, y = this._reader.RowsNumber > rowsnum ? rowsnum : this._reader.RowsNumber; x < y; ++x )
				this.lvRows.Items.Add( this._reader.Rows[this.lvColumns.SelectedItems[0].Index, x] );

			this._column = this.lvColumns.SelectedItems[0].Index;
		}

		/**
		 * <summary>
		 * Zmiana pliku z bazą danych.
		 * </summary>
		 * 
		 * <param name="sender">Obiekt wywołujący zdarzenie.</param>
		 * <param name="ev">Argumenty zdarzenia.</param>
		 * --------------------------------------------------------------------------------------------------------- **/
		private void bChangeFile_Click( object sender, EventArgs ev )
		{
#		if DEBUG
			Program.LogMessage( "Otwieranie okna wyboru nowego pliku bazy danych." );
#		endif

			OpenFileDialog dialog = new OpenFileDialog();

			dialog.Title  = Language.GetLine( "MessageNames", (int)MSGBLIDX.DatabaseSelect );
			dialog.Filter = DatabaseReader.JoinSupportedExtensions( true );
			DialogResult result = dialog.ShowDialog();

			if( result != DialogResult.OK )
				return;

			// zmień plik
			this._reader.ChangeDatabase( dialog.FileName );

			// parsuj ponownie
			this._reader.Parse();

			// odśwież dane
			this._column = -1;
			this.lvColumns_SelectedIndexChanged( null, null );

			this.FillControls();

#		if DEBUG
			Program.LogMessage( "Zmieniono plik bazy danych." );
#		endif
		}

		/**
		 * <summary>
		 * Zmiana kodowania pliku z bazą danych.
		 * </summary>
		 * 
		 * <param name="sender">Obiekt wywołujący zdarzenie.</param>
		 * <param name="ev">Argumenty zdarzenia.</param>
		 * --------------------------------------------------------------------------------------------------------- **/
		private void cbEncoding_SelectedIndexChanged( object sender, EventArgs ev )
		{
			if( this._locked )
				return;

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

			// parsuj ponownie
			this._reader.Parse();

			// ustaw pierwszą kolumnę
			if( this._column != 0 )
			{
				this.lvColumns.SelectedIndices.Clear();
				this.lvColumns.SelectedIndices.Add(0);
			}

			// odśwież dane
			this._column = -1;
			this.lvColumns_SelectedIndexChanged( null, null );

			this.FillControls();

#		if DEBUG
			Program.LogMessage( "Znieniono kodowanie pliku bazy danych." );
#		endif
		}

		/**
		 * <summary>
		 * Zmiana separatora dla pliku z bazą danych.
		 * </summary>
		 * 
		 * <param name="sender">Obiekt wywołujący zdarzenie.</param>
		 * <param name="ev">Argumenty zdarzenia.</param>
		 * --------------------------------------------------------------------------------------------------------- **/
		private void tbSeparator_TextChanged( object sender, EventArgs ev )
		{
			if( this._locked )
				return;

			// brak znaku, pomiń
			if( this.tbSeparator.Text == "" )
				return;

			// pobierz znak dla separatora i ponownie parsuj plik
			this._reader.Separator = this.tbSeparator.Text[0];
			this._reader.Parse();

			// odśwież dane
			this._column = -1;
			this.lvColumns_SelectedIndexChanged( null, null );

			this.FillControls();

#		if DEBUG
			Program.LogMessage( "Zmieniono separator pliku bazy danych." );
#		endif
		}

		/**
		 * <summary>
		 * Zapisanie ustawień pliku.
		 * </summary>
		 * 
		 * <param name="sender">Obiekt wywołujący zdarzenie.</param>
		 * <param name="ev">Argumenty zdarzenia.</param>
		 * --------------------------------------------------------------------------------------------------------- **/
		private void bSave_Click( object sender, EventArgs ev )
		{
#		if DEBUG
			Program.LogMessage( "Zamykanie okna ustawień pliku bazy danych." );
#		endif

			this.DialogResult = DialogResult.OK;
			this.Close();
		}

		/**
		 * <summary>
		 * Rysowanie górnej ramki dla paska informacji.
		 * </summary>
		 * 
		 * <param name="sender">Obiekt wywołujący zdarzenie.</param>
		 * <param name="ev">Argumenty zdarzenia.</param>
		 * --------------------------------------------------------------------------------------------------------- **/
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
