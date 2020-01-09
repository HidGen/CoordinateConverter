using System;
using System.Globalization;
using System.Windows.Data;

namespace CoordinateConverter.ViewModel
{
    public class CheckConverter : IValueConverter 
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (Properties.Settings.Default.ClearRule == true)
                return "Очищать";
            else
                return "Не очищать";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

