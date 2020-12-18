using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Day16
{
    class Program
    {
        static Regex rulesRegex = new Regex(@"(?<field>[a-z ]+): (?<min1>\d+)-(?<max1>\d+) or (?<min2>\d+)-(?<max2>\d+)");

        static Dictionary<string, (int min1, int max1, int min2, int max2)> rules;

        static void Main(string[] args)
        {
            rules = Utils.Utils.ReadLinesFromConsole()
                .Select(_ =>
                {
                    Match match = rulesRegex.Match(_);
                    return (
                        match.Groups["field"].Value,
                        int.Parse(match.Groups["min1"].Value),
                        int.Parse(match.Groups["max1"].Value),
                        int.Parse(match.Groups["min2"].Value),
                        int.Parse(match.Groups["max2"].Value));
                })
                .ToDictionary(g => g.Item1, g => (g.Item2, g.Item3, g.Item4, g.Item5));

            long[] myTicket = Utils.Utils.ReadLinesFromConsole()
                .ToList()
                .Skip(1)
                .First()
                .Split(',')
                .Select(long.Parse)
                .ToArray();

            // Part 1
            //int result = Utils.Utils.ReadLinesFromConsole()
            //    .Skip(1) // nearby tickets
            //    .Aggregate(0, (res, ticket) =>
            //    {
            //        return res + 
            //            ticket.Split(',')
            //           .ToList()
            //           .Select(int.Parse)
            //           .Where(number => rules.All(rule =>
            //                    (number < rule.min1 || number > rule.max1) &&
            //                    (number < rule.min2 || number > rule.max2)))
            //           .Sum();
            //    });

            // Console.WriteLine(result);

            // Part 2
            List<int[]> ticketNumbers = Utils.Utils.ReadLinesFromConsole()
                .Skip(1)
                .Select(ticket => ticket.Split(',').Select(int.Parse))
                .Where(numbers =>
                            numbers
                           .All(number => rules.Any(kvp =>
                                    (number >= kvp.Value.min1 && number <= kvp.Value.max1) ||
                                    (number >= kvp.Value.min2 && number <= kvp.Value.max2))))
                .Select(_ => _.ToArray())
                .ToList();

            Dictionary<string, int> myFields = new Dictionary<string, int>();
            Dictionary<int, HashSet<string>> foundRules = new Dictionary<int, HashSet<string>>();
            Dictionary<string, int> correctRules = new Dictionary<string, int>();

            for (int i = 0; i < ticketNumbers[0].Count(); i++)
            {
                var foundRule = rules
                    .Where(rule => ticketNumbers.All(numbers =>
                        (numbers[i] >= rule.Value.min1 && numbers[i] <= rule.Value.max1) ||
                        (numbers[i] >= rule.Value.min2 && numbers[i] <= rule.Value.max2)));

                foundRules[i] = foundRule.Select(_ => _.Key).ToHashSet();
            }

            while (correctRules.Count < foundRules.Count)
            {
                foreach (var kvp in foundRules)
                {
                    if (kvp.Value.Count() == 1)
                    {
                        string ruleName = kvp.Value.First();
                        correctRules[ruleName] = kvp.Key;
                        foreach (var kvp2 in foundRules)
                        {
                            kvp2.Value.Remove(ruleName);
                        }
                    }
                }
                Console.WriteLine(string.Join(", ", correctRules.Select(_ => $"{_.Key} : {_.Value}")));
            }

            Console.WriteLine("----------------RESULT--------------");
            long res = correctRules
                .Where(_ => _.Key.StartsWith("departure"))
                .Aggregate((long)1, (res, _) =>
                {
                    Console.WriteLine($"{_.Key} : {myTicket[_.Value]}");
                    return res * myTicket[_.Value];
                });
            Console.WriteLine($"Final result : {res}");
         }
    }
}
