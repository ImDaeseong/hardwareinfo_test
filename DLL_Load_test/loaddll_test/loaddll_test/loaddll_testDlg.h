#pragma once

class Cloaddll_testDlg : public CDialog
{
public:
	Cloaddll_testDlg(CWnd* pParent = NULL);	// ǥ�� �������Դϴ�.

	enum { IDD = IDD_LOADDLL_TEST_DIALOG };

	protected:
	virtual void DoDataExchange(CDataExchange* pDX);	// DDX/DDV �����Դϴ�.

protected:
	virtual BOOL OnInitDialog();
	afx_msg void OnPaint();
	DECLARE_MESSAGE_MAP()
};
