using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode2021.Utils
{
    public static class ConversionUtils
    {
        public static long Bin2Int(string bin)
        {
            return Convert.ToInt64(bin, 2);
        }
    }
}