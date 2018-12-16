namespace CoorTrans
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea6 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend6 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series6 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.MenuFile = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuFileOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.MenuFileSaveReport = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuFileSaveChart = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuFileSaveXls = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuFileSaveDXF = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.MenuExit = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuAlgo = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuBLH2XYZ = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuXYZ2BLH = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.MenuBL2xy = new System.Windows.Forms.ToolStripMenuItem();
            this.Menuxy2BL = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.MenuDoALL = new System.Windows.Forms.ToolStripMenuItem();
            this.查看ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuChart = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuReport = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.MenuZoomIn = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuZoomOut = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolOpenData = new System.Windows.Forms.ToolStripButton();
            this.toolSaveReport = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolBLH2XYZ = new System.Windows.Forms.ToolStripButton();
            this.toolXYZ2BLH = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.toolBL2xy = new System.Windows.Forms.ToolStripButton();
            this.toolxy2BL = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
            this.toolChart = new System.Windows.Forms.ToolStripButton();
            this.toolReport = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator9 = new System.Windows.Forms.ToolStripSeparator();
            this.toolZoomIn = new System.Windows.Forms.ToolStripButton();
            this.toolZoomout = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator10 = new System.Windows.Forms.ToolStripSeparator();
            this.toolHelp = new System.Windows.Forms.ToolStripButton();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.menuStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            this.tabPage3.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuFile,
            this.MenuAlgo,
            this.查看ToolStripMenuItem,
            this.MenuHelp});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(978, 32);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // MenuFile
            // 
            this.MenuFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuFileOpen,
            this.toolStripSeparator1,
            this.MenuFileSaveReport,
            this.MenuFileSaveChart,
            this.MenuFileSaveXls,
            this.MenuFileSaveDXF,
            this.toolStripSeparator2,
            this.MenuExit});
            this.MenuFile.Image = ((System.Drawing.Image)(resources.GetObject("MenuFile.Image")));
            this.MenuFile.Name = "MenuFile";
            this.MenuFile.Size = new System.Drawing.Size(104, 28);
            this.MenuFile.Text = "文件(&F)";
            this.MenuFile.Click += new System.EventHandler(this.MenuFile_Click);
            // 
            // MenuFileOpen
            // 
            this.MenuFileOpen.Image = ((System.Drawing.Image)(resources.GetObject("MenuFileOpen.Image")));
            this.MenuFileOpen.Name = "MenuFileOpen";
            this.MenuFileOpen.Size = new System.Drawing.Size(226, 30);
            this.MenuFileOpen.Text = "打开数据文件(&D)";
            this.MenuFileOpen.Click += new System.EventHandler(this.MenuFileOpen_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(223, 6);
            // 
            // MenuFileSaveReport
            // 
            this.MenuFileSaveReport.Image = ((System.Drawing.Image)(resources.GetObject("MenuFileSaveReport.Image")));
            this.MenuFileSaveReport.Name = "MenuFileSaveReport";
            this.MenuFileSaveReport.Size = new System.Drawing.Size(226, 30);
            this.MenuFileSaveReport.Text = "保存报告（&S）";
            this.MenuFileSaveReport.Click += new System.EventHandler(this.MenuFileSaveReport_Click);
            // 
            // MenuFileSaveChart
            // 
            this.MenuFileSaveChart.Name = "MenuFileSaveChart";
            this.MenuFileSaveChart.Size = new System.Drawing.Size(226, 30);
            this.MenuFileSaveChart.Text = "保存图形";
            this.MenuFileSaveChart.Click += new System.EventHandler(this.MenuFileSaveChart_Click);
            // 
            // MenuFileSaveXls
            // 
            this.MenuFileSaveXls.Name = "MenuFileSaveXls";
            this.MenuFileSaveXls.Size = new System.Drawing.Size(226, 30);
            this.MenuFileSaveXls.Text = "保存报表";
            this.MenuFileSaveXls.Click += new System.EventHandler(this.MenuFileSaveXls_Click);
            // 
            // MenuFileSaveDXF
            // 
            this.MenuFileSaveDXF.Name = "MenuFileSaveDXF";
            this.MenuFileSaveDXF.Size = new System.Drawing.Size(226, 30);
            this.MenuFileSaveDXF.Text = "输出DXF文件";
            this.MenuFileSaveDXF.Click += new System.EventHandler(this.MenuFileSaveDXF_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(223, 6);
            // 
            // MenuExit
            // 
            this.MenuExit.Image = ((System.Drawing.Image)(resources.GetObject("MenuExit.Image")));
            this.MenuExit.Name = "MenuExit";
            this.MenuExit.Size = new System.Drawing.Size(226, 30);
            this.MenuExit.Text = "退出（&X）";
            this.MenuExit.Click += new System.EventHandler(this.MenuExit_Click);
            // 
            // MenuAlgo
            // 
            this.MenuAlgo.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuBLH2XYZ,
            this.MenuXYZ2BLH,
            this.toolStripSeparator4,
            this.MenuBL2xy,
            this.Menuxy2BL,
            this.toolStripSeparator5,
            this.MenuDoALL});
            this.MenuAlgo.Image = ((System.Drawing.Image)(resources.GetObject("MenuAlgo.Image")));
            this.MenuAlgo.Name = "MenuAlgo";
            this.MenuAlgo.Size = new System.Drawing.Size(106, 28);
            this.MenuAlgo.Text = "计算(&C)";
            // 
            // MenuBLH2XYZ
            // 
            this.MenuBLH2XYZ.Image = ((System.Drawing.Image)(resources.GetObject("MenuBLH2XYZ.Image")));
            this.MenuBLH2XYZ.Name = "MenuBLH2XYZ";
            this.MenuBLH2XYZ.Size = new System.Drawing.Size(210, 30);
            this.MenuBLH2XYZ.Text = "BLH->XYZ";
            this.MenuBLH2XYZ.Click += new System.EventHandler(this.MenuBLH2XYZ_Click);
            // 
            // MenuXYZ2BLH
            // 
            this.MenuXYZ2BLH.Image = ((System.Drawing.Image)(resources.GetObject("MenuXYZ2BLH.Image")));
            this.MenuXYZ2BLH.Name = "MenuXYZ2BLH";
            this.MenuXYZ2BLH.Size = new System.Drawing.Size(210, 30);
            this.MenuXYZ2BLH.Text = "XYZ->BLH";
            this.MenuXYZ2BLH.Click += new System.EventHandler(this.MenuXYZ2BLH_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(207, 6);
            // 
            // MenuBL2xy
            // 
            this.MenuBL2xy.Image = ((System.Drawing.Image)(resources.GetObject("MenuBL2xy.Image")));
            this.MenuBL2xy.Name = "MenuBL2xy";
            this.MenuBL2xy.Size = new System.Drawing.Size(210, 30);
            this.MenuBL2xy.Text = "BL->xy";
            this.MenuBL2xy.Click += new System.EventHandler(this.MenuBL2xy_Click);
            // 
            // Menuxy2BL
            // 
            this.Menuxy2BL.Image = ((System.Drawing.Image)(resources.GetObject("Menuxy2BL.Image")));
            this.Menuxy2BL.Name = "Menuxy2BL";
            this.Menuxy2BL.Size = new System.Drawing.Size(210, 30);
            this.Menuxy2BL.Text = "xy->BL";
            this.Menuxy2BL.Click += new System.EventHandler(this.Menuxy2BL_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(207, 6);
            // 
            // MenuDoALL
            // 
            this.MenuDoALL.Name = "MenuDoALL";
            this.MenuDoALL.Size = new System.Drawing.Size(210, 30);
            this.MenuDoALL.Text = "一键处理";
            this.MenuDoALL.Click += new System.EventHandler(this.MenuDoALL_Click);
            // 
            // 查看ToolStripMenuItem
            // 
            this.查看ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuChart,
            this.MenuReport,
            this.toolStripSeparator6,
            this.MenuZoomIn,
            this.MenuZoomOut});
            this.查看ToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("查看ToolStripMenuItem.Image")));
            this.查看ToolStripMenuItem.Name = "查看ToolStripMenuItem";
            this.查看ToolStripMenuItem.Size = new System.Drawing.Size(82, 28);
            this.查看ToolStripMenuItem.Text = "查看";
            // 
            // MenuChart
            // 
            this.MenuChart.Image = ((System.Drawing.Image)(resources.GetObject("MenuChart.Image")));
            this.MenuChart.Name = "MenuChart";
            this.MenuChart.Size = new System.Drawing.Size(210, 30);
            this.MenuChart.Text = "点位图";
            this.MenuChart.Click += new System.EventHandler(this.MenuChart_Click);
            // 
            // MenuReport
            // 
            this.MenuReport.Image = ((System.Drawing.Image)(resources.GetObject("MenuReport.Image")));
            this.MenuReport.Name = "MenuReport";
            this.MenuReport.Size = new System.Drawing.Size(210, 30);
            this.MenuReport.Text = "报告";
            this.MenuReport.Click += new System.EventHandler(this.MenuReport_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(207, 6);
            // 
            // MenuZoomIn
            // 
            this.MenuZoomIn.Image = ((System.Drawing.Image)(resources.GetObject("MenuZoomIn.Image")));
            this.MenuZoomIn.Name = "MenuZoomIn";
            this.MenuZoomIn.Size = new System.Drawing.Size(210, 30);
            this.MenuZoomIn.Text = "放大";
            this.MenuZoomIn.Click += new System.EventHandler(this.MenuZoomIn_Click);
            // 
            // MenuZoomOut
            // 
            this.MenuZoomOut.Image = ((System.Drawing.Image)(resources.GetObject("MenuZoomOut.Image")));
            this.MenuZoomOut.Name = "MenuZoomOut";
            this.MenuZoomOut.Size = new System.Drawing.Size(210, 30);
            this.MenuZoomOut.Text = "缩小";
            this.MenuZoomOut.Click += new System.EventHandler(this.MenuZoomOut_Click);
            // 
            // MenuHelp
            // 
            this.MenuHelp.Image = ((System.Drawing.Image)(resources.GetObject("MenuHelp.Image")));
            this.MenuHelp.Name = "MenuHelp";
            this.MenuHelp.Size = new System.Drawing.Size(108, 28);
            this.MenuHelp.Text = "帮助(H)";
            this.MenuHelp.Click += new System.EventHandler(this.MenuHelp_Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolOpenData,
            this.toolSaveReport,
            this.toolStripSeparator3,
            this.toolBLH2XYZ,
            this.toolXYZ2BLH,
            this.toolStripSeparator7,
            this.toolBL2xy,
            this.toolxy2BL,
            this.toolStripSeparator8,
            this.toolChart,
            this.toolReport,
            this.toolStripSeparator9,
            this.toolZoomIn,
            this.toolZoomout,
            this.toolStripSeparator10,
            this.toolHelp});
            this.toolStrip1.Location = new System.Drawing.Point(0, 32);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(978, 31);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolOpenData
            // 
            this.toolOpenData.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolOpenData.Image = ((System.Drawing.Image)(resources.GetObject("toolOpenData.Image")));
            this.toolOpenData.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolOpenData.Name = "toolOpenData";
            this.toolOpenData.Size = new System.Drawing.Size(28, 28);
            this.toolOpenData.Text = "打开数据文件";
            this.toolOpenData.Click += new System.EventHandler(this.toolOpenData_Click);
            // 
            // toolSaveReport
            // 
            this.toolSaveReport.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolSaveReport.Image = ((System.Drawing.Image)(resources.GetObject("toolSaveReport.Image")));
            this.toolSaveReport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolSaveReport.Name = "toolSaveReport";
            this.toolSaveReport.Size = new System.Drawing.Size(28, 28);
            this.toolSaveReport.Text = "保存报告";
            this.toolSaveReport.Click += new System.EventHandler(this.toolSaveReport_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 31);
            // 
            // toolBLH2XYZ
            // 
            this.toolBLH2XYZ.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolBLH2XYZ.Image = ((System.Drawing.Image)(resources.GetObject("toolBLH2XYZ.Image")));
            this.toolBLH2XYZ.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolBLH2XYZ.Name = "toolBLH2XYZ";
            this.toolBLH2XYZ.Size = new System.Drawing.Size(28, 28);
            this.toolBLH2XYZ.Text = "BLH-->XYZ";
            this.toolBLH2XYZ.Click += new System.EventHandler(this.toolBLH2XYZ_Click);
            // 
            // toolXYZ2BLH
            // 
            this.toolXYZ2BLH.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolXYZ2BLH.Image = ((System.Drawing.Image)(resources.GetObject("toolXYZ2BLH.Image")));
            this.toolXYZ2BLH.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolXYZ2BLH.Name = "toolXYZ2BLH";
            this.toolXYZ2BLH.Size = new System.Drawing.Size(28, 28);
            this.toolXYZ2BLH.Text = "XYZ-->BLH";
            this.toolXYZ2BLH.Click += new System.EventHandler(this.toolXYZ2BLH_Click);
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(6, 31);
            // 
            // toolBL2xy
            // 
            this.toolBL2xy.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolBL2xy.Image = ((System.Drawing.Image)(resources.GetObject("toolBL2xy.Image")));
            this.toolBL2xy.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolBL2xy.Name = "toolBL2xy";
            this.toolBL2xy.Size = new System.Drawing.Size(28, 28);
            this.toolBL2xy.Text = "BL-->xy";
            this.toolBL2xy.Click += new System.EventHandler(this.toolBL2xy_Click);
            // 
            // toolxy2BL
            // 
            this.toolxy2BL.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolxy2BL.Image = ((System.Drawing.Image)(resources.GetObject("toolxy2BL.Image")));
            this.toolxy2BL.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolxy2BL.Name = "toolxy2BL";
            this.toolxy2BL.Size = new System.Drawing.Size(28, 28);
            this.toolxy2BL.Text = "toolStripButton1";
            this.toolxy2BL.Click += new System.EventHandler(this.toolxy2BL_Click);
            // 
            // toolStripSeparator8
            // 
            this.toolStripSeparator8.Name = "toolStripSeparator8";
            this.toolStripSeparator8.Size = new System.Drawing.Size(6, 31);
            // 
            // toolChart
            // 
            this.toolChart.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolChart.Image = ((System.Drawing.Image)(resources.GetObject("toolChart.Image")));
            this.toolChart.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolChart.Name = "toolChart";
            this.toolChart.Size = new System.Drawing.Size(28, 28);
            this.toolChart.Text = "点位图";
            this.toolChart.Click += new System.EventHandler(this.toolChart_Click);
            // 
            // toolReport
            // 
            this.toolReport.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolReport.Image = ((System.Drawing.Image)(resources.GetObject("toolReport.Image")));
            this.toolReport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolReport.Name = "toolReport";
            this.toolReport.Size = new System.Drawing.Size(28, 28);
            this.toolReport.Text = "报告";
            this.toolReport.Click += new System.EventHandler(this.toolReport_Click);
            // 
            // toolStripSeparator9
            // 
            this.toolStripSeparator9.Name = "toolStripSeparator9";
            this.toolStripSeparator9.Size = new System.Drawing.Size(6, 31);
            // 
            // toolZoomIn
            // 
            this.toolZoomIn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolZoomIn.Image = ((System.Drawing.Image)(resources.GetObject("toolZoomIn.Image")));
            this.toolZoomIn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolZoomIn.Name = "toolZoomIn";
            this.toolZoomIn.Size = new System.Drawing.Size(28, 28);
            this.toolZoomIn.Text = "放大";
            this.toolZoomIn.Click += new System.EventHandler(this.toolZoomIn_Click);
            // 
            // toolZoomout
            // 
            this.toolZoomout.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolZoomout.Image = ((System.Drawing.Image)(resources.GetObject("toolZoomout.Image")));
            this.toolZoomout.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolZoomout.Name = "toolZoomout";
            this.toolZoomout.Size = new System.Drawing.Size(28, 28);
            this.toolZoomout.Text = "缩小";
            this.toolZoomout.Click += new System.EventHandler(this.toolZoomout_Click);
            // 
            // toolStripSeparator10
            // 
            this.toolStripSeparator10.Name = "toolStripSeparator10";
            this.toolStripSeparator10.Size = new System.Drawing.Size(6, 31);
            // 
            // toolHelp
            // 
            this.toolHelp.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolHelp.Image = ((System.Drawing.Image)(resources.GetObject("toolHelp.Image")));
            this.toolHelp.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolHelp.Name = "toolHelp";
            this.toolHelp.Size = new System.Drawing.Size(28, 28);
            this.toolHelp.Text = "帮助";
            this.toolHelp.Click += new System.EventHandler(this.toolHelp_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Alignment = System.Windows.Forms.TabAlignment.Bottom;
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.ImageList = this.imageList1;
            this.tabControl1.Location = new System.Drawing.Point(0, 63);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(978, 499);
            this.tabControl1.TabIndex = 2;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.dataGridView1);
            this.tabPage1.ImageIndex = 2;
            this.tabPage1.Location = new System.Drawing.Point(4, 4);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(970, 467);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "数据";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(3, 3);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(964, 461);
            this.dataGridView1.TabIndex = 0;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.chart1);
            this.tabPage2.ImageIndex = 1;
            this.tabPage2.Location = new System.Drawing.Point(4, 4);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(970, 467);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "图形";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // chart1
            // 
            chartArea6.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea6);
            legend6.Name = "Legend1";
            this.chart1.Legends.Add(legend6);
            this.chart1.Location = new System.Drawing.Point(3, 3);
            this.chart1.Name = "chart1";
            series6.ChartArea = "ChartArea1";
            series6.Legend = "Legend1";
            series6.Name = "Series1";
            this.chart1.Series.Add(series6);
            this.chart1.Size = new System.Drawing.Size(964, 461);
            this.chart1.TabIndex = 0;
            this.chart1.Text = "chart1";
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.richTextBox1);
            this.tabPage3.ImageIndex = 3;
            this.tabPage3.Location = new System.Drawing.Point(4, 4);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(970, 467);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "报告";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // richTextBox1
            // 
            this.richTextBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBox1.Location = new System.Drawing.Point(3, 3);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(964, 461);
            this.richTextBox1.TabIndex = 0;
            this.richTextBox1.Text = "";
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "chart_24.ico");
            this.imageList1.Images.SetKeyName(1, "GRAPH07.ICO");
            this.imageList1.Images.SetKeyName(2, "microsoft_excel.ico");
            this.imageList1.Images.SetKeyName(3, "ghDoc.ico");
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(978, 562);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = " 坐标转换（Coortrans）V1.1";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            this.tabPage3.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolOpenData;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.ToolStripMenuItem MenuFile;
        private System.Windows.Forms.ToolStripMenuItem MenuFileOpen;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem MenuFileSaveReport;
        private System.Windows.Forms.ToolStripMenuItem MenuFileSaveChart;
        private System.Windows.Forms.ToolStripMenuItem MenuFileSaveXls;
        private System.Windows.Forms.ToolStripMenuItem MenuFileSaveDXF;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem MenuExit;
        private System.Windows.Forms.ToolStripButton toolSaveReport;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem MenuAlgo;
        private System.Windows.Forms.ToolStripMenuItem MenuBLH2XYZ;
        private System.Windows.Forms.ToolStripMenuItem MenuXYZ2BLH;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem MenuBL2xy;
        private System.Windows.Forms.ToolStripMenuItem Menuxy2BL;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripMenuItem MenuDoALL;
        private System.Windows.Forms.ToolStripMenuItem 查看ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem MenuChart;
        private System.Windows.Forms.ToolStripMenuItem MenuReport;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripMenuItem MenuZoomIn;
        private System.Windows.Forms.ToolStripMenuItem MenuZoomOut;
        private System.Windows.Forms.ToolStripMenuItem MenuHelp;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ToolStripButton toolBLH2XYZ;
        private System.Windows.Forms.ToolStripButton toolXYZ2BLH;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.ToolStripButton toolBL2xy;
        private System.Windows.Forms.ToolStripButton toolxy2BL;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator8;
        private System.Windows.Forms.ToolStripButton toolChart;
        private System.Windows.Forms.ToolStripButton toolReport;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator9;
        private System.Windows.Forms.ToolStripButton toolZoomIn;
        private System.Windows.Forms.ToolStripButton toolZoomout;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator10;
        private System.Windows.Forms.ToolStripButton toolHelp;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
    }
}

