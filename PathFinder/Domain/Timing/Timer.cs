using System.Linq;
using PathFinder.Domain.Distance;

namespace PathFinder.Domain.Timing
{
    public interface ITimer
    {
        TimerResult Time(GPSData data);
    }

    public class Timer : ITimer
    {
        readonly double _velocityThresholdForMovingTime;

        public Timer(double velocityThresholdForMovingTime)
        {
            _velocityThresholdForMovingTime = velocityThresholdForMovingTime;
        }

        public TimerResult Time(GPSData data)
        {
            var points = data.SelectMany(w => w).ToArray();
            var totalTime = (points[points.Length - 1].TimeStamp - points[0].TimeStamp).Value.TotalSeconds;
            double movingTime = totalTime; //overwrite if we can

            if (points.Any(p => p.TimeStamp.HasValue))
            {
                movingTime = 0;

                WayPoint currentPoint = null;
                WayPoint previousPoint = null;

                for (var i = 0; i < points.Length; i++)
                {
                    previousPoint = currentPoint;
                    currentPoint = points[i];

                    if (previousPoint != null)
                    {
                        var distance = Calculations.HaversineDistanceInMeters(previousPoint, currentPoint);
                        var seconds = (currentPoint.TimeStamp - previousPoint.TimeStamp).Value.TotalMilliseconds / 1000;

                        if (distance / seconds >= _velocityThresholdForMovingTime) 
                        {
                            movingTime += seconds;
                        }
                    }
                }
            }

            return new TimerResult { TotalTime = totalTime, MovingTime = movingTime };
        }
    }

    public class TimerResult
    {
        public double TotalTime { get; set; }
        public double MovingTime { get; set; }

        public double StoppedTime
        {
            get { return TotalTime - MovingTime; }
        }
    }
}
