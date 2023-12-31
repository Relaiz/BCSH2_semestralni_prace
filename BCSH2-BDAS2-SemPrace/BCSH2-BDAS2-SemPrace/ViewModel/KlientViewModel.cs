﻿using BCSH2_BDAS2_SemPrace.Commands;
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
        private long _ucetNumber;
        private string _ucetName;
        private string _ucetStatus;

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
        private Klient _currentKlient;

        public Klient CurrentKlient
        {
            get { return _currentKlient; }
            set
            {
                _currentKlient = value;
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
        private long UcetNumber
        {
            get { return _ucetNumber; }
            set
            {
                _ucetNumber = value;
                OnPropertyChanged(nameof(UcetNumber));
            }
        }

        private string UcetName
        {
            get { return _ucetName; }
            set
            {
                _ucetName = value;
                OnPropertyChanged(nameof(UcetName));
            }
        }

        private string UcetStatus
        {
            get { return _ucetStatus; }
            set
            {
                _ucetStatus = value;
                OnPropertyChanged(nameof(UcetStatus));
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

        private decimal _zustatek;
        public decimal Zustatek
        {
            get { return _zustatek; }
            set
            {
                _zustatek = value;
                OnPropertyChanged(nameof(Zustatek));
            }
        }

        private decimal _zustatekBlocked;
        public decimal ZustatekBlocked
        {
            get { return _zustatekBlocked; }
            set
            {
                _zustatekBlocked = value;
                OnPropertyChanged(nameof(ZustatekBlocked));
            }
        }

        private int _mesicniPlatby;
        public int MesicniPlatby
        {
            get { return _mesicniPlatby; }
            set
            {
                _mesicniPlatby = value;
                OnPropertyChanged(nameof(MesicniPlatby));
            }
        }
        public ICommand SjednatShuzkuCommand { get; }
        public ICommand TranzakceZUctuCommand { get; }
        public ICommand ZalozitNovyCommand { get; }
        public ICommand DetailUctuCommand { get; }
        public ICommand ExitKlientCommand { get; }
        public ICommand SaveDataCommand { get; }
        public ICommand VytvoritPlatbuCommand { get; }
        public ICommand SouboryCommand { get; }
        private readonly OracleDatabaseService db;
        public KlientViewModel(Klient klient)
        {
            db = new OracleDatabaseService();
            db.OpenConnection();

            _currentKlient = klient;
            listOfKlientUcty = new ObservableCollection<Ucet>();
            RefreshListView();
            FetchAndPopulateData();
            GetMonthlySpendings();
            TranzakceZUctuCommand = new RelayCommand(TranzakceZUctu);
            ZalozitNovyCommand = new RelayCommand(ZalozitNovy);
            DetailUctuCommand = new RelayCommand(DetailUctu);
            ExitKlientCommand = new RelayCommand(ExitKlient);
            VytvoritPlatbuCommand = new RelayCommand(VytvoritPlatbu);
            SaveDataCommand = new RelayCommand(SaveData);
            SouboryCommand = new RelayCommand(SouboryClick);
        }

        private void TranzakceZUctu(object parameter)
        {
            KlientTranzakceZUctuWindow tranzakceWindow = new KlientTranzakceZUctuWindow(selectedUcet);

            // Show the window
            tranzakceWindow.ShowDialog();
            RefreshListView();
        }

        private void ZalozitNovy(object parameter)
        {
            KlientZalozitUcetWindow zalozitUcetWindow = new KlientZalozitUcetWindow(_currentKlient.IdKlient);

            // Show the window
            zalozitUcetWindow.ShowDialog();
            RefreshListView();
        }

        // Fetch total Zustatek and Blokovana Castka for the entire Klient
        private void FetchTotalZustatkyForKlient()
        {
            decimal totalZustatek = 0;
            decimal totalBlockedCastka = 0;

            foreach (var ucet in ListOfKlientUcty)
            {
                decimal volnaCastka;
                decimal blockedCastka;

                // Fetch Zustatek for the Ucet
                FetchZustatekForUcet(ucet.IdUcet, out volnaCastka, out blockedCastka);

                // Sum up Zustatek and Blocked Castka
                totalZustatek += volnaCastka;
                totalBlockedCastka += blockedCastka;
            }

            // Set TotalZustatek and TotalBlockedCastka properties
            Zustatek = totalZustatek;
            ZustatekBlocked = totalBlockedCastka;
        }

        private void FetchZustatekForUcet(int ucetId, out decimal volnaCastka, out decimal blockedCastka)
        {
            // Call the GetZustatekForUcet function from OracleDatabaseService
            Zustatek zustatek = db.GetZustatekForUcet(ucetId);

            // Initialize output variables
            volnaCastka = 0;
            blockedCastka = 0;

            // Check if Zustatek is not null
            if (zustatek != null)
            {
                // Set the output variables
                volnaCastka = zustatek.VolnaCastka;
                blockedCastka = zustatek.BlokovaneCastka;
            }
        }

        private void DetailUctu(object parameter)
        {
            // Check if the selected Ucet is not null
            if (selectedUcet != null)
            {
                KlientDetailUctuWindow detailUctuWindow = new KlientDetailUctuWindow(selectedUcet, _currentKlient);

                detailUctuWindow.ShowDialog();
                RefreshListView();
            }
            else
            {
                // Show a message that no Ucet is selected
                MessageBox.Show("Prosim, vyberte ucet", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void SouboryClick(object parameter)
        {
            KlientOdesilaniSouboruWindow w = new KlientOdesilaniSouboruWindow(_currentKlient);

            w.ShowDialog();
            RefreshListView();
        }

        private void ExitKlient(object parameter)
        {
            LogoutLog(_currentKlient);
            LoginWindow loginWindow = new LoginWindow();

            // Close the current window
            Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive).Close();

            // Show the login window
            loginWindow.Show();
        }

        private void LogoutLog(Klient klient)
        {
            db.OpenConnection();
            string username = $"{klient.Jmeno} {klient.Prijmeni}";
            string tempQuery = "DELETE FROM successful_logins WHERE user_name = :username";

            using (OracleCommand tempCmd = new OracleCommand(tempQuery, db.Connection))
            {
                tempCmd.Parameters.Add("user_name", OracleDbType.Varchar2).Value = username;
                tempCmd.ExecuteNonQuery();
            }
            db.CloseConnection();
        }

        private void VytvoritPlatbu(object parameter)
        {
            // Create the KlientPlatbaWindow
            KlientPlatbaWindow klientPlatbaWindow = new KlientPlatbaWindow(_currentKlient, listOfKlientUcty);

            // Show the window as a dialog
            klientPlatbaWindow.ShowDialog();
            GetMonthlySpendings();
        }

        private List<Ucet> GetUctyForKlient(int klientId)
        {
            OracleDatabaseService db = new OracleDatabaseService();
            db.OpenConnection();
            List<Ucet> ucty = new List<Ucet>();

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

                // Populate the ObservableCollection with Ucty items
                while (reader.Read())
                {
                    Ucet ucet = new Ucet
                    {
                        IdUcet = Convert.ToInt32(reader["id_ucet"]),
                        CisloUctu = Convert.ToInt64(reader["cislo_uctu"]),
                        Nazev = reader["nazev"].ToString(),
                        KlientIdKlient = Convert.ToInt32(reader["klient_id_klient"]),
                        BankIdBank = Convert.ToInt32(reader["bank_id_bank"]),
                        StatusIdStatus = Convert.ToInt32(reader["status_id_status"])
                    };

                    ucty.Add(ucet);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Chyba populaci uctu pro klienta: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                // Return an empty ObservableCollection in case of an error
            }
            finally
            {
                // Close the database connection
                db.CloseConnection();

            }
            return ucty;
        }
        private Zamestnanec GetAssignedZamestnanec(int zameId)
        {
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
                    };

                    return assignedZamestnanec;
                }

                return null; // No Zamestnanec found for the given ID
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Chyba dostanuti Zamestnancu: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }
            finally
            {
                // Close the database connection
                db.CloseConnection();
            }
        }

        private void GetMonthlySpendings()
        {
            db.OpenConnection();
            try
            {
                OracleCommand cmd = db.Connection.CreateCommand();
                cmd.CommandText = "BEGIN :result := GetClientOperationsSum(:p_client_id); END;";
                cmd.CommandType = CommandType.Text;

                // Define the OUT parameter for the function result
                OracleParameter resultParam = new OracleParameter("result", OracleDbType.Decimal);
                resultParam.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(resultParam);

                // Set the value for the input parameter
                cmd.Parameters.Add("p_client_id", OracleDbType.Int32).Value = _currentKlient.IdKlient;

                // Execute the command
                cmd.ExecuteNonQuery();

                decimal resultDecimal = ((OracleDecimal)resultParam.Value).Value;
                int result = Convert.ToInt32(resultDecimal);

                // Now you can use 'result' as needed, for example, assign it to MesicniPlatby
                MesicniPlatby = result;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error getting monthly spendings: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                db.CloseConnection();
            }
        }

        private void SaveData(object parameter)
        {
            db.OpenConnection();
            try
            {
                // Call the PL/SQL procedure to update Klient data
                OracleCommand cmd = db.Connection.CreateCommand();
                cmd.CommandText = "UpdateKlientData";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("p_id_klient", OracleDbType.Int32).Value = _currentKlient.IdKlient;
                cmd.Parameters.Add("p_jmeno", OracleDbType.Varchar2).Value = Name;
                cmd.Parameters.Add("p_prijmeni", OracleDbType.Varchar2).Value = Surname;
                cmd.Parameters.Add("p_tc", OracleDbType.Varchar2).Value = Telephone;

                cmd.ExecuteNonQuery();

                // Refresh data after saving                
                db.CloseConnection();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Chyba ulozeni dat: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void FetchAndPopulateData()
        {
            Name = _currentKlient.Jmeno;
            Surname = _currentKlient.Prijmeni;
            Email = _currentKlient.KlientEmail;
            Telephone = _currentKlient.TelefoniCislo;

            Zamestnanec assignedZamestnanec = GetAssignedZamestnanec(_currentKlient.ZameIdZamestnanec);
            Zamestnanec = $"{assignedZamestnanec.Jmeno} {assignedZamestnanec.Prijmeni}";

            FetchTotalZustatkyForKlient();
        }

        private void RefreshListView()
        {
            List<Ucet> ucty = GetUctyForKlient(_currentKlient.IdKlient);
            FetchTotalZustatkyForKlient();
            GetMonthlySpendings();

            // Clear the existing items
            ListOfKlientUcty.Clear();

            // Add new items using foreach
            foreach (var ucet in ucty)
            {
                ListOfKlientUcty.Add(ucet);
            }

            OnPropertyChanged(nameof(ListOfKlientUcty));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }

}