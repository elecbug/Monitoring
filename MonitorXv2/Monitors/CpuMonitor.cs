using LibreHardwareMonitor.Hardware;
using MonitorXv2.CustomControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonitorXv2.Monitors
{
    public class CpuMonitor : Form
    {
        public CpuMonitor(MainForm form, List<(IHardware, ISensor)> sensors, int taskDelay = 100)
        {
            Text = "CPU";
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            ShowInTaskbar = false;
            ClientSize = new Size(210, 75);
            TopMost = true;
            MinimizeBox = false;
            MaximizeBox = false;

            CustomProgressBar load = new CustomProgressBar()
            {
                Parent = this,
                Location = new Point(5, 5),
                Size = new Size(200, 30),
                Text = "Load",
            };
            CustomProgressBar temp = new CustomProgressBar()
            {
                Parent = this,
                Location = new Point(5, 40),
                Size = new Size(200, 30),
                Text = "Temp",
            };

            new Thread(async () =>
            {
                while (true)
                {
                    form.RefreshHardware();

                    try
                    {
                        Invoke(() =>
                        {
                            load.Percentage = sensors.First(x => x.Item2.Name == "CPU Total" && x.Item2.SensorType == SensorType.Load).Item2.Value!.Value;
                            temp.Percentage = sensors.First(x => x.Item2.Name == "Core Average" && x.Item2.SensorType == SensorType.Temperature).Item2.Value!.Value;
                        });
                    }
                    catch 
                    {
                        return;
                    }

                    await Task.Delay(taskDelay);
                }
            }).Start();
        }
    }
}
