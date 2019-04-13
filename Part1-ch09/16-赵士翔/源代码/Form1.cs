using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace XYZ2NEU
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        Formula formula = new Formula();

        List<XYZPoint> Xpoint;
        List<BLHPoint> Bpoint;
        List<NEUPoint> Npoint;

        string report;

        private BindingList<XYZPoint> Bdata1;
        private BindingList<BLHPoint> Bdata2;
        private BindingList<NEUPoint> Bdata3;

        private void 打开OToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Xpoint = new List<XYZPoint>();

            formula.Readfile(out Xpoint);

            Bdata1 = new BindingList<XYZPoint>(Xpoint);
            this.dataGridView1.DataSource = Bdata1;

            MessageBox.Show("数据读取完成！");
            this.tabControl1.SelectedIndex = 0;
        }

        private void xYZToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Bpoint = new List<BLHPoint>();

            formula.xyz2blh(Xpoint, out Bpoint);

            Bdata2 = new BindingList<BLHPoint>(Bpoint);
            this.dataGridView2.DataSource = Bdata2;

            MessageBox.Show("空间坐标转大地坐标转换完成！");
            this.tabControl1.SelectedIndex = 1;
        }

        private void xYZBLHToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int num = int.Parse(toolStripTextBox1.Text);

            formula.blh2neu(Xpoint, Bpoint, num, out Npoint);

            Bdata3 = new BindingList<NEUPoint>(Npoint);
            this.dataGridView3.DataSource = Bdata3;

            MessageBox.Show("转换为站心坐标完成！");
            this.tabControl1.SelectedIndex = 2;

            report = null;
            formula.Report(Xpoint, Bpoint, num, Npoint, out report);
            richTextBox1.Text = report;
        }

        private void 空间坐标数据ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.tabControl1.SelectedIndex = 0;
        }

        private void 大地坐标数据ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.tabControl1.SelectedIndex = 1;
        }

        private void 站心坐标数据ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.tabControl1.SelectedIndex = 2;
        }

        private void 保存SToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog file = new SaveFileDialog();
            file.InitialDirectory = Application.StartupPath;
            file.RestoreDirectory = true;
            file.Filter = "Dat Files(*.dat)|*.dat";

            if (file.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                var writer = new StreamWriter(file.FileName);

                var arr = report.Split('\n');

                for (int i = 0; i < arr.Count(); i++)
                {
                    writer.WriteLine(arr[i]);
                }
                writer.Close();
            }
        }

        private void 退出EToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }



    }
}
