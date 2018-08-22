namespace CDesigner.Forms
{
	partial class MainForm
	{
	/// @cond COMPONENTS

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
			this.P_P1_Preview = new System.Windows.Forms.Panel();
			this.N_P1_Page = new System.Windows.Forms.NumericUpDown();
			this.L_P1_Page = new System.Windows.Forms.Label();
			this.B_P1_Delete = new System.Windows.Forms.Button();
			this.B_P1_New = new System.Windows.Forms.Button();
			this.TV_P1_Patterns = new System.Windows.Forms.TreeView();
			this.CMS_Pattern = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.TSMI_NewPattern = new System.Windows.Forms.ToolStripMenuItem();
			this.TSMI_Separator_A1 = new System.Windows.Forms.ToolStripSeparator();
			this.TSMI_EditPattern = new System.Windows.Forms.ToolStripMenuItem();
			this.TSMI_LoadData = new System.Windows.Forms.ToolStripMenuItem();
			this.TSMI_Separator_A2 = new System.Windows.Forms.ToolStripSeparator();
			this.TSMI_ImportPattern = new System.Windows.Forms.ToolStripMenuItem();
			this.TSMI_ExportPattern = new System.Windows.Forms.ToolStripMenuItem();
			this.TSMI_Separator_A3 = new System.Windows.Forms.ToolStripSeparator();
			this.TSMI_RemovePattern = new System.Windows.Forms.ToolStripMenuItem();
			this.MS_Main = new System.Windows.Forms.MenuStrip();
			this.TSMI_Pattern = new System.Windows.Forms.ToolStripMenuItem();
			this.TSMI_MenuNewPattern = new System.Windows.Forms.ToolStripMenuItem();
			this.TSMI_RecentOpen = new System.Windows.Forms.ToolStripMenuItem();
			this.TSS_Separator_P1 = new System.Windows.Forms.ToolStripSeparator();
			this.TSMI_ClearRecent = new System.Windows.Forms.ToolStripMenuItem();
			this.TSS_Separator_P2 = new System.Windows.Forms.ToolStripSeparator();
			this.TSMI_Import = new System.Windows.Forms.ToolStripMenuItem();
			this.TSMI_ExportAll = new System.Windows.Forms.ToolStripMenuItem();
			this.TSS_Separator_P3 = new System.Windows.Forms.ToolStripSeparator();
			this.TSMI_Close = new System.Windows.Forms.ToolStripMenuItem();
			this.TSMI_PrintPreview = new System.Windows.Forms.ToolStripMenuItem();
			this.TSMI_PatternEditor = new System.Windows.Forms.ToolStripMenuItem();
			this.TSMI_HomePage = new System.Windows.Forms.ToolStripMenuItem();
			this.TSMI_Tools = new System.Windows.Forms.ToolStripMenuItem();
			this.TSMI_LoadDataFile = new System.Windows.Forms.ToolStripMenuItem();
			this.TSMI_CreateEmpty = new System.Windows.Forms.ToolStripMenuItem();
			this.TSS_Separator_T1 = new System.Windows.Forms.ToolStripSeparator();
			this.TSMI_EditColumns = new System.Windows.Forms.ToolStripMenuItem();
			this.TSMI_EditRows = new System.Windows.Forms.ToolStripMenuItem();
			this.TSMI_CreateDatabase = new System.Windows.Forms.ToolStripMenuItem();
			this.TSMI_ConnectDatabase = new System.Windows.Forms.ToolStripMenuItem();
			this.TSMI_DataBackup = new System.Windows.Forms.ToolStripMenuItem();
			this.TSS_Separator_T2 = new System.Windows.Forms.ToolStripSeparator();
			this.TSMI_CloseData = new System.Windows.Forms.ToolStripMenuItem();
			this.TSMI_SaveData = new System.Windows.Forms.ToolStripMenuItem();
			this.TSMI_Settings = new System.Windows.Forms.ToolStripMenuItem();
			this.TSMI_Generator = new System.Windows.Forms.ToolStripMenuItem();
			this.TSMI_Editor = new System.Windows.Forms.ToolStripMenuItem();
			this.TSMI_General = new System.Windows.Forms.ToolStripMenuItem();
			this.TSMI_Language = new System.Windows.Forms.ToolStripMenuItem();
			this.TSMI_Program = new System.Windows.Forms.ToolStripMenuItem();
			this.TSMI_Info = new System.Windows.Forms.ToolStripMenuItem();
			this.TSMI_Help = new System.Windows.Forms.ToolStripMenuItem();
			this.TSMI_Update = new System.Windows.Forms.ToolStripMenuItem();
			this.CMS_Page = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.TSMI_AddField = new System.Windows.Forms.ToolStripMenuItem();
			this.TSMI_RemoveAll = new System.Windows.Forms.ToolStripMenuItem();
			this.TSMI_Separator_B0 = new System.Windows.Forms.ToolStripSeparator();
			this.TSMI_PageColor = new System.Windows.Forms.ToolStripMenuItem();
			this.TSMI_PageDrawColor = new System.Windows.Forms.ToolStripMenuItem();
			this.TSMI_PageImage = new System.Windows.Forms.ToolStripMenuItem();
			this.TSMI_PageClear = new System.Windows.Forms.ToolStripMenuItem();
			this.TSMI_Separator_B1 = new System.Windows.Forms.ToolStripSeparator();
			this.TSMI_PageDrawImage = new System.Windows.Forms.ToolStripMenuItem();
			this.TSMI_Separator_B2 = new System.Windows.Forms.ToolStripSeparator();
			this.TSMI_AddPage = new System.Windows.Forms.ToolStripMenuItem();
			this.TSMI_RemovePage = new System.Windows.Forms.ToolStripMenuItem();
			this.CMS_Label = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.TSMI_FieldColor = new System.Windows.Forms.ToolStripMenuItem();
			this.TSMI_DrawFieldFill = new System.Windows.Forms.ToolStripMenuItem();
			this.TSMI_FieldImage = new System.Windows.Forms.ToolStripMenuItem();
			this.TSMI_FieldClear = new System.Windows.Forms.ToolStripMenuItem();
			this.TSMI_Separator_C0 = new System.Windows.Forms.ToolStripSeparator();
			this.TSMI_DynamicImage = new System.Windows.Forms.ToolStripMenuItem();
			this.TSMI_StaticImage = new System.Windows.Forms.ToolStripMenuItem();
			this.TSMI_Separator_C1 = new System.Windows.Forms.ToolStripSeparator();
			this.TSMI_FontColor = new System.Windows.Forms.ToolStripMenuItem();
			this.TSMI_FontName = new System.Windows.Forms.ToolStripMenuItem();
			this.TSMI_DynamicText = new System.Windows.Forms.ToolStripMenuItem();
			this.TSMI_StaticText = new System.Windows.Forms.ToolStripMenuItem();
			this.TSMI_Separator_C2 = new System.Windows.Forms.ToolStripSeparator();
			this.TSMI_BorderColor = new System.Windows.Forms.ToolStripMenuItem();
			this.TSMI_DrawBorder = new System.Windows.Forms.ToolStripMenuItem();
			this.TSMI_Separator_C3 = new System.Windows.Forms.ToolStripSeparator();
			this.TSMI_RemoveField = new System.Windows.Forms.ToolStripMenuItem();
			this.CD_ColorDialog = new System.Windows.Forms.ColorDialog();
			this.FD_FontDialog = new System.Windows.Forms.FontDialog();
			this.P_P3_Generator = new System.Windows.Forms.Panel();
			this.TLP_P3_Buttons = new System.Windows.Forms.TableLayoutPanel();
			this.B_P3_GeneratePDF = new System.Windows.Forms.Button();
			this.B_P3_SearchErrors = new System.Windows.Forms.Button();
			this.TV_P3_PageList = new System.Windows.Forms.TreeView();
			this.B_P2_Save = new System.Windows.Forms.Button();
			this.B_P2_LoadData = new System.Windows.Forms.Button();
			this.TLP_P2_InfoControls = new System.Windows.Forms.TableLayoutPanel();
			this.FLP_P2_Page = new System.Windows.Forms.FlowLayoutPanel();
			this.N_P2_Page = new System.Windows.Forms.NumericUpDown();
			this.L_P2_Page = new System.Windows.Forms.Label();
			this.CBX_P2_Scale = new System.Windows.Forms.ComboBox();
			this.CB_P2_AutoSave = new System.Windows.Forms.CheckBox();
			this.P_P2_Pattern = new System.Windows.Forms.Panel();
			this.TLP_P2_Details = new System.Windows.Forms.TableLayoutPanel();
			this.P_P2_Menu = new System.Windows.Forms.Panel();
			this.MS_P2_Menu = new System.Windows.Forms.MenuStrip();
			this.TSMI_FieldSwitch = new System.Windows.Forms.ToolStripMenuItem();
			this.TSMI_DetailSwitch = new System.Windows.Forms.ToolStripMenuItem();
			this.TSMI_PageSwitch = new System.Windows.Forms.ToolStripMenuItem();
			this.P_P2_Editor = new System.Windows.Forms.Panel();
			this.TLP_P2_LabelDetails = new System.Windows.Forms.TableLayoutPanel();
			this.CB_P2_AdditionalMargin = new System.Windows.Forms.CheckBox();
			this.L_P2_AddMargin = new System.Windows.Forms.Label();
			this.CBX_P2_StickPoint = new System.Windows.Forms.ComboBox();
			this.L_P2_StickPoint = new System.Windows.Forms.Label();
			this.CB_P2_UseImageMargin = new System.Windows.Forms.CheckBox();
			this.CB_P2_DrawFrameOutside = new System.Windows.Forms.CheckBox();
			this.L_P2_ImageSettings = new System.Windows.Forms.Label();
			this.CB_P2_StaticImage = new System.Windows.Forms.CheckBox();
			this.CB_P2_DynamicImage = new System.Windows.Forms.CheckBox();
			this.CBX_P2_ImageSettings = new System.Windows.Forms.ComboBox();
			this.N_P2_MarginLR = new System.Windows.Forms.NumericUpDown();
			this.N_P2_MarginTB = new System.Windows.Forms.NumericUpDown();
			this.G_P2_Generator = new System.Windows.Forms.GroupBox();
			this.TLP_P2_Generator = new System.Windows.Forms.TableLayoutPanel();
			this.CB_P2_DrawBorder = new System.Windows.Forms.CheckBox();
			this.CB_P2_DrawColor = new System.Windows.Forms.CheckBox();
			this.CB_P2_DynamicText = new System.Windows.Forms.CheckBox();
			this.CB_P2_StaticText = new System.Windows.Forms.CheckBox();
			this.TLP_P2_PageDetails = new System.Windows.Forms.TableLayoutPanel();
			this.CB_P2_DrawOutside = new System.Windows.Forms.CheckBox();
			this.CB_P2_ApplyMargin = new System.Windows.Forms.CheckBox();
			this.CBX_P2_PageImageSettings = new System.Windows.Forms.ComboBox();
			this.L_P2_PageImageSet = new System.Windows.Forms.Label();
			this.TB_P2_PageWidth = new System.Windows.Forms.TextBox();
			this.TB_P2_PageHeight = new System.Windows.Forms.TextBox();
			this.L_P2_PageWidth = new System.Windows.Forms.Label();
			this.L_P2_PageHeight = new System.Windows.Forms.Label();
			this.CB_P2_DrawPageImage = new System.Windows.Forms.CheckBox();
			this.G_P2_PageApperance = new System.Windows.Forms.GroupBox();
			this.TLP_P2_PageAppearance = new System.Windows.Forms.TableLayoutPanel();
			this.TB_P2_PageColor = new System.Windows.Forms.TextBox();
			this.B_P2_PageColor = new System.Windows.Forms.Button();
			this.TB_P2_PageImage = new System.Windows.Forms.TextBox();
			this.B_P2_PageImage = new System.Windows.Forms.Button();
			this.G_P2_GeneratePDF = new System.Windows.Forms.GroupBox();
			this.TLP_P2_GeneratePDF = new System.Windows.Forms.TableLayoutPanel();
			this.CB_P2_DrawPageColor = new System.Windows.Forms.CheckBox();
			this.TLP_P2_Field = new System.Windows.Forms.TableLayoutPanel();
			this.CBX_P2_TextTransform = new System.Windows.Forms.ComboBox();
			this.L_P2_TextTransform = new System.Windows.Forms.Label();
			this.L_P2_Height = new System.Windows.Forms.Label();
			this.N_P2_BorderSize = new System.Windows.Forms.NumericUpDown();
			this.N_P2_Padding = new System.Windows.Forms.NumericUpDown();
			this.L_P2_Padding = new System.Windows.Forms.Label();
			this.L_P2_TextPosition = new System.Windows.Forms.Label();
			this.TB_P2_LabelName = new System.Windows.Forms.TextBox();
			this.N_P2_PosY = new System.Windows.Forms.NumericUpDown();
			this.N_P2_PosX = new System.Windows.Forms.NumericUpDown();
			this.L_P2_PosX = new System.Windows.Forms.Label();
			this.L_P2_Width = new System.Windows.Forms.Label();
			this.N_P2_Width = new System.Windows.Forms.NumericUpDown();
			this.N_P2_Height = new System.Windows.Forms.NumericUpDown();
			this.L_P2_BorderWidth = new System.Windows.Forms.Label();
			this.CBX_P2_TextPosition = new System.Windows.Forms.ComboBox();
			this.L_P2_PosY = new System.Windows.Forms.Label();
			this.G_P2_Appearance = new System.Windows.Forms.GroupBox();
			this.TLP_P2_FieldAppearance = new System.Windows.Forms.TableLayoutPanel();
			this.B_P2_BackImage = new System.Windows.Forms.Button();
			this.TB_P2_BackImage = new System.Windows.Forms.TextBox();
			this.B_P2_BackColor = new System.Windows.Forms.Button();
			this.TB_P2_BackColor = new System.Windows.Forms.TextBox();
			this.TB_P2_BorderColor = new System.Windows.Forms.TextBox();
			this.B_P2_BorderColor = new System.Windows.Forms.Button();
			this.G_P2_Font = new System.Windows.Forms.GroupBox();
			this.TLP_P2_FontSettings = new System.Windows.Forms.TableLayoutPanel();
			this.TB_P2_FontName = new System.Windows.Forms.TextBox();
			this.B_P2_FontName = new System.Windows.Forms.Button();
			this.TB_P2_FontColor = new System.Windows.Forms.TextBox();
			this.B_P2_FontColor = new System.Windows.Forms.Button();
			this.SC_P3_Generator = new System.Windows.Forms.SplitContainer();
			this.TLP_PageList = new System.Windows.Forms.TableLayoutPanel();
			this.CB_P3_CollatePages = new System.Windows.Forms.CheckBox();
			this.SC_P1_Main = new System.Windows.Forms.SplitContainer();
			this.SC_P1_Details = new System.Windows.Forms.SplitContainer();
			this.RTB_P1_Details = new System.Windows.Forms.RichTextBox();
			this.TLP_Main = new System.Windows.Forms.TableLayoutPanel();
			this.TLP_P1_StatusBar = new System.Windows.Forms.TableLayoutPanel();
			this.CB_P1_ShowDetails = new System.Windows.Forms.CheckBox();
			this.TLP_Generator = new System.Windows.Forms.TableLayoutPanel();
			this.TLP_P3_StatusBar = new System.Windows.Forms.TableLayoutPanel();
			this.TLP_P3_InfoControls = new System.Windows.Forms.TableLayoutPanel();
			this.FLP_P3_Rows = new System.Windows.Forms.FlowLayoutPanel();
			this.N_P3_Rows = new System.Windows.Forms.NumericUpDown();
			this.L_P3_Rows = new System.Windows.Forms.Label();
			this.FLP_P3_Page = new System.Windows.Forms.FlowLayoutPanel();
			this.N_P3_Page = new System.Windows.Forms.NumericUpDown();
			this.L_P3_Page = new System.Windows.Forms.Label();
			this.CBX_P3_Scale = new System.Windows.Forms.ComboBox();
			this.TLP_Pattern = new System.Windows.Forms.TableLayoutPanel();
			this.SC_P2_Pattern = new System.Windows.Forms.SplitContainer();
			this.TLP_P2_StatusBar = new System.Windows.Forms.TableLayoutPanel();
			this.TLP_P2_Buttons = new System.Windows.Forms.TableLayoutPanel();
			this.BW_Updates = new System.ComponentModel.BackgroundWorker();
			this.TP_Tooltip = new System.Windows.Forms.ToolTip(this.components);
			((System.ComponentModel.ISupportInitialize)(this.N_P1_Page)).BeginInit();
			this.CMS_Pattern.SuspendLayout();
			this.MS_Main.SuspendLayout();
			this.CMS_Page.SuspendLayout();
			this.CMS_Label.SuspendLayout();
			this.TLP_P3_Buttons.SuspendLayout();
			this.TLP_P2_InfoControls.SuspendLayout();
			this.FLP_P2_Page.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.N_P2_Page)).BeginInit();
			this.TLP_P2_Details.SuspendLayout();
			this.P_P2_Menu.SuspendLayout();
			this.MS_P2_Menu.SuspendLayout();
			this.P_P2_Editor.SuspendLayout();
			this.TLP_P2_LabelDetails.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.N_P2_MarginLR)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.N_P2_MarginTB)).BeginInit();
			this.G_P2_Generator.SuspendLayout();
			this.TLP_P2_Generator.SuspendLayout();
			this.TLP_P2_PageDetails.SuspendLayout();
			this.G_P2_PageApperance.SuspendLayout();
			this.TLP_P2_PageAppearance.SuspendLayout();
			this.G_P2_GeneratePDF.SuspendLayout();
			this.TLP_P2_GeneratePDF.SuspendLayout();
			this.TLP_P2_Field.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.N_P2_BorderSize)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.N_P2_Padding)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.N_P2_PosY)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.N_P2_PosX)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.N_P2_Width)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.N_P2_Height)).BeginInit();
			this.G_P2_Appearance.SuspendLayout();
			this.TLP_P2_FieldAppearance.SuspendLayout();
			this.G_P2_Font.SuspendLayout();
			this.TLP_P2_FontSettings.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.SC_P3_Generator)).BeginInit();
			this.SC_P3_Generator.Panel1.SuspendLayout();
			this.SC_P3_Generator.Panel2.SuspendLayout();
			this.SC_P3_Generator.SuspendLayout();
			this.TLP_PageList.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.SC_P1_Main)).BeginInit();
			this.SC_P1_Main.Panel1.SuspendLayout();
			this.SC_P1_Main.Panel2.SuspendLayout();
			this.SC_P1_Main.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.SC_P1_Details)).BeginInit();
			this.SC_P1_Details.Panel1.SuspendLayout();
			this.SC_P1_Details.Panel2.SuspendLayout();
			this.SC_P1_Details.SuspendLayout();
			this.TLP_Main.SuspendLayout();
			this.TLP_P1_StatusBar.SuspendLayout();
			this.TLP_Generator.SuspendLayout();
			this.TLP_P3_StatusBar.SuspendLayout();
			this.TLP_P3_InfoControls.SuspendLayout();
			this.FLP_P3_Rows.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.N_P3_Rows)).BeginInit();
			this.FLP_P3_Page.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.N_P3_Page)).BeginInit();
			this.TLP_Pattern.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.SC_P2_Pattern)).BeginInit();
			this.SC_P2_Pattern.Panel1.SuspendLayout();
			this.SC_P2_Pattern.Panel2.SuspendLayout();
			this.SC_P2_Pattern.SuspendLayout();
			this.TLP_P2_StatusBar.SuspendLayout();
			this.TLP_P2_Buttons.SuspendLayout();
			this.SuspendLayout();
			// 
			// P_P1_Preview
			// 
			this.P_P1_Preview.AutoScroll = true;
			this.P_P1_Preview.BackColor = System.Drawing.SystemColors.ScrollBar;
			this.P_P1_Preview.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.P_P1_Preview.Dock = System.Windows.Forms.DockStyle.Fill;
			this.P_P1_Preview.Location = new System.Drawing.Point(0, 0);
			this.P_P1_Preview.Margin = new System.Windows.Forms.Padding(0);
			this.P_P1_Preview.Name = "P_P1_Preview";
			this.P_P1_Preview.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.P_P1_Preview.Size = new System.Drawing.Size(479, 438);
			this.P_P1_Preview.TabIndex = 6;
			this.P_P1_Preview.MouseDown += new System.Windows.Forms.MouseEventHandler(this.P_P1_Preview_MouseDown);
			this.P_P1_Preview.Resize += new System.EventHandler(this.P_P1_Preview_Resize);
			// 
			// N_P1_Page
			// 
			this.N_P1_Page.Dock = System.Windows.Forms.DockStyle.Fill;
			this.N_P1_Page.Enabled = false;
			this.N_P1_Page.Location = new System.Drawing.Point(670, 7);
			this.N_P1_Page.Margin = new System.Windows.Forms.Padding(0, 6, 5, 0);
			this.N_P1_Page.Maximum = new decimal(new int[] {
			1,
			0,
			0,
			0});
			this.N_P1_Page.Minimum = new decimal(new int[] {
			1,
			0,
			0,
			0});
			this.N_P1_Page.Name = "N_P1_Page";
			this.N_P1_Page.Size = new System.Drawing.Size(49, 20);
			this.N_P1_Page.TabIndex = 5;
			this.N_P1_Page.Value = new decimal(new int[] {
			1,
			0,
			0,
			0});
			this.N_P1_Page.ValueChanged += new System.EventHandler(this.N_P1_Page_ValueChanged);
			// 
			// L_P1_Page
			// 
			this.L_P1_Page.AutoSize = true;
			this.L_P1_Page.Dock = System.Windows.Forms.DockStyle.Fill;
			this.L_P1_Page.Location = new System.Drawing.Point(587, 1);
			this.L_P1_Page.Name = "L_P1_Page";
			this.L_P1_Page.Size = new System.Drawing.Size(80, 31);
			this.L_P1_Page.TabIndex = 22;
			this.L_P1_Page.Text = "Strona:";
			this.L_P1_Page.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// B_P1_Delete
			// 
			this.B_P1_Delete.Dock = System.Windows.Forms.DockStyle.Fill;
			this.B_P1_Delete.Enabled = false;
			this.B_P1_Delete.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.B_P1_Delete.Location = new System.Drawing.Point(122, 4);
			this.B_P1_Delete.Margin = new System.Windows.Forms.Padding(2, 3, 5, 3);
			this.B_P1_Delete.Name = "B_P1_Delete";
			this.B_P1_Delete.Size = new System.Drawing.Size(113, 25);
			this.B_P1_Delete.TabIndex = 3;
			this.B_P1_Delete.Text = "Usuń";
			this.B_P1_Delete.Click += new System.EventHandler(this.B_P1_Delete_Click);
			// 
			// B_P1_New
			// 
			this.B_P1_New.Dock = System.Windows.Forms.DockStyle.Fill;
			this.B_P1_New.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.B_P1_New.Location = new System.Drawing.Point(5, 4);
			this.B_P1_New.Margin = new System.Windows.Forms.Padding(5, 3, 2, 3);
			this.B_P1_New.Name = "B_P1_New";
			this.B_P1_New.Size = new System.Drawing.Size(113, 25);
			this.B_P1_New.TabIndex = 2;
			this.B_P1_New.Text = "Nowy wzór";
			this.B_P1_New.Click += new System.EventHandler(this.TSMI_MenuNewPattern_Click);
			// 
			// TV_P1_Patterns
			// 
			this.TV_P1_Patterns.ContextMenuStrip = this.CMS_Pattern;
			this.TV_P1_Patterns.Dock = System.Windows.Forms.DockStyle.Fill;
			this.TV_P1_Patterns.FullRowSelect = true;
			this.TV_P1_Patterns.HideSelection = false;
			this.TV_P1_Patterns.Location = new System.Drawing.Point(0, 0);
			this.TV_P1_Patterns.Margin = new System.Windows.Forms.Padding(0);
			this.TV_P1_Patterns.Name = "TV_P1_Patterns";
			this.TV_P1_Patterns.ShowLines = false;
			this.TV_P1_Patterns.ShowPlusMinus = false;
			this.TV_P1_Patterns.ShowRootLines = false;
			this.TV_P1_Patterns.Size = new System.Drawing.Size(230, 438);
			this.TV_P1_Patterns.TabIndex = 1;
			this.TV_P1_Patterns.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.TV_P1_Patterns_AfterSelect);
			this.TV_P1_Patterns.DoubleClick += new System.EventHandler(this.TSMI_EditPattern_Click);
			this.TV_P1_Patterns.MouseDown += new System.Windows.Forms.MouseEventHandler(this.TV_P1_Patterns_MouseDown);
			// 
			// CMS_Pattern
			// 
			this.CMS_Pattern.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.TSMI_NewPattern,
			this.TSMI_Separator_A1,
			this.TSMI_EditPattern,
			this.TSMI_LoadData,
			this.TSMI_Separator_A2,
			this.TSMI_ImportPattern,
			this.TSMI_ExportPattern,
			this.TSMI_Separator_A3,
			this.TSMI_RemovePattern});
			this.CMS_Pattern.Name = "icPattern";
			this.CMS_Pattern.Size = new System.Drawing.Size(186, 154);
			this.CMS_Pattern.Opening += new System.ComponentModel.CancelEventHandler(this.CMS_Pattern_Opening);
			// 
			// TSMI_NewPattern
			// 
			this.TSMI_NewPattern.Name = "TSMI_NewPattern";
			this.TSMI_NewPattern.ShortcutKeyDisplayString = "Ctrl+N";
			this.TSMI_NewPattern.Size = new System.Drawing.Size(185, 22);
			this.TSMI_NewPattern.Text = "Nowy wzór...";
			this.TSMI_NewPattern.Click += new System.EventHandler(this.TSMI_MenuNewPattern_Click);
			// 
			// TSMI_Separator_A1
			// 
			this.TSMI_Separator_A1.Name = "TSMI_Separator_A1";
			this.TSMI_Separator_A1.Size = new System.Drawing.Size(182, 6);
			// 
			// TSMI_EditPattern
			// 
			this.TSMI_EditPattern.Name = "TSMI_EditPattern";
			this.TSMI_EditPattern.Size = new System.Drawing.Size(185, 22);
			this.TSMI_EditPattern.Text = "Edytuj zaznaczony";
			this.TSMI_EditPattern.Click += new System.EventHandler(this.TSMI_EditPattern_Click);
			// 
			// TSMI_LoadData
			// 
			this.TSMI_LoadData.Name = "TSMI_LoadData";
			this.TSMI_LoadData.ShortcutKeyDisplayString = "";
			this.TSMI_LoadData.Size = new System.Drawing.Size(185, 22);
			this.TSMI_LoadData.Text = "Wczytaj dane...";
			this.TSMI_LoadData.Click += new System.EventHandler(this.TSMI_LoadData_Click);
			// 
			// TSMI_Separator_A2
			// 
			this.TSMI_Separator_A2.Name = "TSMI_Separator_A2";
			this.TSMI_Separator_A2.Size = new System.Drawing.Size(182, 6);
			// 
			// TSMI_ImportPattern
			// 
			this.TSMI_ImportPattern.Name = "TSMI_ImportPattern";
			this.TSMI_ImportPattern.ShortcutKeyDisplayString = "Alt+I";
			this.TSMI_ImportPattern.Size = new System.Drawing.Size(185, 22);
			this.TSMI_ImportPattern.Text = "Importuj...";
			this.TSMI_ImportPattern.Click += new System.EventHandler(this.TSMI_Import_Click);
			// 
			// TSMI_ExportPattern
			// 
			this.TSMI_ExportPattern.Name = "TSMI_ExportPattern";
			this.TSMI_ExportPattern.Size = new System.Drawing.Size(185, 22);
			this.TSMI_ExportPattern.Text = "Eksportuj...";
			this.TSMI_ExportPattern.Click += new System.EventHandler(this.TSMI_ExportPattern_Click);
			// 
			// TSMI_Separator_A3
			// 
			this.TSMI_Separator_A3.Name = "TSMI_Separator_A3";
			this.TSMI_Separator_A3.Size = new System.Drawing.Size(182, 6);
			// 
			// TSMI_RemovePattern
			// 
			this.TSMI_RemovePattern.Name = "TSMI_RemovePattern";
			this.TSMI_RemovePattern.ShortcutKeyDisplayString = "";
			this.TSMI_RemovePattern.Size = new System.Drawing.Size(185, 22);
			this.TSMI_RemovePattern.Text = "Usuń wzór";
			this.TSMI_RemovePattern.Click += new System.EventHandler(this.B_P1_Delete_Click);
			// 
			// MS_Main
			// 
			this.MS_Main.BackColor = System.Drawing.SystemColors.ControlLight;
			this.MS_Main.GripMargin = new System.Windows.Forms.Padding(2);
			this.MS_Main.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.TSMI_Pattern,
			this.TSMI_PrintPreview,
			this.TSMI_PatternEditor,
			this.TSMI_HomePage,
			this.TSMI_Tools,
			this.TSMI_Settings,
			this.TSMI_Language,
			this.TSMI_Program});
			this.MS_Main.Location = new System.Drawing.Point(0, 0);
			this.MS_Main.Name = "MS_Main";
			this.MS_Main.Padding = new System.Windows.Forms.Padding(6, 4, 6, 5);
			this.MS_Main.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
			this.MS_Main.Size = new System.Drawing.Size(724, 32);
			this.MS_Main.TabIndex = 1;
			this.MS_Main.Text = "Menu";
			this.MS_Main.Paint += new System.Windows.Forms.PaintEventHandler(this.MS_Main_Paint);
			// 
			// TSMI_Pattern
			// 
			this.TSMI_Pattern.BackColor = System.Drawing.Color.Transparent;
			this.TSMI_Pattern.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.TSMI_MenuNewPattern,
			this.TSMI_RecentOpen,
			this.TSS_Separator_P2,
			this.TSMI_Import,
			this.TSMI_ExportAll,
			this.TSS_Separator_P3,
			this.TSMI_Close});
			this.TSMI_Pattern.Name = "TSMI_Pattern";
			this.TSMI_Pattern.Padding = new System.Windows.Forms.Padding(6, 2, 6, 2);
			this.TSMI_Pattern.Size = new System.Drawing.Size(50, 23);
			this.TSMI_Pattern.Text = "&Wzór";
			// 
			// TSMI_MenuNewPattern
			// 
			this.TSMI_MenuNewPattern.Name = "TSMI_MenuNewPattern";
			this.TSMI_MenuNewPattern.ShortcutKeyDisplayString = "";
			this.TSMI_MenuNewPattern.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
			this.TSMI_MenuNewPattern.Size = new System.Drawing.Size(220, 22);
			this.TSMI_MenuNewPattern.Text = "Nowy...";
			this.TSMI_MenuNewPattern.Click += new System.EventHandler(this.TSMI_MenuNewPattern_Click);
			// 
			// TSMI_RecentOpen
			// 
			this.TSMI_RecentOpen.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.TSS_Separator_P1,
			this.TSMI_ClearRecent});
			this.TSMI_RecentOpen.Enabled = false;
			this.TSMI_RecentOpen.Name = "TSMI_RecentOpen";
			this.TSMI_RecentOpen.Size = new System.Drawing.Size(220, 22);
			this.TSMI_RecentOpen.Text = "Ostatnio otwierane";
			// 
			// TSS_Separator_P1
			// 
			this.TSS_Separator_P1.Name = "TSS_Separator_P1";
			this.TSS_Separator_P1.Size = new System.Drawing.Size(184, 6);
			// 
			// TSMI_ClearRecent
			// 
			this.TSMI_ClearRecent.Name = "TSMI_ClearRecent";
			this.TSMI_ClearRecent.ShortcutKeyDisplayString = "";
			this.TSMI_ClearRecent.Size = new System.Drawing.Size(187, 22);
			this.TSMI_ClearRecent.Text = "Wyczyść listę wzorów";
			this.TSMI_ClearRecent.Click += new System.EventHandler(this.TSMI_ClearRecent_Click);
			// 
			// TSS_Separator_P2
			// 
			this.TSS_Separator_P2.Name = "TSS_Separator_P2";
			this.TSS_Separator_P2.Size = new System.Drawing.Size(217, 6);
			// 
			// TSMI_Import
			// 
			this.TSMI_Import.Name = "TSMI_Import";
			this.TSMI_Import.ShortcutKeyDisplayString = "";
			this.TSMI_Import.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.I)));
			this.TSMI_Import.Size = new System.Drawing.Size(220, 22);
			this.TSMI_Import.Text = "Importuj...";
			this.TSMI_Import.Click += new System.EventHandler(this.TSMI_Import_Click);
			// 
			// TSMI_ExportAll
			// 
			this.TSMI_ExportAll.Name = "TSMI_ExportAll";
			this.TSMI_ExportAll.ShortcutKeyDisplayString = "";
			this.TSMI_ExportAll.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.E)));
			this.TSMI_ExportAll.Size = new System.Drawing.Size(220, 22);
			this.TSMI_ExportAll.Text = "Eksportuj wszystkie...";
			this.TSMI_ExportAll.Click += new System.EventHandler(this.TSMI_ExportPattern_Click);
			// 
			// TSS_Separator_P3
			// 
			this.TSS_Separator_P3.Name = "TSS_Separator_P3";
			this.TSS_Separator_P3.Size = new System.Drawing.Size(217, 6);
			// 
			// TSMI_Close
			// 
			this.TSMI_Close.Name = "TSMI_Close";
			this.TSMI_Close.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
			this.TSMI_Close.Size = new System.Drawing.Size(220, 22);
			this.TSMI_Close.Text = "Zakończ program";
			this.TSMI_Close.Click += new System.EventHandler(this.TSMI_Close_Click);
			// 
			// TSMI_PrintPreview
			// 
			this.TSMI_PrintPreview.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
			this.TSMI_PrintPreview.BackColor = System.Drawing.Color.Transparent;
			this.TSMI_PrintPreview.Enabled = false;
			this.TSMI_PrintPreview.Name = "TSMI_PrintPreview";
			this.TSMI_PrintPreview.Padding = new System.Windows.Forms.Padding(6, 2, 6, 2);
			this.TSMI_PrintPreview.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.D3)));
			this.TSMI_PrintPreview.Size = new System.Drawing.Size(64, 23);
			this.TSMI_PrintPreview.Text = "Wydruk";
			this.TSMI_PrintPreview.Click += new System.EventHandler(this.TSMI_PrintPreview_Click);
			// 
			// TSMI_PatternEditor
			// 
			this.TSMI_PatternEditor.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
			this.TSMI_PatternEditor.BackColor = System.Drawing.Color.Transparent;
			this.TSMI_PatternEditor.Enabled = false;
			this.TSMI_PatternEditor.Name = "TSMI_PatternEditor";
			this.TSMI_PatternEditor.Padding = new System.Windows.Forms.Padding(6, 2, 6, 2);
			this.TSMI_PatternEditor.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.D2)));
			this.TSMI_PatternEditor.Size = new System.Drawing.Size(57, 23);
			this.TSMI_PatternEditor.Text = "Edytor";
			this.TSMI_PatternEditor.Click += new System.EventHandler(this.TSMI_PatternEditor_Click);
			// 
			// TSMI_HomePage
			// 
			this.TSMI_HomePage.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
			this.TSMI_HomePage.BackColor = System.Drawing.Color.Transparent;
			this.TSMI_HomePage.Enabled = false;
			this.TSMI_HomePage.Name = "TSMI_HomePage";
			this.TSMI_HomePage.Padding = new System.Windows.Forms.Padding(6, 2, 6, 2);
			this.TSMI_HomePage.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.D1)));
			this.TSMI_HomePage.Size = new System.Drawing.Size(63, 23);
			this.TSMI_HomePage.Text = "Główna";
			this.TSMI_HomePage.Click += new System.EventHandler(this.TSMI_HomePage_Click);
			// 
			// TSMI_Tools
			// 
			this.TSMI_Tools.BackColor = System.Drawing.Color.Transparent;
			this.TSMI_Tools.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.TSMI_LoadDataFile,
			this.TSMI_CreateEmpty,
			this.TSS_Separator_T1,
			this.TSMI_EditColumns,
			this.TSMI_EditRows,
			this.TSMI_CreateDatabase,
			this.TSMI_ConnectDatabase,
			this.TSMI_DataBackup,
			this.TSS_Separator_T2,
			this.TSMI_CloseData,
			this.TSMI_SaveData});
			this.TSMI_Tools.Name = "TSMI_Tools";
			this.TSMI_Tools.Padding = new System.Windows.Forms.Padding(6, 2, 6, 2);
			this.TSMI_Tools.Size = new System.Drawing.Size(74, 23);
			this.TSMI_Tools.Text = "&Narzędzia";
			// 
			// TSMI_LoadDataFile
			// 
			this.TSMI_LoadDataFile.Name = "TSMI_LoadDataFile";
			this.TSMI_LoadDataFile.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D)));
			this.TSMI_LoadDataFile.Size = new System.Drawing.Size(240, 22);
			this.TSMI_LoadDataFile.Text = "Wczytaj plik z danymi...";
			this.TSMI_LoadDataFile.Click += new System.EventHandler(this.TSMI_LoadDataFile_Click);
			// 
			// TSMI_CreateEmpty
			// 
			this.TSMI_CreateEmpty.Name = "TSMI_CreateEmpty";
			this.TSMI_CreateEmpty.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.M)));
			this.TSMI_CreateEmpty.Size = new System.Drawing.Size(240, 22);
			this.TSMI_CreateEmpty.Text = "Utwórz bazę w pamięci";
			this.TSMI_CreateEmpty.Click += new System.EventHandler(this.TSMI_CreateEmpty_Click);
			// 
			// TSS_Separator_T1
			// 
			this.TSS_Separator_T1.Name = "TSS_Separator_T1";
			this.TSS_Separator_T1.Size = new System.Drawing.Size(237, 6);
			// 
			// TSMI_EditColumns
			// 
			this.TSMI_EditColumns.Enabled = false;
			this.TSMI_EditColumns.Name = "TSMI_EditColumns";
			this.TSMI_EditColumns.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.C)));
			this.TSMI_EditColumns.Size = new System.Drawing.Size(240, 22);
			this.TSMI_EditColumns.Text = "Edytuj kolumny...";
			this.TSMI_EditColumns.Click += new System.EventHandler(this.TSMI_EditColumns_Click);
			// 
			// TSMI_EditRows
			// 
			this.TSMI_EditRows.Enabled = false;
			this.TSMI_EditRows.Name = "TSMI_EditRows";
			this.TSMI_EditRows.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.D)));
			this.TSMI_EditRows.Size = new System.Drawing.Size(240, 22);
			this.TSMI_EditRows.Text = "Edytuj wiersze...";
			this.TSMI_EditRows.Click += new System.EventHandler(this.TSMI_EditRows_Click);
			// 
			// TSMI_CreateDatabase
			// 
			this.TSMI_CreateDatabase.Name = "TSMI_CreateDatabase";
			this.TSMI_CreateDatabase.Size = new System.Drawing.Size(240, 22);
			this.TSMI_CreateDatabase.Text = "Utwórz bazę danych";
			this.TSMI_CreateDatabase.Visible = false;
			// 
			// TSMI_ConnectDatabase
			// 
			this.TSMI_ConnectDatabase.Name = "TSMI_ConnectDatabase";
			this.TSMI_ConnectDatabase.Size = new System.Drawing.Size(240, 22);
			this.TSMI_ConnectDatabase.Text = "Połącz z bazą danych";
			this.TSMI_ConnectDatabase.Visible = false;
			// 
			// TSMI_DataBackup
			// 
			this.TSMI_DataBackup.Name = "TSMI_DataBackup";
			this.TSMI_DataBackup.Size = new System.Drawing.Size(240, 22);
			this.TSMI_DataBackup.Text = "Kopia zapasowa";
			this.TSMI_DataBackup.Visible = false;
			// 
			// TSS_Separator_T2
			// 
			this.TSS_Separator_T2.Name = "TSS_Separator_T2";
			this.TSS_Separator_T2.Size = new System.Drawing.Size(237, 6);
			// 
			// TSMI_CloseData
			// 
			this.TSMI_CloseData.Enabled = false;
			this.TSMI_CloseData.Name = "TSMI_CloseData";
			this.TSMI_CloseData.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.X)));
			this.TSMI_CloseData.Size = new System.Drawing.Size(240, 22);
			this.TSMI_CloseData.Text = "Zamknij źródło danych";
			this.TSMI_CloseData.Click += new System.EventHandler(this.TSMI_CloseData_Click);
			// 
			// TSMI_SaveData
			// 
			this.TSMI_SaveData.Enabled = false;
			this.TSMI_SaveData.Name = "TSMI_SaveData";
			this.TSMI_SaveData.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.S)));
			this.TSMI_SaveData.Size = new System.Drawing.Size(240, 22);
			this.TSMI_SaveData.Text = "Zapisz bazę do pliku...";
			this.TSMI_SaveData.Click += new System.EventHandler(this.TSMI_SaveData_Click);
			// 
			// TSMI_Settings
			// 
			this.TSMI_Settings.BackColor = System.Drawing.Color.Transparent;
			this.TSMI_Settings.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.TSMI_Generator,
			this.TSMI_Editor,
			this.TSMI_General});
			this.TSMI_Settings.Enabled = false;
			this.TSMI_Settings.Name = "TSMI_Settings";
			this.TSMI_Settings.Padding = new System.Windows.Forms.Padding(6, 2, 6, 2);
			this.TSMI_Settings.Size = new System.Drawing.Size(80, 23);
			this.TSMI_Settings.Text = "&Ustawienia";
			this.TSMI_Settings.Visible = false;
			// 
			// TSMI_Generator
			// 
			this.TSMI_Generator.Enabled = false;
			this.TSMI_Generator.Name = "TSMI_Generator";
			this.TSMI_Generator.Size = new System.Drawing.Size(159, 22);
			this.TSMI_Generator.Text = "Generator PDF...";
			// 
			// TSMI_Editor
			// 
			this.TSMI_Editor.Enabled = false;
			this.TSMI_Editor.Name = "TSMI_Editor";
			this.TSMI_Editor.Size = new System.Drawing.Size(159, 22);
			this.TSMI_Editor.Text = "Edytor...";
			// 
			// TSMI_General
			// 
			this.TSMI_General.Enabled = false;
			this.TSMI_General.Name = "TSMI_General";
			this.TSMI_General.Size = new System.Drawing.Size(159, 22);
			this.TSMI_General.Text = "Ogólne...";
			// 
			// TSMI_Language
			// 
			this.TSMI_Language.Name = "TSMI_Language";
			this.TSMI_Language.Size = new System.Drawing.Size(46, 23);
			this.TSMI_Language.Text = "Język";
			// 
			// TSMI_Program
			// 
			this.TSMI_Program.BackColor = System.Drawing.Color.Transparent;
			this.TSMI_Program.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.TSMI_Info,
			this.TSMI_Help,
			this.TSMI_Update});
			this.TSMI_Program.Name = "TSMI_Program";
			this.TSMI_Program.Padding = new System.Windows.Forms.Padding(6, 2, 6, 2);
			this.TSMI_Program.Size = new System.Drawing.Size(69, 23);
			this.TSMI_Program.Text = "&Program";
			// 
			// TSMI_Info
			// 
			this.TSMI_Info.Name = "TSMI_Info";
			this.TSMI_Info.ShortcutKeys = System.Windows.Forms.Keys.F1;
			this.TSMI_Info.Size = new System.Drawing.Size(203, 22);
			this.TSMI_Info.Text = "Informacje";
			this.TSMI_Info.Click += new System.EventHandler(this.TSMI_Info_Click);
			// 
			// TSMI_Help
			// 
			this.TSMI_Help.Enabled = false;
			this.TSMI_Help.Name = "TSMI_Help";
			this.TSMI_Help.Size = new System.Drawing.Size(203, 22);
			this.TSMI_Help.Text = "Pomoc";
			this.TSMI_Help.Visible = false;
			// 
			// TSMI_Update
			// 
			this.TSMI_Update.Name = "TSMI_Update";
			this.TSMI_Update.ShortcutKeys = System.Windows.Forms.Keys.F9;
			this.TSMI_Update.Size = new System.Drawing.Size(203, 22);
			this.TSMI_Update.Text = "Aktualizuj program...";
			this.TSMI_Update.Click += new System.EventHandler(this.TSMI_Update_Click);
			// 
			// CMS_Page
			// 
			this.CMS_Page.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.TSMI_AddField,
			this.TSMI_RemoveAll,
			this.TSMI_Separator_B0,
			this.TSMI_PageColor,
			this.TSMI_PageDrawColor,
			this.TSMI_PageImage,
			this.TSMI_PageClear,
			this.TSMI_Separator_B1,
			this.TSMI_PageDrawImage,
			this.TSMI_Separator_B2,
			this.TSMI_AddPage,
			this.TSMI_RemovePage});
			this.CMS_Page.Name = "cmPrevPage";
			this.CMS_Page.Size = new System.Drawing.Size(180, 220);
			this.CMS_Page.Opening += new System.ComponentModel.CancelEventHandler(this.CMS_Page_Opening);
			// 
			// TSMI_AddField
			// 
			this.TSMI_AddField.Name = "TSMI_AddField";
			this.TSMI_AddField.Size = new System.Drawing.Size(179, 22);
			this.TSMI_AddField.Text = "Dodaj pole";
			this.TSMI_AddField.Click += new System.EventHandler(this.TSMI_AddField_Click);
			// 
			// TSMI_RemoveAll
			// 
			this.TSMI_RemoveAll.Name = "TSMI_RemoveAll";
			this.TSMI_RemoveAll.Size = new System.Drawing.Size(179, 22);
			this.TSMI_RemoveAll.Text = "Usuń wszystkie pola";
			this.TSMI_RemoveAll.Click += new System.EventHandler(this.TSMI_RemoveAll_Click);
			// 
			// TSMI_Separator_B0
			// 
			this.TSMI_Separator_B0.Name = "TSMI_Separator_B0";
			this.TSMI_Separator_B0.Size = new System.Drawing.Size(176, 6);
			// 
			// TSMI_PageColor
			// 
			this.TSMI_PageColor.Name = "TSMI_PageColor";
			this.TSMI_PageColor.Size = new System.Drawing.Size(179, 22);
			this.TSMI_PageColor.Text = "Kolor tła strony...";
			this.TSMI_PageColor.Click += new System.EventHandler(this.B_P2_PageColor_Click);
			// 
			// TSMI_PageDrawColor
			// 
			this.TSMI_PageDrawColor.CheckOnClick = true;
			this.TSMI_PageDrawColor.Name = "TSMI_PageDrawColor";
			this.TSMI_PageDrawColor.Size = new System.Drawing.Size(179, 22);
			this.TSMI_PageDrawColor.Text = "Rysuj kolor strony";
			this.TSMI_PageDrawColor.CheckedChanged += new System.EventHandler(this.CB_P2_DrawPageColor_CheckedChanged);
			// 
			// TSMI_PageImage
			// 
			this.TSMI_PageImage.Name = "TSMI_PageImage";
			this.TSMI_PageImage.Size = new System.Drawing.Size(179, 22);
			this.TSMI_PageImage.Text = "Obraz tła strony...";
			this.TSMI_PageImage.Click += new System.EventHandler(this.B_P2_PageImage_Click);
			// 
			// TSMI_PageClear
			// 
			this.TSMI_PageClear.Name = "TSMI_PageClear";
			this.TSMI_PageClear.Size = new System.Drawing.Size(179, 22);
			this.TSMI_PageClear.Text = "Wyczyść tło";
			this.TSMI_PageClear.Click += new System.EventHandler(this.TSMI_PageClear_Click);
			// 
			// TSMI_Separator_B1
			// 
			this.TSMI_Separator_B1.Name = "TSMI_Separator_B1";
			this.TSMI_Separator_B1.Size = new System.Drawing.Size(176, 6);
			this.TSMI_Separator_B1.Visible = false;
			// 
			// TSMI_PageDrawImage
			// 
			this.TSMI_PageDrawImage.CheckOnClick = true;
			this.TSMI_PageDrawImage.Name = "TSMI_PageDrawImage";
			this.TSMI_PageDrawImage.Size = new System.Drawing.Size(179, 22);
			this.TSMI_PageDrawImage.Text = "Rysuj obraz strony";
			this.TSMI_PageDrawImage.Visible = false;
			this.TSMI_PageDrawImage.CheckedChanged += new System.EventHandler(this.CB_P2_DrawPageImage_CheckedChanged);
			// 
			// TSMI_Separator_B2
			// 
			this.TSMI_Separator_B2.Name = "TSMI_Separator_B2";
			this.TSMI_Separator_B2.Size = new System.Drawing.Size(176, 6);
			// 
			// TSMI_AddPage
			// 
			this.TSMI_AddPage.Name = "TSMI_AddPage";
			this.TSMI_AddPage.Size = new System.Drawing.Size(179, 22);
			this.TSMI_AddPage.Text = "Dodaj stronę";
			this.TSMI_AddPage.Click += new System.EventHandler(this.TSMI_AddPage_Click);
			// 
			// TSMI_RemovePage
			// 
			this.TSMI_RemovePage.Name = "TSMI_RemovePage";
			this.TSMI_RemovePage.Size = new System.Drawing.Size(179, 22);
			this.TSMI_RemovePage.Text = "Usuń stronę";
			this.TSMI_RemovePage.Click += new System.EventHandler(this.TSMI_RemovePage_Click);
			// 
			// CMS_Label
			// 
			this.CMS_Label.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.TSMI_FieldColor,
			this.TSMI_DrawFieldFill,
			this.TSMI_FieldImage,
			this.TSMI_FieldClear,
			this.TSMI_Separator_C0,
			this.TSMI_DynamicImage,
			this.TSMI_StaticImage,
			this.TSMI_Separator_C1,
			this.TSMI_FontColor,
			this.TSMI_FontName,
			this.TSMI_DynamicText,
			this.TSMI_StaticText,
			this.TSMI_Separator_C2,
			this.TSMI_BorderColor,
			this.TSMI_DrawBorder,
			this.TSMI_Separator_C3,
			this.TSMI_RemoveField});
			this.CMS_Label.Name = "cmPrevLabel";
			this.CMS_Label.Size = new System.Drawing.Size(173, 314);
			this.CMS_Label.Opening += new System.ComponentModel.CancelEventHandler(this.CMS_Label_Opening);
			// 
			// TSMI_FieldColor
			// 
			this.TSMI_FieldColor.Name = "TSMI_FieldColor";
			this.TSMI_FieldColor.Size = new System.Drawing.Size(172, 22);
			this.TSMI_FieldColor.Text = "Kolor tła pola...";
			this.TSMI_FieldColor.Click += new System.EventHandler(this.B_P2_BackColor_Click);
			// 
			// TSMI_DrawFieldFill
			// 
			this.TSMI_DrawFieldFill.CheckOnClick = true;
			this.TSMI_DrawFieldFill.Name = "TSMI_DrawFieldFill";
			this.TSMI_DrawFieldFill.Size = new System.Drawing.Size(172, 22);
			this.TSMI_DrawFieldFill.Text = "Rysuj kolor pola";
			this.TSMI_DrawFieldFill.CheckedChanged += new System.EventHandler(this.CB_P2_DrawColor_CheckedChanged);
			// 
			// TSMI_FieldImage
			// 
			this.TSMI_FieldImage.Name = "TSMI_FieldImage";
			this.TSMI_FieldImage.Size = new System.Drawing.Size(172, 22);
			this.TSMI_FieldImage.Text = "Obraz tła pola...";
			this.TSMI_FieldImage.Click += new System.EventHandler(this.B_P2_BackImage_Click);
			// 
			// TSMI_FieldClear
			// 
			this.TSMI_FieldClear.Name = "TSMI_FieldClear";
			this.TSMI_FieldClear.Size = new System.Drawing.Size(172, 22);
			this.TSMI_FieldClear.Text = "Wyczyść tło";
			this.TSMI_FieldClear.Click += new System.EventHandler(this.TSMI_FieldClear_Click);
			// 
			// TSMI_Separator_C0
			// 
			this.TSMI_Separator_C0.Name = "TSMI_Separator_C0";
			this.TSMI_Separator_C0.Size = new System.Drawing.Size(169, 6);
			this.TSMI_Separator_C0.Visible = false;
			// 
			// TSMI_DynamicImage
			// 
			this.TSMI_DynamicImage.CheckOnClick = true;
			this.TSMI_DynamicImage.Enabled = false;
			this.TSMI_DynamicImage.Name = "TSMI_DynamicImage";
			this.TSMI_DynamicImage.Size = new System.Drawing.Size(172, 22);
			this.TSMI_DynamicImage.Text = "Obraz dynamiczny";
			this.TSMI_DynamicImage.Visible = false;
			// 
			// TSMI_StaticImage
			// 
			this.TSMI_StaticImage.CheckOnClick = true;
			this.TSMI_StaticImage.Enabled = false;
			this.TSMI_StaticImage.Name = "TSMI_StaticImage";
			this.TSMI_StaticImage.Size = new System.Drawing.Size(172, 22);
			this.TSMI_StaticImage.Text = "Obraz statyczny";
			this.TSMI_StaticImage.Visible = false;
			// 
			// TSMI_Separator_C1
			// 
			this.TSMI_Separator_C1.Name = "TSMI_Separator_C1";
			this.TSMI_Separator_C1.Size = new System.Drawing.Size(169, 6);
			// 
			// TSMI_FontColor
			// 
			this.TSMI_FontColor.Name = "TSMI_FontColor";
			this.TSMI_FontColor.Size = new System.Drawing.Size(172, 22);
			this.TSMI_FontColor.Text = "Kolor czcionki...";
			this.TSMI_FontColor.Click += new System.EventHandler(this.B_P2_FontColor_Click);
			// 
			// TSMI_FontName
			// 
			this.TSMI_FontName.Name = "TSMI_FontName";
			this.TSMI_FontName.Size = new System.Drawing.Size(172, 22);
			this.TSMI_FontName.Text = "Zmień czcionkę...";
			this.TSMI_FontName.Click += new System.EventHandler(this.B_P2_FontName_Click);
			// 
			// TSMI_DynamicText
			// 
			this.TSMI_DynamicText.CheckOnClick = true;
			this.TSMI_DynamicText.Name = "TSMI_DynamicText";
			this.TSMI_DynamicText.Size = new System.Drawing.Size(172, 22);
			this.TSMI_DynamicText.Text = "Tekst dynamiczny";
			this.TSMI_DynamicText.CheckedChanged += new System.EventHandler(this.CB_P2_DynamicText_CheckedChanged);
			// 
			// TSMI_StaticText
			// 
			this.TSMI_StaticText.CheckOnClick = true;
			this.TSMI_StaticText.Name = "TSMI_StaticText";
			this.TSMI_StaticText.Size = new System.Drawing.Size(172, 22);
			this.TSMI_StaticText.Text = "Tekst statyczny";
			this.TSMI_StaticText.CheckedChanged += new System.EventHandler(this.CB_P2_StaticText_CheckedChanged);
			// 
			// TSMI_Separator_C2
			// 
			this.TSMI_Separator_C2.Name = "TSMI_Separator_C2";
			this.TSMI_Separator_C2.Size = new System.Drawing.Size(169, 6);
			// 
			// TSMI_BorderColor
			// 
			this.TSMI_BorderColor.Name = "TSMI_BorderColor";
			this.TSMI_BorderColor.Size = new System.Drawing.Size(172, 22);
			this.TSMI_BorderColor.Text = "Kolor ramki...";
			this.TSMI_BorderColor.Click += new System.EventHandler(this.B_P2_BorderColor_Click);
			// 
			// TSMI_DrawBorder
			// 
			this.TSMI_DrawBorder.CheckOnClick = true;
			this.TSMI_DrawBorder.Name = "TSMI_DrawBorder";
			this.TSMI_DrawBorder.Size = new System.Drawing.Size(172, 22);
			this.TSMI_DrawBorder.Text = "Wyświetlaj ramke";
			this.TSMI_DrawBorder.CheckedChanged += new System.EventHandler(this.CB_P2_DrawBorder_CheckedChanged);
			// 
			// TSMI_Separator_C3
			// 
			this.TSMI_Separator_C3.Name = "TSMI_Separator_C3";
			this.TSMI_Separator_C3.Size = new System.Drawing.Size(169, 6);
			// 
			// TSMI_RemoveField
			// 
			this.TSMI_RemoveField.Name = "TSMI_RemoveField";
			this.TSMI_RemoveField.Size = new System.Drawing.Size(172, 22);
			this.TSMI_RemoveField.Text = "Usuń";
			this.TSMI_RemoveField.Click += new System.EventHandler(this.TSMI_RemoveField_Click);
			// 
			// P_P3_Generator
			// 
			this.P_P3_Generator.AutoScroll = true;
			this.P_P3_Generator.BackColor = System.Drawing.SystemColors.ScrollBar;
			this.P_P3_Generator.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.P_P3_Generator.Dock = System.Windows.Forms.DockStyle.Fill;
			this.P_P3_Generator.Location = new System.Drawing.Point(0, 0);
			this.P_P3_Generator.Margin = new System.Windows.Forms.Padding(0);
			this.P_P3_Generator.Name = "P_P3_Generator";
			this.P_P3_Generator.Size = new System.Drawing.Size(453, 438);
			this.P_P3_Generator.TabIndex = 2;
			this.P_P3_Generator.MouseDown += new System.Windows.Forms.MouseEventHandler(this.dpPreview_MouseDown);
			this.P_P3_Generator.Resize += new System.EventHandler(this.P_P3_Generator_Resize);
			// 
			// TLP_P3_Buttons
			// 
			this.TLP_P3_Buttons.ColumnCount = 2;
			this.TLP_P3_Buttons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.TLP_P3_Buttons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.TLP_P3_Buttons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.TLP_P3_Buttons.Controls.Add(this.B_P3_GeneratePDF, 1, 0);
			this.TLP_P3_Buttons.Controls.Add(this.B_P3_SearchErrors, 0, 0);
			this.TLP_P3_Buttons.Dock = System.Windows.Forms.DockStyle.Fill;
			this.TLP_P3_Buttons.Location = new System.Drawing.Point(468, 1);
			this.TLP_P3_Buttons.Margin = new System.Windows.Forms.Padding(1, 0, 5, 0);
			this.TLP_P3_Buttons.Name = "TLP_P3_Buttons";
			this.TLP_P3_Buttons.RowCount = 1;
			this.TLP_P3_Buttons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.TLP_P3_Buttons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 31F));
			this.TLP_P3_Buttons.Size = new System.Drawing.Size(251, 31);
			this.TLP_P3_Buttons.TabIndex = 2;
			// 
			// B_P3_GeneratePDF
			// 
			this.B_P3_GeneratePDF.Dock = System.Windows.Forms.DockStyle.Fill;
			this.B_P3_GeneratePDF.Location = new System.Drawing.Point(127, 3);
			this.B_P3_GeneratePDF.Margin = new System.Windows.Forms.Padding(2, 3, 0, 3);
			this.B_P3_GeneratePDF.Name = "B_P3_GeneratePDF";
			this.B_P3_GeneratePDF.Size = new System.Drawing.Size(124, 25);
			this.B_P3_GeneratePDF.TabIndex = 9;
			this.B_P3_GeneratePDF.Text = "Generuj PDF";
			this.B_P3_GeneratePDF.UseVisualStyleBackColor = true;
			this.B_P3_GeneratePDF.Click += new System.EventHandler(this.B_P3_GeneratePDF_Click);
			// 
			// B_P3_SearchErrors
			// 
			this.B_P3_SearchErrors.Dock = System.Windows.Forms.DockStyle.Fill;
			this.B_P3_SearchErrors.Location = new System.Drawing.Point(0, 3);
			this.B_P3_SearchErrors.Margin = new System.Windows.Forms.Padding(0, 3, 2, 3);
			this.B_P3_SearchErrors.Name = "B_P3_SearchErrors";
			this.B_P3_SearchErrors.Size = new System.Drawing.Size(123, 25);
			this.B_P3_SearchErrors.TabIndex = 8;
			this.B_P3_SearchErrors.Text = "Szukaj błędów";
			this.B_P3_SearchErrors.UseVisualStyleBackColor = true;
			this.B_P3_SearchErrors.Click += new System.EventHandler(this.B_P3_SearchErrors_Click);
			// 
			// TV_P3_PageList
			// 
			this.TV_P3_PageList.Dock = System.Windows.Forms.DockStyle.Fill;
			this.TV_P3_PageList.FullRowSelect = true;
			this.TV_P3_PageList.HideSelection = false;
			this.TV_P3_PageList.Location = new System.Drawing.Point(0, 0);
			this.TV_P3_PageList.Margin = new System.Windows.Forms.Padding(0);
			this.TV_P3_PageList.Name = "TV_P3_PageList";
			this.TV_P3_PageList.ShowLines = false;
			this.TV_P3_PageList.ShowPlusMinus = false;
			this.TV_P3_PageList.ShowRootLines = false;
			this.TV_P3_PageList.Size = new System.Drawing.Size(256, 416);
			this.TV_P3_PageList.TabIndex = 1;
			this.TV_P3_PageList.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.TV_P3_PageList_AfterSelect);
			// 
			// B_P2_Save
			// 
			this.B_P2_Save.Dock = System.Windows.Forms.DockStyle.Fill;
			this.B_P2_Save.Location = new System.Drawing.Point(127, 3);
			this.B_P2_Save.Margin = new System.Windows.Forms.Padding(2, 3, 0, 3);
			this.B_P2_Save.Name = "B_P2_Save";
			this.B_P2_Save.Size = new System.Drawing.Size(124, 25);
			this.B_P2_Save.TabIndex = 5;
			this.B_P2_Save.Text = "Zapisz";
			this.B_P2_Save.UseVisualStyleBackColor = true;
			this.B_P2_Save.Click += new System.EventHandler(this.B_P2_Save_Click);
			// 
			// B_P2_LoadData
			// 
			this.B_P2_LoadData.Dock = System.Windows.Forms.DockStyle.Fill;
			this.B_P2_LoadData.Location = new System.Drawing.Point(0, 3);
			this.B_P2_LoadData.Margin = new System.Windows.Forms.Padding(0, 3, 2, 3);
			this.B_P2_LoadData.Name = "B_P2_LoadData";
			this.B_P2_LoadData.Size = new System.Drawing.Size(123, 25);
			this.B_P2_LoadData.TabIndex = 4;
			this.B_P2_LoadData.Text = "Wczytaj dane";
			this.B_P2_LoadData.UseVisualStyleBackColor = true;
			this.B_P2_LoadData.Click += new System.EventHandler(this.B_P2_LoadData_Click);
			// 
			// TLP_P2_InfoControls
			// 
			this.TLP_P2_InfoControls.BackColor = System.Drawing.Color.Transparent;
			this.TLP_P2_InfoControls.ColumnCount = 3;
			this.TLP_P2_InfoControls.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
			this.TLP_P2_InfoControls.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.TLP_P2_InfoControls.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
			this.TLP_P2_InfoControls.Controls.Add(this.FLP_P2_Page, 2, 0);
			this.TLP_P2_InfoControls.Controls.Add(this.CBX_P2_Scale, 0, 0);
			this.TLP_P2_InfoControls.Controls.Add(this.CB_P2_AutoSave, 1, 0);
			this.TLP_P2_InfoControls.Dock = System.Windows.Forms.DockStyle.Fill;
			this.TLP_P2_InfoControls.Location = new System.Drawing.Point(0, 1);
			this.TLP_P2_InfoControls.Margin = new System.Windows.Forms.Padding(0, 0, 3, 0);
			this.TLP_P2_InfoControls.Name = "TLP_P2_InfoControls";
			this.TLP_P2_InfoControls.RowCount = 1;
			this.TLP_P2_InfoControls.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.TLP_P2_InfoControls.Size = new System.Drawing.Size(464, 31);
			this.TLP_P2_InfoControls.TabIndex = 0;
			// 
			// FLP_P2_Page
			// 
			this.FLP_P2_Page.Controls.Add(this.N_P2_Page);
			this.FLP_P2_Page.Controls.Add(this.L_P2_Page);
			this.FLP_P2_Page.Dock = System.Windows.Forms.DockStyle.Fill;
			this.FLP_P2_Page.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
			this.FLP_P2_Page.Location = new System.Drawing.Point(364, 0);
			this.FLP_P2_Page.Margin = new System.Windows.Forms.Padding(0);
			this.FLP_P2_Page.Name = "FLP_P2_Page";
			this.FLP_P2_Page.Size = new System.Drawing.Size(100, 31);
			this.FLP_P2_Page.TabIndex = 12;
			// 
			// N_P2_Page
			// 
			this.N_P2_Page.Location = new System.Drawing.Point(62, 5);
			this.N_P2_Page.Margin = new System.Windows.Forms.Padding(3, 5, 3, 3);
			this.N_P2_Page.Maximum = new decimal(new int[] {
			1,
			0,
			0,
			0});
			this.N_P2_Page.Minimum = new decimal(new int[] {
			1,
			0,
			0,
			0});
			this.N_P2_Page.Name = "N_P2_Page";
			this.N_P2_Page.Size = new System.Drawing.Size(35, 20);
			this.N_P2_Page.TabIndex = 0;
			this.N_P2_Page.Value = new decimal(new int[] {
			1,
			0,
			0,
			0});
			this.N_P2_Page.ValueChanged += new System.EventHandler(this.N_P2_Page_ValueChanged);
			// 
			// L_P2_Page
			// 
			this.L_P2_Page.AutoSize = true;
			this.L_P2_Page.Location = new System.Drawing.Point(18, 8);
			this.L_P2_Page.Margin = new System.Windows.Forms.Padding(3, 8, 0, 0);
			this.L_P2_Page.Name = "L_P2_Page";
			this.L_P2_Page.Size = new System.Drawing.Size(41, 13);
			this.L_P2_Page.TabIndex = 1;
			this.L_P2_Page.Text = "Strona:";
			// 
			// CBX_P2_Scale
			// 
			this.CBX_P2_Scale.Dock = System.Windows.Forms.DockStyle.Fill;
			this.CBX_P2_Scale.FormattingEnabled = true;
			this.CBX_P2_Scale.Items.AddRange(new object[] {
			"50",
			"75",
			"100",
			"150",
			"200",
			"300"});
			this.CBX_P2_Scale.Location = new System.Drawing.Point(5, 4);
			this.CBX_P2_Scale.Margin = new System.Windows.Forms.Padding(5, 4, 3, 3);
			this.CBX_P2_Scale.Name = "CBX_P2_Scale";
			this.CBX_P2_Scale.Size = new System.Drawing.Size(72, 21);
			this.CBX_P2_Scale.TabIndex = 10;
			this.CBX_P2_Scale.SelectedIndexChanged += new System.EventHandler(this.CBX_P2_Scale_SelectedIndexChanged);
			this.CBX_P2_Scale.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CBX_P2_Scale_KeyDown);
			this.CBX_P2_Scale.Leave += new System.EventHandler(this.CBX_P2_Scale_Leave);
			// 
			// CB_P2_AutoSave
			// 
			this.CB_P2_AutoSave.AutoSize = true;
			this.CB_P2_AutoSave.Dock = System.Windows.Forms.DockStyle.Fill;
			this.CB_P2_AutoSave.Location = new System.Drawing.Point(83, 3);
			this.CB_P2_AutoSave.Name = "CB_P2_AutoSave";
			this.CB_P2_AutoSave.Size = new System.Drawing.Size(278, 25);
			this.CB_P2_AutoSave.TabIndex = 13;
			this.CB_P2_AutoSave.Text = "Automatyczny zapis bez tworzenia podglądu";
			this.CB_P2_AutoSave.UseVisualStyleBackColor = true;
			this.CB_P2_AutoSave.Visible = false;
			// 
			// P_P2_Pattern
			// 
			this.P_P2_Pattern.AutoScroll = true;
			this.P_P2_Pattern.BackColor = System.Drawing.SystemColors.ScrollBar;
			this.P_P2_Pattern.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.P_P2_Pattern.Dock = System.Windows.Forms.DockStyle.Fill;
			this.P_P2_Pattern.Location = new System.Drawing.Point(0, 0);
			this.P_P2_Pattern.Margin = new System.Windows.Forms.Padding(0);
			this.P_P2_Pattern.Name = "P_P2_Pattern";
			this.P_P2_Pattern.Size = new System.Drawing.Size(450, 438);
			this.P_P2_Pattern.TabIndex = 1;
			this.P_P2_Pattern.MouseDown += new System.Windows.Forms.MouseEventHandler(this.P_P2_Pattern_MouseDown);
			this.P_P2_Pattern.Resize += new System.EventHandler(this.P_P2_Pattern_Resize);
			// 
			// TLP_P2_Details
			// 
			this.TLP_P2_Details.ColumnCount = 1;
			this.TLP_P2_Details.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.TLP_P2_Details.Controls.Add(this.P_P2_Menu, 0, 0);
			this.TLP_P2_Details.Controls.Add(this.P_P2_Editor, 0, 1);
			this.TLP_P2_Details.Dock = System.Windows.Forms.DockStyle.Fill;
			this.TLP_P2_Details.Location = new System.Drawing.Point(0, 0);
			this.TLP_P2_Details.Margin = new System.Windows.Forms.Padding(3, 6, 6, 3);
			this.TLP_P2_Details.Name = "TLP_P2_Details";
			this.TLP_P2_Details.RowCount = 2;
			this.TLP_P2_Details.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
			this.TLP_P2_Details.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.TLP_P2_Details.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.TLP_P2_Details.Size = new System.Drawing.Size(259, 438);
			this.TLP_P2_Details.TabIndex = 5;
			// 
			// P_P2_Menu
			// 
			this.P_P2_Menu.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.P_P2_Menu.Controls.Add(this.MS_P2_Menu);
			this.P_P2_Menu.Dock = System.Windows.Forms.DockStyle.Fill;
			this.P_P2_Menu.Location = new System.Drawing.Point(3, 3);
			this.P_P2_Menu.Name = "P_P2_Menu";
			this.P_P2_Menu.Size = new System.Drawing.Size(253, 29);
			this.P_P2_Menu.TabIndex = 75;
			// 
			// MS_P2_Menu
			// 
			this.MS_P2_Menu.BackColor = System.Drawing.SystemColors.ControlLight;
			this.MS_P2_Menu.Dock = System.Windows.Forms.DockStyle.Fill;
			this.MS_P2_Menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.TSMI_FieldSwitch,
			this.TSMI_DetailSwitch,
			this.TSMI_PageSwitch});
			this.MS_P2_Menu.Location = new System.Drawing.Point(0, 0);
			this.MS_P2_Menu.Name = "MS_P2_Menu";
			this.MS_P2_Menu.Size = new System.Drawing.Size(251, 27);
			this.MS_P2_Menu.TabIndex = 68;
			this.MS_P2_Menu.Text = "icPatMenu";
			// 
			// TSMI_FieldSwitch
			// 
			this.TSMI_FieldSwitch.Enabled = false;
			this.TSMI_FieldSwitch.Name = "TSMI_FieldSwitch";
			this.TSMI_FieldSwitch.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D1)));
			this.TSMI_FieldSwitch.Size = new System.Drawing.Size(42, 23);
			this.TSMI_FieldSwitch.Text = "Pole";
			this.TSMI_FieldSwitch.Click += new System.EventHandler(this.TSMI_FieldSwitch_Click);
			// 
			// TSMI_DetailSwitch
			// 
			this.TSMI_DetailSwitch.Name = "TSMI_DetailSwitch";
			this.TSMI_DetailSwitch.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D2)));
			this.TSMI_DetailSwitch.Size = new System.Drawing.Size(70, 23);
			this.TSMI_DetailSwitch.Text = "Szczegóły";
			this.TSMI_DetailSwitch.Click += new System.EventHandler(this.TSMI_DetailSwitch_Click);
			// 
			// TSMI_PageSwitch
			// 
			this.TSMI_PageSwitch.Margin = new System.Windows.Forms.Padding(0, 0, 3, 0);
			this.TSMI_PageSwitch.Name = "TSMI_PageSwitch";
			this.TSMI_PageSwitch.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D3)));
			this.TSMI_PageSwitch.Size = new System.Drawing.Size(53, 23);
			this.TSMI_PageSwitch.Text = "Strona";
			this.TSMI_PageSwitch.Click += new System.EventHandler(this.TSMI_PageSwitch_Click);
			// 
			// P_P2_Editor
			// 
			this.P_P2_Editor.Controls.Add(this.TLP_P2_LabelDetails);
			this.P_P2_Editor.Controls.Add(this.TLP_P2_PageDetails);
			this.P_P2_Editor.Controls.Add(this.TLP_P2_Field);
			this.P_P2_Editor.Dock = System.Windows.Forms.DockStyle.Fill;
			this.P_P2_Editor.Location = new System.Drawing.Point(0, 35);
			this.P_P2_Editor.Margin = new System.Windows.Forms.Padding(0);
			this.P_P2_Editor.Name = "P_P2_Editor";
			this.P_P2_Editor.Size = new System.Drawing.Size(259, 403);
			this.P_P2_Editor.TabIndex = 69;
			// 
			// TLP_P2_LabelDetails
			// 
			this.TLP_P2_LabelDetails.ColumnCount = 2;
			this.TLP_P2_LabelDetails.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.TLP_P2_LabelDetails.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.TLP_P2_LabelDetails.Controls.Add(this.CB_P2_AdditionalMargin, 0, 11);
			this.TLP_P2_LabelDetails.Controls.Add(this.L_P2_AddMargin, 0, 10);
			this.TLP_P2_LabelDetails.Controls.Add(this.CBX_P2_StickPoint, 0, 5);
			this.TLP_P2_LabelDetails.Controls.Add(this.L_P2_StickPoint, 0, 4);
			this.TLP_P2_LabelDetails.Controls.Add(this.CB_P2_UseImageMargin, 0, 7);
			this.TLP_P2_LabelDetails.Controls.Add(this.CB_P2_DrawFrameOutside, 0, 6);
			this.TLP_P2_LabelDetails.Controls.Add(this.L_P2_ImageSettings, 0, 6);
			this.TLP_P2_LabelDetails.Controls.Add(this.CB_P2_StaticImage, 0, 14);
			this.TLP_P2_LabelDetails.Controls.Add(this.CB_P2_DynamicImage, 1, 10);
			this.TLP_P2_LabelDetails.Controls.Add(this.CBX_P2_ImageSettings, 0, 7);
			this.TLP_P2_LabelDetails.Controls.Add(this.N_P2_MarginLR, 0, 12);
			this.TLP_P2_LabelDetails.Controls.Add(this.N_P2_MarginTB, 1, 12);
			this.TLP_P2_LabelDetails.Controls.Add(this.G_P2_Generator, 0, 0);
			this.TLP_P2_LabelDetails.Dock = System.Windows.Forms.DockStyle.Fill;
			this.TLP_P2_LabelDetails.Location = new System.Drawing.Point(0, 0);
			this.TLP_P2_LabelDetails.Margin = new System.Windows.Forms.Padding(0);
			this.TLP_P2_LabelDetails.Name = "TLP_P2_LabelDetails";
			this.TLP_P2_LabelDetails.RowCount = 15;
			this.TLP_P2_LabelDetails.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22F));
			this.TLP_P2_LabelDetails.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
			this.TLP_P2_LabelDetails.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
			this.TLP_P2_LabelDetails.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
			this.TLP_P2_LabelDetails.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.TLP_P2_LabelDetails.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
			this.TLP_P2_LabelDetails.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
			this.TLP_P2_LabelDetails.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
			this.TLP_P2_LabelDetails.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.TLP_P2_LabelDetails.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
			this.TLP_P2_LabelDetails.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.TLP_P2_LabelDetails.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.TLP_P2_LabelDetails.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.TLP_P2_LabelDetails.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.TLP_P2_LabelDetails.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.TLP_P2_LabelDetails.Size = new System.Drawing.Size(259, 403);
			this.TLP_P2_LabelDetails.TabIndex = 5;
			this.TLP_P2_LabelDetails.Visible = false;
			// 
			// CB_P2_AdditionalMargin
			// 
			this.CB_P2_AdditionalMargin.AutoSize = true;
			this.TLP_P2_LabelDetails.SetColumnSpan(this.CB_P2_AdditionalMargin, 2);
			this.CB_P2_AdditionalMargin.Dock = System.Windows.Forms.DockStyle.Fill;
			this.CB_P2_AdditionalMargin.Location = new System.Drawing.Point(3, 307);
			this.CB_P2_AdditionalMargin.Margin = new System.Windows.Forms.Padding(3, 5, 3, 3);
			this.CB_P2_AdditionalMargin.Name = "CB_P2_AdditionalMargin";
			this.CB_P2_AdditionalMargin.Size = new System.Drawing.Size(253, 12);
			this.CB_P2_AdditionalMargin.TabIndex = 76;
			this.CB_P2_AdditionalMargin.Text = "Zastosuj dodatkowy margines tekstu";
			this.CB_P2_AdditionalMargin.UseVisualStyleBackColor = true;
			this.CB_P2_AdditionalMargin.Visible = false;
			// 
			// L_P2_AddMargin
			// 
			this.L_P2_AddMargin.AutoSize = true;
			this.TLP_P2_LabelDetails.SetColumnSpan(this.L_P2_AddMargin, 2);
			this.L_P2_AddMargin.Dock = System.Windows.Forms.DockStyle.Fill;
			this.L_P2_AddMargin.Location = new System.Drawing.Point(3, 262);
			this.L_P2_AddMargin.Name = "L_P2_AddMargin";
			this.L_P2_AddMargin.Size = new System.Drawing.Size(253, 20);
			this.L_P2_AddMargin.TabIndex = 71;
			this.L_P2_AddMargin.Text = "Dodatkowy margines tekstu:";
			this.L_P2_AddMargin.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			this.L_P2_AddMargin.Visible = false;
			// 
			// CBX_P2_StickPoint
			// 
			this.TLP_P2_LabelDetails.SetColumnSpan(this.CBX_P2_StickPoint, 2);
			this.CBX_P2_StickPoint.Dock = System.Windows.Forms.DockStyle.Fill;
			this.CBX_P2_StickPoint.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.CBX_P2_StickPoint.Enabled = false;
			this.CBX_P2_StickPoint.FormattingEnabled = true;
			this.CBX_P2_StickPoint.Items.AddRange(new object[] {
			"Góra-Lewo (lewy górny róg)",
			"Góra-Prawo (prawy górny róg)",
			"Dół-Lewo (lewy dolny róg)",
			"Dół-Prawo (prawy dolny róg)"});
			this.CBX_P2_StickPoint.Location = new System.Drawing.Point(3, 145);
			this.CBX_P2_StickPoint.Name = "CBX_P2_StickPoint";
			this.CBX_P2_StickPoint.Size = new System.Drawing.Size(253, 21);
			this.CBX_P2_StickPoint.TabIndex = 69;
			this.CBX_P2_StickPoint.SelectedIndexChanged += new System.EventHandler(this.CBX_P2_StickPoint_SelectedIndexChanged);
			// 
			// L_P2_StickPoint
			// 
			this.L_P2_StickPoint.AutoSize = true;
			this.TLP_P2_LabelDetails.SetColumnSpan(this.L_P2_StickPoint, 2);
			this.L_P2_StickPoint.Dock = System.Windows.Forms.DockStyle.Fill;
			this.L_P2_StickPoint.Location = new System.Drawing.Point(3, 122);
			this.L_P2_StickPoint.Name = "L_P2_StickPoint";
			this.L_P2_StickPoint.Size = new System.Drawing.Size(253, 20);
			this.L_P2_StickPoint.TabIndex = 70;
			this.L_P2_StickPoint.Text = "Punkt zaczepienia pola:";
			this.L_P2_StickPoint.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// CB_P2_UseImageMargin
			// 
			this.CB_P2_UseImageMargin.AutoSize = true;
			this.TLP_P2_LabelDetails.SetColumnSpan(this.CB_P2_UseImageMargin, 2);
			this.CB_P2_UseImageMargin.Dock = System.Windows.Forms.DockStyle.Fill;
			this.CB_P2_UseImageMargin.Location = new System.Drawing.Point(3, 242);
			this.CB_P2_UseImageMargin.Margin = new System.Windows.Forms.Padding(3, 5, 3, 3);
			this.CB_P2_UseImageMargin.Name = "CB_P2_UseImageMargin";
			this.CB_P2_UseImageMargin.Size = new System.Drawing.Size(253, 17);
			this.CB_P2_UseImageMargin.TabIndex = 12;
			this.CB_P2_UseImageMargin.Text = "Zastosuj margines do obrazu";
			this.CB_P2_UseImageMargin.UseVisualStyleBackColor = true;
			this.CB_P2_UseImageMargin.Visible = false;
			// 
			// CB_P2_DrawFrameOutside
			// 
			this.CB_P2_DrawFrameOutside.AutoSize = true;
			this.TLP_P2_LabelDetails.SetColumnSpan(this.CB_P2_DrawFrameOutside, 2);
			this.CB_P2_DrawFrameOutside.Dock = System.Windows.Forms.DockStyle.Fill;
			this.CB_P2_DrawFrameOutside.Location = new System.Drawing.Point(3, 172);
			this.CB_P2_DrawFrameOutside.Margin = new System.Windows.Forms.Padding(3, 5, 3, 3);
			this.CB_P2_DrawFrameOutside.Name = "CB_P2_DrawFrameOutside";
			this.CB_P2_DrawFrameOutside.Size = new System.Drawing.Size(253, 17);
			this.CB_P2_DrawFrameOutside.TabIndex = 11;
			this.CB_P2_DrawFrameOutside.Text = "Rysuj ramkę na zewnątrz obrazu";
			this.CB_P2_DrawFrameOutside.UseVisualStyleBackColor = true;
			this.CB_P2_DrawFrameOutside.Visible = false;
			// 
			// L_P2_ImageSettings
			// 
			this.L_P2_ImageSettings.AutoSize = true;
			this.TLP_P2_LabelDetails.SetColumnSpan(this.L_P2_ImageSettings, 2);
			this.L_P2_ImageSettings.Dock = System.Windows.Forms.DockStyle.Fill;
			this.L_P2_ImageSettings.Location = new System.Drawing.Point(3, 192);
			this.L_P2_ImageSettings.Name = "L_P2_ImageSettings";
			this.L_P2_ImageSettings.Size = new System.Drawing.Size(253, 25);
			this.L_P2_ImageSettings.TabIndex = 9;
			this.L_P2_ImageSettings.Text = "Ustawienia obrazu:";
			this.L_P2_ImageSettings.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			this.L_P2_ImageSettings.Visible = false;
			// 
			// CB_P2_StaticImage
			// 
			this.CB_P2_StaticImage.AutoSize = true;
			this.CB_P2_StaticImage.Dock = System.Windows.Forms.DockStyle.Fill;
			this.CB_P2_StaticImage.Location = new System.Drawing.Point(3, 346);
			this.CB_P2_StaticImage.Margin = new System.Windows.Forms.Padding(3, 4, 3, 3);
			this.CB_P2_StaticImage.Name = "CB_P2_StaticImage";
			this.CB_P2_StaticImage.Size = new System.Drawing.Size(123, 54);
			this.CB_P2_StaticImage.TabIndex = 4;
			this.CB_P2_StaticImage.Text = "Obraz statyczny";
			this.CB_P2_StaticImage.UseVisualStyleBackColor = true;
			this.CB_P2_StaticImage.Visible = false;
			this.CB_P2_StaticImage.CheckedChanged += new System.EventHandler(this.CB_P2_StaticImage_CheckedChanged);
			// 
			// CB_P2_DynamicImage
			// 
			this.CB_P2_DynamicImage.AutoSize = true;
			this.CB_P2_DynamicImage.Dock = System.Windows.Forms.DockStyle.Fill;
			this.CB_P2_DynamicImage.Location = new System.Drawing.Point(3, 287);
			this.CB_P2_DynamicImage.Margin = new System.Windows.Forms.Padding(3, 5, 3, 3);
			this.CB_P2_DynamicImage.Name = "CB_P2_DynamicImage";
			this.CB_P2_DynamicImage.Size = new System.Drawing.Size(123, 12);
			this.CB_P2_DynamicImage.TabIndex = 8;
			this.CB_P2_DynamicImage.Text = "Obraz dynamiczny";
			this.CB_P2_DynamicImage.UseVisualStyleBackColor = true;
			this.CB_P2_DynamicImage.Visible = false;
			// 
			// CBX_P2_ImageSettings
			// 
			this.TLP_P2_LabelDetails.SetColumnSpan(this.CBX_P2_ImageSettings, 2);
			this.CBX_P2_ImageSettings.Dock = System.Windows.Forms.DockStyle.Fill;
			this.CBX_P2_ImageSettings.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.CBX_P2_ImageSettings.FormattingEnabled = true;
			this.CBX_P2_ImageSettings.Items.AddRange(new object[] {
			"Zostaw proporcje takie jakie są"});
			this.CBX_P2_ImageSettings.Location = new System.Drawing.Point(3, 220);
			this.CBX_P2_ImageSettings.Name = "CBX_P2_ImageSettings";
			this.CBX_P2_ImageSettings.Size = new System.Drawing.Size(253, 21);
			this.CBX_P2_ImageSettings.TabIndex = 10;
			this.CBX_P2_ImageSettings.Visible = false;
			// 
			// N_P2_MarginLR
			// 
			this.N_P2_MarginLR.DecimalPlaces = 1;
			this.N_P2_MarginLR.Dock = System.Windows.Forms.DockStyle.Fill;
			this.N_P2_MarginLR.Location = new System.Drawing.Point(3, 325);
			this.N_P2_MarginLR.Name = "N_P2_MarginLR";
			this.N_P2_MarginLR.Size = new System.Drawing.Size(123, 20);
			this.N_P2_MarginLR.TabIndex = 74;
			this.N_P2_MarginLR.Visible = false;
			// 
			// N_P2_MarginTB
			// 
			this.N_P2_MarginTB.DecimalPlaces = 1;
			this.N_P2_MarginTB.Dock = System.Windows.Forms.DockStyle.Fill;
			this.N_P2_MarginTB.Location = new System.Drawing.Point(132, 325);
			this.N_P2_MarginTB.Name = "N_P2_MarginTB";
			this.N_P2_MarginTB.Size = new System.Drawing.Size(124, 20);
			this.N_P2_MarginTB.TabIndex = 73;
			this.N_P2_MarginTB.Visible = false;
			// 
			// G_P2_Generator
			// 
			this.TLP_P2_LabelDetails.SetColumnSpan(this.G_P2_Generator, 2);
			this.G_P2_Generator.Controls.Add(this.TLP_P2_Generator);
			this.G_P2_Generator.Dock = System.Windows.Forms.DockStyle.Fill;
			this.G_P2_Generator.Location = new System.Drawing.Point(3, 5);
			this.G_P2_Generator.Margin = new System.Windows.Forms.Padding(3, 5, 3, 3);
			this.G_P2_Generator.Name = "G_P2_Generator";
			this.G_P2_Generator.Padding = new System.Windows.Forms.Padding(6, 2, 6, 3);
			this.TLP_P2_LabelDetails.SetRowSpan(this.G_P2_Generator, 4);
			this.G_P2_Generator.Size = new System.Drawing.Size(253, 114);
			this.G_P2_Generator.TabIndex = 77;
			this.G_P2_Generator.TabStop = false;
			this.G_P2_Generator.Text = " Generowanie do pliku PDF ";
			// 
			// TLP_P2_Generator
			// 
			this.TLP_P2_Generator.ColumnCount = 1;
			this.TLP_P2_Generator.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.TLP_P2_Generator.Controls.Add(this.CB_P2_DrawBorder, 0, 0);
			this.TLP_P2_Generator.Controls.Add(this.CB_P2_DrawColor, 0, 1);
			this.TLP_P2_Generator.Controls.Add(this.CB_P2_DynamicText, 0, 3);
			this.TLP_P2_Generator.Controls.Add(this.CB_P2_StaticText, 0, 2);
			this.TLP_P2_Generator.Dock = System.Windows.Forms.DockStyle.Fill;
			this.TLP_P2_Generator.Location = new System.Drawing.Point(6, 15);
			this.TLP_P2_Generator.Name = "TLP_P2_Generator";
			this.TLP_P2_Generator.RowCount = 4;
			this.TLP_P2_Generator.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
			this.TLP_P2_Generator.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
			this.TLP_P2_Generator.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
			this.TLP_P2_Generator.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
			this.TLP_P2_Generator.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.TLP_P2_Generator.Size = new System.Drawing.Size(241, 96);
			this.TLP_P2_Generator.TabIndex = 0;
			// 
			// CB_P2_DrawBorder
			// 
			this.CB_P2_DrawBorder.AutoSize = true;
			this.CB_P2_DrawBorder.Dock = System.Windows.Forms.DockStyle.Fill;
			this.CB_P2_DrawBorder.Location = new System.Drawing.Point(3, 4);
			this.CB_P2_DrawBorder.Margin = new System.Windows.Forms.Padding(3, 4, 3, 3);
			this.CB_P2_DrawBorder.Name = "CB_P2_DrawBorder";
			this.CB_P2_DrawBorder.Size = new System.Drawing.Size(235, 17);
			this.CB_P2_DrawBorder.TabIndex = 1;
			this.CB_P2_DrawBorder.Text = "Wyświetlaj ramke";
			this.CB_P2_DrawBorder.UseVisualStyleBackColor = true;
			this.CB_P2_DrawBorder.CheckedChanged += new System.EventHandler(this.CB_P2_DrawBorder_CheckedChanged);
			// 
			// CB_P2_DrawColor
			// 
			this.CB_P2_DrawColor.AutoSize = true;
			this.CB_P2_DrawColor.Dock = System.Windows.Forms.DockStyle.Fill;
			this.CB_P2_DrawColor.Location = new System.Drawing.Point(3, 28);
			this.CB_P2_DrawColor.Margin = new System.Windows.Forms.Padding(3, 4, 3, 3);
			this.CB_P2_DrawColor.Name = "CB_P2_DrawColor";
			this.CB_P2_DrawColor.Size = new System.Drawing.Size(235, 17);
			this.CB_P2_DrawColor.TabIndex = 6;
			this.CB_P2_DrawColor.Text = "Rysuj kolor pola";
			this.CB_P2_DrawColor.UseVisualStyleBackColor = true;
			this.CB_P2_DrawColor.CheckedChanged += new System.EventHandler(this.CB_P2_DrawColor_CheckedChanged);
			// 
			// CB_P2_DynamicText
			// 
			this.CB_P2_DynamicText.AutoSize = true;
			this.CB_P2_DynamicText.Dock = System.Windows.Forms.DockStyle.Fill;
			this.CB_P2_DynamicText.Location = new System.Drawing.Point(3, 76);
			this.CB_P2_DynamicText.Margin = new System.Windows.Forms.Padding(3, 4, 3, 3);
			this.CB_P2_DynamicText.Name = "CB_P2_DynamicText";
			this.CB_P2_DynamicText.Size = new System.Drawing.Size(235, 17);
			this.CB_P2_DynamicText.TabIndex = 3;
			this.CB_P2_DynamicText.Text = "Tekst dynamiczny";
			this.CB_P2_DynamicText.UseVisualStyleBackColor = true;
			this.CB_P2_DynamicText.CheckedChanged += new System.EventHandler(this.CB_P2_DynamicText_CheckedChanged);
			// 
			// CB_P2_StaticText
			// 
			this.CB_P2_StaticText.AutoSize = true;
			this.CB_P2_StaticText.Dock = System.Windows.Forms.DockStyle.Fill;
			this.CB_P2_StaticText.Location = new System.Drawing.Point(3, 52);
			this.CB_P2_StaticText.Margin = new System.Windows.Forms.Padding(3, 4, 3, 3);
			this.CB_P2_StaticText.Name = "CB_P2_StaticText";
			this.CB_P2_StaticText.Size = new System.Drawing.Size(235, 17);
			this.CB_P2_StaticText.TabIndex = 7;
			this.CB_P2_StaticText.Text = "Tekst statyczny";
			this.CB_P2_StaticText.UseVisualStyleBackColor = true;
			this.CB_P2_StaticText.CheckedChanged += new System.EventHandler(this.CB_P2_StaticText_CheckedChanged);
			// 
			// TLP_P2_PageDetails
			// 
			this.TLP_P2_PageDetails.ColumnCount = 2;
			this.TLP_P2_PageDetails.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.TLP_P2_PageDetails.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.TLP_P2_PageDetails.Controls.Add(this.CB_P2_DrawOutside, 0, 10);
			this.TLP_P2_PageDetails.Controls.Add(this.CB_P2_ApplyMargin, 0, 9);
			this.TLP_P2_PageDetails.Controls.Add(this.CBX_P2_PageImageSettings, 0, 8);
			this.TLP_P2_PageDetails.Controls.Add(this.L_P2_PageImageSet, 0, 7);
			this.TLP_P2_PageDetails.Controls.Add(this.TB_P2_PageWidth, 0, 1);
			this.TLP_P2_PageDetails.Controls.Add(this.TB_P2_PageHeight, 1, 1);
			this.TLP_P2_PageDetails.Controls.Add(this.L_P2_PageWidth, 0, 0);
			this.TLP_P2_PageDetails.Controls.Add(this.L_P2_PageHeight, 1, 0);
			this.TLP_P2_PageDetails.Controls.Add(this.CB_P2_DrawPageImage, 0, 11);
			this.TLP_P2_PageDetails.Controls.Add(this.G_P2_PageApperance, 0, 2);
			this.TLP_P2_PageDetails.Controls.Add(this.G_P2_GeneratePDF, 0, 5);
			this.TLP_P2_PageDetails.Dock = System.Windows.Forms.DockStyle.Fill;
			this.TLP_P2_PageDetails.Location = new System.Drawing.Point(0, 0);
			this.TLP_P2_PageDetails.Name = "TLP_P2_PageDetails";
			this.TLP_P2_PageDetails.RowCount = 12;
			this.TLP_P2_PageDetails.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.TLP_P2_PageDetails.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
			this.TLP_P2_PageDetails.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22F));
			this.TLP_P2_PageDetails.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 29F));
			this.TLP_P2_PageDetails.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 29F));
			this.TLP_P2_PageDetails.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
			this.TLP_P2_PageDetails.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
			this.TLP_P2_PageDetails.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.TLP_P2_PageDetails.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
			this.TLP_P2_PageDetails.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
			this.TLP_P2_PageDetails.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
			this.TLP_P2_PageDetails.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.TLP_P2_PageDetails.Size = new System.Drawing.Size(259, 403);
			this.TLP_P2_PageDetails.TabIndex = 4;
			this.TLP_P2_PageDetails.Visible = false;
			// 
			// CB_P2_DrawOutside
			// 
			this.CB_P2_DrawOutside.AutoSize = true;
			this.TLP_P2_PageDetails.SetColumnSpan(this.CB_P2_DrawOutside, 2);
			this.CB_P2_DrawOutside.Dock = System.Windows.Forms.DockStyle.Fill;
			this.CB_P2_DrawOutside.Location = new System.Drawing.Point(3, 250);
			this.CB_P2_DrawOutside.Margin = new System.Windows.Forms.Padding(3, 5, 3, 3);
			this.CB_P2_DrawOutside.Name = "CB_P2_DrawOutside";
			this.CB_P2_DrawOutside.Size = new System.Drawing.Size(253, 17);
			this.CB_P2_DrawOutside.TabIndex = 14;
			this.CB_P2_DrawOutside.Text = "Rysuj ramkę na zewnątrz obrazu";
			this.CB_P2_DrawOutside.UseVisualStyleBackColor = true;
			this.CB_P2_DrawOutside.Visible = false;
			// 
			// CB_P2_ApplyMargin
			// 
			this.CB_P2_ApplyMargin.AutoSize = true;
			this.TLP_P2_PageDetails.SetColumnSpan(this.CB_P2_ApplyMargin, 2);
			this.CB_P2_ApplyMargin.Dock = System.Windows.Forms.DockStyle.Fill;
			this.CB_P2_ApplyMargin.Location = new System.Drawing.Point(3, 225);
			this.CB_P2_ApplyMargin.Margin = new System.Windows.Forms.Padding(3, 5, 3, 3);
			this.CB_P2_ApplyMargin.Name = "CB_P2_ApplyMargin";
			this.CB_P2_ApplyMargin.Size = new System.Drawing.Size(253, 17);
			this.CB_P2_ApplyMargin.TabIndex = 13;
			this.CB_P2_ApplyMargin.Text = "Zastosuj margines do obrazu";
			this.CB_P2_ApplyMargin.UseVisualStyleBackColor = true;
			this.CB_P2_ApplyMargin.Visible = false;
			// 
			// CBX_P2_PageImageSettings
			// 
			this.TLP_P2_PageDetails.SetColumnSpan(this.CBX_P2_PageImageSettings, 2);
			this.CBX_P2_PageImageSettings.Dock = System.Windows.Forms.DockStyle.Fill;
			this.CBX_P2_PageImageSettings.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.CBX_P2_PageImageSettings.FormattingEnabled = true;
			this.CBX_P2_PageImageSettings.Items.AddRange(new object[] {
			"Zostaw proporcje takie jakie są"});
			this.CBX_P2_PageImageSettings.Location = new System.Drawing.Point(3, 198);
			this.CBX_P2_PageImageSettings.Name = "CBX_P2_PageImageSettings";
			this.CBX_P2_PageImageSettings.Size = new System.Drawing.Size(253, 21);
			this.CBX_P2_PageImageSettings.TabIndex = 11;
			this.CBX_P2_PageImageSettings.Visible = false;
			// 
			// L_P2_PageImageSet
			// 
			this.L_P2_PageImageSet.AutoSize = true;
			this.TLP_P2_PageDetails.SetColumnSpan(this.L_P2_PageImageSet, 2);
			this.L_P2_PageImageSet.Dock = System.Windows.Forms.DockStyle.Fill;
			this.L_P2_PageImageSet.Location = new System.Drawing.Point(3, 175);
			this.L_P2_PageImageSet.Name = "L_P2_PageImageSet";
			this.L_P2_PageImageSet.Size = new System.Drawing.Size(253, 20);
			this.L_P2_PageImageSet.TabIndex = 10;
			this.L_P2_PageImageSet.Text = "Ustawienia obrazu:";
			this.L_P2_PageImageSet.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			this.L_P2_PageImageSet.Visible = false;
			// 
			// TB_P2_PageWidth
			// 
			this.TB_P2_PageWidth.Dock = System.Windows.Forms.DockStyle.Fill;
			this.TB_P2_PageWidth.Enabled = false;
			this.TB_P2_PageWidth.Location = new System.Drawing.Point(3, 23);
			this.TB_P2_PageWidth.Name = "TB_P2_PageWidth";
			this.TB_P2_PageWidth.Size = new System.Drawing.Size(123, 20);
			this.TB_P2_PageWidth.TabIndex = 0;
			// 
			// TB_P2_PageHeight
			// 
			this.TB_P2_PageHeight.Dock = System.Windows.Forms.DockStyle.Fill;
			this.TB_P2_PageHeight.Enabled = false;
			this.TB_P2_PageHeight.Location = new System.Drawing.Point(132, 23);
			this.TB_P2_PageHeight.Name = "TB_P2_PageHeight";
			this.TB_P2_PageHeight.Size = new System.Drawing.Size(124, 20);
			this.TB_P2_PageHeight.TabIndex = 1;
			// 
			// L_P2_PageWidth
			// 
			this.L_P2_PageWidth.AutoSize = true;
			this.L_P2_PageWidth.Dock = System.Windows.Forms.DockStyle.Fill;
			this.L_P2_PageWidth.Location = new System.Drawing.Point(3, 0);
			this.L_P2_PageWidth.Name = "L_P2_PageWidth";
			this.L_P2_PageWidth.Size = new System.Drawing.Size(123, 20);
			this.L_P2_PageWidth.TabIndex = 2;
			this.L_P2_PageWidth.Text = "Szerokość:";
			this.L_P2_PageWidth.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// L_P2_PageHeight
			// 
			this.L_P2_PageHeight.AutoSize = true;
			this.L_P2_PageHeight.Dock = System.Windows.Forms.DockStyle.Fill;
			this.L_P2_PageHeight.Location = new System.Drawing.Point(132, 0);
			this.L_P2_PageHeight.Name = "L_P2_PageHeight";
			this.L_P2_PageHeight.Size = new System.Drawing.Size(124, 20);
			this.L_P2_PageHeight.TabIndex = 3;
			this.L_P2_PageHeight.Text = "Wysokość:";
			this.L_P2_PageHeight.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// CB_P2_DrawPageImage
			// 
			this.CB_P2_DrawPageImage.AutoSize = true;
			this.CB_P2_DrawPageImage.Dock = System.Windows.Forms.DockStyle.Fill;
			this.CB_P2_DrawPageImage.Location = new System.Drawing.Point(3, 274);
			this.CB_P2_DrawPageImage.Margin = new System.Windows.Forms.Padding(3, 4, 3, 3);
			this.CB_P2_DrawPageImage.Name = "CB_P2_DrawPageImage";
			this.CB_P2_DrawPageImage.Size = new System.Drawing.Size(123, 126);
			this.CB_P2_DrawPageImage.TabIndex = 7;
			this.CB_P2_DrawPageImage.Text = "Rysuj obraz strony";
			this.CB_P2_DrawPageImage.UseVisualStyleBackColor = true;
			this.CB_P2_DrawPageImage.Visible = false;
			this.CB_P2_DrawPageImage.CheckedChanged += new System.EventHandler(this.CB_P2_DrawPageImage_CheckedChanged);
			// 
			// G_P2_PageApperance
			// 
			this.TLP_P2_PageDetails.SetColumnSpan(this.G_P2_PageApperance, 2);
			this.G_P2_PageApperance.Controls.Add(this.TLP_P2_PageAppearance);
			this.G_P2_PageApperance.Dock = System.Windows.Forms.DockStyle.Fill;
			this.G_P2_PageApperance.Location = new System.Drawing.Point(3, 48);
			this.G_P2_PageApperance.Name = "G_P2_PageApperance";
			this.G_P2_PageApperance.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
			this.TLP_P2_PageDetails.SetRowSpan(this.G_P2_PageApperance, 3);
			this.G_P2_PageApperance.Size = new System.Drawing.Size(253, 74);
			this.G_P2_PageApperance.TabIndex = 56;
			this.G_P2_PageApperance.TabStop = false;
			this.G_P2_PageApperance.Text = " Wygląd ";
			// 
			// TLP_P2_PageAppearance
			// 
			this.TLP_P2_PageAppearance.ColumnCount = 2;
			this.TLP_P2_PageAppearance.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.TLP_P2_PageAppearance.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.TLP_P2_PageAppearance.Controls.Add(this.TB_P2_PageColor, 0, 0);
			this.TLP_P2_PageAppearance.Controls.Add(this.B_P2_PageColor, 1, 0);
			this.TLP_P2_PageAppearance.Controls.Add(this.TB_P2_PageImage, 0, 1);
			this.TLP_P2_PageAppearance.Controls.Add(this.B_P2_PageImage, 1, 1);
			this.TLP_P2_PageAppearance.Dock = System.Windows.Forms.DockStyle.Fill;
			this.TLP_P2_PageAppearance.Location = new System.Drawing.Point(3, 13);
			this.TLP_P2_PageAppearance.Name = "TLP_P2_PageAppearance";
			this.TLP_P2_PageAppearance.RowCount = 2;
			this.TLP_P2_PageAppearance.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.TLP_P2_PageAppearance.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.TLP_P2_PageAppearance.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.TLP_P2_PageAppearance.Size = new System.Drawing.Size(247, 58);
			this.TLP_P2_PageAppearance.TabIndex = 0;
			// 
			// TB_P2_PageColor
			// 
			this.TB_P2_PageColor.Dock = System.Windows.Forms.DockStyle.Fill;
			this.TB_P2_PageColor.Enabled = false;
			this.TB_P2_PageColor.Location = new System.Drawing.Point(3, 5);
			this.TB_P2_PageColor.Margin = new System.Windows.Forms.Padding(3, 5, 3, 3);
			this.TB_P2_PageColor.Name = "TB_P2_PageColor";
			this.TB_P2_PageColor.Size = new System.Drawing.Size(117, 20);
			this.TB_P2_PageColor.TabIndex = 51;
			// 
			// B_P2_PageColor
			// 
			this.B_P2_PageColor.Dock = System.Windows.Forms.DockStyle.Fill;
			this.B_P2_PageColor.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.B_P2_PageColor.Location = new System.Drawing.Point(126, 2);
			this.B_P2_PageColor.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.B_P2_PageColor.Name = "B_P2_PageColor";
			this.B_P2_PageColor.Size = new System.Drawing.Size(118, 25);
			this.B_P2_PageColor.TabIndex = 52;
			this.B_P2_PageColor.Text = "Kolor";
			this.B_P2_PageColor.UseVisualStyleBackColor = true;
			this.B_P2_PageColor.Click += new System.EventHandler(this.B_P2_PageColor_Click);
			// 
			// TB_P2_PageImage
			// 
			this.TB_P2_PageImage.Dock = System.Windows.Forms.DockStyle.Fill;
			this.TB_P2_PageImage.Enabled = false;
			this.TB_P2_PageImage.Location = new System.Drawing.Point(3, 34);
			this.TB_P2_PageImage.Margin = new System.Windows.Forms.Padding(3, 5, 3, 3);
			this.TB_P2_PageImage.Name = "TB_P2_PageImage";
			this.TB_P2_PageImage.Size = new System.Drawing.Size(117, 20);
			this.TB_P2_PageImage.TabIndex = 54;
			// 
			// B_P2_PageImage
			// 
			this.B_P2_PageImage.Dock = System.Windows.Forms.DockStyle.Fill;
			this.B_P2_PageImage.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.B_P2_PageImage.Location = new System.Drawing.Point(126, 31);
			this.B_P2_PageImage.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.B_P2_PageImage.Name = "B_P2_PageImage";
			this.B_P2_PageImage.Size = new System.Drawing.Size(118, 25);
			this.B_P2_PageImage.TabIndex = 53;
			this.B_P2_PageImage.Text = "Obraz";
			this.B_P2_PageImage.UseVisualStyleBackColor = true;
			this.B_P2_PageImage.Click += new System.EventHandler(this.B_P2_PageImage_Click);
			// 
			// G_P2_GeneratePDF
			// 
			this.TLP_P2_PageDetails.SetColumnSpan(this.G_P2_GeneratePDF, 2);
			this.G_P2_GeneratePDF.Controls.Add(this.TLP_P2_GeneratePDF);
			this.G_P2_GeneratePDF.Dock = System.Windows.Forms.DockStyle.Fill;
			this.G_P2_GeneratePDF.Location = new System.Drawing.Point(3, 128);
			this.G_P2_GeneratePDF.Name = "G_P2_GeneratePDF";
			this.G_P2_GeneratePDF.Padding = new System.Windows.Forms.Padding(6, 2, 6, 3);
			this.TLP_P2_PageDetails.SetRowSpan(this.G_P2_GeneratePDF, 2);
			this.G_P2_GeneratePDF.Size = new System.Drawing.Size(253, 44);
			this.G_P2_GeneratePDF.TabIndex = 57;
			this.G_P2_GeneratePDF.TabStop = false;
			this.G_P2_GeneratePDF.Text = " Generowanie PDF ";
			// 
			// TLP_P2_GeneratePDF
			// 
			this.TLP_P2_GeneratePDF.ColumnCount = 1;
			this.TLP_P2_GeneratePDF.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.TLP_P2_GeneratePDF.Controls.Add(this.CB_P2_DrawPageColor, 0, 0);
			this.TLP_P2_GeneratePDF.Dock = System.Windows.Forms.DockStyle.Fill;
			this.TLP_P2_GeneratePDF.Location = new System.Drawing.Point(6, 15);
			this.TLP_P2_GeneratePDF.Name = "TLP_P2_GeneratePDF";
			this.TLP_P2_GeneratePDF.RowCount = 1;
			this.TLP_P2_GeneratePDF.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.TLP_P2_GeneratePDF.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
			this.TLP_P2_GeneratePDF.Size = new System.Drawing.Size(241, 26);
			this.TLP_P2_GeneratePDF.TabIndex = 0;
			// 
			// CB_P2_DrawPageColor
			// 
			this.CB_P2_DrawPageColor.AutoSize = true;
			this.CB_P2_DrawPageColor.Dock = System.Windows.Forms.DockStyle.Fill;
			this.CB_P2_DrawPageColor.Location = new System.Drawing.Point(3, 4);
			this.CB_P2_DrawPageColor.Margin = new System.Windows.Forms.Padding(3, 4, 3, 3);
			this.CB_P2_DrawPageColor.Name = "CB_P2_DrawPageColor";
			this.CB_P2_DrawPageColor.Size = new System.Drawing.Size(235, 19);
			this.CB_P2_DrawPageColor.TabIndex = 8;
			this.CB_P2_DrawPageColor.Text = "Rysuj kolor strony";
			this.CB_P2_DrawPageColor.UseVisualStyleBackColor = true;
			this.CB_P2_DrawPageColor.CheckedChanged += new System.EventHandler(this.CB_P2_DrawPageColor_CheckedChanged);
			// 
			// TLP_P2_Field
			// 
			this.TLP_P2_Field.AutoSize = true;
			this.TLP_P2_Field.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.TLP_P2_Field.ColumnCount = 2;
			this.TLP_P2_Field.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.TLP_P2_Field.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.TLP_P2_Field.Controls.Add(this.CBX_P2_TextTransform, 0, 15);
			this.TLP_P2_Field.Controls.Add(this.L_P2_TextTransform, 0, 14);
			this.TLP_P2_Field.Controls.Add(this.L_P2_Height, 1, 3);
			this.TLP_P2_Field.Controls.Add(this.N_P2_BorderSize, 1, 15);
			this.TLP_P2_Field.Controls.Add(this.N_P2_Padding, 1, 13);
			this.TLP_P2_Field.Controls.Add(this.L_P2_Padding, 1, 12);
			this.TLP_P2_Field.Controls.Add(this.L_P2_TextPosition, 0, 12);
			this.TLP_P2_Field.Controls.Add(this.TB_P2_LabelName, 0, 0);
			this.TLP_P2_Field.Controls.Add(this.N_P2_PosY, 1, 2);
			this.TLP_P2_Field.Controls.Add(this.N_P2_PosX, 0, 2);
			this.TLP_P2_Field.Controls.Add(this.L_P2_PosX, 0, 1);
			this.TLP_P2_Field.Controls.Add(this.L_P2_Width, 0, 3);
			this.TLP_P2_Field.Controls.Add(this.N_P2_Width, 0, 4);
			this.TLP_P2_Field.Controls.Add(this.N_P2_Height, 1, 4);
			this.TLP_P2_Field.Controls.Add(this.L_P2_BorderWidth, 1, 14);
			this.TLP_P2_Field.Controls.Add(this.CBX_P2_TextPosition, 0, 13);
			this.TLP_P2_Field.Controls.Add(this.L_P2_PosY, 1, 1);
			this.TLP_P2_Field.Controls.Add(this.G_P2_Appearance, 0, 5);
			this.TLP_P2_Field.Controls.Add(this.G_P2_Font, 0, 9);
			this.TLP_P2_Field.Dock = System.Windows.Forms.DockStyle.Fill;
			this.TLP_P2_Field.GrowStyle = System.Windows.Forms.TableLayoutPanelGrowStyle.FixedSize;
			this.TLP_P2_Field.Location = new System.Drawing.Point(0, 0);
			this.TLP_P2_Field.Margin = new System.Windows.Forms.Padding(0);
			this.TLP_P2_Field.Name = "TLP_P2_Field";
			this.TLP_P2_Field.RowCount = 18;
			this.TLP_P2_Field.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
			this.TLP_P2_Field.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.TLP_P2_Field.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
			this.TLP_P2_Field.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.TLP_P2_Field.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
			this.TLP_P2_Field.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 21F));
			this.TLP_P2_Field.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
			this.TLP_P2_Field.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
			this.TLP_P2_Field.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
			this.TLP_P2_Field.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 21F));
			this.TLP_P2_Field.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
			this.TLP_P2_Field.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
			this.TLP_P2_Field.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.TLP_P2_Field.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
			this.TLP_P2_Field.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.TLP_P2_Field.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
			this.TLP_P2_Field.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
			this.TLP_P2_Field.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.TLP_P2_Field.Size = new System.Drawing.Size(259, 403);
			this.TLP_P2_Field.TabIndex = 7;
			// 
			// CBX_P2_TextTransform
			// 
			this.CBX_P2_TextTransform.Dock = System.Windows.Forms.DockStyle.Fill;
			this.CBX_P2_TextTransform.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.CBX_P2_TextTransform.Enabled = false;
			this.CBX_P2_TextTransform.FormattingEnabled = true;
			this.CBX_P2_TextTransform.Items.AddRange(new object[] {
			"Nie zmieniaj",
			"Duże litery",
			"Małe litery",
			"Kapitaliki"});
			this.CBX_P2_TextTransform.Location = new System.Drawing.Point(3, 375);
			this.CBX_P2_TextTransform.Name = "CBX_P2_TextTransform";
			this.CBX_P2_TextTransform.Size = new System.Drawing.Size(123, 21);
			this.CBX_P2_TextTransform.TabIndex = 74;
			this.CBX_P2_TextTransform.SelectedIndexChanged += new System.EventHandler(this.CBX_P2_TextTransform_SelectedIndexChanged);
			// 
			// L_P2_TextTransform
			// 
			this.L_P2_TextTransform.AutoSize = true;
			this.L_P2_TextTransform.Dock = System.Windows.Forms.DockStyle.Fill;
			this.L_P2_TextTransform.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.L_P2_TextTransform.Location = new System.Drawing.Point(3, 352);
			this.L_P2_TextTransform.Name = "L_P2_TextTransform";
			this.L_P2_TextTransform.Size = new System.Drawing.Size(123, 20);
			this.L_P2_TextTransform.TabIndex = 73;
			this.L_P2_TextTransform.Text = "Wyświetlanie tekstu:";
			this.L_P2_TextTransform.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// L_P2_Height
			// 
			this.L_P2_Height.AutoSize = true;
			this.L_P2_Height.Dock = System.Windows.Forms.DockStyle.Fill;
			this.L_P2_Height.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.L_P2_Height.Location = new System.Drawing.Point(132, 70);
			this.L_P2_Height.Name = "L_P2_Height";
			this.L_P2_Height.Size = new System.Drawing.Size(124, 20);
			this.L_P2_Height.TabIndex = 72;
			this.L_P2_Height.Text = "Wysokość:";
			this.L_P2_Height.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// N_P2_BorderSize
			// 
			this.N_P2_BorderSize.DecimalPlaces = 1;
			this.N_P2_BorderSize.Dock = System.Windows.Forms.DockStyle.Fill;
			this.N_P2_BorderSize.Enabled = false;
			this.N_P2_BorderSize.Increment = new decimal(new int[] {
			1,
			0,
			0,
			65536});
			this.N_P2_BorderSize.Location = new System.Drawing.Point(132, 375);
			this.N_P2_BorderSize.Maximum = new decimal(new int[] {
			10,
			0,
			0,
			0});
			this.N_P2_BorderSize.Name = "N_P2_BorderSize";
			this.N_P2_BorderSize.Size = new System.Drawing.Size(124, 20);
			this.N_P2_BorderSize.TabIndex = 65;
			this.N_P2_BorderSize.ValueChanged += new System.EventHandler(this.N_P2_BorderSize_ValueChanged);
			// 
			// N_P2_Padding
			// 
			this.N_P2_Padding.DecimalPlaces = 1;
			this.N_P2_Padding.Dock = System.Windows.Forms.DockStyle.Fill;
			this.N_P2_Padding.Enabled = false;
			this.N_P2_Padding.Increment = new decimal(new int[] {
			1,
			0,
			0,
			65536});
			this.N_P2_Padding.Location = new System.Drawing.Point(132, 330);
			this.N_P2_Padding.Maximum = new decimal(new int[] {
			20,
			0,
			0,
			0});
			this.N_P2_Padding.Name = "N_P2_Padding";
			this.N_P2_Padding.Size = new System.Drawing.Size(124, 20);
			this.N_P2_Padding.TabIndex = 63;
			this.N_P2_Padding.ValueChanged += new System.EventHandler(this.N_P2_Padding_ValueChanged);
			// 
			// L_P2_Padding
			// 
			this.L_P2_Padding.AutoSize = true;
			this.L_P2_Padding.Dock = System.Windows.Forms.DockStyle.Fill;
			this.L_P2_Padding.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.L_P2_Padding.Location = new System.Drawing.Point(132, 307);
			this.L_P2_Padding.Name = "L_P2_Padding";
			this.L_P2_Padding.Size = new System.Drawing.Size(124, 20);
			this.L_P2_Padding.TabIndex = 61;
			this.L_P2_Padding.Text = "Margines:";
			this.L_P2_Padding.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// L_P2_TextPosition
			// 
			this.L_P2_TextPosition.AutoSize = true;
			this.L_P2_TextPosition.Dock = System.Windows.Forms.DockStyle.Fill;
			this.L_P2_TextPosition.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.L_P2_TextPosition.Location = new System.Drawing.Point(3, 307);
			this.L_P2_TextPosition.Name = "L_P2_TextPosition";
			this.L_P2_TextPosition.Size = new System.Drawing.Size(123, 20);
			this.L_P2_TextPosition.TabIndex = 60;
			this.L_P2_TextPosition.Text = "Położenie tekstu:";
			this.L_P2_TextPosition.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// TB_P2_LabelName
			// 
			this.TLP_P2_Field.SetColumnSpan(this.TB_P2_LabelName, 2);
			this.TB_P2_LabelName.Dock = System.Windows.Forms.DockStyle.Fill;
			this.TB_P2_LabelName.Enabled = false;
			this.TB_P2_LabelName.Location = new System.Drawing.Point(3, 3);
			this.TB_P2_LabelName.MaxLength = 127;
			this.TB_P2_LabelName.Name = "TB_P2_LabelName";
			this.TB_P2_LabelName.Size = new System.Drawing.Size(253, 20);
			this.TB_P2_LabelName.TabIndex = 0;
			this.TB_P2_LabelName.TextChanged += new System.EventHandler(this.TB_P2_LabelName_TextChanged);
			this.TB_P2_LabelName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TB_P2_LabelName_KeyPress);
			this.TB_P2_LabelName.Leave += new System.EventHandler(this.TB_P2_LabelName_Leave);
			// 
			// N_P2_PosY
			// 
			this.N_P2_PosY.DecimalPlaces = 1;
			this.N_P2_PosY.Dock = System.Windows.Forms.DockStyle.Fill;
			this.N_P2_PosY.Enabled = false;
			this.N_P2_PosY.Location = new System.Drawing.Point(132, 48);
			this.N_P2_PosY.Maximum = new decimal(new int[] {
			5000,
			0,
			0,
			0});
			this.N_P2_PosY.Name = "N_P2_PosY";
			this.N_P2_PosY.Size = new System.Drawing.Size(124, 20);
			this.N_P2_PosY.TabIndex = 2;
			this.N_P2_PosY.ValueChanged += new System.EventHandler(this.N_P2_PosY_ValueChanged);
			// 
			// N_P2_PosX
			// 
			this.N_P2_PosX.DecimalPlaces = 1;
			this.N_P2_PosX.Dock = System.Windows.Forms.DockStyle.Fill;
			this.N_P2_PosX.Enabled = false;
			this.N_P2_PosX.Location = new System.Drawing.Point(3, 48);
			this.N_P2_PosX.Maximum = new decimal(new int[] {
			5000,
			0,
			0,
			0});
			this.N_P2_PosX.Name = "N_P2_PosX";
			this.N_P2_PosX.Size = new System.Drawing.Size(123, 20);
			this.N_P2_PosX.TabIndex = 1;
			this.N_P2_PosX.ValueChanged += new System.EventHandler(this.N_P2_PosX_ValueChanged);
			// 
			// L_P2_PosX
			// 
			this.L_P2_PosX.AutoSize = true;
			this.L_P2_PosX.Dock = System.Windows.Forms.DockStyle.Fill;
			this.L_P2_PosX.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.L_P2_PosX.Location = new System.Drawing.Point(3, 25);
			this.L_P2_PosX.Name = "L_P2_PosX";
			this.L_P2_PosX.Size = new System.Drawing.Size(123, 20);
			this.L_P2_PosX.TabIndex = 4;
			this.L_P2_PosX.Text = "Pozycja X:";
			this.L_P2_PosX.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// L_P2_Width
			// 
			this.L_P2_Width.AutoSize = true;
			this.L_P2_Width.Dock = System.Windows.Forms.DockStyle.Fill;
			this.L_P2_Width.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.L_P2_Width.Location = new System.Drawing.Point(3, 70);
			this.L_P2_Width.Name = "L_P2_Width";
			this.L_P2_Width.Size = new System.Drawing.Size(123, 20);
			this.L_P2_Width.TabIndex = 6;
			this.L_P2_Width.Text = "Szerokość:";
			this.L_P2_Width.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// N_P2_Width
			// 
			this.N_P2_Width.DecimalPlaces = 1;
			this.N_P2_Width.Dock = System.Windows.Forms.DockStyle.Fill;
			this.N_P2_Width.Enabled = false;
			this.N_P2_Width.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.N_P2_Width.Location = new System.Drawing.Point(3, 93);
			this.N_P2_Width.Maximum = new decimal(new int[] {
			5000,
			0,
			0,
			0});
			this.N_P2_Width.Name = "N_P2_Width";
			this.N_P2_Width.Size = new System.Drawing.Size(123, 20);
			this.N_P2_Width.TabIndex = 3;
			this.N_P2_Width.ValueChanged += new System.EventHandler(this.N_P2_Width_ValueChanged);
			// 
			// N_P2_Height
			// 
			this.N_P2_Height.DecimalPlaces = 1;
			this.N_P2_Height.Dock = System.Windows.Forms.DockStyle.Fill;
			this.N_P2_Height.Enabled = false;
			this.N_P2_Height.Location = new System.Drawing.Point(132, 93);
			this.N_P2_Height.Maximum = new decimal(new int[] {
			5000,
			0,
			0,
			0});
			this.N_P2_Height.Name = "N_P2_Height";
			this.N_P2_Height.Size = new System.Drawing.Size(124, 20);
			this.N_P2_Height.TabIndex = 4;
			this.N_P2_Height.ValueChanged += new System.EventHandler(this.N_P2_Height_ValueChanged);
			// 
			// L_P2_BorderWidth
			// 
			this.L_P2_BorderWidth.AutoSize = true;
			this.L_P2_BorderWidth.Dock = System.Windows.Forms.DockStyle.Fill;
			this.L_P2_BorderWidth.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.L_P2_BorderWidth.Location = new System.Drawing.Point(132, 352);
			this.L_P2_BorderWidth.Name = "L_P2_BorderWidth";
			this.L_P2_BorderWidth.Size = new System.Drawing.Size(124, 20);
			this.L_P2_BorderWidth.TabIndex = 64;
			this.L_P2_BorderWidth.Text = "Grubość ramki:";
			this.L_P2_BorderWidth.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// CBX_P2_TextPosition
			// 
			this.CBX_P2_TextPosition.Dock = System.Windows.Forms.DockStyle.Fill;
			this.CBX_P2_TextPosition.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.CBX_P2_TextPosition.Enabled = false;
			this.CBX_P2_TextPosition.FormattingEnabled = true;
			this.CBX_P2_TextPosition.Items.AddRange(new object[] {
			"Góra-Lewo",
			"Góra-Środek",
			"Góra-Prawo",
			"Środek-Lewo",
			"Środek",
			"Środek-Prawo",
			"Dół-Lewo",
			"Dół-Środek",
			"Dół-Prawo"});
			this.CBX_P2_TextPosition.Location = new System.Drawing.Point(3, 330);
			this.CBX_P2_TextPosition.Name = "CBX_P2_TextPosition";
			this.CBX_P2_TextPosition.Size = new System.Drawing.Size(123, 21);
			this.CBX_P2_TextPosition.TabIndex = 68;
			this.CBX_P2_TextPosition.SelectedIndexChanged += new System.EventHandler(this.CBX_P2_TextPosition_SelectedIndexChanged);
			// 
			// L_P2_PosY
			// 
			this.L_P2_PosY.AutoSize = true;
			this.L_P2_PosY.Dock = System.Windows.Forms.DockStyle.Fill;
			this.L_P2_PosY.Location = new System.Drawing.Point(132, 25);
			this.L_P2_PosY.Name = "L_P2_PosY";
			this.L_P2_PosY.Size = new System.Drawing.Size(124, 20);
			this.L_P2_PosY.TabIndex = 71;
			this.L_P2_PosY.Text = "Pozycja Y:";
			this.L_P2_PosY.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// G_P2_Appearance
			// 
			this.TLP_P2_Field.SetColumnSpan(this.G_P2_Appearance, 2);
			this.G_P2_Appearance.Controls.Add(this.TLP_P2_FieldAppearance);
			this.G_P2_Appearance.Dock = System.Windows.Forms.DockStyle.Fill;
			this.G_P2_Appearance.Location = new System.Drawing.Point(3, 118);
			this.G_P2_Appearance.Margin = new System.Windows.Forms.Padding(3, 3, 3, 2);
			this.G_P2_Appearance.Name = "G_P2_Appearance";
			this.G_P2_Appearance.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
			this.TLP_P2_Field.SetRowSpan(this.G_P2_Appearance, 4);
			this.G_P2_Appearance.Size = new System.Drawing.Size(253, 106);
			this.G_P2_Appearance.TabIndex = 75;
			this.G_P2_Appearance.TabStop = false;
			this.G_P2_Appearance.Text = " Wygląd ";
			// 
			// TLP_P2_FieldAppearance
			// 
			this.TLP_P2_FieldAppearance.ColumnCount = 2;
			this.TLP_P2_FieldAppearance.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.TLP_P2_FieldAppearance.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.TLP_P2_FieldAppearance.Controls.Add(this.B_P2_BackImage, 1, 2);
			this.TLP_P2_FieldAppearance.Controls.Add(this.TB_P2_BackImage, 0, 2);
			this.TLP_P2_FieldAppearance.Controls.Add(this.B_P2_BackColor, 1, 1);
			this.TLP_P2_FieldAppearance.Controls.Add(this.TB_P2_BackColor, 0, 1);
			this.TLP_P2_FieldAppearance.Controls.Add(this.TB_P2_BorderColor, 0, 0);
			this.TLP_P2_FieldAppearance.Controls.Add(this.B_P2_BorderColor, 1, 0);
			this.TLP_P2_FieldAppearance.Dock = System.Windows.Forms.DockStyle.Fill;
			this.TLP_P2_FieldAppearance.Location = new System.Drawing.Point(3, 13);
			this.TLP_P2_FieldAppearance.Margin = new System.Windows.Forms.Padding(0);
			this.TLP_P2_FieldAppearance.Name = "TLP_P2_FieldAppearance";
			this.TLP_P2_FieldAppearance.RowCount = 3;
			this.TLP_P2_FieldAppearance.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
			this.TLP_P2_FieldAppearance.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
			this.TLP_P2_FieldAppearance.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.TLP_P2_FieldAppearance.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.TLP_P2_FieldAppearance.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.TLP_P2_FieldAppearance.Size = new System.Drawing.Size(247, 90);
			this.TLP_P2_FieldAppearance.TabIndex = 0;
			// 
			// B_P2_BackImage
			// 
			this.B_P2_BackImage.Dock = System.Windows.Forms.DockStyle.Fill;
			this.B_P2_BackImage.Enabled = false;
			this.B_P2_BackImage.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.B_P2_BackImage.Location = new System.Drawing.Point(126, 63);
			this.B_P2_BackImage.Margin = new System.Windows.Forms.Padding(3, 3, 3, 2);
			this.B_P2_BackImage.Name = "B_P2_BackImage";
			this.B_P2_BackImage.Size = new System.Drawing.Size(118, 25);
			this.B_P2_BackImage.TabIndex = 52;
			this.B_P2_BackImage.Text = "Obraz";
			this.B_P2_BackImage.UseVisualStyleBackColor = true;
			this.B_P2_BackImage.Click += new System.EventHandler(this.B_P2_BackImage_Click);
			// 
			// TB_P2_BackImage
			// 
			this.TB_P2_BackImage.Dock = System.Windows.Forms.DockStyle.Fill;
			this.TB_P2_BackImage.Enabled = false;
			this.TB_P2_BackImage.Location = new System.Drawing.Point(3, 66);
			this.TB_P2_BackImage.Margin = new System.Windows.Forms.Padding(3, 6, 3, 3);
			this.TB_P2_BackImage.Name = "TB_P2_BackImage";
			this.TB_P2_BackImage.Size = new System.Drawing.Size(117, 20);
			this.TB_P2_BackImage.TabIndex = 51;
			// 
			// B_P2_BackColor
			// 
			this.B_P2_BackColor.Dock = System.Windows.Forms.DockStyle.Fill;
			this.B_P2_BackColor.Enabled = false;
			this.B_P2_BackColor.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.B_P2_BackColor.Location = new System.Drawing.Point(126, 33);
			this.B_P2_BackColor.Margin = new System.Windows.Forms.Padding(3, 3, 3, 2);
			this.B_P2_BackColor.Name = "B_P2_BackColor";
			this.B_P2_BackColor.Size = new System.Drawing.Size(118, 25);
			this.B_P2_BackColor.TabIndex = 50;
			this.B_P2_BackColor.Text = "Kolor";
			this.B_P2_BackColor.UseVisualStyleBackColor = true;
			this.B_P2_BackColor.Click += new System.EventHandler(this.B_P2_BackColor_Click);
			// 
			// TB_P2_BackColor
			// 
			this.TB_P2_BackColor.Dock = System.Windows.Forms.DockStyle.Fill;
			this.TB_P2_BackColor.Enabled = false;
			this.TB_P2_BackColor.Location = new System.Drawing.Point(3, 36);
			this.TB_P2_BackColor.Margin = new System.Windows.Forms.Padding(3, 6, 3, 3);
			this.TB_P2_BackColor.Name = "TB_P2_BackColor";
			this.TB_P2_BackColor.Size = new System.Drawing.Size(117, 20);
			this.TB_P2_BackColor.TabIndex = 49;
			// 
			// TB_P2_BorderColor
			// 
			this.TB_P2_BorderColor.Dock = System.Windows.Forms.DockStyle.Fill;
			this.TB_P2_BorderColor.Enabled = false;
			this.TB_P2_BorderColor.Location = new System.Drawing.Point(3, 6);
			this.TB_P2_BorderColor.Margin = new System.Windows.Forms.Padding(3, 6, 3, 3);
			this.TB_P2_BorderColor.Name = "TB_P2_BorderColor";
			this.TB_P2_BorderColor.Size = new System.Drawing.Size(117, 20);
			this.TB_P2_BorderColor.TabIndex = 5;
			// 
			// B_P2_BorderColor
			// 
			this.B_P2_BorderColor.Dock = System.Windows.Forms.DockStyle.Fill;
			this.B_P2_BorderColor.Enabled = false;
			this.B_P2_BorderColor.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.B_P2_BorderColor.Location = new System.Drawing.Point(126, 3);
			this.B_P2_BorderColor.Margin = new System.Windows.Forms.Padding(3, 3, 3, 2);
			this.B_P2_BorderColor.Name = "B_P2_BorderColor";
			this.B_P2_BorderColor.Size = new System.Drawing.Size(118, 25);
			this.B_P2_BorderColor.TabIndex = 6;
			this.B_P2_BorderColor.Text = "Ramka";
			this.B_P2_BorderColor.UseVisualStyleBackColor = true;
			this.B_P2_BorderColor.Click += new System.EventHandler(this.B_P2_BorderColor_Click);
			// 
			// G_P2_Font
			// 
			this.TLP_P2_Field.SetColumnSpan(this.G_P2_Font, 2);
			this.G_P2_Font.Controls.Add(this.TLP_P2_FontSettings);
			this.G_P2_Font.Dock = System.Windows.Forms.DockStyle.Fill;
			this.G_P2_Font.Location = new System.Drawing.Point(3, 228);
			this.G_P2_Font.Margin = new System.Windows.Forms.Padding(3, 2, 3, 3);
			this.G_P2_Font.Name = "G_P2_Font";
			this.G_P2_Font.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
			this.TLP_P2_Field.SetRowSpan(this.G_P2_Font, 3);
			this.G_P2_Font.Size = new System.Drawing.Size(253, 76);
			this.G_P2_Font.TabIndex = 76;
			this.G_P2_Font.TabStop = false;
			this.G_P2_Font.Text = " Czcionka ";
			// 
			// TLP_P2_FontSettings
			// 
			this.TLP_P2_FontSettings.ColumnCount = 2;
			this.TLP_P2_FontSettings.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.TLP_P2_FontSettings.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.TLP_P2_FontSettings.Controls.Add(this.TB_P2_FontName, 0, 1);
			this.TLP_P2_FontSettings.Controls.Add(this.B_P2_FontName, 1, 1);
			this.TLP_P2_FontSettings.Controls.Add(this.TB_P2_FontColor, 0, 0);
			this.TLP_P2_FontSettings.Controls.Add(this.B_P2_FontColor, 1, 0);
			this.TLP_P2_FontSettings.Dock = System.Windows.Forms.DockStyle.Fill;
			this.TLP_P2_FontSettings.Location = new System.Drawing.Point(3, 13);
			this.TLP_P2_FontSettings.Name = "TLP_P2_FontSettings";
			this.TLP_P2_FontSettings.RowCount = 2;
			this.TLP_P2_FontSettings.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.TLP_P2_FontSettings.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.TLP_P2_FontSettings.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.TLP_P2_FontSettings.Size = new System.Drawing.Size(247, 60);
			this.TLP_P2_FontSettings.TabIndex = 0;
			// 
			// TB_P2_FontName
			// 
			this.TB_P2_FontName.Dock = System.Windows.Forms.DockStyle.Fill;
			this.TB_P2_FontName.Enabled = false;
			this.TB_P2_FontName.Location = new System.Drawing.Point(3, 36);
			this.TB_P2_FontName.Margin = new System.Windows.Forms.Padding(3, 6, 3, 3);
			this.TB_P2_FontName.Name = "TB_P2_FontName";
			this.TB_P2_FontName.Size = new System.Drawing.Size(117, 20);
			this.TB_P2_FontName.TabIndex = 57;
			// 
			// B_P2_FontName
			// 
			this.B_P2_FontName.Dock = System.Windows.Forms.DockStyle.Fill;
			this.B_P2_FontName.Enabled = false;
			this.B_P2_FontName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.B_P2_FontName.Location = new System.Drawing.Point(126, 33);
			this.B_P2_FontName.Margin = new System.Windows.Forms.Padding(3, 3, 3, 2);
			this.B_P2_FontName.Name = "B_P2_FontName";
			this.B_P2_FontName.Size = new System.Drawing.Size(118, 25);
			this.B_P2_FontName.TabIndex = 59;
			this.B_P2_FontName.Text = "Nazwa";
			this.B_P2_FontName.UseVisualStyleBackColor = true;
			this.B_P2_FontName.Click += new System.EventHandler(this.B_P2_FontName_Click);
			// 
			// TB_P2_FontColor
			// 
			this.TB_P2_FontColor.Dock = System.Windows.Forms.DockStyle.Fill;
			this.TB_P2_FontColor.Enabled = false;
			this.TB_P2_FontColor.Location = new System.Drawing.Point(3, 6);
			this.TB_P2_FontColor.Margin = new System.Windows.Forms.Padding(3, 6, 3, 3);
			this.TB_P2_FontColor.Name = "TB_P2_FontColor";
			this.TB_P2_FontColor.Size = new System.Drawing.Size(117, 20);
			this.TB_P2_FontColor.TabIndex = 34;
			// 
			// B_P2_FontColor
			// 
			this.B_P2_FontColor.Dock = System.Windows.Forms.DockStyle.Fill;
			this.B_P2_FontColor.Enabled = false;
			this.B_P2_FontColor.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.B_P2_FontColor.Location = new System.Drawing.Point(126, 3);
			this.B_P2_FontColor.Margin = new System.Windows.Forms.Padding(3, 3, 3, 2);
			this.B_P2_FontColor.Name = "B_P2_FontColor";
			this.B_P2_FontColor.Size = new System.Drawing.Size(118, 25);
			this.B_P2_FontColor.TabIndex = 35;
			this.B_P2_FontColor.Text = "Kolor";
			this.B_P2_FontColor.UseVisualStyleBackColor = true;
			this.B_P2_FontColor.Click += new System.EventHandler(this.B_P2_FontColor_Click);
			// 
			// SC_P3_Generator
			// 
			this.SC_P3_Generator.Dock = System.Windows.Forms.DockStyle.Fill;
			this.SC_P3_Generator.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
			this.SC_P3_Generator.Location = new System.Drawing.Point(5, 37);
			this.SC_P3_Generator.Margin = new System.Windows.Forms.Padding(5);
			this.SC_P3_Generator.Name = "SC_P3_Generator";
			// 
			// SC_P3_Generator.Panel1
			// 
			this.SC_P3_Generator.Panel1.Controls.Add(this.P_P3_Generator);
			this.SC_P3_Generator.Panel1MinSize = 424;
			// 
			// SC_P3_Generator.Panel2
			// 
			this.SC_P3_Generator.Panel2.Controls.Add(this.TLP_PageList);
			this.SC_P3_Generator.Panel2MinSize = 250;
			this.SC_P3_Generator.Size = new System.Drawing.Size(714, 438);
			this.SC_P3_Generator.SplitterDistance = 453;
			this.SC_P3_Generator.SplitterWidth = 5;
			this.SC_P3_Generator.TabIndex = 3;
			// 
			// TLP_PageList
			// 
			this.TLP_PageList.ColumnCount = 1;
			this.TLP_PageList.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.TLP_PageList.Controls.Add(this.CB_P3_CollatePages, 0, 1);
			this.TLP_PageList.Controls.Add(this.TV_P3_PageList, 0, 0);
			this.TLP_PageList.Dock = System.Windows.Forms.DockStyle.Fill;
			this.TLP_PageList.Location = new System.Drawing.Point(0, 0);
			this.TLP_PageList.Name = "TLP_PageList";
			this.TLP_PageList.RowCount = 2;
			this.TLP_PageList.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.TLP_PageList.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22F));
			this.TLP_PageList.Size = new System.Drawing.Size(256, 438);
			this.TLP_PageList.TabIndex = 2;
			// 
			// CB_P3_CollatePages
			// 
			this.CB_P3_CollatePages.AutoSize = true;
			this.CB_P3_CollatePages.Checked = true;
			this.CB_P3_CollatePages.CheckState = System.Windows.Forms.CheckState.Checked;
			this.CB_P3_CollatePages.Dock = System.Windows.Forms.DockStyle.Fill;
			this.CB_P3_CollatePages.Location = new System.Drawing.Point(3, 421);
			this.CB_P3_CollatePages.Margin = new System.Windows.Forms.Padding(3, 5, 3, 0);
			this.CB_P3_CollatePages.Name = "CB_P3_CollatePages";
			this.CB_P3_CollatePages.Size = new System.Drawing.Size(250, 17);
			this.CB_P3_CollatePages.TabIndex = 7;
			this.CB_P3_CollatePages.Text = "Połącz strony w pozycjach";
			this.CB_P3_CollatePages.UseVisualStyleBackColor = true;
			// 
			// SC_P1_Main
			// 
			this.SC_P1_Main.Dock = System.Windows.Forms.DockStyle.Fill;
			this.SC_P1_Main.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
			this.SC_P1_Main.Location = new System.Drawing.Point(5, 37);
			this.SC_P1_Main.Margin = new System.Windows.Forms.Padding(5);
			this.SC_P1_Main.Name = "SC_P1_Main";
			// 
			// SC_P1_Main.Panel1
			// 
			this.SC_P1_Main.Panel1.Controls.Add(this.SC_P1_Details);
			this.SC_P1_Main.Panel1MinSize = 230;
			// 
			// SC_P1_Main.Panel2
			// 
			this.SC_P1_Main.Panel2.Controls.Add(this.P_P1_Preview);
			this.SC_P1_Main.Panel2MinSize = 416;
			this.SC_P1_Main.Size = new System.Drawing.Size(714, 438);
			this.SC_P1_Main.SplitterDistance = 230;
			this.SC_P1_Main.SplitterWidth = 5;
			this.SC_P1_Main.TabIndex = 7;
			// 
			// SC_P1_Details
			// 
			this.SC_P1_Details.Dock = System.Windows.Forms.DockStyle.Fill;
			this.SC_P1_Details.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
			this.SC_P1_Details.Location = new System.Drawing.Point(0, 0);
			this.SC_P1_Details.Name = "SC_P1_Details";
			this.SC_P1_Details.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// SC_P1_Details.Panel1
			// 
			this.SC_P1_Details.Panel1.Controls.Add(this.TV_P1_Patterns);
			this.SC_P1_Details.Panel1MinSize = 300;
			// 
			// SC_P1_Details.Panel2
			// 
			this.SC_P1_Details.Panel2.Controls.Add(this.RTB_P1_Details);
			this.SC_P1_Details.Panel2Collapsed = true;
			this.SC_P1_Details.Panel2MinSize = 100;
			this.SC_P1_Details.Size = new System.Drawing.Size(230, 438);
			this.SC_P1_Details.SplitterDistance = 300;
			this.SC_P1_Details.SplitterWidth = 5;
			this.SC_P1_Details.TabIndex = 8;
			// 
			// RTB_P1_Details
			// 
			this.RTB_P1_Details.Dock = System.Windows.Forms.DockStyle.Fill;
			this.RTB_P1_Details.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.RTB_P1_Details.Location = new System.Drawing.Point(0, 0);
			this.RTB_P1_Details.Name = "RTB_P1_Details";
			this.RTB_P1_Details.Size = new System.Drawing.Size(150, 46);
			this.RTB_P1_Details.TabIndex = 0;
			this.RTB_P1_Details.Text = "";
			// 
			// TLP_Main
			// 
			this.TLP_Main.ColumnCount = 1;
			this.TLP_Main.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.TLP_Main.Controls.Add(this.SC_P1_Main, 0, 0);
			this.TLP_Main.Controls.Add(this.TLP_P1_StatusBar, 0, 1);
			this.TLP_Main.Dock = System.Windows.Forms.DockStyle.Fill;
			this.TLP_Main.Location = new System.Drawing.Point(0, 0);
			this.TLP_Main.Margin = new System.Windows.Forms.Padding(0);
			this.TLP_Main.Name = "TLP_Main";
			this.TLP_Main.Padding = new System.Windows.Forms.Padding(0, 32, 0, 0);
			this.TLP_Main.RowCount = 2;
			this.TLP_Main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.TLP_Main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
			this.TLP_Main.Size = new System.Drawing.Size(724, 512);
			this.TLP_Main.TabIndex = 6;
			// 
			// TLP_P1_StatusBar
			// 
			this.TLP_P1_StatusBar.BackColor = System.Drawing.SystemColors.ControlLight;
			this.TLP_P1_StatusBar.ColumnCount = 5;
			this.TLP_P1_StatusBar.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
			this.TLP_P1_StatusBar.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
			this.TLP_P1_StatusBar.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 80F));
			this.TLP_P1_StatusBar.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
			this.TLP_P1_StatusBar.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 54F));
			this.TLP_P1_StatusBar.Controls.Add(this.B_P1_New, 0, 0);
			this.TLP_P1_StatusBar.Controls.Add(this.B_P1_Delete, 1, 0);
			this.TLP_P1_StatusBar.Controls.Add(this.CB_P1_ShowDetails, 2, 0);
			this.TLP_P1_StatusBar.Controls.Add(this.L_P1_Page, 3, 0);
			this.TLP_P1_StatusBar.Controls.Add(this.N_P1_Page, 4, 0);
			this.TLP_P1_StatusBar.Dock = System.Windows.Forms.DockStyle.Fill;
			this.TLP_P1_StatusBar.Location = new System.Drawing.Point(0, 480);
			this.TLP_P1_StatusBar.Margin = new System.Windows.Forms.Padding(0);
			this.TLP_P1_StatusBar.Name = "TLP_P1_StatusBar";
			this.TLP_P1_StatusBar.Padding = new System.Windows.Forms.Padding(0, 1, 0, 0);
			this.TLP_P1_StatusBar.RowCount = 1;
			this.TLP_P1_StatusBar.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.TLP_P1_StatusBar.Size = new System.Drawing.Size(724, 32);
			this.TLP_P1_StatusBar.TabIndex = 6;
			this.TLP_P1_StatusBar.Paint += new System.Windows.Forms.PaintEventHandler(this.TLP_P1_StatusBar_Paint);
			// 
			// CB_P1_ShowDetails
			// 
			this.CB_P1_ShowDetails.AutoSize = true;
			this.CB_P1_ShowDetails.Dock = System.Windows.Forms.DockStyle.Left;
			this.CB_P1_ShowDetails.Location = new System.Drawing.Point(243, 4);
			this.CB_P1_ShowDetails.Name = "CB_P1_ShowDetails";
			this.CB_P1_ShowDetails.Size = new System.Drawing.Size(213, 25);
			this.CB_P1_ShowDetails.TabIndex = 4;
			this.CB_P1_ShowDetails.Text = "Pokaż szczegóły zaznaczonego wzoru.";
			this.CB_P1_ShowDetails.UseVisualStyleBackColor = true;
			this.CB_P1_ShowDetails.CheckedChanged += new System.EventHandler(this.CB_P1_ShowDetails_CheckedChanged);
			// 
			// TLP_Generator
			// 
			this.TLP_Generator.ColumnCount = 1;
			this.TLP_Generator.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.TLP_Generator.Controls.Add(this.SC_P3_Generator, 0, 0);
			this.TLP_Generator.Controls.Add(this.TLP_P3_StatusBar, 0, 1);
			this.TLP_Generator.Dock = System.Windows.Forms.DockStyle.Fill;
			this.TLP_Generator.Location = new System.Drawing.Point(0, 0);
			this.TLP_Generator.Margin = new System.Windows.Forms.Padding(0);
			this.TLP_Generator.Name = "TLP_Generator";
			this.TLP_Generator.Padding = new System.Windows.Forms.Padding(0, 32, 0, 0);
			this.TLP_Generator.RowCount = 2;
			this.TLP_Generator.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.TLP_Generator.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
			this.TLP_Generator.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.TLP_Generator.Size = new System.Drawing.Size(724, 512);
			this.TLP_Generator.TabIndex = 7;
			this.TLP_Generator.Visible = false;
			// 
			// TLP_P3_StatusBar
			// 
			this.TLP_P3_StatusBar.BackColor = System.Drawing.SystemColors.ControlLight;
			this.TLP_P3_StatusBar.ColumnCount = 2;
			this.TLP_P3_StatusBar.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.TLP_P3_StatusBar.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 257F));
			this.TLP_P3_StatusBar.Controls.Add(this.TLP_P3_InfoControls, 0, 0);
			this.TLP_P3_StatusBar.Controls.Add(this.TLP_P3_Buttons, 1, 0);
			this.TLP_P3_StatusBar.Dock = System.Windows.Forms.DockStyle.Fill;
			this.TLP_P3_StatusBar.Location = new System.Drawing.Point(0, 480);
			this.TLP_P3_StatusBar.Margin = new System.Windows.Forms.Padding(0);
			this.TLP_P3_StatusBar.Name = "TLP_P3_StatusBar";
			this.TLP_P3_StatusBar.Padding = new System.Windows.Forms.Padding(0, 1, 0, 0);
			this.TLP_P3_StatusBar.RowCount = 1;
			this.TLP_P3_StatusBar.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.TLP_P3_StatusBar.Size = new System.Drawing.Size(724, 32);
			this.TLP_P3_StatusBar.TabIndex = 5;
			this.TLP_P3_StatusBar.Paint += new System.Windows.Forms.PaintEventHandler(this.TLP_P1_StatusBar_Paint);
			// 
			// TLP_P3_InfoControls
			// 
			this.TLP_P3_InfoControls.ColumnCount = 4;
			this.TLP_P3_InfoControls.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
			this.TLP_P3_InfoControls.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.TLP_P3_InfoControls.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 200F));
			this.TLP_P3_InfoControls.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 105F));
			this.TLP_P3_InfoControls.Controls.Add(this.FLP_P3_Rows, 2, 0);
			this.TLP_P3_InfoControls.Controls.Add(this.FLP_P3_Page, 3, 0);
			this.TLP_P3_InfoControls.Controls.Add(this.CBX_P3_Scale, 0, 0);
			this.TLP_P3_InfoControls.Dock = System.Windows.Forms.DockStyle.Fill;
			this.TLP_P3_InfoControls.Location = new System.Drawing.Point(0, 1);
			this.TLP_P3_InfoControls.Margin = new System.Windows.Forms.Padding(0, 0, 3, 0);
			this.TLP_P3_InfoControls.Name = "TLP_P3_InfoControls";
			this.TLP_P3_InfoControls.RowCount = 1;
			this.TLP_P3_InfoControls.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.TLP_P3_InfoControls.Size = new System.Drawing.Size(464, 31);
			this.TLP_P3_InfoControls.TabIndex = 3;
			// 
			// FLP_P3_Rows
			// 
			this.FLP_P3_Rows.Controls.Add(this.N_P3_Rows);
			this.FLP_P3_Rows.Controls.Add(this.L_P3_Rows);
			this.FLP_P3_Rows.Dock = System.Windows.Forms.DockStyle.Fill;
			this.FLP_P3_Rows.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
			this.FLP_P3_Rows.Location = new System.Drawing.Point(159, 0);
			this.FLP_P3_Rows.Margin = new System.Windows.Forms.Padding(0);
			this.FLP_P3_Rows.Name = "FLP_P3_Rows";
			this.FLP_P3_Rows.Size = new System.Drawing.Size(200, 31);
			this.FLP_P3_Rows.TabIndex = 13;
			// 
			// N_P3_Rows
			// 
			this.N_P3_Rows.Location = new System.Drawing.Point(155, 5);
			this.N_P3_Rows.Margin = new System.Windows.Forms.Padding(3, 5, 0, 3);
			this.N_P3_Rows.Maximum = new decimal(new int[] {
			2000,
			0,
			0,
			0});
			this.N_P3_Rows.Minimum = new decimal(new int[] {
			1,
			0,
			0,
			0});
			this.N_P3_Rows.Name = "N_P3_Rows";
			this.N_P3_Rows.Size = new System.Drawing.Size(45, 20);
			this.N_P3_Rows.TabIndex = 5;
			this.N_P3_Rows.Value = new decimal(new int[] {
			1,
			0,
			0,
			0});
			// 
			// L_P3_Rows
			// 
			this.L_P3_Rows.AutoSize = true;
			this.L_P3_Rows.Location = new System.Drawing.Point(109, 8);
			this.L_P3_Rows.Margin = new System.Windows.Forms.Padding(3, 8, 0, 0);
			this.L_P3_Rows.Name = "L_P3_Rows";
			this.L_P3_Rows.Size = new System.Drawing.Size(43, 13);
			this.L_P3_Rows.TabIndex = 1;
			this.L_P3_Rows.Text = "Pozycji:";
			// 
			// FLP_P3_Page
			// 
			this.FLP_P3_Page.Controls.Add(this.N_P3_Page);
			this.FLP_P3_Page.Controls.Add(this.L_P3_Page);
			this.FLP_P3_Page.Dock = System.Windows.Forms.DockStyle.Fill;
			this.FLP_P3_Page.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
			this.FLP_P3_Page.Location = new System.Drawing.Point(359, 0);
			this.FLP_P3_Page.Margin = new System.Windows.Forms.Padding(0);
			this.FLP_P3_Page.Name = "FLP_P3_Page";
			this.FLP_P3_Page.Size = new System.Drawing.Size(105, 31);
			this.FLP_P3_Page.TabIndex = 12;
			// 
			// N_P3_Page
			// 
			this.N_P3_Page.Location = new System.Drawing.Point(67, 5);
			this.N_P3_Page.Margin = new System.Windows.Forms.Padding(3, 5, 3, 3);
			this.N_P3_Page.Maximum = new decimal(new int[] {
			1,
			0,
			0,
			0});
			this.N_P3_Page.Minimum = new decimal(new int[] {
			1,
			0,
			0,
			0});
			this.N_P3_Page.Name = "N_P3_Page";
			this.N_P3_Page.Size = new System.Drawing.Size(35, 20);
			this.N_P3_Page.TabIndex = 6;
			this.N_P3_Page.Value = new decimal(new int[] {
			1,
			0,
			0,
			0});
			this.N_P3_Page.ValueChanged += new System.EventHandler(this.N_P3_Page_ValueChanged);
			// 
			// L_P3_Page
			// 
			this.L_P3_Page.AutoSize = true;
			this.L_P3_Page.Location = new System.Drawing.Point(23, 8);
			this.L_P3_Page.Margin = new System.Windows.Forms.Padding(3, 8, 0, 0);
			this.L_P3_Page.Name = "L_P3_Page";
			this.L_P3_Page.Size = new System.Drawing.Size(41, 13);
			this.L_P3_Page.TabIndex = 1;
			this.L_P3_Page.Text = "Strona:";
			// 
			// CBX_P3_Scale
			// 
			this.CBX_P3_Scale.Dock = System.Windows.Forms.DockStyle.Fill;
			this.CBX_P3_Scale.FormattingEnabled = true;
			this.CBX_P3_Scale.Items.AddRange(new object[] {
			"50",
			"75",
			"100",
			"150",
			"200",
			"300"});
			this.CBX_P3_Scale.Location = new System.Drawing.Point(5, 4);
			this.CBX_P3_Scale.Margin = new System.Windows.Forms.Padding(5, 4, 3, 3);
			this.CBX_P3_Scale.Name = "CBX_P3_Scale";
			this.CBX_P3_Scale.Size = new System.Drawing.Size(72, 21);
			this.CBX_P3_Scale.TabIndex = 4;
			this.CBX_P3_Scale.SelectedIndexChanged += new System.EventHandler(this.CBX_P3_Scale_SelectedIndexChanged);
			this.CBX_P3_Scale.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CBX_P3_Scale_KeyDown);
			this.CBX_P3_Scale.Leave += new System.EventHandler(this.CBX_P3_Scale_Leave);
			// 
			// TLP_Pattern
			// 
			this.TLP_Pattern.ColumnCount = 1;
			this.TLP_Pattern.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.TLP_Pattern.Controls.Add(this.SC_P2_Pattern, 0, 0);
			this.TLP_Pattern.Controls.Add(this.TLP_P2_StatusBar, 0, 1);
			this.TLP_Pattern.Dock = System.Windows.Forms.DockStyle.Fill;
			this.TLP_Pattern.Location = new System.Drawing.Point(0, 0);
			this.TLP_Pattern.Margin = new System.Windows.Forms.Padding(0);
			this.TLP_Pattern.Name = "TLP_Pattern";
			this.TLP_Pattern.Padding = new System.Windows.Forms.Padding(0, 32, 0, 0);
			this.TLP_Pattern.RowCount = 2;
			this.TLP_Pattern.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.TLP_Pattern.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
			this.TLP_Pattern.Size = new System.Drawing.Size(724, 512);
			this.TLP_Pattern.TabIndex = 8;
			this.TLP_Pattern.Visible = false;
			// 
			// SC_P2_Pattern
			// 
			this.SC_P2_Pattern.Dock = System.Windows.Forms.DockStyle.Fill;
			this.SC_P2_Pattern.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
			this.SC_P2_Pattern.Location = new System.Drawing.Point(5, 37);
			this.SC_P2_Pattern.Margin = new System.Windows.Forms.Padding(5);
			this.SC_P2_Pattern.Name = "SC_P2_Pattern";
			// 
			// SC_P2_Pattern.Panel1
			// 
			this.SC_P2_Pattern.Panel1.Controls.Add(this.P_P2_Pattern);
			this.SC_P2_Pattern.Panel1MinSize = 425;
			// 
			// SC_P2_Pattern.Panel2
			// 
			this.SC_P2_Pattern.Panel2.Controls.Add(this.TLP_P2_Details);
			this.SC_P2_Pattern.Panel2MinSize = 250;
			this.SC_P2_Pattern.Size = new System.Drawing.Size(714, 438);
			this.SC_P2_Pattern.SplitterDistance = 450;
			this.SC_P2_Pattern.SplitterWidth = 5;
			this.SC_P2_Pattern.TabIndex = 0;
			// 
			// TLP_P2_StatusBar
			// 
			this.TLP_P2_StatusBar.BackColor = System.Drawing.SystemColors.ControlLight;
			this.TLP_P2_StatusBar.ColumnCount = 2;
			this.TLP_P2_StatusBar.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.TLP_P2_StatusBar.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 257F));
			this.TLP_P2_StatusBar.Controls.Add(this.TLP_P2_InfoControls, 0, 0);
			this.TLP_P2_StatusBar.Controls.Add(this.TLP_P2_Buttons, 1, 0);
			this.TLP_P2_StatusBar.Dock = System.Windows.Forms.DockStyle.Fill;
			this.TLP_P2_StatusBar.Location = new System.Drawing.Point(0, 480);
			this.TLP_P2_StatusBar.Margin = new System.Windows.Forms.Padding(0);
			this.TLP_P2_StatusBar.Name = "TLP_P2_StatusBar";
			this.TLP_P2_StatusBar.Padding = new System.Windows.Forms.Padding(0, 1, 0, 0);
			this.TLP_P2_StatusBar.RowCount = 1;
			this.TLP_P2_StatusBar.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.TLP_P2_StatusBar.Size = new System.Drawing.Size(724, 32);
			this.TLP_P2_StatusBar.TabIndex = 1;
			this.TLP_P2_StatusBar.Paint += new System.Windows.Forms.PaintEventHandler(this.TLP_P1_StatusBar_Paint);
			// 
			// TLP_P2_Buttons
			// 
			this.TLP_P2_Buttons.BackColor = System.Drawing.Color.Transparent;
			this.TLP_P2_Buttons.ColumnCount = 2;
			this.TLP_P2_Buttons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.TLP_P2_Buttons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.TLP_P2_Buttons.Controls.Add(this.B_P2_Save, 0, 0);
			this.TLP_P2_Buttons.Controls.Add(this.B_P2_LoadData, 0, 0);
			this.TLP_P2_Buttons.Dock = System.Windows.Forms.DockStyle.Fill;
			this.TLP_P2_Buttons.Location = new System.Drawing.Point(468, 1);
			this.TLP_P2_Buttons.Margin = new System.Windows.Forms.Padding(1, 0, 5, 0);
			this.TLP_P2_Buttons.Name = "TLP_P2_Buttons";
			this.TLP_P2_Buttons.RowCount = 1;
			this.TLP_P2_Buttons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.TLP_P2_Buttons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
			this.TLP_P2_Buttons.Size = new System.Drawing.Size(251, 31);
			this.TLP_P2_Buttons.TabIndex = 1;
			// 
			// TP_Tooltip
			// 
			this.TP_Tooltip.OwnerDraw = true;
			this.TP_Tooltip.Draw += new System.Windows.Forms.DrawToolTipEventHandler(this.TP_Tooltip_Draw);
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(724, 512);
			this.Controls.Add(this.MS_Main);
			this.Controls.Add(this.TLP_Main);
			this.Controls.Add(this.TLP_Generator);
			this.Controls.Add(this.TLP_Pattern);
			this.KeyPreview = true;
			this.MinimumSize = new System.Drawing.Size(740, 550);
			this.Name = "MainForm";
			this.Text = "CDesigner - Kreator Dokumentów";
			this.Move += new System.EventHandler(this.Main_Move);
			this.Resize += new System.EventHandler(this.Main_Resize);
			((System.ComponentModel.ISupportInitialize)(this.N_P1_Page)).EndInit();
			this.CMS_Pattern.ResumeLayout(false);
			this.MS_Main.ResumeLayout(false);
			this.MS_Main.PerformLayout();
			this.CMS_Page.ResumeLayout(false);
			this.CMS_Label.ResumeLayout(false);
			this.TLP_P3_Buttons.ResumeLayout(false);
			this.TLP_P2_InfoControls.ResumeLayout(false);
			this.TLP_P2_InfoControls.PerformLayout();
			this.FLP_P2_Page.ResumeLayout(false);
			this.FLP_P2_Page.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.N_P2_Page)).EndInit();
			this.TLP_P2_Details.ResumeLayout(false);
			this.P_P2_Menu.ResumeLayout(false);
			this.P_P2_Menu.PerformLayout();
			this.MS_P2_Menu.ResumeLayout(false);
			this.MS_P2_Menu.PerformLayout();
			this.P_P2_Editor.ResumeLayout(false);
			this.P_P2_Editor.PerformLayout();
			this.TLP_P2_LabelDetails.ResumeLayout(false);
			this.TLP_P2_LabelDetails.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.N_P2_MarginLR)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.N_P2_MarginTB)).EndInit();
			this.G_P2_Generator.ResumeLayout(false);
			this.TLP_P2_Generator.ResumeLayout(false);
			this.TLP_P2_Generator.PerformLayout();
			this.TLP_P2_PageDetails.ResumeLayout(false);
			this.TLP_P2_PageDetails.PerformLayout();
			this.G_P2_PageApperance.ResumeLayout(false);
			this.TLP_P2_PageAppearance.ResumeLayout(false);
			this.TLP_P2_PageAppearance.PerformLayout();
			this.G_P2_GeneratePDF.ResumeLayout(false);
			this.TLP_P2_GeneratePDF.ResumeLayout(false);
			this.TLP_P2_GeneratePDF.PerformLayout();
			this.TLP_P2_Field.ResumeLayout(false);
			this.TLP_P2_Field.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.N_P2_BorderSize)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.N_P2_Padding)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.N_P2_PosY)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.N_P2_PosX)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.N_P2_Width)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.N_P2_Height)).EndInit();
			this.G_P2_Appearance.ResumeLayout(false);
			this.TLP_P2_FieldAppearance.ResumeLayout(false);
			this.TLP_P2_FieldAppearance.PerformLayout();
			this.G_P2_Font.ResumeLayout(false);
			this.TLP_P2_FontSettings.ResumeLayout(false);
			this.TLP_P2_FontSettings.PerformLayout();
			this.SC_P3_Generator.Panel1.ResumeLayout(false);
			this.SC_P3_Generator.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.SC_P3_Generator)).EndInit();
			this.SC_P3_Generator.ResumeLayout(false);
			this.TLP_PageList.ResumeLayout(false);
			this.TLP_PageList.PerformLayout();
			this.SC_P1_Main.Panel1.ResumeLayout(false);
			this.SC_P1_Main.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.SC_P1_Main)).EndInit();
			this.SC_P1_Main.ResumeLayout(false);
			this.SC_P1_Details.Panel1.ResumeLayout(false);
			this.SC_P1_Details.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.SC_P1_Details)).EndInit();
			this.SC_P1_Details.ResumeLayout(false);
			this.TLP_Main.ResumeLayout(false);
			this.TLP_P1_StatusBar.ResumeLayout(false);
			this.TLP_P1_StatusBar.PerformLayout();
			this.TLP_Generator.ResumeLayout(false);
			this.TLP_P3_StatusBar.ResumeLayout(false);
			this.TLP_P3_InfoControls.ResumeLayout(false);
			this.FLP_P3_Rows.ResumeLayout(false);
			this.FLP_P3_Rows.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.N_P3_Rows)).EndInit();
			this.FLP_P3_Page.ResumeLayout(false);
			this.FLP_P3_Page.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.N_P3_Page)).EndInit();
			this.TLP_Pattern.ResumeLayout(false);
			this.SC_P2_Pattern.Panel1.ResumeLayout(false);
			this.SC_P2_Pattern.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.SC_P2_Pattern)).EndInit();
			this.SC_P2_Pattern.ResumeLayout(false);
			this.TLP_P2_StatusBar.ResumeLayout(false);
			this.TLP_P2_Buttons.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();
		}

