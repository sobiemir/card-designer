namespace CDesigner
{
	partial class MainForm
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
			this.components = new System.ComponentModel.Container();
			this.mpPreview = new System.Windows.Forms.Panel();
			this.mtlStatusBar = new System.Windows.Forms.TableLayoutPanel();
			this.mlStatus = new System.Windows.Forms.Label();
			this.mfPageLayout = new System.Windows.Forms.FlowLayoutPanel();
			this.mnPage = new System.Windows.Forms.NumericUpDown();
			this.mlPage = new System.Windows.Forms.Label();
			this.mbDelete = new System.Windows.Forms.Button();
			this.mbNew = new System.Windows.Forms.Button();
			this.mtvPatterns = new System.Windows.Forms.TreeView();
			this.gcmPattern = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.ictNew = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator10 = new System.Windows.Forms.ToolStripSeparator();
			this.ictEdit = new System.Windows.Forms.ToolStripMenuItem();
			this.ictLoadData = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator9 = new System.Windows.Forms.ToolStripSeparator();
			this.ictImport = new System.Windows.Forms.ToolStripMenuItem();
			this.ictExport = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator11 = new System.Windows.Forms.ToolStripSeparator();
			this.ictDelete = new System.Windows.Forms.ToolStripMenuItem();
			this.imMain = new System.Windows.Forms.MenuStrip();
			this.gmPattern = new System.Windows.Forms.ToolStripMenuItem();
			this.gmpNew = new System.Windows.Forms.ToolStripMenuItem();
			this.gmpRecent = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.gmprClearList = new System.Windows.Forms.ToolStripMenuItem();
			this.gmSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.gmpImport = new System.Windows.Forms.ToolStripMenuItem();
			this.gmpExportAll = new System.Windows.Forms.ToolStripMenuItem();
			this.gmSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.gmpClose = new System.Windows.Forms.ToolStripMenuItem();
			this.gsData = new System.Windows.Forms.ToolStripMenuItem();
			this.gsPattern = new System.Windows.Forms.ToolStripMenuItem();
			this.gsHome = new System.Windows.Forms.ToolStripMenuItem();
			this.gmTools = new System.Windows.Forms.ToolStripMenuItem();
			this.gmtLoadDatabase = new System.Windows.Forms.ToolStripMenuItem();
			this.gmSeparator3 = new System.Windows.Forms.ToolStripSeparator();
			this.gmtColumnsEditor = new System.Windows.Forms.ToolStripMenuItem();
			this.gmtCreateDB = new System.Windows.Forms.ToolStripMenuItem();
			this.gmtConnectDB = new System.Windows.Forms.ToolStripMenuItem();
			this.gmtBackup = new System.Windows.Forms.ToolStripMenuItem();
			this.gmSettings = new System.Windows.Forms.ToolStripMenuItem();
			this.issGeneratePDF = new System.Windows.Forms.ToolStripMenuItem();
			this.issEditor = new System.Windows.Forms.ToolStripMenuItem();
			this.issGeneral = new System.Windows.Forms.ToolStripMenuItem();
			this.gmProgram = new System.Windows.Forms.ToolStripMenuItem();
			this.gmpInfo = new System.Windows.Forms.ToolStripMenuItem();
			this.gmpHelp = new System.Windows.Forms.ToolStripMenuItem();
			this.gmpUpdate = new System.Windows.Forms.ToolStripMenuItem();
			this.icPage = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.icpAddField = new System.Windows.Forms.ToolStripMenuItem();
			this.icpDeleteFields = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator12 = new System.Windows.Forms.ToolStripSeparator();
			this.icpPageColor = new System.Windows.Forms.ToolStripMenuItem();
			this.icpPageBack = new System.Windows.Forms.ToolStripMenuItem();
			this.icpPageClear = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.icpPrintColor = new System.Windows.Forms.ToolStripMenuItem();
			this.icpPrintImage = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
			this.icpAddPage = new System.Windows.Forms.ToolStripMenuItem();
			this.icpDeletePage = new System.Windows.Forms.ToolStripMenuItem();
			this.icLabel = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.iclBackColor = new System.Windows.Forms.ToolStripMenuItem();
			this.iclBackImage = new System.Windows.Forms.ToolStripMenuItem();
			this.iclClearBack = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
			this.iclBackFromDb = new System.Windows.Forms.ToolStripMenuItem();
			this.iclPrintColor = new System.Windows.Forms.ToolStripMenuItem();
			this.iclPrintImage = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
			this.iclFontColor = new System.Windows.Forms.ToolStripMenuItem();
			this.iclFontName = new System.Windows.Forms.ToolStripMenuItem();
			this.iclTextFromDb = new System.Windows.Forms.ToolStripMenuItem();
			this.iclPrintText = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
			this.iclBorderColor = new System.Windows.Forms.ToolStripMenuItem();
			this.iclPrintBorder = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
			this.iclDeleteField = new System.Windows.Forms.ToolStripMenuItem();
			this.gsImage = new System.Windows.Forms.OpenFileDialog();
			this.gsColor = new System.Windows.Forms.ColorDialog();
			this.gsFont = new System.Windows.Forms.FontDialog();
			this.dpPreview = new System.Windows.Forms.Panel();
			this.dtButtonTable = new System.Windows.Forms.TableLayoutPanel();
			this.dbGeneratePDF = new System.Windows.Forms.Button();
			this.dbScan = new System.Windows.Forms.Button();
			this.dtvData = new System.Windows.Forms.TreeView();
			this.gsOpenFile = new System.Windows.Forms.OpenFileDialog();
			this.pbSave = new System.Windows.Forms.Button();
			this.pbLoadData = new System.Windows.Forms.Button();
			this.ptInfoControls = new System.Windows.Forms.TableLayoutPanel();
			this.pflPage = new System.Windows.Forms.FlowLayoutPanel();
			this.pnPage = new System.Windows.Forms.NumericUpDown();
			this.plPage = new System.Windows.Forms.Label();
			this.plStatus = new System.Windows.Forms.Label();
			this.pcbScale = new System.Windows.Forms.ComboBox();
			this.ppPreview = new System.Windows.Forms.Panel();
			this.ptPatternDetails = new System.Windows.Forms.TableLayoutPanel();
			this.icPatMenu = new System.Windows.Forms.MenuStrip();
			this.icpmFieldDetails = new System.Windows.Forms.ToolStripMenuItem();
			this.icpmDetails = new System.Windows.Forms.ToolStripMenuItem();
			this.icpmPageDetails = new System.Windows.Forms.ToolStripMenuItem();
			this.ppDetailsPanel = new System.Windows.Forms.Panel();
			this.ptFieldDetails = new System.Windows.Forms.TableLayoutPanel();
			this.pcbTextTransform = new System.Windows.Forms.ComboBox();
			this.plTextTransform = new System.Windows.Forms.Label();
			this.plHeight = new System.Windows.Forms.Label();
			this.pnBorderSize = new System.Windows.Forms.NumericUpDown();
			this.pnPadding = new System.Windows.Forms.NumericUpDown();
			this.plPadding = new System.Windows.Forms.Label();
			this.plTextPosition = new System.Windows.Forms.Label();
			this.pbFontName = new System.Windows.Forms.Button();
			this.ptbFontName = new System.Windows.Forms.TextBox();
			this.pbBackImage = new System.Windows.Forms.Button();
			this.ptbBackImage = new System.Windows.Forms.TextBox();
			this.pbBackColor = new System.Windows.Forms.Button();
			this.ptbBackColor = new System.Windows.Forms.TextBox();
			this.pbFontColor = new System.Windows.Forms.Button();
			this.ptbFontColor = new System.Windows.Forms.TextBox();
			this.ptbName = new System.Windows.Forms.TextBox();
			this.pnPositionY = new System.Windows.Forms.NumericUpDown();
			this.pnPositionX = new System.Windows.Forms.NumericUpDown();
			this.plPositionX = new System.Windows.Forms.Label();
			this.plWidth = new System.Windows.Forms.Label();
			this.pnWidth = new System.Windows.Forms.NumericUpDown();
			this.pnHeight = new System.Windows.Forms.NumericUpDown();
			this.plBorderColor = new System.Windows.Forms.Label();
			this.ptbBorderColor = new System.Windows.Forms.TextBox();
			this.pbBorderColor = new System.Windows.Forms.Button();
			this.plFont = new System.Windows.Forms.Label();
			this.plBorderWidth = new System.Windows.Forms.Label();
			this.pcbTextAlign = new System.Windows.Forms.ComboBox();
			this.plPositionY = new System.Windows.Forms.Label();
			this.ptDetails = new System.Windows.Forms.TableLayoutPanel();
			this.pcbdAddMargin = new System.Windows.Forms.CheckBox();
			this.pldAdditionalMargin = new System.Windows.Forms.Label();
			this.pcbPosAlign = new System.Windows.Forms.ComboBox();
			this.plStickPoint = new System.Windows.Forms.Label();
			this.pcbUseImageMargin = new System.Windows.Forms.CheckBox();
			this.pcbDrawFrameOutside = new System.Windows.Forms.CheckBox();
			this.plImageSettings = new System.Windows.Forms.Label();
			this.pcbDynImage = new System.Windows.Forms.CheckBox();
			this.pcbStatText = new System.Windows.Forms.CheckBox();
			this.pcbDrawColor = new System.Windows.Forms.CheckBox();
			this.pcbDynText = new System.Windows.Forms.CheckBox();
			this.plPDFGenerate = new System.Windows.Forms.Label();
			this.pcbShowFrame = new System.Windows.Forms.CheckBox();
			this.pcbStatImage = new System.Windows.Forms.CheckBox();
			this.pcxImageSet = new System.Windows.Forms.ComboBox();
			this.pcbMarginLR = new System.Windows.Forms.NumericUpDown();
			this.pcbMarginTB = new System.Windows.Forms.NumericUpDown();
			this.ptPageDetails = new System.Windows.Forms.TableLayoutPanel();
			this.pcbPageLook = new System.Windows.Forms.Label();
			this.ptbPageImage = new System.Windows.Forms.TextBox();
			this.pbPageImage = new System.Windows.Forms.Button();
			this.pbPageColor = new System.Windows.Forms.Button();
			this.ptbPageColor = new System.Windows.Forms.TextBox();
			this.pcbpDrawOutside = new System.Windows.Forms.CheckBox();
			this.pcbpApplyMargin = new System.Windows.Forms.CheckBox();
			this.pcxpImageSet = new System.Windows.Forms.ComboBox();
			this.plpImageSettings = new System.Windows.Forms.Label();
			this.pcbpDrawColor = new System.Windows.Forms.CheckBox();
			this.pcbpDrawImage = new System.Windows.Forms.CheckBox();
			this.ptbpWidth = new System.Windows.Forms.TextBox();
			this.ptbpHeight = new System.Windows.Forms.TextBox();
			this.plpWidth = new System.Windows.Forms.Label();
			this.plpHeight = new System.Windows.Forms.Label();
			this.plpGeneratePDF = new System.Windows.Forms.Label();
			this.scData = new System.Windows.Forms.SplitContainer();
			this.scMain = new System.Windows.Forms.SplitContainer();
			this.mtlButtons = new System.Windows.Forms.TableLayoutPanel();
			this.tlMain = new System.Windows.Forms.TableLayoutPanel();
			this.tlMainStatusBar = new System.Windows.Forms.TableLayoutPanel();
			this.tDataTable = new System.Windows.Forms.TableLayoutPanel();
			this.sbData = new System.Windows.Forms.TableLayoutPanel();
			this.dtInfoControls = new System.Windows.Forms.TableLayoutPanel();
			this.dfPage = new System.Windows.Forms.FlowLayoutPanel();
			this.dnPage = new System.Windows.Forms.NumericUpDown();
			this.dlPage = new System.Windows.Forms.Label();
			this.dlStatus = new System.Windows.Forms.Label();
			this.dcbZoom = new System.Windows.Forms.ComboBox();
			this.tPattern = new System.Windows.Forms.TableLayoutPanel();
			this.scPattern = new System.Windows.Forms.SplitContainer();
			this.sbPattern = new System.Windows.Forms.TableLayoutPanel();
			this.ptStatusButtons = new System.Windows.Forms.TableLayoutPanel();
			this.mtlStatusBar.SuspendLayout();
			this.mfPageLayout.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.mnPage)).BeginInit();
			this.gcmPattern.SuspendLayout();
			this.imMain.SuspendLayout();
			this.icPage.SuspendLayout();
			this.icLabel.SuspendLayout();
			this.dtButtonTable.SuspendLayout();
			this.ptInfoControls.SuspendLayout();
			this.pflPage.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pnPage)).BeginInit();
			this.ptPatternDetails.SuspendLayout();
			this.icPatMenu.SuspendLayout();
			this.ppDetailsPanel.SuspendLayout();
			this.ptFieldDetails.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pnBorderSize)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pnPadding)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pnPositionY)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pnPositionX)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pnWidth)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pnHeight)).BeginInit();
			this.ptDetails.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pcbMarginLR)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pcbMarginTB)).BeginInit();
			this.ptPageDetails.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.scData)).BeginInit();
			this.scData.Panel1.SuspendLayout();
			this.scData.Panel2.SuspendLayout();
			this.scData.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.scMain)).BeginInit();
			this.scMain.Panel1.SuspendLayout();
			this.scMain.Panel2.SuspendLayout();
			this.scMain.SuspendLayout();
			this.mtlButtons.SuspendLayout();
			this.tlMain.SuspendLayout();
			this.tlMainStatusBar.SuspendLayout();
			this.tDataTable.SuspendLayout();
			this.sbData.SuspendLayout();
			this.dtInfoControls.SuspendLayout();
			this.dfPage.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dnPage)).BeginInit();
			this.tPattern.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.scPattern)).BeginInit();
			this.scPattern.Panel1.SuspendLayout();
			this.scPattern.Panel2.SuspendLayout();
			this.scPattern.SuspendLayout();
			this.sbPattern.SuspendLayout();
			this.ptStatusButtons.SuspendLayout();
			this.SuspendLayout();
			// 
			// mpPreview
			// 
			this.mpPreview.AutoScroll = true;
			this.mpPreview.BackColor = System.Drawing.SystemColors.ScrollBar;
			this.mpPreview.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.mpPreview.Dock = System.Windows.Forms.DockStyle.Fill;
			this.mpPreview.Location = new System.Drawing.Point(0, 0);
			this.mpPreview.Margin = new System.Windows.Forms.Padding(1, 0, 0, 0);
			this.mpPreview.Name = "mpPreview";
			this.mpPreview.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.mpPreview.Size = new System.Drawing.Size(416, 408);
			this.mpPreview.TabIndex = 5;
			this.mpPreview.MouseDown += new System.Windows.Forms.MouseEventHandler(this.mpPreview_MouseDown);
			this.mpPreview.Resize += new System.EventHandler(this.mpPreview_Resize);
			// 
			// mtlStatusBar
			// 
			this.mtlStatusBar.ColumnCount = 2;
			this.mtlStatusBar.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 78.57143F));
			this.mtlStatusBar.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 21.42857F));
			this.mtlStatusBar.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.mtlStatusBar.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.mtlStatusBar.Controls.Add(this.mlStatus, 0, 0);
			this.mtlStatusBar.Controls.Add(this.mfPageLayout, 1, 0);
			this.mtlStatusBar.Dock = System.Windows.Forms.DockStyle.Fill;
			this.mtlStatusBar.Location = new System.Drawing.Point(257, 1);
			this.mtlStatusBar.Margin = new System.Windows.Forms.Padding(0, 1, 0, 0);
			this.mtlStatusBar.Name = "mtlStatusBar";
			this.mtlStatusBar.Padding = new System.Windows.Forms.Padding(3);
			this.mtlStatusBar.RowCount = 1;
			this.mtlStatusBar.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.mtlStatusBar.Size = new System.Drawing.Size(427, 30);
			this.mtlStatusBar.TabIndex = 6;
			// 
			// mlStatus
			// 
			this.mlStatus.AutoSize = true;
			this.mlStatus.Dock = System.Windows.Forms.DockStyle.Fill;
			this.mlStatus.Location = new System.Drawing.Point(5, 3);
			this.mlStatus.Margin = new System.Windows.Forms.Padding(2, 0, 3, 0);
			this.mlStatus.Name = "mlStatus";
			this.mlStatus.Size = new System.Drawing.Size(325, 24);
			this.mlStatus.TabIndex = 22;
			this.mlStatus.Text = "Proszę czekać...";
			this.mlStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// mfPageLayout
			// 
			this.mfPageLayout.Controls.Add(this.mnPage);
			this.mfPageLayout.Controls.Add(this.mlPage);
			this.mfPageLayout.Dock = System.Windows.Forms.DockStyle.Fill;
			this.mfPageLayout.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
			this.mfPageLayout.Location = new System.Drawing.Point(333, 3);
			this.mfPageLayout.Margin = new System.Windows.Forms.Padding(0, 0, 3, 0);
			this.mfPageLayout.Name = "mfPageLayout";
			this.mfPageLayout.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.mfPageLayout.Size = new System.Drawing.Size(88, 24);
			this.mfPageLayout.TabIndex = 23;
			// 
			// mnPage
			// 
			this.mnPage.Enabled = false;
			this.mnPage.Location = new System.Drawing.Point(53, 2);
			this.mnPage.Margin = new System.Windows.Forms.Padding(0, 2, 0, 0);
			this.mnPage.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.mnPage.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.mnPage.Name = "mnPage";
			this.mnPage.Size = new System.Drawing.Size(35, 20);
			this.mnPage.TabIndex = 21;
			this.mnPage.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.mnPage.ValueChanged += new System.EventHandler(this.mnPage_ValueChanged);
			// 
			// mlPage
			// 
			this.mlPage.AutoSize = true;
			this.mlPage.Dock = System.Windows.Forms.DockStyle.Right;
			this.mlPage.Location = new System.Drawing.Point(9, 0);
			this.mlPage.Name = "mlPage";
			this.mlPage.Size = new System.Drawing.Size(41, 22);
			this.mlPage.TabIndex = 22;
			this.mlPage.Text = "Strona:";
			this.mlPage.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// mbDelete
			// 
			this.mbDelete.Dock = System.Windows.Forms.DockStyle.Fill;
			this.mbDelete.Enabled = false;
			this.mbDelete.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.mbDelete.Location = new System.Drawing.Point(129, 3);
			this.mbDelete.Margin = new System.Windows.Forms.Padding(3, 3, 1, 3);
			this.mbDelete.Name = "mbDelete";
			this.mbDelete.Size = new System.Drawing.Size(122, 24);
			this.mbDelete.TabIndex = 18;
			this.mbDelete.Text = "Usuń";
			this.mbDelete.Click += new System.EventHandler(this.ictDelete_Click);
			this.mbDelete.MouseEnter += new System.EventHandler(this.mbDelete_MouseEnter);
			this.mbDelete.MouseLeave += new System.EventHandler(this.mlStatus_ClearText);
			// 
			// mbNew
			// 
			this.mbNew.Dock = System.Windows.Forms.DockStyle.Fill;
			this.mbNew.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.mbNew.Location = new System.Drawing.Point(1, 3);
			this.mbNew.Margin = new System.Windows.Forms.Padding(1, 3, 3, 3);
			this.mbNew.Name = "mbNew";
			this.mbNew.Size = new System.Drawing.Size(122, 24);
			this.mbNew.TabIndex = 16;
			this.mbNew.Text = "Nowy";
			this.mbNew.Click += new System.EventHandler(this.gmpNew_Click);
			this.mbNew.MouseEnter += new System.EventHandler(this.mbNew_MouseEnter);
			this.mbNew.MouseLeave += new System.EventHandler(this.mlStatus_ClearText);
			// 
			// mtvPatterns
			// 
			this.mtvPatterns.ContextMenuStrip = this.gcmPattern;
			this.mtvPatterns.Dock = System.Windows.Forms.DockStyle.Fill;
			this.mtvPatterns.FullRowSelect = true;
			this.mtvPatterns.HideSelection = false;
			this.mtvPatterns.Location = new System.Drawing.Point(0, 0);
			this.mtvPatterns.Margin = new System.Windows.Forms.Padding(0, 0, 1, 0);
			this.mtvPatterns.Name = "mtvPatterns";
			this.mtvPatterns.ShowLines = false;
			this.mtvPatterns.ShowPlusMinus = false;
			this.mtvPatterns.ShowRootLines = false;
			this.mtvPatterns.Size = new System.Drawing.Size(250, 408);
			this.mtvPatterns.TabIndex = 7;
			this.mtvPatterns.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.mtvPatterns_AfterSelect);
			this.mtvPatterns.DoubleClick += new System.EventHandler(this.ictEdit_Click);
			this.mtvPatterns.MouseDown += new System.Windows.Forms.MouseEventHandler(this.mtvPatterns_MouseDown);
			// 
			// gcmPattern
			// 
			this.gcmPattern.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ictNew,
            this.toolStripSeparator10,
            this.ictEdit,
            this.ictLoadData,
            this.toolStripSeparator9,
            this.ictImport,
            this.ictExport,
            this.toolStripSeparator11,
            this.ictDelete});
			this.gcmPattern.Name = "icPattern";
			this.gcmPattern.Size = new System.Drawing.Size(154, 154);
			this.gcmPattern.Opening += new System.ComponentModel.CancelEventHandler(this.icPattern_Opening);
			// 
			// ictNew
			// 
			this.ictNew.Name = "ictNew";
			this.ictNew.ShortcutKeyDisplayString = "Ctrl+N";
			this.ictNew.Size = new System.Drawing.Size(153, 22);
			this.ictNew.Text = "Nowy";
			this.ictNew.Click += new System.EventHandler(this.gmpNew_Click);
			// 
			// toolStripSeparator10
			// 
			this.toolStripSeparator10.Name = "toolStripSeparator10";
			this.toolStripSeparator10.Size = new System.Drawing.Size(150, 6);
			// 
			// ictEdit
			// 
			this.ictEdit.Name = "ictEdit";
			this.ictEdit.Size = new System.Drawing.Size(153, 22);
			this.ictEdit.Text = "Edytuj";
			this.ictEdit.Click += new System.EventHandler(this.ictEdit_Click);
			// 
			// ictLoadData
			// 
			this.ictLoadData.Name = "ictLoadData";
			this.ictLoadData.ShortcutKeyDisplayString = "";
			this.ictLoadData.Size = new System.Drawing.Size(153, 22);
			this.ictLoadData.Text = "Wczytaj dane...";
			this.ictLoadData.Click += new System.EventHandler(this.ictLoadData_Click);
			// 
			// toolStripSeparator9
			// 
			this.toolStripSeparator9.Name = "toolStripSeparator9";
			this.toolStripSeparator9.Size = new System.Drawing.Size(150, 6);
			// 
			// ictImport
			// 
			this.ictImport.Enabled = false;
			this.ictImport.Name = "ictImport";
			this.ictImport.Size = new System.Drawing.Size(153, 22);
			this.ictImport.Text = "Importuj...";
			// 
			// ictExport
			// 
			this.ictExport.Enabled = false;
			this.ictExport.Name = "ictExport";
			this.ictExport.Size = new System.Drawing.Size(153, 22);
			this.ictExport.Text = "Eksportuj...";
			// 
			// toolStripSeparator11
			// 
			this.toolStripSeparator11.Name = "toolStripSeparator11";
			this.toolStripSeparator11.Size = new System.Drawing.Size(150, 6);
			// 
			// ictDelete
			// 
			this.ictDelete.Name = "ictDelete";
			this.ictDelete.Size = new System.Drawing.Size(153, 22);
			this.ictDelete.Text = "Usuń";
			this.ictDelete.Click += new System.EventHandler(this.ictDelete_Click);
			// 
			// imMain
			// 
			this.imMain.BackColor = System.Drawing.SystemColors.ControlLight;
			this.imMain.GripMargin = new System.Windows.Forms.Padding(2);
			this.imMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.gmPattern,
            this.gsData,
            this.gsPattern,
            this.gsHome,
            this.gmTools,
            this.gmSettings,
            this.gmProgram});
			this.imMain.Location = new System.Drawing.Point(0, 0);
			this.imMain.Name = "imMain";
			this.imMain.Padding = new System.Windows.Forms.Padding(6, 3, 6, 5);
			this.imMain.Size = new System.Drawing.Size(684, 31);
			this.imMain.TabIndex = 1;
			this.imMain.Text = "Menu";
			this.imMain.Paint += new System.Windows.Forms.PaintEventHandler(this.imMain_Paint);
			// 
			// gmPattern
			// 
			this.gmPattern.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.gmpNew,
            this.gmpRecent,
            this.gmSeparator1,
            this.gmpImport,
            this.gmpExportAll,
            this.gmSeparator2,
            this.gmpClose});
			this.gmPattern.Name = "gmPattern";
			this.gmPattern.Padding = new System.Windows.Forms.Padding(6, 2, 6, 2);
			this.gmPattern.Size = new System.Drawing.Size(50, 23);
			this.gmPattern.Text = "&Wzór";
			// 
			// gmpNew
			// 
			this.gmpNew.Name = "gmpNew";
			this.gmpNew.ShortcutKeyDisplayString = "";
			this.gmpNew.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
			this.gmpNew.Size = new System.Drawing.Size(209, 22);
			this.gmpNew.Text = "Nowy...";
			this.gmpNew.Click += new System.EventHandler(this.gmpNew_Click);
			// 
			// gmpRecent
			// 
			this.gmpRecent.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripSeparator1,
            this.gmprClearList});
			this.gmpRecent.Enabled = false;
			this.gmpRecent.Name = "gmpRecent";
			this.gmpRecent.Size = new System.Drawing.Size(209, 22);
			this.gmpRecent.Text = "Ostatnio otwierane";
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(184, 6);
			// 
			// gmprClearList
			// 
			this.gmprClearList.Name = "gmprClearList";
			this.gmprClearList.ShortcutKeyDisplayString = "";
			this.gmprClearList.Size = new System.Drawing.Size(187, 22);
			this.gmprClearList.Text = "Wyczyść listę wzorów";
			this.gmprClearList.Click += new System.EventHandler(this.gmprClearList_Click);
			// 
			// gmSeparator1
			// 
			this.gmSeparator1.Name = "gmSeparator1";
			this.gmSeparator1.Size = new System.Drawing.Size(206, 6);
			// 
			// gmpImport
			// 
			this.gmpImport.Enabled = false;
			this.gmpImport.Name = "gmpImport";
			this.gmpImport.Size = new System.Drawing.Size(209, 22);
			this.gmpImport.Text = "Importuj...";
			this.gmpImport.Visible = false;
			// 
			// gmpExportAll
			// 
			this.gmpExportAll.Enabled = false;
			this.gmpExportAll.Name = "gmpExportAll";
			this.gmpExportAll.Size = new System.Drawing.Size(209, 22);
			this.gmpExportAll.Text = "Eksportuj wszystkie";
			this.gmpExportAll.Visible = false;
			// 
			// gmSeparator2
			// 
			this.gmSeparator2.Name = "gmSeparator2";
			this.gmSeparator2.Size = new System.Drawing.Size(206, 6);
			this.gmSeparator2.Visible = false;
			// 
			// gmpClose
			// 
			this.gmpClose.Name = "gmpClose";
			this.gmpClose.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
			this.gmpClose.Size = new System.Drawing.Size(209, 22);
			this.gmpClose.Text = "Zakończ program";
			this.gmpClose.Click += new System.EventHandler(this.gmpClose_Click);
			// 
			// gsData
			// 
			this.gsData.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
			this.gsData.Enabled = false;
			this.gsData.Name = "gsData";
			this.gsData.Padding = new System.Windows.Forms.Padding(6, 2, 6, 2);
			this.gsData.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.D3)));
			this.gsData.Size = new System.Drawing.Size(50, 23);
			this.gsData.Text = "Dane";
			this.gsData.Click += new System.EventHandler(this.gsData_Click);
			// 
			// gsPattern
			// 
			this.gsPattern.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
			this.gsPattern.Enabled = false;
			this.gsPattern.Name = "gsPattern";
			this.gsPattern.Padding = new System.Windows.Forms.Padding(6, 2, 6, 2);
			this.gsPattern.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.D2)));
			this.gsPattern.Size = new System.Drawing.Size(50, 23);
			this.gsPattern.Text = "Wzór";
			this.gsPattern.Click += new System.EventHandler(this.gsPattern_Click);
			// 
			// gsHome
			// 
			this.gsHome.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
			this.gsHome.Enabled = false;
			this.gsHome.Name = "gsHome";
			this.gsHome.Padding = new System.Windows.Forms.Padding(6, 2, 6, 2);
			this.gsHome.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.D1)));
			this.gsHome.Size = new System.Drawing.Size(63, 23);
			this.gsHome.Text = "Główna";
			this.gsHome.Click += new System.EventHandler(this.gsMain_Click);
			// 
			// gmTools
			// 
			this.gmTools.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.gmtLoadDatabase,
            this.gmSeparator3,
            this.gmtColumnsEditor,
            this.gmtCreateDB,
            this.gmtConnectDB,
            this.gmtBackup});
			this.gmTools.Name = "gmTools";
			this.gmTools.Padding = new System.Windows.Forms.Padding(6, 2, 6, 2);
			this.gmTools.Size = new System.Drawing.Size(74, 23);
			this.gmTools.Text = "&Narzędzia";
			// 
			// gmtLoadDatabase
			// 
			this.gmtLoadDatabase.Name = "gmtLoadDatabase";
			this.gmtLoadDatabase.Size = new System.Drawing.Size(193, 22);
			this.gmtLoadDatabase.Text = "Wczytaj bazę danych...";
			this.gmtLoadDatabase.Click += new System.EventHandler(this.gmtLoadDatabase_Click);
			// 
			// gmSeparator3
			// 
			this.gmSeparator3.Name = "gmSeparator3";
			this.gmSeparator3.Size = new System.Drawing.Size(190, 6);
			// 
			// gmtColumnsEditor
			// 
			this.gmtColumnsEditor.Enabled = false;
			this.gmtColumnsEditor.Name = "gmtColumnsEditor";
			this.gmtColumnsEditor.Size = new System.Drawing.Size(193, 22);
			this.gmtColumnsEditor.Text = "Zarządzaj kolumnami";
			this.gmtColumnsEditor.Click += new System.EventHandler(this.gmtJoinColumns_Click);
			// 
			// gmtCreateDB
			// 
			this.gmtCreateDB.Name = "gmtCreateDB";
			this.gmtCreateDB.Size = new System.Drawing.Size(193, 22);
			this.gmtCreateDB.Text = "Utwórz bazę danych";
			this.gmtCreateDB.Visible = false;
			// 
			// gmtConnectDB
			// 
			this.gmtConnectDB.Name = "gmtConnectDB";
			this.gmtConnectDB.Size = new System.Drawing.Size(193, 22);
			this.gmtConnectDB.Text = "Połącz z bazą danych";
			this.gmtConnectDB.Visible = false;
			this.gmtConnectDB.Click += new System.EventHandler(this.gmtConnectDB_Click);
			// 
			// gmtBackup
			// 
			this.gmtBackup.Name = "gmtBackup";
			this.gmtBackup.Size = new System.Drawing.Size(193, 22);
			this.gmtBackup.Text = "Kopia zapasowa";
			this.gmtBackup.Visible = false;
			// 
			// gmSettings
			// 
			this.gmSettings.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.issGeneratePDF,
            this.issEditor,
            this.issGeneral});
			this.gmSettings.Name = "gmSettings";
			this.gmSettings.Padding = new System.Windows.Forms.Padding(6, 2, 6, 2);
			this.gmSettings.Size = new System.Drawing.Size(80, 23);
			this.gmSettings.Text = "&Ustawienia";
			this.gmSettings.Visible = false;
			// 
			// issGeneratePDF
			// 
			this.issGeneratePDF.Enabled = false;
			this.issGeneratePDF.Name = "issGeneratePDF";
			this.issGeneratePDF.Size = new System.Drawing.Size(159, 22);
			this.issGeneratePDF.Text = "Generator PDF...";
			this.issGeneratePDF.Click += new System.EventHandler(this.issGeneratePDF_Click);
			// 
			// issEditor
			// 
			this.issEditor.Enabled = false;
			this.issEditor.Name = "issEditor";
			this.issEditor.Size = new System.Drawing.Size(159, 22);
			this.issEditor.Text = "Edytor...";
			this.issEditor.Click += new System.EventHandler(this.issEditor_Click);
			// 
			// issGeneral
			// 
			this.issGeneral.Enabled = false;
			this.issGeneral.Name = "issGeneral";
			this.issGeneral.Size = new System.Drawing.Size(159, 22);
			this.issGeneral.Text = "Ogólne...";
			this.issGeneral.Click += new System.EventHandler(this.issGeneral_Click);
			// 
			// gmProgram
			// 
			this.gmProgram.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.gmpInfo,
            this.gmpHelp,
            this.gmpUpdate});
			this.gmProgram.Name = "gmProgram";
			this.gmProgram.Padding = new System.Windows.Forms.Padding(6, 2, 6, 2);
			this.gmProgram.Size = new System.Drawing.Size(69, 23);
			this.gmProgram.Text = "&Program";
			// 
			// gmpInfo
			// 
			this.gmpInfo.Name = "gmpInfo";
			this.gmpInfo.ShortcutKeys = System.Windows.Forms.Keys.F1;
			this.gmpInfo.Size = new System.Drawing.Size(203, 22);
			this.gmpInfo.Text = "Informacje";
			this.gmpInfo.Click += new System.EventHandler(this.impInfo_Click);
			// 
			// gmpHelp
			// 
			this.gmpHelp.Enabled = false;
			this.gmpHelp.Name = "gmpHelp";
			this.gmpHelp.Size = new System.Drawing.Size(203, 22);
			this.gmpHelp.Text = "Pomoc";
			this.gmpHelp.Visible = false;
			this.gmpHelp.Click += new System.EventHandler(this.impHelp_Click);
			// 
			// gmpUpdate
			// 
			this.gmpUpdate.Name = "gmpUpdate";
			this.gmpUpdate.ShortcutKeys = System.Windows.Forms.Keys.F9;
			this.gmpUpdate.Size = new System.Drawing.Size(203, 22);
			this.gmpUpdate.Text = "Aktualizuj program...";
			this.gmpUpdate.Click += new System.EventHandler(this.gmpUpdate_Click);
			// 
			// icPage
			// 
			this.icPage.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.icpAddField,
            this.icpDeleteFields,
            this.toolStripSeparator12,
            this.icpPageColor,
            this.icpPageBack,
            this.icpPageClear,
            this.toolStripSeparator2,
            this.icpPrintColor,
            this.icpPrintImage,
            this.toolStripSeparator7,
            this.icpAddPage,
            this.icpDeletePage});
			this.icPage.Name = "cmPrevPage";
			this.icPage.Size = new System.Drawing.Size(180, 220);
			this.icPage.Opening += new System.ComponentModel.CancelEventHandler(this.icPage_Opening);
			// 
			// icpAddField
			// 
			this.icpAddField.Name = "icpAddField";
			this.icpAddField.Size = new System.Drawing.Size(179, 22);
			this.icpAddField.Text = "Dodaj pole";
			this.icpAddField.Click += new System.EventHandler(this.icpAddField_Click);
			// 
			// icpDeleteFields
			// 
			this.icpDeleteFields.Name = "icpDeleteFields";
			this.icpDeleteFields.Size = new System.Drawing.Size(179, 22);
			this.icpDeleteFields.Text = "Usuń wszystkie pola";
			this.icpDeleteFields.Click += new System.EventHandler(this.icpDeleteFields_Click);
			// 
			// toolStripSeparator12
			// 
			this.toolStripSeparator12.Name = "toolStripSeparator12";
			this.toolStripSeparator12.Size = new System.Drawing.Size(176, 6);
			// 
			// icpPageColor
			// 
			this.icpPageColor.Name = "icpPageColor";
			this.icpPageColor.Size = new System.Drawing.Size(179, 22);
			this.icpPageColor.Text = "Kolor tła strony...";
			this.icpPageColor.Click += new System.EventHandler(this.icpPageColor_Click);
			// 
			// icpPageBack
			// 
			this.icpPageBack.Enabled = false;
			this.icpPageBack.Name = "icpPageBack";
			this.icpPageBack.Size = new System.Drawing.Size(179, 22);
			this.icpPageBack.Text = "Obraz tła strony...";
			this.icpPageBack.Click += new System.EventHandler(this.pbPageBack_Click);
			// 
			// icpPageClear
			// 
			this.icpPageClear.Name = "icpPageClear";
			this.icpPageClear.Size = new System.Drawing.Size(179, 22);
			this.icpPageClear.Text = "Wyczyść tło";
			this.icpPageClear.Click += new System.EventHandler(this.icpPageClear_Click);
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(176, 6);
			// 
			// icpPrintColor
			// 
			this.icpPrintColor.CheckOnClick = true;
			this.icpPrintColor.Name = "icpPrintColor";
			this.icpPrintColor.Size = new System.Drawing.Size(179, 22);
			this.icpPrintColor.Text = "Rysuj kolor strony";
			this.icpPrintColor.CheckedChanged += new System.EventHandler(this.icpPrintColor_CheckedChanged);
			// 
			// icpPrintImage
			// 
			this.icpPrintImage.CheckOnClick = true;
			this.icpPrintImage.Enabled = false;
			this.icpPrintImage.Name = "icpPrintImage";
			this.icpPrintImage.Size = new System.Drawing.Size(179, 22);
			this.icpPrintImage.Text = "Rysuj obraz strony";
			this.icpPrintImage.CheckedChanged += new System.EventHandler(this.icpPrintImage_CheckedChanged);
			// 
			// toolStripSeparator7
			// 
			this.toolStripSeparator7.Name = "toolStripSeparator7";
			this.toolStripSeparator7.Size = new System.Drawing.Size(176, 6);
			// 
			// icpAddPage
			// 
			this.icpAddPage.Name = "icpAddPage";
			this.icpAddPage.Size = new System.Drawing.Size(179, 22);
			this.icpAddPage.Text = "Dodaj stronę";
			this.icpAddPage.Click += new System.EventHandler(this.icpAddPage_Click);
			// 
			// icpDeletePage
			// 
			this.icpDeletePage.Name = "icpDeletePage";
			this.icpDeletePage.Size = new System.Drawing.Size(179, 22);
			this.icpDeletePage.Text = "Usuń stronę";
			this.icpDeletePage.Click += new System.EventHandler(this.icpDeletePage_Click);
			// 
			// icLabel
			// 
			this.icLabel.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.iclBackColor,
            this.iclBackImage,
            this.iclClearBack,
            this.toolStripSeparator6,
            this.iclBackFromDb,
            this.iclPrintColor,
            this.iclPrintImage,
            this.toolStripSeparator8,
            this.iclFontColor,
            this.iclFontName,
            this.iclTextFromDb,
            this.iclPrintText,
            this.toolStripSeparator4,
            this.iclBorderColor,
            this.iclPrintBorder,
            this.toolStripSeparator5,
            this.iclDeleteField});
			this.icLabel.Name = "cmPrevLabel";
			this.icLabel.Size = new System.Drawing.Size(173, 314);
			this.icLabel.Opening += new System.ComponentModel.CancelEventHandler(this.icLabel_Opening);
			// 
			// iclBackColor
			// 
			this.iclBackColor.Name = "iclBackColor";
			this.iclBackColor.Size = new System.Drawing.Size(172, 22);
			this.iclBackColor.Text = "Kolor tła pola...";
			this.iclBackColor.Click += new System.EventHandler(this.pbBackColor_Click);
			// 
			// iclBackImage
			// 
			this.iclBackImage.Enabled = false;
			this.iclBackImage.Name = "iclBackImage";
			this.iclBackImage.Size = new System.Drawing.Size(172, 22);
			this.iclBackImage.Text = "Obraz tła pola...";
			this.iclBackImage.Click += new System.EventHandler(this.pbBackImage_Click);
			// 
			// iclClearBack
			// 
			this.iclClearBack.Name = "iclClearBack";
			this.iclClearBack.Size = new System.Drawing.Size(172, 22);
			this.iclClearBack.Text = "Wyczyść tło";
			this.iclClearBack.Click += new System.EventHandler(this.iclClearBack_Click);
			// 
			// toolStripSeparator6
			// 
			this.toolStripSeparator6.Name = "toolStripSeparator6";
			this.toolStripSeparator6.Size = new System.Drawing.Size(169, 6);
			// 
			// iclBackFromDb
			// 
			this.iclBackFromDb.CheckOnClick = true;
			this.iclBackFromDb.Enabled = false;
			this.iclBackFromDb.Name = "iclBackFromDb";
			this.iclBackFromDb.Size = new System.Drawing.Size(172, 22);
			this.iclBackFromDb.Text = "Obraz dynamiczny";
			this.iclBackFromDb.Click += new System.EventHandler(this.iclBackFromDb_Click);
			// 
			// iclPrintColor
			// 
			this.iclPrintColor.CheckOnClick = true;
			this.iclPrintColor.Name = "iclPrintColor";
			this.iclPrintColor.Size = new System.Drawing.Size(172, 22);
			this.iclPrintColor.Text = "Rysuj kolor pola";
			this.iclPrintColor.Click += new System.EventHandler(this.iclPrintColor_Click);
			// 
			// iclPrintImage
			// 
			this.iclPrintImage.CheckOnClick = true;
			this.iclPrintImage.Enabled = false;
			this.iclPrintImage.Name = "iclPrintImage";
			this.iclPrintImage.Size = new System.Drawing.Size(172, 22);
			this.iclPrintImage.Text = "Obraz statyczny";
			this.iclPrintImage.Click += new System.EventHandler(this.iclPrintImage_Click);
			// 
			// toolStripSeparator8
			// 
			this.toolStripSeparator8.Name = "toolStripSeparator8";
			this.toolStripSeparator8.Size = new System.Drawing.Size(169, 6);
			// 
			// iclFontColor
			// 
			this.iclFontColor.Name = "iclFontColor";
			this.iclFontColor.Size = new System.Drawing.Size(172, 22);
			this.iclFontColor.Text = "Kolor czcionki...";
			this.iclFontColor.Click += new System.EventHandler(this.pbFontColor_Click);
			// 
			// iclFontName
			// 
			this.iclFontName.Name = "iclFontName";
			this.iclFontName.Size = new System.Drawing.Size(172, 22);
			this.iclFontName.Text = "Zmień czcionkę...";
			this.iclFontName.Click += new System.EventHandler(this.pbFontName_Click);
			// 
			// iclTextFromDb
			// 
			this.iclTextFromDb.CheckOnClick = true;
			this.iclTextFromDb.Name = "iclTextFromDb";
			this.iclTextFromDb.Size = new System.Drawing.Size(172, 22);
			this.iclTextFromDb.Text = "Tekst dynamiczny";
			this.iclTextFromDb.Click += new System.EventHandler(this.iclTextFromDb_Click);
			// 
			// iclPrintText
			// 
			this.iclPrintText.CheckOnClick = true;
			this.iclPrintText.Name = "iclPrintText";
			this.iclPrintText.Size = new System.Drawing.Size(172, 22);
			this.iclPrintText.Text = "Tekst statyczny";
			this.iclPrintText.Click += new System.EventHandler(this.iclPrintText_Click);
			// 
			// toolStripSeparator4
			// 
			this.toolStripSeparator4.Name = "toolStripSeparator4";
			this.toolStripSeparator4.Size = new System.Drawing.Size(169, 6);
			// 
			// iclBorderColor
			// 
			this.iclBorderColor.Name = "iclBorderColor";
			this.iclBorderColor.Size = new System.Drawing.Size(172, 22);
			this.iclBorderColor.Text = "Kolor ramki...";
			this.iclBorderColor.Click += new System.EventHandler(this.pbBorderColor_Click);
			// 
			// iclPrintBorder
			// 
			this.iclPrintBorder.CheckOnClick = true;
			this.iclPrintBorder.Name = "iclPrintBorder";
			this.iclPrintBorder.Size = new System.Drawing.Size(172, 22);
			this.iclPrintBorder.Text = "Wyświetlaj ramke";
			this.iclPrintBorder.Click += new System.EventHandler(this.iclPrintBorder_Click);
			// 
			// toolStripSeparator5
			// 
			this.toolStripSeparator5.Name = "toolStripSeparator5";
			this.toolStripSeparator5.Size = new System.Drawing.Size(169, 6);
			// 
			// iclDeleteField
			// 
			this.iclDeleteField.Name = "iclDeleteField";
			this.iclDeleteField.Size = new System.Drawing.Size(172, 22);
			this.iclDeleteField.Text = "Usuń";
			this.iclDeleteField.Click += new System.EventHandler(this.iclDeleteField_Click);
			// 
			// gsImage
			// 
			this.gsImage.Filter = "JPEG (*.jpg)|*.jpg|PNG (*.png)|*.png|BMP (*.bmp)|*.bmp";
			// 
			// dpPreview
			// 
			this.dpPreview.AutoScroll = true;
			this.dpPreview.BackColor = System.Drawing.SystemColors.ScrollBar;
			this.dpPreview.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.dpPreview.Dock = System.Windows.Forms.DockStyle.Fill;
			this.dpPreview.Location = new System.Drawing.Point(0, 0);
			this.dpPreview.Margin = new System.Windows.Forms.Padding(0);
			this.dpPreview.Name = "dpPreview";
			this.dpPreview.Size = new System.Drawing.Size(416, 414);
			this.dpPreview.TabIndex = 1;
			this.dpPreview.MouseDown += new System.Windows.Forms.MouseEventHandler(this.dpPreview_MouseDown);
			this.dpPreview.Resize += new System.EventHandler(this.dpPreview_Resize);
			// 
			// dtButtonTable
			// 
			this.dtButtonTable.ColumnCount = 2;
			this.dtButtonTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.dtButtonTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.dtButtonTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.dtButtonTable.Controls.Add(this.dbGeneratePDF, 1, 0);
			this.dtButtonTable.Controls.Add(this.dbScan, 0, 0);
			this.dtButtonTable.Dock = System.Windows.Forms.DockStyle.Fill;
			this.dtButtonTable.Location = new System.Drawing.Point(428, 0);
			this.dtButtonTable.Margin = new System.Windows.Forms.Padding(1, 0, 6, 0);
			this.dtButtonTable.Name = "dtButtonTable";
			this.dtButtonTable.RowCount = 1;
			this.dtButtonTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.dtButtonTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
			this.dtButtonTable.Size = new System.Drawing.Size(250, 28);
			this.dtButtonTable.TabIndex = 2;
			// 
			// dbGeneratePDF
			// 
			this.dbGeneratePDF.Dock = System.Windows.Forms.DockStyle.Fill;
			this.dbGeneratePDF.Location = new System.Drawing.Point(128, 3);
			this.dbGeneratePDF.Margin = new System.Windows.Forms.Padding(3, 3, 0, 3);
			this.dbGeneratePDF.Name = "dbGeneratePDF";
			this.dbGeneratePDF.Size = new System.Drawing.Size(122, 22);
			this.dbGeneratePDF.TabIndex = 0;
			this.dbGeneratePDF.Text = "Generuj PDF";
			this.dbGeneratePDF.UseVisualStyleBackColor = true;
			this.dbGeneratePDF.Click += new System.EventHandler(this.dbGeneratePDF_Click);
			// 
			// dbScan
			// 
			this.dbScan.Dock = System.Windows.Forms.DockStyle.Fill;
			this.dbScan.Location = new System.Drawing.Point(0, 3);
			this.dbScan.Margin = new System.Windows.Forms.Padding(0, 3, 3, 3);
			this.dbScan.Name = "dbScan";
			this.dbScan.Size = new System.Drawing.Size(122, 22);
			this.dbScan.TabIndex = 1;
			this.dbScan.Text = "Szukaj błędów";
			this.dbScan.UseVisualStyleBackColor = true;
			this.dbScan.Click += new System.EventHandler(this.dbScan_Click);
			// 
			// dtvData
			// 
			this.dtvData.Dock = System.Windows.Forms.DockStyle.Fill;
			this.dtvData.FullRowSelect = true;
			this.dtvData.HideSelection = false;
			this.dtvData.Location = new System.Drawing.Point(0, 0);
			this.dtvData.Margin = new System.Windows.Forms.Padding(0);
			this.dtvData.Name = "dtvData";
			this.dtvData.ShowLines = false;
			this.dtvData.ShowPlusMinus = false;
			this.dtvData.ShowRootLines = false;
			this.dtvData.Size = new System.Drawing.Size(252, 414);
			this.dtvData.TabIndex = 3;
			this.dtvData.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.dtvData_AfterSelect);
			// 
			// gsOpenFile
			// 
			this.gsOpenFile.Filter = "CSV (*.csv)|*.csv";
			// 
			// pbSave
			// 
			this.pbSave.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pbSave.Location = new System.Drawing.Point(128, 3);
			this.pbSave.Margin = new System.Windows.Forms.Padding(3, 3, 0, 3);
			this.pbSave.Name = "pbSave";
			this.pbSave.Size = new System.Drawing.Size(122, 22);
			this.pbSave.TabIndex = 5;
			this.pbSave.Text = "Zapisz";
			this.pbSave.UseVisualStyleBackColor = true;
			this.pbSave.Click += new System.EventHandler(this.pbSave_Click);
			// 
			// pbLoadData
			// 
			this.pbLoadData.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pbLoadData.Location = new System.Drawing.Point(0, 3);
			this.pbLoadData.Margin = new System.Windows.Forms.Padding(0, 3, 3, 3);
			this.pbLoadData.Name = "pbLoadData";
			this.pbLoadData.Size = new System.Drawing.Size(122, 22);
			this.pbLoadData.TabIndex = 4;
			this.pbLoadData.Text = "Wczytaj dane";
			this.pbLoadData.UseVisualStyleBackColor = true;
			this.pbLoadData.Click += new System.EventHandler(this.pbLoadData_Click);
			// 
			// ptInfoControls
			// 
			this.ptInfoControls.ColumnCount = 3;
			this.ptInfoControls.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
			this.ptInfoControls.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.ptInfoControls.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
			this.ptInfoControls.Controls.Add(this.pflPage, 0, 0);
			this.ptInfoControls.Controls.Add(this.plStatus, 0, 0);
			this.ptInfoControls.Controls.Add(this.pcbScale, 0, 0);
			this.ptInfoControls.Dock = System.Windows.Forms.DockStyle.Fill;
			this.ptInfoControls.Location = new System.Drawing.Point(0, 0);
			this.ptInfoControls.Margin = new System.Windows.Forms.Padding(0, 0, 3, 0);
			this.ptInfoControls.Name = "ptInfoControls";
			this.ptInfoControls.RowCount = 1;
			this.ptInfoControls.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.ptInfoControls.Size = new System.Drawing.Size(424, 28);
			this.ptInfoControls.TabIndex = 0;
			// 
			// pflPage
			// 
			this.pflPage.Controls.Add(this.pnPage);
			this.pflPage.Controls.Add(this.plPage);
			this.pflPage.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pflPage.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
			this.pflPage.Location = new System.Drawing.Point(324, 0);
			this.pflPage.Margin = new System.Windows.Forms.Padding(0);
			this.pflPage.Name = "pflPage";
			this.pflPage.Size = new System.Drawing.Size(100, 28);
			this.pflPage.TabIndex = 12;
			// 
			// pnPage
			// 
			this.pnPage.Location = new System.Drawing.Point(62, 5);
			this.pnPage.Margin = new System.Windows.Forms.Padding(3, 5, 3, 3);
			this.pnPage.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.pnPage.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.pnPage.Name = "pnPage";
			this.pnPage.Size = new System.Drawing.Size(35, 20);
			this.pnPage.TabIndex = 0;
			this.pnPage.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.pnPage.ValueChanged += new System.EventHandler(this.pnPage_ValueChanged);
			// 
			// plPage
			// 
			this.plPage.AutoSize = true;
			this.plPage.Location = new System.Drawing.Point(18, 8);
			this.plPage.Margin = new System.Windows.Forms.Padding(3, 8, 0, 0);
			this.plPage.Name = "plPage";
			this.plPage.Size = new System.Drawing.Size(41, 13);
			this.plPage.TabIndex = 1;
			this.plPage.Text = "Strona:";
			// 
			// plStatus
			// 
			this.plStatus.AutoEllipsis = true;
			this.plStatus.AutoSize = true;
			this.plStatus.Dock = System.Windows.Forms.DockStyle.Fill;
			this.plStatus.Location = new System.Drawing.Point(83, 0);
			this.plStatus.Name = "plStatus";
			this.plStatus.Size = new System.Drawing.Size(238, 28);
			this.plStatus.TabIndex = 11;
			this.plStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// pcbScale
			// 
			this.pcbScale.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pcbScale.Enabled = false;
			this.pcbScale.FormattingEnabled = true;
			this.pcbScale.Items.AddRange(new object[] {
            "50",
            "75",
            "100",
            "150",
            "200",
            "300"});
			this.pcbScale.Location = new System.Drawing.Point(6, 4);
			this.pcbScale.Margin = new System.Windows.Forms.Padding(6, 4, 3, 3);
			this.pcbScale.Name = "pcbScale";
			this.pcbScale.Size = new System.Drawing.Size(71, 21);
			this.pcbScale.TabIndex = 10;
			this.pcbScale.SelectedIndexChanged += new System.EventHandler(this.pcbScale_SelectedIndexChanged);
			this.pcbScale.KeyDown += new System.Windows.Forms.KeyEventHandler(this.pcbScale_KeyDown);
			this.pcbScale.Leave += new System.EventHandler(this.pcbScale_Leave);
			this.pcbScale.MouseEnter += new System.EventHandler(this.pcbScale_MouseEnter);
			this.pcbScale.MouseLeave += new System.EventHandler(this.plStatus_ClearText);
			// 
			// ppPreview
			// 
			this.ppPreview.AutoScroll = true;
			this.ppPreview.BackColor = System.Drawing.SystemColors.ScrollBar;
			this.ppPreview.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.ppPreview.Dock = System.Windows.Forms.DockStyle.Fill;
			this.ppPreview.Location = new System.Drawing.Point(0, 0);
			this.ppPreview.Margin = new System.Windows.Forms.Padding(0);
			this.ppPreview.Name = "ppPreview";
			this.ppPreview.Size = new System.Drawing.Size(416, 414);
			this.ppPreview.TabIndex = 1;
			this.ppPreview.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ppPanelContainer_MouseDown);
			this.ppPreview.Resize += new System.EventHandler(this.ppPanelContainer_Resize);
			// 
			// ptPatternDetails
			// 
			this.ptPatternDetails.ColumnCount = 1;
			this.ptPatternDetails.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.ptPatternDetails.Controls.Add(this.icPatMenu, 0, 0);
			this.ptPatternDetails.Controls.Add(this.ppDetailsPanel, 0, 1);
			this.ptPatternDetails.Dock = System.Windows.Forms.DockStyle.Fill;
			this.ptPatternDetails.Location = new System.Drawing.Point(0, 0);
			this.ptPatternDetails.Margin = new System.Windows.Forms.Padding(3, 6, 6, 3);
			this.ptPatternDetails.Name = "ptPatternDetails";
			this.ptPatternDetails.RowCount = 2;
			this.ptPatternDetails.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
			this.ptPatternDetails.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.ptPatternDetails.Size = new System.Drawing.Size(252, 414);
			this.ptPatternDetails.TabIndex = 5;
			// 
			// icPatMenu
			// 
			this.icPatMenu.BackColor = System.Drawing.SystemColors.ControlLight;
			this.ptPatternDetails.SetColumnSpan(this.icPatMenu, 2);
			this.icPatMenu.Dock = System.Windows.Forms.DockStyle.Fill;
			this.icPatMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.icpmFieldDetails,
            this.icpmDetails,
            this.icpmPageDetails});
			this.icPatMenu.Location = new System.Drawing.Point(3, 3);
			this.icPatMenu.Margin = new System.Windows.Forms.Padding(3, 3, 3, 6);
			this.icPatMenu.Name = "icPatMenu";
			this.icPatMenu.Size = new System.Drawing.Size(246, 26);
			this.icPatMenu.TabIndex = 68;
			this.icPatMenu.Text = "icPatMenu";
			// 
			// icpmFieldDetails
			// 
			this.icpmFieldDetails.Enabled = false;
			this.icpmFieldDetails.Name = "icpmFieldDetails";
			this.icpmFieldDetails.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.Q)));
			this.icpmFieldDetails.Size = new System.Drawing.Size(42, 22);
			this.icpmFieldDetails.Text = "Pole";
			this.icpmFieldDetails.Click += new System.EventHandler(this.icpmFieldDetails_Click);
			// 
			// icpmDetails
			// 
			this.icpmDetails.Name = "icpmDetails";
			this.icpmDetails.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.W)));
			this.icpmDetails.Size = new System.Drawing.Size(70, 22);
			this.icpmDetails.Text = "Szczegóły";
			this.icpmDetails.Click += new System.EventHandler(this.icpmDetails_Click);
			// 
			// icpmPageDetails
			// 
			this.icpmPageDetails.Margin = new System.Windows.Forms.Padding(0, 0, 3, 0);
			this.icpmPageDetails.Name = "icpmPageDetails";
			this.icpmPageDetails.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.E)));
			this.icpmPageDetails.Size = new System.Drawing.Size(53, 22);
			this.icpmPageDetails.Text = "Strona";
			this.icpmPageDetails.Click += new System.EventHandler(this.icpmPageDetails_Click);
			// 
			// ppDetailsPanel
			// 
			this.ppDetailsPanel.Controls.Add(this.ptFieldDetails);
			this.ppDetailsPanel.Controls.Add(this.ptDetails);
			this.ppDetailsPanel.Controls.Add(this.ptPageDetails);
			this.ppDetailsPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.ppDetailsPanel.Location = new System.Drawing.Point(0, 35);
			this.ppDetailsPanel.Margin = new System.Windows.Forms.Padding(0);
			this.ppDetailsPanel.Name = "ppDetailsPanel";
			this.ppDetailsPanel.Size = new System.Drawing.Size(252, 379);
			this.ppDetailsPanel.TabIndex = 69;
			// 
			// ptFieldDetails
			// 
			this.ptFieldDetails.AutoSize = true;
			this.ptFieldDetails.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.ptFieldDetails.ColumnCount = 2;
			this.ptFieldDetails.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.ptFieldDetails.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.ptFieldDetails.Controls.Add(this.pcbTextTransform, 0, 15);
			this.ptFieldDetails.Controls.Add(this.plTextTransform, 0, 14);
			this.ptFieldDetails.Controls.Add(this.plHeight, 1, 3);
			this.ptFieldDetails.Controls.Add(this.pnBorderSize, 1, 15);
			this.ptFieldDetails.Controls.Add(this.pnPadding, 1, 13);
			this.ptFieldDetails.Controls.Add(this.plPadding, 1, 12);
			this.ptFieldDetails.Controls.Add(this.plTextPosition, 0, 12);
			this.ptFieldDetails.Controls.Add(this.pbFontName, 1, 10);
			this.ptFieldDetails.Controls.Add(this.ptbFontName, 0, 10);
			this.ptFieldDetails.Controls.Add(this.pbBackImage, 1, 8);
			this.ptFieldDetails.Controls.Add(this.ptbBackImage, 0, 8);
			this.ptFieldDetails.Controls.Add(this.pbBackColor, 1, 7);
			this.ptFieldDetails.Controls.Add(this.ptbBackColor, 0, 7);
			this.ptFieldDetails.Controls.Add(this.pbFontColor, 1, 11);
			this.ptFieldDetails.Controls.Add(this.ptbFontColor, 0, 11);
			this.ptFieldDetails.Controls.Add(this.ptbName, 0, 0);
			this.ptFieldDetails.Controls.Add(this.pnPositionY, 1, 2);
			this.ptFieldDetails.Controls.Add(this.pnPositionX, 0, 2);
			this.ptFieldDetails.Controls.Add(this.plPositionX, 0, 1);
			this.ptFieldDetails.Controls.Add(this.plWidth, 0, 3);
			this.ptFieldDetails.Controls.Add(this.pnWidth, 0, 4);
			this.ptFieldDetails.Controls.Add(this.pnHeight, 1, 4);
			this.ptFieldDetails.Controls.Add(this.plBorderColor, 0, 5);
			this.ptFieldDetails.Controls.Add(this.ptbBorderColor, 0, 6);
			this.ptFieldDetails.Controls.Add(this.pbBorderColor, 1, 6);
			this.ptFieldDetails.Controls.Add(this.plFont, 0, 9);
			this.ptFieldDetails.Controls.Add(this.plBorderWidth, 1, 14);
			this.ptFieldDetails.Controls.Add(this.pcbTextAlign, 0, 13);
			this.ptFieldDetails.Controls.Add(this.plPositionY, 1, 1);
			this.ptFieldDetails.Dock = System.Windows.Forms.DockStyle.Fill;
			this.ptFieldDetails.GrowStyle = System.Windows.Forms.TableLayoutPanelGrowStyle.FixedSize;
			this.ptFieldDetails.Location = new System.Drawing.Point(0, 0);
			this.ptFieldDetails.Margin = new System.Windows.Forms.Padding(0);
			this.ptFieldDetails.Name = "ptFieldDetails";
			this.ptFieldDetails.RowCount = 18;
			this.ptFieldDetails.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
			this.ptFieldDetails.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.ptFieldDetails.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
			this.ptFieldDetails.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.ptFieldDetails.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
			this.ptFieldDetails.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.ptFieldDetails.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
			this.ptFieldDetails.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
			this.ptFieldDetails.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
			this.ptFieldDetails.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.ptFieldDetails.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
			this.ptFieldDetails.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
			this.ptFieldDetails.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.ptFieldDetails.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
			this.ptFieldDetails.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.ptFieldDetails.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
			this.ptFieldDetails.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
			this.ptFieldDetails.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.ptFieldDetails.Size = new System.Drawing.Size(252, 379);
			this.ptFieldDetails.TabIndex = 7;
			// 
			// pcbTextTransform
			// 
			this.pcbTextTransform.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pcbTextTransform.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.pcbTextTransform.Enabled = false;
			this.pcbTextTransform.FormattingEnabled = true;
			this.pcbTextTransform.Items.AddRange(new object[] {
            "Nie zmieniaj",
            "Duże litery",
            "Małe litery",
            "Kapitaliki"});
			this.pcbTextTransform.Location = new System.Drawing.Point(3, 363);
			this.pcbTextTransform.Name = "pcbTextTransform";
			this.pcbTextTransform.Size = new System.Drawing.Size(120, 21);
			this.pcbTextTransform.TabIndex = 74;
			this.pcbTextTransform.SelectedIndexChanged += new System.EventHandler(this.pcbTextTransform_SelectedIndexChanged);
			// 
			// plTextTransform
			// 
			this.plTextTransform.AutoSize = true;
			this.plTextTransform.Dock = System.Windows.Forms.DockStyle.Fill;
			this.plTextTransform.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.plTextTransform.Location = new System.Drawing.Point(3, 340);
			this.plTextTransform.Name = "plTextTransform";
			this.plTextTransform.Size = new System.Drawing.Size(120, 20);
			this.plTextTransform.TabIndex = 73;
			this.plTextTransform.Text = "Wyświetlanie tekstu:";
			this.plTextTransform.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// plHeight
			// 
			this.plHeight.AutoSize = true;
			this.plHeight.Dock = System.Windows.Forms.DockStyle.Fill;
			this.plHeight.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.plHeight.Location = new System.Drawing.Point(129, 70);
			this.plHeight.Name = "plHeight";
			this.plHeight.Size = new System.Drawing.Size(120, 20);
			this.plHeight.TabIndex = 72;
			this.plHeight.Text = "Wysokość:";
			this.plHeight.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// pnBorderSize
			// 
			this.pnBorderSize.DecimalPlaces = 1;
			this.pnBorderSize.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnBorderSize.Enabled = false;
			this.pnBorderSize.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
			this.pnBorderSize.Location = new System.Drawing.Point(129, 363);
			this.pnBorderSize.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
			this.pnBorderSize.Name = "pnBorderSize";
			this.pnBorderSize.Size = new System.Drawing.Size(120, 20);
			this.pnBorderSize.TabIndex = 65;
			this.pnBorderSize.ValueChanged += new System.EventHandler(this.pnBorderSize_ValueChanged);
			// 
			// pnPadding
			// 
			this.pnPadding.DecimalPlaces = 1;
			this.pnPadding.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnPadding.Enabled = false;
			this.pnPadding.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
			this.pnPadding.Location = new System.Drawing.Point(129, 318);
			this.pnPadding.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
			this.pnPadding.Name = "pnPadding";
			this.pnPadding.Size = new System.Drawing.Size(120, 20);
			this.pnPadding.TabIndex = 63;
			this.pnPadding.ValueChanged += new System.EventHandler(this.pnPadding_ValueChanged);
			// 
			// plPadding
			// 
			this.plPadding.AutoSize = true;
			this.plPadding.Dock = System.Windows.Forms.DockStyle.Fill;
			this.plPadding.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.plPadding.Location = new System.Drawing.Point(129, 295);
			this.plPadding.Name = "plPadding";
			this.plPadding.Size = new System.Drawing.Size(120, 20);
			this.plPadding.TabIndex = 61;
			this.plPadding.Text = "Margines:";
			this.plPadding.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// plTextPosition
			// 
			this.plTextPosition.AutoSize = true;
			this.plTextPosition.Dock = System.Windows.Forms.DockStyle.Fill;
			this.plTextPosition.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.plTextPosition.Location = new System.Drawing.Point(3, 295);
			this.plTextPosition.Name = "plTextPosition";
			this.plTextPosition.Size = new System.Drawing.Size(120, 20);
			this.plTextPosition.TabIndex = 60;
			this.plTextPosition.Text = "Położenie tekstu:";
			this.plTextPosition.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// pbFontName
			// 
			this.pbFontName.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pbFontName.Enabled = false;
			this.pbFontName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.pbFontName.Location = new System.Drawing.Point(129, 242);
			this.pbFontName.Margin = new System.Windows.Forms.Padding(3, 3, 3, 2);
			this.pbFontName.Name = "pbFontName";
			this.pbFontName.Size = new System.Drawing.Size(120, 23);
			this.pbFontName.TabIndex = 59;
			this.pbFontName.Text = "Nazwa";
			this.pbFontName.UseVisualStyleBackColor = true;
			this.pbFontName.Click += new System.EventHandler(this.pbFontName_Click);
			// 
			// ptbFontName
			// 
			this.ptbFontName.Dock = System.Windows.Forms.DockStyle.Fill;
			this.ptbFontName.Enabled = false;
			this.ptbFontName.Location = new System.Drawing.Point(3, 244);
			this.ptbFontName.Margin = new System.Windows.Forms.Padding(3, 5, 3, 3);
			this.ptbFontName.Name = "ptbFontName";
			this.ptbFontName.Size = new System.Drawing.Size(120, 20);
			this.ptbFontName.TabIndex = 57;
			// 
			// pbBackImage
			// 
			this.pbBackImage.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pbBackImage.Enabled = false;
			this.pbBackImage.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.pbBackImage.Location = new System.Drawing.Point(129, 194);
			this.pbBackImage.Margin = new System.Windows.Forms.Padding(3, 3, 3, 2);
			this.pbBackImage.Name = "pbBackImage";
			this.pbBackImage.Size = new System.Drawing.Size(120, 23);
			this.pbBackImage.TabIndex = 52;
			this.pbBackImage.Text = "Obraz";
			this.pbBackImage.UseVisualStyleBackColor = true;
			this.pbBackImage.Click += new System.EventHandler(this.pbBackImage_Click);
			// 
			// ptbBackImage
			// 
			this.ptbBackImage.Dock = System.Windows.Forms.DockStyle.Fill;
			this.ptbBackImage.Enabled = false;
			this.ptbBackImage.Location = new System.Drawing.Point(3, 196);
			this.ptbBackImage.Margin = new System.Windows.Forms.Padding(3, 5, 3, 3);
			this.ptbBackImage.Name = "ptbBackImage";
			this.ptbBackImage.Size = new System.Drawing.Size(120, 20);
			this.ptbBackImage.TabIndex = 51;
			// 
			// pbBackColor
			// 
			this.pbBackColor.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pbBackColor.Enabled = false;
			this.pbBackColor.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.pbBackColor.Location = new System.Drawing.Point(129, 166);
			this.pbBackColor.Margin = new System.Windows.Forms.Padding(3, 3, 3, 2);
			this.pbBackColor.Name = "pbBackColor";
			this.pbBackColor.Size = new System.Drawing.Size(120, 23);
			this.pbBackColor.TabIndex = 50;
			this.pbBackColor.Text = "Kolor";
			this.pbBackColor.UseVisualStyleBackColor = true;
			this.pbBackColor.Click += new System.EventHandler(this.pbBackColor_Click);
			// 
			// ptbBackColor
			// 
			this.ptbBackColor.Dock = System.Windows.Forms.DockStyle.Fill;
			this.ptbBackColor.Enabled = false;
			this.ptbBackColor.Location = new System.Drawing.Point(3, 168);
			this.ptbBackColor.Margin = new System.Windows.Forms.Padding(3, 5, 3, 3);
			this.ptbBackColor.Name = "ptbBackColor";
			this.ptbBackColor.Size = new System.Drawing.Size(120, 20);
			this.ptbBackColor.TabIndex = 49;
			// 
			// pbFontColor
			// 
			this.pbFontColor.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pbFontColor.Enabled = false;
			this.pbFontColor.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.pbFontColor.Location = new System.Drawing.Point(129, 270);
			this.pbFontColor.Margin = new System.Windows.Forms.Padding(3, 3, 3, 2);
			this.pbFontColor.Name = "pbFontColor";
			this.pbFontColor.Size = new System.Drawing.Size(120, 23);
			this.pbFontColor.TabIndex = 35;
			this.pbFontColor.Text = "Kolor";
			this.pbFontColor.UseVisualStyleBackColor = true;
			this.pbFontColor.Click += new System.EventHandler(this.pbFontColor_Click);
			// 
			// ptbFontColor
			// 
			this.ptbFontColor.Dock = System.Windows.Forms.DockStyle.Fill;
			this.ptbFontColor.Enabled = false;
			this.ptbFontColor.Location = new System.Drawing.Point(3, 272);
			this.ptbFontColor.Margin = new System.Windows.Forms.Padding(3, 5, 3, 3);
			this.ptbFontColor.Name = "ptbFontColor";
			this.ptbFontColor.Size = new System.Drawing.Size(120, 20);
			this.ptbFontColor.TabIndex = 34;
			// 
			// ptbName
			// 
			this.ptFieldDetails.SetColumnSpan(this.ptbName, 2);
			this.ptbName.Dock = System.Windows.Forms.DockStyle.Fill;
			this.ptbName.Enabled = false;
			this.ptbName.Location = new System.Drawing.Point(3, 3);
			this.ptbName.MaxLength = 127;
			this.ptbName.Name = "ptbName";
			this.ptbName.Size = new System.Drawing.Size(246, 20);
			this.ptbName.TabIndex = 0;
			this.ptbName.TextChanged += new System.EventHandler(this.ptbName_TextChanged);
			this.ptbName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ptbName_KeyPress);
			this.ptbName.Leave += new System.EventHandler(this.ptbName_Leave);
			// 
			// pnPositionY
			// 
			this.pnPositionY.DecimalPlaces = 1;
			this.pnPositionY.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnPositionY.Enabled = false;
			this.pnPositionY.Location = new System.Drawing.Point(129, 48);
			this.pnPositionY.Maximum = new decimal(new int[] {
            5000,
            0,
            0,
            0});
			this.pnPositionY.Name = "pnPositionY";
			this.pnPositionY.Size = new System.Drawing.Size(120, 20);
			this.pnPositionY.TabIndex = 2;
			this.pnPositionY.ValueChanged += new System.EventHandler(this.pnPositionY_ValueChanged);
			// 
			// pnPositionX
			// 
			this.pnPositionX.DecimalPlaces = 1;
			this.pnPositionX.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnPositionX.Enabled = false;
			this.pnPositionX.Location = new System.Drawing.Point(3, 48);
			this.pnPositionX.Maximum = new decimal(new int[] {
            5000,
            0,
            0,
            0});
			this.pnPositionX.Name = "pnPositionX";
			this.pnPositionX.Size = new System.Drawing.Size(120, 20);
			this.pnPositionX.TabIndex = 1;
			this.pnPositionX.ValueChanged += new System.EventHandler(this.pnPositionX_ValueChanged);
			// 
			// plPositionX
			// 
			this.plPositionX.AutoSize = true;
			this.plPositionX.Dock = System.Windows.Forms.DockStyle.Fill;
			this.plPositionX.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.plPositionX.Location = new System.Drawing.Point(3, 25);
			this.plPositionX.Name = "plPositionX";
			this.plPositionX.Size = new System.Drawing.Size(120, 20);
			this.plPositionX.TabIndex = 4;
			this.plPositionX.Text = "Pozycja X:";
			this.plPositionX.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// plWidth
			// 
			this.plWidth.AutoSize = true;
			this.plWidth.Dock = System.Windows.Forms.DockStyle.Fill;
			this.plWidth.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.plWidth.Location = new System.Drawing.Point(3, 70);
			this.plWidth.Name = "plWidth";
			this.plWidth.Size = new System.Drawing.Size(120, 20);
			this.plWidth.TabIndex = 6;
			this.plWidth.Text = "Szerokość:";
			this.plWidth.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// pnWidth
			// 
			this.pnWidth.DecimalPlaces = 1;
			this.pnWidth.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnWidth.Enabled = false;
			this.pnWidth.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.pnWidth.Location = new System.Drawing.Point(3, 93);
			this.pnWidth.Maximum = new decimal(new int[] {
            5000,
            0,
            0,
            0});
			this.pnWidth.Name = "pnWidth";
			this.pnWidth.Size = new System.Drawing.Size(120, 20);
			this.pnWidth.TabIndex = 3;
			this.pnWidth.ValueChanged += new System.EventHandler(this.pnWidth_ValueChanged);
			// 
			// pnHeight
			// 
			this.pnHeight.DecimalPlaces = 1;
			this.pnHeight.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnHeight.Enabled = false;
			this.pnHeight.Location = new System.Drawing.Point(129, 93);
			this.pnHeight.Maximum = new decimal(new int[] {
            5000,
            0,
            0,
            0});
			this.pnHeight.Name = "pnHeight";
			this.pnHeight.Size = new System.Drawing.Size(120, 20);
			this.pnHeight.TabIndex = 4;
			this.pnHeight.ValueChanged += new System.EventHandler(this.pnHeight_ValueChanged);
			// 
			// plBorderColor
			// 
			this.plBorderColor.AutoSize = true;
			this.ptFieldDetails.SetColumnSpan(this.plBorderColor, 2);
			this.plBorderColor.Dock = System.Windows.Forms.DockStyle.Fill;
			this.plBorderColor.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.plBorderColor.Location = new System.Drawing.Point(3, 115);
			this.plBorderColor.Name = "plBorderColor";
			this.plBorderColor.Size = new System.Drawing.Size(246, 20);
			this.plBorderColor.TabIndex = 10;
			this.plBorderColor.Text = "Wygląd:";
			this.plBorderColor.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// ptbBorderColor
			// 
			this.ptbBorderColor.Dock = System.Windows.Forms.DockStyle.Fill;
			this.ptbBorderColor.Enabled = false;
			this.ptbBorderColor.Location = new System.Drawing.Point(3, 140);
			this.ptbBorderColor.Margin = new System.Windows.Forms.Padding(3, 5, 3, 3);
			this.ptbBorderColor.Name = "ptbBorderColor";
			this.ptbBorderColor.Size = new System.Drawing.Size(120, 20);
			this.ptbBorderColor.TabIndex = 5;
			// 
			// pbBorderColor
			// 
			this.pbBorderColor.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pbBorderColor.Enabled = false;
			this.pbBorderColor.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.pbBorderColor.Location = new System.Drawing.Point(129, 138);
			this.pbBorderColor.Margin = new System.Windows.Forms.Padding(3, 3, 3, 2);
			this.pbBorderColor.Name = "pbBorderColor";
			this.pbBorderColor.Size = new System.Drawing.Size(120, 23);
			this.pbBorderColor.TabIndex = 6;
			this.pbBorderColor.Text = "Ramka";
			this.pbBorderColor.UseVisualStyleBackColor = true;
			this.pbBorderColor.Click += new System.EventHandler(this.pbBorderColor_Click);
			// 
			// plFont
			// 
			this.plFont.AutoSize = true;
			this.ptFieldDetails.SetColumnSpan(this.plFont, 2);
			this.plFont.Dock = System.Windows.Forms.DockStyle.Fill;
			this.plFont.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.plFont.Location = new System.Drawing.Point(3, 219);
			this.plFont.Name = "plFont";
			this.plFont.Size = new System.Drawing.Size(246, 20);
			this.plFont.TabIndex = 53;
			this.plFont.Text = "Czcionka:";
			this.plFont.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// plBorderWidth
			// 
			this.plBorderWidth.AutoSize = true;
			this.plBorderWidth.Dock = System.Windows.Forms.DockStyle.Fill;
			this.plBorderWidth.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.plBorderWidth.Location = new System.Drawing.Point(129, 340);
			this.plBorderWidth.Name = "plBorderWidth";
			this.plBorderWidth.Size = new System.Drawing.Size(120, 20);
			this.plBorderWidth.TabIndex = 64;
			this.plBorderWidth.Text = "Grubość ramki:";
			this.plBorderWidth.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// pcbTextAlign
			// 
			this.pcbTextAlign.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pcbTextAlign.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.pcbTextAlign.Enabled = false;
			this.pcbTextAlign.FormattingEnabled = true;
			this.pcbTextAlign.Items.AddRange(new object[] {
            "Góra-Lewo",
            "Góra-Środek",
            "Góra-Prawo",
            "Środek-Lewo",
            "Środek",
            "Środek-Prawo",
            "Dół-Lewo",
            "Dół-Środek",
            "Dół-Prawo"});
			this.pcbTextAlign.Location = new System.Drawing.Point(3, 318);
			this.pcbTextAlign.Name = "pcbTextAlign";
			this.pcbTextAlign.Size = new System.Drawing.Size(120, 21);
			this.pcbTextAlign.TabIndex = 68;
			this.pcbTextAlign.SelectedIndexChanged += new System.EventHandler(this.pcbTextAlign_SelectedIndexChanged);
			// 
			// plPositionY
			// 
			this.plPositionY.AutoSize = true;
			this.plPositionY.Dock = System.Windows.Forms.DockStyle.Fill;
			this.plPositionY.Location = new System.Drawing.Point(129, 25);
			this.plPositionY.Name = "plPositionY";
			this.plPositionY.Size = new System.Drawing.Size(120, 20);
			this.plPositionY.TabIndex = 71;
			this.plPositionY.Text = "Pozycja Y:";
			this.plPositionY.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// ptDetails
			// 
			this.ptDetails.ColumnCount = 2;
			this.ptDetails.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.ptDetails.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.ptDetails.Controls.Add(this.pcbdAddMargin, 0, 11);
			this.ptDetails.Controls.Add(this.pldAdditionalMargin, 0, 10);
			this.ptDetails.Controls.Add(this.pcbPosAlign, 0, 9);
			this.ptDetails.Controls.Add(this.plStickPoint, 0, 8);
			this.ptDetails.Controls.Add(this.pcbUseImageMargin, 0, 7);
			this.ptDetails.Controls.Add(this.pcbDrawFrameOutside, 0, 6);
			this.ptDetails.Controls.Add(this.plImageSettings, 0, 4);
			this.ptDetails.Controls.Add(this.pcbDynImage, 1, 3);
			this.ptDetails.Controls.Add(this.pcbStatText, 0, 3);
			this.ptDetails.Controls.Add(this.pcbDrawColor, 1, 1);
			this.ptDetails.Controls.Add(this.pcbDynText, 0, 2);
			this.ptDetails.Controls.Add(this.plPDFGenerate, 0, 0);
			this.ptDetails.Controls.Add(this.pcbShowFrame, 0, 1);
			this.ptDetails.Controls.Add(this.pcbStatImage, 1, 2);
			this.ptDetails.Controls.Add(this.pcxImageSet, 0, 5);
			this.ptDetails.Controls.Add(this.pcbMarginLR, 0, 12);
			this.ptDetails.Controls.Add(this.pcbMarginTB, 1, 12);
			this.ptDetails.Dock = System.Windows.Forms.DockStyle.Fill;
			this.ptDetails.Location = new System.Drawing.Point(0, 0);
			this.ptDetails.Margin = new System.Windows.Forms.Padding(0);
			this.ptDetails.Name = "ptDetails";
			this.ptDetails.RowCount = 14;
			this.ptDetails.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.ptDetails.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
			this.ptDetails.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
			this.ptDetails.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
			this.ptDetails.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.ptDetails.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
			this.ptDetails.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
			this.ptDetails.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
			this.ptDetails.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.ptDetails.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
			this.ptDetails.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.ptDetails.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
			this.ptDetails.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
			this.ptDetails.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.ptDetails.Size = new System.Drawing.Size(252, 379);
			this.ptDetails.TabIndex = 5;
			this.ptDetails.Visible = false;
			// 
			// pcbdAddMargin
			// 
			this.pcbdAddMargin.AutoSize = true;
			this.ptDetails.SetColumnSpan(this.pcbdAddMargin, 2);
			this.pcbdAddMargin.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pcbdAddMargin.Location = new System.Drawing.Point(3, 260);
			this.pcbdAddMargin.Margin = new System.Windows.Forms.Padding(3, 5, 3, 3);
			this.pcbdAddMargin.Name = "pcbdAddMargin";
			this.pcbdAddMargin.Size = new System.Drawing.Size(246, 17);
			this.pcbdAddMargin.TabIndex = 76;
			this.pcbdAddMargin.Text = "Zastosuj dodatkowy margines tekstu";
			this.pcbdAddMargin.UseVisualStyleBackColor = true;
			this.pcbdAddMargin.Visible = false;
			this.pcbdAddMargin.CheckedChanged += new System.EventHandler(this.pcbdAddMargin_CheckedChanged);
			// 
			// pldAdditionalMargin
			// 
			this.pldAdditionalMargin.AutoSize = true;
			this.ptDetails.SetColumnSpan(this.pldAdditionalMargin, 2);
			this.pldAdditionalMargin.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pldAdditionalMargin.Location = new System.Drawing.Point(3, 235);
			this.pldAdditionalMargin.Name = "pldAdditionalMargin";
			this.pldAdditionalMargin.Size = new System.Drawing.Size(246, 20);
			this.pldAdditionalMargin.TabIndex = 71;
			this.pldAdditionalMargin.Text = "Dodatkowy margines tekstu:";
			this.pldAdditionalMargin.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			this.pldAdditionalMargin.Visible = false;
			// 
			// pcbPosAlign
			// 
			this.ptDetails.SetColumnSpan(this.pcbPosAlign, 2);
			this.pcbPosAlign.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pcbPosAlign.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.pcbPosAlign.Enabled = false;
			this.pcbPosAlign.FormattingEnabled = true;
			this.pcbPosAlign.Items.AddRange(new object[] {
            "Góra-Lewo (lewy górny róg)",
            "Góra-Prawo (prawy górny róg)",
            "Dół-Lewo (lewy dolny róg)",
            "Dół-Prawo (prawy dolny róg)"});
			this.pcbPosAlign.Location = new System.Drawing.Point(3, 213);
			this.pcbPosAlign.Name = "pcbPosAlign";
			this.pcbPosAlign.Size = new System.Drawing.Size(246, 21);
			this.pcbPosAlign.TabIndex = 69;
			this.pcbPosAlign.SelectedIndexChanged += new System.EventHandler(this.pcbPosAlign_SelectedIndexChanged);
			// 
			// plStickPoint
			// 
			this.plStickPoint.AutoSize = true;
			this.ptDetails.SetColumnSpan(this.plStickPoint, 2);
			this.plStickPoint.Dock = System.Windows.Forms.DockStyle.Fill;
			this.plStickPoint.Location = new System.Drawing.Point(3, 190);
			this.plStickPoint.Name = "plStickPoint";
			this.plStickPoint.Size = new System.Drawing.Size(246, 20);
			this.plStickPoint.TabIndex = 70;
			this.plStickPoint.Text = "Punkt zaczepienia pola:";
			this.plStickPoint.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// pcbUseImageMargin
			// 
			this.pcbUseImageMargin.AutoSize = true;
			this.ptDetails.SetColumnSpan(this.pcbUseImageMargin, 2);
			this.pcbUseImageMargin.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pcbUseImageMargin.Location = new System.Drawing.Point(3, 170);
			this.pcbUseImageMargin.Margin = new System.Windows.Forms.Padding(3, 5, 3, 3);
			this.pcbUseImageMargin.Name = "pcbUseImageMargin";
			this.pcbUseImageMargin.Size = new System.Drawing.Size(246, 17);
			this.pcbUseImageMargin.TabIndex = 12;
			this.pcbUseImageMargin.Text = "Zastosuj margines do obrazu";
			this.pcbUseImageMargin.UseVisualStyleBackColor = true;
			// 
			// pcbDrawFrameOutside
			// 
			this.pcbDrawFrameOutside.AutoSize = true;
			this.ptDetails.SetColumnSpan(this.pcbDrawFrameOutside, 2);
			this.pcbDrawFrameOutside.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pcbDrawFrameOutside.Location = new System.Drawing.Point(3, 145);
			this.pcbDrawFrameOutside.Margin = new System.Windows.Forms.Padding(3, 5, 3, 3);
			this.pcbDrawFrameOutside.Name = "pcbDrawFrameOutside";
			this.pcbDrawFrameOutside.Size = new System.Drawing.Size(246, 17);
			this.pcbDrawFrameOutside.TabIndex = 11;
			this.pcbDrawFrameOutside.Text = "Rysuj ramkę na zewnątrz obrazu";
			this.pcbDrawFrameOutside.UseVisualStyleBackColor = true;
			// 
			// plImageSettings
			// 
			this.plImageSettings.AutoSize = true;
			this.ptDetails.SetColumnSpan(this.plImageSettings, 2);
			this.plImageSettings.Dock = System.Windows.Forms.DockStyle.Fill;
			this.plImageSettings.Location = new System.Drawing.Point(3, 95);
			this.plImageSettings.Name = "plImageSettings";
			this.plImageSettings.Size = new System.Drawing.Size(246, 20);
			this.plImageSettings.TabIndex = 9;
			this.plImageSettings.Text = "Ustawienia obrazu:";
			this.plImageSettings.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// pcbDynImage
			// 
			this.pcbDynImage.AutoSize = true;
			this.pcbDynImage.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pcbDynImage.Location = new System.Drawing.Point(129, 75);
			this.pcbDynImage.Margin = new System.Windows.Forms.Padding(3, 5, 3, 3);
			this.pcbDynImage.Name = "pcbDynImage";
			this.pcbDynImage.Size = new System.Drawing.Size(120, 17);
			this.pcbDynImage.TabIndex = 8;
			this.pcbDynImage.Text = "Obraz dynamiczny";
			this.pcbDynImage.UseVisualStyleBackColor = true;
			this.pcbDynImage.CheckedChanged += new System.EventHandler(this.pcbDynImage_CheckedChanged);
			// 
			// pcbStatText
			// 
			this.pcbStatText.AutoSize = true;
			this.pcbStatText.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pcbStatText.Location = new System.Drawing.Point(3, 75);
			this.pcbStatText.Margin = new System.Windows.Forms.Padding(3, 5, 3, 3);
			this.pcbStatText.Name = "pcbStatText";
			this.pcbStatText.Size = new System.Drawing.Size(120, 17);
			this.pcbStatText.TabIndex = 7;
			this.pcbStatText.Text = "Tekst statyczny";
			this.pcbStatText.UseVisualStyleBackColor = true;
			this.pcbStatText.CheckedChanged += new System.EventHandler(this.pcbStatText_CheckedChanged);
			// 
			// pcbDrawColor
			// 
			this.pcbDrawColor.AutoSize = true;
			this.pcbDrawColor.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pcbDrawColor.Location = new System.Drawing.Point(129, 25);
			this.pcbDrawColor.Margin = new System.Windows.Forms.Padding(3, 5, 3, 3);
			this.pcbDrawColor.Name = "pcbDrawColor";
			this.pcbDrawColor.Size = new System.Drawing.Size(120, 17);
			this.pcbDrawColor.TabIndex = 6;
			this.pcbDrawColor.Text = "Rysuj kolor pola";
			this.pcbDrawColor.UseVisualStyleBackColor = true;
			this.pcbDrawColor.CheckedChanged += new System.EventHandler(this.pcbDrawColor_CheckedChanged);
			// 
			// pcbDynText
			// 
			this.pcbDynText.AutoSize = true;
			this.pcbDynText.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pcbDynText.Location = new System.Drawing.Point(3, 50);
			this.pcbDynText.Margin = new System.Windows.Forms.Padding(3, 5, 3, 3);
			this.pcbDynText.Name = "pcbDynText";
			this.pcbDynText.Size = new System.Drawing.Size(120, 17);
			this.pcbDynText.TabIndex = 3;
			this.pcbDynText.Text = "Tekst dynamiczny";
			this.pcbDynText.UseVisualStyleBackColor = true;
			this.pcbDynText.CheckedChanged += new System.EventHandler(this.pcbDynText_CheckedChanged);
			// 
			// plPDFGenerate
			// 
			this.plPDFGenerate.AutoSize = true;
			this.ptDetails.SetColumnSpan(this.plPDFGenerate, 2);
			this.plPDFGenerate.Dock = System.Windows.Forms.DockStyle.Fill;
			this.plPDFGenerate.Location = new System.Drawing.Point(3, 0);
			this.plPDFGenerate.Name = "plPDFGenerate";
			this.plPDFGenerate.Size = new System.Drawing.Size(246, 20);
			this.plPDFGenerate.TabIndex = 0;
			this.plPDFGenerate.Text = "Generowanie do pliku PDF:";
			this.plPDFGenerate.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// pcbShowFrame
			// 
			this.pcbShowFrame.AutoSize = true;
			this.pcbShowFrame.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pcbShowFrame.Location = new System.Drawing.Point(3, 25);
			this.pcbShowFrame.Margin = new System.Windows.Forms.Padding(3, 5, 3, 3);
			this.pcbShowFrame.Name = "pcbShowFrame";
			this.pcbShowFrame.Size = new System.Drawing.Size(120, 17);
			this.pcbShowFrame.TabIndex = 1;
			this.pcbShowFrame.Text = "Wyświetlaj ramke";
			this.pcbShowFrame.UseVisualStyleBackColor = true;
			this.pcbShowFrame.CheckedChanged += new System.EventHandler(this.pcbShowFrame_CheckedChanged);
			// 
			// pcbStatImage
			// 
			this.pcbStatImage.AutoSize = true;
			this.pcbStatImage.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pcbStatImage.Location = new System.Drawing.Point(129, 50);
			this.pcbStatImage.Margin = new System.Windows.Forms.Padding(3, 5, 3, 3);
			this.pcbStatImage.Name = "pcbStatImage";
			this.pcbStatImage.Size = new System.Drawing.Size(120, 17);
			this.pcbStatImage.TabIndex = 4;
			this.pcbStatImage.Text = "Obraz statyczny";
			this.pcbStatImage.UseVisualStyleBackColor = true;
			this.pcbStatImage.CheckedChanged += new System.EventHandler(this.pcbStatImage_CheckedChanged);
			// 
			// pcxImageSet
			// 
			this.ptDetails.SetColumnSpan(this.pcxImageSet, 2);
			this.pcxImageSet.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pcxImageSet.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.pcxImageSet.FormattingEnabled = true;
			this.pcxImageSet.Items.AddRange(new object[] {
            "Zostaw proporcje takie jakie są"});
			this.pcxImageSet.Location = new System.Drawing.Point(3, 118);
			this.pcxImageSet.Name = "pcxImageSet";
			this.pcxImageSet.Size = new System.Drawing.Size(246, 21);
			this.pcxImageSet.TabIndex = 10;
			// 
			// pcbMarginLR
			// 
			this.pcbMarginLR.DecimalPlaces = 1;
			this.pcbMarginLR.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pcbMarginLR.Location = new System.Drawing.Point(3, 283);
			this.pcbMarginLR.Name = "pcbMarginLR";
			this.pcbMarginLR.Size = new System.Drawing.Size(120, 20);
			this.pcbMarginLR.TabIndex = 74;
			this.pcbMarginLR.Visible = false;
			this.pcbMarginLR.ValueChanged += new System.EventHandler(this.pcbMarginLR_ValueChanged);
			// 
			// pcbMarginTB
			// 
			this.pcbMarginTB.DecimalPlaces = 1;
			this.pcbMarginTB.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pcbMarginTB.Location = new System.Drawing.Point(129, 283);
			this.pcbMarginTB.Name = "pcbMarginTB";
			this.pcbMarginTB.Size = new System.Drawing.Size(120, 20);
			this.pcbMarginTB.TabIndex = 73;
			this.pcbMarginTB.Visible = false;
			this.pcbMarginTB.ValueChanged += new System.EventHandler(this.pcbMarginTB_ValueChanged);
			// 
			// ptPageDetails
			// 
			this.ptPageDetails.ColumnCount = 2;
			this.ptPageDetails.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.ptPageDetails.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.ptPageDetails.Controls.Add(this.pcbPageLook, 0, 2);
			this.ptPageDetails.Controls.Add(this.ptbPageImage, 0, 4);
			this.ptPageDetails.Controls.Add(this.pbPageImage, 1, 4);
			this.ptPageDetails.Controls.Add(this.pbPageColor, 1, 3);
			this.ptPageDetails.Controls.Add(this.ptbPageColor, 0, 3);
			this.ptPageDetails.Controls.Add(this.pcbpDrawOutside, 0, 10);
			this.ptPageDetails.Controls.Add(this.pcbpApplyMargin, 0, 9);
			this.ptPageDetails.Controls.Add(this.pcxpImageSet, 0, 8);
			this.ptPageDetails.Controls.Add(this.plpImageSettings, 0, 7);
			this.ptPageDetails.Controls.Add(this.pcbpDrawColor, 0, 6);
			this.ptPageDetails.Controls.Add(this.pcbpDrawImage, 1, 6);
			this.ptPageDetails.Controls.Add(this.ptbpWidth, 0, 1);
			this.ptPageDetails.Controls.Add(this.ptbpHeight, 1, 1);
			this.ptPageDetails.Controls.Add(this.plpWidth, 0, 0);
			this.ptPageDetails.Controls.Add(this.plpHeight, 1, 0);
			this.ptPageDetails.Controls.Add(this.plpGeneratePDF, 0, 5);
			this.ptPageDetails.Dock = System.Windows.Forms.DockStyle.Fill;
			this.ptPageDetails.Location = new System.Drawing.Point(0, 0);
			this.ptPageDetails.Name = "ptPageDetails";
			this.ptPageDetails.RowCount = 12;
			this.ptPageDetails.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.ptPageDetails.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
			this.ptPageDetails.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.ptPageDetails.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
			this.ptPageDetails.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
			this.ptPageDetails.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.ptPageDetails.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
			this.ptPageDetails.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.ptPageDetails.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
			this.ptPageDetails.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
			this.ptPageDetails.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
			this.ptPageDetails.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.ptPageDetails.Size = new System.Drawing.Size(252, 379);
			this.ptPageDetails.TabIndex = 4;
			this.ptPageDetails.Visible = false;
			// 
			// pcbPageLook
			// 
			this.pcbPageLook.AutoSize = true;
			this.ptPageDetails.SetColumnSpan(this.pcbPageLook, 2);
			this.pcbPageLook.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pcbPageLook.Location = new System.Drawing.Point(3, 45);
			this.pcbPageLook.Name = "pcbPageLook";
			this.pcbPageLook.Size = new System.Drawing.Size(246, 20);
			this.pcbPageLook.TabIndex = 55;
			this.pcbPageLook.Text = "Wygląd:";
			this.pcbPageLook.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// ptbPageImage
			// 
			this.ptbPageImage.Dock = System.Windows.Forms.DockStyle.Fill;
			this.ptbPageImage.Enabled = false;
			this.ptbPageImage.Location = new System.Drawing.Point(3, 93);
			this.ptbPageImage.Name = "ptbPageImage";
			this.ptbPageImage.Size = new System.Drawing.Size(120, 20);
			this.ptbPageImage.TabIndex = 54;
			// 
			// pbPageImage
			// 
			this.pbPageImage.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pbPageImage.Enabled = false;
			this.pbPageImage.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.pbPageImage.Location = new System.Drawing.Point(129, 92);
			this.pbPageImage.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.pbPageImage.Name = "pbPageImage";
			this.pbPageImage.Size = new System.Drawing.Size(120, 21);
			this.pbPageImage.TabIndex = 53;
			this.pbPageImage.Text = "Obraz";
			this.pbPageImage.UseVisualStyleBackColor = true;
			// 
			// pbPageColor
			// 
			this.pbPageColor.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pbPageColor.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.pbPageColor.Location = new System.Drawing.Point(129, 67);
			this.pbPageColor.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.pbPageColor.Name = "pbPageColor";
			this.pbPageColor.Size = new System.Drawing.Size(120, 21);
			this.pbPageColor.TabIndex = 52;
			this.pbPageColor.Text = "Kolor";
			this.pbPageColor.UseVisualStyleBackColor = true;
			this.pbPageColor.Click += new System.EventHandler(this.icpPageColor_Click);
			// 
			// ptbPageColor
			// 
			this.ptbPageColor.Dock = System.Windows.Forms.DockStyle.Fill;
			this.ptbPageColor.Enabled = false;
			this.ptbPageColor.Location = new System.Drawing.Point(3, 68);
			this.ptbPageColor.Name = "ptbPageColor";
			this.ptbPageColor.Size = new System.Drawing.Size(120, 20);
			this.ptbPageColor.TabIndex = 51;
			// 
			// pcbpDrawOutside
			// 
			this.pcbpDrawOutside.AutoSize = true;
			this.ptPageDetails.SetColumnSpan(this.pcbpDrawOutside, 2);
			this.pcbpDrawOutside.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pcbpDrawOutside.Location = new System.Drawing.Point(3, 235);
			this.pcbpDrawOutside.Margin = new System.Windows.Forms.Padding(3, 5, 3, 3);
			this.pcbpDrawOutside.Name = "pcbpDrawOutside";
			this.pcbpDrawOutside.Size = new System.Drawing.Size(246, 17);
			this.pcbpDrawOutside.TabIndex = 14;
			this.pcbpDrawOutside.Text = "Rysuj ramkę na zewnątrz obrazu";
			this.pcbpDrawOutside.UseVisualStyleBackColor = true;
			// 
			// pcbpApplyMargin
			// 
			this.pcbpApplyMargin.AutoSize = true;
			this.ptPageDetails.SetColumnSpan(this.pcbpApplyMargin, 2);
			this.pcbpApplyMargin.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pcbpApplyMargin.Location = new System.Drawing.Point(3, 210);
			this.pcbpApplyMargin.Margin = new System.Windows.Forms.Padding(3, 5, 3, 3);
			this.pcbpApplyMargin.Name = "pcbpApplyMargin";
			this.pcbpApplyMargin.Size = new System.Drawing.Size(246, 17);
			this.pcbpApplyMargin.TabIndex = 13;
			this.pcbpApplyMargin.Text = "Zastosuj margines do obrazu";
			this.pcbpApplyMargin.UseVisualStyleBackColor = true;
			// 
			// pcxpImageSet
			// 
			this.ptPageDetails.SetColumnSpan(this.pcxpImageSet, 2);
			this.pcxpImageSet.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pcxpImageSet.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.pcxpImageSet.FormattingEnabled = true;
			this.pcxpImageSet.Items.AddRange(new object[] {
            "Zostaw proporcje takie jakie są"});
			this.pcxpImageSet.Location = new System.Drawing.Point(3, 183);
			this.pcxpImageSet.Name = "pcxpImageSet";
			this.pcxpImageSet.Size = new System.Drawing.Size(246, 21);
			this.pcxpImageSet.TabIndex = 11;
			// 
			// plpImageSettings
			// 
			this.plpImageSettings.AutoSize = true;
			this.ptPageDetails.SetColumnSpan(this.plpImageSettings, 2);
			this.plpImageSettings.Dock = System.Windows.Forms.DockStyle.Fill;
			this.plpImageSettings.Location = new System.Drawing.Point(3, 160);
			this.plpImageSettings.Name = "plpImageSettings";
			this.plpImageSettings.Size = new System.Drawing.Size(246, 20);
			this.plpImageSettings.TabIndex = 10;
			this.plpImageSettings.Text = "Ustawienia obrazu:";
			this.plpImageSettings.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// pcbpDrawColor
			// 
			this.pcbpDrawColor.AutoSize = true;
			this.pcbpDrawColor.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pcbpDrawColor.Location = new System.Drawing.Point(3, 140);
			this.pcbpDrawColor.Margin = new System.Windows.Forms.Padding(3, 5, 3, 3);
			this.pcbpDrawColor.Name = "pcbpDrawColor";
			this.pcbpDrawColor.Size = new System.Drawing.Size(120, 17);
			this.pcbpDrawColor.TabIndex = 8;
			this.pcbpDrawColor.Text = "Rysuj kolor strony";
			this.pcbpDrawColor.UseVisualStyleBackColor = true;
			this.pcbpDrawColor.CheckedChanged += new System.EventHandler(this.pcbpDrawColor_CheckedChanged);
			// 
			// pcbpDrawImage
			// 
			this.pcbpDrawImage.AutoSize = true;
			this.pcbpDrawImage.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pcbpDrawImage.Enabled = false;
			this.pcbpDrawImage.Location = new System.Drawing.Point(129, 140);
			this.pcbpDrawImage.Margin = new System.Windows.Forms.Padding(3, 5, 3, 3);
			this.pcbpDrawImage.Name = "pcbpDrawImage";
			this.pcbpDrawImage.Size = new System.Drawing.Size(120, 17);
			this.pcbpDrawImage.TabIndex = 7;
			this.pcbpDrawImage.Text = "Rysuj obraz strony";
			this.pcbpDrawImage.UseVisualStyleBackColor = true;
			this.pcbpDrawImage.CheckedChanged += new System.EventHandler(this.pcbpDrawImage_CheckedChanged);
			// 
			// ptbpWidth
			// 
			this.ptbpWidth.Dock = System.Windows.Forms.DockStyle.Fill;
			this.ptbpWidth.Enabled = false;
			this.ptbpWidth.Location = new System.Drawing.Point(3, 23);
			this.ptbpWidth.Name = "ptbpWidth";
			this.ptbpWidth.Size = new System.Drawing.Size(120, 20);
			this.ptbpWidth.TabIndex = 0;
			// 
			// ptbpHeight
			// 
			this.ptbpHeight.Dock = System.Windows.Forms.DockStyle.Fill;
			this.ptbpHeight.Enabled = false;
			this.ptbpHeight.Location = new System.Drawing.Point(129, 23);
			this.ptbpHeight.Name = "ptbpHeight";
			this.ptbpHeight.Size = new System.Drawing.Size(120, 20);
			this.ptbpHeight.TabIndex = 1;
			// 
			// plpWidth
			// 
			this.plpWidth.AutoSize = true;
			this.plpWidth.Dock = System.Windows.Forms.DockStyle.Fill;
			this.plpWidth.Location = new System.Drawing.Point(3, 0);
			this.plpWidth.Name = "plpWidth";
			this.plpWidth.Size = new System.Drawing.Size(120, 20);
			this.plpWidth.TabIndex = 2;
			this.plpWidth.Text = "Szerokość:";
			this.plpWidth.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// plpHeight
			// 
			this.plpHeight.AutoSize = true;
			this.plpHeight.Dock = System.Windows.Forms.DockStyle.Fill;
			this.plpHeight.Location = new System.Drawing.Point(129, 0);
			this.plpHeight.Name = "plpHeight";
			this.plpHeight.Size = new System.Drawing.Size(120, 20);
			this.plpHeight.TabIndex = 3;
			this.plpHeight.Text = "Wysokość:";
			this.plpHeight.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// plpGeneratePDF
			// 
			this.plpGeneratePDF.AutoSize = true;
			this.ptPageDetails.SetColumnSpan(this.plpGeneratePDF, 2);
			this.plpGeneratePDF.Dock = System.Windows.Forms.DockStyle.Fill;
			this.plpGeneratePDF.Location = new System.Drawing.Point(3, 115);
			this.plpGeneratePDF.Name = "plpGeneratePDF";
			this.plpGeneratePDF.Size = new System.Drawing.Size(246, 20);
			this.plpGeneratePDF.TabIndex = 4;
			this.plpGeneratePDF.Text = "Generowanie do pliku PDF:";
			this.plpGeneratePDF.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// scData
			// 
			this.scData.Dock = System.Windows.Forms.DockStyle.Fill;
			this.scData.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
			this.scData.Location = new System.Drawing.Point(6, 6);
			this.scData.Margin = new System.Windows.Forms.Padding(6, 6, 6, 3);
			this.scData.Name = "scData";
			// 
			// scData.Panel1
			// 
			this.scData.Panel1.Controls.Add(this.dpPreview);
			this.scData.Panel1MinSize = 416;
			// 
			// scData.Panel2
			// 
			this.scData.Panel2.Controls.Add(this.dtvData);
			this.scData.Panel2MinSize = 250;
			this.scData.Size = new System.Drawing.Size(672, 414);
			this.scData.SplitterDistance = 416;
			this.scData.SplitterWidth = 6;
			this.scData.TabIndex = 4;
			this.scData.SplitterMoved += new System.Windows.Forms.SplitterEventHandler(this.scData_SplitterMoved);
			// 
			// scMain
			// 
			this.scMain.Dock = System.Windows.Forms.DockStyle.Fill;
			this.scMain.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
			this.scMain.Location = new System.Drawing.Point(6, 6);
			this.scMain.Margin = new System.Windows.Forms.Padding(6);
			this.scMain.Name = "scMain";
			// 
			// scMain.Panel1
			// 
			this.scMain.Panel1.Controls.Add(this.mtvPatterns);
			this.scMain.Panel1MinSize = 250;
			// 
			// scMain.Panel2
			// 
			this.scMain.Panel2.Controls.Add(this.mpPreview);
			this.scMain.Panel2MinSize = 416;
			this.scMain.Size = new System.Drawing.Size(672, 408);
			this.scMain.SplitterDistance = 250;
			this.scMain.SplitterWidth = 6;
			this.scMain.TabIndex = 5;
			this.scMain.SplitterMoved += new System.Windows.Forms.SplitterEventHandler(this.scHome_SplitterMoved);
			// 
			// mtlButtons
			// 
			this.mtlButtons.ColumnCount = 2;
			this.mtlButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.mtlButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.mtlButtons.Controls.Add(this.mbDelete, 0, 0);
			this.mtlButtons.Controls.Add(this.mbNew, 0, 0);
			this.mtlButtons.Dock = System.Windows.Forms.DockStyle.Fill;
			this.mtlButtons.Location = new System.Drawing.Point(5, 1);
			this.mtlButtons.Margin = new System.Windows.Forms.Padding(5, 1, 0, 0);
			this.mtlButtons.Name = "mtlButtons";
			this.mtlButtons.RowCount = 1;
			this.mtlButtons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.mtlButtons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
			this.mtlButtons.Size = new System.Drawing.Size(252, 30);
			this.mtlButtons.TabIndex = 8;
			// 
			// tlMain
			// 
			this.tlMain.ColumnCount = 1;
			this.tlMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tlMain.Controls.Add(this.scMain, 0, 0);
			this.tlMain.Controls.Add(this.tlMainStatusBar, 0, 1);
			this.tlMain.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tlMain.Location = new System.Drawing.Point(0, 31);
			this.tlMain.Name = "tlMain";
			this.tlMain.RowCount = 2;
			this.tlMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tlMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 31F));
			this.tlMain.Size = new System.Drawing.Size(684, 451);
			this.tlMain.TabIndex = 6;
			// 
			// tlMainStatusBar
			// 
			this.tlMainStatusBar.BackColor = System.Drawing.SystemColors.ControlLight;
			this.tlMainStatusBar.ColumnCount = 2;
			this.tlMainStatusBar.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 257F));
			this.tlMainStatusBar.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tlMainStatusBar.Controls.Add(this.mtlStatusBar, 0, 0);
			this.tlMainStatusBar.Controls.Add(this.mtlButtons, 0, 0);
			this.tlMainStatusBar.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tlMainStatusBar.Location = new System.Drawing.Point(0, 420);
			this.tlMainStatusBar.Margin = new System.Windows.Forms.Padding(0);
			this.tlMainStatusBar.Name = "tlMainStatusBar";
			this.tlMainStatusBar.RowCount = 1;
			this.tlMainStatusBar.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tlMainStatusBar.Size = new System.Drawing.Size(684, 31);
			this.tlMainStatusBar.TabIndex = 6;
			this.tlMainStatusBar.Paint += new System.Windows.Forms.PaintEventHandler(this.tlMainStatusBar_Paint);
			// 
			// tDataTable
			// 
			this.tDataTable.ColumnCount = 1;
			this.tDataTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tDataTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tDataTable.Controls.Add(this.scData, 0, 0);
			this.tDataTable.Controls.Add(this.sbData, 0, 1);
			this.tDataTable.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tDataTable.Location = new System.Drawing.Point(0, 31);
			this.tDataTable.Name = "tDataTable";
			this.tDataTable.RowCount = 2;
			this.tDataTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tDataTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
			this.tDataTable.Size = new System.Drawing.Size(684, 451);
			this.tDataTable.TabIndex = 7;
			this.tDataTable.Visible = false;
			// 
			// sbData
			// 
			this.sbData.BackColor = System.Drawing.SystemColors.ControlLight;
			this.sbData.ColumnCount = 2;
			this.sbData.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.sbData.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 257F));
			this.sbData.Controls.Add(this.dtInfoControls, 0, 0);
			this.sbData.Controls.Add(this.dtButtonTable, 1, 0);
			this.sbData.Dock = System.Windows.Forms.DockStyle.Fill;
			this.sbData.Location = new System.Drawing.Point(0, 423);
			this.sbData.Margin = new System.Windows.Forms.Padding(0);
			this.sbData.Name = "sbData";
			this.sbData.RowCount = 1;
			this.sbData.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.sbData.Size = new System.Drawing.Size(684, 28);
			this.sbData.TabIndex = 5;
			// 
			// dtInfoControls
			// 
			this.dtInfoControls.ColumnCount = 3;
			this.dtInfoControls.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
			this.dtInfoControls.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.dtInfoControls.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
			this.dtInfoControls.Controls.Add(this.dfPage, 0, 0);
			this.dtInfoControls.Controls.Add(this.dlStatus, 0, 0);
			this.dtInfoControls.Controls.Add(this.dcbZoom, 0, 0);
			this.dtInfoControls.Dock = System.Windows.Forms.DockStyle.Fill;
			this.dtInfoControls.Location = new System.Drawing.Point(0, 0);
			this.dtInfoControls.Margin = new System.Windows.Forms.Padding(0, 0, 3, 0);
			this.dtInfoControls.Name = "dtInfoControls";
			this.dtInfoControls.RowCount = 1;
			this.dtInfoControls.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.dtInfoControls.Size = new System.Drawing.Size(424, 28);
			this.dtInfoControls.TabIndex = 3;
			// 
			// dfPage
			// 
			this.dfPage.Controls.Add(this.dnPage);
			this.dfPage.Controls.Add(this.dlPage);
			this.dfPage.Dock = System.Windows.Forms.DockStyle.Fill;
			this.dfPage.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
			this.dfPage.Location = new System.Drawing.Point(324, 0);
			this.dfPage.Margin = new System.Windows.Forms.Padding(0);
			this.dfPage.Name = "dfPage";
			this.dfPage.Size = new System.Drawing.Size(100, 28);
			this.dfPage.TabIndex = 12;
			// 
			// dnPage
			// 
			this.dnPage.Location = new System.Drawing.Point(62, 5);
			this.dnPage.Margin = new System.Windows.Forms.Padding(3, 5, 3, 3);
			this.dnPage.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.dnPage.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.dnPage.Name = "dnPage";
			this.dnPage.Size = new System.Drawing.Size(35, 20);
			this.dnPage.TabIndex = 0;
			this.dnPage.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.dnPage.ValueChanged += new System.EventHandler(this.dnPage_ValueChanged);
			// 
			// dlPage
			// 
			this.dlPage.AutoSize = true;
			this.dlPage.Location = new System.Drawing.Point(18, 8);
			this.dlPage.Margin = new System.Windows.Forms.Padding(3, 8, 0, 0);
			this.dlPage.Name = "dlPage";
			this.dlPage.Size = new System.Drawing.Size(41, 13);
			this.dlPage.TabIndex = 1;
			this.dlPage.Text = "Strona:";
			// 
			// dlStatus
			// 
			this.dlStatus.AutoEllipsis = true;
			this.dlStatus.AutoSize = true;
			this.dlStatus.Dock = System.Windows.Forms.DockStyle.Fill;
			this.dlStatus.Location = new System.Drawing.Point(83, 0);
			this.dlStatus.Name = "dlStatus";
			this.dlStatus.Size = new System.Drawing.Size(238, 28);
			this.dlStatus.TabIndex = 11;
			this.dlStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// dcbZoom
			// 
			this.dcbZoom.Dock = System.Windows.Forms.DockStyle.Fill;
			this.dcbZoom.Enabled = false;
			this.dcbZoom.FormattingEnabled = true;
			this.dcbZoom.Items.AddRange(new object[] {
            "50",
            "75",
            "100",
            "150",
            "200",
            "300"});
			this.dcbZoom.Location = new System.Drawing.Point(6, 4);
			this.dcbZoom.Margin = new System.Windows.Forms.Padding(6, 4, 3, 3);
			this.dcbZoom.Name = "dcbZoom";
			this.dcbZoom.Size = new System.Drawing.Size(71, 21);
			this.dcbZoom.TabIndex = 10;
			this.dcbZoom.SelectedIndexChanged += new System.EventHandler(this.dcbZoom_SelectedIndexChanged);
			this.dcbZoom.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dcbZoom_KeyDown);
			this.dcbZoom.Leave += new System.EventHandler(this.dcbZoom_Leave);
			// 
			// tPattern
			// 
			this.tPattern.ColumnCount = 1;
			this.tPattern.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tPattern.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tPattern.Controls.Add(this.scPattern, 0, 0);
			this.tPattern.Controls.Add(this.sbPattern, 0, 1);
			this.tPattern.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tPattern.Location = new System.Drawing.Point(0, 31);
			this.tPattern.Name = "tPattern";
			this.tPattern.RowCount = 2;
			this.tPattern.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tPattern.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
			this.tPattern.Size = new System.Drawing.Size(684, 451);
			this.tPattern.TabIndex = 8;
			this.tPattern.Visible = false;
			// 
			// scPattern
			// 
			this.scPattern.Dock = System.Windows.Forms.DockStyle.Fill;
			this.scPattern.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
			this.scPattern.Location = new System.Drawing.Point(6, 6);
			this.scPattern.Margin = new System.Windows.Forms.Padding(6, 6, 6, 3);
			this.scPattern.Name = "scPattern";
			// 
			// scPattern.Panel1
			// 
			this.scPattern.Panel1.Controls.Add(this.ppPreview);
			this.scPattern.Panel1MinSize = 416;
			// 
			// scPattern.Panel2
			// 
			this.scPattern.Panel2.Controls.Add(this.ptPatternDetails);
			this.scPattern.Panel2MinSize = 250;
			this.scPattern.Size = new System.Drawing.Size(672, 414);
			this.scPattern.SplitterDistance = 416;
			this.scPattern.SplitterWidth = 6;
			this.scPattern.TabIndex = 0;
			this.scPattern.SplitterMoved += new System.Windows.Forms.SplitterEventHandler(this.scPattern_SplitterMoved);
			// 
			// sbPattern
			// 
			this.sbPattern.BackColor = System.Drawing.SystemColors.ControlLight;
			this.sbPattern.ColumnCount = 2;
			this.sbPattern.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.sbPattern.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 257F));
			this.sbPattern.Controls.Add(this.ptInfoControls, 0, 0);
			this.sbPattern.Controls.Add(this.ptStatusButtons, 1, 0);
			this.sbPattern.Dock = System.Windows.Forms.DockStyle.Fill;
			this.sbPattern.Location = new System.Drawing.Point(0, 423);
			this.sbPattern.Margin = new System.Windows.Forms.Padding(0);
			this.sbPattern.Name = "sbPattern";
			this.sbPattern.RowCount = 1;
			this.sbPattern.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.sbPattern.Size = new System.Drawing.Size(684, 28);
			this.sbPattern.TabIndex = 1;
			// 
			// ptStatusButtons
			// 
			this.ptStatusButtons.ColumnCount = 2;
			this.ptStatusButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.ptStatusButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.ptStatusButtons.Controls.Add(this.pbSave, 0, 0);
			this.ptStatusButtons.Controls.Add(this.pbLoadData, 0, 0);
			this.ptStatusButtons.Dock = System.Windows.Forms.DockStyle.Fill;
			this.ptStatusButtons.Location = new System.Drawing.Point(428, 0);
			this.ptStatusButtons.Margin = new System.Windows.Forms.Padding(1, 0, 6, 0);
			this.ptStatusButtons.Name = "ptStatusButtons";
			this.ptStatusButtons.RowCount = 1;
			this.ptStatusButtons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.ptStatusButtons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
			this.ptStatusButtons.Size = new System.Drawing.Size(250, 28);
			this.ptStatusButtons.TabIndex = 1;
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(684, 482);
			this.Controls.Add(this.tlMain);
			this.Controls.Add(this.tDataTable);
			this.Controls.Add(this.tPattern);
			this.Controls.Add(this.imMain);
			this.KeyPreview = true;
			this.MinimumSize = new System.Drawing.Size(700, 520);
			this.Name = "MainForm";
			this.Text = "CDesigner - Kreator Dokumentów";
			this.Move += new System.EventHandler(this.Main_Move);
			this.Resize += new System.EventHandler(this.Main_Resize);
			this.mtlStatusBar.ResumeLayout(false);
			this.mtlStatusBar.PerformLayout();
			this.mfPageLayout.ResumeLayout(false);
			this.mfPageLayout.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.mnPage)).EndInit();
			this.gcmPattern.ResumeLayout(false);
			this.imMain.ResumeLayout(false);
			this.imMain.PerformLayout();
			this.icPage.ResumeLayout(false);
			this.icLabel.ResumeLayout(false);
			this.dtButtonTable.ResumeLayout(false);
			this.ptInfoControls.ResumeLayout(false);
			this.ptInfoControls.PerformLayout();
			this.pflPage.ResumeLayout(false);
			this.pflPage.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.pnPage)).EndInit();
			this.ptPatternDetails.ResumeLayout(false);
			this.ptPatternDetails.PerformLayout();
			this.icPatMenu.ResumeLayout(false);
			this.icPatMenu.PerformLayout();
			this.ppDetailsPanel.ResumeLayout(false);
			this.ppDetailsPanel.PerformLayout();
			this.ptFieldDetails.ResumeLayout(false);
			this.ptFieldDetails.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.pnBorderSize)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pnPadding)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pnPositionY)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pnPositionX)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pnWidth)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pnHeight)).EndInit();
			this.ptDetails.ResumeLayout(false);
			this.ptDetails.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.pcbMarginLR)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pcbMarginTB)).EndInit();
			this.ptPageDetails.ResumeLayout(false);
			this.ptPageDetails.PerformLayout();
			this.scData.Panel1.ResumeLayout(false);
			this.scData.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.scData)).EndInit();
			this.scData.ResumeLayout(false);
			this.scMain.Panel1.ResumeLayout(false);
			this.scMain.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.scMain)).EndInit();
			this.scMain.ResumeLayout(false);
			this.mtlButtons.ResumeLayout(false);
			this.tlMain.ResumeLayout(false);
			this.tlMainStatusBar.ResumeLayout(false);
			this.tDataTable.ResumeLayout(false);
			this.sbData.ResumeLayout(false);
			this.dtInfoControls.ResumeLayout(false);
			this.dtInfoControls.PerformLayout();
			this.dfPage.ResumeLayout(false);
			this.dfPage.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.dnPage)).EndInit();
			this.tPattern.ResumeLayout(false);
			this.scPattern.Panel1.ResumeLayout(false);
			this.scPattern.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.scPattern)).EndInit();
			this.scPattern.ResumeLayout(false);
			this.sbPattern.ResumeLayout(false);
			this.ptStatusButtons.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

		private System.Windows.Forms.MenuStrip imMain;
        private System.Windows.Forms.ToolStripMenuItem gmPattern;
        private System.Windows.Forms.ToolStripMenuItem gmpNew;
		private System.Windows.Forms.ToolStripMenuItem gmpImport;
        private System.Windows.Forms.ToolStripMenuItem gsPattern;
        private System.Windows.Forms.ToolStripMenuItem gsHome;
		private System.Windows.Forms.ToolStripMenuItem gsData;
        private System.Windows.Forms.OpenFileDialog gsImage;
		private System.Windows.Forms.ColorDialog gsColor;
        private System.Windows.Forms.FontDialog gsFont;
        private System.Windows.Forms.ContextMenuStrip icPage;
        private System.Windows.Forms.ToolStripMenuItem icpPageBack;
        private System.Windows.Forms.ToolStripMenuItem icpPageClear;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem icpAddPage;
		private System.Windows.Forms.ToolStripMenuItem icpDeletePage;
        private System.Windows.Forms.ToolStripMenuItem icpAddField;
        private System.Windows.Forms.ContextMenuStrip icLabel;
        private System.Windows.Forms.ToolStripMenuItem iclBorderColor;
        private System.Windows.Forms.ToolStripMenuItem iclPrintBorder;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem iclFontName;
        private System.Windows.Forms.ToolStripMenuItem iclFontColor;
        private System.Windows.Forms.ToolStripMenuItem iclTextFromDb;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripMenuItem iclBackImage;
        private System.Windows.Forms.ToolStripMenuItem iclBackFromDb;
		private System.Windows.Forms.ToolStripMenuItem iclClearBack;
        private System.Windows.Forms.ToolStripMenuItem iclDeleteField;
        private System.Windows.Forms.ToolStripMenuItem iclPrintText;
		private System.Windows.Forms.ToolStripMenuItem iclPrintImage;
		private System.Windows.Forms.ToolStripMenuItem iclBackColor;
        private System.Windows.Forms.ToolStripMenuItem icpPageColor;
		private System.Windows.Forms.ToolStripMenuItem icpDeleteFields;
		private System.Windows.Forms.ToolStripMenuItem icpPrintColor;
		private System.Windows.Forms.ToolStripMenuItem icpPrintImage;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
		private System.Windows.Forms.ToolStripMenuItem iclPrintColor;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator8;
		private System.Windows.Forms.OpenFileDialog gsOpenFile;
		private System.Windows.Forms.ContextMenuStrip gcmPattern;
		private System.Windows.Forms.ToolStripMenuItem ictEdit;
		private System.Windows.Forms.ToolStripMenuItem ictDelete;
		private System.Windows.Forms.ToolStripMenuItem ictLoadData;
		private System.Windows.Forms.ToolStripMenuItem ictNew;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator10;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator9;
		private System.Windows.Forms.ToolStripMenuItem ictImport;
		private System.Windows.Forms.ToolStripMenuItem ictExport;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator11;
		private System.Windows.Forms.TableLayoutPanel mtlStatusBar;
		private System.Windows.Forms.Button mbDelete;
		private System.Windows.Forms.Button mbNew;
		private System.Windows.Forms.FlowLayoutPanel mfPageLayout;
		private System.Windows.Forms.NumericUpDown mnPage;
		private System.Windows.Forms.Label mlPage;
		private System.Windows.Forms.Panel dpPreview;
		private System.Windows.Forms.TreeView mtvPatterns;
		private System.Windows.Forms.TableLayoutPanel dtButtonTable;
		private System.Windows.Forms.TreeView dtvData;
		private System.Windows.Forms.Button pbSave;
		private System.Windows.Forms.Button pbLoadData;
		private System.Windows.Forms.TableLayoutPanel ptInfoControls;
		private System.Windows.Forms.FlowLayoutPanel pflPage;
		private System.Windows.Forms.NumericUpDown pnPage;
		private System.Windows.Forms.Label plPage;
		private System.Windows.Forms.Label plStatus;
		private System.Windows.Forms.ComboBox pcbScale;
		private System.Windows.Forms.Panel ppPreview;
		private System.Windows.Forms.Panel mpPreview;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator12;
		private System.Windows.Forms.Button dbGeneratePDF;
		private System.Windows.Forms.Button dbScan;
		private System.Windows.Forms.TableLayoutPanel ptPatternDetails;
		private System.Windows.Forms.MenuStrip icPatMenu;
		private System.Windows.Forms.ToolStripMenuItem icpmFieldDetails;
		private System.Windows.Forms.ToolStripMenuItem icpmPageDetails;
		private System.Windows.Forms.Panel ppDetailsPanel;
		private System.Windows.Forms.TableLayoutPanel ptFieldDetails;
		private System.Windows.Forms.Label plHeight;
		private System.Windows.Forms.NumericUpDown pnBorderSize;
		private System.Windows.Forms.Label plTextPosition;
		private System.Windows.Forms.Button pbFontName;
		private System.Windows.Forms.TextBox ptbFontName;
		private System.Windows.Forms.Button pbBackImage;
		private System.Windows.Forms.TextBox ptbBackImage;
		private System.Windows.Forms.Button pbBackColor;
		private System.Windows.Forms.TextBox ptbBackColor;
		private System.Windows.Forms.Button pbFontColor;
		private System.Windows.Forms.TextBox ptbFontColor;
		private System.Windows.Forms.TextBox ptbName;
		private System.Windows.Forms.NumericUpDown pnPositionY;
		private System.Windows.Forms.NumericUpDown pnPositionX;
		private System.Windows.Forms.Label plPositionX;
		private System.Windows.Forms.Label plWidth;
		private System.Windows.Forms.NumericUpDown pnWidth;
		private System.Windows.Forms.NumericUpDown pnHeight;
		private System.Windows.Forms.Label plBorderColor;
		private System.Windows.Forms.TextBox ptbBorderColor;
		private System.Windows.Forms.Button pbBorderColor;
		private System.Windows.Forms.Label plFont;
		private System.Windows.Forms.Label plBorderWidth;
		private System.Windows.Forms.ComboBox pcbTextAlign;
		private System.Windows.Forms.ComboBox pcbPosAlign;
		private System.Windows.Forms.Label plStickPoint;
		private System.Windows.Forms.Label plPositionY;
		private System.Windows.Forms.ToolStripMenuItem icpmDetails;
		private System.Windows.Forms.TableLayoutPanel ptDetails;
		private System.Windows.Forms.TableLayoutPanel ptPageDetails;
		private System.Windows.Forms.CheckBox pcbDynImage;
		private System.Windows.Forms.CheckBox pcbStatText;
		private System.Windows.Forms.CheckBox pcbDrawColor;
		private System.Windows.Forms.CheckBox pcbDynText;
		private System.Windows.Forms.Label plPDFGenerate;
		private System.Windows.Forms.CheckBox pcbShowFrame;
		private System.Windows.Forms.CheckBox pcbStatImage;
		private System.Windows.Forms.Label plImageSettings;
		private System.Windows.Forms.CheckBox pcbUseImageMargin;
		private System.Windows.Forms.CheckBox pcbDrawFrameOutside;
		private System.Windows.Forms.ComboBox pcxImageSet;
		private System.Windows.Forms.TextBox ptbpWidth;
		private System.Windows.Forms.TextBox ptbpHeight;
		private System.Windows.Forms.Label plpWidth;
		private System.Windows.Forms.Label plpHeight;
		private System.Windows.Forms.Label plpGeneratePDF;
		private System.Windows.Forms.CheckBox pcbpDrawColor;
		private System.Windows.Forms.CheckBox pcbpDrawImage;
		private System.Windows.Forms.CheckBox pcbpDrawOutside;
		private System.Windows.Forms.CheckBox pcbpApplyMargin;
		private System.Windows.Forms.ComboBox pcxpImageSet;
		private System.Windows.Forms.Label plpImageSettings;
		private System.Windows.Forms.SplitContainer scData;
		private System.Windows.Forms.SplitContainer scMain;
		private System.Windows.Forms.TableLayoutPanel mtlButtons;
		private System.Windows.Forms.Label mlStatus;
		private System.Windows.Forms.TableLayoutPanel tlMain;
		private System.Windows.Forms.TableLayoutPanel tlMainStatusBar;
		private System.Windows.Forms.TableLayoutPanel tDataTable;
		private System.Windows.Forms.TableLayoutPanel sbData;
		private System.Windows.Forms.TableLayoutPanel tPattern;
		private System.Windows.Forms.SplitContainer scPattern;
		private System.Windows.Forms.TableLayoutPanel sbPattern;
		private System.Windows.Forms.TableLayoutPanel ptStatusButtons;
		private System.Windows.Forms.TableLayoutPanel dtInfoControls;
		private System.Windows.Forms.FlowLayoutPanel dfPage;
		private System.Windows.Forms.NumericUpDown dnPage;
		private System.Windows.Forms.Label dlPage;
		private System.Windows.Forms.ComboBox dcbZoom;
		private System.Windows.Forms.Label dlStatus;
		private System.Windows.Forms.ToolStripMenuItem gmpRecent;
		private System.Windows.Forms.ToolStripSeparator gmSeparator1;
		private System.Windows.Forms.ToolStripMenuItem gmpExportAll;
		private System.Windows.Forms.ToolStripSeparator gmSeparator2;
		private System.Windows.Forms.ToolStripMenuItem gmpClose;
		private System.Windows.Forms.ToolStripMenuItem gmSettings;
		private System.Windows.Forms.ToolStripMenuItem issGeneratePDF;
		private System.Windows.Forms.ToolStripMenuItem issEditor;
		private System.Windows.Forms.ToolStripMenuItem issGeneral;
		private System.Windows.Forms.ToolStripMenuItem gmProgram;
		private System.Windows.Forms.ToolStripMenuItem gmpInfo;
		private System.Windows.Forms.ToolStripMenuItem gmpHelp;
		private System.Windows.Forms.Button pbPageColor;
		private System.Windows.Forms.TextBox ptbPageColor;
		private System.Windows.Forms.Label pcbPageLook;
		private System.Windows.Forms.TextBox ptbPageImage;
		private System.Windows.Forms.Button pbPageImage;
		private System.Windows.Forms.ComboBox pcbTextTransform;
		private System.Windows.Forms.Label plTextTransform;
		private System.Windows.Forms.ToolStripMenuItem gmTools;
		private System.Windows.Forms.ToolStripMenuItem gmtColumnsEditor;
		private System.Windows.Forms.Label pldAdditionalMargin;
		private System.Windows.Forms.NumericUpDown pnPadding;
		private System.Windows.Forms.Label plPadding;
		private System.Windows.Forms.CheckBox pcbdAddMargin;
		private System.Windows.Forms.NumericUpDown pcbMarginLR;
		private System.Windows.Forms.NumericUpDown pcbMarginTB;
		private System.Windows.Forms.ToolStripMenuItem gmtConnectDB;
		private System.Windows.Forms.ToolStripMenuItem gmtCreateDB;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripMenuItem gmprClearList;
		private System.Windows.Forms.ToolStripMenuItem gmtBackup;
		private System.Windows.Forms.ToolStripMenuItem gmpUpdate;
		private System.Windows.Forms.ToolStripMenuItem gmtLoadDatabase;
		private System.Windows.Forms.ToolStripSeparator gmSeparator3;

    }
}

