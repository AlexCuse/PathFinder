using System.IO;
using PathFinder.Domain;
using PathFinder.Utilities;
using System.Text;

namespace PathFinder.Transformation
{
    public static class TransformExtensions
    {
        public static GPSData Read(this ITransform transform, byte[] data)
        {
            return Read(transform, data, null);
        }

        public static GPSData Read(this ITransform transform, byte[] data, Encoding encoding)
        {
            transform.ThrowIfNull("transform");
            data.ThrowIfNull("data");
            using (var ms = new MemoryStream(data))
            using (var br = new BinaryReader(ms, encoding ?? Encoding.Default))
                return transform.Read(br);
        }

        public static GPSData Read(this ITransform transform, string data)
        {
            transform.ThrowIfNull("transform");
            data.ThrowIfNull("data");
            using (var sr = new StringReader(data))
                return transform.Read(sr);
        }
    }
}