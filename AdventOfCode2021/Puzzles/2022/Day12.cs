using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using AdventOfCode2021.Datastructures;
using AdventOfCode2021.Utils;

namespace AdventOfCode2021.Puzzles._2022;

public class Day12 : AocPuzzle
{
    public override int Year => 2022;
    public override int Day => 12;

    private Graph _graph;
    private List<string> _list;
    private Vertex _e;
    protected override void Solve(IEnumerable<string> input)
    {
        _list = input.ToList();
        _graph = new Graph();

        Vector2 pos = FindS();
        var start = new Vertex(pos.GetHashCode().ToString())
        {
            Pos = pos
        };
        FloodFill(start, 'a');
        _graph.Dijkstra(pos.GetHashCode().ToString());
        Vertex prev = _e.prev;
        int steps = 0;
        while (prev != null)
        {
            prev = prev.prev;
            steps++;
        }

        PartOne = steps;

        // Part 2
        int fewestSteps = int.MaxValue;
        List<Vector2> aList = FindA();
        foreach (Vector2 vector2 in aList)
        {
            _graph = new Graph();
            start = new Vertex(vector2.GetHashCode().ToString())
            {
                Pos = vector2
            };
            FloodFill(start, 'a');
            _graph.Dijkstra(vector2.GetHashCode().ToString());
            prev = _e.prev;
            steps = 0;
            while (prev != null)
            {
                prev = prev.prev;
                steps++;
            }

            if (steps < fewestSteps && steps != 0) fewestSteps = steps;
        }

        PartTwo = fewestSteps;
    }

    private Vector2 FindS()
    {
        for (int i = 0; i < _list.Count; i++)
        {
            for (int j = 0; j < _list[i].Length; j++)
            {
                if (_list[i][j] == 'S') return new Vector2(j, i);
            }
        }

        return new Vector2();
    }
    
    private List<Vector2> FindA()
    {
        var res = new List<Vector2>();
        for (int i = 0; i < _list.Count; i++)
        {
            for (int j = 0; j < _list[i].Length; j++)
            {
                if (_list[i][j] == 'S' || _list[i][j] == 'a') res.Add(new Vector2(j, i));
            }
        }

        return res;
    }
    private void FloodFill(Vertex current, char currC)
    {
        var directions = new List<Vector2>
        {
            new(1, 0), // right
            new(0, 1), // down
            new(-1, 0), // left
            new(0, -1), // up
        };
        foreach (Vector2 direction in directions)
        {
            Vector2 testPos = current.Pos + direction;
            if (NodeValid(testPos))
            {
                char testC = _list[(int)testPos.Y][(int)testPos.X];
                bool newNode = !_graph.vertexMap.ContainsKey(testPos.GetHashCode().ToString());
                bool isEndNode = false;
                if (testC == 'E')
                {
                    testC = 'z';
                    isEndNode = true;
                }
                int cost = testC - currC;
                if (cost <= 1)
                {
                    Vertex testNode = _graph.AddVertex(testPos.GetHashCode().ToString());
                    if (isEndNode) _e = testNode;
                    testNode.Pos = testPos;
                    
                    _graph.AddEdge(current.name, testNode.name);
                    if(newNode)
                        FloodFill(testNode, testC);
                }
            }
        }
    }

    private bool NodeValid(Vector2 node)
    {
        return node.X >= 0 && node.Y >= 0 && node.Y < _list.Count && node.X < _list[0].Length;
    }
}