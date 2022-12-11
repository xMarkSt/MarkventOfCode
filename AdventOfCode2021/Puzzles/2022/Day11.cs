using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode2021.Utils;

namespace AdventOfCode2021.Puzzles._2022;

public class Day11 : AocPuzzle
{
    public override int Year => 2022;
    public override int Day => 11;

    private List<Monkey> _monkeys;
    
    private static long _lcm;

    protected override void Solve(IEnumerable<string> input)
    {
        // Only does part 2 atm
        _monkeys = new List<Monkey>();
        List<string[]> chunks = input.Chunk(7).ToList();
        // Find lcm of all division test
        IEnumerable<long> divisions = 
            chunks.Select(x => long.Parse(x[3]
                .Split("divisible by ")[1]));
        _lcm = MathUtils.Lcm(divisions);
        
        foreach (string[] monke in chunks)
        {
            var newMonke = new Monkey(_monkeys);
            IEnumerable<long> items = monke[1][18..].Split(", ").AsLongList();
            newMonke.Items = new Queue<long>(items);
            string opStr = monke[2].Split("Operation: new = ")[1];
            newMonke.ParseOperation(opStr);
            newMonke.DivisibleBy = int.Parse(monke[3].Split("divisible by ")[1]);
            newMonke.MonkeyThrowIndexTrue = int.Parse(monke[4].Split("If true: throw to monkey ")[1]);
            newMonke.MonkeyThrowIndexFalse = int.Parse(monke[5].Split("If false: throw to monkey ")[1]);

            _monkeys.Add(newMonke);
        }

        for (int i = 0; i < 10000; i++)
        {
            foreach (Monkey monkey in _monkeys)
            {
                monkey.TakeTurn();
            }
        }

        // Round finished
        long monkeyBusiness = _monkeys
            .OrderByDescending(m => m.InspectionsCount)
            .Select(m => m.InspectionsCount)
            .Take(2)
            .Aggregate((long)1, (acc, val) => acc * val);
        PartTwo = monkeyBusiness;
    }

    private class Monkey
    {
        private static List<Monkey> _monkeys;
        public Queue<long> Items { get; set; }
        private Func<long, long> _operation;
        public long DivisibleBy { get; set; }

        public int MonkeyThrowIndexTrue { get; set; }
        public int MonkeyThrowIndexFalse { get; set; }

        public long InspectionsCount { get; private set; }
        
        public Monkey(List<Monkey> monkeys)
        {
            _monkeys = monkeys;
        }
        public void TakeTurn()
        {
            while (Items.Count != 0)
            {
                long worryLevel = DoOperation(Items.Dequeue());
                worryLevel %= _lcm;
                int index = worryLevel % DivisibleBy == 0 ? MonkeyThrowIndexTrue : MonkeyThrowIndexFalse;
                _monkeys[index].Items.Enqueue(worryLevel);
                InspectionsCount++;
            }
        }
        public void ParseOperation(string operatorString)
        {
            (string a, string op, string b, _) = operatorString.Split(" ");
            
            _operation = old =>
            {
                long opA = a == "old" ? old : long.Parse(a);
                long opB = b == "old" ? old : long.Parse(b);

                return op switch
                {
                    "*" => opA * opB,
                    "+" => opA + opB,
                    _ => -1
                };
            };
        }
        
        private long DoOperation(long worryLevel) => _operation(worryLevel);
    }
}