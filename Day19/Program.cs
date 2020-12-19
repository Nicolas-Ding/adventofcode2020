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

            int part1 = Utils.Utils.ReadLinesFromConsole()
                .Count(line =>
                {
                    int i = 0;
                    int rule42Count = 0;
                    int rule31Count = 0;
                    while (i < line.Length)
                    {
                        var res = rules[42].IsMatch(rules, line.Substring(i));
                        if (!res.isMatch)
                        {
                            break;
                        }
                        i += res.matchLength;
                        rule42Count++;
                    }
                    while (i < line.Length)
                    {
                        var res = rules[31].IsMatch(rules, line.Substring(i));
                        if (!res.isMatch)
                        {
                            return false;
                        }
                        i += res.matchLength;
                        rule31Count++;
                    }
                    //Console.WriteLine($"For line {line}, found {rule42Count} matching rule 42 and {rule31Count} matching rule 31");
                    //Console.WriteLine($"returning {rule42Count > rule31Count && rule42Count > 0 && rule31Count > 0}");
                    return (rule42Count > rule31Count && rule42Count > 0 && rule31Count > 0);
                });

            Console.WriteLine($"Part 1 : {part1}");
        }
    }
}
