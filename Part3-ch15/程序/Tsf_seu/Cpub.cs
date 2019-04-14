using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tsf_seu
{
    public struct kzdxy
    {
        public string name;
        public double x;
        public double y;

    }
    public struct vdir
    {
        public int no;//仪器瞄准已知控制点在控制点中的序号，以便查找对应控制点坐标
        public string name;//仪器瞄准已知控制点名称
        public double v_ang;//方向观测值       
        public double v_dis;//距离观测值
        public double v_a;//方向改正值       
        public double v_d;//距离改正值
        public double dis0;//距离近似值
        public double fwj;//方位角近似值
        public double ang_a;//方向观测值误差方程系数a
        public double ang_b;//方向观测值误差方程系数b
        public double ang_l;//方向观测值误差方程常数项l
        public double dis_a;//距离观测值误差方程系数a
        public double dis_b;//距离观测值误差方程系数b
        public double dis_l;//距离观测值误差方程常数项l
        public double wp;//距离权
    }
    
    
    class Cpub
    {
        public static double pii = 3.1415926;
        public static double po=206.265;
        public static bool data_apply = false;
        public static List<kzdxy> mListkzd = new List<kzdxy>(); // 已知控制点集合
        public static List<vdir> mListvdir = new List<vdir>(); // 观测方向集合        
        public static int n_kzd;//已知控制点总数        
        public static int n_dir;//观测瞄准点方向总数
        public static int n_ang;//观测方向总数
        public static int n_dis;//观测边总数
        public static double  ma;//测角方向误差
        public static double mda;//测距固定误差
        public static double mdb;//测距比例误差


        //通过ref引用函数返回两个参数，还可以通过类、结构体类型返回函数多个参数
        public void fwj_jl(ref double jl, ref double jd, double x1, double y1, double x2, double y2)
        {
            jl = Math.Sqrt((x2 - x1) * (x2 - x1) + (y2 - y1) * (y2 - y1));
            jd = Math.Atan((y2 - y1) / (x2 - x1));
        }

        public static double fwj(double dx, double dy)  //坐标方位角计算，dx,dy分别为坐标增量
        {
            double az = 0;
            if (dx == 0)
            {
                if (dy > 0) az = pii / 2.0;
                else az = pii * 3.0 / 2.0;
                return az;
            }
            az = Math.Atan(dy / dx);
            if (dx > 0 && dy >= 0)//第一象限
                az = az + 0.0;
            else if (dx > 0 && dy < 0)//第2象限
                az = 2 * pii + az;
            else if (dx < 0 && dy >= 0)//第4象限
                az = pii + az;
            else if (dx < 0 && dy <= 0)//第3象限
                az = pii + az;
            return az;
        }

        public static double ddeg(double x)  //角度转化为弧度
        {
            double y, a1, a2, a3;
           
            y = x;
            x = Math.Abs(x);
            a1 = Math.Floor(x);
            

            a2 = (x + 0.00000001 - a1) * 100;
            a3 = Math.Floor(a2);
            x = a1 + a3 / 60.0 + (a2 - a3) / 36.0;
            x = x * pii / 180.0;
            x = Math.Sign(y) * x;          
            return x;
        }
       

        public static double ddms(double x)   //弧度转化为角度
        {
            double y, a1, a2, a3;
            y = x;
            x = Math.Abs(x);
            x = x * 180.0 / pii;
            a1 = Math.Floor(x);
            a2 = (x - a1) * 60.0;
            a3 = Math.Floor(a2);
            x = a1 + a3 / 100.0 + (a2 - a3) * 60.0 / 10000.0;
            return x;
        }
      
       


        public static string rad_dms_str(double rad)//弧度转化为角度字符串
        {
            string str = "",str0="";
            double d = (rad) / Math.PI * 180;
            string sign = "";
            if (d < 0)
            {
                sign = "-";
            }
            d = Math.Abs(d)+0.000005;
            double dd, mm, ss;
            dd = Math.Floor(d);//舍弃小数，保留整数，度。
            mm = Math.Floor((d - dd) * 60.0);//分
            ss = (d - dd - mm / 60.0) * 3600.0;
            
            str0 = (ss + 0.05 + 100.0).ToString ();
            str0=str0.Substring (1, 4);
           // str = sign.ToString() + dd.ToString() + "°" + mm.ToString().PadLeft(2, '0') + "′" + ss.ToString("f1") + "″";
            str = sign.ToString() + dd.ToString() + "°" + mm.ToString().PadLeft(2, '0') + "′" + str0 + "″";
            return str;
        }



        public static bool digstr(String str)//判断字符串是否是真正的数值，包括是否含字母、多个小数点等
        {
            char acd;

            str = str.Trim();

            int nn = 0;
            bool reyn = true;
            if (str == null || "".Equals(str))
                reyn = false;

            else
            {
                for (int i = 0; i < str.Length; i++)
                {
                    acd = char.Parse(str.Substring(i, 1));
                    if (acd == 46)
                        nn = nn + 1;
                    else if (i == 0 && (acd == 45 || acd == 43))
                    {
                        reyn = true;
                    }
                    else if (acd < 48 || acd > 57)
                    {
                        reyn = false;
                    }
                }

            }
            if (nn > 1)
                reyn = false;
            return reyn;

        }
        //==================

        //==================

    }
}
