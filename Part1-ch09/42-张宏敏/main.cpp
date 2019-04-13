#include"Consts.h"
#include"Declare.h"
#include"Structs.h"
#include<stdio.h>



void main()
{
	XYZ xyz[6];
	BLH blh;
	NEU neu[6];
	double xyz1[3];
	int num;
	FILE* fp;
	FILE* fp1;

	//打开数据文件
	if((fp=fopen("XYZ.dat","r"))==NULL)
	{
		printf("文件打开错误！！！");
	}

	//打开结果文件
	if((fp1=fopen("NEU.dat","w"))==NULL)
	{
		printf("文件打开错误！！！");
	}

	//读数据
	FileRead(fp,xyz,&num);

	//将第一个点的坐标转换为大地坐标
	XYZ2BLH(xyz,&blh);
	xyz1[0]=xyz[0].X;
	xyz1[1]=xyz[0].Y;
	xyz1[2]=xyz[0].Z;

	fprintf(fp1,"点号\tN(m)\t\tE(m)\t\tU(m)\n");

	//将其余点的坐标转换为站心坐标
	for(int j=0;j<num;j++)
	{
		XYZ2NEU(xyz+j,neu+j,xyz1,&blh);
		fprintf(fp1,"%s\t",(xyz+j)->name);
		fprintf(fp1,"%lf\t%lf\t%lf\n",(neu+j)->N,(neu+j)->E,(neu+j)->U);
	}
	//system("pause");
	fclose(fp);
	fclose(fp1);
}