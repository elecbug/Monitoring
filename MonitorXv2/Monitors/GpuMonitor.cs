using LibreHardwareMonitor.Hardware;
using MonitorXv2.CustomControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonitorXv2.Monitors
{
    public class GpuMonitor : Form
    {
        public GpuMonitor(MainForm form, List<(IHardware, ISensor)> sensors, int taskDelay = 100)
        {
            Text = "GPU";
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            ShowInTaskbar = false;
            ClientSize = new Size(210, 115);
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
            CustomProgressBar power = new CustomProgressBar()
            {
                Parent = this,
                Location = new Point(5, 75),
                Size = new Size(200, 30),
                Text = "Power",
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
                            load.Percentage = sensors.First(x => x.Item2.Name == "GPU Core" && x.Item2.SensorType == SensorType.Load).Item2.Value!.Value;
                            temp.Percentage = sensors.First(x => x.Item2.Name == "GPU Core" && x.Item2.SensorType == SensorType.Temperature).Item2.Value!.Value;
                            power.Percentage = sensors.First(x => x.Item2.Name == "GPU Package" && x.Item2.SensorType == SensorType.Power).Item2.Value!.Value;
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
