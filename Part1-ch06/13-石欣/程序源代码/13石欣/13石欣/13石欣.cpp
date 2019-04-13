// 13石欣.cpp : 定义控制台应用程序的入口点。
//

#include "stdafx.h"

#define M 64
#include <iostream>
#include <fstream>
#include <iomanip>
using namespace std;

struct Point							//读入的点数据，分别为点名、x坐标、y坐标
{
	char name[4];
	double x;
	double y;
};

struct sanjiaoxing						//存放三角形信息，依次为三角形三条边的长度和面积
{
	double s1;
	double s2;
	double s3;
	double AREA;
};
void read(char *path, Point *data, int *n);

double dist(int point1, int point2, Point *data);

double Area(char point1, char point2, char point3, Point *data, sanjiaoxing *sjx, int xuhao);

void write(char *path, int n, double *area, double sum_area, sanjiaoxing *sjx);

int _tmain(int argc, _TCHAR* argv[])
{
	int n;									//统计点个数
	double sum_area=0, area[M];				//分别代表总面积和6个三角形的面积数组
	Point data[M];							//存放读入点数据的结构体数组
	sanjiaoxing sjx[M];						//存放三角形边长和面积的结构体数组
	read("coord.txt", data, &n);			//读入coord.txt中的点数据，存放在data[M]的结构体数组中，返回读入总点数n
	area[0] = Area('A', 'B', 'H', data, sjx, 0);	//依次计算六个三角形的面积，存放入面积数组area[M]中
	area[1] = Area('B', 'C', 'H', data, sjx, 1);
	area[2] = Area('C', 'H', 'G', data, sjx, 2);
	area[3] = Area('C', 'D', 'G', data, sjx, 3);
	area[4] = Area('D', 'G', 'F', data, sjx, 4);
	area[5] = Area('D', 'F', 'E', data, sjx, 5);
	for (int i = 0; i < 6; i++)				//计算总面积sum_area
		sum_area += area[i];
	write("result.txt", 6, area, sum_area,sjx);	//将计算结果写入result.txt文件中
	cout << "计算结果已存入项目根目录的result.txt文件中。" << endl;
	return 0;
}

void read(char *path, Point *data, int *n)
{
	char line[M];							//临时存放读入一行数据的字符数组
	ifstream fp(path, ios::in);				//读入数据
	if (!fp)								//文件未能正确打开提示
	{
		cerr << "OPEN ERROR!" << endl;
		exit(1);
	}
	for (int i = 0;; i++)
	{
		fp.getline(line, M, '\n');			//读入一行数据存放到line[m]数组
		for (int j = 0; j < M; j++)
		{
			if (line[j] == ',')		line[j] = ' ';
			if (line[j] == '\0')	break;
		}
		if (i>0 && sscanf_s(line, "%s %lf %lf", &data[i - 1].name, _countof(data[i - 1].name), &data[i - 1].x, &data[i - 1].y) != 3)		//当无法写入3个数据时跳出循环，判断为结束
		{
			*n = i;							//统计总的读入数据个数
			break;
		}
	}
	fp.close();								//关闭文件
}

double dist(int point1, int point2, Point *data)
{
	double D, dx, dy;							//分别表示距离D、x增量、y增量
	dx = data[point1].x - data[point2].x;	//计算x增量
	dy = data[point1].y - data[point2].y;	//计算y增量
	D = sqrt(dx*dx + dy*dy);				//计算距离
	return D;
}

double Area(char point1, char point2, char point3, Point *data ,sanjiaoxing *sjx,int xuhao)
{
	int tmp1, tmp2, tmp3;					//将点号转换为读入结构体数组中的下标的整数
	double S1, S2, S3, s, area;				//分别表示三角形的三条边长，s为海伦公式中的中间变量，area为该三角形面积
	tmp1 = point1 - 65;						//将点号转换为整数下标，A对应的ASC码为65
	tmp2 = point2 - 65;
	tmp3 = point3 - 65;
	sjx[xuhao].s1 = dist(tmp1, tmp2, data);				//调用dist函数，求两点之间的边长
	sjx[xuhao].s2 = dist(tmp1, tmp3, data);
	sjx[xuhao].s3 = dist(tmp2, tmp3, data);
	s = (sjx[xuhao].s1 + sjx[xuhao].s2 + sjx[xuhao].s3) / 2;					//按照海伦公式计算三角形面积
	sjx[xuhao].AREA = sqrt(s*(s - sjx[xuhao].s1)*(s - sjx[xuhao].s2)*(s - sjx[xuhao].s3));
	return sjx[xuhao].AREA;
}

void write(char *path, int n, double *area, double sum_area,sanjiaoxing *sjx)
{
	ofstream fp(path, ios::out);			//新建文件
	if (!fp)								//文件未能新建的报错提示
	{
		cerr << "ERROR!" << endl;
		exit(1);
	}
	fp << "三角形序号" << "    " << "边1的长度(m)" << "    " << "边2的长度(m)" << "     " << "边3的长度(m)"<<"       "<<"面积(m²)"<<endl;
	for (int i = 0; i < n; i++)
		fp << fixed << setprecision(3) << i + 1 << setw(18) << sjx[i].s1 << setw(18) << sjx[i].s2 << setw(18) << sjx[i].s3 <<setw(18)<< area[i] << endl;
	fp << "地块总面积：" << sum_area << "平方米" << endl;
}