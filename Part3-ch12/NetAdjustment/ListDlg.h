#pragma once
#include "afxcmn.h"


// CListDlg 对话框

class CListDlg : public CDialogEx
{
	DECLARE_DYNAMIC(CListDlg)

public:
	CListDlg(CWnd* pParent = NULL);   // 标准构造函数
	virtual ~CListDlg();

// 对话框数据
#ifdef AFX_DESIGN_TIME
	enum { IDD = IDD_LISTDIALOG };
#endif

protected:
	virtual void DoDataExchange(CDataExchange* pDX);    // DDX/DDV 支持

	DECLARE_MESSAGE_MAP()
public:
    CListCtrl m_List;
    virtual BOOL OnInitDialog();
//    afx_msg void OnLvnItemchangedList(NMHDR *pNMHDR, LRESULT *pResult);
};

extern CListDlg *pList;