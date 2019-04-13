VERSION 5.00
Object = "{F9043C88-F6F2-101A-A3C9-08002B2F49FB}#1.2#0"; "COMDLG32.OCX"
Object = "{831FDD16-0C5C-11D2-A9FC-0000F8754DA1}#2.0#0"; "MSCOMCTL.OCX"
Begin VB.Form Form1 
   BackColor       =   &H00C0FFC0&
   Caption         =   "内插和拟合算法计算卫星轨道"
   ClientHeight    =   4884
   ClientLeft      =   192
   ClientTop       =   816
   ClientWidth     =   7260
   LinkTopic       =   "Form1"
   ScaleHeight     =   4884
   ScaleWidth      =   7260
   StartUpPosition =   3  '窗口缺省
   Begin MSComctlLib.StatusBar StatusBar1 
      Align           =   2  'Align Bottom
      Height          =   372
      Left            =   0
      TabIndex        =   0
      Top             =   4512
      Width           =   7260
      _ExtentX        =   12806
      _ExtentY        =   656
      _Version        =   393216
      BeginProperty Panels {8E3867A5-8586-11D1-B16A-00C0F0283628} 
         NumPanels       =   4
         BeginProperty Panel1 {8E3867AB-8586-11D1-B16A-00C0F0283628} 
            Style           =   6
            Alignment       =   1
            Object.Width           =   2117
            MinWidth        =   2117
            TextSave        =   "2018/12/17"
         EndProperty
         BeginProperty Panel2 {8E3867AB-8586-11D1-B16A-00C0F0283628} 
            Style           =   5
            Alignment       =   1
            Object.Width           =   2822
            MinWidth        =   2822
            Picture         =   "Form1.frx":0000
            TextSave        =   "9:26"
         EndProperty
         BeginProperty Panel3 {8E3867AB-8586-11D1-B16A-00C0F0283628} 
            Alignment       =   1
            Object.Width           =   4586
            MinWidth        =   4586
            Text            =   "欢迎使用卫星轨道计算程序"
            TextSave        =   "欢迎使用卫星轨道计算程序"
            Object.ToolTipText     =   "欢迎使用卫星轨道计算程序"
         EndProperty
         BeginProperty Panel4 {8E3867AB-8586-11D1-B16A-00C0F0283628} 
            Style           =   1
            Alignment       =   2
            Enabled         =   0   'False
            Object.Width           =   2822
            MinWidth        =   2822
            TextSave        =   "CAPS"
         EndProperty
      EndProperty
      BeginProperty Font {0BE35203-8F91-11CE-9DE3-00AA004BB851} 
         Name            =   "宋体"
         Size            =   9
         Charset         =   134
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
   End
   Begin MSComDlg.CommonDialog CommonDialog2 
      Left            =   6000
      Top             =   3600
      _ExtentX        =   847
      _ExtentY        =   847
      _Version        =   393216
   End
   Begin MSComDlg.CommonDialog CommonDialog1 
      Left            =   600
      Top             =   3480
      _ExtentX        =   847
      _ExtentY        =   847
      _Version        =   393216
   End
   Begin VB.Menu Precise_Ephemeris 
      Caption         =   "精密星历"
      WindowList      =   -1  'True
      Begin VB.Menu Read_Ephemeris_Fifteen 
         Caption         =   "读取精密星历数据(15MIN)"
      End
      Begin VB.Menu Read_Ephemeris_Five 
         Caption         =   "读取精密星历数据结果(5MIN)"
      End
   End
   Begin VB.Menu Interpolation_fitting 
      Caption         =   "内插和拟合算法"
      Begin VB.Menu Lagrange 
         Caption         =   "Lagrange插值"
      End
      Begin VB.Menu Neville 
         Caption         =   "Neville插值"
      End
      Begin VB.Menu Chebyshev 
         Caption         =   "Chebyshev拟合"
      End
      Begin VB.Menu Legendre 
         Caption         =   "Legendre拟合"
      End
   End
   Begin VB.Menu Accuracy_Evaluation 
      Caption         =   "精度评定"
      Begin VB.Menu Mean_Square_Error 
         Caption         =   "中误差(X_Y_Z)"
      End
      Begin VB.Menu Maximum_Error 
         Caption         =   "最大误差(X_Y_Z)"
      End
      Begin VB.Menu Minimum_Error 
         Caption         =   "最小误差(X_Y_Z)"
      End
   End
   Begin VB.Menu Help 
      Caption         =   "帮助"
      Begin VB.Menu Using_Document 
         Caption         =   "使用文档"
      End
   End
   Begin VB.Menu Exit_Program 
      Caption         =   "退出"
   End
End
Attribute VB_Name = "Form1"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Option Explicit
Dim IU As Integer
Dim IP As Integer
Dim Lagrange_Coord() As Double
Dim Neville_Coord() As Double
Dim Chebyshev_Coord() As Double
Dim Legendre_Coord() As Double

Private Sub Read_Ephemeris_Fifteen_Click()
      Dim NL As Integer
      Dim sate_G() As Integer
      Call IGS_Ephemeris_Fifteen(NL, sate_G(), IU)
End Sub

