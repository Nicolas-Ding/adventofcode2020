using System;
using System.Collections;
using System.Collections.Generic;

namespace Utils
{
    public class Utils
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }

        public static IEnumerable<string> ReadLinesFromConsole()
        {
            string line = Console.ReadLine();
            while (!string.IsNullOrEmpty(line))
            {
                yield return line;
                line = Console.ReadLine();
            }
        }
    }
}
