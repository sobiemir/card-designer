namespace CDesigner
{
	partial class DataFilterForm
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
			this.tlForm = new System.Windows.Forms.TableLayoutPanel();
			this.pFilterList = new System.Windows.Forms.Panel();
			this.tlFilterList = new System.Windows.Forms.TableLayoutPanel();
			this.lFilterType = new System.Windows.Forms.Label();
			this.lColumn = new System.Windows.Forms.Label();
			this.lModifier = new System.Windows.Forms.Label();
			this.lExclude = new System.Windows.Forms.Label();
			this.lResult = new System.Windows.Forms.Label();
			this.cbSelectAll = new System.Windows.Forms.CheckBox();
			this.flFilters = new System.Windows.Forms.FlowLayoutPanel();
			this.tlStatusBar = new System.Windows.Forms.TableLayoutPanel();
			this.bDelete = new System.Windows.Forms.Button();
			this.bAddFilter = new System.Windows.Forms.Button();
			this.bRestore = new System.Windows.Forms.Button();
			this.bAccept = new System.Windows.Forms.Button();
			this.tlForm.SuspendLayout();
			this.pFilterList.SuspendLayout();
			this.tlFilterList.SuspendLayout();
			this.tlStatusBar.SuspendLayout();
			this.SuspendLayout();
			// 
			// tlForm
			// 
			this.tlForm.ColumnCount = 6;
			this.tlForm.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
			this.tlForm.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
			this.tlForm.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
			this.tlForm.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
			this.tlForm.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
			this.tlForm.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
			this.tlForm.Controls.Add(this.pFilterList, 0, 0);
			this.tlForm.Controls.Add(this.tlStatusBar, 0, 1);
			this.tlForm.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tlForm.Location = new System.Drawing.Point(0, 0);
			this.tlForm.Name = "tlForm";
			this.tlForm.RowCount = 2;
			this.tlForm.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tlForm.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 37F));
			this.tlForm.Size = new System.Drawing.Size(632, 333);
			this.tlForm.TabIndex = 0;
			// 
			// pFilterList
			// 
			this.pFilterList.AutoScroll = true;
			this.pFilterList.AutoSize = true;
			this.pFilterList.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.tlForm.SetColumnSpan(this.pFilterList, 6);
			this.pFilterList.Controls.Add(this.tlFilterList);
			this.pFilterList.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pFilterList.Location = new System.Drawing.Point(6, 6);
			this.pFilterList.Margin = new System.Windows.Forms.Padding(6, 6, 6, 0);
			this.pFilterList.Name = "pFilterList";
			this.pFilterList.Size = new System.Drawing.Size(620, 290);
			this.pFilterList.TabIndex = 2;
			// 
			// tlFilterList
			// 
			this.tlFilterList.BackColor = System.Drawing.SystemColors.Control;
			this.tlFilterList.ColumnCount = 6;
			this.tlFilterList.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 5F));
			this.tlFilterList.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 24F));
			this.tlFilterList.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
			this.tlFilterList.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 22F));
			this.tlFilterList.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 24F));
			this.tlFilterList.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 5F));
			this.tlFilterList.Controls.Add(this.lFilterType, 2, 0);
			this.tlFilterList.Controls.Add(this.lColumn, 1, 0);
			this.tlFilterList.Controls.Add(this.lModifier, 3, 0);
			this.tlFilterList.Controls.Add(this.lExclude, 5, 0);
			this.tlFilterList.Controls.Add(this.lResult, 4, 0);
			this.tlFilterList.Controls.Add(this.cbSelectAll, 0, 0);
			this.tlFilterList.Controls.Add(this.flFilters, 0, 1);
			this.tlFilterList.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tlFilterList.Location = new System.Drawing.Point(0, 0);
			this.tlFilterList.Margin = new System.Windows.Forms.Padding(6, 6, 6, 0);
			this.tlFilterList.Name = "tlFilterList";
			this.tlFilterList.RowCount = 2;
			this.tlFilterList.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
			this.tlFilterList.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tlFilterList.Size = new System.Drawing.Size(618, 288);
			this.tlFilterList.TabIndex = 3;
			this.tlFilterList.Paint += new System.Windows.Forms.PaintEventHandler(this.tlFilterList_Paint);
			// 
			// lFilterType
			// 
			this.lFilterType.AutoSize = true;
			this.lFilterType.BackColor = System.Drawing.SystemColors.Window;
			this.lFilterType.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lFilterType.Location = new System.Drawing.Point(178, 0);
			this.lFilterType.Margin = new System.Windows.Forms.Padding(0, 0, 0, 1);
			this.lFilterType.Name = "lFilterType";
			this.lFilterType.Size = new System.Drawing.Size(123, 23);
			this.lFilterType.TabIndex = 7;
			this.lFilterType.Text = "Typ filtra";
			this.lFilterType.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// lColumn
			// 
			this.lColumn.AutoSize = true;
			this.lColumn.BackColor = System.Drawing.SystemColors.Window;
			this.lColumn.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lColumn.Location = new System.Drawing.Point(30, 0);
			this.lColumn.Margin = new System.Windows.Forms.Padding(0, 0, 0, 1);
			this.lColumn.Name = "lColumn";
			this.lColumn.Size = new System.Drawing.Size(148, 23);
			this.lColumn.TabIndex = 6;
			this.lColumn.Text = "Kolumna";
			this.lColumn.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// lModifier
			// 
			this.lModifier.AutoSize = true;
			this.lModifier.BackColor = System.Drawing.SystemColors.Window;
			this.lModifier.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lModifier.Location = new System.Drawing.Point(301, 0);
			this.lModifier.Margin = new System.Windows.Forms.Padding(0, 0, 0, 1);
			this.lModifier.Name = "lModifier";
			this.lModifier.Size = new System.Drawing.Size(135, 23);
			this.lModifier.TabIndex = 8;
			this.lModifier.Text = "Modyfikator";
			this.lModifier.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// lExclude
			// 
			this.lExclude.AutoSize = true;
			this.lExclude.BackColor = System.Drawing.SystemColors.Window;
			this.lExclude.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lExclude.Location = new System.Drawing.Point(584, 0);
			this.lExclude.Margin = new System.Windows.Forms.Padding(0, 0, 0, 1);
			this.lExclude.Name = "lExclude";
			this.lExclude.Size = new System.Drawing.Size(34, 23);
			this.lExclude.TabIndex = 10;
			this.lExclude.Text = "W";
			this.lExclude.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// lResult
			// 
			this.lResult.AutoSize = true;
			this.lResult.BackColor = System.Drawing.SystemColors.Window;
			this.lResult.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lResult.Location = new System.Drawing.Point(436, 0);
			this.lResult.Margin = new System.Windows.Forms.Padding(0, 0, 0, 1);
			this.lResult.Name = "lResult";
			this.lResult.Size = new System.Drawing.Size(148, 23);
			this.lResult.TabIndex = 11;
			this.lResult.Text = "Wynik";
			this.lResult.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// cbSelectAll
			// 
			this.cbSelectAll.AutoSize = true;
			this.cbSelectAll.BackColor = System.Drawing.SystemColors.Window;
			this.cbSelectAll.CheckAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.cbSelectAll.Dock = System.Windows.Forms.DockStyle.Fill;
			this.cbSelectAll.Location = new System.Drawing.Point(0, 0);
			this.cbSelectAll.Margin = new System.Windows.Forms.Padding(0, 0, 0, 1);
			this.cbSelectAll.Name = "cbSelectAll";
			this.cbSelectAll.Padding = new System.Windows.Forms.Padding(0, 2, 0, 0);
			this.cbSelectAll.Size = new System.Drawing.Size(30, 23);
			this.cbSelectAll.TabIndex = 14;
			this.cbSelectAll.UseVisualStyleBackColor = false;
			this.cbSelectAll.CheckedChanged += new System.EventHandler(this.cbSelectAll_CheckedChanged);
			// 
			// flFilters
			// 
			this.flFilters.AutoScroll = true;
			this.flFilters.BackColor = System.Drawing.SystemColors.Control;
			this.tlFilterList.SetColumnSpan(this.flFilters, 6);
			this.flFilters.Dock = System.Windows.Forms.DockStyle.Fill;
			this.flFilters.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
			this.flFilters.Location = new System.Drawing.Point(0, 24);
			this.flFilters.Margin = new System.Windows.Forms.Padding(0);
			this.flFilters.Name = "flFilters";
			this.flFilters.Size = new System.Drawing.Size(618, 264);
			this.flFilters.TabIndex = 15;
			this.flFilters.WrapContents = false;
			this.flFilters.Resize += new System.EventHandler(this.flFilters_Resize);
			// 
			// tlStatusBar
			// 
			this.tlStatusBar.BackColor = System.Drawing.SystemColors.ControlLight;
			this.tlStatusBar.ColumnCount = 6;
			this.tlForm.SetColumnSpan(this.tlStatusBar, 6);
			this.tlStatusBar.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
			this.tlStatusBar.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
			this.tlStatusBar.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
			this.tlStatusBar.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
			this.tlStatusBar.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
			this.tlStatusBar.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
			this.tlStatusBar.Controls.Add(this.bDelete, 0, 0);
			this.tlStatusBar.Controls.Add(this.bAddFilter, 0, 0);
			this.tlStatusBar.Controls.Add(this.bRestore, 4, 0);
			this.tlStatusBar.Controls.Add(this.bAccept, 5, 0);
			this.tlStatusBar.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tlStatusBar.Location = new System.Drawing.Point(0, 302);
			this.tlStatusBar.Margin = new System.Windows.Forms.Padding(0, 6, 0, 0);
			this.tlStatusBar.Name = "tlStatusBar";
			this.tlStatusBar.RowCount = 1;
			this.tlStatusBar.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tlStatusBar.Size = new System.Drawing.Size(632, 31);
			this.tlStatusBar.TabIndex = 3;
			this.tlStatusBar.Paint += new System.Windows.Forms.PaintEventHandler(this.tlStatusBar_Paint);
			// 
			// bDelete
			// 
			this.bDelete.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
			this.bDelete.Dock = System.Windows.Forms.DockStyle.Fill;
			this.bDelete.Enabled = false;
			this.bDelete.Location = new System.Drawing.Point(108, 4);
			this.bDelete.Margin = new System.Windows.Forms.Padding(3, 4, 6, 3);
			this.bDelete.Name = "bDelete";
			this.bDelete.Size = new System.Drawing.Size(96, 24);
			this.bDelete.TabIndex = 2;
			this.bDelete.Text = "Usuń";
			this.bDelete.UseVisualStyleBackColor = true;
			this.bDelete.Click += new System.EventHandler(this.bDelete_Click);
			// 
			// bAddFilter
			// 
			this.bAddFilter.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
			this.bAddFilter.Dock = System.Windows.Forms.DockStyle.Fill;
			this.bAddFilter.Location = new System.Drawing.Point(6, 4);
			this.bAddFilter.Margin = new System.Windows.Forms.Padding(6, 4, 3, 3);
			this.bAddFilter.Name = "bAddFilter";
			this.bAddFilter.Size = new System.Drawing.Size(96, 24);
			this.bAddFilter.TabIndex = 1;
			this.bAddFilter.Text = "Dodaj";
			this.bAddFilter.UseVisualStyleBackColor = true;
			this.bAddFilter.Click += new System.EventHandler(this.bAddFilter_Click);
			// 
			// bRestore
			// 
			this.bRestore.Dock = System.Windows.Forms.DockStyle.Fill;
			this.bRestore.Location = new System.Drawing.Point(426, 4);
			this.bRestore.Margin = new System.Windows.Forms.Padding(6, 4, 3, 3);
			this.bRestore.Name = "bRestore";
			this.bRestore.Size = new System.Drawing.Size(96, 24);
			this.bRestore.TabIndex = 3;
			this.bRestore.Text = "Przywróć";
			this.bRestore.UseVisualStyleBackColor = true;
			this.bRestore.Click += new System.EventHandler(this.bRestore_Click);
			// 
			// bAccept
			// 
			this.bAccept.Dock = System.Windows.Forms.DockStyle.Fill;
			this.bAccept.Location = new System.Drawing.Point(528, 4);
			this.bAccept.Margin = new System.Windows.Forms.Padding(3, 4, 6, 3);
			this.bAccept.Name = "bAccept";
			this.bAccept.Size = new System.Drawing.Size(98, 24);
			this.bAccept.TabIndex = 0;
			this.bAccept.Text = "Zastosuj";
			this.bAccept.UseVisualStyleBackColor = true;
			this.bAccept.Click += new System.EventHandler(this.bAccept_Click);
			// 
			// DataFilterForm
			// 
			this.AcceptButton = this.bAccept;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(632, 333);
			this.Controls.Add(this.tlForm);
			this.MaximizeBox = false;
			this.MaximumSize = new System.Drawing.Size(800, 65535);
			this.MinimumSize = new System.Drawing.Size(640, 360);
			this.Name = "DataFilterForm";
			this.Text = "Filtrowanie danych";
			this.ResizeEnd += new System.EventHandler(this.DataFilterForm_ResizeEnd);
			this.tlForm.ResumeLayout(false);
			this.tlForm.PerformLayout();
			this.pFilterList.ResumeLayout(false);
			this.tlFilterList.ResumeLayout(false);
			this.tlFilterList.PerformLayout();
			this.tlStatusBar.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TableLayoutPanel tlForm;
		private System.Windows.Forms.Panel pFilterList;
		private System.Windows.Forms.TableLayoutPanel tlFilterList;
		private System.Windows.Forms.Label lColumn;
		private System.Windows.Forms.Label lFilterType;
		private System.Windows.Forms.Label lModifier;
		private System.Windows.Forms.Label lResult;
		private System.Windows.Forms.TableLayoutPanel tlStatusBar;
		private System.Windows.Forms.Button bDelete;
		private System.Windows.Forms.Button bAddFilter;
		private System.Windows.Forms.Button bAccept;
		private System.Windows.Forms.CheckBox cbSelectAll;
		private System.Windows.Forms.Label lExclude;
		private System.Windows.Forms.Button bRestore;
		private System.Windows.Forms.FlowLayoutPanel flFilters;

	}
}