///
/// $u09 Settings.cs
/// 
/// Plik zawiera klasę pozwalającą na zarządzanie ustawieniami.
/// Umożliwia operacje takie jak, odczyt pliku konfiguracyjnego i zapis.
/// Dodatkowo pozwala na operacje na liście ostatnio otwieranych wzorów, ponieważ w pewnym sensie
/// plik ten też jest częścią konfiguracji aplikacji.
/// Klasa sama wczytuje ustawienia do struktury - wie która wartość jest do którego elementu.
/// 
/// Autor: Kamil Biały
/// Od wersji: 0.8.x.x
/// Ostatnia zmiana: 2016-12-25
/// 
/// CHANGELOG:
/// [13.02.2016] Wyodrębnienie funkcji z formularza ustawień - pierwsza wersja.
/// [04.12.2016] Funkcje do zarządzania ostatnio otwieranymi wzorami.
/// [25.12.2016] Komentarze, regiony, uporządkowanie kodu.
///

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;

namespace CDesigner.Utils
{
	/// 
	/// <summary>
    /// Zarządzanie ustawieniami aplikacji.
    /// Klasa pozwala na wczytanie i zapis ustawień do pliku konfiguracyjnego.
    /// Nazwa pliku konfiguracyjnego to <em>cdset.nfo</em>.
    /// W przypadku zapisu, automatycznie przechodzi po całej strukturze <see cref="SettingsInfo"/>
    /// i zapisuje każdy element z osobna do pliku konfiguracyjnego.
    /// W przypadku odczytu sprawa ma się podobnie, z tą różnicą że do każdego elementu dane są wczytywane.
    /// Klasa posiada również funkcje do zarządzania ostatnio otwieranymi wzorami.
	/// </summary>
    /// 
	/// <example>
	/// Przykład użycia klasy:
	/// <code>
    /// // inicjalizacja ustawień
    /// Settings.Initialize();
    /// Console.WriteLine( "Język programu: " + Settings.Info.Language );
    /// 
    /// // pobierz ustawienia
    /// Settings.Parse();
    /// Console.WriteLine( "Język programu: " + Settings.Info.Language );
    /// 
    /// // pobierz ostatnio otwierane wzory
    /// Console.WriteLine( "\nLista ostatnio otwieranych wzorów:" );
    /// Settings.GetLastPatterns();
    /// foreach( var pattern in Settings.LastPatterns )
    ///     Console.WriteLine( pattern );
    /// 
    /// // dodawanie wzoru
    /// Console.WriteLine( "\nLista ostatnio otwieranych wzorów:" );
    /// Settings.AddToLastPatterns( "Testowy wzór" );
    /// foreach( var pattern in Settings.LastPatterns )
    ///     Console.WriteLine( pattern );
    ///     
    /// // usuwanie wzoru
    /// Console.WriteLine( "\nLista ostatnio otwieranych wzorów:" );
    /// Settings.RemoveFromLastPatterns( "Testowy wzór" );
    /// foreach( var pattern in Settings.LastPatterns )
    ///     Console.WriteLine( pattern );
    ///     
    /// // zapis ustawień
    /// Settings.Info.Language = "rus";
    /// Settings.SaveSettings();
	/// </code>
	/// </example>
	/// 
	public class Settings
    {
#region ZMIENNE
		
        /// <summary>Lista ostatnio otwieranych wzorów.</summary>
        private static List<string> _lastPatterns;

		/// <summary>Struktura z danymi ustawień.</summary>
		public static SettingsInfo Info;

#endregion

#region INICJALIZACJA / WŁAŚCIWOŚCI
        
