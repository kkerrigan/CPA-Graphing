/*
 * Kristian Kerrigan
 * ExtensionMethods.cs
 * This static class contains the method needed to round an integer number
 * to the nearest 10
 */

using System;

namespace KKerrigan_Graphing.Helpers
{
    public static class ExtensionMethods
    {
        public static int RoundToNearestTen(this int i)
        {
            return (int)(Math.Ceiling(i / 10.0d) * 10);
        }
    }
}
