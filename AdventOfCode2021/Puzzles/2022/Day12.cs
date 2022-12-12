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

        Vector2Int pos = FindS();
        var start = new Vertex(pos.ToString());
        FloodFill(start, pos, 'a');
        _graph.Unweighted(pos.ToString());

        PartOne = (int)_e.GetDistance();

        // Part 2
        int fewestSteps = int.MaxValue;
        List<Vector2Int> aList = FindA();
        foreach (Vector2Int vector in aList)
        {
            _graph.ClearAll();
            _graph.Unweighted(vector.ToString());
            int steps = (int)_e.GetDistance();
             if (steps < fewestSteps && steps > 0) fewestSteps = steps;
        }

        PartTwo = fewestSteps;
    }

    private Vector2Int FindS()
    {
        for (int i = 0; i < _list.Count; i++)
        {
            for (int j = 0; j < _list[i].Length; j++)
            {
                if (_list[i][j] == 'S') return new Vector2Int(j, i);
            }
        }

        return new Vector2Int();
    }
    
    private List<Vector2Int> FindA()
    {
        var res = new List<Vector2Int>();
        for (int i = 0; i < _list.Count; i++)
        {
            for (int j = 0; j < _list[i].Length; j++)
            {
                if (_list[i][j] == 'S' || _list[i][j] == 'a') res.Add(new Vector2Int(j, i));
            }
        }

        return res;
    }
    private void FloodFill(Vertex current, Vector2Int currentPos,  char currC)
    {
        var directions = new List<Vector2Int>
        {
            new(1, 0), // right
            new(0, 1), // down
            new(-1, 0), // left
            new(0, -1), // up
        };
        foreach (Vector2Int direction in directions)
        {
            Vector2Int testPos = currentPos + direction;
            if (NodeValid(testPos))
            {
                char testC = _list[testPos.Y][testPos.X];
                bool newNode = !_graph.vertexMap.ContainsKey(testPos.ToString());
                bool isEndNode = false;
                if (testC == 'E')
                {
                    testC = 'z';
                    isEndNode = true;
                }
                int cost = testC - currC;
                if (cost <= 1)
                {
                    Vertex testNode = _graph.AddVertex(testPos.ToString());
                    if (isEndNode) _e = testNode;
                    
                    _graph.AddEdge(current.name, testNode.name);
                    if(newNode)
                        FloodFill(testNode, testPos, testC);
                }
            }
        }
    }

    private bool NodeValid(Vector2Int node)
    {
        return node.X >= 0 && node.Y >= 0 && node.Y < _list.Count && node.X < _list[0].Length;
    }
}