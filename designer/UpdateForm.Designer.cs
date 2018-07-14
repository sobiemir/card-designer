namespace CDesigner
{
	partial class UpdateForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UpdateForm));
			this.tlUpdate = new System.Windows.Forms.TableLayoutPanel();
			this.pChanges = new System.Windows.Forms.Panel();
			this.rtbChanges = new System.Windows.Forms.RichTextBox();
			this.lUpdateChanges = new System.Windows.Forms.Label();
			this.tlBottomBar = new System.Windows.Forms.TableLayoutPanel();
			this.bUpdate = new System.Windows.Forms.Button();
			this.pbUpdate = new System.Windows.Forms.ProgressBar();
			this.pbAppLogo = new System.Windows.Forms.PictureBox();
			this.pVersion = new System.Windows.Forms.Panel();
			this.lNewVersion = new System.Windows.Forms.Label();
			this.lUpdateAvaliable = new System.Windows.Forms.Label();
			this.lCurrentVersion = new System.Windows.Forms.Label();
			this.bwTask = new System.ComponentModel.BackgroundWorker();
			this.tlUpdate.SuspendLayout();
			this.pChanges.SuspendLayout();
			this.tlBottomBar.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pbAppLogo)).BeginInit();
			this.pVersion.SuspendLayout();
			this.SuspendLayout();
			// 
			// tlUpdate
			// 
			this.tlUpdate.ColumnCount = 2;
			this.tlUpdate.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tlUpdate.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 70F));
			this.tlUpdate.Controls.Add(this.pChanges, 0, 2);
			this.tlUpdate.Controls.Add(this.lUpdateChanges, 0, 1);
			this.tlUpdate.Controls.Add(this.tlBottomBar, 0, 3);
			this.tlUpdate.Controls.Add(this.pbAppLogo, 1, 0);
			this.tlUpdate.Controls.Add(this.pVersion, 0, 0);
			this.tlUpdate.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tlUpdate.Location = new System.Drawing.Point(0, 0);
			this.tlUpdate.Name = "tlUpdate";
			this.tlUpdate.RowCount = 4;
			this.tlUpdate.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 56F));
			this.tlUpdate.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tlUpdate.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tlUpdate.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
			this.tlUpdate.Size = new System.Drawing.Size(445, 311);
			this.tlUpdate.TabIndex = 0;
			// 
			// pChanges
			// 
			this.pChanges.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.tlUpdate.SetColumnSpan(this.pChanges, 2);
			this.pChanges.Controls.Add(this.rtbChanges);
			this.pChanges.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pChanges.Location = new System.Drawing.Point(6, 76);
			this.pChanges.Margin = new System.Windows.Forms.Padding(6, 0, 6, 3);
			this.pChanges.Name = "pChanges";
			this.pChanges.Size = new System.Drawing.Size(433, 204);
			this.pChanges.TabIndex = 5;
			// 
			// rtbChanges
			// 
			this.rtbChanges.BackColor = System.Drawing.SystemColors.Window;
			this.rtbChanges.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.rtbChanges.Dock = System.Windows.Forms.DockStyle.Fill;
			this.rtbChanges.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.rtbChanges.Location = new System.Drawing.Point(0, 0);
			this.rtbChanges.Margin = new System.Windows.Forms.Padding(0);
			this.rtbChanges.Name = "rtbChanges";
			this.rtbChanges.ReadOnly = true;
			this.rtbChanges.Size = new System.Drawing.Size(431, 202);
			this.rtbChanges.TabIndex = 2;
			this.rtbChanges.Text = "";
			// 
			// lUpdateChanges
			// 
			this.lUpdateChanges.AutoSize = true;
			this.lUpdateChanges.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lUpdateChanges.ForeColor = System.Drawing.SystemColors.ControlText;
			this.lUpdateChanges.Location = new System.Drawing.Point(6, 56);
			this.lUpdateChanges.Margin = new System.Windows.Forms.Padding(6, 0, 6, 3);
			this.lUpdateChanges.Name = "lUpdateChanges";
			this.lUpdateChanges.Size = new System.Drawing.Size(363, 17);
			this.lUpdateChanges.TabIndex = 3;
			this.lUpdateChanges.Text = "Lista zmian od wersji początkowej:";
			this.lUpdateChanges.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// tlBottomBar
			// 
			this.tlBottomBar.ColumnCount = 2;
			this.tlUpdate.SetColumnSpan(this.tlBottomBar, 2);
			this.tlBottomBar.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 136F));
			this.tlBottomBar.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 60F));
			this.tlBottomBar.Controls.Add(this.bUpdate, 0, 0);
			this.tlBottomBar.Controls.Add(this.pbUpdate, 1, 0);
			this.tlBottomBar.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tlBottomBar.Location = new System.Drawing.Point(0, 283);
			this.tlBottomBar.Margin = new System.Windows.Forms.Padding(0);
			this.tlBottomBar.Name = "tlBottomBar";
			this.tlBottomBar.RowCount = 1;
			this.tlBottomBar.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tlBottomBar.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
			this.tlBottomBar.Size = new System.Drawing.Size(445, 28);
			this.tlBottomBar.TabIndex = 7;
			// 
			// bUpdate
			// 
			this.bUpdate.Dock = System.Windows.Forms.DockStyle.Fill;
			this.bUpdate.Location = new System.Drawing.Point(6, 3);
			this.bUpdate.Margin = new System.Windows.Forms.Padding(6, 3, 3, 3);
			this.bUpdate.Name = "bUpdate";
			this.bUpdate.Size = new System.Drawing.Size(127, 22);
			this.bUpdate.TabIndex = 4;
			this.bUpdate.Text = "Aktualizuj program";
			this.bUpdate.UseVisualStyleBackColor = true;
			this.bUpdate.Click += new System.EventHandler(this.bUpdate_Click);
			// 
			// pbUpdate
			// 
			this.pbUpdate.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pbUpdate.Location = new System.Drawing.Point(139, 5);
			this.pbUpdate.Margin = new System.Windows.Forms.Padding(3, 5, 6, 5);
			this.pbUpdate.Name = "pbUpdate";
			this.pbUpdate.Size = new System.Drawing.Size(300, 18);
			this.pbUpdate.TabIndex = 3;
			// 
			// pbAppLogo
			// 
			this.pbAppLogo.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pbAppLogo.Image = ((System.Drawing.Image)(resources.GetObject("pbAppLogo.Image")));
			this.pbAppLogo.Location = new System.Drawing.Point(375, 6);
			this.pbAppLogo.Margin = new System.Windows.Forms.Padding(0, 6, 6, 6);
			this.pbAppLogo.Name = "pbAppLogo";
			this.tlUpdate.SetRowSpan(this.pbAppLogo, 2);
			this.pbAppLogo.Size = new System.Drawing.Size(64, 64);
			this.pbAppLogo.TabIndex = 8;
			this.pbAppLogo.TabStop = false;
			// 
			// pVersion
			// 
			this.pVersion.Controls.Add(this.lNewVersion);
			this.pVersion.Controls.Add(this.lUpdateAvaliable);
			this.pVersion.Controls.Add(this.lCurrentVersion);
			this.pVersion.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pVersion.Location = new System.Drawing.Point(0, 0);
			this.pVersion.Margin = new System.Windows.Forms.Padding(0);
			this.pVersion.Name = "pVersion";
			this.pVersion.Size = new System.Drawing.Size(375, 56);
			this.pVersion.TabIndex = 9;
			// 
			// lNewVersion
			// 
			this.lNewVersion.AutoSize = true;
			this.lNewVersion.ForeColor = System.Drawing.Color.Gray;
			this.lNewVersion.Location = new System.Drawing.Point(6, 40);
			this.lNewVersion.Margin = new System.Windows.Forms.Padding(6, 6, 0, 0);
			this.lNewVersion.Name = "lNewVersion";
			this.lNewVersion.Size = new System.Drawing.Size(113, 13);
			this.lNewVersion.TabIndex = 11;
			this.lNewVersion.Text = "Wersja po aktualizacji:";
			// 
			// lUpdateAvaliable
			// 
			this.lUpdateAvaliable.AutoSize = true;
			this.lUpdateAvaliable.Location = new System.Drawing.Point(6, 6);
			this.lUpdateAvaliable.Margin = new System.Windows.Forms.Padding(6, 6, 0, 0);
			this.lUpdateAvaliable.Name = "lUpdateAvaliable";
			this.lUpdateAvaliable.Size = new System.Drawing.Size(184, 13);
			this.lUpdateAvaliable.TabIndex = 10;
			this.lUpdateAvaliable.Text = "Dostępna jest nowa wersja programu!";
			// 
			// lCurrentVersion
			// 
			this.lCurrentVersion.AutoSize = true;
			this.lCurrentVersion.ForeColor = System.Drawing.Color.Gray;
			this.lCurrentVersion.Location = new System.Drawing.Point(6, 23);
			this.lCurrentVersion.Margin = new System.Windows.Forms.Padding(6, 6, 0, 0);
			this.lCurrentVersion.Name = "lCurrentVersion";
			this.lCurrentVersion.Size = new System.Drawing.Size(81, 13);
			this.lCurrentVersion.TabIndex = 10;
			this.lCurrentVersion.Text = "Obecna wersja:";
			// 
			// bwTask
			// 
			this.bwTask.WorkerReportsProgress = true;
			this.bwTask.WorkerSupportsCancellation = true;
			// 
			// UpdateForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(445, 311);
			this.Controls.Add(this.tlUpdate);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "UpdateForm";
			this.Text = "Aktualizacja programu";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Update_FormClosing);
			this.tlUpdate.ResumeLayout(false);
			this.tlUpdate.PerformLayout();
			this.pChanges.ResumeLayout(false);
			this.tlBottomBar.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.pbAppLogo)).EndInit();
			this.pVersion.ResumeLayout(false);
			this.pVersion.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TableLayoutPanel tlUpdate;
		private System.Windows.Forms.Panel pChanges;
		private System.Windows.Forms.RichTextBox rtbChanges;
		private System.Windows.Forms.Label lUpdateChanges;
		private System.Windows.Forms.TableLayoutPanel tlBottomBar;
		private System.Windows.Forms.PictureBox pbAppLogo;
		private System.Windows.Forms.Panel pVersion;
		private System.Windows.Forms.Label lUpdateAvaliable;
		private System.Windows.Forms.Label lCurrentVersion;
		private System.Windows.Forms.Label lNewVersion;
		private System.Windows.Forms.ProgressBar pbUpdate;
		private System.ComponentModel.BackgroundWorker bwTask;
		private System.Windows.Forms.Button bUpdate;
	}
}