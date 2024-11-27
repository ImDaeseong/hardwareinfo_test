using System;
using System.Runtime.InteropServices;
using OpenHardwareMonitor.Hardware;

namespace OpenHardwareMonitorWrapper
{
    [ComVisible(true)]
    [Guid("022F4B0F-E9C7-44F9-BF74-40F24D2702AB")]
    public interface IHardwareMonitorWrapper
    {
        float GetCPUTemperature();
        float GetGPUTemperature();
        string GetLastBootTime();
    }

    [Guid("03DCCE08-FF50-47C1-B811-4B198D286703")]
    [ClassInterface(ClassInterfaceType.None)]
    [ComVisible(true)]
    public class HardwareMonitorWrapper : IHardwareMonitorWrapper
    {
        private Computer computer;

        public HardwareMonitorWrapper()
        {
            computer = new Computer();
            computer.CPUEnabled = true;
            computer.GPUEnabled = true;
            computer.Open();
        }

        public float GetCPUTemperature()
        {
            return GetTemperature(HardwareType.CPU);
        }

        public float GetGPUTemperature()
        {
            float nvidiaTemp = GetTemperature(HardwareType.GpuNvidia);
            return nvidiaTemp != 0 ? nvidiaTemp : GetTemperature(HardwareType.GpuAti);
        }

        private float GetTemperature(HardwareType hardwareType)
        {
            foreach (var hardware in computer.Hardware)
            {
                if (hardware.HardwareType == hardwareType)
                {
                    hardware.Update();
                    foreach (var sensor in hardware.Sensors)
                    {
                        if (sensor.SensorType == SensorType.Temperature)
                        {
                            return sensor.Value ?? 0;
                        }
                    }
                }
            }
            return 0;
        }

        public string GetLastBootTime()
        {
            DateTime lastBootTime = DateTime.Now - TimeSpan.FromMilliseconds(Environment.TickCount);
            return lastBootTime.ToString("yyyy-MM-dd HH:mm:ss");
        }

        ~HardwareMonitorWrapper()
        {
            computer.Close();
        }
    }
}