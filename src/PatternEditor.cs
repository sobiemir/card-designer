using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Windows.Forms;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;

namespace CDesigner
{
	public struct PageDetails
	{
		public PageDetails( int pc, int pi ) { this.PrintColor = pc; this.PrintImage = pi; }

		public int PrintColor;
		public int PrintImage;
	};

	public struct DBPageFields
	{
		public bool[] ImageFromDB;
		public bool[] TextFromDB;
		public string[] Name;
		public int[] Column;
		public bool[] Preview;
		public int Fields;
	};

	class PatternEditor
	{
		public static string last_created = "";

		// ------------------------------------------------------------- Create ---------------------------------------
		
		public static void Create( string name, short width, short height )
		{
			// @TODO - dodać informacje o treści dynamicznej

            // utwórz plik konfiguracyjny
            FileStream file = new FileStream( "patterns/" + name + "/config.cfg", FileMode.OpenOrCreate );
            BinaryWriter writer = new BinaryWriter( file );

			/**
			 * Struktura wzoru:
			 * --------------------------------------------
			 * 2 | Szerokość
			 * 2 | Wysokość
			 * 1 | Ilość stron
			 * 1 | Treść dynamiczna
			 * ===================== LOOP =================
			 *   1 | Ilość kontrolek na stronie
			 *   1 | Obraz lub kolor tła ('i' lub 'c')
			 *   4 | Kolor strony
			 * +++++++++++++++++++++ FIELD LOOP +++++++++++
			 *     ? | Nazwa pola
			 *     2 | Pozycja X
			 *     2 | Pozycja Y
			 *     2 | Szerokość pola
			 *     2 | Wysokość pola
			 *     1 | Grubość ramki
			 *     4 | Kolor ramki
			 *     1 | Obraz lub kolor tła ('i' lub 'c')
			 *     4 | Kolor tła
			 *     4 | Kolor czcionki
			 *     ? | Nazwa czcionki
			 *     1 | Styl czcionki
			 *     4 | Rozmiar czcionki
			 *     1 | Położenie tekstu
			 *     1 | Margines wewnętrzny
			 *     1 | Tło z bazy danych
			 *     1 | Drukuj kolor
			 *     1 | Drukuj obraz
			 *     1 | Napis z bazy danych
			 *     1 | Drukuj tekst
			 *     1 | Drukuj ramkę
			 * ++++++++++++++++++++++++++++++++++++++++++++
			 *   1 | Drukuj kolor strony
			 *   1 | Drukuj obraz strony
			 * ============================================
			 * ? | @TODO - opcje wzoru
			 * --------------------------------------------
			**/

            writer.Write( (short)width );
            writer.Write( (short)height );
			writer.Write( (byte)1 );
			writer.Write( (byte)0 );
			writer.Write( (byte)0 );
			writer.Write( 'c' );
			writer.Write( SystemColors.Window.ToArgb() );
			writer.Write( (byte)0 );
			writer.Write( (byte)0 );

            // zamknij uchwyty
            writer.Close( );
            file.Close( );

			// zapisz nazwę ostatnio utworzonego wzoru
			PatternEditor.last_created = name;
		}

		// ------------------------------------------------------------- Open -----------------------------------------
		
