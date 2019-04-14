
// HeightFittingView.cpp : CHeightFittingView 类的实现
//

#include "stdafx.h"
// SHARED_HANDLERS 可以在实现预览、缩略图和搜索筛选器句柄的
// ATL 项目中进行定义，并允许与该项目共享文档代码。
#ifndef SHARED_HANDLERS
#include "HeightFitting.h"
#endif

#include "HeightFittingDoc.h"
#include "HeightFittingView.h"

#include "EditDlg.h"
#include "DataCenter.h"

#ifdef _DEBUG
#define new DEBUG_NEW
#endif


// CHeightFittingView

IMPLEMENT_DYNCREATE(CHeightFittingView, CFormView)

BEGIN_MESSAGE_MAP(CHeightFittingView, CFormView)
    ON_COMMAND(ID_FILE_NEW, &CHeightFittingView::OnFileNew)
    ON_COMMAND(ID_FILE_OPEN, &CHeightFittingView::OnFileOpen)
    ON_COMMAND(ID_FILE_SAVE, &CHeightFittingView::OnFileSave)
    ON_COMMAND(ID_FILE_SAVE_AS, &CHeightFittingView::OnFileSaveAs)
    ON_NOTIFY(TCN_SELCHANGE, IDC_TAB, &CHeightFittingView::OnTcnSelchangeTab)
    ON_COMMAND(ID_EDIT_UNDO, &CHeightFittingView::OnEditUndo)
    ON_COMMAND(ID_EDIT_CUT, &CHeightFittingView::OnEditCut)
    ON_COMMAND(ID_EDIT_COPY, &CHeightFittingView::OnEditCopy)
    ON_COMMAND(ID_EDIT_PASTE, &CHeightFittingView::OnEditPaste)
    ON_COMMAND(ID_RESULT_REPORT, &CHeightFittingView::OnResultReport)
    ON_COMMAND(ID_PICTURE_OPEN, &CHeightFittingView::OnPictureOpen)
    ON_COMMAND(ID_PICTURE_CLOSE, &CHeightFittingView::OnPictureClose)
    ON_COMMAND(ID_PICTURE_BIG, &CHeightFittingView::OnPictureBig)
    ON_COMMAND(ID_PICTURE_SMALL, &CHeightFittingView::OnPictureSmall)
END_MESSAGE_MAP()

// CHeightFittingView 构造/析构

CHeightFittingView::CHeightFittingView()
	: CFormView(IDD_HeightFitting_FORM)
{
	// TODO: 在此处添加构造代码

}

CHeightFittingView::~CHeightFittingView()
{
}

void CHeightFittingView::DoDataExchange(CDataExchange* pDX)
{
    CFormView::DoDataExchange(pDX);
    DDX_Control(pDX, IDC_TAB, m_Tab);
}

BOOL CHeightFittingView::PreCreateWindow(CREATESTRUCT& cs)
{
	// TODO: 在此处通过修改
	//  CREATESTRUCT cs 来修改窗口类或样式

	return CFormView::PreCreateWindow(cs);
}

void CHeightFittingView::OnInitialUpdate()
{
	CFormView::OnInitialUpdate();
	GetParentFrame()->RecalcLayout();
	ResizeParentToFit();

    CRect rect;
    m_Tab.GetClientRect(&rect);

    m_Tab.InsertItem(0, "表格");
    m_Tab.InsertItem(1, "报告");
    m_Tab.InsertItem(2, "图像");

    rect.top    +=  4;
    rect.bottom -= 20;
    rect.left   +=  4;
    rect.right  -=  4;

    LD.Create(IDD_LISTDIALOG,    &m_Tab);
    ED.Create(IDD_EDITDIALOG,    &m_Tab);
    PD.Create(IDD_PICTUREDIALOG, &m_Tab);

    LD.MoveWindow(rect);
    ED.MoveWindow(rect);
    PD.MoveWindow(rect);


    LD.ShowWindow(TRUE);
    m_Tab.SetCurSel(0);
}


#ifdef _DEBUG
void CHeightFittingView::AssertValid() const
{
	CFormView::AssertValid();
}

void CHeightFittingView::Dump(CDumpContext& dc) const
{
	CFormView::Dump(dc);
}

CHeightFittingDoc* CHeightFittingView::GetDocument() const // 非调试版本是内联的
{
	ASSERT(m_pDocument->IsKindOf(RUNTIME_CLASS(CHeightFittingDoc)));
	return (CHeightFittingDoc*)m_pDocument;
}
#endif //_DEBUG


// CHeightFittingView 消息处理程序

/*----------------------------------------------------------------------
 * 功     能： 新建
 *----------------------------------------------------------------------*/
void CHeightFittingView::OnFileNew()
{
    if (pEdit->m_Edit.GetModify())
    {
        int res = MessageBox("文件内容已改变，是否保存", "提示消息", MB_YESNOCANCEL);
        if (res == IDYES)
            OnFileSave();
        else
            return;
    }
}
/*----------------------------------------------------------------------
 * 功     能： 另存为
 *----------------------------------------------------------------------*/
void CHeightFittingView::OnFileSaveAs()
{
    TCHAR fileFilter[] = "文本文件(*.txt)|*.txt|全部文件(*.*)|*.*||";
    CFileDialog fileDlg(FALSE, "txt", 0, NULL, fileFilter, this);
    if (fileDlg.DoModal())
    {
        CString filePath = fileDlg.GetPathName();
        szCurrentPath    = filePath;
    
        CStdioFile file;
        if (!file.Open(filePath, CFile::modeCreate | CFile::modeWrite))
        {
            AfxMessageBox("保存失败！");
            return;
        }
        
        CString text;
        pEdit->m_Edit.GetWindowText(text);
        file.Write(text, text.GetLength());
        file.Flush();
        file.Close();

        pEdit->m_Edit.SetModify(FALSE);
    }
}

