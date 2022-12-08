using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;

namespace AdventOfCode2021.Puzzles._2022;

public class Day08 : AocPuzzle
{
    public override int Year => 2022;
    public override int Day => 8;

    private List<List<int>> _input;
    
    private readonly List<Point> _directions = new()
    {
        // Down
        new Point(0, 1),
        // Right
        new Point(1, 0),
        // Up
        new Point(0, -1),
        // Left
        new Point(-1, 0),
    };

    protected override void Solve(IEnumerable<string> input)
    {
        _input = input
            .Select(x => x
                .Select(c => c - '0')
                .ToList())
            .ToList();
        int count = 0;
        int edgeCount = _input[0].Count * 2 + _input.Count * 2 - 4;
        int maxScore = 0;
        for (int i = 1; i < _input.Count - 1; i++)
        {
            for (int j = 1; j < _input[i].Count - 1; j++)
            {
                // Any tree visible?
                if (_directions.Any(x => CheckDirection(new Point(j, i), x)))
                {
                    count++;
                }
                int score = TreeScore(i, j);
                if (score > maxScore) maxScore = score;
            }
        }

        PartOne = count + edgeCount;
        PartTwo = maxScore;
    }

    private int TreeScore(int row, int col)
    {
        return _directions
            .Select(x => ScoreDirection(new Point(col, row), x))
            .Aggregate(1, (acc, val) => acc * val);
    }
    
    private int ScoreDirection(Point treePos, Point direction)
    {
        int ownHeight = _input[treePos.Y][treePos.X];
        treePos += (Size)direction;
        int viewingDistance = 0;
        while (treePos.X >= 0 && treePos.X < _input[0].Count &&
               treePos.Y >= 0 && treePos.Y < _input.Count)
        {
            viewingDistance++;
            if (_input[treePos.Y][treePos.X] >= ownHeight)
            {
                return viewingDistance;
            }
            treePos += (Size)direction;
        }
        
        return viewingDistance;
    }

    private bool CheckDirection(Point treePos, Point direction)
    {
        int ownHeight = _input[treePos.Y][treePos.X];
        treePos += (Size)direction;
        while (treePos.X >= 0 && treePos.X < _input[0].Count &&
               treePos.Y >= 0 && treePos.Y < _input.Count)
        {
            int tree = _input[treePos.Y][treePos.X];
            if (tree >= ownHeight)
            {
                return false;
            }
            treePos += (Size)direction;
        }

        return true;
    }
}