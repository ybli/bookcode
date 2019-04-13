using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms.DataVisualization.Charting;
using System.Threading;
namespace Contour_Line
{

    //点结构
    public struct point
    {
        public string PointName;
        public double x;
        public double y;
        public double z;
    }

    //线结构
    public struct side
    {
        public List<point> Sp;
    }

    //三角形结构
    public struct triangle
    {
        public List<point> Tp;
        public List<side> Ts;
    }

    //等高线结构
    public struct contour
    {
        public List<point> C;
    }

    //构建三角网以及生成等高线
    class Class_TIN_and_ContourLine
    {
        //已知数据
        public List<point> P = new List<point>();//散点数据

        //待计算数据
        public point BasePoint = new point();//基点
        public List<point> CP = new List<point>();//删除凸包点后的散点数据
        public List<point> O = new List<point>();
        public List<point> PO = new List<point>();//凸包点
        public List<side> S = new List<side>();//边数据
        public List<triangle> T1 = new List<triangle>();//三角形数据
        public List<triangle> T2 = new List<triangle>();//三角形数据
        public List<point> H_p = new List<point>();//等值点
        public List<contour> Contour = new List<contour>();//等高线
        public List<triangle> Intpedgs = new List<triangle>();
        public point P1 = new point();
        public point P2 = new point();
        public point P3 = new point();
        public side SameSide = new side();
        public side SameSide1 = new side();
        public double MinH;
        public double MaxH;
        public List<double> H = new List<double>();//等高线高程集合

        //图形
        public Bitmap TIN_Bmp;
        public Bitmap Contour_Bmp;

        public int Width = 500, Height = 500;
        public int Width1 = 500, Height1 = 500;

        //查找基点
        public void FindBase()
        {
            double a = P.Count;
            //储存包含最小Y值的所有点
            List<point> PointMinY = new List<point>();

            double MinY;
            List<double> Y = new List<double>();

            //计算最小的Y
            for (int i = 0; i < P.Count; i++)
                Y.Add(P[i].y);
            MinY = Y.Min();

            //遍历离散数据,将含有最小Y值的点全部取出
            for (int i = 0; i < P.Count; i++)
            {
                if (MinY == P[i].y)
                    PointMinY.Add(P[i]);
            }

            //判断含有最小Y值的点数量
            if (PointMinY.Count == 1)
                BasePoint = PointMinY[0];
            else
            {
                double MinX;
                List<double> minx = new List<double>();

                for (int i = 0; i < PointMinY.Count; i++)
                    minx.Add(PointMinY[i].x);
                MinX = minx.Min();

                for (int i = 0; i < PointMinY.Count; i++)
                {
                    if (MinX == PointMinY[i].x)
                    {
                        BasePoint = PointMinY[i];
                        break;
                    }
                }
            }
        }


        //按照夹角大小对离散点排序
        public void Rank()
        {
            List<point> M1 = new List<point>();
            M1.AddRange(P);

            M1.Remove(BasePoint);

            List<double> Angle = new List<double>();
            List<double> D = new List<double>();

            List<double> lAngle = new List<double>();
            List<double> lD = new List<double>();
            List<point> lo = new List<point>();

            for (int i = 0; i < M1.Count; i++)
            {
                //计算夹角
                double a;
                if (M1[i].x - BasePoint.x > 0)
                    a = Math.Atan((M1[i].y - BasePoint.y) / (M1[i].x - BasePoint.x));
                else
                    a = Math.PI - Math.Abs(Math.Atan((M1[i].y - BasePoint.y) / (M1[i].x - BasePoint.x)));

                Angle.Add(a);

                //计算距离
                D.Add(Math.Sqrt((M1[i].x - BasePoint.x) * (M1[i].x - BasePoint.x) +
                                (M1[i].y - BasePoint.y) * (M1[i].y - BasePoint.y)));
            }

            //按照夹角排序并剔除里基点近的点
            int sy;

            //排序后的夹角和距离
            List<double> Angle1 = new List<double>();
            List<double> D1 = new List<double>();

            for (int i = 0; i < P.Count - 1; i++)
            {
                sy = Angle.IndexOf(Angle.Min());

                O.Add(M1[sy]);
                Angle1.Add(Angle[sy]);
                D1.Add(D[sy]);

                if (O.Count > 1)
                {
                    int la = O.Count - 1;

                    //夹角相同判断距离
                    if (Angle1[la] == Angle1[la - 1])
                    {
                        if (D1[la] <= D1[la - 1])
                        {
                            O.RemoveAt(la);
                            Angle1.RemoveAt(la);
                            D1.RemoveAt(la);
                        }
                        else
                        {
                            O.RemoveAt(la - 1);
                            Angle1.RemoveAt(la - 1);
                            D1.RemoveAt(la - 1);
                        }
                    }
                }

                M1.RemoveAt(sy);
                Angle.RemoveAt(sy);
                D.RemoveAt(sy);
            }
        }

