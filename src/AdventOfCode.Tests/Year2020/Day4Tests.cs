// <copyright file="Day4Tests.cs" company="Nebulous Industries">
// Copyright (c) Nebulous Industries. All rights reserved.
// </copyright>

namespace NebulousIndustries.AdventOfCode.Tests.Year2020
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using NebulousIndustries.AdventOfCode.Year2020;

    [TestClass]
    public class Day4Tests : DayTests<Day4>
    {
        [TestMethod]
        public void TestAnswers()
        {
            Assert.AreEqual(208, this.Day.Part1());
            Assert.AreEqual(167, this.Day.Part2());
        }
    }
}
