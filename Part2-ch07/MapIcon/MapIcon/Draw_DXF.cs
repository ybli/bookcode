using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace MapIcon
{
    //绘制DXF文件
    class Draw_DXF
    {
        public List<point> P1 = new List<point>();    //散点
        public string FileName;    //目标文件名

        CalParameter_DXFcs dxf = new CalParameter_DXFcs();
        Symbol_DXF DSym = new Symbol_DXF();

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
        public void CalRatio()
        {
            List<double> x = new List<double>();
            List<double> y = new List<double>();

            for (int i = 0; i < P1.Count; i++)
            {
                x.Add(P1[i].x);
                y.Add(P1[i].y);
            }

            dxf.CalRatio(x, y);
        }

        public void Draw()
        {
            Configure_DXF Con = new Configure_DXF(dxf.d);
            DSym.sw = new StreamWriter(FileName);

            //限定绘图区域
            DSym.sw.WriteLine(0);
            DSym.sw.WriteLine("SECTION");
            DSym.sw.WriteLine(2);
            DSym.sw.WriteLine("HEADER");
            DSym.sw.WriteLine(9);
            DSym.sw.WriteLine("$LIMMIN");
            DSym.sw.WriteLine(10);
            DSym.sw.WriteLine(dxf.minx);
            DSym.sw.WriteLine(20);
            DSym.sw.WriteLine(dxf.miny);
            DSym.sw.WriteLine(9);
            DSym.sw.WriteLine("$LIMMAX");
            DSym.sw.WriteLine(10);
            DSym.sw.WriteLine(dxf.maxx);
            DSym.sw.WriteLine(20);
            DSym.sw.WriteLine(dxf.maxy);
            DSym.sw.WriteLine(0);
            DSym.sw.WriteLine("ENDSEC");
            DSym.sw.WriteLine(0);
            DSym.sw.WriteLine("SECTION");
            DSym.sw.WriteLine(2);
            DSym.sw.WriteLine("ENTITIES");

            List<double> x = new List<double>();
            List<double> y = new List<double>();
            List<double> z = new List<double>();
            List<point> DeltP = new List<point>();

            //画点名
            Con.Text();
            for (int i = 0; i < P1.Count; i++)
            {
                DSym.sw.WriteLine(0);
                DSym.sw.WriteLine("TEXT");
                DSym.sw.WriteLine(8);
                DSym.sw.WriteLine("0");
                DSym.sw.WriteLine(62);
                DSym.sw.WriteLine(Con.TextColor);
                DSym.sw.WriteLine(10);
                DSym.sw.WriteLine(P1[i].x);
                DSym.sw.WriteLine(20);
                DSym.sw.WriteLine(P1[i].y);
                DSym.sw.WriteLine(40);
                DSym.sw.WriteLine(Con.TextSize);
                DSym.sw.WriteLine(1);
                DSym.sw.WriteLine(P1[i].PointName);
            }

            //绘图
            int js = 0;
            while (P1.Count >= 1)
            {
                //画多点房屋
                if (P1[js].Code.Contains("DF"))
                {
                    x.Add(P1[js].x);
                    y.Add(P1[js].y);
                    z.Add(P1[js].z);
                    DeltP.Add(P1[js]);

                    //寻找其余点
                    for (int i = 1; i < P1.Count; i++)
                    {
                        if (P1[i].Code == P1[js].Code)
                        {
                            x.Add(P1[i].x);
                            y.Add(P1[i].y);
                            z.Add(P1[i].z);

                            DeltP.Add(P1[i]);
                        }
                    }

                    //删除已画点 
                    for (int i = 0; i < DeltP.Count; i++)
                    {
                        P1.Remove(DeltP[i]);
                    }

                    //绘制
                    DSym.DF(Con.DFColor, x, y, z);

                    x.Clear();
                    y.Clear();
                    z.Clear();
                    DeltP.Clear();
                }
                //画砼房屋
                else if (P1[js].Code.Contains("TF"))
                {
                    x.Add(P1[js].x);
                    y.Add(P1[js].y);
                    z.Add(P1[js].z);
                    DeltP.Add(P1[js]);

                    //寻找其余点
                    for (int i = 1; i < P1.Count; i++)
                    {
                        if (P1[i].Code == P1[js].Code)
                        {
                            x.Add(P1[i].x);
                            y.Add(P1[i].y);
                            z.Add(P1[i].z);

                            DeltP.Add(P1[i]);
                        }
                    }

                    //删除已画点 
                    for (int i = 0; i < DeltP.Count; i++)
                    {
                        P1.Remove(DeltP[i]);
                    }

                    //绘制
                    Con.TFSize();
                    DSym.TF(Con.TFColor, x, y, z, DeltP[0].Code, Con.TFSize_Text);

                    x.Clear();
                    y.Clear();
                    z.Clear();
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

                    for (int i = 0; i < DeltP.Count; i++)
                    {
                        x.Add(DeltP[i].x);
                        y.Add(DeltP[i].y);
                        z.Add(DeltP[i].z);
                    }

                    //绘制
                    DSym.XL(Con.XLColor, x, y, z);

                    x.Clear();
                    y.Clear();
                    z.Clear();
                    DeltP.Clear();
                }
                //画公路
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
                        z.Add(DeltP[i].y);
                    }

                    //绘制
                    DSym.GL(Con.GLColor, x, y, z, DeltP[0].Code);

                    x.Clear();
                    y.Clear();
                    z.Clear();
                    DeltP.Clear();
                }
                //画路灯
                else if (P1[js].Code.Contains("LD"))
                {
                    x.Add(P1[js].x);
                    y.Add(P1[js].y);
                    z.Add(P1[js].z);
                    DeltP.Add(P1[js]);

                    //删除已画点 
                    for (int i = 0; i < DeltP.Count; i++)
                    {
                        P1.Remove(DeltP[i]);
                    }

                    //绘制
                    Con.Ldsize();
                    DSym.LD(Con.LDColor, x[0], y[0], z[0], Con.LdSize);

                    x.Clear();
                    y.Clear();
                    z.Clear();
                    DeltP.Clear();
                }
            }

            //写结尾
            DSym.sw.WriteLine(0);
            DSym.sw.WriteLine("ENDSEC");
            DSym.sw.WriteLine(0);
            DSym.sw.WriteLine("EOF");
            DSym.sw.Close();
        }
    }
}
