using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace landside
{
    public class Coordinate
    {
        /// <summary>
        /// 期数
        /// </summary>
        public double Tm { get; set; }
        /// <summary>
        /// 平面坐标X分量
        /// </summary>
        public double X { get; set; }

        /// <summary>
        /// 平面坐标Y分量
        /// </summary>
        public double Y { get; set; }

        public Coordinate()
        {
            Tm = 0;
            X = 0;
            Y = 0;
        }
        /// <summary>
        /// 将一行文本解析为坐标
        /// 1,492.1373, 973.2576
        /// </summary>
        /// <param name="text">文本</param>
        public void Parse(string text)
        {
            var buf = text.Split(',');
            Tm = Convert.ToInt32(buf[0]);
            X = Convert.ToDouble(buf[1]);
            Y = Convert.ToDouble(buf[2]);
        }

    }
}
