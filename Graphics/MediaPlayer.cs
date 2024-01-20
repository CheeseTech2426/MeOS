using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MeOS.Commands;

namespace MeOS.Graphics {

    public class CMDVideoPlayer : Command {
        public CMDVideoPlayer(String n, string d) : base(n,d) { }
        public override string execute(string[] args) {
            switch (args[0]) {
                case "vid":
                    switch (args[1]) {
                        case "example":
                            MediaPlayer.ExampleVid();
                            MediaPlayer.Parse(@"0:\MeOS\example.vid", false);
                            break;
                        default:
                            MediaPlayer.Parse(args[2], false);
                            break ;
                    }
                    break;
                case "pic":
                    switch (args[1]) {
                        case "example":
                            
                            break;
                    }
                    break;
            }
            return "";
        }
    }

    internal class MediaPlayer {
        private static ConsoleColor[] ColorPalette = {
            ConsoleColor.Black, ConsoleColor.White, ConsoleColor.Gray,
            ConsoleColor.Red, ConsoleColor.Yellow, ConsoleColor.Blue,
            ConsoleColor.Green, ConsoleColor.Magenta
        };

        private static ConsoleColor MapToPalette(int colorIndex) {
            // Map the color index to the limited palette
            return ColorPalette[colorIndex % ColorPalette.Length];
        }

        private static ConsoleColor PickColor() {
            // Use the MapToPalette function to limit the colors
            Random random = new Random();
            ConsoleColor randomColor = MapToPalette(random.Next(ColorPalette.Length));

            return randomColor;
        }

        private static Dictionary<string, int> ColorNameToIndex = new Dictionary<string, int> {
            {"black", 0},
            {"white", 1},
            {"gray", 2},
            {"red", 3},
            {"yellow", 4},
            {"blue", 5},
            {"green", 6},
            {"magenta", 7}
        };

        private static ConsoleColor GetColorFromName(string colorName) {
            if (ColorNameToIndex.ContainsKey(colorName.ToLower())) {
                int colorIndex = ColorNameToIndex[colorName.ToLower()];
                return MapToPalette(colorIndex);
            } else {
                // Handle invalid color name
                return ConsoleColor.Gray; // or any default color
            }
        }

        private static string GetColorNameFromIndex(int colorIndex) {
            foreach (var entry in ColorNameToIndex) {
                if (entry.Value == colorIndex) {
                    return entry.Key;
                }
            }
            return null;
        }

        public static void ExampleVid() {
            // Strings to repeat
            string s0 = "0";
            string s1 = "1";
            string s2 = "2";
            string s3 = "3";
            string s4 = "4";
            string s5 = "5";
            string s6 = "6";
            string s7 = "7";

            // Target length
            int targetLength = 450000;

            // Output file path
            string filePath = @"0:\Media\example.vid";

            // StringBuilder for efficient string concatenation
            StringBuilder result = new StringBuilder(targetLength);

            // Loop to concatenate the strings in the desired pattern
            for (int i = 0; i < targetLength; i += 8) {
                result.Append(s0).Append(s1).Append(s2).Append(s3)
                      .Append(s4).Append(s5).Append(s6).Append(s7);
            }

            // Write the result to the file
            File.WriteAllText(filePath, result.ToString());

            // Output or further use of the result
            CLI.WriteLine("File writing complete.", CLI.foreground, CLI.background);
        }

        public static void ExamplePic() {
            // Strings to repeat
            string s0 = "0";
            string s1 = "1";
            string s2 = "2";
            string s3 = "3";
            string s4 = "4";
            string s5 = "5";
            string s6 = "6";
            string s7 = "7";

            // Target length
            int targetLength = 2000;

            // Output file path
            string filePath = @"0:\Media\example.pic";

            // StringBuilder for efficient string concatenation
            StringBuilder result = new StringBuilder(targetLength);

            // Loop to concatenate the strings in the desired pattern
            for (int i = 0; i < targetLength; i += 8) {
                result.Append(s0).Append(s2).Append(s3).Append(s4)
                      .Append(s5).Append(s6).Append(s7).Append(s1);
            }

            // Write the result to the file
            File.WriteAllText(filePath, result.ToString());

            // Output or further use of the result
            CLI.WriteLine("File writing complete.", CLI.foreground, CLI.background);
        }

        public static void Parse(string file, bool isPhoto) {
            try {
                string ex = "";
                string content = "";
                bool r = false;
                ex = Path.GetExtension(file);
                if (ex == ".pic" && isPhoto)
                    r = false;
                else if (ex == ".vid" && !isPhoto)
                    r = true;
                else
                    r = false;
                
                content = File.ReadAllText(file);

                int maxX = Math.Min(CLI.Width, content.Length);
                int maxY = Math.Min(CLI.Height, content.Length / CLI.Width);
                
                while (r) {
                    for (int y = 0; y < maxY; y++) {
                        for (int x = 0; x < maxX; x++) {
                            char colorChar = content[(y * CLI.Width + x) % content.Length];
                            CLI.DrawPoint(x, y, MapToPalette(int.Parse(colorChar.ToString())));
                        }
                    }
                    ConsoleKeyInfo key = Console.ReadKey(true);
                    if(key.Key == ConsoleKey.Escape) {
                        CLI.Clear();
                        CLI.DrawDialog(15, 5, "MediaPlayer", ConsoleColor.Blue, ConsoleColor.White, "Stopped.");
                        r = false;
                        return;
                    }
                }
            } catch (Exception ex) {
                CLI.WriteLine($"Error reading or parsing the file: {ex.Message}", CLI.foreground, CLI.background);
            }
        }


        static string RepeatString(string str, int count) {
            return new string(str[0], count) + str.Substring(1).PadLeft(str.Length * count, str[0]);
        }
    }
}
