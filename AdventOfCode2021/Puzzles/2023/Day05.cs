using AdventOfCode2021.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021.Puzzles._2023
{
    public class Day05 : AocPuzzle
    {
        public class Map
        {
            public List<Mapping> Mappings { get; set; }

            public Map(IEnumerable<string> input) : this(input, false)
            {
            }

            public Map(IEnumerable<string> input, bool invert)
            {
                Mappings = input.Skip(1).Select(x => new Mapping(x, invert)).ToList();
            }

            public long MapNumber(long number)
            {
                var mapping = Mappings.Where(x => x.SourceRangeStart <= number && number < x.SourceRangeEnd);
                if (mapping.Any())
                {
                    return mapping.First().MapNumber(number);
                }
                return number;
            }
        }

        public class Mapping
        {
            public long SourceRangeStart { get; set; }
            public long SourceRangeEnd { get; set; } // can be replaced by getter
            public long DestinationRangeStart { get; set; }
            public long DestinationRangeEnd { get; set; } // can be replaced by getter
            public long Diff { get; set; }
            public long MapNumber(long number) => number + Diff;

            public Mapping(string input) : this(input, false)
            {
            }

            public Mapping(string input, bool invert)
            {
                if (invert)
                {
                    var split = input.Split(' ').AsLongList().ToList();
                    DestinationRangeStart = split[1];
                    DestinationRangeEnd = SourceRangeStart + split[2];
                    SourceRangeStart = split[0];
                    SourceRangeEnd = split[0] + split[2];

                    Diff = DestinationRangeStart - SourceRangeStart;
                }
                else
                {
                    var split = input.Split(' ').AsLongList().ToList();
                    SourceRangeStart = split[1];
                    SourceRangeEnd = SourceRangeStart + split[2];
                    DestinationRangeStart = split[0];
                    DestinationRangeEnd = split[0] + split[2];

                    Diff = DestinationRangeStart - SourceRangeStart;
                }
            }
        }

        public override int Year => 2023;

        public override int Day => 5;

        protected override void Solve(IEnumerable<string> input)
        {
            List<string> inputList = input.ToList();
            IEnumerable<long> seeds = inputList[0].Split("seeds: ")[1].Split(' ').AsLongList();
            var seeds2 = seeds.Chunk(2);
            // Skip seeds and create maps
            var maps = inputList.Skip(2).SplitByEmptyLine().Select(x => new Map(x)).ToList();

            var locations = new List<long>();

            foreach (long seed in seeds)
            {
                //Console.WriteLine($"{seed} has soil {maps[0].MapNumber(seed)}");

                long number = seed;
                foreach (var map in maps)
                {
                    number = map.MapNumber(number);
                }
                //Console.WriteLine($"{seed} has location {number}");
                locations.Add(number);
            }

            PartOne = locations.OrderBy(x => x).First();

            // Part 2

            var maps2 = inputList.Skip(2).SplitByEmptyLine().Select(x => new Map(x, true)).Reverse().ToList();
            long lowest = long.MaxValue;
            for (long i = 0; i < long.MaxValue; i++)
            {
                long number = i;
                foreach (var map in maps2)
                {
                    number = map.MapNumber(number);
                }
                if (i < lowest && IsSeedInRange(number, seeds2))
                {
                    lowest = i;
                    break;
                }
            }

            PartTwo = lowest;
        }

        private bool IsSeedInRange(long seed, IEnumerable<long[]> seeds)
        {
            foreach (var item in seeds)
            {
                if (seed > item[0] && seed < item[0] + item[1]) return true;
            }
            return false;
        }
    }
}
