using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode2021.Utils;

namespace AdventOfCode2021.Puzzles._2022
{
    public class Day02 : AocPuzzle
    {
        public override int Year => 2022;
        public override int Day => 2;
        
        private Dictionary<string, int> scores = new Dictionary<string, int>()
        {
            {"X", 1},
            {"Y", 2},
            {"Z", 3}
        };

        private Dictionary<string, string> winning = new Dictionary<string, string>
        {
            { "A", "Y" },
            { "B", "Z" },
            { "C", "X" },
        };
            
        private Dictionary<string, string> losing = new Dictionary<string, string>
        {
            { "A", "Z" },
            { "B", "X" },
            { "C", "Y" },
        };
            
        private Dictionary<string, string> draw = new Dictionary<string, string>
        {
            { "A", "X" },
            { "B", "Y" },
            { "C", "Z" },
        };
        protected override void Solve(IEnumerable<string> input)
        {
            var l = input.Select(x => x.Split(' '));
            int part1Score = 0;
            int part2Score = 0;
            foreach (var s in l)
            {
                var (a, b, c) = s;

                string yourans = b switch
                {
                    // Lose
                    "X" => losing[a],
                    // Draw
                    "Y" => draw[a],
                    // Z - Win
                    "Z" => winning[a],
                    _ => throw new ArgumentOutOfRangeException()
                };

                part1Score += GetScore(a, b);
                part2Score += GetScore(a, yourans);
            }

            PartOne = part1Score;
            PartTwo = part2Score;
        }

        private int GetScore(string opponentAnswer, string yourAnswer)
        {
            int roundScore = 0;
            roundScore += scores[yourAnswer];
            if (winning.ContainsKey(opponentAnswer) && winning[opponentAnswer] == yourAnswer)
            {
                roundScore += 6;
            }
            else if (draw.ContainsKey(opponentAnswer) && draw[opponentAnswer] == yourAnswer)
            {
                roundScore += 3;
            }
            return roundScore;
        }
    }
}