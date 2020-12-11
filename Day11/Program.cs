using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualBasic.CompilerServices;

namespace Day11
{
    class Program
    {
        static void Main(string[] args)
        {
            SeatLayout layout = new SeatLayout(Utils.Utils.ReadLinesFromConsole().ToList());
            HashSet<SeatLayout> layouts = new HashSet<SeatLayout>();

            while (!layouts.Contains(layout))
            {
                layouts.Add(layout);
                layout = layout.GetNextStep();
            }

            Console.WriteLine(layout.CountSeats());
        }
    }
}
