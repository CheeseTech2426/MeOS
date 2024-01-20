using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using MeOS.MeScript;
using MeOS.Programs.Games;
using MeOS.Core;
using MeOS.Core.Other;
using MeOS.Graphics;

namespace MeOS.Commands
{
    public class CommandManager
    {

        public static List<Command> commands;
        public List<Command> recoveryUtils;
        public List<Command> gsheetCommand;
        public static List<string> args;


        public CommandManager()
        {
            // Add the commands
            commands = new List<Command>(22);
            commands.Add(new Clock("time", "Tells the current time and date"));
            commands.Add(new Calculator("calc", "Simple calculator"));
            commands.Add(new CreateDir("mkdir", "Creates a directory"));
            commands.Add(new CreateFile("create", "Creates a file"));
            commands.Add(new DeleteFile("del", "Deletes a file"));
            commands.Add(new ReadFile("read", "Reads a file"));
            commands.Add(new WriteFile("write", "Writes to file"));
            commands.Add(new CopyFile("copy", "Copies a file"));
            commands.Add(new RemoveDir("rmdir", "Deletes a directory"));
            commands.Add(new ListFiles("ls", "Lists the files"));
            commands.Add(new ChangeDirectory("cd", "Changes the directory"));
            commands.Add(new ChangeDirectoryBack("cd..", "Goes up a directory"));
            commands.Add(new ClearCommand("clear", "Clears the screen"));
            commands.Add(new MIVCommand("miv", "Launches the MIV Text Editor"));
            commands.Add(new RunInterpreter("mescript", "MeScript interpreter"));
            commands.Add(new RunSearchCommand("find", "Finds a given file"));
            commands.Add(new LaunchGame("game", "Launches games"));
            commands.Add(new TaskCommand("task", "Task Scheduling"));
            commands.Add(new HelpCommand("help", "Displays commands"));
            commands.Add(new Settings("color", "Changes the color of UI objects"));
            commands.Add(new Shutdown("shutdown", "Turns off the computer"));
            commands.Add(new Reboot("reboot", "Restarts the computer"));
            commands.Add(new AboutSystem("about", "About the system"));
            commands.Add(new GraphicsTest("metgltest", "MeTGL Graphics Test"));
            commands.Add(new CMDVideoPlayer("vid", "8-Bit Video player capable of playing .VID files"));
        }


        public string processInput(string input)
        {
            string[] split = input.Split(' ');
            string label = split[0];
            args = new List<string>();

            int ctr = 0;
            foreach (string s in split)
            {
                if (ctr != 0)
                    args.Add(s);
                ++ctr;
            }

            foreach (Command cmd in commands)
            {
                if (cmd.name == label)
                {
                    return cmd.execute(args.ToArray());
                }
            }

            Console.ForegroundColor = ConsoleColor.Red;
            Console.Beep();
            CLI.WriteLine($"Invalid commands: {label}!", CLI.foreground, CLI.background);
            Console.ForegroundColor = ConsoleColor.White;
            return "";
        }





        public string processRecoveryInput(string input)
        {
            string[] spilt = input.Split(' ');
            string label = spilt[0];
            List<string> args = new List<string>();

            int ctr = 0;
            foreach (string s in spilt)
            {
                if (ctr != 0)
                    args.Add(s);
                ++ctr;
            }

            foreach (Command cmd in recoveryUtils)
            {
                if (cmd.name == label)
                    return cmd.execute(args.ToArray());
            }
            Console.ForegroundColor = ConsoleColor.Red;
            CLI.WriteLine($"Invalid command: {label}!", CLI.foreground, CLI.background);
            Console.ForegroundColor = ConsoleColor.White;
            return "";
        }
        public string processGSheet(string input)
        {
            string[] spilt = input.Split(' ');
            string label = spilt[0];
            List<string> args = new List<string>();

            int ctr = 0;
            foreach (string s in spilt)
            {
                if (ctr != 0)
                    args.Add(s);
                ++ctr;
            }

            foreach (Command cmd in gsheetCommand)
            {
                if (cmd.name == label)
                    return cmd.execute(args.ToArray());
            }
            Console.ForegroundColor = ConsoleColor.Red;
            CLI.WriteLine($"Invalid command: {label}!", CLI.foreground, CLI.background);
            Console.ForegroundColor = ConsoleColor.White;
            return "";
        }
    }
}

