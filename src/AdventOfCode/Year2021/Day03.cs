namespace NebulousIndustries.AdventOfCode.Year2021;

public class Day03 : DayBase<SubmarineCommand>
{
    public override long Part1()
    {
        List<string> diagnostics = this.GetInputRaw().ToList();
        IReadOnlyList<int> ones = CountBits(diagnostics);

        int gamma = 0;
        int epsilon = 0;
        for (int i = 0; i < ones.Count; i++)
        {
            gamma <<= 1;
            epsilon <<= 1;

            if (ones[i] > (diagnostics.Count / 2))
            {
                gamma += 1;
            }
            else
            {
                epsilon += 1;
            }
        }

        return gamma * epsilon;
    }

    public override long Part2()
    {
        List<string> diagnostics = this.GetInputRaw().ToList();

        string oxygenPrefix = string.Empty;
        string co2Prefix = string.Empty;
        string oxygenResult = string.Empty;
        string co2Result = string.Empty;
        for (int i = 0; true; i++)
        {
            IEnumerable<string> oxygenDiagnostics = diagnostics.Where(s => s.StartsWith(oxygenPrefix));
            if (oxygenDiagnostics.Count() == 1)
            {
                oxygenResult = oxygenDiagnostics.Single();
            }
            else
            {
                IReadOnlyList<int> oxygenOnes = CountBits(oxygenDiagnostics);
                oxygenPrefix += (oxygenOnes[i] >= (oxygenDiagnostics.Count() / 2.0)) ? "1" : "0";
            }

            IEnumerable<string> co2Diagnostics = diagnostics.Where(s => s.StartsWith(co2Prefix));
            if (co2Diagnostics.Count() == 1)
            {
                co2Result = co2Diagnostics.Single();
            }
            else
            {
                IReadOnlyList<int> co2Ones = CountBits(co2Diagnostics);
                co2Prefix += (co2Ones[i] >= (co2Diagnostics.Count() / 2.0)) ? "0" : "1";
            }

            if (!string.IsNullOrEmpty(oxygenResult) && !string.IsNullOrEmpty(co2Result))
            {
                break;
            }
        }

        return Convert.ToInt32(oxygenResult, 2) * Convert.ToInt32(co2Result, 2);
    }

    protected static IReadOnlyList<int> CountBits(IEnumerable<string> diagnostics)
    {
        List<int> ones = new(Enumerable.Repeat(0, diagnostics.First().Length));
        foreach (string diagnostic in diagnostics)
        {
            for (int i = 0; i < diagnostic.Length; i++)
            {
                if (diagnostic[i] == '1')
                {
                    ones[i]++;
                }
            }
        }

        return ones;
    }
}
