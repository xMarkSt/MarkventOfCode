using System;

namespace AdventOfCode2021.Datastructures;

public class Vector2Int
{
    public int X { get; set; }
    public int Y { get; set; }

    public Vector2Int()
    {
    }

    public Vector2Int(int x, int y)
    {
        this.X = x;
        this.Y = y;
    }

    public static Vector2Int operator +(Vector2Int v1, Vector2Int v2)
    {
        return new Vector2Int(v1.X + v2.X, v1.Y + v2.Y);
    }

    public static Vector2Int operator -(Vector2Int v1, Vector2Int v2)
    {
        return new Vector2Int(v1.X - v2.X, v1.Y - v2.Y);
    }

    public static Vector2Int operator *(Vector2Int v, int scalar)
    {
        return new Vector2Int(v.X * scalar, v.Y * scalar);
    }

    public static Vector2Int operator *(int scalar, Vector2Int v)
    {
        return new Vector2Int(v.X * scalar, v.Y * scalar);
    }
    
    public static Vector2Int operator /(Vector2Int v, int div)
    {
        return new Vector2Int(v.X / div, v.Y / div);
    }

    public static Vector2Int operator /(int div, Vector2Int v)
    {
        return new Vector2Int(div / v.X, div / v.Y);
    }

    public override string ToString()
    {
        return $"[{X},{Y}]";
    }

    private bool Equals(Vector2Int other)
    {
        return X.Equals(other.X) && Y.Equals(other.Y);
    }

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((Vector2Int)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(X, Y);
    }
}