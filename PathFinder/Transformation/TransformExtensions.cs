using System.IO;
using PathFinder.Domain;
using PathFinder.Utilities;

namespace PathFinder.Transformation
{
    public static class TransformExtensions
    {
        public static GPSData Read(this ITextTransform transform, string data)
        {
            transform.ThrowIfNull("transform");
            data.ThrowIfNull("data");
            using (var sr = new StringReader(data))
                return transform.Read(sr);
        }
    }
}