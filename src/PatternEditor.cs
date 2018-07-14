using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Windows.Forms;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using PdfSharp.Pdf;
using PdfSharp.Drawing;
using PdfSharp.Drawing.Layout;

namespace CDesigner
{
	public struct PageDetails
	{
		public PageDetails( int pc, int pi ) { this.PrintColor = pc; this.PrintImage = pi; }

		public int PrintColor;
		public int PrintImage;
	}

	public struct DBPageFields
	{
		public bool[] ImageFromDB;
		public bool[] TextFromDB;
		public string[] Name;
		public int[] Column;
		public bool[] Preview;
		public int Fields;
	}


	public class PatternEditor
	{
		private static string _last_created  = "";
		private static double _pixel_per_dpi = 3.93714927048264;


		// ------------------------------------------------------------- LastCreated ----------------------------------
		
		public static string LastCreated
		{
			get { return PatternEditor._last_created; }
		}

		// ------------------------------------------------------------- CreatePattern --------------------------------
		
		public static void Create( string pattern, short width, short height )
		{
			FileStream   file   = new FileStream( "patterns/" + pattern + "/config.cfg", FileMode.OpenOrCreate );
			BinaryWriter writer = new BinaryWriter( file );

			/**
			 * Struktura wzoru:
			 * 5 | CDCFG
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
			 *     4 | Pozycja X
			 *     4 | Pozycja Y
			 *     4 | Szerokość pola
			 *     4 | Wysokość pola
			 *     4 | Grubość ramki
			 *     4 | Kolor ramki
			 *     1 | Użycie obrazu tła
			 *     4 | Kolor tła
			 *     4 | Kolor czcionki
			 *     ? | Nazwa czcionki
			 *     1 | Styl czcionki
			 *     4 | Rozmiar czcionki
			 *     4 | Położenie tekstu
			 *     1 | Transformacja tekstu
			 *     1 | Margines tekstu
			 *     4 | Margines lewy-prawy
			 *     4 | Margines górny-dolny
			 *     4 | Margines wewnętrzny
			 *     1 | Tło z bazy danych
			 *     1 | Drukuj kolor
			 *     1 | Drukuj obraz
			 *     1 | Napis z bazy danych
			 *     1 | Drukuj tekst
			 *     1 | Drukuj ramkę
			 *     4 | Punkt zaczepienia
			 * ++++++++++++++++++++++++++++++++++++++++++++
			 *   1 | Drukuj kolor strony
			 *   1 | Drukuj obraz strony
			 * ============================================
			 * --------------------------------------------
			**/

			// ciąg rozpoznawczy
			byte[] text = new byte[5] { (byte)'C', (byte)'D', (byte)'C', (byte)'F', (byte)'G' };
			writer.Write( text, 0, 5 );

            writer.Write( width );
            writer.Write( height );
			writer.Write( (byte)1 );
			writer.Write( false );
			writer.Write( (byte)0 );
			writer.Write( false );
			writer.Write( SystemColors.Window.ToArgb() );
			writer.Write( false );
			writer.Write( false );

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

			// ciąg początkowy - rozpoznawczy CDCFG
			byte[] bytes = reader.ReadBytes( 5 );
			if( bytes[0] != 'C' || bytes[1] != 'D' || bytes[2] != 'C' || bytes[3] != 'F' || bytes[4] != 'G' )
				return data;

			// odczytaj opcje podstawowe wzoru
			data.name    = pattern;
			data.size    = new Size( reader.ReadInt16(), reader.ReadInt16() );
			data.pages   = reader.ReadByte();
			data.dynamic = reader.ReadBoolean();

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
				page_data.image      = reader.ReadBoolean();
				page_data.color      = Color.FromArgb( reader.ReadInt32() );
				page_data.field      = new FieldData[page_data.fields];

				// odczytaj dane kontrolki
				for( int y = 0; y < page_data.fields; ++y )
				{
					field_data = new FieldData();

					// dane podstawowe
					field_data.name            = reader.ReadString();
					field_data.bounds          = new RectangleF( reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle() );
					field_data.border_size     = reader.ReadSingle();
					field_data.border_color    = Color.FromArgb( reader.ReadInt32() );
					field_data.image           = reader.ReadBoolean();
					field_data.image_path      = null;
					field_data.color           = Color.FromArgb( reader.ReadInt32() );
					field_data.font_color      = Color.FromArgb( reader.ReadInt32() );
					field_data.font_name       = reader.ReadString();
					field_data.font_style      = (FontStyle)reader.ReadByte();
					field_data.font_size       = reader.ReadSingle();
					field_data.text_align      = (ContentAlignment)reader.ReadInt32();
					field_data.text_transform  = reader.ReadByte();
					field_data.text_add_margin = reader.ReadBoolean();
					field_data.text_leftpad    = reader.ReadSingle();
					field_data.text_toppad     = reader.ReadSingle();
					field_data.padding         = reader.ReadSingle();

					// dane dodatkowe
					field_data.extra = new FieldExtraData();
					field_data.extra.image_from_db = reader.ReadBoolean();
					field_data.extra.print_color   = reader.ReadBoolean();
					field_data.extra.print_image   = reader.ReadBoolean();
					field_data.extra.text_from_db  = reader.ReadBoolean();
					field_data.extra.print_text    = reader.ReadBoolean();
					field_data.extra.print_border  = reader.ReadBoolean();
					field_data.extra.pos_align     = reader.ReadInt32();
					field_data.extra.column        = -1;
					
					page_data.field[y] = field_data;
				}

				// informacje dodatkowe
				page_data.extra = new PageExtraData();
				page_data.extra.print_color = reader.ReadBoolean();
				page_data.extra.print_image = reader.ReadBoolean();
				page_data.extra.image_path  = null;

				data.page[x] = page_data;
			}

