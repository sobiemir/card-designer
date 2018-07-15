using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using CDesigner.Utils;

///
/// $c06 DatabaseReader.cs
/// 
/// Czytnik plików bazy danych.
/// Na razie możliwość odczytu tylko plików .CSV.
/// 
/// Autor: Kamil Biały
/// Od wersji: 0.8.x.x
/// Ostatnia zmiana: 2015-11-08
///
/// Poprawiono błąd który występował przy podaniu separatora nie występującego w nagłówku a występującego w treści.
/// Poprawiono błąd występujący przy podaniu separatora występującego w nierównych ilościach w rekordach.
/// Dodano typy danych dla kolumn.
/// 
/// TODO: Rozszerzyć działanie na inne pliki o innych rozszerzeniach.
/// TODO: Dorobić wczytywanie danych z bazy danych (albo osobną klasę).
/// TODO: Szybkie parsowanie - określona ilość rekordów, określona długość pojedynczego rekordu...
///

namespace CDesigner
{
	///
	/// Klasa odczytu bazy danych.
	/// Na razie może odczytać tylko bazę danych w formacie .CSV.
	/// Planowana jest rozbudowa klasy.
	///
	public class DatabaseReader
	{
		// ===== PRIVATE VARIABLES =====

		/// <summary>Gotowość pliku do dalszego przetwarzania.</summary>
		private bool _ready = false;

		/// <summary>Nazwa pliku.</summary>
		private string _file = "";

		/// <summary>Rozszerzenie pliku.</summary>
		private string _extension = "";

		/// <summary>Separator kolumn (plik CSV).</summary>
		private char _separator = ';';

		/// <summary>Kodowanie pliku.</summary>
		private Encoding _encoding;

		/// <summary>Lista obsługiwanych rozszerzeń plików.</summary>
		private static string[] _supports = {"CSV"};

		/// <summary>Opis rozszerzeń - dla OpenFileDialog.</summary>
		private static string[] _descriptions =
		{
			"Comma-Separated"
		};

		/// <summary>Ilość pobranych kolumny.</summary>
		private int _columns = 0;

		/// <summary>Ilość pobranych wierszy.</summary>
		private int _rows = 0;

		/// <summary>Pobrane kolumny z pliku.</summary>
		private string[]  _column = null;

		/// <summary>Pobrane wiersze z pliku.</summary>
		private string[,] _row = null;

		/// <summary>Lista typów kolumn.</summary>
		private List<DATATYPE> _types = new List<DATATYPE>();

		// ===== PUBLIC FUNCTIONS =====

		/**
		 * <summary>
		 * Pobiera lub ustawia ustawia separator dla kolumn (plik CSV).
		 * Nie wywołuje automatycznie funkcji Parse()!
		 * </summary>
		 * --------------------------------------------------------------------------------------------------------- **/
		public char Separator
		{
			get { return this._separator; }
			set { this._separator = value; }
		}

		/**
		 * <summary>
		 * Pobiera lub ustawia kodowanie pliku.
		 * Domyślnie kodowanie ustawione jest na Encoding.Default.
		 * Nie wywołuje automatycznie funkcji Parse()!
		 * </summary>
		 * --------------------------------------------------------------------------------------------------------- **/
		public Encoding Encoding
		{
			get { return this._encoding; }
			set { this._encoding = value; }
		}

		/**
		 * <summary>
		 * Pobiera ilość dostępnych kolumn.
		 * </summary>
		 * --------------------------------------------------------------------------------------------------------- **/
		public int ColumnsNumber
		{
			get { return this._columns; }
		}

		/**
		 * <summary>
		 * Pobiera ilość dostępnych wierszy.
		 * </summary>
		 * --------------------------------------------------------------------------------------------------------- **/
		public int RowsNumber
		{
			get { return this._rows; }
		}

		/**
		 * <summary>
		 * Pobiera tablicę wczytanych kolumn.
		 * </summary>
		 * --------------------------------------------------------------------------------------------------------- **/
		public string[] Columns
		{
			get { return this._column; }
		}

