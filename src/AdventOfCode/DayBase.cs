// <copyright file="DayBase.cs" company="Nebulous Industries">
// Copyright (c) Nebulous Industries. All rights reserved.
// </copyright>

namespace NebulousIndustries.AdventOfCode
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    public abstract class DayBase<T> : IDay
        where T : IDayInput, new()
    {
        public abstract int Number { get; }

        public IList<T> GetInput()
        {
            string[] lines = File.ReadAllLines($@"Year2020\Inputs\input{this.Number}.txt");
            return lines.Select(l =>
            {
                T t = new T();
                t.Load(l);
                return t;
            }).ToList();
        }

        public abstract long Part1();

        public abstract long Part2();
    }
}
