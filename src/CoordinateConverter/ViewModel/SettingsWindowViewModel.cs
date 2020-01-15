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
        private ICommand returnCommand;
        private ICurrentWindowService Service { get { return GetService<ICurrentWindowService>(); } }

        public bool ClearCheck { get; set; }

        public event EventHandler EditEnded;


        public IEnumerable<CoordViewType> CoordTypeValues
        {
            get
            {
                return Enum.GetValues(typeof(CoordViewType))
                     .Cast<CoordViewType>();
            }
        }
        public ICommand ReturnCommand
        {
            get
            {
                return returnCommand ??
                  (returnCommand = new DelegateCommand(() =>
                  {
                      OnEditEnded(ClearCheck, SelectedCoordViewType);
                  }));
            }
        }

        public CoordViewType SelectedCoordViewType { get; set; }


        private void OnEditEnded(bool clearCheck, CoordViewType viewType)
        {
            try
            { 
                Properties.Settings.Default.ClearCheck = clearCheck;
                Properties.Settings.Default.CoordView = viewType;
                Properties.Settings.Default.Save();
                EditEnded?.Invoke(this, EventArgs.Empty);
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
        

        public SettingsWindowViewModel()
        {
            ClearCheck = Properties.Settings.Default.ClearCheck;           
            SelectedCoordViewType = Properties.Settings.Default.CoordView;
        }

           
    }
}
