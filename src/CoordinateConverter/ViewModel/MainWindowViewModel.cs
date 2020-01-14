using CoordinateConverter.View;
using DevExpress.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using System.ComponentModel;
using CoordinateConverter.Model;
using CoordinateConverter.FileInteractions;
using Microsoft.Win32;
using System.Collections.Specialized;
using CoordinateConverter.ClipboardInteractions;
using System.Windows;
using DevExpress.Mvvm.DataAnnotations;



namespace CoordinateConverter.ViewModel
{

    public class MainWindowViewModel : ViewModelBase, INotifyPropertyChanged, IDragDropTarget
    {       
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
        private string indexList;



        protected IDialogService ClearGridDialogService { get { return this.GetService<IDialogService>("ClearGridDialogService"); } }

        protected IDialogService RangeChoiceDialogService { get { return this.GetService<IDialogService>("RangeChoiceDialogService"); } }

        public bool ClearRule { get; set; }

        public string IndexList
        {
            get => indexList;
            set
            {
                indexList = value;
                RaisePropertyChanged(nameof(IndexList));
            }
        }

        public bool Busy
        {
            get => busy;
            set
            {
                busy = value;
                RaisePropertyChanged(nameof(Busy));
            }
        }

        public ObservableCollection<CompleteRow> CompleteRows { get; }

        public ObservableCollection<CompleteRow> Selection { get; private set; }       

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
                    switch (SelectedSortEnumType)
                    {
                        case SortType.MinMaxX:
                            SortMinMax(SortType.MinMaxX);
                            break;
                        case SortType.MaxMinX:
                            SortMinMax(SortType.MaxMinX);
                            break;
                        case SortType.MinMaxY:
                            SortMinMax(SortType.MinMaxY);
                            break;
                        case SortType.MaxMinY:
                            SortMinMax(SortType.MaxMinY);
                            break;
                     
                    }
                }
                RaisePropertyChanged(nameof(SelectedSortEnumType));
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
        public ICommand SettingsCommand
        {
            get
            {
                return settingsCommand ??
                  (settingsCommand = new DelegateCommand(() =>
                  {
                      var viewModel = new SettingsWindowViewModel();
                      var opensettings = new SettingsWindow { DataContext = viewModel, WindowStartupLocation = WindowStartupLocation.CenterOwner, Owner = Application.Current.MainWindow };
                      viewModel.EditEnded += ViewModel_EditEnded;
                      opensettings.Show();
                  }));
            }
        }

