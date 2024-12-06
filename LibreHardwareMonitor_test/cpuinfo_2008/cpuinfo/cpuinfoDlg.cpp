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

	CString strMessage;
	strMessage.Format(_T("cpu:%s gpu:%s lastboottime:%s"), obj.GetCpu(), obj.GetGpu(), obj.GetLastBootTime());
	AfxMessageBox(strMessage);

	return TRUE;  
}

void CcpuinfoDlg::OnPaint()
{
	CPaintDC dc(this);
}