using PathFinder.Domain;

namespace PathFinder.Loaders
{
    public interface IFileLoader
    {
        GPSData Load(string filePath);
    }
}