using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Day11
{
    public class SeatLayout
    {
        public List<List<char>> Map { get; set; }

        public List<(int deltaY, int deltaX)> NeighborsDelta = new List<(int deltaY, int deltaX)>
        {
            (-1, -1), (-1, 0), (-1, 1),
            (0, -1), (0, 1),
            (1, -1), (1, 0), (1, 1),
        };

        public SeatLayout()
        {

        }

        public SeatLayout(List<string> input)
        {
            Map = input.Select(_ => _.ToList()).ToList();
        }

        public SeatLayout GetNextStep()
        {
            List<List<char>> newList = new List<List<char>>();

            for (int i = 0; i < Map.Count; i++)
            {
                newList.Add(new List<char>());
                for (int j = 0; j < Map[0].Count; j++)
                {
                    //int neighBorCount = CountNeighbors(i, j); // Part 1
                    int neighBorCount = CountVisibleNeighbors(i, j); // Part 2
                    if (Map[i][j] == '.')
                    {
                        newList[i].Add('.');
                    }
                    else if (neighBorCount == 0)
                    {
                        newList[i].Add('#');
                    }
                    else if (neighBorCount >= 5) // 4 for Part 1
                    {
                        newList[i].Add('L');
                    }
                    else
                    {
                        newList[i].Add(Map[i][j]);
                    }
                }
            }

            return new SeatLayout
            {
                Map = newList
            };
        }

        // Part 1
        public int CountNeighbors(int i, int j)
        {
            return NeighborsDelta.Aggregate(0, (i1, _) =>
                i1 + ((i + _.deltaY >= 0) && (i + _.deltaY < Map.Count) && (j + _.deltaX >= 0) && (j + _.deltaX < Map[0].Count)
                    ? (Map[i + _.deltaY][j + _.deltaX] == '#' ? 1 : 0)
                    : 0)
            );
        }

        // Part 2
        public int CountVisibleNeighbors(int i, int j)
        {
            return NeighborsDelta.Aggregate(0, (i1, _) =>
                {
                    int currentDeltaY = _.deltaY;
                    int currentDeltaX = _.deltaX;

                    while ((i + currentDeltaY >= 0) && (i + currentDeltaY < Map.Count) && (j + currentDeltaX >= 0) && (j + currentDeltaX < Map[0].Count))
                    {
                        switch (Map[i + currentDeltaY][j + currentDeltaX])
                        {
                            case '#':
                                return i1 + 1;
                            case 'L':
                                return i1;
                        }

                        currentDeltaX += _.deltaX;
                        currentDeltaY += _.deltaY;
                    }

                    return i1; //We saw no seats, so no occupied seats
                }
            );
        }

        public int CountSeats()
        {
            return Map.Aggregate(0, (i, list) => i + list.Count(_ => _ == '#'));
        }

        public override string ToString()
        {
            return string.Join('\n', Map.Select(_ => new string(_.ToArray())));
        }

        public override int GetHashCode()
        {
            return Map.Aggregate(0, (i, list) => 3 * i + list.Aggregate(0, (i1, c) => 2 * i1 + c));
        }

        public override bool Equals(object? obj)
        {
            if (!(obj is SeatLayout)) return false;

            SeatLayout layout = (SeatLayout) obj;

            for (int i = 0; i < Map.Count; i++)
            {
                for (int j = 0; j < Map[0].Count; j++)
                {
                    if (Map[i][j] != layout.Map[i][j])
                    {
                        return false;
                    }
                }
            }

            return true;
        }
    }
}
