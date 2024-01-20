using MeOS.Commands;
using MeOS.Core.Other;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.Design;
using MeOS.Graphics;

namespace MeOS.Core {
    public static class SystemStartUp {

        public static void KernelInit() {

            BootScreen.Show();
            CLI.WriteLine("\nCredits:", CLI.foreground, CLI.background);
            CLI.WriteLine("MIV by Arun Davies | GitHub: bartashevich - Minor additions by CaveSponge ", CLI.foreground, CLI.background);
            CLI.WriteLine("https://github.com/bartashevich/MIV/tree/master\n------------------------", CLI.foreground, CLI.background);
            Console.Clear();
            CLI.WriteLine("\nWelcome to MeOS!", CLI.foreground, CLI.background);
            TaskScheduler ts = new TaskScheduler();
            Kernel.path = @"0:\";
        }
    }

    public static class BootScreen {
        public static void Show() {
            Console.Clear();
            int w = "| |  | |  __/ |__| |____) |".Length;
            int h = "_ |||_".Length;
            int x = (CLI.Width / 2) - w;
            int y = (CLI.Height / 2) - h;
            (int l, int t) = Console.GetCursorPosition();
            Console.SetCursorPosition(x, y);
            CLI.WriteLine(" __  __       ____   _____ \r\n|  \\/  |     / __ \\ / ____|\r\n| \\  / | ___| |  | | (___  \r\n| |\\/| |/ _ \\ |  | |\\___ \\ \r\n| |  | |  __/ |__| |____) |\r\n|_|  |_|\\___|\\____/|_____/ \r\n                           ", ConsoleColor.White, ConsoleColor.Blue);
            CLI.Wait(400000);
            Console.SetCursorPosition(l, t);
        }


    }
}
