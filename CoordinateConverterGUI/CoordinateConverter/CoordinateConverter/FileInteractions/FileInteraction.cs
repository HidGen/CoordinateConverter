using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using CoordinateConverter.Model;
using Microsoft.Win32;
using Excel = Microsoft.Office.Interop.Excel;

namespace CoordinateConverter.FileInteractions
{
    class FileInteraction : IExcelFileOpen, IXmlFileSave
    {
        public List<RectCoord> Read(string path)
        {
            Excel.Application xlApp;
            Excel.Workbook xlWorkBook;
            Excel.Worksheet xlWorkSheet;
            object misValue = System.Reflection.Missing.Value;
            xlApp = new Excel.Application();
            xlWorkBook = xlApp.Workbooks.Open(path, 0, true, 5, "", "", true, Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);
            xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);
            var range = xlWorkSheet.UsedRange;
            int rw = range.Rows.Count;
            int cl = range.Columns.Count;
            List<RectCoord> rectCoords = new List<RectCoord>();

            for (int rCnt = 1; rCnt <= rw; rCnt++)
            {
                if (ValidateRow(range.Rows[rCnt], cl))
                    rectCoords.Add(AddRow(range.Rows[rCnt], cl));
            }
            return rectCoords;
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
                    writer.WriteElementString("lat", geoCoord.lat.ToString());
                    writer.WriteElementString("long", geoCoord.lon.ToString());
                    writer.WriteEndElement();
                }

                writer.WriteEndElement();
                writer.WriteEndDocument();
            }
        }
        private bool ValidateRow(Excel.Range range, int clm)
        {
            int x = 0;
            for (int cCnt = 1; cCnt <= clm; cCnt++)
            {
                if ((range.Cells[1, cCnt] as Excel.Range).Value2 is double)
                    x++;
                else
                    x = 0;
                if (x == 3)
                {
                    return true;
                }
            }
            return false;
        }
        private RectCoord AddRow(Excel.Range range, int clm)
        {
            int x = 0;
            for (int cCnt = 1; cCnt <= clm; cCnt++)
            {
                if ((range.Cells[1, cCnt] as Excel.Range).Value2 is double)
                    x++;
                else
                    x = 0;
                if (x == 3)
                {
                    RectCoord rectCoord = new RectCoord { x = (range.Cells[1, cCnt - 2] as Excel.Range).Value2, y = (range.Cells[1, cCnt - 1] as Excel.Range).Value2, h = (range.Cells[1, cCnt] as Excel.Range).Value2 };
                    return rectCoord;
                }
            }
            return new RectCoord();
        }
    }
}
