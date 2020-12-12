using System;
using System.Collections.Generic;
using System.Linq;

namespace Day12
{
    class Program
    {
        static void Main(string[] args)
        {
            int x = 0;
            int y = 0;
            int waypointX = 10;
            int waypointY = 1;
            //int angle = 0;

            Utils.Utils.ReadLinesFromConsole()
                .ToList()
                .ForEach(instruction =>
                {
                    char direction = instruction[0];
                    int value = int.Parse(instruction.Substring(1));
                    switch (direction)
                    {
                        case 'N':
                            waypointY += value;
                            break;
                        case 'S':
                            waypointY -= value;
                            break;
                        case 'E':
                            waypointX += value;
                            break;
                        case 'W':
                            waypointX -= value;
                            break;
                        case 'L':
                        case 'R':
                            double angle = Math.Atan2(waypointY, waypointX);
                            angle += ConvertToRadians(value * (direction == 'L' ? 1 : -1));
                            double norm = Math.Sqrt(waypointX * waypointX + waypointY * waypointY);
                            waypointX = (int) Math.Round(norm * Math.Cos(angle), 0);
                            waypointY = (int) Math.Round(norm * Math.Sin(angle), 0);
                            break;
                        case 'F':
                            x += value * waypointX;
                            y += value * waypointY;
                            break;
                    }
                    // Console.WriteLine($"{instruction} : ||x={x}|| + ||y={y}|| = {Math.Abs(x) + Math.Abs(y)} (waypointX = {waypointX}, waypointY = {waypointY})");
                });

            Console.WriteLine($"||x={x}|| + ||y={y}|| = {Math.Abs(x) + Math.Abs(y)}");

        }


        public static double ConvertToRadians(double angle)
        {
            return (Math.PI / 180) * angle;
        }
    }
}
