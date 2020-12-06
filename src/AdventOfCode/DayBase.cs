// <copyright file="DayBase.cs" company="Nebulous Industries">
// Copyright (c) Nebulous Industries. All rights reserved.
// </copyright>

namespace NebulousIndustries.AdventOfCode
{
    using System.Collections.Generic;
    using System.IO;

    public abstract class DayBase : IDay
    {
        public abstract int Number { get; }

        public IEnumerable<string> GetInputRaw()
        {
            return File.ReadAllLines($@"Year2020\Inputs\input{this.Number}.txt");
        }

        public abstract long Part1();

        public abstract long Part2();
    }

    public abstract class DayBase<T> : DayBase
        where T : IDayInput, new()
    {
        public IEnumerable<T> GetInput()
        {
            bool inserted = false;
            T input = new T();
            List<T> inputs = new List<T>();
            foreach (string line in this.GetInputRaw())
            {
                if (!inserted)
                {
                    inputs.Add(input);
                    inserted = true;
                }

                bool keepAggregating = input.Load(line);
                if (!keepAggregating)
                {
                    input = new T();
                    inserted = false;
                }
            }

            return inputs;
        }
    }
}
