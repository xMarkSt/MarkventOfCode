using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode2021.Utils;

namespace AdventOfCode2021.Puzzles
{
    public class Day25 : AocPuzzle
    {
        public override int Year => 2021;
        public override int Day => 25;

        private List<string> inputList;

        private const char CHAR_FACE_EAST = '>';
        private const char CHAR_FACE_SOUTH = 'v';
        private const char CHAR_EMPTY = '.';
        protected override void Solve(IEnumerable<string> input)
        {
            inputList = input.ToList();
            bool changes = true;
            int step = 0;
            while(changes)
            {
                var previousList = new List<string>(inputList);
                inputList = MoveEast();
                inputList = MoveSouth();
                if (inputList.SequenceEqual(previousList)) changes = false;
                step++;
            }
            inputList.ForEach(Console.WriteLine);
            PartOne = step.ToString();
        }

        private bool InBounds(int row, int col) => row < inputList.Count && col < inputList[0].Length;

        private List<string> MoveEast()
        {
            var copy = new List<string>(inputList);
            for (int col = 0; col < inputList[0].Length; col++)
            for (int row = 0; row < inputList.Count; row++)
            {
                int nextIndex = InBounds(row, col + 1) ? col + 1 : 0;
                if (inputList[row][col] == CHAR_FACE_EAST && inputList[row][nextIndex] == CHAR_EMPTY)
                {
                    copy[row] = copy[row].ReplaceAt(col, 1, CHAR_EMPTY.ToString());
                    copy[row] = copy[row].ReplaceAt(nextIndex, 1, CHAR_FACE_EAST.ToString());
                }
            }

            return copy;
        }

        private List<string> MoveSouth()
        {
            var copy = new List<string>(inputList);
            for (int row = 0; row < inputList.Count; row++)
            for (int col = 0; col < inputList[0].Length; col++)
            {
                int nextIndex = InBounds(row+1, col) ? row + 1 : 0;
                if (inputList[row][col] == CHAR_FACE_SOUTH && inputList[nextIndex][col] == CHAR_EMPTY)
                {
                    copy[row] = copy[row].ReplaceAt(col, 1, CHAR_EMPTY.ToString());
                    copy[nextIndex] = copy[nextIndex].ReplaceAt(col, 1, CHAR_FACE_SOUTH.ToString());
                }
            }

            return copy;
        }
    }
}