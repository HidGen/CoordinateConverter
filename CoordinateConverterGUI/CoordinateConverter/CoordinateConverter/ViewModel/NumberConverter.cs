using System;
using System.Globalization;
using System.Windows.Data;

namespace CoordinateConverter.ViewModel
{
    public class NumberConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var i = System.Convert.ToInt32(value);
            return i >= 0 ? i + 1 : (object)null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
