#ifndef _MATRIX_H_
#define _MATRIX_H_

#pragma once

#include <math.h>
#include <vector>

using namespace std;

vector<vector<double>> operator_add (vector<vector<double>> arrA, vector<vector<double>> arrB);//æÿ’Ûº”

vector<vector<double>> operator_multiply (vector<vector<double>> arrA, vector<vector<double>> arrB);//æÿ’Û≥À

vector<vector<double>> operator_number (vector<vector<double>> arrA, double m);

vector<vector<double>> operator_inv (vector<vector<double>> arrA);

vector<vector<double>> operator_converse (vector<vector<double>> arrA);

#endif