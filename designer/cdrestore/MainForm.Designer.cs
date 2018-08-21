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
            this.TLP_Main = new System.Windows.Forms.TableLayoutPanel();
            this.TLP_StatusBar = new System.Windows.Forms.TableLayoutPanel();
            this.B_Delete = new System.Windows.Forms.Button();
            this.PB_Decompress = new System.Windows.Forms.ProgressBar();
            this.B_Restore = new System.Windows.Forms.Button();
            this.TC_Backups = new System.Windows.Forms.TabControl();
            this.TP_Program = new System.Windows.Forms.TabPage();
            this.LV_Program = new System.Windows.Forms.ListView();
            this.CH_ProgramName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.CH_ProgramDate = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.CH_ProgramVersion = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.TP_Pattern = new System.Windows.Forms.TabPage();
            this.LV_Pattern = new System.Windows.Forms.ListView();
            this.CH_PatternName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.CH_PatternDate = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.CH_PatternVersion = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.L_Version = new System.Windows.Forms.Label();
            this.BW_Task = new System.ComponentModel.BackgroundWorker();
            this.TLP_Main.SuspendLayout();
            this.TLP_StatusBar.SuspendLayout();
            this.TC_Backups.SuspendLayout();
            this.TP_Program.SuspendLayout();
            this.TP_Pattern.SuspendLayout();
            this.SuspendLayout();
            // 
            // TLP_Main
            // 
            this.TLP_Main.ColumnCount = 4;
            this.TLP_Main.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.TLP_Main.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.TLP_Main.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.TLP_Main.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.TLP_Main.Controls.Add(this.TLP_StatusBar, 0, 1);
            this.TLP_Main.Controls.Add(this.TC_Backups, 0, 0);
            this.TLP_Main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TLP_Main.Location = new System.Drawing.Point(0, 0);
            this.TLP_Main.Name = "TLP_Main";
            this.TLP_Main.RowCount = 2;
            this.TLP_Main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.TLP_Main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.TLP_Main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.TLP_Main.Size = new System.Drawing.Size(454, 276);
            this.TLP_Main.TabIndex = 0;
            // 
            // TLP_StatusBar
            // 
            this.TLP_StatusBar.BackColor = System.Drawing.SystemColors.ControlLight;
            this.TLP_StatusBar.ColumnCount = 3;
            this.TLP_Main.SetColumnSpan(this.TLP_StatusBar, 4);
            this.TLP_StatusBar.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.TLP_StatusBar.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.TLP_StatusBar.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.TLP_StatusBar.Controls.Add(this.B_Delete, 0, 0);
            this.TLP_StatusBar.Controls.Add(this.PB_Decompress, 0, 0);
            this.TLP_StatusBar.Controls.Add(this.B_Restore, 0, 0);
            this.TLP_StatusBar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TLP_StatusBar.Location = new System.Drawing.Point(0, 244);
            this.TLP_StatusBar.Margin = new System.Windows.Forms.Padding(0);
            this.TLP_StatusBar.Name = "TLP_StatusBar";
            this.TLP_StatusBar.Padding = new System.Windows.Forms.Padding(0, 1, 0, 0);
            this.TLP_StatusBar.RowCount = 1;
            this.TLP_StatusBar.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.TLP_StatusBar.Size = new System.Drawing.Size(454, 32);
            this.TLP_StatusBar.TabIndex = 5;
            this.TLP_StatusBar.Paint += new System.Windows.Forms.PaintEventHandler(this.TLP_StatusBar_Paint);
            // 
            // B_Delete
            // 
            this.B_Delete.Dock = System.Windows.Forms.DockStyle.Fill;
            this.B_Delete.Enabled = false;
            this.B_Delete.Location = new System.Drawing.Point(343, 4);
            this.B_Delete.Margin = new System.Windows.Forms.Padding(3, 3, 6, 3);
            this.B_Delete.Name = "B_Delete";
            this.B_Delete.Size = new System.Drawing.Size(105, 25);
            this.B_Delete.TabIndex = 1;
            this.B_Delete.Text = "Usuń";
            this.B_Delete.UseVisualStyleBackColor = true;
            this.B_Delete.Click += new System.EventHandler(this.bDelete_Click);
            // 
            // PB_Decompress
            // 
            this.PB_Decompress.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PB_Decompress.Location = new System.Drawing.Point(116, 6);
            this.PB_Decompress.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.PB_Decompress.Name = "PB_Decompress";
            this.PB_Decompress.Size = new System.Drawing.Size(221, 21);
            this.PB_Decompress.TabIndex = 4;
            // 
            // B_Restore
            // 
            this.B_Restore.Dock = System.Windows.Forms.DockStyle.Fill;
            this.B_Restore.Enabled = false;
            this.B_Restore.Location = new System.Drawing.Point(6, 4);
            this.B_Restore.Margin = new System.Windows.Forms.Padding(6, 3, 3, 3);
            this.B_Restore.Name = "B_Restore";
            this.B_Restore.Size = new System.Drawing.Size(104, 25);
            this.B_Restore.TabIndex = 0;
            this.B_Restore.Text = "Przywróć";
            this.B_Restore.UseVisualStyleBackColor = true;
            this.B_Restore.Click += new System.EventHandler(this.bRestore_Click);
            // 
            // TC_Backups
            // 
            this.TLP_Main.SetColumnSpan(this.TC_Backups, 4);
            this.TC_Backups.Controls.Add(this.TP_Program);
            this.TC_Backups.Controls.Add(this.TP_Pattern);
            this.TC_Backups.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TC_Backups.Location = new System.Drawing.Point(6, 6);
            this.TC_Backups.Margin = new System.Windows.Forms.Padding(6);
            this.TC_Backups.Multiline = true;
            this.TC_Backups.Name = "TC_Backups";
            this.TC_Backups.Padding = new System.Drawing.Point(6, 4);
            this.TC_Backups.SelectedIndex = 0;
            this.TC_Backups.Size = new System.Drawing.Size(442, 232);
            this.TC_Backups.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.TC_Backups.TabIndex = 3;
            this.TC_Backups.Click += new System.EventHandler(this.tcBackup_Click);
            // 
            // TP_Program
            // 
            this.TP_Program.Controls.Add(this.LV_Program);
            this.TP_Program.Location = new System.Drawing.Point(4, 24);
            this.TP_Program.Name = "TP_Program";
            this.TP_Program.Padding = new System.Windows.Forms.Padding(3);
            this.TP_Program.Size = new System.Drawing.Size(434, 204);
            this.TP_Program.TabIndex = 0;
            this.TP_Program.Text = "Program";
            this.TP_Program.UseVisualStyleBackColor = true;
            // 
            // LV_Program
            // 
            this.LV_Program.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.CH_ProgramName,
            this.CH_ProgramDate,
            this.CH_ProgramVersion});
            this.LV_Program.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LV_Program.FullRowSelect = true;
            this.LV_Program.GridLines = true;
            this.LV_Program.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.LV_Program.HideSelection = false;
            this.LV_Program.Location = new System.Drawing.Point(3, 3);
            this.LV_Program.Margin = new System.Windows.Forms.Padding(0);
            this.LV_Program.MultiSelect = false;
            this.LV_Program.Name = "LV_Program";
            this.LV_Program.ShowGroups = false;
            this.LV_Program.Size = new System.Drawing.Size(428, 198);
            this.LV_Program.TabIndex = 4;
            this.LV_Program.UseCompatibleStateImageBehavior = false;
            this.LV_Program.View = System.Windows.Forms.View.Details;
            this.LV_Program.SelectedIndexChanged += new System.EventHandler(this.lvProgramBackup_SelectedIndexChanged);
            // 
            // CH_ProgramName
            // 
            this.CH_ProgramName.Text = "Nazwa";
            this.CH_ProgramName.Width = 200;
            // 
            // CH_ProgramDate
            // 
            this.CH_ProgramDate.Text = "Data";
            this.CH_ProgramDate.Width = 112;
            // 
            // CH_ProgramVersion
            // 
            this.CH_ProgramVersion.Text = "Wersja";
            this.CH_ProgramVersion.Width = 112;
            // 
            // TP_Pattern
            // 
            this.TP_Pattern.Controls.Add(this.LV_Pattern);
            this.TP_Pattern.Location = new System.Drawing.Point(4, 24);
            this.TP_Pattern.Name = "TP_Pattern";
            this.TP_Pattern.Padding = new System.Windows.Forms.Padding(3);
            this.TP_Pattern.Size = new System.Drawing.Size(434, 204);
            this.TP_Pattern.TabIndex = 1;
            this.TP_Pattern.Text = "Wzory";
            this.TP_Pattern.UseVisualStyleBackColor = true;
            // 
            // LV_Pattern
            // 
            this.LV_Pattern.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.CH_PatternName,
            this.CH_PatternDate,
            this.CH_PatternVersion});
            this.LV_Pattern.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LV_Pattern.FullRowSelect = true;
            this.LV_Pattern.GridLines = true;
            this.LV_Pattern.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.LV_Pattern.HideSelection = false;
            this.LV_Pattern.Location = new System.Drawing.Point(3, 3);
            this.LV_Pattern.Margin = new System.Windows.Forms.Padding(0);
            this.LV_Pattern.MultiSelect = false;
            this.LV_Pattern.Name = "LV_Pattern";
            this.LV_Pattern.ShowGroups = false;
            this.LV_Pattern.Size = new System.Drawing.Size(428, 198);
            this.LV_Pattern.TabIndex = 5;
            this.LV_Pattern.UseCompatibleStateImageBehavior = false;
            this.LV_Pattern.View = System.Windows.Forms.View.Details;
            this.LV_Pattern.SelectedIndexChanged += new System.EventHandler(this.lvPatternBackup_SelectedIndexChanged);
            // 
            // CH_PatternName
            // 
            this.CH_PatternName.Text = "Nazwa";
            this.CH_PatternName.Width = 180;
            // 
            // CH_PatternDate
            // 
            this.CH_PatternDate.Text = "Data";
            this.CH_PatternDate.Width = 112;
            // 
            // CH_PatternVersion
            // 
            this.CH_PatternVersion.Text = "Wersja";
            this.CH_PatternVersion.Width = 112;
            // 
            // L_Version
            // 
            this.L_Version.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.L_Version.ForeColor = System.Drawing.Color.Gray;
            this.L_Version.Location = new System.Drawing.Point(243, 0);
            this.L_Version.Name = "L_Version";
            this.L_Version.Size = new System.Drawing.Size(205, 25);
            this.L_Version.TabIndex = 4;
            this.L_Version.Text = "Zainstalowana wersja: 0.7.2140.5125";
            this.L_Version.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // BW_Task
            // 
            this.BW_Task.WorkerReportsProgress = true;
            this.BW_Task.WorkerSupportsCancellation = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(454, 276);
            this.Controls.Add(this.L_Version);
            this.Controls.Add(this.TLP_Main);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "MainForm";
            this.Text = "CDRestore - Kopia zapasowa";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Main_FormClosing);
            this.TLP_Main.ResumeLayout(false);
            this.TLP_StatusBar.ResumeLayout(false);
            this.TC_Backups.ResumeLayout(false);
            this.TP_Program.ResumeLayout(false);
            this.TP_Pattern.ResumeLayout(false);
            this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TableLayoutPanel TLP_Main;
		private System.Windows.Forms.Button B_Restore;
		private System.Windows.Forms.Button B_Delete;
		private System.Windows.Forms.TabControl TC_Backups;
		private System.Windows.Forms.TabPage TP_Program;
		private System.Windows.Forms.ListView LV_Program;
		private System.Windows.Forms.ColumnHeader CH_ProgramName;
		private System.Windows.Forms.ColumnHeader CH_ProgramDate;
		private System.Windows.Forms.ColumnHeader CH_ProgramVersion;
		private System.Windows.Forms.TabPage TP_Pattern;
		private System.Windows.Forms.ListView LV_Pattern;
		private System.Windows.Forms.ColumnHeader CH_PatternName;
		private System.Windows.Forms.ColumnHeader CH_PatternDate;
		private System.Windows.Forms.ColumnHeader CH_PatternVersion;
		private System.Windows.Forms.Label L_Version;
		private System.Windows.Forms.ProgressBar PB_Decompress;
		private System.ComponentModel.BackgroundWorker BW_Task;
        private System.Windows.Forms.TableLayoutPanel TLP_StatusBar;
	}
}

