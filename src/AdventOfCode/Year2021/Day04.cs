namespace NebulousIndustries.AdventOfCode.Year2021;

public class Day04 : DayBase<BingoCard>
{
    public override long Part1()
    {
        IEnumerable<BingoCard> cards = this.GetInput();
        foreach (int call in BingoCard.Calls)
        {
            foreach (BingoCard card in cards)
            {
                bool matched = card.Mark(call);
                if (matched)
                {
                    return card.Score(call);
                }
            }
        }

        // Must not have found a board...
        return -1;
    }

    public override long Part2()
    {
        IEnumerable<BingoCard> cards = this.GetInput();
        HashSet<BingoCard> notWonYet = new(cards);
        foreach (int call in BingoCard.Calls)
        {
            foreach (BingoCard card in cards.Intersect(notWonYet))
            {
                bool matched = card.Mark(call);
                if (matched)
                {
                    if (notWonYet.Count == 1)
                    {
                        return card.Score(call);
                    }
                    else
                    {
                        notWonYet.Remove(card);
                    }
                }
            }
        }

        // Not all boards must have been winners...
        return -1;
    }
}

public class BingoCard : IDayInput
{
    public static IList<int> Calls { get; } = [];

    public IList<IList<int>> Board { get; } = [];

    public IList<IList<bool>> Marks { get; } = [];

    public bool Load(string input)
    {
        if (input.Contains(','))
        {
            BingoCard.Calls.Clear();
            foreach (string call in input.Split(","))
            {
                BingoCard.Calls.Add(int.Parse(call));
            }

            return true;
        }
        else if (string.IsNullOrEmpty(input))
        {
            if (this.Board.Count == 0)
            {
                // If we haven't parsed any rows yet, keep parsing (this will be the 2nd row)
                return true;
            }
            else
            {
                // Otherwise, finish the object and start a new one
                return false;
            }
        }
        else
        {
            List<int> row = input.Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList();
            this.Board.Add(row);
            this.Marks.Add(new List<bool>(Enumerable.Repeat(false, row.Count)));
            return true;
        }
    }

    public bool Mark(int call)
    {
        for (int r = 0; r < this.Board.Count; r++)
        {
            for (int c = 0; c < this.Board[r].Count; c++)
            {
                if (this.Board[r][c] == call)
                {
                    this.Marks[r][c] = true;

                    // Check for row/column match
                    return this.Marks[r].All(m => m) || this.Marks.All(m => m[c]);
                }
            }
        }

        return false;
    }

    public int Score(int lastCall)
    {
        int sum = 0;
        for (int r = 0; r < this.Board.Count; r++)
        {
            for (int c = 0; c < this.Board[r].Count; c++)
            {
                if (!this.Marks[r][c])
                {
                    sum += this.Board[r][c];
                }
            }
        }

        return sum * lastCall;
    }
}
