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
using CoordinateConverter.FileInteractions;

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

        private readonly ObservableCollection<CompleteRow> selection = new ObservableCollection<CompleteRow>();

        public ObservableCollection<CompleteRow> Selection { get { return this.selection; } }
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
                      var chooserange = new RangeChoiceWindow();
                      chooserange.Show();
                  }));
            }
        }

        private void OpenExecute()
        {
            FileInteraction file = new FileInteraction();
            List<CompleteRow> completeRows = new List<CompleteRow>();
            completeRows = file.OpenFile();        
            foreach (var completeRow in completeRows)            
            CompleteRows.Add(completeRow);
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



        private void AddExecute()
        {
            int foundIndex = default;
            for (int i = 0; i < CompleteRows.Count; i++)
            {
                if (CompleteRows[i] == Selection[0])
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
            //int foundIndex = default;
            //for (int i = 0; i < CompleteRows.Count; i++)
            //{
            //    if (CompleteRows[i] == SelectedRow)
            //    {
            //        foundIndex = i;
            //        break;
            //    }

            //}
            //CompleteRows.RemoveAt(foundIndex);

            Selection.ToList().ForEach(item => CompleteRows.Remove(item));

        }

        private bool DeleteCanExecute()
        {
            return Selection.Count != 0;
        }







        private void MoveUpExecute()
        {

            for (int n = 0; n < Selection.Count; n++)
            {
                int foundIndex = default;
                for (int i = 0; i < CompleteRows.Count; i++)
                {
                    if (CompleteRows[i] == Selection[n])
                    {
                        foundIndex = i;
                        break;
                    }

                }
                CompleteRows.Move(foundIndex, foundIndex - 1);
            }
        }

        private bool MoveUpCanExecute()
        {
            int foundIndex = default;
            for (int j = 0; j < Selection.Count; j++)
            {
                for (int i = 0; i < CompleteRows.Count; i++)
                {
                    if (Selection.Count != 0)
                    {

                        if (CompleteRows[i] == Selection[j])
                        {
                            foundIndex = i;

                            break;
                        }

                    }

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
            for (int n = Selection.Count-1; n >= 0; n--)
            {
                int foundIndex = default;
                for (int i = 0; i < CompleteRows.Count; i++)
                {
                    
                        if (CompleteRows[i] == Selection[n])
                        {
                            foundIndex = i;
                            break;
                        }
                    

                }
                CompleteRows.Move(foundIndex, foundIndex + 1);
            }
        }

        private bool MoveDownCanExecute()
        {
            int foundIndex = default;
            for (int i = 0; i < CompleteRows.Count; i++)
            {
            if (Selection.Count != 0)
            {
                if (CompleteRows[i] == Selection[Selection.Count-1])
                {
                    foundIndex = i;
                    break;
                }
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
