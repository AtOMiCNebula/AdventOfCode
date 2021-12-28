// <copyright file="Day16Tests.cs" company="Nebulous Industries">
// Copyright (c) Nebulous Industries. All rights reserved.
// </copyright>

namespace NebulousIndustries.AdventOfCode.Tests.Year2021
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using NebulousIndustries.AdventOfCode.Year2021;

    [TestClass]
    public class Day16Tests : DayTests<Day16>
    {
        public override long Part1Answer => 923;

        public override long Part2Answer => 258888628940;

        [TestMethod]
        public void TestSimplePacket()
        {
            BITSPacket packet = new();
            packet.Load("D2FE28");

            Assert.AreEqual(6, packet.Version);
            Assert.AreEqual(BITSPacket.PacketType.LiteralValue, packet.Type);
            Assert.AreEqual(2021, packet.LiteralValue);
        }

        [TestMethod]
        public void TestSimpleSubpacket1()
        {
            BITSPacket packet = new();
            packet.Load("38006F45291200");

            Assert.AreEqual(1, packet.Version);
            Assert.AreEqual((BITSPacket.PacketType)6, packet.Type);
            Assert.AreEqual(2, packet.SubPackets.Count);
            Assert.AreEqual(10, packet.SubPackets[0].LiteralValue);
            Assert.AreEqual(20, packet.SubPackets[1].LiteralValue);
        }

        [TestMethod]
        public void TestSimpleSubpacket2()
        {
            BITSPacket packet = new();
            packet.Load("EE00D40C823060");

            Assert.AreEqual(7, packet.Version);
            Assert.AreEqual((BITSPacket.PacketType)3, packet.Type);
            Assert.AreEqual(3, packet.SubPackets.Count);
            Assert.AreEqual(1, packet.SubPackets[0].LiteralValue);
            Assert.AreEqual(2, packet.SubPackets[1].LiteralValue);
            Assert.AreEqual(3, packet.SubPackets[2].LiteralValue);
        }

        [DataTestMethod]
        [DataRow("C200B40A82", BITSPacket.PacketType.OperatorSum, 3)]
        [DataRow("04005AC33890", BITSPacket.PacketType.OperatorProduct, 54)]
        [DataRow("880086C3E88112", BITSPacket.PacketType.OperatorMinimum, 7)]
        [DataRow("CE00C43D881120", BITSPacket.PacketType.OperatorMaximum, 9)]
        [DataRow("D8005AC2A8F0", BITSPacket.PacketType.OperatorLessThan, 1)]
        [DataRow("F600BC2D8F", BITSPacket.PacketType.OperatorGreaterThan, 0)]
        [DataRow("9C005AC2F8F0", BITSPacket.PacketType.OperatorEqualTo, 0)]
        [DataRow("9C0141080250320F1802104A08", BITSPacket.PacketType.OperatorEqualTo, 1)]
        public void TestOperators(string input, BITSPacket.PacketType expectedType, long expectedValue)
        {
            BITSPacket packet = new();
            packet.Load(input);

            Assert.AreEqual(expectedType, packet.Type);
            Assert.AreEqual(expectedValue, packet.Value);
        }
    }
}
