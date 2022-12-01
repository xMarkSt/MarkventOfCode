using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2021.Puzzles
{
    public class Day08 : AocPuzzle
    {
        public override int Year => 2021;
        public override int Day => 8;
        
        protected override void Solve(IEnumerable<string> input)
        {
            var inp = input.Select(x => x.Split(" | ")).ToList();
            var mappings = new List<HashSet<char>[]>();
            foreach (string[] line in inp)
            {
                string output = line[0];
                var outputDigits = output.Split(' ').OrderBy(x => x.Length); // todo rename outputdigits
                mappings.Add(MakeMappings(outputDigits));
            }
            // TODO re-add part 1
            int parttwo = 0;
            int _cntPartOne = 0;
            for (int i = 0; i < inp.Count; i++)
            {
                var currentMappings = mappings[i];
                var outputDigits = inp[i][1].Split(' ');
                string newOutput = "";
                foreach (string digit in outputDigits)
                {
                    // part 1 here
                    if (digit.Length == 2 || digit.Length == 3 || digit.Length == 4 || digit.Length == 7)
                        _cntPartOne++;
                    var hashs = new HashSet<char>(digit);
                    for (int j = 0; j < currentMappings.Length; j++)
                    {
                        if (hashs.SetEquals(currentMappings[j]))
                        {
                            newOutput += j;
                        }
                    }
                }
                parttwo += int.Parse(newOutput);
            }

            PartOne = _cntPartOne.ToString();
            PartTwo = parttwo.ToString();
        }

        private HashSet<char>[] MakeMappings(IOrderedEnumerable<string> outputDigits)
        {
            var mappings = new Dictionary<int, HashSet<char>>();
            foreach (string digit in outputDigits)
            {
                var digitSet = digit.ToCharArray().ToHashSet();
                switch (digit.Length)
                {
                    case 2:
                        mappings.Add(1, digitSet);
                        break;
                    case 3:
                        mappings.Add(7, digitSet);
                        break;
                    case 4:
                        mappings.Add(4, digitSet);
                        break;
                    // 3 is superset of 7
                    case 5 when digitSet.IsSupersetOf(mappings[7]):
                        mappings.Add(3, digitSet);
                        break;
                    // 4 and 5 have 3 segments in common
                    case 5 when SegmentsInCommon(mappings[4], digitSet) == 3:
                        mappings.Add(5, digitSet);
                        break;
                    case 5:
                        mappings.Add(2, digitSet);
                        break;
                    // 9 is superset of 4
                    case 6 when digitSet.IsSupersetOf(mappings[4]):
                        mappings.Add(9, digitSet);
                        break;
                    // 0 is a superset of 7
                    case 6 when digitSet.IsSupersetOf(mappings[7]):
                        mappings.Add(0, digitSet);
                        break;
                    case 6:
                        mappings.Add(6, digitSet);
                        break;
                    case 7:
                        mappings.Add(8, digitSet);
                        break;
                }
            }

            if (mappings.Count != 10)
            {
                Console.WriteLine("something went wrong");
            }

            return mappings
                .OrderBy(x => x.Key)
                .Select(x => x.Value)
                .ToArray();
        }

        private int SegmentsInCommon(HashSet<char> one, HashSet<char> two)
        {
            int inCommon = 0;
            foreach (char c in one)
            {
                if (two.Contains(c)) inCommon++;
            }

            return inCommon;
        }
    }
}