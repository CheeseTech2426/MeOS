using MeOS.Core;
using MeOS.Graphics;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace MeOS.Commands {
    internal class Settings : Command {

        string[] elements = {
            "background",
            "foreground",
            "bar",
            "clock",
            "path",
            "version",
            "commandline",
            "windowTop",
        };

        string[] colors = {
            "black",
            "blue",
            "cyan",
            "darkblue",
            "darkcyan",
            "darkgray",
            "darkgreen",
            "darkmagenta",
            "darkred",
            "darkyellow",
            "gray",
            "green",
            "magenta",
            "red",
            "white",
            "yellow",
        };

        public Settings(String name, String desc) : base(name, desc) { }
        public override string execute(string[] args) {
            ShowColorSelectionUI();
            return "";
        }

        public static void ShowColorSelectionUI() {
            string offset = "    ";
            CLI.Clear();
            for (int i = 0; i < 10; i++) {
                CLI.DrawLineH(ConsoleColor.Blue, 0, 0, CLI.Width);
            }

            CLI.DrawLineH(ConsoleColor.Black, 0, 1, CLI.Width);
            CLI.WriteLine(offset+"\n\nSelect the UI element to change color:", ConsoleColor.White, ConsoleColor.Blue);
            CLI.WriteLine(offset + "1. Background (bg)", ConsoleColor.White, ConsoleColor.Blue);
            CLI.WriteLine(offset + "2. Foreground (fg)", ConsoleColor.White, ConsoleColor.Blue);
            CLI.WriteLine(offset + "3. Bar", ConsoleColor.White, ConsoleColor.Blue);
            CLI.WriteLine(offset + "4. Bar Clock", ConsoleColor.White, ConsoleColor.Blue);
            CLI.WriteLine(offset + "5. Bar Path", ConsoleColor.White, ConsoleColor.Blue);
            CLI.WriteLine(offset + "6. Bar Label", ConsoleColor.White, ConsoleColor.Blue);
            CLI.WriteLine(offset + "7. Command Line", ConsoleColor.White, ConsoleColor.Blue);
            CLI.WriteLine(offset + "8. Window Top", ConsoleColor.White, ConsoleColor.Blue);
            CLI.WriteLine(offset + "9. Username", ConsoleColor.White, ConsoleColor.Blue);
            CLI.WriteLine(offset + "10. Computer Name", ConsoleColor.White, ConsoleColor.Blue);
            CLI.WriteLine(offset + "11 Path Color", ConsoleColor.White, ConsoleColor.Blue);

            (int l, int t) = CLI.StoreCursor();
            CLI.Write("Enter the number corresponding to your choice: ", ConsoleColor.White, ConsoleColor.Blue);
            string choice = Console.ReadLine();

            if (int.TryParse(choice, out int option) && option >= 1 && option <= 8) {
                CLI.EraseLine(t);
                CLI.Write("Enter the color (e.g., red, blue, yellow): ", ConsoleColor.White, ConsoleColor.Blue);
                string color = Console.ReadLine();

                if (CLI.IsValidColor(color)) {
                    string uiElement = GetUIElementFromOption(option);
                    CLI.ChangeLineInFile(CLI.colorConfigureFile, option, color.ToLower());
                    CLI.UpdateColors();
                    CLI.Clear();
                    CLI.DrawDialog(18, 5, "MeOS", ConsoleColor.Blue, ConsoleColor.White, "Color set successfully!");
                } else {
                    CLI.Clear();

                    CLI.DrawDialog(18, 5, "MeOS", ConsoleColor.Blue, ConsoleColor.White, $"Invalid color {color}!");
                }
            } else {
                CLI.Clear();
                CLI.DrawDialog(18, 5, "MeOS", ConsoleColor.Blue, ConsoleColor.White, $"Invalid option!");
            }
        }

        private static string GetUIElementFromOption(int option) {
            switch (option) {
                case 2: return "Background";
                case 1: return "Foreground";
                case 3: return "Bar";
                case 4: return "Bar Clock";
                case 5: return "Bar Path";
                case 6: return "Bar Label";
                case 7: return "Command Line";
                case 8: return "Window Top";
                case 9: return "Username color";
                case 10: return "PC Name color";
                case 11: return "Path Color";
                default: return "Unknown";
            }
        }

    }
}
