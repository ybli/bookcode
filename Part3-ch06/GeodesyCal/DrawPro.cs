using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;


namespace GeodesyCal
{
    public class DrawPro
    {
        List<GeodesicInfo> negdata;
        double maxB, minB, maxL, minL;
        Bitmap bmp;
        Graphics g;
        private int zeroX = 250, zeroY = 1750;

        public Bitmap GetImage(List<GeodesicInfo> data)
        {
            bmp = new Bitmap(2000, 2000);
            g = Graphics.FromImage(bmp);
            this.negdata = data;
            FindMBR();
            DrawGrid();
            //zeroX = 250;
            //zeroY = 1750;
            for (int i = 0; i < data.Count; i++)
            {
                DrawPairPoint(data[i]);
            }
            g.Dispose();
            return bmp;
        }

        private void FindMBR()
        {
            maxB=minB = negdata[0].P1.B;
            maxL=minL = negdata[0].P1.L;
            for (int i = 0; i < negdata.Count; i++)
            {
                Pointinfo p1 = negdata[i].P1;
                Pointinfo p2 = negdata[i].P2;

                //p1
                if (p1.B > maxB)
                {
                    maxB = p1.B;
                }
                else if (p1.B < minB)
                {
                    minB = p1.B;
                }

                if (p1.L > maxL)
                {
                    maxL = p1.L;
                }
                else if (p1.L < minL)
                {
                    minL = p1.L;
                }

                //p2
                if (p2.B > maxB)
                {
                    maxB = p2.B;
                }
                else if (p2.B < minB)
                {
                    minB = p2.B;
                }
                if (p2.L > maxL)
                {
                    maxL = p2.L;
                }
                else if (p2.L < minL)
                {
                    minL = p2.L;
                }

            }

            
        }

        private void DrawPairPoint(GeodesicInfo pair)
        {
            zeroX = zeroX + 250;
            zeroY = zeroY - 250;

            double scaleX, scaleY;
            scaleX = 900 / (maxB - minB);
            scaleY = 900 / (maxL - minL);

            int x,y;
            x = (int)(scaleX * (pair.P1.B - minB));
            y = (int)(scaleY * (pair.P1.L - minL));
            Point p1 = new Point(zeroX+x,zeroY- y);

            x = (int)(scaleX * (pair.P2.B - minB));
            y = (int)(scaleY * (pair.P2.L - minL));
            Point p2 = new Point(zeroX + x, zeroY - y);

            Pen pen = new Pen(Color.Red, 3f);

            Brush brush1 = new SolidBrush(Color.Black);
            Brush brush2 = new SolidBrush(Color.Blue);

            //画线
            g.DrawLine(pen, p1, p2);

            //画点
            g.FillEllipse(brush2, p1.X-5f, p1.Y-5f, 15f, 15f);
            g.FillEllipse(brush2, p2.X-5f, p2.Y-5f, 15f, 15f);

            //点名字
            Font font= new Font("黑体",14,FontStyle.Bold,GraphicsUnit.Millimeter);
            g.DrawString(pair.P1.Name, font, brush1, p1.X + 5f, p1.Y + 25f);
            g.DrawString(pair.P2.Name, font, brush1, p2.X + 5f, p2.Y + 25f);
        }

        private void DrawGrid()
        {
            Pen pen = new Pen(Color.SeaGreen, 2f);

            pen.DashStyle = System.Drawing.Drawing2D.DashStyle.DashDot;
          
            //竖线
            for (int i = 0; i < 16; i++)
            {
                Point p1 = new Point(zeroX + (i * 100), zeroY);
                Point p2 = new Point(zeroX + (i * 100), zeroY - 1500);
                g.DrawLine(pen, p1, p2);
            }

            //横线
            for (int i = 0; i < 16; i++)
            {
                Point p1 = new Point(zeroX, zeroY - i * 100);
                Point p2 = new Point(zeroX + 1500, zeroY - i * 100);
                g.DrawLine(pen, p1, p2);
            }

        }
    
    
    }
}