			// zamknij strumienie
			reader.Close();
			file.Close();

			return data;
		}

		// ------------------------------------------------------------- GetDPIPixelScale -----------------------------
		
		public static Size GetDPIPageSize( Size size, double scale )
		{
			double dpi_pxs = scale * PatternEditor._pixel_per_dpi;
			return new Size( Convert.ToInt32(size.Width * dpi_pxs), Convert.ToInt32(size.Height * dpi_pxs) );
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
				Convert.ToInt32((double)data.size.Width * dpi_pxs),
				Convert.ToInt32((double)data.size.Height * dpi_pxs)
			);

			PageData  page_data;
			FieldData field_data;

			// dodawaj strony
			for( int x = 0; x < data.pages; ++x )
			{
				AlignedPage page = new AlignedPage();
				page.Align = 1;

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

					// granice rodzica
					field.SetParentBounds( data.size.Width, data.size.Height );

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

					field.Font            = new Font( font_family, 8.25f, field_data.font_style, GraphicsUnit.World );
					field.DPIFontSize     = field_data.font_size;
					field.TextAlign       = field_data.text_align;
					field.TextTransform   = field_data.text_transform;
					field.DPIPadding      = field_data.padding;
					field.TextMargin      = new PointF( field_data.text_leftpad, field_data.text_toppad );
					field.ApplyTextMargin = field_data.text_add_margin;
					
					// skalowanie
					field.DPIScale = scale;
					field.Margin   = new Padding(0);

					// dodatkowe informacje
					field.Tag = field_data.extra;

					page.Controls.Add( field );
				}

