using MeOS.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeOS.User
{
    internal class UserManager
    {
        public string UserName { get; set; }
        public static Dictionary<string, string> users = new Dictionary<string, string>();
        public static void CreateUser(string username, string p) {
            if (users.ContainsKey(username)) { return; }
            users.Add(username, p);
            
            string[] a = {
                $"{username}|",
                p
            };
            File.AppendAllLines(@"0:\MeOS\users.me", a);
        }

        public static bool CheckIfUserExists(string user) {
            if (users.ContainsKey(user)) {
                return true;
            } else {
                return false;
            }
        }
            
        public static void LoginPage() {
            CLI.Clear();
            CLI.DrawDialogWithTextField(30, 7, "Login", ConsoleColor.Blue, ConsoleColor.White, "Username: ");
            string user = CLI.GetUserInput();
            if (CheckIfUserExists(user)) {
                CLI.WriteLine($"Welcome {user}!", CLI.foreground, CLI.background);
                Kernel.currentlyLoggedUser = user;
                return;
            } else {
                CLI.DrawDialog(30, 5, "Login", ConsoleColor.Blue, ConsoleColor.White, "Invalid username!");
                LoginPage();
            }
        }

    }
}
