using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 基于hausdorff_Frechet距离的线匹配算法
{
    class CREAT_REPORT
    {       
        public List<List<double>> Hausdorff(shpfile shp1,shpfile shp2)            //求出两个.shp文件的各个线实体的hausdroof距离
        {
            List<List<double>> hausfroff_D = new List<List<double>>();
            for (int i = 0; i < shp1.line.Count; i++)
            {
                List<double> Haus_D = new List<double>();
                for (int j = 0; j < shp2.line.Count; j++)
                {
                    Haus_D.Add(Math.Max(H_line_D(shp1.line[i], shp2.line[j]), H_line_D( shp2.line[j],shp1.line[i]))); 
                }
                hausfroff_D.Add(Haus_D);
            }
            return hausfroff_D;
        }
        private double H_line_D(shpline line1,shpline line2)          //线line1到line2的hausdorff距离
        {
            double shp1_linei_D_max = 0;
                   for (int ii = 0; ii < line1.numpoints; ii++)
                  {
                      double pi_min = D(line1.ponits[ii], line2.ponits[0]);
                        for (int jj = 0; jj < line2.numpoints; jj++)
                            {
                                double d = D(line1.ponits[ii], line2.ponits[jj]);
                                pi_min = pi_min > d ? d : pi_min;
                            }
                            shp1_linei_D_max = shp1_linei_D_max < pi_min ? pi_min : shp1_linei_D_max;
                   }
            return shp1_linei_D_max;                                
        }
        private double D(PointD p0,PointD p1)//两点之间的欧式距离
        {
            double d=Math.Sqrt((p0.X-p1.X)* (p0.X - p1.X)+ (p0.Y - p1.Y)* (p0.Y - p1.Y));
            return d;
        }

        public List<List<double>> improve_Hausdorff(shpfile shp1, shpfile shp2)          //基于改进的Hausdorff_SMHD距离
        {
            List<List<double>> im_Hausdorff_D = new List<List<double>>();
            for (int i = 0; i < shp1.line.Count; i++)
            {
                List<double> Haus_D = new List<double>();
                for (int j = 0; j < shp2.line.Count; j++)
                {
                    Haus_D.Add(im_H_Line_D(shp1.line[i], shp2.line[j]));
                }
                im_Hausdorff_D.Add(Haus_D);
            }
            return im_Hausdorff_D;
        }
        private double im_H_Line_D(shpline line1,shpline line2)           //计算两条线实体之间改进的hausdorff距离
        {
            double h_Line_D = 0;
            PointD p = new PointD();
            List<double> line1_D = im_H_line_length(line1);
            List<double> line2_D = im_H_line_length(line2);
            List<double> Line_mid_D = new List<double>();                   //中点到另一条线实体的各线段的的距离
              if (line1_D.Last() <= line2_D.Last())
            {
                p = line_midpoint(line1, line1_D);
                for (int i = 0; i < line2.numpoints -1; i++)
                {
                    Line_mid_D .Add(perpendicular_length(p, line2.ponits[i],line2.ponits[i + 1]));
                }
                h_Line_D = Line_mid_D.Min();
            }
            else
            {
                p = line_midpoint(line2, line2_D);
                for (int i = 0; i < line1.numpoints - 1; i++)
                {
                    Line_mid_D.Add(perpendicular_length(p, line1.ponits[i], line1.ponits[i + 1]));
                }
                h_Line_D = Line_mid_D.Min();
            }            
            return h_Line_D;
        }
        private List<double> im_H_line_length(shpline line)                   //计算一条线实体的长度
        {
            List<double> length_D = new List<double> ();
            length_D.Add(0.0);
            for (int i = 0; i < line.numpoints - 1; i++)
            {
                length_D.Add(D(line.ponits[0], line.ponits[1])+length_D.Last());
            }
            return length_D;
        }
        private PointD line_midpoint(shpline line, List<double> line_D)         //求线实体的中点
        {
            PointD p = new PointD();
            double D_mid = line_D.Last() / 2.0;
            for (int i = 0; i < line_D.Count-1; i++)
            {
                if (D_mid == line_D[i])
                {
                    p = line.ponits[i];
                    return p;
                }
                else if (D_mid == line_D[i + 1])
                {
                    p = line.ponits[i+1];
                    return p;
                }
                else if (D_mid > line_D[i] && D_mid < line_D[i + 1])
                {
                    p = mid_piont(line.ponits[i], line.ponits[i + 1], D_mid - line_D[i], line_D[i + 1] - line_D[i]);
                    return p;
                }    
            }
            return p; 
        }
        private PointD mid_piont(PointD p1, PointD p2, double D1,double D2)        //     
        {
            PointD p = new PointD();
            p.X = (D1 / D2) * (p2.X - p1.X)+p1.X ;
            p.Y = (D1 / D2) * (p2.Y - p1.Y)+p1.Y;
            return p;
        }
        private double perpendicular_length(PointD p0, PointD p1, PointD p2) //计算点p0到线(p1_p2)的垂线距离
        {            
            if (p1.Y == p2.Y)                                                           //线(p1_p2)水平时
            {                                                                           
                if (p0.X <= Math.Max(p1.X, p2.X) && p0.X >= Math.Min(p1.X, p2.X))
                {
                    return Math.Abs(p1.Y - p0.Y);
                }
                else {
                    return Math.Min( D(p0, p1), D(p0, p2));
                }
            }
            if (p1.X == p2.X)                                                              //线(p1_p2)垂直时
            {
                if (p0.Y <= Math.Max(p1.Y, p2.Y) && p0.Y >= Math.Min(p1.Y, p2.Y))
                {
                    return Math.Abs(p1.X - p0.X);
                }
                else {
                    return Math.Min(D(p0, p1), D(p0, p2));
                }
            }
            PointD pc = new PointD();
            {//求垂足坐标    pointD pc                
                double k = ((p0.X - p1.X) * (p2.X - p1.X) + (p0.Y - p1.Y) * (p2.Y - p1.Y)) / ((p2.X - p1.X) * (p2.X - p1.X) + (p2.Y - p1.Y) * (p2.Y - p1.Y));
                pc.X = p1.X + k * (p2.X - p1.X);
                pc.Y = p1.Y + k * (p2.Y - p1.Y);
            }
            if ((D(p1, pc) <= D(p1, p2))&& D(p2, pc) <= D(p1, p2))
            {
                return D(p0, pc);
            }
            else {
                return Math.Min(D(p0, p1), D(p0, p2));
            }
        }
        public List<List<double>> Feachet(shpfile shp1, shpfile shp2)//求出两个.shp文件的各个线实体的离散Frechet距离
        {
            List<List<double>> Feachet_D = new List<List<double>>();
            for (int i = 0; i < shp1.line.Count; i++)
            {
                List<double> fc_d = new List<double>();
                for (int j = 0; j < shp2.line.Count; j++)
                {
                    fc_d.Add(Feachet_line(shp1.line[i], shp2.line[j]));                    
                }
                Feachet_D.Add(fc_d);
            }
            return  Feachet_D;
        }
        private double Feachet_line(shpline line1,shpline line2)//线line1到line2的frechet距离
        {
            double line1_line2_D ;
            double[,] p_p_fc_D = new double[line1.numpoints, line2.numpoints];
            for (int i = 0; i < line1.numpoints; i++)
            {
                for (int j = 0; j < line2.numpoints; j++)
                {
                    p_p_fc_D[i, j] = -1.0;
                }
            }
            line1_line2_D = P_P_D(line1, line2, p_p_fc_D, line1.numpoints - 1, line2.numpoints - 1);
            return line1_line2_D;
        }
        private double P_P_D(shpline line1, shpline line2, double[,] p_p_fc_D, int i, int j)       //利用递归求frechet距离
        {
            if (p_p_fc_D[i, j] > -1)
            { return p_p_fc_D[i, j]; }
            else if (i == 0 && j == 0)
            { p_p_fc_D[i, j] = D(line1.ponits[i], line2.ponits[j]); }
            else if (i > 0 && j == 0)
            {
                p_p_fc_D[i, j] = Math.Max(P_P_D(line1, line2, p_p_fc_D, i - 1, j),
                  D(line1.ponits[i], line2.ponits[0]));
            }
            else if (i == 0 && j > 0)
            {
                p_p_fc_D[i, j] = Math.Max(P_P_D(line1, line2, p_p_fc_D, i, j - 1),
                    D(line1.ponits[0], line2.ponits[j]));
            }
            else if (i > 0 && j > 0)
            {
                p_p_fc_D[i, j] = Math.Max(Math.Min(Math.Min(P_P_D(line1, line2, p_p_fc_D, i - 1, j), P_P_D(line1, line2, p_p_fc_D, i - 1, j - 1))
                                  , P_P_D(line1, line2, p_p_fc_D, i, j - 1)
                                  ), D(line1.ponits[i], line2.ponits[j]));
            }
            else { p_p_fc_D[i, j] = -1; }
            return p_p_fc_D[i, j];
        }

        public List<List<double>> aver_Feachet(shpfile shp1, shpfile shp2)//求出两个.shp文件的各个线实体的平均Frechet距离
        {           
            List<List<double>> Feachet_D = new List<List<double>>();
            for (int i = 0; i < shp1.line.Count; i++)
            {
                List<double> fc_d = new List<double>();
                for (int j = 0; j < shp2.line.Count; j++)
                {
                    fc_d.Add(aver_Feachet_line(shp1.line[i], shp2.line[j]));
                }
                Feachet_D.Add(fc_d);
            }
            return Feachet_D;
        }
        private double aver_Feachet_line(shpline line1, shpline line2)        //线line1到line2的平均frechet距离
        {
            double[,,] aver_p_p_fc_D = new double[line1.numpoints, line2.numpoints,2];
            for (int i = 0; i < line1.numpoints; i++)
            {
                for (int j = 0; j < line2.numpoints; j++)
                {
                    aver_p_p_fc_D[i, j,0] = -1.0;
                    aver_p_p_fc_D[i, j, 1] = D(line1.ponits[i], line2.ponits[j]);
                }
            }
            aver_P_P_D(line1, line2, aver_p_p_fc_D, line1.numpoints - 1, line2.numpoints - 1);
            List<point_int> p_int = new List<point_int>();
            min_path(aver_p_p_fc_D, line1.numpoints - 1, line2.numpoints - 1,p_int);
            double sum = 0.0;
            for (int i = 0; i < p_int.Count(); i++)
            {
                sum=sum+aver_p_p_fc_D[p_int[i].x, p_int[i].y, 1];
            }      
            return sum / p_int.Count();
        }
        private double aver_P_P_D(shpline line1, shpline line2, double[,,] p_p_fc_D, int i, int j)       //利用递归求frechet距离
        {
            if (p_p_fc_D[i, j,0] > -1)
            { return p_p_fc_D[i, j,0]; }
            else if (i == 0 && j == 0)
            { p_p_fc_D[i, j,0] = p_p_fc_D[i, j, 1]; }
            else if (i > 0 && j == 0)
            {
                p_p_fc_D[i, j,0] = Math.Max(aver_P_P_D(line1, line2, p_p_fc_D, i - 1, j),
                 p_p_fc_D[i, 0, 1]);
            }
            else if (i == 0 && j > 0)
            {
                p_p_fc_D[i, j,0] = Math.Max(aver_P_P_D(line1, line2, p_p_fc_D, i, j - 1),
                   p_p_fc_D[0, j, 1]);
            }
            else if (i > 0 && j > 0)
            {
                p_p_fc_D[i, j,0] = Math.Max(Math.Min(Math.Min(aver_P_P_D(line1, line2, p_p_fc_D, i - 1, j), aver_P_P_D(line1, line2, p_p_fc_D, i - 1, j - 1))
                                  , aver_P_P_D(line1, line2, p_p_fc_D, i, j - 1)
                                  ), p_p_fc_D[i, j, 1]);
            }
            else { p_p_fc_D[i, j,0] = -1; }
            return p_p_fc_D[i, j,0];
        }
        private void min_path(double[,,] aver_p_p_fc_D, int i, int j, List<point_int> p_int)             //递归寻找最短路径
        {
            p_int.Add(new point_int(i, j));
            if (i == 0 && j == 0)
            {
                return;
            }
            point_int p = new point_int(i-1,j-1);
            point_int p_min = min_point_int(aver_p_p_fc_D, new point_int(i - 1,j), new point_int(i,j-1));
            if (p.x == min_point_int(aver_p_p_fc_D, p, p_min).x && p.y == min_point_int(aver_p_p_fc_D, p, p_min).y)
            {
                min_path(aver_p_p_fc_D, p.x, p.y, p_int);
            }
            else
            {
                min_path(aver_p_p_fc_D, p_min.x, p_min.y, p_int);
            }

        }
        private point_int min_point_int(double[,,] aver_p_p_fc_D, point_int p1, point_int p2)       //(a,b)<=(c,d)的比较规则
        {
            if (p1.x < 0||p1.y<0)
            { return p2; }
            if (p2.y < 0)
            { return p1; }
            if (aver_p_p_fc_D[p1.x, p1.y, 0] < aver_p_p_fc_D[p2.x, p2.y, 0])
            {
                return p1;
            }
            else if(aver_p_p_fc_D[p1.x, p1.y, 0]==aver_p_p_fc_D[p2.x, p2.y, 0]&& aver_p_p_fc_D[p1.x, p1.y, 1] < aver_p_p_fc_D[p2.x, p2.y,1])
            {
                return p1;
            }
            return p2;
        }
        private class point_int
        {
            public int x;
            public int y;
            public point_int()
            { }
            public point_int(int i, int j)
            {
                x = i;
                y = j;
            }
        }

    }
}
