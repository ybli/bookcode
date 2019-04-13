namespace WindowsFormsApp1
{
    partial class MainWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
            this.MyMenuStrip = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openDataFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveReportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
            this.savePictureToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.calculateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.calDownholeCoorAziToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.estiThroughErrToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.oneKeyProToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.checkToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dataTableToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.reportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.pointPictureToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.enlargeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.enlittleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MySaveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.MyOpenFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.MyTabControl = new System.Windows.Forms.TabControl();
            this.dataTableTabPage = new System.Windows.Forms.TabPage();
            this.DataTableGridView = new System.Windows.Forms.DataGridView();
            this.traversedata = new System.Windows.Forms.TabPage();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.reportTabPage = new System.Windows.Forms.TabPage();
            this.ReportTextBox = new System.Windows.Forms.TextBox();
            this.pointPictureTabPage = new System.Windows.Forms.TabPage();
            this.pointPictureBox = new System.Windows.Forms.PictureBox();
            this.MyToolStrip = new System.Windows.Forms.ToolStrip();
            this.OpenDataFileToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.SaveReportToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.OneKeyProToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.CheckDataTableToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.CheckReportToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.pointPictureToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.enlargeToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.enlittleToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator10 = new System.Windows.Forms.ToolStripSeparator();
            this.AboutToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.MyMenuStrip.SuspendLayout();
            this.MyTabControl.SuspendLayout();
            this.dataTableTabPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DataTableGridView)).BeginInit();
            this.traversedata.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.reportTabPage.SuspendLayout();
            this.pointPictureTabPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pointPictureBox)).BeginInit();
            this.MyToolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // MyMenuStrip
            // 
            this.MyMenuStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.MyMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.calculateToolStripMenuItem,
            this.checkToolStripMenuItem,
            this.aboutToolStripMenuItem});
            this.MyMenuStrip.Location = new System.Drawing.Point(0, 0);
            this.MyMenuStrip.Name = "MyMenuStrip";
            this.MyMenuStrip.Padding = new System.Windows.Forms.Padding(4, 2, 0, 2);
            this.MyMenuStrip.Size = new System.Drawing.Size(704, 28);
            this.MyMenuStrip.TabIndex = 0;
            this.MyMenuStrip.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openDataFileToolStripMenuItem,
            this.saveReportToolStripMenuItem,
            this.toolStripSeparator8,
            this.savePictureToolStripMenuItem,
            this.toolStripSeparator2,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(44, 24);
            this.fileToolStripMenuItem.Text = "文件";
            // 
            // openDataFileToolStripMenuItem
            // 
            this.openDataFileToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("openDataFileToolStripMenuItem.Image")));
            this.openDataFileToolStripMenuItem.Name = "openDataFileToolStripMenuItem";
            this.openDataFileToolStripMenuItem.Size = new System.Drawing.Size(184, 26);
            this.openDataFileToolStripMenuItem.Text = "打开数据文件";
            this.openDataFileToolStripMenuItem.Click += new System.EventHandler(this.OpenDataFileToolStripMenuItem_Click);
            // 
            // saveReportToolStripMenuItem
            // 
            this.saveReportToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("saveReportToolStripMenuItem.Image")));
            this.saveReportToolStripMenuItem.Name = "saveReportToolStripMenuItem";
            this.saveReportToolStripMenuItem.Size = new System.Drawing.Size(184, 26);
            this.saveReportToolStripMenuItem.Text = "保存结果报告";
            this.saveReportToolStripMenuItem.Click += new System.EventHandler(this.SaveReportToolStripMenuItem_Click);
            // 
            // toolStripSeparator8
            // 
            this.toolStripSeparator8.Name = "toolStripSeparator8";
            this.toolStripSeparator8.Size = new System.Drawing.Size(181, 6);
            // 
            // savePictureToolStripMenuItem
            // 
            this.savePictureToolStripMenuItem.Name = "savePictureToolStripMenuItem";
            this.savePictureToolStripMenuItem.Size = new System.Drawing.Size(184, 26);
            this.savePictureToolStripMenuItem.Text = "导出点位图";
            this.savePictureToolStripMenuItem.Click += new System.EventHandler(this.savePictureToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(181, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("exitToolStripMenuItem.Image")));
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(184, 26);
            this.exitToolStripMenuItem.Text = "退出";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.ExitToolStripMenuItem_Click);
            // 
            // calculateToolStripMenuItem
            // 
            this.calculateToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.calDownholeCoorAziToolStripMenuItem,
            this.estiThroughErrToolStripMenuItem,
            this.toolStripSeparator1,
            this.oneKeyProToolStripMenuItem});
            this.calculateToolStripMenuItem.Name = "calculateToolStripMenuItem";
            this.calculateToolStripMenuItem.Size = new System.Drawing.Size(44, 24);
            this.calculateToolStripMenuItem.Text = "计算";
            // 
            // calDownholeCoorAziToolStripMenuItem
            // 
            this.calDownholeCoorAziToolStripMenuItem.Name = "calDownholeCoorAziToolStripMenuItem";
            this.calDownholeCoorAziToolStripMenuItem.Size = new System.Drawing.Size(208, 22);
            this.calDownholeCoorAziToolStripMenuItem.Text = "陀螺经纬仪定向精度评定";
            this.calDownholeCoorAziToolStripMenuItem.Click += new System.EventHandler(this.CalDownholeCoorAziToolStripMenuItem_Click);
            // 
            // estiThroughErrToolStripMenuItem
            // 
            this.estiThroughErrToolStripMenuItem.Name = "estiThroughErrToolStripMenuItem";
            this.estiThroughErrToolStripMenuItem.Size = new System.Drawing.Size(208, 22);
            this.estiThroughErrToolStripMenuItem.Text = "贯通误差预计";
            this.estiThroughErrToolStripMenuItem.Click += new System.EventHandler(this.EstiThroughErrToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(205, 6);
            // 
            // oneKeyProToolStripMenuItem
            // 
            this.oneKeyProToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("oneKeyProToolStripMenuItem.Image")));
            this.oneKeyProToolStripMenuItem.Name = "oneKeyProToolStripMenuItem";
            this.oneKeyProToolStripMenuItem.Size = new System.Drawing.Size(208, 22);
            this.oneKeyProToolStripMenuItem.Text = "一键处理";
            this.oneKeyProToolStripMenuItem.Click += new System.EventHandler(this.OneKeyProToolStripMenuItem_Click);
            // 
            // checkToolStripMenuItem
            // 
            this.checkToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.dataTableToolStripMenuItem,
            this.toolStripSeparator7,
            this.reportToolStripMenuItem,
            this.toolStripSeparator6,
            this.pointPictureToolStripMenuItem,
            this.enlargeToolStripMenuItem,
            this.enlittleToolStripMenuItem});
            this.checkToolStripMenuItem.Name = "checkToolStripMenuItem";
            this.checkToolStripMenuItem.Size = new System.Drawing.Size(44, 24);
            this.checkToolStripMenuItem.Text = "查看";
            // 
            // dataTableToolStripMenuItem
            // 
            this.dataTableToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("dataTableToolStripMenuItem.Image")));
            this.dataTableToolStripMenuItem.Name = "dataTableToolStripMenuItem";
            this.dataTableToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.dataTableToolStripMenuItem.Text = "数据表格";
            this.dataTableToolStripMenuItem.Click += new System.EventHandler(this.DataTableToolStripMenuItem_Click);
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(121, 6);
            // 
            // reportToolStripMenuItem
            // 
            this.reportToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("reportToolStripMenuItem.Image")));
            this.reportToolStripMenuItem.Name = "reportToolStripMenuItem";
            this.reportToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.reportToolStripMenuItem.Text = "结果报告";
            this.reportToolStripMenuItem.Click += new System.EventHandler(this.ReportToolStripMenuItem_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(121, 6);
            // 
            // pointPictureToolStripMenuItem
            // 
            this.pointPictureToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("pointPictureToolStripMenuItem.Image")));
            this.pointPictureToolStripMenuItem.Name = "pointPictureToolStripMenuItem";
            this.pointPictureToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.pointPictureToolStripMenuItem.Text = "点位图";
            this.pointPictureToolStripMenuItem.Click += new System.EventHandler(this.PointPictureToolStripMenuItem_Click);
            // 
            // enlargeToolStripMenuItem
            // 
            this.enlargeToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("enlargeToolStripMenuItem.Image")));
            this.enlargeToolStripMenuItem.Name = "enlargeToolStripMenuItem";
            this.enlargeToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.enlargeToolStripMenuItem.Text = "放大";
            this.enlargeToolStripMenuItem.Click += new System.EventHandler(this.EnlargeToolStripMenuItem_Click);
            // 
            // enlittleToolStripMenuItem
            // 
            this.enlittleToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("enlittleToolStripMenuItem.Image")));
            this.enlittleToolStripMenuItem.Name = "enlittleToolStripMenuItem";
            this.enlittleToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.enlittleToolStripMenuItem.Text = "缩小";
            this.enlittleToolStripMenuItem.Click += new System.EventHandler(this.EnlittleToolStripMenuItem_Click);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("aboutToolStripMenuItem.Image")));
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(64, 24);
            this.aboutToolStripMenuItem.Text = "关于";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.AboutToolStripMenuItem_Click);
            // 
            // MyOpenFileDialog
            // 
            this.MyOpenFileDialog.FileName = "openFileDialog1";
            // 
            // MyTabControl
            // 
            this.MyTabControl.Alignment = System.Windows.Forms.TabAlignment.Bottom;
            this.MyTabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.MyTabControl.Controls.Add(this.dataTableTabPage);
            this.MyTabControl.Controls.Add(this.traversedata);
            this.MyTabControl.Controls.Add(this.reportTabPage);
            this.MyTabControl.Controls.Add(this.pointPictureTabPage);
            this.MyTabControl.Location = new System.Drawing.Point(0, 57);
            this.MyTabControl.Margin = new System.Windows.Forms.Padding(2);
            this.MyTabControl.Name = "MyTabControl";
            this.MyTabControl.SelectedIndex = 0;
            this.MyTabControl.Size = new System.Drawing.Size(704, 402);
            this.MyTabControl.TabIndex = 1;
            // 
            // dataTableTabPage
            // 
            this.dataTableTabPage.Controls.Add(this.DataTableGridView);
            this.dataTableTabPage.Location = new System.Drawing.Point(4, 4);
            this.dataTableTabPage.Margin = new System.Windows.Forms.Padding(2);
            this.dataTableTabPage.Name = "dataTableTabPage";
            this.dataTableTabPage.Padding = new System.Windows.Forms.Padding(2);
            this.dataTableTabPage.Size = new System.Drawing.Size(696, 376);
            this.dataTableTabPage.TabIndex = 0;
            this.dataTableTabPage.Text = "陀螺定向数据";
            this.dataTableTabPage.UseVisualStyleBackColor = true;
            // 
            // DataTableGridView
            // 
            this.DataTableGridView.AllowUserToAddRows = false;
            this.DataTableGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.DataTableGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DataTableGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DataTableGridView.Location = new System.Drawing.Point(2, 2);
            this.DataTableGridView.Margin = new System.Windows.Forms.Padding(2);
            this.DataTableGridView.Name = "DataTableGridView";
            this.DataTableGridView.RowTemplate.Height = 27;
            this.DataTableGridView.Size = new System.Drawing.Size(692, 372);
            this.DataTableGridView.TabIndex = 0;
            // 
            // traversedata
            // 
            this.traversedata.Controls.Add(this.dataGridView1);
            this.traversedata.Location = new System.Drawing.Point(4, 4);
            this.traversedata.Name = "traversedata";
            this.traversedata.Padding = new System.Windows.Forms.Padding(3);
            this.traversedata.Size = new System.Drawing.Size(696, 376);
            this.traversedata.TabIndex = 4;
            this.traversedata.Text = "导线点数据";
            this.traversedata.UseVisualStyleBackColor = true;
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(3, 3);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(690, 370);
            this.dataGridView1.TabIndex = 0;
            // 
            // reportTabPage
            // 
            this.reportTabPage.Controls.Add(this.ReportTextBox);
            this.reportTabPage.Location = new System.Drawing.Point(4, 4);
            this.reportTabPage.Margin = new System.Windows.Forms.Padding(2);
            this.reportTabPage.Name = "reportTabPage";
            this.reportTabPage.Padding = new System.Windows.Forms.Padding(2);
            this.reportTabPage.Size = new System.Drawing.Size(696, 376);
            this.reportTabPage.TabIndex = 1;
            this.reportTabPage.Text = "结果报告";
            this.reportTabPage.UseVisualStyleBackColor = true;
            // 
            // ReportTextBox
            // 
            this.ReportTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ReportTextBox.Location = new System.Drawing.Point(2, 2);
            this.ReportTextBox.Margin = new System.Windows.Forms.Padding(2);
            this.ReportTextBox.Multiline = true;
            this.ReportTextBox.Name = "ReportTextBox";
            this.ReportTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.ReportTextBox.Size = new System.Drawing.Size(692, 372);
            this.ReportTextBox.TabIndex = 0;
            // 
            // pointPictureTabPage
            // 
            this.pointPictureTabPage.Controls.Add(this.pointPictureBox);
            this.pointPictureTabPage.Location = new System.Drawing.Point(4, 4);
            this.pointPictureTabPage.Margin = new System.Windows.Forms.Padding(2);
            this.pointPictureTabPage.Name = "pointPictureTabPage";
            this.pointPictureTabPage.Padding = new System.Windows.Forms.Padding(2);
            this.pointPictureTabPage.Size = new System.Drawing.Size(696, 376);
            this.pointPictureTabPage.TabIndex = 3;
            this.pointPictureTabPage.Text = "图形";
            this.pointPictureTabPage.UseVisualStyleBackColor = true;
            // 
            // pointPictureBox
            // 
            this.pointPictureBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pointPictureBox.Location = new System.Drawing.Point(2, 2);
            this.pointPictureBox.Margin = new System.Windows.Forms.Padding(2);
            this.pointPictureBox.Name = "pointPictureBox";
            this.pointPictureBox.Size = new System.Drawing.Size(692, 372);
            this.pointPictureBox.TabIndex = 0;
            this.pointPictureBox.TabStop = false;
            // 
            // MyToolStrip
            // 
            this.MyToolStrip.AutoSize = false;
            this.MyToolStrip.ImageScalingSize = new System.Drawing.Size(35, 35);
            this.MyToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.OpenDataFileToolStripButton,
            this.SaveReportToolStripButton,
            this.toolStripSeparator3,
            this.OneKeyProToolStripButton,
            this.toolStripSeparator4,
            this.CheckDataTableToolStripButton,
            this.CheckReportToolStripButton,
            this.toolStripSeparator5,
            this.pointPictureToolStripButton,
            this.enlargeToolStripButton,
            this.enlittleToolStripButton,
            this.toolStripSeparator10,
            this.AboutToolStripButton});
            this.MyToolStrip.Location = new System.Drawing.Point(0, 28);
            this.MyToolStrip.Name = "MyToolStrip";
            this.MyToolStrip.Size = new System.Drawing.Size(704, 32);
            this.MyToolStrip.TabIndex = 2;
            this.MyToolStrip.Text = "工具栏";
            this.MyToolStrip.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.MyToolStrip_ItemClicked);
            // 
            // OpenDataFileToolStripButton
            // 
            this.OpenDataFileToolStripButton.AutoSize = false;
            this.OpenDataFileToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.OpenDataFileToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("OpenDataFileToolStripButton.Image")));
            this.OpenDataFileToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.OpenDataFileToolStripButton.Name = "OpenDataFileToolStripButton";
            this.OpenDataFileToolStripButton.Size = new System.Drawing.Size(36, 36);
            this.OpenDataFileToolStripButton.Text = "打开数据文件";
            this.OpenDataFileToolStripButton.ToolTipText = "打开数据文件";
            this.OpenDataFileToolStripButton.Click += new System.EventHandler(this.OpenDataFileToolStripButton_Click);
            // 
            // SaveReportToolStripButton
            // 
            this.SaveReportToolStripButton.AutoSize = false;
            this.SaveReportToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.SaveReportToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("SaveReportToolStripButton.Image")));
            this.SaveReportToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.SaveReportToolStripButton.Name = "SaveReportToolStripButton";
            this.SaveReportToolStripButton.Size = new System.Drawing.Size(36, 36);
            this.SaveReportToolStripButton.Text = "保存结果报告";
            this.SaveReportToolStripButton.ToolTipText = "保存结果报告";
            this.SaveReportToolStripButton.Click += new System.EventHandler(this.SaveReportToolStripButton_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 32);
            // 
            // OneKeyProToolStripButton
            // 
            this.OneKeyProToolStripButton.AutoSize = false;
            this.OneKeyProToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.OneKeyProToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("OneKeyProToolStripButton.Image")));
            this.OneKeyProToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.OneKeyProToolStripButton.Name = "OneKeyProToolStripButton";
            this.OneKeyProToolStripButton.Size = new System.Drawing.Size(36, 36);
            this.OneKeyProToolStripButton.Text = "一键处理";
            this.OneKeyProToolStripButton.ToolTipText = "一键处理";
            this.OneKeyProToolStripButton.Click += new System.EventHandler(this.OneKeyProToolStripButton_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 32);
            // 
            // CheckDataTableToolStripButton
            // 
            this.CheckDataTableToolStripButton.AutoSize = false;
            this.CheckDataTableToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.CheckDataTableToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("CheckDataTableToolStripButton.Image")));
            this.CheckDataTableToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.CheckDataTableToolStripButton.Name = "CheckDataTableToolStripButton";
            this.CheckDataTableToolStripButton.Size = new System.Drawing.Size(36, 36);
            this.CheckDataTableToolStripButton.Text = "查看数据表格";
            this.CheckDataTableToolStripButton.ToolTipText = "查看数据表格";
            this.CheckDataTableToolStripButton.Click += new System.EventHandler(this.CheckDataTableToolStripButton_Click);
            // 
            // CheckReportToolStripButton
            // 
            this.CheckReportToolStripButton.AutoSize = false;
            this.CheckReportToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.CheckReportToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("CheckReportToolStripButton.Image")));
            this.CheckReportToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.CheckReportToolStripButton.Name = "CheckReportToolStripButton";
            this.CheckReportToolStripButton.Size = new System.Drawing.Size(36, 36);
            this.CheckReportToolStripButton.Text = "查看结果报告";
            this.CheckReportToolStripButton.ToolTipText = "查看结果报告";
            this.CheckReportToolStripButton.Click += new System.EventHandler(this.CheckReportToolStripButton_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 32);
            // 
            // pointPictureToolStripButton
            // 
            this.pointPictureToolStripButton.AutoSize = false;
            this.pointPictureToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.pointPictureToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("pointPictureToolStripButton.Image")));
            this.pointPictureToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.pointPictureToolStripButton.Name = "pointPictureToolStripButton";
            this.pointPictureToolStripButton.Size = new System.Drawing.Size(36, 36);
            this.pointPictureToolStripButton.Text = "查看图形";
            this.pointPictureToolStripButton.ToolTipText = "关于";
            this.pointPictureToolStripButton.Click += new System.EventHandler(this.PointPictureToolStripButton_Click);
            // 
            // enlargeToolStripButton
            // 
            this.enlargeToolStripButton.AutoSize = false;
            this.enlargeToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.enlargeToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("enlargeToolStripButton.Image")));
            this.enlargeToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.enlargeToolStripButton.Name = "enlargeToolStripButton";
            this.enlargeToolStripButton.Size = new System.Drawing.Size(36, 36);
            this.enlargeToolStripButton.Text = "关于";
            this.enlargeToolStripButton.ToolTipText = "关于";
            this.enlargeToolStripButton.Click += new System.EventHandler(this.EnlargeToolStripButton_Click);
            // 
            // enlittleToolStripButton
            // 
            this.enlittleToolStripButton.AutoSize = false;
            this.enlittleToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.enlittleToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("enlittleToolStripButton.Image")));
            this.enlittleToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.enlittleToolStripButton.Name = "enlittleToolStripButton";
            this.enlittleToolStripButton.Size = new System.Drawing.Size(36, 36);
            this.enlittleToolStripButton.Text = "关于";
            this.enlittleToolStripButton.ToolTipText = "关于";
            this.enlittleToolStripButton.Click += new System.EventHandler(this.EnlittleToolStripButton_Click);
            // 
            // toolStripSeparator10
            // 
            this.toolStripSeparator10.Name = "toolStripSeparator10";
            this.toolStripSeparator10.Size = new System.Drawing.Size(6, 32);
            // 
            // AboutToolStripButton
            // 
            this.AboutToolStripButton.AutoSize = false;
            this.AboutToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.AboutToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("AboutToolStripButton.Image")));
            this.AboutToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.AboutToolStripButton.Name = "AboutToolStripButton";
            this.AboutToolStripButton.Size = new System.Drawing.Size(36, 36);
            this.AboutToolStripButton.Text = "关于";
            this.AboutToolStripButton.ToolTipText = "关于";
            this.AboutToolStripButton.Click += new System.EventHandler(this.AboutToolStripButton_Click);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(704, 468);
            this.Controls.Add(this.MyToolStrip);
            this.Controls.Add(this.MyTabControl);
            this.Controls.Add(this.MyMenuStrip);
            this.MainMenuStrip = this.MyMenuStrip;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "MainWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "贯通测量误差预计";
            this.MyMenuStrip.ResumeLayout(false);
            this.MyMenuStrip.PerformLayout();
            this.MyTabControl.ResumeLayout(false);
            this.dataTableTabPage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DataTableGridView)).EndInit();
            this.traversedata.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.reportTabPage.ResumeLayout(false);
            this.reportTabPage.PerformLayout();
            this.pointPictureTabPage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pointPictureBox)).EndInit();
            this.MyToolStrip.ResumeLayout(false);
            this.MyToolStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip MyMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openDataFileToolStripMenuItem;
        private System.Windows.Forms.SaveFileDialog MySaveFileDialog;
        private System.Windows.Forms.OpenFileDialog MyOpenFileDialog;
        private System.Windows.Forms.TabControl MyTabControl;
        private System.Windows.Forms.TabPage dataTableTabPage;
        private System.Windows.Forms.TabPage reportTabPage;
        private System.Windows.Forms.DataGridView DataTableGridView;
        private System.Windows.Forms.TextBox ReportTextBox;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem calculateToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStrip MyToolStrip;
        private System.Windows.Forms.ToolStripMenuItem saveReportToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem calDownholeCoorAziToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem oneKeyProToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton OpenDataFileToolStripButton;
        private System.Windows.Forms.ToolStripButton SaveReportToolStripButton;
        private System.Windows.Forms.ToolStripButton OneKeyProToolStripButton;
        private System.Windows.Forms.ToolStripButton CheckDataTableToolStripButton;
        private System.Windows.Forms.ToolStripButton CheckReportToolStripButton;
        private System.Windows.Forms.ToolStripButton AboutToolStripButton;
        private System.Windows.Forms.ToolStripMenuItem checkToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dataTableToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem reportToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripMenuItem estiThroughErrToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator8;
        private System.Windows.Forms.ToolStripMenuItem savePictureToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripMenuItem pointPictureToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem enlargeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem enlittleToolStripMenuItem;
        private System.Windows.Forms.TabPage pointPictureTabPage;
        private System.Windows.Forms.PictureBox pointPictureBox;
        private System.Windows.Forms.ToolStripButton pointPictureToolStripButton;
        private System.Windows.Forms.ToolStripButton enlargeToolStripButton;
        private System.Windows.Forms.ToolStripButton enlittleToolStripButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator10;
        private System.Windows.Forms.TabPage traversedata;
        private System.Windows.Forms.DataGridView dataGridView1;
    }
}

