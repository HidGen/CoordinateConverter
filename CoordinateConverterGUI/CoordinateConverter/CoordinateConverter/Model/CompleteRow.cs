using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoordinateConverter.Model
{
    public class CompleteRow
    {
        public CompleteRow()
        {
            //rectCoord = new RectCoord();
            //geoCoord = new GeoCoord();
            //if(rectCoord!= null)
           
        }
        private RectCoord rectCoord;
        public RectCoord RectCoord { get => rectCoord;
            set { rectCoord = value;
                rectCoord.PropertyChanged -= CoordPropertyChanged;
                rectCoord.PropertyChanged += CoordPropertyChanged;
                CoordPropertyChanged(this, EventArgs.Empty);
            } }

        public GeoCoord geoCoord { get; set; }

        public string Description { get; set; }
        public void CoordPropertyChanged(object sender, EventArgs e)
        {
            var coordConverter = new CoordConverter();
            geoCoord = coordConverter.Convert(CoordConverter.CoordinateSystem.LCS46_1,rectCoord);
            //rectCoord.PropertyChanged.Invoke(this, EventArgs.Empty);
        }
    }
}
