using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoordinateConverter.Model
{
    public class TableRow
    {
        public RectCoord RectCoord { get; set; }
        public GeoCoord GeoCoord { get; set; }
        public string Description { get; set; }
    }
}
