using BCSH2_BDAS2_SemPrace.Commands;
using BCSH2_BDAS2_SemPrace.DataBase;
using BCSH2_BDAS2_SemPrace.Model;
using BCSH2_BDAS2_SemPrace.View;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace BCSH2_BDAS2_SemPrace.ViewModel
{
    public class ZamestnanecViewModel : INotifyPropertyChanged
    {
        private string _name;
        private string _lastname;
        private string _telCislo;
        private string _email;
        private string _pozice;
        private string _pobocka;
        private string _status;
        private Zamestnanec _currentZamestnanec;
        private ObservableCollection<Klient> listOfKlients;

        public ObservableCollection<Klient> ListOfKlients
        {
            get { return listOfKlients; }
            set
            {
                listOfKlients = value;
                OnPropertyChanged(nameof(ListOfKlients));
            }
        }
        public string Pozice
        {
            get => _pozice;
            set
            {
                _pozice = value;
                OnPropertyChanged(nameof(Pozice));
            }
        }

        public string Pobocka
        {
            get => _pobocka;
            set
            {
                _pobocka = value;
                OnPropertyChanged(nameof(Pobocka));
            }
        }

        public string Status
        {
            get => _status;
            set
            {
                _status = value;
                OnPropertyChanged(nameof(Status));
            }
        }

        public string Email
        {
            get => _email;
            set
            {
                _email = value;
                OnPropertyChanged(nameof(Email));
            }
        }
        public string TelCislo
        {
            get => _telCislo;
            set
            {
                _telCislo = value;
                OnPropertyChanged(nameof(TelCislo));
            }
        }

        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        public string Lastname
        {
            get => _lastname;
            set
            {
                _lastname = value;
                OnPropertyChanged(nameof(Lastname));
            }
        }

        public string FullName => $"{Name} {Lastname}";
       

        private Klient selectedKlient;
        public Klient SelectedKlient
        {
            get { return selectedKlient; }
            set
            {
                selectedKlient = value;
                OnPropertyChanged(nameof(SelectedKlient));
            }
        }
        public Zamestnanec CurrentZamestnanec
        {
            get { return _currentZamestnanec; }
            set
            {
                _currentZamestnanec = value;
                OnPropertyChanged(nameof(CurrentZamestnanec));
            }
        }

        
        public ICommand ShowKlientCommand { get; }
        public ICommand ExitZamestnanecCommand { get; }
        private readonly OracleDatabaseService db;

        public ZamestnanecViewModel(Zamestnanec zamestnanec, String Pozice)
        {
            // LoadZamestnanciCommand = new RelayCommand(LoadZamestnanci);

            db = new OracleDatabaseService();
            db.OpenConnection();

            CurrentZamestnanec = zamestnanec;
            GetPobockaName(CurrentZamestnanec.IdZamestnanec);
            GetStatus(CurrentZamestnanec.IdZamestnanec);
            
            _name = zamestnanec.Jmeno;
            _lastname = zamestnanec.Prijmeni;
            _email = zamestnanec.EmailZamestnanec;
            _telCislo = zamestnanec.TelefoniCislo;
            _pozice = Pozice;

            listOfKlients = new ObservableCollection<Klient>();
            listOfKlients.Add(SelectedKlient);
            PopulateKlientsList();
            ExitZamestnanecCommand = new RelayCommand(Exit);
            ShowKlientCommand = new RelayCommand(ShowKlient);
        }

        

        private void EditKlient(object parameter)
        {

        }

        private void ShowKlient(object parameter)
        {
            if (SelectedKlient != null)
            {
                string jmeno = SelectedKlient.Jmeno;
                string prijmeni = SelectedKlient.Prijmeni;
                Klient klient = db.GetKlientByJmenoPrijmeni(jmeno, prijmeni);


                ShowKlientWindow showKlientWindow = new ShowKlientWindow(klient);
                showKlientWindow.DataContext = klient;
                showKlientWindow.Show();
            }

        }

        private void Exit(object parameter)
        {
            LoginWindow loginWindow = new LoginWindow();

            // Close the current window
            Window currentWindow = Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);

            zamExit(CurrentZamestnanec);
            // Закрываем текущее окно
            currentWindow?.Close();

            // Show the login window
            loginWindow.Show();
        }
        private void zamExit(Zamestnanec zamestnanec)
        {
            try
            { 
            string tableName = "zamestnanec";
            string operation = "exit from account";
            DateTime currentTime = DateTime.Now;
            string username = $"{zamestnanec.Jmeno} {zamestnanec.Prijmeni}";



            string query = "INSERT INTO log_table (tabulka, operace, cas, uzivatel) " +
                              "VALUES (:tabulka, :operace, :cas, :uzivatel)";

            using (OracleCommand cmd = new OracleCommand(query, db.Connection))
            {
                cmd.Parameters.Add("tabulka", OracleDbType.Varchar2).Value = tableName;
                cmd.Parameters.Add("operace", OracleDbType.Varchar2).Value = operation;
                cmd.Parameters.Add("cas", OracleDbType.TimeStampLTZ).Value = currentTime;
                cmd.Parameters.Add("uzivatel", OracleDbType.Varchar2).Value = username;

                int rowsAffected = cmd.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    Console.WriteLine("Success add in  log_table.");
                }
                else
                {
                    Console.WriteLine("Error add.");
                }
            }
        }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                db.CloseConnection();
            }
        }

        private void LoadZamestnanci(object parameter)
        {
            // Используйте _databaseService для выполнения запроса к базе данных Oracle и обновите Zamestnanci
            string query = "SELECT * FROM zamestnanec";
            DataTable result = db.ExecuteQuery(query);

            // Преобразуйте результат в ObservableCollection<ZamestnanecModel> и обновите Zamestnanci
            // ...
        }

        // Добавьте другие методы и обработчики команд по необходимости

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void GetPobockaName(decimal id_zamestnanec)
        {
            
            db.OpenConnection();

            OracleCommand cmd = db.Connection.CreateCommand();  // Создаем команду открытого соединения
            cmd.CommandText = "SELECT f.nazev FROM pobocka f WHERE f.id_pobocka= (SELECT z.pobocka_id_pobocka FROM zamestnanec z WHERE z.id_zamestnanec = :id_zamestnanec)";
            cmd.Parameters.Add("id_zamestnanec", OracleDbType.Decimal).Value = id_zamestnanec;

            try
            {
                OracleDataReader reader = cmd.ExecuteReader();

                string pobockaName = "";

                if (reader.Read())
                {
                    pobockaName = reader["nazev"].ToString();
                }

                reader.Close();  // Закрываем DataReader

                _pobocka= pobockaName;
            }
            finally
            {
                db.CloseConnection();
            }
        }

        private void GetStatus(decimal id_zamestnanec)
        {
            try
            {
                
            db.OpenConnection();

            OracleCommand cmd = db.Connection.CreateCommand();


            cmd.CommandText = "SELECT f.popis FROM status f WHERE f.id_status= (SELECT z.status_id_status FROM zamestnanec z WHERE z.id_zamestnanec = :id_zamestnanec)";
            cmd.Parameters.Add("id_zamestnanec", OracleDbType.Decimal).Value = id_zamestnanec;

            

                OracleDataReader reader = cmd.ExecuteReader();

                string status = "";

                if (reader.Read())
                {
                    status = reader["popis"].ToString();
                }
                reader.Close();

                _status = status;
            }
            finally
            {
                db.CloseConnection();
            }

        }

        private void PopulateKlientsList()
        {
           try
           { 
            db.OpenConnection();

            OracleCommand cmd = db.Connection.CreateCommand();
            






                // Replace 'your_employee_id' with the actual employee ID
                int zamId = _currentZamestnanec.IdZamestnanec;

                List<Klient> clients = db.GetHierarchyInfoFromDatabase(zamId);


                // Clear existing items in the ListView
                listOfKlients.Clear();

                // Add new items to the ListView


                foreach (Klient klient in clients)
                {
                    listOfKlients.Add(klient);
                }
                



            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error populating klients list: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                // Close the database connection
                db.CloseConnection();
            }
        }


    }
}
