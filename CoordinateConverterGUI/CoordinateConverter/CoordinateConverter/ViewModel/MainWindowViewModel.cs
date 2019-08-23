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

using Microsoft.Win32;
using System.Collections.Specialized;
using CoordinateConverter.ClipboardInteractions;
using System.Windows;

namespace CoordinateConverter.ViewModel
{

    public class MainWindowViewModel : ViewModelBase, INotifyPropertyChanged
    {
        private CoordinateType selectedCoordinateEnumType;
        private SortType selectedSortEnumType;
        private CoordViewType selectedCoordViewType;       
        private IExcelFileOpen excelImporter;
        private IXmlFileSave xmlExporter;
        private Copy copy;
        private Paste paste;
        private bool busy;
        private CoordConverter coordConverter;
        private ObservableCollection<int> indexes = new ObservableCollection<int>();
        private ICommand settingsCommand;





        protected IDialogService ClearGridDialogService { get { return this.GetService<IDialogService>("ClearGridDialogService"); } }

        protected IDialogService RangeChoiceDialogService { get { return this.GetService<IDialogService>("RangeChoiceDialogService"); } }

        public bool ClearRule { get; set; }

        public string IndexList
        {
            get => indexList;
            set
            {
                indexList = value;
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
                if (selectedCoordinateEnumType != value)
                {
                    selectedCoordinateEnumType = value;                    
                    if (CompleteRows.Count != 0)
                    {
                        foreach (var completeRow in CompleteRows)
                        completeRow.GeoCoord = coordConverter.Convert(SelectedCoordinateEnumType, completeRow.RectCoord);                 
                    }
                    NotifyPropertyChanged();
                }
            }
        }


        public IEnumerable<SortType> SortEnumTypeValues
        {
            get
            {
                return Enum.GetValues(typeof(SortType))
                    .Cast<SortType>();
            }
        }

