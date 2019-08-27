using CoordinateConverter.FileInteractions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace CoordinateConverter.ViewModel
{
    public class FileDragDropHelper
    {
        public static bool GetIsFileDragDropEnabled(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsFileDragDropEnabledProperty);
        }

        public static void SetIsFileDragDropEnabled(DependencyObject obj, bool value)
        {
            obj.SetValue(IsFileDragDropEnabledProperty, value);
        }

        public static bool GetFileDragDropTarget(DependencyObject obj)
        {
            return (bool)obj.GetValue(FileDragDropTargetProperty);
        }

        public static void SetFileDragDropTarget(DependencyObject obj, bool value)
        {
            obj.SetValue(FileDragDropTargetProperty, value);
        }

        public static readonly DependencyProperty IsFileDragDropEnabledProperty =
                DependencyProperty.RegisterAttached("IsFileDragDropEnabled", typeof(bool), typeof(FileDragDropHelper), new PropertyMetadata(OnFileDragDropEnabled));

        public static readonly DependencyProperty FileDragDropTargetProperty =
                DependencyProperty.RegisterAttached("FileDragDropTarget", typeof(object), typeof(FileDragDropHelper), null);

        private static void OnFileDragDropEnabled(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue == e.OldValue) return;
            var control = d as Control;
            if (control != null)
            {
                control.Drop += OnDrop;
                control.DragOver += OnDragOver;
                control.DragEnter += OnDragOver;
            }
        }

        private static void OnDragOver(object sender, DragEventArgs e)
        {
            DependencyObject d = sender as DependencyObject;
            if (d == null) return;
            Object target = d.GetValue(FileDragDropTargetProperty);
            IDragDropTarget fileTarget = target as IDragDropTarget;
            if (fileTarget != null)
            {
                if (e.Data.GetDataPresent(DataFormats.FileDrop))
                {
                   var filenames =((string[])e.Data.GetData(DataFormats.FileDrop));
                    foreach (var filename in filenames)
                    {
                        string c = Path.GetExtension(filename);
                        if (c == ".xls" || c == ".xlsm" || c == ".xlsx")
                            e.Effects = DragDropEffects.Move;
                        else
                        {
                            e.Effects = DragDropEffects.None;
                            break;
                        }
                       
                    }
                    e.Handled = true;
                }
            }  
                                
           
        }
        

        private static void OnDrop(object _sender, DragEventArgs _dragEventArgs)
        {
            DependencyObject d = _sender as DependencyObject;
            if (d == null) return;
            Object target = d.GetValue(FileDragDropTargetProperty);
            IDragDropTarget fileTarget = target as IDragDropTarget;
            if (fileTarget != null)
            {
                if (_dragEventArgs.Data.GetDataPresent(DataFormats.FileDrop))
                {
                    fileTarget.OnFileDrop((string[])_dragEventArgs.Data.GetData(DataFormats.FileDrop));
                }
                else if((_dragEventArgs.Data.GetDataPresent(DataFormats.Text)))
                {
                   fileTarget.OnTextDrop((string)_dragEventArgs.Data.GetData(DataFormats.Text));
                }
            }

            else
            {
                throw new Exception("FileDragDropTarget object must be of type IFileDragDropTarget");
            }
        }
    }
}