		/**
		 * <summary>
		 * Pobiera tablicę wczytanych wierszy.
		 * </summary>
		 * --------------------------------------------------------------------------------------------------------- **/
		public string[,] Rows
		{
			get { return this._row; }
		}

		/**
		 * <summary>
		 * Zwraca listę typów kolumn.
		 * </summary>
		 * --------------------------------------------------------------------------------------------------------- **/
		public List<DATATYPE> Types
		{
			get { return this._types; }
		}

		/**
		 * <summary>
		 * Pobiera obsługiwane przez parser rozszerzenia plików.
		 * </summary>
		 * --------------------------------------------------------------------------------------------------------- **/
		public static string[] SupportedExtensions
		{
			get { return DatabaseReader._supports; }
		}

		/**
		 * <summary>
		 * Konstruktor klasy DatabaseReader.
		 * Sprawdza czy podany plik istnieje na dysku.
		 * </summary>
		 * 
		 * <param name="file">Plik z obsługiwaną bazą danych do wczytania.</param>
		 * --------------------------------------------------------------------------------------------------------- **/
		public DatabaseReader( string file )
		{
			// sprawdź czy plik istnieje
			try
			{
				File.Open( file, FileMode.Open, FileAccess.Read ).Close();
				this._ready = true;
			}
			catch( IOException ex )
			{
				Program.LogError( ex.Message, Language.GetLine("MessageNames", (int)MSGBLIDX.FileLoading), false );
				this._ready = false;
			}

			// domyślny separator i kodowanie
			this._separator = Settings.Info.DR_Separator;
			switch( Settings.Info.DR_Encoding )
			{
				case 0: this._encoding = Encoding.Default;          break;
				case 1: this._encoding = Encoding.ASCII;            break;
				case 2: this._encoding = Encoding.UTF8;             break;
				case 3: this._encoding = Encoding.BigEndianUnicode; break;
				case 4: this._encoding = Encoding.Unicode;          break;
				case 5: this._encoding = Encoding.UTF32;            break;
				case 6: this._encoding = Encoding.UTF7;             break;
			}

			// pobierz rozszerzenie
			this._extension = new FileInfo(file).Extension.ToLower();
			this._file      = file;
		}

		/**
		 * <summary>
		 * Konstruktor klasy DatabaseReader.
		 * Wersja z wyświetlaniem okna wyboru pliku i ustawień przetwarzania bazy danych.
		 * </summary>
		 * --------------------------------------------------------------------------------------------------------- **/
		public DatabaseReader()
		{
			// okno wyboru pliku
			OpenFileDialog dialog = new OpenFileDialog();

			dialog.Title  = Language.GetLine( "MessageNames", (int)MSGBLIDX.DatafileSelect );
			dialog.Filter = DatabaseReader.JoinSupportedExtensions( true );

			DialogResult result = dialog.ShowDialog();

			this._ready = false;

			// kliknięto anuluj
			if( result != DialogResult.OK )
				return;
			
			// domyślny separator i kodowanie
			this._separator = Settings.Info.DR_Separator;
			switch( Settings.Info.DR_Encoding )
			{
				case 0: this._encoding = Encoding.Default;          break;
				case 1: this._encoding = Encoding.ASCII;            break;
				case 2: this._encoding = Encoding.UTF8;             break;
				case 3: this._encoding = Encoding.BigEndianUnicode; break;
				case 4: this._encoding = Encoding.Unicode;          break;
				case 5: this._encoding = Encoding.UTF32;            break;
				case 6: this._encoding = Encoding.UTF7;             break;
			}

			// rozszerzenie
			this._extension = new FileInfo(dialog.FileName).Extension.ToLower();
			this._file      = dialog.FileName;

			// otwieranie okna ustawień pliku z bazą danych
#		if DEBUG
			Program.LogMessage( "" );
			Program.LogMessage( "SOW ====================================================================" );
			Program.LogMessage( "Otwieranie okna z ustawieniami pliku bazy danych." );
#		endif

			// automatyczne parsowanie pliku
			this.Parse();

			//DatafileSettingsForm settings = new DatafileSettingsForm( this );
			//result = settings.ShowDialog();

#		if DEBUG
			Program.LogMessage( "EOW ====================================================================" );
			Program.LogMessage( "" );
#		endif

			// anulowano przetwarzanie bazy danych
			//if( result != DialogResult.OK )
			//	return;

			this._ready = true;
		}

