using System;
using System.Collections.Generic;
using System.Linq;

namespace Day5
{
    public class Program
    {
        public static void Main()
        {
            string line = Console.ReadLine(); ;
            HashSet<int> seats = new HashSet<int>();
            do
            {
                int id = ComputeId(line);
                seats.Add(id);
                line = Console.ReadLine();
            } while (!string.IsNullOrEmpty(line));

            Console.WriteLine(Enumerable
                .Range(seats.Min() + 1, seats.Count - 2)
                .First(seat => !seats.Contains(seat)));
        }

        public static int ComputeId(string line)
        {
            return Convert.ToInt32(new string(line
                .Select(c => c == 'F' || c == 'L' ? '0' : '1')
                .ToArray()), 2);
        }
    }
}
