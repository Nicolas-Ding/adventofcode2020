using System;
using System.Collections.Generic;

namespace Day15
{
    class Program
    {
        static void Main(string[] args)
        {
            string line = Console.ReadLine();
            string[] input = line.Split(',');
            Dictionary<int, int> gameHistory = new Dictionary<int, int>();
            int turn = 0;

            // Init
            for (turn = 0; turn < input.Length - 1; turn++)
            {
                gameHistory[int.Parse(input[turn])] = turn;
            }

            int lastNumber = int.Parse(input[^1]);

            while (turn < 30000000 - 1)
            {
                if (!gameHistory.ContainsKey(lastNumber))
                {
                    gameHistory[lastNumber] = turn;
                    lastNumber = 0;
                }
                else
                {
                    int newNumber = turn - gameHistory[lastNumber];
                    gameHistory[lastNumber] = turn;
                    lastNumber = newNumber;
                }
                turn++;
            }
            Console.WriteLine(lastNumber);
        }
    }
}