		/**
		 * <summary>
		 * Zwraca informację o tym czy klasa jest gotowa do dalszych operacji.
		 * Dzięki temu można sprawdzić czy ładowanie pliku zostało przerwane.
		 * </summary>
		 * --------------------------------------------------------------------------------------------------------- **/
		public bool IsReady()
		{
			return this._ready;
		}

		/**
		 * <summary>
		 * Robi to samo co konstruktor z tą różnicą że nie tworzy nowego obiektu.
		 * Nie wywołuje automatycznie funkcji Parse()!
		 * </summary>
		 * 
		 * <param name="file">Plik z obsługiwaną bazą danych do wczytania.</param>
		 * --------------------------------------------------------------------------------------------------------- **/
		public void ChangeDatabase( string file )
		{
			// sprawdź czy plik istnieje
			try
			{
				File.Open( file, FileMode.Open, FileAccess.Read ).Close();
				this._ready = true;
			}
			catch( IOException ex )
			{
				Program.LogError( ex.Message, Language.GetLine("MessageNames", (int)MSGBLIDX.FileLoading), false );
				this._ready = false;
			}

			// pobierz rozszerzenie
			this._extension = new FileInfo(file).Extension.ToLower();
			this._file      = file;

			// pozostałe rzeczy się nie zmieniają (kodowanie, separator)...
		}

		/**
		 * <summary>
		 * Automatyczny parser danych.
		 * Wywołuje odpowiednią funkcję w zależności od rozszerzenia pliku.
		 * Można pobrać tylko nazwy kolumn poprzez ustawienie drugiego argumentu na TRUE.
		 * </summary>
		 * 
		 * <param name="cols_only">Pobieranie z pliku tylko kolumn.</param>
		 * --------------------------------------------------------------------------------------------------------- **/
		public void Parse( bool cols_only = false )
		{
			if( this._extension == ".csv" )
			{
				// parsowanie pliku CSV
				this.ParseCSV( this._encoding, cols_only );
				this._ready = true;
			}
			else
			{
				// błąd - brak obsługiwanego rozszerzenia
				Program.LogError
				(
					String.Format
					(
						Language.GetLine("DatabaseSettings", "Messages", 0),
						this._extension,
						DatabaseReader.JoinSupportedExtensions()
					),
					Language.GetLine( "MessageNames", (int)MSGBLIDX.ParseError ),
					false
				);
				this._ready = false;
			}

			GC.Collect();
		}

		/**
		 * <summary>
		 * Łączy po przecinku dostępne rozszerzenia.
		 * Po ustawieniu opcjonalnego argumentu na TRUE, funkcja zwraca filtrowaną listę rozszerzeń - przydatne dla
		 * kontrolki OpenFileDialog.
		 * </summary>
		 * 
		 * <param name="filter">Filtrowana lista rozszerzeń (TRUE/FALSE).</param>
		 * --------------------------------------------------------------------------------------------------------- **/
		public static string JoinSupportedExtensions( bool filter = false )
		{
			string extensions = "";

			// normalna lista rozszerzeń (oddzielona przecinkiem)
			if( !filter )
				for( int x = 0, y = DatabaseReader._supports.Count( ); x < y; ++x )
					if( x == 0 )
						extensions += DatabaseReader._supports[x];
					else
						extensions += ", " + DatabaseReader._supports[x];
			// filtrowana lista rozszerzeń (oddzielona |) - dla OpenFileDialog
			else
				for( int x = 0, y = DatabaseReader._supports.Count( ); x < y; ++x )
					if( x == 0 )
						extensions += DatabaseReader._descriptions[x] + "|*." + DatabaseReader._supports[x];
					else
						extensions += "|" + DatabaseReader._descriptions[x] + "|*." + DatabaseReader._supports[x];

			return extensions;
		}

