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
            // Находим главное окно
            Window loginWindow = Application.Current.Windows.OfType<Window>().SingleOrDefault(w => w.IsActive);

            // Проверяем, является ли текущее окно главным окном
            if (loginWindow != null)
            {
                // Закрываем текущее окно входа
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
                    newUserWindow = new ZamestnanecWindow(zamestnanec,zamestnanecInfo.Pozice);
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
                    newUserWindow = new ZamestnanecWindow(zamestnanec, zamestnanecInfo.Pozice);
                }

            }
            else if (IsKlientUser(userEmail))
            {
                Klient klient = GetKlientFromLogin(userEmail);
                SuccessfulLoginKlient?.Invoke(klient);
                newUserWindow = new KlientWindow(klient);
                Console.WriteLine($"User {klient.Jmeno} {klient.Prijmeni} logged in succesfully.");
                CloseLoginWindow();
                newUserWindow.Show();
            }
        }

        private void ZamLoginSuccess(Zamestnanec zamestnanec)
        {

            try
            {
                
                db.OpenConnection();
                
                string tableName = "zamestnanec";
                string operation = "successful_login";
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

        private void KlientLoginSuccess(Klient klient)
        {

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
                // Assuming you have a Klient class with appropriate properties
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
                   // StatusIdStatus = Convert.ToInt32(result.Rows[0]["status_id_status"]),
                    ZameIdZamestnanec = Convert.ToInt32(result.Rows[0]["zame_id_zamestnanec"]),

                    OdesiFileIdFile = result.Rows[0]["odesi_file_id_file"] as int?,
                    OdesFileIdKlient = result.Rows[0]["odes_file_id_klient"] as int?,
                    IdFile1 = result.Rows[0]["id_file1"] as int?,
                    IdKlient2 = result.Rows[0]["id_klient2"] as int?
                    // Set other properties based on your Zamestnanec class
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
                // Assuming you have a Zamestnanec class with appropriate properties
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
                    // Set other properties based on your Zamestnanec class
                };

                return zamestnanec;
            }

            return null;
        }
        private void HandleSuccessfulLoginZamestnanec(Zamestnanec loggedInUser)
        {
            // Здесь вы можете выполнить действия, связанные с успешным входом пользователя,
            // например, открыть окно для пользователя или выполнить другие действия
           

           
            Console.WriteLine($"User {loggedInUser.Jmeno} logged in successfully.");

            // Пример: Открыть окно для пользователя

            // Закрыть окно входа
            CloseLoginWindow();
        }
    }
}