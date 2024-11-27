#include "pch.h"
#include "CpuManager.h"

#include <atlbase.h>
#include <atlsafe.h>
#include <comutil.h>
#pragma comment(lib, "comsuppw.lib")

#import "OpenHardwareMonitorWrapper.tlb" raw_interfaces_only

CpuManager::CpuManager() 
{

}

CString CpuManager::FormatString(float fValue)
{
	// �Ҽ��� �� �ڸ�����
    CString strResult;
    strResult.Format(_T("%.2f"), fValue); 
    return strResult;
}

void CpuManager::GetCpuInfo()
{
    HRESULT hr = CoInitialize(NULL);
    if (FAILED(hr)) {
        return;
    }

    CComPtr<OpenHardwareMonitorWrapper::IHardwareMonitorWrapper> pWrapper;
    hr = CoCreateInstance(__uuidof(OpenHardwareMonitorWrapper::HardwareMonitorWrapper), 
                          NULL, 
                          CLSCTX_INPROC_SERVER, 
                          __uuidof(OpenHardwareMonitorWrapper::IHardwareMonitorWrapper), 
                          (void**)&pWrapper);
    if (FAILED(hr) || !pWrapper) 
	{
        CoUninitialize();
        return;
    }

    // CPU �µ� 
    float fCpu = 0.0f;
    if (SUCCEEDED(pWrapper->GetCPUTemperature(&fCpu))) 
	{
        CString strCpu = FormatString(fCpu);

    }

    // GPU �µ� 
    float fGpu = 0.0f;
    if (SUCCEEDED(pWrapper->GetGPUTemperature(&fGpu))) 
	{
        CString strGpu = FormatString(fGpu);
    }

    // ������ ���� �ð� 
    BSTR bstrLastBootTime = NULL;
    if (SUCCEEDED(pWrapper->GetLastBootTime(&bstrLastBootTime)))
	{
        CString strLastBootTime(bstrLastBootTime);
        SysFreeString(bstrLastBootTime); 
    }

    CoUninitialize();
}
