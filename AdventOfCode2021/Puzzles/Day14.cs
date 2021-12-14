using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace AdventOfCode2021.Puzzles
{
    public class Day14 : AocPuzzle
    {
        public override int Year => 2021;
        public override int Day => 14;
        protected override void Solve(IEnumerable<string> input)
        {
            string template = input.ToList()[0];
            List<string> pairsInput = input.Skip(2).ToList();
            Dictionary<string, char> pairs = pairsInput.Select(pair => pair.Split(" -> ")).ToDictionary(split => split[0], split => Convert.ToChar(split[1]));

            var newTemplate = new StringBuilder(template);
            
            for (int c = 0; c < 10; c++)
            {
                int insertIndex = 1;
                for (int i = 0; i < template.Length - 1; i++)
                {
                    string key = template[i].ToString() + template[i + 1];
                    newTemplate.Insert(i + insertIndex, pairs[key]);
                    insertIndex++;
                }
            
                template = newTemplate.ToString();
            }
            var groups = template.GroupBy(x => x)
                .Select(x => new { x.Key, Count = x.Count() })
                .OrderBy(x => x.Count);
            PartOne = (groups.Last().Count - groups.First().Count).ToString();

            var pairsCount = new Dictionary<string, long>();
            foreach (string pairsKey in pairs.Keys)
            {
                pairsCount.Add(pairsKey, 0);
            }

            var pairsCountEmpty = new Dictionary<string, long>(pairsCount);
            
            for (int i = 0; i < template.Length - 1; i++)
            {
                string key = template[i].ToString() + template[i + 1];
                pairsCount[key]++;
            }

            for (int i = 0; i < 40; i++)
            {
                var newPairsCount = new Dictionary<string, long>(pairsCountEmpty);
                foreach (string key in pairsCount.Keys.ToList())
                {
                    if (pairsCount[key] > 0)
                    {
                        string newKey = key[0].ToString() + pairs[key];
                        string newKey2 = pairs[key] + key[1].ToString();
                        newPairsCount[newKey] += pairsCount[key];
                        newPairsCount[newKey2] += pairsCount[key];
                    }
                }
                pairsCount = newPairsCount;
            }
            
            // counting
            var countChars = new Dictionary<char, long>();
            foreach (string key in pairsCount.Keys)
            {
                foreach (char c in key)
                {
                    if(!countChars.ContainsKey(c))
                        countChars.Add(c, 0);
                    countChars[c] += pairsCount[key];
                }
            }

            long max = countChars.Values.Max();
            long min = countChars.Values.Min();
            long diff = max - min;
            PartTwo = template;
            double diffD = diff;
            diffD = Math.Ceiling(diffD / 2);
            PartTwo = diffD.ToString(CultureInfo.InvariantCulture);
        }
    }
}