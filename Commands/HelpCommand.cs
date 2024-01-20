using MeOS.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Cosmos.HAL.BlockDevice.ATA_PIO;

namespace MeOS.Commands {
    internal class HelpCommand : Command{
        public HelpCommand(String name, String desc) : base(name, desc) { }
        public string offset = "               ";
        public override string execute(string[] args) {
            int pageSize = 18; // Adjust the number of commands per page as needed
            int totalPages = (CommandManager.commands.Count - 1) / pageSize + 1;
            int currentPage = 1;

            while (true) {
                CLI.Clear();
                CLI.DrawLineH(ConsoleColor.Blue, 1, 2, CLI.Width - 1);
                CLI.Write(1, 2, ConsoleColor.Blue, ConsoleColor.White, $"CMD {offset} DESC - Page {currentPage}/{totalPages}");

                int startIndex = (currentPage - 1) * pageSize;
                int endIndex = Math.Min(currentPage * pageSize, CommandManager.commands.Count - 1);

                for (int row = 4; row <= 4 + endIndex - startIndex; row++) {
                    CLI.DrawLineH(ConsoleColor.Blue, 1, row, CLI.Width - 1);
                }

                for (int cmd = startIndex; cmd <= endIndex; cmd++) {
                    int top = Console.GetCursorPosition().Top;

                    string cn = CommandManager.commands[cmd].name;
                    string cd = CommandManager.commands[cmd].desc;

                    if (cn == null || cd == null) continue;

                    CLI.Write(1, 4 + cmd - startIndex, ConsoleColor.Blue, ConsoleColor.White, " " + cn);
                    CLI.Write(18, 4 + cmd - startIndex, ConsoleColor.Blue, ConsoleColor.White, cd);
                }

                CLI.MoveCursor(0, CLI.Height);
                Console.WriteLine("Press any key to navigate pages (Q to quit)");

                ConsoleKeyInfo key = Console.ReadKey(true);

                if (key.Key == ConsoleKey.Q) {
                    break; // Quit if 'Q' is pressed
                } else if (key.Key == ConsoleKey.RightArrow && currentPage < totalPages) {
                    currentPage++; // Move to the next page if available
                } else if (key.Key == ConsoleKey.LeftArrow && currentPage > 1) {
                    currentPage--; // Move to the previous page if available
                }
            }


            return "";
        }

        private int PrintCommands(List<int> tops, int start = 0) {
            int cmd = start;
            for (cmd = 0; cmd < CommandManager.commands.Count - 1; cmd++) {
                int top = Console.GetCursorPosition().Top;

                string cn = CommandManager.commands[cmd].name;

                string cd = CommandManager.commands[cmd].desc;

                if (cn == null || cd == null) continue;

                CLI.Write(1, tops[cmd], ConsoleColor.Blue, ConsoleColor.White, " " + cn);

                CLI.Write(18, tops[cmd], ConsoleColor.Blue, ConsoleColor.White, cd);

            }
            return cmd;
        }
    }
}
