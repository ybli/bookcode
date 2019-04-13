// MapFrameToolDlg.cpp : implementation file
//

#include "stdafx.h"
#include "MapFrameTool.h"
#include "MapFrameToolDlg.h"
#include "MapFrameInfo.h"

#ifdef _DEBUG
#define new DEBUG_NEW
#undef THIS_FILE
static char THIS_FILE[] = __FILE__;
#endif

/////////////////////////////////////////////////////////////////////////////
// CAboutDlg dialog used for App About

class CAboutDlg : public CDialog
{
public:
	CAboutDlg();

// Dialog Data
	//{{AFX_DATA(CAboutDlg)
	enum { IDD = IDD_ABOUTBOX };
	//}}AFX_DATA

	// ClassWizard generated virtual function overrides
	//{{AFX_VIRTUAL(CAboutDlg)
	protected:
	virtual void DoDataExchange(CDataExchange* pDX);    // DDX/DDV support
	//}}AFX_VIRTUAL

// Implementation
protected:
	//{{AFX_MSG(CAboutDlg)
	//}}AFX_MSG
	DECLARE_MESSAGE_MAP()
};

CAboutDlg::CAboutDlg() : CDialog(CAboutDlg::IDD)
{
	//{{AFX_DATA_INIT(CAboutDlg)
	//}}AFX_DATA_INIT
}

void CAboutDlg::DoDataExchange(CDataExchange* pDX)
{
	CDialog::DoDataExchange(pDX);
	//{{AFX_DATA_MAP(CAboutDlg)
	//}}AFX_DATA_MAP
}

BEGIN_MESSAGE_MAP(CAboutDlg, CDialog)
	//{{AFX_MSG_MAP(CAboutDlg)
		// No message handlers
	//}}AFX_MSG_MAP
END_MESSAGE_MAP()

/////////////////////////////////////////////////////////////////////////////
// CMapFrameToolDlg dialog

CMapFrameToolDlg::CMapFrameToolDlg(CWnd* pParent /*=NULL*/)
	: CDialog(CMapFrameToolDlg::IDD, pParent)
{
	//{{AFX_DATA_INIT(CMapFrameToolDlg)
	m_csMapCode = _T("J500051");
	m_NewMapCode = _T("");
	m_iMapScale = 4;
	m_iTransStyle = 0;
	m_csMap0 = _T("");
	m_csMap1 = _T("");
	m_csMap2 = _T("");
	m_csMap3 = _T("");
	m_csMap4 = _T("");
	m_csMap5 = _T("");
	m_csMap6 = _T("");
	m_csMap7 = _T("");
	m_csMap8 = _T("");
	m_dCenterLY = 116.0730;
	m_dCenterBX = 39.5500;
	m_dFrameBX1 = 0.0;
	m_dFrameBX2 = 0.0;
	m_dFrameBX3 = 0.0;
	m_dFrameBX4 = 0.0;
	m_dFrameLY1 = 0.0;
	m_dFrameLY2 = 0.0;
	m_dFrameLY3 = 0.0;
	m_dFrameLY4 = 0.0;
	m_bUseNewMapCode = FALSE;
	//}}AFX_DATA_INIT
	// Note that LoadIcon does not require a subsequent DestroyIcon in Win32
	m_hIcon = AfxGetApp()->LoadIcon(IDR_MAINFRAME);
}

