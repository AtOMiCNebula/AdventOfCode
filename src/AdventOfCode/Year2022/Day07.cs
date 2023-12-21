// <copyright file="Day07.cs" company="Nebulous Industries">
// Copyright (c) Nebulous Industries. All rights reserved.
// </copyright>

namespace NebulousIndustries.AdventOfCode.Year2022
{
    using System.Collections.Generic;
    using System.Linq;

    public class Day07 : DayBase<CommandOutput>
    {
        public override long Part1()
        {
            IEnumerable<CommandOutput> commands = this.GetInput();
            Dictionary<string, long> folderSizes = Day07.ComputeFolderSizes(commands);
            return folderSizes.Values.Where(v => v <= 100000).Sum();
        }

        public override long Part2()
        {
            const long TotalCapacity = 70000000;
            const long TargetUnused = 30000000;
            const long TargetSize = TotalCapacity - TargetUnused;

            IEnumerable<CommandOutput> commands = this.GetInput();
            Dictionary<string, long> folderSizes = Day07.ComputeFolderSizes(commands);
            long currentSize = folderSizes["/"];
            return folderSizes.Values.OrderBy(v => v).Where(v => currentSize - v < TargetSize).First();
        }

        public static Dictionary<string, long> ComputeFolderSizes(IEnumerable<CommandOutput> commands)
        {
            Dictionary<string, long> folderSizes = new();
            Stack<string> tree = new();
            foreach (CommandOutput command in commands)
            {
                if (command.Command == "cd ..")
                {
                    tree.Pop();
                }
                else if (command.Command.StartsWith("cd "))
                {
                    tree.Push(command.Command[3..]);
                }
                else if (command.Command == "ls")
                {
                    long size = 0;
                    foreach (string entry in command.Output)
                    {
                        string[] split = entry.Split(' ');
                        if (split[0] != "dir")
                        {
                            size += long.Parse(split[0]);
                        }
                    }

                    string path = string.Join('/', tree.Reverse().Skip(1));
                    folderSizes[$"/{path}"] = size;

                    // Add our size to our parent directories, too
                    for (int i = 1; i < tree.Count; i++)
                    {
                        string parentPath = string.Join('/', tree.Skip(i).Reverse().Skip(1));
                        folderSizes[$"/{parentPath}"] += size;
                    }
                }
            }

            return folderSizes;
        }
    }

    public class CommandOutput : IDayInput
    {
        public static string LastCommandStr { get; set; } = string.Empty;

        public string Command { get; set; }

        public IList<string> Output { get; } = new List<string>();

        public void InitializeNewParse()
        {
            CommandOutput.LastCommandStr = string.Empty;
        }

        public bool Load(string input)
        {
            if (!string.IsNullOrEmpty(CommandOutput.LastCommandStr))
            {
                this.Command = CommandOutput.LastCommandStr;
                CommandOutput.LastCommandStr = string.Empty;
            }

            if (input.StartsWith("$ "))
            {
                if (string.IsNullOrEmpty(this.Command))
                {
                    // First command of the parse
                    this.Command = input[2..];
                }
                else
                {
                    CommandOutput.LastCommandStr = input[2..];
                    return false;
                }
            }
            else
            {
                this.Output.Add(input);
            }

            return true;
        }
    }
}
