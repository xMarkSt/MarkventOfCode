using AdventOfCode2021.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode2021.Puzzles._2023
{
    public class Day04 : AocPuzzle
    {
        public override int Year => 2023;

        public override int Day => 4;

        class Card
        {
            public int CardNumber { get; set; }
            public int Amount { get; set; }

            public HashSet<int> Winning { get; set; }
            public HashSet<int> Owned { get; set; }
            public IEnumerable<int> Matching => Owned.Intersect(Winning);

            public Card(string line)
            {
                Amount = 1;
                var split = line.Split(": ");
                CardNumber = int.Parse(split[0].Split("Card ")[1]);
                var split2 = split[1].Split(" | ");
                Winning = split2[0].Split(' ', StringSplitOptions.RemoveEmptyEntries)
                    .AsIntList().ToHashSet();
                Owned = split2[1].Split(' ', StringSplitOptions.RemoveEmptyEntries)
                    .AsIntList().ToHashSet();
            }
        }

        protected override void Solve(IEnumerable<string> input)
        {
            IEnumerable<Card> cards = input.Select(x => new Card(x));
            var cardsDict = cards.ToDictionary(x => x.CardNumber);

            int totalPoints = 0;
            foreach(var card in cards)
            {
                int points = 0;
                var matching = card.Matching;
                int matchCount = matching.Count();
                if(matchCount > 0)
                {
                    points = 1;
                    if(matchCount > 1)
                    {
                        points = (int)Math.Pow(2, matching.Count()-1);
                    }
                    // Part 2
                    // Get copies
                    for(int i = card.CardNumber + 1; i <= card.CardNumber + matchCount; i++) 
                    {
                        cardsDict[i].Amount += cardsDict[card.CardNumber].Amount;
                    }
                }
                totalPoints += points;
            }
            PartOne = totalPoints;

            int total = 0;
            foreach(var card in cardsDict.Values)
            {
                total += card.Amount;
            }
            PartTwo = total;
        }
    }
}
