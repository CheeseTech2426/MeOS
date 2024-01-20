using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace MeOS.Programs.Games {
    internal class RockPaperScissors {
        public string[] options = {
            "rock",
            "paper",
            "scissors"
        };

        public static void Run() {
            bool win = false;
            string choice = "rock";
            Console.WriteLine("Rock, Paper, Scissors? (r/p/s) ");
            string i = Console.ReadLine().ToLower();
            Console.WriteLine(ai(i).ToUpper());
            switch (ai(i)) {
                case "rock":
                    if(choice == "paper") {
                        win = true;
                        break;
                    } else {
                        win = false;
                        break;
                    }
                case "paper":
                    if(choice == "scissors") {
                        win = true;
                        break;
                    } else {
                        win = false;
                        break;
                    }
                case "scissors":
                    if (choice == "rock") {
                        win = true;
                        break;
                    } else {
                        win = false;
                        break;
                    }

            }

            if (win) {
                Console.WriteLine("You won!");
            } else {
                Console.WriteLine("You lost!");
            }
        }

        static string ai(string i) {
            Random random = new Random();
            int change = random.Next(2);

            if (i == null) return null;

            if (i == "rock" || i == "r")
                return (change == 0) ? "paper" : "rock";

            if (i == "paper" || i == "p")
                return (change == 0) ? "scissors" : "paper";

            if (i == "scissors" || i == "s")
                return (change == 0) ? "rock" : "paper";

            return null;
        }

    }
}
