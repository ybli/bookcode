#pragma once

#include <vector>
#include <iostream>
#include <math.h>
#include <afxwin.h>                                                  // use MFC
using namespace std;

#include <Eigen\Dense>
using namespace Eigen;

/********************************************************************************************************/
//                            @ Net adjustment-related definition
typedef unsigned int uint;

typedef struct
{
	string name;
	double x;
	double y;
	double h1;
	double h2;
}PointInfo;

typedef struct
{
	
};

class HeightFitting
{
public:
	HeightFitting(){}
   ~HeightFitting(){}

	MatrixXd mean(MatrixXd m, int dim = 1);
	MatrixXd pinv(Eigen::MatrixXd  A);
	void adjument();

public:
	vector<PointInfo> pList;
	MatrixXd x, v ,X;
	double  sigma;
};

extern HeightFitting *pHfit;//////////////////////////
//extern vector<string> netName;
/********************************************************************************************************/
//                            @ Data Input and Output-related definition
class DataInOut
{

public:
	void writePinfo(const HeightFitting &net);
	bool readData  (HeightFitting &net, const CString &filePath);

private:

	FILE* openFile(const char* mode, const char *filePath)
	{
		FILE *fp = NULL;
		if (NULL == (fp = fopen(filePath, mode)))
			cout << "File can't open!";
		return fp;
	}

private:
};

extern CString outFilePath;




