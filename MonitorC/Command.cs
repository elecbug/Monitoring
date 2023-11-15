using LibreHardwareMonitor.Hardware;
using System.Diagnostics;

namespace MonitorC
{
    public class Command
    {
        private const int PADDING = 13;
        public const string InputPrefix = "<< ";
        public const string OutputPrefix = ">> ";
        public const string EmptyPrefix = "   ";

        private string[] values = new string[0];
        public string Value { set => this.values = value.Split(' '); }

        public Computer Computer { get; private set; }
        public Setting Setting { get; private set; }

        public Command(Computer computer, Setting setting)
        {
            this.Computer = computer;
            this.Setting = setting;

            Console.WriteLine(Command.OutputPrefix + "Starting MonitorC, Welcome "
                + Environment.UserName + "!");
            Console.WriteLine(Command.OutputPrefix + "If you need help, please enter \"help\"");
        }

        public void RunCommand()
        {
            this.Computer.Reset();

            if (this.values.Length == 0)
                return;

            try
            {
                if (this.values[0].ToLower().Equals(Help))
                {

                }
                else if (this.values[0].ToLower().Equals(Enabled))
                {
                    Hardware type = Hardware.None;

                    if (this.values[1].ToLower().Equals(All))
                    {
                        type = Hardware.All;

                        var hwList = this.Computer.Hardware.ToList();

                        foreach (var hw in hwList)
                        {
                            Console.WriteLine(Command.OutputPrefix + "Is enabled "
                                + hw.HardwareType.ToString().PadLeft(PADDING) + ", " + hw.Name);
                        }
                    }
                    else if (Battery.Where(x => x.Equals(this.values[1].ToLower())).Count() > 0)
                    {
                        type = Hardware.Battery;

                        var hwList = this.Computer.Hardware
                            .Where(x => x.HardwareType == HardwareType.Battery).ToList();

                        foreach (var hw in hwList)
                        {
                            Console.WriteLine(Command.OutputPrefix + "Is enabled "
                                + hw.HardwareType.ToString().PadLeft(PADDING) + ", " + hw.Name);
                        }

                        if (hwList.Count == 0)
                        {
                            Console.WriteLine(Command.OutputPrefix + "Is not Enabled "
                                + HardwareType.Battery.ToString());
                        }
                    }
                    else if (Controller.Where(x => x.Equals(this.values[1].ToLower())).Count() > 0)
                    {
                        type = Hardware.Controller;

                        var hwList = this.Computer.Hardware
                            .Where(x => x.HardwareType == HardwareType.EmbeddedController
                            || x.HardwareType == HardwareType.Cooler || x.HardwareType == HardwareType.SuperIO)
                            .ToList();

                        foreach (var hw in hwList)
                        {
                            Console.WriteLine(Command.OutputPrefix + "Is enabled "
                                + hw.HardwareType.ToString().PadLeft(PADDING) + ", " + hw.Name);
                        }

                        if (hwList.Count == 0)
                        {
                            Console.WriteLine(Command.OutputPrefix + "Is not Enabled "
                                + HardwareType.EmbeddedController.ToString());
                        }
                    }
                    else if (CPU.Where(x => x.Equals(this.values[1].ToLower())).Count() > 0)
                    {
                        type = Hardware.CPU;

                        var hwList = this.Computer.Hardware
                            .Where(x => x.HardwareType == HardwareType.Cpu).ToList();

                        foreach (var hw in hwList)
                        {
                            Console.WriteLine(Command.OutputPrefix + "Is enabled "
                                + hw.HardwareType.ToString().PadLeft(PADDING) + ", " + hw.Name);
                        }

                        if (hwList.Count == 0)
                        {
                            Console.WriteLine(Command.OutputPrefix + "Is not Enabled "
                                + HardwareType.Cpu.ToString());
                        }
                    }
                    else if (GPU.Where(x => x.Equals(this.values[1].ToLower())).Count() > 0)
                    {
                        type = Hardware.GPU;

                        var hwList = this.Computer.Hardware.Where(x => x.HardwareType
                            == HardwareType.GpuNvidia || x.HardwareType == HardwareType.GpuAmd
                            || x.HardwareType == HardwareType.GpuIntel).ToList();

                        foreach (var hw in hwList)
                        {
                            Console.WriteLine(Command.OutputPrefix + "Is enabled "
                                + hw.HardwareType.ToString().PadLeft(PADDING) + ", " + hw.Name);
                        }

                        if (hwList.Count == 0)
                        {
                            Console.WriteLine(Command.OutputPrefix + "Is not Enabled "
                                + "GPU");
                        }
                    }
                    else if (Memory.Where(x => x.Equals(this.values[1].ToLower())).Count() > 0)
                    {
                        type = Hardware.Memory;

                        var hwList = this.Computer.Hardware
                            .Where(x => x.HardwareType == HardwareType.Memory).ToList();

                        foreach (var hw in hwList)
                        {
                            Console.WriteLine(Command.OutputPrefix + "Is enabled "
                                + hw.HardwareType.ToString().PadLeft(PADDING) + ", " + hw.Name);
                        }

                        if (hwList.Count == 0)
                        {
                            Console.WriteLine(Command.OutputPrefix + "Is not Enabled "
                                + HardwareType.Memory.ToString());
                        }
                    }
                    else if (MotherBoard.Where(x => x.Equals(this.values[1].ToLower())).Count() > 0)
                    {
                        type = Hardware.MotherBoard;

                        var hwList = this.Computer.Hardware
                            .Where(x => x.HardwareType == HardwareType.Motherboard).ToList();

                        foreach (var hw in hwList)
                        {
                            Console.WriteLine(Command.OutputPrefix + "Is enabled "
                                + hw.HardwareType.ToString().PadLeft(PADDING) + ", " + hw.Name);
                        }

                        if (hwList.Count == 0)
                        {
                            Console.WriteLine(Command.OutputPrefix + "Is not Enabled "
                                + HardwareType.Motherboard.ToString());
                        }
                    }
                    else if (Network.Where(x => x.Equals(this.values[1].ToLower())).Count() > 0)
                    {
                        type = Hardware.Network;

                        var hwList = this.Computer.Hardware
                            .Where(x => x.HardwareType == HardwareType.Network).ToList();

                        foreach (var hw in hwList)
                        {
                            Console.WriteLine(Command.OutputPrefix + "Is enabled "
                                + hw.HardwareType.ToString().PadLeft(PADDING) + ", " + hw.Name);
                        }

                        if (hwList.Count == 0)
                        {
                            Console.WriteLine(Command.OutputPrefix + "Is not Enabled "
                                + HardwareType.Network.ToString());
                        }
                    }
                    else if (PSU.Where(x => x.Equals(this.values[1].ToLower())).Count() > 0)
                    {
                        type = Hardware.PSU;

                        var hwList = this.Computer.Hardware
                            .Where(x => x.HardwareType == HardwareType.Psu).ToList();

                        foreach (var hw in hwList)
                        {
                            Console.WriteLine(Command.OutputPrefix + "Is enabled "
                                + hw.HardwareType.ToString().PadLeft(PADDING) + ", " + hw.Name);
                        }

                        if (hwList.Count == 0)
                        {
                            Console.WriteLine(Command.OutputPrefix + "Is not Enabled "
                                + HardwareType.Psu.ToString());
                        }
                    }
                    else if (Storage.Where(x => x.Equals(this.values[1].ToLower())).Count() > 0)
                    {
                        type = Hardware.Storage;

                        var hwList = this.Computer.Hardware
                            .Where(x => x.HardwareType == HardwareType.Storage).ToList();

                        foreach (var hw in hwList)
                        {
                            Console.WriteLine(Command.OutputPrefix + "Is enabled "
                                + hw.HardwareType.ToString().PadLeft(10) + ", " + hw.Name);
                        }

                        if (hwList.Count == 0)
                        {
                            Console.WriteLine(Command.OutputPrefix + "Is not Enabled "
                                + HardwareType.Storage.ToString());
                        }
                    }
                    else
                    {
                        throw new Exception();
                    }

                    if (this.values.Length > 2)
                    {
                        if (this.values[2].ToLower().Equals(On))
                        {
                            this.Setting.OnOff(true, type);

                            Console.WriteLine(Command.OutputPrefix + "Is on the hardware (Please restart)");
                        }
                        else if (this.values[2].ToLower().Equals(Off))
                        {
                            this.Setting.OnOff(false, type);

                            Console.WriteLine(Command.OutputPrefix + "Is off the hardware (Please restart)");
                        }
                        else
                        {
                            throw new Exception();
                        }

                        this.Setting.Save(Path.Combine(Environment.CurrentDirectory, "data.json"));
                    }
                }
                else if (this.values[0].ToLower().Equals(Info))
                {
                    if (this.values[1].ToLower().Equals(All))
                    {
                        var hwList = this.Computer.Hardware.ToList();

                        foreach (var hw in hwList)
                        {
                            Console.WriteLine(Command.OutputPrefix + "Is enabled "
                                + hw.HardwareType.ToString().PadLeft(PADDING) + ", " + hw.Name);

                            foreach (var sensor in hw.Sensors)
                            {
                                Console.WriteLine((Command.EmptyPrefix + Command.EmptyPrefix
                                    + sensor.Name + "(" + sensor.SensorType.ToString()
                                    + ") ").PadRight(PADDING * 4, '-')
                                    + (" " + (sensor.Value.ToString() ?? "") + SensorUnit(sensor.SensorType)).PadLeft(PADDING * 2, '-'));
                            }

                            Console.WriteLine();
                        }
                    }
                    else if (Battery.Where(x => x.Equals(this.values[1].ToLower())).Count() > 0)
                    {
                        var hwList = this.Computer.Hardware
                            .Where(x => x.HardwareType == HardwareType.Battery).ToList();

                        foreach (var hw in hwList)
                        {
                            Console.WriteLine(Command.OutputPrefix + "Is enabled "
                                + hw.HardwareType.ToString().PadLeft(PADDING) + ", " + hw.Name);

                            foreach (var sensor in hw.Sensors)
                            {
                                Console.WriteLine((Command.EmptyPrefix + Command.EmptyPrefix
                                    + sensor.Name + "(" + sensor.SensorType.ToString()
                                    + ") ").PadRight(PADDING * 4, '-')
                                    + (" " + (sensor.Value.ToString() ?? "") + SensorUnit(sensor.SensorType)).PadLeft(PADDING * 2, '-'));
                            }

                            Console.WriteLine();
                        }

                        if (hwList.Count == 0)
                        {
                            Console.WriteLine(Command.OutputPrefix + "Is not Enabled "
                                + HardwareType.Battery.ToString());
                        }
                    }
                    else if (Controller.Where(x => x.Equals(this.values[1].ToLower())).Count() > 0)
                    {
                        var hwList = this.Computer.Hardware
                            .Where(x => x.HardwareType == HardwareType.EmbeddedController
                            || x.HardwareType == HardwareType.Cooler || x.HardwareType == HardwareType.SuperIO)
                            .ToList();

                        foreach (var hw in hwList)
                        {
                            Console.WriteLine(Command.OutputPrefix + "Is enabled "
                                + hw.HardwareType.ToString().PadLeft(PADDING) + ", " + hw.Name);

                            foreach (var sensor in hw.Sensors)
                            {
                                Console.WriteLine((Command.EmptyPrefix + Command.EmptyPrefix
                                    + sensor.Name + "(" + sensor.SensorType.ToString()
                                    + ") ").PadRight(PADDING * 4, '-')
                                    + (" " + (sensor.Value.ToString() ?? "") + SensorUnit(sensor.SensorType)).PadLeft(PADDING * 2, '-'));
                            }

                            Console.WriteLine();
                        }

                        if (hwList.Count == 0)
                        {
                            Console.WriteLine(Command.OutputPrefix + "Is not Enabled "
                                + HardwareType.EmbeddedController.ToString());
                        }
                    }
                    else if (CPU.Where(x => x.Equals(this.values[1].ToLower())).Count() > 0)
                    {
                        var hwList = this.Computer.Hardware
                            .Where(x => x.HardwareType == HardwareType.Cpu).ToList();

                        foreach (var hw in hwList)
                        {
                            Console.WriteLine(Command.OutputPrefix + "Is enabled "
                                + hw.HardwareType.ToString().PadLeft(PADDING) + ", " + hw.Name);

                            foreach (var sensor in hw.Sensors)
                            {
                                Console.WriteLine((Command.EmptyPrefix + Command.EmptyPrefix
                                    + sensor.Name + "(" + sensor.SensorType.ToString()
                                    + ") ").PadRight(PADDING * 4, '-')
                                    + (" " + (sensor.Value.ToString() ?? "") + SensorUnit(sensor.SensorType)).PadLeft(PADDING * 2, '-'));
                            }

                            Console.WriteLine();
                        }

                        if (hwList.Count == 0)
                        {
                            Console.WriteLine(Command.OutputPrefix + "Is not Enabled "
                                + HardwareType.Cpu.ToString());
                        }
                    }
                    else if (GPU.Where(x => x.Equals(this.values[1].ToLower())).Count() > 0)
                    {
                        var hwList = this.Computer.Hardware.Where(x => x.HardwareType
                            == HardwareType.GpuNvidia || x.HardwareType == HardwareType.GpuAmd
                            || x.HardwareType == HardwareType.GpuIntel).ToList();

                        foreach (var hw in hwList)
                        {
                            Console.WriteLine(Command.OutputPrefix + "Is enabled "
                                + hw.HardwareType.ToString().PadLeft(PADDING) + ", " + hw.Name);

                            foreach (var sensor in hw.Sensors)
                            {
                                Console.WriteLine((Command.EmptyPrefix + Command.EmptyPrefix
                                    + sensor.Name + "(" + sensor.SensorType.ToString()
                                    + ") ").PadRight(PADDING * 4, '-')
                                    + (" " + (sensor.Value.ToString() ?? "") + SensorUnit(sensor.SensorType)).PadLeft(PADDING * 2, '-'));
                            }

                            Console.WriteLine();
                        }

                        if (hwList.Count == 0)
                        {
                            Console.WriteLine(Command.OutputPrefix + "Is not Enabled "
                                + "GPU");
                        }
                    }
                    else if (Memory.Where(x => x.Equals(this.values[1].ToLower())).Count() > 0)
                    {
                        var hwList = this.Computer.Hardware
                            .Where(x => x.HardwareType == HardwareType.Memory).ToList();

                        foreach (var hw in hwList)
                        {
                            Console.WriteLine(Command.OutputPrefix + "Is enabled "
                                + hw.HardwareType.ToString().PadLeft(PADDING) + ", " + hw.Name);

                            foreach (var sensor in hw.Sensors)
                            {
                                Console.WriteLine((Command.EmptyPrefix + Command.EmptyPrefix
                                    + sensor.Name + "(" + sensor.SensorType.ToString()
                                    + ") ").PadRight(PADDING * 4, '-')
                                    + (" " + (sensor.Value.ToString() ?? "") + SensorUnit(sensor.SensorType)).PadLeft(PADDING * 2, '-'));
                            }

                            Console.WriteLine();
                        }

                        if (hwList.Count == 0)
                        {
                            Console.WriteLine(Command.OutputPrefix + "Is not Enabled "
                                + HardwareType.Memory.ToString());
                        }
                    }
                    else if (MotherBoard.Where(x => x.Equals(this.values[1].ToLower())).Count() > 0)
                    {
                        var hwList = this.Computer.Hardware
                            .Where(x => x.HardwareType == HardwareType.Motherboard).ToList();

                        foreach (var hw in hwList)
                        {
                            Console.WriteLine(Command.OutputPrefix + "Is enabled "
                                + hw.HardwareType.ToString().PadLeft(PADDING) + ", " + hw.Name);

                            foreach (var sensor in hw.Sensors)
                            {
                                Console.WriteLine((Command.EmptyPrefix + Command.EmptyPrefix
                                    + sensor.Name + "(" + sensor.SensorType.ToString()
                                    + ") ").PadRight(PADDING * 4, '-')
                                    + (" " + (sensor.Value.ToString() ?? "") + SensorUnit(sensor.SensorType)).PadLeft(PADDING * 2, '-'));
                            }

                            Console.WriteLine();
                        }

                        if (hwList.Count == 0)
                        {
                            Console.WriteLine(Command.OutputPrefix + "Is not Enabled "
                                + HardwareType.Motherboard.ToString());
                        }
                    }
                    else if (Network.Where(x => x.Equals(this.values[1].ToLower())).Count() > 0)
                    {
                        var hwList = this.Computer.Hardware
                            .Where(x => x.HardwareType == HardwareType.Network).ToList();

                        foreach (var hw in hwList)
                        {
                            Console.WriteLine(Command.OutputPrefix + "Is enabled "
                                + hw.HardwareType.ToString().PadLeft(PADDING) + ", " + hw.Name);

                            foreach (var sensor in hw.Sensors)
                            {
                                Console.WriteLine((Command.EmptyPrefix + Command.EmptyPrefix
                                    + sensor.Name + "(" + sensor.SensorType.ToString()
                                    + ") ").PadRight(PADDING * 4, '-')
                                    + (" " + (sensor.Value.ToString() ?? "") + SensorUnit(sensor.SensorType)).PadLeft(PADDING * 2, '-'));
                            }

                            Console.WriteLine();
                        }

                        if (hwList.Count == 0)
                        {
                            Console.WriteLine(Command.OutputPrefix + "Is not Enabled "
                                + HardwareType.Network.ToString());
                        }
                    }
                    else if (PSU.Where(x => x.Equals(this.values[1].ToLower())).Count() > 0)
                    {
                        var hwList = this.Computer.Hardware
                            .Where(x => x.HardwareType == HardwareType.Psu).ToList();

                        foreach (var hw in hwList)
                        {
                            Console.WriteLine(Command.OutputPrefix + "Is enabled "
                                + hw.HardwareType.ToString().PadLeft(PADDING) + ", " + hw.Name);

                            foreach (var sensor in hw.Sensors)
                            {
                                Console.WriteLine((Command.EmptyPrefix + Command.EmptyPrefix
                                    + sensor.Name + "(" + sensor.SensorType.ToString()
                                    + ") ").PadRight(PADDING * 4, '-')
                                    + (" " + (sensor.Value.ToString() ?? "") + SensorUnit(sensor.SensorType)).PadLeft(PADDING * 2, '-'));
                            }

                            Console.WriteLine();
                        }

                        if (hwList.Count == 0)
                        {
                            Console.WriteLine(Command.OutputPrefix + "Is not Enabled "
                                + HardwareType.Psu.ToString());
                        }
                    }
                    else if (Storage.Where(x => x.Equals(this.values[1].ToLower())).Count() > 0)
                    {
                        var hwList = this.Computer.Hardware
                            .Where(x => x.HardwareType == HardwareType.Storage).ToList();

                        foreach (var hw in hwList)
                        {
                            Console.WriteLine(Command.OutputPrefix + "Is enabled "
                                + hw.HardwareType.ToString().PadLeft(10) + ", " + hw.Name);

                            foreach (var sensor in hw.Sensors)
                            {
                                Console.WriteLine((Command.EmptyPrefix + Command.EmptyPrefix
                                    + sensor.Name + "(" + sensor.SensorType.ToString()
                                    + ") ").PadRight(PADDING * 4, '-')
                                    + (" " + (sensor.Value.ToString() ?? "") + SensorUnit(sensor.SensorType)).PadLeft(PADDING * 2, '-'));
                            }

                            Console.WriteLine();
                        }

                        if (hwList.Count == 0)
                        {
                            Console.WriteLine(Command.OutputPrefix + "Is not Enabled "
                                + HardwareType.Storage.ToString());
                        }
                    }
                    else
                    {
                        throw new Exception();
                    }
                }
                else if (this.values[0].ToLower().Equals(View))
                {
                    string text = this.values[1].ToLower();

                    new Thread(() => ThreadingView(text)).Start();

                    while (true)
                    {
                        if (Console.ReadKey().Key == ConsoleKey.Escape)
                        {
                            lock (this.locker)
                            {
                                this.Locker = false;
                            }

                            this.Value = "";
                            RunCommand();

                            break;
                        }
                    }
                }
                else
                {
                    throw new Exception();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(Command.OutputPrefix
                    + "This is an invalid command, if you need help, please enter \"help\"");
                Console.WriteLine(Command.OutputPrefix + ex);
            }
        }

        private object locker = new object();
        public bool Locker { get; set; }

        private async void ThreadingView(string text)
        {
            this.Locker = true;

            while (true)
            {
                lock (this.locker)
                {
                    if (!this.Locker)
                    {
                        return;
                    }
                }

                this.Computer.Reset();

                if (this.Setting.DeleteLog)
                {
                    Console.Clear();
                }

                if (text.Equals(All))
                {
                    var hwList = this.Computer.Hardware.ToList();

                    foreach (var hw in hwList)
                    {
                        Console.WriteLine(Command.OutputPrefix + "Is enabled "
                            + hw.HardwareType.ToString().PadLeft(PADDING) + ", " + hw.Name);

                        foreach (var sensor in hw.Sensors)
                        {
                            Console.WriteLine((Command.EmptyPrefix + Command.EmptyPrefix
                                + sensor.Name + "(" + sensor.SensorType.ToString()
                                + ") ").PadRight(PADDING * 4, '-')
                                + (" " + (sensor.Value.ToString() ?? "") + SensorUnit(sensor.SensorType)).PadLeft(PADDING * 2, '-'));
                        }

                        Console.WriteLine();
                    }
                }
                else if (Battery.Where(x => x.Equals(text)).Count() > 0)
                {
                    var hwList = this.Computer.Hardware
                        .Where(x => x.HardwareType == HardwareType.Battery).ToList();

                    foreach (var hw in hwList)
                    {
                        Console.WriteLine(Command.OutputPrefix + "Is enabled "
                            + hw.HardwareType.ToString().PadLeft(PADDING) + ", " + hw.Name);

                        foreach (var sensor in hw.Sensors)
                        {
                            Console.WriteLine((Command.EmptyPrefix + Command.EmptyPrefix
                                + sensor.Name + "(" + sensor.SensorType.ToString()
                                + ") ").PadRight(PADDING * 4, '-')
                                + (" " + (sensor.Value.ToString() ?? "") + SensorUnit(sensor.SensorType)).PadLeft(PADDING * 2, '-'));
                        }

                        Console.WriteLine();
                    }

                    if (hwList.Count == 0)
                    {
                        Console.WriteLine(Command.OutputPrefix + "Is not Enabled "
                            + HardwareType.Battery.ToString());
                    }
                }
                else if (Controller.Where(x => x.Equals(text)).Count() > 0)
                {
                    var hwList = this.Computer.Hardware
                        .Where(x => x.HardwareType == HardwareType.EmbeddedController
                        || x.HardwareType == HardwareType.Cooler || x.HardwareType == HardwareType.SuperIO)
                        .ToList();

                    foreach (var hw in hwList)
                    {
                        Console.WriteLine(Command.OutputPrefix + "Is enabled "
                            + hw.HardwareType.ToString().PadLeft(PADDING) + ", " + hw.Name);

                        foreach (var sensor in hw.Sensors)
                        {
                            Console.WriteLine((Command.EmptyPrefix + Command.EmptyPrefix
                                + sensor.Name + "(" + sensor.SensorType.ToString()
                                + ") ").PadRight(PADDING * 4, '-')
                                + (" " + (sensor.Value.ToString() ?? "") + SensorUnit(sensor.SensorType)).PadLeft(PADDING * 2, '-'));
                        }

                        Console.WriteLine();
                    }

                    if (hwList.Count == 0)
                    {
                        Console.WriteLine(Command.OutputPrefix + "Is not Enabled "
                            + HardwareType.EmbeddedController.ToString());
                    }
                }
                else if (CPU.Where(x => x.Equals(text)).Count() > 0)
                {
                    var hwList = this.Computer.Hardware
                        .Where(x => x.HardwareType == HardwareType.Cpu).ToList();

                    foreach (var hw in hwList)
                    {
                        Console.WriteLine(Command.OutputPrefix + "Is enabled "
                            + hw.HardwareType.ToString().PadLeft(PADDING) + ", " + hw.Name);

                        foreach (var sensor in hw.Sensors)
                        {
                            Console.WriteLine((Command.EmptyPrefix + Command.EmptyPrefix
                                + sensor.Name + "(" + sensor.SensorType.ToString()
                                + ") ").PadRight(PADDING * 4, '-')
                                + (" " + (sensor.Value.ToString() ?? "") + SensorUnit(sensor.SensorType)).PadLeft(PADDING * 2, '-'));
                        }

                        Console.WriteLine();
                    }

                    if (hwList.Count == 0)
                    {
                        Console.WriteLine(Command.OutputPrefix + "Is not Enabled "
                            + HardwareType.Cpu.ToString());
                    }
                }
                else if (GPU.Where(x => x.Equals(text)).Count() > 0)
                {
                    var hwList = this.Computer.Hardware.Where(x => x.HardwareType
                        == HardwareType.GpuNvidia || x.HardwareType == HardwareType.GpuAmd
                        || x.HardwareType == HardwareType.GpuIntel).ToList();

                    foreach (var hw in hwList)
                    {
                        Console.WriteLine(Command.OutputPrefix + "Is enabled "
                            + hw.HardwareType.ToString().PadLeft(PADDING) + ", " + hw.Name);

                        foreach (var sensor in hw.Sensors)
                        {
                            Console.WriteLine((Command.EmptyPrefix + Command.EmptyPrefix
                                + sensor.Name + "(" + sensor.SensorType.ToString()
                                + ") ").PadRight(PADDING * 4, '-')
                                + (" " + (sensor.Value.ToString() ?? "") + SensorUnit(sensor.SensorType)).PadLeft(PADDING * 2, '-'));
                        }

                        Console.WriteLine();
                    }

                    if (hwList.Count == 0)
                    {
                        Console.WriteLine(Command.OutputPrefix + "Is not Enabled "
                            + "GPU");
                    }
                }
                else if (Memory.Where(x => x.Equals(text)).Count() > 0)
                {
                    var hwList = this.Computer.Hardware
                        .Where(x => x.HardwareType == HardwareType.Memory).ToList();

                    foreach (var hw in hwList)
                    {
                        Console.WriteLine(Command.OutputPrefix + "Is enabled "
                            + hw.HardwareType.ToString().PadLeft(PADDING) + ", " + hw.Name);

                        foreach (var sensor in hw.Sensors)
                        {
                            Console.WriteLine((Command.EmptyPrefix + Command.EmptyPrefix
                                + sensor.Name + "(" + sensor.SensorType.ToString()
                                + ") ").PadRight(PADDING * 4, '-')
                                + (" " + (sensor.Value.ToString() ?? "") + SensorUnit(sensor.SensorType)).PadLeft(PADDING * 2, '-'));
                        }

                        Console.WriteLine();
                    }

                    if (hwList.Count == 0)
                    {
                        Console.WriteLine(Command.OutputPrefix + "Is not Enabled "
                            + HardwareType.Memory.ToString());
                    }
                }
                else if (MotherBoard.Where(x => x.Equals(text)).Count() > 0)
                {
                    var hwList = this.Computer.Hardware
                        .Where(x => x.HardwareType == HardwareType.Motherboard).ToList();

                    foreach (var hw in hwList)
                    {
                        Console.WriteLine(Command.OutputPrefix + "Is enabled "
                            + hw.HardwareType.ToString().PadLeft(PADDING) + ", " + hw.Name);

                        foreach (var sensor in hw.Sensors)
                        {
                            Console.WriteLine((Command.EmptyPrefix + Command.EmptyPrefix
                                + sensor.Name + "(" + sensor.SensorType.ToString()
                                + ") ").PadRight(PADDING * 4, '-')
                                + (" " + (sensor.Value.ToString() ?? "") + SensorUnit(sensor.SensorType)).PadLeft(PADDING * 2, '-'));
                        }

                        Console.WriteLine();
                    }

                    if (hwList.Count == 0)
                    {
                        Console.WriteLine(Command.OutputPrefix + "Is not Enabled "
                            + HardwareType.Motherboard.ToString());
                    }
                }
                else if (Network.Where(x => x.Equals(text)).Count() > 0)
                {
                    var hwList = this.Computer.Hardware
                        .Where(x => x.HardwareType == HardwareType.Network).ToList();

                    foreach (var hw in hwList)
                    {
                        Console.WriteLine(Command.OutputPrefix + "Is enabled "
                            + hw.HardwareType.ToString().PadLeft(PADDING) + ", " + hw.Name);

                        foreach (var sensor in hw.Sensors)
                        {
                            Console.WriteLine((Command.EmptyPrefix + Command.EmptyPrefix
                                + sensor.Name + "(" + sensor.SensorType.ToString()
                                + ") ").PadRight(PADDING * 4, '-')
                                + (" " + (sensor.Value.ToString() ?? "") + SensorUnit(sensor.SensorType)).PadLeft(PADDING * 2, '-'));
                        }

                        Console.WriteLine();
                    }

                    if (hwList.Count == 0)
                    {
                        Console.WriteLine(Command.OutputPrefix + "Is not Enabled "
                            + HardwareType.Network.ToString());
                    }
                }
                else if (PSU.Where(x => x.Equals(text)).Count() > 0)
                {
                    var hwList = this.Computer.Hardware
                        .Where(x => x.HardwareType == HardwareType.Psu).ToList();

                    foreach (var hw in hwList)
                    {
                        Console.WriteLine(Command.OutputPrefix + "Is enabled "
                            + hw.HardwareType.ToString().PadLeft(PADDING) + ", " + hw.Name);

                        foreach (var sensor in hw.Sensors)
                        {
                            Console.WriteLine((Command.EmptyPrefix + Command.EmptyPrefix
                                + sensor.Name + "(" + sensor.SensorType.ToString()
                                + ") ").PadRight(PADDING * 4, '-')
                                + (" " + (sensor.Value.ToString() ?? "") + SensorUnit(sensor.SensorType)).PadLeft(PADDING * 2, '-'));
                        }

                        Console.WriteLine();
                    }

                    if (hwList.Count == 0)
                    {
                        Console.WriteLine(Command.OutputPrefix + "Is not Enabled "
                            + HardwareType.Psu.ToString());
                    }
                }
                else if (Storage.Where(x => x.Equals(text)).Count() > 0)
                {
                    var hwList = this.Computer.Hardware
                        .Where(x => x.HardwareType == HardwareType.Storage).ToList();

                    foreach (var hw in hwList)
                    {
                        Console.WriteLine(Command.OutputPrefix + "Is enabled "
                            + hw.HardwareType.ToString().PadLeft(10) + ", " + hw.Name);

                        foreach (var sensor in hw.Sensors)
                        {
                            Console.WriteLine((Command.EmptyPrefix + Command.EmptyPrefix
                                + sensor.Name + "(" + sensor.SensorType.ToString()
                                + ") ").PadRight(PADDING * 4, '-')
                                + (" " + (sensor.Value.ToString() ?? "") + SensorUnit(sensor.SensorType)).PadLeft(PADDING * 2, '-'));
                        }

                        Console.WriteLine();
                    }

                    if (hwList.Count == 0)
                    {
                        Console.WriteLine(Command.OutputPrefix + "Is not Enabled "
                            + HardwareType.Storage.ToString());
                    }
                }
                else
                {
                    throw new Exception();
                }

                await Task.Delay(1000);
            }
        }

        private string SensorUnit(SensorType sensorType)
        {
            switch (sensorType)
            {
                case SensorType.Voltage: return " V";
                case SensorType.Clock: return " MHz";
                case SensorType.Temperature: return " ^C";
                case SensorType.Load: return " %";
                case SensorType.Power: return " W";
                case SensorType.Data: return " GB";
                case SensorType.SmallData: return " MB";
                case SensorType.Throughput: return " bps";
                default: return "";
            }
        }

        private string Help = "help";
        private string Enabled = "enabled";
        private string All = "all";
        private string On = "on";
        private string Off = "off";
        private string Info = "info";
        private string View = "view";

        private string[] Battery = { "battery", "btr" };
        private string[] Controller = { "controller", "controll", "ctrl", "ctr" };
        private string[] CPU = { "cpu" };
        private string[] GPU = { "gpu", "graphic", "graphiccard" };
        private string[] Memory = { "mem", "memory", "ram" };
        private string[] MotherBoard = { "motherboard", "board", "mainboard", "brd" };
        private string[] Network = { "network", "ethernet", "wifi", "nw", "lan", "wan", "internet", "net" };
        private string[] PSU = { "psu" };
        private string[] Storage = { "storage", "store", "disk", "drive", "hdd", "ssd", "hard", "harddisk" };
    }

    public enum Hardware
    {
        None = 0,
        Battery = 1,
        Controller = 2,
        CPU = 4,
        GPU = 8,
        Memory = 16,
        MotherBoard = 32,
        Network = 64,
        PSU = 128,
        Storage = 256,
        All = 511,
    }
}
