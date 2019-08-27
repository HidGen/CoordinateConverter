using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml;
using CoordinateConverter.Model;
using Microsoft.Win32;
using OfficeOpenXml;

namespace CoordinateConverter.FileInteractions
{
    class FileInteraction : IExcelFileOpen, IXmlFileSave
    {
        public List<RectCoord> Read(string path)
        {
            var rectCoords = new List<RectCoord>();
            using (var package = new ExcelPackage(new FileInfo(path)))
            {
                ExcelWorksheet workSheet = package.Workbook.Worksheets[1];

                for (int i = workSheet.Dimension.Start.Row;
                     i <= workSheet.Dimension.End.Row;
                     i++)
                {
                    int x = 0;
                    for (int j = workSheet.Dimension.Start.Column; j <= workSheet.Dimension.End.Column; j++)
                    {
                        if (workSheet.Cells[i, j].Value is double)                        
                            x++;                        
                        else                        
                            x = 0;
                        
                        if (x == 3)                        
                            rectCoords.Add(new RectCoord { X = (double)workSheet.Cells[i, j - 2].Value, Y = (double)workSheet.Cells[i, j - 1].Value, H = (double)workSheet.Cells[i, j].Value });
                        
                    }
                }
            }
            return rectCoords;
        }

        public Task<List<RectCoord>> ReadAsync(string path)
        {
            return Task<List<RectCoord>>.Run(() =>
            {
                return Read(path);
            });
        }
        public List<RectCoord> ReadRange(string path, string first, string last)
        {
            var rectCoords = new List<RectCoord>();
            try
            {
                using (var package = new ExcelPackage(new FileInfo(path)))
                {
                    ExcelWorksheet workSheet = package.Workbook.Worksheets[1];

                    var data = workSheet.Cells[string.Format("{0}:{1}", first, last)].Value as object[,];
                    var clns = data.GetLength(1);
                    var rws = data.GetLength(0);
                    for (int i = 0; i <= rws - 1; i++)
                    {
                        int x = 0;
                        for (int j = 0;
                             j <= clns - 1;
                             j++)
                        {
                            if (data[i, j] is double)                            
                                x++;                            
                            else                            
                                x = 0;
                            
                            if (x == 3)                            
                                rectCoords.Add(new RectCoord { X = (double)data[i, j - 2], Y = (double)data[i, j - 1], H = (double)data[i, j] });
                            
                        }
                    }
                }
                if (rectCoords.Count == 0)
                {
                    MessageBox.Show(
                        "В данном диапазоне не найдено ни одной записи!",
                        "Внимание!",
                        MessageBoxButton.OK,
                        MessageBoxImage.Exclamation
                        );
                }
                return rectCoords;
            }
            catch
            {
                
                MessageBox.Show(
                        "Неверный диапазон данных или имя файла.",
                        "Ошибка",
                        MessageBoxButton.OK,
                        MessageBoxImage.Error
                        );

                return new List<RectCoord>();
            }

        }
        public Task<List<RectCoord>> ReadRangeAsync(string path, string first, string last)
        {
            return Task<List<RectCoord>>.Run(() =>
            {
                return ReadRange(path, first, last);
            });
        }
        public void SaveToXml(string path, List<GeoCoord> geoCoords)
        {
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            settings.OmitXmlDeclaration = true;
            settings.NewLineOnAttributes = true;
            using (XmlWriter writer = XmlWriter.Create(path, settings))
            {

                writer.WriteStartDocument(false);
                writer.WriteStartElement("gpx", "http://www.topografix.com/GPX/1/1");
                writer.WriteAttributeString("xmlns", "http://www.topografix.com/GPX/1/1");
                writer.WriteAttributeString("xmlns", "gpxx", null, "http://www.garmin.com/xmlschemas/GpxExtensions/v3");
                writer.WriteAttributeString("xmlns", "wptxl", null, "http://www.garmin.com/xmlschemas/WaypointExtension/v1");
                writer.WriteAttributeString("xmlns", "gpxtpx", null, "http://www.garmin.com/xmlschemas/TrackPointExtension/v1");
                writer.WriteAttributeString("creator", "Oregon 650");
                writer.WriteAttributeString("version", "1.1");
                writer.WriteAttributeString("xmlns", "xsi", null, "http://www.w3.org/2001/XMLSchema-instance");
                writer.WriteAttributeString("xsi", "schemaLocation", null, "http://www.topografix.com/GPX/1/1 http://www.topografix.com/GPX/1/1/gpx.xsd http://www.garmin.com/xmlschemas/GpxExtensions/v3 http://www8.garmin.com/xmlschemas/GpxExtensionsv3.xsd http://www.garmin.com/xmlschemas/TrackStatsExtension/v1 http://www8.garmin.com/xmlschemas/TrackStatsExtension.xsd http://www.garmin.com/xmlschemas/WaypointExtension/v1 http://www8.garmin.com/xmlschemas/WaypointExtensionv1.xsd http://www.garmin.com/xmlschemas/TrackPointExtension/v1 http://www.garmin.com/xmlschemas/TrackPointExtensionv1.xsd");
                writer.WriteStartElement("metadata");
                writer.WriteStartElement("link");
                writer.WriteAttributeString("href", "http://www.garmin.com");
                writer.WriteElementString("text", "Garmin International");
                writer.WriteEndElement();
                writer.WriteElementString("time", DateTime.UtcNow.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss'Z'"));
                writer.WriteEndElement();
                foreach (GeoCoord geoCoord in geoCoords)
                {
                    writer.WriteStartElement("wpt");
                    writer.WriteAttributeString("lat", geoCoord.Lat.ToString());
                    writer.WriteAttributeString("long", geoCoord.Lon.ToString());
                    writer.WriteElementString("ele", geoCoord.H.ToString());
                    writer.WriteEndElement();
                }
                writer.WriteEndElement();
                writer.WriteEndDocument();

            }
        }
    }
}
