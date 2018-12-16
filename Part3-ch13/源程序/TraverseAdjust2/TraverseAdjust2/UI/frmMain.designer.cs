namespace TraverseAdjust
{
    partial class frmMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.picMain = new System.Windows.Forms.PictureBox();
            this.sbMain = new System.Windows.Forms.StatusStrip();
            this.sbLable_Author = new System.Windows.Forms.ToolStripStatusLabel();
            this.sbLable_Time = new System.Windows.Forms.ToolStripStatusLabel();
            this.tbMain = new System.Windows.Forms.ToolStrip();
            this.tbbProcess = new System.Windows.Forms.ToolStripButton();
            this.ToolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tbbDesignDoc = new System.Windows.Forms.ToolStripButton();
            this.tbbAbout = new System.Windows.Forms.ToolStripButton();
            this.mnuMain = new System.Windows.Forms.MenuStrip();
            this.mnuFile = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFile_Process = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFile_Bar = new System.Windows.Forms.ToolStripSeparator();
            this.mnuFile_Exit = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuHelp_DesignDoc = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuHelp_About = new System.Windows.Forms.ToolStripMenuItem();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.picMain)).BeginInit();
            this.sbMain.SuspendLayout();
            this.tbMain.SuspendLayout();
            this.mnuMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // picMain
            // 
            this.picMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picMain.Image = ((System.Drawing.Image)(resources.GetObject("picMain.Image")));
            this.picMain.Location = new System.Drawing.Point(0, 50);
            this.picMain.Name = "picMain";
            this.picMain.Size = new System.Drawing.Size(844, 441);
            this.picMain.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picMain.TabIndex = 7;
            this.picMain.TabStop = false;
            // 
            // sbMain
            // 
            this.sbMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sbLable_Author,
            this.sbLable_Time});
            this.sbMain.Location = new System.Drawing.Point(0, 491);
            this.sbMain.Name = "sbMain";
            this.sbMain.Size = new System.Drawing.Size(844, 22);
            this.sbMain.TabIndex = 6;
            this.sbMain.Text = "StatusStrip1";
            // 
            // sbLable_Author
            // 
            this.sbLable_Author.Image = ((System.Drawing.Image)(resources.GetObject("sbLable_Author.Image")));
            this.sbLable_Author.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.sbLable_Author.Name = "sbLable_Author";
            this.sbLable_Author.Size = new System.Drawing.Size(829, 17);
            this.sbLable_Author.Spring = true;
            this.sbLable_Author.Text = "安徽建筑大学测绘工程专业教研室";
            this.sbLable_Author.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // sbLable_Time
            // 
            this.sbLable_Time.Name = "sbLable_Time";
            this.sbLable_Time.Size = new System.Drawing.Size(0, 17);
            // 
            // tbMain
            // 
            this.tbMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tbbProcess,
            this.ToolStripSeparator1,
            this.tbbDesignDoc,
            this.tbbAbout});
            this.tbMain.Location = new System.Drawing.Point(0, 25);
            this.tbMain.Name = "tbMain";
            this.tbMain.Size = new System.Drawing.Size(844, 25);
            this.tbMain.TabIndex = 5;
            this.tbMain.Text = "ToolStrip1";
            this.tbMain.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.tbMain_ItemClicked);
            // 
            // tbbProcess
            // 
            this.tbbProcess.Image = ((System.Drawing.Image)(resources.GetObject("tbbProcess.Image")));
            this.tbbProcess.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbbProcess.Name = "tbbProcess";
            this.tbbProcess.Size = new System.Drawing.Size(126, 22);
            this.tbbProcess.Tag = "Process";
            this.tbbProcess.Text = "导线数据处理(&D)...";
            this.tbbProcess.ToolTipText = "前方交汇数据处理";
            // 
            // ToolStripSeparator1
            // 
            this.ToolStripSeparator1.Name = "ToolStripSeparator1";
            this.ToolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // tbbDesignDoc
            // 
            this.tbbDesignDoc.Image = ((System.Drawing.Image)(resources.GetObject("tbbDesignDoc.Image")));
            this.tbbDesignDoc.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbbDesignDoc.Name = "tbbDesignDoc";
            this.tbbDesignDoc.Size = new System.Drawing.Size(120, 22);
            this.tbbDesignDoc.Tag = "DesignDoc";
            this.tbbDesignDoc.Text = "程序开发文档(&W)";
            // 
            // tbbAbout
            // 
            this.tbbAbout.Image = ((System.Drawing.Image)(resources.GetObject("tbbAbout.Image")));
            this.tbbAbout.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbbAbout.Name = "tbbAbout";
            this.tbbAbout.Size = new System.Drawing.Size(68, 22);
            this.tbbAbout.Tag = "About";
            this.tbbAbout.Text = "关于(&A)";
            // 
            // mnuMain
            // 
            this.mnuMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuFile,
            this.mnuHelp});
            this.mnuMain.Location = new System.Drawing.Point(0, 0);
            this.mnuMain.Name = "mnuMain";
            this.mnuMain.Size = new System.Drawing.Size(844, 25);
            this.mnuMain.TabIndex = 4;
            this.mnuMain.Text = "MenuStrip1";
            // 
            // mnuFile
            // 
            this.mnuFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuFile_Process,
            this.mnuFile_Bar,
            this.mnuFile_Exit});
            this.mnuFile.Name = "mnuFile";
            this.mnuFile.Size = new System.Drawing.Size(58, 21);
            this.mnuFile.Text = "文件(&F)";
            this.mnuFile.DropDownItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.DropDownItemClicked);
            // 
            // mnuFile_Process
            // 
            this.mnuFile_Process.Image = ((System.Drawing.Image)(resources.GetObject("mnuFile_Process.Image")));
            this.mnuFile_Process.Name = "mnuFile_Process";
            this.mnuFile_Process.Size = new System.Drawing.Size(174, 22);
            this.mnuFile_Process.Tag = "Process";
            this.mnuFile_Process.Text = "导线数据处理(&D)...";
            // 
            // mnuFile_Bar
            // 
            this.mnuFile_Bar.Name = "mnuFile_Bar";
            this.mnuFile_Bar.Size = new System.Drawing.Size(171, 6);
            // 
            // mnuFile_Exit
            // 
            this.mnuFile_Exit.Image = ((System.Drawing.Image)(resources.GetObject("mnuFile_Exit.Image")));
            this.mnuFile_Exit.Name = "mnuFile_Exit";
            this.mnuFile_Exit.Size = new System.Drawing.Size(174, 22);
            this.mnuFile_Exit.Tag = "Exit";
            this.mnuFile_Exit.Text = "退出(&E)";
            // 
            // mnuHelp
            // 
            this.mnuHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuHelp_DesignDoc,
            this.mnuHelp_About});
            this.mnuHelp.Name = "mnuHelp";
            this.mnuHelp.Size = new System.Drawing.Size(61, 21);
            this.mnuHelp.Text = "帮助(&H)";
            this.mnuHelp.DropDownItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.DropDownItemClicked);
            // 
            // mnuHelp_DesignDoc
            // 
            this.mnuHelp_DesignDoc.Image = ((System.Drawing.Image)(resources.GetObject("mnuHelp_DesignDoc.Image")));
            this.mnuHelp_DesignDoc.Name = "mnuHelp_DesignDoc";
            this.mnuHelp_DesignDoc.Size = new System.Drawing.Size(177, 22);
            this.mnuHelp_DesignDoc.Tag = "DesignDoc";
            this.mnuHelp_DesignDoc.Text = "程序开发文档(&W)...";
            // 
            // mnuHelp_About
            // 
            this.mnuHelp_About.Image = ((System.Drawing.Image)(resources.GetObject("mnuHelp_About.Image")));
            this.mnuHelp_About.Name = "mnuHelp_About";
            this.mnuHelp_About.Size = new System.Drawing.Size(177, 22);
            this.mnuHelp_About.Tag = "About";
            this.mnuHelp_About.Text = "关于(&A)...";
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.Timer1_Tick);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(844, 513);
            this.Controls.Add(this.picMain);
            this.Controls.Add(this.sbMain);
            this.Controls.Add(this.tbMain);
            this.Controls.Add(this.mnuMain);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "导线简易平差计算程序";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmMain_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.picMain)).EndInit();
            this.sbMain.ResumeLayout(false);
            this.sbMain.PerformLayout();
            this.tbMain.ResumeLayout(false);
            this.tbMain.PerformLayout();
            this.mnuMain.ResumeLayout(false);
            this.mnuMain.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.PictureBox picMain;
        internal System.Windows.Forms.StatusStrip sbMain;
        internal System.Windows.Forms.ToolStripStatusLabel sbLable_Author;
        internal System.Windows.Forms.ToolStripStatusLabel sbLable_Time;
        internal System.Windows.Forms.ToolStrip tbMain;
        internal System.Windows.Forms.ToolStripButton tbbProcess;
        internal System.Windows.Forms.ToolStripSeparator ToolStripSeparator1;
        internal System.Windows.Forms.ToolStripButton tbbDesignDoc;
        internal System.Windows.Forms.ToolStripButton tbbAbout;
        internal System.Windows.Forms.MenuStrip mnuMain;
        internal System.Windows.Forms.ToolStripMenuItem mnuFile;
        internal System.Windows.Forms.ToolStripMenuItem mnuFile_Process;
        internal System.Windows.Forms.ToolStripSeparator mnuFile_Bar;
        internal System.Windows.Forms.ToolStripMenuItem mnuFile_Exit;
        internal System.Windows.Forms.ToolStripMenuItem mnuHelp;
        internal System.Windows.Forms.ToolStripMenuItem mnuHelp_DesignDoc;
        internal System.Windows.Forms.ToolStripMenuItem mnuHelp_About;
        private System.Windows.Forms.Timer timer1;
    }
}