using MeOS.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeOS.Commands {
    internal class AboutSystem : Command{
        public AboutSystem(String name, String desc) : base(name, desc) { }
        public override string execute(string[] args) {
            CLI.Clear();
            CLI.MoveCursor(0, 3);
            int off = 2;
            CLI.Write("+=========================+\r\n| __  __       ___  ____  |\r\n||  \\/  | ___ / _ \\/ ___| |\r\n|| |\\/| |/ _ \\ | | \\___ \\ |\r\n|| |  | |  __/ |_| |___) ||\r\n||_|  |_|\\___|\\___/|____/ |\r\n+=========================+", ConsoleColor.White, ConsoleColor.Blue);
            int w = "+=========================+".Length;
            int w2 = "+=============================+".Length;
            CLI.Write(w + 3, 3, ConsoleColor.Black, ConsoleColor.White, CLI.version);
            CLI.Write(w + 3, 4, ConsoleColor.Black, ConsoleColor.White,  "CPU: "+Cosmos.Core.CPU.GetCPUVendorName());
            CLI.Write(w + 3, 5, ConsoleColor.Black, ConsoleColor.White, "CPU Clock Speed: " + ConvertClockSpeedUnits(Cosmos.Core.CPU.GetCPUCycleSpeed()));
            CLI.Write(w + 3, 6, ConsoleColor.Black, ConsoleColor.White, "Amount RAM: " + ConvertStorageUnit(Cosmos.Core.CPU.GetAmountOfRAM()));
            CLI.Write(w + 3, 7, ConsoleColor.Black, ConsoleColor.White, "Drive Space: " + ConvertStorageUnit(Kernel.vfs.GetTotalSize(@"0:\")));
            CLI.Write(w + 3, 8, ConsoleColor.Black, ConsoleColor.White, "Drive Free Space: " + ConvertStorageUnit(Kernel.vfs.GetTotalFreeSpace(@"0:\")));

            CLI.Write(0, 12 + off, ConsoleColor.Red, ConsoleColor.White, "+=============================+");
            CLI.Write(0, 13 + off, ConsoleColor.Yellow, ConsoleColor.Magenta, "| __  __     _____ ____ _     |");
            CLI.Write(0, 14 + off, ConsoleColor.Green, ConsoleColor.DarkBlue, "||  \\/  | __|_   _/ ___| |    |");
            CLI.Write(0, 15 + off, ConsoleColor.DarkCyan, ConsoleColor.Cyan, "|| |\\/| |/ _ \\| || |  _| |    |");
            CLI.Write(0, 16 + off, ConsoleColor.DarkBlue, ConsoleColor.Green, "|| |  | |  __/| || |_| | |___ |");
            CLI.Write(0, 17 + off, ConsoleColor.Magenta, ConsoleColor.Yellow, "||_|  |_|\\___||_| \\____|_____||");
            CLI.Write(0, 18 + off, ConsoleColor.White, ConsoleColor.Red, "+=============================+");

            CLI.Write(w2 + 3, 7 + 6 + off, ConsoleColor.Black, ConsoleColor.White, "MeOS Text Graphics Library");
            CLI.Write(w2 + 3, 7 + 7 + off, ConsoleColor.Black, ConsoleColor.White, "Version 1.0");
            CLI.Write(w2 + 3, 7 + 8 + off, ConsoleColor.Black, ConsoleColor.White, "You can test the graphics by typing ");
            CLI.Write(w2 + 3, 7 + 9 + off, ConsoleColor.Black, ConsoleColor.White, "'metgltest'.");
            return "";
        }

        public string ConvertStorageUnit(uint val) {
            if (val < 1)
                return val.ToString() + " B";
            else if (val < 1000)
                return (val * 1000).ToString() + " B";
            else if (val < 1000000)
                return (val / 1000).ToString() + " KB";
            else if (val < 1000000000)
                return (val / 1000000).ToString() + " MB";
            else if (val < 1000000000000)
                return (val / 1000000000).ToString() + " GB";
            else
                return (val / 1000000000000).ToString() + " TB";

        }

        public string ConvertStorageUnit(long val) {
            if (val < 1)
                return val.ToString() + " B";
            else if (val < 1000)
                return (val * 1000).ToString() + " B";
            else if (val < 1000000)
                return (val / 1000).ToString() + " KB";
            else if (val < 1000000000)
                return (val / 1000000).ToString() + " MB";
            else if (val < 1000000000000)
                return (val / 1000000000).ToString() + " GB";
            else
                return (val / 1000000000000).ToString() + " TB";

        }

        public string ConvertClockSpeedUnits(long val) {
            if (val < 1000)
                return val.ToString() + " Hz";
            else if (val < 1000000)
                return (val / 1000.0).ToString("0.###") + " KHz";
            else if (val < 1000000000)
                return (val / 1000000.0).ToString("0.###") + " MHz";
            else
                return (val / 1000000000.0).ToString("0.###") + " GHz";
        }
    }
}
