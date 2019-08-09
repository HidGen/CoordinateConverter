using CoordinateConverter.View;

using DevExpress.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.ComponentModel;
using System.Reflection;
using System.Xml.Linq;

using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using CoordinateConverter.FileInteractions;
using CoordinateConverter.Model;

namespace CoordinateConverter.ViewModel
{
    [TypeConverter(typeof(EnumToStringConverter))]
    public enum CoordinateType
    {
        [Description("МСК-46 зона 1")]
        MSK461,
        [Description("МСК-46 зона 2")]
        MSK462,
        [Description("СК-42")]
        SK42,
        [Description("СК-63")]
        SK63
    }
   
    public class MainWindowViewModel
    {
        public ObservableCollection<RectCoord> RectCoords { get; set; }
        public bool RangeCheck { get; set; }

       public ICommand OpenCommand { get; private set; }
        public ICommand SaveCommand { get; private set; }

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

        public void Init()
        {
          //  


        }



       

        public MainWindowViewModel()
        {
         //   coordinate_system = new ObservableCollection<string>();
            Init();
            OpenCommand = new DelegateCommand(OpenExecute,OpenCanExecute);
            SaveCommand = new DelegateCommand(SaveExecute,SaveCanExecute);
        }
        private void OpenExecute()
        {
            FileInteraction file = new FileInteraction();
            List<RectCoord> rectCoords = new List<RectCoord>();
            file.OpenFile(rectCoords);
            RectCoords = new ObservableCollection<RectCoord>();
            foreach (var rectCoord in rectCoords)
                RectCoords.Add(rectCoord);
        }
        private bool OpenCanExecute()
        {
            return true;
        }
        private void SaveExecute()
        {
            FileInteraction file = new FileInteraction();
            file.Save(new List<GeoCoord>());
        }
        private bool SaveCanExecute()
        {
            return true;
        }


        private ICommand settingsCommand;

        public ICommand SettingsCommand
        {
            get
            {
                return settingsCommand ??
                  (settingsCommand = new DelegateCommand(() =>
                  {
                       var viewModel = new SettingsWindowViewModel();
                      var opensettings = new SettingsWindow { DataContext = viewModel};
                      viewModel.EditEnded += ViewModel_EditEnded;
                      opensettings.Show();


                  }));
            }
        }


        private void ViewModel_EditEnded(object sender, SettingsWindowViewModel.SettingsWindowArgs e)
        {
            Debug.WriteLine(e);

            //  dbWorker.AddBook(e.Entity.Book_Name, e.Entity.Author, e.Entity.Papers, e.Entity.Genre, e.Entity.Publisher.ID);


            selectedCoordinateEnumType = e.SelectedType;

            RangeCheck = e.RangeCheck;



          

        }

        private ICommand chooseRangeCommand;

        public ICommand ChooseRangeCommand
        {
            get
            {
                return chooseRangeCommand ??
                  (chooseRangeCommand = new DelegateCommand(() =>
                  {
                      // var viewModel = new ChangeAuthorViewModel(selectedBooks);
                      var chooserange = new RangeChoiceWindow(); // { DataContext = viewModel };
                                                              //  viewModel.EditEnded += AuthorChange_EditEnded;
                      chooserange.Show();


                  }));
            }
        }
    }
}
