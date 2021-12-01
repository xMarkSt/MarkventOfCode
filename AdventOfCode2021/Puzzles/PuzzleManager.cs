using System;
using System.Linq;
using System.Threading.Tasks;

namespace AdventOfCode2021.Puzzles
{
    public class PuzzleManager
    {
        
        public async Task SolvePuzzle(int year, int day)
        {
            AocPuzzle puzzle = FindByYearAndDay(year, day);
            if (puzzle == null)
            {
                Console.WriteLine("Puzzle not found this day/year, use -d or -y to choose a different day and year.");
            }
            else
            {
                await puzzle.LoadInput();
                puzzle.Solve();
                Console.WriteLine("Part one: " + puzzle.PartOne);
                Console.WriteLine("Part two: " + puzzle.PartTwo);
            }
        }
        
        private AocPuzzle FindByYearAndDay(int year, int day)
        {
            AocPuzzle puzzle = typeof(AocPuzzle)
                .Assembly.GetTypes()
                .Where(t => t.IsSubclassOf(typeof(AocPuzzle)) && !t.IsAbstract)
                .Select(t => (AocPuzzle)Activator.CreateInstance(t))
                .FirstOrDefault(puzzle => puzzle != null && puzzle.Day == day && puzzle.Year == year);
            return puzzle;
        }
    }
}