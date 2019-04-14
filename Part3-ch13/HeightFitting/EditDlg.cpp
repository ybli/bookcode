// EditDlg.cpp : 实现文件
//

#include "stdafx.h"
#include "HeightFitting.h"
#include "EditDlg.h"
#include "afxdialogex.h"


// CEditDlg 对话框

IMPLEMENT_DYNAMIC(CEditDlg, CDialogEx)

CEditDlg::CEditDlg(CWnd* pParent /*=NULL*/)
	: CDialogEx(IDD_EDITDIALOG, pParent)
{

}

CEditDlg::~CEditDlg()
{
}

void CEditDlg::DoDataExchange(CDataExchange* pDX)
{
    CDialogEx::DoDataExchange(pDX);
    DDX_Control(pDX, IDC_EDIT, m_Edit);
}


BEGIN_MESSAGE_MAP(CEditDlg, CDialogEx)
END_MESSAGE_MAP()


// CEditDlg 消息处理程序

CEditDlg *pEdit;
BOOL CEditDlg::OnInitDialog()
{
    CDialogEx::OnInitDialog();

    pEdit = this;

    return TRUE;  // return TRUE unless you set the focus to a control
                  // 异常: OCX 属性页应返回 FALSE
}
