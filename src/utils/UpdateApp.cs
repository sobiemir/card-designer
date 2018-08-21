///
/// $u12 UpdateApp.cs
/// 
/// Plik zawierający klasę do pobierania informacji o aktualizacji aplikacji.
/// Pobiera informacje o zmianach w poszczególnych wersjach i numer wersji do której program będzie aktualizowany.
/// Funkcje najlepiej wywoływać w osobnym procesie, nie zakłócając działania aplikacji.
/// 
/// Autor: Kamil Biały
/// Od wersji: 0.8.x
/// Ostatnia zmiana: 2016-11-14
/// 
/// CHANGELOG:
/// [14.11.2016] Wersja początkowa - wszystkie funkcje w tej klasie to funkcje wyodrębnione
///              z formularza aktualizacji aplikacji.
///

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.ComponentModel;

namespace CDesigner.Utils
{
    /// 
    /// <summary>
    /// Klasa informacji o aktualizacji aplikacji.
    /// Pobiera plik zawierający zmiany w poszczególnych wersjach oraz wersję dostępną do aktualizacji.
    /// W przypadku błędu przechowuje wyjątek w specjalnej zmiennej nie zatrzymując programu.
    /// Funkcje wyodrębnione w celu wywoływania w innym procesie.
    /// </summary>
    /// 
	/// <example>
	/// Przykład użycia klasy:
	/// <code>
    /// // sprawdza, czy atkualizacjia jest dostępna
    /// if( !UpdateApp.CheckAvailability() )
    /// {
    ///     Console.WriteLine( "Aktualizacja nie jest dostępna." );
    ///     goto _G_EXCEPTION_RESOLVE;
    /// }
    /// 
    /// Console.WriteLine( "Dostępna jest nowa wersja programu: " +
    ///     UpdateApp.VERSION + ", " + UpdateApp.BUILD_DATE );
    /// 
    /// pobiera listę zmian w poszczególnych wersjach
    /// if( !UpdateApp.GetChangeLog() )
    /// {
    ///     Console.WriteLine( "Nie można pobrać informacji o zmianach." );
    ///     goto _G_EXCEPTION_RESOLVE;
    /// }
    /// 
    /// // wyświetla listę zmian
    /// Console.WriteLine( "Lista zmian począwszy od wersji początkowej:" );
    /// Console.WriteLine( UpdateApp.ChangeLog );
    /// return;
    /// 
    /// _G_EXCEPTION_RESOLVE:
    /// 
    /// // sprawdź, czy nie wystąpił błąd
    /// if( UpdateApp.ConnectionError.Response == null )
    ///     Console.WriteLine( "Nie można połączyć się z serwerem." );
    /// else
    ///     Console.WriteLine( "Wystąpił błąd: " + UpdateApp.ConnectionError.Message );
	/// </code>
	/// </example>
    /// 
    public static class UpdateApp
    {
#region ZMIENNE

        /// <summary>Numer nowej wersji programu.</summary>
        private static string VERSION = null;

        /// <summary>Data kompilacji nowej wersji programu.</summary>
        private static DateTime BUILD_DATE = new DateTime();

        /// <summary>Zawartość pliku zawierającego informacje o zmianach w poszczególnych wersjach.</summary>
        private static string CHANGELOG = null;

        /// <summary>Błąd połączenia z internetem lub serwerem.</summary>
        private static WebException CONNECT_ERROR = null;

        /// <summary>Klucz używany przy łączeniu się z serwerem.</summary>
        private static string KEY = "61EEBC877DBA9736FD93B2C1CC71C";

#endregion

#region WŁAŚCIWOŚCI

        /// <summary>
        /// Czy aktualizacja jest dostępna?
        /// Sprawdza numer wersji i flagę błędu, która uzupełniana jest przy błędzie łączenia z serwerem.
        /// Właściwość tylko do odczytu.
		/// </summary>
        /// 
        /// <seealso cref="CheckAvailability"/>
		//* ============================================================================================================
        public static bool Available
        {
            get
            {
                return UpdateApp.CONNECT_ERROR == null && UpdateApp.VERSION != null
                    && UpdateApp.VERSION != Program.VERSION;
            }
        }

