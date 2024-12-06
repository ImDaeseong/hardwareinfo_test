#include "pch.h"

#include "CpuManager.h"
#include <atlbase.h>
#include <atlsafe.h>
#include <comutil.h>
#pragma comment(lib, "comsuppw.lib")

#import "LibreHardwareMonitorWrapper.tlb" raw_interfaces_only

CpuManager::CpuManager()
{
    m_strCpu = "";
    m_strGpu = "";
    m_strLastBootTime = "";
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

    CComPtr<LibreHardwareMonitorWrapper::IHardwareMonitorWrapper> pWrapper;
    hr = CoCreateInstance(__uuidof(LibreHardwareMonitorWrapper::HardwareMonitorWrapper),
        NULL,
        CLSCTX_INPROC_SERVER,
        __uuidof(LibreHardwareMonitorWrapper::IHardwareMonitorWrapper),
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
        m_strCpu = FormatString(fCpu);
    }

    // GPU �µ� 
    float fGpu = 0.0f;
    if (SUCCEEDED(pWrapper->GetGPUTemperature(&fGpu)))
    {
        m_strGpu = FormatString(fGpu);
    }

    // ������ ���� �ð� 
    BSTR bstrLastBootTime = NULL;
    if (SUCCEEDED(pWrapper->GetLastBootTime(&bstrLastBootTime)))
    {
        CString strTemp(bstrLastBootTime);
        m_strLastBootTime = strTemp;
        SysFreeString(bstrLastBootTime);
    }

    CoUninitialize();
}

CString CpuManager::GetCpu()
{
    return m_strCpu;
}

CString CpuManager::GetGpu()
{
    return m_strGpu;
}

CString CpuManager::GetLastBootTime()
{
    return m_strLastBootTime;
}

