using System;
using System.Collections.Generic;
using System.Linq;

namespace Day17
{
    class Program
    {
        static void Main(string[] args)
        {
            Dictionary<(int x, int y, int z, int w), int> currentState = new Dictionary<(int x, int y, int z, int w), int>();

            int y = 0;
            int z = 0;
            int w = 0;
            Utils.Utils.ReadLinesFromConsole()
                .ToList()
                .ForEach(line =>
                {
                    int x = 0;
                    line.ToList().ForEach(c =>
                    {
                        if (c == '#')
                        {
                            currentState[(x, y, z, w)] = 1;
                        }
                        x++;
                    });
                    y++;
                });

            for (int i = 0; i < 10000; i++)
            {
                int result = 0;
                foreach (var kvp in currentState)
                {
                    // Console.WriteLine($"x={kvp.Key.x} y={kvp.Key.y} z={kvp.Key.z} value = {kvp.Value}");
                    result += kvp.Value;
                }
                Console.WriteLine($"iteration {i} : {result}");
                currentState = Transform(currentState);
            }
        }
        
        public static Dictionary<(int x, int y, int z, int w), int> Transform(Dictionary<(int x, int y, int z, int w), int> currentDictionary)
        {
            Dictionary<(int x, int y, int z, int w), int> neighborsCounts = new Dictionary<(int x, int y, int z, int w), int>();
            Dictionary<(int x, int y, int z, int w), int> newState = new Dictionary<(int x, int y, int z, int w), int>();
            foreach (var cell in currentDictionary)
            {
                if (cell.Value == 1)
                {
                    for (int i = -1; i <= 1; i++)
                        for (int j = -1; j <= 1; j++)
                            for (int k = -1; k <= 1; k++)
                                for (int l = -1; l <= 1; l++)
                                    if (i != 0 || j != 0 || k != 0 || l != 0)
                                        if (neighborsCounts.ContainsKey((cell.Key.x + i, cell.Key.y + j, cell.Key.z + k, cell.Key.w + l)))
                                            neighborsCounts[(cell.Key.x + i, cell.Key.y + j, cell.Key.z + k, cell.Key.w + l)] += 1;
                                        else
                                            neighborsCounts[(cell.Key.x + i, cell.Key.y + j, cell.Key.z + k, cell.Key.w + l)] = 1;
                }
            }

            foreach (var neighborCount in neighborsCounts)
            {
                if ((neighborCount.Value == 3 && !currentDictionary.ContainsKey(neighborCount.Key)) ||
                    ((neighborCount.Value == 2 || neighborCount.Value == 3) && currentDictionary.ContainsKey(neighborCount.Key)))
                {
                    newState[neighborCount.Key] = 1;
                }
            }

            return newState;
        }
    }
}
