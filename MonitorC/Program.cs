using LibreHardwareMonitor.Hardware;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MonitorC
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string resourcePath = Path.Combine(Environment.CurrentDirectory, "data.json");
            Setting? setting = null;

            do
            {
                if (!File.Exists(resourcePath))
                {
                    Console.WriteLine(Command.OutputPrefix + "Resources not found...");
                    Console.WriteLine(Command.OutputPrefix + "Making resources file with [" + resourcePath + "]...");

                    string json1 = JsonSerializer.Serialize(new Setting());

                    using Stream stream = File.Create(resourcePath);
                    using StreamWriter writer = new StreamWriter(stream);
                    writer.Write(json1);

                    Console.WriteLine(Command.OutputPrefix + "Writing default resources...");
                }

                using StreamReader reader = new StreamReader(resourcePath);
                string json2 = reader.ReadToEnd();
                reader.Close();

                try
                {
                    setting = JsonSerializer.Deserialize<Setting>(json2) ?? null;

                    break;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(Command.OutputPrefix + "Resources file is corrupted...");
                    Console.Write(Command.OutputPrefix + "Do you want to use auto reconstructor? [y/n] ");

                    string text = Console.ReadLine() ?? "";

                    if (text.ToLower().StartsWith('y'))
                    {
                        Console.WriteLine(Command.OutputPrefix + "Resources reconstruct...");
                        File.Delete(resourcePath);

                        continue;
                    }
                    else
                    {
                        Console.WriteLine(Command.OutputPrefix + "End Process by " + ex);

                        return;
                    }
                }
            }
            while (true);

            Computer computer = new Computer() 
            {
                IsBatteryEnabled = setting!.IsBatteryEnabled,
                IsControllerEnabled = setting!.IsControllerEnabled,
                IsCpuEnabled = setting!.IsCpuEnabled,
                IsGpuEnabled = setting!.IsGpuEnabled,
                IsMemoryEnabled = setting!.IsMemoryEnabled, 
                IsMotherboardEnabled = setting!.IsMotherboardEnabled,
                IsNetworkEnabled = setting!.IsNetworkEnabled,
                IsPsuEnabled = setting!.IsPsuEnabled,
                IsStorageEnabled = setting!.IsStorageEnabled,
            };
            computer.Open();

            Command command = new Command(computer, setting!);

            while (true)
            {
                Console.Write(Command.InputPrefix);
                command.Value = Console.ReadLine() ?? "";
                command.RunCommand();
            }
        }
    }
}
