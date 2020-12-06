// <copyright file="Day1Tests.cs" company="Nebulous Industries">
// Copyright (c) Nebulous Industries. All rights reserved.
// </copyright>

namespace NebulousIndustries.AdventOfCode.Tests.Year2020
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using NebulousIndustries.AdventOfCode.Year2020;

    [TestClass]
    public class Day1Tests : DayTests<Day1>
    {
        [TestMethod]
        public void TestAnswers()
        {
            Assert.AreEqual(703131, this.Day.Part1());
            Assert.AreEqual(272423970, this.Day.Part2());
        }
    }
}
