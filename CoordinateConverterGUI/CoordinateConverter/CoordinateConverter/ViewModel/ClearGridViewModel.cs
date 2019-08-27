using DevExpress.Mvvm;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows;

namespace CoordinateConverter.ViewModel
{
    public class ClearGridViewModel
    {
        private readonly UICommand registerCommand;

        private readonly UICommand cancelCommand;

        public bool ClearCheck
        {
            get => Properties.Settings.Default.ClearCheck;
            set => SetSettings(value);
        }
       
        public ClearGridViewModel()
        {
            registerCommand =  new UICommand()
            {
                Caption = "Да",
                IsDefault = true,
                Command = new DelegateCommand(() =>
                {
                    Properties.Settings.Default.ClearRule = true;
                    Properties.Settings.Default.Save();
                })
            };

            cancelCommand = new UICommand()
            {
                Caption = "Нет",
                IsCancel = true,
                Command = new DelegateCommand(() =>
                {
                    Properties.Settings.Default.ClearRule = false;
                    Properties.Settings.Default.Save();
                })
            };
        }

        private void SetSettings(bool value)
        {
            try
            {
                Properties.Settings.Default.ClearCheck = value;
                Properties.Settings.Default.Save();
               
            }
            catch 
            {               
                MessageBox.Show(
                  "Произошла ошибка в работе программы",
                  "Ошибка",
                  MessageBoxButton.OK,
                  MessageBoxImage.Error
                  );
                return;
            }            
        }

        public IEnumerable<UICommand> GetCommands()
        {
            return new List<UICommand> { registerCommand, cancelCommand };
        }
    }
}