        //建立由凸包点构成的列表
        public void BuildS()
        {
            double m;
            point Pi = new point();
            point Pj = new point();
            point Pk = new point();

            PO.Add(BasePoint);
            PO.Add(O[0]);
            PO.Add(O[1]);

            for (int i = 2; i < O.Count; i++)
            {
                //次栈项元素
                Pi = PO[PO.Count - 2];

                //栈项元素
                Pj = PO[PO.Count - 1];

                //待压入点
                Pk = O[i];

                //计算叉积
                m = (Pi.x - Pj.x) * (Pk.y - Pj.y) - (Pi.y - Pj.y) * (Pk.x - Pj.x);

                //判断Pk是否入栈
                if (m > 0)  //左转
                {
                    PO.Remove(Pj);

                    while (m > 0)
                    {
                        //次栈项元素
                        Pi = PO[PO.Count - 2];

                        //栈项元素
                        Pj = PO[PO.Count - 1];

                        m = (Pi.x - Pj.x) * (Pk.y - Pj.y) - (Pi.y - Pj.y) * (Pk.x - Pj.x);


                        //左转
                        if (m > 0)
                            PO.Remove(Pj);
                        //右转
                        else
                        {
                            PO.Add(Pk);
                            break;
                        }
                    }

                }
                else       //右转
                {
                    PO.Add(Pk);
                }
            }

            PO.Add(BasePoint);
        }

        //将所有点沿X轴排序
        public void SortX()
        {
            CP.AddRange(P);

            //将凸包点从原有的散点中删除
            for (int i = 0; i < PO.Count - 1; i++)
            {
                CP.Remove(PO[i]);
            }


            List<double> lx = new List<double>();
            List<point> LM = new List<point>();

            for (int i = 0; i < CP.Count; i++)
            {
                lx.Add(CP[i].x);
            }

            int s = CP.Count;

            for (int i = 0; i < s; i++)
            {
                int a = lx.IndexOf(lx.Min());

                LM.Add(CP[a]);

                CP.RemoveAt(a);
                lx.RemoveAt(a);

            }


            CP.Clear();

            CP.AddRange(LM);
        }

        //函数——判断P是否在外接圆的内部
        public bool JudgeInTriangle(point P, triangle T)
        {
            bool Inside = true;
            point A = new point();
            point B = new point();
            point C = new point();

            A = T.Tp[0];
            B = T.Tp[1];
            C = T.Tp[2];

            double x1, y1, x2, y2, x3, y3;
            x1 = A.x;
            y1 = A.y;
            x2 = B.x;
            y2 = B.y;
            x3 = C.x;
            y3 = C.y;

            double x0, y0, r, d;

            x0 = ((B.y - A.y) * (C.y * C.y - A.y * A.y + C.x * C.x - A.x * A.x) - (C.y - A.y) * (B.y * B.y - A.y * A.y + B.x * B.x - A.x * A.x)) /
                (2 * (C.x - A.x) * (B.y - A.y) - 2 * (B.x - A.x) * (C.y - A.y));
            y0 = ((B.x - A.x) * (C.x * C.x - A.x * A.x + C.y * C.y - A.y * A.y) - (C.x - A.x) * (B.x * B.x - A.x * A.x + B.y * B.y - A.y * A.y)) /
                (2 * (C.y - A.y) * (B.x - A.x) - 2 * (B.y - A.y) * (C.x - A.x));

            r = Math.Round(Math.Sqrt((x0 - A.x) * (x0 - A.x) + (y0 - A.y) * (y0 - A.y)), 4);

            d = Math.Round(Math.Sqrt((x0 - P.x) * (x0 - P.x) + (y0 - P.y) * (y0 - P.y)), 4);

            if (d <= r)
                Inside = true;
            else
                Inside = false;

            return Inside;
        }

        //函数——判断两个三角形是否有公共边
        public bool JudgeSame(triangle t1, triangle t2)
        {
            bool same = true;
            for (int i = 0; i < t1.Ts.Count; i++)  //t1的三条边
            {
                for (int j = 0; j < t2.Ts.Count; j++)  //t2的三条边
                {
                    if ((t1.Ts[i].Sp[0].x == t2.Ts[j].Sp[0].x && t1.Ts[i].Sp[0].y == t2.Ts[j].Sp[0].y &&
                         t1.Ts[i].Sp[1].x == t2.Ts[j].Sp[1].x && t1.Ts[i].Sp[1].y == t2.Ts[j].Sp[1].y) ||
                        (t1.Ts[i].Sp[0].x == t2.Ts[j].Sp[1].x && t1.Ts[i].Sp[0].y == t2.Ts[j].Sp[1].y &&
                         t1.Ts[i].Sp[1].x == t2.Ts[j].Sp[0].x && t1.Ts[i].Sp[1].y == t2.Ts[j].Sp[0].y))
                    {
                        SameSide = t1.Ts[i];
                        SameSide1 = t2.Ts[j];
                        same = true;
                        break;
                    }
                    else
                        same = false;
                }
                if (same)
                    break;
            }
            return same;
        }

