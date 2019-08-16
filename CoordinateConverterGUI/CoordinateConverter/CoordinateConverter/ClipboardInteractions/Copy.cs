using CoordinateConverter.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoordinateConverter.ClipboardInteractions
{
    public class Copy
    {
        public void CopyFromTable(IList<CompleteRow> completeRows)
        {
            string str = string.Empty;
            foreach (var completeRow in completeRows)
            {
                str += completeRow.RectCoord.X.ToString() + '\t' + completeRow.RectCoord.Y.ToString() + '\t' + completeRow.RectCoord.H.ToString()
                    + '\t' + completeRow.GeoCoord.Lat.ToString() + '\t' + completeRow.GeoCoord.Lon.ToString() + '\t' + completeRow.GeoCoord.H.ToString() + '\t'
                    + completeRow.Description + '\r' + '\n';
            }
            var dataObject = new System.Windows.DataObject();
            dataObject.SetText(str);
            System.Windows.Clipboard.SetDataObject(dataObject, true);
        }
    }
}