#endregion

		private System.Windows.Forms.MenuStrip MS_Main;
		private System.Windows.Forms.ToolStripMenuItem TSMI_Pattern;
		private System.Windows.Forms.ToolStripMenuItem TSMI_MenuNewPattern;
		private System.Windows.Forms.ToolStripMenuItem TSMI_Import;
		private System.Windows.Forms.ToolStripMenuItem TSMI_PatternEditor;
		private System.Windows.Forms.ToolStripMenuItem TSMI_HomePage;
		private System.Windows.Forms.ToolStripMenuItem TSMI_PrintPreview;
		private System.Windows.Forms.ColorDialog CD_ColorDialog;
		private System.Windows.Forms.FontDialog FD_FontDialog;
		private System.Windows.Forms.ContextMenuStrip CMS_Page;
		private System.Windows.Forms.ToolStripMenuItem TSMI_PageImage;
		private System.Windows.Forms.ToolStripMenuItem TSMI_PageClear;
		private System.Windows.Forms.ToolStripSeparator TSMI_Separator_B1;
		private System.Windows.Forms.ToolStripMenuItem TSMI_AddPage;
		private System.Windows.Forms.ToolStripMenuItem TSMI_RemovePage;
		private System.Windows.Forms.ToolStripMenuItem TSMI_AddField;
		private System.Windows.Forms.ContextMenuStrip CMS_Label;
		private System.Windows.Forms.ToolStripMenuItem TSMI_BorderColor;
		private System.Windows.Forms.ToolStripMenuItem TSMI_DrawBorder;
		private System.Windows.Forms.ToolStripSeparator TSMI_Separator_C2;
		private System.Windows.Forms.ToolStripMenuItem TSMI_FontName;
		private System.Windows.Forms.ToolStripMenuItem TSMI_FontColor;
		private System.Windows.Forms.ToolStripMenuItem TSMI_DynamicText;
		private System.Windows.Forms.ToolStripSeparator TSMI_Separator_C3;
		private System.Windows.Forms.ToolStripMenuItem TSMI_FieldImage;
		private System.Windows.Forms.ToolStripMenuItem TSMI_DynamicImage;
		private System.Windows.Forms.ToolStripMenuItem TSMI_FieldClear;
		private System.Windows.Forms.ToolStripMenuItem TSMI_RemoveField;
		private System.Windows.Forms.ToolStripMenuItem TSMI_StaticText;
		private System.Windows.Forms.ToolStripMenuItem TSMI_StaticImage;
		private System.Windows.Forms.ToolStripMenuItem TSMI_FieldColor;
		private System.Windows.Forms.ToolStripMenuItem TSMI_PageColor;
		private System.Windows.Forms.ToolStripMenuItem TSMI_RemoveAll;
		private System.Windows.Forms.ToolStripMenuItem TSMI_PageDrawColor;
		private System.Windows.Forms.ToolStripMenuItem TSMI_PageDrawImage;
		private System.Windows.Forms.ToolStripSeparator TSMI_Separator_B2;
		private System.Windows.Forms.ToolStripSeparator TSMI_Separator_C0;
		private System.Windows.Forms.ToolStripMenuItem TSMI_DrawFieldFill;
		private System.Windows.Forms.ToolStripSeparator TSMI_Separator_C1;
		private System.Windows.Forms.ContextMenuStrip CMS_Pattern;
		private System.Windows.Forms.ToolStripMenuItem TSMI_EditPattern;
		private System.Windows.Forms.ToolStripMenuItem TSMI_RemovePattern;
		private System.Windows.Forms.ToolStripMenuItem TSMI_LoadData;
		private System.Windows.Forms.ToolStripMenuItem TSMI_NewPattern;
		private System.Windows.Forms.ToolStripSeparator TSMI_Separator_A1;
		private System.Windows.Forms.ToolStripSeparator TSMI_Separator_A2;
		private System.Windows.Forms.ToolStripMenuItem TSMI_ImportPattern;
		private System.Windows.Forms.ToolStripMenuItem TSMI_ExportPattern;
		private System.Windows.Forms.ToolStripSeparator TSMI_Separator_A3;
		private System.Windows.Forms.Button B_P1_Delete;
		private System.Windows.Forms.Button B_P1_New;
		private System.Windows.Forms.NumericUpDown N_P1_Page;
		private System.Windows.Forms.Label L_P1_Page;
		private System.Windows.Forms.Panel P_P3_Generator;
		private System.Windows.Forms.TreeView TV_P1_Patterns;
		private System.Windows.Forms.TableLayoutPanel TLP_P3_Buttons;
		private System.Windows.Forms.TreeView TV_P3_PageList;
		private System.Windows.Forms.Button B_P2_Save;
		private System.Windows.Forms.Button B_P2_LoadData;
		private System.Windows.Forms.TableLayoutPanel TLP_P2_InfoControls;
		private System.Windows.Forms.FlowLayoutPanel FLP_P2_Page;
		private System.Windows.Forms.NumericUpDown N_P2_Page;
		private System.Windows.Forms.Label L_P2_Page;
		private System.Windows.Forms.ComboBox CBX_P2_Scale;
		private System.Windows.Forms.Panel P_P2_Pattern;
		private System.Windows.Forms.Panel P_P1_Preview;
		private System.Windows.Forms.ToolStripSeparator TSMI_Separator_B0;
		private System.Windows.Forms.Button B_P3_GeneratePDF;
		private System.Windows.Forms.Button B_P3_SearchErrors;
		private System.Windows.Forms.TableLayoutPanel TLP_P2_Details;
		private System.Windows.Forms.MenuStrip MS_P2_Menu;
		private System.Windows.Forms.ToolStripMenuItem TSMI_FieldSwitch;
		private System.Windows.Forms.ToolStripMenuItem TSMI_PageSwitch;
		private System.Windows.Forms.Panel P_P2_Editor;
		private System.Windows.Forms.TableLayoutPanel TLP_P2_Field;
		private System.Windows.Forms.Label L_P2_Height;
		private System.Windows.Forms.NumericUpDown N_P2_BorderSize;
		private System.Windows.Forms.Label L_P2_TextPosition;
		private System.Windows.Forms.Button B_P2_FontName;
		private System.Windows.Forms.TextBox TB_P2_FontName;
		private System.Windows.Forms.Button B_P2_BackImage;
		private System.Windows.Forms.TextBox TB_P2_BackImage;
		private System.Windows.Forms.Button B_P2_BackColor;
		private System.Windows.Forms.TextBox TB_P2_BackColor;
		private System.Windows.Forms.Button B_P2_FontColor;
		private System.Windows.Forms.TextBox TB_P2_FontColor;
		private System.Windows.Forms.TextBox TB_P2_LabelName;
		private System.Windows.Forms.NumericUpDown N_P2_PosY;
		private System.Windows.Forms.NumericUpDown N_P2_PosX;
		private System.Windows.Forms.Label L_P2_PosX;
		private System.Windows.Forms.Label L_P2_Width;
		private System.Windows.Forms.NumericUpDown N_P2_Width;
		private System.Windows.Forms.NumericUpDown N_P2_Height;
		private System.Windows.Forms.TextBox TB_P2_BorderColor;
		private System.Windows.Forms.Button B_P2_BorderColor;
		private System.Windows.Forms.Label L_P2_BorderWidth;
		private System.Windows.Forms.ComboBox CBX_P2_TextPosition;
		private System.Windows.Forms.ComboBox CBX_P2_StickPoint;
		private System.Windows.Forms.Label L_P2_StickPoint;
		private System.Windows.Forms.Label L_P2_PosY;
		private System.Windows.Forms.ToolStripMenuItem TSMI_DetailSwitch;
		private System.Windows.Forms.TableLayoutPanel TLP_P2_LabelDetails;
		private System.Windows.Forms.TableLayoutPanel TLP_P2_PageDetails;
		private System.Windows.Forms.CheckBox CB_P2_DynamicImage;
		private System.Windows.Forms.CheckBox CB_P2_StaticText;
		private System.Windows.Forms.CheckBox CB_P2_DrawColor;
		private System.Windows.Forms.CheckBox CB_P2_DynamicText;
		private System.Windows.Forms.CheckBox CB_P2_DrawBorder;
		private System.Windows.Forms.CheckBox CB_P2_StaticImage;
		private System.Windows.Forms.Label L_P2_ImageSettings;
		private System.Windows.Forms.CheckBox CB_P2_UseImageMargin;
		private System.Windows.Forms.CheckBox CB_P2_DrawFrameOutside;
		private System.Windows.Forms.ComboBox CBX_P2_ImageSettings;
		private System.Windows.Forms.TextBox TB_P2_PageWidth;
		private System.Windows.Forms.TextBox TB_P2_PageHeight;
		private System.Windows.Forms.Label L_P2_PageWidth;
		private System.Windows.Forms.Label L_P2_PageHeight;
		private System.Windows.Forms.CheckBox CB_P2_DrawPageColor;
		private System.Windows.Forms.CheckBox CB_P2_DrawPageImage;
		private System.Windows.Forms.CheckBox CB_P2_DrawOutside;
		private System.Windows.Forms.CheckBox CB_P2_ApplyMargin;
		private System.Windows.Forms.ComboBox CBX_P2_PageImageSettings;
		private System.Windows.Forms.Label L_P2_PageImageSet;
		private System.Windows.Forms.SplitContainer SC_P3_Generator;
		private System.Windows.Forms.SplitContainer SC_P1_Main;
		private System.Windows.Forms.TableLayoutPanel TLP_Main;
		private System.Windows.Forms.TableLayoutPanel TLP_P1_StatusBar;
		private System.Windows.Forms.TableLayoutPanel TLP_Generator;
		private System.Windows.Forms.TableLayoutPanel TLP_P3_StatusBar;
		private System.Windows.Forms.TableLayoutPanel TLP_Pattern;
		private System.Windows.Forms.SplitContainer SC_P2_Pattern;
		private System.Windows.Forms.TableLayoutPanel TLP_P2_StatusBar;
		private System.Windows.Forms.TableLayoutPanel TLP_P2_Buttons;
		private System.Windows.Forms.TableLayoutPanel TLP_P3_InfoControls;
		private System.Windows.Forms.FlowLayoutPanel FLP_P3_Page;
		private System.Windows.Forms.NumericUpDown N_P3_Page;
		private System.Windows.Forms.Label L_P3_Page;
		private System.Windows.Forms.ComboBox CBX_P3_Scale;
		private System.Windows.Forms.ToolStripMenuItem TSMI_RecentOpen;
		private System.Windows.Forms.ToolStripSeparator TSS_Separator_P2;
		private System.Windows.Forms.ToolStripMenuItem TSMI_ExportAll;
		private System.Windows.Forms.ToolStripSeparator TSS_Separator_P3;
		private System.Windows.Forms.ToolStripMenuItem TSMI_Close;
		private System.Windows.Forms.ToolStripMenuItem TSMI_Settings;
		private System.Windows.Forms.ToolStripMenuItem TSMI_Generator;
		private System.Windows.Forms.ToolStripMenuItem TSMI_Editor;
		private System.Windows.Forms.ToolStripMenuItem TSMI_General;
		private System.Windows.Forms.ToolStripMenuItem TSMI_Program;
		private System.Windows.Forms.ToolStripMenuItem TSMI_Info;
		private System.Windows.Forms.ToolStripMenuItem TSMI_Help;
		private System.Windows.Forms.Button B_P2_PageColor;
		private System.Windows.Forms.TextBox TB_P2_PageColor;
		private System.Windows.Forms.TextBox TB_P2_PageImage;
		private System.Windows.Forms.Button B_P2_PageImage;
		private System.Windows.Forms.ComboBox CBX_P2_TextTransform;
		private System.Windows.Forms.Label L_P2_TextTransform;
		private System.Windows.Forms.ToolStripMenuItem TSMI_Tools;
		private System.Windows.Forms.ToolStripMenuItem TSMI_EditColumns;
		private System.Windows.Forms.Label L_P2_AddMargin;
		private System.Windows.Forms.NumericUpDown N_P2_Padding;
		private System.Windows.Forms.Label L_P2_Padding;
		private System.Windows.Forms.CheckBox CB_P2_AdditionalMargin;
		private System.Windows.Forms.NumericUpDown N_P2_MarginLR;
		private System.Windows.Forms.NumericUpDown N_P2_MarginTB;
		private System.Windows.Forms.ToolStripMenuItem TSMI_ConnectDatabase;
		private System.Windows.Forms.ToolStripMenuItem TSMI_CreateDatabase;
		private System.Windows.Forms.ToolStripSeparator TSS_Separator_P1;
		private System.Windows.Forms.ToolStripMenuItem TSMI_ClearRecent;
		private System.Windows.Forms.ToolStripMenuItem TSMI_DataBackup;
		private System.Windows.Forms.ToolStripMenuItem TSMI_Update;
		private System.Windows.Forms.ToolStripMenuItem TSMI_LoadDataFile;
		private System.Windows.Forms.ToolStripSeparator TSS_Separator_T1;
		private System.Windows.Forms.ToolStripMenuItem TSMI_EditRows;
		private System.Windows.Forms.ToolStripMenuItem TSMI_CreateEmpty;
		private System.Windows.Forms.ToolStripSeparator TSS_Separator_T2;
		private System.Windows.Forms.ToolStripMenuItem TSMI_SaveData;
		private System.ComponentModel.BackgroundWorker BW_Updates;
		private System.Windows.Forms.Panel P_P2_Menu;
		private System.Windows.Forms.CheckBox CB_P1_ShowDetails;
		private System.Windows.Forms.SplitContainer SC_P1_Details;
		private System.Windows.Forms.RichTextBox RTB_P1_Details;
		private System.Windows.Forms.ToolStripMenuItem TSMI_Language;
		private System.Windows.Forms.CheckBox CB_P2_AutoSave;
		private System.Windows.Forms.GroupBox G_P2_Appearance;
		private System.Windows.Forms.TableLayoutPanel TLP_P2_FieldAppearance;
		private System.Windows.Forms.GroupBox G_P2_Font;
		private System.Windows.Forms.TableLayoutPanel TLP_P2_FontSettings;
		private System.Windows.Forms.GroupBox G_P2_Generator;
		private System.Windows.Forms.TableLayoutPanel TLP_P2_Generator;
		private System.Windows.Forms.GroupBox G_P2_PageApperance;
		private System.Windows.Forms.TableLayoutPanel TLP_P2_PageAppearance;
		private System.Windows.Forms.GroupBox G_P2_GeneratePDF;
		private System.Windows.Forms.TableLayoutPanel TLP_P2_GeneratePDF;
		private System.Windows.Forms.ToolTip TP_Tooltip;
		private System.Windows.Forms.ToolStripMenuItem TSMI_CloseData;
		private System.Windows.Forms.FlowLayoutPanel FLP_P3_Rows;
		private System.Windows.Forms.NumericUpDown N_P3_Rows;
		private System.Windows.Forms.Label L_P3_Rows;
		private System.Windows.Forms.CheckBox CB_P3_CollatePages;
		private System.Windows.Forms.TableLayoutPanel TLP_PageList;

	/// @endcond
	}
}

