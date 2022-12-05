using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode2021.Utils
{
    public static class IEnumerableExtensions
    {
        public static IEnumerable<int> AsIntList(this IEnumerable<string> input) => input.Select(int.Parse);
        public static IEnumerable<long> AsLongList(this IEnumerable<string> input) => input.Select(long.Parse);

        public static IEnumerable<IEnumerable<string>> SplitByEmptyLine(this IEnumerable<string> input)
        {
            return string.Join(Environment.NewLine, input)
                .Split(Environment.NewLine + Environment.NewLine)
                .Select(x => x
                    .Split(Environment.NewLine)
                    .ToList());
        }

        public static void Deconstruct<T>(this IEnumerable<T> seq, out T first, out IEnumerable<T> rest)
        {
            first = seq.FirstOrDefault();
            rest = seq.Skip(1);
        }

        public static void Deconstruct<T>(this IEnumerable<T> seq, out T first, out T second, out IEnumerable<T> rest)
            => (first, (second, rest)) = seq;

        public static void Deconstruct<T>(this IEnumerable<T> seq, out T first, out T second, out T third,
            out IEnumerable<T> rest)
            => (first, second, (third, rest)) = seq;

        public static void Deconstruct<T>(this IEnumerable<T> seq, out T first, out T second, out T third, out T fourth,
            out IEnumerable<T> rest)
            => (first, second, third, (fourth, rest)) = seq;

        public static void Deconstruct<T>(this IEnumerable<T> seq, out T first, out T second, out T third, out T fourth,
            out T fifth, out IEnumerable<T> rest)
            => (first, second, third, fourth, (fifth, rest)) = seq;

        public static void Deconstruct<T>(this IEnumerable<T> seq, out T first, out T second, out T third, out T fourth,
            out T fifth, out T sixth, out IEnumerable<T> rest)
            => (first, second, third, fourth, fifth, (sixth, rest)) = seq;

        public static void Deconstruct<T>(this IEnumerable<T> seq, out T first, out T second, out T third, out T fourth,
            out T fifth, out T sixth, out T seventh, out IEnumerable<T> rest)
            => (first, second, third, fourth, fifth, sixth, (seventh, rest)) = seq;
    }
}