// <copyright file="Day03.cs" company="Nebulous Industries">
// Copyright (c) Nebulous Industries. All rights reserved.
// </copyright>

namespace NebulousIndustries.AdventOfCode.Year2020
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Day03 : DayBase<MapLine>
    {
        public override int Number => 3;

        public override long Part1()
        {
            IEnumerable<MapLine> mapLines = this.GetInput();

            int trees = TraverseMap(mapLines, 3, 1);
            Console.WriteLine($"Found {trees} trees");
            return trees;
        }

        public override long Part2()
        {
            IEnumerable<MapLine> mapLines = this.GetInput();

            long trees1 = TraverseMap(mapLines, 1, 1);
            long trees2 = TraverseMap(mapLines, 3, 1);
            long trees3 = TraverseMap(mapLines, 5, 1);
            long trees4 = TraverseMap(mapLines, 7, 1);
            long trees5 = TraverseMap(mapLines, 1, 2);

            Console.WriteLine($"Found {trees1},{trees2},{trees3},{trees4},{trees5}: {trees1 * trees2 * trees3 * trees4 * trees5}");
            return trees1 * trees2 * trees3 * trees4 * trees5;
        }

        protected static int TraverseMap(IEnumerable<MapLine> mapLines, int xIncr, int yIncr)
        {
            bool first = true;
            int x = 0;
            int trees = 0;
            List<MapLine> mapLinesFiltered = mapLines.Where((l, i) => i % yIncr == 0).ToList();
            foreach (MapLine mapLine in mapLinesFiltered)
            {
                if (first)
                {
                    first = false;
                    continue;
                }

                x += xIncr;
                if (mapLine.ContainsTree(x))
                {
                    trees++;
                }
            }

            return trees;
        }
    }

    public class MapLine : IDayInput
    {
        public string LocalGeology { get; set; }

        public bool ContainsTree(int x)
        {
            char loc = this.LocalGeology[x % this.LocalGeology.Length];
            return loc == '#';
        }

        public bool Load(string input)
        {
            this.LocalGeology = input;

            return false;
        }
    }
}
