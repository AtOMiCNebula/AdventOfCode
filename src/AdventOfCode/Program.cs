﻿// <copyright file="Program.cs" company="Nebulous Industries">
// Copyright (c) Nebulous Industries. All rights reserved.
// </copyright>

namespace NebulousIndustries.AdventOfCode
{
    using System;
    using System.Linq;

    using Microsoft.Extensions.DependencyInjection;

    public static class Program
    {
        public static void Main()
        {
            ServiceCollection collection = new ServiceCollection();

            // Discover all day instances, and add them to our collection
            foreach (Type dayType in typeof(Program).Assembly.GetTypes().Where(t => t.IsAssignableTo(typeof(IDay)) && !t.IsAbstract))
            {
                collection.AddTransient(typeof(IDay), dayType);
            }

            using ServiceProvider provider = collection.BuildServiceProvider();

            IDay day = provider.GetServices<IDay>().OrderByDescending(d => d.Number).First();
            Console.WriteLine($"Part 1 answer: {day.Part1()}");
            Console.WriteLine($"Part 2 answer: {day.Part2()}");
        }
    }
}
