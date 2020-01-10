using DevExpress.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace CoordinateConverter.ViewModel
{
    public class SettingsWindowViewModel: ViewModelBase
    {
        public event EventHandler<SettingsWindowArgs> EditEnded;

        public bool clearCheck;
        protected ICurrentWindowService Service { get { return this.GetService<ICurrentWindowService>(); } }

        public bool ClearCheck {

            get { return clearCheck; }
            set
            {
                clearCheck = value;
            }
        }


        public IEnumerable<CoordViewType> CoordTypeValues
        {
            get
            {
                return Enum.GetValues(typeof(CoordViewType))
                     .Cast<CoordViewType>();
            }
        }

        private CoordViewType selectedCoordViewType;

        public CoordViewType SelectedCoordViewType
        {
            get
            {
                return selectedCoordViewType;
            }
            set
            {
                selectedCoordViewType = value;
            }
        }


        private void OnEditEnded(bool clearCheck, CoordViewType viewType)
        {
            try
            { 
            Properties.Settings.Default.ClearCheck = clearCheck;
            Properties.Settings.Default.CoordView = viewType;
            Properties.Settings.Default.Save();
            EditEnded?.Invoke(this, new SettingsWindowArgs());
                Service.Close();
            }
            catch
            {
                MessageBox.Show(
                 "Не удалось выполнить операцию",
                 "Ошибка",
                 MessageBoxButton.OK,
                 MessageBoxImage.Error
                 );
                return;
            }
        }

        private ICommand returnCommand;

        public SettingsWindowViewModel()
        {
            clearCheck = Properties.Settings.Default.ClearCheck;           
            SelectedCoordViewType = Properties.Settings.Default.CoordView;
        }

        public ICommand ReturnCommand
        {
            get
            {
                return returnCommand ??
                  (returnCommand = new DelegateCommand(() =>
                  {
                      OnEditEnded(ClearCheck, selectedCoordViewType);
                  }));
            }
        }

        public class SettingsWindowArgs : EventArgs
        { 

           
            public bool ClearCheck { get; }
          
        }
    }
}
