using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Numerics;
using System.Text;

namespace AdventOfCode2021.Puzzles
{
    public class Day18 : AocPuzzle
    {
        public override int Year => 2021;
        public override int Day => 18;
        protected override void Solve(IEnumerable<string> input)
        {
            List<string> inputList = input.ToList();
            string previous = inputList[0];
            string sum = "";
            foreach (string s in inputList.Skip(1))
            {
                sum = Add(previous, s);
                string oldsum = "";
                while (sum != oldsum || Nested(sum).HasValue)
                {
                    oldsum = sum;
                    (int first, int firstLength, int second, int secondLength)? nested = Nested(sum);
                    while (nested.HasValue)
                    {
                        sum = Explode(sum, nested.Value.first, nested.Value.firstLength, nested.Value.second, nested.Value.secondLength);
                        nested = Nested(sum);
                    }
                    sum = Split(sum);
                }

                previous = sum;
            }

            PartOne = Magnitude(sum).ToString();
            PartTwo = SolvePartTwo(inputList);
        }

        private string SolvePartTwo(List<string> inputList)
        {
            int highestMagnitude = 0;
            foreach (string s in inputList)
            {
                foreach (string s2 in inputList)
                {
                    string sum = "";
                    if(s == s2) continue;;
                    sum = Add(s, s2);
                    bool reduced = false;
                    while (!reduced)
                    {
                        (int first, int firstLength, int second, int secondLength)? nested = Nested(sum);
                        while (nested.HasValue)
                        {
                            sum = Explode(sum, nested.Value.first, nested.Value.firstLength, nested.Value.second,
                                nested.Value.secondLength);
                            nested = Nested(sum);
                        }
                        string oldsum = sum;
                        sum = Split(sum);
                        if (sum == oldsum && !Nested(sum).HasValue) reduced = true;
                    }

                    int magnitude = Magnitude(sum);
                    if (magnitude > highestMagnitude) highestMagnitude = magnitude;
                }
            }

            return highestMagnitude.ToString();
        }

        private string Split(string pair)
        {
            var builder = new StringBuilder(pair);
            for (int i = 1; i < pair.Length; i++)
            {
                if (char.IsDigit(pair[i]) && char.IsDigit(pair[i - 1]))
                {
                    double num = Convert.ToDouble(pair.Substring(i - 1, 2));
                    string split = SplitInternal(num);
                    
                    builder.Remove(i - 1, 2);
                    builder.Insert(i - 1, split);
                    break;
                }
            }
            return builder.ToString();
        }

        private string SplitInternal(double bigNumber)
        {
            if (bigNumber < 10) return bigNumber.ToString(CultureInfo.InvariantCulture);
            double num = bigNumber;
            double first = Math.Floor(num / 2);
            string firstString = first.ToString();
            double second = Math.Ceiling(num / 2);
            string secondString = second.ToString();
            return $"[{firstString},{secondString}]";
        }

        private string Explode(string pair, int index1, int length1, int index2, int length2)
        {
            int index1Num = int.Parse(pair.Substring(index1, length1));
            int index2Num = int.Parse(pair.Substring(index2, length2));
            var builder = new StringBuilder(pair);
            // search left
            SearchLeft(builder, index1, index1Num);
            int diff = builder.Length - pair.Length;
            index1 += diff;
            index2 += diff;
            // search right
            SearchRight(builder, index2, index2Num);

            int length = length1 + length2 + 3;
            builder.Remove(index1 - 1, length);
            builder.Insert(index1 - 1, 0);
            return builder.ToString();
        }

        private void SearchLeft(StringBuilder builder, int index, int value)
        {
            for (int i = index - 1; i >= 0; i--)
            {
                if (!char.IsDigit(builder[i])) continue;
                int length = 1;
                if (char.IsDigit(builder[i - 1]))
                {
                    i--;
                    length = 2;
                }
                int numLeft = int.Parse(builder.ToString().Substring(i, length));
                int number = numLeft + value;
                builder.Remove(i, length);
                builder.Insert(i, number);
                return;
            }
        }

        private void SearchRight(StringBuilder builder, int index, int value)
        {
            for (int i = index + value.ToString().Length; i < builder.Length; i++)
            {
                if (!char.IsDigit(builder[i])) continue;
                int rightLength = 1;
                if (char.IsDigit(builder[i + 1])) rightLength = 2;
                int numRight = int.Parse(builder.ToString().Substring(i, rightLength));
                int number = numRight + value;
                builder.Remove(i, rightLength);
                builder.Insert(i, number);
                return;
            }
        }

        private string Add(string one, string two)
        {
            return $"[{one},{two}]";
        }

        private (int first, int firstLength, int second, int secondLength)? Nested(string pair)
        {
            int nestCount = 0;
            for (int i = 0; i < pair.Length; i++)
            {
                char c = pair[i];
                if (c == '[')
                {
                    nestCount++;
                    if (nestCount == 5)
                    {
                        if (!char.IsDigit(pair[i+1])) continue;
                        int secondPos = i + 3;
                        int firstLength = 1;
                        int secondLength = 1;
                        // Double digit
                        if(char.IsDigit(pair[i+2]))
                        {
                            firstLength = 2;
                            secondPos = i + 4;
                            if (char.IsDigit(pair[i + 5])) secondLength = 2;
                        }
                        else if (char.IsDigit(pair[i + 4])) secondLength = 2;
                        return (i+1, firstLength, secondPos, secondLength);
                    }
                }
                else if (c == ']') nestCount--;
            }

            return null;
        }

        private int Magnitude(string pair)
        {
            if (pair == "") return 0;
            if (!pair.Contains('[')) return int.Parse(pair);
            int nestCount = 0;
            int commaPos = -1;
            for (int i = 0; i < pair.Length; i++)
            {
                if (pair[i] == '[')
                {
                    nestCount++;
                }
                else if (pair[i] == ']') nestCount--;
                else if (pair[i] == ',' && nestCount == 1)
                {
                    commaPos = i;
                    break;
                }
            }
            if (commaPos == -1) throw new Exception("Invalid syntax");
            string left = pair.Substring(1, commaPos - 1);
            string right = pair.Substring(commaPos + 1, pair.Length - commaPos - 2);
            return Magnitude(left) * 3 + Magnitude(right) * 2;
        }
    }
}