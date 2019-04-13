Imports System.Math
Friend Class DistMeet
    Dim mPtA As ContrPoint
    Dim mPtB As ContrPoint
    Dim mPtP As New ContrPoint
    Dim mDisA As Double
    Dim mDisB As Double
    Sub New(PtName As String, PtA As ContrPoint, PtB As ContrPoint, DisA As Double, DisB As Double)
        mPtP.Name = PtName
        mPtP.Type = 0
        mPtA = PtA
        mPtB = PtB
        mDisA = DisA
        mDisB = DisB
    End Sub
    ReadOnly Property PtP As ContrPoint
        Get
            CalcuDistMeetPt()
            Return mPtP
        End Get
    End Property
    Private Sub CalcuDistMeetPt()
        Dim DistAB As Double = Distance(mPtA, mPtB)
        Dim AzimAB As Double = Azimuth(mPtA, mPtB)

        Dim CosA As Double = (DistAB * DistAB + mDisA * mDisA - mDisB * mDisB) / (2 * DistAB * mDisA)
        Dim SinA As Double = Sqrt(1 - CosA * CosA)

        Dim u As Double = mDisA * CosA
        Dim v As Double = mDisA * SinA

        mPtP.CoorX = mPtA.CoorX + u * Cos(AzimAB) + v * Sin(AzimAB)
        mPtP.CoorY = mPtA.CoorY + u * Sin(AzimAB) - v * Cos(AzimAB)

    End Sub
End Class
