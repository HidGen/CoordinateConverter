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
using System.Diagnostics;
using System.Runtime.CompilerServices;
using CoordinateConverter.Model;
using CoordinateConverter.OpenSaveDialogs;

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

        public ObservableCollection<CompleteRow> CompleteRows { get; }        


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


        public CompleteRow selectedRow;

        public CompleteRow SelectedRow
        {
            get { return selectedRow; }
            set
            {
                selectedRow = value;
            }
        }

        public void Init()
        {
          
        }

        public ICommand OpenCommand { get; private set; }
        public ICommand SaveCommand { get; private set; }

        public ICommand AddRowCommand { get; private set; }
        public ICommand DeleteRowCommand { get; private set; }
        public ICommand MoveUpCommand { get; private set; }

        public ICommand MoveDownCommand { get; private set; }


        public MainWindowViewModel()
        {
         //   coordinate_system = new ObservableCollection<string>();
            Init();
            CompleteRows = new ObservableCollection<CompleteRow>();
            OpenCommand = new DelegateCommand(OpenExecute, OpenCanExecute);
            SaveCommand = new DelegateCommand(SaveExecute, SaveCanExecute);

            AddRowCommand = new DelegateCommand(AddExecute, AddCanExecute);
            DeleteRowCommand = new DelegateCommand(DeleteExecute, DeleteCanExecute);
            MoveUpCommand = new DelegateCommand(MoveUpExecute, MoveUpCanExecute);
            MoveDownCommand = new DelegateCommand(MoveDownExecute, MoveDownCanExecute);
        }



        private ICommand settingsCommand;

        public ICommand SettingsCommand
        {
            get
            {
                return settingsCommand ??
                  (settingsCommand = new DelegateCommand(() =>
                  {
             

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



        private void OpenExecute()
        {
            var open = new OpenDialog();
            List<CompleteRow> completeRows = new List<CompleteRow>();
            completeRows = open.OpenFile(CoordConverter.CoordinateSystem.LCS46_1);           
            foreach (var completeRow in completeRows)            
                CompleteRows.Add(completeRow);
        }
        private bool OpenCanExecute()
        {
            return true;
        }
        private void SaveExecute()
        {
            var save = new SaveDialog();
            var geoCoords = new List<GeoCoord>();
            foreach (CompleteRow completeRow in CompleteRows)
                geoCoords.Add(completeRow.geoCoord);
            save.Save(geoCoords);
        }
        private bool SaveCanExecute()
        {
            return true;
        }



        private void AddExecute()
        {
            int foundIndex = default;
            for (int i = 0; i < CompleteRows.Count; i++)
            {
                if (CompleteRows[i] == SelectedRow)
                {
                    foundIndex = i;
                    break;
                }
               

            }
            CompleteRows.Insert(foundIndex, new CompleteRow());
        }

        private bool AddCanExecute()
        {
            return true;
        }


                    

private void DeleteExecute()
{
            int foundIndex = default;
            for (int i = 0; i < CompleteRows.Count; i++)
            {
                if (CompleteRows[i] == SelectedRow)
                {
                    foundIndex = i;
                    break;
                }

            }
            CompleteRows.RemoveAt(foundIndex);

        }

        private bool DeleteCanExecute()
        {
            return SelectedRow != null;
        }
        private void MoveUpExecute()
        {
            int foundIndex = default;
            for (int i = 0; i < CompleteRows.Count; i++)
            {
                if (CompleteRows[i] == SelectedRow)
                {
                    foundIndex = i;
                    break;
                }

            }
            CompleteRows.Move(foundIndex, foundIndex - 1);
        }

        private bool MoveUpCanExecute()
        {
            int foundIndex = default;
            for (int i = 0; i < CompleteRows.Count; i++)
            {
                if (CompleteRows[i] == SelectedRow)
                {
                    foundIndex = i;
                    break;
                }

            }
            if (foundIndex != 0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        private void MoveDownExecute()
        {
            int foundIndex = default;
            for (int i = 0; i < CompleteRows.Count; i++)
            {
                if (CompleteRows[i] == SelectedRow)
                {
                    foundIndex = i;
                    break;
                }

            }
            CompleteRows.Move(foundIndex, foundIndex + 1);

        }

        private bool MoveDownCanExecute()
        {
            int foundIndex = default;
            for (int i = 0; i < CompleteRows.Count; i++)
            {
                if (CompleteRows[i] == SelectedRow)
                {
                    foundIndex = i;
                    break;
                }

            }
            if (foundIndex != CompleteRows.Count - 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }                                 
                     
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
