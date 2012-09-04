using System.IO;
using PathFinder.Domain;
using PathFinder.Utilities;
using System.Text;

namespace PathFinder.Transformation
{
    public static class TransformExtensions
    {
        public static GPSData TransformInput(this ITransform transform, byte[] data)
        {
            return TransformInput(transform, data, null);
        }

        public static GPSData TransformInput(this ITransform transform, byte[] data, Encoding encoding)
        {
            transform.ThrowIfNull("transform");
            data.ThrowIfNull("data");
            using (var ms = new MemoryStream(data))
            using (var br = new BinaryReader(ms, encoding ?? Encoding.Default))
                return transform.TransformInput(br);
        }

        public static GPSData TransformInput(this ITransform transform, string data)
        {
            transform.ThrowIfNull("transform");
            data.ThrowIfNull("data");
            using (var sr = new StringReader(data))
                return transform.TransformInput(sr);
        }
    }
}