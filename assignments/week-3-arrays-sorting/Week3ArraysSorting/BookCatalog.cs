using System;
using System.Collections.Generic;
using System.IO;

namespace Week3ArraysSorting
{
// Each line in books.txt is one book title.
// We sort the titles using our own mergesort,
// make a 26x26 index based on the first two letters,
// and then use that index to look up books fast.

    public class BookCatalog
    {
        // store original + normalized together so we keep them aligned
        private struct Entry
        {
            public string Norm;
            public string Orig;
            public Entry(string n, string o) { Norm = n; Orig = o; }
        }

        private List<Entry> items = new();   // raw loaded entries
        private Entry[] sorted = Array.Empty<Entry>(); // after sorting

        // 2D index: for each [first, second] letter we store [start, end) bounds
        private int[,] start = new int[26, 26];
        private int[,] end   = new int[26, 26];

        public void Load(string filePath)
        {
            items.Clear();
            foreach (var line in File.ReadAllLines(filePath))
            {
                var orig = line.Trim();
                if (orig.Length == 0) continue;
                var norm = Normalize(orig);
                items.Add(new Entry(norm, orig));
            }
        }

        public void SortAndIndex()
        {
            // sort with mergesort
            sorted = items.ToArray();
            MergeSort(sorted, 0, sorted.Length);

            // initialize index to -1 (means empty)
            for (int i = 0; i < 26; i++)
                for (int j = 0; j < 26; j++)
                    start[i, j] = end[i, j] = -1;

            if (sorted.Length == 0) return;

            // walk once, and record the [start,end) range per [first,second] pair.
            int prevI = -1, prevJ = -1, rangeStart = 0;
            for (int idx = 0; idx < sorted.Length; idx++)
            {
                (int i, int j) = Pair(sorted[idx].Norm);
                if (idx == 0)
                {
                    prevI = i; prevJ = j; rangeStart = 0;
                    start[i, j] = 0;
                }
                else if (i != prevI || j != prevJ)
                {
                    end[prevI, prevJ] = idx;       // previous range closes at this index
                    if (start[i, j] == -1) start[i, j] = idx;
                    prevI = i; prevJ = j; rangeStart = idx;
                }
            }
            // close last range open
            end[prevI, prevJ] = sorted.Length;
        }

        public void LookupLoop()
        {
            Console.WriteLine("\n=== Book Catalog Lookup ===");
            Console.WriteLine("Type a title (or 'exit'). The search ignores capitalization.\n");

            while (true)
            {
                Console.Write("Query> ");
                var q = Console.ReadLine();
                if (q == null) continue;
                if (q.Trim().Equals("exit", StringComparison.OrdinalIgnoreCase)) break;

                var norm = Normalize(q);
                (int i, int j) = Pair(norm);

                int s = start[i, j];
                int e = end[i, j];

                if (s == -1 || e == -1 || s >= e)
                {
                    // empty slice → give a few suggestions near the insertion point
                    Console.WriteLine("No exact match. Suggestions:");
                    SuggestFromAll(norm, 5);
                    Console.WriteLine();
                    continue;
                }

                // binary search inside slice
                int pos = BinarySearch(norm, s, e);
                if (pos >= s && pos < e && sorted[pos].Norm == norm)
                {
                    Console.WriteLine($"Found: {sorted[pos].Orig}\n");
                }
                else
                {
                    Console.WriteLine("No exact match. Suggestions:");
                    // use insertion index inside the slice
                    int ins = pos >= 0 ? pos : (~pos);
                    for (int k = -2; k <= 2; k++)
                    {
                        int idx = ins + k;
                        if (idx >= s && idx < e)
                            Console.WriteLine(" - " + sorted[idx].Orig);
                    }
                    Console.WriteLine();
                }
            }
        }

        // Normalization
        private static string Normalize(string s)
        {
            // trim + uppercase
            return s.Trim().ToUpperInvariant();
        }

        // map first two chars to [0..25]. Non-letter becomes 'A'(0).
        private static (int i, int j) Pair(string norm)
        {
            char c1 = norm.Length > 0 ? norm[0] : 'A';
            char c2 = norm.Length > 1 ? norm[1] : 'A';
            int i = (c1 >= 'A' && c1 <= 'Z') ? c1 - 'A' : 0;
            int j = (c2 >= 'A' && c2 <= 'Z') ? c2 - 'A' : 0;
            return (i, j);
        }

        // Mergesort 
        private static void MergeSort(Entry[] arr, int lo, int hi)
        {
            int len = hi - lo;
            if (len <= 1) return;
            int mid = lo + len / 2;
            MergeSort(arr, lo, mid);
            MergeSort(arr, mid, hi);
            Merge(arr, lo, mid, hi);
        }

        private static void Merge(Entry[] arr, int lo, int mid, int hi)
        {
            int i = lo, j = mid, k = 0;
            var tmp = new Entry[hi - lo];

            while (i < mid && j < hi)
            {
                if (string.Compare(arr[i].Norm, arr[j].Norm, StringComparison.Ordinal) <= 0)
                    tmp[k++] = arr[i++];
                else
                    tmp[k++] = arr[j++];
            }
            while (i < mid) tmp[k++] = arr[i++];
            while (j < hi)  tmp[k++] = arr[j++];

            //Copy merged back to the array slice
            for (int t = 0; t < tmp.Length; t++)
                arr[lo + t] = tmp[t];
        }

        //Binary search in [s, e) on Norm 
        private int BinarySearch(string key, int s, int e)
        {
            int lo = s, hi = e - 1;
            while (lo <= hi)
            {
                int mid = (lo + hi) / 2;
                int cmp = string.Compare(sorted[mid].Norm, key, StringComparison.Ordinal);
                if (cmp == 0) return mid;
                if (cmp < 0) lo = mid + 1; else hi = mid - 1;
            }
            // not found -> return ~insertionIndex 
            return ~lo;
        }

        // fallback -> suggest a few titles near theinsertion point
        private void SuggestFromAll(string norm, int count)
        {
            if (sorted.Length == 0) return;
            int pos = BinarySearch(norm, 0, sorted.Length);
            int ins = pos >= 0 ? pos : (~pos);

            int shown = 0;
            for (int k = -2; k <= 2 && shown < count; k++)
            {
                int idx = ins + k;
                if (idx >= 0 && idx < sorted.Length)
                {
                    Console.WriteLine(" - " + sorted[idx].Orig);
                    shown++;
                }
            }
        }
    }
}
