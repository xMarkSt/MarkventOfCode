using System;

namespace AdventOfCode2021.Datastructures;

public class Vector3Int
{
    private readonly int x;
    private readonly int y;
    private readonly int z;

    public Vector3Int()
    {
    }

    public Vector3Int(int x, int y, int z)
    {
        this.x = x;
        this.y = y;
        this.z = z;
    }

    public static Vector3Int operator +(Vector3Int v1, Vector3Int v2)
    {
        return new Vector3Int(v1.x + v2.x, v1.y + v2.y, v1.z + v2.z);
    }

    public static Vector3Int operator -(Vector3Int v1, Vector3Int v2)
    {
        return new Vector3Int(v1.x - v2.x, v1.y - v2.y, v1.z - v2.z);
    }

    public static Vector3Int operator *(Vector3Int v, int scalar)
    {
        return new Vector3Int(v.x * scalar, v.y * scalar, v.z * scalar);
    }

    public static Vector3Int operator *(int scalar, Vector3Int v)
    {
        return new Vector3Int(v.x * scalar, v.y * scalar, v.z * scalar);
    }
    
    public static Vector3Int operator /(Vector3Int v, int div)
    {
        return new Vector3Int(v.x / div, v.y / div, v.z / div);
    }

    public static Vector3Int operator /(int div, Vector3Int v)
    {
        return new Vector3Int(div / v.x, div / v.y, div / v.z);
    }

    public override string ToString()
    {
        return $"[{x},{y},{z}]";
    }

    private bool Equals(Vector3Int other)
    {
        return x.Equals(other.x) && y.Equals(other.y) && z.Equals(other.z);
    }

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((Vector3Int)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(x, y, z);
    }
}