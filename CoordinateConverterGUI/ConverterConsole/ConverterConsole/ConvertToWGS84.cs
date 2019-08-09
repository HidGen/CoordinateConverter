using System;
using System.Configuration;
using DotSpatial.Projections;

namespace ConverterConsole
{
    class ConvertToWGS84
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
                    BasicTypeString =ConfigurationManager.AppSettings["GKzone2"];
                    //throw new Exception("Coordinate system isn't supported yet");
                    break;
                case CoordinateSystem.CS63:
                    throw new Exception("Coordinate system isn't supported yet");
                    BasicTypeString = @"";
                    break;
            }
        }

        public void Convert(CoordinateSystem basicCoordinateSystem, double[] x, double[] y, double[] z,double[] xy)
        {
            ChangeType(basicCoordinateSystem);
            ProjectionInfo src = ProjectionInfo.FromProj4String(BasicTypeString);
            ProjectionInfo trg = ProjectionInfo.FromProj4String("+proj=longlat +datum=WGS84 +no_defs");
            //xy = new double[2 * x.Length];
            int ixy = 0;
            for (int i = 0; i <= x.Length - 1; i++)
            {
                xy[ixy] = x[i];
                xy[ixy + 1] = y[i];
                z[i] = 0;
                ixy += 2;
            }
            Reproject.ReprojectPoints(xy, z, src, trg, 0, x.Length);    
            
        }
    }

}
