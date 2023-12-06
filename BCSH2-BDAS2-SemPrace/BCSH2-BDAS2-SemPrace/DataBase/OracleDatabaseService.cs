using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oracle.ManagedDataAccess.Types;
using System.Security.Cryptography;
using BCSH2_BDAS2_SemPrace.Model;

namespace BCSH2_BDAS2_SemPrace.DataBase
{
    public class OracleDatabaseService
    {
        private readonly string connectionString;
        private OracleConnection connection;

        public OracleDatabaseService()
        {
            connectionString = "Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=" +
                "(PROTOCOL=TCP)(HOST=fei-sql3.upceucebny.cz)(PORT=1521)))(CONNECT_DATA = " +
                "(SERVER = DEDICATED)(SID = BDAS)));" +
                "User Id = st67094; Password = Awphaiperbist1;";
            connection = new OracleConnection(connectionString);
        }
        public OracleConnection Connection
        {
            get { return connection; }
        }
        public void OpenConnection()
        {
            try
            {
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                    Console.WriteLine("Database connection opened.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error opening database connection: {ex.Message}");
                throw;
            }
        }

        public void CloseConnection()
        {
            try
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                    Console.WriteLine("Database connection closed.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error closing database connection: {ex.Message}");
                throw;
            }
        }

        public DataTable ExecuteQuery(string query)
        {
            try
            {
                OpenConnection();
                Console.WriteLine($"Executing query: {query}");

                using (OracleCommand cmd = new OracleCommand(query, connection))
                {
                    using (OracleDataAdapter adapter = new OracleDataAdapter(cmd))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        Console.WriteLine($"Query result: {dataTable.Rows.Count} rows");

                        return dataTable;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error executing query: {ex.Message}");
                throw;
            }
            finally
            {
                CloseConnection();
            }
        }

        public int ExecuteNonQuery(string query)
        {
            try
            {
                OpenConnection();
                using (OracleCommand cmd = new OracleCommand(query, connection))
                {
                    return cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error executing non-query: {ex.Message}");
                throw;
            }
            finally
            {
                CloseConnection();
            }
        }
        public bool CheckUserCredentials(string email, string password)
        {
            try
            {
                using (OracleCommand cmd = new OracleCommand("CheckPassword", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Хеширование пароля перед передачей
                    // string hashedPassword = HashPassword(password);
                    cmd.Parameters.Add("result", OracleDbType.Boolean).Direction = ParameterDirection.ReturnValue;
                    // Входные параметры
                    cmd.Parameters.Add("p_email", OracleDbType.Varchar2).Value = email;
                    cmd.Parameters.Add("p_password", OracleDbType.Varchar2).Value = password;

                    // Выходной параметр


                    OpenConnection();
                    cmd.ExecuteNonQuery();

                    // Преобразование результата в bool
                    OracleBoolean oracleResult = (OracleBoolean)cmd.Parameters["result"].Value;
                    return (bool)oracleResult;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return false;
            }
            finally
            {
                CloseConnection();
            }
        }
       
        public Klient GetKlientByJmenoPrijmeni(string jmeno, string prijmeni)
        {
            OpenConnection();

            OracleCommand cmd = Connection.CreateCommand();
            cmd.CommandText = "SELECT * FROM Bankir_Klient_View WHERE jmeno = :jmeno AND prijmeni = :prijmeni";
            cmd.Parameters.Add("jmeno", OracleDbType.Varchar2).Value = jmeno;
            cmd.Parameters.Add("prijmeni", OracleDbType.Varchar2).Value = prijmeni;

            try
            {
                OracleDataReader reader = cmd.ExecuteReader();

                Klient klient = null;

                if (reader.Read())
                {
                    klient = new Klient
                    {

                        
                        CisloPrukazu = Convert.ToInt32(reader["cislo_prukazu"]),
                        Jmeno = reader["jmeno"].ToString(),
                        Prijmeni= reader["prijmeni"].ToString(),
                        KlientEmail= reader["klient_email"].ToString(),
                        Adresa = reader["adresa"].ToString()
                    };
                }
                  
                reader.Close();

                return klient;
            }
            finally
            {
                CloseConnection();
            }
        }
        public void CreateNewAccount(int klientId, string newAccountName)
        {
            try
            {
                OpenConnection();

                using (OracleCommand cmd = new OracleCommand("CreateNewAccount", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("p_klient_id", OracleDbType.Decimal).Value = klientId;
                    cmd.Parameters.Add("p_new_account_name", OracleDbType.Varchar2).Value = newAccountName;

                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating new account: {ex.Message}");
                throw;
            }
            finally
            {
                CloseConnection();
            }
        }

        // Update ExecuteQuery to take OracleCommand
        private DataTable ExecuteQuery(OracleCommand cmd)
        {
            try
            {
                OpenConnection();
                Console.WriteLine($"Executing query: {cmd.CommandText}");

                using (OracleDataAdapter adapter = new OracleDataAdapter(cmd))
                {
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    Console.WriteLine($"Query result: {dataTable.Rows.Count} rows");

                    return dataTable;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error executing query: {ex.Message}");
                throw;
            }
            finally
            {
                CloseConnection();
            }
        }

        public void OrderNewCard(int ucetId, string jmeno, string prijmeni, int cisloKarty, string platebniSystem, DateTime platnost, string typ)
        {
            try
            {
                using (OracleCommand cmd = Connection.CreateCommand())
                {
                    // Set the command text to call the PL/SQL procedure
                    cmd.CommandText = "OrderNewCard";
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Add parameters to the command
                    cmd.Parameters.Add("p_ucet_id_ucet", OracleDbType.Int32).Value = ucetId;
                    cmd.Parameters.Add("p_jmeno", OracleDbType.Varchar2).Value = jmeno;
                    cmd.Parameters.Add("p_prijmeni", OracleDbType.Varchar2).Value = prijmeni;
                    cmd.Parameters.Add("p_cislo_karty", OracleDbType.Int32).Value = cisloKarty;
                    cmd.Parameters.Add("p_platebni_system", OracleDbType.Varchar2).Value = platebniSystem;
                    cmd.Parameters.Add("p_platnost", OracleDbType.Date).Value = platnost;
                    cmd.Parameters.Add("p_typ", OracleDbType.Varchar2).Value = typ;

                    // Execute the procedure
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions (e.g., log, throw)
                Console.WriteLine($"Error ordering new card: {ex.Message}");
                // Optionally rethrow the exception if you want to handle it at a higher level
                throw;
            }
        }
        public Zustatek GetZustatekForUcet(int ucetId)
        {
            Zustatek zustatek = null;

            try
            {
                OpenConnection();

                OracleCommand cmd = Connection.CreateCommand();
                cmd.CommandText = "SELECT id_zustatek, volna_castka, blokovane_castka, \"date\", ucet_id_ucet FROM zustatek WHERE ucet_id_ucet = :p_ucet_id";
                cmd.Parameters.Add("p_ucet_id", OracleDbType.Int32).Value = ucetId;

                using (OracleDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        zustatek = new Zustatek
                        {
                            IdZustatek = Convert.ToInt32(reader["id_zustatek"]),
                            BlokovaneCastka = Convert.ToDecimal(reader["blokovane_castka"]),
                            VolnaCastka = Convert.ToDecimal(reader["volna_castka"]),
                            Datum = Convert.ToDateTime(reader["date"]),
                            IdUcet = Convert.ToInt32(reader["ucet_id_ucet"])
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting Zustatek for Ucet: {ex.Message}");
            }
            finally
            {
                CloseConnection();
            }

            return zustatek;
        }
    }
}
