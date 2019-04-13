using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace XYZ2NEU
{
    class Formula
    {
        //数据读取与显示
        public void Readfile(out List<XYZPoint> Xpoint)
        {
            Xpoint = new List<XYZPoint>();

            OpenFileDialog file = new OpenFileDialog();
            file.InitialDirectory = Application.StartupPath;
            file.RestoreDirectory = true;
            file.Filter = "All Files(*.*)|*.*|Dat Files(*.dat)|*.dat|Text Files(*.txt)|*.txt";
            file.FilterIndex = 2;

            if (file.ShowDialog() == DialogResult.OK)
            {
                XYZPoint p;

                var reader = new StreamReader(file.FileName);

                string str = reader.ReadLine();
                str = reader.ReadLine();
                var arr = str.Split(' ');

                while (str != null)
                {
                    arr = str.Split(' ');
                    p = new XYZPoint();
                    p.name = arr[0];
                    p.X = double.Parse(arr[1]);
                    p.Y = double.Parse(arr[2]);
                    p.Z = double.Parse(arr[3]);
                    Xpoint.Add(p);

                    str = reader.ReadLine();
                }
                reader.Close();
            }
        }

        //XYZ转换为BLH
        public void xyz2blh(List<XYZPoint> Xpoint, out List<BLHPoint> Bpoint)
        {
            double a = 6378137.0;
            double f = 1.0 / 298.257223563;
            double b = a - f * a;

            BLHPoint pp;
            Bpoint = new List<BLHPoint>();

            foreach (XYZPoint p in Xpoint)
            {
                double L1 = Math.Atan(p.Y/p.X);
                //if (L1 < 0)
                //{
                //    L1 += 2.0 * Math.PI;
                //}

                double e2 = 2.0 * f - f * f;
                double e12 = (a * a - b * b) / b / b;
                double Sita = Math.Atan(p.Z * a / b / Math.Sqrt(p.X * p.X + p.Y * p.Y));

                double B1 = Math.Atan((p.Z + e12 * b * Math.Sin(Sita) * Math.Sin(Sita) * Math.Sin(Sita)) 
                    / (Math.Sqrt(p.X * p.X + p.Y * p.Y) - e2 * a * Math.Cos(Sita) * Math.Cos(Sita) * Math.Cos(Sita)));

                double N = a / (Math.Sqrt(1.0 - e2 * Math.Sin(B1) * Math.Sin(B1)));
                double H1 = Math.Sqrt(p.X * p.X + p.Y * p.Y) / Math.Cos(B1) - N;

                pp = new BLHPoint();
                pp.name = p.name;
                pp.B = B1;
                pp.L = L1;
                pp.H = H1;

                Bpoint.Add(pp);
            }
        }

        //XYZ经过第一点BLH转换为NEU
        public void blh2neu(List<XYZPoint> Xpoint, List<BLHPoint> Bpoint,int num, out List<NEUPoint> Npoint)
        {
            Npoint = new List<NEUPoint>();
            NEUPoint pp = new NEUPoint();

            double B1 = Bpoint[num].B;
            double L1 = Bpoint[num].L;
            double X1 = Xpoint[num].X;
            double Y1 = Xpoint[num].Y;
            double Z1 = Xpoint[num].Z;

            foreach (XYZPoint p in Xpoint)
            {
                pp = new NEUPoint(); 

                double N1 = (-Math.Sin(B1) * Math.Cos(L1)) * (p.X - X1)
                    + (-Math.Sin(B1) * Math.Sin(L1)) * (p.Y - Y1)
                    + Math.Cos(B1) * (p.Z - Z1);
                double E1 = (-Math.Sin(L1)) * (p.X - X1)
                    + Math.Cos(L1) * (p.Y - Y1)
                    + 0;
                double U1 = Math.Cos(B1)*Math.Cos(L1)*(p.X - X1)
                    + Math.Cos(B1)*Math.Sin(L1)*(p.Y - Y1)
                    + Math.Sin(B1) * (p.Z - Z1);

                pp.name = p.name;
                pp.N = N1;
                pp.E = E1;
                pp.U = U1;

                Npoint.Add(pp);
            }
        }

        //撰写报告
        public void Report(List<XYZPoint> Xpoint, List<BLHPoint> Bpoint, int num, List<NEUPoint> Npoint, out string report)
        {
            report  = null;

            report += "**************计算结果*************\n\n";
            report += "以第" + num.ToString() + "点为站心！\n";
            report += "**************空间坐标*************\n";
            foreach (XYZPoint p in Xpoint)
            {
                report += String.Format("{0,6},{1,15},{2,15},{3,15}", p.name, p.X.ToString("f5"), p.Y.ToString("f5"), p.Z.ToString("f5")) + "\n";
            }
            report += "**************大地坐标*************\n";
            foreach (BLHPoint p in Bpoint)
            {
                report += String.Format("{0,6},{1,15},{2,15},{3,15}", p.name, p.B.ToString("f5"), p.L.ToString("f5"), p.H.ToString("f5")) + "\n";
            }
            report += "**************站心坐标*************\n";
            foreach (NEUPoint p in Npoint)
            {
                report += String.Format("{0,6},{1,15},{2,15},{3,15}", p.name, p.N.ToString("f5"), p.E.ToString("f5"), p.U.ToString("f5")) + "\n";
            }
        }
    }
}
