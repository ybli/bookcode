using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TIN
{
    public struct PointF
    {
        public double x;
        public double y;
        public double z;
    }

    public class Tpoint
    {
        /// <summary>
        /// 点号
        /// </summary>
        public int Num;
        /// <summary>
        /// 点名
        /// </summary>
        public string Name;
        public double x;
        public double y;
        public double h;
        
        public Tpoint(int num, string name, double x, double y, double h)
        {
            Num = num;
            Name = name;
            this.x = x;
            this.y = y;
            this.h = h;
        }

        public Tpoint(double x, double y, double h)
        {
            this.x = x;
            this.y = y;
            this.h = h;
        }

        public Tpoint()
        {

        }

        public static bool operator ==(Tpoint A, Tpoint B)
        {
            bool IsSame = new bool();
            if (A.x == B.x && A.y == B.y)
                IsSame = true;
            else
                IsSame = false;
            return IsSame;
        }

        public static bool operator !=(Tpoint A, Tpoint B)
        {
            return !(A == B);
        }

    }
}
