using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using AdventOfCode2021.Utils;

namespace AdventOfCode2021.Puzzles._2022;

public class Day09 : AocPuzzle
{
    public override int Year => 2022;
    public override int Day => 9;

    private const bool ShouldPrint = false;

    // private Vector2 _head, _tail;
    private List<Vector2> _knots;
    private Vector2 Head
    {
        get => _knots.First();
        set => _knots[0] = value;
    }

    private Vector2 Tail
    {
        get => _knots.Last();
        set => _knots[^1] = value;
    }
    private HashSet<Vector2> _tailVisited = new();
    protected override void Solve(IEnumerable<string> input)
    {
        PartOne = SolvePart(input, 2);
        PartTwo = SolvePart(input, 10);
    }

    private int SolvePart(IEnumerable<string> input, int length)
    {
        _tailVisited = new HashSet<Vector2>();
        _knots = new List<Vector2>();
        // Fill knots
        for (int i = 0; i < length; i++)
        {
            _knots.Add(new Vector2(0, 0));
        }
        _tailVisited.Add(new Vector2(0, 0));
        PrintKnots();
        foreach (string line in input)
        {
            (string direction, string s, _) = line.Split(' ');
            int steps = int.Parse(s);
            Vector2 movement = direction switch
            {
                "R" => new Vector2(1, 0),
                "U" => new Vector2(0, 1),
                "L" => new Vector2(-1, 0),
                "D" => new Vector2(0, -1),
                _ => new Vector2()
            };
            Console.WriteLine($"== {direction} {steps} ==");
            for (int i = 0; i < steps; i++)
            {
                Head += movement;
                // Skip the head
                for (int j = 1; j < _knots.Count; j++)
                {
                    _knots[j] = UpdateKnot(_knots[j], _knots[j-1], movement);
                    if (j == _knots.Count - 1)
                    {
                        _tailVisited.Add(_knots[j]);
                    }
                }
            }
            if(ShouldPrint) PrintKnots();
        }

        return _tailVisited.Count;
        PrintKnots();
    }

    private void UpdateTail(Vector2 headMovement)
    {
        float distance = Vector2.Distance(Head, Tail);
        if ((int)distance <= 1) return;
        Vector2 diff = Head - Tail;
        Vector2 movement = diff - headMovement;
        Tail += movement;
    }

    private Vector2 UpdateKnot(Vector2 knot, Vector2 next, Vector2 nextMovement)
    {
        float distance = Vector2.Distance(next, knot);
        if ((int)distance > 1)
        {
            if ((int)knot.X == (int)next.X || (int)knot.Y == (int)next.Y)
            {
                // Shitty bruteforce method but it works
                var jumps = new List<Vector2>
                {
                    new(0, 1),
                    new(0, -1),
                    new(1, 0),
                    new(-1, 0),
                };
                Vector2 movement = new Vector2(0, 0);
                foreach (Vector2 jump in jumps)
                {
                    Vector2 newPos = knot + jump;
                    if ((int)Vector2.Distance(newPos, next) == 1)
                    {
                        movement = jump;
                    }
                }

                knot += movement;
            }
            else
            {
                // Shitty bruteforce method but it works
                var jumps = new List<Vector2>
                {
                    new(1, 1),
                    new(-1, 1),
                    new(1, -1),
                    new(-1, -1),
                };
                var movement = new Vector2(0, 0);
                foreach (Vector2 jump in jumps)
                {
                    Vector2 newPos = knot + jump;
                    if ((int)Vector2.Distance(newPos, next) == 1)
                    {
                        movement = jump;
                    }
                }
                knot += movement;
            }
        }
        return knot;
    }

    private void PrintKnots()
    {
        int minX = (int)_knots.Min(x => x.X);
        int minY = (int)_knots.Min(x => x.Y);

        int maxX = 25;
        int maxY = 25;

        minX = minX < 0 ? minX : 0;
        minY = minY < 0 ? minY : 0;
        for (int y = maxY; y >= minY; y--)
        {
            for (int x = minX; x <= maxX; x++)
            {
                int i = _knots.FindIndex(v => v == new Vector2(x, y));
                if (i != -1)
                {
                    Console.Write(i == 0 ? "H": i);
                }
                else
                {
                    Console.Write('.');
                }
            }

            Console.WriteLine();
        }

        Console.WriteLine();
    }
}