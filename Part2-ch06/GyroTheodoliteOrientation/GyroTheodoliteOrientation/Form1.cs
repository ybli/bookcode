using System;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Collections.Generic;
using System.Drawing;

namespace WindowsFormsApp1
{
    /// <summary>
    /// 程序的主窗体
    /// </summary>
    public partial class MainWindow : Form
    {
        private bool isReadData = false;    // 是否读入了数据
       // private bool isCalGtroAzi = false;  // 是否计算陀螺方位角
        private bool isCalma = false;  // 是否计算了井下坐标方位角
        private bool isEstiThroErr = false; // 是否计算了贯通误差

        private double ml;  // 量边中误差
        private double mb;  // 导线测角中误差
        private double mv;  // 仪器常数中误差
        private double ma;  // 井下陀螺中误差
        private double Mx;  // 最终误差
        private int PointNum =0;
        private List<ObserveData> groundData = new List<ObserveData>();     // 地面观测数据
        private List<ObserveData> downholeData = new List<ObserveData>();   // 井下观测数据
        private List<double> latList = new List<double>();                  // 当地纬度
        private List<double> groundCoorAziList = new List<double>();        // 地面已知坐标方位角 
        private List<double> downholeCoorAziList = new List<double>();      // 井下坐标方位角 
        private List<TraverseEdge> traverseEdge = new List<TraverseEdge>(); // 加测陀螺边的导线
        private List<TraverseEdge> gyroEdge = new List<TraverseEdge>();    //陀螺边
        private List<TraverseEdge> openTraverse = new List<TraverseEdge>();    //支导线
        private List<TraverseEdge> connectTraverse = new List<TraverseEdge>();    //符合导线
        

        public MainWindow()
        {
            InitializeComponent();

            // 初始化表格的列
            DataTableGridView.Columns.Add("列1", "序号");
            DataTableGridView.Columns.Add("列2", "地理方位角");
            DataTableGridView.Columns.Add("列3", "陀螺方位角");
            DataTableGridView.Columns.Add("列4", "仪器常数");
            DataTableGridView.Columns.Add("列5", "差值");

            dataGridView1.Columns.Add("列1", "导线边");
            dataGridView1.Columns.Add("列2", "起点");
            dataGridView1.Columns.Add("列3", "终点");
            dataGridView1.Columns.Add("列4", "陀螺边");
            dataGridView1.Columns.Add("列5", "观测次数");

            pointBitmap = new Bitmap(600, 300);
        }

        /// <summary>
        /// 打开并读取数据文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OpenDataFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MyOpenFileDialog.InitialDirectory = Application.StartupPath;
            MyOpenFileDialog.Filter = "txt file(*.txt)|*.txt";  // 文件类型过滤器设置为txt文件
            bool estiThroErrData = false;
            if (MyOpenFileDialog.ShowDialog() == DialogResult.OK)
            {
                string[] allText = File.ReadAllLines(MyOpenFileDialog.FileName, Encoding.UTF8); // 读取txt文件中全部的数据
                for (int i = 0; i < allText.Length-1; i++)
                {
                    string[] rowText = allText[i].Replace(" ", "").Replace(",,", ",").Split(',');   // 每一行的数据
                    if(rowText[0] == "")
                    {
                        estiThroErrData = true;
                        i = i + 1;
                        rowText = allText[i].Replace(" ", "").Replace(",,", ",").Split(',');   // 每一行的数据
                        ml = Convert.ToDouble(rowText[0]);
                        mb = AngleTransUtil.DegreeToRand(Convert.ToDouble(rowText[1]) / 3600);   //将mb转成弧度
                        //ma = DegreeToRand(DMSToDegree(Convert.ToDouble(rowText[2]))); 
                        continue;
                    }
                    if(estiThroErrData)
                    {   // 贯通数据
                        bool isDirEdge = Convert.ToInt16(rowText[3]) == 1;
                        int surveyNum = Convert.ToInt16(rowText[4]);
                        TraversePoint startPoint = new TraversePoint(rowText);
                        startPoint.Pointindex = PointNum;
                        rowText = allText[i + 1].Replace(" ", "").Replace(",,", ",").Split(',');   // 每一行的数据
                        TraversePoint endPoint = new TraversePoint(rowText);
                        endPoint.Pointindex = PointNum +1;
                        TraverseEdge traverEdge = new TraverseEdge(startPoint, endPoint, isDirEdge,surveyNum);
                        traverseEdge.Add(traverEdge);
                        PointNum ++;
                    }
                    else
                    {
                        // 陀螺经纬仪观测数据
                        ObserveData obData = new ObserveData(rowText);  // 观测数据
                        if(rowText[0] =="DM")
                        {  //地面
                            groundData.Add(obData);
                        }
                        if (rowText[0] == "JX")
                        {
                            //井下
                            downholeData.Add(obData);
                        }
                    }
                    
                }
            }
            isReadData = true;
            InitState();
            // 更新表格数据
            InsertObDataIntoTable();
        }

