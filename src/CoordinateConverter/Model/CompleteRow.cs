using DevExpress.Mvvm;
using System;

namespace CoordinateConverter.Model
{
    public class CompleteRow: BindableBase
    {
        private RectCoord rectCoord;
        private GeoCoord geoCoord;

        public event EventHandler RectCoordPropertyChanged;

        public CompleteRow()
        {
            rectCoord = new RectCoord();
            geoCoord = new GeoCoord();
            rectCoord.PropertyChanged += RectCoordChanged;
        }
        
        public RectCoord RectCoord
        {
            get => rectCoord;
            set
            {
                if(rectCoord != null)
                rectCoord.PropertyChanged -= RectCoordChanged;
                rectCoord = value;
                if(value != null)
                rectCoord.PropertyChanged += RectCoordChanged;
            }
        }
        
        public GeoCoord GeoCoord
        {
            get => geoCoord;
            set
            {
                geoCoord = value;
                RaisePropertyChanged(nameof(geoCoord));
            }
        }

        public string Description { get; set; }

        public void RectCoordChanged(object sender, EventArgs e)
        {
            RectCoordPropertyChanged?.Invoke(this, EventArgs.Empty);
        }

        public void GeoCoordChanged()
        {
            RaisePropertyChanged(nameof(geoCoord));
        }
    }
}