Private Sub Read_Ephemeris_Five_Click()
      Dim NS As Integer
      Dim planet_G() As Integer
      Call IGS_Ephemeris_Five(NS, planet_G(), IP)
End Sub

Private Sub Lagrange_Click()
      '''''''''''''以PRN2号卫星为例，选取0时0分0秒-4时0分0秒的数据进行试验''''''''''''''
      Dim tran_zhi As Double
      Dim xzhi As Double
      Dim yzhi As Double
      Dim zzhi As Double
      ReDim Lagrange_Coord(1 To 49, 1 To 3)
      Dim i As Integer, j As Integer, k As Integer
      Open App.Path & "\result\Lagrange_Result.txt" For Output As #4
      Print #4, String(10, " ") & "世界时" & String(10, " ") & "卫星轨道X分量" & String(5, " ") & "卫星轨道Y分量" & String(5, " ") & "卫星轨道Z分量"
      '''''''''''''以PRN2号卫星为例，选取0时0分0秒-4时0分0秒的数据进行试验''''''''''''''
      For i = 1 To 49
            xzhi = 0
            yzhi = 0
            zzhi = 0
            For j = 1 To 17
                  tran_zhi = 1
                  For k = 1 To 17
                        If j <> k Then
                              tran_zhi = tran_zhi * (Gplanet(2).Fiv_time(i) - Gsate(2).Fif_time(k)) / (Gsate(2).Fif_time(j) - Gsate(2).Fif_time(k))
                        End If
                  Next k
                  xzhi = xzhi + tran_zhi * Gsate(2).Fif_shuX(j)
                  yzhi = yzhi + tran_zhi * Gsate(2).Fif_shuY(j)
                  zzhi = zzhi + tran_zhi * Gsate(2).Fif_shuZ(j)
            Next j
            Lagrange_Coord(i, 1) = xzhi: Lagrange_Coord(i, 2) = yzhi: Lagrange_Coord(i, 3) = zzhi
            Print #4, Gplanet(2).Fiv_utc(i) & String(4, " ") & Format(Lagrange_Coord(i, 1), "0.00000") & Space(14 - Len(CStr(Format(Lagrange_Coord(i, 1), "0.00000")))) & String(4, " ") & Format(Lagrange_Coord(i, 2), "0.00000") & Space(14 - Len(CStr(Format(Lagrange_Coord(i, 2), "0.00000")))) & String(4, " ") & Format(Lagrange_Coord(i, 3), "0.00000")
      Next i
      Close #4
End Sub

Private Sub Neville_Click()
       '''''''''''''以PRN2号卫星为例，选取0时0分0秒-4时0分0秒的数据进行试验''''''''''''''
      Dim Nev_X() As Double
      Dim Nev_Y() As Double
      Dim Nev_Z() As Double
      ReDim Nev_X(0 To 16, 0 To 16)
      ReDim Nev_Y(0 To 16, 0 To 16)
      ReDim Nev_Z(0 To 16, 0 To 16)
      ReDim Neville_Coord(1 To 49, 1 To 3)
      Dim i As Integer, j As Integer, k As Integer
      Open App.Path & "\result\Neville_Result.txt" For Output As #5
      Print #5, String(10, " ") & "世界时" & String(10, " ") & "卫星轨道X分量" & String(5, " ") & "卫星轨道Y分量" & String(5, " ") & "卫星轨道Z分量"
      For i = 1 To 49
            For j = 0 To 16
                  Nev_X(j, 0) = Gsate(2).Fif_shuX(j + 1)
                  Nev_Y(j, 0) = Gsate(2).Fif_shuY(j + 1)
                  Nev_Z(j, 0) = Gsate(2).Fif_shuZ(j + 1)
                   If j <> 0 Then
                        For k = 1 To j
                              Nev_X(j, k) = ((Gplanet(2).Fiv_time(i) - Gsate(2).Fif_time(j + 1)) * Nev_X(j - 1, k - 1) - (Gplanet(2).Fiv_time(i) - Gsate(2).Fif_time(j - k + 1)) * Nev_X(j, k - 1)) / (Gsate(2).Fif_time(j - k + 1) - Gsate(2).Fif_time(j + 1))
                              Nev_Y(j, k) = ((Gplanet(2).Fiv_time(i) - Gsate(2).Fif_time(j + 1)) * Nev_Y(j - 1, k - 1) - (Gplanet(2).Fiv_time(i) - Gsate(2).Fif_time(j - k + 1)) * Nev_Y(j, k - 1)) / (Gsate(2).Fif_time(j - k + 1) - Gsate(2).Fif_time(j + 1))
                              Nev_Z(j, k) = ((Gplanet(2).Fiv_time(i) - Gsate(2).Fif_time(j + 1)) * Nev_Z(j - 1, k - 1) - (Gplanet(2).Fiv_time(i) - Gsate(2).Fif_time(j - k + 1)) * Nev_Z(j, k - 1)) / (Gsate(2).Fif_time(j - k + 1) - Gsate(2).Fif_time(j + 1))
                        Next k
                    End If
            Next j
            Print #5, Gplanet(2).Fiv_utc(i) & String(4, " ") & Format(Nev_X(16, 16), "0.00000") & Space(14 - Len(CStr(Format(Nev_X(16, 16), "0.00000")))) & String(4, " ") & Format(Nev_Y(16, 16), "0.00000") & Space(14 - Len(CStr(Format(Nev_Y(16, 16), "0.00000")))) & String(4, " ") & Format(Nev_Z(16, 16), "0.00000")
            Neville_Coord(i, 1) = Nev_X(16, 16): Neville_Coord(i, 2) = Nev_Y(16, 16): Neville_Coord(i, 3) = Nev_Z(16, 16)
      Next i
      Close #5
