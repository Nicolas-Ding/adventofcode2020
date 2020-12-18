using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace Day18
{
    class Program
    {
        static void Main(string[] args)
        {
            long sum = 
                Utils.Utils.ReadLinesFromConsole()
                .Select(line =>
                {
                    line = line.Replace("(", "((");
                    line = line.Replace(")", "))");
                    line = line.Replace(" * ", ") * (");
                    return "(" + line + ")";
                })
                .Aggregate((long)0, (i, line) => i + Calculate(0, line));

            Console.WriteLine($"sum : {sum}");
        }

        static long ComputeNext(long result, string line, Func<long, long, long> op, int skip = 2, int spaceAfter = 1)
        {
            switch (line[skip])
            {
                case '(':
                    int closingParenthesisIndex = FindClosingParenthesisIndex(line);
                    result = op(result, Calculate(0, line.Substring(skip + spaceAfter, closingParenthesisIndex - skip - spaceAfter))); 
                    return line.Length > closingParenthesisIndex + 2 ? Calculate(result, line.Substring(closingParenthesisIndex + 2)) : result;
                default: // should be a number
                    result = op(result, (long)char.GetNumericValue(line[skip]));
                    return line.Length > skip + 2 ? Calculate(result, line.Substring(skip + 2)) : result;
            }
        }

        static long Calculate(long result, string line)
        {
            line = line.Trim();

            switch (line[0])
            {
                case '(':
                    return ComputeNext(result, line, (a, b) => b, skip: 1, spaceAfter: 0);
                case '+':
                    return ComputeNext(result, line, (a, b) => a + b);
                case '*':
                    return ComputeNext(result, line, (a, b) => a * b);
                default: // should be a number
                    result = (long)char.GetNumericValue(line[0]);
                    return line.Length > 2 ? Calculate(result, line.Substring(2)) : result;
            }
        }

        static int FindClosingParenthesisIndex(string line)
        {
            int depth = -1;
            for (int i = 0; i < line.Length; i++)
            {
                if (line[i] == '(')
                {
                    depth++;
                }
                if (line[i] == ')')
                {
                    if (depth == 0)
                    {
                        return i;
                    }
                    depth--;
                }
            }
            return -1; // should never happen
        }
    }
}
