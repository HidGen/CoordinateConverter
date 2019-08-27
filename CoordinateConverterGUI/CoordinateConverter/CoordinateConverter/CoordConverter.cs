using CoordinateConverter.Model;
using CoordinateConverter.ViewModel;
using DotSpatial.Projections;
using System;
using System.Configuration;

namespace CoordinateConverter
{
    public class CoordConverter
    {
        public string BasicTypeString { get; private set; }
     
        private void ChangeType(CoordinateType coordinateSystem)
        {

            switch (coordinateSystem)
            {
                case CoordinateType.MSK461:
                    BasicTypeString = ConfigurationManager.AppSettings["LCS46_1"];
                    break;
                case CoordinateType.MSK462:
                    BasicTypeString = ConfigurationManager.AppSettings["LCS46_2"];
                    break;
                case CoordinateType.SK42:
                    //BasicTypeString =ConfigurationManager.AppSettings["CS42"];
                    BasicTypeString = ConfigurationManager.AppSettings["GKzone2"];
                    //throw new Exception("Coordinate system isn't supported yet");
                    break;
                case CoordinateType.SK63:
                    throw new Exception("Coordinate system isn't supported yet");
                    BasicTypeString = @"";
                    break;
            }
        }

        public GeoCoord Convert(CoordinateType basicCoordinateSystem, RectCoord rectCoord)
        {
            ChangeType(basicCoordinateSystem);
            ProjectionInfo src = ProjectionInfo.FromProj4String(BasicTypeString);
            ProjectionInfo trg = ProjectionInfo.FromProj4String("+proj=longlat +datum=WGS84 +no_defs");
            double[] xy = { rectCoord.X, rectCoord.Y };
            double[] h = { rectCoord.H };
            Reproject.ReprojectPoints(xy, h, src, trg, 0, 1);
            var geoCoord = new GeoCoord { Lon = xy[0], Lat = xy[1], H = h[0] };
            return geoCoord;
        }
    }
}
