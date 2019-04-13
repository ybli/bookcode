using System;
using System.Windows.Forms;

/********************************************************************************
** auth： Jin
** date： 2018/12/27
** desc： 边界点类
** Ver.:  1.0
*********************************************************************************/

namespace CalculationOfControlArea
{
    /// <summary>
    /// 边界点类
    /// </summary>
    class BPoint
    {
        #region 成员变量及setter，getter
        /// <summary>
        /// 高斯坐标X
        /// </summary>
        private double x;
        /// <summary>
        /// 高斯坐标Y
        /// </summary>
        private double y;
        /// <summary>
        /// 大地坐标B
        /// </summary>
        private double b = -1;
        /// <summary>
        /// 大地坐标L
        /// </summary>
        private double l = -1;
        /// <summary>
        /// 用于显示的DD.MMSS形式大地坐标B
        /// </summary>
        private string strB = "";
        /// <summary>
        /// 用于显示的DD.MMSS形式大地坐标L
        /// </summary>
        private string strL = "";
        private bool isAdd = false;
        public double X { get { return x; } set { x = value; } }
        public bool IsAdd{get { return isAdd; }set { isAdd = value; }}
        public double Y { get { return y; } set { y = value; } }
        public double B { get { return b; } set { b = value; } }
        public double L { get { return l; } set { l = value; } }
        public string StrB { get { return strB; } set { strB = value; } }
        public string StrL { get { return strL; } set { strL = value; } }
        #endregion

