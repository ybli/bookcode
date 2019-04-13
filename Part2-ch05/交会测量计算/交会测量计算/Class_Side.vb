Imports System.Math
Friend Class Side
    Public Property ValueObsr() As Double

    Friend PtSta As New ContrPoint '声明私有字段
    Friend PtEnd As New ContrPoint
    Friend DXCoor#, DYCoor#

    Public Sub New(ByRef PtOne As ContrPoint, ByRef PtTwo As ContrPoint) '定义构造函数，
        PtSta = PtOne
        PtEnd = PtTwo
    End Sub

    ReadOnly Property PtS() As ContrPoint
        Get
            Return PtSta
        End Get
    End Property

    ReadOnly Property PtE() As ContrPoint
        Get
            Return PtEnd
        End Get
    End Property
    ReadOnly Property Name As String
        Get
            Return PtSta.Name & "_" & PtEnd.Name
        End Get
    End Property

    ReadOnly Property SideType() As Boolean
        Get
            Return PtSta.Type And PtEnd.Type
        End Get
    End Property

    Function Length() As Double
        DXCoor = PtEnd.CoorX - PtSta.CoorX
        DYCoor = PtEnd.CoorY - PtSta.CoorY
        Length = Sqrt(DXCoor ^ 2 + DYCoor ^ 2)
    End Function

    Function AziAng() As Double
        Dim Azimuth#
        DXCoor = PtEnd.CoorX - PtSta.CoorX
        DYCoor = PtEnd.CoorY - PtSta.CoorY
        If DXCoor = 0 And DYCoor = 0 Then
            MsgBox("起点与终点重合！", 0 + 16 + 0)
            End
        End If

        If DXCoor = 0 Then
            If DYCoor < 0 Then
                Return 3 * PI / 2
            ElseIf DYCoor > 0 Then
                Return PI / 2
            End If
        ElseIf DYCoor = 0 Then
            If DXCoor < 0 Then
                Return PI
            ElseIf DXCoor > 0 Then
                Return 0
            End If
        Else
            Azimuth = Atan(DYCoor / DXCoor)
            If DXCoor < 0 Then
                Return Azimuth + PI
            ElseIf DYCoor < 0 Then
                Return Azimuth + 2 * PI
            Else
                Return Azimuth
            End If
        End If
    End Function

End Class
