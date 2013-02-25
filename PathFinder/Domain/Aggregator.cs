using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PathFinder.Domain
{
    public class Aggregator
    {
        private readonly IEnumerable<IDataCollector> _collectors;

        public Aggregator(IEnumerable<IDataCollector> collectors)
        {
            _collectors = collectors;
        }

        public IEnumerable<IDataCollector> Collectors { get { return _collectors; } }

        public void Aggregate(GPSData data)
        {
            var points = data.SelectMany(w => w).ToArray();
            var previous = points.First();
            foreach (var point in points.Skip(1))
            {
                foreach (var collector in _collectors)
                {
                    collector.Collect(previous, point);
                }
                previous = point;
            }
        }
    }
}
