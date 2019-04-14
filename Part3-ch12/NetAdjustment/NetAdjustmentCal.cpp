#include "DataCenter.h"

#define n pInfo.size                                                 // for matrix convient definition
NetAdjustment *pN_Aj = NULL;
vector<string> netName;
/********************************************************************************************************/
//                            @ Private functions
/*-------------------------------------------------------------------
 * Name : getEquation
 * Func : get matrix B, P, L
 * Args : MatrixXd      &B           O
 *        Matrix3d      &P           O
 *        Vector3d      &L           O
 *        baseLineInfo  &binfo       I      
 *        pointIndex    &pIndex      I  (point index)
 *-----------------------------------------------------------------*/
void NetAdjustment::getEquation(MatrixXd &B, Matrix3d  &P, Vector3d &L,
                                const baseLineInfo     &binfo, 
                                const pointIndex       &pIndex)
{
    B.resize(3, 3 * n);                                             
    B.setZero();
    uint p1 = pIndex.ps;
    uint p2 = pIndex.pe;

    for (uint r = 0; r < 3; r++)
    {
        L(r) = -binfo.vector[r] + (pInfo.list[p2].pos[r] -
                                   pInfo.list[p1].pos[r]);
        B(r, 3 * p1 + r) = -1;
        B(r, 3 * p2 + r) =  1;   
    }    
    P = binfo.variance.inverse();
}
/*-------------------------------------------------------------------
 * Name : getG
 * Func : get matrix G
 * Args : MatrixXd  &G       O 
 *-----------------------------------------------------------------*/
void NetAdjustment::getG(MatrixXd &G)
{
    MatrixXd g(3, 3 * n); 
    g.setZero();

    for (int i = 0; i < 3*n;)
    {
        g(0, i++) = 1;
        g(1, i++) = 1;
        g(2, i++) = 1;
    }
    G = g.transpose() / sqrt(n);
}
/*-------------------------------------------------------------------
 * Name : getCtrlG
 * Func : get matrix G of control point 
 * Args : MatrixXd & G       O
 *-----------------------------------------------------------------*/
void NetAdjustment::getCtrlG(MatrixXd & G)
{
    G.resize(3*ctrlPointCnt, 3*n); G.setZero();
    uint ctpCnt = 0;              

    for (int i = 0; i < n; i++)
    {
        if (pInfo.list[i].isControl)
        {
            G(ctpCnt++, 3 * i    ) = 1;
            G(ctpCnt++, 3 * i + 1) = 1;
            G(ctpCnt++, 3 * i + 2) = 1;
        }
        if (ctpCnt >= 3 * ctrlPointCnt)
            break;
    }
}


/*-------------------------------------------------------------------
 * Name : pInfoInit
 * Func : point attribute initialization
 * Args : vector<string> netName    I
 *-----------------------------------------------------------------*/
void NetAdjustment::pInfoInit(vector<string> &netName)
{
    if (pInfo.list == NULL)
    {    
        cout << "Don't have points data !" << endl;
        return;
    }

    for (int i = 0; i < n; i++)
    {
        pInfo.list[i].isControl = false;

        for (uint j = 0; j < netName.size(); j++)
        {
            if (netName[j] == pInfo.list[i].name.data()) {
                pInfo.list[i].isControl = true;
                ctrlPointCnt++;  break;
            }
        }
    }
}


/********************************************************************************************************/
//                            @ Public functions
/*-------------------------------------------------------------------
 * Name : listInit
 * Func : allocate memory ahead of time for data list
 * Args : uint size                 I  (number of file)
 *-----------------------------------------------------------------*/
void NetAdjustment::listInit(uint size)
{
    bInfo.list = new baseLineInfo[size  ];
    pList.list = new pointIndex  [size  ];
    pInfo.list = new pointInfo   [size*2];
}

/*-------------------------------------------------------------------
 * Name : adjument
 * Func : the main function of the adjustment
 * Args : vector<string> netName    I  (name list  of control points)
 *-----------------------------------------------------------------*/
