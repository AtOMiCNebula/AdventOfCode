// <copyright file="ProgramTests.cs" company="Nebulous Industries">
// Copyright (c) Nebulous Industries. All rights reserved.
// </copyright>

namespace NebulousIndustries.AdventOfCode.Tests
{
    using Microsoft.Extensions.DependencyInjection;

    public static class ProgramTests
    {
        public static ServiceProvider GetTestServiceProvider<TDay>()
            where TDay : class, IDay
        {
            ServiceCollection collection = new();
            collection.AddTransient<TDay>();

            return collection.BuildServiceProvider();
        }
    }
}
