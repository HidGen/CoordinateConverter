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
                case CoordinateType.SK42zone2:                   
                    BasicTypeString = ConfigurationManager.AppSettings["GKzone2"];                    
                    break;
                case CoordinateType.SK42zone3:
                    BasicTypeString = ConfigurationManager.AppSettings["GKzone3"];
                    break;
                case CoordinateType.SK42zone4:
                    BasicTypeString = ConfigurationManager.AppSettings["GKzone4"];
                    break;
                case CoordinateType.SK42zone5:
                    BasicTypeString = ConfigurationManager.AppSettings["GKzone5"];
                    break;
                case CoordinateType.SK42zone6:
                    BasicTypeString = ConfigurationManager.AppSettings["GKzone6"];
                    break;
                case CoordinateType.SK42zone7:
                    BasicTypeString = ConfigurationManager.AppSettings["GKzone7"];
                    break;
                case CoordinateType.SK42zone8:
                    BasicTypeString = ConfigurationManager.AppSettings["GKzone8"];
                    break;
                case CoordinateType.SK42zone9:
                    BasicTypeString = ConfigurationManager.AppSettings["GKzone9"];
                    break;
                case CoordinateType.SK42zone10:
                    BasicTypeString = ConfigurationManager.AppSettings["GKzone10"];
                    break;
                case CoordinateType.SK42zone11:
                    BasicTypeString = ConfigurationManager.AppSettings["GKzone11"];
                    break;
                case CoordinateType.SK42zone12:
                    BasicTypeString = ConfigurationManager.AppSettings["GKzone12"];
                    break;
                case CoordinateType.SK42zone13:
                    BasicTypeString = ConfigurationManager.AppSettings["GKzone13"];
                    break;
                case CoordinateType.SK42zone14:
                    BasicTypeString = ConfigurationManager.AppSettings["GKzone14"];
                    break;
                case CoordinateType.SK42zone15:
                    BasicTypeString = ConfigurationManager.AppSettings["GKzone15"];
                    break;
                case CoordinateType.SK42zone16:
                    BasicTypeString = ConfigurationManager.AppSettings["GKzone16"];
                    break;
                case CoordinateType.SK42zone17:
                    BasicTypeString = ConfigurationManager.AppSettings["GKzone17"];
                    break;
                case CoordinateType.SK42zone18:
                    BasicTypeString = ConfigurationManager.AppSettings["GKzone18"];
                    break;
                case CoordinateType.SK42zone19:
                    BasicTypeString = ConfigurationManager.AppSettings["GKzone19"];
                    break;
                case CoordinateType.SK42zone20:
                    BasicTypeString = ConfigurationManager.AppSettings["GKzone20"];
                    break;
                case CoordinateType.SK42zone21:
                    BasicTypeString = ConfigurationManager.AppSettings["GKzone21"];
                    break;
                case CoordinateType.SK42zone22:
                    BasicTypeString = ConfigurationManager.AppSettings["GKzone22"];
                    break;
                case CoordinateType.SK42zone23:
                    BasicTypeString = ConfigurationManager.AppSettings["GKzone23"];
                    break;
                case CoordinateType.SK42zone24:
                    BasicTypeString = ConfigurationManager.AppSettings["GKzone24"];
                    break;
                case CoordinateType.SK42zone25:
                    BasicTypeString = ConfigurationManager.AppSettings["GKzone25"];
                    break;
                case CoordinateType.SK42zone26:
                    BasicTypeString = ConfigurationManager.AppSettings["GKzone26"];
                    break;
                case CoordinateType.SK42zone27:
                    BasicTypeString = ConfigurationManager.AppSettings["GKzone27"];
                    break;
                case CoordinateType.SK42zone28:
                    BasicTypeString = ConfigurationManager.AppSettings["GKzone28"];
                    break;
                case CoordinateType.SK42zone29:
                    BasicTypeString = ConfigurationManager.AppSettings["GKzone29"];
                    break;
                case CoordinateType.SK42zone30:
                    BasicTypeString = ConfigurationManager.AppSettings["GKzone30"];
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
