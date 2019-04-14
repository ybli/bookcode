using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace PPP
{
    class Read
    {
        const double D2R = Math.PI / 180;//度转弧度
        const double AS2R = D2R / 3600.0;
        const double CLIGHT = 299792458.0;         /* speed of light (m/s) */

        /*读取观测值文件 支持ver 2.11*/
        public static int readobs(string path, obs_t obs, station sta)
        {
            using (StreamReader sr = new StreamReader(path))
            {
                if (readobsh(sr, obs, sta) == 1 && (sta.ver < 3 && sta.ver > 0))
                {
                    string line = "";
                    while (!sr.EndOfStream)
                    {
                        time tcurrent = new time(); string[] sprn = new string[64];
                        rtktime rtktcur = new rtktime();
                        int ns = 0;//每个历元观测到的卫星数
                        line = sr.ReadLine();
                        if (line != null)//读取含历元时间信息和卫星数的一行
                        {
                            tcurrent = new time(line.Substring(0, 26), "o");//读取时间信息
                            rtklibcmn.str2time(line.Substring(0, 26), rtktcur);
                            ns = int.Parse(line.Substring(29, 3));//读取当前历元观测到的卫星数
                            sprn = new string[ns];//卫星prn
                            for (int i = 0, j = 32; i < ns; i++, j += 3)
                            {
                                if (j >= 68)
                                {
                                    line = sr.ReadLine();
                                    j = 32;
                                }
                                sprn[i] = line.Substring(j, 3);
                            }
                        }

                        for (int i = 0; i < ns; i++)//读取每个卫星相应的观测值
                        {
                            obs_s sat = new obs_s();
                            sat.t = tcurrent;
                            sat.rtkt = rtktcur;
                            sat.sprn = sprn[i];
                            line = sr.ReadLine();
                            for (int k = 0, j = 0; k < obs.ntype; k++, j += 16)//读取一颗卫星的所有观测值信息
                            {
                                obssat obsat = new obssat();
                                if (j >= 80)
                                {
                                    line = sr.ReadLine();
                                    j = 0;
                                }
                                obsat.type = obs.obstype[k];
                                if (j >= line.Length) obsat.value = 0;
                                else
                                {
                                    if (line.Substring(j, 14).Trim() == "") obsat.value = 0;
                                    else
                                    {
                                        obsat.value = double.Parse(line.Substring(j, 14));
                                    }
                                }
                                sat.type_value.Add(obsat);
                            }
                            obs.obs_b.Add(sat);
                        }
                        obs.n += ns;
                    }
                    return 1;
                }
                else return 0;




            }



        }
        /*读取观测值头文件*/
        public static int readobsh(StreamReader sr, obs_t obs, station sta)
        {
            int flag = 0;
            string line = "";
            while (!sr.EndOfStream)
            {
                line = sr.ReadLine();
                if (line.Contains("RINEX VERSION / TYPE"))
                {
                    sta.ver = double.Parse(line.Substring(0, 9));
                }
                if (line.Contains("MARKER NAME"))
                {
                    sta.name = line.Substring(0, 59).Trim();
                }
                if (line.Contains("ANT # / TYPE"))//接收机天线类型
                {
                    sta.anxtype = line.Substring(20, 20);

                }
                if (line.Contains("ANTENNA: DELTA H/E/N"))
                {
                    string[] ss = line.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
                    sta.atxdel[2] = double.Parse(ss[0]);    //ss: H E N 
                    sta.atxdel[0] = double.Parse(ss[1]);//atxdel: E N U 
                    sta.atxdel[1] = double.Parse(ss[2]);

                }
                if (line.Contains("APPROX POSITION XYZ"))
                {
                    for (int i = 0, j = 0; i < 3; i++, j += 14)
                        sta.xyz[i] = double.Parse(line.Substring(j, 14));
                }
                if (line.Contains("# / TYPES OF OBSERV"))
                {
                    obs.ntype = int.Parse(line.Substring(0, 6));
                    for (int i = 0, j = 10; i < obs.ntype; i++, j += 6)
                    {
                        if (j > 58)
                        {
                            line = sr.ReadLine();
                            j = 10;
                        }
                        obs.obstype[i] = line.Substring(j, 2);
                    }
                }
                if (line.Contains("END OF HEADER")) return 1;

            }

            return flag;
        }
        /*读取IGS精密星历*/
        public static int readsp3(string path, sp3_t sp3)
        {
            using (StreamReader sr = new StreamReader(path))
            {
                double value = 0.0, std = 0.0, ba;
                double[] bfact = new double[2];
                int flag = 0;
                string line = "";
                for (int i = 0; i < 22; i++)//IGS
                {
                    line = sr.ReadLine();
                    if (i == 2)
                    { sp3.ns = int.Parse(line.Substring(4, 2)); }
                    if (i == 14)
                    {
                        bfact[0] = str2num(line, 3, 10);
                        bfact[1] = str2num(line, 14, 12);
                    }

                }
                if (sp3.ns == 0) return 0;
                while (!sr.EndOfStream)
                {
                    time t1 = new time();
                    rtktime rtkt1 = new rtktime();
                    line = sr.ReadLine();
                    if (line.Contains("EOF")) break;
                    if (line.Contains("*"))
                    {
                        t1 = new time(line, "sp3");
                        rtklibcmn.str2time(line.Substring(1), rtkt1);
                    }
                    for (int i = 0; i < sp3.ns; i++)
                    {
                        sp3b sb = new sp3b();
                        sb.t = t1;
                        sb.rtkt = rtkt1;
                        line = sr.ReadLine();
                        if (line.Substring(1, 1) == "G" && line.Substring(0, 1) == "P")//只读取GPS卫星
                        {
                            sb.prn = line.Substring(1, 3);
                            for (int j = 0; j < 4; j++)
                            {
                                value = str2num(line, 4 + j * 14, 14);
                                std = str2num(line, 61 + j * 3, j < 3 ? 2 : 3);
                                if (value != 0 && Math.Abs(value - 999999.999999) >= 1E-6)//读取卫星XYZ坐标，单位为米
                                {
                                    sb.xyzt[j] = value * (j < 3 ? 1000.0 : 1E-6);
                                }
                                if ((ba = bfact[j < 3 ? 0 : 1]) > 0.0 && std > 0.0)
                                {
                                    sb.std[j] = Math.Pow(ba, std) * (j < 3 ? 1E-3 : 1E-12);
                                }
                            }
                        }
                        sp3.sp3_b.Add(sb);
                    }
                }
                return flag;
            }
        }
        /*读取精密钟差文件*/
        public static int readclk(string path, clk_t clkt)
        {
            using (StreamReader sr = new StreamReader(path))
            {
                string line = "";
                while (!line.Contains("END OF HEADER")) line = sr.ReadLine();

                while (!sr.EndOfStream)
                {
                    line = sr.ReadLine();
                    if (line.Substring(0, 2) == "AS")
                    {
                        clk_b clb = new clk_b();
                        clb.prn = line.Substring(3, 3);
                        clb.t = new time(line, "clk");
                        rtklibcmn.str2time(line.Substring(8, 34), clb.rtkt);
                        for (int i = 0, j = 40; i < 2; i++, j += 20)
                            clb.clock[i] = str2num(line, j, 19);
                        clb.std = (float)clb.clock[1];
                        clkt.clk.Add(clb);
                    }
                }
            }
            if (clkt.clk.Count <= 0) return 0;
            return 1;

        }
        /*读取导航文件*/
        public static void readnav(string path, nav_t nav)
        {
            using (StreamReader sr = new StreamReader(path))
            {
                string line = "";
                readnavh(sr, nav);
                if (nav.sys == "G" && nav.ver < 3)
                {
                    while ((line = sr.ReadLine()) != null)//读取到最后一行为null结束
                    {
                        // string s;
                        navb nav_b = new navb();
                        nav_b.prn = "G" + String.Format("{0:D2}", int.Parse(line.Substring(0, 2).Trim()));
                        nav_b.t = new time(line.Substring(3, 19), "n");
                        rtklibcmn.str2time(line.Substring(3, 19), nav_b.rtktoc);
                        for (int i = 0, j = 22; i < 3; i++, j += 19)
                        {
                            string s = line.Substring(j, 19);
                            if (s.Contains("D")) s = s.Replace("D", "e");
                            nav_b.af[i] = double.Parse(s);
                        }

                        for (int i = 0; i < 7; i++)
                        {
                            line = sr.ReadLine();
                            if (i == 2 && line != null)
                            {
                                string s = line.Substring(3, 19);
                                if (s.Contains("D")) s = s.Replace("D", "e");
                                nav_b.toes = double.Parse(s);
                                nav_b.rtktoe = rtklibcmn.adjweek(rtklibcmn.gpst2time(1999, nav_b.toes), nav_b.rtktoc);
                            }
                        }
                        nav.navdata.Add(nav_b);
                    }
                }
            }
        }
        /*读取导航文件头*/
        public static void readnavh(StreamReader sr, nav_t nav)
        {
            string line = "";
            while (!line.Contains("END OF HEADER"))
            {
                line = sr.ReadLine();
                if (line.Contains("RINEX VERSION / TYPE"))//只提取了第一行的信息
                {
                    nav.ver = double.Parse(line.Substring(0, 9));
                    if (line.Substring(40, 1).Trim() == "") nav.sys = "G";
                    else nav.sys = line.Substring(40, 1).Trim();
                }
                if (line.Contains("ION ALPHA"))
                {
                    string ss = line.Replace('D', 'E');
                    string[] s = ss.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
                    for (int i = 0; i < 4; i++) nav.alpha[i] = double.Parse(s[i]);
                }
                if (line.Contains("ION BETA"))
                {
                    string ss = line.Replace('D', 'E');
                    string[] s = ss.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
                    for (int i = 0; i < 4; i++) nav.beta[i] = double.Parse(s[i]);
                }
            }

        }
        /*读取天线文件*/
        public static void readatx(string path, pcv_t pcvs)
        {
            using (StreamReader sr = new StreamReader(path))
            {
                string line = "";
                pcvb pcvd = null;
                int state = 0, freq = 0;
                while (!sr.EndOfStream)
                {
                    line = sr.ReadLine();
                    if (line.Contains("COMMENT")) continue;
                    if (line.Contains("START OF ANTENNA"))
                    { state = 1; }
                    if (line.Contains("END OF ANTENNA"))
                    {
                        state = 0;
                        if (pcvd != null)
                            pcvs.pcvdata.Add(pcvd);
                        pcvd = null;
                    }
                    if (state == 0) continue;
                    if (line.Contains("TYPE / SERIAL NO"))
                    {
                        string type = line.Substring(0, 20);
                        string sys = line.Substring(20, 20);
                        pcvd = new pcvb();
                        pcvd.type = type;
                        pcvd.prn = sys.Trim();
                        // }
                        continue;
                    }
                    if (line.Contains("VALID FROM"))
                    {
                        pcvd.ts = new time(line.Substring(0, 43), "atx");
                        rtklibcmn.str2time(line.Substring(0, 43), pcvd.rtkts);
                        continue;
                    }
                    if (line.Contains("VALID UNTIL"))
                    {
                        pcvd.te = new time(line.Substring(0, 43), "atx");
                        rtklibcmn.str2time(line.Substring(0, 43), pcvd.rtkte);
                        continue;
                    }
                    if (line.Contains("START OF FREQUENCY"))
                    {
                        freq = int.Parse(line.Substring(4, 2));
                        if (freq < 1 || freq >= 3) continue;
                        continue;
                    }
                    if (line.Contains("END OF FREQUENCY"))
                    {
                        freq = 0; continue;
                    }
                    if (line.Contains("NORTH / EAST / UP"))
                    {
                        if (freq < 1 || freq >= 3) continue;
                        string[] ss = line.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
                        pcvd.off[freq - 1] = new double[3];
                        for (int i = 0; i < 3; i++)
                            pcvd.off[freq - 1][i] = double.Parse(ss[i]) * 1E-3;//N E U (m)
                        if (pcvd.prn == "")//接收机
                        { double tep = pcvd.off[freq - 1][0]; pcvd.off[freq - 1][0] = pcvd.off[freq - 1][1]; pcvd.off[freq - 1][1] = tep; }
                        continue;
                    }
                    if (line.Contains("NOAZI"))
                    {
                        if (freq < 1 || freq >= 3) continue;
                        string[] ss = line.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
                        pcvd.var[freq - 1] = new double[ss.Length - 1];
                        for (int i = 0; i < ss.Length - 1; i++)
                            pcvd.var[freq - 1][i] = double.Parse(ss[i + 1]) * 1E-3;//天线相位变化，变换成米
                        continue;
                    }
                }
            }
        }
        /*读取地球自转文件*/
        public static void readerp(string path, erp_t erp)
        {
            using (StreamReader sr = new StreamReader(path))
            {
                string line = "";
                int state = 0;
                erpb erpd = null;
                while (!sr.EndOfStream)
                {
                    line = sr.ReadLine();
                    if (line.Substring(0, 5).Trim() == "MJD")
                    {
                        state = 1; line = sr.ReadLine(); continue;
                    }
                    if (state == 1 && line != null)
                    {
                        string[] ss = line.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
                        erpd = new erpb();
                        if (ss.Length > 14)
                        {
                            erpd.mjd = double.Parse(ss[0]);
                            erpd.xp = double.Parse(ss[1]) * 1E-6 * AS2R;
                            erpd.yp = double.Parse(ss[2]) * 1E-6 * AS2R;
                            erpd.ut1_utc = double.Parse(ss[3]) * 1E-7;
                            erpd.lod = double.Parse(ss[4]) * 1E-7;
                            erpd.xpr = double.Parse(ss[12]) * 1E-6 * AS2R;
                            erpd.ypr = double.Parse(ss[13]) * 1E-6 * AS2R;
                            erp.erpdata.Add(erpd);
                        }

                    }
                }
            }
        }
        /*读取码偏差文件*/
        public static void readdcb(string[] path, dcb_t dcb)
        {
            for (int i = 0; i < 32; i++)
                dcb.dcbdata.Add(new dcbb());

            for (int i = 0; i < path.Length; i++)
            {
                using (StreamReader sr = new StreamReader(path[i]))
                {
                    dcbb data;
                    string line = "";
                    int state = 0, nprn = 0;
                    string[] ss = null;
                    while (!sr.EndOfStream)
                    {
                        line = sr.ReadLine();
                        if (line.Contains("DIFFERENTIAL (P1-C1) CODE BIASES"))
                        {
                            state = 1;
                        }
                        if (line.Contains("DIFFERENTIAL (P1-P2) CODE BIASES")) state = 2;
                        if (state == 0) continue;
                        if ((ss = line.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries)) == null || ss.Count() != 3) continue;
                       
                        nprn = int.Parse(ss[0].Substring(1, 2));
                        if (nprn != 0 && nprn <= 32 && state == 1)
                        {
                            dcb.dcbdata[nprn - 1].prn = nprn;
                            dcb.dcbdata[nprn - 1].P1_C1 = double.Parse(ss[1]) * 1E-9 * CLIGHT;
               
                        }
                        if (nprn != 0 && nprn <= 32 && state == 2)
                        {
                            dcb.dcbdata[nprn - 1].prn = nprn;
                            dcb.dcbdata[nprn - 1].P1_P2 = double.Parse(ss[1]) * 1E-9 * CLIGHT;
                        }
                    }
                }
            }

        }

        /// <summary>
        /// 字符串转double
        /// </summary>
        /// <param name="str">字符串</param>
        /// <param name="i">字符串的起始位置</param>
        /// <param name="n">转换的字符长度</param>
        /// <returns></returns>
        public static double str2num(string str, int i, int n)
        {
            double value = 0.0;
            int num = str.Count();
            if (num <= 0 || num < (i + n)) return 0.0;
            value = str.Substring(i, n).Trim() == "" ? 0.0 : double.Parse(str.Substring(i, n));
            return value;
        }
    }

    public class obs_t//观测文件
    {
        public int n, ntype;
        public string[] obstype = new string[64];
        public List<obs_s> obs_b = new List<obs_s>();
    }
    public class obs_s//观测文件中一个卫星的信息
    {
        public time t;
        public rtktime rtkt = new rtktime();
        public string sprn = "";
        public List<obssat> type_value = new List<obssat>();
   
    }

    public class obssat//卫星的观测值
    {
        public string type = "";
        public double value;
    }
    public class station//测站信息
    {
        public string name = "";
        public double ver = 0;
        public string anxtype = "";
        public double[] xyz = new double[3];
        public double[] atxdel = new double[3];
    }

    public class sp3_t
    {
        public int ns;
        public List<sp3b> sp3_b = new List<sp3b>();
    }
    public class sp3b
    {
        public time t;
        public rtktime rtkt = new rtktime();
        public string prn = "";
        public double[] xyzt = new double[4];
        public double[] std = new double[4];
        public double[] vel = new double[3];
    }

    public class clk_t
    {
        public List<clk_b> clk = new List<clk_b>();
    }
    public class clk_b
    {
        public time t;
        public rtktime rtkt = new rtktime();
        public string prn = "";
        public double[] clock = new double[2];//clock[0]为钟差，clock[1]为钟差方差
        public float std;
    }

    public class nav_t//导航文件结构体
    {
        public double ver;
        public string sys;
        public double[] alpha = new double[4];
        public double[] beta = new double[4];
        public List<navb> navdata = new List<navb>();
    }
    public class navb
    {
        public string prn;
        public time t;
        public double toes;
        public rtktime rtktoc = new rtktime(), rtktoe = new rtktime();
        public double[] af = new double[3];

    }

    public class pcv_t
    {

        public List<pcvb> pcvdata = new List<pcvb>();
    }

    public class pcvb
    {
        public time ts, te;
        public rtktime rtkts = new rtktime(), rtkte = new rtktime();
        public string prn = "";
        public string type = "";
        public double[][] off = new double[2][];
        public double[][] var = new double[2][];

    }

    public class erp_t
    {
        public List<erpb> erpdata = new List<erpb>();
    }

    public class erpb
    {
        public double mjd;
        public double xp, yp;
        public double xpr, ypr;
        public double ut1_utc;
        public double lod;
    }

    public class dcb_t
    {
        public List<dcbb> dcbdata = new List<dcbb>(32);
    }
    public class dcbb
    {
        public int prn;
        public double P1_C1;
        public double P1_P2;

    }
}
