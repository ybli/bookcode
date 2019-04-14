
// NetAdjustmentView.h : CNetAdjustmentView 类的接口
//

#pragma once
#include "afxcmn.h"
#include "EditDlg.h"
#include "ListDlg.h"
#include "PictureDlg.h"


class CNetAdjustmentView : public CFormView
{
protected: // 仅从序列化创建
	CNetAdjustmentView();
	DECLARE_DYNCREATE(CNetAdjustmentView)

public:
#ifdef AFX_DESIGN_TIME
	enum{ IDD = IDD_NETADJUSTMENT_FORM };
#endif

// 特性
public:
	CNetAdjustmentDoc* GetDocument() const;

// 操作
public:

// 重写
public:
	virtual BOOL PreCreateWindow(CREATESTRUCT& cs);
protected:
	virtual void DoDataExchange(CDataExchange* pDX);    // DDX/DDV 支持
	virtual void OnInitialUpdate(); // 构造后第一次调用

// 实现
public:
	virtual ~CNetAdjustmentView();
#ifdef _DEBUG
	virtual void AssertValid() const;
	virtual void Dump(CDumpContext& dc) const;
#endif

protected:

// 生成的消息映射函数
protected:
	DECLARE_MESSAGE_MAP()
public:
    afx_msg void OnFileNew();
    afx_msg void OnFileOpen();
    afx_msg void OnFileSave();
    afx_msg void OnFileSaveAs();

    afx_msg void OnEditUndo();
    afx_msg void OnEditCut();
    afx_msg void OnEditCopy();
    afx_msg void OnEditPaste();

public:
    CTabCtrl     m_Tab; 
    CEditDlg     ED;
    CListDlg     LD;
    CPictureDlg  PD;
    afx_msg void OnTcnSelchangeTab(NMHDR *pNMHDR, LRESULT *pResult);

    afx_msg void OnResultReport();
    afx_msg void OnPictureOpen();
    afx_msg void OnPictureClose();
    afx_msg void OnPictureBig();
    afx_msg void OnPictureSmall();
};

#ifndef _DEBUG  // NetAdjustmentView.cpp 中的调试版本
inline CNetAdjustmentDoc* CNetAdjustmentView::GetDocument() const
   { return reinterpret_cast<CNetAdjustmentDoc*>(m_pDocument); }
#endif