        /// <summary>
        /// Inicjalizacja klasy do odczytu i zapisu ustawień.
        /// Tworzy strukturę zawierającą domyślne ustawienia i sprawdza czy plik z ustawieniami istnieje.
        /// W przypadku gdy plik konfiguracyjny aplikacji nie istnieje, tworzy go.
        /// </summary>
        /// 
        /// <seealso cref="SettingsInfo"/>
        /// <seealso cref="SaveSettings"/>
		//* ============================================================================================================
		public static void Initialize()
		{
#		if DEBUG
			Program.LogMessage( "Uzupełnianie struktury domyślnymi ustawieniami." );
#		endif

			// utwórz strukturę z domyślnymi wartościami
			Settings.Info = new SettingsInfo( true );

			// zapisz ustawienia do pliku jeżeli nie istnieje
			if( !File.Exists("./cdset.nfo") )
			{
#			if DEBUG
				Program.LogMessage( ">> Brak pliku z ustawieniami - tworzenie nowego." );
#			endif

				Settings.SaveSettings();
			}
		}

        /// <summary>
        /// Lista ostatio otwieranych wzorów.
        /// Właściwość pozwala na pobranie i ustawienie listy ostatnio otwieranych wzorów.
        /// W przypadku ustawiania kasowana jest lista ostatnio otwieranych wzorów.
        /// </summary>
        /// 
        /// <seealso cref="GetLastPatterns"/>
        /// <seealso cref="AddToLastPatterns"/>
        /// <seealso cref="RemoveFromLastPatterns"/>
		//* ============================================================================================================
        public static List<string> LastPatterns
		{
			// pobierz ostatnio otwierane wzory
			get { return Settings._lastPatterns; }
			// ustaw ostatnio otwierane wzory
			set
			{
				Settings._lastPatterns = value;
				
				// wyczyść zawartość pliku (po prostu utwórz nowy)
				File.Delete("last.lst");
				File.Create("last.lst");
			}
		}

#endregion

#region OSTATNIO OTWIERANE WZORY

        /// <summary>
        /// Wczytywanie ostatnio otwieranych wzorów.
        /// Funkcja pobiera z pliku ostatnio otwierane wzory i zapisuje je do zmiennej.
        /// Pobranie możliwe jest dzięki właściwości <see cref="LastPatterns"/>.
        /// </summary>
        /// 
        /// <seealso cref="LastPatterns"/>
        /// <seealso cref="AddToLastPatterns"/>
        /// <seealso cref="RemoveFromLastPatterns"/>
		//* ============================================================================================================
        public static void GetLastPatterns()
		{
			// utwórz plik gdy nie istnieje
			if( !File.Exists("last.lst") )
			{
				File.Create( "last.lst" );
				Settings._lastPatterns = new List<string>();
				return;
			}

			// wczytaj ostatnie projekty
			Settings._lastPatterns = new List<string>( File.ReadLines( "last.lst" ) );
		}

        /// <summary>
        /// Dodanie wzoru do listy ostatnio otwieranych.
        /// Funkcja sprawdza czy wzór istnieje już na liście, jeżeli tak, przesuwa go na sam początek.
        /// W przypadku gdy nie istnieje, a lista jest zapełniona, usuwa ostatni element i dodaje wzór na sam początek.
        /// Po wszystkim zapisuje całość do pliku.
        /// </summary>
        /// 
        /// <seealso cref="LastPatterns"/>
        /// <seealso cref="GetLastPatterns"/>
        /// <seealso cref="RemoveFromLastPatterns"/>
        /// 
        /// <param name="pattern">Nazwa wzoru do dodania.</param>
		//* ============================================================================================================
		public static void AddToLastPatterns( string pattern )
		{
            int max = (int)Settings.Info.c02_RecentMax;

			// usuń wzór jeżeli już taki istnieje
			if( Settings._lastPatterns.Contains(pattern) )
				Settings._lastPatterns.Remove( pattern );
			
			// maksymalna ilość wyświetlanych plików
			if( Settings._lastPatterns.Count > max && max > 0 )
				Settings._lastPatterns.RemoveAt( max - 1 );

			// dodaj wzór do listy
			Settings._lastPatterns.Insert( 0, pattern );

			// zapisz nowe ustawienie ostatnich wzorów
			File.WriteAllLines( "last.lst", Settings._lastPatterns.AsEnumerable() );
		}

