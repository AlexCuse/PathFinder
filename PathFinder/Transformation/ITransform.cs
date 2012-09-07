using PathFinder.Domain;
using System.IO;

namespace PathFinder.Transformation
{
    /// <summary>
    /// Defines methods that are used to transform domain values to/from a data format.
    /// When implementing this interface do not maintain unmanaged resources between calls.
    /// </summary>
    public interface ITransform
    {
        GPSData Read(BinaryReader reader);

        GPSData Read(TextReader reader);


        void Write(BinaryWriter writer, GPSData data);

        void Write(TextWriter writer, GPSData data);
    }
}