End Sub

Private Sub Chebyshev_Click()
      '''''''''''''以PRN2号卫星为例，选取0时0分0秒-4时0分0秒的数据进行试验''''''''''''''
      Dim rank As Integer
      rank = 14
      Dim i As Integer
      Dim tao() As Double
      ReDim tao(1 To 17)
      For i = 1 To 17
            tao(i) = 2 * (Gsate(2).Fif_time(i) - Gsate(2).Fif_time(1)) / (Gsate(2).Fif_time(17) - Gsate(2).Fif_time(1)) - 1
      Next i
      Dim coe() As Double
      ReDim coe(1 To 17, 1 To rank)
      Dim j As Integer
      For i = 1 To 17
            For j = 1 To rank
                  If j = 1 Then
                        coe(i, j) = 1
                  ElseIf j = 2 Then
                        coe(i, 2) = tao(i)
                  Else
                        coe(i, j) = 2 * tao(i) * coe(i, j - 1) - coe(i, j - 2)
                  End If
            Next j
      Next i
      Dim xcoor() As Double, ycoor() As Double, zcoor() As Double
      ReDim xcoor(1 To 17, 1 To 1): ReDim ycoor(1 To 17, 1 To 1): ReDim zcoor(1 To 17, 1 To 1)
      For i = 1 To 17
            xcoor(i, 1) = Gsate(2).Fif_shuX(i)
            ycoor(i, 1) = Gsate(2).Fif_shuY(i)
            zcoor(i, 1) = Gsate(2).Fif_shuZ(i)
      Next i
      Dim NS As Integer
      NS = UBound(coe, 1)
      Dim ms As Integer
      ms = UBound(coe, 2)
      Dim coe_T() As Double
      ReDim coe_T(1 To ms, 1 To NS)
      Call Zhuanzhi(coe(), coe_T())
      Dim CTC() As Double
      ReDim CTC(1 To ms, 1 To ms)
      Call Mutliply(coe_T(), coe(), CTC())
      Dim InvR As Boolean
      InvR = MRinv(ms, CTC())
      Dim INVC() As Double
      ReDim INVC(1 To ms, 1 To NS)
      Call Mutliply(CTC(), coe_T(), INVC())
      Dim AX1() As Double
      ReDim AX1(1 To ms, 1 To 1)
      Call Mutliply(INVC(), xcoor(), AX1())      '计算X方向的拟合系数
      Dim AY1() As Double
      ReDim AY1(1 To ms, 1 To 1)
      Call Mutliply(INVC(), ycoor(), AY1())      '计算Y方向的拟合系数
      Dim AZ1() As Double
      ReDim AZ1(1 To ms, 1 To 1) As Double
      Call Mutliply(INVC(), zcoor(), AZ1())      '计算Z方向的拟合系数
      '''''''''''''''''''拟合计算'''''''''''''''''
      Dim tao1(1 To 49) As Double
      For i = 1 To 49
            tao1(i) = 2 * (Gplanet(2).Fiv_time(i) - Gplanet(2).Fiv_time(1)) / (Gplanet(2).Fiv_time(49) - Gplanet(2).Fiv_time(1)) - 1
      Next i
      Dim coe_che() As Double
      ReDim coe_che(1 To 49, 1 To rank)
      For i = 1 To 49
            For j = 1 To rank
                  If j = 1 Then
                        coe_che(i, j) = 1
                  ElseIf j = 2 Then
                        coe_che(i, 2) = tao1(i)
                  Else
                        coe_che(i, j) = 2 * tao1(i) * coe_che(i, j - 1) - coe_che(i, j - 2)
                  End If
            Next j
      Next i
      Dim cheb_X() As Double
      ReDim cheb_X(1 To 49, 1 To 1)
      Call Mutliply(coe_che(), AX1(), cheb_X())
      Dim cheb_Y() As Double
      ReDim cheb_Y(1 To 49, 1 To 1)
      Call Mutliply(coe_che(), AY1(), cheb_Y())
      Dim cheb_Z() As Double
      ReDim cheb_Z(1 To 49, 1 To 1)
      Call Mutliply(coe_che(), AZ1(), cheb_Z())
      ReDim Chebyshev_Coord(1 To 49, 1 To 3)
      Open App.Path & "\result\Chebyshev_Result.txt" For Output As #6
      Print #6, String(10, " ") & "世界时" & String(10, " ") & "卫星轨道X分量" & String(5, " ") & "卫星轨道Y分量" & String(5, " ") & "卫星轨道Z分量"
      For i = 1 To 49
            Print #6, Gplanet(2).Fiv_utc(i) & String(4, " ") & Format(cheb_X(i, 1), "0.00000") & Space(14 - Len(CStr(Format(cheb_X(i, 1), "0.00000")))) & String(4, " ") & Format(cheb_Y(i, 1), "0.00000") & Space(14 - Len(CStr(Format(cheb_Y(i, 1), "0.00000")))) & String(4, " ") & Format(cheb_Z(i, 1), "0.00000")
            Chebyshev_Coord(i, 1) = cheb_X(i, 1): Chebyshev_Coord(i, 2) = cheb_Y(i, 1): Chebyshev_Coord(i, 3) = cheb_Z(i, 1)
      Next i
      Close #6
End Sub

Private Sub Legendre_Click()
      '''''''''''''以PRN2号卫星为例，选取0时0分0秒-4时0分0秒的数据进行试验''''''''''''''
      Dim order As Integer
      order = 14
      Dim i As Integer
      Dim tao() As Double
      ReDim tao(1 To 17)
      For i = 1 To 17
            tao(i) = 2 * (Gsate(2).Fif_time(i) - Gsate(2).Fif_time(1)) / (Gsate(2).Fif_time(17) - Gsate(2).Fif_time(1)) - 1
      Next i
      Dim coef() As Double
      ReDim coef(1 To 17, 1 To order)
      Dim j As Integer
      For i = 1 To 17
            For j = 1 To order
                  If j = 1 Then
                        coef(i, j) = 1
                  ElseIf j = 2 Then
                        coef(i, 2) = tao(i)
                  Else
                        coef(i, j) = (2 * (j - 1) - 1) / (j - 1) * tao(i) * coef(i, j - 1) - (j - 2) / (j - 1) * coef(i, j - 2)
                  End If
            Next j
      Next i
      Dim xcoord() As Double, ycoord() As Double, zcoord() As Double
      ReDim xcoord(1 To 17, 1 To 1): ReDim ycoord(1 To 17, 1 To 1): ReDim zcoord(1 To 17, 1 To 1)
      For i = 1 To 17
            xcoord(i, 1) = Gsate(2).Fif_shuX(i)
            ycoord(i, 1) = Gsate(2).Fif_shuY(i)
            zcoord(i, 1) = Gsate(2).Fif_shuZ(i)
      Next i
      Dim NS As Integer
      NS = UBound(coef, 1)
      Dim ms As Integer
      ms = UBound(coef, 2)
      Dim coef_T() As Double
      ReDim coef_T(1 To ms, 1 To NS)
      Call Zhuanzhi(coef(), coef_T())
      Dim CTC() As Double
      ReDim CTC(1 To ms, 1 To ms)
      Call Mutliply(coef_T(), coef(), CTC())
      Dim InvR As Boolean
      InvR = MRinv(ms, CTC())
      Dim INVC() As Double
      ReDim INVC(1 To ms, 1 To NS)
      Call Mutliply(CTC(), coef_T(), INVC())
      Dim AX1() As Double
      ReDim AX1(1 To ms, 1 To 1)
      Call Mutliply(INVC(), xcoord(), AX1())      '计算X方向的拟合系数
      Dim AY1() As Double
      ReDim AY1(1 To ms, 1 To 1)
      Call Mutliply(INVC(), ycoord(), AY1())      '计算Y方向的拟合系数
      Dim AZ1() As Double
      ReDim AZ1(1 To ms, 1 To 1) As Double
      Call Mutliply(INVC(), zcoord(), AZ1())      '计算Z方向的拟合系数
      '''''''''''''''''''拟合计算'''''''''''''''''
      Dim tao1(1 To 49) As Double
      For i = 1 To 49
            tao1(i) = 2 * (Gplanet(2).Fiv_time(i) - Gplanet(2).Fiv_time(1)) / (Gplanet(2).Fiv_time(49) - Gplanet(2).Fiv_time(1)) - 1
      Next i
      Dim coe_che() As Double
      ReDim coe_che(1 To 49, 1 To order)
      For i = 1 To 49
            For j = 1 To order
                  If j = 1 Then
                        coe_che(i, j) = 1
                  ElseIf j = 2 Then
                        coe_che(i, 2) = tao1(i)
                  Else
                        coe_che(i, j) = (2 * (j - 1) - 1) / (j - 1) * tao1(i) * coe_che(i, j - 1) - (j - 2) / (j - 1) * coe_che(i, j - 2)
                  End If
            Next j
      Next i
      Dim Lege_X() As Double
      ReDim Lege_X(1 To 49, 1 To 1)
      Call Mutliply(coe_che(), AX1(), Lege_X())
      Dim Lege_Y() As Double
      ReDim Lege_Y(1 To 49, 1 To 1)
      Call Mutliply(coe_che(), AY1(), Lege_Y())
      Dim Lege_Z() As Double
      ReDim Lege_Z(1 To 49, 1 To 1)
      Call Mutliply(coe_che(), AZ1(), Lege_Z())
      Open App.Path & "\result\Legendre_Result.txt" For Output As #7
      Print #7, String(10, " ") & "世界时" & String(10, " ") & "卫星轨道X分量" & String(5, " ") & "卫星轨道Y分量" & String(5, " ") & "卫星轨道Z分量"
      ReDim Legendre_Coord(1 To 49, 1 To 3)
      For i = 1 To 49
            Print #7, Gplanet(2).Fiv_utc(i) & String(4, " ") & Format(Lege_X(i, 1), "0.00000") & Space(14 - Len(CStr(Format(Lege_X(i, 1), "0.00000")))) & String(4, " ") & Format(Lege_Y(i, 1), "0.00000") & Space(14 - Len(CStr(Format(Lege_Y(i, 1), "0.00000")))) & String(4, " ") & Format(Lege_Z(i, 1), "0.00000")
            Legendre_Coord(i, 1) = Lege_X(i, 1): Legendre_Coord(i, 2) = Lege_Y(i, 1): Legendre_Coord(i, 3) = Lege_Z(i, 1)
      Next i
      Close #7
