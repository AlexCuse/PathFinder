using PathFinder.Domain.Distance;

namespace PathFinder.Domain.Timing
{
    public class Timer : IDataCollector
    {
        readonly double _velocityThresholdForMovingTime;

        public Timer(double velocityThresholdForMovingTime)
        {
            _velocityThresholdForMovingTime = velocityThresholdForMovingTime;
        }

        double _movingTime;
        double _totalTime;

        public void Collect(WayPoint previousPoint, WayPoint currentPoint)
        {
            if (previousPoint != null)
            {
                var distance = Calculations.HaversineDistanceInMeters(previousPoint, currentPoint);
                var seconds = (currentPoint.TimeStamp - previousPoint.TimeStamp).Value.TotalMilliseconds / 1000;

                _totalTime += seconds;

                if (distance / seconds >= _velocityThresholdForMovingTime)
                {
                    _movingTime += seconds;
                }
            }
        }

        public double TotalTime { get { return _totalTime; } }
        public double MovingTime { get { return _movingTime; } }
        public double StoppedTime { get { return TotalTime - MovingTime; } }
    }
}
