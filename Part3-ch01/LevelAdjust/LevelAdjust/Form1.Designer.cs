namespace LevelDll
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.toolOpenData = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolPreProcess = new System.Windows.Forms.ToolStripButton();
            this.toolProcess = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.toolPrecessipm = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
            this.toolChart = new System.Windows.Forms.ToolStripButton();
            this.toolReport = new System.Windows.Forms.ToolStripButton();
            this.toolSaveReport = new System.Windows.Forms.ToolStripButton();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.MenuFile = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuFileOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.MenuFileSaveReport = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuFileSaveChart = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuFileSaveDXF = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.MenuExit = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuAlgo = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuPreprocess = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuAdjustment = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.MenuPrecession = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.MenuDoALL = new System.Windows.Forms.ToolStripMenuItem();
            this.查看ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuChart = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuReport = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            this.tabPage3.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
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
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 31);
            // 
            // toolPreProcess
            // 
            this.toolPreProcess.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolPreProcess.Image = ((System.Drawing.Image)(resources.GetObject("toolPreProcess.Image")));
            this.toolPreProcess.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolPreProcess.Name = "toolPreProcess";
            this.toolPreProcess.Size = new System.Drawing.Size(28, 28);
            this.toolPreProcess.Text = "数据预处理";
            // 
            // toolProcess
            // 
            this.toolProcess.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolProcess.Image = ((System.Drawing.Image)(resources.GetObject("toolProcess.Image")));
            this.toolProcess.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolProcess.Name = "toolProcess";
            this.toolProcess.Size = new System.Drawing.Size(28, 28);
            this.toolProcess.Text = "XYZ-->BLH";
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(6, 31);
            // 
            // toolPrecessipm
            // 
            this.toolPrecessipm.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolPrecessipm.Image = ((System.Drawing.Image)(resources.GetObject("toolPrecessipm.Image")));
            this.toolPrecessipm.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolPrecessipm.Name = "toolPrecessipm";
            this.toolPrecessipm.Size = new System.Drawing.Size(28, 28);
            this.toolPrecessipm.Text = "精度评价";
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
            this.toolChart.Text = "示意图";
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
            // tabControl1
            // 
            this.tabControl1.Alignment = System.Windows.Forms.TabAlignment.Bottom;
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.ImageList = this.imageList1;
            this.tabControl1.Location = new System.Drawing.Point(0, 63);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(869, 405);
            this.tabControl1.TabIndex = 5;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.dataGridView1);
            this.tabPage1.ImageIndex = 2;
            this.tabPage1.Location = new System.Drawing.Point(4, 4);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabPage1.Size = new System.Drawing.Size(861, 376);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "数据";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(3, 2);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(855, 372);
            this.dataGridView1.TabIndex = 0;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.chart1);
            this.tabPage2.ImageIndex = 1;
            this.tabPage2.Location = new System.Drawing.Point(4, 4);
            this.tabPage2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabPage2.Size = new System.Drawing.Size(861, 376);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "图形";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // chart1
            // 
            chartArea2.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea2);
            this.chart1.Dock = System.Windows.Forms.DockStyle.Fill;
            legend2.Name = "Legend1";
            this.chart1.Legends.Add(legend2);
            this.chart1.Location = new System.Drawing.Point(3, 2);
            this.chart1.Name = "chart1";
            series2.ChartArea = "ChartArea1";
            series2.Legend = "Legend1";
            series2.Name = "Series1";
            this.chart1.Series.Add(series2);
            this.chart1.Size = new System.Drawing.Size(855, 372);
            this.chart1.TabIndex = 0;
            this.chart1.Text = "chart1";
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.richTextBox1);
            this.tabPage3.ImageIndex = 3;
            this.tabPage3.Location = new System.Drawing.Point(4, 4);
            this.tabPage3.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabPage3.Size = new System.Drawing.Size(861, 376);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "报告";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // richTextBox1
            // 
            this.richTextBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBox1.Location = new System.Drawing.Point(3, 2);
            this.richTextBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(855, 372);
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
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolOpenData,
            this.toolSaveReport,
            this.toolStripSeparator3,
            this.toolPreProcess,
            this.toolProcess,
            this.toolStripSeparator7,
            this.toolPrecessipm,
            this.toolStripSeparator8,
            this.toolChart,
            this.toolReport});
            this.toolStrip1.Location = new System.Drawing.Point(0, 32);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(869, 31);
            this.toolStrip1.TabIndex = 4;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuFile,
            this.MenuAlgo,
            this.查看ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(5, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(869, 32);
            this.menuStrip1.TabIndex = 3;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // MenuFile
            // 
            this.MenuFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuFileOpen,
            this.toolStripSeparator1,
            this.MenuFileSaveReport,
            this.MenuFileSaveChart,
            this.MenuFileSaveDXF,
            this.toolStripSeparator2,
            this.MenuExit});
            this.MenuFile.Image = ((System.Drawing.Image)(resources.GetObject("MenuFile.Image")));
            this.MenuFile.Name = "MenuFile";
            this.MenuFile.Size = new System.Drawing.Size(93, 28);
            this.MenuFile.Text = "文件(&F)";
            this.MenuFile.Click += new System.EventHandler(this.MenuFile_Click);
            // 
            // MenuFileOpen
            // 
            this.MenuFileOpen.Image = ((System.Drawing.Image)(resources.GetObject("MenuFileOpen.Image")));
            this.MenuFileOpen.Name = "MenuFileOpen";
            this.MenuFileOpen.Size = new System.Drawing.Size(199, 30);
            this.MenuFileOpen.Text = "打开数据文件(&D)";
            this.MenuFileOpen.Click += new System.EventHandler(this.MenuFileOpen_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(196, 6);
            // 
            // MenuFileSaveReport
            // 
            this.MenuFileSaveReport.Image = ((System.Drawing.Image)(resources.GetObject("MenuFileSaveReport.Image")));
            this.MenuFileSaveReport.Name = "MenuFileSaveReport";
            this.MenuFileSaveReport.Size = new System.Drawing.Size(199, 30);
            this.MenuFileSaveReport.Text = "保存报告（&S）";
            this.MenuFileSaveReport.Click += new System.EventHandler(this.MenuFileSaveReport_Click);
            // 
            // MenuFileSaveChart
            // 
            this.MenuFileSaveChart.Name = "MenuFileSaveChart";
            this.MenuFileSaveChart.Size = new System.Drawing.Size(199, 30);
            this.MenuFileSaveChart.Text = "保存图形";
            this.MenuFileSaveChart.Click += new System.EventHandler(this.MenuFileSaveChart_Click);
            // 
            // MenuFileSaveDXF
            // 
            this.MenuFileSaveDXF.Name = "MenuFileSaveDXF";
            this.MenuFileSaveDXF.Size = new System.Drawing.Size(199, 30);
            this.MenuFileSaveDXF.Text = "输出DXF文件";
            this.MenuFileSaveDXF.Click += new System.EventHandler(this.MenuFileSaveDXF_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(196, 6);
            // 
            // MenuExit
            // 
            this.MenuExit.Image = ((System.Drawing.Image)(resources.GetObject("MenuExit.Image")));
            this.MenuExit.Name = "MenuExit";
            this.MenuExit.Size = new System.Drawing.Size(199, 30);
            this.MenuExit.Text = "退出（&X）";
            this.MenuExit.Click += new System.EventHandler(this.MenuExit_Click);
            // 
            // MenuAlgo
            // 
            this.MenuAlgo.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuPreprocess,
            this.MenuAdjustment,
            this.toolStripSeparator4,
            this.MenuPrecession,
            this.toolStripSeparator5,
            this.MenuDoALL});
            this.MenuAlgo.Image = ((System.Drawing.Image)(resources.GetObject("MenuAlgo.Image")));
            this.MenuAlgo.Name = "MenuAlgo";
            this.MenuAlgo.Size = new System.Drawing.Size(95, 28);
            this.MenuAlgo.Text = "计算(&C)";
            // 
            // MenuPreprocess
            // 
            this.MenuPreprocess.Image = ((System.Drawing.Image)(resources.GetObject("MenuPreprocess.Image")));
            this.MenuPreprocess.Name = "MenuPreprocess";
            this.MenuPreprocess.Size = new System.Drawing.Size(185, 30);
            this.MenuPreprocess.Text = "数据预处理";
            this.MenuPreprocess.Click += new System.EventHandler(this.MenuPreprocess_Click);
            // 
            // MenuAdjustment
            // 
            this.MenuAdjustment.Image = ((System.Drawing.Image)(resources.GetObject("MenuAdjustment.Image")));
            this.MenuAdjustment.Name = "MenuAdjustment";
            this.MenuAdjustment.Size = new System.Drawing.Size(185, 30);
            this.MenuAdjustment.Text = "近似平差";
            this.MenuAdjustment.Click += new System.EventHandler(this.MenuAdjustment_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(182, 6);
            // 
            // MenuPrecession
            // 
            this.MenuPrecession.Image = ((System.Drawing.Image)(resources.GetObject("MenuPrecession.Image")));
            this.MenuPrecession.Name = "MenuPrecession";
            this.MenuPrecession.Size = new System.Drawing.Size(185, 30);
            this.MenuPrecession.Text = "严密平差";
            this.MenuPrecession.Click += new System.EventHandler(this.MenuPrecession_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(182, 6);
            // 
            // MenuDoALL
            // 
            this.MenuDoALL.Name = "MenuDoALL";
            this.MenuDoALL.Size = new System.Drawing.Size(185, 30);
            this.MenuDoALL.Text = "一键处理";
            this.MenuDoALL.Click += new System.EventHandler(this.MenuDoALL_Click);
            // 
            // 查看ToolStripMenuItem
            // 
            this.查看ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuChart,
            this.MenuReport});
            this.查看ToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("查看ToolStripMenuItem.Image")));
            this.查看ToolStripMenuItem.Name = "查看ToolStripMenuItem";
            this.查看ToolStripMenuItem.Size = new System.Drawing.Size(75, 28);
            this.查看ToolStripMenuItem.Text = "查看";
            // 
            // MenuChart
            // 
            this.MenuChart.Image = ((System.Drawing.Image)(resources.GetObject("MenuChart.Image")));
            this.MenuChart.Name = "MenuChart";
            this.MenuChart.Size = new System.Drawing.Size(185, 30);
            this.MenuChart.Text = "示意图";
            // 
            // MenuReport
            // 
            this.MenuReport.Image = ((System.Drawing.Image)(resources.GetObject("MenuReport.Image")));
            this.MenuReport.Name = "MenuReport";
            this.MenuReport.Size = new System.Drawing.Size(185, 30);
            this.MenuReport.Text = "报告";
            this.MenuReport.Click += new System.EventHandler(this.MenuReport_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(869, 468);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "Form1";
            this.Text = "附合水准路线平差（LevelAdjust）V1.1";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            this.tabPage3.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStripButton toolOpenData;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton toolPreProcess;
        private System.Windows.Forms.ToolStripButton toolProcess;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.ToolStripButton toolPrecessipm;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator8;
        private System.Windows.Forms.ToolStripButton toolChart;
        private System.Windows.Forms.ToolStripButton toolReport;
        private System.Windows.Forms.ToolStripButton toolSaveReport;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem MenuFile;
        private System.Windows.Forms.ToolStripMenuItem MenuFileOpen;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem MenuFileSaveReport;
        private System.Windows.Forms.ToolStripMenuItem MenuFileSaveChart;
        private System.Windows.Forms.ToolStripMenuItem MenuFileSaveDXF;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem MenuExit;
        private System.Windows.Forms.ToolStripMenuItem MenuAlgo;
        private System.Windows.Forms.ToolStripMenuItem MenuPreprocess;
        private System.Windows.Forms.ToolStripMenuItem MenuAdjustment;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem MenuPrecession;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripMenuItem MenuDoALL;
        private System.Windows.Forms.ToolStripMenuItem 查看ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem MenuChart;
        private System.Windows.Forms.ToolStripMenuItem MenuReport;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
    }
}

