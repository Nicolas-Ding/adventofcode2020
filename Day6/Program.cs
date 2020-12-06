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
            IEnumerable<char> intersectChars;
            IEnumerable<char> unionChars;

            do
            {
                unionChars = new List<char>();
                intersectChars = Enumerable.Range('a', 26).Select(_ => (char)_).ToList();
                Utils.Utils.ReadLinesFromConsole()
                    .ToList()
                    .ForEach(_ =>
                    {
                        unionChars = _.Union(unionChars);
                        intersectChars = _.Intersect(intersectChars);
                    });
                unionCount += unionChars.Count();
                intersectCount += intersectChars.Count();
            } while (unionChars.Any());

            Console.WriteLine($"Union : {unionCount}");
            Console.WriteLine($"Intersect : {intersectCount - 26}");
        }
    }
}
