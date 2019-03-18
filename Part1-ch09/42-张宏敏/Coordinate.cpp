#include"Structs.h"
#include"Consts.h"
#include<stdio.h>
#include<math.h>

//WGS84转大地坐标
void XYZ2BLH(XYZ* xyz,BLH* blh)
{
	double e2=2*f-f*f;
	double b;
	double e_2;
	double N;  //卯酉圈半径
	double theta;

	
    b=a*(1-f);	
	e_2=(1-(1-f)*(1-f))/((1-f)*(1-f));
	theta=atan2(xyz->Z*a,b*sqrt(xyz->Y*xyz->Y+xyz->X*xyz->X));

	blh->L=atan2(xyz->Y,xyz->X);
	blh->B=atan2((xyz->Z+e_2*b*sin(theta)*sin(theta)*sin(theta)),(sqrt(xyz->Y*xyz->Y+xyz->X*xyz->X)-e2*a*cos(theta)*cos(theta)*cos(theta)));
	double B=blh->B;
	N=a/sqrt(1-e2*sin(B)*sin(B));
	blh->H=(sqrt(xyz->Y*xyz->Y+xyz->X*xyz->X)/cos(blh->B))-N;
	//
}

//XYZ转换到站心坐标
void XYZ2NEU(XYZ* xyz,NEU*neu,double* xyz1,BLH *blh)
{
	double B1[9];
	double re[3];
	double delta[3];
	double B=blh->B;
	double L=blh->L;
	double H=blh->H;

	//坐标差值
	delta[0]=xyz->X-xyz1[0];
	delta[1]=xyz->Y-xyz1[1];
	delta[2]=xyz->Z-xyz1[2];

	//转换矩阵
	B1[0]=-sin(B)*cos(L);
	B1[1]=-sin(B)*sin(L);
	B1[2]=cos(B);
	B1[3]=-sin(L);
	B1[4]=cos(L);
	B1[5]=0;
	B1[6]=cos(B)*cos(L);
	B1[7]=cos(B)*sin(L);
	B1[8]=sin(B);

	Multiply(3,3,3,1,B1,delta,re);
	neu->N=re[0];
	neu->E=re[1];
	neu->U=re[2];
}

