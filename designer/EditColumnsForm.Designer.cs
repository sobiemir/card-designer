namespace CDesigner
{
	partial class EditColumnsForm
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
			this.tpTooltip = new System.Windows.Forms.ToolTip(this.components);
			this.lvPreviewRows = new System.Windows.Forms.ListView();
			this.lvcDataPreview = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.tlStatusBar = new System.Windows.Forms.TableLayoutPanel();
			this.bFilterData = new System.Windows.Forms.Button();
			this.bSave = new System.Windows.Forms.Button();
			this.bEditData = new System.Windows.Forms.Button();
			this.lvDatabaseColumns = new System.Windows.Forms.ListView();
			this.lvcColumns = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.lvNewColumns = new System.Windows.Forms.ListView();
			this.lvcColumnName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.lvcJoinedColumns = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
			this.bAddColumn = new System.Windows.Forms.Button();
			this.bClearColumn = new System.Windows.Forms.Button();
			this.bDeleteColumn = new System.Windows.Forms.Button();
			this.tbColumnName = new System.Windows.Forms.TextBox();
			this.tMainPanel = new System.Windows.Forms.TableLayoutPanel();
			this.tlStatusBar.SuspendLayout();
			this.tableLayoutPanel3.SuspendLayout();
			this.tMainPanel.SuspendLayout();
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
			this.lvPreviewRows.Location = new System.Drawing.Point(6, 267);
			this.lvPreviewRows.Margin = new System.Windows.Forms.Padding(6, 3, 3, 6);
			this.lvPreviewRows.MultiSelect = false;
			this.lvPreviewRows.Name = "lvPreviewRows";
			this.lvPreviewRows.ShowGroups = false;
			this.lvPreviewRows.Size = new System.Drawing.Size(442, 147);
			this.lvPreviewRows.TabIndex = 7;
			this.lvPreviewRows.UseCompatibleStateImageBehavior = false;
			this.lvPreviewRows.View = System.Windows.Forms.View.Details;
			// 
			// lvcDataPreview
			// 
			this.lvcDataPreview.Text = "Podgląd wierszy";
			this.lvcDataPreview.Width = 438;
			// 
			// tlStatusBar
			// 
			this.tlStatusBar.BackColor = System.Drawing.SystemColors.ControlLight;
			this.tlStatusBar.ColumnCount = 4;
			this.tMainPanel.SetColumnSpan(this.tlStatusBar, 2);
			this.tlStatusBar.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 17F));
			this.tlStatusBar.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 49F));
			this.tlStatusBar.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 17F));
			this.tlStatusBar.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 17F));
			this.tlStatusBar.Controls.Add(this.bFilterData, 2, 0);
			this.tlStatusBar.Controls.Add(this.bSave, 3, 0);
			this.tlStatusBar.Controls.Add(this.bEditData, 0, 0);
			this.tlStatusBar.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tlStatusBar.Location = new System.Drawing.Point(0, 420);
			this.tlStatusBar.Margin = new System.Windows.Forms.Padding(0);
			this.tlStatusBar.Name = "tlStatusBar";
			this.tlStatusBar.RowCount = 1;
			this.tlStatusBar.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tlStatusBar.Size = new System.Drawing.Size(684, 31);
			this.tlStatusBar.TabIndex = 11;
			this.tlStatusBar.Paint += new System.Windows.Forms.PaintEventHandler(this.tlStatusBar_Paint);
			// 
			// bFilterData
			// 
			this.bFilterData.Dock = System.Windows.Forms.DockStyle.Fill;
			this.bFilterData.Location = new System.Drawing.Point(454, 4);
			this.bFilterData.Margin = new System.Windows.Forms.Padding(3, 4, 2, 3);
			this.bFilterData.Name = "bFilterData";
			this.bFilterData.Size = new System.Drawing.Size(111, 24);
			this.bFilterData.TabIndex = 0;
			this.bFilterData.Text = "Filtruj dane";
			this.bFilterData.UseVisualStyleBackColor = true;
			this.bFilterData.Click += new System.EventHandler(this.bFilterData_Click);
			// 
			// bSave
			// 
			this.bSave.Dock = System.Windows.Forms.DockStyle.Fill;
			this.bSave.Location = new System.Drawing.Point(569, 4);
			this.bSave.Margin = new System.Windows.Forms.Padding(2, 4, 6, 3);
			this.bSave.Name = "bSave";
			this.bSave.Size = new System.Drawing.Size(109, 24);
			this.bSave.TabIndex = 1;
			this.bSave.Text = "Zapisz";
			this.bSave.UseVisualStyleBackColor = true;
			// 
			// bEditData
			// 
			this.bEditData.Dock = System.Windows.Forms.DockStyle.Fill;
			this.bEditData.Location = new System.Drawing.Point(6, 4);
			this.bEditData.Margin = new System.Windows.Forms.Padding(6, 4, 3, 3);
			this.bEditData.Name = "bEditData";
			this.bEditData.Size = new System.Drawing.Size(107, 24);
			this.bEditData.TabIndex = 2;
			this.bEditData.Text = "Edytuj wiersze";
			this.bEditData.UseVisualStyleBackColor = true;
			this.bEditData.Click += new System.EventHandler(this.bEditData_Click);
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
			this.lvDatabaseColumns.Location = new System.Drawing.Point(454, 6);
			this.lvDatabaseColumns.Margin = new System.Windows.Forms.Padding(3, 6, 6, 6);
			this.lvDatabaseColumns.MultiSelect = false;
			this.lvDatabaseColumns.Name = "lvDatabaseColumns";
			this.tMainPanel.SetRowSpan(this.lvDatabaseColumns, 3);
			this.lvDatabaseColumns.ShowGroups = false;
			this.lvDatabaseColumns.Size = new System.Drawing.Size(224, 408);
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
			this.lvcColumns.Width = 220;
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
			this.lvNewColumns.Location = new System.Drawing.Point(6, 6);
			this.lvNewColumns.Margin = new System.Windows.Forms.Padding(6, 6, 3, 3);
			this.lvNewColumns.MultiSelect = false;
			this.lvNewColumns.Name = "lvNewColumns";
			this.lvNewColumns.ShowGroups = false;
			this.lvNewColumns.Size = new System.Drawing.Size(442, 225);
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
			this.lvcColumnName.Width = 104;
			// 
			// lvcJoinedColumns
			// 
			this.lvcJoinedColumns.Text = "Kolumny";
			this.lvcJoinedColumns.Width = 334;
			// 
			// tableLayoutPanel3
			// 
			this.tableLayoutPanel3.BackColor = System.Drawing.SystemColors.Control;
			this.tableLayoutPanel3.ColumnCount = 4;
			this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
			this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
			this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
			this.tableLayoutPanel3.Controls.Add(this.bAddColumn, 1, 0);
			this.tableLayoutPanel3.Controls.Add(this.bClearColumn, 2, 0);
			this.tableLayoutPanel3.Controls.Add(this.bDeleteColumn, 3, 0);
			this.tableLayoutPanel3.Controls.Add(this.tbColumnName, 0, 0);
			this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel3.Location = new System.Drawing.Point(4, 234);
			this.tableLayoutPanel3.Margin = new System.Windows.Forms.Padding(4, 0, 0, 0);
			this.tableLayoutPanel3.Name = "tableLayoutPanel3";
			this.tableLayoutPanel3.RowCount = 1;
			this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
			this.tableLayoutPanel3.Size = new System.Drawing.Size(447, 30);
			this.tableLayoutPanel3.TabIndex = 18;
			// 
			// bAddColumn
			// 
			this.bAddColumn.Dock = System.Windows.Forms.DockStyle.Fill;
			this.bAddColumn.Location = new System.Drawing.Point(210, 3);
			this.bAddColumn.Name = "bAddColumn";
			this.bAddColumn.Size = new System.Drawing.Size(74, 24);
			this.bAddColumn.TabIndex = 4;
			this.bAddColumn.Text = "Dodaj";
			this.bAddColumn.UseVisualStyleBackColor = true;
			this.bAddColumn.Click += new System.EventHandler(this.bAddColumn_Click);
			// 
			// bClearColumn
			// 
			this.bClearColumn.Dock = System.Windows.Forms.DockStyle.Fill;
			this.bClearColumn.Enabled = false;
			this.bClearColumn.Location = new System.Drawing.Point(290, 3);
			this.bClearColumn.Name = "bClearColumn";
			this.bClearColumn.Size = new System.Drawing.Size(74, 24);
			this.bClearColumn.TabIndex = 5;
			this.bClearColumn.Text = "Wyczyść";
			this.bClearColumn.UseVisualStyleBackColor = true;
			this.bClearColumn.Click += new System.EventHandler(this.bClearColumn_Click);
			// 
			// bDeleteColumn
			// 
			this.bDeleteColumn.Dock = System.Windows.Forms.DockStyle.Fill;
			this.bDeleteColumn.Enabled = false;
			this.bDeleteColumn.Location = new System.Drawing.Point(370, 3);
			this.bDeleteColumn.Name = "bDeleteColumn";
			this.bDeleteColumn.Size = new System.Drawing.Size(74, 24);
			this.bDeleteColumn.TabIndex = 6;
			this.bDeleteColumn.Text = "Usuń";
			this.bDeleteColumn.UseVisualStyleBackColor = true;
			this.bDeleteColumn.Click += new System.EventHandler(this.bDeleteColumn_Click);
			// 
			// tbColumnName
			// 
			this.tbColumnName.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tbColumnName.Location = new System.Drawing.Point(3, 5);
			this.tbColumnName.Margin = new System.Windows.Forms.Padding(3, 5, 3, 3);
			this.tbColumnName.MaxLength = 127;
			this.tbColumnName.Name = "tbColumnName";
			this.tbColumnName.Size = new System.Drawing.Size(201, 20);
			this.tbColumnName.TabIndex = 3;
			this.tbColumnName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbColumnName_KeyPress);
			this.tbColumnName.Leave += new System.EventHandler(this.toolTip_Hide);
			// 
			// tMainPanel
			// 
			this.tMainPanel.ColumnCount = 2;
			this.tMainPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 66F));
			this.tMainPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 34F));
			this.tMainPanel.Controls.Add(this.tableLayoutPanel3, 0, 1);
			this.tMainPanel.Controls.Add(this.lvNewColumns, 0, 0);
			this.tMainPanel.Controls.Add(this.lvDatabaseColumns, 1, 0);
			this.tMainPanel.Controls.Add(this.tlStatusBar, 0, 3);
			this.tMainPanel.Controls.Add(this.lvPreviewRows, 0, 2);
			this.tMainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tMainPanel.Location = new System.Drawing.Point(0, 0);
			this.tMainPanel.Margin = new System.Windows.Forms.Padding(0);
			this.tMainPanel.Name = "tMainPanel";
			this.tMainPanel.RowCount = 4;
			this.tMainPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 60F));
			this.tMainPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
			this.tMainPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 40F));
			this.tMainPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
			this.tMainPanel.Size = new System.Drawing.Size(684, 451);
			this.tMainPanel.TabIndex = 1;
			// 
			// EditColumnsForm
			// 
			this.AcceptButton = this.bSave;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(684, 451);
			this.Controls.Add(this.tMainPanel);
			this.MaximizeBox = false;
			this.MaximumSize = new System.Drawing.Size(900, 65535);
			this.MinimumSize = new System.Drawing.Size(700, 480);
			this.Name = "EditColumnsForm";
			this.Text = "Zarządzanie kolumnami";
			this.Deactivate += new System.EventHandler(this.toolTip_Hide);
			this.Move += new System.EventHandler(this.toolTip_Hide);
			this.tlStatusBar.ResumeLayout(false);
			this.tableLayoutPanel3.ResumeLayout(false);
			this.tableLayoutPanel3.PerformLayout();
			this.tMainPanel.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.ToolTip tpTooltip;
		private System.Windows.Forms.ListView lvPreviewRows;
		private System.Windows.Forms.ColumnHeader lvcDataPreview;
		private System.Windows.Forms.TableLayoutPanel tlStatusBar;
		private System.Windows.Forms.TableLayoutPanel tMainPanel;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
		private System.Windows.Forms.Button bAddColumn;
		private System.Windows.Forms.Button bClearColumn;
		private System.Windows.Forms.Button bDeleteColumn;
		private System.Windows.Forms.TextBox tbColumnName;
		private System.Windows.Forms.ListView lvNewColumns;
		private System.Windows.Forms.ColumnHeader lvcColumnName;
		private System.Windows.Forms.ColumnHeader lvcJoinedColumns;
		private System.Windows.Forms.ListView lvDatabaseColumns;
		private System.Windows.Forms.ColumnHeader lvcColumns;
		private System.Windows.Forms.Button bFilterData;
		private System.Windows.Forms.Button bSave;
		private System.Windows.Forms.Button bEditData;
	}
}