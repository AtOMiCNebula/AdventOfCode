// <copyright file="Day11Tests.cs" company="Nebulous Industries">
// Copyright (c) Nebulous Industries. All rights reserved.
// </copyright>

namespace NebulousIndustries.AdventOfCode.Tests.Year2020
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using NebulousIndustries.AdventOfCode.Year2020;

    [TestClass]
    public class Day11Tests : DayTests<Day11>
    {
        [TestMethod]
        public void TestAnswers()
        {
            Assert.AreEqual(2476, this.Day.Part1());
            Assert.AreEqual(2257, this.Day.Part2());
        }
    }
}
