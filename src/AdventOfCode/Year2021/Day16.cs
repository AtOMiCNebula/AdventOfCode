// <copyright file="Day16.cs" company="Nebulous Industries">
// Copyright (c) Nebulous Industries. All rights reserved.
// </copyright>

namespace NebulousIndustries.AdventOfCode.Year2021
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    public class Day16 : DayBase<BITSPacket>
    {
        public override long Part1()
        {
            BITSPacket packet = this.GetInput().First();
            return packet.VersionSum;
        }

        public override long Part2()
        {
            BITSPacket packet = this.GetInput().First();
            return packet.Value;
        }
    }

    public class BITSPacket : IDayInput
    {
        public enum PacketType
        {
            OperatorSum = 0,
            OperatorProduct = 1,
            OperatorMinimum = 2,
            OperatorMaximum = 3,
            LiteralValue = 4,
            OperatorGreaterThan = 5,
            OperatorLessThan = 6,
            OperatorEqualTo = 7,
        }

        public static int BitsRead { get; set; }

        public int Version { get; set; }

        public PacketType Type { get; set; }

        public long LiteralValue { get; set; }

        public int VersionSum => this.Version + this.SubPackets.Sum(p => p.VersionSum);

        public long Value
        {
            get
            {
                return this.Type switch
                {
                    PacketType.OperatorSum => this.SubPackets.Sum(p => p.Value),
                    PacketType.OperatorProduct => this.SubPackets.Aggregate(1L, (acc, p) => acc * p.Value),
                    PacketType.OperatorMinimum => this.SubPackets.Min(p => p.Value),
                    PacketType.OperatorMaximum => this.SubPackets.Max(p => p.Value),
                    PacketType.LiteralValue => this.LiteralValue,
                    PacketType.OperatorGreaterThan => this.SubPackets[0].Value > this.SubPackets[1].Value ? 1 : 0,
                    PacketType.OperatorLessThan => this.SubPackets[0].Value < this.SubPackets[1].Value ? 1 : 0,
                    PacketType.OperatorEqualTo => this.SubPackets[0].Value == this.SubPackets[1].Value ? 1 : 0,
                    _ => throw new InvalidOperationException(),
                };
            }
        }

        public IList<BITSPacket> SubPackets { get; } = new List<BITSPacket>();

        public void InitializeNewParse()
        {
            BitsRead = 0;
        }

        public bool Load(string input)
        {
            string inputBinary = string.Concat(input.Select(c => Convert.ToString(Convert.ToByte($"{c}", 16), 2).PadLeft(4, '0')));
            using MemoryStream stream = new();
            using (StreamWriter writer = new(stream, leaveOpen: true))
            {
                writer.Write(inputBinary);
            }

            stream.Seek(0, SeekOrigin.Begin);
            using StreamReader reader = new(stream);
            this.Load(reader);
            return false;
        }

        public void Load(StreamReader input)
        {
            this.Version = ReadBits(input, 3);
            this.Type = (PacketType)ReadBits(input, 3);

            switch (this.Type)
            {
                case PacketType.LiteralValue:
                    this.ParseLiteralValue(input);
                    break;

                case PacketType.OperatorSum:
                case PacketType.OperatorProduct:
                case PacketType.OperatorMinimum:
                case PacketType.OperatorMaximum:
                case PacketType.OperatorGreaterThan:
                case PacketType.OperatorLessThan:
                case PacketType.OperatorEqualTo:
                    this.ParseOperator(input);
                    break;
            }
        }

        public void ParseLiteralValue(StreamReader input)
        {
            while (true)
            {
                bool more = ReadBits(input, 1) != 0;
                int chunk = ReadBits(input, 4);
                this.LiteralValue = (this.LiteralValue * 16) + chunk;
                if (!more)
                {
                    break;
                }
            }
        }

        public void ParseOperator(StreamReader input)
        {
            bool readNumberOfSubPackets = ReadBits(input, 1) != 0;
            if (!readNumberOfSubPackets)
            {
                // Read 15 bits for length of subpacket data
                int length = ReadBits(input, 15);
                long startPosition = BitsRead;
                while (BitsRead < startPosition + length)
                {
                    BITSPacket subpacket = new();
                    subpacket.Load(input);
                    this.SubPackets.Add(subpacket);
                }
            }
            else
            {
                // Read 11 bits for number of subpackets
                int number = ReadBits(input, 11);
                for (int p = 0; p < number; p++)
                {
                    BITSPacket subpacket = new();
                    subpacket.Load(input);
                    this.SubPackets.Add(subpacket);
                }
            }
        }

        protected static int ReadBits(StreamReader input, int bits)
        {
            BitsRead += bits;
            char[] data = new char[bits];
            input.Read(data);
            return Convert.ToInt32(new string(data), 2);
        }
    }
}
