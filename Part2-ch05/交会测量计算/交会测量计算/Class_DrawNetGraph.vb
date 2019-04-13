Imports System.Math
Imports System.Drawing.Drawing2D
Friend Class DrawNetGraph
    Dim Graphy As Graphics
    Dim Pen1 As Pen = New Pen(Color.Navy, 1)
    Dim Pen2 As Pen = New Pen(Color.Red, 2)
    Dim Pen3 As Pen = New Pen(Color.LimeGreen, 2)

    Friend Sub DrawContrNetwork(ByRef DrawBoard As Control, ByRef PtArray() As ContrPoint, ByRef SideDic As Dictionary(Of String, Side))
        Dim Xmin#, Xmax#, Ymin#, Ymax#
        Dim Xo#, Yo#, kmin!
        Dim MarkPath1 As New GraphicsPath
        Dim MarkPath2 As New GraphicsPath
        Graphy = DrawBoard.CreateGraphics

        Dim DrawFont As New Font("Times New Roman", 24, FontStyle.Bold, GraphicsUnit.Pixel)
        Dim DrawPtLen As Integer = PtArray.Length
        Dim XCmpare(DrawPtLen - 1) As Double, YCmpare(DrawPtLen - 1) As Double

        '规划屏幕坐标系===================================================================================
        For i = 0 To DrawPtLen - 1
            XCmpare(i) = PtArray(i).CoorX
            YCmpare(i) = PtArray(i).CoorY
        Next
        Array.Sort(XCmpare) : Array.Sort(YCmpare)
        Xmin = XCmpare(0) : Xmax = XCmpare(XCmpare.Length - 1)
        Ymin = YCmpare(0) : Ymax = YCmpare(YCmpare.Length - 1)
        '按X、Y的极值，确定图形在屏幕中的中心坐标
        Xo = (Xmax + Xmin) / 2 : Yo = (Ymax + Ymin) / 2
        '确定实际坐标值与屏幕像素坐标值之间的缩放比例因子，取X与Y中较小的一个，以适应两者。同比例以保持图形不变形。
        kmin = 0.9 * Min(DrawBoard.Height / (Xmax - Xmin), DrawBoard.Width / (Ymax - Ymin))

        '坐标转换,实际坐标转换为屏幕坐标==================================================================
        For i = 0 To DrawPtLen - 1
            Dim ScrX!, ScrY!
            ScrX = (PtArray(i).CoorX - Xo) * kmin
            ScrY = (PtArray(i).CoorY - Yo) * kmin

            PtArray(i).ScrTop = DrawBoard.Height / 2 - ScrX
            PtArray(i).ScrLft = DrawBoard.Width / 2 + ScrY
        Next

        '展绘控制点并注记==================================================================================
        For i = 0 To DrawPtLen - 1
            Dim CirMarkCent As Point = New Point(PtArray(i).ScrLft, PtArray(i).ScrTop)
            Dim R! = 2
            Dim rR! = 8 * R / Sqrt(3)
            If PtArray(i).Type = False Then
                '符号路径添加未知点位，圆点
                MarkPath1.AddEllipse(CirMarkCent.X - R / 2, CirMarkCent.Y - R / 2, R, R)
                '符号路径添加未知点圆圈标志
                MarkPath1.AddEllipse(CInt(CirMarkCent.X - R * 3), CInt(CirMarkCent.Y - R * 3), R * 6, R * 6)
            Else
                Dim TriMark(2) As Point
                '符号路径添加已知点位，圆点
                MarkPath2.AddEllipse(CirMarkCent.X - R / 2, CirMarkCent.Y - R / 2, R, R)
                '符号路径添加已知点三角标志
                TriMark(0) = New Point(rR * Cos(5 / 6 * PI) + CirMarkCent.X, rR * Sin(5 / 6 * PI) + CirMarkCent.Y)
                TriMark(1) = New Point(rR * Cos(3 / 2 * PI) + CirMarkCent.X, rR * Sin(3 / 2 * PI) + CirMarkCent.Y)
                TriMark(2) = New Point(rR * Cos(1 / 6 * PI) + CirMarkCent.X, rR * Sin(1 / 6 * PI) + CirMarkCent.Y)
                MarkPath2.AddPolygon(TriMark)
            End If
            '注记点号
            If PtArray(i).Type = 1 Then
                Graphy.DrawString(PtArray(i).Name, DrawFont, Brushes.Red, New Point(PtArray(i).ScrLft + 15, PtArray(i).ScrTop - 10))
            Else
                Graphy.DrawString(PtArray(i).Name, DrawFont, Brushes.Blue, New Point(PtArray(i).ScrLft + 15, PtArray(i).ScrTop - 10))
            End If
        Next
        '按添加路径绘未知点位符号
        Graphy.DrawPath(Pen3, MarkPath1)
        '按添加路径绘已知点位符号
        Graphy.DrawPath(Pen2, MarkPath2)

        '展绘控制边=======================================================================================
        For Each SdItem As KeyValuePair(Of String, Side) In SideDic
            Call Draw2PtLine(SdItem.Value)
        Next
        Graphy.Dispose()
    End Sub

    '画线段子过程
    Public Sub Draw2PtLine(ByRef SideItm As Side)
        Dim PtA As New Point(SideItm.PtS.ScrLft, SideItm.PtS.ScrTop)
        Dim PtB As New Point(SideItm.PtE.ScrLft, SideItm.PtE.ScrTop)
        If SideItm.PtS.Type And SideItm.PtE.Type Then '已知边画双线
            Dim PtC As Point
            Dim PtD As Point
            Dim K! = 1.2
            Dim AzimRot90 As Double
            For i = 0 To 1
                AzimRot90 = Azimuth(PtA, PtB) + (2 * i - 1) * PI / 2 '双线一端两点方向与直线方向成90度
                '计算双线两端点坐标，循环画出两条线
                PtC = New Point(PtA.X + K * Cos(AzimRot90), PtA.Y + K * Sin(AzimRot90))
                PtD = New Point(PtB.X + K * Cos(AzimRot90), PtB.Y + K * Sin(AzimRot90))
                Graphy.DrawLine(Pens.Red, PtC, PtD)
            Next
        Else                                           '已知边画单线
            Graphy.DrawLine(Pen1, PtA, PtB)
        End If
    End Sub

End Class
