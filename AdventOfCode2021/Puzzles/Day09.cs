using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;

namespace AdventOfCode2021.Puzzles
{
    public class Day09 : AocPuzzle
    {
        public override int Year => 2021;
        public override int Day => 9;

        private List<Point> _lowPoints;
        private List<List<int>> _numbers;
        protected override void Solve(IEnumerable<string> input)
        {
            _lowPoints = new List<Point>();
            _numbers = input.Select(x => x.Select(x => int.Parse(x.ToString())).ToList()).ToList();
            int sum = 0;
            for (int row = 0; row < _numbers.Count; row++)
            {
                var line = _numbers[row];
                for (int i = 0; i < line.Count; i++)
                {
                    bool leftEdgeOrLow = i == 0 || line[i - 1] > line[i];
                    bool topEdgeOrLow = row == 0 || line[i] < _numbers[row-1][i];
                    bool rightEdgeOrLow = i == line.Count - 1 || line[i] < line[i + 1];
                    bool bottomEdgeOrLow = row == _numbers.Count - 1 || line[i] < _numbers[row + 1][i];
                        if ( leftEdgeOrLow && topEdgeOrLow && rightEdgeOrLow && bottomEdgeOrLow )
                    {
                        sum += line[i] + 1;
                        _lowPoints.Add(new Point(i, row));
                    }
                }
            }

            PartOne = sum.ToString();

            List<int> basins = _lowPoints.Select(lowPoint => Flow(lowPoint).Count + 1).OrderByDescending(x => x).ToList();
            PartTwo = (basins[0] * basins[1] * basins[2]).ToString();
        }

        private HashSet<Point> Flow(Point point)
        {

            List<Point> neighbors = GetNeighbors(point);
            var points = new HashSet<Point>(neighbors);
            foreach (Point neighbor in neighbors)
            {
                points.UnionWith(Flow(neighbor));
            }

            return points;
        }

        private List<Point> GetNeighbors(Point point)
        {
            var directions = new List<Point> { new Point(-1, 0), new Point(0, -1), new Point(1, 0), new Point(0, 1) };
            var neighbors = new List<Point>();
            foreach (Point direction in directions)
            {
                Point neighbor = new Point(point.X + direction.X, point.Y + direction.Y);
                if (!InBounds(neighbor)) continue;
                int thisNumber = _numbers[point.Y][point.X];
                int neighborValue = _numbers[neighbor.Y][neighbor.X];
                if (neighborValue > thisNumber && neighborValue < 9)
                {
                    neighbors.Add(neighbor);
                }
            }

            return neighbors;
        }

        private bool InBounds(Point point)
        {
            return point.X >= 0 && point.Y >= 0 && point.X < _numbers[0].Count && point.Y < _numbers.Count;
        }
    }
}