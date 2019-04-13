Imports System.Math
Public Class Road_Element
    Private RoadStaPt, RoadEndPt As RoadPoint
    Private L, T, Q, Ev, m, B, P As Double
    Private mLab As String
    Friend ZH, HY, QZ, YH, HZ, JD As RoadPoint
    Private a, R, Ls As Double
    Private ZX As Boolean 'True为左转，False为右转
    Friend MileStake() As RoadPoint
    Private StakeSpac As Integer
    Dim a1, a2 As Double

    Sub New(ByVal PtSta As RoadPoint, ByVal TurnNode As RoadPoint, ByVal PtEnd As RoadPoint, ByVal Spacing As Integer)
        RoadStaPt = PtSta : JD = TurnNode : RoadEndPt = PtEnd : StakeSpac = Spacing
        R = TurnNode.R : Ls = TurnNode.Ls
        If Ls = 0 Then
            mLab = "YQX"
        Else
            mLab = "HQX"
        End If
        CurvElemCalcu()
        MileCoorCalcu()
    End Sub
    ReadOnly Property Lab As String
        Get
            Return mLab
        End Get
    End Property
    ReadOnly Property TurnAng As Double
        Get
            Return a
        End Get
    End Property
    ReadOnly Property CircRadius As Double
        Get
            Return R
        End Get
    End Property
    ReadOnly Property CurvLeng As Double
        Get
            Return L
        End Get
    End Property
    ReadOnly Property Tangent As Double
        Get
            Return T
        End Get
    End Property
    ReadOnly Property TangCurvDiffer As Double
        Get
            Return Q
        End Get
    End Property
    ReadOnly Property OutVectDis As Double
        Get
            Return Ev
        End Get
    End Property
    ReadOnly Property TangVertiDis As Double
        Get
            Return P
        End Get
    End Property
    ReadOnly Property OutAlleviAng As Double
        Get
            Return B
        End Get
    End Property

    ReadOnly Property InterMovi As Double
        Get
            Return m
        End Get
    End Property
    ReadOnly Property RoadEnd As RoadPoint
        Get
            If RoadEndPt.Lab = "EndPt" Then
                Return RoadEndPt
            Else
                Return HZ
            End If
        End Get
    End Property

    Sub CurvElemCalcu()
        a1 = Azimuth(RoadStaPt, JD) : a2 = Azimuth(JD, RoadEndPt)
        a = a2 - a1

        If a > PI Then
            a = a - 2 * PI
        ElseIf a < -PI Then
            a = a + 2 * PI
        End If

        If a > 0 Then
            ZX = False '右转向
        Else
            ZX = True '左转向
        End If

        a = Abs(a)
        m = Ls / 2 - Ls ^ 3 / (240 * R ^ 2) + Ls ^ 5 / (34560 * R ^ 4)
        P = Ls ^ 2 / (24 * R) - Ls ^ 4 / (2688 * R ^ 3)
        B = Ls / (2 * R)
        T = m + (R + P) * Tan(a / 2)
        L = a * R + Ls
        Ev = (R + P) * (1 / Cos(a / 2)) - R
        Q = 2 * T - L

        JD.LC = RoadStaPt.LC + Distance(RoadStaPt, JD)
        ZH.Name = "ZH" : ZH.Lab = mLab
        ZH.LC = JD.LC - T
        ZH.X = JD.X + T * Cos(a1 + PI)
        ZH.Y = JD.Y + T * Sin(a1 + PI)

        HY.Name = "HY" : HY.Lab = mLab
        HY.LC = ZH.LC + Ls

        QZ.Name = "QZ" : QZ.Lab = mLab
        QZ.LC = ZH.LC + L / 2

        YH.Name = "YH" : YH.Lab = mLab
        YH.LC = ZH.LC + L - Ls

        HZ.Name = "HZ" : HZ.Lab = mLab
        HZ.LC = YH.LC + Ls
        HZ.X = JD.X + T * Cos(a2)
        HZ.Y = JD.Y + T * Sin(a2)

        Dim CurvPt As New RoadPoint
        Dim AftCurvPt As New RoadPoint

        CurvPt.X = Ls - Ls ^ 3 / (40 * R ^ 2) + Ls ^ 5 / (3456 * R ^ 4)
        CurvPt.Y = Ls ^ 2 / (6 * R) - Ls ^ 4 / (336 * R ^ 3)
        AftCurvPt = CoorTransCalcu(CurvPt, ZH, JD, ZX)
        HY.X = AftCurvPt.X : HY.Y = AftCurvPt.Y

        CurvPt.X = m + R * Sin(a / 2)
        CurvPt.Y = P + R * (1 - Cos(a / 2))
        AftCurvPt = CoorTransCalcu(CurvPt, ZH, JD, ZX)
        QZ.X = AftCurvPt.X : QZ.Y = AftCurvPt.Y

        CurvPt.X = m + R * Sin(a - B)
        CurvPt.Y = P + R * (1 - Cos(a - B))
        AftCurvPt = CoorTransCalcu(CurvPt, ZH, JD, ZX)
        YH.X = AftCurvPt.X : YH.Y = AftCurvPt.Y

        If Lab = "YQX" Then
            ZH.Name = "ZY" : HZ.Name = "YZ"
            HY = ZH : YH = HZ
        End If
    End Sub

    Sub MileCoorCalcu()
        Dim CurvPt As New RoadPoint
        Dim AftCurvPt As New RoadPoint
        Dim Mileage As Double
        If RoadEndPt.Lab = "EndPt" Then
            Mileage = HZ.LC + Distance(HZ, RoadEndPt)
            RoadEndPt.LC = Mileage
        Else
            Mileage = HZ.LC
        End If

        ReDim MileStake(0)
        MileStake(0).LC = (Fix(RoadStaPt.LC / StakeSpac) + 1) * StakeSpac
        MileStake(0).Lab = mLab
        Dim I As Integer = 0
        Do
            With MileStake(I)
                If .LC < ZH.LC Then
                    .X = RoadStaPt.X + (.LC - RoadStaPt.LC) * Cos(a1)
                    .Y = RoadStaPt.Y + (.LC - RoadStaPt.LC) * Sin(a1)
                    .Lab = "Line"
                ElseIf .LC < HY.LC Then
                    Dim l As Double = .LC - ZH.LC
                    CurvPt.X = l - l ^ 3 / (40 * R ^ 2) + l ^ 5 / (3456 * R ^ 4)
                    CurvPt.Y = l ^ 2 / (6 * R) - l ^ 4 / (336 * R ^ 3)
                    AftCurvPt = CoorTransCalcu(CurvPt, ZH, JD, ZX)
                    .X = AftCurvPt.X
                    .Y = AftCurvPt.Y
                ElseIf .LC < YH.LC Then
                    Dim c As Double
                    c = (.LC - HY.LC) / R + B
                    CurvPt.X = m + R * Sin(c)
                    CurvPt.Y = P + R * (1 - Cos(c))
                    AftCurvPt = CoorTransCalcu(CurvPt, ZH, JD, ZX)
                    .X = AftCurvPt.X
                    .Y = AftCurvPt.Y
                ElseIf .LC < HZ.LC Then
                    Dim l As Double = HZ.LC - .LC
                    CurvPt.X = l - l ^ 3 / (40 * R ^ 2) + l ^ 5 / (3456 * R ^ 4)
                    CurvPt.Y = l ^ 2 / (6 * R) - l ^ 4 / (336 * R ^ 3)
                    AftCurvPt = CoorTransCalcu(CurvPt, HZ, JD, Not (ZX))
                    .X = AftCurvPt.X
                    .Y = AftCurvPt.Y
                Else
                    If RoadEndPt.Lab = "EndPt" Then
                        .X = HZ.X + (.LC - HZ.LC) * Cos(a2) : .Y = HZ.Y + (.LC - HZ.LC) * Sin(a2)
                        .Lab = "Line"
                    End If
                End If
            End With
            I += 1
            ReDim Preserve MileStake(I)
            MileStake(I).LC = MileStake(I - 1).LC + StakeSpac
            MileStake(I).Lab = mLab
        Loop Until MileStake(I - 1).LC > Mileage
        ReDim Preserve MileStake(I - 2)
    End Sub
End Class
