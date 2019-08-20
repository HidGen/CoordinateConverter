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
                    for (int j = workSheet.Dimension.Start.Column;
                         j <= workSheet.Dimension.End.Column;
                         j++)
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
                    for (int i = 0;
                         i <= rws - 1;
                         i++)
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
                return rectCoords;
            }
            catch
            {
                MessageBox.Show("Неверный диапазон данных или имя файла.");
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
                writer.WriteStartDocument();
                writer.WriteStartElement("GeoCoords");

                foreach (GeoCoord geoCoord in geoCoords)
                {
                    writer.WriteStartElement("GeoCoord");
                    writer.WriteElementString("lat", geoCoord.Lat.ToString());
                    writer.WriteElementString("long", geoCoord.Lon.ToString());
                    writer.WriteEndElement();
                }

                writer.WriteEndElement();
                writer.WriteEndDocument();
            }
        }

        //private bool ValidateRow(Excel.Range range, int clm)
        //{
        //    int x = 0;
        //    for (int cCnt = 1; cCnt <= clm; cCnt++)
        //    {
        //        if ((range.Cells[1, cCnt] as Excel.Range).Value2 is double)
        //            x++;
        //        else
        //            x = 0;
        //        if (x == 3)
        //        {
        //            return true;
        //        }
        //    }
        //    return false;
        //}

        //private RectCoord ReadRow(Excel.Range range, int clm)
        //{
        //    int x = 0;
        //    for (int cCnt = 1; cCnt <= clm; cCnt++)
        //    {
        //        if ((range.Cells[1, cCnt] as Excel.Range).Value2 is double)
        //            x++;
        //        else
        //            x = 0;
        //        if (x == 3)
        //        {
        //            RectCoord rectCoord = new RectCoord { X = (range.Cells[1, cCnt - 2] as Excel.Range).Value2, Y = (range.Cells[1, cCnt - 1] as Excel.Range).Value2, H = (range.Cells[1, cCnt] as Excel.Range).Value2 };
        //            return rectCoord;
        //        }
        //    }
        //    throw new Exception("Строка в таблице имеет неверный формат");
        //}
    }
}
