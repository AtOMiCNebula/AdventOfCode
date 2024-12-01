namespace NebulousIndustries.AdventOfCode.Year2021
{
    using System.Collections.Generic;
    using System.Linq;

    public class Day15 : DayBase
    {
        public override long Part1()
        {
            Node[][] nodes = this.GetInputRaw().Select(r => r.Select(c => new Node(c - '0')).ToArray()).ToArray();
            return Dijkstra(nodes);
        }

        public override long Part2()
        {
            Node[][] nodes = this.GetInputRaw().Select(r => r.Select(c => new Node(c - '0')).ToArray()).ToArray();

            IEnumerable<IEnumerable<Node>> nodesExpanded = Enumerable.Empty<IEnumerable<Node>>();
            for (int yRepeat = 0; yRepeat < 5; yRepeat++)
            {
                for (int yIter = 0; yIter < nodes.Length; yIter++)
                {
                    int y = (yRepeat * nodes.Length) + yIter;

                    IEnumerable<Node> nodesExpandedRow = Enumerable.Empty<Node>();
                    for (int xRepeat = 0; xRepeat < 5; xRepeat++)
                    {
                        int offset = yRepeat + xRepeat;

                        nodesExpandedRow = nodesExpandedRow.Concat(nodes[yIter].Select(n => new Node(n, offset)));
                    }

                    nodesExpanded = nodesExpanded.Append(nodesExpandedRow);
                }
            }

            return Dijkstra(nodesExpanded.Select(r => r.ToArray()).ToArray());
        }

        protected static int Dijkstra(Node[][] nodes)
        {
            nodes[0][0].Distance = 0;
            PriorityQueue<(int Y, int X), int> queue = new();
            queue.Enqueue((0, 0), 0);

            while (queue.Count > 0 && !nodes[^1][^1].Visited)
            {
                (int y, int x) = queue.Dequeue();
                Node node = nodes[y][x];
                if (node.Visited)
                {
                    continue;
                }

                if (y > 0)
                {
                    Node up = nodes[y - 1][x];
                    if (!up.Visited && node.Distance + up.Value < up.Distance)
                    {
                        up.Distance = node.Distance + up.Value;
                        queue.Enqueue((y - 1, x), up.Distance);
                    }
                }

                if (y < nodes.Length - 1)
                {
                    Node down = nodes[y + 1][x];
                    if (!down.Visited && node.Distance + down.Value < down.Distance)
                    {
                        down.Distance = node.Distance + down.Value;
                        queue.Enqueue((y + 1, x), down.Distance);
                    }
                }

                if (x > 0)
                {
                    Node left = nodes[y][x - 1];
                    if (!left.Visited && node.Distance + left.Value < left.Distance)
                    {
                        left.Distance = node.Distance + left.Value;
                        queue.Enqueue((y, x - 1), left.Distance);
                    }
                }

                if (x < nodes[y].Length - 1)
                {
                    Node right = nodes[y][x + 1];
                    if (!right.Visited && node.Distance + right.Value < right.Distance)
                    {
                        right.Distance = node.Distance + right.Value;
                        queue.Enqueue((y, x + 1), right.Distance);
                    }
                }

                node.Visited = true;
            }

            return nodes[^1][^1].Distance;
        }
    }

    public class Node
    {
        public Node(int value)
        {
            this.Value = value;
            this.Distance = int.MaxValue;
            this.Visited = false;
        }

        public Node(Node other, int offset)
        {
            int value = other.Value + offset;
            if (value > 9)
            {
                value -= 9;
            }

            this.Value = value;
            this.Distance = int.MaxValue;
            this.Visited = false;
        }

        public int Value { get; }

        public int Distance { get; set; }

        public bool Visited { get; set; }
    }
}
