using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2021.Puzzles
{
    public class Day01 : AocPuzzle
    {
        public override int Year => 2021;
        public override int Day => 1;

        protected override void Solve(IEnumerable<string> input)
        {
            List<int> numbers = input.Select(int.Parse).ToList();
            int count = 0;
            int threeMeasure = 0;
            for (int i = 1; i < numbers.Count; i++)
            {
                if (numbers[i] > numbers[i - 1]) count++;
                if (i > 2)
                {
                    int sumG1 = numbers[i] + numbers[i - 1] + numbers[i - 2];
                    int sumG2 = numbers[i - 1] + numbers[i - 2] + numbers[i - 3];
                    if (sumG1 > sumG2) threeMeasure++;
                }
            }

            PartOne = count.ToString();
            PartTwo = threeMeasure.ToString();
        }
    }
}