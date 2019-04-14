using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPP
{
    class transcoor
    {
        static double FE_WGS84 = 1.0 / 298.257223563;/* earth flattening (WGS84) */
        static double RE_WGS84 = 6378137.0;/* earth semimajor axis (WGS84) (m) */

        /// <summary>
        /// 空间直角坐标转换为大地坐标
        /// </summary>
        /// <param name="r">为直角坐标xyz</param>
        /// <param name="pos">返回大地坐标BLH（弧度/m）</param>
        public static void ecef2pos(matrix r, matrix pos)//r为直角坐标，pos为返回的大地坐标
        {
            double e2 = FE_WGS84 * (2.0 - FE_WGS84), r2 = r[1, 1] * r[1, 1] + r[2, 1] * r[2, 1], z, zk, v = RE_WGS84, sinp;
            for (z = r[3, 1], zk = 0.0; Math.Abs(z - zk) >= 1E-4;)
            {
                zk = z;
                sinp = z / Math.Sqrt(r2 + z * z);
                v = RE_WGS84 / Math.Sqrt(1.0 - e2 * sinp * sinp);
                z = r[3, 1] + v * e2 * sinp;
            }
            pos[1, 1] = r2 > 1E-12 ? Math.Atan(z / Math.Sqrt(r2)) : (r[3, 1] > 0.0 ? Math.PI / 2.0 : -Math.PI / 2.0);
            pos[2, 1] = r2 > 1E-12 ? Math.Atan2(r[2, 1], r[1, 1]) : 0.0;
            pos[3, 1] = Math.Sqrt(r2 + z * z) - v;
        }
        /// <summary>
        /// 大地坐标转换为空间直角坐标
        /// </summary>
        /// <param name="pos">大地坐标BLH（弧度/m）</param>
        /// <param name="r">返回的空间直角坐标</param>
        public static void pos2ecef(matrix pos, matrix r)
        {
            double sinp = Math.Sin(pos[1, 1]), cosp = Math.Cos(pos[1, 1]), sinl = Math.Sin(pos[2, 1]), cosl = Math.Cos(pos[2, 1]);
            double e2 = FE_WGS84 * (2.0 - FE_WGS84), v = RE_WGS84 / Math.Sqrt(1.0 - e2 * sinp * sinp);

            r[1, 1] = (v + pos[3, 1]) * cosp * cosl;
            r[2, 1] = (v + pos[3, 1]) * cosp * sinl;
            r[3, 1] = (v * (1.0 - e2) + pos[3, 1]) * sinp;
        }

        /// <summary>
        /// 空间直角坐标转换为站心坐标
        /// </summary>
        /// <param name="xyz">测站的坐标</param>
        /// <param name="r">卫星至测站的向量</param>
        /// <param name="e">返回的站心坐标</param>
        public static void xyz2enu(matrix xyz, matrix r, matrix e)//r为卫星至测站的向量
        {
            matrix pos = new matrix(3, 1);
            ecef2pos(xyz, pos);
            double sinB = Math.Sin(pos[1, 1]), sinL = Math.Sin(pos[2, 1]), cosB = Math.Cos(pos[1, 1]), cosL = Math.Cos(pos[2, 1]);
            matrix trans = new matrix(3, 3);
            trans[1, 1] = -sinL; trans[1, 2] = cosL; trans[1, 3] = 0;
            trans[2, 1] = -sinB * cosL; trans[2, 2] = -sinB * sinL; trans[2, 3] = cosB;
            trans[3, 1] = cosB * cosL; trans[3, 2] = cosB * sinL; trans[3, 3] = sinB;
            matrix e_ = trans * r;
            for (int i = 1; i <= 3; i++)
                e[i, 1] = e_[i, 1];
        }
        /*xyz转换至enu的转换矩阵*/
        public static void mat_xyz2enu(matrix pos, double[] E)
        {
            double sinp = Math.Sin(pos[1, 1]), cosp = Math.Cos(pos[1, 1]), sinl = Math.Sin(pos[2, 1]), cosl = Math.Cos(pos[2, 1]);

            E[0] = -sinl; E[3] = cosl; E[6] = 0.0;
            E[1] = -sinp * cosl; E[4] = -sinp * sinl; E[7] = cosp;
            E[2] = cosp * cosl; E[5] = cosp * sinl; E[8] = sinp;

        }
    }
}