void CMapFrameToolDlg::DoDataExchange(CDataExchange* pDX)
{
	CDialog::DoDataExchange(pDX);
	//{{AFX_DATA_MAP(CMapFrameToolDlg)
	DDX_Text(pDX, IDC_MAP_CODE, m_csMapCode);
	DDX_Text(pDX, IDC_NEW_MAP_CODE, m_NewMapCode);
	DDX_Text(pDX, IDC_CENTER_LY, m_csCenterLY);
	DDX_Text(pDX, IDC_CENTER_BX, m_csCenterBX);
	DDX_Text(pDX, IDC_MAP_FRAME_BX1, m_csFrameBX1);
	DDX_Text(pDX, IDC_MAP_FRAME_BX2, m_csFrameBX2);
	DDX_Text(pDX, IDC_MAP_FRAME_BX3, m_csFrameBX3);
	DDX_Text(pDX, IDC_MAP_FRAME_BX4, m_csFrameBX4);
	DDX_Text(pDX, IDC_MAP_FRAME_LY1, m_csFrameLY1);
	DDX_Text(pDX, IDC_MAP_FRAME_LY2, m_csFrameLY2);
	DDX_Text(pDX, IDC_MAP_FRAME_LY3, m_csFrameLY3);
	DDX_Text(pDX, IDC_MAP_FRAME_LY4, m_csFrameLY4);
	DDX_Radio(pDX, IDC_RADIO_MAP_SCALE100, m_iMapScale);
	DDX_Radio(pDX, IDC_RADIO_TRAN_STYLE_CODE_COORD, m_iTransStyle);
	DDX_Text(pDX, IDC_MAP_0, m_csMap0);
	DDX_Text(pDX, IDC_MAP_1, m_csMap1);
	DDX_Text(pDX, IDC_MAP_2, m_csMap2);
	DDX_Text(pDX, IDC_MAP_3, m_csMap3);
	DDX_Text(pDX, IDC_MAP_4, m_csMap4);
	DDX_Text(pDX, IDC_MAP_5, m_csMap5);
	DDX_Text(pDX, IDC_MAP_6, m_csMap6);
	DDX_Text(pDX, IDC_MAP_7, m_csMap7);
	DDX_Text(pDX, IDC_MAP_8, m_csMap8);
	DDX_Check(pDX, IDC_CHECK_USE_NEW_MAP_CODE, m_bUseNewMapCode);
	//}}AFX_DATA_MAP
}

BEGIN_MESSAGE_MAP(CMapFrameToolDlg, CDialog)
	//{{AFX_MSG_MAP(CMapFrameToolDlg)
	ON_WM_SYSCOMMAND()
	ON_WM_PAINT()
	ON_WM_QUERYDRAGICON()
	ON_BN_CLICKED(IDC_RADIO_TRAN_STYLE_COORD_CODE, OnRadioTranStyleCoordCode)
	ON_BN_CLICKED(IDC_RADIO_TRAN_STYLE_CODE_COORD, OnRadioTranStyleCodeCoord)
	ON_BN_CLICKED(IDC_RADIO_MAP_SCALE100, OnRadioMapScale100)
	ON_BN_CLICKED(IDC_RADIO_MAP_SCALE50, OnRadioMapScale50)
	ON_BN_CLICKED(IDC_RADIO_MAP_SCALE25, OnRadioMapScale25)
	ON_BN_CLICKED(IDC_RADIO_MAP_SCALE10, OnRadioMapScale10)
	ON_BN_CLICKED(IDC_RADIO_MAP_SCALE5, OnRadioMapScale5)
	ON_BN_CLICKED(IDC_RADIO_MAP_SCALE25000, OnRadioMapScale25000)
	ON_BN_CLICKED(IDC_RADIO_MAP_SCALE1, OnRadioMapScale1)
	ON_BN_CLICKED(IDC_CHECK_USE_NEW_MAP_CODE, OnCheckUseNewMapCode)
	//}}AFX_MSG_MAP
END_MESSAGE_MAP()

/////////////////////////////////////////////////////////////////////////////
// CMapFrameToolDlg message handlers

BOOL CMapFrameToolDlg::OnInitDialog()
{
	CDialog::OnInitDialog();

	
	
	// Add "About..." menu item to system menu.

	// IDM_ABOUTBOX must be in the system command range.
	ASSERT((IDM_ABOUTBOX & 0xFFF0) == IDM_ABOUTBOX);
	ASSERT(IDM_ABOUTBOX < 0xF000);

	CMenu* pSysMenu = GetSystemMenu(FALSE);
	if (pSysMenu != NULL)
	{
		CString strAboutMenu;
		strAboutMenu.LoadString(IDS_ABOUTBOX);
		if (!strAboutMenu.IsEmpty())
		{
			pSysMenu->AppendMenu(MF_SEPARATOR);
			pSysMenu->AppendMenu(MF_STRING, IDM_ABOUTBOX, strAboutMenu);
		}
	}

	// Set the icon for this dialog.  The framework does this automatically
	//  when the application's main window is not a dialog
	SetIcon(m_hIcon, TRUE);			// Set big icon
	SetIcon(m_hIcon, FALSE);		// Set small icon
	
	// TODO: Add extra initialization here
	UpdateData(FALSE);
	return TRUE;  // return TRUE  unless you set the focus to a control
}

void CMapFrameToolDlg::OnSysCommand(UINT nID, LPARAM lParam)
{
	if ((nID & 0xFFF0) == IDM_ABOUTBOX)
	{
		CAboutDlg dlgAbout;
		dlgAbout.DoModal();
	}
	else
	{
		CDialog::OnSysCommand(nID, lParam);
	}
}

