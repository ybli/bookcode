// ListDlg.cpp : 实现文件
//

#include "stdafx.h"
#include "NetAdjustment.h"
#include "ListDlg.h"
#include "afxdialogex.h"


// CListDlg 对话框

IMPLEMENT_DYNAMIC(CListDlg, CDialogEx)

CListDlg::CListDlg(CWnd* pParent /*=NULL*/)
	: CDialogEx(IDD_LISTDIALOG, pParent)
{

}

CListDlg::~CListDlg()
{
}

void CListDlg::DoDataExchange(CDataExchange* pDX)
{
    CDialogEx::DoDataExchange(pDX);
    DDX_Control(pDX, IDC_LIST, m_List);
}


BEGIN_MESSAGE_MAP(CListDlg, CDialogEx)
//    ON_NOTIFY(LVN_ITEMCHANGED, IDC_LIST, &CListDlg::OnLvnItemchangedList)
END_MESSAGE_MAP()


// CListDlg 消息处理程序

CListDlg *pList;
BOOL CListDlg::OnInitDialog()
{
    CDialogEx::OnInitDialog();

    pList = this;

    CRect rect;
    m_List.GetClientRect(&rect);
    m_List.SetExtendedStyle(m_List.GetExtendedStyle() | LVS_EX_GRIDLINES | LVS_EX_FULLROWSELECT);
    m_List.InsertColumn(0, "起 点"   , LVCFMT_CENTER, 100);
    m_List.InsertColumn(1, "终 点"   , LVCFMT_CENTER, 100);
    m_List.InsertColumn(2, "DX(m)"   , LVCFMT_CENTER, 100);
    m_List.InsertColumn(3, "DY(m)"   , LVCFMT_CENTER, 100);
    m_List.InsertColumn(4, "DZ(m)"   , LVCFMT_CENTER, 100);
    m_List.InsertColumn(5, "Norm "   , LVCFMT_CENTER, 100);
    m_List.InsertColumn(6, "中误差x" , LVCFMT_CENTER, 100);
    m_List.InsertColumn(7, "中误差y" , LVCFMT_CENTER, 100);
    m_List.InsertColumn(8, "中误差z" , LVCFMT_CENTER, 100);

    return TRUE;  // return TRUE unless you set the focus to a control
                  // 异常: OCX 属性页应返回 FALSE
}


//void CListDlg::OnLvnItemchangedList(NMHDR *pNMHDR, LRESULT *pResult)
//{
//    LPNMLISTVIEW pNMLV = reinterpret_cast<LPNMLISTVIEW>(pNMHDR);
//    // TODO: 在此添加控件通知处理程序代码
//    *pResult = 0;
//}
