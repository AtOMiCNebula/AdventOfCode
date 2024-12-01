namespace NebulousIndustries.AdventOfCode.Year2021
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Day13 : DayBase<FoldInstruction>
    {
        public override long Part1()
        {
            IEnumerable<FoldInstruction> folds = this.GetInput();
            return folds.First().Fold(FoldInstruction.Points).Count;
        }

        public override long Part2()
        {
            IEnumerable<FoldInstruction> folds = this.GetInput();

            ISet<(int Y, int X)> points = FoldInstruction.Points;
            foreach (FoldInstruction fold in folds)
            {
                points = fold.Fold(points);
            }

            // Lousy grid output, resulting point grid should resemble letters for the submitted solution
            (int maxY, int maxX) = points.Aggregate((acc, p) => (Math.Max(acc.Y, p.Y), Math.Max(acc.X, p.X)));
            for (int y = 0; y <= maxY; y++)
            {
                for (int x = 0; x <= maxX; x++)
                {
                    Console.Write(points.Contains((y, x)) ? '#' : ' ');
                }
                Console.WriteLine();
            }

            return points.Count;
        }
    }

    public class FoldInstruction : IDayInput
    {
        public static bool ParsingFolds { get; set; }

        public static ISet<(int Y, int X)> Points { get; } = new HashSet<(int Y, int X)>();

        public bool FoldAlongY { get; set; }

        public int FoldAt { get; set; }

        public void InitializeNewParse()
        {
            ParsingFolds = false;
            Points.Clear();
        }

        public bool Load(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                ParsingFolds = true;
                return true;
            }
            else if (!ParsingFolds)
            {
                string[] parts = input.Split(',');
                Points.Add((int.Parse(parts[1]), int.Parse(parts[0])));
                return true;
            }
            else
            {
                string[] parts = input[11..].Split('=');
                this.FoldAlongY = parts[0] == "y";
                this.FoldAt = int.Parse(parts[1]);
                return false;
            }
        }

        public ISet<(int Y, int X)> Fold(ISet<(int Y, int X)> points)
        {
            HashSet<(int Y, int X)> pointsNew = [];
            foreach ((int y, int x) in points)
            {
                if ((this.FoldAlongY && y < this.FoldAt) || (!this.FoldAlongY && x < this.FoldAt))
                {
                    pointsNew.Add((y, x));
                }
                else if (this.FoldAlongY && y > this.FoldAt)
                {
                    pointsNew.Add(((2 * this.FoldAt) - y, x));
                }
                else if (!this.FoldAlongY && x > this.FoldAt)
                {
                    pointsNew.Add((y, (2 * this.FoldAt) - x));
                }
            }

            return pointsNew;
        }
    }
}
