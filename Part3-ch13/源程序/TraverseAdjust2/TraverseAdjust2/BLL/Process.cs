using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

using TraverseAdjust.Entities;
using TraverseAdjust.DAL;
using TraverseAdjust.Common;

namespace TraverseAdjust.BLL
{
    /*
     * 功能概要：平差数据计算，向UI提供访问借口，通过访问DAL实现IO功能
     * 编号：TA_BLL_001
     * 作者：廖振修
     *  创建日期:2016-06-09
     */
    public class Process
    {
        private DataInput mDataInput; // 数据输入对象
        private DataOutPut mDataOutPut;

        private KnowedObsData mKnowedObsData; // 原始观测值对象
        /// <summary>
        ///     ''' 原始观测值对象:只读
        ///     ''' </summary>
        ///     ''' <value>原始观测值对象</value>
        ///     ''' <returns>原始观测值对象</returns>
        ///     ''' <remarks>原始观测值对象:只读</remarks>
        public KnowedObsData KnowedObsData
        {
            get
            {
                return mKnowedObsData;
            }
        }

        private string mDspStr; // 中间计算过程信息
        private TraverseLine mTraverseLine; // 导线对象

        // 构造函数
        public Process()
        {
            mDataInput = new DataInput();
        }

        // 初始化观测值表格
        public void InitlGridObsData(frmProcess viewForm)
        {
            int n;
            n = mKnowedObsData.Pnames.Count; // 总点数（已知+未知)
            viewForm.gridObsData.Rows.Clear(); // 清空原有行
            viewForm.gridObsData.Rows.Add(n); // 增加数据行
            // 设置已知点单元格格式
            // 前面两已知点
            viewForm.gridObsData.Rows[0].DefaultCellStyle.BackColor =Color.LightGray;
            viewForm.gridObsData.Rows[0].ReadOnly = true;
            viewForm.gridObsData.Rows[1].DefaultCellStyle.BackColor = Color.LightGray;
            viewForm.gridObsData.Rows[1].ReadOnly = true;
            viewForm.gridObsData.Rows[1].Cells[2].Style.BackColor = Color.White;
            viewForm.gridObsData.Rows[1].Cells[2].ReadOnly = false;
            // 后面两已知点
            viewForm.gridObsData.Rows[n - 1].DefaultCellStyle.BackColor = Color.LightGray;
            viewForm.gridObsData.Rows[n - 1].ReadOnly = true;
            viewForm.gridObsData.Rows[n - 2].Cells[1].Style.BackColor = Color.LightGray;
            viewForm.gridObsData.Rows[n - 2].Cells[1].ReadOnly = true;

            // 赋值序号及点号
            for (int i = 0; i <= n - 1; i++)
            {
                viewForm.gridObsData.Rows[i].Cells[0].Value = i + 1;
                viewForm.gridObsData.Rows[i].Cells[1].Value = mKnowedObsData.Pnames[i];
            }
            // 赋值已知点距离
            double dist;
            dist = BaseFunction.DistAB(mKnowedObsData.X0[0], mKnowedObsData.Y0[0], mKnowedObsData.X0[1], mKnowedObsData.Y0[1]);
            viewForm.gridObsData.Rows[1].Cells[3].Value = dist.ToString("0.000");
            dist = BaseFunction.DistAB(mKnowedObsData.X0[2], mKnowedObsData.Y0[2], mKnowedObsData.X0[3], mKnowedObsData.Y0[3]);
            viewForm.gridObsData.Rows[n - 1].Cells[3].Value = dist.ToString("0.000");

            // 赋值观测角度默认值
            for (int i = 1; i <= n - 2; i++)
                viewForm.gridObsData.Rows[i].Cells[2].Value = "0.0000";
            // 赋值观测距离默认值
            for (int i = 2; i <= n - 2; i++)
                viewForm.gridObsData.Rows[i].Cells[3].Value = "0.000";
        }

