using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TIN
{
    internal class Triangle
    {
        private double hr;

        public Tpoint p1;
        public Tpoint p2;
        public Tpoint p3;

        public Side S1
        {
            get { return new Side(p1, p2, hr); }
        }
        public Side S2
        {
            get { return new Side(p2, p3, hr); }
        }
        public Side S3
        {
            get { return new Side(p3, p1, hr); }
        }

        private double dh1
        {
            get { return p1.h - hr; }
        }
        private double dh2
        {
            get { return p2.h - hr; }
        }
        private double dh3
        {
            get { return p3.h - hr; }
        }

        /// <summary>
        /// 平均高度
        /// </summary>
        public double Hage
        {
            get { return (p1.h + p2.h + p3.h) / 3; }
        }

        /// <summary>
        /// 三角形面积
        /// </summary>
        public double Area
        {
            get { return Math.Abs((p2.x - p1.x) * (p3.y - p1.y) - (p3.x - p1.x) * (p2.y - p1.y)) / 2; }
        }

        public Triangle(Tpoint p1,Tpoint p2,Tpoint p3)
        {
            this.p1 = p1;
            this.p2 = p2;
            this.p3 = p3;
        }
        public Triangle(Tpoint p1, Tpoint p2, Tpoint p3,double hr)
        {
            this.p1 = p1;
            this.p2 = p2;
            this.p3 = p3;
            this.hr = hr;
            SetV();
        }

        ///// <summary>
        ///// 设置参考面高程值
        ///// </summary>
        ///// <param name="h"></param>
        //public void SetHr(double h)
        //{
        //    hr = h;
        //    SetV();  //算出填方挖方值
        //}

        /// <summary>
        /// 挖方体积（+）
        /// </summary>
        public double V_cut
        {
            get;
            set;
        }
        /// <summary>
        /// 填方体积（-）
        /// </summary>
        public double V_fill
        {
            get;
            set;
        }

        private bool SetV()
        {
            //全挖或全填
            if (hr <= TIN.Min(p1.h, p2.h, p3.h) || hr >= TIN.Max(p1.h, p2.h, p3.h))
            {
                if(hr <= TIN.Min(p1.h, p2.h, p3.h)) //全挖
                {
                    V_cut = Area * (Hage - hr);
                    V_fill = 0;
                }
                else //全填
                {
                    V_fill= Area * (Hage - hr);
                    V_cut = 0;
                }
            }
            else
            {
                if(S1.Incut.Num==-1)
                {
                    if (dh3 >= 0)
                    {
                        double Area1 = Math.Abs((S2.Incut.x - p3.x) * (S3.Incut.y - p3.y) - (S3.Incut.x - p3.x) * (S2.Incut.y - p3.y)) / 2;
                        V_cut = Area1 * ((p3.h + hr + hr) / 3 - hr);
                        V_fill = (Area - Area1) * ((hr + hr + p1.h + p2.h) / 4 - hr);
                    }
                    else
                    {
                        double Area1 = Math.Abs((S2.Incut.x - p3.x) * (S3.Incut.y - p3.y) - (S3.Incut.x - p3.x) * (S2.Incut.y - p3.y)) / 2;
                        V_fill = Area1 * ((p3.h + hr + hr) / 3 - hr);
                        V_cut = (Area - Area1) * ((hr + hr + p1.h + p2.h) / 4 - hr);
                    }
                }
                else if(S2.Incut.Num==-1)
                {
                    if(dh1>=0)
                    {
                        double Area1 = Math.Abs((S1.Incut.x - p1.x) * (S3.Incut.y - p1.y) - (S3.Incut.x - p1.x) * (S1.Incut.y - p1.y)) / 2;
                        V_cut = Area1 * ((p1.h + hr + hr) / 3 - hr);
                        V_fill = (Area - Area1) * ((hr + hr + p2.h + p3.h) / 4 - hr);
                    }
                    else
                    {
                        double Area1 = Math.Abs((S1.Incut.x - p1.x) * (S3.Incut.y - p1.y) - (S3.Incut.x - p1.x) * (S1.Incut.y - p1.y)) / 2;
                        V_fill = Area1 * ((p1.h + hr + hr) / 3 - hr);
                        V_cut = (Area - Area1) * ((hr + hr + p2.h + p3.h) / 4 - hr);
                    }
                }
                else if (S3.Incut.Num == -1)
                {
                    if (dh2 >= 0)
                    {
                        double Area1 = Math.Abs((S1.Incut.x - p2.x) * (S2.Incut.y - p2.y) - (S2.Incut.x - p2.x) * (S1.Incut.y - p2.y)) / 2;
                        V_cut = Area1 * ((p2.h + hr + hr) / 3 - hr);
                        V_fill = (Area - Area1) * ((hr + hr + p1.h + p3.h) / 4 - hr);
                    }
                    else
                    {
                        double Area1 = Math.Abs((S1.Incut.x - p2.x) * (S2.Incut.y - p2.y) - (S2.Incut.x - p2.x) * (S1.Incut.y - p2.y)) / 2;
                        V_fill = Area1 * ((p2.h + hr + hr) / 3 - hr);
                        V_cut = (Area - Area1) * ((hr + hr + p1.h + p3.h) / 4 - hr);
                    }
                }
            }
            return true;
        }
    }
}
