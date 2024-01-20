using MeOS.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeOS.Core {
    internal class Panic {
        public static void PANIC() {
            CLI.Clear();
            for (int i = 0; i < CLI.Height; i++) {
                CLI.DrawLineH(ConsoleColor.Blue, 0, i, CLI.Width);
            }
            CLI.DrawDialog(CLI.Width, CLI.Height, "MeOS", ConsoleColor.Blue, ConsoleColor.White, "An fatal error occured and MeOS cannot continue. Press Enter to reboot!", "   REBOOT   ");
            Cosmos.System.Power.Reboot();
        }
    }
}
