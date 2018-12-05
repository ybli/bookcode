using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaxiData
{
    /// <summary>
    /// 一个时段的速度、方位角计算
    /// </summary>
    class Session
    {
        public int Sn;  //顺序号
        public double StartMjd, EndMjd;
        //public double Dx, Dy;
        public double Length;
        public double Velocity, Azimuth;

        public Session(Epoch start, Epoch end)
        {
            Sn = 0;
            StartMjd = start.Mjd;
            EndMjd = end.Mjd;
            GetLength(start, end);
            GetVelocity();
            GetAzimuth(start, end);
        }
        /// <summary>
        /// 方位角计算
        /// </summary>
        private void GetAzimuth(Epoch start, Epoch end)
        {
            double eps = 1e-5;
            double dx = end.x - start.x;
            double dy = end.y - start.y;
            if (Math.Abs(dx) < eps)
            {
                if (Math.Abs(dy) < eps)
                    Azimuth = 0;
                else if (dy > 0)
                    Azimuth = 0.5*Math.PI;
                else
                {
                    Azimuth = 1.5*Math.PI;
                }
            }
            else
            {
                Azimuth = Math.Atan2(dy, dx);
                if (dx < 0)
                {
                    Azimuth += Math.PI;
                }
            }
            if (Azimuth < 0)
            {
                Azimuth += 2*Math.PI;
            }
            if (Azimuth > 2*Math.PI)
            {
                Azimuth -= 2*Math.PI;
            }
            Azimuth *= 180/Math.PI;
        }

        /// <summary>
        /// 速度，以km/hour为单位
        /// </summary>
        private void GetVelocity()
        {
            double dt = (EndMjd - StartMjd) * 24;//以小时为单位
            Velocity = Length / dt;
        }
        /// <summary>
        /// 以km为单位
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        private void GetLength(Epoch start, Epoch end)
        {
            double dx = end.x - start.x;
            double dy = end.y - start.y;
            Length = Math.Sqrt(dx * dx + dy * dy) / 1000.0;
        }

        public override string ToString()
        {
            string line = $"{Sn:00}, {StartMjd:f5}-{EndMjd:f5}, ";
            line += $"{Velocity:f3}, {Azimuth:f3}";
            return line;
        }
    }
}
