Attribute VB_Name = "Module1"
Public Declare Function ShellExecute Lib "shell32.dll" Alias "ShellExecuteA" (ByVal hwnd As Long, ByVal lpOperation As String, ByVal lpFile As String, ByVal lpParameters As String, ByVal lpDirectory As String, ByVal nShowCmd As Long) As Long
Public Const SW_SHOWNORMAL = 1
Public Type Coord_information
      Fif_time() As Double
      Fif_shuX() As Double
      Fif_shuY() As Double
      Fif_shuZ() As Double
      Fif_utc() As String
      Fiv_time() As Double
      Fiv_shuX() As Double
      Fiv_shuY() As Double
      Fiv_shuZ() As Double
      Fiv_utc() As String
End Type
Public Gsate() As Coord_information
Public Gplanet() As Coord_information

 Sub Zhuanzhi(B1() As Double, BT1() As Double)
      ''''''''''''''''''矩阵转置'''''''''''''''''''''''
      Dim i As Integer, j As Integer
      For i = 1 To UBound(BT1, 1)
            For j = 1 To UBound(BT1, 2)
                  BT1(i, j) = B1(j, i)
            Next j
      Next i
End Sub

Sub Mutliply(Mat1() As Double, Mat2() As Double, Rmat() As Double)
      ''''''''''''''''''矩阵相乘''''''''''''''''''''''''
      Dim i As Integer, j As Integer, k As Integer
      For i = 1 To UBound(Rmat, 1)
            For j = 1 To UBound(Rmat, 2)
                  Rmat(i, j) = 0
                  For k = 1 To UBound(Mat2, 1)
                        Rmat(i, j) = Rmat(i, j) + Mat1(i, k) * Mat2(k, j)
                  Next k
            Next j
      Next i
End Sub

Function MRinv(n As Integer, mtxA() As Double) As Boolean
      '''''''''''mtxA存放原矩阵A,返回时存放其逆矩阵''''''''''''
      ReDim nIs(n) As Integer, nJs(n) As Integer
      Dim i As Integer, j As Integer, k As Integer
      Dim d As Double, p As Double
      For k = 1 To n
            d = 0#
            For i = k To n
                  For j = k To n
                        p = Abs(mtxA(i, j))
                        If (p > d) Then
                              d = p
                              nIs(k) = i
                              nJs(k) = j
                        End If
                  Next j
            Next i
            ''''''''''''''''''''''求解失败''''''''''''''''''''''
            If (d + 1# = 1#) Then
                  MRinv = False
                  Exit Function
            End If
            If (nIs(k) <> k) Then
                  For j = 1 To n
                        p = mtxA(k, j)
                        mtxA(k, j) = mtxA(nIs(k), j)
                        mtxA(nIs(k), j) = p
                  Next j
            End If
            If (nJs(k) <> k) Then
                  For i = 1 To n
                        p = mtxA(i, k)
                        mtxA(i, k) = mtxA(i, nJs(k))
                        mtxA(i, nJs(k)) = p
                  Next i
            End If
            mtxA(k, k) = 1# / mtxA(k, k)
            For j = 1 To n
                  If (j <> k) Then mtxA(k, j) = mtxA(k, j) * mtxA(k, k)
            Next j
            For i = 1 To n
                  If (i <> k) Then
                        For j = 1 To n
                              If (j <> k) Then mtxA(i, j) = mtxA(i, j) - mtxA(i, k) * mtxA(k, j)
                        Next j
                  End If
            Next i
            For i = 1 To n
                  If (i <> k) Then mtxA(i, k) = -mtxA(i, k) * mtxA(k, k)
            Next i
      Next k
      ''''''''''''''''''''''''''' 调整恢复行列次序''''''''''''''''''''''
      For k = n To 1 Step -1
            If (nJs(k) <> k) Then
                  For j = 1 To n
                        p = mtxA(k, j)
                        mtxA(k, j) = mtxA(nJs(k), j)
                        mtxA(nJs(k), j) = p
                  Next j
            End If
            If (nIs(k) <> k) Then
                  For i = 1 To n
                        p = mtxA(i, k)
                        mtxA(i, k) = mtxA(i, nIs(k))
                        mtxA(i, nIs(k)) = p
                  Next i
            End If
      Next k
      '''''''''''''''''''''''''求解成功'''''''''''''''''''''''''''''''
      MRinv = True
End Function

 Sub IGS_Ephemeris_Fifteen(NL As Integer, GPS_sate() As Integer, UI As Integer)
      '''''''''''''''''''''''''''''''''读取IGS精密星历文件(15min)''''''''''''''''''''''''''''
      Dim year As Integer, month As Integer, day As Integer, hour As Integer
      Dim minute As Integer, second As Double, GPT1 As Double, GPST As String
      Dim KN As Integer, data As String, i As Integer
      Dim X_Coord As Double, Y_Coord As Double, Z_Coord As Double
      Dim Eshu() As Double
      ReDim Eshu(1 To 10000, 1 To 4)
      Dim T_shu() As String
      ReDim T_shu(1 To 10000)
      Dim fs As New FileSystemObject, txtf As TextStream
      Form1.CommonDialog1.Filter = "text(*.txt)|*.txt|Rich text files(*.rtf)|*.rtf|All files(*.*)|*.*"
      Form1.CommonDialog1.FilterIndex = 3
      Form1.CommonDialog1.ShowOpen
      If Form1.CommonDialog1.FileName = "" Then Exit Sub
      KN = 0
      Set txtf = fs.OpenTextFile(Form1.CommonDialog1.FileName)
      Do Until txtf.AtEndOfStream = True
            data = txtf.ReadLine
            If Mid(data, 1, 1) = "+" And Val(Mid(data, 5, 2)) <> 0 Then
                  NL = Val(Mid(data, 5, 2))
                  ReDim GPS_sate(1 To NL)
                  For i = 1 To 17
                        GPS_sate(i) = Val(Mid(data, (i - 1) * 3 + 11, 2))
                  Next i
            ElseIf Mid(data, 1, 2) = "+ " And Mid(data, 11, 2) <> 0 Then
                  For i = 18 To NL
                        GPS_sate(i) = Val(Mid(data, (i - 18) * 3 + 11, 2))
                  Next i
            ElseIf Mid(data, 1, 1) = "*" Then
                  year = Val(Mid(data, 4, 4)): month = Val(Mid(data, 9, 3)): day = Val(Mid(data, 12, 3))
                  hour = Val(Mid(data, 15, 3)): minute = Val(Mid(data, 17, 3)): second = Val(Mid(data, 20, 12))
                  '                     Call date2GPS(year, month, day, hour, minute, second, GPST)
                  GPST = Mid(data, 4, 21)
                  GPT1 = hour + minute / 60 + second / 3600
            ElseIf Mid(data, 1, 1) = "P" Then
                  X_Coord = Val(Mid(data, 5, 14)): Y_Coord = Val(Mid(data, 19, 14)): Z_Coord = Val(Mid(data, 33, 14))
                  KN = KN + 1
                  T_shu(KN) = GPST
                  Eshu(KN, 1) = GPT1: Eshu(KN, 2) = X_Coord * 1000: Eshu(KN, 3) = Y_Coord * 1000: Eshu(KN, 4) = Z_Coord * 1000
            End If
            DoEvents
      Loop
      txtf.Close
      UI = KN / NL
      ReDim Gsate(1 To NL)
      Dim j As Integer
      For i = 1 To NL
            ReDim Gsate(i).Fif_utc(1 To UI): ReDim Gsate(i).Fif_time(1 To UI)
            ReDim Gsate(i).Fif_shuX(1 To UI): ReDim Gsate(i).Fif_shuY(1 To UI): ReDim Gsate(i).Fif_shuZ(1 To UI)
            For j = 1 To UI
                  Gsate(i).Fif_utc(j) = T_shu(i + (j - 1) * NL)
                  Gsate(i).Fif_time(j) = Eshu(i + (j - 1) * NL, 1)
                  Gsate(i).Fif_shuX(j) = Eshu(i + (j - 1) * NL, 2)
                  Gsate(i).Fif_shuY(j) = Eshu(i + (j - 1) * NL, 3)
                  Gsate(i).Fif_shuZ(j) = Eshu(i + (j - 1) * NL, 4)
            Next j
            DoEvents
      Next i
End Sub

 Sub IGS_Ephemeris_Five(NS As Integer, GPS_planet() As Integer, PI As Integer)
      '''''''''''''''''''''''''''''''''读取IGS精密星历文件(5min)''''''''''''''''''''''''''''
      Dim nian As Integer, yue As Integer, ri As Integer, shi As Integer
      Dim fen As Integer, miao As Double, GPT2 As Double, GPS_T As String
      Dim PN As Integer, shuju As String, i As Integer
      Dim X_Coor As Double, Y_Coor As Double, Z_Coor As Double
      Dim fs As New FileSystemObject, txtf As TextStream
      Form1.CommonDialog2.Filter = "text(*.txt)|*.txt|Rich text files(*.rtf)|*.rtf|All files(*.*)|*.*"
      Form1.CommonDialog2.FilterIndex = 3
      Form1.CommonDialog2.ShowOpen
      If Form1.CommonDialog2.FileName = "" Then Exit Sub
      PN = 0
      Dim Edata() As Double
      ReDim Edata(1 To 10000, 1 To 4)
      Dim T_data() As String
      ReDim T_data(1 To 10000)
      Set txtf = fs.OpenTextFile(Form1.CommonDialog2.FileName)
      Do Until txtf.AtEndOfStream = True
            shuju = txtf.ReadLine
            If Mid(shuju, 1, 1) = "+" And Val(Mid(shuju, 5, 2)) <> 0 Then
                  NS = Val(Mid(shuju, 5, 2))
                  ReDim GPS_planet(1 To NS)
                  For i = 1 To 17
                        GPS_planet(i) = Val(Mid(shuju, (i - 1) * 3 + 11, 2))
                  Next i
            ElseIf Mid(shuju, 1, 2) = "+ " And Mid(shuju, 11, 2) <> 0 Then
                  For i = 18 To NS
                        GPS_planet(i) = Val(Mid(shuju, (i - 18) * 3 + 11, 2))
                  Next i
            ElseIf Mid(shuju, 1, 1) = "*" Then
                  nian = Val(Mid(shuju, 4, 4)): yue = Val(Mid(shuju, 9, 3)): ri = Val(Mid(shuju, 12, 3))
                  shi = Val(Mid(shuju, 15, 3)): fen = Val(Mid(shuju, 17, 3)): miao = Val(Mid(data, 20, 12))
                  '                     Call date2GPS(nian, yue, ri, shi, fen, miao, GPS_T)
                  GPS_T = Mid(shuju, 4, 21)
                  GPT2 = shi + fen / 60 + miao / 3600
            ElseIf Mid(shuju, 1, 1) = "P" Then
                  X_Coor = Val(Mid(shuju, 5, 14)): Y_Coor = Val(Mid(shuju, 19, 14)): Z_Coor = Val(Mid(shuju, 33, 14))
                  PN = PN + 1
                  T_data(PN) = GPS_T
                  Edata(PN, 1) = GPT2: Edata(PN, 2) = X_Coor * 1000: Edata(PN, 3) = Y_Coor * 1000: Edata(PN, 4) = Z_Coor * 1000
            End If
            DoEvents
      Loop
      txtf.Close
      PI = PN / NS
      ReDim Gplanet(1 To NS)
      Dim j As Integer
      For i = 1 To NS
            ReDim Gplanet(i).Fiv_utc(1 To PI): ReDim Gplanet(i).Fiv_time(1 To PI)
            ReDim Gplanet(i).Fiv_shuX(1 To PI): ReDim Gplanet(i).Fiv_shuY(1 To PI): ReDim Gplanet(i).Fiv_shuZ(1 To PI)
            For j = 1 To PI
                  Gplanet(i).Fiv_utc(j) = T_data(i + (j - 1) * NS)
                  Gplanet(i).Fiv_time(j) = Edata(i + (j - 1) * NS, 1)
                  Gplanet(i).Fiv_shuX(j) = Edata(i + (j - 1) * NS, 2)
                  Gplanet(i).Fiv_shuY(j) = Edata(i + (j - 1) * NS, 3)
                  Gplanet(i).Fiv_shuZ(j) = Edata(i + (j - 1) * NS, 4)
            Next j
            DoEvents
      Next i
End Sub

Sub Max_Zhi(Cha_zhi() As Double, Error_Max As Double)
     Dim i As Integer, zhi As Double
     For i = 1 To UBound(Cha_zhi, 1)
         zhi = Abs(Cha_zhi(i))
         If zhi > Error_Max Then
            Error_Max = zhi
         End If
    Next i
End Sub

Sub Min_Zhi(Cha_zhi() As Double, Error_Min As Double)
     Dim i As Integer, zhi As Double
     Error_Min = Abs(Cha_zhi(1))
     For i = 1 To UBound(Cha_zhi, 1)
         zhi = Abs(Cha_zhi(i))
         If zhi < Error_Min Then
            Error_Min = zhi
         End If
    Next i
End Sub

