using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            listView1.View = View.Details;
            listView1.GridLines = true;
            listView1.FullRowSelect = true;

            listView1.Columns.Add("저장장치", 200, HorizontalAlignment.Center);
            listView1.Columns.Add("온도", 100, HorizontalAlignment.Center);
            listView1.Columns.Add("사용 공간", 300, HorizontalAlignment.Center);
        }        

        private void Form1_Load(object sender, EventArgs e)
        {
            getInfo();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            getInfo();
        }

        private void getInfo()
        {
            using (var monitor = new LibreHardwareMonitor())
            {
                label1.Text = $"CPU 온도: {monitor.GetCPUTemperature()} °C";
                label2.Text = $"GPU 온도: {monitor.GetGPUTemperature()} °C";
                label3.Text = $"마지막 부팅 시간: {monitor.GetLastBootTime()}";

                var cpuInfo = monitor.GetCPUInfo();
                label4.Text = $"CPU: {cpuInfo["Name"]}, 클럭: {cpuInfo["Clock"]}, 부하: {cpuInfo["Load"]}";

                var gpuInfo = monitor.GetGPUInfo();
                label5.Text = $"GPU: {gpuInfo["Name"]}, 클럭: {gpuInfo["Clock"]}, 부하: {gpuInfo["Load"]}, 메모리: {gpuInfo["Memory"]}";

                var mbInfo = monitor.GetMotherboardInfo();
                label6.Text = $"메인보드: {mbInfo["Name"]}, 온도: {mbInfo["Temperature"]}";

                var ramInfo = monitor.GetRAMInfo();
                label7.Text = $"RAM 사용량: {ramInfo["Used"]}, 사용 가능: {ramInfo["Available"]}";

                listView1.Items.Clear();
                var storageInfo = monitor.GetStorageInfo();
                foreach (var storage in storageInfo)
                {
                    ListViewItem item = new ListViewItem(storage["Name"]);
                    item.SubItems.Add(storage["Temperature"]);
                    item.SubItems.Add(storage["UsedSpace"]);
                    listView1.Items.Add(item);
                }
            }
        }

    }
}
