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
        private string connectionString;
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
            if (connection.State == System.Data.ConnectionState.Closed)
            {
                connection.Open();
            }
        }

        public void CloseConnection()
        {
            if (connection.State == System.Data.ConnectionState.Open)
            {
                connection.Close();
            }
        }

        public DataTable ExecuteQuery(string query)
        {
            using (OracleCommand cmd = new OracleCommand(query, connection))
            {
                using (OracleDataAdapter adapter = new OracleDataAdapter(cmd))
                {
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    return dataTable;
                }
            }
        }

        public int ExecuteNonQuery(string query)
        {
            using (OracleCommand cmd = new OracleCommand(query, connection))
            {
                return cmd.ExecuteNonQuery();
            }
        }
    }
}
