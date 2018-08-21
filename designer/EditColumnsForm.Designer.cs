namespace CDesigner.Forms
{
	partial class EditColumnsForm
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.ListViewGroup listViewGroup1 = new System.Windows.Forms.ListViewGroup("STARE KOLUMNY", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup2 = new System.Windows.Forms.ListViewGroup("NOWE KOLUMNY", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem("Set Main");
            System.Windows.Forms.ListViewItem listViewItem2 = new System.Windows.Forms.ListViewItem("Main Prev");
            System.Windows.Forms.ListViewItem listViewItem3 = new System.Windows.Forms.ListViewItem("Main");
            System.Windows.Forms.ListViewItem listViewItem4 = new System.Windows.Forms.ListViewItem("      Pod 1");
            System.Windows.Forms.ListViewItem listViewItem5 = new System.Windows.Forms.ListViewItem("      Pod 2");
            this.TP_Tooltip = new System.Windows.Forms.ToolTip(this.components);
            this.LV_PreviewRows = new System.Windows.Forms.ListView();
            this.CH_DataPreview = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.TLP_StatusBar = new System.Windows.Forms.TableLayoutPanel();
            this.CBX_Step = new System.Windows.Forms.ComboBox();
            this.TLP_Buttons = new System.Windows.Forms.TableLayoutPanel();
            this.B_Cancel = new System.Windows.Forms.Button();
            this.B_Save = new System.Windows.Forms.Button();
            this.LV_DatabaseColumns = new System.Windows.Forms.ListView();
            this.CH_Columns = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.LV_NewColumns = new System.Windows.Forms.ListView();
            this.CH_ColumnName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.CH_JoinedColumns = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.TLP_ColumnManagement = new System.Windows.Forms.TableLayoutPanel();
            this.B_AddColumn = new System.Windows.Forms.Button();
            this.B_ClearColumn = new System.Windows.Forms.Button();
            this.B_DeleteColumn = new System.Windows.Forms.Button();
            this.TB_ColumnName = new System.Windows.Forms.TextBox();
            this.SC_Main = new System.Windows.Forms.SplitContainer();
            this.SC_FiltersAndTypes = new System.Windows.Forms.SplitContainer();
            this.LV_PreviewAllRows = new System.Windows.Forms.ListView();
            this.CB_RecordPreview = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.TLP_FilterGroups = new System.Windows.Forms.TableLayoutPanel();
            this.GB_FilterConfig = new System.Windows.Forms.GroupBox();
            this.TLP_FilterConfig = new System.Windows.Forms.TableLayoutPanel();
            this.CB_AllCopies = new System.Windows.Forms.CheckBox();
            this.L_Result = new System.Windows.Forms.Label();
            this.L_Modifier = new System.Windows.Forms.Label();
            this.L_FilterType = new System.Windows.Forms.Label();
            this.CB_FilterType = new System.Windows.Forms.ComboBox();
            this.TB_Modifier = new System.Windows.Forms.TextBox();
            this.TB_Result = new System.Windows.Forms.TextBox();
            this.CB_Exclude = new System.Windows.Forms.CheckBox();
            this.LV_FilterList = new System.Windows.Forms.ListView();
            this.CH_FilterList = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.GB_ColumnType = new System.Windows.Forms.GroupBox();
            this.TLP_ColumnType = new System.Windows.Forms.TableLayoutPanel();
            this.CBX_Saved = new System.Windows.Forms.ComboBox();
            this.CBX_ColType = new System.Windows.Forms.ComboBox();
            this.B_ChangeColType = new System.Windows.Forms.Button();
            this.B_AdvancedSets = new System.Windows.Forms.Button();
            this.TLP_FilterControls = new System.Windows.Forms.TableLayoutPanel();
            this.B_DeleteFilter = new System.Windows.Forms.Button();
            this.B_ChangeFilter = new System.Windows.Forms.Button();
            this.B_AddFilter = new System.Windows.Forms.Button();
            this.TLP_CreateColumns = new System.Windows.Forms.TableLayoutPanel();
            this.LV_AllColumns = new System.Windows.Forms.ListView();
            this.CH_AllColumns = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.TLP_Main = new System.Windows.Forms.TableLayoutPanel();
            this.TLP_StatusBar.SuspendLayout();
            this.TLP_Buttons.SuspendLayout();
            this.TLP_ColumnManagement.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SC_Main)).BeginInit();
            this.SC_Main.Panel1.SuspendLayout();
            this.SC_Main.Panel2.SuspendLayout();
            this.SC_Main.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SC_FiltersAndTypes)).BeginInit();
            this.SC_FiltersAndTypes.Panel1.SuspendLayout();
            this.SC_FiltersAndTypes.Panel2.SuspendLayout();
            this.SC_FiltersAndTypes.SuspendLayout();
            this.TLP_FilterGroups.SuspendLayout();
            this.GB_FilterConfig.SuspendLayout();
            this.TLP_FilterConfig.SuspendLayout();
            this.GB_ColumnType.SuspendLayout();
            this.TLP_ColumnType.SuspendLayout();
            this.TLP_FilterControls.SuspendLayout();
            this.TLP_CreateColumns.SuspendLayout();
            this.TLP_Main.SuspendLayout();
            this.SuspendLayout();
            // 
            // TP_Tooltip
            // 
            this.TP_Tooltip.OwnerDraw = true;
            this.TP_Tooltip.Draw += new System.Windows.Forms.DrawToolTipEventHandler(this.TP_Tooltip_Draw);
            // 
            // LV_PreviewRows
            // 
            this.LV_PreviewRows.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.CH_DataPreview});
            this.LV_PreviewRows.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LV_PreviewRows.FullRowSelect = true;
            this.LV_PreviewRows.GridLines = true;
            this.LV_PreviewRows.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.LV_PreviewRows.HideSelection = false;
            this.LV_PreviewRows.Location = new System.Drawing.Point(0, 226);
            this.LV_PreviewRows.Margin = new System.Windows.Forms.Padding(0, 2, 0, 0);
            this.LV_PreviewRows.MultiSelect = false;
            this.LV_PreviewRows.Name = "LV_PreviewRows";
            this.LV_PreviewRows.ShowGroups = false;
            this.LV_PreviewRows.Size = new System.Drawing.Size(479, 192);
            this.LV_PreviewRows.TabIndex = 7;
            this.LV_PreviewRows.UseCompatibleStateImageBehavior = false;
            this.LV_PreviewRows.View = System.Windows.Forms.View.Details;
            // 
            // CH_DataPreview
            // 
            this.CH_DataPreview.Text = "Podgląd wierszy";
            this.CH_DataPreview.Width = 475;
            // 
            // TLP_StatusBar
            // 
            this.TLP_StatusBar.BackColor = System.Drawing.SystemColors.ControlLight;
            this.TLP_StatusBar.ColumnCount = 3;
            this.TLP_Main.SetColumnSpan(this.TLP_StatusBar, 2);
            this.TLP_StatusBar.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.TLP_StatusBar.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.TLP_StatusBar.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 220F));
            this.TLP_StatusBar.Controls.Add(this.CBX_Step, 0, 0);
            this.TLP_StatusBar.Controls.Add(this.TLP_Buttons, 2, 0);
            this.TLP_StatusBar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TLP_StatusBar.Location = new System.Drawing.Point(0, 430);
            this.TLP_StatusBar.Margin = new System.Windows.Forms.Padding(0);
            this.TLP_StatusBar.Name = "TLP_StatusBar";
            this.TLP_StatusBar.Padding = new System.Windows.Forms.Padding(0, 1, 0, 0);
            this.TLP_StatusBar.RowCount = 1;
            this.TLP_StatusBar.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.TLP_StatusBar.Size = new System.Drawing.Size(704, 32);
            this.TLP_StatusBar.TabIndex = 11;
            this.TLP_StatusBar.Paint += new System.Windows.Forms.PaintEventHandler(this.TLP_StatusBar_Paint);
            // 
            // CBX_Step
            // 
            this.CBX_Step.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CBX_Step.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CBX_Step.FormattingEnabled = true;
            this.CBX_Step.Items.AddRange(new object[] {
            "1. Tworzenie kolumn",
            "2. Filtrowanie i typy"});
            this.CBX_Step.Location = new System.Drawing.Point(6, 6);
            this.CBX_Step.Margin = new System.Windows.Forms.Padding(6, 5, 2, 3);
            this.CBX_Step.Name = "CBX_Step";
            this.CBX_Step.Size = new System.Drawing.Size(142, 21);
            this.CBX_Step.TabIndex = 3;
            this.CBX_Step.SelectedIndexChanged += new System.EventHandler(this.CBX_Step_SelectedIndexChanged);
            // 
            // TLP_Buttons
            // 
            this.TLP_Buttons.ColumnCount = 2;
            this.TLP_Buttons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.TLP_Buttons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.TLP_Buttons.Controls.Add(this.B_Cancel, 0, 0);
            this.TLP_Buttons.Controls.Add(this.B_Save, 0, 0);
            this.TLP_Buttons.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TLP_Buttons.Location = new System.Drawing.Point(484, 1);
            this.TLP_Buttons.Margin = new System.Windows.Forms.Padding(0);
            this.TLP_Buttons.Name = "TLP_Buttons";
            this.TLP_Buttons.RowCount = 1;
            this.TLP_Buttons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.TLP_Buttons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 31F));
            this.TLP_Buttons.Size = new System.Drawing.Size(220, 31);
            this.TLP_Buttons.TabIndex = 4;
            // 
            // B_Cancel
            // 
            this.B_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.B_Cancel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.B_Cancel.Location = new System.Drawing.Point(112, 3);
            this.B_Cancel.Margin = new System.Windows.Forms.Padding(2, 3, 6, 3);
            this.B_Cancel.Name = "B_Cancel";
            this.B_Cancel.Size = new System.Drawing.Size(102, 25);
            this.B_Cancel.TabIndex = 2;
            this.B_Cancel.Text = "Zaniechaj";
            this.B_Cancel.UseVisualStyleBackColor = true;
            this.B_Cancel.Click += new System.EventHandler(this.B_Cancel_Click);
            // 
            // B_Save
            // 
            this.B_Save.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.B_Save.Dock = System.Windows.Forms.DockStyle.Fill;
            this.B_Save.Location = new System.Drawing.Point(4, 3);
            this.B_Save.Margin = new System.Windows.Forms.Padding(4, 3, 2, 3);
            this.B_Save.Name = "B_Save";
            this.B_Save.Size = new System.Drawing.Size(104, 25);
            this.B_Save.TabIndex = 1;
            this.B_Save.Text = "Zapisz";
            this.B_Save.UseVisualStyleBackColor = true;
            this.B_Save.Click += new System.EventHandler(this.B_Save_Click);
            // 
            // LV_DatabaseColumns
            // 
            this.LV_DatabaseColumns.AllowDrop = true;
            this.LV_DatabaseColumns.CheckBoxes = true;
            this.LV_DatabaseColumns.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.CH_Columns});
            this.LV_DatabaseColumns.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LV_DatabaseColumns.FullRowSelect = true;
            this.LV_DatabaseColumns.GridLines = true;
            this.LV_DatabaseColumns.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.LV_DatabaseColumns.HideSelection = false;
            this.LV_DatabaseColumns.Location = new System.Drawing.Point(0, 0);
            this.LV_DatabaseColumns.Margin = new System.Windows.Forms.Padding(0);
            this.LV_DatabaseColumns.MultiSelect = false;
            this.LV_DatabaseColumns.Name = "LV_DatabaseColumns";
            this.LV_DatabaseColumns.ShowGroups = false;
            this.LV_DatabaseColumns.Size = new System.Drawing.Size(209, 418);
            this.LV_DatabaseColumns.TabIndex = 8;
            this.LV_DatabaseColumns.UseCompatibleStateImageBehavior = false;
            this.LV_DatabaseColumns.View = System.Windows.Forms.View.Details;
            this.LV_DatabaseColumns.SelectedIndexChanged += new System.EventHandler(this.LV_DatabaseColumns_SelectedIndexChanged);
            this.LV_DatabaseColumns.DragOver += new System.Windows.Forms.DragEventHandler(this.LV_DragDropEffects_Move);
            this.LV_DatabaseColumns.MouseDown += new System.Windows.Forms.MouseEventHandler(this.LV_DatabaseColumns_MouseDown);
            // 
            // CH_Columns
            // 
            this.CH_Columns.Text = "Dostępne kolumny";
            this.CH_Columns.Width = 205;
            // 
            // LV_NewColumns
            // 
            this.LV_NewColumns.AllowDrop = true;
            this.LV_NewColumns.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.CH_ColumnName,
            this.CH_JoinedColumns});
            this.LV_NewColumns.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LV_NewColumns.FullRowSelect = true;
            this.LV_NewColumns.GridLines = true;
            this.LV_NewColumns.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.LV_NewColumns.HideSelection = false;
            this.LV_NewColumns.Location = new System.Drawing.Point(0, 0);
            this.LV_NewColumns.Margin = new System.Windows.Forms.Padding(0, 0, 0, 2);
            this.LV_NewColumns.MultiSelect = false;
            this.LV_NewColumns.Name = "LV_NewColumns";
            this.LV_NewColumns.ShowGroups = false;
            this.LV_NewColumns.Size = new System.Drawing.Size(479, 191);
            this.LV_NewColumns.TabIndex = 2;
            this.LV_NewColumns.UseCompatibleStateImageBehavior = false;
            this.LV_NewColumns.View = System.Windows.Forms.View.Details;
            this.LV_NewColumns.SelectedIndexChanged += new System.EventHandler(this.LV_NewColumns_SelectedIndexChanged);
            this.LV_NewColumns.DragDrop += new System.Windows.Forms.DragEventHandler(this.LV_NewColumns_DragDrop);
            this.LV_NewColumns.DragEnter += new System.Windows.Forms.DragEventHandler(this.LV_DragDropEffects_Move);
            this.LV_NewColumns.DragOver += new System.Windows.Forms.DragEventHandler(this.LV_NewColumns_DragOver);
            // 
            // CH_ColumnName
            // 
            this.CH_ColumnName.Text = "Nazwa";
            this.CH_ColumnName.Width = 116;
            // 
            // CH_JoinedColumns
            // 
            this.CH_JoinedColumns.Text = "Kolumny";
            this.CH_JoinedColumns.Width = 359;
            // 
            // TLP_ColumnManagement
            // 
            this.TLP_ColumnManagement.BackColor = System.Drawing.SystemColors.Control;
            this.TLP_ColumnManagement.ColumnCount = 4;
            this.TLP_ColumnManagement.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.TLP_ColumnManagement.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.TLP_ColumnManagement.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.TLP_ColumnManagement.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.TLP_ColumnManagement.Controls.Add(this.B_AddColumn, 1, 0);
            this.TLP_ColumnManagement.Controls.Add(this.B_ClearColumn, 2, 0);
            this.TLP_ColumnManagement.Controls.Add(this.B_DeleteColumn, 3, 0);
            this.TLP_ColumnManagement.Controls.Add(this.TB_ColumnName, 0, 0);
            this.TLP_ColumnManagement.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TLP_ColumnManagement.Location = new System.Drawing.Point(0, 193);
            this.TLP_ColumnManagement.Margin = new System.Windows.Forms.Padding(0);
            this.TLP_ColumnManagement.Name = "TLP_ColumnManagement";
            this.TLP_ColumnManagement.RowCount = 1;
            this.TLP_ColumnManagement.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.TLP_ColumnManagement.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 31F));
            this.TLP_ColumnManagement.Size = new System.Drawing.Size(479, 31);
            this.TLP_ColumnManagement.TabIndex = 18;
            // 
            // B_AddColumn
            // 
            this.B_AddColumn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.B_AddColumn.Location = new System.Drawing.Point(239, 3);
            this.B_AddColumn.Margin = new System.Windows.Forms.Padding(0, 3, 2, 3);
            this.B_AddColumn.Name = "B_AddColumn";
            this.B_AddColumn.Size = new System.Drawing.Size(78, 25);
            this.B_AddColumn.TabIndex = 4;
            this.B_AddColumn.Text = "Dodaj";
            this.B_AddColumn.UseVisualStyleBackColor = true;
            this.B_AddColumn.Click += new System.EventHandler(this.B_AddColumn_Click);
            // 
            // B_ClearColumn
            // 
            this.B_ClearColumn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.B_ClearColumn.Enabled = false;
            this.B_ClearColumn.Location = new System.Drawing.Point(321, 3);
            this.B_ClearColumn.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.B_ClearColumn.Name = "B_ClearColumn";
            this.B_ClearColumn.Size = new System.Drawing.Size(76, 25);
            this.B_ClearColumn.TabIndex = 5;
            this.B_ClearColumn.Text = "Wyczyść";
            this.B_ClearColumn.UseVisualStyleBackColor = true;
            this.B_ClearColumn.Click += new System.EventHandler(this.B_ClearColumn_Click);
            // 
            // B_DeleteColumn
            // 
            this.B_DeleteColumn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.B_DeleteColumn.Enabled = false;
            this.B_DeleteColumn.Location = new System.Drawing.Point(401, 3);
            this.B_DeleteColumn.Margin = new System.Windows.Forms.Padding(2, 3, 0, 3);
            this.B_DeleteColumn.Name = "B_DeleteColumn";
            this.B_DeleteColumn.Size = new System.Drawing.Size(78, 25);
            this.B_DeleteColumn.TabIndex = 6;
            this.B_DeleteColumn.Text = "Usuń";
            this.B_DeleteColumn.UseVisualStyleBackColor = true;
            this.B_DeleteColumn.Click += new System.EventHandler(this.B_DeleteColumn_Click);
            // 
            // TB_ColumnName
            // 
            this.TB_ColumnName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TB_ColumnName.Location = new System.Drawing.Point(0, 6);
            this.TB_ColumnName.Margin = new System.Windows.Forms.Padding(0, 6, 4, 3);
            this.TB_ColumnName.MaxLength = 127;
            this.TB_ColumnName.Name = "TB_ColumnName";
            this.TB_ColumnName.Size = new System.Drawing.Size(235, 20);
            this.TB_ColumnName.TabIndex = 3;
            this.TB_ColumnName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TB_ColumnName_KeyPress);
            this.TB_ColumnName.Leave += new System.EventHandler(this.TP_Tooltip_Hide);
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
            this.SC_Main.Panel1.Controls.Add(this.SC_FiltersAndTypes);
            this.SC_Main.Panel1.Controls.Add(this.TLP_CreateColumns);
            this.SC_Main.Panel1MinSize = 400;
            // 
            // SC_Main.Panel2
            // 
            this.SC_Main.Panel2.Controls.Add(this.LV_AllColumns);
            this.SC_Main.Panel2.Controls.Add(this.LV_DatabaseColumns);
            this.SC_Main.Panel2MinSize = 200;
            this.SC_Main.Size = new System.Drawing.Size(692, 418);
            this.SC_Main.SplitterDistance = 479;
            this.SC_Main.TabIndex = 3;
            // 
            // SC_FiltersAndTypes
            // 
            this.SC_FiltersAndTypes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SC_FiltersAndTypes.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.SC_FiltersAndTypes.Location = new System.Drawing.Point(0, 0);
            this.SC_FiltersAndTypes.Margin = new System.Windows.Forms.Padding(0);
            this.SC_FiltersAndTypes.Name = "SC_FiltersAndTypes";
            // 
            // SC_FiltersAndTypes.Panel1
            // 
            this.SC_FiltersAndTypes.Panel1.Controls.Add(this.LV_PreviewAllRows);
            // 
            // SC_FiltersAndTypes.Panel2
            // 
            this.SC_FiltersAndTypes.Panel2.Controls.Add(this.TLP_FilterGroups);
            this.SC_FiltersAndTypes.Size = new System.Drawing.Size(479, 418);
            this.SC_FiltersAndTypes.SplitterDistance = 230;
            this.SC_FiltersAndTypes.TabIndex = 5;
            this.SC_FiltersAndTypes.Visible = false;
            // 
            // LV_PreviewAllRows
            // 
            this.LV_PreviewAllRows.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.CB_RecordPreview});
            this.LV_PreviewAllRows.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LV_PreviewAllRows.FullRowSelect = true;
            this.LV_PreviewAllRows.GridLines = true;
            this.LV_PreviewAllRows.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.LV_PreviewAllRows.HideSelection = false;
            this.LV_PreviewAllRows.Location = new System.Drawing.Point(0, 0);
            this.LV_PreviewAllRows.Margin = new System.Windows.Forms.Padding(0, 2, 0, 0);
            this.LV_PreviewAllRows.MultiSelect = false;
            this.LV_PreviewAllRows.Name = "LV_PreviewAllRows";
            this.LV_PreviewAllRows.ShowGroups = false;
            this.LV_PreviewAllRows.Size = new System.Drawing.Size(230, 418);
            this.LV_PreviewAllRows.TabIndex = 8;
            this.LV_PreviewAllRows.UseCompatibleStateImageBehavior = false;
            this.LV_PreviewAllRows.View = System.Windows.Forms.View.Details;
            // 
            // CB_RecordPreview
            // 
            this.CB_RecordPreview.Text = "Podgląd wierszy";
            this.CB_RecordPreview.Width = 226;
            // 
            // TLP_FilterGroups
            // 
            this.TLP_FilterGroups.ColumnCount = 1;
            this.TLP_FilterGroups.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.TLP_FilterGroups.Controls.Add(this.GB_FilterConfig, 0, 1);
            this.TLP_FilterGroups.Controls.Add(this.LV_FilterList, 0, 3);
            this.TLP_FilterGroups.Controls.Add(this.GB_ColumnType, 0, 0);
            this.TLP_FilterGroups.Controls.Add(this.TLP_FilterControls, 0, 2);
            this.TLP_FilterGroups.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TLP_FilterGroups.Location = new System.Drawing.Point(0, 0);
            this.TLP_FilterGroups.Name = "TLP_FilterGroups";
            this.TLP_FilterGroups.RowCount = 4;
            this.TLP_FilterGroups.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 0F));
            this.TLP_FilterGroups.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 153F));
            this.TLP_FilterGroups.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 29F));
            this.TLP_FilterGroups.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.TLP_FilterGroups.Size = new System.Drawing.Size(245, 418);
            this.TLP_FilterGroups.TabIndex = 0;
            // 
            // GB_FilterConfig
            // 
            this.GB_FilterConfig.Controls.Add(this.TLP_FilterConfig);
            this.GB_FilterConfig.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GB_FilterConfig.Location = new System.Drawing.Point(0, 2);
            this.GB_FilterConfig.Margin = new System.Windows.Forms.Padding(0, 2, 0, 2);
            this.GB_FilterConfig.Name = "GB_FilterConfig";
            this.GB_FilterConfig.Size = new System.Drawing.Size(245, 149);
            this.GB_FilterConfig.TabIndex = 11;
            this.GB_FilterConfig.TabStop = false;
            this.GB_FilterConfig.Text = "Ustawienia filtrowania";
            // 
            // TLP_FilterConfig
            // 
            this.TLP_FilterConfig.ColumnCount = 2;
            this.TLP_FilterConfig.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 36.40167F));
            this.TLP_FilterConfig.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 63.59833F));
            this.TLP_FilterConfig.Controls.Add(this.CB_AllCopies, 0, 4);
            this.TLP_FilterConfig.Controls.Add(this.L_Result, 0, 2);
            this.TLP_FilterConfig.Controls.Add(this.L_Modifier, 0, 1);
            this.TLP_FilterConfig.Controls.Add(this.L_FilterType, 0, 0);
            this.TLP_FilterConfig.Controls.Add(this.CB_FilterType, 1, 0);
            this.TLP_FilterConfig.Controls.Add(this.TB_Modifier, 1, 1);
            this.TLP_FilterConfig.Controls.Add(this.TB_Result, 1, 2);
            this.TLP_FilterConfig.Controls.Add(this.CB_Exclude, 0, 3);
            this.TLP_FilterConfig.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TLP_FilterConfig.Location = new System.Drawing.Point(3, 16);
            this.TLP_FilterConfig.Name = "TLP_FilterConfig";
            this.TLP_FilterConfig.RowCount = 5;
            this.TLP_FilterConfig.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.TLP_FilterConfig.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.TLP_FilterConfig.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.TLP_FilterConfig.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.TLP_FilterConfig.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.TLP_FilterConfig.Size = new System.Drawing.Size(239, 130);
            this.TLP_FilterConfig.TabIndex = 0;
            // 
            // CB_AllCopies
            // 
            this.CB_AllCopies.AutoSize = true;
            this.TLP_FilterConfig.SetColumnSpan(this.CB_AllCopies, 2);
            this.CB_AllCopies.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CB_AllCopies.Enabled = false;
            this.CB_AllCopies.Location = new System.Drawing.Point(6, 107);
            this.CB_AllCopies.Margin = new System.Windows.Forms.Padding(6, 3, 4, 3);
            this.CB_AllCopies.Name = "CB_AllCopies";
            this.CB_AllCopies.Size = new System.Drawing.Size(229, 20);
            this.CB_AllCopies.TabIndex = 7;
            this.CB_AllCopies.Text = "Zastosuj filtr dla wszystkich kopii kolumny";
            this.CB_AllCopies.UseVisualStyleBackColor = true;
            // 
            // L_Result
            // 
            this.L_Result.AutoSize = true;
            this.L_Result.Dock = System.Windows.Forms.DockStyle.Fill;
            this.L_Result.Location = new System.Drawing.Point(3, 52);
            this.L_Result.Name = "L_Result";
            this.L_Result.Size = new System.Drawing.Size(80, 26);
            this.L_Result.TabIndex = 3;
            this.L_Result.Text = "Wynik:";
            this.L_Result.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // L_Modifier
            // 
            this.L_Modifier.AutoSize = true;
            this.L_Modifier.Dock = System.Windows.Forms.DockStyle.Fill;
            this.L_Modifier.Location = new System.Drawing.Point(3, 26);
            this.L_Modifier.Name = "L_Modifier";
            this.L_Modifier.Size = new System.Drawing.Size(80, 26);
            this.L_Modifier.TabIndex = 1;
            this.L_Modifier.Text = "Modyfikator:";
            this.L_Modifier.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // L_FilterType
            // 
            this.L_FilterType.AutoSize = true;
            this.L_FilterType.Dock = System.Windows.Forms.DockStyle.Fill;
            this.L_FilterType.Location = new System.Drawing.Point(3, 0);
            this.L_FilterType.Name = "L_FilterType";
            this.L_FilterType.Size = new System.Drawing.Size(80, 26);
            this.L_FilterType.TabIndex = 0;
            this.L_FilterType.Text = "Typ filtra:";
            this.L_FilterType.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // CB_FilterType
            // 
            this.CB_FilterType.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CB_FilterType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CB_FilterType.Enabled = false;
            this.CB_FilterType.FormattingEnabled = true;
            this.CB_FilterType.Location = new System.Drawing.Point(88, 3);
            this.CB_FilterType.Margin = new System.Windows.Forms.Padding(2, 3, 4, 3);
            this.CB_FilterType.Name = "CB_FilterType";
            this.CB_FilterType.Size = new System.Drawing.Size(147, 21);
            this.CB_FilterType.TabIndex = 4;
            this.CB_FilterType.SelectedIndexChanged += new System.EventHandler(this.CB_FilterType_SelectedIndexChanged);
            // 
            // TB_Modifier
            // 
            this.TB_Modifier.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TB_Modifier.Enabled = false;
            this.TB_Modifier.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.TB_Modifier.Location = new System.Drawing.Point(88, 28);
            this.TB_Modifier.Margin = new System.Windows.Forms.Padding(2, 2, 4, 3);
            this.TB_Modifier.Name = "TB_Modifier";
            this.TB_Modifier.Size = new System.Drawing.Size(147, 22);
            this.TB_Modifier.TabIndex = 5;
            // 
            // TB_Result
            // 
            this.TB_Result.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TB_Result.Enabled = false;
            this.TB_Result.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.TB_Result.Location = new System.Drawing.Point(88, 54);
            this.TB_Result.Margin = new System.Windows.Forms.Padding(2, 2, 4, 3);
            this.TB_Result.Name = "TB_Result";
            this.TB_Result.Size = new System.Drawing.Size(147, 22);
            this.TB_Result.TabIndex = 0;
            // 
            // CB_Exclude
            // 
            this.CB_Exclude.AutoSize = true;
            this.TLP_FilterConfig.SetColumnSpan(this.CB_Exclude, 2);
            this.CB_Exclude.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CB_Exclude.Enabled = false;
            this.CB_Exclude.Location = new System.Drawing.Point(6, 81);
            this.CB_Exclude.Margin = new System.Windows.Forms.Padding(6, 3, 4, 3);
            this.CB_Exclude.Name = "CB_Exclude";
            this.CB_Exclude.Size = new System.Drawing.Size(229, 20);
            this.CB_Exclude.TabIndex = 6;
            this.CB_Exclude.Text = "Wyklucz znalezione wiersze ze zbioru";
            this.CB_Exclude.UseVisualStyleBackColor = true;
            this.CB_Exclude.CheckedChanged += new System.EventHandler(this.CB_Exclude_CheckedChanged);
            // 
            // LV_FilterList
            // 
            this.LV_FilterList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.CH_FilterList});
            this.LV_FilterList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LV_FilterList.FullRowSelect = true;
            this.LV_FilterList.GridLines = true;
            this.LV_FilterList.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.LV_FilterList.HideSelection = false;
            this.LV_FilterList.Location = new System.Drawing.Point(0, 184);
            this.LV_FilterList.Margin = new System.Windows.Forms.Padding(0, 2, 0, 0);
            this.LV_FilterList.MultiSelect = false;
            this.LV_FilterList.Name = "LV_FilterList";
            this.LV_FilterList.ShowGroups = false;
            this.LV_FilterList.Size = new System.Drawing.Size(245, 234);
            this.LV_FilterList.TabIndex = 9;
            this.LV_FilterList.UseCompatibleStateImageBehavior = false;
            this.LV_FilterList.View = System.Windows.Forms.View.Details;
            this.LV_FilterList.SelectedIndexChanged += new System.EventHandler(this.LV_FilterList_SelectedIndexChanged);
            // 
            // CH_FilterList
            // 
            this.CH_FilterList.Text = "Lista filtrów dla kolumny";
            this.CH_FilterList.Width = 241;
            // 
            // GB_ColumnType
            // 
            this.GB_ColumnType.Controls.Add(this.TLP_ColumnType);
            this.GB_ColumnType.Enabled = false;
            this.GB_ColumnType.Location = new System.Drawing.Point(0, 0);
            this.GB_ColumnType.Margin = new System.Windows.Forms.Padding(0, 0, 0, 2);
            this.GB_ColumnType.Name = "GB_ColumnType";
            this.GB_ColumnType.Size = new System.Drawing.Size(245, 1);
            this.GB_ColumnType.TabIndex = 10;
            this.GB_ColumnType.TabStop = false;
            this.GB_ColumnType.Text = "Typ kolumny";
            this.GB_ColumnType.Visible = false;
            // 
            // TLP_ColumnType
            // 
            this.TLP_ColumnType.ColumnCount = 2;
            this.TLP_ColumnType.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.TLP_ColumnType.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.TLP_ColumnType.Controls.Add(this.CBX_Saved, 1, 0);
            this.TLP_ColumnType.Controls.Add(this.CBX_ColType, 0, 0);
            this.TLP_ColumnType.Controls.Add(this.B_ChangeColType, 0, 1);
            this.TLP_ColumnType.Controls.Add(this.B_AdvancedSets, 1, 1);
            this.TLP_ColumnType.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TLP_ColumnType.Location = new System.Drawing.Point(3, 16);
            this.TLP_ColumnType.Margin = new System.Windows.Forms.Padding(0);
            this.TLP_ColumnType.Name = "TLP_ColumnType";
            this.TLP_ColumnType.RowCount = 2;
            this.TLP_ColumnType.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.TLP_ColumnType.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.TLP_ColumnType.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.TLP_ColumnType.Size = new System.Drawing.Size(239, 0);
            this.TLP_ColumnType.TabIndex = 0;
            // 
            // CBX_Saved
            // 
            this.CBX_Saved.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CBX_Saved.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CBX_Saved.Enabled = false;
            this.CBX_Saved.FormattingEnabled = true;
            this.CBX_Saved.Items.AddRange(new object[] {
            "Tekst",
            "Liczba",
            "Data",
            "Waluta",
            "Znak"});
            this.CBX_Saved.Location = new System.Drawing.Point(121, 2);
            this.CBX_Saved.Margin = new System.Windows.Forms.Padding(2, 2, 4, 0);
            this.CBX_Saved.Name = "CBX_Saved";
            this.CBX_Saved.Size = new System.Drawing.Size(114, 21);
            this.CBX_Saved.TabIndex = 5;
            // 
            // CBX_ColType
            // 
            this.CBX_ColType.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CBX_ColType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CBX_ColType.Enabled = false;
            this.CBX_ColType.FormattingEnabled = true;
            this.CBX_ColType.Items.AddRange(new object[] {
            "Tekst",
            "Liczba",
            "Data",
            "Waluta",
            "Znak"});
            this.CBX_ColType.Location = new System.Drawing.Point(4, 2);
            this.CBX_ColType.Margin = new System.Windows.Forms.Padding(4, 2, 2, 0);
            this.CBX_ColType.Name = "CBX_ColType";
            this.CBX_ColType.Size = new System.Drawing.Size(113, 21);
            this.CBX_ColType.TabIndex = 0;
            // 
            // B_ChangeColType
            // 
            this.B_ChangeColType.Dock = System.Windows.Forms.DockStyle.Fill;
            this.B_ChangeColType.Enabled = false;
            this.B_ChangeColType.Location = new System.Drawing.Point(3, 27);
            this.B_ChangeColType.Margin = new System.Windows.Forms.Padding(3, 2, 1, 3);
            this.B_ChangeColType.Name = "B_ChangeColType";
            this.B_ChangeColType.Size = new System.Drawing.Size(115, 1);
            this.B_ChangeColType.TabIndex = 2;
            this.B_ChangeColType.Text = "Zmień typ kolumny";
            this.B_ChangeColType.UseVisualStyleBackColor = true;
            // 
            // B_AdvancedSets
            // 
            this.B_AdvancedSets.Dock = System.Windows.Forms.DockStyle.Fill;
            this.B_AdvancedSets.Enabled = false;
            this.B_AdvancedSets.Location = new System.Drawing.Point(120, 27);
            this.B_AdvancedSets.Margin = new System.Windows.Forms.Padding(1, 2, 3, 3);
            this.B_AdvancedSets.Name = "B_AdvancedSets";
            this.B_AdvancedSets.Size = new System.Drawing.Size(116, 1);
            this.B_AdvancedSets.TabIndex = 4;
            this.B_AdvancedSets.Text = "Zaawansowane";
            this.B_AdvancedSets.UseVisualStyleBackColor = true;
            // 
            // TLP_FilterControls
            // 
            this.TLP_FilterControls.ColumnCount = 3;
            this.TLP_FilterControls.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.TLP_FilterControls.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.TLP_FilterControls.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.TLP_FilterControls.Controls.Add(this.B_DeleteFilter, 2, 0);
            this.TLP_FilterControls.Controls.Add(this.B_ChangeFilter, 1, 0);
            this.TLP_FilterControls.Controls.Add(this.B_AddFilter, 0, 0);
            this.TLP_FilterControls.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TLP_FilterControls.Location = new System.Drawing.Point(0, 155);
            this.TLP_FilterControls.Margin = new System.Windows.Forms.Padding(0, 2, 0, 2);
            this.TLP_FilterControls.Name = "TLP_FilterControls";
            this.TLP_FilterControls.RowCount = 1;
            this.TLP_FilterControls.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.TLP_FilterControls.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.TLP_FilterControls.Size = new System.Drawing.Size(245, 25);
            this.TLP_FilterControls.TabIndex = 12;
            // 
            // B_DeleteFilter
            // 
            this.B_DeleteFilter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.B_DeleteFilter.Enabled = false;
            this.B_DeleteFilter.Location = new System.Drawing.Point(164, 0);
            this.B_DeleteFilter.Margin = new System.Windows.Forms.Padding(2, 0, 0, 0);
            this.B_DeleteFilter.Name = "B_DeleteFilter";
            this.B_DeleteFilter.Size = new System.Drawing.Size(81, 25);
            this.B_DeleteFilter.TabIndex = 0;
            this.B_DeleteFilter.Text = "Usuń";
            this.B_DeleteFilter.UseVisualStyleBackColor = true;
            this.B_DeleteFilter.Click += new System.EventHandler(this.B_DeleteFilter_Click);
            // 
            // B_ChangeFilter
            // 
            this.B_ChangeFilter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.B_ChangeFilter.Enabled = false;
            this.B_ChangeFilter.Location = new System.Drawing.Point(83, 0);
            this.B_ChangeFilter.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.B_ChangeFilter.Name = "B_ChangeFilter";
            this.B_ChangeFilter.Size = new System.Drawing.Size(77, 25);
            this.B_ChangeFilter.TabIndex = 1;
            this.B_ChangeFilter.Text = "Zmień";
            this.B_ChangeFilter.UseVisualStyleBackColor = true;
            this.B_ChangeFilter.Click += new System.EventHandler(this.B_ChangeFilter_Click);
            // 
            // B_AddFilter
            // 
            this.B_AddFilter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.B_AddFilter.Enabled = false;
            this.B_AddFilter.Location = new System.Drawing.Point(0, 0);
            this.B_AddFilter.Margin = new System.Windows.Forms.Padding(0, 0, 2, 0);
            this.B_AddFilter.Name = "B_AddFilter";
            this.B_AddFilter.Size = new System.Drawing.Size(79, 25);
            this.B_AddFilter.TabIndex = 2;
            this.B_AddFilter.Text = "Dodaj";
            this.B_AddFilter.UseVisualStyleBackColor = true;
            this.B_AddFilter.Click += new System.EventHandler(this.B_AddFilter_Click);
            // 
            // TLP_CreateColumns
            // 
            this.TLP_CreateColumns.ColumnCount = 1;
            this.TLP_CreateColumns.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.TLP_CreateColumns.Controls.Add(this.LV_NewColumns, 0, 0);
            this.TLP_CreateColumns.Controls.Add(this.LV_PreviewRows, 0, 2);
            this.TLP_CreateColumns.Controls.Add(this.TLP_ColumnManagement, 0, 1);
            this.TLP_CreateColumns.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TLP_CreateColumns.Location = new System.Drawing.Point(0, 0);
            this.TLP_CreateColumns.Margin = new System.Windows.Forms.Padding(0);
            this.TLP_CreateColumns.Name = "TLP_CreateColumns";
            this.TLP_CreateColumns.RowCount = 3;
            this.TLP_CreateColumns.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.TLP_CreateColumns.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 31F));
            this.TLP_CreateColumns.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.TLP_CreateColumns.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.TLP_CreateColumns.Size = new System.Drawing.Size(479, 418);
            this.TLP_CreateColumns.TabIndex = 0;
            // 
            // LV_AllColumns
            // 
            this.LV_AllColumns.AllowDrop = true;
            this.LV_AllColumns.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.CH_AllColumns});
            this.LV_AllColumns.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LV_AllColumns.FullRowSelect = true;
            this.LV_AllColumns.GridLines = true;
            listViewGroup1.Header = "STARE KOLUMNY";
            listViewGroup1.Name = "lvgOldCols";
            listViewGroup2.Header = "NOWE KOLUMNY";
            listViewGroup2.Name = "lvgNewCols";
            this.LV_AllColumns.Groups.AddRange(new System.Windows.Forms.ListViewGroup[] {
            listViewGroup1,
            listViewGroup2});
            this.LV_AllColumns.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.LV_AllColumns.HideSelection = false;
            listViewItem1.Group = listViewGroup1;
            listViewItem1.StateImageIndex = 0;
            listViewItem2.Group = listViewGroup1;
            listViewItem2.StateImageIndex = 0;
            listViewItem3.Group = listViewGroup2;
            listViewItem3.StateImageIndex = 0;
            listViewItem4.Group = listViewGroup2;
            listViewItem4.StateImageIndex = 0;
            listViewItem5.Group = listViewGroup2;
            listViewItem5.StateImageIndex = 0;
            this.LV_AllColumns.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1,
            listViewItem2,
            listViewItem3,
            listViewItem4,
            listViewItem5});
            this.LV_AllColumns.Location = new System.Drawing.Point(0, 0);
            this.LV_AllColumns.Margin = new System.Windows.Forms.Padding(0);
            this.LV_AllColumns.MultiSelect = false;
            this.LV_AllColumns.Name = "LV_AllColumns";
            this.LV_AllColumns.Size = new System.Drawing.Size(209, 418);
            this.LV_AllColumns.TabIndex = 9;
            this.LV_AllColumns.UseCompatibleStateImageBehavior = false;
            this.LV_AllColumns.View = System.Windows.Forms.View.Details;
            this.LV_AllColumns.Visible = false;
            this.LV_AllColumns.SelectedIndexChanged += new System.EventHandler(this.LV_AllColumns_SelectedIndexChanged);
            // 
            // CH_AllColumns
            // 
            this.CH_AllColumns.Text = "Wszystkie kolumny";
            this.CH_AllColumns.Width = 207;
            // 
            // TLP_Main
            // 
            this.TLP_Main.ColumnCount = 1;
            this.TLP_Main.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.TLP_Main.Controls.Add(this.TLP_StatusBar, 0, 1);
            this.TLP_Main.Controls.Add(this.SC_Main, 0, 0);
            this.TLP_Main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TLP_Main.Location = new System.Drawing.Point(0, 0);
            this.TLP_Main.Margin = new System.Windows.Forms.Padding(0);
            this.TLP_Main.Name = "TLP_Main";
            this.TLP_Main.RowCount = 2;
            this.TLP_Main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.TLP_Main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.TLP_Main.Size = new System.Drawing.Size(704, 462);
            this.TLP_Main.TabIndex = 0;
            // 
            // EditColumnsForm
            // 
            this.AcceptButton = this.B_Save;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.B_Cancel;
            this.ClientSize = new System.Drawing.Size(704, 462);
            this.Controls.Add(this.TLP_Main);
            this.MaximumSize = new System.Drawing.Size(65535, 65535);
            this.MinimumSize = new System.Drawing.Size(700, 480);
            this.Name = "EditColumnsForm";
            this.Text = "Zarządzanie kolumnami";
            this.Deactivate += new System.EventHandler(this.TP_Tooltip_Hide);
            this.Move += new System.EventHandler(this.TP_Tooltip_Hide);
            this.TLP_StatusBar.ResumeLayout(false);
            this.TLP_Buttons.ResumeLayout(false);
            this.TLP_ColumnManagement.ResumeLayout(false);
            this.TLP_ColumnManagement.PerformLayout();
            this.SC_Main.Panel1.ResumeLayout(false);
            this.SC_Main.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.SC_Main)).EndInit();
            this.SC_Main.ResumeLayout(false);
            this.SC_FiltersAndTypes.Panel1.ResumeLayout(false);
            this.SC_FiltersAndTypes.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.SC_FiltersAndTypes)).EndInit();
            this.SC_FiltersAndTypes.ResumeLayout(false);
            this.TLP_FilterGroups.ResumeLayout(false);
            this.GB_FilterConfig.ResumeLayout(false);
            this.TLP_FilterConfig.ResumeLayout(false);
            this.TLP_FilterConfig.PerformLayout();
            this.GB_ColumnType.ResumeLayout(false);
            this.TLP_ColumnType.ResumeLayout(false);
            this.TLP_FilterControls.ResumeLayout(false);
            this.TLP_CreateColumns.ResumeLayout(false);
            this.TLP_Main.ResumeLayout(false);
            this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.ToolTip TP_Tooltip;
		private System.Windows.Forms.ListView LV_PreviewRows;
		private System.Windows.Forms.ColumnHeader CH_DataPreview;
		private System.Windows.Forms.TableLayoutPanel TLP_StatusBar;
		private System.Windows.Forms.TableLayoutPanel TLP_ColumnManagement;
		private System.Windows.Forms.Button B_AddColumn;
		private System.Windows.Forms.Button B_ClearColumn;
		private System.Windows.Forms.Button B_DeleteColumn;
		private System.Windows.Forms.TextBox TB_ColumnName;
		private System.Windows.Forms.ListView LV_NewColumns;
		private System.Windows.Forms.ColumnHeader CH_ColumnName;
		private System.Windows.Forms.ColumnHeader CH_JoinedColumns;
		private System.Windows.Forms.ListView LV_DatabaseColumns;
		private System.Windows.Forms.ColumnHeader CH_Columns;
		private System.Windows.Forms.Button B_Save;
		private System.Windows.Forms.TableLayoutPanel TLP_Main;
		private System.Windows.Forms.SplitContainer SC_Main;
		private System.Windows.Forms.TableLayoutPanel TLP_CreateColumns;
		private System.Windows.Forms.Button B_Cancel;
		private System.Windows.Forms.ComboBox CBX_Step;
		private System.Windows.Forms.TableLayoutPanel TLP_Buttons;
		private System.Windows.Forms.ListView LV_AllColumns;
		private System.Windows.Forms.ColumnHeader CH_AllColumns;
		private System.Windows.Forms.SplitContainer SC_FiltersAndTypes;
		private System.Windows.Forms.ListView LV_PreviewAllRows;
		private System.Windows.Forms.ColumnHeader CB_RecordPreview;
		private System.Windows.Forms.TableLayoutPanel TLP_FilterGroups;
		private System.Windows.Forms.ListView LV_FilterList;
        private System.Windows.Forms.ColumnHeader CH_FilterList;
		private System.Windows.Forms.GroupBox GB_FilterConfig;
		private System.Windows.Forms.TableLayoutPanel TLP_FilterControls;
		private System.Windows.Forms.Button B_DeleteFilter;
		private System.Windows.Forms.Button B_ChangeFilter;
        private System.Windows.Forms.Button B_AddFilter;
		private System.Windows.Forms.TableLayoutPanel TLP_FilterConfig;
		private System.Windows.Forms.Label L_Modifier;
		private System.Windows.Forms.Label L_FilterType;
		private System.Windows.Forms.Label L_Result;
		private System.Windows.Forms.ComboBox CB_FilterType;
		private System.Windows.Forms.TextBox TB_Modifier;
		private System.Windows.Forms.TextBox TB_Result;
		private System.Windows.Forms.CheckBox CB_Exclude;
        private System.Windows.Forms.CheckBox CB_AllCopies;
        private System.Windows.Forms.GroupBox GB_ColumnType;
        private System.Windows.Forms.TableLayoutPanel TLP_ColumnType;
        private System.Windows.Forms.ComboBox CBX_Saved;
        private System.Windows.Forms.ComboBox CBX_ColType;
        private System.Windows.Forms.Button B_ChangeColType;
        private System.Windows.Forms.Button B_AdvancedSets;

		/// @endcond
	}
}