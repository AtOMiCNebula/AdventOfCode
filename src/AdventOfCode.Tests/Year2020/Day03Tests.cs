// <copyright file="Day03Tests.cs" company="Nebulous Industries">
// Copyright (c) Nebulous Industries. All rights reserved.
// </copyright>

namespace NebulousIndustries.AdventOfCode.Tests.Year2020
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using NebulousIndustries.AdventOfCode.Year2020;

    [TestClass]
    public class Day03Tests : DayTests<Day03>
    {
        [TestMethod]
        public void TestAnswers()
        {
            Assert.AreEqual(173, this.Day.Part1());
            Assert.AreEqual(4385176320, this.Day.Part2());
        }
    }
}
