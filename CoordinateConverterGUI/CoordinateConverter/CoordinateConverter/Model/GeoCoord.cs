using DevExpress.Mvvm;

namespace CoordinateConverter.Model
{
    public class GeoCoord : BindableBase
    {
        public double Lat { get; set; }
        public double Lon { get; set; }
        private double h;
        public double H { get => h; set
            {
                h = value;
                RaisePropertiesChanged(nameof(H));
            } }
    }
}
