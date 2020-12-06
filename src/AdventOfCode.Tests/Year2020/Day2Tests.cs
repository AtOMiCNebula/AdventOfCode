// <copyright file="Day2Tests.cs" company="Nebulous Industries">
// Copyright (c) Nebulous Industries. All rights reserved.
// </copyright>

namespace NebulousIndustries.AdventOfCode.Tests.Year2020
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using NebulousIndustries.AdventOfCode.Year2020;

    [TestClass]
    public class Day2Tests : DayTests<Day2>
    {
        [TestMethod]
        public void TestAnswers()
        {
            Assert.AreEqual(572, this.Day.Part1());
            Assert.AreEqual(306, this.Day.Part2());
        }
    }
}
