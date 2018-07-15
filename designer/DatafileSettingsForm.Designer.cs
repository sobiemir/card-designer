namespace CDesigner
{
	partial class DatafileSettingsForm
	{
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
			this.tMainPanel = new System.Windows.Forms.TableLayoutPanel();
			this.lvColumns = new System.Windows.Forms.ListView();
			this.lvcColumns = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.lvRows = new System.Windows.Forms.ListView();
			this.lvcRows = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.tlStatusBar = new System.Windows.Forms.TableLayoutPanel();
			this.tbSeparator = new System.Windows.Forms.TextBox();
			this.cbEncoding = new System.Windows.Forms.ComboBox();
			this.bSave = new System.Windows.Forms.Button();
			this.bChangeFile = new System.Windows.Forms.Button();
			this.tMainPanel.SuspendLayout();
			this.tlStatusBar.SuspendLayout();
			this.SuspendLayout();
			// 
			// tMainPanel
			// 
			this.tMainPanel.ColumnCount = 5;
			this.tMainPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
			this.tMainPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 6F));
			this.tMainPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33F));
			this.tMainPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 18F));
			this.tMainPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 18F));
			this.tMainPanel.Controls.Add(this.lvColumns, 3, 0);
			this.tMainPanel.Controls.Add(this.lvRows, 0, 0);
			this.tMainPanel.Controls.Add(this.tlStatusBar, 0, 1);
			this.tMainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tMainPanel.Location = new System.Drawing.Point(0, 0);
			this.tMainPanel.Name = "tMainPanel";
			this.tMainPanel.RowCount = 2;
			this.tMainPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tMainPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 31F));
			this.tMainPanel.Size = new System.Drawing.Size(624, 362);
			this.tMainPanel.TabIndex = 2;
			// 
			// lvColumns
			// 
			this.lvColumns.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.lvcColumns});
			this.tMainPanel.SetColumnSpan(this.lvColumns, 2);
			this.lvColumns.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lvColumns.FullRowSelect = true;
			this.lvColumns.GridLines = true;
			this.lvColumns.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
			this.lvColumns.HideSelection = false;
			this.lvColumns.Location = new System.Drawing.Point(401, 6);
			this.lvColumns.Margin = new System.Windows.Forms.Padding(3, 6, 6, 6);
			this.lvColumns.MultiSelect = false;
			this.lvColumns.Name = "lvColumns";
			this.lvColumns.Size = new System.Drawing.Size(217, 319);
			this.lvColumns.TabIndex = 21;
			this.lvColumns.UseCompatibleStateImageBehavior = false;
			this.lvColumns.View = System.Windows.Forms.View.Details;
			this.lvColumns.SelectedIndexChanged += new System.EventHandler(this.lvColumns_SelectedIndexChanged);
			// 
			// lvcColumns
			// 
			this.lvcColumns.Text = "Kolumny";
			this.lvcColumns.Width = 213;
			// 
			// lvRows
			// 
			this.lvRows.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.lvcRows});
			this.tMainPanel.SetColumnSpan(this.lvRows, 3);
			this.lvRows.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lvRows.FullRowSelect = true;
			this.lvRows.GridLines = true;
			this.lvRows.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
			this.lvRows.HideSelection = false;
			this.lvRows.Location = new System.Drawing.Point(6, 6);
			this.lvRows.Margin = new System.Windows.Forms.Padding(6, 6, 3, 6);
			this.lvRows.MultiSelect = false;
			this.lvRows.Name = "lvRows";
			this.lvRows.Size = new System.Drawing.Size(389, 319);
			this.lvRows.TabIndex = 22;
			this.lvRows.UseCompatibleStateImageBehavior = false;
			this.lvRows.View = System.Windows.Forms.View.Details;
			// 
			// lvcRows
			// 
			this.lvcRows.Text = "Podgląd wierszy";
			this.lvcRows.Width = 385;
			// 
			// tlStatusBar
			// 
			this.tlStatusBar.BackColor = System.Drawing.SystemColors.ControlLight;
			this.tlStatusBar.ColumnCount = 5;
			this.tMainPanel.SetColumnSpan(this.tlStatusBar, 5);
			this.tlStatusBar.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 21F));
			this.tlStatusBar.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 5F));
			this.tlStatusBar.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 38F));
			this.tlStatusBar.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 18F));
			this.tlStatusBar.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 18F));
			this.tlStatusBar.Controls.Add(this.tbSeparator, 0, 0);
			this.tlStatusBar.Controls.Add(this.cbEncoding, 0, 0);
			this.tlStatusBar.Controls.Add(this.bSave, 4, 0);
			this.tlStatusBar.Controls.Add(this.bChangeFile, 3, 0);
			this.tlStatusBar.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tlStatusBar.Location = new System.Drawing.Point(0, 331);
			this.tlStatusBar.Margin = new System.Windows.Forms.Padding(0);
			this.tlStatusBar.Name = "tlStatusBar";
			this.tlStatusBar.RowCount = 1;
			this.tlStatusBar.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tlStatusBar.Size = new System.Drawing.Size(624, 31);
			this.tlStatusBar.TabIndex = 23;
			this.tlStatusBar.Paint += new System.Windows.Forms.PaintEventHandler(this.tlStatusBar_Paint);
			// 
			// tbSeparator
			// 
			this.tbSeparator.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tbSeparator.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.tbSeparator.Location = new System.Drawing.Point(134, 5);
			this.tbSeparator.Margin = new System.Windows.Forms.Padding(3, 5, 3, 3);
			this.tbSeparator.MaxLength = 1;
			this.tbSeparator.Name = "tbSeparator";
			this.tbSeparator.Size = new System.Drawing.Size(25, 20);
			this.tbSeparator.TabIndex = 19;
			this.tbSeparator.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.tbSeparator.TextChanged += new System.EventHandler(this.tbSeparator_TextChanged);
			// 
			// cbEncoding
			// 
			this.cbEncoding.Dock = System.Windows.Forms.DockStyle.Fill;
			this.cbEncoding.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbEncoding.FormattingEnabled = true;
			this.cbEncoding.Items.AddRange(new object[] {
            "Domyślne",
            "ASCII",
            "UTF-8",
            "UTF-16 BigEndian",
            "UTF-16 LittleEndian",
            "UTF-32",
            "UTF-7"});
			this.cbEncoding.Location = new System.Drawing.Point(6, 5);
			this.cbEncoding.Margin = new System.Windows.Forms.Padding(6, 5, 3, 3);
			this.cbEncoding.Name = "cbEncoding";
			this.cbEncoding.Size = new System.Drawing.Size(122, 21);
			this.cbEncoding.TabIndex = 17;
			this.cbEncoding.SelectedIndexChanged += new System.EventHandler(this.cbEncoding_SelectedIndexChanged);
			// 
			// bSave
			// 
			this.bSave.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.bSave.Dock = System.Windows.Forms.DockStyle.Fill;
			this.bSave.Location = new System.Drawing.Point(514, 4);
			this.bSave.Margin = new System.Windows.Forms.Padding(3, 4, 6, 3);
			this.bSave.Name = "bSave";
			this.bSave.Size = new System.Drawing.Size(104, 24);
			this.bSave.TabIndex = 0;
			this.bSave.Text = "Zatwierdź";
			this.bSave.UseVisualStyleBackColor = true;
			this.bSave.Click += new System.EventHandler(this.bSave_Click);
			// 
			// bChangeFile
			// 
			this.bChangeFile.Dock = System.Windows.Forms.DockStyle.Fill;
			this.bChangeFile.Location = new System.Drawing.Point(400, 4);
			this.bChangeFile.Margin = new System.Windows.Forms.Padding(1, 4, 3, 3);
			this.bChangeFile.Name = "bChangeFile";
			this.bChangeFile.Size = new System.Drawing.Size(108, 24);
			this.bChangeFile.TabIndex = 1;
			this.bChangeFile.Text = "Zmień";
			this.bChangeFile.UseVisualStyleBackColor = true;
			this.bChangeFile.Click += new System.EventHandler(this.bChangeFile_Click);
			// 
			// DatabaseSettingsForm
			// 
			this.AcceptButton = this.bSave;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(624, 362);
			this.Controls.Add(this.tMainPanel);
			this.MaximizeBox = false;
			this.MaximumSize = new System.Drawing.Size(640, 65535);
			this.MinimumSize = new System.Drawing.Size(640, 400);
			this.Name = "DatabaseSettingsForm";
			this.Text = "Ustawienia pliku bazy danych";
			this.tMainPanel.ResumeLayout(false);
			this.tlStatusBar.ResumeLayout(false);
			this.tlStatusBar.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TableLayoutPanel tMainPanel;
		private System.Windows.Forms.Button bSave;
		private System.Windows.Forms.ComboBox cbEncoding;
		private System.Windows.Forms.TextBox tbSeparator;
		private System.Windows.Forms.Button bChangeFile;
		private System.Windows.Forms.ListView lvColumns;
		private System.Windows.Forms.ListView lvRows;
		private System.Windows.Forms.ColumnHeader lvcColumns;
		private System.Windows.Forms.ColumnHeader lvcRows;
		private System.Windows.Forms.TableLayoutPanel tlStatusBar;
	}
}