using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace AdventOfCode2021.Utils
{
    public static class ExtensionMethods
    {
        public static string ToSimpleString(this Point point)
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

        public static void Deconstruct<T>(this IEnumerable<T> seq, out T first, out IEnumerable<T> rest)
        {
            first = seq.FirstOrDefault();
            rest = seq.Skip(1);
        }

        public static void Deconstruct<T>(this IEnumerable<T> seq, out T first, out T second, out IEnumerable<T> rest)
            => (first, (second, rest)) = seq;

        public static void Deconstruct<T>(this IEnumerable<T> seq, out T first, out T second, out T third, out IEnumerable<T> rest)
            => (first, second, (third, rest)) = seq;

        public static void Deconstruct<T>(this IEnumerable<T> seq, out T first, out T second, out T third, out T fourth, out IEnumerable<T> rest)
            => (first, second, third, (fourth, rest)) = seq;

        public static void Deconstruct<T>(this IEnumerable<T> seq, out T first, out T second, out T third, out T fourth, out T fifth, out IEnumerable<T> rest)
            => (first, second, third, fourth, (fifth, rest)) = seq;
        public static void Deconstruct<T>(this IEnumerable<T> seq, out T first, out T second, out T third, out T fourth, out T fifth, out T sixth, out IEnumerable<T> rest)
            => (first, second, third, fourth, fifth, (sixth, rest)) = seq;
        public static void Deconstruct<T>(this IEnumerable<T> seq, out T first, out T second, out T third, out T fourth, out T fifth, out T sixth, out T seventh, out IEnumerable<T> rest)
            => (first, second, third, fourth, fifth, sixth, (seventh, rest)) = seq;
    }
}