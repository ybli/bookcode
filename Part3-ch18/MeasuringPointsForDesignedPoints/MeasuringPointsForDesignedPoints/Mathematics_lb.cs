using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MeasuringPointsForDesignedPoints
{
    public class Mathematics_lb
    {
        public double Math_AZIMUTH(double DETAX, double DETAY)
        {
            double AZIMUTH = 0.0;
	        if((DETAX > 0) && (DETAY > 0))
	        {
		        AZIMUTH = System.Math.Atan (DETAY/DETAX); // 第一象限
	        }
	        else if((DETAX < 0) && (DETAY > 0))
	        {
		        AZIMUTH = System.Math.PI + System.Math.Atan (DETAY/DETAX); // 第二象限
	        }
	        else if((DETAX < 0) && (DETAY < 0))
	        {
		        AZIMUTH = System.Math.PI + System.Math.Atan (DETAY/DETAX); // 第三象限
	        }
	        else if((DETAX > 0) && (DETAY < 0))
	        {
		        AZIMUTH = 2.0*System.Math.PI + System.Math.Atan (DETAY/DETAX); // 第四象限
	        }
	        else if((DETAX == 0) && (DETAY > 0))
	        {
		        AZIMUTH = System.Math.PI/2.0;
	        }
	        else if((DETAX == 0) && (DETAY < 0))
	        {
		        AZIMUTH = System.Math.PI*3.0/2.0;
	        }
	        else if((DETAX > 0) && (DETAY == 0))
	        {
		        AZIMUTH = 0.0;
	        }
	        else if((DETAX < 0) && (DETAY == 0))
	        {
		        AZIMUTH = System.Math.PI;
	        }
	        return AZIMUTH; // A(i)到A(i+1)方位角
        }

        public double Math_ANGLE(double AZIMUTH1, double AZIMUTH2)
        {
            double ANGLE;
            if(AZIMUTH1 >= AZIMUTH2)
            {
                if((AZIMUTH1 - AZIMUTH2) <= (System.Math.PI ))
                {
                    ANGLE = AZIMUTH1 - AZIMUTH2;
                }
                else
                {
                    ANGLE = 2*System.Math.PI - (AZIMUTH1 - AZIMUTH2);
                }
            }
            else
            {
                if((AZIMUTH2 - AZIMUTH1) < (System.Math.PI))
                {
                    ANGLE = AZIMUTH2 - AZIMUTH1;
                }
                else
                {
                    ANGLE = 2*System.Math.PI - (AZIMUTH2 - AZIMUTH1);
                }
            }
            return ANGLE; // 夹角
        }

        public double[,] Math_xytoXYFCoordinateTransformation(double STA_X0, double STA_Y0, double x, double y, double AZIMUTH_ZH, double K)
        {
            double[,] XYCoorTransfor = new double[1, 2];
            XYCoorTransfor[0,0] = x*System.Math.Cos(AZIMUTH_ZH) - (K*y)*System.Math.Sin(AZIMUTH_ZH) + STA_X0; // X
            XYCoorTransfor[0,1] = x*System.Math.Sin(AZIMUTH_ZH) + (K*y)*System.Math.Cos(AZIMUTH_ZH) + STA_Y0; // Y
            return XYCoorTransfor;
        }

        public double[,] Math_xytoXYSCoordinateTransformation(double STA_X0, double STA_Y0, double x, double y, double AZIMUTH_HZ, double K)
        {
            double[,] XYCoorTransfor = new double[1, 2];
            XYCoorTransfor[0,0] = x*System.Math.Cos(AZIMUTH_HZ) - (-K*y)*System.Math.Sin(AZIMUTH_HZ) + STA_X0; // X
            XYCoorTransfor[0,1] = x*System.Math.Sin(AZIMUTH_HZ) + (-K*y)*System.Math.Cos(AZIMUTH_HZ) + STA_Y0; // Y 
            return XYCoorTransfor;
        }

        public double[,] Math_XYtoxyFCoordinateTransformation(double STA_X0, double STA_Y0, double TRA_X, double TRA_Y, double AZIMUTH_ZH, double K)
        {
            double[,] xyCoorTransfor = new double[1, 2];
            xyCoorTransfor[0,0] = (TRA_X-STA_X0)*System.Math.Cos(AZIMUTH_ZH) + (TRA_Y-STA_Y0)*System.Math.Sin(AZIMUTH_ZH); // X
            xyCoorTransfor[0,1] = K*((TRA_X-STA_X0)*(-System.Math.Sin(AZIMUTH_ZH)) + (TRA_Y-STA_Y0)*System.Math.Cos(AZIMUTH_ZH)); // Y
            return xyCoorTransfor;
        }

        public double[,] Math_XYtoxySCoordinateTransformation(double STA_X0, double STA_Y0, double TRA_X, double TRA_Y, double AZIMUTH_HZ, double K)
        {
            double[,] xyCoorTransfor = new double[1, 2];
            xyCoorTransfor[0,0] = (TRA_X-STA_X0)*System.Math.Cos(AZIMUTH_HZ) + (TRA_Y-STA_Y0)*System.Math.Sin(AZIMUTH_HZ); // X
            xyCoorTransfor[0,1] = -K*((TRA_X-STA_X0)*(-System.Math.Sin(AZIMUTH_HZ)) + (TRA_Y-STA_Y0)*System.Math.Cos(AZIMUTH_HZ)); // Y
            return xyCoorTransfor;
        }

        public double Math_ANGLEtoAZIMUTH(double ANGLE1)
        {
            double AZIMUTH = 0.0;
            if(ANGLE1 >= (Math.PI*2.0))
            {
                AZIMUTH = (ANGLE1/(Math.PI*2.0)-Math.Floor(ANGLE1/(Math.PI*2.0)))*Math.PI*2.0;
            }
            else if((ANGLE1 >= 0) && (ANGLE1 < (Math.PI*2.0)))
            {
                AZIMUTH=ANGLE1;
            }
            else if((ANGLE1 < 0) && (ANGLE1>=-(Math.PI*2.0)))
            {
                AZIMUTH = Math.PI*2.0 + ANGLE1;
            }
            else
            {
                AZIMUTH = Math.PI*2.0-(Math.Abs(ANGLE1)/(Math.PI*2.0) - Math.Floor(Math.Abs(ANGLE1)/(Math.PI*2.0)))*Math.PI*2.0;
            }
            return AZIMUTH;
        }
    }
}
