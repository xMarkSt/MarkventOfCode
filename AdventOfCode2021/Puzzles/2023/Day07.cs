using AdventOfCode2021.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021.Puzzles._2023
{
    public class Day07 : AocPuzzle
    {
        public override int Year => 2023;

        public override int Day => 7;

        private enum HandType
        {
            Unknown,
            HighCard,
            OnePair,
            TwoPair,
            ThreeOfAKind,
            FullHouse,
            FourOfAKind,
            FiveOfAKind
        }

        private class Hand
        {
            public Dictionary<char, int> Cards { get; set; }
            public int Bid { get; set; }
            public HandType Type { get; set; }
            public HandType Type2 { get; set; }

            private string cardsText;

            public Hand(string input)
            {
                var (cards, bid, _) = input.Split(' ');
                cardsText = cards;
                Cards = new Dictionary<char, int>();
                foreach (var card in cards)
                {
                    if (Cards.ContainsKey(card))
                    {
                        Cards[card]++;
                    }
                    else
                    {
                        Cards[card] = 1;
                    }
                    Bid = int.Parse(bid);
                }
                Type = FindType();
                Type2 = FindType2();
            }

            private HandType FindType()
            {
                foreach (var val in Cards.Values.OrderByDescending(x => x))
                {
                    if (val == 5)
                    {
                        return HandType.FiveOfAKind;
                    }
                    else if (val == 4)
                    {
                        return HandType.FourOfAKind;
                    }
                    else if (val == 3)
                    {
                        // full house?
                        if (Cards.ContainsValue(2))
                        {
                            return HandType.FullHouse;
                        }
                        // three of a kind
                        else
                        {
                            return HandType.ThreeOfAKind;
                        }
                    }
                    else if (val == 2)
                    {
                        // 2 pair or 1 pair
                        if (Cards.Values.Where(x => x == 2).Count() == 2)
                        {
                            return HandType.TwoPair;
                        }
                        else
                        {
                            return HandType.OnePair;
                        }
                    }
                }

                if (Cards.Values.All(x => x == 1))
                {
                    return HandType.HighCard;
                }

                return HandType.Unknown;
            }

            private HandType FindType2()
            {
                int jokers = Cards.ContainsKey('J') ? Cards['J'] : 0;
                foreach (var val in Cards.Values.OrderByDescending(x => x))
                {
                    int totalVal = val + jokers;
                    if (totalVal == 5)
                    {
                        return HandType.FiveOfAKind;
                    }
                    else if (totalVal == 4)
                    {
                        return HandType.FourOfAKind;
                    }
                    else if (totalVal == 3)
                    {
                        // full house?
                        if (jokers == 0 && Cards.ContainsValue(2))
                        {
                            return HandType.FullHouse;
                        }
                        // three of a kind
                        else
                        {
                            return HandType.ThreeOfAKind;
                        }
                    }
                    else if (totalVal == 2)
                    {
                        // 2 pair?
                        if (Cards.Values.Where(x => x + jokers == 2).Count() == 2)
                        {
                            return HandType.TwoPair;
                        }
                        else
                        {
                            return HandType.OnePair;
                        }
                    }
                }

                if (Cards.Values.All(x => x == 1))
                {
                    return HandType.HighCard;
                }

                return HandType.Unknown;
            }

            public override string ToString()
            {
                return cardsText;
            }
        }

        private class HandComparer : Comparer<Hand>
        {
            private static readonly List<char> strengths = new() { '2', '3', '4', '5', '6', '7', '8', '9', 'T', 'J', 'Q', 'K', 'A' };
            private static readonly List<char> strengths2 = new() { 'J', '2', '3', '4', '5', '6', '7', '8', '9', 'T', 'Q', 'K', 'A' };

            private readonly bool _useJokers;
            public HandComparer(bool useJokers = false)
            {
                _useJokers = useJokers;
            }

            public override int Compare(Hand x, Hand y)
            {
                int xType = _useJokers ? (int)x.Type2 : (int)x.Type;
                int yType = _useJokers ? (int)y.Type2 : (int)y.Type;
                if (xType == yType)
                {
                    for (int i = 0; i < x.ToString().Length; i++)
                    {
                        int xStrength = _useJokers ? strengths2.IndexOf(x.ToString()[i]) : strengths.IndexOf(x.ToString()[i]);
                        int yStrength = _useJokers ? strengths2.IndexOf(y.ToString()[i]) : strengths.IndexOf(y.ToString()[i]);
                        if (xStrength != yStrength)
                        {
                            return xStrength.CompareTo(yStrength);
                        }
                    }
                    // Equal
                    return 0;
                }
                return (xType).CompareTo(yType);
            }
        }
        protected override void Solve(IEnumerable<string> input)
        {
            var handComparer = new HandComparer();
            var handComparer2 = new HandComparer(true);
            var hands = input.Select(x => new Hand(x)).OrderBy(x => x, handComparer).ToList();

            long totalWinnings = 0;

            for (int i = 0; i < hands.Count(); i++)
            {
                int rank = i + 1;
                long score = hands[i].Bid * rank;
                totalWinnings += score;
                //Console.WriteLine("Hand " + hands[i] + " has type " + hands[i].Type + " and rank " + rank);
            }

            PartOne = totalWinnings;

            var hands2 = input.Select(x => new Hand(x)).OrderBy(x => x, handComparer2).ToList();

            totalWinnings = 0;

            for (int i = 0; i < hands2.Count(); i++)
            {
                int rank = i + 1;
                long score = hands2[i].Bid * rank;
                totalWinnings += score;
                Console.WriteLine("Hand " + hands2[i] + " has type " + hands2[i].Type2 + " and rank " + rank);
            }

            PartTwo = totalWinnings;
        }
    }
}
