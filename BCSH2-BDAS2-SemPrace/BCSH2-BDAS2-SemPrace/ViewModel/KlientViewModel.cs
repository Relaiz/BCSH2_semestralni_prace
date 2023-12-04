using BCSH2_BDAS2_SemPrace.Commands;
using BCSH2_BDAS2_SemPrace.DataBase;
using BCSH2_BDAS2_SemPrace.Model;
using BCSH2_BDAS2_SemPrace.View;
using Oracle.ManagedDataAccess.Client;
using System.Collections.Generic;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using Oracle.ManagedDataAccess.Types;

namespace BCSH2_BDAS2_SemPrace.ViewModel
{
    public class KlientViewModel : INotifyPropertyChanged
    {

        private string _name;
        private string _surname;
        private string _email;
        private string _zamestnanec;
        private string _telephone;

        private ObservableCollection<Ucet> listOfKlientUcty;
        public ObservableCollection<Ucet> ListOfKlientUcty
        {
            get { return listOfKlientUcty; }
            set
            {
                listOfKlientUcty = value;
                OnPropertyChanged(nameof(ListOfKlientUcty));
            }
        }
        private Klient currentKlient;

        public Klient CurrentKlient
        {
            get { return currentKlient; }
            set
            {
                currentKlient = value;
                OnPropertyChanged(nameof(CurrentKlient));
            }
        }
        private Ucet selectedUcet;
        public Ucet SelectedUcet
        {
            get { return selectedUcet; }
            set
            {
                selectedUcet = value;
                OnPropertyChanged(nameof(SelectedUcet));
            }
        }

        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
                OnPropertyChanged(nameof(FullName));
            }
        }

        public string Surname
        {
            get { return _surname; }
            set
            {
                _surname = value;
                OnPropertyChanged(nameof(Surname));
                OnPropertyChanged(nameof(FullName));
            }
        }
        public string FullName => $"{Name} {Surname}";

        public string Email
        {
            get { return _email; }
            set
            {
                _email = value;
                OnPropertyChanged(nameof(Email));
            }
        }

        public string Zamestnanec
        {
            get { return _zamestnanec; }
            set
            {
                _zamestnanec = value;
                OnPropertyChanged(nameof(Zamestnanec));
            }
        }

        public string Telephone
        {
            get { return _telephone; }
            set
            {
                _telephone = value;
                OnPropertyChanged(nameof(Telephone));
            }
        }

        public ICommand TranzakceZUctuCommand { get; }
        public ICommand ZalozitNovyCommand { get; }
        public ICommand DetailUctuCommand { get; }
        public ICommand ExitKlientCommand { get; }
        private readonly OracleDatabaseService db;



        public KlientViewModel(Klient klient)
        {
            db = new OracleDatabaseService();
            db.OpenConnection();

            CurrentKlient = klient;
            ListOfKlientUcty = new ObservableCollection<Ucet>();
            //PopulateUctyListForKlient(klient.IdKlient);
            FetchAndPopulateData();

            // Initialize your commands
            TranzakceZUctuCommand = new RelayCommand(TranzakceZUctu);
            ZalozitNovyCommand = new RelayCommand(ZalozitNovy);
            DetailUctuCommand = new RelayCommand(DetailUctu);
            ExitKlientCommand = new RelayCommand(ExitKlient);
        }

        private void TranzakceZUctu(object parameter)
        {
            // Implement logic for Tranzakce Z Uctu command
            // Open a new window or perform any other action
        }

        private void ZalozitNovy(object parameter)
        {
            // Create a new instance of KlientZalozitUcetWindow
            KlientZalozitUcetWindow zalozitUcetWindow = new KlientZalozitUcetWindow(currentKlient.IdKlient);

            // Show the window
            zalozitUcetWindow.ShowDialog();
        }


        private void DetailUctu(object parameter)
        {
            // Implement logic for Detail Uctu command
            // Open a new window or perform any other action
        }

        private void ExitKlient(object parameter)
        {
            LoginWindow loginWindow = new LoginWindow();

            // Close the current window
            Window currentWindow = Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);

           // zamExit(CurrentZamestnanec);
            // Закрываем текущее окно
            currentWindow?.Close();

            // Show the login window
            loginWindow.Show();
        }

        private ObservableCollection<Ucet> PopulateUctyListForKlient(int klientId)
        {
            OracleDatabaseService db = new OracleDatabaseService();
            db.OpenConnection();

            OracleCommand cmd = db.Connection.CreateCommand();
            try
            {
                // Call the PL/SQL procedure to get Ucty for the specified Klient ID
                cmd.CommandText = "GetUctyForKlient";
                cmd.CommandType = CommandType.StoredProcedure;

                // Add Klient ID parameter
                cmd.Parameters.Add("p_klient_id", OracleDbType.Int32).Value = klientId;

                // Add output cursor parameter
                cmd.Parameters.Add("p_cursor", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

                // Execute the procedure
                cmd.ExecuteNonQuery();

                // Retrieve Ucty information from the output cursor
                OracleRefCursor refCursor = (OracleRefCursor)cmd.Parameters["p_cursor"].Value;
                OracleDataReader reader = refCursor.GetDataReader();

                // Create a list to store Ucty items
                ObservableCollection<Ucet> ucty = new ObservableCollection<Ucet>();

                // Populate the ObservableCollection with Ucty items
                while (reader.Read())
                {
                    Ucet ucet = new Ucet
                    {
                        IdUcet = Convert.ToInt32(reader["id_ucet"]),
                        CisloUctu = Convert.ToInt32(reader["cislo_uctu"]),
                        Nazev = reader["nazev"].ToString(),
                        KlientIdKlient = Convert.ToInt32(reader["klient_id_klient"]),
                        BankIdBank = Convert.ToInt32(reader["bank_id_bank"]),
                        StatusIdStatus = Convert.ToInt32(reader["status_id_status"])
                        // Set other properties based on your Ucet class
                    };

                    ucty.Add(ucet);
                }

                return ucty;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error populating Ucty list for Klient: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return new ObservableCollection<Ucet>(); // Return an empty ObservableCollection in case of an error
            }
            finally
            {
                // Close the database connection
                db.CloseConnection();
            }
        }

        private Zamestnanec GetAssignedZamestnanec(int zameId)
        {
            OracleDatabaseService db = new OracleDatabaseService();
            db.OpenConnection();

            OracleCommand cmd = db.Connection.CreateCommand();
            try
            {
                string query = $"SELECT * FROM Zamestnanec WHERE Id_Zamestnanec = :zameId";
                cmd.CommandText = query;
                cmd.Parameters.Add("zameId", OracleDbType.Int32).Value = zameId;

                OracleDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    Zamestnanec assignedZamestnanec = new Zamestnanec
                    {
                        Jmeno = reader["jmeno"].ToString(),
                        Prijmeni = reader["prijmeni"].ToString(),
                        // Add other properties as needed
                    };

                    return assignedZamestnanec;
                }

                return null; // No Zamestnanec found for the given ID
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error fetching Zamestnanec: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }
            finally
            {
                // Close the database connection
                db.CloseConnection();
            }
        }
        private void FetchAndPopulateData()
        {
            // 1) Fetch and populate data for the Klient
            Name = currentKlient.Jmeno;
            Surname = currentKlient.Prijmeni;
            Email = currentKlient.KlientEmail;
            Telephone = currentKlient.TelefoniCislo;

            // 2) Fetch assigned worker's name and surname
            Zamestnanec assignedZamestnanec = GetAssignedZamestnanec(currentKlient.ZameIdZamestnanec);
            Zamestnanec = $"{assignedZamestnanec.Jmeno} {assignedZamestnanec.Prijmeni}";

            // You might need to add error handling if needed
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public event EventHandler UpdateAccountsListRequested;

        private void OnUpdateAccountsListRequested()
        {
            UpdateAccountsListRequested?.Invoke(this, EventArgs.Empty);
        }
    }
}