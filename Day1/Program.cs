using System;
using System.Collections.Generic;

namespace Day1
{
    class Program
    {
        static void Main(string[] args)
        {
            string line = Console.ReadLine();
            HashSet<int> set = new HashSet<int>();
            while (!string.IsNullOrEmpty(line))
            {
                set.Add(int.Parse(line));
                line = Console.ReadLine();
            }

            foreach (int left in set)
            {
                foreach (int middle in set)
                {
                    if (set.Contains(2020 - left - middle))
                    {
                        Console.WriteLine($"{left} x {middle} x {2020 - left - middle} = {left * middle * (2020 - left - middle)}");
                    }
                }
            }
        }
    }
}
