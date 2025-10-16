using System;
using System.Diagnostics;

namespace Week2Foundations
{
    internal class Program
    {
        static void Main(string[] args)
        {// to call the demos
            RunArrayDemo();
            RunListDemo();
            RunStackDemo();
            RunQueueDemo();
            RunDictionaryDemo();
            RunHashSetDemo();
            RunBenchmarks();   
        }

        // ---------- A. Array Demo ----------
        static void RunArrayDemo()
        {
            Console.WriteLine("=== Array Demo ===");

            int[] arr = new int[10];
            arr[0] = 2;
            arr[2] = 4;
            arr[5] = 8;

            Console.WriteLine("Value at index 2: " + arr[2]);

            int target = 4;
            bool found = false;
            for (int i = 0; i < arr.Length; i++)
            {
                if (arr[i] == target)
                {
                    found = true;
                    break;
                }
            }

            Console.WriteLine("Searching for " + target + ": " + (found ? "found" : "not found"));
            Console.WriteLine();
        }

        static void RunListDemo()
        {
            Console.WriteLine("=== List<T> Demo ===");

            // Start with 1–5
            var numbers = new List<int> { 1, 2, 3, 4, 5 };
            Console.WriteLine("Original list: " + string.Join(", ", numbers));

            // Insert 99 at index 2
            numbers.Insert(2, 6);
            Console.WriteLine("After insert: " + string.Join(", ", numbers));

            // Remove 99
            numbers.Remove(6);
            Console.WriteLine("After remove: " + string.Join(", ", numbers));

            // Print final Count
            Console.WriteLine("Final count: " + numbers.Count);
            Console.WriteLine();
        }

        static void RunStackDemo()
        {
            Console.WriteLine("=== Stack<T> Demo ===");

            var stack = new Stack<string>();

            // Push three pages like a browser history
            stack.Push("Home Page");
            stack.Push("About Page");
            stack.Push("Contact Page");

            // Peek shows the current page
            Console.WriteLine("Peek (current page): " + stack.Peek());

            // Pop all to simulate going “Back”
            Console.Write("Back navigation order: ");
            while (stack.Count > 0)
            {
                Console.Write(stack.Pop());
                if (stack.Count > 0) Console.Write(" -> ");
            }

            Console.WriteLine("\n");
        }

        static void RunQueueDemo()
        {
            Console.WriteLine("=== Queue<T> Demo ===");

            // make a new queue (like a printer waiting line)
            var printQueue = new Queue<string>();

            // add print jobs to the queue
            printQueue.Enqueue("Document Z");
            printQueue.Enqueue("Document Y");
            printQueue.Enqueue("Document X");

            // see who's next in line (first job)
            Console.WriteLine("Next job: " + printQueue.Peek());

            // print them one by one in the same order they came in
            Console.Write("Processing order: ");
            while (printQueue.Count > 0)
            {
                Console.Write(printQueue.Dequeue()); // take out the first one
                if (printQueue.Count > 0) Console.Write(" -> "); // arrow between jobs
            }

            Console.WriteLine("\n"); // just space for clarity
        }
       static void RunDictionaryDemo()
        {
    Console.WriteLine("=== Dictionary<TKey, TValue> Demo ===");

    // make a small dictionary that maps product codes to their quantities
    var inventory = new Dictionary<string, int>
    {
        { "Z100", 5 },
        { "Y200", 8 },
        { "X300", 2 }
    };

    // update one of the quantities
    inventory["Y200"] = 10;

    // show updated values
    Console.WriteLine("Updated Y200 quantity: " + inventory["Y200"]);

    // try to get a key that doesn't exist
    if (inventory.TryGetValue("A999", out int missing))
        Console.WriteLine("Found missing key with value: " + missing);
    else
        Console.WriteLine("Missing key 'A999' not found.");

    Console.WriteLine();
       }

        static void RunHashSetDemo()
        {
            Console.WriteLine("=== HashSet<T> Demo ===");

            // make a hashset with a few numbers (duplicates won't be stored)
            var nums = new HashSet<int> { 1, 2, 3, 3, 4 };

            // try adding a duplicate
            bool added = nums.Add(4);
            Console.WriteLine("Trying to add duplicate '4' again: " + (added ? "added" : "duplicate ignored"));

            // combine with another small set
            var extra = new HashSet<int> { 3, 4, 5 };
            nums.UnionWith(extra); // merge unique values

            Console.WriteLine("Final numbers: " + string.Join(", ", nums));
            Console.WriteLine("Final count: " + nums.Count);
            Console.WriteLine();
        }

        static void RunBenchmarks()
        {
    Console.WriteLine("=== Mini Benchmark: List vs HashSet vs Dictionary ===");

    int[] testSizes = { 1000, 10000, 100000 }; // you can add 250000 if your laptop handles it

    foreach (int N in testSizes)
    {
        Console.WriteLine($"\nN = {N}");

        // build data for each structure
        var list = new List<int>();
        var set = new HashSet<int>();
        var dict = new Dictionary<int, bool>();

        for (int i = 0; i < N; i++)
        {
            list.Add(i);
            set.Add(i);
            dict[i] = true;
        }

        int existing = N - 1;
        int missing = -1;

        // stopwatch helper
        Stopwatch sw = new Stopwatch();

        // --- List.Contains(existing) ---
        sw.Restart();
        list.Contains(existing);
        sw.Stop();
        Console.WriteLine($"List.Contains({existing}):   {sw.Elapsed.TotalMilliseconds:F3} ms");

        // --- HashSet.Contains(existing) ---
        sw.Restart();
        set.Contains(existing);
        sw.Stop();
        Console.WriteLine($"HashSet.Contains({existing}): {sw.Elapsed.TotalMilliseconds:F3} ms");

        // --- Dict.ContainsKey(existing) ---
        sw.Restart();
        dict.ContainsKey(existing);
        sw.Stop();
        Console.WriteLine($"Dict.ContainsKey({existing}): {sw.Elapsed.TotalMilliseconds:F3} ms");

        // --- List.Contains(missing) ---
        sw.Restart();
        list.Contains(missing);
        sw.Stop();
        Console.WriteLine($"List.Contains({missing}):   {sw.Elapsed.TotalMilliseconds:F3} ms");

        // --- HashSet.Contains(missing) ---
        sw.Restart();
        set.Contains(missing);
        sw.Stop();
        Console.WriteLine($"HashSet.Contains({missing}): {sw.Elapsed.TotalMilliseconds:F3} ms");

        // --- Dict.ContainsKey(missing) ---
        sw.Restart();
        dict.ContainsKey(missing);
        sw.Stop();
        Console.WriteLine($"Dict.ContainsKey({missing}): {sw.Elapsed.TotalMilliseconds:F3} ms");
    }

    Console.WriteLine("\nBenchmark done.\n");
         }
      




    }
}
