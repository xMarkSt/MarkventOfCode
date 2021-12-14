using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace AdventOfCode2021.Puzzles
{
    public class Day13 : AocPuzzle
    {
        public override int Year => 2021;
        public override int Day => 13;
        protected override void Solve(IEnumerable<string> input)
        {
            var foldInstructions = new List<string>();
            var dots = new List<Point>();
            foreach (string line in input)
            {
                if (line.Contains(','))
                {
                    string[] split = line.Split(',');
                    dots.Add(new Point(Convert.ToInt32(split[0]), Convert.ToInt32(split[1])));
                }
                else if(!string.IsNullOrEmpty(line))
                {
                    foldInstructions.Add(line);
                }
            }
            HashSet<Point> folded = new HashSet<Point>(dots);
            HashSet<Point> foldedFirst = null;
            foreach (string foldInstruction in foldInstructions)
            {
                if (foldInstruction.Contains('x'))
                {
                    string[] split = foldInstruction.Split("fold along x=");
                    int foldLine = Convert.ToInt32(split[1]);
                    folded = FoldLeft(folded, foldLine);
                    if (foldedFirst == null)
                        foldedFirst = new HashSet<Point>(folded);
                }
                
                else if (foldInstruction.Contains('y'))
                {
                    string[] split = foldInstruction.Split("fold along y=");
                    int foldLine = Convert.ToInt32(split[1]);
                    folded = FoldUp(folded, foldLine);
                    if (foldedFirst == null)
                        foldedFirst = new HashSet<Point>(folded);
                }
            }
            
            PartOne = foldedFirst.Count.ToString();
            int minX = folded.Min(p => p.X);
            int maxX = folded.Max(p => p.X);
            int minY = folded.Min(p => p.Y);
            int maxY = folded.Max(p => p.Y);
            for (int y = minY; y <= maxY; y++)
            {
                for (int x = minX; x <= maxX; x++)
                {
                    if (folded.Contains(new Point(x, y)))
                    {
                        Console.Write("#");
                    }
                    else
                    {
                        Console.Write(".");
                    }
                }
                Console.WriteLine();
            }
        }

        private HashSet<Point> FoldUp(HashSet<Point> dots, int foldLine)
        {
            var folded = new HashSet<Point>();
            foreach (Point dot in dots)
            {
                if (dot.Y > foldLine)
                {
                    int offset = dot.Y - foldLine;
                    folded.Add(new Point( dot.X, foldLine - offset));
                }
                else
                {
                    folded.Add(new Point(dot.X, dot.Y));
                }
            }
            return folded;
        }
        private HashSet<Point> FoldLeft(HashSet<Point> dots, int foldLine)
        {
            var folded = new HashSet<Point>();
            foreach (Point dot in dots)
            {
                if (dot.X > foldLine)
                {
                    int offset = dot.X - foldLine;
                    folded.Add(new Point(foldLine - offset, dot.Y));
                }
                else
                {
                    folded.Add(new Point(dot.X, dot.Y));
                }
            }
            return folded;
        }
    }
}