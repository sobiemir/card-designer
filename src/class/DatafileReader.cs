using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CDesigner
{
	public class DatafileReader
	{
		public DatafileReader()
		{
			
		}

		public DatafileReader( string file )
		{

		}

		~DatafileReader()
		{
			GC.Collect();
		}

		public List<string> Column
		{
			get { return new List<string>(); }
		}

		public List<List<string>> Row
		{
			get { return new List<List<string>>(); }
		}
	}
}
