using DevExpress.Mvvm;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;

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
                first = value;
                first = first?.ToUpper();

            }

        }
        public string Last
        {
            get => last;
            set
            {
                last = value;
                last = last?.ToUpper();
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
                    if (First != string.Empty)
                    {

                        if (!excelCellFormat.IsMatch(First))
                        {
                            error = "Неверный формат";
                            this.errorFirst = true;
                        }
                        else
                            this.errorFirst = false;
                    }
                    else
                        this.errorFirst = true;
                }
                if (columnName == "Last")
                {
                    if (Last != string.Empty)
                    {
                        Last = Last.ToUpper();
                        if (!excelCellFormat.IsMatch(Last))
                        {
                            error = "Неверный формат";
                            this.errorLast = true;
                        }
                        else
                            this.errorLast = false;

                    }
                    else
                        this.errorLast = true;
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
        
        //private bool OKCanExecute()
        //{

        //}

        public IEnumerable<UICommand> GetCommands()
        {
            return new List<UICommand> { okCommand };
        }
    }
}
