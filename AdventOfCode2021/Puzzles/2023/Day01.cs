using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021.Puzzles._2023
{
    public class Day01 : AocPuzzle
    {
        public override int Year => 2023;

        public override int Day => 1;
        private Dictionary<string, int> _dict;

        protected override void Solve(IEnumerable<string> input)
        {
            var dict = new Dictionary<string, int>()
            {
                { "one", 1}, { "two", 2}, {"three", 3}, {"four", 4}, {"five", 5}, {"six", 6}, {"seven", 7}, {"eight", 8}, {"nine", 9}
            };
            var inp = input;

            PartOne = inp.Select(x =>
            x.Where(c => char.IsDigit(c))
            ).Select(x => int.Parse(x.First().ToString() + x.Last().ToString()))
            .Sum();

            int sum = 0;
            foreach (var item in inp)
            {
                var first = int.MaxValue;
                int firstVal = 0;
                var last = -1;
                int lastVal = 0;

                string combined;
                foreach(var key in dict.Keys)
                {
                    int found = item.IndexOf(key);
                    if(found > -1 && found < first) {
                        first = found;
                        firstVal = dict[key];
                    }

                    found = item.LastIndexOf(key);

                    if (found > -1 && found > last)
                    {
                        last = found;
                        lastVal = dict[key];
                    }
                }

                int i = 0;
                foreach (var c in item)
                {

                    if(char.IsDigit(c))
                    {
                        if(i < first)
                        {
                            first = i;
                            firstVal = int.Parse(c.ToString());
                        }
                        if(i > last)
                        {
                            last = i;
                            lastVal = int.Parse(c.ToString());
                        }
                    }
                    i++;
                }
                combined = firstVal.ToString() + lastVal.ToString();
                if(combined.Length > 0 )
                    sum += int.Parse(combined);
            }
            PartTwo = sum;
        }
    }
}
