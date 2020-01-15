using DevExpress.Mvvm;

namespace CoordinateConverter.Model
{
    public class GeoCoord : BindableBase
    {
        private double alt;

        public double Lat { get; set; }

        public double Lon { get; set; }

        public double Alt
        {
            get => alt;
            set
            {
                alt = value;
                RaisePropertiesChanged(nameof(Alt));
            }
        }
    }
}
