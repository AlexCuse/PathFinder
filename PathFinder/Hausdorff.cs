using System;
using System.Linq;
using System.Collections.Generic;
using GeoAPI.Geometries;
using NetTopologySuite.Geometries;

namespace PathFinder
{
    public class Hausdorff
    {
        //Naive calculation of Hausdorff distance between two sets of points
        public static double Distance(IEnumerable<Coordinate> a, IEnumerable<Coordinate> b)
        {
            var distance = 0D; //drive distance up
            var shortest = double.MaxValue; //drive shortest found down

            foreach(var d in a.Select(pa => b.Min(pb => pb.Distance(pa))))
            {
                if(d < shortest)
                    shortest = d;
                if(shortest > distance)
                    distance = shortest;
            }

            return distance;
            //return a.Max(pa => b.Min(pb => pb.Distance(pa)));
        }

        //http://mapcontext.com/autocarto/proceedings/auto-carto-12/pdf/computation-of-the-hausdorff-distance-between-plane.pdf
        public static double Distance(LineString a, LineString b)
        {
            //computation of distances from the vertices of A to polyline B (need to know specific segments)
            var aVertices = a.Points().ToList();
            var bVertices = b.Points().ToList();

            var bSegments = b.Segments().ToList();
            var aSegments = a.Segments().ToList();

            var closestCache = new Dictionary<IGeometry, Tuple<IGeometry, double>>();

            Func<IGeometry, Tuple<IGeometry, double>> closest = p =>
            {
                if(!closestCache.ContainsKey(p))
                {
                    closestCache.Add(p, ClosestGeometry(p, bVertices, bSegments));
                }
                return closestCache[p];
            };

            Func<IGeometry, IGeometry, bool> pointsAreSuccessiveVertices = (cs, ce) =>
            {
                var point1 = cs as IPoint;
                var point2 = ce as IPoint;

                return (point1 != null && point2 != null) && Math.Abs(bVertices.IndexOf(point1) - bVertices.IndexOf(point2)) == 1;
            };

            var verticesWithDistance = aVertices
                .Select(pa => 
                {
                    var cst = closest(pa);
                    return Tuple.Create(pa, cst.Item1, cst.Item2);
                });

            var distance = verticesWithDistance.Max(tpl => tpl.Item3);

            //test to detect whether further calculation is required or a vertex of A bears the greatest distance from A to B
            var needsInvestigation = aSegments
                .Where(aseg =>
                {
                    //if we can confirm distance between segments is increasing/decreasing, no need to investigate that segment
                    // test is that closest components for each point (segment OR vertex) are the same, or closest components are successive vertices in B
                    var closestToStart = closest(aseg.StartPoint).Item1;
                    var closestToEnd = closest(aseg.EndPoint).Item1;

                    return !(closestToStart == closestToEnd 
                        || pointsAreSuccessiveVertices(closestToStart, closestToEnd));
                })
                .Select(aseg =>
                {
                    var cst = closest(aseg);
                    return Tuple.Create(aseg, cst.Item1, cst.Item2);
                })
                .ToList();
                

            if(needsInvestigation.Count > 0) //check if a vertex doesn't bear the greatest distance
            {
                //and if it doesn't  we need to compute the greatest distance on likely segments and reassign distance here
                throw new NotImplementedException();
            }

            return distance;
        }

        static Tuple<IGeometry, double> ClosestGeometry(IGeometry from, List<IPoint> candidatePoints, List<LineString> candidateSegments)
        {
            var pointsLookup = candidatePoints.ToDictionary(bs => bs, from.Distance);
            var pointsKvp = pointsLookup.Single(lu => lu.Value == pointsLookup.Min(alu => alu.Value));

            var segmentsLookup = candidateSegments.ToDictionary(bs => bs, from.Distance);
            var segmentsKvp = segmentsLookup.Single(lu => lu.Value == segmentsLookup.Min(alu => alu.Value));

            if(segmentsKvp.Value < pointsKvp.Value)
            {
                return Tuple.Create((IGeometry)segmentsKvp.Key, segmentsKvp.Value);
            }
            return Tuple.Create((IGeometry)pointsKvp.Key, pointsKvp.Value);
        }
    }
}
