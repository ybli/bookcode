using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniClosedLoopSearch
{
    /// <summary>
    /// 水准点的性质。有已知点和未知点两种。
    /// </summary>
    enum PointNature
    {
        known,//已知点。
        unknow//未知点。
    }
    /// <summary>
    /// 水准点
    /// </summary>
    public class LevelingPoint
    {
        string strLevelingPointName;//水准点名称。
        /// <summary>
        /// //水准点名称。
        /// </summary>
        public string StrLevelingPointName
        {
            get { return strLevelingPointName; }
            set { strLevelingPointName = value; }
        }
        int levelingPointNum;//水准点编号。
        /// <summary>
        /// //水准点编号。
        /// </summary>
        public int LevelingPointNum
        {
            get { return levelingPointNum; }
            set { levelingPointNum = value; }
        }
        int unknowPointNum;//未知点编号。为了计算平差的B和L矩阵准备的。
        /// <summary>
        /// 未知点编号。为了计算平差的B和L矩阵准备的。其中-1代表已知点，0，1，2...代表未知点的序号。
        /// </summary>
        public int UnknowPointNum
        {
            get { return unknowPointNum; }
            set { unknowPointNum = value; }
        }

        private PointNature _pointNature;//点的性质。
        /// <summary>
        /// //点的性质。
        /// </summary>
        internal PointNature PointNature
        {
            get { return _pointNature; }
            set { _pointNature = value; }
        }
        double levelingHeight;//水准点的高程
        /// <summary>
        /// //水准点的高程
        /// </summary>
        public double LevelingHeight
        {
            get { return levelingHeight; }
            set { levelingHeight = value; }
        }

       
    }
}