        private void InitState()
        {
            //isCalGtroAzi = false;
            isCalma = false;
            isEstiThroErr = false;
            DataTableGridView.Rows.Clear();
            ReportTextBox.Clear();

            /**
             * 初始化数据
             */
            //初始化陀螺边
            int gyroEdgeNum = 0;
            for (int i = 0; i < traverseEdge.Count; i++)
            {
                if (traverseEdge[i].IsGyroDirEdge)
                {

                    gyroEdge.Add(traverseEdge[i]);
                    gyroEdgeNum += 1;
                }
            }

            //初始化符合导线和支导线
            TraverseEdge tempTraverse =new TraverseEdge();    //符合导线
            bool isstartEdge = false;   //第一个边是否是陀螺边
            bool isendEdge = false;
            if (traverseEdge[0].IsGyroDirEdge)
            {
                isstartEdge = true;
                tempTraverse.StartPoint = traverseEdge[0].EndPoint;
                tempTraverse.SurveyNum = 1;
            }
            else
            {
                isstartEdge = false;
                tempTraverse.StartPoint = traverseEdge[0].StartPoint;
            }
            for (int i = 0; i < traverseEdge.Count-1; i++)
            {
                tempTraverse.SurveyNum += 1;
                //判断结束边是否是陀螺边
                if (traverseEdge[i].IsGyroDirEdge)
                    {
                        isendEdge = true;
                        if (isstartEdge && isendEdge)
                        {
                            tempTraverse.EndPoint = traverseEdge[i].StartPoint;
                            tempTraverse.SurveyNum -= 1;
                            connectTraverse.Add(tempTraverse);
                            isstartEdge = true;
                            tempTraverse = new TraverseEdge();
                            tempTraverse.StartPoint = traverseEdge[i].EndPoint;
                        }
                        else
                        {
                            tempTraverse.EndPoint = traverseEdge[i].StartPoint;
                            tempTraverse.SurveyNum -= 1;
                            openTraverse.Add(tempTraverse);
                            isstartEdge = true;
                            tempTraverse = new TraverseEdge();
                            tempTraverse.StartPoint = traverseEdge[i].EndPoint;
                        }
                    }    
            }
            
            if (traverseEdge[traverseEdge.Count-1].IsGyroDirEdge)
            {
                
                isendEdge = true;
                if (isstartEdge && isendEdge)
                {
                    tempTraverse.EndPoint = traverseEdge[traverseEdge.Count - 1].StartPoint;
                    connectTraverse.Add(tempTraverse);
                }
                else
                {
                    tempTraverse.EndPoint = traverseEdge[traverseEdge.Count - 1].EndPoint;
                    openTraverse.Add(tempTraverse);
                }
            }
            else
            {
                tempTraverse.SurveyNum += 1;
                tempTraverse.EndPoint = traverseEdge[traverseEdge.Count - 1].EndPoint;
                openTraverse.Add(tempTraverse); 
            }
        }

