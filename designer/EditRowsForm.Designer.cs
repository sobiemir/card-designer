namespace CDesigner.Forms
{
	partial class EditRowsForm
	{
	/// @cond DESIGNER
	  
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
			this.SC_Main = new System.Windows.Forms.SplitContainer();
			this.TLP_DataGrid = new System.Windows.Forms.TableLayoutPanel();
			this.P_Grid = new System.Windows.Forms.Panel();
			this.DGV_Data = new System.Windows.Forms.DataGridView();
			this.DGVTBC_Col1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.DGVTBC_Col2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.DGVTBC_Col3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.DGVTBC_Col4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.TLP_DataControls = new System.Windows.Forms.TableLayoutPanel();
			this.B_RemoveRow = new System.Windows.Forms.Button();
			this.B_InsertRow = new System.Windows.Forms.Button();
			this.B_LastPage = new System.Windows.Forms.Button();
			this.B_NextPage = new System.Windows.Forms.Button();
			this.P_PrevPage = new System.Windows.Forms.Button();
			this.L_PageStat = new System.Windows.Forms.Label();
			this.TB_PageNum = new System.Windows.Forms.TextBox();
			this.TB_RowsPerPage = new System.Windows.Forms.TextBox();
			this.L_RowsPerPage = new System.Windows.Forms.Label();
			this.B_FirstPage = new System.Windows.Forms.Button();
			this.SC_Sidebar = new System.Windows.Forms.SplitContainer();
			this.TLP_ColumnView = new System.Windows.Forms.TableLayoutPanel();
			this.LV_Columns = new System.Windows.Forms.ListView();
			this.CH_Column = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.TLP_Controls = new System.Windows.Forms.TableLayoutPanel();
			this.GB_Type = new System.Windows.Forms.GroupBox();
			this.CBX_ColumnType = new System.Windows.Forms.ComboBox();
			this.GB_SearchAndReplace = new System.Windows.Forms.GroupBox();
			this.TLP_SearchAndReplace = new System.Windows.Forms.TableLayoutPanel();
			this.B_ReplaceAll = new System.Windows.Forms.Button();
			this.B_Count = new System.Windows.Forms.Button();
			this.B_Replace = new System.Windows.Forms.Button();
			this.CB_CaseSensitive = new System.Windows.Forms.CheckBox();
			this.CB_Exact = new System.Windows.Forms.CheckBox();
			this.CB_UseRegex = new System.Windows.Forms.CheckBox();
			this.TB_Search = new System.Windows.Forms.TextBox();
			this.TB_Replace = new System.Windows.Forms.TextBox();
			this.B_SearchRange = new System.Windows.Forms.ComboBox();
			this.B_Search = new System.Windows.Forms.Button();
			this.TLP_StatusBar = new System.Windows.Forms.TableLayoutPanel();
			this.TLP_Buttons = new System.Windows.Forms.TableLayoutPanel();
			this.B_Save = new System.Windows.Forms.Button();
			this.B_Cancel = new System.Windows.Forms.Button();
			this.TLP_Main.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.SC_Main)).BeginInit();
			this.SC_Main.Panel1.SuspendLayout();
			this.SC_Main.Panel2.SuspendLayout();
			this.SC_Main.SuspendLayout();
			this.TLP_DataGrid.SuspendLayout();
			this.P_Grid.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.DGV_Data)).BeginInit();
			this.TLP_DataControls.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.SC_Sidebar)).BeginInit();
			this.SC_Sidebar.Panel1.SuspendLayout();
			this.SC_Sidebar.Panel2.SuspendLayout();
			this.SC_Sidebar.SuspendLayout();
			this.TLP_ColumnView.SuspendLayout();
			this.TLP_Controls.SuspendLayout();
			this.GB_Type.SuspendLayout();
			this.GB_SearchAndReplace.SuspendLayout();
			this.TLP_SearchAndReplace.SuspendLayout();
			this.TLP_StatusBar.SuspendLayout();
			this.TLP_Buttons.SuspendLayout();
			this.SuspendLayout();
			// 
			// TLP_Main
			// 
			this.TLP_Main.ColumnCount = 1;
			this.TLP_Main.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.TLP_Main.Controls.Add(this.SC_Main, 0, 0);
			this.TLP_Main.Controls.Add(this.TLP_StatusBar, 0, 1);
			this.TLP_Main.Dock = System.Windows.Forms.DockStyle.Fill;
			this.TLP_Main.Location = new System.Drawing.Point(0, 0);
			this.TLP_Main.Name = "TLP_Main";
			this.TLP_Main.RowCount = 2;
			this.TLP_Main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.TLP_Main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
			this.TLP_Main.Size = new System.Drawing.Size(784, 522);
			this.TLP_Main.TabIndex = 0;
			// 
			// SC_Main
			// 
			this.SC_Main.Dock = System.Windows.Forms.DockStyle.Fill;
			this.SC_Main.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
			this.SC_Main.Location = new System.Drawing.Point(6, 6);
			this.SC_Main.Margin = new System.Windows.Forms.Padding(6);
			this.SC_Main.Name = "SC_Main";
			// 
			// SC_Main.Panel1
			// 
			this.SC_Main.Panel1.Controls.Add(this.TLP_DataGrid);
			this.SC_Main.Panel1MinSize = 500;
			// 
			// SC_Main.Panel2
			// 
			this.SC_Main.Panel2.Controls.Add(this.SC_Sidebar);
			this.SC_Main.Panel2MinSize = 204;
			this.SC_Main.Size = new System.Drawing.Size(772, 478);
			this.SC_Main.SplitterDistance = 519;
			this.SC_Main.TabIndex = 1;
			// 
			// TLP_DataGrid
			// 
			this.TLP_DataGrid.ColumnCount = 1;
			this.TLP_DataGrid.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.TLP_DataGrid.Controls.Add(this.P_Grid, 0, 1);
			this.TLP_DataGrid.Controls.Add(this.TLP_DataControls, 0, 0);
			this.TLP_DataGrid.Dock = System.Windows.Forms.DockStyle.Fill;
			this.TLP_DataGrid.Location = new System.Drawing.Point(0, 0);
			this.TLP_DataGrid.Margin = new System.Windows.Forms.Padding(0);
			this.TLP_DataGrid.Name = "TLP_DataGrid";
			this.TLP_DataGrid.RowCount = 2;
			this.TLP_DataGrid.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
			this.TLP_DataGrid.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.TLP_DataGrid.Size = new System.Drawing.Size(519, 478);
			this.TLP_DataGrid.TabIndex = 1;
			// 
			// P_Grid
			// 
			this.P_Grid.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.P_Grid.Controls.Add(this.DGV_Data);
			this.P_Grid.Dock = System.Windows.Forms.DockStyle.Fill;
			this.P_Grid.Location = new System.Drawing.Point(0, 26);
			this.P_Grid.Margin = new System.Windows.Forms.Padding(0);
			this.P_Grid.Name = "P_Grid";
			this.P_Grid.Padding = new System.Windows.Forms.Padding(1);
			this.P_Grid.Size = new System.Drawing.Size(519, 452);
			this.P_Grid.TabIndex = 0;
			// 
			// DGV_Data
			// 
			this.DGV_Data.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.DGV_Data.ColumnHeadersHeight = 28;
			this.DGV_Data.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
			this.DGVTBC_Col1,
			this.DGVTBC_Col2,
			this.DGVTBC_Col3,
			this.DGVTBC_Col4});
			this.DGV_Data.Dock = System.Windows.Forms.DockStyle.Fill;
			this.DGV_Data.EnableHeadersVisualStyles = false;
			this.DGV_Data.Location = new System.Drawing.Point(1, 1);
			this.DGV_Data.Margin = new System.Windows.Forms.Padding(0);
			this.DGV_Data.Name = "DGV_Data";
			this.DGV_Data.Size = new System.Drawing.Size(515, 448);
			this.DGV_Data.TabIndex = 0;
			this.DGV_Data.VirtualMode = true;
			this.DGV_Data.CellValueNeeded += new System.Windows.Forms.DataGridViewCellValueEventHandler(this.DGV_Data_CellValueNeeded);
			this.DGV_Data.CellValuePushed += new System.Windows.Forms.DataGridViewCellValueEventHandler(this.DGV_Data_CellValuePushed);
			this.DGV_Data.SelectionChanged += new System.EventHandler(this.DGV_Data_SelectionChanged);
			this.DGV_Data.UserDeletedRow += new System.Windows.Forms.DataGridViewRowEventHandler(this.DGV_Data_UserDeletedRow);
			this.DGV_Data.UserDeletingRow += new System.Windows.Forms.DataGridViewRowCancelEventHandler(this.DGV_Data_UserDeletingRow);
			// 
			// DGVTBC_Col1
			// 
			this.DGVTBC_Col1.HeaderText = "Column1";
			this.DGVTBC_Col1.Name = "DGVTBC_Col1";
			this.DGVTBC_Col1.Width = 73;
			// 
			// DGVTBC_Col2
			// 
			this.DGVTBC_Col2.HeaderText = "Column2";
			this.DGVTBC_Col2.Name = "DGVTBC_Col2";
			this.DGVTBC_Col2.Width = 73;
			// 
			// DGVTBC_Col3
			// 
			this.DGVTBC_Col3.HeaderText = "Column3";
			this.DGVTBC_Col3.Name = "DGVTBC_Col3";
			this.DGVTBC_Col3.Width = 73;
			// 
			// DGVTBC_Col4
			// 
			this.DGVTBC_Col4.HeaderText = "Column4";
			this.DGVTBC_Col4.Name = "DGVTBC_Col4";
			this.DGVTBC_Col4.Width = 73;
			// 
			// TLP_DataControls
			// 
			this.TLP_DataControls.ColumnCount = 10;
			this.TLP_DataControls.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 24F));
			this.TLP_DataControls.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 24F));
			this.TLP_DataControls.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 55F));
			this.TLP_DataControls.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 55F));
			this.TLP_DataControls.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 24F));
			this.TLP_DataControls.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 24F));
			this.TLP_DataControls.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 35F));
			this.TLP_DataControls.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 24F));
			this.TLP_DataControls.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.TLP_DataControls.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 55F));
			this.TLP_DataControls.Controls.Add(this.B_RemoveRow, 7, 0);
			this.TLP_DataControls.Controls.Add(this.B_InsertRow, 6, 0);
			this.TLP_DataControls.Controls.Add(this.B_LastPage, 5, 0);
			this.TLP_DataControls.Controls.Add(this.B_NextPage, 4, 0);
			this.TLP_DataControls.Controls.Add(this.P_PrevPage, 1, 0);
			this.TLP_DataControls.Controls.Add(this.L_PageStat, 3, 0);
			this.TLP_DataControls.Controls.Add(this.TB_PageNum, 2, 0);
			this.TLP_DataControls.Controls.Add(this.TB_RowsPerPage, 9, 0);
			this.TLP_DataControls.Controls.Add(this.L_RowsPerPage, 8, 0);
			this.TLP_DataControls.Controls.Add(this.B_FirstPage, 0, 0);
			this.TLP_DataControls.Dock = System.Windows.Forms.DockStyle.Fill;
			this.TLP_DataControls.Location = new System.Drawing.Point(0, 0);
			this.TLP_DataControls.Margin = new System.Windows.Forms.Padding(0, 0, 0, 2);
			this.TLP_DataControls.Name = "TLP_DataControls";
			this.TLP_DataControls.RowCount = 1;
			this.TLP_DataControls.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.TLP_DataControls.Size = new System.Drawing.Size(519, 24);
			this.TLP_DataControls.TabIndex = 1;
			// 
			// B_RemoveRow
			// 
			this.B_RemoveRow.Dock = System.Windows.Forms.DockStyle.Fill;
			this.B_RemoveRow.Enabled = false;
			this.B_RemoveRow.Location = new System.Drawing.Point(241, 0);
			this.B_RemoveRow.Margin = new System.Windows.Forms.Padding(0);
			this.B_RemoveRow.Name = "B_RemoveRow";
			this.B_RemoveRow.Size = new System.Drawing.Size(24, 24);
			this.B_RemoveRow.TabIndex = 12;
			this.B_RemoveRow.UseVisualStyleBackColor = true;
			this.B_RemoveRow.Click += new System.EventHandler(this.B_RemoveRow_Click);
			// 
			// B_InsertRow
			// 
			this.B_InsertRow.Dock = System.Windows.Forms.DockStyle.Fill;
			this.B_InsertRow.Location = new System.Drawing.Point(217, 0);
			this.B_InsertRow.Margin = new System.Windows.Forms.Padding(11, 0, 0, 0);
			this.B_InsertRow.Name = "B_InsertRow";
			this.B_InsertRow.Size = new System.Drawing.Size(24, 24);
			this.B_InsertRow.TabIndex = 11;
			this.B_InsertRow.UseVisualStyleBackColor = true;
			this.B_InsertRow.Click += new System.EventHandler(this.B_InsertRow_Click);
			// 
			// B_LastPage
			// 
			this.B_LastPage.Dock = System.Windows.Forms.DockStyle.Fill;
			this.B_LastPage.Location = new System.Drawing.Point(182, 0);
			this.B_LastPage.Margin = new System.Windows.Forms.Padding(0);
			this.B_LastPage.Name = "B_LastPage";
			this.B_LastPage.Size = new System.Drawing.Size(24, 24);
			this.B_LastPage.TabIndex = 10;
			this.B_LastPage.UseVisualStyleBackColor = true;
			this.B_LastPage.Click += new System.EventHandler(this.B_LastPage_Click);
			// 
			// B_NextPage
			// 
			this.B_NextPage.Dock = System.Windows.Forms.DockStyle.Fill;
			this.B_NextPage.Location = new System.Drawing.Point(158, 0);
			this.B_NextPage.Margin = new System.Windows.Forms.Padding(0);
			this.B_NextPage.Name = "B_NextPage";
			this.B_NextPage.Size = new System.Drawing.Size(24, 24);
			this.B_NextPage.TabIndex = 9;
			this.B_NextPage.UseVisualStyleBackColor = true;
			this.B_NextPage.Click += new System.EventHandler(this.B_NextPage_Click);
			// 
			// P_PrevPage
			// 
			this.P_PrevPage.Dock = System.Windows.Forms.DockStyle.Fill;
			this.P_PrevPage.Location = new System.Drawing.Point(24, 0);
			this.P_PrevPage.Margin = new System.Windows.Forms.Padding(0);
			this.P_PrevPage.Name = "P_PrevPage";
			this.P_PrevPage.Size = new System.Drawing.Size(24, 24);
			this.P_PrevPage.TabIndex = 8;
			this.P_PrevPage.UseVisualStyleBackColor = true;
			this.P_PrevPage.Click += new System.EventHandler(this.B_PrevPage_Click);
			// 
			// L_PageStat
			// 
			this.L_PageStat.AutoSize = true;
			this.L_PageStat.Dock = System.Windows.Forms.DockStyle.Fill;
			this.L_PageStat.Location = new System.Drawing.Point(103, 0);
			this.L_PageStat.Margin = new System.Windows.Forms.Padding(0, 0, 3, 2);
			this.L_PageStat.Name = "L_PageStat";
			this.L_PageStat.Size = new System.Drawing.Size(52, 22);
			this.L_PageStat.TabIndex = 6;
			this.L_PageStat.Text = "z {0}";
			this.L_PageStat.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// TB_PageNum
			// 
			this.TB_PageNum.Dock = System.Windows.Forms.DockStyle.Fill;
			this.TB_PageNum.Location = new System.Drawing.Point(51, 2);
			this.TB_PageNum.Margin = new System.Windows.Forms.Padding(3, 2, 0, 3);
			this.TB_PageNum.Name = "TB_PageNum";
			this.TB_PageNum.Size = new System.Drawing.Size(52, 20);
			this.TB_PageNum.TabIndex = 5;
			this.TB_PageNum.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.TB_PageNum.TextChanged += new System.EventHandler(this.TB_PageNum_TextChanged);
			this.TB_PageNum.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TB_PageNum_KeyDown);
			this.TB_PageNum.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TB_PageNum_KeyPress);
			// 
			// TB_RowsPerPage
			// 
			this.TB_RowsPerPage.Dock = System.Windows.Forms.DockStyle.Fill;
			this.TB_RowsPerPage.Location = new System.Drawing.Point(464, 2);
			this.TB_RowsPerPage.Margin = new System.Windows.Forms.Padding(0, 2, 0, 3);
			this.TB_RowsPerPage.Name = "TB_RowsPerPage";
			this.TB_RowsPerPage.Size = new System.Drawing.Size(55, 20);
			this.TB_RowsPerPage.TabIndex = 1;
			this.TB_RowsPerPage.Text = "50";
			this.TB_RowsPerPage.TextChanged += new System.EventHandler(this.TB_RowsPerPage_TextChanged);
			this.TB_RowsPerPage.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TB_RowsPerPage_KeyDown);
			this.TB_RowsPerPage.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TB_RowsPerPage_KeyPress);
			// 
			// L_RowsPerPage
			// 
			this.L_RowsPerPage.AutoSize = true;
			this.L_RowsPerPage.Dock = System.Windows.Forms.DockStyle.Fill;
			this.L_RowsPerPage.Location = new System.Drawing.Point(265, 0);
			this.L_RowsPerPage.Margin = new System.Windows.Forms.Padding(0, 0, 3, 2);
			this.L_RowsPerPage.Name = "L_RowsPerPage";
			this.L_RowsPerPage.Size = new System.Drawing.Size(196, 22);
			this.L_RowsPerPage.TabIndex = 2;
			this.L_RowsPerPage.Text = "Ilość wierszy na stronę:";
			this.L_RowsPerPage.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// B_FirstPage
			// 
			this.B_FirstPage.Dock = System.Windows.Forms.DockStyle.Fill;
			this.B_FirstPage.Location = new System.Drawing.Point(0, 0);
			this.B_FirstPage.Margin = new System.Windows.Forms.Padding(0);
			this.B_FirstPage.Name = "B_FirstPage";
			this.B_FirstPage.Size = new System.Drawing.Size(24, 24);
			this.B_FirstPage.TabIndex = 7;
			this.B_FirstPage.UseVisualStyleBackColor = true;
			this.B_FirstPage.Click += new System.EventHandler(this.B_FirstPage_Click);
			// 
			// SC_Sidebar
			// 
			this.SC_Sidebar.Dock = System.Windows.Forms.DockStyle.Fill;
			this.SC_Sidebar.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
			this.SC_Sidebar.Location = new System.Drawing.Point(0, 0);
			this.SC_Sidebar.Margin = new System.Windows.Forms.Padding(0);
			this.SC_Sidebar.Name = "SC_Sidebar";
			this.SC_Sidebar.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// SC_Sidebar.Panel1
			// 
			this.SC_Sidebar.Panel1.Controls.Add(this.TLP_ColumnView);
			this.SC_Sidebar.Panel1MinSize = 150;
			// 
			// SC_Sidebar.Panel2
			// 
			this.SC_Sidebar.Panel2.AutoScroll = true;
			this.SC_Sidebar.Panel2.Controls.Add(this.TLP_Controls);
			this.SC_Sidebar.Panel2MinSize = 100;
			this.SC_Sidebar.Size = new System.Drawing.Size(249, 478);
			this.SC_Sidebar.SplitterDistance = 197;
			this.SC_Sidebar.TabIndex = 0;
			// 
			// TLP_ColumnView
			// 
			this.TLP_ColumnView.ColumnCount = 2;
			this.TLP_ColumnView.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.TLP_ColumnView.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.TLP_ColumnView.Controls.Add(this.LV_Columns, 0, 0);
			this.TLP_ColumnView.Dock = System.Windows.Forms.DockStyle.Fill;
			this.TLP_ColumnView.Location = new System.Drawing.Point(0, 0);
			this.TLP_ColumnView.Name = "TLP_ColumnView";
			this.TLP_ColumnView.RowCount = 2;
			this.TLP_ColumnView.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.TLP_ColumnView.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
			this.TLP_ColumnView.Size = new System.Drawing.Size(249, 197);
			this.TLP_ColumnView.TabIndex = 1;
			// 
			// LV_Columns
			// 
			this.LV_Columns.CheckBoxes = true;
			this.LV_Columns.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
			this.CH_Column});
			this.TLP_ColumnView.SetColumnSpan(this.LV_Columns, 2);
			this.LV_Columns.Dock = System.Windows.Forms.DockStyle.Fill;
			this.LV_Columns.FullRowSelect = true;
			this.LV_Columns.GridLines = true;
			this.LV_Columns.HideSelection = false;
			this.LV_Columns.Location = new System.Drawing.Point(0, 0);
			this.LV_Columns.Margin = new System.Windows.Forms.Padding(0);
			this.LV_Columns.Name = "LV_Columns";
			this.LV_Columns.Size = new System.Drawing.Size(249, 169);
			this.LV_Columns.TabIndex = 0;
			this.LV_Columns.UseCompatibleStateImageBehavior = false;
			this.LV_Columns.View = System.Windows.Forms.View.Details;
			// 
			// CH_Column
			// 
			this.CH_Column.Width = 200;
			// 
			// TLP_Controls
			// 
			this.TLP_Controls.AutoSize = true;
			this.TLP_Controls.ColumnCount = 1;
			this.TLP_Controls.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.TLP_Controls.Controls.Add(this.GB_Type, 0, 0);
			this.TLP_Controls.Controls.Add(this.GB_SearchAndReplace, 0, 1);
			this.TLP_Controls.Dock = System.Windows.Forms.DockStyle.Top;
			this.TLP_Controls.Location = new System.Drawing.Point(0, 0);
			this.TLP_Controls.Name = "TLP_Controls";
			this.TLP_Controls.RowCount = 2;
			this.TLP_Controls.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 46F));
			this.TLP_Controls.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_Controls.Size = new System.Drawing.Size(249, 276);
			this.TLP_Controls.TabIndex = 0;
			// 
			// GB_Type
			// 
			this.GB_Type.Controls.Add(this.CBX_ColumnType);
			this.GB_Type.Dock = System.Windows.Forms.DockStyle.Fill;
			this.GB_Type.Location = new System.Drawing.Point(0, 0);
			this.GB_Type.Margin = new System.Windows.Forms.Padding(0);
			this.GB_Type.Name = "GB_Type";
			this.GB_Type.Padding = new System.Windows.Forms.Padding(6);
			this.GB_Type.Size = new System.Drawing.Size(249, 46);
			this.GB_Type.TabIndex = 0;
			this.GB_Type.TabStop = false;
			this.GB_Type.Text = "Typ kolumny";
			// 
			// CBX_ColumnType
			// 
			this.CBX_ColumnType.Dock = System.Windows.Forms.DockStyle.Fill;
			this.CBX_ColumnType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.CBX_ColumnType.FormattingEnabled = true;
			this.CBX_ColumnType.Items.AddRange(new object[] {
			"String",
			"Integer",
			"Decimal",
			"Character"});
			this.CBX_ColumnType.Location = new System.Drawing.Point(6, 19);
			this.CBX_ColumnType.Margin = new System.Windows.Forms.Padding(0);
			this.CBX_ColumnType.Name = "CBX_ColumnType";
			this.CBX_ColumnType.Size = new System.Drawing.Size(237, 21);
			this.CBX_ColumnType.TabIndex = 0;
			// 
			// GB_SearchAndReplace
			// 
			this.GB_SearchAndReplace.AutoSize = true;
			this.GB_SearchAndReplace.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.GB_SearchAndReplace.Controls.Add(this.TLP_SearchAndReplace);
			this.GB_SearchAndReplace.Dock = System.Windows.Forms.DockStyle.Top;
			this.GB_SearchAndReplace.Location = new System.Drawing.Point(0, 52);
			this.GB_SearchAndReplace.Margin = new System.Windows.Forms.Padding(0, 6, 0, 0);
			this.GB_SearchAndReplace.Name = "GB_SearchAndReplace";
			this.GB_SearchAndReplace.Padding = new System.Windows.Forms.Padding(6);
			this.GB_SearchAndReplace.Size = new System.Drawing.Size(249, 224);
			this.GB_SearchAndReplace.TabIndex = 1;
			this.GB_SearchAndReplace.TabStop = false;
			this.GB_SearchAndReplace.Text = "Wyszukaj i zamień";
			// 
			// TLP_SearchAndReplace
			// 
			this.TLP_SearchAndReplace.AutoSize = true;
			this.TLP_SearchAndReplace.ColumnCount = 2;
			this.TLP_SearchAndReplace.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.TLP_SearchAndReplace.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.TLP_SearchAndReplace.Controls.Add(this.B_ReplaceAll, 0, 7);
			this.TLP_SearchAndReplace.Controls.Add(this.B_Count, 0, 7);
			this.TLP_SearchAndReplace.Controls.Add(this.B_Replace, 0, 6);
			this.TLP_SearchAndReplace.Controls.Add(this.CB_CaseSensitive, 0, 4);
			this.TLP_SearchAndReplace.Controls.Add(this.CB_Exact, 0, 3);
			this.TLP_SearchAndReplace.Controls.Add(this.CB_UseRegex, 0, 2);
			this.TLP_SearchAndReplace.Controls.Add(this.TB_Search, 0, 0);
			this.TLP_SearchAndReplace.Controls.Add(this.TB_Replace, 0, 1);
			this.TLP_SearchAndReplace.Controls.Add(this.B_SearchRange, 0, 5);
			this.TLP_SearchAndReplace.Controls.Add(this.B_Search, 1, 6);
			this.TLP_SearchAndReplace.Dock = System.Windows.Forms.DockStyle.Fill;
			this.TLP_SearchAndReplace.Location = new System.Drawing.Point(6, 19);
			this.TLP_SearchAndReplace.Name = "TLP_SearchAndReplace";
			this.TLP_SearchAndReplace.RowCount = 8;
			this.TLP_SearchAndReplace.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22F));
			this.TLP_SearchAndReplace.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
			this.TLP_SearchAndReplace.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
			this.TLP_SearchAndReplace.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
			this.TLP_SearchAndReplace.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
			this.TLP_SearchAndReplace.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
			this.TLP_SearchAndReplace.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 27F));
			this.TLP_SearchAndReplace.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_SearchAndReplace.Size = new System.Drawing.Size(237, 199);
			this.TLP_SearchAndReplace.TabIndex = 0;
			// 
			// B_ReplaceAll
			// 
			this.B_ReplaceAll.Dock = System.Windows.Forms.DockStyle.Top;
			this.B_ReplaceAll.Location = new System.Drawing.Point(0, 175);
			this.B_ReplaceAll.Margin = new System.Windows.Forms.Padding(0, 3, 2, 0);
			this.B_ReplaceAll.Name = "B_ReplaceAll";
			this.B_ReplaceAll.Size = new System.Drawing.Size(116, 24);
			this.B_ReplaceAll.TabIndex = 11;
			this.B_ReplaceAll.Text = "Zamień wszystkie";
			this.B_ReplaceAll.UseVisualStyleBackColor = true;
			// 
			// B_Count
			// 
			this.B_Count.Dock = System.Windows.Forms.DockStyle.Top;
			this.B_Count.Location = new System.Drawing.Point(120, 175);
			this.B_Count.Margin = new System.Windows.Forms.Padding(2, 3, 0, 0);
			this.B_Count.Name = "B_Count";
			this.B_Count.Size = new System.Drawing.Size(117, 24);
			this.B_Count.TabIndex = 10;
			this.B_Count.Text = "Policz wystąpienia";
			this.B_Count.UseVisualStyleBackColor = true;
			// 
			// B_Replace
			// 
			this.B_Replace.AutoSize = true;
			this.B_Replace.Dock = System.Windows.Forms.DockStyle.Fill;
			this.B_Replace.Location = new System.Drawing.Point(0, 148);
			this.B_Replace.Margin = new System.Windows.Forms.Padding(0, 3, 2, 0);
			this.B_Replace.Name = "B_Replace";
			this.B_Replace.Size = new System.Drawing.Size(116, 24);
			this.B_Replace.TabIndex = 9;
			this.B_Replace.Text = "Zamień";
			this.B_Replace.UseVisualStyleBackColor = true;
			// 
			// CB_CaseSensitive
			// 
			this.CB_CaseSensitive.AutoSize = true;
			this.CB_CaseSensitive.Checked = true;
			this.CB_CaseSensitive.CheckState = System.Windows.Forms.CheckState.Checked;
			this.TLP_SearchAndReplace.SetColumnSpan(this.CB_CaseSensitive, 2);
			this.CB_CaseSensitive.Dock = System.Windows.Forms.DockStyle.Fill;
			this.CB_CaseSensitive.Location = new System.Drawing.Point(3, 98);
			this.CB_CaseSensitive.Name = "CB_CaseSensitive";
			this.CB_CaseSensitive.Size = new System.Drawing.Size(231, 18);
			this.CB_CaseSensitive.TabIndex = 8;
			this.CB_CaseSensitive.Text = "Uwzględniaj wielkość liter";
			this.CB_CaseSensitive.UseVisualStyleBackColor = true;
			// 
			// CB_Exact
			// 
			this.CB_Exact.AutoSize = true;
			this.TLP_SearchAndReplace.SetColumnSpan(this.CB_Exact, 2);
			this.CB_Exact.Dock = System.Windows.Forms.DockStyle.Fill;
			this.CB_Exact.Location = new System.Drawing.Point(3, 74);
			this.CB_Exact.Name = "CB_Exact";
			this.CB_Exact.Size = new System.Drawing.Size(231, 18);
			this.CB_Exact.TabIndex = 7;
			this.CB_Exact.Text = "Szukaj tylko całych wyrazów";
			this.CB_Exact.UseVisualStyleBackColor = true;
			// 
			// CB_UseRegex
			// 
			this.CB_UseRegex.AutoSize = true;
			this.TLP_SearchAndReplace.SetColumnSpan(this.CB_UseRegex, 2);
			this.CB_UseRegex.Dock = System.Windows.Forms.DockStyle.Fill;
			this.CB_UseRegex.Location = new System.Drawing.Point(3, 50);
			this.CB_UseRegex.Name = "CB_UseRegex";
			this.CB_UseRegex.Size = new System.Drawing.Size(231, 18);
			this.CB_UseRegex.TabIndex = 6;
			this.CB_UseRegex.Text = "Użyj wyrażeń regularnych";
			this.CB_UseRegex.UseVisualStyleBackColor = true;
			// 
			// TB_Search
			// 
			this.TLP_SearchAndReplace.SetColumnSpan(this.TB_Search, 2);
			this.TB_Search.Dock = System.Windows.Forms.DockStyle.Fill;
			this.TB_Search.Location = new System.Drawing.Point(0, 0);
			this.TB_Search.Margin = new System.Windows.Forms.Padding(0, 0, 0, 3);
			this.TB_Search.Name = "TB_Search";
			this.TB_Search.Size = new System.Drawing.Size(237, 20);
			this.TB_Search.TabIndex = 0;
			// 
			// TB_Replace
			// 
			this.TLP_SearchAndReplace.SetColumnSpan(this.TB_Replace, 2);
			this.TB_Replace.Dock = System.Windows.Forms.DockStyle.Fill;
			this.TB_Replace.Location = new System.Drawing.Point(0, 25);
			this.TB_Replace.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
			this.TB_Replace.Name = "TB_Replace";
			this.TB_Replace.Size = new System.Drawing.Size(237, 20);
			this.TB_Replace.TabIndex = 1;
			// 
			// B_SearchRange
			// 
			this.TLP_SearchAndReplace.SetColumnSpan(this.B_SearchRange, 2);
			this.B_SearchRange.Dock = System.Windows.Forms.DockStyle.Fill;
			this.B_SearchRange.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.B_SearchRange.FormattingEnabled = true;
			this.B_SearchRange.Items.AddRange(new object[] {
			"Cały zbiór danych",
			"Zaznaczone kolumny",
			"Zaznaczone wiersze",
			"Zaznaczone komórki"});
			this.B_SearchRange.Location = new System.Drawing.Point(0, 122);
			this.B_SearchRange.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
			this.B_SearchRange.Name = "B_SearchRange";
			this.B_SearchRange.Size = new System.Drawing.Size(237, 21);
			this.B_SearchRange.TabIndex = 3;
			// 
			// B_Search
			// 
			this.B_Search.Dock = System.Windows.Forms.DockStyle.Fill;
			this.B_Search.Location = new System.Drawing.Point(120, 148);
			this.B_Search.Margin = new System.Windows.Forms.Padding(2, 3, 0, 0);
			this.B_Search.Name = "B_Search";
			this.B_Search.Size = new System.Drawing.Size(117, 24);
			this.B_Search.TabIndex = 4;
			this.B_Search.Text = "Wyszukaj";
			this.B_Search.UseVisualStyleBackColor = true;
			// 
			// TLP_StatusBar
			// 
			this.TLP_StatusBar.BackColor = System.Drawing.SystemColors.ControlLight;
			this.TLP_StatusBar.ColumnCount = 2;
			this.TLP_StatusBar.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.TLP_StatusBar.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 259F));
			this.TLP_StatusBar.Controls.Add(this.TLP_Buttons, 1, 0);
			this.TLP_StatusBar.Dock = System.Windows.Forms.DockStyle.Fill;
			this.TLP_StatusBar.Location = new System.Drawing.Point(0, 490);
			this.TLP_StatusBar.Margin = new System.Windows.Forms.Padding(0);
			this.TLP_StatusBar.Name = "TLP_StatusBar";
			this.TLP_StatusBar.Padding = new System.Windows.Forms.Padding(0, 1, 0, 0);
			this.TLP_StatusBar.RowCount = 1;
			this.TLP_StatusBar.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.TLP_StatusBar.Size = new System.Drawing.Size(784, 32);
			this.TLP_StatusBar.TabIndex = 2;
			this.TLP_StatusBar.Paint += new System.Windows.Forms.PaintEventHandler(this.TLP_StatusBar_Paint);
			// 
			// TLP_Buttons
			// 
			this.TLP_Buttons.ColumnCount = 2;
			this.TLP_Buttons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.TLP_Buttons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.TLP_Buttons.Controls.Add(this.B_Save, 0, 0);
			this.TLP_Buttons.Controls.Add(this.B_Cancel, 1, 0);
			this.TLP_Buttons.Dock = System.Windows.Forms.DockStyle.Fill;
			this.TLP_Buttons.Location = new System.Drawing.Point(525, 1);
			this.TLP_Buttons.Margin = new System.Windows.Forms.Padding(0);
			this.TLP_Buttons.Name = "TLP_Buttons";
			this.TLP_Buttons.RowCount = 1;
			this.TLP_Buttons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.TLP_Buttons.Size = new System.Drawing.Size(259, 31);
			this.TLP_Buttons.TabIndex = 0;
			// 
			// B_Save
			// 
			this.B_Save.Dock = System.Windows.Forms.DockStyle.Fill;
			this.B_Save.Location = new System.Drawing.Point(4, 3);
			this.B_Save.Margin = new System.Windows.Forms.Padding(4, 3, 2, 3);
			this.B_Save.Name = "B_Save";
			this.B_Save.Size = new System.Drawing.Size(123, 25);
			this.B_Save.TabIndex = 0;
			this.B_Save.Text = "Zapisz";
			this.B_Save.UseVisualStyleBackColor = true;
			this.B_Save.Click += new System.EventHandler(this.B_Save_Click);
			// 
			// B_Cancel
			// 
			this.B_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.B_Cancel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.B_Cancel.Location = new System.Drawing.Point(131, 3);
			this.B_Cancel.Margin = new System.Windows.Forms.Padding(2, 3, 6, 3);
			this.B_Cancel.Name = "B_Cancel";
			this.B_Cancel.Size = new System.Drawing.Size(122, 25);
			this.B_Cancel.TabIndex = 1;
			this.B_Cancel.Text = "Zaniechaj";
			this.B_Cancel.UseVisualStyleBackColor = true;
			this.B_Cancel.Click += new System.EventHandler(this.B_Cancel_Click);
			// 
			// EditRowsForm
			// 
			this.AcceptButton = this.B_Save;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.B_Cancel;
			this.ClientSize = new System.Drawing.Size(784, 522);
			this.Controls.Add(this.TLP_Main);
			this.KeyPreview = true;
			this.MinimumSize = new System.Drawing.Size(800, 560);
			this.Name = "EditRowsForm";
			this.Text = "EditDataForm";
			this.TLP_Main.ResumeLayout(false);
			this.SC_Main.Panel1.ResumeLayout(false);
			this.SC_Main.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.SC_Main)).EndInit();
			this.SC_Main.ResumeLayout(false);
			this.TLP_DataGrid.ResumeLayout(false);
			this.P_Grid.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.DGV_Data)).EndInit();
			this.TLP_DataControls.ResumeLayout(false);
			this.TLP_DataControls.PerformLayout();
			this.SC_Sidebar.Panel1.ResumeLayout(false);
			this.SC_Sidebar.Panel2.ResumeLayout(false);
			this.SC_Sidebar.Panel2.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.SC_Sidebar)).EndInit();
			this.SC_Sidebar.ResumeLayout(false);
			this.TLP_ColumnView.ResumeLayout(false);
			this.TLP_Controls.ResumeLayout(false);
			this.TLP_Controls.PerformLayout();
			this.GB_Type.ResumeLayout(false);
			this.GB_SearchAndReplace.ResumeLayout(false);
			this.GB_SearchAndReplace.PerformLayout();
			this.TLP_SearchAndReplace.ResumeLayout(false);
			this.TLP_SearchAndReplace.PerformLayout();
			this.TLP_StatusBar.ResumeLayout(false);
			this.TLP_Buttons.ResumeLayout(false);
			this.ResumeLayout(false);
		}

