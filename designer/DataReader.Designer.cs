namespace CDesigner
{
	partial class DataReader
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DataReader));
			this.tMainPanel = new System.Windows.Forms.TableLayoutPanel();
			this.fPageContainer = new System.Windows.Forms.FlowLayoutPanel();
			this.lPage = new System.Windows.Forms.Label();
			this.nPage = new System.Windows.Forms.NumericUpDown();
			this.lvPageFields = new System.Windows.Forms.ListView();
			this.lvhPageFields = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.lvhPageCols = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.lvDBCols = new System.Windows.Forms.ListView();
			this.lvhDBCols = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.flEncoding = new System.Windows.Forms.FlowLayoutPanel();
			this.cbEncoding = new System.Windows.Forms.ComboBox();
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.bSave = new System.Windows.Forms.Button();
			this.tbSpaces = new System.Windows.Forms.TextBox();
			this.tMainPanel.SuspendLayout();
			this.fPageContainer.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.nPage)).BeginInit();
			this.flEncoding.SuspendLayout();
			this.tableLayoutPanel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// tMainPanel
			// 
			this.tMainPanel.ColumnCount = 3;
			this.tMainPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.35888F));
			this.tMainPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.3189F));
			this.tMainPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.32222F));
			this.tMainPanel.Controls.Add(this.fPageContainer, 0, 1);
			this.tMainPanel.Controls.Add(this.lvPageFields, 0, 0);
			this.tMainPanel.Controls.Add(this.lvDBCols, 2, 0);
			this.tMainPanel.Controls.Add(this.flEncoding, 1, 1);
			this.tMainPanel.Controls.Add(this.tableLayoutPanel1, 2, 1);
			this.tMainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tMainPanel.Location = new System.Drawing.Point(0, 0);
			this.tMainPanel.Name = "tMainPanel";
			this.tMainPanel.RowCount = 2;
			this.tMainPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tMainPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
			this.tMainPanel.Size = new System.Drawing.Size(574, 352);
			this.tMainPanel.TabIndex = 0;
			// 
			// fPageContainer
			// 
			this.fPageContainer.Controls.Add(this.lPage);
			this.fPageContainer.Controls.Add(this.nPage);
			this.fPageContainer.Dock = System.Windows.Forms.DockStyle.Fill;
			this.fPageContainer.Location = new System.Drawing.Point(3, 324);
			this.fPageContainer.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
			this.fPageContainer.Name = "fPageContainer";
			this.fPageContainer.Size = new System.Drawing.Size(188, 28);
			this.fPageContainer.TabIndex = 4;
			// 
			// lPage
			// 
			this.lPage.AutoSize = true;
			this.lPage.Dock = System.Windows.Forms.DockStyle.Left;
			this.lPage.Location = new System.Drawing.Point(3, 0);
			this.lPage.Name = "lPage";
			this.lPage.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.lPage.Size = new System.Drawing.Size(41, 26);
			this.lPage.TabIndex = 0;
			this.lPage.Text = "Strona:";
			this.lPage.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// nPage
			// 
			this.nPage.Location = new System.Drawing.Point(50, 3);
			this.nPage.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.nPage.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.nPage.Name = "nPage";
			this.nPage.Size = new System.Drawing.Size(35, 20);
			this.nPage.TabIndex = 1;
			this.nPage.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.nPage.ValueChanged += new System.EventHandler(this.nPage_ValueChanged);
			// 
			// lvPageFields
			// 
			this.lvPageFields.AllowDrop = true;
			this.lvPageFields.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.lvhPageFields,
            this.lvhPageCols});
			this.tMainPanel.SetColumnSpan(this.lvPageFields, 2);
			this.lvPageFields.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lvPageFields.FullRowSelect = true;
			this.lvPageFields.GridLines = true;
			this.lvPageFields.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
			this.lvPageFields.HideSelection = false;
			this.lvPageFields.Location = new System.Drawing.Point(6, 6);
			this.lvPageFields.Margin = new System.Windows.Forms.Padding(6, 6, 3, 3);
			this.lvPageFields.MultiSelect = false;
			this.lvPageFields.Name = "lvPageFields";
			this.lvPageFields.Size = new System.Drawing.Size(373, 315);
			this.lvPageFields.TabIndex = 8;
			this.lvPageFields.UseCompatibleStateImageBehavior = false;
			this.lvPageFields.View = System.Windows.Forms.View.Details;
			this.lvPageFields.DragDrop += new System.Windows.Forms.DragEventHandler(this.lvPageFields_DragDrop);
			this.lvPageFields.DragEnter += new System.Windows.Forms.DragEventHandler(this.lvPageFields_DragEnter);
			this.lvPageFields.DragOver += new System.Windows.Forms.DragEventHandler(this.lvPageFields_DragOver);
			this.lvPageFields.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lvPageFields_KeyDown);
			// 
			// lvhPageFields
			// 
			this.lvhPageFields.Text = "Dostępne pola na stronie";
			this.lvhPageFields.Width = 185;
			// 
			// lvhPageCols
			// 
			this.lvhPageCols.Text = "Przyporządkowane kolumny";
			this.lvhPageCols.Width = 184;
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
			this.lvDBCols.Location = new System.Drawing.Point(385, 6);
			this.lvDBCols.Margin = new System.Windows.Forms.Padding(3, 6, 6, 3);
			this.lvDBCols.MultiSelect = false;
			this.lvDBCols.Name = "lvDBCols";
			this.lvDBCols.ShowGroups = false;
			this.lvDBCols.Size = new System.Drawing.Size(183, 315);
			this.lvDBCols.TabIndex = 9;
			this.lvDBCols.UseCompatibleStateImageBehavior = false;
			this.lvDBCols.View = System.Windows.Forms.View.Details;
			this.lvDBCols.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.lvDBCols_ItemChecked);
			this.lvDBCols.DragOver += new System.Windows.Forms.DragEventHandler(this.lvDBCols_DragOver);
			this.lvDBCols.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lvDBCols_MouseDown);
			// 
			// lvhDBCols
			// 
			this.lvhDBCols.Text = "Dostępne kolumny";
			this.lvhDBCols.Width = 179;
			// 
			// flEncoding
			// 
			this.flEncoding.Controls.Add(this.cbEncoding);
			this.flEncoding.Dock = System.Windows.Forms.DockStyle.Fill;
			this.flEncoding.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
			this.flEncoding.Location = new System.Drawing.Point(191, 324);
			this.flEncoding.Margin = new System.Windows.Forms.Padding(0);
			this.flEncoding.Name = "flEncoding";
			this.flEncoding.Size = new System.Drawing.Size(191, 28);
			this.flEncoding.TabIndex = 10;
			// 
			// cbEncoding
			// 
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
			this.cbEncoding.Location = new System.Drawing.Point(73, 3);
			this.cbEncoding.Name = "cbEncoding";
			this.cbEncoding.Size = new System.Drawing.Size(115, 21);
			this.cbEncoding.TabIndex = 0;
			this.cbEncoding.SelectedIndexChanged += new System.EventHandler(this.cbEncoding_SelectedIndexChanged);
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.ColumnCount = 2;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel1.Controls.Add(this.bSave, 1, 0);
			this.tableLayoutPanel1.Controls.Add(this.tbSpaces, 0, 0);
			this.tableLayoutPanel1.Location = new System.Drawing.Point(382, 324);
			this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 1;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(192, 28);
			this.tableLayoutPanel1.TabIndex = 11;
			// 
			// bSave
			// 
			this.bSave.Dock = System.Windows.Forms.DockStyle.Fill;
			this.bSave.Location = new System.Drawing.Point(99, 3);
			this.bSave.Margin = new System.Windows.Forms.Padding(3, 3, 6, 3);
			this.bSave.Name = "bSave";
			this.bSave.Size = new System.Drawing.Size(87, 22);
			this.bSave.TabIndex = 6;
			this.bSave.Text = "Zapisz";
			this.bSave.UseVisualStyleBackColor = true;
			this.bSave.Click += new System.EventHandler(this.bSave_Click);
			// 
			// tbSpaces
			// 
			this.tbSpaces.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tbSpaces.Location = new System.Drawing.Point(3, 4);
			this.tbSpaces.Margin = new System.Windows.Forms.Padding(3, 4, 3, 3);
			this.tbSpaces.Name = "tbSpaces";
			this.tbSpaces.Size = new System.Drawing.Size(90, 20);
			this.tbSpaces.TabIndex = 0;
			this.tbSpaces.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbSpaces_KeyPress);
			this.tbSpaces.Leave += new System.EventHandler(this.tbSpaces_Leave);
			// 
			// DataReader
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(574, 352);
			this.Controls.Add(this.tMainPanel);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.KeyPreview = true;
			this.MaximizeBox = false;
			this.Name = "DataReader";
			this.Text = "Wybór kolumn";
			this.Move += new System.EventHandler(this.DataReader_Move);
			this.tMainPanel.ResumeLayout(false);
			this.fPageContainer.ResumeLayout(false);
			this.fPageContainer.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.nPage)).EndInit();
			this.flEncoding.ResumeLayout(false);
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel1.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TableLayoutPanel tMainPanel;
		private System.Windows.Forms.FlowLayoutPanel fPageContainer;
		private System.Windows.Forms.Label lPage;
		private System.Windows.Forms.NumericUpDown nPage;
		private System.Windows.Forms.ListView lvPageFields;
		private System.Windows.Forms.ColumnHeader lvhPageFields;
		private System.Windows.Forms.ColumnHeader lvhPageCols;
		private System.Windows.Forms.ListView lvDBCols;
		private System.Windows.Forms.ColumnHeader lvhDBCols;
		private System.Windows.Forms.FlowLayoutPanel flEncoding;
		private System.Windows.Forms.ComboBox cbEncoding;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.Button bSave;
		private System.Windows.Forms.TextBox tbSpaces;
	}
}