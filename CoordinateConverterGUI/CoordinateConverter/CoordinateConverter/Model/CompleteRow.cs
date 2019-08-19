using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoordinateConverter.Model
{
    public class CompleteRow
    {

        public event EventHandler RectCoordPropertyChanged;
        public CompleteRow()
        {
            rectCoord = new RectCoord();
            GeoCoord = new GeoCoord();
            


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

        public GeoCoord GeoCoord { get; set; }

        public string Description { get; set; }
        public void RectCoordChanged(object sender, EventArgs e)
        {
            RectCoordPropertyChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
