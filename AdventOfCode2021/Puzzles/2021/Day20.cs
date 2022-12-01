using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace AdventOfCode2021.Puzzles
{
    public class Day20 : AocPuzzle
    {
        public override int Year => 2021;
        public override int Day => 20;

        private int minX, maxX, minY, maxY;
        protected override void Solve(IEnumerable<string> input)
        {
            List<string> inputList = input.ToList();
            string algo = inputList[0];

            var pixelsSet = new HashSet<Point>();
            
            maxY = inputList.Count - 3;
            maxX = inputList[2].Length - 1;
            for (int y = 2; y < inputList.Count; y++)
            {
                for (int x = 0; x < inputList[y].Length; x++)
                {
                    if (inputList[y][x] == '#')
                    {
                        pixelsSet.Add(new Point(x, y - 2));
                    }
                }
            }

            int bMinX = minX;
            int bMinY = minY;
            int bMaxX = maxX;
            int bMaxY = maxY;
            for (int i = 1; i <= 50; i++)
            {
                var copy = new HashSet<Point>(pixelsSet);
                minY -= 2;
                minX -= 2;
                maxX += 2;
                maxY += 2;
                bMinX--;
                bMinY--;
                bMaxX++;
                bMaxY++;
                for (int y = minY; y <= maxY; y++)
                {
                    for (int x = minX; x <= maxX; x++)
                    {
                        var directions = new List<(int x, int y)>
                            { (-1, -1), (0, -1), (1, -1), (-1, 0), (0, 0), (1, 0), (-1, 1), (0, 1), (1, 1) };
                        string binary = "";
                        foreach (var direction in directions)
                        {
                            var near = new Point(x + direction.x, y + direction.y);
                            if (near.X < bMinX || near.X > bMaxX || near.Y < bMinY || near.Y > bMaxY)
                            {
                                if (i % 2 == 0) binary += '1';
                                else binary += '0';
                            }
                            else
                            {
                                if (pixelsSet.Contains(near))
                                {
                                    binary += '1';
                                }
                                else
                                {
                                    binary += '0';
                                }
                            }
                        }
                        int index = (int)Utils.ConversionUtils.Bin2Int(binary);
                        if (algo[index] == '#')
                        {
                            copy.Add(new Point(x, y));
                        }
                        else
                        {
                            copy.Remove(new Point(x, y));
                        }
                    }
                }

                pixelsSet = copy;
            }
            // PrintPixels(pixelsSet);
            PartTwo = pixelsSet.Count.ToString();
        }

        private void PrintPixels(HashSet<Point> pixels)
        {
            for (int y = minY; y <= maxY; y++)
            {
                for (int x = minX; x <= maxX; x++)
                {
                    if (pixels.Contains(new Point(x, y)))
                    {
                        Console.Write('#');
                    }
                    else
                    {
                        Console.Write('.');
                    }
                }

                Console.WriteLine();
            }
        }
    }
}