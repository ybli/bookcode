using matrix;
using NPOI.OpenXmlFormats.Wordprocessing;
using NPOI.XWPF.UserModel;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZedGraph;

namespace ARModel
{
    class Graph
    {

        #region
        ///// <summary>
        /////     设置字体格式
        ///// </summary>
        ///// <param name="doc"></param>
        ///// <param name="table"></param>
        ///// <param name="setText"></param>
        ///// <returns></returns>
        //public XWPFParagraph SetCellText(XWPFDocument doc, XWPFTable table, string setText)
        //{
        //    //table中的文字格式设置
        //    var para = new CT_P();
        //    var pCell = new XWPFParagraph(para, table.Body);
        //    pCell.Alignment = ParagraphAlignment.CENTER; //字体居中
        //    pCell.VerticalAlignment = TextAlignment.CENTER; //字体居中

        //    var r1c1 = pCell.CreateRun();
        //    r1c1.SetText(setText);
        //    r1c1.FontSize = 12;
        //    r1c1.SetFontFamily("华文楷体", FontCharRange.None); //设置雅黑字体

        //    return pCell;
        //}

        ///// <summary>
        /////     设置单元格格式
        ///// </summary>
        ///// <param name="doc">doc对象</param>
        ///// <param name="table">表格对象</param>
        ///// <param name="setText">要填充的文字</param>
        ///// <param name="align">文字对齐方式</param>
        ///// <param name="textPos">rows行的高度</param>
        ///// <returns></returns>
        //public XWPFParagraph SetCellText(XWPFDocument doc, XWPFTable table, string setText, ParagraphAlignment align,
        //    int textPos)
        //{
        //    var para = new CT_P();
        //    var pCell = new XWPFParagraph(para, table.Body);
        //    //pCell.Alignment = ParagraphAlignment.LEFT;//字体
        //    pCell.Alignment = align;

        //    var r1c1 = pCell.CreateRun();
        //    r1c1.SetText(setText);
        //    r1c1.FontSize = 12;
        //    r1c1.SetFontFamily("华文楷体", FontCharRange.None); //设置雅黑字体
        //    r1c1.SetTextPosition(textPos); //设置高度

        //    return pCell;
        //}



        #endregion

        /// <summary>
        /// 绘制拟合图
        /// </summary>
        /// <param name="dataGridView1"></param>
        /// <param name="zedGraphControl4"></param>
        /// <param name="Y"></param>
        /// <param name="FitY"></param>
        /// <param name="order"></param>
        /// <param name="PointNaem"></param>