/*----------------------------------------------------------------------
 * 功     能： 保存
 *----------------------------------------------------------------------*/
void CHeightFittingView::OnFileSave()
{
    if (szCurrentPath == "")
        OnFileSaveAs();
    else
    {
        CStdioFile file;
        if (!file.Open(szCurrentPath, CFile::modeCreate | CFile::modeWrite))
        {
            AfxMessageBox("另存为失败！");
            return;
        }

        CString text;
        pEdit->m_Edit.GetWindowText(text);
        file.Write(text, text.GetLength());
        file.Flush();
        file.Close();

        pEdit->m_Edit.SetModify(FALSE);
    }
}


/*----------------------------------------------------------------------
 * 功     能： 选项卡切换
 *----------------------------------------------------------------------*/
void CHeightFittingView::OnTcnSelchangeTab(NMHDR *pNMHDR, LRESULT *pResult)
{
    *pResult = 0;

    int res  = m_Tab.GetCurSel();
    switch (res)
    {
    case 0: LD.ShowWindow(TRUE );
            ED.ShowWindow(FALSE);
            PD.ShowWindow(FALSE);
            break;
    case 1: LD.ShowWindow(FALSE);
            ED.ShowWindow(TRUE );
            PD.ShowWindow(FALSE);
            break;
    case 2: LD.ShowWindow(FALSE);
            ED.ShowWindow(FALSE);
            PD.ShowWindow(TRUE);
            break;
    default:LD.ShowWindow(TRUE );
            ED.ShowWindow(FALSE);
            PD.ShowWindow(FALSE);
            break;
    }
}

/*----------------------------------------------------------------------
 * 功     能： 撤销
 *----------------------------------------------------------------------*/
void CHeightFittingView::OnEditUndo()
{
    pEdit->m_Edit.Undo();
}

/*----------------------------------------------------------------------
 * 功     能： 剪切
 *----------------------------------------------------------------------*/
void CHeightFittingView::OnEditCut()
{
    pEdit->m_Edit.Cut();
}

/*----------------------------------------------------------------------
 * 功     能： 复制
 *----------------------------------------------------------------------*/
void CHeightFittingView::OnEditCopy()
{
    pEdit->m_Edit.Copy();
}

/*----------------------------------------------------------------------
 * 功     能： 粘贴
 *----------------------------------------------------------------------*/
void CHeightFittingView::OnEditPaste()
{
    pEdit->m_Edit.Paste();
}

/*----------------------------------------------------------------------
 * 功     能： 打开文件
 *----------------------------------------------------------------------*/
bool isDataReady = false;

void CHeightFittingView::OnFileOpen()
{
    if (isDataReady == true) 
    {
        delete pHfit;
        pHfit = NULL;
        isDataReady = false;
    }
        

    TCHAR fileFilter[] = "文本文件(*.txt)|*.txt|全部文件(*.*)|*.*||";
    CFileDialog fileDlg(TRUE, "txt", 0, NULL, fileFilter, this);
    if (fileDlg.DoModal())
    {
        CString filePath = fileDlg.GetPathName();
    
        DataInOut D_IO;
        pHfit = new HeightFitting();

        if (D_IO.readData(*pHfit, filePath))
        {
            ViewCenter::viewList(pList->m_List, pHfit->pList);
            isDataReady = true;
        }
        else
            AfxMessageBox("文件读取失败！");
    }

}

/*----------------------------------------------------------------------
 * 功     能： 生成平差报告
 *----------------------------------------------------------------------*/
bool isDataCal = false;
void CHeightFittingView::OnResultReport()
{
    if (!isDataReady)
    {
        AfxMessageBox("数据还未读取，请先读取数据！");
        return;
    }
    if (isDataCal)     
        return;

    pHfit->adjument();
    isDataCal = true;
    ViewCenter::getMax();
    CString text = ViewCenter::viewReport();
    if (text != "")
    {
        pEdit->m_Edit.SetWindowTextA(text);
        pEdit->m_Edit.SetModify(FALSE);
    }
    else
        AfxMessageBox("报告生成失败");
}

/*----------------------------------------------------------------------
 * 功     能： 打开图片
 *----------------------------------------------------------------------*/
void CHeightFittingView::OnPictureOpen()
{
    if (!isDataReady)
    {
        AfxMessageBox("数据还未读取，请先读取数据！");
        return;
    }
    if (!isDataCal)
    {
        AfxMessageBox("数据还未解算，请先读取数据！");
        return;
    }
    CRect rect;
    pPicture->m_Picture.GetClientRect(&rect);
    ViewCenter VT;
    VT.viewPicture(pPicture->m_Picture.GetWindowDC(), rect);
}

/*----------------------------------------------------------------------
 * 功     能： 关闭图片
 *----------------------------------------------------------------------*/
void CHeightFittingView::OnPictureClose()
{
    pPicture->OnPaint();
}

/*----------------------------------------------------------------------
 * 功     能： 放大
 *----------------------------------------------------------------------*/
void CHeightFittingView::OnPictureBig()
{
    OnPictureClose();

    range.ratio += 0.2;
    if (range.ratio == 4)
        range.ratio =  4;
    OnPictureOpen();
}

/*----------------------------------------------------------------------
 * 功     能： 缩小
 *----------------------------------------------------------------------*/
void CHeightFittingView::OnPictureSmall()
{
    OnPictureClose();

    range.ratio -= 0.2;
    if (range.ratio == 0.2)
        range.ratio =  0.2;
    OnPictureOpen();
}
