using DevExpress.Mvvm;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CoordinateConverter.Model
{
    public class CompleteRow: BindableBase
    {

        public event EventHandler RectCoordPropertyChanged;
        public CompleteRow()
        {
            rectCoord = new RectCoord();
            geoCoord = new GeoCoord();
            rectCoord.PropertyChanged += RectCoordChanged;

        }
        private RectCoord rectCoord;
        public RectCoord RectCoord
        {
            get => rectCoord;
            set
            {
                if(rectCoord!=null)
                rectCoord.PropertyChanged -= RectCoordChanged;
                rectCoord = value;                
                rectCoord.PropertyChanged += RectCoordChanged;
            }
        }
        private GeoCoord geoCoord;
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

        internal void GeoCoordChanged()
        {
            RaisePropertyChanged(nameof(geoCoord));
        }
    }
}
