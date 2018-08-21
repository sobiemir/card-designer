///
/// $u08 Language.cs
/// 
/// Wczytywanie tłumaczeń dla danego języka.
/// Wyszukiwanie odbywa się po podaniu sekcji, podsekcji i indeksu.
/// Więcej informacji w opisach funkcji.
/// 
/// Autor: Kamil Biały
/// Od wersji: 0.8.x.x
/// Ostatnia zmiana: 2016-12-22
/// 
/// CHANGELOG:
/// [14.09.2015] Pierwsza wersja klasy.
/// [01.12.2015] Dodano zamianę znaków \n na nową linię.
/// [06.12.2015] Dodano zamianę znaków \$ na znak \
/// [03.12.2016] Dodano parser dla nazw języka w różnych językach, rozpoczynający się od znaku $.
/// [22.12.2016] Pobieranie języka z ustawień.
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
	/// Klasa wczytująca pliki językowe.
	/// Klasyfikuje linie z tłumaczeniami na sekcje i podsekcje.
    /// Każdą sekcję i podsekcję można pobrać używając odpowiednich funkcji.
    /// Indeksy tłumaczeń w podsekcjach liczone są standardowo od zera.
    /// Klasa jest statyczna, ponieważ dostępny jest tylko jeden język w trakcie działania programu.
    /// Języki wczytywane są z folderu languages umieszczonego w folderze programu.
	/// </summary>
	/// 
	/// <example>
	/// Szybki przykład użycia klasy:
	/// <code>
	/// Language.Initialize();
    /// Language.Parse("pl");
    /// 
    /// // wypisuje wartość z sekcji main i podsekcji sub o indeksie 1
    /// Console.WriteLine( Language.GetLine("foo", "bar", 1) );
    /// 
    /// Language.Initialize();
    /// Language.Parse("en");
    /// 
    /// // wypisuje wartość z sekcji głównej i podsekcji foobar o indeksie 0
    /// Console.WriteLine( Language.GetLine("foobar", 1) );
	/// </code>
	/// </example>
	/// 
	class Language
    {
#region ZMIENNE

		/// <summary>Linie z tłumaczeniami zawierające sekcje i podsekcje.</summary>
		/// @hideinitializer
		private static Dictionary<string, Dictionary<string, List<string>>> _line = null;

		/// <summary>Flaga inicjalizacji klasy.</summary>
		/// @hideinitializer
		private static bool _init = false;

        /// <summary>Kod aktualnie załadowanego języka.</summary>
		/// @hideinitializer
        private static string _code = "";

#endregion

#region POBIERANIE DANYCH

        /// <summary>
        /// Pobiera linie z tłumaczeniami dla danej podsekcji.
        /// Podsekcje pobierane są z sekcji głównej (nie ustawianej w pliku).
        /// </summary>
        /// 
        /// <param name="subsect">Nazwa podsekcji.</param>
        /// 
        /// <returns>Lista tłumaczeń z danej podsekcji.</returns>
        /// 
        /// <seealso cref="Initialize"/>
        /// <seealso cref="Parse"/>
		//* ============================================================================================================
		public static List<string> GetLines( string subsect )
		{
			return Language._line[" "][subsect];
		}

        /// <summary>
		/// Pobiera linie z tłumaczeniami dla danej podsekcji znajdującej się w wybranej sekcji.
        /// </summary>
        /// <param name="section">Nazwa sekcji.</param>
        /// <param name="subsect">Nazwa podsekcji.</param>
        /// 
        /// <returns>Lista tłumaczeń z podanej sekcji i podsekcji.</returns>
        /// 
        /// <seealso cref="Initialize"/>
        /// <seealso cref="Parse"/>
		//* ============================================================================================================
		public static List<string> GetLines( string section, string subsect )
		{
			return Language._line[section][subsect];
		}

        /// <summary>
        /// Pobiera pojedynczą linię o podanym indeksie dla podanej podsekcji.
        /// Podsekcje pobierane są z sekcji głównej.
        /// </summary>
        /// 
        /// <param name="subsect">Nazwa podsekcji.</param>
        /// <param name="index">Indeks linii z tłumaczeniem znajdującym się w podsekcji.</param>
        /// 
        /// <returns>Tłumaczenie z podanej linii.</returns>
        /// 
        /// <seealso cref="Initialize"/>
        /// <seealso cref="Parse"/>
		//* ============================================================================================================
		public static string GetLine( string subsect, int index )
		{
			return Language._line[" "][subsect][index];
		}

        /// <summary>
        /// Pobiera pojedynczą linię o podanym indeksie dla danej podsekcji znajdującej się z wybranej sekcji.
        /// </summary>
        /// 
        /// <param name="section">Nazwa sekcji.</param>
        /// <param name="subsect">Nazwa podsekcji.</param>
        /// <param name="index">Indeks linii tłumaczenia pobieranego z podsekcji.</param>
        /// 
        /// <returns>Tłumaczenie z podanej linii.</returns>
        /// 
        /// <seealso cref="Initialize"/>
        /// <seealso cref="Parse"/>
		//* ============================================================================================================
		public static string GetLine( string section, string subsect, int index )
		{
			return Language._line[section][subsect][index];
		}

        /// <summary>
        /// Pobiera kod aktualnie załadowanego języka.
        /// </summary>
        /// 
        /// <returns>Kod załadowanego języka.</returns>
        /// 
        /// <seealso cref="Initialize"/>
        /// <seealso cref="Parse"/>
		//* ============================================================================================================
        public static string GetCode()
        {
            return Language._code;
        }

#endregion

#region PARSOWANIE PLIKU

        /// <summary>
		/// Inicjalizacja klasy do pobierania tłumaczeń.
		/// W przypadku wcześniejszej inicjalizacji usuwa poprzednie sekcje, podsekcje i linie.
        /// </summary>
        /// 
        /// <seealso cref="Parse"/>
		//* ============================================================================================================
		public static void Initialize()
		{
			// utwórz nowy słownik
			if( Language._line == null )
				Language._line = new Dictionary<string,Dictionary<string,List<string>>>();
			// wyczyść dane ze słownika
			else
			{
				foreach( Dictionary<string, List<string>> line in Language._line.Values )
				{
					foreach( List<string> text in line.Values )
						text.Clear();
					line.Clear();
				}
				Language._line.Clear();
			}

			// sekcja globalna (spacja - nie jest dozwolona w pliku)
			Language._line.Add( " ", new Dictionary<string,List<string>>() );

			// przełącznik inicjalizacji
			Language._init = true;
		}

        /// <summary>
        /// Parser pliku z tłumaczeniami.
        /// Wczytuje sekcje, podsekcje i linie z pliku podanego w parametrze.
        /// Obsługuje tylko pliki o rozszerzeniu LEX.
        /// Pliki wczytywane są z folderu languages umieszonym w folderze głównym programu.
        /// </summary>
        /// 
        /// <param name="lang">Nazwa języka do wczytania.</param>
        /// 
        /// <seealso cref="Initialize"/>
        /// <seealso cref="GetLines"/>
        /// <seealso cref="GetLine"/>
        /// <seealso cref="IsEOL"/>
		//* ============================================================================================================
		public static void Parse( string lang = null )
		{
#		if DEBUG
			Program.LogMessage( "Przetwarzanie pliku językowego." );
#		endif

			// domyślny język lub język pobrany z ustawień
			if( lang == null && Settings.Info.Language != null )
				lang = Settings.Info.Language;
            else if( lang == null )
                lang = "pol";

			// nie wywołano funkcji inicjalizacji
			if( !Language._init )
				throw new Exception( "Klasa językowa nie została przygotowana do działania." );

			// otwórz plik do odczytu
			StreamReader file = new StreamReader( "./languages/" + lang + ".lex" );

			string str  = "";
			int    line = 1,  type = 0,
				   chr;
			
			Dictionary<string, List<string>> section = Language._line[" "];
			List<string> subsect = null;

            // pobierz dane nagłówkowe
            Language.GetLanguageHeader( file );

			// przetwarzaj treść
			try { while( (chr = file.Read()) != -1 )
			{
				switch( type )
				{
				// brak typu
				case 0:
					switch( chr )
					{
					// sekcja
					case '#': type = 1; break;
					// podsekcja
					case '@': type = 2; break;
					// komentarz
					case '%': type = 3; break;
					// informacja o linii - rozpoczęcie pobierania tekstu
					case '|': type = 4; break;
					// inny znak?
					default:
						// koniec linii
						if( Language.IsEOL(chr, file) )
						{
							line++;
							continue;
						}

						// biały znak
						if( char.IsWhiteSpace((char)chr) )
							continue;
						
						// nieprawidłowy znak...
						throw new Exception( "Unexpected character '" + (char)chr + "'" + " in line " + line + "." );
					}
				break;
				// typ 1 - sekcja
				case 1:
					if( !(chr >= '0' && chr <= '9') && !(chr >= 'A' && chr <= 'Z') &&
						!(chr >= 'a' && chr <= 'z') && !(chr == '_' || chr == '-') )
					{
						// koniec linii
						if( Language.IsEOL(chr, file) )
						{
							// odwołanie do sekcji głównej
							if( str == "" )
								str = " ";
							
							// dodaj klucz jeżeli nie istnieje
							if( !Language._line.ContainsKey(str) )
								Language._line.Add( str, new Dictionary<string,List<string>>() );

							// zapisz aktualną sekcję
							section = Language._line[str];

							type = 0;
							str  = "";
							line++;

							continue;
						}

						// nieprawidłowy znak
						throw new Exception( "Not allowed characters for subsection name in line " + line +
							", allowed are [0-9A-Za-z_-]." );
					}

					// zapisz sekcję
					str += (char)chr;
				break;
				// typ 2 - podsekcja
				case 2:
					if( !(chr >= '0' && chr <= '9') && !(chr >= 'A' && chr <= 'Z') &&
						!(chr >= 'a' && chr <= 'z') && !(chr == '_' || chr == '-') )
					{
						// nazwa podsekcji nie może być pusta
						if( str == "" )
							throw new Exception( "Subsection name is empty in line " + line + "." );

						// dodaj klucz jeżeli nie istnieje
						if( !section.ContainsKey(str) )
							section.Add( str, new List<string>() );

						// zapisz aktualną subsekcję
						subsect = section[str];

						// koniec linii
						if( Language.IsEOL(chr, file) )
						{
							type = 0;
							str  = "";
							line++;

							continue;
						}

						// nieprawidłowy znak
						throw new Exception( "Not allowed characters for subsection name in line " + line +
							", allowed are [0-9A-Za-z_-]." );
					}

					// zapisz podsekcję
					str += (char)chr;
				break;
				// komentarz
				case 3:
					// pobieraj do końca linii
					if( chr != '\n' )
						continue;

					type = 0;
					line++;
				break;
				// informacja o linii
				case 4:
                    if( chr >= 'a' && chr <= 'z' )
                        

					// pomiń znaki
					if( chr >= '0' && chr <= '9' )
						continue;

					// koniec linii - brak tekstu w danym elemencie
					if( Language.IsEOL(chr, file) )
					{
						type = 0;
						line++;

						continue;
					}

					// pomiń pierwszy biały znak przed tekstem
					if( char.IsWhiteSpace((char)chr) )
					{
						chr  = file.Read();
						type = 5;
						goto case 5;
					}
				break;
				case 5:
					// koniec linii
					if( Language.IsEOL(chr, file) )
					{
						// brak podsekcji
						if( subsect == null )
							throw new Exception( "Subsection is not defined in line " + line + "." );

						// nowa linia
						str = str.Replace( "\\n", "\n" );
						str = str.Replace( "\\$", "\\" );
						
						// dodaj linię do podsekcji
						subsect.Add( str );

						type = 0;
						str  = "";
						line++;

						continue;
					}

					// zapisz pobierany tekst
					str += (char)chr;
				break;
				}
			} }
			// przechwyć wyjątek
			catch( Exception ex )
				{ throw new Exception( ex.Message, ex ); }
			finally
				{ file.Close(); }

            // zapisz kod języka
            Language._code = lang;

#		if DEBUG
			Program.LogMessage( "Wczytano plik językowy dla języka o kodzie: '" + lang + "'." );
#		endif
		}

        /// <summary>
        /// Pobiera nazwy języka o podanym kodzie.
        /// Jest to lista klucz => wartość, gdzie klucz jest kodem języka, a wartość to przetłumaczona
        /// nazwa języka w języku oznaczonym kodem.
        /// Tłumaczenia języka są oznaczone wstępnie znakiem $, po którym podawany jest skrót języka.
        /// </summary>
        /// 
        /// <param name="lang">Nazwa języka przeznaczonego do parsowania nagłówka.</param>
        /// 
        /// <returns>Lista tłumaczeń nazwy języka.</returns>
        /// 
        /// <seealso cref="Parse"/>
        /// <seealso cref="GetLanguageHeader"/>
		//* ============================================================================================================
        public static Dictionary<string, string> GetLanguageNames( string lang )
        {
			// otwórz plik do odczytu
			StreamReader file = new StreamReader( "./languages/" + lang + ".lex" );

#       if DEBUG
            Program.LogMessage( "Przetwarzanie języka o kodzie '" + lang + "'..." );
#       endif

            var langnames = Language.GetLanguageHeader( file );

            file.Close();
            file.Dispose();

            return langnames;
        }

        /// <summary>
        /// Część parsera pliku tłumaczeń.
        /// Pobiera dane nagłówkowe, czyli nazwę języka zdefiniowanego w pliku w kilku językach.
        /// Dane nagłówkowe mogą być, ale nie muszą.
        /// Gdy ich nie ma, jako nazwę języka przyjmuje się nazwę pliku.
        /// Język może posiadać wartość DEF, która traktowana jest jako wartość domyślna gdy brakuje
        /// tłumaczenia dla danego języka.
        /// </summary>
        /// 
        /// <param name="reader">Strumień pliku do zczytywania znaków.</param>
        /// 
        /// <returns>Lista nazw języka w różnych językach.</returns>
        /// 
        /// <seealso cref="Parse"/>
        /// <seealso cref="GetLanguageNames"/>
        /// <seealso cref="IsEOL"/>
		//* ============================================================================================================
        private static Dictionary<string, string> GetLanguageHeader( StreamReader reader )
        {
            var list = new Dictionary<string, string>();
            int chr, mode = 0;
            var lang = "";
            var str  = "";

            // sprawdzaj dane w pliku nagłówkowym
            try { while( (chr = reader.Peek()) != -1 )
            {
                // szukaj odpowiedniego znaku do zakończenia przetwarzania nagłówka
                if( mode == 0 && !char.IsWhiteSpace((char)chr) && chr != '$' )
                    break;
                
                // koniec linii - dodaj nazwę języka
                if( Language.IsEOL(chr, reader) )
                {
                    if( lang != "" && str != "" )
                        list.Add( lang, str );

                    str  = "";
                    lang = "";
                    mode = 0;
                }
                // tryb nazwy języka
                else if( mode == 0 && chr == '$' )
                    mode = 1;
                // pobieranie kodu języka
                else if( mode == 1 )
                {
                    if( char.IsWhiteSpace((char)chr) )
                        mode = 2;
                    else
                        lang += (char)chr;
                }
                // pobieranie nazwy języka
                else if( mode == 2 )
                    str += (char)chr;

                reader.Read();
            } }
            catch( Exception ex )
                { throw new Exception( ex.Message, ex ); }
            
#       if DEBUG
            Program.LogMessage( "Znaleziono " + list.Count + " tłumaczeń języka." );
#       endif

            return list;
        }

        /// <summary>
		/// Sprawdza czy aktualny znak jest znakiem nowej linii.
		/// Obsługuje znaki CRLF / LF / CR.
        /// </summary>
        /// 
        /// <param name="chr">Aktualny znak.</param>
        /// <param name="reader">Strumień pliku z którego znak został zczytany.</param>
        /// 
        /// <returns>Czy znak jest znakiem końca linii?</returns>
        /// 
        /// <seealso cref="Parse"/>
		//* ============================================================================================================
		private static bool IsEOL( int chr, StreamReader reader )
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

#endregion
    }
}
