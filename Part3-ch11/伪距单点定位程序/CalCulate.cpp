#include "stdafx.h"
#include "DataFile.h"
#include "utils.h"
#include "CalCulate.h"
#include "Matrix.h"

#include <fstream>
#include <cstdio>
#include <stdlib.h>
#include <vector>
#include <math.h>


void calculate(CString strpath,vector<CSPP> dataList,satresults& result)
{
	string approx = "APPROX_POSITION£º";
	string sate_num = "Satellite Number:";
	string bds_time = "BDS time£º";

	string file_name = CStr_to_str(strpath);
	ifstream s;
	s.open(file_name,ios::in);

	string line;
	vector<string> str_list;
	size_t idx;
	getline(s,line);

	//APPROX_POSITION
	string_split(line,",",str_list);
				
	tagDATAHEAR1 head1;
	idx = str_list[0].find(approx);		  
	head1.X = stod(str_list[0].substr(approx.size()));
	head1.Y = stod(str_list[1]);
	idx = str_list[2].find("(m)");		  
	head1.Z = stod(str_list[2].substr(0,idx));

	vector<vector<double>> res6(4,vector<double>(1));
	vector<vector<double>> res3(4,vector<double>(4));
	const int N = dataList.size();
	for (int pos = 0; pos < N; pos++)
	{
		int sum=dataList[pos].m_SymbolSN;
		vector<double> R0(sum);
		vector<double> l(sum);
		vector<double> m(sum);
		vector<double> n(sum);
		for (int i = 0; i < sum; i++)
		{
			R0[i] = sqrtl(pow((dataList[pos].m_X[i] - head1.X), 2) + pow((dataList[pos].m_Y[i] - head1.Y), 2) + pow((dataList[pos].m_Z[i] - head1.Z), 2));
			l[i] = (dataList[pos].m_X[i] - head1.X) / R0[i];
			m[i] = (dataList[pos].m_Y[i] - head1.Y) / R0[i];
			n[i] = (dataList[pos].m_Z[i] - head1.Z) / R0[i];
		}
		vector<vector<double>> B(sum);
		for(int i=0;i<sum;i++)
		{
			B[i].resize(4);
		}
		for (int i = 0; i < sum; i++)
		{
			B[i][0] = l[i];
			B[i][1] = m[i];
			B[i][2] = n[i];
			B[i][3] = -1;

		}
			
		vector<vector<double>> L(1);
		for(int i=0;i<1;i++)
		{
			L[i].resize(sum);
		}
		for (int i = 0; i < sum; i++)
		{
			L[0][i] = dataList[pos].m_Cl[i] - R0[i] + dataList[pos].m_Clock[i] - dataList[pos].m_TtopDelay[i];
		}
		vector<double> m_CL(sum);
		for (int i = 0; i < sum; i++)
		{
			m_CL[i] = dataList[pos].m_Cl[i];
		}

		vector<double> p(sum);
		vector<vector<double>> P(sum);
		for(int i=0;i<sum;i++)
		{
			P[i].resize(sum);
		}
		for (int i = 0; i < sum; i++)
		{
			p[i] = sin(dataList[pos].m_Elevation[i] * 3.1415926 / 180) / 0.04;
		}
		for (int i = 0; i < sum; i++)
		{
			for (int j = 0; j < sum; j++)
			{
				if (i == j)
					P[i][j] = p[i];
				else
					P[i][j] = 0;
					
			}
		}

		vector<vector<double>> TL(sum,vector<double>(1));
		TL=operator_converse(L);
			
		vector<vector<double>> TB(4,vector<double>(sum));
		TB=operator_converse(B);
			
		vector<vector<double>> res1(4,vector<double>(sum));
		res1=operator_multiply(TB,P);
			
		vector<vector<double>> res2(4,vector<double>(4));
		res2=operator_multiply(res1,B);

		//Ð­·½²î¾ØÕó
		res3=operator_inv(res2);

		vector<vector<double>> res4(4,vector<double>(sum));
		res4=operator_multiply(res3,TB);

		vector<vector<double>> res5(4,vector<double>(sum));
		res5=operator_multiply(res4,P);

		//Æ«ÒÆÁ¿
		res6=operator_multiply(res5,TL);

		vector<vector<double>> D(4,vector<double>(1));
		D=operator_number(res6,-1.0);

		vector<vector<double>> V(sum,vector<double>(1));

		V=operator_add(operator_multiply(B,D),TL);

		vector<vector<double>> res7(1,vector<double>(sum));
		res7=operator_multiply(operator_converse(V),P);

		vector<vector<double>> res8(1,vector<double>(1));
		res8=operator_multiply(res7,V);

		result.X=head1.X+D[0][0];
		result.Y=head1.Y+D[1][0];
		result.Z=head1.Z+D[3][0];

		double m0=sqrtl(res8[0][0]/(sum-4));

		result.mx=m0*res3[0][0];
		result.my=m0*res3[1][1];
		result.mz=m0*res3[2][2];

		result.PDOP=sqrtl(res3[0][0]*res3[0][0]+res3[1][1]*res3[1][1]+res3[2][2]*res3[2][2]);

		}

}