using System;
using System.Data.Odbc;

namespace AppointmentScheduler.Data;

public abstract class Repository
{
    private readonly string _connectionString =
        ConfigurationManager.ConnectionStrings["ClientScheduleDb"].ConnectionString;

    protected OdbcConnection GetConnection()
    {
        return new OdbcConnection(_connectionString);
    }
}
