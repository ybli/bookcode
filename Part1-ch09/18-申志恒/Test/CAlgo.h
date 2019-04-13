#ifndef CALGO_H
#define CALGO_H

#include "CDefine.h"
#include <fstream>



class CAlgo
{
public:
	vector<Point> P_res;

	CAlgo(){};

	CVect3 XYZ2BLH(const CVect3& XYZ);

	CVect3 XYZ2NEU(const CVect3& XYZ1,const CVect3& XYZ2);

	void Run(const vector<Point>& Vec_XYZ);

	void SaveFile(const string& filename);

};




#endif