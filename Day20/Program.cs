using System;
using System.Collections.Generic;
using System.Linq;

namespace Day20
{
    class Program
    {

        static List<Tile> tiles = new List<Tile>();

        static void Main(string[] args)
        {
            string line = Console.ReadLine();
            while (line.StartsWith("Tile"))
            {
                tiles.Add(new Tile(Utils.Utils.ReadLinesFromConsole().ToList(), int.Parse(line.Substring(5, 4))));
                line = Console.ReadLine();
            }

            int N = (int)Math.Sqrt(tiles.Count);
            int tileSize = tiles.First().TileData.Count();

            long res = 1;
            HashSet<Tile> corners = new HashSet<Tile>();
            foreach (Tile tile in tiles)
            {
                int count = 0;
                foreach (string border in tile.PossibleBorders)
                {
                    if (!tiles.Any(_=> _.TileNumber != tile.TileNumber && _.PossibleBorders.Contains(border)))
                    {
                        count++;
                        tile.NonMatchingBorder.Add(border);
                    }
                }
                if (count == 4)
                {
                    Console.WriteLine($"{tile.TileNumber} is in a corner");
                    res *= tile.TileNumber;
                    corners.Add(tile);
                }
            }
            Console.WriteLine($"Part 1 : {res}");

            // Part 2, let's construct the real image
            bool shouldReverse = false;
            start:
            List<List<char>> fullImage = new List<List<char>>();
            List<List<int>> fullImageReferences = new List<List<int>>();

            for (int a = 0; a < N * tileSize; a++)
            {
                fullImage.Add(new List<char>());
                fullImageReferences.Add(new List<int>());
                for (int b = 0; b < N * tileSize; b++)
                {
                    fullImage[a].Add('-');
                    fullImageReferences[a].Add(-1);
                }
            }

            Tile currentTile = corners.First();
            int x = 0;
            string currentHorizontalBorder = currentTile.NonMatchingBorder.First();
            if (shouldReverse)
            {
                currentHorizontalBorder = Tile.Reverse(currentHorizontalBorder);
            }
            string currentverticalBorder;

            for (int y = 0; y < N * tileSize; y += tileSize)
            {
                x = 0;
                currentTile = FindNextTileWithBorder(currentHorizontalBorder, y == 0 ? -1 : fullImageReferences[y-1][0]);
                Console.WriteLine($"current horizontal border : {currentHorizontalBorder}");
                FillImage(fullImage, y, x, currentTile.GetTileWithUpperBorder(currentHorizontalBorder));
                FillImageReferences(fullImageReferences, y, x, currentTile);
                for (x = tileSize; x < N * tileSize; x += tileSize)
                {
                    currentverticalBorder = "";
                    for (int i = 0; i < tileSize; i++)
                    {
                        currentverticalBorder += fullImage[y + i][x - 1];
                    }
                    Console.WriteLine($"current vertical border : {currentverticalBorder}");
                    currentTile = FindNextTileWithBorder(currentverticalBorder, fullImageReferences[y][x - 1]);
                    if (currentTile == null)
                    {
                        shouldReverse = true;
                        Console.WriteLine("Reversing...");
                        goto start;
                    }
                    FillImage(fullImage, y, x, currentTile.GetTileWithLeftBorder(currentverticalBorder));
                    FillImageReferences(fullImageReferences, y, x, currentTile);
                }
                currentHorizontalBorder = "";
                for (int i = 0; i < tileSize; i++)
                {
                    currentHorizontalBorder += fullImage[y + tileSize - 1][i];
                }
            }

            foreach (List<char> l in fullImage)
            {
                Console.WriteLine(new string(l.ToArray()));
            }
            //Console.WriteLine();
            //foreach (List<int> l in fullImageReferences)
            //{
            //    Console.WriteLine(string.Join(" ", l));
            //}

            // Get the reduced image
            List<string> reducedImage = new List<string>();
            for (int a = 0; a < N; a++)
            {
                for (int i = 1; i < tileSize - 1; i++)
                {
                    string currentString = "";
                    for (int b = 0; b < N; b++)
                    {
                        for (int j = 1; j < tileSize - 1; j++)
                        {
                            currentString += fullImage[a * tileSize + i][b * tileSize + j];
                        }
                    }
                    reducedImage.Add(currentString);
                }
            }

            Tile finalTile = new Tile(reducedImage, 0);

            for (int i = 0; i < finalTile.TileData.Count; i++)
            {
                Console.WriteLine(finalTile.TileData[i]);
            }

            Console.WriteLine(CountResult(finalTile.TileData));
            Console.WriteLine(CountResult(finalTile.RotateClockwise(finalTile.TileData)));
            Console.WriteLine(CountResult(finalTile.RotateClockwise(finalTile.RotateClockwise(finalTile.TileData))));
            Console.WriteLine(CountResult(finalTile.RotateClockwise(finalTile.RotateClockwise(finalTile.RotateClockwise(finalTile.TileData)))));
            Console.WriteLine(CountResult(finalTile.VerticalSymmetry(finalTile.TileData)));
            //Console.WriteLine();
            //List<string> rotated = finalTile.VerticalSymmetry(finalTile.RotateClockwise(finalTile.TileData));
            //for (int i = 0; i < rotated.Count; i++)
            //{
            //    Console.WriteLine(rotated[i]);
            //}

            Console.WriteLine(CountResult(finalTile.VerticalSymmetry(finalTile.RotateClockwise(finalTile.TileData))));
            Console.WriteLine(CountResult(finalTile.VerticalSymmetry(finalTile.RotateClockwise(finalTile.RotateClockwise(finalTile.TileData)))));
            Console.WriteLine(CountResult(finalTile.VerticalSymmetry(finalTile.RotateClockwise(finalTile.RotateClockwise(finalTile.RotateClockwise(finalTile.TileData))))));


        }

