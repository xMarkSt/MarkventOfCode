using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode2021.Puzzles;

public class Day07 : AocPuzzle
{
    public override int Year => 2021;
    public override int Day => 7;
    protected override void Solve(IEnumerable<string> input)
    {
        var inp = input.ToList();
        List<int> positions = inp[0].Split(',').Select(int.Parse).ToList();
        long lowest = long.MaxValue;
        long lowest2 = long.MaxValue;
        for (int position = 0; position < positions.Max(); position++)
        {
            long count = 0;
            long count2 = 0;
            foreach (int position2 in positions)
            {
                long diff = Math.Abs(position - position2);
                count += diff;
                for (int i = 1; i <= diff; i++)
                {
                    count2 += i;
                }
            }
            if (count < lowest) lowest = count;
            if (count2 < lowest2) lowest2 = count2;
        }
        PartOne = lowest.ToString();
        PartTwo = lowest2.ToString();
    }
}