        //计算三角形面积
        public double Area(point A,point B,point C)
        {
            double s=0;

            double a,b,c;


            a = Math.Sqrt(Math.Pow(B.x - C.x, 2) + Math.Pow(B.y - C.y, 2));
            b = Math.Sqrt(Math.Pow(A.x - C.x, 2) + Math.Pow(A.y - C.y, 2));
            c = Math.Sqrt(Math.Pow(A.x - B.x, 2) + Math.Pow(A.y - B.y, 2));

            double p = 0;

            p = (a + b + c) / 2;

            s = Math.Sqrt(p * (p - a) * (p - b) * (p - c));

            return s;
        }

        //判断点是否位于三角形内
        public bool PinT(point p,triangle t)
        {
            bool In = true;

            double s1, s2, s3, s4;

            s1 = Area(t.Tp[0], t.Tp[1], t.Tp[2]);
            s2 = Area(t.Tp[0], t.Tp[1], p);
            s3 = Area(t.Tp[1], t.Tp[2], p);
            s4 = Area(t.Tp[0], t.Tp[2], p);

            if (Math.Round(s1,4) == Math.Round((s2 + s3 + s4),4))
            {
                In = true;
            }
            else
            {
                In = false;
            }


            return In;
        }


        //构建初始三角网
        public void BuildInitialTin()
        {
            int a = CP.Count;

            for (int i = 0; i < PO.Count - 1; i++)
            {
                side l1 = new side();
                side l2 = new side();
                side l3 = new side();

                l1.Sp = new List<point>();
                l2.Sp = new List<point>();
                l3.Sp = new List<point>();

                triangle t1 = new triangle();
                t1.Ts = new List<side>();
                t1.Tp = new List<point>();

                t1.Tp.Add(PO[i]);
                t1.Tp.Add(PO[i + 1]);
                t1.Tp.Add(CP[0]);

                l1.Sp.Add(PO[i]);
                l1.Sp.Add(PO[i + 1]);

                l2.Sp.Add(PO[i + 1]);
                l2.Sp.Add(CP[0]);

                l3.Sp.Add(PO[i]);
                l3.Sp.Add(CP[0]);

                t1.Ts.Add(l1);
                t1.Ts.Add(l2);
                t1.Ts.Add(l3);

                T1.Add(t1);
            }
        }


        //生成平面三角网
        public void BuildPlaneTIN()
        {
            //循环点
            for (int i = 1; i < CP.Count; i++)
            {
                int T1count = T1.Count;
                List<triangle> T3 = new List<triangle>();

                //找出所有外接圆包含P的三角形
                for (int j = 0; j < T1count; j++)
                {
                    if (JudgeInTriangle(CP[i], T1[j]))
                    {
                        T3.Add(T1[j]);
                    }
                }

                //定位
                List<triangle> FT = new List<triangle>();
                for (int k = 0; k < T3.Count; k++)
                {
                    if (PinT(CP[i], T3[k]))
                    {
                        FT.Add(T3[k]);
                        T3.Remove(T3[k]);
                        break;
                    }
                }

                if (FT.Count > 0)
                {
                    if (T3.Count > 0)
                    {
                        int[] Tnote = new int[T3.Count];

                        //设立标志数组
                        for (int t = 0; t < T3.Count; t++)
                        {
                            Tnote[t] = 0;
                        }

                        int v = 0;
                        int num = 0;
                        bool F = true;
                        while (F)
                        {
                            if (Tnote[v] == 0)
                            {
                                for (int vv = 0; vv < FT.Count; vv++)
                                {
                                    if (JudgeSame(T3[v], FT[vv]))
                                    {
                                        FT.Add(T3[v]);
                                        Tnote[v] = 1;
                                        v = -1;
                                        break;
                                    }
                                    else
                                    {
                                        num = vv;
                                    }
                                }

                                if (v == (T3.Count - 1) && num == (FT.Count - 1))
                                {
                                    F = false;
                                }
                            }

                            v += 1;

                            if (v == T3.Count)
                            {
                                F = false;
                            }
                        }
                    }

                    //将三角形加入到T2中
                    T2.AddRange(FT);

                    //移除T1中三角形;
                    for (int y = 0; y < T2.Count; y++)
                    {
                        T1.Remove(T2[y]);
                    }

                    //寻找公共边
                    for (int t2 = 0; t2 < T2.Count - 1; t2++)  //第一个三角形
                    {
                        int ii = t2 + 1;  //其余三角形
                        while (ii <= T2.Count - 1)
                        {
                            if (JudgeSame(T2[t2], T2[ii]))
                            {
                                //移除公共边
                                T2[t2].Ts.Remove(SameSide);
                                T2[ii].Ts.Remove(SameSide1);
                            }
                            ii += 1;
                        }
                    }

                    //将剩下的边添加到S中
                    S.Clear();
                    for (int a1 = 0; a1 < T2.Count; a1++)
                    {
                        for (int a2 = 0; a2 < T2[a1].Ts.Count; a2++)
                        {
                            S.Add(T2[a1].Ts[a2]);
                        }
                    }

                    //清空T2
                    T2.Clear();

                    //生成新的三角形并添加到T1中
                    for (int a3 = 0; a3 < S.Count; a3++)
                    {
                        side s0 = new side();
                        side s1 = new side();
                        side s2 = new side();
                        triangle t = new triangle();

                        //实例化
                        s0.Sp = new List<point>();
                        s1.Sp = new List<point>();
                        s2.Sp = new List<point>();
                        t.Tp = new List<point>();
                        t.Ts = new List<side>();

                        //三个顶点
                        t.Tp.Add(S[a3].Sp[0]);
                        t.Tp.Add(S[a3].Sp[1]);
                        t.Tp.Add(CP[i]);

                        //三条边
                        s0 = S[a3];
                        s1.Sp.Add(S[a3].Sp[1]);
                        s1.Sp.Add(CP[i]);
                        s2.Sp.Add(S[a3].Sp[0]);
                        s2.Sp.Add(CP[i]);



                        t.Ts.Add(s0);
                        t.Ts.Add(s1);
                        t.Ts.Add(s2);

                        T1.Add(t);
                    }
                }
            }
        }

