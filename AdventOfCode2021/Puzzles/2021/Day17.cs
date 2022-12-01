using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;

namespace AdventOfCode2021.Puzzles
{
    public class Day17 : AocPuzzle
    {
        public override int Year => 2021;
        public override int Day => 17;
        protected override void Solve(IEnumerable<string> input)
        {
            string[] targetList = input.First().Split(',');
            var xSplit = targetList[0].Split("..");
            var ySplit = targetList[1].Split("..");
            int x1 = Convert.ToInt32(xSplit[0].Split("target area: x=")[1]);
            int x2 = Convert.ToInt32(xSplit[1]);
            int y1 = Convert.ToInt32(ySplit[0].Split("y=")[1]);
            int y2 = Convert.ToInt32(ySplit[1]);
            Point probe = Point.Empty;
            List<int> highest = new List<int>();
            var distinct = new HashSet<Vector2>();
            for (int veloX = -250; veloX < 250; veloX++)
            {
                for (int veloY = -250; veloY < 250; veloY++)
                {
                    probe = Point.Empty;
                    Vector2 velocity = new Vector2(veloX, veloY);
                    int maxY = Int32.MinValue;
                    for (int i = 0; i < 1000; i++)
                    {
                        probe.X += (int)velocity.X;
                        probe.Y += (int)velocity.Y;
                        if (probe.Y > maxY) maxY = probe.Y;
                        if (probe.Y >= y1 && probe.Y <= y2 && probe.X >= x1 && probe.X <= x2)
                        {
                            highest.Add(maxY);
                            distinct.Add(new Vector2(veloX, veloY));
                        }
                        if (velocity.X > 0)
                        {
                            velocity.X--;
                        }
                        else if(velocity.X < 0)
                        {
                            velocity.X++;
                        }

                        velocity.Y--;
                        if(probe.Y < y1) break;
                    }
                }
            }

            PartOne = highest.Max().ToString();
            PartTwo = distinct.Count.ToString();
        }
    }
}