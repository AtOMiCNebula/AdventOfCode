// <copyright file="Day08Tests.cs" company="Nebulous Industries">
// Copyright (c) Nebulous Industries. All rights reserved.
// </copyright>

namespace NebulousIndustries.AdventOfCode.Tests.Year2020
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using NebulousIndustries.AdventOfCode.Year2020;

    [TestClass]
    public class Day08Tests : DayTests<Day08>
    {
        [TestMethod]
        public void TestAnswers()
        {
            Assert.AreEqual(1654, this.Day.Part1());
            Assert.AreEqual(833, this.Day.Part2());
        }
    }
}
