using BCSH2_BDAS2_SemPrace.Commands;
using BCSH2_BDAS2_SemPrace.DataBase;
using BCSH2_BDAS2_SemPrace.Model;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace BCSH2_BDAS2_SemPrace.ViewModel
{
    public class KlientPlatbaViewModel : ViewModelBase
    {
        private Ucet _selectedUcet;
        private string _ucetNumber;
        private decimal _castka;
        private string _nazev;

        public Ucet SelectedUcet
        {
            get { return _selectedUcet; }
            set { _selectedUcet = value; OnPropertyChanged(nameof(SelectedUcet)); }
        }

        public string UcetNumber
        {
            get { return _ucetNumber; }
            set { _ucetNumber = value; OnPropertyChanged(nameof(UcetNumber)); }
        }

        public decimal Castka
        {
            get { return _castka; }
            set { _castka = value; OnPropertyChanged(nameof(Castka)); }
        }

        public string Nazev
        {
            get { return _nazev; }
            set { _nazev = value; OnPropertyChanged(nameof(Nazev)); }
        }

        private readonly OracleDatabaseService db;
        private readonly Klient _currentKlient;

        public KlientPlatbaViewModel(Klient currentKlient, ObservableCollection<Ucet> uctyList)
        {
            db = new OracleDatabaseService();
            db.OpenConnection();
            _currentKlient = currentKlient;
            UctyList = uctyList;
            VytvoritPlatbuCommand = new RelayCommand(VytvoritPlatbu);
        }

        public ObservableCollection<Ucet> UctyList { get; set; }

        public ICommand VytvoritPlatbuCommand { get; }

        private void VytvoritPlatbu(object parameter)
        {
            // Check if the selected Ucet is not null
            if (SelectedUcet != null)
            {
                try
                {
                    // Call the stored procedure to create a payment
                    OracleCommand cmd = db.Connection.CreateCommand();
                    cmd.CommandText = "CreatePlatba";
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Set the parameters
                    cmd.Parameters.Add("p_z_ucet_id", OracleDbType.Int32).Value = SelectedUcet.CisloUctu;
                    cmd.Parameters.Add("p_do_ucet_id", OracleDbType.Int32).Value = UcetNumber;
                    cmd.Parameters.Add("p_nazev", OracleDbType.Varchar2).Value = Nazev;
                    cmd.Parameters.Add("p_status_id", OracleDbType.Int32).Value = 9;
                    cmd.Parameters.Add("p_castka", OracleDbType.Decimal).Value = Castka;
                    cmd.Parameters.Add("p_ucet_id_ucet", OracleDbType.Int32).Value = SelectedUcet.IdUcet;

                    // Execute the stored procedure
                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Platba vytvořena!", "Úspěch", MessageBoxButton.OK, MessageBoxImage.Information);
                    Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive)?.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error creating payment: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}