        //计算角度
        public double a1, a2, a3;

        public void Angle(triangle t)
        {
            double a, b, c;

            point A, B, C;

            A = new point();
            B = new point();
            C = new point();

            A.x = t.Tp[0].x;
            A.y = t.Tp[0].y;
            B.x = t.Tp[1].x;
            B.y = t.Tp[1].y;
            C.x = t.Tp[2].x;
            C.y = t.Tp[2].y;

            a = Math.Sqrt(Math.Pow((B.x - C.x), 2) + Math.Pow((B.y - C.y), 2));
            b = Math.Sqrt(Math.Pow((A.x - C.x), 2) + Math.Pow((A.y - C.y), 2));
            c = Math.Sqrt(Math.Pow((A.x - B.x), 2) + Math.Pow((A.y - B.y), 2));

            double cosB, cosC;

            cosB = (c * c + a * a - b * b) / (2 * a * c);
            cosC = (a * a + b * b - c * c) / (2 * a * b);

            a2 = Math.Acos(cosB);
            a3 = Math.Acos(cosC);
            a1 = Math.PI - a2 - a3;
        }

        //删除三角形
        public void DeleteT()
        {
            List<triangle> DelT = new List<triangle>();//三角形数据

            PO.RemoveAt(0);

            for (int i = 0; i < T1.Count; i++)
            {
                for (int j = 0; j < PO.Count; j++)
                {
                    if (T1[i].Tp.Contains(PO[j]))
                    {
                        DelT.Add(T1[i]);
                    }
                }
            }

            double MaxA = 160 * Math.PI / 180;
            double MinA = 5 * Math.PI / 180;

            List<triangle> D = new List<triangle>();//三角形数据

            for (int i = 0; i < DelT.Count; i++)
            {
                Angle(DelT[i]);

                if (a1 > MaxA || a2 > MaxA || a3 > MaxA || a1 < MinA || a2 < MinA || a3 < MinA)
                {
                    D.Add(DelT[i]);
                }
            }

            for (int i = 0; i < D.Count; i++)
            {
                T1.Remove(D[i]);
            }
        }

        //绘制三角网
        public void Draw_TIN()
        {
            TIN_Bmp = new Bitmap(Width1, Height1);
            MainInterface.f1.pb.Size = new Size(Width1, Height1);
            //调用绘图类
            Class_Draw draw = new Class_Draw();
            draw.gp = Graphics.FromImage(TIN_Bmp);
            draw.gp.Clear(Color.Black);

            //将网点坐标储存在列表中
            List<double> x = new List<double>();
            List<double> y = new List<double>();

            for (int i = 0; i < T1.Count; i++)
            {
                x.Add(T1[i].Tp[0].x);
                x.Add(T1[i].Tp[1].x);
                x.Add(T1[i].Tp[2].x);

                y.Add(T1[i].Tp[0].y);
                y.Add(T1[i].Tp[1].y);
                y.Add(T1[i].Tp[2].y);
            }

            draw.CalRatio(x, y, Width1, Height1);

            draw.Change(x, y, Width1 / 2, Height1 / 2);

            //画线
            Pen p1 = new Pen(Color.Red);
            for (int i = 0; i < T1.Count; i++)
            {
                draw.W_line(draw.px[i * 3], draw.py[i * 3], draw.px[i * 3 + 1], draw.py[i * 3 + 1], p1);
                draw.W_line(draw.px[i * 3 + 1], draw.py[i * 3 + 1], draw.px[i * 3 + 2], draw.py[i * 3 + 2], p1);
                draw.W_line(draw.px[i * 3], draw.py[i * 3], draw.px[i * 3 + 2], draw.py[i * 3 + 2], p1);
            }

            //画点
            for (int i = 0; i < T1.Count; i++)
            {
                draw.D_circle(draw.px[i * 3], draw.py[i * 3], 2);
                draw.D_circle(draw.px[i * 3 + 1], draw.py[i * 3 + 1], 2);
                draw.D_circle(draw.px[i * 3 + 2], draw.py[i * 3 + 2], 2);
            }
        }



