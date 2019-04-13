#include "CDefine.h"


CVect3 operator-(const CVect3& v1,const CVect3& v2)
{
	CVect3 res;
	res.a=v1.a-v2.a;
	res.b=v1.b-v2.b;
	res.c=v1.c-v2.c;
	return res;
}


CFile::CFile(const string& filename)
{
	ifstream infile(filename);
	if(!infile.is_open())
	{
		cout<<"文件读取失败"<<endl;
	}

	string LineBuffer;
	stringstream ss;

	getline(infile,LineBuffer);
	count=atoi(LineBuffer.c_str());

	while(!infile.eof())
	{
		Point P_tmp;
		getline(infile,LineBuffer);
		if(LineBuffer=="")break;

		ss<<LineBuffer;
		ss>>P_tmp.name;
		ss>>P_tmp.Pos.a;
		ss>>P_tmp.Pos.b;
		ss>>P_tmp.Pos.c;

		Vec_XYZ.push_back(P_tmp);
		ss.clear();
		ss.str("");
	}
	cout<<"文件读取结束"<<endl;
	infile.close();
}