        // 从窗体界面获取已知信息
        public void GetKnowedInfoFromWin(frmProcess viewForm)
        {
            mKnowedObsData = new KnowedObsData();
            if (viewForm.chkIsConnecting.Checked)
                mKnowedObsData.NetType = 1;
            else
                mKnowedObsData.NetType = 2;
            mKnowedObsData.X0 = new List<double>();
            mKnowedObsData.Y0 = new List<double>();
            mKnowedObsData.X0.Add(Convert.ToDouble(viewForm.txtX1.Text.Trim()));
            mKnowedObsData.Y0.Add(Convert.ToDouble(viewForm.txtY1.Text.Trim()));
            mKnowedObsData.X0.Add(Convert.ToDouble(viewForm.txtX2.Text.Trim()));
            mKnowedObsData.Y0.Add(Convert.ToDouble(viewForm.txtY2.Text.Trim()));

            if (mKnowedObsData.NetType == 1)
            {
                mKnowedObsData.X0.Add(Convert.ToDouble(viewForm.txtX3.Text.Trim()));
                mKnowedObsData.Y0.Add(Convert.ToDouble(viewForm.txtY3.Text.Trim()));
                mKnowedObsData.X0.Add(Convert.ToDouble(viewForm.txtX4.Text.Trim()));
                mKnowedObsData.Y0.Add(Convert.ToDouble(viewForm.txtY4.Text.Trim()));
            }
            else
            {
                mKnowedObsData.X0.Add(Convert.ToDouble(viewForm.txtX2.Text.Trim()));
                mKnowedObsData.Y0.Add(Convert.ToDouble(viewForm.txtY2.Text.Trim()));
                mKnowedObsData.X0.Add(Convert.ToDouble(viewForm.txtX1.Text.Trim()));
                mKnowedObsData.Y0.Add(Convert.ToDouble(viewForm.txtY1.Text.Trim()));
            }

            if (viewForm.chkLeftAngle.Checked)
                mKnowedObsData.AngleType = 1;
            else
                mKnowedObsData.AngleType = 2;
            int un = Convert.ToInt32(viewForm.txtUnknowPtNum.Text.Trim()); // 未知点数
            mKnowedObsData.Pnames = new List<string>();
            mKnowedObsData.Pnames.Add(viewForm.txtP1Name.Text.Trim());
            mKnowedObsData.Pnames.Add(viewForm.txtP2Name.Text.Trim());
            for (int i = 0; i <= un - 1; i++)
                mKnowedObsData.Pnames.Add("P" + i + 1);
            if (mKnowedObsData.NetType == 1)
            {
                mKnowedObsData.Pnames.Add(viewForm.txtP3Name.Text.Trim());
                mKnowedObsData.Pnames.Add(viewForm.txtP4Name.Text.Trim());
            }
            else
            {
                mKnowedObsData.Pnames.Add(viewForm.txtP2Name.Text.Trim());
                mKnowedObsData.Pnames.Add(viewForm.txtP1Name.Text.Trim());
            }
        }

        // 从文件中获取原始数据对象
        public void ShowInputDataFromFile(frmProcess viewForm, string fileName, ref string validateInfo)
        {
            if (fileName.ToUpper().Contains(".TXT"))
                mKnowedObsData = DataInput.InputDataFromTXTFile(fileName, ref validateInfo);
            if (fileName.ToUpper().Contains(".XLS"))
                mKnowedObsData = DataInput.InputDataFromXLSFile(fileName, ref validateInfo);
            if (mKnowedObsData == null)
                return; // 读数据出错
            ShowInputData(viewForm); // 显示数据
        }

        // 平差计算，显示计算过程信息
        public void Calc(frmProcess viewForm)
        {
            KnowedObsData theKnowedObsData = GetKnowedObsDataFromWin(viewForm); // 从数据输入控件中获得原始观测数据
            SetTraverseLineByKnowedObsData(theKnowedObsData); // 初始化导线

            bool isOk = Adjust(); // 导线平差
            viewForm.txtResult.Text = mDspStr; // 显示平差过程成果
            viewForm.gbResult.Enabled = true;
            viewForm.gbOutput.Enabled = isOk;
            if (isOk) mDataOutPut = new DataOutPut(mTraverseLine);// 实例化输出对象
        }

