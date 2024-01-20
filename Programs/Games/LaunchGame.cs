using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MeOS.Commands;
using MeOS.Graphics;

namespace MeOS.Programs.Games {
    internal class LaunchGame : Command {
        public LaunchGame(String name, String desc) : base(name,desc) { }
        public override string execute(string[] args) {
            switch (args[0]) {
                case "tictactoe":
                    TicTacToe.Run();
                    break;
                case "guessthenumber":
                    GuessTheNumber.Run();
                    break;
                case "rockpaperscissors":
                    RockPaperScissors.Run();
                    break;
                default:
                    CLI.WriteLine("Games are:", CLI.foreground, CLI.background);
                    Console.Write("Tic Tac Toe (tictactoe) and Guess the number (guessthenumber)");
                    break;
            }
            return "";
        }
    }
}
