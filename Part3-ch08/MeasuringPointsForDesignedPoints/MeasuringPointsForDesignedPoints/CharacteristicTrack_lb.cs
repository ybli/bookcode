using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MeasuringPointsForDesignedPoints
{
    public class CharacteristicTrack_lb
    {
        public double erfa;

        public double beta0;

        public double m;

        public double p;

        public double T;

        public double L;

        public double E0;

        public double q;

        public double l0; // 缓和曲线

        public double Rad; // 半径

        public double[,] ZH_XY = new double[1, 2];

        public double[,] HY_XY = new double[1, 2];

        public double[,] QZ_XY = new double[1, 2];

        public double[,] YH_XY = new double[1, 2];

        public double[,] HZ_XY = new double[1, 2];

        public double[,] ZH_xy = new double[1, 2];

        public double[,] HY_xy = new double[1, 2];

        public double[,] QZ_xy = new double[1, 2];

        public double[,] YH_xy = new double[1, 2];

        public double[,] HZ_xy = new double[1, 2];

        public double ERFAFun(double ERFA_DFM, double L0, double RADIUS)
        {
            erfa = CharaTrack_INFO(ERFA_DFM, L0, RADIUS)[0, 0];
            return erfa;
        }

        public double BRTA0Fun(double ERFA_DFM, double L0, double RADIUS)
        {
            beta0 = CharaTrack_INFO(ERFA_DFM, L0, RADIUS)[1, 0];
            return beta0;
        }

        public double MFun(double ERFA_DFM, double L0, double RADIUS)
        {
            m = CharaTrack_INFO(ERFA_DFM, L0, RADIUS)[2, 0];
            return m;
        }

        public double PFun(double ERFA_DFM, double L0, double RADIUS)
        {
            p = CharaTrack_INFO(ERFA_DFM, L0, RADIUS)[3, 0];
            return p;
        }

        public double TFun(double ERFA_DFM, double L0, double RADIUS)
        {
            T = CharaTrack_INFO(ERFA_DFM, L0, RADIUS)[4, 0];
            return T;
        }

        public double LFun(double ERFA_DFM, double L0, double RADIUS)
        {
            L = CharaTrack_INFO(ERFA_DFM, L0, RADIUS)[5, 0];
            return L;
        }

        public double E0Fun(double ERFA_DFM, double L0, double RADIUS)
        {
            E0 = CharaTrack_INFO(ERFA_DFM, L0, RADIUS)[6, 0];
            return E0;
        }

        public double QFun(double ERFA_DFM, double L0, double RADIUS)
        {
            q = CharaTrack_INFO(ERFA_DFM, L0, RADIUS)[7, 0];
            return q;
        }

        public double [,] ZH_XYFun(double JDX, double JDY, double AZIMUTH_JD12, double AZIMUTH_JD21, double AZIMUTH_JD23, double AZIMUTH_JD32, double L0, double RADIUS, double K, double ERFA_DFM)
        {
            ZH_XY[0, 0] =  CharaTrack_FivePoints(JDX, JDY, AZIMUTH_JD12, AZIMUTH_JD21, AZIMUTH_JD23, AZIMUTH_JD32, L0, RADIUS, K, ERFA_DFM)[0, 2];
            ZH_XY[0, 1] =  CharaTrack_FivePoints(JDX, JDY, AZIMUTH_JD12, AZIMUTH_JD21, AZIMUTH_JD23, AZIMUTH_JD32, L0, RADIUS, K, ERFA_DFM)[0, 3];
            return ZH_XY;
        }

        public double [,] HY_XYFun(double JDX, double JDY, double AZIMUTH_JD12, double AZIMUTH_JD21, double AZIMUTH_JD23, double AZIMUTH_JD32, double L0, double RADIUS, double K, double ERFA_DFM)
        {
            HY_XY[0, 0] =  CharaTrack_FivePoints(JDX, JDY, AZIMUTH_JD12, AZIMUTH_JD21, AZIMUTH_JD23, AZIMUTH_JD32, L0, RADIUS, K, ERFA_DFM)[1, 2];
            HY_XY[0, 1] =  CharaTrack_FivePoints(JDX, JDY, AZIMUTH_JD12, AZIMUTH_JD21, AZIMUTH_JD23, AZIMUTH_JD32, L0, RADIUS, K, ERFA_DFM)[1, 3];
            return HY_XY;
        }

        public double [,] QZ_XYFun(double JDX, double JDY, double AZIMUTH_JD12, double AZIMUTH_JD21, double AZIMUTH_JD23, double AZIMUTH_JD32, double L0, double RADIUS, double K, double ERFA_DFM)
        {
            QZ_XY[0, 0] =  CharaTrack_FivePoints(JDX, JDY, AZIMUTH_JD12, AZIMUTH_JD21, AZIMUTH_JD23, AZIMUTH_JD32, L0, RADIUS, K, ERFA_DFM)[2, 2];
            QZ_XY[0, 1] =  CharaTrack_FivePoints(JDX, JDY, AZIMUTH_JD12, AZIMUTH_JD21, AZIMUTH_JD23, AZIMUTH_JD32, L0, RADIUS, K, ERFA_DFM)[2, 3];
            return QZ_XY;
        }

        public double [,] YH_XYFun(double JDX, double JDY, double AZIMUTH_JD12, double AZIMUTH_JD21, double AZIMUTH_JD23, double AZIMUTH_JD32, double L0, double RADIUS, double K, double ERFA_DFM)
        {
            YH_XY[0, 0] =  CharaTrack_FivePoints(JDX, JDY, AZIMUTH_JD12, AZIMUTH_JD21, AZIMUTH_JD23, AZIMUTH_JD32, L0, RADIUS, K, ERFA_DFM)[3, 2];
            YH_XY[0, 1] =  CharaTrack_FivePoints(JDX, JDY, AZIMUTH_JD12, AZIMUTH_JD21, AZIMUTH_JD23, AZIMUTH_JD32, L0, RADIUS, K, ERFA_DFM)[3, 3];
            return YH_XY;
        }

        public double [,] HZ_XYFun(double JDX, double JDY, double AZIMUTH_JD12, double AZIMUTH_JD21, double AZIMUTH_JD23, double AZIMUTH_JD32, double L0, double RADIUS, double K, double ERFA_DFM)
        {
            HZ_XY[0, 0] =  CharaTrack_FivePoints(JDX, JDY, AZIMUTH_JD12, AZIMUTH_JD21, AZIMUTH_JD23, AZIMUTH_JD32, L0, RADIUS, K, ERFA_DFM)[4, 2];
            HZ_XY[0, 1] =  CharaTrack_FivePoints(JDX, JDY, AZIMUTH_JD12, AZIMUTH_JD21, AZIMUTH_JD23, AZIMUTH_JD32, L0, RADIUS, K, ERFA_DFM)[4, 3];
            return HZ_XY;
        }

        public double[,] ZH_xyFun()
        {
            ZH_xy[0, 0] = 0.0;
            ZH_xy[0, 1] = 0.0;
            return ZH_xy;
        }

        public double [,] HY_xyFun(double JDX, double JDY, double AZIMUTH_JD12, double AZIMUTH_JD21, double AZIMUTH_JD23, double AZIMUTH_JD32, double L0, double RADIUS, double K, double ERFA_DFM)
        {
            HY_xy[0, 0] =  CharaTrack_FivePoints(JDX, JDY, AZIMUTH_JD12, AZIMUTH_JD21, AZIMUTH_JD23, AZIMUTH_JD32, L0, RADIUS, K, ERFA_DFM)[1, 0];
            HY_xy[0, 1] =  CharaTrack_FivePoints(JDX, JDY, AZIMUTH_JD12, AZIMUTH_JD21, AZIMUTH_JD23, AZIMUTH_JD32, L0, RADIUS, K, ERFA_DFM)[1, 1];
            return HY_xy;
        }

        public double [,] QZ_xyFun(double JDX, double JDY, double AZIMUTH_JD12, double AZIMUTH_JD21, double AZIMUTH_JD23, double AZIMUTH_JD32, double L0, double RADIUS, double K, double ERFA_DFM)
        {
            QZ_xy[0, 0] =  CharaTrack_FivePoints(JDX, JDY, AZIMUTH_JD12, AZIMUTH_JD21, AZIMUTH_JD23, AZIMUTH_JD32, L0, RADIUS, K, ERFA_DFM)[2, 0];
            QZ_xy[0, 1] =  CharaTrack_FivePoints(JDX, JDY, AZIMUTH_JD12, AZIMUTH_JD21, AZIMUTH_JD23, AZIMUTH_JD32, L0, RADIUS, K, ERFA_DFM)[2, 1];
            return QZ_xy;
        }

        public double [,] YH_xyFun(double JDX, double JDY, double AZIMUTH_JD12, double AZIMUTH_JD21, double AZIMUTH_JD23, double AZIMUTH_JD32, double L0, double RADIUS, double K, double ERFA_DFM)
        {
            YH_xy[0, 0] =  CharaTrack_FivePoints(JDX, JDY, AZIMUTH_JD12, AZIMUTH_JD21, AZIMUTH_JD23, AZIMUTH_JD32, L0, RADIUS, K, ERFA_DFM)[3, 0];
            YH_xy[0, 1] =  CharaTrack_FivePoints(JDX, JDY, AZIMUTH_JD12, AZIMUTH_JD21, AZIMUTH_JD23, AZIMUTH_JD32, L0, RADIUS, K, ERFA_DFM)[3, 1];
            return YH_xy;
        }

        public double[,] HZ_xyFun()
        {
            HZ_xy[0, 0] = 0.0;
            HZ_xy[0, 1] = 0.0;
            return HZ_xy;
        }

        public double[,] CharaTrack_INFO(double ERFA_DFM, double L0, double RADIUS)
        {
	        double[,] INFO = new double[8,1];
	        string[] SEPARATEFM = new string[2];
	        string ERFA_DFMstr; // 存储度.分秒形式的字符串
	        ERFA_DFMstr = System.Convert.ToString(ERFA_DFM);
	        int TEMP2 = 0; // 临时字符变量
	        if (ERFA_DFMstr.Contains(".") == false) // 不含有点号；若含有点则返回1
	        {
		        ERFA_DFMstr = string.Concat(ERFA_DFMstr, ".0000");
	        }
	        else
	        {
                List<string> TEMP1 = new List<string>(ERFA_DFMstr.Split ('.'));
		        TEMP2 = TEMP1[1].Length; // 小数点后的所有字符
		        if (TEMP2 < 4)
		        {
			        for (int i = 0; i < (4-TEMP2); i++)
			        {
				        ERFA_DFMstr = string.Concat(ERFA_DFMstr, "0");
			        }
		        }
	        }
	        List<string> SEPARATEDFM = new List<string>(ERFA_DFMstr.Split ('.')); // 取出偏转角
	        SEPARATEFM[0] = SEPARATEDFM[1].Substring (0,2);
	        SEPARATEFM[1] = SEPARATEDFM[1].Substring (2);
	        int TEMP = SEPARATEFM[1].Length ;
	        double ERFA = (System.Convert.ToDouble (SEPARATEDFM[0]) + (System.Convert.ToDouble (SEPARATEFM[0]))/60.0 + (System.Convert.ToDouble (SEPARATEFM[1]))/(System.Math.Pow (10.0, TEMP - 2))/3600.0)/180.0*(System.Math.PI); // 弧度
	        INFO[0,0] = ERFA;
	        double BETA0 = L0/(2.0*RADIUS);
	        INFO[1,0] = BETA0;
	        double M = L0/2.0-L0*L0*L0/(240.0*RADIUS*RADIUS); // 切垂距
	        INFO[2,0] =M;
	        double P = L0*L0/(24.0*RADIUS) - L0*L0*L0*L0/(2688.0*RADIUS*RADIUS*RADIUS);
	        INFO[3,0] = P;
	        double T = M + (RADIUS + P)*(System.Math.Tan (ERFA/2.0)); // 切线长
	        INFO[4,0] = T;
	        double L = 2.0*L0 + RADIUS*(ERFA - 2.0*BETA0); // 曲线长
	        INFO[5,0] = L;
	        double E0 = (RADIUS + P)/(System.Math.Cos (ERFA/2.0)) - RADIUS;
	        INFO[6,0] = E0;
	        double Q = 2.0*T-L;
	        INFO[7,0] = Q;

	        return INFO;
        }

        public double[,] CharaTrack_TangentxyT(double L0, double RADIUS, double LX)
        {
	        double[,] TANGENTxyT = new double[1,2];
	        double C_LR = L0*RADIUS;
	        TANGENTxyT[0,0] = LX-LX*LX*LX*LX*LX/(40.0*C_LR*C_LR) + LX*LX*LX*LX*LX*LX*LX*LX*LX/(3456.0*C_LR*C_LR*C_LR*C_LR);
	        TANGENTxyT[0,1] = LX*LX*LX/(6.0*C_LR) - LX*LX*LX*LX*LX*LX*LX/(336.0*C_LR*C_LR*C_LR) + LX*LX*LX*LX*LX*LX*LX*LX*LX*LX*LX/(42240.0*C_LR*C_LR*C_LR*C_LR*C_LR);
	        return TANGENTxyT;
        }

        public double[,] CharaTrack_TangentxyC(double L0, double RADIUS, double LX, double ERFA_DFM)
        {
	        double[,] TANGENTxyC = new double[1,2];
	        int info_row = CharaTrack_INFO(ERFA_DFM, L0, RADIUS).GetLength(0);
	        int info_col = CharaTrack_INFO(ERFA_DFM, L0, RADIUS).GetLength(1);
	        double[,] info = new double[info_row, info_col];
	        info = CharaTrack_INFO(ERFA_DFM, L0, RADIUS);
	        double beta0 = info[1,0];
	        double m = info[2,0];
	        double p = info[3,0];
	        TANGENTxyC[0,0] = RADIUS*System.Math.Sin((LX - L0)/RADIUS + beta0) + m;
	        TANGENTxyC[0,1] = RADIUS*(1 - System.Math.Cos((LX - L0)/RADIUS + beta0)) + p;
	        return TANGENTxyC;
        }

        public double[,] CharaTrack_FivePoints(double JDX, double JDY, double AZIMUTH_JD12, double AZIMUTH_JD21, double AZIMUTH_JD23, double AZIMUTH_JD32, double L0, double RADIUS, double K, double ERFA_DFM)
        {
	        double[,] FIVEPOINTS = new double[5,4];
	        int info_row = CharaTrack_INFO(ERFA_DFM, L0, RADIUS).GetLength(0);
	        int info_col = CharaTrack_INFO(ERFA_DFM, L0, RADIUS).GetLength(1);
	        double[,] info = new double[info_row, info_col];
	        info = CharaTrack_INFO(ERFA_DFM, L0, RADIUS);
	        double L = info[5,0];
	        double T = info[4,0];
	        double[,] TEMP = new double[1,2];
	        FIVEPOINTS[0,0] = 0.0; // x
	        FIVEPOINTS[0,1] = 0.0; // y
	        FIVEPOINTS[0,2] = JDX + T*(System.Math.Cos (AZIMUTH_JD21));
	        FIVEPOINTS[0,3] = JDY + T*(System.Math.Sin (AZIMUTH_JD21));
	        TEMP = CharaTrack_TangentxyT(L0, RADIUS, L0);
	        FIVEPOINTS[1,0] = TEMP[0,0]; // x
	        FIVEPOINTS[1,1] = TEMP[0,1]; // y
	        FIVEPOINTS[1,2] = FIVEPOINTS[1,0]*System.Math.Cos(AZIMUTH_JD12) - (K*FIVEPOINTS[1,1])*System.Math.Sin(AZIMUTH_JD12) + FIVEPOINTS[0,2];
	        FIVEPOINTS[1,3] = FIVEPOINTS[1,0]*System.Math.Sin(AZIMUTH_JD12) + (K*FIVEPOINTS[1,1])*System.Math.Cos(AZIMUTH_JD12) + FIVEPOINTS[0,3];
	        TEMP = CharaTrack_TangentxyC(L0, RADIUS, L*0.5, ERFA_DFM);
	        FIVEPOINTS[2,0] = TEMP[0,0]; // x
	        FIVEPOINTS[2,1] = TEMP[0,1]; // y
	        FIVEPOINTS[2,2] = FIVEPOINTS[2,0]*System.Math.Cos (AZIMUTH_JD12) - (K*FIVEPOINTS[2,1])*System.Math.Sin (AZIMUTH_JD12) + FIVEPOINTS[0,2];
	        FIVEPOINTS[2,3] = FIVEPOINTS[2,0]*System.Math.Sin (AZIMUTH_JD12) + (K*FIVEPOINTS[2,1])*System.Math.Cos (AZIMUTH_JD12) + FIVEPOINTS[0,3];
	        FIVEPOINTS[4,0] = 0.0; // x
	        FIVEPOINTS[4,1] = 0.0; // y
	        FIVEPOINTS[4,2] = JDX + T*(System.Math.Cos(AZIMUTH_JD23));
	        FIVEPOINTS[4,3] = JDY + T*(System.Math.Sin(AZIMUTH_JD23));
	        TEMP = CharaTrack_TangentxyT(L0, RADIUS, L0);
	        FIVEPOINTS[3,0] = TEMP[0,0]; // x
	        FIVEPOINTS[3,1] = TEMP[0,1]; // y
	        FIVEPOINTS[3,2] = FIVEPOINTS[3,0]*System.Math.Cos(AZIMUTH_JD32) - (-K*FIVEPOINTS[3,1])*System.Math.Sin(AZIMUTH_JD32) + FIVEPOINTS[4,2];
	        FIVEPOINTS[3,3] = FIVEPOINTS[3,0]*System.Math.Sin(AZIMUTH_JD32) + (-K*FIVEPOINTS[3,1])*System.Math.Cos(AZIMUTH_JD32) + FIVEPOINTS[4,3];
	        return FIVEPOINTS;
        }
    }
}
