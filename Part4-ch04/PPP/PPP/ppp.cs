using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PPP
{
    class ppp
    {
        public static sat_t sat = new sat_t();
        public static clk_t clk = new clk_t();
        public static pcv_t pcv = new pcv_t();
        const int nx = 37;//待估参数个数

        const double OMGE = 7.2921151467E-5;
        const double CLIGHT = 299792458.0;
        const double D2R = (Math.PI / 180.0);         /* 度转弧度 */
        const double R2D = (180.0 / Math.PI);         /* 弧度转度 */
        const double FREQ1 = 1.57542E9;           /* L1  frequency (Hz) */
        const double FREQ2 = 1.22760E9;         /* L2  frequency (Hz) */
        const double ERR_CBIAS = 0.3;         /* code bias error std (m) */
        const double ERR_BRDCI = 0.5;         /* broadcast iono model error factor */
        const double REL_HUMI = 0.7;         /* relative humidity for saastamoinen model */
        const double ERR_SAAS = 0.3;         /* saastamoinen model error std (m) */
        const double VAR_POS = (100.0 * 100.0);      /*   receiver position (m^2) */
        const double VAR_CLK = (100.0 * 100.0);      /*   receiver clock (m^2) */
        const double VAR_BIAS = (100.0 * 100.0);
        const double DT = 0.005;
        static int iobss = 0;
        /*标准单点定位*/
        public static int pntpos(List<obs_s> obs, nav_t nav, spp_t spp, dcb_t dcb) //obs均已按照PRN号大小排列好
        {
            /*
             n为观测历元观测到的卫星总数，stat为解算状态
             vsat为卫星的状态 0为error，1为正常
             svh为卫星坐标和钟差计算的状态，-1为不正常
             rs包括卫星坐标和速度，dts包括卫星钟差和钟漂，azel包括卫星方位角和高度角
             var为卫星坐标和钟差的方差
             */
            int n = obs.Count, stat = 1;
            int[] vsat = new int[32], svh = new int[32];
            double[][] rs = new double[n][], dts = new double[n][], azel = new double[n][];
            double[] var = new double[n], resp = new double[n];
            for (int i = 0; i < n; i++)
            {
                rs[i] = new double[6];//卫星坐标和速度
                dts[i] = new double[2];//卫星的钟差和钟漂
                azel[i] = new double[2];//卫星的方位角和高度角
            }
            pppcmn.satpos(obs, nav, sat, clk, pcv, rs, dts, var, svh);
            stat = estpos(spp, obs, dcb, nav, n, rs, dts, var, azel, vsat, svh);

            for (int i = 0; i < n; i++)
            {
                int sat = int.Parse(obs[i].sprn.Substring(1, 2));
                if (vsat[i] == 1) spp.vsat[sat - 1] = 1;

            }
            return stat;

        }
        //obs均已按照PRN号大小排列好
        public static int estpos(spp_t spp, List<obs_s> obs, dcb_t dcb, nav_t nav, int n, double[][] rs, double[][] dts,
                                double[] vare, double[][] azel, int[] vsat, int[] svh)
        {
            double[] v_ = new double[n], x = new double[4], var = new double[n];
            matrix H_ = new matrix(n, 4), H, v, dx, Q,  P;
            int nv;
            double sig;
            int i, j, k, stat = 1;
            for (i = 0; i < 3; i++) x[i] = spp.rr[i];

            for (i = 0; i < 10; i++)//迭代
            {
                nv = rescod(i, obs, nav, n, rs, dts, vare, azel, vsat, dcb, x, H_, v_, var, svh);
                if (nv < 4) break;//nv是参与解算的实际卫星数
                H = new matrix(nv, 4); v = new matrix(nv, 1); P = new matrix(nv, nv);
                for (j = 0; j < nv; j++)
                {
                    sig = Math.Sqrt(var[j]);
                    v[j + 1, 1] = v_[j] / sig;
                    for (k = 1; k <= 4; k++)
                    {
                        H[j + 1, k] = H_[j + 1, k] / sig; 
                    }                 
                }
                dx = matrix.inv(matrix.transp(H) * H) * matrix.transp(H) * v;              
                Q = matrix.transp(H) * H;

                for (j = 0; j < 4; j++) x[j] += dx[j + 1, 1];

                if (Math.Sqrt(dx[1, 1] * dx[1, 1] + dx[2, 1] * dx[2, 1] + dx[3, 1] * dx[3, 1] + dx[4, 1] * dx[4, 1]) < 1E-4)
                {
                    spp.dtr = x[3] / CLIGHT;
                    for (j = 0; j < 3; j++) spp.rr[j] = x[j];                
                    spp.tcurr.gpsec = obs[0].t.gpsec - spp.dtr;
                    spp.tcur = rtklibcmn.timeadd(obs[0].rtkt, -x[3] / CLIGHT);
                    for (j = 0; j < 6; j++) spp.tcurr.calend[j] = obs[0].t.calend[j];

                    stat = valsol(azel, vsat, 15.0 * D2R, n, v, nv, 4);
                    return stat;                   
                }
            }


            return 1;
        }
        public static int rescod(int iter, List<obs_s> obs, nav_t nav, int n, double[][] rs, double[][] dts, double[] vare, double[][] azel, int[] vsat,
                                dcb_t dcb, double[] x, matrix H, double[] v, double[] var, int[] svh)
        {
            double r, dtr, P, elmin = 15 * D2R, el = 0.0; ;
            int i, j, nv = 0;
            double[] rr = new double[3], pos = new double[3], e = new double[3], vardcb = new double[1], ion = new double[2], trop = new double[2];
            matrix p = new matrix(3, 1);
            for (i = 0; i < 3; i++) rr[i] = x[i]; dtr = x[3];
            transcoor.ecef2pos(matrix.Array2matrix(rr), p);
            pos = matrix.matrx2Array(p);

            for (i = 0; i < n; i++)
            {
                vsat[i] = 0; azel[i][0] = azel[i][1] = 0;
                if (svh[i] < 0) continue;
                if ((r = geodist(rs[i], rr, e)) < 0 || (el = pppcmn.satel(rr, rs[i], azel[i])) < elmin) continue;

                if ((P = Prange(obs[i], dcb, azel[i], vardcb)) == 0) continue;

                if (ioncorr(obs[i].t, obs[i].rtkt, nav, pos, azel[i], ion) != 1) continue;//克罗布歇电离层模型              

                if (tropcorr(obs[i].t, pos, azel[i], trop) != 1) continue;//对流层改正

                /* pseudorange residual */ //伪距残差
                v[nv] = P - (r + dtr - CLIGHT * dts[i][0] + ion[0] + trop[0]);

                for (j = 1; j <= 4; j++) H[nv + 1, j] = j <= 3 ? -e[j - 1] : 1.0;//观测矩阵

                vsat[i] = 1;
                //测量误差
                var[nv++] = Math.Pow(100, 2) * (Math.Pow(0.003, 2) + Math.Pow(0.003, 2) / Math.Sin(azel[i][1])) + vardcb[0] + ion[1] + trop[1] + vare[i];
            }



            return nv;
        }

        /*计算几何距离与方向余弦*/
        public static double geodist(double[] rs, double[] rr, double[] e)
        {
            double r;
            for (int i = 0; i < 3; i++) e[i] = rs[i] - rr[i];
            r = Math.Sqrt(e[0] * e[0] + e[1] * e[1] + e[2] * e[2]);
            for (int i = 0; i < 3; i++) e[i] = e[i] / r;
            return r + OMGE * (rs[0] * rr[1] - rs[1] * rr[0]) / CLIGHT;//考虑了地球自转的影响
        }

        public static double Prange(obs_s obs, dcb_t dcb, double[] azel, double[] var)
        {
            double PC, P1 = 0.0, P1_P2, P1_C1, gamma;
            int prn;

            gamma = (FREQ1 * FREQ1) / (FREQ2 * FREQ2);//f1^2/f2^2
            foreach (var v in obs.type_value)
            {
                if (v.type == "C1") { P1 = v.value; break; }
            }
            prn = int.Parse(obs.sprn.Substring(1, 2));
            P1_C1 = dcb.dcbdata[prn - 1].P1_C1;
            P1_P2 = dcb.dcbdata[prn - 1].P1_P2;

            if (P1 == 0) return 0.0;
            P1 += P1_C1;
            PC = P1 - P1_P2 / (1.0 - gamma);
            var[0] = ERR_CBIAS * ERR_CBIAS;
            return PC;
        }
        /*电离层改正用于标准单点定位*/
        public static int ioncorr(time t, rtktime rtkt, nav_t nav, double[] pos, double[] azel, double[] ion)
        {
            ion[0] = ionmodel(t, rtkt, nav.alpha, nav.beta, pos, azel);
            ion[1] = Math.Pow(ion[0] * ERR_BRDCI, 2);
            return 1;
        }
        /*由广播星历计算的电离层模型,克布罗歇模型*/
        public static double ionmodel(time t, rtktime rtkt, double[] alpha, double[] bela, double[] pos, double[] azel)//由广播星历计算的电离层模型,克布罗歇模型
        {
            double[] ion_default = new double[8] {
                 0.1118E-07,-0.7451E-08,-0.5961E-07, 0.1192E-06,
                 0.1167E+06,-0.2294E+06,-0.1311E+06, 0.1049E+07
            };
            double tt, f, psi, phi, lam, amp, per, x;
            int i;

            if (pos[2] < -1E3 || azel[1] <= 0) return 0.0;
            for (i = 0; i < 4; i++)
            {
                if (alpha[i] != 0 || bela[i] != 0) break;

            }
            if (i < 4)
            {
                for (int j = 0; j < 4; j++)
                {
                    ion_default[j] = alpha[j];
                    ion_default[j + 4] = bela[j];
                }

            }
            /* earth centered angle (semi-circle) */
            psi = 0.0137 / (azel[1] / Math.PI + 0.11) - 0.022;
            /* subionospheric latitude/longitude (semi-circle) */
            phi = pos[0] / Math.PI + psi * Math.Cos(azel[0]);
            if (phi > 0.416) phi = 0.416;
            else if (phi < -0.416) phi = -0.416;
            lam = pos[1] / Math.PI + psi * Math.Sin(azel[0]) / Math.Cos(phi * Math.PI);
            /* geomagnetic latitude (semi-circle) */
            phi += 0.064 * Math.Cos((lam - 1.617) * Math.PI);

            /* local time (s) */
            //tt = 43200.0 * lam + time2gpst(t, &week);
            tt = 43200.0 * lam + rtklibcmn.time2gpst(rtkt, null);
            tt -= Math.Floor(tt / 86400.0) * 86400.0; /* 0<=tt<86400 */

            /* slant factor */
            f = 1.0 + 16.0 * Math.Pow(0.53 - azel[1] / Math.PI, 3.0);

            /* ionospheric delay */
            amp = ion_default[0] + phi * (ion_default[1] + phi * (ion_default[2] + phi * ion_default[3]));
            per = ion_default[4] + phi * (ion_default[5] + phi * (ion_default[6] + phi * ion_default[7]));
            amp = amp < 0.0 ? 0.0 : amp;
            per = per < 72000.0 ? 72000.0 : per;
            x = 2.0 * Math.PI * (tt - 50400.0) / per;

            return CLIGHT * f * (Math.Abs(x) < 1.57 ? 5E-9 + amp * (1.0 + x * x * (-0.5 + x * x / 24.0)) : 5E-9);

        }
        /*对流层改正用于标准单点定位*/
        public static int tropcorr(time t, double[] pos, double[] azel, double[] trop)
        {
            trop[0] = pppcmn.tropmodel(t, pos, azel, REL_HUMI);
            trop[1] = Math.Pow(ERR_SAAS / (Math.Sin(azel[1]) + 0.1), 2);
            return 1;
        }
        /*精密单点定位*/
        public static int pppos(ppp_t pppt, List<obs_s> obs, nav_t nav, dcb_t dcb, station sta, erp_t erp)
        {
            int n = obs.Count, nv = 0, satprn;//当前历元观测的卫星个数
            double[][] rs = new double[n][], dts = new double[n][], azel = new double[n][];
            double[] var = new double[n], vare = new double[n];
            int[] svh = new int[32];
            double[] x = new double[nx], v = new double[2 * n], R = new double[2 * n];
            matrix P = new matrix(37, 37);//
            matrix H = new matrix(2 * n, nx);

            for (int i = 0; i < n; i++)
            {
                rs[i] = new double[6];//卫星坐标和速度
                dts[i] = new double[2];//卫星的钟差和钟漂
                azel[i] = new double[2];//卫星的方位角和高度角
            }

            //状态更新
            udstate(pppt, dcb, obs);
            pppcmn.satpos(obs, nav, sat, clk, pcv, rs, dts, vare, svh);

            for (int i = 0; i < nx; i++)
            {
                x[i] = pppt.x[i];
            }
            for (int i = 0; i < 10; i++)//滤波迭代
            {
                if ((nv = res_ppp(obs, sta, pppt, n, rs, dts, azel, vare, erp, dcb, x, R, v, H, svh)) <= 0) break;

                for (int j = 0; j < nx; j++)
                    for (int k = 0; k < nx; k++)
                        P[j + 1, k + 1] = pppt.P[j + 1, k + 1];

                kalman(x, R, v, P, H, nv);

            }
            for (int i = 0; i < nx; i++)
            {
                pppt.x[i] = x[i];
                for (int j = 0; j < nx; j++)
                    pppt.P[i + 1, j + 1] = P[i + 1, j + 1];
            }
            for (int i = 0; i < 3; i++) pppt.spp.rr[i] = pppt.x[i];

            for (int i = 0; i < n; i++)
            {
                satprn = int.Parse(obs[i].sprn.Substring(1, 2));
                if (pppt.vsat[satprn - 1] == 0) continue;
                pppt.outc[satprn - 1] = 0;

            }

            return 1;
        }

        /*扩展卡尔曼滤波*/
        public static void kalman(double[] x, double[] R, double[] v, matrix P, matrix H, int nv)
        {
            matrix x_, xp_, P_, Pp_, H_, v_, R_;
            int i, j, k;
            int[] ix = new int[nx];
            for (i = k = 0; i < nx; i++)
            {
                if (x[i] != 0 && P[i + 1, i + 1] > 0.0) ix[k++] = i;
            }
            x_ = new matrix(k, 1); xp_ = new matrix(k, 1); P_ = new matrix(k, k); Pp_ = new matrix(k, k); H_ = new matrix(nv, k);
            v_ = new matrix(nv, 1); R_ = new matrix(nv, nv);
            for (i = 0; i < k; i++)
            {
                x_[i + 1, 1] = x[ix[i]];//待估参数
                for (j = 0; j < k; j++)
                    P_[i + 1, j + 1] = P[ix[i] + 1, ix[j] + 1];//系统状态矩阵 k*k
                                                              
            }

            for (i = 0; i < nv; i++)//nv为观测方程个数，伪距和相位
            {
                for (j = 0; j < k; j++)
                    H_[i + 1, j + 1] = H[i + 1, ix[j] + 1];//观测矩阵 nv*k
                v_[i + 1, 1] = v[i];//伪距和相位观测残差 nv*1
                R_[i + 1, i + 1] = R[i];//观测噪声矩阵  nv*nv
            }
            filter(x_, P_, H_, R_, v_, xp_, Pp_);
            for (i = 0; i < k; i++)
            {
                x[ix[i]] = xp_[i + 1, 1];
                for (j = 0; j < k; j++)
                    P[ix[i] + 1, ix[j] + 1] = Pp_[i + 1, j + 1];
            }
        }

        public static void filter(matrix x, matrix P, matrix H, matrix R, matrix v, matrix xp, matrix Pp)
        {
            matrix Q = H * P * matrix.transp(H) + R;        
            matrix K = P * matrix.transp(H) * matrix.inv(Q);
            matrix xp_ = new matrix(x.rows, x.columns);
            xp_ = x + K * v;
            matrix Pp_ = new matrix(P.rows, P.columns);
            Pp_ = P - K * H * P;
            for (int i = 1; i <= xp_.rows; i++)
            {
                for (int j = 1; j <= xp_.columns; j++)
                    xp[i, j] = xp_[i, j];
            }
            for (int i = 1; i <= x.rows; i++)
                for (int j = 1; j <= x.rows; j++)
                    Pp[i, j] = Pp_[i, j];
        }

        public static void udstate(ppp_t ppp, dcb_t dcb, List<obs_s> obs)
        {
            //更新位置
            if ((ppp.x[0] * ppp.x[0] + ppp.x[1] * ppp.x[1] + ppp.x[2] * ppp.x[2]) <= 0)//第一个历元
            {
                for (int i = 0; i < 3; i++) initx(ppp, ppp.spp.rr[i], VAR_POS, i);
            }
            //更新钟差
            initx(ppp, ppp.spp.dtr * CLIGHT, VAR_CLK, 3);

            //更新对流层参数
            uptrop(ppp);
            //更新相位偏差
            upbias_ppp(ppp, obs, dcb);
        }

        public static void initx(ppp_t ppp, double xi, double var, int i)
        {
            int j;
            ppp.x[i] = xi;
            for (j = 0; j < nx; j++)
                ppp.P[i + 1, j + 1] = ppp.P[j + 1, i + 1] = i == j ? var : 0;
        }

        public static void uptrop(ppp_t p3)
        {
            double[] pos = new double[3], azel = new double[2] { 0.0, Math.PI / 2 }, var = new double[1], rr = new double[3];
            double ztd;
            matrix pos_ = new matrix(3, 1);
            for (int i = 0; i < 3; i++) rr[i] = p3.x[i];
            if (p3.x[4] == 0)
            {
                transcoor.ecef2pos(matrix.Array2matrix(rr), pos_);
                for (int i = 0; i < 3; i++) pos[i] = pos_[i + 1, 1];
                ztd = sbstrop(p3.spp.tcurr, pos, azel, var);
                initx(p3, ztd, var[0], 4);
            }
            else
            {
                p3.P[5, 5] += Math.Pow(1E-4, 2) * Math.Abs(p3.tt);
            }
        }

        public static void upbias_ppp(ppp_t p3, List<obs_s> obs, dcb_t dcb)
        {
            double[] meas = new double[2], var = new double[2], bias = new double[32], pos = new double[3], rr = new double[3], phw = new double[1];
            double offset = 0.0;
            matrix pos_ = new matrix(3, 1);
            int i, j, k, sat, n = obs.Count;
            for (i = 0; i < 32; i++) p3.cyslip[i] = 0;
            detslp_gf(p3, obs, n);
            /* reset phase-bias if expire obs outage counter */
            for (i = 0; i < 32; i++)
            {
                if (++p3.outc[i] > 5)
                {
                    initx(p3, 0.0, 0.0, i + 5);
                }
            }
            for (i = 0; i < 3; i++) rr[i] = p3.spp.rr[i];
            transcoor.ecef2pos(matrix.Array2matrix(rr), pos_);
            for (i = 0; i < 3; i++) pos[i] = pos_[i + 1, 1];


            for (i = k = 0; i < n; i++)
            {
                sat = int.Parse(obs[i].sprn.Substring(1, 2));
                j = sat + 4;

                if ((corrmens(obs[i], dcb, pos, null, null, phw, meas, var)) != 1) continue; ;

                bias[i] = meas[0] - meas[1];
                if (p3.x[j] == 0 || p3.cyslip[sat - 1] == 1) continue;
                offset += bias[i] - p3.x[j];
                k++;
            }
            /* correct phase-code jump to enssure phase-code coherency */
            if (k >= 2 && Math.Abs(offset / k) > 0.0005 * CLIGHT)
            {
                for (i = 0; i < 32; i++)
                {
                    j = i + 5;
                    if (p3.x[j] != 0) p3.x[j] += offset / k;
                }
            }
            for (i = 0; i < n; i++)
            {
                sat = int.Parse(obs[i].sprn.Substring(1, 2));
                j = sat + 4;
                p3.P[j + 1, j + 1] += Math.Pow(1E-4, 2) * Math.Abs(p3.tt);
                if (p3.x[j] != 0 && p3.cyslip[sat - 1] != 1) continue;//已初始化且不发生周跳
                if (bias[i] == 0) continue;
                initx(p3, bias[i], VAR_BIAS, j);
            }
        }

        public static double gfmeas(obs_s obs)
        {
            double lam1 = CLIGHT / FREQ1, lam2 = CLIGHT / FREQ2;
            double L1 = 0.0, L2 = 0.0;
            foreach (var v in obs.type_value)
            {
                if (v.type == "L1") L1 = v.value;
                if (v.type == "L2") L2 = v.value;
            }
            if (L1 == 0.0 || L2 == 0.0) return 0.0;

            return lam1 * L1 - lam2 * L2;
        }
        /*GF组合探测周跳*/
        public static void detslp_gf(ppp_t p3, List<obs_s> obs, int n)
        {
            double g0, g1;
            int i, sat;
            for (i = 0; i < n; i++)
            {
                sat = int.Parse(obs[i].sprn.Substring(1, 2));
                if ((g1 = gfmeas(obs[i])) == 0) continue;
                g0 = p3.gf[sat - 1];
                p3.gf[sat - 1] = g1;

                if (g0 != 0 && Math.Abs(g1 - g0) > 0.5)
                    p3.cyslip[sat - 1] = 1;
            }
        }

        public static double sbstrop(time t, double[] pos, double[] azel, double[] var)
        {
            double k1 = 77.604, k2 = 382000.0, rd = 287.054, gm = 9.784, g = 9.80665;
            double[] pos_ = new double[3], met = new double[10], ep = new double[6];
            double zh = 0.0, zw = 0.0, c, sinel = Math.Sin(azel[1]), h = pos[2], m, doy = 0.0;
            time tep;
            int i;
            ep[0] = t.calend[0];
            ep[1] = ep[2] = 1.0; ep[3] = ep[4] = ep[5] = 0.0;
            tep = new time(ep);

            doy = (t.gpsec - tep.gpsec) / 86400.0 + 1;
            if (pos[2] < -100.0 || 10000.0 < pos[2] || azel[1] <= 0)
            {
                var[0] = 0.0;
                return 0.0;
            }
            if (zh == 0.0 || Math.Abs(pos[0] - pos_[0]) > 1E-7 || Math.Abs(pos[1] - pos_[1]) > 1E-7 ||
                 Math.Abs(pos[2] - pos_[2]) > 1.0)
            {
                getmet(pos[0] * R2D, met);
                c = Math.Cos(2.0 * Math.PI * (doy - (pos[0] >= 0.0 ? 28.0 : 211.0)) / 365.25);
                for (i = 0; i < 5; i++) met[i] -= met[i + 5] * c;
                zh = 1E-6 * k1 * rd * met[0] / gm;
                zw = 1E-6 * k2 * rd / (gm * (met[4] + 1.0) - met[3] * rd) * met[2] / met[1];
                zh *= Math.Pow(1.0 - met[3] * h / met[1], g / (rd * met[3]));
                zw *= Math.Pow(1.0 - met[3] * h / met[1], (met[4] + 1.0) * g / (rd * met[3]) - 1.0);
                for (i = 0; i < 3; i++) pos_[i] = pos[i];


            }
            m = 1.001 / Math.Sqrt(0.002001 + sinel * sinel);
            var[0] = 0.12 * 0.12 * m * m;
            return (zh + zw) * m;



        }

        public static void getmet(double lat, double[] met)
        {
            double[][] metprm = new double[5][]{ /* lat=15,30,45,60,75 */
       new  double[10] {1013.25,299.65,26.31,6.30E-3,2.77,  0.00, 0.00,0.00,0.00E-3,0.00},
       new  double[10] {1017.25,294.15,21.79,6.05E-3,3.15, -3.75, 7.00,8.85,0.25E-3,0.33},
       new  double[10] {1015.75,283.15,11.66,5.58E-3,2.57, -2.25,11.00,7.24,0.32E-3,0.46},
       new  double[10] {1011.75,272.15, 6.78,5.39E-3,1.81, -1.75,15.00,5.36,0.81E-3,0.74},
       new  double[10] {1013.00,263.65, 4.11,4.53E-3,1.55, -0.50,14.50,3.39,0.62E-3,0.30}
                     };
            int i, j;
            double a;
            lat = Math.Abs(lat);

            if (lat <= 15.0) for (i = 0; i < 10; i++) met[i] = metprm[0][i];
            else if (lat >= 75.0) for (i = 0; i < 10; i++) met[i] = metprm[4][i];
            else
            {
                j = (int)(lat / 15.0); a = (lat - j * 15.0) / 15.0;
                for (i = 0; i < 10; i++) met[i] = (1.0 - a) * metprm[j - 1][i] + a * metprm[j][i];
            }

        }

        public static int res_ppp(List<obs_s> obs, station sta, ppp_t p3, int n, double[][] rs, double[][] dts, double[][] azel, double[] vare, erp_t erp,
                                    dcb_t dcb, double[] x, double[] R, double[] v, matrix H, int[] svh)
        {
            double r, dtrp, vart = Math.Pow(0.01, 2), elmin = 15 * D2R;
            double[] rr = new double[3], disp = new double[3], pos = new double[3], e = new double[3], meas = new double[2], varm = new double[2];
            double[] dtdx = new double[3], dantr = new double[3], var = new double[nx * 2], dants = new double[2], phw = new double[1];
            matrix pos_ = new matrix(3, 1);
            int i, j, nv = 0, k, sat;

            for (i = 0; i < 3; i++) rr[i] = x[i];

            /* earth tides correction */ //地球潮汐改正 固体潮
            tidedisp(obs[0].t, rtklibcmn.gpst2utc(obs[0].rtkt), rr, erp, disp);
            for (i = 0; i < 3; i++) rr[i] += disp[i];
            transcoor.ecef2pos(matrix.Array2matrix(rr), pos_);
            for (i = 0; i < 3; i++) pos[i] = pos_[i + 1, 1];

            for (i = 0; i < 32; i++) p3.vsat[i] = 0;

            for (i = 0; i < n; i++)
            {
                sat = int.Parse(obs[i].sprn.Substring(1, 2));

                //
                if (p3.spp.vsat[sat - 1] == 0 || svh[i] < 0) continue;
                if ((r = geodist(rs[i], rr, e)) <= 0 || pppcmn.satel(rr, rs[i], azel[i]) < elmin) continue;

                dtrp = pppcmn.prectrop(obs[i].t, pos, azel[i], x[4], dtdx);//精密对流层模型

                pppcmn.satanxpcv(rs[i], rr, pcv, dants, obs[i].sprn);//卫星天线相位偏差，返回每个频率的改正值
                pppcmn.antxmodel(pcv, sta.atxdel, azel[i], dantr, sta.anxtype);
                pppcmn.windup(obs[i].t, p3.soltime, rs[i], rr, phw);

                if (corrmens(obs[i], dcb, pos, dantr, dants, phw, meas, varm) != 1) continue;
                /* satellite clock and tropospheric delay */ //卫星钟差和电离层延迟
                r += -CLIGHT * dts[i][0] + dtrp;

                for (j = 0; j < 2; j++)
                {
                    if (meas[j] == 0) continue;
                    v[nv] = meas[j] - r;
                    for (k = 0; k < nx; k++) H[nv + 1, k + 1] = 0.0;
                    for (k = 0; k < 3; k++) H[nv + 1, k + 1] = -e[k];
                    v[nv] -= x[3]; H[nv + 1, 4] = 1.0;
                    H[nv + 1, 5] = dtdx[0];

                    if (j == 0)
                    {
                        v[nv] -= x[4 + sat];
                        H[nv + 1, 5 + sat] = 1.0;
                    }
                    var[nv] = varm[j] + vare[i] + vart + varerr(azel[i][1], j);

                    if (Math.Abs(v[nv]) > 30) continue;

                    if (j == 0) p3.vsat[sat - 1] = 1;
                    nv++;
                }

            }
            for (i = 0; i < nv; i++)
                R[i] = var[i];

            return nv;
        }

        public static int corrmens(obs_s obs, dcb_t dcb, double[] pos, double[] dantr, double[] dants,
                                    double[] phw, double[] meas, double[] var)
        {
            double c1, c2, L1 = 0.0, L2 = 0.0, P1 = 0.0, P2 = 0.0, P1_C1, gamma, lam1, lam2;
            int i = 0, j = 1, k, sat;

            lam1 = CLIGHT / FREQ1; lam2 = CLIGHT / FREQ2;
            gamma = Math.Pow(FREQ1, 2) / Math.Pow(FREQ2, 2);
            c1 = gamma / (gamma - 1.0);  /*  f1^2/(f1^2-f2^2) */
            c2 = -1.0 / (gamma - 1.0);  /* -f2^2/(f1^2-f2^2) */
            sat = int.Parse(obs.sprn.Substring(1, 2));

            foreach (var v in obs.type_value)
            {
                if (v.type == "L1") L1 = v.value * lam1;
                if (v.type == "L2") L2 = v.value * lam2;
                if (v.type == "P2") P2 = v.value;
                if (v.type == "C1") P1 = v.value;
            }
            P1_C1 = dcb.dcbdata[sat - 1].P1_C1; //L1C/A

            if (L1 == 0.0 || L2 == 0.0 || P1 == 0.0 || P2 == 0.0) return 0;

            meas[0] = c1 * L1 + c2 * L2 - (c1 * lam1 + c2 * lam2) * phw[0];
            P1 += P1_C1;
            meas[1] = c1 * P1 + c2 * P2; //伪距IF组合
            var[1] = Math.Pow(ERR_CBIAS, 2);

            for (k = 0; k < 2; k++)
            {
                if (dants != null) meas[k] -= c1 * dants[i] + c2 * dants[j];
                if (dantr != null) meas[k] -= c1 * dantr[i] + c2 * dantr[j];
            }
            return 1;
        }
        public static void tidedisp(time tutc, rtktime utc, double[] rr, erp_t erp, double[] disp)
        {
            time tut = new time();
            rtktime ut = new rtktime();
            double[] pos = new double[2], E = new double[9], erpv = new double[5], rs = new double[3], rm = new double[3];
            double[] gmst = new double[1], drt = new double[3];
            double r;

            if (erp != null) pppcmn.geterp(tutc, utc, erp, erpv);
            tut.ut1 = tutc.utc + erpv[2];
            ut = rtklibcmn.timeadd(utc, erpv[2]);
            r = Math.Sqrt(rr[0] * rr[0] + rr[1] * rr[1] + rr[2] * rr[2]);
            if (r <= 0) return;
            pos[0] = Math.Asin(rr[2] / r);
            pos[1] = Math.Atan2(rr[1], rr[0]);
            transcoor.mat_xyz2enu(matrix.Array2matrix(pos), E);

            pppcmn.sunmoonpos(tutc, utc, erpv, rs, rm, gmst);
            pppcmn.tide_solid(rs, rm, pos, E, gmst[0], drt);

            for (int i = 0; i < 3; i++) disp[i] += drt[i];
        }

        public static double varerr(double el, int type)
        {
            double a, b, fact = 1.0;
            double sinel = Math.Sin(el);
            if (type == 1) fact *= 100.0;//code
                                        
            fact *= 3.0;//
            a = fact * 0.003;
            b = fact * 0.003;
            return a * a + b * b / sinel / sinel;
        }

        public static int inputobs(obs_t obs, obs_t obss)
        {
            int n = 0, sat;
            time t = new time();
            List<obs_s> obs_ = new List<obs_s>();
            for (int i = 0; i < 32; i++)
            {
                obs_.Add(null);
            }
            obs.obs_b = new List<obs_s>();
            if (0 <= iobss && iobss < obss.n)
            {
                t = obss.obs_b[iobss].t;
            }
            for (int i = 0; i < obss.obs_b.Count; i++)
            {
                if ((iobss + i) >= obss.obs_b.Count) { n = i; break; }
                if ((obss.obs_b[iobss + i].t.gpsec - t.gpsec) > DT) { n = i; break; }

            }
            for (int i = 0; i < n; i++)
            {
                sat = int.Parse(obss.obs_b[iobss + i].sprn.Substring(1, 2));
                obs_[sat - 1] = obss.obs_b[iobss + i];

            }
            foreach (var v in obs_)
            {
                if (v != null) obs.obs_b.Add(v);
            }
            iobss += n;
            return n;//n为当前历元观测的卫星数
        }

        public static void propos(obs_t obss, dcb_t dcb, pcv_t pcv, station sta, nav_t nav, erp_t erp,List<result> res,RichTextBox text)
        {
            int nobs = 0;
            rtktime solt = new rtktime();
            string timestr = null;
            obs_t obs = new obs_t();
            ppp_t p3 = new ppp_t();
            spp_t spp = new spp_t();
            while ((nobs = inputobs(obs, obss)) > 0)
            {
                result re = new result();
                solt = spp.tcur;
                if (pntpos(obs.obs_b, nav, spp, dcb) == 0)
                    continue;
                p3.spp = spp;
                p3.soltime = spp.tcur;               
                if (solt.time_int != 0) p3.tt = rtklibcmn.timediff(p3.soltime, solt);             
                if (pppos(p3, obs.obs_b, nav, dcb, sta, erp) != 1) continue;

                for (int j = 0; j < 3; j++) spp.rr[j] = p3.x[j];
                spp.dtr = p3.x[4];
                re.time = obs.obs_b[0].t.calend;
                re.X = p3.x[0];re.Y = p3.x[1];re.Z = p3.x[2];
                timestr = re.time[0] + "/" + re.time[1] + "/" + re.time[2] + " " + re.time[3] + ":" + re.time[4] + ":" + re.time[5];
                text.AppendText( string.Format("{0,-20}",timestr)+ string.Format("{0,-20}", re.X) + string.Format("{0,-20}", re.Y) + string.Format("{0,-20}", re.Z)+"\r\n");
                
                res.Add(re);
           
            }
        }

        public static void dop(int ns, double[][] azel, double elmin, double[] dops)
        {
            double cosel, sinel;
            int i, n;
            matrix Q = new matrix(4, 4);
            matrix H = new matrix(ns, 4);
            for (i = 0; i < 4; i++) dops[i] = 0;
            for (i = n = 0; i < ns; i++)
            {
                if (azel[i][1] < elmin || azel[i][1] <= 0) continue;
                cosel = Math.Cos(azel[i][1]);
                sinel = Math.Sin(azel[i][1]);
                H[n + 1, 1] = cosel * Math.Sin(azel[i][0]);
                H[n + 1, 2] = cosel * Math.Cos(azel[i][0]);
                H[n + 1, 3] = sinel;
                H[n + 1, 4] = 1.0;
                n++;
            }
            if (n < 4) return;
            Q = matrix.inv(matrix.transp(H) * H);
            dops[0] = Math.Sqrt(Q[1, 1] + Q[2, 2] + Q[3, 3] + Q[4, 4]);
            dops[1] = Math.Sqrt(Q[1, 1] + Q[2, 2] + Q[3, 3]);
            dops[2] = Math.Sqrt(Q[1, 1] + Q[2, 2]);
            dops[3] = Math.Sqrt(Q[3, 3]);
        }

        public static int valsol(double[][] azel, int[] vsat, double elmin, int n, matrix v, int nv, int nx)
        {
            double[][] azels = new double[32][];
            double[] dops = new double[4];
            double vv;
            int i, ns;
            double maxgdop = 30.0;
            double[] chisqr = new double[100] {      /* chi-sqr(n) (alpha=0.001) */
    10.8,13.8,16.3,18.5,20.5,22.5,24.3,26.1,27.9,29.6,
    31.3,32.9,34.5,36.1,37.7,39.3,40.8,42.3,43.8,45.3,
    46.8,48.3,49.7,51.2,52.6,54.1,55.5,56.9,58.3,59.7,
    61.1,62.5,63.9,65.2,66.6,68.0,69.3,70.7,72.1,73.4,
    74.7,76.0,77.3,78.6,80.0,81.3,82.6,84.0,85.4,86.7,
    88.0,89.3,90.6,91.9,93.3,94.7,96.0,97.4,98.7,100 ,
    101 ,102 ,103 ,104 ,105 ,107 ,108 ,109 ,110 ,112 ,
    113 ,114 ,115 ,116 ,118 ,119 ,120 ,122 ,123 ,125 ,
    126 ,127 ,128 ,129 ,131 ,132 ,133 ,134 ,135 ,137 ,
    138 ,139 ,140 ,142 ,143 ,144 ,145 ,147 ,148 ,149
};
            vv = matrix.dotvector(v, v);
            if (nv > nx && vv > chisqr[nv - nx - 1])
            {
                Console.WriteLine("chi-square error");
                return 0;
            }
            for (i = 0; i < 32; i++) azels[i] = new double[2];
            for (i = ns = 0; i < n; i++)
            {
                if (vsat[i] == 0) continue;
                azels[ns][0] = azel[i][0];
                azels[ns][1] = azel[i][1];
                ns++;
            }
            dop(ns, azels, elmin, dops);
            if (dops[0] <= 0 || dops[0] > maxgdop)
            {
                Console.WriteLine("gdop error");
                return 0;
            }
            return 1;
        }
    }

    public class spp_t
    {
        public double[] rr = new double[3];
        public double t = 0.0;
        public time tcurr = new time();
        public rtktime tcur = new rtktime();
        public double dtr;
        public double[] x = new double[37];//3+1+1+n GPS nmax=32                                           
        public int[] vsat = new int[32];
    }

    public class ppp_t
    {
        public double tt = 0.0;//上一个历元和当前历元的时间差
        public double[] x = new double[37];//估计参数                                         
        public matrix P = new matrix(37, 37);//待估参数的协方差，系统噪声矩阵
        public double[] cyslip = new double[32];
        public double[] gf = new double[32];
        public double[] vsat = new double[32];
        public double[] outc = new double[32];
        public time tcurrent;
        public rtktime soltime = new rtktime();
        public spp_t spp = new spp_t();
    }
    public class result
    {
        public double[] time = new double[6];
        public double X;
        public double Y;
        public double Z;
        public double error_E;
        public double error_N;
        public double error_U;
    }
}
