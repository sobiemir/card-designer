///
/// $i[xx] DatafileStream.cs
/// 
/// Wczytywanie danych z pliku.
/// 
/// Autor: Kamil Biały
/// Od wersji: 0.8.x.x
/// Ostatnia zmiana: 2016-11-08
/// 
/// CHANGELOG:
/// [10.08.2015] Wersja początkowa.
/// [14.07.2016] Drobne poprawki, komentarze i regiony.
/// [08.11.2016] Zamiana klasy DatabaseReader na IOFileData.
///

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace CDesigner.Utils
{
	/// 
	/// <summary>
	/// Klasa do tworzenia strumienia z wybranego pliku.
	/// Wczytuje cały plik lub jego części do pamięci w zależności od żądania.
	/// </summary>
	///
	/// @todo Automatyczne rozpoznawanie kolumn. [0.9.x.x]
	/// @todo Zautomatyzowanie wczytywania części pliku. [1.0.x.x]
	/// @todo Pozycja startowa dla funkcji parsujących pliki. [0.9.x.x]
	/// @todo Ulepszenie przetwarzania danych z formatu CSV. [1.0.x.x]
	/// 
	public class DatafileStream
	{
#region ZMIENNE
		/// <summary>Lista kolumn pobranych z pliku.</summary>
		private string[] _column;

		/// <summary>Liczba wszystkich kolumn w pliku.</summary>
		private int _columnCount = -1;

		/// <summary>Tablica krotek pobranych z pliku.</summary>
		private string[,] _row;
		/// <summary>Ilość wszystkich wierszy w pliku.</summary>
		private int _rowCount = -1;

		/// <summary>Kodowanie z jakim plik jest otwierany.</summary>
		private Encoding _encoding;

		/// <summary>Znak który traktowany jest jako separator kolumn.</summary>
		private char _separator = ';';

		/// <summary>Informacja o tym czy plik został poprawnie przetworzony.</summary>
		private bool _isReady = false;

		/// <summary>Nazwa rozszerzenia pliku wraz kropką z przodu.</summary>
		private string _extension = null;

		/// <summary>Nazwa przetwarzanego pliku.</summary>
		private string _filename = null;

		/// <summary>Automatyczne sprawdzanie podstawowych typów kolumn.</summary>
		private bool _autoCheck = false;

		/// <summary>Format nowej linii (Linux/Windows/Macintosh).</summary>
		private LINEENDING _lineEnding = LINEENDING.Undefined;

		/// <summary>Podstawowy typ wybranej kolumny.</summary>
		private DATATYPE[] _dataType = null;

		//private string[] _dataFormat = null;
		
		/// <summary>Przepełnienie (gdy pobierane jest tylko kilka rekordów z pliku.</summary>
		private bool _overflow = false;

		/// <summary>Lista obsługiwanych formatów dla plików bazodanowych.</summary>
		private static string[] _supports = {"csv"};
#endregion

#region KONSTRUKTORY / WŁAŚCIWOŚCI
		/// <summary>
		/// Konstruktor klasy z jednym parametrem.
		/// Funkcja jako parametr przyjmuje nazwę pliku do przetwarzania.
		/// Podczas zapisu nazwy pliku sprawdzane jest czy plik istnieje i można go otworzyć (uprawnienia).
		/// Do otwarcia potrzebne są tylko uprawnienia do odczytu, w przypadku błędu wyrzuca komunikat z treścią błędu.
		/// </summary>
		/// 
		/// <example>
		/// Poniższy przykład ładuje plik CSV i wyświetla jego kolumny:
		/// <code>
		/// DatafileStream stream = new DatafileStream( "test.csv" );
		/// 
		/// stream.Encoding  = Encoding.UTF8;
		/// stream.Separator = ';';
		/// 
		/// if( !stream.IsReady() )
		///		return;
		/// 
		/// if( stream.Parse(0, true) )
		/// {
		///		Console.WriteLine( "Ilość kolumn: " + stream.ColumnsNumber );
		///		Console.WriteLine( "Lista kolumn:" );
		///		for( int x = 0; x &lt; stream.ColumnsNumber; ++x )
		///			Console.WriteLine( "\t" + stream.Column[x] );
		///	}
		/// </code>
		/// </example>
		/// 
		/// <param name="file">Nazwa pliku do przetwarzania.</param>
		//* ============================================================================================================
		public DatafileStream( string file )
		{
			// sprawdź czy plik istnieje i czy można go otworzyć
			try
			{
				using( FileStream fs = File.Open(file, FileMode.Open, FileAccess.Read) )
					fs.Close();
				this._isReady = true;
			}
			catch( Exception ex )
			{
				Program.LogError( ex.Message, Language.GetLine("MessageNames", (int)MSGBLIDX.FileLoading), false );
				this._isReady = false;

				return;
			}

			// ustaw separator i automatyczne sprawdzanie typów
			this._separator = Settings.Info.DR_Separator;
			this._autoCheck = Settings.Info.DR_AutoCheck != 0 ? true : false;

			// ustaw domyślne kodowanie pliku
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

			// pobierz rozszerzenie i zapisz nazwę pliku
			this._extension = new FileInfo(file).Extension.ToLower();
			this._filename  = file;
		}

		/// <summary>
		/// Podstawowy typ kolumny.
		/// Na jego podstawie ustalane są filtry które można zastosować dla wybranej kolumny.
		/// Właściwość tylko do odczytu.
		/// </summary>
		//* ============================================================================================================
		public DATATYPE[] DataType
		{
			get { return this._dataType; }
		}

		/// <summary>
		/// Nazwa pliku przekazywana do konstruktora.
		/// Właściwość tylko do odczytu.
		/// </summary>
		//* ============================================================================================================
		public string FileName
		{
			get { return this._filename; }
		}

		/// <summary>
		/// Nadmiarowość danych w bazie danych.
		/// Ustawiana na true gdy przy obcinaniu rekordów z pliku ilość wierszy w kolumnie jest większa od limitu
		/// pobieranych danych ustawionym w funkcji parsującej plik.
		/// Właściwość tylko do odczytu.
		/// </summary>
		//* ============================================================================================================
		public bool Overflow
		{
			get { return this._overflow; }
		}

		/// <summary>
		/// Automatyczne sprawdzanie typów kolumn.
		/// Kolumny mogą być sprawdzane automatycznie po ustawieniu tej właściwości na true.
		/// </summary>
		/// 
		/// @note Operacja planowana na wersję 0.9.x.x
		//* ============================================================================================================
		public bool AutoCheckTypes
		{
			get { return  this._autoCheck; }
			set { this._autoCheck = value; }
		}

		/// <summary>
		/// Kodowanie użyte w pliku.
		/// Plik jest odczytywany w kodowaniu podanym do tej właściwości.
		/// Błędnie zklasyfikowane dla pliku kodowanie może zabużyć całą strukturę bazy danych.
		/// </summary>
		//* ============================================================================================================
		public Encoding Encoding
		{
			get { return this._encoding; }
			set { this._encoding = value; }
		}

		/// <summary>
		/// Lista dostępnych kolumn w pliku bazy danych.
		/// Właściwość tylko do odczytu.
		/// </summary>
		//* ============================================================================================================
		public string[] Column
		{
			get { return this._column; }
		}

		/// <summary>
		/// Liczba dostępnych kolumn w pliku bazy danych.
		/// Właściwość tylko do odczytu.
		/// </summary>
		//* ============================================================================================================
		public int ColumnsNumber
		{
			get { return this._columnCount; }
		}

		/// <summary>
		/// Lista dostępnych krotek w pliku bazy danych.
		/// Poprzez krotki rozumie się wszystkie przecięcia wierszy z kolumnami.
		/// Wałaściwość tylko do odczytu.
		/// </summary>
		//* ============================================================================================================
		public string[,] Row
		{
			get { return this._row; }
		}

		/// <summary>
		/// Liczba dostępnych wierszy w pliku bazy danych.
		/// Aby obliczyć ilość krotek należy pomnożyć ilość kolumn przez ilość wierszy.
		/// Właściwość tylko do odczytu.
		/// </summary>
		//* ============================================================================================================
		public int RowsNumber
		{
			get { return this._rowCount; }
		}

		/// <summary>
		/// Separator kolumn w pliku bazy danych.
		/// Jest to ważny element przetwarzania plików pojedyncze wpisy (krotki) w bazie danych.
		/// Aby dobrać odpowiedni separator najpierw należy zaznajomić się ze strukturą pliku.
		/// </summary>
		//* ============================================================================================================
		public char Separator
		{
			get { return this._separator; }
			set { this._separator = value; }
		}
#endregion

#region FUNKCJE PODSTAWOWE
		/// <summary>
		/// Funkcja pobierająca gotowość klasy do dalszej pracy.
		/// W przypadku jakiegokolwiek błędu wartość ustawiana jest na false.
		/// </summary>
		/// <returns>Gotowość klasy do dalszej pracy (true) lub sygnalizacja błędu (false).</returns>
		//* ============================================================================================================
		public bool IsReady()
		{
			return this._isReady;
		}

		/// <summary>
		/// Zwraca listę rozszerzeń oddzieloną przecinkami.
		/// Funkcja potrafi również utworzyć filtr rozszerzeń dla okna wyboru z dostępnych rozszerzeń i tłumaczeń.
		/// </summary>
		/// 
		/// <param name="filter">Tworzenie filtru dla okna wyboru pliku.</param>
		/// 
		/// <returns>Lista rozszerzeń oddzielona przecinkami lub filtr.</returns>
		//* ============================================================================================================
		public static string ExtensionsFilter( bool filter = false )
		{
			string extensions = "";
			
			// normalna lista rozszerzeń (oddzielona przecinkiem)
			if( !filter )
				for( int x = 0, y = DatafileStream._supports.Count( ); x < y; ++x )
					if( x == 0 )
						extensions += DatafileStream._supports[x];
					else
						extensions += ", " + DatafileStream._supports[x];
			// filtrowana lista rozszerzeń (oddzielona |) - dla OpenFileDialog
			else
			{
				List<string> values = Language.GetLines( "DatafileSettings", "Extensions" );

				for( int x = 0, y = DatafileStream._supports.Count(); x < y; ++x )
					if( x == 0 )
					{
						extensions += values[x];
						extensions += "|*." + DatafileStream._supports[x];
					}
					else
					{
						extensions += "|" + values[x];
						extensions += "|*." + DatafileStream._supports[x];
					}
			}

			return extensions;
		}

		/// <summary>
		/// Podmienia strumień pliku do zczytywania zawartości.
		/// Funkcja sprawdza czy plik istnieje i można go otworzyć (odpowiednie uprawnienia).
		/// Zmienia tylko ustawienia dotyczące bezpośrednio pliku (nazwa, rozszerzenie).
		/// </summary>
		/// 
		/// <param name="file">Nazwa nowego pliku do wczytania.</param>
		//* ============================================================================================================
		public void ChangeFile( string file )
		{
			// sprawdź czy plik istnieje
			try
			{
				using( FileStream fs = File.Open(file, FileMode.Open, FileAccess.Read) )
					fs.Close();
				this._isReady = true;
			}
			catch( IOException ex )
			{
				Program.LogError( ex.Message, Language.GetLine("MessageNames", (int)MSGBLIDX.FileLoading), false );
				this._isReady = false;
			}

			// pobierz rozszerzenie
			this._extension = new FileInfo(file).Extension.ToLower();
			this._filename  = file;

			// pozostałe rzeczy się nie zmieniają (kodowanie, separator)...
		}

		/// <summary>
		/// Parsowanie wcześniej wczytanego pliku.
		/// Automatycznie dobiera odpowiednią funkcję w zależności od rozszerzenia.
		/// W przypadku nieobsługiwanego rozszerzenia wyświetlony zostanie komunikat z błędem.
		/// </summary>
		/// 
		/// <param name="records">Ilość rekordów do pobrania z pliku.</param>
		/// <param name="cols_only">Pobieranie tylko nazw kolumn.</param>
		/// 
		/// <returns>Jeżeli udało się przetworzyć dane z pliku, zwraca true, w przeciwnym razie false.</returns>
		//* ============================================================================================================
		public bool Parse( int records, bool cols_only = false )
		{
			// parsowanie pliku CSV
			if( this._extension == ".csv" )
				this.ParseCSV( this._encoding, records, cols_only );

			// błąd - brak obsługiwanego rozszerzenia
			else
			{
				Program.LogError
				(
					String.Format
					(
						Language.GetLine("DatabaseSettings", "Messages", 0),
						this._extension,
						IOFileData.getExtensionsList()
					),
					Language.GetLine( "MessageNames", (int)MSGBLIDX.ParseError ),
					false
				);

				return false;
			}

			GC.Collect();
			return true;
		}
#endregion

#region FUNKCJE PARSUJĄCE
		/// <summary>
		/// Parsowanie plików o rozszerzeniu CSV (Comma Separated Values).
		/// Pliki o rozszerzeniu CSV w założeniu muszą być oddzielone przecinkami.
		/// Dla języka polskiego są to jednak średniki w związku z tym dla tego formatu dostępny jest wybór separatora.
		/// Funkcjonalność parsera nie jest w całości zrealizowana (brakuje paru elementów).
		/// Więcej informacji na ten temat na stronie opisującej
		/// <a href="https://pl.wikipedia.org/wiki/CSV_(format_pliku)">format CSV</a>.
		/// </summary>
		/// 
		/// <param name="encoding">Kodowanie z jakim zostaną wczytane dane z pliku.</param>
		/// <param name="records">Ilość pobieranych rekordów.</param>
		/// <param name="cols_only">Pobieranie tylko kolumn, bez wierszy.</param>
		//* ============================================================================================================
		private void ParseCSV( Encoding encoding, int records, bool cols_only = false )
		{
			Program.LogMessage( ">> Przetwarzanie pliku CSV." );

			// resetuj koniec linii
			this._lineEnding = LINEENDING.Undefined;

			// otwórz plik do odczytu
			StreamReader file = new StreamReader( this._filename, this._encoding, true );

			int chr,		// numer pobranego znaku
				cols = 1,	// ilość kolumn / numer kolumny
				rows = 0;	// ilość wierszy / numer wiersza

			this._overflow = false;

			// policz ilość kolumn i wykryj znak zakończenia linii (LF / CR / CR+LF)
			while( (chr = file.Read()) != -1 )
			{
				// koniec linii
				if( chr == '\r' || chr == '\n' )
				{
					// unix lub mac
					if( this._lineEnding == LINEENDING.Undefined )
					{
						this._lineEnding = LINEENDING.Unix;
						if( chr == '\r' )
							this._lineEnding = LINEENDING.Macintosh;
					}
					// windows
					else if( this._lineEnding == LINEENDING.Macintosh )
						this._lineEnding = LINEENDING.Windows;

					continue;
				}

				// koniec informacji o kolumnach
				if( this._lineEnding != LINEENDING.Undefined )
					break;

				// zwiększ ilość kolumn
				if( chr == this._separator )
					++cols;
			}

			bool next_row = true;

			// policz ilość wierszy jeżeli potrzeba
			if( !cols_only )
				while( (chr = file.Read()) != -1 )
				{
					// szukaj znaku końca linii (LF / CR / CR+LF)
					if( chr == '\r' || chr == '\n' )
					{
						next_row = true;
						continue;
					}

					// zwiększ ilość wierszy
					if( next_row )
					{
						++rows;
						next_row = false;

						// sprawdź czy istnieje ograniczenie pobieranych wierszy
						if( records > 0 && rows > records )
						{
							this._overflow = true;
							rows = records;
							break;
						}
					}
				}

			this._columnCount = cols;
			this._rowCount    = rows;
			
			// przydziel pamięć
			this._column     = new string[this._columnCount];
			this._row        = rows > 0 ? new string[cols, rows] : null;
			this._dataType   = new DATATYPE[cols];
			//this._dataFormat = new string[cols];
			
			file.Close();
			file = new StreamReader( this._filename, encoding, true );

			cols        = 0;
			string cell = "";

			// pobierz kolumny
			while( (chr = file.Read()) != -1 )
			{
				// szukaj znaku końca linii (LF / CR / CR+LF)
				if( chr == '\r' || chr == '\n' )
				{
					if( this._lineEnding == LINEENDING.Windows && chr == '\n' )
					{
						this._column[cols] = cell;
						break;
					}
					else if( this._lineEnding != LINEENDING.Windows )
					{
						this._column[cols] = cell;
						break;
					}
				}
				// separator - nowa kolumna
				else if( chr == this._separator )
				{
					this._column[cols] = cell;
					cell = "";
					++cols;
				}
				// pobieranie nazwy kolumny
				else
					cell += (char)chr;
			}

			// ustaw typ początkowy dla każdej kolumny - String
			for( int x = 0; x < this._columnCount; ++x )
			{
				this._dataType[x]   = DATATYPE.String;
				//this._dataFormat[x] = "";
			}

#		if DEBUG
			// tylko jedna kolumna?
			if( this._columnCount == 1 )
				Program.LogMessage( "$$ Plik z podanym separatorem zawiera tylko jedną kolumnę!" );

			// wyświetl informacje o pobranych kolumnach jeżeli:
			// - pobierane są tylko kolumny lub
			// - wyłączone jest automatyczne sprawdzanie typów danych
			if( cols_only || !this._autoCheck )
			{
				Program.LogMessage( "Lista kolumn:" );
				for( int x = 0; x < this._columnCount; ++x )
					Program.LogMessage( "    # [string] " + this._column[x] );
				Program.LogMessage( "Ilość kolumn: " + this._columnCount );
			}
#		endif

			// przerwij w przypadku pobierania tylko kolumn
			if( cols_only )
				return;

			cols = 0;
			rows = 0;
			cell = "";

			// do kontrolowania ilości pobieranych wierszy (po co liczyć w pętli)
			int climit = this._columnCount - 1;

			// pobierz wiersze
			while( (chr = file.Read()) != -1 )
			{
				// szukaj znaku końca linii (LF / CR / CR+LF)
				if( chr == '\r' || chr == '\n' )
				{
					// windows koniec linii oznacza jako \r\n
					if( chr == '\n' && this._lineEnding == LINEENDING.Windows )
						continue;

					// przypisz wiersz
					this._row[cols,rows] = cell;
					cell = "";

					++rows;

					// kontrolowanie ilości pobieranych wierszy
					if( rows >= this._rowCount )
						break;

					cols = 0;
				}
				// separator
				else if( chr == this._separator )
				{
					// ups... separator jest, ale dodatkowej kolumny jakby brak...
					// zapewne zły separator
					if( cols >= climit )
					{
						cell += (char)chr;
						continue;
					}

					// no to do następnej kolumny
					this._row[cols,rows] = cell;
					cell = "";

					++cols;
				}
				// pobieranie komórki
				else
					cell += (char)chr;
			}

#		if DEBUG
			//if( this._autoCheck )
			//{
			//    // wyświetl informacje o kolumnach i wierszach
			//    Program.LogMessage( "Lista kolumn:" );
			//    for( int x = 0; x < this._columnCount; ++x )
			//        Program.LogMessage( "    # [" + (this._dataType[x] == DATATYPE.String
			//            ? "string"
			//            : (this._dataType[x] == DATATYPE.Integer
			//                ? "integer"
			//                : (this._dataType[x] == DATATYPE.Float
			//                    ? "float"
			//                    : "char"
			//                )
			//            )
			//        ) + "] " + this._column[x] );
			//    Program.LogMessage( "Ilość kolumn: " + this._columnCount );
			//}

			// wyświetl ilość wierszy
			Program.LogMessage( "Ilość pobranych wierszy: " + this._rowCount );
			if( records > 0 )
				Program.LogMessage( "Ograniczenie: " + (records < 1 ? "(brak)" : records.ToString()) );
#		endif

			// zamknij strumień
			file.Close();
		}
#endregion

		//public void AutoColumnType( int column )
		//{
		//    DATATYPE type   = DATATYPE.Integer;
		//    string   format = "";
		//    int      ival;
		//    double   dval;

		//    for( int x = 0; x < this._rowCount; ++x )
		//    {
		//        // próbuj zamienić na INT
		//        if( type == DATATYPE.Integer && !Int32.TryParse(this._row[column,x], out ival) )
		//        {
		//            // jeżeli nie da rady, próbuj zamienić na DOUBLE
		//            if( Double.TryParse(this._row[column,x], out dval) )
		//                type = DATATYPE.Float;
		//            else
		//            {
		//                // jeżeli nie da rady, sprawdź czy kolumna nie zawiera znaków
		//                for( int y = 0; y <= x; ++y )
		//                    if( this._row[column,y].Length != 1 )
		//                    {
		//                        type = DATATYPE.String;
		//                        break;
		//                    }

		//                // jeżeli nie, ustaw jako STRING
		//                if( type != DATATYPE.String )
		//                    type = DATATYPE.Character;
		//            }
		//        }
		//        // próbuj zamienić na DOUBLE
		//        else if( type == DATATYPE.Float && !Double.TryParse(this._row[column,x], out dval) )
		//        {
		//            // jeżeli nie da rady, sprawdź czy kolumna nie zawiera znaków
		//            for( int y = 0; y <= x; ++y )
		//                if( this._row[column,y].Length != 1 )
		//                {
		//                    type = DATATYPE.String;
		//                    break;
		//                }
						
		//            // jeżeli nie, ustaw jako STRING
		//            if( type != DATATYPE.String )
		//                type = DATATYPE.Character;
		//        }
		//        // sprawdź czy komórka zawiera znak
		//        else if( type == DATATYPE.Character && this._row[column,x].Length > 1 )
		//            type = DATATYPE.String;
		//        // jeżeli typ jest ciągiem znaków, pomiń kolumnę
		//        else if( type == DATATYPE.String )
		//            break;
		//    }

		//    // zamień wartości na podany typ (currency jest typem DOUBLE)
		//    if( this._dataType[column] != type )
		//    {
		//    }

		//    // zapisz typ
		//    this._dataFormat[column] = format;
		//    this._dataType[column]   = type;
		//}


		//public bool CheckTypeForColumn( int column, DATATYPE type )
		//{
		//    bool correct = true;

		//    // tekst jest zawsze dostępny
		//    if( type == DATATYPE.String )
		//        return true;



		//    return correct;


			//string   format = "";
			//int      ival;
			//double   dval;

			//for( int x = 0; x < this._rowCount; ++x )
			//{
			//    // próbuj zamienić na INT
			//    if( type == DATATYPE.Integer && !Int32.TryParse(this._row[column,x], out ival) )
			//    {
			//        // jeżeli nie da rady, próbuj zamienić na DOUBLE
			//        if( Double.TryParse(this._row[column,x], out dval) )
			//            type = DATATYPE.Double;
			//        else
			//        {
			//            // jeżeli nie da rady, sprawdź czy kolumna nie zawiera znaków
			//            for( int y = 0; y <= x; ++y )
			//                if( this._row[column,y].Length != 1 )
			//                {
			//                    type = DATATYPE.String;
			//                    break;
			//                }

			//            // jeżeli nie, ustaw jako STRING
			//            if( type != DATATYPE.String )
			//                type = DATATYPE.Character;
			//        }
			//    }
			//    // próbuj zamienić na DOUBLE
			//    else if( type == DATATYPE.Double && !Double.TryParse(this._row[column,x], out dval) )
			//    {
			//        // jeżeli nie da rady, sprawdź czy kolumna nie zawiera znaków
			//        for( int y = 0; y <= x; ++y )
			//            if( this._row[column,y].Length != 1 )
			//            {
			//                type = DATATYPE.String;
			//                break;
			//            }
						
			//        // jeżeli nie, ustaw jako STRING
			//        if( type != DATATYPE.String )
			//            type = DATATYPE.Character;
			//    }
			//    // sprawdź czy komórka zawiera znak
			//    else if( type == DATATYPE.Character && this._row[column,x].Length > 1 )
			//        type = DATATYPE.String;
			//    // jeżeli typ jest ciągiem znaków, pomiń kolumnę
			//    else if( type == DATATYPE.String )
			//        break;
			//}

			//// zamień wartości na podany typ (currency jest typem DOUBLE)
			//if( this._dataType[column] != type )
			//{
			//}

			//// zapisz typ
			//this._dataFormat[column] = format;
			//this._dataType[column]   = type;
		//}
	}
}