        // 导出平差成果
        public void Output(string outputType)
        {
            switch (outputType)
            {
                case "Map":
                        mDataOutPut.OutPut2Map();
                        break;
                case "DXF":
                        mDataOutPut.Outpt2DXF();
                        break;
                case "XLS":
                        mDataOutPut.Outpt2XLS();
                        break;
                case "TXT":
                        mDataOutPut.Outpt2TXT();
                        break;
            }//endswitch
        }

        // 近似平差计算
        private bool Adjust()
        {
            Intil(); // 初始化导线
            CalcDirect0(); // 计算近似方位角
            if (!AdjDirect()) return false;
            CalcCoord0(); // 计算近似坐标
            if (!AdjCoord()) return false;
            return true;
        }

        // 从窗体界面获取已知信息和观测信息
        private KnowedObsData GetKnowedObsDataFromWin(frmProcess viewForm)
        {
            GetKnowedInfoFromWin(viewForm);
            GetObstDataFromGrid(viewForm);
            return mKnowedObsData;
        }

        // 由原始观测值对象，获取未平差的导线对象
        private void SetTraverseLineByKnowedObsData(KnowedObsData theKnowedObsData)
        {
            mTraverseLine = new TraverseLine();
            mTraverseLine.Lines = new List<MyLine>();

            mTraverseLine.KnowedObsData = theKnowedObsData; // 原始观测数据
            mTraverseLine.NetType = theKnowedObsData.NetType; // 导线类型
            mTraverseLine.AngleType = theKnowedObsData.AngleType;  // 测角类型
        }

        // 初始化导线
        private void Intil()
        {
            MyPoint startPt, endPt;
            MyLine line;
            int n = mTraverseLine.KnowedObsData.Pnames.Count; // 总点数（已知点4+未知点数)
            // 赋值第一条起始边
            startPt = new MyPoint();
            startPt.Name = mTraverseLine.KnowedObsData.Pnames[0];
            startPt.X = mTraverseLine.KnowedObsData.X0[0];
            startPt.Y = mTraverseLine.KnowedObsData.Y0[0];
            startPt.Type = 1;
            endPt = new MyPoint();
            endPt.Name = mTraverseLine.KnowedObsData.Pnames[1];
            endPt.X = mTraverseLine.KnowedObsData.X0[1];
            endPt.Y = mTraverseLine.KnowedObsData.Y0[1];
            endPt.Type = 1;
            line = new MyLine();
            line.StartPt = startPt;
            line.EndPt = endPt;
            line.Name = startPt.Name + endPt.Name;
            line.Distance = BaseFunction.DistAB(line);
            line.Direction = BaseFunction.DirectAB(line);
            line.Type = 1;
            mTraverseLine.Lines.Add(line);
            // 赋值中间未知边
            for (int i = 2; i <= n - 2; i++)
            {
                startPt = endPt;
                endPt = new MyPoint();
                endPt.Name = mTraverseLine.KnowedObsData.Pnames[i];
                endPt.Type = 2;
                if (i == n - 2)
                {
                    endPt.Type = 1;
                    endPt.X = mTraverseLine.KnowedObsData.X0[2];
                    endPt.Y = mTraverseLine.KnowedObsData.Y0[2];
                }
                line = new MyLine();
                line.StartPt = startPt;
                line.EndPt = endPt;
                line.Name = startPt.Name + endPt.Name;
                line.Distance = mTraverseLine.KnowedObsData.SS[i - 2]; // 赋值实测距离
                line.Type = 2;
                mTraverseLine.Lines.Add(line);
            }
            // 赋值最后一条已知边
            startPt = endPt;
            endPt = new MyPoint();
            endPt.Name = mTraverseLine.KnowedObsData.Pnames[n - 1];
            endPt.Type = 1;
            endPt.X = mTraverseLine.KnowedObsData.X0[3];
            endPt.Y = mTraverseLine.KnowedObsData.Y0[3];
            line = new MyLine();
            line.StartPt = startPt;
            line.EndPt = endPt;
            line.Name = startPt.Name + endPt.Name;
            line.Distance = BaseFunction.DistAB(line);
            line.Direction = BaseFunction.DirectAB(line);
            line.Type = 1;
            mTraverseLine.Lines.Add(line);
        }

