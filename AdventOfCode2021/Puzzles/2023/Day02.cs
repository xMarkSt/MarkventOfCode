using AdventOfCode2021.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021.Puzzles._2023
{
    public class Day02 : AocPuzzle
    {
        public override int Year => 2023;

        public override int Day => 2;

        protected override void Solve(IEnumerable<string> input)
        {
            var games = input.Select(x => 
            {
                var split = x.Split(": ");
                return new
                {
                    GameId = int.Parse(split[0].Split("Game ")[1]),
                    Game = split[1],
                };
            });

            int sumIds = 0;
            int sumPowers = 0;

            foreach (var game in games)
            {
                string[] sets = game.Game.Split("; ");

                var colorCounts = new Dictionary<string, int>()
                {
                    { "red", 0 },
                    { "green", 0 },
                    { "blue", 0 },
                };
                foreach (var set in sets)
                {
                    string[] cubes = set.Split(", ");

                    foreach (var cube in cubes)
                    {
                        (string sAmount, string color, _) = cube.Split(' ');
                        int amount = int.Parse(sAmount);
                        if(amount > colorCounts[color])
                        {
                            colorCounts[color] = amount;
                        }
                    }
                }
                Console.WriteLine($"Game {game.GameId} has {colorCounts["red"]} red, {colorCounts["blue"]} blue, {colorCounts["green"]} green, ");
                bool gamePossible = colorCounts["red"] <= 12 && colorCounts["green"] <= 13 && colorCounts["blue"] <= 14;
                if(gamePossible)
                {
                    sumIds += game.GameId;
                }
                sumPowers += colorCounts["red"] * colorCounts["green"] * colorCounts["blue"];
            }

            PartOne = sumIds;
            PartTwo = sumPowers;
        }
    }
}
