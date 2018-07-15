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
            this.tlpMain = new System.Windows.Forms.TableLayoutPanel();
            this.scMain = new System.Windows.Forms.SplitContainer();
            this.lvRows = new System.Windows.Forms.ListView();
            this.lvcRows = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvColumns = new System.Windows.Forms.ListView();
            this.lvcColumns = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tlpStatusBar = new System.Windows.Forms.TableLayoutPanel();
            this.tlpSettings = new System.Windows.Forms.TableLayoutPanel();
            this.tbSeparator = new System.Windows.Forms.TextBox();
            this.sbEncoding = new System.Windows.Forms.ComboBox();
            this.sbSeparator = new System.Windows.Forms.ComboBox();
            this.cbAutoCheck = new System.Windows.Forms.CheckBox();
            this.tbFileName = new System.Windows.Forms.TextBox();
            this.tlButtons = new System.Windows.Forms.TableLayoutPanel();
            this.bChange = new System.Windows.Forms.Button();
            this.bCancel = new System.Windows.Forms.Button();
            this.bSave = new System.Windows.Forms.Button();
            this.cbNoColumns = new System.Windows.Forms.CheckBox();
            this.tlpMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.scMain)).BeginInit();
            this.scMain.Panel1.SuspendLayout();
            this.scMain.Panel2.SuspendLayout();
            this.scMain.SuspendLayout();
            this.tlpStatusBar.SuspendLayout();
            this.tlpSettings.SuspendLayout();
            this.tlButtons.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlpMain
            // 
            this.tlpMain.ColumnCount = 1;
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMain.Controls.Add(this.scMain, 0, 0);
            this.tlpMain.Controls.Add(this.tlpStatusBar, 0, 1);
            this.tlpMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpMain.Location = new System.Drawing.Point(0, 0);
            this.tlpMain.Margin = new System.Windows.Forms.Padding(0);
            this.tlpMain.Name = "tlpMain";
            this.tlpMain.RowCount = 2;
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 61F));
            this.tlpMain.Size = new System.Drawing.Size(624, 388);
            this.tlpMain.TabIndex = 2;
            // 
            // scMain
            // 
            this.tlpMain.SetColumnSpan(this.scMain, 2);
            this.scMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scMain.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.scMain.Location = new System.Drawing.Point(6, 6);
            this.scMain.Margin = new System.Windows.Forms.Padding(6, 6, 6, 4);
            this.scMain.Name = "scMain";
            // 
            // scMain.Panel1
            // 
            this.scMain.Panel1.Controls.Add(this.lvRows);
            this.scMain.Panel1MinSize = 399;
            // 
            // scMain.Panel2
            // 
            this.scMain.Panel2.Controls.Add(this.lvColumns);
            this.scMain.Panel2MinSize = 200;
            this.scMain.Size = new System.Drawing.Size(612, 317);
            this.scMain.SplitterDistance = 399;
            this.scMain.TabIndex = 3;
            // 
            // lvRows
            // 
            this.lvRows.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.lvcRows});
            this.lvRows.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvRows.FullRowSelect = true;
            this.lvRows.GridLines = true;
            this.lvRows.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvRows.HideSelection = false;
            this.lvRows.Location = new System.Drawing.Point(0, 0);
            this.lvRows.Margin = new System.Windows.Forms.Padding(0);
            this.lvRows.MultiSelect = false;
            this.lvRows.Name = "lvRows";
            this.lvRows.Size = new System.Drawing.Size(399, 317);
            this.lvRows.TabIndex = 22;
            this.lvRows.UseCompatibleStateImageBehavior = false;
            this.lvRows.View = System.Windows.Forms.View.Details;
            // 
            // lvcRows
            // 
            this.lvcRows.Text = "Podgląd wierszy";
            this.lvcRows.Width = 396;
            // 
            // lvColumns
            // 
            this.lvColumns.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.lvcColumns});
            this.lvColumns.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvColumns.FullRowSelect = true;
            this.lvColumns.GridLines = true;
            this.lvColumns.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvColumns.HideSelection = false;
            this.lvColumns.Location = new System.Drawing.Point(0, 0);
            this.lvColumns.Margin = new System.Windows.Forms.Padding(0);
            this.lvColumns.MultiSelect = false;
            this.lvColumns.Name = "lvColumns";
            this.lvColumns.Size = new System.Drawing.Size(209, 317);
            this.lvColumns.TabIndex = 21;
            this.lvColumns.UseCompatibleStateImageBehavior = false;
            this.lvColumns.View = System.Windows.Forms.View.Details;
            this.lvColumns.SelectedIndexChanged += new System.EventHandler(this.lvColumns_SelectedIndexChanged);
            // 
            // lvcColumns
            // 
            this.lvcColumns.Text = "Kolumny";
            this.lvcColumns.Width = 204;
            // 
            // tlpStatusBar
            // 
            this.tlpStatusBar.BackColor = System.Drawing.SystemColors.ControlLight;
            this.tlpStatusBar.ColumnCount = 2;
            this.tlpStatusBar.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpStatusBar.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 220F));
            this.tlpStatusBar.Controls.Add(this.tlpSettings, 0, 0);
            this.tlpStatusBar.Controls.Add(this.tlButtons, 1, 0);
            this.tlpStatusBar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpStatusBar.Location = new System.Drawing.Point(0, 327);
            this.tlpStatusBar.Margin = new System.Windows.Forms.Padding(0);
            this.tlpStatusBar.Name = "tlpStatusBar";
            this.tlpStatusBar.RowCount = 1;
            this.tlpStatusBar.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpStatusBar.Size = new System.Drawing.Size(624, 61);
            this.tlpStatusBar.TabIndex = 23;
            this.tlpStatusBar.Paint += new System.Windows.Forms.PaintEventHandler(this.tlpStatusBar_Paint);
            // 
            // tlpSettings
            // 
            this.tlpSettings.ColumnCount = 3;
            this.tlpSettings.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 110F));
            this.tlpSettings.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tlpSettings.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpSettings.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpSettings.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpSettings.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpSettings.Controls.Add(this.tbSeparator, 1, 1);
            this.tlpSettings.Controls.Add(this.sbEncoding, 0, 0);
            this.tlpSettings.Controls.Add(this.sbSeparator, 0, 1);
            this.tlpSettings.Controls.Add(this.cbAutoCheck, 2, 1);
            this.tlpSettings.Controls.Add(this.tbFileName, 2, 0);
            this.tlpSettings.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpSettings.Location = new System.Drawing.Point(0, 1);
            this.tlpSettings.Margin = new System.Windows.Forms.Padding(0, 1, 0, 0);
            this.tlpSettings.Name = "tlpSettings";
            this.tlpSettings.RowCount = 2;
            this.tlpSettings.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpSettings.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpSettings.Size = new System.Drawing.Size(404, 60);
            this.tlpSettings.TabIndex = 22;
            // 
            // tbSeparator
            // 
            this.tbSeparator.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbSeparator.Enabled = false;
            this.tbSeparator.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.tbSeparator.Location = new System.Drawing.Point(112, 33);
            this.tbSeparator.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.tbSeparator.MaxLength = 1;
            this.tbSeparator.Name = "tbSeparator";
            this.tbSeparator.Size = new System.Drawing.Size(36, 20);
            this.tbSeparator.TabIndex = 19;
            this.tbSeparator.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tbSeparator.TextChanged += new System.EventHandler(this.tbSeparator_TextChanged);
            // 
            // sbEncoding
            // 
            this.tlpSettings.SetColumnSpan(this.sbEncoding, 2);
            this.sbEncoding.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sbEncoding.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.sbEncoding.FormattingEnabled = true;
            this.sbEncoding.Items.AddRange(new object[] {
            "Domyślne",
            "ASCII",
            "UTF-8",
            "UTF-16 BigEndian",
            "UTF-16 LittleEndian",
            "UTF-32",
            "UTF-7"});
            this.sbEncoding.Location = new System.Drawing.Point(6, 6);
            this.sbEncoding.Margin = new System.Windows.Forms.Padding(6, 6, 2, 2);
            this.sbEncoding.Name = "sbEncoding";
            this.sbEncoding.Size = new System.Drawing.Size(142, 21);
            this.sbEncoding.TabIndex = 17;
            this.sbEncoding.SelectedIndexChanged += new System.EventHandler(this.sbEncoding_SelectedIndexChanged);
            // 
            // sbSeparator
            // 
            this.sbSeparator.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sbSeparator.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.sbSeparator.FormattingEnabled = true;
            this.sbSeparator.Items.AddRange(new object[] {
            "Średnik",
            "Przecinek",
            "Kropka",
            "Tabulator",
            "Spacja",
            "Inny znak"});
            this.sbSeparator.Location = new System.Drawing.Point(6, 33);
            this.sbSeparator.Margin = new System.Windows.Forms.Padding(6, 3, 2, 3);
            this.sbSeparator.Name = "sbSeparator";
            this.sbSeparator.Size = new System.Drawing.Size(102, 21);
            this.sbSeparator.TabIndex = 21;
            this.sbSeparator.SelectedIndexChanged += new System.EventHandler(this.sbSeparator_SelectedIndexChanged);
            // 
            // cbAutoCheck
            // 
            this.cbAutoCheck.AutoSize = true;
            this.cbAutoCheck.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cbAutoCheck.Enabled = false;
            this.cbAutoCheck.Location = new System.Drawing.Point(154, 31);
            this.cbAutoCheck.Margin = new System.Windows.Forms.Padding(4, 1, 3, 3);
            this.cbAutoCheck.Name = "cbAutoCheck";
            this.cbAutoCheck.Size = new System.Drawing.Size(247, 26);
            this.cbAutoCheck.TabIndex = 22;
            this.cbAutoCheck.Text = "Automatycznie wykrywaj typy kolumn";
            this.cbAutoCheck.UseVisualStyleBackColor = true;
            // 
            // tbFileName
            // 
            this.tbFileName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbFileName.Location = new System.Drawing.Point(154, 6);
            this.tbFileName.Margin = new System.Windows.Forms.Padding(4, 6, 2, 3);
            this.tbFileName.Name = "tbFileName";
            this.tbFileName.Size = new System.Drawing.Size(248, 20);
            this.tbFileName.TabIndex = 23;
            this.tbFileName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbFileName_KeyPress);
            // 
            // tlButtons
            // 
            this.tlButtons.ColumnCount = 2;
            this.tlButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlButtons.Controls.Add(this.bChange, 0, 0);
            this.tlButtons.Controls.Add(this.bCancel, 1, 1);
            this.tlButtons.Controls.Add(this.bSave, 0, 1);
            this.tlButtons.Controls.Add(this.cbNoColumns, 1, 0);
            this.tlButtons.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlButtons.Location = new System.Drawing.Point(404, 1);
            this.tlButtons.Margin = new System.Windows.Forms.Padding(0, 1, 0, 0);
            this.tlButtons.Name = "tlButtons";
            this.tlButtons.RowCount = 2;
            this.tlButtons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tlButtons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tlButtons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlButtons.Size = new System.Drawing.Size(220, 60);
            this.tlButtons.TabIndex = 23;
            // 
            // bChange
            // 
            this.bChange.Dock = System.Windows.Forms.DockStyle.Fill;
            this.bChange.Location = new System.Drawing.Point(4, 3);
            this.bChange.Margin = new System.Windows.Forms.Padding(4, 3, 2, 2);
            this.bChange.Name = "bChange";
            this.bChange.Size = new System.Drawing.Size(104, 25);
            this.bChange.TabIndex = 1;
            this.bChange.Text = "Zmień plik";
            this.bChange.UseVisualStyleBackColor = true;
            this.bChange.Click += new System.EventHandler(this.bChange_Click);
            // 
            // bCancel
            // 
            this.bCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.bCancel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.bCancel.Location = new System.Drawing.Point(112, 32);
            this.bCancel.Margin = new System.Windows.Forms.Padding(2, 2, 6, 3);
            this.bCancel.Name = "bCancel";
            this.bCancel.Size = new System.Drawing.Size(102, 25);
            this.bCancel.TabIndex = 20;
            this.bCancel.Text = "Zaniechaj";
            this.bCancel.UseVisualStyleBackColor = true;
            this.bCancel.Click += new System.EventHandler(this.bCancel_Click);
            // 
            // bSave
            // 
            this.bSave.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.bSave.Dock = System.Windows.Forms.DockStyle.Fill;
            this.bSave.Location = new System.Drawing.Point(4, 32);
            this.bSave.Margin = new System.Windows.Forms.Padding(4, 2, 2, 3);
            this.bSave.Name = "bSave";
            this.bSave.Size = new System.Drawing.Size(104, 25);
            this.bSave.TabIndex = 0;
            this.bSave.Text = "Zatwierdź";
            this.bSave.UseVisualStyleBackColor = true;
            this.bSave.Click += new System.EventHandler(this.bSave_Click);
            // 
            // cbNoColumns
            // 
            this.cbNoColumns.AutoSize = true;
            this.cbNoColumns.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cbNoColumns.Location = new System.Drawing.Point(116, 5);
            this.cbNoColumns.Margin = new System.Windows.Forms.Padding(6, 5, 3, 3);
            this.cbNoColumns.Name = "cbNoColumns";
            this.cbNoColumns.Size = new System.Drawing.Size(101, 22);
            this.cbNoColumns.TabIndex = 21;
            this.cbNoColumns.Text = "Bez nagłówka";
            this.cbNoColumns.UseVisualStyleBackColor = true;
            this.cbNoColumns.CheckedChanged += new System.EventHandler(this.cbNoColumns_CheckedChanged);
            // 
            // DatafileSettingsForm
            // 
            this.AcceptButton = this.bSave;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.bCancel;
            this.ClientSize = new System.Drawing.Size(624, 388);
            this.Controls.Add(this.tlpMain);
            this.MaximumSize = new System.Drawing.Size(65535, 65535);
            this.MinimumSize = new System.Drawing.Size(640, 290);
            this.Name = "DatafileSettingsForm";
            this.Text = "Ustawienia pliku bazy danych";
            this.tlpMain.ResumeLayout(false);
            this.scMain.Panel1.ResumeLayout(false);
            this.scMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.scMain)).EndInit();
            this.scMain.ResumeLayout(false);
            this.tlpStatusBar.ResumeLayout(false);
            this.tlpSettings.ResumeLayout(false);
            this.tlpSettings.PerformLayout();
            this.tlButtons.ResumeLayout(false);
            this.tlButtons.PerformLayout();
            this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TableLayoutPanel tlpMain;
		private System.Windows.Forms.SplitContainer scMain;
		private System.Windows.Forms.ListView lvRows;
		private System.Windows.Forms.ColumnHeader lvcRows;
		private System.Windows.Forms.ListView lvColumns;
		private System.Windows.Forms.ColumnHeader lvcColumns;
		private System.Windows.Forms.TableLayoutPanel tlpStatusBar;
		private System.Windows.Forms.Button bCancel;
		private System.Windows.Forms.TextBox tbSeparator;
		private System.Windows.Forms.ComboBox sbEncoding;
		private System.Windows.Forms.Button bSave;
		private System.Windows.Forms.Button bChange;
		private System.Windows.Forms.ComboBox sbSeparator;
		private System.Windows.Forms.TableLayoutPanel tlpSettings;
		private System.Windows.Forms.TableLayoutPanel tlButtons;
		private System.Windows.Forms.CheckBox cbAutoCheck;
		private System.Windows.Forms.CheckBox cbNoColumns;
		private System.Windows.Forms.TextBox tbFileName;

        /// @endcond
	}
}