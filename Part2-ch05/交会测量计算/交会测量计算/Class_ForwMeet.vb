Imports System.Math
Friend Class ForwMeet
    Dim mPtA As ContrPoint
    Dim mPtB As ContrPoint
    Dim mPtP As New ContrPoint
    Dim mAngA As Double
    Dim mAngB As Double
    Sub New(PtName As String, PtA As ContrPoint, PtB As ContrPoint, AngA As Double, AngB As Double)
        mPtP.Name = PtName
        mPtP.Type = 0
        mPtA = PtA
        mPtB = PtB
        mAngA = AngA
        mAngB = AngB
    End Sub
    ReadOnly Property PtP As ContrPoint
        Get
            CalcuForwMeetPt()
            Return mPtP
        End Get
    End Property
    Private Sub CalcuForwMeetPt()
        Dim CotanA# = 1 / Tan(mAngA)
        Dim CotanB# = 1 / Tan(mAngB)
        mPtP.CoorX = (mPtA.CoorX * CotanB + mPtB.CoorX * CotanA - mPtA.CoorY + mPtB.CoorY) / (CotanA + CotanB)
        mPtP.CoorY = (mPtA.CoorY * CotanB + mPtB.CoorY * CotanA + mPtA.CoorX - mPtB.CoorX) / (CotanA + CotanB)
    End Sub
End Class
