// <copyright file="Day12Tests.cs" company="Nebulous Industries">
// Copyright (c) Nebulous Industries. All rights reserved.
// </copyright>

namespace NebulousIndustries.AdventOfCode.Tests.Year2020
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using NebulousIndustries.AdventOfCode.Year2020;

    [TestClass]
    public class Day12Tests : DayTests<Day12>
    {
        [TestMethod]
        public void TestAnswers()
        {
            Assert.AreEqual(757, this.Day.Part1());
            Assert.AreEqual(51249, this.Day.Part2());
        }
    }
}
