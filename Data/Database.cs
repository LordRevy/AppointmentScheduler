using System;
using System.Data.Odbc;

namespace AppointmentScheduler.Data;

public abstract class Repository
{
    protected readonly string connectionString =
        "Driver={MySQL ODBC 8.0 Driver};Server=127.0.0.1;Port=3306;Database=client_schedule;User=sqlUser;Password=Passw0rd!;";

    protected OdbcConnection GetConnection()
    {
        return new OdbcConnection(connectionString);
    }
}
