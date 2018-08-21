namespace CDesigner
{
	partial class EditRowsForm
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
            this.tlMain = new System.Windows.Forms.TableLayoutPanel();
            this.scMain = new System.Windows.Forms.SplitContainer();
            this.tlDataGrid = new System.Windows.Forms.TableLayoutPanel();
            this.pGrid = new System.Windows.Forms.Panel();
            this.gvData = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tlDataControls = new System.Windows.Forms.TableLayoutPanel();
            this.bRemoveRow = new System.Windows.Forms.Button();
            this.bInsertRow = new System.Windows.Forms.Button();
            this.bLastPage = new System.Windows.Forms.Button();
            this.bNextPage = new System.Windows.Forms.Button();
            this.bPrevPage = new System.Windows.Forms.Button();
            this.lPageStat = new System.Windows.Forms.Label();
            this.tbPageNum = new System.Windows.Forms.TextBox();
            this.tbRowsPerPage = new System.Windows.Forms.TextBox();
            this.lRowsPerPage = new System.Windows.Forms.Label();
            this.bFirstPage = new System.Windows.Forms.Button();
            this.scSidebar = new System.Windows.Forms.SplitContainer();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.lvColumns = new System.Windows.Forms.ListView();
            this.lvcColumnName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tlEditDataControls = new System.Windows.Forms.TableLayoutPanel();
            this.gbType = new System.Windows.Forms.GroupBox();
            this.cbColumnType = new System.Windows.Forms.ComboBox();
            this.gbSearchAndReplace = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.bReplaceAll = new System.Windows.Forms.Button();
            this.bCount = new System.Windows.Forms.Button();
            this.bReplace = new System.Windows.Forms.Button();
            this.cbCaseSensitive = new System.Windows.Forms.CheckBox();
            this.cbExact = new System.Windows.Forms.CheckBox();
            this.cbUseRegex = new System.Windows.Forms.CheckBox();
            this.tbSearch = new System.Windows.Forms.TextBox();
            this.tbReplace = new System.Windows.Forms.TextBox();
            this.bSearchRange = new System.Windows.Forms.ComboBox();
            this.bSearch = new System.Windows.Forms.Button();
            this.tlStatusBar = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.bSave = new System.Windows.Forms.Button();
            this.bCancel = new System.Windows.Forms.Button();
            this.tlMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.scMain)).BeginInit();
            this.scMain.Panel1.SuspendLayout();
            this.scMain.Panel2.SuspendLayout();
            this.scMain.SuspendLayout();
            this.tlDataGrid.SuspendLayout();
            this.pGrid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvData)).BeginInit();
            this.tlDataControls.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.scSidebar)).BeginInit();
            this.scSidebar.Panel1.SuspendLayout();
            this.scSidebar.Panel2.SuspendLayout();
            this.scSidebar.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tlEditDataControls.SuspendLayout();
            this.gbType.SuspendLayout();
            this.gbSearchAndReplace.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tlStatusBar.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlMain
            // 
            this.tlMain.ColumnCount = 1;
            this.tlMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlMain.Controls.Add(this.scMain, 0, 0);
            this.tlMain.Controls.Add(this.tlStatusBar, 0, 1);
            this.tlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlMain.Location = new System.Drawing.Point(0, 0);
            this.tlMain.Name = "tlMain";
            this.tlMain.RowCount = 2;
            this.tlMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tlMain.Size = new System.Drawing.Size(784, 522);
            this.tlMain.TabIndex = 0;
            // 
            // scMain
            // 
            this.scMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scMain.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.scMain.Location = new System.Drawing.Point(6, 6);
            this.scMain.Margin = new System.Windows.Forms.Padding(6);
            this.scMain.Name = "scMain";
            // 
            // scMain.Panel1
            // 
            this.scMain.Panel1.Controls.Add(this.tlDataGrid);
            this.scMain.Panel1MinSize = 500;
            // 
            // scMain.Panel2
            // 
            this.scMain.Panel2.Controls.Add(this.scSidebar);
            this.scMain.Panel2MinSize = 204;
            this.scMain.Size = new System.Drawing.Size(772, 478);
            this.scMain.SplitterDistance = 519;
            this.scMain.TabIndex = 1;
            // 
            // tlDataGrid
            // 
            this.tlDataGrid.ColumnCount = 1;
            this.tlDataGrid.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlDataGrid.Controls.Add(this.pGrid, 0, 1);
            this.tlDataGrid.Controls.Add(this.tlDataControls, 0, 0);
            this.tlDataGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlDataGrid.Location = new System.Drawing.Point(0, 0);
            this.tlDataGrid.Margin = new System.Windows.Forms.Padding(0);
            this.tlDataGrid.Name = "tlDataGrid";
            this.tlDataGrid.RowCount = 2;
            this.tlDataGrid.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.tlDataGrid.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlDataGrid.Size = new System.Drawing.Size(519, 478);
            this.tlDataGrid.TabIndex = 1;
            // 
            // pGrid
            // 
            this.pGrid.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pGrid.Controls.Add(this.gvData);
            this.pGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pGrid.Location = new System.Drawing.Point(0, 26);
            this.pGrid.Margin = new System.Windows.Forms.Padding(0);
            this.pGrid.Name = "pGrid";
            this.pGrid.Padding = new System.Windows.Forms.Padding(1);
            this.pGrid.Size = new System.Drawing.Size(519, 452);
            this.pGrid.TabIndex = 0;
            // 
            // gvData
            // 
            this.gvData.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.gvData.ColumnHeadersHeight = 28;
            this.gvData.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4});
            this.gvData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gvData.EnableHeadersVisualStyles = false;
            this.gvData.Location = new System.Drawing.Point(1, 1);
            this.gvData.Margin = new System.Windows.Forms.Padding(0);
            this.gvData.Name = "gvData";
            this.gvData.Size = new System.Drawing.Size(515, 448);
            this.gvData.TabIndex = 0;
            this.gvData.VirtualMode = true;
            this.gvData.CellValueNeeded += new System.Windows.Forms.DataGridViewCellValueEventHandler(this.gvData_CellValueNeeded);
            this.gvData.CellValuePushed += new System.Windows.Forms.DataGridViewCellValueEventHandler(this.gvData_CellValuePushed);
            this.gvData.SelectionChanged += new System.EventHandler(this.gvData_SelectionChanged);
            this.gvData.UserDeletedRow += new System.Windows.Forms.DataGridViewRowEventHandler(this.gvData_UserDeletedRow);
            this.gvData.UserDeletingRow += new System.Windows.Forms.DataGridViewRowCancelEventHandler(this.gvData_UserDeletingRow);
            // 
            // Column1
            // 
            this.Column1.HeaderText = "Column1";
            this.Column1.Name = "Column1";
            this.Column1.Width = 73;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "Column2";
            this.Column2.Name = "Column2";
            this.Column2.Width = 73;
            // 
            // Column3
            // 
            this.Column3.HeaderText = "Column3";
            this.Column3.Name = "Column3";
            this.Column3.Width = 73;
            // 
            // Column4
            // 
            this.Column4.HeaderText = "Column4";
            this.Column4.Name = "Column4";
            this.Column4.Width = 73;
            // 
            // tlDataControls
            // 
            this.tlDataControls.ColumnCount = 10;
            this.tlDataControls.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.tlDataControls.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.tlDataControls.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 55F));
            this.tlDataControls.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 55F));
            this.tlDataControls.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.tlDataControls.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.tlDataControls.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tlDataControls.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.tlDataControls.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlDataControls.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 55F));
            this.tlDataControls.Controls.Add(this.bRemoveRow, 7, 0);
            this.tlDataControls.Controls.Add(this.bInsertRow, 6, 0);
            this.tlDataControls.Controls.Add(this.bLastPage, 5, 0);
            this.tlDataControls.Controls.Add(this.bNextPage, 4, 0);
            this.tlDataControls.Controls.Add(this.bPrevPage, 1, 0);
            this.tlDataControls.Controls.Add(this.lPageStat, 3, 0);
            this.tlDataControls.Controls.Add(this.tbPageNum, 2, 0);
            this.tlDataControls.Controls.Add(this.tbRowsPerPage, 9, 0);
            this.tlDataControls.Controls.Add(this.lRowsPerPage, 8, 0);
            this.tlDataControls.Controls.Add(this.bFirstPage, 0, 0);
            this.tlDataControls.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlDataControls.Location = new System.Drawing.Point(0, 0);
            this.tlDataControls.Margin = new System.Windows.Forms.Padding(0, 0, 0, 2);
            this.tlDataControls.Name = "tlDataControls";
            this.tlDataControls.RowCount = 1;
            this.tlDataControls.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlDataControls.Size = new System.Drawing.Size(519, 24);
            this.tlDataControls.TabIndex = 1;
            // 
            // bRemoveRow
            // 
            this.bRemoveRow.Dock = System.Windows.Forms.DockStyle.Fill;
            this.bRemoveRow.Enabled = false;
            this.bRemoveRow.Location = new System.Drawing.Point(241, 0);
            this.bRemoveRow.Margin = new System.Windows.Forms.Padding(0);
            this.bRemoveRow.Name = "bRemoveRow";
            this.bRemoveRow.Size = new System.Drawing.Size(24, 24);
            this.bRemoveRow.TabIndex = 12;
            this.bRemoveRow.UseVisualStyleBackColor = true;
            this.bRemoveRow.Click += new System.EventHandler(this.bRemoveRow_Click);
            // 
            // bInsertRow
            // 
            this.bInsertRow.Dock = System.Windows.Forms.DockStyle.Fill;
            this.bInsertRow.Location = new System.Drawing.Point(217, 0);
            this.bInsertRow.Margin = new System.Windows.Forms.Padding(11, 0, 0, 0);
            this.bInsertRow.Name = "bInsertRow";
            this.bInsertRow.Size = new System.Drawing.Size(24, 24);
            this.bInsertRow.TabIndex = 11;
            this.bInsertRow.UseVisualStyleBackColor = true;
            this.bInsertRow.Click += new System.EventHandler(this.bInsertRow_Click);
            // 
            // bLastPage
            // 
            this.bLastPage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.bLastPage.Location = new System.Drawing.Point(182, 0);
            this.bLastPage.Margin = new System.Windows.Forms.Padding(0);
            this.bLastPage.Name = "bLastPage";
            this.bLastPage.Size = new System.Drawing.Size(24, 24);
            this.bLastPage.TabIndex = 10;
            this.bLastPage.UseVisualStyleBackColor = true;
            this.bLastPage.Click += new System.EventHandler(this.bLastPage_Click);
            // 
            // bNextPage
            // 
            this.bNextPage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.bNextPage.Location = new System.Drawing.Point(158, 0);
            this.bNextPage.Margin = new System.Windows.Forms.Padding(0);
            this.bNextPage.Name = "bNextPage";
            this.bNextPage.Size = new System.Drawing.Size(24, 24);
            this.bNextPage.TabIndex = 9;
            this.bNextPage.UseVisualStyleBackColor = true;
            this.bNextPage.Click += new System.EventHandler(this.bNextPage_Click);
            // 
            // bPrevPage
            // 
            this.bPrevPage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.bPrevPage.Location = new System.Drawing.Point(24, 0);
            this.bPrevPage.Margin = new System.Windows.Forms.Padding(0);
            this.bPrevPage.Name = "bPrevPage";
            this.bPrevPage.Size = new System.Drawing.Size(24, 24);
            this.bPrevPage.TabIndex = 8;
            this.bPrevPage.UseVisualStyleBackColor = true;
            this.bPrevPage.Click += new System.EventHandler(this.bPrevPage_Click);
            // 
            // lPageStat
            // 
            this.lPageStat.AutoSize = true;
            this.lPageStat.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lPageStat.Location = new System.Drawing.Point(103, 0);
            this.lPageStat.Margin = new System.Windows.Forms.Padding(0, 0, 3, 2);
            this.lPageStat.Name = "lPageStat";
            this.lPageStat.Size = new System.Drawing.Size(52, 22);
            this.lPageStat.TabIndex = 6;
            this.lPageStat.Text = "z {0}";
            this.lPageStat.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tbPageNum
            // 
            this.tbPageNum.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbPageNum.Location = new System.Drawing.Point(51, 2);
            this.tbPageNum.Margin = new System.Windows.Forms.Padding(3, 2, 0, 3);
            this.tbPageNum.Name = "tbPageNum";
            this.tbPageNum.Size = new System.Drawing.Size(52, 20);
            this.tbPageNum.TabIndex = 5;
            this.tbPageNum.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.tbPageNum.TextChanged += new System.EventHandler(this.tbPageNum_TextChanged);
            this.tbPageNum.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbPageNum_KeyDown);
            this.tbPageNum.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbPageNum_KeyPress);
            // 
            // tbRowsPerPage
            // 
            this.tbRowsPerPage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbRowsPerPage.Location = new System.Drawing.Point(464, 2);
            this.tbRowsPerPage.Margin = new System.Windows.Forms.Padding(0, 2, 0, 3);
            this.tbRowsPerPage.Name = "tbRowsPerPage";
            this.tbRowsPerPage.Size = new System.Drawing.Size(55, 20);
            this.tbRowsPerPage.TabIndex = 1;
            this.tbRowsPerPage.Text = "50";
            this.tbRowsPerPage.TextChanged += new System.EventHandler(this.tbRowsPerPage_TextChanged);
            this.tbRowsPerPage.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbRowsPerPage_KeyDown);
            this.tbRowsPerPage.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbRowsPerPage_KeyPress);
            // 
            // lRowsPerPage
            // 
            this.lRowsPerPage.AutoSize = true;
            this.lRowsPerPage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lRowsPerPage.Location = new System.Drawing.Point(265, 0);
            this.lRowsPerPage.Margin = new System.Windows.Forms.Padding(0, 0, 3, 2);
            this.lRowsPerPage.Name = "lRowsPerPage";
            this.lRowsPerPage.Size = new System.Drawing.Size(196, 22);
            this.lRowsPerPage.TabIndex = 2;
            this.lRowsPerPage.Text = "Ilość wierszy na stronę:";
            this.lRowsPerPage.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // bFirstPage
            // 
            this.bFirstPage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.bFirstPage.Location = new System.Drawing.Point(0, 0);
            this.bFirstPage.Margin = new System.Windows.Forms.Padding(0);
            this.bFirstPage.Name = "bFirstPage";
            this.bFirstPage.Size = new System.Drawing.Size(24, 24);
            this.bFirstPage.TabIndex = 7;
            this.bFirstPage.UseVisualStyleBackColor = true;
            this.bFirstPage.Click += new System.EventHandler(this.bFirstPage_Click);
            // 
            // scSidebar
            // 
            this.scSidebar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scSidebar.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.scSidebar.Location = new System.Drawing.Point(0, 0);
            this.scSidebar.Margin = new System.Windows.Forms.Padding(0);
            this.scSidebar.Name = "scSidebar";
            this.scSidebar.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // scSidebar.Panel1
            // 
            this.scSidebar.Panel1.Controls.Add(this.tableLayoutPanel1);
            this.scSidebar.Panel1MinSize = 150;
            // 
            // scSidebar.Panel2
            // 
            this.scSidebar.Panel2.AutoScroll = true;
            this.scSidebar.Panel2.Controls.Add(this.tlEditDataControls);
            this.scSidebar.Panel2MinSize = 100;
            this.scSidebar.Size = new System.Drawing.Size(249, 478);
            this.scSidebar.SplitterDistance = 197;
            this.scSidebar.TabIndex = 0;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.lvColumns, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(249, 197);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // lvColumns
            // 
            this.lvColumns.CheckBoxes = true;
            this.lvColumns.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.lvcColumnName});
            this.tableLayoutPanel1.SetColumnSpan(this.lvColumns, 2);
            this.lvColumns.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvColumns.FullRowSelect = true;
            this.lvColumns.GridLines = true;
            this.lvColumns.HideSelection = false;
            this.lvColumns.Location = new System.Drawing.Point(0, 0);
            this.lvColumns.Margin = new System.Windows.Forms.Padding(0);
            this.lvColumns.Name = "lvColumns";
            this.lvColumns.Size = new System.Drawing.Size(249, 169);
            this.lvColumns.TabIndex = 0;
            this.lvColumns.UseCompatibleStateImageBehavior = false;
            this.lvColumns.View = System.Windows.Forms.View.Details;
            this.lvColumns.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.lvColumns_ItemChecked);
            this.lvColumns.SelectedIndexChanged += new System.EventHandler(this.lvColumns_SelectedIndexChanged);
            // 
            // lvcColumnName
            // 
            this.lvcColumnName.Width = 200;
            // 
            // tlEditDataControls
            // 
            this.tlEditDataControls.AutoSize = true;
            this.tlEditDataControls.ColumnCount = 1;
            this.tlEditDataControls.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlEditDataControls.Controls.Add(this.gbType, 0, 0);
            this.tlEditDataControls.Controls.Add(this.gbSearchAndReplace, 0, 1);
            this.tlEditDataControls.Dock = System.Windows.Forms.DockStyle.Top;
            this.tlEditDataControls.Location = new System.Drawing.Point(0, 0);
            this.tlEditDataControls.Name = "tlEditDataControls";
            this.tlEditDataControls.RowCount = 2;
            this.tlEditDataControls.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 46F));
            this.tlEditDataControls.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlEditDataControls.Size = new System.Drawing.Size(249, 276);
            this.tlEditDataControls.TabIndex = 0;
            // 
            // gbType
            // 
            this.gbType.Controls.Add(this.cbColumnType);
            this.gbType.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbType.Location = new System.Drawing.Point(0, 0);
            this.gbType.Margin = new System.Windows.Forms.Padding(0);
            this.gbType.Name = "gbType";
            this.gbType.Padding = new System.Windows.Forms.Padding(6);
            this.gbType.Size = new System.Drawing.Size(249, 46);
            this.gbType.TabIndex = 0;
            this.gbType.TabStop = false;
            this.gbType.Text = "Typ kolumny";
            // 
            // cbColumnType
            // 
            this.cbColumnType.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cbColumnType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbColumnType.FormattingEnabled = true;
            this.cbColumnType.Items.AddRange(new object[] {
            "String",
            "Integer",
            "Decimal",
            "Character"});
            this.cbColumnType.Location = new System.Drawing.Point(6, 19);
            this.cbColumnType.Margin = new System.Windows.Forms.Padding(0);
            this.cbColumnType.Name = "cbColumnType";
            this.cbColumnType.Size = new System.Drawing.Size(237, 21);
            this.cbColumnType.TabIndex = 0;
            this.cbColumnType.SelectedIndexChanged += new System.EventHandler(this.cbColumnType_SelectedIndexChanged);
            // 
            // gbSearchAndReplace
            // 
            this.gbSearchAndReplace.AutoSize = true;
            this.gbSearchAndReplace.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.gbSearchAndReplace.Controls.Add(this.tableLayoutPanel2);
            this.gbSearchAndReplace.Dock = System.Windows.Forms.DockStyle.Top;
            this.gbSearchAndReplace.Location = new System.Drawing.Point(0, 52);
            this.gbSearchAndReplace.Margin = new System.Windows.Forms.Padding(0, 6, 0, 0);
            this.gbSearchAndReplace.Name = "gbSearchAndReplace";
            this.gbSearchAndReplace.Padding = new System.Windows.Forms.Padding(6);
            this.gbSearchAndReplace.Size = new System.Drawing.Size(249, 224);
            this.gbSearchAndReplace.TabIndex = 1;
            this.gbSearchAndReplace.TabStop = false;
            this.gbSearchAndReplace.Text = "Wyszukaj i zamień";
            this.gbSearchAndReplace.SizeChanged += new System.EventHandler(this.gbSearchAndReplace_SizeChanged);
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.AutoSize = true;
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Controls.Add(this.bReplaceAll, 0, 7);
            this.tableLayoutPanel2.Controls.Add(this.bCount, 0, 7);
            this.tableLayoutPanel2.Controls.Add(this.bReplace, 0, 6);
            this.tableLayoutPanel2.Controls.Add(this.cbCaseSensitive, 0, 4);
            this.tableLayoutPanel2.Controls.Add(this.cbExact, 0, 3);
            this.tableLayoutPanel2.Controls.Add(this.cbUseRegex, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.tbSearch, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.tbReplace, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.bSearchRange, 0, 5);
            this.tableLayoutPanel2.Controls.Add(this.bSearch, 1, 6);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(6, 19);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 8;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 27F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.Size = new System.Drawing.Size(237, 199);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // bReplaceAll
            // 
            this.bReplaceAll.Dock = System.Windows.Forms.DockStyle.Top;
            this.bReplaceAll.Location = new System.Drawing.Point(0, 175);
            this.bReplaceAll.Margin = new System.Windows.Forms.Padding(0, 3, 2, 0);
            this.bReplaceAll.Name = "bReplaceAll";
            this.bReplaceAll.Size = new System.Drawing.Size(116, 24);
            this.bReplaceAll.TabIndex = 11;
            this.bReplaceAll.Text = "Zamień wszystkie";
            this.bReplaceAll.UseVisualStyleBackColor = true;
            // 
            // bCount
            // 
            this.bCount.Dock = System.Windows.Forms.DockStyle.Top;
            this.bCount.Location = new System.Drawing.Point(120, 175);
            this.bCount.Margin = new System.Windows.Forms.Padding(2, 3, 0, 0);
            this.bCount.Name = "bCount";
            this.bCount.Size = new System.Drawing.Size(117, 24);
            this.bCount.TabIndex = 10;
            this.bCount.Text = "Policz wystąpienia";
            this.bCount.UseVisualStyleBackColor = true;
            // 
            // bReplace
            // 
            this.bReplace.AutoSize = true;
            this.bReplace.Dock = System.Windows.Forms.DockStyle.Fill;
            this.bReplace.Location = new System.Drawing.Point(0, 148);
            this.bReplace.Margin = new System.Windows.Forms.Padding(0, 3, 2, 0);
            this.bReplace.Name = "bReplace";
            this.bReplace.Size = new System.Drawing.Size(116, 24);
            this.bReplace.TabIndex = 9;
            this.bReplace.Text = "Zamień";
            this.bReplace.UseVisualStyleBackColor = true;
            // 
            // cbCaseSensitive
            // 
            this.cbCaseSensitive.AutoSize = true;
            this.cbCaseSensitive.Checked = true;
            this.cbCaseSensitive.CheckState = System.Windows.Forms.CheckState.Checked;
            this.tableLayoutPanel2.SetColumnSpan(this.cbCaseSensitive, 2);
            this.cbCaseSensitive.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cbCaseSensitive.Location = new System.Drawing.Point(3, 98);
            this.cbCaseSensitive.Name = "cbCaseSensitive";
            this.cbCaseSensitive.Size = new System.Drawing.Size(231, 18);
            this.cbCaseSensitive.TabIndex = 8;
            this.cbCaseSensitive.Text = "Uwzględniaj wielkość liter";
            this.cbCaseSensitive.UseVisualStyleBackColor = true;
            // 
            // cbExact
            // 
            this.cbExact.AutoSize = true;
            this.tableLayoutPanel2.SetColumnSpan(this.cbExact, 2);
            this.cbExact.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cbExact.Location = new System.Drawing.Point(3, 74);
            this.cbExact.Name = "cbExact";
            this.cbExact.Size = new System.Drawing.Size(231, 18);
            this.cbExact.TabIndex = 7;
            this.cbExact.Text = "Szukaj tylko całych wyrazów";
            this.cbExact.UseVisualStyleBackColor = true;
            // 
            // cbUseRegex
            // 
            this.cbUseRegex.AutoSize = true;
            this.tableLayoutPanel2.SetColumnSpan(this.cbUseRegex, 2);
            this.cbUseRegex.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cbUseRegex.Location = new System.Drawing.Point(3, 50);
            this.cbUseRegex.Name = "cbUseRegex";
            this.cbUseRegex.Size = new System.Drawing.Size(231, 18);
            this.cbUseRegex.TabIndex = 6;
            this.cbUseRegex.Text = "Użyj wyrażeń regularnych";
            this.cbUseRegex.UseVisualStyleBackColor = true;
            // 
            // tbSearch
            // 
            this.tableLayoutPanel2.SetColumnSpan(this.tbSearch, 2);
            this.tbSearch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbSearch.Location = new System.Drawing.Point(0, 0);
            this.tbSearch.Margin = new System.Windows.Forms.Padding(0, 0, 0, 3);
            this.tbSearch.Name = "tbSearch";
            this.tbSearch.Size = new System.Drawing.Size(237, 20);
            this.tbSearch.TabIndex = 0;
            // 
            // tbReplace
            // 
            this.tableLayoutPanel2.SetColumnSpan(this.tbReplace, 2);
            this.tbReplace.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbReplace.Location = new System.Drawing.Point(0, 25);
            this.tbReplace.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.tbReplace.Name = "tbReplace";
            this.tbReplace.Size = new System.Drawing.Size(237, 20);
            this.tbReplace.TabIndex = 1;
            // 
            // bSearchRange
            // 
            this.tableLayoutPanel2.SetColumnSpan(this.bSearchRange, 2);
            this.bSearchRange.Dock = System.Windows.Forms.DockStyle.Fill;
            this.bSearchRange.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.bSearchRange.FormattingEnabled = true;
            this.bSearchRange.Items.AddRange(new object[] {
            "Cały zbiór danych",
            "Zaznaczone kolumny",
            "Zaznaczone wiersze",
            "Zaznaczone komórki"});
            this.bSearchRange.Location = new System.Drawing.Point(0, 122);
            this.bSearchRange.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.bSearchRange.Name = "bSearchRange";
            this.bSearchRange.Size = new System.Drawing.Size(237, 21);
            this.bSearchRange.TabIndex = 3;
            // 
            // bSearch
            // 
            this.bSearch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.bSearch.Location = new System.Drawing.Point(120, 148);
            this.bSearch.Margin = new System.Windows.Forms.Padding(2, 3, 0, 0);
            this.bSearch.Name = "bSearch";
            this.bSearch.Size = new System.Drawing.Size(117, 24);
            this.bSearch.TabIndex = 4;
            this.bSearch.Text = "Wyszukaj";
            this.bSearch.UseVisualStyleBackColor = true;
            // 
            // tlStatusBar
            // 
            this.tlStatusBar.BackColor = System.Drawing.SystemColors.ControlLight;
            this.tlStatusBar.ColumnCount = 2;
            this.tlStatusBar.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlStatusBar.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 259F));
            this.tlStatusBar.Controls.Add(this.tableLayoutPanel3, 1, 0);
            this.tlStatusBar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlStatusBar.Location = new System.Drawing.Point(0, 490);
            this.tlStatusBar.Margin = new System.Windows.Forms.Padding(0);
            this.tlStatusBar.Name = "tlStatusBar";
            this.tlStatusBar.Padding = new System.Windows.Forms.Padding(0, 1, 0, 0);
            this.tlStatusBar.RowCount = 1;
            this.tlStatusBar.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlStatusBar.Size = new System.Drawing.Size(784, 32);
            this.tlStatusBar.TabIndex = 2;
            this.tlStatusBar.Paint += new System.Windows.Forms.PaintEventHandler(this.tlStatusBar_Paint);
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 2;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.Controls.Add(this.bSave, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.bCancel, 1, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(525, 1);
            this.tableLayoutPanel3.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 1;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(259, 31);
            this.tableLayoutPanel3.TabIndex = 0;
            // 
            // bSave
            // 
            this.bSave.Dock = System.Windows.Forms.DockStyle.Fill;
            this.bSave.Location = new System.Drawing.Point(4, 3);
            this.bSave.Margin = new System.Windows.Forms.Padding(4, 3, 2, 3);
            this.bSave.Name = "bSave";
            this.bSave.Size = new System.Drawing.Size(123, 25);
            this.bSave.TabIndex = 0;
            this.bSave.Text = "Zapisz";
            this.bSave.UseVisualStyleBackColor = true;
            this.bSave.Click += new System.EventHandler(this.bSave_Click);
            // 
            // bCancel
            // 
            this.bCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.bCancel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.bCancel.Location = new System.Drawing.Point(131, 3);
            this.bCancel.Margin = new System.Windows.Forms.Padding(2, 3, 6, 3);
            this.bCancel.Name = "bCancel";
            this.bCancel.Size = new System.Drawing.Size(122, 25);
            this.bCancel.TabIndex = 1;
            this.bCancel.Text = "Zaniechaj";
            this.bCancel.UseVisualStyleBackColor = true;
            this.bCancel.Click += new System.EventHandler(this.bCancel_Click);
            // 
            // EditRowsForm
            // 
            this.AcceptButton = this.bSave;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.bCancel;
            this.ClientSize = new System.Drawing.Size(784, 522);
            this.Controls.Add(this.tlMain);
            this.KeyPreview = true;
            this.MinimumSize = new System.Drawing.Size(800, 560);
            this.Name = "EditRowsForm";
            this.Text = "EditDataForm";
            this.tlMain.ResumeLayout(false);
            this.scMain.Panel1.ResumeLayout(false);
            this.scMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.scMain)).EndInit();
            this.scMain.ResumeLayout(false);
            this.tlDataGrid.ResumeLayout(false);
            this.pGrid.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gvData)).EndInit();
            this.tlDataControls.ResumeLayout(false);
            this.tlDataControls.PerformLayout();
            this.scSidebar.Panel1.ResumeLayout(false);
            this.scSidebar.Panel2.ResumeLayout(false);
            this.scSidebar.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.scSidebar)).EndInit();
            this.scSidebar.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tlEditDataControls.ResumeLayout(false);
            this.tlEditDataControls.PerformLayout();
            this.gbType.ResumeLayout(false);
            this.gbSearchAndReplace.ResumeLayout(false);
            this.gbSearchAndReplace.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.tlStatusBar.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TableLayoutPanel tlMain;
		private System.Windows.Forms.SplitContainer scMain;
		private System.Windows.Forms.TableLayoutPanel tlDataGrid;
		private System.Windows.Forms.Panel pGrid;
		private System.Windows.Forms.DataGridView gvData;
		private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
		private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
		private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
		private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
		private System.Windows.Forms.TableLayoutPanel tlDataControls;
		private System.Windows.Forms.Button bRemoveRow;
		private System.Windows.Forms.Button bInsertRow;
		private System.Windows.Forms.Button bLastPage;
		private System.Windows.Forms.Button bNextPage;
		private System.Windows.Forms.Button bPrevPage;
		private System.Windows.Forms.Label lPageStat;
		private System.Windows.Forms.TextBox tbPageNum;
		private System.Windows.Forms.TextBox tbRowsPerPage;
		private System.Windows.Forms.Label lRowsPerPage;
		private System.Windows.Forms.Button bFirstPage;
		private System.Windows.Forms.TableLayoutPanel tlStatusBar;
		private System.Windows.Forms.SplitContainer scSidebar;
		private System.Windows.Forms.ListView lvColumns;
		private System.Windows.Forms.ColumnHeader lvcColumnName;
		private System.Windows.Forms.TableLayoutPanel tlEditDataControls;
		private System.Windows.Forms.GroupBox gbType;
		private System.Windows.Forms.GroupBox gbSearchAndReplace;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
		private System.Windows.Forms.TextBox tbSearch;
		private System.Windows.Forms.TextBox tbReplace;
		private System.Windows.Forms.ComboBox cbColumnType;
		private System.Windows.Forms.ComboBox bSearchRange;
		private System.Windows.Forms.Button bSearch;
		private System.Windows.Forms.Button bReplaceAll;
		private System.Windows.Forms.Button bCount;
		private System.Windows.Forms.Button bReplace;
		private System.Windows.Forms.CheckBox cbCaseSensitive;
		private System.Windows.Forms.CheckBox cbExact;
		private System.Windows.Forms.CheckBox cbUseRegex;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Button bSave;
        private System.Windows.Forms.Button bCancel;
	}
}