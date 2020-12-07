// <copyright file="Day7Tests.cs" company="Nebulous Industries">
// Copyright (c) Nebulous Industries. All rights reserved.
// </copyright>

namespace NebulousIndustries.AdventOfCode.Tests.Year2020
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using NebulousIndustries.AdventOfCode.Year2020;

    [TestClass]
    public class Day7Tests : DayTests<Day7>
    {
        [TestMethod]
        public void TestAnswers()
        {
            Assert.AreEqual(296, this.Day.Part1());
            Assert.AreEqual(9339, this.Day.Part2());
        }
    }
}
