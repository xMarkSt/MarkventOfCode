using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;

namespace AdventOfCode2021.Utils
{
    public static class ExtensionMethods
    {
        public static string ToSimpleString(this Point point)
        {
            return $"{point.X},{point.Y}";
        }
        
        public static string ToSimpleString(this Vector2 point)
        {
            return $"{point.X},{point.Y}";
        }
        
        //// str - the source string
        //// index- the start location to replace at (0-based)
        //// length - the number of characters to be removed before inserting
        //// replace - the string that is replacing characters
        public static string ReplaceAt(this string str, int index, int length, string replace)
        {
            return str.Remove(index, Math.Min(length, str.Length - index))
                .Insert(index, replace);
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
}