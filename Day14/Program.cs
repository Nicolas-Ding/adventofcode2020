using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Day14
{
    class Program
    {
        static Regex regex = new Regex(@"mem\[(?<key>\d+)\] = (?<value>\d+)");

        static void Main(string[] args)
        {
            string mask = "";
            Dictionary<long, long> memory = new Dictionary<long, long>();
            Dictionary<long, long> memory2 = new Dictionary<long, long>();

            foreach (string line in Utils.Utils.ReadLinesFromConsole())
            {
                if (line.StartsWith("mask = "))
                {
                    mask = line.Substring(7);
                }
                else
                {
                    Match match = regex.Match(line);
                    
                    // Part 1
                    memory[long.Parse(match.Groups["key"].Value)] = 
                        Convert.ToInt64(applyMask(long.Parse(match.Groups["value"].Value), mask), 2);


                    // Part 2
                    char[] part2 = applyMask2(long.Parse(match.Groups["key"].Value), mask);
                    var foundIndexes = new List<int>();
                    for (int i = 0; i < part2.Length; i++)
                    {
                        if (part2[i] == 'X')
                            foundIndexes.Add(i);
                    }
                    foreach (string s in GetAllPossibleValues(part2, foundIndexes))
                    {
                        memory2[Convert.ToInt64(s, 2)] = long.Parse(match.Groups["value"].Value);
                    }
                }
            }
            long sum1 = 0;
            long sum2 = 0;
            foreach (var kvp in memory)
            {
                sum1 += kvp.Value;
            }
            foreach (var kvp in memory2)
            {
                sum2 += kvp.Value;
            }
            Console.WriteLine(sum1);
            Console.WriteLine(sum2);
        }

        static string applyMask(long value, string mask)
        {
            string binaryValue = Convert.ToString(value, 2).PadLeft(36, '0');
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < 36; i++)
            {
                builder.Append(mask[i] == 'X' ? binaryValue[i] : mask[i]);
            }
            return builder.ToString();
        }

        static char[] applyMask2(long value, string mask)
        {
            string binaryValue = Convert.ToString(value, 2).PadLeft(36, '0');
            List<char> builder = new List<char>();
            for (int i = 0; i < 36; i++)
            {
                builder.Add(mask[i] == '0' ? binaryValue[i] : mask[i]);
            }
            return builder.ToArray();
        }

        static IEnumerable<string> GetAllPossibleValues(char[] s, List<int> xPositions)
        {
            if (xPositions.Count == 0)
            {
                yield return new string(s);
            }
            else
            {
                int xPos = xPositions[0];
                s[xPos] = '0';
                List<int> newList = new List<int>();
                for (int i = 1; i < xPositions.Count; i++)
                {
                    newList.Add(xPositions[i]);
                }
                foreach (var sol in GetAllPossibleValues(s, newList))
                {
                    yield return sol;
                }
                s[xPos] = '1';
                foreach (var sol in GetAllPossibleValues(s, newList))
                {
                    yield return sol;
                }
            }
        }
    }
}
