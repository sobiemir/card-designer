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
            this.tpTooltip = new System.Windows.Forms.ToolTip(this.components);
            this.lvPreviewRows = new System.Windows.Forms.ListView();
            this.lvcDataPreview = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tlStatusBar = new System.Windows.Forms.TableLayoutPanel();
            this.cbStep = new System.Windows.Forms.ComboBox();
            this.tbResultButtons = new System.Windows.Forms.TableLayoutPanel();
            this.bCancel = new System.Windows.Forms.Button();
            this.bSave = new System.Windows.Forms.Button();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.lvDatabaseColumns = new System.Windows.Forms.ListView();
            this.lvcColumns = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvNewColumns = new System.Windows.Forms.ListView();
            this.lvcColumnName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvcJoinedColumns = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tlColumnManagement = new System.Windows.Forms.TableLayoutPanel();
            this.bAddColumn = new System.Windows.Forms.Button();
            this.bClearColumn = new System.Windows.Forms.Button();
            this.bDeleteColumn = new System.Windows.Forms.Button();
            this.tbColumnName = new System.Windows.Forms.TextBox();
            this.scMain = new System.Windows.Forms.SplitContainer();
            this.scFiltersAndTypes = new System.Windows.Forms.SplitContainer();
            this.lvPreviewAllRows = new System.Windows.Forms.ListView();
            this.lvcRecordPreview = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tlFilterGroups = new System.Windows.Forms.TableLayoutPanel();
            this.gbFilterConfig = new System.Windows.Forms.GroupBox();
            this.tbFilterConfig = new System.Windows.Forms.TableLayoutPanel();
            this.cbAllCopies = new System.Windows.Forms.CheckBox();
            this.lResult = new System.Windows.Forms.Label();
            this.lModifier = new System.Windows.Forms.Label();
            this.lFilterType = new System.Windows.Forms.Label();
            this.cbFilterType = new System.Windows.Forms.ComboBox();
            this.tbModifier = new System.Windows.Forms.TextBox();
            this.tbResult = new System.Windows.Forms.TextBox();
            this.cbExclude = new System.Windows.Forms.CheckBox();
            this.lvFilterList = new System.Windows.Forms.ListView();
            this.lvcFilterList = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.gbColumnType = new System.Windows.Forms.GroupBox();
            this.tbColumnType = new System.Windows.Forms.TableLayoutPanel();
            this.cbSaved = new System.Windows.Forms.ComboBox();
            this.cbColType = new System.Windows.Forms.ComboBox();
            this.bChangeColType = new System.Windows.Forms.Button();
            this.bTypeSettings = new System.Windows.Forms.Button();
            this.tbFilterControls = new System.Windows.Forms.TableLayoutPanel();
            this.bDeleteFilter = new System.Windows.Forms.Button();
            this.bChangeFilter = new System.Windows.Forms.Button();
            this.bAddFilter = new System.Windows.Forms.Button();
            this.tlCreateColumns = new System.Windows.Forms.TableLayoutPanel();
            this.lvAllColumns = new System.Windows.Forms.ListView();
            this.lvcAllColumns = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tlMain = new System.Windows.Forms.TableLayoutPanel();
            this.tlStatusBar.SuspendLayout();
            this.tbResultButtons.SuspendLayout();
            this.tlColumnManagement.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.scMain)).BeginInit();
            this.scMain.Panel1.SuspendLayout();
            this.scMain.Panel2.SuspendLayout();
            this.scMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.scFiltersAndTypes)).BeginInit();
            this.scFiltersAndTypes.Panel1.SuspendLayout();
            this.scFiltersAndTypes.Panel2.SuspendLayout();
            this.scFiltersAndTypes.SuspendLayout();
            this.tlFilterGroups.SuspendLayout();
            this.gbFilterConfig.SuspendLayout();
            this.tbFilterConfig.SuspendLayout();
            this.gbColumnType.SuspendLayout();
            this.tbColumnType.SuspendLayout();
            this.tbFilterControls.SuspendLayout();
            this.tlCreateColumns.SuspendLayout();
            this.tlMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // tpTooltip
            // 
            this.tpTooltip.OwnerDraw = true;
            this.tpTooltip.Draw += new System.Windows.Forms.DrawToolTipEventHandler(this.tpTooltip_Draw);
            // 
            // lvPreviewRows
            // 
            this.lvPreviewRows.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.lvcDataPreview});
            this.lvPreviewRows.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvPreviewRows.FullRowSelect = true;
            this.lvPreviewRows.GridLines = true;
            this.lvPreviewRows.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvPreviewRows.HideSelection = false;
            this.lvPreviewRows.Location = new System.Drawing.Point(0, 226);
            this.lvPreviewRows.Margin = new System.Windows.Forms.Padding(0, 2, 0, 0);
            this.lvPreviewRows.MultiSelect = false;
            this.lvPreviewRows.Name = "lvPreviewRows";
            this.lvPreviewRows.ShowGroups = false;
            this.lvPreviewRows.Size = new System.Drawing.Size(479, 192);
            this.lvPreviewRows.TabIndex = 7;
            this.lvPreviewRows.UseCompatibleStateImageBehavior = false;
            this.lvPreviewRows.View = System.Windows.Forms.View.Details;
            // 
            // lvcDataPreview
            // 
            this.lvcDataPreview.Text = "Podgląd wierszy";
            this.lvcDataPreview.Width = 475;
            // 
            // tlStatusBar
            // 
            this.tlStatusBar.BackColor = System.Drawing.SystemColors.ControlLight;
            this.tlStatusBar.ColumnCount = 3;
            this.tlMain.SetColumnSpan(this.tlStatusBar, 2);
            this.tlStatusBar.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.tlStatusBar.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlStatusBar.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 220F));
            this.tlStatusBar.Controls.Add(this.cbStep, 0, 0);
            this.tlStatusBar.Controls.Add(this.tbResultButtons, 2, 0);
            this.tlStatusBar.Controls.Add(this.checkBox1, 1, 0);
            this.tlStatusBar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlStatusBar.Location = new System.Drawing.Point(0, 430);
            this.tlStatusBar.Margin = new System.Windows.Forms.Padding(0);
            this.tlStatusBar.Name = "tlStatusBar";
            this.tlStatusBar.Padding = new System.Windows.Forms.Padding(0, 1, 0, 0);
            this.tlStatusBar.RowCount = 1;
            this.tlStatusBar.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlStatusBar.Size = new System.Drawing.Size(704, 32);
            this.tlStatusBar.TabIndex = 11;
            this.tlStatusBar.Paint += new System.Windows.Forms.PaintEventHandler(this.tlStatusBar_Paint);
            // 
            // cbStep
            // 
            this.cbStep.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cbStep.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbStep.FormattingEnabled = true;
            this.cbStep.Items.AddRange(new object[] {
            "1. Tworzenie kolumn",
            "2. Filtrowanie i typy"});
            this.cbStep.Location = new System.Drawing.Point(6, 6);
            this.cbStep.Margin = new System.Windows.Forms.Padding(6, 5, 2, 3);
            this.cbStep.Name = "cbStep";
            this.cbStep.Size = new System.Drawing.Size(142, 21);
            this.cbStep.TabIndex = 3;
            this.cbStep.SelectedIndexChanged += new System.EventHandler(this.cbStep_SelectedIndexChanged);
            // 
            // tbResultButtons
            // 
            this.tbResultButtons.ColumnCount = 2;
            this.tbResultButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tbResultButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tbResultButtons.Controls.Add(this.bCancel, 0, 0);
            this.tbResultButtons.Controls.Add(this.bSave, 0, 0);
            this.tbResultButtons.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbResultButtons.Location = new System.Drawing.Point(484, 1);
            this.tbResultButtons.Margin = new System.Windows.Forms.Padding(0);
            this.tbResultButtons.Name = "tbResultButtons";
            this.tbResultButtons.RowCount = 1;
            this.tbResultButtons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tbResultButtons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 31F));
            this.tbResultButtons.Size = new System.Drawing.Size(220, 31);
            this.tbResultButtons.TabIndex = 4;
            // 
            // bCancel
            // 
            this.bCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.bCancel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.bCancel.Location = new System.Drawing.Point(112, 3);
            this.bCancel.Margin = new System.Windows.Forms.Padding(2, 3, 6, 3);
            this.bCancel.Name = "bCancel";
            this.bCancel.Size = new System.Drawing.Size(102, 25);
            this.bCancel.TabIndex = 2;
            this.bCancel.Text = "Zaniechaj";
            this.bCancel.UseVisualStyleBackColor = true;
            this.bCancel.Click += new System.EventHandler(this.bCancel_Click);
            // 
            // bSave
            // 
            this.bSave.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.bSave.Dock = System.Windows.Forms.DockStyle.Fill;
            this.bSave.Location = new System.Drawing.Point(4, 3);
            this.bSave.Margin = new System.Windows.Forms.Padding(4, 3, 2, 3);
            this.bSave.Name = "bSave";
            this.bSave.Size = new System.Drawing.Size(104, 25);
            this.bSave.TabIndex = 1;
            this.bSave.Text = "Zapisz";
            this.bSave.UseVisualStyleBackColor = true;
            this.bSave.Click += new System.EventHandler(this.bSave_Click);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.checkBox1.Location = new System.Drawing.Point(156, 4);
            this.checkBox1.Margin = new System.Windows.Forms.Padding(6, 3, 3, 3);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(325, 25);
            this.checkBox1.TabIndex = 5;
            this.checkBox1.Text = "Nadpisz dane z zastosowaniem utworzonych filtrów";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.Visible = false;
            // 
            // lvDatabaseColumns
            // 
            this.lvDatabaseColumns.AllowDrop = true;
            this.lvDatabaseColumns.CheckBoxes = true;
            this.lvDatabaseColumns.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.lvcColumns});
            this.lvDatabaseColumns.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvDatabaseColumns.FullRowSelect = true;
            this.lvDatabaseColumns.GridLines = true;
            this.lvDatabaseColumns.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvDatabaseColumns.HideSelection = false;
            this.lvDatabaseColumns.Location = new System.Drawing.Point(0, 0);
            this.lvDatabaseColumns.Margin = new System.Windows.Forms.Padding(0);
            this.lvDatabaseColumns.MultiSelect = false;
            this.lvDatabaseColumns.Name = "lvDatabaseColumns";
            this.lvDatabaseColumns.ShowGroups = false;
            this.lvDatabaseColumns.Size = new System.Drawing.Size(209, 418);
            this.lvDatabaseColumns.TabIndex = 8;
            this.lvDatabaseColumns.UseCompatibleStateImageBehavior = false;
            this.lvDatabaseColumns.View = System.Windows.Forms.View.Details;
            this.lvDatabaseColumns.SelectedIndexChanged += new System.EventHandler(this.lvDatabaseColumns_SelectedIndexChanged);
            this.lvDatabaseColumns.DragOver += new System.Windows.Forms.DragEventHandler(this.dragDropEffects_Move);
            this.lvDatabaseColumns.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lvDatabaseColumns_MouseDown);
            // 
            // lvcColumns
            // 
            this.lvcColumns.Text = "Dostępne kolumny";
            this.lvcColumns.Width = 205;
            // 
            // lvNewColumns
            // 
            this.lvNewColumns.AllowDrop = true;
            this.lvNewColumns.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.lvcColumnName,
            this.lvcJoinedColumns});
            this.lvNewColumns.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvNewColumns.FullRowSelect = true;
            this.lvNewColumns.GridLines = true;
            this.lvNewColumns.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvNewColumns.HideSelection = false;
            this.lvNewColumns.Location = new System.Drawing.Point(0, 0);
            this.lvNewColumns.Margin = new System.Windows.Forms.Padding(0, 0, 0, 2);
            this.lvNewColumns.MultiSelect = false;
            this.lvNewColumns.Name = "lvNewColumns";
            this.lvNewColumns.ShowGroups = false;
            this.lvNewColumns.Size = new System.Drawing.Size(479, 191);
            this.lvNewColumns.TabIndex = 2;
            this.lvNewColumns.UseCompatibleStateImageBehavior = false;
            this.lvNewColumns.View = System.Windows.Forms.View.Details;
            this.lvNewColumns.SelectedIndexChanged += new System.EventHandler(this.lvNewColumns_SelectedIndexChanged);
            this.lvNewColumns.DragDrop += new System.Windows.Forms.DragEventHandler(this.lvNewColumns_DragDrop);
            this.lvNewColumns.DragEnter += new System.Windows.Forms.DragEventHandler(this.dragDropEffects_Move);
            this.lvNewColumns.DragOver += new System.Windows.Forms.DragEventHandler(this.lvNewColumns_DragOver);
            // 
            // lvcColumnName
            // 
            this.lvcColumnName.Text = "Nazwa";
            this.lvcColumnName.Width = 116;
            // 
            // lvcJoinedColumns
            // 
            this.lvcJoinedColumns.Text = "Kolumny";
            this.lvcJoinedColumns.Width = 359;
            // 
            // tlColumnManagement
            // 
            this.tlColumnManagement.BackColor = System.Drawing.SystemColors.Control;
            this.tlColumnManagement.ColumnCount = 4;
            this.tlColumnManagement.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlColumnManagement.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tlColumnManagement.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tlColumnManagement.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tlColumnManagement.Controls.Add(this.bAddColumn, 1, 0);
            this.tlColumnManagement.Controls.Add(this.bClearColumn, 2, 0);
            this.tlColumnManagement.Controls.Add(this.bDeleteColumn, 3, 0);
            this.tlColumnManagement.Controls.Add(this.tbColumnName, 0, 0);
            this.tlColumnManagement.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlColumnManagement.Location = new System.Drawing.Point(0, 193);
            this.tlColumnManagement.Margin = new System.Windows.Forms.Padding(0);
            this.tlColumnManagement.Name = "tlColumnManagement";
            this.tlColumnManagement.RowCount = 1;
            this.tlColumnManagement.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlColumnManagement.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 31F));
            this.tlColumnManagement.Size = new System.Drawing.Size(479, 31);
            this.tlColumnManagement.TabIndex = 18;
            // 
            // bAddColumn
            // 
            this.bAddColumn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.bAddColumn.Location = new System.Drawing.Point(239, 3);
            this.bAddColumn.Margin = new System.Windows.Forms.Padding(0, 3, 2, 3);
            this.bAddColumn.Name = "bAddColumn";
            this.bAddColumn.Size = new System.Drawing.Size(78, 25);
            this.bAddColumn.TabIndex = 4;
            this.bAddColumn.Text = "Dodaj";
            this.bAddColumn.UseVisualStyleBackColor = true;
            this.bAddColumn.Click += new System.EventHandler(this.bAddColumn_Click);
            // 
            // bClearColumn
            // 
            this.bClearColumn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.bClearColumn.Enabled = false;
            this.bClearColumn.Location = new System.Drawing.Point(321, 3);
            this.bClearColumn.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.bClearColumn.Name = "bClearColumn";
            this.bClearColumn.Size = new System.Drawing.Size(76, 25);
            this.bClearColumn.TabIndex = 5;
            this.bClearColumn.Text = "Wyczyść";
            this.bClearColumn.UseVisualStyleBackColor = true;
            this.bClearColumn.Click += new System.EventHandler(this.bClearColumn_Click);
            // 
            // bDeleteColumn
            // 
            this.bDeleteColumn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.bDeleteColumn.Enabled = false;
            this.bDeleteColumn.Location = new System.Drawing.Point(401, 3);
            this.bDeleteColumn.Margin = new System.Windows.Forms.Padding(2, 3, 0, 3);
            this.bDeleteColumn.Name = "bDeleteColumn";
            this.bDeleteColumn.Size = new System.Drawing.Size(78, 25);
            this.bDeleteColumn.TabIndex = 6;
            this.bDeleteColumn.Text = "Usuń";
            this.bDeleteColumn.UseVisualStyleBackColor = true;
            this.bDeleteColumn.Click += new System.EventHandler(this.bDeleteColumn_Click);
            // 
            // tbColumnName
            // 
            this.tbColumnName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbColumnName.Location = new System.Drawing.Point(0, 6);
            this.tbColumnName.Margin = new System.Windows.Forms.Padding(0, 6, 4, 3);
            this.tbColumnName.MaxLength = 127;
            this.tbColumnName.Name = "tbColumnName";
            this.tbColumnName.Size = new System.Drawing.Size(235, 20);
            this.tbColumnName.TabIndex = 3;
            this.tbColumnName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbColumnName_KeyPress);
            this.tbColumnName.Leave += new System.EventHandler(this.toolTip_Hide);
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
            this.scMain.Panel1.Controls.Add(this.scFiltersAndTypes);
            this.scMain.Panel1.Controls.Add(this.tlCreateColumns);
            this.scMain.Panel1MinSize = 400;
            // 
            // scMain.Panel2
            // 
            this.scMain.Panel2.Controls.Add(this.lvAllColumns);
            this.scMain.Panel2.Controls.Add(this.lvDatabaseColumns);
            this.scMain.Panel2MinSize = 200;
            this.scMain.Size = new System.Drawing.Size(692, 418);
            this.scMain.SplitterDistance = 479;
            this.scMain.TabIndex = 3;
            // 
            // scFiltersAndTypes
            // 
            this.scFiltersAndTypes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scFiltersAndTypes.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.scFiltersAndTypes.Location = new System.Drawing.Point(0, 0);
            this.scFiltersAndTypes.Margin = new System.Windows.Forms.Padding(0);
            this.scFiltersAndTypes.Name = "scFiltersAndTypes";
            // 
            // scFiltersAndTypes.Panel1
            // 
            this.scFiltersAndTypes.Panel1.Controls.Add(this.lvPreviewAllRows);
            // 
            // scFiltersAndTypes.Panel2
            // 
            this.scFiltersAndTypes.Panel2.Controls.Add(this.tlFilterGroups);
            this.scFiltersAndTypes.Size = new System.Drawing.Size(479, 418);
            this.scFiltersAndTypes.SplitterDistance = 230;
            this.scFiltersAndTypes.TabIndex = 5;
            this.scFiltersAndTypes.Visible = false;
            // 
            // lvPreviewAllRows
            // 
            this.lvPreviewAllRows.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.lvcRecordPreview});
            this.lvPreviewAllRows.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvPreviewAllRows.FullRowSelect = true;
            this.lvPreviewAllRows.GridLines = true;
            this.lvPreviewAllRows.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvPreviewAllRows.HideSelection = false;
            this.lvPreviewAllRows.Location = new System.Drawing.Point(0, 0);
            this.lvPreviewAllRows.Margin = new System.Windows.Forms.Padding(0, 2, 0, 0);
            this.lvPreviewAllRows.MultiSelect = false;
            this.lvPreviewAllRows.Name = "lvPreviewAllRows";
            this.lvPreviewAllRows.ShowGroups = false;
            this.lvPreviewAllRows.Size = new System.Drawing.Size(230, 418);
            this.lvPreviewAllRows.TabIndex = 8;
            this.lvPreviewAllRows.UseCompatibleStateImageBehavior = false;
            this.lvPreviewAllRows.View = System.Windows.Forms.View.Details;
            // 
            // lvcRecordPreview
            // 
            this.lvcRecordPreview.Text = "Podgląd wierszy";
            this.lvcRecordPreview.Width = 226;
            // 
            // tlFilterGroups
            // 
            this.tlFilterGroups.ColumnCount = 1;
            this.tlFilterGroups.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlFilterGroups.Controls.Add(this.gbFilterConfig, 0, 1);
            this.tlFilterGroups.Controls.Add(this.lvFilterList, 0, 3);
            this.tlFilterGroups.Controls.Add(this.gbColumnType, 0, 0);
            this.tlFilterGroups.Controls.Add(this.tbFilterControls, 0, 2);
            this.tlFilterGroups.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlFilterGroups.Location = new System.Drawing.Point(0, 0);
            this.tlFilterGroups.Name = "tlFilterGroups";
            this.tlFilterGroups.RowCount = 4;
            this.tlFilterGroups.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 76F));
            this.tlFilterGroups.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 153F));
            this.tlFilterGroups.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 29F));
            this.tlFilterGroups.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlFilterGroups.Size = new System.Drawing.Size(245, 418);
            this.tlFilterGroups.TabIndex = 0;
            // 
            // gbFilterConfig
            // 
            this.gbFilterConfig.Controls.Add(this.tbFilterConfig);
            this.gbFilterConfig.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbFilterConfig.Location = new System.Drawing.Point(0, 78);
            this.gbFilterConfig.Margin = new System.Windows.Forms.Padding(0, 2, 0, 2);
            this.gbFilterConfig.Name = "gbFilterConfig";
            this.gbFilterConfig.Size = new System.Drawing.Size(245, 149);
            this.gbFilterConfig.TabIndex = 11;
            this.gbFilterConfig.TabStop = false;
            this.gbFilterConfig.Text = "Ustawienia filtrowania";
            // 
            // tbFilterConfig
            // 
            this.tbFilterConfig.ColumnCount = 2;
            this.tbFilterConfig.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 36.40167F));
            this.tbFilterConfig.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 63.59833F));
            this.tbFilterConfig.Controls.Add(this.cbAllCopies, 0, 4);
            this.tbFilterConfig.Controls.Add(this.lResult, 0, 2);
            this.tbFilterConfig.Controls.Add(this.lModifier, 0, 1);
            this.tbFilterConfig.Controls.Add(this.lFilterType, 0, 0);
            this.tbFilterConfig.Controls.Add(this.cbFilterType, 1, 0);
            this.tbFilterConfig.Controls.Add(this.tbModifier, 1, 1);
            this.tbFilterConfig.Controls.Add(this.tbResult, 1, 2);
            this.tbFilterConfig.Controls.Add(this.cbExclude, 0, 3);
            this.tbFilterConfig.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbFilterConfig.Location = new System.Drawing.Point(3, 16);
            this.tbFilterConfig.Name = "tbFilterConfig";
            this.tbFilterConfig.RowCount = 5;
            this.tbFilterConfig.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.tbFilterConfig.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.tbFilterConfig.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.tbFilterConfig.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.tbFilterConfig.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tbFilterConfig.Size = new System.Drawing.Size(239, 130);
            this.tbFilterConfig.TabIndex = 0;
            // 
            // cbAllCopies
            // 
            this.cbAllCopies.AutoSize = true;
            this.tbFilterConfig.SetColumnSpan(this.cbAllCopies, 2);
            this.cbAllCopies.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cbAllCopies.Enabled = false;
            this.cbAllCopies.Location = new System.Drawing.Point(6, 107);
            this.cbAllCopies.Margin = new System.Windows.Forms.Padding(6, 3, 4, 3);
            this.cbAllCopies.Name = "cbAllCopies";
            this.cbAllCopies.Size = new System.Drawing.Size(229, 20);
            this.cbAllCopies.TabIndex = 7;
            this.cbAllCopies.Text = "Zastosuj filtr dla wszystkich kopii kolumny";
            this.cbAllCopies.UseVisualStyleBackColor = true;
            // 
            // lResult
            // 
            this.lResult.AutoSize = true;
            this.lResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lResult.Location = new System.Drawing.Point(3, 52);
            this.lResult.Name = "lResult";
            this.lResult.Size = new System.Drawing.Size(80, 26);
            this.lResult.TabIndex = 3;
            this.lResult.Text = "Wynik:";
            this.lResult.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lModifier
            // 
            this.lModifier.AutoSize = true;
            this.lModifier.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lModifier.Location = new System.Drawing.Point(3, 26);
            this.lModifier.Name = "lModifier";
            this.lModifier.Size = new System.Drawing.Size(80, 26);
            this.lModifier.TabIndex = 1;
            this.lModifier.Text = "Modyfikator:";
            this.lModifier.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lFilterType
            // 
            this.lFilterType.AutoSize = true;
            this.lFilterType.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lFilterType.Location = new System.Drawing.Point(3, 0);
            this.lFilterType.Name = "lFilterType";
            this.lFilterType.Size = new System.Drawing.Size(80, 26);
            this.lFilterType.TabIndex = 0;
            this.lFilterType.Text = "Typ filtra:";
            this.lFilterType.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cbFilterType
            // 
            this.cbFilterType.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cbFilterType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbFilterType.Enabled = false;
            this.cbFilterType.FormattingEnabled = true;
            this.cbFilterType.Location = new System.Drawing.Point(88, 3);
            this.cbFilterType.Margin = new System.Windows.Forms.Padding(2, 3, 4, 3);
            this.cbFilterType.Name = "cbFilterType";
            this.cbFilterType.Size = new System.Drawing.Size(147, 21);
            this.cbFilterType.TabIndex = 4;
            this.cbFilterType.SelectedIndexChanged += new System.EventHandler(this.cbFilterType_SelectedIndexChanged);
            // 
            // tbModifier
            // 
            this.tbModifier.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbModifier.Enabled = false;
            this.tbModifier.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.tbModifier.Location = new System.Drawing.Point(88, 28);
            this.tbModifier.Margin = new System.Windows.Forms.Padding(2, 2, 4, 3);
            this.tbModifier.Name = "tbModifier";
            this.tbModifier.Size = new System.Drawing.Size(147, 22);
            this.tbModifier.TabIndex = 5;
            // 
            // tbResult
            // 
            this.tbResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbResult.Enabled = false;
            this.tbResult.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.tbResult.Location = new System.Drawing.Point(88, 54);
            this.tbResult.Margin = new System.Windows.Forms.Padding(2, 2, 4, 3);
            this.tbResult.Name = "tbResult";
            this.tbResult.Size = new System.Drawing.Size(147, 22);
            this.tbResult.TabIndex = 0;
            // 
            // cbExclude
            // 
            this.cbExclude.AutoSize = true;
            this.tbFilterConfig.SetColumnSpan(this.cbExclude, 2);
            this.cbExclude.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cbExclude.Enabled = false;
            this.cbExclude.Location = new System.Drawing.Point(6, 81);
            this.cbExclude.Margin = new System.Windows.Forms.Padding(6, 3, 4, 3);
            this.cbExclude.Name = "cbExclude";
            this.cbExclude.Size = new System.Drawing.Size(229, 20);
            this.cbExclude.TabIndex = 6;
            this.cbExclude.Text = "Wyklucz znalezione wiersze ze zbioru";
            this.cbExclude.UseVisualStyleBackColor = true;
            this.cbExclude.CheckedChanged += new System.EventHandler(this.cbExclude_CheckedChanged);
            // 
            // lvFilterList
            // 
            this.lvFilterList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.lvcFilterList});
            this.lvFilterList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvFilterList.FullRowSelect = true;
            this.lvFilterList.GridLines = true;
            this.lvFilterList.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvFilterList.HideSelection = false;
            this.lvFilterList.Location = new System.Drawing.Point(0, 260);
            this.lvFilterList.Margin = new System.Windows.Forms.Padding(0, 2, 0, 0);
            this.lvFilterList.MultiSelect = false;
            this.lvFilterList.Name = "lvFilterList";
            this.lvFilterList.ShowGroups = false;
            this.lvFilterList.Size = new System.Drawing.Size(245, 158);
            this.lvFilterList.TabIndex = 9;
            this.lvFilterList.UseCompatibleStateImageBehavior = false;
            this.lvFilterList.View = System.Windows.Forms.View.Details;
            this.lvFilterList.SelectedIndexChanged += new System.EventHandler(this.lvFilterList_SelectedIndexChanged);
            // 
            // lvcFilterList
            // 
            this.lvcFilterList.Text = "Lista filtrów dla kolumny";
            this.lvcFilterList.Width = 241;
            // 
            // gbColumnType
            // 
            this.gbColumnType.Controls.Add(this.tbColumnType);
            this.gbColumnType.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbColumnType.Location = new System.Drawing.Point(0, 0);
            this.gbColumnType.Margin = new System.Windows.Forms.Padding(0, 0, 0, 2);
            this.gbColumnType.Name = "gbColumnType";
            this.gbColumnType.Size = new System.Drawing.Size(245, 74);
            this.gbColumnType.TabIndex = 10;
            this.gbColumnType.TabStop = false;
            this.gbColumnType.Text = "Typ kolumny";
            // 
            // tbColumnType
            // 
            this.tbColumnType.ColumnCount = 2;
            this.tbColumnType.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tbColumnType.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tbColumnType.Controls.Add(this.cbSaved, 1, 0);
            this.tbColumnType.Controls.Add(this.cbColType, 0, 0);
            this.tbColumnType.Controls.Add(this.bChangeColType, 0, 1);
            this.tbColumnType.Controls.Add(this.bTypeSettings, 1, 1);
            this.tbColumnType.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbColumnType.Location = new System.Drawing.Point(3, 16);
            this.tbColumnType.Margin = new System.Windows.Forms.Padding(0);
            this.tbColumnType.Name = "tbColumnType";
            this.tbColumnType.RowCount = 2;
            this.tbColumnType.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tbColumnType.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tbColumnType.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tbColumnType.Size = new System.Drawing.Size(239, 55);
            this.tbColumnType.TabIndex = 0;
            // 
            // cbSaved
            // 
            this.cbSaved.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cbSaved.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSaved.Enabled = false;
            this.cbSaved.FormattingEnabled = true;
            this.cbSaved.Items.AddRange(new object[] {
            "Tekst",
            "Liczba",
            "Data",
            "Waluta",
            "Znak"});
            this.cbSaved.Location = new System.Drawing.Point(121, 2);
            this.cbSaved.Margin = new System.Windows.Forms.Padding(2, 2, 4, 0);
            this.cbSaved.Name = "cbSaved";
            this.cbSaved.Size = new System.Drawing.Size(114, 21);
            this.cbSaved.TabIndex = 5;
            // 
            // cbColType
            // 
            this.cbColType.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cbColType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbColType.Enabled = false;
            this.cbColType.FormattingEnabled = true;
            this.cbColType.Items.AddRange(new object[] {
            "Tekst",
            "Liczba",
            "Data",
            "Waluta",
            "Znak"});
            this.cbColType.Location = new System.Drawing.Point(4, 2);
            this.cbColType.Margin = new System.Windows.Forms.Padding(4, 2, 2, 0);
            this.cbColType.Name = "cbColType";
            this.cbColType.Size = new System.Drawing.Size(113, 21);
            this.cbColType.TabIndex = 0;
            // 
            // bChangeColType
            // 
            this.bChangeColType.Dock = System.Windows.Forms.DockStyle.Fill;
            this.bChangeColType.Enabled = false;
            this.bChangeColType.Location = new System.Drawing.Point(3, 27);
            this.bChangeColType.Margin = new System.Windows.Forms.Padding(3, 2, 1, 3);
            this.bChangeColType.Name = "bChangeColType";
            this.bChangeColType.Size = new System.Drawing.Size(115, 25);
            this.bChangeColType.TabIndex = 2;
            this.bChangeColType.Text = "Zmień typ kolumny";
            this.bChangeColType.UseVisualStyleBackColor = true;
            // 
            // bTypeSettings
            // 
            this.bTypeSettings.Dock = System.Windows.Forms.DockStyle.Fill;
            this.bTypeSettings.Enabled = false;
            this.bTypeSettings.Location = new System.Drawing.Point(120, 27);
            this.bTypeSettings.Margin = new System.Windows.Forms.Padding(1, 2, 3, 3);
            this.bTypeSettings.Name = "bTypeSettings";
            this.bTypeSettings.Size = new System.Drawing.Size(116, 25);
            this.bTypeSettings.TabIndex = 4;
            this.bTypeSettings.Text = "Zaawansowane";
            this.bTypeSettings.UseVisualStyleBackColor = true;
            // 
            // tbFilterControls
            // 
            this.tbFilterControls.ColumnCount = 3;
            this.tbFilterControls.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tbFilterControls.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tbFilterControls.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tbFilterControls.Controls.Add(this.bDeleteFilter, 2, 0);
            this.tbFilterControls.Controls.Add(this.bChangeFilter, 1, 0);
            this.tbFilterControls.Controls.Add(this.bAddFilter, 0, 0);
            this.tbFilterControls.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbFilterControls.Location = new System.Drawing.Point(0, 231);
            this.tbFilterControls.Margin = new System.Windows.Forms.Padding(0, 2, 0, 2);
            this.tbFilterControls.Name = "tbFilterControls";
            this.tbFilterControls.RowCount = 1;
            this.tbFilterControls.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tbFilterControls.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tbFilterControls.Size = new System.Drawing.Size(245, 25);
            this.tbFilterControls.TabIndex = 12;
            // 
            // bDeleteFilter
            // 
            this.bDeleteFilter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.bDeleteFilter.Enabled = false;
            this.bDeleteFilter.Location = new System.Drawing.Point(164, 0);
            this.bDeleteFilter.Margin = new System.Windows.Forms.Padding(2, 0, 0, 0);
            this.bDeleteFilter.Name = "bDeleteFilter";
            this.bDeleteFilter.Size = new System.Drawing.Size(81, 25);
            this.bDeleteFilter.TabIndex = 0;
            this.bDeleteFilter.Text = "Usuń";
            this.bDeleteFilter.UseVisualStyleBackColor = true;
            this.bDeleteFilter.Click += new System.EventHandler(this.bDeleteFilter_Click);
            // 
            // bChangeFilter
            // 
            this.bChangeFilter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.bChangeFilter.Enabled = false;
            this.bChangeFilter.Location = new System.Drawing.Point(83, 0);
            this.bChangeFilter.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.bChangeFilter.Name = "bChangeFilter";
            this.bChangeFilter.Size = new System.Drawing.Size(77, 25);
            this.bChangeFilter.TabIndex = 1;
            this.bChangeFilter.Text = "Zmień";
            this.bChangeFilter.UseVisualStyleBackColor = true;
            this.bChangeFilter.Click += new System.EventHandler(this.bChangeFilter_Click);
            // 
            // bAddFilter
            // 
            this.bAddFilter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.bAddFilter.Enabled = false;
            this.bAddFilter.Location = new System.Drawing.Point(0, 0);
            this.bAddFilter.Margin = new System.Windows.Forms.Padding(0, 0, 2, 0);
            this.bAddFilter.Name = "bAddFilter";
            this.bAddFilter.Size = new System.Drawing.Size(79, 25);
            this.bAddFilter.TabIndex = 2;
            this.bAddFilter.Text = "Dodaj";
            this.bAddFilter.UseVisualStyleBackColor = true;
            this.bAddFilter.Click += new System.EventHandler(this.bAddFilter_Click);
            // 
            // tlCreateColumns
            // 
            this.tlCreateColumns.ColumnCount = 1;
            this.tlCreateColumns.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlCreateColumns.Controls.Add(this.lvNewColumns, 0, 0);
            this.tlCreateColumns.Controls.Add(this.lvPreviewRows, 0, 2);
            this.tlCreateColumns.Controls.Add(this.tlColumnManagement, 0, 1);
            this.tlCreateColumns.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlCreateColumns.Location = new System.Drawing.Point(0, 0);
            this.tlCreateColumns.Margin = new System.Windows.Forms.Padding(0);
            this.tlCreateColumns.Name = "tlCreateColumns";
            this.tlCreateColumns.RowCount = 3;
            this.tlCreateColumns.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlCreateColumns.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 31F));
            this.tlCreateColumns.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlCreateColumns.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlCreateColumns.Size = new System.Drawing.Size(479, 418);
            this.tlCreateColumns.TabIndex = 0;
            // 
            // lvAllColumns
            // 
            this.lvAllColumns.AllowDrop = true;
            this.lvAllColumns.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.lvcAllColumns});
            this.lvAllColumns.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvAllColumns.FullRowSelect = true;
            this.lvAllColumns.GridLines = true;
            listViewGroup1.Header = "STARE KOLUMNY";
            listViewGroup1.Name = "lvgOldCols";
            listViewGroup2.Header = "NOWE KOLUMNY";
            listViewGroup2.Name = "lvgNewCols";
            this.lvAllColumns.Groups.AddRange(new System.Windows.Forms.ListViewGroup[] {
            listViewGroup1,
            listViewGroup2});
            this.lvAllColumns.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.lvAllColumns.HideSelection = false;
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
            this.lvAllColumns.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1,
            listViewItem2,
            listViewItem3,
            listViewItem4,
            listViewItem5});
            this.lvAllColumns.Location = new System.Drawing.Point(0, 0);
            this.lvAllColumns.Margin = new System.Windows.Forms.Padding(0);
            this.lvAllColumns.MultiSelect = false;
            this.lvAllColumns.Name = "lvAllColumns";
            this.lvAllColumns.Size = new System.Drawing.Size(209, 418);
            this.lvAllColumns.TabIndex = 9;
            this.lvAllColumns.UseCompatibleStateImageBehavior = false;
            this.lvAllColumns.View = System.Windows.Forms.View.Details;
            this.lvAllColumns.Visible = false;
            this.lvAllColumns.SelectedIndexChanged += new System.EventHandler(this.lvAllColumns_SelectedIndexChanged);
            // 
            // lvcAllColumns
            // 
            this.lvcAllColumns.Text = "Wszystkie kolumny";
            this.lvcAllColumns.Width = 207;
            // 
            // tlMain
            // 
            this.tlMain.ColumnCount = 1;
            this.tlMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlMain.Controls.Add(this.tlStatusBar, 0, 1);
            this.tlMain.Controls.Add(this.scMain, 0, 0);
            this.tlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlMain.Location = new System.Drawing.Point(0, 0);
            this.tlMain.Margin = new System.Windows.Forms.Padding(0);
            this.tlMain.Name = "tlMain";
            this.tlMain.RowCount = 2;
            this.tlMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tlMain.Size = new System.Drawing.Size(704, 462);
            this.tlMain.TabIndex = 0;
            // 
            // EditColumnsForm
            // 
            this.AcceptButton = this.bSave;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.bCancel;
            this.ClientSize = new System.Drawing.Size(704, 462);
            this.Controls.Add(this.tlMain);
            this.MaximumSize = new System.Drawing.Size(65535, 65535);
            this.MinimumSize = new System.Drawing.Size(700, 480);
            this.Name = "EditColumnsForm";
            this.Text = "Zarządzanie kolumnami";
            this.Deactivate += new System.EventHandler(this.toolTip_Hide);
            this.Move += new System.EventHandler(this.toolTip_Hide);
            this.tlStatusBar.ResumeLayout(false);
            this.tlStatusBar.PerformLayout();
            this.tbResultButtons.ResumeLayout(false);
            this.tlColumnManagement.ResumeLayout(false);
            this.tlColumnManagement.PerformLayout();
            this.scMain.Panel1.ResumeLayout(false);
            this.scMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.scMain)).EndInit();
            this.scMain.ResumeLayout(false);
            this.scFiltersAndTypes.Panel1.ResumeLayout(false);
            this.scFiltersAndTypes.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.scFiltersAndTypes)).EndInit();
            this.scFiltersAndTypes.ResumeLayout(false);
            this.tlFilterGroups.ResumeLayout(false);
            this.gbFilterConfig.ResumeLayout(false);
            this.tbFilterConfig.ResumeLayout(false);
            this.tbFilterConfig.PerformLayout();
            this.gbColumnType.ResumeLayout(false);
            this.tbColumnType.ResumeLayout(false);
            this.tbFilterControls.ResumeLayout(false);
            this.tlCreateColumns.ResumeLayout(false);
            this.tlMain.ResumeLayout(false);
            this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.ToolTip tpTooltip;
		private System.Windows.Forms.ListView lvPreviewRows;
		private System.Windows.Forms.ColumnHeader lvcDataPreview;
		private System.Windows.Forms.TableLayoutPanel tlStatusBar;
		private System.Windows.Forms.TableLayoutPanel tlColumnManagement;
		private System.Windows.Forms.Button bAddColumn;
		private System.Windows.Forms.Button bClearColumn;
		private System.Windows.Forms.Button bDeleteColumn;
		private System.Windows.Forms.TextBox tbColumnName;
		private System.Windows.Forms.ListView lvNewColumns;
		private System.Windows.Forms.ColumnHeader lvcColumnName;
		private System.Windows.Forms.ColumnHeader lvcJoinedColumns;
		private System.Windows.Forms.ListView lvDatabaseColumns;
		private System.Windows.Forms.ColumnHeader lvcColumns;
		private System.Windows.Forms.Button bSave;
		private System.Windows.Forms.TableLayoutPanel tlMain;
		private System.Windows.Forms.SplitContainer scMain;
		private System.Windows.Forms.TableLayoutPanel tlCreateColumns;
		private System.Windows.Forms.Button bCancel;
		private System.Windows.Forms.ComboBox cbStep;
		private System.Windows.Forms.TableLayoutPanel tbResultButtons;
		private System.Windows.Forms.ListView lvAllColumns;
		private System.Windows.Forms.ColumnHeader lvcAllColumns;
		private System.Windows.Forms.SplitContainer scFiltersAndTypes;
		private System.Windows.Forms.ListView lvPreviewAllRows;
		private System.Windows.Forms.ColumnHeader lvcRecordPreview;
		private System.Windows.Forms.TableLayoutPanel tlFilterGroups;
		private System.Windows.Forms.ListView lvFilterList;
		private System.Windows.Forms.ColumnHeader lvcFilterList;
		private System.Windows.Forms.GroupBox gbColumnType;
		private System.Windows.Forms.GroupBox gbFilterConfig;
		private System.Windows.Forms.TableLayoutPanel tbFilterControls;
		private System.Windows.Forms.Button bDeleteFilter;
		private System.Windows.Forms.Button bChangeFilter;
		private System.Windows.Forms.Button bAddFilter;
		private System.Windows.Forms.TableLayoutPanel tbColumnType;
		private System.Windows.Forms.ComboBox cbColType;
		private System.Windows.Forms.TableLayoutPanel tbFilterConfig;
		private System.Windows.Forms.Label lModifier;
		private System.Windows.Forms.Label lFilterType;
		private System.Windows.Forms.Label lResult;
		private System.Windows.Forms.ComboBox cbFilterType;
		private System.Windows.Forms.TextBox tbModifier;
		private System.Windows.Forms.TextBox tbResult;
		private System.Windows.Forms.CheckBox cbExclude;
		private System.Windows.Forms.CheckBox cbAllCopies;
		private System.Windows.Forms.Button bTypeSettings;
		private System.Windows.Forms.Button bChangeColType;
		private System.Windows.Forms.ComboBox cbSaved;
        private System.Windows.Forms.CheckBox checkBox1;

		/// @endcond
	}
}