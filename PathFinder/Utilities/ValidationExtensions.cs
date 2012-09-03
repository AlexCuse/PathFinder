using System;

namespace PathFinder.Utilities
{
    public static class ValidationExtensions
    {
        public static void ThrowIfNull(this object o, string name)
        {
            if (o == null)
                throw new ArgumentNullException(name);
        }
    }
}