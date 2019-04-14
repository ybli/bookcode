#pragma once
#include "afxwin.h"


// CPictureDlg 对话框

class CPictureDlg : public CDialogEx
{
	DECLARE_DYNAMIC(CPictureDlg)

public:
	CPictureDlg(CWnd* pParent = NULL);   // 标准构造函数
	virtual ~CPictureDlg();

// 对话框数据
#ifdef AFX_DESIGN_TIME
	enum { IDD = IDD_PICTUREDIALOG };
#endif

protected:
	virtual void DoDataExchange(CDataExchange* pDX);    // DDX/DDV 支持

	DECLARE_MESSAGE_MAP()
public:
    CStatic m_Picture;
    virtual BOOL OnInitDialog();
    afx_msg void OnPaint();
};

extern CPictureDlg *pPicture;