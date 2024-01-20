using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using MeOS.Commands;
using MeOS.Graphics;


namespace MeOS.User
{
    internal class PermissionManager
    {



        public static List<string> requestArgs(string app, string message = "")
        {
            CLI.WriteLine("----Permission Manager----", CLI.foreground, CLI.background);
            CLI.WriteLine(message, CLI.foreground, CLI.background);
            CLI.Write($" Can {app} use 'args'? (y/N) ", CLI.foreground, CLI.background);
            string i = Console.ReadLine();
            if (i.ToLower() == "yes" || i.ToLower() == "y")
            {
                return CommandManager.args;
            }
            else
            {
                return null;
            }
        }

        public static bool requestPermission(string className, string method, string app)
        {
            bool a = false;
            bool b = false;
            CLI.WriteLine("----Permission Manager----", CLI.foreground, CLI.background);

            switch (className)
            {
                case "cm": a = true; break;
                default: a = false; break;
            }

            switch (method)
            {
                case "processInput()":
                    b = true;
                    break;
                default: b = false; break;
            }

            Console.Write($" Can use {app} '{method}' from class '{className}'? (y/N) ");
            string c = Console.ReadLine();


            if (a && b && (c.ToLower() == "yes" || c.ToLower() == "y"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    class Permissions {
        string[] permission = {
            "System",
            "Admin",
            "User"
        };
    }
}
