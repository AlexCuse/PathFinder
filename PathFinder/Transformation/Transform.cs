using PathFinder.Transformation.Gpx;
using PathFinder.Utilities;
using System;
using System.Collections.Generic;

namespace PathFinder.Transformation
{
    public static class Transform
    {
        public static readonly ITransform Gpx = new GpxTransform();

        private static readonly Dictionary<string, ITransform> _keyMap = new Dictionary<string, ITransform>
        {
            { "gpx", Gpx }
        };

        public static ITransform Get(string key)
        {
            key.ThrowIfNull("key");
            ITransform result;
            if (!_keyMap.TryGetValue(key.Trim().ToLowerInvariant(), out result))
                throw new ArgumentException();
            return result;
        }
    }
}