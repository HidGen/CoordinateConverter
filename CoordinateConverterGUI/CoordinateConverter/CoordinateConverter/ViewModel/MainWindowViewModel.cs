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
using System.Runtime.CompilerServices;
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

    public class MainWindowViewModel : INotifyPropertyChanged
    {
        public bool RangeCheck { get; set; }

        public ObservableCollection<TestData> testList;
        public ObservableCollection<TestData> TestList {
             get { return testList; }
            set
            {
                testList = value;
                NotifyPropertyChanged();
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
                NotifyPropertyChanged();
            }
        }

        public void Init()
        {
            //  
            TestList = new ObservableCollection<TestData>();

            TestList.Add(new TestData
            {
                CoordinateX = 123.58,
                CoordinateY = 853.21,
                HightH = 20,
                NewCoordinateX = 12.58,
                NewCoordinateY = 85.21,
                NewHightH = 2,
                Description = "First element"
            });

            TestList.Add(new TestData
            {
                CoordinateX = 1543.58,
                CoordinateY = 363.21,
                HightH = 14,
                NewCoordinateX = 1.8,
                NewCoordinateY = 8.1,
                NewHightH = 40,
                Description = "Second element"
            });

            TestList.Add(new TestData
            {
                CoordinateX = 123.58,
                CoordinateY = 853.21,
                HightH = 20,
                NewCoordinateX = 12.58,
                NewCoordinateY = 85.21,
                NewHightH = 2,
                Description = "Third element"
            });

           
        }





        public MainWindowViewModel()
        {
         //   coordinate_system = new ObservableCollection<string>();
            Init();
        }


        private ICommand settingsCommand;

        public ICommand SettingsCommand
        {
            get
            {
                return settingsCommand ??
                  (settingsCommand = new DelegateCommand(() =>
                  {
                      Console.WriteLine(TestList);

                       var viewModel = new SettingsWindowViewModel(RangeCheck, SelectedCoordinateEnumType);
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


            SelectedCoordinateEnumType = e.SelectedType;

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

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
