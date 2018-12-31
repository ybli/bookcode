namespace 基于hausdorff_Frechet距离的线匹配算法
{
    partial class 基于Hausdorff_Frechet距离的线匹配
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(基于Hausdorff_Frechet距离的线匹配));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.文件ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.读取shpfile文件ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.保存报告ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.退出ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.处理ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.基于传统Hausdorff距离 = new System.Windows.Forms.ToolStripMenuItem();
            this.基于离散Frechet距离 = new System.Windows.Forms.ToolStripMenuItem();
            this.基于改进的Hausdorff_SMHD距离 = new System.Windows.Forms.ToolStripMenuItem();
            this.基于平均Frechet距离ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.一键生成全部ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.结果评价ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.读入参考数据 = new System.Windows.Forms.ToolStripMenuItem();
            this.评价 = new System.Windows.Forms.ToolStripMenuItem();
            this.图像ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.形成图像ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.保存图像ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.退出程序 = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.DATA_window = new System.Windows.Forms.ToolStripStatusLabel();
            this.IMAGE_window = new System.Windows.Forms.ToolStripStatusLabel();
            this.REPORT_window = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel4 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel5 = new System.Windows.Forms.ToolStripStatusLabel();
            this.status_bar = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.open_shpfile = new System.Windows.Forms.ToolStripButton();
            this.绘制图形 = new System.Windows.Forms.ToolStripButton();
            this.save_image = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.report = new System.Windows.Forms.ToolStripButton();
            this.save_report = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.结果评价 = new System.Windows.Forms.ToolStripButton();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.threshold_value = new System.Windows.Forms.ToolStripTextBox();
            this.Help = new System.Windows.Forms.ToolStripButton();
            this.DATA_shpLine1 = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DATA_shpLine2 = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DATA_show = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.REPORT_show = new System.Windows.Forms.TextBox();
            this.image_show = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.image_redraw = new System.Windows.Forms.Button();
            this.image_unchang = new System.Windows.Forms.Button();
            this.image_small = new System.Windows.Forms.Button();
            this.image_big = new System.Windows.Forms.Button();
            this.checkedListBox1 = new System.Windows.Forms.CheckedListBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DATA_shpLine1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DATA_shpLine2)).BeginInit();
            this.DATA_show.SuspendLayout();
            this.image_show.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.文件ToolStripMenuItem,
            this.处理ToolStripMenuItem,
            this.结果评价ToolStripMenuItem,
            this.图像ToolStripMenuItem,
            this.退出程序});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(970, 25);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 文件ToolStripMenuItem
            // 
            this.文件ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.读取shpfile文件ToolStripMenuItem,
            this.保存报告ToolStripMenuItem,
            this.退出ToolStripMenuItem});
            this.文件ToolStripMenuItem.Name = "文件ToolStripMenuItem";
            this.文件ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.文件ToolStripMenuItem.Text = "文件";
            // 
            // 读取shpfile文件ToolStripMenuItem
            // 
            this.读取shpfile文件ToolStripMenuItem.Name = "读取shpfile文件ToolStripMenuItem";
            this.读取shpfile文件ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.读取shpfile文件ToolStripMenuItem.Text = "读取文件";
            this.读取shpfile文件ToolStripMenuItem.Click += new System.EventHandler(this.读取文件ToolStripMenuItem_Click);
            // 
            // 保存报告ToolStripMenuItem
            // 
            this.保存报告ToolStripMenuItem.Name = "保存报告ToolStripMenuItem";
            this.保存报告ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.保存报告ToolStripMenuItem.Text = "保存报告";
            this.保存报告ToolStripMenuItem.Click += new System.EventHandler(this.保存报告ToolStripMenuItem_Click);
            // 
            // 退出ToolStripMenuItem
            // 
            this.退出ToolStripMenuItem.Name = "退出ToolStripMenuItem";
            this.退出ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.退出ToolStripMenuItem.Text = "退出";
            this.退出ToolStripMenuItem.Click += new System.EventHandler(this.退出ToolStripMenuItem_Click);
            // 
            // 处理ToolStripMenuItem
            // 
            this.处理ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.基于传统Hausdorff距离,
            this.基于离散Frechet距离,
            this.基于改进的Hausdorff_SMHD距离,
            this.基于平均Frechet距离ToolStripMenuItem,
            this.一键生成全部ToolStripMenuItem});
            this.处理ToolStripMenuItem.Name = "处理ToolStripMenuItem";
            this.处理ToolStripMenuItem.Size = new System.Drawing.Size(56, 21);
            this.处理ToolStripMenuItem.Text = "线匹配";
            // 
            // 基于传统Hausdorff距离
            // 
            this.基于传统Hausdorff距离.Name = "基于传统Hausdorff距离";
            this.基于传统Hausdorff距离.Size = new System.Drawing.Size(263, 22);
            this.基于传统Hausdorff距离.Text = "基于传统Hausdorff距离";
            this.基于传统Hausdorff距离.Click += new System.EventHandler(this.基于传统Hausdorff距离_Click);
            // 
            // 基于离散Frechet距离
            // 
            this.基于离散Frechet距离.Name = "基于离散Frechet距离";
            this.基于离散Frechet距离.Size = new System.Drawing.Size(263, 22);
            this.基于离散Frechet距离.Text = "基于离散Frechet距离";
            this.基于离散Frechet距离.Click += new System.EventHandler(this.基于离散Frechet距离_Click);
            // 
            // 基于改进的Hausdorff_SMHD距离
            // 
            this.基于改进的Hausdorff_SMHD距离.Name = "基于改进的Hausdorff_SMHD距离";
            this.基于改进的Hausdorff_SMHD距离.Size = new System.Drawing.Size(263, 22);
            this.基于改进的Hausdorff_SMHD距离.Text = "基于改进的Hausdorff(SMHD)距离";
            this.基于改进的Hausdorff_SMHD距离.Click += new System.EventHandler(this.基于改进的Hausdorff_SMHD距离_Click);
            // 
            // 基于平均Frechet距离ToolStripMenuItem
            // 
            this.基于平均Frechet距离ToolStripMenuItem.Name = "基于平均Frechet距离ToolStripMenuItem";
            this.基于平均Frechet距离ToolStripMenuItem.Size = new System.Drawing.Size(263, 22);
            this.基于平均Frechet距离ToolStripMenuItem.Text = "基于平均Frechet距离";
            this.基于平均Frechet距离ToolStripMenuItem.Click += new System.EventHandler(this.基于平均Frechet距离ToolStripMenuItem_Click);
            // 
            // 一键生成全部ToolStripMenuItem
            // 
            this.一键生成全部ToolStripMenuItem.Name = "一键生成全部ToolStripMenuItem";
            this.一键生成全部ToolStripMenuItem.Size = new System.Drawing.Size(263, 22);
            this.一键生成全部ToolStripMenuItem.Text = "一键生成全部";
            this.一键生成全部ToolStripMenuItem.Click += new System.EventHandler(this.一键生成全部ToolStripMenuItem_Click);
            // 
            // 结果评价ToolStripMenuItem
            // 
            this.结果评价ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.读入参考数据,
            this.评价});
            this.结果评价ToolStripMenuItem.Name = "结果评价ToolStripMenuItem";
            this.结果评价ToolStripMenuItem.Size = new System.Drawing.Size(68, 21);
            this.结果评价ToolStripMenuItem.Text = "结果评价";
            // 
            // 读入参考数据
            // 
            this.读入参考数据.Name = "读入参考数据";
            this.读入参考数据.Size = new System.Drawing.Size(148, 22);
            this.读入参考数据.Text = "读入参考数据";
            this.读入参考数据.Click += new System.EventHandler(this.读入参考数据_Click);
            // 
            // 评价
            // 
            this.评价.Name = "评价";
            this.评价.Size = new System.Drawing.Size(148, 22);
            this.评价.Text = "评价";
            this.评价.Click += new System.EventHandler(this.评价_Click);
            // 
            // 图像ToolStripMenuItem
            // 
            this.图像ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.形成图像ToolStripMenuItem,
            this.保存图像ToolStripMenuItem});
            this.图像ToolStripMenuItem.Name = "图像ToolStripMenuItem";
            this.图像ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.图像ToolStripMenuItem.Text = "图形";
            // 
            // 形成图像ToolStripMenuItem
            // 
            this.形成图像ToolStripMenuItem.Name = "形成图像ToolStripMenuItem";
            this.形成图像ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.形成图像ToolStripMenuItem.Text = "绘制图形";
            this.形成图像ToolStripMenuItem.Click += new System.EventHandler(this.绘制图像ToolStripMenuItem_Click);
            // 
            // 保存图像ToolStripMenuItem
            // 
            this.保存图像ToolStripMenuItem.Name = "保存图像ToolStripMenuItem";
            this.保存图像ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.保存图像ToolStripMenuItem.Text = "保存图形";
            this.保存图像ToolStripMenuItem.Click += new System.EventHandler(this.保存图像ToolStripMenuItem_Click);
            // 
            // 退出程序
            // 
            this.退出程序.Name = "退出程序";
            this.退出程序.Size = new System.Drawing.Size(44, 21);
            this.退出程序.Text = "退出";
            this.退出程序.Click += new System.EventHandler(this.退出程序_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.DATA_window,
            this.IMAGE_window,
            this.REPORT_window,
            this.toolStripStatusLabel4,
            this.toolStripStatusLabel5,
            this.status_bar});
            this.statusStrip1.Location = new System.Drawing.Point(0, 545);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(970, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // DATA_window
            // 
            this.DATA_window.Name = "DATA_window";
            this.DATA_window.Size = new System.Drawing.Size(56, 17);
            this.DATA_window.Text = "数据窗口";
            this.DATA_window.Click += new System.EventHandler(this.DATA_window_Click);
            // 
            // IMAGE_window
            // 
            this.IMAGE_window.Name = "IMAGE_window";
            this.IMAGE_window.Size = new System.Drawing.Size(56, 17);
            this.IMAGE_window.Text = "图形窗口";
            this.IMAGE_window.Click += new System.EventHandler(this.IMAGE_window_Click);
            // 
            // REPORT_window
            // 
            this.REPORT_window.Name = "REPORT_window";
            this.REPORT_window.Size = new System.Drawing.Size(56, 17);
            this.REPORT_window.Text = "报告窗口";
            this.REPORT_window.Click += new System.EventHandler(this.REPORT_window_Click);
            // 
            // toolStripStatusLabel4
            // 
            this.toolStripStatusLabel4.Name = "toolStripStatusLabel4";
            this.toolStripStatusLabel4.Size = new System.Drawing.Size(124, 17);
            this.toolStripStatusLabel4.Text = "             当前状态： ";
            // 
            // toolStripStatusLabel5
            // 
            this.toolStripStatusLabel5.Name = "toolStripStatusLabel5";
            this.toolStripStatusLabel5.Size = new System.Drawing.Size(0, 17);
            // 
            // status_bar
            // 
            this.status_bar.Name = "status_bar";
            this.status_bar.Size = new System.Drawing.Size(56, 17);
            this.status_bar.Text = "软件启动";
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.open_shpfile,
            this.绘制图形,
            this.save_image,
            this.toolStripSeparator1,
            this.report,
            this.save_report,
            this.toolStripSeparator2,
            this.结果评价,
            this.toolStripLabel1,
            this.threshold_value,
            this.Help});
            this.toolStrip1.Location = new System.Drawing.Point(0, 25);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(970, 25);
            this.toolStrip1.TabIndex = 2;
            this.toolStrip1.Text = "读取文件";
            // 
            // open_shpfile
            // 
            this.open_shpfile.Image = ((System.Drawing.Image)(resources.GetObject("open_shpfile.Image")));
            this.open_shpfile.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.open_shpfile.Name = "open_shpfile";
            this.open_shpfile.Size = new System.Drawing.Size(76, 22);
            this.open_shpfile.Text = "读取文件";
            this.open_shpfile.Click += new System.EventHandler(this.open_shpfile_Click);
            // 
            // 绘制图形
            // 
            this.绘制图形.Image = ((System.Drawing.Image)(resources.GetObject("绘制图形.Image")));
            this.绘制图形.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.绘制图形.Name = "绘制图形";
            this.绘制图形.Size = new System.Drawing.Size(76, 22);
            this.绘制图形.Text = "绘制图形";
            this.绘制图形.Click += new System.EventHandler(this.绘制图形_Click);
            // 
            // save_image
            // 
            this.save_image.Image = ((System.Drawing.Image)(resources.GetObject("save_image.Image")));
            this.save_image.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.save_image.Name = "save_image";
            this.save_image.Size = new System.Drawing.Size(76, 22);
            this.save_image.Text = "保存图形";
            this.save_image.Click += new System.EventHandler(this.save_image_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // report
            // 
            this.report.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.report.Image = ((System.Drawing.Image)(resources.GetObject("report.Image")));
            this.report.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.report.Name = "report";
            this.report.Size = new System.Drawing.Size(60, 22);
            this.report.Text = "生成报告";
            this.report.Click += new System.EventHandler(this.report_Click);
            // 
            // save_report
            // 
            this.save_report.Image = ((System.Drawing.Image)(resources.GetObject("save_report.Image")));
            this.save_report.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.save_report.Name = "save_report";
            this.save_report.Size = new System.Drawing.Size(76, 22);
            this.save_report.Text = "保存报告";
            this.save_report.Click += new System.EventHandler(this.save_report_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // 结果评价
            // 
            this.结果评价.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.结果评价.Image = ((System.Drawing.Image)(resources.GetObject("结果评价.Image")));
            this.结果评价.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.结果评价.Name = "结果评价";
            this.结果评价.Size = new System.Drawing.Size(60, 22);
            this.结果评价.Text = "结果评价";
            this.结果评价.Click += new System.EventHandler(this.结果评价_Click);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(92, 22);
            this.toolStripLabel1.Text = "线匹配的阈值：";
            // 
            // threshold_value
            // 
            this.threshold_value.Name = "threshold_value";
            this.threshold_value.Size = new System.Drawing.Size(40, 25);
            this.threshold_value.Text = "30";
            // 
            // Help
            // 
            this.Help.AutoSize = false;
            this.Help.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.Help.Image = ((System.Drawing.Image)(resources.GetObject("Help.Image")));
            this.Help.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Help.Name = "Help";
            this.Help.Size = new System.Drawing.Size(23, 22);
            this.Help.Text = "帮助说明";
            this.Help.Click += new System.EventHandler(this.Help_Click);
            // 
            // DATA_shpLine1
            // 
            this.DATA_shpLine1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DATA_shpLine1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4});
            this.DATA_shpLine1.Location = new System.Drawing.Point(6, 20);
            this.DATA_shpLine1.Name = "DATA_shpLine1";
            this.DATA_shpLine1.RowTemplate.Height = 23;
            this.DATA_shpLine1.Size = new System.Drawing.Size(443, 463);
            this.DATA_shpLine1.TabIndex = 3;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "折段起点X";
            this.Column1.Name = "Column1";
            // 
            // Column2
            // 
            this.Column2.HeaderText = "折段起点Y";
            this.Column2.Name = "Column2";
            // 
            // Column3
            // 
            this.Column3.HeaderText = "折段终点X";
            this.Column3.Name = "Column3";
            // 
            // Column4
            // 
            this.Column4.HeaderText = "折段终点Y";
            this.Column4.Name = "Column4";
            // 
            // DATA_shpLine2
            // 
            this.DATA_shpLine2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DATA_shpLine2.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewTextBoxColumn3,
            this.dataGridViewTextBoxColumn4});
            this.DATA_shpLine2.Location = new System.Drawing.Point(455, 20);
            this.DATA_shpLine2.Name = "DATA_shpLine2";
            this.DATA_shpLine2.RowTemplate.Height = 23;
            this.DATA_shpLine2.Size = new System.Drawing.Size(445, 463);
            this.DATA_shpLine2.TabIndex = 4;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "折段起点X";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.HeaderText = "折段起点Y";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.HeaderText = "折段终点X";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.HeaderText = "折段终点Y";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            // 
            // DATA_show
            // 
            this.DATA_show.Controls.Add(this.label2);
            this.DATA_show.Controls.Add(this.label1);
            this.DATA_show.Controls.Add(this.DATA_shpLine1);
            this.DATA_show.Controls.Add(this.DATA_shpLine2);
            this.DATA_show.Location = new System.Drawing.Point(12, 53);
            this.DATA_show.Name = "DATA_show";
            this.DATA_show.Size = new System.Drawing.Size(912, 489);
            this.DATA_show.TabIndex = 5;
            this.DATA_show.TabStop = false;
            this.DATA_show.Text = "图层信息";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(622, 5);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 12);
            this.label2.TabIndex = 6;
            this.label2.Text = "线图层2";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(193, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 12);
            this.label1.TabIndex = 5;
            this.label1.Text = "线图层1";
            // 
            // REPORT_show
            // 
            this.REPORT_show.Location = new System.Drawing.Point(12, 53);
            this.REPORT_show.Multiline = true;
            this.REPORT_show.Name = "REPORT_show";
            this.REPORT_show.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.REPORT_show.Size = new System.Drawing.Size(912, 489);
            this.REPORT_show.TabIndex = 6;
            // 
            // image_show
            // 
            this.image_show.Controls.Add(this.label4);
            this.image_show.Controls.Add(this.label3);
            this.image_show.Controls.Add(this.image_redraw);
            this.image_show.Controls.Add(this.image_unchang);
            this.image_show.Controls.Add(this.image_small);
            this.image_show.Controls.Add(this.image_big);
            this.image_show.Controls.Add(this.checkedListBox1);
            this.image_show.Controls.Add(this.panel1);
            this.image_show.Location = new System.Drawing.Point(12, 53);
            this.image_show.Name = "image_show";
            this.image_show.Size = new System.Drawing.Size(912, 489);
            this.image_show.TabIndex = 8;
            this.image_show.TabStop = false;
            this.image_show.Text = "图像显示";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Red;
            this.label4.Location = new System.Drawing.Point(848, 47);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(35, 12);
            this.label4.TabIndex = 7;
            this.label4.Text = "图层2";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Blue;
            this.label3.Location = new System.Drawing.Point(848, 35);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 12);
            this.label3.TabIndex = 6;
            this.label3.Text = "图层1";
            // 
            // image_redraw
            // 
            this.image_redraw.Location = new System.Drawing.Point(843, 113);
            this.image_redraw.Name = "image_redraw";
            this.image_redraw.Size = new System.Drawing.Size(63, 23);
            this.image_redraw.TabIndex = 5;
            this.image_redraw.Text = "图形重绘";
            this.image_redraw.UseVisualStyleBackColor = true;
            this.image_redraw.Click += new System.EventHandler(this.image_redraw_Click);
            // 
            // image_unchang
            // 
            this.image_unchang.Location = new System.Drawing.Point(843, 345);
            this.image_unchang.Name = "image_unchang";
            this.image_unchang.Size = new System.Drawing.Size(63, 23);
            this.image_unchang.TabIndex = 4;
            this.image_unchang.Text = "图形复原";
            this.image_unchang.UseVisualStyleBackColor = true;
            this.image_unchang.Click += new System.EventHandler(this.image_unchang_Click);
            // 
            // image_small
            // 
            this.image_small.Location = new System.Drawing.Point(843, 315);
            this.image_small.Name = "image_small";
            this.image_small.Size = new System.Drawing.Size(63, 23);
            this.image_small.TabIndex = 3;
            this.image_small.Text = "图形缩小";
            this.image_small.UseVisualStyleBackColor = true;
            this.image_small.Click += new System.EventHandler(this.image_small_Click);
            // 
            // image_big
            // 
            this.image_big.Location = new System.Drawing.Point(843, 286);
            this.image_big.Name = "image_big";
            this.image_big.Size = new System.Drawing.Size(63, 23);
            this.image_big.TabIndex = 2;
            this.image_big.Text = "图形放大";
            this.image_big.UseVisualStyleBackColor = true;
            this.image_big.Click += new System.EventHandler(this.image_big_Click);
            // 
            // checkedListBox1
            // 
            this.checkedListBox1.CheckOnClick = true;
            this.checkedListBox1.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.checkedListBox1.FormattingEnabled = true;
            this.checkedListBox1.Items.AddRange(new object[] {
            "图层1",
            "图层2"});
            this.checkedListBox1.Location = new System.Drawing.Point(842, 71);
            this.checkedListBox1.Name = "checkedListBox1";
            this.checkedListBox1.Size = new System.Drawing.Size(64, 36);
            this.checkedListBox1.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Location = new System.Drawing.Point(7, 20);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(835, 470);
            this.panel1.TabIndex = 0;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(835, 464);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // 基于Hausdorff_Frechet距离的线匹配
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(970, 567);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.REPORT_show);
            this.Controls.Add(this.image_show);
            this.Controls.Add(this.DATA_show);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "基于Hausdorff_Frechet距离的线匹配";
            this.Text = "基于几何距离的线匹配";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DATA_shpLine1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DATA_shpLine2)).EndInit();
            this.DATA_show.ResumeLayout(false);
            this.DATA_show.PerformLayout();
            this.image_show.ResumeLayout(false);
            this.image_show.PerformLayout();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 文件ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 读取shpfile文件ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 保存报告ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 退出ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 处理ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 基于传统Hausdorff距离;
        private System.Windows.Forms.ToolStripMenuItem 基于离散Frechet距离;
        private System.Windows.Forms.ToolStripMenuItem 基于改进的Hausdorff_SMHD距离;
        private System.Windows.Forms.ToolStripMenuItem 图像ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 形成图像ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 保存图像ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 退出程序;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel DATA_window;
        private System.Windows.Forms.ToolStripStatusLabel IMAGE_window;
        private System.Windows.Forms.ToolStripStatusLabel REPORT_window;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel4;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel5;
        private System.Windows.Forms.ToolStripStatusLabel status_bar;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton open_shpfile;
        private System.Windows.Forms.ToolStripButton save_report;
        private System.Windows.Forms.DataGridView DATA_shpLine1;
        private System.Windows.Forms.DataGridView DATA_shpLine2;
        private System.Windows.Forms.GroupBox DATA_show;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox REPORT_show;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.ToolStripButton 绘制图形;
        private System.Windows.Forms.GroupBox image_show;
        private System.Windows.Forms.CheckedListBox checkedListBox1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button image_small;
        private System.Windows.Forms.Button image_big;
        private System.Windows.Forms.Button image_unchang;
        private System.Windows.Forms.Button image_redraw;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton report;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton save_image;
        private System.Windows.Forms.ToolStripButton Help;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripTextBox threshold_value;
        private System.Windows.Forms.ToolStripMenuItem 基于平均Frechet距离ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 一键生成全部ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 结果评价ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 读入参考数据;
        private System.Windows.Forms.ToolStripMenuItem 评价;
        private System.Windows.Forms.ToolStripButton 结果评价;
    }
}

