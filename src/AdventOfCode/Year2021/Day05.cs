namespace NebulousIndustries.AdventOfCode.Year2021
{
    using System;
    using System.Collections.Generic;

    public class Day05 : DayBase<LineSegment>
    {
        public override long Part1()
        {
            return this.CountOverlaps(2, false);
        }

        public override long Part2()
        {
            return this.CountOverlaps(2, true);
        }

        protected int CountOverlaps(int threshold, bool allowDiagonals)
        {
            IEnumerable<LineSegment> segments = this.GetInput();

            int overlaps = 0;
            Dictionary<(int Y, int X), int> grid = [];
            foreach (LineSegment segment in segments)
            {
                if (!allowDiagonals && segment.X1 != segment.X2 && segment.Y1 != segment.Y2)
                {
                    continue;
                }

                int dist = Math.Max(Math.Abs(segment.Y2 - segment.Y1), Math.Abs(segment.X2 - segment.X1));
                int yIncr = segment.Y1 == segment.Y2 ? 0 : segment.Y1 < segment.Y2 ? 1 : -1;
                int xIncr = segment.X1 == segment.X2 ? 0 : segment.X1 < segment.X2 ? 1 : -1;
                for (int d = 0; d <= dist; d++)
                {
                    int yCurrent = segment.Y1 + (yIncr * d);
                    int xCurrent = segment.X1 + (xIncr * d);
                    grid.TryAdd((yCurrent, xCurrent), 0);
                    grid[(yCurrent, xCurrent)]++;
                    if (grid[(yCurrent, xCurrent)] == threshold)
                    {
                        overlaps++;
                    }
                }
            }

            return overlaps;
        }
    }

    public class LineSegment : IDayInput
    {
        private static readonly string[] Separators = [",", " -> "];

        public int X1 { get; set; }

        public int Y1 { get; set; }

        public int X2 { get; set; }

        public int Y2 { get; set; }

        public bool Load(string input)
        {
            string[] split = input.Split(LineSegment.Separators, StringSplitOptions.None);
            this.X1 = int.Parse(split[0]);
            this.Y1 = int.Parse(split[1]);
            this.X2 = int.Parse(split[2]);
            this.Y2 = int.Parse(split[3]);
            return false;
        }
    }
}
