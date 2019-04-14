#include "DataCenter.h"

CString outFilePath = "";

/********************************************************************************************************/
//                            @ Data output-related functions

/*-------------------------------------------------------------------
 * Name : writeBinfo
 * Func : output result of base line
 * Args : vector<char*> netName           I
 *        NetAdjustment net               I
 *-----------------------------------------------------------------*/
void DataInOut::writeBinfo(vector<string>    &netName,
                     const NetAdjustment    &net)
{
    string  fileName = "";
    for (uint  i = 0; i < netName.size(); i++)
        fileName = fileName + netName[i];
    
 
    fileName   = fileName +"_binfo.txt";

    FILE* fp   = openFile("w", fileName.data());
    fprintf(fp, "baseline name: componentX(m) , componentY(m) , componentZ(m) ,    length(km) ,    sigmaN(mm) ,    sigmaE(mm) ,    sigmaU(mm)\n");
        
    const pointIndexList   &plist = net.pList;
    const pointInfoList    &pinfo = net.pInfo;
    const baseLineInfoList &binfo = net.bInfo;
    for  (int j = 0; j < binfo.size; j++)
    {
        uint   pStart    = plist.list[j].ps;
        uint   pEnd      = plist.list[j].pe;
        string startName = pinfo.list[pStart].name;
        string endName   = pinfo.list[pEnd  ].name;
        string bInfoName = "  "+ startName+" ->" + endName + " :";
            

        Vector3d vector  = binfo.list[j].vector;
        Matrix3d Qneu    = binfo.list[j].variance;
            
        fprintf(fp, "%s"      , bInfoName.data());
        fprintf(fp, "%15.4f," , vector(0));
        fprintf(fp, "%15.4f," , vector(1));
        fprintf(fp, "%15.4f," , vector(2));
        fprintf(fp, "%15.4f," , vector.norm() / 1000);

        fprintf(fp, "%15.4f," , sqrt(Qneu(0, 0)));
        fprintf(fp, "%15.4f," , sqrt(Qneu(1, 1)));
        fprintf(fp, "%15.4f\n", sqrt(Qneu(2, 2)));         
    }
    fclose(fp);
 }

/*-------------------------------------------------------------------
 * Name : writePinfo
 * Func : output result of points
 * Args : vector<char*> netName           I
 *        NetAdjustment net               I
 *-----------------------------------------------------------------*/
void DataInOut::writePinfo(vector<string>  &netName,
                     const NetAdjustment   &net )
{
    string  fileName = "";
    for (uint  i = 0; i < netName.size(); i++)
        fileName = fileName + netName[i];

    fileName    = fileName + "_pinfo.txt";
    outFilePath = fileName.data();///////////////
    FILE*   fp  = openFile("w", fileName.data());
    fprintf(fp,  "point name: componentX(m) , componentY(m) , componentZ(m) ,    sigmaN(mm) ,    sigmaE(mm) ,    sigmaU(mm),control point\n");

    const pointInfoList & pinfo = net.pInfo;
    for  (int j = 0; j <  pinfo.size; j++)
    {
        string name  = pinfo.list[j].name;
        int   nSize  = name.size();

        if (nSize < 10)
        {                                                            // judge name size and add space character         
            for (int k = 0; k < (10 - nSize); k++)
                name = " " + name;
        }
            
        Vector3d position = pinfo.list[j].pos;
        Matrix3d Qneu     = pinfo.list[j].Qneu;
            
        fprintf(fp, "%s:"    , name.data());
        fprintf(fp, "%15.4f,", position(0));
        fprintf(fp, "%15.4f,", position(1));
        fprintf(fp, "%15.4f,", position(2));

        fprintf(fp, "%15.4f,", 1000 * sqrt(Qneu(0, 0)));
        fprintf(fp, "%15.4f,", 1000 * sqrt(Qneu(1, 1)));
        fprintf(fp, "%15.4f" , 1000 * sqrt(Qneu(2, 2)));
            
        if (pinfo.list[j].isControl)
            fprintf(fp, "%10.0f\n", 1.0);
        else
            fprintf(fp, "%10.0f\n", 0.0);
    }
    fclose(fp);
}



/********************************************************************************************************/
//                            @ Data input-related functions
/*-------------------------------------------------------------------
 * Name : readData
 * Func : read data from file with folder path
 * Args : NetAdjustment   net             IO
 *        CString  folderPath             I
 * Rten : if read success
 *-----------------------------------------------------------------*/
