using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2021.Puzzles
{
    public class Day10 : AocPuzzle
    {
        public override int Year => 2021;
        public override int Day => 10;
        protected override void Solve(IEnumerable<string> input)
        {
            int cnt = 0;
            List<string> valid = new List<string>();
            foreach (string line in input)
            {
                int score = CheckParenthesis(line);
                if (score == 0)
                {
                    valid.Add(line);
                }
                cnt += score;
            }

            var scoreTable = new Dictionary<char, long>()
            {
                { ')', 1 },
                { ']', 2 },
                { '}', 3 },
                { '>', 4 }
            };

            var scores = new List<long>();
            
            foreach (string line in valid)
            {
                long score2 = 0;
                string completion = CompleteParenthesis(line);
                foreach (char c in completion)
                {
                    score2 *= 5;
                    score2 += scoreTable[c];
                }
                scores.Add(score2);
            }

            PartOne = cnt.ToString();
            scores.Sort();
            PartTwo = scores[scores.Count / 2].ToString();
        }
        
        private static int CheckParenthesis(string str)
        {
            var pairs = new Dictionary<char, char>()
            {
                {'{', '}'},
                {'[', ']'},
                {'(', ')'},
                {'<', '>'},
            };
            if (string.IsNullOrEmpty(str))
                return 0;

            Stack<char> stack = new Stack<char>();
            foreach (var current in str)
            {
                if (pairs.ContainsKey(current))
                {
                    stack.Push(current);
                }

                if (pairs.ContainsValue(current))
                {
                    if (stack.Count == 0)
                        return 0;

                    char last = stack.Peek();
                    if (pairs.ContainsKey(last) && pairs[last] == current)
                        stack.Pop();
                    else
                    {
                        switch (current)
                        {
                            case ')':
                                return 3;
                            case ']':
                                return 57;
                            case '}':
                                return 1197;
                            case '>':
                                return 25137;
                        }
                    }
                }
            }
            return 0;
        }
        
        private static string CompleteParenthesis(string str)
        {
            var pairs = new Dictionary<char, char>()
            {
                {'{', '}'},
                {'[', ']'},
                {'(', ')'},
                {'<', '>'},
            };
            
            if (string.IsNullOrEmpty(str))
                return "";

            Stack<char> stack = new Stack<char>();
            foreach (var current in str)
            {
                if (pairs.ContainsKey(current))
                {
                    stack.Push(current);
                }

                if (pairs.ContainsValue(current))
                {
                    if (stack.Count == 0)
                        return "";

                    char last = stack.Peek();
                    if (pairs.ContainsKey(last) && pairs[last] == current)
                        stack.Pop();
                }
            }
            return string.Concat(stack.Select(x => pairs[x]));
        }
    }
}