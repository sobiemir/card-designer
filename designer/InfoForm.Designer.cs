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
            this.GB_Additional = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.PB_RegisterLogo = new System.Windows.Forms.PictureBox();
            this.L_RegisterFor = new System.Windows.Forms.Label();
            this.L_RegisterLine1 = new System.Windows.Forms.Label();
            this.L_RegisterLine2 = new System.Windows.Forms.Label();
            this.L_RegisterLine3 = new System.Windows.Forms.Label();
            this.L_SerialNumber = new System.Windows.Forms.Label();
            this.L_ExpireDate = new System.Windows.Forms.Label();
            this.TLP_Main.SuspendLayout();
            this.TLP_StatusBar.SuspendLayout();
            this.TLP_Info.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PB_AppLogo)).BeginInit();
            this.GB_Additional.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PB_RegisterLogo)).BeginInit();
            this.SuspendLayout();
            // 
            // TLP_Main
            // 
            this.TLP_Main.ColumnCount = 1;
            this.TLP_Main.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.TLP_Main.Controls.Add(this.TLP_StatusBar, 0, 2);
            this.TLP_Main.Controls.Add(this.TLP_Info, 0, 0);
            this.TLP_Main.Controls.Add(this.GB_Additional, 0, 1);
            this.TLP_Main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TLP_Main.Location = new System.Drawing.Point(0, 0);
            this.TLP_Main.Name = "TLP_Main";
            this.TLP_Main.RowCount = 3;
            this.TLP_Main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 140F));
            this.TLP_Main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.TLP_Main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.TLP_Main.Size = new System.Drawing.Size(414, 338);
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
            this.TLP_StatusBar.Location = new System.Drawing.Point(0, 306);
            this.TLP_StatusBar.Margin = new System.Windows.Forms.Padding(0);
            this.TLP_StatusBar.Name = "TLP_StatusBar";
            this.TLP_StatusBar.Padding = new System.Windows.Forms.Padding(0, 1, 0, 0);
            this.TLP_StatusBar.RowCount = 1;
            this.TLP_StatusBar.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.TLP_StatusBar.Size = new System.Drawing.Size(414, 32);
            this.TLP_StatusBar.TabIndex = 38;
            this.TLP_StatusBar.Paint += new System.Windows.Forms.PaintEventHandler(this.TLP_StatusBar_Paint);
            // 
            // B_Close
            // 
            this.B_Close.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.B_Close.AutoSize = true;
            this.B_Close.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.B_Close.Location = new System.Drawing.Point(154, 4);
            this.B_Close.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.B_Close.Name = "B_Close";
            this.B_Close.Size = new System.Drawing.Size(105, 25);
            this.B_Close.TabIndex = 7;
            this.B_Close.Text = "Zamknij";
            this.B_Close.UseVisualStyleBackColor = true;
            // 
            // TLP_Info
            // 
            this.TLP_Info.ColumnCount = 2;
            this.TLP_Info.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 134F));
            this.TLP_Info.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.TLP_Info.Controls.Add(this.PB_AppLogo, 0, 0);
            this.TLP_Info.Controls.Add(this.L_ReleaseDate, 1, 1);
            this.TLP_Info.Controls.Add(this.L_AppName, 1, 0);
            this.TLP_Info.Controls.Add(this.L_Copyright, 1, 4);
            this.TLP_Info.Controls.Add(this.L_Version, 1, 1);
            this.TLP_Info.Controls.Add(this.L_AuthorApp, 1, 2);
            this.TLP_Info.Controls.Add(this.LL_Website, 1, 3);
            this.TLP_Info.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TLP_Info.Location = new System.Drawing.Point(3, 3);
            this.TLP_Info.Name = "TLP_Info";
            this.TLP_Info.RowCount = 6;
            this.TLP_Info.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.TLP_Info.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 21F));
            this.TLP_Info.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 21F));
            this.TLP_Info.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 21F));
            this.TLP_Info.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 21F));
            this.TLP_Info.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 21F));
            this.TLP_Info.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 21F));
            this.TLP_Info.Size = new System.Drawing.Size(408, 134);
            this.TLP_Info.TabIndex = 39;
            // 
            // PB_AppLogo
            // 
            this.PB_AppLogo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PB_AppLogo.Location = new System.Drawing.Point(3, 3);
            this.PB_AppLogo.Name = "PB_AppLogo";
            this.TLP_Info.SetRowSpan(this.PB_AppLogo, 6);
            this.PB_AppLogo.Size = new System.Drawing.Size(128, 128);
            this.PB_AppLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.PB_AppLogo.TabIndex = 40;
            this.PB_AppLogo.TabStop = false;
            // 
            // L_ReleaseDate
            // 
            this.L_ReleaseDate.AutoSize = true;
            this.L_ReleaseDate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.L_ReleaseDate.ForeColor = System.Drawing.Color.Gray;
            this.L_ReleaseDate.Location = new System.Drawing.Point(137, 50);
            this.L_ReleaseDate.Name = "L_ReleaseDate";
            this.L_ReleaseDate.Size = new System.Drawing.Size(268, 21);
            this.L_ReleaseDate.TabIndex = 42;
            this.L_ReleaseDate.Text = "Data kompilacji: 2015.10.10";
            this.L_ReleaseDate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // L_AppName
            // 
            this.L_AppName.AutoSize = true;
            this.L_AppName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.L_AppName.ForeColor = System.Drawing.Color.Black;
            this.L_AppName.Location = new System.Drawing.Point(137, 0);
            this.L_AppName.Name = "L_AppName";
            this.L_AppName.Size = new System.Drawing.Size(268, 29);
            this.L_AppName.TabIndex = 13;
            this.L_AppName.Text = "CardDesigner";
            this.L_AppName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // L_Copyright
            // 
            this.L_Copyright.AutoSize = true;
            this.L_Copyright.Dock = System.Windows.Forms.DockStyle.Fill;
            this.L_Copyright.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.L_Copyright.Location = new System.Drawing.Point(137, 113);
            this.L_Copyright.Name = "L_Copyright";
            this.L_Copyright.Size = new System.Drawing.Size(268, 21);
            this.L_Copyright.TabIndex = 16;
            this.L_Copyright.Text = "Copyright © 2015. All rights reserved.";
            this.L_Copyright.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // L_Version
            // 
            this.L_Version.AutoSize = true;
            this.L_Version.Dock = System.Windows.Forms.DockStyle.Fill;
            this.L_Version.ForeColor = System.Drawing.Color.Gray;
            this.L_Version.Location = new System.Drawing.Point(137, 29);
            this.L_Version.Name = "L_Version";
            this.L_Version.Size = new System.Drawing.Size(268, 21);
            this.L_Version.TabIndex = 14;
            this.L_Version.Text = "0.8.0107.2040 (Gumiennik)";
            this.L_Version.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // L_AuthorApp
            // 
            this.L_AuthorApp.AutoSize = true;
            this.L_AuthorApp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.L_AuthorApp.Location = new System.Drawing.Point(137, 71);
            this.L_AuthorApp.Name = "L_AuthorApp";
            this.L_AuthorApp.Size = new System.Drawing.Size(268, 21);
            this.L_AuthorApp.TabIndex = 15;
            this.L_AuthorApp.Text = "Autor: Kamil Biały";
            this.L_AuthorApp.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LL_Website
            // 
            this.LL_Website.AutoSize = true;
            this.LL_Website.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LL_Website.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.LL_Website.Location = new System.Drawing.Point(137, 92);
            this.LL_Website.Name = "LL_Website";
            this.LL_Website.Size = new System.Drawing.Size(268, 21);
            this.LL_Website.TabIndex = 41;
            this.LL_Website.TabStop = true;
            this.LL_Website.Text = "http://cdesigner.aculo.pl/";
            this.LL_Website.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // GB_Additional
            // 
            this.GB_Additional.Controls.Add(this.tableLayoutPanel1);
            this.GB_Additional.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GB_Additional.Location = new System.Drawing.Point(6, 145);
            this.GB_Additional.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.GB_Additional.Name = "GB_Additional";
            this.GB_Additional.Size = new System.Drawing.Size(402, 156);
            this.GB_Additional.TabIndex = 40;
            this.GB_Additional.TabStop = false;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 137F));
            this.tableLayoutPanel1.Controls.Add(this.PB_RegisterLogo, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.L_RegisterFor, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.L_RegisterLine1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.L_RegisterLine2, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.L_RegisterLine3, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.L_SerialNumber, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.L_ExpireDate, 0, 5);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 16);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 6;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 23F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 23F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 23F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 23F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(396, 137);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // PB_RegisterLogo
            // 
            this.PB_RegisterLogo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PB_RegisterLogo.Location = new System.Drawing.Point(262, 3);
            this.PB_RegisterLogo.Name = "PB_RegisterLogo";
            this.tableLayoutPanel1.SetRowSpan(this.PB_RegisterLogo, 6);
            this.PB_RegisterLogo.Size = new System.Drawing.Size(131, 131);
            this.PB_RegisterLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.PB_RegisterLogo.TabIndex = 0;
            this.PB_RegisterLogo.TabStop = false;
            // 
            // L_RegisterFor
            // 
            this.L_RegisterFor.AutoSize = true;
            this.L_RegisterFor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.L_RegisterFor.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.L_RegisterFor.Location = new System.Drawing.Point(3, 0);
            this.L_RegisterFor.Name = "L_RegisterFor";
            this.L_RegisterFor.Size = new System.Drawing.Size(253, 20);
            this.L_RegisterFor.TabIndex = 1;
            this.L_RegisterFor.Text = "Zarejestrowano dla:";
            this.L_RegisterFor.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // L_RegisterLine1
            // 
            this.L_RegisterLine1.AutoSize = true;
            this.L_RegisterLine1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.L_RegisterLine1.Location = new System.Drawing.Point(3, 20);
            this.L_RegisterLine1.Name = "L_RegisterLine1";
            this.L_RegisterLine1.Size = new System.Drawing.Size(253, 23);
            this.L_RegisterLine1.TabIndex = 2;
            this.L_RegisterLine1.Text = "Państwowa Wyższa Szkoła Zawodowa";
            this.L_RegisterLine1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // L_RegisterLine2
            // 
            this.L_RegisterLine2.AutoSize = true;
            this.L_RegisterLine2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.L_RegisterLine2.Location = new System.Drawing.Point(3, 43);
            this.L_RegisterLine2.Name = "L_RegisterLine2";
            this.L_RegisterLine2.Size = new System.Drawing.Size(253, 23);
            this.L_RegisterLine2.TabIndex = 3;
            this.L_RegisterLine2.Text = "im. Stanisława Pigionia w Krośnie";
            this.L_RegisterLine2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // L_RegisterLine3
            // 
            this.L_RegisterLine3.AutoSize = true;
            this.L_RegisterLine3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.L_RegisterLine3.Location = new System.Drawing.Point(3, 66);
            this.L_RegisterLine3.Name = "L_RegisterLine3";
            this.L_RegisterLine3.Size = new System.Drawing.Size(253, 23);
            this.L_RegisterLine3.TabIndex = 4;
            this.L_RegisterLine3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // L_SerialNumber
            // 
            this.L_SerialNumber.AutoSize = true;
            this.L_SerialNumber.Dock = System.Windows.Forms.DockStyle.Fill;
            this.L_SerialNumber.ForeColor = System.Drawing.Color.Gray;
            this.L_SerialNumber.Location = new System.Drawing.Point(3, 89);
            this.L_SerialNumber.Name = "L_SerialNumber";
            this.L_SerialNumber.Size = new System.Drawing.Size(253, 23);
            this.L_SerialNumber.TabIndex = 5;
            this.L_SerialNumber.Text = "Numer seryjny: 2KJK6-ASTTA-50AC0-AK43A";
            this.L_SerialNumber.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // L_ExpireDate
            // 
            this.L_ExpireDate.AutoSize = true;
            this.L_ExpireDate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.L_ExpireDate.ForeColor = System.Drawing.Color.Gray;
            this.L_ExpireDate.Location = new System.Drawing.Point(3, 112);
            this.L_ExpireDate.Name = "L_ExpireDate";
            this.L_ExpireDate.Size = new System.Drawing.Size(253, 25);
            this.L_ExpireDate.TabIndex = 6;
            this.L_ExpireDate.Text = "Data wygaśnięcia: Nigdy";
            // 
            // InfoForm
            // 
            this.AcceptButton = this.B_Close;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.CancelButton = this.B_Close;
            this.ClientSize = new System.Drawing.Size(414, 338);
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
            this.GB_Additional.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PB_RegisterLogo)).EndInit();
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
        private System.Windows.Forms.GroupBox GB_Additional;
        private System.Windows.Forms.Label L_ReleaseDate;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.PictureBox PB_RegisterLogo;
        private System.Windows.Forms.Label L_RegisterFor;
        private System.Windows.Forms.Label L_RegisterLine1;
        private System.Windows.Forms.Label L_RegisterLine2;
        private System.Windows.Forms.Label L_RegisterLine3;
        private System.Windows.Forms.Label L_SerialNumber;
        private System.Windows.Forms.Label L_ExpireDate;

        /// @endcond
	}
}