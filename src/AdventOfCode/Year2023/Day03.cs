// <copyright file="Day03.cs" company="Nebulous Industries">
// Copyright (c) Nebulous Industries. All rights reserved.
// </copyright>

namespace NebulousIndustries.AdventOfCode.Year2023;

using System;
using System.Collections.Generic;
using System.Linq;

public class Day03 : DayBase<PartMap>
{
    public override long Part1()
    {
        return this.GetInput().Single().GetParts()
            .Sum(p => p.Number);
    }

    public override long Part2()
    {
        return this.GetInput().Single().GetParts()
            .Where(p => p.Symbol == '*')
            .GroupBy(p => (p.SymbolY, p.SymbolX))
            .Where(g => g.Count() == 2)
            .Sum(p => p.First().Number * p.Last().Number);
    }
}

public class PartMap : IDayInput
{
    public List<string> Map { get; } = [];

    public bool Load(string input)
    {
        this.Map.Add($".{input}.");
        return true;
    }

    public IEnumerable<PartMatch> GetParts()
    {
        string wrappingLine = new('.', this.Map[0].Length);
        this.Map.Insert(0, wrappingLine);
        this.Map.Add(wrappingLine);

        for (int y = 1; y < this.Map.Count; y++)
        {
            int numberStartPos = -1;
            for (int x = 1; x < this.Map[y].Length; x++)
            {
                if (numberStartPos == -1 && char.IsDigit(this.Map[y][x]))
                {
                    numberStartPos = x;
                }
                else if (numberStartPos != -1 && !char.IsDigit(this.Map[y][x]))
                {
                    bool keepChecking = true;
                    int numberLength = x - numberStartPos;
                    for (int cy = -1; keepChecking && cy <= 1; cy++)
                    {
                        for (int cx = -1; keepChecking && cx < numberLength + 1; cx++)
                        {
                            int ty = y + cy;
                            int tx = numberStartPos + cx;
                            char c = this.Map[ty][tx];
                            if (c != '.' && !char.IsDigit(c))
                            {
                                yield return new PartMatch
                                {
                                    Number = int.Parse(this.Map[y].Substring(numberStartPos, numberLength)),
                                    Symbol = c,
                                    SymbolY = ty,
                                    SymbolX = tx,
                                };
                                keepChecking = false;
                            }
                        }
                    }

                    numberStartPos = -1;
                }
            }
        }
        yield break;
    }
}

public record class PartMatch
{
    public int Number { get; init; }

    public char Symbol { get; init; }

    public int SymbolY { get; init; }

    public int SymbolX { get; init; }
}
