﻿using System;
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
}