End Sub

Private Sub Mean_Square_Error_Click()
      '''''''''''''''''四种不同算法的精度评定'''''''''''''
      Dim i As Integer
      Dim La_Merror(1 To 49, 1 To 3) As Double
      Dim Ne_Merror(1 To 49, 1 To 3) As Double
      Dim Ch_Merror(1 To 49, 1 To 3) As Double
      Dim Le_Merror(1 To 49, 1 To 3) As Double
      Dim La_RMS(1 To 3) As Double
      Dim Ne_RMS(1 To 3) As Double
      Dim Ch_RMS(1 To 3) As Double
      Dim Le_RMS(1 To 3) As Double
      Open App.Path & "\result\Mean_Error.txt" For Output As #8
      For i = 1 To 49
            '''''''''''''''Lagrange插值'''''''''''''''
            La_Merror(i, 1) = Lagrange_Coord(i, 1) - Gplanet(2).Fiv_shuX(i)
            La_Merror(i, 2) = Lagrange_Coord(i, 2) - Gplanet(2).Fiv_shuY(i)
            La_Merror(i, 3) = Lagrange_Coord(i, 3) - Gplanet(2).Fiv_shuZ(i)
            La_RMS(1) = La_RMS(1) + La_Merror(i, 1) * La_Merror(i, 1)
            La_RMS(2) = La_RMS(2) + La_Merror(i, 2) * La_Merror(i, 2)
            La_RMS(3) = La_RMS(3) + La_Merror(i, 3) * La_Merror(i, 3)
            '''''''''''''''Neville插值'''''''''''''''
            Ne_Merror(i, 1) = Neville_Coord(i, 1) - Gplanet(2).Fiv_shuX(i)
            Ne_Merror(i, 2) = Neville_Coord(i, 2) - Gplanet(2).Fiv_shuY(i)
            Ne_Merror(i, 3) = Neville_Coord(i, 3) - Gplanet(2).Fiv_shuZ(i)
            Ne_RMS(1) = Ne_RMS(1) + Ne_Merror(i, 1) * Ne_Merror(i, 1)
            Ne_RMS(2) = Ne_RMS(2) + Ne_Merror(i, 2) * Ne_Merror(i, 2)
            Ne_RMS(3) = Ne_RMS(3) + Ne_Merror(i, 3) * Ne_Merror(i, 3)
            ''''''''''''''Chebyshev拟合''''''''''''''
            Ch_Merror(i, 1) = Chebyshev_Coord(i, 1) - Gplanet(2).Fiv_shuX(i)
            Ch_Merror(i, 2) = Chebyshev_Coord(i, 2) - Gplanet(2).Fiv_shuY(i)
            Ch_Merror(i, 3) = Chebyshev_Coord(i, 3) - Gplanet(2).Fiv_shuZ(i)
            Ch_RMS(1) = Ch_RMS(1) + Ch_Merror(i, 1) * Ch_Merror(i, 1)
            Ch_RMS(2) = Ch_RMS(2) + Ch_Merror(i, 2) * Ch_Merror(i, 2)
            Ch_RMS(3) = Ch_RMS(3) + Ch_Merror(i, 3) * Ch_Merror(i, 3)
            ''''''''''''''Legendre拟合''''''''''''''
            Le_Merror(i, 1) = Legendre_Coord(i, 1) - Gplanet(2).Fiv_shuX(i)
            Le_Merror(i, 2) = Legendre_Coord(i, 2) - Gplanet(2).Fiv_shuY(i)
            Le_Merror(i, 3) = Legendre_Coord(i, 3) - Gplanet(2).Fiv_shuZ(i)
            Le_RMS(1) = Le_RMS(1) + Le_Merror(i, 1) * Le_Merror(i, 1)
            Le_RMS(2) = Le_RMS(2) + Le_Merror(i, 2) * Le_Merror(i, 2)
            Le_RMS(3) = Le_RMS(3) + Le_Merror(i, 3) * Le_Merror(i, 3)
      Next i
      Print #8, "Lagrange插值的中误差：" & String(4, " ") & "X方向:" & Format(Sqr(La_RMS(1) / 49), "0.#####") & String(4, " ") & "Y方向:" & Format(Sqr(La_RMS(2) / 49), "0.#####") & String(4, " ") & "Z方向:" & Format(Sqr(La_RMS(3) / 49), "0.#####")
      Print #8, "Neville插值的中误差：" & String(4, " ") & "X方向:" & Format(Sqr(Ne_RMS(1) / 49), "0.#####") & String(4, " ") & "Y方向:" & Format(Sqr(Ne_RMS(2) / 49), "0.#####") & String(4, " ") & "Z方向:" & Format(Sqr(Ne_RMS(3) / 49), "0.#####")
      Print #8, "Chebyshev拟合的中误差：" & String(4, " ") & "X方向:" & Format(Sqr(Ch_RMS(1) / 49), "0.#####") & String(4, " ") & "Y方向:" & Format(Sqr(Ch_RMS(2) / 49), "0.#####") & String(4, " ") & "Z方向:" & Format(Sqr(Ch_RMS(3) / 49), "0.#####")
      Print #8, "Legendre拟合的中误差：" & String(4, " ") & "X方向:" & Format(Sqr(Le_RMS(1) / 49), "0.#####") & String(4, " ") & "Y方向:" & Format(Sqr(Le_RMS(2) / 49), "0.#####") & String(4, " ") & "Z方向:" & Format(Sqr(Le_RMS(3) / 49), "0.#####")
      Close #8
End Sub

Private Sub Maximum_Error_Click()
       '''''''''''''''''四种不同算法的精度评定—最大误差'''''''''''''
      Dim i As Integer
      Dim La_Merror(1 To 49, 1 To 3) As Double
      Dim Ne_Merror(1 To 49, 1 To 3) As Double
      Dim Ch_Merror(1 To 49, 1 To 3) As Double
      Dim Le_Merror(1 To 49, 1 To 3) As Double
      Dim Max_La_RMS As Double, La_zhiX() As Double, La_zhiY() As Double, La_zhiZ() As Double
      Dim Max_Ne_RMS As Double, Ne_zhiX() As Double, Ne_zhiY() As Double, Ne_zhiZ() As Double
      Dim Max_Ch_RMS As Double, Ch_zhiX() As Double, Ch_zhiY() As Double, Ch_zhiZ() As Double
      Dim Max_Le_RMS As Double, Le_zhiX() As Double, Le_zhiY() As Double, Le_zhiZ() As Double
      ReDim La_zhiX(1 To 49): ReDim La_zhiY(1 To 49): ReDim La_zhiZ(1 To 49)
      ReDim Ne_zhiX(1 To 49): ReDim Ne_zhiY(1 To 49): ReDim Ne_zhiZ(1 To 49)
      ReDim Ch_zhiX(1 To 49): ReDim Ch_zhiY(1 To 49): ReDim Ch_zhiZ(1 To 49)
      ReDim Le_zhiX(1 To 49): ReDim Le_zhiY(1 To 49): ReDim Le_zhiZ(1 To 49)
      Dim La_Error_Maxx As Double, La_Error_Maxy As Double, La_Error_Maxz As Double
      Dim Ne_Error_Maxx As Double, Ne_Error_Maxy As Double, Ne_Error_Maxz As Double
      Dim Ch_Error_Maxx As Double, Ch_Error_Maxy As Double, Ch_Error_Maxz As Double
      Dim Le_Error_Maxx As Double, Le_Error_Maxy As Double, Le_Error_Maxz As Double
      Open App.Path & "\result\Max_Error.txt" For Output As #9
      For i = 1 To 49
            '''''''''''''''Lagrange插值'''''''''''''''
            La_Merror(i, 1) = Lagrange_Coord(i, 1) - Gplanet(2).Fiv_shuX(i)
            La_Merror(i, 2) = Lagrange_Coord(i, 2) - Gplanet(2).Fiv_shuY(i)
            La_Merror(i, 3) = Lagrange_Coord(i, 3) - Gplanet(2).Fiv_shuZ(i)
            La_zhiX(i) = La_Merror(i, 1)
            La_zhiY(i) = La_Merror(i, 2)
            La_zhiZ(i) = La_Merror(i, 3)
            Call Max_Zhi(La_zhiX(), La_Error_Maxx)
            Call Max_Zhi(La_zhiY(), La_Error_Maxy)
            Call Max_Zhi(La_zhiZ(), La_Error_Maxz)
            '''''''''''''''Neville插值'''''''''''''''
            Ne_Merror(i, 1) = Neville_Coord(i, 1) - Gplanet(2).Fiv_shuX(i)
            Ne_Merror(i, 2) = Neville_Coord(i, 2) - Gplanet(2).Fiv_shuY(i)
            Ne_Merror(i, 3) = Neville_Coord(i, 3) - Gplanet(2).Fiv_shuZ(i)
            Ne_zhiX(i) = Ne_Merror(i, 1)
            Ne_zhiY(i) = Ne_Merror(i, 2)
            Ne_zhiZ(i) = Ne_Merror(i, 3)
            Call Max_Zhi(Ne_zhiX(), Ne_Error_Maxx)
            Call Max_Zhi(Ne_zhiY(), Ne_Error_Maxy)
            Call Max_Zhi(Ne_zhiZ(), Ne_Error_Maxz)
            ''''''''''''''Chebyshev拟合''''''''''''''
            Ch_Merror(i, 1) = Chebyshev_Coord(i, 1) - Gplanet(2).Fiv_shuX(i)
            Ch_Merror(i, 2) = Chebyshev_Coord(i, 2) - Gplanet(2).Fiv_shuY(i)
            Ch_Merror(i, 3) = Chebyshev_Coord(i, 3) - Gplanet(2).Fiv_shuZ(i)
            Ch_zhiX(i) = Ch_Merror(i, 1)
            Ch_zhiY(i) = Ch_Merror(i, 2)
            Ch_zhiZ(i) = Ch_Merror(i, 3)
            Call Max_Zhi(Ch_zhiX(), Ch_Error_Maxx)
            Call Max_Zhi(Ch_zhiY(), Ch_Error_Maxy)
            Call Max_Zhi(Ch_zhiZ(), Ch_Error_Maxz)
            ''''''''''''''Legendre拟合''''''''''''''
            Le_Merror(i, 1) = Legendre_Coord(i, 1) - Gplanet(2).Fiv_shuX(i)
            Le_Merror(i, 2) = Legendre_Coord(i, 2) - Gplanet(2).Fiv_shuY(i)
            Le_Merror(i, 3) = Legendre_Coord(i, 3) - Gplanet(2).Fiv_shuZ(i)
            Le_zhiX(i) = Le_Merror(i, 1)
            Le_zhiY(i) = Le_Merror(i, 2)
            Le_zhiZ(i) = Le_Merror(i, 3)
            Call Max_Zhi(Le_zhiX(), Le_Error_Maxx)
            Call Max_Zhi(Le_zhiY(), Le_Error_Maxy)
            Call Max_Zhi(Le_zhiZ(), Le_Error_Maxz)
      Next i
      Print #9, "Lagrange插值的最大误差值：" & String(4, " ") & "X方向:" & Format(La_Error_Maxx, "0.#####") & String(4, " ") & "Y方向:" & Format(La_Error_Maxy, "0.#####") & String(4, " ") & "Z方向:" & Format(La_Error_Maxz, "0.#####")
      Print #9, "Neville插值的最大误差值：" & String(4, " ") & "X方向:" & Format(Ne_Error_Maxx, "0.#####") & String(4, " ") & "Y方向:" & Format(Ne_Error_Maxy, "0.#####") & String(4, " ") & "Z方向:" & Format(Ne_Error_Maxz, "0.#####")
      Print #9, "Chebyshev拟合的最大误差值：" & String(4, " ") & "X方向:" & Format(Ch_Error_Maxx, "0.#####") & String(4, " ") & "Y方向:" & Format(Ch_Error_Maxy, "0.#####") & String(4, " ") & "Z方向:" & Format(Ch_Error_Maxz, "0.#####")
      Print #9, "Legendre拟合的最大误差值：" & String(4, " ") & "X方向:" & Format(Le_Error_Maxx, "0.#####") & String(4, " ") & "Y方向:" & Format(Le_Error_Maxy, "0.#####") & String(4, " ") & "Z方向:" & Format(Le_Error_Maxz, "0.#####")
      Close #9
