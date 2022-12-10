using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode2021.Utils;

namespace AdventOfCode2021.Puzzles._2022;

public class Day10 : AocPuzzle
{
    public override int Year => 2022;
    public override int Day => 10;

    private int x = 1;
    private string crt;

    private readonly List<int> interesting = new()
    {
        20, 60, 100, 140, 180, 220
    };

    private int signalSum;
    private int _cycle;

    private void DrawPixel()
    {
        int currentRowXPos = (_cycle - 1) % 40;

        if (x - 1 <= currentRowXPos && currentRowXPos <= x + 1)
        {
            crt += "#";
        }
        else
        {
            crt += ".";
        }
        
        if (currentRowXPos == 39)
        {
            crt += Environment.NewLine;
        }
    }

    protected override void Solve(IEnumerable<string> input)
    {
        foreach (string line in input)
        {
            (string instruction, string v, _) = line.Split(' ');
            switch (instruction)
            {
                case "noop":
                    DoCycles(1);
                    break;
                case "addx":
                    DoCycles(2);
                    x += int.Parse(v);
                    break;
            }
        }

        PartOne = signalSum;

        Console.Write(crt);
        Console.WriteLine();
    }

    private void DoCycles(int cycles)
    {
        for (int i = 0; i < cycles; i++)
        {
            _cycle++;
            DrawPixel();
            if (interesting.Contains(_cycle))
            {
                int signalStrength = _cycle * x;
                signalSum += signalStrength;
            }
        }
    }
}