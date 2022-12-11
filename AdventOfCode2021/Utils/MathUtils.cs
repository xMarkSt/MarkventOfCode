using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2021.Utils;

public class MathUtils
{
    public static long Lcm(long a, long b)
    {
        return a * b / Gcd(a, b);
    }
    
    public static long Lcm(IEnumerable<long> numbers)
    {
        return numbers.Aggregate(Lcm);
    }

    public static long Gcd(long a, long b)
    {
        while (a != 0 && b != 0)
        {
            if (a > b)
                a %= b;
            else
                b %= a;
        }

        return a | b;
    }
    
    public static int Gfc(int a, int b)
    {
        while (b != 0)
        {
            int temp = b;
            b = a % b;
            a = temp;
        }
        return a;
    }
}