namespace CDesigner.Forms
{
	partial class InfoForm
	{
	/// @cond COMPONENTS

		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.TLP_Main = new System.Windows.Forms.TableLayoutPanel();
			this.TLP_StatusBar = new System.Windows.Forms.TableLayoutPanel();
			this.B_Close = new System.Windows.Forms.Button();
			this.TLP_Info = new System.Windows.Forms.TableLayoutPanel();
			this.PB_AppLogo = new System.Windows.Forms.PictureBox();
			this.L_ReleaseDate = new System.Windows.Forms.Label();
			this.L_AppName = new System.Windows.Forms.Label();
			this.L_Copyright = new System.Windows.Forms.Label();
			this.L_Version = new System.Windows.Forms.Label();
			this.L_AuthorApp = new System.Windows.Forms.Label();
			this.LL_Website = new System.Windows.Forms.LinkLabel();
			this.TLP_Main.SuspendLayout();
			this.TLP_StatusBar.SuspendLayout();
			this.TLP_Info.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.PB_AppLogo)).BeginInit();
			this.SuspendLayout();
			// 
			// TLP_Main
			// 
			this.TLP_Main.ColumnCount = 1;
			this.TLP_Main.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.TLP_Main.Controls.Add(this.TLP_StatusBar, 0, 1);
			this.TLP_Main.Controls.Add(this.TLP_Info, 0, 0);
			this.TLP_Main.Dock = System.Windows.Forms.DockStyle.Fill;
			this.TLP_Main.Location = new System.Drawing.Point(0, 0);
			this.TLP_Main.Name = "TLP_Main";
			this.TLP_Main.RowCount = 2;
			this.TLP_Main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.TLP_Main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
			this.TLP_Main.Size = new System.Drawing.Size(290, 321);
			this.TLP_Main.TabIndex = 0;
			// 
			// TLP_StatusBar
			// 
			this.TLP_StatusBar.BackColor = System.Drawing.SystemColors.ControlLight;
			this.TLP_StatusBar.ColumnCount = 1;
			this.TLP_StatusBar.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.TLP_StatusBar.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.TLP_StatusBar.Controls.Add(this.B_Close, 0, 0);
			this.TLP_StatusBar.Dock = System.Windows.Forms.DockStyle.Fill;
			this.TLP_StatusBar.Location = new System.Drawing.Point(0, 289);
			this.TLP_StatusBar.Margin = new System.Windows.Forms.Padding(0);
			this.TLP_StatusBar.Name = "TLP_StatusBar";
			this.TLP_StatusBar.Padding = new System.Windows.Forms.Padding(0, 1, 0, 0);
			this.TLP_StatusBar.RowCount = 1;
			this.TLP_StatusBar.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.TLP_StatusBar.Size = new System.Drawing.Size(290, 32);
			this.TLP_StatusBar.TabIndex = 38;
			this.TLP_StatusBar.Paint += new System.Windows.Forms.PaintEventHandler(this.TLP_StatusBar_Paint);
			// 
			// B_Close
			// 
			this.B_Close.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.B_Close.AutoSize = true;
			this.B_Close.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.B_Close.Location = new System.Drawing.Point(92, 4);
			this.B_Close.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
			this.B_Close.Name = "B_Close";
			this.B_Close.Size = new System.Drawing.Size(105, 25);
			this.B_Close.TabIndex = 7;
			this.B_Close.Text = "Zamknij";
			this.B_Close.UseVisualStyleBackColor = true;
			// 
			// TLP_Info
			// 
			this.TLP_Info.ColumnCount = 1;
			this.TLP_Info.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.TLP_Info.Controls.Add(this.PB_AppLogo, 0, 0);
			this.TLP_Info.Controls.Add(this.L_ReleaseDate, 0, 3);
			this.TLP_Info.Controls.Add(this.L_AppName, 0, 1);
			this.TLP_Info.Controls.Add(this.L_Copyright, 0, 6);
			this.TLP_Info.Controls.Add(this.L_Version, 0, 2);
			this.TLP_Info.Controls.Add(this.L_AuthorApp, 0, 4);
			this.TLP_Info.Controls.Add(this.LL_Website, 0, 5);
			this.TLP_Info.Dock = System.Windows.Forms.DockStyle.Fill;
			this.TLP_Info.Location = new System.Drawing.Point(3, 3);
			this.TLP_Info.Name = "TLP_Info";
			this.TLP_Info.RowCount = 7;
			this.TLP_Info.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 130F));
			this.TLP_Info.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.TLP_Info.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 21F));
			this.TLP_Info.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 21F));
			this.TLP_Info.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 21F));
			this.TLP_Info.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 21F));
			this.TLP_Info.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 31F));
			this.TLP_Info.Size = new System.Drawing.Size(284, 283);
			this.TLP_Info.TabIndex = 39;
			// 
			// PB_AppLogo
			// 
			this.PB_AppLogo.Dock = System.Windows.Forms.DockStyle.Fill;
			this.PB_AppLogo.Location = new System.Drawing.Point(3, 10);
			this.PB_AppLogo.Margin = new System.Windows.Forms.Padding(3, 10, 3, 3);
			this.PB_AppLogo.Name = "PB_AppLogo";
			this.PB_AppLogo.Size = new System.Drawing.Size(278, 117);
			this.PB_AppLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this.PB_AppLogo.TabIndex = 40;
			this.PB_AppLogo.TabStop = false;
			// 
			// L_ReleaseDate
			// 
			this.L_ReleaseDate.AutoSize = true;
			this.L_ReleaseDate.Dock = System.Windows.Forms.DockStyle.Fill;
			this.L_ReleaseDate.ForeColor = System.Drawing.Color.Gray;
			this.L_ReleaseDate.Location = new System.Drawing.Point(3, 189);
			this.L_ReleaseDate.Name = "L_ReleaseDate";
			this.L_ReleaseDate.Size = new System.Drawing.Size(278, 21);
			this.L_ReleaseDate.TabIndex = 42;
			this.L_ReleaseDate.Text = "Data kompilacji: 2015.10.10";
			this.L_ReleaseDate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// L_AppName
			// 
			this.L_AppName.AutoSize = true;
			this.L_AppName.Dock = System.Windows.Forms.DockStyle.Fill;
			this.L_AppName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.L_AppName.ForeColor = System.Drawing.Color.Black;
			this.L_AppName.Location = new System.Drawing.Point(3, 130);
			this.L_AppName.Name = "L_AppName";
			this.L_AppName.Size = new System.Drawing.Size(278, 38);
			this.L_AppName.TabIndex = 13;
			this.L_AppName.Text = "CardDesigner";
			this.L_AppName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// L_Copyright
			// 
			this.L_Copyright.AutoSize = true;
			this.L_Copyright.Dock = System.Windows.Forms.DockStyle.Fill;
			this.L_Copyright.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
			this.L_Copyright.Location = new System.Drawing.Point(3, 252);
			this.L_Copyright.Margin = new System.Windows.Forms.Padding(3, 0, 3, 10);
			this.L_Copyright.Name = "L_Copyright";
			this.L_Copyright.Size = new System.Drawing.Size(278, 21);
			this.L_Copyright.TabIndex = 16;
			this.L_Copyright.Text = "Mozilla Public License 2.0";
			this.L_Copyright.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// L_Version
			// 
			this.L_Version.AutoSize = true;
			this.L_Version.Dock = System.Windows.Forms.DockStyle.Fill;
			this.L_Version.ForeColor = System.Drawing.Color.Gray;
			this.L_Version.Location = new System.Drawing.Point(3, 168);
			this.L_Version.Name = "L_Version";
			this.L_Version.Size = new System.Drawing.Size(278, 21);
			this.L_Version.TabIndex = 14;
			this.L_Version.Text = "0.8.0107.2040 (Gumiennik)";
			this.L_Version.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// L_AuthorApp
			// 
			this.L_AuthorApp.AutoSize = true;
			this.L_AuthorApp.Dock = System.Windows.Forms.DockStyle.Fill;
			this.L_AuthorApp.Location = new System.Drawing.Point(3, 210);
			this.L_AuthorApp.Name = "L_AuthorApp";
			this.L_AuthorApp.Size = new System.Drawing.Size(278, 21);
			this.L_AuthorApp.TabIndex = 15;
			this.L_AuthorApp.Text = "Autor: Kamil Biały";
			this.L_AuthorApp.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// LL_Website
			// 
			this.LL_Website.AutoSize = true;
			this.LL_Website.Dock = System.Windows.Forms.DockStyle.Fill;
			this.LL_Website.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
			this.LL_Website.Location = new System.Drawing.Point(3, 231);
			this.LL_Website.Name = "LL_Website";
			this.LL_Website.Size = new System.Drawing.Size(278, 21);
			this.LL_Website.TabIndex = 41;
			this.LL_Website.TabStop = true;
			this.LL_Website.Text = "http://cdesigner.aculo.pl/";
			this.LL_Website.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// InfoForm
			// 
			this.AcceptButton = this.B_Close;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.Control;
			this.CancelButton = this.B_Close;
			this.ClientSize = new System.Drawing.Size(290, 321);
			this.Controls.Add(this.TLP_Main);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "InfoForm";
			this.Text = "Informacje";
			this.TLP_Main.ResumeLayout(false);
			this.TLP_StatusBar.ResumeLayout(false);
			this.TLP_StatusBar.PerformLayout();
			this.TLP_Info.ResumeLayout(false);
			this.TLP_Info.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.PB_AppLogo)).EndInit();
			this.ResumeLayout(false);

		}

#endregion

		private System.Windows.Forms.TableLayoutPanel TLP_Main;
		private System.Windows.Forms.Label L_Copyright;
		private System.Windows.Forms.Label L_AuthorApp;
		private System.Windows.Forms.Label L_Version;
		private System.Windows.Forms.Label L_AppName;
		private System.Windows.Forms.TableLayoutPanel TLP_StatusBar;
		private System.Windows.Forms.Button B_Close;
		private System.Windows.Forms.TableLayoutPanel TLP_Info;
		private System.Windows.Forms.PictureBox PB_AppLogo;
		private System.Windows.Forms.LinkLabel LL_Website;
		private System.Windows.Forms.Label L_ReleaseDate;

	/// @endcond
	}
}