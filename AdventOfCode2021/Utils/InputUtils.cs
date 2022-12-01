using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode2021.Utils
{
    public static class InputUtils
    {
        public static List<string> RegexGroupCapture(Regex regex, string input)
        {
            Match match = regex.Match(input);
            return match.Groups.Values.Skip(1)
                .Select(groupsValue => groupsValue.Value).ToList();
        }
        
        public static IEnumerable<int> AsIntList(this IEnumerable<string> input) => input.Select(int.Parse);
        public static IEnumerable<long> AsLongList(this IEnumerable<string> input) => input.Select(long.Parse);

        public static IEnumerable<IEnumerable<string>> SplitByEmptyLine(this IEnumerable<string> input)
        {
            return string.Join(Environment.NewLine, input).Split(Environment.NewLine + Environment.NewLine)
                .Select(x => x.Split(Environment.NewLine).ToList());
        }
    }
}