using System;
using System.Data.Odbc;

namespace AppointmentScheduler.Data;

public abstract class Database
{
    private readonly string _connectionString =
        ConfigurationManager.ConnectionStrings["ClientScheduleDb"].ConnectionString;

    /// <summary>
    /// Establishes the connection string to the Database.
    /// </summary>
    protected OdbcConnection GetConnection()
    {
        return new OdbcConnection(_connectionString);
    }

    /// <summary>
    /// Method to replace the ? placeholders with user inputs before executing a non-query.
    /// Takes the SQL string, database connection, and a list of user inputs as parameters.
    /// </summary>
    protected static void ExecuteNonQuery(string sql, OdbcConnection conn, params object?[] parameters)
    {
        using var cmd = new OdbcCommand(sql, conn);
    
        foreach (var p in parameters)
        {
            cmd.Parameters.AddWithValue("", p);
        }
        cmd.ExecuteNonQuery();
    }

    /// <summary>
    /// Returns the last ID generated during row creation.
    /// </summary>
    protected static int GetLastId(OdbcConnection conn)
    {
        using var cmd = new OdbcCommand("SELECT LAST_INSERT_ID();", conn);
        var obj = cmd.ExecuteScalar();
        return Convert.ToInt32(obj);
    }
}
