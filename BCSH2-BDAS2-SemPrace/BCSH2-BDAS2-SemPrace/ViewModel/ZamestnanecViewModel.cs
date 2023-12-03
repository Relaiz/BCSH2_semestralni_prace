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

        public ZamestnanecViewModel(Zamestnanec zamestnanec)
        {
            // LoadZamestnanciCommand = new RelayCommand(LoadZamestnanci);

            db = new OracleDatabaseService();
            db.OpenConnection();

            CurrentZamestnanec = zamestnanec;
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


      
    }
}
