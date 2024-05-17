using LibreHardwareMonitor.Hardware;
using System.Diagnostics;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HWM
{
    public partial class MainWindow : Window
    {
        public Computer Computer { get; private set; } = new Computer()
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
        public List<IHardware> Hardwares { get; private set; } = new List<IHardware>();
        public List<(IHardware, ISensor)> Sensors { get; private set; } = new List<(IHardware, ISensor)>();

        public MainWindow()
        {
            InitializeComponent();

            Computer.Open();

            foreach (var hw in Computer.Hardware)
            {
                Hardwares.Add(hw);

                foreach (var sensor in hw.Sensors)
                {
                    Sensors.Add((hw, sensor));

                    Debug.WriteLine($"{hw.Name, -20} {sensor.Name, -20} {sensor.SensorType, -20} {sensor.Value, -20}{SensorUnit(sensor.SensorType)}");
                }
            }
        }

        private void RefreshHardware()
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
    }
}