        public SortType SelectedSortEnumType
        {
            get { return selectedSortEnumType; }
            set
            {
                selectedSortEnumType = value;
                if (CompleteRows.Count != 0)
                {
                    if (SelectedSortEnumType == SortType.MinMaxX)
                    {
                        SortMinMax(SortType.MinMaxX);
                    }
                    else if (SelectedSortEnumType == SortType.MaxMinX)
                    {
                        SortMinMax(SortType.MaxMinX);
                    }
                    else if (SelectedSortEnumType == SortType.MinMaxY)
                    {
                        SortMinMax(SortType.MinMaxY);
                    }
                    else if (SelectedSortEnumType == SortType.MaxMinY)
                    {
                        SortMinMax(SortType.MaxMinY);
                    }
                    else if (SelectedSortEnumType == SortType.MinMaxH)
                    {
                        SortMinMax(SortType.MinMaxH);
                    }
                    else if (SelectedSortEnumType == SortType.MaxMinH)
                    {
                        SortMinMax(SortType.MaxMinH);
                    }
                }
                NotifyPropertyChanged();
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

        public CoordViewType SelectedCoordViewType
        {
            get
            {
                return selectedCoordViewType;
            }
            set
            {
                if (selectedCoordViewType != value)
                {
                    selectedCoordViewType = value;
                    if (CompleteRows != null)
                    {
                        foreach (var item in CompleteRows)
                        {
                            item.GeoCoordChanged();
                        }
                    }
                } 
            }
        }

        public ICommand AddRowCommand { get; private set; }
        public ICommand CopyCommand { get; private set; }
        public ICommand CutCommand { get; private set; }
        public ICommand DeleteRowCommand { get; private set; }
        public ICommand MoveDownCommand { get; private set; }
        public ICommand MoveUpCommand { get; private set; }
        public ICommand OpenCommand { get; private set; }
        public ICommand PasteCommand { get; private set; }
        public ICommand SaveCommand { get; private set; }
        public ICommand<SortType> SortCommand { get; private set; }
        public string indexList;
        public event PropertyChangedEventHandler PropertyChanged;

        public MainWindowViewModel()
        {
            SelectedCoordViewType = Properties.Settings.Default.CoordView;
            var interaction = new FileInteraction();
            excelImporter = interaction;
            xmlExporter = interaction;
            copy = new Copy();
            paste = new Paste();
            coordConverter = new CoordConverter();
            Busy = false;
            CompleteRows = new ObservableCollection<CompleteRow>();
            OpenCommand = new DelegateCommand(OpenExecute);
            SaveCommand = new DelegateCommand(SaveExecute, SaveCanExecute);
            AddRowCommand = new DelegateCommand(AddExecute);
            DeleteRowCommand = new DelegateCommand(DeleteExecute, DeleteCanExecute);
            MoveUpCommand = new DelegateCommand(MoveUpExecute, MoveUpCanExecute);
            MoveDownCommand = new DelegateCommand(MoveDownExecute, MoveDownCanExecute);
            PasteCommand = new DelegateCommand(PasteExecute, PasteCanExecute);
            CopyCommand = new DelegateCommand(CopyExecute, CopyCanExecute);
            CutCommand = new DelegateCommand(CutExecute, CutCanExecute);
            SortCommand = new DelegateCommand<SortType>(SortExecute, SortCanExecute);
            Selection.CollectionChanged += GetSelectedIndexes;
        }


        public ICommand SettingsCommand
        {
            get
            {
                return settingsCommand ??
                  (settingsCommand = new DelegateCommand(() =>
                  {
                      var viewModel = new SettingsWindowViewModel(SelectedCoordinateEnumType);
                      var opensettings = new SettingsWindow { DataContext = viewModel };
                      viewModel.EditEnded += ViewModel_EditEnded;
                      opensettings.Show();
                  }));
            }
        }

        private void ViewModel_EditEnded(object sender, SettingsWindowViewModel.SettingsWindowArgs e)
        {
            SelectedCoordinateEnumType = e.SelectedType;
            SelectedCoordViewType = Properties.Settings.Default.CoordView;
        }

        public void CoordChanged(object sender, EventArgs e)
        {
            var row = sender as CompleteRow;
            row.GeoCoord = coordConverter.Convert(SelectedCoordinateEnumType, row.RectCoord);
        }

        private async void OpenExecute()
        {
            var usedRange = false;
            RangeChoiceViewModel rangeChoiceViewModel = null;

            if (Keyboard.IsKeyDown(Key.LeftShift) || Keyboard.IsKeyDown(Key.RightShift))
            {
                rangeChoiceViewModel = new RangeChoiceViewModel();
                var rangeResult = RangeChoiceDialogService.ShowDialog(
                    dialogCommands: rangeChoiceViewModel.GetCommands(),
                    title: "Открыть",
                    viewModel: rangeChoiceViewModel);

                if (rangeResult == null)
                {
                    return;
                }
                    usedRange = true;
            }

            if (CompleteRows.Count != 0 && Properties.Settings.Default.ClearCheck == false)
            {
                var clearGridViewmodel = new ClearGridViewModel();
                var clearResult = ClearGridDialogService.ShowDialog(
                    dialogCommands: clearGridViewmodel.GetCommands(),
                    title: "Открыть",
                    viewModel: clearGridViewmodel);

                if (clearResult == null)
                {
                    return;
                }     
            }
            
            var dlg = new OpenFileDialog();
            dlg.FileName = "Document";
            dlg.DefaultExt = ".xls";
            dlg.Filter = "Excel documents (.xls;.xlsm;.xlsx)|*.xls;*.xlsm;*.xlsx";
            dlg.Multiselect = true;
            var result = dlg.ShowDialog();
            if (result.HasValue == false || result.Value == false)
            {
                return;
            }

            if (Properties.Settings.Default.ClearRule == true)
            {
                CompleteRows.Clear();
            }
            Busy = true;
            foreach (string filename in dlg.FileNames)
            {
                if (usedRange)
                {
                    var rectCoords = await excelImporter.ReadRangeAsync(filename, rangeChoiceViewModel.First, rangeChoiceViewModel.Last);
                    AddRows(filename, rectCoords);

                }
                else
                {
                    var rectCoords = await excelImporter.ReadAsync(filename);
                    AddRows(filename, rectCoords);
                }
            }
            Busy = false;
        }

        private void AddRows(string filename, List<RectCoord> rectCoords)
        {
            int index = 1;
            foreach (RectCoord rectCoord in rectCoords)
            {
                var completeRow = new CompleteRow();
                completeRow.RectCoordPropertyChanged += CoordChanged;
                completeRow.RectCoord = rectCoord;
                string temp = String.Empty;
                completeRow.Description += "Файл: ";
                for (int i = filename.Length - 1; filename[i] != '\\'; i--)
                {
                    temp += filename[i];

                }
                for (int i = temp.Length - 1; i >= 0; i--)
                {
                    completeRow.Description += temp[i];
                }
                completeRow.Description += "; Строка " + index.ToString();
                completeRow.GeoCoord = coordConverter.Convert(SelectedCoordinateEnumType, rectCoord);
                CompleteRows.Add(completeRow);
                index++;
            }
        }

        private void SaveExecute()
        {
            var geoCoords = new List<GeoCoord>();
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
        }
        private bool SaveCanExecute()
        {
            return CompleteRows.Count != 0;
        }

        private void AddExecute()
        {
            if (Selection.Count != 0)
            {               
                for (int i = 0; i < CompleteRows.Count; i++)
                {
                    if (CompleteRows[i] == Selection[0])
                    {
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
            CompleteRows.MoveUp(Selection);
            GetSelectedIndexesMethod();
        }

        private bool MoveUpCanExecute()
        {
            bool checkUp = default;
            foreach (var row in Selection)
            {
                if (row == CompleteRows[0])
                {
                    checkUp = false;
                    break;
                }
                else
                {
                    checkUp = true;
                }
            }

            return checkUp;
        }

        private void MoveDownExecute()
        {
            CompleteRows.MoveDown(Selection);

            GetSelectedIndexesMethod();
        }

        private bool MoveDownCanExecute()
        {
            bool checkDown = default;
            foreach (var row in Selection)
            {
                if (row == CompleteRows[CompleteRows.Count - 1])
                {
                    checkDown = false;
                    break;
                }
                else
                {
                    checkDown = true;
                }
            }
            return checkDown;
        }
        private void PasteExecute()
        {
            var rectCoords = paste.PasteDataFromExcel();
            foreach (var rectCoord in rectCoords)
            {
                var completeRow = new CompleteRow();
                completeRow.RectCoordPropertyChanged += CoordChanged;
                completeRow.RectCoord = rectCoord;
                completeRow.Description = "From Clipboard";
                completeRow.GeoCoord = coordConverter.Convert(SelectedCoordinateEnumType, rectCoord);
                if (Selection.Count == 1)
                {
                    for (int i = 0; i < CompleteRows.Count; i++)
                    {
                        if (CompleteRows[i] == Selection[0])
                        {
                            CompleteRows.Insert(i, completeRow);
                            break;
                        }
                    }
                }
                else
                {
                    CompleteRows.Add(completeRow);
                }
            }
        }
        private bool PasteCanExecute()
        {
            return Clipboard.ContainsText();
        }
        private void CopyExecute()
        {
            copy.CopyFromTable(Selection);
        }
        private bool CopyCanExecute()
        {
            return Selection.Count != 0;
        }
        private void CutExecute()
        {
            copy.CopyFromTable(Selection);
            Selection.ToList().ForEach(item => CompleteRows.Remove(item));
        }
        private bool CutCanExecute()
        {
            return Selection.Count != 0;
        }

        private void SortExecute(SortType param)
        {
                switch (param)
                {
                    case SortType.MinMaxX:
                        {
                            SelectedSortEnumType = SortType.MinMaxX;
                            return;
                        }
                    case SortType.MaxMinX:
                        {
                            SelectedSortEnumType = SortType.MaxMinX;
                            return;
                        }
                    case SortType.MinMaxY:
                        {
                            SelectedSortEnumType = SortType.MinMaxY;
                            return;
                        }
                    case SortType.MaxMinY:
                        {
                            SelectedSortEnumType = SortType.MaxMinY;
                            return;
                        }
                    case SortType.MinMaxH:
                        {
                            SelectedSortEnumType = SortType.MinMaxH;
                            return;
                        }
                    case SortType.MaxMinH:
                        {
                            SelectedSortEnumType = SortType.MaxMinH;
                            return;
                        }
                }     
        }

        private bool SortCanExecute(SortType param)
        {
            return CompleteRows.Count != 0;
        }

        public void GetSelectedIndexes(object sender, NotifyCollectionChangedEventArgs e)
        {
            GetSelectedIndexesMethod();
        }

        public void GetSelectedIndexesMethod()
        {
            if (Selection.Count == 0)
                return;

            var tempIndexes = new List<int>();

            foreach (var item in Selection)
                tempIndexes.Add(CompleteRows.IndexOf(item) + 1);


            var strBld = new StringBuilder();
            foreach (var index in tempIndexes.OrderBy(x=>x))            
                strBld.Append(index + "; ");

            strBld.Remove(strBld.Length - 2, 2);
            IndexList = strBld.ToString();
        }

        public void SortMinMax(SortType type)
        {
            switch (type)
            {
                case SortType.MinMaxX:
                    {
                        CompleteRows.SortAsc((x, y) => x.RectCoord.X.CompareTo(y.RectCoord.X));
                        return;
                    }
                case SortType.MaxMinX:
                    {
                        CompleteRows.SortDesc((x, y) => x.RectCoord.X.CompareTo(y.RectCoord.X));
                        return;
                    }
                case SortType.MinMaxY:
                    {
                        CompleteRows.SortAsc((x, y) => x.RectCoord.Y.CompareTo(y.RectCoord.Y));
                        return;
                    }
                case SortType.MaxMinY:
                    {
                        CompleteRows.SortDesc((x, y) => x.RectCoord.Y.CompareTo(y.RectCoord.Y));
                        return;
                    }
                case SortType.MinMaxH:
                    {
                        CompleteRows.SortAsc((x, y) => x.RectCoord.H.CompareTo(y.RectCoord.H));
                        return;
                    }
                case SortType.MaxMinH:
                    {
                        CompleteRows.SortDesc((x, y) => x.RectCoord.H.CompareTo(y.RectCoord.H));
                        return;
                    }
            }
        }

        
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }


}
