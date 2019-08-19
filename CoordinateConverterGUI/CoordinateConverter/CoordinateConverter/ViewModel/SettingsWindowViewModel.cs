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

        public bool clearCheck;

        public bool ClearCheck {

            get { return clearCheck; }
            set
            {
                clearCheck = value;
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

        private void OnEditEnded(CoordinateType coordinateType, bool clearCheck)
        {
            Properties.Settings.Default.ClearCheck = clearCheck;
            Properties.Settings.Default.Save();
            EditEnded?.Invoke(this, new SettingsWindowArgs(coordinateType));
        }

        private ICommand returnCommand;

        public SettingsWindowViewModel(CoordinateType selectedCoordinateEnumType)
        {
            this.clearCheck = Properties.Settings.Default.ClearCheck;
            this.SelectedCoordinateEnumType = selectedCoordinateEnumType;
        }

        public ICommand ReturnCommand
        {
            get
            {
                return returnCommand ??
                  (returnCommand = new DelegateCommand(() =>
                  {
                      
                     
                      OnEditEnded(selectedCoordinateEnumType, ClearCheck);

                  }));
            }
        }

        public class SettingsWindowArgs : EventArgs
        {
            public SettingsWindowArgs(CoordinateType selectedType)
            {
                SelectedType = selectedType;
             }

            public CoordinateType SelectedType { get; }
            public bool ClearCheck { get; }
        }

    }
}
