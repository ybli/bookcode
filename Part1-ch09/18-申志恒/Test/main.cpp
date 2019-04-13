#include <iostream>
#include "CDefine.h"
#include "CAlgo.h"


int main()
{
	CFile file("XYZ.dat");
	CAlgo algo;
	algo.Run(file.Vec_XYZ);
	algo.SaveFile("NEU.dat");
	return 0;
}