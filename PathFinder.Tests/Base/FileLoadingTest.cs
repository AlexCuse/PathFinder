using System.IO;
using System.Reflection;
using PathFinder.Domain;
using PathFinder.Transformation;

namespace PathFinder.Tests
{
    public abstract class FileLoadingTest
    {
        protected GPSData LoadData(string fileName)
        {
            GPSData data;
            using (var stream = Assembly.GetExecutingAssembly()
                .GetManifestResourceStream("PathFinder.Tests.TransformationTests.SampleFiles." + fileName))
            using (var reader = new StreamReader(stream))
            {
                data = Transform.Gpx.Read(reader);
            }
            return data;
        }
    }
}
