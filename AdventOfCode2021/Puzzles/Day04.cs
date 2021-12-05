using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2021.Puzzles
{
    public class Day04 : AocPuzzle
    {
        public override int Year => 2021;
        public override int Day => 4;
        protected override void Solve(IEnumerable<string> input)
        {
            var inputList = input.ToList();
            var queue = new Queue<int>(inputList[0].Split(',').Select(int.Parse));
            var currentBoard = new BingoBoard();
            var boards = new List<BingoBoard>();
            foreach (string line in inputList.Skip(2))
            {
                if (string.IsNullOrWhiteSpace(line))
                {
                    boards.Add(currentBoard);
                    currentBoard = new BingoBoard();
                }
                else
                {
                    currentBoard.AddRow(line);
                }
            }

            int winnerScore = -1;
            HashSet<BingoBoard> winners = new HashSet<BingoBoard>();
            while (queue.Count > 0)
            {
                int nextNumber = queue.Dequeue();
                foreach (BingoBoard board in boards)
                {
                    board.MarkNumber(nextNumber);
                    if (board.IsBingo)
                    {
                        if (winnerScore == -1)
                        {
                            winnerScore = board.SumUnmarkedNumbers() * nextNumber;
                        }
                        winners.Add(board);
                        if (winners.Count != boards.Count) continue;
                        PartOne = winnerScore.ToString();
                        PartTwo = (board.SumUnmarkedNumbers() * nextNumber).ToString();
                        return;
                    }
                }
            }
        }
    }

    internal class BingoBoard
    {
        public BingoNumber[][] Rows { get; }
        private int _currentRow;
        private bool _bingo;
        internal BingoBoard()
        {
            Rows = new BingoNumber[5][];
        }

        internal void AddRow(string line)
        {
            var row = line
                .Split(Array.Empty<char>(), StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToList();
            Rows[_currentRow] = new BingoNumber[5];
            for (int i = 0; i < row.Count; i++)
            {
                Rows[_currentRow][i] = new BingoNumber(row[i]);
            }
            _currentRow++;
        }

        internal void MarkNumber(int toMark)
        {
            for (int j = 0; j < Rows.Length; j++)
            {
                BingoNumber[] row = Rows[j];
                for (int i = 0; i < row.Length; i++)
                {
                    if (row[i].Number == toMark)
                    {
                        row[i].Marked = true;
                        CheckBingo(j, i);
                    }
                }
            }
        }

        internal int SumUnmarkedNumbers()
        {
            return Rows
                .Sum(row => row
                    .Where(number => !number.Marked)
                    .Sum(number => number.Number)
                );
        }

        internal bool IsBingo => _bingo;

        private void CheckBingo(int row, int col)
        {
            // Check if row is bingo
            bool rowBingo = true;
            foreach (BingoNumber number in Rows[row])
            {
                if (!number.Marked)
                {
                    rowBingo = false;
                }
            }

            if (rowBingo)
            {
                _bingo = true;
                return;
            }

            // Check if col is bingo
            bool colBingo = true;
            for (int i = 0; i < Rows[row].Length; i++)
            {
                if (!Rows[i][col].Marked)
                {
                    colBingo = false;
                }
            }

            if (colBingo)
            {
                _bingo = true;
            }
        }
    }

    internal struct BingoNumber
    {
        public int Number { get; set; }
        public bool Marked { get; set; }

        public BingoNumber(int number) : this()
        {
            Number = number;
        }
    }
}