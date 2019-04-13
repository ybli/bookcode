namespace DiniRaw2XLS
{
    partial class frmMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.btInput = new System.Windows.Forms.Button();
            this.gbResult = new System.Windows.Forms.GroupBox();
            this.txtRawData = new System.Windows.Forms.RichTextBox();
            this.btOutput = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtUnit = new System.Windows.Forms.TextBox();
            this.gbResult.SuspendLayout();
            this.SuspendLayout();
            // 
            // btInput
            // 
            this.btInput.Image = ((System.Drawing.Image)(resources.GetObject("btInput.Image")));
            this.btInput.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btInput.Location = new System.Drawing.Point(12, 12);
            this.btInput.Name = "btInput";
            this.btInput.Size = new System.Drawing.Size(141, 24);
            this.btInput.TabIndex = 9;
            this.btInput.Text = "导入原始数据(&I)...";
            this.btInput.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btInput.UseVisualStyleBackColor = true;
            this.btInput.Click += new System.EventHandler(this.btInput_Click);
            // 
            // gbResult
            // 
            this.gbResult.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gbResult.Controls.Add(this.txtRawData);
            this.gbResult.Location = new System.Drawing.Point(12, 42);
            this.gbResult.Name = "gbResult";
            this.gbResult.Size = new System.Drawing.Size(1050, 497);
            this.gbResult.TabIndex = 12;
            this.gbResult.TabStop = false;
            this.gbResult.Text = "原始数据";
            // 
            // txtRawData
            // 
            this.txtRawData.BackColor = System.Drawing.Color.Gainsboro;
            this.txtRawData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtRawData.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtRawData.Location = new System.Drawing.Point(3, 17);
            this.txtRawData.Name = "txtRawData";
            this.txtRawData.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedVertical;
            this.txtRawData.Size = new System.Drawing.Size(1044, 477);
            this.txtRawData.TabIndex = 0;
            this.txtRawData.Text = "";
            // 
            // btOutput
            // 
            this.btOutput.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btOutput.Enabled = false;
            this.btOutput.Image = ((System.Drawing.Image)(resources.GetObject("btOutput.Image")));
            this.btOutput.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btOutput.Location = new System.Drawing.Point(930, 545);
            this.btOutput.Name = "btOutput";
            this.btOutput.Size = new System.Drawing.Size(129, 32);
            this.btOutput.TabIndex = 13;
            this.btOutput.Text = "导出手簿(&O)...";
            this.btOutput.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btOutput.UseVisualStyleBackColor = true;
            this.btOutput.Click += new System.EventHandler(this.btOutput_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(202, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 12);
            this.label1.TabIndex = 14;
            this.label1.Text = "测量单位:";
            // 
            // txtUnit
            // 
            this.txtUnit.Location = new System.Drawing.Point(267, 12);
            this.txtUnit.Name = "txtUnit";
            this.txtUnit.Size = new System.Drawing.Size(395, 21);
            this.txtUnit.TabIndex = 15;
            this.txtUnit.Text = "中铁XX局XX公司XX工程项目部";
            this.txtUnit.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1074, 589);
            this.Controls.Add(this.txtUnit);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btOutput);
            this.Controls.Add(this.gbResult);
            this.Controls.Add(this.btInput);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Dini03原始数据转标准手簿";
            this.gbResult.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.Button btInput;
        internal System.Windows.Forms.GroupBox gbResult;
        internal System.Windows.Forms.Button btOutput;
        private System.Windows.Forms.RichTextBox txtRawData;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtUnit;
    }
}

