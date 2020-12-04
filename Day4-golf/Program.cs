using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Day4_golf
{
    class Program
    {
        static void Main(string[] args)
        {
            int result = 0;
            string line;
            line = Console.ReadLine();

            do
            {
                StringBuilder fullId = new StringBuilder();
                do
                {
                    fullId.Append(" ");
                    fullId.Append(line);
                    line = Console.ReadLine();

                } while (!string.IsNullOrEmpty(line));

                if (IsValidPart2(fullId.ToString()))
                {
                    result++;
                }

                line = Console.ReadLine();
            } while (!string.IsNullOrEmpty(line));

            Console.WriteLine(result);
        }

        static List<string> regexes = new List<string> {
            @"byr:(19[2-9][0-9]|200[0-2])\b",
            @"iyr:(201[0-9]|2020)\b",
            @"eyr:(202[0-9]|2030)\b",
            @"hgt:((1[5-8][0-9]|19[0-3])cm|(7[0-6]|59|6[0-9])in)\b",
            @"hcl:#[0-9a-f]{6}\b",
            @"ecl:(amb|blu|brn|gry|grn|hzl|oth)\b",
            @"pid:\d{9}\b" };

        public static bool IsValidPart2(string line)
        {
            return regexes.All(reg => Regex.IsMatch(line, reg));
        }
    }
}
