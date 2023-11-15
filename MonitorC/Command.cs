using LibreHardwareMonitor.Hardware;

namespace MonitorC
{
    public class Command
    {
        public const string InputPrefix = "<< ";
        public const string OutputPrefix = ">> ";

        private string[] values = new string[0];
        public string Value { set => this.values = value.Split(' '); }
        public Computer Computer { get; private set; }

        public Command(Computer computer)
        {
            this.Computer = computer;

            Console.WriteLine(Command.OutputPrefix + "Starting MonitorC, Welcome " + Environment.UserName + "!");
            Console.WriteLine(Command.OutputPrefix + "If you need help, please enter \"help\"");
        }

        public void RunCommand()
        {
            if (this.values.Length == 0)
                return;

            try
            {
                if (this.values[0].ToLower().Equals(Help))
                {

                }
                else if (this.values[0].ToLower().Equals(Enabled))
                {
                    if (this.values[1].ToLower().Equals(All))
                    {
                        var hwList = this.Computer.Hardware.ToList();

                        foreach (var hw in hwList)
                        {
                            Console.WriteLine(Command.OutputPrefix + "Enabled " 
                                + hw.HardwareType.ToString() + ", " + hw.Name);
                        }
                    }
                    else if (Battery.Where(x => x.Equals(this.values[1].ToLower())).Count() > 0)
                    {
                        var hwList = this.Computer.Hardware.Where(x => x.HardwareType == HardwareType.Battery).ToList();

                        foreach (var hw in hwList)
                        {
                            Console.WriteLine(Command.OutputPrefix + "Enabled "
                                + hw.HardwareType.ToString() + ", " + hw.Name);
                        }

                        if (hwList.Count == 0) 
                        {
                            Console.WriteLine(Command.OutputPrefix + "Is not Enabled " + HardwareType.Battery.ToString());
                        }
                    }
                    else if (Controller.Where(x => x.Equals(this.values[1].ToLower())).Count() > 0)
                    {
                        var hwList = this.Computer.Hardware.Where(x => x.HardwareType == HardwareType.EmbeddedController).ToList();

                        foreach (var hw in hwList)
                        {
                            Console.WriteLine(Command.OutputPrefix + "Enabled "
                                + hw.HardwareType.ToString() + ", " + hw.Name);
                        }

                        if (hwList.Count == 0)
                        {
                            Console.WriteLine(Command.OutputPrefix + "Is not Enabled " + HardwareType.EmbeddedController.ToString());
                        }
                    }
                    else if (CPU.Where(x => x.Equals(this.values[1].ToLower())).Count() > 0)
                    {
                        var hwList = this.Computer.Hardware.Where(x => x.HardwareType == HardwareType.Cpu).ToList();

                        foreach (var hw in hwList)
                        {
                            Console.WriteLine(Command.OutputPrefix + "Enabled "
                                + hw.HardwareType.ToString() + ", " + hw.Name);
                        }

                        if (hwList.Count == 0)
                        {
                            Console.WriteLine(Command.OutputPrefix + "Is not Enabled " + HardwareType.Cpu.ToString());
                        }
                    }
                    else if (GPU.Where(x => x.Equals(this.values[1].ToLower())).Count() > 0)
                    {
                        var hwList = this.Computer.Hardware.Where(x => x.HardwareType 
                            == HardwareType.GpuNvidia || x.HardwareType == HardwareType.GpuAmd || x.HardwareType ==HardwareType.GpuIntel).ToList();

                        foreach (var hw in hwList)
                        {
                            Console.WriteLine(Command.OutputPrefix + "Enabled "
                                + hw.HardwareType.ToString() + ", " + hw.Name);
                        }

                        if (hwList.Count == 0)
                        {
                            Console.WriteLine(Command.OutputPrefix + "Is not Enabled " 
                                + "GPU");
                        }
                    }
                    else if (Memory.Where(x => x.Equals(this.values[1].ToLower())).Count() > 0)
                    {
                        var hwList = this.Computer.Hardware.Where(x => x.HardwareType == HardwareType.Memory).ToList();

                        foreach (var hw in hwList)
                        {
                            Console.WriteLine(Command.OutputPrefix + "Enabled "
                                + hw.HardwareType.ToString() + ", " + hw.Name);
                        }

                        if (hwList.Count == 0)
                        {
                            Console.WriteLine(Command.OutputPrefix + "Is not Enabled " + HardwareType.Memory.ToString());
                        }
                    }
                    else if (MotherBoard.Where(x => x.Equals(this.values[1].ToLower())).Count() > 0)
                    {
                        var hwList = this.Computer.Hardware.Where(x => x.HardwareType == HardwareType.Motherboard).ToList();

                        foreach (var hw in hwList)
                        {
                            Console.WriteLine(Command.OutputPrefix + "Enabled "
                                + hw.HardwareType.ToString() + ", " + hw.Name);
                        }

                        if (hwList.Count == 0)
                        {
                            Console.WriteLine(Command.OutputPrefix + "Is not Enabled " + HardwareType.Motherboard.ToString());
                        }
                    }
                    else if (Network.Where(x => x.Equals(this.values[1].ToLower())).Count() > 0)
                    {
                        var hwList = this.Computer.Hardware.Where(x => x.HardwareType == HardwareType.Network).ToList();

                        foreach (var hw in hwList)
                        {
                            Console.WriteLine(Command.OutputPrefix + "Enabled "
                                + hw.HardwareType.ToString() + ", " + hw.Name);
                        }

                        if (hwList.Count == 0)
                        {
                            Console.WriteLine(Command.OutputPrefix + "Is not Enabled " + HardwareType.Network.ToString());
                        }
                    }
                    else if (PSU.Where(x => x.Equals(this.values[1].ToLower())).Count() > 0)
                    {
                        var hwList = this.Computer.Hardware.Where(x => x.HardwareType == HardwareType.Psu).ToList();

                        foreach (var hw in hwList)
                        {
                            Console.WriteLine(Command.OutputPrefix + "Enabled "
                                + hw.HardwareType.ToString() + ", " + hw.Name);
                        }

                        if (hwList.Count == 0)
                        {
                            Console.WriteLine(Command.OutputPrefix + "Is not Enabled " + HardwareType.Psu.ToString());
                        }
                    }
                    else if (Storage.Where(x => x.Equals(this.values[1].ToLower())).Count() > 0)
                    {
                        var hwList = this.Computer.Hardware.Where(x => x.HardwareType == HardwareType.Storage).ToList();

                        foreach (var hw in hwList)
                        {
                            Console.WriteLine(Command.OutputPrefix + "Enabled "
                                + hw.HardwareType.ToString() + ", " + hw.Name);
                        }

                        if (hwList.Count == 0)
                        {
                            Console.WriteLine(Command.OutputPrefix + "Is not Enabled " + HardwareType.Storage.ToString());
                        }
                    }
                    else
                    {
                        throw new Exception();
                    }
                }
                else
                {
                    throw new Exception();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(Command.OutputPrefix + "This is an invalid command, if you need help, please enter \"help\"");
                Console.WriteLine(Command.OutputPrefix + ex);
            }
        }

        private string Help = "help";
        private string Enabled = "enabled";
        private string All = "all";

        private string[] Battery = { "battery", "btr" };
        private string[] Controller = { "controller", "controll", "ctrl", "ctr" };
        private string[] CPU = { "cpu" };
        private string[] GPU = { "gpu" };
        private string[] Memory = { "mem", "memory", "ram" };
        private string[] MotherBoard = { "motherboard", "board", "mainboard", "brd" };
        private string[] Network = { "network", "ethernet", "wifi", "nw", "lan", "wan", "internet", "net" };
        private string[] PSU = { "psu" };
        private string[] Storage = { "storage", "store", "disk", "drive", "hdd", "ssd", "hard", "harddisk" };
    }
}