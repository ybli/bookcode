namespace MapIcon
{
    partial class MainInterface
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainInterface));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.ToolStripMenuItem_MopTXT = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_MDraw = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bMPToolStripMenuItem_MOUT = new System.Windows.Forms.ToolStripMenuItem();
            this.dXFToolStripMenuItem_MOUT = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton_TopTXT = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton_TDraw = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton_DxfOut = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton_Clear = new System.Windows.Forms.ToolStripButton();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.dvg = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.pb = new System.Windows.Forms.PictureBox();
            this.opTXT = new System.Windows.Forms.OpenFileDialog();
            this.saveBMP = new System.Windows.Forms.SaveFileDialog();
            this.saDXF = new System.Windows.Forms.SaveFileDialog();
            this.menuStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dvg)).BeginInit();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pb)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItem_MopTXT,
            this.ToolStripMenuItem_MDraw,
            this.ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(859, 25);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // ToolStripMenuItem_MopTXT
            // 
            this.ToolStripMenuItem_MopTXT.Name = "ToolStripMenuItem_MopTXT";
            this.ToolStripMenuItem_MopTXT.Size = new System.Drawing.Size(68, 21);
            this.ToolStripMenuItem_MopTXT.Text = "打开文件";
            this.ToolStripMenuItem_MopTXT.Click += new System.EventHandler(this.ToolStripMenuItem_MopTXT_Click);
            // 
            // ToolStripMenuItem_MDraw
            // 
            this.ToolStripMenuItem_MDraw.Name = "ToolStripMenuItem_MDraw";
            this.ToolStripMenuItem_MDraw.Size = new System.Drawing.Size(44, 21);
            this.ToolStripMenuItem_MDraw.Text = "成图";
            this.ToolStripMenuItem_MDraw.Click += new System.EventHandler(this.ToolStripMenuItem_MDraw_Click);
            // 
            // ToolStripMenuItem
            // 
            this.ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.bMPToolStripMenuItem_MOUT,
            this.dXFToolStripMenuItem_MOUT});
            this.ToolStripMenuItem.Name = "ToolStripMenuItem";
            this.ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.ToolStripMenuItem.Text = "输出";
            // 
            // bMPToolStripMenuItem_MOUT
            // 
            this.bMPToolStripMenuItem_MOUT.Name = "bMPToolStripMenuItem_MOUT";
            this.bMPToolStripMenuItem_MOUT.Size = new System.Drawing.Size(152, 22);
            this.bMPToolStripMenuItem_MOUT.Text = "BMP";
            this.bMPToolStripMenuItem_MOUT.Click += new System.EventHandler(this.bMPToolStripMenuItem_MOUT_Click);
            // 
            // dXFToolStripMenuItem_MOUT
            // 
            this.dXFToolStripMenuItem_MOUT.Name = "dXFToolStripMenuItem_MOUT";
            this.dXFToolStripMenuItem_MOUT.Size = new System.Drawing.Size(152, 22);
            this.dXFToolStripMenuItem_MOUT.Text = "DXF";
            this.dXFToolStripMenuItem_MOUT.Click += new System.EventHandler(this.dXFToolStripMenuItem_MOUT_Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(30, 30);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton_TopTXT,
            this.toolStripButton_TDraw,
            this.toolStripButton_DxfOut,
            this.toolStripButton_Clear});
            this.toolStrip1.Location = new System.Drawing.Point(0, 25);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(859, 37);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButton_TopTXT
            // 
            this.toolStripButton_TopTXT.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton_TopTXT.Image")));
            this.toolStripButton_TopTXT.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_TopTXT.Name = "toolStripButton_TopTXT";
            this.toolStripButton_TopTXT.Size = new System.Drawing.Size(90, 34);
            this.toolStripButton_TopTXT.Text = "打开文件";
            this.toolStripButton_TopTXT.Click += new System.EventHandler(this.toolStripButton_TopTXT_Click);
            // 
            // toolStripButton_TDraw
            // 
            this.toolStripButton_TDraw.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton_TDraw.Image")));
            this.toolStripButton_TDraw.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_TDraw.Name = "toolStripButton_TDraw";
            this.toolStripButton_TDraw.Size = new System.Drawing.Size(66, 34);
            this.toolStripButton_TDraw.Text = "成图";
            this.toolStripButton_TDraw.Click += new System.EventHandler(this.toolStripButton_TDraw_Click);
            // 
            // toolStripButton_DxfOut
            // 
            this.toolStripButton_DxfOut.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton_DxfOut.Image")));
            this.toolStripButton_DxfOut.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_DxfOut.Name = "toolStripButton_DxfOut";
            this.toolStripButton_DxfOut.Size = new System.Drawing.Size(89, 34);
            this.toolStripButton_DxfOut.Text = "输出DXF";
            this.toolStripButton_DxfOut.Click += new System.EventHandler(this.toolStripButton_DxfOut_Click);
            // 
            // toolStripButton_Clear
            // 
            this.toolStripButton_Clear.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton_Clear.Image")));
            this.toolStripButton_Clear.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_Clear.Name = "toolStripButton_Clear";
            this.toolStripButton_Clear.Size = new System.Drawing.Size(66, 34);
            this.toolStripButton_Clear.Text = "清除";
            this.toolStripButton_Clear.Click += new System.EventHandler(this.toolStripButton_Clear_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 62);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(859, 390);
            this.tabControl1.TabIndex = 2;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.dvg);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(851, 364);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "数据";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // dvg
            // 
            this.dvg.AllowUserToAddRows = false;
            this.dvg.AllowUserToDeleteRows = false;
            this.dvg.AllowUserToResizeColumns = false;
            this.dvg.AllowUserToResizeRows = false;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dvg.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle7;
            this.dvg.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dvg.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4,
            this.Column5});
            this.dvg.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dvg.Location = new System.Drawing.Point(3, 3);
            this.dvg.Name = "dvg";
            this.dvg.RowTemplate.Height = 23;
            this.dvg.Size = new System.Drawing.Size(845, 358);
            this.dvg.TabIndex = 0;
            // 
            // Column1
            // 
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Column1.DefaultCellStyle = dataGridViewCellStyle8;
            this.Column1.HeaderText = "序号";
            this.Column1.Name = "Column1";
            this.Column1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Column2
            // 
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Column2.DefaultCellStyle = dataGridViewCellStyle9;
            this.Column2.HeaderText = "编码";
            this.Column2.Name = "Column2";
            this.Column2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column2.Width = 150;
            // 
            // Column3
            // 
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Column3.DefaultCellStyle = dataGridViewCellStyle10;
            this.Column3.HeaderText = "X";
            this.Column3.Name = "Column3";
            this.Column3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column3.Width = 200;
            // 
            // Column4
            // 
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Column4.DefaultCellStyle = dataGridViewCellStyle11;
            this.Column4.HeaderText = "Y";
            this.Column4.Name = "Column4";
            this.Column4.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column4.Width = 200;
            // 
            // Column5
            // 
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Column5.DefaultCellStyle = dataGridViewCellStyle12;
            this.Column5.HeaderText = "Z";
            this.Column5.Name = "Column5";
            this.Column5.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column5.Width = 150;
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.Color.Black;
            this.tabPage2.Controls.Add(this.pb);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(851, 364);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "图形";
            // 
            // pb
            // 
            this.pb.BackColor = System.Drawing.Color.Black;
            this.pb.Cursor = System.Windows.Forms.Cursors.SizeAll;
            this.pb.Location = new System.Drawing.Point(0, 0);
            this.pb.Name = "pb";
            this.pb.Size = new System.Drawing.Size(200, 200);
            this.pb.TabIndex = 0;
            this.pb.TabStop = false;
            this.pb.Click += new System.EventHandler(this.pb_Click);
            this.pb.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pb_MouseDown);
            this.pb.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pb_MouseMove);
            this.pb.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pb_MouseUp);
            // 
            // opTXT
            // 
            this.opTXT.FileName = "openFileDialog1";
            // 
            // MainInterface
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(859, 452);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainInterface";
            this.Text = "自动成图";
            this.Load += new System.EventHandler(this.MainInterface_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dvg)).EndInit();
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pb)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_MopTXT;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_MDraw;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem bMPToolStripMenuItem_MOUT;
        private System.Windows.Forms.ToolStripMenuItem dXFToolStripMenuItem_MOUT;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripButton_TopTXT;
        private System.Windows.Forms.ToolStripButton toolStripButton_TDraw;
        private System.Windows.Forms.ToolStripButton toolStripButton_DxfOut;
        private System.Windows.Forms.ToolStripButton toolStripButton_Clear;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.DataGridView dvg;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.OpenFileDialog opTXT;
        public System.Windows.Forms.PictureBox pb;
        private System.Windows.Forms.SaveFileDialog saveBMP;
        private System.Windows.Forms.SaveFileDialog saDXF;
    }
}