        /// <summary>
        /// Usuwa wzór z listy ostatnio otwieranych wzorów.
        /// Funkcja usuwa wzór o podanej nazwie z listy, po czym zapisuje całość do pliku.
        /// </summary>
        /// 
        /// <seealso cref="LastPatterns"/>
        /// <seealso cref="GetLastPatterns"/>
        /// <seealso cref="AddToLastPatterns"/>
        /// 
        /// <param name="pattern">Wzór do usunięcia z listy.</param>
		//* ============================================================================================================
		public static void RemoveFromLastPatterns( string pattern )
		{
			if( Settings._lastPatterns.Remove(pattern) )
				File.WriteAllLines( "last.lst", Settings._lastPatterns.AsEnumerable() );
		}

#endregion

#region ZARZĄDZANIE USTAWIENIAMI
        
        /// <summary>
        /// Wczytywanie ustawień z pliku.
        /// Funkcja parsuje plik konfiguracyjny aplikacji i wczytuje ustawienia do zmiennej <see cref="Info"/>.
        /// Parsowanie ustawień powinno odbywać się przed użyciem zmiennej wymienionej powyżej.
        /// Schemat zapisywanych danych do pliku opisany jest w funkcji zapisującej konfigurację.
        /// W przypadku gdy plik zawiera błędne dane nagłówkowe, rzucany jest wyjątek.
        /// </summary>
        /// 
        /// <seealso cref="SaveSettings"/>
        /// <seealso cref="SettingsInfo"/>
		//* ============================================================================================================
		public static void Parse()
		{
			var file   = new FileStream( "./cdset.nfo", FileMode.Open );
			var reader = new BinaryReader( file, Encoding.UTF8 );

#		if DEBUG
			Program.LogMessage( "Przetwarzanie pliku z ustawieniami." );
#		endif

			byte[] s = reader.ReadBytes( 5 );

			// ciąg początkowy - CDSET
			if( s[0] != (byte)'C' || s[1] != (byte)'D' || s[2] != (byte)'S' || s[3] != (byte)'E' || s[4] != (byte)'T' )
				throw new Exception( "Settings file is corrupted. Please, delete it and run application again." );

			// wersja ustawień
			reader.ReadString();

			// ilość i długość ustawień
			int count = reader.ReadInt16();
			reader.ReadInt32();

			// opakuj strukturę
			var setnfo = (Object)Settings.Info;

			// załaduj ustawienia
			for( int x = 0; x < count; ++x )
			{
				// typ ustawienia
				var type = reader.ReadByte();

				if( type == 5 )
				    {}

				// nazwa pola
				var name = reader.ReadString();

				// pobierz pole ze struktury
				var info = Settings.Info.GetType().GetField( name );

				// zmień jego wartość jeżeli pole istnieje
				if( info != null )
                    switch( type )
				    {
				    case 1: info.SetValue( setnfo, reader.ReadString() ); break;
				    case 2: info.SetValue( setnfo, reader.ReadChar() );   break;
				    case 3: info.SetValue( setnfo, reader.ReadByte() );   break;
				    case 4: info.SetValue( setnfo, reader.ReadInt32() );  break;
				    case 5: info.SetValue( setnfo, reader.ReadDouble() ); break;
				    case 6: break;
				    }
                else
                {
                    // odczytaj dane bez zapisywania gdy pole nie istnieje
                    switch( type )
				    {
				    case 1: reader.ReadString(); break;
				    case 2: reader.ReadChar();   break;
				    case 3: reader.ReadByte();   break;
				    case 4: reader.ReadInt32();  break;
				    case 5: reader.ReadDouble(); break;
				    case 6: break;
				    }
                }
			}

			// zmień ustawienia w strukturze
			Settings.Info = (SettingsInfo)setnfo;

			// zamknij plik
			reader.Close();
			file.Close();

#		if DEBUG
			Program.LogMessage( "Wczytano ustawienia aplikacji." );
#		endif
		}

