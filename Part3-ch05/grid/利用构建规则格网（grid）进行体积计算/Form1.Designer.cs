namespace 利用构建规则格网_grid_进行体积计算
{
    partial class 规则网格进行体积计算
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(规则网格进行体积计算));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.文件 = new System.Windows.Forms.ToolStripMenuItem();
            this.打开文件 = new System.Windows.Forms.ToolStripMenuItem();
            this.保存文件 = new System.Windows.Forms.ToolStripMenuItem();
            this.退出1 = new System.Windows.Forms.ToolStripMenuItem();
            this.计算 = new System.Windows.Forms.ToolStripMenuItem();
            this.凸包点示意图ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.体积计算ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.退出 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.openfile = new System.Windows.Forms.ToolStripButton();
            this.savereport = new System.Windows.Forms.ToolStripButton();
            this.get_V = new System.Windows.Forms.ToolStripButton();
            this.生成凸包 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.图像放大 = new System.Windows.Forms.ToolStripButton();
            this.图像缩小 = new System.Windows.Forms.ToolStripButton();
            this.save_DXF = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton6 = new System.Windows.Forms.ToolStripButton();
            this.基准高程 = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton7 = new System.Windows.Forms.ToolStripButton();
            this.网格间隔 = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.help = new System.Windows.Forms.ToolStripButton();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.数据表格 = new System.Windows.Forms.ToolStripStatusLabel();
            this.凸包图形 = new System.Windows.Forms.ToolStripStatusLabel();
            this.报告 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.status = new System.Windows.Forms.ToolStripStatusLabel();
            this.datatable = new System.Windows.Forms.DataGridView();
            this.点名 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.X坐标 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Y坐标 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.H高程 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.report = new System.Windows.Forms.TextBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.menuStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.datatable)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.文件,
            this.计算,
            this.退出});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(668, 25);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 文件
            // 
            this.文件.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.打开文件,
            this.保存文件,
            this.退出1});
            this.文件.Name = "文件";
            this.文件.Size = new System.Drawing.Size(44, 21);
            this.文件.Text = "文件";
            // 
            // 打开文件
            // 
            this.打开文件.Name = "打开文件";
            this.打开文件.Size = new System.Drawing.Size(124, 22);
            this.打开文件.Text = "打开文件";
            this.打开文件.Click += new System.EventHandler(this.打开文件_Click);
            // 
            // 保存文件
            // 
            this.保存文件.Name = "保存文件";
            this.保存文件.Size = new System.Drawing.Size(124, 22);
            this.保存文件.Text = "保存文件";
            this.保存文件.Click += new System.EventHandler(this.保存文件_Click);
            // 
            // 退出1
            // 
            this.退出1.Name = "退出1";
            this.退出1.Size = new System.Drawing.Size(124, 22);
            this.退出1.Text = "退出";
            this.退出1.Click += new System.EventHandler(this.退出1_Click);
            // 
            // 计算
            // 
            this.计算.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.凸包点示意图ToolStripMenuItem,
            this.体积计算ToolStripMenuItem});
            this.计算.Name = "计算";
            this.计算.Size = new System.Drawing.Size(44, 21);
            this.计算.Text = "计算";
            // 
            // 凸包点示意图ToolStripMenuItem
            // 
            this.凸包点示意图ToolStripMenuItem.Name = "凸包点示意图ToolStripMenuItem";
            this.凸包点示意图ToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.凸包点示意图ToolStripMenuItem.Text = "凸包点示意图";
            this.凸包点示意图ToolStripMenuItem.Click += new System.EventHandler(this.凸包点示意图ToolStripMenuItem_Click);
            // 
            // 体积计算ToolStripMenuItem
            // 
            this.体积计算ToolStripMenuItem.Name = "体积计算ToolStripMenuItem";
            this.体积计算ToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.体积计算ToolStripMenuItem.Text = "体积计算";
            this.体积计算ToolStripMenuItem.Click += new System.EventHandler(this.体积计算ToolStripMenuItem_Click);
            // 
            // 退出
            // 
            this.退出.Name = "退出";
            this.退出.Size = new System.Drawing.Size(44, 21);
            this.退出.Text = "退出";
            this.退出.Click += new System.EventHandler(this.退出_Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openfile,
            this.savereport,
            this.生成凸包,
            this.get_V,
            this.toolStripSeparator3,
            this.图像放大,
            this.图像缩小,
            this.save_DXF,
            this.toolStripSeparator1,
            this.toolStripButton6,
            this.基准高程,
            this.toolStripSeparator2,
            this.toolStripButton7,
            this.网格间隔,
            this.toolStripSeparator4,
            this.help});
            this.toolStrip1.Location = new System.Drawing.Point(0, 25);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(668, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // openfile
            // 
            this.openfile.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.openfile.Image = ((System.Drawing.Image)(resources.GetObject("openfile.Image")));
            this.openfile.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.openfile.Name = "openfile";
            this.openfile.Size = new System.Drawing.Size(23, 22);
            this.openfile.Text = "打开数据";
            this.openfile.Click += new System.EventHandler(this.openfile_Click);
            // 
            // savereport
            // 
            this.savereport.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.savereport.Image = ((System.Drawing.Image)(resources.GetObject("savereport.Image")));
            this.savereport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.savereport.Name = "savereport";
            this.savereport.Size = new System.Drawing.Size(23, 22);
            this.savereport.Text = "保存报告";
            this.savereport.Click += new System.EventHandler(this.savereport_Click);
            // 
            // get_V
            // 
            this.get_V.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.get_V.Image = ((System.Drawing.Image)(resources.GetObject("get_V.Image")));
            this.get_V.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.get_V.Name = "get_V";
            this.get_V.Size = new System.Drawing.Size(28, 22);
            this.get_V.Text = " V ";
            this.get_V.ToolTipText = "体积计算，生成报告";
            this.get_V.Click += new System.EventHandler(this.get_V_Click);
            // 
            // 生成凸包
            // 
            this.生成凸包.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.生成凸包.Image = ((System.Drawing.Image)(resources.GetObject("生成凸包.Image")));
            this.生成凸包.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.生成凸包.Name = "生成凸包";
            this.生成凸包.Size = new System.Drawing.Size(23, 22);
            this.生成凸包.Text = "生成凸包";
            this.生成凸包.Click += new System.EventHandler(this.生成凸包_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // 图像放大
            // 
            this.图像放大.Image = ((System.Drawing.Image)(resources.GetObject("图像放大.Image")));
            this.图像放大.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.图像放大.Name = "图像放大";
            this.图像放大.Size = new System.Drawing.Size(37, 22);
            this.图像放大.Text = "+";
            this.图像放大.ToolTipText = "图像放大";
            this.图像放大.Click += new System.EventHandler(this.图像放大_Click);
            // 
            // 图像缩小
            // 
            this.图像缩小.Image = ((System.Drawing.Image)(resources.GetObject("图像缩小.Image")));
            this.图像缩小.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.图像缩小.Name = "图像缩小";
            this.图像缩小.Size = new System.Drawing.Size(33, 22);
            this.图像缩小.Text = "-";
            this.图像缩小.ToolTipText = "图像缩小";
            this.图像缩小.Click += new System.EventHandler(this.图像缩小_Click);
            // 
            // save_DXF
            // 
            this.save_DXF.Image = ((System.Drawing.Image)(resources.GetObject("save_DXF.Image")));
            this.save_DXF.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.save_DXF.Name = "save_DXF";
            this.save_DXF.Size = new System.Drawing.Size(75, 22);
            this.save_DXF.Text = "保存DXF";
            this.save_DXF.Click += new System.EventHandler(this.save_DXF_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButton6
            // 
            this.toolStripButton6.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButton6.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton6.Image")));
            this.toolStripButton6.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton6.Name = "toolStripButton6";
            this.toolStripButton6.Size = new System.Drawing.Size(91, 22);
            this.toolStripButton6.Text = "基准高程(m)：";
            this.toolStripButton6.ToolTipText = "基准高程(m)：";
            // 
            // 基准高程
            // 
            this.基准高程.Name = "基准高程";
            this.基准高程.Size = new System.Drawing.Size(50, 25);
            this.基准高程.ToolTipText = "基准高程";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButton7
            // 
            this.toolStripButton7.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButton7.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton7.Image")));
            this.toolStripButton7.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton7.Name = "toolStripButton7";
            this.toolStripButton7.Size = new System.Drawing.Size(91, 22);
            this.toolStripButton7.Text = "网格间隔(m)：";
            // 
            // 网格间隔
            // 
            this.网格间隔.Name = "网格间隔";
            this.网格间隔.Size = new System.Drawing.Size(50, 25);
            this.网格间隔.ToolTipText = "网格间距";
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
            // 
            // help
            // 
            this.help.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.help.Image = ((System.Drawing.Image)(resources.GetObject("help.Image")));
            this.help.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.help.Name = "help";
            this.help.Size = new System.Drawing.Size(23, 22);
            this.help.Text = "toolStripButton2";
            this.help.ToolTipText = "默认：\r\n基准高程：9 (m)\r\n网格间隔：2 (m)";
            this.help.Click += new System.EventHandler(this.help_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.数据表格,
            this.凸包图形,
            this.报告,
            this.toolStripStatusLabel1,
            this.status});
            this.statusStrip1.Location = new System.Drawing.Point(0, 470);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(668, 22);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // 数据表格
            // 
            this.数据表格.Name = "数据表格";
            this.数据表格.Size = new System.Drawing.Size(56, 17);
            this.数据表格.Text = "数据表格";
            this.数据表格.Click += new System.EventHandler(this.数据表格_Click);
            // 
            // 凸包图形
            // 
            this.凸包图形.Name = "凸包图形";
            this.凸包图形.Size = new System.Drawing.Size(56, 17);
            this.凸包图形.Text = "凸包图形";
            this.凸包图形.Click += new System.EventHandler(this.凸包图形_Click);
            // 
            // 报告
            // 
            this.报告.Name = "报告";
            this.报告.Size = new System.Drawing.Size(32, 17);
            this.报告.Text = "报告";
            this.报告.Click += new System.EventHandler(this.报告_Click);
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(132, 17);
            this.toolStripStatusLabel1.Text = "                目前状态：";
            // 
            // status
            // 
            this.status.Name = "status";
            this.status.Size = new System.Drawing.Size(56, 17);
            this.status.Text = "开启程序";
            // 
            // datatable
            // 
            this.datatable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.datatable.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.点名,
            this.X坐标,
            this.Y坐标,
            this.H高程});
            this.datatable.Location = new System.Drawing.Point(13, 54);
            this.datatable.Name = "datatable";
            this.datatable.RowTemplate.Height = 23;
            this.datatable.Size = new System.Drawing.Size(650, 400);
            this.datatable.TabIndex = 3;
            // 
            // 点名
            // 
            this.点名.HeaderText = "点名";
            this.点名.Name = "点名";
            // 
            // X坐标
            // 
            this.X坐标.HeaderText = "X坐标";
            this.X坐标.Name = "X坐标";
            // 
            // Y坐标
            // 
            this.Y坐标.HeaderText = "Y坐标";
            this.Y坐标.Name = "Y坐标";
            // 
            // H高程
            // 
            this.H高程.HeaderText = "H高程";
            this.H高程.Name = "H高程";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(-1, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(650, 400);
            this.pictureBox1.TabIndex = 4;
            this.pictureBox1.TabStop = false;
            // 
            // report
            // 
            this.report.Location = new System.Drawing.Point(13, 54);
            this.report.Multiline = true;
            this.report.Name = "report";
            this.report.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.report.Size = new System.Drawing.Size(650, 400);
            this.report.TabIndex = 5;
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Location = new System.Drawing.Point(13, 54);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(650, 400);
            this.panel1.TabIndex = 6;
            // 
            // 规则网格进行体积计算
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(668, 492);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.report);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.datatable);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "规则网格进行体积计算";
            this.Text = "规则网格进行体积计算";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.datatable)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 文件;
        private System.Windows.Forms.ToolStripMenuItem 打开文件;
        private System.Windows.Forms.ToolStripMenuItem 保存文件;
        private System.Windows.Forms.ToolStripMenuItem 退出1;
        private System.Windows.Forms.ToolStripMenuItem 计算;
        private System.Windows.Forms.ToolStripMenuItem 凸包点示意图ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 体积计算ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 退出;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel 数据表格;
        private System.Windows.Forms.ToolStripStatusLabel 凸包图形;
        private System.Windows.Forms.ToolStripStatusLabel 报告;
        private System.Windows.Forms.ToolStripButton openfile;
        private System.Windows.Forms.ToolStripButton savereport;
        private System.Windows.Forms.ToolStripButton get_V;
        private System.Windows.Forms.ToolStripButton 生成凸包;
        private System.Windows.Forms.ToolStripButton 图像放大;
        private System.Windows.Forms.ToolStripButton 图像缩小;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton toolStripButton6;
        private System.Windows.Forms.ToolStripTextBox 基准高程;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton toolStripButton7;
        private System.Windows.Forms.ToolStripTextBox 网格间隔;
        private System.Windows.Forms.DataGridView datatable;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TextBox report;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton save_DXF;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripButton help;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel status;
        private System.Windows.Forms.DataGridViewTextBoxColumn 点名;
        private System.Windows.Forms.DataGridViewTextBoxColumn X坐标;
        private System.Windows.Forms.DataGridViewTextBoxColumn Y坐标;
        private System.Windows.Forms.DataGridViewTextBoxColumn H高程;
    }
}

