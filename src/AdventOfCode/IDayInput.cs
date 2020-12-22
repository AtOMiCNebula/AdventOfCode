// <copyright file="IDayInput.cs" company="Nebulous Industries">
// Copyright (c) Nebulous Industries. All rights reserved.
// </copyright>

namespace NebulousIndustries.AdventOfCode
{
    public interface IDayInput
    {
        public void InitializeNewParse()
        {
        }

        public bool Load(string input);
    }
}