		public static int Open( string name, ref Size size, Panel container )
		{
			FileStream file = new FileStream( "patterns/" + name + "/config.cfg", FileMode.OpenOrCreate );
			BinaryReader reader = new BinaryReader( file );

			// wczytaj dane z pliku
			size.Width = reader.ReadInt16( );
			size.Height	= reader.ReadInt16( );
			int pages = reader.ReadByte( );
			bool dynamic = reader.ReadByte( ) == 1;

			// wyczyść kontrolki
			container.Controls.Clear( );

			if( pages == 0 )
				return 0;

			// rysuj strony
			for( int x = 0; x < pages; ++x )
			{
				Panel panel = new Panel( );

				// ilość kontrolek na stronie
				int controls = (int)reader.ReadByte( );

				// obraz strony
				if( reader.ReadByte( ) == 'i' && File.Exists("patterns/" + name + "/images/page" + x + ".jpg") )
					using( Image image = Image.FromFile("patterns/" + name + "/images/page" + x + ".jpg") )
					{
						if( panel.BackgroundImage != null )
							panel.BackgroundImage.Dispose( );

						// skopiuj obraz do pamięci
						Image new_image = new Bitmap( image.Width, image.Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb );
						using( var canavas = Graphics.FromImage(new_image) )
							canavas.DrawImageUnscaled( image, 0, 0 );
						panel.BackgroundImage = new_image;

						image.Dispose( );
					}
				else
					panel.BackgroundImage = null;

				// kolor strony
				panel.BackColor = Color.FromArgb( reader.ReadInt32() );

				for( int y = 0; y < controls; ++y )
				{
					CDField label = new CDField( );

					label.Text = reader.ReadString( );

					// położenie pola
					label.DPILeft = reader.ReadInt16( );
					label.DPITop = reader.ReadInt16( );
					label.DPIWidth = reader.ReadInt16( );
					label.DPIHeight = reader.ReadInt16( );

					// wygląd
					label.DPIBorderSize = reader.ReadByte( );
					label.BorderColor = Color.FromArgb( reader.ReadInt32() );
					
					if( reader.ReadByte() == 'i' && File.Exists("patterns/" + name + "/images/field" + y + "_" + x + ".jpg") )
						using( Image image = Image.FromFile("patterns/" + name + "/images/field" + y + "_" + x + ".jpg") )
						{
							if( label.BackImage != null )
								label.BackImage.Dispose( );

							// skopiuj obraz do pamięci
							Image new_image = new Bitmap( image.Width, image.Height, PixelFormat.Format32bppArgb );
							using( var canavas = Graphics.FromImage(new_image) )
								canavas.DrawImageUnscaled( image, 0, 0 );
							label.BackImage = new_image;

							image.Dispose( );
						}
					else
						label.BackImage = null;

					label.BackColor = Color.FromArgb( reader.ReadInt32() );
					label.ForeColor = Color.FromArgb( reader.ReadInt32() );

					// czcionka
					FontFamily font_family;

					try { font_family = new FontFamily( reader.ReadString() ); }
					catch { font_family = new FontFamily( "Arial" ); }

					label.Font = new Font( font_family, 8.25f, (FontStyle)reader.ReadByte(), GraphicsUnit.Point );
					label.DPIFontSize = (double)reader.ReadSingle( );
					label.TextAlignment = (int)reader.ReadByte( );
					label.DPIPadding = (int)reader.ReadByte( );

					// informacje dodatkowe
					CustomLabelDetails ext;

					ext.ImageFromDB = reader.ReadByte( ) == 1;
					ext.PrintColor = reader.ReadByte( ) == 1;
					ext.PrintImage = reader.ReadByte( ) == 1;
					ext.TextFromDB = reader.ReadByte( ) == 1;
					ext.PrintText = reader.ReadByte( ) == 1;
					ext.PrintBorder = reader.ReadByte( ) == 1;
					label.Tag = ext;

					// dodaj do panelu
					panel.Controls.Add( label );
				}

				// informacje dodatkowe
				PageDetails tag = new PageDetails( );
				tag.PrintColor = (int)reader.ReadByte( );
				tag.PrintImage = (int)reader.ReadByte( );
				panel.Tag = tag;

				// dodaj panel do kontenera
				container.Controls.Add( panel );
			}

			// zamknij plik
			reader.Close( );
			file.Close( );

			return pages;
		}

		// ------------------------------------------------------------- Save -----------------------------------------
		