        public static void GraphFitY(int Width, int Height, ZedGraphControl zedGraphControl4, Matrix Y, Matrix FitY, int order, string PointNaem, DataGridView dataGridView1 = null, bool Fag = true)
        {
         
            zedGraphControl4.BorderStyle = System.Windows.Forms.BorderStyle.None;
            MasterPane masterpane = zedGraphControl4.MasterPane;//获取zedGraphControl1的面板
            //去掉面板中所有的子面板，默认会有一个面板在里面,清除默认面板
            masterpane.PaneList.Clear();
            int Days = Y.Rows;
            if (Fag)
                zedGraphControl4.Visible = true;
            else
                zedGraphControl4.Visible = false;
            zedGraphControl4.IsShowCursorValues = true;//该值确定当鼠标位于ZedGraph.Chart.Rect内时是否将显示显示当前比例尺值的工具提示。
            zedGraphControl4.IsEnableSelection = true;
            //创建一个面板
            GraphPane graphpane = new GraphPane();  //拟合图
            GraphPane graphpane1 = new GraphPane();//残差图
            graphpane.XAxis.Scale.Max = Days;//设置最大刻度                                           //设置公共信息
            SetGraphics(graphpane);
            SetGraphics(graphpane1);
            graphpane.YAxis.MajorGrid.IsZeroLine = false;
            graphpane1.YAxis.MajorGrid.IsZeroLine = false;
            //创建点序列
            PointPairList list = new PointPairList();//创建点的集合 
            if (dataGridView1 != null)
            {


                dataGridView1.RowHeadersVisible = false;//隐藏行头
                dataGridView1.Columns.Add("col1", "期数");
                dataGridView1.Columns.Add("col2", "观测值(mm)");
                dataGridView1.Columns.Add("col3", "拟合值(mm)");
                dataGridView1.Columns.Add("col4", "残差(mm)");
                foreach (DataGridViewColumn item in dataGridView1.Columns)//单元格居中
                {
                    item.SortMode = DataGridViewColumnSortMode.NotSortable;
                    item.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    item.DefaultCellStyle.BackColor = Color.Gainsboro;
                }
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dataGridView1.EnableHeadersVisualStyles = false;//在启动了可视样式的时候，BackColor和ForeColor的值会被忽略。
                dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.Aqua;
                dataGridView1.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
            double[] y = new double[Days];
            double[] _y = new double[Days];
            double[] x = new double[Days];
            double[] error = new double[Days];
            double sd = 0;


            if (!Fag)
            {
             
                zedGraphControl4.Size = new Size(920 - 120, 480);
                graphpane.Rect = new RectangleF(15, 30, 900 - 120, 280);//设置分图的大小和位置
                graphpane.Chart.Rect = new RectangleF(95, 80, 750 - 120, 200);
                graphpane1.Rect = new RectangleF(15, 320, 900 - 120, 150);//设置分图的大小和位置
                graphpane1.Chart.Rect = new RectangleF(95, 330, 750 - 120, 80);
            }
            else
            {
                zedGraphControl4.Size = new Size((int)(0.94117 * Width), (int)(0.65753 * Height));
                graphpane.Rect = new RectangleF((int)(0.01764 * Width), (int)(0.04109 * Height), (int)(0.91764 * Width), (int)(0.38356 * Height));//设置分图的大小和位置900, 300
                graphpane.Chart.Rect = new RectangleF((int)(0.01764 * Width) + 60, (int)(0.04109 * Height) + 40, (int)(0.74117 * Width), (int)(0.27397 * Height));
                graphpane1.Rect = new RectangleF((int)(0.01764 * Width), (int)(0.43835 * Height), (int)(0.91764 * Width), (int)(0.20547 * Height));//设置分图的大小和位置
                graphpane1.Chart.Rect = new RectangleF((int)(0.01764 * Width) + 60, (int)(0.45205 * Height), (int)(0.74117 * Width), (int)(0.27397 * Height) - 100 - (int)(0.01369 * Height));
            }
            for (int i = 0; i < Days; i++)
            {


                x[i] = i + 1;
                y[i] = Y[i, 0];
                _y[i] = FitY[i, 0];
                error[i] = FitY[i, 0] - Y[i, 0];
                if (dataGridView1 != null)
                {
                    dataGridView1.Rows.Add();
                    dataGridView1[3, i].Value = Math.Round(error[i], 2);
                    dataGridView1[0, i].Value = "第" + (order + 1 + i).ToString() + "期";
                    dataGridView1[1, i].Value = Math.Round(y[i], 2);
                    dataGridView1[2, i].Value = Math.Round(_y[i], 2);
                }
                sd += Math.Pow(error[i], 2);
            }
            sd /= Days;
            sd = Math.Sqrt(sd);

            graphpane.YAxis.Title.Text = "位移量/mm";
            graphpane.YAxis.Title.FontSpec.Family = "Arial";
            graphpane.XAxis.Scale.IsVisible = false;
            graphpane.XAxis.Scale.MajorStep = x.Length / graphpane.Rect.Width * 60;
            graphpane.YAxis.Title.FontSpec.Size = 15;
            graphpane.YAxis.Title.FontSpec.IsBold = false;
            graphpane.Legend.Position = LegendPos.TopCenter;
            graphpane.Legend.Border.IsVisible = false;
            graphpane.Legend.FontSpec.Family = "Arial";
            graphpane.Legend.FontSpec.Size = 15;
            graphpane.Legend.FontSpec.IsBold = false;
            graphpane1.XAxis.Scale.Max = Days;//设置最大刻度
            double min, max;
            if ((min = Math.Abs(error.Min())) > (max = Math.Abs(error.Max())))
            {
                graphpane1.YAxis.Scale.Max = (int)min + 1;    //设置最小刻度
                graphpane1.YAxis.Scale.Min = -(int)min - 1;     //设置最大刻度
            }
            else
            {
                graphpane1.YAxis.Scale.Max = (int)max + 1;    //设置最小刻度
                graphpane1.YAxis.Scale.Min = -(int)max - 1;     //设置最大刻度
            }
            graphpane1.YAxis.Scale.MajorStep = graphpane1.YAxis.Scale.Max;//设置大刻度步长
            graphpane1.XAxis.Scale.IsSkipLastLabel = false;

            graphpane1.YAxis.Title.Text = "△/mm";
            graphpane1.YAxis.Title.FontSpec.Family = "Arial";
            graphpane1.YAxis.Title.FontSpec.Size = 15;
            graphpane1.YAxis.Title.FontSpec.IsBold = false;

            graphpane1.XAxis.Title.Text = "期数";
            graphpane1.XAxis.Title.FontSpec.Family = "Arial";
            graphpane1.XAxis.Title.FontSpec.Size = 15;
            graphpane1.XAxis.Title.FontSpec.IsBold = false;


            LineItem myline = graphpane.AddCurve("观测值", null, y, Color.SpringGreen, SymbolType.None);
            LineItem myline1 = graphpane.AddCurve("拟合值", null, _y, Color.Red, SymbolType.None);
            LineItem myline3 = graphpane1.AddCurve("残差", null, error, Color.Red, SymbolType.None);
            myline.Line.Width = 1.5F;//线宽
            myline1.Line.Width = 1.5F;//线宽
            myline3.Line.Width = 1.5F;//线宽
            graphpane1.Legend.IsVisible = false;

            float width = graphpane.Chart.Rect.Width;
            float heigh = graphpane.Chart.Rect.Height;
            float width1 = graphpane1.Chart.Rect.Width;
            float heigh1 = graphpane1.Chart.Rect.Height;
            TextObj text = new TextObj(PointNaem, 2 / width, 2 / heigh);  //定义文本对象
            text.Location.AlignV = AlignV.Top;//确定文本的x轴为于文本的最上端
            text.Location.AlignH = AlignH.Left;//确定文本的y轴为于文本的最左端
            text.Location.CoordinateFrame = CoordType.ChartFraction;//设置文本的坐标系统，原点位于内框的左上角
            text.FontSpec.Border.IsVisible = false;
            text.FontSpec.FontColor = Color.Blue;
            text.FontSpec.Size = 15;
            graphpane.GraphObjList.Add(text);
            TextObj text1 = new TextObj(PointNaem, 2 / width1, 2 / heigh1);  //定义文本对象
            text1.Location.AlignV = AlignV.Top;//确定文本的x轴为于文本的最上端
            text1.Location.AlignH = AlignH.Left;//确定文本的y轴为于文本的最左端
            text1.Location.CoordinateFrame = CoordType.ChartFraction;//设置文本的坐标系统，原点位于内框的左上角
            text1.FontSpec.Border.IsVisible = false;
            text1.FontSpec.FontColor = Color.Blue;
            text1.FontSpec.Size = 15;
            graphpane1.GraphObjList.Add(text1);
            graphpane1.XAxis.Scale.MajorStep = x.Length / graphpane.Rect.Width * 60;
            TextObj text2 = new TextObj("RMS=" + Math.Round(sd, 2) + "mm", 2 / width1, (heigh1 - 2) / heigh1);  //定义文本对象
            text2.Location.AlignV = AlignV.Bottom;//确定文本的x轴为于文本的最下端
            text2.Location.AlignH = AlignH.Left;  //确定文本的y轴为于文本的最左端
            text2.Location.CoordinateFrame = CoordType.ChartFraction;//设置文本的坐标系统，原点位于内框的左上角
            text2.FontSpec.Border.IsVisible = false;
            text2.FontSpec.FontColor = Color.Blue;
            text2.FontSpec.Size = 15;
            graphpane1.GraphObjList.Add(text2);
            masterpane.Add(graphpane);
            masterpane.Add(graphpane1);
            Graphics gd = zedGraphControl4.CreateGraphics();
            masterpane.AxisChange(gd);
            zedGraphControl4.Refresh();

        }

        private static void SetGraphics(GraphPane graphpane)
        {
            graphpane.IsFontsScaled = false; //不随缩放改变字体大小        
            graphpane.Title.FontSpec.FontColor = Color.Black;
            graphpane.Title.FontSpec.Size = 15;
            graphpane.Title.FontSpec.IsBold = false;     //设置字体的粗细
            graphpane.XAxis.MajorTic.IsOpposite = false;//大刻度不在对面显示
            graphpane.XAxis.MinorTic.IsOpposite = false;//小刻度不在对面显示
            graphpane.XAxis.MinorTic.IsOutside = false; //设置小刻度线不显示在里面 
            graphpane.XAxis.MajorTic.IsOutside = false; //设置小刻度线不显示在里面 
            graphpane.XAxis.MinorTic.IsInside = false;  //去掉小刻度线
            graphpane.XAxis.MajorGrid.IsVisible = false;  //将X轴的大刻度网线去掉                                                          //graphpane.XAxis.Scale.BaseTic = ;             //显示的起始刻度值 从1开始显示
            graphpane.XAxis.Scale.Min = 1;                //设置起始刻度 刻度从0开始
                                                          // graphpane.XAxis.Scale.LabelGap = 0;          
            graphpane.XAxis.Scale.BaseTic = 1;           //设置起始标签的刻度位置
            graphpane.XAxis.Scale.FontSpec.Size = 15;
            graphpane.XAxis.Scale.FontSpec.Angle = 0;
            // graphpane.XAxis.Type = AxisType.Text; //设置X轴的刻度为文本格式
            graphpane.YAxis.MajorGrid.IsZeroLine = true;//去掉y=0的那条线
            graphpane.Chart.Border.IsVisible = true;//设置边框
                                                    // graphpane.YAxis.MajorTic.IsOpposite = false;//大刻度不在对面显示               
            graphpane.YAxis.MinorTic.IsOpposite = false;//小刻度不在对面显示
            graphpane.YAxis.MinorTic.IsOutside = false; //设置小刻度线不显示在里面 
            graphpane.YAxis.MajorTic.IsOutside = false; //设置小刻度线不显示在里面 
                                                        // graphpane.XAxis.IsAxisSegmentVisible = false;  //不设置0轴线
            graphpane.YAxis.MinorTic.IsInside = false;  //去掉小刻度线
            graphpane.YAxis.MajorGrid.IsVisible = true; //将Y轴的大刻度网线显示
            graphpane.YAxis.MajorGrid.DashOn = 2F; //Y轴的大刻度网线虚线线长
            graphpane.YAxis.MajorGrid.Color = Color.Gray; //设置格网的颜色
            graphpane.YAxis.Scale.FontSpec.Size = 15;

            graphpane.Legend.IsVisible = true;//是否设置图例
            graphpane.Border.IsVisible = false;//去掉外框线

        }

        public static void GraphBIC(int Width, int Height, ZedGraphControl zedGraphControl3, Matrix BIC, Matrix AIC, bool Fag = true)
        {


         

            MasterPane masterpane = zedGraphControl3.MasterPane;
            masterpane.PaneList.Clear();
            zedGraphControl3.Visible = true;
            zedGraphControl3.IsShowCursorValues = true;//该值确定当鼠标位于ZedGraph.Chart.Rect内时是否将显示显示当前比例尺值的工具提示。
            zedGraphControl3.IsEnableSelection = true;
            zedGraphControl3.BorderStyle = System.Windows.Forms.BorderStyle.None;

            GraphPane graphpane = new GraphPane();//BIC图
            if (!Fag)

            {
                zedGraphControl3.Location = new Point((int)(0.40584 * Width), 0);
                zedGraphControl3.Size = new Size((int)(0.47889* Width), (int)(0.84782* Height));               
                graphpane.Rect = new RectangleF((int)(0.01217*Width), (int)(0.01672*Height), (int)(0.43019*Width), (int)(0.80267*Height));//设置分图的大小和位置900, 300
                graphpane.Chart.Rect = new RectangleF((int)(0.01217 * Width)+60, (int)(0.01672 * Height)+50, (int)(0.35714 * Width), (int)(0.80267 * Height) - 60 - 50);

            }
            else
            {
                zedGraphControl3.Location = new Point(500, 0);
                zedGraphControl3.Size = new Size(590, 507);
                graphpane.Rect = new RectangleF(15, 10, 530, 480);//设置分图的大小和位置900, 300
                graphpane.Chart.Rect = new RectangleF(95, 60, 440, 340);

            }
           

            graphpane.XAxis.Scale.Max = BIC.Rows;
            SetGraphics(graphpane);
            graphpane.YAxis.MajorGrid.IsVisible = false;
            double[] BIC_x = new double[BIC.Rows];
            double[] BIC_y = new double[BIC.Rows];
            double[] AIC_y = new double[AIC.Rows];
            for (int i = 0; i < BIC.Rows; i++)
            {
                BIC_x[i] = i + 1;
                BIC_y[i] = BIC[i, 0];
                AIC_y[i] = AIC[i, 0];
            }
            LineItem myline = graphpane.AddCurve("BIC", BIC_x, BIC_y, Color.SpringGreen, SymbolType.Square);
            LineItem myline1 = graphpane.AddCurve("AIC", BIC_x, AIC_y, Color.Black, SymbolType.Star);
            graphpane.XAxis.Title.Text = "时间延迟";
            graphpane.YAxis.Title.Text = "BIC";

            myline.Symbol.Size = 5.0F; //设置节点形状大小
            myline.Symbol.Fill = new Fill(Color.Red);//设置节点填充
            myline.Symbol.Border.Color = Color.Red;
            myline.Line.Width = 1;//线宽

            myline1.Symbol.Size = 5.0F; //设置节点形状大小
            myline1.Symbol.Fill = new Fill(Color.Red);//设置节点填充
            myline1.Symbol.Border.Color = Color.Blue;
            myline1.Line.Width = 1;//线宽

            masterpane.Add(graphpane);
            Graphics gd = zedGraphControl3.CreateGraphics();
            masterpane.AxisChange(gd);
            zedGraphControl3.Refresh();
        }

        public static void GraphPCF(int Width, int Height, ZedGraphControl zedGraphControl1, Matrix PCF, Matrix PACF, bool Fag = true)
        {

           
            MasterPane masterpane = zedGraphControl1.MasterPane;
            masterpane.PaneList.Clear();
            zedGraphControl1.Visible = true;
            zedGraphControl1.IsShowCursorValues = true;//该值确定当鼠标位于ZedGraph.Chart.Rect内时是否将显示显示当前比例尺值的工具提示。
            zedGraphControl1.IsEnableSelection = true;
            zedGraphControl1.BorderStyle = System.Windows.Forms.BorderStyle.None;

            zedGraphControl1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            GraphPane graphpane = new GraphPane();//ACF图
            GraphPane graphpane1 = new GraphPane();//PACF图
            if (!Fag)
            {
                zedGraphControl1.Location = new Point((int)(0.008116*Width), 0);
                zedGraphControl1.Size = new Size((int)(0.39772 * Width), (int)(0.84782 * Height));
                graphpane.Rect = new RectangleF((int)(0.01217 * Width), (int)(0.01672 * Height), (int)(0.34090 * Width), (int)(0.41806 * Height));//设置分图的大小和位置900, 300
                graphpane.Chart.Rect = new RectangleF((int)(0.01217 * Width) + 60, (int)(0.10033 * Height), (int)(0.25974 * Width), (int)(0.41806 * Height) - 45 - (int)(0.10033 * Height));
                graphpane1.Rect = new RectangleF((int)(0.01217 * Width), (int)(0.41806 * Height), (int)(0.34090 * Width), (int)(0.41806 * Height));//设置分图的大小和位置
                graphpane1.Chart.Rect = new RectangleF((int)(0.01217 * Width) + 60, (int)(0.43478 * Height), (int)(0.25974 * Width), (int)(0.23411 * Height));

            }
            else
            {
                zedGraphControl1.Location = new Point(10, 0);
                zedGraphControl1.Size = new Size(490, 507);

                graphpane.Rect = new RectangleF(15, 10, 420, 250);//设置分图的大小和位置900, 300
                graphpane.Chart.Rect = new RectangleF(95, 60, 320, 140);
                graphpane1.Rect = new RectangleF(15, 250, 420, 250);//设置分图的大小和位置
                graphpane1.Chart.Rect = new RectangleF(95, 260, 320, 140);
            }
            graphpane.XAxis.Scale.Max = PCF.Columns;
            graphpane1.XAxis.Scale.Max = PACF.Columns;
            SetGraphics(graphpane);
            SetGraphics(graphpane1);
            graphpane1.YAxis.MajorGrid.IsVisible = false;
            double[] ACF_x = new double[PCF.Columns];
            double[] ACF_y = new double[PCF.Columns];
            double[] PACF_x = new double[PACF.Columns];
            double[] PACF_y = new double[PACF.Columns];
            for (int i = 0; i < PCF.Columns; i++)
            {
                ACF_x[i] = i + 1;
                PACF_x[i] = i + 1;
                ACF_y[i] = PCF[0, i];
                PACF_y[i] = PACF[0, i];
            }
            BarItem bar1 = graphpane.AddBar("PCF", ACF_x, ACF_y, Color.Blue);
            graphpane.XAxis.Title.Text = "时间延迟";
            graphpane.YAxis.Title.Text = "PCF";
            bar1.Label.IsVisible = false;
            BarItem bar2 = graphpane1.AddBar("PACF", PACF_x, PACF_y, Color.Blue);
            graphpane.YAxis.MajorGrid.IsVisible = false;
            graphpane1.XAxis.Title.Text = "时间延迟";
            graphpane1.YAxis.Title.Text = "PACF";
            bar2.Label.IsVisible = false;

            masterpane.Add(graphpane);
            masterpane.Add(graphpane1);
            Graphics gd = zedGraphControl1.CreateGraphics();
            masterpane.AxisChange(gd);
            zedGraphControl1.Refresh();

        }

        public static void GraphPreY(int Width, int Height, ZedGraphControl zedGraphControl2, Matrix _PreY, Matrix PreEoror, Matrix PreYSource, int DeformationDays, string PointName, DataGridView dataGridView2 = null, int MulStep = 1, bool Fag = true)
        {
            MasterPane masterpane = zedGraphControl2.MasterPane;

            masterpane.PaneList.Clear();
            if (Fag)
                zedGraphControl2.Visible = true;
            else
                zedGraphControl2.Visible = false;

            zedGraphControl2.IsShowCursorValues = true;//该值确定当鼠标位于ZedGraph.Chart.Rect内时是否将显示显示当前比例尺值的工具提示。
            zedGraphControl2.IsEnableSelection = true;
            zedGraphControl2.BorderStyle = System.Windows.Forms.BorderStyle.None;

            int PreDays = _PreY.Rows;
            GraphPane graphpane = new GraphPane();//预测拟合图
            GraphPane graphpane1 = new GraphPane();//预测残差图
            graphpane.XAxis.Scale.Max = PreDays;
            //设置公共信息
            SetGraphics(graphpane);
            SetGraphics(graphpane1);
            graphpane.YAxis.MajorGrid.IsZeroLine = false;
            graphpane1.YAxis.MajorGrid.IsZeroLine = false;
            graphpane1.XAxis.Scale.Max = PreDays;
            graphpane1.YAxis.MajorGrid.IsVisible = false;
          

            graphpane.YAxis.Title.Text = "位移量/mm";
            graphpane.Legend.FontSpec.Family = "Arial";
            graphpane.YAxis.Title.FontSpec.Size = 15;
            graphpane.YAxis.Title.FontSpec.IsBold = false;
            graphpane.Legend.Position = LegendPos.TopCenter;
            graphpane.Legend.Border.IsVisible = false;
            graphpane.Legend.FontSpec.Size = 15;
            graphpane.YAxis.Title.FontSpec.Family = "Arial";
            graphpane.Legend.FontSpec.IsBold = false;
            graphpane.YAxis.Scale.MajorStep = 2;
            graphpane.Legend.IsShowLegendSymbols = true;
            graphpane1.YAxis.Title.Text = "△/mm";
            graphpane1.YAxis.Title.FontSpec.Size = 15;
            graphpane1.YAxis.Title.FontSpec.IsBold = false;
            graphpane1.YAxis.Title.FontSpec.Family = "Arial";

            graphpane1.XAxis.Title.Text = "期数";
            graphpane1.XAxis.Title.FontSpec.Family = "Arial";
            graphpane1.XAxis.Title.FontSpec.Size = 15;
            graphpane1.XAxis.Title.FontSpec.IsBold = false;
            double[] y = new double[PreDays];
            double[] _y = new double[PreDays];
            double[] x = new double[PreDays];
            double[] error = new double[PreDays];
            if (dataGridView2 != null)
            {


                dataGridView2.RowHeadersVisible = false;//隐藏行头
                dataGridView2.Columns.Add("col1", "期数");
                dataGridView2.Columns.Add("col2", "观测值(mm)");
                dataGridView2.Columns.Add("col3", "预测值(mm)");
                dataGridView2.Columns.Add("col4", "残差(mm)");
                foreach (DataGridViewColumn item in dataGridView2.Columns)//单元格居中
                {
                    item.SortMode = DataGridViewColumnSortMode.NotSortable;
                    item.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    item.DefaultCellStyle.BackColor = Color.Gainsboro;
                }

                dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dataGridView2.EnableHeadersVisualStyles = false;//在启动了可视样式的时候，BackColor和ForeColor的值会被忽略。
                dataGridView2.ColumnHeadersDefaultCellStyle.BackColor = Color.Aqua;
                dataGridView2.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
            string[] _x = new string[PreDays];
            double sd = 0.0;
            for (int i = 0; i < PreDays; i++)
            {

                x[i] = DeformationDays + i + MulStep - 1;

                _y[i] = _PreY[i, 0];
                y[i] = PreYSource[i, 0];

                error[i] = PreEoror[i, 0];
                sd += Math.Pow(error[i], 2);
                _x[i] = (DeformationDays + i + 1 + MulStep - 1).ToString();
                //获取曲线的点序列    
                if (dataGridView2 != null)
                {
                    dataGridView2.Rows.Add();
                    dataGridView2[0, i].Value = "第" + (DeformationDays + i + 1 + MulStep - 1).ToString() + "期";
                    dataGridView2[1, i].Value = Math.Round(y[i], 2);
                    dataGridView2[2, i].Value = Math.Round(_y[i], 2);
                    dataGridView2[3, i].Value = Math.Round(error[i], 2);
                }
            }
            sd /= PreDays;
            sd = Math.Sqrt(sd);
            if (!Fag)
            {
                zedGraphControl2.Size = new Size(920 - 120, 480);
                graphpane.Rect = new RectangleF(15, 30, 900 - 120, 280);//设置分图的大小和位置
                graphpane.Chart.Rect = new RectangleF(95, 80, 750 - 120, 200);
                graphpane1.Rect = new RectangleF(15, 320, 900 - 120, 150);//设置分图的大小和位置
                graphpane1.Chart.Rect = new RectangleF(95, 330, 750 - 120, 80);
            }
            else
            {
                zedGraphControl2.Size = new Size((int)(0.94117 * Width), (int)(0.65753 * Height));
                graphpane.Rect = new RectangleF((int)(0.01764 * Width), (int)(0.04109 * Height), (int)(0.91764 * Width), (int)(0.38356 * Height));//设置分图的大小和位置900, 300
                graphpane.Chart.Rect = new RectangleF((int)(0.01764 * Width) + 60, (int)(0.04109 * Height) + 40, (int)(0.74117 * Width), (int)(0.27397 * Height));
                graphpane1.Rect = new RectangleF((int)(0.01764 * Width), (int)(0.43835 * Height), (int)(0.91764 * Width), (int)(0.20547 * Height));//设置分图的大小和位置
                graphpane1.Chart.Rect = new RectangleF((int)(0.01764 * Width) + 60, (int)(0.45205 * Height), (int)(0.74117 * Width), (int)(0.27397 * Height) - 100 - (int)(0.01369 * Height));
            }
        
           



            graphpane.XAxis.Scale.IsVisible = false;
            graphpane.Legend.Position = LegendPos.TopCenter;
            graphpane.Legend.Border.IsVisible = false;
            graphpane.XAxis.Scale.MajorStep = x.Length / graphpane.Rect.Width * 60;
          
            

            graphpane1.XAxis.Scale.MajorStep = x.Length / graphpane.Rect.Width * 60;
            double min, max;
            if ((min = Math.Abs(error.Min())) > (max = Math.Abs(error.Max())))
            {
                graphpane1.YAxis.Scale.Max = (int)min + 1;    //设置最小刻度
                graphpane1.YAxis.Scale.Min = -(int)min - 1;     //设置最大刻度
            }
            else
            {
                graphpane1.YAxis.Scale.Max = (int)max + 1;    //设置最小刻度
                graphpane1.YAxis.Scale.Min = -(int)max - 1;     //设置最大刻度
            }
            graphpane1.YAxis.Scale.MajorStep = graphpane1.YAxis.Scale.Max;//设置大刻度步长
            LineItem myline = graphpane.AddCurve("观测值", null, y, Color.Green, SymbolType.Diamond);
            LineItem myline1 = graphpane.AddCurve("预测值", null, _y, Color.Red, SymbolType.Circle);
            LineItem myline3 = graphpane1.AddCurve("残差", null, error, Color.Red, SymbolType.Circle);
            graphpane1.XAxis.Type = AxisType.Text;
            graphpane1.XAxis.Scale.TextLabels = _x;
            myline.Symbol.Size = 3.0F; //设置节点形状大小
            myline.Symbol.Fill = new Fill(Color.Green);//设置节点填充
            myline.Line.Width = 1;//线宽

            zedGraphControl2.IsShowCursorValues = true;
            myline1.Symbol.Size = 3.0F; //设置节点形状大小
            myline1.Symbol.Fill = new Fill(Color.Red);//设置节点填充
            myline1.Line.Width = 1;//线宽

            myline3.Symbol.Size = 3.0F; //设置节点形状大小
            myline3.Symbol.Fill = new Fill(Color.Red);//设置节点填充
            myline3.Line.Width = 1;//线宽
            graphpane1.Legend.IsVisible = false;

            myline.Symbol.Size = 5.0F; //设置节点形状大小
            myline.Symbol.Fill = new Fill(Color.Green);//设置节点填充
            myline.Line.Width = 1;//线宽
            myline1.Symbol.Size = 5.0F; //设置节点形状大小
            myline1.Symbol.Fill = new Fill(Color.Red);//设置节点填充
            myline1.Line.Width = 1;//线宽

            myline3.Symbol.Size = 5.0F; //设置节点形状大小
            myline3.Symbol.Fill = new Fill(Color.Red);//设置节点填充
            myline3.Line.Width = 1;//线宽
            graphpane1.Legend.IsVisible = false;
            graphpane1.XAxis.Scale.MajorStep = x.Length / graphpane.Rect.Width * 60;
            float width = graphpane.Chart.Rect.Width;
            float heigh = graphpane.Chart.Rect.Height;
            float width1 = graphpane1.Chart.Rect.Width;
            float heigh1 = graphpane1.Chart.Rect.Height;
            TextObj text = new TextObj(PointName + ":" + MulStep + "步预测", 2 / width, 2 / heigh);  //定义文本对象
            text.Location.AlignV = AlignV.Top;//确定文本的x轴为于文本的最上端
            text.Location.AlignH = AlignH.Left;//确定文本的y轴为于文本的最左端
            text.Location.CoordinateFrame = CoordType.ChartFraction;//设置文本的坐标系统，原点位于内框的左上角
            text.FontSpec.Border.IsVisible = false;
            text.FontSpec.FontColor = Color.Blue;
            text.FontSpec.Size = 15;
            graphpane.GraphObjList.Add(text);
            TextObj text1 = new TextObj(PointName, 2 / width1, 2 / heigh1);  //定义文本对象
            text1.Location.AlignV = AlignV.Top;//确定文本的x轴为于文本的最上端
            text1.Location.AlignH = AlignH.Left;//确定文本的y轴为于文本的最左端
            text1.Location.CoordinateFrame = CoordType.ChartFraction;//设置文本的坐标系统，原点位于内框的左上角
            text1.FontSpec.Border.IsVisible = false;
            text1.FontSpec.FontColor = Color.Blue;
            text1.FontSpec.Size = 15;
            graphpane1.GraphObjList.Add(text1);
            graphpane1.XAxis.Scale.MajorStep = x.Length / graphpane.Rect.Width * 60;
            TextObj text2 = new TextObj("RMS=" + Math.Round(sd, 2) + "mm", 2 / width1, (heigh1 - 2) / heigh1);  //定义文本对象
            text2.Location.AlignV = AlignV.Bottom;//确定文本的x轴为于文本的最下端
            text2.Location.AlignH = AlignH.Left;  //确定文本的y轴为于文本的最左端
            text2.Location.CoordinateFrame = CoordType.ChartFraction;//设置文本的坐标系统，原点位于内框的左上角
            text2.FontSpec.Border.IsVisible = false;
            text2.FontSpec.FontColor = Color.Blue;
            text2.FontSpec.Size = 15;
            graphpane1.GraphObjList.Add(text2);
            masterpane.Add(graphpane);
            masterpane.Add(graphpane1);
            Graphics gd = zedGraphControl2.CreateGraphics();
            masterpane.AxisChange(gd);
            zedGraphControl2.Refresh();
        }

      


        public static void GraphDataSource(int Width, int Height, ZedGraphControl zedGraphControl5, double[,] DataSource, string PointNaem, DataGridView dataGridView4 = null)
        {


           
            zedGraphControl5.BorderStyle = System.Windows.Forms.BorderStyle.None;
            MasterPane masterpane = zedGraphControl5.MasterPane;//获取zedGraphControl1的面板
                                                                //去掉面板中所有的子面板，默认会有一个面板在里面,清除默认面板
            masterpane.PaneList.Clear();
            int Days = DataSource.GetLength(0);

            zedGraphControl5.Visible = true;
            zedGraphControl5.IsShowCursorValues = true;//该值确定当鼠标位于ZedGraph.Chart.Rect内时是否将显示显示当前比例尺值的工具提示。
            zedGraphControl5.IsEnableSelection = true;
            //创建一个面板
            GraphPane graphpane = new GraphPane();  //拟合图

            graphpane.XAxis.Scale.Max = Days;//设置最大刻度                                           //设置公共信息
            SetGraphics(graphpane);
            graphpane.YAxis.MajorGrid.IsZeroLine = false;
            //创建点序列
            PointPairList list = new PointPairList();//创建点的集合 
            if (dataGridView4 != null)
            {


                dataGridView4.RowHeadersVisible = false;//隐藏行头
                dataGridView4.Columns.Add("col1", "期数");
                dataGridView4.Columns.Add("col2", "观测值(mm)");
                foreach (DataGridViewColumn item in dataGridView4.Columns)//单元格居中
                {
                    item.SortMode = DataGridViewColumnSortMode.NotSortable;
                    item.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    item.DefaultCellStyle.BackColor = Color.Gainsboro;
                }
                dataGridView4.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dataGridView4.EnableHeadersVisualStyles = false;//在启动了可视样式的时候，BackColor和ForeColor的值会被忽略。
                dataGridView4.ColumnHeadersDefaultCellStyle.BackColor = Color.Aqua;
                dataGridView4.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
            double[] y = new double[Days];
            double[] x = new double[Days];
            for (int i = 0; i < Days; i++)
            {
                x[i] = i + 1;
                y[i] = DataSource[i, 0];
                if (dataGridView4 != null)
                {
                    dataGridView4.Rows.Add();
                    dataGridView4[0, i].Value = "第" + (1 + i).ToString() + "期";
                    dataGridView4[1, i].Value = Math.Round(y[i], 2);
                }
            }




            zedGraphControl5.Size = new Size((int)(0.96740 * Width), (int)(0.59834 * Height));
            graphpane.Rect = new RectangleF((int)(0.015778 * Width), (int)(0.04126 * Height), (int)(0.94637 * Width), (int)(0.55020 * Height));//设置分图的大小和位置900, 300
            graphpane.Chart.Rect = new RectangleF((int)(0.015778 * Width) + 55, (int)(0.11004 * Height), (int)(0.78864 * Width), (int)(0.55020 * Height) - 40 - (int)(0.11004 * Height));


            graphpane.XAxis.Title.Text = "期数";
            graphpane.XAxis.Title.FontSpec.Family = "Arial";
            graphpane.XAxis.Title.FontSpec.Size = 15;
            graphpane.XAxis.Title.FontSpec.IsBold = false;
            graphpane.XAxis.Title.IsVisible = true;

            graphpane.YAxis.Title.Text = "位移量/mm";
            graphpane.YAxis.Title.FontSpec.Family = "Arial";
            graphpane.XAxis.Scale.IsVisible = true;
            graphpane.XAxis.Scale.MajorStep = x.Length / graphpane.Rect.Width * 60;
            graphpane.YAxis.Title.FontSpec.Size = 15;
            graphpane.YAxis.Title.FontSpec.IsBold = false;
            graphpane.Legend.Position = LegendPos.TopCenter;
            graphpane.Legend.Border.IsVisible = false;
            graphpane.Legend.FontSpec.Family = "Arial";
            graphpane.Legend.FontSpec.Size = 15;
            graphpane.Legend.FontSpec.IsBold = false;
            graphpane.Legend.IsVisible = false;
            LineItem myline = graphpane.AddCurve("观测值", null, y, Color.DeepPink, SymbolType.None);
            myline.Line.Width = 1.5F;//线宽

            float width = graphpane.Chart.Rect.Width;
            float heigh = graphpane.Chart.Rect.Height;

            TextObj text = new TextObj(PointNaem, 2 / width, 2 / heigh);  //定义文本对象
            text.Location.AlignV = AlignV.Top;//确定文本的x轴为于文本的最上端
            text.Location.AlignH = AlignH.Left;//确定文本的y轴为于文本的最左端
            text.Location.CoordinateFrame = CoordType.ChartFraction;//设置文本的坐标系统，原点位于内框的左上角
            text.FontSpec.Border.IsVisible = false;
            text.FontSpec.FontColor = Color.Blue;
            text.FontSpec.Size = 15;
            graphpane.GraphObjList.Add(text);
            masterpane.Add(graphpane);

            Graphics gd = zedGraphControl5.CreateGraphics();
            masterpane.AxisChange(gd);

            zedGraphControl5.Refresh();
        }





    }
}
