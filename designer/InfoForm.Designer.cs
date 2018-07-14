namespace CDesigner
{
	partial class InfoForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InfoForm));
			this.tbTable = new System.Windows.Forms.TableLayoutPanel();
			this.pPanel = new System.Windows.Forms.Panel();
			this.pbAppLogo = new System.Windows.Forms.PictureBox();
			this.pbAppDest = new System.Windows.Forms.PictureBox();
			this.lAuthorIT = new System.Windows.Forms.Label();
			this.lAppDest = new System.Windows.Forms.Label();
			this.lCopyright = new System.Windows.Forms.Label();
			this.lAppAuthor = new System.Windows.Forms.Label();
			this.lAppVersion = new System.Windows.Forms.Label();
			this.lAppName = new System.Windows.Forms.Label();
			this.bClose = new System.Windows.Forms.Button();
			this.tbTable.SuspendLayout();
			this.pPanel.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pbAppLogo)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pbAppDest)).BeginInit();
			this.SuspendLayout();
			// 
			// tbTable
			// 
			this.tbTable.ColumnCount = 1;
			this.tbTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tbTable.Controls.Add(this.pPanel, 0, 0);
			this.tbTable.Controls.Add(this.bClose, 0, 1);
			this.tbTable.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tbTable.Location = new System.Drawing.Point(0, 0);
			this.tbTable.Name = "tbTable";
			this.tbTable.RowCount = 2;
			this.tbTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tbTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34F));
			this.tbTable.Size = new System.Drawing.Size(559, 292);
			this.tbTable.TabIndex = 0;
			// 
			// pPanel
			// 
			this.pPanel.BackColor = System.Drawing.SystemColors.Window;
			this.pPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.pPanel.Controls.Add(this.pbAppLogo);
			this.pPanel.Controls.Add(this.pbAppDest);
			this.pPanel.Controls.Add(this.lAuthorIT);
			this.pPanel.Controls.Add(this.lAppDest);
			this.pPanel.Controls.Add(this.lCopyright);
			this.pPanel.Controls.Add(this.lAppAuthor);
			this.pPanel.Controls.Add(this.lAppVersion);
			this.pPanel.Controls.Add(this.lAppName);
			this.pPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pPanel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.pPanel.Location = new System.Drawing.Point(5, 5);
			this.pPanel.Margin = new System.Windows.Forms.Padding(5, 5, 5, 0);
			this.pPanel.Name = "pPanel";
			this.pPanel.Size = new System.Drawing.Size(549, 253);
			this.pPanel.TabIndex = 0;
			// 
			// pbAppLogo
			// 
			this.pbAppLogo.Image = ((System.Drawing.Image)(resources.GetObject("pbAppLogo.Image")));
			this.pbAppLogo.Location = new System.Drawing.Point(252, 6);
			this.pbAppLogo.Name = "pbAppLogo";
			this.pbAppLogo.Size = new System.Drawing.Size(128, 128);
			this.pbAppLogo.TabIndex = 21;
			this.pbAppLogo.TabStop = false;
			// 
			// pbAppDest
			// 
			this.pbAppDest.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pbAppDest.BackgroundImage")));
			this.pbAppDest.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
			this.pbAppDest.Location = new System.Drawing.Point(6, 140);
			this.pbAppDest.Name = "pbAppDest";
			this.pbAppDest.Size = new System.Drawing.Size(374, 105);
			this.pbAppDest.TabIndex = 20;
			this.pbAppDest.TabStop = false;
			// 
			// lAuthorIT
			// 
			this.lAuthorIT.AutoSize = true;
			this.lAuthorIT.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.lAuthorIT.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
			this.lAuthorIT.Location = new System.Drawing.Point(6, 73);
			this.lAuthorIT.Name = "lAuthorIT";
			this.lAuthorIT.Size = new System.Drawing.Size(185, 13);
			this.lAuthorIT.TabIndex = 19;
			this.lAuthorIT.Text = "Instytut Politechniczny - Informatyka";
			// 
			// lAppDest
			// 
			this.lAppDest.AutoSize = true;
			this.lAppDest.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.lAppDest.Location = new System.Drawing.Point(6, 119);
			this.lAppDest.Name = "lAppDest";
			this.lAppDest.Size = new System.Drawing.Size(207, 16);
			this.lAppDest.TabIndex = 17;
			this.lAppDest.Text = "Kopia programu przeznaczona dla:";
			// 
			// lCopyright
			// 
			this.lCopyright.AutoSize = true;
			this.lCopyright.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.lCopyright.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
			this.lCopyright.Location = new System.Drawing.Point(6, 89);
			this.lCopyright.Name = "lCopyright";
			this.lCopyright.Size = new System.Drawing.Size(247, 13);
			this.lCopyright.TabIndex = 16;
			this.lCopyright.Text = "Copyright © 2015. Wszystkie prawa zastrzeżone.";
			// 
			// lAppAuthor
			// 
			this.lAppAuthor.AutoSize = true;
			this.lAppAuthor.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.lAppAuthor.Location = new System.Drawing.Point(6, 55);
			this.lAppAuthor.Name = "lAppAuthor";
			this.lAppAuthor.Size = new System.Drawing.Size(110, 16);
			this.lAppAuthor.TabIndex = 15;
			this.lAppAuthor.Text = "Autor: Kamil Biały";
			// 
			// lAppVersion
			// 
			this.lAppVersion.AutoSize = true;
			this.lAppVersion.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.lAppVersion.ForeColor = System.Drawing.Color.Gray;
			this.lAppVersion.Location = new System.Drawing.Point(7, 25);
			this.lAppVersion.Name = "lAppVersion";
			this.lAppVersion.Size = new System.Drawing.Size(121, 13);
			this.lAppVersion.TabIndex = 14;
			this.lAppVersion.Text = "v0.7.0107-1433790383";
			// 
			// lAppName
			// 
			this.lAppName.AutoSize = true;
			this.lAppName.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.lAppName.ForeColor = System.Drawing.Color.Black;
			this.lAppName.Location = new System.Drawing.Point(6, 6);
			this.lAppName.Name = "lAppName";
			this.lAppName.Size = new System.Drawing.Size(227, 18);
			this.lAppName.TabIndex = 13;
			this.lAppName.Text = "CardDesigner - Kreator dyplomów";
			// 
			// bClose
			// 
			this.bClose.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.bClose.Location = new System.Drawing.Point(229, 263);
			this.bClose.Name = "bClose";
			this.bClose.Size = new System.Drawing.Size(100, 23);
			this.bClose.TabIndex = 2;
			this.bClose.Text = "Zamknij";
			this.bClose.UseVisualStyleBackColor = true;
			this.bClose.Click += new System.EventHandler(this.bClose_Click);
			// 
			// InfoForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.Control;
			this.ClientSize = new System.Drawing.Size(398, 292);
			this.Controls.Add(this.tbTable);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "InfoForm";
			this.Text = "Informacje";
			this.tbTable.ResumeLayout(false);
			this.pPanel.ResumeLayout(false);
			this.pPanel.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.pbAppLogo)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pbAppDest)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TableLayoutPanel tbTable;
		private System.Windows.Forms.Panel pPanel;
		private System.Windows.Forms.PictureBox pbAppDest;
		private System.Windows.Forms.Label lAuthorIT;
		private System.Windows.Forms.Label lCopyright;
		private System.Windows.Forms.Label lAppAuthor;
		private System.Windows.Forms.Button bClose;
		private System.Windows.Forms.PictureBox pbAppLogo;
		private System.Windows.Forms.Label lAppVersion;
		private System.Windows.Forms.Label lAppName;
		private System.Windows.Forms.Label lAppDest;

	}
}