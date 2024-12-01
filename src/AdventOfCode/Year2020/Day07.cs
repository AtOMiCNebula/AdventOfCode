namespace NebulousIndustries.AdventOfCode.Year2020
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Day07 : DayBase<BaggageRule>
    {
        public override long Part1()
        {
            Dictionary<string, BaggageRule> baggageRules = this.GetInput().ToDictionary(r => r.BagColor);
            ResolveEdges(baggageRules);

            return baggageRules.Values.Where(r => DeepContains(baggageRules, r, "shiny gold")).Count();
        }

        public override long Part2()
        {
            Dictionary<string, BaggageRule> baggageRules = this.GetInput().ToDictionary(r => r.BagColor);
            ResolveEdges(baggageRules);

            return DeepCount(baggageRules, baggageRules["shiny gold"]);
        }

        public static void ResolveEdges(Dictionary<string, BaggageRule> baggageRules)
        {
            foreach (BaggageRule baggageRule in baggageRules.Values)
            {
                foreach (BaggageContent baggageContent in baggageRule.Contents)
                {
                    baggageRule.ContainsRules.Add(baggageRules[baggageContent.BagColor]);
                    baggageRules[baggageContent.BagColor].ContainedByRules.Add(baggageRule);
                }
            }
        }

        public static bool DeepContains(Dictionary<string, BaggageRule> baggageRules, BaggageRule baggageRule, string bagColor)
        {
            foreach (BaggageRule containedBaggageRule in baggageRule.ContainsRules)
            {
                if (containedBaggageRule.BagColor == bagColor)
                {
                    return true;
                }
                else if (DeepContains(baggageRules, containedBaggageRule, bagColor))
                {
                    return true;
                }
            }

            return false;
        }

        public static int DeepCount(Dictionary<string, BaggageRule> baggageRules, BaggageRule baggageRule)
        {
            int bags = 0;
            foreach (BaggageContent baggageContent in baggageRule.Contents)
            {
                bags += baggageContent.Count;

                BaggageRule baggageContentRule = baggageRules[baggageContent.BagColor];
                int bagContents = DeepCount(baggageRules, baggageContentRule);
                bags += baggageContent.Count * bagContents;
            }

            Console.WriteLine($"{baggageRule.BagColor} contains {bags} bag(s)");
            return bags;
        }
    }

    public class BaggageRule : IDayInput
    {
        public string BagColor { get; set; }

        public IList<BaggageContent> Contents { get; } = [];

        public IList<BaggageRule> ContainsRules { get; } = [];

        public IList<BaggageRule> ContainedByRules { get; } = [];

        public bool Load(string input)
        {
            this.BagColor = input[0..input.IndexOf(" bags")];

            int i = input.IndexOf(" contain ") + 9;
            string[] containedBags = input[i..].Split(',', '.');
            foreach (string containedBag in containedBags)
            {
                if (string.IsNullOrEmpty(containedBag) || containedBag == "no other bags")
                {
                    break;
                }

                string[] containedBagSplit = containedBag.Trim().Split(' ', 2);
                if (containedBagSplit[0] == "1")
                {
                    this.Contents.Add(new BaggageContent
                    {
                        Count = int.Parse(containedBagSplit[0]),
                        BagColor = containedBagSplit[1][0..^4],
                    });
                }
                else
                {
                    this.Contents.Add(new BaggageContent
                    {
                        Count = int.Parse(containedBagSplit[0]),
                        BagColor = containedBagSplit[1][0..^5],
                    });
                }
            }

            return false;
        }
    }

    public class BaggageContent
    {
        public int Count { get; set; }

        public string BagColor { get; set; }
    }
}
