using System;

namespace CoordinateConverter.Model
{
    public class RectCoord
    {
        public event EventHandler PropertyChanged;

        private double x;
        private double y;
        private double h;

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
            PropertyChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