        /// <summary>
        /// 默认构造器
        /// </summary>
        public BPoint()
        {
            
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// 
        public BPoint(double x, double y, int z)
        {
            this.x = x;
            this.y = y;
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="b">纬度</param>
        /// <param name="l">经度</param>
        /// 
        public BPoint(double b, double l)
        {
            this.b = b;
            this.l = l;
            this.strB = Tool.AngleToDMS(b / Math.PI * 180);
            this.strL = Tool.AngleToDMS(l / Math.PI * 180);
        }
        /// <summary>
        /// 高斯坐标反算大地坐标
        /// </summary>
        public void XYtoBL()
        {
            int p;
            double y2, e, a0, b0, c, n, v, e22, n2, bf, t, k0, k1, k2, k3, k4, l0 = 0;
            if (y < 1000000)
            {
                MessageBox.Show("输入坐标无带号,错误!");
                return;
            }
            p = (int)Math.Floor(y / 1000000.0);
            y2 = y - 500000 - p * 1000000;
            k0 = 1.57048761144159 * 0.0000001;
            e = k0 * x;
            k1 = 5.05250178820567 * 0.001;
            k2 = 2.98472900956587 * 0.00001;
            k3 = 2.41626669230084 * 0.0000001;
            k4 = 2.22241238938534 * 0.000000001;
            bf = e + Math.Cos(e) * ((k1 * Math.Sin(e) - k2 * Math.Pow(Math.Sin(e), 3) +
                k3 * Math.Pow(Math.Sin(e), 5) - k4 * Math.Pow(Math.Sin(e), 7)));
            t = Math.Tan(bf);
            //CGCS坐标系相关参数
            //a0 = 6378137;
            //b0 = 6356752;
            //1980西安椭球参数
            a0 = 6378140;
            b0 = 6356755.29;
            e22 = (Math.Pow(a0, 2) - Math.Pow(b0, 2)) / Math.Pow(b0, 2);
            n2 = e22 * Math.Pow(Math.Cos(bf), 2);
            v = Math.Sqrt(1 + n2);
            c = Math.Pow(a0, 2) / b0;
            n = c / v;
            b = bf - 0.5 * (1 + n2) * t * Math.Pow(y2 / n, 2) + 1.0 / 24.0 *
                (5 + 3 * t * t + n2 - 9 * n2 * t * t) * (1 + n2) * t * Math.Pow(y2 / n, 4) - 1.0 / 720.0 *
                (61 + 90 * t * t + 45 * Math.Pow(t, 4)) * (1 + n2) * t * Math.Pow(y2 / n, 6);
            //中国范围判断是6度带分带还是3度带分带
            if (p >= 13 && p <= 23)
            {
                l0 = 6 * p - 3 / 180.0 * Math.PI;
            }
            else if (p >= 24 && p <= 45)
            {
                l0 = p * 3 / 180.0 * Math.PI;
            }
            l = (1.0 / Math.Cos(bf)) * (y2 / n) - 1.0 / 6.0 * (1 + 2 * t * t + n2) * (1.0 / Math.Cos(bf)) *
                Math.Pow(y2 / n, 3) + 1.2 / 120.0 * (5 + 28 * t * t + 24 * Math.Pow(t, 4) + 6 * n2 + 8 *
                n2 * t * t) * (1.0 / Math.Cos(bf)) * Math.Pow(y2 / n, 5) + l0;
            strB = Tool.AngleToDMS(b / Math.PI * 180);
            strL = Tool.AngleToDMS(l / Math.PI * 180);
        }
        /// <summary>
        /// 计算该点所在图幅编号
        /// </summary>
        /// <param name="meaScale">比例尺</param>
        /// <returns></returns>
        public string GetSheetNum(double meaScale)
        {
            double latDiffer = 0, lonDiffer = 0;
            Tool.SetLatAndLonDif(meaScale, ref latDiffer, ref lonDiffer);
            char[] alpha = { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V' };
            int a = 0, b = 0, c = 0, d = 0;
            String str = "";

            a = (int)Math.Floor(this.B * 180.0 / (Math.PI * 4)) + 1;
            b = (int)Math.Floor(this.L * 180.0 / (Math.PI * 6)) + 31;
            str = alpha[a - 1] + "" + b;

            if (meaScale == 1.0 / (5 * Math.Pow(10, 5)))
            {//比例尺为其他
                c = (int)(4 / latDiffer - Math.Floor(((this.B * 180.0 / Math.PI) % 4) / latDiffer));
                d = (int)Math.Floor(((this.L * 180.0 / Math.PI) % 6) / lonDiffer) + 1;
                string strC = c.ToString("000");
                string strD = d.ToString("000");
                str = alpha[a - 1] + "" + b + "B" + strC + strD;
            }
            else if (meaScale == 1.0 / (2.5 * Math.Pow(10, 5)))
            {//比例尺为1:250000
                c = (int)(4 / latDiffer - Math.Floor(((this.B * 180.0 / Math.PI) % 4) / latDiffer));
                d = (int)Math.Floor(((this.L * 180.0 / Math.PI) % 6) / lonDiffer) + 1;
                string strC = c.ToString("000");
                string strD = d.ToString("000");
                str = alpha[a - 1] + "" + b + "C" + strC + strD;
            }
            else if (meaScale == 1.0 / (1 * Math.Pow(10, 5)))
            {//比例尺为1:100000
                c = (int)(4 / latDiffer - Math.Floor(((this.B * 180.0 / Math.PI) % 4) / latDiffer));
                d = (int)Math.Floor(((this.L * 180.0 / Math.PI) % 6) / lonDiffer) + 1;
                string strC = c.ToString("000");
                string strD = d.ToString("000");
                str = alpha[a - 1] + "" + b + "D" + strC + strD;
            }
            else if (meaScale == 1.0 / (5 * Math.Pow(10, 4)))
            {//比例尺为1:50000
                c = (int)(4 / latDiffer - Math.Floor(((this.B * 180.0 / Math.PI) % 4) / latDiffer));
                d = (int)Math.Floor(((this.L * 180.0 / Math.PI) % 6) / lonDiffer) + 1;
                string strC = c.ToString("000");
                string strD = d.ToString("000");
                str = alpha[a - 1] + "" + b + "E" + strC + strD;
            }
            else if (meaScale == 1.0 / (2.5 * Math.Pow(10, 4)))
            {//比例尺为1:25000
                c = (int)(4 / latDiffer - Math.Floor(((this.B * 180.0 / Math.PI) % 4) / latDiffer));
                d = (int)Math.Floor(((this.L * 180.0 / Math.PI) % 6) / lonDiffer) + 1;
                string strC = c.ToString("000");
                string strD = d.ToString("000");
                str = alpha[a - 1] + "" + b + "F" + strC + strD;
            }
            else if (meaScale == 1.0 / (1 * Math.Pow(10, 4)))
            {//比例尺为1:10000
                c = (int)(4 / latDiffer - Math.Floor(((this.B * 180.0 / Math.PI) % 4) / latDiffer));
                d = (int)Math.Floor(((this.L * 180.0 / Math.PI) % 6) / lonDiffer) + 1;
                string strC = c.ToString("000");
                string strD = d.ToString("000");
                str = alpha[a - 1] + "" + b + "G" + strC + strD;
            }
            else if (meaScale == 1.0 / (5 * Math.Pow(10, 3)))
            {//比例尺为1:5000
                c = (int)(4 / latDiffer - Math.Floor(((this.B * 180.0 / Math.PI) % 4) / latDiffer));
                d = (int)Math.Floor(((this.L * 180.0 / Math.PI) % 6) / lonDiffer) + 1;
                string strC = c.ToString("000");
                string strD = d.ToString("000");
                str = alpha[a - 1] + "" + b + "H" + strC + strD;
            }
            return str;
        }
    }
}
