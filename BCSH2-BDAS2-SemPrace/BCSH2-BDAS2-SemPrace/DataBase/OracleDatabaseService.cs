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
        

        public List<Klient> GetHierarchyInfoFromDatabase(int id_zamestnanec)
        {
            try
            {
                

                // Создайте команду для вызова функции
                using (OracleCommand cmd = new OracleCommand("GetHierarchyInfo", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("result", OracleDbType.RefCursor).Direction = ParameterDirection.ReturnValue;
                    // Параметр для входного значения id_zamestnanec
                    cmd.Parameters.Add("p_id_zamestnanec", OracleDbType.Decimal).Value = id_zamestnanec;

                    // Параметр для выходного значения курсора
                    

                    // Выполните команду
                    using (OracleDataReader reader = cmd.ExecuteReader())
                    {
                        List<string> resultList = new List<string>();
                        string firstName="";
                        string lastName=""; 

                        while (reader.Read())
                        {
                            string full_name = reader["full_name"].ToString();
                            resultList.Add(full_name);
                        }
                        foreach (string fullName in resultList)
                        {
                            string[] names = fullName.Split(' ');
                            firstName = names[0];
                            lastName = names[1];

                        }

                        List<Klient> result = new List<Klient>();
                        result.Add(new Klient {Jmeno=firstName, Prijmeni=lastName });

                        return result;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return null; // или бросьте исключение в зависимости от вашего подхода
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
    }

}
