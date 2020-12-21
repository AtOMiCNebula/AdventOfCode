// <copyright file="DayTests.cs" company="Nebulous Industries">
// Copyright (c) Nebulous Industries. All rights reserved.
// </copyright>

namespace NebulousIndustries.AdventOfCode.Tests
{
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public abstract class DayTests<TDay>
        where TDay : class, IDay
    {
        protected DayTests()
        {
            using ServiceProvider provider = ProgramTests.GetTestServiceProvider<TDay>();
            this.Day = provider.GetRequiredService<TDay>();
        }

        public abstract long Part1Answer { get; }

        public abstract long Part2Answer { get; }

        protected TDay Day { get; }

        [TestMethod]
        public void TestPart1Answer()
        {
            Assert.AreEqual(this.Part1Answer, this.Day.Part1());
        }

        [TestMethod]
        public void TestPart2Answer()
        {
            Assert.AreEqual(this.Part2Answer, this.Day.Part2());
        }
    }
}
