using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;


namespace Contour_Line
{
    public partial class MainInterface : Form
    {
        public static MainInterface f1;

        public MainInterface()
        {
            InitializeComponent();

            f1 = this;
        }

        //图形
        Bitmap TIN1_Bmp = new Bitmap(500, 500);
        Bitmap Contour1_Bmp = new Bitmap(600, 600);

        //坐标数量
        int sum;

        //记录鼠标位置
        double Mouse_X, Mouse_Y;
        double Mouse_X1, Mouse_Y1;

        //判断是否进行图形的移动
        bool IsMove = false;
        bool IsMove1 = false;

        //pictubox的初始位置
        int P_X = 0, P_Y = 0;
        int P1_X = 0, P1_Y = 0;

        //图形移动辅助参数
        int P_X1 = 0, P_Y1 = 0;
        int P1_X1 = 0, P1_Y1 = 0;

        //判断计算是否完成
        bool Calculate = false;

        //窗体初始化
        private void MainInterface_Load(object sender, EventArgs e)
        {
            dvg.Rows.Add(1000);

            tabPage2.MouseWheel += new MouseEventHandler(tabPage2_MouseWheel);
            tabPage3.MouseWheel += new MouseEventHandler(tabPage3_MouseWheel);

            pb.Location = new Point(P_X, P_Y);
            pb1.Location = new Point(P1_X, P1_Y);
        }


        //打开文件——菜单栏
        private void ToolStripMenuItem_OpTxT_Click(object sender, EventArgs e)
        {
            try
            {
                opTxt.Filter = "文本文件|*.txt";
                if (opTxt.ShowDialog() == DialogResult.OK)
                {
                    if (opTxt.FileName != null)
                    {
                        string[] a = new string[4];
                        StreamReader sr = new StreamReader(opTxt.FileName);

                        string A = sr.ReadLine();
                        while (A != null)
                        {
                            a = A.Split(',');
                            dvg[0, sum].Value = a[0];
                            dvg[1, sum].Value = a[1];
                            dvg[2, sum].Value = a[2];
                            dvg[3, sum].Value = a[3];

                            sum += 1;
                            A = sr.ReadLine();
                        }
                        sr.Close();
                    }

                    MessageBox.Show("导入成功", "提示", MessageBoxButtons.OK);
                }
            }
            //源数据容错性检查
            catch
            {
                MessageBox.Show("数据读取失败", "提示", MessageBoxButtons.OK);
                dvg.Rows.Clear();
                dvg.Rows.Add(300);
            }
        }

        //打开文件——工具栏
        private void toolStripButton_OpTxt_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem_OpTxT.PerformClick();
        }


        Class_TIN_and_ContourLine tin;

        //解算——菜单栏
        private void ToolStripMenuItem_MCal_Click(object sender, EventArgs e)
        {
            try
            {
                tin = new Class_TIN_and_ContourLine();

                pb.Location = new Point(0, 0);
                pb1.Location = new Point(0, 0);

                //获取数据
                for (int i = 0; i < sum; i++)
                {
                    point m = new point();
                    m.PointName = Convert.ToString(dvg[0, i].Value);
                    m.x = Convert.ToDouble(dvg[1, i].Value);
                    m.y = Convert.ToDouble(dvg[2, i].Value);
                    m.z = Convert.ToDouble(dvg[3, i].Value);

                    tin.P.Add(m);
                }


                tin.FindBase();
                tin.Rank();
                tin.BuildS();
                tin.SortX();
                tin.BuildInitialTin();
                tin.BuildPlaneTIN();
                tin.DeleteT();
                tin.Draw_TIN();

                tin.Purse_All(1.0);
                tin.Draw_Contour();

                pb.Image = tin.TIN_Bmp;

                pb1.Image = tin.Contour_Bmp;


                TIN1_Bmp = (Bitmap)pb.Image;
                Contour1_Bmp = tin.Contour_Bmp;

                richTextBox1.Text = tin.Report();

                Calculate = true;

                P_X = 0;
                P_Y = 0;
                P1_X = 0;
                P1_Y = 0;

                P_X1 = 0;
                P_Y1 = 0;
                P1_X1 = 0;
                P1_Y1 = 0;
            }
            catch
            {
                MessageBox.Show("无计算数据数据", "提示", MessageBoxButtons.OK);
            }
        }


        //三角网——图形缩放
        private void tabPage2_MouseWheel(object sender, MouseEventArgs e)
        {
            if (Calculate)
            {
                //放大
                if (e.Delta > 0)
                {
                    if (tin.Width1 <= 4000 & tin.Height1 <= 4000)
                    {
                        tin.Width1 += 100;
                        tin.Height1 += 100;

                        tin.Draw_TIN();

                        pb.Image = tin.TIN_Bmp;
                    }
                }
                else if (e.Delta < 0)
                {
                    if (tin.Width1 >= 150 & tin.Height1 >= 150)
                    {
                        tin.Width1 -= 100;
                        tin.Height1 -= 100;

                        tin.Draw_TIN();

                        pb.Image = tin.TIN_Bmp;
                    }
                }
            }
        }

