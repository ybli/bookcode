using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trop
{
    class Algo
    {
        private DataEntity Data;

        public Algo(DataEntity data)
        {
            Data = data;
        }

        public string Compute()
        {
            string line = "测站名   高度角    ZHD      m_d(E)    ZWD      m_w(E)   延迟改正\n";
            double T = 20, P = 1001, H = 45.0;
            foreach (var d in Data.Data)
            {
                var time = d.Time;
                double ele = d.Elv;

                var rx = new Position(d.Lat, d.Lon, d.H, CoordinateSystem.Geodetic);

                var model = new NeillTropModel(rx, time);
                model.SetWeather(T, P, H);
                line += $"{d.Id}      {d.Elv:F2}    {model.DryZenithDelay():F3}     {model.DryMappingFunction(d.Elv):f3} ";
                line += $"   {model.WetZenithDelay():F3}    {model.WetMappingFunction(d.Elv):F3}  ";
                line += $"  {model.Correction(ele):f3} \n";
            }



            return line;
        }
        private string Compute(DataEntity Data)
        {
            var time = new Time(2014, 1, 20, 14, 56, 10.0);
            double ele = 45;
            double T = 20, P = 1001, H = 45.0;
            var rx = new Position(30, 114, 30, CoordinateSystem.Geodetic);


            var model = new NeillTropModel(rx, time);
            model.SetWeather(T, P, H);
            string line = String.Format("elevation:{0:0.00}, ZHD:{1:0.0000},ZWD:{2:0.000},Correction:{3:0.000}",
               ele, model.DryZenithDelay(), model.WetZenithDelay(), model.Correction(ele));

            return line;
        }
    }
}
