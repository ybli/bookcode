#include "ViewCenter.h"

dataRange range;
/*------------------------------------------------------------------------
* 名     称：viewList
* 功     能：查看数据表格
* 输     入：无
* 输     出：无
*-----------------------------------------------------------------------*/
void ViewCenter::viewList(CListCtrl & m_list, vector<string>  &netName)
{
    CString cStr;

    const pointIndexList   &plist = pN_Aj->pList;
    const pointInfoList    &pinfo = pN_Aj->pInfo;
    const baseLineInfoList &binfo = pN_Aj->bInfo;
    for (int i = 0; i < binfo.size; i++)
    {
        uint     pStart    = plist.list[i].ps;
        uint     pEnd      = plist.list[i].pe;
        Vector3d vector    = binfo.list[i].vector;
        Matrix3d Qneu      = binfo.list[i].variance;
        CString  startName = pinfo.list[pStart].name.data();
        CString  endName   = pinfo.list[pEnd  ].name.data();


        m_list.InsertItem (i,  startName);
        m_list.SetItemText(i,  1,endName);

        cStr.Format("%.4lf", vector(0));
        m_list.SetItemText(i, 2,  cStr);
        cStr.Format("%.4lf", vector(1));
        m_list.SetItemText(i, 3,  cStr);
        cStr.Format("%.4lf", vector(2));
        m_list.SetItemText(i, 4,  cStr);
        cStr.Format("%.4lf", vector.norm() / 1000);
        m_list.SetItemText(i, 5,  cStr);

        cStr.Format("%.4lf", sqrt(Qneu(0, 0)));
        m_list.SetItemText(i, 6,  cStr);
        cStr.Format("%.4lf", sqrt(Qneu(1, 1)));
        m_list.SetItemText(i, 7,  cStr);
        cStr.Format("%.4lf", sqrt(Qneu(2, 2)));
        m_list.SetItemText(i, 8,  cStr);
    }
}
/*------------------------------------------------------------------------
* 名     称：viewReport
* 功     能：查看平差报告
* 输     入：无
* 输     出：CString
*-----------------------------------------------------------------------*/
CString ViewCenter::viewReport()
{
    CString resStr;
    CFile file;
    file.Open(outFilePath, CFile::modeRead);
    file.SeekToBegin();
    file.Read(resStr.GetBuffer(file.GetLength()), file.GetLength());
    return resStr;
}


/*------------------------------------------------------------------------
* 名     称：getMax
* 功     能：得到数据范围
* 输     入：无
* 输     出：无
*-----------------------------------------------------------------------*/

void ViewCenter::getMax()
{
    range.minX =  99999999999999999.0;
    range.minY =  99999999999999999.0;
    range.maxX = -99999999999999999.0;
    range.maxY = -99999999999999999.0;
    for (uint i = 0; i < pN_Aj->pInfo.size; i++)
    {
        pointInfo  &pinfo =  pN_Aj->pInfo.list[i];
        range.maxX = (pinfo.pos[0] > range.maxX) ? pinfo.pos[0] : range.maxX;
        range.minX = (pinfo.pos[0] < range.minX) ? pinfo.pos[0] : range.minX;

        range.maxY = (pinfo.pos[1] > range.maxY) ? pinfo.pos[1] : range.maxY;
        range.minY = (pinfo.pos[1] < range.minY) ? pinfo.pos[1] : range.minY;
    }
}
/*------------------------------------------------------------------------
* 名     称：getMax
* 功     能：得到图像改正数
* 输     入：CDC * pDC, const CRect & rect
* 输     出：无
*-----------------------------------------------------------------------*/
void ViewCenter::getRange(CDC * pDC, const CRect & rect)
{
    ratioY = fabs(rect.Width()  * range.ratio / (range.maxY - range.minY));
    ratioX = fabs(rect.Height() * range.ratio / (range.maxX - range.minX));

    double ratio = (ratioX < ratioY) ? ratioX : ratioY;

    double maxY  = range.maxY * ratio;
    double minY  = range.minY * ratio;
    double maxX  = range.maxX * ratio;
    double minX  = range.minX * ratio;

    offsetY = rect.Width()  / 2 - (maxY + minY) / 2;
    offsetX = rect.Height() / 2 + (maxX + minX) / 2;

    ratio  =  ratio;
    ratioY =  ratio;
    ratioX = -ratio;
}

