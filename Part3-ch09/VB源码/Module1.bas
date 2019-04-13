Attribute VB_Name = "Module1"
Option Explicit
Public Declare Function ShellExecute Lib "shell32.dll" Alias "ShellExecuteA" (ByVal hwnd As Long, ByVal lpOperation As String, ByVal lpFile As String, ByVal lpParameters As String, ByVal lpDirectory As String, ByVal nShowCmd As Long) As Long
Public Const SW_SHOWNORMAL = 1

Sub Read_Data(Data_zhi() As String, shu() As String)
      Dim fs As New FileSystemObject, txtf As TextStream
      Form1.CommonDialog1.Filter = "text(*.txt)|*.txt|Rich text files(*.rtf)|*.rtf|All files(*.*)|*.*"
      Form1.CommonDialog1.FilterIndex = 3
      Form1.CommonDialog1.ShowOpen
      If Form1.CommonDialog1.FileName = "" Then Exit Sub
      Set txtf = fs.OpenTextFile(Form1.CommonDialog1.FileName)
      Dim Data As String, shuzhi() As String
      ReDim shu(1 To 10000)
      Dim SN As Integer, NS As Integer
      ReDim Data_zhi(1 To 10000, 1 To 4)
      Do Until txtf.AtEndOfStream = True
            Data = txtf.ReadLine
            SN = SN + 1
            shu(SN) = Data
            shuzhi = Split(Trim(shu(SN)), ",")
            If UBound(shuzhi) = 3 Then
                NS = NS + 1
                Data_zhi(NS, 1) = shuzhi(0)
                Data_zhi(NS, 2) = shuzhi(1)
                Data_zhi(NS, 3) = shuzhi(2)
                Data_zhi(NS, 4) = shuzhi(3)
            End If
      Loop
      txtf.Close
End Sub

Sub Azith_cal(XCoord_A As Double, YCoord_A As Double, XCoord_B As Double, YCoord_B As Double, K_Azith As Double)
     ''''''''''''坐标方位角反算(以度为单位)''''''''''
     K_Azith = Atn(Abs((YCoord_B - YCoord_A) / (XCoord_B - XCoord_A))) * 180 / (4 * Atn(1))
     If (YCoord_B - YCoord_A) > 0 And (XCoord_B - XCoord_A) > 0 Then
         K_Azith = K_Azith
     ElseIf (YCoord_B - YCoord_A) > 0 And (XCoord_B - XCoord_A) < 0 Then
         K_Azith = 180 - K_Azith
     ElseIf (YCoord_B - YCoord_A) < 0 And (XCoord_B - XCoord_A) < 0 Then
         K_Azith = 180 + K_Azith
     ElseIf (YCoord_B - YCoord_A) < 0 And (XCoord_B - XCoord_A) > 0 Then
         K_Azith = 360 - K_Azith
     ElseIf (YCoord_B - YCoord_A) > 0 And (XCoord_B - XCoord_A) = 0 Then
         K_Azith = 90
     ElseIf (YCoord_B - YCoord_A) < 0 And (XCoord_B - XCoord_A) = 0 Then
         K_Azith = 270
     End If
End Sub

Sub Degree_transfer(Degree_Res As Double, Deg_zhi As Double, Min_zhi As Double, Sec_zhi As Double)
    Deg_zhi = Int(Degree_Res)
    Min_zhi = Int((Degree_Res - Deg_zhi) * 60)
    Sec_zhi = ((Degree_Res - Deg_zhi) * 60 - Min_zhi) * 60
    If Abs(Sec_zhi - Int(Sec_zhi) - 1) <= 10 ^ (-4) Then
        Sec_zhi = Int(Sec_zhi) + 1
        If Sec_zhi <= 10 ^ (-4) Then
           Sec_zhi = 0
        ElseIf Abs(Sec_zhi - 60) <= 10 ^ (-4) Then
           Min_zhi = Min_zhi + 1
           Sec_zhi = 0
        End If
    ElseIf Abs(Sec_zhi - Int(Sec_zhi)) <= 10 ^ (-4) Then
        Sec_zhi = Int(Sec_zhi)
    ElseIf Sec_zhi <= 10 ^ (-4) Then
           Sec_zhi = 0
    ElseIf Abs(Sec_zhi - 60) <= 10 ^ (-4) Then
           Min_zhi = Min_zhi + 1
           Sec_zhi = 0
    End If
    If Abs(Min_zhi - Int(Min_zhi) - 1) <= 10 ^ (-4) Then
       Min_zhi = Int(Min_zhi) + 1
       If Abs(Min_zhi - 60) <= 10 ^ (-4) Then
          Deg_zhi = Deg_zhi + 1
          Min_zhi = 0
       ElseIf Min_zhi <= 10 ^ (-4) Then
           Min_zhi = 0
       End If
    ElseIf Abs(Min_zhi - 60) <= 10 ^ (-4) Then
          Deg_zhi = Deg_zhi + 1
          Min_zhi = 0
    ElseIf Min_zhi <= 10 ^ (-4) Then
          Min_zhi = 0
    End If
End Sub

Sub Coord_Compute(Angle As Double, L_Dist As Double, Coor_X0 As Double, Coor_Y0 As Double, Coor_X1 As Double, Coor_Y1 As Double)
    Coor_X1 = Coor_X0 + L_Dist * Cos(Angle * 4 * Atn(1) / 180)
    Coor_Y1 = Coor_Y0 + L_Dist * Sin(Angle * 4 * Atn(1) / 180)
End Sub

Sub Coordniate_Compute(Azith As Double, L_Dist As Double, D_Dist As Double, Coor_XJ As Double, Coor_YJ As Double, Coor_Xi As Double, Coor_Yi As Double)
    Coor_Xi = Coor_XJ + (L_Dist - D_Dist) * Cos(Azith * 4 * Atn(1) / 180)
    Coor_Yi = Coor_YJ + (L_Dist - D_Dist) * Sin(Azith * 4 * Atn(1) / 180)
End Sub

Sub Paixu(N As Integer, A_mat() As Double)
     Dim i As Integer, j As Integer, T_mat As Double
     For i = 1 To N - 1
         For j = 1 To N - 1
             If A_mat(j) > A_mat(j + 1) Then
               T_mat = A_mat(j): A_mat(j) = A_mat(j + 1): A_mat(j + 1) = T_mat
             End If
         Next j
     Next i
End Sub

Sub Interplate_Height(Dist() As Double, Dis() As Double, Point_Data() As String, Inter_K_H As Double)
     Dim m As Integer
     m = UBound(Dis)
     Call Paixu(m, Dis())
     Dim i As Integer, j As Integer
     Dim num As Integer
     Dim Point_name()  As String
     ReDim Point_name(1 To m)
     Dim Sd_Data() As Double
     ReDim Sd_Data(1 To m, 1 To 4)
     For i = 1 To 5
        num = num + 1
        For j = 1 To m
            If Dis(i) = Dist(j) Then
                 Point_name(num) = Point_Data(j, 1)
                 Sd_Data(num, 1) = Val(Point_Data(j, 2))
                 Sd_Data(num, 2) = Val(Point_Data(j, 3))
                 Sd_Data(num, 3) = Val(Point_Data(j, 4))
                 Sd_Data(num, 4) = Dist(j)
            End If
        Next j
     Next i
     Dim sum_hd As Double, sum_d As Double
     For i = 1 To 5
        sum_hd = sum_hd + (Sd_Data(i, 3) / Sd_Data(i, 4))
        sum_d = sum_d + (1 / Sd_Data(i, 4))
     Next i
    
     Inter_K_H = sum_hd / sum_d
End Sub

