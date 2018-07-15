using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using CDesigner.Utils;

///
/// $c03 Language.cs
/// 
/// Wczytywanie tłumaczeń dla danego języka.
/// Wyszukiwanie odbywa się po podaniu sekcji, podsekcji i indeksu.
/// Więcej informacji w opisach funkcji.
/// 
/// Autor: Kamil Biały
/// Od wersji: 0.8.x.x
/// Ostatnia zmiana: 2015-12-06
/// 
/// Dodano zamianę znaków \n na nową linię.
/// Dodano zamianę znaków \$ na znak \
///

namespace CDesigner
{
	/// 
	/// <summary>
	/// Klasa wczytująca pliki językowe.
	/// Klasyfikuje linie z tłumaczeniami na sekcje i podsekcje.
	/// </summary>
	/// 
	class Language
	{
		// ===== PRIVATE VARIABLES ==============================================================

		/// <summary>Linie z tłumaczeniami zawierające sekcje i podsekcje.</summary>
		private static Dictionary<string, Dictionary<string, List<string>>> _line = null;

		/// <summary>Flaga inicjalizacji klasy.</summary>
		private static bool _init = false;

		// ===== GETTERS / SETTERS ==============================================================

		/**
		 * <summary>
		 * Pobiera linie z tłumaczeniami dla danej podsekcji.
		 * Podsekcje pobierane są z sekcji głównej.
		 * </summary>
		 * 
		 * <param name="subsect">Nazwa podsekcji.</param>
		 * --------------------------------------------------------------------------------------------------------- **/
		public static List<string> GetLines( string subsect )
		{
			return Language._line[" "][subsect];
		}

		/**
		 * <summary>
		 * Pobiera linie z tłumaczeniami dla danej podsekcji znajdującej się w wybranej sekcji.
		 * </summary>
		 * 
		 * <param name="section">Nazwa sekcji.</param>
		 * <param name="subsect">Nazwa podsekcji.</param>
		 * --------------------------------------------------------------------------------------------------------- **/
		public static List<string> GetLines( string section, string subsect )
		{
			return Language._line[section][subsect];
		}

		/**
		 * <summary>
		 * Pobiera pojedynczą linię o podanym indeksie dla danej podsekcji.
		 * Podsekcje pobierane są z sekcji głównej.
		 * </summary>
		 * 
		 * <param name="subsect">Nazwa podsekcji.</param>
		 * <param name="index">Indeks linii.</param>
		 * --------------------------------------------------------------------------------------------------------- **/
		public static string GetLine( string subsect, int index )
		{
			return Language._line[" "][subsect][index];
		}

		/**
		 * <summary>
		 * Pobiera pojedynczą linię o podanym indeksie dla danej podsekcji znajdującej się w wybranej sekcji.
		 * </summary>
		 * 
		 * <param name="section">Nazwa sekcji.</param>
		 * <param name="subsect">Nazwa podsekcji.</param>
		 * <param name="index">Indeks linii.</param>
		 * --------------------------------------------------------------------------------------------------------- **/
		public static string GetLine( string section, string subsect, int index )
		{
			return Language._line[section][subsect][index];
		}

		// ===== PUBLIC FUNCTIONS ===============================================================

		/**
		 * <summary>
		 * Inicjalizacja klasy do pobierania tłumaczeń.
		 * W przypadku wcześniejszej inicjalizacji usuwa poprzednie sekcje, podsekcje i linie.
		 * </summary>
		 * --------------------------------------------------------------------------------------------------------- **/
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

		/**
		 * <summary>
		 * Parser pliku z tłumaczeniami.
		 * Wczytuje sekcje, podsekcje i linie z pliku podanego w parametrze.
		 * Obsługuje tylko pliki o rozszerzeniu LEX.
		 * </summary>
		 * 
		 * <param name="lang">Nazwa pliku z tłumaczeniami bez rozszerzenia.</param>
		 * --------------------------------------------------------------------------------------------------------- **/
		public static void Parse( string lang = null )
		{
#		if DEBUG
			Program.LogMessage( "Przetwarzanie pliku językowego." );
#		endif

			// domyślny język lub język pobrany z ustawień
			if( lang == null )
			//	lang = Settings.Info.Language;
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

#		if DEBUG
			Program.LogMessage( "Wczytano plik językowy dla języka o kodzie: '" + lang + "'." );
#		endif
		}

		// ===== PRIVATE FUNCTIONS ==============================================================

		/**
		 * <summary>
		 * Sprawdza czy aktualny znak jest znakiem nowej linii.
		 * Obsługuje znaki CRLF / LF / CR.
		 * </summary>
		 * 
		 * <param name="chr">Aktualny znak.</param>
		 * <param name="file">Otwarty strumień pliku.</param>
		 * --------------------------------------------------------------------------------------------------------- **/
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
	}
}
