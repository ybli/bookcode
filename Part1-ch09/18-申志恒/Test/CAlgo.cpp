#include "CAlgo.h"
#include <iomanip>


CVect3 CAlgo::XYZ2BLH(const CVect3& XYZ)
{
	// a b c 按顺序分别代表 X Y Z 或 N E U 或 B L H 
	CVect3 BLH;
	BLH.b=atan2(XYZ.b,XYZ.a);
	
	double SX2Y2=sqrt(XYZ.a*XYZ.a+XYZ.b*XYZ.b);
	double theta=atan2(XYZ.c*a,SX2Y2*b);
	
	double up=XYZ.c+e2_*b*sin(theta)*sin(theta)*sin(theta);
	double down=SX2Y2-e2*a*cos(theta)*cos(theta)*cos(theta);
	BLH.a=atan2(up,down);

	double N=a/sqrt(1-e2*sin(BLH.a)*sin(BLH.a));
	BLH.c=SX2Y2/cos(BLH.a) - N;

	return BLH;
}

CVect3 CAlgo::XYZ2NEU(const CVect3& XYZ1,const CVect3& XYZ2)
{
	// a b c 按顺序分别代表 X Y Z 或 N E U 或 B L H 
	CVect3 NEU_res;
	CVect3 BLH=XYZ2BLH(XYZ1);
	double T11=-sin(BLH.a)*cos(BLH.b), T12=-sin(BLH.a)*sin(BLH.b), T13=cos(BLH.a), 
		   T21=-sin(BLH.b),            T22=cos(BLH.b),             T23=0,
		   T31=cos(BLH.a)*cos(BLH.b),  T32=cos(BLH.a)*sin(BLH.b),  T33=sin(BLH.a);

	CVect3 delta_Pos=XYZ2-XYZ1;

	NEU_res.a=T11*delta_Pos.a+T12*delta_Pos.b+T13*delta_Pos.c;
	NEU_res.b=T21*delta_Pos.a+T22*delta_Pos.b+T23*delta_Pos.c;
	NEU_res.c=T31*delta_Pos.a+T32*delta_Pos.b+T33*delta_Pos.c;

	return NEU_res;

}

void CAlgo::Run(const vector<Point>& Vec_XYZ)
{
	// a b c 按顺序分别代表 X Y Z 或 N E U 或 B L H 
	CVect3 XYZ0(Vec_XYZ[0].Pos.a,Vec_XYZ[0].Pos.b,Vec_XYZ[0].Pos.c);
	for(int i=0;i,i<Vec_XYZ.size();i++)
	{
		Point NEU_tmp;
		CVect3 XYZi(Vec_XYZ[i].Pos.a,Vec_XYZ[i].Pos.b,Vec_XYZ[i].Pos.c);
		NEU_tmp.name=Vec_XYZ[i].name;
		NEU_tmp.Pos=XYZ2NEU(XYZ0,XYZi);
		P_res.push_back(NEU_tmp);
	}
}

void CAlgo::SaveFile(const string& filename)
{
	// a b c 按顺序分别代表 X Y Z 或 N E U 或 B L H 
	ofstream os(filename);

	os<<"-------------------------空间直角坐标（XYZ）转换为站心直角坐标（NEU）计算结果---------------------------"<<endl;
	os<<P_res.size()<<endl;  // 点的数量
	for(int i=0;i<P_res.size();i++)
	{
		os<<P_res[i].name<<' ';
		os<<setw(11)<<fixed<<setprecision(4)<<P_res[i].Pos.a<<' '<<P_res[i].Pos.b<<' '<<P_res[i].Pos.c<<endl;
	}

	os.close();



}