				// dodatkowe informacje
				page.Tag = page_data.extra;

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
				Convert.ToInt32((double)data.size.Width * dpi_pxs),
				Convert.ToInt32((double)data.size.Height * dpi_pxs)
			);

			PageData  page_data;
			FieldData field_data;

			// dodawaj strony
			for( int x = 0; x < data.pages; ++x )
			{
				AlignedPage page = new AlignedPage();
				page.Align = 1;

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

					// granice rodzica
					field.SetParentBounds( data.size.Width, data.size.Height );

					// tło pola
					if( field_data.extra.print_image && field_data.image && File.Exists("patterns/" + data.name + "/images/field" + y + "_" + x + ".jpg") )
						using( Image image = Image.FromFile("patterns/" + data.name + "/images/field" + y + "_" + x + ".jpg" ) )
						{
							Image new_image = new Bitmap( image.Width, image.Height, PixelFormat.Format32bppArgb );
							using( Graphics canvas = Graphics.FromImage(new_image) )
								canvas.DrawImage( image, 0, 0 );

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

					field.Font            = new Font( font_family, 8.25f, field_data.font_style, GraphicsUnit.Point );
					field.DPIFontSize     = field_data.font_size;
					field.TextAlign       = field_data.text_align;
					field.TextTransform   = field_data.text_transform;
					field.DPIPadding      = field_data.padding;
					field.TextMargin      = new PointF( field_data.text_leftpad, field_data.text_toppad );
					field.ApplyTextMargin = field_data.text_add_margin;
					
					// skalowanie
					field.DPIScale = scale;
					field.Margin   = new Padding(0);

					// dodatkowe informacje
					field.Tag = field_data.extra;

					page.Controls.Add( field );
				}

				// dodatkowe informacje
				page.Tag = page_data.extra;

				panel.Controls.Add( page );
			}
		}

		// ------------------------------------------------------------- DrawRow --------------------------------------
		
		// @TODO
		public static void DrawRow( Panel panel, DataContent data_content, int row )
		{
			AlignedPage page;
			PageField   field;

			// zmień wartości pól
			for( int x = 0; x < panel.Controls.Count; ++x )
			{
				page = (AlignedPage)panel.Controls[x];

				for( int y = 0; y < page.Controls.Count; ++y )
				{
					field = (PageField)page.Controls[y];
					int replace = ((FieldExtraData)field.Tag).column;

					// sprawdź czy pola można zmienić i czy wartości są im przypisane
					if( ((FieldExtraData)field.Tag).text_from_db && replace != -1 )
						field.Text = data_content.row[row,replace];
					else if( ((FieldExtraData)field.Tag).image_from_db && replace != -1 )
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
				Convert.ToInt32((double)data.size.Width * dpi_pxs),
				Convert.ToInt32((double)data.size.Height * dpi_pxs)
			);

			AlignedPage page;
			PageField   field;

			// zmieniaj skale stron i pól
			for( int x = 0; x < panel.Controls.Count; ++x )
			{
				page = (AlignedPage)panel.Controls[x];
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
			int    width   = Convert.ToInt32((double)data.size.Width * dpi_pxs);
			int    height  = Convert.ToInt32((double)data.size.Height * dpi_pxs);

			// rysuj strony
			for( int x = 0; x < data.pages; ++x )
			{
				Bitmap      bmp  = new Bitmap( width, height );
				Graphics    gfx  = Graphics.FromImage( bmp );
				AlignedPage page = (AlignedPage)panel.Controls[x];

				// obraz lub wypełnienie
				if( page.BackgroundImage != null )
					gfx.DrawImage( page.BackgroundImage, 0, 0, width, height );
				else
				{
					SolidBrush brush = new SolidBrush( page.BackColor );
					gfx.FillRectangle( brush, 0, 0, width, height );
				}

				// @TODO - TextTransform
				// rysuj pola
				for( int y = 0; y < page.Controls.Count; ++y )
				{
					PageField  field  = (PageField)page.Controls[y];
					RectangleF bounds = field.DPIBounds;
					
					bounds.X      = (float)((double)bounds.X * dpi_pxs);
					bounds.Y      = (float)((double)bounds.Y * dpi_pxs);
					bounds.Width  = (float)((double)bounds.Width * dpi_pxs);
					bounds.Height = (float)((double)bounds.Height * dpi_pxs);

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
					int        pad = Convert.ToInt32((double)field.DPIPadding * dpi_pxs);
					RectangleF box = new RectangleF( bounds.X + pad, bounds.Y + pad, bounds.Width - pad * 2, bounds.Height - pad * 2 );

					// powięsz prostokąt dla czcionki
					box.Height++;
					box.Width++;

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

					gfx.DrawString( field.Text, font, lbrush, box, format );
					//TextRenderer.DrawText( gfx, fiedl.Text, font, box, Color.Aqua, Color.Transparent, 

					int border_size = Convert.ToInt32((double)field.DPIBorderSize * dpi_pxs);
					Pen border_pen  = new Pen( field.BorderColor );

					if( field.DPIBorderSize > 0.0 && border_size == 0 )
						border_size = 1;

					// rysuj ramkę (x i y liczony jest od lewego górnego rogu, dlatego odejmuje się 1)
					bounds.Width--;
					bounds.Height--;

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
			FileStream   file   = new FileStream( "patterns/" + data.name + "/_config.cfg", FileMode.OpenOrCreate );
			BinaryWriter writer = new BinaryWriter( file );

			byte[] text = new byte[5] { (byte)'C', (byte)'D', (byte)'C', (byte)'F', (byte)'G' };
			writer.Write( text, 0, 5 );

			writer.Write( (short)data.size.Width );
			writer.Write( (short)data.size.Height );
			writer.Write( (byte)panel.Controls.Count );

			bool   dynamic = false;
			string pattern = data.name;
			
			foreach( Panel page in panel.Controls )
				foreach( PageField field in page.Controls )
				{
					FieldExtraData field_extra = (FieldExtraData)field.Tag;
					if( field_extra.text_from_db || field_extra.image_from_db )
					{
						dynamic = true;
						break;
					}
				}
			writer.Write( dynamic );
			
			// zapisz konfiguracje stron
			for( int x = 0; x < panel.Controls.Count; ++x )
			{
				AlignedPage page = (AlignedPage)panel.Controls[x];

				// obraz strony
				writer.Write( (byte)page.Controls.Count );
				if( page.BackgroundImage != null )
				{
					page.BackgroundImage.Save( "patterns/" + pattern + "/images/page" + x + ".jpg" );
					writer.Write( true );
				}
				else
					writer.Write( false );

				writer.Write( page.BackColor.ToArgb() );

				// zapisz konfiguracje pól na stronie
				for( int y = 0; y < page.Controls.Count; ++y )
				{
					PageField field = (PageField)page.Controls[y];

					writer.Write( field.OriginalText );
					writer.Write( field.DPIBounds.X );
					writer.Write( field.DPIBounds.Y );
					writer.Write( field.DPIBounds.Width );
					writer.Write( field.DPIBounds.Height );

					writer.Write( field.DPIBorderSize );
					writer.Write( field.BorderColor.ToArgb() );

					// obraz podglądu lub statyczny
					if( field.BackImage != null )
					{
						field.BackImage.Save( "patterns/" + pattern + "/images/field" + y + "_" + x + ".jpg" );
						writer.Write( true );
					}
					else
						writer.Write( false );

					writer.Write( field.BackColor.ToArgb() );
					writer.Write( field.ForeColor.ToArgb() );

					// czcionka i tekst
					writer.Write( field.Font.Name );
					writer.Write( (byte)field.Font.Style );
					writer.Write( (float)field.DPIFontSize );
					writer.Write( (int)field.TextAlign );
					writer.Write( (byte)field.TextTransform );
					writer.Write( field.ApplyTextMargin );
					writer.Write( field.TextMargin.X );
					writer.Write( field.TextMargin.Y );
					writer.Write( field.DPIPadding );

					FieldExtraData field_extra = (FieldExtraData)field.Tag;

					// informacje dodatkowe pola
					writer.Write( field_extra.image_from_db );
					writer.Write( field_extra.print_color );
					writer.Write( field_extra.print_image );
					writer.Write( field_extra.text_from_db );
					writer.Write( field_extra.print_text );
					writer.Write( field_extra.print_border );
					writer.Write( (int)field_extra.pos_align );
				}

				PageExtraData page_extra = (PageExtraData)page.Tag;

				// informacje dodatkowe strony
				writer.Write( page_extra.print_color );
				writer.Write( page_extra.print_image );
			}

			writer.Close();
			file.Close();

			// usuń stary plik konfiguracyjny
			if( File.Exists("patterns/" + data.name + "/config.cfg") )
				File.Delete( "patterns/" + data.name + "/config.cfg" );

			// przenieś (zmień nazwę) nowego pliku konfiguracyjnego
			File.Move( "patterns/" + data.name + "/_config.cfg", "patterns/" + data.name + "/config.cfg" );
		}

		// ------------------------------------------------------------- GeneratePDF ----------------------------------
		
		public static void GeneratePDF( DataContent data, PatternData pdata )
		{
			double scale = 0.0, scalz = 0.0;

			PdfDocument pdf = new PdfDocument();

			for( int x = 0; x < data.rows; ++x )
			{
				for( int y = 0; y < pdata.pages; ++y )
				{
					PdfPage page = pdf.AddPage();

					// rozmiary strony
					page.Width  = XUnit.FromMillimeter( pdata.size.Width );
					page.Height = XUnit.FromMillimeter( pdata.size.Height );

					// oblicz skale powiększenia
					scale = page.Width.Presentation / (pdata.size.Width * PatternEditor._pixel_per_dpi);
					scalz = page.Width.Value / (pdata.size.Width * PatternEditor._pixel_per_dpi);

					XGraphics gfx = XGraphics.FromPdfPage( page );
					XPdfFontOptions foptions = new XPdfFontOptions( PdfFontEncoding.Unicode, PdfFontEmbedding.Always );
					PageData page_data = pdata.page[y];

					for( int z = 0; z < page_data.fields; ++z )
					{
						// obszar cięcia
						FieldData field_data = page_data.field[z];
						XRect bounds = new XRect
						(
							XUnit.FromMillimeter( (double)field_data.bounds.X ),
							XUnit.FromMillimeter( (double)field_data.bounds.Y ),
							XUnit.FromMillimeter( (double)field_data.bounds.Width ),
							XUnit.FromMillimeter( (double)field_data.bounds.Height )
						);

						// kolor pola 
						if( field_data.extra.print_color )
						{
							XBrush brush = new XSolidBrush( (XColor)field_data.color );

							if( field_data.extra.print_border )
							{
								double bfull   = XUnit.FromMillimeter( (double)field_data.border_size ),
									   bhalf   = bfull * 0.5;
								XRect  cbounds = new XRect( bounds.X + bhalf, bounds.Y + bhalf, bounds.Width - bfull, bounds.Height - bfull );
								gfx.DrawRectangle( brush, cbounds );
							}
							else
								gfx.DrawRectangle( brush, bounds );
						}

						// ramka pola
						if( field_data.extra.print_border && field_data.border_size > 0.0 )
						{
							XPen   pen = new XPen( (XColor)field_data.border_color );
							pen.Width  = XUnit.FromMillimeter( (double)field_data.border_size );
							double prx = pen.Width / 2.0 - 0.01;

							gfx.DrawLine( pen, bounds.X, bounds.Y + prx, bounds.X + bounds.Width, bounds.Y + prx );
							gfx.DrawLine( pen, bounds.X + prx, bounds.Y, bounds.X + prx, bounds.Y + bounds.Height );
							gfx.DrawLine( pen, bounds.X, bounds.Y + bounds.Height - prx, bounds.X + bounds.Width, bounds.Y + bounds.Height - prx );
							gfx.DrawLine( pen, bounds.X + bounds.Width - prx, bounds.Y, bounds.X + bounds.Width - prx, bounds.Y + bounds.Height );
						}

						// rysuj tekst
						if( field_data.extra.print_text || (field_data.extra.text_from_db && field_data.extra.column > -1) )
						{
							float  fsize  = (float)(field_data.font_size * scale);
							XFont  font   = new XFont( field_data.font_name, fsize, (XFontStyle)field_data.font_style, foptions );
							XBrush lbrush = new XSolidBrush( (XColor)field_data.font_color );

							// pobierz napis
							XTextFormatter tf = new XTextFormatter( gfx );
							string text = field_data.extra.print_text
								? field_data.name
								: data.row[x,field_data.extra.column];

							// margines wewnętrzny
							if( field_data.padding > 0.0 )
							{
								double padding = XUnit.FromMillimeter( (double)field_data.padding ),
									   pad2x   = padding * 2.0;

								if( bounds.Width > pad2x && bounds.Height > pad2x )
								{
									bounds.X += padding;
									bounds.Y += padding;
									bounds.Width -= padding * 2.0;
									bounds.Height -= padding * 2.0;
								}
							}

							double font_diff = (((double)((float)font.Height - font.Size) / 2.0) * scale);
							bounds.Y      -= scale;
							bounds.Height += scale * 2.0;

							// sprawdź czy tekst nie jest pusty
							string test = text.Trim();
							if( test != "" && test != null )
							{
								XSize txtm = gfx.MeasureString( text, font );
								
								// rozmieszczenie tekstu
								if( ((int)field_data.text_align & 0x111) != 0 )
									tf.Alignment = XParagraphAlignment.Left;
								else if( ((int)field_data.text_align & 0x222) != 0 )
									tf.Alignment = XParagraphAlignment.Center;
								else
									tf.Alignment = XParagraphAlignment.Right;

								// oblicz realną wysokość tekstu
								int passes = 1;
								for( double twidth = txtm.Width; twidth > bounds.Width; ++passes )
									twidth -= bounds.Width;

								// przyleganie tekstu w pionie
								if( txtm.Height * (double)passes < bounds.Height )
									// wycentrowanie linii
									if( ((int)field_data.text_align & 0x70) != 0 )
									{
										double theight = (bounds.Height - txtm.Height * (double)passes) * 0.5;
										bounds.Y += theight;
									}
									// przyleganie linii do dołu
									else if( ((int)field_data.text_align & 0x700) != 0 )
									{
										double theight = (bounds.Height - txtm.Height * (double)passes);
										bounds.Y += theight;
									}

								tf.DrawString( text, font, lbrush, bounds );
							}
						}
					}
				}
			}

			// zapisz plik pdf
			pdf.Save( "output.pdf" );
		}

		// ------------------------------------------------------------- GetDimensionScale ----------------------------
		
		public static float GetDimensionScale( float width, double scale )
		{
			return (int)((double)width * (PatternEditor._pixel_per_dpi * scale));
		}
	}
}
