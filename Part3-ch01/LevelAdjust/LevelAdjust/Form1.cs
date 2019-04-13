using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LevelLib;
using System.IO;
namespace LevelDll
{
    public partial class Form1 : Form
    {
        DataCenter dataCenter;
        public Form1()
        {
            InitializeComponent();
        }

        #region 文件
        private void toolOpenData_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "(txt文件)|*txt";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                dataCenter = FileCenter.OpenFile(openFileDialog1.FileName);
                dataGridView1.DataSource = Report.GetTable(dataCenter);
            }
        }
        private void MenuFileOpen_Click(object sender, EventArgs e)
        {
            toolOpenData_Click(sender, e);
        }


        private void toolSaveReport_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Filter = "(TXT文件)|*.txt";
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                StreamWriter writer = new StreamWriter(saveFileDialog1.FileName);
                writer.Write(Report.GetReport(dataCenter));
                writer.Close();
            }
        }

        private void MenuFileSaveReport_Click(object sender, EventArgs e)
        {
            toolSaveReport_Click(sender, e);
        }

        private void MenuFileSaveChart_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Filter = "(Jpeg文件)|*.jpg";
            saveFileDialog1.RestoreDirectory = true;
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                DrawChart.SaveChart(chart1, saveFileDialog1.FileName);
            }
        }
        private void MenuFileSaveXls_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Filter = "(xls文件)|*.xls";
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                //FileHandler.SaveTable(saveFileDialog1.FileName, Obs.ToTable());
                // MessageBox.Show("保存表格成功", "信息提示");
            }
        }
        private void MenuFileSaveDXF_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Filter = "(DXF文件)|*.dxf";
            saveFileDialog1.RestoreDirectory = true;
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                Report.SaveDXF(saveFileDialog1.FileName, dataCenter);
            }
        }

        private void MenuExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void MenuReport_Click(object sender, EventArgs e)
        {
            toolReport_Click(sender, e);
        }

        private void toolReport_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 2;
        }
        #endregion

        private void MenuPreprocess_Click(object sender, EventArgs e)
        {
            Algorithm.PreProcess(ref dataCenter);
            dataGridView1.DataSource = Report.GetAllTable(dataCenter);

        }

        private void MenuFile_Click(object sender, EventArgs e)
        {

        }

        private void MenuAdjustment_Click(object sender, EventArgs e)
        {
            Algorithm.ApproximateProcess(ref dataCenter);
        }

        private void MenuPrecession_Click(object sender, EventArgs e)
        {
            Algorithm.FinalProcess(ref dataCenter);
        }

        private void MenuDoALL_Click(object sender, EventArgs e)
        {
            MenuPreprocess_Click(sender, e);
            MenuAdjustment_Click(sender, e);
            MenuPrecession_Click(sender, e);
            richTextBox1.Text =Report.GetReport(dataCenter);
            DrawChart.Draw(dataCenter, ref chart1);
        }


    }
}