		public static void Save( string name, Size size, Panel container )
		{
			FileStream file = new FileStream( "patterns/" + name + "/config.cfg", FileMode.OpenOrCreate );
			BinaryWriter writer = new BinaryWriter( file );

			// podstawowe informacje
			writer.Write( (short)size.Width );
			writer.Write( (short)size.Height );
			writer.Write( (byte)container.Controls.Count );
			
			bool dynamic = false;

			// przeszukaj kontrolki
			foreach( Panel page in container.Controls )
				foreach( CDField field in page.Controls )
				{
					CustomLabelDetails details = (CustomLabelDetails)field.Tag;
 					if( details.TextFromDB || details.ImageFromDB )
					{
						dynamic = true;
						break;
					}
				}

			// treść dynamiczna
			if( dynamic )
				writer.Write( (byte)1 );
			else
				writer.Write( (byte)0 );

			// zapisz konfiguracje stron
			for( int x = 0; x < container.Controls.Count; ++x )
			{
				Panel panel = (Panel)container.Controls[x];
				
				// ilość pól
				writer.Write( (byte)panel.Controls.Count );

				// informacja o tle strony
				if( panel.BackgroundImage != null )
				{
					panel.BackgroundImage.Save( "patterns/" + name + "/images/" + "page" + x + ".jpg" );
					writer.Write('i');
				}
				else
					writer.Write('c');

				// kolor strony
				writer.Write( panel.BackColor.ToArgb() );

				for( int y = 0; y < panel.Controls.Count; ++y )
				{
					CDField label = (CDField)panel.Controls[y];

					// zapisz nazwę (tekst) pola
					writer.Write( label.Text );

					// położenie pola
					writer.Write( (short)label.DPILeft );
					writer.Write( (short)label.DPITop );
					writer.Write( (short)label.DPIWidth );
					writer.Write( (short)label.DPIHeight );

					// wygląd
					writer.Write( (byte)label.DPIBorderSize );
					writer.Write( label.BorderColor.ToArgb() );

					if( label.BackImage != null )
					{
						label.BackImage.Save( "patterns/" + name + "/images/" + "field" + y + "_" + x + ".jpg" );
						writer.Write('i');
					}
					else
						writer.Write('c');

					writer.Write( label.BackColor.ToArgb() );
					writer.Write( label.ForeColor.ToArgb() );

					// czcionka
					writer.Write( label.Font.Name );
					writer.Write( (byte)label.Font.Style );
					writer.Write( (float)label.DPIFontSize );
					writer.Write( (byte)label.TextAlignment );
					writer.Write( (byte)label.DPIPadding );

					// informacje dodatkowe
					CustomLabelDetails ext = (CustomLabelDetails)label.Tag;

					writer.Write( (byte)(ext.ImageFromDB ? 1 : 0) );
					writer.Write( (byte)(ext.PrintColor ? 1 : 0) );
					writer.Write( (byte)(ext.PrintImage ? 1 : 0) );
					writer.Write( (byte)(ext.TextFromDB ? 1 : 0) );
					writer.Write( (byte)(ext.PrintText ? 1 : 0) );
					writer.Write( (byte)(ext.PrintBorder ? 1 : 0) );
				}

				// zapisz dodatkowe informacje
				PageDetails tag = (PageDetails)panel.Tag;

				writer.Write( (byte)tag.PrintColor );
				writer.Write( (byte)tag.PrintImage );
			}
			
            // zamknij uchwyty
            writer.Close( );
            file.Close( );
		}

		// ------------------------------------------------------------- DrawPreview ----------------------------------
		
		public static void DrawPreview( string name, Size size, Panel container, double scale )
		{
			double dpi_px_scale = 3.938095238095238 * scale;
			int width = (int)((double)size.Width * dpi_px_scale),
				height = (int)((double)size.Height * dpi_px_scale);

			// rysuj strony
			for( int x = 0; x < container.Controls.Count; ++x )
			{
				Bitmap bmp = new Bitmap( width, height );
				Graphics gfx = Graphics.FromImage( bmp );
				Panel panel = (Panel)container.Controls[x];

				// obraz lub wypełnienie
				if( panel.BackgroundImage != null )
					gfx.DrawImage( panel.BackgroundImage, 0, 0, width, height );
				else
				{
					SolidBrush brush = new SolidBrush( panel.BackColor );
					gfx.FillRectangle( brush, 0, 0, width, height );
				}

				// rysuj kontrolki
				for( int y = 0; y < panel.Controls.Count; ++y )
				{
					CDField label = (CDField)panel.Controls[y];
					int label_width = (int)((double)label.DPIWidth * dpi_px_scale),
						label_height = (int)((double)label.DPIHeight * dpi_px_scale),
						label_left = (int)((double)label.DPILeft * dpi_px_scale),
						label_top = (int)((double)label.DPITop * dpi_px_scale);

					// koloruj lub rysuj obraz
					if( label.BackImage != null )
						gfx.DrawImage( label.BackImage, label_left, label_top, label_width, label_height );
					else
					{
						SolidBrush brush = new SolidBrush( label.BackColor );
						gfx.FillRectangle( brush, label_left, label_top, label_width, label_height );
					}

					// utwórz czcionkę z nowym rozmiarem
					Font font = new Font
					(
						label.Font.FontFamily,
						(float)(label.DPIFontSize * scale),
						label.Font.Style,
						GraphicsUnit.Point,
						label.Font.GdiCharSet,
						label.Font.GdiVerticalFont
					);

					SolidBrush label_brush = new SolidBrush( label.ForeColor );
					StringFormat format = new StringFormat( );

					// margines wewnętrzny
					int padding = (int)((double)label.DPIPadding * scale);
					RectangleF label_box = new RectangleF
					(
						label_left + padding,
						label_top + padding,
						label_width - padding * 2,
						label_height - padding * 2
					);

					// wykryj przyleganie tekstu
					switch( label.TextAlignment )
					{
					case 0:
						format.LineAlignment = StringAlignment.Near;
						format.Alignment = StringAlignment.Near;
					break;
					case 1:
						format.LineAlignment = StringAlignment.Near;
						format.Alignment = StringAlignment.Center;
					break;
					case 2:
						format.LineAlignment = StringAlignment.Near;
						format.Alignment = StringAlignment.Far;
					break;
					case 3:
						format.LineAlignment = StringAlignment.Center;
						format.Alignment = StringAlignment.Near;
					break;
					case 4:
						format.LineAlignment = StringAlignment.Center;
						format.Alignment = StringAlignment.Center;
					break;
					case 5:
						format.LineAlignment = StringAlignment.Center;
						format.Alignment = StringAlignment.Far;
					break;
					case 6:
						format.LineAlignment = StringAlignment.Far;
						format.Alignment = StringAlignment.Near;
					break;
					case 7:
						format.LineAlignment = StringAlignment.Far;
						format.Alignment = StringAlignment.Center;
					break;
					case 8:
						format.LineAlignment = StringAlignment.Far;
						format.Alignment = StringAlignment.Far;
					break;
					}
					// rysuj napis na polu
					gfx.DrawString( label.Text, font, label_brush, label_box, format );

					int border_size = (int)((double)label.DPIBorderSize * scale);
					Pen border_pen = new Pen( label.BorderColor );
					
					// wychodzi o 1px poza główny prostokąt, dlatego wartości należy zmniejszyć
					label_width--;
					label_height--;

					// rysuj ramkę
					for( int z = 0; z < border_size; z++ )
						gfx.DrawRectangle( border_pen, label_left + z, label_top + z, label_width - z * 2, label_height - z * 2 );
				}
				// zapisz stronę
				bmp.Save( "patterns/" + name + "/preview" + x + ".jpg" );
			}
		}

