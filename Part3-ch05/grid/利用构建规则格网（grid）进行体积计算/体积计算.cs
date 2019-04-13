using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 利用构建规则格网_grid_进行体积计算
{
    
    class 体积计算
    {
       public class grid_point
       {
       public float x, y,h;
       public int i;        //判断在不在凸包内（1是在，0,2是不在）
        
       }

        List<grid_point> grid_p = new List<grid_point>();
        public float[,] out_matrix(float[,] minmax, float grid_spacing)     //输出外包矩形的四角坐标
        {
            float[,] out_m = new float[4, 2];                        //得到外包矩形的四角坐标
            out_m[0, 0] = minmax[0, 0];
            out_m[0, 1] = minmax[0, 1];
            out_m[1, 0] = minmax[0, 0];
            out_m[1, 1] = minmax[1, 1];
            out_m[2, 0] = minmax[1, 0];
            out_m[2, 1] = minmax[1, 1];
            out_m[3, 0] = minmax[1, 0];
            out_m[3, 1] = minmax[0, 1];
            double m = (minmax[1, 0] - minmax[0, 0]) / grid_spacing,n= (minmax[1, 1] - minmax[0, 1]) / grid_spacing;
            for (int i = 0; i < (minmax[1, 0] - minmax[0, 0]) / grid_spacing; i++)     //得到每个格网中心点的坐标
            {

                for (int j = 0; j < (minmax[1, 1] - minmax[0, 1]) / grid_spacing; j++)
                {
                    grid_point p = new grid_point();
                    p.x = minmax[0, 0]+ i * grid_spacing + grid_spacing / 2;
                    p.y = minmax[0, 1]+j * grid_spacing + grid_spacing / 2;
                    p.i = 0;
                    grid_p.Add(p);                    
                }
            }
            return out_m;
        }
        public int get_in_gird_num(List<given_point> convex_hull)        //输出有多少格网点在凸包内
        {
            int n = convex_hull.Count();
            int m = grid_p.Count();
            int num = 0;
            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n-1; j++)                            
                {
                    if (grid_p [i].y<Math .Max (convex_hull[j].Y, convex_hull[j + 1].Y)&&grid_p[i].y >= Math.Min (convex_hull[j].Y, convex_hull[j + 1].Y))
                   { grid_p[i].i = grid_p[i].i + judge(grid_p[i], convex_hull[j], convex_hull[j + 1]); }
            
                }
                if (grid_p[i].i == 1) num++;
            }
            return num;
        }
        int judge(grid_point a, given_point b, given_point c)       //判断点a是否在b—c线的左边
        {
            double m = (c.X - b.X) / (c.Y - b.Y) * (a.y - b.Y) + b.X;
            if (m > a.x) return 1;
            return 0;
        }
        public double get_V(double r, float grid_spacing, float height_datum, List<given_point> given_point)     //得到总的体积 
        {
            double V = 0;
            for (int i = 0; i < grid_p.Count(); i++)                              //循环累加每个在凸包内的体积
            {
                if (grid_p[i].i == 1)
                {
                    double H_sum =                        
                        get_point_h(r, given_point, grid_p[i].x - grid_spacing / 2, grid_p[i].y - grid_spacing / 2) +
                        get_point_h(r, given_point, grid_p[i].x - grid_spacing / 2, grid_p[i].y + grid_spacing / 2) +
                        get_point_h(r, given_point, grid_p[i].x + grid_spacing / 2, grid_p[i].y + grid_spacing / 2) +
                        get_point_h(r, given_point, grid_p[i].x + grid_spacing / 2, grid_p[i].y - grid_spacing / 2);
                    V = V + (H_sum / 4 - height_datum) * grid_spacing * grid_spacing;
                }
            }
            return V;
        }
        double get_point_h(double r, List<given_point> given_point,float X,float Y)     //得到该点的高程
        {
            double H = 0,Di_sum=0;
            for (int i = 0; i < given_point.Count(); i++)
            {
                double D = Math .Sqrt ((given_point[i].X - X) * (given_point[i].X - X) + (given_point[i].Y - Y) * (given_point[i].Y - Y));
                if (D <= r)
                {
                    H += given_point[i].H / D;
                    Di_sum += 1 / D;
                }
            }
            return H/Di_sum ;
        }
    }
}
