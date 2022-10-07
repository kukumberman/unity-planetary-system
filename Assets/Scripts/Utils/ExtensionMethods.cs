using System;
using System.Collections;

public static class ExtensionMethods
{
    public static void ShuffleSelf(this IList collection, Random rnd)
    {
        for (int i = collection.Count - 1; i > 0; i--)
        {
            int j = rnd.Next(0, i + 1);
            (collection[i], collection[j]) = (collection[j], collection[i]);
        }
    }

    public static double NextDouble(this Random random, double minValue, double maxValue)
    {
        return random.NextDouble() * (maxValue - minValue) + minValue;
    }
}
