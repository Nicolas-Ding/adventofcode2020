using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace Utils
{
    public class Utils
    {
        public static IEnumerable<string> ReadLinesFromConsole()
        {
            string line = Console.ReadLine();
            while (!string.IsNullOrEmpty(line))
            {
                yield return line;
                line = Console.ReadLine();
            }
        }

        public static IEnumerable<string> ReadLinesFromFile(string filename)
        {
            using StreamReader file = new StreamReader(filename);
            string line = file.ReadLine();
            while (!string.IsNullOrEmpty(line))
            {
                yield return line;
                line = file.ReadLine();
            }
        }
    }
}