        //判断三角形的一条边中是否存在等值点
        public bool Is_Between(double h, side S)
        {
            bool Is = true;

            if ((h - S.Sp[0].z) * (h - S.Sp[1].z) <= 0)
                Is = true;
            else
                Is = false;

            return Is;
        }

        //等高线位置的确定
        public int dp = 0;
        public point Line_Intp(double h, side S)
        {
            point p = new point();

            double x = 0, y = 0;

            double x1,y1,z1,x2,y2,z2;

            if (S.Sp[0].z == h || S.Sp[1].z == h)
            {
                if (S.Sp[0].z == h)
                {
                    p = S.Sp[0];
                }
                else
                {
                    p = S.Sp[1];
                }

                dp = 1;
            }
            else
            {
                x1 = S.Sp[0].x;
                y1 = S.Sp[0].y;
                z1 = S.Sp[0].z;

                x2 = S.Sp[1].x;
                y2 = S.Sp[1].y;
                z2 = S.Sp[1].z;

                x = x1 + (x2 - x1) / (z2 - z1) * (h - z1);
                y = y1 + (y2 - y1) / (z2 - z1) * (h - z1);

                p.x = x;
                p.y = y;
                p.z = h;

                dp = 0;
            }

            return p;
        }

        //三角形内插，找到高程为h的所有三角形
        public void Tri_Intp(double h)
        {

            List<triangle> LT = new List<triangle>();
            LT.AddRange(T1);

            //寻找高程为“h”的等高线穿过的所有三角形
            for (int i = 0; i < LT.Count; i++)
            {
                if (Is_Between(h, LT[i].Ts[0]) && Is_Between(h, LT[i].Ts[1]) || Is_Between(h, LT[i].Ts[2]))
                {
                    Intpedgs.Add(LT[i]);
                }
            }


            //删除只有一个顶点被穿过的三角形
            List<triangle> Dt1 = new List<triangle>();

            for (int i = 0; i < Intpedgs.Count; i++)
            {
                if (Intpedgs[i].Tp[0].z == h || Intpedgs[i].Tp[1].z == h || Intpedgs[i].Tp[2].z == h)
                {
                    side Ls = new side();
                    Ls.Sp = new List<point>();

                    if (Intpedgs[i].Tp[0].z == h)
                    {
                        Ls.Sp.Add(Intpedgs[i].Tp[1]);
                        Ls.Sp.Add(Intpedgs[i].Tp[2]);
                    }
                    else if (Intpedgs[i].Tp[1].z == h)
                    {
                        Ls.Sp.Add(Intpedgs[i].Tp[0]);
                        Ls.Sp.Add(Intpedgs[i].Tp[2]);
                    }
                    else if (Intpedgs[i].Tp[2].z == h)
                    {
                        Ls.Sp.Add(Intpedgs[i].Tp[0]);
                        Ls.Sp.Add(Intpedgs[i].Tp[1]);
                    }

                    if (!(Is_Between(h, Ls)))
                    {
                        Dt1.Add(Intpedgs[i]);
                    }
                }
            }

            for (int i = 0; i < Dt1.Count; i++)
            {
                Intpedgs.Remove(Dt1[i]);
            }
        }


