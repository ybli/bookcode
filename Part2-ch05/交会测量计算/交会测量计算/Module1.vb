Imports System.Math
Module Module1
    Function Azimuth(StaPt As ContrPoint, EndPt As ContrPoint) As Double
        Dim dX As Double = EndPt.CoorX - StaPt.CoorX
        Dim dY As Double = EndPt.CoorY - StaPt.CoorY
        If dX = 0 And dY = 0 Then
            MsgBox("两端点相同！请检查数据。", 0 + 16 + 0)
            End
        End If
        If dX = 0 Then
            If dY < 0 Then
                Return 3 * PI / 2
            ElseIf dY > 0 Then
                Return PI / 2
            End If
        ElseIf dY = 0 Then
            If dX < 0 Then
                Return PI
            ElseIf dX > 0 Then
                Return 0
            End If
        Else
            If dX < 0 Then
                Return Atan(dY / dX) + PI
            ElseIf dY < 0 Then
                Return Atan(dY / dX) + 2 * PI
            Else
                Return Atan(dY / dX)
            End If
        End If
    End Function
    Function Azimuth(StaPt As Point, EndPt As Point) As Double
        Dim dX As Double = EndPt.X - StaPt.X
        Dim dY As Double = EndPt.Y - StaPt.Y
        If dX = 0 And dY = 0 Then
            MsgBox("两端点相同！请检查数据。", 0 + 16 + 0)
            End
        End If
        If dX = 0 Then
            If dY < 0 Then
                Return 3 * PI / 2
            ElseIf dY > 0 Then
                Return PI / 2
            End If
        ElseIf dY = 0 Then
            If dX < 0 Then
                Return PI
            ElseIf dX > 0 Then
                Return 0
            End If
        Else
            If dX < 0 Then
                Return Atan(dY / dX) + PI
            ElseIf dY < 0 Then
                Return Atan(dY / dX) + 2 * PI
            Else
                Return Atan(dY / dX)
            End If
        End If
    End Function

    Function Distance(StaPt As ContrPoint, EndPt As ContrPoint) As Double
        Dim dX As Double = EndPt.CoorX - StaPt.CoorX
        Dim dY As Double = EndPt.CoorY - StaPt.CoorY
        Return Sqrt(dX * dX + dY * dY)
    End Function

    Function CRad#(ByVal AngDMS)
        Dim AngDEG As Double
        Dim MM As Double, SS As Double

        MM = Int((AngDMS - Int(AngDMS)) * 100)
        SS = ((AngDMS - Int(AngDMS)) * 100 - MM) * 100
        AngDEG = Int(AngDMS) + MM / 60 + SS / 3600
        CRad = AngDEG * PI / 180
    End Function

    Function Dmmss$(ByVal AngDMS)
        Dim DD As Integer, MM As Integer, SS As Single
        DD = Int(AngDMS)
        MM = Int((AngDMS - Int(AngDMS)) * 100)
        SS = ((AngDMS - Int(AngDMS)) * 100 - MM) * 100
        If SS.ToString("00.00") = "60.00" Then
            SS -= 0
            MM += 1
        End If
        If MM.ToString("00") = 60 Then
            MM -= 0
            DD += 1
        End If
        Return String.Format("{0}°{1}′{2}″", DD.ToString("#0"), MM.ToString("00"), SS.ToString("00.00"))
    End Function
    Friend Function InputData()
        Dim DataStr() As String
        Dim OpnFileDlg As New OpenFileDialog
        Dim DataString As String
        Dim ChkChar As Char
        OpnFileDlg.Filter = "Text Files|*.txt|All Files|*.*"
        OpnFileDlg.FilterIndex = 0
        If OpnFileDlg.ShowDialog = DialogResult.OK Then 'Public Function ShowDialog As System.Windows.Forms.DialogResult
            Dim StrmRd As New System.IO.StreamReader(OpnFileDlg.FileName)
            DataString = StrmRd.ReadToEnd
            StrmRd.Close()
        Else
            ReDim DataStr$(0)
            Return DataStr$
            Exit Function
        End If

        DataString = DataString.Replace(vbCrLf, ",")
        DataString = StrReverse(DataString)
        Do
            ChkChar = Mid(DataString, 1, 1)
            If ChkChar = "," Then
                DataString = Mid(DataString, 2, Len(DataString) - 1)
            End If
        Loop Until ChkChar <> ","
        DataString = StrReverse(DataString)
        DataStr$ = Split(DataString, ",")
        Return DataStr$

    End Function

    Friend Sub OutputRst(ReportString As String)
        Dim SavFileDlg As New SaveFileDialog
        SavFileDlg.Filter = "Text Files|*.txt|All Files|*.*"
        SavFileDlg.FilterIndex = 0
        If SavFileDlg.ShowDialog = DialogResult.OK Then
            Dim StrmWr As New System.IO.StreamWriter(SavFileDlg.OpenFile())
            StrmWr.Write(ReportString)
            StrmWr.Close()
        Else
            Exit Sub
        End If
    End Sub

End Module

Friend Structure ForwDataStru
    Dim PtName As String
    Dim PtAname As String
    Dim PtBname As String
    Dim AngA As Double
    Dim AngB As Double
End Structure

Friend Structure BackDataStru
    Dim PtName As String
    Dim PtAname As String
    Dim PtBname As String
    Dim PtCname As String
    Dim AngA As Double
    Dim AngB As Double
    Dim AngC As Double
End Structure

Friend Structure DistDataStru
    Dim PtName As String
    Dim PtAname As String
    Dim PtBname As String
    Dim DisA As Double
    Dim DisB As Double
End Structure

Friend Enum PointType
    UnKnow = 0
    Knowed = 1
End Enum

Friend Enum CalcuProj
    ForwMeet = 1
    BackMeet = 2
    DistMeet = 3
End Enum

Friend Enum CalcuStatus
    UnFins = 1
    Finish = 2
End Enum
'Class CalcuEventArgs
'    Inherits System.EventArgs
'    Property ErrInf As String
'End Class
