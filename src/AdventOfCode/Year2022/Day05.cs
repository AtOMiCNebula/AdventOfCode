namespace NebulousIndustries.AdventOfCode.Year2022
{
    using System.Collections.Generic;
    using System.Linq;

    public class Day05 : DayBase<CrateMove, string>
    {
        public override string Part1()
        {
            IEnumerable<CrateMove> moves = this.GetInput();
            foreach (CrateMove move in moves)
            {
                for (int i = 0; i < move.Quantity; i++)
                {
                    char top = CrateMove.SupplyStacks[move.Source].Pop();
                    CrateMove.SupplyStacks[move.Destination].Push(top);
                }
            }

            return CrateMove.SupplyStacks.Aggregate(string.Empty, (str, stack) => string.Concat(str, stack.Peek()));
        }

        public override string Part2()
        {
            IEnumerable<CrateMove> moves = this.GetInput();
            foreach (CrateMove move in moves)
            {
                Stack<char> temp = new();
                for (int i = 0; i < move.Quantity; i++)
                {
                    temp.Push(CrateMove.SupplyStacks[move.Source].Pop());
                }
                while (temp.Count > 0)
                {
                    CrateMove.SupplyStacks[move.Destination].Push(temp.Pop());
                }
            }

            return CrateMove.SupplyStacks.Aggregate(string.Empty, (str, stack) => string.Concat(str, stack.Peek()));
        }
    }

    public class CrateMove : IDayInput
    {
        public static bool LoadedBoxes { get; set; }

        public static IList<Stack<char>> SupplyStacks { get; } = new List<Stack<char>>();

        public int Quantity { get; set; }

        public int Source { get; set; }

        public int Destination { get; set; }

        public void InitializeNewParse()
        {
            CrateMove.LoadedBoxes = false;
            CrateMove.SupplyStacks.Clear();
        }

        public bool Load(string input)
        {
            if (!CrateMove.LoadedBoxes)
            {
                if (!string.IsNullOrWhiteSpace(input))
                {
                    if (CrateMove.SupplyStacks.Count == 0)
                    {
                        for (int i = 0; i <= input.Length / 4; i++)
                        {
                            CrateMove.SupplyStacks.Add(new Stack<char>());
                        }
                    }

                    if (input[1] != '1')
                    {
                        for (int i = 1; i < input.Length; i += 4)
                        {
                            if (input[i] != ' ')
                            {
                                CrateMove.SupplyStacks[i / 4].Push(input[i]);
                            }
                        }
                    }
                }
                else
                {
                    // Reverse all the stacks, since we parsed top-to-bottom
                    foreach (Stack<char> stack in CrateMove.SupplyStacks)
                    {
                        List<char> temp = new(stack);
                        stack.Clear();
                        temp.ForEach(c => stack.Push(c));
                    }

                    CrateMove.LoadedBoxes = true;
                }

                return true;
            }

            string[] splits = input.Split(' ');
            this.Quantity = int.Parse(splits[1]);
            this.Source = int.Parse(splits[3]) - 1;
            this.Destination = int.Parse(splits[5]) - 1;
            return false;
        }
    }
}
