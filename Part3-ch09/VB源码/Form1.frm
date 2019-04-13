VERSION 5.00
Object = "{F9043C88-F6F2-101A-A3C9-08002B2F49FB}#1.2#0"; "COMDLG32.OCX"
Object = "{831FDD16-0C5C-11D2-A9FC-0000F8754DA1}#2.0#0"; "MSCOMCTL.OCX"
Begin VB.Form Form1 
   Caption         =   "纵横断面计算程序"
   ClientHeight    =   4872
   ClientLeft      =   192
   ClientTop       =   888
   ClientWidth     =   7512
   LinkTopic       =   "Form1"
   ScaleHeight     =   4872
   ScaleWidth      =   7512
   StartUpPosition =   3  '窗口缺省
   Begin MSComctlLib.StatusBar StatusBar1 
      Align           =   2  'Align Bottom
      Height          =   372
      Left            =   0
      TabIndex        =   0
      Top             =   4500
      Width           =   7512
      _ExtentX        =   13250
      _ExtentY        =   656
      _Version        =   393216
      BeginProperty Panels {8E3867A5-8586-11D1-B16A-00C0F0283628} 
         NumPanels       =   3
         BeginProperty Panel1 {8E3867AB-8586-11D1-B16A-00C0F0283628} 
            Style           =   6
            Alignment       =   1
            Object.Width           =   2117
            MinWidth        =   2117
            TextSave        =   "2019/3/7"
         EndProperty
         BeginProperty Panel2 {8E3867AB-8586-11D1-B16A-00C0F0283628} 
            Style           =   5
            Alignment       =   1
            Object.Width           =   2117
            MinWidth        =   2117
            TextSave        =   "17:46"
         EndProperty
         BeginProperty Panel3 {8E3867AB-8586-11D1-B16A-00C0F0283628} 
            Alignment       =   1
            AutoSize        =   1
            Object.Width           =   10583
            MinWidth        =   10583
            Text            =   "欢迎使用该程序"
            TextSave        =   "欢迎使用该程序"
            Object.ToolTipText     =   "欢迎使用该程序"
         EndProperty
      EndProperty
      BeginProperty Font {0BE35203-8F91-11CE-9DE3-00AA004BB851} 
         Name            =   "宋体"
         Size            =   10.2
         Charset         =   134
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
   End
   Begin MSComDlg.CommonDialog CommonDialog1 
      Left            =   240
      Top             =   3480
      _ExtentX        =   847
      _ExtentY        =   847
      _Version        =   393216
   End
   Begin VB.Menu Basic_Cal 
      Caption         =   "基本计算"
      Begin VB.Menu file1 
         Caption         =   "-"
      End
      Begin VB.Menu Data_File_Input 
         Caption         =   "数据文件读入"
      End
      Begin VB.Menu file2 
         Caption         =   "-"
      End
      Begin VB.Menu Azith_Compute 
         Caption         =   "坐标方位角计算"
      End
   End
   Begin VB.Menu Height_Inter 
      Caption         =   "内插点高程值计算"
      Begin VB.Menu file3 
         Caption         =   "-"
      End
      Begin VB.Menu Inv_Dis_Weight 
         Caption         =   "反距离加权法"
      End
      Begin VB.Menu file4 
         Caption         =   "-"
      End
      Begin VB.Menu DM_Area 
         Caption         =   "断面面积计算"
      End
   End
   Begin VB.Menu Road_Zong 
      Caption         =   "道路纵断面计算"
      Begin VB.Menu file5 
         Caption         =   "-"
      End
      Begin VB.Menu Length_Longitudinal 
         Caption         =   "纵断面长度计算"
      End
      Begin VB.Menu file6 
         Caption         =   "-"
      End
      Begin VB.Menu Inter_Coord_Cal 
         Caption         =   "内插点平面坐标计算"
      End
      Begin VB.Menu file8 
         Caption         =   "-"
      End
   End
   Begin VB.Menu Transect 
      Caption         =   "道路横断面计算"
      Begin VB.Menu file7 
         Caption         =   "-"
      End
      Begin VB.Menu Core_Point 
         Caption         =   "横断面中心点计算"
      End
      Begin VB.Menu file9 
         Caption         =   "-"
      End
      Begin VB.Menu Transect_Inv_Point 
         Caption         =   "横断面插值点的平面坐标和高程计算"
      End
      Begin VB.Menu file10 
         Caption         =   "-"
      End
   End
   Begin VB.Menu Help 
      Caption         =   "帮助"
      Begin VB.Menu file11 
         Caption         =   "-"
      End
      Begin VB.Menu Using_Doc 
         Caption         =   "使用文档"
      End
      Begin VB.Menu file12 
         Caption         =   "-"
      End
   End
   Begin VB.Menu Out 
      Caption         =   "退出"
   End
