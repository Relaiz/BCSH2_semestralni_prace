﻿using System;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using BCSH2_BDAS2_SemPrace.Commands;
using BCSH2_BDAS2_SemPrace.DataBase;

namespace BCSH2_BDAS2_SemPrace.ViewModel
{
    public class KlientZalozitUcetViewModel : INotifyPropertyChanged
    {
        private string newAccountName;

        public string NewAccountName
        {
            get { return newAccountName; }
            set
            {
                newAccountName = value;
                OnPropertyChanged(nameof(NewAccountName));
            }
        }

        public ICommand UlozCommand { get; }
        public ICommand StornoCommand { get; }

        private readonly OracleDatabaseService db;
        private readonly int klientId;  // Pass the klientId from the main window

        public KlientZalozitUcetViewModel(int klientId)
        {
            db = new OracleDatabaseService();
            db.OpenConnection();
            this.klientId = klientId;
            NewAccountName = "Bezny ucet";
            UlozCommand = new RelayCommand(Uloz);
            StornoCommand = new RelayCommand(Storno);

        }

        private void Uloz(object parameter)
        {
            try
            {
                if (NewAccountName != null)
                {
                    db.CreateNewAccount(klientId, NewAccountName);

                    MessageBoxResult result = MessageBox.Show("Novy ucet uspesne zalozen.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    if (result == MessageBoxResult.OK)
                    {
                        // Закрываем текущее окно
                        Window actualnWindow = Application.Current.Windows.OfType<Window>().SingleOrDefault(w => w.IsActive);

                        if (actualnWindow != null)
                        {
                            // Закрываем текущее окно
                            actualnWindow.Close();
                        }
                    }
                }
                else {
                    MessageBox.Show("Nazev uctu nesmi byt prazdny!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Chyba zalozeni uctu: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                db.CloseConnection();
            }
        }

        private void Storno(object parameter)
        {
            // Close the window or perform any other action
            Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive)?.Close();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
