namespace WindowsFormsApplication1
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.toolOpen = new System.Windows.Forms.ToolStripButton();
            this.toolSave = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripComboBox1 = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.toolJS = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.toolData = new System.Windows.Forms.ToolStripButton();
            this.toolReport = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.toolHelp = new System.Windows.Forms.ToolStripButton();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.ColID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColX = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColY = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.toolStrip1.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // toolOpen
            // 
            this.toolOpen.Image = ((System.Drawing.Image)(resources.GetObject("toolOpen.Image")));
            this.toolOpen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolOpen.Name = "toolOpen";
            this.toolOpen.Size = new System.Drawing.Size(52, 22);
            this.toolOpen.Text = "打开";
            this.toolOpen.Click += new System.EventHandler(this.toolOpen_Click);
            // 
            // toolSave
            // 
            this.toolSave.Image = ((System.Drawing.Image)(resources.GetObject("toolSave.Image")));
            this.toolSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolSave.Name = "toolSave";
            this.toolSave.Size = new System.Drawing.Size(52, 22);
            this.toolSave.Text = "保存";
            this.toolSave.Click += new System.EventHandler(this.toolSave_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(68, 22);
            this.toolStripLabel1.Text = "压缩阈值：";
            // 
            // toolStripComboBox1
            // 
            this.toolStripComboBox1.Items.AddRange(new object[] {
            "50",
            "80",
            "100"});
            this.toolStripComboBox1.Name = "toolStripComboBox1";
            this.toolStripComboBox1.Size = new System.Drawing.Size(75, 25);
            this.toolStripComboBox1.Text = "50";
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(19, 22);
            this.toolStripLabel2.Text = "m";
            // 
            // toolJS
            // 
            this.toolJS.Image = ((System.Drawing.Image)(resources.GetObject("toolJS.Image")));
            this.toolJS.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolJS.Name = "toolJS";
            this.toolJS.Size = new System.Drawing.Size(52, 22);
            this.toolJS.Text = "计算";
            this.toolJS.Click += new System.EventHandler(this.toolJS_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
            // 
            // toolData
            // 
            this.toolData.Image = ((System.Drawing.Image)(resources.GetObject("toolData.Image")));
            this.toolData.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolData.Name = "toolData";
            this.toolData.Size = new System.Drawing.Size(52, 22);
            this.toolData.Text = "数据";
            this.toolData.Click += new System.EventHandler(this.toolData_Click);
            // 
            // toolReport
            // 
            this.toolReport.Image = ((System.Drawing.Image)(resources.GetObject("toolReport.Image")));
            this.toolReport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolReport.Name = "toolReport";
            this.toolReport.Size = new System.Drawing.Size(52, 22);
            this.toolReport.Text = "报告";
            this.toolReport.Click += new System.EventHandler(this.toolReport_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 25);
            // 
            // toolHelp
            // 
            this.toolHelp.Image = ((System.Drawing.Image)(resources.GetObject("toolHelp.Image")));
            this.toolHelp.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolHelp.Name = "toolHelp";
            this.toolHelp.Size = new System.Drawing.Size(52, 22);
            this.toolHelp.Text = "帮助";
            this.toolHelp.Click += new System.EventHandler(this.toolHelp_Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolOpen,
            this.toolSave,
            this.toolStripSeparator3,
            this.toolStripLabel1,
            this.toolStripComboBox1,
            this.toolStripLabel2,
            this.toolJS,
            this.toolStripSeparator4,
            this.toolData,
            this.toolReport,
            this.toolStripSeparator5,
            this.toolHelp});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(527, 25);
            this.toolStrip1.TabIndex = 2;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.textBox1);
            this.tabPage3.Location = new System.Drawing.Point(4, 4);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(519, 334);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "报告";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // textBox1
            // 
            this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox1.Location = new System.Drawing.Point(6, 6);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBox1.Size = new System.Drawing.Size(507, 322);
            this.textBox1.TabIndex = 0;
            // 
            // tabControl1
            // 
            this.tabControl1.Alignment = System.Windows.Forms.TabAlignment.Bottom;
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Location = new System.Drawing.Point(0, 28);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(527, 360);
            this.tabControl1.TabIndex = 3;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.dataGridView1);
            this.tabPage1.Location = new System.Drawing.Point(4, 4);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(519, 334);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "数据";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // dataGridView1
            // 
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColID,
            this.ColX,
            this.ColY});
            this.dataGridView1.Location = new System.Drawing.Point(3, 3);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(513, 325);
            this.dataGridView1.TabIndex = 0;
            // 
            // ColID
            // 
            this.ColID.HeaderText = "点号";
            this.ColID.Name = "ColID";
            // 
            // ColX
            // 
            this.ColX.HeaderText = "X坐标";
            this.ColX.Name = "ColX";
            // 
            // ColY
            // 
            this.ColY.HeaderText = "Y坐标";
            this.ColY.Name = "ColY";
            // 
            // Column3
            // 
            this.Column3.HeaderText = "    Y坐标";
            this.Column3.Name = "Column3";
            // 
            // Column2
            // 
            this.Column2.HeaderText = "    X坐标";
            this.Column2.Name = "Column2";
            // 
            // Column1
            // 
            this.Column1.HeaderText = "    点名";
            this.Column1.Name = "Column1";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(527, 389);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.toolStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Douglas–Peucker算法数据压缩";
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.ToolStripButton toolOpen;
        private System.Windows.Forms.ToolStripButton toolSave;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripComboBox toolStripComboBox1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
        private System.Windows.Forms.ToolStripButton toolJS;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripButton toolData;
        private System.Windows.Forms.ToolStripButton toolReport;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripButton toolHelp;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColID;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColX;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColY;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
    }
}