End
Attribute VB_Name = "Form1"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Option Explicit
Dim Point_Data() As String, point_shu() As String
Dim Length_Vertical() As Double, Point_Key() As String, LK As Integer
Dim sum_length As Double, Ref_H As Double
Dim Xm() As Double, Ym() As Double
Dim Azith_K01 As Double
Dim Azith_K12 As Double

Private Sub Data_File_Input_Click()
  Call Read_Data(Point_Data(), point_shu())
End Sub

Private Sub Azith_Compute_Click()
      Dim i As Integer
      Dim shu1(1 To 4) As String
      Dim Coor1() As String, Coor2() As String
      Dim A_CoorX As Double, A_CoorY As Double, B_CoorX As Double, B_CoorY As Double
      For i = 1 To 4
         shu1(i) = point_shu(i)
         If Left(Trim(shu1(i)), 1) = "A" Then
            Coor1 = Split(Trim(shu1(i)), ",")
            A_CoorX = Val(Coor1(1))
            A_CoorY = Val(Coor1(2))
         ElseIf Left(Trim(shu1(i)), 1) = "B" Then
            Coor2 = Split(Trim(shu1(i)), ",")
            B_CoorX = Val(Coor2(1))
            B_CoorY = Val(Coor2(2))
         End If
      Next i
      Dim AB_Azith As Double
      Call Azith_cal(A_CoorX, A_CoorY, B_CoorX, B_CoorY, AB_Azith)
      Dim Deg_zhi As Double, Min_zhi As Double, Sec_zhi As Double
      Call Degree_transfer(AB_Azith, Deg_zhi, Min_zhi, Sec_zhi)
      Open App.Path & "\坐标方位角计算报告.txt" For Output As #1
      Print #1, "*******坐标方位角计算结果*******"
      Print #1, "度（dd°）", "分（mm′）", "秒（ss.ssss″）"
      Print #1, Deg_zhi, Min_zhi, Sec_zhi
      Close #1
End Sub

Private Sub Inv_Dis_Weight_Click()
     Me.MousePointer = vbHourglass
     Dim i As Integer, flag As Integer, m As Integer
     Dim PData() As String
     ReDim PData(1 To UBound(Point_Data, 1) - 1, 1 To 4)
     m = 0
     For i = 1 To UBound(Point_Data, 1)
        If Point_Data(i, 1) = "K1" Then
          flag = i
        ElseIf Point_Data(i, 1) <> "K1" Then
          m = m + 1
          PData(m, 1) = Point_Data(i, 1)
          PData(m, 2) = Point_Data(i, 2)
          PData(m, 3) = Point_Data(i, 3)
          PData(m, 4) = Point_Data(i, 4)
        End If
     Next i
     Dim Dist() As Double, Dis() As Double
     ReDim Dist(1 To m)
     ReDim Dis(1 To m)
     For i = 1 To m
       Dist(i) = Sqr((Val(Point_Data(flag, 2)) - Val(PData(i, 2))) ^ 2 + (Val(Point_Data(flag, 3)) - Val(PData(i, 3))) ^ 2)
       Dis(i) = Sqr((Val(Point_Data(flag, 2)) - Val(PData(i, 2))) ^ 2 + (Val(Point_Data(flag, 3)) - Val(PData(i, 3))) ^ 2)
     Next i
     Call Paixu(m, Dis())
     Dim j As Integer
     Dim num As Integer
     Dim Point_name()  As String
     ReDim Point_name(1 To m)
     Dim Sd_Data() As Double
     ReDim Sd_Data(1 To m, 1 To 4)
     num = 0
     For i = 1 To 5
        For j = 1 To m
            If Dis(i) = Dist(j) Then
                 num = num + 1
                 Point_name(num) = PData(j, 1)
                 Sd_Data(num, 1) = Val(PData(j, 2))
                 Sd_Data(num, 2) = Val(PData(j, 3))
                 Sd_Data(num, 3) = Val(PData(j, 4))
                 Sd_Data(num, 4) = Dist(j)
            End If
        Next j
     Next i
     Dim sum_dh As Double, sum_d As Double
     sum_dh = 0
     sum_d = 0
     For i = 1 To 5
         sum_dh = sum_dh + (Sd_Data(i, 3) / Sd_Data(i, 4))
         sum_d = sum_d + (1 / Sd_Data(i, 4))
     Next i
     Dim Inter_K_H As Double
     Inter_K_H = sum_dh / sum_d
     Open App.Path & "\内插K1点高程.txt" For Output As #2
     Print #2, "以K1为内插点，最近5个点的点号、坐标(X,Y,H)和距离如下:"
     For i = 1 To 5
        Print #2, Point_name(i), Sd_Data(i, 1), Sd_Data(i, 2), Sd_Data(i, 3), Format(Sd_Data(i, 4), "0.000")
     Next i
     Print #2, "内插点K1的内插高程为：" & Format(Inter_K_H, "0.000")
     Close #2
     Me.MousePointer = vbDefault
