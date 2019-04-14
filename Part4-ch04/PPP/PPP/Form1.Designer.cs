namespace PPP
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
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.文件ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.读取数据ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.计算ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.开始计算ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.绘图ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.x坐标曲线ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.y坐标曲线ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.z坐标曲线ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.e方向偏差ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.n方向偏差ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.u方向偏差ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.输出文本ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.输出计算坐标ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.filename = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.filepath = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton3 = new System.Windows.Forms.ToolStripButton();
            this.realZ = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.realY = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.realX = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripLabel3 = new System.Windows.Forms.ToolStripLabel();
            this.退出ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            this.tabPage3.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.AutoSize = false;
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.文件ToolStripMenuItem,
            this.计算ToolStripMenuItem,
            this.绘图ToolStripMenuItem,
            this.输出文本ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(695, 35);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 文件ToolStripMenuItem
            // 
            this.文件ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.读取数据ToolStripMenuItem,
            this.退出ToolStripMenuItem});
            this.文件ToolStripMenuItem.Name = "文件ToolStripMenuItem";
            this.文件ToolStripMenuItem.Size = new System.Drawing.Size(51, 31);
            this.文件ToolStripMenuItem.Text = "文件";
            // 
            // 读取数据ToolStripMenuItem
            // 
            this.读取数据ToolStripMenuItem.Name = "读取数据ToolStripMenuItem";
            this.读取数据ToolStripMenuItem.Size = new System.Drawing.Size(181, 26);
            this.读取数据ToolStripMenuItem.Text = "读取数据";
            this.读取数据ToolStripMenuItem.Click += new System.EventHandler(this.读取数据ToolStripMenuItem_Click);
            // 
            // 计算ToolStripMenuItem
            // 
            this.计算ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.开始计算ToolStripMenuItem});
            this.计算ToolStripMenuItem.Name = "计算ToolStripMenuItem";
            this.计算ToolStripMenuItem.Size = new System.Drawing.Size(51, 31);
            this.计算ToolStripMenuItem.Text = "计算";
            // 
            // 开始计算ToolStripMenuItem
            // 
            this.开始计算ToolStripMenuItem.Name = "开始计算ToolStripMenuItem";
            this.开始计算ToolStripMenuItem.Size = new System.Drawing.Size(181, 26);
            this.开始计算ToolStripMenuItem.Text = "开始计算";
            this.开始计算ToolStripMenuItem.Click += new System.EventHandler(this.开始计算ToolStripMenuItem_Click);
            // 
            // 绘图ToolStripMenuItem
            // 
            this.绘图ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.x坐标曲线ToolStripMenuItem,
            this.y坐标曲线ToolStripMenuItem,
            this.z坐标曲线ToolStripMenuItem,
            this.e方向偏差ToolStripMenuItem,
            this.n方向偏差ToolStripMenuItem,
            this.u方向偏差ToolStripMenuItem});
            this.绘图ToolStripMenuItem.Name = "绘图ToolStripMenuItem";
            this.绘图ToolStripMenuItem.Size = new System.Drawing.Size(51, 31);
            this.绘图ToolStripMenuItem.Text = "绘图";
            // 
            // x坐标曲线ToolStripMenuItem
            // 
            this.x坐标曲线ToolStripMenuItem.Name = "x坐标曲线ToolStripMenuItem";
            this.x坐标曲线ToolStripMenuItem.Size = new System.Drawing.Size(156, 26);
            this.x坐标曲线ToolStripMenuItem.Text = "X坐标曲线";
            this.x坐标曲线ToolStripMenuItem.Click += new System.EventHandler(this.x坐标曲线ToolStripMenuItem_Click);
            // 
            // y坐标曲线ToolStripMenuItem
            // 
            this.y坐标曲线ToolStripMenuItem.Name = "y坐标曲线ToolStripMenuItem";
            this.y坐标曲线ToolStripMenuItem.Size = new System.Drawing.Size(156, 26);
            this.y坐标曲线ToolStripMenuItem.Text = "Y坐标曲线";
            this.y坐标曲线ToolStripMenuItem.Click += new System.EventHandler(this.y坐标曲线ToolStripMenuItem_Click);
            // 
            // z坐标曲线ToolStripMenuItem
            // 
            this.z坐标曲线ToolStripMenuItem.Name = "z坐标曲线ToolStripMenuItem";
            this.z坐标曲线ToolStripMenuItem.Size = new System.Drawing.Size(156, 26);
            this.z坐标曲线ToolStripMenuItem.Text = "Z坐标曲线";
            this.z坐标曲线ToolStripMenuItem.Click += new System.EventHandler(this.z坐标曲线ToolStripMenuItem_Click);
            // 
            // e方向偏差ToolStripMenuItem
            // 
            this.e方向偏差ToolStripMenuItem.Name = "e方向偏差ToolStripMenuItem";
            this.e方向偏差ToolStripMenuItem.Size = new System.Drawing.Size(156, 26);
            this.e方向偏差ToolStripMenuItem.Text = "E方向偏差";
            this.e方向偏差ToolStripMenuItem.Click += new System.EventHandler(this.e方向偏差ToolStripMenuItem_Click);
            // 
            // n方向偏差ToolStripMenuItem
            // 
            this.n方向偏差ToolStripMenuItem.Name = "n方向偏差ToolStripMenuItem";
            this.n方向偏差ToolStripMenuItem.Size = new System.Drawing.Size(156, 26);
            this.n方向偏差ToolStripMenuItem.Text = "N方向偏差";
            this.n方向偏差ToolStripMenuItem.Click += new System.EventHandler(this.n方向偏差ToolStripMenuItem_Click);
            // 
            // u方向偏差ToolStripMenuItem
            // 
            this.u方向偏差ToolStripMenuItem.Name = "u方向偏差ToolStripMenuItem";
            this.u方向偏差ToolStripMenuItem.Size = new System.Drawing.Size(156, 26);
            this.u方向偏差ToolStripMenuItem.Text = "U方向偏差";
            this.u方向偏差ToolStripMenuItem.Click += new System.EventHandler(this.u方向偏差ToolStripMenuItem_Click);
            // 
            // 输出文本ToolStripMenuItem
            // 
            this.输出文本ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.输出计算坐标ToolStripMenuItem});
            this.输出文本ToolStripMenuItem.Name = "输出文本ToolStripMenuItem";
            this.输出文本ToolStripMenuItem.Size = new System.Drawing.Size(81, 31);
            this.输出文本ToolStripMenuItem.Text = "输出文本";
            // 
            // 输出计算坐标ToolStripMenuItem
            // 
            this.输出计算坐标ToolStripMenuItem.Name = "输出计算坐标ToolStripMenuItem";
            this.输出计算坐标ToolStripMenuItem.Size = new System.Drawing.Size(174, 26);
            this.输出计算坐标ToolStripMenuItem.Text = "输出计算坐标";
            this.输出计算坐标ToolStripMenuItem.Click += new System.EventHandler(this.输出计算坐标ToolStripMenuItem_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Location = new System.Drawing.Point(0, 477);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(695, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.tabControl1);
            this.panel1.Controls.Add(this.toolStrip1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 35);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(695, 442);
            this.panel1.TabIndex = 2;
            this.panel1.TabStop = true;
            // 
            // tabControl1
            // 
            this.tabControl1.Alignment = System.Windows.Forms.TabAlignment.Bottom;
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 32);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(695, 410);
            this.tabControl1.TabIndex = 1;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.dataGridView1);
            this.tabPage1.Location = new System.Drawing.Point(4, 4);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(687, 381);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "文件";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.filename,
            this.filepath});
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(3, 3);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 27;
            this.dataGridView1.Size = new System.Drawing.Size(681, 375);
            this.dataGridView1.TabIndex = 0;
            // 
            // filename
            // 
            this.filename.HeaderText = "文件名";
            this.filename.Name = "filename";
            this.filename.ReadOnly = true;
            this.filename.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // filepath
            // 
            this.filepath.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.filepath.HeaderText = "文件路径";
            this.filepath.Name = "filepath";
            this.filepath.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.chart1);
            this.tabPage2.Location = new System.Drawing.Point(4, 4);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(687, 381);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "绘图";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // chart1
            // 
            this.chart1.Dock = System.Windows.Forms.DockStyle.Fill;
            legend2.Name = "Legend1";
            this.chart1.Legends.Add(legend2);
            this.chart1.Location = new System.Drawing.Point(3, 3);
            this.chart1.Name = "chart1";
            series2.Legend = "Legend1";
            series2.Name = "Series1";
            this.chart1.Series.Add(series2);
            this.chart1.Size = new System.Drawing.Size(681, 375);
            this.chart1.TabIndex = 0;
            this.chart1.Text = "chart1";
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.richTextBox1);
            this.tabPage3.Location = new System.Drawing.Point(4, 4);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(687, 381);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "文本";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // richTextBox1
            // 
            this.richTextBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBox1.Location = new System.Drawing.Point(3, 3);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(681, 375);
            this.richTextBox1.TabIndex = 0;
            this.richTextBox1.Text = "";
            this.richTextBox1.WordWrap = false;
            // 
            // toolStrip1
            // 
            this.toolStrip1.AutoSize = false;
            this.toolStrip1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1,
            this.toolStripButton2,
            this.toolStripButton3,
            this.realZ,
            this.toolStripLabel1,
            this.realY,
            this.toolStripLabel2,
            this.realX,
            this.toolStripLabel3});
            this.toolStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Padding = new System.Windows.Forms.Padding(1, 0, 3, 0);
            this.toolStrip1.Size = new System.Drawing.Size(695, 32);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.AutoSize = false;
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.White;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Padding = new System.Windows.Forms.Padding(0, 0, 1, 0);
            this.toolStripButton1.Size = new System.Drawing.Size(24, 30);
            this.toolStripButton1.Text = "toolStripButton1";
            this.toolStripButton1.ToolTipText = "读取文件";
            this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton2.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton2.Image")));
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(24, 29);
            this.toolStripButton2.Text = "toolStripButton2";
            this.toolStripButton2.ToolTipText = "开始计算";
            this.toolStripButton2.Click += new System.EventHandler(this.toolStripButton2_Click);
            // 
            // toolStripButton3
            // 
            this.toolStripButton3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton3.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton3.Image")));
            this.toolStripButton3.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton3.Name = "toolStripButton3";
            this.toolStripButton3.Size = new System.Drawing.Size(24, 29);
            this.toolStripButton3.Text = "toolStripButton3";
            this.toolStripButton3.ToolTipText = "保存计算结果";
            this.toolStripButton3.Click += new System.EventHandler(this.toolStripButton3_Click);
            // 
            // realZ
            // 
            this.realZ.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.realZ.AutoSize = false;
            this.realZ.Font = new System.Drawing.Font("Microsoft YaHei UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.realZ.Margin = new System.Windows.Forms.Padding(1, 0, 8, 0);
            this.realZ.Name = "realZ";
            this.realZ.Size = new System.Drawing.Size(100, 24);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(45, 29);
            this.toolStripLabel1.Text = "realZ";
            // 
            // realY
            // 
            this.realY.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.realY.Font = new System.Drawing.Font("Microsoft YaHei UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.realY.Name = "realY";
            this.realY.Size = new System.Drawing.Size(100, 32);
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(45, 29);
            this.toolStripLabel2.Text = "realY";
            // 
            // realX
            // 
            this.realX.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.realX.Font = new System.Drawing.Font("Microsoft YaHei UI", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.realX.Name = "realX";
            this.realX.Size = new System.Drawing.Size(100, 32);
            // 
            // toolStripLabel3
            // 
            this.toolStripLabel3.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripLabel3.Name = "toolStripLabel3";
            this.toolStripLabel3.Size = new System.Drawing.Size(46, 29);
            this.toolStripLabel3.Text = "realX";
            // 
            // 退出ToolStripMenuItem
            // 
            this.退出ToolStripMenuItem.Name = "退出ToolStripMenuItem";
            this.退出ToolStripMenuItem.Size = new System.Drawing.Size(181, 26);
            this.退出ToolStripMenuItem.Text = "退出";
            this.退出ToolStripMenuItem.Click += new System.EventHandler(this.退出ToolStripMenuItem_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(695, 499);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "GPS精密单点定位";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            this.tabPage3.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 文件ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 读取数据ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 计算ToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ToolStripMenuItem 绘图ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 输出文本ToolStripMenuItem;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripMenuItem x坐标曲线ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem y坐标曲线ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem z坐标曲线ToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.DataGridViewTextBoxColumn filename;
        private System.Windows.Forms.DataGridViewTextBoxColumn filepath;
        private System.Windows.Forms.ToolStripMenuItem 开始计算ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 输出计算坐标ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem e方向偏差ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem n方向偏差ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem u方向偏差ToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton toolStripButton2;
        private System.Windows.Forms.ToolStripButton toolStripButton3;
        private System.Windows.Forms.ToolStripTextBox realZ;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripTextBox realY;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
        private System.Windows.Forms.ToolStripTextBox realX;
        private System.Windows.Forms.ToolStripLabel toolStripLabel3;
        private System.Windows.Forms.ToolStripMenuItem 退出ToolStripMenuItem;
    }
}

