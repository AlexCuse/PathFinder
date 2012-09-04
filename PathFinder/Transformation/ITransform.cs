using PathFinder.Domain;
using System.IO;

namespace PathFinder.Transformation
{
    public interface ITransform
    {
        GPSData TransformInput(TextReader textReader);
    }
}