End Sub

Private Sub DM_Area_Click()
     Me.MousePointer = vbHourglass
     Dim H0() As String
     H0 = Split(point_shu(1), ",")
     Ref_H = Val(H0(1))
     Dim K0 As Integer, K1 As Integer, i As Integer
     For i = 1 To UBound(Point_Data, 1)
        If Point_Data(i, 1) = "K0" Then
          K0 = i
        ElseIf Point_Data(i, 1) = "K1" Then
          K1 = i
        End If
     Next i
     Dim DeltaL As Double
     DeltaL = Sqr((Val(Point_Data(K0, 2)) - Val(Point_Data(K1, 2))) ^ 2 + (Val(Point_Data(K0, 3)) - Val(Point_Data(K1, 3))) ^ 2)
     Dim Area As Double
     Area = (Val(Point_Data(K0, 4)) + Val(Point_Data(K1, 4)) - 2 * Ref_H) / 2 * DeltaL
     Open App.Path & "\梯形面积.txt" For Output As #3
     Print #3, "以K_0，K_1为梯形的两个端点的梯形面积为：" & Format(Area, "0.000")
     Me.MousePointer = vbDefault
End Sub

Private Sub Length_Longitudinal_Click()
    Me.MousePointer = vbHourglass
    Dim Key_Dian() As String
    Key_Dian = Split(point_shu(2), ",")
    Dim i As Integer, j As Integer
    ReDim Point_Key(1 To UBound(Point_Data, 1), 1 To 4)
    LK = 0
    For i = 0 To UBound(Key_Dian)
       For j = 1 To UBound(Point_Data, 1)
           If Key_Dian(i) = Point_Data(j, 1) Then
               LK = LK + 1
               Point_Key(LK, 1) = Point_Data(j, 1)
               Point_Key(LK, 2) = Point_Data(j, 2)
               Point_Key(LK, 3) = Point_Data(j, 3)
               Point_Key(LK, 4) = Point_Data(j, 4)
            End If
        Next j
    Next i
    ReDim Length_Vertical(1 To LK - 1)
    For i = 1 To LK - 1
       Length_Vertical(i) = Sqr((Val(Point_Key(i + 1, 2)) - Val(Point_Key(i, 2))) ^ 2 + (Val(Point_Key(i + 1, 3)) - Val(Point_Key(i, 3))) ^ 2)
    Next i
    sum_length = 0
    For i = 1 To LK - 1
       sum_length = sum_length + Length_Vertical(i)
    Next i
    Open App.Path & "\纵断面的总长度.txt" For Output As #4
    Print #4, "纵断面的总长度为：" & Format(sum_length, "0.000")
    Close #4
    Me.MousePointer = vbDefault
End Sub