        // 计算近似方位角
        private void CalcDirect0()
        {
            int n = mTraverseLine.KnowedObsData.bb.Count; // 观测夹角个数
            double alpha; // 近似方位角
            double beta; // 观测夹角
            if (mTraverseLine.NetType == 1)
            {
                for (var i = 1; i <= n; i++)
                {
                    beta = mTraverseLine.KnowedObsData.bb[i - 1];
                    alpha = BaseFunction.DirectAB(mTraverseLine.Lines[i - 1], beta, mTraverseLine.AngleType);
                    mTraverseLine.Lines[i].Direction = alpha;
                }
            }
            else
            {
                if (mTraverseLine.AngleType == 1)
                    mTraverseLine.KnowedObsData.bb[0] = 2 * Math.PI - mTraverseLine.KnowedObsData.bb[0];
                mTraverseLine.KnowedObsData.bb[n - 1] = mTraverseLine.KnowedObsData.bb[n - 1] + (2 * Math.PI - mTraverseLine.KnowedObsData.bb[0]); // 最后一个夹角（转为附和导线)
                if (mTraverseLine.KnowedObsData.bb[n - 1] >= 2 * Math.PI)
                    mTraverseLine.KnowedObsData.bb[n - 1] = mTraverseLine.KnowedObsData.bb[n - 1] - 2 * Math.PI;
                mTraverseLine.AngleType = 2; // 转换为附和导线后，夹角固定为右角
                for (var i = 1; i <= n; i++)
                {
                    beta = mTraverseLine.KnowedObsData.bb[i - 1];
                    alpha = BaseFunction.DirectAB(mTraverseLine.Lines[i - 1], beta, mTraverseLine.AngleType);
                    mTraverseLine.Lines[i].Direction = alpha;
                }
            }

            mDspStr = ">>>  1.近似方位角计算  <<<\r\n";
            for (var i = 1; i <= n; i++)
                mDspStr += mTraverseLine.Lines[i].Name + "  边的近似方位角=" + BaseFunction.Hu2DMS(mTraverseLine.Lines[i].Direction).ToString("0.0000") +"\r\n";
        }

        // 改正方位角
        private bool AdjDirect()
        {
            
            double aaE; // 终止方位角
            double fbeta; // 方位角闭合差
            int n = mTraverseLine.KnowedObsData.bb.Count; // 观测夹角个数
            double fbeta0 = 24 * Math.Sqrt(n) / 10000; // 方位角闭合差限差(三级导线),转为dd.mmss形式
            mDspStr += "\r\n";
            mDspStr += ">>>  2.方位角近似平差  <<<\r\n";

            aaE = BaseFunction.DirectAB(mTraverseLine.KnowedObsData.X0[2], mTraverseLine.KnowedObsData.Y0[2], mTraverseLine.KnowedObsData.X0[3], mTraverseLine.KnowedObsData.Y0[3]);
            // 显示终止方位角
            mDspStr += " 终止方位角: " + BaseFunction.Hu2DMS(aaE).ToString("0.0000") + "\r\n";
            // 计算角度闭合差
            fbeta = mTraverseLine.Lines[n].Direction - aaE;
            mDspStr += "  角度闭合差=" + BaseFunction.Hu2DMS(fbeta).ToString("0.0000") + "  限差=" + fbeta0.ToString("0.0000") +  "\r\n";
            if (Math.Abs(BaseFunction.Hu2DMS(fbeta)) > fbeta0)
            {
                mDspStr += "方位角闭合差超限!\r\n";
                return false;
            }

            // 改正后的方位角
            double Vbeta0 = -fbeta / n;
            double alpha; // 改正后方位角
            for (var i = 1; i <= n; i++)
            {
                alpha = mTraverseLine.Lines[i].Direction;
                alpha += i * Vbeta0;
                if (alpha >= 2 * Math.PI)
                    alpha -= 2 * Math.PI;
                if (alpha < 0)
                    alpha += 2 * Math.PI;
                mTraverseLine.Lines[i].Direction = alpha;
                mDspStr += mTraverseLine.Lines[i].Name + "  边改正后的方位角=" + BaseFunction.Hu2DMS(alpha).ToString("0.0000") + "\r\n";
            }
            mDspStr += "\r\n";
            return true;
        }

