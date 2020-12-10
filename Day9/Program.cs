using System;
using System.Collections.Generic;
using System.Linq;
using Utils;

namespace Day9
{
    class Program : BaseMain
    {
        new static void Main(string[] args)
        {
            RunAndTime(Day9, 20);
        }

        static void Day9()
        {
            Dictionary<long, HashSet<long>> possibleSums = new Dictionary<long, HashSet<long>>();
            List<long> input = Utils.Utils.ReadLinesFromFile("input.txt")
                .Select(_ => long.Parse(_))
                .ToList();

            // Part 1
            int n = 25;
            long result = 0;
            for (int i = 0; i < n; i++)
            {
                possibleSums[input[i]] = input.Skip(i + 1).Take(n - i - 1).Select(_ => _ + input[i]).ToHashSet();
            }

            for (int i = n; i < input.Count; i++)
            {
                if (!possibleSums.Any(kvp => kvp.Value.Contains(input[i])))
                {
                    Console.WriteLine($"Couldn't sum to the number {input[i]}");
                    result = input[i];
                    break;
                }

                possibleSums.Remove(input[i - n]);
                foreach (var key in possibleSums.Keys)
                {
                    possibleSums[key].Add(key + input[i]);
                }
                possibleSums[input[i]] = new HashSet<long>();
            }

            // Part 2
            int minIndex = 0;
            int maxIndex = 0;
            long cumsum = input[0];
            while (cumsum != result && maxIndex < input.Count)
            {
                if (cumsum < result)
                {
                    maxIndex++;
                    cumsum += input[maxIndex];
                }
                else
                {
                    cumsum -= input[minIndex];
                    minIndex++;
                }
            }
            Console.WriteLine($"Could sum to {cumsum} by taking numbers from {minIndex} to {maxIndex}");
            var resultList = input.Skip(minIndex).Take(maxIndex - minIndex + 1).ToList();
            Console.WriteLine($"Numbers were {string.Join(", ", resultList)}");
            Console.WriteLine($"Checking... the sum is actually {resultList.Sum()}");
            Console.WriteLine($"Min of these values is {resultList.Min()}");
            Console.WriteLine($"Max of these values is {resultList.Max()}");
            Console.WriteLine($"FinalResult is {resultList.Min() + resultList.Max()}");
        }
    }
}
