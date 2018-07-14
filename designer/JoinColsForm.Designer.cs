namespace CDesigner
{
	partial class JoinColsForm
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
			this.lvDBCols = new System.Windows.Forms.ListView();
			this.lvhDBCols = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.cbEncoding = new System.Windows.Forms.ComboBox();
			this.bSave = new System.Windows.Forms.Button();
			this.lvhPageFields = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.tMainPanel = new System.Windows.Forms.TableLayoutPanel();
			this.lvPageFields = new System.Windows.Forms.ListView();
			this.listBox1 = new System.Windows.Forms.ListBox();
			this.tableLayoutPanel1.SuspendLayout();
			this.tMainPanel.SuspendLayout();
			this.SuspendLayout();
			// 
			// lvDBCols
			// 
			this.lvDBCols.CheckBoxes = true;
			this.lvDBCols.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.lvhDBCols});
			this.lvDBCols.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lvDBCols.FullRowSelect = true;
			this.lvDBCols.GridLines = true;
			this.lvDBCols.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
			this.lvDBCols.HideSelection = false;
			this.lvDBCols.Location = new System.Drawing.Point(366, 6);
			this.lvDBCols.Margin = new System.Windows.Forms.Padding(3, 6, 6, 3);
			this.lvDBCols.MultiSelect = false;
			this.lvDBCols.Name = "lvDBCols";
			this.tMainPanel.SetRowSpan(this.lvDBCols, 2);
			this.lvDBCols.ShowGroups = false;
			this.lvDBCols.Size = new System.Drawing.Size(205, 316);
			this.lvDBCols.TabIndex = 9;
			this.lvDBCols.UseCompatibleStateImageBehavior = false;
			this.lvDBCols.View = System.Windows.Forms.View.Details;
			// 
			// lvhDBCols
			// 
			this.lvhDBCols.Text = "Dostępne kolumny";
			this.lvhDBCols.Width = 201;
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.ColumnCount = 2;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel1.Controls.Add(this.cbEncoding, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.bSave, 1, 0);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(363, 325);
			this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 1;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(214, 28);
			this.tableLayoutPanel1.TabIndex = 11;
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
			this.cbEncoding.Location = new System.Drawing.Point(3, 3);
			this.cbEncoding.Name = "cbEncoding";
			this.cbEncoding.Size = new System.Drawing.Size(101, 21);
			this.cbEncoding.TabIndex = 0;
			// 
			// bSave
			// 
			this.bSave.Dock = System.Windows.Forms.DockStyle.Fill;
			this.bSave.Location = new System.Drawing.Point(110, 3);
			this.bSave.Margin = new System.Windows.Forms.Padding(3, 3, 6, 3);
			this.bSave.Name = "bSave";
			this.bSave.Size = new System.Drawing.Size(98, 22);
			this.bSave.TabIndex = 6;
			this.bSave.Text = "Zapisz";
			this.bSave.UseVisualStyleBackColor = true;
			// 
			// lvhPageFields
			// 
			this.lvhPageFields.Text = "Nowe kolumny";
			this.lvhPageFields.Width = 350;
			// 
			// tMainPanel
			// 
			this.tMainPanel.ColumnCount = 2;
			this.tMainPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 63F));
			this.tMainPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 37F));
			this.tMainPanel.Controls.Add(this.lvPageFields, 0, 0);
			this.tMainPanel.Controls.Add(this.lvDBCols, 1, 0);
			this.tMainPanel.Controls.Add(this.tableLayoutPanel1, 1, 2);
			this.tMainPanel.Controls.Add(this.listBox1, 0, 1);
			this.tMainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tMainPanel.Location = new System.Drawing.Point(0, 0);
			this.tMainPanel.Name = "tMainPanel";
			this.tMainPanel.RowCount = 3;
			this.tMainPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tMainPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 102F));
			this.tMainPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
			this.tMainPanel.Size = new System.Drawing.Size(577, 353);
			this.tMainPanel.TabIndex = 1;
			// 
			// lvPageFields
			// 
			this.lvPageFields.AllowDrop = true;
			this.lvPageFields.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.lvhPageFields});
			this.lvPageFields.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lvPageFields.FullRowSelect = true;
			this.lvPageFields.GridLines = true;
			this.lvPageFields.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
			this.lvPageFields.HideSelection = false;
			this.lvPageFields.Location = new System.Drawing.Point(6, 6);
			this.lvPageFields.Margin = new System.Windows.Forms.Padding(6, 6, 3, 3);
			this.lvPageFields.MultiSelect = false;
			this.lvPageFields.Name = "lvPageFields";
			this.lvPageFields.Size = new System.Drawing.Size(354, 214);
			this.lvPageFields.TabIndex = 8;
			this.lvPageFields.UseCompatibleStateImageBehavior = false;
			this.lvPageFields.View = System.Windows.Forms.View.Details;
			// 
			// listBox1
			// 
			this.listBox1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.listBox1.FormattingEnabled = true;
			this.listBox1.Location = new System.Drawing.Point(6, 226);
			this.listBox1.Margin = new System.Windows.Forms.Padding(6, 3, 3, 3);
			this.listBox1.Name = "listBox1";
			this.listBox1.Size = new System.Drawing.Size(354, 96);
			this.listBox1.TabIndex = 12;
			// 
			// JoinColsForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(577, 353);
			this.Controls.Add(this.tMainPanel);
			this.Name = "JoinColsForm";
			this.Text = "Łączenie kolumn";
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tMainPanel.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.ListView lvDBCols;
		private System.Windows.Forms.ColumnHeader lvhDBCols;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.Button bSave;
		private System.Windows.Forms.ColumnHeader lvhPageFields;
		private System.Windows.Forms.TableLayoutPanel tMainPanel;
		private System.Windows.Forms.ListView lvPageFields;
		private System.Windows.Forms.ComboBox cbEncoding;
		private System.Windows.Forms.ListBox listBox1;
	}
}