using System;
using System.Globalization;
using System.Windows.Data;

namespace CoordinateConverter.ViewModel
{
    public class CoordTypeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var point = System.Convert.ToDouble(value);
            if (double.IsNaN(point))
                return "NaN";


            var param  = System.Convert.ToString(parameter);
            if (string.IsNullOrEmpty(param))
                return value;

            switch (param)
            {
                case "lat":
                    {
                        var prtef = point > 0 ? " с.ш " : " ю.ш";
                        return ConvertLat(point) + prtef;
                    }
                        
                case "lon":
                    {
                        var prtef = point > 0 ? " в.д" : " з.д";
                        return ConvertLon(point) + prtef;
                    }
                        
                default:
                    return value;
            }
        }

        private string ConvertLat(double value)
        {
            switch (Properties.Settings.Default.CoordView)
            {
                case CoordViewType.Decimal:
                    return ToDegree(value);
                case CoordViewType.MinSec:
                    return LatToDegMinSec(value);
                default:
                    return value.ToString(CultureInfo.InvariantCulture);
            }
        }

        private string ConvertLon(double value)
        {
            switch (Properties.Settings.Default.CoordView)
            {
                case CoordViewType.Decimal:
                    return ToDegree(value);
                case CoordViewType.MinSec:
                    return LonToDegMinSec(value);
                default:
                    return value.ToString(CultureInfo.InvariantCulture);
            }
        }
        
        private string LatToDegMinSec(double value)
        {
            var abs = Math.Abs(value);
            var deg = (int)Math.Truncate(abs);
            var minSec = (abs - deg) * 60.0;
            var min = (int)Math.Truncate(minSec);
            var sec = (minSec - min) * 60.0;

            if (sec >= 60.0) { min += 1; sec = 0.0; }
            if (min >= 60) { deg += 1; min = 0; }

            return $"{deg:00}°{min:00}′{sec:F1}″";
        }

        private string LonToDegMinSec(double value)
        {
            var abs = Math.Abs(value);
            var deg = (int)Math.Truncate(abs);
            var minSec = (abs - deg) * 60.0;
            var min = (int)Math.Truncate(minSec);
            var sec = (minSec - min) * 60.0;

            if (sec >= 60.0) { min += 1; sec = 0.0; }
            if (min >= 60) { deg += 1; min = 0; }

            return $"{deg:000}°{min:00}′{sec:F1}″";
        }
        
        public string ToDegree(double point)
        {
            return Math.Abs(Math.Round(point, 10)).ToString(CultureInfo.InvariantCulture);
        }
                       
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
