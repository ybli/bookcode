using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 利用构建规则格网_grid_进行体积计算
{
    public struct point      //建立新的结构体(有关基点到各点的信息)
        {
            public given_point p;
            public double p0_pn_D;                //到各点的信息
            public double p0_pn_angle;           //到各点的角度
        }
    class 生成凸包
    {
        /*Graham'sScan*/
        List<point> g_point = new List<point>();  //定义新的列表
        point p0;//定义基点     
        public 生成凸包(List<given_point> known_point)   //构造函数   把已知点放到另一个结构体列表中
        {
            int n = known_point.Count();
            for (int i = 0; i < n; i++)
            {
                point a = new point();
                a.p = known_point[i];
                g_point.Add(a);
            }
        }
        public float[,] getminmax()         //获得已知点位中的最大最小X，Y坐标
        {
            float[,] minmax = new float[2, 2];
            minmax[0, 0] = g_point.Min(r => r.p.X);
            minmax[0, 1] = g_point.Min(r => r.p.Y);
            minmax[1, 0] = g_point.Max(r => r.p.X);
            minmax[1, 1] = g_point.Max(r => r.p.Y);
            return minmax;
        }
        point get_p0()              //得到p0基点
        {
            point p0;
            IEnumerable<point> p02 = g_point.Where(r => r.p.Y == g_point.Min(g => g.p.Y));  //寻找到最小的y的点（可能不是一个）
            point[] p01 = p02.ToArray();
            if (p01.Length != 1)
            {
                p0 = p02.Single(r => r.p.X == p02.Min(g => g.p.X));
            }
            else p0 = p01[0];
            g_point.Remove(p0);
            return p0;
        }
        double get_D(point a, point b)     //得到两点之间的距离
        {
            return Math.Sqrt((a.p.X - b.p.X) * (a.p.X - b.p.X) + (a.p.Y - b.p.Y) * (a.p.Y - b.p.Y));
        }
        double get_angle(point a, point b)      //得到两点的向量与x轴的夹角
        {
            double cos_angle;
            double Dx = b.p.X - a.p.X;
            if (Dx == 0) cos_angle = 0;
            else
            {
                cos_angle = Dx / Math.Sqrt(Dx * Dx + (b.p.Y - a.p.Y) * (b.p.Y - a.p.Y));
            }
            return cos_angle;
        }
        void  po_pn_sort()              //对向量<p0,pi>进行排序
        {
            p0 = get_p0();
            int n = g_point.Count();
            for (int i = 0; i < n; i++)                 //求出p0到pi的信息
            {
                point p0_pn = g_point[i];
                p0_pn.p0_pn_D = get_D(p0, p0_pn);
                p0_pn.p0_pn_angle = get_angle(p0, p0_pn);
                g_point[i] = p0_pn;
            }
            for (int i = 0; i < n; i++)                            //进行排序（冒泡排序）复杂度（n*n）
            {
                for (int j = i + 1; j < n; j++)
                {
                    if (g_point[i].p0_pn_angle < g_point[j].p0_pn_angle)     //角度大的放在前面
                    {
                        point p0_pn = g_point[i];
                        g_point[i] = g_point[j];
                        g_point[j] = p0_pn;
                    }
                    else if (g_point[i].p0_pn_angle == g_point[j].p0_pn_angle)//角度相等，剔除距离近的点
                    {
                        if (g_point[i].p0_pn_D < g_point[j].p0_pn_D)
                        {
                            point p0_pn = g_point[i];
                            g_point[i] = g_point[j];
                            g_point[j] = p0_pn;
                        }
                        g_point.Remove(g_point[j]);                //剔除
                    }
                }
            }           
        }
        public List <given_point > get_convex_hull()
        {
            po_pn_sort();
            Stack<point> c_h_point = new Stack<point>();     //建立栈
            c_h_point.Push(p0);
            c_h_point.Push(g_point[0]);
            c_h_point.Push(g_point[1]);
            int n = g_point.Count();
            for (int i = 2; i < n; i++)
            {
                point top = c_h_point.Pop();
                point sec_top = c_h_point.Peek();
                c_h_point.Push(top);

                if (judge_l_r(sec_top, top, g_point[i]) < 0)
                {
                    c_h_point.Push(g_point[i]);
                }
                else
                {
                    while (judge_l_r(sec_top, top, g_point[i]) > 0)
                    {
                        c_h_point.Pop();
                        top = top = c_h_point.Pop();
                        sec_top = c_h_point.Peek();
                        c_h_point.Push(top);
                    }
                    c_h_point.Push(g_point[i]);
                }
            }
            c_h_point.Push(p0);
            point[] c_h = c_h_point.ToArray();
            List<given_point> return_point = new List<given_point>();
            for (int i = c_h.Count()-1; i >= 0; i--)                       
            {
                given_point point = new given_point();
                point = c_h[i].p;
                return_point.Add(point);
            }
            return return_point;
        }
        double  judge_l_r(point a, point b, point c)      //判断左转还是右转
        {
            return (a.p.X - b.p.X) * (c.p.Y  - b.p.Y ) - (a.p.Y - b.p.Y) * (c.p.X  - b.p.X );
        }




        /*快速凸包法*/
        List<given_point> conver_h = new List<given_point>();
        List<given_point> get_four_peak(List<given_point> known_point)
        {
            //第一步找到散点的上下左右四个点（如果有重复的点就随便取一个）
            float[,] minmax = new float[2, 2];
            minmax[0, 0] = known_point.Min(r => r.X);
            minmax[0, 1] = known_point.Min(r => r.Y);
            minmax[1, 0] = known_point.Max(r => r.X);
            minmax[1, 1] = known_point.Max(r => r.Y);
            List<given_point> four_peak = new List<given_point>();

            IEnumerable<given_point> p01 = known_point.Where(r => r.X  == minmax[0, 0]);    //得到最左边的点 x最小
            given_point[] p02 = p01.ToArray();
            four_peak.Add(p02[0]);
            known_point.Remove(p02[0]);

            p01 = known_point.Where(r => r.Y == minmax[1, 1]);                  //得到最上面的点    y最大
            p02 = p01.ToArray();
            four_peak.Add(p02[0]);
            known_point.Remove(p02[0]);

            p01 = known_point.Where(r => r.X  == minmax[1, 0]);                  //得到最右面的点    x最大
            p02 = p01.ToArray();
            four_peak.Add(p02[0]);
            known_point.Remove(p02[0]);

            p01 = known_point.Where(r => r.Y == minmax[0, 1]);                  //得到最下面的点   y最小
            p02 = p01.ToArray();
            four_peak.Add(p02[0]);
            known_point.Remove(p02[0]);
            return four_peak;
        }
        public List<given_point> conver_h1(List<given_point> k_point)
        {
            List<given_point> known_point = k_point.ToList();                      //为防止把原散点列表改变，把原散点列表放到新的列表中
            List<given_point> four_peak = get_four_peak(known_point);     //得到上下左右四个顶点

            conver_h.Add(four_peak[0]);
            for (int i = 0; i < four_peak.Count; i++)                     //把散点分为5个区，得到四个点集（中间的点集不要 ）
            {               
                List<given_point> left_point = new List<given_point>();      //得到在直线左边的点集
                for (int j = 0; j < known_point.Count; j++)
                {
                    if(judge_left (four_peak[i%4],four_peak[(i+1)%4],known_point [j])==1)
                    { left_point.Add(known_point[j]);
                        known_point.Remove(known_point[j]);                    //如果确定该点在这个区域内，则肯定不在其他区域内，则可以删除列表中的该点
                        j--;
                    }        
                }
                fun(left_point, four_peak[i % 4], four_peak[(i + 1) % 4]);         //调用迭代函数，把该区域内的凸包点找出
            }
            return conver_h ;
        }
        int judge_left(given_point p1, given_point p2, given_point p3)    //判断点是否在线的左边
        {
            if (p1.X * p2.Y - p2.X * p1.Y + p3.X * (p1.Y - p2.Y) + p3.Y * (p2.X - p1.X) > 0)
            { return 1; }
            return -1;
        }
        void fun(List<given_point> left_point, given_point head, given_point tail)             //迭代函数
        {
            int arcmax_sub=-1;    //我们用三角形面积判断点的距离 ，得到最远点的序号，如果直线（头—尾）左边无点集，则把 尾 放入凸包点集中
            double arcmax = 0;
            for(int i=0;i<left_point .Count;i++ )
            {
                if (arc(head, tail, left_point[i]) > arcmax)
                { arcmax = arc(head, tail, left_point[i]);arcmax_sub = i; }
            }
            if (arcmax_sub == -1)
            { conver_h.Add(tail); }
            else
            {
                given_point mid = left_point[arcmax_sub];
                left_point.Remove(mid);
                List<given_point> left_point1 = new List<given_point>();      //得到在直线（头—最远点）左边的点集
                for (int j = 0; j < left_point.Count; j++)
                {
                    if (judge_left(head, mid, left_point[j]) == 1) { left_point1.Add(left_point[j]); }
                }
                fun(left_point1, head, mid);

                List<given_point> left_point2 = new List<given_point>();      //得到在直线（最远点—尾）左边的点集
                for (int j = 0; j < left_point.Count; j++)
                {
                    if (judge_left(mid, tail , left_point[j]) == 1) { left_point2.Add(left_point[j]); }
                }
                fun(left_point2, mid,tail);
            }


        } 
        double  arc(given_point p1, given_point p2, given_point p3)               //三点得到面积
        {    
            return Math .Abs( (1.0 / 2) * (p1.X * (p2.Y - p3.Y) + p2.X * (p3.Y - p1.Y) + p3.X * (p1.Y - p2.Y)));
        }

    }
}
