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

        public string InputFileVariant { get; set; } = string.Empty;

        public IEnumerable<string> GetInputRaw()
        {
            int year = this.Number > 100 ? this.Number / 100 : 2020;
            int day = this.Number % 100;
            return File.ReadAllLines($@"Year{year}\Inputs\input{day:D2}{this.InputFileVariant}.txt");
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
            T input = new();
            input.InitializeNewParse();
            List<T> inputs = new();
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
