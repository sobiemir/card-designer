using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.IO;

namespace CDesigner
{
	public partial class NewPattern : Form
	{
		// nazwy formatów papieru
		public static string[] pf_names = 
		{
			"A4",
			"A5"
		};
		// wymiary formatów papieru
		public static int[,] pf_dims =
		{
			{ 210, 297 },
			{ 148, 210 }
		};

		// ---------------------------------------------------------------------------------------------

		public NewPattern( )
		{
			InitializeComponent( );

			// wyświetlanie nazw aktualnych wzorów
			string[] patterns = Directory.GetDirectories( "patterns" );
			foreach( string pattern in patterns )
				cbCopyFrom.Items.Add( pattern.Replace("patterns\\", "") );

			// wyświetlanie nazw dostępnych formatów
			foreach( string pf_name in NewPattern.pf_names )
				cbPaperFormat.Items.Add( pf_name );
		}

		// ---------------------------------------------------------------------------------------------

		private void bCreate_Click(object sender, EventArgs e)
		{
			// brak nazwy wzoru...
			if( tbPatternName.Text == "" )
			{
				MessageBox.Show("Musisz podać nazwę wzoru.");
				return;
			}
			// wzór o tej nazwie już istnieje
			if( Directory.Exists("patterns/" + tbPatternName.Text) )
			{
				MessageBox.Show( "Wzór o tej nazwie już istnieje." );
				return;
			}
			// zerowe wymiary papieru...
			if( nHeight.Value == 0 || nWidth.Value == 0 )
			{
				MessageBox.Show( "Musisz podać wymiary papieru w mm." );
				return;
			}
			// utwórz katalog
			Directory.CreateDirectory( "patterns/" + tbPatternName.Text );

			// utwórz plik konfiguracyjny
			FileStream   file   = new FileStream( "patterns/" + tbPatternName.Text + "/config.cfg", FileMode.OpenOrCreate );
			BinaryWriter writer = new BinaryWriter( file );
			
			// zapisz szerokość i wysokość
			writer.Write( (short)nWidth.Value );
			writer.Write( (short)nHeight.Value );

			// zamknij uchwyty
			writer.Close( );
			file.Close( );

			// zamknij formulatrz
			this.DialogResult = DialogResult.OK;
			this.Close( );
		}

		// ---------------------------------------------------------------------------------------------

		private void cbPaperFormat_SelectedIndexChanged( object sender, EventArgs e )
		{
			// wybrano format papieru
			if( cbPaperFormat.SelectedIndex > 0 )
			{
				// uzupełnij wartości w polach numerycznych
				nWidth.Value  = NewPattern.pf_dims[cbPaperFormat.SelectedIndex - 1, 0];
				nHeight.Value = NewPattern.pf_dims[cbPaperFormat.SelectedIndex - 1, 1];

				// zablokuj pola
				nWidth.Enabled  = false;
				nHeight.Enabled = false;

				return;
			}
			// odblokuj pola numeryczne
			nWidth.Enabled  = true;
			nHeight.Enabled = true;
		}
	}
}
