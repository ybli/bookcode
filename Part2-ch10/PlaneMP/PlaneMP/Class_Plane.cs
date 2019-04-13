using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PlaneMP
{

    //点结构
    public struct point
    {
        public string PointName;
        public double x;
        public double y;
        public double z;
    }

    /// <summary>
    /// 空间平面平整度计算类
    /// </summary>
    class Class_Plane
    {
        public int Point_SUM = 0;    //点数
        public point P1=new point();
        public point P2=new point();
        public point P3=new point();
        public double A0, B0, C0;    //平面方程式参数近似值
        public List<point> M = new List<point>();    //散点集
        public double A1, B1, C1;    //改正后的平面方程式参数
        public List<double> d = new List<double>();    //各个观测点到拟合平面的距离
        public double m0;    //单位权中误差


        //求平面方程式参数近似值
        public void PlaneThreePoint()
        {
            double A, B, C, D;

            P1 = M[0];
            P2 = M[1];
            P3 = M[2];

            A = (P1.y - P2.y) * (P1.z - P3.z) - (P1.y - P3.y) * (P1.z - P2.z);
            B = (P1.x - P3.x) * (P1.z - P2.z) - (P1.x - P2.x) * (P1.z - P3.z);
            C = (P1.x - P2.x) * (P1.y - P3.y) - (P1.x - P3.x) * (P1.y - P2.y);
            D = -P1.x * A - P1.y * B - P1.z * C;

            A0 = A / D;
            B0 = B / D;
            C0 = C / D;
        }

        //计算
        public void Calculate()
        {
            double aa = 0, ab = 0, ac = 0, bb = 0, bc = 0, cc = 0, al = 0, bl = 0, cl = 0;
            double L = 0;
            //由观测值方程式系数及常数项组成法方程式系数及常数项
            for (int i = 0; i < Point_SUM; i++)
            {
                aa += M[i].x * M[i].x;
                ab += M[i].x * M[i].y;
                ac += M[i].x * M[i].z;
                bb += M[i].y * M[i].y;
                bc += M[i].y * M[i].z;
                cc += M[i].z * M[i].z;

                L = (A0 * M[i].x) + (B0 * M[i].y) + (C0 * M[i].z) + 1;

                al += (M[i].x * L);
                bl += (M[i].y * L);
                cl += (M[i].z * L);
            }

            //计算法方程式系数阵的行列式值
            double det = 0;
            det = (aa * bb * cc) + (2 * ab * ac * bc) - (aa * bc * bc) - (bb * ac * ac) - (cc * ab * ab);

            //计算法方程式协因数阵元素(Qij)
            double Q11 = 0, Q22 = 0, Q33 = 0, Q12 = 0, Q13 = 0, Q23 = 0;

            Q11 = (bb * cc - bc * bc) / det;
            Q22 = (aa * cc - ac * ac) / det;
            Q33 = (aa * bb - ab * ab) / det;
            Q12 = (ac * bc - cc * ab) / det;
            Q13 = (ab * bc - bb * ac) / det;
            Q23 = (ab * ac - aa * bc) / det;

            //计算平面方程式参数近似值的改正值
            double dA = 0, dB = 0, dC = 0;

            dA = (Q11 * al + Q12 * bl + Q13 * cl) * -1;
            dB = (Q12 * al + Q22 * bl + Q23 * cl) * -1;
            dC = (Q13 * al + Q23 * bl + Q33 * cl) * -1;

            //计算改正后的A0,B0,C0;
            A1 = A0 + dA;
            B1 = B0 + dB;
            C1 = C0 + dC;

            //计算各点离平面的起伏(d)
            for (int i = 0; i < Point_SUM; i++)
            {
                d.Add(((A1 * M[i].x + B1 * M[i].y + C1 * M[i].z + 1) /
                       Math.Sqrt(A1 * A1 + B1 * B1 + C1 * C1)) * 1000);
            }

            //计算单位权中误差
            double dd = 0;

            for (int i = 0; i < Point_SUM; i++)
            {
                dd += d[i] * d[i];
            }

            m0 = Math.Sqrt(dd / (Point_SUM - 3));
        }

        //生成计算报告
        public string Report()
        {
            string a = "(1)平面方程式参数\n";
            a += string.Format("A={0,-15:f7}B={1,-15:f7}C={2,-15:f7}\n", A1, B1, C1);
            a += "\n";
            a += "(2)平面上的点位起伏\n";
            a += "点名      距离[MM]\n";
            for (int i = 0; i < Point_SUM; i++)
            {
                a += string.Format("{0,-10}{1,-20:f6}\n", i + 1, d[i]);
            }
            a += "\n";
            a += "(3)平面平整度\n";
            a += "M0[MM]=" + m0.ToString("f6");
            return a;
        }

        //保存DXF文件
        public void Draw_DXF(string Filename)
        {
            Class_DrawDXF dr = new Class_DrawDXF();

            dr.sw = new System.IO.StreamWriter(Filename);

            List<double> x = new List<double>();
            List<double> y = new List<double>();
            List<double> z = new List<double>();

            for (int i = 0; i < Point_SUM; i++)
            {
                x.Add(M[i].x);
                y.Add(M[i].y);
                z.Add(M[i].z);
            }

            dr.CalRatio(x, y);

            dr.ConstRange();

            //画点和点名
            for (int i = 0; i < Point_SUM; i++)
            {
                dr.D_Circle(x[i], y[i]);
                dr.D_Text(x[i], y[i], M[i].PointName);
            }

            dr.D_End();
        }
    }
}
