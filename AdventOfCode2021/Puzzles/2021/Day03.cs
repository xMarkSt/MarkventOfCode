using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2021.Puzzles
{
    public class Day03 : AocPuzzle
    {
        public override int Year => 2021;
        public override int Day => 3;

        protected override void Solve(IEnumerable<string> input)
        {
            var code = input.ToList();
            string gamma = "";
            string epsilon = "";
            
            for (int col = 0; col < code[0].Length; col++)
            {
                int count0 = 0;
                int count1 = 0;
                for (int row = 0; row < code.Count; row++)
                {
                    if (code[row][col] == '0')
                    {
                        count0++;
                    }
                    else if (code[row][col] == '1')
                    {
                        count1++;
                    }
                }

                if (count0 > count1)
                {
                    gamma += '0';
                    epsilon += '1';
                }
                else if (count1 > count0)
                {
                    gamma += '1';
                    epsilon += '0';
                }
            }

            int gammaI = Convert.ToInt32(gamma, 2);
            int epsilonI = Convert.ToInt32(epsilon, 2);

            PartOne = (gammaI * epsilonI).ToString();

            string oxygen = "";
            string scrubber = "";
            int codeLength = code[0].Length;
            for (int col = 0; col < codeLength; col++)
            {
                int count0 = 0;
                int count1 = 0;
                for (int row = 0; row < code.Count; row++)
                {
                    if (code[row][col] == '0')
                    {
                        count0++;
                    }
                    else if (code[row][col] == '1')
                    {
                        count1++;
                    }
                }
                if (count0 > count1)
                {
                    oxygen += '0';
                    // scrubber += '1';
                    code = code.Where(x => x[col] == '0').ToList();
                }
                
                else if (count1 >= count0)
                {
                    oxygen += '1';
                    // scrubber += '0';
                    code = code.Where(x => x[col] == '1').ToList();
                }

                if (code.Count == 1)
                {
                    oxygen = code[0];
                    break;
                }
            }
            int oxygenI = Convert.ToInt32(oxygen, 2);

            code = input.ToList();
            codeLength = code[0].Length;
            for (int col = 0; col < codeLength; col++)
            {
                int count0 = 0;
                int count1 = 0;
                for (int row = 0; row < code.Count; row++)
                {
                    if (code[row][col] == '0')
                    {
                        count0++;
                    }
                    else if (code[row][col] == '1')
                    {
                        count1++;
                    }
                }
                if (count0 > count1)
                {
                    // oxygen += '0';
                    scrubber += '1';
                    code = code.Where(x => x[col] == '1').ToList();
                }
                
                else if (count1 >= count0)
                {
                    // oxygen += '1';
                    scrubber += '0';
                    code = code.Where(x => x[col] == '0').ToList();
                }

                if (code.Count == 1)
                {
                    scrubber = code[0];
                    break;
                }
            }
            int scrubberI = Convert.ToInt32(scrubber, 2);

            PartTwo = (oxygenI * scrubberI).ToString();
        }
    }
}