#ifndef STRUCTS_H
#define Structs_H
#include<stdio.h>

//大地坐标
struct BLH
{
	double B;
	double L;
	double H;
};

//WGS84坐标
struct XYZ
{
	char name[5];
	double X;
	double Y;
	double Z;
};

//站心坐标
struct NEU
{
	double N;
	double E;
	double U;
};

//函数申明
void FileRead(FILE* fp,XYZ* xyz,int* num);
void XYZ2BLH(XYZ* xyz,BLH* blh);
void Multiply(int m1,int n1,int m2,int n2,double A[],double B[],double* C );
void XYZ2NEU(XYZ* xyz,NEU*neu,double* xyz1,BLH *blh);

#endif