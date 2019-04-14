using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPP
{
    public class pppcmn
    {
        const double OMGE = 7.2921151467E-5;    /* earth angular velocity (IS-GPS) (rad/s) */ //地球自转角速度
        const int norder = 10;//9
        const double CLIGHT = 299792458.0;
        const double D2R = Math.PI / 180;//度转弧度
        const double AS2R = D2R / 3600.0;//
        const double AU = 149597870691.0;      /* 1 AU (m) */
        const double RE_WGS84 = 6378137.0;           /* earth semimajor axis (WGS84) (m) */
        const double R2D = (180.0 / Math.PI);          /* rad to deg */
        const double GME = 3.986004415E+14; /* earth gravitational constant */
        const double GMS = 1.327124E+20;    /* sun gravitational constant */
        const double GMM = 4.902801E+12;  /* moon gravitational constant */
        const double EXTERR_CLK = 1E-3;            /* extrapolation error for clock (m/s) */
        const double EXTERR_EPH = 5E-7;        /* extrapolation error for ephem (m/s^2) */
        const double STD_BRDCCLK = 30.0;          /* error of broadcast clock (m) */

        static double[] GPS_fre = new double[2] { 1.57542E9, 1.22760E9 };

        /// <summary>
        /// 计算卫星高度角
        /// </summary>
        /// <param name="sta">测站xyz坐标</param>
        /// <param name="satpos">卫星xyz坐标</param>
        /// <returns>返回卫星方位角和高度角</returns>
        public static double satel(double[] sta, double[] satpos, double[] azel)
        {
            double el = Math.PI / 2, az = 0.0;
            matrix vector = new matrix(3, 1); matrix enu = new matrix(3, 1); matrix pos = new matrix(3, 1);
            matrix xyz = new matrix(3, 1);
            xyz[1, 1] = sta[0]; xyz[2, 1] = sta[1]; xyz[3, 1] = sta[2];
            vector[1, 1] = satpos[0] - sta[0]; vector[2, 1] = satpos[1] - sta[1]; vector[3, 1] = satpos[2] - sta[2];
            transcoor.ecef2pos(xyz, pos);
            if (pos[3, 1] > -RE_WGS84)
            {
                transcoor.xyz2enu(xyz, vector, enu);
                el = Math.Asin(enu[3, 1] / Math.Sqrt(matrix.dotvector(enu, enu)));
                az = Math.Atan2(enu[1, 1], enu[2, 1]);//atan2(e/n)
                if (az < 0) az += 2 * Math.PI;
            }
            //el=Math.Asin(enu[2,1]/(enu))
            if (azel != null) { azel[0] = az; azel[1] = el; }
            return el;
        }

        /// <summary>
        /// 拉格朗日插值
        /// </summary>
        /// <param name="x">插值结点</param>
        /// <param name="y">插值结点对应的值</param>
        /// <param name="t0">插值时刻</param>
        /// <returns></returns>
        public static double Lagrange(double[] x, double[] y, double t0)
        {
            double y0 = 0;

            for (int i = 0; i < x.Length; i++)
            {
                double t = 1;
                for (int j = 0; j < x.Length; j++)
                {
                    if (j != i) t = t * (t0 - x[j]) / (x[i] - x[j]);
                }
                y0 += t * y[i];
            }
            return y0;
        }
        /// <summary>
        /// 地球自转改正
        /// </summary>
        /// <param name="k">第K个卫星</param>
        /// <param name="pos">精密星历中的卫星位置</param>
        /// <param name="p">返回改正后的卫星位置</param>
        /// <param name="dt">信号传播时间</param>
        public static void EarhRotation(int k, double[] pos, double[][] p, double dt)
        {
            double sinl, cosl;
            sinl = Math.Sin(OMGE * dt); cosl = Math.Cos(OMGE * dt);
            p[k][0] = pos[0] * cosl - pos[1] * sinl;
            p[k][1] = pos[0] * sinl + pos[1] * cosl;
            p[k][2] = pos[2];
        }

      
        public static void combsp3(sp3_t s, sat_t sat)
        {
           // for (int i = 0; i < 3; i++)
           // {
               /* if (i == 0)//观测时间的前一天
                {
                    foreach (var v in s[i].sp3_b)
                    {
                        if (v.t.calend[3] >= 20)
                        {
                            satb ss = new satb();
                            ss.prn = v.prn; ss.t = v.t;
                            for (int j = 0; j < 3; j++)
                            {
                                ss.xyz[j] = v.xyzt[j];
                                ss.std[j] = v.std[j];
                            }
                            //  ss.xyz[0] = v.xyzt[0];ss.xyz[1] = v.xyzt[1];ss.xyz[2] = v.xyzt[2];
                            // sat.satdata.Add(ss);
                        }
                    }
                    continue;
                }
                if (i == 2)//观测文件的后一天
                {
                    foreach (var v in s[i].sp3_b)
                    {
                        if (v.t.calend[3] <= 3)
                        {
                            satb ss = new satb();
                            ss.prn = v.prn; ss.t = v.t;
                            for (int j = 0; j < 3; j++)
                            {
                                ss.xyz[j] = v.xyzt[j];
                                ss.std[j] = v.std[j];
                            }
                            // ss.xyz[0] = v.xyzt[0]; ss.xyz[1] = v.xyzt[1]; ss.xyz[2] = v.xyzt[2];
                            //sat.satdata.Add(ss);
                        }
                    }
                    continue;
                }*/
                foreach (var v in s.sp3_b)
                {
                    satb ss = new satb();
                    ss.prn = v.prn; ss.t = v.t; ss.rtkt = v.rtkt;
                    for (int j = 0; j < 3; j++)
                    {
                        ss.xyz[j] = v.xyzt[j];
                        ss.std[j] = v.std[j];
                    }
                    //ss.xyz[0] = v.xyzt[0]; ss.xyz[1] = v.xyzt[1]; ss.xyz[2] = v.xyzt[2];
                    sat.satdata.Add(ss);

                }
            //}
        }
        /*使用精密星历和钟差文件计算观测历元的卫星卫星、钟差*/
        public static void satpos(List<obs_s> obssat, nav_t nav, sat_t sat, clk_t clk, pcv_t pcv, double[][] rs, double[][] dts, double[] var, int[] svh)
        {
            //obssat是这个历元所有卫星的观测信息
            double dt, tt = 1E-3;
            double[] rss = new double[3], rst = new double[3], dts1 = new double[1], dts2 = new double[1], dant = new double[3];
            double[] vare = new double[1];
            rtktime[] time = new rtktime[32];
            int k = 0;
            for (int i = 0; i < obssat.Count; i++)
            {
                time tran = new time();
                tran.gpsec = obssat[0].t.gpsec;
                for (int j = 0; j < 6; j++) tran.calend[j] = obssat[0].t.calend[j];
                foreach (var v in obssat[i].type_value)
                {
                    if (v.type == "C1" || v.type == "P2" || v.type == "P1")
                    {
                        if (v.value != 0)
                        {
                            tran.gpsec -= v.value / CLIGHT;
                            time[i] = rtklibcmn.timeadd(obssat[i].rtkt, -v.value / CLIGHT);
                            break;
                        }//用的是周内秒进行计算
                    }
                }
                dt = navclk(nav, obssat[i], tran, time[i]);//由广播星历计算的卫星钟差
                tran.gpsec -= dt; //卫星信号发射时刻
                time[i] = rtklibcmn.timeadd(time[i], -dt);
                if (satpos_clk(tran, time[i], obssat[i], sat, clk, rss, dts1, vare) == 0)
                {
                    svh[i] = -1;
                    continue;
                };//这里的sat是SP3星历组合起来的卫星坐标信息
                tran.gpsec += tt;
                time[i] = rtklibcmn.timeadd(time[i], 1E-3);
                satpos_clk(tran, time[i], obssat[i], sat, clk, rst, dts2, null);

                for (int j = 0; j < pcv.pcvdata.Count; j++)
                {
                    if (pcv.pcvdata[j].prn == obssat[i].sprn && pcv.pcvdata[j].te == null)
                    {
                        k = j; break;
                    }
                }
                //卫星天线相位中心偏差
                satanxoff(tran, time[i], rss, pcv.pcvdata[k], dant);
                for (int n = 0; n < 3; n++)
                {
                    rs[i][n] = rss[n] + dant[n];
                    rs[i][n + 3] = (rst[n] - rss[n]) / tt;
                }
                //卫星钟差的相对论效应
                if (dts1[0] != 0)
                {
                    dts[i][0] = dts1[0] - 2.0 * (rs[i][0] * rs[i][3] + rs[i][1] * rs[i][4] + rs[i][2] * rs[i][5]) / CLIGHT / CLIGHT;
                    dts[i][1] = (dts2[0] - dts1[0]) / tt;
                    var[i] = vare[0];
                }
                else
                {
                    dts[i][0] = navclk(nav, obssat[i], tran, time[i]);
                    dts[i][1] = 0;
                    var[i] = Math.Pow(STD_BRDCCLK, 2);
                }


            }


        }
        /*卫星天线相位偏差改正*/
        public static void satanxoff(time t, rtktime rtkt, double[] rs, pcvb pcv, double[] dant)//天线相位中心偏差,rs为卫星的位置和速度
        {
            double[] erpv = new double[5];//初始值为0
            double[] rsun = new double[3], r = new double[3], ez = new double[3], ey = new double[3], ex = new double[3], es = new double[3];
            double[] gmst = new double[1];
            int i, j = 0, k = 1;//对于GPS来说k=1,j=0
            double gamma, C1, C2, dant1, dant2, lam1, lam2;

            lam1 = CLIGHT / GPS_fre[0]; lam2 = CLIGHT / GPS_fre[1];
            //计算太阳在ECEF下坐标
            t.gpst2utc();//计算出t的UTC时
            sunmoonpos(t, rtklibcmn.gpst2utc(rtkt), erpv, rsun, null, gmst);

            for (i = 0; i < 3; i++) r[i] = -rs[i];
            nomv3d(r, ez);
            for (i = 0; i < 3; i++) r[i] = rsun[i] - rs[i];
            nomv3d(r, es);
            cross3(ez, es, r);//向量的外积
            nomv3d(r, ey);
            cross3(ey, ez, ex);

            gamma = lam2 * lam2 / (lam1 * lam1);
            C1 = gamma / (gamma - 1.0);
            C2 = -1.0 / (gamma - 1.0);

            for (i = 0; i < 3; i++)
            {
                dant1 = pcv.off[j][0] * ex[i] + pcv.off[j][1] * ey[i] + pcv.off[j][2] * ez[i];
                dant2 = pcv.off[k][0] * ex[i] + pcv.off[k][1] * ey[i] + pcv.off[k][2] * ez[i];
                dant[i] = C1 * dant1 + C2 * dant2;

            }
        }
        /*卫星天线相位变化改正*/
        public static void satanxpcv(double[] rs, double[] rr, pcv_t pcv, double[] dant, string prn)//卫星天线相位变化,返回每个频率的改正值
        {
            double[] ru = new double[3], rz = new double[3], eu = new double[3], ez = new double[3];
            double nadir, cosa;
            int i, k = 0;
            for (i = 0; i < 3; i++)
            {
                ru[i] = rr[i] - rs[i];
                rz[i] = -rs[i];
            }
            nomv3d(ru, eu); nomv3d(rz, ez);
            cosa = eu[0] * ez[0] + eu[1] * ez[1] + eu[2] * ez[2];//单位向量点积
            cosa = cosa < -1.0 ? -1.0 : (cosa > 1.0 ? 1.0 : cosa);
            nadir = Math.Acos(cosa);//天底角

            for (i = 0; i < pcv.pcvdata.Count; i++)
            {
                if (pcv.pcvdata[i].prn == prn && pcv.pcvdata[i].te == null) { k = i; break; }
            }
            anxpcvmod(pcv.pcvdata[k], nadir, dant);
        }

        public static void anxpcvmod(pcvb pcv, double nadir, double[] dant)
        {
            for (int i = 0; i < 2; i++)
            {
                dant[i] = interpcv(nadir * R2D * 5.0, pcv.var[i]);
            }

        }
        public static double interpcv(double ang, double[] var)
        {
            double a = ang / 5.0;
            int i = (int)a;//取a得整数部分
            if (i < 0) return var[0]; else if (i >= var.Length) return var[var.Length - 1];//
            return var[i] * (1.0 - a + i) + var[i + 1] * (a - i);
        }
        /*接收机天线相位偏差改正*/
        public static void antxmodel(pcv_t pcv, double[] del, double[] azel, double[] dantr, string atype)
        {
            //double e[3], off[3], cosel = cos(azel[1]);
            double[] e = new double[3], off = new double[3];
            double cosel = Math.Cos(azel[1]);
            int i, j, k = 0;

            e[0] = Math.Sin(azel[0]) * cosel;
            e[1] = Math.Cos(azel[0]) * cosel;
            e[2] = Math.Sin(azel[1]);

            for (i = 0; i < pcv.pcvdata.Count; i++)
            {
                if (pcv.pcvdata[i].type == atype) { k = i; break; }//接收机天线类型
            }

            for (i = 0; i < 2; i++)
            {
                for (j = 0; j < 3; j++) off[j] = pcv.pcvdata[k].off[i][j] + del[j];
                dantr[i] = -(off[0] * e[0] + off[1] * e[1] + off[2] * e[2]) + interpcv(90.0 - azel[1] * R2D, pcv.pcvdata[k].var[i]);
            }
        }

        public static double navclk(nav_t nav, obs_s obsat, time trans, rtktime rtkt) //通过广播星历计算卫星钟差
        {
            int k = 0, i;
            double t, tmax = 7201, tmin, dt = 0;
            tmin = tmax + 1.0;
            /* double t, tmin=7201,dt=0;
             for(int i=0;i<nav.navdata.Count;i++)
             {
                 if (obsat.sprn != nav.navdata[i].prn) continue;
                 if ((t=Math.Abs(trans.gpsec - nav.navdata[i].t.gpsec))>7200) continue;
                 if (t < tmin) { k = i;tmin = t; }
             }

             t = trans.gpsec - nav.navdata[k].t.gpsec;*/

            for (i = 0; i < nav.navdata.Count; i++)
            {
                if (obsat.sprn != nav.navdata[i].prn) continue;
                // if (iode >= 0 && nav->eph[i].iode != iode) continue;
                if ((t = Math.Abs(rtklibcmn.timediff(nav.navdata[i].rtktoe, rtkt))) > tmax) continue;
                // if (iode >= 0) return nav->eph + i;
                if (t <= tmin) { k = i; tmin = t; } /* toe closest to time */
            }
            t = rtklibcmn.timediff(rtkt, nav.navdata[k].rtktoc);
            for (i = 0; i < 2; i++)
                t -= nav.navdata[k].af[0] + nav.navdata[k].af[1] * t + nav.navdata[k].af[2] * t * t;
            dt = nav.navdata[k].af[0] + nav.navdata[k].af[1] * t + nav.navdata[k].af[2] * t * t;
            return dt;
        }

        public static int satpos_clk(time trans, rtktime rtkt, obs_s obsat, sat_t sat, clk_t clkt, double[] rs, double[] dts, double[] var)
        {
            double[][] pos = new double[norder + 1][];
            double[] xt = new double[norder + 1], stde = new double[3], stdc = new double[2];
            double[] dt = new double[norder + 1];
            double vare, varc = 0.0, x1, x2, y1, y2, std;
            List<satb> sat_prn = new List<satb>();
            List<clk_b> clk_prn = new List<clk_b>();
            for (int i = 0; i < norder + 1; i++)
                pos[i] = new double[3];
            int k = 0, k1 = 0, half_order = (norder + 1) / 2, index, m, j;
            rs[0] = rs[1] = rs[2] = dts[0] = 0;
            foreach (var v in sat.satdata)//从SP3文件中提取相同PRN的卫星
            {
                if (v.prn == obsat.sprn)
                {
                    sat_prn.Add(v);
                }
            }
            if (sat_prn.Count < norder + 1) return 0;

            for (m = 0, j = 95; m < j;)
            {
                k = (m + j) / 2;
                if (rtklibcmn.timediff(sat_prn[k].rtkt, rtkt) < 0.0) m = k + 1; else j = k;
            }

            /*  for(int i=0;i<sat_prn.Count;i++)
              {
                  if( (trans.gpsec- sat_prn[i].t.gpsec) < 0) { k = i;break; } //k是内插时刻后一个卫星的索引
              }*/

            // index = k <=0? 0 : k - 1;
            index = m <= 0 ? 0 : m - 1;
            k = index - half_order;
            if (k < 0) k = 0; else if (k + norder >= sat_prn.Count) k = sat_prn.Count - norder - 1;
            for (int i = 0; i < norder + 1; i++)//找到前后各五颗卫星，将时间和坐标放入xt和pos,并加入地球自转改正
            {
                xt[i] = sat_prn[k + i].t.gpsec;
                dt[i] = rtklibcmn.timediff(sat_prn[k + i].rtkt, rtkt);
                EarhRotation(i, sat_prn[k + i].xyz, pos, dt[i]);

            }
            for (int i = 0; i < 3; i++)//X、Y、Z
            {
                double[] y = new double[norder + 1];
                for (j = 0; j < norder + 1; j++)
                    y[j] = pos[j][i];
                //rs[i] = Lagrange(xt, y, trans.gpsec);
                rs[i] = interNev(dt, y, norder + 1);
            }
            for (int i = 0; i < 3; i++)
                stde[i] = sat_prn[index].std[i];//

            std = Math.Sqrt(stde[0] * stde[0] + stde[1] * stde[1] + stde[2] * stde[2]);//轨道内插的方差
            if (dt[0] > 0.0) std += EXTERR_EPH * Math.Pow(dt[0], 2) / 2;
            else if (dt[norder] < 0.0) std += EXTERR_EPH * Math.Pow(dt[norder], 2) / 2;

            vare = Math.Pow(std, 2);

            foreach (var v in clkt.clk)//找到prn相同的所有卫星钟差
            {
                if (v.prn == obsat.sprn)
                {
                    clk_prn.Add(v);
                }
            }
            /* for(int i=0;i<clk_prn.Count;i++)
             {
                 if((trans.gpsec-clk_prn[i].t.gpsec)<0) { k1 = i;break; }
             }*/
            for (m = 0, j = clk_prn.Count - 1; m < j;)
            {
                k = (m + j) / 2;
                if (rtklibcmn.timediff(clk_prn[k].rtkt, rtkt) < 0.0) m = k + 1; else j = k;
            }
            k1 = m <= 0 ? 0 : m - 1;
            if (clk_prn.Count > 0)
            {
                x1 = rtklibcmn.timediff(rtkt, clk_prn[k1].rtkt);
                x2 = rtklibcmn.timediff(rtkt, clk_prn[k1 + 1].rtkt);
                y1 = clk_prn[k1].clock[0];
                y2 = clk_prn[k1 + 1].clock[0];
                if (x1 <= 0)
                {
                    // x1 = clk_prn[k1].t.gpsec; 
                    // x2 = clk_prn[k1 + 1].t.gpsec; 
                    //dts[0] = linerclk(x1, x2, y1, y2, trans.gpsec);
                    if ((dts[0] = y1) != 0)
                    {
                        //varc = clk_prn[k1].clock[1] * CLIGHT - EXTERR_CLK * (trans.gpsec - x1);
                        //  varc = clk_prn[k1].clock[1] * CLIGHT - EXTERR_CLK * (x1);
                        varc = clk_prn[k1].std * CLIGHT - EXTERR_CLK * (x1);
                    }//内插在精密钟差的第一个历元之前
                    else { return 0; }
                }
                // dts[0] = linerclk(clk_prn[k1].t.gpsec, clk_prn[k1 + 1].t.gpsec, clk_prn[k1].clock[0], clk_prn[k1 + 1].clock[0], trans.gpsec);
                else
                {
                    //    dts[0] = linerclk(clk_prn[k1].t.gpsec, clk_prn[k1 - 1].t.gpsec, clk_prn[k1].clock[0], clk_prn[k1 - 1].clock[0], trans.gpsec);
                    // x1 = clk_prn[k1].t.gpsec; y1 = clk_prn[k1].clock[0];
                    // x2 = clk_prn[k1 - 1].t.gpsec; y2 = clk_prn[k1 - 1].clock[0];
                    // double t1 = trans.gpsec - x2, t2 = x1 - trans.gpsec;
                    if (y1 != 0 && y2 != 0)
                    {
                        // dts[0] = linerclk(x1, x2, y1, y2, trans.gpsec);
                        dts[0] = (y2 * x1 - y1 * x2) / (x1 - x2);
                        j = x1 < -x2 ? 0 : 1;
                        //  varc = (x1 < -x2) ? clk_prn[k1].clock[1] * CLIGHT + EXTERR_CLK * Math.Abs(x1) : clk_prn[k1+1].clock[1] * CLIGHT + EXTERR_CLK * Math.Abs(x2);
                        varc = (x1 < -x2) ? clk_prn[k1].std * CLIGHT + EXTERR_CLK * Math.Abs(x1) : clk_prn[k1 + 1].std * CLIGHT + EXTERR_CLK * Math.Abs(x2);
                    }
                    else { dts[0] = 0.0; return 0; }

                    //varc为卫星钟差内插的std
                    // varc=clk_prn[index].clock[1]*CLIGHT+EXTERR_CLK*Math.Abs()
                }
            }
            else { dts[0] = 0.0; return 0; }

            if (var != null)
            {
                var[0] = vare + varc * varc;
            }
            return 1;
        }
        /*内维尔插值*/
        public static double interNev(double[] x, double[] y, int n)
        {
            int i, j;

            for (j = 1; j < n; j++)
            {
                for (i = 0; i < n - j; i++)
                {
                    y[i] = (x[i + j] * y[i] - x[i] * y[i + 1]) / (x[i + j] - x[i]);
                }
            }
            return y[0];

        }

        public static double linerclk(double x1, double x2, double y1, double y2, double x)
        {
            double y = 0;
            y = (y2 - y1) * (x - x1) / (x2 - x1) + y1;
            return y;
        }

        public static void astro_args(double t, double[] f)//获取天文参数，这个地方不太懂，直接参考RTKLIB
        {
            double[][] fc = new double[5][] { /* coefficients for iau 1980 nutation */
                                             new double[5]{ 134.96340251, 1717915923.2178,  31.8792,  0.051635, -0.00024470},
                                             new double[5]{ 357.52910918,  129596581.0481,  -0.5532,  0.000136, -0.00001149},
                                             new double[5]{  93.27209062, 1739527262.8478, -12.7512, -0.001037,  0.00000417},
                                             new double[5]{ 297.85019547, 1602961601.2090,  -6.3706,  0.006593, -0.00003169},
                                             new double[5]{ 125.04455501,   -6962890.2665,   7.4722,  0.007702, -0.00005939}
             };
            double[] tt = new double[4];
            int i, j;
            for (tt[0] = t, i = 1; i < 4; i++) tt[i] = tt[i - 1] * t;
            for (i = 0; i < 5; i++)
            {
                f[i] = fc[i][0] * 3600.0;
                for (j = 0; j < 4; j++) f[i] += fc[i][j + 1] * tt[j];
                // f[i] = Math.IEEERemainder(f[i] * AS2R, 2.0 * Math.PI);//fmod函数可以对浮点数求余，可返回浮点数
                f[i] = (f[i] * AS2R) % (2.0 * Math.PI);
            }
        }
        /*太阳和月亮在惯性坐标系中的位置*/
        public static void sunmoonpos_eci(time tut, rtktime ut, double[] rsun, double[] rmoon)
        {
            double[] ep2000 = new double[6] { 2000, 1, 1, 12, 0, 0 };
            double t, eps, Ms, ls, rs, lm, pm, rm, sine, cose, sinp, cosp, sinl, cosl;
            double[] f = new double[5];
            time tep = new time(ep2000);
            t = (tut.ut1 - tep.gpsec) / 86400.0 / 36525.0;//tut是加过ut1-utc改正的utc
            t = rtklibcmn.timediff(ut, rtklibcmn.epoch2time(ep2000)) / 86400.0 / 36525.0;
            astro_args(t, f);

            //黄赤交角
            eps = 23.439291 - 0.0130042 * t;
            sine = Math.Sin(eps * D2R); cose = Math.Cos(eps * D2R);

            if (rsun != null)
            {
                Ms = 357.5277233 + 35999.05034 * t;
                ls = 280.460 + 36000.770 * t + 1.914666471 * Math.Sin(Ms * D2R) + 0.019994643 * Math.Sin(2.0 * Ms * D2R);
                rs = AU * (1.000140612 - 0.016708617 * Math.Cos(Ms * D2R) - 0.000139589 * Math.Cos(2.0 * Ms * D2R));
                sinl = Math.Sin(ls * D2R); cosl = Math.Cos(ls * D2R);
                rsun[0] = rs * cosl;
                rsun[1] = rs * cose * sinl;
                rsun[2] = rs * sine * sinl;
            }
            if (rmoon != null)
            {
                lm = 218.32 + 481267.883 * t + 6.29 * Math.Sin(f[0]) - 1.27 * Math.Sin(f[0] - 2.0 * f[3]) +
                   0.66 * Math.Sin(2.0 * f[3]) + 0.21 * Math.Sin(2.0 * f[0]) - 0.19 * Math.Sin(f[1]) - 0.11 * Math.Sin(2.0 * f[2]);
                pm = 5.13 * Math.Sin(f[2]) + 0.28 * Math.Sin(f[0] + f[2]) - 0.28 * Math.Sin(f[2] - f[0]) -
                   0.17 * Math.Sin(f[2] - 2.0 * f[3]);
                rm = RE_WGS84 / Math.Sin((0.9508 + 0.0518 * Math.Cos(f[0]) + 0.0095 * Math.Cos(f[0] - 2.0 * f[3]) +
                           0.0078 * Math.Cos(2.0 * f[3]) + 0.0028 * Math.Cos(2.0 * f[0])) * D2R);
                sinl = Math.Sin(lm * D2R); cosl = Math.Cos(lm * D2R);
                sinp = Math.Sin(pm * D2R); cosp = Math.Cos(pm * D2R);
                rmoon[0] = rm * cosp * cosl;
                rmoon[1] = rm * (cose * cosp * sinl - sine * sinp);
                rmoon[2] = rm * (sine * cosp * sinl + cose * sinp);
            }


        }
        /*计算太阳和月亮在ecef中的坐标*/
        public static void sunmoonpos(time tutc, rtktime utc, double[] erpv, double[] rsun, double[] rmoon, double[] gmst)
        {
            double[] rs = new double[3];
            double[] rm = new double[3];
            matrix U = new matrix(3, 3), matrsm = new matrix(3, 1), matrmm = new matrix(3, 1);
            double[] gmst_ = new double[1];
            rtktime ut = new rtktime();

            tutc.ut1 = tutc.utc + erpv[2];/* utc -> ut1 */
            ut = rtklibcmn.timeadd(utc, erpv[2]);
            /* sun and moon position in eci */
            sunmoonpos_eci(tutc, ut, rs, rm);
            /* eci to ecef transformation matrix */
            eci2ecef(tutc, utc, erpv, U, gmst_);

            matrix rsm = matrix.Array2matrix(rs);
            matrix rmm = matrix.Array2matrix(rm);

            if (rsun != null)
            {
                matrsm = U * rsm;
                //matrix.matprint(rsm);matrix.matprint(U);
                for (int i = 0; i < 3; i++)
                    rsun[i] = matrsm[i + 1, 1];
            }
            if (rmoon != null)
            {
                matrmm = U * rmm;
                for (int i = 0; i < 3; i++)
                    rmoon[i] = matrmm[i + 1, 1];
            }
            if (gmst != null) gmst[0] = gmst_[0];
        }

        public static void eci2ecef(time tutc, rtktime utc, double[] erpv, matrix U, double[] gmst)
        {
            double[] ep2000 = new double[6] { 2000, 1, 1, 12, 0, 0 };
            time tep = new time(ep2000);
            time tutc_ = new time();
            rtktime tgps = new rtktime();
            double gmst_;
            matrix U_ = new matrix(3, 3);
            double eps, ze, th, z, t, t2, t3, gast;
            double[] dpsi = new double[1];
            double[] deps = new double[1];
            double[] f = new double[5];
            matrix R1 = new matrix(3, 3); matrix R2 = new matrix(3, 3); matrix R3 = new matrix(3, 3);
            matrix R = new matrix(3, 3);
            matrix W = new matrix(3, 3); matrix N = new matrix(3, 3); matrix P = new matrix(3, 3);
            matrix NP = new matrix(3, 3);
            // double[] R1, R2[, R3[9], R[9], W[9], N[9], P[9], NP[9];
            int i;
            tutc_ = tutc;
            t = (tutc_.gpsec - tep.gpsec + 19.0 + 32.184) / 86400.0 / 36525.0;
            tgps = rtklibcmn.utc2gpst(utc);
            t = (rtklibcmn.timediff(tgps, rtklibcmn.epoch2time(ep2000)) + 19.0 + 32.184) / 86400.0 / 36525.0;
            t2 = t * t; t3 = t2 * t;
            astro_args(t, f);

            /* iau 1976 precession */
            ze = (2306.2181 * t + 0.30188 * t2 + 0.017998 * t3) * AS2R;
            th = (2004.3109 * t - 0.42665 * t2 - 0.041833 * t3) * AS2R;
            z = (2306.2181 * t + 1.09468 * t2 + 0.018203 * t3) * AS2R;
            eps = (84381.448 - 46.8150 * t - 0.00059 * t2 + 0.001813 * t3) * AS2R;
            //Rz(-z,R1); Ry(th,R2); Rz(-ze,R3);
            Rz(-z, R1); Ry(th, R2); Rz(-ze, R3);
            #region
            // R1[1, 1] = Math.Cos(-z);    R1[1, 2] = -Math.Sin(-z);    R1[2, 1] = -R1[1, 2]; R1[2, 2] = R1[1, 1]; R1[3, 3] = 1.0;
            // R2[1, 1] = Math.Cos(th);    R2[1, 3] = Math.Sin(th);    R2[2, 2] = 1.0; R2[3, 1] = -R2[1, 3]; R2[3, 3] = R2[1, 1];
            // R3[1, 1] = Math.Cos(-ze); R3[1, 2] = -Math.Sin(-ze); R3[2, 1] = -R3[1, 2]; R3[2, 2] = R3[1, 1]; R3[3, 3] = 1.0;
            #endregion
            //P = R1 * R2 * R3;
            P = matrix.transp(R1) * matrix.transp(R2) * matrix.transp(R3);
            /* iau 1980 nutation */
            nut_iau1980(t, f, dpsi, deps);
            // matrix R11 = new matrix(3, 3), R22 = new matrix(3, 3), R33 = new matrix(3, 3);
            //Rx(-eps-deps,R1); Rz(-dpsi,R2); Rx(eps,R3);
            Rx(-eps - deps[0], R1); Rz(-dpsi[0], R2); Rx(eps, R3);
            #region
            // R1 = new matrix(3, 3);R2 = new matrix(3, 3);R3 = new matrix(3, 3);
            // R1[1, 1] = 1.0;R1[2, 2]=R1[3,3] = Math.Cos(-eps - deps[0]); R1[2, 3] = -Math.Sin(-eps - deps[0]);R1[3, 2] = -R1[2, 3];
            // R2[1, 1] = R2[2,2]=Math.Cos(-dpsi[0]); R2[1, 2] = -Math.Sin(-dpsi[0]); R2[2, 1] = -R2[1, 2]; R2[3, 3] = 1.0;
            // R3[1, 1] = 1.0;R3[2, 2] = R3[3, 3] = Math.Cos(eps);R3[2, 3] = -Math.Sin(eps);R3[3, 2] = -R3[2, 3];
            #endregion
            // matrix.matprint(R1);matrix.matprint(R2);matrix.matprint(R3);
            //matrix R1R2 = R11 * R22;

            //N = R1 * R2 * R3;
            N = matrix.transp(R1) * matrix.transp(R2) * matrix.transp(R3);
            //tgps = time.utc2gpst(tutc_);
            /* greenwich aparent sidereal time (rad) */ //格林尼治非恒星时
            gmst_ = time.utc2gmst(tutc_, erpv[2]);
            // gmst_ = rtklibcmn.utc2gmst(utc, erpv[2]);
            gast = gmst_ + dpsi[0] * Math.Cos(eps);
            gast += (0.00264 * Math.Sin(f[4]) + 0.000063 * Math.Sin(2.0 * f[4])) * AS2R;
            //          R1 = new matrix(3, 3); R2 = new matrix(3, 3); R3 = new matrix(3, 3);
            //Ry(-erpv[0],R1); Rx(-erpv[1],R2); Rz(gast,R3);
            Ry(-erpv[0], R1); Rx(-erpv[1], R2); Rz(gast, R3);

            W = matrix.transp(R1) * matrix.transp(R2); R = W * matrix.transp(R3);
            NP = N * P;
            U_ = R * NP;

            for (i = 1; i <= U_.rows; i++)
                for (int j = 1; j <= U_.columns; j++)
                    U[i, j] = U_[i, j];
            // U = matrix.matcpy(U_);
            if (gmst != null) gmst[0] = gmst_;
        }

        public static void Rx(double t, matrix X)
        {
            X[1, 1] = 1.0;
            X[1, 2] = X[1, 3] = X[2, 1] = X[3, 1] = 0;
            X[2, 2] = X[3, 3] = Math.Cos(t);
            X[3, 2] = Math.Sin(t); X[2, 3] = -X[3, 2];
        }
        public static void Ry(double t, matrix X)
        {
            X[1, 1] = X[3, 3] = Math.Cos(t);
            X[2, 2] = 1.0;
            X[1, 2] = X[2, 1] = X[2, 3] = X[3, 2] = 0;
            X[1, 3] = Math.Sin(t); X[3, 1] = -X[1, 3];
        }
        public static void Rz(double t, matrix X)
        {
            X[1, 1] = X[2, 2] = Math.Cos(t);
            X[1, 3] = X[2, 3] = X[3, 1] = X[3, 2] = 0;
            X[2, 1] = Math.Sin(t); X[1, 2] = -X[2, 1];
            X[3, 3] = 1.0;

        }
        /*标准化三维向量*/
        public static void nomv3d(double[] a, double[] b)//标准化三维向量
        {
            double r;
            r = matrix.dotvector(matrix.Array2matrix(a), matrix.Array2matrix(a));
            r = Math.Sqrt(r);
            if (r > 0)
            {
                b[0] = a[0] / r;
                b[1] = a[1] / r;
                b[2] = a[2] / r;
            }

        }
        /*三维向量的外积*/
        public static void cross3(double[] a, double[] b, double[] c)//向量的外积
        {
            c[0] = a[1] * b[2] - a[2] * b[1];
            c[1] = a[2] * b[0] - a[0] * b[2];
            c[2] = a[0] * b[1] - a[1] * b[0];
        }
        /* iau 1980 nutation ---------------------------------------------------------*/
        public static void nut_iau1980(double t, double[] f, double[] dpsi, double[] deps)
        {
            double[,] nut = new double[106, 10]{
        {   0,   0,   0,   0,   1, -6798.4, -171996, -174.2, 92025,   8.9},
        {   0,   0,   2,  -2,   2,   182.6,  -13187,   -1.6,  5736,  -3.1},
        {   0,   0,   2,   0,   2,    13.7,   -2274,   -0.2,   977,  -0.5},
        {   0,   0,   0,   0,   2, -3399.2,    2062,    0.2,  -895,   0.5},
        {   0,  -1,   0,   0,   0,  -365.3,   -1426,    3.4,    54,  -0.1},
        {   1,   0,   0,   0,   0,    27.6,     712,    0.1,    -7,   0.0},
        {   0,   1,   2,  -2,   2,   121.7,    -517,    1.2,   224,  -0.6},
        {   0,   0,   2,   0,   1,    13.6,    -386,   -0.4,   200,   0.0},
        {   1,   0,   2,   0,   2,     9.1,    -301,    0.0,   129,  -0.1},
        {   0,  -1,   2,  -2,   2,   365.2,     217,   -0.5,   -95,   0.3},
        {  -1,   0,   0,   2,   0,    31.8,     158,    0.0,    -1,   0.0},
        {   0,   0,   2,  -2,   1,   177.8,     129,    0.1,   -70,   0.0},
        {  -1,   0,   2,   0,   2,    27.1,     123,    0.0,   -53,   0.0},
        {   1,   0,   0,   0,   1,    27.7,      63,    0.1,   -33,   0.0},
        {   0,   0,   0,   2,   0,    14.8,      63,    0.0,    -2,   0.0},
        {  -1,   0,   2,   2,   2,     9.6,     -59,    0.0,    26,   0.0},
        {  -1,   0,   0,   0,   1,   -27.4,     -58,   -0.1,    32,   0.0},
        {   1,   0,   2,   0,   1,     9.1,     -51,    0.0,    27,   0.0},
        {  -2,   0,   0,   2,   0,  -205.9,     -48,    0.0,     1,   0.0},
        {  -2,   0,   2,   0,   1,  1305.5,      46,    0.0,   -24,   0.0},
        {   0,   0,   2,   2,   2,     7.1,     -38,    0.0,    16,   0.0},
        {   2,   0,   2,   0,   2,     6.9,     -31,    0.0,    13,   0.0},
        {   2,   0,   0,   0,   0,    13.8,      29,    0.0,    -1,   0.0},
        {   1,   0,   2,  -2,   2,    23.9,      29,    0.0,   -12,   0.0},
        {   0,   0,   2,   0,   0,    13.6,      26,    0.0,    -1,   0.0},
        {   0,   0,   2,  -2,   0,   173.3,     -22,    0.0,     0,   0.0},
        {  -1,   0,   2,   0,   1,    27.0,      21,    0.0,   -10,   0.0},
        {   0,   2,   0,   0,   0,   182.6,      17,   -0.1,     0,   0.0},
        {   0,   2,   2,  -2,   2,    91.3,     -16,    0.1,     7,   0.0},
        {  -1,   0,   0,   2,   1,    32.0,      16,    0.0,    -8,   0.0},
        {   0,   1,   0,   0,   1,   386.0,     -15,    0.0,     9,   0.0},
        {   1,   0,   0,  -2,   1,   -31.7,     -13,    0.0,     7,   0.0},
        {   0,  -1,   0,   0,   1,  -346.6,     -12,    0.0,     6,   0.0},
        {   2,   0,  -2,   0,   0, -1095.2,      11,    0.0,     0,   0.0},
        {  -1,   0,   2,   2,   1,     9.5,     -10,    0.0,     5,   0.0},
        {   1,   0,   2,   2,   2,     5.6,      -8,    0.0,     3,   0.0},
        {   0,  -1,   2,   0,   2,    14.2,      -7,    0.0,     3,   0.0},
        {   0,   0,   2,   2,   1,     7.1,      -7,    0.0,     3,   0.0},
        {   1,   1,   0,  -2,   0,   -34.8,      -7,    0.0,     0,   0.0},
        {   0,   1,   2,   0,   2,    13.2,       7,    0.0,    -3,   0.0},
        {  -2,   0,   0,   2,   1,  -199.8,      -6,    0.0,     3,   0.0},
        {   0,   0,   0,   2,   1,    14.8,      -6,    0.0,     3,   0.0},
        {   2,   0,   2,  -2,   2,    12.8,       6,    0.0,    -3,   0.0},
        {   1,   0,   0,   2,   0,     9.6,       6,    0.0,     0,   0.0},
        {   1,   0,   2,  -2,   1,    23.9,       6,    0.0,    -3,   0.0},
        {   0,   0,   0,  -2,   1,   -14.7,      -5,    0.0,     3,   0.0},
        {   0,  -1,   2,  -2,   1,   346.6,      -5,    0.0,     3,   0.0},
        {   2,   0,   2,   0,   1,     6.9,      -5,    0.0,     3,   0.0},
        {   1,  -1,   0,   0,   0,    29.8,       5,    0.0,     0,   0.0},
        {   1,   0,   0,  -1,   0,   411.8,      -4,    0.0,     0,   0.0},
        {   0,   0,   0,   1,   0,    29.5,      -4,    0.0,     0,   0.0},
        {   0,   1,   0,  -2,   0,   -15.4,      -4,    0.0,     0,   0.0},
        {   1,   0,  -2,   0,   0,   -26.9,       4,    0.0,     0,   0.0},
        {   2,   0,   0,  -2,   1,   212.3,       4,    0.0,    -2,   0.0},
        {   0,   1,   2,  -2,   1,   119.6,       4,    0.0,    -2,   0.0},
        {   1,   1,   0,   0,   0,    25.6,      -3,    0.0,     0,   0.0},
        {   1,  -1,   0,  -1,   0, -3232.9,      -3,    0.0,     0,   0.0},
        {  -1,  -1,   2,   2,   2,     9.8,      -3,    0.0,     1,   0.0},
        {   0,  -1,   2,   2,   2,     7.2,      -3,    0.0,     1,   0.0},
        {   1,  -1,   2,   0,   2,     9.4,      -3,    0.0,     1,   0.0},
        {   3,   0,   2,   0,   2,     5.5,      -3,    0.0,     1,   0.0},
        {  -2,   0,   2,   0,   2,  1615.7,      -3,    0.0,     1,   0.0},
        {   1,   0,   2,   0,   0,     9.1,       3,    0.0,     0,   0.0},
        {  -1,   0,   2,   4,   2,     5.8,      -2,    0.0,     1,   0.0},
        {   1,   0,   0,   0,   2,    27.8,      -2,    0.0,     1,   0.0},
        {  -1,   0,   2,  -2,   1,   -32.6,      -2,    0.0,     1,   0.0},
        {   0,  -2,   2,  -2,   1,  6786.3,      -2,    0.0,     1,   0.0},
        {  -2,   0,   0,   0,   1,   -13.7,      -2,    0.0,     1,   0.0},
        {   2,   0,   0,   0,   1,    13.8,       2,    0.0,    -1,   0.0},
        {   3,   0,   0,   0,   0,     9.2,       2,    0.0,     0,   0.0},
        {   1,   1,   2,   0,   2,     8.9,       2,    0.0,    -1,   0.0},
        {   0,   0,   2,   1,   2,     9.3,       2,    0.0,    -1,   0.0},
        {   1,   0,   0,   2,   1,     9.6,      -1,    0.0,     0,   0.0},
        {   1,   0,   2,   2,   1,     5.6,      -1,    0.0,     1,   0.0},
        {   1,   1,   0,  -2,   1,   -34.7,      -1,    0.0,     0,   0.0},
        {   0,   1,   0,   2,   0,    14.2,      -1,    0.0,     0,   0.0},
        {   0,   1,   2,  -2,   0,   117.5,      -1,    0.0,     0,   0.0},
        {   0,   1,  -2,   2,   0,  -329.8,      -1,    0.0,     0,   0.0},
        {   1,   0,  -2,   2,   0,    23.8,      -1,    0.0,     0,   0.0},
        {   1,   0,  -2,  -2,   0,    -9.5,      -1,    0.0,     0,   0.0},
        {   1,   0,   2,  -2,   0,    32.8,      -1,    0.0,     0,   0.0},
        {   1,   0,   0,  -4,   0,   -10.1,      -1,    0.0,     0,   0.0},
        {   2,   0,   0,  -4,   0,   -15.9,      -1,    0.0,     0,   0.0},
        {   0,   0,   2,   4,   2,     4.8,      -1,    0.0,     0,   0.0},
        {   0,   0,   2,  -1,   2,    25.4,      -1,    0.0,     0,   0.0},
        {  -2,   0,   2,   4,   2,     7.3,      -1,    0.0,     1,   0.0},
        {   2,   0,   2,   2,   2,     4.7,      -1,    0.0,     0,   0.0},
        {   0,  -1,   2,   0,   1,    14.2,      -1,    0.0,     0,   0.0},
        {   0,   0,  -2,   0,   1,   -13.6,      -1,    0.0,     0,   0.0},
        {   0,   0,   4,  -2,   2,    12.7,       1,    0.0,     0,   0.0},
        {   0,   1,   0,   0,   2,   409.2,       1,    0.0,     0,   0.0},
        {   1,   1,   2,  -2,   2,    22.5,       1,    0.0,    -1,   0.0},
        {   3,   0,   2,  -2,   2,     8.7,       1,    0.0,     0,   0.0},
        {  -2,   0,   2,   2,   2,    14.6,       1,    0.0,    -1,   0.0},
        {  -1,   0,   0,   0,   2,   -27.3,       1,    0.0,    -1,   0.0},
        {   0,   0,  -2,   2,   1,  -169.0,       1,    0.0,     0,   0.0},
        {   0,   1,   2,   0,   1,    13.1,       1,    0.0,     0,   0.0},
        {  -1,   0,   4,   0,   2,     9.1,       1,    0.0,     0,   0.0},
        {   2,   1,   0,  -2,   0,   131.7,       1,    0.0,     0,   0.0},
        {   2,   0,   0,   2,   0,     7.1,       1,    0.0,     0,   0.0},
        {   2,   0,   2,  -2,   1,    12.8,       1,    0.0,    -1,   0.0},
        {   2,   0,  -2,   0,   1,  -943.2,       1,    0.0,     0,   0.0},
        {   1,  -1,   0,  -2,   0,   -29.3,       1,    0.0,     0,   0.0},
        {  -1,   0,   0,   1,   1,  -388.3,       1,    0.0,     0,   0.0},
        {  -1,  -1,   0,   2,   1,    35.0,       1,    0.0,     0,   0.0},
        {   0,   1,   0,   1,   0,    27.3,       1,    0.0,     0,   0.0}
            };
            double ang;
            int i, j;
            dpsi[0] = deps[0] = 0;
            for (i = 0; i < 106; i++)
            {
                ang = 0.0;
                for (j = 0; j < 5; j++) ang += nut[i, j] * f[j];
                dpsi[0] += (nut[i, 6] + nut[i, 7] * t) * Math.Sin(ang);
                deps[0] += (nut[i, 8] + nut[i, 9] * t) * Math.Cos(ang);
            }
            dpsi[0] *= 1E-4 * AS2R; /* 0.1 mas -> rad */
            deps[0] *= 1E-4 * AS2R;
        }
        /*对流层模型*/
        public static double tropmodel(time t, double[] pos, double[] azel, double humid)
        {
            const double temp0 = 15.0; /* temparature at sea level */
            double hgt, pres, temp, e, z, trph, trpw;
            if (pos[2] < -100.0 || 1E4 < pos[2] || azel[1] <= 0) return 0.0;
            /* standard atmosphere */
            hgt = pos[2] < 0.0 ? 0.0 : pos[2];

            pres = 1013.25 * Math.Pow(1.0 - 2.2557E-5 * hgt, 5.2568);
            temp = temp0 - 6.5E-3 * hgt + 273.16;
            e = 6.108 * humid * Math.Exp((17.15 * temp - 4684.0) / (temp - 38.45));

            /* saastamoninen model */
            z = Math.PI / 2.0 - azel[1];
            trph = 0.0022768 * pres / (1.0 - 0.00266 * Math.Cos(2.0 * pos[0]) - 0.00028 * hgt / 1E3) / Math.Cos(z);
            trpw = 0.002277 * (1255.0 / temp + 0.05) * e / Math.Cos(z);
            return trph + trpw;
        }
        /*精密对流层模型*/
        public static double prectrop(time t, double[] pos, double[] azel, double x, double[] dtdx)
        {
            double[] zazel = new double[2] { 0.0, Math.PI / 2.0 };
            double zhd, m_h;
            double[] m_w = new double[1];

            zhd = tropmodel(t, pos, zazel, 0.0);//天顶方向的延迟

            m_h = NMF(t, pos, azel, m_w);
            dtdx[0] = m_w[0];
            return m_h * zhd + m_w[0] * (x - zhd);

        }
        /*NMF投影函数*/
        public static double NMF(time t, double[] pos, double[] azel, double[] mwet)//返回NMF干湿延迟投影函数
        {
            /* ave a b c;amp a b c; wet a b c */
            double[][] coe_table = new double[9][] {
                new double[5]{ 1.2769934E-3, 1.2683230E-3, 1.2465397E-3, 1.2196049E-3, 1.2045996E-3},
                new double[5]{ 2.9153695E-3, 2.9152299E-3, 2.9288445E-3, 2.9022565E-3, 2.9024912E-3},
                new double[5]{ 62.610505E-3, 62.837393E-3, 63.721774E-3, 63.824265E-3, 64.258455E-3},

                new double[5]{ 0.0000000E-0, 1.2709626E-5, 2.6523662E-5, 3.4000452E-5, 4.1202191E-5},
                new double[5]{ 0.0000000E-0, 2.1414979E-5, 3.0160779E-5, 7.2562722E-5, 11.723375E-5},
                new double[5]{ 0.0000000E-0, 9.0128400E-5, 4.3497037E-5, 84.795348E-5, 170.37206E-5},

                new double[5]{ 5.8021897E-4, 5.6794847E-4, 5.8118019E-4, 5.9727542E-4, 6.1641693E-4},
                new double[5]{ 1.4275268E-3, 1.5138625E-3, 1.4572752E-3, 1.5007428E-3, 1.7599082E-3},
                new double[5]{ 4.3472961E-2, 4.6729510E-2, 4.3908931E-2, 4.4626982E-2, 5.4736038E-2}
            };
            double[] aht = new double[3] { 2.53E-5, 5.49E-3, 1.14E-3 };
            double y, cosy, doy, dm, el = azel[1], hgt = pos[2], lat = pos[0] * R2D;
            double[] ah = new double[3], aw = new double[3];
            double[] ep = new double[6];
            time tep;
            ep[0] = t.calend[0]; ep[1] = ep[2] = 1.0; ep[3] = ep[4] = ep[5] = 0.0;
            tep = new time(ep);
            doy = (t.gpsec - tep.gpsec) / 86400.0 + 1.0;//年积日，只考虑了北半球
            y = (doy - 28) / 365.25;
            cosy = Math.Cos(Math.PI * 2 * y);
            lat = Math.Abs(lat);

            for (int i = 0; i < 3; i++)
            {
                ah[i] = intertrop(coe_table[i], lat) - intertrop(coe_table[i + 3], lat) * cosy;//这里为啥是减号，书上是加号
                aw[i] = intertrop(coe_table[i + 6], lat);
            }
            /* ellipsoidal height is used instead of height above sea level */
            dm = (1.0 / Math.Sin(el) - mapf(el, aht[0], aht[1], aht[2])) * hgt / 1E3;
            if (mwet != null) mwet[0] = mapf(el, aw[0], aw[1], aw[2]);
            return mapf(el, ah[0], ah[1], ah[2]) + dm;

        }

        public static double intertrop(double[] coef, double lat)
        {
            int i = (int)(lat / 15.0);
            if (i < 1) return coef[0]; else if (i > 4) return coef[4];
            return coef[i - 1] * (1.0 - lat / 15.0 + i) + coef[i] * (lat / 15.0 - i);
        }
        public static double mapf(double el, double a, double b, double c)
        {
            double sinel = Math.Sin(el);
            return (1.0 + a / (1.0 + b / (1.0 + c))) / (sinel + (a / (sinel + b / (sinel + c))));
        }
        /*天线相位缠绕改正*/
        public static void windup(time t, rtktime soltime, double[] rs, double[] rr, double[] phw)
        {
            double[] ek = new double[3], exs = new double[3], eys = new double[3], ezs = new double[3];
            double[] ess = new double[3], exr = new double[3], eyr = new double[3], eks = new double[3], ekr = new double[3];
            double[] E = new double[9], dr = new double[3], ds = new double[3], drs = new double[3], r = new double[3], rsun = new double[3];
            matrix pos = new matrix(3, 3);
            double cosp, ph;
            double[] erpv = new double[5];
            int i;
            /* sun position in ecef */
            t.gpst2utc();
            sunmoonpos(t, rtklibcmn.gpst2utc(soltime), erpv, rsun, null, null);

            /* unit vector satellite to receiver */
            for (i = 0; i < 3; i++) r[i] = rr[i] - rs[i];
            nomv3d(r, ek);
            /* unit vectors of satellite antenna */
            for (i = 0; i < 3; i++) r[i] = -rs[i];
            nomv3d(r, ezs);
            for (i = 0; i < 3; i++) r[i] = rsun[i] - rs[i];
            nomv3d(r, ess);//卫星至太阳的单位矢量
            cross3(ezs, ess, r);
            nomv3d(r, eys);
            cross3(eys, ezs, exs);

            /* unit vectors of receiver antenna */
            transcoor.ecef2pos(matrix.Array2matrix(rr), pos);
            transcoor.mat_xyz2enu(pos, E);
            exr[0] = E[1]; exr[1] = E[4]; exr[2] = E[7]; /* x = north */
            eyr[0] = -E[0]; eyr[1] = -E[3]; eyr[2] = -E[6]; /* y = west  */
            cross3(ek, eys, eks);
            cross3(ek, eyr, ekr);
            for (i = 0; i < 3; i++)
            {
                ds[i] = exs[i] - ek[i] * matrix.dotvector(matrix.Array2matrix(ek), matrix.Array2matrix(exs)) - eks[i];
                dr[i] = exr[i] - ek[i] * matrix.dotvector(matrix.Array2matrix(ek), matrix.Array2matrix(exr)) + ekr[i];
            }
            cosp = matrix.dotvector(matrix.Array2matrix(ds), matrix.Array2matrix(dr)) / Math.Sqrt(ds[0] * ds[0] + ds[1] * ds[1] + ds[2] * ds[2]) / Math.Sqrt(dr[0] * dr[0] + dr[1] * dr[1] + dr[2] * dr[2]);
            if (cosp < -1.0) cosp = -1.0;
            else if (cosp > 1.0) cosp = 1.0;
            ph = Math.Acos(cosp) / 2.0 / Math.PI;
            cross3(ds, dr, drs);
            if (matrix.dotvector(matrix.Array2matrix(ek), matrix.Array2matrix(drs)) < 0.0) ph = -ph;

            phw[0] = ph + Math.Floor(phw[0] - ph + 0.5);
        }

        /*public static void solidtidedis(time tutc,double[] rr,erp_t erp,double[] dr)
        {
            //time tut=new time();
            double[] pos = new double[2], E = new double[9], drt = new double[3], denu = new double[3];
            double[] rs = new double[3], rm = new double[3],  erpv = new double[5];
            double[] gmst = new double[1];
            double R;
            int i;
            geterp(tutc, erp, erpv);
            dr[0] = dr[1] = dr[2] = 0.0;
            R = Math.Sqrt(rr[0] * rr[0] + rr[1] * rr[1] + rr[2] * rr[2]);
            if (R <= 0) return;
            pos[0] = Math.Asin(rr[2] / R);
            pos[1] = Math.Atan2(rr[1], rr[0]);
            // xyz2enu(pos, E);
            transcoor.mat_xyz2enu(matrix.Array2matrix(pos), E);
            sunmoonpos(tutc, erpv, rs, rm, gmst);
            tide_solid(rs, rm, pos, E, gmst[0],  drt);//地球固体潮位移
            for (i = 0; i < 3; i++) dr[i] += drt[i];
        }*/

        public static void geterp(time t, rtktime utc, erp_t erp, double[] erpv)
        {
            double[] ep = new double[6] { 2000, 1, 1, 12, 0, 0 };
            double mjd, day, a, t1;
            int i, j, k;
            time tep = new time(ep);
            t.gpst2utc();
            t1 = time.gpst2utc(t.utc);
            mjd = 51544.5 + (t1 - tep.gpsec) / 86400.0;
            mjd = 51544.5 + (rtklibcmn.timediff(rtklibcmn.gpst2utc(utc), rtklibcmn.epoch2time(ep))) / 86400.0;
            if (mjd <= erp.erpdata[0].mjd)
            {
                day = mjd - erp.erpdata[0].mjd;
                erpv[0] = erp.erpdata[0].xp + erp.erpdata[0].xpr * day;
                erpv[1] = erp.erpdata[0].yp + erp.erpdata[0].ypr * day;
                erpv[2] = erp.erpdata[0].ut1_utc - erp.erpdata[0].lod * day;
                erpv[3] = erp.erpdata[0].lod;
            }
            if (mjd >= erp.erpdata[erp.erpdata.Count - 1].mjd)
            {
                day = mjd - erp.erpdata[erp.erpdata.Count - 1].mjd;
                erpv[0] = erp.erpdata[erp.erpdata.Count - 1].xp + erp.erpdata[erp.erpdata.Count - 1].xpr * day;
                erpv[1] = erp.erpdata[erp.erpdata.Count - 1].yp + erp.erpdata[erp.erpdata.Count - 1].ypr * day;
                erpv[2] = erp.erpdata[erp.erpdata.Count - 1].ut1_utc - erp.erpdata[erp.erpdata.Count - 1].lod * day;
                erpv[3] = erp.erpdata[erp.erpdata.Count - 1].lod;
            }
            for (j = 0, k = erp.erpdata.Count - 1; j < k - 1;)
            {
                i = (j + k) / 2;
                if (mjd < erp.erpdata[i].mjd) k = i; else j = i;
            }
            if (erp.erpdata[j].mjd == erp.erpdata[j + 1].mjd)
            {
                a = 0.5;
            }
            else
            {
                a = (mjd - erp.erpdata[j].mjd) / (erp.erpdata[j + 1].mjd - erp.erpdata[j].mjd);
            }
            erpv[0] = (1.0 - a) * erp.erpdata[j].xp + a * erp.erpdata[j + 1].xp;
            erpv[1] = (1.0 - a) * erp.erpdata[j].yp + a * erp.erpdata[j + 1].yp;
            erpv[2] = (1.0 - a) * erp.erpdata[j].ut1_utc + a * erp.erpdata[j + 1].ut1_utc;
            erpv[3] = (1.0 - a) * erp.erpdata[j].lod + a * erp.erpdata[j + 1].lod;

        }
        /*固体潮改正*/
        public static void tide_solid(double[] rsun, double[] rmoon, double[] pos, double[] E, double gmst, double[] dr)
        {
            double[] dr1 = new Double[3], dr2 = new Double[3], eu = new Double[3];
            double du, dn, sinl, sin2l;
            eu[0] = E[2]; eu[1] = E[5]; eu[2] = E[8];
            tide_p(eu, rsun, GMS, pos, dr1);
            tide_p(eu, rmoon, GMM, pos, dr2);

            /* step2: frequency domain, only K1 radial */
            sin2l = Math.Sin(2.0 * pos[0]);
            du = -0.012 * sin2l * Math.Sin(gmst + pos[1]);

            dr[0] = dr1[0] + dr2[0] + du * E[2];
            dr[1] = dr1[1] + dr2[1] + du * E[5];
            dr[2] = dr1[2] + dr2[2] + du * E[8];
        }
        public static void tide_p(double[] eu, double[] rp, double GMp, double[] pos, double[] dr)
        {
            const double H3 = 0.292, L3 = 0.015;
            double r, latp, lonp, p, K2, K3, a, H2, L2, dp, du, cosp, sinl, cosl;
            double[] ep = new double[3];
            int i;

            r = Math.Sqrt(rp[0] * rp[0] + rp[1] * rp[1] + rp[2] * rp[2]);
            if (r <= 0) return;
            for (i = 0; i < 3; i++) ep[i] = rp[i] / r;

            K2 = GMp / GME * Math.Pow(RE_WGS84, 4) / (r * r * r);
            K3 = K2 * RE_WGS84 / r;
            latp = Math.Asin(ep[2]); lonp = Math.Atan2(ep[1], ep[0]);
            cosp = Math.Cos(latp); sinl = Math.Sin(pos[0]); cosl = Math.Cos(pos[0]);

            /* step1 in phase (degree 2) */
            p = (3.0 * sinl * sinl - 1.0) / 2.0;
            H2 = 0.6078 - 0.0006 * p;
            L2 = 0.0847 + 0.0002 * p;
            a = matrix.dotvector(matrix.Array2matrix(ep), matrix.Array2matrix(eu));
            dp = K2 * 3.0 * L2 * a;
            du = K2 * (H2 * (1.5 * a * a - 0.5) - 3.0 * L2 * a * a);

            /* step1 in phase (degree 3) */
            dp += K3 * L3 * (7.5 * a * a - 1.5);
            du += K3 * (H3 * (2.5 * a * a * a - 1.5 * a) - L3 * (7.5 * a * a - 1.5) * a);

            /* step1 out-of-phase (only radial) */
            du += 3.0 / 4.0 * 0.0025 * K2 * Math.Sin(2.0 * latp) * Math.Sin(2.0 * pos[0]) * Math.Sin(pos[1] - lonp);
            du += 3.0 / 4.0 * 0.0022 * K2 * cosp * cosp * cosl * cosl * Math.Sin(2.0 * (pos[1] - lonp));

            dr[0] = dp * ep[0] + du * eu[0];
            dr[1] = dp * ep[1] + du * eu[1];
            dr[2] = dp * ep[2] + du * eu[2];

        }

    }


    public class sat_t
    {

        public List<satb> satdata = new List<satb>();
    }

    public class satb
    {
        public time t;//
        public rtktime rtkt;
        public string prn;
        public double[] xyz = new double[3];//卫星坐标
        public double[] std = new double[3];//XYZ方差
        public double[] vel = new double[3];//卫星速度
        public double dts = 0;//卫星钟差
        public int flag = 0;//=0原始观测数据，=1改正后的数据，=-1剔除的数据
        public double el;//卫星高度角

    }
}
