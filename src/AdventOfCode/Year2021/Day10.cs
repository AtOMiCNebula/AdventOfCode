namespace NebulousIndustries.AdventOfCode.Year2021
{
    using System.Collections.Generic;
    using System.Linq;

    public class Day10 : DayBase
    {
        public static IReadOnlyDictionary<char, char> Openers { get; } = new Dictionary<char, char>()
        {
            { '(', ')' },
            { '[', ']' },
            { '{', '}' },
            { '<', '>' },
        };

        public static IReadOnlyDictionary<char, (int CorruptionScore, int AutocompleteScore)> Closers { get; } = new Dictionary<char, (int CorruptionScore, int AutocompleteScore)>()
        {
            { ')', (3, 1) },
            { ']', (57, 2) },
            { '}', (1197, 3) },
            { '>', (25137, 4) },
        };

        public override long Part1()
        {
            return this.CalculateScore(true);
        }

        public override long Part2()
        {
            return this.CalculateScore(false);
        }

        public long CalculateScore(bool scoreCorruptedLines)
        {
            IEnumerable<string> lines = this.GetInputRaw();
            List<long> scores = [];
            foreach (string line in lines)
            {
                long score = 0;
                Stack<char> groups = new();
                bool corrupted = false;
                for (int i = 0; i < line.Length; i++)
                {
                    if (Openers.ContainsKey(line[i]))
                    {
                        groups.Push(Openers[line[i]]);
                    }
                    else if (line[i] == groups.Peek())
                    {
                        groups.Pop();
                    }
                    else if (Closers.ContainsKey(line[i]))
                    {
                        if (scoreCorruptedLines)
                        {
                            score += Closers[line[i]].CorruptionScore;
                        }

                        corrupted = true;
                        break;
                    }
                }

                if (!corrupted && !scoreCorruptedLines)
                {
                    foreach (char closer in groups)
                    {
                        score = (score * 5) + Closers[closer].AutocompleteScore;
                    }
                }

                if (score > 0)
                {
                    scores.Add(score);
                }
            }

            if (scoreCorruptedLines)
            {
                return scores.Sum();
            }
            else
            {
                scores.Sort();
                return scores[scores.Count / 2];
            }
        }
    }
}
