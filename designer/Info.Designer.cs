namespace CDesigner
{
	partial class Info
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
			this.tbTable = new System.Windows.Forms.TableLayoutPanel();
			this.pPanel = new System.Windows.Forms.Panel();
			this.pbImage = new System.Windows.Forms.PictureBox();
			this.lIT = new System.Windows.Forms.Label();
			this.lUniv = new System.Windows.Forms.Label();
			this.lProgram = new System.Windows.Forms.Label();
			this.lCopyright = new System.Windows.Forms.Label();
			this.lAuthor = new System.Windows.Forms.Label();
			this.lVersion = new System.Windows.Forms.Label();
			this.lName = new System.Windows.Forms.Label();
			this.bClose = new System.Windows.Forms.Button();
			this.tbTable.SuspendLayout();
			this.pPanel.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pbImage)).BeginInit();
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
			this.tbTable.Size = new System.Drawing.Size(327, 300);
			this.tbTable.TabIndex = 0;
			// 
			// pPanel
			// 
			this.pPanel.BackColor = System.Drawing.SystemColors.Window;
			this.pPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.pPanel.Controls.Add(this.pbImage);
			this.pPanel.Controls.Add(this.lIT);
			this.pPanel.Controls.Add(this.lUniv);
			this.pPanel.Controls.Add(this.lProgram);
			this.pPanel.Controls.Add(this.lCopyright);
			this.pPanel.Controls.Add(this.lAuthor);
			this.pPanel.Controls.Add(this.lVersion);
			this.pPanel.Controls.Add(this.lName);
			this.pPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pPanel.Location = new System.Drawing.Point(5, 5);
			this.pPanel.Margin = new System.Windows.Forms.Padding(5, 5, 5, 0);
			this.pPanel.Name = "pPanel";
			this.pPanel.Size = new System.Drawing.Size(317, 261);
			this.pPanel.TabIndex = 0;
			// 
			// pbImage
			// 
			this.pbImage.Image = global::CDesigner.Properties.Resources.logoInfo;
			this.pbImage.Location = new System.Drawing.Point(9, 178);
			this.pbImage.Name = "pbImage";
			this.pbImage.Size = new System.Drawing.Size(296, 74);
			this.pbImage.TabIndex = 20;
			this.pbImage.TabStop = false;
			// 
			// lIT
			// 
			this.lIT.AutoSize = true;
			this.lIT.Font = new System.Drawing.Font("Trebuchet MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.lIT.Location = new System.Drawing.Point(9, 78);
			this.lIT.Name = "lIT";
			this.lIT.Size = new System.Drawing.Size(223, 18);
			this.lIT.TabIndex = 19;
			this.lIT.Text = "Instytut Politechniczny - Informatyka";
			// 
			// lUniv
			// 
			this.lUniv.AutoSize = true;
			this.lUniv.Font = new System.Drawing.Font("Trebuchet MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.lUniv.Location = new System.Drawing.Point(9, 157);
			this.lUniv.Name = "lUniv";
			this.lUniv.Size = new System.Drawing.Size(299, 18);
			this.lUniv.TabIndex = 18;
			this.lUniv.Text = "Edukacyjne i obrony pracy inżynierskiej";
			// 
			// lProgram
			// 
			this.lProgram.AutoSize = true;
			this.lProgram.Font = new System.Drawing.Font("Trebuchet MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.lProgram.Location = new System.Drawing.Point(9, 139);
			this.lProgram.Name = "lProgram";
			this.lProgram.Size = new System.Drawing.Size(145, 18);
			this.lProgram.TabIndex = 17;
			this.lProgram.Text = "Program stworzony na potrzeby:";
			// 
			// lCopyright
			// 
			this.lCopyright.AutoSize = true;
			this.lCopyright.Font = new System.Drawing.Font("Trebuchet MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.lCopyright.Location = new System.Drawing.Point(9, 96);
			this.lCopyright.Name = "lCopyright";
			this.lCopyright.Size = new System.Drawing.Size(281, 18);
			this.lCopyright.TabIndex = 16;
			this.lCopyright.Text = "Państwowa Wyższa Szkoła Zawodowa w Krośnie";
			// 
			// lAuthor
			// 
			this.lAuthor.AutoSize = true;
			this.lAuthor.Font = new System.Drawing.Font("Trebuchet MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.lAuthor.Location = new System.Drawing.Point(9, 60);
			this.lAuthor.Name = "lAuthor";
			this.lAuthor.Size = new System.Drawing.Size(113, 18);
			this.lAuthor.TabIndex = 15;
			this.lAuthor.Text = "Autor: Kamil Biały";
			// 
			// lVersion
			// 
			this.lVersion.AutoSize = true;
			this.lVersion.Font = new System.Drawing.Font("Trebuchet MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.lVersion.Location = new System.Drawing.Point(9, 24);
			this.lVersion.Name = "lVersion";
			this.lVersion.Size = new System.Drawing.Size(34, 18);
			this.lVersion.TabIndex = 14;
			this.lVersion.Text = "v0.4.0";
			// 
			// lName
			// 
			this.lName.AutoSize = true;
			this.lName.Font = new System.Drawing.Font("Trebuchet MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.lName.Location = new System.Drawing.Point(9, 6);
			this.lName.Name = "lName";
			this.lName.Size = new System.Drawing.Size(184, 18);
			this.lName.TabIndex = 13;
			this.lName.Text = "CDesigner - Kreator dyplomów";
			// 
			// bClose
			// 
			this.bClose.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.bClose.Location = new System.Drawing.Point(113, 271);
			this.bClose.Name = "bClose";
			this.bClose.Size = new System.Drawing.Size(100, 23);
			this.bClose.TabIndex = 2;
			this.bClose.Text = "Zamknij";
			this.bClose.UseVisualStyleBackColor = true;
			this.bClose.Click += new System.EventHandler(this.bClose_Click);
			// 
			// Info
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.Control;
			this.ClientSize = new System.Drawing.Size(327, 300);
			this.Controls.Add(this.tbTable);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "Info";
			this.Text = "Informacje";
			this.tbTable.ResumeLayout(false);
			this.pPanel.ResumeLayout(false);
			this.pPanel.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.pbImage)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TableLayoutPanel tbTable;
		private System.Windows.Forms.Panel pPanel;
		private System.Windows.Forms.PictureBox pbImage;
		private System.Windows.Forms.Label lIT;
		private System.Windows.Forms.Label lUniv;
		private System.Windows.Forms.Label lProgram;
		private System.Windows.Forms.Label lCopyright;
		private System.Windows.Forms.Label lAuthor;
		private System.Windows.Forms.Label lVersion;
		private System.Windows.Forms.Label lName;
		private System.Windows.Forms.Button bClose;

	}
}