End Sub

Private Sub Minimum_Error_Click()
        '''''''''''''''''四种不同算法的精度评定—最小误差'''''''''''''
      Dim i As Integer
      Dim La_Merror(1 To 49, 1 To 3) As Double
      Dim Ne_Merror(1 To 49, 1 To 3) As Double
      Dim Ch_Merror(1 To 49, 1 To 3) As Double
      Dim Le_Merror(1 To 49, 1 To 3) As Double
      Dim Min_La_RMS As Double, La_zhiX() As Double, La_zhiY() As Double, La_zhiZ() As Double
      Dim Min_Ne_RMS As Double, Ne_zhiX() As Double, Ne_zhiY() As Double, Ne_zhiZ() As Double
      Dim Min_Ch_RMS As Double, Ch_zhiX() As Double, Ch_zhiY() As Double, Ch_zhiZ() As Double
      Dim Min_Le_RMS As Double, Le_zhiX() As Double, Le_zhiY() As Double, Le_zhiZ() As Double
      ReDim La_zhiX(1 To 49): ReDim La_zhiY(1 To 49): ReDim La_zhiZ(1 To 49)
      ReDim Ne_zhiX(1 To 49): ReDim Ne_zhiY(1 To 49): ReDim Ne_zhiZ(1 To 49)
      ReDim Ch_zhiX(1 To 49): ReDim Ch_zhiY(1 To 49): ReDim Ch_zhiZ(1 To 49)
      ReDim Le_zhiX(1 To 49): ReDim Le_zhiY(1 To 49): ReDim Le_zhiZ(1 To 49)
      Dim La_Error_Minx As Double, La_Error_Miny As Double, La_Error_Minz As Double
      Dim Ne_Error_Minx As Double, Ne_Error_Miny As Double, Ne_Error_Minz As Double
      Dim Ch_Error_Minx As Double, Ch_Error_Miny As Double, Ch_Error_Minz As Double
      Dim Le_Error_Minx As Double, Le_Error_Miny As Double, Le_Error_Minz As Double
      Open App.Path & "\result\Min_Error.txt" For Output As #10
      For i = 1 To 49
            '''''''''''''''Lagrange插值'''''''''''''''
            La_Merror(i, 1) = Lagrange_Coord(i, 1) - Gplanet(2).Fiv_shuX(i)
            La_Merror(i, 2) = Lagrange_Coord(i, 2) - Gplanet(2).Fiv_shuY(i)
            La_Merror(i, 3) = Lagrange_Coord(i, 3) - Gplanet(2).Fiv_shuZ(i)
            La_zhiX(i) = La_Merror(i, 1)
            La_zhiY(i) = La_Merror(i, 2)
            La_zhiZ(i) = La_Merror(i, 3)
            Call Min_Zhi(La_zhiX(), La_Error_Minx)
            Call Min_Zhi(La_zhiY(), La_Error_Miny)
            Call Min_Zhi(La_zhiZ(), La_Error_Minz)
            '''''''''''''''Neville插值'''''''''''''''
            Ne_Merror(i, 1) = Neville_Coord(i, 1) - Gplanet(2).Fiv_shuX(i)
            Ne_Merror(i, 2) = Neville_Coord(i, 2) - Gplanet(2).Fiv_shuY(i)
            Ne_Merror(i, 3) = Neville_Coord(i, 3) - Gplanet(2).Fiv_shuZ(i)
            Ne_zhiX(i) = Ne_Merror(i, 1)
            Ne_zhiY(i) = Ne_Merror(i, 2)
            Ne_zhiZ(i) = Ne_Merror(i, 3)
            Call Min_Zhi(Ne_zhiX(), Ne_Error_Minx)
            Call Min_Zhi(Ne_zhiY(), Ne_Error_Miny)
            Call Min_Zhi(Ne_zhiZ(), Ne_Error_Minz)
            ''''''''''''''Chebyshev拟合''''''''''''''
            Ch_Merror(i, 1) = Chebyshev_Coord(i, 1) - Gplanet(2).Fiv_shuX(i)
            Ch_Merror(i, 2) = Chebyshev_Coord(i, 2) - Gplanet(2).Fiv_shuY(i)
            Ch_Merror(i, 3) = Chebyshev_Coord(i, 3) - Gplanet(2).Fiv_shuZ(i)
            Ch_zhiX(i) = Ch_Merror(i, 1)
            Ch_zhiY(i) = Ch_Merror(i, 2)
            Ch_zhiZ(i) = Ch_Merror(i, 3)
            Call Min_Zhi(Ch_zhiX(), Ch_Error_Minx)
            Call Min_Zhi(Ch_zhiY(), Ch_Error_Miny)
            Call Min_Zhi(Ch_zhiZ(), Ch_Error_Minz)
            ''''''''''''''Legendre拟合''''''''''''''
            Le_Merror(i, 1) = Legendre_Coord(i, 1) - Gplanet(2).Fiv_shuX(i)
            Le_Merror(i, 2) = Legendre_Coord(i, 2) - Gplanet(2).Fiv_shuY(i)
            Le_Merror(i, 3) = Legendre_Coord(i, 3) - Gplanet(2).Fiv_shuZ(i)
            Le_zhiX(i) = Le_Merror(i, 1)
            Le_zhiY(i) = Le_Merror(i, 2)
            Le_zhiZ(i) = Le_Merror(i, 3)
            Call Min_Zhi(Le_zhiX(), Le_Error_Minx)
            Call Min_Zhi(Le_zhiY(), Le_Error_Miny)
            Call Min_Zhi(Le_zhiZ(), Le_Error_Minz)
      Next i
      Print #10, "Lagrange插值的最小误差值：" & String(4, " ") & "X方向:" & Format(La_Error_Minx, "0.#####") & String(4, " ") & "Y方向:" & Format(La_Error_Miny, "0.#####") & String(4, " ") & "Z方向:" & Format(La_Error_Minz, "0.#####")
      Print #10, "Neville插值的最小误差值：" & String(4, " ") & "X方向:" & Format(Ne_Error_Minx, "0.#####") & String(4, " ") & "Y方向:" & Format(Ne_Error_Miny, "0.#####") & String(4, " ") & "Z方向:" & Format(Ne_Error_Minz, "0.#####")
      Print #10, "Chebyshev拟合的最小误差值：" & String(4, " ") & "X方向:" & Format(Ch_Error_Minx, "0.#####") & String(4, " ") & "Y方向:" & Format(Ch_Error_Miny, "0.#####") & String(4, " ") & "Z方向:" & Format(Ch_Error_Minz, "0.#####")
      Print #10, "Legendre拟合的最小误差值：" & String(4, " ") & "X方向:" & Format(Le_Error_Minx, "0.#####") & String(4, " ") & "Y方向:" & Format(Le_Error_Miny, "0.#####") & String(4, " ") & "Z方向:" & Format(Le_Error_Minz, "0.#####")
      Close #10
End Sub

Private Sub Using_Document_Click()
       Dim result
       result = ShellExecute(0, vbNullString, App.Path & "\help.pdf", vbNullString, vbNullString, SW_SHOWNORMAL)
       If result <= 32 Then
              MsgBox "打开失败！", vbOKOnly + vbCritical, "错误"
       End If
End Sub

Private Sub Exit_Program_Click()
   End
End Sub
