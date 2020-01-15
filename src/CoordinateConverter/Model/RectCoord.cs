using System;

namespace CoordinateConverter.Model
{
    public class RectCoord
    {
        public event EventHandler PropertyChanged;

        private double x;
        private double y;        

        public double X
        {
            get => x;
            set
            {
                x = value;
                OnPropertyChanged();
            }
        }
       
        public double Y
        {
            get => y;
            set
            {
                y = value;
                OnPropertyChanged();
            }
        }       
       

        public void OnPropertyChanged()
        {
            PropertyChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
