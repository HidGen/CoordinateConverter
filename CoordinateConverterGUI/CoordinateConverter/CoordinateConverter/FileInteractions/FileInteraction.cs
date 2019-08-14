using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        public List<CompleteRow> OpenFile()
        {
            List<RectCoord> rectCoords = new List<RectCoord>();
            OpenFileDialog dlg = new OpenFileDialog();
            List<CompleteRow> completeRows = new List<CompleteRow>();

            dlg.FileName = "Document"; // Default file name
            dlg.DefaultExt = ".xls"; // Default file extension
            dlg.Filter = "Excel documents (.xls;.xlsm;.xlsx)|*.xls;*.xlsm;*.xlsx|All files (*.*)|*.*"; // Filter files by extension
            dlg.Multiselect = true;
            // Show open file dialog box
            bool? result = dlg.ShowDialog();

            if (result == true)
            {
                // Open document
                foreach (string filename in dlg.FileNames)
                {
                    //FileName.Add(filename);
                    foreach (RectCoord rectCoord in Read(filename))
                        completeRows.Add(new CompleteRow {rectCoord=rectCoord });
                }
            }
            return completeRows;
        }
        private List<RectCoord> Read(string reference)
        {
            Excel.Application xlApp;
            Excel.Workbook xlWorkBook;
            Excel.Worksheet xlWorkSheet;
            object misValue = System.Reflection.Missing.Value;
            xlApp = new Excel.Application();          
            xlWorkBook = xlApp.Workbooks.Open(reference, 0, true, 5, "", "", true, Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);
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

        public void Save(List<GeoCoord> geoCoords)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            geoCoords.Add(new GeoCoord { Lat = 20, Lon = 30, H = 0 });
            geoCoords.Add(new GeoCoord { Lat = 30, Lon = 40, H = 0 });
            geoCoords.Add(new GeoCoord { Lat = 40, Lon = 50, H = 0 });
            saveFileDialog.FileName = "Document"; // Default file name
            saveFileDialog.DefaultExt = ".xml"; // Default file extension
            saveFileDialog.Filter = "Xml documents (.xml)|*.xml|All files (*.*)|*.*";
            if (saveFileDialog.ShowDialog() == true)
            {
                SaveToXml(saveFileDialog.FileName, geoCoords);
            }
        }
        private void SaveToXml(string path, List<GeoCoord> geoCoords)
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
                    RectCoord rectCoord = new RectCoord { X = (range.Cells[1, cCnt - 2] as Excel.Range).Value2, Y = (range.Cells[1, cCnt - 1] as Excel.Range).Value2, H = (range.Cells[1, cCnt] as Excel.Range).Value2 };
                    return rectCoord;
                }
            }
            return new RectCoord();
        }
    }
}
