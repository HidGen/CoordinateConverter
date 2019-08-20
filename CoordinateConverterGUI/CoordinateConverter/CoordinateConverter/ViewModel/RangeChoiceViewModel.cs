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
        public string First { get; set; }
        public string Last { get; set; }
        public ICommand OKCommand { get; private set; }
        public RangeChoiceViewModel()
        {
            First = string.Empty;
            Last = string.Empty;
            OKCommand = new DelegateCommand(OKExecute,OKCanExecute);
        }
        private void OKExecute()
        {
            OnEditEnded(First, Last);
        }
        private bool OKCanExecute()
        {
            return (First != String.Empty && Last != String.Empty);
        }

        public event EventHandler<RangeArgs> EditEnded;
        public class RangeArgs : EventArgs
        {
            public RangeArgs(string first, string last)
            {
                First = first;
                Last = last;
            }

            public string First { get; private set; }
            public string Last { get; private set; }

        }

        private void OnEditEnded(string first, string last)
        {
            EditEnded?.Invoke(this, new RangeArgs(first, last));
        }
    }
}
