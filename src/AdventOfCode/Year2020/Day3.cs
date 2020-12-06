// <copyright file="Day3.cs" company="Nebulous Industries">
// Copyright (c) Nebulous Industries. All rights reserved.
// </copyright>

namespace NebulousIndustries.AdventOfCode.Year2020
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Day3 : DayBase<MapLine>
    {
        public override int Number => 3;

        public override void Part1()
        {
            IList<MapLine> mapLines = this.GetInput();

            int trees = TraverseMap(mapLines, 3, 1);
            Console.WriteLine($"Found {trees} trees");
        }

        public override void Part2()
        {
            IList<MapLine> mapLines = this.GetInput();

            long trees1 = TraverseMap(mapLines, 1, 1);
            long trees2 = TraverseMap(mapLines, 3, 1);
            long trees3 = TraverseMap(mapLines, 5, 1);
            long trees4 = TraverseMap(mapLines, 7, 1);
            long trees5 = TraverseMap(mapLines, 1, 2);

            Console.WriteLine($"Found {trees1},{trees2},{trees3},{trees4},{trees5}: {trees1 * trees2 * trees3 * trees4 * trees5}");
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

        public void Load(string input)
        {
            this.LocalGeology = input;
        }
    }
}
