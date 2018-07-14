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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Info));
			this.tbTable = new System.Windows.Forms.TableLayoutPanel();
			this.pPanel = new System.Windows.Forms.Panel();
			this.pbLogo = new System.Windows.Forms.PictureBox();
			this.pbWho = new System.Windows.Forms.PictureBox();
			this.lIT = new System.Windows.Forms.Label();
			this.lProgram = new System.Windows.Forms.Label();
			this.lCopyright = new System.Windows.Forms.Label();
			this.lAuthor = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.bClose = new System.Windows.Forms.Button();
			this.tbTable.SuspendLayout();
			this.pPanel.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pbLogo)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pbWho)).BeginInit();
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
			this.tbTable.Size = new System.Drawing.Size(398, 292);
			this.tbTable.TabIndex = 0;
			// 
			// pPanel
			// 
			this.pPanel.BackColor = System.Drawing.SystemColors.Window;
			this.pPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.pPanel.Controls.Add(this.pbLogo);
			this.pPanel.Controls.Add(this.pbWho);
			this.pPanel.Controls.Add(this.lIT);
			this.pPanel.Controls.Add(this.lProgram);
			this.pPanel.Controls.Add(this.lCopyright);
			this.pPanel.Controls.Add(this.lAuthor);
			this.pPanel.Controls.Add(this.label2);
			this.pPanel.Controls.Add(this.label1);
			this.pPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pPanel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.pPanel.Location = new System.Drawing.Point(5, 5);
			this.pPanel.Margin = new System.Windows.Forms.Padding(5, 5, 5, 0);
			this.pPanel.Name = "pPanel";
			this.pPanel.Size = new System.Drawing.Size(388, 253);
			this.pPanel.TabIndex = 0;
			// 
			// pbLogo
			// 
			this.pbLogo.Image = ((System.Drawing.Image)(resources.GetObject("pbLogo.Image")));
			this.pbLogo.Location = new System.Drawing.Point(252, 6);
			this.pbLogo.Name = "pbLogo";
			this.pbLogo.Size = new System.Drawing.Size(128, 128);
			this.pbLogo.TabIndex = 21;
			this.pbLogo.TabStop = false;
			// 
			// pbWho
			// 
			this.pbWho.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pbWho.BackgroundImage")));
			this.pbWho.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
			this.pbWho.Location = new System.Drawing.Point(6, 140);
			this.pbWho.Name = "pbWho";
			this.pbWho.Size = new System.Drawing.Size(374, 105);
			this.pbWho.TabIndex = 20;
			this.pbWho.TabStop = false;
			// 
			// lIT
			// 
			this.lIT.AutoSize = true;
			this.lIT.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.lIT.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
			this.lIT.Location = new System.Drawing.Point(6, 73);
			this.lIT.Name = "lIT";
			this.lIT.Size = new System.Drawing.Size(185, 13);
			this.lIT.TabIndex = 19;
			this.lIT.Text = "Instytut Politechniczny - Informatyka";
			// 
			// lProgram
			// 
			this.lProgram.AutoSize = true;
			this.lProgram.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.lProgram.Location = new System.Drawing.Point(6, 119);
			this.lProgram.Name = "lProgram";
			this.lProgram.Size = new System.Drawing.Size(207, 16);
			this.lProgram.TabIndex = 17;
			this.lProgram.Text = "Kopia programu przeznaczona dla:";
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
			// lAuthor
			// 
			this.lAuthor.AutoSize = true;
			this.lAuthor.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.lAuthor.Location = new System.Drawing.Point(6, 55);
			this.lAuthor.Name = "lAuthor";
			this.lAuthor.Size = new System.Drawing.Size(110, 16);
			this.lAuthor.TabIndex = 15;
			this.lAuthor.Text = "Autor: Kamil Biały";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.label2.ForeColor = System.Drawing.Color.Gray;
			this.label2.Location = new System.Drawing.Point(7, 25);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(121, 13);
			this.label2.TabIndex = 14;
			this.label2.Text = "v0.7.0261-1433790383";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.label1.ForeColor = System.Drawing.Color.Black;
			this.label1.Location = new System.Drawing.Point(6, 6);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(227, 19);
			this.label1.TabIndex = 13;
			this.label1.Text = "CDesigner - Kreator dyplomów";
			// 
			// bClose
			// 
			this.bClose.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.bClose.Location = new System.Drawing.Point(149, 263);
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
			this.ClientSize = new System.Drawing.Size(398, 292);
			this.Controls.Add(this.tbTable);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "Info";
			this.Text = "Informacje";
			this.tbTable.ResumeLayout(false);
			this.pPanel.ResumeLayout(false);
			this.pPanel.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.pbLogo)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pbWho)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TableLayoutPanel tbTable;
		private System.Windows.Forms.Panel pPanel;
		private System.Windows.Forms.PictureBox pbWho;
		private System.Windows.Forms.Label lIT;
		private System.Windows.Forms.Label lCopyright;
		private System.Windows.Forms.Label lAuthor;
		private System.Windows.Forms.Button bClose;
		private System.Windows.Forms.PictureBox pbLogo;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label lProgram;

	}
}