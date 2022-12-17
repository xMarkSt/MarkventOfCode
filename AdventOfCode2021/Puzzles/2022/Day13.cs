using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode2021.Utils;

namespace AdventOfCode2021.Puzzles._2022;

public class Day13 : AocPuzzle
{
    public override int Year => 2022;
    public override int Day => 13;
    protected override void Solve(IEnumerable<string> input)
    {
        List<string[]> pairs = input.Chunk(3).ToList();
        int indicesSum = 0;
        var packets = new List<IPacket>();
        for (int i = 0; i < pairs.Count; i++)
        {
            // Compare pairs
            (string left, string right, _) = pairs[i];
            IPacket leftPacket = ParsePacketString(left);
            IPacket rightPacket = ParsePacketString(right);
            // Right order?
            if (ComparePackets(leftPacket, rightPacket) < 0)
            {
                indicesSum += i + 1;
            }
            packets.Add(leftPacket);
            packets.Add(rightPacket);
        }

        IPacket divider1 = ParsePacketString("[[2]]");
        IPacket divider2 = ParsePacketString("[[6]]");
        packets.Add(divider1);
        packets.Add(divider2);
        packets.Sort();
        
        PartOne = indicesSum;
        PartTwo = (packets.IndexOf(divider1) + 1) * (packets.IndexOf(divider2) + 1);
    }

    private static int ComparePackets(IPacket leftPacket, IPacket rightPacket, int level = 0, bool print = false)
    {
        if (print)
        {
            for (int l = 0; l < level; l++)
            {
                Console.Write("  ");
            }
            Console.Write($"- Compare {leftPacket} vs {rightPacket}");
            Console.WriteLine();
        }

        if (leftPacket is Packet left && rightPacket is Packet right)
            return left.Value.CompareTo(right.Value);
        if (leftPacket is Packet packet)
        {
            leftPacket = packet.ToCompositePacket();
        }
        if (rightPacket is Packet packet2)
        {
            rightPacket = packet2.ToCompositePacket();
        }
        int i = 0;
        while (true)
        {
            // Left ran out of items first
            if (i == leftPacket.Count)
            {
                return -1;
            }
            
            // Right ran out of items first
            if (i == rightPacket.Count)
            {
                return 1;
            }
            int compValue = ComparePackets(leftPacket.GetChild(i), rightPacket.GetChild(i), level+1);
            if (compValue != 0) return compValue;
            i++;
        }
    }

    private static IPacket ParsePacketString(string packetString)
    {
        // Create a stack of composite packets
        var packets = new Stack<CompositePacket>();
        // Loop through each character in the string
        for (int i = 0; i < packetString.Length; i++)
        {
            char c = packetString[i];

            // If the character is '[', push a new composite packet onto the stack
            if (c == '[')
            {
                packets.Push(new CompositePacket());
            }
            // If the character is ']', pop the last composite packet off the stack
            else if (c == ']')
            {
                CompositePacket currPack = packets.Pop();
                // Check if the stack is empty (this is the root packet)
                if (packets.Count == 0)
                {
                    // This is the root packet, return it
                    return currPack;
                }
                // Add the packet to the last composite packet on the stack
                packets.Peek().Add(currPack);
            }
            // If the character is a digit, create a new packet with that value and
            // add it to the last composite packet on the stack
            else if (char.IsDigit(c))
            {
                // // Read all the digits in the number and store them in a string
                string digitString = new(packetString[i..].TakeWhile(char.IsDigit).ToArray());
                // Parse the string of digits into an integer
                int value = int.Parse(digitString);
                IPacket packet = new Packet(value);
                // Add the packet to the last composite packet on the stack
                packets.Peek().Add(packet);
            }
        }
        
        // The remaining packet on the stack should be the root packet
        return packets.Pop();
    }


    private interface IPacket : IComparable<IPacket>
    {
        int Count { get; }
        IPacket GetChild(int i);
    }
    private class CompositePacket : IPacket
    {
        private readonly List<IPacket> _children;

        public CompositePacket()
        {
            _children = new List<IPacket>();
        }

        public int Count => _children.Count;

        public IPacket GetChild(int i)
        {
            return _children[i];
        }

        public void Add(IPacket packet)
        {
            _children.Add(packet);
        }

        public int CompareTo(IPacket other)
        {
            return ComparePackets(this, other);
        }

        public override string ToString()
        {
            return "[" + string.Join(", ", _children) + "]";
        }
    }
    
    private class Packet : IPacket
    {
        public int Value { get; }

        public int Count => 1;
        public Packet(int value)
        {
            Value = value;
        }

        public IPacket GetChild(int i)
        {
            throw new NotImplementedException();
        }

        public CompositePacket ToCompositePacket()
        {
            var result = new CompositePacket();
            result.Add(this);
            return result;
        }

        public override string ToString()
        {
            return Value.ToString();
        }

        public int CompareTo(IPacket other)
        {
            return ComparePackets(this, other);
        }
    }
}