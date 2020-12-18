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
            foreach (string line in Utils.Utils.ReadLinesFromConsole())
            {
                if (line.StartsWith("mask = "))
                {
                    mask = line.Substring(7);
                }
                else
                {
                    Match match = regex.Match(line);
                    memory[long.Parse(match.Groups["key"].Value)] =
                        Convert.ToInt64(applyMask(long.Parse(match.Groups["value"].Value), mask), 2);
                }
            }
            long sum = 0;
            foreach (var kvp in memory)
            {
                sum += kvp.Value;
            }
            Console.WriteLine(sum);
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
    }
}
