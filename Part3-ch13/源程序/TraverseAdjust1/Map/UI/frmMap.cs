using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Map.Common;
using Map.Entities;

namespace Map.UI
{
    /*
     * 功能概要：图形显示界面
     * 编号：Map_UI_001
     * 作者：廖振修
     *  创建日期:2016-06-09
     */

    public partial class frmMap : Form
    {
        public List<GElement>  gList ; //图元列表字段

        private PointF  mPrevPt, mCurrentPt, mCenterPt ; //前一点、当前点、屏幕中心点(真实坐标)
        private bool  mIsInitl=false ; //是否初始化了
        private  bool mIsBackground_Black=true;//背景色是黑色
        private  bool mHasCoord=true; //是坐标
        private int VWidth, VHeight; //以设备坐标表示的视口长宽

        public frmMap()
        {
            InitializeComponent();
        }

        # region 窗体事件
        //  画布重画
        private void picMap_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = this.picMap.CreateGraphics();
            g.Clear(Color.Black);

            // 初始化后再次刷新
            if (mIsInitl) DrawAll_Map();
        }

        //鼠标一点，动态显示当前鼠标坐标
        private void picMap_MouseMove(object sender, MouseEventArgs e)
        {
            PointF aPos = BaseFunction.DPToRP(new Point(e.X, e.Y));
            // 状态栏显示当前坐标
            sbLabel1.Text = "X=" + aPos.X.ToString("0.000") + "  Y=" + aPos.Y.ToString("0.000");
        }


        //通过Timer控件完成加载窗体后及时显示图形功能
        private void timer1_Tick(object sender, EventArgs e)
        {
            Graphics g = this.picMap.CreateGraphics();

            ToMathCoord(g); // 画布坐标转为实际坐标
            InitlDispMap(); // 画图初始化处理
            DrawAll_Map(); // 画出所有图元
            timer1.Enabled = false;

            // 状态栏中显示当前坐标及图形比例尺
            PointF aPos = BaseFunction.DPToRP(picMap.Location); // 以画布左下角点为初始点
            sbLabel1.Text = "X=" + aPos.X.ToString("0.000") + "  Y=" + aPos.Y.ToString("0.000");
            ShowMapScale();

            mIsInitl = true;
        }

        //鼠标左键落下，记录当前鼠标位置，配合MouseUp事件完成图形平移操作
        private void picMap_MouseDown(object sender, MouseEventArgs e)
        {
            Point aPos = new Point(e.X, e.Y);
            mPrevPt = BaseFunction.DPToRP(aPos);
        }

        //鼠标左键抬起，完成图片平移操作
        private void picMap_MouseUp(object sender, MouseEventArgs e)
        {
            Point aPos = new Point(e.X, e.Y);
            mCurrentPt = BaseFunction.DPToRP(aPos);

            Pan(mPrevPt, mCurrentPt);
        }

        //放大图形
        private void tbZoomIn_Click(object sender, EventArgs e)
        {
            ZoomIn();
            ShowMapScale() ;//调整比例尺
        }

        //缩小图形
        private void tbZoomOut_Click(object sender, EventArgs e)
        {
            ZoomOut();
            ShowMapScale();//调整比例尺
        }

        //改变背景色（黑/白)
        private void btBackground_Click(object sender, EventArgs e)
        {
            if (!mIsBackground_Black)
            {
                btBackground.Text = "白色背景";
                btBackground.ToolTipText = "设置为白色背景";
            }
            else
            {
                btBackground.Text = "黑色背景";
                btBackground.ToolTipText = "设置为黑色背景";
            }//endelse

            mIsBackground_Black = !mIsBackground_Black;

            // 重画
            DrawAll_Map();
        }

        //是否显示坐标系
        private void btHasCoord_Click(object sender, EventArgs e)
        {
            if (!mHasCoord)
            {
                btHasCoord.Text = "无坐标轴";
                btHasCoord.ToolTipText = "设置不显示坐标轴";
            }
            else
            {
                btHasCoord.Text = "有坐标轴";
                btHasCoord.ToolTipText = "设置显示坐标轴";
            }//endelse

            mHasCoord = !mHasCoord;
            // 重画
            DrawAll_Map();
        }

