using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CDesigner.Utils;

namespace CDesigner
{
	public class FilterCreator
	{
		
		private static string _default_format = " ";

		/// <summary>Lista kolumn po przekształceniu.</summary>
		private List<string> _columns;
		
		/// <summary>Lista kolumn do połączenia w jedną.</summary>
		private List<List<int>> _joincols = new List<List<int>>();

		/// <summary>Lista filtrowanych wierszy.</summary>
		private List<string> _rows = new List<string>();

		/// <summary>Ilość wyświetlanych wierszy.</summary>
		private int _rowsnum = 0;
		
		/// <summery>Czytnik bazy danych - filtrowane dane.</summery>
		private DatabaseReader _reader;

		/// <summary>Lista zdefiniowanych filtrów.</summary>
		private List<List<FilterInfo>> _filter;
		
		/// 
		/// ------------------------------------------------------------------------------------------------------------
		public List<List<FilterInfo>> Filter
		{
			get { return this._filter; }
		}

		/// 
		/// ------------------------------------------------------------------------------------------------------------
		public List<string> Columns
		{
			get { return this._columns; }
		}

		///
		/// ------------------------------------------------------------------------------------------------------------
		public List<string> Rows
		{
			get { return this._rows; }
		}

		///
		/// ------------------------------------------------------------------------------------------------------------
		public int RowsNumber
		{
			get { return this._rowsnum; }
		}

		///
		/// ------------------------------------------------------------------------------------------------------------
		public FilterCreator( DatabaseReader reader )
		{
			// pobierz ilość wyświetlanych wierszy
			int rowsnum = reader.RowsNumber > Settings.Info.FC_RowsNumber
				? Settings.Info.FC_RowsNumber
				: reader.RowsNumber;

			this._columns   = new List<string>( reader.ColumnsNumber );
			this._rows      = new List<string>( reader.ColumnsNumber * rowsnum );
			this._reader    = reader;
			this._rowsnum   = rowsnum;

			this._filter = new List<List<FilterInfo>>( reader.ColumnsNumber );

			// skopiuj nazwy kolumn
			foreach( string column in reader.Columns )
			{
				this._columns.Add( column );

				// dodaj listę filtrów dla wybranej kolumny
				this.Filter.Add( new List<FilterInfo>() );
			}

			// zapisz przykładowe dane dla wyświetlanych wierszy
			for( int x = 0; x < reader.ColumnsNumber; ++x )
				for( int y = 0; y < rowsnum; ++y )
					this._rows.Add( reader.Rows[x,y] );
		}

		/// 
		/// ------------------------------------------------------------------------------------------------------------
		public int AddColumn( string name )
		{
			this._columns.Add( name );
			this._joincols.Add( new List<int>() );

			// dodaj nowy filtr
			this._filter.Add( new List<FilterInfo>() );

			// dodaj puste wiersze
			for( int x = 0; x < this._rowsnum; ++x )
				this._rows.Add( "" );

			// zwróć numer kolumny
			return this._columns.Count - 1;
		}

		/// 
		/// ------------------------------------------------------------------------------------------------------------
		public void RemoveColumn( int column )
		{
			if( column < 0 || column >= this._columns.Count )
			{
#			if DEBUG
				Program.LogMessage( "Podano numer kolumny spoza zakresu." );
#			endif
				return;
			}

			// usuń kolumnę
			this._columns.RemoveAt( column );
			this._rows.RemoveRange( column * this._rowsnum, this.RowsNumber );

			// usuń filtry
			this._filter.RemoveAt( column );

			// usuń połączenia kolumny
			if( column > this._reader.ColumnsNumber )
				this._joincols.RemoveAt( column - this._reader.ColumnsNumber );
		}

		///
		/// ------------------------------------------------------------------------------------------------------------
		public void SetColumnJoin( int column, List<int> tojoin )
		{
			// sprawdź czy numer nie wykracza poza zakres
			if( column >= this._columns.Count || column < this._reader.ColumnsNumber )
			{
#			if DEBUG
				Program.LogMessage( "Podano numer kolumny spoza zakresu." );
#			endif
				return;
			}

			// indeks kolumny
			int col = column - this._reader.ColumnsNumber;

			// przypisz nowe kolumny do połączenia
			this._joincols[col] = tojoin;
		}

		///
		/// ------------------------------------------------------------------------------------------------------------
		public void TestFilters()
		{
			for( int x = 0; x < this._rowsnum; ++x )
			{
				for( int y = 0; y < this._columns.Count; ++y )
				{
					List<FilterInfo> finfo = this._filter[y];

					// łączenie danych dla kolumny
					if( y > this._reader.ColumnsNumber )
					{
						string format = null;

						// pobierz format łączenia kolumn
						for( int z = 0; z < finfo.Count; ++z )
							if( finfo[z].Filter == FILTERTYPE.Format )
							{
								format = finfo[z].Result;
								break;
							}

						// brak formatu, przyjmij domyślny
						if( format == null )
							for( int z = 1; z <= this._columns.Count; ++z )
								format += z == 1 ? "#" + z : FilterCreator.DefaultFormat + "#" + z;

#					if DEBUG
						Program.LogMessage( "Przyjęty format łączenia danych dla nowej kolumny: " + format );
#					endif

						
					}
				}
			}
		}

