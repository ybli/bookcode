Module Coordinate
    Implicit none  
    type Topo
        real(8) :: N 
        real(8) :: E
        real(8) :: U
    end type Topo
    type Coor
        Character(4) :: Name
        real(8) :: X
        real(8) :: Y
        real(8) :: Z
    end type Coor
    type Geo
        real(8) :: B
        real(8) :: L
        real(8) :: H
    end type Geo
    real(8),Parameter :: a=6378137.0
    real(8),Parameter :: f=1.0/298.257223563
    real(8),Parameter :: b=a-a*f
    real(8),Parameter :: e2=2*f-f**2
    real(8),Parameter :: e12=(a**2-b**2)/b**2
    Contains
    Subroutine Coordinate2Geodetic(Coor_0,Geo_0)
         type(Coor) :: Coor_0
         type(Geo) :: Geo_0
         real(8) :: theta
         real(8) :: N
         theta=ATAN2(Coor_0.Z*a,b*SQRT(Coor_0.X**2+Coor_0.Y**2))
         Geo_0.L=ATAN2(Coor_0.Y,Coor_0.X)
         Geo_0.B=ATAN2(Coor_0.Z+e12*b*SIN(theta)**3,(SQRT(Coor_0.X**2+Coor_0.Y**2)-e2*a*COS(theta)**3))
         N=a/(SQRT(1-e2*SIN(Geo_0.B)**2))
         Geo_0.H=SQRT(Coor_0.X**2+Coor_0.Y**2)/COS(Geo_0.B)-N
    End Subroutine Coordinate2Geodetic

    Subroutine Coordinate2CopoCentric(Coor_0,Geo_0,Coor_1,Geo_1,Topo_0)
        type(Coor) :: Coor_0,Coor_1
        type(Geo) :: Geo_0,Geo_1
        type(Topo) :: Topo_0
        Integer :: I=1
        real(8) :: T(3,3)
        real(8) :: Delta(3,1),ANS(3,1)
        T(1,1)=-SIN(Geo_0.B)*COS(Geo_0.L)
        T(1,2)=-SIN(Geo_0.B)*SIN(Geo_0.L)
        T(1,3)=COS(Geo_0.B)
        T(2,1)=-SIN(Geo_0.L)
        T(2,2)=COS(Geo_0.L)
        T(2,3)=0
        T(3,1)=COS(Geo_0.B)*COS(Geo_0.L)
        T(3,2)=COS(Geo_0.B)*SIN(Geo_0.L)
        T(3,3)=SIN(Geo_0.B)
        Delta(1,1)=Coor_1.X-Coor_0.X
        Delta(2,1)=Coor_1.Y-Coor_0.Y
        Delta(3,1)=Coor_1.Z-Coor_0.Z
        Do i=1,3
            ANS(i,1)=T(i,1)*Delta(1,1)+T(i,2)*Delta(2,1)+T(i,3)*Delta(3,1)
        End Do
        Topo_0.N=ANS(1,1)
        Topo_0.E=ANS(2,1)
        Topo_0.U=ANS(3,1)
    End Subroutine Coordinate2CopoCentric
End Module Coordinate