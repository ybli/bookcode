
Public Class Form_Calcu
    Dim TextBoxDic As New Dictionary(Of TextBox, String)
    Dim AppPath As String

    Dim CalProj As CalcuProj
    Dim ForwStatus As CalcuStatus
    Dim BackStatus As CalcuStatus
    Dim DistStatus As CalcuStatus
    Dim ErrInf As Boolean = False
    Dim KwPtErr As String

    Dim DataArray() As String
    Friend KnowPt() As ContrPoint
    Dim KnowPtCount As Integer
    Dim UnkwPtCount As Integer
    Friend Structure BackMeetRst
        Dim BackMeetPt As ContrPoint
        Dim DangerCir As String
        Dim AuxiParam As String
    End Structure

    Dim ForwData() As ForwDataStru
    Dim BackData() As BackDataStru
    Dim DistData() As DistDataStru

    Friend ForwMeetPt() As ContrPoint
    Friend BackMeetPt() As BackMeetRst
    Friend DistMeetPt() As ContrPoint

    Friend DrawPt() As ContrPoint
    Friend DrawSdDic As New Dictionary(Of String, Side)
    Friend HandMeetDic As New Dictionary(Of String, String)

    Private Sub Form1_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        TextBoxDic.Add(TextAX, "1,1,1") : TextBoxDic.Add(TextAY, "1,1,1")
        TextBoxDic.Add(TextBX, "1,1,1") : TextBoxDic.Add(TextBY, "1,1,1")
        TextBoxDic.Add(TextCX, "0,1,0") : TextBoxDic.Add(TextCY, "0,1,0")
        TextBoxDic.Add(Textα, "1,1,0") : TextBoxDic.Add(Textβ, "1,1,0") : TextBoxDic.Add(Textγ, "0,1,0")
        TextBoxDic.Add(TextDsa, "0,0,1") : TextBoxDic.Add(TextDsb, "0,0,1")

        CalProj = 1
        SetText(CalProj)
        FileDataRadioBtn.Checked = True
        IputDataRadioBtn.Checked = False

        GrpBxKwPt.Enabled = False
        GrpBxAngl.Enabled = False
        GrpBxDist.Enabled = False

        KnowDataMenuItem.Enabled = True
        SurvDataMenuItem.Enabled = False
        OutputMenuItem.Enabled = False
        DrawMenuItem.Enabled = False

        ForwStatus = 1
        BackStatus = 1
        DistStatus = 1

        AppPath = Mid(Application.StartupPath, 1, Len(Application.StartupPath) - 9)
        PictureBox1.Image = Image.FromFile(AppPath + "ForwMeet.bmp")

    End Sub

#Region "计算模式控制"
    Friend Sub SetText(Proj As CalcuProj)
        For Each Item As KeyValuePair(Of TextBox, String) In TextBoxDic
            Dim EnabSwitch() As String = Item.Value.Split(",")
            Item.Key.Enabled = CInt(EnabSwitch(Proj - 1))
        Next
    End Sub

    Private Sub ModeControl()
        If FileDataRadioBtn.Checked Then
            GrpBxKwPt.Enabled = False
            GrpBxAngl.Enabled = False
            GrpBxDist.Enabled = False
            CalcuMenuItem.Enabled = False
            Button1.Enabled = False
            If KnowPtCount = 0 Then
                KnowDataMenuItem.Enabled = True
                SurvDataMenuItem.Enabled = False
            Else
                KnowDataMenuItem.Enabled = False
                SurvDataMenuItem.Enabled = True
            End If
        Else
            CalcuMenuItem.Enabled = True
            Button1.Enabled = True
            GrpBxKwPt.Enabled = True
            GrpBxAngl.Enabled = True
            GrpBxDist.Enabled = True
            KnowDataMenuItem.Enabled = False
            SurvDataMenuItem.Enabled = False
        End If
    End Sub
    Private Sub 前方交会MenuItem_Click(sender As Object, e As EventArgs) Handles 前方交会ToolStripMenuItem.Click
        CalProj = 1
        SetText(CalProj)
        ModeControl()
        PictureBox1.Image = Image.FromFile(AppPath + "ForwMeet.bmp")
    End Sub
    Private Sub 后方交会MenuItem_Click(sender As Object, e As EventArgs) Handles 后方交会ToolStripMenuItem.Click
        CalProj = 2
        SetText(CalProj)
        ModeControl()
        PictureBox1.Image = Image.FromFile(AppPath + "BackMeet.bmp")
    End Sub
    Private Sub 距离交会MenuItem_Click(sender As Object, e As EventArgs) Handles 距离交会ToolStripMenuItem.Click
        CalProj = 3
        SetText(CalProj)
        ModeControl()
        PictureBox1.Image = Image.FromFile(AppPath + "DistMeet.bmp")
    End Sub
    Private Sub IputDataRadioBtn_CheckedChanged(sender As Object, e As EventArgs) Handles IputDataRadioBtn.CheckedChanged
        清空Button_Click(sender, e)
        ModeControl()
    End Sub
#End Region

