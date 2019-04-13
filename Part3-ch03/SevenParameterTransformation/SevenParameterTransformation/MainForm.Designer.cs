namespace SevenParameterTransformation
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.menuFile = new System.Windows.Forms.ToolStripMenuItem();
            this.menuReadData = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.menuSaveReport = new System.Windows.Forms.ToolStripMenuItem();
            this.menuSaveDXF = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.menuExit = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuCompute = new System.Windows.Forms.ToolStripMenuItem();
            this.menuComputeParameter = new System.Windows.Forms.ToolStripMenuItem();
            this.menuTransform = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.menuDoAll = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuView = new System.Windows.Forms.ToolStripMenuItem();
            this.menuViewData = new System.Windows.Forms.ToolStripMenuItem();
            this.menuViewChart = new System.Windows.Forms.ToolStripMenuItem();
            this.menuViewReport = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.menuZoomIn = new System.Windows.Forms.ToolStripMenuItem();
            this.menuZoomOut = new System.Windows.Forms.ToolStripMenuItem();
            this.menuHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.txtReport = new System.Windows.Forms.RichTextBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolOpen = new System.Windows.Forms.ToolStripButton();
            this.toolSave = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolCalculateSevenPara = new System.Windows.Forms.ToolStripButton();
            this.toolTransform = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.toolZoomIn = new System.Windows.Forms.ToolStripButton();
            this.toolZoomOut = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.toolHelp = new System.Windows.Forms.ToolStripButton();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            this.tabPage3.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuFile,
            this.MenuCompute,
            this.MenuView,
            this.menuHelp});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(630, 25);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // menuFile
            // 
            this.menuFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuReadData,
            this.toolStripSeparator2,
            this.menuSaveReport,
            this.menuSaveDXF,
            this.toolStripSeparator1,
            this.menuExit});
            this.menuFile.Name = "menuFile";
            this.menuFile.Size = new System.Drawing.Size(58, 21);
            this.menuFile.Text = "文件(&F)";
            // 
            // menuReadData
            // 
            this.menuReadData.Image = ((System.Drawing.Image)(resources.GetObject("menuReadData.Image")));
            this.menuReadData.Name = "menuReadData";
            this.menuReadData.Size = new System.Drawing.Size(159, 22);
            this.menuReadData.Text = "打开(&O)";
            this.menuReadData.Click += new System.EventHandler(this.menuReadData_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(156, 6);
            // 
            // menuSaveReport
            // 
            this.menuSaveReport.Image = ((System.Drawing.Image)(resources.GetObject("menuSaveReport.Image")));
            this.menuSaveReport.Name = "menuSaveReport";
            this.menuSaveReport.Size = new System.Drawing.Size(159, 22);
            this.menuSaveReport.Text = "保存报告(&R)";
            this.menuSaveReport.Click += new System.EventHandler(this.menuSaveReport_Click);
            // 
            // menuSaveDXF
            // 
            this.menuSaveDXF.Image = ((System.Drawing.Image)(resources.GetObject("menuSaveDXF.Image")));
            this.menuSaveDXF.Name = "menuSaveDXF";
            this.menuSaveDXF.Size = new System.Drawing.Size(159, 22);
            this.menuSaveDXF.Text = "保存图形dxf(&D)";
            this.menuSaveDXF.Click += new System.EventHandler(this.menuSaveDXF_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(156, 6);
            // 
            // menuExit
            // 
            this.menuExit.Image = ((System.Drawing.Image)(resources.GetObject("menuExit.Image")));
            this.menuExit.Name = "menuExit";
            this.menuExit.Size = new System.Drawing.Size(159, 22);
            this.menuExit.Text = "退出(&X)";
            this.menuExit.Click += new System.EventHandler(this.menuExit_Click);
            // 
            // MenuCompute
            // 
            this.MenuCompute.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuComputeParameter,
            this.menuTransform,
            this.toolStripMenuItem1,
            this.menuDoAll});
            this.MenuCompute.Name = "MenuCompute";
            this.MenuCompute.Size = new System.Drawing.Size(60, 21);
            this.MenuCompute.Text = "计算(&C)";
            // 
            // menuComputeParameter
            // 
            this.menuComputeParameter.Image = ((System.Drawing.Image)(resources.GetObject("menuComputeParameter.Image")));
            this.menuComputeParameter.Name = "menuComputeParameter";
            this.menuComputeParameter.Size = new System.Drawing.Size(151, 22);
            this.menuComputeParameter.Text = "求解七参数(&S)";
            this.menuComputeParameter.Click += new System.EventHandler(this.menuComputeParameter_Click);
            // 
            // menuTransform
            // 
            this.menuTransform.Image = ((System.Drawing.Image)(resources.GetObject("menuTransform.Image")));
            this.menuTransform.Name = "menuTransform";
            this.menuTransform.Size = new System.Drawing.Size(151, 22);
            this.menuTransform.Text = "转换(&T)";
            this.menuTransform.Click += new System.EventHandler(this.menuTransform_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(148, 6);
            // 
            // menuDoAll
            // 
            this.menuDoAll.Name = "menuDoAll";
            this.menuDoAll.Size = new System.Drawing.Size(151, 22);
            this.menuDoAll.Text = "一键处理(&A)";
            this.menuDoAll.Click += new System.EventHandler(this.menuDoAll_Click);
            // 
            // MenuView
            // 
            this.MenuView.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuViewData,
            this.menuViewChart,
            this.menuViewReport,
            this.toolStripMenuItem2,
            this.menuZoomIn,
            this.menuZoomOut});
            this.MenuView.Name = "MenuView";
            this.MenuView.Size = new System.Drawing.Size(60, 21);
            this.MenuView.Text = "查看(&V)";
            // 
            // menuViewData
            // 
            this.menuViewData.Image = ((System.Drawing.Image)(resources.GetObject("menuViewData.Image")));
            this.menuViewData.Name = "menuViewData";
            this.menuViewData.Size = new System.Drawing.Size(118, 22);
            this.menuViewData.Text = "数据(&D)";
            this.menuViewData.Click += new System.EventHandler(this.menuViewData_Click);
            // 
            // menuViewChart
            // 
            this.menuViewChart.Image = ((System.Drawing.Image)(resources.GetObject("menuViewChart.Image")));
            this.menuViewChart.Name = "menuViewChart";
            this.menuViewChart.Size = new System.Drawing.Size(118, 22);
            this.menuViewChart.Text = "图形(&P)";
            this.menuViewChart.Click += new System.EventHandler(this.menuViewChart_Click);
            // 
            // menuViewReport
            // 
            this.menuViewReport.Image = ((System.Drawing.Image)(resources.GetObject("menuViewReport.Image")));
            this.menuViewReport.Name = "menuViewReport";
            this.menuViewReport.Size = new System.Drawing.Size(118, 22);
            this.menuViewReport.Text = "报告(&R)";
            this.menuViewReport.Click += new System.EventHandler(this.menuViewReport_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(115, 6);
            // 
            // menuZoomIn
            // 
            this.menuZoomIn.Image = ((System.Drawing.Image)(resources.GetObject("menuZoomIn.Image")));
            this.menuZoomIn.Name = "menuZoomIn";
            this.menuZoomIn.Size = new System.Drawing.Size(118, 22);
            this.menuZoomIn.Text = "放大(&I)";
            this.menuZoomIn.Click += new System.EventHandler(this.menuZoomIn_Click);
            // 
            // menuZoomOut
            // 
            this.menuZoomOut.Image = ((System.Drawing.Image)(resources.GetObject("menuZoomOut.Image")));
            this.menuZoomOut.Name = "menuZoomOut";
            this.menuZoomOut.Size = new System.Drawing.Size(118, 22);
            this.menuZoomOut.Text = "缩小(&O)";
            this.menuZoomOut.Click += new System.EventHandler(this.menuZoomOut_Click);
            // 
            // menuHelp
            // 
            this.menuHelp.Name = "menuHelp";
            this.menuHelp.Size = new System.Drawing.Size(61, 21);
            this.menuHelp.Text = "帮助(&H)";
            this.menuHelp.Click += new System.EventHandler(this.menuHelp_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(3, 3);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(616, 286);
            this.dataGridView1.TabIndex = 1;
            // 
            // tabControl1
            // 
            this.tabControl1.Alignment = System.Windows.Forms.TabAlignment.Bottom;
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Location = new System.Drawing.Point(0, 53);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(630, 315);
            this.tabControl1.TabIndex = 2;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.dataGridView1);
            this.tabPage1.Location = new System.Drawing.Point(4, 4);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(622, 289);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "数据";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.chart1);
            this.tabPage2.Location = new System.Drawing.Point(4, 4);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(622, 289);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "图形";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // chart1
            // 
            this.chart1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            chartArea1.AxisX.MajorGrid.Enabled = false;
            chartArea1.AxisX.MajorGrid.LineColor = System.Drawing.Color.Transparent;
            chartArea1.AxisY.MajorGrid.Enabled = false;
            chartArea1.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea1);
            this.chart1.Location = new System.Drawing.Point(3, 3);
            this.chart1.Name = "chart1";
            series1.ChartArea = "ChartArea1";
            series1.Name = "Series1";
            this.chart1.Series.Add(series1);
            this.chart1.Size = new System.Drawing.Size(616, 286);
            this.chart1.TabIndex = 0;
            this.chart1.Text = "chart1";
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.txtReport);
            this.tabPage3.Location = new System.Drawing.Point(4, 4);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(622, 289);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "报告";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // txtReport
            // 
            this.txtReport.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtReport.BackColor = System.Drawing.Color.White;
            this.txtReport.Location = new System.Drawing.Point(3, 3);
            this.txtReport.Name = "txtReport";
            this.txtReport.ReadOnly = true;
            this.txtReport.Size = new System.Drawing.Size(616, 286);
            this.txtReport.TabIndex = 0;
            this.txtReport.Text = "";
            this.txtReport.WordWrap = false;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolOpen,
            this.toolSave,
            this.toolStripSeparator3,
            this.toolCalculateSevenPara,
            this.toolTransform,
            this.toolStripSeparator4,
            this.toolZoomIn,
            this.toolZoomOut,
            this.toolStripSeparator5,
            this.toolHelp});
            this.toolStrip1.Location = new System.Drawing.Point(0, 25);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(630, 25);
            this.toolStrip1.TabIndex = 3;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolOpen
            // 
            this.toolOpen.Image = ((System.Drawing.Image)(resources.GetObject("toolOpen.Image")));
            this.toolOpen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolOpen.Name = "toolOpen";
            this.toolOpen.Size = new System.Drawing.Size(52, 22);
            this.toolOpen.Text = "打开";
            this.toolOpen.Click += new System.EventHandler(this.menuReadData_Click);
            // 
            // toolSave
            // 
            this.toolSave.Image = ((System.Drawing.Image)(resources.GetObject("toolSave.Image")));
            this.toolSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolSave.Name = "toolSave";
            this.toolSave.Size = new System.Drawing.Size(52, 22);
            this.toolSave.Text = "保存";
            this.toolSave.Click += new System.EventHandler(this.menuSaveReport_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // toolCalculateSevenPara
            // 
            this.toolCalculateSevenPara.Image = ((System.Drawing.Image)(resources.GetObject("toolCalculateSevenPara.Image")));
            this.toolCalculateSevenPara.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolCalculateSevenPara.Name = "toolCalculateSevenPara";
            this.toolCalculateSevenPara.Size = new System.Drawing.Size(52, 22);
            this.toolCalculateSevenPara.Text = "计算";
            this.toolCalculateSevenPara.Click += new System.EventHandler(this.menuComputeParameter_Click);
            // 
            // toolTransform
            // 
            this.toolTransform.Image = ((System.Drawing.Image)(resources.GetObject("toolTransform.Image")));
            this.toolTransform.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolTransform.Name = "toolTransform";
            this.toolTransform.Size = new System.Drawing.Size(52, 22);
            this.toolTransform.Text = "转换";
            this.toolTransform.Click += new System.EventHandler(this.menuTransform_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
            // 
            // toolZoomIn
            // 
            this.toolZoomIn.Image = ((System.Drawing.Image)(resources.GetObject("toolZoomIn.Image")));
            this.toolZoomIn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolZoomIn.Name = "toolZoomIn";
            this.toolZoomIn.Size = new System.Drawing.Size(52, 22);
            this.toolZoomIn.Text = "放大";
            this.toolZoomIn.Click += new System.EventHandler(this.menuZoomIn_Click);
            // 
            // toolZoomOut
            // 
            this.toolZoomOut.Image = ((System.Drawing.Image)(resources.GetObject("toolZoomOut.Image")));
            this.toolZoomOut.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolZoomOut.Name = "toolZoomOut";
            this.toolZoomOut.Size = new System.Drawing.Size(52, 22);
            this.toolZoomOut.Text = "缩小";
            this.toolZoomOut.Click += new System.EventHandler(this.menuZoomOut_Click);
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
            this.toolHelp.Click += new System.EventHandler(this.menuHelp_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(630, 368);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = "七参数坐标转换";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
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
        private System.Windows.Forms.ToolStripMenuItem menuFile;
        private System.Windows.Forms.ToolStripMenuItem menuReadData;
        private System.Windows.Forms.ToolStripMenuItem menuSaveReport;
        private System.Windows.Forms.ToolStripMenuItem menuSaveDXF;
        private System.Windows.Forms.ToolStripMenuItem MenuCompute;
        private System.Windows.Forms.ToolStripMenuItem menuComputeParameter;
        private System.Windows.Forms.ToolStripMenuItem menuTransform;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem menuDoAll;
        private System.Windows.Forms.ToolStripMenuItem MenuView;
        private System.Windows.Forms.ToolStripMenuItem menuViewData;
        private System.Windows.Forms.ToolStripMenuItem menuViewChart;
        private System.Windows.Forms.ToolStripMenuItem menuViewReport;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem menuZoomIn;
        private System.Windows.Forms.ToolStripMenuItem menuZoomOut;
        private System.Windows.Forms.ToolStripMenuItem menuHelp;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.RichTextBox txtReport;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem menuExit;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolOpen;
        private System.Windows.Forms.ToolStripButton toolSave;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton toolCalculateSevenPara;
        private System.Windows.Forms.ToolStripButton toolTransform;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripButton toolZoomIn;
        private System.Windows.Forms.ToolStripButton toolZoomOut;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripButton toolHelp;
    }
}

