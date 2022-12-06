using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode2021.Utils;

public static class StringUtils
{
    public static List<string> RegexGroupCapture(Regex regex, string input)
    {
        Match match = regex.Match(input);
        return match.Groups.Values.Skip(1)
            .Select(groupsValue => groupsValue.Value).ToList();
    }

    /// <summary>
    /// Returns true if string consists of unique characters
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static bool IsUnique(this string str)
    {
        var chars = new HashSet<char>();
        foreach (char c in str)
        {
            if (chars.Contains(c)) return false;
            chars.Add(c);
        }

        return true;
    }
}