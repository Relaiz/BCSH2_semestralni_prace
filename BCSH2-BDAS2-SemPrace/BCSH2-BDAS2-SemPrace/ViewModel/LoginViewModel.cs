using BCSH2_BDAS2_SemPrace.Commands;
using BCSH2_BDAS2_SemPrace.DataBase;
using BCSH2_BDAS2_SemPrace.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using BCSH2_BDAS2_SemPrace.View;
using System.Windows.Controls;
using Oracle.ManagedDataAccess.Client;

namespace BCSH2_BDAS2_SemPrace.ViewModel
{
    public class LoginViewModel : ViewModelBase
    {
        private OracleDatabaseService db;

        public event Action<Zamestnanec> SuccessfulLoginZamestnanec;
        public event Action<Klient> SuccessfulLoginKlient;
        private string _email;
        private string _password;
        

        public string Email
        {
            get => _email;
            set
            {
                _email = value;
                OnPropertyChanged(nameof(Email));
            }
        }

        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged(nameof(Password));
            }
        }

        public ICommand LoginCommand { get; }

        public LoginViewModel()
        {
            db = new OracleDatabaseService();
            db.OpenConnection();

            LoginCommand = new RelayCommand(Login, CanLogin);
        }

        private bool CanLogin(object parameter)
        {
            return !string.IsNullOrEmpty(_email) && !string.IsNullOrEmpty(_password);
        }

        private void Login(object parameter)
        {
            try
            {
                string email = Email;
                string password = Password;

                if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
                {
                    MessageBox.Show("Please enter both email and password.");
                    return;
                }

                bool isValid = db.CheckUserCredentials(email, password);

                if (isValid)
                {
                    OpenUserWindow(email);
                }
                else
                {
                    MessageBox.Show("Invalid email or password. Please try again.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        private void CloseLoginWindow()
        {
            Window loginWindow = Application.Current.Windows.OfType<Window>().SingleOrDefault(w => w.IsActive);
            if (loginWindow != null)
            {
                loginWindow.Close();
            }
        }

        private void OpenUserWindow(string userEmail)
        {
            Window newUserWindow = null;

            var zamestnanecInfo = GetZamestnanecInfo(userEmail);

            if (zamestnanecInfo.IsZamestnanec)
            {
                if (zamestnanecInfo.Pozice == "Admin")
                {
                    Zamestnanec zamestnanec = GetZamestnanecFromLogin(userEmail);

                    SuccessfulLoginZamestnanec?.Invoke(zamestnanec);

                    newUserWindow = new AdminWindow(zamestnanec);
                    AdminViewModel adminViewModel = new AdminViewModel(zamestnanec);
                    newUserWindow.DataContext= adminViewModel;
                    ZamLoginSuccess(zamestnanec);
                    CloseLoginWindow();
                    newUserWindow.Show();
                }

                else if(zamestnanecInfo.Pozice== "Banker")
                {
                    Zamestnanec zamestnanec = GetZamestnanecFromLogin(userEmail);

                    SuccessfulLoginZamestnanec?.Invoke(zamestnanec);
                    newUserWindow = new ZamestnanecWindow(zamestnanec, zamestnanecInfo.Pozice);
                    Console.WriteLine($"User {zamestnanec.Jmeno} {zamestnanec.Prijmeni} logged in successfully.");
                    ZamLoginSuccess(zamestnanec);
                    CloseLoginWindow();
                    newUserWindow.Show();
                    
                }
                else if  (zamestnanecInfo.Pozice == "Manazer")
                {
                    Zamestnanec zamestnanec = GetZamestnanecFromLogin(userEmail);
                    SuccessfulLoginZamestnanec?.Invoke(zamestnanec);
                    newUserWindow = new ManazerWindow(zamestnanec, zamestnanecInfo.Pozice);
                    Console.WriteLine($"User {zamestnanec.Jmeno} {zamestnanec.Prijmeni} logged in successfully.");
                    ZamLoginSuccess(zamestnanec);
                    CloseLoginWindow();
                    newUserWindow.Show();
                }

            }
            else if (IsKlientUser(userEmail))
            {
                Klient klient = GetKlientFromLogin(userEmail);
                SuccessfulLoginKlient?.Invoke(klient);
                newUserWindow = new KlientWindow(klient);
                Console.WriteLine($"User {klient.Jmeno} {klient.Prijmeni} logged in succesfully.");
                KlientLoginSuccess(klient);
                CloseLoginWindow();
                newUserWindow.Show();
            }
        }
        private void LogSuccessfulLogin(string username)
        {
            
            string tempQuery = "INSERT INTO successful_logins (user_name, log_time) VALUES (:username, SYSTIMESTAMP)";

            using (OracleCommand tempCmd = new OracleCommand(tempQuery, db.Connection))
            {
                tempCmd.Parameters.Add("user_name", OracleDbType.Varchar2).Value = username;
                tempCmd.ExecuteNonQuery();
            }
        }
        private void ZamLoginSuccess(Zamestnanec zamestnanec)
        {

            try
            {
                
                db.OpenConnection();
               
                string username = $"{zamestnanec.Jmeno} {zamestnanec.Prijmeni}";

                LogSuccessfulLogin(username);


             
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

        private void KlientLoginSuccess(Klient klient)
        {
            try
            {
                db.OpenConnection();

                string username = $"{klient.Jmeno} {klient.Prijmeni}";

                LogSuccessfulLogin(username);
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
        private bool IsAdminUser(string email)
        {
            string query = $"SELECT is_admin FROM login WHERE email = '{email}'";
            DataTable result = db.ExecuteQuery(query);

            return result.Rows.Count > 0 && Convert.ToInt32(result.Rows[0]["is_admin"]) == 1;
        }

        private bool IsKlientUser(string email)
        {
            string query = $"SELECT klient_id_klient FROM login WHERE email = '{email}'";
            DataTable result = db.ExecuteQuery(query);

            return result.Rows.Count > 0 && result.Rows[0]["klient_id_klient"] != DBNull.Value;
        }

        private bool IsZamestnanecUser(string email)
        {
            string query = $"SELECT zamestnanec_id_zamestnanec FROM login WHERE email = '{email}'";
            DataTable result = db.ExecuteQuery(query);

            return result.Rows.Count > 0 && result.Rows[0]["zamestnanec_id_zamestnanec"] != DBNull.Value;
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



        private Klient GetKlientFromLogin(string email)
        {
            string query = $"SELECT * FROM klient WHERE klient_email = '{email}'";
            DataTable result = db.ExecuteQuery(query);

            if (result.Rows.Count > 0)
            {
                Klient klient = new Klient
                {
                    IdKlient = Convert.ToInt32(result.Rows[0]["id_klient"]),
                    CisloPrukazu = Convert.ToInt32(result.Rows[0]["cislo_prukazu"]),
                    Jmeno= result.Rows[0]["jmeno"].ToString(),
                    Prijmeni = result.Rows[0]["prijmeni"].ToString(),
                    KlientEmail = result.Rows[0]["klient_email"].ToString(),                   
                    AdresaIdAdres = Convert.ToInt32(result.Rows[0]["adresa_id_adres"]),
                    BankIdBank = Convert.ToInt32(result.Rows[0]["bank_id_bank"]),
                    TelefoniCislo = result.Rows[0]["telefoni_cislo"].ToString(),
                    ZameIdZamestnanec = Convert.ToInt32(result.Rows[0]["zame_id_zamestnanec"]),
                    OdesiFileIdFile = result.Rows[0]["odesi_file_id_file"] as int?,
                    OdesFileIdKlient = result.Rows[0]["odes_file_id_klient"] as int?,
                    IdFile1 = result.Rows[0]["id_file1"] as int?,
                    IdKlient2 = result.Rows[0]["id_klient2"] as int?
                };

                return klient;
            }

            return null;
        }

        private Zamestnanec GetZamestnanecFromLogin(string email)
        {
            string query = $"SELECT * FROM zamestnanec WHERE email_zamestnanec = '{email}'";
            DataTable result = db.ExecuteQuery(query);

            if (result.Rows.Count > 0)
            {

                Zamestnanec zamestnanec = new Zamestnanec
                {
                    IdZamestnanec = Convert.ToInt32(result.Rows[0]["id_zamestnanec"]),
                    Jmeno = result.Rows[0]["jmeno"].ToString(),
                    Prijmeni = result.Rows[0]["prijmeni"].ToString(),
                    Mzda = Convert.ToDecimal(result.Rows[0]["mzda"]),
                    TelefoniCislo = result.Rows[0]["telefoni_cislo"].ToString(),
                    AdresaIdAdres = Convert.ToInt32(result.Rows[0]["adresa_id_adres"]),
                    BankIdBank = Convert.ToInt32(result.Rows[0]["bank_id_bank"]),
                    StatusIdStatus = Convert.ToInt32(result.Rows[0]["status_id_status"]),
                    ZamestnanecIdZamestnanec1= Convert.ToInt32(result.Rows[0]["zamestnanec_id_zamestnanec1"]),
                    PobockaIdPobocka= result.Rows[0]["pobocka_id_pobocka"] as int?,
                    PracePoziceIdPozice = Convert.ToInt32(result.Rows[0]["prace_pozice_id_pozice"]),
                    EmailZamestnanec = result.Rows[0]["email_zamestnanec"].ToString(),
                    OdesilaniFileIdFile = result.Rows[0]["odesilani_file_id_file"] as int?,
                    OdesilaniFileIdKlient = result.Rows[0]["odesilani_file_id_klient"] as int?,
                    IdFile1= result.Rows[0]["id_file1"] as int?,
                    IdKlient1= result.Rows[0]["id_klient1"] as int?
                };

                return zamestnanec;
            }

            return null;
        }
        private void HandleSuccessfulLoginZamestnanec(Zamestnanec loggedInUser)
        {           
            Console.WriteLine($"User {loggedInUser.Jmeno} logged in successfully.");
            CloseLoginWindow();
        }
    }
}