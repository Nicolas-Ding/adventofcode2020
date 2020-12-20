using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Day20
{
    public enum Side
    {
        Top, Left, Right, Bottom
    }

    public class Tile
    {
        public int TileNumber { get; set; }

        public List<string> TileData { get; set; }

        public HashSet<string> PossibleBorders { get; }

        public HashSet<string> NonMatchingBorder { get; set; }

        public string TopBorder { get;  }

        public string TopBorderReversed { get; }

        public string BottomBorder { get; }

        public string BottomBorderReversed { get; }

        public string LeftBorder { get; }

        public string LeftBorderReversed { get; }

        public string RightBorder { get; }

        public string RightBorderReversed { get; }

        public Tile(List<string> tileData, int tileNumber)
        {
            TileData = tileData;
            TileNumber = tileNumber;
            NonMatchingBorder = new HashSet<string>();

            PossibleBorders = new HashSet<string>();

            TopBorder = TileData[0];
            PossibleBorders.Add(TopBorder);
            TopBorderReversed = Reverse(TopBorder);
            PossibleBorders.Add(TopBorderReversed);

            BottomBorder = TileData[^1];
            PossibleBorders.Add(BottomBorder);
            BottomBorderReversed = Reverse(BottomBorder);
            PossibleBorders.Add(BottomBorderReversed);

            LeftBorder = new string(TileData.Select(_ => _[0]).ToArray());
            PossibleBorders.Add(LeftBorder);
            LeftBorderReversed = Reverse(LeftBorder);
            PossibleBorders.Add(LeftBorderReversed);

            RightBorder = new string(TileData.Select(_ => _[^1]).ToArray());
            PossibleBorders.Add(RightBorder);
            RightBorderReversed = Reverse(RightBorder);
            PossibleBorders.Add(RightBorderReversed);
        }

        public static string Reverse(string s)
        {
            char[] charArray = s.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }

        public string GetOppositeBorder(string s)
        {
            if (s == TopBorder)
                return BottomBorder;
            if (s == TopBorderReversed)
                return BottomBorderReversed;
            if (s == BottomBorder)
                return TopBorder;
            if (s == BottomBorderReversed)
                return TopBorderReversed;
            if (s == RightBorder)
                return LeftBorder;
            if (s == RightBorderReversed)
                return LeftBorderReversed;
            if (s == LeftBorder)
                return RightBorder;
            if (s == LeftBorderReversed)
                return RightBorderReversed;
            throw new Exception($"Tile {TileNumber} does not contains border {s} in any direction");
        }

        public (string, string) GetTwoNonMatchingBorder()
        {
            // This method only works if the Tile is a corner
            // and if the nonmatching tiles hashset has been filled
            string leftBorder = NonMatchingBorder.First();
            string bottomBorder = NonMatchingBorder.First(_ => _ != leftBorder && _ != Reverse(leftBorder));
            return (leftBorder, bottomBorder);
        }

        public List<string> GetTileWithUpperBorder(string upperBorder)
        {
            if (upperBorder == TopBorder)
            {
                return TileData;
            }
            if (upperBorder == LeftBorderReversed)
            {
                return RotateClockwise(TileData);
            }
            if (upperBorder == BottomBorderReversed)
            {
                return RotateClockwise(RotateClockwise(TileData));
            }
            if (upperBorder == RightBorder)
            {
                return RotateClockwise(RotateClockwise(RotateClockwise(TileData)));
            }
            if (upperBorder == TopBorderReversed)
            {
                return VerticalSymmetry(TileData);
            }
            if (upperBorder == LeftBorder)
            {
                return VerticalSymmetry(RotateClockwise(TileData));
            }
            if (upperBorder == BottomBorder)
            {
                return VerticalSymmetry(RotateClockwise(RotateClockwise(TileData)));
            }
            if (upperBorder == RightBorderReversed)
            {
                return VerticalSymmetry(RotateClockwise(RotateClockwise(RotateClockwise(TileData))));
            }
            throw new ArgumentException(nameof(upperBorder));
        }

        public List<string> GetTileWithLeftBorder(string leftBorder)
        {
            if (leftBorder == LeftBorder)
            {
                return TileData;
            }
            if (leftBorder == BottomBorder)
            {
                return RotateClockwise(TileData);
            }
            if (leftBorder == RightBorderReversed)
            {
                return RotateClockwise(RotateClockwise(TileData));
            }
            if (leftBorder == TopBorderReversed)
            {
                return RotateClockwise(RotateClockwise(RotateClockwise(TileData)));
            }
            if (leftBorder == LeftBorderReversed)
            {
                return VerticalSymmetry(RotateClockwise(RotateClockwise(TileData)));
            }
            if (leftBorder == BottomBorderReversed)
            {
                return RotateClockwise(VerticalSymmetry(TileData));
            }
            if (leftBorder == RightBorder)
            {
                return VerticalSymmetry(TileData);
            }
            if (leftBorder == TopBorder)
            {
                return VerticalSymmetry(RotateClockwise(TileData));
            }

            throw new ArgumentException(nameof(leftBorder));
        }

        public List<string> VerticalSymmetry(List<string> tileData)
        {
            return tileData.Select(_ => Reverse(_)).ToList();
        }

        public List<string> RotateClockwise(List<string> tileData)
        {
            List<string> result = new List<string>();
            for (int i = 0; i < tileData.Count; i++)
            {
                StringBuilder builder = new StringBuilder();
                for (int j = tileData[i].Length - 1; j >= 0; j--)
                {
                    builder.Append(tileData[j][i]);
                }
                result.Add(builder.ToString());
            }
            return result;
        }
    }
}