// If you add a minimize button to your dialog, you will need the code below
//  to draw the icon.  For MFC applications using the document/view model,
//  this is automatically done for you by the framework.

void CMapFrameToolDlg::OnPaint() 
{
	if (IsIconic())
	{
		CPaintDC dc(this); // device context for painting

		SendMessage(WM_ICONERASEBKGND, (WPARAM) dc.GetSafeHdc(), 0);

		// Center icon in client rectangle
		int cxIcon = GetSystemMetrics(SM_CXICON);
		int cyIcon = GetSystemMetrics(SM_CYICON);
		CRect rect;
		GetClientRect(&rect);
		int x = (rect.Width() - cxIcon + 1) / 2;
		int y = (rect.Height() - cyIcon + 1) / 2;

		// Draw the icon
		dc.DrawIcon(x, y, m_hIcon);
	}
	else
	{
		CDialog::OnPaint();
	}
}

// The system calls this to obtain the cursor to display while the user drags
//  the minimized window.
HCURSOR CMapFrameToolDlg::OnQueryDragIcon()
{
	return (HCURSOR) m_hIcon;
}

//声明“图幅信息类”实例
CMapFrameInfo MapFrameInfo;

void CMapFrameToolDlg::OnOK() 
{
	// TODO: Add extra validation here
	CWnd::UpdateData(TRUE);

	int MapScale;

	//根据用户选择的给比例尺信息变量MapScale赋值
	switch(m_iMapScale)
	{
		case 0:
			MapScale = 100;
			break;
		case 1:
			MapScale = 50;
			break;
		case 2:
			MapScale = 25;
			break;
		case 3:
			MapScale = 10;
			break;
		case 4:
			MapScale = 5;
			break;
		case 5:
			MapScale = 25000;
			break;
		case 6:
			MapScale = 1;
			break;
		default :
			break;
	}
	
	
	if(m_iTransStyle == 0)//图幅编号计算图廓经纬度
	{
		if (!m_bUseNewMapCode)//使用传统图幅编号计算图廓点经纬度
		{
			if(m_csMapCode=="")
			{
				MapFrameInfo.MsgBox("请输入图幅编号！");
				return;
			}
			MapFrameInfo.MapCode2LB(m_csMapCode,MapScale);
		}
		else//使用新图幅编号计算图廓点经纬度
		{
			if(m_NewMapCode.GetLength()<10)
			{
				MapFrameInfo.MsgBox("图幅编号输入不正确！！");
				return;
			}
			MapFrameInfo.NeweMapCode2LB(m_NewMapCode,MapScale);
		}
			
		m_dCenterLY = MapFrameInfo.DEG_DMS(MapFrameInfo.mL1 + MapFrameInfo.dL/2.);
		m_dCenterBX = MapFrameInfo.DEG_DMS(MapFrameInfo.mB1 + MapFrameInfo.dB/2.);
		
		//用计算出的图幅中心点经纬度再算回图号，为了使界面上显示的新旧图号对应同一图幅
		m_csMapCode = MapFrameInfo.LB2MapCode(MapFrameInfo.DMS_DEG(m_dCenterLY),MapFrameInfo.DMS_DEG(m_dCenterBX),MapScale);
		m_NewMapCode = MapFrameInfo.NewMapCode;

		//设置图廓点经纬度
		m_dFrameLY1 = MapFrameInfo.DEG_DMS(MapFrameInfo.mL1);
		m_dFrameBX1 = MapFrameInfo.DEG_DMS(MapFrameInfo.mB1);
		m_dFrameLY2 = MapFrameInfo.DEG_DMS(MapFrameInfo.mL2);
		m_dFrameBX2 = MapFrameInfo.DEG_DMS(MapFrameInfo.mB2);
		m_dFrameLY3 = MapFrameInfo.DEG_DMS(MapFrameInfo.mL3);
		m_dFrameBX3 = MapFrameInfo.DEG_DMS(MapFrameInfo.mB3);
		m_dFrameLY4 = MapFrameInfo.DEG_DMS(MapFrameInfo.mL4);
		m_dFrameBX4 = MapFrameInfo.DEG_DMS(MapFrameInfo.mB4);
	}
	else if(m_iTransStyle == 1)//经纬度计算图幅编号
	{
		m_dCenterLY = atof(m_csCenterLY);
		m_dCenterBX = atof(m_csCenterBX);
		
		//判断是否在我国境内
		if(m_dCenterLY<70 || m_dCenterLY>140 || m_dCenterBX<3 || m_dCenterBX>50)
		{
			AfxMessageBox(_T("经纬度数值不在我国境内，请重新输入！"));
			return;
		}
		
		m_csMapCode = MapFrameInfo.LB2MapCode(MapFrameInfo.DMS_DEG(m_dCenterLY),MapFrameInfo.DMS_DEG(m_dCenterBX),MapScale);
		
		//用计算出和图号再算回经纬度，以设置图幅经纬度信息
		MapFrameInfo.MapCode2LB(m_csMapCode,MapScale);
		
		
		m_NewMapCode = MapFrameInfo.NewMapCode;

		m_dFrameLY1 = MapFrameInfo.DEG_DMS(MapFrameInfo.mL1);
		m_dFrameBX1 = MapFrameInfo.DEG_DMS(MapFrameInfo.mB1);
		m_dFrameLY2 = MapFrameInfo.DEG_DMS(MapFrameInfo.mL2);
		m_dFrameBX2 = MapFrameInfo.DEG_DMS(MapFrameInfo.mB2);
		m_dFrameLY3 = MapFrameInfo.DEG_DMS(MapFrameInfo.mL3);
		m_dFrameBX3 = MapFrameInfo.DEG_DMS(MapFrameInfo.mB3);
		m_dFrameLY4 = MapFrameInfo.DEG_DMS(MapFrameInfo.mL4);
		m_dFrameBX4 = MapFrameInfo.DEG_DMS(MapFrameInfo.mB4);

	}

	//计算接图表
	double dCenterL;
	double dCenterB;
	
	//MapFrameInfo.MapCode2LB(m_csMapCode,MapScale);

	dCenterL = MapFrameInfo.mL1 + MapFrameInfo.dL/2.0;
	dCenterB = MapFrameInfo.mB1 + MapFrameInfo.dB/2.0;

	m_csMap0 = MapFrameInfo.MapCode;
	
	m_csMap1 = MapFrameInfo.LB2MapCode(dCenterL-MapFrameInfo.dL,dCenterB+MapFrameInfo.dB,MapScale);
	m_csMap2 = MapFrameInfo.LB2MapCode(dCenterL,                dCenterB+MapFrameInfo.dB,MapScale);
	m_csMap3 = MapFrameInfo.LB2MapCode(dCenterL+MapFrameInfo.dL,dCenterB+MapFrameInfo.dB,MapScale);

	m_csMap4 = MapFrameInfo.LB2MapCode(dCenterL-MapFrameInfo.dL,dCenterB,MapScale);
	m_csMap5 = MapFrameInfo.LB2MapCode(dCenterL+MapFrameInfo.dL,dCenterB,MapScale);

	m_csMap6 = MapFrameInfo.LB2MapCode(dCenterL-MapFrameInfo.dL,dCenterB-MapFrameInfo.dB,MapScale);
	m_csMap7 = MapFrameInfo.LB2MapCode(dCenterL,                dCenterB-MapFrameInfo.dB,MapScale);
	m_csMap8 = MapFrameInfo.LB2MapCode(dCenterL+MapFrameInfo.dL,dCenterB-MapFrameInfo.dB,MapScale);
	
	UpdateData(FALSE);
}



