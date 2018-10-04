using System;

public static class IntArrayExtensions
{
    public static int[] GetRange(this int[] array, int startIndex, int count)
    {
        var result = new int[count];
        Array.Copy(array, startIndex, result, 0, count);
        return result;
    }
}