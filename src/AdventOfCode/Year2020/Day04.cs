﻿namespace NebulousIndustries.AdventOfCode.Year2020;

public class Day04 : DayBase<Passport>
{
    public override long Part1()
    {
        IEnumerable<Passport> passports = this.GetInput();
        Console.WriteLine($"Counted {passports.Where(p => p.IsValidForPart1()).Count()} valid passports (part 1)");
        return passports.Where(p => p.IsValidForPart1()).Count();
    }

    public override long Part2()
    {
        IEnumerable<Passport> passports = this.GetInput();
        Console.WriteLine($"Counted {passports.Where(p => p.IsValidForPart2()).Count()} valid passports (part 2)");
        return passports.Where(p => p.IsValidForPart2()).Count();
    }
}

public class Passport : IDayInput
{
    public string BirthYear { get; set; }

    public string IssueYear { get; set; }

    public string ExpirationYear { get; set; }

    public string Height { get; set; }

    public string HairColor { get; set; }

    public string EyeColor { get; set; }

    public string PassportID { get; set; }

    public string CountryID { get; set; }

    public bool IsValidForPart1()
    {
        return
            this.BirthYear != null &&
            this.IssueYear != null &&
            this.ExpirationYear != null &&
            this.Height != null &&
            this.HairColor != null &&
            this.EyeColor != null &&
            this.PassportID != null;
    }

    public bool IsValidForPart2()
    {
        if (!this.IsValidForPart1())
        {
            return false;
        }

        int birthYear = int.Parse(this.BirthYear);
        if (birthYear is not (>= 1920 and <= 2002))
        {
            return false;
        }

        int issueYear = int.Parse(this.IssueYear);
        if (issueYear is not (>= 2010 and <= 2020))
        {
            return false;
        }

        int expirationYear = int.Parse(this.ExpirationYear);
        if (expirationYear is not (>= 2020 and <= 2030))
        {
            return false;
        }

        if (!this.Height.EndsWith("cm") && !this.Height.EndsWith("in"))
        {
            return false;
        }
        int height = int.Parse(this.Height[0..(this.Height.Length - 2)]);
        if ((this.Height.EndsWith("cm") && !(150 <= height && height <= 193))
            || (this.Height.EndsWith("in") && !(59 <= height && height <= 76)))
        {
            return false;
        }

        if (this.HairColor.Length != 7 || this.HairColor[0] != '#')
        {
            return false;
        }
        for (int i = 1; i < 7; i++)
        {
            if (this.HairColor[i] is not (>= '0' and <= '9') and not (>= 'a' and <= 'f'))
            {
                return false;
            }
        }

        switch (this.EyeColor)
        {
            case "amb":
            case "blu":
            case "brn":
            case "gry":
            case "grn":
            case "hzl":
            case "oth":
                break;

            default:
                return false;
        }

        if (this.PassportID.Length != 9 || !int.TryParse(this.PassportID, out _))
        {
            return false;
        }

        return true;
    }

    public bool Load(string input)
    {
        if (string.IsNullOrEmpty(input))
        {
            return false;
        }

        string[] splits = input.Split(' ');
        foreach (string split in splits)
        {
            string[] kv = split.Split(':');
            switch (kv[0])
            {
                case "byr":
                    this.BirthYear = kv[1];
                    break;
                case "iyr":
                    this.IssueYear = kv[1];
                    break;
                case "eyr":
                    this.ExpirationYear = kv[1];
                    break;
                case "hgt":
                    this.Height = kv[1];
                    break;
                case "hcl":
                    this.HairColor = kv[1];
                    break;
                case "ecl":
                    this.EyeColor = kv[1];
                    break;
                case "pid":
                    this.PassportID = kv[1];
                    break;
                case "cid":
                    this.CountryID = kv[1];
                    break;
                default:
                    throw new NotSupportedException($"Unexpected passport key '{kv[0]}' with value '{kv[1]}'");
            }
        }

        return true;
    }
}
