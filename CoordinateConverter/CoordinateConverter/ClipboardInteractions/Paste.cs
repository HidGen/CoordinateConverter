using CoordinateConverter.Model;
using System;
using System.Collections.Generic;
using System.Windows;

namespace CoordinateConverter.ClipboardInteractions
{
    public class Paste
    {
        public IList<CompleteRow> PasteDataFromExcel()
        {
            string str = Clipboard.GetText();
            string[] lines;
            var delimiters = new char[] { '\n' };
            var completeRows = new List<CompleteRow>();
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
                        rectCoord.X = Convert.ToDouble(values[1]);
                        rectCoord.Y = Convert.ToDouble(values[2]);                      
                        completeRows.Add(new CompleteRow { RectCoord = rectCoord, Description = values[0] });
                    }
                    catch
                    {
                        try
                        {
                            rectCoord.X = Convert.ToDouble(values[0]);
                            rectCoord.Y = Convert.ToDouble(values[1]);
                            completeRows.Add(new CompleteRow
                            {
                                RectCoord = rectCoord
                            }
                            );
                        }
                        catch
                        {
                            MessageBox.Show(
                           "Неверный формат строки в буфере обмена",
                           "Ошибка",
                           MessageBoxButton.OK,
                           MessageBoxImage.Error
                           );
                            completeRows.Clear();
                            break;
                        }                    
                    }
                }
            }
            return completeRows;
        }
    }
}