		private string FilterValue( string value, int column )
		{
			List<FilterInfo> filters = this._filter[column];

			// brak filtrów dla wybranej kolumny
			if( filters.Count == 0 )
				return value;

			// wyrzuć wiersz [exclude]
			for( int x = 0; x < filters.Count; ++x )
			{
				if( filters[x].Exclude )
				{
					if( filters[x].Filter == FILTERTYPE.Equal && value == filters[x].Modifier )
						return null;
					else if( filters[x].Filter == FILTERTYPE.NotEqual && value != filters[x].Modifier )
						return null;
				}
			}
			/*
					stop = false;

					if( limit == 0 )
						return;

					// wyłącz, zostaw [W/Z] (exclude / leave)
					for( int z = 0; z < filter.Count; ++z )
						if( filter[z].exclude )
						{
							string rowstr = filter[z].real_index > -1
								? this._rows[filter[z].real_index * this._rowsnum + stepper]
								: this._rows[filter[z].index * this._rowsnum + stepper];

							// filtruj gdy elementy nie są równe
							if( filter[z].filter != (int)DataFilterTypes.DFT_NOT_EQUAL &&
								rowstr == filter[z].modifier )
							{
								stop = true;
								break;
							}
							// filtruj gdy elementy są równe
							else if( filter[z].filter == (int)DataFilterTypes.DFT_NOT_EQUAL &&
								rowstr != filter[z].modifier )
							{
								stop = true;
								break;
							}
						}

					// pominięcie rekordu
					if( stop == true )
					{
						--x;
						--maxr;
						continue;
					}
					
					// format dla wiersza
					rows[x] = format;

					// łączenie
					for( int y = 0; y < maxc; ++y )
					{
						// pobierz aktualną komórkę
						rowdat = this._rows[cols[y] * this._rowsnum + stepper];

						// filtrowanie danych podstawowych dla podanej kolumny
						for( int z = 0; z < filter.Count; ++z )
						{
							if( filter[z].index == cols[y] && filter[z].level == 1 && filter[z].parent == 1 )
							{
								switch( filter[z].filter )
								{
								// wielkie litery
								case (int)DataFilterTypes.DFT_UPPER_CASE:
									rowdat = rowdat.ToUpper();
								break;
								// małe litery
								case (int)DataFilterTypes.DFT_LOWER_CASE:
									rowdat = rowdat.ToLower();
								break;
								// nazwa własna (tytuł - pierwsza litera słowa jest duża)
								case (int)DataFilterTypes.DFT_TITLE_CASE:
									rowdat = Program.StringTitleCase( rowdat );
								break;
								}
							}
						}

						// filtrowanie danych dla kolumny łączonej
						for( int z = 0; z < filter.Count; ++z )
						{
							if( filter[z].index == y && filter[z].level == 2 && filter[z].parent == col )
							{
								switch( filter[z].filter )
								{
								// wielkie litery
								case (int)DataFilterTypes.DFT_UPPER_CASE:
									rowdat = rowdat.ToUpper();
								break;
								// małe litery
								case (int)DataFilterTypes.DFT_LOWER_CASE:
									rowdat = rowdat.ToLower();
								break;
								// nazwa własna (tytuł - pierwsza litera słowa jest duża)
								case (int)DataFilterTypes.DFT_TITLE_CASE:
									rowdat = Program.StringTitleCase( rowdat );
								break;
								}
							}
						}

						// wstawianie elementu do kolumny
						rows[x] = rows[x].Replace( "#" + (y+1), rowdat );
						limit--;
					}
				}
			*/
			return "";
		}

		///
		/// ------------------------------------------------------------------------------------------------------------
		public void ApplyFilters()
		{
		}







		/// 
		/// ------------------------------------------------------------------------------------------------------------
		public static string DefaultFormat
		{
			get { return FilterCreator._default_format; }
			set { FilterCreator._default_format = value; }
		}