bool DataInOut::readData(NetAdjustment &net, const CString &folderPath)
{
    getFilePath(folderPath);                                         // get file list
    net.listInit(filePathList.size());                               // initilize the size of data list
    for (uint i = 0; i < filePathList.size(); i++)                   // loop all files of file list 
    {
        CStdioFile inFile;
        if (!inFile.Open(filePathList[i], CFile::modeRead))
        {
            cout << _T("文件未创建成功！") << endl;
            return false;
        }


        CString line = "";
        net.pList.size++;

        while (inFile.ReadString(line))
        {
            if      (line.Mid(0, 2) == "1.")  
            {
                uint ind = readPinfoBlock(inFile, net.pInfo);        // save point infomation and output index
                net.pList.list[net.pList.size].ps = ind;             // save start point index
            }
            else if (line.Mid(0, 2) == "2.") 
            {
                uint ind = readPinfoBlock(inFile, net.pInfo);        // save point infomation and output index
                net.pList.list[net.pList.size].pe = ind;             // save end   point index
            }
            else if (line.Mid(0, 2) == "5.") 
            {
                net.bInfo.size = net.pList.size;
                readBinfoBlock(inFile, net.bInfo);                   // save base line infomation
            }
        }
        inFile.Close();
    }

    net.pInfo.size++;                                                // update size of array
    net.bInfo.size++;
    net.pList.size++;
    return true;
}
/*-------------------------------------------------------------------
 * Name : isPointExist
 * Func : judge if the point has been saved
 * Args : string   name                   I
 *        pointInfoList   pInfo           I
 * Rten : index of point in pointInfoList
 *-----------------------------------------------------------------*/
int  DataInOut::isPointExist(const string &name, const pointInfoList  &pInfo) const
{
    if (pInfo.size < 0)
        return  -1;
    for (int i = 0; i <= pInfo.size; i++)
    {
        if (pInfo.list[i].name == name)
            return i;
    }
    return -1;
}

/*-------------------------------------------------------------------
 * Name : readPinfoBlock
 * Func : deal with the point information block in file
 * Args : CStdioFile    inFile            I
 *        pointInfoList  pInfo            IO
 * Rten : index of point in pointInfoList
 *-----------------------------------------------------------------*/
int  DataInOut::readPinfoBlock(CStdioFile &inFile, pointInfoList  &pInfo)
{ 
    CString line = "";
    int  pCursor = -1;

    inFile.ReadString(line);
    string name = CT2A(line.Mid(30).Trim());

    if ((pCursor = isPointExist(name, pInfo)) != -1)
        return pCursor;
    else
        pInfo.list[++pInfo.size].name = name;                        // if it is a new point, update size and assign point name             

    while (inFile.ReadString(line))
    {   
        if (line.Find(_T("control")) >= 0)
            netName.push_back(name);
        if (line.Find(_T("WGS84 X")) >= 0)
        {
            pointInfo &pinfo =  pInfo.list[pInfo.size];
            pinfo.pos[0]     = _ttof(line.Mid(30));
            inFile.ReadString(line);
            pinfo.pos[1]     = _ttof(line.Mid(30));
            inFile.ReadString(line);
            pinfo.pos[2]     = _ttof(line.Mid(30));
            return pInfo.size;
        }
    }
    return -1;
}

/*-------------------------------------------------------------------
* Name : readBinfoBlock
* Func : deal with the base line information block in file
* Args : CStdioFile       inFile          I
*        baseLineInfoList  pInfo          IO
*-----------------------------------------------------------------*/
// 这个函数是暂时的
void DataInOut::readBinfoBlock(CStdioFile &inFile, baseLineInfoList &bInfo)
{
    CString line = "";

    inFile.ReadString(line);
    inFile.ReadString(line);
    inFile.ReadString(line);
    inFile.ReadString(line);

    int curPos = 0;
                   
    baseLineInfo &binfo  =  bInfo.list[bInfo.size];
    line.Tokenize(_T(" "),  curPos);
    binfo.vector  [0]    = _ttof(line.Tokenize(_T(" "), curPos));
    binfo.vector  [1]    = _ttof(line.Tokenize(_T(" "), curPos));
    binfo.vector  [2]    = _ttof(line.Tokenize(_T(" "), curPos));

    double sigmaX        = _ttof(line.Tokenize(_T(" "), curPos));
    double sigmaY        = _ttof(line.Tokenize(_T(" "), curPos));
    double sigmaZ        = _ttof(line.Tokenize(_T(" "), curPos));
    binfo. sigma         = _ttof(line.Tokenize(_T(" "), curPos)) / 1000;

    binfo.variance.setZero();
    binfo.variance(0, 0) =  sigmaX * sigmaX;
    binfo.variance(1, 1) =  sigmaY * sigmaY;
    binfo.variance(2, 2) =  sigmaZ * sigmaZ;

}

/*-------------------------------------------------------------------
* Name : getFilePath
* Func : get a list of all target file under the folder path
* Args : CString  folderPath              I
*-----------------------------------------------------------------*/
void DataInOut::getFilePath(const CString &folderPath)
{
    CString fileName = _T("");
 
    CFileFind find;
    BOOL isFind = find.FindFile(folderPath + _T("/*.txt"));

    while (isFind)
    {
        isFind = find.FindNextFile();
        if (find.IsDots())
            continue;
        else
        {
            fileName = find.GetFileName();
            fileName = folderPath +"//"+ fileName;
            filePathList.push_back(fileName);
        }                            
    }
}