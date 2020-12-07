using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;

namespace Day7
{
    class Program
    {
        public static Regex LineRegexp = new Regex(@"^(?<containerColor>.*?) bags contain (?:(?<noOtherBag>no other bags)|((?<containedNumber>\d) (?<containedColor>.*?) bags?)(?:, (?!\.))?)+.$");

        static void Main(string[] args)
        {
            Dictionary<string, HashSet<string>> canBeContainedBy = new Dictionary<string, HashSet<string>>();
            Dictionary<string, Dictionary<string, int>> containsBags = new Dictionary<string, Dictionary<string, int>>();

            Utils.Utils.ReadLinesFromConsole()
                .ToList()
                .ForEach(line =>
                {
                    Match match = LineRegexp.Match(line);
                    string containerColor = match.Groups["containerColor"].Value;
                    if (!canBeContainedBy.ContainsKey(containerColor))
                    {
                        canBeContainedBy[containerColor] = new HashSet<string>();
                    }
                    containsBags[containerColor] = new Dictionary<string, int>();
                    if (match.Groups["noOtherBag"].Success)
                    {
                        return;
                    }

                    for (var i = 0; i < match.Groups["containedNumber"].Captures.Count; i++)
                    {
                        string containedColor = match.Groups["containedColor"].Captures[i].Value;
                        int containedNumber = int.Parse(match.Groups["containedNumber"].Captures[i].Value);
                        if (!canBeContainedBy.ContainsKey(containedColor))
                        {
                            canBeContainedBy[containedColor] = new HashSet<string>();
                        }

                        canBeContainedBy[containedColor].Add(containerColor);
                        containsBags[match.Groups["containerColor"].Value][containedColor] = containedNumber;
                    }
                });

            // Part 1
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
