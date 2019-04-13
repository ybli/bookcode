Imports System.ComponentModel
Imports System.Math

Public Class Form1
    Dim m_Delta, m_Pos As Point
    Dim check As Boolean = False

    Private Sub Form1_Paint(sender As Object, e As PaintEventArgs) Handles Me.Paint
        TabControl1.Height = Me.Height - 100
        TabControl1.Width = MenuStrip1.Width
    End Sub

    Private Sub 打开数据文件_Click(sender As Object, e As EventArgs) Handles 打开数据文件.Click, open.Click
        DataInput(DGV)
    End Sub

    Private Sub 保存报告_Click(sender As Object, e As EventArgs) Handles 保存报告.Click, save.Click
        DataOutput(TextBox1)
    End Sub

    Private Sub 计算_Click(sender As Object, e As EventArgs) Handles 计算.Click, count.Click
        TextBox1.Text = ""
        StakeSpac = Val(TextBox2.Text)
        RoadCalculate()
        Result(TextBox1)
        Draw(Chart1)
        DatTab_Point()
        MsgBox("计算完成！"， MsgBoxStyle.Information, "提示")
    End Sub

    Private Sub 示意图_Click(sender As Object, e As EventArgs) Handles 示意图.Click, pic.Click
        TabControl1.SelectedTab = TabPage2
    End Sub

    Private Sub 报告_Click(sender As Object, e As EventArgs) Handles 报告.Click, rep.Click
        TabControl1.SelectedTab = TabPage3
    End Sub

    Private Sub ToolStripButton6_Click(sender As Object, e As EventArgs)
        Chart1.Width = Chart1.Width * 1.1
        Chart1.Height = Chart1.Height * 1.1
    End Sub

    Private Sub ToolStripButton7_Click(sender As Object, e As EventArgs)
        Chart1.Width = Chart1.Width * 0.9
        Chart1.Height = Chart1.Height * 0.9
    End Sub
    '鼠标拖动图形移动
    Private Sub Chart1_MouseDown(sender As Object, e As MouseEventArgs) Handles Chart1.MouseDown
        check = True
        m_Pos = New Point(e.X, e.Y)
    End Sub
    Private Sub Chart1_MouseUp(sender As Object, e As MouseEventArgs) Handles Chart1.MouseUp
        check = False
    End Sub
    Private Sub Chart1_MouseMove(sender As Object, e As MouseEventArgs) Handles Chart1.MouseMove
        If check Then
            Cursor.Current = Cursors.Hand
            m_Delta = New Point(e.X - m_Pos.X, e.Y - m_Pos.Y)
            Chart1.Location = New Point(Chart1.Location + m_Delta)
        End If
    End Sub

    Private Sub 保存图形_Click(sender As Object, e As EventArgs) Handles 保存图形.Click
        SavePic(Chart1)
    End Sub

    Private Sub 保存报表_Click(sender As Object, e As EventArgs)
        'ExportToExcel(DGV)
    End Sub

    Private Sub 退出_Click(sender As Object, e As EventArgs) Handles 退出.Click, clos.Click
        If MsgBox("确定退出程序吗？", vbYesNo + vbQuestion, "警告") = MsgBoxResult.Yes Then
            Application.Exit()
        End If
    End Sub

    Private Sub 刷新ZToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles 刷新ZToolStripMenuItem.Click, refush.Click
        Application.Restart()
    End Sub

    Private Sub grid_Click(sender As Object, e As EventArgs) Handles grid.Click
        TabControl1.SelectedTab = TabPage1
    End Sub

    Private Sub 帮助HToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles 帮助HToolStripMenuItem.Click, help.Click
        MsgBox("原始数据txt文件格式：" & vbCrLf & vbCrLf &
               "起始点名， X坐标， Y坐标" & vbCrLf &
               "交点名1，X坐标，Y坐标，圆曲线半径，缓曲线长" & vbCrLf &
               "      ... ..." & vbCrLf &
               "交点名n，X坐标，Y坐标，圆曲线半径，缓曲线长" & vbCrLf &
               "终端点名， X坐标， Y坐标" & vbCrLf)

    End Sub

    Private Sub big_Click(sender As Object, e As EventArgs) Handles big.Click
        Chart1.Width = Chart1.Width * 1.1
        Chart1.Height = Chart1.Height * 1.1
    End Sub

    Private Sub small_Click(sender As Object, e As EventArgs) Handles small.Click
        Chart1.Width = Chart1.Width * 0.9
        Chart1.Height = Chart1.Height * 0.9
    End Sub
    Private Sub Form1_MouseWheel(sender As Object, e As MouseEventArgs) Handles Me.MouseWheel
        Chart1.Width = Chart1.Width * (1 + e.Delta / 120 * 0.1)
        Chart1.Height = Chart1.Height * (1 + e.Delta / 120 * 0.1)
    End Sub
    Private Sub 输出DXF文件_Click(sender As Object, e As EventArgs) Handles 输出DXF文件.Click
        DXF()
    End Sub

    Private Sub Form1_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
        If MsgBox("确定退出程序吗？", vbYesNo + vbQuestion, "警告") = MsgBoxResult.Yes Then
            e.Cancel = False
        Else
            e.Cancel = True
        End If
    End Sub
End Class
