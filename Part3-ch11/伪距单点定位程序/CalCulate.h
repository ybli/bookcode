#ifndef _CALCULATE_H_
#define _CALCULATE_H_

#pragma once

#include <math.h>
#include <string>
#include <vector>

typedef struct satresults
{
	double X;
	double Y;
	double Z;
	double mx;
	double my;
	double mz;
	double PDOP;
	satresults()
	{
		X = 0.0;
		Y = 0.0;
		Z = 0.0;
		mx=0.0;
		my=0.0;
		mz=0.0;
		PDOP=0.0;
	}
} satresults;

void calculate(CString strpath,vector<CSPP> dataList,satresults& result);

#endif