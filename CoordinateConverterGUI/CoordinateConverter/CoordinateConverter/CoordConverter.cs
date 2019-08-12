using CoordinateConverter.Model;
using DotSpatial.Projections;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoordinateConverter
{
   public class CoordConverter
    {
        public string BasicTypeString { get; private set; }
        public enum CoordinateSystem
        {
            LCS46_1,
            LCS46_2,
            CS42,
            CS63
        }
        public void ChangeType(CoordinateSystem coordinateSystem)
        {
            switch (coordinateSystem)
            {
                case CoordinateSystem.LCS46_1:
                    BasicTypeString = ConfigurationManager.AppSettings["LCS46_1"];
                    break;
                case CoordinateSystem.LCS46_2:
                    BasicTypeString = ConfigurationManager.AppSettings["LCS46_2"];
                    break;
                case CoordinateSystem.CS42:
                    //BasicTypeString =ConfigurationManager.AppSettings["CS42"];
                    BasicTypeString = ConfigurationManager.AppSettings["GKzone2"];
                    //throw new Exception("Coordinate system isn't supported yet");
                    break;
                case CoordinateSystem.CS63:
                    throw new Exception("Coordinate system isn't supported yet");
                    BasicTypeString = @"";
                    break;
            }
        }

        public GeoCoord Convert(CoordinateSystem basicCoordinateSystem, RectCoord rectCoord)
        {
            ChangeType(basicCoordinateSystem);
            ProjectionInfo src = ProjectionInfo.FromProj4String(BasicTypeString);
            ProjectionInfo trg = ProjectionInfo.FromProj4String("+proj=longlat +datum=WGS84 +no_defs");
            double[] xy =  { rectCoord.x, rectCoord.y };
            double[] h = { rectCoord.h };
            Reproject.ReprojectPoints(xy,h , src, trg, 0, 1);
            var geoCoord = new GeoCoord {lon = xy[0],lat= xy[1],h=h[0] };
            return geoCoord;
        }
    }// NuGet DotSpatial.Projections
}
