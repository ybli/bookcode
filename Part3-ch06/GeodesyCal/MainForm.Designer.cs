namespace GeodesyCal
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
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.MenuFile = new System.Windows.Forms.ToolStripMenuItem();
            this.OpenPosData = new System.Windows.Forms.ToolStripMenuItem();
            this.OpenNegData = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.SavePosData = new System.Windows.Forms.ToolStripMenuItem();
            this.SaveNegData = new System.Windows.Forms.ToolStripMenuItem();
            this.SaveImage = new System.Windows.Forms.ToolStripMenuItem();
            this.SaveReport = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.Exit = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuComputation = new System.Windows.Forms.ToolStripMenuItem();
            this.PosComputation = new System.Windows.Forms.ToolStripMenuItem();
            this.NegComputation = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuView = new System.Windows.Forms.ToolStripMenuItem();
            this.ViewPosData = new System.Windows.Forms.ToolStripMenuItem();
            this.ViewNegData = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.ViewPosImage = new System.Windows.Forms.ToolStripMenuItem();
            this.ViewNegImage = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
            this.ViewReport = new System.Windows.Forms.ToolStripMenuItem();
            this.ViewNegReport = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.ImageZoomIn = new System.Windows.Forms.ToolStripMenuItem();
            this.ImageZoomOut = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.VersionInfo = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.ToolOpen = new System.Windows.Forms.ToolStripButton();
            this.ToolSave = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.ToolZoomIn = new System.Windows.Forms.ToolStripButton();
            this.ToolZoomOut = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.EllipsoidA = new System.Windows.Forms.ToolStripLabel();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.Ellipsoidf_ = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.caltype = new System.Windows.Forms.ToolStripLabel();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.EditPage = new System.Windows.Forms.TabPage();
            this.dataGridViewNeg = new System.Windows.Forms.DataGridView();
            this.dataGridViewPos = new System.Windows.Forms.DataGridView();
            this.ImagePage = new System.Windows.Forms.TabPage();
            this.pictureBox = new System.Windows.Forms.PictureBox();
            this.ReportPage = new System.Windows.Forms.TabPage();
            this.richTextBox = new System.Windows.Forms.RichTextBox();
            this.toolPosComputation = new System.Windows.Forms.ToolStripButton();
            this.ToolInvComputation = new System.Windows.Forms.ToolStripButton();
            this.toolHelp = new System.Windows.Forms.ToolStripButton();
            this.menuStrip.SuspendLayout();
            this.toolStrip.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.EditPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewNeg)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewPos)).BeginInit();
            this.ImagePage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            this.ReportPage.SuspendLayout();
            this.SuspendLayout();
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "openFileDialog1";
            // 
            // menuStrip
            // 
            this.menuStrip.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuFile,
            this.MenuComputation,
            this.MenuView,
            this.MenuHelp});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Padding = new System.Windows.Forms.Padding(9, 3, 0, 3);
            this.menuStrip.Size = new System.Drawing.Size(978, 34);
            this.menuStrip.TabIndex = 0;
            this.menuStrip.Text = "menuStrip1";
            // 
            // MenuFile
            // 
            this.MenuFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.OpenPosData,
            this.OpenNegData,
            this.toolStripSeparator4,
            this.SavePosData,
            this.SaveNegData,
            this.SaveImage,
            this.SaveReport,
            this.toolStripSeparator5,
            this.Exit});
            this.MenuFile.Name = "MenuFile";
            this.MenuFile.Size = new System.Drawing.Size(80, 28);
            this.MenuFile.Text = "文件(&F)";
            // 
            // OpenPosData
            // 
            this.OpenPosData.Name = "OpenPosData";
            this.OpenPosData.Size = new System.Drawing.Size(200, 30);
            this.OpenPosData.Text = "导入正算数据";
            this.OpenPosData.Click += new System.EventHandler(this.OpenPosData_Click);
            // 
            // OpenNegData
            // 
            this.OpenNegData.Name = "OpenNegData";
            this.OpenNegData.Size = new System.Drawing.Size(200, 30);
            this.OpenNegData.Text = "导入反算数据";
            this.OpenNegData.Click += new System.EventHandler(this.OpenNegData_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(197, 6);
            // 
            // SavePosData
            // 
            this.SavePosData.Name = "SavePosData";
            this.SavePosData.Size = new System.Drawing.Size(200, 30);
            this.SavePosData.Text = "保存正算表格";
            this.SavePosData.Click += new System.EventHandler(this.SavePosData_Click);
            // 
            // SaveNegData
            // 
            this.SaveNegData.Name = "SaveNegData";
            this.SaveNegData.Size = new System.Drawing.Size(200, 30);
            this.SaveNegData.Text = "保存反算表格";
            this.SaveNegData.Click += new System.EventHandler(this.SaveNegData_Click);
            // 
            // SaveImage
            // 
            this.SaveImage.Name = "SaveImage";
            this.SaveImage.Size = new System.Drawing.Size(200, 30);
            this.SaveImage.Text = "保存图片";
            this.SaveImage.Click += new System.EventHandler(this.SaveImage_Click);
            // 
            // SaveReport
            // 
            this.SaveReport.Name = "SaveReport";
            this.SaveReport.Size = new System.Drawing.Size(200, 30);
            this.SaveReport.Text = "保存计算报告";
            this.SaveReport.Click += new System.EventHandler(this.SaveReport_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(197, 6);
            // 
            // Exit
            // 
            this.Exit.Name = "Exit";
            this.Exit.Size = new System.Drawing.Size(200, 30);
            this.Exit.Text = "退出程序";
            this.Exit.Click += new System.EventHandler(this.Exit_Click);
            // 
            // MenuComputation
            // 
            this.MenuComputation.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.PosComputation,
            this.NegComputation});
            this.MenuComputation.Name = "MenuComputation";
            this.MenuComputation.Size = new System.Drawing.Size(82, 28);
            this.MenuComputation.Text = "计算(&C)";
            // 
            // PosComputation
            // 
            this.PosComputation.Name = "PosComputation";
            this.PosComputation.Size = new System.Drawing.Size(210, 30);
            this.PosComputation.Text = "大地主题正算";
            this.PosComputation.Click += new System.EventHandler(this.PosComputation_Click);
            // 
            // NegComputation
            // 
            this.NegComputation.Name = "NegComputation";
            this.NegComputation.Size = new System.Drawing.Size(210, 30);
            this.NegComputation.Text = "大地主题反算";
            this.NegComputation.Click += new System.EventHandler(this.NegComputation_Click);
            // 
            // MenuView
            // 
            this.MenuView.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ViewPosData,
            this.ViewNegData,
            this.toolStripSeparator7,
            this.ViewPosImage,
            this.ViewNegImage,
            this.toolStripSeparator8,
            this.ViewReport,
            this.ViewNegReport,
            this.toolStripSeparator3,
            this.ImageZoomIn,
            this.ImageZoomOut});
            this.MenuView.Name = "MenuView";
            this.MenuView.Size = new System.Drawing.Size(82, 28);
            this.MenuView.Text = "查看(&V)";
            // 
            // ViewPosData
            // 
            this.ViewPosData.Name = "ViewPosData";
            this.ViewPosData.Size = new System.Drawing.Size(210, 30);
            this.ViewPosData.Text = "正算表格";
            this.ViewPosData.Click += new System.EventHandler(this.ViewPosData_Click);
            // 
            // ViewNegData
            // 
            this.ViewNegData.Name = "ViewNegData";
            this.ViewNegData.Size = new System.Drawing.Size(210, 30);
            this.ViewNegData.Text = "反算表格";
            this.ViewNegData.Click += new System.EventHandler(this.ViewNegData_Click);
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(207, 6);
            // 
            // ViewPosImage
            // 
            this.ViewPosImage.Name = "ViewPosImage";
            this.ViewPosImage.Size = new System.Drawing.Size(210, 30);
            this.ViewPosImage.Text = "正算图片";
            this.ViewPosImage.Click += new System.EventHandler(this.ViewPosImage_Click);
            // 
            // ViewNegImage
            // 
            this.ViewNegImage.Name = "ViewNegImage";
            this.ViewNegImage.Size = new System.Drawing.Size(210, 30);
            this.ViewNegImage.Text = "反算图片";
            this.ViewNegImage.Click += new System.EventHandler(this.ViewImage_Click);
            // 
            // toolStripSeparator8
            // 
            this.toolStripSeparator8.Name = "toolStripSeparator8";
            this.toolStripSeparator8.Size = new System.Drawing.Size(207, 6);
            // 
            // ViewReport
            // 
            this.ViewReport.Name = "ViewReport";
            this.ViewReport.Size = new System.Drawing.Size(210, 30);
            this.ViewReport.Text = "正算报告";
            this.ViewReport.Click += new System.EventHandler(this.ViewReport_Click);
            // 
            // ViewNegReport
            // 
            this.ViewNegReport.Name = "ViewNegReport";
            this.ViewNegReport.Size = new System.Drawing.Size(210, 30);
            this.ViewNegReport.Text = "反算报告";
            this.ViewNegReport.Click += new System.EventHandler(this.ViewNegReport_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(207, 6);
            // 
            // ImageZoomIn
            // 
            this.ImageZoomIn.Name = "ImageZoomIn";
            this.ImageZoomIn.Size = new System.Drawing.Size(210, 30);
            this.ImageZoomIn.Text = "图片放大";
            this.ImageZoomIn.Click += new System.EventHandler(this.ImageZoomIn_Click);
            // 
            // ImageZoomOut
            // 
            this.ImageZoomOut.Name = "ImageZoomOut";
            this.ImageZoomOut.Size = new System.Drawing.Size(210, 30);
            this.ImageZoomOut.Text = "图片缩小";
            this.ImageZoomOut.Click += new System.EventHandler(this.ImageZoomOut_Click);
            // 
            // MenuHelp
            // 
            this.MenuHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.VersionInfo});
            this.MenuHelp.Name = "MenuHelp";
            this.MenuHelp.Size = new System.Drawing.Size(84, 28);
            this.MenuHelp.Text = "帮助(&H)";
            // 
            // VersionInfo
            // 
            this.VersionInfo.Name = "VersionInfo";
            this.VersionInfo.Size = new System.Drawing.Size(210, 30);
            this.VersionInfo.Text = "版本信息";
            this.VersionInfo.Click += new System.EventHandler(this.VersionInfo_Click);
            // 
            // toolStrip
            // 
            this.toolStrip.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolOpen,
            this.ToolSave,
            this.toolPosComputation,
            this.ToolInvComputation,
            this.toolStripSeparator1,
            this.ToolZoomIn,
            this.ToolZoomOut,
            this.toolStripSeparator2,
            this.toolHelp,
            this.EllipsoidA,
            this.toolStripLabel1,
            this.toolStripLabel2,
            this.Ellipsoidf_,
            this.toolStripSeparator6,
            this.caltype});
            this.toolStrip.Location = new System.Drawing.Point(0, 34);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Padding = new System.Windows.Forms.Padding(0, 0, 2, 0);
            this.toolStrip.Size = new System.Drawing.Size(978, 31);
            this.toolStrip.TabIndex = 1;
            this.toolStrip.Text = "toolStrip1";
            // 
            // ToolOpen
            // 
            this.ToolOpen.Image = ((System.Drawing.Image)(resources.GetObject("ToolOpen.Image")));
            this.ToolOpen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ToolOpen.Name = "ToolOpen";
            this.ToolOpen.Size = new System.Drawing.Size(74, 28);
            this.ToolOpen.Text = "打开";
            this.ToolOpen.Click += new System.EventHandler(this.ToolOpen_Click);
            // 
            // ToolSave
            // 
            this.ToolSave.Image = ((System.Drawing.Image)(resources.GetObject("ToolSave.Image")));
            this.ToolSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ToolSave.Name = "ToolSave";
            this.ToolSave.Size = new System.Drawing.Size(74, 28);
            this.ToolSave.Text = "保存";
            this.ToolSave.Click += new System.EventHandler(this.ToolSave_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 31);
            // 
            // ToolZoomIn
            // 
            this.ToolZoomIn.Image = ((System.Drawing.Image)(resources.GetObject("ToolZoomIn.Image")));
            this.ToolZoomIn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ToolZoomIn.Name = "ToolZoomIn";
            this.ToolZoomIn.Size = new System.Drawing.Size(74, 28);
            this.ToolZoomIn.Text = "放大";
            this.ToolZoomIn.Click += new System.EventHandler(this.ToolZoomIn_Click);
            // 
            // ToolZoomOut
            // 
            this.ToolZoomOut.Image = ((System.Drawing.Image)(resources.GetObject("ToolZoomOut.Image")));
            this.ToolZoomOut.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ToolZoomOut.Name = "ToolZoomOut";
            this.ToolZoomOut.Size = new System.Drawing.Size(74, 28);
            this.ToolZoomOut.Text = "缩小";
            this.ToolZoomOut.Click += new System.EventHandler(this.ToolZoomOut_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 31);
            // 
            // EllipsoidA
            // 
            this.EllipsoidA.Name = "EllipsoidA";
            this.EllipsoidA.Size = new System.Drawing.Size(82, 28);
            this.EllipsoidA.Text = "长半轴：";
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(0, 28);
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(20, 28);
            this.toolStripLabel2.Text = "  ";
            // 
            // Ellipsoidf_
            // 
            this.Ellipsoidf_.Name = "Ellipsoidf_";
            this.Ellipsoidf_.Size = new System.Drawing.Size(64, 28);
            this.Ellipsoidf_.Text = "扁率：";
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(6, 31);
            // 
            // caltype
            // 
            this.caltype.Name = "caltype";
            this.caltype.Size = new System.Drawing.Size(64, 28);
            this.caltype.Text = "类型：";
            // 
            // tabControl
            // 
            this.tabControl.Alignment = System.Windows.Forms.TabAlignment.Bottom;
            this.tabControl.Controls.Add(this.EditPage);
            this.tabControl.Controls.Add(this.ImagePage);
            this.tabControl.Controls.Add(this.ReportPage);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Location = new System.Drawing.Point(0, 65);
            this.tabControl.Margin = new System.Windows.Forms.Padding(4);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(978, 497);
            this.tabControl.TabIndex = 2;
            // 
            // EditPage
            // 
            this.EditPage.Controls.Add(this.dataGridViewNeg);
            this.EditPage.Controls.Add(this.dataGridViewPos);
            this.EditPage.Location = new System.Drawing.Point(4, 4);
            this.EditPage.Margin = new System.Windows.Forms.Padding(4);
            this.EditPage.Name = "EditPage";
            this.EditPage.Padding = new System.Windows.Forms.Padding(4);
            this.EditPage.Size = new System.Drawing.Size(970, 465);
            this.EditPage.TabIndex = 0;
            this.EditPage.Text = "编辑";
            this.EditPage.UseVisualStyleBackColor = true;
            // 
            // dataGridViewNeg
            // 
            this.dataGridViewNeg.AllowUserToAddRows = false;
            this.dataGridViewNeg.AllowUserToDeleteRows = false;
            this.dataGridViewNeg.AllowUserToResizeColumns = false;
            this.dataGridViewNeg.AllowUserToResizeRows = false;
            this.dataGridViewNeg.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewNeg.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dataGridViewNeg.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewNeg.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewNeg.Location = new System.Drawing.Point(4, 4);
            this.dataGridViewNeg.Margin = new System.Windows.Forms.Padding(4);
            this.dataGridViewNeg.Name = "dataGridViewNeg";
            this.dataGridViewNeg.RowTemplate.Height = 23;
            this.dataGridViewNeg.Size = new System.Drawing.Size(962, 457);
            this.dataGridViewNeg.TabIndex = 1;
            // 
            // dataGridViewPos
            // 
            this.dataGridViewPos.AllowUserToAddRows = false;
            this.dataGridViewPos.AllowUserToDeleteRows = false;
            this.dataGridViewPos.AllowUserToResizeColumns = false;
            this.dataGridViewPos.AllowUserToResizeRows = false;
            this.dataGridViewPos.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewPos.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dataGridViewPos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewPos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewPos.Location = new System.Drawing.Point(4, 4);
            this.dataGridViewPos.Margin = new System.Windows.Forms.Padding(4);
            this.dataGridViewPos.Name = "dataGridViewPos";
            this.dataGridViewPos.RowTemplate.Height = 23;
            this.dataGridViewPos.Size = new System.Drawing.Size(962, 457);
            this.dataGridViewPos.TabIndex = 0;
            // 
            // ImagePage
            // 
            this.ImagePage.Controls.Add(this.pictureBox);
            this.ImagePage.Location = new System.Drawing.Point(4, 4);
            this.ImagePage.Margin = new System.Windows.Forms.Padding(4);
            this.ImagePage.Name = "ImagePage";
            this.ImagePage.Padding = new System.Windows.Forms.Padding(4);
            this.ImagePage.Size = new System.Drawing.Size(970, 465);
            this.ImagePage.TabIndex = 1;
            this.ImagePage.Text = "图形";
            this.ImagePage.UseVisualStyleBackColor = true;
            // 
            // pictureBox
            // 
            this.pictureBox.Location = new System.Drawing.Point(112, 14);
            this.pictureBox.Margin = new System.Windows.Forms.Padding(4);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(620, 480);
            this.pictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox.TabIndex = 0;
            this.pictureBox.TabStop = false;
            this.pictureBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseDown);
            this.pictureBox.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseMove);
            this.pictureBox.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseUp);
            // 
            // ReportPage
            // 
            this.ReportPage.Controls.Add(this.richTextBox);
            this.ReportPage.Location = new System.Drawing.Point(4, 4);
            this.ReportPage.Margin = new System.Windows.Forms.Padding(4);
            this.ReportPage.Name = "ReportPage";
            this.ReportPage.Size = new System.Drawing.Size(970, 465);
            this.ReportPage.TabIndex = 2;
            this.ReportPage.Text = "报告";
            this.ReportPage.UseVisualStyleBackColor = true;
            // 
            // richTextBox
            // 
            this.richTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.richTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBox.Location = new System.Drawing.Point(0, 0);
            this.richTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.richTextBox.Name = "richTextBox";
            this.richTextBox.Size = new System.Drawing.Size(970, 465);
            this.richTextBox.TabIndex = 0;
            this.richTextBox.Text = "";
            // 
            // toolPosComputation
            // 
            this.toolPosComputation.Image = ((System.Drawing.Image)(resources.GetObject("toolPosComputation.Image")));
            this.toolPosComputation.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolPosComputation.Name = "toolPosComputation";
            this.toolPosComputation.Size = new System.Drawing.Size(74, 28);
            this.toolPosComputation.Text = "正算";
            this.toolPosComputation.Click += new System.EventHandler(this.toolPosComputation_Click);
            // 
            // ToolInvComputation
            // 
            this.ToolInvComputation.Image = ((System.Drawing.Image)(resources.GetObject("ToolInvComputation.Image")));
            this.ToolInvComputation.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ToolInvComputation.Name = "ToolInvComputation";
            this.ToolInvComputation.Size = new System.Drawing.Size(74, 28);
            this.ToolInvComputation.Text = "反算";
            this.ToolInvComputation.Click += new System.EventHandler(this.ToolInvComputation_Click);
            // 
            // toolHelp
            // 
            this.toolHelp.Image = ((System.Drawing.Image)(resources.GetObject("toolHelp.Image")));
            this.toolHelp.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolHelp.Name = "toolHelp";
            this.toolHelp.Size = new System.Drawing.Size(74, 28);
            this.toolHelp.Text = "帮助";
            this.toolHelp.Click += new System.EventHandler(this.toolHelp_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(978, 562);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.toolStrip);
            this.Controls.Add(this.menuStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "大地主题正反算";
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.tabControl.ResumeLayout(false);
            this.EditPage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewNeg)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewPos)).EndInit();
            this.ImagePage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            this.ReportPage.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem MenuFile;
      //  private System.Windows.Forms.ToolStripMenuItem ImportPosData;
        private System.Windows.Forms.ToolStripMenuItem SavePosData;
        private System.Windows.Forms.ToolStripMenuItem SaveNegData;
        private System.Windows.Forms.ToolStripMenuItem SaveImage;
        private System.Windows.Forms.ToolStripMenuItem SaveReport;
        private System.Windows.Forms.ToolStripMenuItem Exit;
        private System.Windows.Forms.ToolStripMenuItem MenuComputation;
        private System.Windows.Forms.ToolStripMenuItem PosComputation;
        private System.Windows.Forms.ToolStripMenuItem NegComputation;
        private System.Windows.Forms.ToolStripMenuItem MenuView;
        private System.Windows.Forms.ToolStripMenuItem ViewPosData;
        private System.Windows.Forms.ToolStripMenuItem ViewNegData;
        private System.Windows.Forms.ToolStripMenuItem ViewNegImage;
        private System.Windows.Forms.ToolStripMenuItem ViewReport;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem ImageZoomIn;
        private System.Windows.Forms.ToolStripMenuItem ImageZoomOut;
        private System.Windows.Forms.ToolStripMenuItem MenuHelp;
        private System.Windows.Forms.ToolStripMenuItem VersionInfo;
        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.ToolStripButton ToolOpen;
        private System.Windows.Forms.ToolStripButton ToolSave;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton ToolZoomIn;
        private System.Windows.Forms.ToolStripButton ToolZoomOut;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripLabel EllipsoidA;
        private System.Windows.Forms.ToolStripLabel Ellipsoidf_;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage EditPage;
        private System.Windows.Forms.DataGridView dataGridViewPos;
        private System.Windows.Forms.TabPage ImagePage;
        private System.Windows.Forms.PictureBox pictureBox;
        private System.Windows.Forms.TabPage ReportPage;
        private System.Windows.Forms.RichTextBox richTextBox;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
        private System.Windows.Forms.DataGridView dataGridViewNeg;
        private System.Windows.Forms.ToolStripMenuItem OpenNegData;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripMenuItem OpenPosData;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripLabel caltype;
        private System.Windows.Forms.ToolStripMenuItem ViewPosImage;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator8;
        private System.Windows.Forms.ToolStripMenuItem ViewNegReport;
        private System.Windows.Forms.ToolStripButton toolPosComputation;
        private System.Windows.Forms.ToolStripButton ToolInvComputation;
        private System.Windows.Forms.ToolStripButton toolHelp;
    }
}

