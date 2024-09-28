using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using OpenHardwareMonitor.Hardware;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            getHardwareInfo();
        }

        private void getHardwareInfo()
        {
            Computer computer = new Computer();
            computer.CPUEnabled = true; // CPU 모니터링 활성화
            computer.GPUEnabled = true; // GPU 모니터링 활성화
            computer.Open();

            foreach (var hardwareItem in computer.Hardware)
            {
                if (hardwareItem.HardwareType == HardwareType.CPU)
                {
                    hardwareItem.Update();
                    foreach (var sensor in hardwareItem.Sensors)
                    {
                        if (sensor.SensorType == SensorType.Temperature)
                        {
                            Console.WriteLine($"{sensor.Name}: {sensor.Value} (CPU)");
                        }
                    }
                }
                else if (hardwareItem.HardwareType == HardwareType.GpuNvidia || hardwareItem.HardwareType == HardwareType.GpuAti)
                {
                    hardwareItem.Update();
                    foreach (var sensor in hardwareItem.Sensors)
                    {
                        if (sensor.SensorType == SensorType.Temperature)
                        {
                            Console.WriteLine($"{sensor.Name}: {sensor.Value} (GPU)");
                        }
                    }
                }
            }

            computer.Close();
        }

    }

}
