namespace MiniClosedLoopSearch
{
    partial class MainFram
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainFram));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.文件ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.导入测站数据ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.导入水准路线数据ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.计算ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.闭合差计算ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.近似高程计算ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.成果ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.输出报告ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.帮助ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton3 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.rtxtReport = new System.Windows.Forms.RichTextBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.dgvStationInfo = new System.Windows.Forms.DataGridView();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.label1 = new System.Windows.Forms.Label();
            this.dgvCtrolPointsInfo = new System.Windows.Forms.DataGridView();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.menuStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvStationInfo)).BeginInit();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCtrolPointsInfo)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.文件ToolStripMenuItem,
            this.计算ToolStripMenuItem,
            this.成果ToolStripMenuItem,
            this.帮助ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(776, 25);
            this.menuStrip1.TabIndex = 8;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 文件ToolStripMenuItem
            // 
            this.文件ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.导入测站数据ToolStripMenuItem,
            this.导入水准路线数据ToolStripMenuItem});
            this.文件ToolStripMenuItem.Name = "文件ToolStripMenuItem";
            this.文件ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.文件ToolStripMenuItem.Text = "文件";
            // 
            // 导入测站数据ToolStripMenuItem
            // 
            this.导入测站数据ToolStripMenuItem.Name = "导入测站数据ToolStripMenuItem";
            this.导入测站数据ToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.导入测站数据ToolStripMenuItem.Text = "导入水准点数据";
            this.导入测站数据ToolStripMenuItem.Click += new System.EventHandler(this.导入测站数据ToolStripMenuItem_Click);
            // 
            // 导入水准路线数据ToolStripMenuItem
            // 
            this.导入水准路线数据ToolStripMenuItem.Name = "导入水准路线数据ToolStripMenuItem";
            this.导入水准路线数据ToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.导入水准路线数据ToolStripMenuItem.Text = "导入水准路线数据";
            this.导入水准路线数据ToolStripMenuItem.Click += new System.EventHandler(this.导入水准路线数据ToolStripMenuItem_Click);
            // 
            // 计算ToolStripMenuItem
            // 
            this.计算ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.闭合差计算ToolStripMenuItem,
            this.近似高程计算ToolStripMenuItem});
            this.计算ToolStripMenuItem.Name = "计算ToolStripMenuItem";
            this.计算ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.计算ToolStripMenuItem.Text = "计算";
            // 
            // 闭合差计算ToolStripMenuItem
            // 
            this.闭合差计算ToolStripMenuItem.Name = "闭合差计算ToolStripMenuItem";
            this.闭合差计算ToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.闭合差计算ToolStripMenuItem.Text = "闭合差计算";
            this.闭合差计算ToolStripMenuItem.Click += new System.EventHandler(this.闭合差计算ToolStripMenuItem_Click);
            // 
            // 近似高程计算ToolStripMenuItem
            // 
            this.近似高程计算ToolStripMenuItem.Name = "近似高程计算ToolStripMenuItem";
            this.近似高程计算ToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.近似高程计算ToolStripMenuItem.Text = "近似高程计算";
            this.近似高程计算ToolStripMenuItem.Click += new System.EventHandler(this.近似高程计算ToolStripMenuItem_Click);
            // 
            // 成果ToolStripMenuItem
            // 
            this.成果ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.输出报告ToolStripMenuItem});
            this.成果ToolStripMenuItem.Name = "成果ToolStripMenuItem";
            this.成果ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.成果ToolStripMenuItem.Text = "成果";
            // 
            // 输出报告ToolStripMenuItem
            // 
            this.输出报告ToolStripMenuItem.Name = "输出报告ToolStripMenuItem";
            this.输出报告ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.输出报告ToolStripMenuItem.Text = "输出报告";
            this.输出报告ToolStripMenuItem.Click += new System.EventHandler(this.输出报告ToolStripMenuItem_Click);
            // 
            // 帮助ToolStripMenuItem
            // 
            this.帮助ToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("帮助ToolStripMenuItem.Image")));
            this.帮助ToolStripMenuItem.Name = "帮助ToolStripMenuItem";
            this.帮助ToolStripMenuItem.Size = new System.Drawing.Size(60, 21);
            this.帮助ToolStripMenuItem.Text = "帮助";
            this.帮助ToolStripMenuItem.Click += new System.EventHandler(this.帮助ToolStripMenuItem_Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton3,
            this.toolStripButton1});
            this.toolStrip1.Location = new System.Drawing.Point(0, 25);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(776, 25);
            this.toolStrip1.TabIndex = 10;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButton3
            // 
            this.toolStripButton3.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton3.Image")));
            this.toolStripButton3.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton3.Name = "toolStripButton3";
            this.toolStripButton3.Size = new System.Drawing.Size(88, 22);
            this.toolStripButton3.Text = "闭合差计算";
            this.toolStripButton3.Click += new System.EventHandler(this.toolStripButton3_Click);
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(100, 22);
            this.toolStripButton1.Text = "近似高程计算";
            this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.rtxtReport);
            this.tabPage3.Location = new System.Drawing.Point(4, 4);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(723, 366);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "报告";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // rtxtReport
            // 
            this.rtxtReport.Location = new System.Drawing.Point(3, 3);
            this.rtxtReport.Name = "rtxtReport";
            this.rtxtReport.Size = new System.Drawing.Size(706, 346);
            this.rtxtReport.TabIndex = 0;
            this.rtxtReport.Text = "";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.dgvStationInfo);
            this.tabPage2.Location = new System.Drawing.Point(4, 4);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(723, 366);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "水准路线信息";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // dgvStationInfo
            // 
            this.dgvStationInfo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvStationInfo.Location = new System.Drawing.Point(6, 6);
            this.dgvStationInfo.Name = "dgvStationInfo";
            this.dgvStationInfo.RowTemplate.Height = 23;
            this.dgvStationInfo.Size = new System.Drawing.Size(687, 338);
            this.dgvStationInfo.TabIndex = 1;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.dgvCtrolPointsInfo);
            this.tabPage1.Location = new System.Drawing.Point(4, 4);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(723, 366);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "水准点信息";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(414, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(305, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "说明：点的属性00表示高程未知点，01代表高程已知点。";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // dgvCtrolPointsInfo
            // 
            this.dgvCtrolPointsInfo.AllowUserToDeleteRows = false;
            this.dgvCtrolPointsInfo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCtrolPointsInfo.Location = new System.Drawing.Point(16, 6);
            this.dgvCtrolPointsInfo.Name = "dgvCtrolPointsInfo";
            this.dgvCtrolPointsInfo.RowTemplate.Height = 23;
            this.dgvCtrolPointsInfo.Size = new System.Drawing.Size(392, 338);
            this.dgvCtrolPointsInfo.TabIndex = 0;
            // 
            // tabControl1
            // 
            this.tabControl1.Alignment = System.Windows.Forms.TabAlignment.Bottom;
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Location = new System.Drawing.Point(12, 53);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(731, 392);
            this.tabControl1.TabIndex = 11;
            this.tabControl1.TabStop = false;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 449);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(776, 22);
            this.statusStrip1.TabIndex = 12;
            this.statusStrip1.Text = "的";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(524, 17);
            this.toolStripStatusLabel1.Text = "请按照导入水准点数据➡导入水准路线数据➡闭合差计算➡近似高程计算➡输出报告的顺序使用！";
            // 
            // MainFram
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(776, 471);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainFram";
            this.Text = "水准网最小闭合环搜索算法";
            this.Load += new System.EventHandler(this.MainFram_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvStationInfo)).EndInit();
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCtrolPointsInfo)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 文件ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 导入水准路线数据ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 计算ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 闭合差计算ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 近似高程计算ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 成果ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 输出报告ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 导入测站数据ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 帮助ToolStripMenuItem;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripButton3;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.RichTextBox rtxtReport;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.DataGridView dgvStationInfo;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.DataGridView dgvCtrolPointsInfo;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.Label label1;
    }
}

