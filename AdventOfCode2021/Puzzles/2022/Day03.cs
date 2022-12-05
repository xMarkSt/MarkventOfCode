using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace AdventOfCode2021.Puzzles._2022
{
    public class Day03 : AocPuzzle
    {
        public override int Year => 2022;
        public override int Day => 3;

        protected override void Solve(IEnumerable<string> input)
        {
            PartOne = input
                .Select(s => s[..(s.Length / 2)]
                    .Intersect(s[(s.Length / 2)..])
                    .First())
                .Select(c => c >= 'a' ? c - 'a' + 1 : c - 'A' + 27)
                .Sum();
            
            PartTwo = input.Chunk(3)
                .Select(s => 
                    s[0].Intersect(s[1])
                        .Intersect(s[2])
                        .First())
                .Select(c => c >= 'a' ? c - 'a' + 1 : c - 'A' + 27)
                .Sum();
        }
    }
}