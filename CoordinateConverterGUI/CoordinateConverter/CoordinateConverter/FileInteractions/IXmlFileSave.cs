using CoordinateConverter.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoordinateConverter.FileInteractions
{
    interface IXmlFileSave
    {
        void SaveToXml(string path, List<GeoCoord> geoCoords);
    }
}