#Region "文件数据导入"
    Private Sub KnowDataMenuItem_Click(sender As Object, e As EventArgs) Handles KnowDataMenuItem.Click
        DataArray = InputData()
        If DataArray(0) = "" Then Exit Sub
        If DataArray.Length Mod 3 = 0 Then
            KnowPtCount = DataArray.Length / 3
            ReDim KnowPt(KnowPtCount - 1）
        Else
            MsgBox("已知点数据组织错误！")
            Exit Sub
        End If
        For i% = 0 To KnowPtCount - 1
            KnowPt(i） = New ContrPoint
            KnowPt(i）.Name = DataArray(3 * i)
            KnowPt(i）.Type = 1
            KnowPt(i）.CoorX = CDbl(DataArray(3 * i + 1))
            KnowPt(i）.CoorY = CDbl(DataArray(3 * i + 2))
        Next
        OutTxtBx.Text = vbCrLf
        OutTxtBx.Text &= "=============================================" & vbCrLf & vbCrLf

        OutTxtBx.Text &= String.Format("计算日期：{0}", System.DateTime.Now) & vbCrLf
        OutTxtBx.Text &= "已知数据：" & vbCrLf
        For i% = 0 To KnowPtCount - 1
            OutTxtBx.Text &= String.Format("{0,5}: x={1:f3}  y={2:f3}"， KnowPt(i).Name, KnowPt(i).CoorX, KnowPt(i).CoorY) & vbCrLf
        Next
        OutTxtBx.Text &= vbCrLf
        OutTxtBx.Text &= "=============================================" & vbCrLf

        KnowDataMenuItem.Enabled = False
        SurvDataMenuItem.Enabled = True
        DrawMenuItem.Enabled = True
    End Sub
    Private Sub SurvDataMenuItem_Click(sender As Object, e As EventArgs) Handles SurvDataMenuItem.Click
        If CalProj = 1 And ForwStatus = 2 Then
            Dim ReturnInf% = MsgBox("前方交会已完成！确定重新计算么？"， 1 + 48 + 256)
            If ReturnInf <> 1 Then
                Exit Sub
            Else
                ForwStatus = 1
            End If
        ElseIf CalProj = 2 And BackStatus = 2 Then
            Dim ReturnInf% = MsgBox("后方交会已完成！确定重新计算么？"， 1 + 48 + 256)
            If ReturnInf <> 1 Then
                Exit Sub
            Else
                BackStatus = 1
            End If
        ElseIf CalProj = 3 And DistStatus = 2 Then
            Dim ReturnInf% = MsgBox("距离交会已完成！确定重新计算么？"， 1 + 48 + 256)
            If ReturnInf <> 1 Then
                Exit Sub
            Else
                DistStatus = 1
            End If
        End If

        DataArray = InputData()
        If DataArray(0) = "" Then Exit Sub
        UnkwPtCount = DataArray.Length
        Select Case CalProj
            Case 1
                If UnkwPtCount Mod 5 = 0 Then
                    UnkwPtCount = DataArray.Length / 5
                    ReDim ForwData(UnkwPtCount - 1)
                    For i% = 0 To UnkwPtCount - 1
                        ForwData(i).PtName = DataArray(5 * i)
                        ForwData(i).PtAname = DataArray(5 * i + 1)
                        ForwData(i).PtBname = DataArray(5 * i + 2)
                        ForwData(i).AngA = CDbl(DataArray(5 * i + 3))
                        ForwData(i).AngB = CDbl(DataArray(5 * i + 4))
                    Next
                Else
                    MsgBox("观测数据组织错误！")
                    Exit Sub
                End If
                OutTxtBx.Text &= vbCrLf
                OutTxtBx.Text &= "前方交会观测数据：" & vbCrLf
                For i% = 0 To UnkwPtCount - 1
                    OutTxtBx.Text &= String.Format("待定点：{0,5}   已知点：{1,5}，{2,5}",
                                     ForwData(i).PtName, ForwData(i).PtAname, ForwData(i).PtBname) & vbCrLf
                    OutTxtBx.Text &= String.Format("观测角值：{0}，  {1}",
                                     Dmmss$(ForwData(i).AngA), Dmmss$(ForwData(i).AngB)) & vbCrLf & vbCrLf
                Next
            Case 2
                If UnkwPtCount Mod 7 = 0 Then
                    UnkwPtCount = DataArray.Length / 7
                    ReDim BackData(UnkwPtCount - 1)
                    For i% = 0 To UnkwPtCount - 1
                        BackData(i).PtName = DataArray(7 * i)
                        BackData(i).PtAname = DataArray(7 * i + 1)
                        BackData(i).PtBname = DataArray(7 * i + 2)
                        BackData(i).PtCname = DataArray(7 * i + 3)
                        BackData(i).AngA = CDbl(DataArray(7 * i + 4))
                        BackData(i).AngB = CDbl(DataArray(7 * i + 5))
                        BackData(i).AngC = CDbl(DataArray(7 * i + 6))
                    Next
                Else
                    MsgBox("观测数据组织错误！")
                    Exit Sub
                End If

                OutTxtBx.Text &= vbCrLf
                OutTxtBx.Text &= "后方交会观测数据：" & vbCrLf
                For i% = 0 To UnkwPtCount - 1
                    OutTxtBx.Text &= String.Format("待定点：{0,5}   已知点：{1,5}，{2,5}，{3,5}",
                                     BackData(i).PtName, BackData(i).PtAname, BackData(i).PtBname, BackData(i).PtCname) & vbCrLf
                    OutTxtBx.Text &= String.Format("观测角值：{0}， {1},  {2}",
                                     Dmmss$(BackData(i).AngA), Dmmss$(BackData(i).AngB), Dmmss$(BackData(i).AngC)) & vbCrLf & vbCrLf
                Next
            Case 3
                If UnkwPtCount Mod 5 = 0 Then
                    UnkwPtCount = DataArray.Length / 5
                    ReDim DistData(UnkwPtCount - 1)
                    For i% = 0 To UnkwPtCount - 1
                        DistData(i).PtName = DataArray(5 * i)
                        DistData(i).PtAname = DataArray(5 * i + 1)
                        DistData(i).PtBname = DataArray(5 * i + 2)
                        DistData(i).DisA = CDbl(DataArray(5 * i + 3))
                        DistData(i).DisB = CDbl(DataArray(5 * i + 4))
                    Next
                Else
                    MsgBox("观测数据组织错误！")
                    Exit Sub
                End If
                OutTxtBx.Text &= vbCrLf
                OutTxtBx.Text &= "距离交会观测数据：" & vbCrLf
                For i% = 0 To UnkwPtCount - 1
                    OutTxtBx.Text &= String.Format("待定点：{0,5}   已知点：{1,5}，{2,5}",
                                     DistData(i).PtName, DistData(i).PtAname, DistData(i).PtBname) & vbCrLf
                    OutTxtBx.Text &= String.Format("观测边长：{0:f3}m，  {1:f3}m",
                                     DistData(i).DisA, DistData(i).DisB) & vbCrLf & vbCrLf
                Next
        End Select
        CalcuMenuItem.Enabled = True
        Button1.Enabled = True
    End Sub

    Function TextCheck(TxBx As TextBox) As Boolean
        If IsNumeric(TxBx.Text) Then
            Return True
        Else
            Return False
        End If
    End Function

#End Region

#Region "计算及其显示"

    Private Sub 计算Button_Click(sender As Object, e As System.EventArgs) Handles Button1.Click， CalcuMenuItem.Click

        If FileDataRadioBtn.Checked Then
            If KnowPtCount = 0 Then
                MsgBox("尚未导入已知点数据！")
                Exit Sub
            ElseIf UnkwPtCount = 0 Then
                MsgBox("尚未导入观测点数据！")
                Exit Sub
            ElseIf CalProj = 1 And ForwStatus = 2 Then
                MsgBox("已完成前方交会计算！")
                Exit Sub
            ElseIf CalProj = 2 And BackStatus = 2 Then
                MsgBox("已完成后方交会计算！")
                Exit Sub
            ElseIf CalProj = 3 And DistStatus = 2 Then
                MsgBox("已完成距离交会计算！")
                Exit Sub
            Else
                CalcuProcess()
            End If
        Else

            HandCalcu()
            If ErrInf = False Then
                Exit Sub
            Else
                DrawMenuItem.Enabled = True
            End If
        End If

        '输出计算成果===========================================================================================
        Select Case CalProj
            Case 1
                OutTxtBx.Text &= "前方交会计算成果：" & vbCrLf
                For i% = 0 To ForwMeetPt.Length - 1
                    OutTxtBx.Text &= String.Format("     {0,5}:  x={1:f3}，y={2:f3}"，
                                     ForwMeetPt(i).Name, ForwMeetPt(i).CoorX, ForwMeetPt(i).CoorY) & vbCrLf
                Next

                If Not (KwPtErr = "") Then
                    OutTxtBx.Text &= "错误点：" & vbCrLf
                    KwPtErr = KwPtErr.Remove(KwPtErr.Length - 1)
                    Dim ErrID() As String = KwPtErr.Split(",")
                    For Each item$ In ErrID
                        OutTxtBx.Text &= "      " & ForwData(CInt(item)).PtName & vbCrLf
                    Next
                End If
                OutTxtBx.Text &= vbCrLf
                OutTxtBx.Text &= "=============================================" & vbCrLf

                If FileDataRadioBtn.Checked Then
                    ForwStatus = 2
                End If

            Case 2
                OutTxtBx.Text &= "后方交会计算成果：" & vbCrLf
                For i% = 0 To BackMeetPt.Length - 1
                    OutTxtBx.Text &= String.Format("     {0,5}:  x={1:f3}，y={2:f3}"，
                                     BackMeetPt(i).BackMeetPt.Name, BackMeetPt(i).BackMeetPt.CoorX, BackMeetPt(i).BackMeetPt.CoorY) & vbCrLf
                    OutTxtBx.Text &= String.Format("危险度：{0}", BackMeetPt(i).DangerCir) & vbCrLf
                    OutTxtBx.Text &= String.Format("辅助计算系数：" & vbCrLf & "{0}"， BackMeetPt(i).AuxiParam) & vbCrLf & vbCrLf
                Next

                If Not (KwPtErr = "") Then
                    OutTxtBx.Text &= "错误点：" & vbCrLf
                    KwPtErr = KwPtErr.Remove(KwPtErr.Length - 1)
                    Dim ErrID() As String = KwPtErr.Split(",")
                    For Each item$ In ErrID
                        OutTxtBx.Text &= "      " & BackData(CInt(item)).PtName & vbCrLf
                    Next
                End If
                OutTxtBx.Text &= vbCrLf
                OutTxtBx.Text &= "=============================================" & vbCrLf

                If FileDataRadioBtn.Checked Then
                    BackStatus = 2
                End If
            Case 3
                OutTxtBx.Text &= "距离交会计算成果：" & vbCrLf
                For i% = 0 To DistMeetPt.Length - 1
                    OutTxtBx.Text &= String.Format("     {0,5}:  x={1:f3}，y={2:f3}"，
                                     DistMeetPt(i).Name, DistMeetPt(i).CoorX, DistMeetPt(i).CoorY) & vbCrLf
                Next
                If Not (KwPtErr = "") Then
                    OutTxtBx.Text &= "错误点：" & vbCrLf
                    KwPtErr = KwPtErr.Remove(KwPtErr.Length - 1)
                    Dim ErrID() As String = KwPtErr.Split(",")
                    For Each item$ In ErrID
                        OutTxtBx.Text &= "      " & DistData(CInt(item)).PtName & vbCrLf
                    Next
                End If
                OutTxtBx.Text &= vbCrLf
                OutTxtBx.Text &= "=============================================" & vbCrLf

                If FileDataRadioBtn.Checked Then
                    DistStatus = 2
                End If
        End Select
        OutputMenuItem.Enabled = True
    End Sub

    Private Sub HandCalcu()
        Static FCount : Static BCount : Static DCount
        Select Case CalProj
            Case 1
                ReDim KnowPt(1)
                If TextCheck(TextAX) And TextCheck(TextAY) And TextCheck(TextBX) And TextCheck(TextBY) Then
                    KnowPt(0) = New ContrPoint : KnowPt(1) = New ContrPoint
                    KnowPt(0).Name = "FMA#" & (FCount + 1) : KnowPt(0).CoorX = TextAX.Text : KnowPt(0).CoorY = TextAY.Text : KnowPt(0).Type = 1
                    KnowPt(1).Name = "FMB#" & (FCount + 1) : KnowPt(1).CoorX = TextBX.Text : KnowPt(1).CoorY = TextBY.Text : KnowPt(1).Type = 1

                    HandMeetDic.Add(KnowPt(0).Name, String.Format("{0:f3}_{1:f3}_{2}", KnowPt(0).CoorX, KnowPt(0).CoorY, KnowPt(0).Type))
                    HandMeetDic.Add(KnowPt(1).Name, String.Format("{0:f3}_{1:f3}_{2}", KnowPt(1).CoorX, KnowPt(1).CoorY, KnowPt(1).Type))
                    KnowPtCount = 2
                    ErrInf = True
                Else
                    MsgBox("已知点数据不全或错误！")
                    ErrInf = False
                    Exit Sub
                End If
                ReDim ForwMeetPt(0)
                ReDim ForwData(0)
                If TextCheck(Textα) And TextCheck(Textβ) Then
                    ForwData(0).PtName = "FPt#" & (FCount + 1)
                    ForwData(0).PtAname = KnowPt(0).Name
                    ForwData(0).PtBname = KnowPt(1).Name
                    ForwData(0).AngA = Textα.Text
                    ForwData(0).AngB = Textβ.Text
                    UnkwPtCount = 1
                    ErrInf = True
                Else
                    MsgBox("观测数据不全或错误！")
                    ErrInf = False
                    Exit Sub
                End If

                OutTxtBx.Text &= vbCrLf
                OutTxtBx.Text &= "=============================================" & vbCrLf & vbCrLf
                OutTxtBx.Text &= "前方交会数据：" & vbCrLf
                OutTxtBx.Text &= "   ---已知点：" & vbCrLf
                For i% = 0 To KnowPtCount - 1
                    OutTxtBx.Text &= String.Format("{0,5}: x={1:f3}  y={2:f3}"， KnowPt(i).Name, KnowPt(i).CoorX, KnowPt(i).CoorY) & vbCrLf
                Next
                OutTxtBx.Text &= "   ---观测角值：" & vbCrLf
                OutTxtBx.Text &= String.Format("        α={0}", Dmmss$(ForwData(0).AngA)) & vbCrLf
                OutTxtBx.Text &= String.Format("        β={0}", Dmmss$(ForwData(0).AngB)) & vbCrLf
                OutTxtBx.Text &= vbCrLf
                CalcuProcess()
                FCount += 1

            Case 2
                ReDim KnowPt(2)
                If TextCheck(TextAX) And TextCheck(TextAY) And TextCheck(TextBX) And TextCheck(TextBY) And TextCheck(TextCX) And TextCheck(TextCY) Then
                    KnowPt(0) = New ContrPoint : KnowPt(1) = New ContrPoint : KnowPt(2) = New ContrPoint
                    KnowPt(0).Name = "BMA#" & (BCount + 1) : KnowPt(0).CoorX = TextAX.Text : KnowPt(0).CoorY = TextAY.Text : KnowPt(0).Type = 1
                    KnowPt(1).Name = "BMB#" & (BCount + 1) : KnowPt(1).CoorX = TextBX.Text : KnowPt(1).CoorY = TextBY.Text : KnowPt(1).Type = 1
                    KnowPt(2).Name = "BMC#" & (BCount + 1) : KnowPt(2).CoorX = TextCX.Text : KnowPt(2).CoorY = TextCY.Text : KnowPt(2).Type = 1

                    HandMeetDic.Add(KnowPt(0).Name, String.Format("{0:f3}_{1:f3}_{2}", KnowPt(0).CoorX, KnowPt(0).CoorY, KnowPt(0).Type))
                    HandMeetDic.Add(KnowPt(1).Name, String.Format("{0:f3}_{1:f3}_{2}", KnowPt(1).CoorX, KnowPt(1).CoorY, KnowPt(1).Type))
                    HandMeetDic.Add(KnowPt(2).Name, String.Format("{0:f3}_{1:f3}_{2}", KnowPt(2).CoorX, KnowPt(2).CoorY, KnowPt(2).Type))
                    KnowPtCount = 3
                    ErrInf = True
                Else
                    MsgBox("已知点数据不全或错误！")
                    ErrInf = False
                    Exit Sub
                End If
                ReDim BackMeetPt(0)
                ReDim BackData(0)
                If TextCheck(Textα) And TextCheck(Textβ) And TextCheck(Textγ) Then
                    BackData(0).PtName = "BPt#" & (BCount + 1)
                    BackData(0).PtAname = KnowPt(0).Name
                    BackData(0).PtBname = KnowPt(1).Name
                    BackData(0).PtCname = KnowPt(2).Name
                    BackData(0).AngA = Textα.Text
                    BackData(0).AngB = Textβ.Text
                    BackData(0).AngC = Textγ.Text
                    UnkwPtCount = 1
                    ErrInf = True
                Else
                    MsgBox("观测数据不全或错误！")
                    ErrInf = False
                    Exit Sub
                End If

                OutTxtBx.Text &= vbCrLf
                OutTxtBx.Text &= "=============================================" & vbCrLf & vbCrLf
                OutTxtBx.Text &= "后方交会数据：" & vbCrLf
                OutTxtBx.Text &= "   ---已知点：" & vbCrLf
                For i% = 0 To KnowPtCount - 1
                    OutTxtBx.Text &= String.Format("        {0,5}: x={1:f3}  y={2:f3}"， KnowPt(i).Name, KnowPt(i).CoorX, KnowPt(i).CoorY) & vbCrLf
                Next
                OutTxtBx.Text &= "   ---观测角值：" & vbCrLf
                OutTxtBx.Text &= String.Format("        α={0}", Dmmss$(BackData(0).AngA)) & vbCrLf
                OutTxtBx.Text &= String.Format("        β={0}", Dmmss$(BackData(0).AngB)) & vbCrLf
                OutTxtBx.Text &= String.Format("        γ={0}", Dmmss$(BackData(0).AngC)) & vbCrLf
                OutTxtBx.Text &= vbCrLf
                CalcuProcess()
                BCount += 1

            Case 3
                ReDim KnowPt(1)
                If TextCheck(TextAX) And TextCheck(TextAY) And TextCheck(TextBX) And TextCheck(TextBY) Then
                    KnowPt(0) = New ContrPoint : KnowPt(1) = New ContrPoint
                    KnowPt(0).Name = "DMA#" & (DCount + 1) : KnowPt(0).CoorX = TextAX.Text : KnowPt(0).CoorY = TextAY.Text : KnowPt(0).Type = 1
                    KnowPt(1).Name = "DMB#" & (DCount + 1) : KnowPt(1).CoorX = TextBX.Text : KnowPt(1).CoorY = TextBY.Text : KnowPt(1).Type = 1

                    HandMeetDic.Add(KnowPt(0).Name, String.Format("{0:f3}_{1:f3}_{2}", KnowPt(0).CoorX, KnowPt(0).CoorY, KnowPt(0).Type))
                    HandMeetDic.Add(KnowPt(1).Name, String.Format("{0:f3}_{1:f3}_{2}", KnowPt(1).CoorX, KnowPt(1).CoorY, KnowPt(1).Type))
                    KnowPtCount = 2
                    ErrInf = True
                Else
                    MsgBox("已知点数据不全或错误！")
                    ErrInf = False
                    Exit Sub
                End If
                ReDim DistMeetPt(0)
                ReDim DistData(0)
                If TextCheck(TextDsa) And TextCheck(TextDsb) Then
                    DistData(0).PtName = "DPt#" & (DCount + 1)
                    DistData(0).PtAname = KnowPt(0).Name
                    DistData(0).PtBname = KnowPt(1).Name
                    DistData(0).DisA = TextDsa.Text
                    DistData(0).DisB = TextDsb.Text
                    UnkwPtCount = 1
                    ErrInf = True
                Else
                    MsgBox("观测数据不全或错误！")
                    ErrInf = False
                    Exit Sub
                End If

                OutTxtBx.Text &= vbCrLf
                OutTxtBx.Text &= "=============================================" & vbCrLf & vbCrLf
                OutTxtBx.Text &= "前方交会数据：" & vbCrLf
                OutTxtBx.Text &= "   ---已知点：" & vbCrLf
                For i% = 0 To KnowPtCount - 1
                    OutTxtBx.Text &= String.Format("{0,5}: x={1:f3}  y={2:f3}"， KnowPt(i).Name, KnowPt(i).CoorX, KnowPt(i).CoorY) & vbCrLf
                Next
                OutTxtBx.Text &= "   ---观测边长：" & vbCrLf
                OutTxtBx.Text &= String.Format("        Dist_a={0:f3}", DistData(0).DisA) & vbCrLf
                OutTxtBx.Text &= String.Format("        Dist_b={0:f3}", DistData(0).DisB) & vbCrLf
                OutTxtBx.Text &= vbCrLf
                CalcuProcess()
                DCount += 1
        End Select
    End Sub

    Private Sub CalcuProcess()
        Select Case CalProj
            Case 1
                ReDim ForwMeetPt(0)
                KwPtErr = ""
                For i% = 0 To UnkwPtCount - 1
                    Dim PA As New ContrPoint
                    Dim PB As New ContrPoint
                    For j% = 0 To KnowPtCount - 1
                        If ForwData(i).PtAname = KnowPt(j).Name Then
                            PA = KnowPt(j)
                        End If
                        If ForwData(i).PtBname = KnowPt(j).Name Then
                            PB = KnowPt(j)
                        End If
                    Next
                    If IsNothing(PA.Name) Or IsNothing(PB.Name) Then
                        MsgBox(String.Format("点{0}要求的已知点不存在！", ForwData(i).PtAname))
                        ForwData(i).PtAname *= "_DataErr"
                        KwPtErr &= i & ","
                        Continue For
                    End If
                    Dim ForwPtCalcu As ForwMeet
                    ForwPtCalcu = New ForwMeet(ForwData(i).PtName, PA, PB, CRad(ForwData(i).AngA), CRad(ForwData(i).AngB))
                    ForwMeetPt(ForwMeetPt.Length - 1) = ForwPtCalcu.PtP
                    If IputDataRadioBtn.Checked Then
                        HandMeetDic.Add(ForwPtCalcu.PtP.Name, String.Format("{0:f3}_{1:f3}_{2}", ForwPtCalcu.PtP.CoorX, ForwPtCalcu.PtP.CoorY, ForwPtCalcu.PtP.Type))
                    End If

                    Try
                        DrawSdDic.Add(PA.Name & "_" & PB.Name, New Side(PA, PB))
                    Catch ex As ArgumentException
                        Dim msg As String = String.Format("{0} ralsed exception :{1}", ex.Source, ex.Message)
                        MessageBox.Show(msg, "My App", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End Try

                    Try
                        DrawSdDic.Add(PA.Name & "_" & ForwPtCalcu.PtP.Name, New Side(PA, ForwPtCalcu.PtP))
                    Catch ex As ArgumentException
                        Dim msg As String = String.Format("{0} ralsed exception :{1}", ex.Source, ex.Message)
                        MessageBox.Show(msg, "My App", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End Try

                    Try
                        DrawSdDic.Add(PB.Name & "_" & ForwPtCalcu.PtP.Name, New Side(PB, ForwPtCalcu.PtP))
                    Catch ex As ArgumentException
                        Dim msg As String = String.Format("{0} ralsed exception :{1}", ex.Source, ex.Message)
                        MessageBox.Show(msg, "My App", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End Try

                    ReDim Preserve ForwMeetPt(ForwMeetPt.Length)
                Next
                ReDim Preserve ForwMeetPt(ForwMeetPt.Length - 2)

            Case 2
                ReDim BackMeetPt(0)
                KwPtErr = ""
                For i% = 0 To UnkwPtCount - 1
                    Dim PA As New ContrPoint
                    Dim PB As New ContrPoint
                    Dim PC As New ContrPoint
                    For j% = 0 To KnowPtCount - 1
                        If BackData(i).PtAname = KnowPt(j).Name Then
                            PA = KnowPt(j)
                            Continue For
                        End If
                        If BackData(i).PtBname = KnowPt(j).Name Then
                            PB = KnowPt(j)
                            Continue For
                        End If
                        If BackData(i).PtCname = KnowPt(j).Name Then
                            PC = KnowPt(j)
                            Continue For
                        End If
                    Next
                    If IsNothing(PA.Name) Or IsNothing(PB.Name) Or IsNothing(PC.Name) Then
                        MsgBox(String.Format("点{0}要求的已知点不存在！", BackData(i).PtName))
                        BackData(i).PtName &= "_DataErr"
                        KwPtErr &= i & ","
                        Continue For
                    End If
                    Dim BackPtCalcu As BackMeet
                    BackPtCalcu = New BackMeet(BackData(i).PtName, PA, PB, PC, CRad(BackData(i).AngA), CRad(BackData(i).AngB), CRad(BackData(i).AngC))
                    BackMeetPt(BackMeetPt.Length - 1).BackMeetPt = BackPtCalcu.PtP
                    BackMeetPt(BackMeetPt.Length - 1).DangerCir = BackPtCalcu.DangerCir
                    BackMeetPt(BackMeetPt.Length - 1).AuxiParam = BackPtCalcu.AuxiParam
                    If IputDataRadioBtn.Checked Then
                        HandMeetDic.Add(BackPtCalcu.PtP.Name, String.Format("{0:f3}_{1:f3}_{2}", BackPtCalcu.PtP.CoorX, BackPtCalcu.PtP.CoorY, BackPtCalcu.PtP.Type))
                    End If

                    Try
                        DrawSdDic.Add(PA.Name & "_" & PB.Name, New Side(PA, PB))
                    Catch ex As ArgumentException
                        Dim msg As String = String.Format("{0} ralsed exception :{1}", ex.Source, ex.Message)
                        MessageBox.Show(msg, "My App", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End Try

                    Try
                        DrawSdDic.Add(PB.Name & "_" & PC.Name, New Side(PB, PC))
                    Catch ex As ArgumentException
                        Dim msg As String = String.Format("{0} ralsed exception :{1}，{2}", ex.Source, ex.Message， ex.ParamName)
                        MessageBox.Show(msg, "My App", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End Try

                    Try
                        DrawSdDic.Add(PC.Name & "_" & PA.Name, New Side(PC, PA))
                    Catch ex As ArgumentException
                        Dim msg As String = String.Format("{0} ralsed exception :{1}，{2}", ex.Source, ex.Message， ex.ParamName)
                        MessageBox.Show(msg, "My App", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End Try

                    Try
                        DrawSdDic.Add(BackPtCalcu.PtP.Name & "_" & PA.Name, New Side(BackPtCalcu.PtP, PA))
                    Catch ex As ArgumentException
                        Dim msg As String = String.Format("{0} ralsed exception :{1}，{2}", ex.Source, ex.Message， ex.ParamName)
                        MessageBox.Show(msg, "My App", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End Try

                    Try
                        DrawSdDic.Add(BackPtCalcu.PtP.Name & "_" & PB.Name, New Side(BackPtCalcu.PtP, PB))
                    Catch ex As ArgumentException
                        Dim msg As String = String.Format("{0} ralsed exception :{1}，{2}", ex.Source, ex.Message， ex.ParamName)
                        MessageBox.Show(msg, "My App", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End Try

                    Try
                        DrawSdDic.Add(BackPtCalcu.PtP.Name & "_" & PC.Name, New Side(BackPtCalcu.PtP, PC))
                    Catch ex As ArgumentException
                        Dim msg As String = String.Format("{0} ralsed exception :{1}，{2}", ex.Source, ex.Message， ex.ParamName)
                        MessageBox.Show(msg, "My App", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End Try

                    ReDim Preserve BackMeetPt(BackMeetPt.Length)
                Next
                ReDim Preserve BackMeetPt(BackMeetPt.Length - 2)

            Case 3
                ReDim DistMeetPt(0)
                KwPtErr = ""
                For i% = 0 To UnkwPtCount - 1
                    Dim PA As New ContrPoint
                    Dim PB As New ContrPoint
                    For j% = 0 To KnowPtCount - 1
                        If DistData(i).PtAname = KnowPt(j).Name Then
                            PA = KnowPt(j)
                        End If
                        If DistData(i).PtBname = KnowPt(j).Name Then
                            PB = KnowPt(j)
                        End If
                    Next
                    If IsNothing(PA.Name) Or IsNothing(PB.Name) Then
                        MsgBox(String.Format("点{0}要求的已知点不存在！", DistData(i).PtAname))
                        DistData(i).PtAname *= "_DataErr"
                        KwPtErr &= i & ","
                        Continue For
                    End If
                    Dim DistPtCalcu As DistMeet
                    DistPtCalcu = New DistMeet(DistData(i).PtName, PA, PB, DistData(i).DisA, DistData(i).DisB)
                    DistMeetPt(DistMeetPt.Length - 1) = DistPtCalcu.PtP
                    If IputDataRadioBtn.Checked Then
                        HandMeetDic.Add(DistPtCalcu.PtP.Name, String.Format("{0:f3}_{1:f3}_{2}", DistPtCalcu.PtP.CoorX, DistPtCalcu.PtP.CoorY, DistPtCalcu.PtP.Type))
                    End If

                    Try
                        DrawSdDic.Add(PA.Name & "_" & PB.Name, New Side(PA, PB))
                    Catch ex As ArgumentException
                        Dim msg As String = String.Format("{0} ralsed exception :{1}", ex.Source, ex.Message)
                        MessageBox.Show(msg, "My App", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End Try

                    Try
                        DrawSdDic.Add(PA.Name & "_" & DistPtCalcu.PtP.Name, New Side(PA, DistPtCalcu.PtP))
                    Catch ex As ArgumentException
                        Dim msg As String = String.Format("{0} ralsed exception :{1}", ex.Source, ex.Message)
                        MessageBox.Show(msg, "My App", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End Try

                    Try
                        DrawSdDic.Add(PB.Name & "_" & DistPtCalcu.PtP.Name, New Side(PB, DistPtCalcu.PtP))
                    Catch ex As ArgumentException
                        Dim msg As String = String.Format("{0} ralsed exception :{1}", ex.Source, ex.Message)
                        MessageBox.Show(msg, "My App", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End Try
                    ReDim Preserve DistMeetPt(DistMeetPt.Length)
                Next
                ReDim Preserve DistMeetPt(DistMeetPt.Length - 2)
        End Select
    End Sub

#End Region
    Private Sub OutputMenuItem_Click(sender As Object, e As EventArgs) Handles OutputMenuItem.Click
        Dim ReportRst As String = OutTxtBx.Text
        OutputRst(ReportRst)
    End Sub

    Private Sub 退出MenuItem_Click(sender As Object, e As EventArgs) Handles EndMenuItem.Click
        End
    End Sub

    Private Sub 清空Button_Click(sender As Object, e As EventArgs) Handles Button2.Click
        ReDim KnowPt(0)
        ReDim ForwMeetPt(0)
        ReDim BackMeetPt(0)
        ReDim DistMeetPt(0)
        ReDim DataArray(0)
        ReDim DrawPt(0)

        HandMeetDic.Clear()
        DrawSdDic.Clear()
        KnowPtCount = 0
        UnkwPtCount = 0

        ForwStatus = 1
        BackStatus = 1
        DistStatus = 1

        For Each Item As KeyValuePair(Of TextBox, String) In TextBoxDic
            Item.Key.Text = ""
        Next
        OutTxtBx.Text = ""

        If CalProj = 1 Then
            SetText(CalProj)
            PictureBox1.Image = Image.FromFile(AppPath + "ForwMeet.bmp")
        ElseIf CalProj = 2 Then
            SetText(CalProj)
            PictureBox1.Image = Image.FromFile(AppPath + "BackMeet.bmp")
        ElseIf CalProj = 3 Then
            SetText(CalProj)
            PictureBox1.Image = Image.FromFile(AppPath + "DistMeet.bmp")
        End If
        KnowDataMenuItem.Enabled = True
        SurvDataMenuItem.Enabled = False

    End Sub

    Private Sub DrawMenuItem_Click(sender As Object, e As EventArgs) Handles DrawMenuItem.Click
        If FileDataRadioBtn.Checked Then
            Dim KnP% = KnowPt.Length
            ReDim DrawPt(KnP - 1)
            For i% = 0 To KnP - 1
                DrawPt(i) = New ContrPoint
                DrawPt(i) = KnowPt(i)
            Next
            If ForwStatus = 2 Then
                Dim FMP As Integer = ForwMeetPt.Length
                Dim PtCountBefore% = DrawPt.Length
                ReDim Preserve DrawPt(PtCountBefore% + FMP - 1)
                For i% = 0 To FMP - 1
                    DrawPt(i + PtCountBefore) = New ContrPoint
                    DrawPt(i + PtCountBefore) = ForwMeetPt(i)
                    'DrawPt(i + PtCountBefore).Type = 0
                Next
            End If
            If BackStatus = 2 Then
                Dim BMP As Integer = BackMeetPt.Length
                Dim PtCountBefore% = DrawPt.Length
                ReDim Preserve DrawPt(PtCountBefore + BMP - 1)
                For i% = 0 To BMP - 1
                    DrawPt(i + PtCountBefore) = New ContrPoint
                    DrawPt(i + PtCountBefore) = BackMeetPt(i).BackMeetPt
                    'DrawPt(i + PtCountBefore).Type = 0
                Next
            End If
            If DistStatus = 2 Then
                Dim DMP As Integer = DistMeetPt.Length
                Dim PtCountBefore% = DrawPt.Length
                ReDim Preserve DrawPt(PtCountBefore + DMP - 1)
                For i% = 0 To DMP - 1
                    DrawPt(i + PtCountBefore) = New ContrPoint
                    DrawPt(i + PtCountBefore) = DistMeetPt(i)
                    'DrawPt(i + PtCountBefore).Type = 0
                Next
            End If
ppp:
            For Each Item As KeyValuePair(Of String, Side) In DrawSdDic
                Dim Ptnam() As String = Item.Key.Split("_")
                For Each SubItem As KeyValuePair(Of String, Side) In DrawSdDic
                    If SubItem.Key = Ptnam(1) & "_" & Ptnam(0) Then
                        DrawSdDic.Remove(SubItem.Key)
                        GoTo ppp
                    End If
                Next
            Next
        Else
qqq:
            For Each PtItem As KeyValuePair(Of String, String) In HandMeetDic
                For Each SubPtItem As KeyValuePair(Of String, String) In HandMeetDic
                    If Not (SubPtItem.Key = PtItem.Key) And (SubPtItem.Value = PtItem.Value) Then
                        HandMeetDic.Add(PtItem.Key & "_" & SubPtItem.Key, PtItem.Value)
                        HandMeetDic.Remove(PtItem.Key)
                        HandMeetDic.Remove(SubPtItem.Key)
                        GoTo qqq
                    End If
                Next
            Next

            Dim AA() As List(Of String)
            Dim DrawPtDic As New Dictionary(Of String, ContrPoint)
            ReDim AA(0)
            ReDim DrawPt(0)
            For Each PtItem As KeyValuePair(Of String, String) In HandMeetDic
                Dim BB() As String = PtItem.Key.Split("_")
                AA(AA.Length - 1) = New List(Of String)
                For Each Str As String In BB
                    AA(AA.Length - 1).Add(Str)
                Next
                AA(AA.Length - 1).Sort()
                Dim PtValue() As String = PtItem.Value.Split("_")
                DrawPt(DrawPt.Length - 1) = New ContrPoint
                DrawPt(DrawPt.Length - 1).Name = AA(AA.Length - 1).Item(0)

                If PtValue(2) = "UnKnow" Then
                    DrawPt(DrawPt.Length - 1).Type = 0
                Else
                    DrawPt(DrawPt.Length - 1).Type = 1
                End If
                DrawPt(DrawPt.Length - 1).CoorX = CDbl(PtValue(0))
                DrawPt(DrawPt.Length - 1).CoorY = CDbl(PtValue(1))
                DrawPtDic.Add(DrawPt(DrawPt.Length - 1).Name, DrawPt(DrawPt.Length - 1))
                ReDim Preserve AA(AA.Length)
                ReDim Preserve DrawPt(DrawPt.Length)
            Next

            ReDim Preserve AA(AA.Length - 2)
            ReDim Preserve DrawPt(DrawPt.Length - 2)
            Dim NewDrawSdDic As New Dictionary(Of String, Side)
            For Each SdItem As KeyValuePair(Of String, Side) In DrawSdDic
                Dim Endpt() As String = SdItem.Key.Split("_")
                For i% = 0 To AA.Length - 1
                    For j% = 0 To AA.Length - 1
                        If AA(i).Contains(Endpt(0)) And AA(j).Contains(Endpt(1)) And Not (i = j) Then
                            If Not NewDrawSdDic.ContainsKey(AA(i).Item(0) & "_" & AA(j).Item(0)) Then
                                NewDrawSdDic.Add(AA(i).Item(0) & "_" & AA(j).Item(0), New Side(DrawPtDic(AA(i).Item(0)), DrawPtDic(AA(j).Item(0))))
                            End If
                        End If
                    Next
                Next
            Next
rrr:
            For Each Item As KeyValuePair(Of String, Side) In NewDrawSdDic
                Dim Ptnam() As String = Item.Key.Split("_")
                For Each SubItem As KeyValuePair(Of String, Side) In NewDrawSdDic
                    If SubItem.Key = Ptnam(1) & "_" & Ptnam(0) Then
                        NewDrawSdDic.Remove(SubItem.Key)
                        GoTo rrr
                    End If
                Next
            Next
            DrawSdDic = NewDrawSdDic
        End If

        Form_Draw.Show()
        Me.Hide()
    End Sub
End Class


