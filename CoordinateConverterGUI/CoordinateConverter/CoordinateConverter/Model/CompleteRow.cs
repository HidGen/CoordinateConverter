using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CoordinateConverter.Model
{
    public class CompleteRow:INotifyPropertyChanged
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
              //  RectCoordChanged(this, EventArgs.Empty);
            }
        }
        private GeoCoord geoCoord;
        public GeoCoord GeoCoord { get => geoCoord;
            set { geoCoord = value;
                NotifyPropertyChanged();
            } }

        public string Description { get; set; }
        public void RectCoordChanged(object sender, EventArgs e)
        {
            RectCoordPropertyChanged?.Invoke(this, EventArgs.Empty);
        }
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
