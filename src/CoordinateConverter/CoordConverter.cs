using CoordinateConverter.Model;
using DotSpatial.Projections;
using System.Configuration;

namespace CoordinateConverter
{
    public class CoordConverter
    {
        public string BasicTypeString { get; private set; }         

        public GeoCoord Convert(RectCoord rectCoord)
        {
            if (rectCoord.Y < 1750000)
                BasicTypeString = ConfigurationManager.AppSettings["LCS46_1"];
            else
                BasicTypeString = ConfigurationManager.AppSettings["LCS46_2"];

            ProjectionInfo src = ProjectionInfo.FromProj4String(BasicTypeString);
            ProjectionInfo trg = ProjectionInfo.FromProj4String("+proj=longlat +datum=WGS84 +no_defs");
            double[] xy = { rectCoord.Y, rectCoord.X };
            double[] h = { 0 };
            Reproject.ReprojectPoints(xy, h, src, trg, 0, 1);
            var geoCoord = new GeoCoord { Lon = xy[0], Lat = xy[1], Alt = h[0] };
            return geoCoord;
        }
    }
}
