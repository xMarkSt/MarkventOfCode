using System;
using AdventOfCode2021.Puzzles;
using CommandLine;

namespace AdventOfCode2021
{
    public class Options
    {
        [Option('d', "day", Required = false, HelpText = "Advent of code day. Defaults to the current day.")]
        public int Day { get; set; } = DateTime.Now.Day;

        [Option('y', "year", Required = false, HelpText = "Advent of code year. Defaults to the current year.")]
        public int Year { get; set; } = DateTime.Now.Year;
    }

    class Program
    {
        static void Main(string[] args)
        {
            Parser.Default.ParseArguments<Options>(args)
                .WithParsed(options =>
                {
                    PuzzleManager pm = new PuzzleManager();
                    pm.SolvePuzzle(options.Year, options.Day).Wait();
                });
        }
    }
}
