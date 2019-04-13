using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TIN
{
    internal class TIN
    {
        #region 字段
        private double hr;
        private Tpoint[] tpoints;
        private List<Tpoint> listp;
        #endregion

        #region 属性

        /// <summary>
        /// 参考面高程
        /// </summary>
        public double H_start
        {
            get { return hr; }
        }

        public Tpoint[] PointCloud
        {
            get { return tpoints; }
            set { tpoints = value; }
        }

        /// <summary>
        /// 平衡高程
        /// </summary>
        public double H0
        {
            get { return SetH0(); }
        }
            
        /// <summary>
        /// 三角网点集
        /// </summary>
        public List<Tpoint> TinNetP
        {
            get { return listp; }
        }

        /// <summary>
        /// 三角网三角形集
        /// </summary>
        public List<Triangle> Net
        {
            get
            {
                return SetNet();
            }
        }       

        /// <summary>
        /// 挖方体积（+）
        /// </summary>
        public double V_cut
        {
            get
            {
                double V = 0.0;
                for (int i = 0; i < Net.Count; i++)
                    V += Net[i].V_cut;
                return V;
            }
        }
        /// <summary>
        /// 填方体积（-）
        /// </summary>
        public double V_fill
        {
            get
            {
                double V = 0.0;
                for (int i = 0; i < Net.Count; i++)
                    V += Net[i].V_fill;
                return V;
            }
        }

        public double V_sum
        {
            get { return V_cut + V_fill; }
        }

        #endregion

        #region 构造
        public TIN(double h,Tpoint[] ts)
        {
            hr = h;
            tpoints = ts;           
        }
        #endregion

        #region 方法

        /// <summary>
        /// 生成初始矩形
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        private Tpoint[] Build_Matrix(Tpoint[] p)
        {
            double[] X = new double[p.Length];
            double[] Y = new double[p.Length];
            for (int i = 0; i < p.Length; i++)
            {
                X[i] = p[i].x;
                Y[i] = p[i].y;
            }
            double Xmin, Xmax, Ymin, Ymax;
            Xmin = X.Min();
            Xmax = X.Max();
            Ymax = Y.Max();
            Ymin = Y.Min();
            Tpoint[] Matrix = new Tpoint[4];
            Matrix[0] = new Tpoint();
            Matrix[0].Name = "P1";
            Matrix[0].x = Xmin - 1;
            Matrix[0].y = Ymin - 1;
            Matrix[1] = new Tpoint();
            Matrix[1].Name = "P2";
            Matrix[1].x = Xmin - 1;
            Matrix[1].y = Ymax + 1;
            Matrix[2] = new Tpoint();
            Matrix[2].Name = "P3";
            Matrix[2].x = Xmax + 1;
            Matrix[2].y = Ymax + 1;
            Matrix[3] = new Tpoint();
            Matrix[3].Name = "P4";
            Matrix[3].x = Xmax + 1;
            Matrix[3].y = Ymin - 1;
            return Matrix;
        }

        /// <summary>
        /// 生成初始三角网
        /// </summary>
        /// <param name="Matrix"></param>
        /// <returns></returns>
        private List<Tpoint> BuildInitialTrinet(Tpoint[] Matrix)
        {
            List<Tpoint> T1 = new List<Tpoint>
            {
                Matrix[0],
                Matrix[1],
                Matrix[2],
                Matrix[0],
                Matrix[2],
                Matrix[3]
            };
            return T1;
        }

        /// <summary>
        /// 通过遍历离散点，生成平面三角网
        /// </summary>
        /// <param name="p"></param>
        /// <param name="T1"></param>
        /// <param name="S"></param>
        /// <returns></returns>
        private List<Tpoint> BuildTrinet(Tpoint[] p, List<Tpoint> T1)
        {
            List<Tpoint> T2 = new List<Tpoint>();
            List<Side> S = new List<Side>();

            for (int i = 0; i < p.Length; i++)
            {
                for (int j = 0; j < T1.Count - 2; j += 3)
                {
                    Tpoint A = new Tpoint
                    {
                        x = T1[j].x,
                        y = T1[j].y
                    };
                    Tpoint B = new Tpoint
                    {
                        x = T1[j + 1].x,
                        y = T1[j + 1].y
                    };
                    Tpoint C = new Tpoint
                    {
                        x = T1[j + 2].x,
                        y = T1[j + 2].y
                    };
                    double x0, y0, r, lr;
                    x0 = ((B.y - A.y) * (C.y * C.y - A.y * A.y + C.x * C.x - A.x * A.x) - (C.y - A.y) * (B.y * B.y - A.y * A.y + B.x * B.x - A.x * A.x))
                        / (2 * (C.x - A.x) * (B.y - A.y) - 2 * (B.x - A.x) * (C.y - A.y));
                    y0 = ((B.x - A.x) * (C.x * C.x - A.x * A.x + C.y * C.y - A.y * A.y) - (C.x - A.x) * (B.x * B.x - A.x * A.x + B.y * B.y - A.y * A.y))
                        / (2 * (C.y - A.y) * (B.x - A.x) - 2 * (B.y - A.y) * (C.x - A.x));
                    r = Math.Sqrt((x0 - A.x) * (x0 - A.x) + (y0 - A.y) * (y0 - A.y));

                    lr = Math.Sqrt((p[i].x - x0) * (p[i].x - x0) + (p[i].y - y0) * (p[i].y - y0)); //P点到外接圆圆心的距离
                    if (lr < r)  //P点在三角形ABC外接圆的内部
                    {
                        T2.Add(T1[j]);
                        T2.Add(T1[j + 1]);
                        T2.Add(T1[j + 2]);
                        T1.RemoveAt(j + 2);
                        T1.RemoveAt(j + 1);
                        T1.RemoveAt(j);
                        j -= 3;
                    }
                }//遍历T1中全部三角形

                //三角形用3点排列变为3边排列
                for (int k = 0; k < T2.Count; k++)
                {
                    Side si;
                    if (k % 3 != 2)
                    {
                        si = new Side(T2[k], T2[k + 1]);
                    }
                    else
                    {
                        si = new Side(T2[k], T2[k - 2]);
                    }
                    S.Add(si);
                }

                //删除S中的公共边
                for (int k = 0; k < S.Count - 1; k++)
                {
                    int sss = 0;
                    for (int m = k + 1; m < S.Count; m++)
                    {
                        if (S[m] == S[k])
                        {
                            sss = 1;
                            S.RemoveAt(m);
                            m--;
                        }
                    }
                    if (sss == 1)
                    {
                        S.RemoveAt(k);
                        k--;
                    }
                }

                T2.Clear();

                for (int k = 0; k < S.Count; k++)
                {
                    T1.Add(S[k].p1);
                    T1.Add(S[k].p2);
                    T1.Add(p[i]);
                }
                S.Clear();
            }//遍历所有离散点
            return T1;
        }

        /// <summary>
        /// 构成不规则三角网
        /// </summary>
        /// <param name="T1"></param>
        /// <param name="Matrix"></param>
        /// <returns></returns>
        private List<Tpoint> finalTrinet(List<Tpoint> T1, Tpoint[] Matrix)
        {
            for (int i = 0; i < T1.Count; i += 3)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (T1[i] == Matrix[j] || T1[i + 1] == Matrix[j] || T1[i + 2] == Matrix[j])
                    {
                        T1.RemoveRange(i, 3);
                        i -= 3;
                        break;
                    }
                }
            }
            return T1;
        }

        /// <summary>
        /// 设置参考点高程
        /// </summary>
        /// <param name="hr"></param>
        /// <returns></returns>
        public bool SetHstart(double hr)
        {
            this.hr = hr;
            return true;
        }

        /// <summary>
        /// 获取等高线
        /// </summary>
        public List<PointF> GetContourLine()
        {
            List<PointF> contourLine = new List<PointF>();
            for (int i = 0; i < Net.Count; i++)
            {
                if (Net[i].S1.Incut.Num != -1)
                {
                    PointF incut = new PointF { x = Net[i].S1.Incut.x, y = Net[i].S1.Incut.y };
                    contourLine.Add(incut);
                }
                if (Net[i].S2.Incut.Num != -1)
                {
                    PointF incut = new PointF { x = Net[i].S2.Incut.x, y = Net[i].S2.Incut.y };
                    contourLine.Add(incut);
                }
                if (Net[i].S3.Incut.Num != -1)
                {
                    PointF incut = new PointF { x = Net[i].S3.Incut.x, y = Net[i].S3.Incut.y };
                    contourLine.Add(incut);
                }
            }
            return contourLine;
        }

        /// <summary>
        /// 平衡高程计算
        /// </summary>
        /// <returns></returns>
        private double SetH0()
        {
            double H = 0.0;
            double S = 0.0;
            for (int i = 0; i < Net.Count; i++)
            {
                H += Net[i].Hage * Net[i].Area;
                S += Net[i].Area;
            }
            H = H / S;
            return H;
        }

        /// <summary>
        /// 计算三角网，获得三角网点集
        /// </summary>
        /// <returns></returns>
        public bool CalTin()
        {         
            Tpoint[] Matrix = Build_Matrix(tpoints);
            List<Tpoint> T1 = BuildInitialTrinet(Matrix);
            T1 = BuildTrinet(tpoints, T1);
            T1 = finalTrinet(T1, Matrix);
            listp = T1;
            return true;
        }

        /// <summary>
        /// 获得三角网三角形集
        /// </summary>
        /// <returns></returns>
        private List<Triangle> SetNet()
        {
            List<Triangle> Tris = new List<Triangle>();
            for (int i = 0; i < TinNetP.Count; i += 3)
            {
                Triangle tri = new Triangle(TinNetP[i], TinNetP[i + 1], TinNetP[i + 2], hr);
                Tris.Add(tri);
            }
            return Tris;
        }

        public static double Max(double a,double b)
        {
            return (a >= b) ? a : b;
        }

        public static double Max(double a,double b,double c)
        {
            return (Max(a, b) > c) ? Max(a, b) : c;
        }

        public static double Min(double a, double b, double c)
        {
            return (Min(a, b) < c) ? Min(a, b) : c;
        }

        public static double Min(double a, double b)
        {
            return (a >= b) ? b : a;
        }
        #endregion
    }
}
