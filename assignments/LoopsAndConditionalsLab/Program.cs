using System;
using System.Collections.Generic;

namespace LoopsAndConditionalsLab
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Task 1: Write a method that calculates the sum of even numbers between 1 and 100 using:

            //for loop
            int sum1 = 0;
            for (int i = 1; i <= 100; i++)
            {
                if (i % 2 == 0)
                {
                    sum1 += i;
                }
            }

            Console.WriteLine("For loop sum: " + sum1);

            //while loop
            int sum2 = 0;
            int j = 1;
            while (j <= 100)
            {
                if (j % 2 == 0)
                {
                    sum2 += j;
                }
                j++;
            }
            Console.WriteLine("While loop sum: " + sum2);

            //foreach loop
            List<int> numbers = new List<int>();
            for (int k = 1; k <= 100; k++)
            {
                numbers.Add(k);
            }

            int sum3 = 0;
            foreach (int n in numbers)
            {
                if (n % 2 == 0)
                {
                    sum3 += n;
                }
            }
            Console.WriteLine("Foreach loop sum: " + sum3);


            // For me, the for-loop was the easiest because the range is clear (1–100), and the construction is the simplest to me.
            // The while-loop worked but it felt like extra steps. The foreach loop was fine too, but I had to build a list first which felt a bit unnecessary.


            //Task 2: Grading with conditionals

            int score = 87; // test score; can be changed for testing other numbers/outcomes.
            string gradeIf = GetLetterGrade_IfElse(score);
            string gradeSwitch = GetLetterGrade_Switch(score);

            Console.WriteLine("\nTask 2: Grading");
            Console.WriteLine("Score: " + score);
            Console.WriteLine("If/Else Grade: " + gradeIf);
            Console.WriteLine("Switch Grade: " + gradeSwitch);

            // Task 3: Big number check

            // Using if/else
            if (sum1 > 2000)
            {
                Console.WriteLine("\nTask 3: if/else says -> That’s a big number!");
            }
            else
            {
                Console.WriteLine("\nTask 3: if/else says -> Number is within range.");
            }

            // Using ternary operator
            string message = (sum1 > 2000) ? "That’s a big number!" : "Number is within range.";
            Console.WriteLine("Task 3: ternary says -> " + message);
        }

        //The if/else was easier for me because it's clear to read, clear steps.
        //the ternary acts the same but in one line, so it's shorter. 
        //I prefer the if/else for the direct clarity, but I like how the ternary operator is short and quick. 

        // with If/Else version
        static string GetLetterGrade_IfElse(int score)
        {
            if (score >= 90 && score <= 100) return "A";
            else if (score >= 80) return "B";
            else if (score >= 70) return "C";
            else if (score >= 60) return "D";
            else return "F";
        }

        // with Switch version
        static string GetLetterGrade_Switch(int score)
        {
            switch (score)
            {
                case int n when (n >= 90 && n <= 100):
                    return "A";
                case int n when (n >= 80):
                    return "B";
                case int n when (n >= 70):
                    return "C";
                case int n when (n >= 60):
                    return "D";
                default:
                    return "F";
            }
        }
        
           
            // The if/else feels more natural to me because it’s like how you’d explain things in plain English: “if this, else that.” 
            // The switch one looks cleaner and it’s kind of satisfying to look at, but it feels more technical. You have to already know
           // the syntax to “translate it” in your head. So right now, if/else makes more sense for me.

    }
}
