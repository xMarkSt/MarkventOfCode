using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace AdventOfCode2021.Puzzles
{
    public class Day11 : AocPuzzle
    {
        public override int Year => 2021;
        public override int Day => 11;

        private static List<Octopus> _octopi;
        protected override void Solve(IEnumerable<string> input)
        {
            _octopi = input.SelectMany((row, y) => row
                    .Select((level, x) => new Octopus(x, y, (int)char.GetNumericValue(level))))
                    .ToList();
            for (int step = 1; step <= 100; step++)
            {
                _octopi.ForEach(octo => octo.EnergyLevel++);
                _octopi.Where(octo => octo.EnergyLevel > 9).ToList()
                    .ForEach(octo => octo.Flash(step));
                _octopi.Where(octo => octo.LastFlashed == step).ToList().ForEach(octo => octo.EnergyLevel = 0);
            }
            
            PartOne = Octopus.flashes.ToString();

            _octopi = input.SelectMany((row, y) => row
                    .Select((level, x) => new Octopus(x, y, (int)char.GetNumericValue(level))))
                .ToList();
            int currentStep = 0;
            while(_octopi.Any(octo => octo.LastFlashed != currentStep))
            {
                currentStep++;
                _octopi.ForEach(octo => octo.EnergyLevel++);
                _octopi.Where(octo => octo.EnergyLevel > 9).ToList()
                    .ForEach(octo => octo.Flash(currentStep));
                _octopi.Where(octo => octo.LastFlashed == currentStep).ToList().ForEach(octo => octo.EnergyLevel = 0);
            }

            PartTwo = currentStep.ToString();
        }

        internal class Octopus
        {
            internal int X { get; set; }
            internal int Y { get; set; }
            internal int EnergyLevel { get; set; }

            internal int LastFlashed { get; set; } = -1;
            
            public static int flashes;

            internal Octopus(int x, int y, int energyLevel)
            {
                X = x;
                Y = y;
                EnergyLevel = energyLevel;
            }

            internal void Flash(int step)
            {
                if(LastFlashed == step) return;
                LastFlashed = step;
                flashes++;

                List<Octopus> neighbors = GetNeighbors();
                foreach (Octopus octo in neighbors)
                {
                    octo.EnergyLevel++;
                    if(octo.EnergyLevel > 9)
                        octo.Flash(step);
                }
            }

            private List<Octopus> GetNeighbors()
            {
                return _octopi.Where(octopus =>
                    octopus.X >= X - 1 && octopus.X <= X + 1 
                                       && octopus.Y >= Y - 1 && octopus.Y <= Y + 1 && octopus != this)
                    .ToList();
            }
        }
    }
}