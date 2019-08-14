using CoordinateConverter.FileInteractions;
using CoordinateConverter.Model;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CoordinateConverter.OpenSaveDialogs
{
    public class OpenDialog
    {
        public List<CompleteRow> OpenFile()
        {
            var fileInteraction = new FileInteraction();
            OpenFileDialog dlg = new OpenFileDialog();
            List<CompleteRow> completeRows = new List<CompleteRow>();

            dlg.FileName = "Document"; // Default file name
            dlg.DefaultExt = ".xls"; // Default file extension
            dlg.Filter = "Excel documents (.xls;.xlsm;.xlsx)|*.xls;*.xlsm;*.xlsx|All files (*.*)|*.*"; // Filter files by extension
            dlg.Multiselect = true;
            //var coordConverter = new CoordConverter();
            //coordConverter.ChangeType(CoordConverter.CoordinateSystem.LCS46_1);
            // Show open file dialog box
            bool? result = dlg.ShowDialog();

            if (result == true)
            {
                // Open document
                foreach (string filename in dlg.FileNames)
                {
                    //FileName.Add(filename);
                    foreach (RectCoord rectCoord in fileInteraction.Read(filename))
                        completeRows.Add(new CompleteRow { RectCoord = rectCoord, Description=filename });
                }
            }
            return completeRows;
        }
    }
}