        /// <summary>
        /// 将观测的数据插入到表格中
        /// </summary>
        private void InsertObDataIntoTable()
        {
            int dataCount = groundData.Count;
            for (int i = 0; i < dataCount; i++)
            {
                DataTableGridView.Rows.Add(1);
                DataTableGridView.Rows[i].Cells[0].Value = i + 1;
                DataTableGridView.Rows[i].Cells[1].Value = AngleTransUtil.DMSToStr(groundData[i].CoorAzi);
                DataTableGridView.Rows[i].Cells[2].Value = AngleTransUtil.DMSToStr(groundData[i].GyroAzi);
            }
            int traverseEdgeCount = traverseEdge.Count;
            for (int i = 0; i < traverseEdgeCount; i++)
            {
                dataGridView1.Rows.Add(1);
                dataGridView1.Rows[i].Cells[0].Value = i + 1;
                dataGridView1.Rows[i].Cells[1].Value = traverseEdge[i].StartPoint.PointName;
                dataGridView1.Rows[i].Cells[2].Value = traverseEdge[i].EndPoint.PointName;
                dataGridView1.Rows[i].Cells[3].Value = traverseEdge[i].IsGyroDirEdge;
                dataGridView1.Rows[i].Cells[4].Value = traverseEdge[i].SurveyNum;
            }
        }

        /// <summary>
        /// 陀螺经纬仪定向精度评定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CalDownholeCoorAziToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (isCalma)
            {
                return;
            }

            //计算仪器常数中误差
            mv = 0;  //仪器常数中误差清零
            double vv = 0;
            double deltaSum = 0;
            for (int i = 0; i < groundData.Count; i++)
            {
                groundData[i].Delta = groundData[i].CoorAzi - groundData[i].GyroAzi;
                DataTableGridView.Rows[i].Cells[3].Value = AngleTransUtil.DMSToStr(AngleTransUtil.DegreeToDMS(groundData[i].Delta));
                deltaSum = deltaSum + groundData[i].Delta;
            }
            double deltaAverage = deltaSum/ groundData.Count;  //仪器常数平均值
            for (int i = 0; i < groundData.Count; i++)
            {
                DataTableGridView.Rows[i].Cells[4].Value = Math.Round(-(groundData[i].CoorAzi - groundData[i].GyroAzi - deltaAverage) * 3600, 2) + "″";
                vv = vv + (groundData[i].CoorAzi - groundData[i].GyroAzi - deltaAverage) * (groundData[i].CoorAzi - groundData[i].GyroAzi - deltaAverage)*3600*3600; //单位：秒
            }
            mv = Math.Sqrt(vv/(groundData.Count-1));  //仪器常数一次测定中误差

            ma = Math.Sqrt(mv * mv / groundData.Count + mv * mv / 2); //井下一次测定中误差

            //计算各个陀螺边的误差
            for (int i = 0; i < gyroEdge.Count; i++)
            {
                    gyroEdge[i].Ma = Math.Sqrt(mv * mv / groundData.Count + mv * mv / gyroEdge[i].SurveyNum);
            }

            MessageBox.Show("陀螺经纬仪定向精度评定完成！", "提示",
                   MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            isCalma = true;
            OutMaReport();
        }

        /// <summary>
        /// 生成结果报告
        /// </summary>
        private void OutMaReport()
        {
            ReportTextBox.AppendText("-------------------- 陀螺经纬仪定向精度评定--------------------" + Environment.NewLine);
            ReportTextBox.AppendText("序号|     地理方位角    |    陀螺方位角    |    仪器常数  |  差值" + Environment.NewLine);
            for (int i = 0; i < groundData.Count; i++)
            {
                ReportTextBox.AppendText(String.Format("{0,-3} | {1,-14} |{2,-14} | {3,-9} |{4,-8}", DataTableGridView.Rows[i].Cells[0].Value.ToString(), 
                    DataTableGridView.Rows[i].Cells[1].Value.ToString(),
                    DataTableGridView.Rows[i].Cells[2].Value.ToString(),
                    DataTableGridView.Rows[i].Cells[3].Value.ToString(),
                    DataTableGridView.Rows[i].Cells[4].Value.ToString()));
                ReportTextBox.AppendText(Environment.NewLine);
            }
            ReportTextBox.AppendText(Environment.NewLine + "仪器常数一次测定中误差为" + String.Format("{0:N4}", mv) + Environment.NewLine);
            ReportTextBox.AppendText(Environment.NewLine+"各个陀螺边的误差为:" + Environment.NewLine);
            for (int i = 0; i < gyroEdge.Count; i++)
            {
                int index = i + 1;
                ReportTextBox.AppendText( "陀螺边" + index + ": " + String.Format("{0:N2}", gyroEdge[i].Ma) + "″" + Environment.NewLine);
            }
        }

