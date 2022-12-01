using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2021.Puzzles
{
    public class Day12 : AocPuzzle
    {
        public override int Year => 2021;
        public override int Day => 12;

        private Dictionary<string, Node> _nodes = new Dictionary<string, Node>();
        private List<string> _paths = new List<string>();
        protected override void Solve(IEnumerable<string> input)
        {
            List<string[]> nodes = input.Select(x => x.Split('-')).ToList();
            foreach (string[] node in nodes)
            {
                if(!_nodes.ContainsKey(node[0]))
                    _nodes.Add(node[0], new Node(node[0]));
                if(!_nodes.ContainsKey(node[1]))
                    _nodes.Add(node[1], new Node(node[1]));
                _nodes[node[0]].AddLink(_nodes[node[1]]);
            }
            Paths();
            PartOne = _paths.Count.ToString();
            _paths = new List<string>();
            Paths2();
            PartTwo = _paths.Count.ToString();
        }

        private void Paths()
        {
            Traverse(_nodes["start"], _nodes["start"].Name);
        }
        
        private void Paths2()
        {
            Traverse2(_nodes["start"], _nodes["start"].Name);
        }

        private void Traverse(Node node, string path)
        {
            if (node.Name == "end")
            {
                _paths.Add(path);
                return;
            }
            foreach (Link nodeLink in node.Links)
            {
                if(nodeLink.Child.Name != "start" && (nodeLink.Child.Name.All(char.IsUpper) || !path.Contains(nodeLink.Child.Name)))
                    Traverse(nodeLink.Child, $"{path}-{nodeLink.Child.Name}");
            }
        }

        private void Traverse2(Node node, string path)
        {
            if (node.Name == "end")
            {
                _paths.Add(path);
                return;
            }   
            foreach (Link nodeLink in node.Links)
            {
                if(nodeLink.Child.Name != "start" && (nodeLink.Child.Name.All(char.IsUpper) || NotMoreThanOneTwiceVisitedSmallCaves(path)))
                    Traverse2(nodeLink.Child, $"{path}-{nodeLink.Child.Name}");
            }
        }
        
        private bool NotMoreThanOneTwiceVisitedSmallCaves(string path)
        {
            string[] smallCavesList = path.Split('-');
            int twiceVisited = 0;
            var counts = new Dictionary<string, int>();
            foreach (string cave in smallCavesList)
            {
                if (cave.All(char.IsLower))
                {
                    if (counts.ContainsKey(cave))
                    {
                        counts[cave] += 1;
                        if (counts[cave] > 2) return false;
                        if (counts[cave] == 2) twiceVisited++;
                        if (twiceVisited == 2) return false;
                    }
                    else
                        counts.Add(cave, 1);
                }
            }

            return true;
        }
    }

    internal class Node
    {
        public string Name { get; set; }
        public List<Link> Links { get; set; }

        public Node(string name)
        {
            Name = name;
            Links = new List<Link>();
        }
        
        /// <summary>
        /// Create a new arc, connecting this Node to the Nod passed in the parameter
        /// Also, it creates the inversed node in the passed node
        /// </summary>
        public void AddLink(Node child)
        {
            Links.Add(new Link(this, child));

            if (!child.Links.Exists(a => a.Parent == child && a.Child == this))
            {
                child.AddLink(this);
            }
        }
    }

    internal class Link
    {
        public Node Parent { get; set; }
        public Node Child { get; set; }

        public Link(Node parent, Node child)
        {
            Parent = parent;
            Child = child;
        }
    }
}