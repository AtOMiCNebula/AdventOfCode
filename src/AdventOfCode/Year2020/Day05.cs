﻿using System.Diagnostics;

namespace NebulousIndustries.AdventOfCode.Year2020;

public class Day05 : DayBase<BoardingPass>
{
    public override long Part1()
    {
        IEnumerable<BoardingPass> boardingPasses = this.GetInput();
        Console.WriteLine($"Highest seat ID: {boardingPasses.Max(p => p.SeatID)}");
        return boardingPasses.Max(p => p.SeatID);
    }

    public override long Part2()
    {
        IEnumerable<BoardingPass> boardingPasses = this.GetInput();
        HashSet<int> seatIDs = boardingPasses.Select(p => p.SeatID).ToHashSet();
        IEnumerable<int> allSeatIDs = Enumerable.Range(0, (int)Math.Pow(2, 10) - 1);
        IEnumerable<int> missingSeatIDs = allSeatIDs.Except(seatIDs);
        foreach (int missingSeatID in missingSeatIDs)
        {
            if (seatIDs.Contains(missingSeatID - 1) && seatIDs.Contains(missingSeatID + 1))
            {
                Console.WriteLine($"Found your seatID: {missingSeatID}");
                return missingSeatID;
            }
        }

        return -1;
    }
}

public class BoardingPass : IDayInput
{
    public int Row { get; set; }

    public int Column { get; set; }

    public int SeatID => (this.Row * 8) + this.Column;

    public bool Load(string input)
    {
        this.Row = BinarySpacePartition(input[..7], 'F', 'B');
        this.Column = BinarySpacePartition(input.Substring(7, 3), 'L', 'R');

        return false;
    }

    public static int BinarySpacePartition(string partition, char lowerHalfChar, char upperHalfChar)
    {
        int lowerBound = 0;
        int upperBound = (int)Math.Pow(2, partition.Length) - 1;

        foreach (char partitionChar in partition)
        {
            int rangeToShrink = (upperBound - lowerBound + 1) / 2;
            if (partitionChar == lowerHalfChar)
            {
                upperBound -= rangeToShrink;
            }
            else if (partitionChar == upperHalfChar)
            {
                lowerBound += rangeToShrink;
            }
            else
            {
                throw new NotSupportedException("Unexpected character");
            }
        }

        Debug.Assert(lowerBound == upperBound, "Expected lowerBound and upperBound to match");
        return lowerBound;
    }
}
