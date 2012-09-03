#define SUPPORT_TASKS

using PathFinder.Domain;
using PathFinder.Utilities;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace PathFinder.Transformation
{
    public static class TransformExtensions
    {

#if SUPPORT_TASKS
        public static Task<GPSData> AsyncTransformInput(this ITransform transform, TextReader textReader)
        {
            transform.ThrowIfNull("transform");
            return Task<GPSData>.Factory.StartNew(() => transform.TransformInput(textReader));
        }

        public static Task<GPSData> AsyncTransformTextInput(this ITransform transform, string text)
        {
            transform.ThrowIfNull("transform");
            return Task<GPSData>.Factory.StartNew(() => transform.TransformTextInput(text));
        }

        public static Task<GPSData> AsyncTransformFileInput(this ITransform transform, string filePath, Encoding encoding = null)
        {
            transform.ThrowIfNull("transform");
            return Task<GPSData>.Factory.StartNew(() => transform.TransformFileInput(filePath, encoding));
        }
#endif

        public static GPSData TransformTextInput(this ITransform transform, string text)
        {
            transform.ThrowIfNull("transform");
            using (var tr = new StringReader(text))
                return transform.TransformInput(tr);
        }

        public static GPSData TransformFileInput(this ITransform transform, string filePath, Encoding encoding = null)
        {
            transform.ThrowIfNull("transform");
            using (var sr = new StreamReader(filePath, encoding ?? Encoding.Default))
                return transform.TransformInput(sr);
        }
    }
}