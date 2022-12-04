using System.Collections.Generic;
using System.Linq;
using AdventOfCode2021.Utils;

namespace AdventOfCode2021.Puzzles._2022
{
    public class Day04 : AocPuzzle
    {
        public override int Year => 2022;
        public override int Day => 4;

        protected override void Solve(IEnumerable<string> input)
        {
            var list = input.Select(x => x.Split(',')
                .Select(y => y.Split('-').AsIntList().ToList()));
            int count = 0;
            int count2 = 0;
            foreach ((List<int> first, List<int> second, _) in list)
            {
                // part 1
                if (first[0] <= second[0] && second[0] <= first[1] &&
                    first[0] <= second[1] && second[1] <= first[1] ||
                    second[0] <= first[0] && first[0] <= second[1] &&
                    second[0] <= first[1] && first[1] <= second[1])
                {
                    count++;
                }

                // part 2
                if (first[0] <= second[0] && second[0] <= first[1] ||
                    first[0] <= second[1] && second[1] <= first[1] ||
                    second[0] <= first[0] && first[0] <= second[1] ||
                    second[0] <= first[1] && first[1] <= second[1])
                {
                    count2++;
                }
            }

            PartOne = count;
            PartTwo = count2;
        }
    }
}