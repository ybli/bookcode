// MapFrameToolDlg.h : header file
//

#if !defined(AFX_MAPFRAMETOOLDLG_H__CC0EB9AC_70CD_4AA1_964D_64C86167D86D__INCLUDED_)
#define AFX_MAPFRAMETOOLDLG_H__CC0EB9AC_70CD_4AA1_964D_64C86167D86D__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000

/////////////////////////////////////////////////////////////////////////////
// CMapFrameToolDlg dialog

class CMapFrameToolDlg : public CDialog
{
// Construction
public:
	double	m_dCenterLY;
	double	m_dCenterBX;
	double	m_dFrameBX1;
	double	m_dFrameBX2;
	double	m_dFrameBX3;
	double	m_dFrameBX4;
	double	m_dFrameLY1;
	double	m_dFrameLY2;
	double	m_dFrameLY3;
	double	m_dFrameLY4;
	CMapFrameToolDlg(CWnd* pParent = NULL);	// standard constructor
	BOOL UpdateData(BOOL bSaveAndValidate = TRUE);
	void ClearMapTabCoordCtrlData(void);

// Dialog Data
	//{{AFX_DATA(CMapFrameToolDlg)
	enum { IDD = IDD_MAPFRAMETOOL_DIALOG };
	CString	m_csMapCode;
	CString	m_NewMapCode;
	CString	m_csCenterLY;
	CString	m_csCenterBX;
	CString	m_csFrameBX1;
	CString	m_csFrameBX2;
	CString	m_csFrameBX3;
	CString	m_csFrameBX4;
	CString	m_csFrameLY1;
	CString	m_csFrameLY2;
	CString	m_csFrameLY3;
	CString	m_csFrameLY4;
	int		m_iMapScale;
	int		m_iTransStyle;
	CString	m_csMap0;
	CString	m_csMap1;
	CString	m_csMap2;
	CString	m_csMap3;
	CString	m_csMap4;
	CString	m_csMap5;
	CString	m_csMap6;
	CString	m_csMap7;
	CString	m_csMap8;
	BOOL	m_bUseNewMapCode;
	//}}AFX_DATA

	// ClassWizard generated virtual function overrides
	//{{AFX_VIRTUAL(CMapFrameToolDlg)
	protected:
	virtual void DoDataExchange(CDataExchange* pDX);	// DDX/DDV support
	//}}AFX_VIRTUAL

// Implementation
protected:
	HICON m_hIcon;

	// Generated message map functions
	//{{AFX_MSG(CMapFrameToolDlg)
	virtual BOOL OnInitDialog();
	afx_msg void OnSysCommand(UINT nID, LPARAM lParam);
	afx_msg void OnPaint();
	afx_msg HCURSOR OnQueryDragIcon();
	virtual void OnOK();
	afx_msg void OnButton1();
	afx_msg void OnButton2();
	afx_msg void OnRadioTranStyleCoordCode();
	afx_msg void OnRadioTranStyleCodeCoord();
	afx_msg void OnRadioMapScale100();
	afx_msg void OnRadioMapScale50();
	afx_msg void OnRadioMapScale25();
	afx_msg void OnRadioMapScale10();
	afx_msg void OnRadioMapScale5();
	afx_msg void OnRadioMapScale25000();
	afx_msg void OnRadioMapScale1();
	afx_msg void OnCheckUseNewMapCode();
	//}}AFX_MSG
	DECLARE_MESSAGE_MAP()
};

//{{AFX_INSERT_LOCATION}}
// Microsoft Visual C++ will insert additional declarations immediately before the previous line.

#endif // !defined(AFX_MAPFRAMETOOLDLG_H__CC0EB9AC_70CD_4AA1_964D_64C86167D86D__INCLUDED_)
