namespace CDesigner.Forms
{
	partial class UpdateForm
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
			this.L_CurVersion = new System.Windows.Forms.Label();
			this.L_UpdateAvailable = new System.Windows.Forms.Label();
			this.L_NewVersion = new System.Windows.Forms.Label();
			this.TLP_StatusBar = new System.Windows.Forms.TableLayoutPanel();
			this.B_Update = new System.Windows.Forms.Button();
			this.PRB_Update = new System.Windows.Forms.ProgressBar();
			this.B_Close = new System.Windows.Forms.Button();
			this.PB_AppLogo = new System.Windows.Forms.PictureBox();
			this.GB_ChangeLog = new System.Windows.Forms.GroupBox();
			this.RTB_Changes = new System.Windows.Forms.RichTextBox();
			this.BW_Update = new System.ComponentModel.BackgroundWorker();
			this.TLP_Main.SuspendLayout();
			this.TLP_StatusBar.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.PB_AppLogo)).BeginInit();
			this.GB_ChangeLog.SuspendLayout();
			this.SuspendLayout();
			// 
			// TLP_Main
			// 
			this.TLP_Main.ColumnCount = 2;
			this.TLP_Main.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.TLP_Main.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 82F));
			this.TLP_Main.Controls.Add(this.L_CurVersion, 0, 1);
			this.TLP_Main.Controls.Add(this.L_UpdateAvailable, 0, 0);
			this.TLP_Main.Controls.Add(this.L_NewVersion, 0, 2);
			this.TLP_Main.Controls.Add(this.TLP_StatusBar, 0, 5);
			this.TLP_Main.Controls.Add(this.PB_AppLogo, 1, 0);
			this.TLP_Main.Controls.Add(this.GB_ChangeLog, 0, 4);
			this.TLP_Main.Dock = System.Windows.Forms.DockStyle.Fill;
			this.TLP_Main.Location = new System.Drawing.Point(0, 0);
			this.TLP_Main.Name = "TLP_Main";
			this.TLP_Main.RowCount = 6;
			this.TLP_Main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22F));
			this.TLP_Main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22F));
			this.TLP_Main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22F));
			this.TLP_Main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 16F));
			this.TLP_Main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.TLP_Main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
			this.TLP_Main.Size = new System.Drawing.Size(454, 372);
			this.TLP_Main.TabIndex = 0;
			// 
			// L_CurVersion
			// 
			this.L_CurVersion.AutoSize = true;
			this.L_CurVersion.ForeColor = System.Drawing.Color.Gray;
			this.L_CurVersion.Location = new System.Drawing.Point(6, 28);
			this.L_CurVersion.Margin = new System.Windows.Forms.Padding(6, 6, 0, 0);
			this.L_CurVersion.Name = "L_CurVersion";
			this.L_CurVersion.Size = new System.Drawing.Size(81, 13);
			this.L_CurVersion.TabIndex = 10;
			this.L_CurVersion.Text = "Obecna wersja:";
			// 
			// L_UpdateAvailable
			// 
			this.L_UpdateAvailable.AutoSize = true;
			this.L_UpdateAvailable.Location = new System.Drawing.Point(6, 6);
			this.L_UpdateAvailable.Margin = new System.Windows.Forms.Padding(6, 6, 0, 0);
			this.L_UpdateAvailable.Name = "L_UpdateAvailable";
			this.L_UpdateAvailable.Size = new System.Drawing.Size(184, 13);
			this.L_UpdateAvailable.TabIndex = 10;
			this.L_UpdateAvailable.Text = "Dostępna jest nowa wersja programu!";
			// 
			// L_NewVersion
			// 
			this.L_NewVersion.AutoSize = true;
			this.L_NewVersion.ForeColor = System.Drawing.Color.Gray;
			this.L_NewVersion.Location = new System.Drawing.Point(6, 50);
			this.L_NewVersion.Margin = new System.Windows.Forms.Padding(6, 6, 0, 0);
			this.L_NewVersion.Name = "L_NewVersion";
			this.L_NewVersion.Size = new System.Drawing.Size(113, 13);
			this.L_NewVersion.TabIndex = 11;
			this.L_NewVersion.Text = "Wersja po aktualizacji:";
			// 
			// TLP_StatusBar
			// 
			this.TLP_StatusBar.BackColor = System.Drawing.SystemColors.ControlLight;
			this.TLP_StatusBar.ColumnCount = 3;
			this.TLP_Main.SetColumnSpan(this.TLP_StatusBar, 2);
			this.TLP_StatusBar.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 110F));
			this.TLP_StatusBar.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.TLP_StatusBar.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 110F));
			this.TLP_StatusBar.Controls.Add(this.B_Update, 0, 0);
			this.TLP_StatusBar.Controls.Add(this.PRB_Update, 1, 0);
			this.TLP_StatusBar.Controls.Add(this.B_Close, 2, 0);
			this.TLP_StatusBar.Dock = System.Windows.Forms.DockStyle.Fill;
			this.TLP_StatusBar.Location = new System.Drawing.Point(0, 340);
			this.TLP_StatusBar.Margin = new System.Windows.Forms.Padding(0);
			this.TLP_StatusBar.Name = "TLP_StatusBar";
			this.TLP_StatusBar.Padding = new System.Windows.Forms.Padding(0, 1, 0, 0);
			this.TLP_StatusBar.RowCount = 1;
			this.TLP_StatusBar.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.TLP_StatusBar.Size = new System.Drawing.Size(454, 32);
			this.TLP_StatusBar.TabIndex = 7;
			this.TLP_StatusBar.Paint += new System.Windows.Forms.PaintEventHandler(this.TLP_StatusBar_Paint);
			// 
			// B_Update
			// 
			this.B_Update.Dock = System.Windows.Forms.DockStyle.Fill;
			this.B_Update.Location = new System.Drawing.Point(6, 4);
			this.B_Update.Margin = new System.Windows.Forms.Padding(6, 3, 1, 3);
			this.B_Update.Name = "B_Update";
			this.B_Update.Size = new System.Drawing.Size(103, 25);
			this.B_Update.TabIndex = 4;
			this.B_Update.Text = "Aktualizuj";
			this.B_Update.UseVisualStyleBackColor = true;
			this.B_Update.Click += new System.EventHandler(this.B_Update_Click);
			// 
			// PRB_Update
			// 
			this.PRB_Update.Dock = System.Windows.Forms.DockStyle.Fill;
			this.PRB_Update.Location = new System.Drawing.Point(112, 5);
			this.PRB_Update.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
			this.PRB_Update.Name = "PRB_Update";
			this.PRB_Update.Size = new System.Drawing.Size(230, 23);
			this.PRB_Update.TabIndex = 3;
			// 
			// B_Close
			// 
			this.B_Close.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.B_Close.Dock = System.Windows.Forms.DockStyle.Fill;
			this.B_Close.Location = new System.Drawing.Point(345, 4);
			this.B_Close.Margin = new System.Windows.Forms.Padding(1, 3, 6, 3);
			this.B_Close.Name = "B_Close";
			this.B_Close.Size = new System.Drawing.Size(103, 25);
			this.B_Close.TabIndex = 5;
			this.B_Close.Text = "Zamknij";
			this.B_Close.UseVisualStyleBackColor = true;
			// 
			// PB_AppLogo
			// 
			this.PB_AppLogo.Dock = System.Windows.Forms.DockStyle.Fill;
			this.PB_AppLogo.Location = new System.Drawing.Point(372, 6);
			this.PB_AppLogo.Margin = new System.Windows.Forms.Padding(0, 6, 6, 0);
			this.PB_AppLogo.Name = "PB_AppLogo";
			this.TLP_Main.SetRowSpan(this.PB_AppLogo, 4);
			this.PB_AppLogo.Size = new System.Drawing.Size(76, 76);
			this.PB_AppLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this.PB_AppLogo.TabIndex = 8;
			this.PB_AppLogo.TabStop = false;
			// 
			// GB_ChangeLog
			// 
			this.TLP_Main.SetColumnSpan(this.GB_ChangeLog, 2);
			this.GB_ChangeLog.Controls.Add(this.RTB_Changes);
			this.GB_ChangeLog.Dock = System.Windows.Forms.DockStyle.Fill;
			this.GB_ChangeLog.Location = new System.Drawing.Point(6, 82);
			this.GB_ChangeLog.Margin = new System.Windows.Forms.Padding(6, 0, 6, 6);
			this.GB_ChangeLog.Name = "GB_ChangeLog";
			this.GB_ChangeLog.Padding = new System.Windows.Forms.Padding(6, 4, 6, 6);
			this.GB_ChangeLog.Size = new System.Drawing.Size(442, 252);
			this.GB_ChangeLog.TabIndex = 12;
			this.GB_ChangeLog.TabStop = false;
			this.GB_ChangeLog.Text = "Lista zmian w poszczególnych wersjach";
			// 
			// RTB_Changes
			// 
			this.RTB_Changes.BackColor = System.Drawing.SystemColors.Window;
			this.RTB_Changes.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.RTB_Changes.Dock = System.Windows.Forms.DockStyle.Fill;
			this.RTB_Changes.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.RTB_Changes.Location = new System.Drawing.Point(6, 17);
			this.RTB_Changes.Name = "RTB_Changes";
			this.RTB_Changes.ReadOnly = true;
			this.RTB_Changes.Size = new System.Drawing.Size(430, 229);
			this.RTB_Changes.TabIndex = 2;
			this.RTB_Changes.Text = "";
			// 
			// BW_Update
			// 
			this.BW_Update.WorkerReportsProgress = true;
			this.BW_Update.WorkerSupportsCancellation = true;
			// 
			// UpdateForm
			// 
			this.AcceptButton = this.B_Update;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.B_Close;
			this.ClientSize = new System.Drawing.Size(454, 372);
			this.Controls.Add(this.TLP_Main);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "UpdateForm";
			this.Text = "Aktualizacja programu";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Update_FormClosing);
			this.TLP_Main.ResumeLayout(false);
			this.TLP_Main.PerformLayout();
			this.TLP_StatusBar.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.PB_AppLogo)).EndInit();
			this.GB_ChangeLog.ResumeLayout(false);
			this.ResumeLayout(false);
		}

#endregion

		private System.Windows.Forms.TableLayoutPanel TLP_Main;
		private System.Windows.Forms.RichTextBox RTB_Changes;
		private System.Windows.Forms.TableLayoutPanel TLP_StatusBar;
		private System.Windows.Forms.ProgressBar PRB_Update;
		private System.ComponentModel.BackgroundWorker BW_Update;
		private System.Windows.Forms.Button B_Update;
		private System.Windows.Forms.Label L_CurVersion;
		private System.Windows.Forms.Label L_UpdateAvailable;
		private System.Windows.Forms.Label L_NewVersion;
		private System.Windows.Forms.PictureBox PB_AppLogo;
		private System.Windows.Forms.Button B_Close;
		private System.Windows.Forms.GroupBox GB_ChangeLog;

	/// @endcond
	}
}