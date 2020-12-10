using System;
using System.Collections.Generic;
using System.Linq;

namespace Day10
{
    public class Program
    {
        public static void Main(string[] args)
        {
            List<int> sorted = Utils.Utils.ReadLinesFromConsole()
                .Select(int.Parse)
                .ToList();
            
            sorted.Add(0);
            sorted.Add(sorted.Max() + 3);
            sorted.Sort();

            // Part 1
            Dictionary<int, int> diffs = new Dictionary<int, int>();
            for (int i = 0; i < sorted.Count - 1; i++)
            {
                int diff = sorted[i + 1] - sorted[i];
                diffs[diff] = 1 + (diffs.TryGetValue(diff, out int val) ? val : 0);
            }

            Console.WriteLine($"{diffs[1]} one-jolt * {diffs[3]} three-jolt = {(long)diffs[1] * diffs[3]}");

            // Part 2
            Dictionary<int, long> possibilities = new Dictionary<int, long> {[sorted[0]] = 1};
            for (int i = 1; i < sorted.Count; i++)
            {
                possibilities[sorted[i]] =
                    (possibilities.TryGetValue(sorted[i] - 1, out long val1) ? val1 : 0)
                    + (possibilities.TryGetValue(sorted[i] - 2, out long val2) ? val2 : 0)
                    + (possibilities.TryGetValue(sorted[i] - 3, out long val3) ? val3 : 0);
            }

            Console.WriteLine($"Counted {possibilities[sorted[^1]]} possibilities");
        }
    }
}
