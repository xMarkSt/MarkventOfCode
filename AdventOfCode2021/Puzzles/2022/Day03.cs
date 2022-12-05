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
            int prioSum = 0;
            foreach (string s in input)
            {
                // First half
                string c1 = s[..(s.Length / 2)];
                // Second half
                string c2 = s[(s.Length / 2)..];
                char both = c1.Intersect(c2).First();
                prioSum += both >= 'a' ? both - 'a' + 1 : both - 'A' + 27;
            }

            PartOne = prioSum;

            prioSum = 0;
            foreach (string[] s in input.Chunk(3))
            {
                char both = s[0].Intersect(s[1]).Intersect(s[2]).First();
                prioSum += both >= 'a' ? both - 'a' + 1 : both - 'A' + 27;
            }
            PartTwo = prioSum;
        }
    }
}