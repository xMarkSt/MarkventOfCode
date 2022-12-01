using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using AdventOfCode2021.Puzzles;

public class Day05 : AocPuzzle
{
    public override int Year => 2021;
    public override int Day => 5;
    protected override void Solve(IEnumerable<string> input)
    {
        var lines = input
            .Select(x => {
                string[] a = x.Split(" -> ");
                Point[] points = a.Select(s =>
                {
                    var w = s.Split(',');
                    return new Point(int.Parse(w[0]), int.Parse(w[1]));
                }).ToArray();
                return new Line(points[0], points[1]);
            })
        .ToList();

        Dictionary<Point, int> counts1 = new Dictionary<Point, int>();
        foreach (Line line in lines)
        {
            foreach (Point point in line.GetPointsBetween())
            {
                if (counts1.ContainsKey(point))
                {
                    counts1[point]++;
                }

                else
                {
                    counts1.Add(point, 1);
                }
            }
        }
        
        Dictionary<Point, int> counts2 = new Dictionary<Point, int>();

        foreach (Line line in lines)
        {
            foreach (Point point in line.GetPointsBetweenWithDiagonal())
            {
                if (counts2.ContainsKey(point))
                {
                    counts2[point]++;
                }

                else
                {
                    counts2.Add(point, 1);
                }
            }
        }

        PartOne = counts1.Count(x => x.Value > 1).ToString();
        PartTwo = counts2.Count(x => x.Value > 1).ToString();
    }
}

internal class Line
{
    public Point A { get; set; }
    public Point B { get; set; }

    public Line(Point a, Point b)
    {
        A = a;
        B = b;
    }

    public List<Point> GetPointsBetween()
    {
        List<Point> pointlist = new List<Point>();
        if (A.X == B.X)
        {
            int minY = Math.Min(A.Y, B.Y);
            int maxY = Math.Max(A.Y, B.Y);
            for (int i = minY; i <= maxY; i++)
            {
                pointlist.Add(new Point(A.X, i));
            }
        }
        
        else if (A.Y == B.Y)
        {
            int minX = Math.Min(A.X, B.X);
            int maxX = Math.Max(A.X, B.X);
            for (int i = minX; i <= maxX; i++)
            {
                pointlist.Add(new Point(i, A.Y));
            }
        }

        return pointlist;
    }
    
    public List<Point> GetPointsBetweenWithDiagonal()
    {
        List<Point> pointlist = GetPointsBetween();
        if(A.X != B.X && A.Y != B.Y)
        {
            int length = Math.Abs(B.Y - A.Y);

            Point direction = new Point(1, 1);
            if (A.Y < B.Y) direction.Y = -1;
            if (A.X < B.X) direction.X = -1;
            var point = B;
            for (int i = 0; i <= length; i++)
            {
                pointlist.Add(point);
                point.X += direction.X; 
                point.Y += direction.Y; 
            }
        }

        return pointlist;
    }
}