using CoordinateConverter.Model;
using System.Collections.Generic;

namespace CoordinateConverter.FileInteractions
{
    interface IXmlFileSave
    {
        void SaveToXml(string path, List<GeoCoord> geoCoords);
    }
}
