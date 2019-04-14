
// HeightFittingView.h : CHeightFittingView 类的接口
//

#pragma once
#include "afxcmn.h"
#include "EditDlg.h"
#include "ListDlg.h"
#include "PictureDlg.h"


class CHeightFittingView : public CFormView
{
protected: // 仅从序列化创建
	CHeightFittingView();
	DECLARE_DYNCREATE(CHeightFittingView)

public:
#ifdef AFX_DESIGN_TIME
	enum{ IDD = IDD_HeightFitting_FORM };
#endif

// 特性
public:
	CHeightFittingDoc* GetDocument() const;

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
	virtual ~CHeightFittingView();
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

#ifndef _DEBUG  // HeightFittingView.cpp 中的调试版本
inline CHeightFittingDoc* CHeightFittingView::GetDocument() const
   { return reinterpret_cast<CHeightFittingDoc*>(m_pDocument); }
#endif

