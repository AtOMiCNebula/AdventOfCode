// <copyright file="Program.cs" company="Nebulous Industries">
// Copyright (c) Nebulous Industries. All rights reserved.
// </copyright>

namespace NebulousIndustries.AdventOfCode
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Microsoft.Extensions.DependencyInjection;

    public static class Program
    {
        public static void Main(string[] args)
        {
            ServiceCollection collection = new();

            // Discover all day instances, and add them to our collection
            foreach (Type dayType in typeof(Program).Assembly.GetTypes().Where(t => t.IsAssignableTo(typeof(IDay)) && !t.IsAbstract))
            {
                collection.AddTransient(typeof(IDay), dayType);
            }

            using ServiceProvider provider = collection.BuildServiceProvider();

            IEnumerable<IDay> days = provider.GetServices<IDay>().OrderByDescending(d => d.Year).ThenByDescending(d => d.Number);
            if (args.Length >= 1)
            {
                days = days.Where(d => d.Year == int.Parse(args[0]));
                if (args.Length >= 2)
                {
                    days = days.Where(d => d.Number == int.Parse(args[1]));
                }
            }

            IDay day = days.First();
            Console.WriteLine($"Day {day.Year}-{day.Number:D2}:");
            Console.WriteLine($"Part 1 answer: {day.Part1()}");
            Console.WriteLine($"Part 2 answer: {day.Part2()}");
        }
    }
}
