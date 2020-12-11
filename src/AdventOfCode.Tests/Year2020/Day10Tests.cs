// <copyright file="Day10Tests.cs" company="Nebulous Industries">
// Copyright (c) Nebulous Industries. All rights reserved.
// </copyright>

namespace NebulousIndustries.AdventOfCode.Tests.Year2020
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using NebulousIndustries.AdventOfCode.Year2020;

    [TestClass]
    public class Day10Tests : DayTests<Day10>
    {
        [TestMethod]
        public void TestAnswers()
        {
            Assert.AreEqual(1700, this.Day.Part1());
            Assert.AreEqual(-1, this.Day.Part2());
        }
    }
}
