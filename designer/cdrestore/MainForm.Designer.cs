namespace CDRestore
{
	partial class MainForm
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
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.bRestore = new System.Windows.Forms.Button();
			this.bDelete = new System.Windows.Forms.Button();
			this.tcBackup = new System.Windows.Forms.TabControl();
			this.tpProgram = new System.Windows.Forms.TabPage();
			this.lvProgramBackup = new System.Windows.Forms.ListView();
			this.lvcName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.lvcDate = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.lvcVersion = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.tpPattern = new System.Windows.Forms.TabPage();
			this.lvPatternBackup = new System.Windows.Forms.ListView();
			this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.pbDecompress = new System.Windows.Forms.ProgressBar();
			this.lVersion = new System.Windows.Forms.Label();
			this.bwTask = new System.ComponentModel.BackgroundWorker();
			this.tableLayoutPanel1.SuspendLayout();
			this.tcBackup.SuspendLayout();
			this.tpProgram.SuspendLayout();
			this.tpPattern.SuspendLayout();
			this.SuspendLayout();
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.ColumnCount = 4;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
			this.tableLayoutPanel1.Controls.Add(this.bRestore, 0, 1);
			this.tableLayoutPanel1.Controls.Add(this.bDelete, 3, 1);
			this.tableLayoutPanel1.Controls.Add(this.tcBackup, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.pbDecompress, 1, 1);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 2;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(434, 266);
			this.tableLayoutPanel1.TabIndex = 0;
			// 
			// bRestore
			// 
			this.bRestore.Dock = System.Windows.Forms.DockStyle.Fill;
			this.bRestore.Enabled = false;
			this.bRestore.Location = new System.Drawing.Point(6, 241);
			this.bRestore.Margin = new System.Windows.Forms.Padding(6, 3, 3, 3);
			this.bRestore.Name = "bRestore";
			this.bRestore.Size = new System.Drawing.Size(99, 22);
			this.bRestore.TabIndex = 0;
			this.bRestore.Text = "Przywróć";
			this.bRestore.UseVisualStyleBackColor = true;
			this.bRestore.Click += new System.EventHandler(this.bRestore_Click);
			// 
			// bDelete
			// 
			this.bDelete.Dock = System.Windows.Forms.DockStyle.Fill;
			this.bDelete.Enabled = false;
			this.bDelete.Location = new System.Drawing.Point(327, 241);
			this.bDelete.Margin = new System.Windows.Forms.Padding(3, 3, 6, 3);
			this.bDelete.Name = "bDelete";
			this.bDelete.Size = new System.Drawing.Size(101, 22);
			this.bDelete.TabIndex = 1;
			this.bDelete.Text = "Usuń";
			this.bDelete.UseVisualStyleBackColor = true;
			this.bDelete.Click += new System.EventHandler(this.bDelete_Click);
			// 
			// tcBackup
			// 
			this.tableLayoutPanel1.SetColumnSpan(this.tcBackup, 4);
			this.tcBackup.Controls.Add(this.tpProgram);
			this.tcBackup.Controls.Add(this.tpPattern);
			this.tcBackup.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tcBackup.Location = new System.Drawing.Point(6, 6);
			this.tcBackup.Margin = new System.Windows.Forms.Padding(6, 6, 6, 3);
			this.tcBackup.Multiline = true;
			this.tcBackup.Name = "tcBackup";
			this.tcBackup.SelectedIndex = 0;
			this.tcBackup.Size = new System.Drawing.Size(422, 229);
			this.tcBackup.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
			this.tcBackup.TabIndex = 3;
			this.tcBackup.Click += new System.EventHandler(this.tcBackup_Click);
			// 
			// tpProgram
			// 
			this.tpProgram.Controls.Add(this.lvProgramBackup);
			this.tpProgram.Location = new System.Drawing.Point(4, 22);
			this.tpProgram.Name = "tpProgram";
			this.tpProgram.Padding = new System.Windows.Forms.Padding(3);
			this.tpProgram.Size = new System.Drawing.Size(414, 203);
			this.tpProgram.TabIndex = 0;
			this.tpProgram.Text = "Program";
			this.tpProgram.UseVisualStyleBackColor = true;
			// 
			// lvProgramBackup
			// 
			this.lvProgramBackup.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.lvcName,
            this.lvcDate,
            this.lvcVersion});
			this.lvProgramBackup.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lvProgramBackup.FullRowSelect = true;
			this.lvProgramBackup.GridLines = true;
			this.lvProgramBackup.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
			this.lvProgramBackup.HideSelection = false;
			this.lvProgramBackup.Location = new System.Drawing.Point(3, 3);
			this.lvProgramBackup.Margin = new System.Windows.Forms.Padding(0);
			this.lvProgramBackup.MultiSelect = false;
			this.lvProgramBackup.Name = "lvProgramBackup";
			this.lvProgramBackup.ShowGroups = false;
			this.lvProgramBackup.Size = new System.Drawing.Size(408, 197);
			this.lvProgramBackup.TabIndex = 4;
			this.lvProgramBackup.UseCompatibleStateImageBehavior = false;
			this.lvProgramBackup.View = System.Windows.Forms.View.Details;
			this.lvProgramBackup.SelectedIndexChanged += new System.EventHandler(this.lvProgramBackup_SelectedIndexChanged);
			// 
			// lvcName
			// 
			this.lvcName.Text = "Nazwa";
			this.lvcName.Width = 180;
			// 
			// lvcDate
			// 
			this.lvcDate.Text = "Data";
			this.lvcDate.Width = 112;
			// 
			// lvcVersion
			// 
			this.lvcVersion.Text = "Wersja";
			this.lvcVersion.Width = 112;
			// 
			// tpPattern
			// 
			this.tpPattern.Controls.Add(this.lvPatternBackup);
			this.tpPattern.Location = new System.Drawing.Point(4, 22);
			this.tpPattern.Name = "tpPattern";
			this.tpPattern.Padding = new System.Windows.Forms.Padding(3);
			this.tpPattern.Size = new System.Drawing.Size(414, 203);
			this.tpPattern.TabIndex = 1;
			this.tpPattern.Text = "Wzory";
			this.tpPattern.UseVisualStyleBackColor = true;
			// 
			// lvPatternBackup
			// 
			this.lvPatternBackup.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3});
			this.lvPatternBackup.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lvPatternBackup.FullRowSelect = true;
			this.lvPatternBackup.GridLines = true;
			this.lvPatternBackup.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
			this.lvPatternBackup.HideSelection = false;
			this.lvPatternBackup.Location = new System.Drawing.Point(3, 3);
			this.lvPatternBackup.Margin = new System.Windows.Forms.Padding(0);
			this.lvPatternBackup.MultiSelect = false;
			this.lvPatternBackup.Name = "lvPatternBackup";
			this.lvPatternBackup.ShowGroups = false;
			this.lvPatternBackup.Size = new System.Drawing.Size(408, 197);
			this.lvPatternBackup.TabIndex = 5;
			this.lvPatternBackup.UseCompatibleStateImageBehavior = false;
			this.lvPatternBackup.View = System.Windows.Forms.View.Details;
			this.lvPatternBackup.SelectedIndexChanged += new System.EventHandler(this.lvPatternBackup_SelectedIndexChanged);
			// 
			// columnHeader1
			// 
			this.columnHeader1.Text = "Nazwa";
			this.columnHeader1.Width = 180;
			// 
			// columnHeader2
			// 
			this.columnHeader2.Text = "Data";
			this.columnHeader2.Width = 112;
			// 
			// columnHeader3
			// 
			this.columnHeader3.Text = "Wersja";
			this.columnHeader3.Width = 112;
			// 
			// pbDecompress
			// 
			this.tableLayoutPanel1.SetColumnSpan(this.pbDecompress, 2);
			this.pbDecompress.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pbDecompress.Location = new System.Drawing.Point(111, 243);
			this.pbDecompress.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
			this.pbDecompress.Name = "pbDecompress";
			this.pbDecompress.Size = new System.Drawing.Size(210, 18);
			this.pbDecompress.TabIndex = 4;
			// 
			// lVersion
			// 
			this.lVersion.ForeColor = System.Drawing.Color.Gray;
			this.lVersion.Location = new System.Drawing.Point(223, 0);
			this.lVersion.Name = "lVersion";
			this.lVersion.Size = new System.Drawing.Size(205, 25);
			this.lVersion.TabIndex = 4;
			this.lVersion.Text = "Zainstalowana wersja: 0.7.2140.5125";
			this.lVersion.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// bwTask
			// 
			this.bwTask.WorkerReportsProgress = true;
			this.bwTask.WorkerSupportsCancellation = true;
			// 
			// Main
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(434, 266);
			this.Controls.Add(this.lVersion);
			this.Controls.Add(this.tableLayoutPanel1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Name = "Main";
			this.Text = "CDRestore - Kopia zapasowa";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Main_FormClosing);
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tcBackup.ResumeLayout(false);
			this.tpProgram.ResumeLayout(false);
			this.tpPattern.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.Button bRestore;
		private System.Windows.Forms.Button bDelete;
		private System.Windows.Forms.TabControl tcBackup;
		private System.Windows.Forms.TabPage tpProgram;
		private System.Windows.Forms.ListView lvProgramBackup;
		private System.Windows.Forms.ColumnHeader lvcName;
		private System.Windows.Forms.ColumnHeader lvcDate;
		private System.Windows.Forms.ColumnHeader lvcVersion;
		private System.Windows.Forms.TabPage tpPattern;
		private System.Windows.Forms.ListView lvPatternBackup;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private System.Windows.Forms.ColumnHeader columnHeader3;
		private System.Windows.Forms.Label lVersion;
		private System.Windows.Forms.ProgressBar pbDecompress;
		private System.ComponentModel.BackgroundWorker bwTask;
	}
}

