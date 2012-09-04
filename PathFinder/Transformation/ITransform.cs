using PathFinder.Domain;
using System.IO;

namespace PathFinder.Transformation
{
    /// <summary>
    /// Defines methods that are used to transform domain values to/from a data format.
    /// </summary>
    public interface ITransform
    {
        /// <summary>
        /// Transforms binary input data to an instance of <see cref="PathFinder.Domain.GPSData" />.
        /// </summary>
        /// <param name="binaryReader">An instance of <see cref="System.IO.BinaryReader" /> to read from. It is the responsibility of the caller to dispose of the instance.</param>
        /// <returns>An instance of <see cref="PathFinder.Domain.GPSData" /> that represents the input.</returns>
        /// <exception cref="System.ArgumentNullException">binaryReader is null</exception>
        GPSData TransformInput(BinaryReader binaryReader);

        /// <summary>
        /// Transforms textual input data to an instance of <see cref="PathFinder.Domain.GPSData" />.
        /// </summary>
        /// <param name="textReader">An instance of <see cref="System.IO.TextReader" /> to read from. It is the responsibility of the caller to dispose of the instance.</param>
        /// <returns>An instance of <see cref="PathFinder.Domain.GPSData" /> that represents the input.</returns>
        /// <exception cref="System.ArgumentNullException">textReader is null</exception>
        GPSData TransformInput(TextReader textReader);
    }
}