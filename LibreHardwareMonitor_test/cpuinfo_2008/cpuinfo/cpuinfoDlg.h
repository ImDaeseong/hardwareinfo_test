#pragma once

class CcpuinfoDlg : public CDialog
{
public:
	CcpuinfoDlg(CWnd* pParent = NULL);	// ǥ�� �������Դϴ�.

	enum { IDD = IDD_CPUINFO_DIALOG };

	protected:
	virtual void DoDataExchange(CDataExchange* pDX);	// DDX/DDV �����Դϴ�.

protected:
	virtual BOOL OnInitDialog();
	afx_msg void OnPaint();
	DECLARE_MESSAGE_MAP()
};
