using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Contour_Line
{

    //3次B样条曲线
    class Class_Spline
    {

        //生成的样条曲线坐标
        public List<double> Bspline_X = new List<double>();
        public List<double> Bspline_Y = new List<double>();


        //插值
        public void D_BSpline(List<double> x, List<double> y)
        {
            Bspline_X.Clear();
            Bspline_Y.Clear();

            int Point_NUM = x.Count;
            int Section_NUM = Point_NUM - 3;

            double f1, f2, f3, f4;

            int n = 50;

            double deltaT = 1.0 / n;
            double T;

            double px, py;

            //闭合曲线
            if (x[0] == x[x.Count - 1] && y[0] == y[y.Count - 1])
            {
                int a = x.Count - 1;

                x.Add(x[1]);
                x.Add(x[2]);
                y.Add(y[1]);
                y.Add(y[2]);

                for (int i = 0; i < a; i++)
                {
                    for (int j = 0; j <= n; j++)
                    {
                        T = j * deltaT;

                        f1 = (-T * T * T + 3 * T * T - 3 * T + 1) / 6.0;
                        f2 = (3 * T * T * T - 6 * T * T + 4) / 6.0;
                        f3 = (-3 * T * T * T + 3 * T * T + 3 * T + 1) / 6.0;
                        f4 = (T * T * T) / 6.0;

                        px = f1 * x[i] + f2 * x[i + 1] + f3 * x[i + 2] + f4 * x[i + 3];
                        py = f1 * y[i] + f2 * y[i + 1] + f3 * y[i + 2] + f4 * y[i + 3];

                        Bspline_X.Add(px);
                        Bspline_Y.Add(py);
                    }
                }
            }
            //非闭合曲线
            else
            {
                double x1, y1, x2, y2, x3, y3, x4, y4;

                x1 = x[0];
                y1 = y[0];
                x2 = x[0];
                y2 = y[0];
                x3 = x[1];
                y3 = y[1];
                x4 = x[1];
                y4 = y[1];

                int m = 10;

                double D1 = 1.0 / m;

                for (int j = 0; j <= m; j++)
                {
                    T = j * D1;

                    f1 = (-T * T * T + 3 * T * T - 3 * T + 1) / 6.0;
                    f2 = (3 * T * T * T - 6 * T * T + 4) / 6.0;
                    f3 = (-3 * T * T * T + 3 * T * T + 3 * T + 1) / 6.0;
                    f4 = (T * T * T) / 6.0;

                    px = f1 * x1 + f2 * x2 + f3 * x3 + f4 * x4;
                    py = f1 * y1 + f2 * y2 + f3 * y3 + f4 * y4;

                    Bspline_X.Add(px);
                    Bspline_Y.Add(py);
                }

                //****************************//

                for (int i = 0; i < Section_NUM; i++)
                {
                    for (int j = 0; j <= n; j++)
                    {
                        T = j * deltaT;

                        f1 = (-T * T * T + 3 * T * T - 3 * T + 1) / 6.0;
                        f2 = (3 * T * T * T - 6 * T * T + 4) / 6.0;
                        f3 = (-3 * T * T * T + 3 * T * T + 3 * T + 1) / 6.0;
                        f4 = (T * T * T) / 6.0;

                        px = f1 * x[i] + f2 * x[i + 1] + f3 * x[i + 2] + f4 * x[i + 3];
                        py = f1 * y[i] + f2 * y[i + 1] + f3 * y[i + 2] + f4 * y[i + 3];

                        Bspline_X.Add(px);
                        Bspline_Y.Add(py);
                    }
                }

                //****************************//

                x1 = x[x.Count - 3];
                y1 = y[x.Count - 3];

                x2 = x[x.Count - 2];
                y2 = y[x.Count - 2];

                x3 = x[x.Count - 1];
                y3 = y[x.Count - 1];

                x4 = x[x.Count - 1];
                y4 = y[x.Count - 1];

                for (int j = 0; j <= m; j++)
                {
                    T = j * D1;

                    f1 = (-T * T * T + 3 * T * T - 3 * T + 1) / 6.0;
                    f2 = (3 * T * T * T - 6 * T * T + 4) / 6.0;
                    f3 = (-3 * T * T * T + 3 * T * T + 3 * T + 1) / 6.0;
                    f4 = (T * T * T) / 6.0;

                    px = f1 * x1 + f2 * x2 + f3 * x3 + f4 * x4;
                    py = f1 * y1 + f2 * y2 + f3 * y3 + f4 * y4;

                    Bspline_X.Add(px);
                    Bspline_Y.Add(py);
                }
            }
        }
    }
}
