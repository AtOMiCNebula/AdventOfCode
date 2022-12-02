// <copyright file="Day02.cs" company="Nebulous Industries">
// Copyright (c) Nebulous Industries. All rights reserved.
// </copyright>

namespace NebulousIndustries.AdventOfCode.Year2022
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Day02 : DayBase<Play>
    {
        public override long Part1()
        {
            IEnumerable<Play> plays = this.GetInput();
            return plays.Sum(p => p.ScoreForPart1);
        }

        public override long Part2()
        {
            IEnumerable<Play> plays = this.GetInput();
            return plays.Sum(p => p.ScoreForPart2);
        }
    }

    public class Play : IDayInput
    {
        public enum PlayChoice
        {
            Rock,
            Paper,
            Scissors,
        }

        public enum Result
        {
            Loss,
            Draw,
            Win,
        }

        public PlayChoice Player { get; set; }

        public PlayChoice Opponent { get; set; }

        public Result ExpectedResult
        {
            get
            {
                return (Result)this.Player;
            }
        }

        public int ScoreForPart1
        {
            get
            {
                return Play.Score(this.Player, this.Opponent);
            }
        }

        public int ScoreForPart2
        {
            get
            {
                PlayChoice playerNeeds = this.ExpectedResult switch
                {
                    Result.Loss => (PlayChoice)(((int)this.Opponent + 2) % 3),
                    Result.Draw => this.Opponent,
                    Result.Win => (PlayChoice)(((int)this.Opponent + 1) % 3),
                    _ => throw new InvalidOperationException(),
                };
                return Play.Score(playerNeeds, this.Opponent);
            }
        }

        public bool Load(string input)
        {
            this.Opponent = (PlayChoice)(input[0] - 'A');
            this.Player = (PlayChoice)(input[2] - 'X');
            return false;
        }

        private static int Score(PlayChoice player, PlayChoice opponent)
        {
            int pickPoints = (int)player + 1;
            int winPoints = ((int)opponent + 1) % 3 == (int)player ? 6 :
                            opponent == player ? 3 :
                            0;
            return pickPoints + winPoints;
        }
    }
}
