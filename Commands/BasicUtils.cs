using Cosmos.System.Graphics;
using MeOS.Core;
using MeOS.Graphics;
using MeOS.MIV;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Diagnostics.SymbolStore;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeOS.Commands
{


    public class BasicUtils {
        public static string CleanPath(string path) {
            if (string.IsNullOrEmpty(path)) {
                return path;
            }

            char separator = '\\'; // Directory separator character

            // Replace consecutive backslashes with a single backslash
            string cleanedPath = "";
            bool lastCharWasBackslash = false;

            foreach (char c in path) {
                if (c == separator) {
                    if (!lastCharWasBackslash) {
                        cleanedPath += c;
                        lastCharWasBackslash = true;
                    }
                } else {
                    cleanedPath += c;
                    lastCharWasBackslash = false;
                }
            }

            return cleanedPath;
        }

        public static string NormalizePath(string path) {
            // Replace multiple backslashes with a single backslash
            path = CleanPath(path);

            // Ensure the path ends with a directory separator
            if (!path.EndsWith("\\")) {
                path += "\\";
            }

            return path;
        }
    }
    
    public class Clock : Command {
        public Clock(String name, String desc) : base(name, desc) { }
        public override string execute(string[] args) {
       
            CLI.DrawDialog(20, 5, "Clock", ConsoleColor.Blue, ConsoleColor.White, DateTime.Now.ToString());
            return "";
        }
    }

    public class Calculator : Command {
        public Calculator(String name, String desc) : base(name,desc) { }
        public override string execute(string[] args) {
            float r = 0f;
            string o = args[1];
            float n1 = Convert.ToUInt32(args[0]);
            float n2 = Convert.ToUInt32(args[2]);

            switch (o) {
                case "+":
                    r = n1 + n2;
                    break;
                case "-":
                    r = n1 - n2;
                    break;
                case "*":
                    r = n1 * n2;
                    break;
                case "/":
                    if (n2 != 0f || n1 != 0) {
                        r = n1 / n2;
                        break;
                    }
                    break;
            }
            return r.ToString();
        }
    }

    public class CreateFile : Command {
        public CreateFile(String name, String desc) : base(name, desc) { }
        public override string execute(string[] args) {
            string r = "Argument 0 cannot be null!";
            if (args[0] == null) return r;
            try {
                System.IO.File.Create(Kernel.path + @"\" + args[0]);
                r = "Successfully created file " + Kernel.path + @"\" + args[0];
            } catch (Exception e){
                r = e.Message;
            }
            return r;
        }
    }

    public class DeleteFile : Command {
        public DeleteFile(String name, String desc) : base(name, desc) { }
        public override string execute(string[] args) {
            string r = "Argument 0 cannot be null!";
            if (args[0] == null) return r;
            try {
                System.IO.File.Delete(Kernel.path + @"\" + args[0]);
                r = "Successfully deleted file " + Kernel.path + @"\" + args[0];
            } catch (Exception e) {
                r = e.Message;
            }
            return r;
        }
    }

    public class CreateDir : Command {
        public CreateDir(String name, String desc) : base(name, desc) { }
        public override string execute(string[] args) {
            string r = "Argument 0 cannot be null!";
            if (args[0] == null) return r;
            try {
                System.IO.Directory.CreateDirectory(Kernel.path + @"\" + args[0] + @"\");
                r = "Successfully created directory " + Kernel.path + @"\" + args[0] + @"\";
            } catch (Exception e) {
                r = e.Message;
            }
            return r;
        }
    }


    public class RemoveDir : Command {
        public RemoveDir(String name, String desc) : base(name, desc) { }
        public override string execute(string[] args) {
            string r = "Argument 0 cannot be null!";
            if (args[0] == null) return r;
            try {
                System.IO.Directory.Delete(Kernel.path + @"\" + args[0] + @"\", true);
                r = "Successfully deleted directory " + Kernel.path + @"\" + args[0] + @"\";
            } catch (Exception e) {
                r = e.Message;
            }
            return r;
        }
    }

    public class CopyFile : Command {
        public CopyFile(String name, String desc) : base(name, desc) { }
        public override string execute(string[] args) {
            try {
                File.Copy(Path.Combine(Kernel.path + args[0]), args[1]);
            } catch(Exception e) {
                CLI.DrawDialog(20,6, "Error", ConsoleColor.Blue, ConsoleColor.White, "Fatal error occured: " + e.Message);
                return "";
            }

            return "";

        }
    }

    public class ReadFile : Command {
        public ReadFile(String name, String desc) : base(name, desc) { }
        public override string execute(string[] args) {
            string r = "Argument 0 cannot be null!";
            if (args[0] == null) return r;
            try {
                string[] d = System.IO.File.ReadAllLines(Kernel.path + args[0]);
                foreach(string s in d) {
                    CLI.WriteLine(s, CLI.foreground, CLI.background);
                }

                r = "End of file.";
            } catch (Exception e) {
                r = e.Message;
            }
            return r;
        }
    }

    public class WriteFile : Command {
        public WriteFile(String name, String desc) : base(name, desc) { }
        public override string execute(string[] args) {
            string r = "Argument 0 cannot be null!";
            if (args[0] == null || args[1] == null) return r;
            try {
                File.WriteAllText(Kernel.path + args[0], args[1]);

                r = "Task completed successfully.";
            } catch (Exception e) {
                r = e.Message;
            }
            return r;
        }
    }

    public class ListFiles : Command {
        public ListFiles(String name, String desc) : base(name, desc) { }
        public override string execute(string[] args) {

            string[] files;
            string[] dirs;

            if (args[0] == null || args[0] == "" || args[0] == " ") {
                args[0] = " ";
                CLI.WriteLine("Files in " + Kernel.path, CLI.foreground, CLI.background);
                files = Directory.GetFiles(Kernel.path);
                dirs= Directory.GetDirectories(Kernel.path);
            } else if (args[0] != null && Directory.Exists(args[0])) {
                CLI.WriteLine("Files in " + args[0], CLI.foreground, CLI.background);
                files = Directory.GetFiles(args[0]);
                dirs = Directory.GetDirectories(args[0]);
            } else {
                args[0] = " ";
                CLI.WriteLine("Files in " + Kernel.path, CLI.foreground, CLI.background);
                files = Directory.GetFiles(Kernel.path);
                dirs = Directory.GetDirectories(Kernel.path);
            }


            foreach (string d in dirs) {
                CLI.WriteLine("  [" + d + "]  ", CLI.foreground, CLI.background);
            }

            foreach (string f in files) {
                CLI.WriteLine("  " + f + "  ", CLI.foreground, CLI.background);
            }
            return "";
        }
    }

    public class ChangeDirectory : Command {
        public ChangeDirectory(string name, String desc) : base(name, desc) { }

        public override string execute(string[] args) {
            if (args.Length == 0 || string.IsNullOrEmpty(args[0])) {
                return "Not a valid directory!";
            }

            
            string newPath = Path.Combine(Kernel.path, args[0]);

            if (!newPath.EndsWith('\\')) newPath += @"\";

            if (!Directory.Exists(newPath)) {
                return "Directory doesn't exist!";
            }

            Kernel.path = BasicUtils.CleanPath(newPath);

            return "";
        }
    }

    public class ChangeDirectoryBack : Command {
        public ChangeDirectoryBack(string name, String desc) : base(name, desc) { }

        public override string execute(string[] args) {
            string path = Kernel.path;

            // Ensure the path ends with a single directory separator
            

            // Count the occurrences of the directory separator
            int separatorCount = path.Count(c => c == '\\');

            if (!path.EndsWith("\\")) {
                path += "\\";
            }

            // If there are two or more occurrences, or the path is only "\\",
            // set the path to the root directory
            if (separatorCount >= 2 || path == "\\" || path.EndsWith("\\\\")) {
                Kernel.path = "0:\\";
                return "";
            }

            // Find the last index of the directory separator
            int lastBackslashIndex = path.LastIndexOf('\\');

            // Ensure we are not at the root directory
            if (lastBackslashIndex >= 0) {
                // Extract the parent directory
                path = path.Substring(0, lastBackslashIndex + 1);

                // Update the kernel path
                Kernel.path = path;

                return "";
            }

            // If we are at the root directory or in an invalid state, display an error message
            return "Error: Cannot navigate up. Already at the root directory or in an invalid state.";
        }
    }





    public class ClearCommand : Command {
        public ClearCommand(String name, String desc) : base(name, desc) { }
        public override string execute(string[] args) {
            CLI.Clear();
            return "";
        }
    }

    public class MIVCommand : Command {
        public MIVCommand(String name, String desc) : base(name, desc) { }
        public override string execute(string[] args) {
            Console.Clear();
            MIV.MIV.StartMIV();
            return "";
        }
    }

    public class GraphicsTest : Command {
        public GraphicsTest(String name, String desc) : base(name, desc) { }

        public override string execute(string[] args) {
            bool r = true;
            Random random = new Random(); // Move the creation of Random outside the loop
            while (r) {
                for (int y = 0; y < CLI.Height; y++) {
                    for (int x = 0; x < CLI.Width; x++) {
                        CLI.DrawPoint(x, y, PickColor(random)); // Pass the Random object to the PickColor function
                    }
                }

                if (Console.KeyAvailable) {
                    ConsoleKeyInfo key = Console.ReadKey(true);
                    if (key.Key == ConsoleKey.Escape) {
                        CLI.DrawDialog(15, 5, "MeTGL", ConsoleColor.Blue, ConsoleColor.White, "Stopped.");
                        return ""; // Exit the loop or method
                    }
                }
            }

            return "";

        }

        private static ConsoleColor PickColor(Random random) {
            ConsoleColor[] availableColors = {
                ConsoleColor.Black, ConsoleColor.DarkBlue, ConsoleColor.DarkGreen,
                ConsoleColor.DarkCyan, ConsoleColor.DarkRed, ConsoleColor.DarkMagenta,
                ConsoleColor.DarkYellow, ConsoleColor.Gray, ConsoleColor.DarkGray,
                ConsoleColor.Blue, ConsoleColor.Green, ConsoleColor.Cyan,
                ConsoleColor.Red, ConsoleColor.Magenta, ConsoleColor.Yellow, ConsoleColor.White
            };

            return availableColors[random.Next(availableColors.Length)];
        }

    }
}
