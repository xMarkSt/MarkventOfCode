using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text.RegularExpressions;
using AdventOfCode2021.Utils;

namespace AdventOfCode2021.Puzzles
{
    public class Day22 : AocPuzzle
    {
        public override int Year => 2021;
        public override int Day => 22;
        protected override void Solve(IEnumerable<string> input)
        {
            var cubes = new HashSet<Cube>();
            var regex = new Regex(@"(\w+) x=(.*)\.\.(.*),y=(.*)\.\.(.*),z=(.*)\.\.(.*)");
            foreach (string line in input)
            {
                var (type, x1, x2, y1, y2, z1, z2, _) = StringUtils.RegexGroupCapture(regex, line);
                for (int x = int.Parse(x1); x <= int.Parse(x2); x++)
                for (int y = int.Parse(y1); y <= int.Parse(y2); y++)
                for (int z = int.Parse(z1); z <= int.Parse(z2); z++)
                {
                    if (type == "on")
                        cubes.Add(new Cube(x, y, z));
                    else
                        cubes.Remove(new Cube(x, y, z));
                }
            }

            PartOne = cubes.Count.ToString();
        }
    }

    internal readonly struct Cube
    {
        private int X { get; }
        private int Y { get; }
        private int Z { get; }

        public Cube(int x, int y, int z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj.GetType() == GetType() && Equals((Cube)obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(X, Y, Z);
        }
        
        private bool Equals(Cube other)
        {
            return X == other.X && Y == other.Y && Z == other.Z;
        }
    }
}