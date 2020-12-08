using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualBasic.CompilerServices;

namespace Day8
{
    public class Program
    {
        public static void Main(string[] args)
        {
            List<string> instructions = Utils.Utils.ReadLinesFromConsole().ToList();
            GameEngine game = new GameEngine(instructions);

            // Part 1
            game.FindInfiniteLoop();
            Console.WriteLine(game.Accumulator);

            // Part 2
            for (int i = 0; i < instructions.Count; i++)
            {
                game.ChangeInstruction(i, InvertNopJmp);
                if (!game.FindInfiniteLoop())
                {
                    Console.WriteLine($"Program terminates when modifying instruction {i}, current Accumulator value is {game.Accumulator}");
                }
                game.ChangeInstruction(i, InvertNopJmp);
            }
        }

        public static string InvertNopJmp(string instruction)
        {
            switch (instruction)
            {
                case "nop":
                    return "jmp";
                case "jmp":
                    return "nop";
                default:
                    return instruction;
            }
        }
    }
}
