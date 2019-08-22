using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace CoordinateConverter.ViewModel
{
    public class NumberConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        { int i;
            try
            {
                i = System.Convert.ToInt32(value);
            }
            catch
            {
                throw new NotImplementedException();
            }
            if (i >= 0 )
            {
                return i+1;
            }
            else 
            { return null; }   
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
