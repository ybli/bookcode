using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Windows.Forms.DataVisualization.Charting;

namespace PlaneMP
{
    public partial class MainInterface : Form
    {
        public MainInterface()
        {
            InitializeComponent();
        }

        //散点数量
        int sum = 0;

        //判断计算是否完成
        bool ht = false;

        //判断是否完成绘图
        bool draw = false;

        //绘图初始区域
        int w = 500, h = 500;

        //鼠标按下是的位置
        int Mpx = 0, Mpy = 0;

        //图形位置
        int x = 0, y = 0;

        //图形移动辅助值
        int x1, y1;

        //判断图形是否移动结束
        bool isMove = false;

        //窗体初始化
        private void MainInterface_Load(object sender, EventArgs e)
        {
            dvg.Rows.Add(300);

            tabPage2.MouseWheel += new MouseEventHandler(tabPage2_MouseWheel);

            chart1.Location = new Point(0, 0);
            chart1.Size = new Size(w, h);
        }

        //打开文件——菜单栏
        private void ToolStripMenuItem_M_openTXT_Click(object sender, EventArgs e)
        {
            try
            {
                opTxt.Filter = "文本文件|*.txt";

                if (opTxt.ShowDialog() == DialogResult.OK)
                {
                    StreamReader sr = new StreamReader(opTxt.FileName);

                    //读取坐标数量
                    sum = Convert.ToInt32(sr.ReadLine());

                    //读取坐标值
                    string A;
                    string[] a = new string[4];
                    for (int i = 0; i < sum; i++)
                    {
                        A = sr.ReadLine();

                        a = A.Split(',');

                        dvg[0, i].Value = a[0];
                        dvg[1, i].Value = a[1];
                        dvg[2, i].Value = a[2];
                        dvg[3, i].Value = a[3];
                    }
                }
            }
            catch
            {
                MessageBox.Show("数据格式错误", "提示", MessageBoxButtons.OK);
            }
        }


        Class_Plane C;

        //解算——菜单栏
        private void ToolStripMenuItem_M_Cal_Click(object sender, EventArgs e)
        {

            C = new Class_Plane();

            try
            {
                C.Point_SUM = sum;

                for (int i = 0; i < sum; i++)
                {
                    point p = new point();

                    p.PointName = Convert.ToString(dvg[0, i].Value);
                    p.x = Convert.ToDouble(dvg[1, i].Value);
                    p.y = Convert.ToDouble(dvg[2, i].Value);
                    p.z = Convert.ToDouble(dvg[3, i].Value);

                    C.M.Add(p);
                }

                C.PlaneThreePoint();
                C.Calculate();
                richTextBox1.Text = C.Report();

                ht = true;
            }
            catch
            {
                MessageBox.Show("请检查源数据", "提示", MessageBoxButtons.OK);
            }
        }


        //保存DXF
        private void ToolStripMenuItem_SaDXF_Click(object sender, EventArgs e)
        {
            saDXF.Filter = "DXF文件|*.dxf";
            if (saDXF.ShowDialog() == DialogResult.OK)
            {
                C.Draw_DXF(saDXF.FileName);
            }
        }

        //显示图形
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPage2;
            x=0;
            y=0;
            x1 = 0;
            y1 = 0;

            chart1.Location = new Point(0, 0);
            chart1.Size = new Size(500, 500);
            chart1.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
            chart1.ChartAreas[0].AxisY.MajorGrid.Enabled = false;
            if (ht)
            {
                for (int i = 0; i < sum; i++)
                {
                    DataPoint p = new DataPoint();
                    p.Label = C.M[i].PointName;

                    p.SetValueXY(C.M[i].x, C.M[i].y);

                    chart1.Series[0].Points.Add(p);
                }

                ht = false;
                draw = true;
            }
        }

        //TabPage2获得焦点
        private void tabPage2_Click(object sender, EventArgs e)
        {
            tabPage2.Focus();
        }

        //图形缩放
        private void tabPage2_MouseWheel(object sender, MouseEventArgs e)
        {
            if (draw)
            {
                //缩小
                if (e.Delta < 0)
                {
                    if (w > 100 && h > 100)
                    {
                        w -= 100;
                        h -= 100;
                    }
                }

                else if (e.Delta > 0)
                {
                    w += 100;
                    h += 100;
                }
            }

            chart1.Size = new Size(w, h);
        }

        //记录鼠标按下的位置
        private void chart1_MouseDown(object sender, MouseEventArgs e)
        {
            Mpx = System.Windows.Forms.Cursor.Position.X;
            Mpy = System.Windows.Forms.Cursor.Position.Y;

            isMove = true;
        }

        //图形移动
        private void chart1_MouseMove(object sender, MouseEventArgs e)
        {
            if (draw && isMove)
            {
                int MoveX, MoveY;

                MoveX = System.Windows.Forms.Cursor.Position.X - Mpx;
                MoveY = System.Windows.Forms.Cursor.Position.Y - Mpy;

                x = x1 + MoveX;
                y = y1 + MoveY;

                chart1.Location = new Point(x, y);
            }
        }

        //结束移动
        private void chart1_MouseUp(object sender, MouseEventArgs e)
        {
            isMove = false;

            x1 = x;
            y1 = y;
        }

        //打开文件——工具栏
        private void toolStripButton_TopenTXT_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem_M_openTXT.PerformClick();
        }

        //解算——工具栏
        private void toolStripButton_T_Cal_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem_M_Cal.PerformClick();
        }

        //保存计算报告
        private void toolStripButton_SaReport_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPage3;

            saTXT.Filter = "文本文件|*.TXT";
            if (saTXT.ShowDialog() == DialogResult.OK)
            {
                richTextBox1.SaveFile(saTXT.FileName, RichTextBoxStreamType.PlainText);
            }
        }

        //保存界面图形
        private void ToolStripMenuItem_SaBMP_Click(object sender, EventArgs e)
        {
            saBMP.Filter = "图行文件|*.bmp";
            if (saBMP.ShowDialog() == DialogResult.OK)
            {
                chart1.SaveImage(saBMP.FileName, ChartImageFormat.Bmp);
            }
        }


        //清除
        private void toolStripButton_Clear_Click(object sender, EventArgs e)
        {
            dvg.Rows.Clear();
            dvg.Rows.Add(300);

            chart1.Series[0].Points.Clear();

            richTextBox1.Clear();

            ht = false;
            draw = false;

            x = 0;
            y = 0;
            x1 = 0;
            y1 = 0;
        }
    }
}
