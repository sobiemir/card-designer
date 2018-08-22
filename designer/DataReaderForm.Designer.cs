namespace CDesigner.Forms
{
	partial class DataReaderForm
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
			this.TLP_StatusBar = new System.Windows.Forms.TableLayoutPanel();
			this.TB_Format = new System.Windows.Forms.TextBox();
			this.F_PageContainer = new System.Windows.Forms.FlowLayoutPanel();
			this.N_Page = new System.Windows.Forms.NumericUpDown();
			this.L_Page = new System.Windows.Forms.Label();
			this.TLP_Buttons = new System.Windows.Forms.TableLayoutPanel();
			this.B_Cancel = new System.Windows.Forms.Button();
			this.B_Save = new System.Windows.Forms.Button();
			this.LV_PageFields = new System.Windows.Forms.ListView();
			this.CH_PageFields = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.CH_PageCols = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.LV_DatabaseCols = new System.Windows.Forms.ListView();
			this.CH_DatabaseCols = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.SC_Main = new System.Windows.Forms.SplitContainer();
			this.TLP_Main = new System.Windows.Forms.TableLayoutPanel();
			this.TP_Tooltip = new System.Windows.Forms.ToolTip(this.components);
			this.TLP_StatusBar.SuspendLayout();
			this.F_PageContainer.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.N_Page)).BeginInit();
			this.TLP_Buttons.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.SC_Main)).BeginInit();
			this.SC_Main.Panel1.SuspendLayout();
			this.SC_Main.Panel2.SuspendLayout();
			this.SC_Main.SuspendLayout();
			this.TLP_Main.SuspendLayout();
			this.SuspendLayout();
			// 
			// TLP_StatusBar
			// 
			this.TLP_StatusBar.BackColor = System.Drawing.SystemColors.ControlLight;
			this.TLP_StatusBar.ColumnCount = 3;
			this.TLP_StatusBar.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
			this.TLP_StatusBar.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.TLP_StatusBar.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 208F));
			this.TLP_StatusBar.Controls.Add(this.TB_Format, 0, 0);
			this.TLP_StatusBar.Controls.Add(this.F_PageContainer, 1, 0);
			this.TLP_StatusBar.Controls.Add(this.TLP_Buttons, 2, 0);
			this.TLP_StatusBar.Dock = System.Windows.Forms.DockStyle.Fill;
			this.TLP_StatusBar.Location = new System.Drawing.Point(0, 320);
			this.TLP_StatusBar.Margin = new System.Windows.Forms.Padding(0);
			this.TLP_StatusBar.Name = "TLP_StatusBar";
			this.TLP_StatusBar.Padding = new System.Windows.Forms.Padding(0, 1, 0, 0);
			this.TLP_StatusBar.RowCount = 1;
			this.TLP_StatusBar.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.TLP_StatusBar.Size = new System.Drawing.Size(574, 32);
			this.TLP_StatusBar.TabIndex = 0;
			this.TLP_StatusBar.Paint += new System.Windows.Forms.PaintEventHandler(this.TLP_StatusBar_Paint);
			// 
			// TB_Format
			// 
			this.TB_Format.Dock = System.Windows.Forms.DockStyle.Fill;
			this.TB_Format.Location = new System.Drawing.Point(5, 6);
			this.TB_Format.Margin = new System.Windows.Forms.Padding(5, 5, 3, 3);
			this.TB_Format.MaxLength = 127;
			this.TB_Format.Name = "TB_Format";
			this.TB_Format.Size = new System.Drawing.Size(112, 20);
			this.TB_Format.TabIndex = 0;
			this.TB_Format.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TB_Format_KeyPress);
			this.TB_Format.Leave += new System.EventHandler(this.TB_Format_Leave);
			// 
			// F_PageContainer
			// 
			this.F_PageContainer.Controls.Add(this.N_Page);
			this.F_PageContainer.Controls.Add(this.L_Page);
			this.F_PageContainer.Dock = System.Windows.Forms.DockStyle.Fill;
			this.F_PageContainer.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
			this.F_PageContainer.Location = new System.Drawing.Point(123, 1);
			this.F_PageContainer.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
			this.F_PageContainer.Name = "F_PageContainer";
			this.F_PageContainer.Size = new System.Drawing.Size(243, 31);
			this.F_PageContainer.TabIndex = 4;
			// 
			// N_Page
			// 
			this.N_Page.Location = new System.Drawing.Point(205, 6);
			this.N_Page.Margin = new System.Windows.Forms.Padding(3, 6, 3, 3);
			this.N_Page.Maximum = new decimal(new int[] {
			1,
			0,
			0,
			0});
			this.N_Page.Minimum = new decimal(new int[] {
			1,
			0,
			0,
			0});
			this.N_Page.Name = "N_Page";
			this.N_Page.Size = new System.Drawing.Size(35, 20);
			this.N_Page.TabIndex = 1;
			this.N_Page.Value = new decimal(new int[] {
			1,
			0,
			0,
			0});
			this.N_Page.ValueChanged += new System.EventHandler(this.N_Page_ValueChanged);
			// 
			// L_Page
			// 
			this.L_Page.AutoSize = true;
			this.L_Page.Dock = System.Windows.Forms.DockStyle.Left;
			this.L_Page.Location = new System.Drawing.Point(158, 0);
			this.L_Page.Name = "L_Page";
			this.L_Page.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.L_Page.Size = new System.Drawing.Size(41, 29);
			this.L_Page.TabIndex = 0;
			this.L_Page.Text = "Strona:";
			this.L_Page.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// TLP_Buttons
			// 
			this.TLP_Buttons.ColumnCount = 2;
			this.TLP_Buttons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.TLP_Buttons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.TLP_Buttons.Controls.Add(this.B_Cancel, 0, 0);
			this.TLP_Buttons.Controls.Add(this.B_Save, 0, 0);
			this.TLP_Buttons.Dock = System.Windows.Forms.DockStyle.Fill;
			this.TLP_Buttons.Location = new System.Drawing.Point(366, 1);
			this.TLP_Buttons.Margin = new System.Windows.Forms.Padding(0);
			this.TLP_Buttons.Name = "TLP_Buttons";
			this.TLP_Buttons.RowCount = 1;
			this.TLP_Buttons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.TLP_Buttons.Size = new System.Drawing.Size(208, 31);
			this.TLP_Buttons.TabIndex = 11;
			// 
			// B_Cancel
			// 
			this.B_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.B_Cancel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.B_Cancel.Location = new System.Drawing.Point(106, 3);
			this.B_Cancel.Margin = new System.Windows.Forms.Padding(2, 3, 5, 3);
			this.B_Cancel.Name = "B_Cancel";
			this.B_Cancel.Size = new System.Drawing.Size(97, 25);
			this.B_Cancel.TabIndex = 7;
			this.B_Cancel.Text = "Zaniechaj";
			this.B_Cancel.UseVisualStyleBackColor = true;
			// 
			// B_Save
			// 
			this.B_Save.Dock = System.Windows.Forms.DockStyle.Fill;
			this.B_Save.Location = new System.Drawing.Point(3, 3);
			this.B_Save.Margin = new System.Windows.Forms.Padding(3, 3, 2, 3);
			this.B_Save.Name = "B_Save";
			this.B_Save.Size = new System.Drawing.Size(99, 25);
			this.B_Save.TabIndex = 6;
			this.B_Save.Text = "Zapisz";
			this.B_Save.UseVisualStyleBackColor = true;
			this.B_Save.Click += new System.EventHandler(this.B_Save_Click);
			// 
			// LV_PageFields
			// 
			this.LV_PageFields.AllowDrop = true;
			this.LV_PageFields.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
			this.CH_PageFields,
			this.CH_PageCols});
			this.LV_PageFields.Dock = System.Windows.Forms.DockStyle.Fill;
			this.LV_PageFields.FullRowSelect = true;
			this.LV_PageFields.GridLines = true;
			this.LV_PageFields.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
			this.LV_PageFields.HideSelection = false;
			this.LV_PageFields.Location = new System.Drawing.Point(0, 0);
			this.LV_PageFields.Margin = new System.Windows.Forms.Padding(6, 6, 3, 3);
			this.LV_PageFields.MultiSelect = false;
			this.LV_PageFields.Name = "LV_PageFields";
			this.LV_PageFields.Size = new System.Drawing.Size(357, 310);
			this.LV_PageFields.TabIndex = 8;
			this.LV_PageFields.UseCompatibleStateImageBehavior = false;
			this.LV_PageFields.View = System.Windows.Forms.View.Details;
			this.LV_PageFields.DragDrop += new System.Windows.Forms.DragEventHandler(this.LV_PageFields_DragDrop);
			this.LV_PageFields.DragEnter += new System.Windows.Forms.DragEventHandler(this.LV_PageFields_DragEnter);
			this.LV_PageFields.DragOver += new System.Windows.Forms.DragEventHandler(this.LV_PageFields_DragOver);
			this.LV_PageFields.KeyDown += new System.Windows.Forms.KeyEventHandler(this.LV_PageFields_KeyDown);
			// 
			// CH_PageFields
			// 
			this.CH_PageFields.Text = "Dostępne pola na stronie";
			this.CH_PageFields.Width = 171;
			// 
			// CH_PageCols
			// 
			this.CH_PageCols.Text = "Przyporządkowane kolumny";
			this.CH_PageCols.Width = 184;
			// 
			// LV_DatabaseCols
			// 
			this.LV_DatabaseCols.CheckBoxes = true;
			this.LV_DatabaseCols.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
			this.CH_DatabaseCols});
			this.LV_DatabaseCols.Dock = System.Windows.Forms.DockStyle.Fill;
			this.LV_DatabaseCols.FullRowSelect = true;
			this.LV_DatabaseCols.GridLines = true;
			this.LV_DatabaseCols.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
			this.LV_DatabaseCols.HideSelection = false;
			this.LV_DatabaseCols.Location = new System.Drawing.Point(0, 0);
			this.LV_DatabaseCols.Margin = new System.Windows.Forms.Padding(3, 6, 6, 3);
			this.LV_DatabaseCols.MultiSelect = false;
			this.LV_DatabaseCols.Name = "LV_DatabaseCols";
			this.LV_DatabaseCols.ShowGroups = false;
			this.LV_DatabaseCols.Size = new System.Drawing.Size(202, 310);
			this.LV_DatabaseCols.TabIndex = 9;
			this.LV_DatabaseCols.UseCompatibleStateImageBehavior = false;
			this.LV_DatabaseCols.View = System.Windows.Forms.View.Details;
			this.LV_DatabaseCols.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.LV_DatabaseCols_ItemChecked);
			this.LV_DatabaseCols.DragOver += new System.Windows.Forms.DragEventHandler(this.LV_DatabaseCols_DragOver);
			this.LV_DatabaseCols.MouseDown += new System.Windows.Forms.MouseEventHandler(this.LV_DatabaseCols_MouseDown);
			// 
			// CH_DatabaseCols
			// 
			this.CH_DatabaseCols.Text = "Dostępne kolumny";
			this.CH_DatabaseCols.Width = 196;
			// 
			// SC_Main
			// 
			this.SC_Main.Dock = System.Windows.Forms.DockStyle.Fill;
			this.SC_Main.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
			this.SC_Main.Location = new System.Drawing.Point(5, 5);
			this.SC_Main.Margin = new System.Windows.Forms.Padding(5);
			this.SC_Main.Name = "SC_Main";
			// 
			// SC_Main.Panel1
			// 
			this.SC_Main.Panel1.Controls.Add(this.LV_PageFields);
			this.SC_Main.Panel1MinSize = 250;
			// 
			// SC_Main.Panel2
			// 
			this.SC_Main.Panel2.Controls.Add(this.LV_DatabaseCols);
			this.SC_Main.Panel2MinSize = 200;
			this.SC_Main.Size = new System.Drawing.Size(564, 310);
			this.SC_Main.SplitterDistance = 357;
			this.SC_Main.SplitterWidth = 5;
			this.SC_Main.TabIndex = 1;
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
			this.TLP_Main.Size = new System.Drawing.Size(574, 352);
			this.TLP_Main.TabIndex = 10;
			// 
			// TP_Tooltip
			// 
			this.TP_Tooltip.OwnerDraw = true;
			this.TP_Tooltip.Draw += new System.Windows.Forms.DrawToolTipEventHandler(this.TP_Tooltip_Draw);
			// 
			// DataReaderForm
			// 
			this.AcceptButton = this.B_Save;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.B_Cancel;
			this.ClientSize = new System.Drawing.Size(574, 352);
			this.Controls.Add(this.TLP_Main);
			this.KeyPreview = true;
			this.Name = "DataReaderForm";
			this.Text = "Wybór kolumn";
			this.Move += new System.EventHandler(this.DataReader_Move);
			this.TLP_StatusBar.ResumeLayout(false);
			this.TLP_StatusBar.PerformLayout();
			this.F_PageContainer.ResumeLayout(false);
			this.F_PageContainer.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.N_Page)).EndInit();
			this.TLP_Buttons.ResumeLayout(false);
			this.SC_Main.Panel1.ResumeLayout(false);
			this.SC_Main.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.SC_Main)).EndInit();
			this.SC_Main.ResumeLayout(false);
			this.TLP_Main.ResumeLayout(false);
			this.ResumeLayout(false);
		}

#endregion

		private System.Windows.Forms.TableLayoutPanel TLP_StatusBar;
		private System.Windows.Forms.FlowLayoutPanel F_PageContainer;
		private System.Windows.Forms.Label L_Page;
		private System.Windows.Forms.NumericUpDown N_Page;
		private System.Windows.Forms.ListView LV_PageFields;
		private System.Windows.Forms.ColumnHeader CH_PageFields;
		private System.Windows.Forms.ColumnHeader CH_PageCols;
		private System.Windows.Forms.ListView LV_DatabaseCols;
		private System.Windows.Forms.ColumnHeader CH_DatabaseCols;
		private System.Windows.Forms.TableLayoutPanel TLP_Buttons;
		private System.Windows.Forms.Button B_Save;
		private System.Windows.Forms.TextBox TB_Format;
		private System.Windows.Forms.SplitContainer SC_Main;
		private System.Windows.Forms.TableLayoutPanel TLP_Main;
		private System.Windows.Forms.Button B_Cancel;
		private System.Windows.Forms.ToolTip TP_Tooltip;

	/// @endcond
	}
}