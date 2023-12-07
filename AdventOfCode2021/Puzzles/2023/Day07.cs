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

        private class Hand
        {
            public Dictionary<char, int> Cards { get; set; }
            public int Bid { get; set; }

            public Hand(string input)
            {
                var (cards, bid, _) = input.Split(' ');
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
                FindType();
            }

            private void FindType()
            {
                foreach (var val in Cards.Values)
                {
                    if (val == 5)
                    {
                        // 5 of kind
                    }
                    else if (val == 4)
                    {
                        // 4 of kind
                    }
                    else if (val == 3)
                    {
                        // full house?
                        if(Cards.ContainsValue(2))
                        {

                        }
                        // three of a kind
                        else
                        {

                        }
                    }
                    else if (val == 2)
                    {
                        // 2 pair or 1 pair
                        if (Cards.Values.Where(x => x == 2).Count() == 2)
                        {

                        }
                    }
                }
            }
        }
        protected override void Solve(IEnumerable<string> input)
        {
            var hands = input.Select(x => new Hand(x)).ToList();
        }
    }
}
