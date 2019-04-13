using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace MapIcon
{
    //计算参数——DXF
    class CalParameter_DXFcs
    {
        public double minx, miny, maxx, maxy;
        public double d;

        //计算比例系数
        public void CalRatio(List<double> x, List<double> y)
        {
            minx = x.Min();
            miny = y.Min();
            maxx = x.Max();
            maxy = y.Max();
            double t1, t2;
            t1 = (maxx - minx) / 100;
            t2 = (maxy - miny) / 100;
            if (t1 > t2)
            {
                d = t1;
            }
            else
            {
                d = t2;
            }
        }
    }
}
