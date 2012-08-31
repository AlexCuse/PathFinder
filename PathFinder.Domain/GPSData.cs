using System;
using System.Collections.ObjectModel;

namespace PathFinder.Domain
{
    public class GPSData : Collection<Way> { }

    public class Way : Collection<WayPoint> { }

    public class WayPoint
    {
        public Decimal Latitude { get; set; }
        public Decimal Longitude { get; set; }
        public Decimal? Elevation { get; set; }
    }
}
