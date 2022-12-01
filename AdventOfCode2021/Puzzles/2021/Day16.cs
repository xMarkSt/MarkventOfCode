using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AdventOfCode2021.Utils;

namespace AdventOfCode2021.Puzzles
{
    public class Day16 : AocPuzzle
    {
        public override int Year => 2021;
        public override int Day => 16;

        private int _versionSum = 0;
        protected override void Solve(IEnumerable<string> input)
        {
            // var list = input.Select(x => Convert.ToString(Convert.ToInt64(x, 16), 2)).ToList();
            string binary = HexStringToBinary(input.First());
            var res = ProcessPacket(binary);
            PartOne = _versionSum.ToString();
            PartTwo = res.Value.ToString();
        }

        private (long Length, long Value) ProcessPacket(string binary, long subAmount = int.MaxValue, int subType = -1)
        {
            long currentBit = 0;
            int subs = 0;
            List<long> subPacketValues = new List<long>();
            while (binary[(int)currentBit..].Contains('1') && subs < subAmount)
            {
                int version = Convert.ToInt32(binary.Substring((int)currentBit, 3), 2);
                _versionSum += version;
                currentBit += 3;
                int packetType = Convert.ToInt32(binary.Substring((int)currentBit, 3), 2);
                currentBit += 3;
                // literal value
                if (packetType == 4)
                {
                    string bits = "";
                    long i = currentBit;
                    bool stop = false;
                    do
                    {
                        bits += binary.Substring((int)(i + 1), 4);
                        if (binary[(Index)i] == '0') stop = true;
                        i += 5;
                    } while (!stop && i < binary.Length);

                    currentBit = i;
                    subPacketValues.Add(ConversionUtils.Bin2Int(bits));
                }
                else // operator, next is length
                {
                    long lengthType = ConversionUtils.Bin2Int(binary[(Index)currentBit].ToString());
                    currentBit++;
                    if (lengthType == 0)
                    {
                        string substr = binary.Substring((int)currentBit, 15);
                        long length = ConversionUtils.Bin2Int(substr);
                        currentBit += 15;
                        (long _, long val) = ProcessPacket(binary.Substring((int)currentBit, (int)length), int.MaxValue, packetType);
                        subPacketValues.Add(val);
                        currentBit += length;
                    }
                    else
                    {
                        string substr = binary.Substring((int)currentBit, 11);
                        currentBit += 11;
                        long subPacketAmount = ConversionUtils.Bin2Int(substr);
                        (long length, long value) = ProcessPacket(binary[(int)currentBit..], subPacketAmount, packetType);
                        currentBit += length;
                        subPacketValues.Add(value);
                    }
                }

                subs++;
            }

            long subPacketResult = subType switch
            {
                0 => subPacketValues.Sum(),
                1 => subPacketValues.Aggregate(1, (long acc, long val) => acc * val),
                2 => subPacketValues.Min(),
                3 => subPacketValues.Max(),
                5 => Convert.ToInt32(subPacketValues[0] > subPacketValues[1]),
                6 => Convert.ToInt32(subPacketValues[0] < subPacketValues[1]),
                7 => Convert.ToInt32(subPacketValues[0] == subPacketValues[1]),
                _ => subPacketValues[0]
            };

            return (currentBit, subPacketResult);
        }
        
        private string HexStringToBinary(string hex) {
            StringBuilder result = new StringBuilder();
            foreach (char c in hex) {
                result.Append(HexCharacterToBinary[char.ToLower(c)]);
            }
            return result.ToString();
        }
        
        private static readonly Dictionary<char, string> HexCharacterToBinary = new Dictionary<char, string> {
            { '0', "0000" },
            { '1', "0001" },
            { '2', "0010" },
            { '3', "0011" },
            { '4', "0100" },
            { '5', "0101" },
            { '6', "0110" },
            { '7', "0111" },
            { '8', "1000" },
            { '9', "1001" },
            { 'a', "1010" },
            { 'b', "1011" },
            { 'c', "1100" },
            { 'd', "1101" },
            { 'e', "1110" },
            { 'f', "1111" }
        };
    }
}