        // 计算近似坐标（方位角改正后）
        private void CalcCoord0()
        {
            mDspStr += ">>>  3.近似坐标计算  <<<\r\n";
            // 计算改正角度后的近似坐标
            int n = mTraverseLine.Lines.Count; // 导线所有直线段数
            MyPoint startPt, endPt;
            for (var i = 1; i <= n - 2; i++)
            {
                startPt = mTraverseLine.Lines[i - 1].EndPt;
                endPt = BaseFunction.GetEndPt(startPt, mTraverseLine.Lines[i].Distance, mTraverseLine.Lines[i].Direction); // 坐标正算，获得直线终点近似坐标
                mTraverseLine.Lines[i].EndPt.X = endPt.X;
                mTraverseLine.Lines[i].EndPt.Y = endPt.Y;
                mDspStr += mTraverseLine.Lines[i].EndPt.Name + " 点的近似坐标为 X=" + endPt.X.ToString("0.000") + "  Y=" + endPt.Y.ToString("0.000") + "\r\n";
            }
            mDspStr += "\r\n";
        }

        // 坐标平差
        private bool AdjCoord()
        {
            mDspStr += ">>>  4.坐标近似平差计算  <<<\r\n";
            double fx, fy, fs;
            double totalS=0.0;
            int n = mTraverseLine.Lines.Count; // 导线所有直线段数
            for (int i = 1; i <= n - 2; i++) // 计算观测直线总长
                totalS += mTraverseLine.Lines[i].Distance;
            double fs0 = 5000.0;

            fx = mTraverseLine.Lines[n - 1].StartPt.X - mTraverseLine.KnowedObsData.X0[2];
            fy = mTraverseLine.Lines[n - 1].StartPt.Y - mTraverseLine.KnowedObsData.Y0[2];
            fs = Math.Sqrt(fx * fx + fy * fy);
            fs = Math.Floor(1 / (fs / totalS));
            mDspStr += " 坐标闭合差fx=" + (fx * 1000).ToString("0.0") + "mm  fy=" + (fy * 1000).ToString("0.0") + "mm   边长相对误差fs=1/" + fs.ToString("0") + "  限差=1/" + fs0.ToString("0") +"\r\n";
            if (fs < fs0)
            {
                mDspStr += "坐标闭合差超限!\r\n";
                return false;
            }
            // 计算坐标改正数
            mDspStr += "坐标改正数\r\n";
            double[] Vx = new double[n - 3 + 1], Vy = new double[n - 3 + 1];
            double Vx0 = -fx / totalS;
            double Vy0 = -fy / totalS;
            for (int i = 0; i <= n - 3; i++)
            {
                Vx[i] = Vx0 * mTraverseLine.Lines[i + 1].Distance;
                Vy[i] = Vy0 * mTraverseLine.Lines[i + 1].Distance;
                mDspStr += mTraverseLine.Lines[i + 1].EndPt.Name + " 点 的坐标改正数Vx=" + (Vx[i] * 1000).ToString("0.0") + "mm  Vy=" + (Vy[i] * 1000).ToString("0.0") + "mm \r\n";
            }

            // 计算坐标改正数
            mDspStr += "坐标平差值\r\n";
            double totalVx, totalVy;
             totalVx= totalVy=0.0;

            for (int i = 0; i <= n - 3; i++)
            {
                totalVx += Vx[i];
                totalVy += Vy[i];
                mTraverseLine.Lines[i + 1].EndPt.X += totalVx;
                mTraverseLine.Lines[i + 1].EndPt.Y += totalVy;
                mDspStr += mTraverseLine.Lines[i + 1].EndPt.Name + " 点 平差坐标X=" + mTraverseLine.Lines[i + 1].EndPt.X.ToString("0.000") + "  Y=" + mTraverseLine.Lines[i + 1].EndPt.Y.ToString("0.000") + "\r\n";
                // 重新计算导线距离、方位角等数据
                mTraverseLine.Lines[i + 1].Direction = BaseFunction.DirectAB(mTraverseLine.Lines[i + 1]);
                mTraverseLine.Lines[i + 1].Distance = BaseFunction.DistAB(mTraverseLine.Lines[i + 1]);
            }
            return true;
        }

