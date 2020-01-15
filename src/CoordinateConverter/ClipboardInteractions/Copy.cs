using CoordinateConverter.Model;
using System.Collections.Generic;
using System.Text;

namespace CoordinateConverter.ClipboardInteractions
{
    public class Copy
    {
        public void CopyFromTable(IList<CompleteRow> completeRows)
        {
            StringBuilder str = new StringBuilder();
            foreach (var completeRow in completeRows)
            {
                str.AppendLine(
                    completeRow.Description + '\t' +
                    completeRow.RectCoord.X.ToString() + '\t' +
                    completeRow.RectCoord.Y.ToString() + '\t' +                     
                    completeRow.GeoCoord.Lat.ToString() + '\t' +
                    completeRow.GeoCoord.Lon.ToString());
            }           
            var dataObject = new System.Windows.DataObject();
            dataObject.SetText(str.ToString());
            System.Windows.Clipboard.SetDataObject(dataObject, true);
        }
    }
}