void NetAdjustment::adjument(vector<string>  &netName)
{
    pInfoInit(netName);                                              // point attribute initialization

    double sigSum = 0;
    for (int  i   = 0; i < bInfo.size; i++)                          // assign base line sigma               
         sigSum  += bInfo.list[i].sigma;
    double sigma  = sigSum / bInfo.size;                             // get mean sigma

    MatrixXd N, W;                                                   // matrix dimension reference :
    N.resize(3 * n, 3 * n); N.setZero();                             // N   (3n, 3n)
    W.resize(3 * n, 1    ); W.setZero();                             // Qxx (3n, 3n)
                                                                     // G   (3n, 3 )
    MatrixXd *lstB = new MatrixXd[bInfo.size];                       // W   (3n, 1 )
    Matrix3d *lstP = new Matrix3d[bInfo.size];                       // x   (3n, 1 )
    Vector3d *lstL = new Vector3d[bInfo.size];                       // B   (3 , 3n)
                                                                     // P   (3 , 3 )
    for (int i = 0; i < bInfo.size; i++)                             // L   (3 , 1 )
    {                                                                 
        MatrixXd B;                                                   
        Matrix3d P;
        Vector3d L;
        getEquation(B, P, L, bInfo.list[i], pList.list[i]);
        P = sigma*sigma*P;
        N = N + B.transpose()*P*B;
        W = W + B.transpose()*P*L;

        lstB[i] = B;
        lstP[i] = P;
        lstL[i] = L;
    }

    MatrixXd G, Qxx, x;                                              
    getG(G);
    N = N + G*G.transpose();

    if (ctrlPointCnt > 0)                                            // superposition matrix G of control points 
    {
        getCtrlG(G);
        N = N + G.transpose()*G;  
    }
    Qxx = N.inverse();
    x   = Qxx*W;
    
    VectorXd VPV;
    Vector3d V;
    for (int i = 0; i < bInfo.size; i++)                             // matrix dimension reference :   
    {                                                                // V   (3  ,  1)
        V   = lstB[i] * x - lstL[i];                                 // VPV (1  ,  1)
        VPV = V.transpose()*lstP[i]*V;   
    }

    sigma = sqrt(VPV(0) / (3*bInfo.size - 3 * n));
    Qxx   = sigma*sigma*Qxx;


    delete[] lstB;                                                   // delete usage space 
    delete[] lstP;
    delete[] lstL;

    coordParam Cs = CoordCpu::getCoordSys(WGS_84);                   // get parameters of WGS-84 system
    for (int i = 0; i < n; i++)
    {
        pInfo.list[i].pos[0] += x(3 * i    );                        // position correction
        pInfo.list[i].pos[1] += x(3 * i + 1);
        pInfo.list[i].pos[2] += x(3 * i + 2);

        double B = 0, L = 0, H = 0;
        CoordCpu::XYZ_BLH(B, L, H,
                          pInfo.list[i].pos[0],
                          pInfo.list[i].pos[1],
                          pInfo.list[i].pos[2],
                          Cs);
        Matrix3d T;
        T << -sin(B)*cos(L), -sin(B)*sin(L), cos(B),
             -sin(L)       ,  cos(L)       , 0     ,
              cos(B)*cos(L),  cos(B)*sin(L), sin(B); 

        pInfo.list[i].Qxx  = Qxx.block  (3 * i, 3 * i, 3, 3);
        pInfo.list[i].Qneu = T.transpose() * pInfo.list[i].Qxx * T;
    }

    DataInOut dataOut;
    dataOut.writeBinfo(netName, *this);                              // output adjustment result of base line 
    dataOut.writePinfo(netName, *this);                              // output adjustment result of point 
 }



//int main()
//{
//    NetAdjustment N_Aj;
//    DataInOut D_IO;
//    vector<char*> netName;
//
//    netName.push_back("022");                                        // control points can be defined by themselves 
//    netName.push_back("024");
//    netName.push_back("014");
//    netName.push_back("035");
//    netName.push_back("011");
//    netName.push_back("016");
//
//    
//    if (!D_IO.readData(N_Aj, "static"))
//        cout << "Data has some thing wrong!" << endl;
//    else
//        N_Aj.adjument(netName);
//
//    return 0;
//}




