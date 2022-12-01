using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2021.Datastructures
{
    public class Vertex : IComparable<Vertex>
    {
        public string name;
        public LinkedList<Edge> adj;
        public double distance;
        public Vertex prev;
        public bool known;
        
        /// <summary>
        ///    Creates a new Vertex instance.
        /// </summary>
        /// <param name="name">The name of the new vertex</param>
        public Vertex(string name)
        {
            this.name = name;
            distance = Graph.INFINITY;
            adj = new LinkedList<Edge>();
        }
        
        public string GetName()
        {
            return name;
        }

        public LinkedList<Edge> GetAdjacents()
        {
            return adj;
        }

        public double GetDistance()
        {
            return distance;
        }

        public Vertex GetPrevious()
        {
            return prev;
        }

        public bool GetKnown()
        {
            return known;
        }

        public void Reset()
        {
            distance = Graph.INFINITY;
            known = false;
            prev = null;
        }
        
        /// <summary>
        ///    Converts this instance of Vertex to its string representation.
        ///    <para>Output will be like : name (distance) [ adj1 (cost) adj2 (cost) .. ]</para>
        ///    <para>Adjecents are ordered ascending by name. If no distance is
        ///    calculated yet, the distance and the parantheses are omitted.</para>
        /// </summary>
        /// <returns>The string representation of this Graph instance</returns> 
        public override string ToString()
        {
            string result = $"{name}";
            if (distance != Graph.INFINITY)
                result += $" ({distance})";
            result += " [";
            foreach (Edge e in adj.OrderBy(x => x.dest.name))
            {
                result += $" {e.dest.name}({e.cost})";
            }

            return result + ']';
        }

        public int CompareTo(Vertex other)
        {
            if (ReferenceEquals(this, other)) return 0;
            if (ReferenceEquals(null, other)) return 1;
            return distance.CompareTo(other.distance);
        }
    }
}