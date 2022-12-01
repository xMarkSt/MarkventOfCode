using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode2021.Utils;

namespace AdventOfCode2021.Puzzles._2022
{
    public class Day01 : AocPuzzle
    {
        public override int Year => 2022;
        public override int Day => 1;
        protected override void Solve(IEnumerable<string> input)
        {
            var res = input.SplitByEmptyLine()
                .Select(x => x.AsLongList().Sum())
                .OrderByDescending(x => x)
                .Take(3);
            PartOne = res.First();
            PartTwo = res.Sum();
        }
    }
}