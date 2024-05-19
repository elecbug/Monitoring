using LibreHardwareMonitor.Hardware;
using MonitorXv2.Monitors;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonitorXv2
{
    public class MainForm : Form
    {
        public TableLayoutPanel MainLayout { get; private set; }
        public GroupBox CheckGroup { get; private set; }
        public CheckBox[] CheckBoxes { get; private set; }
        public Button CreateButton { get; private set; }

        public Computer Computer { get; private set; }
        public List<IHardware> Hardwares { get; private set; } = new List<IHardware>();
        public List<(IHardware, ISensor)> Sensors { get; private set; } = new List<(IHardware, ISensor)>();

        public MainForm()
        {
            // Form setting
            {
                ClientSize = new Size(200, 300);
                Text = "MonitorX v2";
            }

            // Component setting
            {
                MainLayout = new TableLayoutPanel()
                {
                    Parent = this,
                    Dock = DockStyle.Fill,
                };
                MainLayout.ColumnStyles.Add(new ColumnStyle() { Width = 5, SizeType = SizeType.Absolute });
                MainLayout.ColumnStyles.Add(new ColumnStyle() { Width = 1, SizeType = SizeType.Percent });
                MainLayout.ColumnStyles.Add(new ColumnStyle() { Width = 5, SizeType = SizeType.Absolute });
                MainLayout.RowStyles.Add(new RowStyle() { Height = 5, SizeType = SizeType.Absolute });
                MainLayout.RowStyles.Add(new RowStyle() { Height = 1, SizeType = SizeType.Percent });
                MainLayout.RowStyles.Add(new RowStyle() { Height = 35, SizeType = SizeType.Absolute });
                MainLayout.RowStyles.Add(new RowStyle() { Height = 5, SizeType = SizeType.Absolute });

                CheckGroup = new GroupBox()
                {
                    Parent = MainLayout,
                    Dock = DockStyle.Fill,
                    Text = "Show",
                };
                SetMainLayout(CheckGroup, 1, 1);

                CheckBoxes = new CheckBox[9];

                for (int i = 0; i < 9; i++)
                {
                    CheckBoxes[i] = new CheckBox()
                    {
                        Parent = CheckGroup,
                        Location = new Point(5, 15 + i * 20),
                        Text = HardwareFromIndex(i),
                        Checked = true,
                        Visible = i == 1 || i == 2 || i == 3,
                    };
                }

                CreateButton = new Button()
                {
                    Parent = MainLayout,
                    Text = "Create",
                    Dock = DockStyle.Fill,
                };
                SetMainLayout(CreateButton, 2, 1);

                CreateButton.Click += (s, e) =>
                {
                    if (CheckBoxes[1].Checked)
                    {
                        new CpuMonitor(this, Sensors).Show();
                    }
                    if (CheckBoxes[2].Checked)
                    {
                        new GpuMonitor(this, Sensors).Show();
                    }
                    if (CheckBoxes[3].Checked)
                    {
                        new MemoryMonitor(this, Sensors).Show();
                    }
                };
            }

            // HW setting
            {
                Computer = new Computer()
                {
                    IsMotherboardEnabled = true,
                    IsCpuEnabled = true,
                    IsGpuEnabled = true,
                    IsMemoryEnabled = true,
                    IsNetworkEnabled = true,
                    IsStorageEnabled = true,
                    IsBatteryEnabled = true,
                    IsPsuEnabled = true,
                    IsControllerEnabled = true,
                };

                Computer.Open();

                foreach (var hw in Computer.Hardware)
                {
                    Hardwares.Add(hw);

                    foreach (var sensor in hw.Sensors)
                    {
                        Sensors.Add((hw, sensor));

                        Debug.WriteLine($"{hw.Name,-20} {sensor.Name,-20} {sensor.SensorType,-20} " +
                            $"{sensor.Value,-20}{SensorUnit(sensor.SensorType)}");
                    }
                }
            }
        }

        public void RefreshHardware()
        {
            foreach (var hw in Hardwares)
            {
                hw.Update();
            }
        }

        private string SensorUnit(SensorType sensorType)
        {
            switch (sensorType)
            {
                case SensorType.Voltage: return " V";
                case SensorType.Clock: return " MHz";
                case SensorType.Temperature: return " ºC";
                case SensorType.Load: return " %";
                case SensorType.Power: return " W";
                case SensorType.Data: return " GB";
                case SensorType.SmallData: return " MB";
                case SensorType.Throughput: return " bps";
                default: return "";
            }
        }

        private void SetMainLayout(Control control, int row, int col)
        {
            MainLayout.Controls.Add(control, col, row);
        }

        private string HardwareFromIndex(int idx)
        {
            switch (idx)
            {
                case 0: return "Board";
                case 1: return "CPU";
                case 2: return "GPU";
                case 3: return "RAM";
                case 4: return "Network";
                case 5: return "Disk";
                case 6: return "Battery";
                case 7: return "Power";
                case 8: return "Controller";
                default: throw new IndexOutOfRangeException();
            }
        }
    }
}
