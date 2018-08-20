namespace CDesigner.Forms
{
    partial class NewPatternForm
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
            this.TLP_Content = new System.Windows.Forms.TableLayoutPanel();
            this.TB_PatternName = new System.Windows.Forms.TextBox();
            this.CBX_CopyFrom = new System.Windows.Forms.ComboBox();
            this.CBX_PaperFormat = new System.Windows.Forms.ComboBox();
            this.L_PatternName = new System.Windows.Forms.Label();
            this.L_PatternCopy = new System.Windows.Forms.Label();
            this.L_PaperFormat = new System.Windows.Forms.Label();
            this.L_PatternSize = new System.Windows.Forms.Label();
            this.N_Width = new System.Windows.Forms.NumericUpDown();
            this.N_Height = new System.Windows.Forms.NumericUpDown();
            this.B_Create = new System.Windows.Forms.Button();
            this.B_Cancel = new System.Windows.Forms.Button();
            this.TLP_Main = new System.Windows.Forms.TableLayoutPanel();
            this.TLP_StatusBar = new System.Windows.Forms.TableLayoutPanel();
            this.TT_BadChars = new System.Windows.Forms.ToolTip(this.components);
            this.TLP_Content.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.N_Width)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.N_Height)).BeginInit();
            this.TLP_Main.SuspendLayout();
            this.TLP_StatusBar.SuspendLayout();
            this.SuspendLayout();
            // 
            // TLP_Content
            // 
            this.TLP_Content.ColumnCount = 3;
            this.TLP_Content.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33332F));
            this.TLP_Content.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33334F));
            this.TLP_Content.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33334F));
            this.TLP_Content.Controls.Add(this.TB_PatternName, 1, 0);
            this.TLP_Content.Controls.Add(this.CBX_CopyFrom, 1, 1);
            this.TLP_Content.Controls.Add(this.CBX_PaperFormat, 1, 2);
            this.TLP_Content.Controls.Add(this.L_PatternName, 0, 0);
            this.TLP_Content.Controls.Add(this.L_PatternCopy, 0, 1);
            this.TLP_Content.Controls.Add(this.L_PaperFormat, 0, 2);
            this.TLP_Content.Controls.Add(this.L_PatternSize, 0, 3);
            this.TLP_Content.Controls.Add(this.N_Width, 1, 3);
            this.TLP_Content.Controls.Add(this.N_Height, 2, 3);
            this.TLP_Content.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TLP_Content.Location = new System.Drawing.Point(0, 0);
            this.TLP_Content.Margin = new System.Windows.Forms.Padding(0);
            this.TLP_Content.Name = "TLP_Content";
            this.TLP_Content.Padding = new System.Windows.Forms.Padding(3, 3, 3, 5);
            this.TLP_Content.RowCount = 4;
            this.TLP_Content.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.TLP_Content.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.TLP_Content.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.TLP_Content.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.TLP_Content.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.TLP_Content.Size = new System.Drawing.Size(335, 116);
            this.TLP_Content.TabIndex = 0;
            // 
            // TB_PatternName
            // 
            this.TLP_Content.SetColumnSpan(this.TB_PatternName, 2);
            this.TB_PatternName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TB_PatternName.Location = new System.Drawing.Point(115, 6);
            this.TB_PatternName.MaxLength = 127;
            this.TB_PatternName.Name = "TB_PatternName";
            this.TB_PatternName.Size = new System.Drawing.Size(214, 20);
            this.TB_PatternName.TabIndex = 1;
            this.TB_PatternName.Text = " Nazwa wzoru";
            this.TB_PatternName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TB_PatternName_KeyPress);
            this.TB_PatternName.Leave += new System.EventHandler(this.TB_PatternName_Leave);
            // 
            // CBX_CopyFrom
            // 
            this.TLP_Content.SetColumnSpan(this.CBX_CopyFrom, 2);
            this.CBX_CopyFrom.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CBX_CopyFrom.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CBX_CopyFrom.FormattingEnabled = true;
            this.CBX_CopyFrom.Location = new System.Drawing.Point(115, 33);
            this.CBX_CopyFrom.Name = "CBX_CopyFrom";
            this.CBX_CopyFrom.Size = new System.Drawing.Size(214, 21);
            this.CBX_CopyFrom.TabIndex = 2;
            this.CBX_CopyFrom.SelectedIndexChanged += new System.EventHandler(this.CBX_CopyFrom_SelectedIndexChanged);
            // 
            // CBX_PaperFormat
            // 
            this.TLP_Content.SetColumnSpan(this.CBX_PaperFormat, 2);
            this.CBX_PaperFormat.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CBX_PaperFormat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CBX_PaperFormat.FormattingEnabled = true;
            this.CBX_PaperFormat.Items.AddRange(new object[] {
            "Własny"});
            this.CBX_PaperFormat.Location = new System.Drawing.Point(115, 60);
            this.CBX_PaperFormat.Name = "CBX_PaperFormat";
            this.CBX_PaperFormat.Size = new System.Drawing.Size(214, 21);
            this.CBX_PaperFormat.TabIndex = 3;
            this.CBX_PaperFormat.SelectedIndexChanged += new System.EventHandler(this.CBX_PaperFormat_SelectedIndexChanged);
            // 
            // L_PatternName
            // 
            this.L_PatternName.AutoSize = true;
            this.L_PatternName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.L_PatternName.Location = new System.Drawing.Point(6, 3);
            this.L_PatternName.Name = "L_PatternName";
            this.L_PatternName.Size = new System.Drawing.Size(103, 27);
            this.L_PatternName.TabIndex = 6;
            this.L_PatternName.Text = "Nazwa wzoru:";
            this.L_PatternName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // L_PatternCopy
            // 
            this.L_PatternCopy.AutoSize = true;
            this.L_PatternCopy.Dock = System.Windows.Forms.DockStyle.Fill;
            this.L_PatternCopy.Location = new System.Drawing.Point(6, 30);
            this.L_PatternCopy.Name = "L_PatternCopy";
            this.L_PatternCopy.Size = new System.Drawing.Size(103, 27);
            this.L_PatternCopy.TabIndex = 7;
            this.L_PatternCopy.Text = "Kopiuj z wzoru:";
            this.L_PatternCopy.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // L_PaperFormat
            // 
            this.L_PaperFormat.AutoSize = true;
            this.L_PaperFormat.Dock = System.Windows.Forms.DockStyle.Fill;
            this.L_PaperFormat.Location = new System.Drawing.Point(6, 57);
            this.L_PaperFormat.Name = "L_PaperFormat";
            this.L_PaperFormat.Size = new System.Drawing.Size(103, 27);
            this.L_PaperFormat.TabIndex = 8;
            this.L_PaperFormat.Text = "Format papieru:";
            this.L_PaperFormat.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // L_PatternSize
            // 
            this.L_PatternSize.AutoSize = true;
            this.L_PatternSize.Dock = System.Windows.Forms.DockStyle.Fill;
            this.L_PatternSize.Location = new System.Drawing.Point(6, 84);
            this.L_PatternSize.Name = "L_PatternSize";
            this.L_PatternSize.Size = new System.Drawing.Size(103, 27);
            this.L_PatternSize.TabIndex = 9;
            this.L_PatternSize.Text = "Rozmiar (mm):";
            this.L_PatternSize.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // N_Width
            // 
            this.N_Width.Dock = System.Windows.Forms.DockStyle.Fill;
            this.N_Width.Location = new System.Drawing.Point(115, 89);
            this.N_Width.Margin = new System.Windows.Forms.Padding(3, 5, 3, 3);
            this.N_Width.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.N_Width.Name = "N_Width";
            this.N_Width.Size = new System.Drawing.Size(103, 20);
            this.N_Width.TabIndex = 4;
            // 
            // N_Height
            // 
            this.N_Height.Dock = System.Windows.Forms.DockStyle.Fill;
            this.N_Height.Location = new System.Drawing.Point(224, 89);
            this.N_Height.Margin = new System.Windows.Forms.Padding(3, 5, 3, 3);
            this.N_Height.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.N_Height.Name = "N_Height";
            this.N_Height.Size = new System.Drawing.Size(105, 20);
            this.N_Height.TabIndex = 5;
            // 
            // B_Create
            // 
            this.B_Create.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.B_Create.Dock = System.Windows.Forms.DockStyle.Fill;
            this.B_Create.Location = new System.Drawing.Point(115, 4);
            this.B_Create.Margin = new System.Windows.Forms.Padding(3, 3, 2, 3);
            this.B_Create.Name = "B_Create";
            this.B_Create.Size = new System.Drawing.Size(105, 25);
            this.B_Create.TabIndex = 6;
            this.B_Create.Text = "Utwórz";
            this.B_Create.UseVisualStyleBackColor = true;
            this.B_Create.Click += new System.EventHandler(this.B_Create_Click);
            // 
            // B_Cancel
            // 
            this.B_Cancel.AutoSize = true;
            this.B_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.B_Cancel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.B_Cancel.Location = new System.Drawing.Point(223, 4);
            this.B_Cancel.Margin = new System.Windows.Forms.Padding(1, 3, 3, 3);
            this.B_Cancel.Name = "B_Cancel";
            this.B_Cancel.Size = new System.Drawing.Size(106, 25);
            this.B_Cancel.TabIndex = 7;
            this.B_Cancel.Text = "Anuluj";
            this.B_Cancel.UseVisualStyleBackColor = true;
            // 
            // TLP_Main
            // 
            this.TLP_Main.ColumnCount = 1;
            this.TLP_Main.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.TLP_Main.Controls.Add(this.TLP_StatusBar, 0, 1);
            this.TLP_Main.Controls.Add(this.TLP_Content, 0, 0);
            this.TLP_Main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TLP_Main.Location = new System.Drawing.Point(0, 0);
            this.TLP_Main.Name = "TLP_Main";
            this.TLP_Main.RowCount = 2;
            this.TLP_Main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.TLP_Main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.TLP_Main.Size = new System.Drawing.Size(335, 148);
            this.TLP_Main.TabIndex = 10;
            // 
            // TLP_StatusBar
            // 
            this.TLP_StatusBar.BackColor = System.Drawing.SystemColors.ControlLight;
            this.TLP_StatusBar.ColumnCount = 3;
            this.TLP_StatusBar.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.TLP_StatusBar.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 110F));
            this.TLP_StatusBar.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 110F));
            this.TLP_StatusBar.Controls.Add(this.B_Cancel, 2, 0);
            this.TLP_StatusBar.Controls.Add(this.B_Create, 1, 0);
            this.TLP_StatusBar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TLP_StatusBar.Location = new System.Drawing.Point(0, 116);
            this.TLP_StatusBar.Margin = new System.Windows.Forms.Padding(0);
            this.TLP_StatusBar.Name = "TLP_StatusBar";
            this.TLP_StatusBar.Padding = new System.Windows.Forms.Padding(3, 1, 3, 0);
            this.TLP_StatusBar.RowCount = 1;
            this.TLP_StatusBar.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.TLP_StatusBar.Size = new System.Drawing.Size(335, 32);
            this.TLP_StatusBar.TabIndex = 37;
            this.TLP_StatusBar.Paint += new System.Windows.Forms.PaintEventHandler(this.TLP_StatusBar_Paint);
            // 
            // NewPatternForm
            // 
            this.AcceptButton = this.B_Create;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.B_Cancel;
            this.ClientSize = new System.Drawing.Size(335, 148);
            this.Controls.Add(this.TLP_Main);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "NewPatternForm";
            this.Text = "Nowy Wzór";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.NewPatternForm_FormClosing);
            this.Move += new System.EventHandler(this.NewPatternForm_Move);
            this.TLP_Content.ResumeLayout(false);
            this.TLP_Content.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.N_Width)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.N_Height)).EndInit();
            this.TLP_Main.ResumeLayout(false);
            this.TLP_StatusBar.ResumeLayout(false);
            this.TLP_StatusBar.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel TLP_Content;
        private System.Windows.Forms.Button B_Create;
        private System.Windows.Forms.TextBox TB_PatternName;
        private System.Windows.Forms.ComboBox CBX_CopyFrom;
        private System.Windows.Forms.ComboBox CBX_PaperFormat;
        private System.Windows.Forms.Label L_PatternName;
        private System.Windows.Forms.Label L_PatternCopy;
        private System.Windows.Forms.Label L_PaperFormat;
        private System.Windows.Forms.Label L_PatternSize;
        private System.Windows.Forms.NumericUpDown N_Width;
        private System.Windows.Forms.NumericUpDown N_Height;
        private System.Windows.Forms.Button B_Cancel;
        private System.Windows.Forms.TableLayoutPanel TLP_Main;
        private System.Windows.Forms.TableLayoutPanel TLP_StatusBar;
        private System.Windows.Forms.ToolTip TT_BadChars;

        /// @endcond
    }
}