﻿using BCSH2_BDAS2_SemPrace.DataBase;
using BCSH2_BDAS2_SemPrace.Model;
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

            public ZamestnanecWindow(Zamestnanec zamestnanec)
            {
                InitializeComponent();
                Zamestnanec = zamestnanec;
                DataContext = Zamestnanec;
            }

            private void SaveChanges_Click(object sender, RoutedEventArgs e)
            {
                // Call the PL/SQL procedure to get current Zamestnanec information
                Zamestnanec oldZamestnanec = GetZamestnanecInformationFromDatabase(Zamestnanec.Id);

                // Update Zamestnanec information
                OracleDatabaseService db = new OracleDatabaseService();
                db.OpenConnection();

                OracleCommand cmd = new OracleCommand();
                // cmd.Connection = db.Connection;
                cmd.CommandText = "UpdateZamestnanecInfo";
                cmd.CommandType = CommandType.StoredProcedure;

                // Add parameters
                cmd.Parameters.Add("p_zamestnanec_id", OracleDbType.Decimal).Value = Zamestnanec.Id;
                cmd.Parameters.Add("p_jmeno", OracleDbType.Varchar2).Value = Zamestnanec.Jmeno;
                cmd.Parameters.Add("p_prijmeni", OracleDbType.Varchar2).Value = Zamestnanec.Prijmeni;

                // Execute the procedure
                cmd.ExecuteNonQuery();

                db.CloseConnection();

                // Log changes
                LogChanges("UPDATE", "zamestnanec", "jmeno", oldZamestnanec.Jmeno, Zamestnanec.Jmeno);
                LogChanges("UPDATE", "zamestnanec", "prijmeni", oldZamestnanec.Prijmeni, Zamestnanec.Prijmeni);

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
                cmd.Parameters.Add("p_changed_by", OracleDbType.Varchar2).Value = Zamestnanec.Jmeno + Zamestnanec.Prijmeni + Zamestnanec.Id;

                // Execute the procedure
                cmd.ExecuteNonQuery();

                db.CloseConnection();
            }

            private Zamestnanec GetZamestnanecInformationFromDatabase(decimal zamestnanecId)
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
            }
        }
    }
