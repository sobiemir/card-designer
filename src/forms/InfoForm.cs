using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;

namespace CDesigner
{
	public partial class InfoForm : Form
	{
		/// <summary>Wersja aplikacji.</summary>
		public static readonly string VERSION;

		/// <summary>Data kompilacji.</summary>
		public static readonly DateTime BUILD_DATE;

		// ------------------------------------------------------------- Info -----------------------------------------
		/// <summary>
		/// Statyczny konstruktor dla klasy <see cref="InfoForm"/>.
		/// Uzupełnia informacje o wersji aplikacji i dacie kompilacji.
		/// </summary>
		static InfoForm( )
		{
			Version version = Assembly.GetExecutingAssembly().GetName().Version;

			InfoForm.VERSION    = version.ToString();
			InfoForm.BUILD_DATE = new DateTime( 2000, 1, 1 ).Add( new TimeSpan
			(
				TimeSpan.TicksPerDay * version.Build +
				TimeSpan.TicksPerSecond * 2 * version.Revision
			) );
		}

		// ------------------------------------------------------------- Info -----------------------------------------
		/// <summary>
		/// Dynamiczny konstruktor klasy Info.
		/// Aktualizuje informacje o wersji aplikacji i dacie kompilacji w okienku.
		/// </summary>
		public InfoForm( )
		{
			this.InitializeComponent();

			// wyświetl aktualną wersję i date kompilacji programu
			this.lAppVersion.Text = InfoForm.VERSION + ", " + InfoForm.BUILD_DATE.ToString("dd.MM.yyyy");
		}

		// ------------------------------------------------------------- bClose_Click ---------------------------------
		/// <summary>
		/// Zdarzenie wywoływane po kliknięciu w przycisk "Zakończ".
		/// </summary>
		/// <param name="sender">Obiekt wysyłający zdarzenie...</param>
		/// <param name="ev">Argumenty zdarzenia...</param>
		private void bClose_Click( object sender, EventArgs ev )
		{
			this.Close();
		}
	}
}