        /// <summary>
        /// Lista zmian w poszczególnych wersjach.
        /// Zwraca pobraną wcześniej listę zmian we wszystkich wersjach programu.
        /// Właściwość tylko do odczytu.
		/// </summary>
        /// 
        /// <seealso cref="GetChangeLog"/>
		//* ============================================================================================================
        public static string ChangeLog
        {
            get { return UpdateApp.CHANGELOG; }
        }

        /// <summary>
        /// Numer nowej wersji programu, który jest dostępny do aktualizacji.
        /// Zwraca pobraną wcześniej odpowiednią funkcją wersję programu dostępną do aktualizacji.
        /// Właściwość tylko do odczytu.
		/// </summary>
        /// 
        /// <seealso cref="CheckAvailability"/>
		//* ============================================================================================================
        public static string Version
        {
            get { return UpdateApp.VERSION; }
        }

        /// <summary>
        /// Flaga błędu połączenia z serwerem.
        /// W przypadku gdy pobieranie wersji lub pliku z listą zmian nie przebiegnie pomyślnie, zmienna będzie
        /// przechowywała informacje o wyjątku, który wystąpił podczas próby połączenia się z serwerem.
        /// Właściwość tylko do odczytu.
		/// </summary>
        /// 
        /// <seealso cref="GetChangeLog"/>
        /// <seealso cref="CheckAvailability"/>
		//* ============================================================================================================
        public static WebException ConnectionError
        {
            get { return UpdateApp.CONNECT_ERROR; }
        }

        /// <summary>
        /// Data kompilacji aktualizacji.
        /// Pobierana jest z wersji programu i jest równie ważnym znacznikiem aktualizacji.
        /// Właściwość tylko do odczytu.
		/// </summary>
        /// 
        /// <seealso cref="CheckAvailability"/>
		//* ============================================================================================================
        public static DateTime BuildDate
        {
            get { return UpdateApp.BUILD_DATE; }
        }

#endregion

#region POBIERANIE DANYCH

        /// <summary>
        /// Sprawdza czy jest dostępna aktualizacja.
        /// Funkcja wysyła zapytanie do serwera i oczekuje odpowiedzi w postaci nowej wersji programu.
        /// Gdy aktualizacja jest dostępna, serwer zwraca numer nowej wersji programu, w przeciwnym wypadku 0.
        /// Funkcja wyodrębniona z formularza aktualizacji na rzecz możliwości uruchomienia w tle.
        /// </summary>
        /// 
        /// <returns>Czy sprawdzenie powiodło się?</returns>
        /// 
        /// <seealso cref="ConnectionError"/>
        /// <seealso cref="Version"/>
        /// <seealso cref="Available"/>
		//* ============================================================================================================
        public static bool CheckAvailability()
        {
#       if DEBUG
            Program.LogMessage( "Wyszukiwanie dostępnych aktualizacji." );
#       endif
			string url = "http://app.aculo.pl/cdesigner/check/" + Program.VERSION;

            try
            {
                // pobierz dane dotyczące wersji
                var request  = (HttpWebRequest)WebRequest.Create( url );
                var response = (HttpWebResponse)request.GetResponse();
                var reader   = response.GetResponseStream();
                var version  = reader.ReadByte() == '0' ? null : new StreamReader(reader).ReadToEnd();

                response.Close();
                reader.Close();

#           if DEBUG
                if( version == null )
                {
                    Program.LogMessage( "Brak dostępnych aktualizacji." );
                    return true;
                }
                else
                    Program.LogMessage( "Dostępna jest aktualizacja programu do wersji: v" + version + "." );
#           endif

                // brak błędu, resetuj wcześniejszy
                UpdateApp.CONNECT_ERROR = null;
                UpdateApp.VERSION       = version;

			    // podziel wersje na 4 części
			    string[] expl = UpdateApp.VERSION == null
                    ? Program.VERSION.Split('.')
                    : UpdateApp.VERSION.Split('.');

			    // pobierz datę kompilacji z wersji aplikacji
			    try
			    {
				    UpdateApp.BUILD_DATE = new DateTime( 2000, 1, 1 ).Add( new TimeSpan
				    (
					    TimeSpan.TicksPerDay    * Convert.ToInt32(expl[2]) +
					    TimeSpan.TicksPerSecond * 2 * Convert.ToInt32(expl[3])
				    ) );
			    }
			    // błąd konwertowania wersji aplikacji...
			    catch( Exception ex )
			    {
				    UpdateApp.BUILD_DATE = new DateTime( 1970, 1, 1 );
				    Program.LogMessage( "Problem z pobieraniem daty z wersji: " + ex.Message );
			    }
            }
            catch( WebException ex )
            {
                // ustaw błąd
                UpdateApp.CONNECT_ERROR = ex;
                UpdateApp.VERSION       = null;
                return false;
            }
            return true;
        }

