#pragma once
#include <stdio.h>
#include "stdafx.h"

struct dataRange
{
    double maxX,  minX;
    double maxY,  minY;
    double ratio;

    dataRange()
        :ratio(0.8) {}
};
extern dataRange range;

class ViewCenter
{
public:
    static void    getMax();
    static void    viewList(CListCtrl & m_list, vector<string>& netName);   
    static CString viewReport();
public:
    void    viewPicture(CDC *pDC, const CRect &rect);


private:
    void  getRange   (CDC * pDC, const CRect  &rect);
    void  plotLine   (CDC * pDC, const double &x1, const double &y1,
                                 const double &x2, const double &y2, bool isKnown);
    void  setText    (CDC * pDC, const double &x , const double &y,  CString  str);
    void  plotStation(CDC * pDC, const double &x1, const double &y1, bool isKnown);

private:
   double offsetX, ratioX;
   double offsetY, ratioY;

};