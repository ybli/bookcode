Imports System.IO
Imports System.Math
Imports System.Windows.Forms.DataVisualization.Charting
Public Structure RoadPoint
    Dim Name As String   '点名
    Dim Lab As String    '标签（起始点、直线点、缓和曲线点、圆曲线点、终点）
    Dim X As Double      'x坐标
    Dim Y As Double      'y坐标
    Dim LC As Double     '里程
    Dim R As Double      '圆曲线半径
    Dim Ls As Double     '缓曲线长
End Structure

Module Road_Module
    Public DatTab As New DataTable
    Public DataPt() As RoadPoint   '保存已知点的数据
    Public RoadPt() As RoadPoint
    Public Ptnum As Integer      '点个数
    Public StakeSpac As Integer       '里程间隔
    Public YCurveCount, HCurveCount As Integer '圆曲线段和缓和曲线段计数
    Dim Road() As Road_Element

    '------------公共过程--------------
    '导入数据
    Sub DataInput(ByVal DGV As DataGridView)
        ReDim DataPt(0) : ReDim Road(0)
        Dim OFD As New OpenFileDialog
        OFD.Filter = "TXT文本文件(*.txt)|*.txt"
        OFD.FilterIndex = 0
        If OFD.ShowDialog = DialogResult.OK Then
            Dim StreamRd As New StreamReader(OFD.FileName)
            Dim DataStr As String
            Dim DataStrArr() As String
            Dim check As String
            DataStr = StreamRd.ReadToEnd
            DataStr = Replace(DataStr, vbCrLf, ",")
            DataStr = StrReverse(DataStr)
            Do
                check = Mid(DataStr, 1, 1)
                If check = "," Then
                    DataStr = Mid(DataStr, 2, Len(DataStr) - 1)
                End If
            Loop Until check <> ","
            DataStr = StrReverse(DataStr)
            DataStrArr = DataStr.Split(",")
            DatTab.Columns.Clear()
            DatTab.Rows.Clear()
            For i = 0 To 4
                DatTab.Columns.Add()
            Next
            DatTab.Columns(0).ColumnName = "点名"
            DatTab.Columns(1).ColumnName = "X坐标"
            DatTab.Columns(2).ColumnName = "Y坐标"
            DatTab.Columns(3).ColumnName = "标签"
            DatTab.Columns(4).ColumnName = "里程"

            If Not ((DataStrArr.Length - 6) Mod 5 = 0) Then
                MsgBox("道路数据不匹配，请重新组织！")
                End
            Else
                Ptnum = Int(DataStrArr.Length - 6) / 5 + 2
            End If

            ReDim DataPt(Ptnum - 1)

            For i = 0 To Ptnum - 1
                DatTab.Rows.Add()
            Next

            For i = 0 To Ptnum - 1
                If i = 0 Then
                    DataPt(i).Name = DataStrArr(0)
                    DataPt(i).X = DataStrArr(1)
                    DataPt(i).Y = DataStrArr(2)
                    DataPt(i).Lab = "StaPt"
                ElseIf i = Ptnum - 1 Then
                    DataPt(i).Name = DataStrArr(5 * Ptnum - 7)
                    DataPt(i).X = DataStrArr(5 * Ptnum - 6)
                    DataPt(i).Y = DataStrArr(5 * Ptnum - 5)
                    DataPt(i).Lab = "EndPt"
                Else
                    DataPt(i).Name = DataStrArr(5 * i - 2)
                        DataPt(i).X = DataStrArr(5 * i - 1)
                        DataPt(i).Y = DataStrArr(5 * i + 0)
                        DataPt(i).R = DataStrArr(5 * i + 1)
                        DataPt(i).Ls = DataStrArr(5 * i + 2)
                        If DataPt(i).R <> 0 Then
                            If DataPt(i).Ls <> 0 Then
                                HCurveCount += 1
                            Else
                                YCurveCount += 1
                            End If
                        End If
                    End If

                    DatTab(i)(0) = DataPt(i).Name
                    DatTab(i)(1) = DataPt(i).X
                    DatTab(i)(2) = DataPt(i).Y
                    DatTab(i)(3) = DataPt(i).Lab
                Next
                DGV.DataSource = DatTab
                MsgBox("导入数据完成！", MsgBoxStyle.Information, "提示")
            End If
    End Sub
    '导出报告
    Sub DataOutput(ByVal TextBx As TextBox)
        Dim SFD As New SaveFileDialog
        SFD.Filter = "TXT文本文件(*.txt)|*.txt"
        SFD.FilterIndex = 0
        If SFD.ShowDialog = DialogResult.OK Then
            FileOpen(1, SFD.FileName, OpenMode.Output)
            Print(1, TextBx.Text)
            FileClose(1)
            MsgBox("保存报告完成！", MsgBoxStyle.Information, "提示")
        End If
    End Sub
    '保存图片
    Sub SavePic(ByVal RoadChart As Chart)
        Dim SFD As New SaveFileDialog
        SFD.Filter = "BMP格式(*.bmp)|*.bmp"
        If SFD.ShowDialog = DialogResult.OK Then
            RoadChart.SaveImage(SFD.FileName, ChartImageFormat.Bmp)
            MsgBox("图片保存完成！", MsgBoxStyle.Information, "提示")
        End If
    End Sub

    '道路曲线计算
    Sub RoadCalculate()
        ReDim Road(Ptnum - 3)
        DataPt(0).LC = 0
        For i = 1 To Ptnum - 2
            If i = 1 Then
                Road(i - 1) = New Road_Element(DataPt(i - 1), DataPt(i), DataPt(i + 1), StakeSpac)
            Else
                Road(i - 1) = New Road_Element(Road(i - 2).RoadEnd, DataPt(i), DataPt(i + 1), StakeSpac)
            End If
        Next
        Dim M As Integer = 0
        For i% = 0 To Ptnum - 3
            Dim J% = Road(i).MileStake.Count
            ReDim Preserve RoadPt(M + J - 1)
            Road(i).MileStake.CopyTo(RoadPt, M)
            M = RoadPt.Count
        Next

    End Sub

    '绘图
    Sub Draw(ByVal RoadChart As Chart)
        Dim x(), y() As Double
        ReDim x(Ptnum - 1) : ReDim y(Ptnum - 1)
        For i% = 0 To Ptnum - 1
            x(i) = DataPt(i).Y
            y(i) = DataPt(i).X
        Next

        Dim k% = RoadPt.Count - 1
        For i% = 0 To k
            ReDim Preserve x(Ptnum + k) : ReDim Preserve y(Ptnum + k)
            x(Ptnum + i) = RoadPt(i).Y
            y(Ptnum + i) = RoadPt(i).X
        Next

        Dim xmax, xmin, ymax, ymin As Integer
        xmax = (x.Max \ 10 + 1) * 10 : xmin = (x.Min \ 10) * 10
        ymax = (y.Max \ 10 + 1) * 10 : ymin = (y.Min \ 10) * 10

        RoadChart.ChartAreas.Clear()
        RoadChart.Series.Clear()
        RoadChart.Titles.Clear()
        RoadChart.Height = 500
        RoadChart.Width = 500
        Dim ChartTitle As New Title("道路曲线示意图")
        ChartTitle.Font = New Font("黑体", 12)
        RoadChart.Titles.Add(ChartTitle)
        Dim RoadArea As New ChartArea
        RoadArea.AxisX.MajorGrid.Enabled = False
        RoadArea.AxisY.MajorGrid.Enabled = False
        RoadArea.AxisX.Title = "Y坐标(m)"
        RoadArea.AxisY.Title = "X坐标(m)"
        RoadArea.AxisX.Minimum = xmin
        RoadArea.AxisX.Maximum = xmax
        RoadArea.AxisY.Minimum = ymin
        RoadArea.AxisY.Maximum = ymax
        RoadArea.Position.Height = 100
        RoadArea.Position.Width = 100
        RoadArea.AxisX.Interval = 10
        RoadArea.AxisY.Interval = 10
        RoadArea.InnerPlotPosition.Auto = False
        Dim XX, YY As Integer
        XX = xmax - xmin : YY = ymax - ymin

        RoadArea.InnerPlotPosition.FromRectangleF(New Rectangle(10, 10, XX \ 8, YY \ 8))
        RoadChart.ChartAreas.Add(RoadArea)

        Dim series1 As New Series
        series1.ChartType = SeriesChartType.Point
        series1.MarkerStyle = MarkerStyle.Circle
        series1.MarkerColor = Color.Blue
        series1.IsVisibleInLegend = False
        For i = 0 To Ptnum - 1
            series1.Points.AddXY(DataPt(i).Y, DataPt(i).X)
        Next
        RoadChart.Series.Add(series1)

        Dim series2 As New Series
        series2.ChartType = SeriesChartType.Spline
        series2.Color = Color.Orange
        series2.BorderWidth = 3
        series2.MarkerStyle = MarkerStyle.Circle
        series2.MarkerColor = Color.Red
        series2.IsVisibleInLegend = False

        For i% = 0 To RoadPt.Count - 1
            series2.Points.AddXY(RoadPt(i).Y, RoadPt(i).X)
        Next
        RoadChart.Series.Add(series2)

        Dim series3 As New Series
        series3.ChartType = SeriesChartType.Point
        series3.MarkerStyle = MarkerStyle.Circle
        series3.BorderWidth = 10
        series3.MarkerColor = Color.Green
        series3.IsVisibleInLegend = False
        For i = 0 To Ptnum - 3
            If Road(i).JD.Ls = 0 Then
                series3.Points.AddXY(Road(i).ZH.Y, Road(i).ZH.X)
                series3.Points.AddXY(Road(i).QZ.Y, Road(i).QZ.X)
                series3.Points.AddXY(Road(i).HZ.Y, Road(i).HZ.X)
            Else
                series3.Points.AddXY(Road(i).ZH.Y, Road(i).ZH.X)
                series3.Points.AddXY(Road(i).HY.Y, Road(i).HY.X)
                series3.Points.AddXY(Road(i).QZ.Y, Road(i).QZ.X)
                series3.Points.AddXY(Road(i).YH.Y, Road(i).YH.X)
                series3.Points.AddXY(Road(i).HZ.Y, Road(i).HZ.X)
            End If
        Next
        RoadChart.Series.Add(series3)

    End Sub

    '结果输出
    Sub Result(ByVal TxtBx As TextBox)
        TxtBx.Text = vbCrLf & vbCrLf
        TxtBx.Text += Space(10) & New String("=", 75) & vbCrLf
        TxtBx.Text += Space(40) & "道路曲线计算报告" & vbCrLf
        TxtBx.Text += Space(10) & New String("=", 75) & vbCrLf & vbCrLf
        TxtBx.Text += Space(10) & "1.统计信息" & vbCrLf & vbCrLf
        TxtBx.Text += Space(15) & String.Format("道路总长：   {0:f3}  m", Road(Ptnum - 3).RoadEnd.LC) & vbCrLf
        TxtBx.Text += Space(15) & String.Format("圆曲线数目： {0}", YCurveCount) & vbCrLf
        TxtBx.Text += Space(15) & String.Format("缓曲线数目： {0}", HCurveCount) & vbCrLf & vbCrLf
        TxtBx.Text += Space(10) & "2.曲线数据" & vbCrLf & vbCrLf
        For i = 0 To Road.Count - 1
            If Road(i).Lab = "YQX" Then
                Dim CurvName$ = "YQX-" & Road(i).JD.Name
                TxtBx.Text += Space(12) & String.Format("曲线名： YQX-{0}", Road(i).JD.Name) & vbCrLf & vbCrLf
                TxtBx.Text += Space(15) & String.Format("转向角： {0}", D_MM_SS(Road(i).TurnAng)) & vbCrLf
                TxtBx.Text += Space(15) & String.Format("半  径： {0} m", Road(i).CircRadius) & vbCrLf
                TxtBx.Text += Space(15) & String.Format("桩间距： {0} m", StakeSpac) & vbCrLf
                TxtBx.Text += Space(15) & String.Format("切线长： {0:f3} m", Road(i).Tangent) & vbCrLf
                TxtBx.Text += Space(15) & String.Format("曲线长： {0:f3} m", Road(i).CurvLeng) & vbCrLf
                TxtBx.Text += Space(15) & String.Format("外矢距： {0:f3} m", Road(i).OutVectDis) & vbCrLf
                TxtBx.Text += Space(15) & String.Format("切曲差： {0:f3} m", Road(i).TangCurvDiffer) & vbCrLf & vbCrLf

                TxtBx.Text += Space(12) & "圆曲线" & CurvName & "主点里程桩号" & vbCrLf & vbCrLf
                TxtBx.Text += Space(15) & "直圆点 ZY :" & LCZH(Road(i).ZH.LC) & vbCrLf
                TxtBx.Text += Space(15) & "曲中点 QZ :" & LCZH(Road(i).QZ.LC) & vbCrLf
                TxtBx.Text += Space(15) & "圆直点 YZ :" & LCZH(Road(i).HZ.LC) & vbCrLf
                TxtBx.Text += Space(15) & "交  点 JD :" & LCZH(Road(i).JD.LC) & vbCrLf & vbCrLf

                TxtBx.Text += Space(12) & "圆曲线" & CurvName & "主点坐标" & vbCrLf & vbCrLf
                TxtBx.Text += Space(15) & String.Format("{0,-10}{1,-13}{2,-15}", "桩号", "X坐标", "Y坐标") & vbCrLf
                TxtBx.Text += Space(15) & String.Format("{0,-12}{1,-15:f3}{2,-15:f3}", Road(i).ZH.Name, Road(i).ZH.X, Road(i).ZH.Y) & vbCrLf
                TxtBx.Text += Space(15) & String.Format("{0,-12}{1,-15:f3}{2,-15:f3}", Road(i).QZ.Name, Road(i).QZ.X, Road(i).QZ.Y) & vbCrLf
                TxtBx.Text += Space(15) & String.Format("{0,-12}{1,-15:f3}{2,-15:f3}", Road(i).HZ.Name, Road(i).HZ.X, Road(i).HZ.Y) & vbCrLf
                TxtBx.Text += Space(15) & String.Format("{0,-12}{1,-15:f3}{2,-15:f3}", Road(i).JD.Name, Road(i).JD.X, Road(i).JD.Y) & vbCrLf & vbCrLf
            Else
                Dim CurvName$ = "HQX-" & Road(i).JD.Name
                TxtBx.Text += Space(12) & String.Format("曲线名： HQX-{0}", Road(i).JD.Name) & vbCrLf & vbCrLf
                TxtBx.Text += Space(15) & String.Format("转向角： {0}", D_MM_SS(Road(i).TurnAng)) & vbCrLf
                TxtBx.Text += Space(15) & String.Format("半  径： {0} m", Road(i).CircRadius) & vbCrLf
                TxtBx.Text += Space(15) & String.Format("桩间距： {0} m", StakeSpac) & vbCrLf
                TxtBx.Text += Space(15) & String.Format("切线长： {0:f3} m", Road(i).Tangent) & vbCrLf
                TxtBx.Text += Space(15) & String.Format("曲线长： {0:f3} m", Road(i).CurvLeng) & vbCrLf
                TxtBx.Text += Space(15) & String.Format("外矢距： {0:f3} m", Road(i).OutVectDis) & vbCrLf
                TxtBx.Text += Space(15) & String.Format("切曲差： {0:f3} m", Road(i).TangCurvDiffer) & vbCrLf
                TxtBx.Text += Space(15) & String.Format("缓外角： {0}", D_MM_SS(Road(i).OutAlleviAng)) & vbCrLf
                TxtBx.Text += Space(15) & String.Format("切垂距： {0:f3} m", Road(i).TangVertiDis) & vbCrLf
                TxtBx.Text += Space(15) & String.Format("内移量： {0:f3} m", Road(i).InterMovi) & vbCrLf & vbCrLf

                TxtBx.Text += Space(12) & "缓曲线" & CurvName & "主点里程桩号" & vbCrLf & vbCrLf
                TxtBx.Text += Space(15) & "直缓点 ZH :" & LCZH(Road(i).ZH.LC) & vbCrLf
                TxtBx.Text += Space(15) & "缓圆点 HY :" & LCZH(Road(i).HY.LC) & vbCrLf
                TxtBx.Text += Space(15) & "曲中点 QZ :" & LCZH(Road(i).QZ.LC) & vbCrLf
                TxtBx.Text += Space(15) & "圆缓点 YH :" & LCZH(Road(i).YH.LC) & vbCrLf
                TxtBx.Text += Space(15) & "缓直点 HZ :" & LCZH(Road(i).HZ.LC) & vbCrLf
                TxtBx.Text += Space(15) & "交  点 JD :" & LCZH(Road(i).JD.LC) & vbCrLf & vbCrLf

                TxtBx.Text += Space(12) & "缓曲线" & CurvName & "主点坐标" & vbCrLf & vbCrLf
                TxtBx.Text += Space(15) & String.Format("{0,-10}{1,-13}{2,-15}", "桩号", "X坐标", "Y坐标") & vbCrLf
                TxtBx.Text += Space(15) & String.Format("{0,-12}{1,-15:f3}{2,-15:f3}", Road(i).ZH.Name, Road(i).ZH.X, Road(i).ZH.Y) & vbCrLf
                TxtBx.Text += Space(15) & String.Format("{0,-12}{1,-15:f3}{2,-15:f3}", Road(i).HY.Name, Road(i).HY.X, Road(i).HY.Y) & vbCrLf
                TxtBx.Text += Space(15) & String.Format("{0,-12}{1,-15:f3}{2,-15:f3}", Road(i).QZ.Name, Road(i).QZ.X, Road(i).QZ.Y) & vbCrLf
                TxtBx.Text += Space(15) & String.Format("{0,-12}{1,-15:f3}{2,-15:f3}", Road(i).YH.Name, Road(i).YH.X, Road(i).YH.Y) & vbCrLf
                TxtBx.Text += Space(15) & String.Format("{0,-12}{1,-15:f3}{2,-15:f3}", Road(i).HZ.Name, Road(i).HZ.X, Road(i).HZ.Y) & vbCrLf
                TxtBx.Text += Space(15) & String.Format("{0,-12}{1,-15:f3}{2,-15:f3}", Road(i).JD.Name, Road(i).JD.X, Road(i).JD.Y) & vbCrLf & vbCrLf
            End If
        Next

        TxtBx.Text += Space(10) & "3.定桩数据：" & vbCrLf & vbCrLf
        TxtBx.Text += Space(15) & String.Format("{0,-10}{1,-13}{2,-13}{3,-13}{4,-15}", "点名", "里程", "X坐标", "Y坐标", "曲线类型") & vbCrLf
        Dim k As Integer = 1
        For i% = 0 To RoadPt.Count - 1
            TxtBx.Text += Space(15) & String.Format("{0,-12}{1,-15}{2,-15:f3}{3,-15:f3}{4,-15}", "Stake" & (k), LCZH(RoadPt(i).LC), RoadPt(i).X, RoadPt(i).Y, RoadPt(i).Lab) & vbCrLf
            k += 1
        Next
    End Sub

    '表格化显示结果数据
    Sub DatTab_Point()
        Dim k% = Ptnum
        For i% = 0 To RoadPt.Count - 1
            DatTab.Rows.Add()
            DatTab(k)(0) = LCZH(RoadPt(i).LC)
            DatTab(k)(1) = String.Format("{0:f3}", RoadPt(i).X)
            DatTab(k)(2) = String.Format("{0:f3}", RoadPt(i).Y)
            DatTab(k)(3) = RoadPt(i).Lab
            DatTab(k)(4) = String.Format("{0:f3}", RoadPt(i).LC)
            k += 1
        Next
    End Sub

    '输出DXF文件
    Sub DXF()
        Dim SFD As New SaveFileDialog
        SFD.Filter = "DXF文件(*.dxf)|*.dxf"
        SFD.FilterIndex = 0
        If SFD.ShowDialog = DialogResult.OK Then
            Dim d As New DXF_Class(SFD.FileName)
            For i% = 1 To RoadPt.Count - 1
                d.DXF_LINE(RoadPt(i - 1), RoadPt(i))
                d.DXF_Point(RoadPt(i - 1))
            Next
            For i = 0 To Road.Count - 1
                d.DXF_TEXT(Road(i).ZH) : d.DXF_Point(Road(i).ZH) : d.DXF_Circle(Road(i).ZH)
                d.DXF_TEXT(Road(i).HY) : d.DXF_Point(Road(i).HY) : d.DXF_Circle(Road(i).HY)
                d.DXF_TEXT(Road(i).QZ) : d.DXF_Point(Road(i).QZ) : d.DXF_Circle(Road(i).QZ)
                d.DXF_TEXT(Road(i).YH) : d.DXF_Point(Road(i).YH) : d.DXF_Circle(Road(i).YH)
                d.DXF_TEXT(Road(i).HZ) : d.DXF_Point(Road(i).HZ) : d.DXF_Circle(Road(i).HZ)
            Next
            d.DXF_ED()
            MsgBox("输出DXF文件完成！", MsgBoxStyle.Information, "警告")
        End If
    End Sub


    '-----------公共函数--------------
    'SEL为0时，为ZH-QZ段，SEL为1时，为QZ-HZ段
    Function CoorTransCalcu(ByVal Pt As RoadPoint, ByVal YD As RoadPoint, ByVal JD As RoadPoint, ByVal ZX As Boolean) As RoadPoint
        Dim a As Double
        Dim θ As Double
        Dim P1 As New RoadPoint
        a = Azimuth(YD, JD)
        Select Case ZX
            Case True
                θ = a - PI / 2
                P1.X = YD.X + Pt.Y * Cos(θ) - Pt.X * Sin(θ)
                P1.Y = YD.Y + Pt.Y * Sin(θ) + Pt.X * Cos(θ)
            Case False
                P1.X = YD.X + Pt.X * Cos(a) - Pt.Y * Sin(a)
                P1.Y = YD.Y + Pt.X * Sin(a) + Pt.Y * Cos(a)
        End Select
        Return P1
    End Function
    '里程转换为标准格式
    Public Function LCZH(ByVal LC As Double) As String
        Dim B As Integer
        Dim F As Double
        B = LC \ 1000
        F = LC - B * 1000
        LCZH = "K" & B & "+" & Format(F, "0.000")
    End Function
    '转换为度.分秒格式

    '转换为弧度
    Function CRAD#(ByVal DMMSS As Double)
        Dim d, m, s As Double
        d = Int(DMMSS)
        m = Int((DMMSS - d) * 100)
        s = (DMMSS - d - m / 100) * 10000
        CRAD = (d + m / 60 + s / 3600) * PI / 180
    End Function
    '°′″格式输出
    Function D_MM_SS(ByVal AngRAD As Double)
        Dim SS, d, m, s As Double
        SS = AngRAD * 180 / PI * 3600
        d = Floor(SS / 3600)
        m = Floor((SS - d * 3600) / 60)
        s = SS - d * 3600 - m * 60

        D_MM_SS = d & "°" & m & "′" & Format(s, "0.0") & "″"
    End Function

    '计算坐标方位角函数  参数为起始点和终止点 （PtSta、PtEnd） 返回值为弧度
    Function Azimuth(PtSta As RoadPoint, PtEnd As RoadPoint)
        Dim A As Double
        If PtEnd.X = PtSta.X Then
            If PtEnd.Y >= PtSta.Y Then
                A = 0.5 * PI
            Else
                A = 1.5 * PI
            End If
        ElseIf PtEnd.X < PtSta.X Then
            A = Math.Atan((PtEnd.Y - PtSta.Y) / （PtEnd.X - PtSta.X）) + PI
        ElseIf PtEnd.Y < PtSta.Y Then
            A = Math.Atan((PtEnd.Y - PtSta.Y) / （PtEnd.X - PtSta.X）) + PI * 2
        Else
            A = Math.Atan((PtEnd.Y - PtSta.Y) / （PtEnd.X - PtSta.X）)
        End If
        Return A
    End Function

    Function Distance(PtSta As RoadPoint, PtEnd As RoadPoint)
        Return Sqrt(（PtEnd.X - PtSta.X） ^ 2 + (PtEnd.Y - PtSta.Y) ^ 2)
    End Function

End Module
