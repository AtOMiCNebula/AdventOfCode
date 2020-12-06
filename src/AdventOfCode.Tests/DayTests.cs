// <copyright file="DayTests.cs" company="Nebulous Industries">
// Copyright (c) Nebulous Industries. All rights reserved.
// </copyright>

namespace NebulousIndustries.AdventOfCode.Tests
{
    using Microsoft.Extensions.DependencyInjection;

    public class DayTests<TDay>
        where TDay : class, IDay
    {
        public DayTests()
        {
            using ServiceProvider provider = ProgramTests.GetTestServiceProvider<TDay>();
            this.Day = provider.GetRequiredService<TDay>();
        }

        protected TDay Day { get; }
    }
}
