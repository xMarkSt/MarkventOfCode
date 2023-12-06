using AdventOfCode2021.Utils;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021.Puzzles._2023
{
    public class Day06 : AocPuzzle
    {
        public override int Year => 2023;

        public override int Day => 6;

        protected override void Solve(IEnumerable<string> input)
        {
            var inputList = input.Select(x => x
                .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                .Skip(1)
                .AsLongList()
            ).ToList();

            var races = inputList[0].Zip(inputList[1], (time, distance) => new
            {
                Time = time,
                Record = distance
            });

            PartOne = races.Aggregate(1L, (val, race) => val * GetRecordsBeaten(race.Time, race.Record));

            // Part 2
            long time = long.Parse(string.Join("", inputList[0].Select(x => x.ToString())));
            long record = long.Parse(string.Join("", inputList[1].Select(x => x.ToString())));

            long poss = GetRecordsBeaten(time, record);
            PartTwo = poss;
        }

        private static long GetRecordsBeaten(long time, long recordDistance)
        {
            long possibilities = 0;
            for (long i = 1; i < time; i++)
            {
                long speed = i;
                long msLeft = time - i;
                long distance = speed * msLeft;
                if (distance > recordDistance)
                {
                    possibilities++;
                }
            }
            return possibilities;
        }
    }
}