        //tabPage2获得焦点
        private void pb_Click(object sender, EventArgs e)
        {
            tabPage2.Focus();
        }
        private void tabPage2_Click(object sender, EventArgs e)
        {
            tabPage2.Focus();
        }


        //鼠标左键DOWN开始移动
        private void pb_MouseDown(object sender, MouseEventArgs e)
        {
            Mouse_X1 = Cursor.Position.X;
            Mouse_Y1 = Cursor.Position.Y;

            IsMove1 = true;
        }

        //三角网移动
        private void pb_MouseMove(object sender, MouseEventArgs e)
        {
            if (IsMove1)
            {
                //X、Y方向上的移动距离
                double Move_X, Move_Y;
                Move_X = Cursor.Position.X - Mouse_X1;
                Move_Y = Cursor.Position.Y - Mouse_Y1;

                //图形移动后的位置
                P1_X  = P1_X1 + (int)Move_X;
                P1_Y = P1_Y1 + (int)Move_Y;

                pb.Location = new Point(P1_X, P1_Y);
            }
        }

        //鼠标左键UP结束移动
        private void pb_MouseUp(object sender, MouseEventArgs e)
        {
            IsMove1 = false;

            P1_X1 = P1_X;
            P1_Y1 = P1_Y;
        }

        //等高线——图形缩放
        private void tabPage3_MouseWheel(object sender, MouseEventArgs e)
        {
            if (Calculate)
            {
                //放大
                if (e.Delta > 0)
                {
                    if (tin.Width <= 4000 & tin.Height <= 4000)
                    {
                        tin.Width += 100;
                        tin.Height += 100;

                        tin.Draw_Contour();

                        pb1.Image = tin.Contour_Bmp;
                    }
                }
                if (e.Delta < 0)
                {
                    if (tin.Width >=300  & tin.Height >= 300)
                    {
                        tin.Width -= 100;
                        tin.Height -= 100;

                        tin.Draw_Contour();

                        pb1.Image = tin.Contour_Bmp;
                    }
                }
            }
        }

        //tabPage3获得焦点
        private void pb1_Click(object sender, EventArgs e)
        {
            tabPage3.Focus();
        }
        private void tabPage3_Click(object sender, EventArgs e)
        {
            tabPage3.Focus();
        }

        //鼠标左键DOWN开始移动
        private void pb1_MouseDown(object sender, MouseEventArgs e)
        {
            Mouse_X = Cursor.Position.X;
            Mouse_Y = Cursor.Position.Y;

            IsMove = true;
        }

        //等高线移动
        private void pb1_MouseMove(object sender, MouseEventArgs e)
        {
            if (IsMove)
            {
                //X、Y方向上的移动距离
                double Move_X, Move_Y;
                Move_X = Cursor.Position.X - Mouse_X;
                Move_Y = Cursor.Position.Y - Mouse_Y;

                //图形移动后的位置
                P_X = P_X1 + (int)Move_X;
                P_Y = P_Y1 + (int)Move_Y;

                pb1.Location = new Point(P_X, P_Y);
            }
        }

        //鼠标左键UP结束移动
        private void pb1_MouseUp(object sender, MouseEventArgs e)
        {
            IsMove = false;

            P_X1 = P_X;
            P_Y1 = P_Y;
        }


        //清除
        private void toolStripButton_Clear_Click(object sender, EventArgs e)
        {
            dvg.Rows.Clear();
            dvg.Rows.Add(1000);

            sum = 0;

            IsMove = false;
            IsMove1 = false;

            P_X = 0;
            P_Y = 0;
            P1_X = 0;
            P1_Y = 0;
            
            P_X1 = 0;
            P_Y1 = 0;
            P1_X1 = 0;
            P1_Y1 = 0;

            Calculate = false;

            pb.Image = null;
            pb1.Image = null;

            richTextBox1.Clear();
        }


        //保存DXF文件           
        private void ToolStripMenuItem_SaDxF_Click(object sender, EventArgs e)
        {
            saDXF.Filter = "图形文件|*.DXF";
            if (saDXF.ShowDialog() == DialogResult.OK)
            {
                tin.Draw_DXF(saDXF.FileName);
            }
        }

        //解算——工具栏
        private void toolStripButton_Cal_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem_MCal.PerformClick();
        }

        //保存示意图
        private void ToolStripMenuItem_Bmp_Click(object sender, EventArgs e)
        {
            saBMP.Filter = "图形文件|*.Bmp";
            if (saBMP.ShowDialog() == DialogResult.OK)
            {
                tin.Contour_Bmp.Save(saBMP.FileName);
            }
        }
        

        //保存计算报告
        private void toolStripButton_SaTxt_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPage4;

            saReport.Filter = "文本文件|*.txt";
            if (saReport.ShowDialog() == DialogResult.OK)
            {
                richTextBox1.SaveFile(saReport.FileName, RichTextBoxStreamType.PlainText);
            }
        }
    }
}
