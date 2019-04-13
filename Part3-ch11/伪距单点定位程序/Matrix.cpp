#include "StdAfx.h"
#include "Matrix.h"

#include <algorithm>
#include <vector>


using namespace std;

vector<vector<double>> operator_add (vector<vector<double>> arrA, vector<vector<double>> arrB)
{
	//矩阵加法
    // 矩阵arrA的行数
    int rowA = arrA.size();
    //矩阵arrA的列数  
    int colA = arrA[0].size();
    //矩阵arrB的行数  
    int rowB = arrB.size();
    //矩阵arrB的列数  
    int colB = arrB[0].size();

	vector<vector<double>>  res;
    if ((colA != colB) || (rowA != rowB))//判断矩阵行列是否一致  
    {
       AfxMessageBox("矩阵不可运算");
    }
    else
    {
        //设置结果矩阵的大小，初始化为为0  
        res.resize(rowA);
        for (int i = 0; i < rowA; ++i)
        {
            res[i].resize(colB);
        }

        //矩阵相加  
        for (int i = 0; i < rowA; ++i)
        {
            for (int j = 0; j < colB; ++j)
            {

                res[i][j] = arrA[i][j] + arrB[i][j];
                
            }
        }
    }
	return res;
}

vector<vector<double>> operator_multiply (vector<vector<double>> arrA, vector<vector<double>> arrB)
{
	//矩阵乘法
    //矩阵arrA的行数  
    int rowA = arrA.size();
    //矩阵arrA的列数  
    int colA = arrA[0].size();
    //矩阵arrB的行数  
    int rowB = arrB.size();
    //矩阵arrB的列数  
    int colB = arrB[0].size();
    vector<vector<double>>  res;
 
    if (colA != rowB)//如果矩阵arrA的列数不等于矩阵arrB的行数，
    {
        AfxMessageBox("矩阵不可运算");
    }
    else
    {
        //设置结果矩阵的大小，初始化为为0  
        res.resize(rowA);
        for (int i = 0; i < rowA; ++i)
        {
            res[i].resize(colB);
        }

        //矩阵相乘  
        for (int i = 0; i < rowA; ++i)
        {
            for (int j = 0; j < colB; ++j)
            {
                for (int k = 0; k < colA; ++k)
                {
                    res[i][j] += arrA[i][k] * arrB[k][j];
                }
            }
        }
    }
	return res;
}


vector<vector<double>> operator_number (vector<vector<double>> arr, double m)
{
	//矩阵乘法
    //矩阵arrA的行数  
    int row = arr.size();
    //矩阵arrA的列数  
    int col = arr[0].size();

    vector<vector<double>>  res;
    //设置结果矩阵的大小，初始化为为0  
    res.resize(row);
    for (int i = 0; i < row; ++i)
    {
        res[i].resize(col);
    }

    //矩阵相乘  
    for (int i = 0; i < row; ++i)
    {
        for (int j = 0; j < col; ++j)
        {

            res[i][j] = arr[i][j] * m;
        }
    }

    return res;

}

vector<vector<double>> operator_inv (vector<vector<double>> arrA)
{
	//求逆矩阵
    //矩阵arrA的行数  
    int row = arrA.size();
    //矩阵arrA的列数  
    int col = arrA[0].size();
    if (row != col)
    {
        AfxMessageBox("矩阵不可运算");
    }

	vector<vector<double>>  res;
    res.resize(row);
    for (int i = 0; i < row; ++i)
    {
        res[i].resize(col);
        res[i][i] = 1;//初始化单位阵
    }
    int temp_row = 0;
    double max = 0;
    double ratio = 0;
    for (int i = 0; i < row; i++)
    {
        //列选主元素
        max = arrA[i][i];
        temp_row = i;
        for (int i_change = i; i_change < row; i_change++)
        {
            if (i_change == i)
                continue;
            if (max < arrA[i][i_change])
            {
                max = arrA[i][i_change];
                temp_row = i_change;
            }
        }
        if (temp_row != i)
        {
            swap(arrA[i], arrA[temp_row]);
            swap(res[i], res[temp_row]);
        }

        //消元
        for (int i_change = 0; i_change < row; i_change++)
        {
            if (i_change == i)
                continue;
            ratio = arrA[i][i] / arrA[i_change][i];
            
            for (int j = 0; j < col; j++)
            {
                if (j >= i)
                {
                    arrA[i_change][j] = (arrA[i_change][j] - arrA[i][j] / ratio);
                }
                res[i_change][j] = (res[i_change][j] - res[i][j] / ratio);
            }
            
        }
        
        
    }
    //归一
    for (int i = 0; i < row; i++)
    {
        for (int j = 0; j < col; j++)
        {
            res[i][j] = res[i][j] / arrA[i][i];                        
        }
    }

	return res;
}

vector<vector<double>> operator_converse (vector<vector<double>> arrA)
{
	//转置矩阵
    //矩阵arrA的行数  
    int row = arrA.size();
    //矩阵arrA的列数  
    int col = arrA[0].size();

	vector<vector<double>>  res;
	//初始化矩阵
	res.resize(col);
    for (int i = 0; i < col; ++i)
    {
        res[i].resize(row);
    }

	for (int i = 0; i < col; i++)
    {
        for (int j = 0; j < row; j++)
        {
            res[i][j] = arrA[j][i];                        
        }
    }

	return res;
}