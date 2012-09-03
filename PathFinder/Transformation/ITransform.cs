using PathFinder.Domain;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace PathFinder.Transformation
{
    public interface ITransform
    {
        GPSData TransformInput(TextReader textReader);
    }
}