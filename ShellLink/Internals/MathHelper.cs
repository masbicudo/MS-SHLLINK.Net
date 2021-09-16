using System;

namespace ShellLink.Internals
{
    internal static class MathHelper
    {
        public static int Clamp(this int value, int min, int max)
        {
            return Math.Max(min, Math.Min(max, value));
        }
    }
}