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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace BCSH2_BDAS2_SemPrace.ViewModel
{
    public class AdminViewModel : INotifyPropertyChanged
    {
        

        public ICommand OpenKlientWindowCommand { get; private set; }
        public ICommand OpenZamestnanecWindowCommand { get; private set; }
        public ICommand AddBankomatCommand { get; private set; }
        public ICommand AddPobockaCommand { get; private set; }
        public ICommand ExitCommand { get; private set; }
        private Klient _selectedKlient;
        private Zamestnanec _selectedZamestnanec;
        private readonly OracleDatabaseService db;
        public ObservableCollection<Klient> Klienti { get; set; }
        public ObservableCollection<Zamestnanec> Zamestnanci { get; set; }
        private Zamestnanec _admin;

        public Klient SelectedKlient
        {
            get => _selectedKlient;
            set
            {
                _selectedKlient = value;
                OnPropertyChanged(nameof(SelectedKlient));
            }
        }

        public Zamestnanec SelectedZamestnanec
        {
            get => _selectedZamestnanec;
            set
            {
                _selectedZamestnanec = value;
                OnPropertyChanged(nameof(SelectedZamestnanec));
            }
        }
        public Zamestnanec Admin
        {
            get => _admin;
            set
            {
                _admin = value;
                OnPropertyChanged(nameof(Admin));
            }
        }
        public AdminViewModel(Zamestnanec admin)
        {
            db = new OracleDatabaseService();
            db.OpenConnection();
            _admin = new Zamestnanec();
            Admin=admin;
            Klienti = new ObservableCollection<Klient>();
            Zamestnanci = new ObservableCollection<Zamestnanec>();
            LoadKlienty();
            LoadZamestnanci();
            OpenKlientWindowCommand = new RelayCommand(OpenKlientWindow);
            OpenZamestnanecWindowCommand = new RelayCommand(OpenZamestnanecWindow);
            AddBankomatCommand = new RelayCommand(AddBankomat);
            AddPobockaCommand = new RelayCommand(AddPobocka);
            ExitCommand = new RelayCommand(ExitKlient);
            

            // Загрузка данных в списки
        }
        private void LoadKlienty()
        {
            try
            {
                db.OpenConnection();
                Klient klient = null;
                string query = "SELECT id_klient,cislo_prukazu,jmeno,prijmeni,klient_email,telefoni_cislo,zame_id_zamestnanec, adresa FROM klient_info";

                using (OracleCommand cmd = new OracleCommand(query, db.Connection))
                {
                    using (OracleDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            klient = new Klient
                            {
                                IdKlient = Convert.ToInt32(reader["id_klient"]),
                                CisloPrukazu = Convert.ToInt64(reader["cislo_prukazu"]),
                                Jmeno = reader["jmeno"].ToString(),
                                Prijmeni = reader["prijmeni"].ToString(),
                                KlientEmail = reader["klient_email"].ToString(),
                                TelefoniCislo= reader["telefoni_cislo"].ToString(),
                                ZameIdZamestnanec = Convert.ToInt32(reader["zame_id_zamestnanec"]),
                                Adresa =reader["adresa"].ToString()


                            };
                            Klienti.Add(klient);
                        }
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

        private void LoadZamestnanci()
        {
            try
            {
                db.OpenConnection();
                Zamestnanec zamestnanec = null;
                string query = "SELECT id_zamestnanec,jmeno, prijmeni,adresa_id_adres,zamestnanec_id_zamestnanec1, status_id_status, pobocka_id_pobocka,telefoni_cislo,email_zamestnanec, adresa FROM zamestnanec_info";

                using (OracleCommand cmd = new OracleCommand(query, db.Connection))
                {
                    using (OracleDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            zamestnanec = new Zamestnanec
                            {
                                IdZamestnanec = Convert.ToInt32(reader["id_zamestnanec"]),                               
                                Jmeno = reader["jmeno"].ToString(),
                                Prijmeni = reader["prijmeni"].ToString(),
                                AdresaIdAdres = Convert.ToInt32(reader["adresa_id_adres"]),
                                ZamestnanecIdZamestnanec1= Convert.ToInt32(reader["zamestnanec_id_zamestnanec1"]),
                                StatusIdStatus= Convert.ToInt32(reader["status_id_status"]),
                                PobockaIdPobocka = Convert.ToInt32(reader["pobocka_id_pobocka"]),
                                EmailZamestnanec = reader["email_zamestnanec"].ToString(),
                                TelefoniCislo = reader["telefoni_cislo"].ToString(),
                                Adresa = reader["adresa"].ToString()

                            };
                            Zamestnanci.Add(zamestnanec);
                        }
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

        private void OpenKlientWindow(object parameter)
        {
            if (SelectedKlient != null)
            {
                KlientWindow klientWindow = new KlientWindow(SelectedKlient);
                KlientViewModel klientViewModel = new KlientViewModel(SelectedKlient);
                klientWindow.DataContext = klientViewModel;
                klientWindow.ShowDialog();
                LoadKlienty();
            }
        }

        private void OpenZamestnanecWindow(object parameter)
        {
            if (SelectedZamestnanec != null)
            {

                var zamestnanecInfo = GetZamestnanecInfo(SelectedZamestnanec.EmailZamestnanec);
                string pozice = "";

                if (zamestnanecInfo.Pozice == "Banker")
                {
                    pozice = zamestnanecInfo.Pozice;
                    ZamestnanecWindow zamestnanecWindow = new ZamestnanecWindow(SelectedZamestnanec, pozice);
                    ZamestnanecViewModel zamestnanecViewModel = new ZamestnanecViewModel(SelectedZamestnanec,pozice);
                    zamestnanecWindow.DataContext = zamestnanecViewModel;
                    zamestnanecWindow.ShowDialog();
                }
                if (zamestnanecInfo.Pozice == "Manazer")
                {
                    pozice = zamestnanecInfo.Pozice;
                    ManazerWindow manazerWindow = new ManazerWindow(SelectedZamestnanec, pozice);
                    ManazerViewModel manazerViewModel = new ManazerViewModel(SelectedZamestnanec, pozice);
                    manazerWindow.DataContext = manazerViewModel;
                    manazerWindow.ShowDialog();
                }
                
                LoadZamestnanci();
            }
        }
        private (bool IsZamestnanec, string Pozice) GetZamestnanecInfo(string email)
        {
            string query = $"SELECT z.id_zamestnanec, p.pozice_nazev " +
                           $"FROM zamestnanec z " +
                           $"JOIN prace_pozice p ON z.prace_pozice_id_pozice = p.id_pozice " +
                           $"WHERE z.email_zamestnanec = '{email}'";

            DataTable result = db.ExecuteQuery(query);

            if (result.Rows.Count > 0)
            {
                string pozice = result.Rows[0]["pozice_nazev"].ToString();
                return (true, pozice);
            }

            return (false, null);
        }
        private void AddBankomat(object parameter)
        {
            BankomatyWindow bankomatyWindow = new BankomatyWindow(Admin);
            BankomatViewModel bankomatViewModel = new BankomatViewModel(Admin);
            bankomatyWindow.DataContext = bankomatViewModel;
            bankomatyWindow.ShowDialog();
        }

        private void AddPobocka(object parameter)
        {
            PobockyWindov pobockyWindov = new PobockyWindov();
            PobockyViewModel pobockyViewModel = new PobockyViewModel(Admin);
            pobockyWindov.DataContext = pobockyViewModel;
            pobockyWindov.ShowDialog();

        }
        private void ExitKlient(object parameter)
        {
            LogoutLog(_admin);
            LoginWindow loginWindow = new LoginWindow();

            // Close the current window
            Application.Current.Windows.OfType<System.Windows.Window>().SingleOrDefault(x => x.IsActive).Close();

            // Show the login window
            loginWindow.Show();
        }

        private void LogoutLog(Zamestnanec zamestnanec)
        {
            db.OpenConnection();
            string username = $"{zamestnanec.Jmeno} {zamestnanec.Prijmeni}";
            string tempQuery = "DELETE FROM successful_logins WHERE user_name = :username";

            using (OracleCommand tempCmd = new OracleCommand(tempQuery, db.Connection))
            {
                tempCmd.Parameters.Add("user_name", OracleDbType.Varchar2).Value = username;
                tempCmd.ExecuteNonQuery();
            }
            db.CloseConnection();
        }
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}