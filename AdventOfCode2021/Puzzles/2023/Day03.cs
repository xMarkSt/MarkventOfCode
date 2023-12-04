using AdventOfCode2021.Datastructures;
using AdventOfCode2021.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode2021.Puzzles._2023
{
    public class Day03 : AocPuzzle
    {
        class Part
        {
            public Vector2Int Pos { get; set; }
            public Vector2Int EndPos { get; set; }
            public int Value { get; set; }

        }
        public override int Year => 2023;

        public override int Day => 3;

        private List<string> _input;
        private HashSet<Vector2Int> _characters;
        private HashSet<Part> _partNumbers;
        private HashSet<Part> _parts;
        private HashSet<Vector2Int> _asterisks;

        protected override void Solve(IEnumerable<string> input)
        {
            _input = input.ToList();
            _parts = new();
            _partNumbers = new();
            _characters = new();
            _asterisks = new();

            long sum = 0;
            

            ParseInput();

            foreach (var part in _parts)
            {
                if (IsPartNumber(part))
                {
                    _partNumbers.Add(part);
                    sum += part.Value;
                }
            }
            PartOne = sum;

            // Part two
            sum = 0;
            foreach(var gear in _asterisks)
            {
                var adjacent = AdjacentPartNumbers(gear);
                if(adjacent.Count == 2)
                {
                    int gearRatio = adjacent.Aggregate(1, (acc, part) => acc * part.Value);
                    sum += gearRatio;
                }
            }
            PartTwo = sum;
        }

        /// <summary>
        /// Parse parts and characters
        /// </summary>
        private void ParseInput()
        {
            string currentNumber = "";
            Vector2Int startPos = null;
            for (int row = 0; row < _input.Count; row++)
            {
                for (int col = 0; col < _input[row].Length; col++)
                {
                    char val = _input[row][col];
                    if (char.IsDigit(val))
                    {
                        if (startPos == null) startPos = new Vector2Int(col, row);
                        currentNumber += val;
                        // At end of line or next pos is not a digit, so add it
                        if (col + 1 == _input[row].Length || !char.IsDigit(_input[row][col + 1]))
                        {
                            _parts.Add(new Part
                            {
                                Pos = startPos,
                                EndPos = new Vector2Int(col, row),
                                Value = int.Parse(currentNumber)
                            });
                            currentNumber = "";
                            startPos = null;
                        }
                    }
                    else if (val != '.')
                    {
                        var pos = new Vector2Int(col, row);
                        _characters.Add(pos);
                        if (val == '*')
                        {
                            _asterisks.Add(pos);
                        }
                    }
                }
            }
        }

        private bool IsPartNumber(Part part)
        {
            for (int i = part.Pos.X; i <= part.EndPos.X; i++)
            {
                var digitPos = new Vector2Int(i, part.Pos.Y);
                for (int x = -1; x <= 1; x++)
                    for (int y = -1; y <= 1; y++)
                    {
                        var checkPos = digitPos + new Vector2Int(x, y);

                        if (_characters.Contains(checkPos))
                        {
                            return true;
                        }
                    }
            }
            return false;
        }

        private HashSet<Part> AdjacentPartNumbers(Vector2Int pos)
        {
            var adjacentParts = new HashSet<Part>();
            for (int x = -1; x <= 1; x++)
                for (int y = -1; y <= 1; y++)
                {
                    var checkPos = pos + new Vector2Int(x, y);
                    var found = _partNumbers.Where(x => x.Pos.Y == checkPos.Y && x.Pos.X <= checkPos.X && checkPos.X <= x.EndPos.X);
                    adjacentParts.UnionWith(found);
                }
            return adjacentParts;
        }
    }
}