		// ===== PRIVATE FUNCTIONS =====

		/**
		 * <summary>
		 * Parser plików o rozszerzeniu .CSV.
		 * </summary>
		 * 
		 * <param name="encoding">Kodowanie pliku.</param>
		 * <param name="cols_only">Pobieranie tylko nazw kolumn.</param>
		 * --------------------------------------------------------------------------------------------------------- **/
		private void ParseCSV( Encoding encoding, bool cols_only = false )
		{
#		if DEBUG
			Program.LogMessage( ">> Przetwarzanie pliku .CSV." );
#		endif

			// otwórz plik
			StreamReader file = new StreamReader( this._file, encoding, true );

			int chr,		// numer pobranego znaku
				cols = 1,	// ilość kolumn / nr. kolumny
				rows = 0;	// ilość wierszy / nr. wiersza

			// wyczyść typy
			this._types.Clear();

			// policz ilość kolumn
			while( (chr = file.Read()) != -1 )
				if( this.IsEOL(chr, file) )
					break;
				else if( chr == this._separator )
					++cols;

			// policz ilość wierszy
			if( !cols_only )
				while( (chr = file.Read()) != -1 )
					if( this.IsEOL(chr, file) )
						rows++;
			
			// zamknij plik i otwórz ponownie (przesuwanie kursora na początek)
			file.Close();
			file = new StreamReader( this._file, encoding, true );

			// przydziel pamięć
			this._columns = cols;
			this._rows    = rows;
			this._column  = new string[cols];
			this._row     = cols_only ? null : new string[cols,rows];

			cols = 0;
			string cell = "";

			// pobierz kolumny
			while( (chr = file.Read()) != -1 )
				// koniec wiersza
				if( this.IsEOL(chr, file) )
				{
					this._column[cols] = cell;
					break;
				}
				// separator
				else if( chr == this._separator )
				{
					this._column[cols] = cell;
					cell = "";
					++cols;
				}
				// pobieranie nazwy kolumny
				else
					 cell += (char)chr;

			// ustaw typ początkowy dla każdej kolumny - String
			for( int x = 0; x < this._columns; ++x )
				this._types.Add( DATATYPE.String );

#		if DEBUG
			// tylko kolumny, nie można sprawdzić typu...
			if( cols_only || Settings.Info.DR_AutoCheck == 0 )
			{
				Program.LogMessage( "Lista kolumn:" );
				for( int x = 0; x < this._columns; ++x )
					Program.LogMessage( "    # [string] " + this._column[x] );
				Program.LogMessage( "Ilość kolumn: " + this._columns );
			}
#		endif

			// przerwij, jeżeli funkcja miała pobrać tylko kolumny
			if( cols_only )
				return;

#		if DEBUG
			if( Settings.Info.DR_AutoCheck == 0 )
				Program.LogMessage( "Ilość wierszy: " + this._rows );
#		endif

			int separator = cols > 0 ? this._separator : -1;

			// poinformuj o tym że plik zawiera tylko jedną kolumnę
#		if DEBUG
			if( separator == -1 )
				Program.LogMessage( "Plik z podanym separatorem zawiera tylko jedną kolumnę!" );
#		endif

			cols = 0;
			rows = 0;
			cell = "";

			// pobierz wiersze
			while( (chr = file.Read()) != -1 )
			{
				// koniec rekordu
				if( this.IsEOL(chr, file) )
				{
					// zapisz i resetuj komórkę
					this._row[cols,rows] = cell;
					cell = "";

					++rows;

					// kontroler wierszy
					if( rows >= this._rows )
						break;

					cols = 0;
				}
				// separator
				else if( chr == separator )
				{
					this._row[cols,rows] = cell;
					cell = "";

					++cols;

					// kontroler kolumn (coś z plikiem jest nie tak...)
					if( cols >= this._columns )
						--cols;
				}
				// pobieranie komórki
				else
					cell += (char)chr;
			}

			// automatyczne sprawdzanie typu kolumny (nie zawsze trafne)
			if( Settings.Info.DR_AutoCheck == 1 )
			{
				this.AutoCheckType();

#			if DEBUG
				// wyświetl informacje o kolumnach i wierszach
				Program.LogMessage( "Lista kolumn:" );
				for( int x = 0; x < this._columns; ++x )
					Program.LogMessage( "    # [" + (this._types[x] == DATATYPE.String
						? "string"
						: (this._types[x] == DATATYPE.Integer
							? "integer"
							: (this._types[x] == DATATYPE.Float
								? "float"
								: "char"
							)
						)
					) + "] " + this._column[x] );
				Program.LogMessage( "Ilość kolumn: " + this._columns );
				Program.LogMessage( "Ilość wierszy: " + this._rows );
#			endif
			}
			
			// zamknij strumień pliku
			file.Close();

			GC.Collect();
		}