BOOL CMapFrameToolDlg::UpdateData(BOOL bSaveAndValidate)
{
	if(bSaveAndValidate)//控件数据到变量
	{
		CWnd::UpdateData(bSaveAndValidate);

		m_dCenterLY = atof(m_csCenterLY);
		m_dCenterBX = atof(m_csCenterBX);

	}
	else//变量数据到控件
	{
		m_csCenterLY.Format("%12.04lf",m_dCenterLY);
		m_csCenterBX.Format("%12.04lf",m_dCenterBX);
		
		m_csFrameBX1.Format("%12.04lf",m_dFrameBX1);
		m_csFrameBX2.Format("%12.04lf",m_dFrameBX2);
		m_csFrameBX3.Format("%12.04lf",m_dFrameBX3);
		m_csFrameBX4.Format("%12.04lf",m_dFrameBX4);
		m_csFrameLY1.Format("%12.04lf",m_dFrameLY1);
		m_csFrameLY2.Format("%12.04lf",m_dFrameLY2);
		m_csFrameLY3.Format("%12.04lf",m_dFrameLY3);
		m_csFrameLY4.Format("%12.04lf",m_dFrameLY4);
	}
	return CWnd::UpdateData(bSaveAndValidate);
}


void CMapFrameToolDlg::ClearMapTabCoordCtrlData(void)
{
	m_csFrameBX1="0";
	m_csFrameBX2="0";
	m_csFrameBX3="0";
	m_csFrameBX4="0";
	m_csFrameLY1="0";
	m_csFrameLY2="0";
	m_csFrameLY3="0";
	m_csFrameLY4="0";

	
	m_csMap0="";
	m_csMap1="";
	m_csMap2="";
	m_csMap3="";
	m_csMap4="";
	m_csMap5="";
	m_csMap6="";
	m_csMap7="";
	m_csMap8="";

	CWnd::UpdateData(FALSE);
}




