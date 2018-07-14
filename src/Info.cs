using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CDesigner
{
	public partial class Info : Form
	{
		// ------------------------------------------------------------- Info -----------------------------------------
		
		public Info()
		{
			this.InitializeComponent();
		}

		// ------------------------------------------------------------- bClose_Click ---------------------------------
		
		private void bClose_Click( object sender, EventArgs ev )
		{
			this.Close();
		}
	}
}
