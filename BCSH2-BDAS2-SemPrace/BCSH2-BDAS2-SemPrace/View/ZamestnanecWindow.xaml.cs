using BCSH2_BDAS2_SemPrace.DataBase;
using BCSH2_BDAS2_SemPrace.Model;
using BCSH2_BDAS2_SemPrace.ViewModel;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace BCSH2_BDAS2_SemPrace.View
{
    /// <summary>
    /// Interaction logic for ZamestnanecWindow.xaml
    /// </summary>
    public partial class ZamestnanecWindow : Window
    {
        public Zamestnanec Zamestnanec { get; set; }

        public ZamestnanecWindow(Zamestnanec zamestnanec, String Pozice)
        {
            InitializeComponent();
            Zamestnanec = zamestnanec;
            DataContext = new ZamestnanecViewModel(zamestnanec); ;



            nameZam.Content = $"{Zamestnanec.Jmeno} {Zamestnanec.Prijmeni}";
            telZam.Content = $"Tel: {Zamestnanec.TelefoniCislo}";
            emailZam.Content = $"Email: {Zamestnanec.EmailZamestnanec}";
            praceZam.Content = $"{Pozice}";
            string pobockaName = GetPobockaName(Zamestnanec.IdZamestnanec);
            pobockaZam.Content = $"{pobockaName}"; string statusPopis = GetStatus(Zamestnanec.IdZamestnanec);
            statusZam.Content = $"{statusPopis}";
            PopulateKlientsList();
        }
        private string GetPobockaName(decimal id_zamestnanec)
        {
            OracleDatabaseService db = new OracleDatabaseService();
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

                return pobockaName;
            }
            finally
            {
                db.CloseConnection();
            }
        }

        private string GetStatus(decimal id_zamestnanec)
        {
            OracleDatabaseService db = new OracleDatabaseService();
            db.OpenConnection();

            OracleCommand cmd = db.Connection.CreateCommand();


            cmd.CommandText = "SELECT f.popis FROM status f WHERE f.id_status= (SELECT z.status_id_status FROM zamestnanec z WHERE z.id_zamestnanec = :id_zamestnanec)";
            cmd.Parameters.Add("id_zamestnanec", OracleDbType.Decimal).Value = id_zamestnanec;

            try
            {
                OracleDataReader reader = cmd.ExecuteReader();

                string status = "";

                if (reader.Read())
                {
                    status = reader["popis"].ToString();
                }
                reader.Close();
                return status;
            }
            finally
            {
                db.CloseConnection();
            }

        }

        private void PopulateKlientsList()
        {
            OracleDatabaseService db = new OracleDatabaseService();
            db.OpenConnection();

            OracleCommand cmd = db.Connection.CreateCommand();
            try
            {
                int zamId = Zamestnanec.IdZamestnanec;

                List<Klient> clients = db.GetHierarchyInfoFromDatabase(zamId);
                // Clear existing items in the ListView
                klientsList.Items.Clear();
                // Add new items to the ListView               
                klientsList.ItemsSource = clients;
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

        private void ExitClick(object sender, RoutedEventArgs e)
        {

            Console.WriteLine("Button clicked.");

            // Получаем DataContext
            ZamestnanecViewModel viewModel = DataContext as ZamestnanecViewModel;

            if (viewModel == null)
            {
                Console.WriteLine("ViewModel is null.");
                return;
            }

            // Вызываем метод LoginCommand.Execute
            if (viewModel.ExitZamestnanecCommand.CanExecute(null))
            {
                Console.WriteLine("Executing LoginCommand.");

                viewModel.ExitZamestnanecCommand.Execute(null);

            }
            else
            {
                Console.WriteLine("LoginCommand cannot execute.");
            }

        }


        private void ExitLog()
        {
            try
            {
                OracleDatabaseService db = new OracleDatabaseService();
                db.OpenConnection();


            }
            catch { }
            finally { }
        }
        private void PridatKlientClick(object sender, RoutedEventArgs e)
        {

        }
        private void PoslatFileClick(object sender, RoutedEventArgs e)
        {

        }
        private void EditKlientClick(object sender, RoutedEventArgs e)
        {

        }
        private void LogChanges(string operationType, string tableName, string columnName, string oldValue, string newValue)
        {
            OracleDatabaseService db = new OracleDatabaseService();
            db.OpenConnection();

            OracleCommand cmd = new OracleCommand();
            //cmd.Connection = db.;
            cmd.CommandText = "LogPackage.LogChange";
            cmd.CommandType = CommandType.StoredProcedure;

            // Add parameters
            cmd.Parameters.Add("p_operation_type", OracleDbType.Varchar2).Value = operationType;
            cmd.Parameters.Add("p_table_name", OracleDbType.Varchar2).Value = tableName;
            cmd.Parameters.Add("p_column_name", OracleDbType.Varchar2).Value = columnName;
            cmd.Parameters.Add("p_old_value", OracleDbType.Varchar2).Value = oldValue;
            cmd.Parameters.Add("p_new_value", OracleDbType.Varchar2).Value = newValue;
            cmd.Parameters.Add("p_changed_by", OracleDbType.Varchar2).Value = Zamestnanec.Jmeno + Zamestnanec.Prijmeni + Zamestnanec.IdZamestnanec;

            // Execute the procedure
            cmd.ExecuteNonQuery();

            db.CloseConnection();
        }
        private Zamestnanec GetZamestnanecInformationFromDatabase(decimal zamestnanecId)
        {
            OracleDatabaseService db = new OracleDatabaseService();
            db.OpenConnection();

            OracleCommand cmd = new OracleCommand();
            cmd.CommandText = "SELECT id_zamestnanec, jmeno, prijmeni, telefonni_cislo FROM zamestnanec WHERE id_zamestnanec = :zamestnanecId";
            cmd.Parameters.Add("zamestnanecId", OracleDbType.Decimal).Value = zamestnanecId;

            OracleDataReader reader = cmd.ExecuteReader();

            Zamestnanec zamestnanecModel = new Zamestnanec();

            if (reader.Read())
            {
                zamestnanecModel.IdZamestnanec = (int)reader["id_zamestnanec"];
                zamestnanecModel.Jmeno = reader["jmeno"].ToString();
                zamestnanecModel.Prijmeni = reader["prijmeni"].ToString();
                zamestnanecModel.TelefoniCislo = reader["telefonni_cislo"].ToString();
                // Добавьте остальные свойства модели данных
            }

            db.CloseConnection();

            return zamestnanecModel;
        }

        /*private Zamestnanec GetZamestnanecInformationFromDatabase(decimal zamestnanecId)
        {
            OracleDatabaseService db = new OracleDatabaseService();
            db.OpenConnection();

            OracleCommand cmd = new OracleCommand();
            // cmd.Connection = db.Connection;
            cmd.CommandText = "SELECT jmeno, prijmeni, telefonni_cislo FROM zamestnanec WHERE id_zamestnanec = :zamestnanecId";
            cmd.Parameters.Add("zamestnanecId", OracleDbType.Decimal).Value = zamestnanecId;

            OracleDataReader reader = cmd.ExecuteReader();

            Zamestnanec zamestnanec = new Zamestnanec();

            if (reader.Read())
            {
                zamestnanec.Id = (int)zamestnanecId;
                zamestnanec.Jmeno = reader["jmeno"].ToString();
                zamestnanec.Prijmeni = reader["prijmeni"].ToString();
            }

            db.CloseConnection();

            return zamestnanec;
        }*/
    }
}
