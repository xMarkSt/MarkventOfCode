using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Numerics;
using System.Text;
using AdventOfCode2021.Datastructures;
using AdventOfCode2021.Utils;

namespace AdventOfCode2021.Puzzles
{
    public class Day15 : AocPuzzle
    {
        public override int Year => 2021;
        public override int Day => 15;

        protected override void Solve(IEnumerable<string> input)
        {
            Graph graph = new Graph();
            List<string> risks = input.ToList();
            int maxX = risks[0].Length * 5;
            int maxY = risks.Count * 5;
            for (int y = 0; y < maxY; y++)
            {
                for (int x = 0; x < maxX; x++)
                {
                    graph.AddVertex(new Point(x, y).ToSimpleString());
                    Point[] directions =
                        { new Point(1, 0), new Point(0, 1), new Point(-1, 0), new Point(0, -1) };
                    foreach (Point direction in directions)
                    {
                        Point newPoint = new Point(x + direction.X, y + direction.Y);
                        if (newPoint.X >= 0 && newPoint.Y >= 0 &&
                            newPoint.X < maxX &&
                            newPoint.Y < maxY)
                        {
                            int extraX = newPoint.X / risks[0].Length;
                            int extraY = newPoint.Y / risks.Count;
                            double risk = Convert.ToDouble(risks[newPoint.Y % risks.Count][newPoint.X % risks[0].Length]
                                .ToString()) + extraX + extraY;
                            if(risk % 9 == 0) {}
                            risk %= 9;
                            if (risk == 0) risk = 9;
                            graph.AddEdge($"{x},{y}", newPoint.ToSimpleString(),
                                risk);
                        }
                    }
                }
            }

            graph.Dijkstra("0,0");
            Vertex end =
                graph.GetVertex(new Point(maxX - 1, maxY - 1)
                    .ToSimpleString());
            PartTwo = end.distance.ToString(CultureInfo.InvariantCulture);
        }
    }
}