///
/// $c04 AssemblyLoader.cs
/// 
/// Ładowanie brakujących bibliotek z folderu ./libraries.
/// 
/// Autor: Kamil Biały
/// Od wersji: 0.7.x.x
/// Ostatnia zmiana: 2016-07-14
/// 
/// CHANGELOG:
/// [16.07.2015] Wersja początkowa.
/// [14.07.2016] Odświeżone komentarze i regiony.
///

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace CDesigner.Utils
{
	/// 
	/// <summary>
	/// Klasa odpowiedzialna za ładowanie bibliotek.
	/// Przechwytuje wiadomości braku bibliotek i szuka ich w foldrze ./libraries.
	/// Lista bibliotek znajduje się w zmiennych DLL_FILE i DLL_NAMESPACE.
	/// </summary>
	/// 
	/// <example>
	/// Przykład użycia klasy:
	/// <code>
	/// AssemblyLoader.Register();
	/// </code>
	/// </example>
	/// 
	class AssemblyLoader
	{
#region ZMIENNE
		/// <summary>
		/// Lista plików dll do opóźnionego ładowania.
		/// Dostępne biblioteki:
		/// <list type="bullet">
		///		<item><description>PdfSharp.dll</description></item>
		/// </list>
		/// </summary>
		/// @hideinitializer
		//* ============================================================================================================
		private static readonly string[] DLL_FILE =
		{
			"PdfSharp.dll"
		};

		/// <summary>Przestrzenie nazw dla plików DLL.</summary>
		/// Przestrzenie nazw dla dostępnych bibliotek:
		/// <list type="bullet">
		///		<item><description>PdfSharp <i>(PdfSharp)</i></description></item>
		/// </list>
		/// @hideinitializer
		private static readonly string[] DLL_NAMESPACE =
		{
			"PdfSharp"
		};
#endregion

#region FUNKCJE PODSTAWOWE

		/// <summary>
		/// Rejestracja zdarzenia do rozwiązywania problemów z plikami DLL.
		/// Może zostać wywołana kilkukrotnie, ustawienia zostaną nadpisane.
		/// </summary>
        /// 
        /// <seealso cref="ResolveAssembly"/>
		//* ============================================================================================================
		public static void Register()
		{
			AppDomain.CurrentDomain.AssemblyResolve -= AssemblyLoader.ResolveAssembly;
			AppDomain.CurrentDomain.AssemblyResolve += AssemblyLoader.ResolveAssembly;
		}

		/// <summary>
		/// Funkcja rozwiązuje konflikty dotyczące braku biblioteki.
		/// Wczytuje bibliotekę gdy tylko znajdzie ją w folderze libraries.
		/// Funkcja wywoływana automatycznie podczas próby dostępu do funkcji z bliblioteki której nie ma.
		/// </summary>
		/// 
		/// <param name="sender">Obiekt wywołujący zdarzenie.</param>
		/// <param name="ev">Argumenty zdarzenia.</param>
		/// 
		/// <returns>Klasa z załadowaną biblioteką.</returns>
        /// 
        /// <seealso cref="Register"/>
		//* ============================================================================================================
		private static Assembly ResolveAssembly( object sender, ResolveEventArgs ev )
		{
			AssemblyName name = new AssemblyName(ev.Name);
			
#if DEBUG
			Program.LogMessage( "Przygotowywanie elementu: " + name.Name );
#endif

			// nie uwzględniaj bibliotek w zasobach
			if( name.Name == "cdesigner.resources" )
				return null;

			// iteruj po dostępnych bibliotekach
			for( int x = 0, y = AssemblyLoader.DLL_NAMESPACE.Count(); x < y; ++x )
			{
				// sprawdź czy nazwa się zgadza
				if( name.Name == AssemblyLoader.DLL_NAMESPACE[x] )
				{
					// jeżeli tak, wczytaj
					string file = AssemblyLoader.DLL_FILE[x];
#				if DEBUG
					Program.LogMessage( "Wczytywanie biblioteki: '" + file + "' (" + name.Name + ")..." );
#				endif
					return Assembly.LoadFrom( "./libraries/" + file );
				}
			}

			// nieznana biblioteka...
#		if DEBUG
			Program.LogMessage( "Brak dostępnej biblioteki: '" + name.Name + "'..." );
#		endif
			return null;
		}

#endregion
	}
}
