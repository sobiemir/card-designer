using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.IO;
using System.Text.RegularExpressions;

namespace CDesigner
{
	public partial class NewPattern : Form
	{
		private static string[] _format_names = 
		{
			"A4",
			"A5"
		};
		private static int[,] _format_dims =
		{
			{ 210, 297 },
			{ 148, 210 }
		};
		private string _pattern_name = "";

		// ------------------------------------------------------------- FormatNames ----------------------------------
		
		public static string[] FormatNames
		{
			get { return NewPattern._format_names; }
		}

		// ------------------------------------------------------------- FormatDims -----------------------------------
		
		public static int[,] FormatDims
		{
			get { return NewPattern._format_dims; }
		}

		// ------------------------------------------------------------- PatternName ----------------------------------

		public string PatternName
		{
			get { return this._pattern_name; }
		}

		// ------------------------------------------------------------- DetectFormat ---------------------------------
		
		public static int DetectFormat( int width, int height )
		{
			int[,] format_dims = NewPattern.FormatDims;

			// szukaj formatu po rozmiarze
			for( int x = 0; x < format_dims.GetLength(0); ++x )
				if( format_dims[x,0] == width && format_dims[x,1] == height )
					return x;

			return -1;
		}









		// ------------------------------------------------------------- NewPattern -----------------------------------
		
		public NewPattern( )
		{
			this.InitializeComponent();

			// wyświetlanie nazw aktualnych wzorów
			string[] patterns = Directory.GetDirectories( "patterns" );
			foreach( string pattern in patterns )
			{
				// brak pliku konfiguracji
				if( !File.Exists(pattern + "/config.cfg") )
					continue;

				this.cbCopyFrom.Items.Add( pattern.Replace("patterns\\", "") );
			}

			// wyświetlanie nazw dostępnych formatów
			foreach( string format_name in NewPattern._format_names )
				this.cbPaperFormat.Items.Add( format_name );

			// zaznacz format: własny
			this.cbPaperFormat.SelectedIndex = 0;
		}

		// ------------------------------------------------------------- bCreate_Click --------------------------------
		
		private void bCreate_Click( object sender, EventArgs ev )
		{
			string locale_chars = "ĘÓŁŚĄŻŹĆŃęółśążźćń";

			// wyczyść ze zbędnych spacji
			this.tbPatternName.Text = this.tbPatternName.Text.Trim();
			Regex regex = new Regex( @"[ ]{2,}", RegexOptions.None );
			this.tbPatternName.Text = regex.Replace( this.tbPatternName.Text, " " );

			// brak nazwy wzoru...
			if( this.tbPatternName.Text == "" )
			{
				MessageBox.Show
				(
					this,
					"Musisz podać nazwę wzoru.",
					"Nowy wzór",
					MessageBoxButtons.OK,
					MessageBoxIcon.Error
				);
				return;
			}

			// przeskanuj pod kątem poprawności
			regex = new Regex( @"^[0-9a-zA-Z" + locale_chars + @" \-+_]+$" );
			if( !regex.IsMatch(this.tbPatternName.Text) )
			{
				MessageBox.Show
				(
					this,
					"W nazwie wzoru nie może zawierać znaków innych niż:\n" +
					"Znaki alfabetu, liczby, minus, plus, podkreślenie oraz spacje.",
					"Nowy wzór",
					MessageBoxButtons.OK,
					MessageBoxIcon.Error
				);
				return;
			}

			// wzór o tej nazwie już istnieje
			if( Directory.Exists("patterns/" + this.tbPatternName.Text) )
			{
				MessageBox.Show
				(
					this,
					"Wzór o podanej nazwie już istnieje.",
					"Nowy wzór",
					MessageBoxButtons.OK,
					MessageBoxIcon.Error
				);
				return;
			}

			// zerowe wymiary papieru...
			if( this.nHeight.Value == 0 || this.nWidth.Value == 0 )
			{
				MessageBox.Show
				(
					this,
					"Musisz podać wymiary papieru w milimetrach.",
					"Nowy wzór",
					MessageBoxButtons.OK,
					MessageBoxIcon.Error
				);
				return;
			}

			// tworzenie wzoru
			try
			{
				// utwórz katalogi
				Directory.CreateDirectory( "patterns/" + tbPatternName.Text );
				Directory.CreateDirectory( "patterns/" + tbPatternName.Text + "/images" );

				// utwórz pusty wzór
				PatternEditor.Create( this.tbPatternName.Text, (short)this.nWidth.Value, (short)this.nHeight.Value );
			}
			catch
			{
				// brak uprawnień?
				MessageBox.Show
				(
					this,
					"Nie można utworzyć wzoru w folderze głównym programu...\n" +
					"Sprawdź czy posiadasz odpowiednie uprawnienia do zapisywania danych w katalogu programu.",
					"Tworzenie wzoru",
					MessageBoxButtons.OK,
					MessageBoxIcon.Error
				);
				return;
			}

			// zapisz aktualną nazwę szablonu
			this._pattern_name = this.tbPatternName.Text;

			// zamknij formulatrz
			this.DialogResult = DialogResult.OK;
			this.Close();
		}

		// ------------------------------------------------------------- cbPaperFormat_SelectedIndexChanged -----------
		
		private void cbPaperFormat_SelectedIndexChanged( object sender, EventArgs ev )
		{
			// wybrano format papieru
			if( this.cbPaperFormat.SelectedIndex > 0 )
			{
				// uzupełnij wartości w polach numerycznych
				this.nWidth.Value  = NewPattern._format_dims[this.cbPaperFormat.SelectedIndex - 1, 0];
				this.nHeight.Value = NewPattern._format_dims[this.cbPaperFormat.SelectedIndex - 1, 1];

				// zablokuj pola
				this.nWidth.Enabled  = false;
				this.nHeight.Enabled = false;

				return;
			}

			// odblokuj pola numeryczne
			this.nWidth.Enabled  = true;
			this.nHeight.Enabled = true;
		}
	}
}
