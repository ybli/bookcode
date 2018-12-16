// MapFrameTool.h : main header file for the MAPFRAMETOOL application
//

#if !defined(AFX_MAPFRAMETOOL_H__DAFE050C_2763_4AD3_90BD_FB07360321BB__INCLUDED_)
#define AFX_MAPFRAMETOOL_H__DAFE050C_2763_4AD3_90BD_FB07360321BB__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000

#ifndef __AFXWIN_H__
	#error include 'stdafx.h' before including this file for PCH
#endif

#include "resource.h"		// main symbols

/////////////////////////////////////////////////////////////////////////////
// CMapFrameToolApp:
// See MapFrameTool.cpp for the implementation of this class
//

class CMapFrameToolApp : public CWinApp
{
public:
	CMapFrameToolApp();

// Overrides
	// ClassWizard generated virtual function overrides
	//{{AFX_VIRTUAL(CMapFrameToolApp)
	public:
	virtual BOOL InitInstance();
	//}}AFX_VIRTUAL

// Implementation

	//{{AFX_MSG(CMapFrameToolApp)
		// NOTE - the ClassWizard will add and remove member functions here.
		//    DO NOT EDIT what you see in these blocks of generated code !
	//}}AFX_MSG
	DECLARE_MESSAGE_MAP()
};


/////////////////////////////////////////////////////////////////////////////

//{{AFX_INSERT_LOCATION}}
// Microsoft Visual C++ will insert additional declarations immediately before the previous line.

#endif // !defined(AFX_MAPFRAMETOOL_H__DAFE050C_2763_4AD3_90BD_FB07360321BB__INCLUDED_)
