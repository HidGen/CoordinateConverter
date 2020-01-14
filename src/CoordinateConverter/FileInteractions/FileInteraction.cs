using CoordinateConverter.Model;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Xml;

namespace CoordinateConverter.FileInteractions
{
    class FileInteraction : IExcelFileOpen, IXmlFileSave
    {
        public IList<CompleteRow> Read(string path)
        {
            var completeRows = new List<CompleteRow>();
            using (var package = new ExcelPackage(new FileInfo(path)))
            {
                ExcelWorksheet workSheet = package.Workbook.Worksheets[1];
                int? xIndex = null;
                int? descriptionIndex = null;
                int spaceCount = 0;

                for (int i = workSheet.Dimension.Start.Row; i <= workSheet.Dimension.End.Row; i++)
                {
                    if (!xIndex.HasValue)
                    {
                        for (int j = workSheet.Dimension.Start.Column; j <= workSheet.Dimension.End.Column; j++)
                        {                         
                            if (workSheet.Cells[i, j].Value is string || workSheet.Cells[i, j].Value is double)
                                try
                                {
                                    if (workSheet.Cells[i, j + 1].Value is string || workSheet.Cells[i, j + 1].Value is double)
                                    {
                                        double x;
                                        double y;
                                        string description;

                                        if (workSheet.Cells[i, j + 2].Value is string || workSheet.Cells[i, j + 2].Value is double)
                                        {
                                            xIndex = j + 1;
                                            descriptionIndex = j;                                           
                                            x = Convert.ToDouble(workSheet.Cells[i, xIndex.Value].Value);
                                            y = Convert.ToDouble(workSheet.Cells[i, xIndex.Value + 1].Value);
                                            description = workSheet.Cells[i, descriptionIndex.Value].Value.ToString();
                                            completeRows.Add(new CompleteRow { RectCoord = new RectCoord { X = x, Y = y, H = 0 }, Description = description });
                                            break;
                                        }
                                        else
                                        {
                                            xIndex = j;                                          
                                            x = Convert.ToDouble(workSheet.Cells[i, xIndex.Value].Value);
                                            y = Convert.ToDouble(workSheet.Cells[i, xIndex.Value + 1].Value);
                                            description = string.Empty;
                                            completeRows.Add(new CompleteRow { RectCoord = new RectCoord { X = x, Y = y, H = 0 }, Description = description });
                                            break;
                                        }
                                    }
                                }
                                catch {}
                        }
                    }
                    else
                    {
                        double x;
                        double y;
                        string description = string.Empty;
                        try
                        {
                            if (spaceCount >= 2)
                                break;
                            if (workSheet.Cells[i, xIndex.Value].Value == null || workSheet.Cells[i, xIndex.Value + 1].Value == null)
                            {
                                spaceCount += 1;
                                continue;
                            }
                            x = Convert.ToDouble(workSheet.Cells[i, xIndex.Value].Value);
                            y = Convert.ToDouble(workSheet.Cells[i, xIndex.Value + 1].Value);
                            if(descriptionIndex.HasValue)
                            description = workSheet.Cells[i, descriptionIndex.Value].Value.ToString();
                            completeRows.Add(new CompleteRow { RectCoord = new RectCoord { X = x, Y = y, H = 0 }, Description = description });
                            spaceCount = 0;
                        }
                        catch { }
                    }
                }
            }
            return completeRows;
        }

        public Task<IList<CompleteRow>> ReadAsync(string path)
        {
            return Task.Run(() =>
            {
                return Read(path);
            });
        }
        public IList<CompleteRow> ReadRange(string path, string first, string last)
        {
            var completeRows = new List<CompleteRow>();
            try
            {
                using (var package = new ExcelPackage(new FileInfo(path)))
                {
                    ExcelWorksheet workSheet = package.Workbook.Worksheets[1];

                    var data = workSheet.Cells[string.Format("{0}:{1}", first, last)].Value as object[,];
                    var clns = data.GetLength(1);
                    var rws = data.GetLength(0);
                    int? xIndex = null;
                    int? descriptionIndex = null;
                    int spaceCount = 0;

                    for (int i = 0; i <= rws - 1; i++)
                    {
                        if (!xIndex.HasValue)
                        {
                            for (int j = 0; j <= clns - 1; j++)
                            {
                                if (data[i, j] is string || data[i, j] is double)
                                    try
                                    {
                                        if (data[i, j + 1] is string || data[i, j + 1] is double)
                                        {
                                            double x;
                                            double y;
                                            string description;

                                            if (j + 2 <= clns - 1 && (data[i, j + 2] is string || data[i, j + 2] is double))
                                            {
                                                xIndex = j + 1;
                                                descriptionIndex = j;                                               
                                                x = Convert.ToDouble(data[i, xIndex.Value]);
                                                y = Convert.ToDouble(data[i, xIndex.Value + 1]);
                                                description = data[i, descriptionIndex.Value].ToString();
                                                completeRows.Add(new CompleteRow { RectCoord = new RectCoord { X = x, Y = y, H = 0 }, Description = description });
                                                break;
                                            }
                                            else
                                            {
                                                xIndex = j;                                              
                                                x = Convert.ToDouble(data[i, xIndex.Value]);
                                                y = Convert.ToDouble(data[i, xIndex.Value + 1]);
                                                description = string.Empty;
                                                completeRows.Add(new CompleteRow { RectCoord = new RectCoord { X = x, Y = y, H = 0 }, Description = description });
                                                break;
                                            }
                                        }
                                    }
                                    catch { }
                            }
                        }                        
                        else
                        {
                            double x;
                            double y;
                            string description = string.Empty;
                            try
                            {
                                if (spaceCount >= 2)
                                    break;
                                if (data[i, xIndex.Value] == null || data[i, xIndex.Value + 1] == null)
                                {
                                    spaceCount += 1;
                                    continue;
                                }
                                x = Convert.ToDouble(data[i, xIndex.Value]);
                                y = Convert.ToDouble(data[i, xIndex.Value + 1]);
                                if (descriptionIndex.HasValue)
                                    description = data[i, descriptionIndex.Value].ToString();
                                completeRows.Add(new CompleteRow { RectCoord = new RectCoord { X = x, Y = y, H = 0 }, Description = description });
                                spaceCount = 0;
                            }
                            catch { }
                        }
                    }
                }
                if (completeRows.Count == 0)
                {
                    MessageBox.Show(
                        "В данном диапазоне не найдено ни одной записи!",
                        "Внимание!",
                        MessageBoxButton.OK,
                        MessageBoxImage.Exclamation
                        );
                }
                return completeRows;
            }
            catch
            {

                MessageBox.Show(
                        "Неверный диапазон данных или имя файла.",
                        "Ошибка",
                        MessageBoxButton.OK,
                        MessageBoxImage.Error
                        );

                return new List<CompleteRow>();
            }

        }
        public Task<IList<CompleteRow>> ReadRangeAsync(string path, string first, string last)
        {
            return Task.Run(() =>
            {
                return ReadRange(path, first, last);
            });
        }
        public void SaveToXml(string path, List<GeoCoord> geoCoords)
        {
            XmlWriterSettings settings = new XmlWriterSettings
            {
                Indent = true,
                OmitXmlDeclaration = true,
                NewLineOnAttributes = true
            };
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
                    writer.WriteElementString("ele", "0");
                    writer.WriteEndElement();
                }
                writer.WriteEndElement();
                writer.WriteEndDocument();
            }
        }
    }
}
