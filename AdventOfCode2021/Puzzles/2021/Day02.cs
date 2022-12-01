using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2021.Puzzles
{
    public class Day02 : AocPuzzle
    {
        public override int Year => 2021;
        public override int Day => 2;

        protected override void Solve(IEnumerable<string> input)
        {
            var instructions = input.Select(x =>
            {
                var res = x.Split(' ');
                return new
                {
                    direction = res[0],
                    x = int.Parse(res[1])
                };
            });
            int depth = 0;
            int horizontal = 0;
            int aim = 0;
            foreach (var instruction in instructions)
            {
                switch (instruction.direction)
                {
                    case "forward":
                        horizontal+= instruction.x;
                        depth += aim * instruction.x;
                        break;
                    case "down":
                        aim += instruction.x;
                        break;
                    case "up":
                        aim -= instruction.x;
                        break;
                }
            }

            PartOne = (aim * horizontal).ToString();
            PartTwo = (depth * horizontal).ToString();
        }
    }
}