		///
		/// Tworzenie testowej zawartości z uwzględnieniem filtrów.
		/// ------------------------------------------------------------------------------------------------------------
		public void CreateTestContent( int col, List<string> rows, List<int> cols, List<FilterData> filter, int limit )
		{
			int  maxr = rows.Count,
				 maxc = cols.Count;
			bool stop = false;

#		if DEBUG
			Program.LogMessage( "Tworzenie zbioru testowego dla kolumny o id " + col + "." );
#		endif

			if( cols != null )
			{
				string format = "";
				string rowdat = "";

				// wyszukaj format kolumny dla kolumn łączonych
				for( int x = 0; x < filter.Count; ++x )
				{
					if( filter[x].filter == (int)FILTERTYPE.Format && filter[x].index == col )
					{
						format = filter[x].result;
						break;
					}
				}

				// brak formatu, ustaw domyślny
				if( format == "" )
					for( int x = 1; x <= maxc; ++x )
						format += x == 1 ? "#" + x : FilterCreator.DefaultFormat + "#" + x;

#			if DEBUG
				Program.LogMessage( "Przyjęty format kolumny: " + format );
#			endif

				// wyczyść rekordy
				for( int x = 0; x < maxr; ++x )
					rows[x] = "";

				// łączenie kolumn
				for( int x = 0, stepper = 0; x < maxr; ++x, ++stepper )
				{
					stop = false;

					if( limit == 0 )
						return;

					// wyłącz, zostaw [W/Z] (exclude / leave)
					for( int z = 0; z < filter.Count; ++z )
						if( filter[z].exclude )
						{
							string rowstr = filter[z].real_index > -1
								? this._rows[filter[z].real_index * this._rowsnum + stepper]
								: this._rows[filter[z].index * this._rowsnum + stepper];

							// filtruj gdy elementy nie są równe
							if( filter[z].filter != (int)FILTERTYPE.NotEqual &&
								rowstr == filter[z].modifier )
							{
								stop = true;
								break;
							}
							// filtruj gdy elementy są równe
							else if( filter[z].filter == (int)FILTERTYPE.NotEqual &&
								rowstr != filter[z].modifier )
							{
								stop = true;
								break;
							}
						}

					// pominięcie rekordu
					if( stop == true )
					{
						--x;
						--maxr;
						continue;
					}
					
					// format dla wiersza
					rows[x] = format;

					// łączenie
					for( int y = 0; y < maxc; ++y )
					{
						// pobierz aktualną komórkę
						rowdat = this._rows[cols[y] * this._rowsnum + stepper];

						// filtrowanie danych podstawowych dla podanej kolumny
						for( int z = 0; z < filter.Count; ++z )
						{
							if( filter[z].index == cols[y] && filter[z].level == 1 && filter[z].parent == 1 )
							{
								switch( filter[z].filter )
								{
								// wielkie litery
								case (int)FILTERTYPE.UpperCase:
									rowdat = rowdat.ToUpper();
								break;
								// małe litery
								case (int)FILTERTYPE.LowerCase:
									rowdat = rowdat.ToLower();
								break;
								// nazwa własna (tytuł - pierwsza litera słowa jest duża)
								case (int)FILTERTYPE.TitleCase:
									rowdat = Program.StringTitleCase( rowdat );
								break;
								}
							}
						}

						// filtrowanie danych dla kolumny łączonej
						for( int z = 0; z < filter.Count; ++z )
						{
							if( filter[z].index == y && filter[z].level == 2 && filter[z].parent == col )
							{
								switch( filter[z].filter )
								{
								// wielkie litery
								case (int)FILTERTYPE.UpperCase:
									rowdat = rowdat.ToUpper();
								break;
								// małe litery
								case (int)FILTERTYPE.LowerCase:
									rowdat = rowdat.ToLower();
								break;
								// nazwa własna (tytuł - pierwsza litera słowa jest duża)
								case (int)FILTERTYPE.TitleCase:
									rowdat = Program.StringTitleCase( rowdat );
								break;
								}
							}
						}

						// wstawianie elementu do kolumny
						rows[x] = rows[x].Replace( "#" + (y+1), rowdat );
						limit--;
					}
				}
			}
#		if DEBUG
			Program.LogMessage( "Zbiór testowy został utworzony." );
#		endif
		}

		///
		/// ------------------------------------------------------------------------------------------------------------
		public void SetColumnContent( int column, List<int> subcols )
		{
			if( column < 0 || column >= this._columns.Count )
				return;

			int position = column * this._rowsnum;
			int stepper  = 0;

			for( int x = 0; x < this._rowsnum; ++x, ++position, ++stepper )
			{
				this._rows[position] = "";

				for( int y = 0; y < subcols.Count; ++y )
				{
					if( subcols[y] < 0 || subcols[y] >= this._columns.Count )
						continue;

					string value = this._rows[subcols[y] * this._rowsnum + stepper].Trim();
					
					if( value == "" )
						continue;

					if( this._rows[position] == "" )
						this._rows[position] += value;
					else
						this._rows[position] += FilterCreator.DefaultFormat + value;
				}
			}
		}

		///
		/// ------------------------------------------------------------------------------------------------------------
		public void ClearColumn( int column )
		{
			if( column < 0 || column >= this._columns.Count )
				return;

			int position = column * this._rowsnum;

			for( int x = 0; x < this._rowsnum; ++x, ++position )
				this._rows[position] = "";
		}


		///
		/// ------------------------------------------------------------------------------------------------------------
		public void ApplyFilters( DatabaseReader reader, List<FilterData> filters )
		{
		}
	}
}
