using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        string report;
        public List<PointInfo> point;
        public Form1()
        {
            InitializeComponent();
        }

        private void 打开文件ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            point = new List<PointInfo>();
            OpenFileDialog dialog1 = new OpenFileDialog();
            DialogResult result = dialog1.ShowDialog();
            if(result==DialogResult.OK)
            {
                StreamReader rd = new StreamReader(dialog1.FileName);
                int i = 0;
                string[] str=new string[100];
                while(!rd.EndOfStream)
                {
                    str[i] = rd.ReadLine();
                    i++;
                }
                for(int j=1;j<i;j++)
                {
                    string[] a = str[j].Split(new string[]{" "},StringSplitOptions.RemoveEmptyEntries);
                    PointInfo point1=new PointInfo(a[0],double.Parse(a[1]),double.Parse(a[2]),double.Parse(a[3]));
                    point.Add(point1);
                }
                //显示在屏幕上
                string richtext = "";
                string str1 = "   6\n";
                richtext += str1;
                for (int j = 0; j < point.Count(); j++)
                {
                    string str2 = string.Format("{0,6:f4} {1,14:f4} {2,14:f4} {3,14:f4}\n", point[j].name, point[j].X.ToString("f4"), point[j].Y.ToString("f4"), point[j].Z.ToString("f4"));
                    richtext += str2;
                }
                this.richTextBox2.Text = richtext;
            }
          

        }

        private void 退出ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void xYZBLHToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PointInfo p = new PointInfo();
            p.XYZ2BLH(ref point);
            p.XYZ2NEU(ref point);
            //将计算出的结果显示到屏幕上
            string richtext = "";
            string str1 = "  6\n";
            richtext += str1;
            for(int j=0;j<point.Count();j++)
            {
                string str2 = string.Format("{0,6:f4} {1,14:f4} {2,14:f4} {3,14:f4}\n", point[j].name, point[j].N.ToString("f4"), point[j].E.ToString("f4"), point[j].U.ToString("f4"));
                richtext += str2;
            }
            this.richTextBox1.Text = richtext;
            report = richtext;
            tabControl1.TabIndex = 1;
            MessageBox.Show("计算完成！");
        }

        private void 保存报告ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog dialog2 = new SaveFileDialog();
            dialog2.Filter = "*.dat|*.dat";
            DialogResult result = dialog2.ShowDialog();
            if(result==DialogResult.OK)
            {
                StreamWriter wt = new StreamWriter(dialog2.FileName);
                string rep = richTextBox1.Text.Replace("\n","\r\n");
                wt.WriteLine(rep);

                MessageBox.Show("保存成功！");
                wt.Close();
            }
        }

        private void 退出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
