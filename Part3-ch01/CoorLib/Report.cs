using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoorLib
{
    /// <summary>
    /// 产生计算报告
    /// </summary>
    public class Report
    {
        public ObsData Obs;
        public Report(ObsData data)
        {
            Obs = data;
        }
        public string WriteReport()
        {
            string blh2xyz = BLH2XYZ();
            string bl2xy = BL2xy();

            string report = Obs.ToString() + "\r\n";
            report += blh2xyz + "\r\n" + XYZ2BLH() + "\r\n";
            report += bl2xy + "\r\n";// + BL2xy2() + "\n";
            report += xy2BL() + "\r\n";// + xy2BL2();

            return report;
        }
        string BLH2XYZ()
        {
            string res = string.Format("\r\n大地坐标（BLH）转换为空间坐标（XYZ）\r\n");
            res += "--------------------------------------\n";
            res += string.Format("{0,-5}{1,10}{2,15}{3,8:f4}",
                "点名", "B", "L", "  H");
            res += string.Format("{0,15:f4} {1,15:f4}{2,15:f4}\r\n", "X", "Y", "Z");

            Position pos = new Position(Obs.Datum);
            double X, Y, Z;
            for (int i = 0; i < Obs.Data.Count; i++)
            {
                double B = Obs.Data[i].B;
                double L = Obs.Data[i].L;
                double H = Obs.Data[i].H;
                pos.GeodeticToCartesian(B, L, H, out X, out Y, out Z);
                Obs.Data[i].X = X;
                Obs.Data[i].Y = Y;
                Obs.Data[i].Z = Z;


                res += string.Format("{0,-5}{1,15}{2,15}{3,10:f4}",
                    Obs.Data[i].Name, GeoPro.Rad2Str(B), GeoPro.Rad2Str(L), H);
                res += string.Format("{0,15:f4}{1,15:f4}{2,15:f4}\r\n",
                     X, Y, Z);
            }
            return res;
        }
        string XYZ2BLH()
        {
            string res = string.Format("\r\n空间坐标（XYZ）转换为大地坐标（BLH）\r\n");
            res += "--------------------------------------\r\n";
            res += string.Format("{0,-5}{1,15:f4}{2,15:f4}{3,15:f4}",
                "点名", "X", "Y", "Z");
            res += string.Format("{0,15}{1,15}{2,8:f4}\r\n", "B", "L", "   H");
            Position pos = new Position(Obs.Datum);
            double B, L, H;
            for (int i = 0; i < Obs.Data.Count; i++)
            {
                double X = Obs.Data[i].X + 1000.0;//+1000
                double Y = Obs.Data[i].Y + 1000.0;//+1000
                double Z = Obs.Data[i].Z + 1000.0;//+1000
                pos.CartesianToGeodetic(X, Y, Z, out B, out L, out H);


                res += string.Format("{0,-5}{1,15:f4}{2,15:f4}{3,15:f4}",
                     Obs.Data[i].Name, X, Y, Z);
                res += string.Format("{0,15}{1,15}{2,10:f4}\r\n", GeoPro.Rad2Str(B), GeoPro.Rad2Str(L), H);
            }
            return res;
        }
        string BL2xy()
        {
            string res = string.Format("\r\n高斯正算（BL-->xy）\r\n");
            res += "--------------------------------------\r\n";
            res += string.Format("{0,-5} {1,12} {2,12}",
                "点名", "B", "L");
            res += string.Format(" {0,10:f4} {1,10:f4} \r\n", "x", "y");
            Gauss pos = new Gauss(Obs.Datum, Obs.L0);
            double x, y;
            for (int i = 0; i < Obs.Data.Count; i++)
            {
                double B = Obs.Data[i].B;
                double L = Obs.Data[i].L;
                pos.BL2xy(B, L, out x, out y);
                Obs.Data[i].x = x;
                Obs.Data[i].y = y;


                res += string.Format("{0,-5} {1,12} {2,12}",
                    Obs.Data[i].Name, GeoPro.Rad2Str(B), GeoPro.Rad2Str(L));
                res += string.Format(" {0,10:f4} {1,10:f4}\r\n", x, y);
            }
            return res;
        }

  
        

        string xy2BL()
        {
            string res = string.Format("\r\n高斯反算（xy-->BL）\r\n");
            res += "--------------------------------------\r\n";
            res += string.Format("{0,-5} {1,10:f4} {2,10:f4}",
                "点名", "x", "y");
            res += string.Format(" {0,12} {1,12}\r\n", "B", "L");
            Gauss pos = new Gauss(Obs.Datum, Obs.L0);
            double B, L;
            for (int i = 0; i < Obs.Data.Count; i++)
            {
                Obs.Data[i].B = 0; Obs.Data[i].L = 0;
                double x = Obs.Data[i].x +500.0;  //+500
                double y = Obs.Data[i].y +500.0;  //+500
                pos.xy2BL(x, y, out B, out L);

                res += string.Format("{0,-5} {1,10:f4} {2,10:f4}", Obs.Data[i].Name, x, y);
                res += string.Format(" {0,15}  {1,15}\r\n ", GeoPro.Rad2Str(B), GeoPro.Rad2Str(L));
            }
            return res;
        }
   

    }
}
