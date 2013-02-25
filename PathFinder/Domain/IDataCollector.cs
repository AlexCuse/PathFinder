namespace PathFinder.Domain
{
    public interface IDataCollector
    {
        void Collect(WayPoint previous, WayPoint current);
    }
}