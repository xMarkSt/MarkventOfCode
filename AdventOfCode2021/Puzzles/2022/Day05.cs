using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using AdventOfCode2021.Utils;

namespace AdventOfCode2021.Puzzles._2022
{
    public class Day05 : AocPuzzle
    {
        public override int Year => 2022;

        public override int Day => 5;

        protected override void Solve(IEnumerable<string> input)
        {
            var (stacksStrings, instructions, _) = input.SplitByEmptyLine().Select(x => x.ToList());
            var stacks = new List<Stack<char>>();
            var stacks2 = new List<Stack<char>>();
            // Add the stacks to the list
            for (int i = 1; i < stacksStrings[^1].Length; i += 4)
            {
                stacks.Add(new Stack<char>());
                stacks2.Add(new Stack<char>());
            }
            
            // Fill the stacks
            for (int i = stacksStrings.Count - 2; i >= 0; i--)
            {
                string stack = stacksStrings[i];
                int stackIndex = 0;
                for (int j = 1; j < stack.Length; j += 4)
                {
                    char c = stack[j];
                    if (char.IsLetter(c))
                    {
                        stacks[stackIndex].Push(c);
                        stacks2[stackIndex].Push(c);
                    }
                    stackIndex++;
                }
            }
            
            var regex = new Regex(@"move (\d+) from (\d) to (\d)");

            foreach (string instruction in instructions)
            {
                (string amount, string from, string to, _) = StringUtils.RegexGroupCapture(regex, instruction);
                int fromIndex = int.Parse(from) - 1;
                int toIndex = int.Parse(to) - 1;
                
                // Part one
                for (int i = 0; i < int.Parse(amount); i++)
                {
                    var crate = stacks[fromIndex].Pop();
                    stacks[toIndex].Push(crate);
                }
                
                // Part two
                var crates = new StringBuilder();
                for (int i = 0; i < int.Parse(amount); i++)
                {
                    crates.Append(stacks2[fromIndex].Pop());
                }

                for (int i = crates.Length - 1; i >= 0; i--)
                {
                    stacks2[toIndex].Push(crates[i]);
                }
            }
            
            // Top of each stack
            PartOne = stacks.Aggregate("", (current, stack) => current + stack.Peek());
            PartTwo = stacks2.Aggregate("", (current, stack) => current + stack.Peek());
        }
    }
}