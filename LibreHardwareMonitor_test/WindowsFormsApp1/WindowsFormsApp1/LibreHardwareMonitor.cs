using System;
using System.Collections.Generic;
using LibreHardwareMonitor.Hardware;

namespace WindowsFormsApp1
{
    public class LibreHardwareMonitor : IDisposable
    {
        private readonly Computer _computer;

        public LibreHardwareMonitor()
        {
            _computer = new Computer
            {
                IsCpuEnabled = true,
                IsGpuEnabled = true,
                IsMemoryEnabled = true,
                IsMotherboardEnabled = true,
                IsStorageEnabled = true,
                IsNetworkEnabled = true
            };
            _computer.Open();
        }

        // CPU 온도
        public float GetCPUTemperature()
        {
            return GetTemperature(HardwareType.Cpu);
        }

        // GPU 온도
        public float GetGPUTemperature()
        {
            float nvidiaTemp = GetTemperature(HardwareType.GpuNvidia);
            return nvidiaTemp != 0 ? nvidiaTemp : GetTemperature(HardwareType.GpuAmd);
        }

        // 마지막 부팅 시간
        public string GetLastBootTime()
        {
            DateTime lastBootTime = DateTime.Now - TimeSpan.FromMilliseconds(Environment.TickCount);
            return lastBootTime.ToString("yyyy-MM-dd HH:mm:ss");
        }

        // CPU 정보
        public Dictionary<string, string> GetCPUInfo()
        {
            var cpuInfo = new Dictionary<string, string>();
            foreach (var hardware in _computer.Hardware)
            {
                if (hardware.HardwareType == HardwareType.Cpu)
                {
                    cpuInfo["Name"] = hardware.Name;
                    hardware.Update();
                    foreach (var sensor in hardware.Sensors)
                    {
                        if (sensor.SensorType == SensorType.Clock)
                            cpuInfo["Clock"] = $"{sensor.Value} MHz";
                        else if (sensor.SensorType == SensorType.Load)
                            cpuInfo["Load"] = $"{sensor.Value}%";
                    }
                    break;
                }
            }
            return cpuInfo;
        }

        // GPU 정보
        public Dictionary<string, string> GetGPUInfo()
        {
            var gpuInfo = new Dictionary<string, string>();
            foreach (var hardware in _computer.Hardware)
            {
                if (hardware.HardwareType == HardwareType.GpuNvidia || hardware.HardwareType == HardwareType.GpuAmd)
                {
                    gpuInfo["Name"] = hardware.Name;
                    hardware.Update();
                    foreach (var sensor in hardware.Sensors)
                    {
                        if (sensor.SensorType == SensorType.Clock)
                            gpuInfo["Clock"] = $"{sensor.Value} MHz";
                        else if (sensor.SensorType == SensorType.Load)
                            gpuInfo["Load"] = $"{sensor.Value}%";
                        else if (sensor.SensorType == SensorType.SmallData)
                            gpuInfo["Memory"] = $"{sensor.Value} MB";
                    }
                    break;
                }
            }
            return gpuInfo;
        }

        // RAM 정보
        public Dictionary<string, string> GetRAMInfo()
        {
            var ramInfo = new Dictionary<string, string>();
            foreach (var hardware in _computer.Hardware)
            {
                if (hardware.HardwareType == HardwareType.Memory)
                {
                    hardware.Update();
                    foreach (var sensor in hardware.Sensors)
                    {
                        if (sensor.SensorType == SensorType.Data && sensor.Name.Contains("Used"))
                            ramInfo["Used"] = $"{sensor.Value} GB";
                        else if (sensor.SensorType == SensorType.Data && sensor.Name.Contains("Available"))
                            ramInfo["Available"] = $"{sensor.Value} GB";
                    }
                    break;
                }
            }
            return ramInfo;
        }

        // 메인보드 정보
        public Dictionary<string, string> GetMotherboardInfo()
        {
            var mbInfo = new Dictionary<string, string>();
            try
            {
                foreach (var hardware in _computer.Hardware)
                {
                    if (hardware.HardwareType == HardwareType.Motherboard)
                    {
                        mbInfo["Name"] = hardware.Name ?? "Unknown";
                        hardware.Update();
                        foreach (var sensor in hardware.Sensors)
                        {
                            if (sensor.SensorType == SensorType.Temperature && sensor.Value.HasValue)
                            {
                                mbInfo["Temperature"] = $"{sensor.Value:F1} °C";
                                break;
                            }
                        }
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting motherboard info: {ex.Message}");
            }

            if (!mbInfo.ContainsKey("Name")) mbInfo["Name"] = "Unknown";
            if (!mbInfo.ContainsKey("Temperature")) mbInfo["Temperature"] = "N/A";

            return mbInfo;
        }

        // 스토리지 정보
        public List<Dictionary<string, string>> GetStorageInfo()
        {
            var storageList = new List<Dictionary<string, string>>();
            try
            {
                foreach (var hardware in _computer.Hardware)
                {
                    if (hardware.HardwareType == HardwareType.Storage)
                    {
                        var storageInfo = new Dictionary<string, string>();
                        storageInfo["Name"] = hardware.Name ?? "Unknown";
                        hardware.Update();
                        foreach (var sensor in hardware.Sensors)
                        {
                            if (sensor.SensorType == SensorType.Temperature && sensor.Value.HasValue)
                                storageInfo["Temperature"] = $"{sensor.Value:F1} °C";
                            else if (sensor.SensorType == SensorType.Data && sensor.Name.Contains("Used Space") && sensor.Value.HasValue)
                                storageInfo["UsedSpace"] = $"{sensor.Value:F1} GB";
                        }
                        if (!storageInfo.ContainsKey("Temperature")) storageInfo["Temperature"] = "N/A";
                        if (!storageInfo.ContainsKey("UsedSpace")) storageInfo["UsedSpace"] = "N/A";
                        storageList.Add(storageInfo);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting storage info: {ex.Message}");
            }

            if (storageList.Count == 0)
            {
                storageList.Add(new Dictionary<string, string>
                {
                    ["Name"] = "Unknown",
                    ["Temperature"] = "N/A",
                    ["UsedSpace"] = "N/A"
                });
            }

            return storageList;
        }

        private float GetTemperature(HardwareType hardwareType)
        {
            try
            {
                foreach (var hardware in _computer.Hardware)
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
            }
            catch (Exception ex)
            {
            }
            return 0;
        }

        public void Dispose()
        {
            _computer?.Close();
        }

        ~LibreHardwareMonitor()
        {
            Dispose();
        }
    }
}