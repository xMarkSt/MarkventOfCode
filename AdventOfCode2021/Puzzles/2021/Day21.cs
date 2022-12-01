using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2021.Puzzles
{
    public class Day21 : AocPuzzle
    {
        public override int Year => 2021;
        public override int Day => 21;
        
        private int player1Pos;
        private int player2Pos;
        private int die = 1;
        protected override void Solve(IEnumerable<string> input)
        {
            List<string> inputList = input.ToList();
            int player1Pos = int.Parse(inputList[0][^1..]);
            int player2Pos = int.Parse(inputList[1][^1..]);
            PartOne = Solve1(player1Pos, player2Pos);
            die = 1;
            Solve2(player1Pos, player2Pos);
        }

        private bool Solve2(int player1Pos, int player2Pos, int score1 = 0, int score2 = 0)
        {
            bool player1Turn = true;
            while (score1 < 21 && score2 < 21)
            {
                if (player1Turn)
                {
                    Solve2(player1Pos + 1, player2Pos, score1 + 1, score2);
                    Solve2(player1Pos + 2, player2Pos, score1 + 2, score2);
                    Solve2(player1Pos + 3, player2Pos, score1 + 3, score2);
                    score1 += player1Pos;
                }
                else
                {
                    player2Pos = Dice(player2Pos);

                    score2 += player2Pos;
                }
                player1Turn = !player1Turn;
            }

            return score1 > score2;
        }

        private string Solve1(int player1Pos, int player2Pos)
        {
            int score1 = 0;
            int score2 = 0;
            int diceCount = 0;
            bool player1Turn = true;

            while (score1 < 1000 && score2 < 1000)
            {
                if (player1Turn)
                {
                    player1Pos = Dice(player1Pos);
                    score1 += player1Pos;
                }
                else
                {
                    player2Pos = Dice(player2Pos);

                    score2 += player2Pos;
                }
                player1Turn = !player1Turn;
                diceCount += 3;
            }

            return (score2 * diceCount).ToString();
        }

        private int Dice(int playerPos, int amount = 3)
        {
            for (int i = 0; i < amount; i++)
            {
                playerPos += die;
                die++;
                if (die > 100)
                {
                    die = 1;
                }
            }

            if (playerPos > 10)
            {
                playerPos %= 10;
                if (playerPos == 0) playerPos = 10;
            }
            return playerPos;
        }
    }
}