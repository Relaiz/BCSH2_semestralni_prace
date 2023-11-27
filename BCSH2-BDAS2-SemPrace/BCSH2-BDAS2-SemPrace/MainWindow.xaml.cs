using BCSH2_BDAS2_SemPrace.DataBase;
using BCSH2_BDAS2_SemPrace.Model;
using BCSH2_BDAS2_SemPrace.View;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Data;
using System.Text;
using System.Windows;

namespace BCSH2_BDAS2_SemPrace
{
    /// <summary>
    /// The entry point into the application, login window.
    /// </summary>
    public partial class MainWindow : Window
    {
        private OracleDatabaseService db;

        public MainWindow()
        {
            db = new OracleDatabaseService();
            db.OpenConnection();
            InitializeComponent();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string email = EmailTextBox.Text;
            string password = PasswordBox.Password;

            // Check if fields are empty
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Please enter both email and password.");
                return;
            }

            // Check if the user exists in the login table
            string userEmail = CheckUserCredentials(email, password);

            if (string.IsNullOrEmpty(userEmail))
            {
                MessageBox.Show("Invalid email or password. Please try again.");
                return;
            }

            // Determine the user type and open the corresponding window
            OpenUserWindow(userEmail);
        }

        private string CheckUserCredentials(string email, string password)
        {
            /*bool userExists = false;
            string connectionString = "Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=" +
              "(PROTOCOL=TCP)(HOST=fei-sql3.upceucebny.cz)(PORT=1521)))(CONNECT_DATA = " +
              "(SERVER = DEDICATED)(SID = BDAS)));" +
              "User Id = st67094; Password = Awphaiperbist1;";

            using (OracleConnection connection = new OracleConnection(connectionString))
            {
                string query = "SELECT COUNT(*) FROM login WHERE email = 'johndoe@gmail.com' AND heslo = 'password123'";

                using (OracleCommand command = new OracleCommand(query, connection))
                {
                    

                    try
                    {
                        connection.Open();
                        int count = Convert.ToInt32(command.ExecuteScalar());
                        userExists = count > 0;
                    }
                    catch (OracleException ex)
                    {
                        // Handle Oracle exception (e.g., logging, error handling)
                        Console.WriteLine("Oracle Error: " + ex.Message);
                    }
                }
            }

            return email;*/

            try
            {
                string query = $"SELECT email FROM login WHERE email = '{email}' AND heslo = '{password}'";

                DataTable result = db.ExecuteQuery(query);

                return result.Rows.Count > 0 ? result.Rows[0]["email"].ToString() : string.Empty;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return string.Empty;
            }
        }

        private void OpenUserWindow(string userEmail)
        {
            if (IsAdminUser(userEmail))
            {
                // Open Admin window
                //AdminWindow adminWindow = new AdminWindow();
                //adminWindow.Show();
            }
            else if (IsKlientUser(userEmail))
            {
                // Get Klient information
                Klient klient = GetKlientFromLogin(userEmail);

                // Open Klient window
                KlientWindow klientWindow = new KlientWindow(klient);
                klientWindow.Show();
            }
            else if (IsZamestnanecUser(userEmail))
            {
                // Get Zamestnanec information
                Zamestnanec zamestnanec = GetZamestnanecFromLogin(userEmail);

                // Open Zamestnanec window
                ZamestnanecWindow zamestnanecWindow = new ZamestnanecWindow(zamestnanec);
                zamestnanecWindow.Show();
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

        private Klient GetKlientFromLogin(string email)
        {
            string query = $"SELECT * FROM klient WHERE klient_email = '{email}'";
            DataTable result = db.ExecuteQuery(query);

            if (result.Rows.Count > 0)
            {
                // Assuming you have a Klient class with appropriate properties
                Klient klient = new Klient
                {
                    Id = Convert.ToInt32(result.Rows[0]["id_klient"]),
                    Jmeno = result.Rows[0]["jmeno"].ToString(),
                    Prijmeni = result.Rows[0]["prijmeni"].ToString(),
                    Email = result.Rows[0]["klient_email"].ToString(),
                    // Set other properties based on your Klient class
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
                    Id = Convert.ToInt32(result.Rows[0]["id_zamestnanec"]),
                    Jmeno = result.Rows[0]["jmeno"].ToString(),
                    Prijmeni = result.Rows[0]["prijmeni"].ToString(),
                    Email = result.Rows[0]["email_zamestnanec"].ToString(),
                    // Set other properties based on your Zamestnanec class
                };

                return zamestnanec;
            }

            return null;
        }
    }
}