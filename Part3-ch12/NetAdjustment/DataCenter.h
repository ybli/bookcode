#pragma once

#include <vector>
#include <iostream>
#include <math.h>
#include <afxwin.h>                                                  // use MFC
using namespace std;

#include <Eigen\Dense>
using namespace Eigen;

/********************************************************************************************************/
//                            @ Net adjustment-related definition
typedef unsigned int uint;
typedef struct
{
    bool     isControl;
    string   name;
    Vector3d pos ;
    Matrix3d Qneu;
    Matrix3d Qxx ;

}pointInfo;

typedef struct
{
    double   sigma;
    Vector3d vector  ;
    Matrix3d variance;
}baseLineInfo;

typedef struct
{
    int           size;
    pointInfo    *list;
}pointInfoList;

typedef struct
{
    int           size;
    baseLineInfo *list;
}baseLineInfoList;

typedef struct
{
    int ps;                                                          // strat point index
    int pe;                                                          // end   point index
}pointIndex;

typedef struct
{
    int          size;
    pointIndex  *list;
}pointIndexList;

class NetAdjustment
{
public:
    NetAdjustment()
        : ctrlPointCnt(0)
    {
        pInfo.list = NULL, pInfo.size = -1;
        bInfo.list = NULL, bInfo.size = -1; 
        pList.list = NULL, pList.size = -1;     
    };

   ~NetAdjustment()
    {
       delete[] pList.list;
       delete[] pInfo.list;
       delete[] bInfo.list;
    }

    void adjument(vector<string> &netName);
    void listInit(uint size);
private:
    void pInfoInit  (vector<string> &netName);
    void getG       (MatrixXd &G);
    void getCtrlG   (MatrixXd &G);
    void getEquation(MatrixXd &B, Matrix3d  &P, Vector3d &L,
                     const baseLineInfo     &bInfo,
                     const pointIndex       &pIndex);
public :
    baseLineInfoList bInfo;                                          // array of baseline infomation
    pointInfoList    pInfo;                                          // array of point    infomation
    pointIndexList   pList;                                          // array of point index for base line
private :
    uint ctrlPointCnt;                                               // number of control points
};

extern NetAdjustment *pN_Aj;//////////////////////////
extern vector<string> netName;
/********************************************************************************************************/
//                            @ Data Input and Output-related definition
class DataInOut
{
    
public :
    void writeBinfo(vector<string>  &netName, const NetAdjustment  &net);
    void writePinfo(vector<string>  &netName, const NetAdjustment  &net);


    bool readData(NetAdjustment &net,const CString &folderPath);

private:

    FILE* openFile(const char* mode, const char *filePath)
    {
        FILE *fp = NULL;
        if (NULL == (fp = fopen(filePath, mode)))
            cout << "File can't open!";
        return fp;
    }
    uint getFileName()  const { return filePathList.size(); }


    int  isPointExist  (const string  &name, const pointInfoList &pInfo) const;
    int  readPinfoBlock(CStdioFile  &inFile, pointInfoList &pInfo);
    void readBinfoBlock(CStdioFile  &inFile, baseLineInfoList &bInfo);
    void getFilePath   (const CString &folderPath);
    

private:
    vector<CString> filePathList;
};

extern CString outFilePath;


/********************************************************************************************************/
//                            @ Coordinate calculate-related definition
enum coordSys
{
    WGS_84,
    BJ_54,
    XA_80,
    PZ_90,
    CGS_2000
};

typedef struct
{
    coordSys type;                                                   // ellipsoid   type, can be:  WGS_84, BJ_54, XA_80, PZ_90, CGS_2000
    double    A;                                                     // ellipsoidal long radius
    double    Alfa;                                                  // ellipsoidal flattening rate
    double    E2;                                                    // the square of the first eccentricity of the ellipsoid : 2*Cs.Alfa - (Cs.Alfa) ^ 2
    double    UTM;                                                   // longitude of the central meridian (in radians)
    double    X0,Y0;                                                 // X0, Y0 are X and Y coordinates plus constant (in km)
    double    L0;                                                    // for the central meridian longitude (in radians);
    double    H0;                                                    // for the projection surface elevation (here the height of the ground, in m)
    double    DN;                                                    // elevation anomaly (in m)
    double    GM;
    double    Omega;                                                 // earth's angular velocity

}coordParam;

class CoordCpu
{
public:
    CoordCpu() {};

    static coordParam getCoordSys(coordSys type);
    static void       XYZ_BLH(double &B, 
                              double &L, 
                              double &H,
                              const double &X, 
                              const double &Y,
                              const double &Z,
                              const coordParam &Cs);

private:

};
