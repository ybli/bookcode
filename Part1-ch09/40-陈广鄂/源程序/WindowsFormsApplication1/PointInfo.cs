using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication1
{
   public class PointInfo
    {
        public string name;
        public double N; public double E; public double U;
        public double X; public double Y; public double Z;
        public double B; public double L; public double H;

        public PointInfo(string n,double X1,double Y1,double Z1)
        {
            name = n;
            N = E = U = 0;
            X = X1;
            Y = Y1;
            Z = Z1;
            B = L = H = 0;

        }
        public PointInfo()
        {

        }
        public void XYZ2BLH(ref List<PointInfo> point)
        {
            //准备工作
            double a = 6378137.0;
            double f = 1.0 / 298.257223563;
            double b = a - f * a;
            double e2 = (a * a - b * b) / (a * a);
            double e_2 = (a * a - b * b) / (b * b);


            for(int j=0;j<point.Count();j++)
            {
                point[j].L = Math.Atan2(point[j].Y,point[j].X);
              
                double segma = Math.Atan2(point[j].Z * a, b * Math.Sqrt(point[j].X * point[j].X + point[j].Y * point[j].Y));
                
                point[j].B = Math.Atan2(point[j].Z + e_2 * b * Math.Pow(Math.Sin(segma), 3), Math.Sqrt(point[j].X * point[j].X + point[j].Y * point[j].Y) - e2 * a * Math.Pow(Math.Cos(segma), 3));
                double N = a / (Math.Sqrt(1 - e2 * Math.Pow(Math.Sin(point[j].B), 2)));
                double B=point[j].B;
                point[j].H= Math.Sqrt(point[j].X * point[j].X + point[j].Y * point[j].Y)/Math.Cos(B)-N;
            }
        }
        public void XYZ2NEU(ref List<PointInfo> point)
        {

            for(int j=0;j<point.Count();j++)
            {
                //构造矩阵
                double[,] deta = new double[3, 1];
               
                    deta[0, 0] = point[j].X - point[0].X;
                    deta[1, 0] = point[j].Y - point[0].Y;
                    deta[2,0] = point[j].Z - point[0].Z;
                
                //构造T矩阵
                double[,] T = new double[3, 3];
                T[0, 0] = -Math.Sin(point[0].B) * Math.Cos(point[0].L);
                T[0, 1] = -Math.Sin(point[0].B) * Math.Sin(point[0].L);
                T[0, 2] = Math.Cos(point[0].B);
                T[1, 0] = -Math.Sin(point[0].L);
                T[1, 1] = Math.Cos(point[0].L);
                T[1, 2] = 0;
                T[2, 0] = Math.Cos(point[0].B) * Math.Cos(point[0].L);
                T[2, 1] = Math.Cos(point[0].B) * Math.Sin(point[0].L);
                T[2, 2] = Math.Sin(point[0].B);
                //矩阵相乘
                double[,] tmp = multiply(T,deta);

                point[j].N = tmp[0, 0];
                point[j].E = tmp[1, 0];
                point[j].U = tmp[2, 0];
            }
        }

        public double[,] multiply(double[,] a,double[,] b)
        {
            
            int m = a.GetLength(0);
            int n = a.GetLength(1);
            int m1 = b.GetLength(0);
            int n1 = b.GetLength(1);
            double[,] R=new double[m,n1];
            for(int k=0;k<m;k++)
            {
                for(int t=0;t<n1;t++)
                {
                    double sum = 0;
                    for(int q=0;q<n;q++)
                    {
                        sum += a[k, q] * b[q, t];
                    }
                    R[k, t] = sum;
                }
            }
            return R;
        }
    }
}