        /// <summary>
        /// 贯通误差预计
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EstiThroughErrToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!isCalma)
            {
                MessageBox.Show("未计算井下方位角", "错误",
                 MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                return;
            }
            if (isEstiThroErr)
            {
                return;
            }
            ReportTextBox.AppendText(Environment.NewLine+"--------------------贯通误差预计--------------------" + Environment.NewLine);
            ReportTextBox.AppendText(Environment.NewLine + "1、各段重心坐标为：" + Environment.NewLine);
            // 求符合导线的重心
            for (int i = 0; i < connectTraverse.Count; i++)
            {
                connectTraverse[i].Xcoor = traverseEdge[connectTraverse[i].StartPoint.Pointindex].StartPoint.XCoor;
                connectTraverse[i].Ycoor = traverseEdge[connectTraverse[i].StartPoint.Pointindex].StartPoint.YCoor;
                for (int j = 0; j < connectTraverse[i].SurveyNum; j++)
                {
                    connectTraverse[i].Xcoor += traverseEdge[connectTraverse[i].StartPoint.Pointindex + j].EndPoint.XCoor;
                    connectTraverse[i].Ycoor += traverseEdge[connectTraverse[i].StartPoint.Pointindex + j].EndPoint.YCoor;
                }
                connectTraverse[i].Xcoor = connectTraverse[i].Xcoor / (connectTraverse[i].SurveyNum + 1);
                connectTraverse[i].Ycoor = connectTraverse[i].Ycoor / (connectTraverse[i].SurveyNum + 1);
                ReportTextBox.AppendText("方向附合导线"+ (i+1) + "的重心在y'轴坐标:  " + String.Format("{0:N4}", connectTraverse[i].Ycoor) +"m" + Environment.NewLine);
            }
            /**
             * 计算导线量边误差
             */
            ReportTextBox.AppendText(Environment.NewLine + "2、由测角量边引起的终点K的贯通误差为：" + Environment.NewLine);
            double mxKl = 0.0;
            for(int i=0;i < traverseEdge.Count; i++)
            {
                mxKl += Math.Pow(ml * Math.Cos(traverseEdge[i].GetAzi()), 2) / 2;
            }
            ReportTextBox.AppendText("MxKl=" + Math.Round(Math.Sqrt(mxKl), 3) + "m" + Environment.NewLine);

            /**
             * 计算导线测角误差
             */
            double mxKb = 0.0; //初始化导线测角误差
            ReportTextBox.AppendText(Environment.NewLine + "3、由导线测角误差引起的K点贯通误差为" + Environment.NewLine);
            // 符合导线各点到重心在y轴的投影
            ReportTextBox.AppendText("方向符合导线边：" + Environment.NewLine);
            ReportTextBox.AppendText("点号|       η       |      η2" + Environment.NewLine);
            for (int i = 0; i < connectTraverse.Count; i++)
            {
                int Pointindex = connectTraverse[i].StartPoint.Pointindex;
                double Ry = traverseEdge[Pointindex].StartPoint.YCoor - connectTraverse[i].Ycoor;
                double PowerRy = Math.Pow(Ry, 2);
                mxKb +=  PowerRy;
                ReportTextBox.AppendText(String.Format("{0,3} | {1,14} |{2,14}  ", traverseEdge[Pointindex].StartPoint.PointName, Math.Round(Ry, 3), Math.Round(PowerRy, 3)));
                ReportTextBox.AppendText(Environment.NewLine);
                for (int j = 0; j < connectTraverse[i].SurveyNum; j++)
                {
                    Pointindex = connectTraverse[i].StartPoint.Pointindex+j;
                    Ry = traverseEdge[Pointindex].EndPoint.YCoor - connectTraverse[i].Ycoor;
                    PowerRy = Math.Pow(Ry, 2);
                    mxKb += PowerRy;
                    ReportTextBox.AppendText(String.Format("{0,3} | {1,14} |{2,14}  ", traverseEdge[Pointindex].EndPoint.PointName, Math.Round(Ry, 3), Math.Round(PowerRy, 3)));
                    ReportTextBox.AppendText(Environment.NewLine);
                }
                ReportTextBox.AppendText("---------------------------------"+Environment.NewLine);
            }
            // 支导线各点到k点的连线在y轴上的投影
            ReportTextBox.AppendText(Environment.NewLine);
            ReportTextBox.AppendText("支导线边：" + Environment.NewLine);
            ReportTextBox.AppendText("点号|       η       |      η2" + Environment.NewLine);
            for (int i = 0; i < openTraverse.Count; i++)
            {
                int Pointindex = openTraverse[i].StartPoint.Pointindex;
                double Ry = traverseEdge[Pointindex].StartPoint.YCoor - traverseEdge[0].StartPoint.YCoor;
                double PowerRy = Math.Pow(Ry, 2);
                mxKb += PowerRy;
                ReportTextBox.AppendText(String.Format("{0,3} | {1,14} |{2,14}  ", traverseEdge[Pointindex].StartPoint.PointName, Math.Round(Ry, 3), Math.Round(PowerRy, 3)));
                ReportTextBox.AppendText(Environment.NewLine);
                for (int j = 0; j < openTraverse[i].SurveyNum; j++)
                {
                    Pointindex = openTraverse[i].StartPoint.Pointindex+j;
                    Ry = traverseEdge[Pointindex].EndPoint.YCoor - traverseEdge[0].StartPoint.YCoor;
                    PowerRy = Math.Pow(Ry, 2);
                    mxKb += PowerRy;
                    ReportTextBox.AppendText(String.Format("{0,3} | {1,14} |{2,14}  ", traverseEdge[Pointindex].EndPoint.PointName, Math.Round(Ry, 3), Math.Round(PowerRy, 3)));
                    ReportTextBox.AppendText(Environment.NewLine);
                }
            }
            //由导线测角误差引起的K点贯通误差为
            mxKb *= mb * mb / 2;
            ReportTextBox.AppendText(Environment.NewLine);
            ReportTextBox.AppendText("MxKb=" + Math.Round(Math.Sqrt(mxKb), 3) + "m" + Environment.NewLine);


            /**
             * 计算由陀螺定向边的定向误差
             */
            ReportTextBox.AppendText(Environment.NewLine + "4、由陀螺定向边的定向误差引起的K点贯通误差为" + Environment.NewLine);
            double mxKo = 0.0; //初始化陀螺定向边引起贯通误差
            mxKo += Math.Pow(traverseEdge[0].StartPoint.YCoor - connectTraverse[0].Ycoor, 2) * gyroEdge[0].Ma * gyroEdge[0].Ma / (206264.808 * 206264.808);
            for (int i = 0; i < connectTraverse.Count-1; i++)
            {
                mxKo += Math.Pow(connectTraverse[i].Ycoor - connectTraverse[i + 1].Ycoor, 2) * gyroEdge[i + 1].Ma * gyroEdge[i + 1].Ma / (206264.808 * 206264.808);
            }
            mxKo += Math.Pow(traverseEdge[0].StartPoint.YCoor - connectTraverse[connectTraverse.Count - 1].Ycoor, 2) * gyroEdge[gyroEdge.Count - 1].Ma * gyroEdge[gyroEdge.Count - 1].Ma / (206264.808 * 206264.808);
            ReportTextBox.AppendText("MxKo=" + Math.Round(Math.Sqrt(mxKo), 3) + Environment.NewLine);


            ReportTextBox.AppendText(Environment.NewLine + "5、最终K点在水平重要方向x上的贯通估计误差估算公式为" + Environment.NewLine);
            Mx = Math.Sqrt(mxKl + mxKb + mxKo); // 最终误差
            ReportTextBox.AppendText("MxK=" + Math.Round(Mx, 3) + "m" + Environment.NewLine);

            ReportTextBox.AppendText(Environment.NewLine + "6、贯通相遇点K在水平重要方向x上的预计误差为" + Environment.NewLine);
            Mx = 2 * Mx;
            ReportTextBox.AppendText("MxK预=" + Math.Round(Mx, 3) + "m" + Environment.NewLine);
            isEstiThroErr = true;
            MessageBox.Show("导线中加测陀螺定向边后导线误差终点误差估算完成，结果报告已生成", "提示",
                   MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
        }

        /// <summary>
        /// 一键处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OneKeyProToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //DataPreProToolStripMenuItem_Click(sender, e);
            //CalGycoAziToolStripMenuItem_Click(sender, e);
            CalDownholeCoorAziToolStripMenuItem_Click(sender, e);
            EstiThroughErrToolStripMenuItem_Click(sender, e);
        }

        /// <summary>
        /// 保存结果报告
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MySaveFileDialog.InitialDirectory = Application.StartupPath;
            MySaveFileDialog.Filter = "txt file(*.txt)|*.txt";  // 文件类型过滤器设置为txt文件
            if (MySaveFileDialog.ShowDialog() == DialogResult.OK)
            {
                File.WriteAllText(MySaveFileDialog.FileName, ReportTextBox.Text);
            }
        }

        /// <summary>
        /// 关于
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("陀螺经纬仪定向程序", "关于",
                MessageBoxButtons.OK, MessageBoxIcon.None, MessageBoxDefaultButton.Button1);
        }

        /// <summary>
        /// 退出程序
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        /// <summary>
        /// 查看数据表格
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DataTableToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MyTabControl.SelectedIndex = 0;
        }

        /// <summary>
        /// 查看结果报告
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MyTabControl.SelectedIndex = 2;
        }

        private void SaveTableToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (DataTableGridView.Rows.Count == 0)
            {
                MessageBox.Show("没有可以导出的数据", "无数据", MessageBoxButtons.OK, MessageBoxIcon.None, MessageBoxDefaultButton.Button1);
                return;
            }
            MySaveFileDialog.InitialDirectory = Application.StartupPath;
            MySaveFileDialog.Filter = "txt file(*.txt)|*.txt";
            MySaveFileDialog.RestoreDirectory = true;
            if (MySaveFileDialog.ShowDialog() == DialogResult.OK)
            {
                String datatable = FileHelperUtil.DataToTxt(DataTableGridView);
                File.WriteAllText(MySaveFileDialog.FileName, datatable);
                MessageBox.Show("保存成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            }
        }

        private bool isDrawDone = false;    // 判断是否绘制图形的Flag
        private Bitmap pointBitmap;         //点位图

        private void PointPictureToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!isEstiThroErr)
            {
                MessageBox.Show("未计算贯通误差", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                return;
            }
            MyTabControl.SelectedIndex = 3;
            DrawPointPicture();
            isDrawDone = true;
        }

        /// <summary>
        /// 绘制点位图
        /// </summary>
        private void DrawPointPicture()
        {
            Graphics gra = Graphics.FromImage(pointBitmap);   // 在Bitmap上绘制点位图
            //gra.ScaleTransform(1, -1);
            double deltaX = CalculateUtil.GetCoorDelta(traverseEdge, true);
            double deltaY = CalculateUtil.GetCoorDelta(traverseEdge, false);
            double xScale = pointBitmap.Height / deltaX * 0.76;    // X轴方向上的缩放比例
            double yScale = pointBitmap.Width / deltaY * 0.76;   // Y轴方向上的缩放比例 
            if (xScale < yScale)
            { yScale = xScale;  }
            else
            { xScale = yScale; }
            // 所有的导线点中最大和最小的X、Y坐标值(包括控制点）
            double dMinX = CalculateUtil.GetMinCoor(traverseEdge, true);
            double dMinY = CalculateUtil.GetMinCoor(traverseEdge, false);
            double dMaxX = CalculateUtil.GetMaxCoor(traverseEdge, true);
            double dMaxY = CalculateUtil.GetMaxCoor(traverseEdge, false);
            double dmidX = (dMinX + dMaxX) / 2;
            double dmidY = (dMinY + dMaxY) / 2;
            // 通过下述两个平移量将点居中显示
            int pointVerMove = (int)6 * pointBitmap.Width / 10;
            int pointHorMove = (int)6 * pointBitmap.Height / 10;
            // 需要将点名在垂直方向上平移一个量，否则会遮挡点位
            int bPointNameVerMove = -13;
            Brush redPointColor = Brushes.Red;  // 创建画笔，设置线的颜色、粗细
            Brush greenPointColor = Brushes.Green;  // 创建画笔，设置线的颜色、粗细
            Brush bluePointColor = Brushes.Blue;   // 创建画笔，设置线的颜色、粗细
            Pen myRedPen = new Pen(Color.Red, 2);
            Pen myGreenPen = new Pen(Color.Green, 1);
            Pen myBlackPen = new Pen(Color.Black, 1);
            Font myFont = new Font("@Times New Roman", 12);   // 采用宋体，9号字体
            FontFamily fontFamily = new FontFamily("Arial");
            Font font = new Font(fontFamily, 16, FontStyle.Regular, GraphicsUnit.Pixel);
            // 绘制点
            for (int i = 0; i < traverseEdge.Count; i++)
            {
                // 绘制控制点（用圆表示)
                if (traverseEdge[i].IsGyroDirEdge)
                {               
                    float centerX = Convert.ToSingle(-(traverseEdge[i].StartPoint.XCoor - dmidX) * xScale) + pointHorMove;
                    float centerY  = Convert.ToSingle((traverseEdge[i].StartPoint.YCoor - dmidY) * yScale)+ pointVerMove;
                    gra.FillEllipse(redPointColor, centerY-4, centerX-4, 8, 8);
                    gra.DrawString(traverseEdge[i].StartPoint.PointName, myFont, Brushes.Black, new Point(Convert.ToInt32(centerY - 16), Convert.ToInt32(centerX - 20)));
                    centerX = Convert.ToSingle(-(traverseEdge[i].EndPoint.XCoor - dmidX) * xScale) + pointHorMove;
                    centerY = Convert.ToSingle( (traverseEdge[i].EndPoint.YCoor - dmidY) * yScale)+pointVerMove ;
                    gra.FillEllipse(redPointColor, centerY-4, centerX-4, 8, 8);
                }
                else
                {
                    // 绘制导线点
                    gra.FillEllipse(bluePointColor, Convert.ToInt32( (traverseEdge[i].EndPoint.YCoor - dmidY) * yScale+pointVerMove - 4),
                        Convert.ToInt32(-(traverseEdge[i].EndPoint.XCoor - dmidX) * xScale) + pointHorMove - 4, 8, 8);
                    gra.DrawString(traverseEdge[i].StartPoint.PointName, myFont, Brushes.Black, Convert.ToSingle((traverseEdge[i].StartPoint.YCoor - dmidY) * yScale+pointVerMove - 16),
                        Convert.ToSingle(-(traverseEdge[i].StartPoint.XCoor - dmidX) * xScale) + pointHorMove - 20);
                }      
            }
            gra.FillEllipse(greenPointColor, Convert.ToInt32(pointVerMove - 4 + (traverseEdge[traverseEdge.Count-1].EndPoint.YCoor - dmidY) * yScale),
                        Convert.ToInt32(-(traverseEdge[traverseEdge.Count-1].EndPoint.XCoor - dmidX) * xScale) + pointHorMove - 4, 8, 8);

            // 连接导线点
            double X = traverseEdge[0].StartPoint.XCoor;
            double Y = traverseEdge[0].StartPoint.YCoor;
            for (int i = 0; i < traverseEdge.Count; i++)
            {
                if (traverseEdge[i].IsGyroDirEdge)
                {
                    gra.DrawLine(myRedPen, new Point(Convert.ToInt32(pointVerMove + (traverseEdge[i].StartPoint.YCoor - dmidY) * yScale), Convert.ToInt32(-(traverseEdge[i].StartPoint.XCoor - dmidX) * xScale) + pointHorMove),
                                                 new Point(Convert.ToInt32(pointVerMove + (traverseEdge[i].EndPoint.YCoor - dmidY) * yScale), Convert.ToInt32(-(traverseEdge[i].EndPoint.XCoor - dmidX) * xScale) + pointHorMove));
                }
                else
                {
                    gra.DrawLine(myBlackPen, new Point(Convert.ToInt32(pointVerMove + (traverseEdge[i].StartPoint.YCoor - dmidY) * yScale), Convert.ToInt32(-(traverseEdge[i].StartPoint.XCoor - dmidX) * xScale) + pointHorMove),
                             new Point(Convert.ToInt32(pointVerMove + (traverseEdge[i].EndPoint.YCoor - dmidY) * yScale), Convert.ToInt32(-(traverseEdge[i].EndPoint.XCoor - dmidX) * xScale) + pointHorMove));
                }
            }

            // 绘制方向导线点到重心的连线
            for (int i = 0; i < connectTraverse.Count; i++)
            {
                gra.DrawEllipse(myRedPen, Convert.ToInt32(pointVerMove - 4 + (connectTraverse[i].Ycoor - dmidY) * yScale),Convert.ToInt32(-(connectTraverse[i].Xcoor - dmidX) * xScale) + pointHorMove - 4,8, 8);
                Pen pen = new Pen(Color.Black, 1);
                pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Custom;
                pen.DashPattern = new float[] { 5, 5 };
                int pointindex = connectTraverse[i].StartPoint.Pointindex;
                gra.DrawLine(pen, new Point(Convert.ToInt32(pointVerMove + (traverseEdge[pointindex].StartPoint.YCoor - dmidY) * yScale), Convert.ToInt32(-(traverseEdge[pointindex].StartPoint.XCoor - dmidX) * xScale) + pointHorMove),
                                     new Point(Convert.ToInt32(pointVerMove + (connectTraverse[i].Ycoor - dmidY) * yScale), Convert.ToInt32(-(connectTraverse[i].Xcoor - dmidX) * xScale) + pointHorMove));
                for (int j = 0; j < connectTraverse[i].SurveyNum; j++)
                {
                    pointindex = connectTraverse[i].StartPoint.Pointindex + j;
                    gra.DrawLine(pen, new Point(Convert.ToInt32(pointVerMove + (traverseEdge[pointindex].EndPoint.YCoor - dmidY) * yScale), Convert.ToInt32(-(traverseEdge[pointindex].EndPoint.XCoor - dmidX) * xScale) + pointHorMove),
                                     new Point(Convert.ToInt32(pointVerMove + (connectTraverse[i].Ycoor - dmidY) * yScale), Convert.ToInt32(-(connectTraverse[i].Xcoor - dmidX) * xScale) + pointHorMove));
                }
            }


            pointPictureBox.Image = pointBitmap; // 将点位图在PictureBox中显示
        }

        /// <summary>
        /// 放大图形
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EnlargeToolStripButton_Click(object sender, EventArgs e)
        {
            if (!isDrawDone)
            {
                return;
            }
            int newWidth = pointBitmap.Width + 200;
            int newHeight = pointBitmap.Height + 100;
            pointBitmap = new Bitmap(newWidth, newHeight);

            //--------------------------------------------------
            //画图
            DrawPointPicture();
            //--------------------------------------------------
        }

        /// <summary>
        /// 缩小图形
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EnlittleToolStripButton_Click(object sender, EventArgs e)
        {
            if (!isDrawDone)
            {
                return;
            }
            int newWidth = pointBitmap.Width - 200;
            int newHeight = pointBitmap.Height - 100;
            pointBitmap = new Bitmap(newWidth, newHeight);
            if (newWidth < 0 || newHeight < 0)
            {
                return;
            }
            //--------------------------------------------------
            // 画图
            DrawPointPicture();
            //--------------------------------------------------
        }

        /**
         * 菜单栏对应的工具栏按钮
         */
        private void AboutToolStripButton_Click(object sender, EventArgs e)
        {
            AboutToolStripMenuItem_Click(sender, e);
        }

        private void OpenDataFileToolStripButton_Click(object sender, EventArgs e)
        {
            OpenDataFileToolStripMenuItem_Click(sender, e);
        }

        private void SaveReportToolStripButton_Click(object sender, EventArgs e)
        {
            SaveReportToolStripMenuItem_Click(sender, e);
        }

        private void OneKeyProToolStripButton_Click(object sender, EventArgs e)
        {
            OneKeyProToolStripMenuItem_Click(sender, e);
        }

        private void CheckDataTableToolStripButton_Click(object sender, EventArgs e)
        {
            DataTableToolStripMenuItem_Click(sender, e);
        }

        private void CheckReportToolStripButton_Click(object sender, EventArgs e)
        {
            ReportToolStripMenuItem_Click(sender, e);
        }

        private void EnlargeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EnlargeToolStripButton_Click(sender, e);
        }

        private void EnlittleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EnlittleToolStripButton_Click(sender, e);
        }

        private void PointPictureToolStripButton_Click(object sender, EventArgs e)
        {
            PointPictureToolStripMenuItem_Click(sender, e);
        }

        private void MyToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void savePictureToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
