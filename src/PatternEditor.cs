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


	public class PatternEditor
	{
		private static string _last_created  = "";
		private static double _pixel_per_dpi = 3.938095238095238;


		// ------------------------------------------------------------- LastCreated ----------------------------------
		
		public static string LastCreated
		{
			get { return PatternEditor._last_created; }
		}

		// ------------------------------------------------------------- CreatePattern --------------------------------
		
		public static void Create( string pattern, short width, short height )
		{
			FileStream file = new FileStream( "patterns/" + pattern + "/config.cfg", FileMode.OpenOrCreate );
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
			 *   1 | Użycie obrazu tła
			 *   4 | Kolor strony
			 * +++++++++++++++++++++ FIELD LOOP +++++++++++
			 *     ? | Nazwa pola
			 *     2 | Pozycja X
			 *     2 | Pozycja Y
			 *     2 | Szerokość pola
			 *     2 | Wysokość pola
			 *     1 | Grubość ramki
			 *     4 | Kolor ramki
			 *     1 | Użycie obrazu tła
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
			writer.Write( (byte)0 );
			writer.Write( SystemColors.Window.ToArgb() );
			writer.Write( (byte)0 );
			writer.Write( (byte)0 );

            // zamknij uchwyty
            writer.Close( );
            file.Close( );
		}

		// ------------------------------------------------------------- LoadFromFile ---------------------------------
		
		public static PatternData ReadPattern( string pattern, bool only_header = false )
		{
			FileStream   file   = new FileStream( "patterns/" + pattern + "/config.cfg", FileMode.OpenOrCreate );
			BinaryReader reader = new BinaryReader( file );
			PatternData  data   = new PatternData();

			// odczytaj opcje podstawowe wzoru
			data.name    = pattern;
			data.size    = new Size( reader.ReadInt16(), reader.ReadInt16() );
			data.pages   = reader.ReadByte();
			data.dynamic = reader.ReadByte() == 1;

			// tylko nagłówek
			if( only_header )
			{
				data.page = null;

				reader.Close();
				file.Close();

				return data;
			}
			else
				data.page = new PageData[data.pages];

			PageData  page_data;
			FieldData field_data;

			// odczytaj dane strony
			for( int x = 0; x < data.pages; ++x )
			{
				page_data = new PageData();
			
				// dane podstawowe
				page_data.fields     = reader.ReadByte();
				page_data.image      = reader.ReadByte() == 1;
				page_data.color      = Color.FromArgb( reader.ReadInt32() );
				page_data.field      = new FieldData[page_data.fields];
				page_data.image_path = null;

				// odczytaj dane kontrolki
				for( int y = 0; y < page_data.fields; ++y )
				{
					field_data = new FieldData();

					// dane podstawowe
					field_data.name         = reader.ReadString();
					field_data.bounds       = new Rectangle( reader.ReadInt16(), reader.ReadInt16(), reader.ReadInt16(), reader.ReadInt16() );
					field_data.border_size  = reader.ReadByte();
					field_data.border_color = Color.FromArgb( reader.ReadInt32() );
					field_data.image        = reader.ReadByte() == 1;
					field_data.image_path   = null;
					field_data.color        = Color.FromArgb( reader.ReadInt32() );
					field_data.font_color   = Color.FromArgb( reader.ReadInt32() );
					field_data.font_name    = reader.ReadString();
					field_data.font_style   = (FontStyle)reader.ReadByte();
					field_data.font_size    = reader.ReadSingle();
					field_data.text_align   = (ContentAlignment)reader.ReadByte();
					field_data.padding      = new Padding( reader.ReadByte() );

					// dane dodatkowe
					field_data.extra = new FieldExtraData();
					field_data.extra.image_from_db = reader.ReadByte() == 1;
					field_data.extra.print_color   = reader.ReadByte() == 1;
					field_data.extra.print_image   = reader.ReadByte() == 1;
					field_data.extra.text_from_db  = reader.ReadByte() == 1;
					field_data.extra.print_text    = reader.ReadByte() == 1;
					field_data.extra.print_border  = reader.ReadByte() == 1;
					field_data.extra.column        = -1;
					
					page_data.field[y] = field_data;
				}

				// dane dodatkowe
				page_data.extra = new PageExtraData();
				page_data.extra.print_color = reader.ReadByte() == 1;
				page_data.extra.print_image = reader.ReadByte() == 1;

				data.page[x] = page_data;
			}

			// zamknij strumienie
			reader.Close();
			file.Close();

			return data;
		}

		// ------------------------------------------------------------- DrawPreview ----------------------------------
		
		public static void DrawPreview( PatternData data, Panel panel, double scale )
		{
			// wyczyść strony i pola
			panel.Controls.Clear();
			GC.Collect();

			// brak stron...
			if( data.pages == 0 )
				return;

			double dpi_pxs = scale * PatternEditor._pixel_per_dpi;
			Size   pp_size = new Size
			(
				(int)((double)data.size.Width * dpi_pxs),
				(int)((double)data.size.Height * dpi_pxs)
			);

			PageData  page_data;
			FieldData field_data;

			// dodawaj strony
			for( int x = 0; x < data.pages; ++x )
			{
				Panel page = new Panel();
				page_data = data.page[x];

				// tło strony
				if( page_data.image && File.Exists("patterns/" + data.name + "/images/page" + x + ".jpg" ) )
					using( Image image = Image.FromFile("patterns/" + data.name + "/images/page" + x + ".jpg" ) )
					{
						Image new_image = new Bitmap( image.Width, image.Height, PixelFormat.Format32bppArgb );
						using( Graphics canvas = Graphics.FromImage(new_image) )
							canvas.DrawImageUnscaled( image, 0, 0 );

						page.BackgroundImage = new_image;
						image.Dispose();
					}
				else
					page.BackgroundImage = null;

				// kolor strony
				page.BackColor             = page_data.color;
				page.BackgroundImageLayout = ImageLayout.Stretch;

				// skalowanie
				page.Location = new Point(0);
				page.Margin   = new Padding( 0, 0, 0, 0 );
				page.Size     = pp_size;

				if( x != 0 )
					page.Hide();

				// dodawaj pola
				for( int y = 0; y < page_data.fields; ++y )
				{
					PageField field = new PageField();
					field_data = page_data.field[y];

					// wygląd pola
					field.Text          = field_data.name;
					field.DPIBounds     = field_data.bounds;
					field.DPIBorderSize = field_data.border_size;
					field.BorderColor   = field_data.border_color;

					// tło pola
					if( field_data.image && File.Exists("patterns/" + data.name + "/images/field" + y + "_" + x + ".jpg") )
						using( Image image = Image.FromFile("patterns/" + data.name + "/images/field" + y + "_" + x + ".jpg" ) )
						{
							Image new_image = new Bitmap( image.Width, image.Height, PixelFormat.Format32bppArgb );
							using( Graphics canvas = Graphics.FromImage(new_image) )
								canvas.DrawImageUnscaled( image, 0, 0 );

							field.BackImage = new_image;
							field.BackImagePath = "patterns/" + data.name + "/images/field" + y + "_" + x + ".jpg";

							image.Dispose();
						}
					else
					{
						field.BackImage = null;
						field.BackImagePath = "";
					}

					// kolory
					field.BackColor = field_data.color;
					field.ForeColor = field_data.font_color;

					// czcionka
					FontFamily font_family;
					try   { font_family = new FontFamily( field_data.font_name ); }
					catch { font_family = new FontFamily( "Arial" ); }

					field.Font        = new Font( font_family, 8.25f, field_data.font_style, GraphicsUnit.Point );
					field.DPIFontSize = field_data.font_size;
					field.TextAlign   = field_data.text_align;
					field.DPIPadding  = field_data.padding.All;
					
					// skalowanie
					field.DPIScale = scale;
					field.Margin   = new Padding(0);

					// dodatkowe informacje
					field.Tag = field_data;

					page.Controls.Add( field );
				}

				// dodatkowe informacje
				page.Tag = page_data;

				panel.Controls.Add( page );
			}
		}

		// ------------------------------------------------------------- DrawSketch -----------------------------------
		
		public static void DrawSketch( PatternData data, Panel panel, double scale )
		{
			// wyczyść strony i pola
			panel.Controls.Clear();
			GC.Collect();

			// brak stron...
			if( data.pages == 0 )
				return;

			double dpi_pxs = scale * PatternEditor._pixel_per_dpi;
			Size   pp_size = new Size
			(
				(int)((double)data.size.Width * dpi_pxs),
				(int)((double)data.size.Height * dpi_pxs)
			);

			PageData  page_data;
			FieldData field_data;

			// dodawaj strony
			for( int x = 0; x < data.pages; ++x )
			{
				Panel page = new Panel();
				page_data = data.page[x];

				// tło strony
				if( page_data.extra.print_image && page_data.image && File.Exists("patterns/" + data.name + "/images/page" + x + ".jpg" ) )
					using( Image image = Image.FromFile("patterns/" + data.name + "/images/page" + x + ".jpg" ) )
					{
						Image new_image = new Bitmap( image.Width, image.Height, PixelFormat.Format32bppArgb );
						using( Graphics canvas = Graphics.FromImage(new_image) )
							canvas.DrawImageUnscaled( image, 0, 0 );

						page.BackgroundImage = new_image;
						image.Dispose();
					}
				else
					page.BackgroundImage = null;

				// kolor strony
				page.BackColor             = (page_data.extra.print_color) ? page_data.color : SystemColors.Window;
				page.BackgroundImageLayout = ImageLayout.Stretch;

				// skalowanie
				page.Location = new Point(0);
				page.Margin   = new Padding( 0, 0, 0, 0 );
				page.Size     = pp_size;

				if( x != 0 )
					page.Hide();

				// dodawaj pola
				for( int y = 0; y < page_data.fields; ++y )
				{
					PageField field = new PageField();
					field_data = page_data.field[y];

					// wygląd pola
					field.Text          = field_data.extra.print_text ? field_data.name : "";
					field.DPIBounds     = field_data.bounds;
					field.DPIBorderSize = field_data.extra.print_border ? field_data.border_size : 0;
					field.BorderColor   = field_data.border_color;

					// tło pola
					if( field_data.extra.print_image && field_data.image && File.Exists("patterns/" + data.name + "/images/field" + y + "_" + x + ".jpg") )
						using( Image image = Image.FromFile("patterns/" + data.name + "/images/field" + y + "_" + x + ".jpg" ) )
						{
							Image new_image = new Bitmap( image.Width, image.Height, PixelFormat.Format32bppArgb );
							using( Graphics canvas = Graphics.FromImage(new_image) )
								canvas.DrawImageUnscaled( image, 0, 0 );

							field.BackImage = new_image;
							field.BackImagePath = "patterns/" + data.name + "/images/field" + y + "_" + x + ".jpg";

							image.Dispose();
						}
					else
					{
						field.BackImage = null;
						field.BackImagePath = "";
					}

					// kolory
					field.BackColor = field_data.extra.print_color ? field_data.color : Color.Transparent;
					field.ForeColor = field_data.font_color;

					// czcionka
					FontFamily font_family;
					try   { font_family = new FontFamily( field_data.font_name ); }
					catch { font_family = new FontFamily( "Arial" ); }

					field.Font        = new Font( font_family, 8.25f, field_data.font_style, GraphicsUnit.Point );
					field.DPIFontSize = field_data.font_size;
					field.TextAlign   = field_data.text_align;
					field.DPIPadding  = field_data.padding.All;
					
					// skalowanie
					field.DPIScale = scale;
					field.Margin   = new Padding(0);

					// dodatkowe informacje
					field.Tag = field_data;

					page.Controls.Add( field );
				}

				// dodatkowe informacje
				page.Tag = page_data;

				panel.Controls.Add( page );
			}
		}

		// ------------------------------------------------------------- DrawRow --------------------------------------
		
		public static void DrawRow( Panel panel, DataContent data_content, int row )
		{
			Panel     page;
			PageField field;

			// zmień wartości pól
			for( int x = 0; x < panel.Controls.Count; ++x )
			{
				page = (Panel)panel.Controls[x];
				for( int y = 0; y < page.Controls.Count; ++y )
				{
					field = (PageField)page.Controls[y];
					int replace = ((FieldData)field.Tag).extra.column;

					// sprawdź czy pola można zmienić i czy wartości są im przypisane
					if( ((FieldData)field.Tag).extra.text_from_db && replace != -1 )
						field.Text = data_content.row[row,replace];
					else if( ((FieldData)field.Tag).extra.image_from_db && replace != -1 )
					{
						// @TODO obrazek z bazy danych...
					}
				}
			}
		}

		// ------------------------------------------------------------- ChangeScale ----------------------------------
		
		public static void ChangeScale( PatternData data, Panel panel, double scale )
		{
			// oblicz wymiary strony
			double dpi_pxs = scale * PatternEditor._pixel_per_dpi;
			Size   pp_size = new Size
			(
				(int)((double)data.size.Width * dpi_pxs),
				(int)((double)data.size.Height * dpi_pxs)
			);

			Panel     page;
			PageField field;

			// zmieniaj skale stron i pól
			for( int x = 0; x < panel.Controls.Count; ++x )
			{
				page = (Panel)panel.Controls[x];
				page.Size = pp_size;

				for( int y = 0; y < page.Controls.Count; ++y )
				{
					field = (PageField)page.Controls[y];
					field.DPIScale = scale;
				}
			}
		}

		// ------------------------------------------------------------- GeneratePreview ------------------------------
		
		public static void GeneratePreview( PatternData data, Panel panel, double scale )
		{
			double dpi_pxs = PatternEditor._pixel_per_dpi * scale;
			int    width   = (int)((double)data.size.Width * dpi_pxs);
			int    height  = (int)((double)data.size.Height * dpi_pxs);

			// rysuj strony
			for( int x = 0; x < data.pages; ++x )
			{
				Bitmap   bmp  = new Bitmap( width, height );
				Graphics gfx  = Graphics.FromImage( bmp );
				Panel    page = (Panel)panel.Controls[x];

				// obraz lub wypełnienie
				if( page.BackgroundImage != null )
					gfx.DrawImage( page.BackgroundImage, 0, 0, width, height );
				else
				{
					SolidBrush brush = new SolidBrush( page.BackColor );
					gfx.FillRectangle( brush, 0, 0, width, height );
				}

				// rysuj pola
				for( int y = 0; y < page.Controls.Count; ++y )
				{
					PageField field  = (PageField)page.Controls[y];
					Rectangle bounds = field.DPIBounds;
					
					bounds.X      = (int)((double)bounds.X * dpi_pxs);
					bounds.Y      = (int)((double)bounds.Y * dpi_pxs);
					bounds.Width  = (int)((double)bounds.Width * dpi_pxs);
					bounds.Height = (int)((double)bounds.Height * dpi_pxs);

					// koloruj lub rysuj obraz
					if( field.BackImage != null )
						gfx.DrawImage( field.BackImage, bounds );
					else
					{
						SolidBrush brush = new SolidBrush( field.BackColor );
						gfx.FillRectangle( brush, bounds );
					}

					// utwórz czcionkę
					Font font = new Font
					(
						field.Font.FontFamily,
						(float)(field.DPIFontSize * scale),
						field.Font.Style,
						GraphicsUnit.Point,
						field.Font.GdiCharSet,
						field.Font.GdiVerticalFont
					);

					SolidBrush   lbrush = new SolidBrush( field.ForeColor );
					StringFormat format = new StringFormat();

					// margines wewnętrzny
					int        pad = (int)((double)field.DPIPadding * scale);
					RectangleF box = new RectangleF( bounds.X + pad, bounds.Y + pad, bounds.Width - pad * 2, bounds.Height - pad * 2 );

					// rozmieszczenie tekstu
					if( ((int)field.TextAlign & 0x111) != 0 )
						format.Alignment = StringAlignment.Near;
					else if( ((int)field.TextAlign & 0x222) != 0 )
						format.Alignment = StringAlignment.Center;
					else
						format.Alignment = StringAlignment.Far;

					if( ((int)field.TextAlign & 0x7) != 0 )
						format.LineAlignment = StringAlignment.Near;
					else if( ((int)field.TextAlign & 0x70) != 0 )
						format.LineAlignment = StringAlignment.Center;
					else
						format.LineAlignment = StringAlignment.Far;

					// rysuj tekst
					gfx.DrawString( field.Text, font, lbrush, box, format );

					int border_size = (int)((double)field.DPIBorderSize * scale);
					Pen border_pen  = new Pen( field.BorderColor );

					// wartości są zmniejszane, bo ramka wychodzi 1px poza prostokąt
					bounds.Width--;
					bounds.Height--;

					// rysuj ramkę
					for( int z = 0; z < border_size; ++z )
						gfx.DrawRectangle( border_pen, bounds.X + z, bounds.Y + z, bounds.Width - z * 2, bounds.Height - z * 2 );
				}

				// zapisz do pliku
				bmp.Save( "patterns/" + data.name + "/preview" + x + ".jpg" );
			}
		}

		// ------------------------------------------------------------- Save -----------------------------------------
		
		public static void Save( PatternData data, Panel panel )
		{
			FileStream   file   = new FileStream( "patterns/" + data.name + "/config.cfg", FileMode.OpenOrCreate );
			BinaryWriter writer = new BinaryWriter( file );

			//writer.Write( (short)data.size.Width );
			//writer.Write( (short)data.size.Height );
			//writer.Write( (byte)panel.Controls.Count );

			writer.Close();
			file.Close();
			/*
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
		*/
		}
	}
}
