#pragma once

class CpuManager
{
public:
	CpuManager();

	void GetCpuInfo();
	CString FormatString(float fValue);

	CString GetCpu();
	CString GetGpu();
	CString GetLastBootTime();

private:
	CString m_strCpu;
	CString m_strGpu;
	CString m_strLastBootTime;
};
