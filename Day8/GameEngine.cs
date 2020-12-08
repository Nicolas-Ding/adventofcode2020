using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Day8
{
    public class GameEngine
    {
        public List<(string instruction, int parameter)> Instructions;
        public int Accumulator;
        public int CurrentLine;
        public GameEngine(List<string> instructions)
        {
            Instructions = instructions.Select(_ =>
            {
                string[] instruction = _.Split(' ');
                return (instruction[0], int.Parse(instruction[1]));
            }).ToList();
        }

        public bool FindInfiniteLoop()
        {
            HashSet<int> visited = new HashSet<int>();
            Accumulator = 0;
            CurrentLine = 0;
            while (!visited.Contains(CurrentLine))
            {
                visited.Add(CurrentLine);
                if (ProcessLine())
                {
                    // The program finished, so no infinite loop
                    return false;
                }
            }
            return true;
        }

        // returns a boolean if the program is finished
        public bool ProcessLine()
        {
            if (CurrentLine >= Instructions.Count)
            {
                return true;
            }

            var instruction = Instructions[CurrentLine];
            switch (instruction.instruction)
            {
                case "nop":
                    CurrentLine++;
                    break;
                case "acc":
                    Accumulator += instruction.parameter;
                    CurrentLine++;
                    break;
                case "jmp":
                    CurrentLine += instruction.parameter;
                    break;
            }

            return false;
        }

        // Changes instruction at lineNumber to newInstruction. No-op if originalInstruction is specified and doesn't match
        public void ChangeInstruction(int lineNumber, Func<string, string> changeInstruction)
        {
            if (lineNumber > Instructions.Count)
            {
                return;
            }

            Instructions[lineNumber] = (changeInstruction(Instructions[lineNumber].instruction), Instructions[lineNumber].parameter);
        }
    }
}
