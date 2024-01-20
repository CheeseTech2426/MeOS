using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeOS.Programs.Games {
    internal class TicTacToe {
        static char[] board = { ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ' };
        static char currentPlayer = 'X';

        public static void Run() {
            bool gameRunning = true;

            while (gameRunning) {
                DrawBoard();

                Console.WriteLine($"Player {currentPlayer}, enter your move (1-9):");
                int move = Convert.ToInt32(Console.ReadLine());

                if (IsValidMove(move)) {
                    board[move - 1] = currentPlayer;

                    if (CheckWin()) {
                        DrawBoard();
                        Console.WriteLine($"Player {currentPlayer} wins!");

                        Console.WriteLine("Do you want to play again? (Y/N)");
                        string playAgain = Console.ReadLine();

                        if (playAgain.ToUpper() == "Y") {
                            ResetBoard();
                        } else {
                            gameRunning = false;
                        }
                    } else if (IsBoardFull()) {
                        DrawBoard();
                        Console.WriteLine("It's a draw!");

                        Console.WriteLine("Do you want to play again? (Y/N)");
                        string playAgain = Console.ReadLine();

                        if (playAgain.ToUpper() == "Y") {
                            ResetBoard();
                        } else {
                            gameRunning = false;
                        }
                    } else {
                        currentPlayer = (currentPlayer == 'X') ? 'O' : 'X';
                    }
                } else {
                    Console.WriteLine("Invalid move. Try again.");
                }
            }

            Console.WriteLine("Game over. Press any key to exit.");
            Console.ReadKey();
        }

        static void DrawBoard() {
            Console.Clear();
            Console.WriteLine($" {board[0]} | {board[1]} | {board[2]} ");
            Console.WriteLine("---+---+---");
            Console.WriteLine($" {board[3]} | {board[4]} | {board[5]} ");
            Console.WriteLine("---+---+---");
            Console.WriteLine($" {board[6]} | {board[7]} | {board[8]} ");
        }

        static bool IsValidMove(int move) {
            return move >= 1 && move <= 9 && board[move - 1] == ' ';
        }

        static bool CheckWin() {
            return
                (board[0] == board[1] && board[1] == board[2] && board[0] != ' ') ||
                (board[3] == board[4] && board[4] == board[5] && board[3] != ' ') ||
                (board[6] == board[7] && board[7] == board[8] && board[6] != ' ') ||
                (board[0] == board[3] && board[3] == board[6] && board[0] != ' ') ||
                (board[1] == board[4] && board[4] == board[7] && board[1] != ' ') ||
                (board[2] == board[5] && board[5] == board[8] && board[2] != ' ') ||
                (board[0] == board[4] && board[4] == board[8] && board[0] != ' ') ||
                (board[2] == board[4] && board[4] == board[6] && board[2] != ' ');
        }

        static bool IsBoardFull() {
            return !board.Contains(' ');
        }

        static void ResetBoard() {
            Array.Clear(board, 0, board.Length);
            currentPlayer = 'X';
        }
    }    
}

