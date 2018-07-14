namespace CDesigner
{
    partial class NewPattern
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NewPattern));
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.bCreate = new System.Windows.Forms.Button();
			this.tbPatternName = new System.Windows.Forms.TextBox();
			this.cbCopyFrom = new System.Windows.Forms.ComboBox();
			this.cbPaperFormat = new System.Windows.Forms.ComboBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.nWidth = new System.Windows.Forms.NumericUpDown();
			this.nHeight = new System.Windows.Forms.NumericUpDown();
			this.tableLayoutPanel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.nWidth)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.nHeight)).BeginInit();
			this.SuspendLayout();
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.ColumnCount = 3;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33334F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33334F));
			this.tableLayoutPanel1.Controls.Add(this.bCreate, 2, 4);
			this.tableLayoutPanel1.Controls.Add(this.tbPatternName, 1, 0);
			this.tableLayoutPanel1.Controls.Add(this.cbCopyFrom, 1, 1);
			this.tableLayoutPanel1.Controls.Add(this.cbPaperFormat, 1, 2);
			this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.label2, 0, 1);
			this.tableLayoutPanel1.Controls.Add(this.label3, 0, 2);
			this.tableLayoutPanel1.Controls.Add(this.label4, 0, 3);
			this.tableLayoutPanel1.Controls.Add(this.nWidth, 1, 3);
			this.tableLayoutPanel1.Controls.Add(this.nHeight, 2, 3);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(7);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.Padding = new System.Windows.Forms.Padding(3);
			this.tableLayoutPanel1.RowCount = 5;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(296, 139);
			this.tableLayoutPanel1.TabIndex = 0;
			// 
			// bCreate
			// 
			this.bCreate.Dock = System.Windows.Forms.DockStyle.Fill;
			this.bCreate.Location = new System.Drawing.Point(198, 110);
			this.bCreate.Name = "bCreate";
			this.bCreate.Size = new System.Drawing.Size(92, 23);
			this.bCreate.TabIndex = 0;
			this.bCreate.Text = "Utwórz";
			this.bCreate.UseVisualStyleBackColor = true;
			this.bCreate.Click += new System.EventHandler(this.bCreate_Click);
			// 
			// tbPatternName
			// 
			this.tableLayoutPanel1.SetColumnSpan(this.tbPatternName, 2);
			this.tbPatternName.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tbPatternName.Location = new System.Drawing.Point(102, 6);
			this.tbPatternName.Name = "tbPatternName";
			this.tbPatternName.Size = new System.Drawing.Size(188, 20);
			this.tbPatternName.TabIndex = 3;
			this.tbPatternName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbPatternName_KeyPress);
			this.tbPatternName.Leave += new System.EventHandler(this.tbPatternName_Leave);
			// 
			// cbCopyFrom
			// 
			this.tableLayoutPanel1.SetColumnSpan(this.cbCopyFrom, 2);
			this.cbCopyFrom.Dock = System.Windows.Forms.DockStyle.Fill;
			this.cbCopyFrom.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbCopyFrom.Enabled = false;
			this.cbCopyFrom.FormattingEnabled = true;
			this.cbCopyFrom.Location = new System.Drawing.Point(102, 32);
			this.cbCopyFrom.Name = "cbCopyFrom";
			this.cbCopyFrom.Size = new System.Drawing.Size(188, 21);
			this.cbCopyFrom.TabIndex = 4;
			// 
			// cbPaperFormat
			// 
			this.tableLayoutPanel1.SetColumnSpan(this.cbPaperFormat, 2);
			this.cbPaperFormat.Dock = System.Windows.Forms.DockStyle.Fill;
			this.cbPaperFormat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbPaperFormat.FormattingEnabled = true;
			this.cbPaperFormat.Items.AddRange(new object[] {
            "Własny"});
			this.cbPaperFormat.Location = new System.Drawing.Point(102, 58);
			this.cbPaperFormat.Name = "cbPaperFormat";
			this.cbPaperFormat.Size = new System.Drawing.Size(188, 21);
			this.cbPaperFormat.TabIndex = 5;
			this.cbPaperFormat.SelectedIndexChanged += new System.EventHandler(this.cbPaperFormat_SelectedIndexChanged);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.label1.Location = new System.Drawing.Point(6, 3);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(90, 26);
			this.label1.TabIndex = 6;
			this.label1.Text = "Nazwa wzoru:";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.label2.Location = new System.Drawing.Point(6, 29);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(90, 26);
			this.label2.TabIndex = 7;
			this.label2.Text = "Kopiuj z wzoru:";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
			this.label3.Location = new System.Drawing.Point(6, 55);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(90, 26);
			this.label3.TabIndex = 8;
			this.label3.Text = "Format papieru:";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
			this.label4.Location = new System.Drawing.Point(6, 81);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(90, 26);
			this.label4.TabIndex = 9;
			this.label4.Text = "Rozmiar (mm):";
			this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// nWidth
			// 
			this.nWidth.Dock = System.Windows.Forms.DockStyle.Fill;
			this.nWidth.Location = new System.Drawing.Point(102, 84);
			this.nWidth.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
			this.nWidth.Name = "nWidth";
			this.nWidth.Size = new System.Drawing.Size(90, 20);
			this.nWidth.TabIndex = 10;
			// 
			// nHeight
			// 
			this.nHeight.Dock = System.Windows.Forms.DockStyle.Fill;
			this.nHeight.Location = new System.Drawing.Point(198, 84);
			this.nHeight.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
			this.nHeight.Name = "nHeight";
			this.nHeight.Size = new System.Drawing.Size(92, 20);
			this.nHeight.TabIndex = 11;
			// 
			// NewPattern
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(296, 139);
			this.Controls.Add(this.tableLayoutPanel1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "NewPattern";
			this.Text = "Nowy Wzór";
			this.Move += new System.EventHandler(this.NewPattern_Move);
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.nWidth)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.nHeight)).EndInit();
			this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button bCreate;
        private System.Windows.Forms.TextBox tbPatternName;
        private System.Windows.Forms.ComboBox cbCopyFrom;
        private System.Windows.Forms.ComboBox cbPaperFormat;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown nWidth;
        private System.Windows.Forms.NumericUpDown nHeight;

    }
}