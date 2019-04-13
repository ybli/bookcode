namespace CalculationOfControlArea
{
    partial class BoundaryForm
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
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.bPointList = new System.Windows.Forms.ListView();
            this.label6 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textX = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.textY = new System.Windows.Forms.TextBox();
            this.admitBtn = new System.Windows.Forms.Button();
            this.deleteBtn = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBox3 = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.sheetNum = new System.Windows.Forms.Label();
            this.sheetNumText = new System.Windows.Forms.TextBox();
            this.sub = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(546, 59);
            this.comboBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.comboBox1.MaxLength = 20;
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 26);
            this.comboBox1.Sorted = true;
            this.comboBox1.TabIndex = 6;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(469, 60);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 18);
            this.label1.TabIndex = 2;
            this.label1.Text = "代码：";
            // 
            // bPointList
            // 
            this.bPointList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.bPointList.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.bPointList.FullRowSelect = true;
            this.bPointList.Location = new System.Drawing.Point(14, 98);
            this.bPointList.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.bPointList.Name = "bPointList";
            this.bPointList.Size = new System.Drawing.Size(1135, 460);
            this.bPointList.TabIndex = 4;
            this.bPointList.UseCompatibleStateImageBehavior = false;
            this.bPointList.View = System.Windows.Forms.View.Details;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label6.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.Location = new System.Drawing.Point(468, 18);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(210, 24);
            this.label6.TabIndex = 4;
            this.label6.Text = "行政区域边界信息";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(52, 50);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(134, 18);
            this.label3.TabIndex = 3;
            this.label3.Text = "行政区域代码：";
            // 
            // comboBox2
            // 
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Location = new System.Drawing.Point(186, 46);
            this.comboBox2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(121, 26);
            this.comboBox2.Sorted = true;
            this.comboBox2.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(314, 52);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(107, 18);
            this.label4.TabIndex = 7;
            this.label4.Text = "高斯坐标X：";
            // 
            // textX
            // 
            this.textX.Location = new System.Drawing.Point(422, 44);
            this.textX.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textX.Name = "textX";
            this.textX.Size = new System.Drawing.Size(131, 28);
            this.textX.TabIndex = 8;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(566, 52);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(107, 18);
            this.label5.TabIndex = 9;
            this.label5.Text = "高斯坐标Y：";
            // 
            // textY
            // 
            this.textY.Location = new System.Drawing.Point(674, 42);
            this.textY.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textY.Name = "textY";
            this.textY.Size = new System.Drawing.Size(131, 28);
            this.textY.TabIndex = 10;
            // 
            // admitBtn
            // 
            this.admitBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.admitBtn.Location = new System.Drawing.Point(952, 628);
            this.admitBtn.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.admitBtn.Name = "admitBtn";
            this.admitBtn.Size = new System.Drawing.Size(112, 30);
            this.admitBtn.TabIndex = 11;
            this.admitBtn.Text = "添加数据点";
            this.admitBtn.UseVisualStyleBackColor = true;
            this.admitBtn.Click += new System.EventHandler(this.admitBtn_Click);
            // 
            // deleteBtn
            // 
            this.deleteBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.deleteBtn.Location = new System.Drawing.Point(952, 678);
            this.deleteBtn.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.deleteBtn.Name = "deleteBtn";
            this.deleteBtn.Size = new System.Drawing.Size(112, 30);
            this.deleteBtn.TabIndex = 12;
            this.deleteBtn.Text = "删除数据点";
            this.deleteBtn.UseVisualStyleBackColor = true;
            this.deleteBtn.Click += new System.EventHandler(this.deleteBtn_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.textY);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.textX);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.comboBox2);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Location = new System.Drawing.Point(17, 583);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox1.Size = new System.Drawing.Size(849, 154);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "新增数据点";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(88, 120);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(476, 18);
            this.label7.TabIndex = 12;
            this.label7.Text = "请按边界顺时针或逆时针输入，首尾若不一致将自动相连。";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(52, 96);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(548, 18);
            this.label2.TabIndex = 11;
            this.label2.Text = "注：高斯坐标以米为单位，保留4位小数，小数点后第5位四舍五入。";
            // 
            // comboBox3
            // 
            this.comboBox3.FormattingEnabled = true;
            this.comboBox3.Location = new System.Drawing.Point(326, 55);
            this.comboBox3.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.comboBox3.Name = "comboBox3";
            this.comboBox3.Size = new System.Drawing.Size(136, 26);
            this.comboBox3.TabIndex = 15;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(213, 59);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(80, 18);
            this.label8.TabIndex = 16;
            this.label8.Text = "比例尺：";
            // 
            // sheetNum
            // 
            this.sheetNum.AutoSize = true;
            this.sheetNum.Location = new System.Drawing.Point(718, 60);
            this.sheetNum.Name = "sheetNum";
            this.sheetNum.Size = new System.Drawing.Size(80, 18);
            this.sheetNum.TabIndex = 17;
            this.sheetNum.Text = "图幅号：";
            // 
            // sheetNumText
            // 
            this.sheetNumText.Location = new System.Drawing.Point(822, 56);
            this.sheetNumText.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.sheetNumText.Name = "sheetNumText";
            this.sheetNumText.Size = new System.Drawing.Size(112, 28);
            this.sheetNumText.TabIndex = 18;
            // 
            // sub
            // 
            this.sub.Location = new System.Drawing.Point(952, 54);
            this.sub.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.sub.Name = "sub";
            this.sub.Size = new System.Drawing.Size(102, 34);
            this.sub.TabIndex = 21;
            this.sub.Text = "设置图幅";
            this.sub.UseVisualStyleBackColor = true;
            this.sub.Click += new System.EventHandler(this.sub_Click);
            // 
            // BoundaryForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1203, 768);
            this.Controls.Add(this.sub);
            this.Controls.Add(this.sheetNumText);
            this.Controls.Add(this.sheetNum);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.comboBox3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.deleteBtn);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.admitBtn);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.bPointList);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "BoundaryForm";
            this.Text = "边界数据";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.BoundaryForm_FormClosing);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textX;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textY;
        private System.Windows.Forms.Button admitBtn;
        private System.Windows.Forms.Button deleteBtn;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.ListView bPointList;
        public System.Windows.Forms.ComboBox comboBox1;
        public System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.Label label7;
        public System.Windows.Forms.ComboBox comboBox3;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label sheetNum;
        public System.Windows.Forms.TextBox sheetNumText;
        private System.Windows.Forms.Button sub;
    }
}