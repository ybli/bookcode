using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace MapIcon
{

    //点结构
    public struct point
    {
        public string PointName;
        public string Code;
        public double x;
        public double y;
        public double z;
    }

    class Draw_Picture
    {
        public List<point> P = new List<point>();    //散点
        List<point> P1 = new List<point>();
        public int Width;    //图幅宽度
        public int Height;  //图幅高度
        public Bitmap Bmp = new Bitmap(5000, 5000);   //目标图像
        public double SFRadoi;    //缩放系数

        //根据距离将道路点排序
        public void Rank(List<point> RankP)
        {
            List<point> lp = new List<point>();

            //找起始点
            for (int i = 0; i < RankP.Count; i++)
            {
                string a = RankP[i].Code;

                if (a.Substring(0, 1) == "0")
                {
                    lp.Add(RankP[i]);
                    RankP.Remove(RankP[i]);
                    break;
                }
            }

            List<double> Dist = new List<double>();

            //计算其余点到起点的距离
            for (int i = 0; i < RankP.Count; i++)
            {
                Dist.Add(Math.Sqrt((RankP[i].x - lp[0].x) * (RankP[i].x - lp[0].x) + (RankP[i].y - lp[0].y) * (RankP[i].y - lp[0].y)));
            }

            int sy = 0;
            int aa = Dist.Count;

            //根据距离排序
            for (int i = 0; i < aa; i++)
            {

                sy = Dist.IndexOf(Dist.Min());

                lp.Add(RankP[sy]);

                Dist.Remove(Dist[sy]);
                RankP.Remove(RankP[sy]);
            }

            RankP.Clear();

            RankP.AddRange(lp);
        }

        //计算绘图参数
        public void Ratio()
        {
            CalParameter_Picture cal = new CalParameter_Picture();
            
            List<double> x = new List<double>();
            List<double> y = new List<double>();

            for (int i = 0; i < P.Count; i++)
            {
                x.Add(P[i].x);
                y.Add(P[i].y);
            }

            cal.CalRatio(x, y, Width - 200, Height - 200);
            cal.Change(x, y, Width / 2, Height / 2);

            SFRadoi = cal.t;

            for (int i = 0; i < P.Count; i++)
            {
                point p = new point();

                p.PointName = P[i].PointName;
                p.Code = P[i].Code;
                p.x = cal.px[i];
                p.y = cal.py[i];
                p.z = P[i].z;

                P1.Add(p);
            }
        }

        //绘图
        public void Draw()
        {

            MainInterface.f1.pb.Size = new Size(Width, Height);

            Configure_Picture Con = new Configure_Picture(Width);
            Symbol_Picture Dsym = new Symbol_Picture();

            Dsym.gp = Graphics.FromImage(Bmp);

            Dsym.gp.Clear(Color.Black);

            List<double> x = new List<double>();
            List<double> y = new List<double>();
            List<point> DeltP = new List<point>();

            //绘制散点图
            Con.sF();
            for (int i = 0; i < P.Count; i++)
            {
                Dsym.Text(Con.StringBrush, P1[i].x, P1[i].y, Con.StringFont, P1[i].PointName);
            }
            
            int js = 0;
            while (P1.Count >= 1)
            {
                //画多点房屋
                if (P1[js].Code.Contains("DF"))
                {
                    x.Add(P1[js].x);
                    y.Add(P1[js].y);
                    DeltP.Add(P1[js]);

                    //寻找其余点
                    for (int i = 1; i < P1.Count; i++)
                    {
                        if (P1[i].Code == P1[js].Code)
                        {
                            x.Add(P1[i].x);
                            y.Add(P1[i].y);

                            DeltP.Add(P1[i]);
                        }
                    }

                    //删除已画点 
                    for (int i = 0; i < DeltP.Count; i++)
                    {
                        P1.Remove(DeltP[i]);
                    }

                    //绘制
                    Dsym.D_FW(Con.DfPen, x, y);

                    x.Clear();
                    y.Clear();
                    DeltP.Clear();
                }
                //画砼房屋
                else if (P1[js].Code.Contains("TF"))
                {
                    x.Add(P1[js].x);
                    y.Add(P1[js].y);
                    DeltP.Add(P1[js]);

                    //寻找其余点
                    for (int i = 1; i < P1.Count; i++)
                    {
                        if (P1[i].Code == P1[js].Code)
                        {
                            x.Add(P1[i].x);
                            y.Add(P1[i].y);

                            DeltP.Add(P1[i]);
                        }
                    }

                    //删除已画点 
                    for (int i = 0; i < DeltP.Count; i++)
                    {
                        P1.Remove(DeltP[i]);
                    }

                    //绘制
                    Dsym.T_FW(Con.TfPen, x, y, DeltP[0].Code, Con.TfBrush);

                    x.Clear();
                    y.Clear();
                    DeltP.Clear();
                }
                //画小路
                else if (P1[js].Code.Contains("XL"))
                {
                    DeltP.Add(P1[js]);

                    string xl;
                    string nxl;
                    xl = P1[js].Code;

                    if (xl.Substring(0, 1) == "0")
                    {
                        nxl = xl.Substring(1, xl.Length - 1);
                    }
                    else
                    {
                        nxl = xl;
                    }

                    //寻找其余点
                    for (int i = 1; i < P1.Count; i++)
                    {
                        if (P1[i].Code.Contains(nxl))
                        {
                            DeltP.Add(P1[i]);
                        }
                    }

                    //删除已画点 
                    for (int i = 0; i < DeltP.Count; i++)
                    {
                        P1.Remove(DeltP[i]);
                    }

                    //排序
                    Rank(DeltP);

                    for(int i=0;i<DeltP.Count;i++)
                    {
                        x.Add(DeltP[i].x);
                        y.Add(DeltP[i].y);
                    }

                    //绘制
                    Dsym.XL(Con.XlPen, x, y);

                    x.Clear();
                    y.Clear();
                    DeltP.Clear();
                }
                //路灯
                else if (P1[js].Code.Contains("LD"))
                {
                    x.Add(P1[js].x);
                    y.Add(P1[js].y);
                    DeltP.Add(P1[js]);

                    //删除已画点 
                    for (int i = 0; i < DeltP.Count; i++)
                    {
                        P1.Remove(DeltP[i]);
                    }

                    //绘制
                    Con.LDSize();
                    Dsym.LD(Con.LDPen, x[0], y[0], Con.LdSize);

                    x.Clear();
                    y.Clear();
                    DeltP.Clear();
                }
                //公路
                else if (P1[js].Code.Contains("GL"))
                {
                    DeltP.Add(P1[js]);

                    string xl;
                    string nxl;
                    xl = P1[js].Code;

                    if (xl.Substring(0, 1) == "0")
                    {
                        nxl = xl.Substring(1, xl.Length - 1);
                        string[] gl = new string[2];

                        gl = nxl.Split('-');

                        nxl = gl[0];
                    }
                    else
                    {
                        nxl = xl;
                    }

                    //寻找其余点
                    for (int i = 1; i < P1.Count; i++)
                    {
                        if (P1[i].Code.Contains(nxl))
                        {
                            DeltP.Add(P1[i]);
                        }
                    }

                    //删除已画点 
                    for (int i = 0; i < DeltP.Count; i++)
                    {
                        P1.Remove(DeltP[i]);
                    }

                    //排序
                    Rank(DeltP);

                    for (int i = 0; i < DeltP.Count; i++)
                    {
                        x.Add(DeltP[i].x);
                        y.Add(DeltP[i].y);
                    }

                    //绘制
                    Dsym.GL(Con.RoadPen, x, y, DeltP[0].Code, SFRadoi);

                    x.Clear();
                    y.Clear();
                    DeltP.Clear();
                }
            }
        }
    }
}