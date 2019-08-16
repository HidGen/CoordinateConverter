using CoordinateConverter.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CoordinateConverter.ClipboardInteractions
{
    public class Paste
    {
        public List<RectCoord> PasteDataFromExcel()
        {
            string str = Clipboard.GetText();
            string[] lines;
            var delimiters = new char[] { '\n' };
            var rectCoords = new List<RectCoord>();
            str = str.Replace('\r', ' ');
            lines = str.Split(delimiters, StringSplitOptions.None);
            foreach (var line in lines)
            {
                if (line != string.Empty)
                {
                    char[] delimiter = new char[] { '\t' };
                    var values = line.Split(delimiter, StringSplitOptions.None);
                    var rectCoord = new RectCoord();
                    try
                    {
                        rectCoord.X = Convert.ToDouble(values[0]);
                        rectCoord.Y = Convert.ToDouble(values[1]);
                        rectCoord.H = Convert.ToDouble(values[2]);
                        rectCoords.Add(rectCoord);
                    }
                    catch 
                    {                        
                        MessageBox.Show("Неверный формат строки в буфере обмена");
                        rectCoords.Clear();
                        break;
                    }
                }
            }
            return rectCoords;
        }
    }
}
