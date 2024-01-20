using MeOS.Commands;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cosmos.System.FileSystem.ISO9660;
using System.Runtime.InteropServices;
using System.Diagnostics.SymbolStore;
using MeOS.Core;
using MeOS.Core.Other;
using MeOS.Graphics;

namespace MeOS.MeScript
{

    public class RunInterpreter : Command {
        public RunInterpreter(String name, String desc) : base(name, desc) { }
        public override string execute(string[] args) {
            if (File.Exists(Kernel.path + args[0])) {
                Interpreter i = new Interpreter();
                i.Run(Kernel.path + args[0]);
            }
            return "";
        }
    }

    internal class Interpreter {

        readonly VariableHandler v = new VariableHandler();
        public static string temp = "";
        private int currentLine = 0;
        private string[] allLines;
        string a;
        public int sl = 0;
        public int st = 0;
        public int el = 0;
        public int et = 0;

        readonly string[] ends = {
            "endif"
        };

        public void Run(string filePath) {
            try {
                allLines = File.ReadAllLines(filePath);

                foreach (string line in allLines) {
                    ProcessLine(line,filePath);
                    currentLine++;
                }
            } catch (Exception ex) {
                CLI.WriteLine($"An error occurred: {ex.Message}", CLI.foreground, CLI.background);
            }
        }

        public void ProcessLine(string line, string filename = "") {
            string[] lines = line.Split(' ');
            if (GetEnd(lines[0])) return;
            switch (lines[0]) {
                case "get":
                    v.GetVariable(lines[1]);
                    break;
                case "print":
                    temp = filename;
                    Print(lines);
                    break;
                case "set":
                    v.SetVariable(lines[1], lines[2]);
                    break;
                case "putc":
                    Putc(lines);
                    break;
                case "input":
                    GetInput(lines[1],lines[2]);
                    break;
                case "if":
                    IfStatement ifS = new IfStatement();
                    ifS.Process(lines, currentLine, allLines);
                    break;
                case "confstore":
                    if (lines[1] != null && lines[2] != null)
                        StoreConf.Store(StoreConf.confFile,lines[1], lines[2]);
                    else break;
                    break;
                case "getline":
                    a = lines[1];
                    if (a == null || a == "") { a = "a"; } else { a = lines[1]; }
                    v.SetVariable(a, lines[int.Parse(lines[2])]);
                    break;
                case "confget":
                    if (int.Parse(lines[1]) != -1) {
                        v.SetVariable(lines[2], StoreConf.Get(StoreConf.confFile, int.Parse(lines[1])));
                    } else break;
                    break;
                case "getfilename":
                    a = lines[1];
                    if (a == null || a == "") { a = "a"; } else { a = lines[1]; }
                    v.SetVariable(a, filename);
                    break;
                case "getcurrentline":
                    a = lines[1];
                    if (a == null || a == "") { a = "a"; } else { a = lines[1]; }
                    v.SetVariable(a, currentLine.ToString());
                    break;
                case "savetemp":
                    StoreConf.StoreTemp(filename, temp);
                    break;
                case "gettemp":
                    v.SetVariable("temp", StoreConf.GetTemp());
                    break;
                case "drawlineh":
                    sl = int.Parse(lines[2]);
                    st = int.Parse(lines[3]);
                    el = int.Parse(lines[4]);
                    CLI.DrawLineH(CLI.StringToConsoleColor(lines[1]), sl, st, el);
                    break;
                case "drawlinev":
                    sl = int.Parse(lines[2]);
                    st = int.Parse(lines[3]);
                    et = int.Parse(lines[4]);
                    CLI.DrawLineV(CLI.StringToConsoleColor(lines[1]), sl, st, et);
                    break;
                case "drawpoint":
                    sl = int.Parse(lines[1]);
                    st = int.Parse(lines[2]);
                    CLI.DrawPoint(sl, st, CLI.StringToConsoleColor(lines[3]));
                    break;
                case "dialog":
                    sl = int.Parse(lines[1]);
                    st = int.Parse(lines[2]);
                    CLI.DrawDialog(sl, st, lines[3], CLI.StringToConsoleColor(lines[4]), CLI.StringToConsoleColor(lines[5]), lines[6]);
                    break;
                default:
                    temp = filename;
                    CLI.WriteLine(ExecuteCommand(line), CLI.foreground, CLI.background);
                    break;
            }
        }


        public string ExecuteCommand(string command) {
            return Kernel.cm.processInput(command);
        }

        public void Print(string[] lines) {
            if (lines[1] == "[get]") {
                temp = v.GetVariable(lines[2]);
                CLI.WriteLine(temp, CLI.foreground, CLI.background);
            } else if (lines[1] == "[args]") {
                List<string> a = CommandManager.args;
                foreach (string b in a)
                    Console.Write(b + " ");

            } else if (lines[1] == "[random]") {
                CLI.WriteLine(SystemDictionary.PickRandomWord(), CLI.foreground, CLI.background);
            } else {
                for (int i = 1; i < lines.Length; i++) {
                    Console.Write(lines[i] + " ");
                }
            }
        }

        public void Putc(string[] lines) {
            char temp = lines[1][0];
            (int left, int top) = Console.GetCursorPosition();

            try {
                Console.SetCursorPosition(int.Parse(lines[2]), int.Parse(lines[3]));
                Console.Write(temp);
                Console.SetCursorPosition(left, top + 1);
            } catch(Exception e) {
                Console.SetCursorPosition(left, top + 1);
                CLI.WriteLine(e.Message, CLI.foreground, CLI.background);
                return;
            }
        }

        public bool GetEnd(string input) {
            for(int i = 0; i < ends.Length; i++) {
                if (ends[i] == input)
                    return true;
            }
            return false;
        }

        public void GetInput(string variable = "a", string prompt = "") {
            if(prompt == null || prompt == "") { } else {
                CLI.WriteLine(prompt, CLI.foreground, CLI.background);
            }

            v.SetUserInput(Console.ReadLine(), variable);
        }
    }
}