		// ------------------------------------------------------------- Copy -----------------------------------------
		
		public static void Copy( string pat1, string pat2 )
		{
		}

		// ------------------------------------------------------------- GetFields ------------------------------------
		
		public static int GetPagesFields( string pattern, ref DBPageFields[] fields )
		{
			FileStream file = new FileStream( "patterns/" + pattern + "/config.cfg", FileMode.OpenOrCreate );
			BinaryReader reader = new BinaryReader( file );

			// wysokość i szerokość...
			int itrash;
			itrash = reader.ReadInt16( );
			itrash = reader.ReadInt16( );
			int pages = reader.ReadByte( );
			bool dynamic = reader.ReadByte( ) == 1;

			if( pages == 0 )
				return 0;

			fields = new DBPageFields[pages];

			// lista stron
			for( int x = 0; x < pages; ++x )
			{
				// przydziel pamięć na dane
				fields[x] = new DBPageFields( );
				fields[x].Fields = (int)reader.ReadByte( );
				fields[x].Name = new string[fields[x].Fields];
				fields[x].Column = new int[fields[x].Fields];
				fields[x].ImageFromDB = new bool[fields[x].Fields];
				fields[x].TextFromDB = new bool[fields[x].Fields];
				fields[x].Preview = new bool[fields[x].Fields];

				// śmieci...
				itrash = reader.ReadByte( );
				itrash = reader.ReadInt32( );

				// lista kontrolek na stronie
				for( int y = 0; y < fields[x].Fields; ++y )
				{
					// nazwa kontrolki
					fields[x].Name[y] = reader.ReadString( );
					fields[x].Column[y] = -1;
					fields[x].Preview[y] = false;

					// śmieci
					itrash = reader.ReadInt16( );
					itrash = reader.ReadInt16( );
					itrash = reader.ReadInt16( );
					itrash = reader.ReadInt16( );
					itrash = reader.ReadByte( );
					itrash = reader.ReadInt32( );
					itrash = reader.ReadByte( );
					itrash = reader.ReadInt32( );
					itrash = reader.ReadInt32( );

					string strash = reader.ReadString( );
					itrash = reader.ReadByte( );
					float ftrash = reader.ReadSingle( );
					itrash = reader.ReadByte( );
					itrash = reader.ReadByte( );

					// informacje o treści dynamicznej
					fields[x].ImageFromDB[y] = reader.ReadByte( ) == 1;
					itrash = reader.ReadByte( );
					itrash = reader.ReadByte( );
					fields[x].TextFromDB[y] = reader.ReadByte( ) == 1;
					itrash = reader.ReadByte( );
					itrash = reader.ReadByte( );
				}

				// śmieci
				itrash = reader.ReadByte( );
				itrash = reader.ReadByte( );
			}

			// zamknij uchwyty do pliku
			reader.Close( );
			file.Close( );

			return pages;
		}
	}
}
