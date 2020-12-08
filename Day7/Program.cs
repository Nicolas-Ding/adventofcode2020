using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
using Utils;

namespace Day7
{
    public class Program : BaseMain
    {
        public static Regex InputLineRegexp = new Regex(@"(?<containerColor>.*?) bags contain (?<containedColors>.*?)\.");
        public static Regex ColoredBagRegexp = new Regex(@"(?<containedNumber>\d) (?<containedColor>.*?) bags?");

        public static void Main(string[] args)
        {
            RunAndTime(AocDay7, 20);
        }

        static void AocDay7()
        {
            Dictionary<string, HashSet<string>> canBeContainedBy = new Dictionary<string, HashSet<string>>();
            Dictionary<string, Dictionary<string, int>> containsBags = new Dictionary<string, Dictionary<string, int>>();
            Utils.Utils.ReadLinesFromFile("input.txt")
                .ToList()
                .ForEach(line =>
                {
                    Match match = InputLineRegexp.Match(line);
                    string containerColor = match.Groups["containerColor"].Value;
                    if (!canBeContainedBy.ContainsKey(containerColor))
                    {
                        canBeContainedBy[containerColor] = new HashSet<string>();
                    }

                    containsBags[containerColor] = new Dictionary<string, int>();

                    if (match.Groups["containedColors"].Value == "no other bags")
                    {
                        return;
                    }

                    match.Groups["containedColors"].Value.Split(", ").ToList().ForEach(containedColorString =>
                    {
                        Match containedMatch = ColoredBagRegexp.Match(containedColorString);
                        string containedColor = containedMatch.Groups["containedColor"].Value;
                        if (!canBeContainedBy.ContainsKey(containedColor))
                        {
                            canBeContainedBy[containedColor] = new HashSet<string>();
                        }

                        canBeContainedBy[containedColor].Add(containerColor);
                        containsBags[match.Groups["containerColor"].Value][containedColor] =
                            int.Parse(containedMatch.Groups["containedNumber"].Value);
                    });
                });

            // Part 1
            long frequency = Stopwatch.Frequency;
            long nanosecPerTick = (1000L * 1000L * 1000L) / frequency;

            HashSet<string> canContainShinyGold = new HashSet<string>();
            Queue<string> colorsQueue = new Queue<string>();
            colorsQueue.Enqueue("shiny gold");
            while (colorsQueue.Any())
            {
                string currentColor = colorsQueue.Dequeue();
                foreach (string newContainer in canBeContainedBy[currentColor])
                {
                    canContainShinyGold.Add(newContainer);
                    colorsQueue.Enqueue(newContainer);
                }
            }

            Console.WriteLine($"Number of bag colors that can contains at least one shiny gold : {canContainShinyGold.Count}");

            // Part 2
            Queue<(string, int)> containedBagsQueue = new Queue<(string, int)>();
            int result = 0;
            containedBagsQueue.Enqueue(("shiny gold", 1));
            while (containedBagsQueue.Any())
            {
                (string currentColor, int currentCount) = containedBagsQueue.Dequeue();
                foreach (var kvp in containsBags[currentColor])
                {
                    result += kvp.Value * currentCount;
                    containedBagsQueue.Enqueue((kvp.Key, kvp.Value * currentCount));
                }
            }
            Console.WriteLine($"Number of bag that a shiny gold contains : {result}");
        }
    }
}
