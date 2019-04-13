using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Drawing.Drawing2D;

namespace MapIcon
{
    public partial class MainInterface : Form
    {
        public static MainInterface f1;

        public MainInterface()
        {
            InitializeComponent();

            f1 = this;
        }

        //散点数量
        int sum = 0;

        //判断计算是否完成
        bool Calculate = false;

        //窗体初始位置
        int Px = 0, Py = 0;

        //窗体移动辅助参数
        int Px1 = 0, Py1 = 0;

        //判断窗体是否可以移动
        bool IsMove = false;

        //记录鼠标位置
        double Mouse_X, Mouse_Y;

        //窗体初始化
        private void MainInterface_Load(object sender, EventArgs e)
        {
            pb.MouseWheel += new MouseEventHandler(pb_MouseWheel);

            pb.Location = new Point(Px, Py);
        }

        //打开文件——菜单栏
        private void ToolStripMenuItem_MopTXT_Click(object sender, EventArgs e)
        {            
            try
            {
                opTXT.Filter = "文本文件|*.txt";
                if (opTXT.ShowDialog() == DialogResult.OK)
                {
                    StreamReader sr = new StreamReader(opTXT.FileName);

                    //获取点数
                    string A = sr.ReadLine();
                    while (A != null)
                    {
                        sum += 1;

                        A = sr.ReadLine();
                    }
                    sr.Close();
                    sr.Dispose();

                    dvg.Rows.Add(sum + 10);

                    StreamReader sr1 = new StreamReader(opTXT.FileName);

                    //数据读取
                    for (int i = 0; i < sum; i++)
                    {
                        string[] a = new string[5];

                        a = sr1.ReadLine().Split(',');

                        dvg[0, i].Value = a[0];
                        dvg[1, i].Value = a[1];
                        dvg[2, i].Value = a[2];
                        dvg[3, i].Value = a[3];
                        dvg[4, i].Value = a[4];
                    }

                    sr1.Close();
                    sr1.Dispose();
                }
            }
            catch
            {
                MessageBox.Show("请检查源数据", "提示", MessageBoxButtons.OK);
            }
        }

        //打开文件——工具栏
        private void toolStripButton_TopTXT_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem_MopTXT.PerformClick();
        }

        Draw_Picture dr = new Draw_Picture();

        //成图——菜单栏
        private void ToolStripMenuItem_MDraw_Click(object sender, EventArgs e)
        {
            pb.Location = new Point(0, 0);
            dr.Width = 500;
            dr.Height = 500;

            dr.P.Clear();

            //获取原始数据点
            for (int i = 0; i < sum; i++)
            {
                point p = new point();
                p.PointName = Convert.ToString(dvg[0, i].Value);
                p.Code = Convert.ToString(dvg[1, i].Value);
                p.x = Convert.ToDouble(dvg[2, i].Value);
                p.y = Convert.ToDouble(dvg[3, i].Value);
                p.z = Convert.ToDouble(dvg[4, i].Value);

                dr.P.Add(p);
            }

            dr.Ratio();
            dr.Draw();

            pb.Image = dr.Bmp;

            Calculate = true;

            Px = 0;
            Py = 0;
            Px1 = 0; 
            Py1 = 0;
        }

        //缩放
        private void pb_MouseWheel(object sender, MouseEventArgs e)
        {
            if (Calculate)
            {
                //放大
                if (e.Delta > 0)
                {
                    if (dr.Width <= 4000)
                    {
                        dr.Width += 100;
                        dr.Height += 100;

                        dr.Ratio();
                        dr.Draw();

                        pb.Image = dr.Bmp;
                    }
                }
                //缩小
                else if (e.Delta < 0)
                {
                    if (dr.Width >= 301)
                    {
                        dr.Width -= 100;
                        dr.Height -= 100;

                        dr.Ratio();
                        dr.Draw();

                        pb.Image = dr.Bmp;
                    }
                }
            }
        }

        //绘图框获取焦点
        private void pb_Click(object sender, EventArgs e)
        {
            pb.Focus();
        }

        //鼠标左键DOWN开始移动
        private void pb_MouseDown(object sender, MouseEventArgs e)
        {
            Mouse_X = Cursor.Position.X;
            Mouse_Y = Cursor.Position.Y;

            IsMove = true;
        }

        //绘图框的移动
        private void pb_MouseMove(object sender, MouseEventArgs e)
        {
            if (IsMove)
            {
                //X、Y方向上的移动距离
                double Move_X, Move_Y;
                Move_X = Cursor.Position.X - Mouse_X;
                Move_Y = Cursor.Position.Y - Mouse_Y;

                //绘图框移动后位置
                Px = Px1 + (int)Move_X;
                Py = Py1 + (int)Move_Y;

                pb.Location = new Point(Px, Py);

                pb.Image = dr.Bmp;
            }
        }

        //结束移动
        private void pb_MouseUp(object sender, MouseEventArgs e)
        {
            IsMove = false;

            Px1 = Px;
            Py1 = Py;
        }

        //保存BMP
        private void bMPToolStripMenuItem_MOUT_Click(object sender, EventArgs e)
        {
            saveBMP.Filter = "图形|*.bmp";
            if (saveBMP.ShowDialog() == DialogResult.OK)
            {
                Bitmap bb = new Bitmap(pb.Width, pb.Height);

                Graphics g = Graphics.FromImage(bb);

                Rectangle re= new Rectangle(0, 0, pb.Width, pb.Height);
                Rectangle re1 = new Rectangle(0, 0, pb.Width, pb.Height);

                g.DrawImage(pb.Image, re, re1, GraphicsUnit.Pixel);

                bb.Save(saveBMP.FileName);
            }
        }

        //清除
        private void toolStripButton_Clear_Click(object sender, EventArgs e)
        {
            pb.Image = null;

            sum = 0;

            Calculate = false;

            Px = 0;
            Py = 0;

            Px1 = 0;
            Py1 = 0;

            dvg.Rows.Clear();
        }

        //保存DXF文件
        private void toolStripButton_DxfOut_Click(object sender, EventArgs e)
        {
            saDXF.Filter = "图形|*.DXF";
            if (saDXF.ShowDialog() == DialogResult.OK)
            {
                Draw_DXF dxf = new Draw_DXF();

                dxf.P1.AddRange(dr.P);
                dxf.FileName = saDXF.FileName;

                dxf.CalRatio();
                dxf.Draw();
            }
        }

        private void dXFToolStripMenuItem_MOUT_Click(object sender, EventArgs e)
        {
            toolStripButton_DxfOut.PerformClick();
        }

        private void toolStripButton_TDraw_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem_MDraw.PerformClick();
        }

    }
}
