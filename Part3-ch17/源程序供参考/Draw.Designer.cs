namespace 开采沉陷计算
{
    partial class Draw
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.保存DXFToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.图像复位ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.图像复位ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.保存BMPToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.保存DXFToolStripMenuItem,
            this.图像复位ToolStripMenuItem,
            this.图像复位ToolStripMenuItem1,
            this.保存BMPToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(829, 25);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 保存DXFToolStripMenuItem
            // 
            this.保存DXFToolStripMenuItem.Name = "保存DXFToolStripMenuItem";
            this.保存DXFToolStripMenuItem.Size = new System.Drawing.Size(67, 21);
            this.保存DXFToolStripMenuItem.Text = "保存DXF";
            this.保存DXFToolStripMenuItem.Click += new System.EventHandler(this.保存DXFToolStripMenuItem_Click);
            // 
            // 图像复位ToolStripMenuItem
            // 
            this.图像复位ToolStripMenuItem.Name = "图像复位ToolStripMenuItem";
            this.图像复位ToolStripMenuItem.Size = new System.Drawing.Size(12, 21);
            // 
            // 图像复位ToolStripMenuItem1
            // 
            this.图像复位ToolStripMenuItem1.Name = "图像复位ToolStripMenuItem1";
            this.图像复位ToolStripMenuItem1.Size = new System.Drawing.Size(68, 21);
            this.图像复位ToolStripMenuItem1.Text = "图像复位";
            this.图像复位ToolStripMenuItem1.Click += new System.EventHandler(this.图像复位ToolStripMenuItem1_Click);
            // 
            // 保存BMPToolStripMenuItem
            // 
            this.保存BMPToolStripMenuItem.Name = "保存BMPToolStripMenuItem";
            this.保存BMPToolStripMenuItem.Size = new System.Drawing.Size(71, 21);
            this.保存BMPToolStripMenuItem.Text = "保存BMP";
            this.保存BMPToolStripMenuItem.Click += new System.EventHandler(this.保存BMPToolStripMenuItem_Click_1);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(0, 28);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(829, 448);
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseMove);
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.FileOk += new System.ComponentModel.CancelEventHandler(this.saveFileDialog1_FileOk);
            // 
            // Draw
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(829, 478);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Draw";
            this.Text = "绘图";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Draw_FormClosing);
            this.Load += new System.EventHandler(this.Draw_Load);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseWheel);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 保存DXFToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 图像复位ToolStripMenuItem;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ToolStripMenuItem 图像复位ToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem 保存BMPToolStripMenuItem;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
    }
}