void CMapFrameToolDlg::OnRadioMapScale100() 
{
	// TODO: Add your control notification handler code here
	m_iMapScale = 0;
	ClearMapTabCoordCtrlData();
	m_csMapCode = "";
	m_NewMapCode = "";
	if(m_iTransStyle == 0)
		m_csMapCode = "I50";
	UpdateData(FALSE);

	//OnOK();
}

void CMapFrameToolDlg::OnRadioMapScale50() 
{
	// TODO: Add your control notification handler code here
	m_iMapScale = 1;
	ClearMapTabCoordCtrlData();
	m_csMapCode = "";
	m_NewMapCode = "";
	if(m_iTransStyle == 0)
		m_csMapCode = "I501";
	UpdateData(FALSE);
	//OnOK();
}

void CMapFrameToolDlg::OnRadioMapScale25() 
{
	// TODO: Add your control notification handler code here
	m_iMapScale = 2;
	ClearMapTabCoordCtrlData();
	m_csMapCode = "";
	m_NewMapCode = "";
	if(m_iTransStyle == 0)
		m_csMapCode = "I5012";
	UpdateData(FALSE);
	//nOK();
}

void CMapFrameToolDlg::OnRadioMapScale10() 
{
	// TODO: Add your control notification handler code here
	m_iMapScale = 3;
	ClearMapTabCoordCtrlData();
	m_csMapCode = "";
	m_NewMapCode = "";
	if(m_iTransStyle == 0)
		m_csMapCode = "I50120";
	UpdateData(FALSE);
	//OnOK();
}

void CMapFrameToolDlg::OnRadioMapScale5() 
{
	// TODO: Add your control notification handler code here
	m_iMapScale = 4;
	ClearMapTabCoordCtrlData();
	m_csMapCode = "";
	m_NewMapCode = "";
	if(m_iTransStyle == 0)
		m_csMapCode = "I501201";
	UpdateData(FALSE);
	//OnOK();
}

void CMapFrameToolDlg::OnRadioMapScale25000() 
{
	// TODO: Add your control notification handler code here
	m_iMapScale = 5;
	ClearMapTabCoordCtrlData();
	m_csMapCode = "";
	m_NewMapCode = "";
	if(m_iTransStyle == 0)
		m_csMapCode = "I5012013";
	UpdateData(FALSE);
	//OnOK();
}

void CMapFrameToolDlg::OnRadioMapScale1() 
{
	// TODO: Add your control notification handler code here
	m_iMapScale = 6;
	ClearMapTabCoordCtrlData();
	m_csMapCode = "";
	m_NewMapCode = "";
	if(m_iTransStyle == 0)
		m_csMapCode = "I5012063";
	UpdateData(FALSE);
	//OnOK();
}

void CMapFrameToolDlg::OnRadioTranStyleCodeCoord() 
{
	// TODO: Add your control notification handler code here
	GetDlgItem(IDC_CHECK_USE_NEW_MAP_CODE)->EnableWindow(TRUE);
	m_iTransStyle = 0;

	m_dCenterLY = 0;
	m_dCenterBX = 0;
	
	UpdateData(FALSE);
	ClearMapTabCoordCtrlData();
	//OnOK();
}


void CMapFrameToolDlg::OnRadioTranStyleCoordCode() 
{
	// TODO: Add your control notification handler code here
	GetDlgItem(IDC_CHECK_USE_NEW_MAP_CODE)->EnableWindow(FALSE);
	m_iTransStyle = 1;

	m_csMapCode = "";
	m_NewMapCode = "";
	
	UpdateData(FALSE);
	ClearMapTabCoordCtrlData();
	//OnOK();
}



void CMapFrameToolDlg::OnCheckUseNewMapCode() 
{
	// TODO: Add your control notification handler code here
	CWnd::UpdateData(TRUE);
	if(m_bUseNewMapCode)
	{
		GetDlgItem(IDC_NEW_MAP_CODE)->EnableWindow(TRUE);
	}
	else
	{
		GetDlgItem(IDC_NEW_MAP_CODE)->EnableWindow(FALSE);
	}
}