        static int CountResult(List<string> image)
        {
            string line1 = "..................#.";
            string line2 = "#....##....##....###";
            string line3 = ".#..#..#..#..#..#...";

            int count = 0;
            for (int i = 0; i < image.Count() - 2; i++)
            {
                for (int j = 0; j < image[i].Length - 20; j++)
                {
                    if (image[i][j + 18] == '#' &&
                        image[i+1][j] == '#' &&
                        image[i+1][j + 5] == '#' &&
                        image[i + 1][j + 6] == '#' &&
                        image[i + 1][j + 11] == '#' &&
                        image[i + 1][j + 12] == '#' &&
                        image[i + 1][j + 17] == '#' &&
                        image[i + 1][j + 18] == '#' &&
                        image[i + 1][j + 19] == '#' &&
                        image[i + 2][j + 1] == '#' &&
                        image[i + 2][j + 4] == '#' &&
                        image[i + 2][j + 7] == '#' &&
                        image[i + 2][j + 10] == '#' &&
                        image[i + 2][j + 13] == '#' &&
                        image[i + 2][j + 16] == '#')
                    {
                        count++;
                    }
                }
            }
            return image.Sum(_ => _.Count(_ => _ == '#')) - count * 15;
        }

        static Tile FindNextTileWithBorder(string border, int tileNumber)
        {
            return tiles.FirstOrDefault(_ => _.TileNumber != tileNumber && _.PossibleBorders.Contains(border));
        }

        static void FillImage(List<List<char>> bigImage, int y, int x, List<string> smallImage)
        {
            for (int i = 0; i < smallImage.Count; i++)
            {
                for (int j = 0; j < smallImage[i].Length; j++)
                {
                    bigImage[i + y][j + x] = smallImage[i][j];
                }
            }
        }

        static void FillImageReferences(List<List<int>> bigImage, int y, int x, Tile tile)
        {
            for (int i = 0; i < tile.TileData.Count; i++)
            {
                for (int j = 0; j < tile.TileData[i].Length; j++)
                {
                    bigImage[i + y][j + x] = tile.TileNumber;
                }
            }
        }
    }
}
