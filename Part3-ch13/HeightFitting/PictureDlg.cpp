// PictureDlg.cpp : 实现文件
//

#include "stdafx.h"
#include "HeightFitting.h"
#include "PictureDlg.h"
#include "afxdialogex.h"


// CPictureDlg 对话框

IMPLEMENT_DYNAMIC(CPictureDlg, CDialogEx)

CPictureDlg::CPictureDlg(CWnd* pParent /*=NULL*/)
	: CDialogEx(IDD_PICTUREDIALOG, pParent)
{

}

CPictureDlg::~CPictureDlg()
{
}

void CPictureDlg::DoDataExchange(CDataExchange* pDX)
{
    CDialogEx::DoDataExchange(pDX);
    DDX_Control(pDX, IDC_PICTURESTATIC, m_Picture);
}


BEGIN_MESSAGE_MAP(CPictureDlg, CDialogEx)
    ON_WM_PAINT()
END_MESSAGE_MAP()


// CPictureDlg 消息处理程序

CPictureDlg  *pPicture;
BOOL CPictureDlg::OnInitDialog()
{
    CDialogEx::OnInitDialog();

    pPicture = this;

    return TRUE;  // return TRUE unless you set the focus to a control
                  // 异常: OCX 属性页应返回 FALSE
}


void CPictureDlg::OnPaint()
{
    CPaintDC dc(this); 

    CRect rect;
    m_Picture.GetClientRect(&rect);

    CBrush  newBrush;
    CBrush *oldBrush;
    newBrush.CreateSolidBrush(RGB(255, 255, 255));
    oldBrush = m_Picture.GetWindowDC()->SelectObject(&newBrush);
    m_Picture.GetWindowDC()->Rectangle(rect);

    oldBrush = m_Picture.GetWindowDC()->SelectObject(oldBrush);
    newBrush.DeleteObject();
}
