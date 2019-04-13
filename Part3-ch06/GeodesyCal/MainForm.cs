using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GeodesyCal
{
    public partial class MainForm : Form
    {
        Ellipsoid MyEllipsoid;
        List<GeodesicInfo> PosData;
        List<GeodesicInfo> NegData;
        DataTable PosDataTable;
        DataTable NegDataTable;
        Bitmap MyNegImage;
        Bitmap MyPosImage;
        StringBuilder NegReport;
        StringBuilder PosReport;
        DrawPro MyDrawPro;

        BesselDirect DirectPro;
        BesselInverse InversePro;

        //拖动图片控件
        bool iflag = false;
        int ix, iy;

        public MainForm()
        {
            InitializeComponent();
            MyDrawPro = new DrawPro();
        }

        private void PosData2Table()
        {
            PosDataTable = new DataTable();
            PosDataTable.Columns.Add("起点", System.Type.GetType("System.String"));
            PosDataTable.Columns.Add("B1", System.Type.GetType("System.String"));
            PosDataTable.Columns.Add("L1", System.Type.GetType("System.String"));
            PosDataTable.Columns.Add("A1", System.Type.GetType("System.String"));
            PosDataTable.Columns.Add("S", System.Type.GetType("System.String"));
            PosDataTable.Columns.Add("终点", System.Type.GetType("System.String"));
            PosDataTable.Columns.Add("B2", System.Type.GetType("System.String"));
            PosDataTable.Columns.Add("L2", System.Type.GetType("System.String"));
            PosDataTable.Columns.Add("A2", System.Type.GetType("System.String"));

            DataRow dr;
            for (int i = 0; i < PosData.Count; i++)
            {
                GeodesicInfo data = PosData[i];
                Pointinfo p1 = data.P1;
                dr = PosDataTable.NewRow();

                dr["起点"] = p1.Name;
                dr["B1"] = GeoPro.DMS2String(p1.B);
                dr["L1"] = GeoPro.DMS2String(p1.L);
                dr["A1"] = GeoPro.DMS2String(data.A12); 
                dr["S"] = data.S.ToString("0.000");
                
                
                PosDataTable.Rows.Add(dr);
      
            }

            dataGridViewPos.DataSource = PosDataTable;
            for (int i = 0; i < dataGridViewPos.ColumnCount; i++)
            {
                dataGridViewPos.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            EllipsoidA.Text = "椭球长半轴（a）：" + MyEllipsoid.a;
            Ellipsoidf_.Text = "椭球扁率（f）：" + MyEllipsoid.f;
        }
        private void UpdatePosTable()
        {
            DataRow dr;
            for (int i = 0; i < PosData.Count; i++)
            {
                GeodesicInfo data = PosData[i];
                
                dr = PosDataTable.Rows[i];
                Pointinfo p2 = data.P2;
                dr["终点"] = p2.Name;
                dr["B2"] = GeoPro.DMS2String(p2.B);
                dr["L2"] = GeoPro.DMS2String(p2.L);
                dr["A2"] = GeoPro.DMS2String(data.A21);   
            }
        }
   

        private void NegData2Table()
        {
            NegDataTable = new DataTable();
            NegDataTable.Columns.Add("起点", System.Type.GetType("System.String"));
            NegDataTable.Columns.Add("B1", System.Type.GetType("System.String"));
            NegDataTable.Columns.Add("L1", System.Type.GetType("System.String"));
            NegDataTable.Columns.Add("终点", System.Type.GetType("System.String"));
            NegDataTable.Columns.Add("B2", System.Type.GetType("System.String"));
            NegDataTable.Columns.Add("L2", System.Type.GetType("System.String"));
            NegDataTable.Columns.Add("A1", System.Type.GetType("System.String"));
            NegDataTable.Columns.Add("A2", System.Type.GetType("System.String"));
            NegDataTable.Columns.Add("S", System.Type.GetType("System.String"));

            DataRow dr;
            for (int i = 0; i < NegData.Count; i++)
            {
                GeodesicInfo data = NegData[i];
                Pointinfo p1 = data.P1;
                Pointinfo p2 = data.P2;
                dr = NegDataTable.NewRow();

                dr["起点"] = p1.Name;
                dr["B1"] = GeoPro.DMS2String(p1.B);
                dr["L1"] = GeoPro.DMS2String(p1.L);
                dr["终点"] = p2.Name;
                dr["B2"] = GeoPro.DMS2String(p2.B);
                dr["L2"] = GeoPro.DMS2String(p2.L);


                NegDataTable.Rows.Add(dr);

            }

            EllipsoidA.Text = "椭球长半轴（a）：" + MyEllipsoid.a;
            Ellipsoidf_.Text = "椭球扁率（f）：" + MyEllipsoid.f;

            dataGridViewNeg.DataSource = NegDataTable;
            for (int i = 0; i < dataGridViewNeg.ColumnCount; i++)
            {
                dataGridViewNeg.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;

            }

        }
        private void UpdateNegTable()
        {
            DataRow dr;
            for (int i = 0; i < NegData.Count; i++)
            {
                GeodesicInfo data = NegData[i];
                dr = NegDataTable.Rows[i];
                dr["A1"] = GeoPro.DMS2String(data.A12);
                dr["A2"] = GeoPro.DMS2String(data.A21);
                dr["S"] = data.S.ToString("0.000");
            }
        }


        private void ImportPosData(object sender, EventArgs e)
        {
            try
            {    
                openFileDialog.Filter = "数据(*.txt)|*.txt";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    NegData= FileHelper.ReadNegData(openFileDialog.FileName, ref MyEllipsoid);
                    NegData2Table();
                   // MessageBox.Show("导入成功！");
                    MyNegImage = MyDrawPro.GetImage(NegData);
                    pictureBox.BorderStyle = BorderStyle.FixedSingle;
                    pictureBox.Image = MyNegImage;
                                      
                    ViewNegData_Click(sender,e);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("打开失败！");
            }
        }

        private void ViewPosData_Click(object sender, EventArgs e)
        {
            if (PosData == null)
            {
                MessageBox.Show("没有数据！");
                return;
            }

            tabControl.SelectTab(0);
            
            dataGridViewPos.Visible = true;
            dataGridViewNeg.Visible = false;

            caltype.Text = "数据类型：正算数据";
        }

        private void ViewNegData_Click(object sender, EventArgs e)
        {
            if (NegData == null)
            {
                MessageBox.Show("没有数据！");
                return;
            }

            tabControl.SelectTab(0);

            
            dataGridViewPos.Visible = false;
            dataGridViewNeg.Visible = true;
            caltype.Text = "数据类型：反算数据";
            
        }

        private void ViewImage_Click(object sender, EventArgs e)
        {
            if (MyNegImage == null)
            {
                MessageBox.Show("没有数据！");
                return;
            }
            tabControl.SelectTab(1);
            pictureBox.Image = MyNegImage;
        }

        private void ViewReport_Click(object sender, EventArgs e)
        {
            if (PosReport == null)
            {
                MessageBox.Show("没有数据！");
                return;
            }
            tabControl.SelectTab(2);
            richTextBox.Text = PosReport.ToString();
        }

        private void ImageZoomIn_Click(object sender, EventArgs e)
        {
            if (MyNegImage == null)
            {
                MessageBox.Show("没有数据！");
                return;
            }
            tabControl.SelectTab(1);
            ToolZoomIn_Click(sender, e);
        }

        private void ImageZoomOut_Click(object sender, EventArgs e)
        {
            if (MyNegImage == null)
            {
                MessageBox.Show("没有数据！");
                return;
            }
            tabControl.SelectTab(1);
            ToolZoomOut_Click(sender, e);
        }

        private void ToolZoomIn_Click(object sender, EventArgs e)
        {
            if (MyNegImage == null)
            {
                MessageBox.Show("没有数据！");
                return;
            }

            pictureBox.Width = Convert.ToInt16(pictureBox.Width * 1.2);
            pictureBox.Height = Convert.ToInt16(pictureBox.Height * 1.2);
        }

        private void ToolZoomOut_Click(object sender, EventArgs e)
        {
            if (MyNegImage == null)
            {
                MessageBox.Show("没有数据！");
                return;
            }
            pictureBox.Width = Convert.ToInt16(pictureBox.Width / 1.2);
            pictureBox.Height = Convert.ToInt16(pictureBox.Height / 1.2);
        }

        private void VersionInfo_Click(object sender, EventArgs e)
        {
            string copyright = "《测绘程序设计试题集（试题10 大地主题计算）》配套程序\n作者：李英冰，赵望宇，辛绍铭，李萌\n";
            copyright += "武汉大学测绘学院\r\nEMAIL: ybli@sgg.whu.edu.cn\r\n2017.7.30";
            MessageBox.Show(copyright);
        }

        private void ToolSave_Click(object sender, EventArgs e)
        {
            int index = tabControl.SelectedIndex;
            switch (index)
            { 
                case 0:
                    SaveNegData_Click(sender, e);
                    break;
                case 1:
                    SaveImage_Click(sender, e);
                    break;
                case 2:
                    SaveReport_Click(sender, e);
                    break;
            }
        }

        private void PosComputation_Click(object sender, EventArgs e)
        {
            if (PosData == null)
            {
                MessageBox.Show("没有正算数据");
                return;
            }
            ViewPosData_Click(sender, e);

            try
            {
                //计算
                DirectPro = new BesselDirect(MyEllipsoid);
                DirectPro.DirecPro(PosData);
                //MessageBox.Show("计算成功！");
                UpdatePosTable();
                PosReport = FileHelper.GetReport(MyEllipsoid, PosData,1);
                //MessageBox.Show("已生成计算报告！");
                pictureBox.BorderStyle = BorderStyle.FixedSingle;
                MyPosImage = MyDrawPro.GetImage(PosData);
                pictureBox.Image = MyPosImage;
                //MessageBox.Show("已绘制图形");
                richTextBox.Text = PosReport.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("计算失败！");
            }
           
            
        }

        private void NegComputation_Click(object sender, EventArgs e)
        {
            if (NegData == null)
            {
                MessageBox.Show("没有反算数据");
                return;
            }
            ViewNegData_Click(sender,e);
            try
            {
                //计算
                InversePro = new BesselInverse(MyEllipsoid);
                InversePro.InversePro(NegData);

                MessageBox.Show("计算成功！");
                UpdateNegTable();
              
                NegReport = FileHelper.GetReport(MyEllipsoid, NegData,2);
                MessageBox.Show("已生成计算报告！");
                richTextBox.Text = NegReport.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("计算失败！");
            }
        }

        private void ToolOpen_Click(object sender, EventArgs e)
        {
            //ImportPosData(sender, e);
            OpenPosData_Click(sender, e);
        }

        private void SaveImage_Click(object sender, EventArgs e)
        {
            try
            {
                saveFileDialog.Filter = "图片(*.bmp)|*.bmp";
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    MyNegImage.Save(saveFileDialog.FileName);
                    MessageBox.Show("保存成功！");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("保存失败！");
            }
        }

        private void SavePosData_Click(object sender, EventArgs e)
        {
            if (NegDataTable == null)
            {
                MessageBox.Show("没有正算数据");
                return;
            }

            try
            {
                saveFileDialog.Filter = "数据表格(*.xlsx)|*.xlsx";
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    FileHelper.SaveDataTable(saveFileDialog.FileName,PosDataTable);
                    MessageBox.Show("保存成功！");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("保存失败！");
            }
        }

        private void SaveNegData_Click(object sender, EventArgs e)
        {
            if (NegDataTable == null)
            {
                MessageBox.Show("没有反算数据");
                return;
            }

            try
            {
                saveFileDialog.Filter = "数据表格(*.xlsx)|*.xlsx";
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    FileHelper.SaveDataTable(saveFileDialog.FileName, NegDataTable);
                    MessageBox.Show("保存成功！");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("保存失败！");
            }
        }

        private void SaveReport_Click(object sender, EventArgs e)
        {
            try
            {
                saveFileDialog.Filter = "报告(*.txt)|*.txt";
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    FileHelper.SaveReport(saveFileDialog.FileName,NegReport);
                    MessageBox.Show("保存成功！");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("保存失败！");
            }
        }

        private void Exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pictureBox_MouseDown(object sender, MouseEventArgs e)
        {

            iflag = true;                                                    //标识，鼠标按下
            ix = e.X;                                                    //记录鼠标的X坐标
            iy = e.Y;
        }

        private void pictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (iflag)
            {
                //设置pictureBox1控件的位置
                pictureBox.Left = pictureBox.Left + (e.X - ix);
                pictureBox.Top = pictureBox.Top + (e.Y - iy);
            }
        }

        private void pictureBox_MouseUp(object sender, MouseEventArgs e)
        {
            iflag = false;
        }
      
        private void OpenPosData_Click(object sender, EventArgs e)
        {
            try
            {
                openFileDialog.Filter = "数据(*.txt)|*.txt";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    PosData = FileHelper.ReadPosData(openFileDialog.FileName, ref MyEllipsoid);
                    //MessageBox.Show("打开成功!");
                    PosData2Table();
                    ViewPosData_Click(sender, e);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("打开失败！");
            }
        }

        private void OpenNegData_Click(object sender, EventArgs e)
        {
            try
            {
                openFileDialog.Filter = "数据(*.txt)|*.txt";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    NegData = FileHelper.ReadNegData(openFileDialog.FileName, ref MyEllipsoid);
                   // MessageBox.Show("打开成功!");
                    NegData2Table();
                    MyNegImage = MyDrawPro.GetImage(NegData);
                    pictureBox.BorderStyle = BorderStyle.FixedSingle;
                    pictureBox.Image = MyNegImage;
                    MessageBox.Show("已绘制图形");
                    ViewNegData_Click(sender, e);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("打开失败！");
            }
        }

        private void ViewPosImage_Click(object sender, EventArgs e)
        {
            if (MyPosImage == null)
            {
                MessageBox.Show("没有数据！");
                return;
            }
            tabControl.SelectTab(1);
            pictureBox.Image = MyPosImage;
        }

        private void toolPosComputation_Click(object sender, EventArgs e)
        {
            PosComputation_Click(sender, e);
        }

        private void ToolInvComputation_Click(object sender, EventArgs e)
        {
            NegComputation_Click(sender, e);
        }

        private void toolHelp_Click(object sender, EventArgs e)
        {
            VersionInfo_Click(sender, e);
        }

        private void ViewNegReport_Click(object sender, EventArgs e)
        {
            if (NegReport == null)
            {
                MessageBox.Show("没有数据！");
                return;
            }
            tabControl.SelectTab(2);
            richTextBox.Text = NegReport.ToString();
        }

     

   

     
    }
}
