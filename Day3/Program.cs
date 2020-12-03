using System;
using System.Collections.Generic;
using System.Linq;

namespace Day3
{
    class Program
    {
        static void Main(string[] args)
        {
            string line = Console.ReadLine();
            List<string> map = new List<string>();
            while (!string.IsNullOrEmpty(line))
            {
                map.Add(line);
                line = Console.ReadLine();
            }

            Console.WriteLine(Solve(map, 1, 1)
                              * Solve(map, 3, 1)
                              * Solve(map, 5, 1)
                              * Solve(map, 7, 1)
                              * Solve(map, 1, 2));
        }

        public static long Solve(List<string> map, int rightSlope = 3, int downSlope = 1)
        {
            return map
                .Where((line, index) => index % downSlope == 0 && line[(rightSlope * (index / downSlope)) % line.Length] == '#')
                .Count();
        }
    }
}
