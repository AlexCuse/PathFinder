using PathFinder.Domain;

namespace PathFinder.Loaders
{
    public interface IFileLoader
    {
        PathCollection Load(string filePath);
    }
}