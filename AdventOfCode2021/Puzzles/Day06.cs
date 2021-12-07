using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2021.Puzzles
{
    public class Day06 : AocPuzzle
    {
        public override int Year => 2021;
        public override int Day => 6;
        protected override void Solve(IEnumerable<string> input)
        {
            var inp = input.ToList();
            List<int> fishes = inp[0].Split(',').Select(int.Parse).ToList();
            var fishesCount = new Dictionary<int, long>();
            
            // Fill dictionary with 0-8 keys and count 0.
            for (int i = 0; i <= 8; i++)
            {
                fishesCount.Add(i, 0);
            }
            
            // Load data
            foreach (int fish in fishes)
            { 
                fishesCount[fish]++;
            }
            
            PartOne = Simulate(fishesCount, 80).ToString();
            PartTwo = Simulate(fishesCount, 256).ToString();
        }

        private long Simulate(Dictionary<int, long> startData, int maxDays)
        {
            int days = 0;
            while (days < maxDays)
            {
                long zeroCount = startData[0];

                for (int day = 1; day <= 8; day++)
                {
                    startData[day-1] = startData[day];
                }
   
                startData[6] += zeroCount;
                startData[8] = zeroCount;
                days++;
            }
            return startData.Values.Sum();
        }
    }
}