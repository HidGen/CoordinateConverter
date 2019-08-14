using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoordinateConverter.Model
{
    public class CompleteRow
    {
        public RectCoord rectCoord { get; set; }

        public GeoCoord geoCoord { get; set; }

        public string Description { get; set; }
    }
}
