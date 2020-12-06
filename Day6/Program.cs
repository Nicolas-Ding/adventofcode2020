using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Day6
{
    public class Program
    {
        public static void Main(string[] args)
        {
            int unionCount = 0;
            int intersectCount = 0;
            string unionChars = "";

            do
            {
                var inputs = Utils.Utils.ReadLinesFromConsole().ToList();

                unionChars = inputs.Aggregate("", (prev, next) => new string(prev.Union(next).ToArray()));
                intersectCount += inputs.Aggregate(unionChars, (prev, next) => new string(prev.Intersect(next).ToArray())).Length;

                unionCount += unionChars.Length;
            } while (unionChars.Length > 0);

            Console.WriteLine($"Union : {unionCount}");
            Console.WriteLine($"Intersect : {intersectCount}");
        }
    }
}
