using System;
using System.Collections.ObjectModel;

namespace PathFinder.Domain
{
    public class GPSData : Collection<Way> { }

    public class Way : Collection<WayPoint> { }

    public class WayPoint
    {
        public Double Latitude { get; set; }
        public Double Longitude { get; set; }
        public Double? Elevation { get; set; }
        public DateTime? TimeStamp { get; set; }
    }
}
