﻿// <copyright file="Day9Tests.cs" company="Nebulous Industries">
// Copyright (c) Nebulous Industries. All rights reserved.
// </copyright>

namespace NebulousIndustries.AdventOfCode.Tests.Year2020
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using NebulousIndustries.AdventOfCode.Year2020;

    [TestClass]
    public class Day9Tests : DayTests<Day9>
    {
        [TestMethod]
        public void TestAnswers()
        {
            Assert.AreEqual(375054920, this.Day.Part1());
            Assert.AreEqual(54142584, this.Day.Part2());
        }
    }
}
