﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace CDesigner
{
	class DatabaseReader
	{
		/// Nazwa pliku.
		private string _file;

		/// Rozszerzenie pliku.
		private string _extension;

		/// Separator kolumn (plik CSV).
		private char _separator = ';';

		/// Kodowanie pliku.
		private Encoding _encoding;

		/// Lista obsługiwanych rozszerzeń plików.
		private string[] _supports = {"csv"};

		/// Ilość pobranych kolumny.
		public int _columns = 0;

		/// Ilość pobranych wierszy.
		public int _rows = 0;

		/// Pobrane kolumny
		public string[]  _column = null;

		/// Pobrane wiersze.
		public string[,] _row = null;

		/// 
		/// Konstruktor klasy DatabaseReader.
		/// Pobiera jako argument nazwę pliku, po czym sprawdza czy plik istnieje.
		/// ------------------------------------------------------------------------------------------------------------
		public DatabaseReader( string file )
		{
			// sprawdź czy plik istnieje
			try
				{ File.Open( file, FileMode.Open, FileAccess.Read ).Close(); }
			catch( IOException ex )
				{ Program.LogError(ex.Message, "Błąd wczytywania pliku", false); }

			this._extension = new FileInfo(file).Extension.ToLower();
			this._file      = file;
			this._encoding  = Encoding.Default;
		}

		/// 
		/// Pobiera/ustawia separator dla kolumn (plik CSV).
		/// ------------------------------------------------------------------------------------------------------------
		public char Separator
		{
			get { return this._separator; }
			set { this._separator = value; }
		}

		/// 
		/// Pobiera/ustawia kodowanie pliku.
		/// Domyślnie kodowanie ustawione jest na Encoding.Default.
		/// ------------------------------------------------------------------------------------------------------------
		public Encoding Encoding
		{
			get { return this._encoding; }
			set { this._encoding = value; }
		}

		/// 
		/// Parser danych - wczytywanie danych z określonej lokalizacji.
		/// Można pobrać tylko nazwy kolumn poprzez ustawienie drugiego argumentu na TRUE.
		/// ------------------------------------------------------------------------------------------------------------
		public void Parse( bool cols_only = false )
		{
			if( this._extension == ".csv" )
				this.ParseCSV( this._encoding, cols_only );
			else
				Program.LogError( "Brak obsługi bazy danych o rozszerzeniu: '" + this._extension + "'. " +
					"Obsługiwane rozszerzenia: " + this.JoinSupportedExtensions(),
					"Nieprawidłowy format pliku", false );
		}

		/// 
		/// Pobiera ilość dostępnych kolumn.
		/// ------------------------------------------------------------------------------------------------------------
		public int ColumnsNumber
		{
			get { return this._columns; }
		}

		/// 
		/// Pobiera ilość dostępnych wierszy.
		/// ------------------------------------------------------------------------------------------------------------
		public int RowsNumber
		{
			get { return this._rows; }
		}

		/// 
		/// Pobiera wczytane kolumny.
		/// ------------------------------------------------------------------------------------------------------------
		public string[] Columns
		{
			get { return this._column; }
		}

		/// 
		/// Pobiera wczytane wiersze.
		/// ------------------------------------------------------------------------------------------------------------
		public string[,] Rows
		{
			get { return this._row; }
		}

		/// 
		/// Pobiera dostępne rozszerzenia plików.
		/// ------------------------------------------------------------------------------------------------------------
		public string[] SupportedExtensions
		{
			get { return this._supports; }
		}

		/// 
		/// Łączy po przecinku dostępne rozszerzenia.
		/// Po ustawieniu opcjonalnego argumentu na TRUE, funkcja zwraca filtrowaną listę rozszerzeń - przydatne dla
		/// kontrolki OpenFileDialog.
		/// ------------------------------------------------------------------------------------------------------------
		public string JoinSupportedExtensions( bool filter = false )
		{
			string extensions = "";

			if( !filter )
				for( int x = 0, y = this._supports.Count(); x < y; ++x )
					if( x == 0 )
						extensions += this._supports[x];
					else
						extensions += ", " + this._supports[x];
			//else
			//	for( int x = 0, y = this._supports.Count(); x < y; ++x )


			return extensions;
		}

		/// 
		/// Parser plików o rozrzeszeniu .CSV
		/// ------------------------------------------------------------------------------------------------------------
		private void ParseCSV( Encoding encoding, bool cols_only = false )
		{
			// otwórz plik
			StreamReader file = new StreamReader( this._file, encoding, true );

			int chr,		// numer pobranego znaku
				cols = 1,	// ilość kolumn / nr. kolumny
				rows = 0;	// ilość wierszy / nr. wiersza

			// policz ilość kolumn
			while( (chr = file.Read()) != -1 )
				if( chr == '\n' )
					break;
				else if( chr == this._separator )
					++cols;

			// policz ilość wierszy
			if( !cols_only )
				while( (chr = file.Read()) != -1 )
					if( chr == '\n' )
						rows++;
			
			// zamknij plik i otwórz ponownie (przesuwanie kursora na początek)
			file.Close();
			file = new StreamReader( this._file, encoding, true );

			// przydziel pamięć
			this._columns = cols;
			this._rows    = rows;
			this._column  = new string[cols];
			this._row     = cols_only ? null : new string[rows,cols];

			cols = 0;

			// pobierz kolumny
			while( (chr = file.Read()) != -1 )
				if( chr == '\n' )
					break;
				else if( chr == this._separator )
					++cols;
				else
					this._column[cols] += (char)chr;

			// przerwij, jeżeli funkcja miała pobrać tylko kolumny
			if( cols_only )
				return;

			cols = 0;
			rows = 0;

			// pobierz wiersze
			while( (chr = file.Read()) != -1 )
				if( chr == '\n' )
				{
					++rows;
					cols = 0;
				}
				else if( chr == ';' )
					++cols;
				else
					this._row[rows,cols] += (char)chr;
			
			// zamknij strumień pliku
			file.Close();
		}
	}
}