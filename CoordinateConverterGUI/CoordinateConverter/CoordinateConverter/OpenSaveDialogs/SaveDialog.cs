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
    class SaveDialog
    {
        public void Save(List<GeoCoord> geoCoords)
        {
            var fileInteraction = new FileInteraction();
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.FileName = "Document"; // Default file name
            saveFileDialog.DefaultExt = ".xml"; // Default file extension
            saveFileDialog.Filter = "Xml documents (.xml)|*.xml|All files (*.*)|*.*";
            if (saveFileDialog.ShowDialog() == true)
            {
                fileInteraction.SaveToXml(saveFileDialog.FileName, geoCoords);
            }
        }
    }
}