        //存为图片
        private void tbSave_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveDlg = new SaveFileDialog();
            string fileName = "";
            DialogResult dlgR;
            {
                var withBlock = saveDlg;
                withBlock.Title = "图形转存";
                withBlock.Filter = "JPEG文件(*.jgp)|*.jpg|PNG文件(*.png)|*.png";
                dlgR = withBlock.ShowDialog();
                fileName = withBlock.FileName;
            }

            if (dlgR == DialogResult.Cancel) return;

            SavePicture(fileName);// 保存图片
        }
        # endregion

        # region 自定义函数
        /// <summary>
        /// 图形显示初始化
        ///</summary>
        private void InitlDispMap()
        {
            mIsBackground_Black = true; // 背景色为黑色
            mHasCoord = true; // 画坐标

            PointF CenPt0; // 实际坐标表示的画布中心点
            Point dpcentPt0; // 设备像素坐标表示的画布中心点
            PointF cenpt1=new PointF(); // 由图形包络矩形计算出的图形中心点

            BaseFunction.viewScale = 1.0; // 比例尺初始为1
            dpcentPt0 = new Point(VWidth / 2, VHeight / 2);
            BaseFunction.viewScale = GetViewScale(ref cenpt1); // 计算缩放比例
            CenPt0 = BaseFunction.DPToRP(dpcentPt0);

            Pan(cenpt1, CenPt0); // 把图形中心点平移到画布中心
        }

        /// <summary>
        /// 以画布左下角为原点，建立数学坐标系
        /// </summary>
        private void ToMathCoord(Graphics g)
        {
            BaseFunction.Kx = 25.3995 / g.DpiX;
            BaseFunction.Ky = 25.3995 / g.DpiY;
            VWidth = this.picMap.Width;
            VHeight = this.picMap.Height;

            BaseFunction.LX0 = 0;
            BaseFunction.LY0 = VHeight * BaseFunction.Ky;
        }

        /// <summary>
        ///  图形平移
        /// </summary>
        /// <param name="BeginP">平移起点</param>
        /// <param name="EndP">平移终点</param>
        private void Pan(PointF BeginP, PointF EndP)
        {
            float DX, DY;
            DX = EndP.X - BeginP.X;
            DY = EndP.Y - BeginP.Y;

            BaseFunction.RX0 = BaseFunction.RX0 - DX;
            BaseFunction.RY0 = BaseFunction.RY0 - DY;

            DrawAll_Map(); // 重画图元
        }

        /// <summary>
        /// 以左下角为基点图形放大2倍
        ///</summary>
        private void ZoomIn()
        {
            BaseFunction.viewScale *= 2;
            DrawAll_Map(); // 重画图元
        }

        /// <summary>
        ///以左下角为基点图形缩小2倍
        ///</summary>
        private void ZoomOut()
        {
            BaseFunction.viewScale *= 0.5;
            DrawAll_Map(); // 重画图元
        }

        /// <summary>
        /// 计算缩放比例，并返回图形中心点
        ///</summary>
        /// <param name="CenterPoint">图形中心点</param>
        /// <returns>缩放比例</returns>
        private float GetViewScale(ref PointF CenterPoint)
        {
            RectangleF gBound = GetBound(gList); // 获得图元列表的包络矩形
            if (gBound == null) return 1.0f; // 无包络矩形，默认为1

            float theWidth = 0.0f;
            float theHeight = 0.0f;

            theWidth = gBound.Width;
            theHeight = gBound.Height;

            // ***扩大范围1/10
            theWidth = theWidth + theWidth /10.0f;
            theHeight = theHeight + theHeight / 10.0f;

            CenterPoint.X = gBound.X + theWidth / 2.0f - theWidth /20.0f;
            CenterPoint.Y = gBound.Y + theHeight /2.0f - theHeight /20.0f;

            if (theWidth < 0.00001) theWidth = 0.00001f;
            if (theHeight < 0.00001) theHeight = 0.00001f;

            Point theDpPt0 = new Point(0, 0); // 设备坐标左下角点
            Point theDpPt1 = new Point(VWidth, VHeight); // 设备坐标右上角点
            PointF theRPt0 = BaseFunction.DPToRP(theDpPt0); // 转为实际坐标
            PointF theRPt1 = BaseFunction.DPToRP(theDpPt1);

            float viewscale1, viewscale2;

            viewscale1 = Math.Abs((theRPt1.X - theRPt0.X)) /theWidth; // 横向比例
            viewscale2 = Math.Abs((theRPt1.Y - theRPt0.Y)) /theHeight; // 纵向比例

            // 选比例小者返回
            if (viewscale1 > viewscale2)
                return viewscale2;
            else
                return viewscale1;
        }

        /// <summary>
        ///通过图元列表中的点图元，计算图形包络矩形
        ///</summary>
        /// <param name="gList">图元列表</param>
        /// <returns>包络矩形</returns>
        private RectangleF GetBound(List<GElement> gList)
        {
            List<double> ListX = new List<double>(); // 点图元的X坐标列表
            List<double> ListY = new List<double>(); // 点图元的Y坐标列表
            // 获得所有点图元坐标列表
            foreach (GElement ge in gList)
            {
                if (ge is PointN)
                {
                    PointN tempP =(PointN) ge;
                    ListX.Add(tempP.Point.X);
                    ListY.Add(tempP.Point.Y);
                }
                else if (ge is PointUn)
                {
                    PointUn tempP = (PointUn)ge;
                    ListX.Add(tempP.Point.X);
                    ListY.Add(tempP.Point.Y);
                }//endif
            }//endfor

            // 求最大、最小坐标值
            float minX, minY;
            float maxX, maxY;
            ListX.Sort();
            ListY.Sort();

            minX =(float) ListX[0];
            minY = (float)ListY[0];
            maxX = (float)ListX[ListX.Count - 1];
            maxY = (float)ListY[ListY.Count - 1];

            return new RectangleF(minX, minY, maxX - minX, maxY - minY);
        }

        /// <summary>
        /// 在窗体控件中展现图形
        /// </summary>
        private void DrawAll_Map()
        {
            Graphics g = this.picMap.CreateGraphics();
            DrawAll(g);
        }

        /// <summary>
        ///在画布中画出所有图元
        /// </summary>
        /// <remarks></remarks>
        private void DrawAll(Graphics g)
        {
            // 设置颜色
            Color background = Color.Black; // 背景色
            Color objColor = Color.White; // 对象颜色
            // 清除图形
            if (mIsBackground_Black)
            {
                background = Color.Black;
                objColor = Color.White;
            }
            else
            {
                background = Color.White;
                objColor = Color.Black;
            }
            // 清除图形对象
            // Dim g As Graphics = Me.picMap.CreateGraphics()
            g.Clear(background);
            foreach (GElement ge in gList)
            {
                if (ge.Color == Color.Black || ge.Color == Color.White) ge.Color = objColor;
                 ge.Draw(g);
            }

            // 画坐标系
            if (mHasCoord) DrawCoord(g, objColor);
        }

        /// <summary>
        /// /绘制坐标轴
        /// </summary>
        /// <param name="g"></param>
        /// <param name="background"></param>
        private void DrawCoord(System.Drawing.Graphics g, Color  color)
        {
            PointF pointSW, pointNE; // '显示区域的西南点,东北点(实际坐标)
            Point tempPt;

            tempPt = new Point(0, VHeight);
            pointSW = BaseFunction.DPToRP(tempPt);
            tempPt = new Point(VWidth, 0);
            pointNE = BaseFunction.DPToRP(tempPt);
             Coord coord = new Coord(pointSW, pointNE,color);
            coord.Draw(g);
        }

        /// <summary>
        /// 状态栏中显示图形比例尺
        ///</summary>
        private void ShowMapScale()
        {
            int scale = System.Convert.ToInt32(1.0 / (BaseFunction.viewScale * BaseFunction.Kx) * 1000); // 图形比例尺
            sbLabel2.Text = "比例尺  1:" + scale.ToString();
        }

        /// <summary>
        ///图形存为图片
        /// </summary>
        /// <param name="fileName">文件名</param>
        private void SavePicture(string fileName)
        {
            Bitmap bmp = new Bitmap(picMap.Width, picMap.Height); // 定义内存图片
            Graphics g = Graphics.FromImage(bmp); // 定义内存画布
            DrawAll(g); // 重画对象

            // 根据文件后缀保存图片
            string picType = fileName.Substring(fileName.Length - 3, 3); // 文件后缀
            switch (picType)
            {
                case "jpg":
                    {
                        bmp.Save(fileName, System.Drawing.Imaging.ImageFormat.Jpeg);
                        break;
                    }

                case "png":
                    {
                        bmp.Save(fileName, System.Drawing.Imaging.ImageFormat.Png);
                        break;
                    }
            }

            MessageBox.Show("转存图片成功！", "恭喜", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        # endregion
    }//endclass
}//endspace