Private Sub Inter_Coord_Cal_Click()
    Me.MousePointer = vbHourglass
    Dim i As Integer, j As Integer
    '''''''先计算K0，K1线段的坐标'''''''
    Call Azith_cal(Val(Point_Key(1, 2)), Val(Point_Key(1, 3)), Val(Point_Key(2, 2)), Val(Point_Key(2, 3)), Azith_K01)
    Dim K01 As Integer
    K01 = Int(Length_Vertical(1) / 10)
    Dim L01_DK() As Double
    ReDim L01_DK(1 To K01)
    Dim CoordXi() As Double, CoordYi() As Double
    ReDim CoordXi(1 To K01): ReDim CoordYi(1 To K01)
    Dim Coor_X1 As Double, Coor_Y1 As Double
    For i = 1 To K01
        L01_DK(i) = 10 + (i - 1) * 10
        Call Coord_Compute(Azith_K01, L01_DK(i), Val(Point_Key(1, 2)), Val(Point_Key(1, 3)), Coor_X1, Coor_Y1)
        CoordXi(i) = Coor_X1
        CoordYi(i) = Coor_Y1
    Next i
    Dim Distance() As Double
    ReDim Distance(1 To K01, 1 To UBound(Point_Data, 1))
    Dim Dist_1() As Double
    ReDim Dist_1(1 To UBound(Point_Data, 1))
    Dim Dist_2() As Double
    ReDim Dist_2(1 To UBound(Point_Data, 1))
    Dim Inter_K_H As Double
    Dim NC_Height() As Double
    ReDim NC_Height(1 To K01)
    For i = 1 To K01
       For j = 1 To UBound(Point_Data, 1)
          Distance(i, j) = Sqr((Val(Point_Data(j, 2)) - CoordXi(i)) ^ 2 + (Val(Point_Data(j, 3)) - CoordYi(i)) ^ 2)
          Dist_1(j) = Distance(i, j)
          Dist_2(j) = Distance(i, j)
       Next j
       Call Interplate_Height(Dist_1(), Dist_2(), Point_Data(), Inter_K_H)
       NC_Height(i) = Inter_K_H
    Next i
    ''''''''''''其次计算K1,K2线段的坐标'''''''
    Call Azith_cal(Val(Point_Key(2, 2)), Val(Point_Key(2, 3)), Val(Point_Key(3, 2)), Val(Point_Key(3, 3)), Azith_K12)
    Dim K12 As Integer
    K12 = Int(Length_Vertical(2) / 10)
    Dim L12_DK() As Double
    ReDim L12_DK(1 To K01)
    Dim Coor_Xi() As Double, Coor_Yi() As Double
    ReDim Coor_Xi(1 To K12): ReDim Coor_Yi(1 To K12)
    Dim Coor_X2 As Double, Coor_Y2 As Double
    For i = 1 To K12
        L12_DK(i) = L01_DK(K01) + i * 10
        Call Coordniate_Compute(Azith_K12, L12_DK(i), Length_Vertical(1), Val(Point_Key(2, 2)), Val(Point_Key(2, 3)), Coor_X2, Coor_Y2)
        Coor_Xi(i) = Coor_X2
        Coor_Yi(i) = Coor_Y2
    Next i
    Dim Distance1() As Double
    ReDim Distance1(1 To K12, 1 To UBound(Point_Data, 1))
    Dim Distan_1() As Double
    ReDim Distan_1(1 To UBound(Point_Data, 1))
    Dim Distan_2() As Double
    ReDim Distan_2(1 To UBound(Point_Data, 1))
    Dim Inter_K12_H As Double
    Dim N_Height() As Double
    ReDim N_Height(1 To K12)
    For i = 1 To K12
       For j = 1 To UBound(Point_Data, 1)
          Distance1(i, j) = Sqr((Val(Point_Data(j, 2)) - Coor_Xi(i)) ^ 2 + (Val(Point_Data(j, 3)) - Coor_Yi(i)) ^ 2)
          Distan_1(j) = Distance1(i, j)
          Distan_2(j) = Distance1(i, j)
       Next j
       Call Interplate_Height(Distan_1(), Distan_2(), Point_Data(), Inter_K12_H)
       N_Height(i) = Inter_K12_H
    Next i
    Dim ZD_name() As String, ZD_zhi() As Double
    ReDim ZD_name(1 To K01 + K12 + 3)
    ReDim ZD_zhi(1 To K01 + K12 + 3, 1 To 4)
    For i = 1 To (K01 + K12 + 3)
        If i = 1 Then
            ZD_name(i) = Point_Key(1, 1)
            ZD_zhi(i, 1) = 0
            ZD_zhi(i, 2) = Val(Point_Key(1, 2))
            ZD_zhi(i, 3) = Val(Point_Key(1, 3))
            ZD_zhi(i, 4) = Val(Point_Key(1, 4))
       ElseIf i > 1 And i <= (1 + K01) Then
            ZD_name(i) = "V-" & (i - 1)
            ZD_zhi(i, 1) = L01_DK(i - 1)
            ZD_zhi(i, 2) = CoordXi(i - 1)
            ZD_zhi(i, 3) = CoordYi(i - 1)
            ZD_zhi(i, 4) = NC_Height(i - 1)
       ElseIf i = (2 + K01) Then
            ZD_name(i) = Point_Key(2, 1)
            ZD_zhi(i, 1) = Length_Vertical(1)
            ZD_zhi(i, 2) = Val(Point_Key(2, 2))
            ZD_zhi(i, 3) = Val(Point_Key(2, 3))
            ZD_zhi(i, 4) = Val(Point_Key(2, 4))
       ElseIf i > (2 + K01) And i < (K01 + K12 + 3) Then
            ZD_name(i) = "V-" & (i - 2)
            ZD_zhi(i, 1) = L12_DK(i - (2 + K01))
            ZD_zhi(i, 2) = Coor_Xi(i - (2 + K01))
            ZD_zhi(i, 3) = Coor_Yi(i - (2 + K01))
            ZD_zhi(i, 4) = N_Height(i - (2 + K01))
      ElseIf i = (K01 + K12 + 3) Then
            ZD_name(i) = Point_Key(3, 1)
            ZD_zhi(i, 1) = Length_Vertical(1) + Length_Vertical(2)
            ZD_zhi(i, 2) = Val(Point_Key(3, 2))
            ZD_zhi(i, 3) = Val(Point_Key(3, 3))
            ZD_zhi(i, 4) = Val(Point_Key(3, 4))
      End If
   Next i
   Dim Szhi() As Double
   ReDim Szhi(1 To (K01 + K12 + 2))
   Dim sum_Szhi As Double
   sum_Szhi = 0
   For i = 1 To (K01 + K12 + 2)
      Szhi(i) = (ZD_zhi(i + 1, 4) + ZD_zhi(i, 4) - 2 * Ref_H) / 2 * (ZD_zhi(i + 1, 1) - ZD_zhi(i, 1))
      sum_Szhi = sum_Szhi + Szhi(i)
   Next i
   Open App.Path & "\纵断面计算报告.txt" For Output As #5
   Print #5, "**************************纵断面计算报告**************************"
   Print #5, "纵断面信息"
   Print #5, "纵断面的面积为：" & Format(sum_Szhi, "0.000")
   Print #5, "纵断面的全长为：" & Format(sum_length, "0.000")
   Print #5, "线路主点:"
   Print #5, "点名", "里程K(m)", "X坐标(m)", "Y坐标(m)", "H坐标(m)"
   For i = 1 To (K01 + K12 + 3)
      Print #5, ZD_name(i), Format(ZD_zhi(i, 1), "0.000"), Format(ZD_zhi(i, 2), "0.000"), Format(ZD_zhi(i, 3), "0.000"), Format(ZD_zhi(i, 4), "0.000")
   Next i
   Close #5
   Me.MousePointer = vbDefault
