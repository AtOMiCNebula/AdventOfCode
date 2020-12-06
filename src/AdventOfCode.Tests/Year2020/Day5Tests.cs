// <copyright file="Day5Tests.cs" company="Nebulous Industries">
// Copyright (c) Nebulous Industries. All rights reserved.
// </copyright>

namespace NebulousIndustries.AdventOfCode.Tests.Year2020
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using NebulousIndustries.AdventOfCode.Year2020;

    [TestClass]
    public class Day5Tests : DayTests<Day5>
    {
        [TestMethod]
        public void TestAnswers()
        {
            Assert.AreEqual(913, this.Day.Part1());
            Assert.AreEqual(717, this.Day.Part2());
        }
    }
}
