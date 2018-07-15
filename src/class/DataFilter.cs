using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CDesigner
{
	public class DataFilter
	{
		private string _default_format = " ";
		private List<string> _columns;
		private List<string> _rows;
		private int _rowsnum = 0;

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
		public DataFilter( DatabaseReader reader )
		{
			this._columns = new List<string>( reader.ColumnsNumber );
			this._rows = new List<string>( reader.RowsNumber * reader.ColumnsNumber );
			this._rowsnum = reader.RowsNumber;

			foreach( string column in reader.Columns )
				this._columns.Add( column );

			foreach( string row in reader.Rows )
				this._rows.Add( row );
		}

		///
		/// ------------------------------------------------------------------------------------------------------------
		public int AddColumn( string name )
		{
			this._columns.Add( name );
			
			for( int x = 0; x < this._rowsnum; ++x )
				this._rows.Add( "" );

			return this._columns.Count - 1;
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
						this._rows[position] += this._default_format + value;
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
		public void RemoveColumn( int column )
		{
			if( column < 0 || column >= this._columns.Count )
				return;

			this._columns.RemoveAt( column );
			this._rows.RemoveRange( column * this._rowsnum, this.RowsNumber );
		}

		///
		/// ------------------------------------------------------------------------------------------------------------
		public void ApplyFilters( DatabaseReader reader, List<FilterData> filters )
		{
			for( int x = 0; x < reader.ColumnsNumber; ++x )
			{
				int position = x * this._rowsnum;

				for( int y = 0; y < reader.RowsNumber; ++y, ++position )
					if( reader.Rows[x,y] != this._rows[position] )
					{
						this._rows[position] = reader.Rows[x,y];
						Console.WriteLine( "SIEMA" );
					}
			}

			foreach( FilterData filter in filters )
			{
				if( filter.column < 0 || filter.column >= this._columns.Count )
					continue;

				int position = filter.column * this._rowsnum;

				switch( filter.filter )
				{
				case 0:
					for( int x = 0; x < this._rowsnum; ++x, ++position )
						this._rows[position] = this._rows[position].ToUpper( );
				break;
				case 1:
					for( int x = 0; x < this._rowsnum; ++x, ++position )
						this._rows[position] = this._rows[position].ToLower( );
				break;
				case 2:
					for( int x = 0; x < this._rowsnum; ++x, ++position )
						this._rows[position] = Program.StringTitleCase( this._rows[position] );
				break;
				case 3:
				break;
				case 4:
				break;
				case 5:
				break;
				}
			}
		}
	}
}
