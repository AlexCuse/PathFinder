using PathFinder.Transformation.Gpx;
using PathFinder.Utilities;
using System;
using System.Collections.Generic;

namespace PathFinder.Transformation
{
    public static class Transform
    {
        public static readonly ITextTransform Gpx = new GpxTransform();

        private static readonly Dictionary<string, ITextTransform> _keyMap = new Dictionary<string, ITextTransform>
        {
            { "gpx", Gpx }
        };

        public static ITextTransform Get(string key)
        {
            key.ThrowIfNull("key");
            ITextTransform result;
            if (!_keyMap.TryGetValue(key.Trim().ToLowerInvariant(), out result))
                throw new ArgumentException();
            return result;
        }
    }
}