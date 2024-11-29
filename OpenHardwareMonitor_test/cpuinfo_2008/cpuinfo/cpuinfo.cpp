#include "stdafx.h"
#include "cpuinfo.h"
#include "cpuinfoDlg.h"

#ifdef _DEBUG
#define new DEBUG_NEW
#endif

BEGIN_MESSAGE_MAP(CcpuinfoApp, CWinAppEx)
END_MESSAGE_MAP()

CcpuinfoApp::CcpuinfoApp()
{
}


CcpuinfoApp theApp;


BOOL CcpuinfoApp::InitInstance()
{
	
	INITCOMMONCONTROLSEX InitCtrls;
	InitCtrls.dwSize = sizeof(InitCtrls);
	InitCtrls.dwICC = ICC_WIN95_CLASSES;
	InitCommonControlsEx(&InitCtrls);

	CWinAppEx::InitInstance();

	AfxEnableControlContainer();
	
	CcpuinfoDlg dlg;
	m_pMainWnd = &dlg;
	INT_PTR nResponse = dlg.DoModal();
	if (nResponse == IDOK)
	{
	}
	else if (nResponse == IDCANCEL)
	{
	}

	return FALSE;
}
