using System.Windows;
using System.Windows.Input;
using CoordinateConverter.Model;
using DevExpress.Mvvm;
using DevExpress.Mvvm.UI.Interactivity;
using DevExpress.Xpf.Grid;

namespace CoordinateConverter
{
    public class NewItemRowBehavior : Behavior<TableView>
    {
        public ICommand NewItemRowUpdated
        {
            get { return (ICommand)GetValue(NewItemRowUpdatedProperty); }
            set { SetValue(NewItemRowUpdatedProperty, value); }
        }
        public static readonly DependencyProperty NewItemRowUpdatedProperty =
            DependencyProperty.Register("NewItemRowUpdated", typeof(ICommand), typeof(NewItemRowBehavior), new PropertyMetadata(null));

        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.RowUpdated += OnRowUpdated;
        }
        protected override void OnDetaching()
        {
            base.OnDetaching();
        }

        void OnRowUpdated(object sender, RowEventArgs e)
        {
            
            OnRowUpdated(e);
        }

        protected virtual void OnRowUpdated(RowEventArgs e)
        {
            try
            {
                if (e.RowHandle == GridControl.NewItemRowHandle && NewItemRowUpdated != null && NewItemRowUpdated.CanExecute(e.Row))
                    NewItemRowUpdated.Execute(e.Row);
            }
            catch { }
        }
    }
}
