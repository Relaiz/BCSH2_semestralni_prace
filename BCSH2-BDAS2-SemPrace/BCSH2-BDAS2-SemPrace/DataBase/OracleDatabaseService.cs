using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}