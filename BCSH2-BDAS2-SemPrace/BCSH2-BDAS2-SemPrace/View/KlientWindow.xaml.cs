using BCSH2_BDAS2_SemPrace.DataBase;
using BCSH2_BDAS2_SemPrace.Model;
using Oracle.ManagedDataAccess.Client;
using System.Collections.Generic;
using System;
using System.Data;
using System.Windows;
using Oracle.ManagedDataAccess.Types;
using BCSH2_BDAS2_SemPrace.ViewModel;
using System.Windows.Controls;

namespace BCSH2_BDAS2_SemPrace.View
{
    /// <summary>
    /// Interaction logic for KlientWindow.xaml
    /// </summary>
    public partial class KlientWindow : Window
    {
        public Klient Klient { get; set; }
        public ListView AccountsList
        {
            get { return accountsList; }
        }
        public KlientWindow(Klient klient)
        {
            InitializeComponent();

            Klient = klient;

            // Create an instance of KlientViewModel and set it as the DataContext
            KlientViewModel viewModel = new KlientViewModel(klient);
            this.DataContext = viewModel;

            // Subscribe to the event
            ((KlientViewModel)DataContext).UpdateAccountsListRequested += (sender, args) => PopulateUctyListForKlient(klient.IdKlient);
        }

        private void SaveChanges_Click(object sender, RoutedEventArgs e)
        {
            // Call the PL/SQL procedure to get current Klient information
            Klient oldKlient = GetKlientInformationFromDatabase(Klient.IdKlient);

            // Update Klient information
            OracleDatabaseService db = new OracleDatabaseService();
            db.OpenConnection();

            OracleCommand cmd = new OracleCommand();
           // cmd.Connection = db.Connection;
            cmd.CommandText = "UpdateKlientInfo";
            cmd.CommandType = CommandType.StoredProcedure;

            // Add parameters
            cmd.Parameters.Add("p_klient_id", OracleDbType.Decimal).Value = Klient.IdKlient;
            cmd.Parameters.Add("p_jmeno", OracleDbType.Varchar2).Value = Klient.Jmeno;
            cmd.Parameters.Add("p_prijmeni", OracleDbType.Varchar2).Value = Klient.Prijmeni;
            cmd.Parameters.Add("p_telefonni_cislo", OracleDbType.Varchar2).Value = Klient.TelefoniCislo;

            // Execute the procedure
            cmd.ExecuteNonQuery();

            db.CloseConnection();

            // Log changes
            LogChanges("UPDATE", "klient", "jmeno", oldKlient.Jmeno, Klient.Jmeno);
            LogChanges("UPDATE", "klient", "prijmeni", oldKlient.Prijmeni, Klient.Prijmeni);
            LogChanges("UPDATE", "klient", "telefonni_cislo", oldKlient.TelefoniCislo, Klient.TelefoniCislo);

            MessageBox.Show("Changes saved successfully.");
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
            cmd.Parameters.Add("p_changed_by", OracleDbType.Varchar2).Value = Klient.Jmeno + Klient.Prijmeni + Klient.IdKlient;

            // Execute the procedure
            cmd.ExecuteNonQuery();

            db.CloseConnection();
        }

        private Klient GetKlientInformationFromDatabase(decimal klientId)
        {
            OracleDatabaseService db = new OracleDatabaseService();
            db.OpenConnection();

            OracleCommand cmd = new OracleCommand();
           // cmd.Connection = db.Connection;
            cmd.CommandText = "SELECT jmeno, prijmeni, telefonni_cislo FROM klient WHERE id_klient = :klientId";
            cmd.Parameters.Add("klientId", OracleDbType.Decimal).Value = klientId;

            OracleDataReader reader = cmd.ExecuteReader();

            Klient klient = new Klient();

            if (reader.Read())
            {
                klient.IdKlient = (int)klientId;
                klient.Jmeno = reader["jmeno"].ToString();
                klient.Prijmeni = reader["prijmeni"].ToString();
                klient.TelefoniCislo = reader["telefonni_cislo"].ToString();
            }

            db.CloseConnection();

            return klient;
        }

        private void PopulateUctyListForKlient(int klientId)
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

                // Clear existing items in the ListView
                accountsList.Items.Clear();

                // Create a list to store Ucty items
                List<Ucet> ucty = new List<Ucet>();

                // Populate the list with Ucty items
                while (reader.Read())
                {
                    Ucet ucet = new Ucet
                    {
                        // Map Ucty properties from the reader
                        // Example: UcetNumber = reader["ucet_number"].ToString(),
                    };
                    ucty.Add(ucet);
                }

                // Set the ItemsSource of the ListView to the Ucty list
                accountsList.ItemsSource = ucty;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error populating Ucty list for Klient: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                // Close the database connection
                db.CloseConnection();
            }
        }

    }
}
