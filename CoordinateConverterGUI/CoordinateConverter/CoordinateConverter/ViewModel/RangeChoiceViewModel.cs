using DevExpress.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CoordinateConverter.ViewModel
{
    class RangeChoiceViewModel
    {

        private readonly UICommand okCommand;

        private string first;
        private string last;

        public string First
        {
            get => first;
            set
            {
                first = value;
            }

        }
        public string Last
        {
            get => last;
            set
            {
                last = value;
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

                })
            };
        }

        public IEnumerable<UICommand> GetCommands()
        {
            return new List<UICommand> { okCommand };
        }
    }
}
