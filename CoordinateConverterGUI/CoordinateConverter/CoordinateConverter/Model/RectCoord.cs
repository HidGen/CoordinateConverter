using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoordinateConverter.Model
{
    public class RectCoord
    {
        public event EventHandler PropertyChanged;

        private double x;
        public double X
        {
            get => x;
            set
            {
                x = value;
                OnPropertyChanged();
            }
        }
        private double y;
        public double Y
        {
            get => y;
            set
            {
                y = value;
                OnPropertyChanged();
            }
        }
        private double h;
        public double H
        {
            get => h;
            set
            {
                h = value;
                OnPropertyChanged();
            }
        }
        public void OnPropertyChanged()
        {
            //EventHandler handler = ThresholdReached;
            PropertyChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
