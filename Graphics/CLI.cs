using Cosmos.System.Network.IPv4.TCP;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MeOS.Graphics {
    internal class CLI {

        /* VALUES*/
        public static readonly int Width = Console.WindowWidth;
        public static readonly int Height = Console.WindowHeight;

        /* COLORS */
        public static ConsoleColor foreground;
        public static ConsoleColor background;
        public static ConsoleColor barColor;
        public static ConsoleColor barClockColor;
        public static ConsoleColor barPathColor;
        public static ConsoleColor barMeOSColor;
        public static ConsoleColor commandPromptColor;
        public static ConsoleColor windowTopColor;
        public static ConsoleColor userColor;
        public static ConsoleColor pcNameColor;
        public static ConsoleColor pathColor;

        /* READ ONLY VALUES*/
        public static readonly string version = "MeOS v1.0 Aurora";
        public static readonly string colorConfigureFile = @"0:\MeOS\color.conf";

        /* PRIVATE VALUES */
        private static string textBoxResult;

        public static void Write(string text, ConsoleColor fg, ConsoleColor bg) {
            Console.ForegroundColor = fg;
            Console.BackgroundColor = bg;
            Console.Write(text);
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Black;
        }

        public static void WriteLine(string text, ConsoleColor fg, ConsoleColor bg) {
            Console.ForegroundColor = fg;
            Console.BackgroundColor = bg;
            Console.WriteLine(text);
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Black;
        }


        public static void init() {
            
            if (!File.Exists(colorConfigureFile)) File.Create(colorConfigureFile);
            if(File.ReadAllText(colorConfigureFile) == "") {
                
                WriteLineToFile(colorConfigureFile, "white");
                WriteLineToFile(colorConfigureFile, "black");
                WriteLineToFile(colorConfigureFile, "blue");
                WriteLineToFile(colorConfigureFile, "yellow");
                WriteLineToFile(colorConfigureFile, "magenta");
                WriteLineToFile(colorConfigureFile, "gray");
                WriteLineToFile(colorConfigureFile, "green");
                WriteLineToFile(colorConfigureFile, "darkblue");
                WriteLineToFile(colorConfigureFile, "darkblue");

                WriteLineToFile(colorConfigureFile, "yellow");
                WriteLineToFile(colorConfigureFile, "blue");
                WriteLineToFile(colorConfigureFile, "gray");


                foreground = StringToConsoleColor(ReadLine(colorConfigureFile, 1));
                background = StringToConsoleColor(ReadLine(colorConfigureFile, 2));
                barColor = StringToConsoleColor(ReadLine(colorConfigureFile, 3));
                barClockColor = StringToConsoleColor(ReadLine(colorConfigureFile, 4));
                barPathColor = StringToConsoleColor(ReadLine(colorConfigureFile, 5));
                barMeOSColor = StringToConsoleColor(ReadLine(colorConfigureFile, 6));
                commandPromptColor = StringToConsoleColor(ReadLine(colorConfigureFile, 7));
                windowTopColor = StringToConsoleColor(ReadLine(colorConfigureFile, 8));
                userColor = StringToConsoleColor(ReadLine(colorConfigureFile, 9));
                pcNameColor = StringToConsoleColor(ReadLine(colorConfigureFile, 10));
                pathColor = StringToConsoleColor(ReadLine(colorConfigureFile, 11));
            } else {
                UpdateColors();
            }

            textBoxResult = "";
        }

        public static void Wait(int time) { for (int i = 0; i < time; i++) { } }
        
        public static void EraseLine(int line) {
            Console.SetCursorPosition(0, line);
            for (int i = 0; i < CLI.Width; i++) ;
                Console.Write(" ");
            Console.SetCursorPosition(0, line);
        }

        public static void PrintCommandLine(string path) {

            PrintBar();
            CLI.Write("$", pcNameColor, background);
            CLI.Write(Kernel.currentlyLoggedUser, userColor, background);
            CLI.Write("@meos-pc", pcNameColor, background);
            CLI.Write(@"> ", ConsoleColor.White, background);
            CLI.Write(Kernel.path + "~ ", pathColor, background);
        }

        public static void DrawLineH(ConsoleColor bg, int startLeft, int startTop, int stopLeft) {
            (int l, int t) = Console.GetCursorPosition();
            Console.SetCursorPosition(startLeft, startTop);
            for (int i = startLeft; i < stopLeft; i++) {
                Write(" ", ConsoleColor.White, bg);
            }
            Console.SetCursorPosition(l, t);
            Console.BackgroundColor = ConsoleColor.Black;
        }
        public static void DrawLineV(ConsoleColor bg, int startLeft, int startTop, int stopTop) {
            (int l, int t) = Console.GetCursorPosition();
            Console.SetCursorPosition(startLeft, startTop);
            for (int i = startTop; i < stopTop; i++) {
                Write(" \n", ConsoleColor.White, bg);
            }
            Reset(l, t);
        }

        public static void Reset(int l, int t) {
            Console.SetCursorPosition(l, t);
            Console.BackgroundColor = background;
            Console.ForegroundColor = foreground;
        }

        public static void UpdateColors() {
            Kernel.showBar = true;
            CheckColors();
            foreground = StringToConsoleColor(ReadLine(colorConfigureFile, 1));
            background = StringToConsoleColor(ReadLine(colorConfigureFile, 2));
            barColor = StringToConsoleColor(ReadLine(colorConfigureFile, 3));
            barClockColor = StringToConsoleColor(ReadLine(colorConfigureFile, 4));
            barPathColor = StringToConsoleColor(ReadLine(colorConfigureFile, 5));
            barMeOSColor = StringToConsoleColor(ReadLine(colorConfigureFile, 6));
            commandPromptColor = StringToConsoleColor(ReadLine(colorConfigureFile, 7));
            windowTopColor = StringToConsoleColor(ReadLine(colorConfigureFile, 8));
        }


        static void CheckColors() {
            for (int i = 0; i < 11; i++) {
                if (ReadLine(colorConfigureFile, i) == null) {
                    File.Delete(colorConfigureFile); 
                    init();
                }
            } 
            
        }

        public static void Write(int x, int y, ConsoleColor bg, ConsoleColor fg, string c = " ") {
            (int l, int t) = Console.GetCursorPosition();
            Console.SetCursorPosition(x, y);
            Console.BackgroundColor = bg;
            Console.ForegroundColor = fg;
            Write(c, fg, bg);
            Reset(l, t);
        }

        public static void WriteLine(int x, int y, ConsoleColor bg, ConsoleColor fg, string c = " ") {
            (int l, int t) = Console.GetCursorPosition();
            Console.SetCursorPosition(x, y);
            Console.BackgroundColor = bg;
            Console.ForegroundColor = fg;
            Write(c + "\n", fg, bg);
            Reset(l, t);
        }

        public static string GetUserInput() {
            return textBoxResult;
        }

        public static void DrawPoint(int x, int y, ConsoleColor color) {
            (int l, int t) = Console.GetCursorPosition();
            Console.SetCursorPosition(x, y);
            Console.BackgroundColor = color;
            Console.Write(" ");
            Reset(l, t);
        }

        public static void PrintBar() {
            if (Kernel.showBar) {
                CLI.DrawLineH(barColor, 0, 0, Console.WindowWidth);
                CLI.Write(0, 0, barColor, barClockColor, DateTime.Now.ToString("dd-MM-yyyy HH:mm"));
                CLI.Write((Width / 2) - Kernel.path.Length, 0, barColor, barPathColor, Kernel.path);
                CLI.Write(Width - version.Length, 0, barColor, barMeOSColor, version);
            }
        }

        public static void Clear() {
            SetBackgroundColor(background);
            Console.Clear();
            PrintBar();
        }

        public static void Clear(ConsoleColor bg) {
            SetBackgroundColor(bg);
            Console.Clear();
            PrintBar();
            SetBackgroundColor(background);
        }

        public static void DrawDialog(int pw, int ph, string title, ConsoleColor bg, ConsoleColor fg, string message, string buttonText = "  ENTER  ") {
            int w = Width;
            int h = Height;
            bool dialog = true;
            int cx = w / 2;
            int cy = h / 2;
            int x1 = Math.Max(0, cx - pw);
            int x2 = Math.Min(CLI.Width - 1, cx + pw);
            int y1 = Math.Max(0, cy - ph);
            int y2 = Math.Min(CLI.Height - 1, cy + ph);
            int dashes = Math.Max(1, pw - title.Length + 1);
            int m = pw % 0;

            DrawLineH(windowTopColor, x1, y1, x2);
            for (int i = y1 + 1; i < y2; i++) {
                DrawLineH(bg, x1, i, x2);
            }

            // window top
            if (m == 0) {
                Write(x1, y1, windowTopColor, fg, new string('-', dashes) + "[" + title + "]" + new string('-', dashes));
            } else {
                Write(x1, y1, windowTopColor, fg, new string('-', dashes) + "[" + title + "]" + new string('-', dashes + 1));
            }
            int messageX = cx - message.Length / 2;
            int messageY = cy;

            // Check if the message fits within the dialog, adjust if necessary
            if (messageX < x1) {
                messageX = x1;
            } else if (messageX + message.Length > x2) {
                messageX = x2 - message.Length;
            }

            // message
            Write(messageX, messageY, bg, fg, message);

            DrawLineH(bg, x1 + (cx - 2), y2 - 1, x2 - (cx - 2));

            int okButtonX = cx - buttonText.Length / 2;
            int okButtonY = Math.Min(CLI.Height - 1, y2 - 2);

            Write(okButtonX, okButtonY, ConsoleColor.White, ConsoleColor.Black, buttonText);

            while (dialog) {
                ConsoleKeyInfo key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.Enter) {
                    for (int i = y1; i < y2; i++) {
                        DrawLineH(ConsoleColor.Black, x1, i, x2);
                    }
                    dialog = false;
                }
            }

        }


        public static void DrawDialogWithTextField(int pw, int ph, string title, ConsoleColor bg, ConsoleColor fg, string message, string buttonText = "  ENTER  ", string initialInput = "") {
            (int l, int t) = StoreCursor();
            int w = Width;
            int h = Height;
            bool dialog = true;
            int cx = w / 2;
            int cy = h / 2;
            int x1 = Math.Max(0, cx - pw);
            int x2 = Math.Min(CLI.Width - 1, cx + pw);
            int y1 = Math.Max(0, cy - ph);
            int y2 = Math.Min(CLI.Height - 1, cy + ph);
            int dashes = Math.Max(1, pw - title.Length + 1);
            int m = pw % 2;

            DrawLineH(windowTopColor, x1, y1, x2);
            for (int i = y1 + 1; i < y2; i++) {
                DrawLineH(bg, x1, i, x2);
            }

            // window top
            if (m == 0) {
                Write(x1, y1, windowTopColor, fg, new string('-', dashes) + "[" + title + "]" + new string('-', dashes));
            } else {
                Write(x1, y1, windowTopColor, fg, new string('-', dashes) + "[" + title + "]" + new string('-', dashes + 1));
            }
            int messageX = cx - message.Length / 2;
            int messageY = cy - 3;

            // Check if the message fits within the dialog, adjust if necessary
            if (messageX < x1) {
                messageX = x1;
            } else if (messageX + message.Length > x2) {
                messageX = x2 - message.Length;
            }

            // message
            Write(messageX, messageY, bg, fg, message);

            DrawLineH(bg, x1 + (cx - 2), y2 - 1, x2 - (cx - 2));

            int okButtonX = cx - buttonText.Length / 2;
            int okButtonY = Math.Min(CLI.Height - 1, y2 - 2);

            Write(okButtonX, okButtonY, ConsoleColor.White, ConsoleColor.Black, buttonText);

            // Input field rendering
            int inputFieldWidth = pw - 4; // Width excluding borders
            int inputFieldX = cx - inputFieldWidth / 2; // Center the text box horizontally
            int inputFieldY = messageY + 2; // Shift the text box below the message

            // Render the input field
            DrawTextBoxBorder(inputFieldX, inputFieldY - 1, inputFieldX + inputFieldWidth, inputFieldY + 1, ConsoleColor.White);
            Write(inputFieldX + 1, inputFieldY-1, ConsoleColor.White, fg, new string(' ', inputFieldWidth)); // Clear previous content

            // Set cursor position inside the input field initially
            Console.SetCursorPosition(inputFieldX + initialInput.Length / 2 + 1, inputFieldY);

            while (dialog) {
                UpdateTextBox(inputFieldX, inputFieldY - 1, inputFieldX + inputFieldWidth, inputFieldY + 1, ConsoleColor.White);
                ConsoleKeyInfo key = Console.ReadKey(true);

                if (char.IsLetterOrDigit(key.KeyChar) || char.IsPunctuation(key.KeyChar) || char.IsSymbol(key.KeyChar) || char.IsWhiteSpace(key.KeyChar)) {
                    // Append the typed character to the input field
                    initialInput += key.KeyChar;

                    // Render the updated input field
                    Write(inputFieldX + 1, inputFieldY, ConsoleColor.White, fg, initialInput);
                } else if (key.Key == ConsoleKey.Backspace && initialInput.Length > 0) {
                    // Remove the last character when backspace is pressed
                    initialInput = initialInput.Substring(0, initialInput.Length - 1);

                    // Render the updated input field
                    Write(inputFieldX + 1, inputFieldY, ConsoleColor.White, fg, initialInput);
                } else if (key.Key == ConsoleKey.Enter) {
                    // Clear the dialog and return the input field value when Enter is pressed
                    for (int i = y1; i < y2; i++) {
                        DrawLineH(ConsoleColor.Black, x1, i, x2);
                    }
                    dialog = false;
                }
                
            }
            MoveCursor(l, t);
            textBoxResult = initialInput;
        }

        private static void DrawTextBoxBorder(int x1, int y1, int x2, int y2, ConsoleColor color) {
            for(int i = y1; i < y2; i++)
                DrawLineH(color, x1, i, x2);
           
            
            for(int i = x2; i > x1 - 25; i--)
                Write(x1, y1, ConsoleColor.White, ConsoleColor.White, "-");

            for (int i = x2; i > x1 - 25; i--)
                Write(x1, y2, ConsoleColor.White, ConsoleColor.White, "-");

            Write(x1, y1, ConsoleColor.White, ConsoleColor.White, "+");
            Write(x2, y1, ConsoleColor.White, ConsoleColor.White, "+");

            Write(x1, y2, ConsoleColor.White, ConsoleColor.White, "+");
            Write(x2, y2, ConsoleColor.White, ConsoleColor.White, "+");

            Write(x1, y1 + 1, ConsoleColor.White, ConsoleColor.White, "|");
            Write(x2, y1 + 1, ConsoleColor.White, ConsoleColor.White, "|");
            
        }

        static void UpdateTextBox(int x1, int y1, int x2, int y2, ConsoleColor color) {
            DrawLineH(color, x1, y2, x2);
            Write(x1, y2, ConsoleColor.White, ConsoleColor.White, RepeatString("-", (x2 - x1) - 25));
            Write(x1, y2, ConsoleColor.White, ConsoleColor.White, "+");
            Write(x2, y2, ConsoleColor.White, ConsoleColor.White, "+");
            DrawLineH(color, x1, y1, x2);
            Write(x1, y1, ConsoleColor.White, ConsoleColor.White, RepeatString("-", (x2 - x1) - 25));
            Write(x1, y1, ConsoleColor.White, ConsoleColor.White, "+");
            Write(x2, y1, ConsoleColor.White, ConsoleColor.White, "+");
        }

        static string RepeatString(string str, int count) {
            return new string(str[0], count) + str.Substring(1).PadLeft(str.Length * count, str[0]);
        }

        private static void DrawBorder(int x1, int y1, int x2, int y2, ConsoleColor color) {
            DrawLineH(color, x1, y1, x2);
            DrawLineH(color, x1, y2, x2);

            for (int i = y1 + 1; i < y2; i++) {
                Write(x1, i, color, color, "|");
                Write(x2, i, color, color, "|");
            }
        }

        public static void MoveCursor(int left, int top) {
            Console.SetCursorPosition(left, top);
        }

        public static (int, int) StoreCursor() {
            return Console.GetCursorPosition();
        }

        public static void SetBackgroundColor(ConsoleColor c) {
            Console.BackgroundColor = c;
        }
        public static void SetForegroundColor(ConsoleColor c) {
            Console.ForegroundColor = c;
        }

        static void WriteLineToFile(string filePath, string line) {
            try {
                // Append the line to the file
                using (StreamWriter writer = new StreamWriter(filePath, true)) {
                    writer.WriteLine(line);
                }
            } catch (Exception ex) {
                CLI.WriteLine($"Error writing to the file: {ex.Message}", CLI.foreground, CLI.background);
            }
        }

        public static string[] ReadLinesFromFile(string filePath) {
            try {
                string[] lines = File.ReadAllLines(filePath);
                return lines;
            } catch (Exception ex) {
                DrawDialog(16, 6, "Error", ConsoleColor.Blue, ConsoleColor.White, $"An Error Occured:\n{ex.Message}");
                return null;
            }
        }

        static string ReadLine (string filePath, int lineNumber) {
            try {
                // Read all lines from the file
                string[] lines = File.ReadAllLines(filePath);

                // Check if the specified line number is valid
                if (lineNumber > 0 && lineNumber <= lines.Length) {
                    // Return the specified line
                    return lines[lineNumber - 1];
                } else {
                    CLI.WriteLine($"Invalid line number: {lineNumber}", CLI.foreground, CLI.background);
                    return null;
                }
            } catch (Exception ex) {
                CLI.WriteLine($"Error reading from the file: {ex.Message}", CLI.foreground, CLI.background);
                return null;
            }
        }

        public static void ChangeLineInFile(string filePath, int lineNumber, string newLine) {
            try {
                // Read all lines from the file
                string[] lines = File.ReadAllLines(filePath);

                // Update the specified line
                if (lineNumber > 0 && lineNumber <= lines.Length) {
                    lines[lineNumber - 1] = newLine;

                    // Write the modified lines back to the file
                    File.WriteAllLines(filePath, lines);
                } else {
                    CLI.WriteLine($"Invalid line number: {lineNumber}", CLI.foreground, CLI.background);
                }
            } catch (Exception ex) {
                CLI.WriteLine($"Error changing line in the file: {ex.Message}", CLI.foreground, CLI.background);
            }
        }

        public static ConsoleColor StringToConsoleColor(string color) {
            string c = color.ToLower();
            if (c == "black") { return ConsoleColor.Black; }
            if (c == "blue") { return ConsoleColor.Blue; }
            if (c == "cyan") { return ConsoleColor.Cyan; }
            if (c == "darkblue") { return ConsoleColor.DarkBlue; }
            if (c == "darkcyan") { return ConsoleColor.DarkCyan; }
            if (c == "darkgray") { return ConsoleColor.DarkGray; }
            if (c == "darkgreen") { return ConsoleColor.DarkGreen; }
            if (c == "darkmagenta") { return ConsoleColor.DarkMagenta; }
            if (c == "darkred") { return ConsoleColor.DarkRed; }
            if (c == "darkyellow") { return ConsoleColor.DarkYellow; }
            if (c == "gray") { return ConsoleColor.Gray; }
            if (c == "green") { return ConsoleColor.Green; }
            if (c == "magenta") { return ConsoleColor.Magenta; }
            if (c == "red") { return ConsoleColor.Red; }
            if (c == "white") { return ConsoleColor.White; }
            if (c == "yellow") { return ConsoleColor.Yellow; }
            return ConsoleColor.Black; // Default color
        }


        public static bool IsValidColor(string color) {
            string c = color.ToLower();
            switch (c) {
                case "black":
                case "blue":
                case "cyan":
                case "darkblue":
                case "darkcyan":
                case "darkgray":
                case "darkgreen":
                case "darkmagenta":
                case "darkred":
                case "darkyellow":
                case "gray":
                case "green":
                case "magenta":
                case "red":
                case "white":
                case "yellow":
                    return true;
                default:
                    return false; 
            }
        }


    }
}
