#pragma once

#ifndef __AFXWIN_H__
	#error "PCH�� ���� �� ������ �����ϱ� ���� 'stdafx.h'�� �����մϴ�."
#endif

#include "resource.h"		// �� ��ȣ�Դϴ�.

class CcpuinfoApp : public CWinAppEx
{
public:
	CcpuinfoApp();

public:
	virtual BOOL InitInstance();
	DECLARE_MESSAGE_MAP()
};

extern CcpuinfoApp theApp;