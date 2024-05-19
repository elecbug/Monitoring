using LibreHardwareMonitor.Hardware;
using MonitorXv2.CustomControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonitorXv2.Monitors
{
    public class MemoryMonitor : Form
    {
        public MemoryMonitor(MainForm form, List<(IHardware, ISensor)> sensors, int taskDelay = 100)
        {
            Text = "RAM";
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            ShowInTaskbar = false;
            ClientSize = new Size(210, 40);
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

            new Thread(async () =>
            {
                while (true)
                {
                    form.RefreshHardware();

                    try
                    {
                        Invoke(() =>
                        {
                            load.Percentage = sensors.First(x => x.Item2.Name == "Memory" && x.Item2.SensorType == SensorType.Load).Item2.Value!.Value;
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
