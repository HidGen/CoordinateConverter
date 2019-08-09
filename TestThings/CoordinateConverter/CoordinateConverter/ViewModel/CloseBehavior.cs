using DevExpress.Mvvm.UI.Interactivity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interactivity;

namespace CoordinateConverter.ViewModel
{
    public class CloseBehavior : System.Windows.Interactivity.Behavior<Button>
    {
        protected override void OnAttached()
        {
            AssociatedObject.Click += OnClick;
        }

        protected override void OnDetaching()
        {
            AssociatedObject.Click -= OnClick;
        }

        private void OnClick(object sender, RoutedEventArgs e)
        {
            //Application.Current.Shutdown();
            //    Window somewindow = Window.GetWindow(e);
            /* var originalControl = GetOriginalControl(window) as DependencyObject;
             window.Close();*/


            var window = Window.GetWindow(AssociatedObject);
            window.Close();
        }
    }
}
