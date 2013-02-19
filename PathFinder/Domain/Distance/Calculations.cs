using System;

namespace PathFinder.Domain.Distance
{
    public static class Calculations
    {
        public static double HaversineDistanceInMeters(WayPoint a, WayPoint b)
        {
            const double EARTH_RADIUS = 6371; //radius in KM
            var lat = (b.Latitude - a.Latitude).ToRadians();
            var lng = (b.Longitude - a.Longitude).ToRadians();
            var h1 = Math.Sin(lat / 2) * Math.Sin(lat / 2) +
                          Math.Cos(a.Latitude.ToRadians()) * Math.Cos(b.Latitude.ToRadians()) *
                          Math.Sin(lng / 2) * Math.Sin(lng / 2);
            var h2 = 2 * Math.Asin(Math.Min(1, Math.Sqrt(h1)));
            return EARTH_RADIUS * h2 * 1000; //convert to meters
        }
    }
}
