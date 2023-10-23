using OpenHardwareMonitor.Hardware;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Monitoring
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
            this.Text = "Monitoring";
            
            this.Computer = new Computer()
            {
                CPUEnabled = true,
                GPUEnabled = true,
                FanControllerEnabled = true,
                RAMEnabled = true,
                HDDEnabled = true,
                MainboardEnabled = true,
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

            List<IHardware> hardwares = this.Computer.Hardware.ToList();

            foreach (var unit in hardwares)
            {
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

                this.ListViews.Add(view);

                view.Columns.Add("Name");
                view.Columns.Add("Type");
                view.Columns.Add("Value");

                view.Columns[2].TextAlign = HorizontalAlignment.Right;

                for (int i = 0; i < view.Columns.Count; i++)
                {
                    view.Columns[i].Width = this.TabControl.Width / (view.Columns.Count + 1);
                }

                view.Items.Clear();

                unit.Update();

                foreach (var sensor in unit.Sensors)
                {
                    ListViewItem item = new ListViewItem(new string[]
                    {
                        sensor.Name,
                        sensor.SensorType.ToString(),
                        sensor.Value.ToString() + SensorUnit(sensor.SensorType)
                    });

                    this.ListItems.Add(item, sensor);
                    view.Items.Add(item);
                }

                this.Load += MainFormLoad;
            }
        }

        private void MainFormLoad(object sender, EventArgs e)
        {
            ListRefresh();
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
                default: return " ??";
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
