using System;
using System.Collections.Generic;
using System.Drawing;
using System.Numerics;
using AdventOfCode2021.Utils;

namespace AdventOfCode2021.Puzzles._2022;

public class Day09 : AocPuzzle
{
    public override int Year => 2022;
    public override int Day => 9;

    private Vector2 _head, _tail;
    private HashSet<Vector2> _tailVisited = new();
    protected override void Solve(IEnumerable<string> input)
    {
        _head = new Vector2(0, 0);
        _tail = new Vector2(0, 0);
        _tailVisited.Add(new Vector2(0, 0));
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
            for (int i = 0; i < steps; i++)
            {
                _head += movement;
                UpdateTail(movement);
                _tailVisited.Add(_tail);
            }
        }

        PartOne = _tailVisited.Count;
    }

    private void UpdateTail(Vector2 headMovement)
    {
        float distance = Vector2.Distance(_head, _tail);
        if ((int)distance <= 1) return;
        Vector2 diff = _head - _tail;
        Vector2 movement = diff - headMovement;
        _tail += movement;
    }
}