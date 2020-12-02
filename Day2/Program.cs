using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;

namespace Day2
{
    class Program
    {
        public static Regex Regexp = new Regex(@"(?<min>\d+)-(?<max>\d+) (?<letter>[a-z]): (?<password>[a-z]+)");

        public static void Main(string[] args)
        {
            string line = Console.ReadLine();
            int result = 0;
            while (!string.IsNullOrEmpty(line))
            {
                var parsed = Parse(line);
                if (IsValidPart2(parsed.password, parsed.letter, parsed.min, parsed.max))
                {
                    result++;
                }
                line = Console.ReadLine();
            }
            Console.WriteLine(result);
        }

        public static (string password, char letter, int min, int max) Parse(string line)
        {
            Match match = Regexp.Match(line);
            return (
                match.Groups["password"].Value,
                match.Groups["letter"].Value[0],
                int.Parse(match.Groups["min"].Value),
                int.Parse(match.Groups["max"].Value));
        }

        public static bool IsValidPart1(string password, char letter, int min, int max)
        {
            var count = password.Count(c => c == letter);
            return count >= min && count <= max;
        }

        public static bool IsValidPart2(string password, char letter, int min, int max)
        {
            return password[min - 1] == letter ^ password[max - 1] == letter;
        }
    }
}
