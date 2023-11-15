using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MonitorC
{
    public class Setting
    {
        public int Timer { get; set; } = 3;
        public bool DeleteLog { get; set; } = false;
        public bool IsBatteryEnabled { get; set; } = true;
        public bool IsControllerEnabled { get; set; } = true;
        public bool IsCpuEnabled { get; set; } = true;
        public bool IsGpuEnabled { get; set; } = true;
        public bool IsMemoryEnabled { get; set; } = true;
        public bool IsMotherboardEnabled { get; set; } = true;
        public bool IsNetworkEnabled { get; set; } = true;
        public bool IsPsuEnabled { get; set; } = true;
        public bool IsStorageEnabled { get; set; } = true;

        public void OnOff(bool boolean, Hardware hardware)
        {
            switch (hardware)
            {
                case Hardware.None: return;
                case Hardware.Battery: this.IsBatteryEnabled = boolean; return;
                case Hardware.Controller: this.IsControllerEnabled = boolean; return;
                case Hardware.CPU: this.IsCpuEnabled = boolean; return;
                case Hardware.GPU: this.IsGpuEnabled = boolean; return;
                case Hardware.Memory: this.IsMemoryEnabled = boolean; return;
                case Hardware.MotherBoard: this.IsMotherboardEnabled = boolean; return;
                case Hardware.Network: this.IsNetworkEnabled = boolean; return;
                case Hardware.PSU: this.IsPsuEnabled = boolean; return;
                case Hardware.Storage: this.IsStorageEnabled = boolean; return;
                case Hardware.All:
                    this.IsBatteryEnabled = boolean;
                    this.IsControllerEnabled = boolean;
                    this.IsCpuEnabled = boolean;
                    this.IsGpuEnabled = boolean;
                    this.IsMemoryEnabled = boolean;
                    this.IsMotherboardEnabled = boolean;
                    this.IsNetworkEnabled = boolean;
                    this.IsPsuEnabled = boolean;
                    this.IsStorageEnabled = boolean;

                    return;
            }
        }

        public void Save(string path)
        {
            string json = JsonSerializer.Serialize(this);

            using Stream stream = File.Create(path);
            using StreamWriter writer = new StreamWriter(stream);
            writer.Write(json);
        }
    }
}
