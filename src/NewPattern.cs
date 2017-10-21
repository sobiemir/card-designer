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

		// ------------------------------------------------------------- NewPattern -----------------------------------
		
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

		// ------------------------------------------------------------- bCreate_Click --------------------------------
		
        private void bCreate_Click( object sender, EventArgs ev )
        {
            // brak nazwy wzoru...
            if( tbPatternName.Text == "" )
            {
                MessageBox.Show( this, "Musisz podać nazwę wzoru.", "Błąd danych" );
                return;
            }
            // wzór o tej nazwie już istnieje
            if( Directory.Exists("patterns/" + tbPatternName.Text) )
            {
                MessageBox.Show( this, "Wzór o tej nazwie już istnieje.", "Błąd danych" );
                return;
            }
            // zerowe wymiary papieru...
            if( nHeight.Value == 0 || nWidth.Value == 0 )
            {
                MessageBox.Show( this, "Musisz podać wymiary papieru w mm.", "Błąd danych" );
                return;
            }

            // utwórz katalog
            Directory.CreateDirectory( "patterns/" + tbPatternName.Text );
			Directory.CreateDirectory( "patterns/" + tbPatternName.Text + "/images" );

			// utwórz pusty wzór
			PatternEditor.Create( this.tbPatternName.Text, (short)this.nWidth.Value, (short)this.nHeight.Value );

            // zamknij formulatrz
            this.DialogResult = DialogResult.OK;
            this.Close( );
        }

		// ------------------------------------------------------------- cbPaperFormat_SelectedIndexChanged -----------
		
        private void cbPaperFormat_SelectedIndexChanged( object sender, EventArgs ev )
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