#endregion

		private System.Windows.Forms.TableLayoutPanel TLP_Main;
		private System.Windows.Forms.SplitContainer SC_Main;
		private System.Windows.Forms.TableLayoutPanel TLP_DataGrid;
		private System.Windows.Forms.Panel P_Grid;
		private System.Windows.Forms.DataGridView DGV_Data;
		private System.Windows.Forms.DataGridViewTextBoxColumn DGVTBC_Col1;
		private System.Windows.Forms.DataGridViewTextBoxColumn DGVTBC_Col2;
		private System.Windows.Forms.DataGridViewTextBoxColumn DGVTBC_Col3;
		private System.Windows.Forms.DataGridViewTextBoxColumn DGVTBC_Col4;
		private System.Windows.Forms.TableLayoutPanel TLP_DataControls;
		private System.Windows.Forms.Button B_RemoveRow;
		private System.Windows.Forms.Button B_InsertRow;
		private System.Windows.Forms.Button B_LastPage;
		private System.Windows.Forms.Button B_NextPage;
		private System.Windows.Forms.Button P_PrevPage;
		private System.Windows.Forms.Label L_PageStat;
		private System.Windows.Forms.TextBox TB_PageNum;
		private System.Windows.Forms.TextBox TB_RowsPerPage;
		private System.Windows.Forms.Label L_RowsPerPage;
		private System.Windows.Forms.Button B_FirstPage;
		private System.Windows.Forms.TableLayoutPanel TLP_StatusBar;
		private System.Windows.Forms.SplitContainer SC_Sidebar;
		private System.Windows.Forms.ListView LV_Columns;
		private System.Windows.Forms.ColumnHeader CH_Column;
		private System.Windows.Forms.TableLayoutPanel TLP_Controls;
		private System.Windows.Forms.GroupBox GB_Type;
		private System.Windows.Forms.GroupBox GB_SearchAndReplace;
		private System.Windows.Forms.TableLayoutPanel TLP_SearchAndReplace;
		private System.Windows.Forms.TextBox TB_Search;
		private System.Windows.Forms.TextBox TB_Replace;
		private System.Windows.Forms.ComboBox CBX_ColumnType;
		private System.Windows.Forms.ComboBox B_SearchRange;
		private System.Windows.Forms.Button B_Search;
		private System.Windows.Forms.Button B_ReplaceAll;
		private System.Windows.Forms.Button B_Count;
		private System.Windows.Forms.Button B_Replace;
		private System.Windows.Forms.CheckBox CB_CaseSensitive;
		private System.Windows.Forms.CheckBox CB_Exact;
		private System.Windows.Forms.CheckBox CB_UseRegex;
		private System.Windows.Forms.TableLayoutPanel TLP_ColumnView;
		private System.Windows.Forms.TableLayoutPanel TLP_Buttons;
		private System.Windows.Forms.Button B_Save;
		private System.Windows.Forms.Button B_Cancel;

	/// @endcond
	}
}