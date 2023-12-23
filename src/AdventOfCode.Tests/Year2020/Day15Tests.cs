// <copyright file="Day15Tests.cs" company="Nebulous Industries">
// Copyright (c) Nebulous Industries. All rights reserved.
// </copyright>

namespace NebulousIndustries.AdventOfCode.Tests.Year2020
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using NebulousIndustries.AdventOfCode.Year2020;

    [TestClass]
    public class Day15Tests : DayTests<Day15>
    {
        private static readonly int[] Part1StartingNumbers = [0, 3, 6];

        public override long Part1Answer => 1294;

        public override long Part2Answer => 573522;

        [TestMethod]
        [DataRow(1, 0)]
        [DataRow(2, 3)]
        [DataRow(3, 6)]
        [DataRow(4, 0)]
        [DataRow(5, 3)]
        [DataRow(6, 3)]
        [DataRow(7, 1)]
        [DataRow(8, 0)]
        [DataRow(9, 4)]
        [DataRow(10, 0)]
        public void TestPart1Walkthrough(int num, int expectedResult)
        {
            Assert.AreEqual(expectedResult, Day15.GetResult(Part1StartingNumbers, num));
        }

        [TestMethod]
        [DataRow(1, 3, 2, 1)]
        [DataRow(2, 1, 3, 10)]
        [DataRow(1, 2, 3, 27)]
        [DataRow(2, 3, 1, 78)]
        [DataRow(3, 2, 1, 438)]
        [DataRow(3, 1, 2, 1836)]
        public void TestPart1MoreExamples(int first, int second, int third, int expectedResult)
        {
            Assert.AreEqual(expectedResult, Day15.GetResult(new[] { first, second, third }, 2020));
        }

        [TestMethod]
        [DataRow(0, 3, 6, 175594)]
        [DataRow(1, 3, 2, 2578)]
        [DataRow(2, 1, 3, 3544142)]
        [DataRow(1, 2, 3, 261214)]
        [DataRow(2, 3, 1, 6895259)]
        [DataRow(3, 2, 1, 18)]
        [DataRow(3, 1, 2, 362)]
        public void TestPart2MoreExamples(int first, int second, int third, int expectedResult)
        {
            Assert.AreEqual(expectedResult, Day15.GetResult(new[] { first, second, third }, 30000000));
        }
    }
}