        //等高线的追踪——(一个高程值)
        public void Puesr(double h)
        {
            Intpedgs.Clear();
            Tri_Intp(h);

            //判断开曲线是否转向
            bool flag = false;

            //起始边
            side Str_Line = new side();

            point Str_Point = new point();

            //离去边
            side Leave_Line = new side();

            side L1 = new side();
            side L2 = new side();

            //设立标记数组
            int[] Tri_Note = new int[Intpedgs.Count];

            for (int i = 0; i < Intpedgs.Count; i++)
            {
                Tri_Note[i] = 0;
            }
         
            point p1 = new point();

            //遍历高程为"h"的等高线所穿过的三角形
            for (int i = 0; i < Intpedgs.Count; i++)
            {
                if (Tri_Note[i] == 0)
                {
                    H_p.Clear();

                    flag = false;

                    //寻找搜索起点
                    if (Is_Between(h, Intpedgs[i].Ts[0]))
                    {
                        Str_Line = Intpedgs[i].Ts[0];
                        L1 = Intpedgs[i].Ts[1];
                        L2 = Intpedgs[i].Ts[2];
                    }
                    else if (Is_Between(h, Intpedgs[i].Ts[1]))
                    {
                        Str_Line = Intpedgs[i].Ts[1];
                        L1 = Intpedgs[i].Ts[0];
                        L2 = Intpedgs[i].Ts[2];
                    }
                    else
                    {
                        Str_Line = Intpedgs[i].Ts[2];
                        L1 = Intpedgs[i].Ts[0];
                        L2 = Intpedgs[i].Ts[1];
                    }
                    Tri_Note[i] = 1;   //对处理过的三角形标记，以后不再处理

                    p1 = Line_Intp(h, Str_Line);
                    H_p.Add(p1);

                    //寻找离去边
                    point p12 = new point();

                    //过三角形顶点
                    if (dp == 1)
                    {
                        if (L1.Sp.Contains(p1))
                        {
                            Leave_Line = L2;

                            p12 = Line_Intp(h, Leave_Line);
                            H_p.Add(p12);
                        }
                        else if (L2.Sp.Contains(p1))
                        {
                            Leave_Line = L1;

                            p12 = Line_Intp(h, Leave_Line);
                            H_p.Add(p12);
                        }
                    }
                    else
                    {
                        if (Is_Between(h, L1))
                        {
                            Leave_Line = L1;

                            p12 = Line_Intp(h, Leave_Line);
                            H_p.Add(p12);
                        }
                        else
                        {
                            Leave_Line = L2;

                            p12 = Line_Intp(h, Leave_Line);
                            H_p.Add(p12);
                        }

                        if (dp == 1)
                        {
                            Str_Point = p12;

                            for (int look = 0; look < Intpedgs.Count; look++)
                            {
                                if (Tri_Note[look] == 0)
                                {
                                    //找相邻三角形
                                    if (Intpedgs[look].Tp.Contains(Str_Point))
                                    {
                                        //找离去边
                                        if (Intpedgs[look].Ts[0].Sp.Contains(Str_Point))
                                        {
                                            Leave_Line = Intpedgs[look].Ts[0];
                                        }
                                        else if (Intpedgs[look].Ts[1].Sp.Contains(Str_Point))
                                        {
                                            Leave_Line = Intpedgs[look].Ts[1];
                                        }
                                        else
                                        {
                                            Leave_Line = Intpedgs[look].Ts[2];
                                        }

                                        break;
                                    }
                                }
                            }
                        }
                    }


                    //进入相邻三角形
                    bool Finish = true;  //判断一条等高线是否寻找完成

                    int t = 0;

                    while (Finish)
                    {
                        //寻找相邻三角形
                        if (Tri_Note[t] == 0)
                        {
                            //三角形定位
                            if (Intpedgs[t].Tp.Contains(Leave_Line.Sp[0]) && Intpedgs[t].Tp.Contains(Leave_Line.Sp[1]))
                            {
                                Tri_Note[t] = 1;

                                //确定进入边
                                if (Intpedgs[t].Ts[0].Sp.Contains(Leave_Line.Sp[0]) && Intpedgs[t].Ts[0].Sp.Contains(Leave_Line.Sp[1]))
                                {
                                    L1 = Intpedgs[t].Ts[1];
                                    L2 = Intpedgs[t].Ts[2];
                                }
                                else if (Intpedgs[t].Ts[1].Sp.Contains(Leave_Line.Sp[0]) && Intpedgs[t].Ts[1].Sp.Contains(Leave_Line.Sp[1]))
                                {
                                    L1 = Intpedgs[t].Ts[0];
                                    L2 = Intpedgs[t].Ts[2];
                                }
                                else
                                {
                                    L1 = Intpedgs[t].Ts[0];
                                    L2 = Intpedgs[t].Ts[1];
                                }

                                //寻找离去边
                                if (dp == 1)
                                {
                                    if (L1.Sp.Contains(p12))
                                    {
                                        Leave_Line = L2;

                                        p12 = Line_Intp(h, Leave_Line);
                                        H_p.Add(p12);
                                    }
                                    else if (L2.Sp.Contains(p12))
                                    {
                                        Leave_Line = L1;

                                        p12 = Line_Intp(h, Leave_Line);
                                        H_p.Add(p12);
                                    }
                                }
                                else
                                {
                                    if (Is_Between(h, L1))
                                    {
                                        Leave_Line = L1;

                                        p12 = Line_Intp(h, Leave_Line);
                                        H_p.Add(p12);
                                    }
                                    else
                                    {
                                        Leave_Line = L2;

                                        p12 = Line_Intp(h, Leave_Line);
                                        H_p.Add(p12);
                                    }

                                    if (dp == 1)
                                    {
                                        Str_Point = p12;

                                        for (int look = 0; look < Intpedgs.Count; look++)
                                        {
                                            if (Tri_Note[look] == 0)
                                            {
                                                //找相邻三角形
                                                if (Intpedgs[look].Tp.Contains(Str_Point))
                                                {
                                                    //找离去边
                                                    if (Intpedgs[look].Ts[0].Sp.Contains(Str_Point))
                                                    {
                                                        Leave_Line = Intpedgs[look].Ts[0];
                                                    }
                                                    else if (Intpedgs[look].Ts[1].Sp.Contains(Str_Point))
                                                    {
                                                        Leave_Line = Intpedgs[look].Ts[1];
                                                    }
                                                    else
                                                    {
                                                        Leave_Line = Intpedgs[look].Ts[2];
                                                    }

                                                    break;
                                                }
                                            }
                                        }
                                    }
                                }

                                t = -1;
                            }
                        }

                        //闭曲线
                        if ((H_p[0].x == H_p[H_p.Count - 1].x) && (H_p[0].y == H_p[H_p.Count - 1].y))
                        {
                            break;
                        }
                        //开曲线
                        else
                        {
                            if (t == Intpedgs.Count - 1 && flag == false)
                            {
                                //将开曲线转向
                                H_p.Reverse();

                                Leave_Line = Str_Line;

                                flag = true;

                                t = -1;
                            }
                        }

                        if (t == Intpedgs.Count - 1)
                            break;

                        t += 1;
                    }

                    contour Contour1 = new contour();

                    Contour1.C = new List<point>();
                    Contour1.C.AddRange(H_p);

                    Contour.Add(Contour1);

                    int a = Contour.Count;
                }
            }
        }


