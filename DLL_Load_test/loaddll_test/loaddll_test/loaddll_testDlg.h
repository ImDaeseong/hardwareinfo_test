#pragma once

class Cloaddll_testDlg : public CDialog
{
public:
	Cloaddll_testDlg(CWnd* pParent = NULL);	// 표준 생성자입니다.

	enum { IDD = IDD_LOADDLL_TEST_DIALOG };

	protected:
	virtual void DoDataExchange(CDataExchange* pDX);	// DDX/DDV 지원입니다.

protected:
	virtual BOOL OnInitDialog();
	afx_msg void OnPaint();
	DECLARE_MESSAGE_MAP()
};
