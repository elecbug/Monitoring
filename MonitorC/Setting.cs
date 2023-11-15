using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    }
}