/*------------------------------------------------------------------------
* 名     称：plotLine
* 功     能：画线
* 输     入：CDC * pDC, double x1, double y1,
*            double x2, double y2, bool isKnown
* 输     出：无
*-----------------------------------------------------------------------*/
void ViewCenter::plotLine(CDC * pDC, const double &x1, const double &y1, 
                                     const double &x2, const double &y2, bool isKnown)
{
    CPen  newPen;
    CPen *oldPen;
    oldPen = pDC->SelectObject(&newPen);

    if (isKnown)
    {
        newPen.CreatePen(BS_SOLID, 3, RGB(100, 0, 0));
        oldPen = pDC->SelectObject(&newPen);
        pDC->MoveTo(x1, y1);
        pDC->LineTo(x2, y2);
    }
    if (!isKnown)
    {
        newPen.CreatePen(BS_SOLID, 1, RGB(0, 0, 0));
        oldPen = pDC->SelectObject(&newPen);
        pDC->MoveTo(x1, y1);
        pDC->LineTo(x2, y2);
    }
    oldPen = pDC->SelectObject(oldPen);
    newPen.DeleteObject();
}
/*------------------------------------------------------------------------
* 名     称：plotStation
* 功     能：画站
* 输     入：CDC * pDC, double x1, double y1, bool isKnown
* 输     出：无
*-----------------------------------------------------------------------*/
void ViewCenter::plotStation(CDC * pDC, const double &x1, const double &y1, bool isKnown)
{
    CPen  newPen;
    CPen *oldPen;
    newPen.CreatePen(BS_SOLID, 1, RGB(0, 0, 0));

    oldPen = pDC->SelectObject(&newPen);

    if (isKnown)
    {
        pDC->MoveTo(x1 + 6, y1 + 6);
        pDC->LineTo(x1 - 6, y1 + 6);
        pDC->LineTo(x1,     y1 - 6);
        pDC->LineTo(x1 + 6, y1 + 6);
        pDC->MoveTo(x1,     y1    );
    }
    if (!isKnown)
    {
        pDC->MoveTo (x1, y1);
        pDC->Ellipse(x1 - 4, y1 - 4, x1 + 4, y1 + 4);
    }

    oldPen = pDC->SelectObject(oldPen);
    newPen.DeleteObject();
}

/*------------------------------------------------------------------------
* 名     称：setText
* 功     能：设置文本
* 输     入：CDC * pDC, double x, double y , CString str
* 输     出：无
*-----------------------------------------------------------------------*/
void ViewCenter::setText(CDC * pDC, const double &x, const double &y, CString str)
{
    CFont  newFont;
    CFont *oldFont;

    newFont.CreatePointFont(70, _T("Arial"), NULL);

    oldFont = pDC->SelectObject(&newFont);
    pDC->SetTextColor(RGB(255, 0, 0));

    pDC->TextOutA(x - 40, y, str);

    oldFont = pDC->SelectObject(oldFont);
    newFont.DeleteObject();
}

/*-------------------------------------------------------------------
* 名     称：viewPicture
* 功     能：绘制缩略图
* 输     入：无
* 输     出：无
*-------------------------------------------------------------------*/
void ViewCenter::viewPicture(CDC *pDC, const CRect &rect)
{
    double x1 = 0;
    double y1 = 0;
    double x2 = 0;
    double y2 = 0;

    getRange(pDC, rect);                                                                     

    for (uint i = 0; i < pN_Aj->pList.size; i++)
    {
        pointIndex &plist = pN_Aj->pList.list[i];

        x1 = range.ratio * (pN_Aj->pInfo.list[plist.ps].pos[1] * ratioY + offsetY);
        y1 = range.ratio * (pN_Aj->pInfo.list[plist.ps].pos[0] * ratioX + offsetX);
    
        x2 = range.ratio * (pN_Aj->pInfo.list[plist.pe].pos[1] * ratioY + offsetY);
        y2 = range.ratio * (pN_Aj->pInfo.list[plist.pe].pos[0] * ratioX + offsetX);

        if (pN_Aj->pInfo.list[plist.ps].isControl)
            plotStation(pDC, x1, y1, true);
        else
            plotStation(pDC, x1, y1, false);

        if (pN_Aj->pInfo.list[plist.pe].isControl)
            plotStation(pDC, x2, y2, true);
        else
            plotStation(pDC, x2, y2, false);

        CString staS(pN_Aj->pInfo.list[plist.ps].name.c_str());
        CString staE(pN_Aj->pInfo.list[plist.pe].name.c_str());
        setText(pDC, x1, y1, staS);
        setText(pDC, x2, y2, staE);

        if (pN_Aj->pInfo.list[plist.ps].isControl &&
            pN_Aj->pInfo.list[plist.pe].isControl)
        {
            plotLine(pDC, x1, y1, x2, y2, true);
        }
        else
            plotLine(pDC, x1, y1, x2, y2, false);           
    }                                                                                            
}