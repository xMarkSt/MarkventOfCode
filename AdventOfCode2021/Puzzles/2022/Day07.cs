using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using AdventOfCode2021.Utils;

namespace AdventOfCode2021.Puzzles._2022;

public class Day07 : AocPuzzle
{
    public override int Year => 2022;
    public override int Day => 7;

    private FileNode _tree;

    protected override void Solve(IEnumerable<string> input)
    {
        _tree = new FileNode(0, "/");
        FileNode currentNode = _tree;
        foreach (string line in input.Skip(1))
        {
            switch (line[0])
            {
                // Command
                case '$':
                    if (line.Substring(2, 2) == "cd")
                    {
                        string searchValue = line[5..];
                        if(searchValue == "..")
                        {
                            currentNode = currentNode.Parent;
                        }
                        else
                        {
                            currentNode = currentNode.FindChild(searchValue);
                        }
                    }
                    break;
                // Directory
                case 'd':
                    string dirName = line.Split(' ')[1];
                    currentNode.AddChild(new FileNode(0, dirName));
                    break;
                // File
                default:
                    (string size, string name, _) = line.Split(' ');
                    currentNode.AddChild(new FileNode(long.Parse(size), name));
                    break;
            }
        }
        
        long totalSum = 0;

        _tree.TraverseDirectory(size =>
        {
            if (size <= 100000)
            {
                totalSum += size;
            }
        });
        PartOne = totalSum;

        long spaceNeeded = _tree.Size - 40000000;
        long smallest = long.MaxValue;
        _tree.TraverseDirectory(size =>
        {
            if (size >= spaceNeeded && size < smallest)
            {
                smallest = size;
            }
        });

        PartTwo = smallest;
    }
}

public class FileNode
{
    private readonly List<FileNode> _children = new();

    public FileNode(long size, string name)
    {
        Size = size;
        Name = name;
    }

    public FileNode Parent { get; private set; }

    public long Size { get; private set; }

    public string Name { get; }

    public ReadOnlyCollection<FileNode> Children => _children.AsReadOnly();

    // Find direct child (not recursive
    public FileNode FindChild(string name)
    {
        return Children.FirstOrDefault(current => current.Name == name);
    }
    
    public FileNode AddChild(FileNode node)
    {
        node.Parent = this;
        IncreaseSizeBy(node.Size);
        _children.Add(node);
        return node;
    }

    private void IncreaseSizeBy(long size)
    {
        Size += size;
        Parent?.IncreaseSizeBy(size);
    }
    
    public void TraverseDirectory(Action<long> action)
    {
        if (Children.Count > 0)
        {
            action(Size);
        }
        foreach (FileNode child in _children)
            child.TraverseDirectory(action);
    }
}