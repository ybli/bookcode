#include "DataCenter.h"

#define n pInfo.size                                                 // for matrix convient definition
HeightFitting *pHfit = NULL;
//vector<string> netName;
/********************************************************************************************************/
//                            @ Private functions


/*-------------------------------------------------------------------
 * Name : adjument
 * Func : the main function of the adjustment
 * Args : 
 *-----------------------------------------------------------------*/
void HeightFitting::adjument()
{
	int len = pList.size();
	MatrixXd XY(len, 2), H1(len, 1), H2(len, 1);
	for (int i = 0; i < len; i++)
	{
		XY(i, 0) = pList[i].x;
		XY(i, 1) = pList[i].y;
		H1(i) = pList[i].h1;
		H2(i) = pList[i].h2;
	}

	int t = 3;
	int r = len - t;
	//重心化 1代表一列一个平均数，2代表一行一个,此处应为一列一个
	MatrixXd m(1, 2), M0(len, 2), xy(len, 2);
	m = mean(XY);
	for (int i = 0; i < len; i++)
	{
		M0(i, 0) = m(0);
		M0(i, 1) = m(1);
	}
	xy = XY - M0;
	MatrixXd B(len, 3), L(len, 1);
	for (int i = 0; i < len; i++)
	{
		B(i, 0) = 1;
		B(i, 1) = xy(i, 0);
		B(i, 2) = xy(i, 1);
		L(i) = H2(i) - H1(i);
	}
	x.resize(3  , 1);
	v.resize(len, 1);
	X.resize(len, 1);
	//MatrixXd x(3, 1), V(len, 1);
	x = pinv(B.transpose()*B)*B.transpose()*L;
	v = B * x - L;
	MatrixXd te = v.transpose()*v;
    sigma = sqrt(te(0) / r);

	X = v + H1;          //@@@@@@@@@@@@@@@@@@@@@@@@@@@

	DataInOut io;
	io.writePinfo(*this);
}


MatrixXd HeightFitting::mean(MatrixXd m, int dim)
{
	int r, c;
	r = int(m.rows());
	c = int(m.cols());

	MatrixXd an;
	if (dim == 1)
	{
		an.resize(1, c);
		for (int i = 0; i < c; i++)
		{
			an(0, i) = m.col(i).sum() / r;
		}
	}
	else if(dim == 2)
	{

	}

	return an;
}

MatrixXd HeightFitting::pinv(Eigen::MatrixXd  A)
{
	Eigen::JacobiSVD<Eigen::MatrixXd> svd(A, Eigen::ComputeFullU | Eigen::ComputeFullV);//M=USV*
	double  pinvtoler = 1.e-8; //tolerance
	int row = int(A.rows());
	int col = int(A.cols());
	int k = min(row, col);
	Eigen::MatrixXd X = Eigen::MatrixXd::Zero(col, row);
	Eigen::MatrixXd singularValues_inv = svd.singularValues();//奇异值
	Eigen::MatrixXd singularValues_inv_mat = Eigen::MatrixXd::Zero(col, row);
	for (long i = 0; i < k; ++i) {
		if (singularValues_inv(i) > pinvtoler)
			singularValues_inv(i) = 1.0 / singularValues_inv(i);
		else singularValues_inv(i) = 0;
	}
	for (long i = 0; i < k; ++i)
	{
		singularValues_inv_mat(i, i) = singularValues_inv(i);
	}
	X = (svd.matrixV())*(singularValues_inv_mat)*(svd.matrixU().transpose());//X=VS+U*

	return X;
}

