'Imports System.Math
'Imports System.Drawing.Drawing2D
Friend Class Form_Draw
    Private Sub DrawFormLoad(sender As Object, e As System.EventArgs) Handles Me.Load
        Me.WindowState = FormWindowState.Maximized
        Panel1.Height = My.Computer.Screen.Bounds.Height - 130
        Panel1.Width = My.Computer.Screen.Bounds.Width - 280
        TextBox1.Height = My.Computer.Screen.Bounds.Height - 130
        TextBox1.Multiline = True
        TextBox1.ScrollBars = ScrollBars.Vertical
        SaveGraphMenuItem.Enabled = False
    End Sub

    Private Sub 绘图MenuItem_Click(sender As Object, e As EventArgs) Handles DrawMenuItem.Click
        Dim DrawNet As New DrawNetGraph
        DrawNet.DrawContrNetwork(Me.Panel1, Form_Calcu.DrawPt, Form_Calcu.DrawSdDic)

        TextBox1.Text &= vbCrLf
        For Each SideItm As KeyValuePair(Of String, Side) In Form_Calcu.DrawSdDic
            TextBox1.Text &= SideItm.Key & vbCrLf
        Next

        TextBox1.Text &= vbCrLf
        For i = 0 To Form_Calcu.DrawPt.Length - 1
            If Form_Calcu.DrawPt(i).Type = 1 Then
                TextBox1.Text += String.Format("{0}_X = {1:f4}", Form_Calcu.DrawPt(i).Name, Form_Calcu.DrawPt(i).CoorX) + vbCrLf
                TextBox1.Text += String.Format("{0}_Y = {1:f4}", Form_Calcu.DrawPt(i).Name, Form_Calcu.DrawPt(i).CoorY) + vbCrLf + vbCrLf
            End If
        Next

        For i = 0 To Form_Calcu.DrawPt.Length - 1
            If Form_Calcu.DrawPt(i).Type = 0 Then
                TextBox1.Text += String.Format("{0}_X = {1:f4}", Form_Calcu.DrawPt(i).Name, Form_Calcu.DrawPt(i).CoorX) + vbCrLf
                TextBox1.Text += String.Format("{0}_Y = {1:f4}", Form_Calcu.DrawPt(i).Name, Form_Calcu.DrawPt(i).CoorY) + vbCrLf + vbCrLf
            End If
        Next
        SaveGraphMenuItem.Enabled = True
    End Sub

    Private Sub 保存图形MenuItem_Click(sender As Object, e As EventArgs) Handles SaveGraphMenuItem.Click
        SaveGraphToFile(Panel1)
    End Sub

    Private Sub SaveGraphToFile(CopyObj As Control)
        Dim SourUpLfPt As New Point(CopyObj.Left - 3, CopyObj.Top + 23)
        Dim DestUpLfPt As New Point(0, 0)
        Dim CopySize As New Size(CopyObj.Width + 8, CopyObj.Height + 5)
        Dim CopyBitmap As Image = New Bitmap(CopyObj.Width + 8, CopyObj.Height + 5)
        Using Gr As Graphics = Graphics.FromImage(CopyBitmap)
            Gr.CopyFromScreen(SourUpLfPt, DestUpLfPt, CopySize) 'Public Sub CopyFromScreen ( upperLeftSource As Point, upperLeftDestination As Point,blockRegionSize As Size )
            Dim SFile As New SaveFileDialog
            SFile.Filter = "BMP Files|*.bmp|All Files|*.*"
            If SFile.ShowDialog() = DialogResult.OK Then
                CopyBitmap.Save(SFile.FileName, System.Drawing.Imaging.ImageFormat.Bmp) '存储一张位图
            End If
            SFile.Dispose()
        End Using
    End Sub

    Private Sub 退出ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles 退出ToolStripMenuItem.Click
        Close()
        Form_Calcu.Show()
    End Sub
    Private Sub DrawFormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Form_Calcu.Show()
    End Sub

End Class