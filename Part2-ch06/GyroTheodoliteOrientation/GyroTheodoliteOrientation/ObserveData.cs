using System;

namespace WindowsFormsApp1
{
    /// <summary>
    /// 观测数据
    /// </summary>
    class ObserveData
    {
        //private float[] preMeaZeroPos = new float[3];   // 测前零位的3个逆转点读数
        //private float[] postMeaZeroPos = new float[3]; // 测后零位的3个逆转点读数
        //private double[] inverPointRead = new double[5];    // 陀螺北方向值的5个逆转点读数
        //private double[] MeaLineDir = new double[2];    // 地面测线方向
        private double gyroAzi;   // 陀螺边方位角
        private double coorAzi;    // 坐标方位角
        private double delta;    // 仪器常数
        //private double oriPointHorCoor;   // 定向点横坐标

        /// <summary>
        /// 构造器
        /// </summary>
        public ObserveData(string[] rowText)
        {
            //坐标边方位角
            coorAzi = AngleTransUtil.DMSToDegree(Convert.ToDouble(rowText[1]));
            //陀螺边方位角
            gyroAzi = AngleTransUtil.DMSToDegree(Convert.ToDouble(rowText[2]));
            // 定向点横坐标
            //OriPointHorCoor = Convert.ToDouble(rowText[3]);
        }

        /// <summary>
        /// Get和Set方法
        /// </summary>
        public double Delta { get { return delta;} set { delta = value;} }
        public double GyroAzi { get { return gyroAzi;} set { gyroAzi = value;} }
        public double CoorAzi { get { return coorAzi; } set { coorAzi = value; } }

    }
}
