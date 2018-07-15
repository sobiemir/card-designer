using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

///
/// $c04 AssemblyLoader.cs
/// 
/// Ładowanie brakujących bibliotek z folderu ./libraries.
/// 
/// Autor: Kamil Biały
/// Od wersji: 0.7.x.x
/// Ostatnia zmiana: 2015-07-16
///

namespace CDesigner
{
	/// 
	/// <summary>
	/// Klasa odpowiedzialna za ładowanie bibliotek.
	/// Przechwytuje wiadomości braku bibliotek i szuka ich w foldrze ./libraries.
	/// Lista bibliotek znajduje się w zmiennych DLL_FILE i DLL_NAMESPACE.
	/// </summary>
	/// 
	class AssemblyLoader
	{
		// ===== PRIVATE VARIABLES ==============================================================

		/// <summary>Lista plików dll do wczytania.</summary>
		private static readonly string[] DLL_FILE =
		{
			"PdfSharp.dll",
			"SQLite.Core.dll"
		};

		/// <summary>Nazwy przestrzeni plików dll.</summary>
		private static readonly string[] DLL_NAMESPACE =
		{
			"PdfSharp",
			"System.Data.SQLite"
		};
		
		// ===== PUBLIC FUNCTIONS ===============================================================

		/**
		 * <summary>
		 * Rejestracja zdarzenia do rozwiązywania problemów z plikami DLL.
		 * Może zostać wywołana kilkakrotnie...
		 * </summary>
		 * --------------------------------------------------------------------------------------------------------- **/
		public static void Register()
		{
			AppDomain.CurrentDomain.AssemblyResolve -= AssemblyLoader.ResolveAssembly;
			AppDomain.CurrentDomain.AssemblyResolve += AssemblyLoader.ResolveAssembly;
		}

		// ===== PRIVATE FUNCTIONS ==============================================================

		/**
		 * <summary>
		 * Funkcja wczytuje odpowiednie biblioteki z folderu libraries.
		 * Dodaje obsługę zdarzenia wykonywanego przy braku biblioteki w programie.
		 * </summary>
		 * 
		 * <param name="sender">Obiekt wywołujący zdarzenie.</param>
		 * <param name="ev">Argumenty zdarzenia.</param>
		 * --------------------------------------------------------------------------------------------------------- **/
		private static Assembly ResolveAssembly( object sender, ResolveEventArgs ev )
		{
			AssemblyName name = new AssemblyName(ev.Name);
			
#if DEBUG
			Program.LogMessage( "Przygotowywanie elementu: " + name.Name );
#endif

			// nie uwzględniaj bibliotek w zasobach
			if( name.Name == "CDesigner.resources" )
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
	}
}
