
// SPPDlg.h : 头文件
//

#pragma once


// CSPPDlg 对话框
class CSPPDlg : public CDHtmlDialog
{
// 构造
public:
	CSPPDlg(CWnd* pParent = NULL);	// 标准构造函数

// 对话框数据
	enum { IDD = IDD_SPP_DIALOG, IDH = IDR_HTML_SPP_DIALOG };

	CEdit m_path;
	CListCtrl m_Grid;
	CStatusBar m_StatusBar;
	CList<CRect,CRect&> m_listRect; 

	protected:
	virtual void DoDataExchange(CDataExchange* pDX);	// DDX/DDV 支持

	HRESULT OnButtonOK(IHTMLElement *pElement);
	HRESULT OnButtonCancel(IHTMLElement *pElement);

// 实现
protected:
	HICON m_hIcon;

	// 生成的消息映射函数
	virtual BOOL OnInitDialog();
	afx_msg void OnSysCommand(UINT nID, LPARAM lParam);
	afx_msg void OnPaint();
	afx_msg HCURSOR OnQueryDragIcon();
	DECLARE_MESSAGE_MAP()
	DECLARE_DHTML_EVENT_MAP()
public:
	afx_msg void OnFileopen();
	afx_msg void OnSave();
	afx_msg void OnAnother();
	afx_msg void OnClose();
	afx_msg void OnCalculate();
	afx_msg void OnSize(UINT nType, int cx, int cy);
	void resize(void);

	POINT old;

};
