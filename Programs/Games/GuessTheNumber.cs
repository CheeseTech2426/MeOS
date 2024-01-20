using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeOS.Programs.Games {
    internal class GuessTheNumber {

        public static void Run() {
            int n;
            int t = 15;
            Random r = new Random(Environment.TickCount);
            n = r.Next(10000);

            while (t > 0) {
                Console.Write("Guess: ");
                if (int.TryParse(Console.ReadLine(), out int g)) {
                    if (g > n) {
                        Console.WriteLine("Lower");
                    } else if (g < n) {
                        Console.WriteLine("Higher");
                    } else {
                        Console.WriteLine($"Congratulations! You guessed the number {n} with {t} attempts remaining.");
                        return;
                    }

                    t--;
                }
            }

            Console.WriteLine($"You lost! The number was {n}");
        }

    }
}
