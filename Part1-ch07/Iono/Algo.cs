using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Iono
{
    class Algo
    {
        DataEntity data;

        public Algo(DataEntity data)
        {
            this.data = data;
        }

        public string Compute()
        {
            double[] alpha = new double[]
            {0.1397E-07, -0.7451E-08, -0.5960E-07, 0.1192E-06};
            double[] beta = new double[]
            { 0.1270E+06, -0.1966E+06, 0.6554E+05, 0.2621E+06 };

            var model = new IonoModel(alpha, beta);
            string line = model.ToString() + "\r\n";
            var wgs84 = new WGS84Ellipsoid();
            var rx = new Position(-2225669.7744, 4998936.1598, 3265908.9678, CoordinateSystem.Cartesian, wgs84);
            line += string.Format("Position(XYZ):{0}", rx.ToString(CoordinateSystem.Cartesian)) + "\r\n";
            line += string.Format("Position(BLH):{0}", rx.ToString(CoordinateSystem.Geodetic)) + "\r\n";

            var time = data.Time;
            line += string.Format("SV     EL(°)      AZ(°)    L1（m）    L2（m）  \r\n");

            foreach (var d in data.Data)
            {
                var sv=new Position(d.X,d.Y,d.Z,CoordinateSystem.Cartesian,wgs84);
                double svel = rx.Elevation(sv);
                double svaz = rx.Azimuth(sv);
                double c1 = model.GetCorrectionL1(time, rx, svel, svaz);
                double c2 = model.GetCorrectionL2(time, rx, svel, svaz);
                if (svel < 0)
                {
                    c1 = 0;
                    c2 = 0;
                }
                line += string.Format("{0}  {1,10:f3} {2,10:f3} {3,8:f4} {4,8:f4}",
                    d.Id, svel, svaz, c1, c2) + "\r\n";
            }

            return line;
        }
    }
}
