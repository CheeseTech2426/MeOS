using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MeOS.Core;
using MeOS.Graphics;

namespace MeOS.Commands
{
    internal class TaskCommand : Command {
        public TaskCommand(String name, String desc) : base(name, desc) { }

        public override string execute(string[] args) {
            string r = "";
            TaskScheduler scheduler = new TaskScheduler();
            switch (args[0]) {
                case "schedule":
                    string name = "";
                    string task = "";
                    if (args[1] == null) return "Argument cannot be null!";
                    if (File.Exists(args[1])) {
                        if (Path.GetExtension(args[1]) == ".sch") {
                            string[] lines = File.ReadAllLines(args[1]);
                            for (int i = 0; i < lines.Length; i++) {
                                string l = lines[i];
                                string[] w = l.Split(' ');
                                name = w[0];
                                task = Arr2Str(w);
                                scheduler.Schedule(name, task);
                            }
                            lines = null;
                        } else {
                            r = "Invalid file type! .sch expected!";
                            return r;
                        }
                        scheduler.RunAllTasks();
                    }
                    break;
                case "run":
                    scheduler.RunAllTasks();
                    break;
                case "testdialog":
                    CLI.DrawDialogWithTextField(20, 5, "Test", ConsoleColor.Blue, ConsoleColor.White, "Echo");
                    Console.WriteLine(CLI.GetUserInput());
                    break; 
            }
            return r;
        }


        private string Arr2Str(string[] input) {
            // Skip the first element and join the rest of the words into a single string
            string mergedString = string.Join(" ", input.Skip(1));

            return mergedString;
        }

    }
}
