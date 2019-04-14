using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MeasuringPointsForDesignedPoints
{
    public class CalDev
    {
        public List<CalDevData> CalDevRes(List<InfoData> ListInfoDa, List<CzxData> ListCzxDa)
        {
            List<CalDevData> ListCalDevDa = new List<CalDevData>();

            Mathematics_lb Math_lb = new Mathematics_lb();

            CharacteristicTrack_lb Chatra_lb = new CharacteristicTrack_lb();

            LateralDeviationSet_lb Latdev_lb = new LateralDeviationSet_lb();

            double N_L_DETA = 0.0;

            double detax_jd21 = ListInfoDa[0].X - ListInfoDa[1].X;
            double detay_jd21 = ListInfoDa[0].Y - ListInfoDa[1].Y;
            double azimuth_jd21 = Math_lb.Math_AZIMUTH(detax_jd21, detay_jd21);
            double azimuth_jd12 = Math_lb.Math_AZIMUTH(-detax_jd21, -detay_jd21);
            double detax_jd23 = ListInfoDa[2].X - ListInfoDa[1].X;
            double detay_jd23 = ListInfoDa[2].Y - ListInfoDa[1].Y;
            double azimuth_jd23 = Math_lb.Math_AZIMUTH(detax_jd23, detay_jd23); // 计算交点到后一个交点的方位角,JD(i)到JD(i+1)的方位角
            double azimuth_jd32 = Math_lb.Math_AZIMUTH(-detax_jd23, -detay_jd23); // 计算JD(i+1)到JD(i)的方位角
            
            Chatra_lb.l0 = ListInfoDa[1].l0; // 缓和曲线
            Chatra_lb.Rad = ListInfoDa[1].Rad; // 半径
            Chatra_lb.erfa = Chatra_lb.ERFAFun(ListInfoDa[1].erfa, Chatra_lb.l0, Chatra_lb.Rad);
            Chatra_lb.beta0 = Chatra_lb.BRTA0Fun(ListInfoDa[1].erfa, Chatra_lb.l0, Chatra_lb.Rad);
            Chatra_lb.m = Chatra_lb.MFun(ListInfoDa[1].erfa, Chatra_lb.l0, Chatra_lb.Rad);
            Chatra_lb.p = Chatra_lb.PFun(ListInfoDa[1].erfa, Chatra_lb.l0, Chatra_lb.Rad);
            Chatra_lb.T = Chatra_lb.TFun(ListInfoDa[1].erfa, Chatra_lb.l0, Chatra_lb.Rad);
            Chatra_lb.L = Chatra_lb.LFun(ListInfoDa[1].erfa, Chatra_lb.l0, Chatra_lb.Rad);
            Chatra_lb.E0 = Chatra_lb.E0Fun(ListInfoDa[1].erfa, Chatra_lb.l0, Chatra_lb.Rad);
            Chatra_lb.q = Chatra_lb.QFun(ListInfoDa[1].erfa, Chatra_lb.l0, Chatra_lb.Rad);
            
            // 曲线特征点HY、YH点方位角
            double azimuth_HY = Math_lb.Math_ANGLEtoAZIMUTH(azimuth_jd12 + ListInfoDa[1].K * Chatra_lb.beta0);
            double azimuth_YHJ = Math_lb.Math_ANGLEtoAZIMUTH(azimuth_jd23 + ListInfoDa[1].K * (-1.0 * Chatra_lb.beta0));
            double azimuth_YH = azimuth_YHJ;
            double jd_mileage = ListInfoDa[1].Mil + Chatra_lb.T; // 交点的里程

            Chatra_lb.ZH_XY = Chatra_lb.ZH_XYFun(ListInfoDa[1].X, ListInfoDa[1].Y, azimuth_jd12, azimuth_jd21, azimuth_jd23, azimuth_jd32, Chatra_lb.l0, Chatra_lb.Rad, ListInfoDa[1].K, ListInfoDa[1].erfa);
            Chatra_lb.HY_xy = Chatra_lb.HY_xyFun(ListInfoDa[1].X, ListInfoDa[1].Y, azimuth_jd12, azimuth_jd21, azimuth_jd23, azimuth_jd32, Chatra_lb.l0, Chatra_lb.Rad, ListInfoDa[1].K, ListInfoDa[1].erfa);
            Chatra_lb.HY_XY = Chatra_lb.HY_XYFun(ListInfoDa[1].X, ListInfoDa[1].Y, azimuth_jd12, azimuth_jd21, azimuth_jd23, azimuth_jd32, Chatra_lb.l0, Chatra_lb.Rad, ListInfoDa[1].K, ListInfoDa[1].erfa);
            Chatra_lb.QZ_xy = Chatra_lb.QZ_xyFun(ListInfoDa[1].X, ListInfoDa[1].Y, azimuth_jd12, azimuth_jd21, azimuth_jd23, azimuth_jd32, Chatra_lb.l0, Chatra_lb.Rad, ListInfoDa[1].K, ListInfoDa[1].erfa);
            Chatra_lb.QZ_XY = Chatra_lb.QZ_XYFun(ListInfoDa[1].X, ListInfoDa[1].Y, azimuth_jd12, azimuth_jd21, azimuth_jd23, azimuth_jd32, Chatra_lb.l0, Chatra_lb.Rad, ListInfoDa[1].K, ListInfoDa[1].erfa);
            Chatra_lb.YH_xy = Chatra_lb.YH_xyFun(ListInfoDa[1].X, ListInfoDa[1].Y, azimuth_jd12, azimuth_jd21, azimuth_jd23, azimuth_jd32, Chatra_lb.l0, Chatra_lb.Rad, ListInfoDa[1].K, ListInfoDa[1].erfa);
            Chatra_lb.YH_XY = Chatra_lb.YH_XYFun(ListInfoDa[1].X, ListInfoDa[1].Y, azimuth_jd12, azimuth_jd21, azimuth_jd23, azimuth_jd32, Chatra_lb.l0, Chatra_lb.Rad, ListInfoDa[1].K, ListInfoDa[1].erfa);
            Chatra_lb.HZ_XY = Chatra_lb.HZ_XYFun(ListInfoDa[1].X, ListInfoDa[1].Y, azimuth_jd12, azimuth_jd21, azimuth_jd23, azimuth_jd32, Chatra_lb.l0, Chatra_lb.Rad, ListInfoDa[1].K, ListInfoDa[1].erfa);

            // 交点JD与曲中点QZ的方位角
            double detax_JQ = Chatra_lb.QZ_XY[0, 0] - ListInfoDa[1].X;
            double detay_JQ = Chatra_lb.QZ_XY[0, 1] - ListInfoDa[1].Y;
            double azimuth_JQ = Math_lb.Math_AZIMUTH(detax_JQ, detay_JQ);

            int befqzsl = 0;
            int befqzc = 0;
            int befqzcc = 0;
            int aftqzcc = 0;
            int aftqzc = 0;
            int aftqzsl = 0;
            string[,] radQZ = new string[1, 4];
            double[,] T_N_L_befQZc = new double[ListCzxDa.Count, 13];
            double[,] T_N_L_aftQZc = new double[ListCzxDa.Count, 13];
            double detax_temp = 0.0; // 高斯坐标系下的X差
            double detay_temp = 0.0; // 高斯坐标系下的Y差
            double d_temp = 0.0;
            double azimuth_temp = 0.0;
            double angle_temp = 0.0;
            double jdH_temp = 0.0;
            double x_temp = 0.0; // 切线坐标系下测点的x坐标
            double y_temp = 0.0; // 切线坐标系下测点的y坐标
            double lx_temp = 0.0;
            double ly_temp = 0.0;
            double l_temp = 0.0;
            double mileage_temp1 = 0.0;
            double[,] Newton_L_temp = new double[1, 13];
            double N_L_L0; // 储存初值

            string[,] zx_temp = new string[ListCzxDa.Count, 1];
            string[,] dis_temp = new string[ListCzxDa.Count, 2];
            double[,] func_singleone = new double[1, 2]; // 临时变量供函数输出值
            string[,] qx_calculate = new string[ListCzxDa.Count, 2];
            double[,] XY_Simpson = new double[1, 2];

            for (int i = 0; i < ListCzxDa.Count; i++)
            {
                CalDevData CalDevDa = new CalDevData();

                ListCalDevDa.Add(CalDevDa);

                detax_temp = ListCzxDa[i].X - ListInfoDa[1].X;
                detay_temp = ListCzxDa[i].Y - ListInfoDa[1].Y;
                d_temp = Math.Sqrt((detax_temp) * (detax_temp) + (detay_temp) * (detay_temp)); // 测点与交点的距离
                azimuth_temp = Math_lb.Math_AZIMUTH(detax_temp, detay_temp);

                if (Math_lb.Math_ANGLE(azimuth_temp, azimuth_jd21) < Math_lb.Math_ANGLE(azimuth_jd21, azimuth_JQ)) // 若测点不在内侧，其与JD21方向的夹角也不会超过偏转角的1/2
                {
                    angle_temp = Math_lb.Math_ANGLE(azimuth_temp, azimuth_jd21);
                    if ((d_temp * (Math.Cos(angle_temp))) >= Chatra_lb.T)
                    {
                        ListCalDevDa[befqzsl].Name = ListCzxDa[i].Name;
                        ListCalDevDa[befqzsl].X = ListCzxDa[i].X;
                        ListCalDevDa[befqzsl].Y = ListCzxDa[i].Y;

                        mileage_temp1 = jd_mileage - d_temp * Math.Cos(angle_temp);
                        ListCalDevDa[befqzsl].PMil = mileage_temp1; // 里程
                        ListCalDevDa[befqzsl].Length = d_temp * (Math.Cos(angle_temp)) - Chatra_lb.T;			 
                        lx_temp = -(d_temp * (Math.Cos(angle_temp)) - Chatra_lb.T);
                        ly_temp = 0.0;
                        func_singleone = Math_lb.Math_xytoXYFCoordinateTransformation(Chatra_lb.ZH_XY[0, 0], Chatra_lb.ZH_XY[0, 1], lx_temp, ly_temp, azimuth_jd12, ListInfoDa[1].K);
                        ListCalDevDa[befqzsl].DX = func_singleone[0, 0];
                        ListCalDevDa[befqzsl].DY = func_singleone[0, 1];
                        if ((Math_lb.Math_ANGLE(azimuth_temp, azimuth_jd21)) < (Math_lb.Math_ANGLE(azimuth_jd21, azimuth_jd23)) && (Math_lb.Math_ANGLE(azimuth_temp, azimuth_jd23)) < (Math_lb.Math_ANGLE(azimuth_jd21, azimuth_jd23))) // 判断实测点是否在JD(i)到JD(i-1)与JD(i)到JD(i+1)所组成的夹角之间
                        {
                            ListCalDevDa[befqzsl].VDevVal = 1000.0 * d_temp * (Math.Sin(angle_temp)); // 横向偏差为正(mm)
                        }
                        else
                        {
                            ListCalDevDa[befqzsl].VDevVal = -1000.0 * d_temp * (Math.Sin(angle_temp)); // 横向偏差为负(mm)
                        }
                        ListCalDevDa[befqzsl].TAzi = azimuth_jd12;
                        ListCalDevDa[befqzsl].Poslabel = "Line"; // 位置标签
                        befqzsl++;
                    }
                    else
                    {
                        if ((Math_lb.Math_ANGLE(azimuth_temp, azimuth_jd21)) < (Math_lb.Math_ANGLE(azimuth_jd21, azimuth_jd23)) && (Math_lb.Math_ANGLE(azimuth_temp, azimuth_jd23)) < (Math_lb.Math_ANGLE(azimuth_jd21, azimuth_jd23))) // 判断实测点是否在JD(i)到JD(i-1)与JD(i)到JD(i+1)所组成的夹角之间
                        {
                            jdH_temp = (Chatra_lb.T - Chatra_lb.HY_xy[0, 0] - Chatra_lb.HY_xy[0, 1] * (Math.Tan(Chatra_lb.beta0))) * (Math.Sin((Math.PI / 2.0) + Chatra_lb.beta0)) / (Math.Sin((Math.PI / 2.0) - angle_temp - Chatra_lb.beta0));
                        }
                        else
                        {
                            jdH_temp = (Chatra_lb.T - Chatra_lb.HY_xy[0, 0] - Chatra_lb.HY_xy[0, 1] * (Math.Tan(Chatra_lb.beta0))) * (Math.Sin((Math.PI / 2.0) - Chatra_lb.beta0)) / (Math.Sin((Math.PI / 2.0) - angle_temp + Chatra_lb.beta0));
                        }
                        if (d_temp > jdH_temp)
                        {
                            ListCalDevDa[befqzsl + befqzc].Name = ListCzxDa[i].Name;
                            ListCalDevDa[befqzsl + befqzc].X = ListCzxDa[i].X;
                            ListCalDevDa[befqzsl + befqzc].Y = ListCzxDa[i].Y;

                            func_singleone = Math_lb.Math_XYtoxyFCoordinateTransformation(Chatra_lb.ZH_XY[0, 0], Chatra_lb.ZH_XY[0, 1], ListCzxDa[i].X, ListCzxDa[i].Y, azimuth_jd12, ListInfoDa[1].K);
                            x_temp = func_singleone[0, 0];
                            y_temp = func_singleone[0, 1];
                            ListCalDevDa[befqzsl + befqzc].TCorx = x_temp;
                            ListCalDevDa[befqzsl + befqzc].TCory = y_temp; // 计算测点切线坐标系下坐标
                            if (befqzc == 0)
                            {
                                dis_temp[i, 0] = ListCzxDa[i].Name;
                                dis_temp[i, 1] = Convert.ToString(Math.Sqrt((ListCzxDa[i].X - Chatra_lb.ZH_XY[0, 0]) * (ListCzxDa[i].X - Chatra_lb.ZH_XY[0, 0]) + (ListCzxDa[i].Y - Chatra_lb.ZH_XY[0, 1]) * (ListCzxDa[i].Y - Chatra_lb.ZH_XY[0, 1])));
                            }
                            else
                            {
                                dis_temp[i, 0] = ListCzxDa[i].Name;
                                dis_temp[i, 1] = Convert.ToString(Convert.ToDouble(dis_temp[i - 1, 1]) + Math.Sqrt((ListCzxDa[i].X - ListCzxDa[i - 1].X) * (ListCzxDa[i].X - ListCzxDa[i - 1].X) + (ListCzxDa[i].Y - ListCzxDa[i - 1].Y) * (ListCzxDa[i].Y - ListCzxDa[i - 1].Y)));
                            }
                            //=================================================================
                            //
                            N_L_L0 = Convert.ToDouble(dis_temp[i, 1]);
                            Newton_L_temp = Latdev_lb.Lateral_TraCURVE_Dis(Chatra_lb.Rad, Chatra_lb.l0, x_temp, y_temp, N_L_L0, N_L_DETA); // 存储迭代次数及迭代值
                            for (int j = 0; j < 13; j++)
                            {
                                T_N_L_befQZc[befqzc, j] = Newton_L_temp[0, j];
                            }
                            l_temp = T_N_L_befQZc[befqzc, 0];
                            ListCalDevDa[befqzsl + befqzc].TAzi = Math_lb.Math_ANGLEtoAZIMUTH(azimuth_jd12 + ListInfoDa[1].K * l_temp * l_temp / (2.0 * Chatra_lb.Rad * Chatra_lb.l0));
                            //
                            //=================================================================
                            ListCalDevDa[befqzsl + befqzc].Length = l_temp;
                            mileage_temp1 = jd_mileage - Chatra_lb.T + l_temp;
                            ListCalDevDa[befqzsl + befqzc].PMil = mileage_temp1;
                            func_singleone = Chatra_lb.CharaTrack_TangentxyT(Chatra_lb.l0, Chatra_lb.Rad, l_temp);
                            lx_temp = func_singleone[0, 0];
                            ly_temp = func_singleone[0, 1];
                            //=================================================================
                            //
                            if (Math_lb.Math_AZIMUTH(ly_temp, -lx_temp * ListInfoDa[1].K) > Math_lb.Math_AZIMUTH(y_temp, -x_temp * ListInfoDa[1].K))
                            {
                                ListCalDevDa[befqzsl + befqzc].VDevVal = Math.Sqrt((lx_temp - x_temp) * (lx_temp - x_temp) + (ly_temp - y_temp) * (ly_temp - y_temp)) * (-1000.0 * ListInfoDa[1].K); // mm
                            }
                            else if (Math_lb.Math_AZIMUTH(ly_temp, -lx_temp * ListInfoDa[1].K) < Math_lb.Math_AZIMUTH(y_temp, -x_temp * ListInfoDa[1].K))
                            {
                                ListCalDevDa[befqzsl + befqzc].VDevVal = Math.Sqrt((lx_temp - x_temp) * (lx_temp - x_temp) + (ly_temp - y_temp) * (ly_temp - y_temp)) * (1000.0 * ListInfoDa[1].K); // mm
                            }
                            else // 万一不在上述范围内,给出指示值以方便检查
                            {
                                ListCalDevDa[befqzsl + befqzc].VDevVal = 10000000000;
                            }
                            //
                            //=================================================================
                            func_singleone = Math_lb.Math_xytoXYFCoordinateTransformation(Chatra_lb.ZH_XY[0, 0], Chatra_lb.ZH_XY[0, 1], lx_temp, ly_temp, azimuth_jd12, ListInfoDa[1].K);
                            ListCalDevDa[befqzsl + befqzc].DX = func_singleone[0, 0];
                            ListCalDevDa[befqzsl + befqzc].DY = func_singleone[0, 1];
                            //=================================================================
                            ListCalDevDa[befqzsl + befqzc].Poslabel = "FirstTransitionCurve"; // 位置标签
                            befqzc++;
                        }
                        else
                        {
                            ListCalDevDa[befqzsl + befqzc + befqzcc].Name = ListCzxDa[i].Name;
                            ListCalDevDa[befqzsl + befqzc + befqzcc].X = ListCzxDa[i].X;
                            ListCalDevDa[befqzsl + befqzc + befqzcc].Y = ListCzxDa[i].Y;

                            func_singleone = Math_lb.Math_XYtoxyFCoordinateTransformation(Chatra_lb.ZH_XY[0, 0], Chatra_lb.ZH_XY[0, 1], ListCzxDa[i].X, ListCzxDa[i].Y, azimuth_jd12, ListInfoDa[1].K);
                            x_temp = func_singleone[0, 0];
                            y_temp = func_singleone[0, 1];
                            ListCalDevDa[befqzsl + befqzc + befqzcc].TCorx = x_temp;
                            ListCalDevDa[befqzsl + befqzc + befqzcc].TCory = y_temp;

                            if ((befqzcc == 0) && (i == 0))
                            {
                                dis_temp[i, 0] = ListCzxDa[i].Name;
                                dis_temp[i, 1] = Convert.ToString(Math.Sqrt((ListCzxDa[i].X - Chatra_lb.ZH_XY[0, 0]) * (ListCzxDa[i].X - Chatra_lb.ZH_XY[0, 0]) + (ListCzxDa[i].Y - Chatra_lb.ZH_XY[0, 1]) * (ListCzxDa[i].Y - Chatra_lb.ZH_XY[0, 1])));
                            }
                            else
                            {
                                dis_temp[i, 0] = ListCzxDa[i].Name;
                                dis_temp[i, 1] = Convert.ToString(Convert.ToDouble(dis_temp[i - 1, 1]) + Math.Sqrt((ListCzxDa[i].X - ListCzxDa[i - 1].X) * (ListCzxDa[i].X - ListCzxDa[i - 1].X) + (ListCzxDa[i].Y - ListCzxDa[i - 1].Y) * (ListCzxDa[i].Y - ListCzxDa[i - 1].Y)));
                            }
                            //=================================================================
                            //
                            l_temp = Latdev_lb.Lateral_CirCURVE(Chatra_lb.Rad, Chatra_lb.l0, Chatra_lb.beta0, Chatra_lb.m, Chatra_lb.p, x_temp, y_temp);
                            ListCalDevDa[befqzsl + befqzc + befqzcc].TAzi = Math_lb.Math_ANGLEtoAZIMUTH(azimuth_HY + ListInfoDa[1].K * (l_temp - Chatra_lb.l0) / Chatra_lb.Rad);
                            //
                            //=================================================================
                            ListCalDevDa[befqzsl + befqzc + befqzcc].Length = l_temp;
                            mileage_temp1 = jd_mileage - Chatra_lb.T + l_temp;
                            ListCalDevDa[befqzsl + befqzc + befqzcc].PMil = mileage_temp1;
                            func_singleone = Chatra_lb.CharaTrack_TangentxyC(Chatra_lb.l0, Chatra_lb.Rad, l_temp, ListInfoDa[1].erfa);
                            lx_temp = func_singleone[0, 0];
                            ly_temp = func_singleone[0, 1];
                            //=================================================================
                            //
                            if (Math_lb.Math_AZIMUTH(ly_temp, -lx_temp * ListInfoDa[1].K) > Math_lb.Math_AZIMUTH(y_temp, -x_temp * ListInfoDa[1].K))
                            { 
                                ListCalDevDa[befqzsl + befqzc + befqzcc].VDevVal = Math.Sqrt((lx_temp - x_temp) * (lx_temp - x_temp) + (ly_temp - y_temp) * (ly_temp - y_temp)) * (-1000.0 * ListInfoDa[1].K); // mm
                            }
                            else if (Math_lb.Math_AZIMUTH(ly_temp, -lx_temp * ListInfoDa[1].K) < Math_lb.Math_AZIMUTH(y_temp, -x_temp * ListInfoDa[1].K))
                            {
                                ListCalDevDa[befqzsl + befqzc + befqzcc].VDevVal = Math.Sqrt((lx_temp - x_temp) * (lx_temp - x_temp) + (ly_temp - y_temp) * (ly_temp - y_temp)) * (1000.0 * ListInfoDa[1].K); // mm
                            }
                            else // 万一不在上述范围内,给出指示值以方便检查
                            {
                                ListCalDevDa[befqzsl + befqzc + befqzcc].VDevVal = 10000000000;
                            }
                            //
                            //=================================================================
                            func_singleone = Math_lb.Math_xytoXYFCoordinateTransformation(Chatra_lb.ZH_XY[0, 0], Chatra_lb.ZH_XY[0, 1], lx_temp, ly_temp, azimuth_jd12, ListInfoDa[1].K);
                            ListCalDevDa[befqzsl + befqzc + befqzcc].DX = func_singleone[0, 0];
                            ListCalDevDa[befqzsl + befqzc + befqzcc].DY = func_singleone[0, 1];
                            //=================================================================
                            ListCalDevDa[befqzsl + befqzc + befqzcc].Poslabel = "CircularCurve"; // 位置标签
                            befqzcc++;
                        }
                    }
                }
                else if (Math_lb.Math_ANGLE(azimuth_temp, azimuth_jd23) < Math_lb.Math_ANGLE(azimuth_JQ, azimuth_jd23)) // 若测点不在内侧，其与JD23方向的夹角也不会超过偏转角的1/2
                {
                    angle_temp = Math_lb.Math_ANGLE(azimuth_temp, azimuth_jd23);
                    if ((d_temp * (Math.Cos(angle_temp))) >= Chatra_lb.T)
                    {
                        ListCalDevDa[befqzsl + befqzc + befqzcc + aftqzcc + aftqzc + aftqzsl].Name = ListCzxDa[i].Name;
                        ListCalDevDa[befqzsl + befqzc + befqzcc + aftqzcc + aftqzc + aftqzsl].X = ListCzxDa[i].X;
                        ListCalDevDa[befqzsl + befqzc + befqzcc + aftqzcc + aftqzc + aftqzsl].Y = ListCzxDa[i].Y;

                        mileage_temp1 = jd_mileage + d_temp * (Math.Cos(angle_temp)) - Chatra_lb.q;
                        ListCalDevDa[befqzsl + befqzc + befqzcc + aftqzcc + aftqzc + aftqzsl].PMil = mileage_temp1; // 里程
                        ListCalDevDa[befqzsl + befqzc + befqzcc + aftqzcc + aftqzc + aftqzsl].Length = d_temp * Math.Cos(angle_temp) - Chatra_lb.T;
                        lx_temp = -(d_temp * (Math.Cos(angle_temp)) - Chatra_lb.T);
                        ly_temp = 0.0;
                        func_singleone = Math_lb.Math_xytoXYSCoordinateTransformation(Chatra_lb.HZ_XY[0, 0], Chatra_lb.HZ_XY[0, 1], lx_temp, ly_temp, azimuth_jd32, ListInfoDa[1].K);
                        ListCalDevDa[befqzsl + befqzc + befqzcc + aftqzcc + aftqzc + aftqzsl].DX = func_singleone[0, 0];
                        ListCalDevDa[befqzsl + befqzc + befqzcc + aftqzcc + aftqzc + aftqzsl].DY = func_singleone[0, 1];
                        if ((Math_lb.Math_ANGLE(azimuth_temp, azimuth_jd21)) < (Math_lb.Math_ANGLE(azimuth_jd21, azimuth_jd23)) && (Math_lb.Math_ANGLE(azimuth_temp, azimuth_jd23)) < (Math_lb.Math_ANGLE(azimuth_jd21, azimuth_jd23))) // 判断实测点是否在JD(i)到JD(i-1)与JD(i)到JD(i+1)所组成的夹角之间
                        {
                            ListCalDevDa[befqzsl + befqzc + befqzcc + aftqzcc + aftqzc + aftqzsl].VDevVal = 1000.0 * d_temp * (Math.Sin(angle_temp)); //  mm
                        }
                        else
                        {
                            ListCalDevDa[befqzsl + befqzc + befqzcc + aftqzcc + aftqzc + aftqzsl].VDevVal = -1000.0 * d_temp * (Math.Sin(angle_temp)); //  mm
                        }
                        ListCalDevDa[befqzsl + befqzc + befqzcc + aftqzcc + aftqzc + aftqzsl].TAzi = azimuth_jd23;
                        ListCalDevDa[befqzsl + befqzc + befqzcc + aftqzcc + aftqzc + aftqzsl].Poslabel = "Line"; // 位置标签
                        aftqzsl++;
                    }
                    else
                    {
                        if ((Math_lb.Math_ANGLE(azimuth_temp, azimuth_jd21)) < (Math_lb.Math_ANGLE(azimuth_jd21, azimuth_jd23)) && (Math_lb.Math_ANGLE(azimuth_temp, azimuth_jd23)) < (Math_lb.Math_ANGLE(azimuth_jd21, azimuth_jd23)))//判断实测点是否在JD(i)到JD(i-1)与JD(i)到JD(i+1)所组成的夹角之间
                        {
                            jdH_temp = (Chatra_lb.T - Chatra_lb.YH_xy[0, 0] - Chatra_lb.YH_xy[0, 1] * (Math.Tan(Chatra_lb.beta0))) * (Math.Sin((Math.PI / 2.0) + Chatra_lb.beta0)) / (Math.Sin((Math.PI / 2.0) - angle_temp - Chatra_lb.beta0));
                        }
                        else
                        {
                            jdH_temp = (Chatra_lb.T - Chatra_lb.YH_xy[0, 0] - Chatra_lb.YH_xy[0, 1] * (Math.Tan(Chatra_lb.beta0))) * (Math.Sin((Math.PI / 2.0) - Chatra_lb.beta0)) / (Math.Sin((Math.PI / 2.0) - angle_temp + Chatra_lb.beta0));
                        }
                        if (d_temp > jdH_temp)
                        {
                            ListCalDevDa[befqzsl + befqzc + befqzcc + aftqzcc + aftqzc].Name = ListCzxDa[i].Name;
                            ListCalDevDa[befqzsl + befqzc + befqzcc + aftqzcc + aftqzc].X = ListCzxDa[i].X;
                            ListCalDevDa[befqzsl + befqzc + befqzcc + aftqzcc + aftqzc].Y = ListCzxDa[i].Y;

                            func_singleone = Math_lb.Math_XYtoxySCoordinateTransformation(Chatra_lb.HZ_XY[0, 0], Chatra_lb.HZ_XY[0, 1], ListCzxDa[i].X, ListCzxDa[i].Y, azimuth_jd32, ListInfoDa[1].K);
                            x_temp = func_singleone[0, 0];
                            y_temp = func_singleone[0, 1];
                            ListCalDevDa[befqzsl + befqzc + befqzcc + aftqzcc + aftqzc].TCorx = x_temp;
                            ListCalDevDa[befqzsl + befqzc + befqzcc + aftqzcc + aftqzc].TCory = y_temp; // 计算测点切线坐标系下坐标
                            if (aftqzc == 0)
                            {
                                dis_temp[i, 0] = ListCzxDa[i].Name;
                                dis_temp[i, 1] = Convert.ToString(Math.Sqrt((ListCzxDa[i].X - Chatra_lb.YH_XY[0, 0]) * (ListCzxDa[i].X - Chatra_lb.YH_XY[0, 0]) + (ListCzxDa[i].Y - Chatra_lb.YH_XY[0, 1]) * (ListCzxDa[i].Y - Chatra_lb.YH_XY[0, 1])));
                            }
                            else
                            {
                                dis_temp[i, 0] = ListCzxDa[i].Name;
                                dis_temp[i, 1] = Convert.ToString(Convert.ToDouble(dis_temp[i - 1, 1]) + Math.Sqrt((ListCzxDa[i].X - ListCzxDa[i - 1].X) * (ListCzxDa[i].X - ListCzxDa[i - 1].X) + (ListCzxDa[i].Y - ListCzxDa[i - 1].Y) * (ListCzxDa[i].Y - ListCzxDa[i - 1].Y)));
                            }
                            //=================================================================
                            //
                            N_L_L0 = Chatra_lb.l0 - Convert.ToDouble(dis_temp[i, 1]);
                            Newton_L_temp = Latdev_lb.Lateral_TraCURVE_Dis(Chatra_lb.Rad, Chatra_lb.l0, x_temp, y_temp, N_L_L0, N_L_DETA);
                            for (int j = 0; j < 13; j++)
                            {
                                T_N_L_aftQZc[aftqzc, j] = Newton_L_temp[0, j];
                            }
                            l_temp = T_N_L_aftQZc[aftqzc, 0]; // 迭代值
                            ListCalDevDa[befqzsl + befqzc + befqzcc + aftqzcc + aftqzc].TAzi = Math_lb.Math_ANGLEtoAZIMUTH(azimuth_YH + ListInfoDa[1].K * ((Chatra_lb.l0 - l_temp) / Chatra_lb.Rad - (Chatra_lb.l0 - l_temp) * (Chatra_lb.l0 - l_temp) / (2.0 * Chatra_lb.Rad * Chatra_lb.l0)));
                            //=================================================================
                            ListCalDevDa[befqzsl + befqzc + befqzcc + aftqzcc + aftqzc].Length = l_temp; // 储存曲线长
                            mileage_temp1 = jd_mileage - Chatra_lb.T + Chatra_lb.L - l_temp;
                            ListCalDevDa[befqzsl + befqzc + befqzcc + aftqzcc + aftqzc].PMil = mileage_temp1; // 计算里程
                            func_singleone = Chatra_lb.CharaTrack_TangentxyT(Chatra_lb.l0, Chatra_lb.Rad, l_temp);
                            lx_temp = func_singleone[0, 0];
                            ly_temp = func_singleone[0, 1];
                            //=================================================================
                            //
                            if (Math_lb.Math_AZIMUTH(ly_temp, lx_temp * ListInfoDa[1].K) > Math_lb.Math_AZIMUTH(y_temp, x_temp * ListInfoDa[1].K))
                            {
                                ListCalDevDa[befqzsl + befqzc + befqzcc + aftqzcc + aftqzc].VDevVal = Math.Sqrt((lx_temp - x_temp) * (lx_temp - x_temp) + (ly_temp - y_temp) * (ly_temp - y_temp)) * (1000.0 * ListInfoDa[1].K); // mm
                            }
                            else if (Math_lb.Math_AZIMUTH(ly_temp, lx_temp * ListInfoDa[1].K) < Math_lb.Math_AZIMUTH(y_temp, x_temp * ListInfoDa[1].K))
                            {
                                ListCalDevDa[befqzsl + befqzc + befqzcc + aftqzcc + aftqzc].VDevVal = Math.Sqrt((lx_temp - x_temp) * (lx_temp - x_temp) + (ly_temp - y_temp) * (ly_temp - y_temp)) * (-1000.0 * ListInfoDa[1].K); // mm
                            }
                            else // 万一不在上述范围内,给出指示值以方便检查
                            {
                                ListCalDevDa[befqzsl + befqzc + befqzcc + aftqzcc + aftqzc].VDevVal = 10000000000;
                            }
                            //
                            //=================================================================
                            func_singleone = Math_lb.Math_xytoXYSCoordinateTransformation(Chatra_lb.HZ_XY[0, 0], Chatra_lb.HZ_XY[0, 1], lx_temp, ly_temp, azimuth_jd32, ListInfoDa[1].K);
                            ListCalDevDa[befqzsl + befqzc + befqzcc + aftqzcc + aftqzc].DX = func_singleone[0, 0];
                            ListCalDevDa[befqzsl + befqzc + befqzcc + aftqzcc + aftqzc].DY = func_singleone[0, 1];
                            //=================================================================
                            ListCalDevDa[befqzsl + befqzc + befqzcc + aftqzcc + aftqzc].Poslabel = "SecondTransitionCurve"; // 位置标签
                            aftqzc++;
                        }
                        else
                        {
                            ListCalDevDa[befqzsl + befqzc + befqzcc + aftqzcc].Name = ListCzxDa[i].Name;
                            ListCalDevDa[befqzsl + befqzc + befqzcc + aftqzcc].X = ListCzxDa[i].X;
                            ListCalDevDa[befqzsl + befqzc + befqzcc + aftqzcc].Y = ListCzxDa[i].Y;

                            func_singleone = Math_lb.Math_XYtoxyFCoordinateTransformation(Chatra_lb.ZH_XY[0, 0], Chatra_lb.ZH_XY[0, 1], ListCzxDa[i].X, ListCzxDa[i].Y, azimuth_jd12, ListInfoDa[1].K);
                            x_temp = func_singleone[0, 0];
                            y_temp = func_singleone[0, 1];
                            ListCalDevDa[befqzsl + befqzc + befqzcc + aftqzcc].TCorx = x_temp;
                            ListCalDevDa[befqzsl + befqzc + befqzcc + aftqzcc].TCory = y_temp; // 计算测点切线坐标系下坐标
                            if ((aftqzcc == 0) && (i == 0))
                            {
                                dis_temp[i, 0] = ListCzxDa[i].Name;
                                dis_temp[i, 1] = Convert.ToString(Math.Sqrt((ListCzxDa[i].X - Chatra_lb.ZH_XY[0, 0]) * (ListCzxDa[i].X - Chatra_lb.ZH_XY[0, 0]) + (ListCzxDa[i].Y - Chatra_lb.ZH_XY[0, 1]) * (ListCzxDa[i].Y - Chatra_lb.ZH_XY[0, 1])));
                            }
                            else
                            {
                                dis_temp[i, 0] = ListCzxDa[i].Name;
                                dis_temp[i, 1] = Convert.ToString(Convert.ToDouble(dis_temp[i - 1, 1]) + Math.Sqrt((ListCzxDa[i].X - ListCzxDa[i - 1].X) * (ListCzxDa[i].X - ListCzxDa[i - 1].X) + (ListCzxDa[i].Y - ListCzxDa[i - 1].Y) * (ListCzxDa[i].Y - ListCzxDa[i - 1].Y)));
                            }
                            //=================================================================
                            //
                            l_temp = Latdev_lb.Lateral_CirCURVE(Chatra_lb.Rad, Chatra_lb.l0, Chatra_lb.beta0, Chatra_lb.m, Chatra_lb.p, x_temp, y_temp);
                            ListCalDevDa[befqzsl + befqzc + befqzcc + aftqzcc].TAzi = Math_lb.Math_ANGLEtoAZIMUTH(azimuth_HY + ListInfoDa[1].K * (l_temp - Chatra_lb.l0) / Chatra_lb.Rad);
                            //
                            //=================================================================
                            ListCalDevDa[befqzsl + befqzc + befqzcc + aftqzcc].Length = l_temp; // 曲线长
                            mileage_temp1 = jd_mileage - Chatra_lb.T + l_temp;
                            ListCalDevDa[befqzsl + befqzc + befqzcc + aftqzcc].PMil = mileage_temp1;
                            func_singleone = Chatra_lb.CharaTrack_TangentxyC(Chatra_lb.l0, Chatra_lb.Rad, l_temp, ListInfoDa[1].erfa);
                            lx_temp = func_singleone[0, 0];
                            ly_temp = func_singleone[0, 1];
                            //=================================================================
                            //
                            if (Math_lb.Math_AZIMUTH(ly_temp, -lx_temp * ListInfoDa[1].K) > Math_lb.Math_AZIMUTH(y_temp, -x_temp * ListInfoDa[1].K))
                            {
                                ListCalDevDa[befqzsl + befqzc + befqzcc + aftqzcc].VDevVal = Math.Sqrt((lx_temp - x_temp) * (lx_temp - x_temp) + (ly_temp - y_temp) * (ly_temp - y_temp)) * (-1000.0 * ListInfoDa[1].K); // mm
                            }
                            else if (Math_lb.Math_AZIMUTH(ly_temp, -lx_temp * ListInfoDa[1].K) < Math_lb.Math_AZIMUTH(y_temp, -x_temp * ListInfoDa[1].K))
                            {
                                ListCalDevDa[befqzsl + befqzc + befqzcc + aftqzcc].VDevVal = Math.Sqrt((lx_temp - x_temp) * (lx_temp - x_temp) + (ly_temp - y_temp) * (ly_temp - y_temp)) * (1000.0 * ListInfoDa[1].K); // mm
                            }
                            else // 万一不在上述范围内,给出指示值以方便检查
                            {
                                ListCalDevDa[befqzsl + befqzc + befqzcc + aftqzcc].VDevVal = 10000000000;
                            }
                            //
                            //=================================================================
                            func_singleone = Math_lb.Math_xytoXYFCoordinateTransformation(Chatra_lb.ZH_XY[0, 0], Chatra_lb.ZH_XY[0, 1], lx_temp, ly_temp, azimuth_jd12, ListInfoDa[1].K);
                            ListCalDevDa[befqzsl + befqzc + befqzcc + aftqzcc].DX = func_singleone[0, 0];
                            ListCalDevDa[befqzsl + befqzc + befqzcc + aftqzcc].DY = func_singleone[0, 1];
                            //=================================================================
                            ListCalDevDa[befqzsl + befqzc + befqzcc + aftqzcc].Poslabel = "CircularCurve"; // 位置标签
                            aftqzcc++;
                        }
                    }
                }
            }

            double[,] N_L_befQZc = new double[befqzc, 13]; // 存储迭代次数及值*******
            double[,] N_L_aftQZc = new double[aftqzc, 13];
            for (int i = 0; i < befqzc; i++)
            {
                for (int j = 0; j < 13; j++)
                {
                    N_L_befQZc[i, j] = T_N_L_befQZc[i, j];
                }
            }
            for (int i = 0; i < aftqzc; i++)
            {
                for (int j = 0; j < 13; j++)
                {
                    N_L_aftQZc[i, j] = T_N_L_aftQZc[i, j];
                }
            }

            return ListCalDevDa;
        }
    }
}
