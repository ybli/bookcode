#include<stdio.h>
#include"Consts.h"
#include"Structs.h"

void FileRead(FILE* fp,XYZ* xyz,int* num)
{
	char buff[MAXRAWLEN];

	//读取坐标点数
	fgets(buff,MAXRAWLEN,fp);
	sscanf(buff,"%d",num);
	//读取坐标信息
	for(int i=0;i<*num;i++)
	{
		fgets(buff,5,fp);
		sscanf(buff,"%s",&((xyz+i)->name));
		fgets(buff,50,fp);
		sscanf(buff,"%lf %lf %lf",&((xyz+i)->X),&((xyz+i)->Y),&((xyz+i)->Z));
	}
	
}