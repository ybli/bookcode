#ifndef _DATAFILE_H_
#define _DATAFILE_H_

#include <string>
#include <vector>
#include "utils.h"

#pragma once


typedef struct tagDATAHEAR1  //观测数据第一行
{
	unsigned char SatPrn;
	long double X;
	long double Y;
	long double Z;
	tagDATAHEAR1()
	{
		X = 0.0;
		Y = 0.0;
		Z = 0.0;
	}
} tagDATAHEAR1;

typedef struct tagDATAHEAR2  //观测数据第二行
{
	unsigned char SatPrns;
	unsigned char SatS;
	unsigned char SatX;
	unsigned char SatY;
	unsigned char SatZ;
	unsigned char SatCLOCK;
	unsigned char SatELEVATION;
	unsigned char SatC1;
	unsigned char SatTrop;

} tagDATAHEAR2;

class CSPP
{
public:
	int m_SymbolSN;   //Satellite Number
	int m_GPSTIME;    //GPStime
	vector<string> m_Symbol;    //C10-C32
	vector<double> m_X, m_Y, m_Z;//卫星所处的位置
	vector<float> m_Clock;//卫星钟改正数
	vector<float> m_Elevation;//卫星高度角
	vector<double> m_Cl;//卫星钟
	vector<float> m_TtopDelay;//卫星延迟
	
};

bool DataRead(CString strpath,vector<CSPP> data);

void DataOut(CListCtrl m_Grid);

#endif