using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using MoreLinq.Extensions;

// ReSharper disable AssignNullToNotNullAttribute
// ReSharper disable PossibleNullReferenceException

namespace Day13
{
    class Program
    {
        static void Main(string[] args)
        {
            int start = int.Parse(Console.ReadLine());
            string input = Console.ReadLine();

            // Part 1
            int earliestBus = input
                .Split(',')
                .Where(_ => _ != "x")
                .Select(int.Parse)
                .MinBy(_ => _ * (int)Math.Ceiling((double)start / _))
                .Min();
            Console.WriteLine($"ID : {earliestBus}, Result : {earliestBus * (earliestBus * (int)Math.Ceiling((double)start / earliestBus) - start)}");

            // Part 2
            List<string> ids = input
                .Split(',')
                .ToList();

            long result = int.Parse(ids[0]);
            long factor = result;
            for (int i = 1; i < ids.Count; i++)
            {

                if (!int.TryParse(ids[i], out int id))
                {
                    continue;
                }
                Console.WriteLine($"current id : {id}, current factor : {factor}, current result : {result}");

                while (result % id != GetPositive(-i, id))
                {
                    result += factor;
                }

                factor *= id;
            }
            Console.WriteLine(result);
        }

        static int GetPositive(int i, int step)
        {
            int result;
            for (result = i; result < 0; result += step)
            {

            }
            return result;
        }
    }
}
