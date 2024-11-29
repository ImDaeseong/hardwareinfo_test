#include "stdafx.h"
#include "loaddll_test.h"
#include "loaddll_testDlg.h"

#ifdef _DEBUG
#define new DEBUG_NEW
#endif


typedef int (__cdecl* AddFunction)(int, int);
typedef const char* (__cdecl* GetMessageFunction)();


void CallMyLibrary() {

    HMODULE hDll = LoadLibrary(L"myFunctionlib.dll");
    if (!hDll) {
        return;
    }

    AddFunction Add = (AddFunction)GetProcAddress(hDll, "Add");
    if (!Add) {
        FreeLibrary(hDll);
        return;
    }

    GetMessageFunction GetMessage = (GetMessageFunction)GetProcAddress(hDll, "GetMessage");
    if (!GetMessage) {
        FreeLibrary(hDll);
        return;
    }

    int nResult = Add(5, 10);

    const char* chMessage = GetMessage();

	CString strMessage;
	strMessage.Format(_T("%d %s"), nResult, CA2T(chMessage));
	AfxMessageBox(strMessage);

    FreeLibrary(hDll);
}

Cloaddll_testDlg::Cloaddll_testDlg(CWnd* pParent /*=NULL*/)
	: CDialog(Cloaddll_testDlg::IDD, pParent)
{
}

void Cloaddll_testDlg::DoDataExchange(CDataExchange* pDX)
{
	CDialog::DoDataExchange(pDX);
}

BEGIN_MESSAGE_MAP(Cloaddll_testDlg, CDialog)
	ON_WM_PAINT()
END_MESSAGE_MAP()

BOOL Cloaddll_testDlg::OnInitDialog()
{
	CDialog::OnInitDialog();

	CallMyLibrary();

	return TRUE; 
}

void Cloaddll_testDlg::OnPaint()
{
	CPaintDC dc(this);
}