        //寻找所有等高线
        public void Purse_All(double dh)
        {
            List<double> z = new List<double>();

            //求最小等高线高程
            for (int i = 0; i < T1.Count; i++)
            {
                z.Add(T1[i].Tp[0].z);
                z.Add(T1[i].Tp[1].z);
                z.Add(T1[i].Tp[2].z);
            }


            MinH = z.Min();
            MaxH = z.Max();

            double h;

            h = Math.Floor(MinH) + 1;

            while (h <= MaxH)
            {
                H.Add(h);

                Puesr(h);

                h += dh;
            }
        }

        //绘制等高线和——窗体界面绘图
        public void Draw_Contour()
        {
            Contour_Bmp = new Bitmap(Width, Height);

            MainInterface.f1.pb1.Size = new Size(Width, Height);

            //调用绘图类
            Class_Draw draw = new Class_Draw();
            draw.gp = Graphics.FromImage(Contour_Bmp);
            draw.gp.Clear(Color.Black);

            List<double> x = new List<double>();
            List<double> y = new List<double>();
            List<double> z = new List<double>();

            //将网点坐标储存在列表中
            for (int i = 0; i < P.Count; i++)
            {
                x.Add(P[i].x);
                y.Add(P[i].y);
            }

            //将等值点储存在列表中
            for (int i = 0; i < Contour.Count; i++)
            {
                for (int j = 0; j < Contour[i].C.Count; j++)
                {
                    x.Add(Contour[i].C[j].x);
                    y.Add(Contour[i].C[j].y);
                    z.Add(Contour[i].C[j].z);
                }
            }

            draw.CalRatio(x, y, Width - 100, Height - 100);

            draw.Change(x, y, Width / 2, Height / 2);


            float FontSize = 0;
            FontSize = Width / 200;

            Font ft = new Font("宋体", FontSize, FontStyle.Regular);
            //绘制散点图
            for (int i = 0; i < P.Count; i++)
            {
                draw.D_text(draw.px[i], draw.py[i], P[i].z.ToString("F2"), ft);
                draw.D_circle(draw.px[i], draw.py[i], (float)Width / 2000);
            }

            //绘制等高线
            List<double> x2 = new List<double>();
            List<double> y2 = new List<double>();

            for (int i = P.Count; i < x.Count; i++)
            {
                x2.Add(draw.px[i]);
                y2.Add(draw.py[i]);
            }

            int sum = 0;
            for (int i = 0; i < Contour.Count; i++)
            {

                List<double> x1 = new List<double>();
                List<double> y1 = new List<double>();
                List<double> z1 = new List<double>();

                for (int j = 0; j < Contour[i].C.Count; j++)
                {
                    if (i == 0)
                    {
                        x1.Add(x2[j]);
                        y1.Add(y2[j]);
                        z1.Add(z[j]);
                    }
                    else
                    {
                        x1.Add(x2[sum + j]);
                        y1.Add(y2[sum + j]);
                        z1.Add(z[sum + j]);
                    }
                }

                if (z1[0] % 5 == 0)
                {
                    Pen Jp = new Pen(Color.Green, 2);

                    draw.D_Curve(x1, y1, Jp);
                }
                else
                {
                    Pen Jp = new Pen(Color.Yellow);

                    draw.D_Curve(x1, y1, Jp);
                }

                sum += Contour[i].C.Count;
            }
        }

