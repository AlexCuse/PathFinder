namespace PathFinder.Domain.Distance
{
    public class Odometer : IDataCollector
    {
        double _distance;
        public void Collect(WayPoint previous, WayPoint current)
        {
            _distance += Calculations.HaversineDistanceInMeters(previous, current);
        }

        public double Distance { get { return _distance; } } 
    }
}
