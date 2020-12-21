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
        [TestMethod]
        public void TestAnswers()
        {
            Assert.AreEqual(1294, this.Day.Part1());
            Assert.AreEqual(-1, this.Day.Part2());
        }
    }
}
