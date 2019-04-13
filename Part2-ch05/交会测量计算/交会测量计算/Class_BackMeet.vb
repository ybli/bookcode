Imports System.Math
Friend Class BackMeet

    Dim mPtA As ContrPoint
    Dim mPtB As ContrPoint
    Dim mPtC As ContrPoint
    Dim mPtP As New ContrPoint
    Dim mAngA As Double
    Dim mAngB As Double
    Dim mAngC As Double

    Dim mAuxiParam As String

    Sub New(PtName As String, PtA As ContrPoint, PtB As ContrPoint, PtC As ContrPoint, AngA As Double, AngB As Double, AngC As Double)
        mPtP.Name = PtName
        mPtP.Type = 0
        mPtA = PtA
        mPtB = PtB
        mPtC = PtC
        mAngA = AngA
        mAngB = AngB
        mAngC = AngC
    End Sub
    ReadOnly Property PtP As ContrPoint
        Get
            CalcuBackMeetPt()
            Return mPtP
        End Get
    End Property
    ReadOnly Property AuxiParam As String
        Get
            If mAuxiParam = "" Then
                MsgBox("请先进行交会点计算！")
                Return "-----"
            Else
                Return mAuxiParam
            End If
        End Get
    End Property
    ReadOnly Property DangerCir As String
        Get
            If mAuxiParam = "" Then
                MsgBox("请先进行交会点计算！")
                Return "-----"
            Else
                Return CalcuDangerCir()
            End If
        End Get
    End Property
    Private Sub CalcuBackMeetPt()
        Dim angA As Double = Azimuth(mPtA, mPtC) - Azimuth(mPtA, mPtB)
        Dim angB As Double = Azimuth(mPtB, mPtA) - Azimuth(mPtB, mPtC)
        Dim angC As Double = Azimuth(mPtC, mPtB) - Azimuth(mPtC, mPtA)

        Dim CotAA# = 1 / Tan(angA)
        Dim CotBB# = 1 / Tan(angB)
        Dim CotCC# = 1 / Tan(angC)

        Dim CotVA# = 1 / Tan(mAngA)
        Dim CotVB# = 1 / Tan(mAngB)
        Dim CotVC# = 1 / Tan(mAngC)

        Dim PA# = 1 / (CotAA - CotVA)
        Dim PB# = 1 / (CotBB - CotVB)
        Dim PC# = 1 / (CotCC - CotVC)

        mAuxiParam = String.Format("PA:{0:f10},PB:{1:f10},PC:{2:f10}", PA, PB, PC)

        mPtP.CoorX = (PA * mPtA.CoorX + PB * mPtB.CoorX + PC * mPtC.CoorX) / (PA + PB + PC)
        mPtP.CoorY = (PA * mPtA.CoorY + PB * mPtB.CoorY + PC * mPtC.CoorY) / (PA + PB + PC)

    End Sub
    Private Function CalcuDangerCir() As String

        Dim RA2 As Double = mPtB.CoorX ^ 2 + mPtB.CoorY ^ 2 - mPtC.CoorX ^ 2 - mPtC.CoorY ^ 2
        Dim RB2 As Double = mPtC.CoorX ^ 2 + mPtC.CoorY ^ 2 - mPtA.CoorX ^ 2 - mPtA.CoorY ^ 2
        Dim RC2 As Double = mPtA.CoorX ^ 2 + mPtA.CoorY ^ 2 - mPtB.CoorX ^ 2 - mPtB.CoorY ^ 2

        Dim CirCent As New ContrPoint

        CirCent.CoorX = -(mPtA.CoorY * RA2 + mPtB.CoorY * RB2 + mPtC.CoorY * RC2) / 2 /
                         (mPtA.CoorX * (mPtB.CoorY - mPtC.CoorY) +
                          mPtB.CoorX * (mPtC.CoorY - mPtA.CoorY) +
                          mPtC.CoorX * (mPtA.CoorY - mPtB.CoorY))
        CirCent.CoorY = -(mPtA.CoorX * RA2 + mPtB.CoorX * RB2 + mPtC.CoorX * RC2) / 2 /
                         (mPtA.CoorY * (mPtB.CoorX - mPtC.CoorX) +
                          mPtB.CoorY * (mPtC.CoorX - mPtA.CoorX) +
                          mPtC.CoorY * (mPtA.CoorX - mPtB.CoorX))

        Dim Dr As Double
        Dr = Distance(CirCent, mPtA)

        Dim Pr As Double
        Pr = Distance(CirCent, mPtP)

        Dim DangerDegree As Single = 1 - Abs(((Pr - Dr) / Dr))
        If DangerDegree > 0.8 Then
            Return DangerDegree.ToString("%0.00 ") & "> %80  危险！"
        Else
            Return DangerDegree.ToString("%0.00 ") & "< %80  正常！"
        End If

    End Function
End Class
