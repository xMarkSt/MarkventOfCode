using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode2021.Utils;

namespace AdventOfCode2021.Puzzles._2022;

public class Day06 : AocPuzzle
{
    public override int Year => 2022;
    public override int Day => 6;
    protected override void Solve(IEnumerable<string> input)
    {
        string str = string.Join("", input);
        PartOne = FindFirstUnique(str, 4);
        PartTwo = FindFirstUnique(str, 14);
    }

    private static int FindFirstUnique(string str, int length)
    {
        for (int i = length - 1; i < str.Length; i++)
        {
            string lastChars = str.Substring(i - (length-1), length);
            if (lastChars.IsUnique())
            {
                return i + 1;
            }
        }
        return -1;
    }
}