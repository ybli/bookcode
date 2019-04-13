using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Iono
{
    public partial class Form1 : Form
    {
        public string re;
        private DataEntity Data;
        public Form1()
        {
            InitializeComponent();
        }//初始化

        private void toolOpen_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                Data = FileHelper.Read(openFileDialog1.FileName);
                richTextBox1.Text = Data.ToString();
            }//打开文件
        }

        private void toolCal_Click(object sender, EventArgs e)
        {
            Algo go=new Algo(Data);

            richTextBox1.Text = go.Compute();
            re = go.Compute();
        }//进行计算

        private void toolSave_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                FileHelper.Write(re, saveFileDialog1.FileName);
            }
        }//保存文件

        private void toolHelp_Click(object sender, EventArgs e)
        {
            string copyright = "《测绘程序设计试题集（试题7 电离层改正）》配套程序\n作者：李英冰\n";
            copyright += "武汉大学测绘学院\r\nEMAIL: ybli@sgg.whu.edu.cn\r\n2017.7.27";
            richTextBox1.Text = copyright;
        }//帮助

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
