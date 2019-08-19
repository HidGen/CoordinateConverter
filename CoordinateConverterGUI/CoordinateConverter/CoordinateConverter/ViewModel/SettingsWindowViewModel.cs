using DevExpress.Mvvm;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CoordinateConverter.ViewModel
{
    public class SettingsWindowViewModel
    {
        public event EventHandler<SettingsWindowArgs> EditEnded;

        public bool rangeCheck;

        public bool RangeCheck {

            get { return rangeCheck; }
            set
            {
                rangeCheck = value;
                // OnPropertyChanged("SelectedCoordinateEnumType");
            }
        }

        public IEnumerable<CoordinateType> CoordinateEnumTypeValues
        {
            get
            {
                return Enum.GetValues(typeof(CoordinateType))
                    .Cast<CoordinateType>();
            }
        }

        public CoordinateType selectedCoordinateEnumType;
        public CoordinateType SelectedCoordinateEnumType
        {
            get { return selectedCoordinateEnumType; }
            set
            {
                selectedCoordinateEnumType = value;
                // OnPropertyChanged("SelectedCoordinateEnumType");
            }
        }

        private void OnEditEnded(CoordinateType coordinateType, bool rangeCheck)
        {
            EditEnded?.Invoke(this, new SettingsWindowArgs(coordinateType, rangeCheck));
        }

        private ICommand returnCommand;

        public SettingsWindowViewModel(bool rangeCheck, CoordinateType selectedCoordinateEnumType)
        {
            this.rangeCheck = rangeCheck;
            this.SelectedCoordinateEnumType = selectedCoordinateEnumType;
        }

        public ICommand ReturnCommand
        {
            get
            {
                return returnCommand ??
                  (returnCommand = new DelegateCommand(() =>
                  {
                      
                     
                      OnEditEnded(selectedCoordinateEnumType, RangeCheck);

                  }));
            }
        }

        public class SettingsWindowArgs : EventArgs
        {
            public SettingsWindowArgs(CoordinateType selectedType, bool rangeCheck)
            {
                SelectedType = selectedType;
                RangeCheck = rangeCheck;
            }

            public CoordinateType SelectedType { get; }
            public bool RangeCheck { get; }
        }

    }
}
