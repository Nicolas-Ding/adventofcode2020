using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Utils
{
    public class BaseMain
    {
        public static void Main(string[] args)
        {
        }

        public static void RunAndTime(Action aocProgram, int times = 1)
        {
            List<long> allTimes = new List<long>();
            long minTime = int.MaxValue;
            long maxTime = 0;
            Stopwatch stopwatch = new Stopwatch();

            for (int i = 0; i < times; i++)
            {
                stopwatch.Restart();
                aocProgram();
                long ellapsed = stopwatch.ElapsedMilliseconds;
                allTimes.Add(ellapsed);
                minTime = Math.Min(minTime, ellapsed);
                maxTime = Math.Max(maxTime, ellapsed);
            }

            Console.WriteLine();
            Console.WriteLine("################################################");
            Console.WriteLine($"Run the program {times} time.");
            Console.WriteLine($"Average time was {allTimes.Average()}ms");
            Console.WriteLine($"Minimum time was {minTime}ms.");
            Console.WriteLine($"Maximum time was {maxTime}ms.");
            Console.WriteLine("################################################");
            Console.WriteLine($"times were : {string.Join(",  ", allTimes.Select(_ => _+"ms"))}");
        }
    }
}
