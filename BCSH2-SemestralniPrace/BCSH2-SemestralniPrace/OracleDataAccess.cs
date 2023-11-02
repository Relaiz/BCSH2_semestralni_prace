using Oracle.ManagedDataAccess.Client;
using System.Collections.Generic;

public class OracleDataAccess
{
    private string connectionString; // Read from configuration

    public OracleDataAccess(string connectionString)
    {
        this.connectionString = connectionString;
    }

    public IEnumerable<string> GetSomeData()
    {
        using (var connection = new OracleConnection(connectionString))
        {
            connection.Open();

            // Perform database operations (e.g., querying) here
            // ...

            connection.Close();
        }

        // Return data obtained from the database
        // ...
    }
}