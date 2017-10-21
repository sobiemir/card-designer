namespace CDesigner
{
	partial class SetColumns
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
			this.tMainPanel = new System.Windows.Forms.TableLayoutPanel();
			this.bSave = new System.Windows.Forms.Button();
			this.fPageContainer = new System.Windows.Forms.FlowLayoutPanel();
			this.lPage = new System.Windows.Forms.Label();
			this.nPage = new System.Windows.Forms.NumericUpDown();
			this.lvPageFields = new System.Windows.Forms.ListView();
			this.lvhPageFields = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.lvhPageCols = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.lvDBCols = new System.Windows.Forms.ListView();
			this.lvhDBCols = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.tMainPanel.SuspendLayout();
			this.fPageContainer.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.nPage)).BeginInit();
			this.SuspendLayout();
			// 
			// tMainPanel
			// 
			this.tMainPanel.ColumnCount = 2;
			this.tMainPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 66.66666F));
			this.tMainPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
			this.tMainPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tMainPanel.Controls.Add(this.bSave, 1, 1);
			this.tMainPanel.Controls.Add(this.fPageContainer, 0, 1);
			this.tMainPanel.Controls.Add(this.lvPageFields, 0, 0);
			this.tMainPanel.Controls.Add(this.lvDBCols, 1, 0);
			this.tMainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tMainPanel.Location = new System.Drawing.Point(0, 0);
			this.tMainPanel.Name = "tMainPanel";
			this.tMainPanel.RowCount = 2;
			this.tMainPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tMainPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
			this.tMainPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tMainPanel.Size = new System.Drawing.Size(539, 334);
			this.tMainPanel.TabIndex = 0;
			// 
			// bSave
			// 
			this.bSave.Dock = System.Windows.Forms.DockStyle.Right;
			this.bSave.Location = new System.Drawing.Point(453, 309);
			this.bSave.Margin = new System.Windows.Forms.Padding(3, 3, 6, 3);
			this.bSave.Name = "bSave";
			this.bSave.Size = new System.Drawing.Size(80, 22);
			this.bSave.TabIndex = 3;
			this.bSave.Text = "Zapisz";
			this.bSave.UseVisualStyleBackColor = true;
			this.bSave.Click += new System.EventHandler(this.bSave_Click);
			// 
			// fPageContainer
			// 
			this.fPageContainer.Controls.Add(this.lPage);
			this.fPageContainer.Controls.Add(this.nPage);
			this.fPageContainer.Dock = System.Windows.Forms.DockStyle.Fill;
			this.fPageContainer.Location = new System.Drawing.Point(3, 306);
			this.fPageContainer.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
			this.fPageContainer.Name = "fPageContainer";
			this.fPageContainer.Size = new System.Drawing.Size(356, 28);
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
			this.lvPageFields.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lvPageFields.FullRowSelect = true;
			this.lvPageFields.GridLines = true;
			this.lvPageFields.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
			this.lvPageFields.HideSelection = false;
			this.lvPageFields.Location = new System.Drawing.Point(6, 6);
			this.lvPageFields.Margin = new System.Windows.Forms.Padding(6, 6, 3, 3);
			this.lvPageFields.MultiSelect = false;
			this.lvPageFields.Name = "lvPageFields";
			this.lvPageFields.Size = new System.Drawing.Size(350, 297);
			this.lvPageFields.TabIndex = 8;
			this.lvPageFields.UseCompatibleStateImageBehavior = false;
			this.lvPageFields.View = System.Windows.Forms.View.Details;
			this.lvPageFields.DragDrop += new System.Windows.Forms.DragEventHandler(this.lvPageFields_DragDrop);
			this.lvPageFields.DragEnter += new System.Windows.Forms.DragEventHandler(this.lvPageFields_DragEnter);
			this.lvPageFields.DragOver += new System.Windows.Forms.DragEventHandler(this.lvPageFields_DragOver);
			// 
			// lvhPageFields
			// 
			this.lvhPageFields.Text = "Dostępne pola na stronie";
			this.lvhPageFields.Width = 173;
			// 
			// lvhPageCols
			// 
			this.lvhPageCols.Text = "Przyporządkowane kolumny";
			this.lvhPageCols.Width = 173;
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
			this.lvDBCols.Location = new System.Drawing.Point(362, 6);
			this.lvDBCols.Margin = new System.Windows.Forms.Padding(3, 6, 6, 3);
			this.lvDBCols.MultiSelect = false;
			this.lvDBCols.Name = "lvDBCols";
			this.lvDBCols.ShowGroups = false;
			this.lvDBCols.Size = new System.Drawing.Size(171, 297);
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
			this.lvhDBCols.Width = 167;
			// 
			// SetColumns
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(539, 334);
			this.Controls.Add(this.tMainPanel);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.KeyPreview = true;
			this.MaximizeBox = false;
			this.Name = "SetColumns";
			this.Text = "Wybór kolumn";
			this.tMainPanel.ResumeLayout(false);
			this.fPageContainer.ResumeLayout(false);
			this.fPageContainer.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.nPage)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TableLayoutPanel tMainPanel;
		private System.Windows.Forms.Button bSave;
		private System.Windows.Forms.FlowLayoutPanel fPageContainer;
		private System.Windows.Forms.Label lPage;
		private System.Windows.Forms.NumericUpDown nPage;
		private System.Windows.Forms.ListView lvPageFields;
		private System.Windows.Forms.ColumnHeader lvhPageFields;
		private System.Windows.Forms.ColumnHeader lvhPageCols;
		private System.Windows.Forms.ListView lvDBCols;
		private System.Windows.Forms.ColumnHeader lvhDBCols;
	}
}