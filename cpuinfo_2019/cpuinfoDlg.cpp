#include "pch.h"
#include "framework.h"
#include "cpuinfo.h"
#include "cpuinfoDlg.h"
#include "afxdialogex.h"

#ifdef _DEBUG
#define new DEBUG_NEW
#endif

CcpuinfoDlg::CcpuinfoDlg(CWnd* pParent /*=nullptr*/)
	: CDialogEx(IDD_CPUINFO_DIALOG, pParent)
{
}

void CcpuinfoDlg::DoDataExchange(CDataExchange* pDX)
{
	CDialogEx::DoDataExchange(pDX);
}

BEGIN_MESSAGE_MAP(CcpuinfoDlg, CDialogEx)
	ON_WM_PAINT()
END_MESSAGE_MAP()

BOOL CcpuinfoDlg::OnInitDialog()
{
	CDialogEx::OnInitDialog();

	CpuManager obj;
	obj.GetCpuInfo();

	return TRUE;  
}

void CcpuinfoDlg::OnPaint()
{
	CPaintDC dc(this);
}

