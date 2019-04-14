namespace Map.UI
{
    partial class frmMap
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMap));
            this.tb1 = new System.Windows.Forms.ToolStrip();
            this.tbSave = new System.Windows.Forms.ToolStripButton();
            this.ToolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tbZoomIn = new System.Windows.Forms.ToolStripButton();
            this.tbZoomOut = new System.Windows.Forms.ToolStripButton();
            this.ToolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btBackground = new System.Windows.Forms.ToolStripButton();
            this.btHasCoord = new System.Windows.Forms.ToolStripButton();
            this.sb1 = new System.Windows.Forms.StatusStrip();
            this.sbLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.sbLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.picMap = new System.Windows.Forms.PictureBox();
            this.tb1.SuspendLayout();
            this.sb1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picMap)).BeginInit();
            this.SuspendLayout();
            // 
            // tb1
            // 
            this.tb1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tbSave,
            this.ToolStripSeparator1,
            this.tbZoomIn,
            this.tbZoomOut,
            this.ToolStripSeparator2,
            this.btBackground,
            this.btHasCoord});
            this.tb1.Location = new System.Drawing.Point(0, 0);
            this.tb1.Name = "tb1";
            this.tb1.Size = new System.Drawing.Size(684, 25);
            this.tb1.TabIndex = 6;
            this.tb1.Text = "ToolStrip1";
            // 
            // tbSave
            // 
            this.tbSave.Image = ((System.Drawing.Image)(resources.GetObject("tbSave.Image")));
            this.tbSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbSave.Name = "tbSave";
            this.tbSave.Size = new System.Drawing.Size(100, 22);
            this.tbSave.Text = "图形转存(&S)...";
            this.tbSave.Click += new System.EventHandler(this.tbSave_Click);
            // 
            // ToolStripSeparator1
            // 
            this.ToolStripSeparator1.Name = "ToolStripSeparator1";
            this.ToolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // tbZoomIn
            // 
            this.tbZoomIn.Image = ((System.Drawing.Image)(resources.GetObject("tbZoomIn.Image")));
            this.tbZoomIn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbZoomIn.Name = "tbZoomIn";
            this.tbZoomIn.Size = new System.Drawing.Size(52, 22);
            this.tbZoomIn.Text = "放大";
            this.tbZoomIn.ToolTipText = "单击放大2倍";
            this.tbZoomIn.Click += new System.EventHandler(this.tbZoomIn_Click);
            // 
            // tbZoomOut
            // 
            this.tbZoomOut.Image = ((System.Drawing.Image)(resources.GetObject("tbZoomOut.Image")));
            this.tbZoomOut.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbZoomOut.Name = "tbZoomOut";
            this.tbZoomOut.Size = new System.Drawing.Size(52, 22);
            this.tbZoomOut.Text = "缩小";
            this.tbZoomOut.ToolTipText = "单击缩小2倍";
            this.tbZoomOut.Click += new System.EventHandler(this.tbZoomOut_Click);
            // 
            // ToolStripSeparator2
            // 
            this.ToolStripSeparator2.Name = "ToolStripSeparator2";
            this.ToolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // btBackground
            // 
            this.btBackground.Image = ((System.Drawing.Image)(resources.GetObject("btBackground.Image")));
            this.btBackground.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btBackground.Name = "btBackground";
            this.btBackground.Size = new System.Drawing.Size(76, 22);
            this.btBackground.Text = "切换背景";
            this.btBackground.ToolTipText = "设置为白色背景";
            this.btBackground.Click += new System.EventHandler(this.btBackground_Click);
            // 
            // btHasCoord
            // 
            this.btHasCoord.Image = ((System.Drawing.Image)(resources.GetObject("btHasCoord.Image")));
            this.btHasCoord.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btHasCoord.Name = "btHasCoord";
            this.btHasCoord.Size = new System.Drawing.Size(76, 22);
            this.btHasCoord.Text = "无坐标轴";
            this.btHasCoord.ToolTipText = "取消坐标轴";
            this.btHasCoord.Click += new System.EventHandler(this.btHasCoord_Click);
            // 
            // sb1
            // 
            this.sb1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sbLabel1,
            this.sbLabel2});
            this.sb1.Location = new System.Drawing.Point(0, 439);
            this.sb1.Name = "sb1";
            this.sb1.Size = new System.Drawing.Size(684, 22);
            this.sb1.TabIndex = 7;
            this.sb1.Text = "StatusStrip1";
            // 
            // sbLabel1
            // 
            this.sbLabel1.Name = "sbLabel1";
            this.sbLabel1.Size = new System.Drawing.Size(669, 17);
            this.sbLabel1.Spring = true;
            this.sbLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // sbLabel2
            // 
            this.sbLabel2.Name = "sbLabel2";
            this.sbLabel2.Size = new System.Drawing.Size(0, 17);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // picMap
            // 
            this.picMap.BackColor = System.Drawing.Color.Black;
            this.picMap.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picMap.Location = new System.Drawing.Point(0, 25);
            this.picMap.Name = "picMap";
            this.picMap.Size = new System.Drawing.Size(684, 414);
            this.picMap.TabIndex = 9;
            this.picMap.TabStop = false;
            this.picMap.MouseDown += new System.Windows.Forms.MouseEventHandler(this.picMap_MouseDown);
            this.picMap.MouseMove += new System.Windows.Forms.MouseEventHandler(this.picMap_MouseMove);
            this.picMap.MouseUp += new System.Windows.Forms.MouseEventHandler(this.picMap_MouseUp);
            // 
            // frmMap
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(684, 461);
            this.Controls.Add(this.picMap);
            this.Controls.Add(this.tb1);
            this.Controls.Add(this.sb1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmMap";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "图形显示";
            this.tb1.ResumeLayout(false);
            this.tb1.PerformLayout();
            this.sb1.ResumeLayout(false);
            this.sb1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picMap)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.ToolStrip tb1;
        internal System.Windows.Forms.ToolStripButton tbSave;
        internal System.Windows.Forms.ToolStripSeparator ToolStripSeparator1;
        internal System.Windows.Forms.ToolStripButton tbZoomIn;
        internal System.Windows.Forms.ToolStripButton tbZoomOut;
        internal System.Windows.Forms.ToolStripSeparator ToolStripSeparator2;
        internal System.Windows.Forms.ToolStripButton btBackground;
        internal System.Windows.Forms.ToolStripButton btHasCoord;
        internal System.Windows.Forms.StatusStrip sb1;
        internal System.Windows.Forms.ToolStripStatusLabel sbLabel1;
        internal System.Windows.Forms.ToolStripStatusLabel sbLabel2;
        private System.Windows.Forms.Timer timer1;
        internal System.Windows.Forms.PictureBox picMap;
    }
}