        public MainWindowViewModel()
        {
            Selection = new ObservableCollection<CompleteRow>();
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


        private void ViewModel_EditEnded(object sender, EventArgs e)
        {           
            SelectedCoordViewType = Properties.Settings.Default.CoordView;
            RaisePropertyChanged(nameof(SelectedCoordViewType));
        }

        public void CoordChanged(object sender, EventArgs e)
        {
            var row = sender as CompleteRow;
            row.GeoCoord = coordConverter.Convert(row.RectCoord);
        }

        private async void OpenExecute()
        {
            try
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
                        return;

                }

                var dlg = new OpenFileDialog
                {
                    FileName = "Документ",
                    DefaultExt = ".xls",
                    Filter = "Файл Excel (.xls;.xlsm;.xlsx)|*.xls;*.xlsm;*.xlsx",
                    Multiselect = true,
                    Title = "Выбор файла"
                };
                var result = dlg.ShowDialog();
                if (result.HasValue == false || result.Value == false)                
                    return;                

                if (Properties.Settings.Default.ClearRule == true)                
                    CompleteRows.Clear();
                
                Busy = true;
                foreach (string filename in dlg.FileNames)
                {
                    if (usedRange)
                    {
                        var completeRows = await excelImporter.ReadRangeAsync(filename, rangeChoiceViewModel.First, rangeChoiceViewModel.Last);
                        AddRows(filename, completeRows);
                    }
                    else
                    {
                        var completeRows = await excelImporter.ReadAsync(filename);
                        AddRows(filename, completeRows);
                    }
                }
                Busy = false;
            }
            catch
            {
                MessageBox.Show(
                 "Не удалось выполнить комманду",
                 "Ошибка",
                 MessageBoxButton.OK,
                 MessageBoxImage.Error
                 );
                Busy = false;
                return;
            }
        }

        private void AddRows(string filename, IList<CompleteRow> completeRows)
        {
            try
            {
                int index = 1;
                foreach (var completeRow in completeRows)
                {                    
                    completeRow.RectCoordPropertyChanged += CoordChanged;                   
                    string temp = string.Empty;
                    completeRow.Description += " Файл: ";
                    for (int i = filename.Length - 1; filename[i] != '\\'; i--)
                        temp += filename[i];             

                    for (int i = temp.Length - 1; i >= 0; i--)                    
                        completeRow.Description += temp[i];                    
                    
                    completeRow.GeoCoord = coordConverter.Convert(completeRow.RectCoord);
                    CompleteRows.Add(completeRow);
                    index++;
                }
            }
            catch
            {
                MessageBox.Show(
                 "Не удалось выполнить комманду",
                 "Ошибка",
                 MessageBoxButton.OK,
                 MessageBoxImage.Error
                 );
                return;
            }
        }

        private void SaveExecute()
        {
            try
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog
                {
                    FileName = "Документ",
                    DefaultExt = ".xml",
                    Filter = "Файл Xml (.xml)|*.xml|All files (*.*)|*.*"
                };

                var geoCoords = CompleteRows.Select(x => x.GeoCoord).ToList();

                if (saveFileDialog.ShowDialog() == true)
                {
                    xmlExporter.SaveToXml(saveFileDialog.FileName, geoCoords);
                }
            }
            catch
            {
                MessageBox.Show(
                 "Не удалось выполнить комманду",
                 "Ошибка",
                 MessageBoxButton.OK,
                 MessageBoxImage.Error
                 );
                return;
            }
        }
        private bool SaveCanExecute()
        {
            return CompleteRows.Count != 0;
        }

        private void AddExecute()
        {
            try
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
            catch
            {
                MessageBox.Show(
                 "Не удалось выполнить комманду",
                 "Ошибка",
                 MessageBoxButton.OK,
                 MessageBoxImage.Error
                 );
                return;
            }
        }

        private void DeleteExecute()
        {
            try
            {
                Selection.ToList().ForEach(item => item.RectCoordPropertyChanged -= CoordChanged);
                Selection.ToList().ForEach(item => CompleteRows.Remove(item));
            }
            catch
            {
                MessageBox.Show(
                 "Не удалось выполнить комманду",
                 "Ошибка",
                 MessageBoxButton.OK,
                 MessageBoxImage.Error
                 );
                return;
            }
        }

        private bool DeleteCanExecute()
        {
            return Selection.Count != 0;
        }

        private void MoveUpExecute()
        {
            try
            {
                CompleteRows.MoveUp(Selection);
                GetSelectedIndexesMethod();
            }
            catch
            {
                MessageBox.Show(
                 "Не удалось выполнить комманду",
                 "Ошибка",
                 MessageBoxButton.OK,
                 MessageBoxImage.Error
                 );
                return;
            }
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
            try
            {
                CompleteRows.MoveDown(Selection);
                GetSelectedIndexesMethod();
            }
            catch
            {
                MessageBox.Show(
                 "Не удалось выполнить комманду",
                 "Ошибка",
                 MessageBoxButton.OK,
                 MessageBoxImage.Error
                 );
                return;
            }
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
            try
            {

                var insertIndex = CompleteRows.Count;
                if (Selection.Count == 1)
                    insertIndex = CompleteRows.IndexOf(Selection[0]);


                var completeRows = paste.PasteDataFromExcel();
                foreach (var completeRow in completeRows)
                {                    
                    completeRow.RectCoordPropertyChanged += CoordChanged;                    
                    completeRow.Description += string.Empty;
                    completeRow.GeoCoord = coordConverter.Convert(completeRow.RectCoord);
                    CompleteRows.Insert(insertIndex, completeRow);
                    insertIndex++;
                }
            }
            catch
            {
                MessageBox.Show(
                 "Не удалось выполнить комманду",
                 "Ошибка",
                 MessageBoxButton.OK,
                 MessageBoxImage.Error
                 );
                return;
            }
        }

        private bool PasteCanExecute()
        {
            return Clipboard.ContainsText();
        }
        private void CopyExecute()
        {
            try
            {
                copy.CopyFromTable(Selection);
            }
            catch
            {
                MessageBox.Show(
                 "Не удалось выполнить комманду",
                 "Ошибка",
                 MessageBoxButton.OK,
                 MessageBoxImage.Error
                 );
                return;
            }
        }
        private bool CopyCanExecute()
        {
            return Selection.Count != 0;
        }
        private void CutExecute()
        {
            try
            {
                copy.CopyFromTable(Selection);
                Selection.ToList().ForEach(item => CompleteRows.Remove(item));
            }
            catch
            {
                MessageBox.Show(
                 "Не удалось выполнить комманду",
                 "Ошибка",
                 MessageBoxButton.OK,
                 MessageBoxImage.Error
                 );
                return;
            }
        }
        private bool CutCanExecute()
        {
            return Selection.Count != 0;
        }

        private void SortExecute(SortType param)
        {
            try
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
                }
            }
            catch
            {
                MessageBox.Show(
                 "Не удалось выполнить комманду",
                 "Ошибка",
                 MessageBoxButton.OK,
                 MessageBoxImage.Error
                 );
                return;
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
            {
                IndexList = string.Empty;
                return;
            }


            var tempIndexes = new List<int>();

            foreach (var item in Selection)
                tempIndexes.Add(CompleteRows.IndexOf(item) + 1);


            var strBld = new StringBuilder();
            foreach (var index in tempIndexes.OrderBy(x => x))
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
            }
        }

        static void MyHandler(object sender, UnhandledExceptionEventArgs args)
        {
            Exception e = (Exception)args.ExceptionObject;
            Console.WriteLine("MyHandler caught : " + e.Message);
            Console.WriteLine("Runtime terminating: {0}", args.IsTerminating);
        }

        public async void OnFileDrop(string[] filepaths)
        {
            int i = 0;

            foreach (var file in filepaths)
            {

                if (CompleteRows.Count != 0 && Properties.Settings.Default.ClearCheck == false && i < 1)
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
                if (Properties.Settings.Default.ClearRule == true && i < 1)
                {
                    CompleteRows.Clear();
                }
                Busy = true;
                var rectCoords = await excelImporter.ReadAsync(filepaths[i]);
                AddRows(filepaths[i++], rectCoords);
                Busy = false;
            }
        }

        public void OnTextDrop(string str)
        {
            string[] lines;
            var delimiters = new char[] { '\n' };
            var completeRows = new List<CompleteRow>();
            str = str.Replace('\r', ' ');
            lines = str.Split(delimiters, StringSplitOptions.None);
            foreach (var line in lines)
            {
                if (line != string.Empty)
                {
                    char[] delimiter = new char[] { '\t' };
                    var values = line.Split(delimiter, StringSplitOptions.None);
                    var rectCoord = new RectCoord();
                    try
                    {
                        rectCoord.X = Convert.ToDouble(values[1]);
                        rectCoord.Y = Convert.ToDouble(values[2]);
                        completeRows.Add(new CompleteRow { RectCoord = rectCoord, Description = values[0] });
                    }
                    catch
                    {
                        try
                        {
                            rectCoord.X = Convert.ToDouble(values[0]);
                            rectCoord.Y = Convert.ToDouble(values[1]);
                            completeRows.Add(new CompleteRow
                            {
                                RectCoord = rectCoord
                            }
                            );
                        }
                        catch
                        {
                            MessageBox.Show(
                           "Неверный формат строки в буфере обмена",
                           "Ошибка",
                           MessageBoxButton.OK,
                           MessageBoxImage.Error
                           );
                            completeRows.Clear();
                            break;
                        }
                    }
                }               
            }
            foreach (var completeRow in completeRows)
            {
                completeRow.RectCoordPropertyChanged += CoordChanged;                
                completeRow.GeoCoord = coordConverter.Convert(completeRow.RectCoord);
                CompleteRows.Add(completeRow);
            }
        }
        
        [Command]
        public void NewItemAdded(CompleteRow row)
        {
            row.RectCoordPropertyChanged += CoordChanged;
            row.GeoCoord = coordConverter.Convert(row.RectCoord);
        }
    }
}
