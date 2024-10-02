#include "pch.h"
#include <windows.h> 

using namespace System;
using namespace System::Runtime::InteropServices;
using namespace OpenHardwareMonitor::Hardware;


public ref class NativeMethods
{
public:
    [DllImport("user32.dll", CharSet = CharSet::Unicode, SetLastError = true)]
    static HWND FindWindow(String^ lpClassName, String^ lpWindowName);

    [DllImport("user32.dll", CharSet = CharSet::Unicode, SetLastError = true)]
    static IntPtr SendMessageTimeout(IntPtr hWnd, UInt32 Msg, IntPtr wParam, IntPtr lParam, UInt32 fuFlags, UInt32 uTimeout, IntPtr lpdwResult);
};

ref class UpdateVisitor : public IVisitor {
public:
    virtual void VisitComputer(IComputer^ computer) override {
        for each (IHardware ^ hardware in computer->Hardware) {
            hardware->Accept(this);  
        }
    }

    virtual void VisitHardware(IHardware^ hardware) override {
        hardware->Update();  
        for each (IHardware ^ subHardware in hardware->SubHardware) {
            subHardware->Accept(this);
        }
    }

    virtual void VisitSensor(ISensor^ sensor) override {
    }

    virtual void VisitParameter(IParameter^ parameter) override {
    }
};

float GetHardwareTemperature(IHardware^ hardware) {
    for each (ISensor ^ sensor in hardware->Sensors) {
        if (sensor->SensorType == SensorType::Temperature) {
            return sensor->Value.GetValueOrDefault();
        }
    }
    return -1;
}

String^ GetHardwareTemperatureString(IHardware^ hardware, String^ hardwareName) {
    float temperature = GetHardwareTemperature(hardware);
    if (temperature != -1) {
        return String::Format("{0} Temperature: {1}°C", hardwareName, temperature);
    }
    else {
        return String::Format("{0} Temperature: N/A", hardwareName);
    }
}

void SendMessageApp(HWND hwnd, String^ message)
{
    IntPtr ptr = Marshal::StringToHGlobalUni(message);

    COPYDATASTRUCT cds;
    cds.dwData = 1;
    cds.cbData = (DWORD)message->Length * sizeof(wchar_t); 
    cds.lpData = (PVOID)(ptr.ToPointer());

    IntPtr resultPtr = IntPtr::Zero;
    if (NativeMethods::SendMessageTimeout((IntPtr)hwnd, WM_COPYDATA, IntPtr::Zero, (IntPtr)(&cds), SMTO_BLOCK, 3000, resultPtr) == IntPtr::Zero) {
        Console::WriteLine("failed");
    }
    else {
        Console::WriteLine("success");
    }
    Marshal::FreeHGlobal(ptr);
}


int main(array<System::String ^> ^args)
{
    HWND hwnd = (HWND)NativeMethods::FindWindow("myClassName", nullptr);
    if (hwnd == NULL) {
        Console::WriteLine("window not found.");
        return -1;
    }

    Computer^ computer = gcnew Computer();
    computer->CPUEnabled = true;
    computer->GPUEnabled = true;
    computer->Open();

    UpdateVisitor^ updateVisitor = gcnew UpdateVisitor();
    computer->Accept(updateVisitor);

    String^ temperatureMessage = "";
    for each (IHardware ^ hardware in computer->Hardware) {

        if (hardware->HardwareType == HardwareType::CPU) {

            //String^ cpuTemperatureString = GetHardwareTemperatureString(hardware, "CPU");
            //Console::WriteLine(cpuTemperatureString);

            temperatureMessage += GetHardwareTemperatureString(hardware, "CPU") + "\n";
        }
        else if (hardware->HardwareType == HardwareType::GpuNvidia || hardware->HardwareType == HardwareType::GpuAti) {
            
            //String^ gpuTemperatureString = GetHardwareTemperatureString(hardware, "GPU");
            //Console::WriteLine(gpuTemperatureString);  

            temperatureMessage += GetHardwareTemperatureString(hardware, "GPU") + "\n";
        }
    }

    //온도 전달
    if (!String::IsNullOrEmpty(temperatureMessage)) {
        SendMessageApp(hwnd, temperatureMessage);
    }

    computer->Close();

    return 0;
}
