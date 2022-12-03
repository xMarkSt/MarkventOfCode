using System.Collections.Generic;
using System.Diagnostics;

namespace AdventOfCode2021.Puzzles._2022
{
    public class Day03 : AocPuzzle
    {
        public override int Year => 2022;
        public override int Day => 3;
        protected override void Solve(IEnumerable<string> input)
        {
            int prioSum = 0;
            foreach (var s in input)
            {
                string c1 = s.Substring(0, s.Length / 2);
                string c2 = s.Substring(s.Length / 2);
                char? both = FindContainsBoth(c1, c2);
                if (both.HasValue)
                {
                    int prio;
                    // Small
                    if (both >= 'a')
                    {
                        prio = (int)(both - 'a' + 1);
                    }
                    else
                    {
                        prio = (int)(both - 'A' + 27);
                    }

                    prioSum += prio;
                }
                else
                {
                    Debug.Write("No letter in both comopartments?");
                }
            }

            PartOne = prioSum;

            prioSum = 0;
            string s1 = "";
            string s2 = "";
            string s3 = "";
            int cnt = 0;
            foreach (string s in input)
            {
                if (cnt == 0)
                {
                    s1 = s;
                    cnt++;
                }
                else if (cnt == 1)
                {
                    s2 = s;
                    cnt++;
                }
                else if (cnt == 2)
                {
                    s3 = s;
                    char? both = FindContainsThree(s1, s2, s3);
                    if (both.HasValue)
                    {
                        int prio;
                        // Small
                        if (both >= 'a')
                        {
                            prio = (int)(both - 'a' + 1);
                        }
                        else
                        {
                            prio = (int)(both - 'A' + 27);
                        }

                        prioSum += prio;
                        cnt = 0;
                    }
                    else
                    {
                        Debug.Write("No letter in both comopartments?");
                    }

                }
            }

            PartTwo = prioSum;
        }

        char? FindContainsBoth(string a, string b)
        {
            foreach (char c1 in a)
            foreach (char c2 in b)
            {
                if (c1 == c2) return c1;
            }

            return null;
        }
        
        char? FindContainsThree(string a, string b, string c)
        {
            foreach (char c1 in a)
            foreach (char c2 in b)
            foreach (char c3 in c)
            {
                if (c1 == c2 && c1 == c3) return c1;
            }

            return null;
        }
    }
}