using LibreHardwareMonitor.Hardware;
using System.Diagnostics;
using System.Globalization;

namespace MonitorX
{
    public partial class MainForm : Form
    {
        public TabControl TabControl { get; private set; }
        public Computer Computer { get; private set; }
        public List<ListView> ListViews { get; private set; }
        public Dictionary<ListViewItem, ISensor> ListItems { get; private set; }

        public MainForm()
        {
            InitializeComponent();

            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "MonitorX";
            this.Load += MainFormLoad;

            this.Computer = new Computer()
            {
                IsBatteryEnabled = true,
                IsControllerEnabled = true,
                IsCpuEnabled = true,
                IsGpuEnabled = true,
                IsMemoryEnabled = true,
                IsNetworkEnabled = true,
                IsMotherboardEnabled = true,
                IsPsuEnabled = true,
                IsStorageEnabled = true,
            };
            this.Computer.Open();

            this.TabControl = new TabControl()
            {
                Parent = this,
                Visible = true,
                Dock = DockStyle.Fill,
            };

            this.ListViews = new List<ListView>();
            this.ListItems = new Dictionary<ListViewItem, ISensor>();

            ListSetting();
        }

        private void ListSetting()
        {
            List<IHardware> hardwares = this.Computer.Hardware.ToList();

            for (int i = 0; i < hardwares.Count; i++)
            {
                IHardware? unit = hardwares[i];

                if (unit.SubHardware.Length > 0)
                {
                    hardwares.AddRange(unit.SubHardware.ToList());
                }

                this.TabControl.TabPages.Add(unit.Name);

                ListView view = new ListView()
                {
                    Parent = this.TabControl.TabPages[this.TabControl.TabPages.Count - 1],
                    Visible = true,
                    Dock = DockStyle.Fill,
                    View = View.Details,
                };
                view.ListViewItemSorter = new ListViewColumnSorter();
                view.ColumnClick += ViewColumnClick;

                this.ListViews.Add(view);

                view.Columns.Add("Name");
                view.Columns.Add("Type");
                view.Columns.Add("Value");

                view.Columns[2].TextAlign = HorizontalAlignment.Right;

                for (int j = 0; j < view.Columns.Count; j++)
                {
                    view.Columns[j].Width = this.TabControl.Width / (view.Columns.Count + 1);
                }

                view.Items.Clear();

                unit.Update();

                int idx = 0;
                foreach (var sensor in unit.Sensors)
                {
                    ListViewItem item = new ListViewItem(new string[]
                    {
                        sensor.Name,
                        sensor.SensorType.ToString(),
                        sensor.Value.ToString() + SensorUnit(sensor.SensorType),
                        idx++.ToString(),
                    });

                    this.ListItems.Add(item, sensor);
                    view.Items.Add(item);
                }
            }
        }

        private void ViewColumnClick(object? sender, ColumnClickEventArgs e)
        {
            ListViewColumnSorter sorter = ((sender as ListView)!.ListViewItemSorter as ListViewColumnSorter)!;

            if (e.Column == sorter.SortColumn)
            {
                if (sorter.Order == SortOrder.Ascending)
                {
                    sorter.Order = SortOrder.Descending;
                }
                else if (sorter.Order == SortOrder.Descending)
                {
                    sorter.Order = SortOrder.None;
                }
                else
                {
                    sorter.Order = SortOrder.Ascending;
                }
            }
            else
            {
                sorter.SortColumn = e.Column;
                sorter.Order = SortOrder.Ascending;
            }
            (sender as ListView)!.Sort();
        }

        private void MainFormLoad(object? sender, EventArgs e)
        {
            ListRefresh();
        }

        private string SensorUnit(SensorType sensorType)
        {
            switch (sensorType)
            {
                case SensorType.Voltage: return " V";
                case SensorType.Clock: return " MHz";
                case SensorType.Temperature: return " ¨¬C";
                case SensorType.Load: return " %";
                case SensorType.Power: return " W";
                case SensorType.Data: return " GB";
                case SensorType.SmallData: return " MB";
                case SensorType.Throughput: return " Kbps";
                default: return "";
            }
        }

        private void ListRefresh()
        {
            new Thread(async () =>
            {
                while (true)
                {
                    foreach (var hw in this.Computer.Hardware)
                    {
                        hw.Update();
                    }

                    foreach (var item in this.ListItems)
                    {
                        try
                        {
                            this.Invoke(new Action(() => item.Key.SubItems[2].Text
                                = item.Value.Value.ToString() + SensorUnit(item.Value.SensorType)));
                        }
                        catch (Exception ex)
                        {
                            Debug.WriteLine(ex);
                        }
                    }

                    await Task.Delay(1500);
                }
            }).Start();
        }
    }
}