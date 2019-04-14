using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoorTran
{
    public class EarthPara
    {
        public double a, b;//长半轴与短半轴;
        public double f, invf;//扁率及其倒数;
        public double e1Square, e2Square;//第一、二偏心率的平方;
        public double M0;
        public int L0;//中央子午线经度;

        //无参构造函数时默认的椭球参数;
        public EarthPara()
        {
            a = 6378137.0;
            invf = 298.3;
            L0 = -120;//自定的！！！
            Init();
        }

        void Init()
        {
            f = 1.0 / invf;
            b = a * (1 - f);
            e1Square = (a * a - b * b) / a / a;
            e2Square = (a * a - b * b) / b / b;
            M0 = a * (1 - e1Square);
        }

        public EarthPara(double _a, double _invf)
        {
            a = _a;
            invf = _invf;
            Init();
        }

        //输入：纬度;
        //输出：各种辅助量;
        #region 计算辅助量
        public double W(double B)
        {
            return Math.Sqrt(1 - e1Square * Math.Pow(Math.Sin(B), 2));
        }
        public double Eta2(double B)
        {
            return e2Square * Math.Pow(Math.Cos(B), 2);
        }
        public double t(double B)
        {
            return Math.Tan(B);
        }
        //卯酉圈与子午圈;
        public double N(double B)
        {
            return a / W(B);
        }
        public double M(double B)
        {
            return a * (1 - e1Square) / Math.Pow(W(B), 3);
        }
        #endregion
    }
}
