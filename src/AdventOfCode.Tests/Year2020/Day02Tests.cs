// <copyright file="Day02Tests.cs" company="Nebulous Industries">
// Copyright (c) Nebulous Industries. All rights reserved.
// </copyright>

namespace NebulousIndustries.AdventOfCode.Tests.Year2020
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using NebulousIndustries.AdventOfCode.Year2020;

    [TestClass]
    public class Day02Tests : DayTests<Day02>
    {
        [TestMethod]
        public void TestAnswers()
        {
            Assert.AreEqual(572, this.Day.Part1());
            Assert.AreEqual(306, this.Day.Part2());
        }
    }
}