        // 从观测值列表中获得观测数据
        private void GetObstDataFromGrid(frmProcess viewForm)
        {
            int n = mKnowedObsData.Pnames.Count;
            // 点名
            for (int i = 0; i <= n - 1; i++)
                mKnowedObsData.Pnames[i] = viewForm.gridObsData.Rows[i].Cells[1].Value.ToString();
            // 夹角观测值
            mKnowedObsData.bb = new List<double>();
            for (int i = 0; i <= n - 3; i++)
            {
                mKnowedObsData.bb.Add(Convert.ToDouble(viewForm.gridObsData.Rows[i + 1].Cells[2].Value));
                mKnowedObsData.bb[i] = BaseFunction.DMS2Hu(mKnowedObsData.bb[i]);
            }
            // 距离观测值
            mKnowedObsData.SS = new List<double>();
            for (int i = 0; i <= n - 4; i++)
                mKnowedObsData.SS.Add(Convert.ToDouble(viewForm.gridObsData.Rows[i + 2].Cells[3].Value));
        }

        // 显示导入的文件数据
        private void ShowInputData(frmProcess viewForm)
        {
            viewForm.txtUnknowPtNum.Text = (mKnowedObsData.Pnames.Count - 4).ToString();
            if (mKnowedObsData.NetType == 1)
                viewForm.chkIsConnecting.Checked = true;
            else
                viewForm.chkIsConnecting.Checked = false;
            if (mKnowedObsData.AngleType == 1)
                viewForm.chkLeftAngle.Checked = true;
            else
                viewForm.chkLeftAngle.Checked = false;
            viewForm.txtP1Name.Text = mKnowedObsData.Pnames[0];
            viewForm.txtX1.Text = mKnowedObsData.X0[0].ToString("0.000");
            viewForm.txtY1.Text = mKnowedObsData.Y0[0].ToString("0.000");
            viewForm.txtP2Name.Text = mKnowedObsData.Pnames[1];
            viewForm.txtX2.Text = mKnowedObsData.X0[1].ToString("0.000");
            viewForm.txtY2.Text = mKnowedObsData.Y0[1].ToString("0.000");
            if (mKnowedObsData.NetType == 1)
            {
                viewForm.txtP3Name.Text = mKnowedObsData.Pnames[mKnowedObsData.Pnames.Count - 2];
                viewForm.txtX3.Text = mKnowedObsData.X0[2].ToString("0.000");
                viewForm.txtY3.Text = mKnowedObsData.Y0[2].ToString("0.000");
                viewForm.txtP4Name.Text = mKnowedObsData.Pnames[mKnowedObsData.Pnames.Count - 1];
                viewForm.txtX4.Text = mKnowedObsData.X0[3].ToString("0.000");
                viewForm.txtY4.Text = mKnowedObsData.Y0[3].ToString("0.000");
            }
            // 初始化观测值表格
            InitlGridObsData(viewForm);
            // 赋值夹角观测值
            for (int i = 0; i <= mKnowedObsData.bb.Count - 1; i++)
                viewForm.gridObsData.Rows[i + 1].Cells[2].Value = mKnowedObsData.bb[i].ToString("0.0000");
            // 赋值边长观测值
            for (int i = 0; i <= mKnowedObsData.SS.Count - 1; i++)
                viewForm.gridObsData.Rows[i + 2].Cells[3].Value = mKnowedObsData.SS[i].ToString("0.000");
        }
    }
}//endspace