		/**
		 * <summary>
		 * Automatyczne rozpoznawanie typów kolumn dla danych.
		 * </summary>
		 * --------------------------------------------------------------------------------------------------------- **/
		private void AutoCheckType()
		{
#		if DEBUG
			Program.LogMessage( "Uruchomienie automatycznego rozpoznawania typów kolumn." );
#		endif

			int    ival;
			double dval;

			// dla każdej kolumny...
			for( int x = 0; x < this._columns; ++x )
			{
				DATATYPE type = DATATYPE.Integer;

				// dla każdego wiersza...
				for( int y = 0; y < this._rows; ++y )
				{
					// próbuj zamienić na INT
					if( type == DATATYPE.Integer && !Int32.TryParse(this._row[x,y], out ival) )
					{
						// jeżeli nie da rady, próbuj zamienić na DOUBLE
						if( Double.TryParse(this._row[x,y], out dval) )
							type = DATATYPE.Float;
						else
						{
							// jeżeli nie da rady, sprawdź czy kolumna nie zawiera znaków
							for( int z = 0; z <= y; ++z )
								if( this._row[x,y].Length != 1 )
								{
									type = DATATYPE.String;
									break;
								}

							// jeżeli nie, ustaw jako STRING
							if( type != DATATYPE.String )
								type = DATATYPE.Character;
						}
					}
					// próbuj zamienić na DOUBLE
					else if( type == DATATYPE.Float && !Double.TryParse(this._row[x,y], out dval) )
					{
						// jeżeli nie da rady, sprawdź czy kolumna nie zawiera znaków
						for( int z = 0; z <= y; ++z )
							if( this._row[x,y].Length != 1 )
							{
								type = DATATYPE.String;
								break;
							}
						
						// jeżeli nie, ustaw jako STRING
						if( type != DATATYPE.String )
							type = DATATYPE.Character;
					}
					// sprawdź czy komórka zawiera znak
					else if( type == DATATYPE.Character && this._row[x,y].Length > 1 )
						type = DATATYPE.String;
					// jeżeli typ jest ciągiem znaków, pomiń kolumnę
					else if( type == DATATYPE.String )
						break;
				}

				// zapisz typ kolumny
				this._types[x] = type;
			}
		}

		/**
		 * <summary>
		 * Sprawdza czy aktualny znak jest znakiem nowej linii.
		 * Obsługuje znaki CRLF / LF / CR.
		 * </summary>
		 * 
		 * <param name="chr">Aktualny znak.</param>
		 * <param name="file">Otwarty strumień pliku.</param>
		 * --------------------------------------------------------------------------------------------------------- **/
		private bool IsEOL( int chr, StreamReader reader )
		{
			// CR / CRLF
			if( chr == '\r' )
			{
				if( reader.Peek() == '\n' )
					reader.Read();

				return true;
			}
			// LF
			else if( chr == '\n' )
				return true;

			return false;
		}
	}
}
