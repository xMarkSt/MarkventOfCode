using System.Collections.Generic;
using System.Linq;
using AdventOfCode2021.Puzzles;

namespace AdventOfCode2021.Datastructures
{
    public class Graph
    {
        public static readonly double INFINITY = System.Double.MaxValue;

        public Dictionary<string, Vertex> vertexMap;


        //----------------------------------------------------------------------
        // Constructor
        //----------------------------------------------------------------------

        public Graph()
        {
            vertexMap = new Dictionary<string, Vertex>();
        }


        //----------------------------------------------------------------------
        // Interface methods that have to be implemented for exam
        //----------------------------------------------------------------------

        /// <summary>
        ///    Adds a vertex to the graph. If a vertex with the given name
        ///    already exists, no action is performed.
        /// </summary>
        /// <param name="name">The name of the new vertex</param>
        public Vertex AddVertex(string name)
        {
            if (vertexMap.ContainsKey(name)) return vertexMap[name];
            var newVertex = new Vertex(name);
            vertexMap.Add(name, newVertex);
            return newVertex;
        }

        /// <summary>
        ///    Gets a vertex from the graph by name. If no such vertex exists,
        ///    a new vertex will be created and returned.
        /// </summary>
        /// <param name="name">The name of the vertex</param>
        /// <returns>The vertex withe the given name</returns>
        public Vertex GetVertex(string name)
        {
            if (vertexMap.ContainsKey(name))
            {
                return vertexMap[name];
            }

            Vertex newVertex = new Vertex(name);
            vertexMap.Add(name, newVertex);
            return newVertex;
        }


        /// <summary>
        ///    Creates an edge between two vertices. Vertices that are non existing
        ///    will be created before adding the edge.
        ///    There is no check on multiple edges!
        /// </summary>
        /// <param name="source">The name of the source vertex</param>
        /// <param name="dest">The name of the destination vertex</param>
        /// <param name="cost">cost of the edge</param>
        public void AddEdge(string source, string dest, double cost = 1)
        {
            Vertex sourceVertex = GetVertex(source);
            Vertex destVertex = GetVertex(dest);
            sourceVertex.adj.AddLast(new Edge(destVertex, cost));
        }


        /// <summary>
        ///    Clears all info within the vertices. This method will not remove any
        ///    vertices or edges.
        /// </summary>
        public void ClearAll()
        {
            foreach (string key in vertexMap.Keys)
            {
                vertexMap[key].Reset();
            }
        }

        /// <summary>
        ///    Performs the Breadth-First algorithm for unweighted graphs.
        /// </summary>
        /// <param name="name">The name of the starting vertex</param>
        public void Unweighted(string name)
        {
            Vertex start = GetVertex(name);
            start.distance = 0;
            var queue = new Queue<Vertex>();
            queue.Enqueue(start);
            while (queue.Count != 0)
            {
                Vertex currentVertex = queue.Dequeue();
                foreach (Edge edge in currentVertex.GetAdjacents())
                {
                    // Found a new Vertex
                    if (edge.dest.distance == INFINITY)
                    {
                        edge.dest.distance = currentVertex.distance + 1;
                        edge.dest.prev = currentVertex;
                        queue.Enqueue(edge.dest);
                    }
                }
            }
        }

        /// <summary>
        ///    Performs the Dijkstra algorithm for weighted graphs.
        /// </summary>
        /// <param name="name">The name of the starting vertex</param>
        public void Dijkstra(string name)
        {
            // Insert vertex 'name' into priority queue
            Vertex start = GetVertex(name);
            start.distance = 0;
            var priorityQueue = new PriorityQueue<Vertex>();
            priorityQueue.Add(start);

            while (priorityQueue.size != 0)
            {
                Vertex currentVertex = priorityQueue.Remove();
                if (currentVertex.known)
                    continue;
                currentVertex.known = true;
                foreach (Edge edge in currentVertex.GetAdjacents())
                {
                    if (!edge.dest.known)
                    {
                        if (currentVertex.distance + edge.cost < edge.dest.distance)
                        {
                            edge.dest.distance = currentVertex.distance + edge.cost;
                            edge.dest.prev = currentVertex;
                        }

                        priorityQueue.Add(edge.dest);
                    }
                }
            }
        }

        /// <summary>
        ///    Converts this instance of Graph to its string representation.
        ///    It will call the ToString method of each Vertex. The output is
        ///    ascending on vertex name.
        /// </summary>
        /// <returns>The string representation of this Graph instance</returns>
        public override string ToString()
        {
            string result = "";
            foreach (string key in vertexMap.Keys.OrderBy(x => x))
            {
                result += vertexMap[key] + "\n";
            }

            return result;
        }
    }
}