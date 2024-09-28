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

            InitListView();
        }
        private void InitListView()
        {
            listView1.View = View.Details;
            listView1.GridLines = true;
            listView1.FullRowSelect = true;
            listView1.Columns.Add("온도", 300, HorizontalAlignment.Center);

            listView2.View = View.Details;
            listView2.GridLines = true;
            listView2.FullRowSelect = true;
            listView2.Columns.Add("온도", 300, HorizontalAlignment.Center);
        }

        private void setListview1(String sValue)
        {
            ListViewItem item = new ListViewItem();
            item.Text = sValue;
            listView1.Items.Add(item);
        }

        private void setListview2(String sValue)
        {
            ListViewItem item = new ListViewItem();
            item.Text = sValue;
            listView2.Items.Add(item);
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
                            //Console.WriteLine($"{sensor.Name}: {sensor.Value}");
                            String sValue = String.Format("{0}: {1}", sensor.Name, sensor.Value);
                            setListview1(sValue);
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
                            //Console.WriteLine($"{sensor.Name}: {sensor.Value}");
                            String sValue = String.Format("{0}: {1}", sensor.Name, sensor.Value);
                            setListview2(sValue);
                        }
                    }
                }
            }

            computer.Close();
        }

    }

}
