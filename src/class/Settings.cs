using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;

///
/// $c02 Settings.cs
/// 
/// Wczytywanie ustawień aplikacji.
/// Zapis do struktury i możliwość szybkiego sprawdzania ustawień.
/// Więcej informacji w opisach funkcji.
/// 
/// Autor: Kamil Biały
/// Od wersji: 0.8.x.x
/// Ostatnia zmiana: 2015-02-13
/// 
/// Zmieniono model ustawień - z bazy danych na plik.
/// Baza danych powodowała problemy przy łączeniu się z dwoma instancjami aplikacji na raz.
///

namespace CDesigner
{
	/// 
	/// <summary>
	/// Klasa wczytująca ustawienia.
	/// Zapisuje ustawienia do struktury.
	/// </summary>
	/// 
	public class Settings
	{
		// ===== PRIVATE VARIABLES ==============================================================
		
		// ===== PUBLIC VARIABLES ===============================================================

		/// <summary>Struktura z danymi ustawień.</summary>
		public static SettingsInfo Info;

		// ===== PUBLIC FUNCTIONS ===============================================================

		/**
		 * <summary>
		 * Inicjalizacja klasy do odczytu i zapisu ustawień.
		 * Uzupełnia strukturę ustawieniami domyślnymi.
		 * W przypadku braku pliku przekierowuje do nowego pliku.
		 * </summary>
		 * --------------------------------------------------------------------------------------------------------- **/
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

		/**
		 * <summary>
		 * Wczytywanie ustawień z pliku i zapisywanie wartości do struktury Info.
		 * Plik ustawień: cdset.nfo.
		 * W przypadku braku pliku wyrzucany jest błąd, warto go więc wcześniej utworzyć uzupełniając
		 * strukturę danymi domyślnymi (uruchomienie funkcji Initialize).
		 * </summary>
		 * --------------------------------------------------------------------------------------------------------- **/
		public static void Parse()
		{
			FileStream   file   = new FileStream( "./cdset.nfo", FileMode.Open );
			BinaryReader reader = new BinaryReader( file, Encoding.UTF8 );

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
			Object setnfo = (Object)Settings.Info;

			// załaduj ustawienia
			for( int x = 0; x < count; ++x )
			{
				// typ ustawienia
				Byte type = reader.ReadByte();

				if( type == 5 )
				{
					// TODO : ARRAY OF ELEMENTS
				}

				// nazwa pola
				String name = reader.ReadString();

				// pobierz pole ze struktury
				FieldInfo info = Settings.Info.GetType().GetField( name );

				// zmień jego wartość
				switch( type )
				{
				case 1: info.SetValue( setnfo, reader.ReadString() ); break;
				case 2: info.SetValue( setnfo, reader.ReadChar() );   break;
				case 3: info.SetValue( setnfo, reader.ReadByte() );   break;
				case 4: info.SetValue( setnfo, reader.ReadInt32() );  break;
				case 5: info.SetValue( setnfo, reader.ReadDouble() ); break;
				case 6:
					// TODO : ARRAY OF ELEMENTS
				break;
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

		/**
		 * <summary>
		 * Zapis ustawień do pliku.
		 * Gdy plik istnieje, usuwa go i tworzy nowy z nowymi ustawieniami.
		 * </summary>
		 * --------------------------------------------------------------------------------------------------------- **/
		public static void SaveSettings()
		{
#		if DEBUG
			Program.LogMessage( "Zapisywanie ustawień do pliku." );
#		endif

			// usuń plik jeżeli istnieje
			if( File.Exists("./cdset.nfo") )
				File.Delete( "./cdset.nfo" );

			// utwórz nowy plik
			FileStream   file   = new FileStream( "./cdset.nfo", FileMode.Create );
			BinaryWriter writer = new BinaryWriter( file, Encoding.UTF8 );

			/**
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
			**/

			// ciąg rozpoznawczy
			byte[] text = new byte[5] { (byte)'C', (byte)'D', (byte)'S', (byte)'E', (byte)'T' };
			writer.Write( text, 0, 5 );

			// zapisz aktualną wersję programu
			writer.Write( Program.VERSION );

			// ilość ustawień
			writer.Write( (Int16)SettingsInfo.MemberData.Length );
			writer.Write( (Int32)Settings.GetDataSize() );

			// ustawienia
			for( int x = 0; x < SettingsInfo.MemberData.Length; ++x )
			{
				SettingMemberData member = SettingsInfo.MemberData[x];

				// typ
				writer.Write( member.Type );

				// tablica
				if( member.Type == 5 )
				{
					// TODO : ARRAY OF ELEMENTS
				}

				// nazwa
				writer.Write( member.Name );
				
				// wartość ustawienia
				FieldInfo info  = Settings.Info.GetType().GetField( member.Name );
				object    value = info.GetValue( Settings.Info );

				switch( member.Type )
				{
				case 1: writer.Write( (String)value ); break;
				case 2: writer.Write( (Char)value );   break;
				case 3: writer.Write( (Byte)value );   break;
				case 4: writer.Write( (Int32)value );  break;
				case 5: writer.Write( (Double)value ); break;
				case 6:
					// TODO : ARRAY OF ELEMENTS
				break;
				}
			}

			// zamknij plik
			writer.Close();
			file.Close();
		}

		/**
		 * <summary>
		 * Oblicza rozmiar ustawień.
		 * Rozmiar zapisywany jest do pliku ustawień.
		 * </summary>
		 * --------------------------------------------------------------------------------------------------------- **/
		public static Int32 GetDataSize()
		{
#		if DEBUG
			Program.LogMessage( "Obliczanie rozmiaru bloku ustawień." );
#		endif

			Int32 length = 0;

			// sumuj każde zapisane pole
			for( int x = 0; x < SettingsInfo.MemberData.Length; ++x )
			{
				SettingMemberData member = SettingsInfo.MemberData[x];

				string name = member.Name;
				int    len  = 0;

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
				case 6:
					// TODO
				break;
				}
			}
			return length;
		}
	}
}
