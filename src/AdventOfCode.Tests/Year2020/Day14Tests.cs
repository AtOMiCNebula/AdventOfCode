// <copyright file="Day14Tests.cs" company="Nebulous Industries">
// Copyright (c) Nebulous Industries. All rights reserved.
// </copyright>

namespace NebulousIndustries.AdventOfCode.Tests.Year2020
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using NebulousIndustries.AdventOfCode.Year2020;

    [TestClass]
    public class Day14Tests : DayTests<Day14>
    {
        [TestMethod]
        public void TestAnswers()
        {
            Assert.AreEqual(14954914379452, this.Day.Part1());
            Assert.AreEqual(3415488160714, this.Day.Part2());
        }
    }
}
