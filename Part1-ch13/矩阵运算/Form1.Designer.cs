namespace 矩阵运算
{
    partial class MartixProcess
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
            this.LeftMartix = new System.Windows.Forms.RichTextBox();
            this.RightMartix = new System.Windows.Forms.RichTextBox();
            this.ResultMartix = new System.Windows.Forms.RichTextBox();
            this.Transpose = new System.Windows.Forms.Button();
            this.Inverse = new System.Windows.Forms.Button();
            this.Determinant = new System.Windows.Forms.Button();
            this.Left = new System.Windows.Forms.Label();
            this.Right = new System.Windows.Forms.Label();
            this.radioButton_Add = new System.Windows.Forms.RadioButton();
            this.radioButton_Sub = new System.Windows.Forms.RadioButton();
            this.radioButton_Converse = new System.Windows.Forms.RadioButton();
            this.radioButton_Mul = new System.Windows.Forms.RadioButton();
            this.Calculate = new System.Windows.Forms.Button();
            this.LeftMartix2 = new System.Windows.Forms.RichTextBox();
            this.ResultMartix2 = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // LeftMartix
            // 
            this.LeftMartix.Location = new System.Drawing.Point(63, 102);
            this.LeftMartix.Name = "LeftMartix";
            this.LeftMartix.Size = new System.Drawing.Size(232, 169);
            this.LeftMartix.TabIndex = 0;
            this.LeftMartix.Text = "";
            // 
            // RightMartix
            // 
            this.RightMartix.Location = new System.Drawing.Point(389, 98);
            this.RightMartix.Name = "RightMartix";
            this.RightMartix.Size = new System.Drawing.Size(232, 169);
            this.RightMartix.TabIndex = 1;
            this.RightMartix.Text = "";
            // 
            // ResultMartix
            // 
            this.ResultMartix.Location = new System.Drawing.Point(726, 103);
            this.ResultMartix.Name = "ResultMartix";
            this.ResultMartix.Size = new System.Drawing.Size(232, 169);
            this.ResultMartix.TabIndex = 2;
            this.ResultMartix.Text = "";
            // 
            // Transpose
            // 
            this.Transpose.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Transpose.Location = new System.Drawing.Point(450, 326);
            this.Transpose.Name = "Transpose";
            this.Transpose.Size = new System.Drawing.Size(137, 38);
            this.Transpose.TabIndex = 8;
            this.Transpose.Text = "转置";
            this.Transpose.UseVisualStyleBackColor = true;
            this.Transpose.Click += new System.EventHandler(this.Transpose_Click);
            // 
            // Inverse
            // 
            this.Inverse.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Inverse.Location = new System.Drawing.Point(450, 448);
            this.Inverse.Name = "Inverse";
            this.Inverse.Size = new System.Drawing.Size(137, 38);
            this.Inverse.TabIndex = 9;
            this.Inverse.Text = "求逆";
            this.Inverse.UseVisualStyleBackColor = true;
            this.Inverse.Click += new System.EventHandler(this.Inverse_Click);
            // 
            // Determinant
            // 
            this.Determinant.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Determinant.Location = new System.Drawing.Point(450, 390);
            this.Determinant.Name = "Determinant";
            this.Determinant.Size = new System.Drawing.Size(137, 38);
            this.Determinant.TabIndex = 10;
            this.Determinant.Text = "求行列式";
            this.Determinant.UseVisualStyleBackColor = true;
            this.Determinant.Click += new System.EventHandler(this.Determinant_Click);
            // 
            // Left
            // 
            this.Left.AutoSize = true;
            this.Left.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Left.Location = new System.Drawing.Point(63, 66);
            this.Left.Name = "Left";
            this.Left.Size = new System.Drawing.Size(79, 20);
            this.Left.TabIndex = 11;
            this.Left.Text = "左矩阵:";
            // 
            // Right
            // 
            this.Right.AutoSize = true;
            this.Right.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Right.Location = new System.Drawing.Point(398, 62);
            this.Right.Name = "Right";
            this.Right.Size = new System.Drawing.Size(79, 20);
            this.Right.TabIndex = 12;
            this.Right.Text = "右矩阵:";
            // 
            // radioButton_Add
            // 
            this.radioButton_Add.AutoSize = true;
            this.radioButton_Add.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.radioButton_Add.Location = new System.Drawing.Point(315, 103);
            this.radioButton_Add.Name = "radioButton_Add";
            this.radioButton_Add.Size = new System.Drawing.Size(49, 34);
            this.radioButton_Add.TabIndex = 13;
            this.radioButton_Add.TabStop = true;
            this.radioButton_Add.Tag = "";
            this.radioButton_Add.Text = "+";
            this.radioButton_Add.UseVisualStyleBackColor = true;
            // 
            // radioButton_Sub
            // 
            this.radioButton_Sub.AutoSize = true;
            this.radioButton_Sub.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.radioButton_Sub.Location = new System.Drawing.Point(315, 145);
            this.radioButton_Sub.Name = "radioButton_Sub";
            this.radioButton_Sub.Size = new System.Drawing.Size(49, 34);
            this.radioButton_Sub.TabIndex = 14;
            this.radioButton_Sub.TabStop = true;
            this.radioButton_Sub.Tag = "";
            this.radioButton_Sub.Text = "-";
            this.radioButton_Sub.UseVisualStyleBackColor = true;
            // 
            // radioButton_Converse
            // 
            this.radioButton_Converse.AutoSize = true;
            this.radioButton_Converse.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.radioButton_Converse.Location = new System.Drawing.Point(315, 233);
            this.radioButton_Converse.Name = "radioButton_Converse";
            this.radioButton_Converse.Size = new System.Drawing.Size(49, 34);
            this.radioButton_Converse.TabIndex = 16;
            this.radioButton_Converse.TabStop = true;
            this.radioButton_Converse.Tag = "";
            this.radioButton_Converse.Text = "/";
            this.radioButton_Converse.UseVisualStyleBackColor = true;
            // 
            // radioButton_Mul
            // 
            this.radioButton_Mul.AutoSize = true;
            this.radioButton_Mul.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.radioButton_Mul.Location = new System.Drawing.Point(315, 188);
            this.radioButton_Mul.Name = "radioButton_Mul";
            this.radioButton_Mul.Size = new System.Drawing.Size(49, 34);
            this.radioButton_Mul.TabIndex = 15;
            this.radioButton_Mul.TabStop = true;
            this.radioButton_Mul.Tag = "";
            this.radioButton_Mul.Text = "x";
            this.radioButton_Mul.UseVisualStyleBackColor = true;
            // 
            // Calculate
            // 
            this.Calculate.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Calculate.Location = new System.Drawing.Point(647, 170);
            this.Calculate.Name = "Calculate";
            this.Calculate.Size = new System.Drawing.Size(51, 38);
            this.Calculate.TabIndex = 17;
            this.Calculate.Text = "=";
            this.Calculate.UseVisualStyleBackColor = true;
            this.Calculate.Click += new System.EventHandler(this.Calculate_Click);
            // 
            // LeftMartix2
            // 
            this.LeftMartix2.Location = new System.Drawing.Point(67, 326);
            this.LeftMartix2.Name = "LeftMartix2";
            this.LeftMartix2.Size = new System.Drawing.Size(232, 169);
            this.LeftMartix2.TabIndex = 18;
            this.LeftMartix2.Text = "";
            // 
            // ResultMartix2
            // 
            this.ResultMartix2.Location = new System.Drawing.Point(726, 323);
            this.ResultMartix2.Name = "ResultMartix2";
            this.ResultMartix2.Size = new System.Drawing.Size(232, 169);
            this.ResultMartix2.TabIndex = 19;
            this.ResultMartix2.Text = "";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(75, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 15);
            this.label1.TabIndex = 20;
            this.label1.Text = "说明:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(140, 25);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(397, 15);
            this.label2.TabIndex = 21;
            this.label2.Text = "输入矩阵时，两两元素用逗号间隔，行与行之间用回车间隔";
            // 
            // MartixProcess
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1019, 521);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ResultMartix2);
            this.Controls.Add(this.LeftMartix2);
            this.Controls.Add(this.Calculate);
            this.Controls.Add(this.radioButton_Converse);
            this.Controls.Add(this.radioButton_Mul);
            this.Controls.Add(this.radioButton_Sub);
            this.Controls.Add(this.radioButton_Add);
            this.Controls.Add(this.Right);
            this.Controls.Add(this.Left);
            this.Controls.Add(this.Determinant);
            this.Controls.Add(this.Inverse);
            this.Controls.Add(this.Transpose);
            this.Controls.Add(this.ResultMartix);
            this.Controls.Add(this.RightMartix);
            this.Controls.Add(this.LeftMartix);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "MartixProcess";
            this.Text = "矩阵基本运算";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox LeftMartix;
        private System.Windows.Forms.RichTextBox RightMartix;
        private System.Windows.Forms.RichTextBox ResultMartix;
        private System.Windows.Forms.Button Transpose;
        private System.Windows.Forms.Button Inverse;
        private System.Windows.Forms.Button Determinant;
        private System.Windows.Forms.Label Left;
        private System.Windows.Forms.Label Right;
        private System.Windows.Forms.RadioButton radioButton_Add;
        private System.Windows.Forms.RadioButton radioButton_Sub;
        private System.Windows.Forms.RadioButton radioButton_Converse;
        private System.Windows.Forms.RadioButton radioButton_Mul;
        private System.Windows.Forms.Button Calculate;
        private System.Windows.Forms.RichTextBox LeftMartix2;
        private System.Windows.Forms.RichTextBox ResultMartix2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}

