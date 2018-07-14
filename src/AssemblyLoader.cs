using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace CDesigner
{
	class AssemblyLoader
	{
		/// <summary>Lista plików dll do wczytania.</summary>
		private static readonly string[] DLL_FILE =
		{
			"pdfsharp.dll"
		};

		/// <summary>Nazwy przestrzeni plików dll.</summary>
		private static readonly string[] DLL_NAMESPACE =
		{
			"PdfSharp"
		};

		// ------------------------------------------------------------- Register -------------------------------------
		/// <summary>
		/// Rejestracja zdarzenia do rozwiązywania problemów z plikami DLL.
		/// Może zostać wywołana kilkakrotnie...
		/// </summary>
		public static void Register( )
		{
			AppDomain.CurrentDomain.AssemblyResolve -= AssemblyLoader.ResolveAssembly;
			AppDomain.CurrentDomain.AssemblyResolve += AssemblyLoader.ResolveAssembly;
		}

		// ------------------------------------------------------------- ResolveAssembly ------------------------------
		/// <summary>
		/// Funkcja wczytuje odpowiednie biblioteki z folderu libraries.
		/// Dodaje odpowiednie zdarzenia dla 
		/// </summary>
		private static Assembly ResolveAssembly( object sender, ResolveEventArgs ev )
		{
			AssemblyName name = new AssemblyName(ev.Name);
			
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
					Console.WriteLine( "Ładowanie biblioteki: \"" + file + "\" (" + name.Name + ")..." );
#				endif
					return Assembly.LoadFrom( "./libraries/" + file );
				}
			}

			// nieznana biblioteka...
#		if DEBUG
			Console.WriteLine( "Brak dostępnej biblioteki: \"" + name.Name + "\"..." );
#		endif
			return null;
		}
	}
}
