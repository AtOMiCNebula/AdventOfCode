// <copyright file="Day12.cs" company="Nebulous Industries">
// Copyright (c) Nebulous Industries. All rights reserved.
// </copyright>

namespace NebulousIndustries.AdventOfCode.Year2021
{
    using System.Collections.Generic;
    using System.Linq;

    public class Day12 : DayBase<CavePath>
    {
        public static ISet<string> FoundPaths { get; } = new HashSet<string>();

        public override long Part1()
        {
            return this.CountPaths(false);
        }

        public override long Part2()
        {
            return this.CountPaths(true);
        }

        protected int CountPaths(bool allowOneDoubleVisit)
        {
            IEnumerable<CavePath> paths = this.GetInput();
            FoundPaths.Clear();
            Cave start = Cave.GetCave("start");
            CountPaths(paths, start, start, Cave.GetCave("end"), allowOneDoubleVisit, new HashSet<Cave>(), start.Name);
            return FoundPaths.Count;
        }

        protected static void CountPaths(IEnumerable<CavePath> paths, Cave current, Cave start, Cave end, bool allowOneDoubleVisit, HashSet<Cave> visited, string debug)
        {
            foreach (Cave next in current.Connections.Except(visited))
            {
                if (next == end)
                {
                    FoundPaths.Add($"{debug}-{next.Name}");
                }
                else if (next == start)
                {
                    continue;
                }
                else
                {
                    HashSet<Cave> nextVisited = new(visited);
                    if (!next.AllowsMultipleVisits)
                    {
                        if (allowOneDoubleVisit)
                        {
                            CountPaths(paths, next, start, end, false, nextVisited, $"{debug}-{next.Name}");
                        }

                        nextVisited.Add(next);
                    }

                    CountPaths(paths, next, start, end, allowOneDoubleVisit, nextVisited, $"{debug}-{next.Name}");
                }
            }
        }
    }

    public class CavePath : IDayInput
    {
        public Cave A { get; set; }

        public Cave B { get; set; }

        public void InitializeNewParse()
        {
            Cave.Caves.Clear();
        }

        public bool Load(string input)
        {
            string[] caveNames = input.Split("-");
            this.A = Cave.GetCave(caveNames[0]);
            this.B = Cave.GetCave(caveNames[1]);
            this.A.Connections.Add(this.B);
            this.B.Connections.Add(this.A);
            return false;
        }
    }

    public class Cave
    {
        public Cave(string name)
        {
            this.Name = name;
            this.AllowsMultipleVisits = name.All(c => char.IsUpper(c));
        }

        public static IDictionary<string, Cave> Caves { get; } = new Dictionary<string, Cave>();

        public string Name { get; }

        public bool AllowsMultipleVisits { get; }

        public IList<Cave> Connections { get; } = new List<Cave>();

        public static Cave GetCave(string caveName)
        {
            if (!Caves.ContainsKey(caveName))
            {
                Caves.Add(caveName, new Cave(caveName));
            }

            return Caves[caveName];
        }
    }
}