        /// <summary>
        /// Pobiera plik z informacją o zmianach w programie.
        /// Funkcja wysyła do serwera prośbę o przesłanie zawartości pliku ze zmianami.
        /// Po otrzymaniu przechowuje je w zmiennej dla szybkiego dostępu w późniejszych fazach programu.
        /// Funkcja wyodrębniona z formularza aktualizacji na rzecz możliwości uruchomienia w tle.
        /// </summary>
        /// 
        /// <returns>Czy pobieranie pliku ze zmianami powiodło się?</returns>
        /// 
        /// <seealso cref="ChangeLog"/>
        /// <seealso cref="ConnectionError"/>
		//* ============================================================================================================
        public static bool GetChangeLog()
        {
#       if DEBUG
            Program.LogMessage( "Pobieranie pliku ze zmianami w poszczególnych wersjach." );
#       endif
            try
            {
                // pobierz dane dotyczące wersji
			    var request   = (HttpWebRequest)WebRequest.Create( "http://app.aculo.pl/cdesigner/readme" );
			    var response  = (HttpWebResponse)request.GetResponse();
                var reader    = response.GetResponseStream();
                var changelog = new StreamReader(reader).ReadToEnd();

                response.Close();
                reader.Close();

#           if DEBUG
                Program.LogMessage( "Pobrano plik zawierający zmiany w poszczególnych wersjach programu." );
#           endif

                // brak błędu, resetuj wcześniejszy
                UpdateApp.CONNECT_ERROR = null;
                UpdateApp.CHANGELOG     = changelog;
            }
            catch( WebException ex )
            {
                // błąd, ustaw wyjątek w zmiennej
                UpdateApp.CONNECT_ERROR = ex;
                UpdateApp.CHANGELOG     = null;
                
                return false;
            }
            return true;
        }

#endregion

#region MIGRACJE

        /// <summary>
        /// Pobiera nazwy wszystkich plików do kompresji.
        /// Po pobraniu danych plików do kompresji ich nazwy zapisywane są w pliku update.lst.
        /// Pliki wymienione w zmiennej przeznaczone są do kompresji aktualizacji.
        /// Wszystkie te pliki będą podmieniane w procesie aktualizacji po jej akceptacji.
        /// </summary>
        /// 
        /// <seealso cref="Forms.UpdateForm"/>
        /// 
        /// <returns>Lista plików do kompresji.</returns>
		//* ============================================================================================================
        public static List<string> ListChanges()
        {
            List<string> files = new List<string>();

            files.Add( "./cdesigner.exe" );
            files.Add( "./cdrestore.exe" );
            
            // na razie dodaj plik konfiguracyjny - potem dodawaj tylko zmiany
            files.Add( "./cdset.nfo" );

            string[] directories =
            {
                "./libraries",
                "./images",
                "./languages"
            };

            // wyszukaj wszystkie pliki w tych folderach
            files.AddRange( Program.GetFilesFromFolder(directories[0], true) );
            files.AddRange( Program.GetFilesFromFolder(directories[1], true) );
            files.AddRange( Program.GetFilesFromFolder(directories[2], true) );

            // zapisz do pliku
            DataBackup.CreateFileList( files, "./update.lst" );

            // zwróć listę plików
            return files;
        }
        
        /// <summary>
        /// Adres strony do pobrania aktualizacji.
        /// Adres zawiera klucz dostępu do aktualizacji, działa na zasadzie hasła.
        /// </summary>
        /// 
        /// <seealso cref="Forms.UpdateForm"/>
        /// 
        /// <returns>Link do pobrania aktualizacji.</returns>
		//* ============================================================================================================
        public static string DownloadLink()
        {
            return "http://app.aculo.pl/cdesigner/download/" + UpdateApp.KEY;
        }

#endregion
    }
}
