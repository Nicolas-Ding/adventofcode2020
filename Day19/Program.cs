using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Day19
{
    class Program
    {

        static Regex onlyLetters = new Regex(@"[a-z]+");

        static void Main(string[] args)
        {
            Dictionary<int, Rule> rules = Utils.Utils.ReadLinesFromConsole()
                .Select(_ => _.Split(':'))
                .ToDictionary(
                    _ => int.Parse(_[0]),
                    _ => _[1].Trim().StartsWith('"') ?
                        new Rule { Letter = _[1].Trim().Trim('"') } : 
                        new Rule { PossibleRules = _[1].Trim()
                            .Split('|')
                            .Select(subRule => subRule.Trim().Split().Select(int.Parse).ToList())
                            .ToList()}
                        );

            // Part 2 Special update
            rules[8] = new Rule
            {
                PossibleRules = new List<List<int>>
                {
                    new List<int> { 42 },
                    new List<int> { 42, 8 }
                }
            };

            rules[11] = new Rule
            {
                PossibleRules = new List<List<int>>
                {
                    new List<int> { 42, 31 },
                    new List<int> { 42, 11, 31 }
                }
            };

            //foreach (var kvp in rules)
            //{
            //    Console.WriteLine($"{kvp.Key}: {kvp.Value}");
            //}

            int part1 = Utils.Utils.ReadLinesFromConsole()
                .Count(line =>
                {
                    var res = rules[0].IsMatch(rules, line);
                    return res.isMatch && res.matchLength == line.Length;
                });

            Console.WriteLine($"Part 1 : {part1}");
        }
    }
}
