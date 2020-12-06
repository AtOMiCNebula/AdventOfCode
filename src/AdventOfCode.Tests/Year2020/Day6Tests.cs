﻿// <copyright file="Day6Tests.cs" company="Nebulous Industries">
// Copyright (c) Nebulous Industries. All rights reserved.
// </copyright>

namespace NebulousIndustries.AdventOfCode.Tests.Year2020
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using NebulousIndustries.AdventOfCode.Year2020;

    [TestClass]
    public class Day6Tests : DayTests<Day6>
    {
        [TestMethod]
        public void TestAnswers()
        {
            Assert.AreEqual(6170, this.Day.Part1());
            Assert.AreEqual(2947, this.Day.Part2());
        }
    }
}
