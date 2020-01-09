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
                case CoordinateType.SK63AreaCzone1:
                    BasicTypeString = ConfigurationManager.AppSettings["GK63AreaCzone1"];
                    break;
                case CoordinateType.SK63AreaCzone2:
                    BasicTypeString = ConfigurationManager.AppSettings["GK63AreaCzone2"];
                    break;
                case CoordinateType.SK63AreaCzone3:
                    BasicTypeString = ConfigurationManager.AppSettings["GK63AreaCzone3"];
                    break;
                case CoordinateType.SK63AreaCzone4:
                    BasicTypeString = ConfigurationManager.AppSettings["GK63AreaCzone4"];
                    break;
                case CoordinateType.SK63AreaCzone5:
                    BasicTypeString = ConfigurationManager.AppSettings["GK63AreaCzone5"];
                    break;
                case CoordinateType.SK63AreaCzone6:
                    BasicTypeString = ConfigurationManager.AppSettings["GK63AreaCzone6"];
                    break;
                case CoordinateType.SK63AreaDzone1:
                    BasicTypeString = ConfigurationManager.AppSettings["GK63AreaDzone1"];
                    break;
                case CoordinateType.SK63AreaDzone2:
                    BasicTypeString = ConfigurationManager.AppSettings["GK63AreaDzone2"];
                    break;
                case CoordinateType.SK63AreaDzone3:
                    BasicTypeString = ConfigurationManager.AppSettings["GK63AreaDzone3"];
                    break;
                case CoordinateType.SK63AreaDzone4:
                    BasicTypeString = ConfigurationManager.AppSettings["GK63AreaDzone4"];
                    break;
                case CoordinateType.SK63AreaDzone5:
                    BasicTypeString = ConfigurationManager.AppSettings["GK63AreaDzone5"];
                    break;
                case CoordinateType.SK63AreaDzone6:
                    BasicTypeString = ConfigurationManager.AppSettings["GK63AreaDzone6"];
                    break;
                case CoordinateType.SK63AreaDzone7:
                    BasicTypeString = ConfigurationManager.AppSettings["GK63AreaDzone7"];
                    break;
                case CoordinateType.SK63AreaDzone8:
                    BasicTypeString = ConfigurationManager.AppSettings["GK63AreaDzone8"];
                    break;
                case CoordinateType.SK63AreaEzone1:
                    BasicTypeString = ConfigurationManager.AppSettings["GK63AreaEzone1"];
                    break;
                case CoordinateType.SK63AreaEzone2:
                    BasicTypeString = ConfigurationManager.AppSettings["GK63AreaEzone2"];
                    break;
                case CoordinateType.SK63AreaEzone3:
                    BasicTypeString = ConfigurationManager.AppSettings["GK63AreaEzone3"];
                    break;
                case CoordinateType.SK63AreaEzone4:
                    BasicTypeString = ConfigurationManager.AppSettings["GK63AreaEzone4"];
                    break;
                case CoordinateType.SK63AreaEzone5:
                    BasicTypeString = ConfigurationManager.AppSettings["GK63AreaEzone5"];
                    break;
                case CoordinateType.SK63AreaFzone1:
                    BasicTypeString = ConfigurationManager.AppSettings["GK63AreaFzone1"];
                    break;
                case CoordinateType.SK63AreaFzone2:
                    BasicTypeString = ConfigurationManager.AppSettings["GK63AreaFzone2"];
                    break;
                case CoordinateType.SK63AreaFzone3:
                    BasicTypeString = ConfigurationManager.AppSettings["GK63AreaFzone3"];
                    break;
                case CoordinateType.SK63AreaGzone1:
                    BasicTypeString = ConfigurationManager.AppSettings["GK63AreaGzone1"];
                    break;
                case CoordinateType.SK63AreaGzone2:
                    BasicTypeString = ConfigurationManager.AppSettings["GK63AreaGzone2"];
                    break;
                case CoordinateType.SK63AreaGzone3:
                    BasicTypeString = ConfigurationManager.AppSettings["GK63AreaGzone3"];
                    break;
                case CoordinateType.SK63AreaGzone4:
                    BasicTypeString = ConfigurationManager.AppSettings["GK63AreaGzone4"];
                    break;
                case CoordinateType.SK63AreaGzone5:
                    BasicTypeString = ConfigurationManager.AppSettings["GK63AreaGzone5"];
                    break;
                case CoordinateType.SK63AreaGzone6:
                    BasicTypeString = ConfigurationManager.AppSettings["GK63AreaGzone6"];
                    break;
                case CoordinateType.SK63AreaGzone7:
                    BasicTypeString = ConfigurationManager.AppSettings["GK63AreaGzone7"];
                    break;
                case CoordinateType.SK63AreaGzone8:
                    BasicTypeString = ConfigurationManager.AppSettings["GK63AreaGzone8"];
                    break;
                case CoordinateType.SK63AreaGzone9:
                    BasicTypeString = ConfigurationManager.AppSettings["GK63AreaGzone9"];
                    break;
                case CoordinateType.SK63AreaIzone1:
                    BasicTypeString = ConfigurationManager.AppSettings["GK63AreaIzone1"];
                    break;
                case CoordinateType.SK63AreaIzone2:
                    BasicTypeString = ConfigurationManager.AppSettings["GK63AreaIzone2"];
                    break;
                case CoordinateType.SK63AreaIzone3:
                    BasicTypeString = ConfigurationManager.AppSettings["GK63AreaIzone3"];
                    break;
                case CoordinateType.SK63AreaIzone4:
                    BasicTypeString = ConfigurationManager.AppSettings["GK63AreaIzone4"];
                    break;
                case CoordinateType.SK63AreaJzone1:
                    BasicTypeString = ConfigurationManager.AppSettings["GK63AreaJzone1"];
                    break;
                case CoordinateType.SK63AreaJzone2:
                    BasicTypeString = ConfigurationManager.AppSettings["GK63AreaJzone2"];
                    break;
                case CoordinateType.SK63AreaJzone3:
                    BasicTypeString = ConfigurationManager.AppSettings["GK63AreaJzone3"];
                    break;
                case CoordinateType.SK63AreaJzone4:
                    BasicTypeString = ConfigurationManager.AppSettings["GK63AreaJzone4"];
                    break;
                case CoordinateType.SK63AreaJzone5:
                    BasicTypeString = ConfigurationManager.AppSettings["GK63AreaJzone5"];
                    break;
                case CoordinateType.SK63AreaLzone1:
                    BasicTypeString = ConfigurationManager.AppSettings["GK63AreaLzone1"];
                    break;
                case CoordinateType.SK63AreaLzone2:
                    BasicTypeString = ConfigurationManager.AppSettings["GK63AreaLzone2"];
                    break;
                case CoordinateType.SK63AreaLzone3:
                    BasicTypeString = ConfigurationManager.AppSettings["GK63AreaLzone3"];
                    break;
                case CoordinateType.SK63AreaLzone4:
                    BasicTypeString = ConfigurationManager.AppSettings["GK63AreaLzone4"];
                    break;
                case CoordinateType.SK63AreaLzone5:
                    BasicTypeString = ConfigurationManager.AppSettings["GK63AreaLzone5"];
                    break;
                case CoordinateType.SK63AreaLzone6:
                    BasicTypeString = ConfigurationManager.AppSettings["GK63AreaLzone6"];
                    break;
                case CoordinateType.SK63AreaMzone1:
                    BasicTypeString = ConfigurationManager.AppSettings["GK63AreaMzone1"];
                    break;
                case CoordinateType.SK63AreaMzone2:
                    BasicTypeString = ConfigurationManager.AppSettings["GK63AreaMzone2"];
                    break;
                case CoordinateType.SK63AreaMzone3:
                    BasicTypeString = ConfigurationManager.AppSettings["GK63AreaMzone3"];
                    break;
                case CoordinateType.SK63AreaMzone4:
                    BasicTypeString = ConfigurationManager.AppSettings["GK63AreaMzone4"];
                    break;
                case CoordinateType.SK63AreaPzone1:
                    BasicTypeString = ConfigurationManager.AppSettings["GK63AreaPzone1"];
                    break;
                case CoordinateType.SK63AreaPzone2:
                    BasicTypeString = ConfigurationManager.AppSettings["GK63AreaPzone2"];
                    break;
                case CoordinateType.SK63AreaPzone3:
                    BasicTypeString = ConfigurationManager.AppSettings["GK63AreaPzone3"];
                    break;
                case CoordinateType.SK63AreaPzone4:
                    BasicTypeString = ConfigurationManager.AppSettings["GK63AreaPzone4"];
                    break;
                case CoordinateType.SK63AreaQzone1:
                    BasicTypeString = ConfigurationManager.AppSettings["GK63AreaQzone1"];
                    break;
                case CoordinateType.SK63AreaQzone2:
                    BasicTypeString = ConfigurationManager.AppSettings["GK63AreaQzone2"];
                    break;
                case CoordinateType.SK63AreaQzone3:
                    BasicTypeString = ConfigurationManager.AppSettings["GK63AreaQzone3"];
                    break;
                case CoordinateType.SK63AreaQzone4:
                    BasicTypeString = ConfigurationManager.AppSettings["GK63AreaQzone4"];
                    break;
                case CoordinateType.SK63AreaQzone5:
                    BasicTypeString = ConfigurationManager.AppSettings["GK63AreaQzone5"];
                    break;
                case CoordinateType.SK63AreaRzone1:
                    BasicTypeString = ConfigurationManager.AppSettings["GK63AreaRzone1"];
                    break;
                case CoordinateType.SK63AreaRzone2:
                    BasicTypeString = ConfigurationManager.AppSettings["GK63AreaRzone2"];
                    break;
                case CoordinateType.SK63AreaRzone3:
                    BasicTypeString = ConfigurationManager.AppSettings["GK63AreaRzone3"];
                    break;
                case CoordinateType.SK63AreaSzone1:
                    BasicTypeString = ConfigurationManager.AppSettings["GK63AreaSzone1"];
                    break;
                case CoordinateType.SK63AreaSzone2:
                    BasicTypeString = ConfigurationManager.AppSettings["GK63AreaSzone2"];
                    break;
                case CoordinateType.SK63AreaSzone3:
                    BasicTypeString = ConfigurationManager.AppSettings["GK63AreaSzone3"];
                    break;
                case CoordinateType.SK63AreaSzone4:
                    BasicTypeString = ConfigurationManager.AppSettings["GK63AreaSzone4"];
                    break;
                case CoordinateType.SK63AreaSzone5:
                    BasicTypeString = ConfigurationManager.AppSettings["GK63AreaSzone5"];
                    break;
                case CoordinateType.SK63AreaSzone6:
                    BasicTypeString = ConfigurationManager.AppSettings["GK63AreaSzone6"];
                    break;
                case CoordinateType.SK63AreaSzone7:
                    BasicTypeString = ConfigurationManager.AppSettings["GK63AreaSzone7"];
                    break;
                case CoordinateType.SK63AreaSzone8:
                    BasicTypeString = ConfigurationManager.AppSettings["GK63AreaSzone8"];
                    break;
                case CoordinateType.SK63AreaSzone9:
                    BasicTypeString = ConfigurationManager.AppSettings["GK63AreaSzone9"];
                    break;
                case CoordinateType.SK63AreaSzone10:
                    BasicTypeString = ConfigurationManager.AppSettings["GK63AreaSzone10"];
                    break;
                case CoordinateType.SK63AreaSzone11:
                    BasicTypeString = ConfigurationManager.AppSettings["GK63AreaSzone11"];
                    break;
                case CoordinateType.SK63AreaSzone12:
                    BasicTypeString = ConfigurationManager.AppSettings["GK63AreaSzone12"];
                    break;
                case CoordinateType.SK63AreaSzone13:
                    BasicTypeString = ConfigurationManager.AppSettings["GK63AreaSzone13"];
                    break;
                case CoordinateType.SK63AreaTzone1:
                    BasicTypeString = ConfigurationManager.AppSettings["GK63AreaTzone1"];
                    break;
                case CoordinateType.SK63AreaTzone2:
                    BasicTypeString = ConfigurationManager.AppSettings["GK63AreaTzone2"];
                    break;
                case CoordinateType.SK63AreaTzone3:
                    BasicTypeString = ConfigurationManager.AppSettings["GK63AreaTzone3"];
                    break;
                case CoordinateType.SK63AreaTzone4:
                    BasicTypeString = ConfigurationManager.AppSettings["GK63AreaTzone4"];
                    break;
                case CoordinateType.SK63AreaVzone1:
                    BasicTypeString = ConfigurationManager.AppSettings["GK63AreaVzone1"];
                    break;
                case CoordinateType.SK63AreaVzone2:
                    BasicTypeString = ConfigurationManager.AppSettings["GK63AreaVzone2"];
                    break;
                case CoordinateType.SK63AreaVzone3:
                    BasicTypeString = ConfigurationManager.AppSettings["GK63AreaVzone3"];
                    break;
                case CoordinateType.SK63AreaVzone4:
                    BasicTypeString = ConfigurationManager.AppSettings["GK63AreaVzone4"];
                    break;
                case CoordinateType.SK63AreaVzone5:
                    BasicTypeString = ConfigurationManager.AppSettings["GK63AreaVzone5"];
                    break;
                case CoordinateType.SK63AreaVzone6:
                    BasicTypeString = ConfigurationManager.AppSettings["GK63AreaVzone6"];
                    break;
                case CoordinateType.SK63AreaW6Degreezone1:
                    BasicTypeString = ConfigurationManager.AppSettings["GK63AreaW6Degreezone1"];
                    break;
                case CoordinateType.SK63AreaW6Degreezone2:
                    BasicTypeString = ConfigurationManager.AppSettings["GK63AreaW6Degreezone2"];
                    break;
                case CoordinateType.SK63AreaW6Degreezone3:
                    BasicTypeString = ConfigurationManager.AppSettings["GK63AreaW6Degreezone3"];
                    break;
                case CoordinateType.SK63AreaW6Degreezone4:
                    BasicTypeString = ConfigurationManager.AppSettings["GK63AreaW6Degreezone4"];
                    break;
                case CoordinateType.SK63AreaW3Degreezone1:
                    BasicTypeString = ConfigurationManager.AppSettings["GK63AreaW3Degreezone1"];
                    break;
                case CoordinateType.SK63AreaW3Degreezone2:
                    BasicTypeString = ConfigurationManager.AppSettings["GK63AreaW3Degreezone2"];
                    break;
                case CoordinateType.SK63AreaW3Degreezone3:
                    BasicTypeString = ConfigurationManager.AppSettings["GK63AreaW3Degreezone3"];
                    break;
                case CoordinateType.SK63AreaW3Degreezone4:
                    BasicTypeString = ConfigurationManager.AppSettings["GK63AreaW3Degreezone4"];
                    break;
                case CoordinateType.SK63AreaW3Degreezone5:
                    BasicTypeString = ConfigurationManager.AppSettings["GK63AreaW3Degreezone5"];
                    break;
                case CoordinateType.SK63AreaW3Degreezone6:
                    BasicTypeString = ConfigurationManager.AppSettings["GK63AreaW3Degreezone6"];
                    break;
                case CoordinateType.SK63AreaW3Degreezone7:
                    BasicTypeString = ConfigurationManager.AppSettings["GK63AreaW3Degreezone7"];
                    break;
                case CoordinateType.SK63AreaW3Degreezone8:
                    BasicTypeString = ConfigurationManager.AppSettings["GK63AreaW3Degreezone8"];
                    break;
                case CoordinateType.SK63AreaXzone1:
                    BasicTypeString = ConfigurationManager.AppSettings["GK63AreaXzone1"];
                    break;
                case CoordinateType.SK63AreaXzone2:
                    BasicTypeString = ConfigurationManager.AppSettings["GK63AreaXzone2"];
                    break;
                case CoordinateType.SK63AreaXzone3:
                    BasicTypeString = ConfigurationManager.AppSettings["GK63AreaXzone3"];
                    break;
                case CoordinateType.SK63AreaXzone4:
                    BasicTypeString = ConfigurationManager.AppSettings["GK63AreaXzone4"];
                    break;
                case CoordinateType.SK63AreaXzone5:
                    BasicTypeString = ConfigurationManager.AppSettings["GK63AreaXzone5"];
                    break;
                case CoordinateType.SK63AreaXzone6:
                    BasicTypeString = ConfigurationManager.AppSettings["GK63AreaXzone6"];
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
