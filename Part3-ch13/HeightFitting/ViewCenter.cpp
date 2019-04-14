#include "ViewCenter.h"

dataRange range;
/*------------------------------------------------------------------------
* 名     称：viewList
* 功     能：查看数据表格
* 输     入：无
* 输     出：无
*-----------------------------------------------------------------------*/
void ViewCenter::viewList(CListCtrl & m_list, vector<PointInfo>  &pList)
{
	CString cStr;
	for (int i = 0; i < pList.size(); i++)
	{
		PointInfo &pInfo = pList[i];
		CString  name = pInfo.name.data();

		m_list.InsertItem(i, pInfo.name.data());
		cStr.Format("%.4lf", pInfo.x);
		m_list.SetItemText(i, 1, cStr);
		cStr.Format("%.4lf", pInfo.y);
		m_list.SetItemText(i, 2, cStr);
		cStr.Format("%.4lf", pInfo.h1);
		m_list.SetItemText(i, 3, cStr);
		cStr.Format("%.4lf", pInfo.h2);
		m_list.SetItemText(i, 4, cStr);
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
	range.minX = 99999999999999999.0;
	range.minY = 99999999999999999.0;
	range.maxX = -99999999999999999.0;
	range.maxY = -99999999999999999.0;
	for (uint i = 0; i < pHfit->pList.size(); i++)
	{
		PointInfo  &pinfo = pHfit->pList[i];
		range.maxX = (pinfo.h1 > range.maxX) ? pinfo.h1 : range.maxX;
		range.maxX = (pinfo.h2 > range.maxX) ? pinfo.h2 : range.maxX;

		//range.minX = (pinfo.h1 < range.minX) ? pinfo.h1 : range.minX;
		//range.minX = (pinfo.h2 < range.minX) ? pinfo.h2 : range.minX;
		range.minX = range.maxX-5;

		range.maxY = 25;
		range.minY = 0;
		//range.maxY = (pinfo.pos[1] > range.maxY) ? pinfo.pos[1] : range.maxY;
		//range.minY = (pinfo.pos[1] < range.minY) ? pinfo.pos[1] : range.minY;
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

//	double ratio = (ratioX < ratioY) ? ratioX : ratioY;

	double maxY = range.maxY * ratioY;
	double minY = range.minY * ratioY;
	double maxX = range.maxX * ratioX;
	double minX = range.minX * ratioX;

	offsetY = rect.Width() / 2 - (maxY + minY) / 2;
	offsetX = rect.Height() / 2 + (maxX + minX) / 2;

	//ratio = ratio;
	ratioY = ratioY;
	ratioX = -ratioX;
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
		newPen.CreatePen(BS_SOLID, 1, RGB(255, 0, 0));
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
		pDC->LineTo(x1, y1 - 6);
		pDC->LineTo(x1 + 6, y1 + 6);
		pDC->MoveTo(x1, y1);
	}
	if (!isKnown)
	{
		pDC->MoveTo(x1, y1);
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
	double x10 = 0;
	double y10 = 0;
	double x20 = 0;
	double y20 = 0;

	getRange(pDC, rect);

	int si = pHfit->pList.size();
	for (int i = 0; i < si; i++)
	{
		PointInfo &plist = pHfit->pList[i];

		x1 = range.ratio * ((25.0/si)* i * ratioY + offsetY);
		y1 = range.ratio * (plist.h1 * ratioX + offsetX);
		plotStation(pDC, x1, y1, false);//o-h1 d-h2

		x2 = range.ratio * ((25.0 / si)* i * ratioY + offsetY);
		y2 = range.ratio * (plist.h2 * ratioX + offsetX);
		plotStation(pDC, x2, y2, true);
		
		if (i != 0)
		{
			plotLine(pDC, x10, y10, x1, y1, true);
			plotLine(pDC, x20, y20, x2, y2, false);
		}
		
		x10 = x1;
		x20 = x2;
		y10 = y1;
		y20 = y2;
	}

	plotStation(pDC, 575, 22, false);
	plotStation(pDC, 575, 42, true);
	setText(pDC,590,20,"H1");
	setText(pDC,590,40,"H2");
}