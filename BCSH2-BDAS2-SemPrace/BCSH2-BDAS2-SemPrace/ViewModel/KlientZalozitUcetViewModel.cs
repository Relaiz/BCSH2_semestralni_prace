using System;
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
        private readonly int klientId;  

        public KlientZalozitUcetViewModel(int klientId)
        {
            db = new OracleDatabaseService();
            db.OpenConnection();
            this.klientId = klientId;

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

                    MessageBox.Show("New account created successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive)?.Close();
                }
                else {
                    MessageBox.Show("Name can't be empty!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error creating new account: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                db.CloseConnection();
            }
        }

        private void Storno(object parameter)
        {
            Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive)?.Close();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
