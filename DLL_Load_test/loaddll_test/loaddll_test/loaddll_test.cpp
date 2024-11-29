#include "stdafx.h"
#include "loaddll_test.h"
#include "loaddll_testDlg.h"

#ifdef _DEBUG
#define new DEBUG_NEW
#endif

BEGIN_MESSAGE_MAP(Cloaddll_testApp, CWinAppEx)
END_MESSAGE_MAP()

Cloaddll_testApp::Cloaddll_testApp()
{
}

Cloaddll_testApp theApp;

BOOL Cloaddll_testApp::InitInstance()
{
	INITCOMMONCONTROLSEX InitCtrls;
	InitCtrls.dwSize = sizeof(InitCtrls);
	InitCtrls.dwICC = ICC_WIN95_CLASSES;
	InitCommonControlsEx(&InitCtrls);

	CWinAppEx::InitInstance();

	AfxEnableControlContainer();
	
	Cloaddll_testDlg dlg;
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
