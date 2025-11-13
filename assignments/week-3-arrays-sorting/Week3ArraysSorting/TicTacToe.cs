using System;

namespace Week3ArraysSorting
{
    // 3x3 tic-tac-toe using a 2D array
    public class TicTacToe
    {
        private char[,] board = new char[3, 3];
        private char current = 'X';

        public void Run()
        {
            Reset();
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== Tic-Tac-Toe (3x3) ===");
                Render();

                // to ask for row and col input (eg: 1 3) or "q" to quit
                Console.Write($"\nPlayer {current}, enter row and col (e.g. 2 3) or 'q' to quit: ");
                var input = Console.ReadLine();
                if (input == null) continue;
                if (input.Trim().Equals("q", StringComparison.OrdinalIgnoreCase)) return;

                var parts = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                if (parts.Length != 2 || !int.TryParse(parts[0], out int r) || !int.TryParse(parts[1], out int c))
                {
                    Pause("Invalid format. Use: 2 3");
                    continue;
                }

                // to convert to 0-based and validate the range and empty cell
                r--; c--;
                if (!InRange(r) || !InRange(c))
                {
                    Pause("Row/Col must be between 1 and 3.");
                    continue;
                }
                if (board[r, c] != ' ')
                {
                    Pause("That spot is taken. Try another.");
                    continue;
                }

                // to place the mark
                board[r, c] = current;

                // to check win or draw after the move
                if (IsWin(current))
                {
                    Console.Clear();
                    Render();
                    Console.WriteLine($"\nPlayer {current} wins! 🎉");
                    if (!AskReplay()) return;
                }
                else if (IsDraw())
                {
                    Console.Clear();
                    Render();
                    Console.WriteLine("\nIt's a draw.");
                    if (!AskReplay()) return;
                }
                else
                {
                    // to switch player
                    current = current == 'X' ? 'O' : 'X';
                }
            }
        }

        private void Reset()
        {
            for (int r = 0; r < 3; r++)
                for (int c = 0; c < 3; c++)
                    board[r, c] = ' ';
            current = 'X';
        }

        private static bool InRange(int i) => i >= 0 && i < 3;

        private void Render()
        {
            // ASCII board
            for (int r = 0; r < 3; r++)
            {
                Console.Write(" ");
                for (int c = 0; c < 3; c++)
                {
                    Console.Write(board[r, c] == ' ' ? '.' : board[r, c]);
                    if (c < 2) Console.Write(" | ");
                }
                Console.WriteLine();
                if (r < 2) Console.WriteLine("---+---+---");
            }
        }

        private bool IsWin(char p)
        {
            // for rows and cols
            for (int i = 0; i < 3; i++)
            {
                if (board[i,0] == p && board[i,1] == p && board[i,2] == p) return true;
                if (board[0,i] == p && board[1,i] == p && board[2,i] == p) return true;
            }
            // to check the diagonals
            if (board[0,0] == p && board[1,1] == p && board[2,2] == p) return true;
            if (board[0,2] == p && board[1,1] == p && board[2,0] == p) return true;
            return false;
        }

        private bool IsDraw()
        {
            foreach (var cell in board) if (cell == ' ') return false;
            return true;
        }

        private static void Pause(string msg)
        {
            Console.WriteLine(msg);
            Console.Write("Press ENTER...");
            Console.ReadLine();
        }

        private bool AskReplay()
        {
            Console.Write("\nPlay again? (y/n): ");
            var ans = Console.ReadLine();
            if (ans?.Trim().ToLower() == "y")
            {
                Reset();
                return true;
            }
            return false;
        }
    }
}