End Sub

Private Sub Core_Point_Click()
   Dim i As Integer
   ReDim Xm(1 To (LK - 1))
   ReDim Ym(1 To (LK - 1))
   For i = 1 To (LK - 1)
       Xm(i) = (Val(Point_Key(i, 2)) + Val(Point_Key(i + 1, 2))) / 2
       Ym(i) = (Val(Point_Key(i, 3)) + Val(Point_Key(i + 1, 3))) / 2
   Next i
   Open App.Path & "\横断面中心点坐标.txt" For Output As #6
   Print #6, "横断面中心点坐标信息：", "X坐标(m)", "Y坐标(m)"
   For i = 1 To (LK - 1)
      Print #6, "第" & i & "个中点坐标：", Format(Xm(i), "0.000"), Format(Ym(i), "0.000")
   Next i
   Close #6
End Sub

Private Sub Transect_Inv_Point_Click()
   Me.MousePointer = vbHourglass
   Dim Am_angle() As Double
   ReDim Am_angle(1 To (LK - 1))
   Dim i As Integer, Azith_Inv As Double
   For i = 1 To (LK - 1)
      Call Azith_cal(Val(Point_Key(i, 2)), Val(Point_Key(i, 3)), Val(Point_Key(i + 1, 2)), Val(Point_Key(i + 1, 3)), Azith_Inv)
      Am_angle(i) = Azith_Inv + 90
   Next i
   '''''''''''''''''''''按照5m内插，对于25米长两边各5个点''''''''''''''''''
   ''''''''''''''先计算K0,K1的中间点(以M0为中间点)''''''''''''
   Dim XJ_Coor() As Double, YJ_Coor() As Double
   ReDim XJ_Coor(1 To 10)
   ReDim YJ_Coor(1 To 10)
   For i = 1 To 10
     If i <= 5 Then
         XJ_Coor(i) = Xm(1) + (-5) * i * Cos(Am_angle(1) * 4 * Atn(1) / 180)
         YJ_Coor(i) = Ym(1) + (-5) * i * Sin(Am_angle(1) * 4 * Atn(1) / 180)
     ElseIf i > 5 Then
         XJ_Coor(i) = Xm(1) + 5 * i * Cos(Am_angle(1) * 4 * Atn(1) / 180)
         YJ_Coor(i) = Ym(1) + 5 * i * Sin(Am_angle(1) * 4 * Atn(1) / 180)
     End If
   Next i
    Dim XJ_Coord() As Double, YJ_Coord() As Double
    ReDim XJ_Coord(1 To 11)
    ReDim YJ_Coord(1 To 11)
    For i = 1 To 11
        If i <= 5 Then
           XJ_Coord(i) = XJ_Coor(i)
           YJ_Coord(i) = YJ_Coor(i)
        ElseIf i = 6 Then
           XJ_Coord(i) = Xm(1)
           YJ_Coord(i) = Ym(1)
        Else
           XJ_Coord(i) = XJ_Coor(i - 1)
           YJ_Coord(i) = YJ_Coor(i - 1)
        End If
    Next i
    Dim Dist_M1() As Double
    ReDim Dist_M1(1 To 11, 1 To UBound(Point_Data, 1))
    Dim DM1() As Double
    ReDim DM1(1 To UBound(Point_Data, 1))
    Dim D1_M() As Double
    ReDim D1_M(1 To UBound(Point_Data, 1))
    Dim Inter_MH1 As Double
    Dim M1_Height() As Double
    ReDim M1_Height(1 To 11)
    Dim j As Integer
    For i = 1 To 11
       For j = 1 To UBound(Point_Data, 1)
          Dist_M1(i, j) = Sqr((Val(Point_Data(j, 2)) - XJ_Coord(i)) ^ 2 + (Val(Point_Data(j, 3)) - YJ_Coord(i)) ^ 2)
          DM1(j) = Dist_M1(i, j)
          D1_M(j) = Dist_M1(i, j)
       Next j
       Call Interplate_Height(DM1(), D1_M(), Point_Data(), Inter_MH1)
       M1_Height(i) = Inter_MH1
    Next i
   Dim S_zhi() As Double
   ReDim S_zhi(1 To 10)
   Dim sum1_area As Double
   sum1_area = 0
   For i = 1 To 10
      S_zhi(i) = (M1_Height(i) + M1_Height(i + 1) - 2 * Ref_H) / 2 * 5
      sum1_area = sum1_area + S_zhi(i)
   Next i
    Open App.Path & "\以M0为中间点内插的平面坐标和高程.txt" For Output As #7
    Print #7, "以M0为中间点的横断面面积为:" & Format(sum1_area, "0.000")
    Print #7, "****************************************************************************"
    Print #7, "点名", "X坐标(m)", "Y坐标(m)", "H坐标(m)"
    For i = 1 To 11
        Print #7, i & "-M", Format(XJ_Coord(i), "0.000"), Format(YJ_Coord(i), "0.000"), Format(M1_Height(i), "0.000")
    Next i
    Close #7
    ''''''''''''其次计算K1,K2的中间点(以M1为中间点)'''''''
   Dim Coor_XJ() As Double, Coor_YJ() As Double
   ReDim Coor_XJ(1 To 10)
   ReDim Coor_YJ(1 To 10)
   For i = 1 To 10
     If i <= 5 Then
         Coor_XJ(i) = Xm(2) + (-5) * i * Cos(Am_angle(2) * 4 * Atn(1) / 180)
         Coor_YJ(i) = Ym(2) + (-5) * i * Sin(Am_angle(2) * 4 * Atn(1) / 180)
     ElseIf i > 5 Then
         Coor_XJ(i) = Xm(2) + 5 * i * Cos(Am_angle(2) * 4 * Atn(1) / 180)
         Coor_YJ(i) = Ym(2) + 5 * i * Sin(Am_angle(2) * 4 * Atn(1) / 180)
     End If
   Next i
   Dim Coord_XJ() As Double, Coord_YJ() As Double
   ReDim Coord_XJ(1 To 11)
   ReDim Coord_YJ(1 To 11)
   For i = 1 To 11
        If i <= 5 Then
           Coord_XJ(i) = Coor_XJ(i)
           Coord_YJ(i) = Coor_YJ(i)
        ElseIf i = 6 Then
           Coord_XJ(i) = Xm(2)
           Coord_YJ(i) = Ym(2)
        Else
           Coord_XJ(i) = Coor_XJ(i - 1)
           Coord_YJ(i) = Coor_YJ(i - 1)
        End If
    Next i
    Dim Dist_M2() As Double
    ReDim Dist_M2(1 To 11, 1 To UBound(Point_Data, 1))
    Dim DM2() As Double
    ReDim DM2(1 To UBound(Point_Data, 1))
    Dim D2_M() As Double
    ReDim D2_M(1 To UBound(Point_Data, 1))
    Dim Inter_MH2 As Double
    Dim M2_Height() As Double
    ReDim M2_Height(1 To 11)
    For i = 1 To 11
       For j = 1 To UBound(Point_Data, 1)
          Dist_M2(i, j) = Sqr((Val(Point_Data(j, 2)) - Coord_XJ(i)) ^ 2 + (Val(Point_Data(j, 3)) - Coord_YJ(i)) ^ 2)
          DM2(j) = Dist_M2(i, j)
          D2_M(j) = Dist_M2(i, j)
       Next j
       Call Interplate_Height(DM2(), D2_M(), Point_Data(), Inter_MH2)
       M2_Height(i) = Inter_MH2
    Next i
   Dim zhi_S() As Double
   ReDim zhi_S(1 To 10)
   Dim area_sum1 As Double
   area_sum1 = 0
   For i = 1 To 10
      zhi_S(i) = (M2_Height(i) + M2_Height(i + 1) - 2 * Ref_H) / 2 * 5
      area_sum1 = area_sum1 + zhi_S(i)
   Next i
    Open App.Path & "\以M1为中间点内插的平面坐标和高程.txt" For Output As #8
    Print #8, "以M1为中间点的横断面面积为:" & Format(area_sum1, "0.000")
    Print #8, "****************************************************************************"
    Print #8, "点名", "X坐标(m)", "Y坐标(m)", "H坐标(m)"
    For i = 1 To 11
        Print #8, i & "-M", Format(Coord_XJ(i), "0.000"), Format(Coord_YJ(i), "0.000"), Format(M2_Height(i), "0.000")
    Next i
    Close #8
    Me.MousePointer = vbDefault
End Sub

Private Sub Using_Doc_Click()
       Dim result
       result = ShellExecute(0, vbNullString, App.Path & "\help.pdf", vbNullString, vbNullString, SW_SHOWNORMAL)
       If result <= 32 Then
              MsgBox "打开失败！", vbOKOnly + vbCritical, "错误"
       End If
End Sub

Private Sub Out_Click()
   End
End Sub

