#include "stdafx.h"
#include "cpuinfo.h"
#include "cpuinfoDlg.h"

#ifdef _DEBUG
#define new DEBUG_NEW
#endif

CcpuinfoDlg::CcpuinfoDlg(CWnd* pParent /*=NULL*/)
	: CDialog(CcpuinfoDlg::IDD, pParent)
{
}

void CcpuinfoDlg::DoDataExchange(CDataExchange* pDX)
{
	CDialog::DoDataExchange(pDX);
}

BEGIN_MESSAGE_MAP(CcpuinfoDlg, CDialog)
	ON_WM_PAINT()
END_MESSAGE_MAP()

BOOL CcpuinfoDlg::OnInitDialog()
{
	CDialog::OnInitDialog();

	
	CpuManager obj;
	obj.GetCpuInfo();

	return TRUE;  
}

void CcpuinfoDlg::OnPaint()
{
	CPaintDC dc(this);
}