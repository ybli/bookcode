using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MeasuringPointsForDesignedPoints
{
    public class LateralDeviationSet_lb
    {
        public double[,] Lateral_TraCURVE_Dis(double RADIUS, double L0, double x, double y, double N_L0, double N_DETA)
        {
	        double CURVE_L = N_L0;
	        double CURVE_LL = N_L0+100;
	        double A = 1.0/(40.0*RADIUS*RADIUS*L0*L0);
	        double B = 1.0/(6.0*RADIUS*L0);
	        int N= 0; // 迭代次数
	        double[,] Newton_L = new double[1, 13];
	        for(int j = 0; j < 13; j++)
	        {
		        Newton_L[0,j] = 1111111;
	        }
	        while((Math.Abs (CURVE_LL-CURVE_L)) > N_DETA) // N_DETA是迭代阈值
	        {
		        CURVE_LL = CURVE_L;
		        CURVE_L = CURVE_L - (5*A*A*CURVE_L*CURVE_L*CURVE_L*CURVE_L*CURVE_L*CURVE_L*CURVE_L*CURVE_L*CURVE_L + (3.0*B*B-6.0*A)*CURVE_L*CURVE_L*CURVE_L*CURVE_L*CURVE_L + 5.0*A*x*CURVE_L*CURVE_L*CURVE_L*CURVE_L - 3.0*B*y*CURVE_L*CURVE_L + CURVE_L - x)/(45.0*A*A*CURVE_L*CURVE_L*CURVE_L*CURVE_L*CURVE_L*CURVE_L*CURVE_L*CURVE_L + 5.0*(3.0*B*B - 6.0*A)*CURVE_L*CURVE_L*CURVE_L*CURVE_L + 20.0*A*x*CURVE_L*CURVE_L*CURVE_L - 6.0*B*y*CURVE_L + 1);
		        Newton_L[0,N+2] = CURVE_L;
		        N = N+1;
		        if(N == 11)
		        {
			        break;
		        }
	        }
	        Newton_L[0,0] = CURVE_L; // 迭代值
	        Newton_L[0,1] = N;
	        return Newton_L;
        }

        public double Lateral_CirCURVE(double RADIUS, double L0, double BETA, double M, double P, double x, double y)
        {
	        double CIRCULAR_CURVE_L = 0;
	        CIRCULAR_CURVE_L = (Math.Atan ((x - M)/(RADIUS + P - y)) - BETA)*RADIUS + L0;
	        return CIRCULAR_CURVE_L;
        }
    }
}
