#include "DataCenter.h"


/********************************************************************************************************/
//                            @ Public functions
/*-------------------------------------------------------------------
 * Name : getCoordSys
 * Func : initialize constant parameter of coordinate fram 
 * Args : coordSys type    I  
 * Retn : coordParam      
 *-----------------------------------------------------------------*/
coordParam CoordCpu::getCoordSys(coordSys type)
{
    coordParam Cs;
    Cs.type  = WGS_84; 
    Cs.A     = 6378137.0; 
    Cs.Alfa  = 1.0 / 298.257223563;
    Cs.E2    = 0.00669437999014132; 
    Cs.UTM   = 1.0;
    Cs.X0    = 0; 
    Cs.Y0    = 0; 
    Cs.L0    = 0;
    Cs.H0    = 0;
    Cs.DN    = 0; 
    Cs.GM    = 0;
    Cs.Omega = 0;

    switch (type)
    {
    case WGS_84   : break;
    case BJ_54    :{
                    Cs.type  = BJ_54;
                    Cs.A     = 6378245.0;
                    Cs.Alfa  = 1.0 / 298.3;
                    Cs.E2    = 2 * Cs.Alfa - (Cs.Alfa * Cs.Alfa);
                    break;
    }
    case XA_80    :{
                    Cs.type  = XA_80;
                    Cs.A     = 6378140.0;
                    Cs.Alfa  = 1.0 / 298.257;
                    Cs.E2    = 2 * Cs.Alfa - (Cs.Alfa * Cs.Alfa);
                    Cs.E2    = 0.006694384999588;
                    break;
    }
    case PZ_90    :{
                    Cs.type  = PZ_90;
                    Cs.A     = 6378136.0;
                    Cs.Alfa  = 1.0 / 298.257839303;
                    Cs.E2    = 2 * Cs.Alfa - (Cs.Alfa * Cs.Alfa);
                    Cs.GM    = 3.9860044*1e+14;
                    Cs.Omega = 7.292115*1e-5;
                    Cs.UTM   = 0;
                    break;
    }
    case CGS_2000: {      
                    Cs.type  = CGS_2000;
                    Cs.A     = 6378137.0;
                    Cs.Alfa  = 1.0 / 298.257222101;
                    Cs.E2    = 2 * Cs.Alfa - (Cs.Alfa * Cs.Alfa);
                    Cs.GM    = 3.986004418*1e+14;
                    Cs.Omega = 7.292115*1e-5;
                    Cs.UTM   = 0;
                    break;

    }
    default: cout << "Con't match cooriante system, "
                     "all parameters have been set to the parameters of WGS-84!" 
                  << endl;
             break;
    }
    return Cs;
}

/*-------------------------------------------------------------------
* Name : XYZ_BLH
* Func : trancport X Y Z to B L H
* Args : double B, L, H       O
*        double X, Y, Z       I
*        coordParam   Cs      I
*-----------------------------------------------------------------*/
void CoordCpu::XYZ_BLH(double &B,
                       double &L,
                       double &H,
                       const double &X,
                       const double &Y,
                       const double &Z,
                       const coordParam &Cs)
{
   double R  = sqrt (X*X + Y*Y);
   double B0 = atan2(Z, R);
   double N  = 0;
   while (1)
   {
       N  = Cs.A / sqrt(1 - Cs.E2*sin(B0)*sin(B0));
       B  = atan2(Z + N*Cs.E2*sin(B0), R);
       if  (fabs(B - B0) < 1e-12)
            break;
       B0 = B;
       L  = atan2(Y, X);
       H  = R / cos(B) - N;
   }
}