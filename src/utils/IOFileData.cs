///
/// $i[xx] IOFileData.cs
/// 
/// Okno edycji bazy danych.
/// Uruchamia okno filtrowania danych.
/// Pozwala na uruchomienie filtrowania danych i łączenie komórek.
/// 
/// Autor: Kamil Biały
/// Od wersji: 0.8.x.x
/// Ostatnia zmiana: 2016-11-08
/// 
/// CHANGELOG:
/// [16.08.2016] Wersja początkowa - wczytywanie danych z pliku CSV.
/// [23.08.2016] Ulepszenie wczytywania, dodane wyjątki, patrz funkcja parseCSV.
/// [26.08.2016] Filtry dla dostępnych rozszerzeń przeniesione z pliku DatabaseReader.
/// [08.11.2016] Drobne komentarze i poprawki.
///

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace CDesigner.Utils
{
	/// 
	/// <summary>
	/// Klasa umożliwiająca odczyt lub zapis danych do pliku.
    /// Pozwala na wczytanie całego lub wybranej części pliku, co znacznie przyspiesza mniejsze operacje
    /// które nie muszą być widoczne dla wszystkich danych.
    /// W tym momencie klasa może wczytać i zapisać tylko pliki w formacie <em>csv</em>.
    /// Szczegóły dotyczące wczytywania konkretnych typów plików znajdują się w funkcjach parsujących te formaty.
	/// </summary>
	///
	/// @todo <dfn><small>[0.9.x.x]</small></dfn> Wczytywanie danych z formatu <em>arff</em>.
	/// @todo <dfn><small>[0.9.x.x]</small></dfn> Automatyczne rozpoznawanie kolumn.
	/// @todo <dfn><small>[0.9.x.x]</small></dfn> Pozycja startowa dla funkcji parsujących pliki.
	/// @todo <dfn><small>[1.0.x.x]</small></dfn> Zapis danych do formatu <em>csv</em>.
	/// 
    public class IOFileData : DataStorage
    {
#region ZMIENNE
        /// <summary>Lista obsługiwanych formatów dla plików bazodanowych.</summary>
        private static string[] _parsers = {"csv"};
        
		/// <summary>Znak traktowany jako separator kolumn, domyślnie jest nim średnik.</summary>
        private int _separator;

        /// <summary>Znak otaczający pole (komórkę), domyślnie jest nim cudzysłów.</summary>
        private int _enclosing;
		
        /// <summary>Kodowanie z jakim plik jest otwierany.</summary>
        private Encoding _encoding;
		
        /// <summary>Nazwa rozszerzenia pliku wraz kropką z przodu.</summary>
        private string _extension;
		
        /// <summary>Nazwa pliku przypisanego do klasy.</summary>
        private string _fileName;
		
        /// <summary>Format nowej linii (CR / LF / CR+LF).</summary>
        private LINEENDING _lineEnding;
#endregion

#region KONSTRUKTORY / WŁAŚCIWOŚCI
        /// <summary>
        /// Pusty konstruktor klasy.
        /// Uzupełnia dane domyślnymi wartościami i wywołuje konstruktor klasy bazowej.
        /// </summary>
        /// 
        /// <seealso cref="FileName"/>
        /// <seealso cref="Encoding"/>
		//* ============================================================================================================
        public IOFileData()
            : base()
        {
            this._separator  = (int)';';
            this._enclosing  = (int)'"';
            this._encoding   = Encoding.Default;
            this._extension  = null;
            this._fileName   = null;
            this._lineEnding = LINEENDING.Undefined;
        }
        
        /// <summary>
        /// Główny konstruktor klasy.
        /// Przypisuje plik do klasy i uzupełnia dane wartościami przekazanymi jako argumenty funkcji.
        /// Uruchamia konstruktor klasy bazowej.
        /// </summary>
        /// 
        /// <param name="file">Nazwa wczytywanego do pamięci pliku.</param>
        /// <param name="encoding">Kodowanie znaków w pliku.</param>
        /// <param name="separator">Separator kolumn w pliku, domyślnie średnik.</param>
        /// <param name="autocheck">Automatyczne sprawdzanie typów danych.</param>
        /// 
        /// <seealso cref="parse"/>
		//* ============================================================================================================
        public IOFileData( string file, Encoding encoding, int separator = ';' )
            : base()
        {
			// ustaw separator, automatyczne sprawdzanie typów i kodowanie
			this._separator  = separator;
            this._enclosing  = (int)'"';
            this._encoding   = encoding;
            this._lineEnding = LINEENDING.Undefined;
            
            // ustaw plik
            this.FileName = file;
        }
        
		/// <summary>
		/// Separator kolumn w pliku bazy danych.
		/// Jest to ważny element używany podczas dzielenia wierszy na części w pliku z danymi.
		/// Aby dobrać odpowiedni separator, należy najpierw zaznajomić się ze strukturą pliku.
		/// </summary>
		//* ============================================================================================================
        public int Separator
        {
            get { return this._separator; }
            set { this._separator = value; }
        }
        
		/// <summary>
		/// Znak zakrywający, umieszczony na początku i na końcu wartości pola.
        /// W zdecydowanej większości przypadków znakiem tym jest cudzysłów lub apostrof.
		/// Aby dobrać odpowiedni znak zakrywający, należy najpierw zaznajomić się ze strukturą pliku.
        /// Nie jest wymagane, aby komórka posiadała znak zakrywający gdy nie zawiera znaków specjalnych.
		/// </summary>
        /// 
        /// <seealso cref="parse"/>
        /// <seealso cref="_isQuoted"/>
		//* ============================================================================================================
        public int EnclosingChar
        {
            get { return this._enclosing; }
            set { this._enclosing = value; }
        }
        
		/// <summary>
		/// Kodowanie znaków użyte w pliku.
		/// Plik jest odczytywany w kodowaniu podanym do tej właściwości.
		/// Błędnie zklasyfikowane dla pliku kodowanie może zabużyć całą strukturę danych.
		/// </summary>
		//* ============================================================================================================
        public Encoding Encoding
        {
            get { return this._encoding; }
            set { this._encoding = value; }
        }
        
		/// <summary>
		/// Rozszerzenie przypisanego do klasy pliku.
        /// Przed przypisaniem nowej nazwy pliku do klasy sprawdzane jest jego rozszerzenie.
        /// Dzięki temu klasa wie, której funkcji użyć do parsowania jego zawartości.
        /// Właściwość tylko do odczytu.
		/// </summary>
        /// 
        /// <seealso cref="Extension"/>
        /// <seealso cref="parse"/>
		//* ============================================================================================================
        public string Extension
        {
            get { return this._extension; }
        }
        
		/// <summary>
		/// Nazwa wczytywanego pliku.
        /// Plik jest odczytywany dopiero po wywołaniu funkcji <see cref="parse"/>.
        /// Podczas przypisywania pliku funkcja sprawdza czy podany plik istnieje na dysku.
        /// Z pliku pobierane jest rozszerzenie i na jego podstawie dopasowywana jest funkcja parsująca dane.
        /// Gdy plik nie istnieje, stan gotowości klasy jest zawieszany.
		/// </summary>
        /// 
        /// <seealso cref="Extension"/>
        /// <seealso cref="Ready"/>
        /// <seealso cref="parse"/>
		//* ============================================================================================================
        public string FileName
        {
            get { return this._fileName; }
            set
            {
			    // sprawdź czy plik istnieje
			    try
			    {
				    using( var fs = File.Open(value, FileMode.Open, FileAccess.Read) )
					    fs.Close();
				    this._isReady = true;
			    }
			    catch( IOException ex )
			    {
				    Program.LogError( ex.Message, Language.GetLine("MessageNames", (int)MSGBLIDX.FileLoading), false );
				    this._isReady = false;
			    }

			    // pobierz rozszerzenie
			    this._extension = new FileInfo(value).Extension.ToLower();
			    this._fileName  = value;
            }
        }
        
		/// <summary>
        /// Wykryty w pliku znak nowej linii.
        /// Znak nowej linii wykrywany jest podczas początkowej analizy pliku.
        /// Raz wykryty pozostaje niezmienny podczas dowolnego działania na pliku.
        /// Właściwość tylko do odczytu.
		/// </summary>
        /// 
        /// <seealso cref="parse"/>
        /// <seealso cref="_isEOL"/>
		//* ============================================================================================================
        public LINEENDING LineEnding
        {
            get { return this._lineEnding; }
        }
        
		/// <summary>
        /// Lista rozszerzeń plików rozpoznawanych przez klasę.
        /// Klasa umożliwia odczyt danych z plików tylko o rozszerzeniach zwróconych prez tą właściwość.
        /// Właściwość tylko do odczytu.
		/// </summary>
        /// 
        /// <seealso cref="parse"/>
        /// <seealso cref="getExtensionsFilter"/>
		//* ============================================================================================================
        public static string[] Extensions
        {
            get { return IOFileData._parsers; }
        }
#endregion

#region PODSTAWOWE FUNKCJE
        /// <summary>
        /// Wczytuje dane z pliku do pamięci.
        /// Funkcja po rozszerzeniu wywołuje odpowiedni parser dla pliku.
        /// Dzięki temu proces wczytywania danych jest zautomatyzowany.
        /// Funkcja pozwala na pobranie tylko nazw kolumn lub tylko kilku wierszy spośród wszystkich.
        /// </summary>
        /// 
        /// <seealso cref="parseCSV"/>
        /// 
        /// <param name="records">Maksymalna ilość pobieranych wierszy, -1 aby pobrać wszystkie.</param>
        /// <param name="hascolumns">Czy plik posiada wiersz zawierający definicję kolumn?</param>
		//* ============================================================================================================
        public void parse( int records, bool hascolumns = true )
        {
            // brak gotowości do parsowania pliku
            if( !this._isReady )
                return;

            // no tak być nie może...
            if( this._separator == this._enclosing )
                return;

            // sprawdź rozszerzenie
            if( this._extension == ".csv" )
                this.parseCSV( records, hascolumns );

            GC.Collect();
        }

        /// <summary>
        /// Zapis danych z pamięci do pliku.
        /// Funkcja po rozszerzeniu wywołuje odpowiednią funkcję zapisywania danych.
        /// Dzięki temu proces zapisu danych jest po części zautomatyzowany.
        /// </summary>
        /// 
        /// <param name="filename">Nazwa pliku do zapisania danych.</param>
		//* ============================================================================================================
        public void save( string filename )
        {
            // brak gotowości zapisywania
            if( !this._isReady )
                return;

            // informacje o pliku
            FileInfo finfo = new FileInfo( filename );

            // sprawdź rozszerzenie
            if( finfo.Extension.ToLower() == ".csv" )
                this.saveCSV( filename );

            GC.Collect();
        }

        /// <summary>
		/// Zwraca listę obsługiwanych rozszerzeń oddzieloną przecinkami.
		/// Funkcja potrafi również utworzyć listę dla filtra wyboru plików z dostępnych rozszerzeń i tłumaczeń.
		/// </summary>
		/// 
		/// <param name="filter">Tworzenie filtra dla okna wyboru pliku.</param>
		/// 
		/// <returns>Rozszerzenia oddzielone przecinkami lub filtr wyboru plików.</returns>
		//* ============================================================================================================
		public static string getExtensionsList( bool filter = false )
		{
			string extensions = "";
			
			// normalna lista rozszerzeń (oddzielona przecinkiem)
			if( !filter )
				for( int x = 0, y = IOFileData._parsers.Count( ); x < y; ++x )
					if( x == 0 )
						extensions += IOFileData._parsers[x];
					else
						extensions += ", " + IOFileData._parsers[x];
			// filtrowana lista rozszerzeń (oddzielona |) - dla OpenFileDialog
			else
			{
				List<string> values = Language.GetLines( "Extensions" );

				for( int x = 0, y = IOFileData._parsers.Count(); x < y; ++x )
					if( x == 0 )
					{
						extensions += values[x];
						extensions += "|*." + IOFileData._parsers[x];
					}
					else
					{
						extensions += "|" + values[x];
						extensions += "|*." + IOFileData._parsers[x];
					}
			}

			return extensions;
		}
#endregion

#region PARSER CSV
        /// <summary>
        /// Wczytuje dane z pliku CSV do pamięci.
        /// Aby zobaczyć czy dane zostały wczytane poprawnie należy sprawdzić właściwość <see cref="Ready"/>.
        /// Funkcja może wczytać same kolumny lub plik nie zawierający definicji kolumn.
        ///
        /// Główna implementacja formatu CSV (comma separated values) zakłada że:
        /// <list type="bullet">
        /// <item>Każdy wiersz z założenia kończony jest znakiem (CR+LF).</item>
        /// <item>Ostatni wiersz może, ale nie musi mieć znaku końca linii.</item>
        /// <item>Plik może zawierać nazwy kolumn w pierwszym wierszu.</item>
        /// <item>W każdym wierszu może być kilka kolumn oddzielonych separatorem (przecinkiem).</item>
        /// <item>Każdy kolejny wiersz musi zawierać tyle samo kolumn co pierwsza.</item>
        /// <item>Pojedyncza komórka może być ujęta w cudzysłowiach.</item>
        /// <item>Aby umieścić znak specjalny w polu, należy pole objąć cudzysłowiem.</item>
        /// <item>Aby wypisać cudzysłów w cudzysłowiu, należy go poprzedzić znakiem cudzysłowia.</item>
        /// </list>
        /// 
        /// Choć istnieje wiele implementacji tego formatu, główne elementy są jednak niezmienne.
        /// Aby pozwolić wczytać więcej plików z tym formatem, dodane zostały następujące możliwości:
        /// <list type="bullet">
        /// <item>Każdy wiersz kończy się takim znakiem jaki został wykryty na początku (CR, LF, CR+LF).</item>
        /// <item>Domyślny separator kolumn (tutaj średnik) można zmienić na inny.</item>
        /// <item>Komórka nie musi być ujmowana w apostrofy, może to być dowolna para znaków.</item>
        /// </list>
        /// 
        /// Zachowania podczas dziwnych sytuacji:
        /// <list type="number">
        /// <item>Cudzysłów jest wpisany do pola które nie jest ujęte w cudzysłowie.</item><br />
        ///     <description><i>Cudzysłów traktowany jest jako normalny znak pola.</i></description>
        /// <item>Wiesz zawiera za dużą liczbę kolumn.</item><br />
        ///     <description><i>Kolumny wykraczające poza wyktytą ilość dołączane są do ostatniej
        ///                     kolumny.</i></description>
        /// <item>Wiersz zabiera zbyt małą ilość kolumn.</item><br />
        ///     <description><i>Kolumny które nie istnieją uzupełniane są pustymi wartościami.</i></description>
        /// <item>Po cudzysłowie zamykającym wartość znajdują się znaki inne niż separator kolumny.</item><br />
        ///     <description><i>Wszystkie znaki po cudzysłowiu do napotkania separatora kolumny lub znaku końca linii
        ///                     są pomijane.</i></description>
        /// </list>
        /// </summary>
        /// 
        /// <seealso cref="Separator" />
        /// <seealso cref="EnclosingChar" />
        /// <seealso cref="Encoding" />
        /// <seealso cref="LineEnding" />
        /// <seealso cref="Ready" />
        /// <seealso cref="parse" />
        /// 
        /// <param name="records">Maksymalna ilość pobieranych wierszy, -1 aby pobrać wszystkie.</param>
        /// <param name="hascolumns">Czy plik posiada wiersz zawierający definicję kolumn?</param>
		//* ============================================================================================================
        protected void parseCSV( int records, bool hascolumns )
        {
            if( !this._isReady || this._separator == this._enclosing )
                return;

            this._isReady    = false;
            this._lineEnding = LINEENDING.Undefined;

            var file   = new StreamReader( this._fileName, this._encoding, true );
            int chr    = -1,
                column = 0,
                row    = 0;

            string cell   = "";
            bool   inside = false,
                   begin  = true;

            // analizuj plik (ilość kolumn i ilość wierszy, znaki końca linii)
            this._analyzeORIOLFile( file, records );

            this._columns = new string[this._columnsNumber];
            this._rows    = records == 0 ? null : new List<string[]>( this._rowsNumber );
            
            // pobierz nazwy kolumn jeżeli plik je posiada
            if( hascolumns )
            {
                while( (chr = file.Read()) != -1 )
                {
                    // znaki otaczające
                    if( this._isQuoted(chr, file, inside) )
                        if( inside || begin )
                        {
                            inside = begin;
                            begin  = true;
                            continue;
                        }
                    begin = false;

                    if( inside )
                    {
                        cell += (char)chr;
                        continue;
                    }
                    
                    // koniec linii
                    if( this._isEOL(chr, file) )
                    {
						this._columns[column] = cell;
                        break;
                    }
                    // separator kolumn
                    else if( chr == this._separator )
                    {
					    this._columns[column] = cell;
					    cell  = "";
                        begin = true;
					    
                        ++column;
                        continue;
                    }
                    cell += (char)chr;
                }
            }
            // uzupełnij nazwy kolumn (plik ich nie posiada)
            else
                for( int x = 0; x < this._columnsNumber; ++x )
                    this._columns[x] = "Kolumna #" + (x + 1); // #LANG

            // nie przechodź dalej, pobierano tylko kolumny
            if( records == 0 )
            {
                this._isReady = true;
                return;
            }

            // dodaj tablicę do listy
            this._rows.Add( new string[this._columnsNumber] );

            // po co potem liczyć cały czas...
            int limitm1 = this._columnsNumber - 1;

            column = 0;
            row    = 0;
            inside = false;
            begin  = true;
            cell   = "";

            // pobieraj wiersze
            while( (chr = file.Read()) != -1 )
            {
                // znaki otaczające
                if( this._isQuoted(chr, file, inside) )
                    if( inside || begin )
                    {
                        inside = begin;
                        begin  = true;
                        continue;
                    }
                begin = false;

                if( inside )
                {
                    cell += (char)chr;
                    continue;
                }

                // koniec linii
                if( this._isEOL(chr, file) )
                {
                    this._rows[row][column] = cell;
                    cell  = "";
                    begin = true;

                    // gdy ilość kolumn nie za bardzo się zgadza
                    if( column < this._columnsNumber - 1 )
                        for( int x = 0; x < this._columnsNumber; ++x )
                            this._rows[row][x] = "";

                    ++row;
                    column = 0;

                    // ograniczenie pobierania wierszy
                    if( row >= this._rowsNumber )
                        break;

                    this._rows.Add( new string[this._columnsNumber] );
                    continue;
                }
                // separator - nowa kolumna
                else if( chr == this._separator )
                {
                    // gdy ilość kolumn nie za bardzo się zgadza
                    if( column >= limitm1 )
                    {
                        cell  += (char)chr;
                        column = limitm1;
                    }
                    else
                    {
                        this._rows[row][column] = cell;
                        cell  = "";
                        begin = true;
                        
                        ++column;
                    }
                    continue;
                }
                cell += (char)chr;
            }

            // ostatnia wartość gdy koniec pliku nie jest poprzedzony znakiem nowej linii
            if( cell != "" && row < this._rowsNumber && column < this._columnsNumber )
            {
                this._rows[row][column] = cell;
                
                // gdy ilość kolumn nie za bardzo się zgadza
                if( column < this._columnsNumber - 1 )
                    for( int x = 0; x < this._columnsNumber; ++x )
                        this._rows[row][x] = "";
            }

            file.Close();
            this._isReady = true;
        }

        /// <summary>
        /// Zapis danych do pliku o rozszerzeniu CSV (comma separated values).
        /// Zapisane dane są zgodne z formatem CSV opisanym w dokumencie RFC 4180.
        /// Kolumny rozdzielane są znakiem przecinku, a znakiem otaczającym ktorkę jest znak apostrofu.
        /// Każdy wiersz kończony jest znakiem nowej linii, zapisywanym jako zlepek dwóch znaków: CRLF.
        /// </summary>
        /// 
        /// <param name="filename">Nazwa pliku wyjściowego do którego będą zapisywane dane.</param>
		//* ============================================================================================================
        protected void saveCSV( string filename )
        {
            var separator  = ',';
            var enclosing  = '"';
            var writer     = new StreamWriter( filename );
            var hasspecial = false;
            var savestr    = "";

            // zapisz kolumny
            for( int x = 0; x < this.ColumnsNumber; ++x )
            {
                hasspecial = false;
                savestr    = this.Column[x];
                
                // sprawdź czy ciąg zawiera znak otaczający krotkę
                if( savestr.IndexOf(enclosing) > -1 )
                {
                    savestr.Replace( enclosing.ToString(), "\\" + enclosing );
                    hasspecial = true;
                }
                // sprawdź czy ciąg zawiera separator
                if( savestr.IndexOf(separator) > -1 )
                    hasspecial = true;

                // otocz znakami jeżeli ciąg zawiera znaki specjalne
                if( hasspecial )
                    savestr = enclosing + savestr + enclosing;

                // zapisz do strumienia
                if( x > 0 )
                    writer.Write( separator + savestr );
                else
                    writer.Write( savestr );
            }
            
            // zapisz wiersze
            for( int x = 0; x < this.RowsNumber; ++x )
            {
                // nowa linia
                writer.Write( "\r\n" );

                // zapisz aktualną krotkę
                for( int y = 0; y < this.ColumnsNumber; ++y )
                {
                    hasspecial = false;
                    savestr    = this.Row[x][y];

                    // sprawdź czy ciąg zawiera znak otaczający krotkę
                    if( savestr.IndexOf(enclosing) > -1 )
                    {
                        savestr.Replace( enclosing.ToString(), "\\" + enclosing );
                        hasspecial = true;
                    }
                    // sprawdź czy ciąg zawiera separator
                    if( savestr.IndexOf(separator) > -1 )
                        hasspecial = true;

                    // otocz znakami ciąg jeżeli zawiera znak specjalny
                    if( hasspecial )
                        savestr = enclosing + savestr + enclosing;

                    // zapisz do strumienia
                    if( y > 0 )
                        writer.Write( separator + savestr );
                    else
                        writer.Write( savestr );
                }
            }
            // zamknij strumień
            writer.Close();
        }

#endregion

#region FUNKCJE WSPÓLNE DLA PARSERÓW
        /// <summary>
        /// Analizuje plik pod kątem ilości kolumn i wierszy.
        /// ORIO (One Row In One Line) bazuje jak sama nazwa wskazuje na założeniu, że każdy wiersz znajduje się
        /// w osobnej linii, a kolumny oddzielone są separatorami.
        /// W przypadku gdy w pliku znajduje się więcej wierszy niż podana maksymalna ilość do zliczenia, to
        /// automatycznie na true ustawia się flaga nadmiarowości danych.
        /// </summary>
        /// 
        /// <seealso cref="parseCSV" />
        /// <seealso cref="Separator" />
        /// <seealso cref="Overflow" />
        /// <seealso cref="LineEnding" />
        /// 
        /// <param name="stream">Strumień danych do pliku przeznaczonego do analizy.</param>
        /// <param name="rowlimit">Maksymalna ilość wierszy do zliczenia, -1 zlicza wszystkie.</param>
		//* ============================================================================================================
        private void _analyzeORIOLFile( StreamReader stream, int rowlimit )
        {
            int  chr     = -1,
                 columns = 1,
                 rows    = 0;
            bool inside  = false,
                 begin   = true;

            this._overflow = false;

            // policz ilość kolumn
            while( (chr = stream.Read()) != -1 )
            {
                // znaki otaczające
                if( this._isQuoted(chr, stream, inside) )
                    inside = begin;
                begin = false;

                if( inside )
                    continue;
                
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
                {
					++columns;
                    begin = true;
                }
            }

            inside = false;
            begin  = true;

            // zlicz wiersze w pliku
			if( rowlimit != 0 )
				while( (chr = stream.Read()) != -1 )
                {
                    // znaki otaczające
                    if( this._isQuoted(chr, stream, inside) )
                        inside = begin;
                    begin = false;

                    if( inside )
                        continue;

                    if( this._isEOL(chr, stream) )
                    {
                        ++rows;
                        begin = true;

                        // ograniczenie pobieranych wierszy
                        if( rowlimit > 0 && rows > rowlimit )
                        {
                            this._overflow = true;
                            rows = rowlimit;
                            break;
                        }
                    }
                    else if( chr == this._separator )
                        begin = true;
                }

            // zapisz ilość kolumn i wierszy
            this._columnsNumber = columns;
            this._rowsNumber    = rows;
            
            // przewiń na początek
            stream.BaseStream.Seek( 0, SeekOrigin.Begin );
            stream.DiscardBufferedData();
        }

        /// <summary>
        /// Sprawdza czy znak jest znakiem nowej linii.
        /// Działa na znakach LF / CR / CR+LF, funkcja wewnętrzna, nie ma zastosowania na zewnątrz.
        /// Podczas przetwarzania znaku CR+LF, gdy znak LF nie zostanie znaleziony po znaku CR, to
        /// CR nie zostanie pominięty.
        /// </summary>
        /// 
        /// <seealso cref="LineEnding"/>
        /// 
        /// <param name="chr">Aktualny znak pobrany ze strumienia.</param>
        /// <param name="stream">Strumień danych z którego zostanie pobrany kolejny znak (CR+LF).</param>
        /// <param name="read">Konsumuj pierwszy znak w kombinacji CR+LF.</param>
        /// 
        /// <returns>Czy został znaleziony znak nowej linii?</returns>
		//* ============================================================================================================
        private bool _isEOL( int chr, StreamReader stream, bool read = true )
        {
			// szukaj znaku końca linii (LF / CR / CR+LF)
            if( chr == '\r' || chr == '\n' )
            {
                // LF
                if( chr == '\n' )
                    return true;
                else if( chr == '\r' )
                    // CR
                    if( this._lineEnding == LINEENDING.Macintosh )
                        return true;
                    // CR+LF
                    else if( this._lineEnding == LINEENDING.Windows )
                    {
                        chr = stream.Peek();

                        if( chr == -1 || chr != '\n' )
                            return false;
                        
                        if( read )
                            stream.Read();

                        return true;
                    }
            }
            return false;
        }

        /// <summary>
        /// Sprawdza czy znak jest znakiem okrywającym pole.
        /// Domyślnie znakiem okrywającym jest cudzysłów, ale można go zmienić.
        /// W przypadku gdy po znaku okrywającym kończącym pole nie ma separatora lub znaku
        /// nowej linii, to wszystkie znaki po wystąpieniu znaku okrywającego są pomijane.
        /// Użycie podwójnego znaku okrywającego, np. podwójnego cudzysłowia, powoduje zwrócenie
        /// informacji o braku jego wystąpienia i skonsumowaniu pierwszego znaku ze strumienia.
        /// </summary>
        /// 
        /// <seealso cref="parseCSV" />
        /// <seealso cref="EnclosingChar" />
        /// <seealso cref="LineEnding" />
        /// 
        /// <param name="chr">Aktualny znak pobrany ze strumienia.</param>
        /// <param name="stream">Strumień danych na którym funkcja może operować.</param>
        /// <param name="inside">Czy jesteśmy w środku pola, czy wykryty już został znak okrywający?</param>
        /// 
        /// <returns>Zwraca true gdy przekazany znak jest znakiem okrywającym lub false gdy nie jest.</returns>
		//* ============================================================================================================
        private bool _isQuoted( int chr, StreamReader stream, bool inside )
        {
            if( chr == this._enclosing )
            {
                if( inside )
                {
                    chr = stream.Peek();

                    // podwójny znak otaczający w środku oznacza znak pojedynczy
                    if( chr == this._enclosing )
                    {
                        stream.Read();
                        return false;
                    }
                    
                    // znak końca pola, szukaj separatora i pomijaj znaki pomiędzy
                    while( chr != -1 && chr != this._separator )
                    {
                        stream.Read();

                        // znak nowej linii
                        if( this._isEOL(chr, stream, false) )
                            break;

                        chr = stream.Peek();
                    }
                }
                return true;
            }
            return false;
        }
#endregion
    }
}
