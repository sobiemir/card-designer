namespace CDesigner.Forms
{
	partial class DatafileSettingsForm
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
            this.SC_Main = new System.Windows.Forms.SplitContainer();
            this.LV_Rows = new System.Windows.Forms.ListView();
            this.CH_Rows = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.LV_Columns = new System.Windows.Forms.ListView();
            this.CH_Columns = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.TLP_StatusBar = new System.Windows.Forms.TableLayoutPanel();
            this.TLP_Settings = new System.Windows.Forms.TableLayoutPanel();
            this.TB_Separator = new System.Windows.Forms.TextBox();
            this.CBX_Encoding = new System.Windows.Forms.ComboBox();
            this.CBX_Separator = new System.Windows.Forms.ComboBox();
            this.CB_AutoCheck = new System.Windows.Forms.CheckBox();
            this.TB_FileName = new System.Windows.Forms.TextBox();
            this.TLP_Buttons = new System.Windows.Forms.TableLayoutPanel();
            this.B_Change = new System.Windows.Forms.Button();
            this.B_Cancel = new System.Windows.Forms.Button();
            this.B_Save = new System.Windows.Forms.Button();
            this.CB_NoColumns = new System.Windows.Forms.CheckBox();
            this.TLP_Main.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SC_Main)).BeginInit();
            this.SC_Main.Panel1.SuspendLayout();
            this.SC_Main.Panel2.SuspendLayout();
            this.SC_Main.SuspendLayout();
            this.TLP_StatusBar.SuspendLayout();
            this.TLP_Settings.SuspendLayout();
            this.TLP_Buttons.SuspendLayout();
            this.SuspendLayout();
            // 
            // TLP_Main
            // 
            this.TLP_Main.ColumnCount = 1;
            this.TLP_Main.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.TLP_Main.Controls.Add(this.SC_Main, 0, 0);
            this.TLP_Main.Controls.Add(this.TLP_StatusBar, 0, 1);
            this.TLP_Main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TLP_Main.Location = new System.Drawing.Point(0, 0);
            this.TLP_Main.Margin = new System.Windows.Forms.Padding(0);
            this.TLP_Main.Name = "TLP_Main";
            this.TLP_Main.RowCount = 2;
            this.TLP_Main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.TLP_Main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 61F));
            this.TLP_Main.Size = new System.Drawing.Size(625, 388);
            this.TLP_Main.TabIndex = 2;
            // 
            // SC_Main
            // 
            this.TLP_Main.SetColumnSpan(this.SC_Main, 2);
            this.SC_Main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SC_Main.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.SC_Main.Location = new System.Drawing.Point(6, 6);
            this.SC_Main.Margin = new System.Windows.Forms.Padding(6);
            this.SC_Main.Name = "SC_Main";
            // 
            // SC_Main.Panel1
            // 
            this.SC_Main.Panel1.Controls.Add(this.LV_Rows);
            this.SC_Main.Panel1MinSize = 399;
            // 
            // SC_Main.Panel2
            // 
            this.SC_Main.Panel2.Controls.Add(this.LV_Columns);
            this.SC_Main.Panel2MinSize = 200;
            this.SC_Main.Size = new System.Drawing.Size(613, 315);
            this.SC_Main.SplitterDistance = 401;
            this.SC_Main.TabIndex = 3;
            // 
            // LV_Rows
            // 
            this.LV_Rows.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.CH_Rows});
            this.LV_Rows.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LV_Rows.FullRowSelect = true;
            this.LV_Rows.GridLines = true;
            this.LV_Rows.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.LV_Rows.HideSelection = false;
            this.LV_Rows.Location = new System.Drawing.Point(0, 0);
            this.LV_Rows.Margin = new System.Windows.Forms.Padding(0);
            this.LV_Rows.MultiSelect = false;
            this.LV_Rows.Name = "LV_Rows";
            this.LV_Rows.Size = new System.Drawing.Size(401, 315);
            this.LV_Rows.TabIndex = 22;
            this.LV_Rows.UseCompatibleStateImageBehavior = false;
            this.LV_Rows.View = System.Windows.Forms.View.Details;
            // 
            // CH_Rows
            // 
            this.CH_Rows.Text = "Podgląd wierszy";
            this.CH_Rows.Width = 396;
            // 
            // LV_Columns
            // 
            this.LV_Columns.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.CH_Columns});
            this.LV_Columns.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LV_Columns.FullRowSelect = true;
            this.LV_Columns.GridLines = true;
            this.LV_Columns.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.LV_Columns.HideSelection = false;
            this.LV_Columns.Location = new System.Drawing.Point(0, 0);
            this.LV_Columns.Margin = new System.Windows.Forms.Padding(0);
            this.LV_Columns.MultiSelect = false;
            this.LV_Columns.Name = "LV_Columns";
            this.LV_Columns.Size = new System.Drawing.Size(208, 315);
            this.LV_Columns.TabIndex = 21;
            this.LV_Columns.UseCompatibleStateImageBehavior = false;
            this.LV_Columns.View = System.Windows.Forms.View.Details;
            this.LV_Columns.SelectedIndexChanged += new System.EventHandler(this.lvColumns_SelectedIndexChanged);
            // 
            // CH_Columns
            // 
            this.CH_Columns.Text = "Kolumny";
            this.CH_Columns.Width = 204;
            // 
            // TLP_StatusBar
            // 
            this.TLP_StatusBar.BackColor = System.Drawing.SystemColors.ControlLight;
            this.TLP_StatusBar.ColumnCount = 2;
            this.TLP_StatusBar.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.TLP_StatusBar.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 220F));
            this.TLP_StatusBar.Controls.Add(this.TLP_Settings, 0, 0);
            this.TLP_StatusBar.Controls.Add(this.TLP_Buttons, 1, 0);
            this.TLP_StatusBar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TLP_StatusBar.Location = new System.Drawing.Point(0, 327);
            this.TLP_StatusBar.Margin = new System.Windows.Forms.Padding(0);
            this.TLP_StatusBar.Name = "TLP_StatusBar";
            this.TLP_StatusBar.RowCount = 1;
            this.TLP_StatusBar.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.TLP_StatusBar.Size = new System.Drawing.Size(625, 61);
            this.TLP_StatusBar.TabIndex = 23;
            this.TLP_StatusBar.Paint += new System.Windows.Forms.PaintEventHandler(this.tlpStatusBar_Paint);
            // 
            // TLP_Settings
            // 
            this.TLP_Settings.ColumnCount = 3;
            this.TLP_Settings.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 110F));
            this.TLP_Settings.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.TLP_Settings.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.TLP_Settings.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.TLP_Settings.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.TLP_Settings.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.TLP_Settings.Controls.Add(this.TB_Separator, 1, 1);
            this.TLP_Settings.Controls.Add(this.CBX_Encoding, 0, 0);
            this.TLP_Settings.Controls.Add(this.CBX_Separator, 0, 1);
            this.TLP_Settings.Controls.Add(this.CB_AutoCheck, 2, 1);
            this.TLP_Settings.Controls.Add(this.TB_FileName, 2, 0);
            this.TLP_Settings.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TLP_Settings.Location = new System.Drawing.Point(0, 1);
            this.TLP_Settings.Margin = new System.Windows.Forms.Padding(0, 1, 0, 0);
            this.TLP_Settings.Name = "TLP_Settings";
            this.TLP_Settings.RowCount = 2;
            this.TLP_Settings.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.TLP_Settings.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.TLP_Settings.Size = new System.Drawing.Size(405, 60);
            this.TLP_Settings.TabIndex = 22;
            // 
            // TB_Separator
            // 
            this.TB_Separator.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TB_Separator.Enabled = false;
            this.TB_Separator.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.TB_Separator.Location = new System.Drawing.Point(112, 33);
            this.TB_Separator.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.TB_Separator.MaxLength = 1;
            this.TB_Separator.Name = "TB_Separator";
            this.TB_Separator.Size = new System.Drawing.Size(36, 20);
            this.TB_Separator.TabIndex = 19;
            this.TB_Separator.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.TB_Separator.TextChanged += new System.EventHandler(this.tbSeparator_TextChanged);
            // 
            // CBX_Encoding
            // 
            this.TLP_Settings.SetColumnSpan(this.CBX_Encoding, 2);
            this.CBX_Encoding.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CBX_Encoding.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CBX_Encoding.FormattingEnabled = true;
            this.CBX_Encoding.Items.AddRange(new object[] {
            "Domyślne",
            "ASCII",
            "UTF-8",
            "UTF-16 BigEndian",
            "UTF-16 LittleEndian",
            "UTF-32",
            "UTF-7"});
            this.CBX_Encoding.Location = new System.Drawing.Point(6, 6);
            this.CBX_Encoding.Margin = new System.Windows.Forms.Padding(6, 6, 2, 2);
            this.CBX_Encoding.Name = "CBX_Encoding";
            this.CBX_Encoding.Size = new System.Drawing.Size(142, 21);
            this.CBX_Encoding.TabIndex = 17;
            this.CBX_Encoding.SelectedIndexChanged += new System.EventHandler(this.sbEncoding_SelectedIndexChanged);
            // 
            // CBX_Separator
            // 
            this.CBX_Separator.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CBX_Separator.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CBX_Separator.FormattingEnabled = true;
            this.CBX_Separator.Items.AddRange(new object[] {
            "Średnik",
            "Przecinek",
            "Kropka",
            "Tabulator",
            "Spacja",
            "Inny znak"});
            this.CBX_Separator.Location = new System.Drawing.Point(6, 33);
            this.CBX_Separator.Margin = new System.Windows.Forms.Padding(6, 3, 2, 3);
            this.CBX_Separator.Name = "CBX_Separator";
            this.CBX_Separator.Size = new System.Drawing.Size(102, 21);
            this.CBX_Separator.TabIndex = 21;
            this.CBX_Separator.SelectedIndexChanged += new System.EventHandler(this.sbSeparator_SelectedIndexChanged);
            // 
            // CB_AutoCheck
            // 
            this.CB_AutoCheck.AutoSize = true;
            this.CB_AutoCheck.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CB_AutoCheck.Enabled = false;
            this.CB_AutoCheck.Location = new System.Drawing.Point(154, 31);
            this.CB_AutoCheck.Margin = new System.Windows.Forms.Padding(4, 1, 3, 3);
            this.CB_AutoCheck.Name = "CB_AutoCheck";
            this.CB_AutoCheck.Size = new System.Drawing.Size(248, 26);
            this.CB_AutoCheck.TabIndex = 22;
            this.CB_AutoCheck.Text = "Automatycznie wykrywaj typy kolumn";
            this.CB_AutoCheck.UseVisualStyleBackColor = true;
            // 
            // TB_FileName
            // 
            this.TB_FileName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TB_FileName.Location = new System.Drawing.Point(154, 6);
            this.TB_FileName.Margin = new System.Windows.Forms.Padding(4, 6, 2, 3);
            this.TB_FileName.Name = "TB_FileName";
            this.TB_FileName.Size = new System.Drawing.Size(249, 20);
            this.TB_FileName.TabIndex = 23;
            this.TB_FileName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbFileName_KeyPress);
            // 
            // TLP_Buttons
            // 
            this.TLP_Buttons.ColumnCount = 2;
            this.TLP_Buttons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.TLP_Buttons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.TLP_Buttons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.TLP_Buttons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.TLP_Buttons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.TLP_Buttons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.TLP_Buttons.Controls.Add(this.B_Change, 0, 0);
            this.TLP_Buttons.Controls.Add(this.B_Cancel, 1, 1);
            this.TLP_Buttons.Controls.Add(this.B_Save, 0, 1);
            this.TLP_Buttons.Controls.Add(this.CB_NoColumns, 1, 0);
            this.TLP_Buttons.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TLP_Buttons.Location = new System.Drawing.Point(405, 1);
            this.TLP_Buttons.Margin = new System.Windows.Forms.Padding(0, 1, 0, 0);
            this.TLP_Buttons.Name = "TLP_Buttons";
            this.TLP_Buttons.RowCount = 2;
            this.TLP_Buttons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.TLP_Buttons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.TLP_Buttons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.TLP_Buttons.Size = new System.Drawing.Size(220, 60);
            this.TLP_Buttons.TabIndex = 23;
            // 
            // B_Change
            // 
            this.B_Change.Dock = System.Windows.Forms.DockStyle.Fill;
            this.B_Change.Location = new System.Drawing.Point(4, 3);
            this.B_Change.Margin = new System.Windows.Forms.Padding(4, 3, 2, 2);
            this.B_Change.Name = "B_Change";
            this.B_Change.Size = new System.Drawing.Size(104, 25);
            this.B_Change.TabIndex = 1;
            this.B_Change.Text = "Zmień plik";
            this.B_Change.UseVisualStyleBackColor = true;
            this.B_Change.Click += new System.EventHandler(this.bChange_Click);
            // 
            // B_Cancel
            // 
            this.B_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.B_Cancel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.B_Cancel.Location = new System.Drawing.Point(112, 32);
            this.B_Cancel.Margin = new System.Windows.Forms.Padding(2, 2, 6, 3);
            this.B_Cancel.Name = "B_Cancel";
            this.B_Cancel.Size = new System.Drawing.Size(102, 25);
            this.B_Cancel.TabIndex = 20;
            this.B_Cancel.Text = "Zaniechaj";
            this.B_Cancel.UseVisualStyleBackColor = true;
            this.B_Cancel.Click += new System.EventHandler(this.bCancel_Click);
            // 
            // B_Save
            // 
            this.B_Save.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.B_Save.Dock = System.Windows.Forms.DockStyle.Fill;
            this.B_Save.Location = new System.Drawing.Point(4, 32);
            this.B_Save.Margin = new System.Windows.Forms.Padding(4, 2, 2, 3);
            this.B_Save.Name = "B_Save";
            this.B_Save.Size = new System.Drawing.Size(104, 25);
            this.B_Save.TabIndex = 0;
            this.B_Save.Text = "Zatwierdź";
            this.B_Save.UseVisualStyleBackColor = true;
            this.B_Save.Click += new System.EventHandler(this.bSave_Click);
            // 
            // CB_NoColumns
            // 
            this.CB_NoColumns.AutoSize = true;
            this.CB_NoColumns.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CB_NoColumns.Location = new System.Drawing.Point(116, 5);
            this.CB_NoColumns.Margin = new System.Windows.Forms.Padding(6, 5, 3, 3);
            this.CB_NoColumns.Name = "CB_NoColumns";
            this.CB_NoColumns.Size = new System.Drawing.Size(101, 22);
            this.CB_NoColumns.TabIndex = 21;
            this.CB_NoColumns.Text = "Bez nagłówka";
            this.CB_NoColumns.UseVisualStyleBackColor = true;
            this.CB_NoColumns.CheckedChanged += new System.EventHandler(this.cbNoColumns_CheckedChanged);
            // 
            // DatafileSettingsForm
            // 
            this.AcceptButton = this.B_Save;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.B_Cancel;
            this.ClientSize = new System.Drawing.Size(625, 388);
            this.Controls.Add(this.TLP_Main);
            this.MaximumSize = new System.Drawing.Size(65535, 65535);
            this.MinimumSize = new System.Drawing.Size(640, 290);
            this.Name = "DatafileSettingsForm";
            this.Text = "Ustawienia pliku bazy danych";
            this.TLP_Main.ResumeLayout(false);
            this.SC_Main.Panel1.ResumeLayout(false);
            this.SC_Main.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.SC_Main)).EndInit();
            this.SC_Main.ResumeLayout(false);
            this.TLP_StatusBar.ResumeLayout(false);
            this.TLP_Settings.ResumeLayout(false);
            this.TLP_Settings.PerformLayout();
            this.TLP_Buttons.ResumeLayout(false);
            this.TLP_Buttons.PerformLayout();
            this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TableLayoutPanel TLP_Main;
		private System.Windows.Forms.SplitContainer SC_Main;
		private System.Windows.Forms.ListView LV_Rows;
		private System.Windows.Forms.ColumnHeader CH_Rows;
		private System.Windows.Forms.ListView LV_Columns;
		private System.Windows.Forms.ColumnHeader CH_Columns;
		private System.Windows.Forms.TableLayoutPanel TLP_StatusBar;
		private System.Windows.Forms.Button B_Cancel;
		private System.Windows.Forms.TextBox TB_Separator;
		private System.Windows.Forms.ComboBox CBX_Encoding;
		private System.Windows.Forms.Button B_Save;
		private System.Windows.Forms.Button B_Change;
		private System.Windows.Forms.ComboBox CBX_Separator;
		private System.Windows.Forms.TableLayoutPanel TLP_Settings;
		private System.Windows.Forms.TableLayoutPanel TLP_Buttons;
		private System.Windows.Forms.CheckBox CB_AutoCheck;
		private System.Windows.Forms.CheckBox CB_NoColumns;
		private System.Windows.Forms.TextBox TB_FileName;

        /// @endcond
	}
}