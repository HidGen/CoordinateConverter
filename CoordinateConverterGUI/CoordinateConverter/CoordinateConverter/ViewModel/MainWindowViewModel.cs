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
using CoordinateConverter.OpenSaveDialogs;
using Microsoft.Win32;
using System.Collections.Specialized;

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
        private CoordinateType selectedCoordinateEnumType;
        private CompleteRow selectedRow;
        private IExcelFileOpen excelImporter;
        private IXmlFileSave xmlExporter;
        private bool busy;
        private CoordConverter coordConverter;
        private ObservableCollection<int> indexes = new ObservableCollection<int>();
        public string indexList;

        public bool RangeCheck { get; set; }

        public string IndexList
        {
            get => indexList;
            set
            {
                indexList = value;
                NotifyPropertyChanged();
            }
        }

        public ObservableCollection<int> Indexes
        {
            get => indexes;
            set
            {
                indexes = value;
                NotifyPropertyChanged();
            }
        }

        public bool Busy
        {
            get => busy;
            set
            {
                busy = value;
                NotifyPropertyChanged();
            }
        }

        public ObservableCollection<CompleteRow> CompleteRows { get; }

        public ObservableCollection<CompleteRow> selection = new ObservableCollection<CompleteRow>();

        public ObservableCollection<CompleteRow> Selection
        {
            get
            {
              
                return this.selection;
               
            }
            //set
            //{
            //    selection = value;
            //    Indexes = GetSelectedIndexes();


            //}
        }

        public IEnumerable<CoordinateType> CoordinateEnumTypeValues
        {
            get
            {
                return Enum.GetValues(typeof(CoordinateType))
                    .Cast<CoordinateType>();
            }
        }
                
        public CoordinateType SelectedCoordinateEnumType
        {
            get { return selectedCoordinateEnumType; }
            set
            {
                selectedCoordinateEnumType = value;
                //сюда

                NotifyPropertyChanged();
            }
        }

        public CompleteRow SelectedRow
        {
            get { return selectedRow; }
            set
            {
                selectedRow = value;
            }
        }

        public ICommand OpenCommand { get; private set; }
        public ICommand SaveCommand { get; private set; }
        public ICommand AddRowCommand { get; private set; }
        public ICommand DeleteRowCommand { get; private set; }
        public ICommand MoveUpCommand { get; private set; }
        public ICommand MoveDownCommand { get; private set; }

        public MainWindowViewModel()
        {
            var interaction = new FileInteraction();
            excelImporter = interaction;
            xmlExporter = interaction;
            coordConverter = new CoordConverter();
            Busy = false;
            


            CompleteRows = new ObservableCollection<CompleteRow>();
            OpenCommand = new DelegateCommand(OpenExecute, OpenCanExecute);
            SaveCommand = new DelegateCommand(SaveExecute, SaveCanExecute);
            AddRowCommand = new DelegateCommand(AddExecute, AddCanExecute);
            DeleteRowCommand = new DelegateCommand(DeleteExecute, DeleteCanExecute);
            MoveUpCommand = new DelegateCommand(MoveUpExecute, MoveUpCanExecute);
            MoveDownCommand = new DelegateCommand(MoveDownExecute, MoveDownCanExecute);
            Indexes = new ObservableCollection<int>();
               Selection.CollectionChanged += GetSelectedIndexes;
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

        public void CoordChanged(object sender, EventArgs e)
        {
            //coordConverter.Convert(SelectedCoordinateEnumType, rectCoord);
            var row = sender as CompleteRow;
            row.GeoCoord = coordConverter.Convert(SelectedCoordinateEnumType, row.RectCoord);
        }

        private async void OpenExecute()
        {

            var dlg = new OpenFileDialog();
            dlg.FileName = "Document"; 
            dlg.DefaultExt = ".xls"; 
            dlg.Filter = "Excel documents (.xls;.xlsm;.xlsx)|*.xls;*.xlsm;*.xlsx|All files (*.*)|*.*"; // Filter files by extension
            dlg.Multiselect = true;       
            var result = dlg.ShowDialog();
            if (result.HasValue == false || result.Value == false)
                return;
            // true
            Busy = true;

            foreach (string filename in dlg.FileNames)
            {
                var rectCoords = await excelImporter.ReadAsync(filename);
                foreach (RectCoord rectCoord in rectCoords)
                {
                    var completeRow = new CompleteRow();
                    completeRow.RectCoordPropertyChanged += CoordChanged;
                    completeRow.RectCoord = rectCoord;
                    completeRow.Description = filename;
                    completeRow.GeoCoord = coordConverter.Convert(SelectedCoordinateEnumType, rectCoord);
                    CompleteRows.Add(completeRow);
                }                  
            }

            Busy = false;
            // false
        }



        private bool OpenCanExecute()
        {
            return true;
        }
        private void SaveExecute()
        {
            //var save = new SaveDialog();
            var geoCoords = new List<GeoCoord>();
            //var fileInteraction = new FileInteraction();
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.FileName = "Document"; // Default file name
            saveFileDialog.DefaultExt = ".xml"; // Default file extension
            saveFileDialog.Filter = "Xml documents (.xml)|*.xml|All files (*.*)|*.*";
            foreach (CompleteRow completeRow in CompleteRows)
                geoCoords.Add(completeRow.GeoCoord);
            if (saveFileDialog.ShowDialog() == true)
            {
                xmlExporter.SaveToXml(saveFileDialog.FileName, geoCoords);
            }

            //save.Save(geoCoords);
        }
        private bool SaveCanExecute()
        {
            return true;
        }

        private void AddExecute()
        {
            if (Selection.Count != 0)
            {
               // int foundIndex = default;
                for (int i = 0; i < CompleteRows.Count ; i++)
                {

                    if (CompleteRows[i] == Selection[0])
                    {
                        //  foundIndex = i;
                        var completeRow = new CompleteRow();
                        completeRow.RectCoordPropertyChanged += CoordChanged;
                        CompleteRows.Insert(i, completeRow);
                        break;
                    }
                }
                
            }
            else
            {
                var completeRow = new CompleteRow();
                completeRow.RectCoordPropertyChanged += CoordChanged;
                CompleteRows.Insert(CompleteRows.Count, completeRow);
            }
        }

        private bool AddCanExecute()
        {
            return true;
        }

        private void DeleteExecute()
        {
            Selection.ToList().ForEach(item => item.RectCoordPropertyChanged -= CoordChanged);
            Selection.ToList().ForEach(item => CompleteRows.Remove(item));
        }

        private bool DeleteCanExecute()
        {
            return Selection.Count != 0;
        }

        private void MoveUpExecute()
        {

            for (int i = 0; i < CompleteRows.Count ; i++)
            {
                for (int j = 0; j < Selection.Count ; j++)
                {
                    if (Selection[j] == CompleteRows[i])
                    {
                        CompleteRows.Move(i, i - 1);
                        break;
                    }
                }
            }

        }

        private bool MoveUpCanExecute()
        {
            bool checkDown = default;
            foreach (var row in Selection)
            {
                if (row == CompleteRows[0])
                {
                    checkDown = false;
                    break;
                }
                else
                {
                    checkDown = true;
                }
            }

            if (checkDown == true)
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
            for (int i = CompleteRows.Count-1; i >= 0; i--)
            {
                for (int j = 0; j < Selection.Count  ; j++)
                {
                    if (Selection[j] == CompleteRows[i])
                    {
                        CompleteRows.Move(i, i + 1);
                        break;
                    }
                }
            }
        }

        private bool MoveDownCanExecute()
        {
            bool checkDown = default;
            foreach (var row in Selection)
            {
                if (row == CompleteRows[CompleteRows.Count - 1])
                {
                    checkDown =  false;
                    break;
                }
                else
                {
                    checkDown = true;
                }
            }

            if (checkDown == true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void GetSelectedIndexes(object sender, NotifyCollectionChangedEventArgs e)
        {
            //   var SelectedIndexes = new ObservableCollection<int>();
            Indexes.Clear();
            for (int i = 0; i < CompleteRows.Count; i++)
            {
                for (int j = 0; j < Selection.Count; j++)
                {
                    if (Selection[j] == CompleteRows[i])
                    {
                        // CompleteRows.Move(i, i + 1);
                        Indexes.Add(i+1);
                        break;
                    }
                }
            }

            IndexList = "";

            for (int i = 0; i< Indexes.Count; i++)
            {
                if (i != (Indexes.Count -1))
                {
                    IndexList += Indexes[i] + "; ";
                }
                else
                {
                    IndexList += Indexes[i];
                }
            }

          //  return SelectedIndexes;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }


}
