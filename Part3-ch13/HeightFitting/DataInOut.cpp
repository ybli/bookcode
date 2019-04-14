#include "DataCenter.h"

CString outFilePath = "Result.txt";

/********************************************************************************************************/
//                            @ Data output-related functions

/*-------------------------------------------------------------------
 * Name : writePinfo
 * Func : output result of points
 * Args : vector<char*> netName           I
 *        HeightFitting net               I
 *-----------------------------------------------------------------*/
void DataInOut::writePinfo(const HeightFitting &net)
{
    FILE*   fp  = openFile("w", outFilePath);
	
	fprintf(fp, "x:  ");	
	for (int i = 0; i < net.x.size(); i++){
		fprintf(fp, "%15.4f,", net.x(i));
	}
	fprintf(fp, "\nv:  ");
	for (int i = 0; i < net.v.size(); i++){
		fprintf(fp, "%15.4f,", net.v(i));
	}
	fprintf(fp, "\nSigma:  ");
	fprintf(fp, "%15.4f", net.sigma);

    fclose(fp);
}



/********************************************************************************************************/
//                            @ Data input-related functions
/*-------------------------------------------------------------------
 * Name : readData
 * Func : read data from file with folder path
 * Args : HeightFitting   net             IO
 *        CString    filePath             I
 * Rten : if read success
 *-----------------------------------------------------------------*/
bool DataInOut::readData(HeightFitting &net, const CString &filePath)
{
    CStdioFile inFile;
    if (!inFile.Open(filePath, CFile::modeRead))
    {
        cout << _T("文件未创建成功！") << endl;
        return false;
    }

	
	CString mark = ",";
    CString line = " ";
    while (inFile.ReadString(line))
    {	
		PointInfo pInfo;
		int   pos  =  0;
		pInfo.name =      line.Tokenize(mark, pos) ; 
		pInfo.x    = atof(line.Tokenize(mark, pos));
		pInfo.y    = atof(line.Tokenize(mark, pos));
		pInfo.h1   = atof(line.Tokenize(mark, pos));
		pInfo.h2   = atof(line.Tokenize(mark, pos));
		net.pList.push_back(pInfo);
    }
    inFile.Close();
    
    return true;
}

