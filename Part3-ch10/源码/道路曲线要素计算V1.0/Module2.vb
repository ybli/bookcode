Imports System.IO
Imports System.Math
Imports System.Windows.Forms.DataVisualization.Charting
Module Module2
    Public DatTab As New DataTable
    Public Pt() As Pot   '保存已知点的数据
    Public Ptnum As Integer
    Public Structure Pot
        Dim ID As String
        Dim lab As String
        Dim X As Double
        Dim Y As Double
        Dim LC As Double
        Dim ZX As String
        Dim R As Double
        Dim Ls As Double
    End Structure

    '------------公共过程--------------
    '导入数据
    Sub DataInput(ByVal DGV As DataGridView)
        Dim OFD As New OpenFileDialog
        OFD.Filter = "TXT文本文件(*.txt)|*.txt"
        OFD.FilterIndex = 0
        If OFD.ShowDialog = DialogResult.OK Then
            Dim sr As New StreamReader(OFD.FileName)
            Dim data As String
            Dim s() As String
            Dim check As String
            data = sr.ReadToEnd
            data = Replace(data, vbCrLf, ",")
            data = StrReverse(data)
            Do
                check = Mid(data, 1, 1)
                If check = "," Then
                    data = Mid(data, 2, Len(data) - 1)
                End If
            Loop Until check <> ","
            data = StrReverse(data)
            s = Split(data, ",")
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
            Ptnum = s.Length / 5
            For i = 0 To Ptnum - 1
                DatTab.Rows.Add()
            Next
            Dim m As Integer
            For i = 0 To Ptnum - 1
                ReDim Preserve Pt(i)
                Pt(i).ID = s(m)
                Pt(i).X = s(m + 1)
                Pt(i).Y = s(m + 2)
                Pt(i).R = s(m + 3)
                Pt(i).Ls = s(m + 4)
                m += 5
                DatTab(i)(0) = Pt(i).ID
                DatTab(i)(1) = Pt(i).X
                DatTab(i)(2) = Pt(i).Y
            Next

            DGV.DataSource = DatTab
            For Each p As Pot In Pt
                If p.R <> 0 Then
                    If p.Ls <> 0 Then
                        p.lab = "YQX"
                    Else
                        p.lab = "HQX"
                    End If
                End If
            Next
            MsgBox("导入数据完成！", MsgBoxStyle.Information, "提示")
        End If
    End Sub
    '导出报告
    Sub DataOutput(ByVal te As TextBox)
        Dim SFD As New SaveFileDialog
        SFD.Filter = "TXT文本文件(*.txt)|*.txt"
        SFD.FilterIndex = 0
        If SFD.ShowDialog = DialogResult.OK Then
            FileOpen(1, SFD.FileName, OpenMode.Output)
            Print(1, te.Text)
            FileClose(1)
            MsgBox("保存报告完成！", MsgBoxStyle.Information, "提示")
        End If
    End Sub
    '保存图片
    Sub SavePic(ByVal chart1 As Chart)
        Dim SFD As New SaveFileDialog
        SFD.Filter = "BMP格式(*.bmp)|*.bmp"
        If SFD.ShowDialog = DialogResult.OK Then
            chart1.SaveImage(SFD.FileName, ChartImageFormat.Bmp)
            MsgBox("图片保存完成！", MsgBoxStyle.Information, "提示")
        End If
    End Sub
    '保存报表
    Sub ExportToExcel(ByVal dgv As DataGridView)
        Dim excel As Microsoft.Office.Interop.Excel._Application = New Microsoft.Office.Interop.Excel.Application()
        Dim workbook As Microsoft.Office.Interop.Excel._Workbook = excel.Workbooks.Add(Type.Missing)
        Dim worksheet As Microsoft.Office.Interop.Excel._Worksheet = Nothing
        Try
            worksheet = workbook.ActiveSheet
            worksheet.Name = "ExportedFromDatGrid"
            Dim cellRowIndex As Integer = 1
            Dim cellColumnIndex As Integer = 1
            For i As Integer = -1 To dgv.Rows.Count - 2
                For j As Integer = 0 To dgv.Columns.Count - 1

                    If cellRowIndex = 1 Then
                        worksheet.Cells(cellRowIndex, cellColumnIndex) = dgv.Columns(j).HeaderText
                    Else
                        worksheet.Cells(cellRowIndex, cellColumnIndex) = dgv.Rows(i).Cells(j).Value.ToString()
                    End If
                    cellColumnIndex += 1
                Next
                cellColumnIndex = 1
                cellRowIndex += 1
            Next
            Dim saveDialog As New SaveFileDialog()
            saveDialog.Filter = "Excel files (*.xlsx)|*.xlsx"
            saveDialog.FilterIndex = 1

            If saveDialog.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
                workbook.SaveAs(saveDialog.FileName)
                MsgBox("导出成功！", MsgBoxStyle.Information, "提示")
            Else
                excel.Quit()
                workbook = Nothing
                excel = Nothing
            End If
        Catch ex As System.Exception
            MessageBox.Show(ex.Message)
        Finally
            excel.Quit()
            workbook = Nothing
            excel = Nothing
        End Try
    End Sub

    Sub Count(ByVal te As TextBox, ByVal DGV As DataGridView)
        Pt(0).LC = 0
        For n = 0 To Ptnum - 2
            Pt(n + 1).LC = Pt(n).LC + Sqrt((Pt(n + 1).X - Pt(n).X) ^ 2 + (Pt(n + 1).Y - Pt(n).Y) ^ 2)
        Next
        Dim a_y, a1, a2, a3, a_h As Double
        a1 = Amize(Pt(0), Pt(1))
        a2 = Amize(Pt(1), Pt(2))
        a3 = Amize(Pt(2), Pt(3))
        a_y = a2 - a1
        a_h = a3 - a2
        If a_y > 0 Then
            Pt(1).ZX = "R"
        Else
            Pt(1).ZX = "L"
        End If
        If a_h > 0 Then
            Pt(2).ZX = "R"
        Else
            Pt(2).ZX = "L"
        End If
        Dim ZY, QZ, YZ As Pot
        Dim T, L, E_, q As Double
        T = Pt(1).R * Tan(a_y / 2)
        L = Pt(1).R * a_y
        E_ = Pt(1).R * (1 / Cos(a_y) - 1)
        q = 2 * T - L
        ZY.LC = Pt(1).LC - T
        QZ.LC = ZY.LC + L / 2
        YZ.LC = ZY.LC + L
        ZY.X = Pt(1).X - T * Cos(a1)
        ZY.Y = Pt(1).Y - T * Sin(a1)
        YZ.X = Pt(1).X - T * Cos(a2)
        YZ.Y = Pt(1).Y - T * Sin(a2)
        Dim P_xl() As Pot
        Dim s As Double
        Dim x, y As Double
        Dim i As Integer = 1
        ReDim P_xl(0)
        P_xl(0).LC = 0
        P_xl(0).X = Pt(0).X : P_xl(0).Y = Pt(0).Y
        Do While P_xl(i).LC < Pt(3).LC
            P_xl(i).LC = P_xl(i - 1).LC + 10
            i += 1
        Loop
        Dim ZH, HY, HQZ, YH, HZ As Pot
        Dim M, P_, B, Th, Lh, Eh, H_q As Double
        M = Pt(2).Ls / 2 - Pt(2).Ls ^ 2 / (240 * Pt(2).R ^ 2)
        P_ = Pt(2).Ls ^ 2 / (24 * Pt(2).R)
        B = Pt(2).Ls / (2 * Pt(2).R)
        Th = M + (Pt(2).R + P_) * Tan(a_h / 2)
        Lh = Pt(2).R * (a_h - 2 * B) + 2 * Pt(2).Ls
        Eh = (Pt(2).R + P_) * (1 / Cos(a_h / 2)) - Pt(2).R
        H_q = 2 * Th - Lh
        ZH.LC = Pt(2).LC - Th
        HY.LC = ZH.LC + Pt(2).Ls
        HQZ.LC = ZH.LC + Lh / 2
        YH.LC = ZH.LC + Lh - Pt(2).Ls
        HZ.LC = YH.LC + Pt(2).Ls
        ZH.X = Pt(2).X - Th * Cos(a1)
        ZH.Y = Pt(2).Y - Th * Sin(a1)
        HZ.X = Pt(2).X - Th * Cos(a2)
        HZ.Y = Pt(2).Y - Th * Sin(a2)
        For n = 0 To P_xl.Length - 1
            s = (P_xl(n).LC - ZY.LC) / PI
            If P_xl(n).LC <= ZY.LC Then
                P_xl(n).X = Pt(0).X + (P_xl(n).LC - Pt(0).LC) * Cos(a1)
                P_xl(n).Y = Pt(0).Y + (P_xl(n).LC - Pt(0).LC) * Sin(a1)
            ElseIf P_xl(n).LC <= YZ.LC Then
                x = Pt(1).R * Sin(s)
                y = Pt(1).R * (1 - Cos(s))
                If Pt(1).ZX = "L" Then
                    P_xl(n).X = ZY.X + x * Cos(a1) + y * Sin(a1)
                    P_xl(n).Y = ZY.Y + x * Sin(a1) - y * Cos(a1)
                Else
                    P_xl(n).X = ZY.X + x * Cos(a1) - y * Sin(a1)
                    P_xl(n).Y = ZY.Y + x * Sin(a1) + y * Cos(a1)
                End If
            ElseIf P_xl(n).LC <= ZH.LC Then
                P_xl(n).X = YZ.X + (P_xl(n).LC - YZ.LC) * Cos(a2)
                P_xl(n).Y = YZ.Y + (P_xl(n).LC - YZ.LC) * Sin(a2)
            ElseIf P_xl(n).LC <= HQZ.LC Then

            ElseIf P_xl(n).LC <= HZ.LC Then

            Else
                P_xl(n).X = HZ.X + (P_xl(n).LC - HZ.LC) * Cos(a3)
                P_xl(n).Y = HZ.Y + (P_xl(n).LC - HZ.LC) * Sin(a3)
            End If
        Next
    End Sub
    '-----------公共函数--------------
    '里程转换为标准格式
    Public Function LCZH(ByVal LC As Double) As String
        Dim B As Integer
        Dim F As Double
        B = LC \ 1000
        F = LC - B * 1000
        LCZH = "K" & B & "+" & Format(F, "0.000")
    End Function
    '转换为度.分秒格式
    Function CDMMSS#(ByVal Angrad As Double)
        Dim dd, d, m, s As Double
        dd = Angrad * 180 / Math.PI
        d = Int(dd)
        m = Int((dd - d) * 60)
        s = dd * 3600 - m * 60 - d * 3600
        CDMMSS = d + m / 100 + s / 10000
    End Function
    '转换为弧度
    Function CRAD#(ByVal DMMSS As Double)
        Dim d, m, s As Double
        d = Int(DMMSS)
        m = Int((DMMSS - d) * 100)
        s = (DMMSS - d - m / 100) * 10000
        CRAD = (d + m / 60 + s / 3600) * PI / 180
    End Function
    '°′″格式输出
    Function D_MM_SS(ByVal AngDMS As Double)
        Dim D, M, S As Double
        D = Int(AngDMS)
        M = Int((AngDMS - D) * 100)
        S = (AngDMS - D - M / 100) * 10000
        D_MM_SS = D & "°" & M & "′" & Format(S, "0.0") & "″"
    End Function
    '坐标方位角计算
    Function Amize#(ByVal pot_str As Pot, ByVal pot_end As Pot)
        If pot_str.X = pot_end.X Then
            If pot_end.Y >= pot_str.Y Then
                Amize = Math.PI / 2
            Else
                Amize = Math.PI * 3 / 2
            End If
        ElseIf pot_end.X < pot_str.X Then
            Amize = Math.Atan((pot_end.Y - pot_str.Y) / (pot_end.X - pot_str.X)) + Math.PI
        ElseIf pot_end.Y < pot_str.Y Then
            Amize = Atan((pot_end.Y - pot_str.Y) / (pot_end.X - pot_str.X)) + Math.PI * 2
        Else
            Amize = Math.Atan((pot_end.Y - pot_str.Y) / (pot_end.X - pot_str.X))
        End If
    End Function
End Module