        /// <summary>
        /// Zapis ustawień do pliku.
        /// Aby funkcja mogła zapisać dane do pliku, należy wcześniej wywołać funkcję <see cref="Initialize"/> lub
        /// <see cref="Parse"/>, aby przynajmniej przykładowe dane mogły być załadowane.
        /// Funkcja zapisuje wszystkie dane ze zmiennej <see cref="Info"/> do pliku konfiguracyjnego, używając schematu
        /// podanego poniżej w tabeli:
        /// <table>
        ///     <tr><th>Bajty</th><th>Nazwa</th><th>Opis</th></tr>
        ///     <tr><td>5</td><td>IDENTITY</td><td>Identyfikator pliku, zawsze CDSET.</td></tr>
        ///     <tr><td>?</td><td>VERSION</td><td>Wersja programu, który zapisał ustawienia.</td></tr>
        ///     <tr><td>2</td><td>COUNT</td><td>Ilość zapisanych ustawień.</td></tr>
        ///     <tr><td>4</td><td>LENGTH</td><td>Rozmiar zapisanych ustawień po tym bloku.</td></tr>
        ///     <tr><th colspan="3">Pętla ustawień</th></tr>
        ///     <tr><td>1</td><td>STYPE</td><td>Typ zapisanego ustawienia.</td></tr>
        ///     <tr><td>2</td><td>SACOUNT</td><td>Ilość elementów w tablicy (tylko dla ARRAY).</td></tr>
        ///     <tr><td>1</td><td>SATYPE</td><td>Typy elementów w tablicy (tylko dla ARRAY).</td></tr>
        ///     <tr><td>?</td><td>SNAME</td><td>Nazwa ustawienia, taka jak w strukturze.</td></tr>
        ///     <tr><td>?</td><td>SVALUE</td><td>Wartość ustawienia, wielkość zależna od typu.</td></tr>
        /// </table>
        /// Jak widać powyżej, niektóre wielkości zależne są od typu. Lista dostępnych typów:
		/// <list type="bullet">
		///		<item><description>String (typ tekstowy - ciąg znaków)</description></item>
		///		<item><description>Char (typ znakowy - tylko jeden znak)</description></item>
		///		<item><description>Byte (pojedynczy bajt)</description></item>
		///		<item><description>Int32 (typ numeryczny zapisany na 32 bitach)</description></item>
		///		<item><description>Double (typ zmiennoprzecinkowy)</description></item>
		///		<item><description>Array (tablica - nie jest zaimplementowana)</description></item>
		/// </list>
        /// Przed zapisem usuwany jest stary plik konfiguracyjny, w razie błędu w trakcie zapisu, program pozostaje
        /// bez konfiguracji.
        /// </summary>
        /// 
        /// <seealso cref="Parse"/>
        /// <seealso cref="Initialize"/>
        /// <seealso cref="GetDataSize"/>
        /// <seealso cref="SettingsInfo"/>
		//* ============================================================================================================
		public static void SaveSettings()
		{
#		if DEBUG
			Program.LogMessage( "Zapisywanie ustawień do pliku." );
#		endif

			// usuń plik jeżeli istnieje
			if( File.Exists("./cdset.nfo") )
				File.Delete( "./cdset.nfo" );

			// utwórz nowy plik
			var file   = new FileStream( "./cdset.nfo", FileMode.Create );
			var writer = new BinaryWriter( file, Encoding.UTF8 );

			/*
			 * Struktura pliku z ustawieniami:
			 * 5 | CDSET
			 * ? | Wersja bazy danych
			 * ------------------------------------------------------------------------
			 * 2 | Ilość ustawień
			 * 4 | Długość bloku
			 * ===================== LOOP =============================================
			 *   1 | Typ ustawienia (1:string/2:char/3:byte/4:int/5:double/6:array)
			 *   2 | Ilość elementów w tablicy [tylko dla ARRAY]
			 *   2 | Typy elementów w tablicy [tylko dla ARRAY]
			 *   ? | Nazwa ustawienia
			 *   ? | Wartość ustawienia
			 * ========================================================================
			 * ------------------------------------------------------------------------
			*/

			// ciąg rozpoznawczy
			var text = new byte[5] { (byte)'C', (byte)'D', (byte)'S', (byte)'E', (byte)'T' };
			writer.Write( text, 0, 5 );

			// zapisz aktualną wersję programu
			writer.Write( Program.VERSION );

			// ilość ustawień
			writer.Write( (Int16)SettingsInfo.MemberData.Length );
			writer.Write( (Int32)Settings.GetDataSize() );

			// ustawienia
			for( int x = 0; x < SettingsInfo.MemberData.Length; ++x )
			{
				var member = SettingsInfo.MemberData[x];

				// typ
				writer.Write( member.Type );

				// tablica
				if( member.Type == 5 )
				    {}

				// nazwa
				writer.Write( member.Name );
				
				// wartość ustawienia
				var info  = Settings.Info.GetType().GetField( member.Name );
				var value = info.GetValue( Settings.Info );

				switch( member.Type )
				{
				case 1: writer.Write( (String)value ); break;
				case 2: writer.Write( (Char)value );   break;
				case 3: writer.Write( (Byte)value );   break;
				case 4: writer.Write( (Int32)value );  break;
				case 5: writer.Write( (Double)value ); break;
				case 6: break;
				}
			}

			// zamknij plik
			writer.Close();
			file.Close();
		}
        
