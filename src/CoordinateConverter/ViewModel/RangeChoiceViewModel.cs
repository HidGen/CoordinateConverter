using DevExpress.Mvvm;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text.RegularExpressions;

namespace CoordinateConverter.ViewModel
{
    class RangeChoiceViewModel : IDataErrorInfo
    {
        private readonly UICommand okCommand;

        private string first;
        private string last;
        private bool errorFirst;
        private bool errorLast;

        public string First
        {
            get => first;
            set
            {
                first = value?.ToUpper();
            }

        }
        public string Last
        {
            get => last;
            set
            {
                last = value?.ToUpper();
            }
        }

        public string Error => null;

        public string this[string columnName]
        {
            get
            {
                string error = String.Empty;
                Regex excelCellFormat = new Regex(@"^[A-Z]+[0-9]+$");
                if (columnName == "First")
                {
                    if (!string.IsNullOrEmpty(First))
                    {
                        if (!excelCellFormat.IsMatch(First))
                        {
                            error = "Неверный формат ячейки Excel";
                            errorFirst = true;
                        }
                        else
                            errorFirst = false;
                    }
                    else
                        errorFirst = true;
                }
                if (columnName == "Last")
                {
                    if (Last != string.Empty)
                    {                       
                        if (!excelCellFormat.IsMatch(Last))
                        {
                            error = "Неверный формат ячейки Excel";
                            errorLast = true;
                        }
                        else
                            errorLast = false;
                    }
                    else
                        errorLast = true;
                }

                return error;
            }
        }

        public RangeChoiceViewModel()
        {
            okCommand = new UICommand()
            {
                Caption = "OK",
                IsDefault = true,
                Command = new DelegateCommand(() =>
                {

                },
                () =>
                {
                    return !errorFirst && !errorLast;
                })
            };
            first = string.Empty;
            last = string.Empty;
        }


        public IEnumerable<UICommand> GetCommands()
        {
            return new List<UICommand> { okCommand };
        }
    }
}