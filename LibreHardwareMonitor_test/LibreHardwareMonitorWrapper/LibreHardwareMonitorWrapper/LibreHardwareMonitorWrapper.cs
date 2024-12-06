using System;
using System.Runtime.InteropServices;
using LibreHardwareMonitor.Hardware;

namespace LibreHardwareMonitorWrapper
{
    [ComVisible(true)]
    [Guid("903F9099-D4CF-451A-970B-A8DCADFB7FC0")]
    public interface IHardwareMonitorWrapper
    {
        float GetCPUTemperature();
        float GetGPUTemperature();
        string GetLastBootTime();
    }

    [Guid("C62C1DF8-3D82-4551-B61E-B2D851583318")]
    [ClassInterface(ClassInterfaceType.None)]
    [ComVisible(true)]
    public class HardwareMonitorWrapper : IHardwareMonitorWrapper
    {
        private Computer computer;

        public HardwareMonitorWrapper()
        {
            computer = new Computer
            {
                IsCpuEnabled = true,
                IsGpuEnabled = true,
                IsMemoryEnabled = true,
                IsMotherboardEnabled = true,
                IsStorageEnabled = true,
                IsNetworkEnabled = true
            };
            computer.Open();
        }

        public float GetCPUTemperature()
        {
            return GetTemperature(HardwareType.Cpu);
        }

        public float GetGPUTemperature()
        {
            float nvidiaTemp = GetTemperature(HardwareType.GpuNvidia);
            float amdTemp = GetTemperature(HardwareType.GpuAmd);

            return nvidiaTemp != 0 ? nvidiaTemp : (amdTemp != 0 ? amdTemp : -1);
        }

        public string GetLastBootTime()
        {
            DateTime lastBootTime = DateTime.Now - TimeSpan.FromMilliseconds(Environment.TickCount);
            return lastBootTime.ToString("yyyy-MM-dd HH:mm:ss");
        }


        private float GetTemperature(HardwareType hardwareType)
        {
            foreach (IHardware hardware in computer.Hardware)
            {
                if (hardware.HardwareType == hardwareType)
                {
                    hardware.Update();
                    foreach (ISensor sensor in hardware.Sensors)
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

        ~HardwareMonitorWrapper()
        {
            computer.Close();
        }
    }
}
