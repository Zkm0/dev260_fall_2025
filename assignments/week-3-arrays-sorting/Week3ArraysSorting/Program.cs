using System;

namespace Week3ArraysSorting
{
    internal class Program
    {
        static void Main(string[] args)
        {//menu t pick what to run 
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Week 3: Arrays & Sorting");
                Console.WriteLine("1) Play Tic-Tac-Toe (Part A)");
                Console.WriteLine("2) Book Catalog Lookup (Part B)");
                Console.WriteLine("0) Exit");
                Console.Write("Choose: ");
                var ch = Console.ReadLine();

                if (ch == "1")
                {
                    var game = new TicTacToe();
                    game.Run();
                }
                else if (ch == "2")
                {
                    var cat = new BookCatalog();
                    try
                    {
                        // books.txt file should be in the same folder as the project folder for the project to run
                        cat.Load("books.txt");
                        cat.SortAndIndex();
                        cat.LookupLoop();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error: " + ex.Message);
                        Console.WriteLine("Make sure books.txt exists.");
                        Console.WriteLine("Press ENTER...");
                        Console.ReadLine();
                    }
                }
                else if (ch == "0") break; //to exit the app
            }
        }
    }
}
