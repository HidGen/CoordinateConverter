using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConverterConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine("C#: Test coordinate conversion from ED50 to RD New");
            // double[] x = {1299124.3692459164};
            // double[] y = {420786.76325313374};
            // double[] z = new double[x.Length];
            // double[] xy = new double[x.Length * 2];
            // double[] xy1 = new double[x.Length * 2];
            // double[] x1 = { 2634488.63 };
            // double[] y1 = { 5787021.72};
            // for (int i = 0; i <= z.Length - 1; i++)
            // {
            //     Console.WriteLine("input geographic LCS46_1 p{0} = {1} {2}", i + 1, x[i], y[i]);
            // }
            // ConvertToWGS84 convertToWGS84= new ConvertToWGS84();
            // convertToWGS84.Convert(ConvertToWGS84.CoordinateSystem.LCS46_1,x,y,z,xy);
            // convertToWGS84.Convert(ConvertToWGS84.CoordinateSystem.CS42, x1, y1, z, xy1);
            //// convertToWGS84.Convert(ConvertToWGS84.CoordinateSystem.CS42,x,y)

            // //DotSpatial.Projections.ProjectionInfo src =
            // //DotSpatial.Projections.ProjectionInfo.FromProj4String(proj4_LCS46_1);
            // // DotSpatial.Projections.ProjectionInfo src1 =
            // //DotSpatial.Projections.ProjectionInfo.FromProj4String(proj4_LCS46_2);

            // //DotSpatial.Projections.ProjectionInfo trg1 =
            // //DotSpatial.Projections.ProjectionInfo.FromProj4String("+proj=longlat +ellps=WGS84 +datum=WGS84 +no_defs");
            // //DotSpatial.Projections.ProjectionInfo trgs = DotSpatial.Projections.ProjectionInfo.FromProj4String("+proj=longlat +datum=WGS84 +no_defs");
            // //DotSpatial.Projections.Reproject.ReprojectPoints(xy, z, src, trgs, 0, x.Length);
            // //DotSpatial.Projections.Reproject.ReprojectPoints(xy1, z1, src1, trg1, 0, x.Length);
            // //src.GeographicInfo.Datum.ToWGS84 = new double[] { 24, -123, -94, -0.02, 0.25, 0.13, 1.1 };
            // int ixy = 0;
            // int ixy1 = 0;
            // for (int i = 0; i <= x.Length - 1; i++)
            // {
            //     Console.WriteLine("output WGS84 New p{0} = {1} {2}", i + 1, xy[ixy], xy[ixy+1]);
            //     Console.WriteLine("output WGS84 New p{0} = {1} {2}", i + 1, xy1[ixy1], xy1[ixy1 + 1]);
            //     //Console.WriteLine(z[i]);
            //     ixy += 2;
            //     ixy1 += 2;
            // }
            var package = new ExcelPackage(new FileInfo(@"C:\Users\Лаврушов\Desktop\dd.xlsx"));

            ExcelWorksheet workSheet = package.Workbook.Worksheets[1];

            for (int i = workSheet.Dimension.Start.Row;
                     i <= workSheet.Dimension.End.Row;
                     i++)
            {
                int x = 0;
                for (int j = workSheet.Dimension.Start.Column;
                         j <= workSheet.Dimension.End.Column;
                         j++)
                {
                    object cellValue = workSheet.Cells[i, j].Value;
                    if (workSheet.Cells[i, j].Value is double)
                        x++;
                    if(x==3)
                        Console.WriteLine(cellValue);
                }
            }
                    Console.Write("Press any key to continue . . . ");
            Console.ReadKey(true);
        }
    }
}