        /// <summary>
        /// Oblicza rozmiar ustawień konfiguracyjnych.
        /// Funkcja oblicza rozmiar całej struktury zmiennej <see cref="Info"/>.
        /// Sprawdza każdy element i w zależności od typu odpowiednio zwiększa licznik rozmiaru.
        /// Funkcja używana przy zapisie ustawień.
        /// </summary>
        /// 
        /// <seealso cref="SaveSettings"/>
        /// <seealso cref="SettingsInfo"/>
        /// 
        /// <returns>Rozmiar ustawień konfiguracyjnych.</returns>
		//* ============================================================================================================
		public static Int32 GetDataSize()
		{
#		if DEBUG
			Program.LogMessage( "Obliczanie rozmiaru bloku ustawień." );
#		endif

			Int32 length = 0;

			// sumuj każde zapisane pole
			for( int x = 0; x < SettingsInfo.MemberData.Length; ++x )
			{
				var member = SettingsInfo.MemberData[x];

				var name = member.Name;
				int len  = 0;

				// pobierz długość nazwy atrybutu
				len = System.Text.ASCIIEncoding.UTF8.GetByteCount( name );

				// nazwa nie powinna być większa niż 127...
				if( len >= 127 )
					throw new Exception( "Too big size of setting string value..." );

				length += len + 1;

				switch( member.Type )
				{
				case 1:
					// pobierz długość nazwy atrybutu
					len = System.Text.ASCIIEncoding.UTF8.GetByteCount( name );

					// tekst nie powinien być w każdym razie większy niż 16k...
					if( len >= 127 * 127 )
						throw new Exception( "Too big size of setting string value..." );

					// jeżeli tekst jest większy niż 127, dodaj dwa bajty
					if( len > 127 )
						length += len + 2;
					else
						length += len + 1;
				break;
				case 2: length += sizeof(Char);   break;
				case 3: length += sizeof(Byte);   break;
				case 4: length += sizeof(Int32);  break;
				case 5: length += sizeof(Double); break;
				case 6: break;
				}
			}
			return length;
        }

#endregion
    }
}