        //绘制CAD图形
        public void Draw_DXF(string FileName)
        {
            //调用绘图类
            Class_Draw_DXF dxf= new Class_Draw_DXF();

            dxf.sw = new System.IO.StreamWriter(FileName);

            List<double> x = new List<double>();
            List<double> y = new List<double>();
            List<double> z = new List<double>();

            //将网点坐标储存在列表中
            for (int i = 0; i < T1.Count; i++)
            {
                x.Add(T1[i].Tp[0].x);
                x.Add(T1[i].Tp[1].x);
                x.Add(T1[i].Tp[2].x);

                y.Add(T1[i].Tp[0].y);
                y.Add(T1[i].Tp[1].y);
                y.Add(T1[i].Tp[2].y);

                z.Add(T1[i].Tp[0].z);
                z.Add(T1[i].Tp[1].z);
                z.Add(T1[i].Tp[2].z);
            }

            //将等值点储存在列表中
            for (int i = 0; i < Contour.Count; i++)
            {
                for (int j = 0; j < Contour[i].C.Count; j++)
                {
                    x.Add(Contour[i].C[j].x);
                    y.Add(Contour[i].C[j].y);
                    z.Add(Contour[i].C[j].z);
                }
            }

            //参数计算
            dxf.CalRatio(x, y);

            //限定绘图区域
            dxf.ConstRange();


            //绘制三角网
            for (int i = 0; i < T1.Count; i++)
            {
                dxf.D_Line(x[i * 3], y[i * 3], z[i * 3], x[i * 3 + 1], y[i * 3 + 1], z[i * 3 + 1], 1);
                dxf.D_Line(x[i * 3 + 1], y[i * 3 + 1], z[i * 3 + 1], x[i * 3 + 2], y[i * 3 + 2], z[i * 3 + 2], 1);
                dxf.D_Line(x[i * 3], y[i * 3], z[i * 3], x[i * 3 + 2], y[i * 3 + 2], z[i * 3 + 2], 1);
            }

            //绘制等高线
            List<double> x2 = new List<double>();
            List<double> y2 = new List<double>();
            List<double> z2 = new List<double>();

            for (int i = 3 * T1.Count; i < x.Count; i++)
            {
                x2.Add(x[i]);
                y2.Add(y[i]);
                z2.Add(z[i]);
            }

            int sum = 0;
            for (int i = 0; i < Contour.Count; i++)
            {

                List<double> x1 = new List<double>();
                List<double> y1 = new List<double>();
                List<double> z1 = new List<double>();

                for (int j = 0; j < Contour[i].C.Count; j++)
                {
                    if (i == 0)
                    {
                        x1.Add(x2[j]);
                        y1.Add(y2[j]);
                        z1.Add(z2[j]);
                    }
                    else
                    {
                        x1.Add(x2[sum + j]);
                        y1.Add(y2[sum + j]);
                        z1.Add(z2[sum + j]);
                    }
                }

                if (z1[0] % 5 == 0)
                {
                    for (int l = 0; l < x1.Count - 1; l++)
                    {
                        dxf.D_Line(x1[l], y1[l], z1[l], x1[l + 1], y1[l + 1], z1[l + 1], 3);
                    }
                }
                else
                {
                    for (int l = 0; l < x1.Count - 1; l++)
                    {
                        dxf.D_Line(x1[l], y1[l], z1[l], x1[l + 1], y1[l + 1], z1[l + 1], 2);
                    }
                }

                sum += Contour[i].C.Count;
            }



            dxf.D_End();
        }


        struct H_S
        {
           public  double H;
           public  double S;
        }

        //计算报告
        public string Report()
        {
            string a = null;
            a += "************************TIN体积计算报告************************\n";
            a += "\n------------------基本信息------------------\n";
            a += "三角形个数：" + T1.Count.ToString() + "\n";
            a += "等高线数量：" + Contour.Count.ToString("N0") + "\n";

            a += "\n------------------前20个三角形------------------\n";
            a += "序号      A         B         C\n";
            for (int i = 0; i < T1.Count; i++)
            {
                a += string.Format("{0,-10}{1,-10}{2,-10}{3,-10}\n",
                                   i + 1, T1[i].Tp[0].PointName, T1[i].Tp[1].PointName, T1[i].Tp[2].PointName);
            }

            a += "\n------------------凸包点------------------\n";
            a += "序号            点名\n";
            for (int i = 0; i < PO.Count; i++)
            {
                a += string.Format("{0,-22}{1,-10}\n", i + 1, PO[i].PointName);
            }

            List<H_S> Dgx = new List<H_S>();

            List<contour> Contour1 = new List<contour>();

            Contour1.AddRange(Contour);

            for (int i = 0; i < H.Count; i++)
            {
                List<contour> Contour2 = new List<contour>();

                H_S s = new H_S();

                s.H = H[i];

                double h2;
                h2 = H[i];

                int j = 0;

                while (j < Contour1.Count)
                {

                    double hl = 0;

                    hl = Contour1[j].C[0].z;

                    if (hl == h2)
                    {
                        s.S += 1;

                        Contour2.Add(Contour1[j]);
                    }

                    j += 1;
                }

                Dgx.Add(s);

                for (int ll = 0; ll < Contour2.Count; ll++)
                {
                    Contour1.Remove(Contour2[ll]);
                }

                
            }

            a += "\n------------------等高线信息------------------\n";
            a += "高程                  数量\n";
            for (int i = 0; i < Dgx.Count; i++)
            {
                a += string.Format("{0,-22}{1,-10}\n", Dgx[i].H, Dgx[i].S);
            }

            return a;
        }
    }
}
